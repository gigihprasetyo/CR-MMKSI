Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain

Public Class FrmPopUpSalesDeliveryVechileDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgSalesDelieveryVechile As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack() Then
            BindResult()
        End If
    End Sub
    Private Sub BindResult()
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(DeliveryCustomerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DeliveryCustomerDetail), "DeliveryCustomerHeader.ID", MatchType.Exact, Request.Params("ID")))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DeliveryCustomerDetail), "ChassisMaster.ChassisNumber", Sort.SortDirection.ASC))

        Dim arrList As ArrayList = New DeliveryCustomerDetailFacade(User).Retrieve(criterias, sortColl)
        If arrList.Count > 0 Then
            dgSalesDelieveryVechile.DataSource = arrList
            dgSalesDelieveryVechile.VirtualItemCount = arrList.Count
        Else
            dgSalesDelieveryVechile.DataSource = New ArrayList
        End If
        dgSalesDelieveryVechile.DataBind()
    End Sub

    Private Sub dgSalesDelieveryVechile_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesDelieveryVechile.ItemDataBound
        If e.Item.ItemIndex >= 0 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1) + (dgSalesDelieveryVechile.CurrentPageIndex * dgSalesDelieveryVechile.PageSize)


        End If

    End Sub
End Class

