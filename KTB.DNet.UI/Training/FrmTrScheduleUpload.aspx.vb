#Region "Custom Namespace"
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.UI.Helper
Imports GlobalExtensions
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmTrScheduleUpload
    Inherits System.Web.UI.Page


    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property
    Private ReadOnly Property PrivMenu As String
        Get
            Select Case Request.QueryString("area")
                Case "1"
                    Return "A"
                Case "2"
                    Return "B"
                Case "3"
                    Return "C"
            End Select
            Return "B"
        End Get
    End Property

#Region "Custom Variable Declaration"
    Private sHScheduleUpload As New TrainingHelpers(Me.Page)
    Private _pathFolder As String = KTB.DNet.Lib.WebConfig.GetValue("TrainingScheduleDestFileDirectory")
#End Region


#Region "Custom Method"

    Private Sub TitleDescription(ByVal areaid As String)
        Dim desc As String = "Upload"
        If Me.IsDealer Then
            sHScheduleUpload.SetEdit(False)
            desc = "Download"
        End If

        If areaid.Equals("1") Then
            lblPageTitle.Text = String.Format("Training Sales - {0} Jadwal", desc)
        ElseIf areaid.Equals("2") Then
            lblPageTitle.Text = String.Format("Training After Sales - {0} Jadwal", desc)
        ElseIf areaid.Equals("3") Then
            lblPageTitle.Text = String.Format("Training Customer Satisfaction - {0} Jadwal", desc)
        Else
            lblPageTitle.Text = String.Format("Training - {0} Jadwal", desc)
        End If
    End Sub

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "ScheduleYear"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

    End Sub

    Private Sub ClearData()
        Me.txtDesc.ReadOnly = False
        Me.txtScheduleName.ReadOnly = False
        Me.ddlYear.Enabled = True
        Me.ddlKategory.Enabled = True
        Me.File.Disabled = False
        Me.txtDesc.Text = String.Empty
        Me.txtScheduleName.Text = String.Empty
        Me.ddlYear.SelectedIndex = 0
        If ddlKategory.Items.Count.Equals(2) Then
            Me.ddlKategory.SelectedIndex = 1
        Else
            Me.ddlKategory.SelectedIndex = 0
        End If

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

        ddlYear.Items.Insert(0, New ListItem("Pilih Tahun", "-1"))
    End Sub

    Private Sub BindDDLKategori()
        Dim criterias As CriteriaComposite
        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.JobPositionCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.JobPositionCategory), "AreaID", MatchType.Exact, AreaId))

        Dim arrCtg As ArrayList = New JobPositionCategoryFacade(User).Retrieve(criterias)
        ddlKategory.ClearSelection()
        ddlKategory.Items.Clear()
        ddlKategory.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each item As JobPositionCategory In arrCtg
            ddlKategory.Items.Add(New ListItem(item.Description, item.ID.ToString()))
        Next
        If arrCtg.Count.Equals(1) Then
            ddlKategory.SelectedIndex = 1
        Else
            ddlKategory.SelectedValue = "-1"
        End If

    End Sub

    Private Sub BindDataGrid(Optional ByVal indexPage As Integer = 0)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As CriteriaComposite
            criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If ddlYear.IsSelected Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "ScheduleYear", MatchType.Exact, CType(ddlYear.SelectedValue, Integer)))
            End If

            If ddlKategory.IsSelected Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "JobPositionCategory.ID", MatchType.Exact, CType(ddlKategory.SelectedValue, Integer)))
            End If

            If txtScheduleName.IsNotEmpty Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "Name", MatchType.[Partial], txtScheduleName.Text.Trim))
            End If

            If txtDesc.IsNotEmpty Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "Description", MatchType.[Partial], txtDesc.Text.Trim))
            End If
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrScheduleUpload), "JobPositionCategory.AreaID", MatchType.Exact, AreaId))

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
        objTrScheduleUpload.JobPositionCategory = New JobPositionCategory(CInt(ddlKategory.SelectedValue))

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If File.PostedFile.ContentType.ToString = "application/vnd.ms-excel" OrElse _
                File.PostedFile.ContentType.ToString = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" OrElse _
                File.PostedFile.ContentType.ToString = "application/octet-stream" OrElse
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
            If Not FileName.IsNullorEmpty Then
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

                        Dim objUpload As New UploadToWebServer
                        objUpload.Upload(Me.File.PostedFile.InputStream, newFileLocation)

                        CopyFileToWebServer(GetCompleteFilePath(newFileLocation))

                        objTrScheduleUpload.FilePath = _pathFolder & FileName
                        objTrScheduleUpload.UploadDate = Today

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
        ddlKategory.ClearSelection()
        ddlKategory.SelectedValue = objTrScheduleUpload.JobPositionCategory.ID.ToString()
        ddlYear.ClearSelection()
        ddlYear.SelectedValue = objTrScheduleUpload.ScheduleYear
        txtScheduleName.Text = objTrScheduleUpload.Name
        txtDesc.Text = objTrScheduleUpload.Description
        Me.btnSimpan.Enabled = EditStatus
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sHScheduleUpload.CheckPrivilege("Priv11" + PrivMenu)
        If Me.IsDealer Then
            sHScheduleUpload.SetEdit(False)
        End If
        If Not IsPostBack Then
            TitleDescription(AreaId)
            InitiatePage()
            SetControlPrivilege()
            BindToYear()
            BindDDLKategori()
            BindDataGrid()
        End If

    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = sHScheduleUpload.IsEdit
        btnBatal.Visible = sHScheduleUpload.IsEdit
        trUpload.Visible = sHScheduleUpload.IsEdit
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
            Me.ddlKategory.Enabled = False
            Me.txtScheduleName.ReadOnly = True
            Me.txtDesc.ReadOnly = True
            Me.File.Disabled = True
            dtgSchedule.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewGroup(e.Item.Cells(0).Text, True)
            dtgSchedule.SelectedIndex = e.Item.ItemIndex
            Me.ddlYear.Enabled = False
            Me.ddlKategory.Enabled = False
            Me.txtScheduleName.ReadOnly = False
            Me.txtDesc.ReadOnly = False
            Me.File.Disabled = False
        ElseIf e.CommandName = "Delete" Then
            DeleteSchedule(e.Item.Cells(0).Text)
        ElseIf e.CommandName = "Download" Then
            'Dim _linkButton As LinkButton = e.Item.FindControl("lbtnDownload")
            Dim _lblDownload As Label = e.Item.FindControl("lblDownload")

            'Dim fileInfo As New fileInfo(Server.MapPath("") & "\..\" & _lblDownload.Text)
            Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\" & _lblDownload.Text)
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
                    Dim lblKategori As Label = e.Item.FindControl("lblKategori")
                    Dim txtDescription As TextBox = e.Item.FindControl("txtDescription")
                    '_linkButton.Text = str(str.Length - 1) 'RowValue.FilePath
                    _lblDownload.Text = RowValue.FilePath
                    lblKategori.Text = RowValue.JobPositionCategory.Description
                    txtDescription.Text = RowValue.Description
                    txtDescription.Disabled()
                    txtDescription.BackColor = e.Item.BackColor
                    If Not e.Item.FindControl("btnHapus") Is Nothing Then
                        CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                    End If
                    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                        CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgSchedule.CurrentPageIndex * dtgSchedule.PageSize)
                    End If

                End If
            End If

        End If
        If sHScheduleUpload.IsEdit Then
            If Not e.Item.FindControl("btnUbah") Is Nothing Then
                CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = sHScheduleUpload.IsEdit
            End If

            If Not e.Item.FindControl("btnHapus") Is Nothing Then
                CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = sHScheduleUpload.IsEdit
            End If
        Else
            e.Item.Cells(e.Item.Cells.Count - 1).Visible = False
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


        If Not ddlYear.IsSelected Then
            bCheckMandatory = False
            MessageBox.Show("Tahun belum dipilih !")
        End If

        If Not ddlKategory.IsSelected Then
            bCheckMandatory = False
            MessageBox.Show("Kategori belum dipilih !")
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