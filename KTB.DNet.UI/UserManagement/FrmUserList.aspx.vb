#Region " Summary "
'--------------------------------------------'
'-- Program Code : FrmUserList.aspx        --'
'-- Program Name : Daftar User             --'
'-- Description  :                         --'
'--------------------------------------------'
'-- Programmer   : Agus Pirnadi            --'
'-- Start Date   : Dec 16 2005             --'
'-- Update By    :                         --'
'-- Last Update  : Jan 25 2007             --'
'--------------------------------------------'
'-- Copyright ? 2005 by Intimedia          --'
'--------------------------------------------'
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
 
Imports System.Security.Principal
Imports System.Web.Security
Imports System.Configuration
Imports System.Configuration.ConfigurationSettings
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Lib
Imports Microsoft.Practices.EnterpriseLibrary.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security
Imports Microsoft.Practices.EnterpriseLibrary.Security.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database.Authentication.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database
Imports Microsoft.Practices.EnterpriseLibrary.Security.Cryptography

#End Region
 

Public Class FrmUserList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblLogin As System.Web.UI.WebControls.Label
    Protected WithEvents txtLoginName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents ddlUserStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblFirst As System.Web.UI.WebControls.Label
    Protected WithEvents txtFirstName As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgUserList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblGroupName As System.Web.UI.WebControls.Label
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealers As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"
    Private sessHelp As SessionHelper = New SessionHelper
    Private _UserProfileFacade As New UserProfileFacade(User)
    Private confContext As ConfigurationContext
    Private dbAuthenticationProvider As DbAuthenticationProviderData
    Private userRoleMgr As UserRoleManager
    Private hashProvider As IHashProvider
#End Region

#Region " Custom Method "

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
            m.BlockName = "user-management"


            Try
                m.UserName = IIf(User.Identity.Name.Length > 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name).ToLower
            Catch ex As Exception
                m.UserName = "Wb-Usr"
            End Try
            m.Dealer = CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode.ToLower
            objSyslog.LogError(m)
        End If
    End Sub

    Private Sub Initialize()
        '-- Init objects

        Dim oDealer As Dealer = CType(Session("DEALER"), Dealer)

        '-- Display dealer info from login session "DEALER"
        If Not IsNothing(oDealer) Then
            lblDealerCode.Text = oDealer.DealerCode
            If Not IsNothing(oDealer.DealerGroup) Then
                lblGroupName.Text = oDealer.DealerGroup.GroupName
            End If
            lblDealerName.Text = oDealer.DealerName
        End If

        '-- Clear text fields
        txtLoginName.Text = String.Empty
        txtFirstName.Text = String.Empty

        '-- Bind "Status User" dropdownlist
        ddlUserStatus.Items.Add(New ListItem("Pilih", -1))
        ddlUserStatus.Items.Add(New ListItem("Baru", 0))
        ddlUserStatus.Items.Add(New ListItem("Aktif", 1))
        ddlUserStatus.Items.Add(New ListItem("Non-aktif", 2))

        '-- Display grid column headers
        dgUserList.DataSource = New ArrayList
        dgUserList.DataBind()

        '-- Popup dealer list
        lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"

    End Sub

    Private Sub SaveCriteria()
        '-- Save selection criteria

        '-- Save selection criteria for later restore
        Dim crits As Hashtable = New Hashtable
        crits.Add("DealerCode", lblDealerCode.Text)
        crits.Add("DealerName", lblDealerName.Text)
        crits.Add("LoginName", txtLoginName.Text)
        crits.Add("FirstName", txtFirstName.Text)
        crits.Add("UserStatus", ddlUserStatus.SelectedValue)
        crits.Add("DealerList", txtKodeDealer.Text)
        sessHelp.SetSession("FrmUserListCrits", crits)
    End Sub

    Private Sub ReadCriteria()
        '-- Read selection criteria

        '-- Init sorting column and sort direction defaults
        ViewState("currSortColumn") = ""
        ViewState("currSortDirection") = Sort.SortDirection.ASC

        '-- Restore selection criteria
        Dim crits As Hashtable
        crits = CType(sessHelp.GetSession("FrmUserListCrits"), Hashtable)
        If Not IsNothing(crits) Then
            lblDealerCode.Text = CStr(crits.Item("DealerCode"))
            lblDealerName.Text = CStr(crits.Item("DealerName"))
            txtLoginName.Text = CStr(crits.Item("LoginName"))
            txtFirstName.Text = CStr(crits.Item("FirstName"))
            ddlUserStatus.SelectedValue = CStr(crits.Item("UserStatus"))
            txtKodeDealer.Text = CStr(crits.Item("DealerList"))
        End If

    End Sub

    Private Sub ReadData()
        '-- Read all data selected

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Include members of its dealer group
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim sUserDealers As String = DealerFacade.GenerateDealerCodeSelection(objDealer, User)
        If Not IsNothing(objDealer) AndAlso objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(UserInfo), "Dealer.DealerCode", MatchType.InSet, sUserDealers))
        End If

        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(UserInfo), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        '-- Nama login
        If txtLoginName.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.[Partial], txtLoginName.Text.Trim()))
        End If

        '-- Nama depan
        If txtFirstName.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(UserInfo), "FirstName", MatchType.[Partial], txtFirstName.Text.Trim()))
        End If

        '-- Status user
        If ddlUserStatus.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(UserInfo), "UserStatus", MatchType.Exact, ddlUserStatus.SelectedValue))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(UserInfo), "UserName", Sort.SortDirection.ASC))  '-- Nama login

        '-- Retrieve recordset
        Dim UserList As ArrayList = New UserInfoFacade(User).Retrieve(criterias, sortColl)

        '-- Store UserList into session for later use
        sessHelp.SetSession("UserList", UserList)

        If UserList.Count = 0 And IsPostBack Then
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        '-- Bind page-i

        '-- Read UserList from session
        Dim UserList As ArrayList = CType(sessHelp.GetSession("UserList"), ArrayList)

        If UserList.Count <> 0 Then
            '-- Sort first
            SortListControl(UserList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            '-- Then paging
            Dim PagedList As ArrayList = ArrayListPager.DoPage(UserList, pageIndex, dgUserList.PageSize)
            dgUserList.DataSource = PagedList
            dgUserList.VirtualItemCount = UserList.Count()
            dgUserList.CurrentPageIndex = pageIndex
            dgUserList.DataBind()
        Else
            '-- Display datagrid header only
            dgUserList.DataSource = New ArrayList
            dgUserList.VirtualItemCount = 0
            dgUserList.CurrentPageIndex = 0
            dgUserList.DataBind()
        End If

    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)
        '-- Sort arraylist

        If SortColumn.Trim <> "" Then
            Dim isASC As Boolean = (SortDirection = Sort.SortDirection.ASC)  '-- Is sorted ascending?
            Dim objListComparer As IComparer = New ListComparer(isASC, SortColumn)
            pCompletelist.Sort(objListComparer)
        End If

    End Sub

    Private Sub ActivateUser(ByVal nID As Integer)
        '-- Activate user

        Dim oUser As UserInfo = New UserInfoFacade(User).Retrieve(nID)
        oUser.UserStatus = EnumUserStatus.UserStatus.Aktif  '-- User Aktif
        Dim nResult = New UserInfoFacade(User).Update(oUser)
    End Sub

    Private Sub InactivateUser(ByVal nID As Integer)
        '-- Inactivate user

        Dim oUser As UserInfo = New UserInfoFacade(User).Retrieve(nID)
        oUser.UserStatus = EnumUserStatus.UserStatus.Tidak_Aktif  '-- User Tidak Aktif
        Dim nResult = New UserInfoFacade(User).Update(oUser)
        If nResult <> -1 Then
            DeleteUserFromUserGroupMember(oUser.ID)
        End If
    End Sub

    Private Sub DeleteUserFromUserGroupMember(ByVal oUserID As Integer)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserGroupMember), "UserInfo.ID", MatchType.Exact, oUserID))

        Dim facade As UserGroupMemberFacade = New UserGroupMemberFacade(User)
        Dim oUserGroupMemberList As ArrayList = facade.Retrieve(criterias)
        If oUserGroupMemberList.Count > 0 Then
            For Each ugm As UserGroupMember In oUserGroupMemberList
                facade.DeleteFromDB(ugm)
            Next
        End If

    End Sub

    Private Sub DeleteUser(ByVal nID As Integer)
        '-- Delete user

        '-- Retrieve this user object
        Dim oUser As UserInfo = New UserInfoFacade(User).Retrieve(nID)

        '-- Physical deletion
        Try
            Dim nResult = New UserInfoFacade(User).DeleteFromDB(oUser)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try

    End Sub

    Private Sub UserPrivilege()
        '-- Set user privileges
        ''''Dim bResetPassword As Boolean = SecurityProvider.Authorize(Context.User, SR.SEResetPassword_Privilege)
        ''''sessHelp.SetSession("ResetPassword", bResetPassword)
        ''''Dim bActivation As Boolean = SecurityProvider.Authorize(Context.User, SR.SEActivatedUser_Privilege)
        ''''sessHelp.SetSession("Activation", bActivation)

        If Not IsNothing(sessHelp.GetSession("DEALER")) Then
            Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                '-- As KTB user
                If Not SecurityProvider.Authorize(Context.User, SR.AdminViewListOfUserKTB_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Daftar User")
                End If
                sessHelp.SetSession("UserEditable", SecurityProvider.Authorize(Context.User, SR.AdminUpdateUserKTB_Privilege))
                Return

            ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER OrElse objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                '-- As ordinal user
                If Not SecurityProvider.Authorize(Context.User, SR.AdminViewListOfUserDealer_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Daftar User")
                End If
                sessHelp.SetSession("UserEditable", SecurityProvider.Authorize(Context.User, SR.AdminUpdateUserDealer_Privilege))
                Return

            End If
        End If


        Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Daftar User")
    End Sub

    Private Function UnregisterUserProfile(ByVal UserID As Integer) As Boolean
        Dim retValue As Boolean = False
        Dim criteria As New CriteriaComposite(New criteria(GetType(UserProfile), "UserID", MatchType.Exact, UserID))
        Dim arrUserProfile As ArrayList = _UserProfileFacade.Retrieve(criteria)
        If arrUserProfile.Count > 0 Then
            Dim objUserProfile As UserProfile = arrUserProfile(0)
            objUserProfile.RegistrationStatus = EnumSE.RegistrationStatus.NotRegister
            objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.NotActive
            objUserProfile.ActivationCode = String.Empty
            objUserProfile.TempActivationCode = String.Empty
            objUserProfile.Bingo = Nothing
            Dim result As Integer = _UserProfileFacade.Update(objUserProfile)
            If Not result = -1 Then
                retValue = True
                DeleteUserFromUserGroupMember(UserID)
            Else
                retValue = False
            End If
        Else
            retValue = False
        End If
        Return retValue
    End Function

    Private Sub LogTosyslog2(ByVal message As String)
        Dim strLog As String = KTB.DNet.Lib.WebConfig.GetValue("EnableSyslog")
        If strLog.Trim.ToUpper = "Y" Then
            Dim objSyslog As SyslogParameter = New SyslogParameter(User)
            Dim m As KTB.DNet.Lib.SysLogXMLMessage = New KTB.DNet.Lib.SysLogXMLMessage
            m.Action = "Reset User Password"
            m.SubBlockName = "User Management"
            m.FullMessage = message
            m.ModuleName = "General"
            m.Pages = HttpContext.Current.Request.Url.LocalPath
            m.RemoteIPAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
            m.StatusResult = "Sukses"
            m.Status = KTB.DNet.[Lib].DNetLogFormatStatus.Direct
            m.BlockName = "Role"
            Try
                m.UserName = IIf(User.Identity.Name.Length >= 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name)
            Catch ex As Exception
                m.UserName = "Wb-Usr"
            End Try
            objSyslog.LogError(m)
        End If
    End Sub
#End Region

#Region " EventHandler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '-- Page loading

        If Not IsPostBack Then
            UserPrivilege()  '-- Set user privileges
            Initialize()     '-- Init
            ReadCriteria()   '-- Read selection criteria

            '-- Check if requested to re-read datagrid content
            '-- Any query string means re-reading
            If Request.QueryString.Count <> 0 Then
                ReadData()   '-- Read all data matching criteria
                BindPage(0)  '-- Bind page-1
            End If
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        '-- Search users

        SaveCriteria()  '-- Save selection criteria
        ReadData()      '-- Read all data matching criteria
        BindPage(0)     '-- Bind page-1

    End Sub

    Private Sub dgUserList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUserList.ItemDataBound
        '-- Handles data binding

        If e.Item.ItemIndex <> -1 Then

            e.Item.Cells(1).Text = (dgUserList.CurrentPageIndex * dgUserList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            Dim RowValue As UserInfo = CType(e.Item.DataItem, UserInfo)  '-- Current record
            'Dim ObjUserProfile As UserProfile = _UserProfileFacade.Retrieve(RowValue.UserProfile.ID)

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnActive As LinkButton = CType(e.Item.FindControl("lbtnActive"), LinkButton)
            Dim lbtnGroup As LinkButton = CType(e.Item.FindControl("lbtnGroup"), LinkButton)
            Dim lbtnInactive As LinkButton = CType(e.Item.FindControl("lbtnInactive"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lbtnRole As LinkButton = CType(e.Item.FindControl("lbtnRole"), LinkButton)
            Dim lbtnUnRegister As LinkButton = CType(e.Item.FindControl("lbtnUnRegister"), LinkButton)
            Dim lnkResetPwd As LinkButton = CType(e.Item.FindControl("lnkResetPwd"), LinkButton)
            Dim lbtnSetPassword As LinkButton = CType(e.Item.FindControl("lbtnSetPassword"), LinkButton)

            If Not RowValue.UserProfile Is Nothing Then
                'lbtnUnRegister.Visible = (RowValue.UserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active) And _
                '   (RowValue.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register) And SecurityProvider.Authorize(Context.User, SR.SEUnregisterUser_Privilege)
                lbtnUnRegister.Visible = RowValue.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register And SecurityProvider.Authorize(Context.User, SR.SEUnregisterUser_Privilege)

            ElseIf RowValue.UserProfile Is Nothing Then
                lbtnUnRegister.Visible = False
            End If

            Dim bUserEditable As Boolean = CType(sessHelp.GetSession("UserEditable"), Boolean)
            lbtnEdit.Visible = bUserEditable  '-- "Edit User" privilege

            Dim oDealer As Dealer = CType(Session("DEALER"), Dealer)  '-- Dealer of logged-in user

            If RowValue.UserStatus = EnumUserStatus.UserStatus.Baru Then
                '-- User Baru

                lbtnGroup.Visible = False
                lbtnInactive.Visible = False
                lbtnRole.Visible = False

                '-- Only dealer KTB may use "Activate" button
                If Not IsNothing(oDealer) Then
                    If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        lbtnActive.Visible = True
                    Else
                        lbtnActive.Visible = False
                    End If
                Else
                    lbtnActive.Visible = False
                End If

                '-- Only if the logged-in user is of the same dealer as this user then
                '-- he/she may delete this user unless he/she is a member of KTB dealer.
                '-- A member of KTB dealer may always delete this user.
                '--
                '''Dim bActivation As Boolean = CType(sessHelp.GetSession("Activation"), Boolean)
                '''CType(e.Item.FindControl("lbtnInactive"), LinkButton).Visible = bActivation
                '''CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = bActivation
                If Not IsNothing(oDealer) Then
                    If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        lbtnDelete.Visible = True
                    Else
                        If oDealer.DealerCode = RowValue.Dealer.DealerCode Then
                            '-- Same dealer
                            lbtnDelete.Visible = True
                        Else
                            '-- Different dealer
                            lbtnDelete.Visible = False
                            lbtnEdit.Visible = False
                        End If
                    End If
                Else
                    lbtnDelete.Visible = False
                    lbtnEdit.Visible = False
                End If

            ElseIf RowValue.UserStatus = EnumUserStatus.UserStatus.Tidak_Aktif Then
                '-- User non-aktif
                lbtnGroup.Visible = False
                lbtnInactive.Visible = False
                lbtnRole.Visible = False
                'Update heru
                lbtnDelete.Visible = False
                '-- Only if the logged-in user is of the same dealer as this user then
                '-- he/she may activate/delete this user unless he/she is a member of KTB dealer.
                '-- A member of KTB dealer may always activate/delete this user.
                '--
                If Not IsNothing(oDealer) Then
                    If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        lbtnActive.Visible = True
                        'lbtnDelete.Visible = True
                    Else
                        If oDealer.DealerCode = RowValue.Dealer.DealerCode Then
                            '-- Same dealer
                            lbtnActive.Visible = True
                            'lbtnDelete.Visible = True
                        Else
                            '-- Different dealer
                            lbtnActive.Visible = False
                            'lbtnDelete.Visible = False
                            lbtnEdit.Visible = False
                        End If
                    End If
                Else
                    lbtnActive.Visible = False
                    lbtnDelete.Visible = False
                    lbtnEdit.Visible = False
                End If

            ElseIf RowValue.UserStatus = EnumUserStatus.UserStatus.Aktif Then
                '-- User Aktif
                lbtnActive.Visible = False
                lbtnDelete.Visible = False

                '-- Only if the logged-in user is of the same dealer as this user then
                '-- he/she may inactivate this user unless he/she is a member of KTB dealer.
                '-- A member of KTB dealer may always inactivate this user.
                '--
                If Not IsNothing(oDealer) Then
                    If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        lbtnInactive.Visible = True
                        lbtnGroup.Visible = True
                        lbtnRole.Visible = True
                    Else
                        If oDealer.DealerCode = RowValue.Dealer.DealerCode Then
                            '-- Same dealer
                            lbtnInactive.Visible = True
                            lbtnGroup.Visible = True
                            lbtnRole.Visible = True
                        Else
                            '-- Different dealer
                            lbtnInactive.Visible = False
                            lbtnGroup.Visible = False
                            lbtnRole.Visible = False
                            lbtnEdit.Visible = False
                        End If
                    End If
                Else
                    lbtnInactive.Visible = False
                    lbtnGroup.Visible = False
                    lbtnRole.Visible = False
                    lbtnEdit.Visible = False
                End If
            End If

            If (e.Item.Cells(8).Text) = "01/01/1753 0:00:00" Then
                e.Item.Cells(8).Text = "NA"
            End If


            If Not e.Item.FindControl("lbtnActive") Is Nothing Then
                '-- Confirm activation
                CType(e.Item.FindControl("lbtnActive"), LinkButton).ToolTip = "Aktifkan"
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Aktifkan record ini?');")
            End If
            If Not e.Item.FindControl("lbtnInactive") Is Nothing Then
                '-- Confirm inactivation
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).ToolTip = "Non Aktifkan"
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Attributes.Add("OnClick", "return confirm('Non-Aktifkan record ini?');")
            End If
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                '-- Confirm deletion
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).ToolTip = "Hapus"
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Hapus record ini?');")
            End If
            If Not e.Item.FindControl("lbtnUnRegister") Is Nothing Then
                '-- Confirm UnRegister
                CType(e.Item.FindControl("lbtnUnRegister"), LinkButton).ToolTip = " Un-Register"
                CType(e.Item.FindControl("lbtnUnRegister"), LinkButton).Attributes.Add("OnClick", "return confirm('Anda yakin akan melakukan proses UnRegister user ini?');")
            End If
            If Not e.Item.FindControl("lnkResetPwd") Is Nothing Then
                '-- Confirm Reset Password
                CType(e.Item.FindControl("lnkResetPwd"), LinkButton).ToolTip = "Reset Password"
                Dim strUser As String = " User " & RowValue.UserName & ", Kode Dealer " & RowValue.Dealer.DealerCode
                CType(e.Item.FindControl("lnkResetPwd"), LinkButton).Attributes.Add("OnClick", "return confirm('Anda yakin akan melakukan proses Reset Password " & strUser & " ?');")
                ''''Dim bResetPassword As Boolean = CType(sessHelp.GetSession("ResetPassword"), Boolean)
                ''''CType(e.Item.FindControl("lnkResetPwd"), LinkButton).Visible = bResetPassword
            End If
            If Not e.Item.FindControl("lbtnSetPassword") Is Nothing Then
                '-- Confirm Reset Password
                'CType(e.Item.FindControl("lnkResetPwd"), LinkButton).ToolTip = "Reset Password"
                Dim strUser As String = " User " & RowValue.UserName & ", Kode Dealer " & RowValue.Dealer.DealerCode
                CType(e.Item.FindControl("lnkResetPwd"), LinkButton).Attributes.Add("OnClick", "return confirm('Anda yakin akan melakukan proses Reset Password 'test123' atas user " & strUser & " ?');")
                ''''Dim bResetPassword As Boolean = CType(sessHelp.GetSession("ResetPassword"), Boolean)
                ''''CType(e.Item.FindControl("lnkResetPwd"), LinkButton).Visible = bResetPassword
                Dim isAllow As Boolean = SecurityProvider.Authorize(Context.User, SR.general_admin_reset_password_test123_privilege)

                lbtnSetPassword.Visible = isAllow
            End If

        End If
    End Sub

    Private Function GenerateNewPassword() As String
        Dim _strPassword As String
        Dim randomGenerator As New RandomGenerator
        _strPassword = randomGenerator.GenarateRandom(8) 'Generete Alphanumeric
        Return _strPassword.ToLower
    End Function

    Public Function ResetPassword(ByVal objUser As UserInfo, Optional ByVal sPassword As String = "") As String
        Dim result As String = String.Empty
        confContext = CType(ConfigurationManager.GetCurrentContext(), ConfigurationContext)
        Dim securitySetting As SecuritySettings = CType(confContext.GetConfiguration(SecuritySettings.SectionName), SecuritySettings)
        dbAuthenticationProvider = CType(securitySetting.AuthenticationProviders(0), DbAuthenticationProviderData)
        userRoleMgr = New UserRoleManager(dbAuthenticationProvider.Database, confContext)
        Dim hashProviderFac As HashProviderFactory = New HashProviderFactory(confContext)
        hashProvider = hashProviderFac.CreateHashProvider(dbAuthenticationProvider.HashProvider)
        Dim _strPassword, _msgContent, _userName As String
        If Not IsNothing(objUser) Then
            If sPassword = "" Then
                _strPassword = GenerateNewPassword()
            Else
                _strPassword = sPassword
            End If
            result = "User Name : " & objUser.UserName & ", Org : " & objUser.Dealer.DealerCode & ", Password baru : " & _strPassword
            Dim pwd As Byte() = hashProvider.CreateHash(Encoding.Unicode.GetBytes(_strPassword))
            objUser.Password = pwd
            _userName = objUser.Dealer.ID.ToString.PadLeft(6, "0") & objUser.UserName
            Try
                userRoleMgr.ChangeUserPassword(_userName, pwd)
                'LogTosyslog("User " & User.Identity.Name & " Reset Password.")
                'old before 2007 05 15
                '_msgContent = enumSMS.GetContentMessage(enumSMS.ContentMessage.ResetPasswordSuccess, Nothing, "", "", "", _strPassword)
                'chages at 2007 05 15

                'change the sms message, regarding on andry request.
                'Dim strUserAndDealer As String = objUser.UserName + " & " + objUser.Dealer.DealerCode
                _msgContent = enumSMS.GetContentMessage(enumSMS.ContentMessage.ResetPasswordSuccess, objUser, "", "", "", _strPassword)
                'LogTosyslog2(_msgContent)
                'end changes
                If _msgContent <> String.Empty Then
                    'Sms.Sendto(objUser.HandPhone, _msgContent)
                    Dim otpfunc As New OTPFunction

                    otpfunc.SendSMSNotif(objUser.HandPhone, _msgContent)
                    If (Not otpfunc.boolReturn) Then

                    End If
                End If
            Catch ex As Exception
                Return result
                MessageBox.Show("Reset Password tidak berhasil : " & ex.Message)
                'LogTosyslog("password user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & " tidak berhasil di reset oleh " & CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode & "-" & CType(Session("LOGINUSERINFO"), UserInfo).UserName, "pass-reset-admin", "failed")
                LogTosyslog(" resetting password for user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & " was executed by " & CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode & "-" & CType(Session("LOGINUSERINFO"), UserInfo).UserName & " but failed ", "forcedpassword-reset-not", "failed", "web-security", "read")

            End Try
        End If
        Return result
    End Function


    Private Sub dgUserList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgUserList.SortCommand
        '-- Sort datagrid rows based on a column header clicked

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

        BindPage(0)  '-- Bind page-1

    End Sub

    Private Sub dgUserList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgUserList.ItemCommand

        If e.CommandName = "Edit" Then
            '-- Edit user
            Response.Redirect("frmUserInfoDetail.aspx?id=" & e.Item.Cells(0).Text & "&type=3")

        ElseIf e.CommandName = "Active" Then
            '-- Activate user
            ActivateUser(e.Item.Cells(0).Text)
            ReadData()   '-- Re-read data
            BindPage(0)  '-- Bind page-1

        ElseIf e.CommandName = "Group" Then
            '-- Set Dealer group
            Response.Redirect("FrmUserOrganizationAssignment.aspx?id=" & e.Item.Cells(0).Text)

        ElseIf e.CommandName = "Inactive" Then
            '-- Inactivate user
            InactivateUser(e.Item.Cells(0).Text)
            ReadData()   '-- Re-read data
            BindPage(0)  '-- Bind page-1

        ElseIf e.CommandName = "Delete" Then
            '-- Delete user
            DeleteUser(e.Item.Cells(0).Text)
            ReadData()   '-- Re-read data
            BindPage(0)  '-- Bind page-1

        ElseIf e.CommandName = "Role" Then
            '-- Set User role
            Response.Redirect("FrmEditUserRole.aspx?UserID=" & e.Item.Cells(0).Text)

        ElseIf e.CommandName = "Unregister" Then
            '-- Set User Activation and Registration Status
            Dim nID As Integer = e.Item.Cells(0).Text
            Dim oUser As UserInfo = New UserInfoFacade(User).Retrieve(nID)
            If UnregisterUserProfile(CInt(e.Item.Cells(0).Text)) Then
                BindPage(0)
                MessageBox.Show("Unregister Berhasil")
                LogTosyslog(" user " & oUser.Dealer.DealerCode & "-" & oUser.UserName & " berhasil di unregister oleh " & CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode & "-" & CType(Session("LOGINUSERINFO"), UserInfo).UserName, "user-unregister-admin", "sucess")
            Else
                MessageBox.Show("Unregister Tidak Berhasil")
                LogTosyslog(" user " & oUser.Dealer.DealerCode & "-" & oUser.UserName & " gagal di unregister oleh " & CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode & "-" & CType(Session("LOGINUSERINFO"), UserInfo).UserName, "user-unregister-admin", "failed")

            End If
        ElseIf e.CommandName = "ResetPassword" Then
            Dim nID As Integer = e.Item.Cells(0).Text
            Dim oUser As UserInfo = New UserInfoFacade(User).Retrieve(nID)
            If Not oUser Is Nothing Then
                Dim result = ResetPassword(oUser)
                ''''If Not oUser.UserProfile Is Nothing Then
                ''''    If ((oUser.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.NotRegister) Or (oUser.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register And oUser.UserProfile.ActivationStatus <> EnumSE.ActivationCodeStatus.Active)) Then
                ''''        MessageBox.Show(result)
                ''''    Else
                MessageBox.Show("Reset Password Berhasil")
                LogTosyslog("password user " & oUser.Dealer.DealerCode & "-" & oUser.UserName & " berhasil di reset oleh " & CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode & "-" & CType(Session("LOGINUSERINFO"), UserInfo).UserName, "pass-reset-admin", "sucess")

                ''''    End If
                ''''Else
                ''''    MessageBox.Show(result)
                ''''End If
            Else
                MessageBox.Show("Session User sudah tidak valid, silahkan login ulang.")
            End If

        ElseIf e.CommandName = "SetPassword" Then
            Dim nID As Integer = e.Item.Cells(0).Text
            Dim oUser As UserInfo = New UserInfoFacade(User).Retrieve(nID)
            If Not oUser Is Nothing Then
                Dim result = ResetPassword(oUser, "test123")
                MessageBox.Show("Reset Password 'test123' Berhasil")
                LogTosyslog("password user " & oUser.Dealer.DealerCode & "-" & oUser.UserName & " berhasil di reset test123 oleh " & CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode & "-" & CType(Session("LOGINUSERINFO"), UserInfo).UserName, "pass-reset-admin", "sucess")
            Else
                MessageBox.Show("Session User sudah tidak valid, silahkan login ulang.")
            End If
        End If

    End Sub

    Private Sub dgUserList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgUserList.PageIndexChanged
        '-- Change datagrid page
        BindPage(e.NewPageIndex)
    End Sub

#End Region

End Class
