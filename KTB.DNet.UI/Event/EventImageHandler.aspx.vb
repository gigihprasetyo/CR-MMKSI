Imports System.IO
Imports System.Text
Imports KTB.DNet.Utility
Imports System.Configuration


Public Class EventImageHandler
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
        Dim strFileName As String = Request.QueryString("file")
        If strFileName = "" Then
            Response.Write("Error: File Not Found!")
        Else
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim _success As Boolean = False
            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\" & strFileName
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Try
                _success = imp.Start()
                If _success Then
                    Dim finfo As FileInfo = New FileInfo(DestFile)
                    If finfo.Exists() Then
                        Dim fs As FileStream = finfo.OpenRead()
                        Dim lBytes As Long = fs.Length
                        If (lBytes > 0) Then
                            Dim fileData(lBytes - 1) As Byte

                            ' Read the file into a byte array
                            fs.Read(fileData, 0, lBytes)
                            fs.Close()

                            Response.ContentType = "Image/jpeg"
                            Response.BinaryWrite(fileData)
                            Response.End()
                        Else
                            Response.Write("File tidak ditemukan")
                            Response.End()
                        End If
                    Else
                        Response.Write("File tidak ditemukan")
                        Response.End()
                    End If
                Else
                    Response.Write("File tidak ditemukan")
                    Response.End()
                End If
            Catch ex As Exception
                imp.StopImpersonate()
                Response.Write("File tidak ditemukan")
                Response.End()
            End Try
            imp.StopImpersonate()
        End If

    End Sub

End Class
