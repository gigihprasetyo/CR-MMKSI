Imports System
Imports System.IO
Imports System.Text
Imports System.Security.Principal
Imports System.Web.Security
Imports System.Configuration
Imports System.Web.UI.WebControls

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security

Public Class DownloadWithoutLogIn
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strFileName As String = String.Empty
        Dim Path As String = String.Empty
        strFileName = KTB.DNet.Lib.WebConfig.GetValue("UserManualLogin")

        If strFileName = "" Then
            Response.Write("Error: File Not Found!")
        Else
            Dim finfo As FileInfo = New FileInfo(strFileName)
            Response.ContentType = "application/x-download"
            Response.AddHeader("Content-Disposition", "attachment;filename=""" & finfo.Name & """")
            Dim filePath As String
            If Left(strFileName, 2) = "\\" Then
                filePath = strFileName
            Else
                filePath = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\" & strFileName
            End If
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Try
                If imp.Start() Then
                    Response.WriteFile(filePath)
                    Response.End()
                    imp.StopImpersonate()
                    imp = Nothing
                Else
                    MessageBox.Show("File tidak ditemukan.")
                    Response.End()
                End If
            Catch ex As Exception
                Response.End()
            End Try

        End If
    End Sub

End Class