#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit

#End Region

Public Class EntryMaterialPromotion
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtNoBarang As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNamaBarang As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSatuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHarga As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMaterialPromotion As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtKeterangan As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlDescription As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
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

#Region "Deklarasi"
    Private sHelper As New SessionHelper
    Dim arlNewMaterial As New ArrayList
    Dim criterias As CriteriaComposite
    Private _createPriv As Boolean = False
    Private _historyPriv As Boolean = False
    Private _isFirstLoad As Boolean = False
#End Region

#Region "Custom Method"
    Private Sub BindToGridMaterial(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If indexPage >= 0 Then
            If ViewState("ButtonMode") = "Search" Then
                'CreateSearchCriteria()
                ViewState.Add("ButtonMode", "Normal")
            Else
                'CreateCriteria()
            End If
            Dim arlMtrlPromotion As ArrayList = New MaterialPromotionFacade(User).RetrieveActiveList(indexPage + 1, dtgMaterialPromotion.PageSize, totalRow, ViewState("SortColMP"), ViewState("SortDirectionMP"), CType(sHelper.GetSession("CRITERIAS"), CriteriaComposite))
            dtgMaterialPromotion.DataSource = arlMtrlPromotion
            dtgMaterialPromotion().VirtualItemCount = totalRow

            If indexPage = 0 Then
                dtgMaterialPromotion.CurrentPageIndex = 0
            End If

            dtgMaterialPromotion.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Bugs 1101
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "Status", MatchType.Exact, 0))
    End Sub

    Private Sub CreateSearchCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtNoBarang.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "GoodNo", MatchType.Exact, txtNoBarang.Text.Trim))
        End If
        If txtNamaBarang.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "Name", MatchType.[Partial], txtNamaBarang.Text.Trim))
        End If
        If txtSatuan.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "Unit", MatchType.[Partial], txtSatuan.Text.Trim))
        End If
        If txtHarga.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "Price", MatchType.Exact, CType(txtHarga.Text.Trim, Decimal)))
        End If
        If ddlStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        Else
            If _isFirstLoad Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "Status", MatchType.Exact, CByte(EnumMaterialPromotion.MasterMaterialPromotionStatus.Aktif)))
            End If
        End If
        sHelper.SetSession("CRITERIAS", criterias)
    End Sub

    Private Sub ClearControl()
        txtHarga.Text = ""
        txtHarga.Enabled = True
        txtNamaBarang.Text = ""
        txtNamaBarang.Enabled = True
        txtNoBarang.Text = ""
        txtNoBarang.Enabled = True
        txtSatuan.Text = ""
        txtSatuan.Enabled = True
        txtKeterangan.Text = ""
        btnSimpan.Enabled = _createPriv
        ddlStatus.SelectedIndex = 0
        dtgMaterialPromotion.SelectedIndex = -1
    End Sub

    Private Sub MPToControl(ByVal objMaterialPromotion As KTB.DNet.Domain.MaterialPromotion)
        txtHarga.Text = FormatNumber(objMaterialPromotion.Price, 0, , , TriState.UseDefault)
        txtNamaBarang.Text = objMaterialPromotion.Name
        txtNoBarang.Text = objMaterialPromotion.GoodNo
        txtSatuan.Text = objMaterialPromotion.Unit
    End Sub

    Private Sub ResetControl(ByVal mode As String)
        If mode = "View" Then
            txtHarga.Enabled = False
            txtNamaBarang.Enabled = False
            txtNoBarang.Enabled = False
            txtSatuan.Enabled = False
        Else
            txtHarga.Enabled = True
            txtNamaBarang.Enabled = True
            txtNoBarang.Enabled = True
            txtSatuan.Enabled = True
        End If
    End Sub

    Private Function CekTransactionUsed(ByVal objDomain As KTB.DNet.Domain.MaterialPromotion) As Integer
        Dim nResult As Integer = 1
        If objDomain.MaterialPromotionAllocations.Count > 0 Then
            nResult = -1
        End If

        If objDomain.MaterialPromotionRequestDetails.Count > 0 Then
            nResult = -1
        End If

        If objDomain.MaterialPromotionStockAdjustments.Count > 0 Then
            nResult = -1
        End If

        'If objDomain.MaterialPromotionGIDetails.Count > 0 Then
        '    nResult = -1
        'End If

        If objDomain.MaterialPromotionGIGRs.Count > 0 Then
            nResult = -1
        End If

        If objDomain.MaterialPromotionPriceHistorys.Count > 1 Then
            nResult = -1
        End If

        Return nResult
    End Function

    Sub BindMPMasterStatus()
        ddlStatus.Items.Clear()
        Dim al As ArrayList = New EnumMaterialPromotion().RetrieveMPMasterStatus
        ddlStatus.DataSource = al
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        InitiateAuthorization()
        _createPriv = CheckCreatePriv()

        If Not IsPostBack Then
            ViewState.Add("SortColMP", "GoodNo")
            ViewState.Add("SortDirectionMP", Sort.SortDirection.ASC)
            BindMPMasterStatus()
            _isFirstLoad = True
            CreateSearchCriteria()
            BindToGridMaterial(0)
        Else
            _isFirstLoad = False
        End If
        btnSimpan.Visible = _createPriv

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim pageIndex As Integer = dtgMaterialPromotion.CurrentPageIndex
        If Page.IsValid Then
            If ViewState("SaveModeMP") = "Edit" Then
                Dim id As Integer = ViewState("IDMP")
                Dim objMPSave As KTB.DNet.Domain.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(id)
                Dim objMaterialHistory As MaterialPromotionPriceHistory
                Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                objMPSave.ProductCategory = New ProductCategoryFacade(User).Retrieve(companyCode)
                Dim statusBaru As Short
                If ddlStatus.SelectedIndex <> 0 Then
                    statusBaru = CType(ddlStatus.SelectedValue, Short)
                Else
                    statusBaru = 0
                End If
                objMPSave.Status = statusBaru

                Dim hrgbaru As Decimal = CType(txtHarga.Text.Trim, Decimal)
                If hrgbaru <> objMPSave.Price Then
                    'create new history and insert new value
                    objMPSave.Price = hrgbaru

                    objMaterialHistory = New MaterialPromotionPriceHistory
                    objMaterialHistory.Price = hrgbaru
                    objMaterialHistory.Description = txtKeterangan.Text
                    objMaterialHistory.MaterialPromotion = objMPSave

                    'objMPSave.MaterialPromotionPriceHistorys.Insert(0, objmaterialhistory)
                    'Dim iResult As Integer = New MaterialPromotionFacade(User).InsertTransaction(objMPSave, objmaterialhistory)
                    If (New MaterialPromotionFacade(User).InsertTransaction(objMPSave, objMaterialHistory) <> -1) Then
                        ViewState.Add("SaveModeMP", "Add")
                        'ClearControl()
                        BindToGridMaterial(pageIndex)
                        pnlDescription.Visible = False
                        MessageBox.Show("Data berhasil disimpan")
                    Else
                        MessageBox.Show("Data gagal disimpan")
                    End If

                Else
                    Dim id2 As Integer = ViewState("IDMP")
                    Dim objMP As KTB.DNet.Domain.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(id2)
                    If objMP.MaterialPromotionPriceHistorys.Count > 0 Then
                        Dim objMHistory As MaterialPromotionPriceHistory = objMP.MaterialPromotionPriceHistorys(0)
                    End If
                    'Dim arlMPEdit1 As ArrayList = sHelper.GetSession("Material")

                    objMP.GoodNo = txtNoBarang.Text.Trim.ToUpper
                    objMP.Name = txtNamaBarang.Text.Trim.ToUpper
                    objMP.Unit = txtSatuan.Text.Trim.ToUpper
                    objMP.Price = txtHarga.Text.Trim
                    objMP.Status = statusBaru
                    objMP.ProductCategory = New ProductCategoryFacade(User).Retrieve(companyCode)
                    'Dim iResultUpdate As Integer = New MaterialPromotionFacade(User).Update(objMP)
                    If (New MaterialPromotionFacade(User).Update(objMP) <> -1) Then
                        ViewState.Add("SaveModeMP", "Add")
                        'ClearControl()
                        BindToGridMaterial(pageIndex)
                        pnlDescription.Visible = False
                        MessageBox.Show("Data berhasil disimpan")
                    Else
                        MessageBox.Show("Data gagal disimpan")
                    End If
                End If
            ElseIf ViewState("SaveModeMP") = "View" Then
                pnlDescription.Visible = False
                btnSimpan.Text = "Simpan"
                ClearControl()
                ViewState.Add("SaveModeMP", "Add")
            Else
                If txtNamaBarang.Text = String.Empty Then
                    MessageBox.Show("Nama Barang tidak boleh kosong!")
                Else
                    Dim objMaterialPromotion As New KTB.DNet.Domain.MaterialPromotion
                    Dim objMaterialHistory As New MaterialPromotionPriceHistory
                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                    objMaterialPromotion.GoodNo = txtNoBarang.Text.Trim.ToUpper
                    objMaterialPromotion.Name = txtNamaBarang.Text.Trim.ToUpper
                    objMaterialPromotion.Unit = txtSatuan.Text.Trim.ToUpper
                    objMaterialPromotion.Price = txtHarga.Text.Trim
                    objMaterialPromotion.Status = New EnumMaterialPromotion().MasterMaterialPromotionStatus.Aktif
                    objMaterialPromotion.ProductCategory = New ProductCategoryFacade(User).Retrieve(companyCode)
                    objMaterialHistory.Price = txtHarga.Text.Trim

                    objMaterialPromotion.MaterialPromotionPriceHistorys.Add(objMaterialHistory)

                    'validate goodno
                    Dim objMaterialPromotionFacade As New MaterialPromotionFacade(User)
                    If objMaterialPromotionFacade.ValidateCode(objMaterialPromotion.GoodNo) > 0 Then
                        MessageBox.Show("Nomor barang sudah pernah digunakan!")
                    Else
                        'Dim iResult As Integer = objMaterialPromotionFacade.Insert(objMaterialPromotion)
                        If (objMaterialPromotionFacade.Insert(objMaterialPromotion) <> -1) Then
                            MessageBox.Show("Data berhasil disimpan")
                        Else
                            MessageBox.Show("Data gagal disimpan")
                        End If
                        'ClearControl()
                        BindToGridMaterial(pageIndex)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dtgMaterialPromotion_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMaterialPromotion.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim intNo As Integer = e.Item.ItemIndex + 1 + (dtgMaterialPromotion.CurrentPageIndex * dtgMaterialPromotion.PageSize)
            e.Item.Cells(0).Text = intNo

            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            lbtnDelete.Visible = _createPriv

            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim lbtnvHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
            lbtnvHistory.Attributes.Add("OnClick", "showPopUp('../PopUp/PopUpMPPriceHistory.aspx?id=" & lblID.Text & "&time=" & Date.Now & "','',500,400);return false;")
            _historyPriv = CheckIconHistoryPriv()
            lbtnvHistory.Visible = _historyPriv

            Dim lblHarga As Label = CType(e.Item.FindControl("lblHarga"), Label)
            lblHarga.Text = FormatNumber(lblHarga.Text, 0, , , TriState.UseDefault)

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lbtnActive As LinkButton = CType(e.Item.FindControl("lbtnActive"), LinkButton)
            Dim lbtnNonActive As LinkButton = CType(e.Item.FindControl("lbtnNonActive"), LinkButton)
            Select Case lblStatus.Text
                Case "0"
                    lblStatus.Text = "Aktif"
                    lbtnActive.Visible = False
                    lbtnNonActive.Visible = _createPriv
                    'lbtnNonActive.Visible = True
                Case "1"
                    lblStatus.Text = "Tidak Aktif"
                    lbtnActive.Visible = _createPriv
                    'lbtnActive.Visible = True
                    lbtnNonActive.Visible = False
            End Select

            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnView.Visible = _createPriv
            lbtnEdit.Visible = _createPriv

        End If
    End Sub

    Private Sub dtgMaterialPromotion_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMaterialPromotion.ItemCommand
        If e.CommandName = "Delete" Then
            Dim id As Integer = CInt(CType(e.Item.FindControl("lblID"), Label).Text)
            Dim objMaterialPromotion As KTB.DNet.Domain.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(id)
            If CekTransactionUsed(objMaterialPromotion) <> -1 Then
                Dim iresult As Integer = New MaterialPromotionFacade(User).Delete(objMaterialPromotion)
                If iresult = -1 Then
                    MessageBox.Show(SR.DeleteFail)
                    Exit Sub
                Else
                    MessageBox.Show(SR.DeleteSucces)
                End If
            Else
                MessageBox.Show("No barang: " & objMaterialPromotion.GoodNo & " sudah dipakai dalam transaksi, tidak bisa dihapus")
            End If
            BindToGridMaterial(0)

        ElseIf e.CommandName = "Edit" Then
            ClearControl()
            pnlDescription.Visible = True
            ViewState.Add("SaveModeMP", "Edit")
            Dim id As Integer = CInt(CType(e.Item.FindControl("lblID"), Label).Text)
            Dim objMPEdit As KTB.DNet.Domain.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(id)
            dtgMaterialPromotion.SelectedIndex = e.Item.ItemIndex
            ViewState.Add("IDMP", id)
            MPToControl(objMPEdit)
            ResetControl(e.CommandName)
        ElseIf e.CommandName = "View" Then
            ClearControl()
            ViewState.Add("SaveModeMP", "View")
            Dim id As Integer = CInt(CType(e.Item.FindControl("lblID"), Label).Text)
            Dim objMPEdit As KTB.DNet.Domain.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(id)
            MPToControl(objMPEdit)
            ResetControl(e.CommandName)
            btnSimpan.Enabled = False
            dtgMaterialPromotion.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Activate" Then
            Dim id As Integer = CInt(CType(e.Item.FindControl("lblID"), Label).Text)
            Dim objMPMaster As KTB.DNet.Domain.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(id)
            objMPMaster.Status = New EnumMaterialPromotion().MasterMaterialPromotionStatus.Aktif
            If (New MaterialPromotionFacade(User).Update(objMPMaster) <> -1) Then
                MessageBox.Show("Ubah status ke Aktif berhasil")
                BindToGridMaterial(dtgMaterialPromotion.CurrentPageIndex)
            Else
                MessageBox.Show("Ubah status ke Aktif gagal")
            End If
        ElseIf e.CommandName = "DeActivate" Then
            Dim id As Integer = CInt(CType(e.Item.FindControl("lblID"), Label).Text)
            Dim objMPMaster As KTB.DNet.Domain.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(id)
            objMPMaster.Status = New EnumMaterialPromotion().MasterMaterialPromotionStatus.Tidak_Aktif
            If (New MaterialPromotionFacade(User).Update(objMPMaster) <> -1) Then
                MessageBox.Show("Ubah status ke Tidak Aktif berhasil")
                BindToGridMaterial(dtgMaterialPromotion.CurrentPageIndex)
            Else
                MessageBox.Show("Ubah status ke Tidak Aktif gagal")
            End If
        End If
    End Sub

    Private Sub dtgMaterialPromotion_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMaterialPromotion.PageIndexChanged
        dtgMaterialPromotion.CurrentPageIndex = e.NewPageIndex
        BindToGridMaterial(dtgMaterialPromotion.CurrentPageIndex)
        btnSimpan.Text = "Simpan"
        ClearControl()
        ViewState.Add("SaveModeMP", "Add")

    End Sub

    Private Sub dtgMaterialPromotion_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMaterialPromotion.SortCommand
        If e.SortExpression = ViewState("SortColMP") Then
            If ViewState("SortDirectionMP") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirectionMP", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirectionMP", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColMP", e.SortExpression)
        BindToGridMaterial(dtgMaterialPromotion.CurrentPageIndex)
        btnSimpan.Text = "Simpan"
        'ClearControl()
        ViewState.Add("SaveModeMP", "Add")

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ViewState.Add("SaveModeMP", "Add")
        ClearControl()
        pnlDescription.Visible = False
        BindToGridMaterial(0)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewState.Add("ButtonMode", "Search")
        dtgMaterialPromotion.CurrentPageIndex = 0
        CreateSearchCriteria()
        BindToGridMaterial(dtgMaterialPromotion.CurrentPageIndex)
    End Sub

#End Region


#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.MaterialPromotionMasterView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Material Promosi - Master Material Promosi")
        End If
    End Sub

    Private Function CheckCreatePriv() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.MaterialPromotionMasterCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CheckIconHistoryPriv() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.MaterialPromotionViewHistory_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region
End Class
