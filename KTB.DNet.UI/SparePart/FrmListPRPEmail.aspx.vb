#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmListPRPEmail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEmail As System.Web.UI.WebControls.DataGrid
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

#Region "Private Variable"
    Private sHPRP As SessionHelper = New SessionHelper
#End Region

#Region "Event Method"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchByUserName(txtUserName.Text.Trim)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Simpan()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Batal()
    End Sub

    Private Sub dtgEmail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEmail.ItemDataBound
        DataGridBoundItem(e)
    End Sub

    Private Sub dtgEmail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEmail.PageIndexChanged
        dtgEmail.CurrentPageIndex = e.NewPageIndex
        dtgEmail.SelectedIndex = -1
        BindDataGrid(dtgEmail.CurrentPageIndex)
    End Sub

    Private Sub dtgEmail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEmail.SortCommand
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

        dtgEmail.SelectedIndex = -1
        dtgEmail.CurrentPageIndex = 0
        BindDataGrid(dtgEmail.CurrentPageIndex)
        ClearPage()
    End Sub

    Private Sub dtgEmail_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgEmail.SelectedIndexChanged
        DataGridIndexChange()
    End Sub

    Private Sub dtgEmail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEmail.ItemCommand
        DataGridItemCommand(e)
    End Sub

#End Region

#Region "Custom Method"

    Private Sub SearchByUserName(ByVal userName As String)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PRPUserEmail), "UserName", MatchType.[Partial], userName))
        Dim totalRow As Integer = 0
        Dim sorts As SortCollection = New SortCollection
        sorts.Add(New Sort(GetType(PRPUserEmail), ViewState("vsSortColumn"), ViewState("vsSortDirect")))
        Dim objPRPUserEmails As ArrayList = New PRPUserEmailFacade(User).RetrieveByCriteria(crit, sorts, 0, dtgEmail.PageSize, totalRow)
        dtgEmail.DataSource = objPRPUserEmails
        dtgEmail.VirtualItemCount = totalRow
        dtgEmail.DataBind()
    End Sub

    Private Sub InitiatePage()
        ClearPage()
        ViewState("vsSortColumn") = "UserName"
        ViewState("vsSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewEmailReceiverPRP_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PARTSHOP REWARD PROGRAM - Email")
        End If
    End Sub

    Private Sub ClearPage()
        Me.txtUserName.ReadOnly = False
        Me.txtEmail.ReadOnly = False
        Me.ddlTipe.Enabled = True
        Me.txtUserName.Text = String.Empty
        Me.txtEmail.Text = String.Empty

        Me.ddlTipe.SelectedIndex = 0
        If dtgEmail.Items.Count > 0 Then
            dtgEmail.SelectedIndex = -1
        End If
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(PRPUserEmail), "UserName", MatchType.[Partial], txtUserName.Text.Trim))

            Dim sorts As SortCollection = New SortCollection
            sorts.Add(New Sort(GetType(PRPUserEmail), ViewState("vsSortColumn"), ViewState("vsSortDirect")))
            Dim objPRPUserEmails As ArrayList = New PRPUserEmailFacade(User).RetrieveByCriteria(crit, sorts, indexPage + 1, dtgEmail.PageSize, totalRow)

            dtgEmail.DataSource = objPRPUserEmails
            dtgEmail.VirtualItemCount = totalRow
            dtgEmail.DataBind()
        End If
    End Sub

    Private Sub Simpan()
        If ViewState("vsProcess") = "Insert" Then
            Insert()
        ElseIf ViewState("vsProcess") = "Edit" Then
            Update()
        End If
    End Sub

    Private Sub Insert()
        If Not Page.IsValid Then
            Return
        End If
        If IsPageValid() Then
            'If IsNameExist(txtUserName.Text) Then
            '    MessageBox.Show(SR.DataIsExist("Nama User"))
            '    Return
            'End If

            Dim obj As PRPUserEmail = New PRPUserEmail
            obj.UserName = txtUserName.Text
            FillObjectFromForm(obj)

            Dim nResult As Integer
            Try
                nResult = New PRPUserEmailFacade(User).Insert(obj)
                If nResult <= -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                    ClearPage()
                    dtgEmail.CurrentPageIndex = 0
                    dtgEmail.SelectedIndex = -1
                    BindDataGrid(dtgEmail.CurrentPageIndex)
                End If
            Catch
                MessageBox.Show(SR.SaveFail)
            End Try
        End If
    End Sub

    Private Sub Update()
        If Not Page.IsValid Then
            Return
        End If
        If IsPageValid() Then
            'If Not IsNameExist(txtUserName.Text) Then
            '    MessageBox.Show(SR.DataNotFound("Nama User"))
            '    Return
            'End If

            Dim obj As PRPUserEmail = sHPRP.GetSession("objPRPUserEmail")
            FillObjectFromForm(obj)

            Dim nResult As Integer
            Try
                nResult = New PRPUserEmailFacade(User).Update(obj)
                If nResult <= -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                    ClearPage()
                    dtgEmail.CurrentPageIndex = 0
                    dtgEmail.SelectedIndex = -1
                    BindDataGrid(dtgEmail.CurrentPageIndex)
                End If
            Catch
                MessageBox.Show(SR.SaveFail)
            End Try
        End If
    End Sub

    Private Sub LoadFormFrom(ByVal obj As PRPUserEmail)
        If Not IsNothing(obj) Then
            txtUserName.Text = obj.UserName
            txtEmail.Text = obj.Email
            Dim lItem As ListItem = ddlTipe.Items.FindByText(obj.Tipe)
            If Not IsNothing(lItem) Then
                ddlTipe.SelectedIndex = ddlTipe.Items.IndexOf(lItem)
            End If
        End If
    End Sub

    Private Sub FillObjectFromForm(ByVal obj As PRPUserEmail)
        obj.Email = txtEmail.Text
        obj.Tipe = ddlTipe.SelectedItem.Text
    End Sub

    Private Function IsPageValid() As Boolean
        If txtEmail.Text = "" Then
            MessageBox.Show("Email masih kosong")
            Return False
        End If
        If txtUserName.Text = "" Then
            MessageBox.Show("Nama masih kosong")
            Return False
        End If
        If txtEmail.Text.IndexOf("<") >= 0 Or txtEmail.Text.IndexOf(">") >= 0 _
        Or txtUserName.Text.IndexOf("<") >= 0 Or txtUserName.Text.IndexOf(">") >= 0 Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return False
        End If
        Return True
    End Function

    Private Function IsNameExist(ByVal name As String) As Boolean
        Return New PRPUserEmailFacade(User).ValidateName(name) > 0
    End Function

    Private Sub Batal()
        ClearPage()
        dtgEmail.SelectedIndex = -1
    End Sub

    Private Sub DataGridBoundItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If Not e.Item.DataItem Is Nothing Then
                e.Item.DataItem.GetType().ToString()
                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Or ItemType = ListItemType.SelectedItem Then
                    CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgEmail.CurrentPageIndex * dtgEmail.PageSize)
                End If

                Dim LinkHapus As LinkButton = CType(e.Item.FindControl("btnHapus"), LinkButton)
                LinkHapus.Visible = SecurityProvider.Authorize(Context.User, SR.DeletePartIncidentalEmail_Privilege)
                If Not LinkHapus Is Nothing Then
                    CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If

            End If
        End If
    End Sub

    Private Sub DataGridIndexChange()
        BindDataGrid(dtgEmail.CurrentPageIndex)
        ClearPage()
    End Sub

    Private Sub DataGridItemCommand(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewData(CInt(e.Item.Cells(0).Text), False)
            Me.ddlTipe.Enabled = False
            Me.txtEmail.ReadOnly = True
            Me.txtUserName.ReadOnly = True
            dtgEmail.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewData(CInt(e.Item.Cells(0).Text), True)
            dtgEmail.SelectedIndex = e.Item.ItemIndex
            Me.ddlTipe.Enabled = True
            Me.txtEmail.ReadOnly = False
            Me.txtUserName.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeleteData(CInt(e.Item.Cells(0).Text))
        End If
    End Sub

    Private Sub ViewData(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objPRPUserEmail As PRPUserEmail = New PRPUserEmailFacade(User).Retrieve(nID)
        sHPRP.SetSession("objPRPUserEmail", objPRPUserEmail)

        Me.ddlTipe.SelectedValue = objPRPUserEmail.Tipe
        Me.txtEmail.Text = objPRPUserEmail.Email
        Me.txtUserName.Text = objPRPUserEmail.UserName
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub DeleteData(ByVal nID As Integer)
        With New PRPUserEmailFacade(User)
            Dim objPRPUserEmail As PRPUserEmail = .Retrieve(nID)
            '.DeleteFromDB(objPRPUserEmail)
            objPRPUserEmail.RowStatus = -1
            .Update(objPRPUserEmail)
            dtgEmail.CurrentPageIndex = 0
            BindDataGrid(dtgEmail.CurrentPageIndex)
            ClearPage()
        End With
    End Sub

#End Region

End Class
