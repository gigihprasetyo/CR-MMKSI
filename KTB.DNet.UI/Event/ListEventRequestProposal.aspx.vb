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

Public Class ListEventRequestProposal
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
    Protected WithEvents calDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlModel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlNamaKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEvent As System.Web.UI.WebControls.DataGrid
    Protected WithEvents tblCategoryModelType As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents btnValidate As System.Web.UI.WebControls.Button
    Protected WithEvents btnExcel As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelValidate As System.Web.UI.WebControls.Button

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
    Private Sub InitAuthorization()
        Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")
        If Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            txtDealerCode.Text = objUserInfo.Dealer.DealerCode
            txtDealerCode.Enabled = False
            lblSearchDealer.Enabled = True
            dtgEvent.Columns(3).Visible = False
            dtgEvent.Columns(4).Visible = False
            lblSearchDealer.Visible = False
        Else
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            btnValidate.Visible = False
            btnCancelValidate.Visible = False
        End If
    End Sub
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
            ddlNamaKegiatan.Items.Insert(0, "Pilih Jenis Kegiatan")
        End If
    End Sub
    Private Function BuiltCriteria() As CriteriaComposite
        Dim objComposite As New CriteriaComposite(New Criteria(GetType(EventProposal), "RowStatus", _
            CType(DBRowStatus.Active, Short)))
        Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")

        'If Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
        '    objComposite.opAnd(New Criteria(GetType(EventProposal), "EventProposalStatus", _
        '        CType(EnumEventProposalStatus.EventProposalStatus.Baru, Byte)))
        'Else
        '    objComposite.opAnd(New Criteria(GetType(EventProposal), "EventProposalStatus", _
        '        CType(EnumEventProposalStatus.EventProposalStatus.Validasi, Byte)))
        'End If

        If txtDealerCode.Text.Length > 0 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "Dealer.DealerCode", MatchType.InSet, _
                String.Format("('{0}')", txtDealerCode.Text.Replace(";", "','"))))
        End If
        If ddlSalesmanArea.SelectedValue <> -1 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.SalesmanArea", _
                ddlSalesmanArea.SelectedValue))
        End If
        If cbPeriode.Checked Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventDateStart", _
                MatchType.GreaterOrEqual, calDari.Value))
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventDateEnd", _
                MatchType.LesserOrEqual, calSampai.Value))
        End If
        If ddlJenisKegiatan.SelectedValue <> -1 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.ActivityType", _
                ddlJenisKegiatan.SelectedValue))
        End If
        If ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Small_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Launching_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Exhibition_Khusus, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Showroom_Event, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Others, Integer) Then
            If ddlCategory.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.Category", ddlCategory.SelectedValue))
            End If
            If ddlModel.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.VechileType.VechileModel", _
                    ddlModel.SelectedValue))
            End If
            If ddlType.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.VechileType", _
                    ddlType.SelectedValue))
            End If
        End If
        If ddlNamaKegiatan.SelectedIndex > 0 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventName", _
                ddlNamaKegiatan.SelectedItem.Text))
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventYear", _
                ddlYear.SelectedValue))
        End If
        Return objComposite
    End Function
    Private Sub BindGrid()
        Dim objFacade As New EventProposalFacade(User)
        Dim totalRows As Integer = 0
        dtgEvent.DataSource = objFacade.RetrieveActiveList(StoreCriteria, dtgEvent.CurrentPageIndex + 1, _
            dtgEvent.PageSize, totalRows, GridSortColumn, GridSortDirection)
        dtgEvent.VirtualItemCount = totalRows
        dtgEvent.DataBind()
        If totalRows = 0 Then
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            InitAuthorization()
            FillSalesmanArea()
            FillJenisKegiatan()
            FillNamaKegiatan()
            ShowModelCategoryType(False)
            FillCategory()
            ddlCategory_SelectedIndexChanged(Nothing, Nothing)
            ddlModel_SelectedIndexChanged(Nothing, Nothing)
            FillYear()
        End If
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
        BindGrid()
    End Sub
    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgEvent.CurrentPageIndex = 0
        StoreCriteria = BuiltCriteria()
        BindGrid()
    End Sub
    Private Sub cbPeriode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPeriode.CheckedChanged
        calDari.Enabled = cbPeriode.Checked
        calSampai.Enabled = cbPeriode.Checked
    End Sub
    Private Sub dtgEvent_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEvent.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgEvent.PageSize * dtgEvent.CurrentPageIndex)).ToString
            Dim lnkPenyelesaian As HyperLink = e.Item.FindControl("lnkPenyelesaian")
            If Not IsNothing(lnkPenyelesaian) Then
                Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")
                lnkPenyelesaian.Visible = Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB
            End If
        End If
    End Sub
    Private Sub dtgEvent_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEvent.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                dtgEvent.EditItemIndex = e.Item.ItemIndex
            Case "Update"
                Dim objFacade As New EventProposalFacade(User)
                Dim objItem As EventProposal = objFacade.Retrieve(CInt(e.CommandArgument))
                Dim objHistory As New EventProposalHistory
                Dim objHistoryFacade As New EventProposalHistoryFacade(User)
                objHistory.ActivityPlaceOld = objItem.ActivityPlace
                objItem.ActivityPlace = CType(e.Item.FindControl("txtActivityPlace"), TextBox).Text
                objHistory.ActivityPlaceNew = objItem.ActivityPlace
                objHistory.ActivityScheduleOld = objItem.ActivitySchedule
                objItem.ActivitySchedule = CType(e.Item.FindControl("calActivitySchedule"), IntiCalendar).Value
                objHistory.ActivityScheduleNew = objItem.ActivitySchedule
                objItem.Comment = CType(e.Item.FindControl("txtComment"), TextBox).Text
                Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")
                objHistory.UpdateBy = String.Format("{0}-{1}", objUserInfo.Dealer.DealerCode, objUserInfo.UserName)
                objHistory.EventProposal = objItem
                If objFacade.Update(objItem) = 1 Then
                    objHistoryFacade.Insert(objHistory)
                    MessageBox.Show(SR.SaveSuccess)
                    dtgEvent.EditItemIndex = -1
                Else
                    MessageBox.Show(SR.SaveFail)
                End If
            Case "Cancel"
                dtgEvent.EditItemIndex = -1
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
                objEvent.EventProposalStatus = EnumEventProposalStatus.EventProposalStatus.Validasi
                If objFacade.Update(objEvent) <> 1 Then
                    MessageBox.Show(SR.SaveFail)
                End If
            End If
        Next
        If counter = 0 Then
            MessageBox.Show(SR.DataNotChooseYet("Proposal yang mau di validasi"))
        Else
            MessageBox.Show(SR.ValidateSucces)
            BindGrid()
        End If
    End Sub
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim idColl As New ArrayList
        For Each items As DataGridItem In dtgEvent.Items
            Dim chkSelect As CheckBox = items.FindControl("chkSelect")
            If Not IsNothing(chkSelect) AndAlso chkSelect.Checked Then
                idColl.Add(items.Cells(0).Text)
            End If
        Next
        If idColl.Count > 0 Then
            Dim idBuilt As New StringBuilder("(")
            For Each id As String In idColl
                If idBuilt.Length > 1 Then
                    idBuilt.Append(",")
                End If
                idBuilt.Append(id)
            Next
            idBuilt.Append(")")
            Response.Redirect(String.Format("FrmEventProposalExcelDownload.aspx?Mode=Excel&idin={0}", _
                idBuilt.ToString))
        Else
            MessageBox.Show("Tandai data yang ingin di download")
        End If
    End Sub
    Private Sub dtgEvent_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEvent.PageIndexChanged
        dtgEvent.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub
    Private Sub btnCancelValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelValidate.Click
        Dim objFacade As New EventProposalFacade(User)
        Dim counter As Int32 = 0
        For Each items As DataGridItem In dtgEvent.Items
            Dim chkSelect As CheckBox = items.FindControl("chkSelect")
            If Not IsNothing(chkSelect) AndAlso chkSelect.Checked Then
                counter = counter + 1
                Dim objEvent As EventProposal = objFacade.Retrieve(CInt(items.Cells(0).Text))
                objEvent.EventProposalStatus = EnumEventProposalStatus.EventProposalStatus.Baru
                If objFacade.Update(objEvent) <> 1 Then
                    MessageBox.Show(SR.SaveFail)
                End If
            End If
        Next
        If counter = 0 Then
            MessageBox.Show("Proposal yang mau batal validasi belum dipilih")
        Else
            MessageBox.Show("Pembatalan validasi proposal berhasil")
            BindGrid()
        End If
    End Sub
#End Region

End Class