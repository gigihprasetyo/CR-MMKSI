Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopUpWSCParamKodeKerusakanSelection
    Inherits System.Web.UI.Page
    Private DmgType As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Request.QueryString("DmgType").ToString) Then
            DmgType = Request.QueryString("DmgType").ToString
            lblHeader.Text = DmgType
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState.Add("SortColDmgCode", "PositionCode")
            ViewState.Add("SortDirDmgCode", Sort.SortDirection.ASC)
        End If
        BindSearch()
    End Sub


    Public Sub BindSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PositionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If DmgType.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(PositionWSC), "PositionCategory", MatchType.Exact, DmgType))
        End If
        If Not txtRecallRegNo.Text = "" Then
            criterias.opAnd(New Criteria(GetType(PositionWSC), "PositionCode", MatchType.[Partial], txtRecallRegNo.Text))
        End If
        If Not txtDEscription.Text = "" Then
            criterias.opAnd(New Criteria(GetType(PositionWSC), "Description", MatchType.[Partial], txtDEscription.Text))
        End If

        dtgDmgCodeSelection.DataSource = New PositionWSCFacade(User).RetrieveActiveList(criterias, ViewState("SortColDmgCode"), ViewState("SortDirDmgCode"))
        dtgDmgCodeSelection.DataBind()

        If dtgDmgCodeSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub


    Private Sub dtgDmgCodeSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDmgCodeSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()
    End Sub

    Private Sub dtgDmgCodeSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDmgCodeSelection.SortCommand
        If e.SortExpression = ViewState("SortColDmgCode") Then
            If ViewState("SortDirDmgCode") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirDmgCode", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirDmgCode", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColDmgCode", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgDmgCodeSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDmgCodeSelection.PageIndexChanged
        dtgDmgCodeSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub

End Class