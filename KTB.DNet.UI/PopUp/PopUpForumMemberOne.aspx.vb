#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Buletin
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class PopUpForumMemberOne
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtUserGroup As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchUserGroup As System.Web.UI.WebControls.Label
    Protected WithEvents txtUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlBuletin As System.Web.UI.WebControls.Panel
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgAboutMember As System.Web.UI.WebControls.DataGrid

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
    Private objUserInfoFacade As UserInfoFacade
    Private arlMember As ArrayList
    Private sHelper As SessionHelper = New SessionHelper
    Dim criterias As CriteriaComposite
#End Region

#Region "Custom Method"
    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer

        If indexPage >= 0 Then
            CreateCriteria()
            'arlMember = New UserInfoFacade(User).Retrieve(criterias)
            arlMember = New UserInfoFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgAboutMember.PageSize, totalRow, sHelper.GetSession("SortColPopUp"), sHelper.GetSession("SortDirectionPopUp"))

            If arlMember.Count > 0 Then
                dtgAboutMember.DataSource = arlMember
            Else
                dtgAboutMember.DataSource = New ArrayList
                MessageBox.Show("Data tidak ditemukan")
            End If
        End If

        dtgAboutMember.VirtualItemCount = totalRow
        dtgAboutMember.DataBind()
    End Sub

    Private Sub CreateCriteria()
        Dim criteriaGroupMember As CriteriaComposite
        Dim criteriaDealer As CriteriaComposite
        Dim arlGroupMember As ArrayList
        Dim strUserID As String = ""

        criteriaGroupMember = New CriteriaComposite(New Criteria(GetType(UserGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'add by ery
        'refer bug 1059
        Dim strUserGroup As String = txtUserGroup.Text.Replace(";", "','")
        Dim critsUG As New CriteriaComposite(New Criteria(GetType(UserGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critsUG.opAnd(New Criteria(GetType(UserGroup), "Code", MatchType.InSet, "('" & strUserGroup & "')"))
        Dim arlUserGroup As ArrayList = New UserGroupFacade(User).Retrieve(critsUG)
        Dim strUGMemberID As String = ""
        If arlUserGroup.Count > 0 Then
            For i As Integer = 0 To arlUserGroup.Count - 1
                Dim objUserGroup As UserGroup = arlUserGroup(i)
                If objUserGroup.UserGroupMembers.Count > 0 Then
                    For Each item As UserGroupMember In objUserGroup.UserGroupMembers
                        strUGMemberID &= item.ID & ";"
                    Next
                End If
            Next
        End If

        If strUGMemberID <> "" Then
            strUGMemberID = Left(strUGMemberID, strUGMemberID.Length - 1)
            criteriaGroupMember.opAnd(New Criteria(GetType(UserGroupMember), "ID", MatchType.InSet, "(" & strUGMemberID.Replace(";", ",") & ")"))
        End If

        'additional by Ery
        'untuk menampilkan user yang aktif saja
        criteriaGroupMember.opAnd(New Criteria(GetType(UserGroupMember), "UserInfo.UserStatus", MatchType.Exact, CByte(EnumUserStatus.UserStatus.Aktif)))

        'Framework limitation
        'arlGroupMember = New UserGroupMemberFacade(User).Retrieve(criteriaGroupMember)

        'If arlGroupMember.Count > 0 Then
        '    For Each item As UserGroupMember In arlGroupMember
        '        strUserID = strUserID & item.UserInfo.ID & ","
        '    Next
        '    strUserID = Left(strUserID, strUserID.Length - 1)
        'End If

        criteriaDealer = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'added by ery
        criteriaDealer.opAnd(New Criteria(GetType(UserInfo), "UserStatus", MatchType.Exact, CByte(EnumUserStatus.UserStatus.Aktif)))
        If (txtDealer.Text <> "") Then
            criteriaDealer.opAnd(New Criteria(GetType(UserInfo), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealer.Text.Replace(";", "','") & "')"))
        End If

        'If (strUserID <> "") Then
        If (txtUserGroup.Text <> "") Then
            'Framework limitation
            criteriaDealer.opAnd(New Criteria(GetType(UserInfo), "ID", MatchType.InSet, "(" & "select userid from usergroupmember m join usergroup u on(m.usergroupid=u.id) where u.code in('" & strUserGroup & "')" & ")"))
            'criteriaDealer.opAnd(New Criteria(GetType(UserInfo), "ID", MatchType.InSet, "(" & strUserID & ")"))
        End If

        If (txtUserName.Text <> "") Then
            criteriaDealer.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.[Partial], txtUserName.Text.Trim))
        End If
        criterias = criteriaDealer
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            sHelper.SetSession("SortColPopUp", "UserName")
            sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
            'BindDatagrid(0)

            ' 08-Nov-2007   Deddy H     Fix dengan kondisi tertentu, reference bug 1383, point 2
            'lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            If Not IsNothing(Request.QueryString("All")) Then
                lblSearchDealer.Attributes("onClick") = "showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx?x=Territory&All=True','',500,760,DealerSelection);"
            Else
                lblSearchDealer.Attributes("onClick") = "showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx?x=Territory','',500,760,DealerSelection);"
            End If


            lblSearchUserGroup.Attributes("onclick") = "ShowPPUserGroupSelection();"
            pnlBuletin.Visible = True
        End If
    End Sub

    Private Sub dtgAboutMember_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgAboutMember.SortCommand
        If e.SortExpression = sHelper.GetSession("SortColPopUp") Then
            If sHelper.GetSession("SortDirectionPopUp") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortColPopUp", e.SortExpression)
        BindDatagrid(dtgAboutMember.CurrentPageIndex)
    End Sub

    Private Sub dtgAboutMember_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAboutMember.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arlMember Is Nothing) Then
                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgAboutMember.CurrentPageIndex * dtgAboutMember.PageSize)

            End If
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgAboutMember.CurrentPageIndex = 0
        sHelper.SetSession("SortColPopUp", "ID")
        sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.DESC)
        BindDatagrid(0)
    End Sub

    Private Sub dtgAboutMember_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAboutMember.PageIndexChanged
        dtgAboutMember.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgAboutMember.CurrentPageIndex)

    End Sub
End Class
