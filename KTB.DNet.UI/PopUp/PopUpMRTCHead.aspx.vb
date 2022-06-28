#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.General
#End Region

Public Class PopUpMRTCHead
    Inherits System.Web.UI.Page

    Protected countChk As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            'ddlStatus.Enabled = False
            'ddlStatus.Items.Add("")
            'ddlStatus.Items(2).Value = ""
        End If
    End Sub

    Private Sub InitiatePage()
        Try
            If String.IsNullOrEmpty(Request.QueryString("dealerCode")) Then
                btnSearch.Enabled = False
                Throw New Exception("Error : harap memilih Owner MRTC terlebih dahulu")
                Exit Sub
            End If

            FillDealerInfo(Request.QueryString("dealerCode"))
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            ClearData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub FillDealerInfo(ByVal dealerId As String)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, dealerId))
        Dim arlDealer As ArrayList = New DealerFacade(User).Retrieve(criterias)

        If arlDealer.Count > 0 Then
            Dim dealer As Dealer = CType(arlDealer(0), Dealer)
            txtDealerCode.Text = dealer.DealerCode
        Else
            Throw New Exception("Dealer tidak ditemukan")
        End If

    End Sub

    Private Sub ClearData()
        Me.txtName.Text = String.Empty
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrTrainee), "Name", MatchType.[Partial], txtName.Text))
        End If

        If txtDealerCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrTrainee), "Dealer.DealerCode", MatchType.[Partial], txtDealerCode.Text))
        End If

        Return criterias
    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        dtgUserInfo.DataSource = New TrTraineeFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dtgUserInfo.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgUserInfo.VirtualItemCount = totalRow
        dtgUserInfo.DataBind()
    End Sub

    Private Sub dtgUserInfo_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgUserInfo.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As TrTrainee = CType(e.Item.DataItem, TrTrainee)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                Dim lblTraineeName As Label = CType(e.Item.FindControl("lblTraineeName"), Label)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)

                lblID.Text = RowValue.ID
                lblTraineeName.Text = RowValue.Name
                lblDealerCode.Text = RowValue.Dealer.DealerCode + " - " + RowValue.Dealer.DealerName

            End If
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindDataGrid(0)

        If dtgUserInfo.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgUserInfo_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgUserInfo.PageIndexChanged
        dtgUserInfo.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgUserInfo.CurrentPageIndex)
    End Sub

    Private Sub dtgUserInfo_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgUserInfo.SortCommand
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

        dtgUserInfo.CurrentPageIndex = 0
        BindDataGrid(dtgUserInfo.CurrentPageIndex)
    End Sub
End Class