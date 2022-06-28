Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security

Namespace KTB.DNet.UI.SparePart

    Public Class FrmManagePRPCategory
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents txtCategoryName As System.Web.UI.WebControls.TextBox
        Protected WithEvents dtgPRPCategory As System.Web.UI.WebControls.DataGrid
        Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
        Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
        Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
        Protected WithEvents rfv1 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents rfv3 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents rfv2 As System.Web.UI.WebControls.RequiredFieldValidator
        Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object
        Private bPrivilegeMngPRPCat As Boolean = False

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Dim sHPRPCategory As SessionHelper = New SessionHelper

#Region "Form's Event"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            ActivateUserPrivilege()
            If Not IsPostBack Then
                InitiatePage()
                BindDDL()
                BindDataGrid(0)
            End If
        End Sub

        Private Sub BindDDL()
            ddlStatus.DataSource = New EnumDNET().RetrievePRP
            ddlStatus.DataTextField = "NameType"
            ddlStatus.DataValueField = "ValType"
            ddlStatus.DataBind()
        End Sub

        Private Sub SetControlPrivilege()
            btnSimpan.Visible = bPrivilegeMngPRPCat
            btnBatal.Visible = bPrivilegeMngPRPCat
        End Sub

        Private Sub ActivateUserPrivilege()
            bPrivilegeMngPRPCat = SecurityProvider.Authorize(Context.User, SR.SaveCategoryPRP_Privilege)

            If Not SecurityProvider.Authorize(Context.User, SR.ViewCategoryPRP_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Parts - Form Pengelolaan Kategori PRP")
            End If
        End Sub
        Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
            If Not IsFormValid() Then
                Return
            End If

            If Not IsUnhack() Then
                MessageBox.Show("< dan > bukan karakter valid")
                Return
            End If

            Dim nResult As Integer = 0

            If ViewState("vsProcess") = "Insert" Then
                nResult = InsertPRPCategory()
            ElseIf ViewState("vsProcess") = "Update" Then
                nResult = UpdatePRPCategory()
            End If

            If nResult <> -1 Then
                ClearData()
                dtgPRPCategory.CurrentPageIndex = 0
                BindDataGrid(dtgPRPCategory.CurrentPageIndex)
            End If
        End Sub

        Private Sub dtgPRPCategory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPRPCategory.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then

                If Not e.Item.FindControl("lnkDelete") Is Nothing Then
                    CType(e.Item.FindControl("lnkDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = CStr(e.Item.ItemIndex + 1 + (dtgPRPCategory.CurrentPageIndex * dtgPRPCategory.PageSize))

                Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

                If Not e.Item.DataItem Is Nothing Then
                    e.Item.DataItem.GetType().ToString()
                    Dim RowValue As PRPCategory = CType(e.Item.DataItem, PRPCategory)

                    Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                    Dim lnkEdit As LinkButton = CType(e.Item.FindControl("lnkEdit"), LinkButton)

                    If RowValue.Status = 0 Then
                        lblStatus.Text = "Aktif"
                    ElseIf RowValue.Status = 1 Then
                        lblStatus.Text = "Tidak Aktif"
                    End If

                End If
            End If

            ActivateUserPrivilege()
            dtgPRPCategory.Columns(5).Visible = bPrivilegeMngPRPCat

        End Sub

        Private Sub dtgPRPCategory_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPRPCategory.ItemCommand
            If e.CommandName = "Edit" Then
                dtgPRPCategory.SelectedIndex = e.Item.ItemIndex
                Dim objPRPCategory As PRPCategory = New PRPCategoryFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                ViewState("vsProcess") = "Update"
                sHPRPCategory.SetSession("objPRPCategory", objPRPCategory)
                FillFormFromObject(objPRPCategory)
            ElseIf e.CommandName = "Delete" Then
                Dim objPRPCategory As PRPCategory = New PRPCategoryFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                If HasPRP(objPRPCategory) Then
                    MessageBox.Show("Masih ada PRP yang menggunakan kategori ini")
                    Return
                End If
                Try
                    DeletePRPCategory(objPRPCategory)
                    MessageBox.Show(SR.DeleteSucces)
                Catch ex As Exception
                    MessageBox.Show(SR.DeleteFail)
                End Try
                ClearData()
                dtgPRPCategory.CurrentPageIndex = 0
                BindDataGrid(dtgPRPCategory.CurrentPageIndex)
            End If
        End Sub

        Private Sub dtgPRPCategory_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPRPCategory.PageIndexChanged
            dtgPRPCategory.SelectedIndex = -1
            dtgPRPCategory.CurrentPageIndex = e.NewPageIndex
            BindDataGrid(dtgPRPCategory.CurrentPageIndex)
        End Sub

        Private Sub dtgPRPCategory_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPRPCategory.SortCommand
            If CType(ViewState("vsSortColumn"), String) = e.SortExpression Then
                Select Case CType(ViewState("vsSortDirect"), Sort.SortDirection)

                    Case Sort.SortDirection.ASC
                        ViewState("vsSortDirect") = Sort.SortDirection.DESC

                    Case Sort.SortDirection.DESC
                        ViewState("vsSortDirect") = Sort.SortDirection.ASC
                End Select
            Else
                ViewState("vsSortColumn") = e.SortExpression
                ViewState("vsSortDirect") = Sort.SortDirection.ASC
            End If

            dtgPRPCategory.SelectedIndex = -1
            dtgPRPCategory.CurrentPageIndex = 0
            BindDataGrid(dtgPRPCategory.CurrentPageIndex)
            ClearData()
        End Sub

        Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
            ClearData()
        End Sub

#End Region

#Region "Private Method"

        Private Sub InitiatePage()
            ClearData()
            SetControlPrivilege()
            ViewState("vsSortColumn") = "CategoryName"
            ViewState("vsSortDirect") = Sort.SortDirection.ASC
        End Sub

        Private Sub BindDataGrid(ByVal indexPage As Integer)
            Dim totalRow As Integer = 0

            If (indexPage >= 0) Then
                dtgPRPCategory.DataSource = New PRPCategoryFacade(User).RetrieveList(indexPage + 1, dtgPRPCategory.PageSize, totalRow, CType(ViewState("vsSortColumn"), String), _
                  CType(ViewState("vsSortDirect"), Sort.SortDirection))
                dtgPRPCategory.VirtualItemCount = totalRow
                bPrivilegeMngPRPCat = SecurityProvider.Authorize(Context.User, SR.SaveCategoryPRP_Privilege)
                dtgPRPCategory.DataBind()
            End If
        End Sub

        Private Sub ClearData()
            txtCategoryName.Text = ""
            txtDescription.Text = ""
            ddlStatus.SelectedIndex = 0

            ViewState.Add("vsProcess", "Insert")
            dtgPRPCategory.SelectedIndex = -1
        End Sub

        Private Function InsertPRPCategory() As Integer
            Dim objPRPCategory As PRPCategory = New PRPCategory
            Dim objPRPCategoryFacade As PRPCategoryFacade = New PRPCategoryFacade(User)
            Dim nResult = -1

            Try
                FillingObjectFromForm(objPRPCategory)

                If isNameExist(objPRPCategory.CategoryName) Then
                    MessageBox.Show(SR.DataIsExist("Nama Kategori"))
                Else
                    nResult = objPRPCategoryFacade.Insert(objPRPCategory)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                End If
            Catch ex As InvalidConstraintException
                MessageBox.Show(ex.Message)
            Catch ex As DataException
                MessageBox.Show(ex.Message)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Return nResult
        End Function

        Private Function UpdatePRPCategory() As Integer
            Dim objPRPCategory As PRPCategory = sHPRPCategory.GetSession("objPRPCategory")
            Dim objPRPCategoryFacade As PRPCategoryFacade = New PRPCategoryFacade(User)
            Dim nResult = -1

            Try
                FillingObjectFromForm(objPRPCategory)

                nResult = objPRPCategoryFacade.Update(objPRPCategory)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                End If
            Catch ex As InvalidConstraintException
                MessageBox.Show(ex.Message)
            Catch ex As DataException
                MessageBox.Show(ex.Message)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Return nResult
        End Function

        Private Sub FillingObjectFromForm(ByVal inObj As PRPCategory)
            inObj.CategoryName = txtCategoryName.Text
            inObj.Description = txtDescription.Text
            inObj.Status = ddlStatus.SelectedValue
        End Sub

        Private Sub FillFormFromObject(ByVal inObj As PRPCategory)
            txtCategoryName.Text = inObj.CategoryName
            txtDescription.Text = inObj.Description
            ddlStatus.SelectedIndex = inObj.Status
        End Sub

        Private Function IsFormValid() As Boolean
            If txtCategoryName.Text = "" Then
                Return False
            End If

            If txtDescription.Text = "" Then
                Return False
            End If

            If Not Page.IsValid Then
                Return False
            End If

            Return True
        End Function

        Private Function IsUnhack() As Boolean
            If txtCategoryName.Text.IndexOf("<") >= 0 Or txtCategoryName.Text.IndexOf(">") >= 0 Or txtCategoryName.Text.IndexOf("'") >= 0 Then
                Return False
            End If

            If txtDescription.Text.IndexOf("<") >= 0 Or txtDescription.Text.IndexOf(">") >= 0 Or txtDescription.Text.IndexOf("'") >= 0 Then
                Return False
            End If

            Return True
        End Function

        Private Function isNameExist(ByVal name As String) As Boolean
            Dim objFacade As PRPCategoryFacade = New PRPCategoryFacade(User)

            Try
                If objFacade.ValidateName(name) = 0 Then
                    Return False
                End If
            Catch
                Throw
            End Try

            Return True
        End Function

        Private Function HasPRP(ByVal PRPCat As PRPCategory) As Boolean
            Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPFile), "PRPCategory.ID", MatchType.Exact, PRPCat.ID))
            Dim PRPs As ArrayList = New PRPFileFacade(User).Retrieve(critComp)
            Return PRPs.Count > 0
        End Function

        Private Sub DeletePRPCategory(ByVal PRPCat As PRPCategory)
            Try
                Dim objFacade As PRPCategoryFacade = New PRPCategoryFacade(User)
                If objFacade.DeleteFromDB(PRPCat) < 0 Then
                    Throw New Exception(SR.DeleteFail)
                End If
            Catch
                Throw
            End Try
        End Sub
#End Region

    End Class

End Namespace
