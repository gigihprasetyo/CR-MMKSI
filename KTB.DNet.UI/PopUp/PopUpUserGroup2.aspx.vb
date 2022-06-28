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

Public Class PopUpUserGroup2
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"
    Private objUserGroupFacade As UserGroupFacade
    Private arlUserGroup As ArrayList = New ArrayList
    Private sHelper As SessionHelper = New SessionHelper
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnSaveBuletin As System.Web.UI.WebControls.Button
    Protected WithEvents PnlNonBuletin As System.Web.UI.WebControls.Panel
    Protected WithEvents PnlBuletin As System.Web.UI.WebControls.Panel
    Protected WithEvents btnsimpan As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtvalue2 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button

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
            sHelper.SetSession("arrUserGroup", arlUserGroup)
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
        Else
            PnlBuletin.Visible = False
            PnlNonBuletin.Visible = True
        End If
    End Sub

    Private Sub dtgUserGroup_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUserGroup.ItemDataBound
        Dim objUserGroup As New UserGroup
        If e.Item.ItemIndex <> -1 Then
            If Not (arlUserGroup Is Nothing) Then
                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgUserGroup.CurrentPageIndex * dtgUserGroup.PageSize)
                If Not sHelper.GetSession("Buletin") Is Nothing Then
                    Dim objBuletinCategory As BuletinCategory = sHelper.GetSession("Buletin")
                    objUserGroup = CType(arlUserGroup(e.Item.ItemIndex), UserGroup)
                    For Each row As BuletinDetail In objBuletinCategory.BuletinDetails
                        If row.UserGroup.ID = objUserGroup.ID Then
                            Dim _chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
                            _chkItemChecked.Checked = True
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgUserGroup.CurrentPageIndex = 0
        CreateCriteria()
        BindDatagrid(0)

    End Sub

    Private Sub dtgUserGroup_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgUserGroup.PageIndexChanged
        InsertValueToSession()
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

    Private Sub InsertValueToSession()

        Dim arlDataSave As New ArrayList
        Dim objUserGroup As UserGroup
        Dim objBuletinCategory As New BuletinCategory
        If Not sHelper.GetSession("arrUserGroup") Is Nothing Then
            arlUserGroup = sHelper.GetSession("arrUserGroup")
        End If
        If Not sHelper.GetSession("Buletin") Is Nothing Then
            objBuletinCategory = sHelper.GetSession("Buletin")
        End If

        Dim id As Integer = CInt(Request.QueryString("id"))
        Dim Flag As Integer = 0

        For Each item As DataGridItem In dtgUserGroup.Items
            Dim chkCek As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim lblGroupCode As Label = CType(item.FindControl("Label1"), Label)
            Dim lblCode As Label = item.FindControl("lblUserName")
            If chkCek.Checked = True Then
                If arlUserGroup.Count > 0 Then
                    objUserGroup = CType(arlUserGroup(item.ItemIndex), UserGroup)
                    Dim i As Integer = 0
                    For Each row As BuletinDetail In objBuletinCategory.BuletinDetails
                        If row.UserGroup.ID = objUserGroup.ID Then
                            i = 1
                            Exit For
                        End If
                    Next
                    If i = 0 Then
                        Dim objBuletinDetail As New BuletinDetail
                        objBuletinDetail.BuletinCategory = objBuletinCategory
                        objBuletinDetail.UserGroup = objUserGroup
                        objBuletinCategory.BuletinDetails.Add(objBuletinDetail)
                    End If
                End If
            Else
                If arlUserGroup.Count > 0 Then
                    objUserGroup = CType(arlUserGroup(item.ItemIndex), UserGroup)
                    Dim i As Integer = 0
                    For Each row As BuletinDetail In objBuletinCategory.BuletinDetails
                        If row.UserGroup.ID = objUserGroup.ID Then
                            If objBuletinCategory.BuletinDetails.Count = 1 Then
                                Flag = 1
                            Else
                                objBuletinCategory.BuletinDetails.RemoveAt(i)
                            End If

                            Exit For
                        End If
                        i = i + 1
                    Next
                End If
            End If
        Next

        Dim strUserGroup As String = String.Empty
        txtvalue2.Value = String.Empty
        If Flag = 1 Then
            objBuletinCategory.BuletinDetails.RemoveAt(0)
        End If

        If Flag = 0 Then
            For Each row As BuletinDetail In objBuletinCategory.BuletinDetails
                If txtvalue2.Value = String.Empty Then
                    strUserGroup = row.UserGroup.Code
                    txtvalue2.Value = strUserGroup
                Else
                    strUserGroup = strUserGroup & ";" & row.UserGroup.Code
                    txtvalue2.Value = strUserGroup
                End If
            Next
        Else
            txtvalue2.Value = String.Empty
        End If




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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        InsertValueToSession()
        RegisterStartupScript("CloseWindow", "<script>SaveUserGroup();</script>")
        'Response.Write("<script language='javascript'>")
        'Response.Write("var lbltext = document.getElementById('txtvalue2');")
        'Response.Write("var UserGroup = lbltext.value;")
        'Response.Write("window.returnValue = UserGroup;")
        'Response.Write("window.close();")
        'Response.Write("</script>")


    End Sub

End Class
