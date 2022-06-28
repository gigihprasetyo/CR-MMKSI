
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmModule
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTemplateFileName As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvDeskripsi As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgModule As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private m_bFormPrivilege As Boolean = False
    Private ListModule As ArrayList
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
        End If

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        If txtName.Text = "" Then
            MessageBox.Show("Nama belum diisi")
            Return
        End If
        txtDeskripsi.ReadOnly = False
        txtTemplateFileName.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertModel()
        Else
            UpdateModel()
            MessageBox.Show("Ubah Sukses")
        End If
        ClearData()
        dtgModule.CurrentPageIndex = 0
        BindDataGrid(dtgModule.CurrentPageIndex)
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchModuleByCriteria()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtName.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        txtTemplateFileName.ReadOnly = False
        ClearData()
    End Sub

    Private Sub dtgModule_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgModule.ItemCommand
        If e.CommandName = "Edit" Then
            txtName.ReadOnly = False
            txtDeskripsi.ReadOnly = False
            txtTemplateFileName.ReadOnly = False
            ViewState.Add("vsProcess", "Edit")
            ViewModel(e.Item.Cells(0).Text, True)
            ''dtgModule.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Try
                txtName.ReadOnly = False
                txtTemplateFileName.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                DeleteModule(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        ElseIf e.CommandName = "Activate" Then
            Try
                Activate(e.Item.Cells(0).Text)
                txtName.ReadOnly = False
                txtTemplateFileName.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                ClearData()
            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        ElseIf e.CommandName = "Deactivate" Then
            Try
                DeActivate(e.Item.Cells(0).Text)

                txtName.ReadOnly = False
                txtTemplateFileName.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                ClearData()
            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        End If
    End Sub

    Private Sub dtgModule_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgModule.ItemDataBound
        Dim RowValue As AssistModule = CType(e.Item.DataItem, AssistModule)
        Dim actLb As LinkButton
        Dim nactLb As LinkButton
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If
        If Not e.Item.FindControl("linkButonActive") Is Nothing Then
            CType(e.Item.FindControl("linkButonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diAktifkan?');")
            actLb = CType(e.Item.FindControl("linkButonActive"), LinkButton)
        End If

        If Not e.Item.FindControl("LinkButtonNonActive") Is Nothing Then
            CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diNonAktifkan?');")
            nactLb = CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton)

            actLb.Visible = False
            nactLb.Visible = False

            If m_bFormPrivilege = True Then
                If RowValue.Status = 0 Then
                    actLb.Visible = True
                Else
                    nactLb.Visible = True
                End If
            End If
        End If

        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = m_bFormPrivilege
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgModule.CurrentPageIndex * dtgModule.PageSize)
        End If

    End Sub

    Private Sub dtgModule_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgModule.SortCommand
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

        dtgModule.SelectedIndex = -1
        dtgModule.CurrentPageIndex = 0
        BindDataGrid(dtgModule.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgModule_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgModule.PageIndexChanged
        dtgModule.SelectedIndex = -1
        dtgModule.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgModule.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        'm_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_Module_Edit_Privilege)
        m_bFormPrivilege = False
        'user tidak usah diberi privilage buat create edit hapus

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_Module_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Master - Module")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Description"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

    End Sub



    Private Sub ClearData()
        txtName.Text = String.Empty
        txtDeskripsi.Text = String.Empty
        txtTemplateFileName.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistModule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If txtName.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.AssistModule), "Name", MatchType.[Partial], txtName.Text.Trim))
            End If
            If txtDeskripsi.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistModule), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
            End If
            If txtTemplateFileName.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistModule), "TemplateFileName", MatchType.[Partial], txtTemplateFileName.Text.Trim))
            End If

            ListModule = New AssistModuleFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgModule.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgModule.DataSource = ListModule
            dtgModule.VirtualItemCount = totRow
            dtgModule.DataBind()
        End If
    End Sub

    Private Sub Activate(ByVal nID As Integer)
        Dim objModule As AssistModule = New AssistModuleFacade(User).Retrieve(nID)
        Dim facade As AssistModuleFacade = New AssistModuleFacade(User)
        objModule.Status = 1
        facade.Update(objModule)
        dtgModule.CurrentPageIndex = 0
        BindDataGrid(dtgModule.CurrentPageIndex)
    End Sub

    Private Sub DeActivate(ByVal nID As Integer)
        Dim objModule As AssistModule = New AssistModuleFacade(User).Retrieve(nID)
        Dim facade As AssistModuleFacade = New AssistModuleFacade(User)
        objModule.Status = 0
        facade.Update(objModule)
        dtgModule.CurrentPageIndex = 0
        BindDataGrid(dtgModule.CurrentPageIndex)
    End Sub

    Private Function InsertModel() As Integer
        Dim objModuleFacade As AssistModuleFacade = New AssistModuleFacade(User)
        Dim objModule As AssistModule = New AssistModule
        Dim nResult As Integer

        If objModuleFacade.ValidateCode(txtName.Text) = 0 Then
            objModule.Name = txtName.Text
            objModule.Description = txtDeskripsi.Text
            objModule.TemplateFileName = txtTemplateFileName.Text
            objModule.Status = 1
            nResult = New AssistModuleFacade(User).Insert(objModule)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Module"))
        End If
        Return nResult
    End Function

    Private Sub UpdateModel()
        Dim objModule As AssistModule = CType(Session.Item("vsModule"), AssistModule)
        objModule.Name = txtName.Text
        objModule.Description = txtDeskripsi.Text
        objModule.TemplateFileName = txtTemplateFileName.Text
        Dim nResult = New AssistModuleFacade(User).Update(objModule)
    End Sub

    Private Sub DeleteModule(ByVal nID As Integer)
        Dim objModule As AssistModule = New AssistModuleFacade(User).Retrieve(nID)

        objModule.RowStatus = CType(DBRowStatus.Deleted, Short)
        Dim facade As AssistModuleFacade = New AssistModuleFacade(User)
        Dim nResult As Integer = facade.Update(objModule)

        If nResult <> -1 Then
            ClearData()
            dtgModule.CurrentPageIndex = 0
            BindDataGrid(dtgModule.CurrentPageIndex)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objModule As AssistModule = New AssistModuleFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsModule", objModule)
        If IsNothing(objModule) Then
            txtDeskripsi.Text = ""
            txtName.Text = ""
            txtTemplateFileName.Text = ""
        Else
            txtName.Text = objModule.Name
            txtDeskripsi.Text = objModule.Description
            txtTemplateFileName.Text = objModule.TemplateFileName
        End If

        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub SearchModuleByCriteria()
        dtgModule.CurrentPageIndex = 0
        BindDataGrid(dtgModule.CurrentPageIndex)
    End Sub
#End Region

End Class
