Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Public Class FrmSAPTargetofSales
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dgSalesTarget As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlPosisi As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private objFacade As New JobPositionFacade(User)
    Private objDomain As New JobPosition
    Private _list As New ArrayList
    Private _sessHelper As New SessionHelper
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack() Then
            ViewState("CurrentSortColumn") = "Code"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            _sessHelper.SetSession("BINDSTATUS", "NOTHING")
            BindDDL()
            BindDetail(0)
        End If
    End Sub
    Private Sub BindDetail(ByVal indexpage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlPosisi.SelectedValue <> 0 Then
            criterias.opAnd(New Criteria(GetType(JobPosition), "ID", MatchType.Exact, ddlPosisi.SelectedValue))
        End If
        _list = objFacade.RetrieveByCriteria(criterias, indexpage + 1, dgSalesTarget.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        _sessHelper.SetSession("SortViewVC", criterias)
        dgSalesTarget.DataSource = _list
        dgSalesTarget.VirtualItemCount = totalRow
        dgSalesTarget.DataBind()
    End Sub
    Private Sub BindDDL()
        _list = objFacade.RetrieveActiveList()
        For Each item As JobPosition In _list
            Dim _listItem As New ListItem(item.Description, item.ID)
            ddlPosisi.Items.Add(_listItem)
        Next
        Dim _listItemInvalid As New ListItem("Pilih Posisi", 0)
        _listItemInvalid.Selected = True
        ddlPosisi.Items.Add(_listItemInvalid)
    End Sub
    Private Sub dgSalesTarget_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesTarget.ItemCommand
        Select Case e.CommandName
            Case "Update"
                Dim txtTarget As TextBox = CType(e.Item.FindControl("txtEditTarget"), TextBox)
                objDomain = objFacade.Retrieve(Integer.Parse(e.CommandArgument))
                objDomain.SalesTarget = txtTarget.Text
                Dim n As Integer = objFacade.Update(objDomain)
                If n <> -1 Then
                    MessageBox.Show("Data berhasil di-update")
                    dgSalesTarget.EditItemIndex = -1
                    BindDetail(dgSalesTarget.CurrentPageIndex)
                End If
            Case "Edit"
                dgSalesTarget.EditItemIndex = e.Item.ItemIndex
                BindDetail(0)
            Case "Cancel"
                dgSalesTarget.EditItemIndex = -1
                BindDetail(0)
        End Select
    End Sub
    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dgSalesTarget.CurrentPageIndex = 0
        BindDetail(dgSalesTarget.CurrentPageIndex)
    End Sub

    Private Sub dgSalesTarget_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesTarget.PageIndexChanged
        dgSalesTarget.CurrentPageIndex = e.NewPageIndex
        BindDetail(dgSalesTarget.CurrentPageIndex)
    End Sub

    Private Sub dgSalesTarget_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesTarget.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgSalesTarget.SelectedIndex = -1
        dgSalesTarget.CurrentPageIndex = 0
        bindGridSorting(dgSalesTarget.CurrentPageIndex)
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dgSalesTarget.DataSource = objFacade.RetrieveByCriteria(CType(_sessHelper.GetSession("SortViewVC"), CriteriaComposite), indexPage + 1, dgSalesTarget.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgSalesTarget.VirtualItemCount = totalRow
            dgSalesTarget.DataBind()
        End If

    End Sub

    
End Class
