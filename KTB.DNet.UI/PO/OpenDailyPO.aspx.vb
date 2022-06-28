#Region "Custom Namespace Imports"
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class OpenDailyPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisPesanan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblspaNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblspaDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPK As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPKValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents lblKotaValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunPerakitanAtauImport As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTahunPerakitanAtauImport As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTanggalPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalPesananValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblRencanaPenebusan As System.Web.UI.WebControls.Label
    Protected WithEvents ddlRencanaPenebusan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents txtNomorPesanan As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents dtgPesananKendaraan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblLagend As System.Web.UI.WebControls.Label
    Protected WithEvents lblLagend2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents lblTotalUnit As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalUnitValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaUnit As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaUnitPD As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBaru As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents btnKembali As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents HideField As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCodeValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblCityValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorKontrak As System.Web.UI.WebControls.Label
    Protected WithEvents txtNomorKOntrak As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlPeriodeKontrak As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNomerPk As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerPKNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKondisiPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKondisiPesanan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTotalSisaTebus As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgContract As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPerhatian As System.Web.UI.WebControls.Label
    Protected WithEvents lblDokumen As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggal As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents lblQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlFreePPh As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private ArrListContractHeader As ArrayList
    Private objContractHeader As ContractHeader
    Private totalSisaTebus As Int64 = 0
    Private totalSisaUnit As Integer = 0
    Private totQty As Integer = 0
    Private sessionHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(ddlJenisPesanan.SelectedIndex)
        objSSPO.Add(ddlPeriodeKontrak.SelectedIndex)
        objSSPO.Add(ddlKondisiPesanan.SelectedIndex)
        objSSPO.Add(txtNomorKOntrak.Text)
        objSSPO.Add(txtDealerPKNumber.Text)
        objSSPO.Add(ddlKategori.SelectedIndex)
        objSSPO.Add(dtgContract.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessionHelper.SetSession("SESSIONDAILYPO", objSSPO)
    End Sub

    Private Sub GetSessionCriteria()
        Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONDAILYPO")
        If Not objSSPO Is Nothing Then
            ddlJenisPesanan.SelectedIndex = objSSPO.Item(0)
            ddlPeriodeKontrak.SelectedIndex = objSSPO.Item(1)
            ddlKondisiPesanan.SelectedIndex = objSSPO.Item(2)
            txtNomorKOntrak.Text = objSSPO.Item(3)
            txtDealerPKNumber.Text = objSSPO.Item(4)
            ddlKategori.SelectedIndex = objSSPO.Item(5)
            dtgContract.CurrentPageIndex = objSSPO.Item(6)
            ViewState("CurrentSortColumn") = objSSPO.Item(7)
            ViewState("CurrentSortDirect") = objSSPO.Item(8)
        End If
    End Sub

    Private Sub DisplayFootNote(ByVal _visible As Boolean)
        lblPerhatian.Visible = _visible
        lblDokumen.Visible = _visible
        lblTanggal.Visible = _visible
        lblspaDate.Visible = _visible
        lblspaNumber.Visible = _visible
    End Sub

    Private Sub RetrieveDealer()
        Dim objSessionHelper As New SessionHelper
        Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            lblDealerCodeValue.Text = objDealer.DealerCode & " / " & objDealer.SearchTerm1
            lblNamaDealerValue.Text = objDealer.DealerName
            lblCityValue.Text = objDealer.City.CityName
            lblspaNumber.Text = objDealer.SPANumber

            If objDealer.SPADate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                lblspaDate.Text = Nothing
            Else
                lblspaDate.Text = Format(objDealer.SPADate, "dd MMMMMMMMMMMMMMMM yyyy")
            End If
        Else
            'Response.Redirect("../SessionExpired.htm")
        End If
    End Sub

    Private Sub RetrieveMaster()

        'Bind To DropDownlist Category 
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrayListCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)

        If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
            Dim listitemBlanks As New ListItem("Silahkan Pilih", -1)
            ddlKategori.Items.Add(listitemBlanks)
        End If

        For Each item As Category In arrayListCategory
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) Then
                    Dim listItems1 As New ListItem(item.CategoryCode, item.ID)
                    ddlKategori.Items.Add(listItems1)
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) Then
                    Dim listItems2 As New ListItem(item.CategoryCode, item.ID)
                    ddlKategori.Items.Add(listItems2)
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) Then
                    Dim listItems3 As New ListItem(item.CategoryCode, item.ID)
                    ddlKategori.Items.Add(listItems3)
                End If
            End If
        Next
        ddlKategori.ClearSelection()
        '--DropDownList Jenis Pesanan
        If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) Then
            Dim itemBlanks As ListItem = New ListItem("Silahkan Pilih", -1)
            ddlJenisPesanan.Items.Add(itemBlanks)
        End If
        For Each itemx As ListItem In LookUp.ArrayJenisPesanan
            If itemx.Text = "Bulanan" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindBulanan_Privilege) Then
                    ddlJenisPesanan.Items.Add(itemx)
                End If
            ElseIf itemx.Text = "Tambahan" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindTambahan_Privilege) Then
                    ddlJenisPesanan.Items.Add(itemx)
                End If
            End If
        Next
        ddlJenisPesanan.ClearSelection()
        '--DropDownList Kondisi Pesanan
        If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) Then
            Dim listitemBlankx = New ListItem("Silahkan Pilih", -1)
            ddlKondisiPesanan.Items.Add(listitemBlankx)
        End If
        For Each itemxx As ListItem In LookUp.ArrayPurpose
            If itemxx.Text = "Khusus" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiKhusus_Privilege) Then
                    ddlKondisiPesanan.Items.Add(itemxx)
                End If
            ElseIf itemxx.Text = "Biasa" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiBiasa_Privilege) Then
                    ddlKondisiPesanan.Items.Add(itemxx)
                End If
            End If
        Next
        ddlKondisiPesanan.ClearSelection()

        ddlPeriodeKontrak.DataSource = LookUp.ArraylistMonth(True, 0, 1, DateTime.Now)
        ddlPeriodeKontrak.DataBind()
        ddlPeriodeKontrak.ClearSelection()
        If ddlPeriodeKontrak.Items.Count - 1 > 0 Then
            ddlPeriodeKontrak.SelectedIndex = ddlPeriodeKontrak.Items.Count - 1
        Else
            ddlPeriodeKontrak.SelectedIndex = 0
        End If


    End Sub

    Private Sub BindDataToGrid(ByVal pageIndex As Integer, Optional ByVal isButtonClick As Boolean = False)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim dealer As String() = lblDealerCodeValue.Text.Split("/")
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "Dealer.DealerCode", MatchType.Exact, dealer(0).Trim))

        If ddlJenisPesanan.SelectedIndex <> 0 And ddlJenisPesanan.SelectedIndex <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractType", MatchType.Exact, ddlJenisPesanan.SelectedValue))
        End If

        If txtNomorKOntrak.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractNumber", MatchType.Exact, txtNomorKOntrak.Text))
        End If

        If txtDealerPKNumber.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "DealerPKNumber", MatchType.Exact, txtDealerPKNumber.Text))
        End If


        If ddlKategori.SelectedIndex <> 0 And ddlKategori.SelectedIndex <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        End If

        If ddlKondisiPesanan.SelectedIndex <> 0 And ddlKondisiPesanan.SelectedIndex <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "Purpose", MatchType.Exact, ddlKondisiPesanan.SelectedValue))
        End If

        If ddlPeriodeKontrak.SelectedIndex <> -1 Then
            Dim tanggal As DateTime = CType(ddlPeriodeKontrak.SelectedItem.ToString, DateTime)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractPeriodMonth", MatchType.Exact, CInt(tanggal.Month)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractPeriodYear", MatchType.Exact, CInt(tanggal.Year)))
        End If
        'Start  : RemainModule : DailyPO -> FreePPh Doni N
        If ddlFreePPh.SelectedValue = 0 Then
            'Dim subQuery As String
            'subQuery = "select count(poh.id) from POHeader poh where poh.RowStatus=0 and poh.FreePPh22Indicator=0 and poh.ContractHeaderID=ContractHeader.ID"
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "RowStatus", MatchType.No, "(" & subQuery & ")"), "(", True)
            'subQuery = "select count(poh.id) from POHeader poh where poh.RowStatus=0 and poh.ContractHeaderID=ContractHeader.ID"
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "RowStatus", MatchType.Exact, "(" & subQuery & ")"), ")", False)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "FreePPh22Indicator", MatchType.Exact, ddlFreePPh.SelectedValue))
        ElseIf ddlFreePPh.SelectedValue = 1 Then
            'Dim subQuery As String
            'subQuery = "select count(poh.id) from POHeader poh where poh.RowStatus=0 and poh.FreePPh22Indicator=1 and poh.ContractHeaderID=ContractHeader.ID"
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "RowStatus", MatchType.No, "(" & subQuery & ")"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "FreePPh22Indicator", MatchType.Exact, ddlFreePPh.SelectedValue))
        End If
        'End    : RemainModule : DailyPO -> FreePPh Doni N
        'Start  : 20131203:donas:angga:TOP Interest
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "IsCarriedOver", MatchType.No, 1))
        'End    : : 20131203:donas:angga:TOP Interest
        If pageIndex >= 0 Then
            ArrListContractHeader = New ContractHeaderFacade(User).RetrieveActiveList(criterias, pageIndex + 1, dtgContract.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgContract.DataSource = ArrListContractHeader
            dtgContract.VirtualItemCount = totalRow
            If ArrListContractHeader.Count > 0 Then
                DisplayFootNote(True)
                dtgContract.DataBind()
            Else
                dtgContract.DataBind()
                DisplayFootNote(False)
                If isButtonClick Then
                    MessageBox.Show("Data Tidak Ditemukan")
                End If

            End If
        End If


        ''



        ''
    End Sub

    Private Sub BindDdlFreePPh()
        With ddlFreePPh
            .Items.Clear()
            .Items.Add(New ListItem("Silahkan Pilih", -1))
            .Items.Add(New ListItem("Tidak", 0))
            .Items.Add(New ListItem("Bebas", 1))
        End With
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckUserPrivilege()
        If Not IsPostBack Then
            RetrieveDealer()
            RetrieveMaster()
            BindDdlFreePPh()
            ViewState("CurrentSortColumn") = "ContractNumber"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            ArrListContractHeader = New ArrayList
            dtgContract.DataSource = ArrListContractHeader
            GetSessionCriteria()
            BindDataToGrid(dtgContract.CurrentPageIndex)
            lblTotalSisaTebus.Text = FormatNumber(totalSisaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            If totQty < 0 Then
                lblQuantity.Text = FormatNumber(0, 0, , , TriState.UseDefault) & " Unit"
            Else
                lblQuantity.Text = FormatNumber(totQty, 0, , , TriState.UseDefault) & " Unit"
            End If
        End If
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.PengajuanPOView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pengajuan PO")
        End If
        dtgContract.Columns(10).Visible = SecurityProvider.Authorize(Context.User, SR.DaftarMOViewDetail_Privilege)
        dtgContract.Columns(11).Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPOIconCreatePrivilege)

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        Label4.Visible = isPriceVisible
        Label20.Visible = isPriceVisible
        Label19.Visible = isPriceVisible
        lblTotalSisaTebus.Visible = isPriceVisible
        dtgContract.Columns(10).Visible = isPriceVisible
    End Sub

    Sub dtgContract_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If (e.Item.ItemIndex <> -1) Then
            objContractHeader = ArrListContractHeader(e.Item.ItemIndex)
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgContract.PageSize * dtgContract.CurrentPageIndex)).ToString
            e.Item.Cells(5).Text = objContractHeader.DealerPKNumber
            If Not objContractHeader.Category Is Nothing Then
                e.Item.Cells(6).Text = objContractHeader.Category.CategoryCode
            End If
            e.Item.Cells(7).Text = CType(objContractHeader.ContractType, enumOrderType.OrderType).ToString
            e.Item.Cells(8).Text = objContractHeader.ProductionYear
            e.Item.Cells(9).Text = objContractHeader.ProjectName
            e.Item.Cells(10).Text = FormatNumber(objContractHeader.TotalSisaJumlahTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            totalSisaTebus += objContractHeader.TotalSisaJumlahTebus
            totalSisaUnit += objContractHeader.TotalSisaUnit
            totQty += objContractHeader.TotalQuantity
            Dim lBtn As LinkButton = e.Item.FindControl("lbtnCreatePO")
            ' Heru yang ganti
            'If Cint(objContractHeader.TotalSisaJumlahTebus) =0  Then
            If objContractHeader.TotalSisaJumlahTebus < 1 Then
                lBtn.Visible = False
            ElseIf objContractHeader.Status = "LOCK" Then
                lBtn.Text = "<img src=""../images/unlock.gif"" border=""0"" alt=""Dikunci"">"
                lBtn.Enabled = False
            End If

            If lBtn.Visible Then
                Dim lbtnCreatePO As LinkButton = e.Item.FindControl("lbtnCreatePO")
                If Not IsNothing(lbtnCreatePO) Then
                    If objContractHeader.IsCarriedOver = 1 Then
                        lbtnCreatePO.Visible = False
                    Else
                        lbtnCreatePO.Visible = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub BtnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click

        dtgContract.CurrentPageIndex = 0
        BindDataToGrid(dtgContract.CurrentPageIndex, True)
        lblTotalSisaTebus.Text = FormatNumber(totalSisaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblQuantity.Text = FormatNumber(totQty, 0, , , TriState.UseDefault) & " Unit"
      End Sub

    Sub dtgContract_ItemCommand(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        Select Case (e.CommandName)
            Case "View"
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../PK/ListContractDetail.aspx?id=" & e.Item.Cells(0).Text)
            Case "Create"
                sessionHelper.SetSession("PrevPagePO", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../PO/frmCreatePO.aspx?id=" & e.Item.Cells(0).Text)
        End Select
    End Sub

    Private Sub dtgContract_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgContract.PageIndexChanged
        dtgContract.SelectedIndex = -1
        dtgContract.CurrentPageIndex = e.NewPageIndex
        BindDataToGrid(dtgContract.CurrentPageIndex)
    End Sub

    Private Sub dtgContract_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgContract.SortCommand
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

        dtgContract.SelectedIndex = -1
        dtgContract.CurrentPageIndex = 0
        BindDataToGrid(dtgContract.CurrentPageIndex)
    End Sub

#End Region



End Class