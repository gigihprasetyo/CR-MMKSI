#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.Utility
#End Region

Public Class PopUpCourseMany
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodeKategori As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNamaKategori As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgCourseSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtSelectedIndex As System.Web.UI.HtmlControls.HtmlInputHidden
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

    Protected countChk As Integer = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            'ddlStatus.Enabled = False
            'ddlStatus.Items.Add("")
            'ddlStatus.Items(2).Value = ""
        End If
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "CourseCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub ClearData()
        Me.txtKodeKategori.Text = String.Empty
        Me.txtNamaKategori.Text = String.Empty
        ddlStatus.SelectedIndex = 0
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeKategori.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrCourse), "CourseCode", MatchType.[Partial], txtKodeKategori.Text))
        End If
        If txtNamaKategori.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrCourse), "CourseName", MatchType.[Partial], txtNamaKategori.Text))
        End If

        'If ddlStatus.SelectedValue <> "" Then
        'criterias.opAnd(New Criteria(GetType(TrCourse), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        'End If

        'ada permintaan untuk menampilkan yang aktif saja, tidak menggunakan ddlstatus lagi
        criterias.opAnd(New Criteria(GetType(TrCourse), "Status", MatchType.Exact, CType(EnumTrDataStatus.DataStatusType.Active, String)))

        Return criterias
    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        dtgCourseSelection.DataSource = New TrCourseFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dtgCourseSelection.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgCourseSelection.VirtualItemCount = totalRow
        dtgCourseSelection.DataBind()
    End Sub

    Private Sub dtgDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCourseSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        'If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
        '    Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
        '    e.Item.Cells(0).Controls.Add(rdbChoice)
        'End If

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As TrCourse = CType(e.Item.DataItem, TrCourse)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                'Dim lblKodeKategori As Label = CType(e.Item.FindControl("lblKodeKategori"), Label)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                'If Not IsNothing(RowValue.CourseCode) Then
                '    lblKodeKategori.Text = RowValue.CourseCode.ToString()
                'End If

                If Not IsNothing(RowValue.Status) Then
                    If RowValue.Status = "1" Then
                        lblStatus.Text = "Aktif"
                    Else
                        lblStatus.Text = "Tidak Aktif"
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)

        If dtgCourseSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgCourseSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCourseSelection.PageIndexChanged
        dtgCourseSelection.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgCourseSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgCourseSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCourseSelection.SortCommand
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

        dtgCourseSelection.CurrentPageIndex = 0
        BindDataGrid(dtgCourseSelection.CurrentPageIndex)
    End Sub

End Class
