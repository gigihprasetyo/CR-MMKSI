Imports System.Text
Imports System.IO
Imports System.Reflection
Imports PDFHelper
Imports GlobalExtensions

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
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
Imports Patagames.Pdf

#End Region

Public Class FrmUploadESRUT
    Inherits System.Web.UI.Page

    Dim helpers As TrainingHelpers = New TrainingHelpers(Me.Page)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()
    End Sub

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.ESRUT_Upload_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ESRUT - Upload ESRUT")
        End If
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Try

            btnProcess.Text = "Harap menunggu..."
            ValidateFile()
            Dim esrutHeader As ESRUTHeader = GetESRUTHeaderData(False)

            Dim iResult As Integer = New ESRUTHeaderFacade(User).Insert(esrutHeader)

            If iResult > 0 Then
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        Finally
            RestateButton()
        End Try


    End Sub

    Private Sub RestateButton()
        btnProcess.Text = "Proses"
        btnProcess.Enabled = True
        btnReupload.Text = "Reupload"
        btnReupload.Enabled = True
    End Sub

    Private Function GetESRUTHeaderData(ByVal isReupload As Boolean) As ESRUTHeader
        Dim excelExtension() As String = {".xls", ".xlsx"}
        Dim excelFileName As String = UploadAndGetFileName(uplExcel, excelExtension)
        Dim esrutHeader As ESRUTHeader = MappingESRUTDataFromExcelFile(excelFileName, isReupload)

        Dim pdfExtension() As String = {".pdf"}
        Dim pdfFileName As String = UploadAndGetFileName(uplPdf, pdfExtension)
        ValidateESRUTItemWithPdfFile(esrutHeader, pdfFileName)

        Return esrutHeader
    End Function

    Private Function UploadAndGetFileName(ByVal fileUpload As System.Web.UI.HtmlControls.HtmlInputFile, ByVal extension() As String) As String

        Dim fileInfo As FileInfo
        Dim fileName As String = String.Empty
        Dim fileSize As Long = GetMaxFileSizeByAppConfig()
        Dim errMessage As String = GetErrorMessageByAppConfig()
        'Dim listTrClass As List(Of TrClass) = New List(Of TrClass)
        'Dim listError As List(Of ErrorExcelUpload) = New List(Of ErrorExcelUpload)
        Dim resultUpload As String = helpers.UploadFile(fileUpload, KTB.DNet.Lib.WebConfig.GetValue("ESRUTVTADirectory"), fileSize, extension, errMessage)
        Dim errArr() As String = resultUpload.Split("|")
        If errArr(0).ToLower().Equals("error") Then
            Throw New Exception(errArr(1))
        Else
            fileName = KTB.DNet.Lib.WebConfig.GetValue("SAN") & errArr(1)
        End If

        Return fileName

    End Function

    Private Function MappingESRUTDataFromExcelFile(ByVal excelFileName As String, ByVal isReupload As Boolean) As ESRUTHeader
        Dim result As New ESRUTHeader

        Dim fileInfo As FileInfo = New FileInfo(excelFileName)

        Using excelPkg As New ExcelPackage(fileInfo)
            Using ws As ExcelWorksheet = excelPkg.Workbook.Worksheets.Item(1)
                Dim ColumnCount As Integer = ws.Dimension.End.Column
                Dim RowCount As Integer = ws.Dimension.End.Row

                result.NoPengajuan = ws.GetCellValue(3, 3).ToString()
                result.Perusahaan = ws.GetCellValue(2, 3).ToString()
                result.ExcelFilePath = excelFileName.Replace(KTB.DNet.Lib.WebConfig.GetValue("SAN").ToString(), "")

                If result.NoPengajuan = String.Empty Then
                    Throw New Exception("No Pengajuan tidak ditemukan , harap cek file excel yang diupload")
                End If

                If result.Perusahaan = String.Empty Then
                    Throw New Exception("Perusahan tidak ditemukan , harap cek file excel yang diupload")
                End If

                Dim arlItem As New ArrayList

                For idx As Integer = 5 To RowCount

                    Dim item As New ESRUTItem
                    Try
                        item.PageNumber = CInt(ws.GetCellValue(idx, 1))
                        item.EngineNumber = ws.GetCellValue(idx, 2)
                        item.ChassisNumber = ws.GetCellValue(idx, 3)
                        item.NomorSRUT = ws.GetCellValue(idx, 4)
                        item.URLQRCode = ws.GetCellValue(idx, 5)
                    Catch ex As Exception
                        Throw New Exception("Terjadi kesalahan dalam memproses file excel, harap cek kembali file anda")
                    End Try
                    ValidateESRUTItem(item, isReupload)

                    item.Status = SetItemStatusAndChassisMasterID(item)

                    arlItem.Add(item)

                Next

                result.ListOfItem = arlItem
            End Using
        End Using


        Return result
    End Function

    Private Sub ValidateESRUTItemWithPdfFile(ByRef esrutHeader As ESRUTHeader, ByVal pdfFileName As String)
        Dim listPdfFile As List(Of PdfDocument) = New List(Of PdfDocument)
        Dim fileExt As String = System.IO.Path.GetExtension(pdfFileName)

        Dim pdfDoc As PdfDocument = PdfReader.Open(pdfFileName, PdfDocumentOpenMode.Import)

        esrutHeader.PdfFilePath = pdfFileName.Replace(KTB.DNet.Lib.WebConfig.GetValue("SAN").ToString(), "")

        If esrutHeader.ListOfItem.Count <> pdfDoc.PageCount Then
            Throw New Exception("Jumlah Row ESRUT tidak sama dengan Jumlah Halaman file PDF")
        End If

        For idx As Integer = 0 To pdfDoc.PageCount - 1
            Dim item As ESRUTItem = CType(esrutHeader.ListOfItem(idx), ESRUTItem)

            Dim outputText As String = ""

            For index As Integer = 0 To pdfDoc.Pages(idx).Contents.Elements.Count - 1
                Dim stream As PdfDictionary.PdfStream = pdfDoc.Pages(idx).Contents.Elements.GetDictionary(index).Stream
                outputText += New PDFParser().ExtractTextFromPDFBytes(stream.Value)
            Next

            item.Status = SetItemStatusByDocument(item, outputText)
        Next
    End Sub



    Private Sub CreateDirectory(ByVal finfo As FileInfo)


        Dim dirInfo As DirectoryInfo = New DirectoryInfo(finfo.DirectoryName)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub

    Private Function ValidateESRUTItem(item As ESRUTItem, ByVal isReupload As Boolean) As Integer
        If item.ChassisNumber = String.Empty Then
            Throw New Exception("Nomor chassis kosong pada baris " & item.PageNumber)
        End If

        If item.EngineNumber = String.Empty Then
            Throw New Exception("Nomor mesin kosong pada baris " & item.PageNumber)
        End If

        If isReupload = False Then
            If ChassisAlreadyUploaded(item.ChassisNumber) = True Then
                Throw New Exception("Nomor chassis " & item.ChassisNumber & " sudah pernah diupload, harap menggunakan fitur reupload")
            End If
        End If

    End Function

    Private Function ChassisAlreadyUploaded(ByVal chassisNumber As String) As Boolean

        Dim criteria As New CriteriaComposite(New Criteria(GetType(ESRUTItem), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(ESRUTItem), "ChassisNumber", MatchType.Exact, chassisNumber))

        Dim arlResult As ArrayList = New ESRUTItemFacade(User).Retrieve(criteria)

        If arlResult.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function SetItemStatusAndChassisMasterID(ByRef item As ESRUTItem) As Short
        Dim chassisMasterData As ChassisMaster = New ChassisMasterFacade(User).Retrieve(item.ChassisNumber)

        If chassisMasterData.ID = 0 Then
            Return EnumESRUT.ESRUTItemStatus.NOT_EXISTS
        Else
            item.ChassisMaster = chassisMasterData
            If chassisMasterData.EngineNumber <> item.EngineNumber Then
                Return EnumESRUT.ESRUTItemStatus.NOT_VALID
            Else
                Return EnumESRUT.ESRUTItemStatus.VALID
            End If
        End If

    End Function

    Private Function SetItemStatusByDocument(item As ESRUTItem, outputText As String) As Short

        If Not outputText.Contains(item.ChassisNumber) Or Not outputText.Contains(item.EngineNumber) Then
            Select Case item.Status
                Case CInt(EnumESRUT.ESRUTItemStatus.VALID)
                    Return EnumESRUT.ESRUTItemStatus.VALID_DOCUMENT_NOT_MATCH
                Case CInt(EnumESRUT.ESRUTItemStatus.NOT_EXISTS)
                    Return EnumESRUT.ESRUTItemStatus.NOT_EXIST_DOCUMENT_NOT_MATCH
                Case CInt(EnumESRUT.ESRUTItemStatus.NOT_VALID)
                    Return EnumESRUT.ESRUTItemStatus.NOT_VALID_DOCUMENT_NOT_MATCH
            End Select
        Else
            Return item.Status
        End If
    End Function

    Private Sub ValidateFile()
        If uplExcel.PostedFile.FileName = String.Empty Then
            Throw New Exception("Harap Upload File Excel VTA")
        End If

        If uplPdf.PostedFile.FileName = String.Empty Then
            Throw New Exception("Harap Upload File Excel VTA")
        End If

    End Sub

    Private Sub btnReupload_Click(sender As Object, e As EventArgs) Handles btnReupload.Click
        Try
            btnProcess.Text = "Harap menunggu..."
            ValidateFile()
            Dim esrutHeader As ESRUTHeader = GetESRUTHeaderData(True)

            Dim iResult As Integer = New ESRUTHeaderFacade(User).Reupload(esrutHeader)

            If iResult > 0 Then
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        Finally
            RestateButton()
        End Try
    End Sub

    Private Function GetMaxFileSizeByAppConfig() As Long
        Dim fileSize As Long
        Try
            Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
            Dim objAppConfig As AppConfig = objAppConfigFacade.Retrieve("ESRUT_MAX_FILE_SIZE")
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
            Dim objAppConfig As AppConfig = objAppConfigFacade.Retrieve("ESRUT_ERROR_MESSAGE")
            errMessage = objAppConfig.Value
        Catch ex As Exception
            errMessage = ""
        End Try

        Return errMessage
    End Function

End Class