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

Public Class MasterVenueEvent
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
            ViewState("currSortColumn") = "VenueName"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            PageInit()
            BindddlCity()
            BindddlStatus()
            '-- Restore selection criteria
            'ReadCriteria()

            ReadData()   '-- Read all data matching criteria
            BindGrid(dgEventVenue.CurrentPageIndex)  '-- Bind page-1
        End If
    End Sub

    Private Sub BindddlCity()
        ddlKota.Items.Clear()
        ddlKota.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        With ddlKota.Items
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(NationalEventCity), "City.CityName", Sort.SortDirection.ASC))
            Dim arrCity As ArrayList = New NationalEventCityFacade(User).Retrieve(crits, sortColl)
            For Each _neCity As NationalEventCity In arrCity
                .Add(New ListItem(_neCity.City.CityName, _neCity.City.ID))
            Next
        End With
    End Sub

    Sub BindddlStatus()
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
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrNationalEventList, pageIndex, dgEventVenue.PageSize)
            dgEventVenue.DataSource = PagedList
            dgEventVenue.VirtualItemCount = arrNationalEventList.Count()
            dgEventVenue.DataBind()
        Else
            dgEventVenue.DataSource = New ArrayList
            dgEventVenue.VirtualItemCount = 0
            dgEventVenue.CurrentPageIndex = 0
            dgEventVenue.DataBind()
        End If
    End Sub

    Protected Sub dgEventVenue_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEventVenue.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblKota As Label = CType(e.Item.FindControl("lblKota"), Label)
        Dim lblNamaVenue As Label = CType(e.Item.FindControl("lblNamaVenue"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim imgActif As HtmlImage = CType(e.Item.FindControl("imgActif"), HtmlImage)
        Dim imgNonActif As HtmlImage = CType(e.Item.FindControl("imgNonActif"), HtmlImage)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As NationalEventVenue = CType(e.Item.DataItem, NationalEventVenue)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgEventVenue.PageSize * dgEventVenue.CurrentPageIndex)).ToString
            lblKota.Text = oData.City.CityName
            lblNamaVenue.Text = oData.VenueName
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
    '        lblKota.Text = CStr(crit.Item("Kota"))

    '        ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
    '        ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
    '        dgNationalEvent.CurrentPageIndex = CInt(crit.Item("PageIndex"))
    '    End If
    'End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("City.CityName", ddlKota.Text)
        crit.Add("Description", txtNamaVenue.Text)
        crit.Add("Status", ddlStatus.Text.Trim)

        crit.Add("PageIndex", dgEventVenue.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession(SessionCriteriaProposalEvent, crit) '-- Store in session
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventVenue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlKota.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(NationalEventVenue), "City.ID", MatchType.Exact, ddlKota.SelectedValue))
        End If

        If txtNamaVenue.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(NationalEventVenue), "VenueName", MatchType.Partial, txtNamaVenue.Text.Trim))
        End If

        If ddlStatus.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(NationalEventVenue), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(NationalEventVenue), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrNationalEventList As ArrayList = New NationalEventVenueFacade(User).RetrieveByCriteria(crit, sortColl)

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
        BindGrid(dgEventVenue.CurrentPageIndex)  '-- Bind page-1
        StoreCriteria()
    End Sub

    Protected Sub dgEventVenue_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEventVenue.ItemCommand
        Select Case e.CommandName
            Case "Detail"

            Case "Edit"
                hdnNationalEventID.Value = CInt(e.Item.Cells(0).Text)
                Dim objEventVenue As NationalEventVenue = New NationalEventVenueFacade(User).Retrieve(CInt(hdnNationalEventID.Value))
                ddlKota.SelectedValue = objEventVenue.City.ID
                txtNamaVenue.Text = objEventVenue.VenueName
                ddlStatus.SelectedIndex = CInt(objEventVenue.Status.Trim) + 1
                ddlKota.Enabled = False
            Case "Delete"
                btnDelete_Click(CInt(e.Item.Cells(0).Text))
        End Select
    End Sub

    Private Sub ClearAll()
        hdnNationalEventID.Value = 0
        ddlKota.SelectedValue = -1
        txtNamaVenue.Text = ""
        ddlStatus.SelectedIndex = -1
    End Sub

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If (txtNamaVenue.Text.Trim = String.Empty) Then
            sb.Append("Nama Venue Harus Diisi\n")
        End If

        If (ddlKota.SelectedValue = "-1") Then
            sb.Append("Kota Harus Diisi\n")
        End If

        If (ddlStatus.SelectedValue = "-1") Then
            sb.Append("Field status harus dipilih\n")
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(NationalEventVenue), "VenueName", MatchType.Exact, Me.txtNamaVenue.Text))
        criterias.opAnd(New Criteria(GetType(NationalEventVenue), "City.ID", MatchType.Exact, Me.ddlKota.SelectedValue))
        Dim arrNationalEventVenue As ArrayList = New NationalEventVenueFacade(User).Retrieve(criterias)
        If arrNationalEventVenue.Count > 0 Then
            If hdnNationalEventID.Value = 0 Then
                sb.Append("Event Venue ini sudah ada. Silahkan masukan Event Venue yang baru\n")
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

        Dim _MasterEventVenue As NationalEventVenue
        _MasterEventVenue = New NationalEventVenue
        _MasterEventVenue.City = New KTB.DNet.BusinessFacade.General.CityFacade(User).Retrieve(CInt(Me.ddlKota.SelectedValue))
        _MasterEventVenue.VenueName = Me.txtNamaVenue.Text
        _MasterEventVenue.Status = Me.ddlStatus.SelectedValue

        Dim _result As Integer = 0
        If hdnNationalEventID.Value > 0 Then
            _MasterEventVenue.ID = CInt(hdnNationalEventID.Value)
            _result = New NationalEventVenueFacade(User).Update(_MasterEventVenue)
        Else
            _MasterEventVenue.RowStatus = 0
            _result = New NationalEventVenueFacade(User).Insert(_MasterEventVenue)
        End If

        If _result > 0 Then
            ClearAll()
            ddlKota.Enabled = True
            MessageBox.Show("Simpan Data Berhasil !")
        Else
            MessageBox.Show("Simpan Data Gagal")
        End If

        ReadData()
        BindGrid(dgEventVenue.CurrentPageIndex)
    End Sub

    Private Sub btnDelete_Click(ByVal ID As Integer)
        '----- Proses Validasi Delete
        Dim criterias As New CriteriaComposite(New Criteria(GetType(NationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(NationalEvent), "NationalEventVenue.ID", MatchType.Exact, ID))
        Dim arrEventType As ArrayList = New NationalEventFacade(User).Retrieve(criterias)
        If Not IsNothing(arrEventType) AndAlso arrEventType.Count > 0 Then
            MessageBox.Show("Event Venue ini sudah dipakai di Daftar National Event")
            Exit Sub
        Else
            Dim objNationalEventVenueFacade As NationalEventVenueFacade = New NationalEventVenueFacade(User)
            Dim objNationalEventVenue As NationalEventVenue = objNationalEventVenueFacade.Retrieve(ID)
            objNationalEventVenue.RowStatus = -1
            objNationalEventVenueFacade.Update(objNationalEventVenue)
            ClearAll()
            ReadData()
            BindGrid(dgEventVenue.CurrentPageIndex)
            Exit Sub
        End If

    End Sub
End Class