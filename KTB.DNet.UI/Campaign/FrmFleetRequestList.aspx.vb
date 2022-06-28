#Region "Custom Namespace Imports"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

#Region "DotNet Namespace"
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper

#End Region

Public Class FrmFleetRequestList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents dtgFleetRequest As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGetDealer As System.Web.UI.WebControls.Button
    Protected WithEvents btnProcess As System.Web.UI.WebControls.Button


    Protected WithEvents ddlFleetNumber As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNoRegRequest As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkTglPengajuan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icTglPengajuan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglPengajuanTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNamaKonsumen As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatusKonsumen As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlProfilBisnis As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkMulaiPengadaan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icTglMulaiPengadaan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglMulaiPengadaanTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkSelesaiPengadaan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icTglSelesaiPengadaan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglSelesaiPengadaanTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlRubahStatus As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Custom Variable Declaration "
    'Private _nDealerID As Integer
    Private _sessHelper As SessionHelper = New SessionHelper
    Private _ListFleetRequest As ArrayList = New ArrayList
    Private _ListFleetRequest2 As ArrayList = New ArrayList
    Private _isDealer As Boolean = False
    'Private _isShowDetailAllowed As Boolean = False

    Private _sessDataList As String = "FrmFleetRequestList._sessDataList"
    Private _sessProfilBisnis As String = "FrmFleetRequestList._sessProfilBisnis"
    Private _sessCriteria As String = "FrmFleetRequestList._sessCriteria"
#End Region

#Region " Custom Method "

    Private Sub ReadData()

        If txtDealerCode.Text.Trim <> "" Then
            Dim objDealerFind As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim.Split(";")(0))
            lblDealerName.Text = objDealerFind.DealerName
            lblDealerTerm.Text = objDealerFind.SearchTerm2
        Else
            If txtDealerCode.Visible Then
                lblDealerName.Text = String.Empty
                lblDealerTerm.Text = String.Empty
            End If
        End If

        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetRequest), "RowStatus", MatchType.Exact, 0))

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(FleetRequest), "FleetMasterDealer.Dealer.DealerCode", MatchType.Exact, CType(Session("DEALER"), Dealer).DealerCode))
        ElseIf (txtDealerCode.Text.Trim <> "") Then
            criterias.opAnd(New Criteria(GetType(FleetRequest), "FleetMasterDealer.Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If

        If ddlFleetNumber.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(FleetRequest), "FleetMasterDealer.FleetMaster.ID", MatchType.Exact, ddlFleetNumber.SelectedValue))

        If txtNoRegRequest.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(FleetRequest), "NoRegRequest", MatchType.[Partial], txtNoRegRequest.Text.Trim))
        End If

        If txtNamaKonsumen.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(FleetRequest), "NamaKonsumen", MatchType.[Partial], txtNamaKonsumen.Text.Trim))
        End If

        If ddlStatusKonsumen.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(FleetRequest), "StatusKonsumen", MatchType.Exact, ddlStatusKonsumen.SelectedValue))

        If ddlProfilBisnis.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(FleetRequest), "ProfilBisnis", MatchType.Exact, ddlProfilBisnis.SelectedValue))

        If chkTglPengajuan.Checked Then
            If icTglPengajuan.Value <= icTglPengajuanTo.Value Then
                criterias.opAnd(New Criteria(GetType(FleetRequest), "TanggalPengajuan", MatchType.GreaterOrEqual, Format(icTglPengajuan.Value, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(FleetRequest), "TanggalPengajuan", MatchType.LesserOrEqual, Format(icTglPengajuanTo.Value, "yyyy/MM/dd")))
            Else
                criterias.opAnd(New Criteria(GetType(FleetRequest), "TanggalPengajuan", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(FleetRequest), "TanggalPengajuan", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                icTglPengajuan.Value = Date.Now
                icTglPengajuanTo.Value = Date.Now
            End If
        End If

        If chkMulaiPengadaan.Checked Then
            If icTglMulaiPengadaan.Value <= icTglPengajuanTo.Value Then
                criterias.opAnd(New Criteria(GetType(FleetRequest), "MulaiPengadaan", MatchType.GreaterOrEqual, Format(icTglMulaiPengadaan.Value, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(FleetRequest), "MulaiPengadaan", MatchType.LesserOrEqual, Format(icTglMulaiPengadaanTo.Value, "yyyy/MM/dd")))
            Else
                criterias.opAnd(New Criteria(GetType(FleetRequest), "MulaiPengadaan", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(FleetRequest), "MulaiPengadaan", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                icTglMulaiPengadaan.Value = Date.Now
                icTglMulaiPengadaanTo.Value = Date.Now
            End If
        End If


        If chkSelesaiPengadaan.Checked Then
            If icTglSelesaiPengadaan.Value <= icTglSelesaiPengadaanTo.Value Then
                criterias.opAnd(New Criteria(GetType(FleetRequest), "SelesaiPengadaan", MatchType.GreaterOrEqual, Format(icTglSelesaiPengadaan.Value, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(FleetRequest), "SelesaiPengadaan", MatchType.LesserOrEqual, Format(icTglSelesaiPengadaanTo.Value, "yyyy/MM/dd")))
            Else
                criterias.opAnd(New Criteria(GetType(FleetRequest), "SelesaiPengadaan", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(FleetRequest), "SelesaiPengadaan", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                icTglSelesaiPengadaan.Value = Date.Now
                icTglSelesaiPengadaanTo.Value = Date.Now
            End If
        End If

        If ddlStatus.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(FleetRequest), "Status", MatchType.Exact, ddlStatus.SelectedValue))

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(FleetRequest), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        _ListFleetRequest = New FleetRequestFacade(User).Retrieve(criterias, sortColl)

        _sessHelper.SetSession(_sessDataList, _ListFleetRequest)

    End Sub

    Private Sub BindTodtgFleetRequest(ByVal pageindex As Integer)
        _ListFleetRequest = CType(_sessHelper.GetSession(_sessDataList), ArrayList)
        If _ListFleetRequest.Count <> 0 Then
            Dim PagedList As ArrayList = ArrayListPager.DoPage(_ListFleetRequest, pageindex, dtgFleetRequest.PageSize)
            dtgFleetRequest.DataSource = PagedList
            dtgFleetRequest.VirtualItemCount = _ListFleetRequest.Count()
        Else
            dtgFleetRequest.DataSource = New ArrayList
            dtgFleetRequest.VirtualItemCount = 0
            dtgFleetRequest.CurrentPageIndex = 0
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Pengajuan Extended Free Service"))
            End If
        End If
        dtgFleetRequest.DataBind()
    End Sub


    Private Sub BindHeader()
        If _isDealer Then
            Dim ObjMainDealer As Dealer = CType(Session("DEALER"), Dealer)
            lblDealerCode.Text = ObjMainDealer.DealerCode
            lblDealerName.Text = ObjMainDealer.DealerName
            lblDealerTerm.Text = ObjMainDealer.SearchTerm2
        Else
            lblDealerCode.Text = ""
            lblDealerName.Text = ""
            lblDealerTerm.Text = ""
        End If

        BindFleetRequestStatus()
        BindRubahStatus()
        BindFleetNumber()
        BindStatusKonsumen()
        BindProfilBisnis()
    End Sub


    Private Sub BindStatusKonsumen()
        ddlStatusKonsumen.Items.Clear()
        ddlStatusKonsumen.Items.Add(New ListItem("Silakan Pilih", -1))

        Dim enumTmp As EnumStatusKonsumen = New EnumStatusKonsumen
        For Each oStatus As EnumStsKonsumen In enumTmp.RetrieveStatusKonsumen()
            ddlStatusKonsumen.Items.Add(New ListItem(oStatus.NameStatus, oStatus.ValStatus))
        Next
    End Sub


    Private Sub BindFleetNumber()
        Dim ObjMainDealer As Dealer = CType(Session("DEALER"), Dealer)

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arrTmp As ArrayList

        If ObjMainDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criteria.opAnd(New Criteria(GetType(FleetMaster), "ID", MatchType.InSet, "(select FleetMasterID from FleetMasterDealer where DealerID = '" & ObjMainDealer.ID & "')"))
        End If


        arrTmp = New FleetMasterFacade(User).Retrieve(criteria)

        With ddlFleetNumber.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each objTmp As FleetMaster In arrTmp
                .Add(New ListItem(objTmp.NoFleet, objTmp.ID))
            Next
        End With

    End Sub

    Private Sub BindProfilBisnis()

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arrTmp As ArrayList

        criteria.opAnd(New Criteria(GetType(ProfileDetail), "ProfileHeader.Code", MatchType.Exact, "CBU_LOADPROFILE1"))
        arrTmp = New ProfileDetailFacade(User).Retrieve(criteria)

        With ddlProfilBisnis.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each objTmp As ProfileDetail In arrTmp
                .Add(New ListItem(objTmp.Description, objTmp.Code))
            Next
        End With

        _sessHelper.SetSession(_sessProfilBisnis, arrTmp)
    End Sub

    Private Sub BindFleetRequestStatus()
        Dim enumTmp As EnumFleetRequest = New EnumFleetRequest
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Silakan Pilih", -1))
        For Each oStatus As EnumFleetRequestStatus In enumTmp.RetrieveFleetRequestStatus()
            If oStatus.ValStatus <> EnumFleetRequest.FleetRequestStatus.BatalValidasi AndAlso oStatus.ValStatus <> EnumFleetRequest.FleetRequestStatus.BatalKonfirmasi Then
                ddlStatus.Items.Add(New ListItem(oStatus.NameStatus, oStatus.ValStatus))
            End If
        Next
    End Sub

    Private Sub BindRubahStatus()
        Dim ObjMainDealer As Dealer = CType(Session("DEALER"), Dealer)

        If ObjMainDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then

        End If

        Dim enumTmp As EnumFleetRequest = New EnumFleetRequest
        ddlRubahStatus.Items.Clear()
        ddlRubahStatus.Items.Add(New ListItem("Silakan Pilih", -1))
        For Each oStatus As EnumFleetRequestStatus In enumTmp.RetrieveFleetRequestStatus()
            If ObjMainDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If oStatus.ValStatus = EnumFleetRequest.FleetRequestStatus.Batal Or oStatus.ValStatus = EnumFleetRequest.FleetRequestStatus.Validasi Or oStatus.ValStatus = EnumFleetRequest.FleetRequestStatus.BatalValidasi Then
                    ddlRubahStatus.Items.Add(New ListItem(oStatus.NameStatus, oStatus.ValStatus))
                End If
            Else
                If oStatus.ValStatus = EnumFleetRequest.FleetRequestStatus.Konfirmasi Or oStatus.ValStatus = EnumFleetRequest.FleetRequestStatus.BatalKonfirmasi Or oStatus.ValStatus = EnumFleetRequest.FleetRequestStatus.Ditolak Then
                    ddlRubahStatus.Items.Add(New ListItem(oStatus.NameStatus, oStatus.ValStatus))
                End If
            End If
        Next
    End Sub

    Public Function GetProfilBisnis(ByVal strCode As String) As String

        Dim arrTmp As ArrayList = _sessHelper.GetSession(_sessProfilBisnis)
        Dim strReturn As String = ""

        For Each objTmp As ProfileDetail In arrTmp
            If objTmp.Code = strCode Then
                strReturn = objTmp.Description
            End If
        Next

        Return strReturn
    End Function


    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("lblDealerCode", lblDealerCode.Text)
        crits.Add("lblDealerName", lblDealerName.Text)
        crits.Add("lblDealerTerm", lblDealerTerm.Text)
        crits.Add("lblSearchDealer", lblSearchDealer.Text)
        crits.Add("txtDealerCode", txtDealerCode.Text)
        crits.Add("ddlFleetNumber", ddlFleetNumber.SelectedValue.ToString)
        crits.Add("txtNoRegRequest", txtNoRegRequest.Text)
        crits.Add("txtNamaKonsumen", txtNamaKonsumen.Text)
        crits.Add("ddlStatusKonsumen", ddlStatusKonsumen.SelectedValue.ToString)
        crits.Add("ddlProfilBisnis", ddlProfilBisnis.SelectedValue.ToString)
        crits.Add("chkTglPengajuan", chkTglPengajuan.Checked)
        crits.Add("icTglPengajuan", icTglPengajuan.Value)
        crits.Add("icTglPengajuanTo", icTglPengajuanTo.Value)
        crits.Add("chkMulaiPengadaan", chkMulaiPengadaan.Checked)
        crits.Add("icTglMulaiPengadaan", icTglMulaiPengadaan.Value)
        crits.Add("icTglMulaiPengadaanTo", icTglMulaiPengadaanTo.Value)
        crits.Add("chkSelesaiPengadaan", chkSelesaiPengadaan.Checked)
        crits.Add("icTglSelesaiPengadaan", icTglSelesaiPengadaan.Value)
        crits.Add("icTglSelesaiPengadaanTo", icTglSelesaiPengadaanTo.Value)
        crits.Add("ddlStatus", ddlStatus.SelectedValue.ToString)
        _sessHelper.SetSession(_sessCriteria, crits)
    End Sub

    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(_sessHelper.GetSession(_sessCriteria), Hashtable)

        If Not IsNothing(crits) Then
            lblDealerCode.Text = CStr(crits.Item("lblDealerCode"))
            lblDealerName.Text = CStr(crits.Item("lblDealerName"))
            lblDealerTerm.Text = CStr(crits.Item("lblDealerTerm"))
            lblSearchDealer.Text = CStr(crits.Item("lblSearchDealer"))
            txtDealerCode.Text = CStr(crits.Item("txtDealerCode"))
            ddlFleetNumber.SelectedValue = CStr(crits.Item("ddlFleetNumber"))
            txtNoRegRequest.Text = CStr(crits.Item("txtNoRegRequest"))
            txtNamaKonsumen.Text = CStr(crits.Item("txtNamaKonsumen"))
            ddlStatusKonsumen.SelectedValue = CStr(crits.Item("ddlStatusKonsumen"))
            ddlProfilBisnis.SelectedValue = CStr(crits.Item("ddlProfilBisnis"))
            chkTglPengajuan.Checked = CBool(crits.Item("chkTglPengajuan"))
            icTglPengajuan.Value = CStr(crits.Item("icTglPengajuan"))
            icTglPengajuanTo.Value = CStr(crits.Item("icTglPengajuanTo"))
            chkMulaiPengadaan.Checked = CBool(crits.Item("chkMulaiPengadaan"))
            icTglMulaiPengadaan.Value = CStr(crits.Item("icTglMulaiPengadaan"))
            icTglMulaiPengadaanTo.Value = CStr(crits.Item("icTglMulaiPengadaanTo"))
            chkSelesaiPengadaan.Checked = CBool(crits.Item("chkSelesaiPengadaan"))
            icTglSelesaiPengadaan.Value = CStr(crits.Item("icTglSelesaiPengadaan"))
            icTglSelesaiPengadaanTo.Value = CStr(crits.Item("icTglSelesaiPengadaanTo"))
            ddlStatus.SelectedValue = CStr(crits.Item("ddlStatus"))
        End If
    End Sub


#End Region

#Region " Event Handler"

    Private Sub InitiateAuthorization()

        If SecurityProvider.Authorize(Context.User, SR.Lihat_Daftar_Fleet_Privilege) Then
            If Not (SecurityProvider.Authorize(Context.User, SR.Proses_Pengajuan_Fleet_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.Input_Pengajuan_Fleet_Privilege)) Then
                ddlRubahStatus.Visible = False
                btnProcess.Visible = False
            End If
        Else
            Server.Transfer("../FrmAccessDenied.aspx?modulName=DAFTAR EXTENDED FREE SERVICE")
        End If

        Dim ObjMainDealer As Dealer = CType(Session("DEALER"), Dealer)

        If ObjMainDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then

            _isDealer = True

            txtDealerCode.Visible = False
            lblSearchDealer.Visible = False
            lblDealerCode.Visible = True

            dtgFleetRequest.Columns(3).Visible = False
            btnProcess.Attributes.Add("onclick", "return confirm('Apakah anda yakin akan merubah status ?');")

        Else

            txtDealerCode.Visible = True
            lblSearchDealer.Visible = True
            lblDealerCode.Visible = False
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
            btnGetDealer.Style("display") = "none"
            ViewState("currSortColumn") = "NoRegRequest"
            ViewState("currSortTable") = GetType(FleetRequest)
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindHeader()
            dtgFleetRequest.DataSource = New ArrayList
            dtgFleetRequest.DataBind()
            ReadCriteria()
            ReadData()
            BindTodtgFleetRequest(0)
        End If
    End Sub

    Sub dtgFleetRequest_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If (e.Item.ItemIndex <> -1) Then
            Dim objFleetRequest As FleetRequest
            objFleetRequest = CType(_ListFleetRequest(e.Item.ItemIndex), FleetRequest)

            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgFleetRequest.PageSize * dtgFleetRequest.CurrentPageIndex)).ToString
            CType(e.Item.FindControl("lblStatusKonsumen"), Label).Text = Replace(CType(objFleetRequest.StatusKonsumen, EnumStatusKonsumen.StatusKonsumen).ToString, "-1", "")
            CType(e.Item.FindControl("lblStatus"), Label).Text = CType(objFleetRequest.Status, EnumFleetRequest.FleetRequestStatus).ToString
            CType(e.Item.FindControl("txtBatalKonfirmasi"), TextBox).Text = objFleetRequest.BatalKonfirmasiNote

            If objFleetRequest.Status = EnumFleetRequest.FleetRequestStatus.Baru Then
                If SecurityProvider.Authorize(Context.User, SR.Input_Pengajuan_Fleet_Privilege) Then
                    CType(e.Item.FindControl("linkDelete"), LinkButton).Visible = True
                    CType(e.Item.FindControl("linkEdit"), LinkButton).Visible = True
                Else
                    CType(e.Item.FindControl("linkDelete"), LinkButton).Visible = False
                    CType(e.Item.FindControl("linkEdit"), LinkButton).Visible = False
                End If
            Else
                CType(e.Item.FindControl("linkDelete"), LinkButton).Visible = False
                CType(e.Item.FindControl("linkEdit"), LinkButton).Visible = False
            End If

        End If
    End Sub


    Private Sub dtgFleetRequest_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgFleetRequest.SortCommand
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

        '-- Bind page-1
        dtgFleetRequest.CurrentPageIndex = 0
        ReadData()
        BindTodtgFleetRequest(dtgFleetRequest.CurrentPageIndex)
    End Sub

    Private Sub dtgFleetRequest_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFleetRequest.PageIndexChanged
        dtgFleetRequest.CurrentPageIndex = e.NewPageIndex
        BindTodtgFleetRequest(e.NewPageIndex)
    End Sub

    Private Sub dtgFleetRequest_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgFleetRequest.ItemCommand
        If e.CommandName = "View" Then
            Response.Redirect("FrmFleetRequest.aspx?Mode=View&Src=List&ID=" & CType(e.CommandArgument, Integer))
        ElseIf e.CommandName = "Delete" Then
            Dim oFleetRequest As FleetRequest = New FleetRequestFacade(User).Retrieve(CType(e.CommandArgument, Integer))
            Dim oFleetRequestFacade As FleetRequestFacade = New FleetRequestFacade(User)
            oFleetRequestFacade.Delete(oFleetRequest)
            '---delete Attachment
            BindTodtgFleetRequest(dtgFleetRequest.CurrentPageIndex)
        ElseIf e.CommandName = "Edit" Then
            Response.Redirect("FrmFleetRequest.aspx?Mode=Edit&Src=List&ID=" & CType(e.CommandArgument, Integer))
        End If
    End Sub

    Private Sub btnGetDealer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDealer.Click
        If txtDealerCode.Text.Length > 0 Then
            Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
            lblDealerName.Text = ObjDealer.DealerName
            lblDealerTerm.Text = ObjDealer.SearchTerm2
        End If
    End Sub


    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click

        If (txtDealerCode.Text.Trim <> "") Then
            ViewState("currSortTable") = GetType(FleetRequest)
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        Else

            ViewState("currSortColumn") = "NoRegRequest"
            ViewState("currSortTable") = GetType(FleetRequest)
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        SaveCriteria()
        ReadData()
        dtgFleetRequest.CurrentPageIndex = 0
        BindTodtgFleetRequest(dtgFleetRequest.CurrentPageIndex)

    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click

        If ddlRubahStatus.SelectedIndex = 0 Then
            MessageBox.Show("Perubahan Status belum dipilih.")
            Return
        End If

        Dim oFleetRequest As FleetRequest
        Dim oFleetRequestFacade As FleetRequestFacade = New FleetRequestFacade(User)
        Dim stsOld, stsNew As EnumFleetRequest.FleetRequestStatus
        Dim strFailUpdated As String = ""
        Dim intUpdatedCount As Integer = 0
        Dim strMsg As String = ""

        For Each tr As TableRow In dtgFleetRequest.Items
            If CType(tr.Cells(1).FindControl("chkFleetRequest"), CheckBox).Checked Then

                oFleetRequest = New FleetRequestFacade(User).Retrieve(CType(tr.Cells(0).Text, Integer))
                stsOld = oFleetRequest.Status
                stsNew = ddlRubahStatus.SelectedValue

                If (stsOld = EnumFleetRequest.FleetRequestStatus.Baru AndAlso (stsNew = EnumFleetRequest.FleetRequestStatus.Batal OrElse stsNew = EnumFleetRequest.FleetRequestStatus.Validasi)) OrElse _
                    (stsOld = EnumFleetRequest.FleetRequestStatus.Validasi AndAlso (stsNew = EnumFleetRequest.FleetRequestStatus.BatalValidasi OrElse stsNew = EnumFleetRequest.FleetRequestStatus.Konfirmasi OrElse stsNew = EnumFleetRequest.FleetRequestStatus.Ditolak)) OrElse _
                    (stsOld = EnumFleetRequest.FleetRequestStatus.Konfirmasi AndAlso stsNew = EnumFleetRequest.FleetRequestStatus.BatalKonfirmasi) Then

                    If stsNew = EnumFleetRequest.FleetRequestStatus.BatalKonfirmasi Then
                        oFleetRequest.BatalKonfirmasiNote = CType(tr.FindControl("txtBatalKonfirmasi"), TextBox).Text
                    End If

                    If stsNew = EnumFleetRequest.FleetRequestStatus.BatalKonfirmasi Then stsNew = EnumFleetRequest.FleetRequestStatus.Validasi
                    If stsNew = EnumFleetRequest.FleetRequestStatus.BatalValidasi Then stsNew = EnumFleetRequest.FleetRequestStatus.Baru
                    oFleetRequest.Status = stsNew

                    oFleetRequestFacade.Update(oFleetRequest)
                    intUpdatedCount = intUpdatedCount + 1
                Else
                    strFailUpdated = strFailUpdated & "," & tr.Cells(6).Text
                End If

            End If
        Next
        dtgFleetRequest.DataBind()
        BindTodtgFleetRequest(dtgFleetRequest.CurrentPageIndex)

        strMsg = intUpdatedCount & " Pengajuan Extended Free Service Status berhasil diupdate."
        If strFailUpdated <> "" Then
            strMsg = strMsg & "\n\nPengajuan Extended Free Service berikut gagal diupdate : " & Right(strFailUpdated, Len(strFailUpdated) - 1)
        End If
        MessageBox.Show(strMsg)
    End Sub


#End Region


End Class