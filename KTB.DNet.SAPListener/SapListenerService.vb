Imports System.Timers
Imports System.ServiceProcess
Imports System.Threading
Imports System.Configuration
Imports KTB.DNet.Parser
Imports System.IO
Imports KTB.DNet.Lib


Namespace KTB.DNet.SapListener

    Public Class SAPListenerService
        Inherits System.ServiceProcess.ServiceBase

#Region "Variable Declarartion"
        Private SAPFileWatcherList As New ArrayList
        Private ConnectionString As String
        Private DestinationFolder As String
        Private SourceFolder As String
        Private SO_FileFolder As String
        Protected _timer As System.Timers.Timer
#End Region

#Region " Component Designer generated code "

        Public Sub New()
            MyBase.New()

            ' This call is required by the Component Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call
            If Not EventLog.SourceExists("KTB.DNet.Source") Then
                EventLog.CreateEventSource("KTB.DNet.Source", "KTB.DNet.Log")
            End If
          

            Try
                If Not EventLog.SourceExists("MFTBC.DNet.Source") Then
                    EventLog.CreateEventSource("MFTBC.DNet.Source", "MFTBC.DNet.Log")
                End If

            Catch ex As Exception
                SAPLog.WriteEntry(ex.Message)
            End Try


            Try
                If Not EventLog.SourceExists("MMC.DNet.Source") Then
                    EventLog.CreateEventSource("MMC.DNet.Source", "MMC.DNet.Log")
                End If

            Catch ex As Exception
                SAPLog.WriteEntry(ex.Message)
            End Try
            SAPLog.Source = "MMC.DNet.Source"
            SAPLog.Log = "MMC.DNet.Log"

        End Sub

        'UserService overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub




        Private components As System.ComponentModel.IContainer


        Friend WithEvents SAPLog As System.Diagnostics.EventLog
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.SAPLog = New System.Diagnostics.EventLog
            CType(Me.SAPLog, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ServiceName = "FSWatcherService"
            CType(Me.SAPLog, System.ComponentModel.ISupportInitialize).EndInit()
        End Sub

#End Region

#Region "Protected Method"
        Protected Overrides Sub OnStart(ByVal args() As String)
            PrepareStartService()
            Dim al As ArrayList = PopulateDirectory()
            For Each dirName As String In al
                SAPFileWatcherList.Add(Initialize(dirName, True))
            Next
            InitializedTimer()
        End Sub

        Protected Sub InitializedTimer()
            _timer = New System.Timers.Timer
            _timer.Interval = CInt(WebConfig.GetValue("TimerElapsed"))
            AddHandler _timer.Elapsed, AddressOf OnTimer
            '_timer.Start()
        End Sub

        Protected Sub OnTimer(ByVal source As Object, ByVal e As ElapsedEventArgs)

            If Now.Hour = 12 Then
                Dim finfo As FileInfo
                Dim logFile As String = WebConfig.GetValue("DNetLogFile")
                Dim logFileFolder As String = WebConfig.GetValue("DNetLogFolder")
                Dim dir As DirectoryInfo = New DirectoryInfo(logFileFolder)
                If Not dir.Exists Then
                    dir.Create()
                End If
                finfo = New FileInfo(logFile)
                If finfo.Exists Then
                    Try
                        SAPLog.WriteEntry("Moving Log File ", EventLogEntryType.Warning)
                        finfo.MoveTo(logFileFolder & Now.Day & Now.Month & Now.Year & Now.Hour & Now.Minute & Now.Second & logFile)
                        finfo.Create()
                    Catch ex As Exception
                        SAPLog.WriteEntry("Error Process Log File", EventLogEntryType.Error)
                    End Try
                End If
            End If

        End Sub

        Protected Overrides Sub OnContinue()
            SAPLog.WriteEntry("In OnContinue.")
        End Sub
        Protected Overrides Sub OnStop()
            For Each Obj As SAPFileWatcher In SAPFileWatcherList
                Obj.StopWatch()
            Next
        End Sub
#End Region

#Region "Private Method"
        Private Sub PrepareStartService()
            ConnectionString = WebConfig.GetValue("ConnectionString")
            DestinationFolder = WebConfig.GetValue("DestinationFolder")
            SourceFolder = WebConfig.GetValue("SourceFolder")
            SO_FileFolder = WebConfig.GetValue("SO_FILE_FOLDER")
            ParserHelper.ConnectionString = ConnectionString
            If Not IO.Directory.Exists(DestinationFolder) Then
                IO.Directory.CreateDirectory(DestinationFolder)
            End If
            If Not IO.Directory.Exists(SourceFolder) Then
                IO.Directory.CreateDirectory(SourceFolder)
            End If
            If Not IO.Directory.Exists(SO_FileFolder) Then
                IO.Directory.CreateDirectory(SO_FileFolder)
            End If
            SAPLog.WriteEntry("In OnStart")
            'SAPLog.WriteEntry("ConStr = " + ConnectionString)
            'SAPLog.WriteEntry("Source : " & SourceFolder)
            'SAPLog.WriteEntry("Destination : " & DestinationFolder)
        End Sub
        Private Function PopulateDirectory() As ArrayList
            Dim al As New ArrayList
            al.Add(SourceFolder)
            Return al
        End Function
        Private Function Initialize(ByVal sDir As String, ByVal subFolder As Boolean) As SAPFileWatcher
            Dim objWatcher As New SAPFileWatcher
            objWatcher.FolderToMonitor = sDir
            objWatcher.IncludeSubfolders = subFolder
            objWatcher.StartWatch()
            Return objWatcher
        End Function
#End Region

    End Class
End Namespace
