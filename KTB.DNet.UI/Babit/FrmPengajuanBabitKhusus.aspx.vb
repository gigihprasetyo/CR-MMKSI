Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports System.IO
Imports KTB.DNet.Security

Public Class FrmPengajuanBabitKhusus
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblProvince As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoPerjanjian As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaAlokasiBabit As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblJenisKegiatan As System.Web.UI.WebControls.Label
    Protected WithEvents txtDana As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblDana As System.Web.UI.WebControls.Label
    Protected WithEvents fuBabitKhusus As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents hdnPameran As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnEvent As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnIklan As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents pnlScript As System.Web.UI.WebControls.Panel
    Protected WithEvents hdnPameranSubmit As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnEventSubmit As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnIklanSubmit As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblUploadFile As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoSuratDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnFindDealer As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoPersetujuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents hdnValNew As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnValSubmit As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Dim en As EnumBabit
    Dim objDealer As New Dealer
    Dim sHelper As New SessionHelper
    Private MAX_FILE_SIZE As Integer = 5120000
#End Region

#Region "Custom Methods"

    Private Function SetCompPrivilage(ByVal blnAccess As Boolean)
        'fuBabitKhusus.Visible = blnAccess
        'btnSave.Enabled = blnAccess
        'btnSubmit.Enabled = blnAccess
    End Function

    Private Function IsLoginAsDealer() As Boolean
        Return (CType(sHelper.GetSession("DEALER"), Dealer).TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub fillDealerInfoFromKTB()
        Dim arlBabitAllocation As New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Status", MatchType.Exact, CType(EnumBabit.BabitAllocationStatus.Rilis, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.AllocationType", MatchType.Exact, CType(EnumBabit.BabitAllocationType.Babit_Khusus, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.StartPeriod", MatchType.LesserOrEqual, Month(Date.Now)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.EndPeriod", MatchType.LesserOrEqual, DateTime.Now.AddMonths(1).Month()))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYear", MatchType.LesserOrEqual, Year(Date.Now)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYearEnd", MatchType.GreaterOrEqual, Year(Date.Now)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim()))
        arlBabitAllocation = New BabitSalesComm.BabitFacade(User).RetrieveBabitAllocation(criterias, "CreatedTime", Sort.SortDirection.DESC)

        Dim objTmpDealer As Dealer
        If (Request.QueryString("Mode") = "NewFromAlloc") Then
            objTmpDealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
            If (IsNothing(objTmpDealer)) Then
                MessageBox.Show("Kode dealer tidak ditemukan")
                Return
            End If
            lblDealerCode.Text = objTmpDealer.DealerCode
            lblDealerName.Text = objTmpDealer.DealerName
            lblCity.Text = objTmpDealer.City.CityName
            lblProvince.Text = objTmpDealer.Province.ProvinceName
        End If

        If (arlBabitAllocation.Count = 0) Then
            btnSave.Enabled = False
        Else
            Dim oBabitAllocation As New BabitAllocation
            If (Request.QueryString("Mode") = "NewFromAlloc") Then
                Dim n As Integer = 0
                For Each obj As BabitAllocation In arlBabitAllocation
                    Dim dtmFrom As DateTime = New DateTime(obj.Babit.BabitYear, obj.Babit.StartPeriod, 1)
                    Dim dtmTo As DateTime = New DateTime(obj.Babit.BabitYearEnd, obj.Babit.EndPeriod, 2)
                    If (DateTime.Now >= dtmFrom) And (DateTime.Now <= dtmTo) Then
                        oBabitAllocation = New BabitAllocation
                        oBabitAllocation = obj
                        Exit For
                    Else
                        n += 1
                    End If
                Next
                If (n >= arlBabitAllocation.Count) Then
                    MessageBox.Show("Alokasi babit khusus untuk periode ini tidak tersedia")
                    Return
                End If
            Else
                oBabitAllocation = CType(arlBabitAllocation(0), BabitAllocation)
            End If

            sHelper.SetSession("BabitAllocation", oBabitAllocation)
            lblNoPerjanjian.Text = oBabitAllocation.NoPerjanjian
            lblPeriode.Text = String.Format("{0} - {1}", New DateTime(oBabitAllocation.Babit.BabitYear, oBabitAllocation.Babit.StartPeriod, 1).ToString("dd MMM yyyy"), New DateTime(oBabitAllocation.Babit.BabitYearEnd, oBabitAllocation.Babit.EndPeriod, 1).ToString("dd MMM yyyy"))
            lblSisaAlokasiBabit.Text = Convert.ToInt64(oBabitAllocation.SisaBabit).ToString("#,##0")
            fuBabitKhusus.Visible = True
            lblDealerCode.Text = oBabitAllocation.Dealer.DealerCode
            lblDealerName.Text = oBabitAllocation.Dealer.DealerName
            lblCity.Text = oBabitAllocation.Dealer.City.CityName
            lblProvince.Text = oBabitAllocation.Dealer.Province.ProvinceName
        End If
    End Sub

    Sub DealerData(ByVal oDealer As Dealer)
        If IsLoginAsDealer() Then
            Dim arlBabitAllocation As New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Status", MatchType.Exact, CType(EnumBabit.BabitAllocationStatus.Rilis, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.AllocationType", MatchType.Exact, CType(EnumBabit.BabitAllocationType.Babit_Khusus, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.StartPeriod", MatchType.LesserOrEqual, Month(Date.Now)))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, Month(Date.Now)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYear", MatchType.LesserOrEqual, Year(Date.Now)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYearEnd", MatchType.GreaterOrEqual, Year(Date.Now)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.Exact, oDealer.DealerCode))
            arlBabitAllocation = New BabitSalesComm.BabitFacade(User).RetrieveBabitAllocation(criterias, "CreatedTime", Sort.SortDirection.DESC)
            If (arlBabitAllocation.Count <= 0) Then
                btnSave.Enabled = False
                ddlJenisKegiatan.Enabled = False
                MessageBox.Show("Alokasi babit khusus untuk periode ini tidak tersedia")
            Else
                Dim n As Integer = 0
                Dim oBabitAllocation As New BabitAllocation
                For Each obj As BabitAllocation In arlBabitAllocation
                    Dim dtmFrom As DateTime = New DateTime(obj.Babit.BabitYear, obj.Babit.StartPeriod, 1)
                    Dim dtmTo As DateTime = New DateTime(obj.Babit.BabitYearEnd, obj.Babit.EndPeriod, 2)
                    If (DateTime.Now >= dtmFrom) And (DateTime.Now <= dtmTo) Then
                        oBabitAllocation = New BabitAllocation
                        oBabitAllocation = obj
                        Exit For
                    Else
                        n += 1
                    End If
                Next
                If (n >= arlBabitAllocation.Count) Then
                    MessageBox.Show("Alokasi babit khusus untuk periode ini tidak tersedia")
                    Return
                End If

                sHelper.SetSession("BabitAllocation", oBabitAllocation)
                lblNoPerjanjian.Text = oBabitAllocation.NoPerjanjian
                lblPeriode.Text = String.Format("{0} - {1}", New DateTime(oBabitAllocation.Babit.BabitYear, oBabitAllocation.Babit.StartPeriod, 1).ToString("dd MMM yyyy"), New DateTime(oBabitAllocation.Babit.BabitYearEnd, oBabitAllocation.Babit.EndPeriod, 1).ToString("dd MMM yyyy"))
                lblSisaAlokasiBabit.Text = Convert.ToInt64(oBabitAllocation.SisaBabit).ToString("#,##0")
                btnSave.Enabled = True
                btnSubmit.Enabled = False
                txtDana.Enabled = True
                fuBabitKhusus.Visible = True
            End If
            txtDealerCode.Visible = False
            lblPopUpDealer.Visible = False
            btnFindDealer.Visible = False
            lblDealerCode.Text = oDealer.DealerCode
            lblDealerName.Text = oDealer.DealerName
            lblCity.Text = oDealer.City.CityName
            lblProvince.Text = oDealer.Province.ProvinceName
            txtNoPersetujuan.Text = "Diisi oleh MMKSI"
            txtNoPersetujuan.Enabled = False
        Else
            txtDealerCode.Visible = True
            lblPopUpDealer.Visible = True
            btnFindDealer.Visible = True
            lblDealerCode.Visible = False
            fuBabitKhusus.Visible = True
            lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        End If


        'lblDealerCode.Text = oDealer.DealerCode
        'lblDealerName.Text = oDealer.DealerName
        'lblCity.Text = oDealer.City.CityName
        'lblProvince.Text = oDealer.Province.ProvinceName

        'Dim arlBabitAllocation As New ArrayList
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Status", MatchType.Exact, CType(EnumBabit.BabitAllocationStatus.Rilis, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.AllocationType", MatchType.Exact, CType(EnumBabit.BabitAllocationType.Babit_Khusus, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.Exact, oDealer.DealerCode))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.StartPeriod", MatchType.LesserOrEqual, Month(Date.Now)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, Month(Date.Now)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYear", MatchType.Exact, Year(Date.Now)))
        'btnSave.Enabled = False
        'btnSubmit.Enabled = False
        'arlBabitAllocation = New BabitSalesComm.BabitFacade(User).RetrieveBabitAllocation(criterias)
        'If (arlBabitAllocation.Count > 0) Then
        '    btnSave.Enabled = True
        '    btnSubmit.Enabled = True
        '    Dim oBabitAllocation As New BabitAllocation
        '    oBabitAllocation = CType(arlBabitAllocation(0), BabitAllocation)
        '    'Todo session
        '    'Session("BabitAllocation") = oBabitAllocation
        '    sHelper.SetSession("BabitAllocation", oBabitAllocation)
        '    lblNoPerjanjian.Text = oBabitAllocation.NoPerjanjian
        '    lblPeriode.Text = MonthName(oBabitAllocation.Babit.StartPeriod, False) & " sampai " & MonthName(oBabitAllocation.Babit.EndPeriod, False) & " " & oBabitAllocation.Babit.BabitYear
        '    lblSisaAlokasiBabit.Text = Convert.ToInt64(oBabitAllocation.SisaBabit).ToString("#,##0")
        '    btnSave.Enabled = True
        '    txtDana.Enabled = True
        '    ddlJenisKegiatan.Enabled = True
        '    fuBabitKhusus.Visible = True
        'Else
        '    btnSave.Enabled = False
        '    btnSubmit.Enabled = False
        '    txtDana.Enabled = False
        '    ddlJenisKegiatan.Enabled = False
        '    fuBabitKhusus.Visible = False
        '    MessageBox.Show("Alokasi Babit Untuk Periode Ini Tidak Tersedia")
        'End If
    End Sub

    Sub DealerData(ByVal oDealer As Dealer, ByVal oBabitAllocation As BabitAllocation)
        lblDealerName.Text = oDealer.DealerName
        lblCity.Text = oDealer.City.CityName
        lblProvince.Text = oDealer.Province.ProvinceName
        If IsLoginAsDealer() Then
            lblDealerCode.Text = oDealer.DealerCode
            txtDealerCode.Visible = False
            lblPopUpDealer.Visible = False
            btnFindDealer.Visible = False
        Else
            txtDealerCode.Text = oDealer.DealerCode
            txtDealerCode.Visible = True
            lblPopUpDealer.Visible = True
            btnFindDealer.Visible = True
            lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        End If

        If oBabitAllocation Is Nothing Then
            lblNoPerjanjian.Text = String.Empty
            lblPeriode.Text = String.Empty
            lblSisaAlokasiBabit.Text = String.Empty
        Else
            lblNoPerjanjian.Text = oBabitAllocation.NoPerjanjian
            lblPeriode.Text = MonthName(oBabitAllocation.Babit.StartPeriod, False) & " - " & MonthName(oBabitAllocation.Babit.EndPeriod, False) & " " & oBabitAllocation.Babit.BabitYear
            lblSisaAlokasiBabit.Text = Convert.ToInt64(oBabitAllocation.SisaBabit).ToString("#,##0")
        End If
    End Sub

    Sub BindJenisKegiatan()
        Dim arlTmp As New ArrayList
        Dim arl As New ArrayList

        en = New EnumBabit
        ddlJenisKegiatan.DataTextField = "BabitValue"
        ddlJenisKegiatan.DataValueField = "BabitCode"
        arlTmp = en.BabitProposalTypeList()

        Dim objDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            For Each item As BabitItem In arlTmp
                If (item.BabitValue = EnumBabit.BabitProposalType.Pameran.ToString) Then
                    arl.Add(item)
                ElseIf (item.BabitValue = EnumBabit.BabitProposalType.Even.ToString) Then
                    arl.Add(item)
                End If
            Next
            ddlJenisKegiatan.DataSource = arl
            ddlJenisKegiatan.DataBind()
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            For Each item As BabitItem In arlTmp
                If (CekCreatePameranPrivilege()) And (item.BabitValue = EnumBabit.BabitProposalType.Pameran.ToString) Then
                    arl.Add(item)
                    'ElseIf (CekCreateIklanPrivilege()) And (item.BabitValue = EnumBabit.BabitProposalType.Iklan.ToString) Then
                    'arl.Add(item)
                ElseIf (CekCreateEventPrivilege()) And (item.BabitValue = EnumBabit.BabitProposalType.Even.ToString) Then
                    arl.Add(item)
                End If
            Next
            ddlJenisKegiatan.DataSource = arl
            ddlJenisKegiatan.DataBind()
        End If
        ddlJenisKegiatan.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Sub Mode(ByVal isView As Boolean)
        lblJenisKegiatan.Visible = isView
        lblDana.Visible = isView
        'lblUploadFile.Visible = isView

        fuBabitKhusus.Visible = Not isView

        txtDana.Visible = Not isView
        ddlJenisKegiatan.Visible = Not isView
        btnSave.Visible = Not isView
    End Sub

    Sub ViewData(ByVal id As Integer)
        pnlScript.Visible = False
        btnBack.Visible = True
        Dim oBabitProposal As New BabitProposal
        oBabitProposal = New BabitSalesComm.BabitProposalFacade(User).Retrieve(id)
        sHelper.SetSession("BabitProposal", oBabitProposal)
        txtNoPersetujuan.Text = oBabitProposal.NoPengajuan
        ddlJenisKegiatan.SelectedValue = oBabitProposal.ActivityType
        txtDana.Text = oBabitProposal.BabitKhususAmount.ToString("#,##0")
        lblDana.Text = oBabitProposal.BabitKhususAmount.ToString("#,##0")
        txtNoSuratDealer.Text = oBabitProposal.NoSuratDealer
        lblUploadFile.Text = oBabitProposal.FileName
        txtNoSuratDealer.Text = oBabitProposal.NoSuratDealer
        txtNoPersetujuan.Text = oBabitProposal.NoPengajuan

        If Not IsLoginAsDealer() Then
            Dim ktbid As Integer = 0
            'cek if the proposal CreatedBy ktb user, so it can be edit by ktb
            Try
                ktbid = CInt(oBabitProposal.CreatedBy.Substring(0, 6))
                If (ktbid = objDealer.ID) Then
                    btnSubmit.Enabled = True
                Else
                    btnSubmit.Enabled = False
                End If
                If (oBabitProposal.Status <> EnumBabit.StatusBabitProposal.Baru) Then
                    btnSubmit.Enabled = False
                End If
            Catch ex As Exception
                btnSubmit.Enabled = False
            End Try
        ElseIf (oBabitProposal.Status = EnumBabit.StatusBabitProposal.Baru) Then
            btnSubmit.Enabled = True
        Else
            btnSubmit.Enabled = False
        End If

        DealerData(oBabitProposal.Dealer, oBabitProposal.BabitAllocation)

        If (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
            lblJenisKegiatan.Text = EnumBabit.BabitProposalType.Pameran.ToString()
            sHelper.SetSession("BabitProposalPameran", oBabitProposal)
        ElseIf (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Even, Short)) Then
            lblJenisKegiatan.Text = EnumBabit.BabitProposalType.Even.ToString()
            sHelper.SetSession("BabitProposalEvent", oBabitProposal)
        ElseIf (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
            lblJenisKegiatan.Text = EnumBabit.BabitProposalType.Iklan.ToString()
            sHelper.SetSession("BabitProposalIklan", oBabitProposal)
        End If
        btnSave.Text = "Ubah"
        btnSave.Enabled = True
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususView_Privilege) Then
            'Server.Transfer("../FrmAccessDenied.aspx?modulName=Pengajuan BABIT Khusus")
        End If
    End Sub

    Private Function CekCreatePameranPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususCreatepameran_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekCreateIklanPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususCreateIklan_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekCreateEventPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitKhususCreateEvent_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        objDealer = CType(Session("DEALER"), Dealer)
        InitiateAuthorization()
        If (Not IsPostBack) Then
            BindJenisKegiatan()

            If (Request.QueryString("Mode") = "Edit") Then
                ViewData(Convert.ToInt64(Request.QueryString("id")))
                Mode(False)
                txtNoPersetujuan.Enabled = True
                txtNoSuratDealer.Enabled = True
                txtDealerCode.Enabled = True
                txtDana.Enabled = False
            ElseIf (Request.QueryString("Mode") = "View") Then
                ViewData(Convert.ToInt64(Request.QueryString("id")))
                Mode(True)
                txtNoPersetujuan.Enabled = False
                txtNoSuratDealer.Enabled = False
                txtDealerCode.Enabled = False
                txtDana.Enabled = False
            Else
                DealerData(objDealer)
                btnBack.Visible = False
            End If

            ' add security
            If (CekCreatePameranPrivilege()) Or (CekCreateIklanPrivilege()) Or (CekCreateEventPrivilege()) Then
                SetCompPrivilage(True)
            Else
                SetCompPrivilage(False)
            End If
            'Else
            '    If Request.Form("hdnValNew") = "1" Then
            '        btnSave_Click(Nothing, Nothing)
            '        hdnValNew.Value = "-1"
            '        sesHelper.RemoveSession(FU)
            '        sesHelper.RemoveSession(FU_NAME)
            '    ElseIf Request.Form("hdnValNew") = "0" Then
            '        hdnValNew.Value = "-1"
            '        sesHelper.RemoveSession(FU)
            '        sesHelper.RemoveSession(FU_NAME)
            '    End If

            '    If Request.Form("hdnValSubmit") = "1" Then
            '        btnSubmit_Click(Nothing, Nothing)
            '        hdnValSubmit.Value = "-1"
            '    ElseIf Request.Form("hdnValSubmit") = "0" Then
            '        hdnValSubmit.Value = "-1"
            '    End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objTmpDealer As Dealer
        If Not IsLoginAsDealer() Then
            fillDealerInfoFromKTB()
            If (txtDealerCode.Text = String.Empty) Then
                MessageBox.Show("Kode dealer harus diisi")
                btnSave.Enabled = True
                Exit Sub
            End If
            If txtNoPersetujuan.Text = String.Empty Then
                MessageBox.Show("No babit khusus harus diisi.")
                btnSave.Enabled = True
                Exit Sub
            End If
            objTmpDealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(txtDealerCode.Text)
        Else : objTmpDealer = objDealer
        End If

        If (txtDana.Text.Trim = String.Empty) Then
            MessageBox.Show("Dana Babit Khusus harus diisi.")
            btnSave.Enabled = True
            Exit Sub
        Else
            If CDec(txtDana.Text.Trim) < 0 Then
                MessageBox.Show("Dana Babit Khusus harus lebih besar dari 0")
                btnSave.Enabled = True
                Exit Sub
            ElseIf CDec(txtDana.Text.Trim) > 999999999999999999 Then
                MessageBox.Show("Nilai Dana Babit Khusus telah melebihi batas maksimal yg di perbolehkan")
                btnSave.Enabled = True
                Exit Sub
            End If
        End If

        If ddlJenisKegiatan.SelectedIndex = 0 Then
            MessageBox.Show("Jenis Kegiatan harus diisi")
            btnSave.Enabled = True
            Exit Sub
        End If

        If fuBabitKhusus.PostedFile.FileName <> String.Empty Then
            If IsLoginAsDealer() Then
                If fuBabitKhusus.PostedFile.ContentLength = 0 Then
                    MessageBox.Show("File belum diisi")
                    btnSave.Enabled = True
                    Exit Sub
                End If
            End If
            If fuBabitKhusus.PostedFile.ContentLength > MAX_FILE_SIZE Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                btnSave.Enabled = True
                Return
            End If
        End If

        If (Request.QueryString("Mode") = "Edit") Then
            Dim oBabitProposal As BabitProposal = CType(Session("BabitProposal"), BabitProposal)

            Dim fileName As String = DateTime.Now.ToString("ddMMyyyyHHmmss") & "\" & Path.GetFileNameWithoutExtension(fuBabitKhusus.PostedFile.FileName)
            Dim PathVal As String
            Dim ext As String

            If (oBabitProposal.FileName <> String.Empty) Then
                PathVal = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("BabitKhususDir") & "\" & oBabitProposal.FileName
            End If

            If fuBabitKhusus.Value <> "" OrElse fuBabitKhusus.Value <> Nothing Then
                ext = Path.GetExtension(fuBabitKhusus.PostedFile.FileName)
                Dim finfo2 As New FileInfo(fuBabitKhusus.PostedFile.FileName)
                oBabitProposal.FileName = String.Format("{0}{1}", fileName, ext)
            End If

            If fuBabitKhusus.Value <> "" OrElse fuBabitKhusus.Value <> Nothing Then
                Dim fileSize As Integer = MAX_FILE_SIZE
                If fuBabitKhusus.PostedFile.ContentLength > fileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & fileSize & "byte")
                    btnSave.Enabled = True
                    Exit Sub
                Else
                    Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("BabitKhususDir") & "\" & String.Format("{0}{1}", fileName, ext)   '-- Destination file
                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                    Dim success As Boolean = False
                    Dim finfo As New FileInfo(DestFile)
                    Try
                        success = imp.Start()
                        If success Then
                            If Not finfo.Directory.Exists Then
                                Directory.CreateDirectory(finfo.DirectoryName)
                            End If
                            fuBabitKhusus.PostedFile.SaveAs(DestFile)
                            If (File.Exists(PathVal)) Then
                                File.Delete(PathVal)
                            End If

                            imp.StopImpersonate()
                            imp = Nothing
                        End If
                    Catch ex As Exception
                        Throw ex
                        Exit Sub
                    End Try
                End If
            End If

            oBabitProposal.BabitKhususAmount = CDec(txtDana.Text)
            oBabitProposal.ActivityType = ddlJenisKegiatan.SelectedValue
            oBabitProposal.NoSuratDealer = txtNoSuratDealer.Text
            oBabitProposal.NoPengajuan = txtNoPersetujuan.Text

            Dim arl As New ArrayList
            arl.Add(oBabitProposal)
            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(arl) <> -1) Then
                Dim Obj As New BabitProposal
                Obj = New BabitSalesComm.BabitProposalFacade(User).Retrieve(oBabitProposal.ID)
                If (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
                    'Todo session
                    'Session("BabitProposalPameran") = Obj
                    sHelper.SetSession("BabitProposalPameran", Obj)
                    hdnPameran.Value = Obj.NoPengajuan
                ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Even, Short)) Then
                    'Todo session
                    'Session("BabitProposalEvent") = Obj
                    sHelper.SetSession("BabitProposalEvent", Obj)
                    hdnEvent.Value = Obj.NoPengajuan
                ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
                    'Todo session
                    'Session("BabitProposalIklan") = Obj
                    sHelper.SetSession("BabitProposalIklan", Obj)
                    hdnIklan.Value = Obj.NoPengajuan
                End If
                If Not IsLoginAsDealer() Then
                    Dim ktbid As Integer = 0
                    'cek if the proposal CreatedBy ktb user, so it can be edit by ktb
                    Try
                        ktbid = CInt(obj.CreatedBy.Substring(0, 6))
                        If (ktbid = objDealer.ID) Then
                            If (obj.Status = EnumBabit.StatusBabitProposal.Baru) Then
                                btnSubmit.Enabled = True
                            Else
                                btnSubmit.Enabled = False
                            End If
                        Else
                            btnSubmit.Enabled = False
                        End If
                    Catch ex As Exception
                        btnSubmit.Enabled = False
                    End Try
                    btnSave.Enabled = True
                ElseIf (obj.Status = EnumBabit.StatusBabitProposal.Baru) Then
                    btnSubmit.Enabled = True
                End If
                lblUploadFile.Text = oBabitPRoposal.FileName
                MessageBox.Show(SR.UpdateSucces)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            Dim oBabitProposal As New BabitProposal
            oBabitProposal.BabitAllocation = CType(Session("BabitAllocation"), BabitAllocation)
            oBabitProposal.BabitKhususAmount = CDec(txtDana.Text)
            oBabitProposal.Dealer = objTmpDealer
            oBabitProposal.FileName = String.Empty
            oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Baru, Short)
            oBabitProposal.ActivityType = ddlJenisKegiatan.SelectedValue
            oBabitProposal.NoPengajuan = txtNoPersetujuan.Text
            oBabitProposal.NoSuratDealer = txtNoSuratDealer.Text

            Dim fileName As String = DateTime.Now.ToString("ddMMyyyyHHmmss") & "\" & Path.GetFileNameWithoutExtension(fuBabitKhusus.PostedFile.FileName)
            Dim ext As String

            If fuBabitKhusus.Value <> "" OrElse fuBabitKhusus.Value <> Nothing Then
                ext = Path.GetExtension(fuBabitKhusus.PostedFile.FileName)
                Dim finfo2 As New FileInfo(fuBabitKhusus.PostedFile.FileName)
                oBabitProposal.FileName = String.Format("{0}{1}", fileName, ext)
            End If

            If fuBabitKhusus.Value <> "" OrElse fuBabitKhusus.Value <> Nothing Then
                Dim fileSize As Integer = MAX_FILE_SIZE
                If fuBabitKhusus.PostedFile.ContentLength > fileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & fileSize & "byte")
                    btnSave.Enabled = True
                    Exit Sub
                Else
                    Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("BabitKhususDir") & "\" & String.Format("{0}{1}", fileName, ext)   '-- Destination file
                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                    Dim success As Boolean = False
                    Dim finfo As New FileInfo(DestFile)
                    Try
                        success = imp.Start()
                        If success Then
                            If Not finfo.Directory.Exists Then
                                Directory.CreateDirectory(finfo.DirectoryName)
                            End If
                            fuBabitKhusus.PostedFile.SaveAs(DestFile)

                            imp.StopImpersonate()
                            imp = Nothing
                        End If
                    Catch ex As Exception
                        Throw ex
                        Exit Sub
                    End Try
                End If
            End If

            If (New BabitSalesComm.BabitProposalFacade(User).Insert(oBabitProposal) <> -1) Then
                Dim Obj As New BabitProposal
                Obj = New BabitSalesComm.BabitProposalFacade(User).Retrieve(oBabitProposal.ID)
                If (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
                    'Todo session
                    'Session("BabitProposalPameran") = Obj
                    sHelper.SetSession("BabitProposalPameran", Obj)
                    hdnPameran.Value = Obj.NoPengajuan
                ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Even, Short)) Then
                    'Todo session
                    'Session("BabitProposalEvent") = Obj
                    sHelper.SetSession("BabitProposalEvent", Obj)
                    hdnEvent.Value = Obj.NoPengajuan
                ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
                    'Todo session
                    'Session("BabitProposalIklan") = Obj
                    sHelper.SetSession("BabitProposalIklan", Obj)
                    hdnIklan.Value = Obj.NoPengajuan
                End If
                MessageBox.Show(SR.SaveSuccess)
                btnSubmit.Enabled = True
                btnSave.Enabled = False
                lblUploadFile.Text = oBabitProposal.FileName
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If fuBabitKhusus.PostedFile.FileName <> String.Empty Then
            If fuBabitKhusus.PostedFile.ContentLength = 0 Then
                MessageBox.Show("File invalid")
                Exit Sub
            End If
        End If

        If (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
            Dim oBabitProposal As BabitProposal = CType(Session("BabitProposalPameran"), BabitProposal)
            oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Validasi, Short)
            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(oBabitProposal) <> -1) Then
                btnSubmit.Enabled = False
                hdnPameranSubmit.Value = oBabitProposal.NoPengajuan
                MessageBox.Show("Validasi berhasil")
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Even, Short)) Then
            Dim oBabitProposal As BabitProposal = CType(Session("BabitProposalEvent"), BabitProposal)
            oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Validasi, Short)
            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(oBabitProposal) <> -1) Then
                btnSubmit.Enabled = False
                hdnEventSubmit.Value = oBabitProposal.NoPengajuan
                MessageBox.Show("Validasi berhasil")
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
            Dim oBabitProposal As BabitProposal = CType(Session("BabitProposalIklan"), BabitProposal)
            oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Validasi, Short)
            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(oBabitProposal) <> -1) Then
                btnSubmit.Enabled = False
                hdnIklanSubmit.Value = oBabitProposal.NoPengajuan
                MessageBox.Show("Validasi berhasil")
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub

    'Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
    '    Response.Redirect("../Babit/FrmListPengajuanBabitKhusus.aspx", True)
    'End Sub

    Private Sub btnFindDealer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindDealer.Click
        fillDealerInfoFromKTB()
        If (btnSave.Enabled = False) Then
            MessageBox.Show("Alokasi babit khusus untuk periode ini tidak tersedia")
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        btnSave.Enabled = True
        btnSubmit.Enabled = False
        ddlJenisKegiatan.SelectedIndex = 0
        txtNoSuratDealer.Text = String.Empty
        txtDana.Text = "0"
        txtDealerCode.Text = ""
        txtNoPersetujuan.Text = ""
    End Sub

#End Region

End Class
