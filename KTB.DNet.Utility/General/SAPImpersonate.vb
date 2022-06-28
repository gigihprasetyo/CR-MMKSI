Imports System
Imports System.io
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Security.Permissions
Imports Microsoft.VisualBasic
Imports System.Diagnostics

<Assembly: SecurityPermissionAttribute(SecurityAction.RequestMinimum, UnmanagedCode:=True), _
 Assembly: PermissionSetAttribute(SecurityAction.RequestMinimum, Name:="FullTrust")> 
Namespace KTB.DNet.Utility
    Public Class SAPImpersonate
        Private Declare Auto Function LogonUser Lib "advapi32.dll" (ByVal lpszUsername As [String], _
            ByVal lpszDomain As [String], ByVal lpszPassword As [String], _
            ByVal dwLogonType As Integer, ByVal dwLogonProvider As Integer, _
            ByRef phToken As IntPtr) As Boolean

        <DllImport("kernel32.dll")> _
        Public Shared Function FormatMessage(ByVal dwFlags As Integer, ByRef lpSource As IntPtr, _
            ByVal dwMessageId As Integer, ByVal dwLanguageId As Integer, ByRef lpBuffer As [String], _
            ByVal nSize As Integer, ByRef Arguments As IntPtr) As Integer

        End Function

        Public Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Boolean

        Public Declare Auto Function DuplicateToken Lib "advapi32.dll" (ByVal ExistingTokenHandle As IntPtr, _
                ByVal SECURITY_IMPERSONATION_LEVEL As Integer, _
                ByRef DuplicateTokenHandle As IntPtr) As Boolean


        Public Sub New(ByVal user As String, ByVal passwd As String, ByVal Machine As String)
            _userName = user
            _password = passwd
            _MachineName = Machine
        End Sub

        Private _userName As String
        Private _password As String
        Private _MachineName As String
 
        

        Public Function GetErrorMessage(ByVal errorCode As Integer) As String
            Dim FORMAT_MESSAGE_ALLOCATE_BUFFER As Integer = &H100
            Dim FORMAT_MESSAGE_IGNORE_INSERTS As Integer = &H200
            Dim FORMAT_MESSAGE_FROM_SYSTEM As Integer = &H1000

            Dim messageSize As Integer = 255
            Dim lpMsgBuf As String
            Dim dwFlags As Integer = FORMAT_MESSAGE_ALLOCATE_BUFFER Or FORMAT_MESSAGE_FROM_SYSTEM Or FORMAT_MESSAGE_IGNORE_INSERTS

            Dim ptrlpSource As IntPtr = IntPtr.Zero
            Dim prtArguments As IntPtr = IntPtr.Zero

            Dim retVal As Integer = FormatMessage(dwFlags, ptrlpSource, errorCode, 0, lpMsgBuf, _
                messageSize, prtArguments)
            If 0 = retVal Then
                Throw New Exception("Failed to format message for error code " + errorCode.ToString() + ". ")
            End If

            Return lpMsgBuf
        End Function 'GetErrorMessage

        Private tokenHandle As New IntPtr(0)
        Private dupeTokenHandle As New IntPtr(0)
        Private impersonatedUser As WindowsImpersonationContext

        <PermissionSetAttribute(SecurityAction.Demand, Name:="FullTrust")> _
        Public Function Start() As Boolean
            Try
                Const LOGON32_PROVIDER_DEFAULT As Integer = 0
                Const LOGON32_LOGON_INTERACTIVE As Integer = 9 '2/9
                Const SecurityImpersonation As Integer = 2

                tokenHandle = IntPtr.Zero
                dupeTokenHandle = IntPtr.Zero
                Dim returnValue As Boolean = LogonUser(_userName, _MachineName, _password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, tokenHandle)
                Dim retVal As Boolean = DuplicateToken(tokenHandle, SecurityImpersonation, dupeTokenHandle)

                If retVal = True And returnValue = True Then
                    Dim newId As New WindowsIdentity(dupeTokenHandle)
                    impersonatedUser = newId.Impersonate()
                    Return True
                Else
                    Return False
                End If

                'If False = retVal Then
                '    CloseHandle(tokenHandle)
                '    Throw New Exception("Exception thrown in trying to duplicate token.")
                'End If


                'If returnValue Then

                'Else
                '    Dim ret As Integer = Marshal.GetLastWin32Error()
                '    Throw New Exception(GetErrorMessage(ret))
                'End If


            Catch ex As Exception
                Throw New Exception("Failed Impersonate : " & ex.Message)
            End Try

        End Function

        Public Sub StopImpersonate()
            If Not System.IntPtr.op_Equality(tokenHandle, IntPtr.Zero) Then
                CloseHandle(tokenHandle)
            End If
            If Not System.IntPtr.op_Equality(dupeTokenHandle, IntPtr.Zero) Then
                CloseHandle(dupeTokenHandle)
            End If
            impersonatedUser.Undo()
        End Sub



    End Class
End Namespace
