Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Public Class FrmKindOfLetter
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlDepartemen As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgKindOfLetter As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim _sessHelper As New SessionHelper

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.SalesKindLetterView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Jenis Surat")
        End If
    End Sub

    Private Function CheckCmdButtonPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.SaleskindLetterEdit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack() Then
            ViewState("CurrentSortColumn") = "Description"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Viewstate.Add("vsproses", "Insert")
            BindDataGrid(0)
            BindDDL()
            If CheckCmdButtonPriv() = False Then
                btnSave.Enabled = False
            Else
                btnSave.Enabled = True
            End If
        End If
    End Sub
    Public Sub BindDataGrid(ByVal idx As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KindOfLetter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _sessHelper.SetSession("SortViewLetter", criterias)
        arrList = New KindOfLetterFacade(User).RetrieveByCriteria(criterias, idx + 1, dgKindOfLetter.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgKindOfLetter.DataSource = arrList
        dgKindOfLetter.VirtualItemCount = totalRow
        dgKindOfLetter.DataBind()
    End Sub
    Private Sub BindDDL()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Department), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim _arr As ArrayList = New DepartmentFacade(User).RetrieveByCriteria(criterias)
        'remark by Ery
        'refer bug 1064
        'For Each item As Department In _arr
        '    Dim _list As New ListItem(item.Description, item.Code)
        '    ddlDepartemen.Items.Add(_list)
        'Next
        For Each item As Department In _arr
            Dim _list As New ListItem(item.Code, item.Code)
            ddlDepartemen.Items.Add(_list)
        Next
        Dim _lists As New ListItem("Pilih Departement", "Invalid")
        _lists.Selected = True
        ddlDepartemen.Items.Insert(0, _lists)
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim objKindOfLetter As New KindOfLetter

        If Not CType(ViewState("vsproses"), String) = "Insert" Then
            objKindOfLetter = CType(Session.Item("vsKindOfLetter"), KindOfLetter)
        End If
        Dim n As Integer = -1
        If CType(ViewState("vsproses"), String) = "Insert" Then
            If CheckValidation(txtCode.Text, objKindOfLetter) = True Then
                objKindOfLetter.Description = txtDeskripsi.Text
                objKindOfLetter.Department = New DepartmentFacade(User).Retrieve(ddlDepartemen.SelectedValue)
                objKindOfLetter.Code = txtCode.Text
                n = New KindOfLetterFacade(User).Insert(objKindOfLetter)
                If n >= 0 Then
                    MessageBox.Show("Simpan sukses")
                    txtCode.Enabled = True
                    ClearData()
                    dgKindOfLetter.SelectedIndex = -1
                    BindDataGrid(dgKindOfLetter.CurrentPageIndex)
                Else
                    MessageBox.Show("Simpan gagal")
                End If
            End If
        Else
            objKindOfLetter.Description = txtDeskripsi.Text
            objKindOfLetter.Department = New DepartmentFacade(User).Retrieve(ddlDepartemen.SelectedValue)
            'objKindOfLetter.Code = txtCode.Text
            n = New KindOfLetterFacade(User).Update(objKindOfLetter)
            If n >= 0 Then
                txtCode.Enabled = True
                MessageBox.Show("Update data sukses")
                ClearData()
                dgKindOfLetter.SelectedIndex = -1
                BindDataGrid(dgKindOfLetter.CurrentPageIndex)
            Else
                MessageBox.Show("Update data gagal")
            End If
            Viewstate.Add("vsproses", "Insert")
        End If
        'txtCode.Text = txtCode.Text.Replace("'", "")
        'If CheckValidation(txtCode.Text, objKindOfLetter) = True Then
        '    Dim n As Integer = -1
        '    If CType(ViewState("vsproses"), String) = "Insert" Then
        '        objKindOfLetter.Description = txtDeskripsi.Text
        '        objKindOfLetter.Department = New DepartmentFacade(User).Retrieve(ddlDepartemen.SelectedValue)
        '        objKindOfLetter.Code = txtCode.Text
        '        n = New KindOfLetterFacade(User).Insert(objKindOfLetter)
        '    Else
        '        objKindOfLetter.Description = txtDeskripsi.Text
        '        objKindOfLetter.Department = New DepartmentFacade(User).Retrieve(ddlDepartemen.SelectedValue)
        '        objKindOfLetter.Code = txtCode.Text
        '        n = New KindOfLetterFacade(User).Update(objKindOfLetter)
        '        Viewstate.Add("vsproses", "Insert")
        '    End If
        '    If n >= 0 Then
        '        MessageBox.Show("Simpan sukses")
        '        BindDataGrid(dgKindOfLetter.CurrentPageIndex)
        '    Else
        '        MessageBox.Show("Simpan gagal")
        '    End If

        'End If

    End Sub

    Private Sub dgKindOfLetter_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgKindOfLetter.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                ViewState.Add("vsproses", "Edit")
                setControlUpdate(Integer.Parse(e.Item.Cells(0).Text))
            Case "Delete"
                deleteData(Integer.Parse(e.Item.Cells(0).Text))
        End Select
    End Sub
    Private Sub setControlUpdate(ByVal id As Integer)
        Dim objKindOfLetter As KindOfLetter = New KindOfLetterFacade(User).Retrieve(id)
        txtCode.Text = objKindOfLetter.Code
        txtCode.Enabled = False
        txtDeskripsi.Text = objKindOfLetter.Description
        ddlDepartemen.SelectedValue = objKindOfLetter.Department.Code
        'Todo session
        Session.Add("vsKindOfLetter", objKindOfLetter)
    End Sub
    Private Sub deleteData(ByVal id As Integer)
        Try
            Dim objKindOfLetterFacade As New KindOfLetterFacade(User)
            Dim objKindOfLetter As KindOfLetter = objKindOfLetterFacade.Retrieve(id)
            If objKindOfLetter.Letters.Count > 0 Then
                MessageBox.Show("Data telah di Referensi dalam transaksi.")
            Else
                objKindOfLetterFacade.DeleteFromDB(objKindOfLetter)
                BindDataGrid(dgKindOfLetter.CurrentPageIndex)
            End If
        Catch
            MessageBox.Show(SR.DeleteFail)
        End Try
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dgKindOfLetter.DataSource = New KindOfLetterFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("sortViewLetter"), CriteriaComposite), indexPage + 1, dgKindOfLetter.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgKindOfLetter.VirtualItemCount = totalRow
            dgKindOfLetter.DataBind()
        End If

    End Sub

    Private Sub dgKindOfLetter_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgKindOfLetter.PageIndexChanged
        dgKindOfLetter.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgKindOfLetter.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dgKindOfLetter_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgKindOfLetter.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgKindOfLetter.SelectedIndex = -1
        dgKindOfLetter.CurrentPageIndex = 0
        bindGridSorting(dgKindOfLetter.CurrentPageIndex)
    End Sub
    Private Sub ClearData()
        txtDeskripsi.Text = String.Empty
        txtCode.Text = String.Empty
        txtCode.Enabled = True
        ddlDepartemen.SelectedValue = "Invalid"
    End Sub
    Private Function CheckValidation(ByVal Code As String, ByVal obj As KindOfLetter) As Boolean
        Dim bcheck As Boolean = True
        If txtCode.Text.Trim = String.Empty Then
            bcheck = False
            MessageBox.Show("Kode surat masih kosong")
        End If
        If txtDeskripsi.Text.Trim = String.Empty Then
            bcheck = False
            MessageBox.Show("Deskripsi surat masih kosong")
        End If
        If ddlDepartemen.SelectedValue = "Invalid" Then
            bcheck = False
            MessageBox.Show("Departemen surat masih belum dipilih")
        End If
        Dim _arr As ArrayList = New KindOfLetterFacade(User).Retrieve(Code, obj)
        If _arr.Count > 0 Then
            bcheck = False
            MessageBox.Show("Kode surat sudah ada")
        End If
        Return bcheck
    End Function

    Private Sub dgKindOfLetter_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgKindOfLetter.ItemDataBound
        Dim objKindOfLetter As KindOfLetter = e.Item.DataItem
        If Not e.Item.DataItem Is Nothing Then
            Dim lblCreatedTime As Label = CType(e.Item.FindControl("lblCreatedTime"), Label)
            lblCreatedTime.Text = objKindOfLetter.CreatedTime.ToShortDateString

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgKindOfLetter.CurrentPageIndex * dgKindOfLetter.PageSize)

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            If CheckCmdButtonPriv() = False Then
                lbtnEdit.Visible = False
                lbtnDelete.Visible = False
            Else
                lbtnEdit.Visible = True
                lbtnDelete.Visible = True
            End If

        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        ClearData()
        Viewstate.Add("vsproses", "Insert")
    End Sub
End Class
