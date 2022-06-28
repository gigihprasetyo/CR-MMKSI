Imports System.IO
Imports System.Text
Imports KTB.DNet.Utility
Imports System.Configuration

Public Class frmUELA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnApprove As System.Web.UI.WebControls.Button
    Protected WithEvents btnDisapprove As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private stream As StreamReader
    Private sb As StringBuilder
    'Private _text As String
    'Public ReadOnly Property GetText() As String
    '    Get
    '        Return _text
    '    End Get
    'End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Dim fileName As String = Server.MapPath("") & "\" & "EULA.txt"
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim fileName As String = KTB.DNet.Lib.WebConfig.GetValue("EULAPATH") & "EULA.txt"

        If Not IsNothing(Request.Item("type")) Then
            If Request.Item("type") = "0" Then
            Else 'DSF
                fileName = KTB.DNet.Lib.WebConfig.GetValue("EULAPATH") & "EULA_DSF.txt"
            End If

        End If
        Dim sb As StringBuilder = LoadFile(fileName)

        Label1.Text = sb.ToString()
        '_text = sb.ToString()
        'txtEULA.Text = sb.ToString
        'EULAArea.Value = sb.ToString
        'Response.Write(sb.ToString())
    End Sub

    Protected Function NextLine(ByVal stream As StreamReader)
        Dim stemp As Integer = stream.Read
        Dim sReturn = ""
        While (Not (stemp = -1) And (Not stemp = 13)) 'char 10 = /n
            Dim str As String = stemp.ToString
            sReturn += ChrW(stemp)
            stemp = stream.Read
        End While
        Dim strx As String = sReturn.ToString.Trim
        strx = strx.Replace("""", "''")
        Return strx
    End Function


    Private Function LoadFile(ByVal fileName As String) As StringBuilder
        sb = New StringBuilder
        Dim fileNamex As String = fileName ' KTB.DNet.Lib.WebConfig.GetValue("EULAPATH") & "EULA.txt"
        Try
            Dim val As String
            stream = New StreamReader(fileNamex, True)
            val = NextLine(stream).Trim()
            While (Not val = "</EULA>")
                If Not val = "<EULA>" Then
                    sb.Append(val)
                End If
                val = NextLine(stream)
            End While
        Catch ex As Exception
            MessageBox.Show("Gagal Loading File EULA ")
        Finally
            If Not stream Is Nothing Then
                stream.Close()
                stream = Nothing
            End If
        End Try
        Return sb

    End Function

    Private Sub btnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Response.Redirect("FrmDNETSurvey.aspx?type=" & Request.QueryString("type").ToString())


        'Response.Redirect("default_general.aspx?type=" & Request.QueryString("type").ToString())
    End Sub

    Private Sub btnDisapprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisapprove.Click
        Response.Redirect("Login.aspx")
    End Sub
End Class
