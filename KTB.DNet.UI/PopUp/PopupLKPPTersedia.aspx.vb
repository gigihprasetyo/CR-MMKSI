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
Imports KTB.DNet.BusinessFacade.LKPP

#End Region


Public Class PopupLKPPTersedia
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
        dtgLKPP.CurrentPageIndex = 0
        BindDataGrid(dtgLKPP.CurrentPageIndex)

        If dtgLKPP.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgLKPP_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgLKPP.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objV_LKPPByDealer As V_LKPPbyDealer = e.Item.DataItem
            e.Item.Cells(4).Text = objV_LKPPByDealer.CreatedTime.ToString("dd-MM-yyyy")
            e.Item.Cells(5).Text = UserInfo.Convert(objV_LKPPByDealer.CreatedBy)
        End If
    End Sub

    Private Sub dtgLKPP_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgLKPP.PageIndexChanged
        dtgLKPP.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgLKPP.CurrentPageIndex)
    End Sub

    Private Sub dtgLKPP_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgLKPP.SortCommand
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
        dtgLKPP.CurrentPageIndex = 0
        BindDataGrid(dtgLKPP.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"
    Private Sub ClearData()
        Me.txtLKPPNumber.Text = String.Empty
        Me.txtIntitutionName.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ReferenceNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("CriteriaLKPPTersedia"), CriteriaComposite)) Then

            Dim sortColl As SortCollection = New SortCollection
            Dim arrLKPPHeader As ArrayList = New ArrayList
            sortColl.Add(New Sort(GetType(V_LKPPbyDealer), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            arrLKPPHeader = New V_LKPPByDealerFacade(User).RetrieveActiveList(indexPage + 1, dtgLKPP.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection), CType(_sessHelper.GetSession("CriteriaLKPPTersedia"), CriteriaComposite))

            dtgLKPP.DataSource = arrLKPPHeader
            dtgLKPP.VirtualItemCount = totalRow
            dtgLKPP.DataBind()
        End If

        If dtgLKPP.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub CreateCriteriaSearch()

        Dim crits As New CriteriaComposite(New Criteria(GetType(V_LKPPbyDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(V_LKPPbyDealer), "Status", MatchType.Exact, CType(EnumStatusLKPP.StatusLKPP.Setuju, Integer)))

        If Not IsNothing(Request.QueryString("SN")) OrElse Not IsNothing(Request.QueryString("VC")) Then
            Dim critLKD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If Not IsNothing(Request.QueryString("SN")) Then
                Dim cMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(Request.QueryString("SN"))
                critLKD.opAnd(New Criteria(GetType(LKPPDetail), "VechileType.ID", MatchType.Exact, cMaster.VechileColor.VechileType.ID))
            ElseIf Not IsNothing(Request.QueryString("VC")) Then
                Dim vVechileColor As VechileColor = New VechileColorFacade(User).Retrieve(CInt(Request.QueryString("VC")))
                critLKD.opAnd(New Criteria(GetType(LKPPDetail), "VechileType.ID", MatchType.Exact, vVechileColor.VechileType.ID))
            End If

            Dim arlLKD As ArrayList = New LKPPDetailFacade(User).Retrieve(critLKD)
            Dim HeaderIDs As String = String.Empty
            For Each item As LKPPDetail In arlLKD
                If HeaderIDs.Length = 0 Then
                    HeaderIDs = item.LKPPHeader.ID
                Else
                    HeaderIDs = HeaderIDs & ", " & item.LKPPHeader.ID
                End If
            Next
            If HeaderIDs.Trim.Length = 0 Then
                crits.opAnd(New Criteria(GetType(V_LKPPbyDealer), "LKPPHeaderID", MatchType.InSet, "(0)"))
            Else
                crits.opAnd(New Criteria(GetType(V_LKPPbyDealer), "LKPPHeaderID", MatchType.InSet, "(" & HeaderIDs & ")"))
            End If
        End If

        If txtLKPPNumber.Text <> "" Then
            crits.opAnd(New Criteria(GetType(V_LKPPbyDealer), "ReferenceNumber", MatchType.[Partial], txtLKPPNumber.Text))
        End If
        If txtIntitutionName.Text <> "" Then
            crits.opAnd(New Criteria(GetType(V_LKPPbyDealer), "GovInstName", MatchType.[Partial], txtIntitutionName.Text))
        End If

        Dim objdealer As Dealer = New SessionHelper().GetSession("DEALER")
        If Not Convert.ToInt32(objdealer.Title) = EnumDealerTittle.DealerTittle.KTB Then
            'If Val(Request.QueryString("IsGroupDealer")) = 0 Then
            '    crits.opAnd(New Criteria(GetType(V_LKPPbyDealer), "DealerID", MatchType.Exact, objdealer.ID))
            'Else
            'For LKPP Get by dealer group
            Dim DealerGroupID As Integer = objdealer.DealerGroup.ID
            If DealerGroupID = 21 Then 'Single Dealer
                crits.opAnd(New Criteria(GetType(V_LKPPbyDealer), "DealerID", MatchType.Exact, objdealer.ID))
            Else
                crits.opAnd(New Criteria(GetType(V_LKPPbyDealer), "DealerID", MatchType.InSet, "(select ID from Dealer where DealerGroupID=" & DealerGroupID.ToString & ")"))
            End If
            'End If
        End If


        _sessHelper.SetSession("CriteriaLKPPTersedia", crits)

    End Sub
#End Region

End Class