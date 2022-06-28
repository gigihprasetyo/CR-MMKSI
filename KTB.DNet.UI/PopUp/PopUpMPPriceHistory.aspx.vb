#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class PopUpMPPriceHistory
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnTutup As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPriceHistory As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalrow As Integer = 0
        If indexPage >= 0 Then
            Dim id As Integer = Request.QueryString("id")
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionPriceHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MaterialPromotionPriceHistory), "MaterialPromotion.ID", MatchType.Exact, id))
            Dim arlMPPrice As ArrayList = New MaterialPromotionPriceHistoryFacade(User).RetrieveActiveList(indexPage + 1, dtgPriceHistory.PageSize, totalrow, viewstate("SortColumn"), viewstate("SortDirection"), criterias)

            dtgPriceHistory.DataSource = arlMPPrice
            dtgPriceHistory.VirtualItemCount = totalrow

            If indexPage = 0 Then
                dtgPriceHistory.CurrentPageIndex = 0
            End If
            dtgPriceHistory.DataBind()
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            viewstate.Add("SortColumn", "LastUpdateTime")
            viewstate.Add("SortDirection", Sort.SortDirection.ASC)
            BindToGrid(0)
        End If
    End Sub

    Private Sub dtgPriceHistory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPriceHistory.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1)
        End If
    End Sub

    Private Sub btnTutup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTutup.Click
        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub

    Private Sub dtgPriceHistory_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPriceHistory.SortCommand
        If e.SortExpression = viewstate("SortColumn") Then
            If viewstate("SortDirection") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirection", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColumn", e.SortExpression)
        BindToGrid(0)
    End Sub

    Private Sub dtgPriceHistory_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPriceHistory.PageIndexChanged
        dtgPriceHistory.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgPriceHistory.CurrentPageIndex)
    End Sub
End Class
