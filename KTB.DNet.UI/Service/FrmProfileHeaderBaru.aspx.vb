#Region "Custom Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmProfileHeaderBaru
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlDataType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDataLength As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlControlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSelectionMode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMandatory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgProfileHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCodeProfile As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgProfile As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents lblLebar As System.Web.UI.WebControls.Label
    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblttk2 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Private objProfileDetail As ProfileDetailFacade
    Private objProfileHeader As ProfileHeaderFacade
    Private sessionHelper As New sessionHelper
    Private profDetail As ProfileDetail
    Private profHeader As ProfileHeader
    Private mode As Integer
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ProfileTransactionNew_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Profile Transaksi - Profile Baru")
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            InitiateAuthorization()
            BindHeaderToForm()
            BindDataToGrid()
        End If
    End Sub

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

    Private Sub AddDataToGrid(ByVal e As DataGridCommandEventArgs)
        Dim txtKode As TextBox = e.Item.FindControl("txtFooterKode")
        Dim txtDesc As TextBox = e.Item.FindControl("txtFooterDeskripsi")
        If (txtKode.Text.Trim = String.Empty Or txtDesc.Text.Trim = String.Empty) Then
            MessageBox.Show("Kode dan Deskripsi harus diisi")
            Exit Sub
        Else
            If (ValidateDuplicate(txtKode.Text)) Then
                profDetail = New ProfileDetail
                profDetail.Code = txtKode.Text
                profDetail.Description = txtDesc.Text
                Dim arrTempData As ArrayList = sessionHelper.GetSession("AddData")
                If arrTempData Is Nothing Then
                    arrTempData = New ArrayList
                End If
                arrTempData.Add(profDetail)
                sessionHelper.SetSession("AddData", arrTempData)
                BindDataToGrid()
            End If
        End If
    End Sub

    Private Function ValidateDuplicate(ByVal kode As String) As Boolean
        Dim arlCekdata As ArrayList = sessionHelper.GetSession("AddData")
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

    Private Sub BindDataToGrid()
        Dim arrTempData As ArrayList = sessionHelper.GetSession("AddData")
        If Not arrTempData Is Nothing Then
            dtgProfile.DataSource = arrTempData
        Else
            dtgProfile.DataSource = New ArrayList
        End If
        dtgProfile.DataBind()
    End Sub

    Private Sub SearchProfileHeaderDetails()
        profHeader = sessionHelper.GetSession("ProfileHeader")
        Dim criteria As New CriteriaComposite(New criteria(GetType(ProfileHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New criteria(GetType(ProfileHeader), "ID", MatchType.Exact, profHeader.ID))

        profHeader = New ProfileHeaderFacade(User).Retrieve(criteria)(0)
        sessionHelper.SetSession("ProfileHeader", profHeader)
    End Sub

    Private Function SaveProfileHeader() As Integer
        Dim objProfileHeaderFacade As ProfileHeaderFacade = New ProfileHeaderFacade(User)
        Dim profDetaillist As ArrayList = sessionHelper.GetSession("AddData")
        Dim objProfileHeader As New ProfileHeader
        objProfileHeader.Code = txtCode.Text
        objProfileHeader.Description = txtDescription.Text
        objProfileHeader.DataType = ddlDataType.SelectedValue

        objProfileHeader.ControlType = ddlControlType.SelectedValue
        objProfileHeader.Mandatory = ddlMandatory.SelectedValue
        objProfileHeader.Status = ddlStatus.SelectedValue

        If ddlDataType.SelectedValue = EnumDataType.DataType.Text And ddlControlType.SelectedValue = EnumControlType.ControlType.Text Then
            If txtDataLength.Text > 100 Then
                MessageBox.Show("Maksimum Lebar Data 100 Char")
                Return -1
            Else
                objProfileHeader.DataLength = CType(txtDataLength.Text, Integer)

                Return objProfileHeaderFacade.Insert(objProfileHeader, profDetaillist)
            End If
        ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Numeric Then
            If txtDataLength.Text > 30 Then
                MessageBox.Show("Maksimum Lebar Data 30")
                Return -1
            Else
                objProfileHeader.DataLength = CType(txtDataLength.Text, Integer)

                Return objProfileHeaderFacade.Insert(objProfileHeader, profDetaillist)
            End If
        ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Text And ddlControlType.SelectedValue = EnumControlType.ControlType.List Then
            If profDetaillist Is Nothing Then
                MessageBox.Show("Untuk Text-List, minimal harus ada 1 detail")
                Return -1
            Else
                objProfileHeader.DataLength = 0

                Return objProfileHeaderFacade.Insert(objProfileHeader, profDetaillist)
            End If
        ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Text And ddlControlType.SelectedValue = EnumControlType.ControlType.CheckListBox Then
            If profDetaillist Is Nothing Then
                MessageBox.Show("Untuk Text-CheckListBox, minimal harus ada 1 detail")
                Return -1
            Else
                objProfileHeader.DataLength = 0

                Return objProfileHeaderFacade.Insert(objProfileHeader, profDetaillist)
            End If
        End If
    End Function

    Private Function VaidateCodeDuplicate() As Integer
        Dim iCode As Integer
        profHeader = sessionHelper.GetSession("ProfileHeader")
        If profHeader Is Nothing Then
            iCode = New ProfileHeaderFacade(User).ValidateCode(txtCode.Text)
        Else
            iCode = New ProfileHeaderFacade(User).ValidateCode(profHeader.Code)
        End If
        Return iCode
    End Function

    Private Sub UpdateData(ByVal e As DataGridCommandEventArgs)
        Dim arrDataUpd As ArrayList = sessionHelper.GetSession("AddData")

        Dim lblNo As Label = e.Item.FindControl("lblNo")
        Dim lblKode As Label = e.Item.FindControl("lblEditKode")
        Dim txtDesc As TextBox = e.Item.FindControl("txtEditDeskripsi")

        profDetail = New ProfileDetail
        profDetail.ID = CType(lblNo.Text, Integer)
        profDetail.Code = lblKode.Text
        profDetail.Description = txtDesc.Text

        arrDataUpd.RemoveAt(CType(lblNo.Text, Integer) - 1)
        arrDataUpd.Insert((CType(lblNo.Text, Integer) - 1), profDetail)

        sessionHelper.SetSession("AddData", arrDataUpd)

        dtgProfile.EditItemIndex = -1
        BindDataToGrid()
    End Sub

    Private Function CekTextBoxForText(ByVal ddlControlType As DropDownList, ByVal txtDataLength As TextBox) As Boolean
        Dim retVal As Boolean = False
        If ddlControlType.SelectedValue = EnumControlType.ControlType.Text Then
            If txtDataLength.Text <> String.Empty Then
                retVal = True
            Else
                retVal = False
            End If
        ElseIf ddlDataType.SelectedValue = EnumDataType.DataType.Numeric And ddlControlType.SelectedValue = EnumControlType.ControlType.Text Then
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

    Private Sub ClearControl()
        txtCode.Text = ""
        txtDataLength.Text = ""
        txtDescription.Text = ""
        ddlControlType.SelectedIndex = 0
        ddlDataType.SelectedIndex = 0
        ddlMandatory.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
    End Sub
#End Region


    Private Sub dtgProfile_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProfile.ItemCommand
        If e.CommandName = "Add" Then
            'add the data
            AddDataToGrid(e)
        End If
    End Sub

    Private Sub dtgProfile_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProfile.DeleteCommand
        Dim arrData As ArrayList = sessionHelper.GetSession("AddData")
        profHeader = sessionHelper.GetSession("ProfileHeader")
        Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")

        mode = CType(sessionHelper.GetSession("SaveMode"), Integer)
        If mode = EnumSaveMode.Mode.SaveArrayList Then
            'delete the arrayList
            arrData.Remove(arrData.Item(CType(lbl1.Text, Integer) - 1))
            sessionHelper.SetSession("AddData", arrData)
            BindDataToGrid()
        End If
    End Sub

    Private Sub dtgProfile_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProfile.UpdateCommand
        UpdateData(e)
        dtgProfile.ShowFooter = True
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If CheckValidation() Then
            If VaidateCodeDuplicate() > 0 Then
                MessageBox.Show("Kode " & txtCode.Text & " sudah didefinisikan!")
            Else
                If CekTextBoxForText(ddlControlType, txtDataLength) Then
                    If SaveProfileHeader() <> -1 Then
                        mode = EnumSaveMode.Mode.SaveDB
                        sessionHelper.SetSession("SaveMode", mode)
                        sessionHelper.SetSession("ProfileHeader", profHeader)
                        MessageBox.Show("Simpan Profile Header berhasil")
                    Else
                        MessageBox.Show("Simpan Profile Header gagal")
                    End If
                Else
                    MessageBox.Show("Lebar data harus didefinisikan")
                End If
            End If
        End If
    End Sub

    Private Function CheckValidation() As Boolean
        Dim blnValid As Boolean = True

        If txtCode.Text = "" Then
            blnValid = False
            MessageBox.Show("Silakan input Kode terlebih dahulu")
            Return (blnValid)
        End If

        If txtDescription.Text = "" Then
            blnValid = False
            MessageBox.Show("Silakan input Deskripsi terlebih dahulu")
            Return (blnValid)
        End If

        If ddlDataType.SelectedIndex = 0 Then
            blnValid = False
            MessageBox.Show("Silakan pilih Tipe Data terlebih dahulu")
            Return (blnValid)
        End If

        If ddlControlType.SelectedIndex = 0 Then
            blnValid = False
            MessageBox.Show("Silakan pilih Tipe Kontrol terlebih dahulu")
            Return (blnValid)
        End If

        Return blnValid
    End Function

    Private Sub dtgProfile_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProfile.EditCommand
        Dim lblno As Label = e.Item.FindControl("lblNo")

        dtgProfile.EditItemIndex = CType(lblno.Text, Integer) - 1
        dtgProfile.ShowFooter = False
        BindDataToGrid()

    End Sub

    Private Sub dtgProfile_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProfile.CancelCommand
        dtgProfile.EditItemIndex = -1
        dtgProfile.ShowFooter = True
        BindDataToGrid()
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
End Class
