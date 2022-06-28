Imports System
Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Imports Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
Imports System.Text

Namespace KTB.DNet.UI.Helper
    Public Class DNetEncryption
        Private Const symmProvider As String = "symprovider"

        Private Shared Function ConvertStringToByteArray(ByVal s As [String]) As [Byte]()
            Return (New UnicodeEncoding).GetBytes(s)
        End Function

        Public Shared Function EncryptMD5(ByVal password As String) As [Byte]()
            Dim dataToHash As [Byte]() = ConvertStringToByteArray(password)
            Dim hashvalue As Byte() = CType(System.Security.Cryptography.CryptoConfig.CreateFromName("MD5"), HashAlgorithm).ComputeHash(dataToHash)
            Return hashvalue
        End Function

        Public Shared Function EncryptBase64(ByVal password As String) As [Byte]()
            Dim pwd As [Byte]() = ConvertStringToByteArray(password)
            Dim bytePwd As Byte() = ConvertStringToByteArray(Convert.ToBase64String(pwd))
            Return bytePwd
        End Function

        Public Shared Function DecryptBase64(ByVal password As Byte()) As String
            Dim pwd As String = (New UnicodeEncoding).GetString(password)
            Dim strpwd As [Byte]() = Convert.FromBase64String(pwd)
            Return (New UnicodeEncoding).GetString(strpwd)
        End Function

        Public Shared Function DecryptBase64(ByVal password As String) As [Byte]()
            Dim pwd As [Byte]() = ConvertStringToByteArray(password)
            Dim bytePwd As Byte() = ConvertStringToByteArray(Convert.ToBase64String(pwd))
            Return bytePwd
        End Function

        Public Shared Function CompareBase64(ByVal pwd1 As [Byte](), ByVal pwd2 As [Byte]()) As Boolean
            Return pwd1.Equals(pwd2)
        End Function

        Public Shared Function ComparePassword(ByVal password1 As [Byte](), ByVal password2 As [Byte]()) As Boolean
            Dim i As Integer = 0
            Dim same As Boolean = True
            Do
                If password1(i) <> password2(i) Then
                    same = False
                    Exit Do
                End If
                i += 1
            Loop While i < password1.Length
            If same Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Function SymmetricEncrypt(ByVal text As String, ByVal password As String) As String
            Dim k As Integer
            Dim a As Integer
            Dim _returnText As String

            For i As Integer = 1 To password.Length
                k = k + (Asc(Mid(password, i, 1)) * i)
            Next

            Do While k > 255
                k = k - 255
            Loop

            For i As Integer = 1 To text.Length
                a = Asc(Mid(text, i, 1)) + k
                If a > 255 Then a = a - 255
                _returnText = _returnText + Chr(a)
                k = k + a
                If k > 255 Then k = k - 255
            Next

            Return _returnText
        End Function

        Public Shared Function SymmetricDecrypt(ByVal text As String, ByVal password As String) As String
            Dim k As Integer
            Dim a As Integer
            Dim _returnText As String
            For i As Integer = 1 To password.Length
                k = k + (Asc(Mid(password, i, 1)) * i)
                If k > 255 Then k = k - 255
            Next

            Do While k > 255
                k = k - 255
            Loop

            For iii As Integer = 1 To text.Length
                a = Asc(Mid(text, iii, 1)) - k
                If a < 0 Then a = a + 255
                _returnText = _returnText + Chr(a)
                k = k + Asc(Mid(text, iii, 1))
                If k > 255 Then k = k - 255
            Next
            Return _returnText
        End Function

        Public Shared Function ClearEnterCharacter(ByVal text As String) As String
            Dim a As Integer
            Dim _returnText As String
            Dim _byte As Byte() = (New UnicodeEncoding).GetBytes(text)

            For Each item As Byte In _byte
                If item <> 0 Then
                    If item = 13 Then
                        _returnText = _returnText & "<br>"
                    Else
                        If item = 10 Then
                            item = 32
                        End If
                        _returnText = _returnText + Chr(item)
                    End If

                End If
            Next
            Return _returnText
        End Function
    End Class

    
End Namespace

