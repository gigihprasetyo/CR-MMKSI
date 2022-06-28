Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Lib
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.PageHelper
Imports KTB.DNet.BusinessFacade.General
Imports Microsoft.Practices.EnterpriseLibrary.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security
Imports Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
Imports Microsoft.Practices.EnterpriseLibrary.Security.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database.Authentication.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database
Imports System.Text
Imports System.Web.Security
Imports KTB.DNet.Security
Imports System.Security.Principal
Imports System.Configuration

Public Class OTP
    Inherits System.Web.UI.UserControl

#Region " Private Variables"

    Private _UserProfileFacade As New UserProfileFacade(user)
    Private confContext As ConfigurationContext
    Private dbAuthenticationProvider As DbAuthenticationProviderData
    Private userRoleMgr As UserRoleManager
    Private hashProvider As IHashProvider
#End Region


    Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("OTP"), Nothing)
    Private ss As SessionHelper = New SessionHelper
    Dim expiredBingoCount As String = KTB.DNet.Lib.WebConfig.GetValue("ExpiredCount").ToString
    Private strActivityType As String
    Private bolStaCon As Boolean

    Public Property ActivityType() As String
        Get
            Return strActivityType
        End Get
        Set(value As String)
            strActivityType = value
        End Set
    End Property

    Public Property StatusControl() As Boolean
        Get
            Return bolStaCon
        End Get
        Set(value As Boolean)
            bolStaCon = value
            If bolStaCon = True Then
                lblAlert.Text = ""
                txtKodeOTP.Text = ""
            End If
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
            If ss.GetSession("OTPReload") = True Then
                Dim objOTP As KTB.DNet.Domain.OTP
                Dim result As Integer
                Dim arlOTP As ArrayList
                Dim criOTP As CriteriaComposite
                Dim str As String
                Dim objUser As UserInfo

                Try

                    If CType(ss.GetSession("LOGINUSERINFO"), UserInfo) Is Nothing Then
                        objUser = CType(ss.GetSession("ResetPass"), UserInfo)
                    Else
                        objUser = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
                    End If

                    Dim StaBol As Boolean

                    If btnSimpan.Enabled = False Then
                        Return
                    End If

                    If String.IsNullOrEmpty(txtKodeOTP.Text) Then
                        Return
                    Else
                        If CType(ss.GetSession("OTPLog"), KTB.DNet.Domain.OTP) IsNot Nothing Then
                            objOTP = CType(ss.GetSession("OTPLog"), KTB.DNet.Domain.OTP)
                            objOTP = New OTPFacade(user).Retrieve(objOTP.ID)
                            If DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") > objOTP.ValidUntil Then
                                Return
                            End If
                        End If
                    End If


                    'criOTP = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.OTP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'criOTP.opAnd(New Criteria(GetType(KTB.DNet.Domain.OTP), "ProcessType", MatchType.Exact, CType(ViewState("ProcessType"), Short)))
                    'criOTP.opAnd(New Criteria(GetType(KTB.DNet.Domain.OTP), "UserInfo.ID", MatchType.Exact, objUser.ID))

                    'objOTP = CType((New OTPFacade(user).RetrieveList(criOTP, "ID", Sort.SortDirection.DESC).Item(0)), KTB.DNet.Domain.OTP)

                    'If DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") > objOTP.ValidUntil Then
                    '    Return
                    'End If


                    StaBol = New OTPFacade(user).CheckOTPValid(objUser.ID, txtKodeOTP.Text)

                    If StaBol = False Then
                        lblAlert.Text = "Kode OTP Anda Tidak Valid"
                        txtKodeOTP.Text = ""
                    Else
                        Select Case strActivityType
                            Case "ResetToken"
                                resetToken(objUser)
                            Case "ResetPassword"
                                Dim resul As String = ResetPassword(objUser, "")
                                If Not String.IsNullOrEmpty(resul) Then

                                    Dim objAC As New AppConfig
                                    objAC = New AppConfigFacade(user).Retrieve("ResetPageOTP")
                                    Session.Clear()
                                    Response.Write("<script language='javascript'>alert('Password Telah Di Reset, Password Baru Telah Dikirim Ke Handphone Anda');</script>")
                                    'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & objAC.Value & "';", True)
                                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & GetAppURL() & "';", True)
                                    'Response.Redirect("Login.aspx", True)
                                End If
                            Case "RegistrasiUser"
                                AktivasiUser(objUser)
                            Case "ChangePhoneNumber"
                                ChangePhoneNumber(objUser)
                        End Select
                    End If

                Catch ex As Exception
                    MessageBox.Show(e.ToString)
                End Try
            End If
        End If
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        If ss.GetSession("OTPReload") IsNot Nothing Then
            ss.SetSession("OTPReload", True)
        Else
            ss.SetSession("OTPReload", False)
        End If
    End Sub

#Region "Custom Method"

    Private Function ChangePhoneNumber(ByVal objUser As UserInfo) As String
        Dim objotherUserInfo As UserInfo
        Dim objotherUserInfofac As New UserInfoFacade(user)
        Dim cri As CriteriaComposite
        Dim arrUserInfo As New ArrayList
        Dim result As Integer = 0

        Try
            cri = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cri.opAnd(New Criteria(GetType(UserInfo), "OrganizationID", MatchType.Exact, objUser.Dealer.ID))
            cri.opAnd(New Criteria(GetType(UserInfo), "ID", MatchType.Exact, objUser.ID))
            'cri.opAnd(New Criteria(GetType(UserInfo), "HandPhone", MatchType.Exact, objUser.HandPhone))

            arrUserInfo = objotherUserInfofac.RetrieveActiveList(cri, "ID", Sort.SortDirection.ASC)

            Dim objUsPro As New UserProfile

            If arrUserInfo.Count > 0 Then

                result = objotherUserInfofac.Update(objUser)

                If result > 0 Then

                    '        Dim X As String = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiX").ToString
                    '       Dim Y As String = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiY").ToString
                    Try
                        objUsPro = New UserProfileFacade(user).Retrieve(objUser.ID, "")
                        CheckMultiUser(objUser, objUsPro)
                        'SendingBingoCode(X, Y)
                        MessageBox.Show("Token baru berhasil dikirimkan ke HP anda")
                        LogTosyslog("new token has been sent to user HP", "token-sent", "success", "web-security", "edit")
                    Catch ex As Exception
                        MessageBox.Show("Token baru tidak berhasil dikirimkan ke HP anda")
                        LogTosyslog("new token has been sent to user HP", "token-sent-not", "failed", "web-security", "read")
                        LogTosyslog("new token has been sent to user HP", "sms-sent-not", "failed", "web-device", "listen")

                    End Try
                    Session.Add("CaptchaImageText", GenerateRandomCode)
                    Dim objAC As New AppConfig
                    objAC = New AppConfigFacade(user).Retrieve("ResetPageOTP")
                    Session.Clear()
                    Response.Write("<script language='javascript'>alert('Ganti Nomor Hp Berhasil. Silahkan Login Kembali');</script>")
                    ' Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & objAC.Value & "';", True)
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & GetAppURL() & "';", True)

                    'Server.Transfer("Login.aspx", True)
                    'Response.Redirect("Login.aspx", True)
                    'Server.Transfer(objAC.Value)
                    'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & objAC.Value & "';", True)
                End If
            Else
                MessageBox.Show("Update Gagal")
            End If



        Catch ex As Exception

        End Try


    End Function


    Private Function GetAppURL() As String
        Dim result As String = String.Empty
        result = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd((New Char() {"/"})) + "/"
        Return result
    End Function

    Private Function GenerateNewPassword() As String
        Dim _strPassword As String
        Dim randomGenerator As New RandomGenerator
        _strPassword = randomGenerator.GenarateRandom(8) 'Generete Alphanumeric
        Return _strPassword.ToLower
    End Function

    Private Function ResetPassword(ByVal objUser As UserInfo, Optional ByVal sPassword As String = "") As String
        Dim result As String = String.Empty
        confContext = CType(ConfigurationManager.GetCurrentContext(), ConfigurationContext)
        Dim securitySetting As SecuritySettings = CType(confContext.GetConfiguration(SecuritySettings.SectionName), SecuritySettings)
        dbAuthenticationProvider = CType(securitySetting.AuthenticationProviders(0), DbAuthenticationProviderData)
        userRoleMgr = New UserRoleManager(dbAuthenticationProvider.Database, confContext)
        Dim hashProviderFac As HashProviderFactory = New HashProviderFactory(confContext)
        hashProvider = hashProviderFac.CreateHashProvider(dbAuthenticationProvider.HashProvider)
        Dim _strPassword, _msgContent, _userName As String
        If Not IsNothing(objUser) Then
            If sPassword = "" Then
                _strPassword = GenerateNewPassword()
            Else
                _strPassword = sPassword
            End If
            result = "User Name : " & objUser.UserName & ", Org : " & objUser.Dealer.DealerCode & ", Password baru : " & _strPassword
            Dim pwd As Byte() = hashProvider.CreateHash(Encoding.Unicode.GetBytes(_strPassword))
            objUser.Password = pwd
            _userName = objUser.Dealer.ID.ToString.PadLeft(6, "0") & objUser.UserName
            Try

                
                _msgContent = enumSMS.GetContentMessage(enumSMS.ContentMessage.ResetPasswordSuccess, objUser, "", "", "", _strPassword)
                
                If _msgContent <> String.Empty AndAlso SendMessage2(objUser.HandPhone, _msgContent) Then
                    'Sms.Sendto(objUser.HandPhone, _msgContent)
                    'SendMessage(objUser.HandPhone, _msgContent)
                    userRoleMgr.ChangeUserPassword(_userName, pwd)
                Else
                    Throw New Exception("Kirim ke " & objUser.HandPhone)
                End If
            Catch ex As Exception
                Return result
                MessageBox.Show("Reset Password tidak berhasil : " & ex.Message)
                'LogTosyslog("password user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & " tidak berhasil di reset oleh " & CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode & "-" & CType(Session("LOGINUSERINFO"), UserInfo).UserName, "pass-reset-admin", "failed")
                LogTosyslog(" resetting password for user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & " was executed by " & CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode & "-" & CType(Session("LOGINUSERINFO"), UserInfo).UserName & " but failed ", "forcedpassword-reset-not", "failed", "web-security", "read")

            End Try
        End If
        Return result
    End Function

    Private Sub resetToken(objUser As UserInfo)
        Dim objotherUserInfo As UserInfo
        Dim objotherUserInfofac As New UserInfoFacade(user)
        Dim cri As CriteriaComposite
        Dim arrUserInfo As New ArrayList
        Dim result As Integer = 0

        Try
            cri = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cri.opAnd(New Criteria(GetType(UserInfo), "OrganizationID", MatchType.Exact, objUser.Dealer.ID))
            cri.opAnd(New Criteria(GetType(UserInfo), "ID", MatchType.Exact, objUser.ID))
            cri.opAnd(New Criteria(GetType(UserInfo), "HandPhone", MatchType.Exact, objUser.HandPhone))

            arrUserInfo = objotherUserInfofac.RetrieveActiveList(cri, "ID", Sort.SortDirection.ASC)

            Dim objUsPro As New UserProfile
            objUsPro = CType(arrUserInfo.Item(0), UserInfo).UserProfile

            result = New UserProfileFacade(user).Update(objUsPro)
            If result > 0 Then

                If arrUserInfo.Count > 0 Then

                    Dim X As String = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiX").ToString
                    Dim Y As String = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiY").ToString
                    Try
                        SendingBingoCode(X, Y)
                        MessageBox.Show("Token baru berhasil dikirimkan ke HP anda")
                        LogTosyslog("new token has been sent to user HP", "token-sent", "success", "web-security", "edit")
                    Catch ex As Exception
                        MessageBox.Show("Token baru tidak berhasil dikirimkan ke HP anda")
                        LogTosyslog("new token has been sent to user HP", "token-sent-not", "failed", "web-security", "read")
                        LogTosyslog("new token has been sent to user HP", "sms-sent-not", "failed", "web-device", "listen")

                    End Try
                    Session.Add("CaptchaImageText", GenerateRandomCode)
                    Dim objAC As New AppConfig
                    objAC = New AppConfigFacade(user).Retrieve("ResetPageOTP")

                    'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & objAC.Value & "';", True)
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & GetAppURL() & "';", True)
                    Session.Clear()
                    Response.Write("<script language='javascript'>alert('Token Anda Sudah Di Reset Dan Dikirim Melalui SMS. Silahkan Login Kembali');</script>")
                    'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & objAC.Value & "';", True)
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & GetAppURL() & "';", True)

                Else

                End If
            Else
                MessageBox.Show("Update Gagal")
            End If

        Catch ex As Exception

        End Try


        

    End Sub

    Private Sub AktivasiUser(objUser As UserInfo)
        Dim objotherUserInfo As UserInfo
        Dim objotherUserInfofac As New UserInfoFacade(user)
        Dim cri As CriteriaComposite
        Dim arrUserInfo As New ArrayList
        Dim result As Integer = 0

        Try
            cri = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cri.opAnd(New Criteria(GetType(UserInfo), "OrganizationID", MatchType.Exact, objUser.Dealer.ID))
            cri.opAnd(New Criteria(GetType(UserInfo), "ID", MatchType.Exact, objUser.ID))
            cri.opAnd(New Criteria(GetType(UserInfo), "HandPhone", MatchType.Exact, objUser.HandPhone))

            arrUserInfo = objotherUserInfofac.RetrieveActiveList(cri, "ID", Sort.SortDirection.ASC)

            Dim objUsPro As New UserProfile
            objUsPro = CType(arrUserInfo.Item(0), UserInfo).UserProfile

            objUsPro.RegistrationStatus = EnumSE.RegistrationStatus.Register
            objUsPro.ActivationStatus = EnumSE.ActivationCodeStatus.Active

            result = New UserProfileFacade(user).Update(objUsPro)
            If result > 0 Then

                If arrUserInfo.Count > 0 Then

                    
                    Try
                        CheckMultiUser(objUser, objUsPro)
                        MessageBox.Show("Token baru berhasil dikirimkan ke HP anda")
                        LogTosyslog("new token has been sent to user HP", "token-sent", "success", "web-security", "edit")
                    Catch ex As Exception
                        MessageBox.Show("Token baru tidak berhasil dikirimkan ke HP anda")
                        LogTosyslog("new token has been sent to user HP", "token-sent-not", "failed", "web-security", "read")
                        LogTosyslog("new token has been sent to user HP", "sms-sent-not", "failed", "web-device", "listen")

                    End Try
                    Session.Add("CaptchaImageText", GenerateRandomCode)
                    Dim objAC As New AppConfig
                    objAC = New AppConfigFacade(user).Retrieve("ResetPageOTP")
                    Session.Clear()
                    Response.Write("<script language='javascript'>alert('Aktivasi Login Anda Berhasil. Silahkan Login Kembali');</script>")
                    'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & objAC.Value & "';", True)
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Redirect", "window.parent.location='" & GetAppURL() & "';", True)

                Else

                End If
            Else
                MessageBox.Show("Update Gagal")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function CheckMultiUser(objUser As UserInfo, ByVal objUserProf As UserProfile) As Boolean
        Dim ret As Boolean = False
        Dim cri As CriteriaComposite
        Dim arrUserInfo As New ArrayList
        Dim res As Integer

        Try

            Dim X As String = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiX").ToString
            Dim Y As String = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiY").ToString

            cri = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cri.opAnd(New Criteria(GetType(UserInfo), "ID", MatchType.No, objUser.ID))
            cri.opAnd(New Criteria(GetType(UserInfo), "HandPhone", MatchType.Exact, objUser.HandPhone))
            cri.opAnd(New Criteria(GetType(UserInfo), "UserProfile.RegistrationStatus", MatchType.Exact, CType(EnumSE.RegistrationStatus.Register, Short)))
            cri.opAnd(New Criteria(GetType(UserInfo), "UserProfile.ActivationStatus", MatchType.Exact, CType(EnumSE.ActivationCodeStatus.Active, Short)))

            arrUserInfo = New UserInfoFacade(user).RetrieveActiveList(cri, "ID", Sort.SortDirection.ASC)

            If arrUserInfo.Count > 0 Then
                Dim objBingo As Bingo
                Dim objUserPro As UserProfile
                Dim ContentSMS As String = "Token MMKSI Anda Sama Seperti Token Anda Pada Dealer: "

                objUserPro = New UserProfileFacade(user).Retrieve((CType(arrUserInfo.Item(0), UserInfo).ID), "")
                objBingo = objUserPro.Bingo

                ContentSMS = ContentSMS + (CType(arrUserInfo.Item(0), UserInfo).Dealer.DealerCode)
                ContentSMS = ContentSMS + " . Dengan SN : " + objBingo.SerialNumber.ToString

                If LogBingo(objUserPro, objUser) Then
                    SendMessage(ContentSMS)
                End If

                objUserProf.Bingo = objBingo
                res = New UserProfileFacade(user).Update(objUserProf)
            Else
                SendingBingoCode(X, Y)
            End If

        Catch ex As Exception

        End Try
        Return ret
    End Function

    Private Function GenerateRandomCode() As String
        Try
            Dim generator As RandomGenerator = New RandomGenerator
            Return generator.GenarateRandomCharacterOnly(5).ToUpper

        Catch ex As Exception
            Return "ACFDE"
        End Try
    End Function

    Private Sub LogTosyslog(ByVal message As String, ByVal sbMsg As String, ByVal rslMsg As String, Optional ByVal mdlmsg As String = "web-security", Optional ByVal sbAction As String = "view")
        Dim strLog As Boolean = KTB.DNet.Lib.WebConfig.GetValue("EnableSyslog")
        If strLog Then
            Dim objSyslog As SyslogParameter = New SyslogParameter(user)
            Dim m As KTB.DNet.Lib.SysLogXMLMessage = New KTB.DNet.Lib.SysLogXMLMessage
            m.Action = sbAction.ToLower
            m.SubBlockName = sbMsg.ToLower
            m.FullMessage = message.ToLower
            m.ModuleName = mdlmsg.ToLower
            m.Pages = HttpContext.Current.Request.Url.LocalPath
            m.RemoteIPAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
            m.StatusResult = rslMsg.ToLower
            m.Status = KTB.DNet.[Lib].DNetLogFormatStatus.Direct
            m.BlockName = "private-data"


            Try
                m.UserName = IIf(user.Identity.Name.Length > 6, Right(user.Identity.Name, user.Identity.Name.Length - 6), user.Identity.Name).ToLower
            Catch ex As Exception
                m.UserName = "Wb-Usr"
            End Try
            m.Dealer = CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode.ToLower
            objSyslog.LogError(m)
        End If
    End Sub


    Private Sub SendingBingoCode(ByVal x As Int16, ByVal y As Int16)
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser Is Nothing Then
            MessageBox.Show("Session anda sudah habis, Silahkan Login Ulang")
            Return
        End If
        If Sms.IsSMSGatewayLive Then
            Dim bingoHashing As HashingBingo = New HashingBingo(3, 7, 2)
            Dim sn = SerialValidation()
            bingoHashing.GenerateBingo()
            Dim smsBingo As String = bingoHashing.BingoSMS
            Dim ContentSMS As String = enumSMS.GetContentMessage(enumSMS.ContentMessage.BingoCardNotification, objUser.UserProfile, sn, smsBingo, expiredBingoCount)
            Dim listBingo As ArrayList = bingoHashing.Bingo
            If LogBingo(listBingo, x, y, sn) Then
                SendMessage(ContentSMS)
            End If
        Else
            MessageBox.Show("Data Anda saat ini tidak dapat diproses, Silahkan coba lagi atau hubungi D-NET Admin.")
        End If
    End Sub

    Private Function SerialValidation() As String
        Dim _sn As String
        Dim random As RandomGenerator = New RandomGenerator
        Dim fBingo As BingoFacade = New BingoFacade(user)
        Do
            _sn = random.GetRandomNumeric(8)
        Loop Until fBingo.RetrieveBingoForValidation(_sn) = True
        Return _sn
    End Function

    Private Sub SendMessage(ByVal msg As String)
        Dim HP As String = ""
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUser Is Nothing Then
            If objUser.HandPhone <> String.Empty Then
                HP = objUser.HandPhone
            End If
        End If
        If HP.Length > 0 Then
            Dim otpfunc As New OTPFunction

            otpfunc.SendSMSNotif(HP, msg)
            If (Not otpfunc.boolReturn) Then
                MessageBox.Show("Pengiriman SMS Gagal. Silahkan Menghubungi Administrator Anda")
                Return
        End If
            'Sms.Sendto(HP, msg)
        End If
    End Sub

    Private Sub SendMessage(ByVal strNoPhone As String, ByVal msg As String)
        
        Dim otpfunc As New OTPFunction

        otpfunc.SendSMSNotif(strNoPhone, msg)
        If (Not otpfunc.boolReturn) Then
            MessageBox.Show("Pengiriman SMS Gagal. Silahkan Menghubungi Administrator Anda")
            Return
        End If
        'Sms.Sendto(HP, msg)

    End Sub

    Private Function SendMessage2(ByVal strNoPhone As String, ByVal msg As String) As Boolean

        Dim otpfunc As New OTPFunction

        otpfunc.SendSMSNotif(strNoPhone, msg)
        Return otpfunc.boolReturn
        'Sms.Sendto(HP, msg)

    End Function

    Private Function LogBingo(ByVal objUsPro As UserProfile, ByVal objUsIn As UserInfo) As Boolean
        Dim result As Boolean = True
        Dim intresult As Integer
        Dim sn As String
        Dim criUsIn As CriteriaComposite
        Dim arrUsIn As ArrayList

        Try
            criUsIn = New CriteriaComposite(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criUsIn.opAnd(New Criteria(GetType(UserProfile), "UserInfo.ID", MatchType.Exact, objUsIn.ID))

            arrUsIn = New UserProfileFacade(user).Retrieve(criUsIn)

            For Each row As UserProfile In arrUsIn
                row.Bingo.ID = objUsPro.Bingo.ID
                intresult = New UserProfileFacade(user).Update(row)
                If intresult < 1 Then
                    MessageBox.Show("-OTP Generate Token Aktivitas User-.Generate Bingo Gagal, Silahkan Menghubungi IT admin ")
                    result = False
                    Exit For
                End If
            Next

        Catch ex As Exception

        End Try

        Return result
    End Function


    Private Function LogBingo(ByVal listBingo As ArrayList, ByVal x As Int16, ByVal y As Int16, ByVal sn As String) As Boolean
        Dim result As Boolean = False
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        Dim _bingo As KTB.DNet.Domain.Bingo = New KTB.DNet.Domain.Bingo
        _bingo.DimensiX = x
        _bingo.DimensiY = y
        _bingo.SerialNumber = sn
        _bingo.Handphone = objUser.HandPhone
        _bingo.ExpiredCount = expiredBingoCount
        For Each item As String() In listBingo
            Dim matrix As BingoMatrix = New BingoMatrix
            matrix.PosisiY = CType(item(0), String)
            matrix.PosisiX = CType(item(1), String)
            matrix.Code = CType(item(2), String)
            _bingo.BingoMatrixs.Add(matrix)
        Next
        Dim _bingoFacade As BingoFacade = New BingoFacade(user)
        Dim i As Int16 = _bingoFacade.Insert(_bingo, objUser.UserProfile, user.Identity.Name, False)
        If i = 0 Then
            result = True
        End If
        Return result
    End Function

#End Region


End Class