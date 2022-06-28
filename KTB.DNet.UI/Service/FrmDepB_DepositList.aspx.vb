
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Security

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Linq
#End Region

Public Class FrmDepB_DepositList
    Inherits System.Web.UI.Page

#Region "Declaration"
    Private sHelper As New SessionHelper
    Private _selectedDepositAIDs As String

    Dim TotSA As Long = 0
    Dim TotD As Long = 0
    Dim TotK As Long = 0
    Dim TotSAkh As Long = 0

    Dim TotalDebet As Long = 0
    Dim TotalKredit As Long = 0

    Dim i As Integer = 0
    Dim Tipe As String = String.Empty

    Dim TotalDebetAll As Long = 0
    Dim TotalKreditAll As Long = 0

    Dim _lihat_daftar_depositB_Privilege As Boolean = False
#End Region

#Region "Event"

    Private Sub InitiateAuthorization()
        _lihat_daftar_depositB_Privilege = SecurityProvider.Authorize(Context.User, SR.lihat_daftar_depositB_Privilege)

        If Not _lihat_daftar_depositB_Privilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sparepart - Daftar Deposit B")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Else
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        End If
        If Not IsPostBack Then
            Initialize()
            ViewState("currSortColumn") = "Dealer.DealerCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim ArrDummy As ArrayList = New ArrayList
        Dim arDummyHeader As ArrayList = New ArrayList
        sHelper.SetSession("DepBDataToDownload", ArrDummy)
        sHelper.SetSession("DepBHeaderDownload", arDummyHeader)
        Dim diff As Integer = DateDiff(DateInterval.Month, icPeriodeFrom.Value, icPeriodeTo.Value)
        If diff <= 12 Then
            BindDataGrid()
        Else
            MessageBox.Show("Periode melebihi 12 bulan.")
        End If
    End Sub

    Private Sub BtnDownload_Click(sender As Object, e As EventArgs) Handles BtnDownload.Click
        Dim data As ArrayList = CType(sHelper.GetSession("DepBDataToDownload"), ArrayList)
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub

    Private Sub BtnDownloadHeader_Click(sender As Object, e As EventArgs) Handles BtnDownloadHeader.Click
        'sHelper.SetSession("AllHeaderToDownload", arlDepositBHeader)
        Dim data As ArrayList = CType(sHelper.GetSession("DepBHeaderDownload"), ArrayList)
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data, False, True)
        End If
    End Sub

    Private Sub BtnDownloadDtl_Click(sender As Object, e As EventArgs) Handles BtnDownloadDtl.Click
        Dim data As ArrayList = CType(sHelper.GetSession("DetailsToDownload"), ArrayList)

        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownloadDetail(data)
        End If
    End Sub

#End Region

#Region "Custom"


    Private Sub Initialize()
        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
        BindStatus()
    End Sub

    Private Sub BindStatus()

    End Sub

    Private Sub BindDataGrid()
        Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        Else
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If oDealer.DealerGroup.ID = 21 Then 'single dealer
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "Dealer.ID", MatchType.Exact, oDealer.ID))
                Else
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "Dealer.DealerGroup.ID", MatchType.Exact, oDealer.DealerGroup.ID))
                End If
            End If
        End If

        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        End If

        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                    icPeriodeFrom.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                    icPeriodeTo.Value.Day, 0, 0, 0)
        dtEnd = dtEnd.AddDays(1)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.GreaterOrEqual, dtStart))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.Lesser, dtEnd))

        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositBHeader), ViewState("currSortColumn"), ViewState("currSortDirection")))
            If ViewState("currSortColumn").ToString().Contains("DealerCode") Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositBHeader), "ProductCategory.Code", ViewState("currSortDirection")))
            End If
        Else
            sortColl = Nothing
        End If

        Dim arlDepositBHeader As ArrayList = New ArrayList()
        arlDepositBHeader = New DepositBHeaderFacade(User).Retrieve(criterias, sortColl)
        sHelper.SetSession("AllHeaderToDownload", arlDepositBHeader)
        'If arlDepositBHeader.Count > 0 Then
        '    sHelper.SetSession("AllHeaderToDownload", arlDepositBHeader)
        'End If

        Dim arlDepositBHeaderFilter As New ArrayList
        For Each item As DepositBHeader In arlDepositBHeader
            If (Not IsExist(item.Dealer.DealerCode, item.ProductCategory.Code, arlDepositBHeaderFilter)) Then
                arlDepositBHeaderFilter.Add(item)
            End If
        Next

        If (arlDepositBHeaderFilter.Count > 0) Then
            'sHelper.SetSession("AllHeaderToDownload", arlDepositBHeaderFilter)
            dtlDepositA.Visible = True
            dtlDepositA.DataSource = arlDepositBHeaderFilter
            dtlDepositA.DataBind()
        Else
            dtlDepositA.Visible = False
            sHelper.SetSession("AllHeaderToDownload", Nothing)
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Function IsExist(ByVal DealerCode As String, ByVal ProductCategoryCode As String, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As DepositBHeader In arl
            If item.Dealer.DealerCode.Trim().ToUpper() = DealerCode.Trim().ToUpper() AndAlso item.ProductCategory.Code = ProductCategoryCode Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Private Sub GetAllTotal(ByVal DealerCode As String, ByVal ProductCategoryCode As String, ByRef TotalSaldo As Long, ByRef TotalDebet As Long, ByRef TotalKredit As Long, ByRef TotalSaldoAkhir As Long)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "ProductCategory.Code", MatchType.Exact, ProductCategoryCode))

        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                    icPeriodeFrom.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                    icPeriodeTo.Value.Day, 0, 0, 0)
        dtEnd = dtEnd.AddDays(1)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.GreaterOrEqual, dtStart))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.Lesser, dtEnd))

        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositBHeader), "TransactionDate", Sort.SortDirection.DESC))

        Dim arl As New ArrayList
        arl = New DepositBHeaderFacade(User).Retrieve(criterias, sortColl)
        If arl.Count > 0 Then
            For Each item As DepositBHeader In arl
                TotalDebet += item.DebetAmount
                TotalKredit += item.CreditAmount
            Next
            TotalSaldo = arl(arl.Count - 1).BeginingBalance
            TotalSaldoAkhir = arl(0).EndBalance
        End If
    End Sub

    Sub ToggleDisplay(ByVal Bool As Boolean)
        pnlSearch.Visible = Not Bool
        pnlDetails.Visible = Bool
    End Sub



    Sub BindDepositBForDetails(ByVal DealerCode As String, ByVal ProductCode As String)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        If ProductCode <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "ProductCategory.Code", MatchType.Exact, ProductCode))
        End If


        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                    icPeriodeFrom.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                    icPeriodeTo.Value.Day, 0, 0, 0)
        dtEnd = dtEnd.AddDays(1)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.GreaterOrEqual, dtStart))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.Lesser, dtEnd))

        Dim arlDepositB As ArrayList = New ArrayList
        arlDepositB = New DepositBHeaderFacade(User).Retrieve(criterias)

        lblPeriode.Text = icPeriodeFrom.Value.ToString("MMMM") + " " + icPeriodeFrom.Value.ToString("yyyy") + " - " + icPeriodeTo.Value.ToString("MMMM") + " " + icPeriodeTo.Value.ToString("yyyy")
        Dim strDepositAIDs As String = String.Empty
        For Each depositAItem As DepositBHeader In arlDepositB
            If strDepositAIDs.Length > 0 Then
                strDepositAIDs += ","
            End If
            strDepositAIDs += depositAItem.ID.ToString()
        Next

        lblDealerDetail.Text = DealerCode
        lblProdukDetail.Text = ProductCode

        strDepositAIDs = "(" + strDepositAIDs + ")"
        _selectedDepositAIDs = strDepositAIDs
        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "DepositBHeader.ID", MatchType.InSet, strDepositAIDs))

        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositBDetail), "TransactionDate", Sort.SortDirection.ASC))
        Dim arlDepositDetails As ArrayList = New DepositBDetailFacade(User).Retrieve(criterias, sortColl)

        dtlDetails.DataSource = arlDepositDetails
        sHelper.SetSession("DetailsToDownload", arlDepositDetails)
        dtlDetails.DataBind()
    End Sub
#End Region

    Private Sub dtlDepositA_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles dtlDepositA.ItemCommand
        If (e.CommandName = "detail") Then
            ToggleDisplay(True)
            Dim StrProcuctCode As String = CType(e.Item.FindControl("lblProductCategoryHeader"), Label).Text
            BindDepositBForDetails(e.CommandArgument, StrProcuctCode)
            lblTotalDebetAll.Text = "Rp " & CType(e.Item.FindControl("lblDebet"), Label).Text
            lblTotalKreditAll.Text = "Rp " & CType(e.Item.FindControl("lblKredit"), Label).Text
        End If
    End Sub


    Private Sub dtlDepositA_ItemDataBound(sender As Object, e As DataListItemEventArgs) Handles dtlDepositA.ItemDataBound
        Dim TotalSaldoAwal As Long = 0
        Dim TotalDebet As Long = 0
        Dim TotalKredit As Long = 0
        Dim TotalSaldoAkhir As Long = 0

        If e.Item.ItemType = ListItemType.Header Then
            Dim hypKodeDealer As HyperLink = CType(e.Item.FindControl("hypKodeDealer"), HyperLink)
            Dim lc As New LiteralControl
            lc.ID = "hypKodeDealer"

            hypKodeDealer.NavigateUrl = Page.GetPostBackClientHyperlink(lc, "Dealer.DealerCode")
        End If

        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim oD As DepositBHeader = CType(e.Item.DataItem, DepositBHeader)


            Dim lblSaldoAwal As Label = CType(e.Item.FindControl("lblSaldoAwal"), Label)
            Dim lblDebet As Label = CType(e.Item.FindControl("lblDebet"), Label)
            Dim lblKredit As Label = CType(e.Item.FindControl("lblKredit"), Label)
            Dim lblSaldoAkhir As Label = CType(e.Item.FindControl("lblSaldoAkhir"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblProductCategoryHeader As Label = CType(e.Item.FindControl("lblProductCategoryHeader"), Label)
            GetAllTotal(oD.Dealer.DealerCode, oD.ProductCategory.Code, TotalSaldoAwal, TotalDebet, TotalKredit, TotalSaldoAkhir)

            lblSaldoAwal.Text = IIf(TotalSaldoAwal = 0, 0, TotalSaldoAwal.ToString("#,###"))
            lblDebet.Text = IIf(TotalDebet = 0, 0, TotalDebet.ToString("#,###"))
            lblKredit.Text = IIf(TotalKredit = 0, 0, TotalKredit.ToString("#,###"))
            Dim strSaldoAkhir As String = IIf(TotalSaldoAkhir = 0, 0, TotalSaldoAkhir.ToString("#,###"))
            lblSaldoAkhir.Text = strSaldoAkhir
            If strSaldoAkhir.Contains("-") Then
                lblSaldoAkhir.ForeColor = Color.Red
            End If
            TotSA += TotalSaldoAwal
            TotD += TotalDebet
            TotK += TotalKredit
            TotSAkh += TotalSaldoAkhir

            Dim arHeader As New ArrayList

            Dim depo As New DepositBTemp
            depo.DealerCode = oD.Dealer.DealerCode
            depo.DealerName = oD.Dealer.DealerName
            depo.PoductCatecoryCode = lblProductCategoryHeader.Text
            depo.SaldoAwal = TotalSaldoAwal
            depo.Debet = TotalDebet
            depo.Kredit = TotalKredit
            depo.SaldoAkhir = TotalSaldoAkhir
            arHeader.Add(depo)

            Dim arrToDownloadHeader As ArrayList = sHelper.GetSession("DepBHeaderDownload")
            If arrToDownloadHeader Is Nothing Then
                arrToDownloadHeader = New ArrayList
            End If

            arrToDownloadHeader.AddRange(arHeader)
            sHelper.SetSession("DepBHeaderDownload", arrToDownloadHeader)

            Dim dtgDetail As DataGrid = CType(e.Item.FindControl("dtgDetail"), DataGrid)

            Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                        DateTime.DaysInMonth(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month), _
                                        0, 0, 0)
            Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                        DateTime.DaysInMonth(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month), _
                                        0, 0, 0)

            Dim arlDetails As New ArrayList
            While DateTime.op_LessThanOrEqual(dtStart, dtEnd)
                Dim dtPeriodFrom As DateTime = New DateTime(dtStart.Year, dtStart.Month, _
                                                1, 0, 0, 0)
                Dim dtPeriodEnd As DateTime = New DateTime(dtStart.Year, dtStart.Month, _
                                                DateTime.DaysInMonth(dtStart.Year, dtStart.Month), 0, 0, 0)
                dtPeriodEnd = dtPeriodEnd.AddDays(1)

                Dim criterias As CriteriaComposite = Nothing
                Dim aggregateSum As Aggregate = Nothing
                Dim isDataExists As Boolean = True
                Dim val As Object
                'Saldo Awal
                criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "Dealer.DealerCode", MatchType.Exact, oD.Dealer.DealerCode))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.GreaterOrEqual, dtPeriodFrom))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.Lesser, dtPeriodEnd))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "ProductCategory.Code", MatchType.Exact, oD.ProductCategory.Code))
                Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositBHeader), "TransactionDate", Sort.SortDirection.DESC))

                Dim listDepositB As ArrayList = New DepositBHeaderFacade(User).Retrieve(criterias, sortColl)
                Dim periodDebet As Decimal = 0
                Dim saldoAwal As Decimal = 0
                Dim saldoAkhir As Decimal = 0
                Dim periodKredit As Decimal = 0

                If listDepositB.Count > 0 Then
                    For Each item As DepositBHeader In listDepositB
                        periodDebet += item.DebetAmount
                        periodKredit += item.CreditAmount
                    Next
                    saldoAwal = listDepositB(listDepositB.Count - 1).BeginingBalance
                    saldoAkhir = listDepositB(0).EndBalance
                End If

                If (saldoAwal = 0 And saldoAkhir = 0 And periodDebet = 0 And periodKredit = 0) Then
                    isDataExists = False
                End If

                If isDataExists Then
                    Dim saldoPerPeriod As New DepositBPerPeriode
                    saldoPerPeriod.DealerCode = oD.Dealer.DealerCode
                    saldoPerPeriod.DealerName = oD.Dealer.DealerName
                    saldoPerPeriod.Periode = dtStart.ToString("MMMM")
                    saldoPerPeriod.SaldoAwal = saldoAwal
                    saldoPerPeriod.Debet = periodDebet
                    saldoPerPeriod.Kredit = periodKredit
                    saldoPerPeriod.SaldoAkhir = saldoAkhir
                    saldoPerPeriod.PoductCatecoryCode = lblProductCategoryHeader.Text
                    saldoPerPeriod.EnuPeriode = dtStart.ToString("yyyyMM")

                    arlDetails.Add(saldoPerPeriod)
                End If

                dtStart = dtStart.AddMonths(1)
            End While
            Dim arrToDownload As New ArrayList
            If Not IsNothing(sHelper.GetSession("DepBDataToDownload")) Then
                arrToDownload = sHelper.GetSession("DepBDataToDownload")
            End If
            arrToDownload.AddRange(arlDetails)
            sHelper.SetSession("DepBDataToDownload", arrToDownload)
            dtgDetail.DataSource = arlDetails
            dtgDetail.DataBind()
        End If

        If (e.Item.ItemType = ListItemType.Footer) Then
            Dim lblTotalSaldoAwal As Label = CType(e.Item.FindControl("lblTotalSaldoAwal"), Label)
            Dim lblTotalDebet As Label = CType(e.Item.FindControl("lblTotalDebet"), Label)
            Dim lblTotalKredit As Label = CType(e.Item.FindControl("lblTotalKredit"), Label)
            Dim lblTotalSaldoAkhir As Label = CType(e.Item.FindControl("lblTotalSaldoAkhir"), Label)
            lblTotalSaldoAwal.Text = IIf(TotSA = 0, 0, TotSA.ToString("#,###"))
            lblTotalDebet.Text = IIf(TotD = 0, 0, TotD.ToString("#,###"))
            lblTotalKredit.Text = IIf(TotK = 0, 0, TotK.ToString("#,###"))
            Dim strTotalSaldoAkhir As String = IIf(TotSAkh = 0, 0, TotSAkh.ToString("#,###"))
            lblTotalSaldoAkhir.Text = strTotalSaldoAkhir
        End If
    End Sub

    Private Class DepositBTemp

        Private _DealerCode As String = String.Empty
        Public Property DealerCode() As String
            Get
                Return _DealerCode
            End Get
            Set(ByVal Value As String)
                _DealerCode = Value
            End Set
        End Property
        Private _DealerName As String = String.Empty
        Public Property DealerName() As String
            Get
                Return _DealerName
            End Get
            Set(ByVal Value As String)
                _DealerName = Value
            End Set
        End Property

        Private _SaldoAwal As Decimal
        Public Property SaldoAwal() As Decimal
            Get
                Return _SaldoAwal
            End Get
            Set(ByVal Value As Decimal)
                _SaldoAwal = Value
            End Set
        End Property

        Private _Debet As Decimal
        Public Property Debet() As Decimal
            Get
                Return _Debet
            End Get
            Set(ByVal Value As Decimal)
                _Debet = Value
            End Set
        End Property

        Private _Kredit As Decimal
        Public Property Kredit() As Decimal
            Get
                Return _Kredit
            End Get
            Set(ByVal Value As Decimal)
                _Kredit = Value
            End Set
        End Property

        Private _SaldoAkhir As Decimal
        Public Property SaldoAkhir() As Decimal
            Get
                Return _SaldoAkhir
            End Get
            Set(ByVal Value As Decimal)
                _SaldoAkhir = Value
            End Set
        End Property

        Private _PoductCatecoryCode As String
        Public Property PoductCatecoryCode() As String
            Get
                Return _PoductCatecoryCode
            End Get
            Set(ByVal value As String)
                _PoductCatecoryCode = value
            End Set
        End Property

    End Class


    Private Class DepositBPerPeriode
        Private _DealerCode As String = String.Empty
        Public Property DealerCode() As String
            Get
                Return _DealerCode
            End Get
            Set(ByVal Value As String)
                _DealerCode = Value
            End Set
        End Property
        Private _DealerName As String = String.Empty
        Public Property DealerName() As String
            Get
                Return _DealerName
            End Get
            Set(ByVal Value As String)
                _DealerName = Value
            End Set
        End Property
        Private _Periode As String = String.Empty
        Public Property Periode() As String
            Get
                Return _Periode
            End Get
            Set(ByVal Value As String)
                _Periode = Value
            End Set
        End Property

        Private _SaldoAwal As Decimal
        Public Property SaldoAwal() As Decimal
            Get
                Return _SaldoAwal
            End Get
            Set(ByVal Value As Decimal)
                _SaldoAwal = Value
            End Set
        End Property

        Private _Debet As Decimal
        Public Property Debet() As Decimal
            Get
                Return _Debet
            End Get
            Set(ByVal Value As Decimal)
                _Debet = Value
            End Set
        End Property

        Private _Kredit As Decimal
        Public Property Kredit() As Decimal
            Get
                Return _Kredit
            End Get
            Set(ByVal Value As Decimal)
                _Kredit = Value
            End Set
        End Property

        Private _SaldoAkhir As Decimal
        Public Property SaldoAkhir() As Decimal
            Get
                Return _SaldoAkhir
            End Get
            Set(ByVal Value As Decimal)
                _SaldoAkhir = Value
            End Set
        End Property



        Private _PoductCatecoryCode As String
        Public Property PoductCatecoryCode() As String
            Get
                Return _PoductCatecoryCode
            End Get
            Set(ByVal value As String)
                _PoductCatecoryCode = value
            End Set
        End Property


        Private _EnuPeriode As String
        Public Property EnuPeriode() As String
            Get
                Return _EnuPeriode
            End Get
            Set(ByVal value As String)
                _EnuPeriode = value
            End Set
        End Property


    End Class


    Private Sub DoDownload(ByVal data As ArrayList, Optional ByVal isDetail As Boolean = False, Optional ByVal isHeader As Boolean = False)
        Dim sFileName As String = "DepositB"
        If isHeader = True Then
            sFileName = "DepositBHeader"
        End If

        sFileName = sFileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim DepositAData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DepositAData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(DepositAData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                If isDetail = True And isHeader = False Then
                    WriteAllDepositBDetail(sw, data)
                ElseIf isDetail = False And isHeader = False Then
                    WriteDepositBData(sw, data)
                End If

                If isHeader = True And isDetail = False Then
                    WriteDepositBDataHeader(sw, data)
                End If


                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub


    Private Sub WriteDepositBData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder



        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Umum - Daftar Deposit B")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Produk" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Periode" & tab)
            itemLine.Append("Saldo Awal" & tab)
            itemLine.Append("Debet(Rp)" & tab)
            itemLine.Append("Kredit (Rp)" & tab)
            itemLine.Append("Saldo(Akhir)" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            Dim DealerCode As String = ""
            Dim ProductCategoryCode As String = String.Empty

            Dim LDepA = From ObjDepositAPerPeriode As DepositBPerPeriode In data
                        Select ObjDepositAPerPeriode
                        Order By ObjDepositAPerPeriode.DealerCode Ascending, ObjDepositAPerPeriode.PoductCatecoryCode Ascending, ObjDepositAPerPeriode.EnuPeriode Ascending
            '' CR SPlit Dep A
            'For Each item As DepositAPerPeriode In data
            For Each item As DepositBPerPeriode In LDepA
                itemLine.Remove(0, itemLine.Length)

                If DealerCode <> item.DealerCode.ToString OrElse ProductCategoryCode <> item.PoductCatecoryCode Then
                    'If ProductCategoryCode <> item.PoductCatecoryCode Then
                    itemLine.Append(item.DealerCode & tab)
                    itemLine.Append(item.PoductCatecoryCode & tab)
                    itemLine.Append(item.DealerName & tab)
                    itemLine.Append(item.Periode & tab)
                    itemLine.Append(Val(item.SaldoAwal).ToString & tab)
                    itemLine.Append(Val(item.Debet).ToString & tab)
                    itemLine.Append(Val(item.Kredit).ToString & tab)
                    itemLine.Append(Val(item.SaldoAkhir).ToString & tab)
                    'End If


                Else
                    itemLine.Append(tab)
                    itemLine.Append(tab)
                    itemLine.Append(tab)
                    itemLine.Append(item.Periode & tab)
                    itemLine.Append(Val(item.SaldoAwal).ToString & tab)
                    itemLine.Append(Val(item.Debet).ToString & tab)
                    itemLine.Append(Val(item.Kredit).ToString & tab)
                    itemLine.Append(Val(item.SaldoAkhir).ToString & tab)
                End If
                sw.WriteLine(itemLine.ToString())
                DealerCode = item.DealerCode.ToString
                ProductCategoryCode = item.PoductCatecoryCode
            Next
        End If
    End Sub

    Private Sub WriteDepositBDataHeader(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder



        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Umum - Daftar Deposit B Header")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Produk" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Saldo Awal" & tab)
            itemLine.Append("Debet(Rp)" & tab)
            itemLine.Append("Kredit (Rp)" & tab)
            itemLine.Append("Saldo(Akhir)" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            Dim DealerCode As String = ""
            Dim ProductCategoryCode As String = String.Empty

            Dim LDepA = From ObjDepositAPerPeriode As DepositBTemp In data
                        Select ObjDepositAPerPeriode
                        Order By ObjDepositAPerPeriode.DealerCode Ascending, ObjDepositAPerPeriode.PoductCatecoryCode Ascending

            For Each item As DepositBTemp In LDepA
                itemLine.Remove(0, itemLine.Length)

                If DealerCode <> item.DealerCode.ToString OrElse ProductCategoryCode <> item.PoductCatecoryCode Then
                    itemLine.Append(item.DealerCode & tab)
                    itemLine.Append(item.PoductCatecoryCode & tab)
                    itemLine.Append(item.DealerName & tab)
                    itemLine.Append(Val(item.SaldoAwal).ToString & tab)
                    itemLine.Append(Val(item.Debet).ToString & tab)
                    itemLine.Append(Val(item.Kredit).ToString & tab)
                    itemLine.Append(Val(item.SaldoAkhir).ToString & tab)
                Else
                    itemLine.Append(tab)
                    itemLine.Append(tab)
                    itemLine.Append(tab)
                    itemLine.Append(Val(item.SaldoAwal).ToString & tab)
                    itemLine.Append(Val(item.Debet).ToString & tab)
                    itemLine.Append(Val(item.Kredit).ToString & tab)
                    itemLine.Append(Val(item.SaldoAkhir).ToString & tab)
                End If
                sw.WriteLine(itemLine.ToString())
                DealerCode = item.DealerCode.ToString
                ProductCategoryCode = item.PoductCatecoryCode
            Next
        End If
    End Sub


    Private Sub WriteAllDepositBDetail(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Umum - Daftar Deposit B Detail")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Produk" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Tanggal Transaksi" & tab)
            itemLine.Append("Keterangan" & tab)
            itemLine.Append("No Dokumen" & tab)
            itemLine.Append("Reference" & tab)
            itemLine.Append("Text" & tab)
            itemLine.Append("Debet(Rp)" & tab)
            itemLine.Append("Kredit (Rp)" & tab)


            sw.WriteLine(itemLine.ToString())

            For Each header As DepositBHeader In data
                For Each item As DepositBDetail In header.DepositBDetails
                    If item.RowStatus = 0 Then
                        itemLine.Remove(0, itemLine.Length)
                        itemLine.Append(header.Dealer.DealerCode & tab)
                        itemLine.Append(header.ProductCategory.Code & tab)
                        itemLine.Append(header.Dealer.DealerName & tab)
                        itemLine.Append(FormatDateTime(item.TransactionDate, 2) & tab)
                        itemLine.Append(item.Tipe & tab)
                        itemLine.Append(item.DocumentNumber & tab)
                        itemLine.Append(item.Reff & tab)
                        itemLine.Append(item.Description & tab)

                        If item.StatusDebet = 0 Then
                            itemLine.Append("0" & tab)
                            itemLine.Append(Val(item.Amount) & tab)
                        Else
                            itemLine.Append(Val(item.Amount) & tab)
                            itemLine.Append("0" & tab)
                        End If

                        sw.WriteLine(itemLine.ToString())
                    End If
                Next
            Next


        End If
    End Sub


    Private Sub DoDownloadDetail(ByVal data As ArrayList, Optional ByVal isAll As Boolean = False)
        Dim sFileName As String
        sFileName = "DepositBDetail" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim DepositAData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DepositAData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(DepositAData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                If isAll Then
                    WriteAllDepositBDetail(sw, data)
                Else
                    WriteDepositBDetail(sw, data)
                End If


                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub


    Private Sub WriteDepositBDetail(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Umum - Daftar Deposit B Detail")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(lblDealerDetail.Text & " - " & lblProdukDetail.Text)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)


            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Tanggal Transaksi" & tab)
            itemLine.Append("Keterangan" & tab)
            itemLine.Append("No Dokumen" & tab)
            itemLine.Append("Reference" & tab)
            itemLine.Append("Text" & tab)
            itemLine.Append("Debet(Rp)" & tab)
            itemLine.Append("Kredit (Rp)" & tab)


            sw.WriteLine(itemLine.ToString())

            'Dim i As Integer = 1
            'Dim DealerCode As String = ""

            For Each item As DepositBDetail In data
                itemLine.Remove(0, itemLine.Length)

                itemLine.Append(FormatDateTime(item.TransactionDate, 2) & tab)
                itemLine.Append(item.Tipe & tab)
                itemLine.Append(item.DocumentNumber & tab)
                itemLine.Append(item.Reff & tab)
                itemLine.Append(item.Description & tab)

                If item.StatusDebet = 0 Then
                    itemLine.Append("0" & tab)
                    itemLine.Append(Val(item.Amount) & tab)
                Else
                    itemLine.Append(Val(item.Amount) & tab)
                    itemLine.Append("0" & tab)
                End If

                sw.WriteLine(itemLine.ToString())
            Next
        End If
    End Sub

    Private Sub BtnDownloadAllDetail_Click(sender As Object, e As EventArgs) Handles BtnDownloadAllDetail.Click
        Dim data As ArrayList = CType(sHelper.GetSession("AllHeaderToDownload"), ArrayList)
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownloadDetail(data, True)
        End If
    End Sub


    Private _lastTransactionDate As String = String.Empty
    Private _lastPeriode As String = String.Empty
    Private _lastDate As String = String.Empty

    Private Sub dtlDetails_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles dtlDetails.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim oDepositBDetails As DepositBDetail = CType(e.Item.DataItem, DepositBDetail)

            Dim currentPeriod As String = oDepositBDetails.TransactionDate.ToString("MMMM")
            Dim currentDate As String = oDepositBDetails.TransactionDate.ToString("dd/MM/yyyy")
            If Not _lastDate.Equals(currentDate) Then
                Dim lblTransactionDate As Label = CType(e.Item.FindControl("lblTransactionDate"), Label)
                lblTransactionDate.Text = currentDate
                _lastDate = currentDate
            End If
            If Not _lastPeriode.Equals(currentPeriod) Then
                _lastPeriode = currentPeriod

                Dim tblRow As New TableRow
                tblRow.BackColor = Color.Gray
                Dim cell As New TableCell
                cell.ColumnSpan = 4

                Dim tblImage As New Table
                tblImage.CellSpacing = 0
                Dim trImage As New TableRow
                Dim cellImage As TableCell = New TableCell
                cellImage.Controls.Add(New LiteralControl("<span onclick=""toggleDepositDetail(this)""><img src=""../images/plus.gif""><img style=""display:none"" src=""../images/minus.gif""></span>"))
                trImage.Cells.Add(cellImage)

                cellImage = New TableCell
                cellImage.ForeColor = Color.White
                cellImage.Font.Bold = True
                cellImage.Text = currentPeriod
                trImage.Cells.Add(cellImage)

                tblImage.Rows.Add(trImage)
                cell.Controls.Add(tblImage)
                tblRow.Cells.Add(cell)

                cell = New TableCell
                cell.ForeColor = Color.White
                cell.Font.Bold = True
                cell.Text = "Sub Total"
                tblRow.Cells.Add(cell)

                Dim dtStart As DateTime = New DateTime(oDepositBDetails.TransactionDate.Year, oDepositBDetails.TransactionDate.Month, _
                                                1, 0, 0, 0)
                Dim dtEnd As DateTime = New DateTime(oDepositBDetails.TransactionDate.Year, oDepositBDetails.TransactionDate.Month, _
                                                DateTime.DaysInMonth(oDepositBDetails.TransactionDate.Year, oDepositBDetails.TransactionDate.Month), 0, 0, 0)
                dtEnd = dtEnd.AddDays(1)
                'Debet
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "DepositBHeader.ID", MatchType.InSet, _selectedDepositAIDs))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "StatusDebet", MatchType.Exact, 1))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "TransactionDate", MatchType.GreaterOrEqual, dtStart))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "TransactionDate", MatchType.Lesser, dtEnd))

                Dim aggregateSum As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.DepositBDetail), "Amount", AggregateType.Sum)

                Dim periodDebet As Decimal = IsDBNull(New DepositBDetailFacade(User).RetrieveScalar(criterias, aggregateSum), 0)

                cell = New TableCell
                cell.ForeColor = Color.White
                cell.HorizontalAlign = HorizontalAlign.Right
                cell.Font.Bold = True
                cell.Text = periodDebet.ToString("#,###")
                tblRow.Cells.Add(cell)

                'Kredit
                criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "DepositBHeader.ID", MatchType.InSet, _selectedDepositAIDs))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "StatusDebet", MatchType.Exact, 0))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "TransactionDate", MatchType.GreaterOrEqual, dtStart))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDetail), "TransactionDate", MatchType.Lesser, dtEnd))

                aggregateSum = New Aggregate(GetType(KTB.DNet.Domain.DepositBDetail), "Amount", AggregateType.Sum)

                Dim periodKredit As Decimal = IsDBNull(New DepositBDetailFacade(User).RetrieveScalar(criterias, aggregateSum), 0)

                cell = New TableCell
                cell.ForeColor = Color.White
                cell.HorizontalAlign = HorizontalAlign.Right
                cell.Font.Bold = True
                cell.Text = periodKredit.ToString("#,###")
                tblRow.Cells.Add(cell)

                'e.Item.Controls.AddAt(0, New LiteralControl("</td><td colspan=""6""></td></tr style=""display:none"">"))
                e.Item.Controls.AddAt(0, tblRow)
                e.Item.Controls.AddAt(1, New LiteralControl(String.Format("<tr id=""tr{0}"" style=""display:none"">", currentPeriod)))
            Else
                e.Item.Controls.AddAt(0, New LiteralControl(String.Format("<tr id=""tr{0}"" style=""display:none"">", currentPeriod)))
            End If
            e.Item.Controls.Add(New LiteralControl("</tr>"))

            If (oDepositBDetails.StatusDebet = 0) Then
                Dim lblKredit As Label = CType(e.Item.FindControl("lblKredit"), Label)
                lblKredit.Text = oDepositBDetails.Amount.ToString("#,###")
                TotalKredit += oDepositBDetails.Amount
            Else
                Dim lblDebet As Label = CType(e.Item.FindControl("lblDebet"), Label)
                lblDebet.Text = oDepositBDetails.Amount.ToString("#,###")
                TotalDebet += oDepositBDetails.Amount
            End If

            'If e.Item.ItemIndex = (CType(dtlDetails.DataSource, ArrayList).Count - 1) Then
            '    e.Item.Controls.Add(New LiteralControl(String.Format("</td></tr></div><tr style=""display:none""><td>", currentPeriod)))
            'End If
        End If
        If (e.Item.ItemType = ListItemType.Footer) Then
            Dim lblTotalDebetDetails As Label = CType(e.Item.FindControl("lblTotalDebetDetails"), Label)
            Dim lblTotalKreditDetails As Label = CType(e.Item.FindControl("lblTotalKreditDetails"), Label)
            lblTotalDebetDetails.Text = IIf(TotalDebet = 0, 0, TotalDebet.ToString("#,###"))
            lblTotalKreditDetails.Text = IIf(TotalKredit = 0, 0, TotalKredit.ToString("#,###"))
            TotalDebetAll += TotalDebet
            TotalKreditAll += TotalKredit
            TotalKredit = 0
            TotalDebet = 0
            i = 0
            Tipe = String.Empty
        End If
    End Sub


    Private Function IsDBNull(ByVal obj As Object, ByVal replacement As Decimal) As Decimal
        If obj Is DBNull.Value Then
            Return replacement
        End If
        Return CType(obj, Decimal)
    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ToggleDisplay(False)
    End Sub
End Class