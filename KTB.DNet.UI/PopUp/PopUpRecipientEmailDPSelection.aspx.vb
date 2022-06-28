#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade

#End Region

Public Class PopUpRecipientEmailDPSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtRecipientName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecipientPosition As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgEmailUser As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private strDealerCode As String = String.Empty
    Private strDiscountProposalHeaderID As String = String.Empty

    Private Sub ClearData()
        Me.txtRecipientName.Text = String.Empty
        Me.txtRecipientPosition.Text = String.Empty
        Me.txtEmail.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "RecipientName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
        btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        Dim arlEmailUser As ArrayList = New ArrayList
        arlEmailUser = New DiscountProposalEmailUserFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, _
            dtgEmailUser.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgEmailUser.DataSource = arlEmailUser
        dtgEmailUser.VirtualItemCount = totalRow
        dtgEmailUser.DataBind()
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim strSQL As String = ""
        Dim criterias As New CriteriaComposite(New Criteria(GetType(DiscountProposalEmailUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If strDiscountProposalHeaderID.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(DiscountProposalEmailUser), "DiscountProposalHeader.ID", MatchType.No, strDiscountProposalHeaderID.Trim))
        End If

        If strDealerCode <> "" Then
            criterias.opAnd(New Criteria(GetType(DiscountProposalEmailUser), "DiscountProposalHeader.Dealer.DealerCode", MatchType.Exact, strDealerCode))
        End If
        If txtRecipientName.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(DiscountProposalEmailUser), "RecipientName", MatchType.[Partial], txtRecipientName.Text.Trim))
        End If
        If txtRecipientPosition.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(DiscountProposalEmailUser), "RecipientPosition", MatchType.[Partial], txtRecipientPosition.Text.Trim))
        End If
        If txtEmail.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(DiscountProposalEmailUser), "Email", MatchType.[Partial], txtEmail.Text.Trim))
        End If

        Return criterias
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            strDealerCode = Request.QueryString("DealerCode")
            strDiscountProposalHeaderID = Request.QueryString("DiscountProposalHeaderID")
        Catch
        End Try
        If Not Page.IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
        If dtgEmailUser.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgEmailUser_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEmailUser.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)

            Dim lblNoGridRow As Label = CType(e.Item.FindControl("lblNoGridRow"), Label)
            lblNoGridRow.Text = (e.Item.ItemIndex + 1 + (dtgEmailUser.CurrentPageIndex * dtgEmailUser.PageSize)).ToString

            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim lblRecipientName As Label = CType(e.Item.FindControl("lblRecipientName"), Label)
            Dim lblRecipientPosition As Label = CType(e.Item.FindControl("lblRecipientPosition"), Label)
            Dim lblEmail As Label = CType(e.Item.FindControl("lblEmail"), Label)

            Dim objDiscountProposalEmailUser As DiscountProposalEmailUser = CType(e.Item.DataItem, DiscountProposalEmailUser)
            lblID.Text = objDiscountProposalEmailUser.ID
            lblRecipientName.Text = objDiscountProposalEmailUser.RecipientName
            lblRecipientPosition.Text = objDiscountProposalEmailUser.RecipientPosition
            lblEmail.Text = objDiscountProposalEmailUser.Email
        End If
    End Sub

    Private Sub dtgPosisi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEmailUser.PageIndexChanged
        dtgEmailUser.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgEmailUser.CurrentPageIndex)
    End Sub

    Private Sub dtgPosisi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEmailUser.SortCommand
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
        dtgEmailUser.CurrentPageIndex = 0
        BindDataGrid(dtgEmailUser.CurrentPageIndex)
    End Sub

End Class
