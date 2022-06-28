#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.AlertManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports CKEditor.NET
#End Region

Imports System.IO

Public Class FrmTransactionalAlert
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealers As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlAlertCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAlertModul As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtStatus As System.Web.UI.WebControls.TextBox
    Protected WithEvents cblStatusList As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents txtNamaAlert As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDeskripsiAlert As CKEditor.NET.CKEditorControl
    Protected WithEvents txtJumlahKarakter As System.Web.UI.WebControls.TextBox
    Protected WithEvents icValidTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icValidFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents hdnJumlahKarakter As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtStartHour As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlFontEffect As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkViaDashboard As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtFrekuensiViaDashboard As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlFrekuensiTypeViaDashboard As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkViaAlert As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtFrekuensiViaAlert As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlFrekuensiTypeViaAlert As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkViaSMS As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtFrekuensiViaSMS As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlFrekuensiTypeViaSMS As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkViaEmail As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtFrekuensiViaEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlFrekuensiTypeViaEmail As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkIncludeHoliday As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtEndHour As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDiuploadOleh As System.Web.UI.WebControls.Label
    Protected WithEvents btnSaveAs As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents fileUploadSound As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents rdlAlertSounds As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents txtUserGroup As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlJenisAlert As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlJenisAlert As System.Web.UI.WebControls.DropDownList
    Protected WithEvents pnlStatus As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlFontEffect As System.Web.UI.WebControls.Panel
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents lblUserGroup As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"
    Dim _sHelper As New SessionHelper
#End Region

#Region "Cek Privilege"
    Dim bCekSetAlertPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.AlertManagementMaintain_Privilege)

#End Region

#Region "Properties"
    Private Property SesAlertMaster() As AlertMaster
        Get
            Return CType(_sHelper.GetSession("AlertMasterDomain"), AlertMaster)
        End Get
        Set(ByVal Value As AlertMaster)
            _sHelper.SetSession("AlertMasterDomain", Value)
        End Set
    End Property
    Private Property SesMode() As enumMode.Mode
        Get
            Return CType(_sHelper.GetSession("AlertMasterMode"), enumMode.Mode)
        End Get
        Set(ByVal Value As enumMode.Mode)
            _sHelper.SetSession("AlertMasterMode", Value)
        End Set
    End Property
    Private Property SesType() As EnumAlertManagement.AlertManagementType
        Get
            Return CType(_sHelper.GetSession("AlertMasterType"), EnumAlertManagement.AlertManagementType)
        End Get
        Set(ByVal Value As EnumAlertManagement.AlertManagementType)
            _sHelper.SetSession("AlertMasterType", Value)
        End Set
    End Property
    Private Property SesAlertStatuss() As ArrayList
        Get
            Return CType(_sHelper.GetSession("AlertStatuss"), ArrayList)
        End Get
        Set(ByVal Value As ArrayList)
            _sHelper.SetSession("AlertStatuss", Value)
        End Set
    End Property
    Private Property SesAlertGroups() As ArrayList
        Get
            Return CType(_sHelper.GetSession("AlertGroups"), ArrayList)
        End Get
        Set(ByVal Value As ArrayList)
            _sHelper.SetSession("AlertGroups", Value)
        End Set
    End Property
    Private Property SesAlertSounds() As ArrayList
        Get
            Return CType(_sHelper.GetSession("AlertSounds"), ArrayList)
        End Get
        Set(ByVal Value As ArrayList)
            _sHelper.SetSession("AlertSounds", Value)
        End Set
    End Property
#End Region

#Region "Custom Methods"
    Private Sub InitType()
        Select Case Request.QueryString("Mode").Split(";")(1)
            Case "T"
                SesType = EnumAlertManagement.AlertManagementType.Transactional
            Case "A"
                SesType = EnumAlertManagement.AlertManagementType.Announcement
        End Select
    End Sub
    Private Sub InitMode()
        Select Case Request.QueryString("Mode").Split(";")(0)
            Case "New"
                SesMode = enumMode.Mode.NewItemMode
            Case "Edit"
                SesMode = enumMode.Mode.EditMode
            Case "View"
                SesMode = enumMode.Mode.ViewMode
        End Select
    End Sub
    Private Sub InitSessions()
        If SesMode = enumMode.Mode.NewItemMode Then
            SesAlertMaster = New AlertMaster
            SesAlertStatuss = New ArrayList
            SesAlertGroups = New ArrayList
            SesAlertSounds = New ArrayList
            ClearForm()
        Else
            SesAlertMaster = New AlertMasterFacade(User).Retrieve(CInt(Request.QueryString("id")))
            SesAlertStatuss = SesAlertMaster.AlertStatuss
            SesAlertGroups = SesAlertMaster.AlertGroups
            SesAlertSounds = SesAlertMaster.AlertSounds
            FillForm()
        End If
    End Sub
    Private Sub ClearForm()
        ddlAlertCategory.SelectedIndex = 0
        ddlAlertModul.SelectedIndex = 0
        txtStatus.Text = String.Empty
        txtNamaAlert.Text = String.Empty
        txtDeskripsiAlert.Text = String.Empty
        txtJumlahKarakter.Text = String.Empty
        ddlFontEffect.SelectedIndex = 0
        ddlJenisAlert.SelectedIndex = 0
        icValidFrom.Value = Today
        icValidTo.Value = Today
        chkIncludeHoliday.Checked = False
        txtStartHour.Text = String.Empty
        txtEndHour.Text = String.Empty

        chkViaDashboard.Checked = False
        chkViaAlert.Checked = False
        chkViaSMS.Checked = False
        chkViaEmail.Checked = False

        txtFrekuensiViaDashboard.Text = String.Empty
        txtFrekuensiViaAlert.Text = String.Empty
        txtFrekuensiViaSMS.Text = String.Empty
        txtFrekuensiViaEmail.Text = String.Empty

        ddlFrekuensiTypeViaDashboard.SelectedIndex = 0
        ddlFrekuensiTypeViaAlert.SelectedIndex = 0
        ddlFrekuensiTypeViaSMS.SelectedIndex = 0
        ddlFrekuensiTypeViaEmail.SelectedIndex = 0

        txtUserGroup.Text = String.Empty

        rdlAlertSounds.Items.Clear()

    End Sub
    Private Sub FillForm()
        Dim objdomain As AlertMaster
        objdomain = SesAlertMaster

        Try
            ddlAlertCategory.SelectedValue = objdomain.AlertModul.AlertCategory.ID
            RebindModulDropdownList()
        Catch ex As Exception
            MessageBox.Show("Kategori modul " + objdomain.AlertModul.AlertCategory.Description + " tidak ditemukan dalam list.")
        End Try

        Try
            ddlAlertModul.SelectedValue = objdomain.AlertModul.ID
        Catch ex As Exception
            MessageBox.Show("Modul " + objdomain.AlertModul.Description + " tidak ditemukan dalam list.")
        End Try


        If IsTransactionalAlert() Then
            If SesAlertStatuss.Count > 0 Then
                For Each item As AlertStatus In SesAlertStatuss
                    For Each iList As ListItem In cblStatusList.Items
                        If item.Status = iList.Value Then
                            iList.Selected = True
                            txtStatus.Text += iList.Text & ";"
                            Exit For
                        End If
                    Next
                Next
            Else
                txtStatus.Text = String.Empty
            End If
            ddlFontEffect.SelectedValue = objdomain.FontEffect
            chkIncludeHoliday.Checked = objdomain.IsIncludeHoliday
        End If

        If IsAnnouncementAlert() Then
            ddlJenisAlert.SelectedValue = objdomain.AnnouncementAlertType
        End If

        txtNamaAlert.Text = objdomain.Name
        txtDeskripsiAlert.Text = objdomain.Desc
        hdnJumlahKarakter.Value = txtDeskripsiAlert.Text.Length
        icValidFrom.Value = objdomain.DateValidFrom
        icValidTo.Value = objdomain.DateValidTo
        txtStartHour.Text = objdomain.TimeStartFrom.ToString("HH:mm")
        txtEndHour.Text = objdomain.TimeStartTo.ToString("HH:mm")

        chkViaDashboard.Checked = objdomain.IsViaDashboard
        chkViaAlert.Checked = objdomain.IsViaAlertBox
        chkViaSMS.Checked = objdomain.IsViaSMS
        chkViaEmail.Checked = objdomain.IsViaEmail

        txtFrekuensiViaDashboard.Text = objdomain.ViaDashboardFrequency
        txtFrekuensiViaAlert.Text = objdomain.ViaAlertBoxFrequency
        txtFrekuensiViaSMS.Text = objdomain.ViaSMSFrequency
        txtFrekuensiViaEmail.Text = objdomain.ViaEmailFrequency

        ddlFrekuensiTypeViaDashboard.SelectedValue = objdomain.ViaDashboardFreqType.Trim()
        ddlFrekuensiTypeViaAlert.SelectedValue = objdomain.ViaAlertBoxFreqType.Trim()
        ddlFrekuensiTypeViaSMS.SelectedValue = objdomain.ViaSMSFreqType.Trim()
        ddlFrekuensiTypeViaEmail.SelectedValue = objdomain.ViaEmailFreqType.Trim()

        If SesAlertGroups.Count > 0 Then
            Dim sGroup As String = String.Empty
            For Each item As AlertGroup In SesAlertGroups
                sGroup += item.UserGroup.Code & ";"
            Next
            If sGroup.Length > 0 Then
                sGroup = sGroup.Substring(0, sGroup.Length - 1)
            End If
            txtUserGroup.Text = sGroup
        Else
            txtUserGroup.Text = String.Empty
        End If

        InitDisplayAlertSound()

        If SesAlertSounds.Count > 0 Then
            Dim i As Integer = 0
            For Each sound As AlertSound In SesAlertSounds
                If sound.IsSelected Then
                    rdlAlertSounds.SelectedValue = sound.ID
                End If
                i += 1
            Next
        End If


    End Sub
    Private Sub InitDisplay()       ' set display for transactional alert and announcement alert
        If SesMode = enumMode.Mode.NewItemMode Then
            btnSaveAs.Visible = False
            btnCancel.Visible = False
        Else
            btnSaveAs.Visible = True
            btnCancel.Visible = True
        End If

        If SesMode = enumMode.Mode.ViewMode Then
            ddlAlertCategory.Enabled = False
            ddlAlertModul.Enabled = False
            lblDealers.Visible = False
            txtNamaAlert.Enabled = False
            ddlFontEffect.Enabled = False
            icValidFrom.Enabled = False
            icValidTo.Enabled = False
            txtStartHour.Enabled = False
            txtEndHour.Enabled = False
            chkViaAlert.Enabled = False
            chkIncludeHoliday.Enabled = False
            chkViaDashboard.Enabled = False
            chkViaEmail.Enabled = False
            chkViaSMS.Enabled = False
            txtFrekuensiViaAlert.Enabled = False
            txtFrekuensiViaDashboard.Enabled = False
            txtFrekuensiViaEmail.Enabled = False
            txtFrekuensiViaSMS.Enabled = False
            ddlFrekuensiTypeViaAlert.Enabled = False
            ddlFrekuensiTypeViaDashboard.Enabled = False
            ddlFrekuensiTypeViaEmail.Enabled = False
            ddlFrekuensiTypeViaSMS.Enabled = False
            lblUserGroup.Visible = False
            txtUserGroup.Enabled = False
            fileUploadSound.Visible = False
            btnUpload.Visible = False
            rdlAlertSounds.Enabled = False

            btnSave.Visible = False
            btnSaveAs.Visible = False
            btnCancel.Visible = True
        End If


        If IsTransactionalAlert() Then
            pnlStatus.Visible = True
            pnlFontEffect.Visible = True
            chkIncludeHoliday.Visible = True
        Else
            pnlStatus.Visible = False
            pnlFontEffect.Visible = False
            chkIncludeHoliday.Visible = False
        End If

        If IsAnnouncementAlert() Then
            InitDisplayJenisAlert()
            pnlJenisAlert.Visible = True
        Else
            pnlJenisAlert.Visible = False
        End If

        InitDisplayAlertCategory()
        InitDisplayFontEffect()
        InitDisplayAlertMediaDropdowns()
        Dim objDomain As AlertMaster = SesAlertMaster
    End Sub
    Private Sub InitDisplayAlertMediaDropdowns()

        ddlFrekuensiTypeViaAlert.Items.Clear()
        ddlFrekuensiTypeViaAlert.DataSource = New EnumAlertManagement().RetrieveAlertMediaType()
        ddlFrekuensiTypeViaAlert.DataTextField = "NameStatus"
        ddlFrekuensiTypeViaAlert.DataValueField = "ValStatus"
        ddlFrekuensiTypeViaAlert.DataBind()

        ddlFrekuensiTypeViaDashboard.Items.Clear()
        ddlFrekuensiTypeViaDashboard.DataSource = New EnumAlertManagement().RetrieveAlertMediaType()
        ddlFrekuensiTypeViaDashboard.DataTextField = "NameStatus"
        ddlFrekuensiTypeViaDashboard.DataValueField = "ValStatus"
        ddlFrekuensiTypeViaDashboard.DataBind()

        ddlFrekuensiTypeViaEmail.Items.Clear()
        ddlFrekuensiTypeViaEmail.DataSource = New EnumAlertManagement().RetrieveAlertMediaType()
        ddlFrekuensiTypeViaEmail.DataTextField = "NameStatus"
        ddlFrekuensiTypeViaEmail.DataValueField = "ValStatus"
        ddlFrekuensiTypeViaEmail.DataBind()

        ddlFrekuensiTypeViaSMS.Items.Clear()
        ddlFrekuensiTypeViaSMS.DataSource = New EnumAlertManagement().RetrieveAlertMediaType()
        ddlFrekuensiTypeViaSMS.DataTextField = "NameStatus"
        ddlFrekuensiTypeViaSMS.DataValueField = "ValStatus"
        ddlFrekuensiTypeViaSMS.DataBind()

        ddlFrekuensiTypeViaAlert.SelectedIndex = 0
        ddlFrekuensiTypeViaDashboard.SelectedIndex = 0
        ddlFrekuensiTypeViaEmail.SelectedIndex = 0
        ddlFrekuensiTypeViaSMS.SelectedIndex = 0
    End Sub
    Private Sub InitDisplayFontEffect()
        ddlFontEffect.Items.Clear()
        ddlFontEffect.DataSource = New EnumAlertManagement().RetrieveTextEffects()
        ddlFontEffect.DataTextField = "NameStatus"
        ddlFontEffect.DataValueField = "ValStatus"
        ddlFontEffect.DataBind()
    End Sub
    Private Sub InitDisplayJenisAlert()
        ddlJenisAlert.Items.Clear()
        ddlJenisAlert.DataSource = New EnumAlertManagement().RetrieveAnnouncementAlertType()
        ddlJenisAlert.DataTextField = "NameStatus"
        ddlJenisAlert.DataValueField = "ValStatus"
        ddlJenisAlert.DataBind()
    End Sub

    Private Sub InitDisplayAlertCategory()
        Dim facade As New AlertCategoryFacade(User)
        Dim arlAlertCategory As ArrayList = facade.RetrieveActiveList()

        ddlAlertCategory.Items.Clear()
        If arlAlertCategory.Count > 0 Then
            ddlAlertCategory.DataSource = arlAlertCategory
            ddlAlertCategory.DataTextField = "Description"
            ddlAlertCategory.DataValueField = "ID"
            ddlAlertCategory.DataBind()
            ddlAlertCategory.SelectedIndex = 0

            RebindModulDropdownList()
        End If
    End Sub
    Private Sub RebindModulDropdownList()
        Dim categoryId As Integer = CInt(ddlAlertCategory.SelectedValue)
        Dim arlModul As ArrayList = New AlertModulFacade(User).RetrieveActiveListByCategoryID(categoryId)

        ddlAlertModul.Items.Clear()
        If arlModul.Count > 0 Then
            ddlAlertModul.DataSource = arlModul
            ddlAlertModul.DataTextField = "Description"
            ddlAlertModul.DataValueField = "ID"
            ddlAlertModul.DataBind()
            ddlAlertModul.SelectedIndex = 0

            RebindStatusCheckboxes()
        End If
    End Sub
    Private Sub RebindStatusCheckboxes()
        Dim modulId As Integer = CInt(ddlAlertModul.SelectedValue)

        Dim objModul As AlertModul = New AlertModulFacade(User).Retrieve(modulId)
        txtStatus.Text = String.Empty
        cblStatusList.Items.Clear()
        Try
            Dim objEnum As Object = Activator.CreateInstance(objModul.EnumAssemblyName, objModul.EnumClassName).Unwrap
            Dim methodInfoRetrieveStatusList As System.Reflection.MethodInfo = objEnum.GetType().GetMethod(objModul.EnumMethodToCall)
            Dim objResult As Object = methodInfoRetrieveStatusList.Invoke(objEnum, Nothing)
            cblStatusList.DataSource = objResult
            cblStatusList.DataTextField = objModul.EnumStatusNamePropertyName
            cblStatusList.DataValueField = objModul.EnumStatusIDPropertName
            cblStatusList.DataBind()
        Catch ex As Exception
            'MessageBox.Show("Gagal mengambil daftar status")
        End Try

    End Sub

    Private Function IsFileOKToSave(ByVal fileUpload As HtmlInputFile) As Boolean

        If fileUpload.PostedFile Is Nothing OrElse fileUpload.Value = String.Empty Then
            Return False
        End If

        If Path.GetExtension(fileUpload.PostedFile.FileName).ToLower() <> ".wav" Then
            Return False
        End If

        Return True
    End Function
    Private Function IsOKToSave(ByVal isSaveAs As Boolean) As Boolean

        Dim objdomain As AlertMaster


        Dim strErrMsg As String = String.Empty
        If IsTransactionalAlert() Then
            If Not IsOneStatusSelected() Then
                strErrMsg += "Status harus dipilih.\n"
            End If
        End If
        If IsNamaAlertEmpty() Then
            strErrMsg += "Nama Alert harus diisi.\n"
        Else
            If SesMode = enumMode.Mode.NewItemMode Or isSaveAs Then
                If IsNamaAlertAlreadyExists(True) Then
                    strErrMsg += "Nama Alert sudah ada.\n"
                End If
            ElseIf SesMode = enumMode.Mode.EditMode Then
                If IsNamaAlertAlreadyExists(False) Then
                    strErrMsg += "Nama Alert sudah ada.\n"
                End If
            End If
        End If

        If IsDeskripsiAlertEmpty() Then
            strErrMsg += "Deskripsi Alert harus diisi.\n"
        End If
        If Not IsPeriodValidIsValid() Then
            strErrMsg += "Tanggal Valid awal harus lebih kecil dari tanggal valid akhir.\n"
        End If

        If Not IsHourValid(txtStartHour.Text.Trim()) Then
            strErrMsg += "Jam Mulai tidak valid. Jam dan menit harus diisi angka.\n"
        End If

        If Not IsHourValid(txtEndHour.Text.Trim()) Then
            strErrMsg += "Jam Akhir tidak valid. Jam dan menit harus diisi angka.\n"
        End If

        If IsHourValid(txtStartHour.Text.Trim()) And IsHourValid(txtEndHour.Text.Trim()) Then
            Try
                Dim StartTime As New DateTime(1900, 1, 1, txtStartHour.Text.Trim.Split(":")(0), txtStartHour.Text.Trim.Split(":")(0), 0)
                Dim EndTime As New DateTime(1900, 1, 1, txtEndHour.Text.Trim.Split(":")(0), txtEndHour.Text.Trim.Split(":")(0), 59)
                If StartTime > EndTime Then
                    strErrMsg += "Jam mulai tidak boleh lebih besar dari jam akhir.\n"
                End If
            Catch ex As Exception
                strErrMsg += "Jam mulai atau jam akhir tidak valid. Jam dan menit harus diisi angka.\n"
            End Try
        End If

        If Not IsOneAlertMediaEntered() Then
            strErrMsg += "Alert Media harus diisi.\n"
        End If

        If IsTransactionalAlert() Then
            If Not IsFrekuensiAlertEntered() Then
                strErrMsg += "Frekuensi alert harus diisi.\n"
            End If

            If Not IsFrekuensiAlertValid() Then
                strErrMsg += "Frekuensi alert harus diisi angka.\n"
            End If
        End If


        If Not IsUserGroupEntered() Then
            strErrMsg += "User Group harus diisi.\n"
        End If

        'If rdlAlertSounds.Items.Count > 0 Then
        '    Dim isSelected As Boolean = False
        '    For Each item As ListItem In rdlAlertSounds.Items
        '        If item.Selected Then
        '            isSelected = True
        '        End If
        '    Next
        '    If Not isSelected Then
        '        strErrMsg += "Silakan pilih Sound yg akan di gunakan."
        '    End If
        'End If

        If strErrMsg.Length > 0 Then
            MessageBox.Show(strErrMsg)
            Return False
        End If

        Return True
    End Function
    Private Function IsTransactionalAlert() As Boolean
        If SesType = EnumAlertManagement.AlertManagementType.Transactional Then
            Return True
        End If
        Return False
    End Function
    Private Function IsAnnouncementAlert() As Boolean
        If SesType = EnumAlertManagement.AlertManagementType.Announcement Then
            Return True
        End If
        Return False
    End Function
    Private Function IsOneStatusSelected() As Boolean
        For Each item As ListItem In cblStatusList.Items
            If item.Selected Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function IsNamaAlertEmpty() As Boolean
        Return txtNamaAlert.Text.Trim().Length <= 0
    End Function
    Private Function IsNamaAlertAlreadyExists(ByVal IsSaveAs) As Boolean

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(AlertMaster), "Name", MatchType.Exact, txtNamaAlert.Text.Trim()))

        If Not IsSaveAs Then
            Dim objdomain As AlertMaster
            objdomain = SesAlertMaster

            criterias.opAnd(New Criteria(GetType(AlertMaster), "ID", MatchType.No, objdomain.ID))
        End If

        Dim arl As ArrayList = New AlertMasterFacade(User).Retrieve(criterias)
        Return arl.Count > 0
    End Function
    Private Function IsDeskripsiAlertEmpty() As Boolean
        Return txtDeskripsiAlert.Text.Trim().Length <= 0
    End Function
    Private Function IsPeriodValidIsValid() As Boolean
        Return DateDiff(DateInterval.Day, icValidFrom.Value, icValidTo.Value) >= 0
    End Function
    Private Function IsHourValid(ByVal strHour As String) As Boolean
        Dim arrTime As String() = strHour.Split(":".ToCharArray())

        If arrTime.Length < 2 Then
            Return False
        End If

        If (Not IsNumeric(arrTime(0))) Or (Not IsNumeric(arrTime(1))) Then
            Return False
        End If

        Dim hour As Integer = CInt(arrTime(0))
        Dim minute As Integer = CInt(arrTime(1))
        If hour < 0 Or hour > 23 Then
            Return False
        End If

        If minute < 0 Or minute > 59 Then
            Return False
        End If
        Return True
    End Function
    Private Function IsOneAlertMediaEntered() As Boolean
        Return (chkViaAlert.Checked Or chkViaDashboard.Checked Or chkViaEmail.Checked Or chkViaSMS.Checked)
    End Function
    Private Function IsFrekuensiAlertEntered()
        Dim strFreq As String
        If chkViaAlert.Checked Then
            If txtFrekuensiViaAlert.Text.Trim() = String.Empty Then
                Return False
            End If
        End If
        If chkViaDashboard.Checked Then
            If txtFrekuensiViaDashboard.Text.Trim() = String.Empty Then
                Return False
            End If
        End If
        If chkViaEmail.Checked Then
            If txtFrekuensiViaEmail.Text.Trim() = String.Empty Then
                Return False
            End If
        End If
        If chkViaSMS.Checked Then
            If txtFrekuensiViaSMS.Text.Trim() = String.Empty Then
                Return False
            End If
        End If
        Return True
    End Function
    Private Function IsFrekuensiAlertValid() As Boolean
        Dim strFreq As String
        If chkViaAlert.Checked Then
            strFreq = txtFrekuensiViaAlert.Text.Trim()
            If strFreq.Length <= 0 Then
                Return False
            End If
            Return IsNumeric(strFreq)
        End If
        If chkViaDashboard.Checked Then
            strFreq = txtFrekuensiViaDashboard.Text.Trim()
            If strFreq.Length <= 0 Then
                Return False
            End If
            Return IsNumeric(strFreq)
        End If
        If chkViaEmail.Checked Then
            strFreq = txtFrekuensiViaEmail.Text.Trim()
            If strFreq.Length <= 0 Then
                Return False
            End If
            Return IsNumeric(strFreq)
        End If
        If chkViaSMS.Checked Then
            strFreq = txtFrekuensiViaSMS.Text.Trim()
            If strFreq.Length <= 0 Then
                Return False
            End If
            Return IsNumeric(strFreq)
        End If
        Return False
    End Function
    Private Function IsUserGroupEntered() As Boolean
        Return txtUserGroup.Text.Trim().Length > 0
    End Function

    Private Sub InitDisplayAlertSound()   'hrs di cek kebenarannya
        rdlAlertSounds.Items.Clear()
        If SesAlertSounds.Count > 0 Then
            lblDiuploadOleh.Text = CommonFunction.FormatSavedUser(SesAlertMaster.UploadedBy, User)
            rdlAlertSounds.DataSource = SesAlertSounds
            rdlAlertSounds.DataValueField = "ID"
            rdlAlertSounds.DataTextField = "FileName"
            rdlAlertSounds.DataBind()
        End If

    End Sub
    Private Sub ExtractTimeFromString(ByVal str As String, ByRef hourHolder As Integer, ByRef minuteHolder As Integer)
        Dim arrTime As String() = str.Split(":".ToCharArray())
        hourHolder = CInt(arrTime(0))
        minuteHolder = CInt(arrTime(1))
    End Sub

    Private Sub SaveFile(ByVal fileUpload As HtmlInputFile, ByVal objAlertSound As AlertSound)
        Dim dirName As String = KTB.DNet.Lib.WebConfig.GetValue("AlertManagement")
        Dim SrcFile As String = Path.GetFileName(fileUpload.PostedFile.FileName)  '-- Source file name
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & dirName & "\" & SrcFile     '-- Destination file
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                fileUpload.PostedFile.SaveAs(DestFile)
                objAlertSound.FilePath = dirName & "\" & SrcFile
                objAlertSound.FileName = SrcFile

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SaveAlertMaster(ByVal isSaveAs As Boolean)
        Dim objdomain As AlertMaster

        If isSaveAs Then
            objdomain = New AlertMaster
            SesMode = enumMode.Mode.NewItemMode
        Else
            objdomain = SesAlertMaster
        End If



        objdomain.AlertModul = New AlertModulFacade(User).Retrieve(CInt(ddlAlertModul.SelectedValue))
        objdomain.Name = txtNamaAlert.Text.Trim()
        objdomain.Desc = txtDeskripsiAlert.Text.Trim()
        objdomain.AlertType = SesType

        If IsTransactionalAlert() Then
            SesAlertStatuss = New ArrayList
            For Each item As ListItem In cblStatusList.Items
                If item.Selected Then
                    Dim sts As New AlertStatus
                    sts.AlertMaster = objdomain
                    sts.Status = CInt(item.Value)
                    SesAlertStatuss.Add(sts)
                End If
            Next
            objdomain.FontEffect = ddlFontEffect.SelectedValue
            objdomain.IsIncludeHoliday = chkIncludeHoliday.Checked
        End If

        If IsAnnouncementAlert() Then
            objdomain.AnnouncementAlertType = ddlJenisAlert.SelectedValue
        End If

        objdomain.DateValidFrom = New DateTime(icValidFrom.Value.Year, icValidFrom.Value.Month, icValidFrom.Value.Day, 0, 0, 0)
        objdomain.DateValidTo = New DateTime(icValidTo.Value.Year, icValidTo.Value.Month, icValidTo.Value.Day, 23, 59, 59)
        Dim hour As Integer
        Dim minute As Integer
        ExtractTimeFromString(txtStartHour.Text.Trim(), hour, minute)
        objdomain.TimeStartFrom = New DateTime(1900, 1, 1, hour, minute, 0)
        ExtractTimeFromString(txtEndHour.Text.Trim(), hour, minute)
        objdomain.TimeStartTo = New DateTime(1900, 1, 1, hour, minute, 0)

        objdomain.IsViaAlertBox = chkViaAlert.Checked
        objdomain.ViaAlertBoxFreqType = ddlFrekuensiTypeViaAlert.SelectedValue
        If txtFrekuensiViaAlert.Text.Trim().Length > 0 Then
            objdomain.ViaAlertBoxFrequency = CInt(txtFrekuensiViaAlert.Text.Trim())
        End If

        objdomain.IsViaDashboard = chkViaDashboard.Checked
        objdomain.ViaDashboardFreqType = ddlFrekuensiTypeViaDashboard.SelectedValue
        If txtFrekuensiViaDashboard.Text.Trim().Length > 0 Then
            objdomain.ViaDashboardFrequency = CInt(txtFrekuensiViaDashboard.Text.Trim())
        End If

        objdomain.IsViaEmail = chkViaEmail.Checked
        objdomain.ViaEmailFreqType = ddlFrekuensiTypeViaEmail.SelectedValue
        If txtFrekuensiViaEmail.Text.Trim().Length > 0 Then
            objdomain.ViaEmailFrequency = CInt(txtFrekuensiViaEmail.Text.Trim())
        End If

        objdomain.IsViaSMS = chkViaSMS.Checked
        objdomain.ViaSMSFreqType = ddlFrekuensiTypeViaSMS.SelectedValue
        If txtFrekuensiViaSMS.Text.Trim().Length > 0 Then
            objdomain.ViaSMSFrequency = CInt(txtFrekuensiViaSMS.Text.Trim())
        End If

        SesAlertGroups = New ArrayList
        Dim arrGroup As String() = txtUserGroup.Text.Trim().Split(";".ToCharArray())
        For Each strGroup As String In arrGroup
            Dim objAlertGroup As New AlertGroup
            objAlertGroup.UserGroup = New UserManagement.UserGroupFacade(User).Retrieve(strGroup)
            SesAlertGroups.Add(objAlertGroup)
        Next

        If rdlAlertSounds.Items.Count > 0 Then
            Dim i As Integer = 0
            For Each item As ListItem In rdlAlertSounds.Items
                CType(SesAlertSounds.Item(i), AlertSound).IsSelected = item.Selected
                i += 1
            Next
        End If

        Dim result As Integer
        Dim facade As New AlertMasterFacade(User)

        If SesMode = enumMode.Mode.EditMode Then
            result = facade.Update(objdomain, SesAlertStatuss, SesAlertGroups, SesAlertSounds)
        ElseIf SesMode = enumMode.Mode.NewItemMode Then
            result = facade.Insert(objdomain, SesAlertStatuss, SesAlertGroups, SesAlertSounds)
        End If

        If result <> -1 Then
            SesMode = enumMode.Mode.EditMode
            MessageBox.Show(SR.SaveSuccess)
            'ReloadForm(result)
        Else
            MessageBox.Show(SR.SaveFail)
        End If

    End Sub

    Private Sub ReloadForm(ByVal id As Integer)
        If SesMode = enumMode.Mode.EditMode Then
            Server.Transfer("~/AlertManagement/FrmTransactionalAlert.aspx?Mode=Edit;" & Request.QueryString("Mode").Split(";")(1) & "&id=" & id.ToString)
        ElseIf SesMode = enumMode.Mode.ViewMode Then
            Server.Transfer("~/AlertManagement/FrmTransactionalAlert.aspx?Mode=View;" & Request.QueryString("Mode").Split(";")(1) & "&id=" & id.ToString)
        End If
    End Sub

#End Region

#Region "Event"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.RegisterStartupScript("txtStatusHolder", "<script language=javascript> var txtStatusHolder = document.all." + txtStatus.ClientID + ";</script>")
        'txtJumlahKarakter.Text = txtDeskripsiAlert.Value.Length.ToString()
        If Not Me.IsPostBack Then
            InitMode()
            InitType()
            InitDisplay()
            InitSessions()
        End If
        btnSave.Enabled = bCekSetAlertPriv
        txtUserGroup.Enabled = bCekSetAlertPriv
        lblUserGroup.Visible = bCekSetAlertPriv
    End Sub

    Private Sub ddlAlertCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAlertCategory.SelectedIndexChanged
        RebindModulDropdownList()
    End Sub
    Private Sub ddlAlertModul_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAlertModul.SelectedIndexChanged
        RebindStatusCheckboxes()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If IsOKToSave(False) Then
            SaveAlertMaster(False)
        End If
    End Sub
    Private Sub btnSaveAs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAs.Click
        If IsOKToSave(True) Then
            SaveAlertMaster(True)
        End If
    End Sub
    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        If Not IsFileOKToSave(fileUploadSound) Then
            MessageBox.Show("Tipe file Tidak Valid")
            Return
        End If
        Dim objAlertSound As New AlertSound
        SaveFile(fileUploadSound, objAlertSound)
        SesAlertMaster.UploadedBy = User.Identity.Name

        objAlertSound.AlertMaster = SesAlertMaster
        SesAlertSounds.Add(objAlertSound)
        InitDisplayAlertSound()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Server.Transfer("~/AlertManagement/FrmAlertManagemenList.aspx?Type=" & Request.QueryString("Mode").Split(";")(1))
    End Sub
#End Region

End Class
