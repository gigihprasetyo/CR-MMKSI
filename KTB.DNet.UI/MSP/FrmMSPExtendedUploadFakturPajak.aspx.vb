Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
Imports OfficeOpenXml
Imports System.Collections.Generic
Imports System.Linq
Imports PdfSharp
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports System.Text
Imports System.IO
Imports System.Reflection
Imports PDFHelper
Imports Excel
Imports GlobalExtensions
Imports System

Public Class FrmMSPExtendedUploadFakturPajak
    Inherits System.Web.UI.Page

    Dim helpers As TrainingHelpers = New TrainingHelpers(Me.Page)
    Private oDealer As Dealer
    Private _sessHelper As New SessionHelper
    Private _UploadPriv As Boolean = False
    Private _DaftarPriv As Boolean = False


    Private Sub InitPriv()
        _UploadPriv = SecurityProvider.Authorize(Context.User, SR.Efaktur_Upload)
        _DaftarPriv = SecurityProvider.Authorize(Context.User, SR.Efaktur_Lihat_Daftar)
        If Not _UploadPriv Then
            trUpload1.Visible = False
            trUpload2.Visible = False
            trUpload3.Visible = False
            trUpload4.Visible = False
            trUpload5.Visible = False
        End If

        If Not _DaftarPriv Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=EFaktur - Dokumen EFaktur")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitPriv()
        oDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        If Not IsPostBack Then
            BindDropdownList()
            ViewState("CurrentSortColumn") = "CreatedTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                lblSearchDealer.Attributes("OnClick") = "showPopUp('../General/../PopUp/PopUpDealerSelection.aspx', '', 500, 760, DealerSelection);"
                lblKodeDealer.Visible = False
            Else
                lblSearchDealer.Visible = False
                txtKodeDealer.Visible = False
                lblKodeDealer.Text = oDealer.DealerCode
            End If
        Else
            If Request.Form("hdnValSubmit") = "1" Then
                btnUpload_Click(Nothing, Nothing)
                hdnValSubmit.Value = "-1"
            End If
        End If
    End Sub

    Private Sub BindDropdownList()
        ddlTransaksiFind.DataSource = New EnumEFaktur().RetrieveTransactionType
        ddlTransaksiFind.DataTextField = "NameTransactionType"
        ddlTransaksiFind.DataValueField = "ValTransactionType"
        ddlTransaksiFind.DataBind()
        'ddlTransaksiFind.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
        'ddlTransaksiFind.SelectedIndex = 0

        ddlTransaksiUpload.DataSource = New EnumEFaktur().RetrieveTransactionType
        ddlTransaksiUpload.DataTextField = "NameTransactionType"
        ddlTransaksiUpload.DataValueField = "ValTransactionType"
        ddlTransaksiUpload.DataBind()
        ddlTransaksiUpload.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
        ddlTransaksiUpload.SelectedIndex = 0
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            btnUpload.Text = "Harap menunggu..."
            If ddlTransaksiUpload.SelectedIndex = 0 Then
                Throw New Exception("Tipe Transaksi belum di pilih")
            End If

            Dim EFakturHeader As EFakturHeader
            If hdnValSubmit.Value = "-1" Then
                EFakturHeader = GetEFakturHeaderData()
                _sessHelper.SetSession("oEFakturHeader", EFakturHeader)
            ElseIf hdnValSubmit.Value = "1" Then
                EFakturHeader = _sessHelper.GetSession("oEFakturHeader")
            End If

            Dim ValidData As Boolean = True
            Dim InvalidDebitNumber As String = String.Empty
            For Each EFItem As EFakturItem In EFakturHeader.ListOfItem
                If EFItem.Status <> EnumEFaktur.EnumEFakturStatus.VALID Then
                    InvalidDebitNumber = InvalidDebitNumber & EFItem.DocNumber & "\n"
                    ValidData = False
                End If
            Next

            If Not ValidData Then
                Throw New Exception("Upload Data GAGAL, no debit memo \nberikut tidak ditemukan:\n" & InvalidDebitNumber)
            End If

            Dim ArlDuplicate As New ArrayList
            CheckDuplicateData(EFakturHeader, ArlDuplicate)
            If ArlDuplicate.Count > 0 Then
                Dim duplicateDebit As String = String.Empty
                For Each item As EFakturItem In ArlDuplicate
                    If duplicateDebit.Length = 0 Then
                        duplicateDebit = item.DocNumber & "\n"
                    Else
                        duplicateDebit = duplicateDebit & item.DocNumber & "\n"
                    End If
                Next
                If hdnValSubmit.Value = "-1" Then
                    MessageBox.Confirm("Dokumen faktur pajak atas nomor debit memo : \n" & duplicateDebit & "Sudah tersedia, apakah dokumen akan direvisi?", "hdnValSubmit")
                    Return
                ElseIf hdnValSubmit.Value = "1" Then
                    For Each item As EFakturItem In ArlDuplicate
                        DeleteEFaktur(item)
                    Next
                End If
            End If

            Dim iResult As Integer = New EFakturHeaderFacade(User).Insert(EFakturHeader)

            If iResult > 0 Then
                MessageBox.Show("Upload file Berhasil")
            Else
                MessageBox.Show("Upload file Gagal")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            RestateButton()
        End Try

    End Sub

    Public Sub CheckDuplicateData(ByVal oEFakturHeader As EFakturHeader, ByRef arlDuplicate As ArrayList)
        If oEFakturHeader.ListOfItem.Count > 0 Then
            For Each item As EFakturItem In oEFakturHeader.ListOfItem
                Dim oItem As EFakturItem = New EFakturItemFacade(User).Retrieve(item.DocNumber, oEFakturHeader.TransactionType)
                If oItem.ID > 0 Then
                    arlDuplicate.Add(oItem)
                End If
            Next
        End If
    End Sub

    Private Function GetEFakturHeaderData() As EFakturHeader
        Dim excelExtension() As String = {".xls", ".xlsx"}
        Dim excelFileName As String = UploadAndGetFileName(uplExcel, excelExtension)
        Dim EFakturHeader As EFakturHeader = MappingEFakturDataFromExcelFile(excelFileName)

        Dim pdfExtension() As String = {".pdf"}
        Dim pdfFileName As String = UploadAndGetFileName(uplPdf, pdfExtension)
        ValidateEFakturItemWithPdfFile(EFakturHeader, pdfFileName)
        Return EFakturHeader
    End Function

    Private Function MappingEFakturDataFromExcelFile(ByVal excelFileName As String) As EFakturHeader
        Dim result As New EFakturHeader

        Dim fileInfo As FileInfo = New FileInfo(excelFileName)

        Using excelPkg As New ExcelPackage(fileInfo)
            Using ws As ExcelWorksheet = excelPkg.Workbook.Worksheets.Item(1)
                Dim ColumnCount As Integer = ws.Dimension.End.Column
                Dim RowCount As Integer = ws.Dimension.End.Row

                Dim arlItem As New ArrayList

                For idx As Integer = 2 To RowCount

                    Dim item As New EFakturItem
                    Try
                        item.DocNumber = ws.GetCellValue(idx, 2)
                        If ws.GetCellValue(idx, 3) <> "" Then
                            item.PageNumber = ws.GetCellValue(idx, 3)
                        End If
                    Catch ex As Exception
                        Throw New Exception("Terjadi kesalahan dalam memproses file excel, harap cek kembali file anda")
                    End Try

                    item.Status = SetItemStatusAndDebitNumber(item)

                    arlItem.Add(item)

                Next
                result.TransactionType = ddlTransaksiUpload.SelectedValue
                result.ListOfItem = arlItem
            End Using
        End Using

        If File.Exists(excelFileName) Then
            File.Delete(excelFileName)
        End If

        Return result
    End Function

    Private Function UploadAndGetFileName(ByVal fileUpload As System.Web.UI.HtmlControls.HtmlInputFile, ByVal extension() As String) As String

        Dim fileInfo As FileInfo
        Dim fileName As String = String.Empty
        Dim fileSize As Long = GetMaxFileSizeByAppConfig()
        Dim errMessage As String = GetErrorMessageByAppConfig()
        'Dim listTrClass As List(Of TrClass) = New List(Of TrClass)
        'Dim listError As List(Of ErrorExcelUpload) = New List(Of ErrorExcelUpload)
        Dim resultUpload As String = UploadFile(fileUpload, KTB.DNet.Lib.WebConfig.GetValue("EFakturVTADirectory"), fileSize, extension, errMessage)
        Dim errArr() As String = resultUpload.Split("|")
        If errArr(0).ToLower().Equals("error") Then
            Throw New Exception(errArr(1))
        Else
            fileName = KTB.DNet.Lib.WebConfig.GetValue("SAN") & errArr(1)
        End If

        Return fileName

    End Function

    Private Function GetMaxFileSizeByAppConfig() As Long
        Dim fileSize As Long
        Try
            Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
            Dim objAppConfig As AppConfig = objAppConfigFacade.Retrieve("EFaktur_MAX_FILE_SIZE")
            fileSize = Convert.ToInt64(objAppConfig.Value)
        Catch ex As Exception
            fileSize = 10240000
        End Try

        Return fileSize
    End Function

    Private Function GetErrorMessageByAppConfig() As String
        Dim errMessage As String
        Try
            Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
            Dim objAppConfig As AppConfig = objAppConfigFacade.Retrieve("EFaktur_ERROR_MESSAGE")
            errMessage = objAppConfig.Value
        Catch ex As Exception
            errMessage = ""
        End Try

        Return errMessage
    End Function

    Private Sub ValidateEFakturItemWithPdfFile(ByRef oEFakturHeader As EFakturHeader, ByVal pdfFileName As String)
        Dim listPdfFile As List(Of PdfDocument) = New List(Of PdfDocument)
        Dim fileExt As String = System.IO.Path.GetExtension(pdfFileName)

        Dim pdfDoc As PdfDocument = PdfReader.Open(pdfFileName, PdfDocumentOpenMode.Import)

        oEFakturHeader.PdfFilePath = pdfFileName.Replace(KTB.DNet.Lib.WebConfig.GetValue("SAN").ToString(), "")

        'If oEFakturHeader.ListOfItem.Count <> pdfDoc.PageCount Then
        '    Throw New Exception("Jumlah Row EFaktur tidak sama dengan Jumlah Halaman file PDF")
        'End If

        For Each item As EFakturItem In oEFakturHeader.ListOfItem
            Dim pdfPages As Integer = 0
            Dim pdfText As String = String.Empty
            Dim isDataOK As Boolean = False
            For idx As Integer = 0 To pdfDoc.PageCount - 1
                Dim outputText As String = ""

                For index As Integer = 0 To pdfDoc.Pages(idx).Contents.Elements.Count - 1
                    Dim stream As PdfDictionary.PdfStream = pdfDoc.Pages(idx).Contents.Elements.GetDictionary(index).Stream
                    outputText += New PDFParser().ExtractTextFromPDFBytes(stream.Value)
                Next
                If outputText.Contains(item.DocNumber) Then
                    pdfText = outputText
                    pdfPages = idx + 1
                    isDataOK = True
                    Exit For
                End If
            Next
            If Not isDataOK Then
                Throw New Exception("Data EFaktur Excel tidak sama dengan data file PDF")
            End If

            If item.PageNumber = "" Then
                item.PageNumber = pdfPages
            End If
            item.Status = SetItemStatusByDocument(item, pdfText)
        Next

    End Sub

    Private Function SetItemStatusAndDebitNumber(ByRef item As EFakturItem) As Short
        If ddlTransaksiUpload.SelectedValue = EnumEFaktur.EnumEFakturTransactionType.MSPEXTENDED Then
            Dim oMSPExFakturPajak As MSPExFakturPajak = New MSPExFakturPajakFacade(User).RetrieveByFakturPajakNo(item.DocNumber)

            If oMSPExFakturPajak.ID = 0 Then
                Return EnumEFaktur.EnumEFakturStatus.NOT_EXISTS
            Else
                item.DocNumber = oMSPExFakturPajak.FakturPajakNo
                Return EnumEFaktur.EnumEFakturStatus.VALID
            End If
        End If
    End Function

    Private Function SetItemStatusByDocument(item As EFakturItem, outputText As String) As Short
        If Not outputText.Contains(item.DocNumber) Then
            Select Case item.Status
                Case CInt(EnumEFaktur.EnumEFakturStatus.VALID)
                    Return EnumEFaktur.EnumEFakturStatus.VALID_DOCUMENT_NOT_MATCH
            End Select
        Else
            Return item.Status
        End If
    End Function

    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "XLS" Then
            retValue = True
        ElseIf ext.ToUpper() = "XLSX" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function

    Private Sub RestateButton()
        btnUpload.Text = "Upload"
        btnUpload.Enabled = True
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ddlTransaksiFind.SelectedIndex = 0
        ddlTransaksiUpload.SelectedIndex = 0
        txtDocumentNo.Text = String.Empty
        txtKodeDealer.Text = String.Empty
        DateFrom.Value = Date.Now.Date
        DateTo.Value = Date.Now.Date
        dtgEFaktur.DataSource = New ArrayList
        dtgEFaktur.DataBind()
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindGrid(0)
    End Sub

    Private Sub BindGrid(ByVal index As Integer)
        Dim crit As CriteriaComposite = SearchCriteria()
        Dim arlFaktur As ArrayList = New EFakturItemFacade(User).Retrieve(crit)
        Dim totalRow As Integer = 0
        Dim arlData As ArrayList = New EFakturItemFacade(User).RetrieveActiveList(index + 1, dtgEFaktur.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection), crit)
        If arlData.Count = 0 Then
            MessageBox.Show("Data tidak ditemukan")
            dtgEFaktur.DataSource = New ArrayList
            dtgEFaktur.DataBind()
            Exit Sub
        End If

        dtgEFaktur.CurrentPageIndex = index
        dtgEFaktur.DataSource = arlData
        dtgEFaktur.VirtualItemCount = totalRow
        dtgEFaktur.DataBind()
    End Sub

    Protected Sub dtgEFaktur_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgEFaktur.PageIndexChanged
        dtgEFaktur.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

    Public Function SearchCriteria() As CriteriaComposite
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EFakturItem), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        crit.opAnd(New Criteria(GetType(EFakturItem), "CreatedTime", MatchType.GreaterOrEqual, Format(DateFrom.Value, "yyyy/MM/dd")))
        DateTo.Value = DateAdd(DateInterval.Day, 1, DateTo.Value)
        crit.opAnd(New Criteria(GetType(EFakturItem), "CreatedTime", MatchType.LesserOrEqual, Format(DateTo.Value, "yyyy/MM/dd")))

        If ddlTransaksiFind.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(EFakturItem), "EFakturHeader.TransactionType", MatchType.Exact, ddlTransaksiFind.SelectedValue))
        End If

        Dim sDealerCode As String = String.Empty
        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            sDealerCode = txtKodeDealer.Text.Trim.Replace(";", "','")
        Else
            sDealerCode = lblKodeDealer.Text
        End If

        If sDealerCode.Trim.Length > 0 Then
            Dim sDocNumber As String = String.Empty
            Select Case ddlTransaksiFind.SelectedValue
                Case EnumEFaktur.EnumEFakturTransactionType.MSPEXTENDED
                    Dim critDebMem As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExDebitMemo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critDebMem.opAnd(New Criteria(GetType(MSPExDebitMemo), "MSPExRegistration.Dealer.DealerCode", MatchType.InSet, "('" & sDealerCode & "')"))
                    Dim arlDebitMemo As ArrayList = New MSPExDebitMemoFacade(User).Retrieve(critDebMem)
                    If arlDebitMemo.Count > 0 Then
                        For Each oDebitMemo As MSPExDebitMemo In arlDebitMemo
                            If sDocNumber.Length = 0 Then
                                sDocNumber = oDebitMemo.DebitMemoNo
                            Else
                                sDocNumber = sDocNumber & "','" & oDebitMemo.DebitMemoNo
                            End If
                        Next
                    End If

                    Dim critMSPExFakturPajak As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExFakturPajak), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critMSPExFakturPajak.opAnd(New Criteria(GetType(MSPExFakturPajak), "MSPExRegistration.Dealer.DealerCode", MatchType.InSet, "('" & sDealerCode & "')"))
                    Dim arlMSPExFakturPajak As ArrayList = New MSPExFakturPajakFacade(User).Retrieve(critMSPExFakturPajak)
                    If arlMSPExFakturPajak.Count > 0 Then
                        For Each oMSPExFakturPajak As MSPExFakturPajak In arlMSPExFakturPajak
                            If sDocNumber.Length = 0 Then
                                sDocNumber = oMSPExFakturPajak.FakturPajakNo
                            Else
                                sDocNumber = sDocNumber & "','" & oMSPExFakturPajak.FakturPajakNo
                            End If
                        Next
                    End If




                    crit.opAnd(New Criteria(GetType(EFakturItem), "DocNumber", MatchType.InSet, "('" & sDocNumber & "')"))

                Case Else
                    Exit Select
            End Select
        End If

        If txtDocumentNo.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(EFakturItem), "DocNumber", MatchType.Exact, txtDocumentNo.Text.Trim))
        End If

        Return crit
    End Function

    Protected Sub dtgEFaktur_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgEFaktur.ItemCommand
        Try
            Select Case e.CommandName
                Case "Download"
                    Dim item As EFakturItem = New EFakturItemFacade(User).Retrieve(CInt(e.CommandArgument))
                    DownloadSingleEFaktur(item)
                Case "Delete"
                    Dim item As EFakturItem = New EFakturItemFacade(User).Retrieve(CInt(e.CommandArgument))
                    Dim SucessDelete As Boolean = DeleteEFaktur(item)
                    If SucessDelete Then
                        MessageBox.Show("Faktur " & item.DocNumber & " Berhasil dihapus")
                    End If
                    BindGrid(0)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function DeleteEFaktur(ByVal item As EFakturItem) As Boolean
        Try
            Dim facade As New EFakturItemFacade(User)
            facade.Delete(item)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub DownloadSingleEFaktur(ByVal data As EFakturItem)
        Dim pdfDoc As New PdfDocument()
        pdfDoc.Options.FlateEncodeMode = PdfFlateEncodeMode.BestCompression
        pdfDoc.Options.UseFlateDecoderForJpegImages = PdfUseFlateDecoderForJpegImages.Automatic
        pdfDoc.Options.NoCompression = False
        pdfDoc.Options.CompressContentStreams = True
        Try
            Dim page As PdfPage = Nothing
            If data.PageNumber.Contains("-") Then
                Dim startPage As Integer = CInt(data.PageNumber.Split("-")(0))
                Dim endPage As Integer = CInt(data.PageNumber.Split("-")(1))
                For i As Integer = startPage To endPage
                    page = GetPagePerEFaktur(data.EFakturHeader.PdfFilePath, i)
                    pdfDoc.AddPage(page)
                Next
            Else
                page = GetPagePerEFaktur(data.EFakturHeader.PdfFilePath, data.PageNumber)
                pdfDoc.AddPage(page)
            End If
        Catch
            Return
        End Try
        Dim fileName As String = Guid.NewGuid().ToString().Substring(0, 5) & ".pdf"
        SaveFileToTempAndDownload(pdfDoc, fileName)
    End Sub

    Public Function UploadFile(ByVal ctrInput As HtmlInputFile, ByVal subPath As String, ByVal maxSize As Long, Optional ByVal extension() As String = Nothing, Optional ByVal errMessage As String = "") As String
        Dim strComplaintNumber As String
        Dim success As Boolean = False
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim ServerTarget As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        Dim maxFinfo As Long = maxSize

        Dim SrcFile As String = Path.GetFileName(ctrInput.PostedFile.FileName)  '-- Source file name
        If String.IsNullorEmpty(SrcFile) Then
            Return "Error|Silahkan Mengupload Terlebih Dahulu"
        End If

        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim fileExt As String = System.IO.Path.GetExtension(ctrInput.PostedFile.FileName)
        Dim newNameFile As String = String.Format("\{0}\{1}\{2}{3}", _
                                                 subPath, _
                                                 Date.Now.Year, _
                                                 Guid.NewGuid().ToString().Substring(0, 5), _
                                                 fileExt)
        Try
            success = imp.Start()
            If extension IsNot Nothing Then
                If Not (extension.Contains(fileExt)) Then
                    Return "Error|Format File Hanya : " & String.Join(", ", extension)
                End If
            End If

            Dim warnSize As String = "0"
            If maxFinfo > 5120000 Then
                warnSize = "10"
            Else
                warnSize = "5"
            End If

            If ctrInput.PostedFile.ContentLength > maxFinfo Then
                If errMessage = "" Then
                    Return "Error|Ukuran File Maximal " & warnSize & " Mb"
                Else
                    Return errMessage
                End If

            End If

            If imp.Start() Then
                Dim NewFileLocation As String = ServerTarget & newNameFile
                Dim strFileName As String = Path.GetFileName(ctrInput.PostedFile.FileName)

                If Not Directory.Exists(NewFileLocation) Then
                    Directory.CreateDirectory(Path.GetDirectoryName(NewFileLocation))
                End If

                If File.Exists(NewFileLocation) Then
                    File.Delete(Path.GetDirectoryName(NewFileLocation))
                End If

                Dim objUpload As New UploadToWebServer
                objUpload.Upload(ctrInput.PostedFile.InputStream, NewFileLocation)

                Return "Success|" & newNameFile
            End If

        Catch ex As Exception
            Throw ex
        End Try

        Return ServerTarget
    End Function

    Private Function GetPagePerEFaktur(ByVal mainFilePath As String, ByVal pageNumber As Integer) As PdfPage
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim filePath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + mainFilePath
        Dim fileInfo As New FileInfo(filePath)
        Dim destFilePath As String = fileInfo.FullName
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                If (fileInfo.Exists) Then
                    Dim pdfDoc As PdfDocument = PdfReader.Open(filePath, PdfDocumentOpenMode.Import)
                    Dim page As PdfPage = pdfDoc.Pages(pageNumber - 1)
                    Return page
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                End If
                imp.StopImpersonate()
            End If
        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail(fileInfo.Name))
        End Try
    End Function

    Private Sub SaveFileToTempAndDownload(pdfDoc As PdfDocument, fileName As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                pdfDoc.Save(Server.MapPath("~/DataTemp/" & fileName))
                imp.StopImpersonate()
            End If
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail(fileName))
        End Try
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)

    End Sub

    Protected Sub dtgEFaktur_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgEFaktur.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblDocNo As Label = CType(e.Item.FindControl("lblDocNo"), Label)
            Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            Dim lbtnHapus As LinkButton = CType(e.Item.FindControl("lbtnHapus"), LinkButton)

            Dim rowValue As EFakturItem = CType(e.Item.DataItem, EFakturItem)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgEFaktur.CurrentPageIndex * dtgEFaktur.PageSize)

            If ddlTransaksiFind.SelectedValue = EnumEFaktur.EnumEFakturTransactionType.MSPEXTENDED Then
                Dim oMSPExFakturPajak As MSPExFakturPajak = New MSPExFakturPajakFacade(User).RetrieveByFakturPajakNo(rowValue.DocNumber)
                If oMSPExFakturPajak.ID > 0 Then
                    lblDealerCode.Text = oMSPExFakturPajak.MSPExRegistration.Dealer.DealerCode
                    lblDealerName.Text = oMSPExFakturPajak.MSPExRegistration.Dealer.DealerName
                Else
                    Dim oMSPExDebitMemo As MSPExDebitMemo = New MSPExDebitMemoFacade(User).Retrieve(rowValue.DocNumber)
                    If oMSPExDebitMemo.ID > 0 Then
                        lblDealerCode.Text = oMSPExDebitMemo.MSPExRegistration.Dealer.DealerCode
                        lblDealerName.Text = oMSPExDebitMemo.MSPExRegistration.Dealer.DealerName
                    End If
                End If
            End If
            lblDocNo.Text = rowValue.DocNumber
            lbtnHapus.CommandArgument = rowValue.ID
            lbtnDownload.CommandArgument = rowValue.ID
            If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                lbtnHapus.Visible = False
            End If
        End If
    End Sub
End Class