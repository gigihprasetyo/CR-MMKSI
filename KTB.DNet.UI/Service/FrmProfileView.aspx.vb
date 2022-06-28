#Region "Custom Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmProfileView
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlDataType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlControlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMandatory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblLebar As System.Web.UI.WebControls.Label
    Protected WithEvents lblttk2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDataLength As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgProfile As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "internal enum"
    Private Enum ActionMode
        ActExists = 0
        ActDelete = 1
        ActUpdate = 2
        ActNew = 3
    End Enum
#End Region

#Region "deklarasi"
    Private _ProfHeader As ProfileHeader
    Private _ProfHeaderFacade As New ProfileHeaderFacade(User)
    Private _profDetail As ProfileDetail
    Private _ArlData As ArrayList
    Private _sHelper As New SessionHelper
    Private _vMode As String
#End Region

#Region "custom method"
    Private Sub BindHeaderToForm()
        Me.ddlDataType.DataSource = New EnumDataType().RetrieveDataType()
        Me.ddlDataType.DataTextField = "DescStatus"
        Me.ddlDataType.DataValueField = "ValStatus"
        Me.ddlDataType.DataBind()
        Me.ddlDataType.Items.Insert(0, "Pilih")

        Me.ddlControlType.DataSource = New EnumControlType().RetrieveControlType
        Me.ddlControlType.DataTextField = "DescStatus"
        Me.ddlControlType.DataValueField = "ValStatus"
        Me.ddlControlType.DataBind()
        Me.ddlControlType.Items.Insert(0, "Pilih")

        Me.ddlMandatory.DataSource = New EnumMandatory().RetrieveMandatory
        Me.ddlMandatory.DataTextField = "DescStatus"
        Me.ddlMandatory.DataValueField = "ValStatus"
        Me.ddlMandatory.DataBind()

        Me.ddlStatus.DataSource = New EnumStatusProfile().RetrieveStatusMode
        Me.ddlStatus.DataTextField = "DescStatus"
        Me.ddlStatus.DataValueField = "ValStatus"
        Me.ddlStatus.DataBind()
    End Sub

    Private Sub ConfigureControlData(ByVal vmode As String)
        If Not (vmode.Equals("EDIT")) Then
            dtgProfile.ShowFooter = False
            dtgProfile.Columns(3).Visible = False
            dtgProfile.Columns(4).Visible = False
            dtgProfile.Columns(5).Visible = False
            txtCode.Enabled = False
            txtDataLength.Enabled = False
            txtDescription.Enabled = False
            ddlControlType.Enabled = False
            ddlDataType.Enabled = False
            ddlMandatory.Enabled = False
            ddlStatus.Enabled = False
            btnSimpan.Enabled = False
        Else
            Dim id As Integer = CInt(_sHelper.GetSession("PHid"))
            Dim facade As New ProfileHeaderToGroupFacade(User)
            If facade.ValidateHeaderAssign(id) = False Then
                If ddlDataType.SelectedValue <> "Pilih" Then
                    If ddlDataType.SelectedValue = EnumDataType.DataType.Text And ddlControlType.SelectedValue = EnumControlType.ControlType.Text Then
                        ddlControlType.Enabled = True
                        ddlDataType.Enabled = True
                        dtgProfile.ShowFooter = False
                        txtDataLength.Enabled = True
                    ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Numeric Then
                        ddlControlType.Enabled = True
                        ddlDataType.Enabled = True
                        dtgProfile.ShowFooter = False
                        txtDataLength.Enabled = True
                    ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Dates Then
                        ddlControlType.Enabled = True
                        ddlDataType.Enabled = True
                        dtgProfile.ShowFooter = False
                        txtDataLength.Enabled = False
                    Else
                        ddlControlType.Enabled = False
                        ddlDataType.Enabled = False
                        dtgProfile.ShowFooter = True
                        txtDataLength.Enabled = True
                    End If
                End If
                txtCode.Enabled = True
                txtDescription.Enabled = True
                ddlMandatory.Enabled = True
                ddlStatus.Enabled = True
                btnSimpan.Enabled = True
            Else
                If ddlDataType.SelectedValue = EnumDataType.DataType.Text And ddlControlType.SelectedValue = EnumControlType.ControlType.Text Then
                    dtgProfile.ShowFooter = False
                    txtDataLength.Enabled = True
                ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Numeric Then
                    dtgProfile.ShowFooter = False
                    txtDataLength.Enabled = True
                ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Dates Then
                    dtgProfile.ShowFooter = False
                    txtDataLength.Enabled = False
                Else
                    dtgProfile.ShowFooter = True
                    txtDataLength.Enabled = False
                    dtgProfile.Columns(4).Visible = False
                End If
                txtCode.Enabled = False
                txtDescription.Enabled = False
                ddlControlType.Enabled = False
                ddlDataType.Enabled = False
                ddlMandatory.Enabled = True
                ddlStatus.Enabled = True
                btnSimpan.Enabled = True
            End If
        End If
    End Sub

    Private Sub ConfigureViewHeaderData(ByVal profHeader As ProfileHeader)
        txtCode.Text = profHeader.Code
        txtDescription.Text = profHeader.Description
        txtDataLength.Text = profHeader.DataLength
        ddlControlType.SelectedValue = CInt(profHeader.ControlType)
        ddlDataType.SelectedValue = profHeader.DataType
        ddlMandatory.SelectedValue = profHeader.Mandatory
        ddlStatus.SelectedValue = profHeader.Status
        If ddlControlType.SelectedValue = EnumControlType.ControlType.Text Then
            txtDataLength.Visible = True
            lblLebar.Visible = True
            lblttk2.Visible = True
            dtgProfile.ShowFooter = False
        Else
            If Not (ddlDataType.SelectedValue = "Pilih" OrElse ddlControlType.SelectedValue = "Pilih") Then
                'cek jika tipe data text dengan kontrol list atau checklistbox,
                'walau tidak diassign akan tetapi punya detail, tipe dan kontrol tidak bisa diubah
                If (ddlDataType.SelectedValue = EnumDataType.DataType.Text And ddlControlType.SelectedValue = EnumControlType.ControlType.CheckListBox) Then
                    If profHeader.ProfileDetails.Count > 0 Then
                        ddlDataType.Enabled = False
                        ddlControlType.Enabled = False
                        txtDataLength.Visible = False
                        lblLebar.Visible = False
                        lblttk2.Visible = False
                        dtgProfile.ShowFooter = True
                    Else
                        ddlDataType.Enabled = True
                        ddlControlType.Enabled = True
                        txtDataLength.Visible = False
                        lblLebar.Visible = False
                        lblttk2.Visible = False
                        dtgProfile.ShowFooter = True
                    End If
                ElseIf (ddlDataType.SelectedValue = EnumDataType.DataType.Text And ddlControlType.SelectedValue = EnumControlType.ControlType.List) Then
                    If profHeader.ProfileDetails.Count > 0 Then
                        ddlDataType.Enabled = False
                        ddlControlType.Enabled = False
                        txtDataLength.Visible = False
                        lblLebar.Visible = False
                        lblttk2.Visible = False
                        dtgProfile.ShowFooter = True
                    Else
                        ddlDataType.Enabled = True
                        ddlControlType.Enabled = True
                        txtDataLength.Visible = False
                        lblLebar.Visible = False
                        lblttk2.Visible = False
                        dtgProfile.ShowFooter = True
                    End If
                ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Text And ddlControlType.SelectedValue = EnumControlType.ControlType.Text Then
                    dtgProfile.ShowFooter = False
                End If
                'Else
                '    txtDataLength.Visible = False
                '    lblLebar.Visible = False
                '    lblttk2.Visible = False
                '    dtgProfile.ShowFooter = True
            End If
        End If
    End Sub

    'hanya digunakan pada saat page load
    Private Sub BindToGrid(ByVal profHeader As ProfileHeader)
        If Not profHeader.ProfileDetails.Count > 0 Then
            _ArlData = New ArrayList
            dtgProfile.DataSource = _ArlData
        Else
            _ArlData = profHeader.ProfileDetails
            dtgProfile.DataSource = _ArlData
        End If
        dtgProfile.DataBind()
        _sHelper.SetSession("AddData", _ArlData)
    End Sub

    Private Sub BindDataToGrid()
        Dim arrTempData As ArrayList = _sHelper.GetSession("AddData")

        If Not arrTempData Is Nothing Then
            dtgProfile.DataSource = arrTempData
        Else
            dtgProfile.DataSource = New ArrayList
        End If
        '_sHelper.SetSession("DataDeleted", arrDeleted)
        _sHelper.SetSession("AddData", arrTempData)
        dtgProfile.DataBind()
    End Sub


    Private Sub AddDataToGrid(ByVal e As DataGridCommandEventArgs)
        Dim txtKode As TextBox = e.Item.FindControl("txtFooterKode")
        Dim txtDesc As TextBox = e.Item.FindControl("txtFooterDeskripsi")
        Dim vMode As String = CType(viewstate("ViewMode"), String)
        If Not (vMode = "EDIT" Or vMode = "DETAILS") Then
            _ProfHeader = _sHelper.GetSession("ProfileHeader")
            If (txtKode.Text.Trim() = String.Empty And txtDesc.Text.Trim() = String.Empty) Then
                MessageBox.Show("Kode dan Deskripsi harus diisi")
                Exit Sub
            Else
                If (ValidateDuplicate(txtKode.Text)) Then
                    Dim arrTempData As ArrayList = _sHelper.GetSession("AddData")
                    _profDetail = New ProfileDetail
                    _profDetail.ProfileHeader = _ProfHeader
                    _profDetail.Code = txtKode.Text
                    _profDetail.Description = txtDesc.Text
                    _profDetail.RowStatus = ActionMode.ActNew

                    If arrTempData Is Nothing Then
                        arrTempData = New ArrayList
                    Else
                        arrTempData.Add(_profDetail)
                        _sHelper.SetSession("AddData", arrTempData)
                        BindDataToGrid()
                    End If
                End If
            End If
        ElseIf vMode = "EDIT" Then
            Dim objPH As ProfileHeader = _sHelper.GetSession("ProfileHeaderEdit")
            Dim arlPD As ArrayList = objPH.ProfileDetails
            _sHelper.SetSession("AddData", arlPD)
            If (txtKode.Text.Trim() = String.Empty Or txtDesc.Text.Trim() = String.Empty) Then
                MessageBox.Show("Kode dan Deskripsi harus diisi")
                Exit Sub
            Else
                If ValidateDuplicate(txtKode.Text) Then
                    Dim arrTempData As ArrayList = _sHelper.GetSession("AddData")
                    _profDetail = New ProfileDetail
                    _profDetail.ProfileHeader = objPH
                    _profDetail.Code = txtKode.Text
                    _profDetail.Description = txtDesc.Text
                    _profDetail.RowStatus = ActionMode.ActNew

                    If arrTempData Is Nothing Then
                        arrTempData = New ArrayList
                    Else
                        arrTempData.Add(_profDetail)
                        _sHelper.SetSession("AddData", arrTempData)
                        BindDataToGrid()
                    End If
                End If
            End If
        End If
    End Sub

    Private Function ValidateDuplicate(ByVal kode As String) As Boolean
        Dim arlCekdata As ArrayList = _sHelper.GetSession("AddData")
        If Not arlCekdata Is Nothing Then
            For Each item As ProfileDetail In arlCekdata
                If (item.Code.ToUpper = kode.ToUpper) Then
                    MessageBox.Show("Error: Duplikasi Kode")
                    Return False
                End If
            Next
        End If
        Return True
    End Function

    Private Sub UpdateData(ByVal e As DataGridCommandEventArgs)
        Dim arrDataUpd As ArrayList = _sHelper.GetSession("AddData")

        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
        Dim lblHeaderID As Label = CType(e.Item.FindControl("lblHeaderID"), Label)
        Dim lblKode As Label = e.Item.FindControl("lblEditKode")

        Dim txtDesc As TextBox = e.Item.FindControl("txtEditDeskripsi")

        _profDetail = New ProfileDetail
        _profDetail.ID = CType(lblID.Text, Integer)
        _profDetail.ProfileHeader = New ProfileHeaderFacade(User).Retrieve(CType(lblHeaderID.Text, Integer))
        _profDetail.Code = lblKode.Text
        _profDetail.Description = txtDesc.Text

        arrDataUpd.RemoveAt(CType(lblNo.Text, Integer) - 1)
        arrDataUpd.Insert((CType(lblNo.Text, Integer) - 1), _profDetail)

        _sHelper.SetSession("AddData", arrDataUpd)


        dtgProfile.EditItemIndex = -1
        BindDataToGrid()
    End Sub

    Private Function SaveProfileHeader() As Integer
        Dim profDetaillist As ArrayList = _sHelper.GetSession("AddData")
        Dim profDetailDeleted As ArrayList = _sHelper.GetSession("DeletedData")
        Dim objProfileHeaderFacade As ProfileHeaderFacade = New ProfileHeaderFacade(User)

        '_ProfHeader = _sHelper.GetSession("ProfileHeader") 
        _ProfHeader = _sHelper.GetSession("ProfileHeaderEdit")
        If _ProfHeader Is Nothing Then
            _ProfHeader = New ProfileHeader
        End If

        _ProfHeader.Code = txtCode.Text
        _ProfHeader.Description = txtDescription.Text
        _ProfHeader.DataType = ddlDataType.SelectedValue
        _ProfHeader.ControlType = ddlControlType.SelectedValue
        _ProfHeader.Mandatory = ddlMandatory.SelectedValue
        _ProfHeader.Status = ddlStatus.SelectedValue

        If ddlDataType.SelectedValue = EnumDataType.DataType.Text And ddlControlType.SelectedValue = EnumControlType.ControlType.Text Then
            If txtDataLength.Text > 100 Then
                MessageBox.Show("Maksimum Lebar Data 100 Char")
                Return -1
            Else
                _ProfHeader.DataLength = CType(txtDataLength.Text, Integer)

                Return objProfileHeaderFacade.Update(_ProfHeader, profDetaillist, profDetailDeleted)
            End If
        ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Numeric Then
            If txtDataLength.Text > 30 Then
                MessageBox.Show("Maksimum Lebar Data 30 Digit ")
                Return -1
            Else
                _ProfHeader.DataLength = CType(txtDataLength.Text, Integer)

                Return objProfileHeaderFacade.Update(_ProfHeader, profDetaillist, profDetailDeleted)
            End If
        ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Text And ddlControlType.SelectedValue = EnumControlType.ControlType.List Then
            If profDetaillist Is Nothing Then
                MessageBox.Show("Untuk Text-List, minimal harus ada 1 detail")
                Return -1
            Else
                _ProfHeader.DataLength = 0

                Return objProfileHeaderFacade.Update(_ProfHeader, profDetaillist, profDetailDeleted)
            End If
        ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Text And ddlControlType.SelectedValue = EnumControlType.ControlType.CheckListBox Then
            If profDetaillist Is Nothing Then
                MessageBox.Show("Untuk Text-CheckListBox, minimal harus ada 1 detail")
                Return -1
            Else
                _ProfHeader.DataLength = 0

                Return objProfileHeaderFacade.Update(_ProfHeader, profDetaillist, profDetailDeleted)
            End If
        Else
            _ProfHeader.DataLength = 0
            Return objProfileHeaderFacade.Update(_ProfHeader, profDetaillist, profDetailDeleted)
        End If
    End Function

    Private Function CekTextBoxForText(ByVal ddlControlType As DropDownList, ByVal txtDataLength As TextBox) As Boolean
        Dim retVal As Boolean = False
        If ddlControlType.SelectedValue = EnumControlType.ControlType.Text Then
            If txtDataLength.Text <> String.Empty Then
                retVal = True
            Else
                retVal = False
            End If
        ElseIf ddlControlType.SelectedValue = EnumControlType.ControlType.List Or ddlControlType.SelectedValue = EnumControlType.ControlType.Calendar Or ddlControlType.SelectedValue = EnumControlType.ControlType.CheckListBox Then
            retVal = True
        End If
        Return retVal
    End Function

#End Region

#Region "Cek Privilege"
    Private cekCmdBtnPrivilege As Boolean
    Private Function CekButtonPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ProfileListGroupEdit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            cekCmdBtnPrivilege = CekButtonPrivilege()
            BindHeaderToForm()
            _vMode = _sHelper.GetSession("ViewMode")
            If _vMode = "DETAILS" Then
                ConfigureControlData(_vMode)
                _ProfHeader = _sHelper.GetSession("ProfileHeader")
                ConfigureViewHeaderData(_ProfHeader)
                viewstate.Add("ViewMode", _vMode)
                BindToGrid(_ProfHeader)
            ElseIf _vMode = "EDIT" Then
                Dim id As Integer = CInt(_sHelper.GetSession("PHid"))
                Dim objProfileHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(id)
                ConfigureViewHeaderData(objProfileHeader)
                ConfigureControlData(_vMode)
                viewstate.Add("ViewMode", _vMode)
                BindToGrid(objProfileHeader)
                _sHelper.SetSession("ProfileHeaderEdit", objProfileHeader)
                If IsProfileUsedInTransaction(objProfileHeader) Then
                    ddlDataType.Enabled = False
                    ddlControlType.Enabled = False
                    txtCode.ReadOnly = True
                    txtDescription.ReadOnly = True
                End If
            End If
            If cekCmdBtnPrivilege = False Then
                btnSimpan.Enabled = False
            Else
                btnSimpan.Enabled = True
            End If
        End If
    End Sub

    Private Function IsProfileUsedInTransaction(ByVal objProfile As ProfileHeader) As Boolean
        Dim criterias As CriteriaComposite
        Dim List As ArrayList

        'CustomerRequestProfile
        criterias = New CriteriaComposite(New Criteria(GetType(CustomerRequestProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerRequestProfile), "ProfileHeader.ID", MatchType.Exact, objProfile.ID))
        List = New CustomerRequestProfileFacade(User).Retrieve(criterias)
        If Not List Is Nothing Then
            If List.Count > 0 Then
                Return True
            End If
        End If

        'DealerProfile
        criterias = New CriteriaComposite(New Criteria(GetType(DealerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DealerProfile), "ProfileHeader.ID", MatchType.Exact, objProfile.ID))
        List = New DealerProfileFacade(User).Retrieve(criterias)
        If Not List Is Nothing Then
            If List.Count > 0 Then
                Return True
            End If
        End If

        'CustomerProfile
        criterias = New CriteriaComposite(New Criteria(GetType(CustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerProfile), "ProfileHeader.ID", MatchType.Exact, objProfile.ID))
        List = New CustomerProfileFacade(User).Retrieve(criterias)
        If Not List Is Nothing Then
            If List.Count > 0 Then
                Return True
            End If
        End If

        'ChasisMasterProfile
        criterias = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, objProfile.ID))
        List = New ChassisMasterProfileFacade(User).Retrieve(criterias)
        If Not List Is Nothing Then
            If List.Count > 0 Then
                Return True
            End If
        End If

        'SalesmanProfile
        criterias = New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, objProfile.ID))
        List = New SalesmanProfileFacade(User).Retrieve(criterias)
        If Not List Is Nothing Then
            If List.Count > 0 Then
                Return True
            End If
        End If

        'PQRProfile
        criterias = New CriteriaComposite(New Criteria(GetType(PQRProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PQRProfile), "ProfileHeader.ID", MatchType.Exact, objProfile.ID))
        List = New PQRProfileFacade(User).Retrieve(criterias)
        If Not List Is Nothing Then
            If List.Count > 0 Then
                Return True
            End If
        End If

        Return False
    End Function
    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        _sHelper.RemoveSession("AddData")
        _sHelper.RemoveSession("DeletedData")
        _sHelper.SetSession("BackMode", "FrmProfileHeader")
        Response.Redirect("FrmProfileHeader.aspx")
    End Sub

    Private Sub dtgProfile_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProfile.ItemCommand
        If e.CommandName = "Add" Then
            'add the data
            AddDataToGrid(e)
        End If
    End Sub

    Private Sub dtgProfile_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProfile.DeleteCommand
        Dim objPH As ProfileHeader = _sHelper.GetSession("ProfileHeaderEdit")
        Dim Facade As ProfileHeaderToGroupFacade = New ProfileHeaderToGroupFacade(User)
        If Facade.ValidateHeaderAssign(objPH.ID) = True Then
            MessageBox.Show("Profile Header ini sudah di-assign ke Profile Group, Item tidak bisa dihapus")
        Else
            Dim profDetailList As ArrayList = _sHelper.GetSession("AddData")
            Dim profDetailsDeleted As New ArrayList
            Dim lblno As Label = CType(e.Item.FindControl("lblNo"), Label)
            _profDetail = New ProfileDetail
            Dim indexData As Integer = CInt(lblno.Text) - 1
            Dim lblkode As Label = CType(e.Item.FindControl("lblKode"), Label)
            Dim lbldesc As Label = CType(e.Item.FindControl("lblDeskripsi"), Label)

            _profDetail = profDetailList.Item(indexData)
            If _profDetail.RowStatus = ActionMode.ActNew Then
                profDetailList.RemoveAt(indexData)
            ElseIf _profDetail.RowStatus = DBRowStatus.Active Then
                'delete data first at indexdata
                profDetailList.RemoveAt(indexData)

                _profDetail.Code = lblkode.Text
                _profDetail.Description = lbldesc.Text
                _profDetail.RowStatus = DBRowStatus.Deleted
                profDetailsDeleted.Add(_profDetail)
            End If

            _sHelper.SetSession("AddData", profDetailList)
            _sHelper.SetSession("DeletedData", profDetailsDeleted)
            BindDataToGrid()
        End If
    End Sub

    Private Sub dtgProfile_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProfile.EditCommand
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

        dtgProfile.EditItemIndex = CType(lblNo.Text, Integer) - 1
        dtgProfile.ShowFooter = False

        BindDataToGrid()
    End Sub

    Private Sub dtgProfile_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProfile.CancelCommand
        dtgProfile.EditItemIndex = -1
        dtgProfile.ShowFooter = True
        BindDataToGrid()
    End Sub

    Private Sub dtgProfile_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProfile.UpdateCommand
        UpdateData(e)
        dtgProfile.ShowFooter = True
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim vModeSave As String = CType(viewstate("ViewMode"), String)
        If Not (vModeSave = "EDIT" Or vModeSave = "DETAILS") Then
            If CekTextBoxForText(ddlControlType, txtDataLength) Then
                SaveProfileHeader()
                MessageBox.Show("Simpan Profile Header berhasil")
                'Response.Redirect("FrmProfileHeader.aspx")
            Else
                MessageBox.Show("Lebar data harus didefinisikan")
            End If
        ElseIf vModeSave = "EDIT" Then
            If CekTextBoxForText(ddlControlType, txtDataLength) Then
                If SaveProfileHeader() <> -1 Then
                    MessageBox.Show("Simpan Profile Header berhasil")
                Else
                    MessageBox.Show("Simpan Profile Header gagal")
                End If

                'Response.Redirect("FrmProfileHeader.aspx")
            Else
                MessageBox.Show("Lebar data harus didefinisikan")
            End If
        End If
    End Sub

    Private Sub ddlDataType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlDataType.SelectedIndexChanged
        If ddlDataType.SelectedIndex <> 0 Then
            If ddlDataType.SelectedValue = EnumDataType.DataType.Dates Then
                ddlControlType.SelectedIndex = 0
                ddlControlType.SelectedValue = EnumControlType.ControlType.Calendar
                ddlControlType.Enabled = False
                dtgProfile.ShowFooter = False
            ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Numeric Then
                ddlControlType.SelectedIndex = 0
                ddlControlType.SelectedValue = EnumControlType.ControlType.Text
                ddlControlType.Enabled = False
                dtgProfile.ShowFooter = False
                lblLebar.Visible = True
                lblttk2.Visible = True
                txtDataLength.Visible = True
            Else
                ddlControlType.SelectedIndex = 0
                ddlControlType.Enabled = True
                dtgProfile.ShowFooter = True
            End If
        End If
    End Sub

    Private Sub ddlControlType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlControlType.SelectedIndexChanged
        If ddlControlType.SelectedIndex <> 0 Then
            If ddlControlType.SelectedValue = EnumControlType.ControlType.Text Then
                dtgProfile.ShowFooter = False
                lblLebar.Visible = True
                lblttk2.Visible = True
                txtDataLength.Visible = True
            ElseIf ddlControlType.SelectedValue = EnumControlType.ControlType.Calendar Then
                'cek di tipe data 
                If ddlDataType.SelectedValue = EnumDataType.DataType.Text Then
                    MessageBox.Show("Untuk tipe data text, Tipe kontrol tidak menerima Calendar")
                    ddlControlType.SelectedIndex = 0
                End If
            ElseIf (ddlControlType.SelectedValue = EnumControlType.ControlType.List Or ddlControlType.SelectedValue = EnumControlType.ControlType.CheckListBox) Then
                dtgProfile.ShowFooter = True
                lblLebar.Visible = False
                lblttk2.Visible = False
                txtDataLength.Visible = False
            End If
        End If
    End Sub
End Class
