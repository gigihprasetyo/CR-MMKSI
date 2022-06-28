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
Imports KTB.DNet.BusinessFacade.PO

#End Region


Public Class PopUpPODestinationSelection
    Inherits System.Web.UI.Page

#Region "Declaration"
    Private _sessHelper As SessionHelper = New SessionHelper
#End Region

#Region "Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            CreateCriteriaSearch()
            dtgPODestination.CurrentPageIndex = 0
            BindDataGrid(dtgPODestination.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CreateCriteriaSearch()
        dtgPODestination.CurrentPageIndex = 0
        BindDataGrid(dtgPODestination.CurrentPageIndex)

        If dtgPODestination.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgPODestination_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgPODestination.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objPODestination As PODestination = e.Item.DataItem

        End If
    End Sub

    Private Sub dtgPODestination_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgPODestination.PageIndexChanged
        dtgPODestination.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPODestination.CurrentPageIndex)
    End Sub

    Private Sub dtgPODestination_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgPODestination.SortCommand
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
        dtgPODestination.CurrentPageIndex = 0
        BindDataGrid(dtgPODestination.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"
    Private Sub ClearData()
        Me.txtPODestinationCode.Text = String.Empty
        Me.txtNama.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("CriteriaPODestination"), CriteriaComposite)) Then

            Dim sortColl As SortCollection = New SortCollection
            Dim arrPODestination As ArrayList = New ArrayList
            sortColl.Add(New Sort(GetType(PODestination), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            arrPODestination = New PODestinationFacade(User).RetrieveList(CType(_sessHelper.GetSession("CriteriaPODestination"), CriteriaComposite), indexPage + 1, dtgPODestination.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgPODestination.DataSource = arrPODestination
            dtgPODestination.VirtualItemCount = totalRow
            dtgPODestination.DataBind()
        End If

        If dtgPODestination.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub CreateCriteriaSearch()
        Dim objDealer As Dealer = Session.Item("DEALER")

        Dim crits As New CriteriaComposite(New Criteria(GetType(PODestination), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(PODestination), "Dealer.ID", MatchType.Exact, objDealer.ID))

        If txtPODestinationCode.Text <> "" Then
            crits.opAnd(New Criteria(GetType(PODestination), "Code", MatchType.[Partial], txtPODestinationCode.Text))
        End If
        If txtNama.Text <> "" Then
            crits.opAnd(New Criteria(GetType(PODestination), "Nama", MatchType.[Partial], txtNama.Text))
        End If

        _sessHelper.SetSession("CriteriaPODestination", crits)

    End Sub
#End Region
End Class