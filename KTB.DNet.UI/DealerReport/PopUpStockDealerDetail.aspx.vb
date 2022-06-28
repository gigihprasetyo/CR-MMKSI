Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search

Public Class PopUpStockDealerDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblStockPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents lblColor As System.Web.UI.WebControls.Label
    Protected WithEvents dgDetails As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Sub BindData(ByVal ID As String, ByVal VCID As String)
        Dim arl As New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "DealerStockReportHeader.ID", MatchType.Exact, ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "VechileColor.ID", MatchType.Exact, VCID))
        arl = New DealerReport.DealerStockReportDetailFacade(User).Retrieve(criterias)
        Dim oD As DealerStockReportDetail = CType(arl(0), DealerStockReportDetail)
        lblDealer.Text = oD.DealerStockReportHeader.Dealer.DealerCode & " / " & oD.DealerStockReportHeader.Dealer.DealerName
        Dim dt As New Date(2000, oD.DealerStockReportHeader.PeriodMonth, 1)
        lblStockPeriode.Text = dt.ToString("MMM") & " " & oD.DealerStockReportHeader.PeriodYear
        lblColor.Text = oD.VechileColor.MaterialNumber & " - " & oD.VechileColor.MaterialDescription
        dgDetails.DataSource = arl
        dgDetails.DataBind()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BindData(Request.QueryString("ID"), Request.QueryString("VCID"))
    End Sub

    Private Sub dgDetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDetails.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
        End If
    End Sub
End Class
