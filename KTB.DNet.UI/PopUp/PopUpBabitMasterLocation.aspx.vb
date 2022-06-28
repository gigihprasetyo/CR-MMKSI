#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
#End Region

Public Class PopUpBabitMasterLocation
    Inherits System.Web.UI.Page

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#Region " custom Declaration "
    Protected countChk As Integer = 0
    Private objDealer As Dealer
    Private sessionHelper As New SessionHelper
#End Region

#Region " Custom Method "

    Private Sub ClearData()
        Me.txtLocationName.Text = String.Empty
    End Sub

    'Private Sub BindddlProvince()
    '    ddlProvince.Items.Clear()
    '    ddlProvince.Items.Add(New ListItem("Silahkan Pilih", -1))

    '    Dim arlProvince As ArrayList = New ProvinceFacade(User).RetrieveActiveList()
    '    For Each prov As Province In arlProvince
    '        ddlProvince.Items.Add(New ListItem(prov.ProvinceName, prov.ID))
    '    Next
    'End Sub

    'Private Sub BindddlCity()
    '    ddlCity.Items.Clear()
    '    ddlCity.Items.Add(New ListItem("Silahkan Pilih", -1))
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "Province.ID", MatchType.Exact, ddlProvince.SelectedValue))

    '    Dim arlCity As ArrayList = New CityFacade(User).Retrieve(criterias)
    '    For Each c As City In arlCity
    '        ddlCity.Items.Add(New ListItem(c.CityName, c.ID))
    '    Next
    'End Sub

    Private Sub BindSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitMasterLocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitMasterLocation), "Status", MatchType.Exact, 1))
        If txtLocationName.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(BabitMasterLocation), "LocationName", MatchType.Partial, txtLocationName.Text))
        End If

        objDealer = CType(SessionHelper.GetSession("DEALER"), Dealer)
        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            Dim crtSpecialCity As New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtSpecialCity.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
            crtSpecialCity.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objDealer.City.ID))

            Dim arrSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(crtSpecialCity)

            Dim strCityID As String = String.Empty
            If arrSpecialCity.Count > 0 Then
                Dim oBabitSpecialCity As BabitSpecialCity = CType(arrSpecialCity(0), BabitSpecialCity)
                Dim crtSpecialCity2 As New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crtSpecialCity2.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
                crtSpecialCity2.opAnd(New Criteria(GetType(BabitSpecialCity), "BabitSpecialProvince.ID", MatchType.Exact, oBabitSpecialCity.BabitSpecialProvince.ID))

                Dim arrSpecialCity2 As ArrayList = New BabitSpecialCityFacade(User).Retrieve(crtSpecialCity2)

                For Each item As BabitSpecialCity In arrSpecialCity2
                    strCityID += item.City.ID.ToString() + ","
                Next
            End If

            If strCityID = String.Empty Then
                criterias.opAnd(New Criteria(GetType(BabitMasterLocation), "City.Province.ID", MatchType.Exact, objDealer.City.Province.ID))
            Else
                strCityID = strCityID.Substring(0, strCityID.Length - 1)
                criterias.opAnd(New Criteria(GetType(BabitMasterLocation), "City.ID", MatchType.InSet, "(" + strCityID + ")"))
            End If
        End If
        'If ddlProvince.SelectedValue <> "-1" Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitMasterLocation), "City.Province.ID", MatchType.Exact, ddlProvince.SelectedValue))

        '    If ddlCity.SelectedValue <> "-1" Then
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitMasterLocation), "City.ID", MatchType.Exact, ddlCity.SelectedValue))
        '    End If
        'End If
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitMasterLocation), "LocationName", Sort.SortDirection.ASC))

        dtgLocationSelection.DataSource = New BabitMasterLocationFacade(User).Retrieve(criterias, sortColl)
        dtgLocationSelection.DataBind()
    End Sub

#End Region

#Region "Event"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            'BindddlProvince()

            ClearData()
        Else
            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub

    Protected Sub dtgLocationSelection_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgLocationSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As BabitMasterLocation = CType(e.Item.DataItem, BabitMasterLocation)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)

            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindSearch()

        If dtgLocationSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Protected Sub dtgLocationSelection_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgLocationSelection.PageIndexChanged
        dtgLocationSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub
#End Region

    'Protected Sub ddlProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvince.SelectedIndexChanged
    '    BindddlCity()
    'End Sub

    Private Sub dtgLocationSelection_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgLocationSelection.SortCommand

    End Sub
End Class