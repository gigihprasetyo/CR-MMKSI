Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports System.IO
Imports System.Collections.Generic
Imports Spire.Doc
Imports KTB.DNet.Security

Public Class FrmUploadFaktureEvidance
    Inherits System.Web.UI.Page
    Dim _sessHelper As New SessionHelper
    Dim filePath As String = String.Empty
    Dim filePathName As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + KTB.DNet.Lib.WebConfig.GetValue("MonthlyDocEvidance") & ""
        
        txtNomorFaktur.Text = "10"
        txtNomorFaktur3.Text = "21"
        HDCriteria.Value = CType(Request.QueryString("Criterias"), String)
        SCDealer.Value = CType(Request.QueryString("SCDealer"), String)
        JDoc.Value = CType(Request.QueryString("JDoc"), String)
        NoFaktur.Value = CType(Request.QueryString("NoFaktur"), String)
        BillingNo.Value = CType(Request.QueryString("BillingNo"), String)
        AccountingNo.Value = CType(Request.QueryString("AccountingNo"), String)
        Month.Value = CType(Request.QueryString("Month"), String)
        Year.Value = CType(Request.QueryString("Year"), String)
        MonthTo.Value = CType(Request.QueryString("MonthTo"), String)
        YearTo.Value = CType(Request.QueryString("YearTo"), String)
        PCategory.Value = CType(Request.QueryString("PCategory"), String)
        Download.Value = CType(Request.QueryString("Download"), String)

        If Not Page.IsPostBack Then
            Dim appConf As AppConfig = New AppConfigFacade(User).Retrieve("EnumMonthlyDocumentToFakturDTOP")
            JenisDokumen()
            ICTglUpload.Value = Date.Now.Date
            'set tgl 10,15 atau 25
            Dim dateH11 As Date = ICTglUpload.Value.AddDays(CInt(appConf.Value))
            If dateH11.Day > 10 AndAlso dateH11.Day < 15 Then
                dateH11 = New Date(dateH11.Year, dateH11.Month, 15)
            ElseIf dateH11.Day > 15 AndAlso dateH11.Day < 25 Then
                dateH11 = New Date(dateH11.Year, dateH11.Month, 25)
            Else
                dateH11 = New Date(dateH11.Year, dateH11.Month, 10)
            End If
            ICTanggalTranferPlanning.Value = New Date(dateH11.Year, dateH11.Month, dateH11.Day)

            BindBankAccount(CType(Request.QueryString("DealerCode"), String))

            Dim fcd As New MonthlyDocumentFacade(User)
            Dim dataMD As MonthlyDocument = fcd.Retrieve(CType(Request.QueryString("IDmonth"), Integer))
            ddlJenisDokumen.SelectedValue = dataMD.Kind

            If CType(Request.QueryString("mode"), String) = "view" Then
                btnSave.Enabled = True
                txtDeskripsiPembayaran.Enabled = False
                ICTanggalTranferPlanning.Enabled = False
                dFaktur.Visible = False
                dFaktur2.Visible = True
                lblFaktur.Visible = True

                Dim objMonthEvi As New MonthlyDocumentToFakturEvidance
                Dim crit As New CriteriaComposite(New Criteria(GetType(MonthlyDocumentToFakturEvidance), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(MonthlyDocumentToFakturEvidance), "MonthlyDocumentID", MatchType.Exact, CType(Request.QueryString("IDmonth"), Integer)))

                Dim arra As ArrayList = (New MonthlyDocumentToFakturEvidanceFacade(User).Retrieve(crit))
                objMonthEvi = CType(arra.Item(0), MonthlyDocumentToFakturEvidance)
                txtDeskripsiPembayaran.Text = objMonthEvi.PaymentDescription
                lblFaktur.Text = objMonthEvi.FakturNumber
                lblNomorUpload.Text = CType(objMonthEvi.ID, String)
                lblPathFile.Text = objMonthEvi.FileNamePath
                lblPathImage.Text = objMonthEvi.EvidancePath
                ICTanggalTranferPlanning.Value = objMonthEvi.PlanningTransferDate
                ICTglUpload.Value = objMonthEvi.UploadDate
                'photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=" & DestFile & objMonthEvi.EvidancePath '''& "&type=" & "SPKCustomer"
                'photoView.CssClass = "ShowControl"

                If Not String.IsNullorEmpty(objMonthEvi.FileNamePath) Then
                    IdUpload.Visible = False
                    IdPath.Visible = True
                End If

                If dataMD.id <> 0 AndAlso dataMD.AccountNumberBank <> "" Then
                    Dim dtbank As DealerBankAccount = New DealerBankAccountFacade(User).RetrieveByAccNo(dataMD.AccountNumberBank.Trim, CType(Request.QueryString("DealerCode"), String))
                    If dtbank.ID <> 0 Then
                        ddlRekeningTransfer.SelectedValue = dtbank.ID
                        ddlRekeningTransfer.Enabled = False
                    End If
                End If

                If objMonthEvi.FileNamePath <> "" Then
                    btnSave.Enabled = False
                    'photoSrc.Visible = False
                End If

            Else
                lblFaktur.Visible = False
                dFaktur.Visible = True
                dFaktur2.Visible = False
                btnSave.Enabled = True
            End If

        End If
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim strError As String = ""

        If String.IsNullOrEmpty(uploadFile(strError)) Then
            MessageBox.Show("Silahkan Mengupload Foto KTP Terlobih Dahulu")
            Return
        End If

        If Not String.IsNullOrEmpty(strError) Then
            MessageBox.Show(strError)
            Return
        End If
        _sessHelper.SetSession("UploadImage", "FAKTUR")

    End Sub

    Private Function uploadFile(ByRef errmsg As String) As String
        Dim fcd As New MonthlyDocumentFacade(User)
        Dim dataMD As MonthlyDocument = fcd.Retrieve(CType(Request.QueryString("IDmonth"), Integer))
        If dataMD.id <> 0 Then
            filePathName = dataMD.AccountingNo & "_" & dataMD.BillingNo
        End If

        Dim strComplaintNumber As String

        Dim SrcFile As String = Path.GetFileName(photoSrc.PostedFile.FileName)  '-- Source file name

        If String.IsNullorEmpty(SrcFile) Then
            errmsg = "Data Lampiran tidak boleh kosong"
            Return errmsg
        End If

        'Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + KTB.DNet.Lib.WebConfig.GetValue("MonthlyDocEvidance") & ""
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("MonthlyDocumentEvidance")
        Dim newNameFile As String = filePathName & "_" & Guid.NewGuid().ToString().Substring(0, 5) & ".pdf"
        'DestFile = DestFile & SrcFile

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim maxFinfo As Long = KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize")
        Dim finfo As New FileInfo(DestFile)
        Dim fileExt As String = System.IO.Path.GetExtension(Me.photoSrc.PostedFile.FileName)
        Try

            If (fileExt.ToUpper() <> ".PDF") Then
                errmsg = "Format file Hanya : pdf"
                Return errmsg
            End If

            If Me.photoSrc.PostedFile.ContentLength > 5120000 Then
                errmsg = "Ukuran File Maximal 5 MB"
                Return errmsg
            End If

            If imp.Start() Then
                Dim FileLocationSaveToDB = "\" & CType(_sessHelper.GetSession("DEALER"), Dealer).DealerCode & "\" & newNameFile
                Dim NewFileLocation As String = DestFile & "\" & CType(_sessHelper.GetSession("DEALER"), Dealer).DealerCode & "\" & newNameFile
                Dim strFileName As String = Path.GetFileName(Me.photoSrc.PostedFile.FileName)
                NewFileLocation = NewFileLocation & fileExt

                If Not IO.Directory.Exists(NewFileLocation) Then
                    IO.Directory.CreateDirectory(Path.GetDirectoryName(NewFileLocation))
                End If

                If IO.File.Exists(NewFileLocation) Then
                    IO.File.Delete(Path.GetDirectoryName(NewFileLocation))
                End If

                Dim objUpload As New UploadToWebServer
                objUpload.Upload(Me.photoSrc.PostedFile.InputStream, NewFileLocation)
                lblPathImage.Text = FileLocationSaveToDB & fileExt

                filePath = newNameFile

                'photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=" & FileLocationSaveToDB & fileExt & "&type=" & "FakturEvidanceMonthlyDocument"
                'photoView.CssClass = "ShowControl"
                imp.StopImpersonate()
                imp = Nothing
            End If

        Catch ex As Exception
            Throw ex
        End Try

        Return DestFile
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim id As Integer = CType(Request.QueryString("IDMonth"), Integer)
        Dim retun As Integer
        Dim strError As String = ""

        Dim facade As New MonthlyDocumentFacade(User)
        Dim dataMD As MonthlyDocument = facade.Retrieve(id)
        If dataMD.id <> 0 AndAlso dataMD.AccountNumberBank = "" Then
            If ddlRekeningTransfer.SelectedValue <> 0 Then
                Dim resultUpdate = facade.ExecuteSPAccountNumber(id, ddlRekeningTransfer.SelectedValue)
            End If
        End If

        'check tgl planning H+13
        Dim appConf As AppConfig = New AppConfigFacade(User).Retrieve("EnumMonthlyDocumentToFakturDTOP")
        Dim dateH11 As New Date(CInt(ICTanggalTranferPlanning.Value.Year), CInt(ICTanggalTranferPlanning.Value.Month), CInt(ICTanggalTranferPlanning.Value.Day))
        
        If dateH11 < ICTglUpload.Value Then
            MessageBox.Show("Tanggal Rencana Transfer tidak boleh kurang dari hari ini")
            Return
        End If

        If dateH11 < ICTglUpload.Value.AddDays(appConf.Value) Then
            MessageBox.Show("Tanggal Rencana Transfer tidak boleh kurang dari " & appConf.Value & " hari dari tgl upload")
            Return
        End If

        If dateH11.Day <> "10" AndAlso dateH11.Day <> "15" AndAlso dateH11.Day <> "25" Then
            MessageBox.Show("Silahkan pilih tanggal 10, 15, 25 !")
            Return
        End If

        'cek holiday
        For i As Integer = 1 To 31
            If IsNationalHolidayExist(dateH11.Year, dateH11.Month, dateH11.Day) Then
                dateH11 = dateH11.AddDays(1)
            Else
                Exit For
            End If
        Next

        If Not String.IsNullorEmpty(uploadFile(strError)) Then
            If Not String.IsNullorEmpty(strError) Then
                MessageBox.Show(strError)
                Return
            End If
        End If

        Dim objmonevi As New MonthlyDocumentToFakturEvidance
        Dim arra As MonthlyDocumentToFakturEvidance = New MonthlyDocumentToFakturEvidanceFacade(User).RetrieveByMDId(id)
        If arra.ID > 0 Then
            If arra.FileNamePath = String.Empty Or arra.FileNamePath = "" Then
                With objmonevi
                    .ID = arra.ID
                    '.MonthlyDocumentID = id
                    .FakturNumber = arra.FakturNumber
                    .PaymentDescription = arra.PaymentDescription
                    .UploadDate = Now.Date
                    .PlanningTransferDate = dateH11
                    .FileNamePath = IIf(String.IsNullorEmpty(filePath), arra.FileNamePath, filePath)
                    .CreatedBy = arra.CreatedBy
                    .MonthlyDocument = dataMD
                End With
                retun = New MonthlyDocumentToFakturEvidanceFacade(User).Update(objmonevi)
            End If
        Else
            If String.IsNullorEmpty(txtNomorFaktur.Text) Or String.IsNullorEmpty(txtNomorFaktur2.Text) Or String.IsNullorEmpty(txtNomorFaktur3.Text) Or String.IsNullorEmpty(txtNomorFaktur4.Text) Then
                MessageBox.Show("Silahkan input No Faktur Dahulu sesuai format")
                Return
            End If

            If txtNomorFaktur4.Text.Length <> 8 Then
                MessageBox.Show("Silahkan input No Faktur dengan 8 karakter")
                Return
            End If

            If String.IsNullorEmpty(txtDeskripsiPembayaran.Text) Then
                MessageBox.Show("Silahkan input Deskripsi Pembayaran Dahulu")
                Return
            End If

            If ddlRekeningTransfer.SelectedValue = 0 Then
                MessageBox.Show("Silahkan pilih Bank Account Number Dahulu")
                Return
            End If

            ICTanggalTranferPlanning.Value = dateH11

            With objmonevi
                .FakturNumber = txtNomorFaktur.Text & "." & txtNomorFaktur2.Text & "." & txtNomorFaktur3.Text & "-" & txtNomorFaktur4.Text
                .PaymentDescription = txtDeskripsiPembayaran.Text
                .EvidancePath = lblPathImage.Text
                '.MonthlyDocumentID = id
                .UploadDate = Now.Date
                .PlanningTransferDate = dateH11
                .FileNamePath = filePath
                .MonthlyDocument = dataMD
            End With

            If Not cekKelengkapan(objmonevi) Then
                lblError.Text = objmonevi.ErrorMessage
                Return
            End If
            lblError.Text = ""
            retun = New MonthlyDocumentToFakturEvidanceFacade(User).Insert(objmonevi)
            If retun > 0 Then
                TransferToSAP() '------> transfer to SAP
            End If
        End If

        If retun > 0 Then
            lblNomorUpload.Text = retun
            objmonevi.ID = retun
            _sessHelper.SetSession("IDUploadEvidance", objmonevi)
            btnSave.Enabled = False
            ddlRekeningTransfer.Enabled = False
            IdUpload.Visible = False
            IdPath.Visible = True
            lblPathFile.Text = filePath
            MessageBox.Show("Proses Simpan Berhasil")
        Else
            MessageBox.Show("Proses Simpan Gagal")
        End If

    End Sub

    Private Function IsNationalHolidayExist(ByVal year As Integer, ByVal month As Integer, ByVal day As Integer) As Boolean
        Dim objNatfacade As NationalHolidayFacade = New NationalHolidayFacade(User)
        If objNatfacade.IsActiveDateExist(year, month, day) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function cekKelengkapan(ByRef objmonevi As MonthlyDocumentToFakturEvidance) As Boolean
        Dim ret As Boolean = True
        With objmonevi
            'If String.IsNullOrEmpty(.EvidancePath) Then
            '    .ErrorMessage = " Silahkan Upload Bukti Terlebih Dahulu <br/>"
            '    ret = False
            'End If

            If String.IsNullOrEmpty(.FakturNumber) Then
                .ErrorMessage = " Nomor Faktur Harus DiIsi <br/>"
                ret = False
            End If

        End With

        Return ret
    End Function

    Private Sub TransferToSAP()
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)
        Dim fName As String = "BILLINGUPDATE_" & sSuffix

        'Dim BillingUpdate As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\SalesComm\BabitPaymentData" & sSuffix & ".txt"
        Dim BillingUpdate As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "MonthlyDocEvidance\BillingUpdate\" & fName & ".txt"
        'Dim BillingUpdate As String = "\\172.17.31.128\Data\NFS_MMC\DNET\SERVICE\" & sSuffix & ".txt"""
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then
                Dim dirInfo As New DirectoryInfo(Path.GetDirectoryName(BillingUpdate))
                If Not dirInfo.Exists Then
                    dirInfo.Create()
                End If

                Dim finfo As FileInfo = New FileInfo(BillingUpdate)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                Dim fs As FileStream = New FileStream(BillingUpdate, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteBillingData(sw)

                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
                'MessageBox.Show("Data berhasil diupload ke SAP")

            End If

        Catch ex As Exception
            Dim errMess As String = ex.Message
        End Try
    End Sub

    Private Function WriteBillingData(ByRef sw As StreamWriter)
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)
        Dim lines As New System.Text.StringBuilder
        Dim separator As String = ";"

        Dim id As Integer = CType(Request.QueryString("IDMonth"), Integer)
        Dim arra As MonthlyDocument = New MonthlyDocumentFacade(User).Retrieve(id)
        If arra.id > 0 Then

            lines.Append("H")
            lines.Append(separator)

            'lines.Append("BILLINGUPDATE_" & sSuffix & "\nH")
            'lines.Append(separator)

            lines.Append(arra.BillingNo)
            lines.Append(separator)

            lines.Append(arra.AccountingNo)
            lines.Append(separator)

            lines.Append(arra.TransferDate.ToString("ddMMyyyy"))
            lines.Append(separator)

            Dim dataMDFE As MonthlyDocumentToFakturEvidance = New MonthlyDocumentToFakturEvidanceFacade(User).RetrieveByMDId(id)
            If dataMDFE.ID <> 0 Then
                lines.Append(dataMDFE.FakturNumber)
                lines.Append(separator)

                lines.Append(dataMDFE.PaymentDescription)
                lines.Append(separator)
            Else
                lines.Append("")
                lines.Append(separator)

                lines.Append("")
                lines.Append(separator)
            End If

            Dim dtbank As DealerBankAccount = New DealerBankAccountFacade(User).Retrieve(CInt(ddlRekeningTransfer.SelectedValue))
            If dtbank.ID <> 0 Then
                lines.Append(dtbank.BankAccount)
                lines.Append(separator)
            Else
                lines.Append("")
                lines.Append(separator)
            End If
            
        End If

        sw.WriteLine(lines.ToString())

    End Function

    Private Sub BindBankAccount(ByVal Code As String)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "Dealer.DealerCode", MatchType.Exact, Code))

        Dim Coll As ArrayList = New DealerBankAccountFacade(User).Retrieve(criterias)
        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlRekeningTransfer.Items.Add(item)
        If Coll.Count > 0 Then
            For Each dba As DealerBankAccount In Coll
                item = New ListItem(dba.BankAccount & " | " & dba.BankName, dba.ID)
                ddlRekeningTransfer.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Server.Transfer("frmMonthlyDocument.aspx?SCDealer=" & SCDealer.Value & "&JDoc=" & JDoc.Value & "&NoFaktur=" & NoFaktur.Value _
                        & "&Month=" & Month.Value & "&Year=" & Year.Value & "&PCategory=" & PCategory.Value & "&BillingNo=" & BillingNo.Value _
                            & "&Download=" & Download.Value & "&MonthTo=" & MonthTo.Value & "&YearTo=" & YearTo.Value & "&AccountingNo=" & AccountingNo.Value & "&Criterias=True", True)
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        'Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & "MonthlyDocEvidance\" & CType(_sessHelper.GetSession("DEALER"), Dealer).DealerCode & "\" & lblPathFile.Text & ".pdf")
        'Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("MonthlyDocumentEvidance") & "\" & CType(_sessHelper.GetSession("DEALER"), Dealer).DealerCode & "\" & lblPathFile.Text)
        'Dim FileDownload As String = "\\172.17.31.135\MDNET_REPO\Repository\BSI-Net\MonthlyDocEvidance\" & CType(_sessHelper.GetSession("DEALER"), Dealer).DealerCode & "\" & lblPathFile.Text & ".pdf"

        Dim FileDownload As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "MonthlyDocEvidance\" & CType(_sessHelper.GetSession("DEALER"), Dealer).DealerCode & "\" & lblPathFile.Text & ".pdf"

        Dim fileExists As Boolean = CheckFileExist(FileDownload)
        If fileExists Then
            Response.Redirect("../Download.aspx?file=" & FileDownload)
        Else
            MessageBox.Show("File tidak ditemukan!")
            Return
        End If


    End Sub

    Private Function CheckFileExist(ByVal fileinfo As String) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return New FileInfo(fileinfo).Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try

        Return False
    End Function

    Private Function CheckFileExist(ByVal fileinfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return fileinfo.Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try

    End Function

    Private Sub JenisDokumen()
        Dim arlList As New ArrayList
        If Not SecurityProvider.Authorize(Context.User, SR.DokumenServiceAll_Privilege) Then
            Me.ddlJenisDokumen.DataSource = New ArrayList
            Me.ddlJenisDokumen.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            Dim arlListTemp As ArrayList = MonthlyDocumentType.RetrieveDocumentType()
            For Each item As MonthlyDocumentTypeListItem In arlListTemp
                If SecurityProvider.Authorize(Context.User, SR.DocServiceIndepB_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Interest Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DocServiceKudepB_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Kwitansi Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DocServicePMLett_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Periodical_Maintenance_Letter Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_wscsta_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Warranty_ESP_Status_List Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lwsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Warranty_ESP_Letter Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lfsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Service_Campaign_Letter Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lfs001_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Service_Regular_Letter Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lpdi01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.PDI_Letter Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_kfsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Service_Campaign Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_kwsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_ESP_Warranty Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_Depb01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Report Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lpdi02_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kend_Belum_PDI_List Then
                        arlList.Add(item)
                    End If
                End If

                'farid additional 20190828 ---------------------------------------------------------------------------------
                If SecurityProvider.Authorize(Context.User, SR.Free_service_regular_status_list_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_service_regular_status_list Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Free_service_campaign_status_list_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_service_campaign_status_list Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Free_maintenance_status_list_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_maintenance_status_list Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Free_labor_status_list_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_labor_status_list Then
                        arlList.Add(item)
                    End If
                End If
                'farid additional 20190828 ---------------------------------------------------------------------------------


                'Tambahan CR Standard
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_fll01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_ESP_Labour_Letter Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.DokumenService_kfl01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_ESP_Free_Labour Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.dokumen_service_lfm01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Maintenance_Letter Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.dokumen_service_kfm01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Maintenance Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Kwitansi_Warranty_Spare_Part_Accessories_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Warranty_Spare_Part_Accessories Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Kwitansi_Free_Maintenance_and_Campaign_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Maintenance_and_Campaign Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Free_Maintenance_and_campaign_letter_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Maintenance_and_campaign_letter Then
                        arlList.Add(item)
                    End If
                End If

            Next
            'Start  : by donas for Yurike/b Widya on 2014.09.09'
            Dim aList As New ArrayList
            For Each item As MonthlyDocumentTypeListItem In arlList
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kend_Belum_PDI_List Then
                Else
                    aList.Add(item)
                End If
            Next
            arlList = aList
            'End    : by donas for Yurike/b Widya on 2014.09.09'

            Me.ddlJenisDokumen.DataSource = arlList
            Me.ddlJenisDokumen.DataTextField = "NameStatus"
            Me.ddlJenisDokumen.DataValueField = "ValStatus"
            Me.ddlJenisDokumen.DataBind()
            Me.ddlJenisDokumen.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        End If
    End Sub
End Class