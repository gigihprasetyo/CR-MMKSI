Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Salesman


Public Class PopUpSalesmanUniformOrderSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgOrderNoSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNoOrder As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Protected countChk As Integer = 0
    Private objDealer As Dealer
    Private criterias As CriteriaComposite

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("DealerCode")) Then
                txtDealerCode.Text = Request.QueryString("DealerCode")
                txtDealerCode.ReadOnly = True
            End If
            Initialize()
            BindGrid(0)
            If dtgOrderNoSelection.Items.Count > 0 Then
                btnChoose.Disabled = False
            Else
                btnChoose.Disabled = True
            End If
        End If
    End Sub

    Private Sub Initialize()
        ViewState("SortCol") = "OrderNumber"
        ViewState("SortDir") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub ClearData()
        'txtDealerName.Text = String.Empty
        txtNoOrder.Text = String.Empty
    End Sub

    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not IsNothing(Request.QueryString("DealerCode")) Then
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "Dealer.DealerCode", MatchType.InSet, CommonFunction.GetStrValue(Request.QueryString("DealerCode"), ";", ",")))
        End If
        If txtNoOrder.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "OrderNumber", MatchType.[Partial], txtNoOrder.Text))
        End If
    End Sub

    Public Sub BindGrid(ByVal idxPage As Integer)
        If idxPage >= 0 Then
            CreateCriteria()

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(CType(ViewState("SortCol"), String))) And (Not IsNothing(CType(ViewState("SortCol"), String))) Then
                sortColl.Add(New Sort(GetType(SalesmanUniformOrderHeader), "OrderNumber", CType(ViewState("SortDir"), Sort.SortDirection)))
            Else
                sortColl = Nothing
            End If

            dtgOrderNoSelection.DataSource = New SalesmanUniformOrderHeaderFacade(User).Retrieve(criterias, sortColl)
            dtgOrderNoSelection.DataBind()
        End If
    End Sub

    Private Sub dtgOrderNoSelection_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgOrderNoSelection.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanUniformOrderHeader As SalesmanUniformOrderHeader = e.Item.DataItem
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgOrderNoSelection.CurrentPageIndex = 0
        BindGrid(dtgOrderNoSelection.CurrentPageIndex)

        If dtgOrderNoSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgOrderNoSelection_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgOrderNoSelection.SortCommand
        If e.SortExpression = CType(viewstate("SortCol"), String) Then
            If viewstate("SortDir") = Sort.SortDirection.ASC Then
                ViewState("SortDir") = Sort.SortDirection.DESC
            Else
                ViewState("SortDir") = Sort.SortDirection.ASC
            End If
        Else
            viewstate("SortCol") = e.SortExpression
            viewstate("SortDir") = Sort.SortDirection.ASC
        End If
        dtgOrderNoSelection.SelectedIndex = -1
        BindGrid(dtgOrderNoSelection.CurrentPageIndex)

    End Sub

    Private Sub dtgOrderNoSelection_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgOrderNoSelection.PageIndexChanged
        dtgOrderNoSelection.CurrentPageIndex = e.NewPageIndex
        BindGrid(dtgOrderNoSelection.CurrentPageIndex)
    End Sub

End Class
