Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class FrmCustomerGroup
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents dtgCustomerGroup As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCustomerGroupName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustomerGroupCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustomerGroupDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Private _sessHelper As SessionHelper = New SessionHelper
    Private bPrivilegeChangesCustomerGroup As Boolean
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private _input As Boolean
    Private _edit As Boolean = True

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            InitiatePage()
        End If
        'Put user code to initialize the page here
        txtCustomerGroupCode.MaxLength = 3
    End Sub

#Region "public function"
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objCustomerGroup As CustomerGroup = New CustomerGroup
        Dim objCustomerGroupFacade As CustomerGroupFacade = New CustomerGroupFacade(User)
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCustomerGroupCode.Text = String.Empty Then
                If objCustomerGroupFacade.ValidateCode(txtCustomerGroupCode.Text) <= 0 Then
                    objCustomerGroup.Code = txtCustomerGroupCode.Text
                    objCustomerGroup.Name = txtCustomerGroupName.Text
                    objCustomerGroup.Description = txtCustomerGroupDescription.Text
                    nResult = New CustomerGroupFacade(User).Insert(objCustomerGroup)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kode"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode"))
            End If
        Else
            nResult = UpdateCustomerGroup()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgCustomerGroup.CurrentPageIndex = 0
        BindDatagrid(dtgCustomerGroup.CurrentPageIndex)
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CustomerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        CreateCriteria(criterias)
        _sessHelper.SetSession("CRITERIAS", criterias)
        dtgCustomerGroup.CurrentPageIndex = 0
        BindDatagrid(dtgCustomerGroup.CurrentPageIndex)
        btnSimpan.Enabled = True
    End Sub

#End Region

#Region "private function"
    Private Sub CheckPrivilege()
        Dim BCustomerGroupPrivilege As Boolean = SecurityProvider.Authorize(Context.User, SR.CustomerGroup_List_Privilege)
        If Not BCustomerGroupPrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FLEET MANAGEMENT - Customer Group")
        End If

        _input = SecurityProvider.Authorize(Context.User, SR.CustomerGroup_Input_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.CustomerGroup_Edit_Privilege)
        btnSimpan.Visible = _input
    End Sub

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()
        txtCustomerGroupCode.Text() = String.Empty
        txtCustomerGroupName.Text = String.Empty
        txtCustomerGroupDescription.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        txtCustomerGroupCode.ReadOnly = False
        txtCustomerGroupName.ReadOnly = False
        txtCustomerGroupDescription.ReadOnly = False
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        If (indexPage >= 0) Then
            Dim criterias As CriteriaComposite = _sessHelper.GetSession("CRITERIAS")
            arrList = New CustomerGroupFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgCustomerGroup.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgCustomerGroup.DataSource = arrList
            dtgCustomerGroup.VirtualItemCount = totalRow
            dtgCustomerGroup.DataBind()
        End If
    End Sub

    Private Sub dtgCustomerGroup_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCustomerGroup.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As CustomerGroup = CType(e.Item.DataItem, CustomerGroup)
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")

                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                If RowValue.ID = 1 Then
                    If Not IsNothing(lbtnEdit) Then
                        lbtnEdit.Visible = False
                    End If
                    If Not IsNothing(lbtnDelete) Then
                        lbtnDelete.Visible = False
                    End If
                Else
                    lbtnEdit.Visible = _edit
                    lbtnDelete.Visible = _input
                   
                End If

            End If
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCustomerGroup.CurrentPageIndex * dtgCustomerGroup.PageSize)
        End If
    End Sub

    Private Sub dtgCustomerGroupe_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCustomerGroup.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCustomerGroupCode.ReadOnly = True
            txtCustomerGroupName.ReadOnly = True
            txtCustomerGroupDescription.ReadOnly = True
            ViewCustomerGroup(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewCustomerGroup(e.Item.Cells(0).Text, True)
            dtgCustomerGroup.SelectedIndex = e.Item.ItemIndex

            txtCustomerGroupCode.ReadOnly = True
            txtCustomerGroupName.ReadOnly = False
            txtCustomerGroupDescription.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim strValidateDelete As String = ValidateDelete(e.Item.Cells(0).Text)
                If strValidateDelete <> String.Empty Then
                    MessageBox.Show(strValidateDelete)
                Else
                    DeleteCustomerGroup(e.Item.Cells(0).Text)
                End If

                dtgCustomerGroup.CurrentPageIndex = 0
                BindDatagrid(dtgCustomerGroup.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()

        End If
    End Sub

    Private Function ValidateDelete(ByVal fleetCustomerID As Integer) As String
        Dim str As String = String.Empty

        ' validate customer group id related to fleet customer
        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt.opAnd(New Criteria(GetType(FleetCustomer), "CustomerGroup", MatchType.Exact, fleetCustomerID))
        Dim facFleet As FleetCustomerFacade = New FleetCustomerFacade(User)
        Dim arrListFleet As ArrayList = facFleet.Retrieve(crt)
        If arrListFleet.Count > 0 Then
            str = "Grup konsumen gagal dihapus. Data sudah digunakan pada Fleet Customer."
        End If

        Return str
    End Function

    Private Sub ViewCustomerGroup(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objCustomerGroup As CustomerGroup = New CustomerGroupFacade(User).Retrieve(nID)
        If Not objCustomerGroup Is Nothing Then
            _sessHelper.SetSession("vsCustomerGroup", objCustomerGroup)

            txtCustomerGroupCode.Text = objCustomerGroup.Code
            txtCustomerGroupName.Text = objCustomerGroup.Name
            txtCustomerGroupDescription.Text = objCustomerGroup.Description
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If
    End Sub

    Private Sub DeleteCustomerGroup(ByVal nID As Integer)

        Dim objCustomerGroup As CustomerGroup = New CustomerGroupFacade(User).Retrieve(nID)
        If Not objCustomerGroup Is Nothing Then
            objCustomerGroup.RowStatus = DBRowStatus.Deleted
            Dim nResult = New CustomerGroupFacade(User).Update(objCustomerGroup)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If

    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal CustomerGroupID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "CustomerGroup", MatchType.Exact, CustomerGroupID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function UpdateCustomerGroup() As Integer
        Dim objCustomerGroup As CustomerGroup = CType(Session.Item("vsCustomerGroup"), CustomerGroup)
        objCustomerGroup.Name = txtCustomerGroupName.Text
        objCustomerGroup.Description = txtCustomerGroupDescription.Text
        Try
            Return New CustomerGroupFacade(User).Update(objCustomerGroup)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)

        If txtCustomerGroupCode.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerGroup), "Code", MatchType.Exact, txtCustomerGroupCode.Text.Trim))
        End If
        If txtCustomerGroupName.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerGroup), "Name", MatchType.[Partial], txtCustomerGroupName.Text.Trim))
        End If
        If txtCustomerGroupDescription.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerGroup), "Description", MatchType.[Partial], txtCustomerGroupDescription.Text.Trim))
        End If
    End Sub

    Private Sub dtgCustomerGroup_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCustomerGroup.SortCommand
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

        dtgCustomerGroup.SelectedIndex = -1
        dtgCustomerGroup.CurrentPageIndex = 0
        BindDatagrid(dtgCustomerGroup.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

    Private Sub dtgCustomerGroup_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgCustomerGroup.PageIndexChanged
        dtgCustomerGroup.SelectedIndex = -1
        dtgCustomerGroup.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgCustomerGroup.CurrentPageIndex)
    End Sub
End Class