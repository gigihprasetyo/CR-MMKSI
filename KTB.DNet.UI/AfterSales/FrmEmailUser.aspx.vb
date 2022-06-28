
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmEmailUser
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEmailUser As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private sessHelp As SessionHelper = New SessionHelper

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private m_bFormPrivilege As Boolean = False
    Private ListEmailUser As ArrayList
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
        End If

    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchEmailUserByCriteria()
    End Sub

    Private Sub dtgEmailUser_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEmailUser.ItemCommand

    End Sub

    Private Sub dtgEmailUser_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEmailUser.ItemDataBound
        Dim RowValue As V_AssistEmailUser = CType(e.Item.DataItem, V_AssistEmailUser)
        Dim actLb As LinkButton
        Dim nactLb As LinkButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgEmailUser.CurrentPageIndex * dtgEmailUser.PageSize)
        End If

    End Sub

    Private Sub dtgEmailUser_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEmailUser.SortCommand
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

        dtgEmailUser.SelectedIndex = -1
        dtgEmailUser.CurrentPageIndex = 0
        BindDataGrid(dtgEmailUser.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgEmailUser_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEmailUser.PageIndexChanged
        dtgEmailUser.SelectedIndex = -1
        dtgEmailUser.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgEmailUser.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()

    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_EmailUser_View_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_EmailUser_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Master - Email User")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Dealer.DealerCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        Dim objUserInfo As UserInfo = sessHelp.GetSession("LOGINUSERINFO")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            'txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")

        Else
            txtKodeDealer.ReadOnly = False

        End If
    End Sub

    Private Sub ClearData()
        txtEmail.Text = String.Empty
        txtNama.Text = String.Empty

        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_AssistEmailUser), "Dealer.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If Me.txtKodeDealer.Text.Trim <> "" Then
                Dim sCodes As String = Me.txtKodeDealer.Text.Trim()
                sCodes = "'" & sCodes.Replace(";", "','") & "'"
                criterias.opAnd(New Criteria(GetType(V_AssistEmailUser), "Dealer.DealerCode", MatchType.InSet, "(" & sCodes & ")"))
            End If

            If txtNama.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_AssistEmailUser), "Name", MatchType.[Partial], txtNama.Text.Trim))
            End If
            If txtEmail.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_AssistEmailUser), "Email", MatchType.[Partial], txtEmail.Text.Trim))
            End If

            ListEmailUser = New V_AssistEmailUserFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgEmailUser.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgEmailUser.DataSource = ListEmailUser
            dtgEmailUser.VirtualItemCount = totRow
            dtgEmailUser.DataBind()
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objEmailUser As V_AssistEmailUser = New V_AssistEmailUserFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsEmailUser", objEmailUser)
        If IsNothing(objEmailUser) Then
            txtNama.Text = ""
            txtEmail.Text = ""
        Else
            txtNama.Text = objEmailUser.Name
            txtEmail.Text = objEmailUser.Email
        End If

    End Sub

    Private Sub SearchEmailUserByCriteria()
        dtgEmailUser.CurrentPageIndex = 0
        BindDataGrid(dtgEmailUser.CurrentPageIndex)
    End Sub
#End Region

End Class
