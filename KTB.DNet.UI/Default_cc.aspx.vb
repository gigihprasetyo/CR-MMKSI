#Region "Imports Statement"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports System.Web.Security
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.AlertManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports system.Configuration.ConfigurationSettings
#End Region

Public Class default_cc
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents GeneralLinkButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents SalesLinkButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ServiceLinkButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents SparepartLinkButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents CalenderLabel As System.Web.UI.WebControls.Label
    Protected WithEvents LogoutLinkButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents MyMenu As KTB.DNet.Menu.TreeMenu
    Protected WithEvents labelUser As System.Web.UI.WebControls.Label
    Protected WithEvents LabelDealer As System.Web.UI.WebControls.Label
    Protected WithEvents LabelSearchTerm As System.Web.UI.WebControls.Label
    Protected WithEvents UbahDataLinkButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents LabelNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lnkSecurity As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblvaliduntil As System.Web.UI.WebControls.Label
    Protected WithEvents imgStar As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents RsdLinkButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents PromoLinkButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents AfterSalesLinkButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents MrkLinkButton As System.Web.UI.WebControls.LinkButton
    Protected WithEvents pnlEmailValidator As System.Web.UI.WebControls.Panel
    Protected WithEvents LnkTerms As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnCallCenter As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnTraining As System.Web.UI.WebControls.LinkButton
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        'Email Validation
        Dim objUserInfo As UserInfo = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If objUserInfo.EmailValidation <> 1 Then
            Dim objEmailValidator As UserEmailValidation = New UserEmailValidation
            objEmailValidator.GenerateEmailValidationButton(pnlEmailValidator, User)
        End If
        'End Email Validation

    End Sub

#End Region

#Region "Private Declaration"
    Private containerPage As default_cc
    Private _menuMapper As MenuMapper
    Private _menuPage As String = String.Empty
    Private _errorpage As String = "error.html"
    Private sessionHelper As New SessionHelper
#End Region

#Region "Public Declaration"
    Public MainFrameSrc As String = "Welcome.aspx?modcat=callcenter"
#End Region

#Region "Event Handler"
    Private Sub IsBingoNoExpiration()
        If User.Identity.Name <> String.Empty Then
            Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
            If objDealer Is Nothing Then
                objDealer = New Dealer
            End If
            Dim objUserInfo As UserInfo = sessionHelper.GetSession("LOGINUSERINFO")
            If Not objUserInfo.UserProfile Is Nothing Then
                If Not objUserInfo.UserProfile.Bingo Is Nothing Then
                    If objUserInfo.UserProfile.Bingo.ExpiredCount < 0 Then
                        Label1.Visible = False
                        lblvaliduntil.Visible = False
                        imgStar.Visible = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objUser As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER.LEASING Then
            LnkTerms.Attributes.Add("onclick", "window.open('euladsf.html','disclaimer','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=700,height=500'); return false;")
        Else
            LnkTerms.Attributes.Add("onclick", "window.open('eula2.html','disclaimer','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=700,height=500'); return false;")
        End If
        _Default.CheckBingoLogin()
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If User.Identity.Name Is Nothing Then
            dologout()
            Return
        End If
        If User.Identity.Name.Trim = "" Then
            dologout()
            Return
        End If


        If Not Page.IsPostBack Then
            Initialization()
            IsBingoNoExpiration()
            If Not Request.QueryString("screenid") = Nothing Then
                MyMenu.ScreenId = Integer.Parse(Request.QueryString("screenid"))
                DisplayTreeMenu()
            End If
        End If
        CheckCallCenter()
    End Sub


    Private Sub SalesLinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesLinkButton.Click
        sessionHelper.RemoveAllExceptLoginData()
        Response.Redirect("default.aspx")
    End Sub

    Private Sub GeneralLinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GeneralLinkButton.Click
        sessionHelper.RemoveAllExceptLoginData()
        Response.Redirect("default_general.aspx")
    End Sub
    Private Sub dologout()
        LogTosyslog("user logout successfully", "logout-service", "success")
        Session.RemoveAll()
        FormsAuthentication.SignOut()
        RegisterStartupScript("OpenNewWindow", "<script>OpenNewWindow('login.aspx')</script>")
    End Sub
    Private Sub LogoutLinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutLinkButton.Click
        dologout()
    End Sub

    Private Sub SparepartLinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SparepartLinkButton.Click
        sessionHelper.RemoveAllExceptLoginData()
        Response.Redirect("default_parts.aspx")
    End Sub

    Private Sub ServiceLinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServiceLinkButton.Click
        'Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        'If Not objDealer Is Nothing Then
        '    If objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
        '        SetTreeMenu("TreeMenu.XML.PathFromRootFreeService", 9000)
        '    Else
        '        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
        '            SetTreeMenu("TreeMenu.XML.PathFromRootCallCenter", 9000)
        '        End If
        '    End If
        'End If
        sessionHelper.RemoveAllExceptLoginData()
        Response.Redirect("default_service.aspx")
    End Sub

    Private Sub UbahDataLinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UbahDataLinkButton.Click
        MainFrameSrc = "UserManagement/frmUserInfoDetail.aspx?type=2"
    End Sub

#End Region

#Region "Private Method"

    Private Sub SetModule(ByVal objDealer As Dealer)
        RsdLinkButton.Enabled = False
        MrkLinkButton.Enabled = False
        PromoLinkButton.Enabled = False
        SalesLinkButton.Enabled = False
        ServiceLinkButton.Enabled = False
        SparepartLinkButton.Enabled = False
        If objDealer.SalesUnitFlag = 1 Then
            SalesLinkButton.Enabled = True
            RsdLinkButton.Enabled = True
            MrkLinkButton.Enabled = True
            PromoLinkButton.Enabled = True
        End If
        If objDealer.ServiceFlag = 1 Then
            ServiceLinkButton.Enabled = True
        End If
        If objDealer.SparepartFlag = 1 Then
            SparepartLinkButton.Enabled = True
        End If
    End Sub

    Private Sub Initialization()
        sessionHelper.RemoveAllExceptLoginData()
        CalenderLabel.Text = CType(DateTime.Now.DayOfWeek, LookUp.EnumHari).ToString & ", " & DateTime.Now.Day & " " & CType(DateTime.Now.Month, LookUp.EnumBulan).ToString & " " & DateTime.Now.Year
        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        Dim objUserInfo As New UserInfo
        If Not objDealer Is Nothing Then
            If User.Identity.Name <> String.Empty Then
                objUserInfo = sessionHelper.GetSession("LOGINUSERINFO")
                labelUser.Text = objUserInfo.FirstName & ","
            End If
            SetModule(objDealer)
            lblStatus.Text = "Status : " & objUserInfo.RegistrationStatus
            LabelNamaDealer.Text = objDealer.DealerName
            LabelSearchTerm.Text = objDealer.SearchTerm1 & " / " & objDealer.SearchTerm2
            LabelDealer.Text = "(" & objDealer.DealerCode & ")"
            imgStar.Visible = False
            If Not objUserInfo.UserProfile Is Nothing Then
                If Not objUserInfo.UserProfile.Bingo Is Nothing Then
                    lblvaliduntil.Text = objUserInfo.UserProfile.Bingo.BingoValidUntil.ToString("dd-MMM-yyy hh:mm:ss") & " WIB"
                    Dim t1 As DateTime = objUserInfo.UserProfile.Bingo.BingoValidUntil
                    Dim t2 As DateTime = System.DateTime.Now
                    If DateTime.Compare(t1, t2) >= 0 Then
                        Dim BingoReminderDay = KTB.DNet.Lib.WebConfig.GetValue("BingoReminderDay")
                        Dim diff1 As System.TimeSpan
                        diff1 = t1.Subtract(t2)
                        Dim days As Integer = diff1.Days + 1
                        Dim LoginMode As String = String.Empty
                        LoginMode = KTB.DNet.Lib.WebConfig.GetValue("LoginMode")
                        If LoginMode = "ORIGINAL" Then
                            If days <= BingoReminderDay Then
                                imgStar.Visible = True
                            Else
                                imgStar.Visible = False
                            End If
                        End If
                    End If
                End If
            End If

            If objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                'SetTreeMenu("TreeMenu.XML.PathFromRootFreeService", 9000)
            Else
                SetTreeMenu("TreeMenu.XML.PathFromRootCallCenter", 11100)
            End If
        End If

    End Sub

    Private Sub DisplayTreeMenu()
        If Not Request.QueryString("screenid") = Nothing Then
            containerPage = Me.Page
            _menuMapper = New MenuMapper
            _menuPage = _menuMapper.GetMenuPage(Request.QueryString("screenid"))
            Dim objUser As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
            Dim reason As String = ""
            If Not CommonFunction.ValidPage(Request.QueryString("screenid"), objUser.Dealer, reason) Then
                containerPage.MainFrameSrc = "FrmInvalidMenuControl.aspx?reason=" & Server.UrlEncode(reason.ToString).ToString()
            Else
                If Not _menuPage = String.Empty Then
                    containerPage.MainFrameSrc = _menuPage
                Else
                    containerPage.MainFrameSrc = _errorpage
                End If
                If Not _menuMapper Is Nothing Then
                    _menuMapper = Nothing
                End If
            End If
        Else
            dologout()
        End If

    End Sub

    Private Sub SetTreeMenu(ByVal strXMLPath As String, ByVal intScreenID As Int16)
        MyMenu.ResetCache()
        MyMenu.XMLPath = strXMLPath
        MyMenu.ScreenId = intScreenID
    End Sub

    Private Sub lnkSecurity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSecurity.Click
        MainFrameSrc = "UserManagement/frmSecurityPage.aspx"
    End Sub

#End Region

#Region "Clock"
    Public Function getServerDateItems() As String
        Return DateTime.Now.Year & "," & DateTime.Now.Month() & "," & DateTime.Now.Day & "," & DateTime.Now.Hour & "," & DateTime.Now.Minute & "," & DateTime.Now.Second
    End Function

    Public Function clockTimeString(ByVal inHours As Integer, ByVal inMinutes As Integer, ByVal inSeconds As Integer) As String
        Dim strInHours As String
        Dim strInMinutes As String
        Dim strInSeconds As String
        Dim strAMPM As String

        If inHours < 12 Then
            strAMPM = " AM"
        Else
            strAMPM = " PM"
        End If

        If inHours = 0 Then
            inHours = 12
        ElseIf inHours <= 12 Then
            inHours = inHours
        Else
            inHours -= 12
        End If
        If inHours.ToString.Length = 1 Then
            strInHours = "0" & inHours.ToString
        Else
            strInHours = inHours.ToString
        End If

        If inMinutes.ToString.Length = 1 Then
            strInMinutes = "0" & inMinutes.ToString
        Else
            strInMinutes = inMinutes.ToString
        End If

        If inSeconds.ToString.Length = 1 Then
            strInSeconds = "0" & inSeconds.ToString
        Else
            strInSeconds = inSeconds.ToString
        End If
        Return strInHours & ":" & strInMinutes & strAMPM
    End Function
#End Region


    Private Sub RsdLinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RsdLinkButton.Click
        sessionHelper.RemoveAllExceptLoginData()
        Response.Redirect("default_rsd.aspx")
    End Sub

    Private Sub PromoLinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PromoLinkButton.Click
        sessionHelper.RemoveAllExceptLoginData()
        Response.Redirect("default_promotion.aspx")
    End Sub

    Private Sub AfterSalesLinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AfterSalesLinkButton.Click
        sessionHelper.RemoveAllExceptLoginData()
        Response.Redirect("default_aftersales.aspx")
    End Sub

    Private Sub MrkLinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MrkLinkButton.Click
        sessionHelper.RemoveAllExceptLoginData()
        Response.Redirect("default_marketing.aspx")
    End Sub

    Private Sub LogTosyslog(ByVal message As String, ByVal sbMsg As String, ByVal rslMsg As String)
        Dim strLog As Boolean = KTB.DNet.Lib.WebConfig.GetValue("EnableSyslog")
        If strLog Then
            Dim objSyslog As SyslogParameter = New SyslogParameter(User)
            Dim m As KTB.DNet.Lib.SysLogXMLMessage = New KTB.DNet.Lib.SysLogXMLMessage
            m.Action = "click"
            m.SubBlockName = "user-logout"
            m.FullMessage = message.ToLower
            m.ModuleName = "web-security"
            m.Pages = HttpContext.Current.Request.Url.LocalPath
            m.RemoteIPAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
            m.StatusResult = rslMsg.ToLower
            m.Status = KTB.DNet.[Lib].DNetLogFormatStatus.Direct
            m.BlockName = sbMsg.ToLower
            Try
                m.UserName = IIf(User.Identity.Name.Length > 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name).ToLower
            Catch ex As Exception
                m.UserName = "Wb-Usr"
            End Try
            m.Dealer = CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode.ToLower
            objSyslog.LogError(m)
        End If
    End Sub

    Private Sub lbtnCallCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCallCenter.Click
        Dim sUrl As String = KTB.DNet.Lib.WebConfig.GetValue("CallCenterURL")

        If CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            SetTreeMenu("TreeMenu.XML.PathFromRootCallCenter", 11100)
        Else
            KTB.DNet.Utility.CommonFunction.OpenCallCenter(Me, sUrl)
            SetTreeMenu("TreeMenu.XML.PathFromRootCallCenterKTB", 11300)
        End If
    End Sub
    Private Sub CheckCallCenter()
        Dim IsEnableCallCenter As Boolean = KTB.DNet.Lib.WebConfig.GetValue("IsEnableCallCenter")
        If CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            If CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                IsEnableCallCenter = KTB.DNet.Lib.WebConfig.GetValue("IsEnableCallCenterForDealer")
            Else
                IsEnableCallCenter = False
            End If
        End If
        Me.lbtnCallCenter.Enabled = IsEnableCallCenter
    End Sub
    Private Sub lbtnTraining_Click(sender As Object, e As EventArgs) Handles lbtnTraining.Click
        sessionHelper.RemoveAllExceptLoginData()
        Response.Redirect("default_training.aspx")
    End Sub
End Class
