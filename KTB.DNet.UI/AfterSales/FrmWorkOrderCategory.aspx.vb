
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmWorkOrderCategory
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvDeskripsi As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgWorkOrderCategory As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtWorkOrderCategory As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlWorkOrderType As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private m_bFormPrivilege As Boolean = False
    Private ListWorkOrderCategory As ArrayList
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        If ddlWorkOrderType.SelectedValue <= 0 Then
            MessageBox.Show("Work Order Type belum dipilih")
            Return
        End If
        If txtWorkOrderCategory.Text = "" Then
            MessageBox.Show("Work Order Category belum diisi")
            Return
        End If
        txtWorkOrderCategory.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertModel()
        Else
            UpdateModel()
            MessageBox.Show("Ubah Sukses")
        End If
        ClearData()
        dtgWorkOrderCategory.CurrentPageIndex = 0
        BindDataGrid(dtgWorkOrderCategory.CurrentPageIndex)
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchWorkOrderCategoryByCriteria()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtWorkOrderCategory.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        ClearData()
    End Sub

    Private Sub dtgWorkOrderCategory_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgWorkOrderCategory.ItemCommand
        If e.CommandName = "Edit" Then
            txtWorkOrderCategory.ReadOnly = False
            txtDeskripsi.ReadOnly = False
            ViewState.Add("vsProcess", "Edit")
            ViewModel(e.Item.Cells(0).Text, True)
            ''dtgWorkOrderCategory.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Try
                txtWorkOrderCategory.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                DeleteWorkOrderCategory(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        ElseIf e.CommandName = "Activate" Then
            Try
                Activate(e.Item.Cells(0).Text)
                txtWorkOrderCategory.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                ClearData()
            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        ElseIf e.CommandName = "Deactivate" Then
            Try
                DeActivate(e.Item.Cells(0).Text)
                txtWorkOrderCategory.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                ClearData()
            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        End If
    End Sub

    Private Sub BindWorkOrderType()
        '
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.AssistWorkOrderType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderType), "Status", MatchType.Exact, 1))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(AssistWorkOrderType), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))

        Dim WorkOrderTypeColl As ArrayList = New AssistWorkOrderTypeFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlWorkOrderType.Items.Add(item)
        If WorkOrderTypeColl.Count > 0 Then
            For Each cat As AssistWorkOrderType In WorkOrderTypeColl
                item = New ListItem(cat.WorkOrderType, cat.ID)
                ddlWorkOrderType.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub dtgWorkOrderCategory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgWorkOrderCategory.ItemDataBound
        Dim RowValue As AssistWorkOrderCategory = CType(e.Item.DataItem, AssistWorkOrderCategory)
        Dim actLb As LinkButton
        Dim nactLb As LinkButton
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If
        If Not e.Item.FindControl("linkButonActive") Is Nothing Then
            CType(e.Item.FindControl("linkButonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diAktifkan?');")
            actLb = CType(e.Item.FindControl("linkButonActive"), LinkButton)
        End If

        If Not e.Item.FindControl("LinkButtonNonActive") Is Nothing Then
            CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diNonAktifkan?');")
            nactLb = CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton)

            actLb.Visible = False
            nactLb.Visible = False
            If m_bFormPrivilege = True Then
                If RowValue.Status = 0 Then
                    actLb.Visible = True
                Else
                    nactLb.Visible = True
                End If
            End If
            End If

            If Not e.Item.FindControl("btnUbah") Is Nothing Then
                CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = m_bFormPrivilege
            End If

            If Not e.Item.FindControl("btnHapus") Is Nothing Then
                CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = m_bFormPrivilege
            End If

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgWorkOrderCategory.CurrentPageIndex * dtgWorkOrderCategory.PageSize)
            End If

    End Sub

    Private Sub dtgWorkOrderCategory_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgWorkOrderCategory.SortCommand
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

        dtgWorkOrderCategory.SelectedIndex = -1
        dtgWorkOrderCategory.CurrentPageIndex = 0
        BindDataGrid(dtgWorkOrderCategory.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgWorkOrderCategory_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgWorkOrderCategory.PageIndexChanged
        dtgWorkOrderCategory.SelectedIndex = -1
        dtgWorkOrderCategory.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgWorkOrderCategory.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_WorkOrderCategory_Edit_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_WorkOrderCategory_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Master - Work Order Category")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Description"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        BindWorkOrderType()
    End Sub

    Private Sub ClearData()
        txtWorkOrderCategory.Text = String.Empty
        ddlWorkOrderType.SelectedValue = 0
        txtDeskripsi.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If ddlWorkOrderType.SelectedValue > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderCategory), "AssistWorkOrderType.ID", MatchType.Exact, ddlWorkOrderType.SelectedValue))
            End If
            If txtWorkOrderCategory.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderCategory), "WorkOrderCategory", MatchType.[Partial], txtWorkOrderCategory.Text.Trim))
            End If
            If txtDeskripsi.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderCategory), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
            End If

            ListWorkOrderCategory = New AssistWorkOrderCategoryFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgWorkOrderCategory.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgWorkOrderCategory.DataSource = ListWorkOrderCategory
            dtgWorkOrderCategory.VirtualItemCount = totRow
            dtgWorkOrderCategory.DataBind()
        End If
    End Sub

    Private Sub Activate(ByVal nID As Integer)
        Dim objWorkOrderCategory As AssistWorkOrderCategory = New AssistWorkOrderCategoryFacade(User).Retrieve(nID)
        Dim facade As AssistWorkOrderCategoryFacade = New AssistWorkOrderCategoryFacade(User)
        objWorkOrderCategory.Status = 1
        facade.Update(objWorkOrderCategory)
        dtgWorkOrderCategory.CurrentPageIndex = 0
        BindDataGrid(dtgWorkOrderCategory.CurrentPageIndex)
    End Sub

    Private Sub DeActivate(ByVal nID As Integer)
        Dim objWorkOrderCategory As AssistWorkOrderCategory = New AssistWorkOrderCategoryFacade(User).Retrieve(nID)
        Dim facade As AssistWorkOrderCategoryFacade = New AssistWorkOrderCategoryFacade(User)
        objWorkOrderCategory.Status = 0
        facade.Update(objWorkOrderCategory)
        dtgWorkOrderCategory.CurrentPageIndex = 0
        BindDataGrid(dtgWorkOrderCategory.CurrentPageIndex)
    End Sub

    Private Function InsertModel() As Integer
        Dim objWorkOrderCategoryFacade As AssistWorkOrderCategoryFacade = New AssistWorkOrderCategoryFacade(User)
        Dim objWorkOrderCategory As AssistWorkOrderCategory = New AssistWorkOrderCategory
        Dim nResult As Integer

        If objWorkOrderCategoryFacade.ValidateCode(txtWorkOrderCategory.Text, ddlWorkOrderType.SelectedValue) = 0 Then
            objWorkOrderCategory.AssistWorkOrderType = New AssistWorkOrderTypeFacade(User).Retrieve(CType(Me.ddlWorkOrderType.SelectedValue, Integer))
            objWorkOrderCategory.WorkOrderCategory = txtWorkOrderCategory.Text
            objWorkOrderCategory.Description = txtDeskripsi.Text
            objWorkOrderCategory.Status = 1
            nResult = New AssistWorkOrderCategoryFacade(User).Insert(objWorkOrderCategory)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Work Order Category"))
        End If
        Return nResult
    End Function

    Private Sub UpdateModel()
        Dim objWorkOrderCategory As AssistWorkOrderCategory = CType(Session.Item("vsWorkOrderCategory"), AssistWorkOrderCategory)
        objWorkOrderCategory.AssistWorkOrderType = New AssistWorkOrderTypeFacade(User).Retrieve(CType(Me.ddlWorkOrderType.SelectedValue, Integer))
        objWorkOrderCategory.WorkOrderCategory = txtWorkOrderCategory.Text
        objWorkOrderCategory.Description = txtDeskripsi.Text

        Dim nResult = New AssistWorkOrderCategoryFacade(User).Update(objWorkOrderCategory)
    End Sub

    Private Sub DeleteWorkOrderCategory(ByVal nID As Integer)
        Dim objWorkOrderCategory As AssistWorkOrderCategory = New AssistWorkOrderCategoryFacade(User).Retrieve(nID)

        objWorkOrderCategory.RowStatus = CType(DBRowStatus.Deleted, Short)
        Dim facade As AssistWorkOrderCategoryFacade = New AssistWorkOrderCategoryFacade(User)
        Dim nResult As Integer = facade.Update(objWorkOrderCategory)

        If nResult <> -1 Then
            ClearData()
            dtgWorkOrderCategory.CurrentPageIndex = 0
            BindDataGrid(dtgWorkOrderCategory.CurrentPageIndex)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objWorkOrderCategory As AssistWorkOrderCategory = New AssistWorkOrderCategoryFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsWorkOrderCategory", objWorkOrderCategory)
        If IsNothing(objWorkOrderCategory) Then
            txtDeskripsi.Text = ""
            ddlWorkOrderType.SelectedValue = ""
            txtWorkOrderCategory.Text = ""
        Else
            ddlWorkOrderType.SelectedValue = CType(objWorkOrderCategory.AssistWorkOrderType.ID, String)
            txtWorkOrderCategory.Text = objWorkOrderCategory.WorkOrderCategory
            txtDeskripsi.Text = objWorkOrderCategory.Description
        End If

        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub SearchWorkOrderCategoryByCriteria()
        dtgWorkOrderCategory.CurrentPageIndex = 0
        BindDataGrid(dtgWorkOrderCategory.CurrentPageIndex)
    End Sub
#End Region

End Class
