Imports System.ServiceProcess

Public Class DNetService
    Inherits System.ServiceProcess.ServiceBase

#Region " Component Designer generated code "

    Public Sub New()
        MyBase.New()

        ' This call is required by the Component Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call
        'If Not EventLog.SourceExists("KTB.DNet.Service") Then
        '    EventLog.CreateEventSource("KTB.DNet", "KTB.DNet.Service")
        'End If
        Try
            EventLog.CreateEventSource(Library.EventLogSource, Library.EventLogName)
        Catch ex As Exception

        End Try
        EventLog1.Source = Library.EventLogSource
        EventLog1.Log = Library.EventLogName
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

    ' The main entry point for the process
    <MTAThread()> _
    Public Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' More than one NT Service may run within the same process. To add
        ' another service to this process, change the following line to
        ' create a second service object. For example,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New DNetService}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    Friend WithEvents EventLog1 As System.Diagnostics.EventLog
    Friend WithEvents Timer1 As System.Timers.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.EventLog1 = New System.Diagnostics.EventLog
        Me.Timer1 = New System.Timers.Timer
        CType(Me.EventLog1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Timer1, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Timer1
        '
        Me.Timer1.Interval = 60000
        '
        'DNetService
        '
        Me.ServiceName = "DNetService"
        CType(Me.EventLog1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Timer1, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

#End Region

    Private _IndentPartEquipmentPO As IndentPartEquipmentPO

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        Me.Timer1.Enabled = True
        EventLog1.WriteEntry("In OnStart")
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        EventLog1.WriteEntry("In OnStop.")
    End Sub

    Protected Overrides Sub OnContinue()
        EventLog1.WriteEntry("In OnContinue.")
    End Sub

    Private Sub Timer1_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Timer1.Elapsed
        DoIndentPartEquipmentPO()
        'LogHelper.WriteLog(Now.ToString("HH:mm:ss"))
    End Sub

    Private Sub DoIndentPartEquipmentPO()
        If IsNothing(_IndentPartEquipmentPO) Then
            LogHelper.WriteLog("Intantiate object IndentPartEquipmentPO")
            _IndentPartEquipmentPO = New IndentPartEquipmentPO
        End If
        _IndentPartEquipmentPO.DoJob()
        ' Dim Freq As Integer = ConfigurationSettings.a
    End Sub

End Class
