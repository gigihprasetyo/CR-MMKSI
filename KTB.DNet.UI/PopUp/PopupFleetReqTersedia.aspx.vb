#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade

#End Region


Public Class PopupFleetReqTersedia
    Inherits System.Web.UI.Page

#Region "Declaration"
    Private _sessHelper As SessionHelper = New SessionHelper
#End Region

#Region "Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CreateCriteriaSearch()
        dtgFleetRequest.CurrentPageIndex = 0
        BindDataGrid(dtgFleetRequest.CurrentPageIndex)

        If dtgFleetRequest.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgFleetRequest_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgFleetRequest.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objFleetRequest As FleetRequest = e.Item.DataItem
            e.Item.Cells(4).Text = objFleetRequest.CreatedTime.ToString("dd-MM-yyyy")
            e.Item.Cells(5).Text = UserInfo.Convert(objFleetRequest.CreatedBy)
        End If
    End Sub

    Private Sub dtgFleetRequest_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgFleetRequest.PageIndexChanged
        dtgFleetRequest.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgFleetRequest.CurrentPageIndex)
    End Sub

    Private Sub dtgFleetRequest_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgFleetRequest.SortCommand
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
        dtgFleetRequest.CurrentPageIndex = 0
        BindDataGrid(dtgFleetRequest.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"
    Private Sub ClearData()
        Me.txtFleetRequestNumber.Text = String.Empty
        Me.txtNamaKonsumen.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "NoRegRequest"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("CriteriaFleetRequestTersedia"), CriteriaComposite)) Then

            Dim sortColl As SortCollection = New SortCollection
            Dim arrFleetRequest As ArrayList = New ArrayList
            sortColl.Add(New Sort(GetType(FleetRequest), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            arrFleetRequest = New FleetRequestFacade(User).RetrieveList(CType(_sessHelper.GetSession("CriteriaFleetRequestTersedia"), CriteriaComposite), indexPage + 1, dtgFleetRequest.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgFleetRequest.DataSource = arrFleetRequest
            dtgFleetRequest.VirtualItemCount = totalRow
            dtgFleetRequest.DataBind()
        End If

        If dtgFleetRequest.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub CreateCriteriaSearch()

        Dim crits As New CriteriaComposite(New Criteria(GetType(FleetRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(FleetRequest), "Status", MatchType.Exact, CType(EnumFleetRequest.FleetRequestStatus.Konfirmasi, Integer)))
        If txtFleetRequestNumber.Text <> "" Then
            crits.opAnd(New Criteria(GetType(FleetRequest), "NoRegRequest", MatchType.[Partial], txtFleetRequestNumber.Text))
        End If
        If txtNamaKonsumen.Text <> "" Then
            crits.opAnd(New Criteria(GetType(FleetRequest), "NamaKonsumen", MatchType.[Partial], txtNamaKonsumen.Text))
        End If

        Dim objdealer As Dealer = New SessionHelper().GetSession("DEALER")
        If Not objdealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If Val(Request.QueryString("IsGroupDealer")) = 0 Then
                crits.opAnd(New Criteria(GetType(FleetRequest), "DealerID", MatchType.Exact, objdealer.ID))
            Else
                'For FleetRequest Get by dealer group
                Dim DealerGroupID As Integer = objdealer.DealerGroup.ID
                If DealerGroupID = 21 Then 'Single Dealer
                    crits.opAnd(New Criteria(GetType(FleetRequest), "FleetMasterDealer.Dealer.ID", MatchType.Exact, objdealer.ID))
                Else
                    crits.opAnd(New Criteria(GetType(FleetRequest), "FleetMasterDealer.Dealer.ID", MatchType.InSet, "(select ID from Dealer where DealerGroupID=" & DealerGroupID.ToString & ")"))
                End If
            End If
        Else
            crits.opAnd(New Criteria(GetType(FleetRequest), "FleetMasterDealer.Dealer.ID", MatchType.Exact, objdealer.ID))
        End If


        _sessHelper.SetSession("CriteriaFleetRequestTersedia", crits)

    End Sub
#End Region
End Class