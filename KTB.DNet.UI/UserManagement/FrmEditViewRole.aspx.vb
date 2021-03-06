Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility

Public Class FrmEditViewRole
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTerm1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Regularexpressionvalidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgOrgPrivilege As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtFilterDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRoleName As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sHRole As SessionHelper = New SessionHelper
    Private objDealer As Dealer
    Private objEditingRole As Role
    Private backURL As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objEditingRole = CType(sHRole.GetSession("editRole"), Role)

        If Not Request.QueryString("backURL") Is Nothing AndAlso Request.QueryString("backURL") <> String.Empty Then
            backURL = Request.QueryString("backURL")
        Else
            backURL = CType(sHRole.GetSession("backURL"), String)
        End If

        'btnBack.Visible = backURL <> "./FrmNewRole.aspx"


        If IsNothing(objEditingRole) Then
            Return
        End If

        objDealer = objEditingRole.Dealer

        If Not IsPostBack Then
            If backURL = "./FrmNewRole.aspx" Then
                MessageBox.Show(SR.SaveSuccess)
            End If
            InitiatePage()
            If Not IsNothing(sHRole.GetSession("txtFilterDescription")) Then
                Dim txt As String = CType(sHRole.GetSession("txtFilterDescription"), String)
                sHRole.RemoveSession("txtFilterDescription")
                txtFilterDescription.Text = txt
                BindDataGrid()
            End If

            'BindDataGrid()
        Else
            ListingCheckedData()
        End If
    End Sub

    Public Sub InitiatePage()
        lblCode.Text = objDealer.DealerCode
        lblName.Text = objDealer.DealerName
        lblSearchTerm1.Text = objDealer.SearchTerm1

        ViewState("vsSortColumn") = "Privilege.ID"
        ViewState("vsSortDirect") = Sort.SortDirection.ASC

        If Not IsNothing(objEditingRole) Then
            If Not IsNothing(sHRole.GetSession("vsProcess")) Then
                If sHRole.GetSession("vsProcess") = "Edit" Then
                    ViewState("vsProcess") = "vsUpdate"
                    SetUpdateMode(objEditingRole)
                ElseIf sHRole.GetSession("vsProcess") = "View" Then
                    ViewState("vsProcess") = "vsView"
                    SetViewMode(objEditingRole)
                End If
            Else
                Response.Write("<script language=javascript>history.back(-1);</script>")
            End If
        Else
            Response.Write("<script language=javascript>history.back(-1);</script>")
        End If

        CreateCheckedData(objEditingRole)
        sHRole.RemoveSession("objDataGrid")
    End Sub

    Public Sub SetUpdateMode(ByVal objRole As Role)
        txtRoleName.Text = objRole.RoleName
        txtDescription.Text = objRole.Description
        ddlStatus.SelectedIndex = 0
        If objEditingRole.RoleStatus Then
            ddlStatus.SelectedIndex = 1
        End If


        txtRoleName.ReadOnly = True
        txtDescription.ReadOnly = False
        ddlStatus.Enabled = True
        btnSave.Visible = True
    End Sub

    Public Sub SetViewMode(ByVal objRole As Role)
        txtRoleName.Text = objRole.RoleName
        txtDescription.Text = objRole.Description
        ddlStatus.SelectedIndex = 0
        If objEditingRole.RoleStatus Then
            ddlStatus.SelectedIndex = 1
        End If

        txtRoleName.ReadOnly = True
        txtDescription.ReadOnly = False
        ddlStatus.Enabled = False
        btnSave.Visible = False
    End Sub

    Public Sub BindDataGrid()
        Dim objOrganizationPrivilege As ArrayList = sHRole.GetSession("objDataGrid")

        If IsNothing(objOrganizationPrivilege) Then
            Try
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrganizationPrivilege), "Dealer.ID", MatchType.Exact, objDealer.ID))
                'Dim PrivTitle As String = New System.Text.StringBuilder("('").Append(objDealer.Title).Append("','").Append(CStr(EnumDealerTittle.DealerTittle.KTB_DEALER)).Append("')").ToString()
                'crit.opAnd(New Criteria(GetType(OrganizationPrivilege), "Privilege.Title", MatchType.InSet, PrivTitle))
                crit.opAnd(New Criteria(GetType(OrganizationPrivilege), "Dealer.ID", MatchType.Exact, objDealer.ID))

                If txtFilterDescription.Text <> "" Then
                    Dim strFilter() As String = txtFilterDescription.Text.Split(";")
                    For i As Integer = 0 To strFilter.Length - 1
                        crit.opAnd(New Criteria(GetType(OrganizationPrivilege), "Privilege.Description", MatchType.[Partial], strFilter(i)))
                    Next
                End If

                Dim totalRow As Integer = 0

                Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

                If (Not IsNothing(ViewState("vsSortColumn"))) And (Not IsNothing(ViewState("vsSortDirect"))) Then
                    sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(OrganizationPrivilege), ViewState("vsSortColumn"), ViewState("vsSortDirect")))
                Else
                    sortColl = Nothing
                End If


                objOrganizationPrivilege = New OrganizationPrivilegeFacade(User).Retrieve(crit, sortColl)

                btnSave.Enabled = objOrganizationPrivilege.Count > 0
                sHRole.SetSession("objDataGrid", objOrganizationPrivilege)
            Catch ex As Exception
                MessageBox.Show("Harap periksa kembali kategori pencarian anda")
                objOrganizationPrivilege = New ArrayList
                dtgOrgPrivilege.DataBind()
                Return
            End Try

        End If

        If objOrganizationPrivilege.Count > 0 Then
            dtgOrgPrivilege.DataSource = objOrganizationPrivilege
        Else
            dtgOrgPrivilege.DataSource = Nothing
            MessageBox.Show("Data tidak ditemukan ")
        End If

        dtgOrgPrivilege.DataBind()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If ViewState("vsProcess") = "vsUpdate" Then
            If Not Page.IsValid Then
                Return
            End If

            If isPageValid() Then
                If Not IsUnhack() Then
                    MessageBox.Show("< dan > bukan karakter valid")
                    Return
                End If
                Try
                    UpdateData()
                    MessageBox.Show(SR.SaveSuccess)
                    LogChanges()
                    dtgOrgPrivilege.CurrentPageIndex = 0
                    sHRole.RemoveSession("objDataGrid")
                    BindDataGrid()
                Catch ex As Exception
                    MessageBox.Show(SR.SaveFail)
                End Try
            Else
                MessageBox.Show("Data tidak valid")
            End If
        End If
    End Sub

    Private Sub LogChanges()

        Dim RemovedPrivilege As String = ""
        Dim AddedPrivilege As String = ""

        For Each item As DataGridItem In dtgOrgPrivilege.Items
            Dim cbItem As CheckBox = item.FindControl("cbItem")
            Dim cbInitial As CheckBox = item.FindControl("cbInitial")
            If cbItem.Checked <> cbInitial.Checked Then
                Dim lblPrivilegeName As Label = item.FindControl("lblPrivilegeName")
                If cbItem.Checked Then
                    AddedPrivilege += lblPrivilegeName.Text & ","
                Else
                    RemovedPrivilege += lblPrivilegeName.Text & ","
                End If
            End If

        Next

        If AddedPrivilege <> "" Then
            AddedPrivilege = Left(AddedPrivilege, AddedPrivilege.Length - 1)
            LogTosyslog("privilege added for role " & objEditingRole.Description & " are : " & AddedPrivilege, "privilege-added", "success", "web-security", "add")
        End If
        If RemovedPrivilege <> "" Then
            RemovedPrivilege = Left(RemovedPrivilege, RemovedPrivilege.Length - 1)
            LogTosyslog("privilege removed for role " & objEditingRole.Description & " are : " & RemovedPrivilege, "privilige-removed", "success", "web-security", "delete")
        End If



    End Sub

    Private Sub LogTosyslog(ByVal message As String, ByVal sbMsg As String, ByVal rslMsg As String, Optional ByVal mdlmsg As String = "web-security", Optional ByVal sbAction As String = "view")
        Dim strLog As Boolean = KTB.DNet.Lib.WebConfig.GetValue("EnableSyslog")
        If strLog Then
            Dim objSyslog As SyslogParameter = New SyslogParameter(User)
            Dim m As KTB.DNet.Lib.SysLogXMLMessage = New KTB.DNet.Lib.SysLogXMLMessage
            m.Action = sbAction.ToLower
            m.SubBlockName = sbMsg.ToLower
            m.FullMessage = message.ToLower
            m.ModuleName = mdlmsg.ToLower
            m.Pages = HttpContext.Current.Request.Url.LocalPath
            m.RemoteIPAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
            m.StatusResult = rslMsg.ToLower
            m.Status = KTB.DNet.[Lib].DNetLogFormatStatus.Direct
            m.BlockName = "privilege-management"

            Try
                m.UserName = IIf(User.Identity.Name.Length > 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name).ToLower
            Catch ex As Exception
                m.UserName = "Wb-Usr"
            End Try
            m.Dealer = CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode.ToLower
            objSyslog.LogError(m)
        End If
    End Sub

    Private Function isPageValid() As Boolean
        If txtRoleName.Text = "" Then
            Return False
        End If
        If txtDescription.Text = "" Then
            Return False
        End If
        Return True
    End Function

    Private Sub UpdateData()
        Dim objFacade As RoleFacade = New RoleFacade(User)
        If Not isRoleExist() Then
            Throw New Exception(SR.DataNotFound("Nama Role"))
        End If
        Dim objRole As Role = objEditingRole

        objRole.RoleStatus = CInt(ddlStatus.SelectedValue)
        objRole.Description = txtDescription.Text

        Dim checkedData As ArrayList = CType(sHRole.GetSession("checkedData"), ArrayList)

        'Pak jgn diubah lagi, tlg diupdate RoleFacadenya aja'
        Try
            If objFacade.Update(objRole, checkedData) = -1 Then
                Throw New Exception(SR.SaveFail)
            End If
            objRole = New RoleFacade(User).Retrieve(objRole.ID)
            sHRole.SetSession("editRole", objRole)
            CreateCheckedData(objRole)
        Catch
            Throw New Exception(SR.SaveFail)
        End Try
    End Sub

    Private Function isRoleExist() As Boolean
        Dim objFacade As RoleFacade = New RoleFacade(User)
        Return (objFacade.ValidateCode(objDealer.ID, txtRoleName.Text) > 0)
    End Function

    Private Sub dtgOrgPrivilege_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgOrgPrivilege.ItemDataBound
        If e.Item.ItemType = ListItemType.Header And ViewState("vsProcess") = "vsView" Then
            Dim cbAll As CheckBox = e.Item.FindControl("cbAll")
            cbAll.Visible = False
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgOrgPrivilege.CurrentPageIndex * dtgOrgPrivilege.PageSize)
            Dim cbItem As CheckBox = e.Item.FindControl("cbItem")
            Dim cbInitial As CheckBox = e.Item.FindControl("cbInitial")
            Dim id As String = e.Item.Cells(2).Text
            Dim checkedData As ArrayList = CType(sHRole.GetSession("checkedData"), ArrayList)
            cbItem.Checked = False
            cbInitial.Checked = False
            If Not IsNothing(checkedData) Then
                If checkedData.Contains(id) Then
                    cbItem.Checked = True
                    cbInitial.Checked = True
                End If
            End If
            If ViewState("vsProcess") = "vsView" Then
                cbItem.Enabled = False
            End If
        End If
    End Sub

    Private Sub dtgOrgPrivilege_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgOrgPrivilege.SortCommand
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

        dtgOrgPrivilege.SelectedIndex = -1
        dtgOrgPrivilege.CurrentPageIndex = 0
        sHRole.RemoveSession("objDataGrid")
        BindDataGrid()
    End Sub

    Private Sub ListingCheckedData()
        Dim listOfCheckedData As ArrayList = CType(sHRole.GetSession("checkedData"), ArrayList)
        If IsNothing(listOfCheckedData) Then
            listOfCheckedData = New ArrayList
        End If

        For Each item As DataGridItem In dtgOrgPrivilege.Items
            If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                Dim cbItem As CheckBox = CType(item.FindControl("cbItem"), CheckBox)
                If cbItem.Checked Then
                    If Not listOfCheckedData.Contains(item.Cells(2).Text) Then
                        listOfCheckedData.Add(item.Cells(2).Text)
                    End If
                Else
                    If listOfCheckedData.Contains(item.Cells(2).Text) Then
                        listOfCheckedData.Remove(item.Cells(2).Text)
                    End If
                End If
            End If
        Next

        sHRole.SetSession("checkedData", listOfCheckedData)

    End Sub

    Private Sub CreateCheckedData(ByVal objRole As Role)
        Dim listOfCheckedData As ArrayList = New ArrayList
        For Each item As RoleOrganizationPrivilege In objRole.RoleOrganizationPrivileges
            If Not IsNothing(item.OrganizationPrivilege) Then
                listOfCheckedData.Add(CStr(item.OrganizationPrivilege.ID))
            End If
        Next
        sHRole.SetSession("checkedData", listOfCheckedData)
    End Sub

    Private Sub dtgOrgPrivilege_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgOrgPrivilege.PageIndexChanged
        dtgOrgPrivilege.CurrentPageIndex = e.NewPageIndex
        BindDataGrid()
    End Sub

    Private Sub btnCari_Filter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgOrgPrivilege.CurrentPageIndex = 0
        sHRole.RemoveSession("objDataGrid")
        BindDataGrid()
    End Sub

    Private Function IsUnhack() As Boolean
        If txtRoleName.Text.IndexOf("<") >= 0 Or txtRoleName.Text.IndexOf(">") >= 0 Or txtRoleName.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        If txtDescription.Text.IndexOf("<") >= 0 Or txtDescription.Text.IndexOf(">") >= 0 Or txtDescription.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        Return True
    End Function

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(backURL)
    End Sub

End Class
