#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Helper
#End Region

Public Class FrmInputBabitEventReport
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"

    Dim objDealer As New Dealer
    Dim objLoginUser As UserInfo
    Dim objEventReportHeader As BabitEventReportHeader

    Private arlEventReportDtl As ArrayList = New ArrayList
    Private arlEventReportDoc As ArrayList = New ArrayList
    Private arlEventReportAct As ArrayList = New ArrayList

    Private arlDeleteEventReportDtl As ArrayList = New ArrayList
    Private arlDeleteEventReportDoc As ArrayList = New ArrayList
    Private arlDeleteEventReportAct As ArrayList = New ArrayList

    Const sessEventReportHdr As String = "sessDataEventReportHdr"
    Const sessEventReportDoc As String = "sessDataEventReportDoc"
    Const sessEventReportAct As String = "sessDataEventReportAct"
    Const sessEventReportDtl As String = "sessDataEventReportDtl"

    Const sessDeleteEventReportDoc As String = "sessDeleteDataEventReportDoc"
    Const sessDeleteEventReportAct As String = "sessDeleteDataEventReportAct"
    Const sessDeleteEventReportDtl As String = "sessDeleteDataEventReportDtl"

    Private MAX_FILE_SIZE As Integer = 5120000
    Private TempDirectory As String
    Private TargetDirectory As String
    Private sesHelper As New SessionHelper
    Private intBabitParameterHeaderID As Integer = 0
    Private intActivityType As Integer = 0
    Private intItemIndex As Integer = 0
    Private Const strTypeCode As String = "L"
    Private Const strCategory As String = "EnumBabit.BabitParameterCategory"
    Private Const strValueCodeActivity As String = "Aktivitas"
    Dim Mode As String = "New"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property
#End Region

#Region "Custom Methods"

    Private Sub LoadDataBabitEventReport(intBabitEventReportHeaderID As Integer)
        Dim objEventReportDetail As BabitEventReportDetail

        arlEventReportDtl = New ArrayList
        arlEventReportDoc = New ArrayList
        arlEventReportAct = New ArrayList

        objEventReportHeader = New BabitEventReportHeaderFacade(User).Retrieve(intBabitEventReportHeaderID)
        If Not IsNothing(objEventReportHeader) Then
            sesHelper.SetSession(sessEventReportHdr, objEventReportHeader)

            objDealer = objEventReportHeader.Dealer
            sesHelper.SetSession("FrmInputBabitEventReport.DEALER", objDealer)

            Me.lblDealerCodeName.Text = objEventReportHeader.Dealer.DealerCode & " / " & objEventReportHeader.Dealer.DealerName
            Me.txtDealerCode.Text = objEventReportHeader.Dealer.DealerCode
            If Not IsNothing(objEventReportHeader.DealerBranch) Then
                Me.txtTOCode.Text = objEventReportHeader.DealerBranch.DealerBranchCode
                Me.hdntxtTOCode.Value = objEventReportHeader.DealerBranch.DealerBranchCode
                Me.lblTOCodeName.Text = objEventReportHeader.DealerBranch.DealerBranchCode & " / " & objEventReportHeader.DealerBranch.Name
                Me.lblTOName.Text = objEventReportHeader.DealerBranch.Name
                Me.hdnlblTOName.Value = objEventReportHeader.DealerBranch.Name
            End If
            Me.icPeriodStart.Value = objEventReportHeader.PeriodStart
            Me.icPeriodEnd.Value = objEventReportHeader.PeriodEnd
            Me.txtEventReportName.Text = objEventReportHeader.EventReportName
            If Not IsNothing(objEventReportHeader.BabitEventProposalHeader) Then
                Me.txtEventRegNumber.Text = objEventReportHeader.BabitEventProposalHeader.EventRegNumber
                Me.lblEventProposalName.Text = objEventReportHeader.BabitEventProposalHeader.EventProposalName
                Me.hdnEventProposalName.Value = objEventReportHeader.BabitEventProposalHeader.EventProposalName
                BindGridBabitEventSPK(txtEventRegNumber.Text)
            End If
            Me.txtLocationName.Text = objEventReportHeader.LocationName
            Me.txtAttendeeQty.Text = objEventReportHeader.AttendeeQty
            Me.txtInvitationQty.Text = objEventReportHeader.InvitationQty
            Me.txtNotes.Text = objEventReportHeader.Notes
            txtNotesMMKSI.Text = objEventReportHeader.NotesMMKSI

            'Pindah ke bawah
            'txtSubsidyAmount.Text = objEventReportHeader.ConfirmedBudget.ToString("#,##0")

            txtApprovedBudget.Text = objEventReportHeader.ApprovedBudget.ToString("#,##0")

            Dim strDealerCodes As String = String.Empty
            If Not IsNothing(objEventReportHeader.CollaborateDealer) AndAlso objEventReportHeader.CollaborateDealer.Trim() <> "" Then
                Dim arrCollaborateDealerID As String() = objEventReportHeader.CollaborateDealer.Split(";")
                For Each dealerID As String In arrCollaborateDealerID
                    Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(CInt(dealerID))
                    If Not IsNothing(objDealer) AndAlso objDealer.ID > 0 Then
                        If strDealerCodes = String.Empty Then
                            strDealerCodes = objDealer.DealerCode
                        Else
                            strDealerCodes += ";" & objDealer.DealerCode
                        End If
                    End If
                Next
            End If
            Me.txtCollaborateDealer.Text = strDealerCodes

            'Dim intProvinceID As Integer = 0
            'Dim intCityID As Integer = 0
            'If Not IsNothing(objEventReportHeader.City) Then
            '    intCityID = objEventReportHeader.City.ID
            '    If Not IsNothing(objEventReportHeader.City.Province) Then
            '        intProvinceID = objEventReportHeader.City.Province.ID
            '    End If
            'End If
            'Me.ddlProvinsi.SelectedValue = intProvinceID
            'BindddlCity()
            'Me.ddlKota.SelectedValue = intCityID

            Dim strSql As String = String.Empty
            If Not IsNothing(objEventReportHeader.City) AndAlso objEventReportHeader.City.ID > 0 Then
                Dim criterias4 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias4.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objEventReportHeader.City.ID))
                criterias4.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
                Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias4)
                If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
                    ddlProvinsi.Items.Clear()
                    ddlProvinsi.Items.Add(New ListItem("Silahkan Pilih", -1))

                    For Each prov As BabitSpecialCity In arlBabitSpecialCity
                        ddlProvinsi.Items.Add(New ListItem(prov.BabitSpecialProvince.Name, prov.BabitSpecialProvince.ID))
                    Next
                    ddlProvinsi.SelectedValue = arlBabitSpecialCity(0).BabitSpecialProvince.ID
                    BindddlCity(ddlProvinsi.SelectedValue, True)
                    Me.ddlKota.SelectedValue = objEventReportHeader.City.ID
                Else
                    Dim criterias5 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    strSql = "SELECT ProvinceID FROM City WHERE ID = " & objEventReportHeader.City.ID
                    criterias5.opAnd(New Criteria(GetType(Province), "ID", MatchType.InSet, "(" & strSql & ")"))
                    Dim arlProvince As ArrayList = New ProvinceFacade(User).Retrieve(criterias5)
                    If Not IsNothing(arlProvince) AndAlso arlProvince.Count > 0 Then
                        ddlProvinsi.Items.Clear()
                        ddlProvinsi.Items.Add(New ListItem("Silahkan Pilih", -1))

                        For Each prov As Province In arlProvince
                            ddlProvinsi.Items.Add(New ListItem(prov.ProvinceName, prov.ID))
                        Next
                        ddlProvinsi.SelectedValue = arlProvince(0).ID
                        BindddlCity(ddlProvinsi.SelectedValue, False)
                        Me.ddlKota.SelectedValue = objEventReportHeader.City.ID
                    End If
                End If
            End If

            'Untuk MMKSI dan Dealer
            'If Not IsLoginAsDealer() Then
            Dim dblBiaya As Double = 0
            Dim dblSubsidyAmount As Double = 0
            Dim dblMaxSubsidy As Double = 0

            Dim dsBabitEventReportHeader As DataSet = New BabitEventReportHeaderFacade(User).DoRetrieveDataset(objEventReportHeader.ID)
            Try
                Dim SubsidyTarget As Double = objEventReportHeader.BabitEventProposalHeader.EventDealerHeader.SubsidyTarget
                Dim SumPrice As Double = dsBabitEventReportHeader.Tables(0).Rows(0)("SumPrice")
                dblMaxSubsidy = objEventReportHeader.BabitEventProposalHeader.EventDealerHeader.MaxSubsidy

                Dim maxSubsidy As Decimal = 0
                If dblMaxSubsidy > 0 Then
                    maxSubsidy = dblMaxSubsidy / 100
                End If

                Dim val As Double = 0
                val = SumPrice * maxSubsidy

                If val < SubsidyTarget Then
                    dblSubsidyAmount = val
                ElseIf val > SubsidyTarget Then
                    dblSubsidyAmount = SubsidyTarget
                Else
                    dblSubsidyAmount = val
                End If
            Catch
            End Try
            'Me.txtSubsidyAmount.Text = IIf(dblSubsidyAmount = "", 0, dblSubsidyAmount.ToString("#,##0"))
            txtSubsidyAmount.Text = IIf(objEventReportHeader.ConfirmedBudget <= 0, dblSubsidyAmount.ToString("#,##0"), objEventReportHeader.ConfirmedBudget.ToString("#,##0"))
            'End If

            Me.hdnBabitEventProposalHeaderID.Value = objEventReportHeader.BabitEventProposalHeader.ID
            Me.hdnEventDealerHeaderID.Value = objEventReportHeader.BabitEventProposalHeader.EventDealerHeader.ID

            strSql = "select ID from BabitParameterDetail "
            strSql += "where BabitParameterHeaderID in ( "
            strSql += "select ID from BabitParameterHeader "
            strSql += "where BabitMasterEventTypeID in ( "
            strSql += "select ID from BabitMasterEventType where TypeCode = '" & strTypeCode & "') "
            strSql += "and ParameterCategory in ( "
            strSql += "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode = 'RincianKegiatan') "
            strSql += ") "

            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitEventReportHeader.ID", MatchType.Exact, intBabitEventReportHeaderID))
            criterias.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitParameterDetail.ID", MatchType.InSet, "(" & strSql & ")"))
            criterias.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitParameterDetail.BabitParameterHeader.Status", MatchType.Exact, 1))
            arlEventReportDtl = New BabitEventReportDetailFacade(User).Retrieve(criterias)
            sesHelper.SetSession(sessEventReportDtl, arlEventReportDtl)
            BindGridBabitEventReportDtl()

            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitEventReportDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitEventReportDocument), "BabitEventReportHeader.ID", MatchType.Exact, intBabitEventReportHeaderID))
            arlEventReportDoc = New BabitEventReportDocumentFacade(User).Retrieve(criterias2)
            sesHelper.SetSession(sessEventReportDoc, arlEventReportDoc)
            BindGridBabitEventReportDoc()

            strSql = "select ID from BabitParameterDetail "
            strSql += "where BabitParameterHeaderID in ( "
            strSql += "select ID from BabitParameterHeader "
            strSql += "where BabitMasterEventTypeID in ( "
            strSql += "select ID from BabitMasterEventType where TypeCode = '" & strTypeCode & "') "
            strSql += "and ParameterCategory in ( "
            strSql += "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode = '" & strValueCodeActivity & "') "
            strSql += ") "

            Dim criterias3 As New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitEventReportHeader.ID", MatchType.Exact, intBabitEventReportHeaderID))
            criterias3.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitParameterDetail.ID", MatchType.InSet, "(" & strSql & ")"))
            criterias3.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitParameterDetail.BabitParameterHeader.Status", MatchType.Exact, 1))
            arlEventReportAct = New BabitEventReportDetailFacade(User).Retrieve(criterias3)
            sesHelper.SetSession(sessEventReportAct, arlEventReportAct)
            BindGridBabitEventReportAct()

            Me.lblPopUpDealer.Visible = False
            Me.lblPopUpTO.Visible = False
            Me.lblPopUpEventProposal.Visible = False
            Me.txtDealerCode.Visible = False
            Me.txtTOCode.Enabled = False
            Me.txtEventRegNumber.Enabled = False
            Me.btnBack.Visible = True

            If Request.QueryString("Mode") <> "Edit" Then
                enabledProperty(False)
            Else
                enabledProperty(True)
            End If
        End If
    End Sub

    Private Sub enabledProperty(ByVal enb As Boolean)
        Me.icPeriodStart.Enabled = enb
        Me.icPeriodEnd.Enabled = enb
        Me.txtEventReportName.Enabled = enb
        Me.txtAttendeeQty.Enabled = enb
        Me.txtEventRegNumber.Enabled = enb
        Me.txtInvitationQty.Enabled = enb
        Me.txtLocationName.Enabled = enb
        Me.txtNotes.Enabled = enb
        Me.ddlKota.Enabled = enb
        Me.btnSave.Visible = enb
        Me.txtCollaborateDealer.Enabled = enb
        Me.lblSearchDealer.Visible = enb
        Me.txtSubsidyAmount.Enabled = enb
        Me.txtApprovedBudget.Enabled = False
        Me.txtNotesMMKSI.Enabled = enb
        Me.ddlProvinsi.Enabled = enb

        Me.dgUploadFile.ShowFooter = enb
        Me.dgBabitEventReport.ShowFooter = enb
        Me.dgBabitEventReportActivity.ShowFooter = enb
        Me.dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = enb
        Me.dgBabitEventReport.Columns(dgBabitEventReport.Columns.Count - 1).Visible = enb
        Me.dgBabitEventReportActivity.Columns(dgBabitEventReportActivity.Columns.Count - 1).Visible = enb
    End Sub

    Private Sub BindddlProvince()
        ddlKota.Items.Clear()
        ddlProvinsi.Items.Clear()
        ddlProvinsi.Items.Add(New ListItem("Silahkan Pilih", -1))

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objLoginUser.Dealer.City.ID))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
        Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias)
        If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
            For Each prov As BabitSpecialCity In arlBabitSpecialCity
                ddlProvinsi.Items.Add(New ListItem(prov.BabitSpecialProvince.Name, prov.BabitSpecialProvince.ID))
            Next
            If arlBabitSpecialCity.Count = 1 Then
                ddlProvinsi.SelectedValue = arlBabitSpecialCity(0).BabitSpecialProvince.ID
                BindddlCity(ddlProvinsi.SelectedValue, True)
            End If
        Else
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim strSQL As String = "SELECT ProvinceID FROM City WHERE ID = " & objLoginUser.Dealer.City.ID
            criterias2.opAnd(New Criteria(GetType(Province), "ID", MatchType.InSet, "(" & strSQL & ")"))
            Dim arlProvince As ArrayList = New ProvinceFacade(User).Retrieve(criterias2)
            If Not IsNothing(arlProvince) AndAlso arlProvince.Count > 0 Then
                For Each prov As Province In arlProvince
                    ddlProvinsi.Items.Add(New ListItem(prov.ProvinceName, prov.ID))
                Next
                If arlProvince.Count = 1 Then
                    ddlProvinsi.SelectedValue = arlProvince(0).ID()
                    BindddlCity(ddlProvinsi.SelectedValue, False)
                End If
            End If
        End If
    End Sub

    'Private Sub BindddlCity()
    '    ddlKota.Items.Clear()
    '    ddlKota.Items.Add(New ListItem("Silahkan Pilih", -1))
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "Province.ID", MatchType.Exact, ddlProvinsi.SelectedValue))

    '    Dim arlCity As ArrayList = New CityFacade(User).Retrieve(criterias)
    '    For Each c As City In arlCity
    '        ddlKota.Items.Add(New ListItem(c.CityName, c.ID))
    '    Next
    'End Sub

    Private Sub BindddlCity(ProvinceID As Integer, _isSpecial As Boolean)
        ddlKota.Items.Clear()
        ddlKota.Items.Add(New ListItem("Silahkan Pilih", -1))

        If _isSpecial Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "BabitSpecialProvince.ID", MatchType.Exact, ProvinceID))
            criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
            Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias)
            If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
                For Each c As BabitSpecialCity In arlBabitSpecialCity
                    ddlKota.Items.Add(New ListItem(c.City.CityName, c.City.ID))
                Next
            End If
        Else
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "Province.ID", MatchType.Exact, ProvinceID))
            Dim arlCity As ArrayList = New CityFacade(User).Retrieve(criterias2)
            If Not IsNothing(arlCity) AndAlso arlCity.Count > 0 Then
                For Each c As City In arlCity
                    ddlKota.Items.Add(New ListItem(c.CityName, c.ID))
                Next
            End If
        End If
    End Sub

    Private Sub ClearTempData()
        'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        'Dim success As Boolean = False

        'Try
        '    success = imp.Start()
        '    If success Then
        '        Dim dir As New DirectoryInfo(TempDirectory)
        '        dir.Delete(True)
        '    End If
        'Catch ex As Exception
        '    'Throw ex
        'End Try
    End Sub

    Protected Sub ddlProvinsi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvinsi.SelectedIndexChanged
        Dim _isSpecial As Boolean = False
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objLoginUser.Dealer.City.ID))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
        Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias)
        If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
            _isSpecial = True
        End If

        BindddlCity(ddlProvinsi.SelectedValue, _isSpecial)

        BindGridBabitEventReportDtl()
        BindGridBabitEventReportAct()
    End Sub

    Private Sub ClearAll()
        hdnBabitEventReportHeaderID.Value = ""
        hdnBabitEventProposalHeaderID.Value = ""
        txtTOCode.Text = ""
        hdntxtTOCode.Value = ""
        lblTOCodeName.Text = ""
        lblTOName.Text = ""
        hdnlblTOName.Value = ""
        lblEventProposalName.Text = ""
        hdnEventProposalName.Value = ""
        txtEventReportName.Text = ""
        txtEventRegNumber.Text = ""
        Me.txtAttendeeQty.Text = "0"
        Me.txtInvitationQty.Text = "0"
        Me.txtLocationName.Text = ""
        Me.txtNotes.Text = ""
        Me.txtNotesMMKSI.Text = ""
        Me.ddlKota.SelectedValue = "-1"

        icPeriodStart.Value = Date.Now
        icPeriodEnd.Value = Date.Now
        sesHelper.SetSession(sessEventReportDtl, New ArrayList)
        sesHelper.SetSession(sessEventReportDoc, New ArrayList)
        sesHelper.SetSession(sessEventReportAct, New ArrayList)

        BindGridBabitEventReportDtl()
        BindGridBabitEventReportDoc()
        BindGridBabitEventReportAct()
        BindGridBabitEventSPK(Me.txtEventRegNumber.Text)

    End Sub

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Sub CommitAttachment(ByVal AttachmentCollection As ArrayList)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim TargetFInfo As FileInfo
        Dim TempFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As BabitEventReportDocument In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        TargetFInfo = New FileInfo(TargetDirectory + obj.Path)
                        TempFInfo = New FileInfo(TempDirectory + obj.Path)

                        If TempFInfo.Exists Then
                            If Not TargetFInfo.Directory.Exists Then
                                Directory.CreateDirectory(TargetFInfo.DirectoryName)
                            End If
                            TempFInfo.MoveTo(TargetFInfo.FullName)
                        End If
                        obj.AttachmentData.SaveAs(TargetDirectory + obj.Path)
                    End If
                Next

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Sub GetDealerData(ByVal oDealer As Dealer)
        txtDealerCode.Visible = False
        lblPopUpDealer.Visible = False
        lblDealerCodeName.Visible = True
        lblDealerCodeName.Text = oDealer.DealerCode & " / " & oDealer.DealerName
        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
    End Sub

    Sub BindGridBabitEventSPK(strEventRegNumber As String)
        Dim dSEventSPKList As DataSet = New BabitEventReportHeaderFacade(User).RetrieveFromSP(strEventRegNumber)
        If dSEventSPKList.Tables.Count > 0 Then
            dgBabitEventSPK.DataSource = dSEventSPKList.Tables(0)
        Else
            dgBabitEventSPK.DataSource = New ArrayList
        End If
        dgBabitEventSPK.DataBind()
    End Sub

    Sub BindGridBabitEventReportAct()
        arlEventReportAct = CType(sesHelper.GetSession(sessEventReportAct), ArrayList)
        If IsNothing(arlEventReportAct) Then arlEventReportAct = New ArrayList()

        CommonFunction.SortListControl(arlEventReportAct, "BabitParameterDetail.ID", Sort.SortDirection.ASC)

        dgBabitEventReportActivity.DataSource = arlEventReportAct
        dgBabitEventReportActivity.DataBind()
        sesHelper.SetSession(sessEventReportAct, arlEventReportAct)
    End Sub

    Sub BindGridBabitEventReportDoc()
        arlEventReportDoc = CType(sesHelper.GetSession(sessEventReportDoc), ArrayList)
        If IsNothing(arlEventReportDoc) Then arlEventReportDoc = New ArrayList()
        dgUploadFile.DataSource = arlEventReportDoc
        dgUploadFile.DataBind()
    End Sub

    Sub BindGridBabitEventReportDtl()
        arlEventReportDtl = CType(sesHelper.GetSession(sessEventReportDtl), ArrayList)
        If IsNothing(arlEventReportDtl) Then
            arlEventReportDtl = GetArrayGridEventReport(hdnBabitEventReportHeaderID.Value)
        End If

        Dim dataList As ArrayList = New ArrayList
        dataList = New System.Collections.ArrayList(
                        (From obj As BabitEventReportDetail In arlEventReportDtl.OfType(Of BabitEventReportDetail)()
                            Where obj.Description <> "#"
                            Select obj).ToList())

        CommonFunction.SortListControl(dataList, "BabitParameterDetail.BabitParameterHeader.ID", Sort.SortDirection.ASC)

        Dim objBabitParamDtl As New BabitParameterDetail
        Dim oBabitEventReportDetail As New BabitEventReportDetail
        Dim intBabitParameterHeaderID As Integer = 0
        Dim arlEvent2 As ArrayList = New ArrayList

        For i As Integer = 0 To dataList.Count - 1
            Dim objBabitEvent As BabitEventReportDetail = CType(dataList(i), BabitEventReportDetail)
            If i = 0 Then
                intBabitParameterHeaderID = objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID
            End If
            If intBabitParameterHeaderID <> objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID Then
                oBabitEventReportDetail = New BabitEventReportDetail
                oBabitEventReportDetail.BabitEventReportHeader = New BabitEventReportHeader

                objBabitParamDtl = New BabitParameterDetail
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, intBabitParameterHeaderID))
                Dim array As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criterias)
                If Not IsNothing(array) AndAlso array.Count > 0 Then
                    objBabitParamDtl = CType(array(0), BabitParameterDetail)
                End If
                oBabitEventReportDetail.BabitParameterDetail = objBabitParamDtl
                oBabitEventReportDetail.Description = "#"
                arlEvent2.Add(oBabitEventReportDetail)
                intBabitParameterHeaderID = objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID
            End If

            arlEvent2.Add(objBabitEvent)
        Next
        CommonFunction.SortListControl(arlEvent2, "BabitParameterDetail.BabitParameterHeader.ID", Sort.SortDirection.ASC)

        dgBabitEventReport.DataSource = arlEvent2
        dgBabitEventReport.DataBind()
        sesHelper.SetSession(sessEventReportDtl, arlEvent2)
    End Sub

    Private Function GetArrayGridEventReport(ByVal _intBabitEventReportHeaderID As Integer) As ArrayList
        Dim arr As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitEventReportHeader.ID", MatchType.Exact, _intBabitEventReportHeaderID))
        arr = New BabitEventReportDetailFacade(User).Retrieve(criterias)
        If IsNothing(arr) Then arr = New ArrayList
        Return arr
    End Function

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If Not IsLoginAsDealer() Then
            If (txtDealerCode.Text = String.Empty) Then
                sb.Append("Kode Dealer Harus Diisi\n")
            End If
        End If

        If txtEventRegNumber.Text.Trim = "" Then
            sb.Append("Nomor Reg Proposal harus Diisi\n")
        End If
        If txtEventReportName.Text.Trim = "" Then
            sb.Append("Nama Laporan Event harus Diisi\n")
        End If
        If txtLocationName.Text.Trim = "" Then
            sb.Append("Nama Lokasi harus Diisi\n")
        End If
        If (ddlProvinsi.SelectedValue.Trim = String.Empty) Then
            sb.Append("Provinsi Harus Diisi\n")
        End If
        If (ddlKota.SelectedValue.Trim = String.Empty) Then
            sb.Append("Kota Harus Diisi\n")
        End If
        If txtInvitationQty.Text.Trim = "" OrElse txtInvitationQty.Text.Trim = "0" Then
            sb.Append("Jumlah Undangan Event harus Diisi\n")
        End If
        If txtAttendeeQty.Text.Trim = "" OrElse txtAttendeeQty.Text.Trim = "0" Then
            sb.Append("Jumlah Kehadiran harus Diisi\n")
        End If

        If (icPeriodStart.Value > icPeriodEnd.Value) Then
            sb.Append("Tanggal awal harus lebih kecil atau sama dengan tanggal akhir\n")
        End If

        If (sesHelper.GetSession(sessEventReportAct) Is Nothing) Then
            sb.Append("Data Aktivitas Laporan belum ada\n")
        Else
            If CType(sesHelper.GetSession(sessEventReportAct), ArrayList).Count = 0 Then
                sb.Append("Data Aktivitas Laporan belum ada\n")
            End If
        End If
        If (sesHelper.GetSession(sessEventReportDoc) Is Nothing) Then
            sb.Append("Data Lampiran Dokumen belum ada\n")
        Else
            If CType(sesHelper.GetSession(sessEventReportDoc), ArrayList).Count = 0 Then
                sb.Append("Data Lampiran Dokumen belum ada\n")
            End If
        End If
        If (sesHelper.GetSession(sessEventReportDtl) Is Nothing) Then
            sb.Append("Data Rincian Kegiatan belum ada\n")
        Else
            If CType(sesHelper.GetSession(sessEventReportDtl), ArrayList).Count = 0 Then
                sb.Append("Data Rincian Kegiatan belum ada\n")
            End If
        End If

        If txtInvitationQty.Text.Trim = "" Then
            txtInvitationQty.Text = "0"
        End If
        If txtAttendeeQty.Text.Trim = "" Then
            txtAttendeeQty.Text = "0"
        End If

        '--- di skip karna tgl laporan bisa di luar tgl proposal pada real nya
        'If Me.hdnBabitEventProposalHeaderID.Value.Trim <> "" Then
        '    Dim objBabitEventProposalHeader As BabitEventProposalHeader = New BabitEventProposalHeaderFacade(User).Retrieve(CInt(Me.hdnBabitEventProposalHeaderID.Value))
        '    If Not IsNothing(objBabitEventProposalHeader) Then
        '        If ValidatePeriodProposalEvent(objBabitEventProposalHeader.PeriodStart, objBabitEventProposalHeader.PeriodEnd, icPeriodStart.Value, icPeriodEnd.Value) Then
        '            sb.Append("Tanggal Laporan Event diluar dari periode Proposal Event \n")
        '        End If
        '    End If
        'End If

        If Mode = "Edit" Then
            If Me.txtSubsidyAmount.Text.Trim = "" Then Me.txtSubsidyAmount.Text = 0

            If Not IsLoginAsDealer() Then
                If TR_Jml_Subsidi.Visible = True Then
                    If txtSubsidyAmount.Text.Trim = 0 OrElse txtSubsidyAmount.Text.Trim = "" Then
                        sb.Append("- Jumlah Subsidi harus Diisi.\n")
                    End If
                End If
            End If
        End If

        Return sb.ToString()
    End Function

    Private Function ValidateIsMandatoryParamBabitEvent(ByRef strParamName As String) As String
        strParamName = String.Empty
        Dim dataList As ArrayList = New ArrayList
        arlEventReportDtl = CType(sesHelper.GetSession(sessEventReportDtl), ArrayList)

        Dim isMandatory As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "IsMandatory", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
        Dim arrBabitParamHdr As ArrayList = New BabitParameterHeaderFacade(User).Retrieve(criterias)
        If Not IsNothing(arrBabitParamHdr) AndAlso arrBabitParamHdr.Count > 0 Then
            For Each objParam As BabitParameterHeader In arrBabitParamHdr
                dataList = New ArrayList(
                                (From obj As BabitEventReportDetail In arlEventReportDtl.OfType(Of BabitEventReportDetail)()
                                    Where obj.BabitParameterDetail.BabitParameterHeader.ID = objParam.ID
                                    Select obj).ToList())
                If dataList.Count = 0 Then
                    If strParamName = String.Empty Then
                        strParamName = objParam.ParameterName
                    Else
                        strParamName += ", " & objParam.ParameterName
                    End If
                End If
            Next
        End If

        Return strParamName
    End Function

    Private Function ValidatePeriodProposalEvent(ByVal eventDateStart As DateTime, ByVal eventDateEnd As DateTime, ByVal propDateStart As DateTime, ByVal propDateEnd As DateTime) As Boolean
        If propDateStart < eventDateStart _
            OrElse propDateStart > eventDateEnd _
            OrElse propDateEnd < eventDateStart _
            OrElse propDateEnd > eventDateEnd Then

            Return True
        End If
        Return False
    End Function

    Private Sub UploadAttachment(ByVal ObjAttachment As BabitEventReportDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.Path) Then
                    finfo = New FileInfo(TargetPath + ObjAttachment.Path)

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.Path)
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each obj As BabitEventReportDocument In AttachmentCollection
                If Not IsNothing(obj.AttachmentData) Then
                    If Path.GetFileName(obj.AttachmentData.FileName) = FileName Then
                        bResult = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return bResult
    End Function

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Mode = "New" Then
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Input_Event_Laporan_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - INPUT LAPORAN EVENT ")
            End If
        ElseIf Mode = "Edit" Then
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Laporan_Edit_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - EDIT LAPORAN EVENT ")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Laporan_Display_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - DISPLAY LAPORAN EVENT ")
            End If
        End If
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
            btnBack.Visible = True
        End If
        If Mode = "New" Then  ' Login as Dealer
            objDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = CType(sesHelper.GetSession("FrmInputBabitEventReport.DEALER"), Dealer)
        End If
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If (Not IsPostBack) Then
            InitiateAuthorization()

            BindGridBabitEventReportAct()
            BindGridBabitEventReportDtl()
            BindGridBabitEventReportDoc()
            BindGridBabitEventSPK(Me.txtEventRegNumber.Text)
            BindddlProvince()

            ClearAll()

            If Not IsNothing(Request.QueryString("BabitEventReportHeaderID")) Then
                hdnBabitEventReportHeaderID.Value = Request.QueryString("BabitEventReportHeaderID")
                LoadDataBabitEventReport(hdnBabitEventReportHeaderID.Value)
            End If
            GetDealerData(objDealer)
            If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
                If Mode = "Edit" Then
                    If objEventReportHeader.Status >= 4 Then  '--> status Konfirmasi
                        txtSubsidyAmount.Enabled = True
                        txtNotesMMKSI.Enabled = True
                    Else
                        txtSubsidyAmount.Enabled = False
                        txtNotesMMKSI.Enabled = False
                    End If
                End If
            Else
                txtSubsidyAmount.Enabled = False
                txtNotesMMKSI.Enabled = False
                If Mode = "New" Then
                    TR_Jml_Subsidi.Visible = False
                    TR_CatatanMKS.Visible = False
                Else
                    TR_Jml_Subsidi.Visible = True
                    TR_CatatanMKS.Visible = True
                End If
            End If

            Dim strDealerGroupID As String = ""
            If IsLoginAsDealer() Then
                strDealerGroupID = CStr(objLoginUser.Dealer.DealerGroup.ID)
            Else
                If Not IsNothing(objEventReportHeader) AndAlso objEventReportHeader.ID > 0 Then
                    strDealerGroupID = objEventReportHeader.Dealer.DealerGroup.ID
                End If
            End If
            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblPopUpTO.Attributes("onclick") = "ShowPPDealerBranchSelection();"
            lblPopUpEventProposal.Attributes("onclick") = "ShowPPEventProposalSelection();"
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelectionGab('" & strDealerGroupID & "','" & CType(GetFromSession("DEALER"), Dealer).DealerCode & "');"

            If Mode = "New" Then
                btnCetak.Visible = False
            End If
        End If
    End Sub

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Private Sub dgBabitEventReportActivity_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitEventReportActivity.ItemCommand
        Dim lblActivityType As Label
        Dim ddlActivityType As DropDownList
        Dim lblDescription As Label
        Dim txtFDescription As TextBox
        Dim txtEDescription As TextBox

        objEventReportHeader = CType(sesHelper.GetSession(sessEventReportHdr), BabitEventReportHeader)
        arlEventReportAct = CType(sesHelper.GetSession(sessEventReportAct), ArrayList)
        If IsNothing(arlEventReportAct) Then arlEventReportAct = New ArrayList

        Select Case e.CommandName
            Case "add"
                ddlActivityType = CType(e.Item.FindControl("ddlFActivityType"), DropDownList)
                txtFDescription = CType(e.Item.FindControl("txtFDescription"), TextBox)
                If ddlActivityType.SelectedValue = "-1" Then
                    MessageBox.Show("Tipe Aktivitas harus diisi.")
                    Return
                End If
                If txtFDescription.Text.Trim = "" Then
                    MessageBox.Show("Deskripsi harus diisi.")
                    Return
                End If
                Dim objBabitEventReportDetail As BabitEventReportDetail = New BabitEventReportDetail
                objBabitEventReportDetail.BabitEventReportHeader = New BabitEventReportHeader
                objBabitEventReportDetail.BabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlActivityType.SelectedValue))
                objBabitEventReportDetail.Description = txtFDescription.Text.Trim
                arlEventReportAct.Add(objBabitEventReportDetail)

            Case "save" 'Update this datagrid item   
                ddlActivityType = CType(e.Item.FindControl("ddlEActivityType"), DropDownList)
                txtEDescription = CType(e.Item.FindControl("txtEDescription"), TextBox)
                If ddlActivityType.SelectedValue = "-1" Then
                    MessageBox.Show("Tipe Aktivitas harus diisi.")
                    Return
                End If
                If txtEDescription.Text.Trim = "" Then
                    MessageBox.Show("Deskripsi harus diisi.")
                    Return
                End If
                Dim objBabitEventReportDetail As BabitEventReportDetail = CType(arlEventReportAct(e.Item.ItemIndex), BabitEventReportDetail)
                objBabitEventReportDetail.BabitEventReportHeader = objEventReportHeader
                objBabitEventReportDetail.BabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlActivityType.SelectedValue))
                objBabitEventReportDetail.Description = txtEDescription.Text.Trim()

                dgBabitEventReportActivity.EditItemIndex = -1
                dgBabitEventReportActivity.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgBabitEventReportActivity.ShowFooter = False
                dgBabitEventReportActivity.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Try
                    Dim oBabitEventReportDetail As BabitEventReportDetail = CType(arlEventReportAct(e.Item.ItemIndex), BabitEventReportDetail)
                    If oBabitEventReportDetail.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sesHelper.GetSession(sessDeleteEventReportAct), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oBabitEventReportDetail)
                        sesHelper.SetSession(sessDeleteEventReportAct, arrDelete)
                    End If
                    arlEventReportAct.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try
            Case "cancel" 'Cancel Update this datagrid item 
                dgBabitEventReportActivity.EditItemIndex = -1
                dgBabitEventReportActivity.ShowFooter = True
        End Select

        sesHelper.SetSession(sessEventReportAct, arlEventReportAct)
        BindGridBabitEventReportAct()
        BindGridBabitEventReportDtl()
    End Sub

    Private Function GetDataAcivitas() As ArrayList
        Dim sSQL As String = String.Empty
        sSQL += "select ID,BabitParameterHeaderID,ParameterDetailName,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from BabitParameterDetail "
        sSQL += "where BabitParameterHeaderID in ("
        sSQL += "select ID from BabitParameterHeader "
        sSQL += "where Status = 1 and ParameterCategory in ("
        sSQL += "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode= '" & strValueCodeActivity & "') "
        sSQL += "and BabitMasterEventTypeID in ("
        sSQL += "select ID from BabitMasterEventType where TypeCode = '" & strTypeCode & "'))"

        Dim result As ArrayList = New BabitParameterDetailFacade(User).RetrieveSP(sSQL)
        If IsNothing(result) Then result = New ArrayList

        Return result
    End Function

    Private Sub dgBabitEventReportActivity_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitEventReportActivity.ItemDataBound

        Dim lblActivityType As Label
        Dim ddlActivityType As DropDownList
        Dim lblDescription As Label
        Dim txtFDescription As TextBox
        Dim txtEDescription As TextBox

        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        If e.Item.ItemType = ListItemType.Footer Then
            ddlActivityType = CType(e.Item.FindControl("ddlFActivityType"), DropDownList)
            Dim arrDDL As ArrayList = GetDataAcivitas()
            With ddlActivityType
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterDetailName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            End With
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objBabitEventReportDetail As BabitEventReportDetail = CType(e.Item.DataItem, BabitEventReportDetail)
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgBabitEventReportActivity.CurrentPageIndex * dgBabitEventReportActivity.PageSize)

            lblActivityType = CType(e.Item.FindControl("lblActivityType"), Label)
            lblDescription = CType(e.Item.FindControl("lblDescription"), Label)
            lblActivityType.Text = objBabitEventReportDetail.BabitParameterDetail.ParameterDetailName
            lblDescription.Text = objBabitEventReportDetail.Description
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim objBabitEventReportDetail As BabitEventReportDetail = CType(e.Item.DataItem, BabitEventReportDetail)
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgBabitEventReportActivity.CurrentPageIndex * dgBabitEventReportActivity.PageSize)

            ddlActivityType = CType(e.Item.FindControl("ddlEActivityType"), DropDownList)
            Dim arrDDL As ArrayList = GetDataAcivitas()
            With ddlActivityType
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterDetailName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            End With
            ddlActivityType.SelectedValue = objBabitEventReportDetail.BabitParameterDetail.ID
        End If
    End Sub

    Private Sub dgBabitEventReport_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitEventReport.ItemCommand
        Dim oBabitParameterDetail As New BabitParameterDetail
        Dim ddlECategoryBabitEvent As DropDownList
        Dim ddlFCategoryBabitEvent As DropDownList
        Dim ddlEJenisBabitEvent As DropDownList
        Dim ddlFJenisBabitEvent As DropDownList
        Dim lblCategoryBabitEvent As Label
        Dim lblJenisBabitEvent As Label
        Dim txtEDesc As TextBox
        Dim txtFDesc As TextBox
        Dim txtEQty As TextBox
        Dim txtFQty As TextBox

        objEventReportHeader = CType(sesHelper.GetSession(sessEventReportHdr), BabitEventReportHeader)
        arlEventReportDtl = CType(sesHelper.GetSession(sessEventReportDtl), ArrayList)

        Select Case e.CommandName
            Case "add"
                ddlFCategoryBabitEvent = CType(e.Item.FindControl("ddlFCategoryBabitEvent"), DropDownList)
                ddlFJenisBabitEvent = CType(e.Item.FindControl("ddlFJenisBabitEvent"), DropDownList)
                txtFDesc = CType(e.Item.FindControl("txtFDesc"), TextBox)
                txtFQty = CType(e.Item.FindControl("txtFQty"), TextBox)
                txtFDesc = CType(e.Item.FindControl("txtFDesc"), TextBox)

                If ddlFCategoryBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Kategori Kegiatan harus diisi.")
                    Return
                End If
                If ddlFJenisBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Jenis Kegiatan harus diisi.")
                    Return
                End If
                If txtFDesc.Text.Trim = "" Then
                    MessageBox.Show("Deskripsi Kegiatan harus diisi.")
                    Return
                End If
                If txtFQty.Text.Trim = "" OrElse txtFQty.Text.Trim = "0" Then
                    MessageBox.Show("Qty harus diisi.")
                    Return
                End If
                oBabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlFJenisBabitEvent.SelectedValue))

                Dim oBabitEventReportDetail As New BabitEventReportDetail
                oBabitEventReportDetail.BabitEventReportHeader = New BabitEventReportHeader
                oBabitEventReportDetail.BabitParameterDetail = oBabitParameterDetail
                oBabitEventReportDetail.Description = txtFDesc.Text.Trim
                oBabitEventReportDetail.Qty = txtFQty.Text.Trim
                arlEventReportDtl.Add(oBabitEventReportDetail)

            Case "save" 'Update this datagrid item   
                ddlECategoryBabitEvent = CType(e.Item.FindControl("ddlECategoryBabitEvent"), DropDownList)
                ddlEJenisBabitEvent = CType(e.Item.FindControl("ddlEJenisBabitEvent"), DropDownList)
                txtEDesc = CType(e.Item.FindControl("txtEDesc"), TextBox)
                txtEQty = CType(e.Item.FindControl("txtEQty"), TextBox)

                If ddlECategoryBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Kategori Kegiatan harus diisi.")
                    Return
                End If
                If ddlEJenisBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Jenis Kegiatan harus diisi.")
                    Return
                End If
                If txtEDesc.Text.Trim = "" Then
                    MessageBox.Show("Deskripsi Kegiatan harus diisi.")
                    Return
                End If
                If txtEQty.Text.Trim = "" OrElse txtEQty.Text.Trim = "0" Then
                    MessageBox.Show("Qty harus diisi.")
                    Return
                End If

                oBabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlEJenisBabitEvent.SelectedValue))
                Dim oBabitEventReportDetail As BabitEventReportDetail = CType(arlEventReportDtl(e.Item.ItemIndex), BabitEventReportDetail)
                oBabitEventReportDetail.BabitEventReportHeader = objEventReportHeader
                oBabitEventReportDetail.BabitParameterDetail = oBabitParameterDetail
                oBabitEventReportDetail.Description = txtEDesc.Text.Trim()
                oBabitEventReportDetail.Qty = txtEQty.Text.Trim
                dgBabitEventReport.EditItemIndex = -1
                dgBabitEventReport.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgBabitEventReport.ShowFooter = False
                dgBabitEventReport.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Try
                    Dim oDeleteBabitEventReportDetail As BabitEventReportDetail = CType(arlEventReportDtl(e.Item.ItemIndex), BabitEventReportDetail)
                    If oDeleteBabitEventReportDetail.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sesHelper.GetSession(sessDeleteEventReportDtl), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oDeleteBabitEventReportDetail)
                        sesHelper.SetSession(sessDeleteEventReportDtl, arrDelete)
                    End If
                    arlEventReportDtl.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try

            Case "cancel" 'Cancel Update this datagrid item 
                dgBabitEventReport.EditItemIndex = -1
                dgBabitEventReport.ShowFooter = True
        End Select

        sesHelper.SetSession(sessEventReportDtl, arlEventReportDtl)
        BindGridBabitEventReportDtl()
        BindGridBabitEventReportAct()
    End Sub

    Public Sub ddlFCategoryBabitEvent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlFCategoryBabitEvent As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFCategoryBabitEvent.Parent.Parent()
        Dim ddlFJenisBabitEvent As DropDownList
        If gridItem.DataSetIndex > -1 Then
            ddlFJenisBabitEvent = gridItem.FindControl("ddlEJenisBabitEvent")
        Else
            ddlFJenisBabitEvent = gridItem.FindControl("ddlFJenisBabitEvent")
        End If
        ddlFJenisBabitEvent.Items.Clear()
        If ddlFCategoryBabitEvent.SelectedIndex > 0 Then
            arrDDL = New ArrayList
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, ddlFCategoryBabitEvent.SelectedValue))
            arrDDL = New BabitParameterDetailFacade(User).Retrieve(criterias2)

            ddlFJenisBabitEvent.DataSource = arrDDL
            ddlFJenisBabitEvent.DataTextField = "ParameterDetailName"
            ddlFJenisBabitEvent.DataValueField = "ID"
            ddlFJenisBabitEvent.DataBind()
            ddlFJenisBabitEvent.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlFJenisBabitEvent.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End If
    End Sub

    Private Sub dgBabitEventReport_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitEventReport.ItemDataBound
        Dim ddlECategoryBabitEvent As DropDownList
        Dim ddlFCategoryBabitEvent As DropDownList
        Dim ddlEJenisBabitEvent As DropDownList
        Dim ddlFJenisBabitEvent As DropDownList
        Dim lblCategoryBabitEvent As Label
        Dim lblJenisBabitEvent As Label
        Dim lblDescription As Label
        Dim lblQty As Label
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        If e.Item.ItemType = ListItemType.Footer Then
            ddlFCategoryBabitEvent = CType(e.Item.FindControl("ddlFCategoryBabitEvent"), DropDownList)
            ddlFJenisBabitEvent = CType(e.Item.FindControl("ddlFJenisBabitEvent"), DropDownList)

            Dim strSql As String = String.Empty
            strSql = "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode = 'RincianKegiatan'"
            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.InSet, "(" & strSql & ")"))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
            arrDDL = New BabitParameterHeaderFacade(User).Retrieve(criterias)

            With ddlFCategoryBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            End With

            ddlFJenisBabitEvent.Items.Clear()
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oBERD As BabitEventReportDetail = CType(e.Item.DataItem, BabitEventReportDetail)

            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitParameterHeaderID = oBERD.BabitParameterDetail.BabitParameterHeader.ID
            Else
                If intBabitParameterHeaderID <> oBERD.BabitParameterDetail.BabitParameterHeader.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitParameterHeaderID = oBERD.BabitParameterDetail.BabitParameterHeader.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitEventReport.CurrentPageIndex * dgBabitEventReport.PageSize)

            lblCategoryBabitEvent = CType(e.Item.FindControl("lblCategoryBabitEvent"), Label)
            lblJenisBabitEvent = CType(e.Item.FindControl("lblJenisBabitEvent"), Label)
            lblDescription = CType(e.Item.FindControl("lblDescription"), Label)
            lblQty = CType(e.Item.FindControl("lblQty"), Label)
            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnDelete = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            lblCategoryBabitEvent.Text = oBERD.BabitParameterDetail.BabitParameterHeader.ParameterName()
            lblJenisBabitEvent.Text = oBERD.BabitParameterDetail.ParameterDetailName()
            lblQty.Text = oBERD.Qty

            If lblDescription.Text.Trim.ToLower = "#" Then
                e.Item.Cells(0).Text = "No."
                e.Item.Cells(1).Text = "Kategori"
                e.Item.Cells(2).Text = "Jenis"
                e.Item.Cells(3).Text = "Deskripsi"
                e.Item.Cells(4).Text = "Qty"
                e.Item.Cells(0).Style.Add("text-align", "center")
                e.Item.Cells(1).Style.Add("text-align", "center")
                e.Item.Cells(2).Style.Add("text-align", "center")
                e.Item.Cells(3).Style.Add("text-align", "center")
                e.Item.Cells(4).Style.Add("text-align", "center")
                e.Item.Cells(0).Style.Add("font-weight", "bold")
                e.Item.Cells(1).Style.Add("font-weight", "bold")
                e.Item.Cells(2).Style.Add("font-weight", "bold")
                e.Item.Cells(3).Style.Add("font-weight", "bold")
                e.Item.Cells(4).Style.Add("font-weight", "bold")
                e.Item.Cells(0).ForeColor = Color.White
                e.Item.Cells(1).ForeColor = Color.White
                e.Item.Cells(2).ForeColor = Color.White
                e.Item.Cells(3).ForeColor = Color.White
                e.Item.Cells(4).ForeColor = Color.White
                e.Item.Cells(0).BackColor = Color.FromName("C9A557")
                e.Item.Cells(1).BackColor = Color.FromName("C9A557")
                e.Item.Cells(2).BackColor = Color.FromName("C9A557")
                e.Item.Cells(3).BackColor = Color.FromName("C9A557")
                e.Item.Cells(4).BackColor = Color.FromName("C9A557")
                e.Item.Cells(5).BackColor = Color.FromName("C9A557")

                lblDescription.Attributes("style") = "display:none"
                lblJenisBabitEvent.Text = ""
                lblQty.Text = ""
                lbtnEdit.Attributes("style") = "display:none"
                lbtnDelete.Attributes("style") = "display:none"
                lblCategoryBabitEvent.Attributes("style") = "display:none"
                e.Item.Cells(0).Text = ""
            Else
                lblDescription.Attributes("style") = "display:table-row"
                lbtnEdit.Attributes("style") = "display:table-row"
                lbtnDelete.Attributes("style") = "display:table-row"
                lblCategoryBabitEvent.Attributes("style") = "display:table-row"
            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oBERD As BabitEventReportDetail = CType(e.Item.DataItem, BabitEventReportDetail)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitParameterHeaderID = oBERD.BabitParameterDetail.BabitParameterHeader.ID
            Else
                If intBabitParameterHeaderID <> oBERD.BabitParameterDetail.BabitParameterHeader.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitParameterHeaderID = oBERD.BabitParameterDetail.BabitParameterHeader.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitEventReport.CurrentPageIndex * dgBabitEventReport.PageSize)

            ddlECategoryBabitEvent = CType(e.Item.FindControl("ddlECategoryBabitEvent"), DropDownList)

            Dim strSql As String = String.Empty
            strSql = "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode = 'RincianKegiatan'"
            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.InSet, "(" & strSql & ")"))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
            arrDDL = New BabitParameterHeaderFacade(User).Retrieve(criterias)
            With ddlECategoryBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBERD.BabitParameterDetail.BabitParameterHeader.ID
            End With

            ddlEJenisBabitEvent = CType(e.Item.FindControl("ddlEJenisBabitEvent"), DropDownList)
            arrDDL = New ArrayList
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, oBERD.BabitParameterDetail.BabitParameterHeader.ID))
            arrDDL = New BabitParameterDetailFacade(User).Retrieve(criterias2)
            With ddlEJenisBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterDetailName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBERD.BabitParameterDetail.ID
            End With
        End If
    End Sub

    Private Sub dgUploadFile_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUploadFile.ItemCommand
        Dim _arrDataUploadFile As ArrayList = CType(sesHelper.GetSession(sessEventReportDoc), ArrayList)
        If IsNothing(_arrDataUploadFile) Then _arrDataUploadFile = New ArrayList

        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("UploadFile"), HtmlInputFile)
                Dim txtKeterangan As TextBox = CType(e.Item.FindControl("txtKeterangan"), TextBox)
                Dim ddlFCategoryEvent As DropDownList = CType(e.Item.FindControl("ddlFCategoryEvent"), DropDownList)
                Dim objPostedData As HttpPostedFile
                Dim objBabitEventReportDocument As BabitEventReportDocument = New BabitEventReportDocument()
                Dim sFileName As String

                '========= Validasi  =======================================================================
                If ddlFCategoryEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Nama Kategori harus diisi.")
                    Return
                End If
                If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                    MessageBox.Show("Lampiran masih kosong")
                    Return
                End If
                Dim _filename As String = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName)
                If _filename.Trim().Length <= 0 Then
                    MessageBox.Show("Upload file belum diisi\n")
                    Return
                End If
                If _filename.Trim().Length > 0 Then
                    If FileUpload.PostedFile.ContentLength > MAX_FILE_SIZE Then
                        MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                        Return
                    End If
                End If
                Dim ext As String = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName)
                If Not (ext.ToUpper() = ".JPG" OrElse ext.ToUpper() = ".JPEG" OrElse ext.ToUpper() = ".PDF") Then
                    MessageBox.Show("Hanya menerima file format (PDF/JPG/JPEG)")
                    Return
                End If
                If Not IsNothing(FileUpload) OrElse FileUpload.Value <> String.Empty Then
                    objPostedData = FileUpload.PostedFile
                Else
                    objPostedData = Nothing
                End If

                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                        BindGridBabitEventReportDoc()
                        Return
                    End If

                    Dim strDealerCode As String = String.Empty
                    If IsLoginAsDealer() Then
                        strDealerCode = lblDealerCodeName.Text.Split("/")(0).Trim
                    Else
                        strDealerCode = txtDealerCode.Text
                    End If

                    If Not FileIsExist(sFileName, _arrDataUploadFile) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        Dim strBabitPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("BabitFileDirectory")
                        Dim strBabitPathFile As String = "\EVENT\" & objDealer.DealerCode & "\EventReport\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        Dim strDestFile As String = strBabitPathConfig & strBabitPathFile '--Destination File                       

                        objBabitEventReportDocument.BabitEventReportHeader = New BabitEventReportHeader()
                        objBabitEventReportDocument.EventDealerRequiredDocument = New EventDealerRequiredDocumentFacade(User).Retrieve(CInt(ddlFCategoryEvent.SelectedValue))
                        objBabitEventReportDocument.AttachmentData = objPostedData
                        objBabitEventReportDocument.FileName = sFileName
                        objBabitEventReportDocument.Path = strDestFile
                        objBabitEventReportDocument.FileDescription = IIf(txtKeterangan.Text.Trim = "", "Laporan Event Document", txtKeterangan.Text)

                        UploadAttachment(objBabitEventReportDocument, TempDirectory)

                        _arrDataUploadFile.Add(objBabitEventReportDocument)
                        sesHelper.SetSession(sessEventReportDoc, _arrDataUploadFile)
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If

            Case "Delete" 'Delete this datagrid item 
                Dim oBabitEventReportDocument As BabitEventReportDocument = CType(_arrDataUploadFile(e.Item.ItemIndex), BabitEventReportDocument)
                If oBabitEventReportDocument.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sesHelper.GetSession(sessDeleteEventReportDoc), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oBabitEventReportDocument)
                    sesHelper.SetSession(sessDeleteEventReportDoc, arrDelete)
                End If
                RemoveBabitEventReportAttachment(CType(_arrDataUploadFile(e.Item.ItemIndex), BabitEventReportDocument), TempDirectory)
                _arrDataUploadFile.RemoveAt(e.Item.ItemIndex)

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)
        End Select

        BindGridBabitEventReportDoc()
    End Sub

    Private Sub RemoveBabitEventReportAttachment(ByVal ObjAttachment As BabitEventReportDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(TargetPath + ObjAttachment.Path)
                If finfo.Exists Then
                    finfo.Delete()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub dgUploadFile_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUploadFile.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgUploadFile.CurrentPageIndex * dgUploadFile.PageSize)

            Dim arrUpload As ArrayList = CType(sesHelper.GetSession(sessEventReportDoc), ArrayList)
            If Not IsNothing(arrUpload) AndAlso arrUpload.Count > 0 Then
                Dim objBabitEventReportDocument As BabitEventReportDocument = arrUpload(e.Item.ItemIndex)
                Dim lblFileName As Label = CType(e.Item.FindControl("lblFileName"), Label)
                lblFileName.Text = Path.GetFileName(objBabitEventReportDocument.FileName)
            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            Dim ddlFCategoryEvent As DropDownList = CType(e.Item.FindControl("ddlFCategoryEvent"), DropDownList)
            Dim arrDDL As ArrayList = New ArrayList
            If hdnEventDealerHeaderID.Value.Trim <> "" Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDealerRequiredDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(EventDealerRequiredDocument), "DocumentType", MatchType.Exact, 2))
                criterias.opAnd(New Criteria(GetType(EventDealerRequiredDocument), "EventDealerHeader.ID", MatchType.Exact, hdnEventDealerHeaderID.Value))
                arrDDL = New EventDealerRequiredDocumentFacade(User).Retrieve(criterias)
            End If
            With ddlFCategoryEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "DocumentName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            End With
        End If

    End Sub

    Private Function ValidateDealers(ByVal _dealers As String) As Boolean
        Dim bcheck As Boolean = True
        Dim i As Integer
        Dim items() As String = _dealers.Split(";")
        For i = 0 To items.Length - 1
            Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(items(i))
            If objDealerTmp.ID = 0 Then
                MessageBox.Show("Dealer " + items(i) + " tidak valid")
                bcheck = False
                Exit For
            End If

        Next
        If ValidateDealerDuplication(_dealers) <> String.Empty Then
            MessageBox.Show("Duplikasi Dealer " + ValidateDealerDuplication(_dealers))
            bcheck = False
        End If
        Return bcheck
    End Function

    Private Function ValidateDealerDuplication(ByVal _dealers As String) As String
        Dim bcheck As Boolean = True
        Dim _dealerDuplicate As String = String.Empty
        Dim i As Integer
        Dim j As Integer
        Dim list() As String = _dealers.Split(";")
        For i = 0 To list.Length - 2
            For j = i + 1 To list.Length - 1
                If list(i) = list(j) Then
                    bcheck = False
                    Exit For
                End If
            Next
            If bcheck = False Then
                _dealerDuplicate = list(i)
                Exit For
            End If
        Next
        Return _dealerDuplicate
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If
        If Me.txtCollaborateDealer.Text.Trim <> "" Then
            If Not ValidateDealers(Me.txtCollaborateDealer.Text.Trim) Then Exit Sub
        End If

        arlEventReportDtl = CType(sesHelper.GetSession(sessEventReportDtl), ArrayList)
        arlEventReportDtl = New System.Collections.ArrayList(
                (From obj As BabitEventReportDetail In arlEventReportDtl.OfType(Of BabitEventReportDetail)()
                    Where obj.Description <> "#"
                    Select obj).ToList())

        arlEventReportAct = CType(sesHelper.GetSession(sessEventReportAct), ArrayList)
        arlEventReportDoc = CType(sesHelper.GetSession(sessEventReportDoc), ArrayList)

        arlDeleteEventReportDtl = CType(sesHelper.GetSession(sessDeleteEventReportDtl), ArrayList)
        arlDeleteEventReportDoc = CType(sesHelper.GetSession(sessDeleteEventReportDoc), ArrayList)
        arlDeleteEventReportAct = CType(sesHelper.GetSession(sessDeleteEventReportAct), ArrayList)

        arlDeleteEventReportDtl = IIf(Not IsNothing(arlDeleteEventReportDtl), arlDeleteEventReportDtl, New ArrayList)
        arlDeleteEventReportDoc = IIf(Not IsNothing(arlDeleteEventReportDoc), arlDeleteEventReportDoc, New ArrayList)
        arlDeleteEventReportAct = IIf(Not IsNothing(arlDeleteEventReportAct), arlDeleteEventReportAct, New ArrayList)

        Dim _oDealer As Dealer = New Dealer
        If IsLoginAsDealer() Then
            _oDealer = SesDealer
        Else
            _oDealer = New DealerFacade(User).Retrieve(Me.txtDealerCode.Text)
        End If
        If Mode = "New" Then
            objEventReportHeader = New BabitEventReportHeader
        Else
            objEventReportHeader = CType(sesHelper.GetSession(sessEventReportHdr), BabitEventReportHeader)
        End If
        objEventReportHeader.Dealer = _oDealer
        objEventReportHeader.DealerBranch = New DealerBranchFacade(User).Retrieve(txtTOCode.Text)
        objEventReportHeader.BabitEventProposalHeader = New BabitEventProposalHeaderFacade(User).Retrieve(CType(hdnBabitEventProposalHeaderID.Value, Integer))
        objEventReportHeader.EventReportName = txtEventReportName.Text
        objEventReportHeader.PeriodStart = icPeriodStart.Value
        objEventReportHeader.PeriodEnd = icPeriodEnd.Value
        If Mode = "New" Then
            objEventReportHeader.Status = 0  '--- Status Baru
        End If
        objEventReportHeader.LocationName = txtLocationName.Text
        Dim intCityID As Integer = 0
        If ddlKota.SelectedValue.Trim <> "" Then
            intCityID = CType(ddlKota.SelectedValue, Integer)
        End If
        objEventReportHeader.City = New CityFacade(User).Retrieve(intCityID)
        objEventReportHeader.InvitationQty = txtInvitationQty.Text
        objEventReportHeader.AttendeeQty = txtAttendeeQty.Text
        objEventReportHeader.Notes = txtNotes.Text
        If objEventReportHeader.Status >= 4 Then  '--> status Konfirmasi
            objEventReportHeader.NotesMMKSI = txtNotesMMKSI.Text
            objEventReportHeader.ConfirmedBudget = txtSubsidyAmount.Text
        End If

        Dim strDealerID As String = String.Empty
        If txtCollaborateDealer.Text.Trim() <> "" Then
            Dim CollaborateDealerCode As String() = txtCollaborateDealer.Text.Split(";")
            For Each dealerCode As String In CollaborateDealerCode
                Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(dealerCode)
                If Not IsNothing(objDealer) AndAlso objDealer.ID > 0 Then
                    If strDealerID = String.Empty Then
                        strDealerID = CStr(objDealer.ID)
                    Else
                        strDealerID += ";" & CStr(objDealer.ID)
                    End If
                End If
            Next
        End If
        objEventReportHeader.CollaborateDealer = strDealerID

        Dim _result As Integer = 0
        If Mode <> "Edit" Then
            _result = New BabitEventReportDetailFacade(User).InsertTransaction(objEventReportHeader, arlEventReportDtl, arlEventReportDoc, arlEventReportAct)
        Else
            _result = New BabitEventReportDetailFacade(User).UpdateTransaction(objEventReportHeader, arlEventReportDtl, arlDeleteEventReportDtl, arlEventReportDoc, arlDeleteEventReportDoc, arlEventReportAct, arlDeleteEventReportAct)
        End If

        Dim strJs As String = String.Empty
        If _result > 0 Then
            CommitAttachment(arlEventReportDoc)
            If Mode = "Edit" Then
                RemoveBabitProposalAttachment(arlDeleteEventReportDoc, TargetDirectory)
            End If
            strJs = "alert('Simpan Data Berhasil');"
            strJs += "window.location = '../Babit/FrmBabitEventReportList.aspx';"
            ClearTempData()
            ClearAll()
        Else
            strJs = "alert('Simpan Data Gagal');"
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub RemoveBabitProposalAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As BabitEventReportDocument In AttachmentCollection
                    finfo = New FileInfo(TargetPath + obj.Path)
                    If finfo.Exists Then
                        finfo.Delete()
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnGetInfoDealer_Click(sender As Object, e As EventArgs) Handles btnGetInfoDealer.Click
        Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
        lblDealerCodeName.Text = objDealer.DealerCode & " / " & objDealer.DealerName
    End Sub

    Private Sub btnGetInfoDealerBranch_Click(sender As Object, e As EventArgs) Handles btnGetInfoDealerBranch.Click
        Dim objDealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(hdntxtTOCode.Value.Trim())
        lblTOName.Text = objDealerBranch.Name
        txtTOCode.Text = objDealerBranch.DealerBranchCode
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        Me.ClearAll()
        Me.btnSave.Visible = True
    End Sub

    Private Sub btnGetInfoEventProposal_Click(sender As Object, e As EventArgs) Handles btnGetInfoEventProposal.Click
        If Me.hdnBabitEventProposalHeaderID.Value.Trim = "" Then Exit Sub

        arlEventReportDoc = CType(sesHelper.GetSession(sessEventReportDoc), ArrayList)
        If IsNothing(arlEventReportDoc) Then arlEventReportDoc = New ArrayList
        Dim objBabitEventProposalHeader As BabitEventProposalHeader = New BabitEventProposalHeaderFacade(User).Retrieve(CInt(Me.hdnBabitEventProposalHeaderID.Value))
        For Each obj As BabitEventReportDocument In arlEventReportDoc
            If obj.EventDealerRequiredDocument.EventDealerHeader.ID <> objBabitEventProposalHeader.EventDealerHeader.ID Then
                sesHelper.SetSession(sessEventReportDoc, New ArrayList)
                Exit For
            End If
        Next

        BindGridBabitEventReportDoc()
        Me.txtEventRegNumber.Text = objBabitEventProposalHeader.EventRegNumber
        Me.lblEventProposalName.Text = objBabitEventProposalHeader.EventProposalName
        Me.hdnEventProposalName.Value = objBabitEventProposalHeader.EventProposalName

        BindGridBabitEventSPK(Me.txtEventRegNumber.Text)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/Babit/FrmBabitEventReportList.aspx")
    End Sub

#End Region

End Class
