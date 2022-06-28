#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.MDP
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports System.Linq
#End Region

Public Class PopUpStatusMasterDealer
    Inherits System.Web.UI.Page

#Region "Private Varibles"
    Private arlMDPMasterDealerHistory As ArrayList
    Private _sessHelper As SessionHelper = New SessionHelper
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblKodeDealer.Text = Request.QueryString("DealerCode")
        BindDataGrid()
    End Sub

    Private Sub BindDataGrid()
        ReadData()   '-- Read all data matching criteria
        BindPage(0)  '-- Bind page-1
    End Sub

    Private Sub ReadData()
        Dim objDealer As New Dealer
        '-- Search criteria:
        '-- Row status
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterDealerHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If lblKodeDealer.Text <> "" Then
            objDealer = New DealerFacade(User).Retrieve(lblKodeDealer.Text)
            criterias.opAnd(New Criteria(GetType(MDPMasterDealerHistory), "MDPMasterDealer.Dealer.ID", MatchType.Exact, objDealer.ID))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MDPMasterDealerHistory), "MDPMasterDealer.Dealer.DealerCode", Sort.SortDirection.ASC))

        '-- Retrieve color list
        Dim arrMDPMasterDealerHistoryList As ArrayList = New MDPMasterDealerHistoryFacade(User).Retrieve(criterias, sortColl)

        '-- Store MDP Master Dealer List into session
        _sessHelper.SetSession("sesMDPMasterDealerHistoryList", arrMDPMasterDealerHistoryList)

        If arrMDPMasterDealerHistoryList.Count < 0 Then
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        '-- Bind page-i

        '-- Read MDPMasterDealerList from session
        Dim MDPMasterDealerHistoryList As ArrayList = CType(_sessHelper.GetSession("sesMDPMasterDealerHistoryList"), ArrayList)

        If Not IsNothing(MDPMasterDealerHistoryList) AndAlso MDPMasterDealerHistoryList.Count <> 0 Then
            '-- Then paging
            Dim PagedList As ArrayList = ArrayListPager.DoPage(MDPMasterDealerHistoryList, pageIndex, dtgStatusMasterDealer.PageSize)
            dtgStatusMasterDealer.DataSource = PagedList
            dtgStatusMasterDealer.VirtualItemCount = MDPMasterDealerHistoryList.Count
            dtgStatusMasterDealer.CurrentPageIndex = pageIndex
            dtgStatusMasterDealer.DataBind()
        Else
            '-- Display datagrid header only
            dtgStatusMasterDealer.DataSource = New ArrayList
            dtgStatusMasterDealer.VirtualItemCount = 0
            dtgStatusMasterDealer.CurrentPageIndex = 0
            dtgStatusMasterDealer.DataBind()
        End If

    End Sub

    Private Sub dtgStatusMasterDealer_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgStatusMasterDealer.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            lblNo.Text = (dtgStatusMasterDealer.PageSize * dtgStatusMasterDealer.CurrentPageIndex) + e.Item.ItemIndex + 1

            Dim lblStatusLama As Label = e.Item.FindControl("lblStatusLama")
            lblStatusLama.Text = IIf(e.Item.DataItem.StatusFrom = 1, "Aktif", "Tidak Aktif")

            Dim lblStatusBaru As Label = e.Item.FindControl("lblStatusBaru")
            lblStatusBaru.Text = IIf(e.Item.DataItem.StatusTo = 1, "Aktif", "Tidak Aktif")

            Dim lblTglUpdate As Label = e.Item.FindControl("lblTglUpdate")
            lblTglUpdate.Text = Format(e.Item.DataItem.LastUpdateTime, "dd/MM/yyyy")
        End If
    End Sub

End Class