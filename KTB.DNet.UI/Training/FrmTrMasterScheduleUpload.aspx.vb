#Region "Custom Namespace"
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.UI.Helper
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmTrMasterScheduleUpload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtScheduleName As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgSchedule As System.Web.UI.WebControls.DataGrid
    Protected WithEvents File As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Private bPrivilegeChangeUpSche As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private sHScheduleUpload As SessionHelper = New SessionHelper
    Private _pathFolder As String = KTB.DNet.Lib.WebConfig.GetValue("TrainingScheduleDestFileDirectory")
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "ScheduleYear"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub ClearData()
        Me.txtDesc.ReadOnly = False
        Me.txtScheduleName.ReadOnly = False
        Me.ddlYear.Enabled = True
        Me.File.Disabled = False
        Me.txtDesc.Text = String.Empty
        Me.txtScheduleName.Text = String.Empty
        Me.ddlYear.SelectedIndex = 0
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        dtgSchedule.SelectedIndex = -1
    End Sub

    Private Sub BindToYear()
        Dim strSQL As String = "select MIN(ID) from TrScheduleUpload where ScheduleYear = "
        strSQL = strSQL & "(select MIN(ScheduleYear) from TrScheduleUpload)) "
        strSQL = strSQL & "OR ID in "
        strSQL = strSQL & "(select MAX(ID) from TrScheduleUpload where ScheduleYear = "
        strSQL = strSQL & "(select MAX(ScheduleYear) from TrScheduleUpload)"

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrScheduleUpload), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
        criterias.opAnd(New Criteria(GetType(TrScheduleUpload), "ID", MatchType.InSet, "(" & strSQL & ")"))

        Dim arlYear As ArrayList = New TrScheduleUploadFacade(User).Retrieve(criterias)
        If arlYear.Count = 2 Then
            Dim iStart As Integer = CType(arlYear(0), TrScheduleUpload).ScheduleYear
            Dim iEnd As Integer = CType(arlYear(1), TrScheduleUpload).ScheduleYear
            For i As Integer = iStart To iEnd + 2
                ddlYear.Items.Insert(0, New ListItem(iStart.ToString, iStart))
                iStart = iStart + 1
            Next
        End If
        
        ddlYear.Items.Insert(0, New ListItem("Pilih Tahun", "0"))
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As CriteriaComposite
            criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If ddlYear.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "ScheduleYear", MatchType.Exact, CType(ddlYear.SelectedValue, Integer)))
            End If
            If txtScheduleName.Text.Trim <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "Name", MatchType.[Partial], txtScheduleName.Text.Trim))
            End If
            If txtDesc.Text.Trim <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "Description", MatchType.[Partial], txtDesc.Text.Trim))
            End If

            dtgSchedule.DataSource() = New TrScheduleUploadFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgSchedule.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgSchedule.VirtualItemCount = totalRow
            dtgSchedule.DataBind()

        End If
    End Sub

    Private Sub CopyFileToWebServer(ByVal DestFilePath As String)
        Dim finfo As New FileInfo(DestFilePath)
        If Not finfo.Directory.Exists Then
            Directory.CreateDirectory(finfo.DirectoryName)
        End If
        File.PostedFile.SaveAs(DestFilePath)
    End Sub

    Private Sub CopytoAnOtherWebServer(ByVal finfo As FileInfo)
        Dim objFileHelper As FileHelper = New FileHelper
        objFileHelper.TransferToListWebServer(finfo, "", True, "TrainingScheduleDirectory")
    End Sub

    Private Function InsertSchedule(ByVal FileName As String) As Integer
        Dim nResult As Integer = -1

        Dim objTrScheduleUpload As TrScheduleUpload = New TrScheduleUpload
        objTrScheduleUpload.Name = Me.txtScheduleName.Text
        objTrScheduleUpload.ScheduleYear = Me.ddlYear.SelectedValue
        objTrScheduleUpload.Description = Me.txtDesc.Text
        objTrScheduleUpload.CreatedTime = Today

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If File.PostedFile.ContentType.ToString = "application/vnd.ms-excel" OrElse _
                File.PostedFile.ContentType.ToString = "application/octet-stream" OrElse _
                File.PostedFile.ContentType.ToString = "text/plain" OrElse _
                File.PostedFile.ContentType.ToString = "application/txt" OrElse _
                File.PostedFile.ContentType.ToString = "application/pdf" Then

                If imp.Start() Then
                    Dim newFileLocation As String = GetFilePath(FileName)
                    
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(Me.File.PostedFile.InputStream, GetCompleteFilePath(newFileLocation))

                    CopyFileToWebServer(GetCompleteFilePath(newFileLocation))

                    objTrScheduleUpload.FilePath = _pathFolder & FileName
                    objTrScheduleUpload.UploadDate = Today

                    ' Copy To Another Server
                    'CopytoAnOtherWebServer(New FileInfo(GetCompleteFilePath(newFileLocation)))

                    imp.StopImpersonate()
                    imp = Nothing
                End If
                nResult = New TrScheduleUploadFacade(User).Insert(objTrScheduleUpload)
            Else
                Return -2
            End If

        Catch ex As Exception
            nResult = -1
        End Try

        Return nResult
    End Function

    Private Sub DeleteFileInAllWebServer(ByVal finfo As FileInfo)
        Dim objFileHelper As FileHelper = New FileHelper
        objFileHelper.DeleteFileInWebServer(finfo, "", True, "TrainingScheduleDirectory")
    End Sub

    Private Function GetFilePath(ByVal FileName As String) As String
        Return _pathFolder & FileName
    End Function

    Private Function GetCompleteFilePath(ByVal FilePath As String) As String
        'Dim fileInfo1 As New FileInfo(Server.MapPath(""))
        Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
        Return fileInfo1.Directory.FullName & "\" & FilePath
    End Function

    Private Function UpdateSchedule(ByVal FileName As String) As Integer
        Dim nResult As Integer = -1

        Dim objTrScheduleUpload As TrScheduleUpload = CType(sHScheduleUpload.GetSession("vsTrSchedule"), TrScheduleUpload)
        objTrScheduleUpload.Name = Me.txtScheduleName.Text
        objTrScheduleUpload.ScheduleYear = Me.ddlYear.SelectedValue
        objTrScheduleUpload.Description = Me.txtDesc.Text
        objTrScheduleUpload.LastUpdateTime = Today

        Try
            'upload if user updated the file
            If FileName <> "" Then
                If File.PostedFile.ContentType.ToString = "application/vnd.ms-excel" OrElse _
                    File.PostedFile.ContentType.ToString = "application/octet-stream" OrElse _
                    File.PostedFile.ContentType.ToString = "text/plain" OrElse _
                    File.PostedFile.ContentType.ToString = "application/txt" Then

                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                    If imp.Start() Then

                        If objTrScheduleUpload.FilePath <> "" Then
                            DeleteFileInAllWebServer(New FileInfo( _
                                GetCompleteFilePath(objTrScheduleUpload.FilePath)))
                        End If

                        Dim newFileLocation As String = GetFilePath(FileName)
                        'If Not IO.Directory.Exists(newFileLocation) Then
                        '    IO.Directory.CreateDirectory(Path.GetDirectoryName(newFileLocation))
                        'End If

                        Dim objUpload As New UploadToWebServer
                        objUpload.Upload(Me.File.PostedFile.InputStream, newFileLocation)

                        CopyFileToWebServer(GetCompleteFilePath(newFileLocation))

                        objTrScheduleUpload.FilePath = _pathFolder & FileName
                        objTrScheduleUpload.UploadDate = Today

                        ' Copy To Another Server
                        'CopytoAnOtherWebServer(New FileInfo(GetCompleteFilePath(newFileLocation)))

                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Else
                    Return -2
                End If
            End If
            nResult = New TrScheduleUploadFacade(User).Update(objTrScheduleUpload)

        Catch ex As Exception
            nResult = -1
        End Try

        Return nResult
    End Function

    Private Sub DeleteSchedule(ByVal nID As Integer)
        Dim objTrScheduleUpload As TrScheduleUpload = New TrScheduleUploadFacade(User).Retrieve(nID)
        Dim facade As TrScheduleUploadFacade = New TrScheduleUploadFacade(User)
        facade.DeleteFromDB(objTrScheduleUpload)
        dtgSchedule.CurrentPageIndex = 0
        BindDataGrid(dtgSchedule.CurrentPageIndex)
        dtgSchedule.SelectedIndex = -1
    End Sub

    Private Sub ViewGroup(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objTrScheduleUpload As TrScheduleUpload = New TrScheduleUploadFacade(User).Retrieve(nID)
        sHScheduleUpload.SetSession("vsTrSchedule", objTrScheduleUpload)

        ddlYear.SelectedValue = objTrScheduleUpload.ScheduleYear
        txtScheduleName.Text = objTrScheduleUpload.Name
        txtDesc.Text = objTrScheduleUpload.Description
        Me.btnSimpan.Enabled = EditStatus
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            SetControlPrivilege()
            BindToYear()
            BindDataGrid(0)
        End If
        'btnSimpan.Attributes.Add("onclick", "return PathsOk(Form1)")
        'btnBatal.Attributes.Add("onclick", "return PathsOk(Form1)")
    End Sub
    Private Sub ActivateUserPrivilege()
        bPrivilegeChangeUpSche = SecurityProvider.Authorize(Context.User, SR.TrainingUpdateUploadJadwal_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.TrainingViewUploadJadwal_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Form Kelas")
        End If
    End Sub
    Private Sub SetControlPrivilege()
        btnSimpan.Visible = bPrivilegeChangeUpSche
        btnBatal.Visible = bPrivilegeChangeUpSche
    End Sub
    Private Sub dtgSchedule_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSchedule.PageIndexChanged
        dtgSchedule.SelectedIndex = -1
        dtgSchedule.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgSchedule.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgSchedule_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSchedule.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewGroup(e.Item.Cells(0).Text, False)
            Me.ddlYear.Enabled = False
            Me.txtScheduleName.ReadOnly = True
            Me.txtDesc.ReadOnly = True
            Me.File.Disabled = True
            dtgSchedule.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewGroup(e.Item.Cells(0).Text, True)
            dtgSchedule.SelectedIndex = e.Item.ItemIndex
            Me.ddlYear.Enabled = False
            Me.txtScheduleName.ReadOnly = False
            Me.txtDesc.ReadOnly = False
            Me.File.Disabled = False
        ElseIf e.CommandName = "Delete" Then
            DeleteSchedule(e.Item.Cells(0).Text)
        ElseIf e.CommandName = "Download" Then
            'Dim _linkButton As LinkButton = e.Item.FindControl("lbtnDownload")
            Dim _lblDownload As Label = e.Item.FindControl("lblDownload")

            'Dim fileInfo As New fileInfo(Server.MapPath("") & "\..\" & _lblDownload.Text)
            Dim fileInfo As New fileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\" & _lblDownload.Text)
            Dim fileExist As Boolean = False
            fileExist = CheckFileExist(fileInfo)
            If (fileExist) Then
                Try
                    Response.Redirect("../Download.aspx?file=" & _lblDownload.Text)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(_lblDownload.Text))
                End Try
            Else
                MessageBox.Show(SR.FileNotFound(fileInfo.Name))
            End If
        End If
    End Sub

    Private Function CheckFileExist(ByVal fileinfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return fileinfo.Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try

    End Function

    Private Sub dtgSchedule_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSchedule.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            If Not e.Item.DataItem Is Nothing Then
                e.Item.DataItem.GetType().ToString()
                Dim RowValue As TrScheduleUpload = CType(e.Item.DataItem, TrScheduleUpload)

                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Or ItemType = ListItemType.SelectedItem Then

                    Dim str As String() = RowValue.FilePath.Split("\")
                    Dim _linkButton As LinkButton = e.Item.FindControl("lbtnDownload")
                    Dim _lblDownload As Label = e.Item.FindControl("lblDownload")
                    _linkButton.Text = str(str.Length - 1) 'RowValue.FilePath
                    _lblDownload.Text = RowValue.FilePath

                    If Not e.Item.FindControl("btnHapus") Is Nothing Then
                        CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                    End If

                    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                        CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgSchedule.CurrentPageIndex * dtgSchedule.PageSize)
                    End If

                End If
            End If

        End If

        'tambahan priviledge
        ActivateUserPrivilege()
        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = bPrivilegeChangeUpSche
        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = bPrivilegeChangeUpSche
        End If

    End Sub

    Private Sub dtgSchedule_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSchedule.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        dtgSchedule.SelectedIndex = -1
        dtgSchedule.CurrentPageIndex = 0
        BindDataGrid(dtgSchedule.CurrentPageIndex)
        ClearData()
    End Sub
    Private Function IsUnhack() As Boolean
        If txtDesc.Text.IndexOf("<") >= 0 Or txtDesc.Text.IndexOf(">") >= 0 Or txtDesc.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtScheduleName.Text.IndexOf("<") >= 0 Or txtScheduleName.Text.IndexOf(">") >= 0 Or txtScheduleName.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtScheduleName.Text.Trim = "" Then
            MessageBox.Show("Nama Jadwal harus diisi")
            Return False
        End If

        Return True
    End Function
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        'Untuk mozilla
        If Not Page.IsValid Then
            Return
        End If
        If Not IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If
        Dim nResult As Integer = -1

        Dim bCheckMandatory As Boolean = True
        'modifikasi untuk keperluan bug fixed

        If ddlYear.SelectedValue = "0" Then
            bCheckMandatory = False
            MessageBox.Show("Tahun belum dipilih !")
        End If

        If bCheckMandatory Then
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                If File.PostedFile.FileName <> "" Then
                    nResult = InsertSchedule(Path.GetFileName(Me.File.PostedFile.FileName))
                    If nResult = -2 Then
                        MessageBox.Show("Format file yang dipilih salah")
                    Else
                        If nResult = -1 Then
                            MessageBox.Show(SR.SaveFail)
                        Else
                            MessageBox.Show(SR.SaveSuccess)

                            ClearData()
                            dtgSchedule.CurrentPageIndex = 0
                            BindDataGrid(dtgSchedule.CurrentPageIndex)
                            dtgSchedule.SelectedIndex = -1
                        End If
                    End If
                Else
                    MessageBox.Show("Lokasi file tidak boleh kosong")
                End If
            Else
                nResult = UpdateSchedule(Path.GetFileName(Me.File.PostedFile.FileName))
                If nResult = -2 Then
                    MessageBox.Show("Format file yang dipilih salah")
                Else
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                        dtgSchedule.CurrentPageIndex = 0
                        BindDataGrid(dtgSchedule.CurrentPageIndex)
                        dtgSchedule.SelectedIndex = -1
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindDataGrid(0)
    End Sub

#End Region

    
End Class