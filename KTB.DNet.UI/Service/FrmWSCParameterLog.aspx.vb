Imports System.IO

Public Class FrmWSCParameterLog
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim filename = String.Format("{0}", "ParameterError.txt")
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename
        Try
            Dim sReader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(LocalDest)
            Do While sReader.Peek() >= 0
                lbLog.Items.Add(sReader.ReadLine())
            Loop
            sReader.Close()
        Catch
        End Try
    End Sub

End Class