Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class PopUpDealerBankAccount
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgDealerBankAccount As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    'Private objDealerBankAccount As DealerBankAccount = New DealerBankAccount
    'Private objDealerBankAccountFacade As DealerBankAccountFacade = New DealerBankAccountFacade(User)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("currentSortColumn") = "BankAccount"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            BindDataGrid(0)
        End If
    End Sub

    Public Sub BindDataGrid(ByVal IndexPage As Integer)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DealerBankAccount), "Dealer.ID", MatchType.Exact, CInt(Request.QueryString("DealerID"))))
        _arrList = New DealerBankAccountFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgDealerBankAccount.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        dgDealerBankAccount.DataSource = _arrList
        dgDealerBankAccount.VirtualItemCount = totalRow
        dgDealerBankAccount.DataBind()
    End Sub

    Private Sub dgDealerBankAccount_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDealerBankAccount.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgDealerBankAccount.CurrentPageIndex * dgDealerBankAccount.PageSize)
            End If
        End If
    End Sub

    Private Sub dgDealerBankAccount_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDealerBankAccount.PageIndexChanged
        dgDealerBankAccount.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgDealerBankAccount.CurrentPageIndex)
    End Sub

    Private Sub dgDealerBankAccount_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDealerBankAccount.SortCommand
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

        dgDealerBankAccount.SelectedIndex = -1
        dgDealerBankAccount.CurrentPageIndex = 0
        BindDataGrid(dgDealerBankAccount.CurrentPageIndex)
    End Sub
End Class
