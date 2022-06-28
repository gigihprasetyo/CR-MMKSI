Imports System.IO
Imports KTB.DNet.Utility
Imports System.Web
Imports System.Web.Mail
Imports KTB.DNet.Lib
Imports KTB.DNet.Domain

Namespace KTB.DNet.TokenAlert
    Public Class Utility


        Public Shared Function GetIPv4Address() As String
            GetIPv4Address = String.Empty
            'Dim strHostName As String = System.Net.Dns.GetHostName()
            'Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

            'For Each ipheal As System.Net.IPAddress In iphe.AddressList
            '    If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
            '        GetIPv4Address = ipheal.ToString() & System.Configuration.ConfigurationSettings.AppSettings.Get("AppID")
            '    End If
            'Next

        End Function

        Public Shared Function GetFileContents(ByVal FullPath As String, _
           Optional ByRef ErrInfo As String = "") As String

            Dim strContents As String
            Dim objReader As StreamReader
            Try

                objReader = New StreamReader(FullPath)
                strContents = objReader.ReadToEnd()
                objReader.Close()
                Return strContents
            Catch Ex As Exception
                ErrInfo = Ex.Message
            End Try
        End Function

        Public Shared Function SaveTextToFile(ByVal strData As String, _
         ByVal FullPath As String, _
           Optional ByVal ErrInfo As String = "") As Boolean

            Dim Contents As String
            Dim bAns As Boolean = False
            Dim objReader As StreamWriter
            Try
                objReader = New StreamWriter(FullPath)
                objReader.WriteLine(strData)
                objReader.Close()
                bAns = True
            Catch Ex As Exception
                ErrInfo = Ex.Message

            End Try
            Return bAns
        End Function
        Public Shared Function SaveTextToFile(ByVal strData As String, _
         ByVal FullPath As String, ByVal bolAppend As Boolean, _
           Optional ByVal ErrInfo As String = "") As Boolean

            Dim Contents As String
            Dim bAns As Boolean = False
            Dim objReader As StreamWriter
            Try
                objReader = New StreamWriter(FullPath, bolAppend)
                objReader.WriteLine(strData)
                objReader.Close()
                bAns = True
            Catch Ex As Exception
                ErrInfo = Ex.Message

            End Try
            Return bAns
        End Function

        Public Shared Function SaveTextToFile(ByVal strDataArray As String(), _
        ByVal FullPath As String, _
          Optional ByVal ErrInfo As String = "") As Boolean

            Dim Contents As String
            Dim bAns As Boolean = False
            Dim objReader As StreamWriter
            Try

                objReader = New StreamWriter(FullPath)
                Dim strData As String
                For Each strData In strDataArray
                    objReader.WriteLine(strData)
                Next
                objReader.Close()
                bAns = True
            Catch Ex As Exception
                ErrInfo = Ex.Message

            End Try
            Return bAns
        End Function


        Public Shared Sub CreateFile(ByVal _fileName As String)
            Dim oWrite As System.IO.StreamWriter
            'Dim oFile As System.IO.File
            If Not System.IO.File.Exists(_fileName) Then
                oWrite = File.CreateText(_fileName)
            End If



        End Sub
        Public Shared Sub CreateDir(ByVal _dirName As String)
            'Dim oWrite As System.IO.
            'Dim oDir As System.IO.Directory
            Dim oDirInfo As System.IO.DirectoryInfo
            oDirInfo = Directory.CreateDirectory(_dirName)

        End Sub

        Public Shared Sub SendEmail(ByVal emailTo As String, ByVal contentEmail As String)
            Dim emailFrom As String = WebConfig.GetValue("EmailFrom")
            Dim smtp As String = WebConfig.GetValue("SMTP")
            Dim ObjEmail As DNetMail = New DNetMail(smtp)
            Try
                ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet]", Mail.MailFormat.Html, contentEmail)
            Catch ex As Exception

            End Try

        End Sub

        Public Shared Sub SendEmail(ByVal emailTo As String, ByVal titleEmail As String, ByVal contentEmail As String)
            titleEmail = titleEmail & Utility.GetIPv4Address()
            Dim emailFrom As String = WebConfig.GetValue("EmailFrom")
            Dim smtp As String = WebConfig.GetValue("SMTP")
            Dim ObjEmail As DNetMail = New DNetMail(smtp)
            Try
                ObjEmail.sendMail(emailTo, "", emailFrom, titleEmail, Mail.MailFormat.Html, contentEmail)
            Catch ex As Exception

            End Try

        End Sub


        Public Shared Function SendSMS(ByVal _stringPhNumber As String, ByVal _strMessage As String) As Boolean

            'Dim objSMS As [Lib].Sms = New [Lib].Sms
            If Sms.IsSMSGatewayLive Then
                Sms.Sendto(_stringPhNumber, _strMessage)
                Return True
            Else
                Return False

            End If
        End Function
        Public Shared Function SendSMS(ByVal TokenObj As v_GetTokenExpired, ByVal _strMessage As String) As Boolean

            'Dim objSMS As [Lib].Sms = New [Lib].Sms
            If Sms.IsSMSGatewayLive Then
                Sms.Sendto(TokenObj.Handphone, _strMessage)
                Return True
            Else
                Return False

            End If
        End Function

        Public Shared Function MessageContent(ByVal TokenObj As v_GetTokenExpired, ByVal msgSmsContent As String) As String
            Dim szFormats() As String = {TokenObj.DateRemind.ToString("dd MMMM yyyy")}
            Dim _retValue As String = String.Format(msgSmsContent, szFormats) '= msgSmsContent1 + TokenObj.DateRemind.ToString("dd MMMM yyyy") + " " + msgSmsContent2
            Return _retValue
        End Function

        Public Shared Function LogMessageContent(ByVal TokenObj As v_GetTokenExpired, ByVal _optional As Integer) As String
            Dim _retValue As String = String.Empty
            Select Case _optional
                Case 1 'SMS-Success Log File
                    _retValue = TokenObj.DealerCode.ToString() + "_" + TokenObj.UserName + "_" + TokenObj.Handphone + "_" + DateTime.Now.ToString.ToString()
                Case 2 'SMS Failed Log File
                    _retValue = TokenObj.DealerCode.ToString() + "_" + TokenObj.UserName + "_" + TokenObj.Handphone + "_" + DateTime.Now.ToString.ToString() + "FAILED_" + TokenObj.LastUpdateBy
                Case 3 'SMS Log Email Notification
                    _retValue = "<TR/><TD>" + TokenObj.DealerCode.ToString() + "</TD><TD>" + TokenObj.UserName + "</TD><TD>" + TokenObj.Handphone + "</TD><TD>" + DateTime.Now.ToString.ToString() + "</TD><TD>" + TokenObj.LastUpdateBy + "</TD></TR>"
                Case 4 'SMS NoNumberPhone Log Email
                    _retValue = "<TR/><TD>" + TokenObj.DealerCode.ToString() + "</TD><TD>" + TokenObj.UserName + "</TD><TD>" + "NO NUMBER" + "</TD><TD>" + DateTime.Now.ToString.ToString() + "</TD><TD>" + TokenObj.LastUpdateBy + "</TD></TR>"
                Case 5 'Mail-Failed Log Email Notification

                Case 6 'SMS NoNumberPhone Log File
                    _retValue = TokenObj.DealerCode.ToString() + "_" + TokenObj.UserName + "_NoNumber_" + DateTime.Now.ToString.ToString()
                Case 7 'NoEmailAddress
                    _retValue = "<TR/><TD>" + TokenObj.DealerCode.ToString() + "</TD><TD>" + TokenObj.UserName + "</TD><TD>" + "NoEmailAddress" + "</TD><TD>" + DateTime.Now.ToString.ToString() + "</TD><TD>" + TokenObj.LastUpdateBy + "</TD></TR>"

                Case 8 'Gateway Down
                    _retValue = TokenObj.DealerCode.ToString() + "_" + TokenObj.UserName + "_GateWayDown_" + TokenObj.Handphone + "_" + DateTime.Now.ToString.ToString()
                Case 9 'success Send Email For Each TokenObj
                    _retValue = "<TR/><TD>" + TokenObj.DealerCode.ToString() + "</TD><TD>" + TokenObj.UserName + "</TD><TD>" + TokenObj.Email + "</TD><TD>" + DateTime.Now.ToString.ToString() + "</TD><TD>" + TokenObj.LastUpdateBy + "</TD></TR>"


            End Select
            Return _retValue
        End Function



    End Class
End Namespace

