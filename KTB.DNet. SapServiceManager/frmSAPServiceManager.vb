Imports System.Configuration

Namespace KTB.DNet.SapServiceManager

    Public Class frmSAPServiceManager
        Inherits System.Windows.Forms.Form

#Region "Variable Declaration"
        Private _SAPManager As SAPListenerManager
        Private NotifyIconApplication As NotifyIcon
#End Region

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()
            InitializeComponent()
        End Sub


        Public Sub New(ByVal _notifyIcon As NotifyIcon)
            MyBase.New()
            NotifyIconApplication = _notifyIcon
            InitializeComponent()
        End Sub

        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private components As System.ComponentModel.IContainer
        Friend WithEvents cmbServiceName As System.Windows.Forms.ComboBox
        Friend WithEvents cmdStart As System.Windows.Forms.Button
        Friend WithEvents cmdStop As System.Windows.Forms.Button
        Friend WithEvents cmdCheckStatus As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
        Friend WithEvents stsBarState As System.Windows.Forms.StatusBar
        Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
        Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSAPServiceManager))
            Me.cmdStart = New System.Windows.Forms.Button
            Me.cmdStop = New System.Windows.Forms.Button
            Me.cmdCheckStatus = New System.Windows.Forms.Button
            Me.cmbServiceName = New System.Windows.Forms.ComboBox
            Me.Label1 = New System.Windows.Forms.Label
            Me.stsBarState = New System.Windows.Forms.StatusBar
            Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel
            Me.PictureBox1 = New System.Windows.Forms.PictureBox
            Me.PictureBox2 = New System.Windows.Forms.PictureBox
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'cmdStart
            '
            Me.cmdStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdStart.Image = CType(resources.GetObject("cmdStart.Image"), System.Drawing.Image)
            Me.cmdStart.Location = New System.Drawing.Point(213, 208)
            Me.cmdStart.Name = "cmdStart"
            Me.cmdStart.Size = New System.Drawing.Size(56, 23)
            Me.cmdStart.TabIndex = 10
            '
            'cmdStop
            '
            Me.cmdStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdStop.Image = CType(resources.GetObject("cmdStop.Image"), System.Drawing.Image)
            Me.cmdStop.Location = New System.Drawing.Point(213, 256)
            Me.cmdStop.Name = "cmdStop"
            Me.cmdStop.Size = New System.Drawing.Size(56, 23)
            Me.cmdStop.TabIndex = 11
            '
            'cmdCheckStatus
            '
            Me.cmdCheckStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdCheckStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cmdCheckStatus.Image = CType(resources.GetObject("cmdCheckStatus.Image"), System.Drawing.Image)
            Me.cmdCheckStatus.Location = New System.Drawing.Point(213, 160)
            Me.cmdCheckStatus.Name = "cmdCheckStatus"
            Me.cmdCheckStatus.Size = New System.Drawing.Size(56, 23)
            Me.cmdCheckStatus.TabIndex = 9
            '
            'cmbServiceName
            '
            Me.cmbServiceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbServiceName.Items.AddRange(New Object() {"KTB.Dnet.SapListener"})
            Me.cmbServiceName.Location = New System.Drawing.Point(96, 40)
            Me.cmbServiceName.Name = "cmbServiceName"
            Me.cmbServiceName.Size = New System.Drawing.Size(160, 21)
            Me.cmbServiceName.TabIndex = 8
            '
            'Label1
            '
            Me.Label1.Location = New System.Drawing.Point(0, 40)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(80, 23)
            Me.Label1.TabIndex = 4
            Me.Label1.Text = "Service Name"
            '
            'stsBarState
            '
            Me.stsBarState.Location = New System.Drawing.Point(0, 306)
            Me.stsBarState.Name = "stsBarState"
            Me.stsBarState.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1})
            Me.stsBarState.ShowPanels = True
            Me.stsBarState.Size = New System.Drawing.Size(282, 22)
            Me.stsBarState.TabIndex = 7
            '
            'StatusBarPanel1
            '
            Me.StatusBarPanel1.Text = "StatusBarPanel1"
            Me.StatusBarPanel1.Width = 500
            '
            'PictureBox1
            '
            Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
            Me.PictureBox1.Location = New System.Drawing.Point(0, 64)
            Me.PictureBox1.Name = "PictureBox1"
            Me.PictureBox1.Size = New System.Drawing.Size(189, 227)
            Me.PictureBox1.TabIndex = 8
            Me.PictureBox1.TabStop = False
            '
            'PictureBox2
            '
            Me.PictureBox2.BackColor = System.Drawing.Color.FromArgb(CType(234, Byte), CType(234, Byte), CType(234, Byte))
            Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
            Me.PictureBox2.Location = New System.Drawing.Point(0, 0)
            Me.PictureBox2.Name = "PictureBox2"
            Me.PictureBox2.Size = New System.Drawing.Size(189, 37)
            Me.PictureBox2.TabIndex = 12
            Me.PictureBox2.TabStop = False
            '
            'frmSAPServiceManager
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.BackColor = System.Drawing.Color.FromArgb(CType(234, Byte), CType(234, Byte), CType(234, Byte))
            Me.ClientSize = New System.Drawing.Size(282, 328)
            Me.Controls.Add(Me.PictureBox2)
            Me.Controls.Add(Me.PictureBox1)
            Me.Controls.Add(Me.stsBarState)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.cmbServiceName)
            Me.Controls.Add(Me.cmdCheckStatus)
            Me.Controls.Add(Me.cmdStop)
            Me.Controls.Add(Me.cmdStart)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "frmSAPServiceManager"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "SAP Service Manager"
            CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

#End Region

#Region "Private Function"
        Private Sub InitializeService(ByVal _SAPManager As SAPListenerManager)
            If _SAPManager.Status = ServiceProcess.ServiceControllerStatus.Running Then
                cmdStart.Enabled = False
                'lblStart.Enabled = False
                cmdStop.Enabled = True
                'LblStop.Enabled = True
                NotifyIconApplication.Text = ConfigurationSettings.AppSettings("DefaultStartText")
                NotifyIconApplication.Icon = New System.Drawing.Icon(Application.StartupPath & "\" & ConfigurationSettings.AppSettings("DefaultStartIcon"))
                PictureBox1.Image = New System.Drawing.Bitmap(Application.StartupPath & "\" & ConfigurationSettings.AppSettings("DefaultStartImage"))
                Me.Icon = New System.Drawing.Icon(Application.StartupPath & "\" & ConfigurationSettings.AppSettings("DefaultStartIcon"))
            Else
                cmdStart.Enabled = True
                'lblStart.Enabled = True
                cmdStop.Enabled = False
                'LblStop.Enabled = False
                PictureBox1.Image = New System.Drawing.Bitmap(Application.StartupPath & "\" & ConfigurationSettings.AppSettings("DefaultStopImage"))
                NotifyIconApplication.Text = ConfigurationSettings.AppSettings("DefaultStopText")
                NotifyIconApplication.Icon = New System.Drawing.Icon(Application.StartupPath & "\" & ConfigurationSettings.AppSettings("DefaultStopIcon"))
                Me.Icon = New System.Drawing.Icon(Application.StartupPath & "\" & ConfigurationSettings.AppSettings("DefaultStopIcon"))

            End If
            stsBarState.Panels(0).Text = _SAPManager.GetServiceStatus
        End Sub
        Private Sub CheckStatus()
            Try
                _SAPManager = New SAPListenerManager(cmbServiceName.Text.Trim)
                InitializeService(_SAPManager)
            Finally
                _SAPManager = Nothing
            End Try

        End Sub


        Private Sub StartService()
            Dim status As Boolean = True
            Try
                _SAPManager = New SAPListenerManager(cmbServiceName.Text.Trim)
                _SAPManager.StartService()
                InitializeService(_SAPManager)
            Catch ex As Exception
                MessageBox.Show("Could Not Start Service :" & ex.Message.ToString)
            Finally
                _SAPManager = Nothing
            End Try
        End Sub
        Private Sub StopService()
            Try
                _SAPManager = New SAPListenerManager(cmbServiceName.Text.Trim)
                _SAPManager.StopService()
                InitializeService(_SAPManager)
            Finally
                _SAPManager = Nothing
            End Try
        End Sub


#End Region

#Region "Windows Event"

        Private Sub frmSAPServiceManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            cmbServiceName.SelectedIndex = 0
            CheckStatus()
        End Sub

        Private Sub cmdCheckStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheckStatus.Click

            CheckStatus()
        End Sub

        Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
            'cmdStart.Enabled = False
            'lblStart.Enabled = False
            StartService()
        End Sub

        Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
            Dim result As DialogResult = MessageBox.Show("Are you sure you wish to STOP SAP Service on This Server ?", "SAP Service Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If result = DialogResult.Yes Then
                cmdStop.Enabled = False
                'LblStop.Enabled = False
                StopService()
            End If
        End Sub



#End Region

       
    End Class
End Namespace
