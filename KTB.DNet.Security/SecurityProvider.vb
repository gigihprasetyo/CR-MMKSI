#Region "Imports Library"
Imports Microsoft.Practices.EnterpriseLibrary.Security
Imports System.Security.Principal
Imports KTB.DNet.Security.Database.Authorization
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration
#End Region
Namespace KTB.DNet.Security

    Public Class SecurityProvider

#Region "Static Variable "
        Private Shared strAuthenticaticationProvider As String = "Database Provider"
        Private Shared strRoleProviderName As String = "Role Database Provider"
        Private Shared strRuleProviderName As String = "Database Rules Provider"
#End Region

#Region "Static Method"
        Public Shared Function Authenticate(ByVal userName As String, ByVal password As Byte(), ByRef identity As IIdentity) As Boolean
            Dim bAuthenticated As Boolean = False
            Try
                Dim authenticationProvider As IAuthenticationProvider = AuthenticationFactory.GetAuthenticationProvider(strAuthenticaticationProvider)
                bAuthenticated = authenticationProvider.Authenticate(New NamePasswordCredential(userName, password), identity)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Security Policy")
            End Try
            Return bAuthenticated
        End Function

        Public Shared Function GetRoles(ByVal identity As IIdentity) As IPrincipal
            Dim roleProvider As IRolesProvider = RolesFactory.GetRolesProvider(strRoleProviderName)
            Return roleProvider.GetRoles(identity)
        End Function

        Public Shared Function Authorize(ByVal prinsipal As IPrincipal, ByVal context As String) As Boolean
            Dim bAuthorized As Boolean = False
            Dim isUsedAuthorization As Boolean = CBool(KTB.DNet.Lib.WebConfig.GetValue("IsUsedSecurityPrivilege"))
            If isUsedAuthorization Then
                Try
                    'Dim ruleProvider As IAuthorizationProvider = AuthorizationFactory.GetAuthorizationProvider(strRuleProviderName)
                    'prinsipal = GetRoles(prinsipal.Identity)
                    'bAuthorized = ruleProvider.Authorize(prinsipal, context)


                    ''  Dim ruleProvider As DbRulesAuthorizationProvider = New DbRulesAuthorizationProvider()
                    Dim ruleProvider As IAuthorizationProvider = AuthorizationFactory.GetAuthorizationProvider(strRuleProviderName)
                    bAuthorized = ruleProvider.Authorize(prinsipal, context)
                Catch ex As Exception
                    'Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Security Policy")
                End Try
            Else
                bAuthorized = True
            End If
            Return bAuthorized
        End Function
#End Region

    End Class

End Namespace
