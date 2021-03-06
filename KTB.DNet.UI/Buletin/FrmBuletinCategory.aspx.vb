#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Buletin
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.UserManagement

#End Region

#Region ".NET Base Class Namespace Imports"

Imports System.IO
Imports System.Text

#End Region

Public Class FrmBuletinCategory
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlParent As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSubParent As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgBuletinCategory As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents valname As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents val2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblUserGroup As System.Web.UI.WebControls.Label
    Protected WithEvents txtUserGroup As System.Web.UI.WebControls.TextBox
    Protected WithEvents val3 As System.Web.UI.WebControls.RequiredFieldValidator

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
    Private objBuletinCategory As BuletinCategory
    Private arlBuletinCategory As ArrayList
    Private arlTotalBuletinCategory As ArrayList
    Dim sHelper As New SessionHelper
    Dim arrBuletinUserGroups As ArrayList
#End Region


#Region "Properties"
    Private Property SesAlertGroups() As ArrayList
        Get
            Return CType(sHelper.GetSession("AlertGroups"), ArrayList)
        End Get
        Set(ByVal Value As ArrayList)
            sHelper.SetSession("AlertGroups", Value)
        End Set
    End Property
#End Region

#Region "CustomMethod"

    '-- Bind To DropdownList Parent
    Private Sub PopulateParent()
        Dim org As Dealer = CType(Session.Item("DEALER"), Dealer)
        ddlParent.Items.Clear()
        Dim _ItemBlank = New ListItem("Silahkan Pilih", -1)
        Dim _BuletinFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        Dim list As ArrayList = _BuletinFacade.RetrieveParentList(org.Title)   '-- Retrive data with TopParent = 0
        Dim li As ListItem
        ddlParent.Items.Add(_ItemBlank)
        If list.Count > 0 Then
            For Each item As BuletinCategory In list
                li = New ListItem
                li.Text = item.Code
                li.Value = item.ID
                ddlParent.Items.Add(li)
            Next
        End If
    End Sub

    Private Sub FindBulletinCategory(ByVal currentPageIndex As Integer)
        Dim total = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlParent.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BuletinCategory), "TopParent", MatchType.Exact, ddlParent.SelectedItem.Value))
        End If

        If ddlSubParent.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BuletinCategory), "Parent", MatchType.Exact, ddlSubParent.SelectedItem.Value))
        End If

        If txtName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BuletinCategory), "Code", MatchType.[Partial], txtName.Text))
        End If

        If txtDescription.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BuletinCategory), "Description", MatchType.[Partial], txtDescription.Text))
        End If

        If ddlStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BuletinCategory), "Status", MatchType.Exact, ddlStatus.SelectedItem.Value))
        End If

        arlBuletinCategory = New BuletinCategoryFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgBuletinCategory.PageSize, _
        total, CType(ViewState("CurrentSortColumn"), String), _
        CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgBuletinCategory.DataSource = arlBuletinCategory

        arlTotalBuletinCategory = New BuletinCategoryFacade(User).Retrieve(criterias)
        dtgBuletinCategory.VirtualItemCount = total

    End Sub

    '-- Bind To DropDownList SubParent Depend on DropdownList Parent
    Private Sub PopulateSelectionCategory(ByVal parent As Integer)
        ddlSubParent.Items.Clear()
        Dim org As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim _ItemBlank = New ListItem("Silahkan Pilih", -1)
        Dim _BuletinFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        Dim list As ArrayList = _BuletinFacade.PopulateListView(ddlParent.SelectedValue, org.Title) '-- Retrive Data With Selected Parent
        Dim _item As ListItem
        Dim space As String = String.Empty
        ddlSubParent.Items.Add(_ItemBlank)
        If list.Count > 0 Then
            For Each item As BuletinCategory In list
                space = BuildLeadingSpace(item.Leveling) '--Get Level
                _item = New ListItem
                _item.Value = item.ID
                _item.Text = space & item.Code
                ddlSubParent.Items.Add(_item)
                space = String.Empty
            Next
        End If
    End Sub

    '-- Add Character For Dislay on DropDownList SubParent
    Private Function BuildLeadingSpace(ByVal count As Integer) As String
        Dim space As String = String.Empty
        If count > 1 Then
            For i As Integer = 0 To count - 2
                ' space += " . . . "
                space += Chr(3) & Chr(3) & Chr(3)
            Next
            space = space & "> "
        End If
        Return space
    End Function

    Protected Overrides Sub Render(ByVal writer As UI.HtmlTextWriter)
        Dim stream As MemoryStream = New MemoryStream
        Dim textW As TextWriter = New StreamWriter(stream)
        Dim newWriter As UI.HtmlTextWriter = New UI.HtmlTextWriter(textW)
        MyBase.Render(newWriter)
        newWriter.Close()
        textW.Close()
        Dim str As String = System.Text.Encoding.UTF8.GetString(stream.GetBuffer).Replace(Chr(3), "&nbsp;")

        'Dim intSubKategori As Integer = str.IndexOf("<select name=""ddlSubParent"" id=""ddlSubParent"">")
        'Dim strSubKategori As String = str.Substring(intSubKategori)
        'strSubKategori = strSubKategori.Substring(0, strSubKategori.IndexOf("</select>") + 9)
        'str.Replace(strSubKategori, "")
        'str.Insert(intSubKategori, strSubKategori.Replace(Chr(3), "&nbsp;"))

        writer.Flush()
        writer.Write(str)
    End Sub



    '-- Bind To DropdownList SubParent
    Private Sub PopulateSubParent()
        ddlSubParent.Items.Clear()
        Dim _ItemBlank = New ListItem("Silahkan Pilih", -1)
        Dim _BuletinFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        Dim list As ArrayList = _BuletinFacade.RetrieveSubParentList '-- Retrive Data with TopParent <> 0
        Dim _item As ListItem
        Dim space As String = String.Empty
        ddlSubParent.Items.Add(_ItemBlank)
        If list.Count > 0 Then
            For Each item As BuletinCategory In list
                space = BuildLeadingSpace(item.Leveling)
                _item = New ListItem
                _item.Value = item.ID
                _item.Text = space & item.Code
                ddlSubParent.Items.Add(_item)
                space = String.Empty
            Next
        End If
    End Sub

    '-- Change DropDownList Parent with SubParentValue
    Private Sub ChangePopulateSubParent()
        ddlParent.Items.Clear()
        Dim _ItemBlank = New ListItem("Silahkan Pilih", -1)
        Dim _BuletinFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        Dim list As ArrayList = _BuletinFacade.RetrieveSubParentList
        Dim _item As ListItem
        Dim space As String = String.Empty
        If list.Count > 0 Then
            ddlParent.Items.Add(_ItemBlank)
            For Each item As BuletinCategory In list
                space = BuildLeadingSpace(item.Leveling)
                _item = New ListItem
                _item.Value = item.ID
                _item.Text = space & item.Code
                ddlParent.Items.Add(_item)
                space = String.Empty
            Next
        End If
    End Sub

    '-- DropdownList Parent on Index Change
    Private Sub ddlParent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlParent.SelectedIndexChanged
        ddlSubParent.Enabled = True
        PopulateSelectionCategory(ddlParent.SelectedValue)
    End Sub

    Private Sub BindToDropdownList()
        '--DropdopwnList Status
        ddlStatus.DataSource = enumStatusBuletin.RetrieveStatus()
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
        ddlStatus.SelectedIndex = -1
    End Sub

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            arlBuletinCategory = New BuletinCategoryFacade(User).RetrieveActiveList(indexPage + 1, dtgBuletinCategory.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgBuletinCategory.DataSource = arlBuletinCategory
            dtgBuletinCategory.VirtualItemCount = totalRow
            dtgBuletinCategory.DataBind()
        End If
    End Sub

    '-- Get Fresh UI 
    Private Sub ClearData()
        ddlParent.Items.Clear()
        PopulateParent()
        txtName.Text() = String.Empty
        txtDescription.Text() = String.Empty
        btnSimpan.Enabled = True
        ddlParent.Enabled = True
        ddlStatus.Enabled = True
        ddlParent.SelectedIndex = -1
        ddlSubParent.SelectedIndex = -1
        ddlStatus.SelectedIndex = -1
        dtgBuletinCategory.SelectedIndex = -1
        txtName.ReadOnly = False
        txtDescription.ReadOnly = False
        txtUserGroup.Text = String.Empty
        'txtUserGroup.ReadOnly = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub UpdateBuletin()
        Dim org As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim objBCategory As BuletinCategory = CType(sHelper.GetSession("Buletin"), BuletinCategory)
        Dim objBCategoryFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        ' If objBCategoryFacade.ValidateCode(txtName.Text) <= 0 Then '-- Check Duplicate Data
        objBCategory.Code = txtName.Text
        objBCategory.Description = txtDescription.Text
        objBCategory.Status = ddlStatus.SelectedValue
        Dim nResult As Integer = -1
        Try

            Dim facade As BuletinDetailFacade = New BuletinDetailFacade(User)
            Dim objBCDetail As New BuletinCategory
            objBCDetail = objBCategoryFacade.Retrieve(objBCategory.ID)
            facade.DeleteFromDB(objBCDetail)

            Dim objBuletinDetail As New BuletinDetail
            Dim arrGroup As String() = txtUserGroup.Text.Trim().Split(";".ToCharArray())
            Dim arrBuletinDetail As New ArrayList
            'arrBuletinDetail = objBCategory.BuletinDetails

            'For Each strGroup As String In arrGroup
            '    objBuletinDetail = New BuletinDetail
            '    objBuletinDetail.BuletinCategory = objBCategory
            '    objBuletinDetail.UserGroup = New UserGroupFacade(User).Retrieve(strGroup)
            '    objBCategory.BuletinDetails.Add(objBuletinDetail)
            'Next

            nResult = New BuletinCategoryFacade(User).Update(objBCategory) '-- Update Data To Database
            'added by SS
            Dim list As ArrayList = New BuletinCategoryFacade(User).PopulateListView(objBCategory.ID, org.Title)
            For Each item As BuletinCategory In list
                item.Status = ddlStatus.SelectedValue
                nResult = New BuletinCategoryFacade(User).Update(item)
            Next

            MessageBox.Show(SR.UpdateSucces)
            BindDataGrid(0)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
        ' Else
        '    MessageBox.Show(SR.DataIsExist("Nama Buletin Kategori"))
        ' End If

    End Sub

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    '-- Criteria For Check Record Dependencies on Buletin
    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
    ByVal BuletinCategoryID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "BuletinCategory", MatchType.Exact, BuletinCategoryID)) '-- Check Id in Use or No
        Return criterias
    End Function

    '-- Criteria For Check Record Dependencies on Buletin Category
    Private Function CreateCriteriaForCheckRecordInBuletinCategory(ByVal DomainType As Type, _
   ByVal BuletinCategoryID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "Parent", MatchType.Exact, BuletinCategoryID)) '--Check Parent in use or No 
        Return criterias
    End Function

    'Delete Record Permanently
    Private Sub DeleteBuletin(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        '--Check Dependency on Table Buletin Category
        If New HelperFacade(User, GetType(BuletinCategory)).IsRecordExist(CreateCriteriaForCheckRecordInBuletinCategory(GetType(BuletinCategory), nID), _
                  CreateAggreateForCheckRecord(GetType(BuletinCategory))) Then
            iRecordCount = iRecordCount + 1
        End If

        '--Check Dependency on Table Buletin
        If New HelperFacade(User, GetType(Buletin)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(Buletin), nID), _
            CreateAggreateForCheckRecord(GetType(Buletin))) Then
            iRecordCount = iRecordCount + 1
        End If

        If iRecordCount > 0 Then '-- Check Result 
            MessageBox.Show(SR.CannotDelete)
        Else '-- Delete Permanently From Database
            Try
                Dim objBCategory As BuletinCategory = New BuletinCategoryFacade(User).Retrieve(nID)
                Dim facade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
                facade.DeleteFromDB(objBCategory)
                MessageBox.Show(SR.DeleteSucces)
                dtgBuletinCategory.CurrentPageIndex = 0
                BindDataGrid(dtgBuletinCategory.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
                dtgBuletinCategory.SelectedIndex = -1
                ClearData()
            End Try

        End If

    End Sub

    Private Sub ViewBuletin(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objBCategory As BuletinCategory = New BuletinCategoryFacade(User).Retrieve(nID)
        If Not IsNothing(objBCategory) Then
            'Todo session
            'Session.Add("Buletin", objBCategory)
            sHelper.SetSession("Buletin", objBCategory)
            If objBCategory.Parent = 0 Then  '-- TopParent
                PopulateParent()
                PopulateSubParent()
                ddlSubParent.SelectedValue = -1
                ddlParent.SelectedValue = -1 'objBCategory.ID
                txtName.Text = objBCategory.Code
                txtDescription.Text = objBCategory.Description
                ddlParent.Enabled = False
                ddlSubParent.Enabled = False
            ElseIf objBCategory.Parent <> 0 AndAlso objBCategory.Leveling > 1 Then '-- Not a TopParent and Level > 1
                'ChangePopulateSubParent()
                PopulateSubParent()
                ddlParent.SelectedValue = objBCategory.TopParent
                ddlSubParent.SelectedValue = objBCategory.Parent
                txtName.Text = objBCategory.Code
                txtDescription.Text = objBCategory.Description
                ddlSubParent.Enabled = False
                ddlParent.Enabled = False
            Else '-- Not a TopParent and Level = 1
                PopulateParent()
                PopulateSubParent()
                ddlParent.SelectedValue = objBCategory.Parent
                ddlSubParent.SelectedValue = -1 'objBCategory.ID
                txtName.Text = objBCategory.Code
                txtDescription.Text = objBCategory.Description
                ddlSubParent.Enabled = False
                ddlParent.Enabled = False
            End If

            ddlStatus.SelectedValue = objBCategory.Status

            Dim sGroup As String = String.Empty
            For Each item As BuletinDetail In objBCategory.BuletinDetails
                sGroup += item.UserGroup.Code & ";"
            Next
            If sGroup.Length > 0 Then
                sGroup = sGroup.Substring(0, sGroup.Length - 1)
            End If
            txtUserGroup.Text = sGroup
            'txtUserGroup.ReadOnly = True

        Else
            If EditStatus Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.ViewFail)
            End If
            dtgBuletinCategory.SelectedIndex = -1
            ClearData()
        End If
    End Sub

#End Region

#Region "Event Hendlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.BuletinViewCategory_Privilege) Then '-- Check User Privilege
                Response.Redirect("../frmAccessDenied.aspx?modulName=Buletin Kategory")
            End If
            InitiatePage()
            PopulateParent()     '-- Bind To DropdownList Parent
            PopulateSubParent()  '-- Bind To DropdownList Sub Parent
            BindToDropdownList() '-- Bind To DropdownList Status
            BindDataGrid(0)
            ddlSubParent.Enabled = False

            btnSimpan.Attributes.Add("onclick", "return ValidateEmpty();")
        End If
        txtUserGroup.Attributes.Add("readonly", "readonly")
        lblUserGroup.Attributes("onclick") = "ShowPPUserGroup()"
        'txtUserGroup.Attributes("onclick") = "this.blur()"
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objBCategory As BuletinCategory = New BuletinCategory
        Dim objBCategoryFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then  '-- If Condition is Insert
            If ddlParent.SelectedValue = -1 And ddlSubParent.SelectedValue = -1 Then  '--Create TopParent
                If objBCategoryFacade.ValidateCode(txtName.Text, 0, 0, 0) <= 0 Then
                    objBCategory.TopParent = 0
                    objBCategory.Parent = 0
                    objBCategory.Leveling = 0
                    objBCategory.Code = txtName.Text
                    objBCategory.Description = txtDescription.Text
                    objBCategory.Status = ddlStatus.SelectedValue

                    If txtUserGroup.Text.Trim <> "" Then
                        Dim objBuletinDetail As New BuletinDetail
                        Dim arrGroup As String() = txtUserGroup.Text.Trim().Split(";".ToCharArray())

                        For Each strGroup As String In arrGroup
                            Dim objUG As UserGroup
                            objBuletinDetail = New BuletinDetail
                            objBuletinDetail.BuletinCategory = objBCategory
                            objUG = New UserGroupFacade(User).Retrieve(strGroup)
                            If objUG Is Nothing Then
                            Else
                                If objUG.ID < 1 Then
                                Else
                                    objBuletinDetail.UserGroup = objUG
                                    objBCategory.BuletinDetails.Add(objBuletinDetail)
                                End If
                            End If
                        Next
                    End If

                    nResult = New BuletinCategoryFacade(User).Insert(objBCategory)

                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Nama Buletin Kategori"))
                End If

            ElseIf ddlParent.SelectedValue <> -1 And ddlSubParent.SelectedValue = -1 Then  'Create SubParent on Level 1 
                If objBCategoryFacade.ValidateCode(txtName.Text, ddlParent.SelectedValue, ddlParent.SelectedValue, 1) <= 0 Then
                    objBCategory.TopParent = ddlParent.SelectedValue
                    objBCategory.Parent = ddlParent.SelectedValue
                    objBCategory.Leveling = 1
                    objBCategory.Code = txtName.Text
                    objBCategory.Description = txtDescription.Text
                    objBCategory.Status = ddlStatus.SelectedValue

                    'Dim objBuletinDetail As New BuletinDetail
                    'Dim arrGroup As String() = txtUserGroup.Text.Trim().Split(";".ToCharArray())

                    'For Each strGroup As String In arrGroup
                    '    objBuletinDetail = New BuletinDetail
                    '    objBuletinDetail.BuletinCategory = objBuletinCategory
                    '    objBuletinDetail.UserGroup = New UserGroupFacade(User).Retrieve(strGroup)
                    '    objBCategory.BuletinDetails.Add(objBuletinDetail)
                    'Next
                    If txtUserGroup.Text.Trim <> "" Then
                        Dim objBuletinDetail As New BuletinDetail
                        Dim arrGroup As String() = txtUserGroup.Text.Trim().Split(";".ToCharArray())

                        For Each strGroup As String In arrGroup
                            Dim objUG As UserGroup
                            objBuletinDetail = New BuletinDetail
                            objBuletinDetail.BuletinCategory = objBCategory
                            objUG = New UserGroupFacade(User).Retrieve(strGroup)
                            If objUG Is Nothing Then
                            Else
                                If objUG.ID < 1 Then
                                Else
                                    objBuletinDetail.UserGroup = objUG
                                    objBCategory.BuletinDetails.Add(objBuletinDetail)
                                End If
                            End If
                        Next
                    End If

                    nResult = New BuletinCategoryFacade(User).Insert(objBCategory)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Nama Buletin Kategori"))
                End If

            ElseIf ddlParent.SelectedValue <> -1 And ddlSubParent.SelectedValue <> -1 Then '-- Create SubParent on Level  Greater Then 1 
                Dim obj As BuletinCategory = New BuletinCategoryFacade(User).Retrieve(CInt(ddlSubParent.SelectedValue))
                If objBCategoryFacade.ValidateCode(txtName.Text, ddlParent.SelectedValue, ddlSubParent.SelectedValue, obj.Leveling + 1) <= 0 Then
                    objBCategory.TopParent = ddlParent.SelectedValue
                    objBCategory.Parent = ddlSubParent.SelectedValue
                    objBCategory.Leveling = obj.Leveling + 1
                    objBCategory.Code = txtName.Text
                    objBCategory.Description = txtDescription.Text
                    objBCategory.Status = ddlStatus.SelectedValue


                    'Dim objBuletinDetail As New BuletinDetail
                    'Dim arrGroup As String() = txtUserGroup.Text.Trim().Split(";".ToCharArray())

                    'For Each strGroup As String In arrGroup
                    '    objBuletinDetail = New BuletinDetail
                    '    objBuletinDetail.UserGroup = New UserGroupFacade(User).Retrieve(strGroup)
                    '    objBuletinDetail.BuletinCategory = objBCategory
                    '    objBCategory.BuletinDetails.Add(objBuletinDetail)
                    'Next
                    If txtUserGroup.Text.Trim <> "" Then
                        Dim objBuletinDetail As New BuletinDetail
                        Dim arrGroup As String() = txtUserGroup.Text.Trim().Split(";".ToCharArray())

                        For Each strGroup As String In arrGroup
                            Dim objUG As UserGroup
                            objBuletinDetail = New BuletinDetail
                            objBuletinDetail.BuletinCategory = objBCategory
                            objUG = New UserGroupFacade(User).Retrieve(strGroup)
                            If objUG Is Nothing Then
                            Else
                                If objUG.ID < 1 Then
                                Else
                                    objBuletinDetail.UserGroup = objUG
                                    objBCategory.BuletinDetails.Add(objBuletinDetail)
                                End If
                            End If
                        Next
                    End If

                    nResult = New BuletinCategoryFacade(User).Insert(objBCategory)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Nama Buletin Kategori"))
                End If

            Else

                MessageBox.Show(SR.GridIsEmpty("Nama Buletin Kategori"))
            End If

        Else
            UpdateBuletin() '-- Update Change
        End If

        ClearData()
        ddlSubParent.Enabled = False
        dtgBuletinCategory.CurrentPageIndex = 0
        BindDataGrid(dtgBuletinCategory.CurrentPageIndex)
    End Sub

    Private Function PopulateCategoryName(ByVal parent As Integer) As String
        Dim _objBuletinCategory As BuletinCategory
        Dim _arlBuletinCategory As ArrayList
        Dim CategoryName As String
        Dim _tmpParent As Integer = parent

        While _tmpParent > 0
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BuletinCategory), "ID", MatchType.Exact, _tmpParent))
            _arlBuletinCategory = New BuletinCategoryFacade(User).Retrieve(criterias)
            If CategoryName = String.Empty Then
                CategoryName = _arlBuletinCategory(0).code
            Else
                CategoryName = _arlBuletinCategory(0).code & " > " & CategoryName
            End If
            _tmpParent = _arlBuletinCategory(0).parent

        End While

        Return CategoryName
    End Function

    Private Sub dtgBuletinCategory_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBuletinCategory.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arlBuletinCategory Is Nothing) Then
                objBuletinCategory = arlBuletinCategory(e.Item.ItemIndex)
                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgBuletinCategory.CurrentPageIndex * dtgBuletinCategory.PageSize)

                Dim _lblNamaKategori As Label = CType(e.Item.FindControl("lblNamaKategori"), Label)
                If objBuletinCategory.Parent <> 0 Then
                    _lblNamaKategori.Text = PopulateCategoryName(objBuletinCategory.Parent) & " > " & objBuletinCategory.Code '-- Perent Level to subParent
                Else
                    _lblNamaKategori.Text = objBuletinCategory.Code '-- TopParent Lavel
                End If

                Dim _lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                _lblStatus.Text = CType(objBuletinCategory.Status, enumStatusBuletin.StatusBuletin).ToString
            End If

        End If
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If
    End Sub

    Private Sub dtgBuletinCategory_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgBuletinCategory.ItemCommand
        If (e.CommandName = "View") Then             '-- View Condition
            ViewState.Add("vsProcess", "View")
            ViewBuletin(e.Item.Cells(0).Text, False)
            ddlParent.Enabled = False
            ddlSubParent.Enabled = False
            ddlStatus.Enabled = False
            txtName.ReadOnly = True
            txtDescription.ReadOnly = True
            btnSimpan.Enabled = False
        ElseIf e.CommandName = "Edit" Then           '-- Edit/Update Condition
            ViewState.Add("vsProcess", "Edit")
            ViewBuletin(e.Item.Cells(0).Text, True)
            dtgBuletinCategory.SelectedIndex = e.Item.ItemIndex
            ddlStatus.Enabled = True
            txtName.ReadOnly = True
            txtDescription.ReadOnly = False
            'txtUserGroup.ReadOnly = False
            btnSimpan.Enabled = True
        ElseIf e.CommandName = "Delete" Then         '-- Delete Permanentely Conditon
            DeleteBuletin(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState.Clear()
        Response.Redirect("../default.aspx")
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData() '-- Refresh UI
        ddlSubParent.Enabled = False
    End Sub

    '-- Sorting DataGrid
    Private Sub dtgBuletinCategory_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgBuletinCategory.SortCommand
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

        dtgBuletinCategory.SelectedIndex = -1
        dtgBuletinCategory.CurrentPageIndex = 0
        BindDataGrid(dtgBuletinCategory.CurrentPageIndex)
        ClearData()
    End Sub

    '-- Paging Datagrid
    Private Sub dtgBuletinCategory_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgBuletinCategory.PageIndexChanged
        dtgBuletinCategory.SelectedIndex = -1
        dtgBuletinCategory.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgBuletinCategory.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        FindBulletinCategory(dtgBuletinCategory.CurrentPageIndex)
        dtgBuletinCategory.DataBind()
    End Sub
#End Region


    Private Sub dtgBuletinCategory_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBuletinCategory.ItemCreated

    End Sub

    Private Sub LinkButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Write("<script language='javascript'>showPopUp('../General/../PopUp/PopUpUserGroup.aspx?x=Territory','',500,760);</script>")
    End Sub
End Class
