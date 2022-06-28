#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmUserGroup
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgUserRole1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents RequiredFieldValidator10 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtUserGroup As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpBulletinMember As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

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
    Dim sHelper As New SessionHelper
    'Dim arlUseGroupMember As ArrayList
    Dim arrList As ArrayList
    Dim criterias As CriteriaComposite
    Private cekPrivileges As Boolean
    Private cekAdminEditGroupUser As Boolean
#End Region

#Region "internal enum"
    Private Enum Mode
        InsertData = 1
        EditData = 2
    End Enum
#End Region

#Region "Custom Method"





#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.AdminViewGroupUserList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - User Group")
        End If
    End Sub

    Dim bCekBtnGridPriv As Boolean = SecurityProvider.Authorize(context.User, SR.AdminViewGroupUserList_Privilege)
    Dim bCekEditGroupPriv As Boolean = SecurityProvider.Authorize(context.User, SR.AdminEditGroupUser_Privilege)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        'ReadCriteria()
        If Not IsPostBack Then
            ReadCriteria()
            ViewState("CurrentSortColumn") = "Code"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            ViewState("process") = "insert"
            btnSimpan.Text = "Simpan"
            If Request.QueryString("isBack") <> String.Empty And Request.QueryString("isBack") = "true" Then
                ViewState("process") = "cari"
                ViewState("CurrentSortColumn") = sHelper.GetSession("SortColumnGroup")
                ViewState("CurrentSortDirect") = sHelper.GetSession("SortDirectGroup")
                BindGrid(sHelper.GetSession("idxPageGroup"))
            Else
                BindGrid(0)
            End If
        End If

        If ViewState("process") = "edit" Then
            btnSimpan.Text = "Ubah"
        Else
            btnSimpan.Text = "Simpan"
        End If
        btnSimpan.Visible = bCekEditGroupPriv
    End Sub

    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(UserGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtUserGroup.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(UserGroup), "Code", MatchType.[Partial], txtUserGroup.Text.Trim))
        End If

        If txtDeskripsi.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(UserGroup), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
        End If
        SaveCriteria()
    End Sub

    Private Sub SaveCriteria()
        '-- Save selection criteria for later restore
        Dim crits As Hashtable = New Hashtable
        crits.Add("UserGroup", txtUserGroup.Text.Trim())
        crits.Add("Description", txtDeskripsi.Text.Trim())
        sHelper.SetSession("FrmUserGroupCrits", crits)
    End Sub

    Private Sub ReadCriteria()
        '-- Restore selection criteria

        Dim crits As Hashtable
        crits = CType(sHelper.GetSession("FrmUserGroupCrits"), Hashtable)
        If Not IsNothing(crits) Then
            txtUserGroup.Text = CStr(crits.Item("UserGroup"))
            txtDeskripsi.Text = CStr(crits.Item("Description"))
        End If
    End Sub

    Private Sub BindGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        If ViewState("process") = "cari" Then
            ViewState("process") = "insert"
            CreateCriteria()
            arrList = New UserGroupFacade(User).RetrieveActiveList(idxPage + 1, dtgUserRole1.PageSize, totalRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"), criterias)
        Else
            arrList = New UserGroupFacade(User).RetrieveActiveList(idxPage + 1, dtgUserRole1.PageSize, totalRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
        End If

        dtgUserRole1.CurrentPageIndex = idxPage
        If arrList.Count > 0 Then
            dtgUserRole1.DataSource = arrList
        Else
            dtgUserRole1.DataSource = New ArrayList
            MessageBox.Show("Data tidak ditemukan")
        End If

        dtgUserRole1.VirtualItemCount = totalRow
        dtgUserRole1.DataBind()

        sHelper.SetSession("SortColumnGroup", ViewState("CurrentSortColumn"))
        sHelper.SetSession("SortDirectGroup", ViewState("CurrentSortDirect"))
        sHelper.SetSession("idxPageGroup", idxPage)

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        If ViewState("process") = "insert" Then
            RequiredFieldValidator10.Enabled = True
        End If
        If txtUserGroup.Text = "" Then
            MessageBox.Show("User group harus diisi")
            Exit Sub
        End If

        Dim modeData As Integer = CInt(sHelper.GetSession("Mode"))
        Dim objUserGroupFacade As New UserGroupFacade(User)

        If Not Page.IsValid Then
            Return
        End If

        Dim idEdit As Integer
        If ViewState("process") = "edit" Then
            idEdit = CType(sHelper.GetSession("objToEdit"), UserGroup).ID
        Else
            idEdit = 0
        End If

        If (New UserGroupFacade(User).ValidateCode(txtUserGroup.Text.Trim, idEdit)) > 0 Then
            MessageBox.Show("Kode Sudah Ada")
            Return
        End If

        Dim result As Integer

        If ViewState("process") = "edit" Then
            Dim objToEdit As UserGroup = sHelper.GetSession("objToEdit")
            objToEdit.Description = txtDeskripsi.Text
            objToEdit.Code = txtUserGroup.Text.Trim
            result = New UserGroupFacade(User).Update(objToEdit)

        Else
            Dim objToInsert As UserGroup = New UserGroup
            objToInsert.Description = txtDeskripsi.Text
            objToInsert.Code = txtUserGroup.Text.Trim
            result = New UserGroupFacade(User).Insert(objToInsert)

        End If

        If result > 0 Then
            MessageBox.Show(SR.SaveSuccess)
            ViewState("process") = "insert"
            btnSimpan.Text = "Simpan"
            txtDeskripsi.Text = ""
            txtUserGroup.Text = ""
            BindGrid(dtgUserRole1.CurrentPageIndex)
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub dtgUserRole1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgUserRole1.ItemCommand
        Dim facade As UserGroupFacade = New UserGroupFacade(User)
        If e.CommandName = "Delete" Then
            Dim result As Integer = facade.DeleteFromDB(facade.Retrieve(CInt(e.CommandArgument)))
            If result > 0 Then
                MessageBox.Show(SR.DeleteSucces)
                BindGrid(0)
            Else
                MessageBox.Show(SR.DeleteFail)
            End If

        ElseIf e.CommandName = "Edit" Then
            ViewState("process") = "edit"
            btnSimpan.Text = "Ubah"
            Dim objToEdit As UserGroup = facade.Retrieve(CInt(e.CommandArgument))
            txtDeskripsi.Text = objToEdit.Description
            txtUserGroup.Text = objToEdit.Code
            sHelper.SetSession("objToEdit", objToEdit)

        ElseIf e.CommandName = "View" Then
            ViewState("process") = "insert"
            btnSimpan.Text = "Simpan"
            Response.Redirect("FrmAddUserOnGroup.aspx?id=" & e.CommandArgument)
        End If
    End Sub

    Private Sub dtgUserRole1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUserRole1.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim obj As UserGroup = e.Item.DataItem

            Dim lblno As Label = CType(e.Item.FindControl("lblno"), Label)
            lblno.Text = (dtgUserRole1.CurrentPageIndex * dtgUserRole1.PageSize) + e.Item.ItemIndex + 1

            'cek privilege
            Dim lbtndelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)

            lbtndelete.CommandArgument = obj.ID.ToString
            lbtndelete.Attributes.Add("onclick", "return confirm('Yakin ingin dihapus?');")
            lbtndelete.Visible = bCekEditGroupPriv
            lbtnView.CommandArgument = obj.ID.ToString
            lbtnView.Visible = bCekEditGroupPriv
            lbtnEdit.CommandArgument = obj.ID.ToString
            lbtnEdit.Visible = bCekEditGroupPriv
        End If
    End Sub

    Private Sub dtgUserRole1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgUserRole1.PageIndexChanged
        dtgUserRole1.CurrentPageIndex = e.NewPageIndex
        BindGrid(dtgUserRole1.CurrentPageIndex)

    End Sub

    Private Sub dtgUserRole1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgUserRole1.SortCommand

        If e.SortExpression = ViewState("CurrentSortColumn") Then
            If ViewState("CurrentSortDirect") = Sort.SortDirection.ASC Then
                ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            Else
                ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End If
        End If
        ViewState("CurrentSortColumn") = e.SortExpression
        dtgUserRole1.SelectedIndex = -1
        BindGrid(dtgUserRole1.CurrentPageIndex)

    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ViewState("process") = "insert"
        btnSimpan.Text = "Simpan"
        txtDeskripsi.Text = ""
        txtUserGroup.Text = ""

    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        ViewState("process") = "cari"
        RequiredFieldValidator10.Enabled = False
        dtgUserRole1.CurrentPageIndex = 0
        BindGrid(dtgUserRole1.CurrentPageIndex)
    End Sub
End Class
