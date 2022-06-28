#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility

#End Region

Public Class PopUpDetailInOutMP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgMPStock As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button

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
    Private Sub BindToGrid()
        Dim id As Integer = Request.QueryString("id")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionStockAdjustment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionStockAdjustment), "ID", MatchType.Exact, id))

        Dim arlMPStock As ArrayList = New MaterialPromotionStockAdjustmentFacade(User).Retrieve(criterias)
        If arlMPStock.Count > 0 Then
            dtgMPStock.DataSource = arlMPStock
        Else
            dtgMPStock.DataSource = New ArrayList
        End If

        dtgMPStock.DataBind()
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            BindToGrid()
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
            Dim id As Integer = Request.QueryString("idheader")
            Dim objMPMaster As KTB.DNet.Domain.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(id)
            Dim lblStock As Label = CType(e.Item.FindControl("lblStockAkhir"), Label)
            lblStock.Text = objMPMaster.Stock
        End If
    End Sub

End Class
