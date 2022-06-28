Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Configuration
Imports System.IO
Imports System.Text


Namespace KTB.DNet.SapServiceManager

    Public Class ServiceManagerApplicationContext
        Inherits ApplicationContext

        Private components As System.ComponentModel.IContainer
        Private ServiceNotifyIcon As System.Windows.Forms.NotifyIcon
        Private ServiceNotifyIconContextMenu As System.Windows.Forms.ContextMenu
        Private exitContextMenuItem As System.Windows.Forms.MenuItem
        Private showContextMenuItem As System.Windows.Forms.MenuItem
        Private mainForms As System.Windows.Forms.Form
        Private sapProcessorForm As System.Windows.Forms.Form
        Private ContextMenu1 As New ContextMenu
        Private NotifyIcon1 As NotifyIcon
        Private _SapServiceManager As SAPListenerManager
        Private WithEvents SapTimer As Timer
        Private LOG As EventLog
        Private sw As StreamWriter

        Public Sub New()
            InitializeContext()
            InitializeTimer()
        End Sub

        Private Sub InitializeTimer()
            SapTimer = New Timer
            Dim intTime As Integer = 3600001
            Dim _time As String = ConfigurationSettings.AppSettings.Item("TimerElapsed").Trim
            Try
                intTime = Integer.Parse(_time)
            Catch ex As Exception
                intTime = 3600001
            End Try
            SapTimer.Interval = intTime
            If SapTimer.Enabled = False Then
                SapTimer.Start()
            End If
        End Sub

        Private Sub SAPTimerTick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SapTimer.Tick
            If Now.Hour > 18 Then
                CheckLogFile()
                ClearServerLog()
            End If
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
                    MessageBox.Show("Failed to Move Log File, The File Already In used by Other Process ")
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
                        MessageBox.Show("Failed to Move Log File : " & ex.Message)
                    End Try
                End If
            End If
        End Sub

        Private Sub InitializeContext()
            NotifyIcon1 = New NotifyIcon
            Me.components = New System.ComponentModel.Container
            Me.ServiceNotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
            Me.ServiceNotifyIconContextMenu = New System.Windows.Forms.ContextMenu
            Me.showContextMenuItem = New System.Windows.Forms.MenuItem
            Me.exitContextMenuItem = New System.Windows.Forms.MenuItem

            ContextMenu1.MenuItems.Add("SAP Service &Manager", New System.EventHandler(AddressOf MenuOpenClick))
            ContextMenu1.MenuItems.Add("&SAP Manual Processor", New System.EventHandler(AddressOf MenuSAPProcessorClick))
            ContextMenu1.MenuItems.Add("E&xit", New System.EventHandler(AddressOf MenuCloseClick))

            ' Set properties of NotifyIcon component.
            _SapServiceManager = New SAPListenerManager(ConfigurationSettings.AppSettings("DefaultService"))
            If _SapServiceManager.Status = ServiceProcess.ServiceControllerStatus.Running Then
                NotifyIcon1.Icon = New System.Drawing.Icon(Application.StartupPath & "\" & ConfigurationSettings.AppSettings("DefaultStartIcon"))
                NotifyIcon1.Text = ConfigurationSettings.AppSettings("DefaultStartText")
            Else
                NotifyIcon1.Icon = New System.Drawing.Icon(Application.StartupPath & "\" & ConfigurationSettings.AppSettings("DefaultStopIcon"))
                NotifyIcon1.Text = ConfigurationSettings.AppSettings("DefaultStopText")
            End If
            _SapServiceManager = Nothing
            NotifyIcon1.Visible = True
            NotifyIcon1.ContextMenu = ContextMenu1
            AddHandler NotifyIcon1.DoubleClick, AddressOf Me.NotifyIconDoubleClick
        End Sub

        Public Sub MenuOpenClick(ByVal sender As Object, ByVal e As System.EventArgs)
            ShowForm(NotifyIcon1)
        End Sub

        Public Sub MenuSAPProcessorClick(ByVal sender As Object, ByVal e As System.EventArgs)
            If sapProcessorForm Is Nothing Then
                sapProcessorForm = New FrmSAPProcessorMain
                AddHandler sapProcessorForm.Closed, AddressOf SAPMainformClosed
                sapProcessorForm.Show()
            Else
                sapProcessorForm.Activate()
            End If
            If Not Me.mainForms Is Nothing Then
                Me.mainForms.Close()
                Me.mainForms = Nothing
            End If

        End Sub

        Public Sub MenuCloseClick(ByVal sender As Object, ByVal e As System.EventArgs)
            ExitThread()
        End Sub

        Public Sub NotifyIconDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
            ShowForm(NotifyIcon1)
        End Sub

        Private Sub ShowForm(ByVal _notifyIcon As NotifyIcon)
            If mainForms Is Nothing Then
                mainForms = New frmSAPServiceManager(_notifyIcon)
                AddHandler mainForms.Closed, AddressOf MainformClosed
                mainForms.Show()
            Else
                mainForms.Activate()
            End If
            If Not Me.sapProcessorForm Is Nothing Then
                Me.sapProcessorForm.Close()
                Me.sapProcessorForm = Nothing
            End If
        End Sub

        Private Sub MainformClosed(ByVal sender As Object, ByVal e As EventArgs)
            Me.mainForms = Nothing
        End Sub

        Private Sub SAPMainformClosed(ByVal sender As Object, ByVal e As EventArgs)
            Me.sapProcessorForm = Nothing
        End Sub

        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
        End Sub

        Protected Overloads Overrides Sub ExitThreadCore()
            If Not mainForms Is Nothing Then
                mainForms.Close()
            End If
            MyBase.ExitThreadCore()
        End Sub

    End Class
End Namespace
