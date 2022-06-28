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

Public Class MasterTypeEvent
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
            ViewState("currSortColumn") = "Name"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            PageInit()
            BindddlStatus()
            '-- Restore selection criteria
            'ReadCriteria()

            ReadData()   '-- Read all data matching criteria
            BindGrid(dgEventType.CurrentPageIndex)  '-- Bind page-1
        End If
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
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrNationalEventList, pageIndex, dgEventType.PageSize)
            dgEventType.DataSource = PagedList
            dgEventType.VirtualItemCount = arrNationalEventList.Count()
            dgEventType.DataBind()
        Else
            dgEventType.DataSource = New ArrayList
            dgEventType.VirtualItemCount = 0
            dgEventType.CurrentPageIndex = 0
            dgEventType.DataBind()
        End If
    End Sub

    Protected Sub dgEventType_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEventType.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblNamaEvent As Label = CType(e.Item.FindControl("lblNamaEvent"), Label)
        Dim lblDeskripsi As Label = CType(e.Item.FindControl("lblDeskripsi"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim imgActif As HtmlImage = CType(e.Item.FindControl("imgActif"), HtmlImage)
        Dim imgNonActif As HtmlImage = CType(e.Item.FindControl("imgNonActif"), HtmlImage)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As NationalEventType = CType(e.Item.DataItem, NationalEventType)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgEventType.PageSize * dgEventType.CurrentPageIndex)).ToString
            lblNamaEvent.Text = oData.Name
            lblDeskripsi.Text = oData.Description
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
    '        lblNamaEvent.Text = CStr(crit.Item("NamaEvent"))

    '        ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
    '        ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
    '        dgNationalEvent.CurrentPageIndex = CInt(crit.Item("PageIndex"))
    '    End If
    'End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("Name", txtNamaEvent.Text)
        crit.Add("Description", txtDeskripsi.Text)
        crit.Add("Status", ddlStatus.Text.Trim)

        crit.Add("PageIndex", dgEventType.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession(SessionCriteriaProposalEvent, crit) '-- Store in session
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtNamaEvent.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(NationalEventType), "Name", MatchType.Partial, txtNamaEvent.Text.Trim))
        End If

        If txtDeskripsi.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(NationalEventType), "Description", MatchType.Partial, txtDeskripsi.Text.Trim))
        End If

        If ddlStatus.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(NationalEventType), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(NationalEventType), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrNationalEventList As ArrayList = New NationalEventTypeFacade(User).RetrieveByCriteria(crit, sortColl)        

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
        BindGrid(dgEventType.CurrentPageIndex)  '-- Bind page-1
        StoreCriteria()
    End Sub

    Protected Sub dgEventType_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEventType.ItemCommand
        Select Case e.CommandName
            Case "Detail"

            Case "Edit"
                hdnNationalEventID.Value = CInt(e.Item.Cells(0).Text)
                Dim objEventType As NationalEventType = New NationalEventTypeFacade(User).Retrieve(CInt(hdnNationalEventID.Value))
                txtNamaEvent.Text = objEventType.Name
                txtDeskripsi.Text = objEventType.Description
                ddlStatus.SelectedIndex = CInt(objEventType.Status.Trim) + 1
            Case "Delete"
                btnDelete_Click(CInt(e.Item.Cells(0).Text))
        End Select
    End Sub

    Private Sub ClearAll()
        hdnNationalEventID.Value = 0
        txtNamaEvent.Text = ""
        txtDeskripsi.Text = ""
        ddlStatus.SelectedIndex = -1
    End Sub

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If (txtNamaEvent.Text.Trim = String.Empty) Then
            sb.Append("Nama Event harus diisi\n")
        End If

        If (txtDeskripsi.Text = String.Empty) Then
            sb.Append("Deskripsi harus diisi\n")
        End If

        If (ddlStatus.SelectedValue = "-1") Then
            sb.Append("Field status harus dipilih\n")
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(NationalEventType), "Name", MatchType.Exact, Me.txtNamaEvent.Text.Trim))
        criterias.opAnd(New Criteria(GetType(NationalEventType), "Description", MatchType.Exact, Me.txtDeskripsi.Text))
        Dim arrNationalEventType As ArrayList = New NationalEventTypeFacade(User).Retrieve(criterias)
        If arrNationalEventType.Count > 0 Then
            If hdnNationalEventID.Value = 0 Then
                sb.Append("Event Type ini sudah ada. Silahkan masukan Event Type yang baru\n")
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

        Dim _MasterEventType As NationalEventType
        _MasterEventType = New NationalEventType
        _MasterEventType.Name = Me.txtNamaEvent.Text.Trim
        _MasterEventType.Description = Me.txtDeskripsi.Text
        _MasterEventType.Status = Me.ddlStatus.SelectedValue

        Dim _result As Integer = 0
        If hdnNationalEventID.Value > 0 Then
            _MasterEventType.ID = CInt(hdnNationalEventID.Value)
            _result = New NationalEventTypeFacade(User).Update(_MasterEventType)
        Else
            _MasterEventType.RowStatus = 0
            _result = New NationalEventTypeFacade(User).Insert(_MasterEventType)
        End If

        If _result > 0 Then
            ClearAll()
            MessageBox.Show("Simpan Data Berhasil !")
        Else
            MessageBox.Show("Simpan Data Gagal")
        End If

        ReadData()
        BindGrid(dgEventType.CurrentPageIndex)

    End Sub

    Private Sub btnDelete_Click(ByVal ID As Integer)
        '----- Proses Validasi Delete
        Dim criterias As New CriteriaComposite(New Criteria(GetType(NationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(NationalEvent), "NationalEventType.ID", MatchType.Exact, ID))
        Dim arrEventType As ArrayList = New NationalEventFacade(User).Retrieve(criterias)
        If Not IsNothing(arrEventType) AndAlso arrEventType.Count > 0 Then
            MessageBox.Show("Event Type ini sudah dipakai di Daftar National Event")
            Exit Sub
        Else
            Dim objNationalEventTypeFacade As NationalEventTypeFacade = New NationalEventTypeFacade(User)
            Dim objNationalEventType As NationalEventType = objNationalEventTypeFacade.Retrieve(ID)
            objNationalEventType.RowStatus = -1
            objNationalEventTypeFacade.Update(objNationalEventType)
            ClearAll()
            ReadData()
            BindGrid(dgEventType.CurrentPageIndex)
            Exit Sub
        End If

    End Sub
End Class