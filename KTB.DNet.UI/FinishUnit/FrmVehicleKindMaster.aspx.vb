
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmVehicleKindMaster
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvDeskripsi As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgModel As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtModel As System.Web.UI.WebControls.TextBox

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
    Private ListVehicleModel As ArrayList
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
        If ddlCategory.SelectedValue > 0 Then
            txtModel.ReadOnly = False
            txtDeskripsi.ReadOnly = False
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                InsertModel()
            Else
                UpdateModel()
                MessageBox.Show("Ubah Sukses")
            End If
            ClearData()
            dtgModel.CurrentPageIndex = 0
            BindDataGrid(dtgModel.CurrentPageIndex)
        Else
            MessageBox.Show("Data Kategori Belum dipilih")
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchVehicleModelByCriteria()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtModel.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        ClearData()
    End Sub

    Private Sub dtgModel_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgModel.ItemCommand
        If e.CommandName = "Edit" Then
            txtModel.ReadOnly = False
            txtDeskripsi.ReadOnly = False
            ViewState.Add("vsProcess", "Edit")
            ViewModel(e.Item.Cells(0).Text, True)
            ''dtgModel.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Try
                txtModel.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                DeleteVehicleKind(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        End If
    End Sub

    Private Sub dtgModel_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgModel.ItemDataBound
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        'tambahan privilege
        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            'CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = m_bFormPrivilege
            '--> Request Mas Benny di Hide Tombol Editnya Tgl 20190319
            CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = False
        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = m_bFormPrivilege
        End If
        'akhir privilege

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgModel.CurrentPageIndex * dtgModel.PageSize)
        End If

        If e.Item.ItemIndex <> -1 Then
            Try
                e.Item.Cells(2).Text = CType(ListVehicleModel.Item(e.Item.ItemIndex), VehicleKind).VehicleKindGroup.Description
            Catch ex As Exception
                e.Item.Cells(2).Text = ""
            End Try
        End If
    End Sub

    Private Sub dtgModel_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgModel.SortCommand
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

        dtgModel.SelectedIndex = -1
        dtgModel.CurrentPageIndex = 0
        BindDataGrid(dtgModel.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgModel_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgModel.PageIndexChanged
        dtgModel.SelectedIndex = -1
        dtgModel.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgModel.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        'btnSimpan.Visible = m_bFormPrivilege       '--> Request Mas Benny di Hide Tombol Simpannya Tgl 20190319
        btnSimpan.Visible = False
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ChangeKindModel_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ViewKindModel_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - MODEL")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Description"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        BindVehicleKindGroup()
    End Sub

    Private Sub BindVehicleKindGroup()
        '
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(VehicleKindGroup), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))

        Dim VehicleKindGroupColl As ArrayList = New VehicleKindGroupFacade(User).Retrieve(criterias, sortColl)

        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlCategory.Items.Add(item)
        If VehicleKindGroupColl.Count > 0 Then
            For Each cat As VehicleKindGroup In VehicleKindGroupColl
                item = New ListItem(cat.Description, cat.ID)
                ddlCategory.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub ClearData()
        txtModel.Text = String.Empty
        txtDeskripsi.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VehicleKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If ddlCategory.SelectedValue > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VehicleKind), "VehicleKindGroup.ID", MatchType.Exact, ddlCategory.SelectedValue))
            End If
            If txtModel.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VehicleKind), "Code", MatchType.[Partial], txtModel.Text.Trim))
            End If
            If txtDeskripsi.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VehicleKind), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
            End If

            ListVehicleModel = New VehicleKindFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgModel.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgModel.DataSource = ListVehicleModel
            dtgModel.VirtualItemCount = totRow
            dtgModel.DataBind()
        End If
    End Sub



    Private Function InsertModel() As Integer
        Dim objVehicleKindFacade As VehicleKindFacade = New VehicleKindFacade(User)
        Dim objVehicleKind As VehicleKind = New VehicleKind
        Dim nResult As Integer

        If objVehicleKindFacade.ValidateCode(txtModel.Text) = 0 Then
            'objVehicleKind.VehicleKindGroup = New VehicleKindGroupFacade(User).Retrieve(CType(Me.ddlCategory.SelectedValue, Integer))
            objVehicleKind.VehicleKindGroupID = CType(Me.ddlCategory.SelectedValue, Integer)
            objVehicleKind.Code = txtModel.Text
            objVehicleKind.Description = txtDeskripsi.Text
            nResult = New VehicleKindFacade(User).Insert(objVehicleKind)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Vehicle Model"))
        End If
        Return nResult
    End Function

    Private Sub UpdateModel()
        Dim objVehicleKind As VehicleKind = CType(Session.Item("vsModel"), VehicleKind)
        objVehicleKind.Code = txtModel.Text
        objVehicleKind.Description = txtDeskripsi.Text
        'objVehicleKind.VehicleKindGroup = New VehicleKindGroupFacade(User).Retrieve(CType(Me.ddlCategory.SelectedValue, Integer))
        objVehicleKind.VehicleKindGroupID = CType(Me.ddlCategory.SelectedValue, Integer)
        Dim nResult = New VehicleKindFacade(User).Update(objVehicleKind)
    End Sub

    Private Sub DeleteVehicleKind(ByVal nID As Integer)
        Dim objVehicleKind As VehicleKind = New VehicleKindFacade(User).Retrieve(nID)

        objVehicleKind.RowStatus = CType(DBRowStatus.Deleted, Short)
        Dim facade As VehicleKindFacade = New VehicleKindFacade(User)
        Dim nResult As Integer = facade.Update(objVehicleKind)

        If nResult <> -1 Then
            ClearData()
            dtgModel.CurrentPageIndex = 0
            BindDataGrid(dtgModel.CurrentPageIndex)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objVehicleKind As VehicleKind = New VehicleKindFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsModel", objVehicleKind)
        If IsNothing(objVehicleKind) Then
            txtModel.Text = ""
            txtDeskripsi.Text = ""
            Me.ddlCategory.SelectedValue = ""
        Else
            txtModel.Text = objVehicleKind.Code
            txtDeskripsi.Text = objVehicleKind.Description
            Me.ddlCategory.SelectedValue = CType(objVehicleKind.VehicleKindGroup.ID, String)
        End If

        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub SearchVehicleModelByCriteria()
        dtgModel.CurrentPageIndex = 0
        BindDataGrid(dtgModel.CurrentPageIndex)
    End Sub
#End Region

End Class
