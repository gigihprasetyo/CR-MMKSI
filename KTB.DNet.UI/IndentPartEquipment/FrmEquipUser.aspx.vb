#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.IO
Imports KTB.DNet.Parser

#End Region

Public Class FrmEquipUser
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
    Protected WithEvents txtPositionCC As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPIUser As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

    Protected WithEvents dfPartShop As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button

    Protected WithEvents btnSimpanUpload As System.Web.UI.WebControls.Button

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

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.PengajuanPartIndentPartCreate_Privilege) Then
            'Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Indent Part Equipment - Daftar Status PO")
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.Buat_daftar_penerima_email_indent_part_equipment_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Indent Part Equipment - Penerima Email")
        End If
        If SecurityProvider.Authorize(Context.User, SR.Buat_daftar_penerima_email_indent_part_equipment_privilege) Then
            btnSimpan.Visible = True
            Me.dtgPIUser.Columns(7).Visible = True
        Else
            btnSimpan.Visible = False
            Me.dtgPIUser.Columns(7).Visible = False
        End If
        'btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.SavePartIncidentalEmail_Privilege)
    End Sub

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "UserName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            'get all data from course to fill prerequirecode base on coursecode
            _arrPIUser = New EquipUserFacade(User).RetrieveList()
            _SessionHelper.SetSession("objPIUser", _arrPIUser)
            'get prerequire data
            'Dim arlTmp As ArrayList = New EquipUserFacade(User).RetrieveActiveList(indexPage + 1, dtgPIUser.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            Dim arlTmp As ArrayList = Me.getData(totalRow)
            dtgPIUser.DataSource = arlTmp
            dtgPIUser.VirtualItemCount = totalRow
            dtgPIUser.DataBind()
            If (IsNothing(dtgPIUser.Items) Or dtgPIUser.Items.Count = 0) Then
                MessageBox.Show("Data tidak ditemukan")
            End If
        End If
    End Sub

    Private Function getData(ByRef TotRow As Integer) As ArrayList
        'Dim oEUFac = New EquipUserFacade(User)
        Dim cEU As New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sEU As New SortCollection
        Dim aEUs As ArrayList

        If (Me.txtUserName.Text.Trim <> String.Empty) Then
            cEU.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.[Partial], Me.txtUserName.Text.Trim()))
        End If
        If (Me.txtEmail.Text.Trim <> String.Empty) Then
            cEU.opAnd(New Criteria(GetType(EquipUser), "Email", MatchType.[Partial], Me.txtEmail.Text.Trim()))
        End If
        If (Me.txtPositionCC.Text.Trim <> String.Empty) Then
            cEU.opAnd(New Criteria(GetType(EquipUser), "PositionCC", MatchType.[Partial], Me.txtPositionCC.Text.Trim()))
        End If

        If ddlGroup.SelectedValue.Trim.Length > 0 Then
            cEU.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, CType(Me.ddlGroup.SelectedValue, Short)))
        End If

        cEU.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, CType(Me.ddlTipe.SelectedValue, String)))
        ' .RetrieveByCriteria(cEU, Me.dtgPIUser.CurrentPageIndex + 1, Me.dtgPIUser.PageSize, TotRow)
        aEUs = New EquipUserFacade(User).RetrieveActiveList(cEU, dtgPIUser.CurrentPageIndex + 1, dtgPIUser.PageSize, TotRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))

        Return aEUs
    End Function

    Private Sub BindDropDownList(ByVal dropdown As DropDownList)
        'dropdown.Items.Add(New ListItem("Approve Pengajuan SPPO", EquipUser.EquipUserGroup.Approved))   'exist
        'dropdown.Items.Add(New ListItem("Pengajuan SPPO Deposit B", EquipUser.EquipUserGroup.Deposit_B)) 'exist
        'dropdown.Items.Add(New ListItem("Pemindahan SPPO Deposit B ke C", EquipUser.EquipUserGroup.Deposit_C)) 'exist
        'dropdown.Items.Add(New ListItem("Tolak Pengajuan SPPO", EquipUser.EquipUserGroup.Reject))   'exist
        'dropdown.Items.Add(New ListItem("Pengajuan Estimasi", EquipUser.EquipUserGroup.Pengajuan_Estimasi)) 'exist
        'dropdown.Items.Add(New ListItem("Konfirmasi Harga Estimasi", EquipUser.EquipUserGroup.Konfirmasi_Harga_Estimasi))

        dropdown.Items.Add(New ListItem("Rilis Deposit B", EquipUser.EquipUserGroup.Approved))
        dropdown.Items.Add(New ListItem("Pengajuan Order Deposit B", EquipUser.EquipUserGroup.Deposit_B))
        dropdown.Items.Add(New ListItem("Pengajuan Order Deposit C", EquipUser.EquipUserGroup.Deposit_C))
        dropdown.Items.Add(New ListItem("Tolak Deposit B", EquipUser.EquipUserGroup.Reject))
        dropdown.Items.Add(New ListItem("Pengajuan Estimasi", EquipUser.EquipUserGroup.Pengajuan_Estimasi))
        dropdown.Items.Add(New ListItem("Konfirmasi Harga Estimasi", EquipUser.EquipUserGroup.Konfirmasi_Harga_Estimasi))

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
        btnCari.Enabled = True
        BindDataGrid(0)
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Function InsertGroup() As Integer
        Dim objEquipUser As EquipUser = New EquipUser
        Dim nResult As Integer = -1
        objEquipUser.UserName = Me.txtUserName.Text
        objEquipUser.Tipe = Me.ddlTipe.SelectedValue
        objEquipUser.Email = Me.txtEmail.Text
        objEquipUser.PositionCC = ddlTipe.SelectedValue
        objEquipUser.GroupType = CInt(ddlGroup.SelectedValue)
        nResult = New EquipUserFacade(User).Insert(objEquipUser)
        Return nResult
    End Function

    Private Function UpdateGroup() As Integer
        Dim facade As New EquipUserFacade(User)
        Dim objEquipUser As EquipUser = CType(_SessionHelper.GetSession("vsPIUser"), EquipUser)
        If Not IsNothing(objEquipUser) Then
            objEquipUser.UserName = Me.txtUserName.Text
            objEquipUser.Email = Me.txtEmail.Text
            objEquipUser.Tipe = Me.ddlTipe.SelectedValue
            objEquipUser.PositionCC = txtPositionCC.Text
            objEquipUser.GroupType = CInt(ddlGroup.SelectedValue)
            'If facade.ValidateValue(txtEmail.Text, objEquipUser.id) = 0 Then
            If facade.ValidateValue(txtUserName.Text, txtEmail.Text, CType(ddlGroup.SelectedValue, Short), CType(ddlTipe.SelectedValue, Short), txtPositionCC.Text.Trim) = 0 Then 'If facade.ValidateValue(txtEmail.Text, objEquipUser.id, ) = 0 Then
                Return facade.Update(objEquipUser)
            Else
                Return -2
            End If
        End If
        Return -1
    End Function

    Private Sub DeletePreRequire(ByVal nID As Integer)
        Dim objEquipUser As EquipUser = New EquipUserFacade(User).Retrieve(nID)
        Dim facade As EquipUserFacade = New EquipUserFacade(User)
        facade.DeleteFromDB(objEquipUser)
        dtgPIUser.CurrentPageIndex = 0
        BindDataGrid(dtgPIUser.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub ViewGroup(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objEquipUser As EquipUser = New EquipUserFacade(User).Retrieve(nID)
        _SessionHelper.SetSession("vsPIUser", objEquipUser)

        Me.ddlTipe.SelectedValue = objEquipUser.Tipe
        Me.ddlGroup.SelectedValue = objEquipUser.GroupType
        txtPositionCC.Text = objEquipUser.PositionCC
        Me.txtEmail.Text = objEquipUser.Email
        Me.txtUserName.Text = objEquipUser.UserName
        Me.btnSimpan.Enabled = EditStatus
    End Sub

#End Region

#Region "event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDropDownList(ddlGroup)
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSimpanUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpanUpload.Click

        Dim objEquipUser As EquipUser = New EquipUser
        Dim objEquipUserFacade As EquipUserFacade = New EquipUserFacade(User)
        Dim nResult = -1

        Dim arList As ArrayList = CType(_SessionHelper.GetSession("SessUpload"), ArrayList)
        Dim errData As String = ""
        For Each data As EquipUser In arList
            Try

                objEquipUser.UserName = data.UserName
                objEquipUser.Email = data.Email
                objEquipUser.PositionCC = data.PositionCC
                objEquipUser.Tipe = data.Tipe
                objEquipUser.GroupType = data.GroupType
                nResult = objEquipUserFacade.Insert(objEquipUser)
                If nResult = -1 Then
                    If errData.Trim.Length = 0 Then
                        errData = data.Email
                    Else
                        errData = errData & ", " & data.Email
                    End If
                End If
            Catch ex As Exception
                Dim debug = ""
            End Try
        Next

        ClearData()
        BindDataGrid(0)

        If errData.Trim.Length > 0 Then
            MessageBox.Show("Error Insert pada data berikut: " & errData)
        End If

        UploadControl(False)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objEquipUser As EquipUser = New EquipUser
        Dim objEquipUserFacade As EquipUserFacade = New EquipUserFacade(User)
        Dim nResult = -1

        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If objEquipUserFacade.ValidateValue(txtUserName.Text, txtEmail.Text, CType(ddlGroup.SelectedValue, Short)) = 0 Then
                objEquipUser.UserName = Me.txtUserName.Text
                objEquipUser.Email = Me.txtEmail.Text
                objEquipUser.PositionCC = txtPositionCC.Text 'ddlPositionCC.SelectedValue
                objEquipUser.Tipe = ddlTipe.SelectedValue
                objEquipUser.GroupType = CInt(ddlGroup.SelectedValue)
                nResult = objEquipUserFacade.Insert(objEquipUser)
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
                    MessageBox.Show(SR.DataIsExist("EquipUser"))
                Else
                    MessageBox.Show(SR.UpdateSucces)
                    ClearData()
                    dtgPIUser.CurrentPageIndex = 0
                    BindDataGrid(dtgPIUser.CurrentPageIndex)
                End If
            End If
        End If
        ViewState("vsUpload") = Nothing
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
            txtPositionCC.Enabled = False
            Me.txtEmail.ReadOnly = True
            Me.txtUserName.ReadOnly = True
            dtgPIUser.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewGroup(e.Item.Cells(0).Text, True)
            dtgPIUser.SelectedIndex = e.Item.ItemIndex
            Me.ddlTipe.Enabled = True
            txtPositionCC.Enabled = True
            Me.txtEmail.ReadOnly = False
            Me.txtUserName.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeletePreRequire(e.Item.Cells(0).Text)
        End If
    End Sub

    Private deletePartPrivilege As Boolean
    Private Sub dtgPIUser_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPIUser.ItemDataBound
        If IsNothing(ViewState("vsUpload")) Then
            If (e.Item.ItemIndex = -1) Then Return
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
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

    Private Sub dtgPIUser_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPIUser.PageIndexChanged
        dtgPIUser.SelectedIndex = -1
        dtgPIUser.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPIUser.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgPIUser.CurrentPageIndex = 0
        BindDataGrid(dtgPIUser.CurrentPageIndex)
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If (Not dfPartShop.PostedFile Is Nothing) AndAlso (dfPartShop.PostedFile.ContentLength > 0) Then
            ViewState("vsUpload") = "InsertUpload"
            ViewState("vsProcess") = "Upload"
            Dim fileExt As String = Path.GetExtension(dfPartShop.PostedFile.FileName)
            If Not (fileExt.ToLower() = ".xls" OrElse fileExt.ToLower() = ".xlsx") Then
                MessageBox.Show("Hanya Menerima File Excell")
                Return
            End If

            dtgPIUser.DataSource = New ArrayList
            dtgPIUser.DataBind()
            dtgPIUser.Visible = False

            Me.btnSimpan.Enabled = False

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate

            Dim msg As String = ""
            Try
                Dim SrcFile As String = Path.GetFileName(dfPartShop.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.ToString("dd/MM/yyy HHmmss") & SrcFile

                imp = New SAPImpersonate(_user, _password, _webServer)

                If imp.Start() Then
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(dfPartShop.PostedFile.InputStream, targetFile)
                    Dim parser As UploadEquipUserParser = New UploadEquipUserParser(dfPartShop.PostedFile.ContentType.ToString)

                    '-- Parse data file and store result into arraylist
                    Dim arlSPDueDateNotification As ArrayList = CType(parser.ParseExcelNoTransaction(targetFile, "[Sheet1$A1:C152]", "User"), ArrayList)

                    Dim i As Integer
                    If arlSPDueDateNotification.Count <= 0 Then
                        btnSimpan.Enabled = False
                    End If

                    _SessionHelper.SetSession("SessUpload", arlSPDueDateNotification)
                    dtgPIUser.DataSource = arlSPDueDateNotification '-- Reset datagrid first
                    dtgPIUser.CurrentPageIndex = 0
                    BindUpload(arlSPDueDateNotification)
                    dtgPIUser.Visible = True
                    UploadControl(True)
                End If
            Catch ex As Exception
                MessageBox.Show("Fail To Process " & ex.Message)
            Finally
                imp.StopImpersonate()
                imp = Nothing
            End Try
        Else
            MessageBox.Show("Berkas Upload tidak boleh Kosong")
        End If
    End Sub


    Private Sub UploadControl(ByVal state As Boolean)
        btnSimpan.Visible = Not state
        btnSimpanUpload.Visible = state
        txtUserName.Enabled = Not state
        txtEmail.Enabled = Not state
        txtPositionCC.Enabled = Not state
        ddlTipe.Enabled = Not state
        ddlGroup.Enabled = Not state
    End Sub

    Private Sub BindUpload(ByVal _arlData As ArrayList)
        Dim totalRow As Integer = 0

        Try
            If Not IsNothing(_arlData) Then
                btnSimpan.Enabled = True
                totalRow = _arlData.Count
                dtgPIUser.DataSource = _arlData
                dtgPIUser.VirtualItemCount = totalRow
                dtgPIUser.DataBind()
                btnCari.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class