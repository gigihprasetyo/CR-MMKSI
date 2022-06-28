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
Imports System.IO
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic
Imports System.Linq

Public Class Login
    Inherits System.Web.UI.Page
    Private objUserInfo As UserInfo
    Private cooValue As String
    Private clientCookies As String
    Protected WithEvents PCImage As System.Web.UI.WebControls.Image
    Protected WithEvents DealerTextBox As System.Web.UI.WebControls.TextBox
    Protected WithEvents NameTextBox As System.Web.UI.WebControls.TextBox
    Protected WithEvents PasswordTextBox As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
    Protected WithEvents pnlAnnouncement As System.Web.UI.WebControls.Panel
    Protected WithEvents lblAnnouncement As System.Web.UI.WebControls.Label
    Protected WithEvents LtrTime As System.Web.UI.WebControls.Literal
    Protected WithEvents lblScript As System.Web.UI.WebControls.Label
    Private CookieName = KTB.DNet.Lib.WebConfig.GetValue("DNETPhisingGuard")


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LoginButton As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents lbtnForget As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkImage As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnDownloadUserManualLogin As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblDonwloadPath As System.Web.UI.WebControls.Label
    'Private authenticationProvider As IAuthenticationProvider
    'Private rolesProvider As IRolesProvider
    Private identity As IIdentity

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub RedirectPage()
        If KTB.DNet.Lib.WebConfig.GetValue("RedirectTrue").ToString.ToLower().Equals("true") Then
            Dim StrBlockDomain As String = KTB.DNet.Lib.WebConfig.GetValue("RedirectFrom").ToString.ToLower()
            Dim StrRedirectTo As String = KTB.DNet.Lib.WebConfig.GetValue("RedirectTo").ToString.ToLower()
            Dim hostname As String = Request.Url.Host



            Dim listBLockDomain As New List(Of String)()
            For Each Str As String In StrBlockDomain.Split(";")
                listBLockDomain.Add(Str)
            Next



            If listBLockDomain.Contains(hostname) Then
                System.Web.HttpContext.Current.Response.Redirect(StrRedirectTo)
            End If
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Response.AppendHeader("X-FRAME-OPTIONS", "SOMEORIGIN")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If _isValidBrowser() Then
            RedirectPage()
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            If Not Page.IsPostBack Then
                LoginButton.Attributes.Add("onclick", "return HandleValidator();")
                If Request.QueryString("msg") = "Expired" Then
                    lblMessage.Text = "Maaf, Session habis atau Anda belum login. Silakan login kembali"
                End If
                If Request.QueryString("msg") = "kick" Then
                    lblMessage.Text = "Ada user yang sedang login menggunakan account Anda"
                End If

                Try
                    Dim objPC As PCPhisingGuard = RetrieveImage()
                    If Not objPC Is Nothing Then
                        LtrTime.Visible = False
                        PCImage.ImageUrl = "WebResources\GetPCImage.aspx"
                    Else
                        PCImage.Visible = False
                        DisplayTime()
                    End If
                Catch ex As Exception

                End Try
            End If

        Else
            Dim errorConf As String = KTB.DNet.Lib.WebConfig.GetValue("INVALIDBROWSERMESSAGE")
            Dim errorMessage As String = errorConf & ", Browser anda : " & browserName.ToString & ", dengan tipe : " & browserType.ToString & " Versi : " & browserVersion
            If Not minimumVersion = 0 Then
                errorMessage = errorMessage & ". Harap gunakan browser " & browserName.ToString & " dengan versi minimal " & minimumVersion
            End If
            Response.Write(errorMessage)
            Response.End()
            Response.Close()
        End If

        DisplayAnnouncement()

    End Sub

    Private Function RetrieveImage() As PCPhisingGuard
        Dim loop1 As Integer
        Dim arr1() As String
        Dim MyCookieColl As HttpCookieCollection
        Dim MyCookie As HttpCookie

        MyCookieColl = Request.Cookies
        arr1 = MyCookieColl.AllKeys
        ' Grab individual cookie objects by cookie name     
        For loop1 = 0 To arr1.GetUpperBound(0)
            MyCookie = MyCookieColl(arr1(loop1))
            If MyCookie.Name = CookieName Then
                clientCookies = MyCookie.Value.ToString
            End If
        Next
        Dim objPCPHisingGuard As PCPhisingGuard = RetrievePCPhisingGuard(clientCookies)
        Return objPCPHisingGuard
    End Function

    Private Function RetrievePCPhisingGuard(ByVal CookiesValue As String) As PCPhisingGuard
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PCPhisingGuard), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PCPhisingGuard), "CookiesValue", MatchType.Exact, CookiesValue))
        Dim ArlPCPhisingGuard As ArrayList = New PCPhisingGuardFacade(Nothing).Retrieve(criterias)
        If ArlPCPhisingGuard.Count > 0 Then
            Return CType(ArlPCPhisingGuard(0), PCPhisingGuard)
        Else
            Return Nothing
        End If
    End Function

    Private Sub DisplayTime()
        Dim strTime As String = ""
        Dim minute As String = Now.Minute
        If minute.Length = 1 Then
            minute = "0" & minute
        End If
        strTime = "<br><FONT size=3>" & Now.Date.ToString("dd MMM").ToUpper & "</FONT><br><br>"
        strTime &= Now.Hour & " : " & minute & " WIB<br>"
        LtrTime.Text = strTime
    End Sub

    Private Sub DisplayAnnouncement()
        Dim success As Boolean = False
        Dim Connect As Boolean = False
        Dim sr As StreamReader
        Dim filename = "Announcement.html"
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("AnnouncementPath") & "\" & filename
        If (Connect = False) Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim finfo As New FileInfo(DestFile)
            Try
                success = imp.Start()
                If success Then
                    If finfo.Exists Then
                        sr = New StreamReader(DestFile)
                        Try
                            Dim Announcement As String = sr.ReadToEnd
                            lblAnnouncement.Text = Announcement.Replace(CommonFunction.GetPageBreakFCKeditor, "<br>")
                            sr.Close()
                        Catch ex As Exception
                            sr.Close()
                        End Try
                    End If
                    Connect = True
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception
                Throw ex
                Exit Sub
            End Try
        End If

    End Sub
    Private browserName As String = ""
    Private browserType As String = ""
    Private browserVersion As String = ""
    Private minimumVersion As Double = 0
    Private invalidType As Boolean = False

    Private Function IsValidBrowser() As Boolean
        Dim rejectedBrowser As String = KTB.DNet.Lib.WebConfig.GetValue("VALIDBROWSER").ToUpper()
        Dim rejectedBrowserType As String = KTB.DNet.Lib.WebConfig.GetValue("INVALIDBROWSERTYPE").ToUpper()
        Dim acceptedBrowserType As String = KTB.DNet.Lib.WebConfig.GetValue("VALIDBROWSERTYPE").ToUpper()
        Dim ctx As HttpContext = HttpContext.Current
        browserName = ctx.Request.Browser.Browser
        browserType = ctx.Request.Browser.Type
        Dim b As String = getBrowser(ctx.Request.Browser.Capabilities("").ToString().ToUpper())(0)

        For Each item As String In rejectedBrowser.Trim.Split(";")
            If (browserName.ToUpper.Trim = item.ToUpper.Trim) Then
                For Each rejectItem As String In rejectedBrowserType.Trim.Split(";")
                    If browserType.Trim.ToUpper = rejectItem.Trim.ToUpper Then
                        Return False
                    End If
                Next
                For Each acceptItem As String In acceptedBrowserType.Trim.Split(";")
                    If acceptItem.Trim.ToUpper = browserType.Trim.ToUpper Then
                        Return True
                    End If
                Next
                Return False
            End If
        Next
        Return False
    End Function

    Private Function _isValidBrowser() As Boolean
        'Dim allowedBrowser As String = KTB.DNet.Lib.WebConfig.GetValue("ALLOWEDBROWSER").ToUpper()
        'Dim rejectedBrowser As String = KTB.DNet.Lib.WebConfig.GetValue("VALIDBROWSER").ToUpper()
        'Dim rejectedBrowserType As String = KTB.DNet.Lib.WebConfig.GetValue("INVALIDBROWSERTYPE").ToUpper()
        Dim allowedBrowser As String() = KTB.DNet.Lib.WebConfig.GetValue("BROWSERVERSION").ToUpper().Trim.Split(";")
        Dim acceptedBrowserType As String = KTB.DNet.Lib.WebConfig.GetValue("VALIDBROWSERTYPE").ToUpper()
        Dim isUseVersion As Boolean = CBool(KTB.DNet.Lib.WebConfig.GetValue("ISUSEVERSION"))
        Dim ctx As HttpContext = HttpContext.Current
        Dim result As Boolean = False
        Dim browserIdentity As String() = getBrowser(ctx.Request.Browser.Capabilities("").ToString().ToUpper())
        browserVersion = browserIdentity(1)
        browserName = ctx.Request.Browser.Browser
        browserType = ctx.Request.Browser.Type

        For Each item As String In allowedBrowser
            If browserIdentity(0).ToUpper.Trim = item.Split("|")(0) Then
                result = True
                Double.TryParse(item.Split("|")(1), minimumVersion)
                Exit For
            End If
        Next

        If Not result Then
            For Each item1 As String In acceptedBrowserType.Trim.Split(";")
                If (browserType.ToUpper.Trim = item1.ToUpper.Trim) Then
                    result = True
                    Exit For
                End If
            Next
        End If

        If isUseVersion And result Then
            If minimumVersion = 0 Then
                result = True
            Else
                Dim userVersion As Double = 0
                Double.TryParse(browserVersion.Replace(".", ","), userVersion)
                If userVersion < minimumVersion Then
                    result = False
                End If
            End If
        End If

        Return result
    End Function

    Private Function getBrowser(ByVal userAgentStr As String) As String()
        Dim version As String = String.Empty
        Try
            Dim stage1 As Boolean = userAgentStr.Contains("MOZILLA")
            If stage1 Then
                If userAgentStr.Contains("MSIE") Then
                    version = getVersion("MSIE", userAgentStr)
                    Return {"INTERNETEXPLORER", version}
                ElseIf userAgentStr.Contains("TRIDENT") Then
                    version = getVersion("TRIDENT", userAgentStr)
                    Return {"INTERNETEXPLORER", version}
                ElseIf userAgentStr.Contains("APPLEWEBKIT") Then
                    If userAgentStr.Contains("CHROME") Then
                        If userAgentStr.Contains("EDGE") Or userAgentStr.Contains("EDG") Then
                            version = getVersion("EDGE", userAgentStr)
                            If version = 0 Then
                                version = getVersion("EDG", userAgentStr)
                            End If
                            Return {"EDGE", version}
                        ElseIf userAgentStr.Contains("OPR") Then
                            version = getVersion("OPR", userAgentStr)
                            Return {"OPERA", version}
                        Else
                            version = getVersion("CHROME", userAgentStr)
                            Return {"CHROME", version}
                        End If
                    ElseIf userAgentStr.Contains("SAFARI") Then
                        If userAgentStr.Contains("CRIOS") Then
                            version = getVersion("CRIOS", userAgentStr)
                            Return {"CHROME", version}
                        End If
                        version = getVersion("SAFARI", userAgentStr)
                        Return {"SAFARI", version}
                    End If
                ElseIf userAgentStr.Contains("FIREFOX") Then
                    version = getVersion("FIREFOX", userAgentStr)
                    Return {"FIREFOX", version}
                End If
            End If
            Return {"UNKNOWN", "0.0"}
        Catch ex As Exception
            Return {"UNKNOWN", "0.0"}
        End Try
    End Function

    Private Function getVersion(ByVal strBrowser As String, ByVal userAgent As String) As String
        Try
            Dim loc As Integer = userAgent.IndexOf(strBrowser)
            Dim tempVersion As String = String.Empty
            If strBrowser = "TRIDENT" Then
                loc = userAgent.IndexOf("RV")
                For i As Integer = loc To userAgent.Length - 1
                    If userAgent(i) = ":" Then
                        For j As Integer = i + 1 To userAgent.Length - 1
                            If userAgent(j) = ")" Then
                                Exit For
                            End If
                            tempVersion += userAgent(j)
                        Next
                        Exit For
                    End If
                Next
            ElseIf strBrowser = "MSIE" Then
                loc = userAgent.IndexOf("MSIE")
                For i As Integer = loc To userAgent.Length - 1
                    If userAgent(i) = " " Then
                        For j As Integer = i + 1 To userAgent.Length - 1
                            If userAgent(j) = ";" Then
                                Exit For
                            End If
                            tempVersion += userAgent(j)
                        Next
                        Exit For
                    End If
                Next
            Else
                For i As Integer = loc To userAgent.Length - 1
                    If userAgent(i) = "/" Then
                        For j As Integer = i + 1 To userAgent.Length - 1
                            If userAgent(j) = " " Then
                                Exit For
                            End If
                            tempVersion += userAgent(j)
                        Next
                        Exit For
                    End If
                Next
            End If

            Dim dotCount As Int16 = 0
            For i As Integer = 0 To tempVersion.Length - 1
                If tempVersion(i) = "." Then
                    If dotCount = 1 Then
                        tempVersion = tempVersion.Substring(0, i)
                        Exit For
                    End If
                    dotCount = dotCount + 1
                End If
            Next
            Return tempVersion
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Private Function GotoMainMenu(ByVal objDealer As Dealer) As Boolean
        Dim UrlGeneral As String = "default_general.aspx?type=0"
        Dim UrlEULA As String = "frmUELA.aspx?type=0"
        Dim UrlEULA2 As String = "frmUELA.aspx?type=1"

        LogTosyslog("user login successfully", "process-completed", "success", "web-security", "entry")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlEULA + """)</script>")
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
            RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlEULA2 + """)</script>")
        Else
            RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlGeneral + """)</script>")
        End If
    End Function

    Private Sub LoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        If Not Page.IsValid Then
            Return
        End If
        Try
            'Dim x As Integer = CInt("dd")
            Dim objSessionHelper As New SessionHelper
            Dim LoginMode As String = String.Empty
            LoginMode = KTB.DNet.Lib.WebConfig.GetValue("LoginMode")
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(DealerTextBox.Text)
            If (Not objDealer Is Nothing) And (objDealer.ID > 0) Then
                Dim objInfoUser As New UserInfo
                If (Not objDealer.DealerCode = "") And (objDealer.Status = EnumDealerStatus.DealerStatus.Aktive) Then
                    If objDealer.OrganizationBranchType = CShort(EnumOrganizationBranchType.EnumOrgBranchType.DealerBranch) Or _
                        objDealer.OrganizationBranchType = CShort(EnumOrganizationBranchType.EnumOrgBranchType.e1S_Satelite) Or _
                        objDealer.OrganizationBranchType = CShort(EnumOrganizationBranchType.EnumOrgBranchType.TempOutlet) Then
                        objInfoUser = New UserInfoFacade(User).Retrieve(NameTextBox.Text, objDealer.ParentDealer.DealerCode)

                    Else
                        objInfoUser = New UserInfoFacade(User).Retrieve(NameTextBox.Text, DealerTextBox.Text)
                        If Not IsNothing(objInfoUser.DealerBranch) AndAlso objInfoUser.DealerBranch.ID > 0 Then
                            If Not CountLoginFailed(objUserInfo) Then
                                MessageBox.Show("User Name dan Passwordnya salah atau belum diaktifkan...!!")
                                LogTosyslog("wrong user name or wrong password ", "user-activated-not", "failed", "web-security", "read")
                            End If
                            Session("CountLogin") = IIf(IsNothing(Session("CountLogin")), 0, CType(Session("CountLogin"), Integer) + 1)
                        End If
                    End If


                    If Not IsNothing(Session("CountLogin")) AndAlso CType(Session("CountLogin"), Integer) = 3 Then
                        If Not objInfoUser Is Nothing Then
                            If LoginMode.ToUpper.Trim = "LIVE" Then
                                LockUser(objInfoUser)
                                '----------Send Email------------'
                                Dim emailTo As String = KTB.DNet.Lib.WebConfig.GetValue("EmailAdminDNET")
                                Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
                                Dim msgEmailContent As String = enumSMS.GetContentEmail(enumSMS.ContentMessage.LoginFail, objInfoUser).ToString
                                SendEmail(emailTo, emailFrom, msgEmailContent)
                            End If
                        End If
                        RegisterClientScriptBlock("WindowClose", "<script language=JavaScript>window.close();</script>")
                    Else
                        Dim pwdBytes As Byte() = Encoding.Unicode.GetBytes(PasswordTextBox.Text)
                        Dim strDealerID As String = objDealer.ID.ToString.PadLeft(6, "0")
                        Dim authenticated As Boolean = SecurityProvider.Authenticate(strDealerID & NameTextBox.Text.Trim, pwdBytes, identity)

                        'Bypass
                        Dim isByPass As Boolean = False
                        If Not authenticated Then
                            
                            If Not objInfoUser Is Nothing And objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                                If PasswordTextBox.Text.Trim = ByPassKey(objInfoUser.LastUpdateTime.ToString("ssfff")) Then
                                    authenticated = True
                                    isByPass = True
                                End If
                            End If
                        End If

                        'Is User Lock or Not
                        If LoginMode.ToUpper.Trim = "LIVE" Then
                            Dim finishLock As Date = Now
                            If isUserLock(objUserInfo, finishLock) Then
                                MessageBox.Show("Login gagal. Akses Login User telah terkunci. Silahkan coba lagi setelah pukul " & finishLock.AddMinutes(1).ToString("HH:mm"))
                                Return
                            End If
                        End If
                        If objDealer.OrganizationBranchType = CShort(EnumOrganizationBranchType.EnumOrgBranchType.DealerBranch) Or _
                                objDealer.OrganizationBranchType = CShort(EnumOrganizationBranchType.EnumOrgBranchType.e1S_Satelite) Or _
                                objDealer.OrganizationBranchType = CShort(EnumOrganizationBranchType.EnumOrgBranchType.TempOutlet) Then
                            objUserInfo = New UserInfoFacade(User).Retrieve(NameTextBox.Text, objDealer.ParentDealer.DealerCode)
                        Else
                            objUserInfo = New UserInfoFacade(User).Retrieve(NameTextBox.Text, DealerTextBox.Text)
                        End If
                        If authenticated And objUserInfo.UserStatus = EnumUserStatus.UserStatus.Aktif Then
                            If isByPass Then
                                Response.Cookies.Add(FormsAuthentication.GetAuthCookie(strDealerID & NameTextBox.Text.Trim, False))
                            Else
                                Response.Cookies.Add(FormsAuthentication.GetAuthCookie(identity.Name, False))
                            End If
                            'validity bingo
                            Dim StatusBingo As Boolean
                            If Not IsNothing(objUserInfo) Then
                                If Not IsNothing(objUserInfo.UserProfile) Then

                                    If Not objUserInfo.UserProfile.Bingo Is Nothing Then
                                        If objUserInfo.UserProfile.Bingo.IsValidBingo Then
                                            StatusBingo = True
                                        End If
                                    Else
                                        If LoginMode.ToUpper.Trim = "LIVE" Then
                                            If (objUserInfo.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register) And (objUserInfo.UserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active) Then
                                                MessageBox.Show("Token Anda tidak terdaftar, Hubungi Admin D-NET")
                                                LogTosyslog("token is not registered, contact D-NET administrators", "token-found-not", "failed", "web-security", "read")
                                                Return
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            If (LoginMode.Trim.ToUpper <> "LIVE" Or isByPass) Then
                                If isByPass Then
                                    Dim intResult As Integer = New UserInfoFacade(User).Update(objUserInfo)
                                    Session.Add("BingoLoginStatus", True)
                                End If
                                GotoMainMenu(objDealer)
                            End If
                            If (LoginMode.Trim.ToUpper = "INITIAL") Or (LoginMode.Trim.ToUpper = "LIVE") Then
                                If (Not objUserInfo.UserProfile Is Nothing) Then
                                    If objUserInfo.UserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active And objUserInfo.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register And objUserInfo.UserProfile.TransitionHP.Trim = String.Empty Then
                                        If LoginMode.ToUpper.Trim = "LIVE" Then
                                            Dim UrlUserProfile As String = "frmSecondAuth.aspx"
                                            RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlUserProfile + """)</script>")
                                        Else
                                            GotoMainMenu(objDealer)
                                        End If
                                    ElseIf objUserInfo.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.NotRegister Then
                                        If LoginMode.Trim.ToUpper = "LIVE" Then
                                            Dim UrlUserProfile As String = "frmSecondLogin.aspx"
                                            RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlUserProfile + """)</script>")
                                        Else
                                            GotoMainMenu(objDealer)
                                        End If
                                    Else
                                        Dim expCount As Integer
                                        Try
                                            expCount = CInt(KTB.DNet.Lib.WebConfig.GetValue("ActivationCodeExpiredTime"))
                                        Catch ex As Exception
                                            expCount = 1
                                        End Try
                                        Dim sentActStatus As DateTime = objUserInfo.UserProfile.ActivationSentTime
                                        Dim tmpDate As DateTime = sentActStatus.AddDays(expCount)
                                        If LoginMode.ToUpper.Trim = "LIVE" Then
                                            If tmpDate >= Now Then
                                                MessageBox.Show("Silahkan aktifkan kode aktivasi melalui HP")
                                                LogTosyslog("user need to re-send the activation code using their handphone", "activation-required", "failed", "web-security", "read")
                                            Else
                                                Dim objProfile As UserProfile = objUserInfo.UserProfile
                                                If objProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active Then
                                                    objProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register
                                                    objProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active
                                                    objProfile.TransitionActivationCode = String.Empty
                                                    objProfile.TransitionHP = String.Empty
                                                    Dim objProfileFacade As UserProfileFacade = New UserProfileFacade(User)
                                                    objProfileFacade.Update(objProfile)
                                                    Dim UrlUserProfile As String = "frmSecondAuth.aspx?isExpired=true"
                                                    RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlUserProfile + """)</script>")
                                                Else
                                                    objProfile.RegistrationStatus = EnumSE.RegistrationStatus.NotRegister
                                                    objProfile.ActivationStatus = EnumSE.ActivationCodeStatus.NotActive
                                                    objProfile.ActivationCode = String.Empty
                                                    objProfile.TransitionActivationCode = String.Empty
                                                    objProfile.TempActivationCode = String.Empty
                                                    objProfile.TransitionHP = String.Empty
                                                    Dim objProfileFacade As UserProfileFacade = New UserProfileFacade(User)
                                                    objProfileFacade.Update(objProfile)
                                                    Dim UrlUserProfile As String = "frmSecondLogin.aspx?isExpired=true"
                                                    RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlUserProfile + """)</script>")
                                                End If
                                            End If
                                        Else
                                            GotoMainMenu(objDealer)
                                        End If
                                    End If
                                Else
                                    If LoginMode.Trim.ToUpper = "LIVE" Then
                                        Dim UrlSecondAuthenticate As String = "frmSecondLogin.aspx"
                                        Session.Add("INTIAL_LOGIN", "1")
                                        RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlSecondAuthenticate + """)</script>")
                                    Else
                                        GotoMainMenu(objDealer)
                                    End If
                                End If
                            End If
                            If Not IsNothing(objUserInfo.DealerBranch) AndAlso objUserInfo.DealerBranch.ID > 0 Then
                                objDealer.ParentDealer.IsBranch = True
                                objDealer.ParentDealer.BranchCode = objDealer.DealerCode
                                objDealer = objDealer.ParentDealer
                            End If

                            objSessionHelper.SetSession("DEALER", objDealer)
                            objSessionHelper.SetSession("LOGINUSERINFO", objUserInfo)
                            UpdateLastLoginInfo(LoginMode.ToUpper)
                            CheckingPendingUpdate(objUserInfo)
                        Else
                            If Not CountLoginFailed(objUserInfo) Then
                                MessageBox.Show("User Name dan Passwordnya salah atau belum diaktifkan...!!")
                                LogTosyslog("wrong user name or wrong password ", "user-activated-not", "failed", "web-security", "read")
                            End If
                            Session("CountLogin") = IIf(IsNothing(Session("CountLogin")), 0, CType(Session("CountLogin"), Integer) + 1)
                        End If

                    End If
                Else
                    If Not CountLoginFailed(objUserInfo) Then
                        MessageBox.Show("Dealer tidak ditemukan atau tidak aktif.")
                        LogTosyslog("dealer code cant be found or not yet active ", "dealer-found-not", "failed", "web-security", "read")
                    End If
                End If
            Else
                If Not CountLoginFailed(objUserInfo) Then
                    MessageBox.Show("Dealer tidak ditemukan atau tidak aktif.")
                    LogTosyslog("dealer code cant be found or not yet active ", "dealer-found-not", "failed", "web-security", "read")
                End If
            End If
        Catch ex As Exception
            CountLoginFailed(objUserInfo)
            LogError(ex)
            Dim errorMsg As String = ConvertErrorCode(ex)
            Response.Redirect("errorCode.aspx?error=" & errorMsg)
        End Try
    End Sub

    Private Sub SendSMS(ByVal hp As String, ByVal message As String)
        'Dim _sms As KTB.DNet.Lib.Sms = New KTB.DNet.Lib.Sms
        '_sms.Sendto(hp, message)

        Dim otpfunc As New OTPFunction

        otpfunc.SendSMSNotif(hp, message)
        If (Not otpfunc.boolReturn) Then
            'MessageBox.Show("Pengiriman SMS Gagal. Silahkan Menghubungi Administrator Anda")
            'Return
        End If
    End Sub

    Private Sub CheckingPendingUpdate(ByVal objUserInfo As UserInfo)
        Dim objUserProfile As UserProfile = objUserInfo.UserProfile
        If Not objUserProfile Is Nothing Then
            If objUserProfile.TransitionHP.Trim <> String.Empty And objUserProfile.TransitionActivationCode.Trim <> String.Empty Then
                Dim newDate As DateTime = objUserProfile.TransitionProcessDate.AddHours(24)
                If newDate < Now Then
                    Dim hpBaru As String = objUserProfile.TransitionHP
                    objUserProfile.TransitionHP = String.Empty
                    objUserProfile.TransitionActivationCode = String.Empty
                    objUserProfile.TransitionProcessDate = Now
                    Try
                        Dim i As Integer = New UserProfileFacade(User).Update(objUserProfile)
                        SendSMS(objUserInfo.HandPhone, "Transisi HP Baru " & hpBaru & " anda dibatalkan sebab sudah melewati batasan aktivasi 24 jam. No HP " & objUserInfo.HandPhone & " yang valid.")
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If
    End Sub

    Private Function isUserLock(ByVal objUser As UserInfo, ByRef finishLock As Date) As Boolean
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
                        finishLock = item.FinishLock
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function

    Private Function GetErrorCode(ByVal _exception As String) As String
        Dim _errorMapper As ErrorMapper = New ErrorMapper
        Dim Code As String = _errorMapper.GetErrorCode(_exception)

        If Not Code = String.Empty Then
            Return Code
        Else
            Return "0"
        End If

        If Not _errorMapper Is Nothing Then
            _errorMapper = Nothing
        End If

    End Function

    Private Function ConvertErrorCode(ByVal e As Exception) As String
        Dim code As String = "99999"
        If Not e Is Nothing Then
            Dim exceptionType As String = e.GetType.ToString
            LogError(e)
            Dim _code = GetErrorCode(exceptionType)
            If TypeOf e Is System.Data.SqlClient.SqlException Then
                Dim sqlEx As SqlClient.SqlException = CType(e, SqlClient.SqlException)
                If sqlEx.Number = 18456 Then
                    _code = _code & " : Login to Database failed"
                Else
                    _code = _code & " : Database Unavailable"
                End If
            End If
            If _code <> "0" Then
                code = _code
            End If
        End If
        Return code
    End Function

    Private Sub LogError(ByVal e As Exception)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        If imp.Start() Then
            Try
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Application Policy")
            Catch ex As Exception
                Dim str As String = ex.Message
            Finally
                If Not imp Is Nothing Then
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            End Try
        End If
    End Sub

    Private Sub UpdateLastLoginInfo(ByVal loginMod As String)
        If loginMod = "INITIAL" Or loginMod = "LIVE" Then
            DoUpdateProfile(objUserInfo)
        End If
    End Sub

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

    Private Sub lbtnForget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnForget.Click
        Response.Redirect("frmForgetPasswordNext.aspx")
    End Sub

    Private Sub lnkImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkImage.Click
        Response.Redirect("frmPCPhisingGuard.aspx")
    End Sub

    Private Sub SendEmail(ByVal emailTo As String, ByVal emailFrom As String, ByVal contentEmail As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Try
            ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet]", Mail.MailFormat.Html, contentEmail)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LogTosyslog(ByVal message As String, ByVal sbMsg As String, ByVal rslMsg As String, Optional ByVal mdlmsg As String = "web-security", Optional ByVal sbAction As String = "entry")
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
            m.BlockName = "login-first"

            m.UserName = NameTextBox.Text.ToLower
            m.Dealer = DealerTextBox.Text.ToLower
            objSyslog.LogError(m)
        End If
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
        End If
        'LogTosyslog("User " & User.Identity.Name & " terkunci")
        LogTosyslog("too many login failure. user will be locked for several minutes", "user-lockout", "failed", "web-security", "entry")

    End Sub

    Private Function CountLoginFailed(ByVal objUserInfo As UserInfo) As Boolean
        Dim result As Boolean = False
        Dim count As Integer = 0
        Dim _LoginMode As String = String.Empty
        _LoginMode = KTB.DNet.Lib.WebConfig.GetValue("LoginMode")


        If Not objUserInfo Is Nothing Then
            Dim objSessionHelper As SessionHelper = New SessionHelper
            Try
                If _LoginMode.Trim.ToUpper = "LIVE" Then
                    count = CInt(objSessionHelper.GetSession("LOGINCONT"))
                Else
                    count = 0
                End If

                count += 1
                If count >= 3 Then
                    MessageBox.Show("Login gagal. Akses Login User telah terkunci. Silahkan login kembali setelah 15 menit.")
                    LockUser(objUserInfo)
                    count = 0
                    '----------Send Email------------'
                    Dim emailTo As String = KTB.DNet.Lib.WebConfig.GetValue("EmailAdminDNET")
                    Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
                    Dim msgEmailContent As String = enumSMS.GetContentEmail(enumSMS.ContentMessage.LoginFail, objUserInfo).ToString
                    SendEmail(emailTo, emailFrom, msgEmailContent)
                    objSessionHelper.RemoveSession("LOGINCONT")
                    result = True
                End If
                objSessionHelper.SetSession("LOGINCONT", count)
            Catch ex As Exception
                count = 0
            End Try
        End If
        'LogTosyslog("User " & User.Identity.Name & " gagal login.")

        Return result
    End Function

    Private Function ByPassKey(ByVal strSource As String) As String
        Dim result As String = ""
        Dim strChar As String = ""

        For i As Integer = 0 To strSource.Length - 1
            strChar = strSource.Substring(i, 1)
            If i Mod 2 = 0 Then
                strChar = Chr(Asc(strChar) + i + 20)
            Else
                strChar = Chr(Asc(strChar) - i + 20)
            End If
            result &= strChar
        Next

        Return result

    End Function


    Protected Sub lbtnDownloadUserManualLogin_Click(sender As Object, e As EventArgs) Handles lbtnDownloadUserManualLogin.Click
        Response.Redirect("General/DownloadWithoutLogIn.aspx")
    End Sub

End Class
