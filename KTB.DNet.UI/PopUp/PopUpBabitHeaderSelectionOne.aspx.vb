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

Public Class PopUpBabitHeaderSelectionOne
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgBabitEventSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtBabitRegNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBabitDealerNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents icBabitEventDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icBabitEventDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents chkTanggal As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " custom Declaration "
    Private sesHelper As New SessionHelper
    Private SessionGridData = "PopUpBabitEventSelectionOne.gridList"
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
            BindGrid(dtgBabitEventSelection.CurrentPageIndex)  '-- Bind page-1
        End If
    End Sub

    Public Sub ReadData()
        Dim strSQL As String = String.Empty

        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        strSQL = "select ValueId from StandardCode where Category = 'EnumBabit.BabitStatus' and ValueCode = 'Setuju'"
        criterias.opAnd(New Criteria(GetType(BabitHeader), "BabitStatus", MatchType.InSet, "(" & strSQL & ")"))

        strSQL = "select ValueId from StandardCode where Category = 'EnumBabit.BabitType' and ValueCode = 'BabitEvent'"
        criterias.opAnd(New Criteria(GetType(BabitHeader), "BabitMasterEventType.FormType", MatchType.InSet, "(" & strSQL & ")"))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(BabitHeader), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        End If

        If Not txtBabitRegNumber.Text = "" Then
            criterias.opAnd(New Criteria(GetType(BabitHeader), "BabitRegNumber", MatchType.[Partial], txtBabitRegNumber.Text))
        End If

        If Not txtBabitDealerNumber.Text = "" Then
            criterias.opAnd(New Criteria(GetType(BabitHeader), "BabitDealerNumber", MatchType.[Partial], txtBabitDealerNumber.Text))
        End If

        If chkTanggal.Checked Then
            Dim tglFrom As New Date(icBabitEventDateFrom.Value.Year, icBabitEventDateFrom.Value.Month, icBabitEventDateFrom.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icBabitEventDateTo.Value.Year, icBabitEventDateTo.Value.Month, icBabitEventDateTo.Value.Day, 23, 59, 59)

            criterias.opAnd(New Criteria(GetType(BabitHeader), "PeriodStart", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "((", True)
            criterias.opAnd(New Criteria(GetType(BabitHeader), "PeriodStart", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), ")", False)

            criterias.opOr(New Criteria(GetType(BabitHeader), "PeriodEnd", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "(", True)
            criterias.opAnd(New Criteria(GetType(BabitHeader), "PeriodEnd", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), "))", False)
        End If

        strSQL = "Select BabitHeaderID From BabitReportHeader where RowStatus = 0"
        criterias.opAnd(New Criteria(GetType(BabitHeader), "ID", MatchType.NotInSet, "(" & strSQL & ")"))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitHeader), ViewState("currSortColumn").ToString(), [Enum].Parse(GetType(Sort.SortDirection), ViewState("currSortDirection").ToString())))

        Dim arrBabitEventList As ArrayList = New ArrayList
        arrBabitEventList = New BabitHeaderFacade(User).RetrieveActiveList(criterias, sortColl)

        sesHelper.SetSession(SessionGridData, arrBabitEventList)
        If arrBabitEventList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrBabitEventList As ArrayList = CType(sesHelper.GetSession(SessionGridData), ArrayList)

        If arrBabitEventList.Count <> 0 Then
            CommonFunction.SortListControl(arrBabitEventList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            'Dim PagedList As ArrayList = ArrayListPager.DoPage(arrBabitEventList, pageIndex, dtgBabitEventSelection.PageSize)
            dtgBabitEventSelection.DataSource = arrBabitEventList
            dtgBabitEventSelection.VirtualItemCount = arrBabitEventList.Count()
            dtgBabitEventSelection.DataBind()
            btnChoose.Disabled = False
        Else
            dtgBabitEventSelection.DataSource = New ArrayList
            dtgBabitEventSelection.VirtualItemCount = 0
            dtgBabitEventSelection.CurrentPageIndex = 0
            dtgBabitEventSelection.DataBind()
        End If
    End Sub

    Private Sub dtgBabitEventSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBabitEventSelection.ItemDataBound
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
        dtgBabitEventSelection.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dtgBabitEventSelection.CurrentPageIndex)  '-- Bind page-1
        If dtgBabitEventSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgBabitEventSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgBabitEventSelection.SortCommand

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

        dtgBabitEventSelection.CurrentPageIndex = 0
        ReadData()
        BindGrid(dtgBabitEventSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgBabitEventSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgBabitEventSelection.PageIndexChanged

        ReadData()
        dtgBabitEventSelection.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

#End Region

#Region " Custom Method "

    Private Sub ClearData()
        Me.txtBabitRegNumber.Text = String.Empty
        Me.txtBabitDealerNumber.Text = String.Empty
        Me.icBabitEventDateFrom.Value = Date.Now
        Me.icBabitEventDateTo.Value = Date.Now
        chkTanggal.Checked = False
        dtgBabitEventSelection.DataSource = New ArrayList
        dtgBabitEventSelection.DataBind()
    End Sub

#End Region

End Class
