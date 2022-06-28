#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmTrUserEmail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPEUser As System.Web.UI.WebControls.DataGrid

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

    Private Sub ActivatePrivilege()
        'SR.TrainingReferensi_Privilege - digunakan juga utk module ini.
        Dim objDealer As Dealer = CType(New SessionHelper().GetSession("DEALER"), Dealer)
        If Not IsNothing(objDealer) Then
            If Not SecurityProvider.Authorize(Context.User, SR.TrainingReferensi_Privilege) Or objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Penerima Email")
            End If
        Else
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Penerima Email")
        End If
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            'get all data from course to fill prerequirecode base on coursecode
            _arrPIUser = New TrUserEmailFacade(User).RetrieveList()
            _SessionHelper.SetSession("objPIUser", _arrPIUser)
            'get prerequire data
            dtgPEUser.DataSource = New TrUserEmailFacade(User).RetrieveActiveList(indexPage + 1, dtgPEUser.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgPEUser.VirtualItemCount = totalRow
            dtgPEUser.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        Me.txtUserName.ReadOnly = False
        Me.txtEmail.ReadOnly = False
        Me.ddlTipe.Enabled = True
        Me.txtUserName.Text = String.Empty
        Me.txtEmail.Text = String.Empty

        Me.ddlTipe.SelectedIndex = 0
        If dtgPEUser.Items.Count > 0 Then
            dtgPEUser.SelectedIndex = -1
        End If
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Function InsertGroup() As Integer
        Dim objTrUserEmail As TrUserEmail = New TrUserEmail
        Dim nResult As Integer = -1
        objTrUserEmail.UserName = Me.txtUserName.Text
        objTrUserEmail.Tipe = Me.ddlTipe.SelectedValue
        objTrUserEmail.Email = Me.txtEmail.Text
        nResult = New TrUserEmailFacade(User).Insert(objTrUserEmail)
        Return nResult
    End Function

    Private Function UpdateGroup() As Integer
        Dim objTrUserEmail As TrUserEmail = CType(_SessionHelper.GetSession("vsPIUser"), TrUserEmail)
        If Not IsNothing(objTrUserEmail) Then
            objTrUserEmail.UserName = Me.txtUserName.Text
            objTrUserEmail.Email = Me.txtEmail.Text
            objTrUserEmail.Tipe = Me.ddlTipe.SelectedValue

            Return New TrUserEmailFacade(User).Update(objTrUserEmail)
        End If
        Return -1
    End Function

    Private Sub DeletePreRequire(ByVal nID As Integer)
        Dim objTrUserEmail As TrUserEmail = New TrUserEmailFacade(User).Retrieve(nID)
        Dim facade As TrUserEmailFacade = New TrUserEmailFacade(User)
        facade.DeleteFromDB(objTrUserEmail)
        dtgPEUser.CurrentPageIndex = 0
        BindDataGrid(dtgPEUser.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub ViewGroup(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objTrUserEmail As TrUserEmail = New TrUserEmailFacade(User).Retrieve(nID)
        _SessionHelper.SetSession("vsPIUser", objTrUserEmail)

        Me.ddlTipe.SelectedValue = objTrUserEmail.Tipe
        Me.txtEmail.Text = objTrUserEmail.Email
        Me.txtUserName.Text = objTrUserEmail.UserName
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub dtgPEUser_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dtgPEUser.SelectedIndex = -1
        dtgPEUser.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPEUser.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivatePrivilege()
        If Not IsPostBack Then
            CheckUserPrivilege()
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

        Dim objTrUserEmail As TrUserEmail = New TrUserEmail
        Dim objTrUserEmailFacade As TrUserEmailFacade = New TrUserEmailFacade(User)
        Dim nResult = -1

        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If objTrUserEmailFacade.ValidateValue(txtEmail.Text) = 0 Then
                objTrUserEmail.UserName = Me.txtUserName.Text
                objTrUserEmail.Email = Me.txtEmail.Text
                objTrUserEmail.Tipe = ddlTipe.SelectedValue
                nResult = objTrUserEmailFacade.Insert(objTrUserEmail)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                    ClearData()
                    dtgPEUser.CurrentPageIndex = 0
                    BindDataGrid(dtgPEUser.CurrentPageIndex)
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
                    MessageBox.Show(SR.DataIsExist("TrUserEmail"))
                Else
                    MessageBox.Show(SR.UpdateSucces)
                    ClearData()
                    dtgPEUser.CurrentPageIndex = 0
                    BindDataGrid(dtgPEUser.CurrentPageIndex)
                End If
            End If
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        dtgPEUser.SelectedIndex = -1
    End Sub

    Private Sub dtgPEUser_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPEUser.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewGroup(e.Item.Cells(0).Text, False)
            Me.ddlTipe.Enabled = False
            Me.txtEmail.ReadOnly = True
            Me.txtUserName.ReadOnly = True
            dtgPEUser.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewGroup(e.Item.Cells(0).Text, True)
            dtgPEUser.SelectedIndex = e.Item.ItemIndex
            Me.ddlTipe.Enabled = True
            Me.txtEmail.ReadOnly = False
            Me.txtUserName.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeletePreRequire(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Sub dtgPEUser_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPEUser.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If Not e.Item.DataItem Is Nothing Then
                e.Item.DataItem.GetType().ToString()
                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Or ItemType = ListItemType.SelectedItem Then
                    Dim _no As Label = CType(e.Item.FindControl("lblNo"), Label)
                    _no.Text = e.Item.ItemIndex + 1 + (dtgPEUser.CurrentPageIndex * dtgPEUser.PageSize)
                End If

                Dim LinkHapus As LinkButton = CType(e.Item.FindControl("btnHapus"), LinkButton)
                LinkHapus.Visible = SecurityProvider.Authorize(Context.User, SR.DeletePartIncidentalEmail_Privilege)
                If Not LinkHapus Is Nothing Then
                    CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If

            End If
        End If
    End Sub

    Private Sub dtgPEUser_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPEUser.SortCommand
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

        dtgPEUser.SelectedIndex = -1
        dtgPEUser.CurrentPageIndex = 0
        BindDataGrid(dtgPEUser.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

End Class
