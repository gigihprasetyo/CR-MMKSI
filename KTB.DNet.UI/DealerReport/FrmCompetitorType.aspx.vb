Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class FrmCompetitorType
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
    Protected WithEvents ddlKelas As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMerek As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnImport As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variable"
    Private _sessHelper As New SessionHelper
    Dim bEditPriv As Boolean
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack() Then
            ClearData()
            ViewState("CurrentSortColumn") = "Code"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Viewstate.Add("vsproses", "Insert")
            _sessHelper.SetSession("BINDSTATUS", "NOTHING")
            BindDDL()
            dgCompetitorType.CurrentPageIndex = 0
            BindData(dgCompetitorType.CurrentPageIndex)
        End If
        btnImport.Visible = bCekBtnImportPriv
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objCT As New CompetitorType
        Dim n As Integer
        If CheckDataValid(txtKode.Text.Trim) Then
            If CType(ViewState("vsproses"), String) = "Insert" Then
                objCT.Description = txtDeskripsi.Text
                objCT.Code = txtKode.Text.Trim
                objCT.Status = EnumStatusSPL.StatusSPL.Aktif
                objCT.CompetitorBrand = New CompetitorBrandFacade(User).Retrieve(Integer.Parse(ddlMerek.SelectedValue))
                objCT.VehicleClass = New VehicleClassFacade(User).Retrieve(Integer.Parse(ddlKelas.SelectedValue))

                n = New CompetitorTypeFacade(User).Insert(objCT)
            Else
                objCT = CType(_sessHelper.GetSession("EDITCB"), CompetitorType)
                objCT.Description = txtDeskripsi.Text.Trim
                'objCT.Code = txtKode.Text.Trim
                txtKode.Enabled = True
                txtDeskripsi.Enabled = True
                ddlMerek.Enabled = True
                objCT.CompetitorBrand = New CompetitorBrandFacade(User).Retrieve(Integer.Parse(ddlMerek.SelectedValue))
                objCT.VehicleClass = New VehicleClassFacade(User).Retrieve(Integer.Parse(ddlKelas.SelectedValue))

                n = New CompetitorTypeFacade(User).Update(objCT)
                Viewstate.Add("vsproses", "Insert")
            End If
            If n >= 0 Then
                MessageBox.Show("Simpan sukses")
                BindData(dgCompetitorType.CurrentPageIndex)
            Else
                MessageBox.Show("Simpan gagal")
            End If
            ClearData()
            dgCompetitorType.SelectedIndex = -1
            _sessHelper.SetSession("BINDSTATUS", "NOTHING")
        End If
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dgCompetitorType.CurrentPageIndex = 0
        BindResult(0)
        _sessHelper.SetSession("BINDSTATUS", "CARI")
    End Sub
    Private Sub dgCompetitorType_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgCompetitorType.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                ViewState.Add("vsproses", "Edit")
                dgCompetitorType.SelectedIndex = e.Item.ItemIndex
                setControlUpdate(Integer.Parse(e.Item.Cells(0).Text))
                BindData(dgCompetitorType.CurrentPageIndex)

                btnCari.Visible = False
                If bEditPriv Then
                    btnBatal.Visible = True
                Else
                    btnBatal.Visible = bEditPriv
                End If
                btnImport.Visible = bCekBtnImportPriv

            Case "Activation"

                Dim objCT As CompetitorType = New CompetitorTypeFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                If objCT.Status = 0 Then
                    objCT.Status = 1
                Else
                    objCT.Status = 0
                End If
                If New CompetitorTypeFacade(User).Update(objCT) Then
                    MessageBox.Show("Status " + CType(objCT.Status, EnumStatusSPL.StatusSPL).ToString.Replace("_", " "))
                    BindData(dgCompetitorType.CurrentPageIndex)
                End If
        End Select
    End Sub
    Private Sub dgCompetitorType_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCompetitorType.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = (dgCompetitorType.CurrentPageIndex * dgCompetitorType.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            If Not e.Item.DataItem Is Nothing Then
                Dim objCT As CompetitorType = e.Item.DataItem
                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                Dim lbtnActivation As LinkButton = CType(e.Item.FindControl("lbtnActivation"), LinkButton)
                Dim lbtnInactivation As LinkButton = CType(e.Item.FindControl("lbtnInactivation"), LinkButton)

                lbtnEdit.Visible = bEditPriv
                If bEditPriv Then
                    If objCT.Status = 0 Then
                        'lbtnActivation.Text = CType(1, EnumStatusSPL.StatusSPL).ToString.Replace("_", " ")
                        lbtnInactivation.Visible = False
                    Else
                        'lbtnInactivation.Text = CType(0, EnumStatusSPL.StatusSPL).ToString.Replace("_", " ")
                        lbtnActivation.Visible = False
                    End If
                Else
                    lbtnInactivation.Visible = bEditPriv
                    lbtnActivation.Visible = bEditPriv
                End If


            End If
        End If

    End Sub
    Private Sub dgCompetitorType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgCompetitorType.PageIndexChanged
        dgCompetitorType.CurrentPageIndex = e.NewPageIndex
        If CType(_sessHelper.GetSession("BINDSTATUS"), String) = "CARI" Then
            BindResult(dgCompetitorType.CurrentPageIndex)
        Else
            BindData(dgCompetitorType.CurrentPageIndex)
        End If
        ClearData()
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
    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        ImportVechileType()
    End Sub
    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        txtKode.Enabled = True
        txtDeskripsi.Enabled = True
        ddlMerek.Enabled = True
        dgCompetitorType.SelectedIndex = -1
    End Sub

#End Region

#Region "Custom Method"

#Region "Check Privilage"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.TypeMaintenanceView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=DEALER REPORT - Tipe Kompetitor")
        End If

        bEditPriv = SecurityProvider.Authorize(context.User, SR.TypeMaintenanceEdit_Privilege)
        btnSave.Visible = bEditPriv
    End Sub

    Dim bCekBtnImportPriv As Boolean = SecurityProvider.Authorize(context.User, SR.TypeMaintenanceImport_Privilege)

#End Region


    Private Sub ClearData()
        ViewState.Add("vsproses", "Insert")
        _sessHelper.RemoveSession("EDITCB")
        txtKode.Text = String.Empty
        txtDeskripsi.Text = String.Empty
        ddlKelas.SelectedValue = 0
        ddlMerek.SelectedValue = 0

        btnCari.Visible = True
        If bEditPriv Then
            btnBatal.Visible = False
        Else
            btnBatal.Visible = bEditPriv
        End If

        btnImport.Visible = bCekBtnImportPriv
    End Sub
    Private Sub BindDDL()
        Dim _sortVC As SortCollection = New SortCollection
        Dim _sortCB As SortCollection = New SortCollection
        _sortVC.Add(New Sort(GetType(VehicleClass), "Description", Sort.SortDirection.ASC))
        _sortCB.Add(New Sort(GetType(CompetitorBrand), "Description", Sort.SortDirection.ASC))

        '--Vehicle Class (modif by ronny, sort and data with status=1)
        Dim criteriaVC As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaVC.opAnd(New Criteria(GetType(VehicleClass), "Status", MatchType.Exact, CType(EnumStatusSPL.StatusSPL.Aktif, Short)))

        Dim _kelas As ArrayList = New VehicleClassFacade(User).Retrieve(criteriaVC, _sortVC)

        Dim _emptyKelas As New ListItem("Pilih Kelas", 0)
        _emptyKelas.Selected = True
        ddlKelas.Items.Add(_emptyKelas)

        For Each _itemKelas As VehicleClass In _kelas
            Dim _listItemKelas As New ListItem(_itemKelas.Description, _itemKelas.ID)
            ddlKelas.Items.Add(_listItemKelas)
        Next

        '--Vehicle Class (modif by ronny, sort and data with status=1)
        Dim criteriaBC As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaBC.opAnd(New Criteria(GetType(CompetitorBrand), "Status", MatchType.Exact, CType(EnumStatusSPL.StatusSPL.Aktif, Short)))

        Dim _emptyMerek As New ListItem("Pilih Merek", 0)
        _emptyMerek.Selected = True
        ddlMerek.Items.Add(_emptyMerek)

        Dim _Merek As ArrayList = New CompetitorBrandFacade(User).Retrieve(criteriaBC, _sortCB)
        For Each _itemMerek As CompetitorBrand In _Merek
            Dim _listItemMerek As New ListItem(_itemMerek.Description, _itemMerek.ID)
            ddlMerek.Items.Add(_listItemMerek)

        Next

    End Sub
    Private Sub BindData(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(CompetitorType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        arrList = New CompetitorTypeFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgCompetitorType.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgCompetitorType.DataSource = arrList
        dgCompetitorType.VirtualItemCount = totalRow
        dgCompetitorType.DataBind()
    End Sub
    Private Sub BindResult(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(CompetitorType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKode.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(CompetitorType), "Code", MatchType.[Partial], txtKode.Text.Trim))
        End If
        If txtDeskripsi.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(CompetitorType), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
        End If
        If ddlKelas.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(CompetitorType), "VehicleClass.ID", MatchType.Exact, Integer.Parse(ddlKelas.SelectedValue)))
        End If
        If ddlMerek.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(CompetitorType), "CompetitorBrand.ID", MatchType.Exact, Integer.Parse(ddlMerek.SelectedValue)))
        End If
        Dim arrList As ArrayList = New CompetitorTypeFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgCompetitorType.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgCompetitorType.DataSource = arrList
        dgCompetitorType.VirtualItemCount = totalRow
        dgCompetitorType.DataBind()
        _sessHelper.SetSession("SortViewVC", criterias)
    End Sub
    Private Sub setControlUpdate(ByVal id As Integer)
        Dim objCT As CompetitorType = New CompetitorTypeFacade(User).Retrieve(id)
        txtKode.Text = objCT.Code
        txtKode.Enabled = False
        txtDeskripsi.Text = objCT.Description
        txtDeskripsi.Enabled = True
        ddlMerek.Enabled = False
        Try
            ddlKelas.SelectedValue = objCT.VehicleClass.ID
        Catch ex As Exception
            ddlKelas.SelectedIndex = -1
        End Try
        Try
            ddlMerek.SelectedValue = objCT.CompetitorBrand.ID
        Catch ex As Exception
            ddlMerek.SelectedIndex = -1
        End Try
        _sessHelper.SetSession("EDITCB", objCT)
    End Sub
    Private Function CheckDataValid(ByVal code As String) As Boolean
        Dim objCT As CompetitorType = CType(_sessHelper.GetSession("EDITCB"), CompetitorType)
        Dim bCheck As Boolean = True
        Dim criterias As New CriteriaComposite(New Criteria(GetType(CompetitorType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CompetitorType), "Code", MatchType.Exact, txtKode.Text.Trim))

        If CType(ViewState("vsproses"), String) = "Edit" Then
            criterias.opAnd(New Criteria(GetType(CompetitorType), "ID", MatchType.No, objCT.ID))
        End If
        Dim _arr As ArrayList = New CompetitorTypeFacade(User).RetrieveByCriteria(criterias)
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
        If ddlKelas.SelectedValue = 0 Then
            bCheck = False
            MessageBox.Show("Kelas belum dipilih")
        End If
        If ddlMerek.SelectedValue = 0 Then
            bCheck = False
            MessageBox.Show("Merek belum dipilih")
        End If
        Return bCheck
    End Function
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            If CType(_sessHelper.GetSession("BINDSTATUS"), String) = "CARI" Then
                dgCompetitorType.DataSource = New CompetitorTypeFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("sortViewVC"), CriteriaComposite), indexPage + 1, dgCompetitorType.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(CompetitorType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                dgCompetitorType.DataSource = New CompetitorTypeFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgCompetitorType.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            End If
            dgCompetitorType.VirtualItemCount = totalRow
            dgCompetitorType.DataBind()
        End If

    End Sub
    Private Sub ImportVechileType()
        Dim vtFacade As VechileTypeFacade = New VechileTypeFacade(User)
        Dim list As ArrayList = vtFacade.RetrieveActiveList
        Dim CompetitorTypeList As ArrayList = New ArrayList
        Dim objCompetitorType As CompetitorType
        Dim objBrand As CompetitorBrand = GetMerk()

        If objBrand.ID > 0 Then
            If list.Count > 0 Then
                For Each item As VechileType In list
                    objCompetitorType = New CompetitorType
                    objCompetitorType.Code = item.VechileTypeCode
                    objCompetitorType.CompetitorBrand = objBrand
                    objCompetitorType.VehicleClass = item.VehicleClass
                    objCompetitorType.Description = item.Description
                    objCompetitorType.Status = EnumStatusSPL.StatusSPL.Aktif
                    CompetitorTypeList.Add(objCompetitorType)
                Next
                Dim i As Integer = New CompetitorTypeFacade(User).Import(CompetitorTypeList)

                If i >= 0 Then
                    MessageBox.Show("Import data berhasil")
                    dgCompetitorType.CurrentPageIndex = 0
                    BindResult(0)
                    _sessHelper.SetSession("BINDSTATUS", "CARI")
                Else
                    MessageBox.Show("Import data gagal")
                End If
            End If
        Else
            MessageBox.Show("Merk Mitsubishi belum di daftarkan")
        End If

    End Sub
    Private Function GetMerk() As CompetitorBrand
        Dim objBrand As CompetitorBrand = New CompetitorBrandFacade(User).Retrieve("Mitsubishi")
        Return objBrand
    End Function
#End Region


End Class
