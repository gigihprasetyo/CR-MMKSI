#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmAddUserOnGroup
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents dgMember As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgUser As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtUserID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOrganisasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPosition As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearchAssigned As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtUserIDAssigned As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOrganisasiAssigned As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerAssigned As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPositionAssigned As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private SessHelper As SessionHelper = New SessionHelper

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.AdminViewGroupUserList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Member Group")
        End If
    End Sub

    Dim bCekEditGroupPriv As Boolean = SecurityProvider.Authorize(context.User, SR.AdminEditGroupUser_Privilege)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then
            isiDDl()
            Dim objGroup As UserGroup = New UserGroupFacade(User).Retrieve(CInt(Request.QueryString("id")))
            lblCode.Text = objGroup.Code
            lblDescription.Text = objGroup.Description
            ViewState("CurrentSortColumnMember") = "UserInfo.Dealer.DealerCode"
            ViewState("CurrentSortDirectMember") = Sort.SortDirection.ASC
            ViewState("CurrentSortColumnUser") = "Dealer.DealerCode"
            ViewState("CurrentSortDirectUser") = Sort.SortDirection.ASC
            BindGridMember(0)

            dgUser.DataSource = New ArrayList
            dgUser.DataBind()
        End If

    End Sub

    Private Sub isiDDl()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        ddlPosition.DataSource = New JobPositionFacade(User).RetrieveActiveList(criterias, "Description", Sort.SortDirection.ASC)
        ddlPosition.DataTextField = "Description"
        ddlPosition.DataValueField = "ID"
        ddlPosition.DataBind()
        ddlPosition.Items.Insert(0, New ListItem("Pilih Posisi/Jabatan", 0))
        ddlPosition.SelectedIndex = 0


        ddlPositionAssigned.DataSource = New JobPositionFacade(User).RetrieveActiveList(criterias, "Description", Sort.SortDirection.ASC)
        ddlPositionAssigned.DataTextField = "Description"
        ddlPositionAssigned.DataValueField = "ID"
        ddlPositionAssigned.DataBind()
        ddlPositionAssigned.Items.Insert(0, New ListItem("Pilih Posisi/Jabatan", 0))
        ddlPositionAssigned.SelectedIndex = 0


    End Sub

    Private Sub BindGridMember(ByVal idxPage As Integer)

        Dim arlUserInfoToInclude As ArrayList



        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserGroupMember), "UserGroup.ID", MatchType.Exact, Request.QueryString("id")))

        criterias.opAnd(New Criteria(GetType(UserGroupMember), "UserInfo.UserName", MatchType.[Partial], txtUserIDAssigned.Text))

        If ddlPositionAssigned.SelectedValue <> "0" Then
            criterias.opAnd(New Criteria(GetType(UserGroupMember), "UserInfo.JobPosition.ID", MatchType.Exact, ddlPositionAssigned.SelectedValue))
        End If

        If txtOrganisasiAssigned.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(UserGroupMember), "UserInfo.Dealer.DealerCode", MatchType.InSet, "('" & txtOrganisasiAssigned.Text.Replace(";", "','") & "')"))
        End If


        Dim arrList As ArrayList = New UserGroupMemberFacade(User).RetrieveActiveList(idxPage + 1, dgMember.PageSize, totalRow, ViewState("CurrentSortColumnMember"), ViewState("CurrentSortDirectMember"), criterias)
        SessHelper.SetSession("arrMember", arrList)
        dgMember.CurrentPageIndex = idxPage
        dgMember.DataSource = arrList
        dgMember.VirtualItemCount = totalRow
        dgMember.DataBind()

    End Sub

    Private Sub BindGridUser(ByVal idxPage As Integer)

        'Get ID not included
        'Dim IDnotIncluded As String = ""
        'Dim criteriaMember As New CriteriaComposite(New Criteria(GetType(UserGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criteriaMember.opAnd(New Criteria(GetType(UserGroupMember), "UserGroup.ID", MatchType.Exact, Request.QueryString("id")))
        'Dim arrMember As ArrayList = New UserGroupMemberFacade(User).Retrieve(criteriaMember)


        'For Each itemMember As UserGroupMember In arrMember
        '    IDnotIncluded = IDnotIncluded & itemMember.UserInfo.ID.ToString & ","
        'Next


        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserInfo), "UserStatus", MatchType.Exact, CByte(EnumUserStatus.UserStatus.Aktif)))
        criterias.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.[Partial], txtUserID.Text))

        If txtOrganisasi.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(UserInfo), "Dealer.DealerCode", MatchType.InSet, "('" & txtOrganisasi.Text.Replace(";", "','") & "')"))
        End If

        'If IDnotIncluded <> "" Then
        '    IDnotIncluded = Left(IDnotIncluded, IDnotIncluded.Length - 1)
        'Framework limitation SQLQuery To Long
            criterias.opAnd(New Criteria(GetType(UserInfo), "ID", MatchType.NotInSet, "select userid from usergroupmember where usergroupid=" & Request.QueryString("id") & "  "))
        'End If

        If ddlPosition.SelectedValue <> "0" Then
            criterias.opAnd(New Criteria(GetType(UserInfo), "JobPosition.ID", MatchType.Exact, ddlPosition.SelectedValue))
        End If

        Dim arrList As ArrayList = New UserInfoFacade(User).RetrieveActiveList(idxPage + 1, dgUser.PageSize, totalRow, ViewState("CurrentSortColumnUser"), ViewState("CurrentSortDirectUser"), criterias)

        If (idxPage * dgUser.PageSize) >= totalRow Then
            If idxPage = 0 Then
                dgUser.DataSource = New ArrayList
                dgUser.VirtualItemCount = 0
                dgUser.DataBind()
                Exit Sub
            Else
                idxPage = arrList.Count \ dgUser.PageSize
                arrList = New UserInfoFacade(User).RetrieveActiveList(idxPage + 1, dgUser.PageSize, totalRow, ViewState("CurrentSortColumnUser"), ViewState("CurrentSortDirectUser"), criterias)
            End If
        End If

        SessHelper.SetSession("arrUser", arrList)

        dgUser.CurrentPageIndex = idxPage
        dgUser.DataSource = arrList
        dgUser.VirtualItemCount = totalRow
        dgUser.DataBind()

    End Sub




    Private Sub dgMember_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgMember.SortCommand
        If e.SortExpression = ViewState("CurrentSortColumnMember") Then
            If ViewState("CurrentSortDirectMember") = Sort.SortDirection.ASC Then
                ViewState("CurrentSortDirectMember") = Sort.SortDirection.DESC
            Else
                ViewState("CurrentSortDirectMember") = Sort.SortDirection.ASC
            End If
        End If
        ViewState("CurrentSortColumnMember") = e.SortExpression
        dgMember.SelectedIndex = -1

        BindGridMember(dgMember.CurrentPageIndex)

    End Sub

    Private Sub dgUser_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgUser.SortCommand

        If e.SortExpression = ViewState("CurrentSortColumnUser") Then
            If ViewState("CurrentSortDirectUser") = Sort.SortDirection.ASC Then
                ViewState("CurrentSortDirectUser") = Sort.SortDirection.DESC
            Else
                ViewState("CurrentSortDirectUser") = Sort.SortDirection.ASC
            End If
        End If
        ViewState("CurrentSortColumnUser") = e.SortExpression
        dgUser.SelectedIndex = -1

        BindGridUser(dgUser.CurrentPageIndex)

    End Sub

    Private Sub dgMember_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgMember.PageIndexChanged
        dgMember.CurrentPageIndex = e.NewPageIndex
        BindGridMember(dgMember.CurrentPageIndex)

    End Sub

    Private Sub dgUser_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgUser.PageIndexChanged
        dgUser.CurrentPageIndex = e.NewPageIndex
        BindGridUser(dgUser.CurrentPageIndex)

    End Sub

    Private Sub dgMember_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMember.ItemCommand
        If e.CommandName = "Delete" Then
            Dim facade As UserGroupMemberFacade = New UserGroupMemberFacade(User)
            facade.DeleteFromDB(SessHelper.GetSession("arrMember")(e.Item.ItemIndex))

            If dgMember.Items.Count = 1 Then
                If dgMember.CurrentPageIndex = 0 Then
                    dgMember.DataSource = New ArrayList
                    dgMember.VirtualItemCount = 0
                    dgMember.DataBind()
                Else
                    BindGridMember(dgMember.CurrentPageIndex - 1)
                End If
            Else
                BindGridMember(dgMember.CurrentPageIndex)
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim objGroup As UserGroup = New UserGroupFacade(User).Retrieve(CInt(Request.QueryString("id")))
        Dim facade As UserGroupMemberFacade = New UserGroupMemberFacade(User)
        Dim counter As Integer = 0
        For Each item As DataGridItem In dgUser.Items
            Dim chk As CheckBox = item.FindControl("chk")
            If chk.Checked Then
                Dim objToInsert As UserGroupMember = New UserGroupMember
                objToInsert.UserGroup = objGroup
                objToInsert.UserInfo = SessHelper.GetSession("arrUser")(counter)
                Dim result As Integer = facade.Insert(objToInsert)
            End If
            counter += 1
        Next
        BindGridMember(dgMember.CurrentPageIndex)
        BindGridUser(dgUser.CurrentPageIndex)
        MessageBox.Show("Data Sudah Disimpan")
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindGridUser(0)
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmUserGroup.aspx?isBack=true")
        'Response.Redirect("FrmUserGroup.aspx")
    End Sub

    Private Sub Button1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Load

    End Sub

    Private Sub btnSearchAssigned_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchAssigned.Click
        BindGridMember(0)
    End Sub

    Private Sub dgMember_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgMember.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            lblNo.Text = (dgMember.CurrentPageIndex * dgMember.PageSize) + e.Item.ItemIndex + 1

            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("Linkbutton1"), LinkButton)
            lbtnDelete.Visible = bCekEditGroupPriv
        End If
    End Sub

    Private Sub dgUser_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUser.ItemDataBound

    End Sub
End Class
