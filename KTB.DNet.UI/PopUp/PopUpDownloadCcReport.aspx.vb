Imports System.IO
Imports KTB.DNet.Utility
Imports System.Configuration

Public Class PopUpDownloadCcReport
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
        strFileName = Request.QueryString("file")
        If strFileName = "" Then
            Response.Write("Error: File Not Found!")
        Else
            Dim finfo As FileInfo = New FileInfo(strFileName)
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/x-download"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & finfo.Name)
            Dim filePath As String
            If Left(strFileName, 2) = "\\" Then
                filePath = strFileName
            Else
                filePath = KTB.DNet.Lib.WebConfig.GetValue("CC_REPORT") & "\" & strFileName
            End If
            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("CC_User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("CC_Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("CC_Server")
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
                imp.StopImpersonate()
                imp = Nothing
            Catch ex As Exception
                'Response.WriteFile("File tidak ditemukan.")
                'MessageBox.Show("File tidak ditemukan.")
                Response.End()
            End Try

            'If (Request.QueryString("source") <> String.Empty) Then
            '    'Response.Redirect(Request.QueryString("source"))
            '    Response.Redirect("http://localhost/ktb.dnet/Service/frmMonthlyDocument.aspx")
            'Else

            'End If
        End If 'Put user code to initialize the page here
    End Sub

End Class
