Imports System.IO

Public Class DownloadLocal
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
        Dim finfo As FileInfo = New FileInfo(strFileName)

        If strFileName = "" Then
            Response.Write("Error: File Not Found!")
        Else
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/x-download"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & finfo.Name)
            
            Dim filePath As String = Server.MapPath("") & "\" & strFileName
            Dim newfinfo As FileInfo = New FileInfo(filePath)
            If newfinfo.Exists Then
                Response.WriteFile(filePath)
                Response.End()
            Else
                Response.WriteFile("File tidak ditemukan")
                Response.End()
            End If
        End If
    End Sub

End Class
