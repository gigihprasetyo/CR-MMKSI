#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Lib

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.UI.Helper
Imports System.IO
Imports System.Text

#End Region



Public Class FrmChecklistMaintenance
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSMSNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSMSMessage As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSMSSend As System.Web.UI.WebControls.Button
    Protected WithEvents txtEmailAddress As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmailMessage As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnEmailSend As System.Web.UI.WebControls.Button
    Protected WithEvents txtServerPath As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtFileName As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlMain As System.Web.UI.WebControls.Panel

    Protected WithEvents txtSAN As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUpload As System.Web.UI.WebControls.Button
    Protected WithEvents txtFileSap As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLog As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLogName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDownloadLog As System.Web.UI.WebControls.Button


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    'Private _SalesmanAreaFacade As New SalesmanAreaFacade(User)
    'Private _DealerFacade As New DealerFacade(User)
    'Private _editPriv As Boolean = False
    Private sessHelper As New SessionHelper
    'Private objDealer As Dealer

#End Region

#Region "Custom Methods"

    Private Sub Download()
        Dim sFileName As String
        Dim dFileName As String
        Dim NewFileName As String
        sFileName = txtServerPath.Text & txtFileName.Text

        If txtFileName.Text.EndsWith(".txt") Then
            NewFileName = txtFileName.Text.Substring(0, txtFileName.Text.Length - 4) & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".txt"
        Else
            NewFileName = txtFileName.Text.Trim & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & IIf(txtFileName.Text.Length > 4, Right(txtFileName.Text, 4), "")
        End If

        dFileName = Server.MapPath("./../DataTemp") & "\" & NewFileName.Replace(" ", "")

        Dim _user As String
        _user = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String
        _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String
        _webServer = KTB.DNet.Lib.WebConfig.GetValue("Checklist.Server")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then
                Dim sFinfo As FileInfo = New FileInfo(sFileName)
                Dim dFInfo As FileInfo = New FileInfo(dFileName)
                If dFInfo.Exists Then
                    dFInfo.Delete()
                End If
                dFInfo = Nothing
                dFInfo = sFinfo.CopyTo(dFileName)
                'IO.File.Copy(sFinfo.FullName, dFInfo.FullName, True)

                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & NewFileName.Replace(" ", ""))
        Catch ex As Exception
            Dim dummy As Integer = 0
            MessageBox.Show("Download data gagal. " & ex.Message)
        End Try
    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CheckPrivilege()

        If Not IsPostBack Then
            txtSMSNumber.Text = sessHelper.GetSession("Checklist.SMSNumber") ' KTB.DNet.Lib.WebConfig.GetValue("Checklist.SMSNumber") 
            txtSMSMessage.Text = KTB.DNet.Lib.WebConfig.GetValue("Checklist.SMSMessage")
            txtEmailAddress.Text = sessHelper.GetSession("Checklist.EmailAddress") '  KTB.DNet.Lib.WebConfig.GetValue("Checklist.EmailAddress")
            txtEmailMessage.Text = KTB.DNet.Lib.WebConfig.GetValue("Checklist.EmailMessage")
            txtServerPath.Text = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("AnnouncementPath") & "\" 'KTB.DNet.Lib.WebConfig.GetValue("Checklist.ServerPath")
            txtFileName.Text = KTB.DNet.Lib.WebConfig.GetValue("Checklist.FileName")

            txtSAN.Text = ConfigurationSettings.AppSettings.Item("SAPServerFolder")
            txtFileSap.Text = "DNETTesting.txt"
            txtLog.Text = ConfigurationSettings.AppSettings.Item("WSMTesting")
            txtLogName.Text = ConfigurationSettings.AppSettings.Item("WSMTestingFile")
        End If
    End Sub


    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Download()
    End Sub
    Private Sub btnSMSSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMSSend.Click
        Dim objSMS As KTB.DNet.Lib.Sms = New KTB.DNet.Lib.Sms
        If objSMS.IsSMSGatewayLive Then

            Dim otpfunc As New OTPFunction

            otpfunc.SendSMSNotif(txtSMSNumber.Text, txtSMSMessage.Text)

            If otpfunc.boolReturn Then
                MessageBox.Show("Send SMS Berhasil")
            Else
                MessageBox.Show("Send SMS gagal")
            End If
        Else
            MessageBox.Show("SMS Server tidak aktif")
            Exit Sub
        End If

    End Sub

    Private Sub btnEmailSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEmailSend.Click
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailTo As String = KTB.DNet.Lib.WebConfig.GetValue("EmailAdminDNET")
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")

        'ObjEmail.sendMail(emailTo, "", emailFrom, "KTB-DNET", Mail.MailFormat.Html, contentEmail)
        ObjEmail.sendMail(txtEmailAddress.Text, "", emailFrom, "[MMKSI-DNet]", Mail.MailFormat.Html, txtEmailMessage.Text)
    End Sub
#End Region


#Region "Privilege"

    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ProfileListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Dealer - Daftar Profile")
        End If
    End Sub

    'Private Sub CheckPrivDealerOnly()
    '    If Not SecurityProvider.Authorize(context.User, SR.ProfileListCreate_Privilege) Then
    '        Server.Transfer("../FrmAccessDenied.aspx?modulName=Dealer - Daftar Profile")
    '    End If
    'End Sub

#End Region

    Protected Sub txtUpload_Click(sender As Object, e As EventArgs) Handles txtUpload.Click

        Try
            If txtSAN.Text.Trim().Length = 0 OrElse Not txtSAN.Text.Substring(0, 2).Equals("\\") Then
                MessageBox.Show("Invalid Format")
                Return
            End If

            Dim fileName As String = Server.MapPath("") & "\..\" & ConfigurationSettings.AppSettings.Get("SAPTesting")   ' txtFileSap.Text.Trim()
            Dim Fi As FileInfo = New FileInfo(fileName)
            Dim FileNameDest As String = txtSAN.Text.Trim() & "\" & txtFileSap.Text.Trim()

            If Fi.Exists Then
                Dim _user As String = ConfigurationSettings.AppSettings.Get("User")
                Dim _password As String = ConfigurationSettings.AppSettings.Get("Password")
                Dim strServer = txtSAN.Text.Split("\")
                Dim server As String = strServer(2)
              
                Dim _webServer As String = server 'ConfigurationSettings.AppSettings.Get("SAPServer") '172.17.104.204

                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                If imp.Start() Then
                    Dim finfo As FileInfo = New FileInfo(FileNameDest)
                    If finfo.Exists Then
                        finfo.Delete()  '-- Delete temp file if exists
                    End If

                    Fi.CopyTo(FileNameDest, True)

                    imp.StopImpersonate()
                    imp = Nothing
                    MessageBox.Show("Succes ")
                Else
                    MessageBox.Show("Gagal Impersonate : ")

                End If
            Else
                Throw New Exception("File Upload Tidak Ada")

            End If

        Catch ex As Exception
            MessageBox.Show("Failed : " & ex.Message)
        End Try
    End Sub

    Protected Sub btnDownloadLog_Click(sender As Object, e As EventArgs) Handles btnDownloadLog.Click
        Dim sFileName As String
        Dim dFileName As String
        Dim NewFileName As String
        sFileName = txtLog.Text & txtLogName.Text

        If txtLog.Text.Trim().Length = 0 OrElse Not txtLog.Text.Substring(0, 2).Equals("\\") Then
            MessageBox.Show("Invalid Format")
            Return
        End If

        Dim strServer = txtSAN.Text.Split("\")
        Dim searver As String = strServer(2)

        If txtLogName.Text.EndsWith(".txt") Then
            NewFileName = txtLogName.Text.Substring(0, txtFileName.Text.Length - 4) & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".txt"
        Else
            NewFileName = txtLogName.Text.Trim & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & IIf(txtLogName.Text.Length > 4, Right(txtLogName.Text, 4), "")
        End If

        dFileName = Server.MapPath("./../DataTemp") & "\" & NewFileName

        Dim _user As String
        _user = ConfigurationSettings.AppSettings.Get("User")
        Dim _password As String
        _password = ConfigurationSettings.AppSettings.Get("Password")
        Dim _webServer As String
        _webServer = searver 'ConfigurationSettings.AppSettings.Get("SAPServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then
                Dim sFinfo As FileInfo = New FileInfo(sFileName)
                If Not sFinfo.Exists Then
                    imp.StopImpersonate()
                    imp = Nothing
                    Throw New Exception("File Log is not Exist")
                End If
                Dim dFInfo As FileInfo = New FileInfo(dFileName)
                If dFInfo.Exists Then
                    dFInfo.Delete()
                End If
                dFInfo = Nothing
                dFInfo = sFinfo.CopyTo(dFileName)
                'IO.File.Copy(sFinfo.FullName, dFInfo.FullName, True)

                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & NewFileName)
        Catch ex As Exception
            Dim dummy As Integer = 0
            MessageBox.Show("Download data gagal. " & ex.Message)
        End Try
    End Sub
End Class
