#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Tools
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmAccessRoles
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents dgMember As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtOrganisasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgUser As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtOrganisasiAssigned As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerAssigned As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPositionAssigned As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearchAssigned As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents txtUserName As System.Web.UI.WebControls.TextBox

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            Dim objQry As BCPQuery = New BCPQueryFacade(User).Retrieve(CInt(Request.QueryString("id")))
            lblCode.Text = objQry.Title
            lblDescription.Text = objQry.FlName
            ViewState("CurrentSortColumnMember") = "UserRole.UserInfo.Dealer.DealerCode"
            ViewState("CurrentSortDirectMember") = Sort.SortDirection.ASC
            ViewState("CurrentSortColumnUser") = "UserInfo.Dealer.DealerCode"
            ViewState("CurrentSortDirectUser") = Sort.SortDirection.ASC
            BindGridBCPRoles(0)

            dgUser.DataSource = New ArrayList
            dgUser.DataBind()
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmReportList.aspx?isBack=true")
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

        BindGridBCPRoles(dgMember.CurrentPageIndex)
    End Sub

    Private Sub dgMember_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgMember.PageIndexChanged
        dgMember.CurrentPageIndex = e.NewPageIndex
        BindGridBCPRoles(dgMember.CurrentPageIndex)
    End Sub

    Private Sub dgMember_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMember.ItemCommand
        If e.CommandName = "Delete" Then
            Dim facade As BCPRolesFacade = New BCPRolesFacade(User)
            facade.DeleteFromDB(CType(SessHelper.GetSession("arrMember")(e.Item.ItemIndex), BCPRoles))

            If dgMember.Items.Count = 1 Then
                If dgMember.CurrentPageIndex = 0 Then
                    dgMember.DataSource = New ArrayList
                    dgMember.VirtualItemCount = 0
                    dgMember.DataBind()
                Else
                    BindGridBCPRoles(dgMember.CurrentPageIndex - 1)
                End If
            Else
                BindGridBCPRoles(dgMember.CurrentPageIndex)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindGridUserRole(0)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim objQry As BCPQuery = New BCPQueryFacade(User).Retrieve(CInt(Request.QueryString("id")))
        Dim facade As BCPRolesFacade = New BCPRolesFacade(User)
        Dim counter As Integer = 0
        For Each item As DataGridItem In dgUser.Items
            Dim chk As CheckBox = item.FindControl("chk")
            If chk.Checked Then
                Dim objToInsert As BCPRoles = New BCPRoles
                objToInsert.BCPQuery = objQry
                objToInsert.UserRole = SessHelper.GetSession("arrUser")(counter)
                Dim result As Integer = facade.Insert(objToInsert)
            End If
            counter += 1
        Next
        BindGridBCPRoles(dgMember.CurrentPageIndex)
        BindGridUserRole(dgUser.CurrentPageIndex)
        MessageBox.Show("Data Sudah Disimpan")
    End Sub

#Region "Custom"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.AdminViewGroupUserList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Member Group")
        End If
    End Sub

    Private Sub BindGridBCPRoles(ByVal idxPage As Integer)

        Dim arlUserInfoToInclude As ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BCPRoles), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BCPRoles), "BCPQuery.ID", MatchType.Exact, Request.QueryString("id")))

        Dim arrList As ArrayList = New BCPRolesFacade(User).RetrieveActiveList(criterias, idxPage + 1, dgMember.PageSize, totalRow, ViewState("CurrentSortColumnMember"), ViewState("CurrentSortDirectMember"))

        SessHelper.SetSession("arrMember", arrList)
        dgMember.CurrentPageIndex = idxPage
        dgMember.DataSource = arrList
        dgMember.VirtualItemCount = totalRow
        dgMember.DataBind()

    End Sub

    Private Sub BindGridUserRole(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtOrganisasi.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(UserRole), "UserInfo.Dealer.DealerCode", MatchType.InSet, "('" & txtOrganisasi.Text.Replace(";", "','") & "')"))
        End If
        If txtUserName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(UserRole), "UserInfo.UserName", MatchType.[Partial], txtUserName.Text.Trim))
        End If

        Dim arrList As ArrayList = New UserRoleFacade(User).RetrieveActiveList(criterias, idxPage + 1, dgUser.PageSize, totalRow, ViewState("CurrentSortColumnUser"), ViewState("CurrentSortDirectUser"))

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
#End Region

End Class
