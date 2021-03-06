#Region "Imports"
Imports System.Web
Imports System.Web.SessionState
Imports Microsoft.Practices.EnterpriseLibrary.Security
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Security
Imports System.Configuration
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain

Imports System.Reflection
Imports System.Diagnostics

#End Region

Public Class [Global]
    Inherits System.Web.HttpApplication

#Region " Component Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

#Region "Private Variable"
    Private _user As String
    Private _password As String
    Private _webServer As String

#End Region

#Region "Application"

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        'Application("RuleProvider") = AuthorizationFactory.GetAuthorizationProvider("RuleProvider")
        ' Fires when the application is started
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        'Dim str() As String
        'str(1) = "Admin"
        'Context.User = New System.Security.Principal.GenericPrincipal(New System.Security.Principal.GenericIdentity("User1"), str)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        'Fix hanya untuk Regional Setting ID
        Dim lang As String = "id-ID"
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = _
                New System.Globalization.CultureInfo(lang)
        Catch
            ' NOOP
        End Try


        Try
            HttpContext.Current.Response.AddHeader("x-frame-options", "SAMEORIGIN")
        Catch ex As Exception

        End Try
    End Sub

    Sub Application_EndRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        'Fix hanya untuk Regional Setting ID
        Try
            Const HTTPOnly As String = ";HttpOnly"
            Const ReqSSL As String = ";requireSSL"
            Dim path As String
            For Each cookie As String In Response.Cookies
                path = Response.Cookies(cookie).Path
                If Not path.ToUpper.IndexOf(HTTPOnly) = 0 Then
                    Response.Cookies(cookie).Path += HTTPOnly
                End If
                If Not path.ToUpper.IndexOf(ReqSSL) = 0 Then
                    Response.Cookies(cookie).Path += ReqSSL
                End If

            Next

        Catch
            ' NOOP
        End Try
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
        'If Not Context.User Is Nothing Then
        '    Context.User = SecurityProvider.GetRoles(Context.User.Identity)
        'End If
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        If IsSyslogEnabled() Then
            'LogError(m)
            Dim ex As Exception = Server.GetLastError().GetBaseException()
            'LogTosyslog(ex.StackTrace, "error-message", "failed", ex.Message)
            LogTosyslog("<exception>" & ex.Message & "</exception><stack>" & ex.StackTrace & "</stack>", "error-handler", "failed", )

        End If
        Dim currentContext As HttpContext = HttpContext.Current
        If currentContext Is Nothing Then
            Response.Redirect(currentContext.Request.ApplicationPath & "/Login.aspx?Msg=GetOut")
        End If
        'If currentContext.User.Identity.Name.Length < 1 Then
        '    Response.Redirect(currentContext.Request.ApplicationPath & "/Login.aspx?Msg=GetOut")
        'End If

        Dim objUser As System.Security.Principal.IPrincipal = currentContext.User

        Dim showFMessage As String = "Y"
        Try
            showFMessage = ConfigurationSettings.AppSettings.Item("FM")
        Catch ex As Exception
            showFMessage = "Y"
        End Try
        If showFMessage.ToUpper = "Y" Then
            Dim IsLogError As String = ConfigurationSettings.AppSettings.Item("LOGERROR")
            Dim code As String = ConvertErrorCode(GetLastError)
            Dim ctx As HttpContext = HttpContext.Current
            Dim ex As Exception = Server.GetLastError().GetBaseException()
            Dim ErrDesc As String = ex.Message
            ErrDesc = ErrDesc.Replace("<", "")
            ErrDesc = ErrDesc.Replace(">", "")
            If IsLogError.Trim.Substring(0, 1).ToUpper = "Y" Then
                Response.Redirect(ctx.Request.ApplicationPath & "/ErrorCode.aspx?error=" & code & "&ErrorDetail=" & ErrDesc)
            End If
        End If
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
        ''Additional By Ali
        '' 2015 04 20
        'Try
        '    Dim runtime As HttpRuntime = DirectCast(GetType(System.Web.HttpRuntime).InvokeMember("_theRuntime", BindingFlags.NonPublic Or BindingFlags.[Static] Or BindingFlags.GetField, Nothing, Nothing, Nothing), HttpRuntime)

        '    If runtime Is Nothing Then
        '        Return
        '    End If

        '    Dim shutDownMessage As String = DirectCast(runtime.[GetType]().InvokeMember("_shutDownMessage", BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.GetField, Nothing, runtime, Nothing), String)

        '    Dim shutDownStack As String = DirectCast(runtime.[GetType]().InvokeMember("_shutDownStack", BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.GetField, Nothing, runtime, Nothing), String)

        '    If Not EventLog.SourceExists(".NET Runtime") Then
        '        EventLog.CreateEventSource(".NET Runtime", "Application")
        '    End If

        '    Dim log As New EventLog()
        '    log.Source = ".NET Runtime"
        '    log.WriteEntry([String].Format(vbCr & vbLf & vbCr & vbLf & "_shutDownMessage={0}" & vbCr & vbLf & vbCr & vbLf & "_shutDownStack={1}", shutDownMessage, shutDownStack), EventLogEntryType.[Error])

        'Catch ex As Exception
        '    If Not EventLog.SourceExists(".NET Runtime") Then
        '        EventLog.CreateEventSource(".NET Runtime", "Application")
        '    End If

        '    Dim log As New EventLog()
        '    log.Source = ".NET Runtime"
        '    log.WriteEntry(ex.Message, EventLogEntryType.[Error])

        'End Try
          ''End Of Aditional by Ali
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

    Private Sub Global_AuthorizeRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.AuthorizeRequest
    End Sub

    Private Sub Global_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRequestHandlerExecute
        Dim currentContext As HttpContext = HttpContext.Current
        Dim requestingLoginPage As Boolean = False
        Try
            Dim objDealer As KTB.DNet.Domain.Dealer = CType(currentContext.Session("DEALER"), KTB.DNet.Domain.Dealer)
            Dim objUserInfo As KTB.DNet.Domain.UserInfo = CType(currentContext.Session("LOGINUSERINFO"), KTB.DNet.Domain.UserInfo)
            Dim objUser As System.Security.Principal.IPrincipal = currentContext.User
            If (objDealer Is Nothing OrElse objUserInfo Is Nothing OrElse objUser Is Nothing) Then
                Try
                    Dim obj As Object = CType(currentContext.Handler, Login)
                    requestingLoginPage = True
                Catch
                    Try
                        Dim obj As Object = CType(currentContext.Handler, GetPCImage)
                        requestingLoginPage = True
                    Catch ex As Exception
                        Try
                            Dim obj As Object = CType(currentContext.Handler, ErrorCode)
                            requestingLoginPage = True
                        Catch _ex As Exception
                            Try
                                Dim obj As Object = CType(currentContext.Handler, frmPCPhisingGuard)
                                requestingLoginPage = True
                            Catch exc As Exception
                                Try
                                    Dim obj As Object = CType(currentContext.Handler, frmSecondLogin)
                                    requestingLoginPage = True
                                Catch ex1 As Exception
                                    Try
                                        Dim obj As Object = CType(currentContext.Handler, frmSecondAuth)
                                        requestingLoginPage = True
                                    Catch ex2 As Exception
                                        Try
                                            Dim obj As Object = CType(currentContext.Handler, frmActCodeVerification)
                                            requestingLoginPage = True
                                        Catch ex5 As Exception
                                            Try
                                                Dim obj As Object = CType(currentContext.Handler, frmForgetPasswordNext)
                                                requestingLoginPage = True
                                            Catch ex6 As Exception
                                                Try
                                                    Dim obj As Object = CType(currentContext.Handler, frmForgetPassword)
                                                    requestingLoginPage = True
                                                Catch ex7 As Exception
                                                    Try
                                                        Dim obj As Object = CType(currentContext.Handler, FrmForgetToken)
                                                        requestingLoginPage = True
                                                    Catch ex9 As Exception
                                                        Try
                                                            Dim obj As Object = CType(currentContext.Handler, JpegImage)
                                                            requestingLoginPage = True
                                                        Catch ex8 As Exception
                                                            Try
                                                                Dim obj As Object = CType(currentContext.Handler, DownloadWithoutLogIn)
                                                                requestingLoginPage = True
                                                            Catch ex10 As Exception
                                                                requestingLoginPage = False
                                                            End Try

                                                        End Try
                                                    End Try
                                                    
                                                End Try
                                            End Try
                                        End Try
                                    End Try
                                End Try
                            End Try
                        End Try
                    End Try
                End Try
                If Not requestingLoginPage Then
                    Response.Redirect(currentContext.Request.ApplicationPath & "/Login.aspx?Msg=GetOut")
                End If
            Else
                If objUserInfo Is Nothing Then
                    Response.Redirect(currentContext.Request.ApplicationPath & "/Login.aspx?Msg=GetOut")
                End If
            End If
            KickOtherUser()
        Catch ex As Exception
            'Response.Redirect("Login.aspx")
            'If Not currentContext Is Nothing Then
            '    Response.Redirect(currentContext.Request.ApplicationPath & "/Login.aspx#Expired")
            'End If
        End Try
    End Sub
#End Region

#Region "Private Method"

    Private Function GetLastError() As Exception
        Return Server.GetLastError.InnerException
    End Function

    'Private Function GetSyslogParameter(ByVal e As Exception) As KTB.DNet.Lib.SysLogXMLMessage
    '    Dim m As New KTB.DNet.Lib.SysLogXMLMessage
    '    m.UserName = IIf(User.Identity.Name.Length >= 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name)
    '    m.FullMessage = e.ToString
    '    m.ModuleName = e.Source
    '    m.Pages = HttpContext.Current.Request.Url.LocalPath
    '    If Not e.TargetSite Is Nothing Then
    '        m.SubBlockName = e.TargetSite.Name
    '    End If

    '    Dim strRemoteIP As String = HttpContext.Current.Request.Params("REMOTE_ADDR")
    '    If strRemoteIP Is Nothing Then
    '        strRemoteIP = String.Empty
    '    End If
    '    m.RemoteIPAddress = strRemoteIP
    '    Return m
    'End Function

    Private Function IsSyslogEnabled() As Boolean
        Dim strLog As String = ConfigurationSettings.AppSettings.Item("EnableSyslog")
        Return CType(strLog.Trim(), Boolean)
    End Function

    Private Function ConvertErrorCode(ByVal e As Exception) As String
        Dim code As String = GetErrorCode("System.Exception")
        If Not e Is Nothing Then
            Dim exceptionType As String = e.GetType.ToString

            'LogError(e)
            Dim objSysLogParameter As SyslogParameter = New SyslogParameter(User)
            Dim m As KTB.DNet.Lib.SysLogXMLMessage = objSysLogParameter.GetSyslogParameter(e)

            'If IsSyslogEnabled() Then
            '    'LogError(m)
            '    LogTosyslog(Server.GetLastError.InnerException.Message, "", "")
            'End If

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

    Private Sub LogTosyslog(ByVal message As String, ByVal sbMsg As String, ByVal rslMsg As String, Optional ByVal mdlmsg As String = "web-exception", Optional ByVal sbAction As String = "view")
        Dim strLog As Boolean = System.Configuration.ConfigurationSettings.AppSettings.Item("EnableSyslog")
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
            m.BlockName = "error-handler"

            m.UserName = IIf(User.Identity.Name.Length >= 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name)
            If Not IsNothing(Session("LOGINUSERINFO")) Then
                m.Dealer = CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode.ToLower
            End If
            objSyslog.LogError(m)
        End If
    End Sub


    Private Sub LogError(ByVal e As Exception)
        _user = ConfigurationSettings.AppSettings.Get("User")
        _password = ConfigurationSettings.AppSettings.Get("Password")
        _webServer = ConfigurationSettings.AppSettings.Get("WebServer")   '172.17.104.204
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

    Private Sub LogError(ByVal message As KTB.DNet.Lib.SysLogXMLMessage)
        Dim strServerName As String = ConfigurationSettings.AppSettings.Get("SyslogServerHostName")
        Dim SecondarystrServerName As String = ConfigurationSettings.AppSettings.Get("SyslogServerHostName")
        Dim PortNo As Integer = CInt(ConfigurationSettings.AppSettings.Get("SyslogServerPortNumber"))
        Dim IsUseUDP As Boolean = CBool(ConfigurationSettings.AppSettings.Get("IsSyslogUseUDP"))
        Dim logger As New KTB.DNet.Lib.SyslogLogger(strServerName, PortNo, IsUseUDP)
        Dim Secondarylogger As New KTB.DNet.Lib.SyslogLogger(SecondarystrServerName, PortNo, IsUseUDP)
        Try
            logger.Log(message)
        Catch ex As KTB.DNet.Lib.SysLogServerNotAvailableException
            Try
                Secondarylogger.Log(message)
            Catch ex1 As Exception
                Dim objSysLogParameter As SyslogParameter = New SyslogParameter(User)
                objSysLogParameter.DeferredSyslogMessage(message)
            End Try
        Catch ex As Exception
            LogError(ex)
        End Try

    End Sub

    'Private Sub DeferredSyslogMessage(ByVal message As KTB.DNet.Lib.SysLogXMLMessage)

    '    Dim objDomain As New KTB.DNet.Domain.SysLog
    '    Dim facade As New KTB.DNet.BusinessFacade.General.SysLogFacade(User)

    '    Try
    '        objDomain.Action = message.Action
    '        objDomain.BlockName = message.BlockName
    '        objDomain.FullMessage = message.FullMessage
    '        objDomain.ModuleName = message.ModuleName
    '        objDomain.Pages = message.Pages
    '        objDomain.RemoteIPAddress = message.RemoteIPAddress
    '        objDomain.ResultCode = message.Result
    '        objDomain.Status = KTB.DNet.Lib.DNetLogFormatStatus.Deferred.ToString()
    '        objDomain.SubBlockName = message.SubBlockName
    '        objDomain.UserName = message.UserName
    '        objDomain.LogTime = DateTime.Now

    '        facade.Insert(objDomain)
    '    Catch ex As Exception
    '        LogError(ex)
    '    End Try


    'End Sub

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

    Private Sub KickOtherUser()
        Dim LoginMode As String = String.Empty
        LoginMode = ConfigurationSettings.AppSettings.Item("LoginMode")
        If LoginMode.Trim.ToUpper = "LIVE" Then
            Dim currentContext As HttpContext = HttpContext.Current
            LoginMode = ConfigurationSettings.AppSettings.Item("LoginMode")
            Dim sHelper As SessionHelper = New SessionHelper
            If LoginMode.ToUpper = "INITIAL" Or LoginMode.ToUpper = "LIVE" Then
                Dim objUser As KTB.DNet.Domain.UserInfo = CType(currentContext.Session("LOGINUSERINFO"), KTB.DNet.Domain.UserInfo)
                If Not objUser Is Nothing Then
                    Dim objNewUserInfo As KTB.DNet.Domain.UserInfo = New KTB.DNet.BusinessFacade.UserManagement.UserInfoFacade(User).Retrieve(objUser.ID)
                    Dim strSessionID As String = currentContext.Session.SessionID
                    If Not objNewUserInfo.UserProfile Is Nothing Then
                        Dim userSessionID As String = objNewUserInfo.UserProfile.SessionID
                        If strSessionID <> userSessionID Then
                            Session.RemoveAll()
                            Response.Redirect(currentContext.Request.ApplicationPath & "/Login.aspx?Msg=kick")
                        End If
                    End If
                End If
            End If
        End If
    End Sub

#End Region



End Class
