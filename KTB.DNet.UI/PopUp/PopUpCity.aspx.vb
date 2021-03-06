#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
#End Region

Public Class PopUpCity
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgCity As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub ClearData()
        Me.txtKode.Text = String.Empty
        Me.txtDeskripsi.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "CityCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        dtgCity.DataSource = New CityFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, _
            dtgCity.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgCity.VirtualItemCount = totalRow
        dtgCity.DataBind()
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(City), "CityCode", MatchType.[Partial], txtKode.Text))
        End If
        If txtDeskripsi.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(City), "CityName", MatchType.[Partial], txtDeskripsi.Text))
        End If
        criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))
        Return criterias
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
        If dtgCity.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgPosisi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCity.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
        
    End Sub

    Private Sub dtgPosisi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCity.PageIndexChanged
        dtgCity.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgCity.CurrentPageIndex)
    End Sub

    Private Sub dtgPosisi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCity.SortCommand
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
        dtgCity.CurrentPageIndex = 0
        BindDataGrid(dtgCity.CurrentPageIndex)
    End Sub

End Class
