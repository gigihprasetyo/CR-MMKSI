Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO

Public Class PopUpChassisCBUReturn
    Inherits System.Web.UI.Page
    Private sessHelper As SessionHelper = New SessionHelper
    Dim objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Dim crit As CriteriaComposite
    Dim objCMFacade As ChassisMasterFacade = New ChassisMasterFacade(User)

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("type")) Then
                Select Case CInt(Request.QueryString("type").ToString)
                    Case 1
                        lblTitle.Text = "Pencarian Chassis"
                    Case 2
                        lblTitle.Text = "Pencarian DO"
                End Select
            End If
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        RefreshGrid()
    End Sub

    Protected Sub dgInfoChassis_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgInfoChassis.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim RowValue As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

            If RowValue.GIDate <> "1/1/1900" Then
                lblStatus.Text = "Keluar"
            ElseIf RowValue.GIDate = "1/1/1900" Then
                lblStatus.Text = "Belum Keluar"
            ElseIf RowValue.SONumber.Trim <> "" Then
                Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
                Dim arlPOH As New ArrayList
                Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crtPOH.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, RowValue.SONumber))
                crtPOH.opAnd(New Criteria(GetType(POHeader), "RemarkStatus", MatchType.Exact, CType(enumPORemarkStatus.PORemarkStatus.TahanDO, Short)))
                'crtPOH.opAnd(New Criteria(GetType(POHeader), "Remark", MatchType.No, ""))
                arlPOH = objPOHFac.Retrieve(crtPOH)
                If arlPOH.Count > 0 Then
                    lblStatus.Text = "Tahan DO"
                End If
            End If
        End If
    End Sub

    Protected Sub dgInfoChassis_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
        If e.SortExpression = ViewState.Item("SortCol") Then
            If ViewState.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("SortCol") = e.SortExpression
        dgInfoChassis.SelectedIndex = -1
        dgInfoChassis.CurrentPageIndex = 0
        RefreshGrid(0)
    End Sub

    Protected Sub dgInfoChassis_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs)
        RefreshGrid(e.NewPageIndex)
    End Sub
#End Region

#Region "Custom Method"

    Private Sub RefreshGrid(Optional indexPage As Integer = 0)
        Dim totalRow As Integer
        Dim dateFrom As DateTime
        Dim dateTo As DateTime
        Dim query As String = String.Empty

        crit = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not IsNothing(Request.QueryString("code")) Then
            If Request.QueryString("code").ToString <> "MKS" Then
                Dim dealer As Dealer = New DealerFacade(User).Retrieve(Request.QueryString("code").ToString)
                crit.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.DealerGroup.ID", MatchType.Exact, dealer.DealerGroup.ID))
            End If
        End If

        query = "SELECT DealerSystems.DealerID FROM DealerSystems WHERE DealerSystems.GoLiveDate IS NOT NULL"
        crit.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.ID", MatchType.NotInSet, Query))

        If Not txtNoDO.Text.Equals("") Then
            crit.opAnd(New Criteria(GetType(ChassisMaster), "DONumber", MatchType.StartsWith, txtNoDO.Text))
        End If

        If Not txtNoPO.Text.Equals("") Then
            crit.opAnd(New Criteria(GetType(ChassisMaster), "PONumber", MatchType.StartsWith, txtNoPO.Text))
        End If

        If Not txtNoRangka.Text.Equals("") Then
            crit.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.StartsWith, txtNoRangka.Text))
        End If

        If CBTglCetak.Checked Then
            dateFrom = New DateTime(icTglCetakDOFrom.Value.Year, icTglCetakDOFrom.Value.Month, icTglCetakDOFrom.Value.Day, 0, 0, 0)
            dateTo = New DateTime(icTglCetakDOTo.Value.Year, icTglCetakDOTo.Value.Month, icTglCetakDOTo.Value.Day, 23, 59, 59)

            crit.opAnd(New Criteria(GetType(ChassisMaster), "DODate", MatchType.GreaterOrEqual, dateFrom))
            crit.opAnd(New Criteria(GetType(ChassisMaster), "DODate", MatchType.LesserOrEqual, dateTo))
        End If

        If CBTglKeluar.Checked Then
            dateFrom = New DateTime(icTglKeluarFrom.Value.Year, icTglKeluarFrom.Value.Month, icTglKeluarFrom.Value.Day, 0, 0, 0)
            dateTo = New DateTime(icTglKeluarTo.Value.Year, icTglKeluarTo.Value.Month, icTglKeluarTo.Value.Day, 23, 59, 59)

            crit.opAnd(New Criteria(GetType(ChassisMaster), "GIDate", MatchType.GreaterOrEqual, dateFrom))
            crit.opAnd(New Criteria(GetType(ChassisMaster), "GIDate", MatchType.LesserOrEqual, dateTo))
        End If

        Dim data As ArrayList = objCMFacade.RetrieveByCriteria(crit, indexPage + 1, dgInfoChassis.PageSize, totalRow, ViewState.Item("SortCol"), ViewState.Item("SortDirection"))

        If data.Count = 0 Then
            indexPage = 0
            dgInfoChassis.CurrentPageIndex = 0
        Else
            dgInfoChassis.CurrentPageIndex = indexPage
        End If

        dgInfoChassis.DataSource = data
        dgInfoChassis.VirtualItemCount = totalRow
        dgInfoChassis.DataBind()

        inputPilih.Disabled = data.Count = 0

    End Sub
#End Region
End Class