Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.Collections.Generic

Public Class FrmKategoriDiskon
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents dtgDiscountMaster As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCategory As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Private _sessHelper As SessionHelper = New SessionHelper
    Private bPrivilegeChangesDiscountMaster As Boolean
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
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
            BindDdlStatus()
            InitiatePage()
            btnSimpan.Enabled = False
            btnSearch_Click(Nothing, Nothing)
        End If

        'Put user code to initialize the page here
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = bPrivilegeChangesDiscountMaster
        btnBatal.Visible = bPrivilegeChangesDiscountMaster
    End Sub

    Private Sub ActivateUserPrivilege()
        bPrivilegeChangesDiscountMaster = True
        'bPrivilegeChangesDiscountMaster = SecurityProvider.Authorize(Context.User, SR.ChangeDiscountMaster_Privilege)

        'If Not SecurityProvider.Authorize(Context.User, SR.ViewDiscountMaster_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=DISCOUNT PROPOSAL - Discount Master")
        'End If
    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Status"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDdlStatus()
        Dim list As List(Of ListItem) = New List(Of ListItem)
        list.Add(New ListItem("Aktif", 1))
        list.Add(New ListItem("Non Aktif", 0))

        ddlStatus.DataSource = list
        ddlStatus.DataTextField = "Text"
        ddlStatus.DataValueField = "Value"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Pilih", ""))
    End Sub
    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgDiscountMaster.DataSource = New DiscountMasterFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("CRITERIAS"), CriteriaComposite), indexPage + 1, _
                    dtgDiscountMaster.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgDiscountMaster.VirtualItemCount = totalRow
            dtgDiscountMaster.DataBind()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objDiscountMaster As DiscountMaster = New DiscountMaster
        Dim objDiscountMasterFacade As DiscountMasterFacade = New DiscountMasterFacade(User)
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtCode.Text = String.Empty Then
                If objDiscountMasterFacade.ValidateCode(txtCode.Text) <= 0 Then
                    objDiscountMaster.Code = txtCode.Text
                    objDiscountMaster.Category = txtCategory.Text
                    objDiscountMaster.Status = ddlStatus.SelectedValue
                    objDiscountMaster.Description = txtDescription.Text
                    nResult = New DiscountMasterFacade(User).Insert(objDiscountMaster)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kode Kota"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode Kota"))
            End If
        Else
            nResult = UpdateDiscountMaster()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
            End If
        End If

        dtgDiscountMaster.CurrentPageIndex = 0
        BindDatagrid(dtgDiscountMaster.CurrentPageIndex)
    End Sub

    Private Function UpdateDiscountMaster() As Integer
        Dim objDiscountMaster As DiscountMaster = CType(Session.Item("vsDiscountMaster"), DiscountMaster)
        objDiscountMaster.Status = ddlStatus.SelectedValue
        objDiscountMaster.Category = txtCategory.Text
        objDiscountMaster.Description = txtDescription.Text
        Try
            Return New DiscountMasterFacade(User).Update(objDiscountMaster)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Sub dtgDiscountMaster_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDiscountMaster.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As DiscountMaster = CType(e.Item.DataItem, DiscountMaster)
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
            CType(e.Item.FindControl("linkButonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diAktifkan?');")
            CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diNonAktifkan?');")
            Dim actLb As LinkButton = CType(e.Item.FindControl("linkButonActive"), LinkButton)
            Dim nactLb As LinkButton = CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton)

            'tambahan Privilege
            ActivateUserPrivilege()
            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = bPrivilegeChangesDiscountMaster
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = bPrivilegeChangesDiscountMaster
            End If

            If Not e.Item.FindControl("linkButonActive") Is Nothing Then
                CType(e.Item.FindControl("linkButonActive"), LinkButton).Visible = bPrivilegeChangesDiscountMaster
            End If

            If Not e.Item.FindControl("LinkButtonNonActive") Is Nothing Then
                CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton).Visible = bPrivilegeChangesDiscountMaster
            End If

            If RowValue.Status = 0 Then
                actLb.Visible = bPrivilegeChangesDiscountMaster
                nactLb.Visible = False
            Else
                actLb.Visible = False
                nactLb.Visible = bPrivilegeChangesDiscountMaster
            End If

        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgDiscountMaster.CurrentPageIndex * dtgDiscountMaster.PageSize)
        End If
    End Sub
    Private Sub dtgDiscountMaster_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDiscountMaster.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtCode.ReadOnly = True
            txtCategory.ReadOnly = True
            ddlStatus.Enabled = False
            txtDescription.ReadOnly = True
            ViewDiscountMaster(e.Item.Cells(0).Text, False)
            dtgDiscountMaster.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewDiscountMaster(e.Item.Cells(0).Text, True)
            dtgDiscountMaster.SelectedIndex = e.Item.ItemIndex
            ddlStatus.Enabled = True
            txtCode.ReadOnly = True
            txtCategory.ReadOnly = False
            txtDescription.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteDiscountMaster(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
        ElseIf e.CommandName = "Activate" Then
            Activate(e.Item.Cells(0).Text)
            ddlStatus.Enabled = True
            txtCode.ReadOnly = False
            txtCategory.ReadOnly = False
            ClearData()
        ElseIf e.CommandName = "Deactivate" Then
            DeActivate(e.Item.Cells(0).Text)
            ddlStatus.Enabled = True
            txtCode.ReadOnly = False
            txtCategory.ReadOnly = False
            ClearData()
        End If
    End Sub

    Private Sub Activate(ByVal nID As Integer)
        Dim objDiscountMaster As DiscountMaster = New DiscountMasterFacade(User).Retrieve(nID)
        Dim facade As DiscountMasterFacade = New DiscountMasterFacade(User)
        objDiscountMaster.Status = 1
        facade.Update(objDiscountMaster)
        dtgDiscountMaster.CurrentPageIndex = 0
        BindDatagrid(dtgDiscountMaster.CurrentPageIndex)
    End Sub

    Private Sub DeActivate(ByVal nID As Integer)
        Dim objDiscountMaster As DiscountMaster = New DiscountMasterFacade(User).Retrieve(nID)
        Dim facade As DiscountMasterFacade = New DiscountMasterFacade(User)
        objDiscountMaster.Status = 0
        facade.Update(objDiscountMaster)
        dtgDiscountMaster.CurrentPageIndex = 0
        BindDatagrid(dtgDiscountMaster.CurrentPageIndex)
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal DiscountMasterID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "DiscountMaster", MatchType.Exact, DiscountMasterID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteDiscountMaster(ByVal nID As Integer)
        If New HelperFacade(User, GetType(DiscountProposalDetailApprovaltoSPL)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(DiscountProposalDetailApprovaltoSPL), nID), _
            CreateAggreateForCheckRecord(GetType(DiscountProposalDetailApprovaltoSPL))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objDiscountMaster As DiscountMaster = New DiscountMasterFacade(User).Retrieve(nID)
            If Not objDiscountMaster Is Nothing Then
                Dim nResult = New DiscountMasterFacade(User).Delete(objDiscountMaster)
                If nResult < 0 Then
                    MessageBox.Show(SR.DeleteFail)
                End If
            Else
                MessageBox.Show(SR.DeleteFail)
            End If
            dtgDiscountMaster.CurrentPageIndex = 0
            BindDatagrid(dtgDiscountMaster.CurrentPageIndex)
        End If
    End Sub

    Private Sub ViewDiscountMaster(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objDiscountMaster As DiscountMaster = New DiscountMasterFacade(User).Retrieve(nID)
        If Not objDiscountMaster Is Nothing Then
            _sessHelper.SetSession("vsDiscountMaster", objDiscountMaster)
            'ViewState.Add("vsDiscountMaster", objDiscountMaster)
            If IsNothing(objDiscountMaster.Status) Then
                ddlStatus.SelectedIndex = 0
            Else
                ddlStatus.SelectedValue = objDiscountMaster.Status
            End If
            txtCode.Text = objDiscountMaster.Code
            txtCategory.Text = objDiscountMaster.Category
            txtDescription.Text = objDiscountMaster.Description
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ViewState.Clear()
        Response.Redirect("../default.aspx")
    End Sub

    Private Sub ClearData()
        txtCode.Text() = String.Empty
        txtCategory.Text = String.Empty
        txtDescription.Text = String.Empty
        ddlStatus.SelectedIndex = 0
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        ddlStatus.Enabled = True
        txtCode.ReadOnly = False
        txtCategory.ReadOnly = False
        txtDescription.ReadOnly = False
        dtgDiscountMaster.SelectedIndex = -1
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub dtgDiscountMaster_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDiscountMaster.PageIndexChanged
        dtgDiscountMaster.SelectedIndex = -1
        dtgDiscountMaster.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgDiscountMaster.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgDiscountMaster_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDiscountMaster.SortCommand
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

        dtgDiscountMaster.SelectedIndex = -1
        dtgDiscountMaster.CurrentPageIndex = 0
        BindDatagrid(dtgDiscountMaster.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DiscountMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        _sessHelper.SetSession("CRITERIAS", criterias)
        dtgDiscountMaster.CurrentPageIndex = 0
        BindDatagrid(dtgDiscountMaster.CurrentPageIndex)
        btnSimpan.Enabled = True
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        If ddlStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DiscountMaster), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If
        If txtCode.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DiscountMaster), "Code", MatchType.Exact, txtCode.Text.Trim))
        End If
        If txtCategory.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DiscountMaster), "Category", MatchType.[Partial], txtCategory.Text.Trim))
        End If
        If txtDescription.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DiscountMaster), "Description", MatchType.[Partial], txtDescription.Text.Trim))
        End If
    End Sub
End Class

