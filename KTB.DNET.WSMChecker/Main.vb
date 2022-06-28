Imports System.serviceprocess
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web
Imports System.Net

#Region "Custom Namespace Imports"
Imports KTB.DNET.Utility
Imports KTB.DNET.Lib

#End Region

Module Main

    Sub Main()
        Try
            RestartService()
        Catch ex As Exception
            WriteLog(ex.Message)
            WriteLog("\n")
            WriteLog(ex.InnerException.ToString())
        End Try

    End Sub

    Private Sub Announce(ByVal IsSuccess As Boolean, ByVal strMessage As String)
        Dim SMSTos As String = String.Empty

        SMSTos = KTB.DNet.Lib.WebConfig.GetValue("SMSRecipient")


        Dim EmailTos As String = KTB.DNet.Lib.WebConfig.GetValue("EmailRecipient")
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        Dim IsAlwaysSendSMS As Boolean = False

        If KTB.DNet.Lib.WebConfig.GetValue("AlwaysSendSMS").Trim.ToLower = "true" Then IsAlwaysSendSMS = True

        For Each sEmail As String In EmailTos.Split(";")
            If sEmail.Trim <> "" Then
                Try
                    ObjEmail.sendMail(sEmail, "", emailFrom, "[MMKSI-DNet] - WSM Checking", System.Web.Mail.MailFormat.Html, strMessage)
                Catch ex As Exception
                    WriteLog("Sending email failed to " & sEmail)
                    WriteLog("Error : " & ex.Message)
                End Try
            End If
        Next

        If IsAlwaysSendSMS OrElse Not IsSuccess Then
            Try
                'Dim objSMS As Sms = New Sms
                For Each sSmsNo As String In SMSTos.Split(";")
                    If sSmsNo.Trim <> "" Then
                        Try
                            If Sms.IsSMSGatewayLive Then
                                Sms.Sendto(sSmsNo, "[WSM Checking] " & strMessage)
                            Else
                                WriteLog("SMS Server tidak aktif")
                            End If
                        Catch ex As Exception
                            WriteLog("Sending SMS failed to " & sSmsNo)
                            WriteLog("Error : " & ex.Message)
                        End Try
                    End If
                Next
            Catch ex As Exception
                WriteLog(ex.Message)
            End Try
        End If

    End Sub

    Private Function RestartService() As Boolean
        Dim SVCName As String = KTB.DNet.Lib.WebConfig.GetValue("ServiceName")


        Try
            Dim oSC As New ServiceController(SVCName)
            If Not IsNothing(oSC) Then
                If oSC.Status = ServiceControllerStatus.Running Then
                    WriteLog("Current Status = Running. Action->Restart")
                    oSC.Stop()
                    oSC.WaitForStatus(ServiceControllerStatus.Stopped)
                    oSC.Start()
                    WriteLog("Restart Success")
                    Announce(True, "Restart " & SVCName & " In " & GetServerInfo() & " Success")
                ElseIf oSC.Status = ServiceControllerStatus.Stopped Then
                    WriteLog("Current Status = Stopped. Action->Start")
                    oSC.Start()
                    WriteLog("Start Success")
                    Announce(True, "Start " & SVCName & " In " & GetServerInfo() & " Success")
                End If
            Else
                WriteLog("Service Control is Nothing")
                Announce(False, "Service Control is Nothing. Service Name : " & SVCName & ";Server : " & GetServerInfo())
            End If
        Catch ex As Exception
            WriteLog("Error : Service Name = [" & SVCName & "]")
            WriteLog("Error : " & ex.Message)
            Announce(False, ex.Message & ". Server : " & GetServerInfo())
        End Try
    End Function

    Private Sub WriteLog(ByVal str As String)
        Dim FolderToSave As String = KTB.DNet.Lib.WebConfig.GetValue("FolderLog")
        Dim FileName As String = FolderToSave & "\" & "WSMChecker.log"
        Dim oFI As FileInfo
        Dim oSW As StreamWriter

        oFI = New FileInfo(FileName)
        If oFI.Directory.Exists = False Then
            oFI.Directory.Create()
        End If
        If oFI.Exists = False Then oFI.Create()
        oFI = Nothing

        oSW = New StreamWriter(FileName, True)
        str = Now.ToString("yyyyMMdd hh:mm:ss") & " = " & str & vbCrLf
        oSW.Write(str)
        oSW.Flush()
        oSW.Close()
    End Sub

    Private Function GetServerInfo() As String
        Dim sHostName As String = ""
        Dim sIP As String = ""

        Try
            sHostName = Dns.GetHostName()
            Try
                Dim oIPHE As IPHostEntry = Dns.GetHostByName(sHostName)
                If oIPHE.AddressList.Length > 0 Then
                    sIP = oIPHE.AddressList(0).ToString
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
        Return sHostName & " (" & sIP & ")"
    End Function

End Module
