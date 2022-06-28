Public Class Library
    Public Shared Function GetIPv4Address() As String
        GetIPv4Address = String.Empty
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                GetIPv4Address = ipheal.ToString() & System.Configuration.ConfigurationSettings.AppSettings.Get("AppID")
            End If
        Next

    End Function
    Public Const EventLogSource As String = "MMC.DNet.App"
    Public Const EventLogName As String = "MMC.Service.DNet"

    Public Enum Frequency
        Once = 0
        Daily = 1
        Weekly = 2
        Monthly = 3
        CustomDay = 4
        CustomHour = 5
        CustomMinute = 6
    End Enum


End Class
