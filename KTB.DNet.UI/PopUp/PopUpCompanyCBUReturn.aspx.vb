Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade

Public Class PopUpCompanyCBUReturn
    Inherits System.Web.UI.Page
    Private sessHelper As SessionHelper = New SessionHelper
    Dim objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Dim crit As CriteriaComposite
    Dim objCMLFacade As ChassisMasterLogisticCompanyFacade = New ChassisMasterLogisticCompanyFacade(User)

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        RefreshGrid()
    End Sub

    Protected Sub dgInfoCompany_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
        If e.SortExpression = ViewState.Item("SortCol") Then
            If ViewState.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("SortCol") = e.SortExpression
        dgInfoCompany.SelectedIndex = -1
        dgInfoCompany.CurrentPageIndex = 0
        RefreshGrid(0)
    End Sub

    Protected Sub dgInfoCompany_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs)
        RefreshGrid(e.NewPageIndex)
    End Sub
#End Region

#Region "Custom Method"

    Private Sub RefreshGrid(Optional indexPage As Integer = 0)
        Dim totalRow As Integer
        Dim dateFrom As DateTime
        Dim dateTo As DateTime

        crit = New CriteriaComposite(New Criteria(GetType(ChassisMasterLogisticCompany), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not txtCompanyName.Text.Equals("") Then
            crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "Name", MatchType.StartsWith, txtCompanyName.Text))
        End If

        If Not txtKode.Text.Equals("") Then
            crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "Kode", MatchType.StartsWith, txtKode.Text))
        End If

        Dim data As ArrayList = objCMLFacade.RetrieveByCriteria(crit, indexPage + 1, dgInfoCompany.PageSize, totalRow, ViewState.Item("SortCol"), ViewState.Item("SortDirection"))

        If data.Count = 0 Then
            indexPage = 0
            dgInfoCompany.CurrentPageIndex = 0
        Else
            dgInfoCompany.CurrentPageIndex = indexPage
        End If

        dgInfoCompany.DataSource = data
        dgInfoCompany.VirtualItemCount = totalRow
        dgInfoCompany.DataBind()

        inputPilih.Disabled = data.Count = 0

    End Sub
#End Region
End Class