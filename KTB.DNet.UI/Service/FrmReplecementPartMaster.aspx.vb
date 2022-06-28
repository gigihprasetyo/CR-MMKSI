Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security

Public Class FrmReplecementPartMaster
    Inherits System.Web.UI.Page
    Dim sHPart As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgPart As System.Web.UI.WebControls.DataGrid
    Private bPrivilegeViewPart As Boolean = False

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
        'btnSimpan.Visible = bPrivilegeChangeGroup
        'btnBatal.Visible = bPrivilegeChangeGroup
    End Sub

    Private Sub ActivateUserPrivilege()
        'bPrivilegeViewPart = SecurityProvider.Authorize(Context.User, "")


        'If Not bPrivilegeViewPart Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=SERVICE - Penggatian Part Master")
        'End If
    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgPart.DataSource = New ReplecementPartMasterFacade(User).RetrieveActiveList(indexPage + 1, dtgPart.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgPart.VirtualItemCount = totalRow
            'bPrivilegeChangeGroup = SecurityProvider.Authorize(Context.User, SR.ChangeGroup_Privilege)
            dtgPart.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        Me.txtCode.ReadOnly = False
        Me.txtName.ReadOnly = False
        Me.txtName.Text = String.Empty
        Me.txtCode.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        If dtgPart.Items.Count > 0 Then
            dtgPart.SelectedIndex = -1
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objReplecementPartMaster As ReplecementPartMaster = New ReplecementPartMaster
        Dim objReplecementPartMasterFacade As ReplecementPartMasterFacade = New ReplecementPartMasterFacade(User)
        Dim nResult = -1
        Me.txtCode.Enabled = True
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If objReplecementPartMasterFacade.ValidateCode(Me.txtCode.Text) = 0 Then
                objReplecementPartMaster.Code = Me.txtCode.Text
                objReplecementPartMaster.PartDescription = Me.txtName.Text
                nResult = New ReplecementPartMasterFacade(User).Insert(objReplecementPartMaster)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else

                    MessageBox.Show(SR.SaveSuccess)
                End If
            Else
                MessageBox.Show(SR.DataIsExist("Kode Item "))
            End If
        Else
            UpdateGroup()
        End If

        ClearData()
        dtgPart.CurrentPageIndex = 0
        BindDataGrid(dtgPart.CurrentPageIndex)
    End Sub



    Private Sub dtgPart_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPart.ItemDataBound
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgPart.CurrentPageIndex * dtgPart.PageSize)
        End If

        'tambahan Privilege
        ActivateUserPrivilege()
        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            'CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = bPrivilegeChangeGroup
        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            'CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = bPrivilegeChangeGroup
        End If

    End Sub

    Private Sub UpdateGroup()
        Dim objReplecementPartMaster As ReplecementPartMaster = CType(sHPart.GetSession("vsPart"), ReplecementPartMaster)
        objReplecementPartMaster.PartDescription = Me.txtName.Text
        objReplecementPartMaster.Code = Me.txtCode.Text
        Try
            Dim nResult = New ReplecementPartMasterFacade(User).Update(objReplecementPartMaster)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try

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

    Private Sub DeletePart(ByVal nID As Integer)

        Dim objReplecementPartMaster As ReplecementPartMaster = New ReplecementPartMasterFacade(User).Retrieve(nID)

        If Not objReplecementPartMaster Is Nothing Then
            Dim facade As ReplecementPartMasterFacade = New ReplecementPartMasterFacade(User)
            Try
                facade.DeleteFromDB(objReplecementPartMaster)
                MessageBox.Show(SR.DeleteSucces)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            dtgPart.CurrentPageIndex = 0
            BindDataGrid(dtgPart.CurrentPageIndex)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If


    End Sub



    Private Sub ViewPart(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objReplecementPartMaster As ReplecementPartMaster = New ReplecementPartMasterFacade(User).Retrieve(nID)
        If Not objReplecementPartMaster Is Nothing Then
            sHPart.SetSession("vsPart", objReplecementPartMaster)
            Me.txtCode.Text = objReplecementPartMaster.Code
            Me.txtName.Text = objReplecementPartMaster.PartDescription
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If

    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub



    Private Sub dtgPart_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dtgPart.SelectedIndex = -1
        dtgPart.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPart.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgPart_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPart.SortCommand
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
        dtgPart.SelectedIndex = -1
        BindDataGrid(dtgPart.CurrentPageIndex)
    End Sub

    Private Sub dtgPart_PageIndexChanged1(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPart.PageIndexChanged
        dtgPart.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPart.CurrentPageIndex)
    End Sub

    Private Sub dtgPart_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPart.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewPart(e.Item.Cells(0).Text, False)
            Me.txtCode.ReadOnly = True
            Me.txtName.ReadOnly = True
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewPart(e.Item.Cells(0).Text, True)
            dtgPart.SelectedIndex = e.Item.ItemIndex
            Me.txtCode.ReadOnly = True
            Me.txtName.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            DeletePart(e.Item.Cells(0).Text)
        End If
    End Sub
End Class
