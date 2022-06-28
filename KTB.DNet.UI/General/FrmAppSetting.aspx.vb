#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
#End Region

Public Class FrmAppSetting
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False
    Private count As Integer = 0

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeArea As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgArea As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNamaArea As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents test As System.Web.UI.HtmlControls.HtmlAnchor
    Protected WithEvents txtNilai As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"
    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ENHAppSettingEdit_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ENHAppSettingView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Setting Aplikasi")
        End If

    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgArea.DataSource = New AppSettingFacade(User).RetrieveActiveList(indexPage + 1, dtgArea.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgArea.VirtualItemCount = totalRow
            dtgArea.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        txtKodeArea.Text() = String.Empty
        txtNamaArea.Text() = String.Empty
        txtNilai.Text = String.Empty
        btnSimpan.Enabled = True
        txtKodeArea.ReadOnly = False
        txtNamaArea.ReadOnly = False
        txtNilai.ReadOnly = False
        dtgArea.SelectedIndex = -1
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub UpdateAppSetting()
        Dim objAppSetting As AppSetting = CType(Session.Item("vsAppSetting"), AppSetting)
        objAppSetting.Description = txtNamaArea.Text.Trim
        objAppSetting.Nilai = txtNilai.Text.Trim

        Dim nResult As Integer = -1
        Try
            nResult = New AppSettingFacade(User).Update(objAppSetting)
            MessageBox.Show(SR.UpdateSucces)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal AppSettingID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "AppSetting", MatchType.Exact, AppSettingID))
        Return criterias
    End Function

    Private Sub DeleteAppSetting(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0
        Dim nResult As Integer = -1
        Try
            Dim objAppSetting As AppSetting = New AppSettingFacade(User).Retrieve(nID)
            Dim facade As AppSettingFacade = New AppSettingFacade(User)
            If Not objAppSetting Is Nothing Then
                nResult = facade.DeleteFromDB(objAppSetting)
                MessageBox.Show(SR.DeleteSucces)
            End If
            dtgArea.CurrentPageIndex = 0
            BindDataGrid(dtgArea.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
            dtgArea.SelectedIndex = -1
            ClearData()
        End Try

    End Sub

    Private Sub ViewAppSetting(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objAppSetting As AppSetting = New AppSettingFacade(User).Retrieve(nID)
        If Not objAppSetting Is Nothing Then
            Session.Add("vsAppSetting", objAppSetting)
            txtKodeArea.Text = objAppSetting.Code
            txtNamaArea.Text = objAppSetting.Description
            txtNilai.Text = objAppSetting.Nilai
            Me.btnSimpan.Enabled = EditStatus
        Else
            If EditStatus Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.ViewFail)
            End If
            dtgArea.SelectedIndex = -1
            ClearData()
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDataGrid(0)
            InitiatePage()
        End If
        AssignControlAttribute()
    End Sub

    Private Sub AssignControlAttribute()
        btnSimpan.Attributes.Add("onclick", New CommonFunction().PreventDoubleClick(btnSimpan))
    End Sub

    Private Sub InsertAppSetting()
        Dim objAppSetting As AppSetting = New AppSetting
        Dim objAppSettingFacade As AppSettingFacade = New AppSettingFacade(User)
        Dim nResult As Integer = -1
        If Not txtKodeArea.Text = String.Empty Then
            If objAppSettingFacade.ValidateCode(txtKodeArea.Text) <= 0 Then
                objAppSetting.Code = txtKodeArea.Text.Trim
                objAppSetting.Description = txtNamaArea.Text.Trim
                objAppSetting.Nilai = txtNilai.Text.Trim
                nResult = New AppSettingFacade(User).Insert(objAppSetting)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                End If
            Else
                MessageBox.Show(SR.DataIsExist("Kode Area 1"))
            End If
        Else
            MessageBox.Show(SR.GridIsEmpty("Kode Area 1"))
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        txtKodeArea.ReadOnly = False
        txtNilai.ReadOnly = False

        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertAppSetting()
        Else
            UpdateAppSetting()
        End If
        ClearData()
        dtgArea.CurrentPageIndex = 0
        BindDataGrid(dtgArea.CurrentPageIndex)
    End Sub

    Private Sub dtgArea_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgArea.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgArea.CurrentPageIndex * dtgArea.PageSize)
        End If

        If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
        End If
    End Sub

    Private Sub dtgArea_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgArea.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewAppSetting(e.Item.Cells(0).Text, False)
            txtKodeArea.ReadOnly = True
            txtNamaArea.ReadOnly = True
            txtNilai.ReadOnly = True
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewAppSetting(e.Item.Cells(0).Text, True)
            dtgArea.SelectedIndex = e.Item.ItemIndex
            txtKodeArea.ReadOnly = True
            txtNamaArea.ReadOnly = False
            txtNilai.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            count += 1
            If count = 1 Then
                DeleteAppSetting(e.Item.Cells(0).Text)
                ClearData()
            End If
        End If
    End Sub

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState.Clear()
        Response.Redirect("../default.aspx")
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        txtKodeArea.ReadOnly = False
    End Sub

    Private Sub dtgArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgArea.SelectedIndexChanged

    End Sub

    Private Sub dtgArea_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgArea.SortCommand
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

        dtgArea.SelectedIndex = -1
        dtgArea.CurrentPageIndex = 0
        BindDataGrid(dtgArea.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgArea_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgArea.PageIndexChanged
        dtgArea.SelectedIndex = -1
        dtgArea.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgArea.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

End Class