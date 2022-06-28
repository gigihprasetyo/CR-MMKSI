
Imports System.IO
Imports KTB.DNet.Utility
Public Class PopUpDownloadEstimate
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbtnSDGroup01 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnSDGroup02 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnSDGroup03 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnSDGroup04 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnSDGroup05 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button


    Protected WithEvents lbtnSDGroup06 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnSDGroup07 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnSDGroup08 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnSDGroup09 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnSDGroup10 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("term2")) Then
                ViewState("vsTerm2") = Request.QueryString("term2")
                If Not IsNothing(Request.QueryString("DealerCode")) Then
                    ViewState("DealerCode") = Request.QueryString("DealerCode")

                    lblDealerCode.Text = " - " & ViewState("DealerCode").ToString()
                End If

            Else
                ViewState("vsTerm2") = "JKT2"
            End If
        End If
    End Sub


    Private Function DownLoadFile(ByVal fileName As String, ByVal dealerTerm2 As String) As Integer
        Dim nReturn As Int16 = 0
        Dim fileInfo0 As FileInfo
        'Dim fileInfo1 As New FileInfo(Server.MapPath(""))
        Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
        Dim destFilePath As String
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim success As Boolean = False
        Dim newFileInfo As FileInfo

        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, KTB.DNet.Lib.WebConfig.GetValue("WebServer"))
        Dim impCreate As SAPImpersonate = New SAPImpersonate(_user, _password, KTB.DNet.Lib.WebConfig.GetValue("WebServer"))

        fileInfo0 = New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SPFileDirectory").ToString & "\" & dealerTerm2.Trim & "\" & fileName.ToUpper.Trim)
        destFilePath = fileInfo1.Directory.FullName & "\" & "DataFile\SP\" & dealerTerm2.Trim & "\" & fileName.ToUpper.Trim
        newFileInfo = New FileInfo(destFilePath)
        Try
            If impCreate.Start Then
                If Not newFileInfo.Directory.Exists Then
                    newFileInfo.Directory.Create()
                End If
                newFileInfo = New FileInfo(destFilePath)

                If Not newFileInfo.Directory.Exists Then
                    newFileInfo.Directory.Create()
                End If
                impCreate.StopImpersonate()
                impCreate = Nothing
            End If
        Catch ex As Exception

        End Try

        success = False
        Try
            success = imp.Start()
            If success Then
                If (fileInfo0.Exists) Then
                    fileInfo0.CopyTo(destFilePath, True)
                    imp.StopImpersonate()
                    imp = Nothing
                    Response.Redirect("../Download.aspx?file=DataFile\SP\" & dealerTerm2.Trim & "\" & fileName.ToUpper.Trim)
                Else

                    nReturn = -1 'MessageBox.Show("SDGROUP0" & i.ToString & ".DLR:  File tidak ada")
                End If
            End If
        Catch ex As Exception
            'MessageBox.Show("Gagal Download File.")
        End Try
        Return nReturn



    End Function

    Private Sub lbtnSDGroup01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSDGroup01.Click
        If DownLoadFile("SDGROUP01.DLR", CType(ViewState("vsTerm2"), String)) = -1 Then
            MessageBox.Show(SR.FileNotFound("SDGROUP01.DLR"))
        End If
    End Sub

    Private Sub lbtnSDGroup02_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSDGroup02.Click
        If DownLoadFile("SDGROUP02.DLR", CType(ViewState("vsTerm2"), String)) = -1 Then
            MessageBox.Show(SR.FileNotFound("SDGROUP02.DLR"))
        End If
    End Sub

    Private Sub lbtnSDGroup03_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSDGroup03.Click
        If DownLoadFile("SDGROUP03.DLR", CType(ViewState("vsTerm2"), String)) = -1 Then
            MessageBox.Show(SR.FileNotFound("SDGROUP03.DLR"))
        End If
    End Sub

    Private Sub lbtnSDGroup04_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSDGroup04.Click
        If DownLoadFile("SDGROUP04.DLR", CType(ViewState("vsTerm2"), String)) = -1 Then
            MessageBox.Show(SR.FileNotFound("SDGROUP04.DLR"))
        End If
    End Sub

    Private Sub lbtnSDGroup05_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSDGroup05.Click
        If DownLoadFile("SDGROUP05.DLR", CType(ViewState("vsTerm2"), String)) = -1 Then
            MessageBox.Show(SR.FileNotFound("SDGROUP05.DLR"))
        End If
    End Sub


    Private Sub lbtnSDGroup06_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSDGroup06.Click
        If DownLoadFile("SDGROUP06.DLR", CType(ViewState("vsTerm2"), String)) = -1 Then
            MessageBox.Show(SR.FileNotFound("SDGROUP06.DLR"))
        End If
    End Sub

    Private Sub lbtnSDGroup07_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSDGroup07.Click
        If DownLoadFile("SDGROUP07.DLR", CType(ViewState("vsTerm2"), String)) = -1 Then
            MessageBox.Show(SR.FileNotFound("SDGROUP07.DLR"))
        End If
    End Sub


    Private Sub lbtnSDGroup08_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSDGroup08.Click
        If DownLoadFile("SDGROUP08.DLR", CType(ViewState("vsTerm2"), String)) = -1 Then
            MessageBox.Show(SR.FileNotFound("SDGROUP08.DLR"))
        End If
    End Sub

    Private Sub lbtnSDGroup09_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSDGroup09.Click
        If DownLoadFile("SDGROUP09.DLR", CType(ViewState("vsTerm2"), String)) = -1 Then
            MessageBox.Show(SR.FileNotFound("SDGROUP09.DLR"))
        End If
    End Sub

    Private Sub lbtnSDGroup10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSDGroup10.Click
        If DownLoadFile("SDGROUP10.DLR", CType(ViewState("vsTerm2"), String)) = -1 Then
            MessageBox.Show(SR.FileNotFound("SDGROUP10.DLR"))
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("../Sparepart/FrmPurchaseOrderEstimate.aspx")
    End Sub
End Class
