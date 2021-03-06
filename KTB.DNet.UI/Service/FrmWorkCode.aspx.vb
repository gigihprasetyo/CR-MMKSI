#Region "Summary"
'// ===========================================================================		
'// Author Name   : Agus Pirnadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright ? 2005 
'// ---------------------
'// $History      : $
'// Generated on 29/09/2005
'// ===========================================================================		
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmWorkCode
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False
    Private sessHelper As New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgModel As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvDeskripsi As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " private custom variable "
    Private ListWorkCode As ArrayList
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "KodeKerja"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        BindStatus()
    End Sub

    Private Sub BindStatus()
        Dim StatusColl As ArrayList = New EnumJobPositionDescription().RetrieveStatus
        Dim item As ListItem
        item = New ListItem("Silahkan pilih", -1)
        ddlStatus.Items.Add(item)
        If StatusColl.Count > 0 Then
            For Each li As EnumDescJobPosition In StatusColl
                item = New ListItem(li.NameStatus, li.ValStatus)
                ddlStatus.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub ClearData()
        txtKode.Text = String.Empty
        txtDeskripsi.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub
    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        If txtKode.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DeskripsiKodeKerja), "KodeKerja", MatchType.Exact, txtKode.Text.Trim))
        End If
        If txtDeskripsi.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DeskripsiKodeKerja), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
        End If

    End Sub
    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            ListWorkCode = New DeskripsiWorkPositionFacade(User).RetrieveActiveList(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), indexPage + 1, _
                    dtgModel.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgModel.DataSource = ListWorkCode
            dtgModel.VirtualItemCount = totRow
            dtgModel.DataBind()
        End If
    End Sub

    Private Function InsertModel() As Integer
        Dim objDescFacade As DeskripsiWorkPositionFacade = New DeskripsiWorkPositionFacade(User)
        Dim objDesc As DeskripsiKodeKerja = New DeskripsiKodeKerja
        Dim nResult As Integer

        If objDescFacade.ValidateCode(txtKode.Text) = 0 Then
            objDesc.Status = 0
            objDesc.KodeKerja = txtKode.Text
            objDesc.Description = txtDeskripsi.Text
            nResult = New DeskripsiWorkPositionFacade(User).Insert(objDesc)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Deskripsi Kode Posisi"))
        End If
        Return nResult
    End Function

    Private Sub UpdateModel()
        Dim objDesc As DeskripsiKodeKerja = CType(Session.Item("vsDesc"), DeskripsiKodeKerja)
        objDesc.Description = txtDeskripsi.Text
        objDesc.Status = 0
        Dim nResult = New DeskripsiWorkPositionFacade(User).Update(objDesc)
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal DescID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "DeskripsiKodeKerja", MatchType.Exact, DescID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteModel(ByVal nID As Integer)
        Dim objDesc As DeskripsiKodeKerja = New DeskripsiWorkPositionFacade(User).Retrieve(nID)
        Dim facade As DeskripsiWorkPositionFacade = New DeskripsiWorkPositionFacade(User)
        facade.DeleteFromDB(objDesc)
        ClearData()
        dtgModel.CurrentPageIndex = 0
        BindDataGrid(dtgModel.CurrentPageIndex)
    End Sub

    Private Sub ViewDeskripsiKodePosisi(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objDesc As DeskripsiKodeKerja = New DeskripsiWorkPositionFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsDesc", objDesc)
        If IsNothing(objDesc) Then
            txtKode.Text = ""
            txtDeskripsi.Text = ""
            Me.ddlStatus.SelectedValue = ""
        Else
            txtKode.Text = objDesc.KodeKerja
            txtDeskripsi.Text = objDesc.Description
            Me.ddlStatus.ClearSelection()
            Me.ddlStatus.SelectedValue = CType(objDesc.Status, String)
        End If
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub SearchCodePositionByCriteria()
        dtgModel.CurrentPageIndex = 0
        BindDataGrid(dtgModel.CurrentPageIndex)
    End Sub

#End Region

#Region "EventHandler"

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        sessHelper.SetSession("CRITERIAS", criterias)
        SearchCodePositionByCriteria()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            CreateCriteria(criterias)
            sessHelper.SetSession("CRITERIAS", criterias)
            BindDataGrid(0)
        End If
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ENHServiceKodeKerjaDescEdit_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ENHServiceKodeKerjaDescView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SERVICE - Deskripsi Kode Kerja")
        End If

    End Sub

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState.Clear()
        Response.Redirect("../Default.aspx")
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        ' If ddlStatus.SelectedValue >= 0 Then
        txtKode.ReadOnly = False
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
        'Else
        '    MessageBox.Show("Data Status Belum dipilih")
        'End If
    End Sub

    Private Sub dtgModel_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgModel.ItemDataBound
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        'tambahan privilege
        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = m_bFormPrivilege
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
                Dim lblKode As Label = CType(e.Item.FindControl("lblkode"), Label)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

                lblKode.Text = CType(ListWorkCode.Item(e.Item.ItemIndex), DeskripsiKodeKerja).KodeKerja
                lblStatus.Text = CType(CType(ListWorkCode.Item(e.Item.ItemIndex), DeskripsiKodeKerja).Status, EnumJobPositionDescription.JobPosition).ToString

            Catch ex As Exception
                e.Item.Cells(2).Text = ""
                e.Item.Cells(4).Text = ""
            End Try
        End If

    End Sub

    Private Sub dtgModel_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgModel.ItemCommand
        If (e.CommandName = "View") Then
            txtKode.ReadOnly = True
            txtDeskripsi.ReadOnly = True
            ViewState.Add("vsProcess", "View")
            ViewDeskripsiKodePosisi(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            txtKode.ReadOnly = True
            txtDeskripsi.ReadOnly = False
            ViewState.Add("vsProcess", "Edit")
            ViewDeskripsiKodePosisi(e.Item.Cells(0).Text, True)
        ElseIf e.CommandName = "Delete" Then
            Try
                txtKode.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                DeleteModel(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtKode.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        ClearData()
    End Sub

    Private Sub dtgModel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgModel.SelectedIndexChanged

    End Sub

    Private Sub dtgModel_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgModel.PageIndexChanged
        dtgModel.SelectedIndex = -1
        dtgModel.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgModel.CurrentPageIndex)
        ClearData()
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

#End Region


    Private Sub dtgModel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgModel.Load

    End Sub
End Class