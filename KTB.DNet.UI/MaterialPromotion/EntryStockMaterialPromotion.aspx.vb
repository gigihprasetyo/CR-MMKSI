#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class EntryStockMaterialPromotion
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents ddlAdjType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPenjelasan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeBarang As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJumlah As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKeterangan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpMPMaster As System.Web.UI.WebControls.Label
    Protected WithEvents dtgMPStock As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNmBarang As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnBaru As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

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
    Private arlMasterData As ArrayList
    Private sHelper As New SessionHelper
    Private _createPriv As Boolean = False
#End Region

#Region "internal enum"
    Private Enum AdjustmentType
        AdjIn = 1
        AdjOut = 2
    End Enum
#End Region

#Region "Custom Method"
    Private Sub MapDDL()
        Dim adjType() As String = {"Silahkan Pilih", "Adjustment In", "Adjustment Out"}
        For i As Integer = 0 To adjType.Length - 1
            ddlAdjType.Items.Add(adjType(i))
        Next
    End Sub

    Private Function AdjTypeNo() As Short
        Select Case ddlAdjType.SelectedValue
            Case "Adjustment In"
                Return 1
            Case "Adjustment Out"
                Return 2
        End Select
    End Function
    Private Sub CreateCriteria()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtKodeBarang.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "GoodNo", MatchType.InSet, "('" & txtKodeBarang.Text.Replace(";", "','") & "')"))
        End If
        If txtNmBarang.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "Name", MatchType.[Partial], txtNmBarang.Text))
        End If
        sHelper.SetSession("CRITERIAS", criterias)
    End Sub

    Private Sub BindDatagridSearch(ByVal indexpage As Integer)
        Dim totalRow As Integer = 0
        If indexpage >= 0 Then
            Dim arlMasterData As ArrayList = New MaterialPromotionFacade(User).RetrieveActiveList(indexpage + 1, dtgMPStock.PageSize, totalRow, viewstate("SortColumn"), viewstate("SortDirection"), CType(sHelper.GetSession("CRITERIAS"), CriteriaComposite))
            dtgMPStock.DataSource = arlMasterData
            dtgMPStock.VirtualItemCount = totalRow
            If indexpage = 0 Then
                dtgMPStock.CurrentPageIndex = 0
            End If
            dtgMPStock.DataBind()
        End If
    End Sub

    Private Sub RetrieveAllData(ByVal indexpage As Integer)
        Dim totalRow As Integer = 0
        If indexpage >= 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim arlMasterData As ArrayList = New MaterialPromotionFacade(User).RetrieveActiveList(indexpage + 1, dtgMPStock.PageSize, totalRow, viewstate("SortColumn"), viewstate("SortDirection"), criterias)
            dtgMPStock.DataSource = arlMasterData
            dtgMPStock.VirtualItemCount = totalRow
            If indexpage = 0 Then
                dtgMPStock.CurrentPageIndex = 0
            End If
            dtgMPStock.DataBind()
        End If
    End Sub

    Private Sub GetGeneratedNumber(ByVal id As Integer)
        Dim objMPStock As MaterialPromotionStockAdjustment = New MaterialPromotionStockAdjustmentFacade(User).Retrieve(id)
        If Not objMPStock Is Nothing Then
            txtKeterangan.Text = objMPStock.Keterangan
        End If
    End Sub

    Private Sub ClearControl()
        ddlAdjType.SelectedIndex = 0
        txtKodeBarang.Text = String.Empty
        txtNmBarang.Text = String.Empty
        txtJumlah.Text = String.Empty
        txtKeterangan.Text = String.Empty
        txtPenjelasan.Text = String.Empty

        'Add by Andra AR - 02/12/2008 
        If viewstate("SaveMode") = "View" Then
            ddlAdjType.Enabled = False
            txtKodeBarang.Enabled = False
            txtNmBarang.Enabled = False
            txtJumlah.Enabled = False
            txtPenjelasan.Enabled = False
        Else
            ddlAdjType.Enabled = True
            txtKodeBarang.Enabled = False
            txtNmBarang.Enabled = True
            txtJumlah.Enabled = True
            txtPenjelasan.Enabled = True
        End If
        'End of Add by Andra AR - 02/12/2008 
    End Sub

    Private Sub MapDbtoControl()
        Dim id As Integer = CInt(viewstate("ID"))
        Dim mpMaster As KTB.DNet.Domain.MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(id)
        txtKodeBarang.Text = mpMaster.GoodNo
        txtNmBarang.Text = mpMaster.Name

        If ViewState("SaveMode") = "View" Then
            txtJumlah.Text = mpMaster.Stock
        End If
    End Sub

    Private Function CekGeneratedNo() As Boolean
        If txtKeterangan.Text = "" Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        _createPriv = CheckCreatePriv()
        
        If Not IsPostBack Then
            viewstate.Add("SortColumn", "GoodNo")
            viewstate.Add("SortDirection", Sort.SortDirection.ASC)
            CreateCriteria()
            BindDatagridSearch(0)

            MapDDL()
            lblPopUpMPMaster.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpMPMaster.aspx','',600,600,MaterialPromotion);")
            'lblPopUpMPMaster.Attributes("onclick") = "ShowPPMPMaster();"
        End If
        btnSimpan.Visible = _createPriv
        txtNmBarang.Attributes.Add("readonly", "readonly")
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If viewstate("SaveMode") = "View" Then
            btnSimpan.Text = "Simpan"
            ClearControl()
            viewstate("SaveMode") = "NotView"

            'Add by Andra AR - 02/12/2008 
            ddlAdjType.Enabled = True
            txtKodeBarang.Enabled = False
            txtNmBarang.Enabled = True
            txtJumlah.Enabled = True
            txtPenjelasan.Enabled = True
            'End of Add by Andra AR - 02/12/2008 
        Else
            Dim objUserInfo As UserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)

            If CekGeneratedNo() = False Then
                Dim adjType As Short = AdjTypeNo()
                Dim goodno As String = txtKodeBarang.Text.Trim
                Dim nmBarang As String = txtNmBarang.Text
                Dim qty As Integer
                If (txtJumlah.Text <> "") Then
                    qty = CInt(txtJumlah.Text)
                Else
                    qty = 0
                End If
                'Dim ktrgn As String = txtKeterangan.Text.Trim
                Dim desc As String = txtPenjelasan.Text.Trim

                Dim objMPMaster As KTB.DNet.Domain.MaterialPromotion = New KTB.DNet.Domain.MaterialPromotion
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "GoodNo", MatchType.Exact, goodno))
                Dim arlMPMaster As ArrayList = New MaterialPromotionFacade(User).Retrieve(criterias)
                If Not arlMPMaster Is Nothing Then
                    If arlMPMaster.Count > 0 Then
                        objMPMaster = arlMPMaster(0)
                    End If
                End If

                objMPMaster.GoodNo = goodno
                objMPMaster.Name = nmBarang

                'cek apakah diobject yang diambil sudah ada stoknya
                If objMPMaster.Stock = 0 Then
                    If adjType = AdjustmentType.AdjIn Then
                        Dim stok As Integer = objMPMaster.Stock
                        objMPMaster.Stock = objMPMaster.Stock + qty

                        Dim objStockMP As New KTB.DNet.Domain.MaterialPromotionStockAdjustment
                        objStockMP.Dealer = objUserInfo.Dealer
                        objStockMP.StockAwal = stok
                        objStockMP.AdjustType = adjType
                        objStockMP.Description = desc
                        objStockMP.Qty = qty

                        objMPMaster.MaterialPromotionStockAdjustments.Insert(0, objStockMP)

                        Dim iResult As Integer = New MaterialPromotionFacade(User).InsertTransactionStock(objMPMaster)

                        If iresult <> -1 Then
                            'set keterangan dengan generated number
                            GetGeneratedNumber(iResult)
                            MessageBox.Show("Data berhasil disimpan")
                        End If

                        BindDatagridSearch(dtgMPStock.CurrentPageIndex)
                    ElseIf adjType = AdjustmentType.AdjOut Then
                        MessageBox.Show("Tidak bisa melakukan insert data Adjustment Out, karena Stok anda: " & objMPMaster.Stock)
                    Else
                        MessageBox.Show("Silahkan pilih Adjustment yang diinginkan")
                    End If
                Else
                    If adjType = AdjustmentType.AdjIn Then
                        Dim stok As Integer = objMPMaster.Stock
                        objMPMaster.Stock = objMPMaster.Stock + qty

                        Dim objStockMP As New KTB.DNet.Domain.MaterialPromotionStockAdjustment
                        objStockMP.Dealer = objUserInfo.Dealer
                        objStockMP.StockAwal = stok
                        objStockMP.AdjustType = adjType
                        objStockMP.Description = desc
                        objStockMP.Qty = qty

                        objMPMaster.MaterialPromotionStockAdjustments.Insert(0, objStockMP)

                        Dim iResult As Integer = New MaterialPromotionFacade(User).InsertTransactionStock(objMPMaster)

                        If iresult <> -1 Then
                            'set keterangan dengan generated number
                            GetGeneratedNumber(iResult)
                            MessageBox.Show("Data berhasil disimpan")
                        End If

                        BindDatagridSearch(dtgMPStock.CurrentPageIndex)
                    ElseIf adjType = AdjustmentType.AdjOut Then
                        Dim stok As Integer = objMPMaster.Stock
                        objMPMaster.Stock = objMPMaster.Stock - qty

                        If objMPMaster.Stock < 0 Then
                            MessageBox.Show("Adjustment Out tidak bisa dilakukan. Stok kurang dari 1")
                        Else
                            Dim objStockMP As New KTB.DNet.Domain.MaterialPromotionStockAdjustment
                            objStockMP.Dealer = objUserInfo.Dealer
                            objStockMP.StockAwal = stok
                            objStockMP.AdjustType = adjType
                            objStockMP.Description = desc
                            objStockMP.Qty = qty

                            objMPMaster.MaterialPromotionStockAdjustments.Insert(0, objStockMP)

                            Dim iResult As Integer = New MaterialPromotionFacade(User).InsertTransactionStock(objMPMaster)

                            If iresult <> -1 Then
                                'set keterangan dengan generated number
                                GetGeneratedNumber(iResult)
                                MessageBox.Show("Data berhasil disimpan")
                            End If

                            BindDatagridSearch(dtgMPStock.CurrentPageIndex)
                        End If
                    Else
                        MessageBox.Show("Silahkan pilih Adjustment yang diinginkan")
                    End If
                End If
            Else
                MessageBox.Show("Tidak bisa menyimpan data dengan Keterangan yang sama")
            End If
        End If
    End Sub

    Private Sub dtgMPStock_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMPStock.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
            lbtnHistory.Attributes.Add("OnClick", "showPopUp('../PopUp/PopUpStockMP.aspx?id=" & lblID.Text & "&time=" & Date.Now & "','',650,600);return false;")
            Dim _AdjustPriv As Boolean = False

            _AdjustPriv = CheckIconAdjustmentPriv()
            lbtnHistory.Visible = _AdjustPriv

            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            lbtnView.Visible = _createPriv

            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgMPStock.CurrentPageIndex * dtgMPStock.PageSize)
        End If
    End Sub

    Private Sub dtgMPStock_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMPStock.ItemCommand
        If e.CommandName = "View" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            viewstate.Add("ID", lblID.Text)
            viewstate.Add("SaveMode", "View")
            btnSimpan.Text = "Kembali"
            ClearControl()
            MapDbtoControl()
        ElseIf e.CommandName = "Edit" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            viewstate.Add("ID", lblID.Text)
            viewstate.Add("SaveMode", "Edit")
            btnSimpan.Text = "Simpan"
            ClearControl()
            MapDbtoControl()
        End If
    End Sub

    Private Sub dtgMPStock_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMPStock.SortCommand
        If e.SortExpression = viewstate("SortColumn") Then
            If viewstate("SortDirection") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirection", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColumn", e.SortExpression)
        BindDatagridSearch(dtgMPStock.CurrentPageIndex)
    End Sub

    Private Sub dtgMPStock_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMPStock.PageIndexChanged
        dtgMPStock.CurrentPageIndex = e.NewPageIndex
        BindDatagridSearch(dtgMPStock.CurrentPageIndex)
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        ddlAdjType.SelectedIndex = 0
        txtKodeBarang.Text = String.Empty
        txtNmBarang.Text = String.Empty
        txtJumlah.Text = String.Empty
        txtKeterangan.Text = String.Empty
        txtPenjelasan.Text = String.Empty
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        CreateCriteria()
        BindDatagridSearch(0)
    End Sub

#End Region


#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionViewStock_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Material Promosi - Stock Material Promosi")
        End If
    End Sub

    Private Function CheckCreatePriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionCreateStock_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CheckIconAdjustmentPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionViewAdjusmentStock_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

End Class
