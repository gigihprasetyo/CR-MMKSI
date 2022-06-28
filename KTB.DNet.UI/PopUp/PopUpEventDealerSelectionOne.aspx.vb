#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade

#End Region

Public Class PopUpEventDealerSelectionOne
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgEventDealerSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtEventName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlEventCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icEventDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEventDateTo As KTB.DNet.WebCC.IntiCalendar
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
    Private objDealer As Dealer
#End Region

#Region " Custom Method "

    Private Sub ClearData()
        Me.txtEventName.Text = String.Empty
        Me.ddlEventCategory.SelectedValue = String.Empty
        Me.icEventDateFrom.Value = Date.Now
        Me.icEventDateTo.Value = Date.Now
        chkTanggal.Checked = False
        dtgEventDealerSelection.DataSource = New ArrayList
        dtgEventDealerSelection.DataBind()
    End Sub

#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            'Added by Ery for sorting
            ViewState.Add("SortColEventDealer", "EventName")
            ViewState.Add("SortDirEventDealer", Sort.SortDirection.ASC)

            objDealer = Session("DEALER")
            ClearData()
            BindEventCategory()
        End If
    End Sub

    Private Sub BindEventCategory()
        ddlEventCategory.Items.Clear()
        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(Category), "ID", Sort.SortDirection.ASC))
        Dim arrCategory As ArrayList = New CategoryFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each objCategory As Category In arrCategory
            li = New ListItem(objCategory.CategoryCode, objCategory.ID.ToString)
            ddlEventCategory.Items.Add(li)
        Next

        li = New ListItem("Silahkan pilih", "0")
        ddlEventCategory.Items.Insert(0, li)
        ddlEventCategory.SelectedIndex = 0
    End Sub

    Public Sub BindSearch()
        objDealer = Session("DEALER")

        Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDealerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not txtEventName.Text.Trim = "" Then
            criterias.opAnd(New Criteria(GetType(EventDealerHeader), "EventName", MatchType.[Partial], txtEventName.Text))
        End If
        If Not ddlEventCategory.SelectedIndex = 0 Then
            criterias.opAnd(New Criteria(GetType(EventDealerHeader), "Category.ID", MatchType.Exact, ddlEventCategory.SelectedValue))
        End If

        Dim strSql As String = String.Empty
        If chkTanggal.Checked = True Then
            strSql = "Select ID from EventDealerHeader "
            strSql += "where (PeriodStart >= '" & Format(icEventDateFrom.Value, "yyyy/MM/dd") & "' and PeriodStart <= '" & Format(icEventDateTo.Value, "yyyy/MM/dd") & "') "
            strSql += "or (PeriodEnd >= '" & Format(icEventDateFrom.Value, "yyyy/MM/dd") & "' and PeriodEnd <= '" & Format(icEventDateTo.Value, "yyyy/MM/dd") & "') "
            strSql += "or (PeriodStart >= '" & Format(icEventDateFrom.Value, "yyyy/MM/dd") & "' and PeriodEnd <= '" & Format(icEventDateTo.Value, "yyyy/MM/dd") & "') "
            strSql += "or (PeriodStart <= '" & Format(icEventDateFrom.Value, "yyyy/MM/dd") & "' and PeriodEnd >= '" & Format(icEventDateTo.Value, "yyyy/MM/dd") & "') "
            strSql += "and RowStatus=0"
            criterias.opAnd(New Criteria(GetType(EventDealerHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        strSql = "Select EventDealerHeaderID from EventDealerDetail where DealerID = " & objDealer.ID
        criterias.opAnd(New Criteria(GetType(EventDealerHeader), "ID", MatchType.InSet, "(" & strSql & ")"))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(EventDealerHeader), ViewState("SortColEventDealer").ToString(), [Enum].Parse(GetType(Sort.SortDirection), ViewState("SortDirEventDealer").ToString())))

        Dim arrGrid As ArrayList = New EventDealerHeaderFacade(User).RetrieveActiveList(criterias, sortColl)
        If IsNothing(arrGrid) Then arrGrid = New ArrayList
        dtgEventDealerSelection.DataSource = arrGrid
        dtgEventDealerSelection.DataBind()
    End Sub

    Private Sub dtgEventDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEventDealerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As EventDealerHeader = CType(e.Item.DataItem, EventDealerHeader)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()
        If dtgEventDealerSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

#End Region

    Private Sub dtgEventDealerSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEventDealerSelection.SortCommand
        If e.SortExpression = ViewState("SortColEventDealer") Then
            If ViewState("SortDirEventDealer") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirEventDealer", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirEventDealer", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColEventDealer", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgEventDealerSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEventDealerSelection.PageIndexChanged
        dtgEventDealerSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub

End Class
