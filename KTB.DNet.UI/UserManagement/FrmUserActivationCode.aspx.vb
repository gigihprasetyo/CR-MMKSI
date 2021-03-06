#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security

#End Region

Public Class FrmUserActivationCode
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtHandphone As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgUserList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFirstName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLastName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox

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
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            'UserPrivilege()  '-- Set user privileges
            Initialize()
            BindPage(0)  '-- Bind page-1
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindPage(0)  '-- Bind page-1
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtKodeDealer.Text = String.Empty
        txtHandphone.Text = String.Empty
        txtUserName.Text = String.Empty
        txtFirstName.Text = String.Empty
        txtLastName.Text = String.Empty
        txtEmail.Text = String.Empty
    End Sub

    Private Sub dgUserList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgUserList.PageIndexChanged
        dgUserList.CurrentPageIndex = e.NewPageIndex
        BindPage(dgUserList.CurrentPageIndex)
    End Sub

    Private Sub dgUserList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUserList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dgUserList.PageSize * dgUserList.CurrentPageIndex)).ToString
        End If
    End Sub

    Private Sub dgUserList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgUserList.SortCommand
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

        dgUserList.SelectedIndex = -1
        dgUserList.CurrentPageIndex = 0
        BindPage(dgUserList.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"

    Private Sub Initialize()
        '-- Init sorting column and sort direction defaults
        ViewState("currSortColumn") = "UserInfo.UserName"
        ViewState("currSortDirection") = Sort.SortDirection.ASC

        '-- Display grid column headers
        dgUserList.DataSource = New ArrayList
        dgUserList.DataBind()

        '-- Popup dealer list
        lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"

    End Sub


    Private Sub BindPage(ByVal currentPageIndex As Integer)
        'Dim o As New UserProfile
        'o.UserInfo.HandPhone()
        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(UserProfile), "UserInfo.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If
        '--UserName
        If txtUserName.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(UserProfile), "UserInfo.UserName", MatchType.[Partial], txtUserName.Text.Trim()))
        End If
        '--FirstName
        If txtFirstName.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(UserProfile), "UserInfo.FirstName", MatchType.[Partial], txtFirstName.Text.Trim()))
        End If
        '--LastName
        If txtLastName.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(UserProfile), "UserInfo.LastName", MatchType.[Partial], txtLastName.Text.Trim()))
        End If
        '--Email
        If txtEmail.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(UserProfile), "UserInfo.Email", MatchType.[Partial], txtEmail.Text.Trim()))
        End If
        '--Handphone
        If txtHandphone.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(UserProfile), "UserInfo.HandPhone", MatchType.[Partial], txtHandphone.Text.Trim()))
        End If

        '-- Retrieve recordset
        'Dim UserList As ArrayList = New UserProfileFacade(User).Retrieve(criterias, sortColl)
        Dim total As Integer = 0
        Dim UserList As ArrayList = New UserProfileFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dgUserList.PageSize, _
                        total, CType(ViewState("currSortColumn"), String), _
                        CType(ViewState("currSortDirection"), Sort.SortDirection))

        dgUserList.DataSource = UserList
        dgUserList.VirtualItemCount = total
        If UserList.Count > 0 Then
            dgUserList.DataBind()
        Else
            dgUserList.DataBind()
            MessageBox.Show("Data Tidak Ditemukan")
        End If

    End Sub

    Private Sub UserPrivilege()

        If Not IsNothing(sessHelp.GetSession("DEALER")) Then
            Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                '-- As KTB user
                If Not SecurityProvider.Authorize(Context.User, SR.AdminViewListOfUserKTB_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - User Activation Code")
                End If
                sessHelp.SetSession("UserEditable", SecurityProvider.Authorize(Context.User, SR.AdminUpdateUserKTB_Privilege))
                Return

            ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER OrElse objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                '-- As ordinal user
                If Not SecurityProvider.Authorize(Context.User, SR.AdminViewListOfUserDealer_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - User Activation Code")
                End If
                sessHelp.SetSession("UserEditable", SecurityProvider.Authorize(Context.User, SR.AdminUpdateUserDealer_Privilege))
                Return

            End If
        End If


        Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - User Activation Code")
    End Sub
#End Region

    
    
End Class
