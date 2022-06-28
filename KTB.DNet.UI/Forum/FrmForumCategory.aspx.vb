#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmForumCategory
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ValidateCode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgCategory As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCategory As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlSttaus As System.Web.UI.WebControls.DropDownList
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private objlForumCategory As ForumCategory
    Private arlForumCategory As ArrayList
    Private sHelper As SessionHelper = New SessionHelper
#End Region

#Region "Custom Method"
    Private Sub DeleteForumCategory(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0
        Dim objForumCategory As ForumCategory = New ForumCategoryFacade(User).Retrieve(nID)

        If objForumCategory.Forums.Count > 0 Then
            MessageBox.Show(SR.DeleteFail)
        Else
            Dim objForumCategoryFacade As ForumCategoryFacade = New ForumCategoryFacade(User)
            objForumCategoryFacade.DeleteFromDB(objForumCategory)
        End If
        BindDatagrid(dtgCategory.CurrentPageIndex)
    End Sub
    Private Sub ClearData()
        txtCategory.Text = String.Empty
        ddlSttaus.SelectedIndex = 0
        btnSimpan.Enabled = True
        dtgCategory.SelectedIndex = -1
        ViewState.Add("vsProcess", "Insert")
    End Sub
    Private Function InsertCategory() As Boolean
        Dim objForumCategoryFacade As ForumCategoryFacade = New ForumCategoryFacade(User)
        Dim objForumCategory As ForumCategory = New ForumCategory
        Dim nResult As Integer

        Dim Idedit As Integer
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            Idedit = 0
        ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
            Idedit = CType(sHelper.GetSession("objedit"), ForumCategory).ID
        End If
        Dim codeIsValid As Integer = New ForumCategoryFacade(User).ValidateCode(txtCategory.Text, Idedit)


        If codeIsValid > 0 Then
            MessageBox.Show(SR.DataIsExist("Kategori Forum"))
            Return False
        Else
            objForumCategory.Category = txtCategory.Text.Trim.ToUpper

            objForumCategory.Status = ddlSttaus.SelectedValue

            If CType(ViewState("vsProcess"), String) = "Insert" Then
                nResult = New ForumCategoryFacade(User).Insert(objForumCategory)
            ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
                objForumCategory.ID = Idedit
                nResult = New ForumCategoryFacade(User).Update(objForumCategory)
            End If

            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
                Return False
            Else
                MessageBox.Show(SR.SaveSuccess)
                Return True
            End If
        End If
    End Function
    Private Sub ViewCategory(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objForumCategory As ForumCategory = New ForumCategoryFacade(User).Retrieve(nID)
        txtCategory.Text = objForumCategory.Category
        ddlSttaus.SelectedValue = CType(objForumCategory.Status, Integer)
        btnSimpan.Enabled = EditStatus
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ForumCategoryView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Forum - Kategori")
        End If
    End Sub

    Private cmdButtonPriv As Boolean
    Private Function CekCmdBtnPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ForumCategoryEdit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'ActivateUserPrivilege()
        InitiateAuthorization()
        cmdButtonPriv = CekCmdBtnPrivilege()
        If Not IsPostBack Then
            ClearData()
            sHelper.SetSession("SortColForumCategory", "Category")
            sHelper.SetSession("SortDirectionForumCategory", Sort.SortDirection.ASC)
            BindDatagrid(0)
            'InitiatePage()

            If cmdButtonPriv = False Then
                btnSimpan.Enabled = False
                btnBatal.Enabled = False
            Else
                btnSimpan.Enabled = True
                btnBatal.Enabled = True
            End If
        End If

    End Sub
    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            arlForumCategory = New ForumCategoryFacade(User).RetrieveActiveList(indexPage + 1, dtgCategory.PageSize, totalRow, sHelper.GetSession("SortColForumCategory"), sHelper.GetSession("SortDirectionForumCategory"))
            dtgCategory.DataSource = arlForumCategory
            dtgCategory.VirtualItemCount = totalRow
            dtgCategory.DataBind()
        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim result As Boolean
        result = InsertCategory()
        dtgCategory.CurrentPageIndex = 0
        BindDatagrid(dtgCategory.CurrentPageIndex)
        If result = True Then
            ClearData()
        End If
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtCategory.ReadOnly = False
        'ddlSttaus.Enabled = False
        ClearData()
    End Sub
    Private Sub dtgCategory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCategory.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arlForumCategory Is Nothing) Then
                objlForumCategory = arlForumCategory(e.Item.ItemIndex)
                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgCategory.CurrentPageIndex * dtgCategory.PageSize)

                Dim _lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                _lblStatus.Text = CType(objlForumCategory.Status, enumStatusForumCategory.StatusForumCategory).ToString.Replace("_", " ")

                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                If Not lbtnDelete Is Nothing Then
                    lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If

                Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                _lbtnEdit.CommandArgument = objlForumCategory.ID

                'cek privilege
                If cmdButtonPriv = False Then
                    lbtnDelete.Visible = False
                    _lbtnEdit.Visible = False
                Else
                    lbtnDelete.Visible = True
                    _lbtnEdit.Visible = True
                End If
            End If
        End If
    End Sub
    Private Sub dtgCategory_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCategory.ItemCommand
        If e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            dtgCategory.SelectedIndex = e.Item.ItemIndex
            Dim objForumCategory As ForumCategory = New ForumCategoryFacade(User).Retrieve(CInt(e.CommandArgument))
            sHelper.SetSession("objedit", objForumCategory)

            ddlSttaus.Enabled = True
            ViewCategory(e.Item.Cells(0).Text, True)


        ElseIf e.CommandName = "Delete" Then
            DeleteForumCategory(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub
    Private Sub dtgCategory_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCategory.SortCommand
        If e.SortExpression = sHelper.GetSession("SortColForumCategory") Then
            If sHelper.GetSession("SortDirectionForumCategory") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirectionForumCategory", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirectionForumCategory", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortColForumCategory", e.SortExpression)
        dtgCategory.SelectedIndex = -1
        dtgCategory.CurrentPageIndex = 0
        BindDatagrid(0)
    End Sub
    Private Sub dtgCategory_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCategory.PageIndexChanged
        dtgCategory.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgCategory.CurrentPageIndex)
    End Sub
End Class

