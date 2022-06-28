#Region "Summary"
'-- ===========================================================================
'-- AUTHOR        : Didin K
'-- PURPOSE       : Get IPrincipal for Test Fixtures.
'-- SPECIAL NOTES :
'-- ---------------------
'-- Copyright  2005 
'-- ---------------------
'-- Created on 06/01/2005 - 10:25:48
'--
'-- ===========================================================================	
#End Region

Imports System.Security.Principal
Imports System.Text
Imports KTB.DNet.Security


Namespace KTB.DNET.BusinessFacade.TestFixture
    Public Class UserTestFixtures
        ' This user used just for test fixture
        ' The GetUser function will return "nothing" if user "000053didin" not found on db
        Private Const CONST_PSWD = "d"
        Private Const CONST_USER = "000053didin"

        Public Shared Function GetUser() As IPrincipal
            Dim identity As IIdentity
            Try
                Dim pwdBytes As Byte() = Encoding.Unicode.GetBytes(CONST_PSWD)
                Dim authenticated As Boolean = SecurityProvider.Authenticate(CONST_USER, pwdBytes, identity)
                If authenticated Then
                    Return SecurityProvider.GetRoles(identity)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Dim err As String = ex.Message
            End Try
        End Function
    End Class
End Namespace

