#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.Utility

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
#End Region

Public Class PopUpUniformRecap
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblGrandTotal As System.Web.UI.WebControls.Label
    Protected WithEvents dtgUniform As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private SessHelper As SessionHelper = New SessionHelper
    Private arlUniform As ArrayList = New ArrayList

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here  
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            Bindata()
        End If

    End Sub
    Private Sub Bindata()

        Dim objEnumUniformSize As EnumUniformSize = New EnumUniformSize
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

        Dim totalrow As Integer = 0
        arlUniform = New SalesmanUniformFacade(User).RetrieveByCriteria(criterias, 1, dtgUniform.PageSize, totalrow, "SalesmanUniformCode", Sort.SortDirection.ASC)

        For Each itemUniform As SalesmanUniform In arlUniform
            itemUniform.SRecap = 0
            itemUniform.LRecap = 0
            itemUniform.MRecap = 0
            itemUniform.XLRecap = 0
            itemUniform.XXLRecap = 0

            For Each objOrder As SalesmanUniformOrderDetail In CType(SessHelper.GetSession("arlOrder"), ArrayList)
                If Not IsNothing(objOrder.UniformSize) Then

                    If itemUniform.ID = objOrder.SalesmanUniform.ID Then
                        Select Case objOrder.UniformSize
                            Case objEnumUniformSize.UniformSize.S
                                itemUniform.SRecap += 1
                            Case objEnumUniformSize.UniformSize.M
                                itemUniform.MRecap += 1
                            Case objEnumUniformSize.UniformSize.L
                                itemUniform.LRecap += 1
                            Case objEnumUniformSize.UniformSize.XL
                                itemUniform.XLRecap += 1
                            Case objEnumUniformSize.UniformSize.XXL
                                itemUniform.XXLRecap += 1
                        End Select
                    End If

                End If
            Next
        Next

        'Count Total
        Dim SubTotal As Double = 0
        For Each itemUniform As SalesmanUniform In arlUniform
            SubTotal += (itemUniform.SRecap + itemUniform.MRecap + itemUniform.LRecap + itemUniform.XLRecap + itemUniform.XXLRecap) * itemUniform.DealerPrice
        Next

        lblGrandTotal.Text() = SubTotal.ToString
        dtgUniform.VirtualItemCount = totalrow
        dtgUniform.DataSource = arlUniform
        dtgUniform.DataBind()

    End Sub

    Private Sub dtgUniform_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUniform.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanUniform As SalesmanUniform = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgUniform.CurrentPageIndex * dtgUniform.PageSize)

            Dim lblSalesmanUnifDistributionCodeNew As Label = CType(e.Item.FindControl("lblSalesmanUnifDistributionCode"), Label)
            lblSalesmanUnifDistributionCodeNew.Text = objSalesmanUniform.SalesmanUnifDistribution.SalesmanUnifDistributionCode

            Dim lblDescriptionNew As Label = CType(e.Item.FindControl("lblDescription"), Label)
            lblDescriptionNew.Text = objSalesmanUniform.SalesmanUnifDistribution.Description

            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                Dim objuniform As SalesmanUniform = arlUniform(e.Item.ItemIndex)
                Dim lblUnitPrice As Label = e.Item.FindControl("lblUnitPrice")
                Dim lblPrice As Label = e.Item.FindControl("lblPrice")
                lblUnitPrice.Text = objuniform.DealerPrice.ToString("#,##0")
                Dim subtotal As Double = 0
                subtotal = (objuniform.SRecap + objuniform.MRecap + objuniform.LRecap + objuniform.XLRecap + objuniform.XXLRecap) * objuniform.DealerPrice
                lblPrice.Text = subtotal.ToString("#,##0")
            End If
        End If
    End Sub
End Class
