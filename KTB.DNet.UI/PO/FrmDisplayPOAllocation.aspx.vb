#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports System.IO
#End Region

Public Class FrmDisplayPOAllocation
    Inherits System.Web.UI.Page

#Region "Private variables"
    Private ssHelper As SessionHelper = New SessionHelper
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload2 As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload1 As System.Web.UI.WebControls.Button
    Private FlagDownload As String
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTahunPerakitan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents icPermintaanKirim2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPermintaanKirim1 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlTipeWarna As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgPOAllocation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisOrder As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents CheckBoxDS As System.Web.UI.WebControls.CheckBox

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
    Private arlPODetail As ArrayList
    Private objPODetail As PODetail
    Private arlGroupPODetail As ArrayList
    Private _ATPStok As Double = 0
    Private _TotalOrder As Double = 0
    Private _TotalAlokasi As Double = 0
    Private _OrderTidakTerpenuhi As Double = 0
#End Region

#Region "Custom Method"

    Private Sub BindddlCategory()

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrayListCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
        If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
            Dim listitemBlank As New ListItem("Silahkan Pilih", -1)
            ddlKategori.Items.Add(listitemBlank)
        End If
        For Each item As Category In arrayListCategory
            Dim listItem As New ListItem(item.CategoryCode, item.ID)
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            End If
        Next
    End Sub

    Private Sub BindddlTipe(ByVal Category As String)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Category.CategoryCode", MatchType.Exact, Category))
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("VechileTypeCode")) Then
            sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If
        ddlTipe.DataSource = New VechileTypeFacade(User).Retrieve(criterias, sortColl)
        ddlTipe.DataTextField = "VechileTypeCode"
        ddlTipe.DataValueField = "id"
        ddlTipe.DataBind()
        ddlTipe.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub BindddlTipeWarna(ByVal KodeTipe As String)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, KodeTipe))
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("MaterialNumber")) Then
            sortColl.Add(New Sort(GetType(VechileColor), "MaterialNumber", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If
        ddlTipeWarna.DataSource = New VechileColorFacade(User).Retrieve(criterias, sortColl)
        ddlTipeWarna.DataTextField = "MaterialNumber"
        ddlTipeWarna.DataValueField = "id"
        ddlTipeWarna.DataBind()
        ddlTipeWarna.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub BindToDropDownlist()
        '--DropDownList Tahun Perakitan
        Me.ddlTahunPerakitan.DataSource = LookUp.ArraylistYear(True, 10, 1, DateTime.Now.Year.ToString)
        Me.ddlTahunPerakitan.DataBind()
        Me.ddlTahunPerakitan.SelectedValue = Now.Year

        BindddlCategory()
        If ddlKategori.Items.Count <> 0 Then
            BindddlTipe(ddlKategori.SelectedItem.ToString)
            If ddlTipe.Items.Count <> 0 Then
                BindddlTipeWarna(ddlTipe.SelectedItem.ToString)
            End If
        End If

        '--DropDownList Jenis Order
        For Each item As ListItem In LookUp.ArrayJenisPO
            If item.Text = "Harian" Then
                If SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Harian) Then
                    ddlJenisOrder.Items.Add(item)
                End If
            ElseIf item.Text = "Tambahan" Then
                If SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Tambahan) Then
                    ddlJenisOrder.Items.Add(item)
                End If
            End If
        Next

    End Sub

    Private Sub BindToDataGrid(ByVal IsRefresh As Boolean)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.Exact, CType(enumStatusPO.Status.Konfirmasi, Short)))

        '--Get Criterias From Calendar
        If CType(icPermintaanKirim1.Value, Date) <= CType(icPermintaanKirim2.Value, Date) Then
            Dim TanggalAwal As New DateTime(CInt(icPermintaanKirim1.Value.Year), CInt(icPermintaanKirim1.Value.Month), CInt(icPermintaanKirim1.Value.Day), 0, 0, 0)
            Dim TanggalAkhir As New DateTime(CInt(icPermintaanKirim2.Value.Year), CInt(icPermintaanKirim2.Value.Month), CInt(icPermintaanKirim2.Value.Day), 0, 0, 0)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReqAllocationDateTime", MatchType.Lesser, Format(TanggalAkhir.AddDays(1), "yyyy-MM-dd HH:mm:ss")))
        Else
            MessageBox.Show(SR.InvalidRangeDate)
        End If

        '-- Get Criterias From DropDownlist
        If ddlTahunPerakitan.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ContractHeader.ProductionYear", MatchType.Exact, ddlTahunPerakitan.SelectedValue))
        End If

        If ddlKategori.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ContractHeader.Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        End If

        If ddlTipe.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "ContractDetail.VechileColor.VechileType.ID", ddlTipe.SelectedValue))
        End If

        If ddlTipeWarna.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "ContractDetail.VechileColor.ID", ddlTipeWarna.SelectedValue))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.POType", ddlJenisOrder.SelectedValue))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(PODetail), "ContractDetail.VechileColor.MaterialNumber", Sort.SortDirection.ASC))
        Try
            arlPODetail = New PODetailFacade(User).Retrieve(criterias, sortColl)
        Catch ex As Exception
            If IsRefresh Then
                MessageBox.Show("Data belum dapat ditampilkan saat ini, karena kapasitas server sedang penuh.\nSilakan klik tombol 'Cari' setelah 2-3 menit.")
            Else
                MessageBox.Show("Data belum dapat ditampilkan saat ini, karena kapasitas server sedang penuh.\nSilakan ulangi setelah 2-3 menit.")
            End If
        End Try

        dtgPOAllocation.DataSource = GroupArrayList(arlPODetail)
        Dim ssHelper As SessionHelper = New SessionHelper
        ssHelper.SetSession("POTODOWNLOWD", arlPODetail)
        If arlPODetail.Count > 0 Then
            dtgPOAllocation.DataBind()
        Else
            dtgPOAllocation.DataBind()
            MessageBox.Show("Data Tidak Ditemukan")
        End If

    End Sub

    Private Function GroupArrayList(ByVal arrList As ArrayList) As ArrayList
        arlGroupPODetail = New ArrayList
        For Each item As PODetail In arlPODetail
            If (arlGroupPODetail.Count <> 0) Then
                Dim IsAlreadyExist As Boolean = False
                For Each item1 As PODetail In arlGroupPODetail
                    If item1.ContractDetail.VechileColor.ID = item.ContractDetail.VechileColor.ID Then
                        If item1.POHeader.ContractHeader.ProductionYear = item.POHeader.ContractHeader.ProductionYear Then
                            IsAlreadyExist = True
                            Exit For
                        End If
                    End If
                Next
                If Not IsAlreadyExist Then
                    arlGroupPODetail.Add(item)
                End If
            Else
                arlGroupPODetail.Add(item)
            End If
        Next
        Return arlGroupPODetail
    End Function

    Private Function CountTotal(ByVal objPODetail As PODetail, ByVal Type As String) As Integer
        Dim Total As Integer = 0
        For Each item As PODetail In arlPODetail
            If (item.ContractDetail.VechileColor.ID = objPODetail.ContractDetail.VechileColor.ID) Then
                If item.POHeader.ContractHeader.ProductionYear = objPODetail.POHeader.ContractHeader.ProductionYear Then
                    Select Case (Type)
                        Case "O/C Unit"
                            Total = Total + CInt(item.ContractDetail.SisaUnit)
                        Case "Propose"
                            Total = Total + CInt(item.ProposeQty)
                        Case "Alokasi"
                            Total = Total + CInt(item.AllocQty)
                        Case "Request"
                            Total = Total + CInt(item.ReqQty)
                    End Select
                End If
            End If
        Next
        Return Total
    End Function


    Private Sub SetTotalOCAndSO(ByVal oPOD As PODetail)
        Dim PeriodMonth As Integer, PeriodYear As Integer, VehicleColorID As Integer, DealerID As Integer
        Dim TotOC As Decimal, TotSO As Decimal

        PeriodMonth = oPOD.POHeader.ContractHeader.ContractPeriodMonth
        PeriodYear = oPOD.POHeader.ContractHeader.ContractPeriodYear
        VehicleColorID = oPOD.ContractDetail.VechileColor.ID
        DealerID = oPOD.POHeader.Dealer.ID

        If IsNothing(ViewState.Item(PeriodYear.ToString & "." & PeriodMonth.ToString & "." & VehicleColorID.ToString & "." & DealerID.ToString & ".TotalOC")) Then

            ViewState.Add(PeriodYear.ToString & "." & PeriodMonth.ToString & "." & VehicleColorID.ToString & "." & DealerID.ToString & ".TotalOC", 0)
            ViewState.Add(PeriodYear.ToString & "." & PeriodMonth.ToString & "." & VehicleColorID.ToString & "." & DealerID.ToString & ".TotalSO", 0)

            Dim cPOD As New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aPOD As ArrayList
            Dim oPODFac As New PODetailFacade(User)
            Dim aggPOD As New Aggregate(GetType(PODetail), "AllocQty", AggregateType.Sum)
            Dim cCD As New CriteriaComposite(New Criteria(GetType(ContractDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aCD As ArrayList
            Dim oCDFac As New ContractDetailFacade(User)
            Dim aggCD As New Aggregate(GetType(ContractDetail), "TargetQty", AggregateType.Sum)


            TotOC = 0
            TotSO = 0
            cPOD.opAnd(New Criteria(GetType(PODetail), "POHeader.ContractHeader.ContractPeriodMonth", MatchType.Exact, PeriodMonth))
            cPOD.opAnd(New Criteria(GetType(PODetail), "POHeader.ContractHeader.ContractPeriodYear", MatchType.Exact, PeriodYear))
            cPOD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.ID", MatchType.Exact, VehicleColorID))
            cPOD.opAnd(New Criteria(GetType(PODetail), "POHeader.Dealer.ID", MatchType.Exact, DealerID))
            aPOD = oPODFac.Retrieve(cPOD)
            Dim Idx As Integer = 0
            For Each oPODetail As PODetail In aPOD
                If oPODetail.POHeader.Status = enumStatusPO.Status.Selesai Then
                    'If IsAlreadyCounted(oPODetail, Idx, aPOD) = False Then
                    '    TotOC += oPODetail.ContractDetail.TargetQty
                    'End If
                    TotSO += oPODetail.AllocQty
                End If
                Idx += 1
            Next

            cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.ContractPeriodMonth", MatchType.Exact, PeriodMonth))
            cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.ContractPeriodYear", MatchType.Exact, PeriodYear))
            cCD.opAnd(New Criteria(GetType(ContractDetail), "VechileColor.ID", MatchType.Exact, VehicleColorID))
            cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.Dealer.ID", MatchType.Exact, DealerID))
            aCD = oCDFac.Retrieve(cCD)
            For Each oCD As ContractDetail In aCD
                TotOC += oCD.TargetQty
            Next

            ViewState.Item(PeriodYear.ToString & "." & PeriodMonth.ToString & "." & VehicleColorID.ToString & "." & DealerID.ToString & ".TotalOC") = TotOC
            ViewState.Item(PeriodYear.ToString & "." & PeriodMonth.ToString & "." & VehicleColorID.ToString & "." & DealerID.ToString & ".TotalSO") = TotSO
        End If
    End Sub

    Private Function IsAlreadyCounted(ByVal oPOD As PODetail, ByVal Idx As Integer, ByRef aPOD As ArrayList) As Boolean
        Dim i As Integer
        Dim oPODetail As PODetail
        Dim bDone As Boolean = False

        For i = 0 To aPOD.Count - 1
            If i < Idx Then
                oPODetail = CType(aPOD(i), PODetail)
                If oPODetail.ContractDetail.ID = oPOD.ContractDetail.ID Then
                    bDone = True
                    Return bDone
                End If
            End If
        Next
        Return bDone
    End Function

    Private Sub GetTotalOCAndSO(ByVal oPOD As PODetail, ByRef TotOC As Decimal, ByRef TotSO As Decimal)
        Dim PeriodMonth As Integer, PeriodYear As Integer, VehicleColorID As Integer, DealerID As Integer

        PeriodMonth = oPOD.POHeader.ContractHeader.ContractPeriodMonth
        PeriodYear = oPOD.POHeader.ContractHeader.ContractPeriodYear
        VehicleColorID = oPOD.ContractDetail.VechileColor.ID
        DealerID = oPOD.POHeader.Dealer.ID

        If Not IsNothing(ViewState.Item(PeriodYear.ToString & "." & PeriodMonth.ToString & "." & VehicleColorID.ToString & "." & DealerID.ToString & ".TotalOC")) Then
            TotOC = CType(ViewState.Item(PeriodYear.ToString & "." & PeriodMonth.ToString & "." & VehicleColorID.ToString & "." & DealerID.ToString & ".TotalOC"), Decimal)
            TotSO = CType(ViewState.Item(PeriodYear.ToString & "." & PeriodMonth.ToString & "." & VehicleColorID.ToString & "." & DealerID.ToString & ".TotalSO"), Decimal)
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack Then
            BindToDropDownlist()
            RefreshData()

            If (ddlJenisOrder.SelectedValue = LookUp.EnumJenisOrder.Tambahan) Then
                CheckBoxDS.Checked = True
            Else
                CheckBoxDS.Checked = False
            End If
        End If

    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.AlokasiPOView_Privilege) _
            OrElse Not SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Harian) _
            OrElse Not SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Tambahan) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Alokasi PO")
        End If
        dtgPOAllocation.Columns(8).Visible = SecurityProvider.Authorize(Context.User, SR.AlokasiPODetail_Privielge)

    End Sub

    Private Sub RefreshData()
        If Not Request.QueryString("orderType") = String.Empty Then
            icPermintaanKirim1.Value = Request.QueryString("start")
            icPermintaanKirim2.Value = Request.QueryString("end")
            ddlTahunPerakitan.SelectedValue = Request.QueryString("productionYear")
            ddlJenisOrder.SelectedValue = Request.QueryString("orderType")
            If ddlKategori.Items.Count > 0 Then
                Try
                    ddlKategori.SelectedValue = Request.QueryString("kategori")
                    Me.ddlKategori_SelectedIndexChanged(Nothing, Nothing)
                    If ddlTipe.Items.Count > 0 Then
                        Try
                            ddlTipe.SelectedValue = Request.QueryString("Tipe")
                            Me.ddlTipe_SelectedIndexChanged(Nothing, Nothing)
                            If Me.ddlTipeWarna.Items.Count > 0 Then
                                Try
                                    ddlTipeWarna.SelectedValue = Request.QueryString("MaterialNumber")
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try
            End If
            'ddlKategori.SelectedValue = Request.QueryString("kategori")
            'ddlTipe.SelectedValue = Request.QueryString("Tipe")
            'ddlTipeWarna.SelectedValue = Request.QueryString("MaterialNumber")
            BindToDataGrid(True)
        End If

    End Sub

    Sub dtgPOAllocation_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If Not (arlGroupPODetail Is Nothing) Then
            If Not (e.Item.ItemIndex = -1) Then
                objPODetail = arlGroupPODetail(e.Item.ItemIndex)
                e.Item.Cells(0).Text = objPODetail.ContractDetail.VechileColor.ID
                e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgPOAllocation.PageSize * dtgPOAllocation.CurrentPageIndex)).ToString
                e.Item.Cells(2).Text = objPODetail.ContractDetail.VechileColor.MaterialDescription
                e.Item.Cells(3).Text = objPODetail.ContractDetail.VechileColor.MaterialNumber
                e.Item.Cells(4).Text = CountTotal(objPODetail, "O/C Unit")
                e.Item.Cells(5).Text = CountTotalUnit(objPODetail)
                _ATPStok += CType(e.Item.Cells(5).Text, Double)
                e.Item.Cells(6).Text = CountTotal(objPODetail, "Request")
                _TotalOrder += CType(e.Item.Cells(6).Text, Double)
                e.Item.Cells(7).Text = CountTotal(objPODetail, "Alokasi")
                _TotalAlokasi += CType(e.Item.Cells(7).Text, Double)
                If e.Item.Cells(7).Text <> 0 Then
                    e.Item.Cells(10).Text = "*"
                End If
                e.Item.Cells(8).Text = (CInt(e.Item.Cells(6).Text) - CInt(e.Item.Cells(7).Text)).ToString
                _OrderTidakTerpenuhi += CType(e.Item.Cells(8).Text, Double)
            End If
            If e.Item.ItemType = ListItemType.Footer Then
                e.Item.Cells(3).Text = "SubTotal :"
                e.Item.Cells(5).Text = FormatNumber(_ATPStok, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(6).Text = FormatNumber(_TotalOrder, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(7).Text = FormatNumber(_TotalAlokasi, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(8).Text = FormatNumber(_OrderTidakTerpenuhi, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        ' Modified by Ikhsan, 20090113
        ' Requested by Yurike, as bug
        ' Start -----
        If Not (CheckBoxDS.Checked) Then
            FlagDownload = "TRUE"
        Else
            FlagDownload = "FALSE"
        End If
        ssHelper.RemoveSession("ATPFlag")
        ssHelper.SetSession("ATPFlag", FlagDownload)
        ' End -------

        If IsPermintaanValid() Then
            BindToDataGrid(False)
        Else
            MessageBox.Show("Range Permintaan Kirim harus dalam 1 bulan")
        End If

    End Sub

    Private Function CountTotalUnit(ByVal pODetail As PODetail) As Integer
        'Dim FlagDownload As Boolean = False
        Dim arrListPPQty As ArrayList
        Dim total As Integer
        Dim TotalATP As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "MaterialNumber", MatchType.Exact, pODetail.ContractDetail.VechileColor.MaterialNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeMonth", MatchType.Exact, DateTime.Now.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeYear", MatchType.Exact, DateTime.Now.Year))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "ProductionYear", MatchType.Exact, pODetail.POHeader.ContractHeader.ProductionYear))

        If (ddlJenisOrder.SelectedValue = LookUp.EnumJenisOrder.Harian) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", MatchType.Exact, DateTime.Now.Day))
            arrListPPQty = New PPQtyFacade(User).Retrieve(criterias)
            If arrListPPQty.Count > 0 Then
                TotalATP = CType(arrListPPQty(0), PPQty).TotalSisa() ' .TotalATPInDB()
            End If
            For Each item As PPQty In arrListPPQty
                total = total + item.AllocationQty
            Next
        Else
            ' Modified by Ikhsan, 22 October 2008
            ' To anticipate the minus value of ATP Stock
            ' Requested by Yurike/Andra/Doni KTB
            Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", AggregateType.Max)
            Dim MaxTgl As Integer = New PPQtyFacade(User).RetrieveScalar(criterias, agg)
            If MaxTgl <> 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", MatchType.Exact, MaxTgl))
                arrListPPQty = New PPQtyFacade(User).Retrieve(criterias)
                If arrListPPQty.Count > 0 Then
                    TotalATP = CType(arrListPPQty(0), PPQty).TotalSisa() '.TotalATPInDB() ' .TotalSisa()
                End If
                For Each item As PPQty In arrListPPQty
                    total = total + item.AllocationQty
                Next
                Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, pODetail.ContractDetail.VechileColor.MaterialNumber))
                'If Not (CheckBoxDS.Checked) Then
                If CType(ssHelper.GetSession("ATPFlag"), String) = "TRUE" Then
                    criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.Exact, CType(enumStatusPK.Status.Rilis, Integer)))
                    'criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
                    'FlagDownload = "FALSE"
                    FlagDownload = "TRUE"
                Else
                    criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
                    'FlagDownload = "TRUE"
                    FlagDownload = "FALSE"
                    'ssHelper.RemoveSession("ATPFlag")
                    'ssHelper.SetSession("ATPFlag", FlagDownload)
                End If
                ssHelper.RemoveSession("ATPFlag")
                ssHelper.SetSession("ATPFlag", FlagDownload)
                'criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseDate", MatchType.Exact, MaxTgl))
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseMonth", MatchType.Exact, DateTime.Now.Month))
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseYear", MatchType.Exact, DateTime.Now.Year))
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ContractHeader.ProductionYear", MatchType.Exact, pODetail.POHeader.ContractHeader.ProductionYear))

                Dim arrListPODetail = New PODetailFacade(User).Retrieve(criterias1)
                For Each item As PODetail In arrListPODetail
                    total = total - item.AllocQty
                Next
                'ATP MINUS
                'if total < 0 then total = 0
            Else
                Return 0
            End If
        End If
        Return TotalATP 'total
    End Function

    Sub dtgPOAllocation_itemCommand(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        Response.Redirect("../PO/FrmEntryPOAllocation.aspx?id=" & e.Item.Cells(0).Text & "&start=" & icPermintaanKirim1.Value.ToShortDateString & "&end=" & icPermintaanKirim2.Value.ToShortDateString & "&orderType=" & ddlJenisOrder.SelectedValue.ToString & "&productionYear=" & ddlTahunPerakitan.SelectedValue & "&Kategori=" & ddlKategori.SelectedValue & "&Tipe=" & ddlTipe.SelectedValue & "&MaterialNumber=" & ddlTipeWarna.SelectedValue)
    End Sub

    Private Function IsPermintaanValid() As Boolean
        If icPermintaanKirim1.Value.Month <> icPermintaanKirim2.Value.Month Or icPermintaanKirim1.Value.Year <> icPermintaanKirim2.Value.Year Then
            Return False
        End If
        Return True
    End Function

    Private Sub ddlKategori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlKategori.SelectedIndexChanged
        BindddlTipe(ddlKategori.SelectedItem.ToString)
        BindddlTipeWarna(ddlTipe.SelectedItem.ToString)
    End Sub

    Private Sub ddlTipe_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTipe.SelectedIndexChanged
        BindddlTipeWarna(ddlTipe.SelectedItem.ToString)
    End Sub

#End Region


    Private Sub btnDownload1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload1.Click
        If IsPermintaanValid() Then
            BindToDataGrid(False)
        Else
            MessageBox.Show("Range Permintaan Kirim harus dalam 1 bulan")
            Exit Sub
        End If
        Response.Redirect("FrmDisplayPOAllocationDownload.aspx")
    End Sub

    Private Sub btnDownload2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload2.Click
        If IsPermintaanValid() Then
            BindToDataGrid(False)
        Else
            MessageBox.Show("Range Permintaan Kirim harus dalam 1 bulan")
            Exit Sub
        End If

        If ddlKategori.SelectedIndex = 0 Then
            MessageBox.Show("Kategori harus dipilih")
            Exit Sub
        End If

        Dim success As Boolean = False
        Dim Connect As Boolean = False
        Dim sw As StreamWriter
        Dim filename As String = "DataTemp\POAllocation.xls"
        Dim DestFile As String = Server.MapPath("").Replace("\PO", "") & "\" & filename '-- Destination file

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If (Connect = False) Then
            imp = New SAPImpersonate(_user, _password, _webServer)
            Dim finfo As New FileInfo(DestFile)
            Try
                success = imp.Start()
                If success Then
                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    sw = New StreamWriter(DestFile)
                    Connect = True
                End If
            Catch ex As Exception
                Throw ex
                Exit Sub
            End Try
        End If

        Dim i As Integer = 0
        Dim strLine As String = String.Empty
        Dim strTab As String = Chr(9)
        Dim VehicleColorID As Integer = 0
        Dim ProductionYear As Integer = 0
        Dim TotalOC As Integer = 0
        Dim TotalSO As Integer = 0
        Dim PCTSO As Integer = 0
        Dim OrdReq As Integer = 0
        Dim StokATP As Integer = 0
        Dim OrdConfirm As Integer = 0
        Dim STotalOC As Integer
        Dim STotalSO As Integer
        Dim SOrdReq As Integer
        Dim SOrdConfirm As Integer
        Dim TTotalOC As Integer
        Dim TTotalSO As Integer
        Dim TOrdReq As Integer
        Dim TOrdConfirm As Integer
        Dim TStokATP As Integer = 0
        Dim vstTotalOC As Decimal = 0
        Dim vstTotalSO As Decimal = 0

        strLine = "No" + strTab + "Dealer" + strTab + "Nama Dealer" + strTab + "Nomor PO" + strTab + "Nama Pesanan Khusus" + strTab + _
             "Cara Pembayaran" + strTab + "Model/Tipe/Warna" + strTab + "Total OC (a)" + strTab + "Total SO (b)" + strTab + _
             "% (c=b/a)" + strTab + "Dealer Order" + strTab + "ATP Stock (d)" + strTab + "Dealer Order Confirmation (e)" + strTab + "Balance Unit (f=d-e)" + strTab + _
             "Balance OC (g=a-b-e)" + strTab + "Reason"
        sw.WriteLine(strLine)

        For Each item As PODetail In arlPODetail
            If VehicleColorID = 0 Then
                VehicleColorID = item.ContractDetail.VechileColor.ID
                ProductionYear = item.POHeader.ContractHeader.ProductionYear
            End If
            Me.SetTotalOCAndSO(item)
            Me.GetTotalOCAndSO(item, vstTotalOC, vstTotalSO)

            If item.ContractDetail.VechileColor.ID <> VehicleColorID Then
                PCTSO = 0
                If STotalOC > 0 Then
                    PCTSO = STotalSO / STotalOC * 100
                End If
                strLine = strTab + strTab + strTab + strTab + strTab + strTab + "SubTotal" + strTab + _
                      STotalOC.ToString + strTab + _
                      STotalSO.ToString + strTab + _
                      PCTSO.ToString + strTab + _
                      SOrdReq.ToString + strTab + _
                      StokATP.ToString + strTab + _
                      SOrdConfirm.ToString + strTab + _
                      CType(StokATP - SOrdConfirm, String) + strTab + _
                      CType(STotalOC - STotalSO - SOrdConfirm, String)
                sw.WriteLine(strLine)
                sw.WriteLine(strTab)

                VehicleColorID = item.ContractDetail.VechileColor.ID
                i = 0
                STotalOC = 0
                STotalSO = 0
                SOrdReq = 0
                SOrdConfirm = 0
                TStokATP = TStokATP + StokATP
            ElseIf item.POHeader.ContractHeader.ProductionYear <> ProductionYear Then
                PCTSO = 0
                If STotalOC > 0 Then
                    PCTSO = STotalSO / STotalOC * 100
                End If
                strLine = strTab + strTab + strTab + strTab + strTab + strTab + "SubTotal" + strTab + _
                      STotalOC.ToString + strTab + _
                      STotalSO.ToString + strTab + _
                      PCTSO.ToString + strTab + _
                      SOrdReq.ToString + strTab + _
                      StokATP.ToString + strTab + _
                      SOrdConfirm.ToString + strTab + _
                      CType(StokATP - SOrdConfirm, String) + strTab + _
                      CType(STotalOC - STotalSO - SOrdConfirm, String)

                sw.WriteLine(strLine)
                sw.WriteLine(strTab)

                ProductionYear = item.POHeader.ContractHeader.ProductionYear
                i = 0
                STotalOC = 0
                STotalSO = 0
                SOrdReq = 0
                SOrdConfirm = 0
                TStokATP = TStokATP + StokATP
            End If

            i = i + 1
            TotalOC = vstTotalOC '20110927 'TotalOC = CInt(item.ContractDetail.TargetQty)
            TotalSO = vstTotalSO '20110927 'TotalSO = CInt(item.AllocQty
            PCTSO = 0
            If TotalOC > 0 Then
                PCTSO = TotalSO / TotalOC * 100
            End If
            OrdReq = CInt(item.ReqQty)
            StokATP = CInt(item.StokATP)
            OrdConfirm = item.AllocQty 'vstTotalSO - item.AllocQty '20110927 'OrdConfirm = CInt(item.ProposeQty)

            STotalOC = vstTotalOC 'STotalOC = STotalOC + CInt(item.ContractDetail.TargetQty)
            STotalSO = vstTotalSO 'STotalSO = STotalSO + CInt(item.AllocQty)
            SOrdReq = SOrdReq + CInt(item.ReqQty)
            SOrdConfirm = item.AllocQty ' vstTotalSO - item.AllocQty '20110927 'SOrdConfirm = SOrdConfirm + CInt(item.ProposeQty)

            TTotalOC = vstTotalOC '20110927 'TTotalOC = TTotalOC + CInt(item.ContractDetail.TargetQty)
            TTotalSO = vstTotalSO '20110927 'TTotalSO = TTotalSO + CInt(item.AllocQty)


            TOrdReq = TOrdReq + CInt(item.ReqQty)
            TOrdConfirm = item.AllocQty ' vstTotalSO - item.AllocQty '20110927 'TOrdConfirm = TOrdConfirm + CInt(item.ProposeQty)


            strLine = i.ToString + strTab + _
                      item.ContractDetail.ContractHeader.Dealer.DealerCode + strTab + _
                      item.ContractDetail.ContractHeader.Dealer.SearchTerm1 + strTab + _
                      item.POHeader.DealerPONumber + strTab + _
                      item.ContractDetail.ContractHeader.ProjectName + strTab + _
                      item.POHeader.TermOfPayment.Description + strTab + _
                      item.ContractDetail.VechileColor.MaterialDescription + strTab + _
                      TotalOC.ToString + strTab + _
                      TotalSO.ToString + strTab + _
                      PCTSO.ToString + strTab + _
                      OrdReq.ToString + strTab + _
                      StokATP.ToString + strTab + _
                      OrdConfirm.ToString + strTab + _
                      CType(StokATP - OrdConfirm, String) + strTab + _
                      CType(TotalOC - TotalSO - OrdConfirm, String)
            sw.WriteLine(strLine)
        Next


        PCTSO = 0
        If STotalOC > 0 Then
            PCTSO = STotalSO / STotalOC * 100
        End If
        strLine = strTab + strTab + strTab + strTab + strTab + strTab + "SubTotal" + strTab + _
              STotalOC.ToString + strTab + _
              STotalSO.ToString + strTab + _
              PCTSO.ToString + strTab + _
              SOrdReq.ToString + strTab + _
              StokATP.ToString + strTab + _
              SOrdConfirm.ToString + strTab + _
              CType(StokATP - SOrdConfirm, String) + strTab + _
              CType(STotalOC - STotalSO - SOrdConfirm, String)
        sw.WriteLine(strLine)

        sw.WriteLine(strTab)

        PCTSO = 0
        If TTotalOC > 0 Then
            PCTSO = TTotalSO / TTotalOC * 100
        End If
        strLine = strTab + strTab + strTab + strTab + strTab + strTab + "Total" + strTab + _
              TTotalOC.ToString + strTab + _
              TTotalSO.ToString + strTab + _
              PCTSO.ToString + strTab + _
              TOrdReq.ToString + strTab + _
              TStokATP.ToString + strTab + _
              TOrdConfirm.ToString + strTab + _
              CType(TStokATP - TOrdConfirm, String) + strTab + _
              CType(TTotalOC - TTotalSO - TOrdConfirm, String)
        sw.WriteLine(strLine)

        sw.WriteLine(strTab)

        strLine = strTab + strTab + strTab + strTab + strTab + strTab + strTab + strTab + "Prepared By" + strTab + strTab + "Checked By" + strTab + strTab + "Known By"
        sw.WriteLine(strLine)
        sw.WriteLine(strTab)
        sw.WriteLine(strTab)
        sw.WriteLine(strTab)
        sw.WriteLine(strTab)
        If ddlKategori.SelectedItem.Text = "LCV" Then
            strLine = strTab + strTab + strTab + strTab + strTab + strTab + strTab + strTab + strTab + strTab + "Rudi Darmawan"
        ElseIf ddlKategori.SelectedItem.Text = "CV" Then
            strLine = strTab + strTab + strTab + strTab + strTab + strTab + strTab + strTab + strTab + strTab + "Arya Pamungkas"
        Else
            strLine = strTab + strTab + strTab + strTab + strTab + strTab + strTab + strTab + strTab + strTab + "Sugeng Prasojo"
        End If
        strLine = strLine + strTab + strTab + "David K Supangkat"
        sw.WriteLine(strLine)
        strLine = strTab + strTab + strTab + strTab + strTab + strTab + strTab + strTab + "Staff" + strTab + strTab + "Section Head" + strTab + strTab + "Departement Head"
        sw.WriteLine(strLine)

        If (success = True) Then
            sw.Close()
            'Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("DownloadPOAllocation") & "\" & filename
            imp.StopImpersonate()
            imp = Nothing
            Response.Redirect("../Downloadlocal.aspx?file=" & filename)
        Else
            MessageBox.Show("Download file PO Allocation gagal")
        End If
    End Sub

    Private Sub ddlJenisOrder_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJenisOrder.SelectedIndexChanged
        'Modified by Ikhsan, 20081118
        'Requested by Yurike as Part of CR
        ddlJenisOrder.AutoPostBack = True
        If (ddlJenisOrder.SelectedValue = LookUp.EnumJenisOrder.Tambahan) Then
            CheckBoxDS.Checked = True
        Else
            CheckBoxDS.Checked = False
        End If
    End Sub
End Class