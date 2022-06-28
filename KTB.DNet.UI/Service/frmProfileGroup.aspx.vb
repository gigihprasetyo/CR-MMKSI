Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class frmProfileGroup
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents dgProfileGroup As System.Web.UI.WebControls.DataGrid
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvDeskripsi As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _edit As Boolean
    Private _view As Boolean
    Private ProfileGroupFacade As New ProfileGroupFacade(User)
    Private sessHelper As New SessionHelper
#End Region

#Region "PrivateCustomMethods"
    Private Sub ViewProfileGroup(ByVal idProfileGroup As Integer)
        Dim ObjProfileGroup As ProfileGroup = ProfileGroupFacade.Retrieve(idProfileGroup)
        txtCode.Text = ObjProfileGroup.Code
        txtDescription.Text = ObjProfileGroup.Description
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ProfileGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtCode.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(ProfileGroup), "Code", MatchType.Exact, txtCode.Text.Trim()))
        End If
        If txtDescription.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(ProfileGroup), "Description", MatchType.Exact, txtDescription.Text.Trim()))
        End If
        arrList = ProfileGroupFacade.RetrieveByCriteria(criterias, idxPage + 1, dgProfileGroup.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgProfileGroup.DataSource = arrList
        dgProfileGroup.VirtualItemCount = totalRow
        dgProfileGroup.DataBind()
    End Sub
    Private Sub EnableControl(ByVal isEnable As Boolean)
        If CType(sessHelper.GetSession("Status"), String) = "Update" And isEnable Then
            txtCode.ReadOnly = True
        Else
            txtCode.ReadOnly = Not isEnable
        End If
        txtDescription.ReadOnly = Not isEnable
    End Sub
    Private Sub Initialize()
        txtCode.Text = ""
        txtDescription.Text = ""
        'btnSave.Enabled = True
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        sessHelper.SetSession("Status", "Insert")
    End Sub
    Private Sub CheckPrivilege()
        'If Not SecurityProvider.Authorize(context.User, SR.ENHGeneralPosisiJabatanView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=MASTER - Posisi Jabatan")
        'End If
        '_edit = SecurityProvider.Authorize(context.User, SR.ENHGeneralPosisiJabatanUbah_Privilege)
        '_view = SecurityProvider.Authorize(context.User, SR.ENHGeneralPosisiJabatanView_Privilege)
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ProfileGroupView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Profile Transaksi - Profile Baru")
        End If
    End Sub

    Private CekButtonSimpan As Boolean
    Private Function SaveButtonPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ProfileGroupEdit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CheckPrivilege()
        If Not IsPostBack Then
            InitiateAuthorization()
            CekButtonSimpan = SaveButtonPrivilege()
            If CekButtonSimpan = False Then
                btnSave.Enabled = False
            Else
                btnSave.Enabled = True
            End If
            Initialize()
            EnableControl(True)
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Page.IsValid Then
            If CType(sessHelper.GetSession("Status"), String) = "Insert" Then
                If ProfileGroupFacade.IsProfileGroupFound(txtCode.Text.Trim()) Then
                    MessageBox.Show(SR.DataIsExist("Profile Group"))
                    Return
                End If
                Dim ObjProfileGroup As New ProfileGroup
                ObjProfileGroup.Code = txtCode.Text.Trim()
                ObjProfileGroup.Description = txtDescription.Text.Trim()
                Dim i As Integer = ProfileGroupFacade.Insert(ObjProfileGroup)
                If i = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    Initialize()
                    EnableControl(True)
                    BindDataGrid(0)
                    MessageBox.Show(SR.SaveSuccess)
                End If
            ElseIf CType(sessHelper.GetSession("Status"), String) = "Update" Then
                Dim idProfileGroup As Integer = CInt(sessHelper.GetSession("IDProfileGroup"))
                Dim ObjProfileGroup As ProfileGroup = ProfileGroupFacade.Retrieve(idProfileGroup)
                ObjProfileGroup.Code = txtCode.Text.Trim()
                ObjProfileGroup.Description = txtDescription.Text.Trim()
                Dim i As Integer = ProfileGroupFacade.Update(ObjProfileGroup)
                If i = -1 Then
                    MessageBox.Show(SR.UpdateFail)
                Else
                    Initialize()
                    EnableControl(True)
                    BindDataGrid(0)
                    MessageBox.Show(SR.UpdateSucces)
                End If
            End If
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Initialize()
        EnableControl(True)
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        BindDataGrid(0)
    End Sub
    Private Sub dgProfileGroup_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgProfileGroup.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgProfileGroup.CurrentPageIndex * dgProfileGroup.PageSize)
            Dim lView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            If SaveButtonPrivilege() = False Then
                lView.Visible = False
                lEdit.Visible = False
                lDelete.Visible = False
            Else
                lView.Visible = True
                lEdit.Visible = True
                lDelete.Visible = True
            End If
        End If
    End Sub
    Private Sub dgProfileGroup_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProfileGroup.ItemCommand
        If e.CommandName = "View" Then
            ViewProfileGroup(CInt(e.Item.Cells(0).Text))
            EnableControl(False)
            btnSave.Enabled = False
        ElseIf e.CommandName = "Edit" Then
            sessHelper.SetSession("IDProfileGroup", CInt(e.Item.Cells(0).Text))
            sessHelper.SetSession("Status", "Update")
            ViewProfileGroup(CInt(e.Item.Cells(0).Text))
            EnableControl(True)
            btnSave.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Dim ObjProfileGroup As ProfileGroup = ProfileGroupFacade.Retrieve(CInt(e.Item.Cells(0).Text))
            Try
                ProfileGroupFacade.Delete(ObjProfileGroup)
                'Initialize()
                EnableControl(True)
                BindDataGrid(0)
                MessageBox.Show(SR.DeleteSucces)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        ElseIf e.CommandName = "ProfileHeader" Then
            sessHelper.SetSession("IDProfileGroup", CInt(e.Item.Cells(0).Text))
            Response.Redirect("frmProfileGroupDetail.aspx?qwqewqwqewwkiopqreopqropr=" & e.Item.Cells(0).Text)
        End If
    End Sub
    Private Sub dgProfileGroup_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgProfileGroup.SortCommand
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
        dgProfileGroup.SelectedIndex = -1
        dgProfileGroup.CurrentPageIndex = 0
        BindDataGrid(dgProfileGroup.CurrentPageIndex)
    End Sub
    Private Sub dgProfileGroup_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProfileGroup.PageIndexChanged
        dgProfileGroup.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgProfileGroup.CurrentPageIndex)

    End Sub
#End Region

End Class
