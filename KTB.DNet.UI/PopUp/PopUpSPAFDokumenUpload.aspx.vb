Imports System.IO

Imports KTB.DNet.Utility

Public Class PopUpSPAFDokumenUpload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents fileUploadSPAFDoc As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents pnlRunCloseWindow As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private ReadOnly Property QueryStringID() As String
        Get
            Return Request.QueryString("ID")
        End Get
    End Property

    Private Function GetDirUpload() As DirectoryInfo
        Return New DirectoryInfo(String.Format("{0}{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
            KTB.DNet.Lib.WebConfig.GetValue("DaftarDokumenDestFileDirectory"), QueryStringID))
    End Function

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim DirUpload As DirectoryInfo = GetDirUpload()
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        If imp.Start Then
            If Not DirUpload.Exists Then
                DirUpload.Create()
            End If
            If Not fileUploadSPAFDoc.PostedFile Is Nothing Then
                If fileUploadSPAFDoc.PostedFile.ContentType.ToLower = "text/plain" OrElse _
                    fileUploadSPAFDoc.PostedFile.ContentType.ToLower = "text/csv" OrElse _
                    fileUploadSPAFDoc.PostedFile.ContentType.ToLower = "application/msword" OrElse _
                    fileUploadSPAFDoc.PostedFile.ContentType.ToLower = "application/vnd.ms-excel" OrElse _
                    fileUploadSPAFDoc.PostedFile.ContentType.ToLower = "application/zip" OrElse _
                    fileUploadSPAFDoc.PostedFile.ContentType.ToLower = "application/pdf" Then
                    Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
                    If fileUploadSPAFDoc.PostedFile.ContentLength < maxFileSize Then
                        Dim fileUpload As New FileInfo(String.Format("{0}\{1}", _
                            DirUpload.FullName, Path.GetFileName(fileUploadSPAFDoc.PostedFile.FileName)))
                        Try
                            Dim objUpload As New UploadToWebServer
                            objUpload.Upload(fileUploadSPAFDoc.PostedFile.InputStream, fileUpload.FullName)
                            pnlRunCloseWindow.Visible = True
                            imp.StopImpersonate()
                            imp = Nothing
                        Catch ex As Exception
                            MessageBox.Show("Upload file tidak berhasil")
                        End Try
                    Else
                        MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    End If
                Else
                    MessageBox.Show("Tipe dokumen tidak diperbolehkan. Hanya dokumen text, csv, msword, msexcel, zip dan pdf yang boleh diupload")
                End If
            Else
                MessageBox.Show("Masukkan nama file")
            End If
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
    End Sub
End Class
