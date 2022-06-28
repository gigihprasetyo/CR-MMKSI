#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
#End Region

Public Class FrmMainArea
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeArea As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgMainArea As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNamaArea As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPIC As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents test As System.Web.UI.HtmlControls.HtmlAnchor

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
        ViewState("CurrentSortColumn") = "AreaCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ChangeArea1_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ViewArea1_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - MainArea")
        End If

    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgMainArea.DataSource = New MainAreaFacade(User).RetrieveActiveList(indexPage + 1, dtgMainArea.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgMainArea.VirtualItemCount = totalRow
            dtgMainArea.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        txtKodeArea.Text() = String.Empty
        txtNamaArea.Text() = String.Empty
        txtPIC.Text() = String.Empty
        btnSimpan.Enabled = True
        txtKodeArea.ReadOnly = False
        txtNamaArea.ReadOnly = False
        txtPIC.ReadOnly = False
        dtgMainArea.SelectedIndex = -1
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub UpdateArea()
        Dim objMainArea As MainArea = CType(Session.Item("vsMainArea"), MainArea)
        objMainArea.Description = txtNamaArea.Text
        objMainArea.PICSales = txtPIC.Text
        Dim nResult As Integer = -1
        Try
            nResult = New MainAreaFacade(User).Update(objMainArea)
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
        ByVal MainAreaID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "MainArea", MatchType.Exact, MainAreaID))
        Return criterias
    End Function

    Private Sub DeleteArea(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        If New HelperFacade(User, GetType(Dealer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(Dealer), nID), _
            CreateAggreateForCheckRecord(GetType(Dealer))) Then
            iRecordCount = iRecordCount + 1
        End If

        Dim nResult As Integer = -1
        If iRecordCount > 0 Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Try
                Dim objMainArea As MainArea = New MainAreaFacade(User).Retrieve(nID)
                Dim facade As MainAreaFacade = New MainAreaFacade(User)
                'facade.DeleteFromDB(objMainArea)
                facade.Delete(objMainArea)
                MessageBox.Show(SR.DeleteSucces)
                dtgMainArea.CurrentPageIndex = 0
                BindDataGrid(dtgMainArea.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
                dtgMainArea.SelectedIndex = -1
                ClearData()
            End Try
        End If
    End Sub

    Private Sub ViewArea(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objMainArea As MainArea = New MainAreaFacade(User).Retrieve(nID)
        If Not objMainArea Is Nothing Then
            'Todo session
            Session.Add("vsMainArea", objMainArea)
            txtKodeArea.Text = objMainArea.AreaCode
            txtNamaArea.Text = objMainArea.Description
            txtPIC.Text = objMainArea.PICSales
            Me.btnSimpan.Enabled = EditStatus
        Else
            If EditStatus Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.ViewFail)
            End If
            dtgMainArea.SelectedIndex = -1
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

    Private Sub InsertArea()
        Dim objMainArea As MainArea = New MainArea
        Dim objMainAreaFacade As MainAreaFacade = New MainAreaFacade(User)
        Dim nResult As Integer = -1
        If Not txtKodeArea.Text = String.Empty Then
            If objMainAreaFacade.ValidateCode(txtKodeArea.Text) <= 0 Then
                objMainArea.AreaCode = txtKodeArea.Text
                objMainArea.Description = txtNamaArea.Text
                objMainArea.PICSales = txtPIC.Text
                nResult = New MainAreaFacade(User).Insert(objMainArea)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                End If
            Else
                MessageBox.Show(SR.DataIsExist("Kode Main Area"))
            End If
        Else
            MessageBox.Show(SR.GridIsEmpty("Kode Main Area"))
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        txtKodeArea.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertArea()
        Else
            UpdateArea()
        End If
        ClearData()
        dtgMainArea.CurrentPageIndex = 0
        BindDataGrid(dtgMainArea.CurrentPageIndex)
    End Sub

    Private Sub dtgMainArea_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMainArea.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMainArea.CurrentPageIndex * dtgMainArea.PageSize)
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

    Private Sub dtgMainArea_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMainArea.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewArea(e.Item.Cells(0).Text, False)
            txtKodeArea.ReadOnly = True
            txtNamaArea.ReadOnly = True
            txtPIC.ReadOnly = True
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewArea(e.Item.Cells(0).Text, True)
            dtgMainArea.SelectedIndex = e.Item.ItemIndex
            txtKodeArea.ReadOnly = True
            txtNamaArea.ReadOnly = False
            txtPIC.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeleteArea(e.Item.Cells(0).Text)
            ClearData()
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

    Private Sub dtgMainArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgMainArea.SelectedIndexChanged

    End Sub

    Private Sub dtgMainArea_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMainArea.SortCommand
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

        dtgMainArea.SelectedIndex = -1
        dtgMainArea.CurrentPageIndex = 0
        BindDataGrid(dtgMainArea.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgMainArea_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMainArea.PageIndexChanged
        dtgMainArea.SelectedIndex = -1
        dtgMainArea.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgMainArea.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

End Class