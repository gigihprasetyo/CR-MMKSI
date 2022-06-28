#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports System.Collections.Generic
#End Region

Public Class PopUpNoAccEvent
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Private SessionGridData = "PopUpNoAccEvent.gridList"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.DESC
            ClearData()
            ReadData()   '-- Read all data matching criteria
            BindGrid(dtgAccrued.CurrentPageIndex)  '-- Bind page-1
        End If

    End Sub

    Protected Sub dtgAccrued_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgAccrued.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim lblAccKey As Label = CType(e.Item.FindControl("lblAccKey"), Label)
        Dim lblDesc As Label = CType(e.Item.FindControl("lblDesc"), Label)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As MasterAccrued = CType(e.Item.DataItem, MasterAccrued)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
                lblAccKey.Text = RowValue.AccKey
                lblDesc.Text = RowValue.Description
            End If
        End If
    End Sub

    Private Sub ClearData()
        Me.txtAccKey.Text = String.Empty
        Me.txtDesc.Text = String.Empty
        dtgAccrued.DataSource = New ArrayList
        dtgAccrued.DataBind()
    End Sub

    Public Sub ReadData()
        Dim strSQL As String = String.Empty

        Dim criterias As New CriteriaComposite(New Criteria(GetType(MasterAccrued), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not txtAccKey.Text = "" Then
            criterias.opAnd(New Criteria(GetType(MasterAccrued), "AccKey", MatchType.[Partial], txtAccKey.Text))
        End If

        If Not txtDesc.Text = "" Then
            criterias.opAnd(New Criteria(GetType(MasterAccrued), "Description", MatchType.[Partial], txtDesc.Text))
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MasterAccrued), ViewState("currSortColumn").ToString(), [Enum].Parse(GetType(Sort.SortDirection), ViewState("currSortDirection").ToString())))

        Dim arrBabitEventList As ArrayList = New ArrayList
        arrBabitEventList = New MasterAccruedFacade(User).RetrieveActiveList(criterias, sortColl)

        sesHelper.SetSession(SessionGridData, arrBabitEventList)
        If arrBabitEventList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrBabitEventList As ArrayList = CType(sesHelper.GetSession(SessionGridData), ArrayList)

        If arrBabitEventList.Count <> 0 Then
            CommonFunction.SortListControl(arrBabitEventList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            'Dim PagedList As ArrayList = ArrayListPager.DoPage(arrBabitEventList, pageIndex, dtgBabitEventSelection.PageSize)
            dtgAccrued.DataSource = arrBabitEventList
            dtgAccrued.VirtualItemCount = arrBabitEventList.Count()
            dtgAccrued.DataBind()
            btnChoose.Disabled = False
        Else
            dtgAccrued.DataSource = New ArrayList
            dtgAccrued.VirtualItemCount = 0
            dtgAccrued.CurrentPageIndex = 0
            dtgAccrued.DataBind()
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ReadData()
        dtgAccrued.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dtgAccrued.CurrentPageIndex)  '-- Bind page-1
        If dtgAccrued.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub
End Class