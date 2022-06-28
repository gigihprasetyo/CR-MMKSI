#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class PopUpGroupMember
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtPosisi As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchPosisi As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgAboutMember As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents pnlHtmlControl As System.Web.UI.WebControls.Panel
    Protected WithEvents panelNew As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents panelAdd As System.Web.UI.WebControls.Panel
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgAddMember As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Private arlMember As ArrayList
    Private arlGroupMember As ArrayList
    Private criterias As CriteriaComposite
    Private sHelper As New SessionHelper
    Private _QSID As String
    Private cmd As String
#End Region

#Region "Custom Method"
    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then

            CreateCriteria()
            arlMember = New ArrayList
            arlMember = New UserInfoFacade(User).RetrieveActiveList(indexPage + 1, dtgAboutMember.PageSize, totalRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"), criterias)
            dtgAboutMember.DataSource = arlMember
            dtgAboutMember.VirtualItemCount = totalRow
            dtgAboutMember.DataBind()
        End If
    End Sub

    Private Sub BindDatagrid2(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        SearchUserMember()
        If (indexPage >= 0) Then

            CreateCriteria()
            arlMember = New ArrayList
            arlMember = New UserInfoFacade(User).RetrieveActiveList(indexPage + 1, dtgAddMember.PageSize, totalRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"), criterias)

            dtgAddMember.DataSource = arlMember
            dtgAddMember.VirtualItemCount = totalRow

            dtgAddMember.DataBind()

        End If
    End Sub

    Private Sub SearchUserMember()
        'get registered member
        Dim id As Integer = _QSID
        criterias = New CriteriaComposite(New Criteria(GetType(UserGroupMember), "UserGroup.ID", MatchType.Exact, id))
        Dim arlUserGroupMember As ArrayList = New UserGroupMemberFacade(User).Retrieve(criterias)
        Dim arlUserGMember As New ArrayList
        For Each item As UserGroupMember In arlUserGroupMember
            arlUserGMember.Add(item.UserInfo)
        Next

        sHelper.SetSession("arlUserGMember", arlUserGMember)
    End Sub

    Private Sub CreateCriteria()
        'criterias = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If (txtDealer.Text <> String.Empty) Then
            Dim arrDealer As String = txtDealer.Text.Replace(";", "','")
            Dim strsql As String = "('" & arrDealer & "')"
            criterias = New CriteriaComposite(New Criteria(GetType(UserInfo), "Dealer.DealerCode", MatchType.InSet, strsql))
        End If

        If txtPosisi.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(UserInfo), "JobPosition", MatchType.Exact, txtPosisi.Text.Trim))
        End If
    End Sub

    Private Sub EnableControl(ByVal status As Boolean)
        txtDealer.Enabled = status
        txtPosisi.Enabled = status
        btnSearch.Enabled = status
        pnlHtmlControl.Enabled = status
        lblSearchDealer.Enabled = status

    End Sub

    Private Sub ViewData()
        Dim idgroup As Integer = CInt(_QSID)
        criterias = New CriteriaComposite(New Criteria(GetType(UserGroupMember), "UserGroup.ID", MatchType.Exact, idgroup))
        Dim arlUserGroupMember As ArrayList = New UserGroupMemberFacade(User).Retrieve(criterias)

        Dim arrTempData As ArrayList = New ArrayList
        For Each item As UserGroupMember In arlUserGroupMember
            arrTempData.Add(item.UserInfo)
        Next
        sHelper.SetSession("ArrayViewData", arrTempData)
        BindViewData(arrTempData)
    End Sub

    Private Sub BindViewData(ByVal arrTempData As ArrayList)
        If arrTempData Is Nothing Then
            arrTempData = New ArrayList
        Else
            dtgAboutMember.DataSource = arrTempData
        End If
        dtgAboutMember.DataBind()
    End Sub

    Private Sub SavetToArrayList()
        Dim arlUserGMember As ArrayList = sHelper.GetSession("arlUserGMember")
        Dim arlCheckedData As New ArrayList
        Dim ojUsegroup As UserGroup = New UserGroup(CInt(Request.QueryString("ID")))

        Dim objUserGroupMember As UserGroupMember
        For Each item As DataGridItem In dtgAddMember.Items
            Dim checkedItem As CheckBox = CType(item.FindControl("chkBuletinMemberChecked"), CheckBox)
            Dim lblID As Label = CType(item.FindControl("lblID"), Label)
            Dim lblusername As Label = CType(item.FindControl("lblUserName"), Label)
            Dim lblJobPosition As Label = CType(item.FindControl("lblJobPosition"), Label)
            Dim lblKodeAdd As Label = CType(item.FindControl("lblKodeAdd"), Label)
            Dim objUserInfo As UserInfo = New UserInfo(CInt(lblID.Text))
            If checkedItem.Checked Then
                objUserGroupMember = New UserGroupMember(CInt(lblID.Text))
                objUserGroupMember.UserGroup = ojUsegroup
                objUserGroupMember.UserInfo = objUserInfo
                arlCheckedData.Add(objUserGroupMember)
            End If
        Next


        Dim objUserGroupMemberFacade As UserGroupMemberFacade = New UserGroupMemberFacade(User)
        objUserGroupMemberFacade.Update(arlCheckedData, ojUsegroup)
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _QSID = Request.QueryString("ID")
        cmd = Request.QueryString("cmd")
        If cmd = "Add" Then
            ViewState("CurrentSortColumn") = "UserName"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            btnBatal.Attributes("onclick") = "ClosePopUp();"
            EnableControl(True)
            panelAdd.Visible = True
            panelNew.Visible = False
        ElseIf cmd = "New" Then
            ViewState("CurrentSortColumn") = "UserName"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            EnableControl(True)
            btnBack.Attributes("onclick") = "ClosePopUp();"
            panelNew.Visible = True
            panelAdd.Visible = False
            'ViewData()
        Else
            ViewState("CurrentSortColumn") = "UserName"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            EnableControl(False)
            btnBack.Attributes("onclick") = "ClosePopUp();"
            panelNew.Visible = True
            panelAdd.Visible = False
            ViewData()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If cmd = "Add" Then
            ViewState("CurrentSortColumn") = "UserName"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            BindDatagrid2(0)
        Else
            ViewState("CurrentSortColumn") = "UserName"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            BindDatagrid(0)
        End If

    End Sub

    Private Sub dtgAboutMember_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dtgAboutMember.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgAboutMember.CurrentPageIndex)
    End Sub

    Private Sub dtgAboutMember_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgAboutMember.SortCommand
        If e.SortExpression = Viewstate("CurrentSortColumn") Then
            If ViewState("CurrentSortDirect") = Sort.SortDirection.ASC Then
                ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            Else
                ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End If
        End If
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        BindDatagrid(0)

    End Sub

    Private Sub dtgAddMember_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAddMember.ItemDataBound
        Dim arlUserGMember As ArrayList = sHelper.GetSession("arlUserGMember")
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lblno As Label = CType(e.Item.FindControl("lblNoAdd"), Label)
            lblno.Text = e.Item.ItemIndex + 1
        End If

        For Each item As DataGridItem In dtgAddMember.Items
            Dim checkedItem As CheckBox = CType(item.FindControl("chkBuletinMemberChecked"), CheckBox)
            Dim lblID As Label = CType(item.FindControl("lblID"), Label)
            For Each item2 As UserInfo In arlUserGMember
                If item2.ID = lblID.Text Then
                    checkedItem.Checked = True
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        SavetToArrayList()
        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub

    Private Sub dtgAboutMember_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAboutMember.ItemDataBound
        If Not _QSID = String.Empty Then
            e.Item.Cells(0).Visible = False
        Else
            e.Item.Cells(0).Visible = True
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            If Not (arlMember Is Nothing) Then
                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 '+ (dtgAboutMember.CurrentPageIndex * dtgAboutMember.PageSize)

            End If
        End If
    End Sub
End Class
