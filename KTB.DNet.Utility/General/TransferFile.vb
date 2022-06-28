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
    Public Class TransferFile
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

        Private _fileName As String
        Private _userName As String
        Private _password As String
        Private _MachineName As String
        
        Property FileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal Value As String)
                _fileName = Value
            End Set
        End Property

        Property UserName() As String
            Get

            End Get
            Set(ByVal Value As String)

            End Set
        End Property

        Property Password() As String
            Get
                Return _password
            End Get
            Set(ByVal Value As String)
                _password = Value
            End Set
        End Property

        Property MachineName() As String
            Get
                Return _MachineName
            End Get
            Set(ByVal Value As String)
                _MachineName = Value
            End Set
        End Property

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


        <PermissionSetAttribute(SecurityAction.Demand, Name:="FullTrust")> _
        Public Sub Transfer(ByVal fName As String, ByVal DestinationFolder As String)
            Dim tokenHandle As New IntPtr(0)
            Dim dupeTokenHandle As New IntPtr(0)
            Dim impersonatedUser As WindowsImpersonationContext
            Try
                Const LOGON32_PROVIDER_DEFAULT As Integer = 0
                'This parameter causes LogonUser to create a primary token.
                Const LOGON32_LOGON_INTERACTIVE As Integer = 2
                Const SecurityImpersonation As Integer = 2

                tokenHandle = IntPtr.Zero
                dupeTokenHandle = IntPtr.Zero
                Dim returnValue As Boolean = LogonUser(_userName, _MachineName, _password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, tokenHandle)
                If returnValue = False Then
                    Throw New Exception("User : " & _userName & " ,Password : " & _password & " ,MechineName : " & _MachineName & " Fail to Logon   ")
                End If
                Dim retVal As Boolean = DuplicateToken(tokenHandle, SecurityImpersonation, dupeTokenHandle)
                If False = retVal Then
                    CloseHandle(tokenHandle)
                    Throw New Exception("Exception thrown in trying to duplicate token.")
                End If

                Dim newId As New WindowsIdentity(dupeTokenHandle)
                impersonatedUser = newId.Impersonate()

                If returnValue Then
                    copyFile(fName, DestinationFolder)
                Else
                    Dim ret As Integer = Marshal.GetLastWin32Error()
                    Throw New Exception("User : " & _userName & " ,Password : " & _password & " ,MechineName : " & _MachineName & " Fail to Logon ,  " & GetErrorMessage(ret))
                End If

                If Not System.IntPtr.op_Equality(tokenHandle, IntPtr.Zero) Then
                    CloseHandle(tokenHandle)
                End If
                If Not System.IntPtr.op_Equality(dupeTokenHandle, IntPtr.Zero) Then
                    CloseHandle(dupeTokenHandle)
                End If
            Catch ex As Exception
                Throw New Exception("User : " & _userName & " ,Password : " & _password & " ,MechineName : " & _MachineName & " Fail to Impersonate ,  " & ex.Message, ex)
            Finally
                If Not impersonatedUser Is Nothing Then
                    impersonatedUser.Undo()
                End If
            End Try
        End Sub

        Public Sub xcopyFile(ByVal fileName As String, ByVal destinationFolder As String)
            Dim _process As Process
            Dim finfo As FileInfo = New FileInfo(fileName)
            Dim fName As String = "C:\" & finfo.Name
            Try
                'Dim str1 As String = "/C net use \\172.17.104.204 /user:admin ""admin""&copy c:\xxx.log \\172.17.104.204\ZDnet&pause"
                'Dim copystr As String = "/C net use \\" & _MachineName & " /user:" & _userName & " """ & _password & """&copy " & fileName & " " & destinationFolder & "&pause"
                Dim copystr As String = "/C net use \\" & _MachineName & " /user:" & _userName & " """ & _password & """&copy " & fileName & " " & destinationFolder
                Dim deletstr1 As String = "/C net use " & destinationFolder & " /delete"
                '  Dim deletstr2 As String = "/C net use \\" & _MachineName & "\IPC$" & " /delete >" & fName & "-03"

                _process = New Process
                _process.StartInfo = New ProcessStartInfo("cmd.exe", deletstr1)
                _process.Start()

                '_process = New Process
                '_process.StartInfo = New ProcessStartInfo("cmd.exe", deletstr2)
                '_process.Start()

                _process = New Process
                _process.StartInfo = New ProcessStartInfo("cmd.exe", copystr)
                _process.Start()

                _process = New Process
                _process.StartInfo = New ProcessStartInfo("cmd.exe", deletstr1)
                _process.Start()

                '_process = New Process
                '_process.StartInfo = New ProcessStartInfo("cmd.exe", deletstr2)
                '_process.Start()
                '_process.Refresh()

            Finally
                _process = Nothing
            End Try

        End Sub

        Public Sub copyFile(ByVal fileName As String, ByVal destinationFolder As String)
            copyFile(fileName, destinationFolder, True)
        End Sub

        Public Sub copyFile(ByVal fileName As String, ByVal destinationFolder As String, ByVal overwrite As Boolean)
            'Dim _process As Process
            Dim finfo As FileInfo = New FileInfo(fileName)
            'Dim fName As String = "C:\" & finfo.Name
            Try
                'Dim ConnectCmd As String = "/C net use \\" & _MachineName & " /user:" & _userName & " """ & _password
                'Dim deleteCmd As String = "/C net use \\" & _MachineName & " /delete"

                '_process = New Process
                '_process.StartInfo = New ProcessStartInfo("cmd.exe", deleteCmd)
                '_process.Start()


                '_process = New Process
                '_process.StartInfo = New ProcessStartInfo("cmd.exe", ConnectCmd)
                '_process.Start()

                Dim DirFinfo As DirectoryInfo = New DirectoryInfo(destinationFolder)
                If Not DirFinfo.Exists Then
                    DirFinfo.Create()
                End If
                If finfo.Exists = True Then
                    finfo.CopyTo(destinationFolder & "\" & finfo.Name, overwrite)
                End If
                '_process = New Process
                '_process.StartInfo = New ProcessStartInfo("cmd.exe", deleteCmd)
                '_process.Start()
            Catch ex As Exception
                Throw ex
            Finally
                '_process = Nothing
            End Try

        End Sub

        Public Sub deleteFile(ByVal fileName As String)
            Dim _process As Process
            Dim finfo As FileInfo = New FileInfo(fileName)
            'Dim fName As String = "C:\" & finfo.Name
            Try
                Dim ConnectCmd As String = "/C net use \\" & _MachineName & " /user:" & _userName & " """ & _password
                Dim deleteCmd As String = "/C net use \\" & _MachineName & " /delete"

                _process = New Process
                _process.StartInfo = New ProcessStartInfo("cmd.exe", deleteCmd)
                _process.Start()


                _process = New Process
                _process.StartInfo = New ProcessStartInfo("cmd.exe", ConnectCmd)
                _process.Start()

                If Not finfo.Exists Then
                    Throw New IOException("File tidak ditemukan")
                End If

                finfo.Delete()

                _process = New Process
                _process.StartInfo = New ProcessStartInfo("cmd.exe", deleteCmd)
                _process.Start()
            Catch ex As Exception
                Throw ex
            Finally
                _process = Nothing
            End Try

        End Sub

        Public Function CreateDirectory(ByVal DestinationFolder As String)
            Dim tokenHandle As New IntPtr(0)
            Dim dupeTokenHandle As New IntPtr(0)
            Dim impersonatedUser As WindowsImpersonationContext
            Try
                Const LOGON32_PROVIDER_DEFAULT As Integer = 0
                'This parameter causes LogonUser to create a primary token.
                Const LOGON32_LOGON_INTERACTIVE As Integer = 2
                Const SecurityImpersonation As Integer = 2

                tokenHandle = IntPtr.Zero
                dupeTokenHandle = IntPtr.Zero
                Dim returnValue As Boolean = LogonUser(_userName, _MachineName, _password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, tokenHandle)
                If returnValue = False Then
                    Throw New Exception("User : " & _userName & " ,Password : " & _password & " ,MechineName : " & _MachineName & " Fail to Logon   ")
                End If
                Dim retVal As Boolean = DuplicateToken(tokenHandle, SecurityImpersonation, dupeTokenHandle)
                If False = retVal Then
                    CloseHandle(tokenHandle)
                    Throw New Exception("Exception thrown in trying to duplicate token.")
                End If

                Dim newId As New WindowsIdentity(dupeTokenHandle)
                impersonatedUser = newId.Impersonate()

                If returnValue Then
                    Dim finfo As FileInfo = New FileInfo(FileName)
                    Try
                        Dim DirFinfo As DirectoryInfo = New DirectoryInfo(DestinationFolder)
                        If Not DirFinfo.Exists Then
                            DirFinfo.Create()
                        End If
                    Catch ex As Exception
                        Throw ex
                    Finally
                    End Try
                Else
                    Dim ret As Integer = Marshal.GetLastWin32Error()
                    Throw New Exception("User : " & _userName & " ,Password : " & _password & " ,MechineName : " & _MachineName & " Fail to Logon ,  " & GetErrorMessage(ret))
                End If

                If Not System.IntPtr.op_Equality(tokenHandle, IntPtr.Zero) Then
                    CloseHandle(tokenHandle)
                End If
                If Not System.IntPtr.op_Equality(dupeTokenHandle, IntPtr.Zero) Then
                    CloseHandle(dupeTokenHandle)
                End If
            Catch ex As Exception
                Throw New Exception("User : " & _userName & " ,Password : " & _password & " ,MechineName : " & _MachineName & " Fail to Impersonate ,  " & ex.Message, ex)
            Finally
                If Not impersonatedUser Is Nothing Then
                    impersonatedUser.Undo()
                End If
            End Try
        End Function
    End Class
End Namespace
