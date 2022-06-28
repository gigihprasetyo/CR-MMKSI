Imports System.Collections
Imports System.Threading

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.AlertManagement    

Module MainModule
    Dim NULL_DATETIME As DateTime = New DateTime(1900, 1, 1, 0, 0, 0)
    Const MINUTES_TO_CACHE As Integer = 21

    Dim arl As ArrayList    
    ReadOnly listLock As New Object
    Dim rnd As New Random

    Dim DashboardQueue As New Queue
    Dim AlertBoxQueue As New Queue
    Dim SMSQueue As New Queue
    Dim EmailQueue As New Queue

    ReadOnly dashboardWorkerLock As New Object
    ReadOnly alertBoxWorkerLock As New Object
    ReadOnly smsWorkerLock As New Object
    ReadOnly emailWorkerLock As New Object
    ReadOnly dbWorkerLock As New Object
    ReadOnly mainThreadLock As New Object
    ReadOnly syslogWorkerLock As New Object

    Dim exitSignal As Boolean = False
    Dim bIsProcessing As Boolean = False
    Dim nextRunForDBWorker As DateTime
    Sub Main()
        arl = ArrayList.Synchronized(New ArrayList)
        Log("Creating DashboardQueue")
        DashboardQueue = Queue.Synchronized(New Queue)
        Log("Creating AlertBoxQueue")
        AlertBoxQueue = Queue.Synchronized(New Queue)
        Log("Creating SMSQueue")
        SMSQueue = Queue.Synchronized(New Queue)
        Log("Creating EmailQueue")
        EmailQueue = Queue.Synchronized(New Queue)

        Thread.CurrentThread.Name = "MainThread"

        nextRunForDBWorker = DateTime.Now

        Log("Creating DBWorker")
        Dim DBWorker As New Thread(AddressOf DBWorkerMethod)
        DBWorker.Name = "DBWorker"
        DBWorker.IsBackground = True
        'We comments the dashboard processing because for dashboard alerts we dont have to use
        'periodic processing
        'Log("Creating dashboardWorker")
        'Dim dashboardWorker As New Thread(AddressOf DashboardWorkerMethod)
        'dashboardWorker.Name = "DashboardWorker"
        'dashboardWorker.IsBackground = True

        Log("Creating alertBoxWorker")
        Dim alertBoxWorker As New Thread(AddressOf AlertBoxWorkerMethod)
        alertBoxWorker.Name = "AlertBoxWorker"
        alertBoxWorker.IsBackground = True

        Log("Creating smsWorker")
        Dim smsWorker As New Thread(AddressOf SMSWorkerMethod)
        smsWorker.Name = "SMSWorker"
        smsWorker.IsBackground = True

        Log("Creating emailWorker")
        Dim emailWorker As New Thread(AddressOf EmailWorkerMethod)
        emailWorker.Name = "EmailWorker"
        emailWorker.IsBackground = True

        Log("Creating Syslog worker")
        Dim syslogWorker As New Thread(AddressOf SyslogWorkerMethod)
        syslogWorker.Name = "SyslogWorker"
        syslogWorker.IsBackground = True


        Log("Starting DBWorker")
        DBWorker.Start()

        'We comments the dashboard processing because for dashboard alerts we dont have to use
        'periodic processing
        'Log("Starting dashboardWorker")
        'dashboardWorker.Start()
        Log("Starting alertBoxWorker")
        alertBoxWorker.Start()
        Log("Starting smsWorker")
        smsWorker.Start()
        Log("Starting emailWorker")
        emailWorker.Start()
        Log("Starting SyslogWorker")
        syslogWorker.Start()

        Dim DoExit As Boolean = False

        Dim timerProcessing As New System.Threading.Timer(New TimerCallback(AddressOf TimerProcessingCallback), Nothing, 0, 500)
        Dim timerForSyslog As New System.Threading.Timer(New TimerCallback(AddressOf TimerForSyslogCallback), Nothing, 0, 1000 * 60 * MINUTES_TO_CACHE)
        Dim timerForDBWorker As New System.Threading.Timer(New TimerCallback(AddressOf DBWorkerTimerCallback), Nothing, 0, 1000 * 60 * MINUTES_TO_CACHE)

        Dim charCode As Integer
        Do
            SyncLock mainThreadLock
                charCode = Console.Read
            End SyncLock            
        Loop While charCode <> 13
        'SyncLock dbWorkerLock
        '    do

        '        While arl.Count = 0
        '            Monitor.Wait(dbWorkerLock)
        '        End While               
        '    Loop
        'End SyncLock


        exitSignal = True
        SyncLock dashboardWorkerLock
            Monitor.Pulse(dashboardWorkerLock)
        End SyncLock
        SyncLock alertBoxWorkerLock
            Monitor.Pulse(alertBoxWorkerLock)
        End SyncLock

        SyncLock smsWorkerLock
            Monitor.Pulse(smsWorkerLock)
        End SyncLock

        SyncLock emailWorkerLock
            Monitor.Pulse(emailWorkerLock)
        End SyncLock

        'dashboardWorker.Join()
        'alertBoxWorker.Join()
        'smsWorker.Join()
        'emailWorker.Join()

    End Sub

    Private Function CalculateNextRun(ByVal currentDateTime As DateTime, ByVal AlertFrequency As Integer, ByVal AlertFrequencyType As Integer) As DateTime

        Dim result As DateTime
        Select Case CType(AlertFrequencyType, EnumAlertManagement.AlertMediaType)
            Case EnumAlertManagement.AlertMediaType.Menit
                result = currentDateTime.AddMinutes(AlertFrequency)                
            Case EnumAlertManagement.AlertMediaType.Jam
                result = currentDateTime.AddHours(AlertFrequency)
            Case EnumAlertManagement.AlertMediaType.Hari
                result = currentDateTime.AddDays(AlertFrequency)
            Case EnumAlertManagement.AlertMediaType.Minggu
                result = currentDateTime.AddDays(AlertFrequency * 7)
            Case EnumAlertManagement.AlertMediaType.Bulan
                result = currentDateTime.AddMonths(AlertFrequency)
            Case EnumAlertManagement.AlertMediaType.Tahun
                result = currentDateTime.AddYears(AlertFrequency)
            Case Else
                LogError("Invalid Alert frequency type.")
        End Select

        result = New DateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, 0)

        Return result

    End Function

    Private Function IfAllNextRunNotWithinTimeRange(ByVal alert As AlertMaster) As Boolean
        Dim IsNotWithinTimeRange As Boolean = (DateTime.op_GreaterThan(alert.NextRunForAlertBox, nextRunForDBWorker) And _
                    DateTime.op_GreaterThan(alert.NextRunForDashboard, nextRunForDBWorker) And _
                    DateTime.op_GreaterThan(alert.NextRunForEmail, nextRunForDBWorker) And _
                    DateTime.op_GreaterThan(alert.NextRunForSMS, nextRunForDBWorker))

        Log("Alert " + alert.ID.ToString + " IsNotWithinTimeRange = " + IsNotWithinTimeRange.ToString())

        Return IsNotWithinTimeRange
    End Function

    Private Function IsOneTimeAlert(ByVal alert As AlertMaster)
        Return (CType(alert.AnnouncementAlertType, EnumAlertManagement.AnnouncementAlertType) = EnumAlertManagement.AnnouncementAlertType.OneTimeAlert)
    End Function

    Private Sub Log(ByVal str As String)
        Dim threadName As String = Thread.CurrentThread.Name
        If threadName Is Nothing Then
            threadName = "MainThread"
        ElseIf threadName.Length <= 0 Then
            threadName = "MainThread"
        End If
        Dim logger As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        logger.Info(String.Format("[{0} : {2}]:{1}", threadName, str, DateTime.Now.ToLongTimeString()))
    End Sub

    Private Sub LogError(ByVal str As String)
        Dim threadName As String = Thread.CurrentThread.Name
        If threadName Is Nothing Then
            threadName = "MainThread"
        ElseIf threadName.Length <= 0 Then
            threadName = "MainThread"
        End If
        Dim logger As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
        logger.Error(String.Format("[{0} : {2}]:{1}", threadName, str, DateTime.Now.ToLongTimeString()))
    End Sub

    Private Sub TimerForSyslogCallback(ByVal state As Object)
        Log("TIMER SIGNALLED FOR SYSLOG")

        SyncLock syslogWorkerLock
            Monitor.Pulse(syslogWorkerLock)
        End SyncLock
    End Sub
    Private Sub TimerProcessingCallback(ByVal state As Object)
        Log("Checking data availability...")
        If bIsProcessing Then
            Log("Still processing, returning...")
            Exit Sub
        End If

        If arl.Count > 0 Then
            bIsProcessing = True
            Log(arl.Count.ToString + " datas is available. Processing...")

            Dim lastAlertID As Integer = 0
            Dim lastIndex As Integer = 0
            While arl.Count > 0

                Dim item As AlertMaster = CType(arl(lastIndex), AlertMaster)
                If lastAlertID = item.ID Then
                    lastIndex += 1
                    If lastIndex >= arl.Count Then Exit While
                    item = CType(arl(lastIndex), AlertMaster)
                End If

                Log("Processing alert : " + item.ID.ToString)
                Dim currentTime As DateTime = AlertDataRetriever.GetCurrentDateTime()
                Dim IsDoAlertUpdate As Boolean = False
                If DueAlert(item) Then
                    'If IsTransactionalAlert(item) Then

                    'Else
                    Dim newNextRun As DateTime
                    'We comments the dashboard processing because for dashboard alerts we dont have to use
                    'periodic processing
                    'If IsDueForDashboard(item, currentTime) Then
                    '    ProcessForDashboardToWorkerThread(item)

                    '    newNextRun = CalculateNextRun(currentTime, item.ViaDashboardFrequency, CInt(item.ViaDashboardFreqType))
                    '    If Not item.NextRunForDashboard.Equals(newNextRun) Then
                    '        Log("Updating " + item.ID.ToString + " NextRunForDashboard from " + item.NextRunForDashboard.ToString("dd/MM/yyyy hh:mm:ss") + " to " + newNextRun.ToString("dd/MM/yyyy hh:mm:ss"))
                    '        item.NextRunForDashboard = newNextRun
                    '        IsDoAlertUpdate = True
                    '    End If
                    'End If
                    If IsDueForAlertBox(item, currentTime) Then
                        ProcessForAlertBoxToWorkerThread(item)

                        newNextRun = CalculateNextRun(currentTime, item.ViaAlertBoxFrequency, CInt(item.ViaAlertBoxFreqType))
                        If Not item.NextRunForAlertBox.Equals(newNextRun) Then
                            Log("Updating " + item.ID.ToString + " NextRunForAlertBox from " + item.NextRunForAlertBox.ToString("dd/MM/yyyy hh:mm:ss") + " to " + newNextRun.ToString("dd/MM/yyyy hh:mm:ss"))
                            item.NextRunForAlertBox = newNextRun
                            IsDoAlertUpdate = True
                        End If
                    End If
                    If IsDueForSMS(item, currentTime) Then
                        ProcessForSMSToWorkerThread(item)

                        newNextRun = CalculateNextRun(currentTime, item.ViaSMSFrequency, CInt(item.ViaSMSFreqType))
                        If Not item.NextRunForSMS.Equals(newNextRun) Then
                            Log("Updating " + item.ID.ToString + " NextRunForSMS from " + item.NextRunForSMS.ToString("dd/MM/yyyy hh:mm:ss") + " to " + newNextRun.ToString("dd/MM/yyyy hh:mm:ss"))
                            item.NextRunForSMS = newNextRun
                            IsDoAlertUpdate = True
                        End If
                    End If
                    If IsDueForEmail(item, currentTime) Then
                        ProcessForEmailToWorkerThread(item)

                        newNextRun = CalculateNextRun(currentTime, item.ViaEmailFrequency, CInt(item.ViaEmailFreqType))
                        If Not item.NextRunForEmail.Equals(newNextRun) Then
                            Log("Updating " + item.ID.ToString + " NextRunForEmail from " + item.NextRunForEmail.ToString("dd/MM/yyyy hh:mm:ss") + " to " + newNextRun.ToString("dd/MM/yyyy hh:mm:ss"))
                            item.NextRunForEmail = newNextRun
                            IsDoAlertUpdate = True
                        End If
                    End If
                    'End If

                    If IsDoAlertUpdate Then
                        Dim alertMasterFacade As New AlertMasterFacade(GetUserPrincipal())
                        Log("Updating alert to DB")
                        alertMasterFacade.Update(item, item.AlertStatuss, item.AlertGroups, item.AlertSounds, False)
                    End If

                    If IfAllNextRunNotWithinTimeRange(item) Then
                        Log("Alert " + item.ID.ToString() + " is removed from list because its not within time range for DBWorker.")
                        arl.RemoveAt(lastIndex)
                        If lastIndex >= arl.Count Then Exit While
                    ElseIf IsOneTimeAlert(item) And Not IsTransactionalAlert(item) Then
                        Log("Alert " + item.ID.ToString() + " is removed from list because its a one-time alert.")
                        arl.RemoveAt(lastIndex)
                        If lastIndex >= arl.Count Then Exit While
                    End If
                Else
                    Log("Alert " + item.ID.ToString() + " is removed from list because its not due for current date.")
                    arl.RemoveAt(lastIndex)
                    If lastIndex >= arl.Count Then Exit While
                End If

                lastAlertID = item.ID

#If DEBUG Then
                'Thread.Sleep(500)
#End If
            End While
            bIsProcessing = False
        Else
            Log("No data available.")
        End If
    End Sub

    Private Sub ProcessSyslogDeferredMessages()
        'Titipan untuk syslog
        Dim syslogFacade As New KTB.DNet.BusinessFacade.General.SysLogFacade(GetUserPrincipal())

        Dim totalRows As Integer
        Dim syslogList As ArrayList = syslogFacade.RetrieveActiveList(1, 200, totalRows)
        log(String.Format("{0} data(s) retrieved from syslog.", syslogList.Count))
        For Each log As SysLog In syslogList
            Dim sysparam As New KTB.DNet.Utility.SyslogParameter

            Dim syslogmsg As KTB.DNet.Lib.SysLogXMLMessage = sysparam.GetSyslogParameter(log.Action, log.BlockName, log.FullMessage, _
                                                                log.ModuleName, log.Pages, log.RemoteIPAddress, log.ResultCode, _
                                                                log.Status, log.SubBlockName, log.UserName)
            Try
                sysparam.LogError(syslogmsg, True)
                syslogFacade.Delete(log)
                MainModule.Log(String.Format("Successfully process syslog message with ID: {0}", log.ID.ToString()))
            Catch ex As Exception
                MainModule.LogError(String.Format("Failed to process syslog{3}Primary server: {0}{3}Secondary server:{1}{3}Error Msg:{2}", sysparam.GetPrimaryServerName(), sysparam.GetSecondaryServerName(), ex.Message, vbNewLine))
                'just eat the exception
            End Try
        Next


        'End of Titipan
    End Sub

    Dim IsAlreadyRetrieving As Boolean = False

    Private Sub RetrieveDataFromDB()
        If IsAlreadyRetrieving Then Return
        IsAlreadyRetrieving = True

        'Clear old data
        'Must make sure that we are not clearing when the processing thread is still doing his job
        arl.Clear()

        Log("********************************************")
        Log("Retrieving from DB...")

        'Dim count As Integer = rnd.Next(1, 30)

        'For i As Integer = 1 To count
        '    Dim alert As New AlertMaster
        '    alert.Name = "Alert" + i.ToString() + "_" + DateTime.Now.ToLongTimeString()

        '    arl.Add(alert)
        'Next        

        Dim retriever As New AlertDataRetriever
        arl = ArrayList.Synchronized(retriever.Retrieve())

        Log(arl.Count.ToString() + " datas retrieved. Total: " + arl.Count.ToString)
        Log("********************************************")



        IsAlreadyRetrieving = False
        nextRunForDBWorker = nextRunForDBWorker.AddMinutes(MINUTES_TO_CACHE)

        'SyncLock dbWorkerLock
        '    Monitor.Pulse(dbWorkerLock)
        'End SyncLock
    End Sub

    Private Sub DBWorkerTimerCallback(ByVal state As Object)
        Log("TIMER SIGNALLED FOR DBWORKER")

        SyncLock dbWorkerLock
            Monitor.Pulse(dbWorkerLock)
        End SyncLock

    End Sub

    Private Sub DBWorkerMethod()
        While Not exitSignal
            SyncLock dbWorkerLock
                Monitor.Wait(dbWorkerLock)
            End SyncLock
            RetrieveDataFromDB()
        End While

    End Sub
    Private Function GetUserPrincipal() As System.Security.Principal.IPrincipal
        Dim ident As New System.Security.Principal.GenericIdentity("SYSTEM")
        Dim prin As New System.Security.Principal.GenericPrincipal(ident, New String() {})

        'Console.WriteLine("WARNING!!!! Still using dummy user, dont forget to change on production.")
        Return prin
    End Function

    Private Sub InsertToUserAlert(ByVal alert As AlertMaster)
        For Each grp As AlertGroup In alert.AlertGroups
            For Each member As UserGroupMember In grp.UserGroup.UserGroupMembers
                Dim objUserAlert As New UserAlert
                objUserAlert.UserInfo = member.UserInfo
                objUserAlert.AlertMaster = alert
                Dim facade As New UserAlertFacade(GetUserPrincipal)
                facade.Insert(objUserAlert)
            Next
        Next
    End Sub

    Private Sub DashboardWorkerMethod()
        While Not exitSignal
            SyncLock dashboardWorkerLock
                Log("DashboardWorkerMethod: DashboardQueue.Count = " + DashboardQueue.Count.ToString())
                While (DashboardQueue.Count = 0)
                    Monitor.Wait(dashboardWorkerLock)
                End While

                While DashboardQueue.Count > 0
                    Dim alert As AlertMaster = CType(DashboardQueue.Dequeue(), AlertMaster)

                    InsertToUserAlert(alert)

                    Log(alert.ID.ToString + " has been processed to dashboard")

#If DEBUG Then
                    'Thread.Sleep(1500)
#End If

                End While

            End SyncLock

        End While
    End Sub

    Private Sub AlertBoxWorkerMethod()

        While Not exitSignal
            SyncLock alertBoxWorkerLock
                While (AlertBoxQueue.Count = 0)
                    Monitor.Wait(alertBoxWorkerLock)
                End While

                While AlertBoxQueue.Count > 0
                    Dim alert As AlertMaster = CType(AlertBoxQueue.Dequeue(), AlertMaster)
                    InsertToUserAlert(alert)
                    Log(alert.ID.ToString + " has been processed to alert box")
#If DEBUG Then
                    'Thread.Sleep(1500)
#End If
                End While

            End SyncLock

        End While

    End Sub
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

            str = str.Remove(startIndex, endIndex - startIndex + 1)

            startIndex = GetHTMLOpenTagIndex(str)
        End While

        Return str
    End Function

    Private Sub SMSWorkerMethod()

        While Not exitSignal
            SyncLock smsWorkerLock
                While (SMSQueue.Count = 0)
                    Monitor.Wait(smsWorkerLock)
                End While

                While SMSQueue.Count > 0
                    Dim alert As AlertMaster = CType(SMSQueue.Dequeue(), AlertMaster)
                    'Thread.Sleep(500)                    

                    Dim strDesc As String = alert.Desc
                    If strDesc.Trim().StartsWith("<p>") Then
                        strDesc = strDesc.Substring(3, strDesc.Length - 3)
                        strDesc = strDesc.Substring(0, strDesc.Length - 4)
                    End If
                    strDesc = "[DNetAlert] " + RemoveAllHTMLTags(strDesc)

                    For Each algrp As AlertGroup In alert.AlertGroups

                        For Each member As UserGroupMember In algrp.UserGroup.UserGroupMembers
                            KTB.DNet.Lib.Sms.Sendto(member.UserInfo.HandPhone, strDesc)
                        Next
                    Next

                    'KTB.DNet.Lib.Sms.Sendto("081806472362", RemoveAllHTMLTags(strDesc))

                    Log(alert.ID.ToString + " has been processed to SMS")
#If DEBUG Then
                    'Thread.Sleep(1500)
#End If

                End While

            End SyncLock

        End While

    End Sub

    Private Sub EmailWorkerMethod()

        While Not exitSignal
            SyncLock emailWorkerLock
                While (EmailQueue.Count = 0)
                    Monitor.Wait(emailWorkerLock)
                End While

                While EmailQueue.Count > 0
                    Dim alert As AlertMaster = CType(EmailQueue.Dequeue(), AlertMaster)


                    Dim mailRecipients As String = String.Empty
                    For Each algrp As AlertGroup In alert.AlertGroups
                        For Each member As UserGroupMember In algrp.UserGroup.UserGroupMembers
                            If mailRecipients.Length > 0 Then
                                mailRecipients += ";"
                            End If
                            mailRecipients += member.UserInfo.Email
                        Next
                    Next

                    'Log("Recipients: " + mailRecipients)

                    If mailRecipients.Trim().Length > 0 Then
                        'mailRecipients = "irwansyah@intimediatalents.com"
                        'Console.WriteLine("alert.Name: {0}. alert.Desc: {1}.", alert.Name, alert.Desc)
                        SendEmail(mailRecipients, alert.Name, alert.Desc)
                    End If


                    Log(alert.ID.ToString + " has been processed to Email")
#If DEBUG Then
                    'Thread.Sleep(1500)
#End If

                End While

            End SyncLock

        End While

    End Sub

    Private Sub SyslogWorkerMethod()
        While Not exitSignal
            SyncLock syslogWorkerLock
                Monitor.Wait(syslogWorkerLock)
            End SyncLock
            ProcessSyslogDeferredMessages()
        End While
    End Sub

    Private Sub ProcessForDashboardToWorkerThread(ByVal alertData As AlertMaster)
        SyncLock dashboardWorkerLock
            DashboardQueue.Enqueue(alertData)
            Log("Adding new data to DashboardQueue. Count = " + DashboardQueue.Count.ToString)

            Monitor.Pulse(dashboardWorkerLock)
        End SyncLock

    End Sub

    Private Sub ProcessForAlertBoxToWorkerThread(ByVal alertData As AlertMaster)
        SyncLock alertBoxWorkerLock
            AlertBoxQueue.Enqueue(alertData)

            Monitor.Pulse(alertBoxWorkerLock)
        End SyncLock

    End Sub

    Private Sub ProcessForSMSToWorkerThread(ByVal alertData As AlertMaster)
        SyncLock smsWorkerLock
            SMSQueue.Enqueue(alertData)

            Monitor.Pulse(smsWorkerLock)
        End SyncLock

    End Sub

    Private Sub ProcessForEmailToWorkerThread(ByVal alertData As AlertMaster)
        SyncLock emailWorkerLock
            EmailQueue.Enqueue(alertData)

            Monitor.Pulse(emailWorkerLock)
        End SyncLock

    End Sub

    Private Function IsDueForDashboard(ByVal alertData As AlertMaster, ByVal DateToCheck As DateTime) As Boolean
        Dim bIsDue As Boolean = False
        If alertData.IsViaDashboard Then
            If alertData.NextRunForDashboard.Equals(NULL_DATETIME) Then
                bIsDue = True
            Else
                Dim nextRunForDashboard As DateTime = New DateTime(alertData.NextRunForDashboard.Year, alertData.NextRunForDashboard.Month, alertData.NextRunForDashboard.Day, _
                    alertData.NextRunForDashboard.Hour, alertData.NextRunForDashboard.Minute, 0)
                DateToCheck = New DateTime(DateToCheck.Year, DateToCheck.Month, DateToCheck.Day, DateToCheck.Hour, _
                                    DateToCheck.Minute, 0)
                'use less than or equal just in case our service has been shutdowned
                bIsDue = (DateTime.op_LessThanOrEqual(nextRunForDashboard, DateToCheck))
            End If
        End If
        If bIsDue Then
            Log("Alert " + alertData.ID.ToString() + " is due for dashboard.")
            Return True
        Else
            Log("Alert " + alertData.ID.ToString() + " is not due for dashboard. Due Date: " + alertData.NextRunForDashboard.ToString("yyyy-MM-dd hh:mm:ss"))
            Return False
        End If
    End Function

    Private Function IsDueForAlertBox(ByVal alertData As AlertMaster, ByVal DateToCheck As DateTime) As Boolean
        Dim bIsDue As Boolean = False
        If alertData.IsViaAlertBox Then
            If alertData.NextRunForAlertBox.Equals(NULL_DATETIME) Then
                bIsDue = True
            Else
                Dim nextRunForAlertBox As DateTime = New DateTime(alertData.NextRunForAlertBox.Year, alertData.NextRunForAlertBox.Month, alertData.NextRunForAlertBox.Day, _
                    alertData.NextRunForAlertBox.Hour, alertData.NextRunForAlertBox.Minute, 0)
                DateToCheck = New DateTime(DateToCheck.Year, DateToCheck.Month, DateToCheck.Day, DateToCheck.Hour, _
                                    DateToCheck.Minute, 0)
                'use less than or equal just in case our service has been shutdowned
                bIsDue = (DateTime.op_LessThanOrEqual(nextRunForAlertBox, DateToCheck))
            End If
        End If
        If bIsDue Then
            Log("Alert " + alertData.ID.ToString() + " is due for alert box.")
            Return True
        Else
            Log("Alert " + alertData.ID.ToString() + " is not due for alert box. Due Date: " + alertData.NextRunForAlertBox.ToString("yyyy-MM-dd hh:mm:ss"))
            Return False
        End If
    End Function

    Private Function IsDueForSMS(ByVal alertData As AlertMaster, ByVal DateToCheck As DateTime) As Boolean
        Dim bIsDue As Boolean = False
        If alertData.IsViaSMS Then
            If alertData.NextRunForSMS.Equals(NULL_DATETIME) Then
                bIsDue = True
            Else
                Dim nextRunForSMS As DateTime = New DateTime(alertData.NextRunForSMS.Year, alertData.NextRunForSMS.Month, alertData.NextRunForSMS.Day, _
                    alertData.NextRunForSMS.Hour, alertData.NextRunForSMS.Minute, 0)
                DateToCheck = New DateTime(DateToCheck.Year, DateToCheck.Month, DateToCheck.Day, DateToCheck.Hour, _
                                    DateToCheck.Minute, 0)
                'use less than or equal just in case our service has been shutdowned
                bIsDue = (DateTime.op_LessThanOrEqual(nextRunForSMS, DateToCheck))
            End If
        End If
        If bIsDue Then
            Log("Alert " + alertData.ID.ToString() + " is due for SMS.")
            Return True
        Else
            Log("Alert " + alertData.ID.ToString() + " is not due for SMS. Due Date: " + alertData.NextRunForSMS.ToString("yyyy-MM-dd hh:mm:ss"))
            Return False
        End If
    End Function

    Private Function IsDueForEmail(ByVal alertData As AlertMaster, ByVal DateToCheck As DateTime) As Boolean
        Dim bIsDue As Boolean = False
        If alertData.IsViaEmail Then
            If alertData.NextRunForEmail.Equals(NULL_DATETIME) Then
                bIsDue = True
            Else
                Dim nextRunForEmail As DateTime = New DateTime(alertData.NextRunForEmail.Year, alertData.NextRunForEmail.Month, alertData.NextRunForEmail.Day, _
                    alertData.NextRunForEmail.Hour, alertData.NextRunForEmail.Minute, 0)
                DateToCheck = New DateTime(DateToCheck.Year, DateToCheck.Month, DateToCheck.Day, DateToCheck.Hour, _
                                    DateToCheck.Minute, 0)
                'use less than or equal just in case our service has been shutdowned
                bIsDue = (DateTime.op_LessThanOrEqual(nextRunForEmail, DateToCheck))
            End If
        End If
        If bIsDue Then
            Log("Alert " + alertData.ID.ToString() + " is due for email.")
            Return True
        Else
            Log("Alert " + alertData.ID.ToString() + " is not due for email. Due Date: " + alertData.NextRunForEmail.ToString("yyyy-MM-dd hh:mm:ss"))
            Return False
        End If
    End Function

    Private Function IsPeriodicAlert(ByVal alertData As AlertMaster) As Boolean
        If (alertData.AnnouncementAlertType = EnumAlertManagement.AnnouncementAlertType.PeriodicAlert) Then
            Log("Alert " + alertData.ID.ToString() + " is a periodic alert.")
            Return True
        Else
            Log("Alert " + alertData.ID.ToString() + " is an one-time alert.")
            Return False
        End If
    End Function
    Private Function IsTransactionalAlert(ByVal alertData As AlertMaster) As Boolean
        If (alertData.AlertType = EnumAlertManagement.AlertManagementType.Transactional) Then
            Log("Alert " + alertData.ID.ToString() + " is a transactional alert.")
            Return True
        Else
            Log("Alert " + alertData.ID.ToString() + " is an announcement alert.")
            IsPeriodicAlert(alertData)
            Return False
        End If

    End Function

    Private Function IsCurrentDateIsNationalHoliday(ByVal currentDateTime As DateTime)
        Dim holiday As NationalHoliday

        currentDateTime = New DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, 0, 0, 0)

        Dim cc As New KTB.DNet.Domain.Search.CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, 0))
        cc.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, currentDateTime))

        Dim holidayFacade As New KTB.DNet.BusinessFacade.General.NationalHolidayFacade(GetUserPrincipal())
        Dim arlHolidays As ArrayList = holidayFacade.RetrieveByCriteria(cc)

        Return (arlHolidays.Count > 0)
    End Function

    Private Function IsIncludeHoliday(ByVal alertData As AlertMaster) As Boolean
        Return (IsTransactionalAlert(alertData) And alertData.IsIncludeHoliday)
    End Function

    Private Function DueAlert(ByVal alertData As AlertMaster) As Boolean
        Dim bIsDue As Boolean = False
        Dim currentDateTime As DateTime = AlertDataRetriever.GetCurrentDateTime()

        currentDateTime = New DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, 0, 0, 0)

        If IsTransactionalAlert(alertData) Then
            If IsCurrentDateIsNationalHoliday(currentDateTime) Then
                bIsDue = IsIncludeHoliday(alertData)
            Else
                bIsDue = True
            End If
        ElseIf CType(alertData.AnnouncementAlertType, EnumAlertManagement.AnnouncementAlertType) = EnumAlertManagement.AnnouncementAlertType.OneTimeAlert Then
            bIsDue = (alertData.NextRunForAlertBox.Equals(NULL_DATETIME) And _
                       alertData.NextRunForDashboard.Equals(NULL_DATETIME) And _
                       alertData.NextRunForEmail.Equals(NULL_DATETIME) And _
                       alertData.NextRunForSMS.Equals(NULL_DATETIME))
        Else
            bIsDue = True
        End If

        'If bIsDue Then
        '    'Dim dtCheckForValidTime As DateTime = New DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, _
        '    '                                            currentDateTime.Hour, currentDateTime.Minute, 0)
        '    Dim dtCheckForValidTime As DateTime = AlertDataRetriever.GetCurrentDateTime()
        '    Dim dtAlertValidTimeFrom As DateTime = New DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, _
        '                                                alertData.TimeStartFrom.Hour, alertData.TimeStartFrom.Minute, 0)
        '    Dim dtAlertValidTimeTo As DateTime = New DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, _
        '                                                alertData.TimeStartTo.Hour, alertData.TimeStartTo.Minute, 0)

        '    bIsDue = (DateTime.op_GreaterThanOrEqual(dtCheckForValidTime, dtAlertValidTimeFrom) And _
        '                DateTime.op_LessThanOrEqual(dtCheckForValidTime, dtAlertValidTimeTo))
        'End If


        If bIsDue Then
            Log("Alert " + alertData.ID.ToString() + " is due.")

            Return True
        Else
            Log("Alert " + alertData.ID.ToString() + " is not due.")

            Return False
        End If
    End Function

    Private Sub ProcessAlert()

    End Sub

    Private Sub RetrieveAlertFromDB(ByVal state As Object)

        For i As Integer = 1 To 500
            Dim alert As New AlertMaster
            alert.Name = "Alert" + i.ToString()

            arl.Add(alert)
        Next

        Monitor.Pulse(listLock)
    End Sub

End Module

'Public Class AlertMaster
'    Public DateValidFrom As DateTime = New DateTime(2007, 10, 23, 0, 0, 0)
'    Public DateValidTo As DateTime = New DateTime(2007, 10, 23, 0, 0, 0)
'    Public TimeStartFrom As DateTime = New DateTime(1900, 1, 1, 10, 0, 0)
'    Public TimeStartTo As DateTime = New DateTime(1900, 1, 1, 18, 0, 0)
'    Public Name As String = String.Empty
'End Class
