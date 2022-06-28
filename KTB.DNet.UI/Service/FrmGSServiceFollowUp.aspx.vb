#Region " Customer Name Space "

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessValidation
Imports System.Collections.Generic

#End Region

Public Class FrmGSServiceFollowUp
    Inherits System.Web.UI.Page


#Region "PRIVATE VARIABLES"

    Private _sessHelper As SessionHelper = New SessionHelper
    Private enumStatus As ArrayList
    Private _userInfo As UserInfo
    Private viewPriv As Boolean
    Private inputPriv As Boolean
    Private intSvcReminderID As Integer

#End Region


#Region "Cek Privilege"

    Private Sub InitiateAuthorization()
        If CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_Input_Privilage) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Service")
            Else
                inputPriv = SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_Input_Privilage)
                viewPriv = True
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_View_Privilage) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Service")
            Else
                viewPriv = SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_View_Privilage)
                inputPriv = SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_Input_Privilage)
            End If
        End If

    End Sub

    Dim _dealerSystem As DealerSystems

#End Region



#Region "CUSTOM SUBS/FUNCS"

    Private Sub initSession()
        Dim svcReminderFUID As Integer = CInt(_sessHelper.GetSession("SVCREMINDERID"))
        intSvcReminderID = svcReminderFUID
        Dim svcReminder As ServiceReminder = New ServiceReminderFacade(User).Retrieve(svcReminderFUID)
        _sessHelper.SetSession("SVCREMINDER", svcReminder)
        _sessHelper.RemoveSession("ServiceBooking")
        'Dim svc As ServiceBooking = New ServiceBooking
        'Session("ServiceBooking") = svc
    End Sub

    Private Sub initDetailField()
        Dim svcReminder As ServiceReminder = CType(_sessHelper.GetSession("SVCREMINDER"), ServiceReminder)
        _userInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim isKTB As Boolean = IIf(_userInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)
        Dim totday As Double = 0
        Dim errMsg As String = String.Empty

        If Not ServiceReminderValidation.ValidateSvcReminder(svcReminder, errMsg) AndAlso Not isKTB Then
            MessageBox.Show(errMsg)
            Server.Transfer("~/Service" & _sessHelper.GetSession("backURL").ToString)
        End If

        lblDealerCode.Text = _userInfo.Dealer.DealerCode & " / " & _userInfo.Dealer.SearchTerm1
        lblConsumenName.Text = svcReminder.CustomerName & " / " & svcReminder.CustomerPhoneNumber
        lblPhone.Text = svcReminder.ContactPersonName & " / " & svcReminder.ContactPersonPhoneNumber
        lblReminderDate.Text = svcReminder.ServiceReminderDate
        lblRemark.Text = svcReminder.Remark
        binDdlStatus()
        lblChassisNo.Text = svcReminder.ChassisNumber
        lblEngineNo.Text = GetPersona(svcReminder.ChassisNumber)
        'lblEngineNo.Text = svcReminder.ContactPersonName & " / " & svcReminder.ContactPersonPhoneNumber 'svcReminder.EngineNumber
        lblVehicleType.Text = svcReminder.VehicleType
        lblOdometer.Text = getActualKM(svcReminder.ChassisNumber)
        lblRemark2.Text = svcReminder.PMKind.KindDescription
        txtWONumber.Text = svcReminder.WONumber

        'If getStatusDesc(svcReminder.Status) = "Complete" Then
        '    setInputField(False)
        'Else
        setInputField(True)
        'End If

        If Not inputPriv OrElse _sessHelper.GetSession("SVCMODE") = "VIEW" Then
            setInputField(False)
        End If

        icBookingDate.Enabled = False
        txtResponse.Enabled = False

        If Not IsNothing(svcReminder.CaseNumber) Then
            Dim critt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critt.opAnd(New Criteria(GetType(CustomerCase), "CaseNumber", MatchType.Exact, svcReminder.CaseNumber))
            Dim arrLstSR As ArrayList = New KTB.DNet.BusinessFacade.CustomerCaseFacade(User).Retrieve(critt)
            If arrLstSR.Count > 0 Then
                'objSR = CType(arrLstSR(0), ServiceReminder)
                btnCC.Visible = True
            Else
                btnCC.Visible = False
            End If
        Else
            btnCC.Visible = False
        End If

    End Sub

    Private Sub initRbtn()
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.GSR.Response"))
        Dim arrRespon As ArrayList = New StandardCodeFacade(User).Retrieve(criteria)
        _sessHelper.SetSession("ENUMRESPON", arrRespon)

        '_userInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        'Dim isKTB As Boolean = IIf(_userInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)

        'Dim statusListItemFirst As New ListItem("silahkan Pilih", -1)
        'ddlStatus.Items.Add(statusListItemFirst)

        For Each s As StandardCode In arrRespon
            If s.ValueCode <> "New" Then
                Dim ResponListItem As New ListItem(s.ValueDesc, s.ValueId)
                'ddlStatus.Items.Add(statusListItem)
                rbtnResponses.Items.Add(ResponListItem)
            End If
        Next
        Dim intCount As Integer = arrRespon.Count + 1
        Dim responListItemFirst As New ListItem("Respon Lain", intCount)
        rbtnResponses.Items.Add(responListItemFirst)
        'rbtnResponses.Items.Add("Respon Lain")
        rbtnResponses.SelectedValue = 1
    End Sub

    Private Function getActualKM(ByVal chassisNumber As String) As Integer
        Dim actualKMDS As DataSet = New ServiceReminderFacade(User).RetrieveSp("sp_GetServiceReminder_LastKM @ChassisNumber=" & chassisNumber)
        Dim result As Integer = 0
        If actualKMDS.Tables.Count > 0 Then
            Dim actualKMTbl As DataTable = actualKMDS.Tables(0)
            If actualKMTbl.Rows.Count > 0 Then
                result = CInt(actualKMTbl.Rows(0)("ActualKM"))
            End If
        End If

        Return result
    End Function

    Private Function GetPersona(ByVal chassisNumber As String) As String
        Dim objServicereminder As ServiceReminder = New ServiceReminder
        Dim strResult As String
        Dim ds As DataSet = New ServiceReminderFacade(User).GetPersona(chassisNumber)
        If ds.Tables(0).Rows.Count > 0 Then
            strResult = ds.Tables(0).Rows(0).Item("persona_name").ToString() & " / " & ds.Tables(0).Rows(0).Item("persona_phone").ToString()
        Else
            strResult = "" & " / " & ""
        End If
        Return strResult
    End Function

    Private Sub setInputField(ByVal stat As Boolean)
        ddlStatus.Enabled = stat
        icBookingDate.Enabled = stat
        rbtnResponses.Enabled = stat
        btnSave.Enabled = stat
        btnSB.Enabled = stat
        'btnCC.Enabled = stat
        btnEditPemilik.Enabled = stat
        btnEditKonsumen.Enabled = stat
        chBookingDate.Enabled = stat
    End Sub

    Private Sub binDdlStatus()
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "GlobalServiceReminder.Status"))
        Dim arrStatus As ArrayList = New StandardCodeFacade(User).Retrieve(criteria)
        _sessHelper.SetSession("ENUMSTATUS", arrStatus)

        _userInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim isKTB As Boolean = IIf(_userInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)

        Dim statusListItemFirst As New ListItem("silahkan Pilih", -1)
        ddlStatus.Items.Add(statusListItemFirst)

        For Each s As StandardCode In arrStatus
            If s.ValueCode <> "New" Then
                Dim statusListItem As New ListItem(s.ValueDesc, s.ValueId)
                ddlStatus.Items.Add(statusListItem)

                If Not isKTB AndAlso s.ValueId = 3 Then
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub initDgSvcReminderFU(Optional ByVal indexPage As Integer = 0)
        Dim totalRow As Integer = 0

        _userInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim isKTB As Boolean = IIf(_userInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminderFollowUp), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(ServiceReminderFollowUp), "ServiceReminder.ID", MatchType.Exact, CInt(_sessHelper.GetSession("SVCREMINDERID"))))

        If Not isKTB Then
            criteria.opAnd(New Criteria(GetType(ServiceReminderFollowUp), "FollowUpStatus", MatchType.LesserOrEqual, 3))
        End If

        Dim arrSvcReminderFU As ArrayList = New ServiceReminderFollowUpFacade(User).RetrieveActiveList(criteria, indexPage + 1, dgSvcReminderFU.PageSize, _
                                                                                                totalRow, "ID", Sort.SortDirection.DESC)

        _sessHelper.SetSession("ARRSVCREMINDERFU", arrSvcReminderFU)

        dgSvcReminderFU.CurrentPageIndex = indexPage
        dgSvcReminderFU.DataSource = arrSvcReminderFU
        dgSvcReminderFU.VirtualItemCount = totalRow
        dgSvcReminderFU.DataBind()
    End Sub

    Private Function getFollowUpActionStr() As String
        Dim strReturn As String = String.Empty

        If rbtnResponses.SelectedValue = 8 Then
            Dim txtResValue As String = txtResponse.Text
            If txtResValue = String.Empty OrElse String.IsNullOrWhiteSpace(txtResValue) Then
                MessageBox.Show("Silahkan isi response terlebih dahulu")
            Else
                strReturn = txtResValue
            End If
        Else
            strReturn = rbtnResponses.SelectedValue

            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.GSR.Response"))
            criteria.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, CInt(strReturn)))
            Dim arrResponse As ArrayList = New StandardCodeFacade(User).Retrieve(criteria)
            Dim objStandardCode As New StandardCode
            If Not IsNothing(arrResponse) AndAlso arrResponse.Count > 0 Then
                objStandardCode = CType(arrResponse(0), StandardCode)
                strReturn = objStandardCode.ValueDesc
            End If
            'If strReturn = 1 Then
            '    strReturn = "Follow Up Konsumen"
            'ElseIf strReturn = 2 Then
            '    strReturn = "Konsumen belum ada respon"
            'ElseIf strReturn = 3 Then
            '    strReturn = "Konsumen telah Booking service"
            'ElseIf strReturn = 4 Then
            '    strReturn = "Konsumen jadwal ulang service"
            'ElseIf strReturn = 5 Then
            '    strReturn = "Konsumen telah melakukan service di dealer lain"
            'ElseIf strReturn = 6 Then
            '    strReturn = "Konsumen telah datang melakukan service"
            'End If
        End If

        Return strReturn
    End Function

    Private Function getStatusDesc(statusID As Integer) As String
        Dim enumStatus As ArrayList = CType(_sessHelper.GetSession("ENUMSTATUS"), ArrayList)

        If IsNothing(enumStatus) Then
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "GlobalServiceReminder.Status"))
            enumStatus = CType(New StandardCodeFacade(User).Retrieve(criteria), ArrayList)
        End If

        For Each s As StandardCode In enumStatus
            If s.ValueId = statusID Then
                Return s.ValueDesc
            End If
        Next

        If statusID > 3 Then
            Return "Complete"
        End If

        Return ""
    End Function

    Private Function insertFollowUp(svcReminderFU As ServiceReminderFollowUp) As Integer
        Dim result As Integer = -1
        Dim svcReminderFUFacade As New ServiceReminderFollowUpFacade(User)

        result = svcReminderFUFacade.Insert(svcReminderFU)

        Return result
    End Function

    Private Function updateFollowUp(svcReminderFU As ServiceReminderFollowUp) As Integer
        Dim result As Integer = -1
        Dim svcReminderFUFacade As New ServiceReminderFollowUpFacade(User)

        result = svcReminderFUFacade.Update(svcReminderFU)

        Return result
    End Function

    Private Function updateSvcReminder(svcReminder As ServiceReminder) As Integer
        Dim result As Integer = -1

        If chBookingDate.Checked Then
            If icBookingDate.Value > svcReminder.MaxFUDealerDate Then
                svcReminder.MaxFUDealerDate = icBookingDate.Value
            End If
        End If

        Dim svcReminderFacade As New ServiceReminderFacade(User)
        result = svcReminderFacade.Update(svcReminder)

        Return result
    End Function

    Private Sub saveEditSvcReminderFU()
        Dim svcReminderFU As ServiceReminderFollowUp = CType(_sessHelper.GetSession("SVCREMINDERFU_EDIT"), ServiceReminderFollowUp)
        Dim errList As New List(Of ValidResult)
        Dim updateResult As Integer
        Dim svcReminder As ServiceReminder = CType(_sessHelper.GetSession("SVCREMINDER"), ServiceReminder)
        svcReminderFU.ServiceReminder = svcReminder

        If txtWONumber.Text.Trim <> "" Then
            svcReminder.WONumber = txtWONumber.Text.Trim
            svcReminderFU.ServiceReminder.WONumber = txtWONumber.Text.Trim
        End If

        If ddlStatus.SelectedValue = "2" Then
            svcReminder.WONumber = ""
            svcReminderFU.ServiceReminder.WONumber = ""
        End If

        svcReminder.Status = ddlStatus.SelectedValue
        svcReminderFU.ServiceReminder.Status = ddlStatus.SelectedValue
        svcReminderFU.FollowUpDate = DateTime.Now
        svcReminderFU.FollowUpStatus = ddlStatus.SelectedValue

        If chBookingDate.Checked Then
            svcReminderFU.BookingDate = icBookingDate.Value
        Else
            If getStatusDesc(svcReminderFU.ServiceReminder.Status) = "Complete" Then
                svcReminderFU.BookingDate = DateTime.Now
            Else
                svcReminderFU.BookingDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
            End If
        End If

        svcReminderFU.FollowUpAction = getFollowUpActionStr()

        If getStatusDesc(svcReminderFU.ServiceReminder.Status) = "Complete" Then
            svcReminderFU.ServiceReminder.Status = ddlStatus.SelectedValue
        End If

        'write to db
        If ServiceReminderValidation.ValidateFollowUp(svcReminderFU, errList) Then
            updateResult = updateFollowUp(svcReminderFU)
            updateResult = updateSvcReminder(svcReminder)
        Else
            updateResult = 0
        End If

        If updateResult > 0 Then
            MessageBox.Show("simpan data berhasil")
        Else
            Dim errStr As String = String.Empty
            For Each e As ValidResult In errList
                errStr = errStr & e.Message & "\n"
            Next
            MessageBox.Show("simpan data gagal \n" & errStr)
        End If
    End Sub

    Private Sub saveNewSvcReminderFU()
        Dim svcReminderFU As ServiceReminderFollowUp = New ServiceReminderFollowUp
        Dim svcBooking As ServiceBooking = New ServiceBooking
        Dim objSRFU As ServiceReminderFollowUp = New ServiceReminderFollowUp
        Dim objSR As ServiceReminder = New ServiceReminder
        Dim CC As CustomerCase = New CustomerCase
        Dim CCResponse As CustomerCaseResponse = New CustomerCaseResponse
        Dim errList As New List(Of ValidResult)
        Dim insertResult As Integer
        Dim svcReminder As ServiceReminder = CType(_sessHelper.GetSession("SVCREMINDER"), ServiceReminder)
        svcReminderFU.ServiceReminder = svcReminder
        Dim nResult As Integer = 0

        If ddlStatus.SelectedValue = "3" Then
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminderFollowUp), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(ServiceReminderFollowUp), "ServiceReminder.ID", MatchType.Exact, svcReminder.ID))
            ViewState("CurrentSortColumn") = "ServiceBooking.ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(ServiceReminderFollowUp), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
            Dim arrSvcReminderFU As ArrayList = New ServiceReminderFollowUpFacade(User).Retrieve(criteria, sortColl)
            If Not IsNothing(arrSvcReminderFU) AndAlso arrSvcReminderFU.Count > 0 Then
                objSRFU = CType(arrSvcReminderFU(0), ServiceReminderFollowUp)

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ServiceBooking), "ID", MatchType.Exact, objSRFU.ServiceBooking.ID))
                Dim arrSvcBooking As ArrayList = New ServiceBookingFacade(User).Retrieve(criterias)

                If Not IsNothing(arrSvcBooking) AndAlso arrSvcBooking.Count > 0 Then
                    svcBooking = CType(arrSvcBooking(0), ServiceBooking)
                    If svcBooking.Status = 1 Then
                        svcBooking.Status = 0
                        nResult = New ServiceBookingFacade(User).UpdateStatus(svcBooking.ID)
                        If nResult <> -1 Then
                            'kalo ada casenumber
                            'If Not IsNothing(svcReminder.CaseNumber) Then
                            '    Dim criteriass As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            '    criteriass.opAnd(New Criteria(GetType(CustomerCase), "CaseNumber", MatchType.Exact, svcReminder.CaseNumber))
                            '    Dim arrCC As ArrayList = New CustomerCaseFacade(User).Retrieve(criteriass)
                            '    If Not IsNothing(arrCC) AndAlso arrCC.Count > 0 Then
                            '        CC = CType(arrCC(0), CustomerCase)
                            '        CCResponse.CustomerCase = CC
                            '        CCResponse.ServiceBooking = Nothing
                            '        'If (txtWONumber.Text <> "") Then
                            '        '    CCResponse.WorkOrderNumber = txtWONumber.Text.ToString()
                            '        'Else
                            '        '    CCResponse.WorkOrderNumber = svcReminder.WONumber
                            '        'End If
                            '        CCResponse.WorkOrderNumber = svcReminder.WONumber
                            '        CCResponse.Subject = CC.Subject
                            '        CCResponse.Description = ""
                            '        CCResponse.Response = 0
                            '        CCResponse.BookingDatetime = "1753-01-01"
                            '        CCResponse.Status = 4
                            '        CCResponse.IsSend = 0
                            '        CCResponse.RowStatus = 0
                            '        nResult = New CustomerCaseResponseFacade(User).Insert(CCResponse) 'insert new row customercaseresponse
                            '        If nResult <> -1 Then
                            '            CC.Status = 4
                            '            nResult = New CustomerCaseFacade(User).Update(CC) 'update status casemanagement
                            '        End If
                            '    End If
                            'End If
                        End If
                    End If
                End If

            End If


        End If

        If txtWONumber.Text.Trim <> "" Then
            svcReminder.WONumber = txtWONumber.Text.Trim
            svcReminderFU.ServiceReminder.WONumber = txtWONumber.Text.Trim
        End If

        If ddlStatus.SelectedValue = "2" Then
            svcReminder.WONumber = ""
            svcReminderFU.ServiceReminder.WONumber = ""
        End If




        svcReminder.Status = ddlStatus.SelectedValue
        svcReminderFU.ServiceReminder.Status = ddlStatus.SelectedValue
        svcReminderFU.FollowUpDate = DateTime.Now
        svcReminderFU.FollowUpStatus = ddlStatus.SelectedValue
        If chBookingDate.Checked Then
            svcReminderFU.BookingDate = icBookingDate.Value
        Else
            svcReminderFU.BookingDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        End If

        svcReminderFU.FollowUpAction = getFollowUpActionStr()

        If ServiceReminderValidation.ValidateFollowUp(svcReminderFU, errList) Then
            insertResult = insertFollowUp(svcReminderFU)
            insertResult = updateSvcReminder(svcReminder)
        Else
            insertResult = 0
        End If

        If insertResult > 0 Then
            MessageBox.Show("simpan data berhasil")
        Else
            Dim errStr As String = String.Empty
            For Each e As ValidResult In errList
                errStr = errStr & e.Message & "\n"
            Next
            MessageBox.Show("simpan data gagal \n" & errStr)
        End If
    End Sub

    Private Sub completeFU()
        Dim svcReminderFU As ServiceReminderFollowUp = New ServiceReminderFollowUp
        Dim errList As New List(Of ValidResult)
        Dim insertResult As Integer
        Dim svcReminder As ServiceReminder = CType(_sessHelper.GetSession("SVCREMINDER"), ServiceReminder)
        svcReminderFU.ServiceReminder = svcReminder

        If txtWONumber.Text.Trim <> "" Then
            svcReminder.WONumber = txtWONumber.Text.Trim
            svcReminderFU.ServiceReminder.WONumber = txtWONumber.Text.Trim
        End If

        If ddlStatus.SelectedValue = "2" Then
            svcReminder.WONumber = ""
            svcReminderFU.ServiceReminder.WONumber = ""
        End If




        svcReminder.Status = ddlStatus.SelectedValue
        svcReminderFU.ServiceReminder.Status = ddlStatus.SelectedValue
        svcReminderFU.FollowUpDate = DateTime.Now
        svcReminderFU.FollowUpStatus = ddlStatus.SelectedValue
        If chBookingDate.Checked Then
            svcReminderFU.BookingDate = icBookingDate.Value
        Else
            svcReminderFU.BookingDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        End If

        svcReminderFU.FollowUpAction = getFollowUpActionStr()

        If ServiceReminderValidation.ValidateFollowUp(svcReminderFU, errList) Then
            insertResult = insertFollowUp(svcReminderFU)
            insertResult = updateSvcReminder(svcReminder)
        Else
            insertResult = 0
        End If

        If insertResult > 0 Then
            MessageBox.Show("simpan data berhasil")
        Else
            Dim errStr As String = String.Empty
            For Each e As ValidResult In errList
                errStr = errStr & e.Message & "\n"
            Next
            MessageBox.Show("simpan data gagal \n" & errStr)
        End If
    End Sub

    Private Function GetStatusSVCReminder() As Integer
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(ServiceReminder), "ID", MatchType.Exact, _sessHelper.GetSession("SVCREMINDERID")))
        Dim arrSvcReminder As ArrayList = New ServiceReminderFacade(User).Retrieve(criteria)
        Dim objSvcReminder As New ServiceReminder
        Dim intStatus As Integer = 0
        If Not IsNothing(arrSvcReminder) AndAlso arrSvcReminder.Count > 0 Then
            objSvcReminder = CType(arrSvcReminder(0), ServiceReminder)
            intStatus = objSvcReminder.Status
        End If
        Return intStatus
    End Function

    Private Function GetDayConfig() As Integer
        Dim intDay As Integer = 0
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, "ServiceBooking.AvailableDayEdit"))
        Dim arrLst As ArrayList = New AppConfigFacade(User).Retrieve(crit)

        If arrLst.Count > 0 Then
            Dim appconf As AppConfig = CType(arrLst(0), AppConfig)
            intDay = CInt(appconf.Value)
        End If
        Return intDay
    End Function

    Private Function GetServiceBooking(ByVal intIDSB As Integer) As KTB.DNet.Domain.ServiceBooking
        Dim objSB As New ServiceBooking
        'If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
        '    objStandardCode = CType(arrDDL(0), StandardCode)
        '    strPickUpType = objStandardCode.ValueDesc
        'End If
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ServiceBooking), "ID", MatchType.Exact, intIDSB))
        Dim arrLst As ArrayList = New ServiceBookingFacade(User).Retrieve(crit)

        If Not IsNothing(arrLst) AndAlso arrLst.Count > 0 Then
            objSB = CType(arrLst(0), ServiceBooking)
        End If
        Return objSB
    End Function

    Private Function GetLastServiceBooking(ByVal intIDSR As Integer) As Boolean
        Dim objSR As New KTB.DNet.Domain.ServiceReminderFollowUp
        Dim boolLastSvcBooking As Boolean = False
        'If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
        '    objStandardCode = CType(arrDDL(0), StandardCode)
        '    strPickUpType = objStandardCode.ValueDesc
        'End If
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminderFollowUp), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ServiceReminderFollowUp), "ServiceReminder.ID", MatchType.Exact, intIDSR))
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        Dim arrLst As ArrayList = New ServiceReminderFollowUpFacade(User).Retrieve(crit)
        If (arrLst.Count > 0) Then
            For i As Integer = 0 To arrLst.Count - 1
                If Not IsNothing(arrLst) AndAlso arrLst.Count > 0 Then
                    objSR = CType(arrLst(i), ServiceReminderFollowUp)
                    If Not IsNothing(objSR.ServiceBooking) Then
                        Dim objSB As New KTB.DNet.Domain.ServiceBooking
                        objSB = GetServiceBooking(objSR.ServiceBooking.ID)
                        If Not IsNothing(objSB) Then
                            If (objSB.Status = 1) Then
                                'disini cek tanggalnya dulu
                                Dim intDayOff As Integer = GetDayConfig()
                                'Dim objSB As ServiceBooking = CType(_sessHelper.GetSession("objSB"), ServiceBooking)
                                Dim Tanggal As Date = objSB.IncomingDateStart.Date
                                Dim intSelisihTgl As Integer = (Tanggal - Date.Now.Date).TotalDays
                                If (intSelisihTgl >= intDayOff) Then
                                    boolLastSvcBooking = True
                                    _sessHelper.SetSession("objSB", objSB)
                                    _sessHelper.SetSession("strSRFUID", objSR.ID)
                                    Exit For
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        End If
        'If Not IsNothing(arrLst) AndAlso arrLst.Count > 0 Then
        '    For Each s As ServiceReminderFollowUp In arrLst
        '        If Not IsNothing(s.ServiceBooking) Then
        '            objSR = CType(arrLst(0), ServiceReminderFollowUp)
        '            Exit For
        '        End If
        '    Next
        'End If
        Return boolLastSvcBooking
    End Function
    

#End Region

#Region "EVENT HANDLER"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            txtWONumber.Enabled = False

            'If (GetStatusSVCReminder() <= 2) Then
            '    btnSB.Enabled = True
            'Else
            '    btnSB.Enabled = False
            'End If
            initRbtn()
            initSession()
            initDetailField()
            initDgSvcReminderFU()

        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        'Dim objGSR As New ServiceReminder
        'Dim CCResponse As New CustomerCaseResponse
        'Dim CC As New CustomerCase

        '_sessHelper.RemoveSession("SVCREMINDERFU_EDIT")
        'Dim strS As String = _sessHelper.GetSession("backURL").ToString()

        'If Not IsNothing(_sessHelper.GetSession("modeForm")) Then
        '    If (_sessHelper.GetSession("modeForm") = "CC") Then
        '        Dim strMode As String = _sessHelper.GetSession("mode").ToString()
        '        If Not IsNothing(_sessHelper.GetSession("SVCREMINDERID")) Then
        '            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            criteria.opAnd(New Criteria(GetType(ServiceReminder), "ID", MatchType.Exact, _sessHelper.GetSession("SVCREMINDERID")))
        '            Dim sortt As Sort = New Sort(GetType(ServiceReminder), "ID", sortt.SortDirection.DESC)
        '            Dim sortst As SortCollection = New SortCollection
        '            sortst.Add(sortt)
        '            Dim arrLst As ArrayList = New KTB.DNet.BusinessFacade.Service.ServiceReminderFacade(User).Retrieve(criteria, sortst)
        '            If Not IsNothing(arrLst) AndAlso arrLst.Count > 0 Then
        '                objGSR = CType(arrLst(0), ServiceReminder)
        '                If Not IsNothing(objGSR.CaseNumber) Then
        '                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '                    criterias.opAnd(New Criteria(GetType(CustomerCase), "CaseNumber", MatchType.Exact, objGSR.CaseNumber))
        '                    Dim sort As Sort = New Sort(GetType(CustomerCase), "ID", sort.SortDirection.DESC)
        '                    Dim sorts As SortCollection = New SortCollection
        '                    sorts.Add(sort)
        '                    Dim arrLstcc As ArrayList = New KTB.DNet.BusinessFacade.CustomerCaseFacade(User).Retrieve(criterias, sorts)
        '                    If Not IsNothing(arrLstcc) AndAlso arrLstcc.Count > 0 Then
        '                        CC = CType(arrLstcc(0), CustomerCase)

        '                        If (strMode = "edit") Then
        '                            Server.Transfer("~/Marketing/FrmCustomerCaseResponse.aspx?mode=edit&caseId=" & CC.ID)
        '                        Else
        '                            Server.Transfer("~/Marketing/FrmCustomerCaseResponse.aspx?mode=view&caseId=" & CC.ID)
        '                        End If

        '                        'Server.Transfer(_sessHelper.GetSession("backURL").ToString)
        '                    End If
        '                End If
        '            End If
        '        End If

        '    End If
        'Else
        '    Server.Transfer("~/Service" & _sessHelper.GetSession("backURL").ToString)
        'End If
        _sessHelper.SetSession("backURL", "/FrmGSDaftarService.aspx") 'back
        Server.Transfer("~/Service" & _sessHelper.GetSession("backURL").ToString)
    End Sub

    Protected Sub btnCC_Click(sender As Object, e As EventArgs) Handles btnCC.Click
        Dim objGSR As New ServiceReminder
        Dim CCResponse As New CustomerCaseResponse
        Dim CC As New CustomerCase
        'Dim svcReminder As ServiceReminder = CType(_sessHelper.GetSession("SVCREMINDER"), ServiceReminder)
     
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(ServiceReminder), "ID", MatchType.Exact, CInt(_sessHelper.GetSession("SVCREMINDERID"))))
        Dim sortt As Sort = New Sort(GetType(ServiceReminder), "ID", sortt.SortDirection.DESC)
        Dim sortst As SortCollection = New SortCollection
        sortst.Add(sortt)
        Dim arrLstSR As ArrayList = New KTB.DNet.BusinessFacade.Service.ServiceReminderFacade(User).Retrieve(criteria, sortst)
        If Not IsNothing(arrLstSR) AndAlso arrLstSR.Count > 0 Then
            objGSR = CType(arrLstSR(0), ServiceReminder)
        End If

        If Not IsNothing(objGSR.CaseNumber) Then
            Dim critt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerCase), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critt.opAnd(New Criteria(GetType(CustomerCase), "CaseNumber", MatchType.Exact, objGSR.CaseNumber))
            Dim arrLstCC As ArrayList = New KTB.DNet.BusinessFacade.CustomerCaseFacade(User).Retrieve(critt)
            If arrLstCC.Count > 0 Then
                CC = CType(arrLstCC(0), CustomerCase)
                If (CC.Status <> 4) Then
                    _sessHelper.SetSession("backURL", "~/Service/FrmGSServiceFollowUp.aspx")
                    _sessHelper.SetSession("modeForm", "GSR")
                    _sessHelper.SetSession("model", _sessHelper.GetSession("Sessmode"))
                    _sessHelper.SetSession("caseId", CC.ID)
                    _sessHelper.SetSession("Sessmode", "VIEW")
                    If Not IsNothing(_sessHelper.GetSession("caseId")) Then
                        'Response.Redirect("~/Marketing/FrmCustomerCaseResponse.aspx")
                        Server.Transfer("~/Marketing/FrmCustomerCaseResponse.aspx?mode=edit&caseId=" & CC.ID)
                    End If
                    'Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=View&CCID=" + CCResponse.ID.ToString())
                Else
                    _sessHelper.SetSession("backURL", "~/Service/FrmGSServiceFollowUp.aspx")
                    _sessHelper.SetSession("modeForm", "GSR")
                    _sessHelper.SetSession("model", _sessHelper.GetSession("Sessmode"))
                    _sessHelper.SetSession("caseId", CC.ID)
                    _sessHelper.SetSession("Sessmode", "EDIT")
                    If Not IsNothing(_sessHelper.GetSession("caseId")) Then
                        'Response.Redirect("~/Marketing/FrmCustomerCaseResponse.aspx")
                        Server.Transfer("~/Marketing/FrmCustomerCaseResponse.aspx?mode=view&caseId=" & CC.ID)
                    End If
                    'Response.Redirect("~/Service/FrmInputServiceBooking.aspx?menufrom=CustomerCase&Mode=View&CCID=" + CCResponse.ID.ToString())
                End If
            End If
        End If
    End Sub

    Protected Sub btnSB_Click(sender As Object, e As EventArgs) Handles btnSB.Click
        'Dim svc As ServiceBooking = New ServiceBooking
        'Session("ServiceBooking") = svc
        'svc.ChassisMaster = 1
        '_sessHelper.SetSession("ServiceBooking") = svc
        '_sessHelper.SetSession("SVCMODE", "EDIT")
        Dim svcReminderFU As New ServiceReminderFollowUp
        Dim arrSvcReminderFU As ArrayList
        If Not IsNothing(_sessHelper.GetSession("ARRSVCREMINDERFU")) Then
            arrSvcReminderFU = CType(_sessHelper.GetSession("ARRSVCREMINDERFU"), ArrayList)
            svcReminderFU = arrSvcReminderFU(0)
        End If
        If Not IsNothing(_sessHelper.GetSession("SVCREMINDERID")) Then
            Dim strServiceReminderID = ""
            strServiceReminderID = _sessHelper.GetSession("SVCREMINDERID")
            If Not IsNothing(_sessHelper.GetSession("SVCMODE")) Then
                Dim strSVCMode = ""
                strSVCMode = _sessHelper.GetSession("SVCMODE")
                If strSVCMode = "EDIT" Then
                    'Dim objSR As ServiceReminderFollowUp = GetLastServiceBooking(svcReminderFU.ServiceReminder.ID)
                    Dim boolLastSvc As Boolean = GetLastServiceBooking(svcReminderFU.ServiceReminder.ID)
                    If (boolLastSvc = True) Then
                        Dim intDayOff As Integer = GetDayConfig()
                        Dim objSB As ServiceBooking = CType(_sessHelper.GetSession("objSB"), ServiceBooking)
                        Dim Tanggal As Date = objSB.IncomingDateStart.Date
                        Dim intSelisihTgl As Integer = (Tanggal - Date.Now.Date).TotalDays

                        If (objSB.Status = 0) Then
                            Response.Redirect("FrmInputServiceBooking.aspx?menufrom=GSR&Mode=New&FUID=" + svcReminderFU.ID.ToString())
                        Else
                            If (intSelisihTgl >= intDayOff) Then
                                Response.Redirect("FrmInputServiceBooking.aspx?menufrom=GSR&Mode=Edit&FUID=" + _sessHelper.GetSession("strSRFUID").ToString())
                            Else
                                Response.Redirect("FrmInputServiceBooking.aspx?menufrom=GSR&Mode=New&FUID=" + svcReminderFU.ID.ToString())
                            End If
                        End If

                        
                    Else
                        Response.Redirect("FrmInputServiceBooking.aspx?menufrom=GSR&Mode=New&FUID=" + svcReminderFU.ID.ToString())
                    End If
                Else
                    Response.Redirect("FrmInputServiceBooking.aspx?menufrom=GSR&Mode=View&FUID=" + svcReminderFU.ID.ToString())
                End If

            End If
        End If


    End Sub

    Protected Sub btnHidden_Click(sender As Object, e As EventArgs) Handles btnHidden.Click
        initSession()
        initDetailField()
        initDgSvcReminderFU()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ddlStatus.SelectedValue = -1 Then
            MessageBox.Show("Silahkan pilih status!")
            Exit Sub
        End If

        If Not IsNothing(_sessHelper.GetSession("SVCREMINDERFU_EDIT")) Then
            saveEditSvcReminderFU()
        Else
            saveNewSvcReminderFU()
        End If

        If (ddlStatus.SelectedItem.Text.ToString = "Complete") Then
            'completeFU()
            _sessHelper.SetSession("SVCMODE", "VIEW")
            setInputField(False)
            'initDetailField()
        End If

        _sessHelper.RemoveSession("SVCREMINDERFU_EDIT")
        rbtnResponses.SelectedValue = "1"
        icBookingDate.Value = DateTime.Now
        ddlStatus.SelectedValue = -1
        txtResponse.Text = ""
        txtWONumber.Text = ""
        txtWONumber.Enabled = False
        chBookingDate.Checked = False
        icBookingDate.Enabled = False

        initDgSvcReminderFU()
        'initDetailField()
      
    End Sub

    Protected Sub dgSvcReminderFU_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgSvcReminderFU.ItemDataBound
        Dim lblNo As Label
        Dim lblStatus As Label
        Dim lblResponse As Label
        Dim lblBookingSvcDate As Label
        Dim lblUpdateDate As Label
        Dim lblUserUpdate As Label
        Dim lblNoServiceBooking As Label
        Dim lbtnEdit As LinkButton
        Dim lbServiceBooking As LinkButton

        Dim index = e.Item.ItemIndex
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        Dim arrSvcReminderFU As ArrayList = CType(_sessHelper.GetSession("ARRSVCREMINDERFU"), ArrayList)
        Dim svcReminderFU As New ServiceReminderFollowUp

        If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem Then
            lblNo = CType(e.Item.FindControl("lblNo"), Label)
            lblStatus = CType(e.Item.FindControl("lblStatus"), Label)
            lblResponse = CType(e.Item.FindControl("lblResponse"), Label)
            lblBookingSvcDate = CType(e.Item.FindControl("lblBookingSvcDate"), Label)
            lblUpdateDate = CType(e.Item.FindControl("lblUpdateDate"), Label)
            lblUserUpdate = CType(e.Item.FindControl("lblUserUpdate"), Label)
            lblNoServiceBooking = CType(e.Item.FindControl("lblNoServiceBooking"), Label)
            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbServiceBooking = CType(e.Item.FindControl("lbServiceBooking"), LinkButton)

            svcReminderFU = arrSvcReminderFU(index)

            lblNo.Text = index + 1
            lblStatus.Text = getStatusDesc(svcReminderFU.FollowUpStatus)
            lblResponse.Text = svcReminderFU.FollowUpAction

            If svcReminderFU.BookingDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                lblBookingSvcDate.Text = ""
            Else
                lblBookingSvcDate.Text = svcReminderFU.BookingDate
            End If
            If Not IsNothing(svcReminderFU.ServiceBooking) Then
                lblNoServiceBooking.Text = svcReminderFU.ServiceBooking.ServiceBookingCode
                lbServiceBooking.Text = svcReminderFU.ServiceBooking.ServiceBookingCode
                lbtnEdit.Visible = False
            Else
                lblNoServiceBooking.Text = ""
            End If
            lblUpdateDate.Text = svcReminderFU.FollowUpDate
            lblUserUpdate.Text = svcReminderFU.CreatedBy

            If lblStatus.Text = "New" Then
                lbtnEdit.Visible = False
            End If

            If index + 1 = 1 Then
                'If getStatusDesc(svcReminderFU.FollowUpStatus) = "Complete" Then
                '    setInputField(False)
                'End If

                If CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title = EnumDealerTittle.DealerTittle.KTB OrElse _sessHelper.GetSession("SVCMODE") = "View" Then
                    lbtnEdit.Visible = False
                End If
            Else
                lbtnEdit.Visible = False
            End If
        End If
    End Sub

    Protected Sub dgSvcReminderFU_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgSvcReminderFU.ItemCommand
        If e.CommandName = "Edit" Then
            Dim arrSvcReminderFU As ArrayList = CType(_sessHelper.GetSession("ARRSVCREMINDERFU"), ArrayList)
            Dim svcReminderFU As ServiceReminderFollowUp = CType(arrSvcReminderFU(e.Item.ItemIndex), ServiceReminderFollowUp)

            If Not inputPriv OrElse _sessHelper.GetSession("SVCMODE") = "VIEW" Then 'OrElse getStatusDesc(svcReminderFU.FollowUpStatus) = "Complete" Then
                setInputField(False)
            Else
                setInputField(True)
                If svcReminderFU.BookingDate.Year <> 1753 Then
                    chBookingDate.Checked = True
                End If

            End If

            If getStatusDesc(svcReminderFU.FollowUpStatus) = "Complete" AndAlso Not IsKTB() Then
                txtWONumber.Enabled = True
            End If
            ddlStatus.SelectedValue = svcReminderFU.FollowUpStatus
            If svcReminderFU.BookingDate.Year = 1753 Then
                icBookingDate.Value = DateTime.Now
            Else
                icBookingDate.Value = svcReminderFU.BookingDate
            End If

            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.GSR.Response"))
            criteria.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, svcReminderFU.FollowUpAction))
            Dim arrResponse As ArrayList = New StandardCodeFacade(User).Retrieve(criteria)
            Dim objStandardCode As New StandardCode
            If Not IsNothing(arrResponse) AndAlso arrResponse.Count > 0 Then
                objStandardCode = CType(arrResponse(0), StandardCode)
                rbtnResponses.SelectedValue = objStandardCode.ValueId
            Else
                rbtnResponses.SelectedValue = "7"
                txtResponse.Text = svcReminderFU.FollowUpAction
            End If

            'If svcReminderFU.FollowUpAction = "Follow Up Konsumen" Then
            '    rbtnResponses.SelectedValue = "1"
            'ElseIf svcReminderFU.FollowUpAction = "Konsumen belum ada respon" Then
            '    rbtnResponses.SelectedValue = "2"
            'ElseIf svcReminderFU.FollowUpAction = "Konsumen telah Booking service" Then
            '    rbtnResponses.SelectedValue = "3"
            'ElseIf svcReminderFU.FollowUpAction = "Konsumen jadwal ulang service" Then
            '    rbtnResponses.SelectedValue = "4"
            'ElseIf svcReminderFU.FollowUpAction = "Konsumen telah melakukan service di dealer lain" Then
            '    rbtnResponses.SelectedValue = "5"
            'ElseIf svcReminderFU.FollowUpAction = "Konsumen telah datang melakukan service" Then
            '    rbtnResponses.SelectedValue = "6"
            'Else
            '    rbtnResponses.SelectedValue = "7"
            '    txtResponse.Text = svcReminderFU.FollowUpAction
            'End If

            _sessHelper.RemoveSession("SVCREMINDERFU_EDIT")
            _sessHelper.SetSession("SVCREMINDERFU_EDIT", svcReminderFU)
        ElseIf e.CommandName = "View" Then
            Dim arrSvcReminderFU As ArrayList = CType(_sessHelper.GetSession("ARRSVCREMINDERFU"), ArrayList)
            Dim svcReminderFU As ServiceReminderFollowUp = CType(arrSvcReminderFU(e.Item.ItemIndex), ServiceReminderFollowUp)
            Response.Redirect("FrmInputServiceBooking.aspx?menufrom=GSR&Mode=View&FUID=" + svcReminderFU.ID.ToString())
            _sessHelper.RemoveSession("SVCREMINDERFU_EDIT")
            _sessHelper.SetSession("SVCREMINDERFU_EDIT", svcReminderFU)
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        _sessHelper.RemoveSession("SVCREMINDERFU_EDIT")
        rbtnResponses.SelectedValue = "1"
        icBookingDate.Value = DateTime.Now
        icBookingDate.Enabled = False
        chBookingDate.Checked = False
        ddlStatus.SelectedValue = -1
        txtResponse.Text = ""
    End Sub

    Protected Sub chBookingDate_CheckedChanged(sender As Object, e As EventArgs) Handles chBookingDate.CheckedChanged
        If chBookingDate.Checked Then
            icBookingDate.Enabled = True
        Else
            icBookingDate.Enabled = False
        End If
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        If ddlStatus.SelectedValue = 3 Then
            txtWONumber.Enabled = True
            icBookingDate.Value = DateTime.Now
            icBookingDate.Enabled = False
            chBookingDate.Checked = False
        ElseIf ddlStatus.SelectedValue = 2 Then
            Dim arrSvcReminderFU As ArrayList = CType(_sessHelper.GetSession("ARRSVCREMINDERFU"), ArrayList)
            If arrSvcReminderFU.Count > 0 Then
                Dim svcReminderFU As ServiceReminderFollowUp = arrSvcReminderFU(0)
                If svcReminderFU.BookingDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                    icBookingDate.Value = svcReminderFU.BookingDate
                    chBookingDate.Checked = True
                End If
            End If

            txtWONumber.Enabled = False
            txtWONumber.Text = String.Empty
        Else
            txtWONumber.Enabled = False
            txtWONumber.Text = String.Empty
            chBookingDate.Checked = False
        End If
    End Sub

    Protected Sub rbtnResponses_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbtnResponses.SelectedIndexChanged
        If rbtnResponses.SelectedValue = 8 Then
            txtResponse.Enabled = True
        Else
            txtResponse.Enabled = False
            txtResponse.Text = ""
        End If
    End Sub

#End Region

    Protected Sub btnEditPemilik_Click(sender As Object, e As ImageClickEventArgs)
        RegisterStartupScript("Open", String.Format("<script>ShowPopUpEdit({0},'{1}');</script>", _
                            _sessHelper.GetSession("SVCREMINDERID"), "Pemilik"))
        'btnHidden_Click(sender, e)
        Return
      End Sub

    Protected Sub btnEditKonsumen_Click(sender As Object, e As ImageClickEventArgs)
        RegisterStartupScript("Open", String.Format("<script>ShowPopUpEdit({0},'{1}');</script>", _
                            _sessHelper.GetSession("SVCREMINDERID"), "Konsumen"))
        'btnHidden_Click(sender, e)
        Return
    End Sub

    Protected Sub btnHidden_Click1(sender As Object, e As EventArgs)

    End Sub
End Class