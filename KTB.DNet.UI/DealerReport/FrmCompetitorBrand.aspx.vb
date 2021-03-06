Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Public Class FrmCompetitorBrand
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dgCompetitorType As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
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
    Private mEdit As Boolean
    Private _sessHelper As New SessionHelper
    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.MerkMaintenanceView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Merk Kompetitor")
        End If
        mEdit = SecurityProvider.Authorize(Context.User, SR.MerkMaintenanceEdit_Privilege)
    End Sub



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Authorization()
        If Not IsPostBack() Then
            btnSave.Visible = mEdit
            btnCancel.Visible = mEdit
            ViewState("CurrentSortColumn") = "Code"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Viewstate.Add("vsproses", "Insert")
            _sessHelper.SetSession("BINDSTATUS", "NOTHING")
            BindDropDown()
            BindDataDefault(0)
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objCB As New CompetitorBrand
        Dim n As Integer
        If CheckDataValid(txtKode.Text.Trim) Then
            If CType(ViewState("vsproses"), String) = "Insert" Then
                objCB.Description = txtDeskripsi.Text
                objCB.Code = txtKode.Text.Trim
                objCB.Status = ddlStatus.SelectedValue
                n = New CompetitorBrandFacade(User).Insert(objCB)
            Else
                objCB = CType(_sessHelper.GetSession("EDITCB"), CompetitorBrand)
                objCB.Description = txtDeskripsi.Text.Trim
                'objCB.Code = txtKode.Text.Trim
                txtKode.Enabled = True
                objCB.Status = ddlStatus.SelectedValue
                n = New CompetitorBrandFacade(User).Update(objCB)
                Viewstate.Add("vsproses", "Insert")
            End If
            If n >= 0 Then
                ClearData()
                MessageBox.Show("Simpan sukses")
                BindData(dgCompetitorType.CurrentPageIndex)
            Else
                MessageBox.Show("Simpan gagal")
            End If
            dgCompetitorType.SelectedIndex = -1
            _sessHelper.SetSession("BINDSTATUS", "NOTHING")
        End If
    End Sub
    Private Sub ClearData()
        _sessHelper.RemoveSession("EDITCB")
        txtKode.Text = String.Empty
        txtDeskripsi.Text = String.Empty
        ddlStatus.SelectedIndex = -1
        Viewstate.Add("vsproses", "Insert")
    End Sub

    Private Sub BindDataDefault(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        criterias.opAnd(New Criteria(GetType(CompetitorBrand), "Status", MatchType.Exact, 0))
       
        arrList = New CompetitorBrandFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgCompetitorType.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgCompetitorType.DataSource = arrList
        dgCompetitorType.VirtualItemCount = totalRow
        dgCompetitorType.DataBind()
    End Sub

    Private Sub BindData(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlStatus.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(CompetitorBrand), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Integer)))
        End If

        arrList = New CompetitorBrandFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgCompetitorType.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgCompetitorType.DataSource = arrList
        dgCompetitorType.VirtualItemCount = totalRow
        dgCompetitorType.DataBind()
    End Sub

    Private Sub BindResult(ByVal indexPage As Integer)
        Try
            Dim totalRow As Integer = 0
            Dim arrList As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If txtKode.Text.Trim <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(CompetitorBrand), "Code", MatchType.[Partial], txtKode.Text.Trim))
            End If

            If txtDeskripsi.Text.Trim <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(CompetitorBrand), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
            End If
            If ddlStatus.SelectedValue <> -1 Then
                criterias.opAnd(New Criteria(GetType(CompetitorBrand), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Integer)))
            End If
            arrList = New CompetitorBrandFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgCompetitorType.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            _sessHelper.SetSession("SortViewVC", criterias)

            dgCompetitorType.DataSource = arrList
            dgCompetitorType.VirtualItemCount = totalRow
            dgCompetitorType.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgCompetitorType_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgCompetitorType.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                ViewState.Add("vsproses", "Edit")
                setControlUpdate(Integer.Parse(e.Item.Cells(0).Text))
                dgCompetitorType.SelectedIndex = e.Item.ItemIndex
            Case "Activation"

                Dim objCB As CompetitorBrand = New CompetitorBrandFacade(User).Retrieve(Integer.Parse(e.Item.Cells(0).Text))
                If objCB.Status = 0 Then
                    objCB.Status = 1
                Else
                    objCB.Status = 0
                End If
                If New CompetitorBrandFacade(User).Update(objCB) Then
                    MessageBox.Show("Status " + CType(objCB.Status, EnumStatusSPL.StatusSPL).ToString.Replace("_", " "))
                    BindData(dgCompetitorType.CurrentPageIndex)
                End If
        End Select
    End Sub
    Private Sub setControlUpdate(ByVal id As Integer)
        Dim objCB As CompetitorBrand = New CompetitorBrandFacade(User).Retrieve(id)
        txtKode.Text = objCB.Code
        txtKode.Enabled = False
        txtDeskripsi.Text = objCB.Description
        ddlStatus.SelectedValue = objCB.Status
        _sessHelper.SetSession("EDITCB", objCB)
    End Sub

    Private Sub BindDropDown()
        CommonFunction.BindFromEnum("StatusSPL", ddlStatus, User, True, "NameStatus", "ValStatus")
    End Sub

    Private Function CheckDataValid(ByVal code As String) As Boolean
        Dim objCB As CompetitorBrand = CType(_sessHelper.GetSession("EDITCB"), CompetitorBrand)
        Dim bCheck As Boolean = True
        Dim criterias As New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CompetitorBrand), "Code", MatchType.Exact, txtKode.Text.Trim))

        If CType(ViewState("vsproses"), String) = "Edit" Then
            criterias.opAnd(New Criteria(GetType(CompetitorBrand), "ID", MatchType.No, objCB.ID))
        End If
        Dim _arr As ArrayList = New CompetitorBrandFacade(User).RetrieveByCriteria(criterias)
        If _arr.Count > 0 Then
            bCheck = False
            MessageBox.Show("Kode " & txtKode.Text.Trim & " Telah Terdaftar")
            Return bCheck
        End If
        If txtKode.Text.Trim = String.Empty Then
            bCheck = False
            MessageBox.Show("Kode masih kosong")
            Return bCheck
        End If
        If txtDeskripsi.Text.Trim = String.Empty Then
            bCheck = False
            MessageBox.Show("Deskripsi masih kosong")
            Return bCheck
        End If
        If ddlStatus.SelectedValue = -1 Then
            bCheck = False
            MessageBox.Show("Status masih belum dipilih")
            Return bCheck
        End If
        Return bCheck
    End Function

    Private Sub dgCompetitorType_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCompetitorType.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objCB As CompetitorBrand = e.Item.DataItem
            Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            _lblNo.Text = (dgCompetitorType.CurrentPageIndex * dgCompetitorType.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            Dim lbtnActivation As LinkButton = CType(e.Item.FindControl("lbtnActivation"), LinkButton)
            Dim lbtnInactivation As LinkButton = CType(e.Item.FindControl("lbtnInactivation"), LinkButton)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnInactivation.Visible = mEdit
            lbtnActivation.Visible = mEdit
            If objCB.Status = 0 Then
                'lbtnActivation.Text = CType(1, EnumStatusSPL.StatusSPL).ToString.Replace("_", " ")
                lbtnInactivation.Visible = False
            Else
                ' lbtnInactivation.Text = CType(0, EnumStatusSPL.StatusSPL).ToString.Replace("_", " ")
                lbtnActivation.Visible = False
            End If
            lbtnEdit.Visible = mEdit
        End If
    End Sub

    Private Sub dgCompetitorType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgCompetitorType.PageIndexChanged
        dgCompetitorType.CurrentPageIndex = e.NewPageIndex
        If CType(_sessHelper.GetSession("BINDSTATUS"), String) = "CARI" Then
            BindResult(dgCompetitorType.CurrentPageIndex)
        Else
            BindData(dgCompetitorType.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        dgCompetitorType.CurrentPageIndex = 0
        BindResult(dgCompetitorType.CurrentPageIndex)
        _sessHelper.SetSession("BINDSTATUS", "CARI")
    End Sub

    Private Sub dgCompetitorType_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgCompetitorType.SortCommand
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

        dgCompetitorType.SelectedIndex = -1
        dgCompetitorType.CurrentPageIndex = 0
        bindGridSorting(dgCompetitorType.CurrentPageIndex)
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            If CType(_sessHelper.GetSession("BINDSTATUS"), String) = "CARI" Then
                dgCompetitorType.DataSource = New CompetitorBrandFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("sortViewVC"), CriteriaComposite), indexPage + 1, dgCompetitorType.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                dgCompetitorType.DataSource = New CompetitorBrandFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgCompetitorType.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            End If
            dgCompetitorType.VirtualItemCount = totalRow
            dgCompetitorType.DataBind()
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
        txtKode.Enabled = True
        dgCompetitorType.SelectedIndex = -1
    End Sub
End Class
