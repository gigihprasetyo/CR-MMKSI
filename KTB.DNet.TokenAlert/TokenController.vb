Imports KTB.DNet.Lib
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Security.Principal
Imports System.Web.Mail


Namespace KTB.DNet.TokenAlert
    Public Class TokenController

        ''' <summary>
        ''' This Function Will Get List Of ExpiredNumber 
        ''' With _dayRemider as Parameter
        ''' Handphone Number place on second column (number 1)
        ''' </summary>
        ''' <remarks>by ags on 09 Desember 09</remarks>
        Public Function GetListPhone(ByVal _dayReminder As Integer) As ArrayList
            Dim _vGetTokenAlertfacade As New v_GetTokenExpiredFacade
            Dim _listTokenExpired As New ArrayList
            Dim crtTokenAlertParam As CriteriaComposite

            crtTokenAlertParam = New CriteriaComposite(New Criteria(GetType(v_GetTokenExpired), "DayRemind", MatchType.Exact, _dayReminder))
            _listTokenExpired = _vGetTokenAlertfacade.Retrieve(crtTokenAlertParam)

            Return _listTokenExpired

        End Function

        Public Function GetListPhone(ByVal _dayReminderFrom As Integer, ByVal _dayReminderTo As Integer) As ArrayList
            Dim _vGetTokenAlertfacade As v_GetTokenExpiredFacade = New v_GetTokenExpiredFacade

            Dim _listTokenExpired As New ArrayList
            Dim crtTokenAlertParam As CriteriaComposite

            crtTokenAlertParam = New CriteriaComposite(New Criteria(GetType(v_GetTokenExpired), "DayRemind", MatchType.GreaterOrEqual, _dayReminderFrom))
            crtTokenAlertParam.opAnd(New Criteria(GetType(v_GetTokenExpired), "DayRemind", MatchType.LesserOrEqual, _dayReminderTo))

            _listTokenExpired = _vGetTokenAlertfacade.Retrieve(crtTokenAlertParam)

            Return _listTokenExpired

        End Function
        Public Function GetListPhone(ByVal _dayReminderFrom As Integer, ByVal _dayReminderTo As Integer, ByVal _strOpt As String) As ArrayList
            Dim _vGetTokenAlertfacade As v_GetTokenExpiredFacade = New v_GetTokenExpiredFacade

            Dim _listTokenExpired As New ArrayList
            Dim crtTokenAlertParam As CriteriaComposite

            crtTokenAlertParam = New CriteriaComposite(New Criteria(GetType(v_GetTokenExpired), "DayRemind", MatchType.GreaterOrEqual, _dayReminderFrom))
            crtTokenAlertParam.opAnd(New Criteria(GetType(v_GetTokenExpired), "DayRemind", MatchType.LesserOrEqual, _dayReminderTo))
            crtTokenAlertParam.opAnd(New Criteria(GetType(v_GetTokenExpired), "DayAlertStatus", MatchType.IsNull, _dayReminderFrom), "(", True)
            crtTokenAlertParam.opOr(New Criteria(GetType(v_GetTokenExpired), "DayAlertStatus", MatchType.Lesser, _dayReminderFrom))
            crtTokenAlertParam.opOr(New Criteria(GetType(v_GetTokenExpired), "DayAlertStatus", MatchType.Greater, _dayReminderTo), ")", False)

   

            _listTokenExpired = _vGetTokenAlertfacade.Retrieve(crtTokenAlertParam)

            Return _listTokenExpired

        End Function

        ''' <summary>
        ''' This Method Will Send SMS Message to All Handphone Number in Array List
        ''' Handphone Number And Send Email Message
        ''' </summary>
        ''' <remarks>by ags on 09 Desember 09</remarks>
        Public Sub SendAlertToAll(ByVal _listTokenExpired As ArrayList, ByVal msgSmsContent As String)
            Dim TokenObj As v_GetTokenExpired

            Dim StringEmailPIC As String = WebConfig.GetValue("EmailPIC")
            Dim TitleEmailPIC As String = WebConfig.GetValue("TitleEmailPIC")

            Dim TitleEmail As String = WebConfig.GetValue("TitleEmail")

            Dim messageHistory As System.Text.StringBuilder = New StringBuilder
            Dim messageHistoryEmail As System.Text.StringBuilder = New StringBuilder
            Dim messageHistoryMailFailed As System.Text.StringBuilder = New StringBuilder
            Dim messageContent As String = String.Empty

            messageHistory.Append("<Table border=1 cellpadding=0 cellspacing=0>")
            messageHistory.Append("<TR><TH><B>DEALER CODE</B></TH><TH><B>USER NAME</B></TH><TH><B>NO HANDPHONE</B></TH><TH><B>TIME</B></TH><TH><B>STATUS</B></TH></TR>")


            messageHistoryEmail.Append("<Table border=1 cellpadding=0 cellspacing=0>")
            messageHistoryEmail.Append("<TR><TH><B>DEALER CODE</B></TH><TH><B>USER NAME</B></TH><TH><B>EMAIL ADDRESS</B></TH><TH><B>TIME</B></TH><TH><B>STATUS</B></TH></TR>")
            Try
                For Each TokenObj In _listTokenExpired
                    Try
                        'Status Pengiriman SMS
                        If TokenObj.Handphone <> "" Then

                            messageContent = Utility.MessageContent(TokenObj, msgSmsContent)

                            If Utility.SendSMS(TokenObj, messageContent) Then
                                EntryLogFile(TokenObj, 1)
                                TokenObj.LastUpdateBy = "SUKSES"
                                messageHistory.Append(Utility.LogMessageContent(TokenObj, 3))
                                UpdateUserInfo(TokenObj)
                            Else
                                TokenObj.LastUpdateBy = "GateWay Down"
                                EntryLogFile(TokenObj, 2)
                            End If

                        Else
                            TokenObj.LastUpdateBy = "GAGAL"
                            EntryLogFile(TokenObj, 6)
                            messageHistory.Append(Utility.LogMessageContent(TokenObj, 4))

                        End If



                    Catch ex As Exception
                        TokenObj.LastUpdateBy = ex.Message
                        EntryLogFile(TokenObj, 2)
                        TokenObj.LastUpdateBy = "GAGAL " + "<font color=Red>" + ex.Message + "</font>"
                        messageHistory.Append(Utility.LogMessageContent(TokenObj, 3))
                    End Try

                    Try
                        'Status Pengiriman Email
                        If TokenObj.Email <> String.Empty Then
                            SendEmail(TokenObj, TitleEmail)
                            TokenObj.LastUpdateBy = "SUKSES"
                            messageHistoryEmail.Append(Utility.LogMessageContent(TokenObj, 9))
                        Else
                            TokenObj.LastUpdateBy = "GAGAL"
                            messageHistoryEmail.Append(Utility.LogMessageContent(TokenObj, 7))

                        End If
                    Catch ex As Exception
                        TokenObj.LastUpdateBy = "GAGAL :" + "<font color=Red>" + ex.Message + "</font>"
                        messageHistoryEmail.Append(Utility.LogMessageContent(TokenObj, 9))
                    End Try

                    Dim _intDelayTime As Integer = CType(WebConfig.GetValue("DelayTime"), Integer)
                    System.Threading.Thread.Sleep(_intDelayTime)

                Next
                EntryLogFile(TokenObj, 9999)
            Catch ex As Exception

            End Try
           
            messageHistory.Append("</Table>")
            messageHistoryEmail.Append("</Table>")

            Utility.SendEmail(StringEmailPIC, TitleEmailPIC, messageHistory.ToString())
            Utility.SendEmail(StringEmailPIC, "[MMKSI-DNet] Status Pengiriman Email", messageHistoryEmail.ToString())


        End Sub




        ''' <summary>
        ''' This Function Will Create Log File With LogType=1-->SuccesSend SMS Message
        ''' LogType=0-->Failed Send SMS Message
        ''' </summary>
        ''' <remarks>by ags on 09 Desember 09</remarks>
        Protected Function CreateLogFile(ByVal _logType As Integer, ByVal _logFileName As String) As String
            'GetPath
            Dim aPath As String
            Dim aName As String

            aName = _
              System.Reflection.Assembly.GetExecutingAssembly. _
              GetModules()(0).FullyQualifiedName

            aPath = System.IO.Path.GetDirectoryName(aName)
            'CreateDirectory
            Utility.CreateDir(aPath + "\LogFile")
            _logFileName = aPath + "\LogFile\" + _logFileName

            'CreateFile First

            Dim _retVal As String
            _retVal = _logFileName + "Success" + "__" + Guid.NewGuid().ToString()


            If _logType <> 1 Then
                _retVal = _logFileName + "Failed" + "__" + Guid.NewGuid().ToString()
            End If
            _retVal = _retVal + ".txt"
            Utility.CreateFile(_retVal)

            Return _retVal


        End Function

        Public Sub CreateLogFile()
            Utility.CreateFile("TokenNotify.log")

        End Sub
        Protected Sub UpdateUserInfo(ByVal TokenObj As v_GetTokenExpired)

            Dim _userInfoFacade As New UserInfoFacade(New GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim _userInfo As UserInfo = New UserInfo
            _userInfo = GetUserInfoById(TokenObj)
            _userInfo.TokenAlertTime = DateTime.Now
            _userInfoFacade.Update(_userInfo)

        End Sub

        Protected Function GetUserInfoById(ByVal TokenObj As v_GetTokenExpired) As UserInfo

            Dim _userInfoFacade As New UserInfoFacade(New GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim _UserInfo As UserInfo = New UserInfo
            _UserInfo = _userInfoFacade.Retrieve(TokenObj.ID)

            Return _UserInfo


        End Function

        Public Sub SendEmail(ByVal TokenObj As v_GetTokenExpired, ByVal _titleEmail As String)
            _titleEmail = _titleEmail & Utility.GetIPv4Address()
            Dim smtp As String = WebConfig.GetValue("SMTP")
            Dim mail As DNetMail = New DNetMail(smtp)
            Const TEMP_EMAIL_TOKENALERT = "EmailTemplate.htm"
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) & "\" & TEMP_EMAIL_TOKENALERT)
            Dim szEmailFormat As String = sr.ReadToEnd()
            sr.Close()

            Dim szTos As String = String.Empty 'cc
            Dim szCcs As String = String.Empty ''cc1
            Dim szItems As String = ""
            Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")

            Dim szFormats() As String = {TokenObj.Name, TokenObj.DateRemind.ToString("dd MMMM yyyy"), TokenObj.Handphone, TokenObj.ActivationCode, TokenObj.DealerCode, TokenObj.username}
            Dim szEmailContent As String = String.Format(szEmailFormat, szFormats)

            'mail.sendMail(TEMP_EMAIL_TOKENALERT, szTos, szCcs, _titleEmail, szFormats)

            Utility.SendEmail(TokenObj.Email, _titleEmail, szEmailContent)


        End Sub

        Public Sub EntryLogFile(ByVal TokenObj As v_GetTokenExpired, ByVal _opt As Integer)
            If _opt = 1 Then
                Utility.SaveTextToFile(Utility.LogMessageContent(TokenObj, 1), "TokenNotify.log", True, "")
            ElseIf _opt = 2 Then
                Utility.SaveTextToFile(Utility.LogMessageContent(TokenObj, 2) + TokenObj.LastUpdateBy, "TokenNotify.log", True, "")
            ElseIf _opt = 6 Then
                Utility.SaveTextToFile(Utility.LogMessageContent(TokenObj, 6), "TokenNotify.log", True, "")
            Else
                Utility.SaveTextToFile("------------------------------------------------", "TokenNotify.log", True, "")
            End If

        End Sub



    End Class

End Namespace
