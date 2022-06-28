Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopUpWSCParamBuletinNumSelection
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState.Add("SortColBuletinNum", "RecallRegNo")
            ViewState.Add("SortDirBuletinNum", Sort.SortDirection.ASC)
        End If
        BindSearch()
    End Sub


    Public Sub BindSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not txtRecallRegNo.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RecallRegNo", MatchType.[Partial], txtRecallRegNo.Text))
        End If

        If Not txtDEscription.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "Description", MatchType.[Partial], txtDEscription.Text))
        End If

        dtgBuletinNumSelection.DataSource = New RecallCategoryFacade(User).RetrieveActiveList(criterias, ViewState("SortColBuletinNum"), ViewState("SortDirBuletinNum"))
        dtgBuletinNumSelection.DataBind()

        If dtgBuletinNumSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub


    Private Sub dtgBuletinNumSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBuletinNumSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()
    End Sub

    Private Sub dtgBuletinNumSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgBuletinNumSelection.SortCommand
        If e.SortExpression = viewstate("SortColBuletinNum") Then
            If viewstate("SortDirBuletinNum") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirBuletinNum", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirBuletinNum", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColBuletinNum", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgBuletinNumSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgBuletinNumSelection.PageIndexChanged
        dtgBuletinNumSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub
End Class