Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class FrmVehicleClass
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dgVechileClass As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private _sessHelper As New SessionHelper

#Region "Cek Privilege"
    Dim bCekPriv As Boolean = SecurityProvider.Authorize(context.User, SR.ClassEdit_Privilege)
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ClassView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MAINTENANCE - Kelas")
        End If
    End Sub

    Private Sub ActivateBtnPriv()
        btnSave.Enabled = bCekPriv
        btnCancel.Enabled = bCekPriv
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        ActivateBtnPriv()
        If Not IsPostBack() Then
            ViewState("CurrentSortColumn") = "Code"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Viewstate.Add("vsproses", "Insert")
            _sessHelper.SetSession("BINDSTATUS", "NOTHING")
            BindData(0)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objVC As New VehicleClass
        Dim n As Integer
        If CheckDataValid(txtKode.Text.Trim) Then
            If CType(ViewState("vsproses"), String) = "Insert" Then
                objVC.Description = txtDeskripsi.Text
                objVC.Code = txtKode.Text.Trim
                objVC.Status = EnumStatusSPL.StatusSPL.Aktif

                n = New VehicleClassFacade(User).Insert(objVC)
            Else
                objVC = CType(_sessHelper.GetSession("EDITVC"), VehicleClass)
                objVC.Description = txtDeskripsi.Text.Trim
                objVC.Code = txtKode.Text.Trim
                n = New VehicleClassFacade(User).Update(objVC)
                Viewstate.Add("vsproses", "Insert")
            End If
            If n >= 0 Then
                MessageBox.Show("Simpan sukses")
                BindData(dgVechileClass.CurrentPageIndex)
            Else
                MessageBox.Show("Simpan gagal")
            End If
            ClearData()
            dgVechileClass.SelectedIndex = -1
            _sessHelper.SetSession("BINDSTATUS", "NOTHING")
        End If

    End Sub
    Private Sub ClearData()
        _sessHelper.RemoveSession("EDITVC")
        txtKode.Text = String.Empty
        txtDeskripsi.Text = String.Empty
        dgVechileClass.SelectedIndex = -1
    End Sub

    Private Sub BindData(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VehicleClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        arrList = New VehicleClassFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgVechileClass.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgVechileClass.DataSource = arrList
        dgVechileClass.VirtualItemCount = totalRow
        dgVechileClass.DataBind()
    End Sub
    Private Sub BindResult(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VehicleClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKode.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(VehicleClass), "Code", MatchType.[Partial], txtKode.Text.Trim))
        End If
        If txtDeskripsi.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(VehicleClass), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
        End If

        Dim arrList As ArrayList = New VehicleClassFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgVechileClass.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgVechileClass.DataSource = arrList
        dgVechileClass.VirtualItemCount = totalRow
        dgVechileClass.DataBind()
        _sessHelper.SetSession("SortViewVC", criterias)
    End Sub

    Private Sub dgVechileClass_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgVechileClass.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                ViewState.Add("vsproses", "Edit")
                setControlUpdate(Integer.Parse(e.Item.Cells(0).Text))
                dgVechileClass.SelectedIndex = e.Item.ItemIndex
            Case "Activation"

                Dim objVC As VehicleClass = New VehicleClassFacade(User).Retrieve(Integer.Parse(e.Item.Cells(0).Text))
                If objVC.Status = 0 Then
                    objVC.Status = 1
                Else
                    objVC.Status = 0
                End If
                If New VehicleClassFacade(User).Update(objVC) Then
                    MessageBox.Show("Status " + CType(objVC.Status, EnumStatusSPL.StatusSPL).ToString.Replace("_", " "))
                    BindData(dgVechileClass.CurrentPageIndex)
                End If
        End Select
    End Sub
    Private Sub setControlUpdate(ByVal id As Integer)
        Dim objVC As VehicleClass = New VehicleClassFacade(User).Retrieve(id)
        txtKode.Text = objVC.Code
        txtDeskripsi.Text = objVC.Description
        _sessHelper.SetSession("EDITVC", objVC)
    End Sub

    Private Function CheckDataValid(ByVal code As String) As Boolean
        Dim objVC As VehicleClass = CType(_sessHelper.GetSession("EDITVC"), VehicleClass)
        Dim bCheck As Boolean = True
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VehicleClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VehicleClass), "Code", MatchType.Exact, txtKode.Text.Trim))

        If CType(ViewState("vsproses"), String) = "Edit" Then
            criterias.opAnd(New Criteria(GetType(VehicleClass), "ID", MatchType.No, objVC.ID))
        End If
        Dim _arr As ArrayList = New VehicleClassFacade(User).RetrieveByCriteria(criterias)
        If _arr.Count > 0 Then
            bCheck = False
            MessageBox.Show("Duplikasi kode")
        End If
        If txtKode.Text.Trim = String.Empty Then
            bCheck = False
            MessageBox.Show("Kode masih kosong")
        End If
        If txtDeskripsi.Text.Trim = String.Empty Then
            bCheck = False
            MessageBox.Show("Deskripsi masih kosong")
        End If
        Return bCheck
    End Function

    Private Sub dgVechileClass_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgVechileClass.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objVC As VehicleClass = e.Item.DataItem
            Dim lbtnActivation As LinkButton = CType(e.Item.FindControl("lbtnActivation"), LinkButton)
            Dim lbtnInactivation As LinkButton = CType(e.Item.FindControl("lbtnInactivation"), LinkButton)
            If objVC.Status = EnumStatusSPL.StatusSPL.Aktif Then
                lbtnInactivation.Visible = True
                lbtnActivation.Visible = False
            Else
                lbtnInactivation.Visible = False
                lbtnActivation.Visible = True
            End If

        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = CType(((dgVechileClass.CurrentPageIndex * dgVechileClass.PageSize) + e.Item.ItemIndex + 1), String)   '-- Column No

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnActivation As LinkButton = CType(e.Item.FindControl("lbtnActivation"), LinkButton)
            Dim lbtnInactivation As LinkButton = CType(e.Item.FindControl("lbtnInactivation"), LinkButton)
            lbtnEdit.Visible = bCekPriv
            lbtnInactivation.Visible = bCekPriv
            lbtnActivation.Visible = bCekPriv
        End If
    End Sub

    Private Sub dgVechileClass_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgVechileClass.PageIndexChanged
        dgVechileClass.CurrentPageIndex = e.NewPageIndex
        If CType(_sessHelper.GetSession("BINDSTATUS"), String) = "CARI" Then
            BindResult(dgVechileClass.CurrentPageIndex)
        Else
            BindData(dgVechileClass.CurrentPageIndex)
        End If
        ClearData()
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dgVechileClass.CurrentPageIndex = 0
        BindResult(0)
        _sessHelper.SetSession("BINDSTATUS", "CARI")
    End Sub

    Private Sub dgVechileClass_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgVechileClass.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgVechileClass.SelectedIndex = -1
        dgVechileClass.CurrentPageIndex = 0
        bindGridSorting(dgVechileClass.CurrentPageIndex)
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            If CType(_sessHelper.GetSession("BINDSTATUS"), String) = "CARI" Then
                dgVechileClass.DataSource = New VehicleClassFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("sortViewVC"), CriteriaComposite), indexPage + 1, dgVechileClass.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(VehicleClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                dgVechileClass.DataSource = New VehicleClassFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgVechileClass.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            End If
            dgVechileClass.VirtualItemCount = totalRow
            dgVechileClass.DataBind()
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
    End Sub
End Class
