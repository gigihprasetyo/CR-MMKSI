
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNET.Security
Imports System.Windows
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmWorkOrderType
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
    Protected WithEvents dtgWorkOrderType As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtWorkOrderType As System.Web.UI.WebControls.TextBox

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
    Private ListWorkOrderType As ArrayList
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
        If txtWorkOrderType.Text = "" Then
            MessageBox.Show("Work Order Type belum diisi")
            Return
        End If

        txtDeskripsi.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertModel()
        Else
            UpdateModel()
            MessageBox.Show("Ubah Sukses")
        End If
        ClearData()
        dtgWorkOrderType.CurrentPageIndex = 0
        BindDataGrid(dtgWorkOrderType.CurrentPageIndex)
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchWorkOrderTypeByCriteria()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtWorkOrderType.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        ClearData()
    End Sub

    Private Sub dtgWorkOrderType_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgWorkOrderType.ItemCommand
        If e.CommandName = "Edit" Then
            txtWorkOrderType.ReadOnly = False
            txtDeskripsi.ReadOnly = False
            ViewState.Add("vsProcess", "Edit")
            ViewModel(e.Item.Cells(0).Text, True)
            ''dtgWorkOrderType.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Try
                txtWorkOrderType.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                DeleteWorkOrderType(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        ElseIf e.CommandName = "Activate" Then
            Try
                Activate(e.Item.Cells(0).Text)
                txtWorkOrderType.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                ClearData()

            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        ElseIf e.CommandName = "Deactivate" Then
            Try
                DeActivate(e.Item.Cells(0).Text)

                txtWorkOrderType.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                ClearData()
            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        End If
    End Sub

    Private Sub dtgWorkOrderType_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgWorkOrderType.ItemDataBound
        Dim RowValue As AssistWorkOrderType = CType(e.Item.DataItem, AssistWorkOrderType)
        Dim actLb As LinkButton
        Dim nactLb As LinkButton
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin data ini akan dihapus?');")
        End If
        If Not e.Item.FindControl("linkButonActive") Is Nothing Then
            CType(e.Item.FindControl("linkButonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin data ini dan work order category akan diAktifkan? ');")
            actLb = CType(e.Item.FindControl("linkButonActive"), LinkButton)
        End If

        If Not e.Item.FindControl("LinkButtonNonActive") Is Nothing Then
            CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin data ini dan work order category akan diNonAktifkan?');")
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
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgWorkOrderType.CurrentPageIndex * dtgWorkOrderType.PageSize)
        End If

    End Sub

    Private Sub dtgWorkOrderType_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgWorkOrderType.SortCommand
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

        dtgWorkOrderType.SelectedIndex = -1
        dtgWorkOrderType.CurrentPageIndex = 0
        BindDataGrid(dtgWorkOrderType.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgWorkOrderType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgWorkOrderType.PageIndexChanged
        dtgWorkOrderType.SelectedIndex = -1
        dtgWorkOrderType.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgWorkOrderType.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_WorkOrderType_Edit_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_WorkOrderType_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Master - Work Order Type")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Description"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

    End Sub

    Private Sub ClearData()
        txtWorkOrderType.Text = String.Empty
        txtDeskripsi.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If txtWorkOrderType.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderType), "WorkOrderType", MatchType.[Partial], txtWorkOrderType.Text.Trim))
            End If
            If txtDeskripsi.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderType), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
            End If

            ListWorkOrderType = New AssistWorkOrderTypeFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgWorkOrderType.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgWorkOrderType.DataSource = ListWorkOrderType
            dtgWorkOrderType.VirtualItemCount = totRow
            dtgWorkOrderType.DataBind()
        End If
    End Sub


    Private Function UpdateStatusWorkOrderCategory(ByVal nID As Int32, ByVal status As Int32)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistWorkOrderCategory), "WorkOrderTypeID", MatchType.Exact, nID))
        Dim facadeCategory As AssistWorkOrderCategoryFacade = New AssistWorkOrderCategoryFacade(User)
        Dim listworkordercategory As ArrayList = facadeCategory.RetrieveByCriteria(criterias)
        facadeCategory.UpdateStatusList(listworkordercategory, status)
    End Function

    Private Sub Activate(ByVal nID As Integer)
        Dim objWorkOrderType As AssistWorkOrderType = New AssistWorkOrderTypeFacade(User).Retrieve(nID)
        Dim facade As AssistWorkOrderTypeFacade = New AssistWorkOrderTypeFacade(User)
        objWorkOrderType.Status = 1
        facade.Update(objWorkOrderType)
        dtgWorkOrderType.CurrentPageIndex = 0
        UpdateStatusWorkOrderCategory(nID, 1)
        BindDataGrid(dtgWorkOrderType.CurrentPageIndex)
    End Sub

    Private Sub DeActivate(ByVal nID As Integer)
        Dim objWorkOrderType As AssistWorkOrderType = New AssistWorkOrderTypeFacade(User).Retrieve(nID)
        Dim facade As AssistWorkOrderTypeFacade = New AssistWorkOrderTypeFacade(User)
        objWorkOrderType.Status = 0
        facade.Update(objWorkOrderType)
        dtgWorkOrderType.CurrentPageIndex = 0
        UpdateStatusWorkOrderCategory(nID, 0)
        BindDataGrid(dtgWorkOrderType.CurrentPageIndex)
    End Sub

    Private Function InsertModel() As Integer
        Dim objWorkOrderTypeFacade As AssistWorkOrderTypeFacade = New AssistWorkOrderTypeFacade(User)
        Dim objWorkOrderType As AssistWorkOrderType = New AssistWorkOrderType
        Dim nResult As Integer

        If objWorkOrderTypeFacade.ValidateCode(txtWorkOrderType.Text) = 0 Then
            objWorkOrderType.WorkOrderType = txtWorkOrderType.Text
            objWorkOrderType.Description = txtDeskripsi.Text
            objWorkOrderType.Status = 1
            nResult = New AssistWorkOrderTypeFacade(User).Insert(objWorkOrderType)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Work Order Type"))
        End If
        Return nResult
    End Function

    Private Sub UpdateModel()
        Dim objWorkOrderType As AssistWorkOrderType = CType(Session.Item("vsWorkOrderType"), AssistWorkOrderType)
        objWorkOrderType.WorkOrderType = txtWorkOrderType.Text
        objWorkOrderType.Description = txtDeskripsi.Text

        Dim nResult = New AssistWorkOrderTypeFacade(User).Update(objWorkOrderType)
    End Sub

    Private Sub DeleteWorkOrderType(ByVal nID As Integer)
        Dim objWorkOrderType As AssistWorkOrderType = New AssistWorkOrderTypeFacade(User).Retrieve(nID)

        objWorkOrderType.RowStatus = CType(DBRowStatus.Deleted, Short)
        Dim facade As AssistWorkOrderTypeFacade = New AssistWorkOrderTypeFacade(User)
        Dim nResult As Integer = facade.Update(objWorkOrderType)

        If nResult <> -1 Then
            ClearData()
            dtgWorkOrderType.CurrentPageIndex = 0
            BindDataGrid(dtgWorkOrderType.CurrentPageIndex)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objWorkOrderType As AssistWorkOrderType = New AssistWorkOrderTypeFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsWorkOrderType", objWorkOrderType)
        If IsNothing(objWorkOrderType) Then
            txtDeskripsi.Text = ""
            txtWorkOrderType.Text = ""
        Else
            txtWorkOrderType.Text = objWorkOrderType.WorkOrderType
            txtDeskripsi.Text = objWorkOrderType.Description
        End If

        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub SearchWorkOrderTypeByCriteria()
        dtgWorkOrderType.CurrentPageIndex = 0
        BindDataGrid(dtgWorkOrderType.CurrentPageIndex)
    End Sub
#End Region

End Class
