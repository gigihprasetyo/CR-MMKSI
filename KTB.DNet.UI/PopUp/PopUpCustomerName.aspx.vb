#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
#End Region

Public Class PopUpCustomerName
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtCustomerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustomerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgCustomerName As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSearch2 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "PrivateVariables"
    Private sessHelper As New SessionHelper
#End Region


    Private Sub ClearData()
        Me.txtCustomerCode.Text = String.Empty
        Me.txtCustomerName.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "CustomerCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        'dtgCustomerName.DataSource = New CustomerDealerFacade(User).RetrieveActiveList(indexPage + 1, _
        '    dtgCustomerName.PageSize, totalRow)
        dtgCustomerName.DataSource = New CustomerDealerFacade(User).RetrieveByCriteria(CriteriaSearch(), indexPage + 1, _
            dtgCustomerName.PageSize, totalRow)
        dtgCustomerName.VirtualItemCount = totalRow
        dtgCustomerName.DataBind()
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim _dealerCode As String = objuser.Dealer.DealerCode

        Dim criterias As New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerDealer), "Dealer.DealerCode", MatchType.Exact, _dealerCode))
        If txtCustomerCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.Code", MatchType.Exact, txtCustomerCode.Text))
        End If
        If txtCustomerName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.Name1", MatchType.Exact, txtCustomerName.Text))
        End If
        Return criterias
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
        End If
    End Sub

    'Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    BindDataGrid(0)
    '    If dtgCustomerName.Items.Count > 0 Then
    '        btnChoose.Disabled = False
    '    Else
    '        btnChoose.Disabled = True
    '        MessageBox.Show("Data tidak ditemukan")
    '    End If
    'End Sub

    Private Sub dtgCustomerName_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCustomerName.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        'If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
        '    Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
        '    e.Item.Cells(0).Controls.Add(rdbChoice)
        'End If

    End Sub

    Private Sub dtgCustomerName_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCustomerName.PageIndexChanged
        dtgCustomerName.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgCustomerName.CurrentPageIndex)
    End Sub

    Private Sub dtgCustomerName_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCustomerName.SortCommand
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
        dtgCustomerName.CurrentPageIndex = 0
        BindDataGrid(dtgCustomerName.CurrentPageIndex)
    End Sub

    Private Sub btnSearch2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch2.Click
        BindDataGrid(0)
        If dtgCustomerName.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub
End Class
