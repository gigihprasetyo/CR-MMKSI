#Region "Custom Namespace Imports"
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports System.IO
#End Region
Public Class OutStandingMO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblJenisPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisPesanan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNomorKontrak As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNomorKOntrak As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPeriodeKontrak As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNomerPk As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerPKNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKondisiPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKondisiPesanan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalSisaTebus As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgContract As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCodeValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblCityValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblPerhatian As System.Web.UI.WebControls.Label
    Protected WithEvents lblDokumen As System.Web.UI.WebControls.Label
    Protected WithEvents lblspaNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggal As System.Web.UI.WebControls.Label
    Protected WithEvents lblspaDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents KTBSearchDealer As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents DealerInfo1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents DealerInfo2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents chbxSisaMOQty As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    ' Modified by Ikhsan, 20081117
    ' Requested by Yurike as Part Of CR
    ' Start ------------------------------------------------------------------
    Protected WithEvents lblTotalSisaTebusVH As System.Web.UI.WebControls.Label
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    ' End   ------------------------------------------------------------------
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private arrListContractDetail As ArrayList
    Private arrListContractDetailAll As ArrayList
    Private objContractDetail As ContractDetail
    Private objDealer As Dealer
    Private objSessionHelper As New SessionHelper
    Private totalSisaTebus As Double = 0
    ' Modified by Ikhsan, 20081117
    ' Requested by Yurike as Part Of CR
    ' Start ------------------------------------------------------------------
    Private totalSisaTebusVH As Double = 0
    ' End   ------------------------------------------------------------------
    Private totalMO As Integer = 0
    Private totalSisaMO As Integer = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckUserPrivilege()
        If Not IsPostBack Then

            RetrieveMaster()
            ViewState("CurrentSortColumn") = "ContractHeader.ContractNumber"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            arrListContractDetail = New ArrayList
            'BindDataToGrid(dtgContract.CurrentPageIndex)
            'lblTotalSisaTebus.Text = FormatNumber(totalSisaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            'Start Remaining Module - hyperlink from FrmCeilingVsAllocation
            btnKembali.Visible = False
            If Not Request.Item("DealerCode") Is Nothing Then
                txtKodeDealer.Text = CType(Request.Item("DealerCode"), String)
                BindDataToGrid(0)
                btnKembali.Visible = True
            End If
            'End Remaining Module - hyperlink from FrmCeilingVsAllocation
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub BindDataToGrid(ByVal pageIndex As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        If ddlJenisPesanan.SelectedIndex <> 0 And ddlJenisPesanan.SelectedIndex <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.ContractType", MatchType.Exact, ddlJenisPesanan.SelectedValue))
        End If

        If txtNomorKOntrak.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.ContractNumber", MatchType.Exact, txtNomorKOntrak.Text))
        End If

        If txtDealerPKNumber.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.DealerPKNumber", MatchType.Exact, txtDealerPKNumber.Text))
        End If


        If ddlKategori.SelectedIndex <> 0 And ddlKategori.SelectedIndex <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        End If

        If ddlKondisiPesanan.SelectedIndex <> 0 And ddlKondisiPesanan.SelectedIndex <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.Purpose", MatchType.Exact, ddlKondisiPesanan.SelectedValue))
        End If

        If ddlPeriodeKontrak.SelectedIndex <> -1 Then
            Dim tanggal As DateTime = CType(ddlPeriodeKontrak.SelectedItem.ToString, DateTime)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.ContractPeriodMonth", MatchType.Exact, CInt(tanggal.Month)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.ContractPeriodYear", MatchType.Exact, CInt(tanggal.Year)))
        End If
        If Me.chbxSisaMOQty.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.IsCarriedOver", MatchType.No, 1))
        End If

        If pageIndex >= 0 Then
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(ContractDetail), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            arrListContractDetailAll = New ContractDetailFacade(User).Retrieve(criterias, sortColl)
            Dim totalNonZeroRow As Integer = 0
            Dim sisaUnit As Long = 0
            For Each item As ContractDetail In arrListContractDetailAll
                sisaUnit = item.SisaUnit
                ' Modified by Ikhsan, 20081117
                ' Requested by Yurike as Part Of CR
                ' Start ------------------------------------------------------------------
                'totalSisaTebus += CDbl(sisaUnit) * (CDbl(item.Amount) + CDbl(item.PPh22))

                totalSisaTebusVH += CDbl(sisaUnit) * CDbl(item.Amount)
                totalSisaTebus += CDbl(sisaUnit) * CDbl(item.PPh22)
                totalMO += item.TargetQty
                totalSisaMO += sisaUnit
                If sisaUnit > 0 Then
                    totalNonZeroRow += 1
                End If
            Next
            'lblTotalSisaTebus.Text = FormatNumber(totalSisaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotalSisaTebusVH.Text = FormatNumber(totalSisaTebusVH, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotalSisaTebus.Text = FormatNumber(totalSisaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            If chbxSisaMOQty.Checked Then
                arrListContractDetail = GetNonZeroSisaQuantity()
                dtgContract.VirtualItemCount = totalNonZeroRow
                objSessionHelper.SetSession("MOCollection", GetNonZeroSisaQuantityforDownload())
            Else
                arrListContractDetail = New ContractDetailFacade(User).RetrieveActiveList(criterias, pageIndex + 1, dtgContract.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
                dtgContract.VirtualItemCount = totalRow
                objSessionHelper.SetSession("MOCollection", arrListContractDetailAll)
            End If


            dtgContract.DataSource = arrListContractDetail
            If arrListContractDetail.Count > 0 AndAlso objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                DisplayFootNote(True)
                dtgContract.DataBind()
            Else
                dtgContract.DataBind()
                DisplayFootNote(False)
                If arrListContractDetail.Count = 0 Then
                    MessageBox.Show("Data Tidak Ditemukan")
                End If
            End If


        End If

    End Sub

    Private Function GetNonZeroSisaQuantity() As ArrayList
        Dim NonZeroArrlist As New ArrayList
        For Each item As ContractDetail In arrListContractDetailAll
            If item.SisaUnit > 0 Then
                NonZeroArrlist.Add(item)
            End If
        Next
        Dim returnedArrList As New ArrayList
        Dim startIndex As Integer = dtgContract.PageSize * dtgContract.CurrentPageIndex
        Dim endIndex As Integer = dtgContract.PageSize * dtgContract.CurrentPageIndex + (dtgContract.PageSize - 1)
        For i As Integer = startIndex To endIndex
            Try
                Dim _contractDetail As ContractDetail = NonZeroArrlist(i)
                returnedArrList.Add(_contractDetail)
            Catch ex As Exception
                Return returnedArrList
            End Try
        Next
        Return returnedArrList
    End Function

    ' Modified By Ikhsan, 20080109
    ' Requested by Yurike/Peggy as Part of CR
    ' Enhance download mode for un-checked type
    ' Start -----
    Private Function GetNonZeroSisaQuantityforDownload() As ArrayList
        Dim NonZeroArrlist As New ArrayList
        For Each item As ContractDetail In arrListContractDetailAll
            If item.SisaUnit > 0 Then
                NonZeroArrlist.Add(item)
            End If
        Next
        'Dim returnedArrList As New ArrayList
        'Dim startIndex As Integer = dtgContract.PageSize * dtgContract.CurrentPageIndex
        'Dim endIndex As Integer = dtgContract.PageSize * dtgContract.CurrentPageIndex + (dtgContract.PageSize - 1)
        'For i As Integer = startIndex To endIndex
        '    Try
        '        Dim _contractDetail As ContractDetail = NonZeroArrlist(i)
        '        returnedArrList.Add(_contractDetail)
        '    Catch ex As Exception
        '        Return returnedArrList
        '    End Try
        'Next
        Return NonZeroArrlist
        ' End -----
    End Function


    Private Sub DisplayFootNote(ByVal _visible As Boolean)
        lblPerhatian.Visible = _visible
        lblDokumen.Visible = _visible
        lblTanggal.Visible = _visible
        lblspaDate.Visible = _visible
        lblspaNumber.Visible = _visible
    End Sub

    Private Sub RetrieveMaster()

        'Bind To DropDownlist Category 
        Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory
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

        ddlPeriodeKontrak.DataSource = LookUp.ArraylistMonth(True, 6, 0, DateTime.Now)
        ddlPeriodeKontrak.DataBind()
        ddlPeriodeKontrak.ClearSelection()

        ddlPeriodeKontrak.SelectedIndex = 0



    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ENHPODaftarMODetilSisa_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Sisa O/C")
        End If
        ' Modified by Ikhsan, 20090112
        ' Requested by Peggy as Part Of CR
        ' Adding Privilege
        ' Start -----
        'dtgContract.Columns(9).Visible = SecurityProvider.Authorize(Context.User, SR.DaftarMOViewDetail_Privilege)
        'dtgContract.Columns(10).Visible = SecurityProvider.Authorize(Context.User, SR.PengajuanPOIconCreatePrivilege)
        'Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        'Label4.Visible = isPriceVisible
        'Label7.Visible = isPriceVisible
        'Label10.Visible = isPriceVisible
        'lblTotalSisaTebus.Visible = isPriceVisible
        'lblTotalSisaTebusVH.Visible = isPriceVisible
        'dtgContract.Columns(7).Visible = isPriceVisible
        'dtgContract.Columns(8).Visible = isPriceVisible
        Dim isPricePPVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaPPTidakTampil_Privilege))
        Dim isPriceVHVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaVHTidakTampil_Privilege))

        ' For VH
        Label2.Visible = isPriceVHVisible
        Label3.Visible = isPriceVHVisible
        Label9.Visible = isPriceVHVisible
        lblTotalSisaTebusVH.Visible = isPriceVHVisible
        dtgContract.Columns(7).Visible = isPriceVHVisible

        ' For PP Value
        Label4.Visible = isPricePPVisible
        Label7.Visible = isPricePPVisible
        Label10.Visible = isPricePPVisible
        lblTotalSisaTebus.Visible = isPricePPVisible
        dtgContract.Columns(8).Visible = isPricePPVisible

    End Sub

    Private Sub BtnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click

        If ddlKategori.SelectedValue <> -1 Then
            dtgContract.CurrentPageIndex = 0
            BindDataToGrid(dtgContract.CurrentPageIndex)
            'lblTotalSisaTebus.Text = FormatNumber(totalSisaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotalSisaTebusVH.Text = FormatNumber(totalSisaTebusVH, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblTotalSisaTebus.Text = FormatNumber(totalSisaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Else
            MessageBox.Show("Anda Belum memilih Kategori")
        End If
    End Sub

    Sub dtgContract_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If (e.Item.ItemIndex <> -1) Then
            If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
                objContractDetail = arrListContractDetail(e.Item.ItemIndex)
                'e.Item.Cells(0).Text = objContractDetail.ContractHeader.Dealer.DealerCode
                e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dtgContract.PageSize * dtgContract.CurrentPageIndex)).ToString
                Dim lblDealerCode As Label = e.Item.FindControl("lblDealerCode")
                lblDealerCode.Text = objContractDetail.ContractHeader.Dealer.DealerCode
                If lblDealerCode.Text <> String.Empty Then
                    lblDealerCode.ToolTip = objContractDetail.ContractHeader.Dealer.SearchTerm1
                End If
                e.Item.Cells(2).Text = objContractDetail.ContractHeader.ContractNumber
                e.Item.Cells(3).Text = objContractDetail.VechileColor.MaterialNumber

                Dim lbtnSPLNumber As LinkButton = e.Item.FindControl("lbtnSPLNumber")
                lbtnSPLNumber.Text = objContractDetail.ContractHeader.SPLNumber

                e.Item.Cells(5).Text = objContractDetail.ContractHeader.ProjectName
                e.Item.Cells(6).Text = objContractDetail.TargetQty
                e.Item.Cells(7).Text = objContractDetail.SisaUnit
                'Dim sisaTebus As Double = CDbl(objContractDetail.SisaUnit) * (CDbl(objContractDetail.Amount) + CDbl(objContractDetail.PPh22))
                Dim sisaTebusVH As Double = CDbl(objContractDetail.SisaUnit) * CDbl(objContractDetail.Amount)
                Dim sisaTebus As Double = CDbl(objContractDetail.SisaUnit) * CDbl(objContractDetail.PPh22)
                e.Item.Cells(8).Text = FormatNumber(sisaTebusVH, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(9).Text = FormatNumber(sisaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'totalSisaTebus += sisaTebus
                'e.Item.Cells(9).Text = FormatNumber(objContractDetail.TotalSisaJumlahTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'totalSisaTebus += objContractDetail.TotalSisaJumlahTebus            
            End If
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(6).Text = FormatNumber(totalMO, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(7).Text = FormatNumber(totalSisaMO, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'e.Item.Cells(8).Text = FormatNumber(totalSisaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(8).Text = FormatNumber(totalSisaTebusVH, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(9).Text = FormatNumber(totalSisaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)


        End If
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

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click

        If ddlKategori.SelectedValue <> -1 Then
            Dim _fileHelper As New FileHelper
            Dim fileInfo1 As New FileInfo(Server.MapPath(""))
            Try
                Dim str As FileInfo = _fileHelper.TransferOutStandingMOtoText(CType(objSessionHelper.GetSession("MOCollection"), ArrayList), fileInfo1)
                Response.Redirect("../Downloadlocal.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("OutStandingMODestFileDirectory").ToString & "\" & str.Name)
            Catch ex As Exception
                MessageBox.Show("Gagal Download File.")
            End Try
        Else
            MessageBox.Show("Anda Belum memilih Kategori")
        End If

    End Sub

    Private Sub dtgContract_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgContract.ItemCommand
        If e.CommandName = "DetailProjectName" Then
            'Dim lbtnProjectName As LinkButton = e.Item.FindControl("lbtnProjectName")

            'objSessionHelper.SetSession("Status", "View")
            'objSessionHelper.SetSession("IDSPLHeader", CInt(e.Item.Cells(0).Text))
            'Response.Redirect("FrmSPLDetail.aspx")
        ElseIf e.CommandName = "DetailSPL" Then
            Dim lbtnSPLNumber As LinkButton = e.Item.FindControl("lbtnSPLNumber")
            Dim objSPL As SPL
            Dim objSPLFac As SPLFacade = New SPLFacade(User)

            objSPL = objSPLFac.Retrieve(lbtnSPLNumber.Text)
            If Not objSPL Is Nothing Then
                If objSPL.ID > 0 Then
                    objSessionHelper.SetSession("Status", "View")
                    objSessionHelper.SetSession("IDSPLHeader", objSPL.ID)
                    Response.Redirect("../FinishUnit/FrmSPLDetail.aspx")
                End If
            End If

        End If

    End Sub

    Private Sub btnKembali_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmCeilingVsAllocation.aspx")
    End Sub
End Class
