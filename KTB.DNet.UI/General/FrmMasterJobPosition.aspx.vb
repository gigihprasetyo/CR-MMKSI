Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Public Class FrmMasterJobPosition
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblMenu As System.Web.UI.WebControls.Label
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgJobPosition As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents cblMenu As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents lstBoxMenu As System.Web.UI.WebControls.ListBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _edit As Boolean
    Private _view As Boolean
    Dim objCategory As New JobPosition
    Dim arrJobPositionList As New ArrayList
    Private JobPosFacade As New JobPositionFacade(User)
    Private JobPosToMenuFacade As New JobPositionToMenuFacade(User)
    Private sessHelper As New SessionHelper
#End Region

#Region "PrivateCustomMethods"
    Private Sub ViewJobPosition(ByVal idJobPos As Integer)
        Dim ObjJobPosition As JobPosition = JobPosFacade.Retrieve(idJobPos)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(JobPositionToMenu), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(JobPositionToMenu), "JobPosition.ID", MatchType.Exact, ObjJobPosition.ID))

        Dim arrJP2Menu As ArrayList = JobPosToMenuFacade.Retrieve(criterias)

        If Not IsNothing(ObjJobPosition) Then
            txtCode.Text = ObjJobPosition.Code
            txtDescription.Text = ObjJobPosition.Description
            ddlCategory.SelectedValue = IIf(IsNothing(ObjJobPosition.Category), 0, ObjJobPosition.Category)
            If CType(ddlCategory.SelectedValue, Integer) > 0 Then
                BindListBox(CType(ddlCategory.SelectedValue, Integer))
                For Each jp2menu As JobPositionToMenu In arrJP2Menu
                    For Each li As ListItem In lstBoxMenu.Items
                        If CType(jp2menu.JobPositionMenu.ID, Integer) = li.Value Then
                            li.Selected = True
                        End If
                    Next
                Next
            End If
        End If
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        'Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtCode.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.[Partial], txtCode.Text.Trim()))
        End If
        If txtDescription.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(JobPosition), "Description", MatchType.[Partial], txtDescription.Text.Trim()))
        End If
        If ddlCategory.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(JobPosition), "Category", MatchType.Exact, CType(ddlCategory.SelectedValue, Integer)))
        End If
        arrJobPositionList = JobPosFacade.RetrieveByCriteria(criterias, idxPage + 1, dgJobPosition.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If arrJobPositionList.Count > 0 Then
            dgJobPosition.CurrentPageIndex = idxPage
            dgJobPosition.DataSource = arrJobPositionList
            dgJobPosition.VirtualItemCount = totalRow
            dgJobPosition.DataBind()
        Else
            MessageBox.Show(SR.DataNotFound("Job Position"))
        End If

    End Sub
    Private Sub BindListBox(ByVal iCategory As Integer)
        Dim arrJobPositionMenuList As New ArrayList
        Dim objFacade As JobPositionMenuFacade = New JobPositionMenuFacade(User)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionMenu), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        arrJobPositionMenuList.Clear()
        If iCategory = 0 Then
            MenuControlVisible(False)
        Else
            MenuControlVisible(True)
        End If
        criterias.opAnd(New Criteria(GetType(JobPositionMenu), "Category", MatchType.Exact, iCategory))
        arrJobPositionMenuList = objFacade.Retrieve(criterias)

        sessHelper.SetSession("arrJobPosMenu", arrJobPositionMenuList)
        lstBoxMenu.DataSource = sessHelper.GetSession("arrJobPosMenu")
        lstBoxMenu.DataTextField = "Name"
        lstBoxMenu.DataValueField = "ID"
        lstBoxMenu.DataBind()

    End Sub

    Private Sub MenuControlVisible(ByVal mBol As Boolean)
        lblMenu.Visible = mBol
        lstBoxMenu.Visible = mBol
        'cblMenu.Visible = mBol
    End Sub
    Private Sub EnableControl(ByVal isEnable As Boolean)
        If CType(sessHelper.GetSession("Status"), String) = "Update" And isEnable Then
            txtCode.ReadOnly = True
        Else
            txtCode.ReadOnly = Not isEnable
        End If
        txtDescription.ReadOnly = Not isEnable
    End Sub
    Private Sub Initialize()
        txtCode.Text = ""
        txtDescription.Text = ""
        BindDdlCategory()
        MenuControlVisible(False)
        btnSave.Enabled = True
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        sessHelper.SetSession("Status", "Insert")
    End Sub
    Private Sub BindDdlCategory()
        ddlCategory.Items.Clear()
        ddlCategory.Items.Add(New ListItem("Silahkan Pilih", 0))
        For Each li As CategoryPosition In EnumCategoryPosition.RetrieveCategoryPosition()
            ddlCategory.Items.Add(New ListItem(li.NameStatus, li.ValStatus))
        Next
        ddlCategory.DataBind()
    End Sub
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ENHGeneralPosisiJabatanView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MASTER - Posisi Jabatan")
        End If
        _edit = SecurityProvider.Authorize(context.User, SR.ENHGeneralPosisiJabatanUbah_Privilege)
        _view = SecurityProvider.Authorize(context.User, SR.ENHGeneralPosisiJabatanView_Privilege)
    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
            EnableControl(True)
            BindDataGrid(0)
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim strMessage As String
        strMessage = String.Empty
        dgJobPosition.CurrentPageIndex = 0
        If txtCode.Text.Trim() = "" Then
            strMessage = "Posisi tidak boleh kosong \n"
        End If
        If txtDescription.Text.Trim() = "" Then
            strMessage += "Deskripsi tidak boleh kosong \n"
        End If

        If ddlCategory.SelectedIndex = 0 Then
            strMessage += "Pilih kategory posisi \n"
        End If

        If lstBoxMenu.SelectedValue = "" Then
            strMessage += "Pilih kategory menu"
        End If

        If strMessage <> String.Empty Then
            MessageBox.Show(strMessage)
            Exit Sub
        End If
        'Validate Description
        Dim IdEdit As Integer
        If CType(sessHelper.GetSession("Status"), String) = "Insert" Then
            IdEdit = 0
        Else
            IdEdit = CInt(sessHelper.GetSession("IDJobPosition"))
        End If

        If New JobPositionFacade(User).ValidateDesc(txtDescription.Text.Trim(), IdEdit) > 0 Then
            MessageBox.Show("Deskripsi sudah ada")
            Exit Sub
        End If

        If CType(sessHelper.GetSession("Status"), String) = "Insert" Then
            Dim oJ As JobPosition
            oJ = JobPosFacade.RetrieveNotActive(txtCode.Text)
            If Not IsNothing(oJ) Then
                sessHelper.SetSession("mode", "insert")
                sessHelper.SetSession("IDJobPosition", oJ.ID)
                sessHelper.SetSession("Status", "Update")
            End If
        End If

        If CType(sessHelper.GetSession("Status"), String) = "Insert" Then
            If JobPosFacade.IsJobPositionFound(txtCode.Text.Trim()) Then
                MessageBox.Show(SR.DataIsExist("Posisi Jabatan"))
                Return
            End If
            Dim ObjJobPosition As New JobPosition
            ObjJobPosition.Code = txtCode.Text.Trim()
            ObjJobPosition.Description = txtDescription.Text.Trim()
            ObjJobPosition.Category = CType(ddlCategory.SelectedValue, Integer)

            Dim arrListJobPositionMenu As New ArrayList
            Dim objJobPositionMenu As JobPositionMenu
            Dim li As ListItem
            For iCount As Integer = 0 To lstBoxMenu.Items.Count - 1
                If lstBoxMenu.Items(iCount).Selected Then
                    objJobPositionMenu = CType(sessHelper.GetSession("arrJobPosMenu"), ArrayList)(iCount)
                    arrListJobPositionMenu.Add(objJobPositionMenu)
                End If
            Next

            Dim iInsert As Integer = JobPosFacade.Insert(ObjJobPosition, arrListJobPositionMenu)
            If iInsert = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                Initialize()
                EnableControl(True)
                BindDataGrid(0)
                MessageBox.Show(SR.SaveSuccess)
                'End If
            End If
        ElseIf CType(sessHelper.GetSession("Status"), String) = "Update" Then
            Dim idJobPos As Integer = CInt(sessHelper.GetSession("IDJobPosition"))
            Dim ObjJobPosition As JobPosition = JobPosFacade.Retrieve(idJobPos)

            ObjJobPosition.Code = txtCode.Text.Trim()
            ObjJobPosition.Description = txtDescription.Text.Trim()
            ObjJobPosition.RowStatus = CType(DBRowStatus.Active, Short)
            ObjJobPosition.Category = CType(ddlCategory.SelectedValue, Integer)

            Dim arrListJobPositionMenu As New ArrayList
            Dim objJobPositionMenu As JobPositionMenu
            Dim li As ListItem
            For iCount As Integer = 0 To lstBoxMenu.Items.Count - 1
                If lstBoxMenu.Items(iCount).Selected Then
                    objJobPositionMenu = CType(sessHelper.GetSession("arrJobPosMenu"), ArrayList)(iCount)
                    arrListJobPositionMenu.Add(objJobPositionMenu)
                End If
            Next

            'Dim i As Integer = JobPosFacade.Update(ObjJobPosition)
            Dim iUpdate As Integer = JobPosFacade.Update(ObjJobPosition, arrListJobPositionMenu)
            If iUpdate = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                Initialize()
                EnableControl(True)
                BindDataGrid(0)
                If CType(sessHelper.GetSession("mode"), String) = "insert" Then
                    MessageBox.Show(SR.SaveSuccess)
                    sessHelper.SetSession("mode", "")
                Else
                    MessageBox.Show(SR.UpdateSucces)
                End If
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Initialize()
        EnableControl(True)
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
    End Sub
    Private Sub dgJobPosition_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgJobPosition.ItemDataBound
        If dgJobPosition.CurrentPageIndex >= 0 Then
            If Not e.Item.DataItem Is Nothing Then
                objCategory = CType(arrJobPositionList(e.Item.ItemIndex), JobPosition)
                e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgJobPosition.CurrentPageIndex * dgJobPosition.PageSize)
                e.Item.Cells(4).Text = KTB.DNet.Domain.EnumCategoryPosition.GetStringValue(objCategory.Category)
                Dim lView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
                Dim lEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                Dim lDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lView.Visible = _view
                lEdit.Visible = _edit
                lDelete.Visible = _edit
            End If
        End If

    End Sub
    Private Sub dgJobPosition_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgJobPosition.ItemCommand
        If e.CommandName = "View" Then
            ViewJobPosition(CInt(e.Item.Cells(0).Text))
            EnableControl(False)
            btnSave.Enabled = False
        ElseIf e.CommandName = "Edit" Then
            sessHelper.SetSession("IDJobPosition", CInt(e.Item.Cells(0).Text))
            sessHelper.SetSession("Status", "Update")
            ViewJobPosition(CInt(e.Item.Cells(0).Text))
            EnableControl(True)
            btnSave.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            Dim ObjJobPosition As JobPosition = JobPosFacade.Retrieve(CInt(e.Item.Cells(0).Text))
            Dim arrUserInfo As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(UserInfo), "JobPosition.ID", MatchType.Exact, CInt(e.Item.Cells(0).Text)))
            arrUserInfo = New UserInfoFacade(User).Retrieve(crit)

            If arrUserInfo.Count > 0 Then
                MessageBox.Show("Beberapa user memiliki posisi pekerjaan ini")
            Else
                Try
                    JobPosFacade.Delete(ObjJobPosition)
                    Initialize()
                    EnableControl(True)
                    BindDataGrid(0)
                    MessageBox.Show(SR.DeleteSucces)
                Catch ex As Exception
                    MessageBox.Show(SR.DeleteFail)
                End Try
            End If
        End If
    End Sub
    Private Sub dgJobPosition_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgJobPosition.SortCommand
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
        dgJobPosition.SelectedIndex = -1
        dgJobPosition.CurrentPageIndex = 0
        BindDataGrid(dgJobPosition.CurrentPageIndex)
    End Sub
    Private Sub dgJobPosition_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgJobPosition.PageIndexChanged
        dgJobPosition.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgJobPosition.CurrentPageIndex)
    End Sub
#End Region

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        BindListBox(CType(ddlCategory.SelectedValue, Integer))
        'BindDataGrid(0)

    End Sub
End Class
