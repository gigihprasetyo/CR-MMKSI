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

Public Class PopUpEventNational
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents dtgNationalEventSelection As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents txtEventName As System.Web.UI.WebControls.TextBox
    'Protected WithEvents ddlEventCategory As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents icEventDateFrom As KTB.DNet.WebCC.IntiCalendar
    'Protected WithEvents icEventDateTo As KTB.DNet.WebCC.IntiCalendar
    'Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    'Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    'Protected WithEvents chkTanggal As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property

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
        Me.ddlCityPopUp.SelectedValue = String.Empty
        Me.ddlVenuePopUp.SelectedValue = String.Empty
        Me.icEventDateFrom.Value = Date.Now
        Me.icEventDateTo.Value = Date.Now
        chkTanggal.Checked = False
        dtgNationalEventSelection.DataSource = New ArrayList
        dtgNationalEventSelection.DataBind()
    End Sub

#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            BindddlCityPopUp()
            BindddlVenuePopUp(ddlCityPopUp.SelectedValue)

            'Added by Ery for sorting
            ViewState.Add("SortColNationalEvent", "CreatedTime")
            ViewState.Add("SortDirNationalEvent", Sort.SortDirection.DESC)

            objDealer = Session("DEALER")
            ClearData()
            'BindEventCategory()
        End If
    End Sub

    Private Sub ddlCityPopUp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCityPopUp.SelectedIndexChanged
        If ddlCityPopUp.SelectedIndex = 0 Then ddlVenuePopUp.SelectedIndex = 0
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(NationalEventCity), "ID", MatchType.Exact, ddlCityPopUp.SelectedValue))
        Dim arrCity As ArrayList = New NationalEventCityFacade(User).Retrieve(crits)
        Dim objNationalEventCity As New NationalEventCity
        If arrCity.Count > 0 Then
            objNationalEventCity = CType(arrCity(0), NationalEventCity)
            BindddlVenuePopUp(objNationalEventCity.City.ID)
        End If
    End Sub

    Private Sub BindddlCityPopUp()
        ddlCityPopUp.Items.Clear()
        ddlCityPopUp.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        With ddlCityPopUp.Items
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim arrCity As ArrayList = New NationalEventCityFacade(User).Retrieve(crits)
            For Each _neCity As NationalEventCity In arrCity
                .Add(New ListItem(_neCity.City.CityName, _neCity.ID))
            Next
        End With
    End Sub

    Private Sub BindddlVenuePopUp(ByVal intCityID As Integer)
        ddlVenuePopUp.Items.Clear()
        ddlVenuePopUp.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        With ddlVenuePopUp.Items
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventVenue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(NationalEventVenue), "City.ID", MatchType.Exact, intCityID))
            Dim arrVenue As ArrayList = New NationalEventVenueFacade(User).Retrieve(crits)
            For Each _neVenue As NationalEventVenue In arrVenue
                .Add(New ListItem(_neVenue.VenueName, _neVenue.ID))
            Next
        End With
    End Sub

    Public Sub BindSearch()
        objDealer = Session("DEALER")

        Dim criterias As New CriteriaComposite(New Criteria(GetType(NationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not ddlCityPopUp.SelectedIndex = 0 Then
            criterias.opAnd(New Criteria(GetType(NationalEvent), "NationalEventCity.ID", MatchType.Exact, ddlCityPopUp.SelectedValue))
        End If

        If Not ddlVenuePopUp.SelectedIndex = 0 Then
            criterias.opAnd(New Criteria(GetType(NationalEvent), "NationalEventVenue.ID", MatchType.Exact, ddlVenuePopUp.SelectedValue))
        End If

        Dim strSql As String = String.Empty
        If chkTanggal.Checked = True Then
            strSql = "Select ID from NationalEvent "
            strSql += "where (PeriodStart >= '" & Format(icEventDateFrom.Value, "yyyy/MM/dd") & "' and PeriodStart <= '" & Format(icEventDateTo.Value, "yyyy/MM/dd") & "') "
            strSql += "or (PeriodEnd >= '" & Format(icEventDateFrom.Value, "yyyy/MM/dd") & "' and PeriodEnd <= '" & Format(icEventDateTo.Value, "yyyy/MM/dd") & "') "
            strSql += "or (PeriodStart >= '" & Format(icEventDateFrom.Value, "yyyy/MM/dd") & "' and PeriodEnd <= '" & Format(icEventDateTo.Value, "yyyy/MM/dd") & "') "
            strSql += "or (PeriodStart <= '" & Format(icEventDateFrom.Value, "yyyy/MM/dd") & "' and PeriodEnd >= '" & Format(icEventDateTo.Value, "yyyy/MM/dd") & "') "
            strSql += "and RowStatus=0"
            criterias.opAnd(New Criteria(GetType(NationalEvent), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        'strSql = "Select NationalEventHeaderID from NationalEventDetail where DealerID = " & objDealer.ID
        'criterias.opAnd(New Criteria(GetType(NationalEventHeader), "ID", MatchType.InSet, "(" & strSql & ")"))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(NationalEvent), ViewState("SortColNationalEvent").ToString(), [Enum].Parse(GetType(Sort.SortDirection), ViewState("SortDirNationalEvent").ToString())))

        Dim arrNationalEventList As ArrayList = New NationalEventFacade(User).RetrieveByCriteria(criterias, sortColl)
        Dim arrNationalEventListFix As ArrayList = New ArrayList
        If IsLoginAsDealer() Then
            For Each arrNationalEvent As NationalEvent In arrNationalEventList
                Dim arrDealerCityID() As String = arrNationalEvent.DealerCityID.Split(";")
                Dim findDealer = Array.Find(arrDealerCityID, Function(s) s = SesDealer().City.ID)
                If findDealer = SesDealer().City.ID Then
                    arrNationalEventListFix.Add(arrNationalEvent)
                End If
            Next
        Else
            arrNationalEventListFix.AddRange(arrNationalEventList)
        End If

        If IsNothing(arrNationalEventListFix) Then arrNationalEventListFix = New ArrayList
        dtgNationalEventSelection.DataSource = arrNationalEventListFix
        dtgNationalEventSelection.DataBind()
    End Sub

    Private Sub dtgNationalEventSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgNationalEventSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As NationalEvent = CType(e.Item.DataItem, NationalEvent)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()
        If dtgNationalEventSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

#End Region

    Private Sub dtgNationalEventSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgNationalEventSelection.SortCommand
        If e.SortExpression = ViewState("SortColNationalEvent") Then
            If ViewState("SortDirNationalEvent") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirNationalEvent", Sort.SortDirection.ASC)
            Else
                ViewState.Add("SortDirNationalEvent", Sort.SortDirection.DESC)
            End If
        End If
        ViewState.Add("SortColNationalEvent", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgNationalEventSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgNationalEventSelection.PageIndexChanged
        dtgNationalEventSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function
End Class