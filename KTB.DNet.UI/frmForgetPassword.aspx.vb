#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Text
Imports System.Security.Principal
Imports System.Web.Security
Imports System.Configuration
Imports System.Configuration.ConfigurationSettings
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Lib

Imports Microsoft.Practices.EnterpriseLibrary.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security
Imports Microsoft.Practices.EnterpriseLibrary.Security.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database.Authentication.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database
Imports Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
#End Region

Public Class frmForgetPassword
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents HyperLink1 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblQuesFavorit As System.Web.UI.WebControls.Label
    Protected WithEvents txtAnsFavorit As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnVerify As System.Web.UI.WebControls.Button
    Protected WithEvents chkHandPhone As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkEmail As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblUserID As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents imgPhisingGuard As System.Web.UI.WebControls.Image

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable"
    Private sessHelp As SessionHelper = New SessionHelper
    Private objUserInfo As UserInfo
    Private identity As IIdentity
    Private pricipal As IPrincipal
    Private confContext As ConfigurationContext
    Private dbAuthenticationProvider As DbAuthenticationProviderData
    Private userRoleMgr As UserRoleManager
    Private hashProvider As IHashProvider
    Private ObjEmail As DNetMail
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            txtAnsFavorit.Text = ""
            sessHelp.SetSession("LOGINCOUNT", 0)  '-- Init session LOGINCOUNT
            LoadDefaultUser()
        End If
    End Sub

    Private Sub LoadDefaultUser()
        Dim objUserInfo As UserInfo = CType(Session.Item("LOGINUSERINFO"), UserInfo)
        Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        If (Not objUserInfo Is Nothing) And (Not objDealer Is Nothing) Then
            lblKodeDealer.Text = objDealer.DealerCode
            lblUserID.Text = objUserInfo.UserName
            lblQuesFavorit.Text = objUserInfo.Question
            Me.imgPhisingGuard.ImageUrl = "WebResources/GetImage.aspx?title=" & objUserInfo.UserProfile.ImageDescription & " &id=" & objUserInfo.UserProfile.ImageID
            btnVerify.Enabled = True
        Else
            btnVerify.Enabled = False
        End If
    End Sub

    Private Sub btnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerify.Click
        Dim objUserInfo As UserInfo = sessHelp.GetSession("LOGINUSERINFO")
        If objUserInfo Is Nothing Then
            Response.Redirect("Login.aspx")
            Return
        End If
        If isUserLock(objUserInfo) Then
            MessageBox.Show("Untuk sementara Anda tidak boleh login")
            LogTosyslog("User has been administratively locked by super-user", "user-blocked", "failed", "web-security", "read")
            Return
        End If
        Dim strMess As String

        If chkHandPhone.Checked = False And chkEmail.Checked = False Then
            MessageBox.Show("Silahkan Pilih Metode Pengiriman Password Baru Anda.")
            Return
        End If

        If Not Sms.IsSMSGatewayLive Then
            MessageBox.Show("Data Anda saat ini tidak dapat diproses, Silahkan coba lagi atau hubungi D-NET Admin.")
            LogTosyslog("SMS gateway cant be contacted, please check D-NET GSM modem", "sms-sent-not", "failed", "sms-device", "listen")
            Return
        End If


        If DoValidateQuestion(objUserInfo) Then
            Dim isHp As Boolean = chkHandPhone.Checked
            Dim isEmail As Boolean = chkEmail.Checked
            Dim isSMSSuccess As Boolean = False
            Dim isEmailSuccess As Boolean = False
            Dim noHP As String = objUserInfo.HandPhone.Substring(0, objUserInfo.HandPhone.Length - 4)
            noHP += "XXXX"
            strMess = "Password Berhasil Dikirim"
            SendPassword(objUserInfo, isHp, isEmail, isEmailSuccess, isSMSSuccess)

            If isSMSSuccess Then
                strMess += " lewat SMS : " & noHP
                'LogTosyslog(strMess, "sms-sent", "success", "sms-loginforget")
                LogTosyslog(" user have reset the password from forget password page, sms sent successfully", "forgetpassword-reset", "success", "web-security", "click")

            End If

            If isEmailSuccess Then
                If isHp Then
                    strMess += "  dan lewat Email : " & objUserInfo.Email
                Else
                    strMess += " lewat Email " & objUserInfo.Email
                End If
                'LogTosyslog(strMess, "email-sent", "success", "email-loginforget")
                LogTosyslog(" user have reset the password from forget password page, email sent successfully", "forgetpassword-reset", "success", "web-security", "click")

            End If

            If Not isSMSSuccess And Not isEmailSuccess Then
                strMess = "Password Gagal Dikirim"
            End If

            If Not isSMSSuccess Then
                'LogTosyslog(strMess, "sms-sent", "failed", "sms-loginforget")
                LogTosyslog(" user have reset the password from forget password page, but sms was not sent successfully, please check D-NET GSM modem", "forgetpassword-reset", "success", "web-security", "click")
            End If

            If Not isEmailSuccess Then
                'LogTosyslog(strMess, "email-sent", "failed", "email-loginforget")
                LogTosyslog(" user have reset the password from forget password page, but email was not sent successfully, please check D-NET mailserver", "forgetpassword-reset", "success", "web-security", "click")

            End If

            Response.Redirect("frmForgetPasswordConfirmation.aspx?msg=" & strMess)
        Else
            CountLoginFailed(objUserInfo)  '-- Lock user if exceed quota
            If isUserLock(objUserInfo) Then
                MessageBox.Show("Untuk Sementara user anda terkunci.")
                LogTosyslog("User have been locked due to many password attempt ", "user-lockout", "failed", "web-security", "read")
                Return
            End If
            MessageBox.Show("Pertanyaan dan jawaban tidak valid")
            LogTosyslog("Secret question was not answered correctly", "secretqa-valid-not", "failed", "web-security", "entry")

        End If
    End Sub

    Private Sub HyperLink1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HyperLink1.Click
        Session.RemoveAll()
        FormsAuthentication.SignOut()
        Response.Redirect("Login.aspx")
    End Sub

#End Region

#Region "Custom Method"
    Private Function DoValidateQuestion(ByVal objUser As UserInfo) As Boolean
        Dim DNetSecurity As KTB.DNet.UI.Helper.DNetEncryption
        Dim AnswerFav As String = txtAnsFavorit.Text.Trim
        Dim DBAnswerFav As String = DNetSecurity.SymmetricDecrypt(objUser.Answer, objUser.Question)
        Dim isAuth As Boolean = DBAnswerFav.Equals(AnswerFav)
        Return isAuth
    End Function

    Private Function GenerateNewPassword() As String
        Dim _strPassword As String
        Dim randomGenerator As New randomGenerator
        _strPassword = randomGenerator.GenarateRandom(8) 'Generete Alphanumeric
        Return _strPassword.ToLower
    End Function

    Public Sub ExecuteResetPassword(ByVal objUser As UserInfo, ByVal isHP As Boolean, ByVal isEmail As Boolean, ByRef isEmailSuccess As Boolean, ByRef isSMSSuccess As Boolean)
        confContext = CType(ConfigurationManager.GetCurrentContext(), ConfigurationContext)
        Dim securitySetting As SecuritySettings = CType(confContext.GetConfiguration(SecuritySettings.SectionName), SecuritySettings)
        dbAuthenticationProvider = CType(securitySetting.AuthenticationProviders(0), DbAuthenticationProviderData)
        userRoleMgr = New UserRoleManager(dbAuthenticationProvider.Database, confContext)
        Dim hashProviderFac As HashProviderFactory = New HashProviderFactory(confContext)
        hashProvider = hashProviderFac.CreateHashProvider(dbAuthenticationProvider.HashProvider)

        Dim _strPassword, _msgContent, _userName As String

        If Not IsNothing(objUser) Then
            _strPassword = GenerateNewPassword()
            Dim pwd As Byte() = hashProvider.CreateHash(Encoding.Unicode.GetBytes(_strPassword))
            objUser.Password = pwd

            _userName = objUser.Dealer.ID.ToString.PadLeft(6, "0") & objUser.UserName
            Try
                userRoleMgr.ChangeUserPassword(_userName, pwd)
                _msgContent = enumSMS.GetContentMessage(enumSMS.ContentMessage.ResetPasswordSuccess, Nothing, "", "", "", _strPassword)
                If _msgContent <> String.Empty Then
                    'InsertToSmsHistory(inbox)
                    If isHP Then

                        Dim otpfunc As New OTPFunction

                        otpfunc.SendSMSNotif(objUser.HandPhone, _msgContent)

                        If otpfunc.boolReturn Then
                            isSMSSuccess = True
                        Else
                            isSMSSuccess = False

                        End If
                    End If
                    If isEmail Then
                        Try
                            Dim strFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailAddressRecovery")
                            Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
                            _msgContent = GenerateHTMLEmailPassordRecovery(_msgContent)
                            ObjEmail = New DNetMail(smtp)
                            'masih masuk ke bulk atau spam
                            Try
                                ObjEmail.sendMail(objUser.Email, "", strFrom, "[MMKSI-DNet] Reset Password", Mail.MailFormat.Html, _msgContent)
                                isEmailSuccess = True
                            Catch ex As Exception
                                isEmailSuccess = False
                            End Try
                        Catch ex As Exception
                            isEmailSuccess = False
                        End Try
                    End If
                End If
            Catch ex As Exception
                'Password tidak bisa diupdate
            End Try
        End If

    End Sub

    Private Function SendPassword(ByVal objUser As UserInfo, ByVal isHP As Boolean, ByVal isEmail As Boolean, ByRef isEmailSuccess As Boolean, ByRef isSMSSuccess As Boolean) As Boolean
        ExecuteResetPassword(objUser, isHP, isEmail, isEmailSuccess, isSMSSuccess)
    End Function

    Private Function GenerateHTMLEmailPassordRecovery(ByVal bodyMsg As String) As String
        Dim sb As StringBuilder = New StringBuilder
        sb.Append("<html><body><table>")
        sb.Append("<html><body><table>")
        sb.Append("<tr><td>")
        sb.Append("Reset Password Berhasil")
        sb.Append("<td/><tr/>")
        sb.Append("<tr><td>")
        sb.Append(bodyMsg.Remove(0, ("Reset Password Berhasil ").Length))
        sb.Append("<td/><tr/>")
        Return sb.ToString()
    End Function

    Private Function isUserLock(ByVal objUser As UserInfo) As Boolean
        Dim loginMode As String = KTB.DNet.Lib.WebConfig.GetValue("LoginMode")
        If loginMode.Trim.ToUpper = "LIVE" Then
            If Not objUser Is Nothing Then
                Dim CurrentIP As String = HttpContext.Current.Request.UserHostAddress
                Dim collFinishLock As ArrayList = New ArrayList
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LockUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(LockUser), "IPAddress", MatchType.Exact, CurrentIP))
                criterias.opAnd(New Criteria(GetType(LockUser), "UserID", MatchType.Exact, objUser.ID))
                Dim listuserLosck As ArrayList = New LockUserFacade(User).Retrieve(criterias)
                Dim CurrentTime As DateTime = Now
                For Each item As LockUser In listuserLosck
                    If CurrentTime <= item.FinishLock Then
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function

    Private Sub LockUser(ByVal objUser As UserInfo)
        Dim loginMode As String = KTB.DNet.Lib.WebConfig.GetValue("LoginMode")
        If loginMode.Trim.ToUpper = "LIVE" Then
            If Not objUser Is Nothing Then
                Dim objLockUser As LockUser = New LockUser
                objLockUser.StartLock = Now
                objLockUser.UserID = objUser.ID
                Dim lockTime As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("LoginLock"))
                objLockUser.FinishLock = objLockUser.StartLock.AddMinutes(lockTime)
                objLockUser.IPAddress = HttpContext.Current.Request.UserHostAddress
                Dim objLockUserFacade As LockUserFacade = New LockUserFacade(User)
                Dim i As Integer = objLockUserFacade.Insert(objLockUser)

                '----------Send Email------------'
                Dim emailTo As String = KTB.DNet.Lib.WebConfig.GetValue("EmailAdminDNET")
                Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
                Dim msgEmailContent As String = enumSMS.GetContentEmail(enumSMS.ContentMessage.LoginFail, objUser).ToString
                SendEmail(emailTo, emailFrom, msgEmailContent)

            End If
        End If

    End Sub


    Private Sub LogTosyslog(ByVal message As String, ByVal sbMsg As String, ByVal rslMsg As String, Optional ByVal mdlmsg As String = "web-security", Optional ByVal sbAction As String = "click")
        Dim strLog As Boolean = KTB.DNet.Lib.WebConfig.GetValue("EnableSyslog")
        If strLog Then
            Dim objSyslog As SyslogParameter = New SyslogParameter(User)
            Dim m As KTB.DNet.Lib.SysLogXMLMessage = New KTB.DNet.Lib.SysLogXMLMessage
            m.Action = sbAction.ToLower
            m.SubBlockName = sbMsg.ToLower
            m.FullMessage = message.ToLower
            m.ModuleName = mdlmsg.ToLower
            m.Pages = HttpContext.Current.Request.Url.LocalPath
            m.RemoteIPAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
            m.StatusResult = rslMsg.ToLower
            m.Status = KTB.DNet.[Lib].DNetLogFormatStatus.Direct
            m.BlockName = "forget-first"
            Try
                m.UserName = IIf(User.Identity.Name.Length > 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name).ToLower
            Catch ex As Exception
                m.UserName = "Wb-Usr"
            End Try
            m.Dealer = CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode.ToLower
            objSyslog.LogError(m)
        End If
    End Sub

    Private Sub CountLoginFailed(ByVal objUserInfo As UserInfo)
        If Not objUserInfo Is Nothing Then
            Dim objSessionHelper As SessionHelper = New SessionHelper
            Dim count As Integer = 0
            Try
                count = IIf(Not objSessionHelper.GetSession("LOGINCOUNT") Is Nothing, _
                            CInt(objSessionHelper.GetSession("LOGINCOUNT")), 0)
                count += 1
                If count = 3 Then
                    LockUser(objUserInfo)
                    count = 0
                End If
                objSessionHelper.SetSession("LOGINCOUNT", count)
            Catch ex As Exception
                count = 0
            End Try
        End If
        'LogTosyslog("User " & User.Identity.Name & " Gagal Login  ")
    End Sub

    Private Sub SendEmail(ByVal emailTo As String, ByVal emailFrom As String, ByVal contentEmail As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Try
            ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet]", Mail.MailFormat.Html, contentEmail)
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
