#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility

#End Region

Public Class PopUpStockMP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMPStock As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim sHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If indexPage >= 0 Then
            Dim id As Integer = Request.QueryString("id")
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionStockAdjustment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MaterialPromotionStockAdjustment), "MaterialPromotion.ID", MatchType.Exact, id))

            Dim arlMPStock As ArrayList = New MaterialPromotionStockAdjustmentFacade(User).RetrieveActiveList(indexPage + 1, dtgMPStock.PageSize, totalRow, viewstate("SortColPopUp"), viewstate("SortDirectionPopUp"), criterias)
            dtgMPStock.DataSource = arlMPStock
            dtgMPStock.VirtualItemCount = totalRow
            If indexPage = 0 Then
                dtgMPStock.CurrentPageIndex = 0
            End If

            dtgMPStock.DataBind()
        End If

    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            viewstate.Add("SortColPopUp", "CreatedTime")
            viewstate.Add("SortDirectionPopUp", Sort.SortDirection.ASC)
            BindToGrid(0)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub

    Private Sub dtgMPStock_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMPStock.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblAdjustment As Label = CType(e.Item.FindControl("lblAdjustment"), Label)
            lblNo.Text = e.Item.ItemIndex + 1
            If lblAdjustment.Text = 1 Then
                lblAdjustment.Text = "In"
            Else
                lblAdjustment.Text = "Out"
            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            Dim id As Integer = Request.QueryString("id")
            Dim objMPMaster As KTB.DNet.Domain.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(id)
            Dim lblStock As Label = CType(e.Item.FindControl("lblStockAkhir"), Label)
            lblStock.Text = objMPMaster.Stock.ToString("#,##0")
        End If
    End Sub

    Private Sub dtgMPStock_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMPStock.PageIndexChanged
        dtgMPStock.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgMPStock.CurrentPageIndex)
    End Sub

    Private Sub dtgMPStock_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMPStock.SortCommand
        If e.SortExpression = viewstate("SortColPopUp") Then
            If viewstate("SortDirectionPopUp") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirectionPopUp", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirectionPopUp", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColPopUp", e.SortExpression)
        BindToGrid(0)
    End Sub
End Class
