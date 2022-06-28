Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class FrmBuatKuitansiPencairanDepositA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipePengajuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNomorKuitansi As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalKwitansi As System.Web.UI.WebControls.Label
    Protected WithEvents lblTelahTerimaDari As System.Web.UI.WebControls.Label
    Protected WithEvents lblUangSejumlah As System.Web.UI.WebControls.Label
    Protected WithEvents lblUangPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents txtSign As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents lblFooter As System.Web.UI.WebControls.Label
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents btnCetak As System.Web.UI.WebControls.Button
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRekening As System.Web.UI.WebControls.Label
    Protected WithEvents txtJabatan As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlNoRegPengajuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNoRefSuratPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents imgDealer As System.Web.UI.WebControls.ImageButton
    Protected WithEvents btnImgDealer As System.Web.UI.WebControls.Button
    Protected WithEvents lblProduk As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Internal Enum"
    'Private Enum TipePengajuan
    '    Offset = 1
    '    CashTahunan = 2
    '    CashIncidental = 3
    '    CashInterest = 4
    'End Enum

    'Private Enum XStatusPencairan
    '    Baru = 0
    '    Validasi = 1
    '    BatalValidasi = 2
    '    Konfirmasi = 3
    '    Setuju = 4
    '    Pending = 5
    '    Tolak = 6
    'End Enum

    'Private Enum EnumDepositA.StatusPencairanKTB
    '    Konfirmasi = 10
    '    Setuju = 11
    '    Tolak = 12
    '    Pending = 13
    '    Blok = 14
    '    Selesai = 16
    'End Enum

    'Private Enum XStatusKuitansiDealer
    '    Baru = 0
    '    Validasi = 1
    'End Enum

    'Private Enum XStatusKuitansiKTB
    '    Validasi = 1
    '    Konfirmasi = 10
    '    Printed = 11
    'End Enum

#End Region

#Region "Private Variables"
    Private arlDepositAPencairan As ArrayList = New ArrayList
    Private arlDepositAPencairanFilter As ArrayList = New ArrayList
    Private objDepositAPencairanH As New DepositAPencairanH
    Private objDepositAKuitansi As New DepositAKuitansiPencairan
    Private objUserInfo As UserInfo
    Dim sHelper As New SessionHelper
    Dim IsDealer As Boolean = True
    Const DocTypePengajuan = 1
    Const DocTypeKuitansi = 2

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            ' btnCetak.Attributes("onclick") = "PrintDocument();"

            objUserInfo = sHelper.GetSession("LOGINUSERINFO")
            lblFooter.Text = objUserInfo.Dealer.City.CityName & " , " & DateTime.Now.ToString("dd/MM/yyyy")
            If Not Request.QueryString("id") Is Nothing Then
                BindDataToForm(CInt(Request.QueryString("id")))
                btnKembali.Visible = True
                ViewState("NoKuitansi") = Request.QueryString("NoKuitansi")
                ViewState("PeriodeFromKuitansi") = Request.QueryString("PeriodeFromKuitansi")
                ViewState("PeriodeToKuitansi") = Request.QueryString("PeriodeToKuitansi")
                ViewState("NoPengajuan") = Request.QueryString("NoPengajuan")
                ViewState("PeriodeFromPengajuan") = Request.QueryString("PeriodeFromPengajuan")
                ViewState("PeriodeToPengajuan") = Request.QueryString("PeriodeToPengajuan")
            Else
                BindToForm()
                btnKembali.Visible = False
            End If
        End If
    End Sub

    Private Sub InitiateAuthorization()
        objUserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(context.User, SR.DepositA_kuitansi_pencairan_buat_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Deposit A - Buat Kuitansi Pencairan")
                Me.btnSimpan.Visible = False
            End If
        Else
            If Not SecurityProvider.Authorize(context.User, SR.DepositA_daftar_kuitansi_pencairan_depositA_lihat_detail_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Deposit A - Lihat Detail Kuitansi Pencairan")
                Me.btnSimpan.Visible = False
            End If
        End If


    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        EnableControl()
        BindToForm()
        ddlNoRegPengajuan.Items.Clear()
        ddlNoRegPengajuan.Items.Add("Silahkan Pilih")
    End Sub

    Private Sub btnValidasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidasi.Click
        Dim objDepositAKuitansiPencairan As New DepositAKuitansiPencairan
        Dim intResult As Integer
        objDepositAKuitansiPencairan = New DepositAKuitansiPencairanFacade(User).Retrieve(CType(sHelper.GetSession("DepositID"), Integer))
        If Not objDepositAKuitansiPencairan Is Nothing Then
            If objDepositAKuitansiPencairan.ID > 0 Then
                objDepositAKuitansiPencairan.Status = EnumDepositA.StatusPencairan.Validasi ' StatusPencairan.Validasi
                intResult = New DepositAKuitansiPencairanFacade(User).Update(objDepositAKuitansiPencairan)
                If intResult = -1 Then
                    MessageBox.Show("Validasi Gagal")
                Else
                    MessageBox.Show("Validasi Berhasil")
                    btnValidasi.Enabled = False
                End If
            End If
        Else
            MessageBox.Show("Validasi Gagal")
        End If
    End Sub
    Private Sub ddlTipePengajuan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTipePengajuan.SelectedIndexChanged
        If txtKodeDealer.Text.Trim <> "" Then
            lblProduk.Text = ""
            If ddlTipePengajuan.SelectedIndex <> 0 Then
                ClearForm()
                Dim objDealer As Dealer
                If Not sHelper.GetSession("OBJDEALER") Is Nothing Then
                    objDealer = CType(sHelper.GetSession("OBJDEALER"), Dealer)
                End If

                Dim objKwitansi As DepositAKuitansiPencairan
                ddlNoRegPengajuan.Items.Clear()
                ddlNoRegPengajuan.Items.Add("Silahkan Pilih")
                objUserInfo = sHelper.GetSession("LOGINUSERINFO")
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Type", MatchType.Exact, ddlTipePengajuan.SelectedIndex))
                If Request.QueryString("id") Is Nothing Then
                    criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Status", MatchType.Exact, CType(EnumDepositA.StatusPencairanKTB.Setuju, Integer)))
                End If
                If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Dealer.ID", MatchType.Exact, objDealer.ID))
                End If

                arlDepositAPencairanFilter = New DepositAPencairanHFacade(User).Retrieve(criterias)
                If arlDepositAPencairanFilter.Count > 0 Then
                    For Each item As DepositAPencairanH In arlDepositAPencairanFilter
                        'objKwitansi = New DepositAKuitansiPencairanFacade(User).Retrieve(item.NoSurat)
                        'If objKwitansi.ID = 0 Then
                        ddlNoRegPengajuan.Items.Add(item.NoReg)
                        'End If
                    Next
                End If
                If Not Request.QueryString("id") Is Nothing Then
                    objDepositAKuitansi = New DepositAKuitansiPencairanFacade(User).Retrieve(CType(Request.QueryString("id"), Integer))
                    ddlNoRegPengajuan.SelectedValue = objDepositAKuitansi.NoReg
                    ddlNoRegPengajuan_SelectedIndexChanged(Nothing, Nothing)
                End If
            Else
                ddlNoRegPengajuan.Items.Clear()
                ddlNoRegPengajuan.Items.Add("Silahkan Pilih")
            End If
        Else
            MessageBox.Show("Tentukan Dealer terlebih dahulu.")
        End If

    End Sub


    Private Sub ddlNoRegPengajuan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlNoRegPengajuan.SelectedIndexChanged
        If ddlNoRegPengajuan.SelectedIndex <> 0 Then
            ClearForm()
            Dim objDepositA As New DepositAPencairanH
            Dim objDealer As Dealer = CType(sHelper.GetSession("OBJDEALER"), Dealer)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Type", MatchType.Exact, ddlTipePengajuan.SelectedIndex))
            If Request.QueryString("id") Is Nothing Then
                criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Status", MatchType.Exact, CType(EnumDepositA.StatusPencairanKTB.Setuju, Integer)))
            End If
            'criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Dealer.ID", MatchType.Exact, objUserInfo.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "NoReg", MatchType.Exact, ddlNoRegPengajuan.SelectedValue.ToString))
            arlDepositAPencairanFilter = New DepositAPencairanHFacade(User).Retrieve(criterias)

            If arlDepositAPencairanFilter.Count > 0 Then
                objDepositA = arlDepositAPencairanFilter(0)
                If Not IsNothing(objDepositA) Then
                    lblNoRefSuratPengajuan.Text = objDepositA.NoSurat
                    Dim selectedTipe As EnumDepositA.TipePengajuan = CType([Enum].Parse(GetType(EnumDepositA.TipePengajuan), ddlTipePengajuan.SelectedValue), EnumDepositA.TipePengajuan)
                    If selectedTipe = EnumDepositA.TipePengajuan.Offset Then
                        lblUangSejumlah.Text = FormatNumber(objDepositA.DealerAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Else
                        lblUangSejumlah.Text = FormatNumber(objDepositA.ApprovalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    End If
                    lblTglPengajuan.Text = Format(objDepositA.CreatedTime, "dd/MM/yyyy")
                    lblProduk.Text = objDepositA.ProductCategory.Code
                    If Not objDepositA.DealerBankAccount Is Nothing Then
                        If objDepositA.DealerBankAccount.ID > 0 Then
                            lblNoRekening.Text = objDepositA.DealerBankAccount.BankAccount
                        Else
                            lblNoRekening.Text = String.Empty
                        End If
                    End If

                    lblUangPembayaran.Text = GetDetailDescriptions(objDepositA.DepositAPencairanDs)
                End If
            End If
        End If
    End Sub


    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        If ddlTipePengajuan.SelectedIndex = 0 And Request.QueryString("id") Is Nothing Then
            MessageBox.Show("Pilih dulu tipe pengajuan pencairan")
            Exit Sub
        End If

        If ddlNoRegPengajuan.SelectedIndex = 0 Then
            If ddlNoRegPengajuan.SelectedIndex = 0 And Request.QueryString("id") Is Nothing Then
                MessageBox.Show("Pilih dulu nomor registrasi surat pengajuan")
                Exit Sub
            End If
        End If

        If txtNomorKuitansi.Text.Trim = String.Empty Then
            MessageBox.Show("Isi dulu nomor kwitansinya")
            Exit Sub
        End If

        If txtNomorKuitansi.Text.Trim = String.Empty Then
            MessageBox.Show("Isi dulu nomor kwitansinya")
            Exit Sub
        End If

        If txtSign.Text.Trim = String.Empty Then
            MessageBox.Show("Isi dulu nama signnya")
            Exit Sub
        End If


        Dim intResult = CInt(sHelper.GetSession("DepositID"))
        If intResult > 0 Then
            objDepositAKuitansi = New DepositAKuitansiPencairanFacade(User).Retrieve(intResult)
            If objDepositAKuitansi.ID = 0 Then
                MessageBox.Show(SR.UpdateFail)
                Exit Sub
            End If
        End If


        'objUserInfo = sHelper.GetSession("LOGINUSERINFO")
        Dim objDealer As Dealer = CType(sHelper.GetSession("OBJDEALER"), Dealer)
        If objDealer.ID > 0 Then

            If Request.QueryString("id") Is Nothing Then
                'objDepositAKuitansi.Dealer = objUserInfo.Dealer
                'objDepositAKuitansi.Type = ddlTipePengajuan.SelectedIndex
                'objDepositAKuitansi.NoSurat = lblNoRefSuratPengajuan.Text
                Dim objSurat As New DepositAPencairanH
                Dim objKuitansi As New DepositAKuitansiPencairan
                objSurat = New DepositAPencairanHFacade(User).Retrieve(ddlNoRegPengajuan.SelectedItem.Text)
                If objSurat.ID > 0 Then
                    Dim iResult As Integer
                    objSurat.Status = EnumDepositA.StatusPencairanKTB.Selesai
                    iResult = New DepositAPencairanHFacade(User).Update(objSurat)
                    If iResult > -1 Then
                        objKuitansi = New DepositAKuitansiPencairanFacade(User).Retrieve(objSurat.NoReg)
                        If Not (objKuitansi.ID > 0) Then
                            objKuitansi = New DepositAKuitansiPencairan
                            objKuitansi.Dealer = objDealer
                            objKuitansi.ProductCategory = New ProductCategoryFacade(User).Retrieve(lblProduk.Text)
                            objKuitansi.Type = ddlTipePengajuan.SelectedIndex
                            objKuitansi.NoSurat = lblNoRefSuratPengajuan.Text
                            objKuitansi.DNNumber = objSurat.DNNumber
                            objKuitansi.AssignmentNumber = objSurat.AssignmentNumber
                            objKuitansi.RequestedTime = New Date(lblTglPengajuan.Text.Trim.Substring(6, 4), lblTglPengajuan.Text.Trim.Substring(3, 2), lblTglPengajuan.Text.Trim.Substring(0, 2))
                            objKuitansi.Description = lblUangPembayaran.Text
                            objKuitansi.Status = EnumDepositA.StatusPencairan.Baru
                            objKuitansi.ReceiptNumber = txtNomorKuitansi.Text
                            objKuitansi.ReceiptDate = New Date(lblTanggalKwitansi.Text.Trim.Substring(6, 4), lblTanggalKwitansi.Text.Trim.Substring(3, 2), lblTanggalKwitansi.Text.Trim.Substring(0, 2))
                            objKuitansi.TotalAmount = CDec(lblUangSejumlah.Text)
                            objKuitansi.SignedBy = txtSign.Text
                            objKuitansi.Jabatan = txtJabatan.Text
                            objKuitansi.NoReg = ddlNoRegPengajuan.SelectedValue.ToString
                            intResult = New DepositAKuitansiPencairanFacade(User).Insert(objKuitansi)
                            If intResult = -1 Then
                                MessageBox.Show(SR.SaveFail)
                                btnValidasi.Enabled = False
                            Else
                                Dim nResult As Integer = InsertHistory(objKuitansi.NoSurat, objKuitansi.Status, EnumDepositA.StatusKuitansiDealer.Baru, DocTypeKuitansi)
                                If nResult > -1 Then
                                    sHelper.SetSession("DepositID", intResult)
                                    MessageBox.Show(SR.SaveSuccess)
                                    DisableControl()
                                    btnSimpan.Enabled = False
                                    btnValidasi.Enabled = True
                                Else
                                    sHelper.SetSession("DepositID", intResult)
                                    MessageBox.Show("Insert History Fail")
                                    DisableControl()
                                End If
                            End If
                        Else
                            objKuitansi.ReceiptNumber = txtNomorKuitansi.Text
                            objKuitansi.SignedBy = txtSign.Text
                            objKuitansi.Jabatan = txtJabatan.Text
                            intResult = New DepositAKuitansiPencairanFacade(User).Update(objKuitansi)
                            sHelper.SetSession("DepositID", objKuitansi.ID)
                            If intResult = -1 Then
                                MessageBox.Show(SR.UpdateFail)
                                btnValidasi.Enabled = False
                            Else
                                MessageBox.Show(SR.UpdateSucces)
                                DisableControl()
                                btnSimpan.Enabled = False
                                btnValidasi.Enabled = True
                            End If
                        End If
                    Else
                        MessageBox.Show(SR.SaveFail)
                    End If


                End If
            Else
                objDepositAKuitansi = New DepositAKuitansiPencairan
                objDepositAKuitansi = New DepositAKuitansiPencairanFacade(User).Retrieve(CInt(Request.QueryString("id")))
                If objDepositAKuitansi.ID > 0 Then
                    objDepositAKuitansi.ReceiptNumber = txtNomorKuitansi.Text
                    objDepositAKuitansi.SignedBy = txtSign.Text
                    objDepositAKuitansi.Jabatan = txtJabatan.Text
                    intResult = New DepositAKuitansiPencairanFacade(User).Update(objDepositAKuitansi)
                    sHelper.SetSession("DepositID", objDepositAKuitansi.ID)
                    If intResult = -1 Then
                        MessageBox.Show(SR.UpdateFail)
                        btnValidasi.Enabled = False
                    Else
                        MessageBox.Show(SR.UpdateSucces)
                        DisableControl()
                        btnSimpan.Enabled = False
                        btnValidasi.Enabled = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Session("IsBindDataGrid") = True

        Dim NoPengajuan As String = ViewState("NoPengajuan")
        Dim NoKuitansi As String = ViewState("NoKuitansi")
        Dim PeriodeFromKuitansi As String = ViewState("PeriodeFromKuitansi")
        Dim PeriodeToKuitansi As String = ViewState("PeriodeToKuitansi")
        Dim PeriodeFromPengajuan As String = ViewState("PeriodeFromPengajuan")
        Dim PeriodeToPengajuan As String = ViewState("PeriodeToPengajuan")

        Server.Transfer("../FinishUnit/FrmDaftarKuitansiPencairanDepositA.aspx?NoKuitansi=" & NoKuitansi & "&PeriodeFromKuitansi=" & PeriodeFromKuitansi & "&PeriodeToKuitansi=" & PeriodeToKuitansi & "&NoPengajuan=" & NoPengajuan & "&PeriodeFromPengajuan=" & PeriodeFromPengajuan & "&PeriodeToPengajuan=" & PeriodeToPengajuan)
    End Sub

    Private Sub btnCetak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCetak.Click
        'Dim objKuitansiDepositA As DepositAKuitansiPencairan = New FinishUnit.DepositAKuitansiPencairanFacade(User).Retrieve(ddlNoRegPengajuan.SelectedItem.Text)
        'If objKuitansiDepositA.ID > 0 Then
        '    Dim oldStatus As Integer = objKuitansiDepositA.Status
        '    objKuitansiDepositA.Status = StatusKuitansiKTB.Printed
        '    Dim NewStatus As Integer = objKuitansiDepositA.Status
        '    Dim nResult As Integer = New FinishUnit.DepositAKuitansiPencairanFacade(User).Update(objKuitansiDepositA)
        '    If nResult > -1 Then
        '        'insert history
        '        nResult = InsertHistory(objKuitansiDepositA.NoSurat, oldStatus, NewStatus, DocTypeKuitansi)
        '        If nResult > -1 Then
        '            MessageBox.Show(SR.SaveSuccess)
        '        Else
        '            MessageBox.Show("Insert History Fail")
        '        End If
        '    End If
        'End If

        Dim objKuitansiDepositA As DepositAKuitansiPencairan = New FinishUnit.DepositAKuitansiPencairanFacade(User).Retrieve(ddlNoRegPengajuan.SelectedItem.Text)
        If objKuitansiDepositA.ID > 0 Then
            Server.Transfer("../FinishUnit/FrmKwitansiPencairan.aspx?id=" & objKuitansiDepositA.ID)
        End If
    End Sub

#End Region

#Region "Custom Method"
    Private Sub BindDataToForm(ByVal ID As Integer)
        objDepositAKuitansi = New DepositAKuitansiPencairanFacade(User).Retrieve(ID)
        If objDepositAKuitansi.ID > 0 Then
            sHelper.SetSession("OBJDEALER", objDepositAKuitansi.Dealer)
            BindTipePengajuan()
            'BindNomorRegistrasi()
            'lblKodeDealer.Text = objDepositAKuitansi.Dealer.DealerCode
            txtKodeDealer.Text = objDepositAKuitansi.Dealer.DealerCode
            lblNamaDealer.Text = objDepositAKuitansi.Dealer.DealerName
            lblProduk.Text = objDepositAKuitansi.ProductCategory.Code
            ddlTipePengajuan.SelectedIndex = objDepositAKuitansi.Type
            ddlTipePengajuan_SelectedIndexChanged(Nothing, Nothing)
            'Dim objDepositAPencairan As New DepositAPencairanH
            'objDepositAPencairan = New DepositAPencairanHFacade(User).Retrieve(objDepositAKuitansi.NoSurat)
            'If objDepositAPencairan.ID > 0 Then
            '    If objDepositAPencairan.DealerBankAccount.ID > 0 Then
            '        lblNoRekening.Text = objDepositAPencairan.DealerBankAccount.BankAccount
            '    End If
            'End If

            lblTglPengajuan.Text = CDate(objDepositAKuitansi.CreatedTime).ToString("dd/MM/yyyy")
            txtNomorKuitansi.Text = objDepositAKuitansi.ReceiptNumber
            lblTanggalKwitansi.Text = objDepositAKuitansi.ReceiptDate

            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            If companyCode = "MMC" Then
                lblTelahTerimaDari.Text = "PT Mitsubishi Motors Krama Yudha Sales Indonesia"
            Else
                lblTelahTerimaDari.Text = "PT Krama Yudha Tiga Berlian Motor"
            End If

            lblUangSejumlah.Text = FormatNumber(objDepositAKuitansi.TotalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblUangPembayaran.Text = objDepositAKuitansi.Description
            txtSign.Text = objDepositAKuitansi.SignedBy
            txtJabatan.Text = objDepositAKuitansi.Jabatan

            If objDepositAKuitansi.Status = EnumDepositA.StatusPencairan.Baru Then
                txtNomorKuitansi.ReadOnly = False
                txtSign.ReadOnly = False
                txtJabatan.ReadOnly = False
            Else
                txtNomorKuitansi.ReadOnly = True
                txtSign.ReadOnly = True
                txtJabatan.ReadOnly = True
            End If

            objUserInfo = sHelper.GetSession("LOGINUSERINFO")
            If objUserInfo.Dealer.Title = 1 Then
                btnCetak.Enabled = False
                btnNew.Enabled = False
                btnSimpan.Enabled = False
                btnValidasi.Enabled = False
            Else
                'If objDepositAKuitansi.Status = 1 Or objDepositAKuitansi.Status = 10 Or objDepositAKuitansi.Status = 11 Or objDepositAKuitansi.Status = 12 Then
                If objDepositAKuitansi.Status = EnumDepositA.StatusKuitansiKTB.Validasi Or objDepositAKuitansi.Status = EnumDepositA.StatusKuitansiKTB.Konfirmasi Or objDepositAKuitansi.Status = EnumDepositA.StatusKuitansiKTB.Printed Or objDepositAKuitansi.Status = EnumDepositA.StatusKuitansiKTB.Selesai Or objDepositAKuitansi.Status = EnumDepositA.StatusKuitansiKTB.Cair Then
                    btnCetak.Visible = True
                Else
                    btnCetak.Visible = False
                End If
                If objDepositAKuitansi.Status <> EnumDepositA.StatusPencairan.Baru Then
                    'Or objDepositAKuitansi.Status = StatusPencairan.Konfirmasi _
                    'Or objDepositAKuitansi.Status = StatusPencairanKTB.Konfirmasi _
                    'Or objDepositAKuitansi.Status = StatusPencairanKTB.Selesai Then
                    btnNew.Enabled = False
                    btnSimpan.Enabled = False
                Else
                    btnNew.Enabled = True
                    btnSimpan.Enabled = True
                End If
                btnValidasi.Enabled = False
            End If
        End If
    End Sub

    Private Sub BindToForm()
        'objUserInfo = sHelper.GetSession("LOGINUSERINFO")
        'If objUserInfo.ID > 0 Then
        '    lblKodeDealer.Text = objUserInfo.Dealer.DealerCode
        '    lblNamaDealer.Text = objUserInfo.Dealer.DealerName
        'End If
        imgDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        'txtKodeDealer.ReadOnly = True
        txtKodeDealer.Attributes.Add("readonly", "readonly")


        If Not CType(sHelper.GetSession("OBJDEALER"), Dealer) Is Nothing Then
            Dim oDealer As Dealer = CType(sHelper.GetSession("OBJDEALER"), Dealer)
            If oDealer.ID > 0 Then
                txtKodeDealer.Text = oDealer.DealerCode
                lblNamaDealer.Text = oDealer.DealerName
            End If
        End If
        BindTipePengajuan()
        lblTglPengajuan.Text = String.Empty
        lblNoRefSuratPengajuan.Text = String.Empty
        txtNomorKuitansi.Text = String.Empty
        lblNoRekening.Text = String.Empty
        lblUangSejumlah.Text = String.Empty
        lblUangPembayaran.Text = String.Empty
        txtSign.Text = String.Empty
        txtJabatan.Text = String.Empty
        lblTanggalKwitansi.Text = DateTime.Now.Date.ToString("dd/MM/yyyy")

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        If companyCode = "MMC" Then
            lblTelahTerimaDari.Text = "PT Mitsubishi Motors Krama Yudha Sales Indonesia"
        Else
            lblTelahTerimaDari.Text = "PT Krama Yudha Tiga Berlian Motor"
        End If

        sHelper.SetSession("DepositID", 0)
        btnSimpan.Enabled = True
        btnValidasi.Enabled = False
    End Sub

    Private Sub ClearForm()
        'objUserInfo = sHelper.GetSession("LOGINUSERINFO")
        'If objUserInfo.ID > 0 Then
        'lblKodeDealer.Text = objUserInfo.Dealer.DealerCode
        'lblNamaDealer.Text = objUserInfo.Dealer.DealerName
        'End If
        If sHelper.GetSession("OBJDEALER") Is Nothing Then
            txtKodeDealer.Text = String.Empty
            lblNamaDealer.Text = String.Empty
        Else
            Dim objDealer As Dealer = CType(sHelper.GetSession("OBJDEALER"), Dealer)
            txtKodeDealer.Text = objDealer.DealerCode
            lblNamaDealer.Text = objDealer.DealerName
        End If
        lblTglPengajuan.Text = String.Empty
        txtNomorKuitansi.Text = String.Empty
        lblNoRefSuratPengajuan.Text = String.Empty
        lblNoRekening.Text = String.Empty
        lblUangSejumlah.Text = String.Empty
        lblUangPembayaran.Text = String.Empty
        txtSign.Text = String.Empty
        lblTanggalKwitansi.Text = DateTime.Now.Date.ToString("dd/MM/yyyy")

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        If companyCode = "MMC" Then
            lblTelahTerimaDari.Text = "PT Mitsubishi Motors Krama Yudha Sales Indonesia"
        Else
            lblTelahTerimaDari.Text = "PT Krama Yudha Tiga Berlian Motor"
        End If
    End Sub

    Private Sub BindTipePengajuan()
        ddlTipePengajuan.DataSource = [Enum].GetNames(GetType(EnumDepositA.TipePengajuan))
        ddlTipePengajuan.DataBind()
        ddlTipePengajuan.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
        ddlTipePengajuan.SelectedIndex = 0
    End Sub

    Private Sub BindNomorRegistrasi()
        ddlNoRegPengajuan.Items.Clear()
        ddlNoRegPengajuan.Items.Add("Silahkan Pilih")
        objUserInfo = sHelper.GetSession("LOGINUSERINFO")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Type", MatchType.Exact, ddlTipePengajuan.SelectedIndex))
        'criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Status", MatchType.Exact, CType(StatusPencairanKTB.Setuju, Integer)))
        criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Dealer.ID", MatchType.Exact, objUserInfo.Dealer.ID))
        arlDepositAPencairanFilter = New DepositAPencairanHFacade(User).Retrieve(criterias)
        If arlDepositAPencairanFilter.Count > 0 Then
            For Each item As DepositAPencairanH In arlDepositAPencairanFilter
                ddlNoRegPengajuan.Items.Add(item.NoReg)
            Next
        End If
    End Sub

    Private Function InsertHistory(ByVal NoSurat As String, ByVal OldStatus As Integer, ByVal NewStatus As Integer, ByVal DocType As Integer)
        Dim objHistoryDepositAKuitansiPencairan As DepositAStatusHistory = New DepositAStatusHistory
        objHistoryDepositAKuitansiPencairan.DocNumber = NoSurat
        objHistoryDepositAKuitansiPencairan.OldStatus = OldStatus
        objHistoryDepositAKuitansiPencairan.NewStatus = NewStatus
        objHistoryDepositAKuitansiPencairan.DocType = DocType
        Dim nResult As Integer = -1
        nResult = New FinishUnit.DepositAStatusHistoryFacade(User).Insert(objHistoryDepositAKuitansiPencairan)
        Return nResult
    End Function

    Private Sub DisableControl()
        ddlTipePengajuan.Enabled = False
        txtNomorKuitansi.Enabled = False
    End Sub

    Private Sub EnableControl()
        ddlTipePengajuan.Enabled = True
        txtNomorKuitansi.Enabled = True
        lblProduk.Text = ""
    End Sub

    Private Function GetDetailDescriptions(ByVal arlDepositAPencairanD As ArrayList) As String
        Dim strResult As String = String.Empty
        Dim objDepositAPencairanD As DepositAPencairanD
        For Each objDepositAPencairanD In arlDepositAPencairanD
            strResult = strResult + objDepositAPencairanD.Description & ";"
        Next
        strResult = Left(strResult, strResult.Length - 1)
        Return strResult
    End Function
#End Region



    Private Sub imgDealer_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDealer.Click
        If txtKodeDealer.Text.Trim <> String.Empty Then
            Dim objDealer As Dealer = New General.DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
            If objDealer.ID > 0 Then
                sHelper.SetSession("OBJDEALER", objDealer)
                BindToForm()
            End If
        End If

    End Sub

    Private Sub btnImgDealer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImgDealer.Click
        Me.imgDealer_Click(Nothing, Nothing)
    End Sub
End Class
