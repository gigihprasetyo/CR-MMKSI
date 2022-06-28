Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI
Imports Ktb.DNet.Security


Public Class FrmProvince
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgProvince As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtProvince As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProvCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Private _sessHelper As SessionHelper = New SessionHelper
    Private bPrivilegeChangesProvince As Boolean
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDatagrid(0)
        End If
        'Put user code to initialize the page here
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = bPrivilegeChangesProvince
        btnBatal.Visible = bPrivilegeChangesProvince
    End Sub

    Private Sub ActivateUserPrivilege()
        
        bPrivilegeChangesProvince = SecurityProvider.Authorize(Context.User, SR.ChangeProvince_Privilege)
        
        If Not SecurityProvider.Authorize(Context.User, SR.ViewProvince_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Propinsi")
        End If
    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "ProvinceCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgProvince.DataSource = New ProvinceFacade(User).RetrieveActiveList(indexPage + 1, dtgProvince.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgProvince.VirtualItemCount = totalRow
            dtgProvince.DataBind()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objProvince As Province = New Province
        Dim objProvinceFacade As ProvinceFacade = New ProvinceFacade(User)
        Dim nResult As Integer = -1

        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtProvCode.Text = String.Empty Then
                If objProvinceFacade.ValidateCode(txtProvCode.Text) <= 0 Then
                    objProvince.ProvinceCode = txtProvCode.Text
                    objProvince.ProvinceName = txtProvince.Text
                    nResult = New ProvinceFacade(User).Insert(objProvince)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kode Propinsi"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode Propinsi"))
            End If
        Else
            Try
                nResult = UpdateProvince()
            Catch ex As Exception
                nResult = -1
            End Try

            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgProvince.CurrentPageIndex = 0
        BindDatagrid(dtgProvince.CurrentPageIndex)
    End Sub

    Private Function UpdateProvince() As Integer
        Dim objProvince As Province = CType(Session.Item("vsProvince"), Province)
        objProvince.ProvinceName = txtProvince.Text
        Return New ProvinceFacade(User).Update(objProvince)
    End Function

    Private Sub dtgProvince_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgProvince.ItemDataBound
        'If Not e.Item.DataItem Is Nothing Then
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgProvince.CurrentPageIndex * dtgProvince.PageSize)
        End If

        'tambahan Privilege
        ActivateUserPrivilege()
        If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = bPrivilegeChangesProvince
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = bPrivilegeChangesProvince
        End If

        'End If

    End Sub

    Private Sub dtgProvince_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgProvince.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtProvCode.ReadOnly = True
            txtProvince.ReadOnly = True
            ViewProvince(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewProvince(e.Item.Cells(0).Text, True)
            dtgProvince.SelectedIndex = e.Item.ItemIndex
            txtProvCode.ReadOnly = True
            txtProvince.ReadOnly = False

        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteProvince(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        End If
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal ProvinceID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If DomainType.FullName = GetType(EndCustomer).FullName Then
            criterias.opAnd(New Criteria(DomainType, "Customer.City.Province", MatchType.Exact, ProvinceID))
        Else
            criterias.opAnd(New Criteria(DomainType, "Province", MatchType.Exact, ProvinceID))
        End If

        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function


    Private Sub DeleteProvince(ByVal nID As Integer)
        'Dim ArryList As ArrayList = New ArrayList
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Province.ID", MatchType.Exact, nID))
        'ArryList = New DealerFacade(User).Retrieve(criterias)

        Dim iRecordCount As Integer = 0

        If New HelperFacade(User, GetType(EndCustomer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(EndCustomer), nID), _
            CreateAggreateForCheckRecord(GetType(EndCustomer))) Then
            iRecordCount = iRecordCount + 1
        End If
        If New HelperFacade(User, GetType(City)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(City), nID), _
            CreateAggreateForCheckRecord(GetType(City))) Then
            iRecordCount = iRecordCount + 1
        End If
        If New HelperFacade(User, GetType(Dealer)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(Dealer), nID), _
            CreateAggreateForCheckRecord(GetType(Dealer))) Then
            iRecordCount = iRecordCount + 1
        End If

        If iRecordCount > 0 Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objProvince As Province = New ProvinceFacade(User).Retrieve(nID)
            'objProvince.RowStatus = DBRowStatus.Deleted
            'Dim nResult = New ProvinceFacade(User).Update(objProvince)
            Dim facade As ProvinceFacade = New ProvinceFacade(User)
            facade.DeleteFromDB(objProvince)
            dtgProvince.CurrentPageIndex = 0
            BindDatagrid(dtgProvince.CurrentPageIndex)
        End If
    End Sub

    Private Sub ViewProvince(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objProvince As Province = New ProvinceFacade(User).Retrieve(nID)
        If Not objProvince Is Nothing Then
            _sessHelper.SetSession("vsProvince", objProvince)
            'ViewState.Add("vsProvince", objProvince)
            txtProvCode.Text = objProvince.ProvinceCode
            txtProvince.Text = objProvince.ProvinceName
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If
        
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState.Clear()
        Response.Redirect("../default.aspx")
    End Sub

    Private Sub ClearData()
        txtProvCode.Text = String.Empty
        txtProvince.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        txtProvCode.ReadOnly = False
        txtProvince.ReadOnly = False
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgProvince_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgProvince.SelectedIndexChanged

    End Sub

    Private Sub dtgProvince_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgProvince.SortCommand
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

        dtgProvince.SelectedIndex = -1
        dtgProvince.CurrentPageIndex = 0
        BindDatagrid(dtgProvince.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgProvince_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgProvince.PageIndexChanged
        dtgProvince.SelectedIndex = -1
        dtgProvince.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgProvince.CurrentPageIndex)
        ClearData()
    End Sub
End Class

