Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search

Imports KTB.DNet.Utility

Public Class FrmNewRole
  Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

  'This call is required by the Web Form Designer.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

  End Sub
  Protected WithEvents lblName As System.Web.UI.WebControls.Label
  Protected WithEvents btnBack As System.Web.UI.WebControls.Button
  Protected WithEvents btnSave As System.Web.UI.WebControls.Button
  Protected WithEvents txtRoleName As System.Web.UI.WebControls.TextBox
  Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
  Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
  Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
  Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
  Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
  Protected WithEvents Regularexpressionvalidator2 As System.Web.UI.WebControls.RegularExpressionValidator
  Protected WithEvents lblSearchTerm1 As System.Web.UI.WebControls.Label
  Protected WithEvents dtgOrgPrivilege As System.Web.UI.WebControls.DataGrid
  Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
  Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
  Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
  Protected WithEvents txtFilterDescription As System.Web.UI.WebControls.TextBox
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

  Private sHRole As SessionHelper = New SessionHelper
  Private objDealer As Dealer
  Private sessDealer As Dealer

  Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    sessDealer = sHRole.GetSession("DEALER")
    ActivateUserPrivilege()
    If Not IsPostBack Then
      lblPopUpDealer.Attributes.Add("onClick", "ShowPPDealerSelection();")
    End If
    If sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
      DealerPageLoad()
    ElseIf sessDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
      txtDealerCode.Attributes.Add("onblur", "FindDealer()")
      KtbPageLoad()
    End If
  End Sub

  Private Sub ActivateUserPrivilege()
        If Not IsNothing(sessDealer) Then
            If sessDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If Not SecurityProvider.Authorize(Context.User, SR.AdminCreateNewRoleKTB_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Role Baru")
                End If
                Return
            ElseIf sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER OrElse sessDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                If Not SecurityProvider.Authorize(Context.User, SR.AdminCreateNewRoleDealer_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Role Baru")
                End If
                Return
            End If
        End If
        Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Role Baru")
  End Sub

  Public Sub InitiatePage()
    txtDealerCode.Text = objDealer.DealerCode
    txtDealerCode.AutoPostBack = False
    lblName.Text = objDealer.DealerName
    lblSearchTerm1.Text = objDealer.SearchTerm1
    ViewState("vsProcess") = "vsInsert"
    ViewState("vsSortColumn") = "Privilege.ID"
    ViewState("vsSortDirect") = Sort.SortDirection.ASC
    sHRole.RemoveSession("objDataGrid")
    sHRole.RemoveSession("checkedData")
  End Sub

  Private Sub KtbInitiatePage()
    txtDealerCode.Text = ""
    txtDealerCode.AutoPostBack = True
    lblName.Text = ""
    lblSearchTerm1.Text = ""
    ViewState("vsProcess") = "vsInitiate"
    ViewState("vsSortColumn") = "Privilege.ID"
    ViewState("vsSortDirect") = Sort.SortDirection.ASC
    sHRole.RemoveSession("objDataGrid")
  End Sub

  Public Sub BindDataGrid()
    Dim objOrganizationPrivilege As ArrayList = sHRole.GetSession("objDataGrid")

    btnSave.Enabled = False
    If IsNothing(objOrganizationPrivilege) Then
      Try
        Dim crit As CriteriaComposite
                'Dim PrivTitle As String = New System.Text.StringBuilder("('").Append(objDealer.Title).Append("','").Append(CStr(EnumDealerTittle.DealerTittle.KTB_DEALER)).Append("','").Append(CStr(EnumDealerTittle.DealerTittle.DEALER_LEASING)).Append("')").ToString()
                Dim PrivTitle As String = "('0','1','2','3','4','5','6')"


        crit = New CriteriaComposite(New Criteria(GetType(OrganizationPrivilege), "Dealer.ID", MatchType.Exact, objDealer.ID))
        crit.opAnd(New Criteria(GetType(OrganizationPrivilege), "Privilege.Title", MatchType.InSet, PrivTitle))

        If txtFilterDescription.Text <> "" Then
          Dim strFilter() As String = txtFilterDescription.Text.Split(";")
          For i As Integer = 0 To strFilter.Length - 1
            crit.opAnd(New Criteria(GetType(OrganizationPrivilege), "Privilege.Description", MatchType.[Partial], strFilter(i)))
          Next
        End If

        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

        If (Not IsNothing(ViewState("vsSortColumn"))) And (Not IsNothing(ViewState("vsSortDirect"))) Then
          sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(OrganizationPrivilege), ViewState("vsSortColumn"), ViewState("vsSortDirect")))
        Else
          sortColl = Nothing
        End If
        dtgOrgPrivilege.CurrentPageIndex = 0

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

  Public Sub ClearPage()
    txtRoleName.Text = ""
    txtDescription.Text = ""
    ddlStatus.SelectedIndex = 0

    For Each item As DataGridItem In dtgOrgPrivilege.Items
      If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
        Dim cbItem As CheckBox = CType(item.FindControl("cbItem"), CheckBox)
        cbItem.Checked = False
      ElseIf item.ItemType = ListItemType.Header Then
        Dim cbAll As CheckBox = CType(item.FindControl("cbAll"), CheckBox)
        cbAll.Checked = False
      End If
    Next

    sHRole.RemoveSession("checkedData")
  End Sub


  Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    If ViewState("vsProcess") = "vsInsert" Then
      If Not Page.IsValid Then
        Return
      End If

      If isPageValid() Then
        If Not IsUnhack() Then
          MessageBox.Show("< dan > bukan karakter valid")
          Return
        End If

        If HasSpaceInRoleName() Then
          MessageBox.Show("Tidak boleh ada spasi pada role name")
          Return
        End If
                objDealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
                If Not IsNothing(objDealer) And objDealer.ID <> 0 Then
                    sHRole.SetSession("objDealer", objDealer)
                Else
                    sHRole.RemoveSession("objDealer")
                End If

                Try
                    Dim nID As Integer = InsertData()

                    sHRole.SetSession("vsProcess", "Edit")
                    Dim objRole As Role = New RoleFacade(User).Retrieve(nID)
                    sHRole.SetSession("editRole", objRole)
                    sHRole.SetSession("txtFilterDescription", txtFilterDescription.Text)
                    sHRole.SetSession("backURL", "./FrmNewRole.aspx")
                    Response.Redirect("frmEditViewRole.aspx")

                    'MessageBox.Show(SR.SaveSuccess)
                    'ClearPage()
                    'sHRole.RemoveSession("objDataGrid")
                    'If sessDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    '    txtDealerCode.Text = ""
                    '    objDealer = Nothing
                    '    sHRole.RemoveSession("objDealer")
                    'Else
                    '    BindDataGrid()
                    'End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Else
        MessageBox.Show("Data tidak valid")
      End If
    End If
  End Sub

  Private Function isPageValid() As Boolean
    If txtRoleName.Text = "" Then
      Return False
    End If
    If txtDescription.Text = "" Then
      Return False
    End If
    If sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER And Not (sessDealer Is objDealer) Then
      Return False
    End If
    Return True
  End Function

  Private Function InsertData() As Integer
    Dim objFacade As RoleFacade = New RoleFacade(User)
    If isRoleExist() Then
      Throw New Exception(SR.DataIsExist("Nama Role"))
    End If
    Dim objRole As Role = New Role
    objRole.Dealer = objDealer
    objRole.RoleName = txtRoleName.Text
    objRole.Description = txtDescription.Text
    objRole.RoleStatus = CInt(ddlStatus.SelectedValue)

    Dim checkedData As ArrayList = CType(sHRole.GetSession("checkedData"), ArrayList)
    If Not IsNothing(checkedData) Then
      For Each item As String In checkedData
        Dim objDetail As RoleOrganizationPrivilege = New RoleOrganizationPrivilege
        objDetail.OrganizationPrivilege = New OrganizationPrivilegeFacade(User).Retrieve(CInt(item))
        objRole.RoleOrganizationPrivileges.Add(objDetail)
      Next
    End If

    Dim nResult As Integer = -2
    Try
      nResult = objFacade.Insert(objRole)
      If nResult = -1 Then
        Throw New Exception(SR.SaveFail)
      End If
    Catch
      Throw New Exception(SR.SaveFail)
    End Try
    Return nResult
  End Function

  Private Function isRoleExist() As Boolean
    Dim objFacade As RoleFacade = New RoleFacade(User)
    Return (objFacade.ValidateCode(objDealer.ID, txtRoleName.Text) > 0)
  End Function

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

  Private Sub dtgOrgPrivilege_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgOrgPrivilege.PageIndexChanged
    dtgOrgPrivilege.CurrentPageIndex = e.NewPageIndex
    BindDataGrid()
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

  Private Sub dtgOrgPrivilege_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgOrgPrivilege.ItemDataBound
    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
      e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgOrgPrivilege.CurrentPageIndex * dtgOrgPrivilege.PageSize)
      Dim cbItem As CheckBox = e.Item.FindControl("cbItem")
      Dim id As String = e.Item.Cells(2).Text
      Dim checkedData As ArrayList = CType(sHRole.GetSession("checkedData"), ArrayList)
      cbItem.Checked = False
      If Not IsNothing(checkedData) Then
        If checkedData.Contains(id) Then
          cbItem.Checked = True
        End If
      End If
    End If
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

  Private Function HasSpaceInRoleName() As Boolean
    Return txtRoleName.Text.IndexOf(" ") >= 0
  End Function

  Private Sub DealerPageLoad()
    objDealer = sessDealer
    If Not IsPostBack Then
      txtDealerCode.ReadOnly = True
      lblPopUpDealer.Visible = False
      InitiatePage()
      'BindDataGrid()
      ClearPage()
    Else
      ListingCheckedData()
    End If
  End Sub

  Private Sub KtbPageLoad()
    If Not IsPostBack Then
      KtbInitiatePage()
      txtDealerCode.ReadOnly = False
      lblPopUpDealer.Visible = True
      'DealerCodeChanged()
    Else
      objDealer = sHRole.GetSession("objDealer")
      ListingCheckedData()
      If Not IsNothing(objDealer) Then
        BindDataGrid()
      End If
    End If
  End Sub

  Private Sub txtDealerCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDealerCode.TextChanged
    DealerCodeChanged()
  End Sub

  Private Sub DealerCodeChanged()
    objDealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
    If Not IsNothing(objDealer) And objDealer.ID <> 0 Then
      sHRole.SetSession("objDealer", objDealer)
    Else
      sHRole.RemoveSession("objDealer")
    End If
    InitiatePage()
    txtDealerCode.AutoPostBack = True
    dtgOrgPrivilege.CurrentPageIndex = 0
    sHRole.RemoveSession("objDataGrid")
    BindDataGrid()
    ClearPage()
  End Sub
End Class
