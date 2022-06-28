Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search

Public Class FrmArea2
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

  End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeArea As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgArea As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlArea1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNamaArea As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtACFinishUnit As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtACSparePart As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtACService As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator

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
        bindddlArea()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "AreaCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub bindddlArea()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area1), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim sortColl As SortCollection = New SortCollection

        sortColl.Add(New Sort(GetType(Area1), "AreaCode", Sort.SortDirection.ASC))

        Dim arl As ArrayList = New Area1Facade(User).Retrieve(criterias, sortColl)

        ddlArea1.DataSource = arl
        ddlArea1.DataValueField = "ID"
        ddlArea1.DataTextField = "Description"
        ddlArea1.DataBind()

        ddlArea1.Items.Insert(0, New ListItem("Silakan Pilih", ""))
    End Sub


    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ChangeArea2_Privilege)
        If Not SecurityProvider.Authorize(Context.User, SR.ViewArea2_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Area2")
        End If
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgArea.DataSource = New Area2Facade(User).RetrieveActiveList(indexPage + 1, dtgArea.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgArea.VirtualItemCount = totalRow
            dtgArea.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        txtKodeArea.Text() = String.Empty
        txtNamaArea.Text() = String.Empty
        txtACFinishUnit.Text() = String.Empty
        txtACService.Text() = String.Empty
        txtACSparePart.Text() = String.Empty
        ddlArea1.ClearSelection()
        btnSimpan.Enabled = True
        txtKodeArea.ReadOnly = False
        txtNamaArea.ReadOnly = False
        txtACFinishUnit.ReadOnly = False
        txtACService.ReadOnly = False
        txtACSparePart.ReadOnly = False
        ddlArea1.Enabled = True
        dtgArea.SelectedIndex = -1
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub UpdateArea()
        Dim objArea2 As Area2 = CType(Session.Item("vsArea2"), Area2)
        objArea2.Description = txtNamaArea.Text
        objArea2.ACFinishUnit = txtACFinishUnit.Text
        objArea2.ACSparePart = txtACSparePart.Text
        objArea2.ACService = txtACService.Text
        objArea2.Area1 = New Area1Facade(User).Retrieve(CType(ddlArea1.SelectedValue, Integer))
        Dim nResult As Integer = -1
        Try
            nResult = New Area2Facade(User).Update(objArea2)
            MessageBox.Show(SR.UpdateSucces)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal Area2ID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "Area2", MatchType.Exact, Area2ID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteArea(ByVal nID As Integer)
        Dim nResult As Integer = -1

        If New HelperFacade(User, GetType(Dealer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(Dealer), nID), _
            CreateAggreateForCheckRecord(GetType(Dealer))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Try
                Dim objArea2 As Area2 = New Area2Facade(User).Retrieve(nID)
                Dim facade As Area2Facade = New Area2Facade(User)
                facade.DeleteFromDB(objArea2)
                MessageBox.Show(SR.DeleteSucces)
                dtgArea.CurrentPageIndex = 0
                BindDatagrid(dtgArea.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
                dtgArea.SelectedIndex = -1
                ClearData()
            End Try
        End If
    End Sub

    Private Sub ViewArea(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objArea2 As Area2 = New Area2Facade(User).Retrieve(nID)
        If Not objArea2 Is Nothing Then
            'Todo session
            Session.Add("vsArea2", objArea2)
            txtKodeArea.Text = objArea2.AreaCode
            txtNamaArea.Text = objArea2.Description
            txtACFinishUnit.Text = objArea2.ACFinishUnit
            txtACService.Text = objArea2.ACService
            txtACSparePart.Text = objArea2.ACSparePart
            bindddlArea()
            If Not objArea2.Area1 Is Nothing Then
                ddlArea1.SelectedValue = objArea2.Area1.ID
            End If
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

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDatagrid(0)
            InitiatePage()
        End If
    End Sub

    Private Sub InsertArea()
        Dim objArea2 As Area2 = New Area2
        Dim objArea2Facade As Area2Facade = New Area2Facade(User)
        Dim nResult As Integer = -1
        If Not txtKodeArea.Text = String.Empty Then
            If objArea2Facade.ValidateCode(txtKodeArea.Text) <= 0 Then
                objArea2.AreaCode = txtKodeArea.Text
                objArea2.Description = txtNamaArea.Text
                objArea2.ACFinishUnit = txtACFinishUnit.Text
                objArea2.ACSparePart = txtACSparePart.Text
                objArea2.ACService = txtACService.Text
                objArea2.Area1 = New Area1Facade(User).Retrieve(CType(ddlArea1.SelectedValue, Integer))
                nResult = New Area2Facade(User).Insert(objArea2)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                End If
            Else
                MessageBox.Show(SR.DataIsExist("Kode Area 2"))
            End If
        Else
            MessageBox.Show(SR.GridIsEmpty("Kode Area 2"))
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
        dtgArea.CurrentPageIndex = 0
        BindDatagrid(dtgArea.CurrentPageIndex)
    End Sub

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState.Clear()
        Response.Redirect("../default.aspx")
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        txtKodeArea.ReadOnly = False
    End Sub

    Private Sub dtgArea_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgArea.ItemDataBound
        Dim RowValue As New Area2
        RowValue = CType(e.Item.DataItem, Area2)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgArea.CurrentPageIndex * dtgArea.PageSize)
            If IsNothing(RowValue.Area1) Then
                CType(e.Item.FindControl("lblArea1"), Label).Text = ""
            Else
                CType(e.Item.FindControl("lblArea1"), Label).Text = RowValue.Area1.Description
            End If
        End If

        If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
        End If
    End Sub

    Private Sub dtgArea_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgArea.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewArea(e.Item.Cells(0).Text, False)
            txtKodeArea.ReadOnly = True
            txtNamaArea.ReadOnly = True
            txtACFinishUnit.ReadOnly = True
            txtACService.ReadOnly = True
            txtACSparePart.ReadOnly = True
            ddlArea1.Enabled = False
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewArea(e.Item.Cells(0).Text, True)
            dtgArea.SelectedIndex = e.Item.ItemIndex
            txtKodeArea.ReadOnly = True
            txtNamaArea.ReadOnly = False
            txtNamaArea.ReadOnly = False
            txtACFinishUnit.ReadOnly = False
            txtACService.ReadOnly = False
            txtACSparePart.ReadOnly = False
            ddlArea1.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            DeleteArea(e.Item.Cells(0).Text)
            ClearData()
        End If
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
        BindDatagrid(dtgArea.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgArea_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgArea.PageIndexChanged
        dtgArea.SelectedIndex = -1
        dtgArea.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgArea.CurrentPageIndex)
        ClearData()
    End Sub
#End Region

    Private Sub ddlArea1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlArea1.SelectedIndexChanged
        If ddlArea1.SelectedValue > 0 Then
            btnSimpan.Enabled = True
        Else
            btnSimpan.Enabled = False
        End If
    End Sub
End Class



