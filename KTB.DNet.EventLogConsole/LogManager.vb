Imports System.Configuration
Imports System.IO
Imports System.Text


Public Class LogManager
    Private LOG As EventLog
    Private sw As StreamWriter

    Public Sub New()

    End Sub

    Public Sub BackupLog()
        Try
            Console.WriteLine("Mulai backup log.......")
            CheckLogFile()
            ClearServerLog()
            Console.WriteLine("Backup log berhasil.......")
        Catch ex As Exception
            Console.WriteLine("Gagal Backup Log.......")
        End Try
    End Sub

    Private Sub ClearServerLog()
        ClearLog("KTB.DNet.Log")
        ClearLog("System")
        ClearLog("Application")
        ClearLog("Security")
    End Sub

    Private Sub ClearLog(ByVal LogName As String)
        Dim logFolderFile As String = ConfigurationSettings.AppSettings.Item("DNetLogFolder").Trim & "EventViewer\" & Now.Year & Now.Month & Now.Day
        Dim dir As DirectoryInfo = New DirectoryInfo(logFolderFile)
        Dim logEntry As EventLogEntry
        Dim sb As StringBuilder = New StringBuilder
        If Not dir.Exists Then
            dir.Create()
        End If
        LOG = New EventLog
        LOG.Log = LogName
        For i As Integer = 0 To LOG.Entries.Count - 1
            logEntry = LOG.Entries.Item(i)
            sb.Append("Start  ------------------------------------------" & Chr(13) & Chr(10))
            sb.Append("Type      : " & logEntry.EntryType.ToString.ToUpper & Chr(13) & Chr(10))
            sb.Append("event ID  : " & logEntry.EventID.ToString & Chr(13) & Chr(10))
            sb.Append("Category  : " & logEntry.Category.ToString & Chr(13) & Chr(10))
            sb.Append("Message   : " & logEntry.Message.ToString & Chr(13) & Chr(10))
            sb.Append("Generated : " & logEntry.TimeGenerated.ToString & Chr(13) & Chr(10))
            sb.Append("Writed    : " & logEntry.TimeWritten.ToString & Chr(13) & Chr(10))
            sb.Append("End  ------------------------------------------" & Chr(13) & Chr(10))
            sb.Append(" " & Chr(13) & Chr(10))
        Next
        LOG.Clear()
        Dim fileName As String = logFolderFile & "\" & LogName & ".log"
        CreateFile(fileName)
        sw.WriteLine(sb.ToString)
        CloseWriter()
    End Sub

    Private Sub CreateFile(ByVal fileName As String)
        Dim finfo As New FileInfo(fileName)
        If Not finfo.Directory.Exists Then
            Directory.CreateDirectory(finfo.DirectoryName)
        End If
        If finfo.Exists Then
            finfo.Delete()
        End If
        sw = New StreamWriter(fileName)
    End Sub

    Private Sub CloseWriter()
        If Not sw Is Nothing Then
            sw.Flush()
            sw.Close()
        End If
    End Sub

    Private Sub CheckLogFile()
        Dim logFile As String = ConfigurationSettings.AppSettings.Item("DNetLogFile").Trim
        Dim logFolderFile As String = ConfigurationSettings.AppSettings.Item("DNetLogFolder").Trim
        Dim DestLogFolder As String = logFolderFile & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Second & ".log"
        Dim finfo As FileInfo = New FileInfo(logFile)
        Dim fs As FileStream
        Dim succes As Boolean = False
        If finfo.Exists Then
            Try
                fs = finfo.OpenWrite()
                succes = True
            Catch ex As Exception
                Console.WriteLine("Failed to Move Log File, The File Already In used by Other Process ")
            Finally
                If Not fs Is Nothing Then
                    fs.Close()
                    fs = Nothing
                End If
            End Try
            If succes Then
                Dim destFileInfo As FileInfo = New FileInfo(DestLogFolder)
                If Not destFileInfo.Directory.Exists Then
                    destFileInfo.Directory.Create()
                End If
                Try
                    finfo.MoveTo(DestLogFolder)
                Catch ex As Exception
                    Console.WriteLine("Failed to Move Log File : " & ex.Message)
                End Try
            End If
        End If
    End Sub
End Class
