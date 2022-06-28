Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security

Public Class FrmGroup
    Inherits System.Web.UI.Page
    Dim sHDealGroup As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgGroup As System.Web.UI.WebControls.DataGrid
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtGroupCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGroupName As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private bPrivilegeChangeGroup As Boolean = False
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        InitializeComponent()
    End Sub
    'CODEGEN: This method call is required by the Web Form Designer
    'Do not modify it using the code editor.


#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = bPrivilegeChangeGroup
        btnBatal.Visible = bPrivilegeChangeGroup
    End Sub

    Private Sub ActivateUserPrivilege()
        bPrivilegeChangeGroup = SecurityProvider.Authorize(Context.User, SR.ChangeGroup_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ViewGroup_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Group")
        End If
    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "DealerGroupCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgGroup.DataSource = New DealerGroupFacade(User).RetrieveActiveList(indexPage + 1, dtgGroup.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgGroup.VirtualItemCount = totalRow
            bPrivilegeChangeGroup = SecurityProvider.Authorize(Context.User, SR.ChangeGroup_Privilege)
            dtgGroup.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        Me.txtGroupCode.ReadOnly = False
        Me.txtGroupName.ReadOnly = False
        Me.txtGroupName.Text() = String.Empty
        Me.txtGroupCode.Text() = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        If dtgGroup.Items.Count > 0 Then
            dtgGroup.SelectedIndex = -1
        End If
    End Sub
    'Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '   ViewState.Clear()
    '  Response.Redirect("../Default.aspx")
    'End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objDealerGroup As DealerGroup = New DealerGroup
        Dim objDealerGroupFacade As DealerGroupFacade = New DealerGroupFacade(User)
        Dim nResult = -1
        Me.txtGroupCode.Enabled = True
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If objDealerGroupFacade.ValidateCode(Me.txtGroupCode.Text) = 0 Then
                objDealerGroup.DealerGroupCode = Me.txtGroupCode.Text
                objDealerGroup.GroupName = Me.txtGroupName.Text
                nResult = New DealerGroupFacade(User).Insert(objDealerGroup)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else

                    MessageBox.Show(SR.SaveSuccess)
                End If
            Else
                MessageBox.Show(SR.DataIsExist("Dealer Group"))
            End If
        Else
            UpdateGroup()
        End If

        ClearData()
        dtgGroup.CurrentPageIndex = 0
        BindDataGrid(dtgGroup.CurrentPageIndex)
    End Sub

    'Private Function IsExistGroup(ByVal strGroupCode As String) As Boolean
    '    Dim objDealerGroupFacade As DealerGroupFacade = New DealerGroupFacade(User)
    '    If objDealerGroupFacade.ValidateCode(strGroupCode) > 0 Then
    '        MessageBox.Show(SR.DataIsExist("Kode Dealer Group"))
    '        Return True
    '    End If
    '    Return False
    'End Function

    'Private Function InsertGroup() As Integer
    '    Dim objDealerGroup As DealerGroup = New DealerGroup
    '    Dim nResult As Integer = -1
    '    If Not IsExistGroup(txtGroupCode.Text) Then
    '        objDealerGroup.DealerGroupCode = Me.txtGroupCode.Text
    '        objDealerGroup.GroupName = Me.txtGroupName.Text
    '        nResult = New DealerGroupFacade(User).Insert(objDealerGroup)
    '    Else
    '    End If
    '    Return nResult
    'End Function

    Private Sub dtgGroup_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgGroup.ItemDataBound
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgGroup.CurrentPageIndex * dtgGroup.PageSize)
        End If

        'tambahan Privilege
        ActivateUserPrivilege()
        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = bPrivilegeChangeGroup
        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = bPrivilegeChangeGroup
        End If

    End Sub

    Private Sub UpdateGroup()
        Dim objDealerGroup As DealerGroup = CType(sHDealGroup.GetSession("vsDealerGroup"), DealerGroup)
        objDealerGroup.GroupName = Me.txtGroupName.Text
        objDealerGroup.DealerGroupCode = Me.txtGroupCode.Text
        Try
            Dim nResult = New DealerGroupFacade(User).Update(objDealerGroup)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try

    End Sub

    Private Sub dtgGroup_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgGroup.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewGroup(e.Item.Cells(0).Text, False)
            Me.txtGroupCode.ReadOnly = True
            Me.txtGroupName.ReadOnly = True
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewGroup(e.Item.Cells(0).Text, True)
            dtgGroup.SelectedIndex = e.Item.ItemIndex
            Me.txtGroupCode.ReadOnly = True
            Me.txtGroupName.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeleteGroup(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
    ByVal DealerGroupID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "DealerGroup", MatchType.Exact, DealerGroupID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteGroup(ByVal nID As Integer)
        If New HelperFacade(User, GetType(Dealer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(Dealer), nID), _
            CreateAggreateForCheckRecord(GetType(Dealer))) Then

            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objDealerGroup As DealerGroup = New DealerGroupFacade(User).Retrieve(nID)
            If Not objDealerGroup Is Nothing Then
                Dim facade As DealerGroupFacade = New DealerGroupFacade(User)
                facade.DeleteFromDB(objDealerGroup)
                dtgGroup.CurrentPageIndex = 0
                BindDataGrid(dtgGroup.CurrentPageIndex)
            Else
                MessageBox.Show(SR.DeleteFail)
            End If
            'objDealerGroup.RowStatus = DBRowStatus.Deleted
            'Dim nResult = New DealerGroupFacade(User).Update(objDealerGroup)

        End If
    End Sub

    'Private Function DealerGroupIsUsed(ByVal nID As Integer) As Boolean
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerGroup.ID", MatchType.Exact, nID))
    '    Dim arlDealer As ArrayList = New DealerFacade(User).Retrieve(criterias)
    '    Return arlDealer.Count > 0
    'End Function

    Private Sub ViewGroup(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objDealerGroup As DealerGroup = New DealerGroupFacade(User).Retrieve(nID)
        If Not objDealerGroup Is Nothing Then
            sHDealGroup.SetSession("vsDealerGroup", objDealerGroup)
            Me.txtGroupCode.Text = objDealerGroup.DealerGroupCode
            Me.txtGroupName.Text = objDealerGroup.GroupName
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If
        
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgGroup_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgGroup.SortCommand
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

        dtgGroup.SelectedIndex = -1
        dtgGroup.CurrentPageIndex = 0
        BindDataGrid(dtgGroup.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgGroup_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgGroup.PageIndexChanged
        dtgGroup.SelectedIndex = -1
        dtgGroup.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgGroup.CurrentPageIndex)
        ClearData()
    End Sub
End Class
