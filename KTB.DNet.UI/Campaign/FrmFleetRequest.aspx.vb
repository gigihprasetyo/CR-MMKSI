Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Imports System.Text
Imports System.IO

Public Class FrmFleetRequest
#Region "Private Variable"

    Inherits System.Web.UI.Page

    Private sessHelper As New SessionHelper
    Private oDealer As New Dealer
    Private oLoginUser As New UserInfo

    Private oFleetRequest As FleetRequest
    Private oFleetRequestFacade As New FleetRequestFacade(User)

    Private Mode As enumMode.Mode

    Private AttachmentDirectory As String
    Private TargetDirectory As String
    Private TempDirectory As String
    Private _sessFleetRequest As String = "FrmFleetRequest._sessFleetRequest"
    Private _sessNEW_FleetRequest As String = "FrmFleetRequest._sessNEW_FleetRequest"

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents ddlFleetNumber As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblReqNum As System.Web.UI.WebControls.Label
    Protected WithEvents icTglPengajuan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNamaKonsumen As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatusKonsumen As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlProfilBisnis As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtKebutuhanUnit As System.Web.UI.WebControls.TextBox
    Protected WithEvents icTglMulaiPengadaan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglSelesaiPengadaan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents linkDeleteAttachment As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkAttachment As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal2 As System.Web.UI.WebControls.Button
    Protected WithEvents file1 As System.Web.UI.HtmlControls.HtmlInputFile



    Protected WithEvents lblFleetNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaKonsumen As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusKonsumen As System.Web.UI.WebControls.Label
    Protected WithEvents lblProfilBisnis As System.Web.UI.WebControls.Label
    Protected WithEvents lblKebutuhanUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglMulaiPengadaan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglSelesaiPengadaan As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        AttachmentDirectory = KTB.DNet.Lib.WebConfig.GetValue("FleetAttachment")
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")

    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If SecurityProvider.Authorize(Context.User, SR.Lihat_Daftar_Fleet_Privilege) Then
            If Request.QueryString("Mode").ToString() = "New" OrElse Request.QueryString("Mode").ToString() = "Edit" Then
                If Not SecurityProvider.Authorize(Context.User, SR.Input_Pengajuan_Fleet_Privilege) Then
                    btnSimpan.Visible = False
                End If
            End If
        Else
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PENGAJUAN EXTENDED FREE SERVICE - Buat/Edit")
        End If
    End Sub

    'Dim bCekPriv As Boolean = (SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege))

#End Region


    Private Sub ClearForm()

        Dim ObjMainDealer As Dealer = CType(Session("DEALER"), Dealer)
        If ObjMainDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblDealerCode.Text = ObjMainDealer.DealerCode
            lblDealerName.Text = ObjMainDealer.DealerName
            lblDealerTerm.Text = ObjMainDealer.SearchTerm2
        End If

        ddlFleetNumber.SelectedIndex = 0
        lblReqNum.Text = "[AutoNumber]"
        txtNamaKonsumen.Text = ""
        ddlStatusKonsumen.SelectedIndex = 0
        ddlProfilBisnis.SelectedIndex = 0
        txtKebutuhanUnit.Text = ""
        lblStatus.Text = "Baru"

    End Sub

    Private Sub LoadForm()
        lblDealerCode.Text = oFleetRequest.FleetMasterDealer.Dealer.DealerCode
        lblDealerName.Text = oFleetRequest.FleetMasterDealer.Dealer.DealerName
        lblDealerTerm.Text = oFleetRequest.FleetMasterDealer.Dealer.SearchTerm2
        ddlFleetNumber.SelectedValue = oFleetRequest.FleetMasterDealer.FleetMaster.ID
        lblReqNum.Text = oFleetRequest.NoRegRequest
        txtNamaKonsumen.Text = oFleetRequest.NamaKonsumen
        ddlStatusKonsumen.SelectedValue = oFleetRequest.StatusKonsumen
        ddlProfilBisnis.SelectedValue = oFleetRequest.ProfilBisnis
        txtKebutuhanUnit.Text = oFleetRequest.KebutuhanUnit
        lblStatus.Text = CType(oFleetRequest.Status, EnumFleetRequest.FleetRequestStatus).ToString
        icTglPengajuan.Value = oFleetRequest.TanggalPengajuan.ToString("dd/MM/yyyy")
        icTglMulaiPengadaan.Value = oFleetRequest.MulaiPengadaan.ToString("dd/MM/yyyy")
        icTglSelesaiPengadaan.Value = oFleetRequest.SelesaiPengadaan.ToString("dd/MM/yyyy")
        linkAttachment.Text = oFleetRequest.Attachment
        'If linkAttachment.Text <> "" Then linkDeleteAttachment.Visible = True Else linkDeleteAttachment.Visible = False
    End Sub

    Private Sub fillForm()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        oFleetRequest = CType(sessHelper.GetSession(_sessFleetRequest), FleetRequest)

        If Mode = enumMode.Mode.NewItemMode Then
            oFleetRequest = CType(sessHelper.GetSession(_sessNEW_FleetRequest), FleetRequest)
            ClearForm()
        ElseIf Mode = enumMode.Mode.EditMode OrElse Mode = enumMode.Mode.ViewMode Then
            LoadForm()
        End If

        setFormView()

    End Sub


    Private Sub setFormView()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        oFleetRequest = CType(sessHelper.GetSession(_sessFleetRequest), FleetRequest)

        If Mode = enumMode.Mode.ViewMode Then
            lblFleetNumber.Text = ddlFleetNumber.SelectedItem.Text
            lblTglPengajuan.Text = icTglPengajuan.Value
            lblNamaKonsumen.Text = txtNamaKonsumen.Text
            lblStatusKonsumen.Text = ddlStatusKonsumen.SelectedItem.Text
            lblProfilBisnis.Text = ddlProfilBisnis.SelectedItem.Text
            lblKebutuhanUnit.Text = txtKebutuhanUnit.Text
            lblTglMulaiPengadaan.Text = icTglMulaiPengadaan.Value
            lblTglSelesaiPengadaan.Text = icTglSelesaiPengadaan.Value

            lblFleetNumber.Visible = True
            lblTglPengajuan.Visible = True
            lblNamaKonsumen.Visible = True
            lblStatusKonsumen.Visible = True
            lblProfilBisnis.Visible = True
            lblKebutuhanUnit.Visible = True
            lblTglMulaiPengadaan.Visible = True
            lblTglSelesaiPengadaan.Visible = True

            ddlFleetNumber.Visible = False
            icTglPengajuan.Visible = False
            txtNamaKonsumen.Visible = False
            ddlStatusKonsumen.Visible = False
            ddlProfilBisnis.Visible = False
            txtKebutuhanUnit.Visible = False
            icTglMulaiPengadaan.Visible = False
            icTglSelesaiPengadaan.Visible = False
            file1.Visible = False
        End If

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

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetMasterDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arrTmp As ArrayList

        If ObjMainDealer.TitleDealer = "DEALER" Then
            criteria.opAnd(New Criteria(GetType(FleetMasterDealer), "Dealer.ID", MatchType.Exact, ObjMainDealer.ID))
        End If

        arrTmp = New FleetMasterDealerFacade(User).Retrieve(criteria)

        With ddlFleetNumber.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each objTmp As FleetMasterDealer In arrTmp
                .Add(New ListItem(objTmp.FleetMaster.NoFleet, objTmp.ID))
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

    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)

        AttachmentDirectory = KTB.DNet.Lib.WebConfig.GetValue("FleetAttachmentDir")
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")

        InitiateAuthorization()
        If Not IsPostBack Then

            BindFleetNumber()
            BindStatusKonsumen()
            BindProfilBisnis()
            If Request.QueryString("Mode").ToString() = "New" Then
                ViewState("Mode") = enumMode.Mode.NewItemMode
                oFleetRequest = New FleetRequest

                sessHelper.SetSession(_sessNEW_FleetRequest, oFleetRequest)

                btnKembali.Visible = False
                btnSimpan.Visible = True

            ElseIf Request.QueryString("Mode").ToString() = "Edit" OrElse Request.QueryString("Mode").ToString() = "View" Then
                If Request.QueryString("Mode").ToString() = "Edit" Then
                    ViewState("Mode") = enumMode.Mode.EditMode
                    btnSimpan.Visible = True
                ElseIf Request.QueryString("Mode").ToString() = "View" Then
                    ViewState("Mode") = enumMode.Mode.ViewMode
                    btnSimpan.Visible = False
                    btnBatal2.Visible = False
                End If

                Dim objFleetRequestFacade As FleetRequestFacade = New FleetRequestFacade(User)
                oFleetRequest = objFleetRequestFacade.Retrieve(CInt(Request.QueryString("ID")))

                sessHelper.SetSession(_sessFleetRequest, oFleetRequest)

                If Request.QueryString("Src").ToString <> "" Then btnKembali.Visible = True
            End If

            fillForm()

        End If


    End Sub



    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim result As Integer
        Dim ErrMessage As String = String.Empty

        If Not Page.IsValid Then
            Return
        End If

        If ValidateSaveData() Then


            BindFleetRequestDomain()
            If Mode = enumMode.Mode.NewItemMode Then
                oFleetRequest = sessHelper.GetSession(_sessNEW_FleetRequest)
            End If

            Mode = CType(ViewState("Mode"), enumMode.Mode)
            Dim oFleetRequestFacade As FleetRequestFacade = New FleetRequestFacade(User)
            If Mode = enumMode.Mode.NewItemMode Then
                result = oFleetRequestFacade.Insert(oFleetRequest)
                If result > 0 Then oFleetRequest = oFleetRequestFacade.Retrieve(result)
            ElseIf Mode = enumMode.Mode.EditMode Then
                result = oFleetRequestFacade.Update(oFleetRequest)
            End If


            If result > 0 Then

                MessageBox.Show(SR.SaveSuccess)

                '--- Upload Attachment
                Dim strAttachment As String = UploadAttachment(oFleetRequest.NoRegRequest)

                If strAttachment <> "" Then
                    oFleetRequest.Attachment = strAttachment
                    result = oFleetRequestFacade.Update(oFleetRequest)
                End If

                If result > 0 Then
                    sessHelper.SetSession(_sessFleetRequest, oFleetRequest)
                    ReloadForm(result)
                End If
            Else
                ErrMessage = Err.Description
                If ErrMessage = String.Empty Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(ErrMessage)
                End If

            End If

        Else
            MessageBox.Show("Data tidak lengkap atau belum tepat. Silakan diperiksa lagi ")
        End If


    End Sub
    Private Sub ReloadForm(ByVal id As Integer)
        oFleetRequest = CType(sessHelper.GetSession(_sessFleetRequest), FleetRequest)

        If oDealer.Title = 0 Then 'Dealer
            Select Case CType(oFleetRequest.Status, EnumFleetRequest.FleetRequestStatus)
                Case EnumFleetRequest.FleetRequestStatus.Batal
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumFleetRequest.FleetRequestStatus.Baru
                    ViewState("Mode") = enumMode.Mode.EditMode
                Case EnumFleetRequest.FleetRequestStatus.Validasi
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumFleetRequest.FleetRequestStatus.BatalValidasi
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumFleetRequest.FleetRequestStatus.Konfirmasi
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumFleetRequest.FleetRequestStatus.BatalKonfirmasi
                    ViewState("Mode") = enumMode.Mode.ViewMode
            End Select
        ElseIf oDealer.Title = 1 Then ' KTB
            ViewState("Mode") = enumMode.Mode.ViewMode
        End If


        'If CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.EditMode Then
        '    Server.Transfer("~/Campaign/FrmFleetRequest.aspx?Mode=Edit")
        'ElseIf CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.ViewMode Then
        '    Server.Transfer("~/Campaign/FrmFleetRequest.aspx?Mode=View")
        'End If

        fillForm()

    End Sub


    Private Function UploadAttachment(ByVal strNoRegRequest As String) As String
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Dim strReturn As String = ""

        Try
            success = imp.Start()
            If success Then
                If (Not file1.PostedFile Is Nothing) AndAlso (file1.PostedFile.ContentLength > 0) Then

                    strReturn = Path.GetFileName(file1.PostedFile.FileName)

                    Dim DestFile As String = TargetDirectory & AttachmentDirectory & "\" & strNoRegRequest & "\" & strReturn
                    'strError = "1"
                    ''---------- Pake ini Error -----------
                    'dfChassis.PostedFile.SaveAs(DestFile) '-- Copy source file to destination file

                    ''---------- Pake UploadToWebServer saja -----------
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(file1.PostedFile.InputStream, DestFile)
                    'strError = "2"

                End If
                imp.StopImpersonate()
                imp = Nothing
            End If

        Catch Ex As Exception
            MessageBox.Show(SR.UploadFail(strReturn))
            strReturn = ""
        End Try

        Return strReturn
    End Function


    Private Sub BindFleetRequestDomain()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        oFleetRequest = CType(sessHelper.GetSession(_sessFleetRequest), FleetRequest)

        If Mode = enumMode.Mode.NewItemMode Then
            oFleetRequest = CType(sessHelper.GetSession(_sessNEW_FleetRequest), FleetRequest)

        ElseIf Mode = enumMode.Mode.EditMode Then
            'oFleetRequest.NoRegRequest = "test001" 'lblReqNum.Text
        End If

        Dim oFleetMasterDealer As FleetMasterDealer = New FleetMasterDealerFacade(User).Retrieve(CInt(ddlFleetNumber.SelectedValue))
        oFleetRequest.FleetMasterDealer = oFleetMasterDealer

        oFleetRequest.TanggalPengajuan = icTglPengajuan.Value
        oFleetRequest.NamaKonsumen = txtNamaKonsumen.Text
        oFleetRequest.StatusKonsumen = ddlStatusKonsumen.SelectedValue
        oFleetRequest.ProfilBisnis = ddlProfilBisnis.SelectedValue
        oFleetRequest.KebutuhanUnit = txtKebutuhanUnit.Text
        oFleetRequest.MulaiPengadaan = icTglMulaiPengadaan.Value
        oFleetRequest.SelesaiPengadaan = icTglSelesaiPengadaan.Value
        oFleetRequest.Attachment = ""
        oFleetRequest.Status = 0

        If Mode = enumMode.Mode.NewItemMode Then
            sessHelper.SetSession(_sessNEW_FleetRequest, oFleetRequest)
        End If

    End Sub


    Private Function ValidateSaveData() As Boolean


        If txtNamaKonsumen.Text = String.Empty Then
            MessageBox.Show("Nama Konsumen masih kosong")
            Return False
        End If


        If txtKebutuhanUnit.Text = String.Empty Then
            MessageBox.Show("Kebutuhan Unit masih kosong")
            Return False
        End If


        If ddlFleetNumber.SelectedIndex = 0 Then
            MessageBox.Show("Silakan Pilih No Surat MFTBC.")
            Return False
        End If

        If ddlStatusKonsumen.SelectedIndex = 0 Then
            MessageBox.Show("Silakan Pilih Status Konsumen.")
            Return False
        End If

        If ddlProfilBisnis.SelectedIndex = 0 Then
            MessageBox.Show("Silakan Pilih Profil Bisnis.")
            Return False
        End If

        If CDate(icTglPengajuan.Value) > CDate(icTglMulaiPengadaan.Value) Then
            MessageBox.Show("Tanggal pengajuan tidak boleh melebihi tanggal mulai pengadaan.")
            Return False
        End If

        If CDate(icTglMulaiPengadaan.Value) > CDate(icTglSelesaiPengadaan.Value) Then
            MessageBox.Show("Tanggal mulai pengadaan tidak boleh melebihi tanggal selesai pengadaan.")
            Return False
        End If

        If (file1.PostedFile Is Nothing) OrElse (file1.PostedFile.ContentLength <= 0) Then
            MessageBox.Show("Silahkan lampirkan berkas.")
            Return False
        End If

        Return True

    End Function


    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmFleetRequestList.aspx")
    End Sub

    Private Sub linkDeleteAttachment_Command(sender As Object, e As CommandEventArgs) Handles linkDeleteAttachment.Command
        oFleetRequest = sessHelper.GetSession(_sessFleetRequest)
        Dim oFleetRequestFacade As FleetRequestFacade = New FleetRequestFacade(User)
        Dim result As Integer = 0

        oFleetRequest.Attachment = ""
        result = oFleetRequestFacade.Update(oFleetRequest)

        If result > 0 Then
            sessHelper.SetSession(_sessFleetRequest, oFleetRequest)
            ReloadForm(result)

            '---delete Attachment
            'Dim finfo As FileInfo
            'Dim DestFile As String = Server.MapPath("") & "\..\DataFile\FleetRequest\" & oFleetRequest.NoRegRequest.Trim

            'finfo = New FileInfo(DestFile)
            'If finfo.Exists Then
            '    finfo.Delete()
            'End If
        Else
            MessageBox.Show("Hapus Attachment Gagal!")
        End If
    End Sub

    Private Sub linkAttachment_Command(sender As Object, e As CommandEventArgs) Handles linkAttachment.Command
        Response.Redirect("../Download.aspx?file=" & AttachmentDirectory & "\" & lblReqNum.Text & "\" & linkAttachment.Text)
    End Sub

    Protected Sub btnBatal2_Click(sender As Object, e As EventArgs) Handles btnBatal2.Click
        fillForm()
    End Sub
End Class
