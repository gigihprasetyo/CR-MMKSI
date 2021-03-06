#Region "Custom imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmProfileHeader
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblKodeProfile As System.Web.UI.WebControls.Label
    Protected WithEvents txtProfileCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgProfileHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "deklarasi"
    Private profHeader As ProfileHeader
    Private profHeaderArray As ArrayList
    Private sHelper As New SessionHelper
    Private _ProfileHeader As New ProfileHeaderFacade(User)
    Private criterias As CriteriaComposite
#End Region

#Region "Custom Method"

    Private Sub MapArray()
        profHeader = sHelper.GetSession("ProfileHeader")
        Dim _frmBackSession As String
        _frmBackSession = CStr(sHelper.GetSession("BackMode"))
        If _frmBackSession <> String.Empty Then
            sHelper.RemoveSession("ProfileHeader")
        End If
        If profHeader Is Nothing Then
            profHeaderArray = New ArrayList
            dtgProfileHeader.DataSource = profHeaderArray
        Else
            profHeaderArray = New ArrayList
            profHeaderArray.Add(profHeader)
            dtgProfileHeader.DataSource = profHeaderArray
        End If

        dtgProfileHeader.DataBind()
    End Sub

    Private Sub BindDataGrid(ByVal pageIndex As Integer)
        Dim totalRow As Integer = 0
        Dim arlSearch As New ArrayList

        If viewstate("isBack") = 0 Then
            criterias = New CriteriaComposite(New Criteria(GetType(ProfileHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If txtProfileCode.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(ProfileHeader), "Code", MatchType.[Partial], txtProfileCode.Text.Trim()))
            End If
            If txtDescription.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(ProfileHeader), "Description", MatchType.[Partial], txtDescription.Text.Trim()))
            End If

            criterias.opAnd(New Criteria(GetType(ProfileHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))

            arlSearch = _ProfileHeader.RetrieveActiveList(criterias, pageIndex + 1, dtgProfileHeader.PageSize, totalRow, viewstate("SortColPH"), viewstate("SortDirectionPH"))
        Else
            criterias = sHelper.GetSession("CritsPH")
            dtgProfileHeader.CurrentPageIndex = pageIndex
            arlSearch = _ProfileHeader.RetrieveActiveList(criterias, pageIndex + 1, dtgProfileHeader.PageSize, totalRow, sHelper.GetSession("SortColumnPH"), sHelper.GetSession("CurrentPageIndexPH"))
        End If

        dtgProfileHeader.DataSource = arlSearch
        dtgProfileHeader.VirtualItemCount = totalRow
        If pageIndex = 0 Then
            dtgProfileHeader.CurrentPageIndex = 0
        End If
        dtgProfileHeader.DataBind()

        sHelper.SetSession("ProfHeaderArray", arlSearch)
        sHelper.SetSession("CritsPH", criterias)
        sHelper.SetSession("SortColumnPH", viewstate("SortColPH"))
        sHelper.SetSession("SortDirPH", viewstate("SortDirectionPH"))
        sHelper.SetSession("CurrentPageIndexPH", pageIndex)
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ProfileListGroupView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Profile Transaksi - Daftar Profile")
        End If
    End Sub

    Private cekCmdBtnPrivilege As Boolean
    Private Function CekButtonPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ProfileListGroupEdit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        cekCmdBtnPrivilege = CekButtonPrivilege()
        If Not IsPostBack Then

            MapArray()

            Me.ddlStatus.DataSource = New EnumStatusProfile().RetrieveStatusMode
            Me.ddlStatus.DataTextField = "DescStatus"
            Me.ddlStatus.DataValueField = "ValStatus"
            Me.ddlStatus.DataBind()

            viewstate.Add("SortColPH", "Code")
            viewstate.Add("SortDirectionPH", Sort.SortDirection.ASC)

            If sHelper.GetSession("BackMode") = "FrmProfileHeader" Then
                viewState.Add("isBack", 1)
                BindDataGrid(CInt(sHelper.GetSession("CurrentPageIndexPH")))
            Else
                viewState.Add("isBack", 0)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgProfileHeader.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub

    Private Sub dtgProfileHeader_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgProfileHeader.ItemDataBound
        Dim lblControl As Label = e.Item.FindControl("lblControl")
        Dim lblStatus As Label = e.Item.FindControl("lblStatus")

        If Not e.Item.DataItem Is Nothing Then
            Dim objProfileHeader As ProfileHeader = e.Item.DataItem

            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgProfileHeader.CurrentPageIndex * dtgProfileHeader.PageSize)


            lblControl.Text = objProfileHeader.ControlType
            If CInt(lblControl.Text) = EnumControlType.ControlType.Text Then
                lblControl.Text = "Text"
            ElseIf CInt(lblControl.Text) = EnumControlType.ControlType.List Then
                lblControl.Text = "List"
            ElseIf CInt(lblControl.Text) = EnumControlType.ControlType.Calendar Then
                lblControl.Text = "Calendar"
            Else
                lblControl.Text = "CheckListBox"
            End If

            lblStatus.Text = objProfileHeader.Status
            If CInt(lblStatus.Text) = EnumStatusProfile.StatusMode.Active Then
                lblStatus.Text = "Active"
            ElseIf CInt(lblStatus.Text) = EnumStatusProfile.StatusMode.Inactive Then
                lblStatus.Text = "InActive"
            End If

            'for privilege
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnViewDetails As LinkButton = CType(e.Item.FindControl("lbtnViewDetails"), LinkButton)
            If cekCmdBtnPrivilege = False Then
                lbtnEdit.Visible = False
            Else
                lbtnEdit.Visible = True
            End If
        End If
    End Sub

    Private Sub dtgProfileHeader_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProfileHeader.ItemCommand
        If e.CommandName = "Details" Then
            sHelper.SetSession("ViewMode", e.CommandName.ToString.ToUpper)
            'Dim lblKode As Label = CType(e.Item.FindControl("lblKode"), Label)
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            profHeader = New ProfileHeaderFacade(User).Retrieve(CInt(e.CommandArgument))
            sHelper.SetSession("ProfileHeader", profHeader)
            Response.Redirect("FrmProfileView.aspx")
        ElseIf e.CommandName = "Edit" Then
            sHelper.SetSession("ViewMode", e.CommandName.ToString.ToUpper)
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            sHelper.SetSession("PHid", lblid.Text)
            Response.Redirect("FrmProfileView.aspx")
        End If
    End Sub

    Private Sub dtgProfileHeader_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgProfileHeader.PageIndexChanged
        dtgProfileHeader.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgProfileHeader.CurrentPageIndex)
    End Sub

    Private Sub dtgProfileHeader_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgProfileHeader.SortCommand
        If e.SortExpression = viewstate("SortColPH") Then
            If viewstate("SortDirectionPH") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirectionPH", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirectionPH", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColPH", e.SortExpression)
        BindDataGrid(dtgProfileHeader.CurrentPageIndex)
    End Sub
End Class
