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

Public Class FrmInputBabitEventProposal
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"

    Dim objDealer As New Dealer
    Dim objLoginUser As UserInfo
    Dim objEventProposalHeader As BabitEventProposalHeader

    Private arlEventProposalDtl As ArrayList = New ArrayList
    Private arlEventProposalDoc As ArrayList = New ArrayList
    Private arlEventProposalAct As ArrayList = New ArrayList

    Private arlDeleteEventProposalDtl As ArrayList = New ArrayList
    Private arlDeleteEventProposalDoc As ArrayList = New ArrayList
    Private arlDeleteEventProposalAct As ArrayList = New ArrayList

    Const sessEventProposalHdr As String = "sessDataEventProposalHdr"
    Const sessEventProposalDoc As String = "sessDataEventProposalDoc"
    Const sessEventProposalAct As String = "sessDataEventProposalAct"
    Const sessEventProposalDtl As String = "sessDataEventProposalDtl"

    Const sessDeleteEventProposalDoc As String = "sessDeleteDataEventProposalDoc"
    Const sessDeleteEventProposalAct As String = "sessDeleteDataEventProposalAct"
    Const sessDeleteEventProposalDtl As String = "sessDeleteDataEventProposalDtl"

    Private MAX_FILE_SIZE As Integer = 5120000
    Private TempDirectory As String
    Private TargetDirectory As String
    Private sesHelper As New SessionHelper
    Private intBabitParameterHeaderID As Integer = 0
    Private intActivityType As Integer = 0
    Private intItemIndex As Integer = 0
    Private Const strTypeCode As String = "V"
    Private Const strEnumBabitCategory As String = "EnumBabit.BabitParameterCategory"
    Private Const strValueCodeBiaya As String = "Biaya"
    Private Const strValueCodeAct As String = "Aktivitas"
    Dim IsDealer As Boolean = False
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

    Private Sub LoadDataBabitEventProposal(intBabitEventProposalHeaderID As Integer)
        Dim objBabitEventProposalDetail As BabitEventProposalDetail

        arlEventProposalDtl = New ArrayList
        arlEventProposalDoc = New ArrayList
        arlEventProposalAct = New ArrayList

        objEventProposalHeader = New BabitEventProposalHeaderFacade(User).Retrieve(intBabitEventProposalHeaderID)
        If Not IsNothing(objEventProposalHeader) Then
            sesHelper.SetSession(sessEventProposalHdr, objEventProposalHeader)

            objDealer = objEventProposalHeader.Dealer
            sesHelper.SetSession("FrmInputBabitEventProposal.DEALER", objDealer)
            BindddlProvince()

            Me.lblEventRegNumber.Text = objEventProposalHeader.EventRegNumber
            Me.lblDealerCodeName.Text = objEventProposalHeader.Dealer.DealerCode & " / " & objEventProposalHeader.Dealer.DealerName
            Me.txtDealerCode.Text = objEventProposalHeader.Dealer.DealerCode
            If Not IsNothing(objEventProposalHeader.DealerBranch) Then
                Me.txtTOCode.Text = objEventProposalHeader.DealerBranch.DealerBranchCode
                Me.hdntxtTOCode.Value = objEventProposalHeader.DealerBranch.DealerBranchCode
                Me.lblTOCodeName.Text = objEventProposalHeader.DealerBranch.DealerBranchCode & " / " & objEventProposalHeader.DealerBranch.Name
                Me.lblTOName.Text = objEventProposalHeader.DealerBranch.Name
                Me.hdnlblTOName.Value = objEventProposalHeader.DealerBranch.Name
            End If
            Me.icPeriodStart.Value = objEventProposalHeader.PeriodStart
            Me.icPeriodEnd.Value = objEventProposalHeader.PeriodEnd
            Me.txtProposalEventName.Text = objEventProposalHeader.EventProposalName
            Me.hdnEventID.Value = objEventProposalHeader.EventDealerHeader.ID
            Me.txtEventCode.Text = objEventProposalHeader.EventDealerHeader.EventName
            Me.lblPeriodStartEvent.Text = objEventProposalHeader.EventDealerHeader.PeriodStart.ToString("dd/MM/yyyy")
            Me.lblPeriodEndEvent.Text = objEventProposalHeader.EventDealerHeader.PeriodEnd.ToString("dd/MM/yyyy")
            Me.txtNotes.Text = objEventProposalHeader.Notes
            Me.txtLocationName.Text = objEventProposalHeader.LocationName

            Dim criterias5 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias5.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objEventProposalHeader.City.ID))
            criterias5.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
            Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias5)
            If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
                ddlProvinsi.SelectedValue = CType(arlBabitSpecialCity(0), BabitSpecialCity).BabitSpecialProvince.ID
                BindddlCity(ddlProvinsi.SelectedValue, True)
            Else
                Dim criterias4 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias4.opAnd(New Criteria(GetType(City), "ID", MatchType.Exact, objEventProposalHeader.City.ID))
                Dim arlCity As ArrayList = New CityFacade(User).Retrieve(criterias4)
                If Not IsNothing(arlCity) AndAlso arlCity.Count > 0 Then
                    ddlProvinsi.SelectedValue = CType(arlCity(0), City).Province.ID
                    BindddlCity(ddlProvinsi.SelectedValue, False)
                End If
            End If
            Me.ddlKota.SelectedValue = objEventProposalHeader.City.ID

            Dim strDealerCodes As String = String.Empty
            If Not IsNothing(objEventProposalHeader.CollaborateDealer) AndAlso objEventProposalHeader.CollaborateDealer.Trim() <> "" Then
                Dim arrCollaborateDealerID As String() = objEventProposalHeader.CollaborateDealer.Split(";")
                For Each dealerID As String In arrCollaborateDealerID
                    Dim oDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(CInt(dealerID))
                    If Not IsNothing(oDealer) AndAlso oDealer.ID > 0 Then
                        If strDealerCodes = String.Empty Then
                            strDealerCodes = oDealer.DealerCode
                        Else
                            strDealerCodes += ";" & oDealer.DealerCode
                        End If
                    End If
                Next
            End If

            Me.txtCollaborateDealer.Text = strDealerCodes

            '--- Binding Grid Proposal Biaya
            arlEventProposalDtl = New BabitEventProposalDetailFacade(User).RetrieveDataBabitEventProposalDetail(intBabitEventProposalHeaderID, strTypeCode, strEnumBabitCategory, strValueCodeBiaya)
            sesHelper.SetSession(sessEventProposalDtl, arlEventProposalDtl)
            BindGridBabitProposalEventProposalDtl()

            '--- Binding Grid Proposal Aktivitas
            arlEventProposalAct = New BabitEventProposalDetailFacade(User).RetrieveDataBabitEventProposalDetail(intBabitEventProposalHeaderID, strTypeCode, strEnumBabitCategory, strValueCodeAct)
            sesHelper.SetSession(sessEventProposalAct, arlEventProposalAct)
            BindGridBabitProposalEventProposalAct()

            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitEventProposalDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitEventProposalDocument), "BabitEventProposalHeader.ID", MatchType.Exact, intBabitEventProposalHeaderID))
            arlEventProposalDoc = New BabitEventProposalDocumentFacade(User).Retrieve(criterias2)
            sesHelper.SetSession(sessEventProposalDoc, arlEventProposalDoc)
            BindGridBabitProposalEventProposalDoc()

            Me.lblPopUpDealer.Visible = False
            Me.lblPopUpTO.Visible = False
            Me.lblPopUpEvent.Visible = False
            Me.txtEventCode.Enabled = False
            Me.txtTOCode.Enabled = False
            Me.txtDealerCode.Enabled = False
            Me.txtCollaborateDealer.Enabled = False
            Me.lblSearchDealer.Visible = False
            Me.btnBack.Visible = True

            Me.icPeriodStart.Enabled = False
            Me.icPeriodEnd.Enabled = False
            Me.txtProposalEventName.Enabled = False
            Me.txtLocationName.Enabled = False
            Me.ddlProvinsi.Enabled = False
            Me.ddlKota.Enabled = False

            Me.dgUploadFile.ShowFooter = False
            Me.dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = False
            Me.dgBabitEventProposal.ShowFooter = False
            Me.dgBabitEventProposal.Columns(dgBabitEventProposal.Columns.Count - 1).Visible = False
            Me.dgBabitEventProposalActivity.ShowFooter = False
            Me.dgBabitEventProposalActivity.Columns(dgBabitEventProposalActivity.Columns.Count - 1).Visible = False
            Me.btnSave.Visible = False

            If Request.QueryString("Mode") = "Edit" Then
                Me.icPeriodStart.Enabled = True
                Me.icPeriodEnd.Enabled = True
                Me.txtProposalEventName.Enabled = True
                Me.txtLocationName.Enabled = True
                Me.ddlProvinsi.Enabled = True
                Me.ddlKota.Enabled = True
                Me.txtDealerCode.Visible = False

                Me.dgUploadFile.ShowFooter = True
                Me.dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = True
                Me.dgBabitEventProposal.ShowFooter = True
                Me.dgBabitEventProposal.Columns(dgBabitEventProposal.Columns.Count - 1).Visible = True
                Me.dgBabitEventProposalActivity.ShowFooter = True
                Me.dgBabitEventProposalActivity.Columns(dgBabitEventProposalActivity.Columns.Count - 1).Visible = True
                Me.btnSave.Visible = True
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

    Private Function GetTotalPriceByCategory(ByVal intBabitParameterHeaderID As Integer) As Double
        Dim dblSumTotalPrice As Double = 0
        dblSumTotalPrice = (From item As BabitEventProposalDetail In arlEventProposalDtl
                            Where item.BabitParameterDetail.BabitParameterHeader.ID = intBabitParameterHeaderID And item.Item <> "Total Biaya :"
                                Select (item.Qty * item.Price)).Sum()
        Return dblSumTotalPrice
    End Function

    Private Sub ClearAll()
        hdnEventID.Value = ""
        hdnEventName.Value = ""
        hdnEventDealerRequiredDocumentID.Value = ""
        hdnBabitEventProposalHeaderID.Value = ""
        lblEventRegNumber.Text = "Auto Generated"
        txtTOCode.Text = ""
        hdntxtTOCode.Value = ""
        lblTOCodeName.Text = ""
        lblTOName.Text = ""
        lblPeriodStartEvent.Text = ""
        lblPeriodEndEvent.Text = ""
        hdnlblTOName.Value = ""
        txtProposalEventName.Text = ""
        txtEventCode.Text = ""
        txtLocationName.Text = ""
        txtCollaborateDealer.Text = ""
        ddlProvinsi.SelectedIndex = 0
        ddlKota.SelectedIndex = 0
        icPeriodStart.Value = Date.Now
        icPeriodEnd.Value = Date.Now
        sesHelper.SetSession(sessEventProposalDtl, New ArrayList)
        sesHelper.SetSession(sessEventProposalDoc, New ArrayList)
        sesHelper.SetSession(sessEventProposalAct, New ArrayList)

        BindGridBabitProposalEventProposalDtl()
        BindGridBabitProposalEventProposalDoc()
        BindGridBabitProposalEventProposalAct()
    End Sub

    Private Sub BindddlProvince()
        ddlKota.Items.Clear()
        ddlKota.Items.Add(New ListItem("Silahkan Pilih", -1))
        ddlProvinsi.Items.Clear()
        ddlProvinsi.Items.Add(New ListItem("Silahkan Pilih", -1))

        'If CType(sesHelper.GetSession("DEALER"), Dealer).Title <> EnumDealerTittle.DealerTittle.KTB Then
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objDealer.City.ID))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
        Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias)
        If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
            For Each prov As BabitSpecialCity In arlBabitSpecialCity
                ddlProvinsi.Items.Add(New ListItem(prov.BabitSpecialProvince.Name, prov.BabitSpecialProvince.ID))
            Next
            If arlBabitSpecialCity.Count = 1 Then
                ddlProvinsi.SelectedValue = arlBabitSpecialCity(0).BabitSpecialProvince.ID
                BindddlCity(Me.ddlProvinsi.SelectedValue, True)
            End If
        Else
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim strSQL As String = "SELECT ProvinceID FROM City WHERE ID = " & objDealer.City.ID
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
        'Else
        '    Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Dim arlProvince As ArrayList = New ProvinceFacade(User).Retrieve(criterias2)
        '    If Not IsNothing(arlProvince) AndAlso arlProvince.Count > 0 Then
        '        For Each prov As Province In arlProvince
        '            ddlProvinsi.Items.Add(New ListItem(prov.ProvinceName, prov.ID))
        '        Next
        '        If arlProvince.Count = 1 Then
        '            ddlProvinsi.SelectedValue = arlProvince(0).ID()
        '            BindddlCity(ddlProvinsi.SelectedValue, False)
        '        End If
        '    End If
        'End If
    End Sub

    Protected Sub ddlProvinsi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvinsi.SelectedIndexChanged
        'BindddlCity()

        Dim _isSpecial As Boolean = False
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objDealer.City.ID))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
        Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias)
        If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
            _isSpecial = True
        End If

        BindddlCity(ddlProvinsi.SelectedValue, _isSpecial)
    End Sub

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

    Private Function GetRegNumber() As String
        'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim YearParameter As Integer = Date.Today.Year
        If Date.Today.Month = 12 Then
            YearParameter = Date.Today.Year + 1
        End If
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventProposalHeader), "EventRegNumber", MatchType.StartsWith, "V"))
        crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "CreatedTime", MatchType.GreaterOrEqual, Date.Today.Year & Date.Today.Month.ToString("d2") & "01"))
        crit.opAnd(New Criteria(GetType(BabitEventProposalHeader), "CreatedTime", MatchType.Lesser, YearParameter & Date.Today.AddMonths(1).Month.ToString("d2") & "01"))
        Dim arrl As ArrayList = New BabitEventProposalHeaderFacade(User).Retrieve(crit)
        Dim _return As String
        If arrl.Count > 0 Then
            'Dim objBH As BabitEventProposalHeader = CommonFunction.SortListControl(arrl, "ID", Sort.SortDirection.DESC)(0)
            Dim objBH As BabitEventProposalHeader = CommonFunction.SortListControl(arrl, "EventRegNumber", Sort.SortDirection.DESC)(0)
            Dim noReg As String = objBH.EventRegNumber
            _return = "V" & Date.Today.Month.ToString("d2") & Date.Today.ToString("yy") & (CInt(noReg.Substring(5, 5)) + 1).ToString("d5")
        Else
            _return = "V" & Date.Today.Month.ToString("d2") & Date.Today.ToString("yy") & CInt(1).ToString("d5")
        End If
        Return _return
    End Function

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
                For Each obj As BabitEventProposalDocument In AttachmentCollection
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

    Sub BindGridBabitProposalEventProposalAct()
        arlEventProposalAct = CType(sesHelper.GetSession(sessEventProposalAct), ArrayList)
        If IsNothing(arlEventProposalAct) Then arlEventProposalAct = New ArrayList()

        CommonFunction.SortListControl(arlEventProposalAct, "BabitParameterDetail.ID", Sort.SortDirection.ASC)

        dgBabitEventProposalActivity.DataSource = arlEventProposalAct
        dgBabitEventProposalActivity.DataBind()
        sesHelper.SetSession(sessEventProposalAct, arlEventProposalAct)
    End Sub

    Sub BindGridBabitProposalEventProposalDoc()
        arlEventProposalDoc = CType(sesHelper.GetSession(sessEventProposalDoc), ArrayList)
        If IsNothing(arlEventProposalDoc) Then arlEventProposalDoc = New ArrayList()
        dgUploadFile.DataSource = arlEventProposalDoc
        dgUploadFile.DataBind()
    End Sub

    Sub BindGridBabitProposalEventProposalDtl()
        arlEventProposalDtl = CType(sesHelper.GetSession(sessEventProposalDtl), ArrayList)
        If IsNothing(arlEventProposalDtl) Then
            arlEventProposalDtl = GetArrayGridEvent(hdnBabitEventProposalHeaderID.Value)
        End If

        Dim dataList As ArrayList = New ArrayList
        dataList = New System.Collections.ArrayList(
                        (From obj As BabitEventProposalDetail In arlEventProposalDtl.OfType(Of BabitEventProposalDetail)()
                            Where obj.Item <> "Total Biaya :"
                            Select obj).ToList())

        CommonFunction.SortListControl(dataList, "BabitParameterDetail.BabitParameterHeader.ID", Sort.SortDirection.ASC)

        Dim objBabitParamDtl As New BabitParameterDetail
        Dim oBabitEventProposalDetail As New BabitEventProposalDetail
        Dim intBabitParameterHeaderID As Integer = 0
        Dim arlEvent2 As ArrayList = New ArrayList

        For i As Integer = 0 To dataList.Count - 1
            Dim objBabitEvent As BabitEventProposalDetail = CType(dataList(i), BabitEventProposalDetail)
            If i = 0 Then
                intBabitParameterHeaderID = objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID
            End If
            If intBabitParameterHeaderID <> objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID Then
                oBabitEventProposalDetail = New BabitEventProposalDetail
                oBabitEventProposalDetail.BabitEventProposalHeader = New BabitEventProposalHeader

                objBabitParamDtl = New BabitParameterDetail
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, intBabitParameterHeaderID))
                Dim array As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criterias)
                If Not IsNothing(array) AndAlso array.Count > 0 Then
                    objBabitParamDtl = CType(array(0), BabitParameterDetail)
                End If
                oBabitEventProposalDetail.BabitParameterDetail = objBabitParamDtl
                oBabitEventProposalDetail.Item = "Total Biaya :"
                oBabitEventProposalDetail.TotalPrice = GetTotalPriceByCategory(intBabitParameterHeaderID)
                arlEvent2.Add(oBabitEventProposalDetail)
                intBabitParameterHeaderID = objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID
            End If

            arlEvent2.Add(objBabitEvent)

            If i = dataList.Count - 1 Then
                oBabitEventProposalDetail = New BabitEventProposalDetail
                oBabitEventProposalDetail.BabitEventProposalHeader = New BabitEventProposalHeader

                objBabitParamDtl = New BabitParameterDetail
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, intBabitParameterHeaderID))
                Dim array As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criterias)
                If Not IsNothing(array) AndAlso array.Count > 0 Then
                    objBabitParamDtl = CType(array(0), BabitParameterDetail)
                End If
                oBabitEventProposalDetail.BabitParameterDetail = objBabitParamDtl
                oBabitEventProposalDetail.Item = "Total Biaya :"
                oBabitEventProposalDetail.TotalPrice = GetTotalPriceByCategory(intBabitParameterHeaderID)
                arlEvent2.Add(oBabitEventProposalDetail)
            End If
        Next
        CommonFunction.SortListControl(arlEvent2, "BabitParameterDetail.BabitParameterHeader.ID", Sort.SortDirection.ASC)


        dgBabitEventProposal.DataSource = arlEvent2
        dgBabitEventProposal.DataBind()
        sesHelper.SetSession(sessEventProposalDtl, arlEvent2)
    End Sub

    Private Function GetArrayGridEvent(ByVal _BabitEventProposalHeaderID As Integer) As ArrayList
        Dim arr As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitEventProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitEventProposalDetail), "BabitEventProposalHeader.ID", MatchType.Exact, _BabitEventProposalHeaderID))
        arr = New BabitEventProposalDetailFacade(User).Retrieve(criterias)
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
        'If (txtTOCode.Text.Trim = String.Empty) Then
        '    sb.Append("Kode Temporary Outlet Harus Diisi\n")
        'End If

        If txtEventCode.Text.Trim = "" Then
            sb.Append("Nama Event harus Diisi\n")
        End If
        If txtProposalEventName.Text.Trim = "" Then
            sb.Append("Nama Proposal Event harus Diisi\n")
        End If
        If (icPeriodStart.Value > icPeriodEnd.Value) Then
            sb.Append("Tanggal awal harus lebih kecil atau sama dengan tanggal akhir\n")
        End If
        If txtLocationName.Text.Trim = "" Then
            sb.Append("Nama Lokasi harus Diisi\n")
        End If
        If ddlKota.SelectedIndex = 0 Then
            sb.Append("Kota/Kab harus Diisi\n")
        End If

        If (sesHelper.GetSession(sessEventProposalAct) Is Nothing) Then
            sb.Append("Data Rencana Aktivitas belum ada\n")
        Else
            If CType(sesHelper.GetSession(sessEventProposalAct), ArrayList).Count = 0 Then
                sb.Append("Data Rencana Aktivitas belum ada\n")
            End If
        End If
        If (sesHelper.GetSession(sessEventProposalDoc) Is Nothing) Then
            sb.Append("Data Upload Dokumen Pendukung belum ada\n")
        Else
            If CType(sesHelper.GetSession(sessEventProposalDtl), ArrayList).Count = 0 Then
                sb.Append("Data Upload Dokumen Pendukung belum ada\n")
            End If
        End If
        If (sesHelper.GetSession(sessEventProposalDtl) Is Nothing) Then
            sb.Append("Data Biaya belum ada\n")
        Else
            If CType(sesHelper.GetSession(sessEventProposalDtl), ArrayList).Count = 0 Then
                sb.Append("Data Biaya belum ada\n")
            End If
        End If

        If Me.hdnEventID.Value.Trim <> "" Then
            Dim objEventDealerHeader As EventDealerHeader = New EventDealerHeaderFacade(User).Retrieve(CInt(Me.hdnEventID.Value))
            If Not IsNothing(objEventDealerHeader) Then
                If ValidatePeriodProposalEvent(objEventDealerHeader.PeriodStart, objEventDealerHeader.PeriodEnd, icPeriodStart.Value, icPeriodEnd.Value) Then
                    sb.Append("Tanggal Proposal Event diluar dari periode Daftar Event dari MMKSI \n")
                End If
            End If
        End If

        Return sb.ToString()
    End Function

    Private Function ValidateIsMandatoryParamBabitEvent(ByRef strParamName As String) As String
        strParamName = String.Empty
        Dim dataList As ArrayList = New ArrayList
        arlEventProposalDtl = CType(sesHelper.GetSession(sessEventProposalDtl), ArrayList)

        Dim isMandatory As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "IsMandatory", MatchType.Exact, 1))
        Dim arrBabitParamHdr As ArrayList = New BabitParameterHeaderFacade(User).Retrieve(criterias)
        If Not IsNothing(arrBabitParamHdr) AndAlso arrBabitParamHdr.Count > 0 Then
            For Each objParam As BabitParameterHeader In arrBabitParamHdr
                dataList = New ArrayList(
                                (From obj As BabitEventProposalDetail In arlEventProposalDtl.OfType(Of BabitEventProposalDetail)()
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

    Private Sub UploadAttachment(ByVal ObjAttachment As BabitEventProposalDocument, ByVal TargetPath As String)
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
            For Each obj As BabitEventProposalDocument In AttachmentCollection
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
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Input_Event_Proposal_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - INPUT PROPOSAL EVENT")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Proposal_Display_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - INPUT PROPOSAL EVENT")
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
            objDealer = CType(sesHelper.GetSession("FrmInputBabitEventProposal.DEALER"), Dealer)
        End If
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
    End Sub

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
            IsDealer = False
        Else
            IsDealer = True
        End If

        If (Not IsPostBack) Then
            InitiateAuthorization()

            BindGridBabitProposalEventProposalAct()
            BindGridBabitProposalEventProposalDoc()
            BindGridBabitProposalEventProposalDtl()

            If Not IsNothing(Request.QueryString("BabitEventProposalHeaderID")) Then
                hdnBabitEventProposalHeaderID.Value = Request.QueryString("BabitEventProposalHeaderID")
                LoadDataBabitEventProposal(hdnBabitEventProposalHeaderID.Value)
            Else
                BindddlProvince()
            End If
            GetDealerData(objDealer)

            Dim strDealerGroupID As String = ""
            If IsDealer Then
                strDealerGroupID = CStr(objLoginUser.Dealer.DealerGroup.ID)
            Else
                If Not IsNothing(objEventProposalHeader) AndAlso objEventProposalHeader.ID > 0 Then
                    strDealerGroupID = objEventProposalHeader.Dealer.DealerGroup.ID
                End If
            End If
            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblPopUpTO.Attributes("onclick") = "ShowPPDealerBranchSelection();"
            lblPopUpEvent.Attributes("onclick") = "ShowPPEventDealerSelection();"
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelectionGab('" & strDealerGroupID & "','" & CType(GetFromSession("DEALER"), Dealer).DealerCode & "');"

            If IsDealer Then
                txtNotes.Enabled = False
            Else
                txtNotes.Enabled = True
            End If
        End If
    End Sub

    Private Sub dgBabitEventProposalActivity_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitEventProposalActivity.ItemCommand
        Dim lblActivityType As Label
        Dim ddlFActivityType As DropDownList
        Dim ddlEActivityType As DropDownList
        Dim lblDescription As Label
        Dim txtFDescription As TextBox
        Dim txtEDescription As TextBox

        objEventProposalHeader = CType(sesHelper.GetSession(sessEventProposalHdr), BabitEventProposalHeader)
        arlEventProposalAct = CType(sesHelper.GetSession(sessEventProposalAct), ArrayList)
        If IsNothing(arlEventProposalAct) Then arlEventProposalAct = New ArrayList

        Select Case e.CommandName
            Case "add"
                ddlFActivityType = CType(e.Item.FindControl("ddlFActivityType"), DropDownList)
                txtFDescription = CType(e.Item.FindControl("txtFDescription"), TextBox)
                If ddlFActivityType.SelectedValue = "-1" Then
                    MessageBox.Show("Tipe Aktivitas harus diisi.")
                    Return
                End If
                If txtFDescription.Text.Trim = "" Then
                    MessageBox.Show("Deskripsi harus diisi.")
                    Return
                End If
                Dim objBabitEventProposalDetail As BabitEventProposalDetail = New BabitEventProposalDetail
                objBabitEventProposalDetail.BabitEventProposalHeader = New BabitEventProposalHeader
                objBabitEventProposalDetail.BabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlFActivityType.SelectedValue))
                objBabitEventProposalDetail.Description = txtFDescription.Text.Trim
                arlEventProposalAct.Add(objBabitEventProposalDetail)

            Case "save" 'Update this datagrid item   
                ddlEActivityType = CType(e.Item.FindControl("ddlEActivityType"), DropDownList)
                txtEDescription = CType(e.Item.FindControl("txtEDescription"), TextBox)
                If ddlEActivityType.SelectedValue = "-1" Then
                    MessageBox.Show("Tipe Aktivitas harus diisi.")
                    Return
                End If
                If txtEDescription.Text.Trim = "" Then
                    MessageBox.Show("Deskripsi harus diisi.")
                    Return
                End If
                Dim objBabitEventProposalDetail As BabitEventProposalDetail = CType(arlEventProposalAct(e.Item.ItemIndex), BabitEventProposalDetail)
                objBabitEventProposalDetail.BabitEventProposalHeader = objEventProposalHeader
                objBabitEventProposalDetail.BabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlEActivityType.SelectedValue))
                objBabitEventProposalDetail.Description = txtEDescription.Text.Trim()

                dgBabitEventProposalActivity.EditItemIndex = -1
                dgBabitEventProposalActivity.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgBabitEventProposalActivity.ShowFooter = False
                dgBabitEventProposalActivity.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Try
                    Dim oBabitEventProposalDetail As BabitEventProposalDetail = CType(arlEventProposalAct(e.Item.ItemIndex), BabitEventProposalDetail)
                    If oBabitEventProposalDetail.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sesHelper.GetSession(sessDeleteEventProposalAct), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oBabitEventProposalDetail)
                        sesHelper.SetSession(sessDeleteEventProposalAct, arrDelete)
                    End If
                    arlEventProposalAct.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try

            Case "cancel" 'Cancel Update this datagrid item 
                dgBabitEventProposalActivity.EditItemIndex = -1
                dgBabitEventProposalActivity.ShowFooter = True
        End Select

        sesHelper.SetSession(sessEventProposalAct, arlEventProposalAct)
        BindGridBabitProposalEventProposalAct()
    End Sub

    Private Function GetDataAcivitas() As ArrayList
        Dim sSQL As String = String.Empty
        sSQL = "select ID,BabitParameterHeaderID,ParameterDetailName,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime "
        sSQL += "from BabitParameterDetail "
        sSQL += "where BabitParameterHeaderID in ("
        sSQL += "select ID from BabitParameterHeader "
        sSQL += "where Status = 1 and ParameterCategory in ("
        sSQL += "select ValueID from StandardCode where Category = '" & strEnumBabitCategory & "' and ValueCode= '" & strValueCodeAct & "') "
        sSQL += "and BabitMasterEventTypeID in ("
        sSQL += "select ID from BabitMasterEventType where TypeCode = '" & strTypeCode & "'))"

        Dim result As ArrayList = New BabitParameterDetailFacade(User).RetrieveSP(sSQL)
        If IsNothing(result) Then result = New ArrayList

        Return result
    End Function

    Private Function GetDataBiaya() As ArrayList
        Dim strSql As String = String.Empty
        strSql = "select ValueID from StandardCode where Category = '" & strEnumBabitCategory & "' and ValueCode = '" & strValueCodeBiaya & "'"
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.InSet, "(" & strSql & ")"))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
        arrDDL = New BabitParameterHeaderFacade(User).Retrieve(criterias)

        If IsNothing(arrDDL) Then arrDDL = New ArrayList
        Return arrDDL
    End Function

    Private Sub dgBabitEventProposalActivity_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitEventProposalActivity.ItemDataBound

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
            Dim objBabitEventProposalDetail As BabitEventProposalDetail = CType(e.Item.DataItem, BabitEventProposalDetail)
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgBabitEventProposalActivity.CurrentPageIndex * dgBabitEventProposalActivity.PageSize)

            lblActivityType = CType(e.Item.FindControl("lblActivityType"), Label)
            lblDescription = CType(e.Item.FindControl("lblDescription"), Label)
            lblActivityType.Text = objBabitEventProposalDetail.BabitParameterDetail.ParameterDetailName
            lblDescription.Text = objBabitEventProposalDetail.Description
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim objBabitEventProposalDetail As BabitEventProposalDetail = CType(e.Item.DataItem, BabitEventProposalDetail)
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgBabitEventProposalActivity.CurrentPageIndex * dgBabitEventProposalActivity.PageSize)

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
            ddlActivityType.SelectedValue = objBabitEventProposalDetail.BabitParameterDetail.ID
        End If
    End Sub

    Private Sub dgBabitEventProposal_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitEventProposal.ItemCommand
        Dim oBabitParameterDetail As New BabitParameterDetail
        Dim ddlECategoryBabitEvent As DropDownList
        Dim ddlFCategoryBabitEvent As DropDownList
        Dim ddlEJenisBabitEvent As DropDownList
        Dim ddlFJenisBabitEvent As DropDownList
        Dim lblCategoryBabitEvent As Label
        Dim lblJenisBabitEvent As Label
        Dim txtEItem As TextBox
        Dim txtFItem As TextBox
        Dim txtEQty As TextBox
        Dim txtFQty As TextBox
        Dim txtEPrice As TextBox
        Dim txtFPrice As TextBox
        Dim txtETotalPrice As TextBox
        Dim txtFTotalPrice As TextBox
        Dim txtEDesc As TextBox
        Dim txtFDesc As TextBox

        objEventProposalHeader = CType(sesHelper.GetSession(sessEventProposalHdr), BabitEventProposalHeader)
        arlEventProposalDtl = CType(sesHelper.GetSession(sessEventProposalDtl), ArrayList)

        Select Case e.CommandName
            Case "add"
                ddlFCategoryBabitEvent = CType(e.Item.FindControl("ddlFCategoryBabitEvent"), DropDownList)
                ddlFJenisBabitEvent = CType(e.Item.FindControl("ddlFJenisBabitEvent"), DropDownList)
                txtFItem = CType(e.Item.FindControl("txtFItem"), TextBox)
                txtFQty = CType(e.Item.FindControl("txtFQty"), TextBox)
                txtFPrice = CType(e.Item.FindControl("txtFPrice"), TextBox)
                txtFTotalPrice = CType(e.Item.FindControl("txtFTotalPrice"), TextBox)
                txtFDesc = CType(e.Item.FindControl("txtFDesc"), TextBox)

                If ddlFCategoryBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Event Kategori harus diisi.")
                    Return
                End If
                If ddlFJenisBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Jenis Event harus diisi.")
                    Return
                End If
                If txtFItem.Text.Trim = "" Then
                    MessageBox.Show("Item Event harus diisi.")
                    Return
                End If
                If txtFQty.Text.Trim = "" OrElse txtFQty.Text.Trim = "0" Then
                    MessageBox.Show("Qty Event harus diisi.")
                    Return
                End If
                If txtFPrice.Text.Trim = "" OrElse txtFPrice.Text.Trim = "0" Then
                    MessageBox.Show("Price Event harus diisi.")
                    Return
                End If
                txtFTotalPrice.Text = txtFQty.Text * txtFPrice.Text()

                oBabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlFJenisBabitEvent.SelectedValue))

                Dim oBabitEventProposalDetail As New BabitEventProposalDetail
                oBabitEventProposalDetail.BabitEventProposalHeader = New BabitEventProposalHeader
                oBabitEventProposalDetail.BabitParameterDetail = oBabitParameterDetail
                oBabitEventProposalDetail.Item = txtFItem.Text.Trim
                oBabitEventProposalDetail.Qty = txtFQty.Text.Trim
                oBabitEventProposalDetail.Price = txtFPrice.Text.Trim
                oBabitEventProposalDetail.TotalPrice = oBabitEventProposalDetail.Qty * oBabitEventProposalDetail.Price
                oBabitEventProposalDetail.Description = txtFDesc.Text.Trim
                arlEventProposalDtl.Add(oBabitEventProposalDetail)

            Case "save" 'Update this datagrid item   
                ddlECategoryBabitEvent = CType(e.Item.FindControl("ddlECategoryBabitEvent"), DropDownList)
                ddlEJenisBabitEvent = CType(e.Item.FindControl("ddlEJenisBabitEvent"), DropDownList)
                txtEItem = CType(e.Item.FindControl("txtEItem"), TextBox)
                txtEQty = CType(e.Item.FindControl("txtEQty"), TextBox)
                txtEPrice = CType(e.Item.FindControl("txtEPrice"), TextBox)
                txtETotalPrice = CType(e.Item.FindControl("txtETotalPrice"), TextBox)
                txtEDesc = CType(e.Item.FindControl("txtEDesc"), TextBox)

                If ddlECategoryBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Event Kategori harus diisi.")
                    Return
                End If
                If ddlEJenisBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Jenis Event harus diisi.")
                    Return
                End If
                If txtEItem.Text.Trim = "" Then
                    MessageBox.Show("Item Event harus diisi.")
                    Return
                End If
                If txtEQty.Text.Trim = "" OrElse txtEQty.Text.Trim = "0" Then
                    MessageBox.Show("Qty Event harus diisi.")
                    Return
                End If
                If txtEPrice.Text.Trim = "" OrElse txtEPrice.Text.Trim = "0" Then
                    MessageBox.Show("Price Event harus diisi.")
                    Return
                End If
                txtETotalPrice.Text = txtEQty.Text * txtEPrice.Text()

                oBabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlEJenisBabitEvent.SelectedValue))
                Dim oBabitEventProposalDetail As BabitEventProposalDetail = CType(arlEventProposalDtl(e.Item.ItemIndex), BabitEventProposalDetail)
                oBabitEventProposalDetail.BabitEventProposalHeader = objEventProposalHeader
                oBabitEventProposalDetail.BabitParameterDetail = oBabitParameterDetail
                oBabitEventProposalDetail.Item = txtEItem.Text.Trim
                oBabitEventProposalDetail.Qty = txtEQty.Text.Trim
                oBabitEventProposalDetail.Price = txtEPrice.Text.Trim
                oBabitEventProposalDetail.Description = txtEDesc.Text.Trim()
                dgBabitEventProposal.EditItemIndex = -1
                dgBabitEventProposal.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgBabitEventProposal.ShowFooter = False
                dgBabitEventProposal.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Try
                    Dim oDeleteBabitEventProposalDetail As BabitEventProposalDetail = CType(arlEventProposalDtl(e.Item.ItemIndex), BabitEventProposalDetail)
                    If oDeleteBabitEventProposalDetail.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sesHelper.GetSession(sessDeleteEventProposalDtl), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oDeleteBabitEventProposalDetail)
                        sesHelper.SetSession(sessDeleteEventProposalDtl, arrDelete)
                    End If
                    arlEventProposalDtl.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try

            Case "cancel" 'Cancel Update this datagrid item 
                dgBabitEventProposal.EditItemIndex = -1
                dgBabitEventProposal.ShowFooter = True
        End Select

        sesHelper.SetSession(sessEventProposalDtl, arlEventProposalDtl)
        BindGridBabitProposalEventProposalDtl()
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

    Private Sub dgBabitEventProposal_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitEventProposal.ItemDataBound
        Dim ddlECategoryBabitEvent As DropDownList
        Dim ddlFCategoryBabitEvent As DropDownList
        Dim ddlEJenisBabitEvent As DropDownList
        Dim ddlFJenisBabitEvent As DropDownList
        Dim lblCategoryBabitEvent As Label
        Dim lblJenisBabitEvent As Label
        Dim lblItem As Label
        Dim lblQty As Label
        Dim lblPrice As Label
        Dim lblTotalPrice As Label
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        If e.Item.ItemType = ListItemType.Footer Then
            ddlFCategoryBabitEvent = CType(e.Item.FindControl("ddlFCategoryBabitEvent"), DropDownList)
            ddlFJenisBabitEvent = CType(e.Item.FindControl("ddlFJenisBabitEvent"), DropDownList)

            Dim arrDDL As ArrayList = GetDataBiaya()
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
            Dim oBED As BabitEventProposalDetail = CType(e.Item.DataItem, BabitEventProposalDetail)

            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitParameterHeaderID = oBED.BabitParameterDetail.BabitParameterHeader.ID
            Else
                If intBabitParameterHeaderID <> oBED.BabitParameterDetail.BabitParameterHeader.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitParameterHeaderID = oBED.BabitParameterDetail.BabitParameterHeader.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitEventProposal.CurrentPageIndex * dgBabitEventProposal.PageSize)

            lblCategoryBabitEvent = CType(e.Item.FindControl("lblCategoryBabitEvent"), Label)
            lblJenisBabitEvent = CType(e.Item.FindControl("lblJenisBabitEvent"), Label)
            lblItem = CType(e.Item.FindControl("lblItem"), Label)
            lblQty = CType(e.Item.FindControl("lblQty"), Label)
            lblPrice = CType(e.Item.FindControl("lblPrice"), Label)
            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnDelete = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lblTotalPrice = CType(e.Item.FindControl("lblTotalPrice"), Label)

            lblCategoryBabitEvent.Text = oBED.BabitParameterDetail.BabitParameterHeader.ParameterName()
            lblJenisBabitEvent.Text = oBED.BabitParameterDetail.ParameterDetailName()
            lblQty.Text = oBED.Qty
            lblPrice.Text = oBED.Price

            If lblItem.Text.Trim.ToLower = "total biaya :" Then
                e.Item.Cells(3).BackColor = Color.SkyBlue
                e.Item.Cells(4).BackColor = Color.SkyBlue
                e.Item.Cells(5).BackColor = Color.SkyBlue
                e.Item.Cells(6).BackColor = Color.SkyBlue
                lblItem.Font.Bold = True
                lblTotalPrice.Font.Bold = True
                lblJenisBabitEvent.Text = ""
                lblQty.Text = ""
                lblPrice.Text = ""
                lbtnEdit.Attributes("style") = "display:none"
                lbtnDelete.Attributes("style") = "display:none"
                lblCategoryBabitEvent.Attributes("style") = "display:none"
                e.Item.Cells(0).Text = ""
            Else
                lblTotalPrice.Text = Format(oBED.Qty * oBED.Price, "###,###,##0")
                lblItem.Font.Bold = False
                lblTotalPrice.Font.Bold = False
                lbtnEdit.Attributes("style") = "display:table-row"
                lbtnDelete.Attributes("style") = "display:table-row"
                lblCategoryBabitEvent.Attributes("style") = "display:table-row"
            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oBED As BabitEventProposalDetail = CType(e.Item.DataItem, BabitEventProposalDetail)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitParameterHeaderID = oBED.BabitParameterDetail.BabitParameterHeader.ID
            Else
                If intBabitParameterHeaderID <> oBED.BabitParameterDetail.BabitParameterHeader.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitParameterHeaderID = oBED.BabitParameterDetail.BabitParameterHeader.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitEventProposal.CurrentPageIndex * dgBabitEventProposal.PageSize)

            ddlECategoryBabitEvent = CType(e.Item.FindControl("ddlECategoryBabitEvent"), DropDownList)

            Dim arrDDL As ArrayList = GetDataBiaya()
            With ddlECategoryBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBED.BabitParameterDetail.BabitParameterHeader.ID
            End With

            ddlEJenisBabitEvent = CType(e.Item.FindControl("ddlEJenisBabitEvent"), DropDownList)
            arrDDL = New ArrayList
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, oBED.BabitParameterDetail.BabitParameterHeader.ID))
            arrDDL = New BabitParameterDetailFacade(User).Retrieve(criterias2)
            With ddlEJenisBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterDetailName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBED.BabitParameterDetail.ID
            End With
        End If
    End Sub

    Private Sub dgUploadFile_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUploadFile.ItemCommand
        Dim _arrDataUploadFile As ArrayList = CType(sesHelper.GetSession(sessEventProposalDoc), ArrayList)
        If IsNothing(_arrDataUploadFile) Then _arrDataUploadFile = New ArrayList

        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("UploadFile"), HtmlInputFile)
                Dim txtKeterangan As TextBox = CType(e.Item.FindControl("txtKeterangan"), TextBox)
                Dim ddlFEventDealerRequiredDocument As DropDownList = CType(e.Item.FindControl("ddlFEventDealerRequiredDocument"), DropDownList)
                Dim objPostedData As HttpPostedFile
                Dim objBabitEventProposalDocument As BabitEventProposalDocument = New BabitEventProposalDocument()
                Dim sFileName As String

                '========= Validasi  =======================================================================
                If ddlFEventDealerRequiredDocument.SelectedValue = "-1" Then
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
                        BindGridBabitProposalEventProposalDoc()
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
                        Dim strBabitPathFile As String = "\EVENT\" & objDealer.DealerCode & "\EventProposal\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        Dim strDestFile As String = strBabitPathConfig & strBabitPathFile '--Destination File                       

                        objBabitEventProposalDocument.BabitEventProposalHeader = New BabitEventProposalHeader()
                        objBabitEventProposalDocument.EventDealerRequiredDocument = New EventDealerRequiredDocumentFacade(User).Retrieve(CInt(ddlFEventDealerRequiredDocument.SelectedValue))
                        objBabitEventProposalDocument.AttachmentData = objPostedData
                        objBabitEventProposalDocument.FileName = sFileName
                        objBabitEventProposalDocument.Path = strDestFile
                        objBabitEventProposalDocument.FileDescription = IIf(txtKeterangan.Text.Trim = "", "Proposal Event Document", txtKeterangan.Text)

                        UploadAttachment(objBabitEventProposalDocument, TempDirectory)

                        _arrDataUploadFile.Add(objBabitEventProposalDocument)
                        sesHelper.SetSession(sessEventProposalDoc, _arrDataUploadFile)
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If

            Case "Delete" 'Delete this datagrid item 
                Dim oBabitEventProposalDocument As BabitEventProposalDocument = CType(_arrDataUploadFile(e.Item.ItemIndex), BabitEventProposalDocument)
                If oBabitEventProposalDocument.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sesHelper.GetSession(sessDeleteEventProposalDoc), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oBabitEventProposalDocument)
                    sesHelper.SetSession(sessDeleteEventProposalDoc, arrDelete)
                End If
                RemoveBabitEventProposalAttachment(CType(_arrDataUploadFile(e.Item.ItemIndex), BabitEventProposalDocument), TempDirectory)
                _arrDataUploadFile.RemoveAt(e.Item.ItemIndex)

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End Select

        BindGridBabitProposalEventProposalDoc()
    End Sub

    Private Sub RemoveBabitEventProposalAttachment(ByVal ObjAttachment As BabitEventProposalDocument, ByVal TargetPath As String)
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

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub dgUploadFile_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUploadFile.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgUploadFile.CurrentPageIndex * dgUploadFile.PageSize)

            Dim arrUpload As ArrayList = CType(sesHelper.GetSession(sessEventProposalDoc), ArrayList)
            If Not IsNothing(arrUpload) AndAlso arrUpload.Count > 0 Then
                Dim objBabitEventProposalDocument As BabitEventProposalDocument = arrUpload(e.Item.ItemIndex)
                Dim lblFileName As Label = CType(e.Item.FindControl("lblFileName"), Label)
                lblFileName.Text = Path.GetFileName(objBabitEventProposalDocument.FileName)
            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            Dim ddlFEventDealerRequiredDocument As DropDownList = CType(e.Item.FindControl("ddlFEventDealerRequiredDocument"), DropDownList)
            Dim arrDDL As ArrayList = New ArrayList
            If hdnEventID.Value.Trim <> "" Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDealerRequiredDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(EventDealerRequiredDocument), "DocumentType", MatchType.Exact, 2))
                criterias.opAnd(New Criteria(GetType(EventDealerRequiredDocument), "EventDealerHeader.ID", MatchType.Exact, hdnEventID.Value))
                arrDDL = New EventDealerRequiredDocumentFacade(User).Retrieve(criterias)
            End If
            If Not IsNothing(arrDDL) Then
                With ddlFEventDealerRequiredDocument
                    .Items.Clear()
                    .DataSource = arrDDL
                    .DataTextField = "DocumentName"
                    .DataValueField = "ID"
                    .DataBind()
                    .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                End With
            End If
        End If
    End Sub

    Private Function ValidateCollaborateDealers(ByVal _dealers As String) As Boolean
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
        Dim strDealerDuplication As String = ValidateDealerDuplication(_dealers)
        If strDealerDuplication <> String.Empty Then
            MessageBox.Show("Duplikasi Dealer " + strDealerDuplication)
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
            If Not ValidateCollaborateDealers(Me.txtCollaborateDealer.Text.Trim) Then Exit Sub
        End If

        arlEventProposalDtl = CType(sesHelper.GetSession(sessEventProposalDtl), ArrayList)
        arlEventProposalDtl = New System.Collections.ArrayList(
                (From obj As BabitEventProposalDetail In arlEventProposalDtl.OfType(Of BabitEventProposalDetail)()
                    Where obj.Item <> "Total Biaya :"
                    Select obj).ToList())

        arlEventProposalAct = CType(sesHelper.GetSession(sessEventProposalAct), ArrayList)
        arlEventProposalDoc = CType(sesHelper.GetSession(sessEventProposalDoc), ArrayList)

        arlDeleteEventProposalDtl = CType(sesHelper.GetSession(sessDeleteEventProposalDtl), ArrayList)
        arlDeleteEventProposalDoc = CType(sesHelper.GetSession(sessDeleteEventProposalDoc), ArrayList)
        arlDeleteEventProposalAct = CType(sesHelper.GetSession(sessDeleteEventProposalAct), ArrayList)

        arlDeleteEventProposalDtl = IIf(Not IsNothing(arlDeleteEventProposalDtl), arlDeleteEventProposalDtl, New ArrayList)
        arlDeleteEventProposalDoc = IIf(Not IsNothing(arlDeleteEventProposalDoc), arlDeleteEventProposalDoc, New ArrayList)
        arlDeleteEventProposalAct = IIf(Not IsNothing(arlDeleteEventProposalAct), arlDeleteEventProposalAct, New ArrayList)

        Dim _oDealer As Dealer = New Dealer
        If IsLoginAsDealer() Then
            _oDealer = SesDealer
        Else
            _oDealer = New DealerFacade(User).Retrieve(Me.txtDealerCode.Text)
        End If
        Dim strEventRegNumber As String = String.Empty
        If Request.QueryString("Mode") <> "Edit" Then
            objEventProposalHeader = New BabitEventProposalHeader
            strEventRegNumber = GetRegNumber()
        Else
            objEventProposalHeader = CType(sesHelper.GetSession(sessEventProposalHdr), BabitEventProposalHeader)
            strEventRegNumber = Me.lblEventRegNumber.Text
        End If
        objEventProposalHeader.Dealer = _oDealer
        objEventProposalHeader.DealerBranch = New DealerBranchFacade(User).Retrieve(txtTOCode.Text)
        objEventProposalHeader.EventDealerHeader = New EventDealerHeaderFacade(User).Retrieve(CType(hdnEventID.Value, Integer))
        objEventProposalHeader.EventRegNumber = strEventRegNumber
        objEventProposalHeader.PeriodStart = icPeriodStart.Value
        objEventProposalHeader.PeriodEnd = icPeriodEnd.Value
        If Request.QueryString("Mode") <> "Edit" Then
            objEventProposalHeader.EventStatus = 0  '--- Status Baru
        End If
        objEventProposalHeader.EventProposalName = txtProposalEventName.Text
        objEventProposalHeader.Notes = txtNotes.Text
        objEventProposalHeader.City = New CityFacade(User).Retrieve(CInt(ddlKota.SelectedValue))
        objEventProposalHeader.LocationName = txtLocationName.Text.Trim

        Dim strDealerID As String = String.Empty
        If txtCollaborateDealer.Text.Trim() <> "" Then
            Dim CollaborateDealerCode As String() = txtCollaborateDealer.Text.Split(";")
            For Each dealerCode As String In CollaborateDealerCode
                Dim oDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(dealerCode)
                If Not IsNothing(oDealer) AndAlso oDealer.ID > 0 Then
                    If strDealerID = String.Empty Then
                        strDealerID = CStr(oDealer.ID)
                    Else
                        strDealerID += ";" & CStr(oDealer.ID)
                    End If
                End If
            Next
        End If
        objEventProposalHeader.CollaborateDealer = strDealerID

        Dim _result As Integer = 0
        If Request.QueryString("Mode") <> "Edit" Then
            _result = New BabitEventProposalDetailFacade(User).InsertTransaction(objEventProposalHeader, arlEventProposalDtl, arlEventProposalDoc, arlEventProposalAct)
        Else
            _result = New BabitEventProposalDetailFacade(User).UpdateTransaction(objEventProposalHeader, arlEventProposalDtl, arlDeleteEventProposalDtl, arlEventProposalDoc, arlDeleteEventProposalDoc, arlEventProposalAct, arlDeleteEventProposalAct)
        End If

        Dim strJs As String = String.Empty
        If _result > 0 Then
            CommitAttachment(arlEventProposalDoc)
            If Request.QueryString("Mode") = "Edit" Then
                RemoveBabitProposalAttachment(arlDeleteEventProposalDoc, TargetDirectory)
            End If
            strJs = "alert('Simpan Data Berhasil');"
            strJs += "window.location = '../Babit/FrmBabitEventProposalList.aspx';"
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
                For Each obj As BabitEventProposalDocument In AttachmentCollection
                    finfo = New FileInfo(TargetPath + obj.Path)
                    If finfo.Exists Then
                        finfo.Delete()
                    End If
                Next

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnGetInfoDealer_Click(sender As Object, e As EventArgs) Handles btnGetInfoDealer.Click
        Dim oDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
        lblDealerCodeName.Text = oDealer.DealerCode & " / " & oDealer.DealerName
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

    Private Sub btnFindEvent_Click(sender As Object, e As EventArgs) Handles btnFindEvent.Click
        If Me.hdnEventID.Value.Trim = "" Then Exit Sub

        arlEventProposalDoc = CType(sesHelper.GetSession(sessEventProposalDoc), ArrayList)
        If IsNothing(arlEventProposalDoc) Then arlEventProposalDoc = New ArrayList

        Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDealerRequiredDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(EventDealerRequiredDocument), "EventDealerHeader.ID", MatchType.Exact, hdnEventID.Value))
        Dim arlEventDealerRequiredDocument As ArrayList = New EventDealerRequiredDocumentFacade(User).Retrieve(criterias)

        Dim blnIsExistDoc As Boolean = False
        For Each obj As BabitEventProposalDocument In arlEventProposalDoc
            For Each obj2 As EventDealerRequiredDocument In arlEventDealerRequiredDocument
                If obj.EventDealerRequiredDocument.ID = obj2.ID Then
                    blnIsExistDoc = True
                    Exit For
                End If
            Next
            If blnIsExistDoc = False Then
                sesHelper.SetSession(sessEventProposalDoc, New ArrayList)
                Exit For
            End If
        Next

        BindGridBabitProposalEventProposalDoc()
        Me.txtEventCode.Text = hdnEventName.Value
        Dim objEventDealer As EventDealerHeader = New EventDealerHeaderFacade(User).Retrieve(CType(hdnEventID.Value, Integer))
        If Not IsNothing(objEventDealer) Then
            Me.lblPeriodStartEvent.Text = objEventDealer.PeriodStart.ToString("dd/MM/yyyy")
            Me.lblPeriodEndEvent.Text = objEventDealer.PeriodEnd.ToString("dd/MM/yyyy")
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/Babit/FrmBabitEventProposalList.aspx")
    End Sub

#End Region

End Class
