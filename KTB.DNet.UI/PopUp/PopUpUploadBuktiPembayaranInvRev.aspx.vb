Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports System.IO
Imports KTB.DNet.Utility

Public Class PopUpUploadBuktiPembayaranInvRev
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblRegNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblFileUpload As System.Web.UI.WebControls.Label
    Protected WithEvents lblEvidencePath As System.Web.UI.WebControls.Label
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label
    Protected WithEvents UploadFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnTutup As System.Web.UI.WebControls.Button
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

    Dim objRevPaymentHdr As RevisionPaymentHeader

    Private ReadOnly Property QueryStringID() As String
        Get
            Return Request.QueryString("ID")
        End Get
    End Property

    Private Function GetDirUpload() As DirectoryInfo
        Return New DirectoryInfo(String.Format("{0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("FinishUnitFileDirectory"), _
            KTB.DNet.Lib.WebConfig.GetValue("InvoiceDestFileDirectory"), lblRegNumber.Text.Trim))
    End Function

    Function GetEvidencePath() As String
        Return String.Format("{0}\{1}\{2}", KTB.DNet.Lib.WebConfig.GetValue("InvoiceDestFileDirectory"), lblRegNumber.Text.Trim, Path.GetFileName(UploadFile.PostedFile.FileName))
    End Function

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        objRevPaymentHdr = New RevisionPaymentHeaderFacade(User).Retrieve(Integer.Parse(Request.QueryString("ID")))

        Dim DirUpload As DirectoryInfo = GetDirUpload()
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        If imp.Start Then
            If Not DirUpload.Exists Then
                DirUpload.Create()
            End If
            If (Not UploadFile.PostedFile Is Nothing) And UploadFile.PostedFile.ContentLength > 0 Then
                If UploadFile.PostedFile.ContentType.ToLower = "text/plain" OrElse _
                    UploadFile.PostedFile.ContentType.ToLower = "text/csv" OrElse _
                    UploadFile.PostedFile.ContentType.ToLower = "application/msword" OrElse _
                    UploadFile.PostedFile.ContentType.ToLower = "application/vnd.ms-excel" OrElse _
                    UploadFile.PostedFile.ContentType.ToLower = "application/zip" OrElse _
                    UploadFile.PostedFile.ContentType.ToLower = "application/pdf" Then
                    Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
                    If UploadFile.PostedFile.ContentLength < maxFileSize Then
                        Dim fileUpload As New FileInfo(String.Format("{0}\{1}", DirUpload.FullName, Path.GetFileName(UploadFile.PostedFile.FileName)))
                        Try
                            objRevPaymentHdr.EvidencePath = GetEvidencePath()
                            If New RevisionPaymentHeaderFacade(User).Update(objRevPaymentHdr) <> -1 Then
                                Dim objUpload As New UploadToWebServer
                                objUpload.Upload(UploadFile.PostedFile.InputStream, fileUpload.FullName)
                                pnlRunCloseWindow.Visible = True
                            Else
                                MessageBox.Show("Upload file tidak berhasil")
                            End If
                        Catch ex As Exception
                            MessageBox.Show("Upload file tidak berhasil")
                        End Try
                    Else
                        MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    End If
                Else
                    MessageBox.Show("Tipe dokumen ini tidak diperbolehkan. Hanya dokumen text, csv, msword, msexcel, zip dan pdf yang boleh diupload")
                End If
            Else
                MessageBox.Show("Upload file belum diisi\n")
            End If
            imp.StopImpersonate()
            imp = Nothing
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            objRevPaymentHdr = New RevisionPaymentHeaderFacade(User).Retrieve(Integer.Parse(Request.QueryString("ID")))
            lblRegNumber.Text = objRevPaymentHdr.RegNumber
            Dim strEvidencePath As String = objRevPaymentHdr.EvidencePath
            Dim lastItem As String = "[...]"
            If strEvidencePath <> "" Then
                Dim strList() As String = strEvidencePath.Split("\")
                lastItem = "[ " & strList(strList.Length() - 1) & " ]"
            End If
            lblFileName.Text = lastItem
        End If
    End Sub
End Class
