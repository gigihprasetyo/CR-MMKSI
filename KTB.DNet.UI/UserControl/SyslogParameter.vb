#Region "Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Security
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Helper
Imports System.Security.Principal
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class SyslogParameter

#Region "Constructor"
    Public Sub New()
    End Sub

    Public Sub New(ByVal userPrincipal As IPrincipal)
        mUser = userPrincipal
    End Sub


#End Region

#Region "Private Variable"
    Private mUser As IPrincipal
#End Region

#Region "Property"

#End Region

#Region "Public Method"

    Public Function GetSyslogParameter(ByVal action As String, ByVal blockName As String, ByVal FullMessage As String, ByVal moduleName As String, ByVal pages As String, ByVal IpAddress As String, ByVal code As String, ByVal status As String, ByVal subBlockName As String, ByVal userName As String) As KTB.DNet.Lib.SysLogXMLMessage
        Dim m As New KTB.DNet.Lib.SysLogXMLMessage
        m.Action = action
        m.BlockName = blockName
        m.FullMessage = FullMessage
        m.ModuleName = moduleName
        m.Pages = pages
        m.RemoteIPAddress = IpAddress
        m.Result = code
        m.Status = status
        m.SubBlockName = subBlockName
        m.UserName = userName
        Return m
    End Function

    Public Function GetSyslogParameter(ByVal e As Exception) As KTB.DNet.Lib.SysLogXMLMessage
        Dim m As New KTB.DNet.Lib.SysLogXMLMessage
        m.UserName = mUser.Identity.Name
        m.FullMessage = e.ToString
        m.ModuleName = e.Source
        m.Pages = HttpContext.Current.Request.Url.LocalPath
        If Not e.TargetSite Is Nothing Then
            m.BlockName = e.TargetSite.Name
        End If

        Dim strRemoteIP As String = HttpContext.Current.Request.Params("REMOTE_ADDR")
        If strRemoteIP Is Nothing Then
            strRemoteIP = String.Empty
        End If
        m.RemoteIPAddress = strRemoteIP
        Return m
    End Function

    Public Sub DeferredSyslogMessage(ByVal message As KTB.DNet.Lib.SysLogXMLMessage)
        Dim objDomain As New KTB.DNet.Domain.SysLog
        Dim facade As New KTB.DNet.BusinessFacade.General.SysLogFacade(mUser)
        Try
            objDomain.Action = message.Action
            objDomain.BlockName = message.BlockName
            objDomain.FullMessage = message.FullMessage
            objDomain.ModuleName = message.ModuleName
            objDomain.Pages = message.Pages
            objDomain.RemoteIPAddress = message.RemoteIPAddress
            objDomain.ResultCode = message.Result
            objDomain.Status = KTB.DNet.Lib.DNetLogFormatStatus.Deferred.ToString()
            objDomain.SubBlockName = message.SubBlockName
            objDomain.UserName = message.UserName
            objDomain.LogTime = DateTime.Now
            facade.Insert(objDomain)
        Catch ex As Exception
            LogError(ex)
        End Try
    End Sub

    Public Sub LogError(ByVal message As KTB.DNet.Lib.SysLogXMLMessage)
        Dim strServerName As String = ConfigurationSettings.AppSettings.Get("SyslogServerHostName")
        Dim SecondarystrServerName As String = ConfigurationSettings.AppSettings.Get("SyslogSecondaryServerHostName")
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
                Dim objSysLogParameter As SyslogParameter = New SyslogParameter(Threading.Thread.CurrentPrincipal)
                objSysLogParameter.DeferredSyslogMessage(message)
            End Try
        Catch ex As Exception
            LogError(ex)
        End Try

    End Sub

    Public Sub LogError(ByVal e As Exception)
        Dim _user As String = ConfigurationSettings.AppSettings.Get("User")
        Dim _password As String = ConfigurationSettings.AppSettings.Get("Password")
        Dim _webServer As String = ConfigurationSettings.AppSettings.Get("WebServer")   '172.17.104.204
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


#End Region


#Region "Private Method"

#End Region




End Class

