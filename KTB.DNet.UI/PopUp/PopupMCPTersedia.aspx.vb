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
#End Region


Public Class PopupMCPTersedia
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
        dtgMCP.CurrentPageIndex = 0
        BindDataGrid(dtgMCP.CurrentPageIndex)

        If dtgMCP.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgMCP_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMCP.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objV_MCPByDealer As V_MCPbyDealer = e.Item.DataItem
            e.Item.Cells(4).Text = objV_MCPByDealer.CreatedTime.ToString("dd-MM-yyyy")
            e.Item.Cells(5).Text = UserInfo.Convert(objV_MCPByDealer.CreatedBy)
        End If
    End Sub

    Private Sub dtgMCP_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMCP.PageIndexChanged
        dtgMCP.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgMCP.CurrentPageIndex)
    End Sub

    Private Sub dtgMCP_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgMCP.SortCommand
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
        dtgMCP.CurrentPageIndex = 0
        BindDataGrid(dtgMCP.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"
    Private Sub ClearData()
        Me.txtMCPNumber.Text = String.Empty
        Me.txtIntitutionName.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ReferenceNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("CriteriaMCPTersedia"), CriteriaComposite)) Then

            Dim sortColl As SortCollection = New SortCollection
            Dim arrMCPHeader As ArrayList = New ArrayList
            sortColl.Add(New Sort(GetType(V_MCPbyDealer), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            arrMCPHeader = New V_MCPByDealerFacade(User).RetrieveActiveList(indexPage + 1, dtgMCP.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection), CType(_sessHelper.GetSession("CriteriaMCPTersedia"), CriteriaComposite))

            dtgMCP.DataSource = arrMCPHeader
            dtgMCP.VirtualItemCount = totalRow
            dtgMCP.DataBind()
        End If

        If dtgMCP.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub CreateCriteriaSearch()

        Dim crits As New CriteriaComposite(New Criteria(GetType(V_MCPbyDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(V_MCPbyDealer), "Status", MatchType.Exact, CType(EnumStatusMCP.StatusMCP.Aktif, Integer)))
        If txtMCPNumber.Text <> "" Then
            crits.opAnd(New Criteria(GetType(V_MCPbyDealer), "ReferenceNumber", MatchType.[Partial], txtMCPNumber.Text))
        End If
        If txtIntitutionName.Text <> "" Then
            crits.opAnd(New Criteria(GetType(V_MCPbyDealer), "GovInstName", MatchType.[Partial], txtIntitutionName.Text))
        End If

        Dim objdealer As Dealer = New SessionHelper().GetSession("DEALER")
        If Not objdealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If Val(Request.QueryString("IsGroupDealer")) = 0 Then
                crits.opAnd(New Criteria(GetType(V_MCPbyDealer), "DealerID", MatchType.Exact, objdealer.ID))
            Else
                'For MCP Get by dealer group
                Dim DealerGroupID As Integer = objdealer.DealerGroup.ID
                If DealerGroupID = 21 Then 'Single Dealer
                    crits.opAnd(New Criteria(GetType(V_MCPbyDealer), "DealerID", MatchType.Exact, objdealer.ID))
                Else
                    crits.opAnd(New Criteria(GetType(V_MCPbyDealer), "DealerID", MatchType.InSet, "(select ID from Dealer where DealerGroupID=" & DealerGroupID.ToString & ")"))
                End If
            End If
        End If


        _sessHelper.SetSession("CriteriaMCPTersedia", crits)

    End Sub
#End Region
End Class