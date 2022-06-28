Imports Ktb.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text

Public Class FrmLeasingFeeList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlVehicleCAtegory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlVehicleType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtmFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dtmTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkPeriode As System.Web.UI.WebControls.CheckBox
    Protected WithEvents dtgLeasingFee As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private shelp As SessionHelper = New SessionHelper
    Private objDealer As Dealer
    Private Const SDEALER As String = "DEALER"
    Private Const CRITS As String = "FrmLeasingFeeListCriteria"

    Private Sub BindVehicleCategory()
        Dim vmf As KTB.DNet.BusinessFacade.FinishUnit.VechileModelFacade = New KTB.DNet.BusinessFacade.FinishUnit.VechileModelFacade(User)
        Dim arl As ArrayList = vmf.RetrieveList("Description", Sort.SortDirection.ASC)
        ddlVehicleCAtegory.DataValueField = "ID"
        ddlVehicleCAtegory.DataTextField = "Description"
        ddlVehicleCAtegory.DataSource = arl
        ddlVehicleCAtegory.DataBind()
        ddlVehicleCAtegory.Items.Insert(0, New ListItem("All Variant", "0"))
    End Sub

    Private Sub BindVehicleType()
        ddlVehicleType.DataTextField = "Description"
        ddlVehicleType.DataValueField = "ID"
        ddlVehicleType.DataSource = New KTB.DNet.BusinessFacade.FinishUnit.VechileTypeFacade(User).RetrieveByVehicleModelId(CInt(ddlVehicleCAtegory.SelectedValue), "Description", Sort.SortDirection.ASC)
        ddlVehicleType.DataBind()
        ddlVehicleType.Items.Insert(0, New ListItem("All Type", "0"))
    End Sub

    Private Sub createCriteria()
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingFee), "RowStatus", MatchType.Exact, CInt(DBRowStatus.Active)))
        If (ddlVehicleType.SelectedValue <> "" And ddlVehicleType.SelectedValue <> "0") Then
            crit.opAnd(New Criteria(GetType(LeasingFee), "VechileType", MatchType.Exact, CInt(ddlVehicleType.SelectedValue)))
        Else
            If (ddlVehicleCAtegory.SelectedValue <> "" And ddlVehicleCAtegory.SelectedValue <> "0") Then
                crit.opAnd(New Criteria(GetType(LeasingFee), "VechileType.VechileModel", MatchType.Exact, CInt(ddlVehicleCAtegory.SelectedValue)))
            End If
        End If
        If (chkPeriode.Checked) Then
            crit.opAnd(New Criteria(GetType(LeasingFee), "DateFrom", MatchType.GreaterOrEqual, dtmFrom.Value))
            crit.opAnd(New Criteria(GetType(LeasingFee), "DateTo", MatchType.LesserOrEqual, dtmTo.Value))
        End If
        shelp.SetSession(CRITS, crit)
    End Sub

    Private Sub bindGrid()
        Dim crit As CriteriaComposite = CType(shelp.GetSession(CRITS), CriteriaComposite)
        Dim facade As LeasingFeeFacade = New LeasingFeeFacade(User)
        Dim totalrows As Integer = 0
        Dim arl As ArrayList = facade.RetrieveByCriteria(crit, dtgLeasingFee.CurrentPageIndex + 1, dtgLeasingFee.PageSize, totalrows, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
        dtgLeasingFee.DataSource = arl
        dtgLeasingFee.VirtualItemCount = totalrows
        dtgLeasingFee.DataBind()
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.General_manual_doc_privilege) Then
            'Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Indent Part Equipment - Daftar Status PO")
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        objDealer = CType(shelp.GetSession(SDEALER), Dealer)

        If IsPostBack Then Return

        BindVehicleCategory()

        If (Not IsNothing(Request.QueryString("isback"))) Then
            bindGrid()
        End If
    End Sub

    Private Sub ddlVehicleCAtegory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlVehicleCAtegory.SelectedIndexChanged
        If (ddlVehicleCAtegory.SelectedValue = "0") Then
            Return
        End If
        BindVehicleType()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        createCriteria()
        bindGrid()
    End Sub

    Private Sub dtgLeasingFee_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgLeasingFee.PageIndexChanged
        dtgLeasingFee.CurrentPageIndex = e.NewPageIndex
        bindGrid()
    End Sub

    Private Sub dtgLeasingFee_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgLeasingFee.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        bindGrid()
    End Sub

    Private Sub dtgLeasingFee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLeasingFee.ItemDataBound
        If e.Item.ItemIndex = -1 Then Return

        e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgLeasingFee.PageSize * dtgLeasingFee.CurrentPageIndex)).ToString

        Dim lnkDel As LinkButton = CType(e.Item.FindControl("lnkDel"), LinkButton)
        lnkDel.Attributes.Add("onclick", "return confirm('Yakin mau hapus ?')")

    End Sub

    Private Sub dtgLeasingFee_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgLeasingFee.ItemCommand
        If (e.CommandName = "Del") Then
            Dim lff As KTB.DNet.BusinessFacade.General.LeasingFeeFacade = New KTB.DNet.BusinessFacade.General.LeasingFeeFacade(User)
            Dim obj As LeasingFee = lff.Retrieve(CInt(e.CommandArgument))
            lff.Delete(obj)
            MessageBox.Show("Data sudah dihapus")
            bindGrid()
        End If
    End Sub
End Class
