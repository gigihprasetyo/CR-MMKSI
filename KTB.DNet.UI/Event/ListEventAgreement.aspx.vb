#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.WebCC
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class ListEventAgreement
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlSalesmanArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbPeriode As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlModel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlNamaKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidate As System.Web.UI.WebControls.Button
    Protected WithEvents tblCategoryModelType As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents calDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dtgEvent As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSubTitle As System.Web.UI.WebControls.Label
    Protected WithEvents btnExcelDwl As System.Web.UI.WebControls.Button
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"

#End Region

#Region " Private Variables"
    Private IsRemovedFile As Boolean = False
#End Region

#Region "Custom Method"
    Private Property StoreCriteria() As CriteriaComposite
        Get
            Return (New SessionHelper).GetSession("StorePageCriteria")
        End Get
        Set(ByVal Value As CriteriaComposite)
            Dim sHelper As New SessionHelper
            sHelper.SetSession("StorePageCriteria", Value)
        End Set
    End Property
    Private Property GridSortColumn() As String
        Get
            If ViewState("SortColumn") Is Nothing Then
                ViewState("SortColumn") = "ID"
            End If
            Return ViewState("SortColumn")
        End Get
        Set(ByVal Value As String)
            ViewState("SortColumn") = Value
        End Set
    End Property
    Private Property GridSortDirection() As Sort.SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = Sort.SortDirection.ASC
            End If
            Return ViewState("SortDirection")
        End Get
        Set(ByVal Value As Sort.SortDirection)
            ViewState("SortDirection") = Value
        End Set
    End Property
    Private Sub FillSalesmanArea()
        ddlSalesmanArea.Items.Clear()
        Dim objArea As New SalesmanAreaFacade(User)
        ddlSalesmanArea.DataSource = objArea.RetrieveActiveList
        ddlSalesmanArea.DataTextField = "AreaDesc"
        ddlSalesmanArea.DataValueField = "ID"
        ddlSalesmanArea.DataBind()
        ddlSalesmanArea.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub FillJenisKegiatan()
        ddlJenisKegiatan.Items.Clear()
        Dim objActivityType As New ActivityTypeFacade(User)
        ddlJenisKegiatan.DataSource = objActivityType.RetrieveActiveList()
        ddlJenisKegiatan.DataTextField = "ActivityName"
        ddlJenisKegiatan.DataValueField = "ID"
        ddlJenisKegiatan.DataBind()
        ddlJenisKegiatan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub ShowModelCategoryType(ByVal IsVisible As Boolean)
        tblCategoryModelType.Visible = IsVisible
    End Sub
    Private Sub FillCategory()
        ddlCategory.Items.Clear()
        Dim objCategory As New CategoryFacade(User)
        ddlCategory.DataSource = objCategory.RetrieveActiveList
        ddlCategory.DataTextField = "Description"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub FillModel(ByVal CategoryID As Integer)
        ddlModel.Items.Clear()
        If CategoryID > -1 Then
            Dim objModel As New VechileModelFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VechileModel), "Category", CategoryID))
            ddlModel.DataSource = objModel.RetrieveList("Description", Sort.SortDirection.ASC, crit)
            ddlModel.DataTextField = "Description"
            ddlModel.DataValueField = "ID"
            ddlModel.DataBind()
            ddlModel.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlModel.Items.Insert(0, New ListItem("Pilih Kategori", -1))
        End If
    End Sub
    Private Sub FillType(ByVal ModelID As Integer)
        ddlType.Items.Clear()
        If ModelID > -1 Then
            Dim objType As New VechileTypeFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", CType(DBRowStatus.Active, Short)))
            Dim sorts As New SortCollection
            sorts.Add(New Sort(GetType(VechileType), "Description", Sort.SortDirection.ASC))
            crit.opAnd(New Criteria(GetType(VechileType), "VechileModel", ModelID))
            ddlType.DataSource = objType.RetrieveByCriteria(crit, sorts)
            ddlType.DataTextField = "Description"
            ddlType.DataValueField = "ID"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlType.Items.Insert(0, New ListItem("Pilih Model", -1))
        End If
    End Sub
    Private Sub FillYear()
        ddlYear.Items.Clear()
        For i As Int32 = Now.Year - 5 To Now.Year + 5
            ddlYear.Items.Add(i)
        Next
        ddlYear.SelectedValue = Now.Year
    End Sub
    Private Sub FillNamaKegiatan()
        Dim objFacade As New EventParameterFacade(User)
        ddlNamaKegiatan.Items.Clear()
        If ddlJenisKegiatan.SelectedValue <> -1 Then
            ddlNamaKegiatan.DataSource = objFacade.RetrieveNamaKegiatan(ddlJenisKegiatan.SelectedValue)
            ddlNamaKegiatan.DataTextField = "EventName"
            ddlNamaKegiatan.DataValueField = "EventName"
            ddlNamaKegiatan.DataBind()
            ddlNamaKegiatan.Items.Insert(0, "Silahkan Pilih")
        Else
            ddlNamaKegiatan.Items.Insert(0, New ListItem("Pilih Jenis Kegiatan", -1))
        End If
    End Sub
    'Private Sub FillStatus()
    '    ddlStatusProposal.Items.Clear()
    '    For Each enus As EnumItem In EnumEventProposalStatus.RetrieveStatus
    '        ddlStatusProposal.Items.Add(New ListItem(enus.Name, enus.ID))
    '    Next
    '    ddlStatusProposal.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    'End Sub
    Private Sub HideButtonIfNew()
        'Dim IsNew As Boolean = ddlStatusProposal.SelectedValue = CType(EnumEventProposalStatus.EventProposalStatus.Baru, String)
        Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")
        If Not IsNothing(objUserInfo.Dealer) And objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            dtgEvent.Columns(dtgEvent.Columns.Count - 1).Visible = True
            btnValidate.Visible = True
        Else
            dtgEvent.Columns(dtgEvent.Columns.Count - 1).Visible = False
            btnValidate.Visible = False
        End If
    End Sub
    Private Function BuiltCriteria() As CriteriaComposite
        Dim objComposite As New CriteriaComposite(New Criteria(GetType(V_EventProposalAgreement), "RowStatus", _
            CType(DBRowStatus.Active, Short)))

        Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")
        If Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            'objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "EventAgreementStatus", CInt(EnumEventAgreementStatus.EventAgreementStatus.Baru)))
        Else
            objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "EventAgreementStatus", CInt(EnumEventAgreementStatus.EventAgreementStatus.Validasi)))
        End If

        If txtDealerCode.Text.Length > 0 Then
            objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "Dealer.DealerCode", MatchType.InSet, _
                String.Format("('{0}')", txtDealerCode.Text.Replace(";", "','"))))
        End If
        If ddlSalesmanArea.SelectedValue <> -1 Then
            objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "EventParameter.SalesmanArea", _
                ddlSalesmanArea.SelectedValue))
        End If
        If cbPeriode.Checked Then
            objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "EventParameter.EventDateStart", _
                MatchType.GreaterOrEqual, calDari.Value))
            objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "EventParameter.EventDateEnd", _
                MatchType.LesserOrEqual, calSampai.Value))
        End If
        If ddlJenisKegiatan.SelectedValue <> -1 Then
            objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "EventParameter.ActivityType", _
                ddlJenisKegiatan.SelectedValue))
        End If
        If ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Small_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Launching_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Exhibition_Khusus, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Showroom_Event, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Others, Integer) Then
            If ddlCategory.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "EventParameter.Category", ddlCategory.SelectedValue))
            End If
            If ddlModel.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "EventParameter.VechileType.VechileModel", _
                    ddlModel.SelectedValue))
            End If
            If ddlType.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "EventParameter.VechileType", _
                    ddlType.SelectedValue))
            End If
        End If
        If ddlNamaKegiatan.SelectedIndex > 0 Then
            objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "EventParameter.EventName", _
                ddlNamaKegiatan.SelectedItem.Text))
            objComposite.opAnd(New Criteria(GetType(V_EventProposalAgreement), "EventParameter.EventYear", _
                ddlYear.SelectedValue))
        End If
        Return objComposite
    End Function
    Private Sub BindGrid()
        Dim objFacade As New EventProposalFacade(User)
        Dim totalRows As Int32 = 0
        dtgEvent.DataSource = objFacade.RetrieveAgreement(StoreCriteria, dtgEvent.CurrentPageIndex + 1, _
            dtgEvent.PageSize, totalRows, GridSortColumn, GridSortDirection)
        dtgEvent.VirtualItemCount = totalRows
        dtgEvent.DataBind()
        If totalRows = 0 Then
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub
    Private Sub InitAuthorization()
        Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")
        If Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            txtDealerCode.Text = objUserInfo.Dealer.DealerCode
            txtDealerCode.Enabled = False
            lblSearchDealer.Enabled = True
            lblSearchDealer.Visible = False
            'ddlStatusProposal.SelectedValue = CType(EnumEventProposalStatus.EventProposalStatus.Validasi, Int32)
            'trStatus.Visible = False
        Else
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            btnValidate.Visible = False
        End If
    End Sub
    Private Sub HideColumn()
        If ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Truck_Campaign, Integer) Then
            dtgEvent.Columns(7).Visible = False
            dtgEvent.Columns(8).Visible = False
            dtgEvent.Columns(9).Visible = False
            dtgEvent.Columns(10).Visible = False
            dtgEvent.Columns(11).Visible = False
            dtgEvent.Columns(12).Visible = True
        Else
            dtgEvent.Columns(7).Visible = True
            dtgEvent.Columns(8).Visible = True
            dtgEvent.Columns(9).Visible = True
            dtgEvent.Columns(10).Visible = True
            dtgEvent.Columns(11).Visible = True
            dtgEvent.Columns(12).Visible = False
        End If
    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnValidate.Attributes.Add("onclick", "return confirm('Yakin data akan divalidasi? Jika Ya Tekan Yes, Jika Tidak Tekan No');")
        If Not IsPostBack Then
            HideButtonIfNew()
            FillSalesmanArea()
            FillJenisKegiatan()
            FillNamaKegiatan()
            ShowModelCategoryType(False)
            FillCategory()
            ddlCategory_SelectedIndexChanged(Nothing, Nothing)
            ddlModel_SelectedIndexChanged(Nothing, Nothing)
            FillYear()
            'FillStatus()
            InitAuthorization()
        End If
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        HideButtonIfNew()
        HideColumn()
        dtgEvent.EditItemIndex = -1
        dtgEvent.CurrentPageIndex = 0
        StoreCriteria = BuiltCriteria()
        BindGrid()
    End Sub
    Private Sub ddlJenisKegiatan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJenisKegiatan.SelectedIndexChanged
        ShowModelCategoryType(ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Small_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Launching_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Exhibition_Khusus, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Showroom_Event, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Others, Integer))
        FillNamaKegiatan()
    End Sub
    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        FillModel(ddlCategory.SelectedValue)
        FillType(ddlModel.SelectedValue)
    End Sub
    Private Sub ddlModel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlModel.SelectedIndexChanged
        FillType(ddlModel.SelectedValue)
    End Sub
    Private Sub dtgEvent_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEvent.SortCommand
        If GridSortColumn = e.SortExpression Then
            Select Case GridSortDirection
                Case Sort.SortDirection.ASC
                    GridSortDirection = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    GridSortDirection = Sort.SortDirection.ASC
            End Select
        Else
            GridSortColumn = e.SortExpression
            GridSortDirection = Sort.SortDirection.ASC
        End If
        dtgEvent.EditItemIndex = -1
        dtgEvent.CurrentPageIndex = 0
        BindGrid()
    End Sub
    Private Sub cbPeriode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPeriode.CheckedChanged
        calDari.Enabled = cbPeriode.Checked
        calSampai.Enabled = cbPeriode.Checked
    End Sub
    Private Sub dtgEvent_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEvent.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgEvent.PageSize * dtgEvent.CurrentPageIndex)).ToString
            Dim objEvent As V_EventProposalAgreement = e.Item.DataItem
            e.Item.Cells(16).Text = IIf(objEvent.EventAgreementStatus = EnumEventAgreementStatus.EventAgreementStatus.Baru, _
                EnumEventAgreementStatus.EventAgreementStatus.Baru.ToString, _
                EnumEventAgreementStatus.EventAgreementStatus.Validasi.ToString)
            If dtgEvent.EditItemIndex = e.Item.ItemIndex Then
                Dim lnkEditDownLoad As HyperLink = e.Item.FindControl("lnkEditDownLoad")
                Dim flUpload As HtmlInputFile = e.Item.FindControl("flUpload")
                Dim lnbRemoveFile As LinkButton = e.Item.FindControl("lnbRemoveFile")
                lnkEditDownLoad.Visible = objEvent.SubsidiFile.Length > 0 AndAlso Not IsRemovedFile
                lnbRemoveFile.Visible = objEvent.SubsidiFile.Length > 0 AndAlso Not IsRemovedFile
                flUpload.Visible = objEvent.SubsidiFile.Length = 0 OrElse IsRemovedFile
                If objEvent.SubsidiFile.Length > 0 Then
                    lnkEditDownLoad.NavigateUrl = String.Format("~\Download.aspx?file={0}\({1}){2}", _
                        KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.ID, _
                        objEvent.SubsidiFile)
                End If
            Else
                Dim lnkDownload As HyperLink = e.Item.FindControl("lnkDownload")
                If objEvent.SubsidiFile.Length > 0 Then
                    lnkDownload.NavigateUrl = String.Format("~\Download.aspx?file={0}\({1}){2}", _
                        KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.ID, _
                        objEvent.SubsidiFile)
                End If
            End If
        End If
    End Sub
    Private Sub dtgEvent_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEvent.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                dtgEvent.EditItemIndex = e.Item.ItemIndex
            Case "Update"
                Dim evtId As Int32 = e.CommandArgument
                Dim objFacade As New EventProposalFacade(User)
                Dim objEvent As EventProposal = objFacade.Retrieve(evtId)
                Dim txtFavorCost As TextBox = e.Item.FindControl("txtFavorCost")
                Dim flUpload As HtmlInputFile = e.Item.FindControl("flUpload")
                If flUpload.Visible AndAlso flUpload.PostedFile.FileName.Length > 0 Then
                    Dim objFileServer As New UploadToWebServer
                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                    If imp.Start() Then
                        File.Delete(String.Format("{0}\{1}\({2}){3}", _
                            KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                            objEvent.ID, objEvent.SubsidiFile))
                        objFileServer.Upload(flUpload.PostedFile.InputStream, String.Format("{0}\{1}\({2}){3}", _
                            KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), _
                            objEvent.ID, Path.GetFileName(flUpload.PostedFile.FileName)))
                    End If
                    objEvent.SubsidiFile = Path.GetFileName(flUpload.PostedFile.FileName)
                    imp.StopImpersonate()
                    imp = Nothing
                ElseIf objEvent.SubsidiFile.Length = 0 AndAlso Not flUpload.PostedFile Is Nothing Then
                    MessageBox.Show("Masukkan surat subsidi")
                    Exit Sub
                End If
                Dim objEventView As V_EventProposalAgreement = objFacade.RetrieveAgreement(objEvent.ID)
                Dim approvedCost As Decimal = 0
                If IsNumeric(txtFavorCost.Text) Then
                    approvedCost = txtFavorCost.Text
                    If objEventView.TotalCost > 0 AndAlso approvedCost > (objEventView.TotalCost / 2) Then
                        MessageBox.Show("Biaya disetujui tidak bisa lebih besar dari 1/2 biaya total")
                        Exit Sub
                    End If
                End If
                objEvent.ApproveCost = approvedCost
                Dim objHistory As New EventProposalHistoryAgreement
                objHistory.ActivitySchedule = objEvent.ActivitySchedule
                objHistory.ApprovedCost = objEvent.ApproveCost
                objHistory.EventName = objEvent.EventParameter.EventName
                objHistory.EventProposal = objEvent
                objHistory.ProposedCost = objEventView.TotalCost
                Dim objUserInfo As UserInfo = New SessionHelper().GetSession("LOGINUSERINFO")
                objHistory.UpdateBy = String.Format("{0}-{1}", objUserInfo.Dealer.DealerCode, objUserInfo.UserName)
                If objFacade.Update(objEvent, objHistory) = 2 Then
                    MessageBox.Show(SR.SaveSuccess)
                Else
                    MessageBox.Show(SR.SaveFail)
                End If
                dtgEvent.EditItemIndex = -1
            Case "Cancel"
                dtgEvent.EditItemIndex = -1
            Case "DeleteFile"
                IsRemovedFile = True
        End Select
        BindGrid()
    End Sub
    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Dim objFacade As New EventProposalFacade(User)
        Dim counter As Int32 = 0
        For Each items As DataGridItem In dtgEvent.Items
            Dim chkSelect As CheckBox = items.FindControl("chkSelect")
            If Not IsNothing(chkSelect) AndAlso chkSelect.Checked Then
                counter = counter + 1
                Dim objEvent As EventProposal = objFacade.Retrieve(CInt(items.Cells(0).Text))
                objEvent.EventAgreementStatus = EnumEventAgreementStatus.EventAgreementStatus.Validasi
                If objFacade.Update(objEvent) <> 1 Then
                    MessageBox.Show(SR.SaveFail)
                End If
            End If
        Next
        If counter = 0 Then
            MessageBox.Show("Tandai terlebih dahulu yang mau di validasi")
        Else
            BindGrid()
        End If
    End Sub
    Private Sub btnExcelDwl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelDwl.Click
        Dim idColl As New ArrayList
        For Each items As DataGridItem In dtgEvent.Items
            Dim chkSelect As CheckBox = items.FindControl("chkSelect")
            If Not IsNothing(chkSelect) AndAlso chkSelect.Checked Then
                idColl.Add(items.Cells(0).Text)
            End If
        Next
        If idColl.Count > 0 Then
            Dim strBui As New StringBuilder("(")
            For Each id As String In idColl
                If strBui.Length > 1 Then
                    strBui.Append(",")
                End If
                strBui.Append(id)
            Next
            strBui.Append(")")
            If ddlNamaKegiatan.SelectedValue <> -1 Then
                Response.Redirect(String.Format("FrmEventProposalExcelDownload.aspx?mode=Agreement&idin={0}&NameKegiatan={1}", _
                    strBui.ToString, ddlNamaKegiatan.SelectedItem.Text))
            Else
                Response.Redirect(String.Format("FrmEventProposalExcelDownload.aspx?mode=Agreement&idin={0}", _
                    strBui.ToString))
            End If
        Else
            MessageBox.Show("Tandai data yang ingin di download")
        End If
    End Sub
    Private Sub dtgEvent_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEvent.PageIndexChanged
        dtgEvent.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub
#End Region

End Class