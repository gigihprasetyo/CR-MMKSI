Imports System.Text
Imports System.IO
Imports System.Reflection


#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
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
Imports GlobalExtensions
#End Region

Public Class FrmGlobalUpload
    Inherits System.Web.UI.Page

    Dim helpers As TrainingHelpers = New TrainingHelpers(Me.Page)
    Private _uploadProfile As GlobalUpload
    Private _uploadFacade As GlobalUploadFacade

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            InitObject()
            If Not Page.IsPostBack Then
                InitForm()
            End If
        Catch ex As Exception
            If Not ex.Message.StartsWith("Thread was") Then
                MessageBox.Show(ex.Message)
            End If
        End Try

    End Sub

    Private Sub InitObject()
        _uploadFacade = New GlobalUploadFacade(User)
        _uploadProfile = GetUploadProfile()
        lblTitle.Text = "Upload - " & _uploadProfile.DisplayName
        helpers = New TrainingHelpers(Me.Page, lblTitle.Text)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, _uploadProfile.PrivilegeName)
        helpers.Privilage()
    End Sub

    Private Sub InitForm()
        lblDisplayName.Text = _uploadProfile.DisplayName
    End Sub

    Private Sub lnkDownload_Click(sender As Object, e As EventArgs) Handles lnkDownload.Click
        Try
            If _uploadProfile.FacadeName.ToLower().Contains("parser") Then
                DownloadTemplateFromParser()
            Else
                DownloadTemplateFromFacade()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DownloadTemplateFromParser()
        Dim parserType As Type = GetParserType()
        Dim parser As Object = GetParser()
        Dim downloadMethod As MethodInfo = parserType.GetMethod(_uploadProfile.DownloadMethodName)
        Dim downloadValue As Object = downloadMethod.Invoke(parser, New Object() {})
    End Sub

    Private Function GetParserType() As Type
        Dim parserClassName As String = _uploadProfile.FacadeName & ",KTB.DNet.Parser"
        Dim parserType As Type = Type.GetType(parserClassName)

        Return parserType
    End Function

    Private Function GetParser() As Object

        Dim parserType As Type = GetParserType()

        Dim types As Type() = New Type(1) {}
        types(0) = GetType(System.Security.Principal.IPrincipal)
        types(1) = GetType(System.Web.UI.Page)

        Dim parserConstructor As ConstructorInfo = parserType.GetConstructor(types)
        Dim parser As Object = parserConstructor.Invoke(New Object() {User, Me.Page})

        Return parser
    End Function

    Private Sub DownloadTemplateFromFacade()
        Dim dtTemplate As DataTable = _uploadFacade.GetTemplateExcel(_uploadProfile)
        Dim template As ExcelTemplate = New ExcelTemplate(Me.Page)
        template.FileName = _uploadProfile.DownloadFileName & ".xls"
        template.SheetName = "Upload Template"
        template.Judul = _uploadProfile.DownloadFileName

        Dim dataRow As List(Of ExcelTemplateColumn) = New List(Of ExcelTemplateColumn)

        For i As Integer = 1 To dtTemplate.Columns.Count
            template.AddField(i, dtTemplate.Columns(i - 1).ColumnName)
        Next

        For j As Integer = 0 To dtTemplate.Rows.Count - 1
            Dim arrValue As New ArrayList
            For k As Integer = 0 To dtTemplate.Columns.Count - 1
                arrValue.Add(dtTemplate.Rows(j)(k).ToString())
            Next
            template.AddRow(arrValue)
        Next

        template.DownLoad()
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            If fileUpload.PostedFile.FileName = String.Empty Then
                Throw New Exception("Tidak ada file yang diupload")
            End If

            Dim fileName As String = UploadAndGetFileName(_uploadProfile)

            Dim result As Object = ProcessData(_uploadProfile, fileName)

            Dim errorList As List(Of ErrorExcelUpload) = CType(result, List(Of ErrorExcelUpload))

            If errorList.Count = 0 Then
                MessageBox.Show(SR.UploadSucces(fileUpload.PostedFile.FileName))
            Else
                helpers.SetSession("namaFile", fileUpload.PostedFile.FileName)
                helpers.SetSession("dataError", errorList)
                Me.Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "document.getElementById('btnShowPopup').click();", True)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function UploadAndGetFileName(ByVal uploadProfile As GlobalUpload) As String

        Dim fileInfo As FileInfo
        Dim fileName As String = String.Empty
        'Dim listTrClass As List(Of TrClass) = New List(Of TrClass)
        'Dim listError As List(Of ErrorExcelUpload) = New List(Of ErrorExcelUpload)
        Dim resultUpload As String = helpers.UploadFile(fileUpload, KTB.DNet.Lib.WebConfig.GetValue("GlobalUpload"), uploadProfile.MaxFileSize)
        Dim errArr() As String = resultUpload.Split("|")
        If errArr(0).Equals("Error") Then
            Throw New Exception(errArr(1))
        Else
            fileName = KTB.DNet.Lib.WebConfig.GetValue("SAN") & errArr(1)
        End If

        Return fileName

    End Function

    Private Function GetUploadProfile() As GlobalUpload
        Dim code As String = Page.Request.QueryString("code")
        Dim result As GlobalUpload = _uploadFacade.Retrieve(code)

        If result.ID = 0 Then
            Throw New Exception("Tipe Upload tidak ditemukan")
        End If

        Return result
    End Function

    Private Function ProcessData(ByVal uploadProfile As GlobalUpload, ByVal fileName As String) As Object

        Dim parserClass As Type = Type.GetType(uploadProfile.ParserName & ",KTB.DNet.Parser")
        Dim types As Type() = New Type(1) {}
        types(0) = GetType(System.Security.Principal.IPrincipal)
        types(1) = GetType(System.Web.UI.Page)

        Dim parserConstructor As ConstructorInfo = parserClass.GetConstructor(types)
        Dim parser As AbstractExcelParser = parserConstructor.Invoke(New Object() {User, Me.Page})

        Dim processResult As Object = parser.ParseExcelNoTransaction(fileName, String.Empty, User.Identity.Name)

        Return processResult

    End Function

End Class