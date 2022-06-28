Imports KTB.DNet.Domain
Imports ktb.DNet.BusinessFacade
Imports System.Web.SessionState
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search

Namespace KTB.DNet.UI
    Public Class AlertManagementHTTPHandler
        Implements System.Web.IHttpHandler
        Implements IRequiresSessionState

        Public ReadOnly Property IsReusable() As Boolean Implements System.Web.IHttpHandler.IsReusable
            Get
                Return True
            End Get
        End Property

        Private Function GetHTMLOpenTagIndex(ByVal str As String)
            Return str.IndexOf("<")
        End Function

        Private Function GetHTMLCloseTagIndex(ByVal str As String)
            Return str.IndexOf(">")
        End Function

        Private Function RemoveAllHTMLTags(ByVal str As String) As String

            Dim startIndex As Integer = GetHTMLOpenTagIndex(str)
            While startIndex >= 0
                Dim endIndex As Integer = GetHTMLCloseTagIndex(str)

                If endIndex > 0 Then
                    str = str.Remove(startIndex, endIndex - startIndex + 1)

                Else

                    str = str.Remove(startIndex, str.Length - startIndex)

                End If

                startIndex = GetHTMLOpenTagIndex(str)
            End While

            Return str.Replace("&nbsp;", "")
        End Function

        Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest

            Dim _shelper As New SessionHelper
            Dim uafacade As New AlertManagement.UserAlertFacade(System.Threading.Thread.CurrentPrincipal)
            Dim asFacade As New AlertManagement.AlertSoundFacade(System.Threading.Thread.CurrentPrincipal)
            Dim amFacade As New AlertManagement.AlertMasterFacade(System.Threading.Thread.CurrentPrincipal)
            Dim nhFacade As New General.NationalHolidayFacade(System.Threading.Thread.CurrentPrincipal)
            '  dim objuserinfo as userinfo = ctype(session("loginuserinfo"), userinfo)
            Dim objuserinfo As UserInfo = CType(_shelper.GetSession("loginuserinfo"), UserInfo)


            Dim response As HttpResponse = context.Response

            Dim IsNowHoliday As Boolean
            Dim result As Integer = nhFacade.IsActiveDateExist(Today.Year, Today.Month, Today.Day)
            If result = 0 Then
                IsNowHoliday = False
            Else
                IsNowHoliday = True
            End If

            Dim strSql As String = "select AlertMasterID from AlertGroup where UserGroupID in(Select UserGroupID from userGroupMember where UserID=" & objuserinfo.ID.ToString & ")"

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AlertMaster), "IsViaAlertBox", MatchType.Exact, 1))

            If IsNowHoliday Then
                criterias.opAnd(New Criteria(GetType(AlertMaster), "IsIncludeHoliday", MatchType.Exact, 1))
            End If

            criterias.opAnd(New Criteria(GetType(AlertMaster), "DateValidFrom", MatchType.LesserOrEqual, Date.Today))
            criterias.opAnd(New Criteria(GetType(AlertMaster), "DateValidTo", MatchType.GreaterOrEqual, Date.Today))

            criterias.opAnd(New Criteria(GetType(AlertMaster), "TimeStartFrom", MatchType.LesserOrEqual, New DateTime(1900, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, 0)))
            criterias.opAnd(New Criteria(GetType(AlertMaster), "TimeStartTo", MatchType.GreaterOrEqual, New DateTime(1900, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, 0)))


            criterias.opAnd(New Criteria(GetType(AlertMaster), "ID", MatchType.InSet, "(" & strSql & ")"))

            Dim listAlert As ArrayList = amFacade.Retrieve(criterias)
            Dim alertList As New ArrayList

            Dim lastShown As DateTime
            Dim NextShown As DateTime
            Dim arlUserAlert As ArrayList
            For Each item As AlertMaster In listAlert
                Dim criteriaUser As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserAlert), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaUser.opAnd(New Criteria(GetType(UserAlert), "UserInfo.ID", MatchType.Exact, objuserinfo.ID))
                criteriaUser.opAnd(New Criteria(GetType(UserAlert), "AlertMaster.ID", MatchType.Exact, item.ID))
                arlUserAlert = uafacade.Retrieve(criteriaUser)

                Dim objUserAlert As UserAlert
                If arlUserAlert.Count > 0 Then
                    objUserAlert = CType(arlUserAlert.Item(0), UserAlert)

                    lastShown = objUserAlert.LastUpdateTime
                    Select Case item.ViaAlertBoxFreqType.Trim
                        Case EnumAlertManagement.AlertMediaType.Menit
                            NextShown = lastShown.AddMinutes(item.ViaAlertBoxFrequency)
                        Case EnumAlertManagement.AlertMediaType.Jam
                            NextShown = lastShown.AddHours(item.ViaAlertBoxFrequency)
                        Case EnumAlertManagement.AlertMediaType.Hari
                            NextShown = lastShown.AddDays(item.ViaAlertBoxFrequency)
                        Case EnumAlertManagement.AlertMediaType.Minggu
                            NextShown = lastShown.AddDays(item.ViaAlertBoxFrequency * 7)
                        Case EnumAlertManagement.AlertMediaType.Bulan
                            NextShown = lastShown.AddMonths(item.ViaAlertBoxFrequency)
                        Case EnumAlertManagement.AlertMediaType.Tahun
                            NextShown = lastShown.AddYears(item.ViaAlertBoxFrequency)
                    End Select
                    If NextShown < DateTime.Now Then
                        alertList.Add(item)
                        result = uafacade.Update(objUserAlert)
                    End If
                Else
                    objUserAlert = New UserAlert
                    objUserAlert.AlertMaster = item
                    objUserAlert.UserInfo = objuserinfo
                    alertList.Add(item)
                    result = uafacade.Insert(objUserAlert)
                End If


            Next

            'SendingEmail()

            context.Response.Expires = 0
            'Dim strJSON As String = ""

            'strJSON += "'Desc': '" + DateTime.Now.ToString() + "'"
            'strJSON += ", 'SoundFile' : 'tada.wav'"

            'strJSON = "[{" + strJSON + "}]"

            'response.Write(strJSON)

            'response.Flush()
            'response.End()

            '--------------------

            Dim strJSON As String = ""
            'Dim alertList As New ArrayList
            'clear the cache
            'objuserinfo.UserAlerts.Clear()
            'Dim alertsCount As Integer = objuserinfo.UserAlerts.Count - 1
            'While alertsCount >= 0
            '    Dim objUserAlert As UserAlert = CType(objuserinfo.UserAlerts(alertsCount), UserAlert)
            '    If objUserAlert.AlertMaster.IsViaAlertBox Then 'nanti jika ada enum akan diubah
            '        alertList.Add(objUserAlert.AlertMaster)
            '        'delete setelah dimasukin ke arrayList                
            '        uafacade.DeleteFromDB(objUserAlert)
            '        objuserinfo.UserAlerts.RemoveAt(alertsCount)
            '    End If
            '    alertsCount -= 1
            'End While

            Dim number As Integer = 1
            If alertList.Count > 0 Then
                For Each objAlertMaster As AlertMaster In alertList
                    Dim strDesc As String = objAlertMaster.Desc
                    If strDesc.Trim().StartsWith("<p>") Then
                        strDesc = strDesc.Substring(3, strDesc.Length - 3)
                        strDesc = strDesc.Substring(0, strDesc.Length - 4)
                    End If
                    strDesc = RemoveAllHTMLTags(strDesc)
                    'strJSON += "{'Desc':'" + strDesc.Replace("'", "\'") + "'"
                    ' strJSON += ",'SoundFile':'DataFile/Wav/Test.wav'}"

                    If alertList.Count = 1 Then
                        strJSON &= strDesc & Chr(13)
                    Else
                        strJSON &= Chr(13) & "Alert " & number.ToString & ":" & Chr(13) & strDesc & Chr(13)
                    End If

                    'Dim selectedAlertSound As String = GetSelectedAlertSound(objAlertMaster)

                    'If selectedAlertSound.Trim().Length > 0 Then
                    '    selectedAlertSound = CStr(KTB.DNet.Lib.WebConfig.GetValue("SAN")) + selectedAlertSound
                    'End If

                    'strJSON += ",'SoundFile':'" & selectedAlertSound.Replace("'", "\'") & "'}"
                    'If (number < alertList.Count) Then
                    '    strJSON += ","
                    'End If
                    number += 1
                Next
            End If
            'If strJSON <> "" Then strJSON &= Chr(13)
            'strJSON = "[" + strJSON + "]"

            response.Write(strJSON)

            response.Flush()

            response.End()

        End Sub

        Private Function GetSelectedAlertSound(ByVal alert As AlertMaster) As String
            For Each sound As AlertSound In alert.AlertSounds
                If sound.IsSelected Then
                    Return sound.FilePath
                End If
            Next
            Return String.Empty
        End Function
    End Class
End Namespace

