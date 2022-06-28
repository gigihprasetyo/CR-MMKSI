Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.Helper
Imports Microsoft.Practices.EnterpriseLibrary.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security
Imports Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
Imports Microsoft.Practices.EnterpriseLibrary.Security.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database.Authentication.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database
Imports System.Text
Imports KTB.DNet.Security
Imports System.Security.Principal
Public Class FrmGroupDetail
    Inherits System.Web.UI.Page
    Private objUser As UserInfo
    Protected WithEvents LblID As System.Web.UI.WebControls.Label
    Protected WithEvents LblLogin As System.Web.UI.WebControls.Label
    Protected WithEvents lblerror As System.Web.UI.WebControls.Label
    Private sessionHelper As New sessionHelper
    Private ObjUserID As Integer
    Dim RoleNew As ArrayList = New ArrayList
    Private sessDealer As Dealer

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LblKodeOrg As System.Web.UI.WebControls.Label
    Protected WithEvents LblNamaOrg As System.Web.UI.WebControls.Label
    Protected WithEvents dtgUserRole1 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                               ByVal SortDirection As Integer)
        '-- Sort arraylist

        If SortColumn.Trim <> "" Then
            Dim isASC As Boolean = (SortDirection = Sort.SortDirection.ASC)  '-- Is sorted ascending?
            Dim objListComparer As IComparer = New ListComparer(isASC, SortColumn)
            pCompletelist.Sort(objListComparer)
        End If

    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sessDealer = sessionHelper.GetSession("DEALER")
        ActivateUserPrivilege()
        If Not IsPostBack Then
            If IsNothing(Request.QueryString("UserID")) = False Then
                GetUserData()
            Else
                Response.Redirect("frmUserList.aspx?Reread=True")
            End If
        End If
    End Sub
    Private Sub ActivateUserPrivilege()
        If Not IsNothing(sessDealer) Then
            If sessDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If Not SecurityProvider.Authorize(Context.User, SR.AdminUpdateRoleKTB_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Edit User Role")
                End If
                Return
            ElseIf sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If Not SecurityProvider.Authorize(Context.User, SR.AdminUpdateRoleDealer_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Edit User Role")
                End If
                Return
            End If
        End If
        Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Edit User Role")
    End Sub
    Sub GetUserData()
        objUser = New KTB.DNet.BusinessFacade.UserManagement.UserInfoFacade(User).Retrieve(CInt(Request.QueryString("UserID")))
        sessionHelper.SetSession("UserEditRole", objUser)
        FillUserHeader(objUser)
        BindUserRole(objUser)
    End Sub
    Sub FillUserHeader(ByVal Obj As UserInfo)
        If IsNothing(Obj) = False Then
            Me.LblKodeOrg.Text = Obj.Dealer.DealerCode
            Me.LblNamaOrg.Text = Obj.Dealer.DealerName
            Me.LblID.Text = Obj.ID
            Me.LblLogin.Text = Obj.UserName
        End If
    End Sub
    Private Function GetUserRole(ByVal nID As Integer) As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserRole), "UserInfo.ID", MatchType.Exact, nID))
        Return New UserRoleFacade(User).Retrieve(criterias)
    End Function

    Sub BindUserRoles(ByVal Obj As UserInfo)
        Dim coll As ArrayList = CType(sessionHelper.GetSession("RoleList"), ArrayList)
        SortListControl(coll, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))

        Me.dtgUserRole1.DataSource = coll
       
        Me.dtgUserRole1.DataBind()
        'Dim listOfCheckedData As ArrayList = New ArrayList
        sessionHelper.RemoveSession("sesRoleOld")
        Dim arlUserRole As ArrayList = GetUserRole(Obj.ID)
        If arlUserRole.Count > 0 Then
            sessionHelper.SetSession("sesRoleOld", arlUserRole)
            Dim nLoop As Integer = 0
            For Each item As DataGridItem In dtgUserRole1.Items
                If nLoop >= arlUserRole.Count Then Exit For
                For i As Integer = 0 To arlUserRole.Count - 1
                    If CType(arlUserRole(i), UserRole).Role.ID = CType(item.Cells(0).Text, Integer) Then
                        Dim checkbox1 As CheckBox = item.FindControl("cbItem")
                        checkbox1.Checked = True
                        nLoop += 1
                    End If
                Next
            Next
        End If
    End Sub


    Sub BindUserRole(ByVal Obj As UserInfo)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Role), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Role), "Dealer.ID", MatchType.Exact, objUser.Dealer.ID))
        Dim coll As ArrayList = New RoleFacade(User).Retrieve(criterias)
        sessionHelper.SetSession("RoleList", coll)
        Me.dtgUserRole1.DataSource = coll
        Me.dtgUserRole1.VirtualItemCount = totalRow

        Me.dtgUserRole1.DataBind()
        'Dim listOfCheckedData As ArrayList = New ArrayList
        sessionHelper.RemoveSession("sesRoleOld")
        Dim arlUserRole As ArrayList = GetUserRole(Obj.ID)
        If arlUserRole.Count > 0 Then
            sessionHelper.SetSession("sesRoleOld", arlUserRole)
            Dim nLoop As Integer = 0
            For Each item As DataGridItem In dtgUserRole1.Items
                If nLoop >= arlUserRole.Count Then Exit For
                For i As Integer = 0 To arlUserRole.Count - 1
                    If CType(arlUserRole(i), UserRole).Role.ID = CType(item.Cells(0).Text, Integer) Then
                        Dim checkbox1 As CheckBox = item.FindControl("cbItem")
                        checkbox1.Checked = True
                        nLoop += 1
                    End If
                Next
            Next
        End If
    End Sub


    Private Function CheckUserRoleExist(ByVal RoleID As Integer, ByVal UserID As Integer)
        Dim ExistingRole As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserRole), "UserInfo", MatchType.Exact, UserID))
        criterias.opAnd(New Criteria(GetType(UserRole), "Role", MatchType.Exact, RoleID))
        Dim RoleList As New UserRoleFacade(User)

        ExistingRole = RoleList.Retrieve(criterias)
        Return ExistingRole

    End Function

    Private Function CheckUserRoleExist1(ByVal RoleID As Integer, ByVal UserID As Integer)
        Dim ExistingRole As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserRole), "UserInfo", MatchType.Exact, UserID))
        criterias.opAnd(New Criteria(GetType(UserRole), "Role", MatchType.Exact, RoleID))
        Dim RoleList As New UserRoleFacade(User)

        ExistingRole = RoleList.Retrieve(criterias)
        Return ExistingRole

    End Function
    Sub CheckUserRole(ByVal ObjUser As UserInfo)
        CreateCheckedData(ObjUser)
        For Each item As DataGridItem In dtgUserRole1.Items
            Dim checkbox As checkbox = item.FindControl("cbItem")
        Next
    End Sub
    Private Sub CreateCheckedData(ByVal objRole As UserInfo)
        Dim listOfCheckedData As ArrayList = New ArrayList
        For Each item As UserRole In objRole.UserRoles
            listOfCheckedData.Add(CStr(item.Role.ID))
        Next
        sessionHelper.SetSession("checkedData", listOfCheckedData)
    End Sub
    Private Function CreateUserRole(ByVal roleID As Integer) As UserRole
        Dim objUserRole As UserRole = New UserRole
        objUserRole.Role = New RoleFacade(User).Retrieve(roleID)
        objUserRole.UserInfo = CType(Session("UserEditRole"), UserInfo)
        objUserRole.RowStatus = 0
        Return objUserRole
    End Function
    Private Sub MergeRoleUpdate(ByVal roleID As Integer)
        Dim bFound As Boolean = False
        Dim RoleArray As ArrayList = New ArrayList
        RoleNew = CType(Session("sesRoleOld"), ArrayList)
        'If IsNothing(RoleNew) = False Then
        '    For Each objUserRole As userRole In RoleNew
        '        If objUserRole.UserInfo.ID = CType(Session("UserEditRole"), UserInfo).ID AndAlso objUserRole.Role.ID = roleID Then
        '            'objUserRole.RowStatus = DBRowStatus.Active
        '            bFound = True
        '            Exit For
        '        End If
        '    Next
        'End If
        If Not bFound Then
            Dim userRole As userRole = CreateUserRole(roleID)
            If IsNothing(RoleNew) = True Then
                RoleArray.Add(userRole)
            Else
                RoleNew.Add(userRole)
            End If
        End If
        If IsNothing(RoleNew) = False Then
            sessionHelper.SetSession("sesRoleOld", RoleNew)
        Else
            sessionHelper.SetSession("sesRoleOld", RoleArray)
        End If
    End Sub
    Private Sub MergeCheckBoxSelected()
        RoleNew = CType(Session("sesRoleOld"), ArrayList)
        If IsNothing(RoleNew) = False Then
            For Each objUserRole As UserRole In RoleNew
                objUserRole.RowStatus = DBRowStatus.Deleted
            Next
            sessionHelper.SetSession("sesRoleOld", RoleNew)
        End If

        For Each item As DataGridItem In dtgUserRole1.Items
            Dim checkbox1 As CheckBox = item.FindControl("cbItem")
            If checkbox1.Checked = True Then
                MergeRoleUpdate(CType(item.Cells(0).Text, Integer))
            End If
        Next

    End Sub
    Private Function CheckUserRoleExist(ByVal Criterias As ICriteria)
        Dim RoleList As ArrayList
        Dim ObjUserRole As New UserRoleFacade(User)
        RoleList = ObjUserRole.Retrieve(Criterias)
        Return RoleList
    End Function
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        Dim ObjUserAddRole As UserInfo = New KTB.DNet.BusinessFacade.UserManagement.UserInfoFacade(User).Retrieve(CType(Me.LblID.Text, Integer))
        Dim ObjRoleDelete As New KTB.DNet.BusinessFacade.UserManagement.UserRoleFacade(User)
        Dim ObjUserRole As UserRole = New KTB.DNet.BusinessFacade.UserManagement.UserRoleFacade(User).Retrieve(CType(Me.LblID.Text, Integer))
        Dim Respon As Integer
        MergeCheckBoxSelected()
        If IsNothing(Session("sesRoleOld")) = False Then
            Dim nResult As Integer = New UserRoleFacade(User).UpdateUserRole(CType(Session("sesRoleOld"), ArrayList))
            If nResult = 1 Then
                MessageBox.Show(SR.SaveSuccess)
                Me.lblerror.Text = "Tekan tombol kembali untuk kembali ke halaman daftar user"
                Me.lblerror.Font.Bold = True
            Else
                MessageBox.Show(SR.SaveFail)
                Me.lblerror.Text = "penambahan role gagal, mohon hubungi administrator / web master"
                Me.lblerror.Font.Bold = True
            End If
        Else
            Me.lblerror.Text = "Tidak ada data role yang perlu disimpan silakan tekan tombol kembali!"
            Me.lblerror.Font.Bold = True
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Response.Redirect("frmUserList.aspx?Reread=True")
    End Sub

    Private Sub dtgUserRole1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgUserRole1.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        'BindDatagrid()
        objUser = New KTB.DNet.BusinessFacade.UserManagement.UserInfoFacade(User).Retrieve(CInt(Request.QueryString("UserID")))

        BindUserRoles(objUser) '--
    End Sub

    Private Sub dtgUserRole1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUserRole1.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgUserRole1.CurrentPageIndex * dtgUserRole1.PageSize)
        End If
    End Sub
End Class
