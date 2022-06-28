#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Buletin
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Collections.Generic

#End Region

Public Class PopUpUserGroup
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"
    Private objUserGroupFacade As UserGroupFacade
    Private arlUserGroup As ArrayList = New ArrayList
    Private sHelper As SessionHelper = New SessionHelper
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnSaveBuletin As System.Web.UI.WebControls.Button
    Protected WithEvents btnSavePresentation As System.Web.UI.WebControls.Button
    Protected WithEvents PnlNonBuletin As System.Web.UI.WebControls.Panel
    Protected WithEvents PnlBuletin As System.Web.UI.WebControls.Panel
    Protected WithEvents PnlPresentation As System.Web.UI.WebControls.Panel

    Dim criterias As CriteriaComposite
    Const strViewCrit As String = "strCrit"
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgUserGroup As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtUserGroup As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(UserGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtUserGroup.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(UserGroup), "Description", MatchType.[Partial], txtUserGroup.Text))
        End If
        sHelper.SetSession(strViewCrit, criterias)
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then

            criterias = CType(sHelper.GetSession(strViewCrit), CriteriaComposite)

            arlUserGroup = New UserGroupFacade(User).RetrieveActiveList(indexPage + 1, dtgUserGroup.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection), criterias)

            dtgUserGroup.DataSource = arlUserGroup
            dtgUserGroup.VirtualItemCount = totalRow
            dtgUserGroup.DataBind()
            If dtgUserGroup.Items.Count > 0 Then
                btnChoose.Disabled = False
            Else
                btnChoose.Disabled = True
            End If
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            dtgUserGroup.DataSource = arlUserGroup
            dtgUserGroup.DataBind()
            SetPanel()
        End If
    End Sub

    Private Sub SetPanel()
        If Val(Request.QueryString("isBuletin")) = 1 Then
            PnlBuletin.Visible = True
            PnlNonBuletin.Visible = False
            PnlPresentation.Visible = False
        ElseIf Val(Request.QueryString("isPresentation")) = 1 Then
            PnlBuletin.Visible = False
            PnlNonBuletin.Visible = False
            PnlPresentation.Visible = True
        Else
            PnlBuletin.Visible = False
            PnlNonBuletin.Visible = True
        End If
    End Sub

    Private Sub dtgUserGroup_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUserGroup.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arlUserGroup Is Nothing) Then
                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgUserGroup.CurrentPageIndex * dtgUserGroup.PageSize)

            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgUserGroup.CurrentPageIndex = 0
        CreateCriteria()
        BindDatagrid(0)

    End Sub

    Private Sub dtgUserGroup_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgUserGroup.PageIndexChanged

        dtgUserGroup.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgUserGroup.CurrentPageIndex)
    End Sub

    Private Sub dtgUserGroup_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgUserGroup.SortCommand
        If e.SortExpression = ViewState("CurrentSortColumn") Then
            If ViewState("CurrentSortDirect") = Sort.SortDirection.ASC Then
                ViewState.Add("CurrentSortDirect", Sort.SortDirection.DESC)
            Else
                ViewState.Add("CurrentSortDirect", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("CurrentSortColumn", e.SortExpression)
        BindDatagrid(0)
    End Sub

    Private Sub btnSaveBuletin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveBuletin.Click
        Dim arlDataSave As New ArrayList
        Dim id As Integer = CInt(Request.QueryString("id"))
        For Each item As DataGridItem In dtgUserGroup.Items
            Dim chkCek As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim lblGroupCode As Label = CType(item.FindControl("Label1"), Label)
            Dim lblCode As Label = item.FindControl("lblUserName")

            If chkCek.Checked = True Then

                'get Buletin
                Dim objBuletin As KTB.DNet.Domain.Buletin = New BuletinFacade(User).Retrieve(id)

                'get Group
                Dim objGroup As UserGroup = New UserGroupFacade(User).Retrieve(lblGroupCode.Text)

                'cek double buletin dan userGroup
                Dim criteriaCek As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaCek.opAnd(New Criteria(GetType(BuletinGroupMember), "Buletin.ID", MatchType.Exact, objBuletin.ID))
                criteriaCek.opAnd(New Criteria(GetType(BuletinGroupMember), "UserGroup.ID", MatchType.Exact, objGroup.ID))
                Dim arlCek As ArrayList = New BuletinGroupMemberFacade(User).Retrieve(criteriaCek)

                If (arlCek.Count = 0) Then
                    'fill arlDataSave
                    Dim obj As BuletinGroupMember = New BuletinGroupMember
                    obj.Buletin = objBuletin
                    obj.UserGroup = objGroup

                    Dim objBuletinGroupMemberFacade As BuletinGroupMemberFacade = New BuletinGroupMemberFacade(User)
                    objBuletinGroupMemberFacade.Insert(obj)

                    'arlDataSave.Add(obj)
                End If
            End If
        Next

        'If arlDataSave.Count > 0 Then
        '    Dim objBuletinGroupMemberFacade As BuletinGroupMemberFacade = New BuletinGroupMemberFacade(User)
        '    objBuletinGroupMemberFacade.Insert(CType(arlDataSave(0), BuletinGroupMember))
        'End If

        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub

    Private Sub btnSaveBuletinOld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveBuletin.Click
        Dim arlDataSave As New ArrayList
        Dim id As Integer = CInt(Request.QueryString("id"))
        For Each item As DataGridItem In dtgUserGroup.Items
            Dim chkCek As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim lblGroupCode As Label = CType(item.FindControl("Label1"), Label)
            Dim lblCode As Label = item.FindControl("lblUserName")

            If chkCek.Checked = True Then

                'get Buletin
                Dim objBuletin As KTB.DNet.Domain.Buletin = New BuletinFacade(User).Retrieve(id)

                'get Group
                Dim objGroup As UserGroup = New UserGroupFacade(User).Retrieve(lblGroupCode.Text)
                'fill arlDataSave
                For Each itemUser As UserGroupMember In objGroup.UserGroupMembers
                    Dim obj As BuletinMember = New BuletinMember
                    obj.Buletin = objBuletin
                    obj.UserInfo = itemUser.UserInfo
                    'cek existing data
                    Dim criteriaCek As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriaCek.opAnd(New Criteria(GetType(BuletinMember), "Buletin.ID", MatchType.Exact, id))
                    criteriaCek.opAnd(New Criteria(GetType(BuletinMember), "UserInfo.ID", MatchType.Exact, itemUser.UserInfo.ID))
                    Dim arlCek As ArrayList = New BuletinMemberFacade(User).Retrieve(criteriaCek)
                    If arlCek.Count = 0 Then
                        arlDataSave.Add(obj)
                    End If
                Next


            End If
        Next

        If arlDataSave.Count > 0 Then
            Dim objBuletinMemberFacade As BuletinMemberFacade = New BuletinMemberFacade(User)
            objBuletinMemberFacade.InsertTransaction(arlDataSave)
        End If

        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub

    Protected Sub btnSavePresentation_Click(sender As Object, e As EventArgs) Handles btnSavePresentation.Click
        Dim arlDataSave As New ArrayList
        Dim id As Integer = CInt(Request.QueryString("id"))
        Dim objPresentation As KTB.DNet.Domain.Presentation = New PresentationFacade(User).Retrieve(id)
        Dim arrUserGroup As New List(Of UserGroup)

        For Each item As DataGridItem In dtgUserGroup.Items
            Dim chkCek As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim lblUserGroupID As Label = CType(item.FindControl("Label2"), Label)

            If chkCek.Checked = True Then
                'get Group
                Dim objGroup As UserGroup = New UserGroup(CInt(lblUserGroupID.Text))
                objGroup.MarkLoaded()
                arrUserGroup.Add(objGroup)
            End If
        Next

        If arrUserGroup.Count > 0 Then
            Dim objPresentationGroupFacade As PresentationGroupFacade = New PresentationGroupFacade(User)
            objPresentationGroupFacade.InsertUserGroup(objPresentation, arrUserGroup)
        End If

        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub
End Class
