#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports System.Collections.Generic
#End Region

Public Class PopUpSearchBabitPameran
    Inherits System.Web.UI.Page

#Region " custom Declaration "
    Private sesHelper As New SessionHelper
    Private SessionGridData = "PopUpBabitPameranSelectionOne.gridList"
    Private objDealer As Dealer
#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            ViewState("currSortColumn") = "BabitRegNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            objDealer = Session("DEALER")
            ClearData()
            ReadData()   '-- Read all data matching criteria
            BindGrid(dtgEventProposalSelection.CurrentPageIndex)  '-- Bind page-1
        End If
    End Sub

    Public Sub ReadData()
        Dim crit As New CriteriaComposite(New Criteria(GetType(BabitReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arrBRH As ArrayList = New BabitReportHeaderFacade(User).Retrieve(crit)
        Dim notIN As String = ""
        'For Each BRH As BabitReportHeader In arrBRH
        '    If Not IsNothing(BRH.BabitHeader) Then

        '        If notIN.Length > 0 Then
        '            notIN = notIN & ", " & BRH.BabitHeader.ID
        '        Else
        '            notIN = BRH.BabitHeader.ID
        '        End If
        '    End If

        'Next

        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitHeader), "BabitStatus", MatchType.Exact, 5))
        criterias.opAnd(New Criteria(GetType(BabitHeader), "BabitMasterEventType.FormType", MatchType.Exact, 2))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(BabitHeader), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        End If

        If Not txtEventRegNumber.Text = "" Then
            criterias.opAnd(New Criteria(GetType(BabitHeader), "BabitRegNumber", MatchType.[Partial], txtEventRegNumber.Text))
        End If

        If Not txtEventName.Text = "" Then
            criterias.opAnd(New Criteria(GetType(BabitHeader), "BabitDealerNumber", MatchType.[Partial], txtEventName.Text))
        End If

        If chkTanggal.Checked Then
            Dim tglFrom As New Date(icEventDateFrom.Value.Year, icEventDateFrom.Value.Month, icEventDateFrom.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icEventDateTo.Value.Year, icEventDateTo.Value.Month, icEventDateTo.Value.Day, 23, 59, 59)

            criterias.opAnd(New Criteria(GetType(BabitHeader), "PeriodStart", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "((", True)
            criterias.opAnd(New Criteria(GetType(BabitHeader), "PeriodStart", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), ")", False)

            criterias.opOr(New Criteria(GetType(BabitHeader), "PeriodEnd", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "(", True)
            criterias.opAnd(New Criteria(GetType(BabitHeader), "PeriodEnd", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), "))", False)
        End If

        'If notIN.Length > 0 Then
        '    criterias.opAnd(New Criteria(GetType(BabitHeader), "ID", MatchType.NotInSet, notIN))
        'End If

        'criterias.opAnd(New Criteria(GetType(BabitHeader), "ID", MatchType.NotInSet, "SELECT BabitHeaderID FROM BabitReportHeader where Rowstatus = 0"))
        criterias.opAnd(New Criteria(GetType(BabitHeader), "ID", MatchType.NotInSet, "Select BabitHeaderID from BabitReportHeader where babitheaderid is not null and rowstatus =0"))

        Dim sortColl As SortCollection = New SortCollection

        sortColl.Add(New Sort(GetType(BabitHeader), ViewState("currSortColumn").ToString(), [Enum].Parse(GetType(Sort.SortDirection), ViewState("currSortDirection").ToString())))

        Dim arrBabitMasterRetailTargetList As ArrayList = New ArrayList

        arrBabitMasterRetailTargetList = New BabitHeaderFacade(User).RetrieveActiveList(criterias, sortColl)

        sesHelper.SetSession(SessionGridData, arrBabitMasterRetailTargetList)
        If arrBabitMasterRetailTargetList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrBabitMasterRetailTargetList As ArrayList = CType(sesHelper.GetSession(SessionGridData), ArrayList)

        If arrBabitMasterRetailTargetList.Count <> 0 Then
            CommonFunction.SortListControl(arrBabitMasterRetailTargetList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            'Dim PagedList As ArrayList = ArrayListPager.DoPage(arrBabitMasterRetailTargetList, pageIndex, dtgEventProposalSelection.PageSize)
            dtgEventProposalSelection.DataSource = arrBabitMasterRetailTargetList
            dtgEventProposalSelection.VirtualItemCount = arrBabitMasterRetailTargetList.Count()
            dtgEventProposalSelection.DataBind()
            btnChoose.Disabled = False
        Else
            dtgEventProposalSelection.DataSource = New ArrayList
            dtgEventProposalSelection.VirtualItemCount = 0
            dtgEventProposalSelection.CurrentPageIndex = 0
            dtgEventProposalSelection.DataBind()
        End If
    End Sub

    Private Sub dtgEventProposalSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEventProposalSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As BabitHeader = CType(e.Item.DataItem, BabitHeader)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ReadData()
        dtgEventProposalSelection.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dtgEventProposalSelection.CurrentPageIndex)  '-- Bind page-1
        If dtgEventProposalSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgEventProposalSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEventProposalSelection.SortCommand

        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        dtgEventProposalSelection.CurrentPageIndex = 0
        ReadData()
        BindGrid(dtgEventProposalSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgEventProposalSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEventProposalSelection.PageIndexChanged

        ReadData()
        dtgEventProposalSelection.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

#End Region

#Region " Custom Method "

    Private Sub ClearData()
        Me.txtEventRegNumber.Text = String.Empty
        Me.txtEventName.Text = String.Empty
        Me.icEventDateFrom.Value = Date.Now
        Me.icEventDateTo.Value = Date.Now
        chkTanggal.Checked = False
        dtgEventProposalSelection.DataSource = New ArrayList
        dtgEventProposalSelection.DataBind()
    End Sub

#End Region

End Class