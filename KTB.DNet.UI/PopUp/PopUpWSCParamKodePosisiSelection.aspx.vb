Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper

Public Class PopUpWSCParamKodePosisiSelection
    Inherits System.Web.UI.Page
    Private sessHelper As SessionHelper = New SessionHelper
    Private typeClaim As String = String.Empty
    Private strSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState.Add("SortColPositionCode", "KodePosition")
            ViewState.Add("SortDirPositionCode", Sort.SortDirection.ASC)
            typeClaim = Request.QueryString("TypeClaim").ToString
            sessHelper.SetSession("TypeClaim", typeClaim)
            InitiatePage()
        End If
        If Not IsNothing(sessHelper.GetSession("TypeClaim")) Then
            typeClaim = sessHelper.GetSession("TypeClaim").ToString
        End If
    End Sub

    Private Sub ClearData()
        Me.txtRecallRegNo.Text = String.Empty
        Me.txtDEscription.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "KodePosition"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()

        dtgPositionCodeSelection.CurrentPageIndex = 0
        BindSearch(dtgPositionCodeSelection.CurrentPageIndex)
    End Sub

    Public Sub BindSearch(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtRecallRegNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "KodePosition", MatchType.StartsWith, txtRecallRegNo.Text))
        End If
        If txtDEscription.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "Description", MatchType.[Partial], txtDEscription.Text))
        End If
        If typeClaim = "Z2" Then
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "KodePosition", MatchType.NotLike, "''[a-z]%''"))
        ElseIf typeClaim = "Z4" Then
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "KodePosition", MatchType.NotLike, "''[0-9]%''"))
        End If

        Dim arrl As ArrayList = New DeskripsiPositionCodeFacade(User).RetrieveActiveList(indexPage + 1, dtgPositionCodeSelection.PageSize, totalRow, CType(ViewState("SortColumn"), String), CType(ViewState("SortDirection"), Sort.SortDirection), criterias)
        'RetrieveByCriteria(criterias, ViewState("SortColPositionCode"), ViewState("SortDirPositionCode"))

        'Dim arrl As ArrayList = New DeskripsiPositionCodeFacade(User).RetrieveActiveList(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dtgPositionCodeSelection.VirtualItemCount = totalRow
        If indexPage = 0 Then
            dtgPositionCodeSelection.CurrentPageIndex = 0
        End If

        Dim PagedList As ArrayList = ArrayListPager.DoPage(arrl, indexPage + 1, dtgPositionCodeSelection.PageSize)
        dtgPositionCodeSelection.DataSource = arrl
        'dtgPositionCodeSelection.VirtualItemCount = arrl.Count()

        dtgPositionCodeSelection.DataBind()

        'dtgPositionCodeSelection.DataSource = arrl
        'dtgPositionCodeSelection.VirtualItemCount = totalRow
        'dtgPositionCodeSelection.DataBind()

        If dtgPositionCodeSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgPositionCodeSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPositionCodeSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not IsNothing(e.Item.DataItem) Then
            e.Item.DataItem.GetType().ToString()
        End If

        'If Not IsNothing(e.Item.DataItem) Then
        '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then
        '        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        '        lblNo.Text = e.Item.ItemIndex + 1 + (dtgPositionCodeSelection.CurrentPageIndex * dtgPositionCodeSelection.PageSize)
        '    End If
        'End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgPositionCodeSelection.CurrentPageIndex = 0
        BindSearch(dtgPositionCodeSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgPositionCodeSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPositionCodeSelection.SortCommand
        If e.SortExpression = ViewState("SortColPositionCode") Then
            If ViewState("SortDirPositionCode") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirPositionCode", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirPositionCode", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColPositionCode", e.SortExpression)
        BindSearch(dtgPositionCodeSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgPositionCodeSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPositionCodeSelection.PageIndexChanged
        dtgPositionCodeSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch(dtgPositionCodeSelection.CurrentPageIndex)
    End Sub
End Class