Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopUpWSCParamKodeKerjaSelection
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState.Add("SortColPositionCode", "KodeKerja")
            ViewState.Add("SortDirPositionCode", Sort.SortDirection.ASC)
        End If
        BindSearch()
    End Sub

    Public Sub BindSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtRecallRegNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(DeskripsiKodeKerja), "KodeKerja", MatchType.StartsWith, txtRecallRegNo.Text))
        End If
        If txtDEscription.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(DeskripsiKodeKerja), "Description", MatchType.[Partial], txtDEscription.Text))
        End If

        dtgWorkCodeSelection.DataSource = New DeskripsiWorkPositionFacade(User).RetrieveActiveList(criterias, ViewState("SortColWorkCode"), ViewState("SortDirWorkCode"))
        dtgWorkCodeSelection.DataBind()

        If dtgWorkCodeSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgWorkCodeSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgWorkCodeSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()
    End Sub

    Private Sub dtgWorkCodeSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgWorkCodeSelection.SortCommand
        If e.SortExpression = viewstate("SortColWorkCode") Then
            If viewstate("SortDirWorkCode") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirWorkCode", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirWorkCode", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColWorkCode", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgWorkCodeSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgWorkCodeSelection.PageIndexChanged
        dtgWorkCodeSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub
End Class