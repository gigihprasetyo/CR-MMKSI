#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Text
Imports System.Security.Principal
Imports System.Web.Security
Imports System.Configuration
Imports System.Web.UI.WebControls
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PageHelper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Lib

''Imports Microsoft.Practices.EnterpriseLibrary.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security
Imports Microsoft.Practices.EnterpriseLibrary.Security.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database.Authentication.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database
Imports Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
#End Region

Public Class frmForgetPasswordNext
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DealerTextBox As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents NameTextBox As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents btnReSendOTP As System.Web.UI.WebControls.Button
    'Protected WithEvents lblQuestion2 As System.Web.UI.WebControls.Label
    'Protected WithEvents txtAnswer2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    'Protected WithEvents lblQuestion1 As System.Web.UI.WebControls.Label
    'Protected WithEvents txtAnswer1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHome As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents CodeNumberTextBox As System.Web.UI.WebControls.TextBox
    Protected WithEvents ImgCaptcha As System.Web.UI.WebControls.Image
    Protected WithEvents lnkBack As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents question1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents question2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents otpdiv As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblPhoneNo As System.Web.UI.WebControls.Label
    Protected WithEvents OTPPage As Global.OTP

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
    Private objUserPro As UserProfile
    Private identity As IIdentity
    Private pricipal As IPrincipal
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtHome.Text = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd((New Char() {"/"})) + "/"
        If Not Page.IsPostBack Then
            Session.Add("CaptchaImageText", GenerateRandomCode.ToUpper)
            ImgCaptcha.ImageUrl = "JpegImage.aspx"
            question2.Visible = False
            otpdiv.Visible = False
            ViewState("vsProcess") = CType("False", String)

            If Not String.IsNullOrEmpty(CType(sessHelp.GetSession("vsCaptcha"), String)) Then
                MessageBox.Show(CType(sessHelp.GetSession("vsCaptcha"), String))
            Else
                ViewState("vsCaptcha") = ""
            End If

        Else

            If CType(ViewState("vsProcess"), String) = "False" Then

                objUserInfo = Nothing
                objUserInfo = New UserInfoFacade(User).Retrieve(NameTextBox.Text, DealerTextBox.Text)

                If objUserInfo.ID > 0 Then
                    objUserPro = New UserProfileFacade(User).Retrieve(objUserInfo.ID, DealerTextBox.Text)
                    If objUserPro IsNot Nothing Then
                        sessHelp.SetSession("ResetPass", objUserInfo)
                        'Dim otp As New OTPFunction
                        'otp.generateCodeOTP(objUserInfo)
                        sessHelp.SetSession("OTPReload", False)
                        otpdiv.Visible = True
                        btnFind.Enabled = False

                    Else
                        sessHelp.SetSession("LOGINUSERINFO", New UserInfo)
                    End If
                End If
            Else

                Session.Add("CaptchaImageText", GenerateRandomCode.ToUpper)
                ImgCaptcha.ImageUrl = "JpegImage.aspx"
                sessHelp.SetSession("OTPReload", True)
                'generateCodeOTP()
            End If
        End If

        btnReSendOTP.Attributes.Add("style", "visibility: hidden")

    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim captcaText As String = String.Empty
        captcaText = Session.Item("CaptchaImageText")
        If CodeNumberTextBox.Text.ToUpper.Trim <> captcaText.Trim.ToUpper Then
            Session.Add("CaptchaImageText", GenerateRandomCode.ToUpper)
            sessHelp.SetSession("vsCaptcha", "Kode Captcha Anda Tidak Valid")
            ViewState("vsProcess") = CType("False", String)
            Response.Redirect(Request.RawUrl)
            'Return
        End If
        objUserInfo = New UserInfoFacade(User).Retrieve(NameTextBox.Text, DealerTextBox.Text)
        If (Not objUserInfo Is Nothing) AndAlso (objUserInfo.ID > 0) Then '-- User is registered
            If String.IsNullOrEmpty(objUserInfo.HandPhone) Then
                MessageBox.Show("User Name Anda Belum Melakukan Aktivasi User")
                Return
            End If
            If Not objUserInfo.UserProfile Is Nothing Then  '-- User has profile
                '-- Take 2 pairs of question & answer randomly from user profile table
                Randomize()  '-- Initialize random-number generator
                Dim arRandNo(1) As Integer  '-- Array of two integers

                arRandNo(0) = CInt(Int((5 * Rnd()) + 1))  '-- First integer between 1 and 5
                Do
                    arRandNo(1) = CInt(Int((5 * Rnd()) + 1))  '-- Second integer between 1 and 5
                Loop Until arRandNo(0) <> arRandNo(1)  '-- The first may not equal the second

                sessHelp.SetSession("arRandNo", arRandNo)  '-- Store array random number

                question2.Visible = True

                sessHelp.SetSession("LOGINCOUNT", 0)  '-- Init session LOGINCOUNT
                If Not String.IsNullOrEmpty(objUserInfo.HandPhone) Then
                    lblPhoneNo.Text = objUserInfo.HandPhone
                Else
                    lblPhoneNo.Text = objUserInfo.Telephone
                End If


                sessHelp.SetSession("ResetPass", objUserInfo)

                sessHelp.SetSession("OTPReload", False)
                ViewState("vsProcess") = CType("True", String)
                otpdiv.Visible = True
                ViewState("Captcha") = CodeNumberTextBox.Text

                Dim otpfunc As New OTPFunction

                otpfunc.generateCodeOTP(lblPhoneNo.Text.Trim, objUserInfo)
                If (Not otpfunc.boolReturn) Then
                    MessageBox.Show("Pengiriman SMS Gagal Silakan Hubungi Administrator Anda")
                    Return
                End If

                ImgCaptcha.Visible = False
                CodeNumberTextBox.Visible = False

                lblPhoneNo.Text = Replace(lblPhoneNo.Text, Right(lblPhoneNo.Text, 4), "****")
            Else  '-- User without profile
                'btnVerify.Enabled = False  '-- Disable button Verifikasi
                Dim strHeader As String
                Try
                    strHeader = KTB.DNet.Lib.WebConfig.GetValue("HeaderKTB").ToString()
                Catch ex As Exception
                    strHeader = "D-NET"
                End Try

                Session.Clear()
                Response.Write("<script language='javascript'>alert('Silahkan Login ke Dnet , lalu isilah nomor HP Anda pada Halaman Sekuriti, sebab Kata Kunci yang baru akan dikirimkan ke HP Anda.');</script>")
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & GetAppURL() & "';", True)
            End If
            sessHelp.SetSession("OTPLog", Nothing)
        Else  '-- User is not registered
            'btnVerify.Enabled = False  '-- Disable button Verifikasi
            If Not IsUserValid() Then
                MessageBox.Show("User Name yang dimasukkan tidak terdaftar")
                LogTosyslog("Unregistered username was entered", "user-found-not", "failed", "web-security", "entry")
            Else
                MessageBox.Show("Cek ulang kode organisasi dan nama user anda")
                LogTosyslog("Unregistered dealer code was entered", "dealer-found-not", "failed", "web-security", "entry")
            End If
        End If

        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Session.Add("CaptchaImageText", GenerateRandomCode.ToUpper)
        'ImgCaptcha.ImageUrl = "JpegImage.aspx"

        CodeNumberTextBox.Text = String.Empty
    End Sub
    Private Sub btnReSendOTP_Click(sender As Object, e As EventArgs) Handles btnReSendOTP.Click

        Dim objUser As UserInfo = sessHelp.GetSession("ResetPass")

        Dim otpfunc As New OTPFunction

        otpfunc.generateCodeOTP(objUser)
        If (Not otpfunc.boolReturn) Then
            MessageBox.Show("Pengiriman SMS Gagal Silakan Hubungi Administrator Anda")
            Return
        End If
        OTPPage.StatusControl = True
    End Sub

    Private Sub btnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim captcaText As String = String.Empty
        captcaText = Session.Item("CaptchaImageText")
        If CodeNumberTextBox.Text.ToUpper.Trim <> captcaText.Trim.ToUpper Then
            Session.Add("CaptchaImageText", GenerateRandomCode.ToUpper)
            MessageBox.Show("Kode Captcha yang anda masukan salah")
            Return
        End If

        '-- Try to read this user
        objUserInfo = New UserInfoFacade(User).Retrieve(NameTextBox.Text, DealerTextBox.Text)

        If Not objUserInfo Is Nothing Then  '-- User is registered
            If isUserLock(objUserInfo) Then
                MessageBox.Show("Untuk Sementara Anda tidak boleh login")
                LogTosyslog("user has been administratively locked by super user", "user-blocked", "failed", "web-security", "read")
                Return
            End If
            If Not objUserInfo.UserProfile Is Nothing Then  '-- User has profile
                If DoValidateQuestion(objUserInfo) Then
                    Session.Add("DEALER", objUserInfo.Dealer)
                    Session.Add("LOGINUSERINFO", objUserInfo)
                    DoUpdateProfile(objUserInfo)
                    LogTosyslog("forget password phase 1 success", "process-completed", "success", "web-security", "read")
                    Dim urlSecodnForgetPassword As String = "frmForgetPassword.aspx"
                    RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + urlSecodnForgetPassword + """)</script>")
                Else
                    CountLoginFailed(objUserInfo)  '-- Lock user if exceed quota
                    If isUserLock(objUserInfo) Then
                        MessageBox.Show("Untuk sementara login anda terkunci.")  '-- Redirect to Login screen if failed 3 times
                        LogTosyslog("User have been locked due to many password attempt", "user-lockout", "failed", "web-security", "read")
                        Return
                    End If
                    MessageBox.Show("Pertanyaan dan jawaban tidak valid")
                    LogTosyslog("Secret question was not answered correctly", "secretqa-valid-not", "failed", "web-security", "entry")

                End If

            Else  '-- User without profile
                MessageBox.Show("User Name yang dimasukkan tidak punya profile")
                LogTosyslog("user does not have profile", "user-found-not", "failed", "web-security", "read")
            End If

        Else  '-- User is not registered
            If Not IsUserValid() Then
                MessageBox.Show("User Name yang dimasukkan tidak terdaftar")
                LogTosyslog("user cant be found, retries still valid", "user-found-not", "failed", "web-security", "read")
            Else
                MessageBox.Show("Cek ulang kode organisasi dan nama user anda")
                LogTosyslog("user cant be found, locked because exceeded attempt", "user-lockout", "failed", "web-security", "read")
            End If
        End If
    End Sub

    Private Function GetAppURL() As String
        Dim result As String = String.Empty
        result = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd((New Char() {"/"})) + "/"
        Return result
    End Function

    Private Function IsUserValid() As Boolean
        Dim objSessionHelper As SessionHelper = New SessionHelper
        Dim count As Integer = 0
        Try
            count = IIf(Not objSessionHelper.GetSession("LOGINCOUNT") Is Nothing, _
                        CInt(objSessionHelper.GetSession("LOGINCOUNT")), 0)
            count += 1
            If count = 3 Then
                Return True
            End If
            objSessionHelper.SetSession("LOGINCOUNT", count)
        Catch ex As Exception
            count = 0
        End Try
        'LogTosyslog("User " & User.Identity.Name & " tidak valid.")
        Return False
    End Function

    Private Sub DoUpdateProfile(ByVal objUser As UserInfo)
        If Not objUser.UserProfile Is Nothing Then
            Dim objUserDB As UserInfo = New UserInfoFacade(User).Retrieve(objUser.ID)
            Dim objUserProfile As UserProfile = objUserDB.UserProfile
            If objUser.LastLogin.Day = Now.Day Then
                objUserProfile.LoginCount += 1
            Else
                objUserProfile.LoginCount = 1
            End If
            objUserProfile.SessionID = HttpContext.Current.Session.SessionID
            objUserProfile.LastLoginIPAddress = HttpContext.Current.Request.UserHostAddress

            Dim objUserProfileFacade As UserProfileFacade = New UserProfileFacade(User)
            objUserProfileFacade.Update(objUserProfile)
        End If
    End Sub

    Private Sub HyperLink1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Redirect("Login.aspx")
    End Sub

#End Region

#Region "Custom Method"

    Private Function DoValidateQuestion(ByVal objUser As UserInfo) As Boolean
        Dim DNetSecurity As KTB.DNet.UI.Helper.DNetEncryption

        'Dim Answer1 As String = txtAnswer1.Text.Trim
        'Dim Answer2 As String = txtAnswer2.Text.Trim

        Dim DBAnswerFav As String = DNetSecurity.SymmetricDecrypt(objUser.Answer, objUser.Question)
        Dim DBAnswer1 As String = String.Empty
        Dim DBAnswer2 As String = String.Empty

        Dim firstSeletecd As String = CType(sessHelp.GetSession("arRandNo"), Object)(0)
        Dim secondSelected As String = CType(sessHelp.GetSession("arRandNo"), Object)(1)

        If firstSeletecd = "" OrElse secondSelected = "" Then
            Return False
        End If

        Select Case firstSeletecd
            Case 1
                DBAnswer1 = DNetSecurity.SymmetricDecrypt(objUser.UserProfile.Answer1, objUser.UserProfile.Question1)
            Case 2
                DBAnswer1 = DNetSecurity.SymmetricDecrypt(objUser.UserProfile.Answer2, objUser.UserProfile.Question2)
            Case 3
                DBAnswer1 = DNetSecurity.SymmetricDecrypt(objUser.UserProfile.Answer3, objUser.UserProfile.Question3)
            Case 4
                DBAnswer1 = DNetSecurity.SymmetricDecrypt(objUser.UserProfile.Answer4, objUser.UserProfile.Question4)
            Case 5
                DBAnswer1 = DNetSecurity.SymmetricDecrypt(objUser.UserProfile.Answer5, objUser.UserProfile.Question5)
        End Select

        Select Case secondSelected
            Case 1
                DBAnswer2 = DNetSecurity.SymmetricDecrypt(objUser.UserProfile.Answer1, objUser.UserProfile.Question1)
            Case 2
                DBAnswer2 = DNetSecurity.SymmetricDecrypt(objUser.UserProfile.Answer2, objUser.UserProfile.Question2)
            Case 3
                DBAnswer2 = DNetSecurity.SymmetricDecrypt(objUser.UserProfile.Answer3, objUser.UserProfile.Question3)
            Case 4
                DBAnswer2 = DNetSecurity.SymmetricDecrypt(objUser.UserProfile.Answer4, objUser.UserProfile.Question4)
            Case 5
                DBAnswer2 = DNetSecurity.SymmetricDecrypt(objUser.UserProfile.Answer5, objUser.UserProfile.Question5)
        End Select

        'Dim isAuth As Boolean = (Answer1.Trim.ToUpper.Equals(DBAnswer1.Trim.ToUpper)) And (Answer2.Trim.ToUpper.Equals(DBAnswer2.Trim.ToUpper))
        'Return isAuth
        Return True
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
                Dim msgEmailContent As String = enumSMS.GetContentEmail(enumSMS.ContentMessage.LoginFail, objUserInfo).ToString
                SendEmail(emailTo, emailFrom, msgEmailContent)
            End If
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
        '    LogTosyslog("User " & User.Identity.Name & " Gagal Login  ")
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
            m.BlockName = "forget-second"

            m.UserName = NameTextBox.Text.ToLower
            m.Dealer = DealerTextBox.Text.ToLower
            objSyslog.LogError(m)
        End If
    End Sub

    Private Function GenerateRandomCode() As String
        Try
            Dim generator As RandomGenerator = New RandomGenerator
            Return generator.GenarateRandomCharacterOnly(5).ToUpper
        Catch ex As Exception
            Return "ADFER"
        End Try
    End Function

    Private Sub SendEmail(ByVal emailTo As String, ByVal emailFrom As String, ByVal contentEmail As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet]", Mail.MailFormat.Html, contentEmail)
    End Sub

    Private Sub lnkBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBack.Click
        Session.RemoveAll()
        FormsAuthentication.SignOut()
        Response.Redirect("login.aspx")
    End Sub



#End Region

End Class
