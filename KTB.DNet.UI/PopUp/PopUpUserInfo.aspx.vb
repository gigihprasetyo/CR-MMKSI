#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class PopUpUserInfo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgUserInfo As System.Web.UI.WebControls.DataGrid
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

#Region "Custom Variable Declaration"
    Private arlUser As ArrayList = New ArrayList
    Private sHelper As SessionHelper = New SessionHelper
    Dim criterias As CriteriaComposite
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            sHelper.SetSession("CurrentSortColumn", "ID")
            sHelper.SetSession("CurrentSortDirect", Sort.SortDirection.ASC)
            bindToGrid(0)
        End If
    End Sub

    Private Sub dtgUserInfo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUserInfo.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not IsNothing(arlUser) Then
                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgUserInfo.CurrentPageIndex * dtgUserInfo.PageSize)

                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelection"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)

                Dim lblNama As Label = CType(e.Item.FindControl("lblNama"), Label)
                Dim lblUserID As Label = CType(e.Item.FindControl("lblUserID"), Label)

                Dim objUserInfo As UserInfo = arlUser(e.Item.ItemIndex)
                lblNama.Text = objUserInfo.FirstName & " " & objUserInfo.LastName
                lblUserID.Text = objUserInfo.Dealer.DealerCode & "-" & objUserInfo.UserName
            End If
        End If

    End Sub

    Private Sub dtgUserInfo_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgUserInfo.PageIndexChanged
        dtgUserInfo.SelectedIndex = -1
        dtgUserInfo.CurrentPageIndex = e.NewPageIndex
        'bindToGrid(dtgUserInfo.CurrentPageIndex)
        Dim total As Integer = 0
        arlUser = New UserInfoFacade(User).RetrieveActiveList(CType(sHelper.GetSession("CriteriaSessUserInfo"), CriteriaComposite), dtgUserInfo.CurrentPageIndex + 1, dtgUserInfo.PageSize, total, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dtgUserInfo.DataSource = arlUser
        dtgUserInfo.DataBind()

    End Sub

    Private Sub dtgUserInfo_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgUserInfo.SortCommand
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
        'bindToGrid(dtgUserInfo.CurrentPageIndex)
        Dim total As Integer = 0
        arlUser = New UserInfoFacade(User).RetrieveActiveList(CType(sHelper.GetSession("CriteriaSessUserInfo"), CriteriaComposite), dtgUserInfo.CurrentPageIndex + 1, dtgUserInfo.PageSize, total, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgUserInfo.DataSource = arlUser
        dtgUserInfo.DataBind()
    End Sub

    Private Sub bindToGrid(ByVal _idx As Integer)
        Dim totalRow As Integer = 0
        CreateCriteria()
        arlUser = New UserInfoFacade(User).RetrieveActiveList(criterias, _idx + 1, dtgUserInfo.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If arlUser.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
        dtgUserInfo.DataSource = arlUser
        dtgUserInfo.VirtualItemCount = totalRow
        dtgUserInfo.DataBind()
    End Sub

    Private Sub CreateCriteria()
        Dim criteriaUser As CriteriaComposite

        criteriaUser = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaUser.opAnd(New Criteria(GetType(UserInfo), "Email", MatchType.No, String.Empty))
        criteriaUser.opAnd(New Criteria(GetType(UserInfo), "UserStatus", MatchType.Exact, CByte(EnumUserStatus.UserStatus.Aktif)))
        criteriaUser.opAnd(New Criteria(GetType(UserInfo), "Dealer.Title", MatchType.Exact, CByte(EnumDealerTittle.DealerTittle.KTB)))

        If txtUserName.Text.Trim <> String.Empty Then
            criteriaUser.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.[Partial], txtUserName.Text.Trim))
        End If
        If txtEmail.Text.Trim <> String.Empty Then
            criteriaUser.opAnd(New Criteria(GetType(UserInfo), "Email", MatchType.[Partial], txtEmail.Text.Trim))
        End If

        criterias = criteriaUser
        sHelper.SetSession("CriteriaSessUserInfo", criterias)

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        sHelper.SetSession("CurrentSortColumn", "ID")
        sHelper.SetSession("CurrentSortDirect", Sort.SortDirection.ASC)
        bindToGrid(0)
    End Sub
End Class
