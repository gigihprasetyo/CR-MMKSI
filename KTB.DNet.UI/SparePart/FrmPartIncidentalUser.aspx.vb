#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmPartIncidentalUser
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents dtgPIUser As System.Web.UI.WebControls.DataGrid
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtPositionCC As System.Web.UI.WebControls.TextBox

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
    Private _SessionHelper As SessionHelper = New SessionHelper
    Private _arrPIUser As ArrayList
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "UserName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    'Private Sub BindToDDL()
    '    ddlCourseId.DataSource = New TrCourseFacade(User).RetrieveList("CourseCode", Sort.SortDirection.ASC)
    '    ddlCourseId.DataTextField = "CourseCode"
    '    ddlCourseId.DataValueField = "ID"
    '    ddlCourseId.DataBind()

    '    ddlPreReqCourID.DataSource = New TrCourseFacade(User).RetrieveList("CourseCode", Sort.SortDirection.ASC)
    '    ddlPreReqCourID.DataTextField = "CourseCode"
    '    ddlPreReqCourID.DataValueField = "ID"
    '    ddlPreReqCourID.DataBind()

    '    'insert empty value in first row
    '    ddlCourseId.Items.Insert(0, New ListItem("", ""))
    '    ddlPreReqCourID.Items.Insert(0, New ListItem("", ""))
    'End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            'get all data from course to fill prerequirecode base on coursecode
            _arrPIUser = New PartIncidentalUserFacade(User).RetrieveList()
            _SessionHelper.SetSession("objPIUser", _arrPIUser)
            'get prerequire data
            dtgPIUser.DataSource = New PartIncidentalUserFacade(User).RetrieveActiveList(indexPage + 1, dtgPIUser.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgPIUser.VirtualItemCount = totalRow
            dtgPIUser.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        Me.txtUserName.ReadOnly = False
        Me.txtEmail.ReadOnly = False
        Me.ddlTipe.Enabled = True
        Me.txtUserName.Text = String.Empty
        Me.txtEmail.Text = String.Empty

        Me.ddlTipe.SelectedIndex = 0
        txtPositionCC.Text = ""
        'ddlPositionCC.SelectedIndex = 0
        If dtgPIUser.Items.Count > 0 Then
            dtgPIUser.SelectedIndex = -1
        End If
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    'Private Function IsExistGroup(ByVal _courseid As Integer, ByVal _prerequireid As Integer) As Boolean
    '    Dim objPreRequireFacade As TrPreRequireFacade = New TrPreRequireFacade(User)
    '    If objPreRequireFacade.ValidateCode(_courseid, _prerequireid) > 0 Then
    '        MessageBox.Show(SR.DataIsExist("Kode Course"))
    '        Return True
    '    End If
    '    Return False
    'End Function

    Private Function InsertGroup() As Integer
        Dim objPartIncidentalUser As PartIncidentalUser = New PartIncidentalUser
        Dim nResult As Integer = -1
        'If Not IsExistGroup(CInt(ddlCourseId.SelectedValue), CInt(ddlPreReqCourID.SelectedValue)) Then
        objPartIncidentalUser.UserName = Me.txtUserName.Text
        objPartIncidentalUser.Tipe = Me.ddlTipe.SelectedValue
        objPartIncidentalUser.Email = Me.txtEmail.Text
        objPartIncidentalUser.PositionCC = txtPositionCC.Text 'ddlPositionCC.SelectedValue
        nResult = New PartIncidentalUserFacade(User).Insert(objPartIncidentalUser)
        'Else
        'End If
        Return nResult
    End Function

    Private Function UpdateGroup() As Integer
        Dim objPartIncidentalUser As PartIncidentalUser = CType(_SessionHelper.GetSession("vsPIUser"), PartIncidentalUser)
        If Not IsNothing(objPartIncidentalUser) Then
            objPartIncidentalUser.UserName = Me.txtUserName.Text
            objPartIncidentalUser.Email = Me.txtEmail.Text
            objPartIncidentalUser.Tipe = Me.ddlTipe.SelectedValue
            objPartIncidentalUser.PositionCC = txtPositionCC.Text 'ddlPositionCC.SelectedValue

            Return New PartIncidentalUserFacade(User).Update(objPartIncidentalUser)
        End If
        Return -1
    End Function

    Private Sub DeletePreRequire(ByVal nID As Integer)
        Dim objPartIncidentalUser As PartIncidentalUser = New PartIncidentalUserFacade(User).Retrieve(nID)
        Dim facade As PartIncidentalUserFacade = New PartIncidentalUserFacade(User)
        facade.DeleteFromDB(objPartIncidentalUser)
        dtgPIUser.CurrentPageIndex = 0
        BindDataGrid(dtgPIUser.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub ViewGroup(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objPartIncidentalUser As PartIncidentalUser = New PartIncidentalUserFacade(User).Retrieve(nID)
        _SessionHelper.SetSession("vsPIUser", objPartIncidentalUser)

        Me.ddlTipe.SelectedValue = objPartIncidentalUser.Tipe
        txtPositionCC.Text = objPartIncidentalUser.PositionCC
        'ddlPositionCC.SelectedValue = objPartIncidentalUser.PositionCC
        Me.txtEmail.Text = objPartIncidentalUser.Email
        Me.txtUserName.Text = objPartIncidentalUser.UserName
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub dtgPIUser_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPIUser.PageIndexChanged
        dtgPIUser.SelectedIndex = -1
        dtgPIUser.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPIUser.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        CheckUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewPartIncidentalEmail_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Penerima Email")
        End If
        btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.SavePartIncidentalEmail_Privilege)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objPartIncidentalUser As PartIncidentalUser = New PartIncidentalUser
        Dim objPartIncidentalUserFacade As PartIncidentalUserFacade = New PartIncidentalUserFacade(User)
        Dim nResult = -1

        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If objPartIncidentalUserFacade.ValidateValue(txtEmail.Text) = 0 Then
                objPartIncidentalUser.UserName = Me.txtUserName.Text
                objPartIncidentalUser.Email = Me.txtEmail.Text
                objPartIncidentalUser.PositionCC = txtPositionCC.Text 'ddlPositionCC.SelectedValue
                objPartIncidentalUser.Tipe = ddlTipe.SelectedValue
                nResult = objPartIncidentalUserFacade.Insert(objPartIncidentalUser)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                    ClearData()
                    dtgPIUser.CurrentPageIndex = 0
                    BindDataGrid(dtgPIUser.CurrentPageIndex)
                End If
            Else
                MessageBox.Show(SR.DataIsExist("Penerima Email"))
            End If

        Else
            Dim intUpdateResult As Integer = UpdateGroup()
            If intUpdateResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                If intUpdateResult = -2 Then
                    MessageBox.Show(SR.DataIsExist("PartIncidentalUser"))
                Else
                    MessageBox.Show(SR.UpdateSucces)
                    ClearData()
                    dtgPIUser.CurrentPageIndex = 0
                    BindDataGrid(dtgPIUser.CurrentPageIndex)
                End If
            End If
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        dtgPIUser.SelectedIndex = -1
    End Sub

    Private Sub dtgPIUser_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPIUser.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewGroup(e.Item.Cells(0).Text, False)
            Me.ddlTipe.Enabled = False
            'ddlPositionCC.enabled = False
            txtPositionCC.Enabled = False
            Me.txtEmail.ReadOnly = True
            Me.txtUserName.ReadOnly = True
            dtgPIUser.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewGroup(e.Item.Cells(0).Text, True)
            dtgPIUser.SelectedIndex = e.Item.ItemIndex
            Me.ddlTipe.Enabled = True
            'Me.ddlPositionCC.Enabled = True
            txtPositionCC.Enabled = True
            Me.txtEmail.ReadOnly = False
            Me.txtUserName.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeletePreRequire(e.Item.Cells(0).Text)
        End If
    End Sub

    Private deletePartPrivilege As Boolean
    Private Sub dtgPIUser_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPIUser.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If Not e.Item.DataItem Is Nothing Then
                'e.Item.DataItem.GetType().ToString()
                If e.Item.ItemIndex = 0 Then
                    deletePartPrivilege = SecurityProvider.Authorize(Context.User, SR.DeletePartIncidentalEmail_Privilege)
                End If
                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Or ItemType = ListItemType.SelectedItem Then
                    CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgPIUser.CurrentPageIndex * dtgPIUser.PageSize)
                End If

                Dim LinkHapus As LinkButton = CType(e.Item.FindControl("btnHapus"), LinkButton)
                LinkHapus.Visible = deletePartPrivilege
                If Not LinkHapus Is Nothing Then
                    CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If

            End If
        End If
    End Sub

    Private Sub dtgPIUser_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPIUser.SortCommand
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

        dtgPIUser.SelectedIndex = -1
        dtgPIUser.CurrentPageIndex = 0
        BindDataGrid(dtgPIUser.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

End Class