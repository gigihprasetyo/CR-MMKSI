Imports KTB.Dnet.Parser
Imports System.IO
Imports System.Configuration
Imports System.Threading

Namespace KTB.DNet.SapServiceManager


    Public Class FrmSAPProcessorMain
        Inherits System.Windows.Forms.Form

#Region "Private Variable"
        Private totalFolderSize As Long
#End Region
#Region "Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call

        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents Panel3 As System.Windows.Forms.Panel
        Friend WithEvents Panel4 As System.Windows.Forms.Panel
        Friend WithEvents txtFolderName As System.Windows.Forms.TextBox
        Friend WithEvents btnExit As System.Windows.Forms.Button
        Friend WithEvents Panel2 As System.Windows.Forms.Panel
        Friend WithEvents CheckedListBoxFile As System.Windows.Forms.CheckedListBox
        Friend WithEvents cbSelectAll As System.Windows.Forms.CheckBox
        Friend WithEvents btnProcess As System.Windows.Forms.Button
        Friend WithEvents btnDelete As System.Windows.Forms.Button
        Friend WithEvents btnSearch As System.Windows.Forms.Button
        Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
        Friend WithEvents Panel5 As System.Windows.Forms.Panel
        Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
        Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmSAPProcessorMain))
            Me.txtFolderName = New System.Windows.Forms.TextBox
            Me.btnSearch = New System.Windows.Forms.Button
            Me.btnProcess = New System.Windows.Forms.Button
            Me.Label1 = New System.Windows.Forms.Label
            Me.btnDelete = New System.Windows.Forms.Button
            Me.btnExit = New System.Windows.Forms.Button
            Me.Panel1 = New System.Windows.Forms.Panel
            Me.cbSelectAll = New System.Windows.Forms.CheckBox
            Me.Panel3 = New System.Windows.Forms.Panel
            Me.Panel5 = New System.Windows.Forms.Panel
            Me.Panel4 = New System.Windows.Forms.Panel
            Me.StatusBar1 = New System.Windows.Forms.StatusBar
            Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel
            Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel
            Me.Panel2 = New System.Windows.Forms.Panel
            Me.CheckedListBoxFile = New System.Windows.Forms.CheckedListBox
            Me.Panel1.SuspendLayout()
            Me.Panel3.SuspendLayout()
            Me.Panel5.SuspendLayout()
            Me.Panel4.SuspendLayout()
            CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.Panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'txtFolderName
            '
            Me.txtFolderName.Location = New System.Drawing.Point(136, 16)
            Me.txtFolderName.Name = "txtFolderName"
            Me.txtFolderName.Size = New System.Drawing.Size(184, 20)
            Me.txtFolderName.TabIndex = 0
            Me.txtFolderName.Text = ""
            '
            'btnSearch
            '
            Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
            Me.btnSearch.Location = New System.Drawing.Point(328, 16)
            Me.btnSearch.Name = "btnSearch"
            Me.btnSearch.Size = New System.Drawing.Size(24, 23)
            Me.btnSearch.TabIndex = 1
            Me.btnSearch.Text = "..."
            '
            'btnProcess
            '
            Me.btnProcess.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnProcess.Location = New System.Drawing.Point(112, 64)
            Me.btnProcess.Name = "btnProcess"
            Me.btnProcess.TabIndex = 2
            Me.btnProcess.Text = "&Process"
            '
            'Label1
            '
            Me.Label1.Location = New System.Drawing.Point(16, 16)
            Me.Label1.Name = "Label1"
            Me.Label1.TabIndex = 3
            Me.Label1.Text = "Folder to Browse"
            '
            'btnDelete
            '
            Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnDelete.Location = New System.Drawing.Point(24, 64)
            Me.btnDelete.Name = "btnDelete"
            Me.btnDelete.TabIndex = 5
            Me.btnDelete.Text = "&Delete"
            '
            'btnExit
            '
            Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnExit.Location = New System.Drawing.Point(208, 64)
            Me.btnExit.Name = "btnExit"
            Me.btnExit.TabIndex = 6
            Me.btnExit.Text = "&Exit"
            '
            'Panel1
            '
            Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel1.Controls.Add(Me.cbSelectAll)
            Me.Panel1.Controls.Add(Me.Label1)
            Me.Panel1.Controls.Add(Me.txtFolderName)
            Me.Panel1.Controls.Add(Me.btnSearch)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(504, 88)
            Me.Panel1.TabIndex = 7
            '
            'cbSelectAll
            '
            Me.cbSelectAll.Location = New System.Drawing.Point(16, 47)
            Me.cbSelectAll.Name = "cbSelectAll"
            Me.cbSelectAll.Size = New System.Drawing.Size(120, 24)
            Me.cbSelectAll.TabIndex = 4
            Me.cbSelectAll.Text = "Select All"
            '
            'Panel3
            '
            Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel3.Controls.Add(Me.Panel5)
            Me.Panel3.Controls.Add(Me.StatusBar1)
            Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel3.Location = New System.Drawing.Point(0, 358)
            Me.Panel3.Name = "Panel3"
            Me.Panel3.Size = New System.Drawing.Size(504, 72)
            Me.Panel3.TabIndex = 9
            '
            'Panel5
            '
            Me.Panel5.Controls.Add(Me.Panel4)
            Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.Panel5.Location = New System.Drawing.Point(0, -52)
            Me.Panel5.Name = "Panel5"
            Me.Panel5.Size = New System.Drawing.Size(502, 100)
            Me.Panel5.TabIndex = 9
            '
            'Panel4
            '
            Me.Panel4.Controls.Add(Me.btnExit)
            Me.Panel4.Controls.Add(Me.btnDelete)
            Me.Panel4.Controls.Add(Me.btnProcess)
            Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
            Me.Panel4.Location = New System.Drawing.Point(198, 0)
            Me.Panel4.Name = "Panel4"
            Me.Panel4.Size = New System.Drawing.Size(304, 100)
            Me.Panel4.TabIndex = 7
            '
            'StatusBar1
            '
            Me.StatusBar1.Location = New System.Drawing.Point(0, 48)
            Me.StatusBar1.Name = "StatusBar1"
            Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2})
            Me.StatusBar1.ShowPanels = True
            Me.StatusBar1.Size = New System.Drawing.Size(502, 22)
            Me.StatusBar1.TabIndex = 8
            Me.StatusBar1.Text = "StatusBar1"
            '
            'StatusBarPanel1
            '
            Me.StatusBarPanel1.Alignment = System.Windows.Forms.HorizontalAlignment.Right
            Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.StatusBarPanel1.MinWidth = 100
            Me.StatusBarPanel1.Width = 186
            '
            'StatusBarPanel2
            '
            Me.StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
            Me.StatusBarPanel2.MinWidth = 300
            Me.StatusBarPanel2.Text = "StatusBarPanel2"
            Me.StatusBarPanel2.Width = 300
            '
            'Panel2
            '
            Me.Panel2.Controls.Add(Me.CheckedListBoxFile)
            Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel2.Location = New System.Drawing.Point(0, 88)
            Me.Panel2.Name = "Panel2"
            Me.Panel2.Size = New System.Drawing.Size(504, 270)
            Me.Panel2.TabIndex = 10
            '
            'CheckedListBoxFile
            '
            Me.CheckedListBoxFile.Dock = System.Windows.Forms.DockStyle.Fill
            Me.CheckedListBoxFile.Location = New System.Drawing.Point(0, 0)
            Me.CheckedListBoxFile.Name = "CheckedListBoxFile"
            Me.CheckedListBoxFile.Size = New System.Drawing.Size(504, 259)
            Me.CheckedListBoxFile.TabIndex = 0
            '
            'FrmSAPProcessorMain
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(504, 430)
            Me.Controls.Add(Me.Panel2)
            Me.Controls.Add(Me.Panel3)
            Me.Controls.Add(Me.Panel1)
            Me.MinimumSize = New System.Drawing.Size(504, 432)
            Me.Name = "FrmSAPProcessorMain"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "SAP File Processor"
            Me.Panel1.ResumeLayout(False)
            Me.Panel3.ResumeLayout(False)
            Me.Panel5.ResumeLayout(False)
            Me.Panel4.ResumeLayout(False)
            CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.Panel2.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

#End Region


#Region "Windows Event"
        Private Sub FrmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            totalFolderSize = 0
        End Sub

        Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            If CheckedListBoxFile.Items.Count > 0 Then
                If MessageBox.Show("Are you sure to Delete this files ?", "Process Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Try
                        DeleteFiles()
                        CheckedListBoxFile.Items.Clear()
                        cbSelectAll.Checked = False
                        MessageBox.Show("File Succesfuly Deleted.")
                    Catch ex As Exception
                        MessageBox.Show("Delete Error : " & ex.Message, "SAP Processor", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            Else
                MessageBox.Show("No File Selected.", "SAP Processor", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
      
        End Sub

        Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
            If CheckedListBoxFile.Items.Count > 0 Then
                totalFolderSize = 0
                If MessageBox.Show("Are you sure to Process this files ?", "Process Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Try
                        ProcessFile()
                        CheckedListBoxFile.Items.Clear()
                        cbSelectAll.Checked = False
                        MessageBox.Show("File Succesfuly Processe.")
                    Catch ex As Exception
                        MessageBox.Show("Delete Error : " & ex.Message, "SAP Processor", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End Try
                End If
            Else
                MessageBox.Show("No File Selected.", "SAP Processor", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub cbSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSelectAll.CheckedChanged
            If cbSelectAll.Checked = True Then
                If CheckedListBoxFile.Items.Count > 0 Then
                    For i As Integer = 0 To CheckedListBoxFile.Items.Count - 1
                        'CheckedListBoxFile.SetItemCheckState(i, CheckState.Checked)
                        CheckedListBoxFile.SetItemChecked(i, True)
                    Next
                End If
            Else
                If CheckedListBoxFile.Items.Count > 0 Then
                    For i As Integer = 0 To CheckedListBoxFile.Items.Count - 1
                        'CheckedListBoxFile.SetItemCheckState(i, CheckState.Checked)
                        CheckedListBoxFile.SetItemChecked(i, False)
                    Next

                End If
            End If

        End Sub

        Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
            Application.Exit()
        End Sub

        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Dim folder As New FolderBrowserDialog
            If folder.ShowDialog = DialogResult.OK Then
                Try
                    DisplayFile(folder.SelectedPath)
                Catch ex As Exception
                    MessageBox.Show("Error Display File : " & ex.Message, "DNet Manual Processor", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
            cbSelectAll.Checked = False
        End Sub

        Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If txtFolderName.Text <> "" Then
                DisplayFile(txtFolderName.Text)
            Else
                MessageBox.Show("Folder not selected.")
            End If
            cbSelectAll.Checked = False
        End Sub
#End Region

#Region "Private Method"
        Private Sub DisplayFile(ByVal path As String)
            CheckedListBoxFile.Items.Clear()
            DisplayListOfFile(path)
            StatusBar1.Panels(1).Text = " Total Size :" & totalFolderSize

        End Sub

        Private Sub DisplayListOfFile(ByVal folder As String)
            DisplayProcessFile(folder)
            Dim childFolder As String = folder
            If Directory.GetDirectories(childFolder).Length > 0 Then
                For Each item As String In Directory.GetDirectories(childFolder)
                    DisplayListOfFile(item)
                Next
            End If
        End Sub

        Private Sub DisplayProcessFile(ByVal folder)
            Dim dir As Directory
            Dim finfo As FileInfo
            For Each item As String In dir.GetFiles(folder)
                finfo = New FileInfo(item)
                totalFolderSize = totalFolderSize + finfo.Length
                CheckedListBoxFile.Items.Add(item & " @ " & "Size : " & finfo.Length & " Bytes", False)
            Next
        End Sub

        Private Sub DeleteFiles()
            Dim fileName As String
            Dim str() As String
            Dim finfo As FileInfo
            For Each itemChecked As Object In CheckedListBoxFile.CheckedItems
                str = itemChecked.ToString.Split("@")
                fileName = str(0).Trim
                finfo = New FileInfo(fileName)
                If finfo.Exists Then
                    finfo.Delete()
                End If
            Next
        End Sub

        Private Sub ProcessFile()
            Dim fileName As String
            Dim str() As String
            Dim obj As Object = New Object
            For Each itemChecked As Object In CheckedListBoxFile.CheckedItems
                str = itemChecked.ToString.Split("@")
                fileName = str(0).Trim
                Dim destFolder As String = ConfigurationSettings.AppSettings("DestinationFolder")
                Dim _worker As New Worker(fileName, destFolder)
                _worker.Work(obj)
            Next
        End Sub


#End Region








    End Class
End Namespace