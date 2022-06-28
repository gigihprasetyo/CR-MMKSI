#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports System.Collections.Generic
Imports System.Linq

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
#End Region

Public Class PopUpMRTC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState.Add("CurrentSortColumn", "Code")
            ViewState.Add("CurrentSortDirect", Sort.SortDirection.ASC)
            ClearData()
        End If
    End Sub

    Private Sub ClearData()
        txtMRTCCode.Text = String.Empty
        txtMRTCName.Text = String.Empty
        dtgMRTCSelection.DataSource = Nothing
        dtgMRTCSelection.DataBind()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        dtgMRTCSelection.DataSource = New TrMRTCFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dtgMRTCSelection.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgMRTCSelection.VirtualItemCount = totalRow
        dtgMRTCSelection.DataBind()
    End Sub

    Private Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrMRTC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtMRTCCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "Code", MatchType.[Partial], txtMRTCCode.Text))
        End If

        If txtMRTCName.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(TrMRTC), "Name", MatchType.[Partial], txtMRTCName.Text))
        End If
        criterias.opAnd(New Criteria(GetType(TrMRTC), "Status", MatchType.Exact, 1))

        Return criterias
    End Function

    Private Sub dtgMRTCSelection_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMRTCSelection.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then

            Dim RowValue As TrMRTC = CType(e.Item.DataItem, TrMRTC)

            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)

            Dim lblMRTCCode As Label = CType(e.Item.FindControl("lblMRTCCode"), Label)
            Dim lblMRTCName As Label = CType(e.Item.FindControl("lblMRTCName"), Label)
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Dim lblProvince As Label = CType(e.Item.FindControl("lblProvince"), Label)
            Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
            Dim lblAddress As Label = CType(e.Item.FindControl("lblAddress"), Label)

            lblMRTCCode.Text = RowValue.Code
            lblMRTCName.Text = RowValue.Name
            lblDealer.Text = RowValue.Dealer.DealerCode & "-" & RowValue.Dealer.DealerName
            lblProvince.Text = RowValue.City.Province.ProvinceName
            lblCity.Text = RowValue.City.CityName
            lblAddress.Text = RowValue.Address

        End If
    End Sub

    Private Sub dtgMRTCSelection_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMRTCSelection.PageIndexChanged
        dtgMRTCSelection.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgMRTCSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgMRTCSelection_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgMRTCSelection.SortCommand
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

        dtgMRTCSelection.CurrentPageIndex = 0
        BindDataGrid(dtgMRTCSelection.CurrentPageIndex)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            BindDataGrid(0)

            btnChoose.Disabled = False

            If dtgMRTCSelection.Items.Count < 0 Then
                btnChoose.Disabled = True
                MessageBox.Show("Data tidak ditemukan")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class