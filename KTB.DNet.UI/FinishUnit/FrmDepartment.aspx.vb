Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security

Public Class FrmDepartment
    Inherits System.Web.UI.Page
    Dim sHDealGroup As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgGroup As System.Web.UI.WebControls.DataGrid
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtGroupCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGroupName As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents txtDept As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtPrev As System.Web.UI.WebControls.TextBox
    Private bPrivilegeChangeGroup As Boolean = False
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        InitializeComponent()
    End Sub
    'CODEGEN: This method call is required by the Web Form Designer
    'Do not modify it using the code editor.


#End Region

#Region "Cek Privilege"
    Dim depPriv As Boolean = SecurityProvider.Authorize(context.User, SR.DepartmentEdit_Privilege)

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.DepartmentView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MAINTENANCE - Departemen")
        End If
    End Sub

    Private Sub ActivateControlPrivilege()
        btnSimpan.Enabled = depPriv
        btnBatal.Enabled = depPriv
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'ActivateUserPrivilege()
        InitiateAuthorization()
        ActivateControlPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub SetControlPrivilege()
        'btnSimpan.Visible = bPrivilegeChangeGroup
        'btnBatal.Visible = bPrivilegeChangeGroup
    End Sub

    Private Sub ActivateUserPrivilege()
        'bPrivilegeChangeGroup = SecurityProvider.Authorize(Context.User, SR.ChangeGroup_Privilege)

        'If Not SecurityProvider.Authorize(Context.User, SR.ViewGroup_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Group")
        'End If
    End Sub

    Private Sub InitiatePage()
        ClearData()
        'SetControlPrivilege()
        'ActivateControlPrivilege()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgGroup.DataSource = New DepartmentFacade(User).RetrieveActiveList(indexPage + 1, dtgGroup.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgGroup.VirtualItemCount = totalRow
            'bPrivilegeChangeGroup = SecurityProvider.Authorize(Context.User, SR.ChangeGroup_Privilege)
            dtgGroup.DataBind()
        End If

    End Sub

    Private Sub ClearData()
        Me.txtDept.ReadOnly = False
        Me.txtDesc.ReadOnly = False
        Me.txtPrev.ReadOnly = False
        Me.txtDesc.Text = String.Empty
        Me.txtDept.Text = String.Empty
        Me.txtPrev.Text = String.Empty
        'btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        dtgGroup.SelectedIndex = -1
    End Sub


    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objDepartment As Department = New Department
        Dim objDepartmentFacade As DepartmentFacade = New DepartmentFacade(User)
        Dim nResult = -1
        Me.txtDept.Enabled = True
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If objDepartmentFacade.ValidateCode(Me.txtDept.Text) = 0 Then
                objDepartment.Code = Me.txtDept.Text
                objDepartment.Description = Me.txtDesc.Text
                objDepartment.Privilege = Me.txtPrev.Text
                nResult = New DepartmentFacade(User).Insert(objDepartment)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else

                    MessageBox.Show(SR.SaveSuccess)
                End If
            Else
                MessageBox.Show(SR.DataIsExist("Department"))
            End If
        Else
            UpdateDepartment()
        End If

        ClearData()
        dtgGroup.CurrentPageIndex = 0
        BindDataGrid(dtgGroup.CurrentPageIndex)
    End Sub


    Private Sub dtgGroup_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgGroup.ItemDataBound
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgGroup.CurrentPageIndex * dtgGroup.PageSize)
        End If

        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = depPriv
        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = depPriv
        End If

    End Sub

    Private Sub UpdateDepartment()
        Dim objDepartment As Department = CType(sHDealGroup.GetSession("vsDepartment"), Department)
        objDepartment.Description = Me.txtDesc.Text
        objDepartment.Privilege = Me.txtPrev.Text
        objDepartment.Code = Me.txtDept.Text
        Try
            Dim nResult = New DepartmentFacade(User).Update(objDepartment)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try

    End Sub

    Private Sub dtgGroup_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgGroup.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewDeaprtment(e.Item.Cells(0).Text, False)
            Me.txtDept.ReadOnly = True
            Me.txtDesc.ReadOnly = True
            Me.txtPrev.ReadOnly = True
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewDeaprtment(e.Item.Cells(0).Text, True)
            dtgGroup.SelectedIndex = e.Item.ItemIndex
            Me.txtDept.ReadOnly = True
            Me.txtDesc.ReadOnly = False
            Me.txtPrev.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeleteDepartment(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
    ByVal DepartmentID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "Department", MatchType.Exact, DepartmentID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteDepartment(ByVal nID As Integer)
        Dim objDepartment As Department = New DepartmentFacade(User).Retrieve(nID)
        If Not objDepartment Is Nothing Then
            Dim facade As DepartmentFacade = New DepartmentFacade(User)

            If objDepartment.KindOfLetters.Count > 0 Or objDepartment.Letters.Count > 0 Then
                MessageBox.Show("Data Sudah digunakan dalam transaksi.")
            Else
                facade.DeleteFromDB(objDepartment)
                dtgGroup.CurrentPageIndex = 0
                BindDataGrid(dtgGroup.CurrentPageIndex)
                MessageBox.Show(SR.DeleteSucces)
            End If
        Else
            MessageBox.Show(SR.DeleteFail)
        End If


    End Sub



    Private Sub ViewDeaprtment(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objDepartment As Department = New DepartmentFacade(User).Retrieve(nID)
        If Not objDepartment Is Nothing Then
            sHDealGroup.SetSession("vsDepartment", objDepartment)
            Me.txtDept.Text = objDepartment.Code
            Me.txtDesc.Text = objDepartment.Description
            Me.txtPrev.Text = objDepartment.Privilege
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If

    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgGroup_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgGroup.SortCommand
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

        dtgGroup.SelectedIndex = -1
        dtgGroup.CurrentPageIndex = 0
        BindDataGrid(dtgGroup.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgGroup_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgGroup.PageIndexChanged
        dtgGroup.SelectedIndex = -1
        dtgGroup.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgGroup.CurrentPageIndex)
        ClearData()
    End Sub
End Class
