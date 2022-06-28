Imports System
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security
Imports System.Text
Imports System.Security.Principal
Imports System.Web.Security
Imports System.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Lib

Imports System.Math
Imports System.Drawing

Public Class frmSecondAuth
    Inherits System.Web.UI.Page

#Region "Privateuser"
    Private objUserInfo As UserInfo
    Private ss As SessionHelper = New SessionHelper
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents lblAlertBingo As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastLogin As System.Web.UI.WebControls.Label
    Protected WithEvents lblJumlahLogin As System.Web.UI.WebControls.Label
    Protected WithEvents lblSerial As System.Web.UI.WebControls.Label
    Protected WithEvents lblValidUntil As System.Web.UI.WebControls.Label
    Protected WithEvents PanelBingo As System.Web.UI.WebControls.Panel
    Protected WithEvents btnProsess As System.Web.UI.WebControls.Button
    Protected WithEvents btnLogout As System.Web.UI.WebControls.Button
    Protected WithEvents photoView As System.Web.UI.WebControls.Image
    Protected WithEvents imgStar As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents lblValiduntil2 As System.Web.UI.WebControls.Label
    Protected WithEvents LnkTerms As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnForget As System.Web.UI.WebControls.LinkButton
    Private rndResult As String = String.Empty
#End Region

   

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents Textbox7 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    'Private authenticationProvider As IAuthenticationProvider
    'Private rolesProvider As IRolesProvider
    Private identity As IIdentity
    Private htKeys As Hashtable

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        InitializeComponent()
        Dim x As Integer
        Dim y As Integer
        Dim w As Integer
        Try
            Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
            x = objUser.UserProfile.Bingo.DimensiX
            y = objUser.UserProfile.Bingo.DimensiY
        Catch ex As Exception
            x = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiX")
            y = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiY")
        End Try
        w = KTB.DNet.Lib.WebConfig.GetValue("BingoCombination")
        GenerateTextBox(w, x, y)
    End Sub

#End Region

    Private Function GenerateRandomCode() As String
        Try
            Dim generator As RandomGenerator = New RandomGenerator
            Return generator.GenarateRandomCharacterOnly(5).ToUpper
        Catch ex As Exception
            Return "ACFSE"
        End Try
    End Function

    Private Sub lbtnForget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnForget.Click
        Response.Redirect("frmForgetToken.aspx")
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            imgStar.Visible = False
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Not Page.IsPostBack Then
                Dim isExpired As String = ""
                isExpired = Request.QueryString("isExpired")
                Session.Add("CaptchaImageText", GenerateRandomCode)
                Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
                If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER.LEASING Then
                    LnkTerms.Attributes.Add("onclick", "window.open('euladsf.html','disclaimer','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=700,height=500'); return false;")
                Else
                    LnkTerms.Attributes.Add("onclick", "window.open('eula2.html','disclaimer','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=700,height=500'); return false;")
                End If

                If Not objUser Is Nothing Then
                    If Not objUser.UserProfile Is Nothing Then
                        If isExpired = "TRUE" Then
                            MessageBox.Show("Mohon maaf proses transisi HP anda ke HP : " & objUser.UserProfile.TransitionHP & " dibatalkan sampai saat ini anda belum mengaktivasinya, nomer HP anda yang valid kembali menjadi : " & objUser.HandPhone)
                            LogTosyslog("Mohon maaf proses transisi HP anda ke HP : " & objUser.UserProfile.TransitionHP & " dibatalkan sampai saat ini anda belum mengaktivasinya, nomer HP anda yang valid kembali menjadi : " & objUser.HandPhone, "handphone-reverted", "failed", "web-security", "reset")

                        End If
                        Me.photoView.ImageUrl = "WebResources/GetImage.aspx?title=" & objUser.UserProfile.ImageDescription & " &id=" & objUser.UserProfile.ImageID
                        lblLastLogin.Text = objUser.LastLogin.ToString("dd-MMM-yyyy HH:mm:ss") & " WIB"
                        lblJumlahLogin.Text = objUser.UserProfile.LoginCount & "x pada hari ini"
                        If Not objUser.UserProfile Is Nothing Then
                            If Not objUser.UserProfile.Bingo Is Nothing Then
                                lblSerial.Text = objUser.UserProfile.Bingo.SerialNumber
                                Dim t1 As DateTime = objUser.UserProfile.Bingo.BingoValidUntil
                                Dim t2 As DateTime = System.DateTime.Now
                                If objUser.UserProfile.Bingo.ExpiredCount < 0 Then
                                    btnProsess.Enabled = True
                                    imgStar.Visible = False
                                    lblValidUntil.Visible = False
                                    lblValiduntil2.Visible = False
                                Else
                                    If DateTime.Compare(t1, t2) >= 0 Then
                                        Dim BingoReminderDay = KTB.DNet.Lib.WebConfig.GetValue("BingoReminderDay")
                                        Dim diff1 As System.TimeSpan
                                        diff1 = t1.Subtract(t2)
                                        Dim days As Integer = diff1.Days + 1
                                        If days <= BingoReminderDay Then
                                            lblValidUntil.Text = "Token Anda akan kadaluarsa " & days & " hari lagi. Silakan mengakses halaman sekuriti dan perhatikan tombol dengan tanda bintang "
                                            imgStar.Visible = True
                                            lblValiduntil2.Text = " sebagai panduan untuk mereset token Anda."
                                        End If
                                    Else
                                        btnProsess.Enabled = False
                                        imgStar.Visible = False
                                        lblValidUntil.Text = "Token sudah kadaluarsa, segera lakukan  Reset Token via SMS"
                                    End If
                                End If
                            End If
                        End If
                    Else
                        Me.photoView.ImageUrl = "WebResources/GetImage.aspx?id=0"
                    End If
                End If
            End If

            btnLogout.Attributes.Add("onclick", "return confirm('Anda yakin akan keluar dari halaman ini ?');")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        
    End Sub

    Private Function ConvertToCoordinat(ByVal x As Integer, ByVal y As Integer, ByVal posisi As Integer) As String
        If x <> 0 And y <> 0 Then
            Dim cordX As Integer = posisi Mod x
            Dim cordY As Integer = Floor(posisi / x) + 1
            If cordX = 0 Then
                cordX = x
                cordY = cordY - 1
            End If
            Dim result As String = cordX & "-" & cordY
            Return result
        Else
            Return String.Empty
        End If
    End Function

    Private Sub GenerateTextBox(ByVal count As String, ByVal x As Int16, ByVal y As Int16)
        Dim rnd As RandomGenerator = New RandomGenerator
        If ss.GetSession("RANDOM") Is Nothing Then
            rndResult = rnd.GetRandomNumeric(count, 1, x * y, True)
            ss.SetSession("RANDOM", rndResult)
        Else
            rndResult = CType(ss.GetSession("RANDOM"), String)
        End If

        Dim value As String() = rndResult.Split(";".ToCharArray())
        Dim _hastTable As Hashtable = New Hashtable(CInt(count))
        PanelBingo.Controls.Add(GetLiteral("<Table>"))
        PanelBingo.Controls.Add(GetLiteral("<TR>"))
        PanelBingo.Controls.Add(GetLiteral("<TD align=center>"))
        PanelBingo.Controls.Add(GetLiteral("&nbsp;"))
        PanelBingo.Controls.Add(GetLiteral("</TD>"))

        For z As Integer = 1 To x
            Dim myLabel As Label = New Label
            PanelBingo.Controls.Add(GetLiteral("<TD align=center>"))
            PanelBingo.Controls.Add(myLabel)
            PanelBingo.Controls.Add(GetLiteral("</TD>"))
        Next

        PanelBingo.Controls.Add(GetLiteral("</TR>"))
        For i As Integer = 1 To y
            PanelBingo.Controls.Add(GetLiteral("<TR>"))
            PanelBingo.Controls.Add(GetLiteral("<TD>"))
            Dim myLabel As Label = New Label
            myLabel.Text = "#" & i
            PanelBingo.Controls.Add(myLabel)
            PanelBingo.Controls.Add(GetLiteral("</TD>"))
            For j As Integer = 1 To x
                Dim id As String = j.ToString & "-" & i.ToString
                Dim txtBox As TextBox = New TextBox
                txtBox.ID = id
                txtBox.Width = New Unit(25)
                PanelBingo.Controls.Add(GetLiteral("<TD>"))
                If IsSelectedBingo(j, i, value, x, y) Then
                    txtBox.Enabled = True
                    txtBox.TextMode = TextBoxMode.Password
                    txtBox.MaxLength = 2
                    txtBox.Attributes.Add("onkeypress", "return alphaNumericWith(event,'');")
                    _hastTable.Add(id, String.Empty)
                Else
                    txtBox.Enabled = False
                    txtBox.Text = ""
                    txtBox.TextMode = TextBoxMode.Password
                    txtBox.BackColor = Color.Gray
                End If
                PanelBingo.Controls.Add(txtBox)
                PanelBingo.Controls.Add(GetLiteral("</TD>"))
            Next
            PanelBingo.Controls.Add(GetLiteral("</TR>"))
        Next
        PanelBingo.Controls.Add(GetLiteral("</TR>"))
        PanelBingo.Controls.Add(GetLiteral("</Table>"))
        ss.SetSession("BINGOKEY", _hastTable)
    End Sub

    Private Function IsSelectedBingo(ByVal cordX As Integer, ByVal cordY As Integer, ByVal value As String(), ByVal matricX As Integer, ByVal matricY As Integer) As Boolean
        For Each item As String In value
            Dim temp As String = ConvertToCoordinat(matricX, matricY, item)
            Dim tempSplited As String() = temp.Split("-".ToCharArray)
            If cordX = tempSplited(0) And cordY = tempSplited(1) Then
                Return True
                Exit For
            End If
        Next
        Return False
    End Function

    Private Function GetLiteral(ByVal text As String)
        Dim rv As Literal
        rv = New Literal
        rv.Text = text
        GetLiteral = rv
    End Function

    Private Function ReadUserEntryKeys() As Hashtable
        Dim ht As Hashtable = CType(ss.GetSession("BINGOKEY"), Hashtable)
        Dim newHT As Hashtable = New Hashtable(ht.Count)
        If Not ht Is Nothing Then
            For Each key As String In ht.Keys
                newHT.Add(key, CType(Me.FindControl(key), TextBox).Text.ToUpper.Trim)
            Next
        End If
        Return newHT
    End Function

    Private Sub GotoMainMenu()
        Dim objDealer As Dealer = CType(ss.GetSession("DEALER"), Dealer)
        Dim UrlGeneral As String = "default_general.aspx?type=0"
        Dim UrlEULA As String = "frmUELA.aspx?type=0"
        Dim UrlEULA2 As String = "frmUELA.aspx?type=1"

        LogTosyslog("user authenticate successfully on 2nd auth page", "process-completed", "success", "web-security", "entry")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Response.Redirect(UrlEULA)
            'RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlEULA + """)</script>")
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
            'RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlEULA2 + """)</script>")
            Response.Redirect(UrlEULA2)
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Response.Redirect(UrlGeneral)
            'RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlGeneral + """)</script>")
        End If
    End Sub

    Private Function CompareKey(ByVal ht As Hashtable) As Boolean
        Dim result As Boolean = True
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUser Is Nothing Then
            Dim valid As Boolean = True
            Dim _bingo As KTB.DNet.Domain.Bingo
            If Not objUser.UserProfile Is Nothing Then
                If Not objUser.UserProfile.Bingo Is Nothing Then
                    _bingo = objUser.UserProfile.Bingo
                Else
                    Return False
                End If
            Else
                Return False
            End If
            If Not _bingo Is Nothing Then
                For Each item As String In ht.Keys
                    Dim val As String = ht.Item(item)
                    If IsBingoValid(item, val, _bingo.BingoMatrixs) = False Then
                        Return False
                    End If
                Next
            End If
        Else
            Return False
        End If
        Return result
    End Function

    Private Function IsBingoValid(ByVal key As String, ByVal val As String, ByVal matrix As ArrayList) As Boolean
        Dim keys() As String = key.Split("-")
        Dim x As Integer = keys(0)
        Dim y As Integer = keys(1)
        Dim valCode As String = String.Empty
        For Each item As BingoMatrix In matrix
            If item.PosisiX = x And item.PosisiY = y Then
                valCode = item.Code
                Exit For
            End If
        Next
        Return DNetEncryption.VerifyHash(val, "SHA512", valCode)
    End Function

    Private Function RetriveBingo() As KTB.DNet.Domain.Bingo
        Dim _bingoFacade As BingoFacade = New BingoFacade(User)
        Dim listBingo As ArrayList = _bingoFacade.RetrieveList()
        If listBingo.Count > 0 Then
            Return CType(listBingo(0), KTB.DNet.Domain.Bingo)
        Else
            Return Nothing
        End If
    End Function

    Private Sub btnLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Session.RemoveAll()
        FormsAuthentication.SignOut()
        Response.Redirect("login.aspx")
        'RegisterStartupScript("OpenNewWindow", "<script>OpenNewWindow('login.aspx')</script>")
    End Sub

    Private Sub btnProsess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProsess.Click
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)

        Session.Add("BingoLoginStatus", False)
        ' Session("BingoLoginStatus") = False
        If Not CheckUserSession(objUser) Then
            Session.RemoveAll()
            Response.Redirect("login.aspx")
        End If
        If Not isUserLock(objUser) Then
            htKeys = ReadUserEntryKeys()
            If Not htKeys Is Nothing Then
                If CompareKey(htKeys) Then
                    UpdateLastLogin()
                    Session("BingoLoginStatus") = True
                    GotoMainMenu()
                Else
                    If Not CountLoginFailed(objUser) Then
                        MessageBox.Show("Token yang anda masukan tidak sesuai")
                    End If
                End If
            End If
        Else
            'MessageBox.Show("Login gagal. Akses Login User telah terkunci")
            Dim strmess As String = "Login gagal. Akses Login User telah terkunci"
            Response.Redirect("frmForgetPasswordConfirmation.aspx?msg2Auth=" & strmess)
        End If
    End Sub

    Private Function CheckUserSession(ByVal objUser As UserInfo) As Boolean
        If Not objUser Is Nothing Then
            If Not objUser.UserProfile Is Nothing Then
                Return True
            End If
        End If
        Return False
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

    Private Sub UpdateLastLogin()
        Dim objUserInfoFacade As New UserInfoFacade(User)
        Dim objuserinfo As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        objuserinfo.LastLogin = DateTime.Now
        objUserInfoFacade.Update(objuserinfo)
    End Sub

    Private Sub SendEmail(ByVal emailTo As String, ByVal emailFrom As String, ByVal contentEmail As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet]", Mail.MailFormat.Html, contentEmail)
    End Sub

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
            End If
            LogTosyslog("Login failure. user access has been locked", "user-lockout", "failed", "web-security", "read")
        End If

    End Sub

    Private Function CountLoginFailed(ByVal objUserInfo As UserInfo) As Boolean
        Dim result As Boolean = False
        If Not objUserInfo Is Nothing Then
            Dim objSessionHelper As SessionHelper = New SessionHelper
            Dim count As Integer = 0
            Try
                count = CInt(objSessionHelper.GetSession("BINGOCOUNT"))
                count += 1
                objSessionHelper.SetSession("BINGOCOUNT", count)
                If count >= 3 Then
                    'MessageBox.Show("Login gagal. Akses Login User telah terkunci")
                    LockUser(objUserInfo)
                    '----------Send Email------------'
                    Dim emailTo As String = KTB.DNet.Lib.WebConfig.GetValue("EmailAdminDNET")
                    Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
                    Dim msgEmailContent As String = enumSMS.GetContentEmail(enumSMS.ContentMessage.TokenFail, objUserInfo).ToString
                    SendEmail(emailTo, emailFrom, msgEmailContent)

                    objSessionHelper.RemoveSession("BINGOCOUNT")

                    Dim strmess As String = "Login gagal. Akses Login User telah terkunci"
                    Response.Redirect("frmForgetPasswordConfirmation.aspx?msg2Auth=" & strmess)

                    result = True
                End If

            Catch ex As Exception
                count = 0
            End Try
        End If
        'LogTosyslog("User : " & User.Identity.Name & " gagal login ke dua")
        LogTosyslog("Invalid token was entered", "token-valid-not", "failed", "web-security", "entry")
        Return result
    End Function

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
            m.BlockName = "login-second"

            Try
                m.UserName = IIf(User.Identity.Name.Length > 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name).ToLower
            Catch ex As Exception
                m.UserName = "Wb-Usr"
            End Try
            m.Dealer = CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode.ToLower
            objSyslog.LogError(m)
        End If
    End Sub


End Class
