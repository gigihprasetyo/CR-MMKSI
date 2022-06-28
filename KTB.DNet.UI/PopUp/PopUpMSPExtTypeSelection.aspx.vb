Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopUpMSPExtTypeSelection
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState.Add("SortColPositionCode", "Code")
            ViewState.Add("SortDirPositionCode", Sort.SortDirection.ASC)
        End If
        BindSearch()
    End Sub

    Public Sub BindSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(MSPExType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtRecallRegNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(MSPExType), "Code", MatchType.StartsWith, txtRecallRegNo.Text))
        End If
        If txtDEscription.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(MSPExType), "Description", MatchType.[Partial], txtDEscription.Text))
        End If

        dtgCodeSelection.DataSource = New MSPExTypeFacade(User).RetrieveActiveList(criterias, ViewState("SortColWorkCode"), ViewState("SortDirWorkCode"))
        dtgCodeSelection.DataBind()

        If dtgCodeSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgCodeSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCodeSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()
    End Sub

    Private Sub dtgCodeSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCodeSelection.SortCommand
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

    Private Sub dtgCodeSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCodeSelection.PageIndexChanged
        dtgCodeSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub
End Class