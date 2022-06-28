#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.WebCC
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmEventFinishingList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlSalesmanArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbPeriode As System.Web.UI.WebControls.CheckBox
    Protected WithEvents calDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlModel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlNamaKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents tblCategoryModelType As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents dtgEvent As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private epf As EventProposalFacade
    Private eff As EventProposalFileFacade
    Private _objDealer As DEALER
    Dim _sesshelper As New SessionHelper
    Dim _isKTB As Boolean
    Dim EPS As String = "EventProposalSessionFile"
    Dim SDEALER As String = "DEALER"
    Dim CRITS As String = "critseventfinishinglist"
    Private Const DataSourceName As String = "EventFinishingListSource"

#Region "function"

    Private Property GridSortColumn() As String
        Get
            If ViewState("SortColumn") Is Nothing Then
                ViewState("SortColumn") = "ID"
            End If
            Return ViewState("SortColumn")
        End Get
        Set(ByVal Value As String)
            ViewState("SortColumn") = Value
        End Set
    End Property

    Private Property GridSortDirection() As Sort.SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = Sort.SortDirection.ASC
            End If
            Return ViewState("SortDirection")
        End Get
        Set(ByVal Value As Sort.SortDirection)
            ViewState("SortDirection") = Value
        End Set
    End Property

    Private Sub InitAuthorization()
        Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")
        If Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            txtDealerCode.Text = objUserInfo.Dealer.DealerCode
            txtDealerCode.Enabled = False
            lblSearchDealer.Enabled = True
            dtgEvent.Columns(3).Visible = False
            dtgEvent.Columns(4).Visible = False
            lblSearchDealer.Visible = False
        Else
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        End If
    End Sub

    Private Sub FillSalesmanArea()
        ddlSalesmanArea.Items.Clear()
        Dim objArea As New SalesmanAreaFacade(User)
        ddlSalesmanArea.DataSource = objArea.RetrieveActiveList
        ddlSalesmanArea.DataTextField = "AreaDesc"
        ddlSalesmanArea.DataValueField = "ID"
        ddlSalesmanArea.DataBind()
        ddlSalesmanArea.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub

    Private Sub FillJenisKegiatan()
        ddlJenisKegiatan.Items.Clear()
        Dim objActivityType As New ActivityTypeFacade(User)
        ddlJenisKegiatan.DataSource = objActivityType.RetrieveActiveList()
        ddlJenisKegiatan.DataTextField = "ActivityName"
        ddlJenisKegiatan.DataValueField = "ID"
        ddlJenisKegiatan.DataBind()
        ddlJenisKegiatan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub

    Private Sub ShowModelCategoryType(ByVal IsVisible As Boolean)
        tblCategoryModelType.Visible = IsVisible
    End Sub

    Private Sub FillCategory()
        ddlCategory.Items.Clear()
        Dim objCategory As New CategoryFacade(User)
        ddlCategory.DataSource = objCategory.RetrieveActiveList
        ddlCategory.DataTextField = "Description"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub

    Private Sub FillModel(ByVal CategoryID As Integer)
        ddlModel.Items.Clear()
        If CategoryID > -1 Then
            Dim objModel As New VechileModelFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VechileModel), "Category", CategoryID))
            ddlModel.DataSource = objModel.RetrieveList("Description", Sort.SortDirection.ASC, crit)
            ddlModel.DataTextField = "Description"
            ddlModel.DataValueField = "ID"
            ddlModel.DataBind()
            ddlModel.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlModel.Items.Insert(0, New ListItem("Pilih Kategori", -1))
        End If
    End Sub

    Private Sub FillType(ByVal ModelID As Integer)
        ddlType.Items.Clear()
        If ModelID > -1 Then
            Dim objType As New VechileTypeFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", CType(DBRowStatus.Active, Short)))
            Dim sorts As New SortCollection
            sorts.Add(New Sort(GetType(VechileType), "Description", Sort.SortDirection.ASC))
            crit.opAnd(New Criteria(GetType(VechileType), "VechileModel", ModelID))
            ddlType.DataSource = objType.RetrieveByCriteria(crit, sorts)
            ddlType.DataTextField = "Description"
            ddlType.DataValueField = "ID"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlType.Items.Insert(0, New ListItem("Pilih Model", -1))
        End If
    End Sub

    Private Sub FillYear()
        ddlYear.Items.Clear()
        For i As Int32 = Now.Year - 5 To Now.Year + 5
            ddlYear.Items.Add(i)
        Next
        ddlYear.SelectedValue = Now.Year
    End Sub

    Private Sub FillNamaKegiatan()
        Dim objFacade As New EventParameterFacade(User)
        ddlNamaKegiatan.Items.Clear()
        If ddlJenisKegiatan.SelectedValue <> -1 Then
            ddlNamaKegiatan.DataSource = objFacade.RetrieveNamaKegiatan(ddlJenisKegiatan.SelectedValue)
            ddlNamaKegiatan.DataTextField = "EventName"
            ddlNamaKegiatan.DataValueField = "EventName"
            ddlNamaKegiatan.DataBind()
            ddlNamaKegiatan.Items.Insert(0, "Silahkan Pilih")
        Else
            ddlNamaKegiatan.Items.Insert(0, "Pilih Jenis Kegiatan")
        End If
    End Sub

    Private Sub BuiltCriteria()
        Dim objComposite As New CriteriaComposite(New Criteria(GetType(EventProposal), "RowStatus", _
            CType(DBRowStatus.Active, Short)))
        Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")

        'If Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
        '    'objComposite.opAnd(New Criteria(GetType(EventProposal), "EventProposalStatus", _
        '    '    CType(EnumEventProposalStatus.EventProposalStatus.Baru, Byte)))
        'Else
        '    objComposite.opAnd(New Criteria(GetType(EventProposal), "EventProposalStatus", _
        '        CType(EnumEventProposalStatus.EventProposalStatus.Validasi, Byte)))
        'End If

        If txtDealerCode.Text.Length > 0 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "Dealer.DealerCode", MatchType.InSet, _
                String.Format("('{0}')", txtDealerCode.Text.Replace(";", "','"))))
        End If
        If ddlSalesmanArea.SelectedValue <> -1 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.SalesmanArea", _
                ddlSalesmanArea.SelectedValue))
        End If
        If cbPeriode.Checked Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventDateStart", _
                MatchType.GreaterOrEqual, calDari.Value))
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventDateEnd", _
                MatchType.LesserOrEqual, calSampai.Value))
        End If
        If ddlJenisKegiatan.SelectedValue <> -1 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.ActivityType", _
                ddlJenisKegiatan.SelectedValue))
        End If
        If ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Small_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Launching_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Exhibition_Khusus, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Showroom_Event, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Others, Integer) Then
            If ddlCategory.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.Category", ddlCategory.SelectedValue))
            End If
            If ddlModel.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.VechileType.VechileModel", _
                    ddlModel.SelectedValue))
            End If
            If ddlType.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.VechileType", _
                    ddlType.SelectedValue))
            End If
        End If
        If ddlNamaKegiatan.SelectedIndex > 0 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventName", _
                ddlNamaKegiatan.SelectedItem.Text))
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventYear", _
                ddlYear.SelectedValue))
        End If
        _sesshelper.SetSession(CRITS, objComposite)
    End Sub

    Private Sub BindGrid()
        Dim crit As CriteriaComposite = CType(_sesshelper.GetSession(CRITS), CriteriaComposite)

        Dim objFacade As New EventProposalFacade(User)
        Dim arl As ArrayList = objFacade.Retrieve(crit)
        FillTotalCost(arl)
        _sesshelper.SetSession(DataSourceName, arl)
        dtgEvent.DataSource = CommonFunction.SortArraylist(arl, GetType(EventProposal), GridSortColumn, GridSortDirection)
        dtgEvent.DataBind()
        If arl.Count = 0 Then
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub HideColumn()
        If ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Truck_Campaign, Integer) Then
            dtgEvent.Columns(9 + 2).Visible = False
            dtgEvent.Columns(10 + 2).Visible = False
            dtgEvent.Columns(11 + 2).Visible = False
            dtgEvent.Columns(12 + 2).Visible = False
            dtgEvent.Columns(13 + 2).Visible = True
            dtgEvent.Columns(14 + 2).Visible = True
        Else
            dtgEvent.Columns(9 + 2).Visible = True
            dtgEvent.Columns(10 + 2).Visible = True
            dtgEvent.Columns(11 + 2).Visible = True
            dtgEvent.Columns(12 + 2).Visible = True
            dtgEvent.Columns(13 + 2).Visible = False
            dtgEvent.Columns(14 + 2).Visible = False
        End If
    End Sub
    Private Sub FillTotalCost(ByVal target As ArrayList)
        Dim objFacade As New EventProposalFacade(User)
        Dim crit As New CriteriaComposite(New Criteria(GetType(V_EventProposalAgreement), "RowStatus", _
            CType(DBRowStatus.Active, Short)))
        Dim sortColl As New SortCollection
        sortColl.Add(New Sort(GetType(V_EventProposalAgreement), "ID"))
        Dim views As ArrayList = objFacade.RetrieveAgreement(crit, sortColl)
        For Each prop As EventProposal In target
            For Each view As V_EventProposalAgreement In views
                If prop.ID = view.ID Then
                    prop.TotalCost = view.TotalCost
                    Exit For
                End If
            Next
        Next
    End Sub

#End Region

#Region "event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        eff = New EventProposalFileFacade(User)
        epf = New EventProposalFacade(User)
        _objDealer = _sesshelper.GetSession("DEALER")
        If (_objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            _isKTB = True
        ElseIf (_objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            _isKTB = False
            dtgEvent.Columns(3).Visible = False
            dtgEvent.Columns(4).Visible = False
            'dtgEvent.Columns(11 + 2).Visible = False
        End If

        If IsPostBack Then Return

        InitAuthorization()
        FillSalesmanArea()
        FillJenisKegiatan()
        FillNamaKegiatan()
        ShowModelCategoryType(False)
        FillCategory()
        ddlCategory_SelectedIndexChanged(Nothing, Nothing)
        ddlModel_SelectedIndexChanged(Nothing, Nothing)
        FillYear()
    End Sub

    Private Sub ddlJenisKegiatan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJenisKegiatan.SelectedIndexChanged
        ShowModelCategoryType(ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Small_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Launching_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Exhibition_Khusus, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Showroom_Event, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Others, Integer))
        FillNamaKegiatan()
        HideColumn()
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        FillModel(ddlCategory.SelectedValue)
        FillType(ddlModel.SelectedValue)
    End Sub

    Private Sub ddlModel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlModel.SelectedIndexChanged
        FillType(ddlModel.SelectedValue)
    End Sub

    Private Sub dtgEvent_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEvent.SortCommand
        If GridSortColumn = e.SortExpression Then
            Select Case GridSortDirection
                Case Sort.SortDirection.ASC
                    GridSortDirection = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    GridSortDirection = Sort.SortDirection.ASC
            End Select
        Else
            GridSortColumn = e.SortExpression
            GridSortDirection = Sort.SortDirection.ASC
        End If

        Dim arl As ArrayList = _sesshelper.GetSession(DataSourceName)
        dtgEvent.DataSource = CommonFunction.SortArraylist(arl, GetType(EventProposal), GridSortColumn, GridSortDirection)
        dtgEvent.DataBind()
    End Sub

    Private Sub dtgEvent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEvent.ItemDataBound
        If IsNothing(e.Item.DataItem) Then Return

        e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgEvent.PageSize * dtgEvent.CurrentPageIndex)).ToString

        Dim objEp As EventProposal = CType(e.Item.DataItem, EventProposal)

        Dim lnkPenilaianKTB As LinkButton = CType(e.Item.FindControl("lnkPenilaianKTB"), LinkButton)
        Dim objPenilaian As EventProposalFile = eff.RetriveEventProposalFile(objEp.ID, _isKTB, EventProposalFile.EnumContentType.Penilaian_KTB)
        If (Not IsNothing(objPenilaian)) Then
            lnkPenilaianKTB.Text = objPenilaian.FileName
        End If

        Dim lnkLaporanAcara As LinkButton = CType(e.Item.FindControl("lnkLaporanAcara"), LinkButton)
        Dim objLaporanAcara As EventProposalFile = eff.RetriveEventProposalFile(objEp.ID, _isKTB, EventProposalFile.EnumContentType.Laporan_Acara)
        If (Not IsNothing(objLaporanAcara)) Then
            lnkLaporanAcara.Text = objLaporanAcara.FileName
        End If

        'Dim lnkLaporanPenjualan As LinkButton = CType(e.Item.FindControl("lnkLaporanPenjualan"), LinkButton)
        'Dim objLaporanPenjualan As EventProposalFile = eff.RetriveEventProposalFile(objEp.ID, _isKTB, EventProposalFile.EnumContentType.Laporan_Penjualan)
        'If (Not IsNothing(objLaporanPenjualan)) Then
        '    lnkLaporanPenjualan.Text = objLaporanPenjualan.FileName
        'End If
    End Sub

    Private Sub dtgEvent_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEvent.ItemCommand
        Dim _filename As String = ""
        If (e.CommandName = "DownloadPenilaianKTB") Then
            Dim _objEp As EventProposal = epf.Retrieve(CInt(e.CommandArgument))
            Dim obj As EventProposalFile = eff.RetriveEventProposalFile(_objEp.ID, _isKTB, EventProposalFile.EnumContentType.Penilaian_KTB)
            If (IsNothing(obj)) Then
                MessageBox.Show("File tidak ditemukan")
                Return
            End If
            _filename = obj.FileName
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory") & "\" & _filename
            Response.Redirect("../Download.aspx?file=" & PathFile, True)
        ElseIf (e.CommandName = "DownloadLaporanAcara") Then
            Dim _objEp As EventProposal = epf.Retrieve(CInt(e.CommandArgument))
            Dim obj As EventProposalFile = eff.RetriveEventProposalFile(_objEp.ID, _isKTB, EventProposalFile.EnumContentType.Laporan_Acara)
            If (IsNothing(obj)) Then
                MessageBox.Show("File tidak ditemukan")
                Return
            End If
            _filename = obj.FileName
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory") & "\" & _filename
            Response.Redirect("../Download.aspx?file=" & PathFile, True)
        ElseIf (e.CommandName = "DownloadLaporanPenjualan") Then
            Dim _objEp As EventProposal = epf.Retrieve(CInt(e.CommandArgument))
            Dim obj As EventProposalFile = eff.RetriveEventProposalFile(_objEp.ID, _isKTB, EventProposalFile.EnumContentType.Laporan_Penjualan)
            If (IsNothing(obj)) Then
                MessageBox.Show("File tidak ditemukan")
                Return
            End If
            _filename = obj.FileName
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory") & "\" & _filename
            Response.Redirect("../Download.aspx?file=" & PathFile, True)
        End If
        dtgEvent.DataBind()
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgEvent.CurrentPageIndex = 0
        BuiltCriteria()
        BindGrid()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim SW As StreamWriter
        Dim _filename As String = String.Format("{0}{1}{2}", "Event", Date.Now.ToString("ddMMyyyyHHmmss"), ".xls")
        Dim _destFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory") & "\" & _filename  '-- Destination file
        Dim _spliterChr As Char = Chr(9)
        Dim _connected As Boolean = False
        Dim _success As Boolean = False
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(_destFile)
        Try
            _success = imp.Start()
            If _success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                SW = New StreamWriter(_destFile)
                _connected = True
                'write title
                SW.WriteLine(String.Format("Nama Kegiatan : {0}", ddlJenisKegiatan.SelectedItem.Text))
                If (cbPeriode.Checked) Then
                    SW.WriteLine("Periode Kegiatan : {0} - {1}", calDari.Value.ToString("dd MMM yyyy"), calSampai.Value.ToString("dd MMM yyyy"))
                End If
                SW.WriteLine("")
                SW.WriteLine(String.Format("Kode Dealer{0}Nama Dealer{0}Tempat Kegiatan{0}Tgl Acara{0}Undangan{0}Undangan Hadir{0}Prosentase", _spliterChr))
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
            Exit Sub
        End Try

        Dim _hasCheckedData As Boolean = False
        For Each item As DataGridItem In dtgEvent.Items
            Dim chkSelect As CheckBox = CType(item.FindControl("chkSelect"), CheckBox)
            If (chkSelect.Checked) Then
                Dim obj As EventProposal = epf.Retrieve(CInt(item.Cells(0).Text))
                Dim szRow = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}%", _spliterChr, _
                        obj.Dealer.DealerCode, _
                        obj.Dealer.DealerName, _
                        obj.ActivityPlace, _
                        obj.ActivitySchedule.ToString("dd MMM yyyy"), _
                        CInt(obj.InvitationNumber), _
                        CInt(obj.AttendantNumber), _
                        obj.PercentageAttendent)
                SW.WriteLine(szRow)
                _hasCheckedData = True
            End If
        Next
        If _success Then
            If (_hasCheckedData) Then
                SW.Close()
                imp.StopImpersonate()
                imp = Nothing
                Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory") & "\" & _filename
                Response.Redirect("../Download.aspx?file=" & PathFile, True)
            Else
                MessageBox.Show("Belum ada data yang dipilih")
            End If
        Else
            MessageBox.Show("Belum ada data yang dipilih")
        End If
    End Sub

    Private Sub cbPeriode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPeriode.CheckedChanged
        calDari.Enabled = cbPeriode.Checked
        calSampai.Enabled = cbPeriode.Checked
    End Sub

#End Region

    'Private Sub dtgEvent_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgEvent.PreRender

    '    'If (dtgEvent.Controls.Count <= 0) Then Return

    '    'Dim tbl As Table = dtgEvent.Controls(0)
    '    'Dim realRow As TableRow = tbl.Rows(1)
    '    'Dim tmpRow As TableRow = tbl.Rows(1)
    '    'Dim tmpRow2 As TableRow = tbl.Rows(1)

    '    'Dim modRow As TableRow = New DataGridItem(1, -1, ListItemType.Header)

    '    'Dim n As Integer = realRow.Cells.Count - 2
    '    'For i As Integer = 0 To n
    '    '    Dim cell As TableCell = New TableCell
    '    '    If i = 9 Then
    '    '        cell.ColumnSpan = 2
    '    '        cell.Text = " --- "
    '    '    Else
    '    '        If realRow.Cells(i).Controls.Count > 1 Then
    '    '            cell.Controls.Add(realRow.Cells(i).Controls(0))
    '    '        End If
    '    '        cell.RowSpan = 2
    '    '    End If
    '    '    modRow.Cells.Add(cell)
    '    'Next

    '    'tbl.Rows.RemoveAt(1)
    '    'tbl.Rows.AddAt(1, modRow)

    '    'Dim newRow As TableRow = New DataGridItem(1, -1, ListItemType.Header)
    '    'Dim cell1 As TableCell = tmpRow.Cells(7)
    '    'newRow.Cells.Add(cell1)

    '    'Dim cell2 As TableCell = tmpRow.Cells(8)
    '    'newRow.Cells.Add(cell2)

    '    'Dim cell3 As TableCell = tmpRow.Cells(8)
    '    'newRow.Cells.Add(cell3)

    '    'tbl.Rows.AddAt(2, newRow)

    '    'Dim topRow As New DataGridItem(1, -1, ListItemType.Header)

    '    'For i As Integer = 0 To tbl.Rows(0).Cells.Count - 1
    '    '    Dim cell As TableCell = New TableCell
    '    '    tblRow.Cells.Add(cell)
    '    'Next
    '    'tbl.Rows.AddAt(2, tblRow)

    '    'For i As Integer = 0 To tbl.Rows(2).Cells.Count - 1
    '    '    If (i = 9) Then
    '    '        tbl.Rows(2).Cells(i).ColumnSpan = 2
    '    '    Else
    '    '        tbl.Rows(2).Cells(i).RowSpan = 2
    '    '    End If
    '    'Next



    '    'Dim cellTitleSpan, cellOwner, cellDriver As TableCell
    '    'For i As Int32 = 0 To realRow.Cells.Count - 1
    '    '    If i <> 9 AndAlso i <> 10 Then
    '    '        realRow.Cells(i).RowSpan = 2
    '    '    Else
    '    '        If i = 9 Then
    '    '            realRow.Cells(i).ColumnSpan = 2
    '    '            realRow.Cells(i).Text = "Jumlah Undangan"
    '    '        End If
    '    '        'If i = 6 Then
    '    '        '    realRow.Cells(i).ColumnSpan = 2
    '    '        'End If
    '    '    End If
    '    'Next

    'End Sub

    Private Sub dtgEvent_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEvent.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then

            'If (dtgEvent.Controls.Count < 0) Then
            '    Return
            'End If

            'Dim realRow As TableRow = e.Item
            'Dim tbl As Table = dtgEvent.Controls(0)

            'Dim modRow As TableRow = New DataGridItem(1, -1, ListItemType.Header)
            'Dim n As Integer = e.Item.Cells.Count - 1
            'For i As Integer = 0 To n
            '    Dim cell As TableCell = New TableCell
            '    If (i <> 9) Then
            '        cell.BorderStyle = BorderStyle.None
            '        cell.BorderWidth = New Unit("0px")
            '        cell.BackColor = System.Drawing.Color.Transparent
            '    End If
            '    modRow.Cells.Add(cell)
            'Next

            'modRow.Cells(9).ColumnSpan = 2
            'modRow.Cells(9).Text = "MERGED CELLS"
            'Dim mergedCell As New TableCell
            'mergedCell = modRow.Cells(10)
            'modRow.Cells.Remove(mergedCell)
            'tbl.Rows.AddAt(1, modRow)

            'Dim realRow2 As TableRow
            'realRow2 = e.Item
            'Dim n As Integer = realRow2.Cells.Count - 2
            'Dim tmpRow As TableRow = New DataGridItem(1, -1, ListItemType.Header)
            'Dim tmpRow2 As TableRow = New DataGridItem(1, -1, ListItemType.Header)
            'For i As Integer = 0 To n
            '    If (i <> 9) Then
            '        Dim cell As TableCell = New TableCell
            '        'If realRow2.Cells(i).Controls.Count > 0 Then
            '        '    cell.Controls.Add(realRow2.Cells(i).Controls(0))
            '        'End If
            '        cell.RowSpan = 2
            '        tmpRow.Cells.Add(cell)
            '    Else
            '        Dim cell2 As TableCell = New TableCell
            '        cell2.ColumnSpan = 2
            '        cell2.Text = "MERGED CELLS"
            '        tmpRow.Cells.Add(cell2)

            '        Dim cell As TableCell = New TableCell
            '        'If realRow2.Cells(i).Controls.Count > 0 Then
            '        '    cell.Controls.Add(realRow2.Cells(i).Controls(0))
            '        'End If
            '        tmpRow2.Cells.Add(cell)
            '    End If
            'Next

            'tbl.Rows.RemoveAt(1)
            'tbl.Rows.AddAt(1, tmpRow)
            'tbl.Rows.AddAt(2, tmpRow2)

            'Dim n2 As Integer = modRow.Cells.Count - 2
            'Dim tmpRow As TableRow = New DataGridItem(1, -1, ListItemType.Header)
            'Dim i2 As Integer = 0
            'While i2 < n2
            '    If (modRow.Cells(i2).ColumnSpan = 0) Then
            '        Dim cell As TableCell = realRow.Cells(i2)
            '        tmpRow.Cells.Add(cell)
            '        modRow.Cells(i2).RowSpan = 2
            '        i2 += 1
            '    End If
            'End While
            'tbl.Rows.RemoveAt(1)
            'tbl.Rows.AddAt(1, modRow)
            'tbl.Rows.AddAt(1, tmpRow)
            'For i As Integer = 0 To modRow.Cells.Count - 1
            '    If (i <> 9) Then
            '        If (i < modRow.Cells.Count - 2) Then

            '            Dim cell As New TableCell
            '            cell = modRow.Cells(i + 1)
            '            modRow.Cells(i).Text = cell.Text
            '            modRow.Cells(i).RowSpan = 2

            '            modRow.Cells.Remove(cell)
            '            realRow.Cells.Remove(cell)
            '        End If
            '    End If
            'Next


            'e.Item.Cells(9).ColumnSpan = 2
            'e.Item.Cells(9).Text = "Company and Contact"
            'Dim mergedCell As New TableCell
            'mergedCell = e.Item.Cells(10)
            'e.Item.Cells.Remove(mergedCell)

            'Dim tmpRow As TableRow = e.Item
            'Dim tmpRow2 As TableRow = e.Item

            'Dim modRow As TableRow = New DataGridItem(1, -1, ListItemType.Header)

            'Dim n As Integer = realRow.Cells.Count - 2
            'For i As Integer = 0 To n
            '    Dim cell As TableCell = New TableCell
            '    If i = 9 Then
            '        cell.ColumnSpan = 2
            '        cell.Text = " --- "
            '    Else
            '        If i < realRow.Cells.Count - 2 Then
            '            cell = realRow.Cells(i)
            '        End If
            '        cell.RowSpan = 2
            '    End If
            '    modRow.Cells.Add(cell)
            'Next

            ''For i As Integer = 0 To tmpRow2.Cells.Count - 2
            ''    If i = 9 Then
            ''        tmpRow2.Cells(i)
            ''    End If
            ''Next

            'tbl.Rows.RemoveAt(1)
            'tbl.Rows.AddAt(1, modRow)

            'Dim newRow As TableRow = New DataGridItem(1, -1, ListItemType.Header)
            'Dim cell1 As TableCell = tmpRow.Cells(7)
            'newRow.Cells.Add(cell1)

            'Dim cell2 As TableCell = tmpRow.Cells(8)
            'newRow.Cells.Add(cell2)

            ''Dim cell3 As TableCell = tmpRow.Cells(8)
            ''newRow.Cells.Add(cell3)

            'tbl.Rows.AddAt(2, newRow)

        End If

    End Sub

    Private Sub dtgEvent_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEvent.PageIndexChanged
        dtgEvent.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub
End Class
