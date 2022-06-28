Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class FrmDepB_KuitansiPencairan
    Inherits System.Web.UI.Page

#Region "Private Variables"
    Private arlDepositBPencairan As ArrayList = New ArrayList
    Private arlDepositBPencairanFilter As ArrayList = New ArrayList
    Private objDepositBPencairanHeader As New DepositBPencairanHeader
    Private objDepositBKuitansi As New DepositBReceipt
    Private objUserInfo As UserInfo
    Dim sHelper As New SessionHelper
    Dim IsDealer As Boolean = True
    Const DocTypePengajuan = 1
    Const DocTypeKuitansi = 2

#End Region

#Region "Event Handler"

    Private Sub InitiateAuthorization()
        objUserInfo = sHelper.GetSession("LOGINUSERINFO")

        Dim _input_kuitansi_pencairan_Privilege As Boolean = False
        Dim _lihat_daftar_kuitansi_pencairan_Privilege As Boolean = False

        _input_kuitansi_pencairan_Privilege = SecurityProvider.Authorize(Context.User, SR.input_kuitansi_pencairan_Privilege)
        _lihat_daftar_kuitansi_pencairan_Privilege = SecurityProvider.Authorize(Context.User, SR.lihat_daftar_kuitansi_pencairan_Privilege)


     

        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not _input_kuitansi_pencairan_Privilege Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Deposit A - Buat Kuitansi Pencairan")

            End If
        Else
            If Not (_lihat_daftar_kuitansi_pencairan_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Deposit A - Lihat Kuitansi Pencairan")
            End If
            btnSimpan.Visible = False
            btnCetak.Visible = False
            btnNew.Visible = False

        End If


    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                ViewState("PeriodeFromPengajuan") = Request.QueryString("PeriodeFromPengajuan")
                ViewState("PeriodeToPengajuan") = Request.QueryString("PeriodeToPengajuan")
            Else
                BindToForm()
                btnKembali.Visible = False
            End If
        End If
    End Sub

    Private Sub ddlTipePengajuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipePengajuan.SelectedIndexChanged

        ddlNoRegPengajuan.Items.Clear()

        If Not Request.QueryString("id") Is Nothing Then
            If Not (objDepositBKuitansi) Is Nothing Then
                ddlNoRegPengajuan.Items.Add(objDepositBKuitansi.DepositBPencairanHeader.NoReg)
            End If
        Else
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositBPencairanHeader), "TipePengajuan", MatchType.Exact, CInt(ddlTipePengajuan.SelectedValue)))
            criterias.opAnd(New Criteria(GetType(DepositBPencairanHeader), "Status", MatchType.Exact, CType(DepositBEnum.StatusPencairan.Konfirmasi, Integer)))
            objUserInfo = sHelper.GetSession("LOGINUSERINFO")
            If Not IsNothing(objUserInfo) Then
                If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    criterias.opAnd(New Criteria(GetType(DepositBPencairanHeader), "Dealer.ID", MatchType.Exact, objUserInfo.Dealer.ID))
                End If
            End If
            arlDepositBPencairanFilter = New DepositBPencairanHeaderFacade(User).Retrieve(criterias)
            If arlDepositBPencairanFilter.Count > 0 Then
                ddlNoRegPengajuan.Items.Add("Silahkan Pilih")
                For Each item As DepositBPencairanHeader In arlDepositBPencairanFilter
                    If Not (item.DepositBReceipts.Count > 0) Then
                        ddlNoRegPengajuan.Items.Add(item.NoReg)
                    End If
                Next
            End If
        End If
        If ddlNoRegPengajuan.Items.Count = 0 Then
            MessageBox.Show("Tidak ada data untuk dibuatkan kuitansi")
            btnSimpan.Enabled = False
        Else
            btnSimpan.Enabled = True
        End If

    End Sub

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        If ddlNoRegPengajuan.Items.Count > 0 Then
            Dim objKuitansiDepositB As DepositBReceipt = New DepositBReceiptFacade(User).RetrieveByNoRegPencairanHeader(ddlNoRegPengajuan.SelectedItem.Text)
            If objKuitansiDepositB.ID > 0 Then
                Server.Transfer("../Service/FrmDepB_KuitansiPrint.aspx?id=" & objKuitansiDepositB.ID)
            End If
        Else
            MessageBox.Show("Tidak ada No. Reg Pengajuan yang akan di cetak.")
        End If

    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        If ddlTipePengajuan.SelectedIndex = 0 And Request.QueryString("id") Is Nothing Then
            MessageBox.Show("Pilih dulu tipe pengajuan pencairan")
            Exit Sub
        End If

        If ddlNoRegPengajuan.Items.Count = 0 Then
            MessageBox.Show("Tidak ada data untuk dibuatkan kuitansi")
            Exit Sub
        Else
            If ddlNoRegPengajuan.SelectedItem.Text = "" Then
                MessageBox.Show("Pilih dulu nomor registrasi surat pengajuan")
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
        'Cek Nomor Kuitansi
        'Dim o As New DepositBReceipt
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(DepositBReceipt), "Type", MatchType.Exact, ddlTipePengajuan.SelectedIndex))
        'criterias.opAnd(New Criteria(GetType(DepositBReceipt), "Dealer.ID", MatchType.Exact, objUserInfo.Dealer.ID))
        'arlDepositBPencairanFilter = New DepositBReceiptFacade(User).Retrieve(criterias)

        'Dim objKuitansi As DepositBReceipt = New DepositBReceiptFacade(User).RetrieveByPencairanHeader(objPencairan.ID)


        Dim intResult = CInt(sHelper.GetSession("DepositID"))
        If intResult > 0 Then
            objDepositBKuitansi = New DepositBReceiptFacade(User).Retrieve(intResult)
            If objDepositBKuitansi.ID = 0 Then
                MessageBox.Show(SR.UpdateFail)
                Exit Sub
            End If
        End If

        Dim objDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        If objDealer.ID > 0 Then

            If Request.QueryString("id") Is Nothing Then
                Dim objPencairan As New DepositBPencairanHeader
                Dim objKuitansi As New DepositBReceipt
                objPencairan = New DepositBPencairanHeaderFacade(User).Retrieve(ddlNoRegPengajuan.SelectedItem.Text)
                If objPencairan.ID > 0 Then
                    'Dim iResult As Integer
                    'objPencairan.Status = DepositBEnum.StatusPencairan.Selesai
                    'iResult = New DepositBPencairanHeaderFacade(User).Update(objPencairan)
                    'If iResult > -1 Then
                    objKuitansi = New DepositBReceiptFacade(User).RetrieveByPencairanHeader(objPencairan.ID)
                    If Not (objKuitansi.ID > 0) Then
                        objKuitansi = New DepositBReceipt
                        objKuitansi.DepositBPencairanHeader = objPencairan
                        objKuitansi.NomorKuitansi = txtNomorKuitansi.Text
                        objKuitansi.Keterangan = lblUangPembayaran.Text
                        objKuitansi.TanggalKuitansi = New Date(lblTanggalKwitansi.Text.Trim.Substring(6, 4), lblTanggalKwitansi.Text.Trim.Substring(3, 2), lblTanggalKwitansi.Text.Trim.Substring(0, 2))
                        objKuitansi.NamaPejabat = txtSign.Text
                        objKuitansi.Jabatan = txtJabatan.Text
                        intResult = New DepositBReceiptFacade(User).Insert(objKuitansi)
                        If intResult = -1 Then
                            MessageBox.Show(SR.SaveFail)
                        Else
                            'Dim nResult As Integer = InsertHistory(objKuitansi.NoSurat, objKuitansi.Status, DepositBEnum.StatusKuitansiDealer.Baru, DocTypeKuitansi)
                            'If nResult > -1 Then
                            sHelper.SetSession("DepositID", intResult)
                            MessageBox.Show(SR.SaveSuccess)
                            DisableControl()
                            btnSimpan.Enabled = False
                            btnCetak.Visible = True
                        End If
                    Else
                        objKuitansi.NomorKuitansi = txtNomorKuitansi.Text
                        objKuitansi.Keterangan = lblUangPembayaran.Text
                        objKuitansi.TanggalKuitansi = New Date(lblTanggalKwitansi.Text.Trim.Substring(6, 4), lblTanggalKwitansi.Text.Trim.Substring(3, 2), lblTanggalKwitansi.Text.Trim.Substring(0, 2))
                        objKuitansi.NamaPejabat = txtSign.Text
                        objKuitansi.Jabatan = txtJabatan.Text
                        intResult = New DepositBReceiptFacade(User).Update(objKuitansi)
                        sHelper.SetSession("DepositID", objKuitansi.ID)
                        If intResult = -1 Then
                            MessageBox.Show(SR.UpdateFail)
                        Else
                            MessageBox.Show(SR.UpdateSucces)
                            DisableControl()
                            btnSimpan.Enabled = False
                            btnCetak.Visible = True
                        End If
                    End If
                    'Else
                    '    MessageBox.Show(SR.SaveFail)
                    'End If
                End If
            Else
                objDepositBKuitansi = New DepositBReceipt
                objDepositBKuitansi = New DepositBReceiptFacade(User).Retrieve(CInt(Request.QueryString("id")))
                If objDepositBKuitansi.ID > 0 Then
                    objDepositBKuitansi.NomorKuitansi = txtNomorKuitansi.Text
                    objDepositBKuitansi.Keterangan = lblUangPembayaran.Text
                    objDepositBKuitansi.TanggalKuitansi = New Date(lblTanggalKwitansi.Text.Trim.Substring(6, 4), lblTanggalKwitansi.Text.Trim.Substring(3, 2), lblTanggalKwitansi.Text.Trim.Substring(0, 2))
                    objDepositBKuitansi.NamaPejabat = txtSign.Text
                    objDepositBKuitansi.Jabatan = txtJabatan.Text
                    intResult = New DepositBReceiptFacade(User).Update(objDepositBKuitansi)
                    sHelper.SetSession("DepositID", objDepositBKuitansi.ID)
                    If intResult = -1 Then
                        MessageBox.Show(SR.UpdateFail)
                    Else
                        MessageBox.Show(SR.UpdateSucces)
                        DisableControl()
                        btnSimpan.Enabled = False
                        btnCetak.Visible = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Session("IsBindDataGrid") = True

        ViewState("NoKuitansi") = Request.QueryString("NoKuitansi")
        ViewState("PeriodeFromKuitansi") = Request.QueryString("PeriodeFromKuitansi")
        ViewState("PeriodeToKuitansi") = Request.QueryString("PeriodeToKuitansi")
        ViewState("PeriodeFromPengajuan") = Request.QueryString("PeriodeFromPengajuan")
        ViewState("PeriodeToPengajuan") = Request.QueryString("PeriodeToPengajuan")

        Dim NoKuitansi As String = ViewState("NoKuitansi")
        Dim PeriodeFromKuitansi As String = ViewState("PeriodeFromKuitansi")
        Dim PeriodeToKuitansi As String = ViewState("PeriodeToKuitansi")
        Dim PeriodeFromPengajuan As String = ViewState("PeriodeFromPengajuan")
        Dim PeriodeToPengajuan As String = ViewState("PeriodeToPengajuan")

        Server.Transfer("../Service/FrmDepB_KuitansiPencairanList.aspx?NoKuitansi=" & NoKuitansi & "&PeriodeFromKuitansi=" & PeriodeFromKuitansi & "&PeriodeToKuitansi=" & PeriodeToKuitansi & "&PeriodeFromPengajuan=" & PeriodeFromPengajuan & "&PeriodeToPengajuan=" & PeriodeToPengajuan)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        EnableControl()
        BindToForm()
        ddlNoRegPengajuan.Items.Clear()
        ddlNoRegPengajuan.Items.Add("Silahkan Pilih")
    End Sub
#End Region

#Region "Custom Method"
   

    Private Sub BindData(ByVal objKuitansi As DepositBReceipt)
        If objKuitansi.ID > 0 Then
            BindTipePengajuan()
            ddlTipePengajuan.SelectedValue = CInt(objKuitansi.DepositBPencairanHeader.TipePengajuan)
            ddlTipePengajuan_SelectedIndexChanged(Nothing, Nothing)

            txtKodeDealer.Text = objKuitansi.DepositBPencairanHeader.Dealer.DealerCode
            lblNamaDealer.Text = objKuitansi.DepositBPencairanHeader.Dealer.DealerName
            lblProduk.Text = objKuitansi.DepositBPencairanHeader.ProductCategory.Code
            lblTglPengajuan.Text = CDate(objKuitansi.CreatedTime).ToString("dd/MM/yyyy")

            lblNoRefSuratPengajuan.Text = objKuitansi.DepositBPencairanHeader.NoReferensi
            lblNoRekening.Text = objKuitansi.DepositBPencairanHeader.DealerBankAccount.BankAccount

            txtNomorKuitansi.Text = objKuitansi.NomorKuitansi
            lblTanggalKwitansi.Text = objKuitansi.TanggalKuitansi
            lblTelahTerimaDari.Text = "PT Mitsubishi Motors Krama Yudha Sales Indonesia"
            lblUangSejumlah.Text = FormatNumber(objKuitansi.DepositBPencairanHeader.DealerAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblUangPembayaran.Text = objKuitansi.Keterangan
            txtSign.Text = objKuitansi.NamaPejabat
            txtJabatan.Text = objKuitansi.Jabatan

        End If
    End Sub


    Private Sub BindDataToForm(ByVal ID As Integer)
        objDepositBKuitansi = New DepositBReceiptFacade(User).Retrieve(ID)
        If objDepositBKuitansi.ID > 0 Then

            BindData(objDepositBKuitansi)

            objUserInfo = sHelper.GetSession("LOGINUSERINFO")
            If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                btnCetak.Visible = True
                btnSimpan.Visible = True
                btnNew.Visible = False
            Else
                btnCetak.Visible = False
                btnNew.Visible = False
                btnSimpan.Visible = False
            End If
            txtKodeDealer.Enabled = False
            ddlNoRegPengajuan.Enabled = False
            ddlTipePengajuan.Enabled = False
        End If
    End Sub

    Private Sub BindToForm()

        imgDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        txtKodeDealer.Attributes.Add("readonly", "readonly")

        If Not (objUserInfo) Is Nothing Then
            If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
                lblNamaDealer.Text = objUserInfo.Dealer.DealerName
            Else
                txtKodeDealer.Text = String.Empty
                lblNamaDealer.Text = String.Empty
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
        lblTelahTerimaDari.Text = "PT Mitsubishi Motors Krama Yudha Sales Indonesia"
        sHelper.SetSession("DepositID", 0)
        btnSimpan.Enabled = True
    End Sub

    Private Sub ClearForm()

        'If sHelper.GetSession("DEALER") Is Nothing Then
        '    txtKodeDealer.Text = String.Empty
        '    lblNamaDealer.Text = String.Empty
        'Else
        '    Dim objDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        '    txtKodeDealer.Text = objDealer.DealerCode
        '    lblNamaDealer.Text = objDealer.DealerName
        'End If
        lblTglPengajuan.Text = String.Empty
        txtNomorKuitansi.Text = String.Empty
        lblNoRefSuratPengajuan.Text = String.Empty
        lblNoRekening.Text = String.Empty
        lblUangSejumlah.Text = String.Empty
        lblUangPembayaran.Text = String.Empty
        txtSign.Text = String.Empty
        lblTanggalKwitansi.Text = DateTime.Now.Date.ToString("dd/MM/yyyy")
        lblTelahTerimaDari.Text = "PT Mitsubishi Motors Krama Yudha Sales Indonesia"
    End Sub

    Private Sub BindTipePengajuan()
        Dim items As New ArrayList
        Dim _arrStatus As ArrayList = New DepositBEnum().RetrieveTipePengajuan(True)
        For Each item As EnumProperty In _arrStatus
            items.Add(item)
        Next

        ddlTipePengajuan.DataSource = items
        ddlTipePengajuan.DataTextField = "NameType"
        ddlTipePengajuan.DataValueField = "ValType"
        ddlTipePengajuan.DataBind()
    End Sub

    Private Sub BindNomorRegistrasi()
        ddlNoRegPengajuan.Items.Clear()
        ddlNoRegPengajuan.Items.Add("Silahkan Pilih")
        objUserInfo = sHelper.GetSession("LOGINUSERINFO")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DepositBPencairanHeader), "Type", MatchType.Exact, ddlTipePengajuan.SelectedIndex))
        criterias.opAnd(New Criteria(GetType(DepositBPencairanHeader), "Dealer.ID", MatchType.Exact, objUserInfo.Dealer.ID))
        arlDepositBPencairanFilter = New DepositBPencairanHeaderFacade(User).Retrieve(criterias)
        If arlDepositBPencairanFilter.Count > 0 Then
            For Each item As DepositBPencairanHeader In arlDepositBPencairanFilter
                ddlNoRegPengajuan.Items.Add(item.NoReg)
            Next
        End If
    End Sub

    Private Function InsertHistory(ByVal NoSurat As String, ByVal OldStatus As Integer, ByVal NewStatus As Integer, ByVal DocType As Integer)
        'Dim objHistoryDepositBReceipt As DepositBStatusHistory = New DepositBStatusHistory
        'objHistoryDepositBReceipt.DocNumber = NoSurat
        'objHistoryDepositBReceipt.OldStatus = OldStatus
        'objHistoryDepositBReceipt.NewStatus = NewStatus
        'objHistoryDepositBReceipt.DocType = DocType
        'Dim nResult As Integer = -1
        'nResult = New FinishUnit.DepositBStatusHistoryFacade(User).Insert(objHistoryDepositBReceipt)
        'Return nResult
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

    Private Function GetDetailDescriptions(ByVal arlDepositBPencairanDetail As ArrayList) As String
        Dim strResult As String = String.Empty
        Dim objDepositBPencairanDetail As DepositBPencairanDetail
        For Each objDepositBPencairanDetail In arlDepositBPencairanDetail
            strResult = strResult + objDepositBPencairanDetail.Description & ";"
        Next
        strResult = Left(strResult, strResult.Length - 1)
        Return strResult
    End Function
#End Region

    Private Sub ddlNoRegPengajuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNoRegPengajuan.SelectedIndexChanged
        ClearForm()

        Dim objDeposit As New DepositBPencairanHeader
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DepositBPencairanHeader), "TipePengajuan", MatchType.Exact, ddlTipePengajuan.SelectedValue))
        criterias.opAnd(New Criteria(GetType(DepositBPencairanHeader), "NoReg", MatchType.Exact, ddlNoRegPengajuan.SelectedValue.ToString))
        arlDepositBPencairanFilter = New DepositBPencairanHeaderFacade(User).Retrieve(criterias)

        If arlDepositBPencairanFilter.Count > 0 Then
            objDeposit = arlDepositBPencairanFilter(0)
            If Not IsNothing(objDeposit) Then
                If objDeposit.DepositBReceipts.Count > 0 Then
                    Dim objKuitansi As DepositBReceipt = objDeposit.DepositBReceipts(0)
                    BindData(objKuitansi)
                Else
                    lblNoRefSuratPengajuan.Text = objDeposit.NoReferensi
                    lblNoRekening.Text = objDeposit.DealerBankAccount.BankAccount
                    Dim selectedTipe As DepositBEnum.TipePengajuan = CType([Enum].Parse(GetType(DepositBEnum.TipePengajuan), ddlTipePengajuan.SelectedValue), DepositBEnum.TipePengajuan)
                    Select Case selectedTipe
                        Case DepositBEnum.TipePengajuan.Offset_SP
                            lblUangSejumlah.Text = FormatNumber(objDeposit.DealerAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                        Case DepositBEnum.TipePengajuan.Interest
                            lblUangSejumlah.Text = FormatNumber(objDeposit.DepositBInterestHeader.NettoAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                        Case Else
                            lblUangSejumlah.Text = FormatNumber(objDeposit.ApprovalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    End Select
                    'If selectedTipe = DepositBEnum.TipePengajuan.Offset_SP Then
                    '    lblUangSejumlah.Text = FormatNumber(objDeposit.DealerAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    'Else
                    '    lblUangSejumlah.Text = FormatNumber(objDeposit.ApprovalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    'End If
                    lblTglPengajuan.Text = Format(objDeposit.CreatedTime, "dd/MM/yyyy")
                    lblProduk.Text = objDeposit.ProductCategory.Code
                    If Not objDeposit.DealerBankAccount Is Nothing Then
                        If objDeposit.DealerBankAccount.ID > 0 Then
                            lblNoRekening.Text = objDeposit.DealerBankAccount.BankAccount
                        Else
                            lblNoRekening.Text = String.Empty
                        End If
                    End If

                    lblUangPembayaran.Text = GetDetailDescriptions(objDeposit.DepositBPencairanDetails)
                End If
            End If
        End If
    End Sub

    Private Sub imgDealer_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDealer.Click
        If txtKodeDealer.Text.Trim <> String.Empty Then
            Dim objDealer As Dealer = New General.DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
            If objDealer.ID > 0 Then
                sHelper.SetSession("DEALER", objDealer)
                BindToForm()
            End If
        End If

    End Sub

    Private Sub btnImgDealer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImgDealer.Click
        Me.imgDealer_Click(Nothing, Nothing)
    End Sub
End Class