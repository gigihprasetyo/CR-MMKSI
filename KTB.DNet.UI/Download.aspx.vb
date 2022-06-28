Imports System.IO
Imports KTB.DNet.Utility
Imports System.Configuration
 
Public Class Download
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strFileName As String
        ' Retrieve the filename to download
        strFileName = Request.QueryString("file")
        '' Check for directory traversal issues.
        'If InStr(1, strFileName, "\", 1) <> 0 Then strFileName = ""
        'If InStr(1, strFileName, "/", 1) <> 0 Then strFileName = ""
        'If InStr(1, strFileName, "..", 1) <> 0 Then strFileName = ""

        ' You'll probably want to add additional safeguards as to what
        ' files people can download.  Since this is our sample area,
        ' everything is fair game, but please note that this file will
        ' send users your "web.config" file, Access database files, and
        ' lots of other goodies you may not really want to share... so
        ' PLEASE BE CAREFUL!!!

        ' Since we're doing this for illustration, we want users to get
        ' something and not just an error if they didn't pass in a filename.
        'If strFileName = "" Then strFileName = "DataFile/price.exe"

        If strFileName = "" Then
            Response.Write("Error: File Not Found!")
        Else
            Dim finfo As FileInfo = New FileInfo(strFileName)

            Dim fileDisplayName As String

            If String.IsNullOrEmpty(Request.QueryString("name")) Then
                fileDisplayName = finfo.Name
            Else
                fileDisplayName = Request.QueryString("name") & finfo.Extension
            End If

            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/x-download"
            'Response.AddHeader("Content-Disposition", "attachment;filename=" & finfo.Name)
            Response.AddHeader("Content-Disposition", "attachment;filename=""" & fileDisplayName & """")
            ' If we needed to edit the file at all we could read it using something
            ' like the GetTextFromFile function in our view source sample:
            ' http://aspnet.asp101.com/samples/source.aspx
            ' Here we'll just be reading it and writing it back out so
            ' Response.WriteFile is easier and faster.
            ' Writes the specified file directly to an HTTP content output stream.
            'TO DO
            'Harus di ganti dengan Path yang fix ngga boleh pake server map path

            'Dim filePath As String = Server.MapPath("") & "\" & strFileName
            Dim filePath As String
            If Left(strFileName, 2) = "\\" Then
                filePath = strFileName
            Else
                filePath = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\" & strFileName
            End If
            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Try
                If imp.Start() Then
                    Response.WriteFile(filePath)
                    Response.End()
                Else
                    'Response.WriteFile("File tidak ditemukan.")
                    MessageBox.Show("File tidak ditemukan.")
                    Response.End()
                End If
            Catch ex As Exception
                'Response.WriteFile("File tidak ditemukan.")
                'MessageBox.Show("File tidak ditemukan.")
                Response.End()
            End Try
            imp.StopImpersonate()
            imp = Nothing
            'If (Request.QueryString("source") <> String.Empty) Then
            '    'Response.Redirect(Request.QueryString("source"))
            '    Response.Redirect("http://localhost/ktb.dnet/Service/frmMonthlyDocument.aspx")
            'Else

            'End If
        End If 'Put user code to initialize the page here
    End Sub

End Class
