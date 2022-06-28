Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports OfficeOpenXml

Public Class MasterCityEvent
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private deletePriv As Boolean
    Private oDealer As Dealer
    Private SessionGridDataNationalEvent As String = "MasterTypeEvent.NationalEvent"
    Private SessionCriteriaProposalEvent As String = "MasterTypeEvent.CriteriaEventType"

    Private Sub PageInit()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If (Not IsPostBack) Then
            ViewState("currSortColumn") = "City.CityName"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            PageInit()
            BindddlCity()
            BindddlStatus()
            '-- Restore selection criteria
            'ReadCriteria()

            ReadData()   '-- Read all data matching criteria
            BindGrid(dgEventCity.CurrentPageIndex)  '-- Bind page-1

            trMultiCity.Visible = False

            lblSearchKota.Attributes("onclick") = "ShowPopUpMultipleCity();"
        End If
    End Sub

    Private Sub BindddlCity()
        ddlKota.Items.Clear()
        ddlKota.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        With ddlKota.Items
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(City), "CityName", Sort.SortDirection.ASC))
            Dim arrCity As ArrayList = New KTB.DNet.BusinessFacade.General.CityFacade(User).Retrieve(crits, sortColl)
            For Each _neCity As City In arrCity
                .Add(New ListItem(_neCity.CityName, _neCity.ID))
            Next
        End With
    End Sub

    Private Sub BindddlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Tidak Aktif", 0))
        ddlStatus.Items.Add(New ListItem("Aktif", 1))
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silakan Piih ", -1))
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrNationalEventList As ArrayList = CType(sesHelper.GetSession(SessionGridDataNationalEvent), ArrayList)
        If arrNationalEventList.Count <> 0 Then
            CommonFunction.SortListControl(arrNationalEventList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrNationalEventList, pageIndex, dgEventCity.PageSize)
            dgEventCity.DataSource = PagedList
            dgEventCity.VirtualItemCount = arrNationalEventList.Count()
            dgEventCity.DataBind()
        Else
            dgEventCity.DataSource = New ArrayList
            dgEventCity.VirtualItemCount = 0
            dgEventCity.CurrentPageIndex = 0
            dgEventCity.DataBind()
        End If
    End Sub

    Protected Sub dgEventCity_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEventCity.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblKodeKota As Label = CType(e.Item.FindControl("lblKodeKota"), Label)
        Dim lblNamaKota As Label = CType(e.Item.FindControl("lblNamaKota"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim imgActif As HtmlImage = CType(e.Item.FindControl("imgActif"), HtmlImage)
        Dim imgNonActif As HtmlImage = CType(e.Item.FindControl("imgNonActif"), HtmlImage)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As NationalEventCity = CType(e.Item.DataItem, NationalEventCity)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgEventCity.PageSize * dgEventCity.CurrentPageIndex)).ToString
            lblKodeKota.Text = oData.City.CityCode
            lblNamaKota.Text = oData.City.CityName
            lblStatus.Text = oData.Status.Trim

            If lblStatus.Text = "0" Then
                lblStatus.Text = "Tidak Aktif"
                imgActif.Visible = False
                imgNonActif.Visible = True
            Else
                imgActif.Visible = True
                imgNonActif.Visible = False
                lblStatus.Text = "Aktif"
            End If

            Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
        End If
    End Sub

    'Private Sub ReadCriteria()
    '    Dim crit As Hashtable
    '    crit = CType(sesHelper.GetSession(SessionCriteriaProposalEvent), Hashtable)
    '    If Not crit Is Nothing Then
    '        txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
    '        lblKodeDealer.Text = CStr(crit.Item("DealerCode"))
    '        txtKodeEvent.Text = CStr(crit.Item("DealerBranchCode"))
    '        cbDate.Checked = CBool(crit.Item("cbDate"))
    '        icPeriodeStart.Value = CStr(crit.Item("PeriodStart"))
    '        icPeriodeEnd.Value = CStr(crit.Item("PeriodEnd"))
    '        lblKodeKota.Text = CStr(crit.Item("KodeKota"))

    '        ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
    '        ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
    '        dgNationalEvent.CurrentPageIndex = CInt(crit.Item("PageIndex"))
    '    End If
    'End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("City.ID", ddlKota.Text)
        crit.Add("Status", ddlStatus.Text.Trim)

        crit.Add("PageIndex", dgEventCity.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession(SessionCriteriaProposalEvent, crit) '-- Store in session
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlKota.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(NationalEventCity), "City.ID", MatchType.Exact, ddlKota.SelectedValue))
        End If

        If ddlStatus.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(NationalEventCity), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(NationalEventCity), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrNationalEventList As ArrayList = New NationalEventCityFacade(User).RetrieveByCriteria(crit, sortColl)

        sesHelper.SetSession(SessionGridDataNationalEvent, arrNationalEventList)
        If arrNationalEventList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ReadData()
        'dgNationalEvent.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dgEventCity.CurrentPageIndex)  '-- Bind page-1
        StoreCriteria()
    End Sub

    Protected Sub dgEventCity_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEventCity.ItemCommand
        Select Case e.CommandName
            Case "Detail"

            Case "Edit"
                hdnNationalEventID.Value = CInt(e.Item.Cells(0).Text)
                Dim objEventCity As NationalEventCity = New NationalEventCityFacade(User).Retrieve(CInt(hdnNationalEventID.Value))
                ddlKota.SelectedValue = objEventCity.City.ID
                ddlStatus.SelectedIndex = CInt(objEventCity.Status.Trim) + 1
                ddlKota.Enabled = False
            Case "Delete"
                btnDelete_Click(CInt(e.Item.Cells(0).Text))
        End Select
    End Sub

    Private Sub ClearAll()
        hdnNationalEventID.Value = 0
        ddlKota.SelectedIndex = -1
        ddlStatus.SelectedIndex = -1
    End Sub

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If (ddlKota.SelectedValue = "-1") Then
            sb.Append("Kota Harus Diisi\n")
        End If

        If (ddlStatus.SelectedValue = "-1") Then
            sb.Append("Field status harus dipilih\n")
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(NationalEventCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(NationalEventCity), "City.ID", MatchType.Exact, ddlKota.SelectedValue))
        Dim arrNationalEventCity As ArrayList = New NationalEventCityFacade(User).Retrieve(criterias)
        If arrNationalEventCity.Count > 0 Then
            If hdnNationalEventID.Value = 0 Then
                sb.Append("Event City ini sudah ada. Silahkan masukan Event City yang baru\n")
            End If
        End If

        Return sb.ToString()
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If

        Dim _MasterEventCity As NationalEventCity
        _MasterEventCity = New NationalEventCity
        _MasterEventCity.City = New KTB.DNet.BusinessFacade.General.CityFacade(User).Retrieve(CInt(Me.ddlKota.SelectedValue))
        _MasterEventCity.Status = Me.ddlStatus.SelectedValue

        Dim _result As Integer = 0
        If hdnNationalEventID.Value > 0 Then
            _MasterEventCity.ID = CInt(hdnNationalEventID.Value)
            _result = New NationalEventCityFacade(User).Update(_MasterEventCity)
        Else
            _result = New NationalEventCityFacade(User).Insert(_MasterEventCity)
        End If

        If _result > 0 Then
            ClearAll()
            ddlKota.Enabled = True
            MessageBox.Show("Simpan Data Berhasil !")
        Else
            MessageBox.Show("Simpan Data Gagal")
        End If

        ReadData()
        BindGrid(dgEventCity.CurrentPageIndex)

    End Sub

    Private Sub btnDelete_Click(ByVal ID As Integer)
        '----- Proses Validasi Delete

        Dim criterias As New CriteriaComposite(New Criteria(GetType(NationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(NationalEvent), "NationalEventCity.ID", MatchType.Exact, ID))

        Dim arrNationalEvent As ArrayList = New NationalEventFacade(User).Retrieve(criterias)
        Dim objNationalEventCityFacade As NationalEventCityFacade = New NationalEventCityFacade(User)
        Dim objNationalEventCity As NationalEventCity = objNationalEventCityFacade.Retrieve(CType(ID, Integer))

        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(NationalEventVenue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(NationalEventVenue), "City.ID", MatchType.Exact, objNationalEventCity.City.ID))

        Dim arrNationalEventVenue As ArrayList = New NationalEventVenueFacade(User).Retrieve(criterias2)
        If Not IsNothing(arrNationalEvent) AndAlso arrNationalEvent.Count > 0 Then
            MessageBox.Show("Event City ini sudah dipakai di Daftar National Event")
            Exit Sub
        ElseIf Not IsNothing(arrNationalEventVenue) AndAlso arrNationalEventVenue.Count > 0 Then
            MessageBox.Show("Event City ini sudah dipakai di National Event Venue")
            Exit Sub
        Else
            objNationalEventCity.RowStatus = -1
            objNationalEventCityFacade.Update(objNationalEventCity)
            ClearAll()
            ReadData()
            BindGrid(dgEventCity.CurrentPageIndex)
            Exit Sub
        End If

        'Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias2.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.ID", MatchType.Exact, Me.hdnBabitMasterEventTypeID.Value))
        'Dim arrBabitParameterHeader As ArrayList = New BabitParameterHeaderFacade(User).Retrieve(criterias2)
        'If Not IsNothing(arrBabitParameterHeader) AndAlso arrBabitParameterHeader.Count > 0 Then
        '    MessageBox.Show("Kode Kegiatan ini sudah dipakai di Daftar Parameter Babit")
        '    Exit Sub
        'End If
        ''--------------------------

        'Dim objBabitMasterEventTypeFacade As BabitMasterEventTypeFacade = New BabitMasterEventTypeFacade(User)
        'Dim objBabitMasterEventType As BabitMasterEventType = objBabitMasterEventTypeFacade.Retrieve(CType(Me.hdnBabitMasterEventTypeID.Value, Integer))
        'objBabitMasterEventType.RowStatus = -1
        'objBabitMasterEventTypeFacade.Update(objBabitMasterEventType)

        'Response.Redirect("~/Babit/FrmInputMasterEventType.aspx")
    End Sub
End Class