Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Linq
#End Region


Public Class FrmDepositA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents icTglPengajuanFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents pnlSearch As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlDetails As System.Web.UI.WebControls.Panel
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotalDebetAll As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalKreditAll As System.Web.UI.WebControls.Label
    Protected WithEvents icPeriodeFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPeriodeTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dtlDepositA As System.Web.UI.WebControls.DataList
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents dtlDetails As System.Web.UI.WebControls.Repeater
    Protected WithEvents BtnDownloadDtl As System.Web.UI.WebControls.Button
    Protected WithEvents BtnDownloadAllDetail As System.Web.UI.WebControls.Button
    Protected WithEvents BtnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents BtnDownloadHeader As System.Web.UI.WebControls.Button
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblDealerDetail As System.Web.UI.WebControls.Label
    Protected WithEvents lblProdukDetail As System.Web.UI.WebControls.Label

    '    Protected WithEvents Button1 As System.Web.UI.WebControls.Button


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"

    Private arlDepositA As ArrayList = New ArrayList
    Private arlDepositAFilter As ArrayList = New ArrayList
    Private arl As ArrayList = New ArrayList
    Private arlDepositDetails As ArrayList = New ArrayList

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

    Dim sHelper As New SessionHelper

#End Region

#Region "Custom Method"

    Private Sub AddPeriodCriteria(ByVal criterias As CriteriaComposite, ByVal ColumnName As String)
        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                    icPeriodeFrom.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                    icPeriodeTo.Value.Day, 0, 0, 0)
        dtEnd = dtEnd.AddDays(1)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), ColumnName, MatchType.GreaterOrEqual, dtStart))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), ColumnName, MatchType.Lesser, dtEnd))
    End Sub

    Private Sub GetAllTotal(ByVal DealerCode As String, ByRef TotalSaldo As Long, ByRef TotalDebet As Long, ByRef TotalKredit As Long, ByRef TotalSaldoAkhir As Long)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        AddPeriodCriteria(criterias, "TransactionDate")
        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositA), "TransactionDate", Sort.SortDirection.DESC))

        arl = New FinishUnit.DepositAFacade(User).Retrieve(criterias, sortColl)
        If arl.Count > 0 Then
            For Each item As DepositA In arl
                TotalDebet += item.DebetAmount
                TotalKredit += item.CreditAmount
            Next
            TotalSaldo = arl(arl.Count - 1).BeginingBalance
            TotalSaldoAkhir = arl(0).EndBalance
        End If
    End Sub

    Private Sub GetAllTotal(ByVal DealerCode As String, ByVal ProductCategoryCode As String, ByRef TotalSaldo As Long, ByRef TotalDebet As Long, ByRef TotalKredit As Long, ByRef TotalSaldoAkhir As Long)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "ProductCategory.Code", MatchType.Exact, ProductCategoryCode))
        AddPeriodCriteria(criterias, "TransactionDate")
        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositA), "TransactionDate", Sort.SortDirection.DESC))

        arl = New FinishUnit.DepositAFacade(User).Retrieve(criterias, sortColl)
        If arl.Count > 0 Then
            For Each item As DepositA In arl
                TotalDebet += item.DebetAmount
                TotalKredit += item.CreditAmount
            Next
            TotalSaldo = arl(arl.Count - 1).BeginingBalance
            TotalSaldoAkhir = arl(0).EndBalance
        End If
    End Sub

    Private Function IsExist(ByVal DealerCode As String, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As DepositA In arl
            If item.Dealer.DealerCode.Trim().ToUpper() = DealerCode.Trim().ToUpper() Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Private Function IsExist(ByVal DealerCode As String, ByVal ProductCategoryCode As String, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As DepositA In arl
            If item.Dealer.DealerCode.Trim().ToUpper() = DealerCode.Trim().ToUpper() AndAlso item.ProductCategory.Code = ProductCategoryCode Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function


    Sub BindDepositA()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtKodeDealer.Text.Trim() <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
            End If
        Else
            If (txtKodeDealer.Text.Trim() <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
            Else
                MessageBox.Show("Tentukan dealer terlebih dahulu")
                Exit Sub
            End If
        End If


        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        End If
        AddPeriodCriteria(criterias, "TransactionDate")

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositA), ViewState("currSortColumn"), ViewState("currSortDirection")))
            If ViewState("currSortColumn").ToString().Contains("DealerCode") Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositA), "ProductCategory.Code", ViewState("currSortDirection")))
            End If
        Else
            sortColl = Nothing
        End If

        arlDepositA = New FinishUnit.DepositAFacade(User).Retrieve(criterias, sortColl)
        If arlDepositA.Count > 0 Then
            sHelper.SetSession("AllHeaderToDownload", arlDepositA)
        End If

        Dim DealerCode As String = String.Empty
        For Each item As DepositA In arlDepositA
            If (Not IsExist(item.Dealer.DealerCode, item.ProductCategory.Code, arlDepositAFilter)) Then
                arlDepositAFilter.Add(item)
            End If
        Next

        If (arlDepositAFilter.Count > 0) Then
            dtlDepositA.Visible = True
            dtlDepositA.DataSource = arlDepositAFilter
            dtlDepositA.DataBind()

        Else
            dtlDepositA.Visible = False

            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Sub GetDataAllDetails()
        'Dim arrHeaderToDownload As ArrayList = sHelper.GetSession("AllHeaderToDownload")
        'If IsNothing(arrHeaderToDownload) Then
        '    Exit Sub
        'End If
        'If arrHeaderToDownload.Count > 0 Then
        '    Dim strDepositAIDs As String = String.Empty
        '    For Each depositAItem As DepositA In arrHeaderToDownload
        '        If strDepositAIDs.Length > 0 Then
        '            strDepositAIDs += ","
        '        End If
        '        strDepositAIDs += depositAItem.ID.ToString()
        '    Next

        '    strDepositAIDs = "(" + strDepositAIDs + ")"

        'End If

        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                            icPeriodeFrom.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                    icPeriodeTo.Value.Day, 0, 0, 0)
        dtEnd = dtEnd.AddDays(1)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "DepositA.ID", MatchType.InSet, strDepositAIDs))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "TransactionDate", MatchType.GreaterOrEqual, dtStart))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "TransactionDate", MatchType.Lesser, dtEnd))

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositADetail), "DepositA.Dealer.DealerCode", Sort.SortDirection.ASC))
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositADetail), "TransactionDate", Sort.SortDirection.ASC))
        Dim arlAllDepositDetails As ArrayList = New FinishUnit.DepositADetailFacade(User).Retrieve(criterias, sortColl)

        sHelper.SetSession("AllDetailsToDownload", arlAllDepositDetails)


    End Sub

    Private _selectedDepositAIDs As String
    Sub BindDepositAForDetails(ByVal DealerCode As String, ByVal ProductCode As String)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        If ProductCode <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "ProductCategory.Code", MatchType.Exact, ProductCode))
        End If


        AddPeriodCriteria(criterias, "TransactionDate")

        arlDepositA = New FinishUnit.DepositAFacade(User).Retrieve(criterias)

        lblPeriode.Text = icPeriodeFrom.Value.ToString("MMMM") + " " + icPeriodeFrom.Value.ToString("yyyy") + " - " + icPeriodeTo.Value.ToString("MMMM") + " " + icPeriodeTo.Value.ToString("yyyy")
        Dim strDepositAIDs As String = String.Empty
        For Each depositAItem As DepositA In arlDepositA
            If strDepositAIDs.Length > 0 Then
                strDepositAIDs += ","
            End If
            strDepositAIDs += depositAItem.ID.ToString()
        Next

        ''Deposit A CR
        lblDealerDetail.Text = DealerCode
        lblProdukDetail.Text = ProductCode

        strDepositAIDs = "(" + strDepositAIDs + ")"
        _selectedDepositAIDs = strDepositAIDs
        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "DepositA.ID", MatchType.InSet, strDepositAIDs))

        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositADetail), "TransactionDate", Sort.SortDirection.ASC))
        Dim arlDepositDetails As ArrayList = New FinishUnit.DepositADetailFacade(User).Retrieve(criterias, sortColl)

        dtlDetails.DataSource = arlDepositDetails
        ' Modified by Ikhsan, 20081211
        ' Requested by Rina, as Part of CR
        ' Download for detail Page of Deposit A
        ' Start -----
        sHelper.SetSession("DetailsToDownload", arlDepositDetails)
        ' end -------
        dtlDetails.DataBind()
    End Sub

    Sub BindDepositADetails(ByVal ID As Integer, ByVal dg As DataGrid)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "DepositA.ID", MatchType.Exact, ID))

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositADetail), "StatusDebet", Sort.SortDirection.ASC))
        arlDepositDetails = New FinishUnit.DepositADetailFacade(User).Retrieve(criterias, sortColl)
        AddHandler dg.ItemDataBound, AddressOf ItemDataBound
        dg.DataSource = arlDepositDetails
        dg.DataBind()
    End Sub


    Private Sub ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim oDepositADetails As DepositADetail = CType(e.Item.DataItem, DepositADetail)
            Dim lblTransactionDate As Label = CType(e.Item.FindControl("lblTransactionDate"), Label)
            If (i = 0) Then
                lblTransactionDate.Text = oDepositADetails.TransactionDate.ToString("dd/MM/yyyy")
            End If
            If (oDepositADetails.StatusDebet = 0) Then
                If (Tipe <> "Penerimaan") Then
                    e.Item.Cells(1).Text = "Penerimaan"
                    Tipe = "Penerimaan"
                End If
                e.Item.Cells(7).Text = oDepositADetails.Amount.ToString("#,###")
                TotalKredit += oDepositADetails.Amount
            Else
                If (Tipe <> "Pengeluaran") Then
                    e.Item.Cells(1).Text = "Pengeluaran"
                    Tipe = "Pengeluaran"
                End If
                e.Item.Cells(6).Text = oDepositADetails.Amount.ToString("#,###")
                TotalDebet += oDepositADetails.Amount
            End If
            i = i + 1
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

    Sub ToggleDisplay(ByVal Bool As Boolean)
        pnlSearch.Visible = Not Bool
        pnlDetails.Visible = Bool
    End Sub

    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function


#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        'mod by Ery
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = 1 Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Else
            'lblSearchDealer.Visible = False
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            'txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
            '  txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        End If


        Dim val As Object = Request.Form("__EVENTTARGET")
        If (val = Nothing) Then
            val = String.Empty
        End If
        Dim clientID As String = val.ToString()
        If clientID = "hypKodeDealer" Then
            Dim sortExpression As String = Request.Form("__EVENTARGUMENT")
            If CType(ViewState("currSortColumn"), String) = sortExpression Then
                Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                    Case Sort.SortDirection.ASC
                        ViewState("currSortDirection") = Sort.SortDirection.DESC

                    Case Sort.SortDirection.DESC
                        ViewState("currSortDirection") = Sort.SortDirection.ASC
                End Select
            Else
                ViewState("currSortColumn") = sortExpression
                ViewState("currSortDirection") = Sort.SortDirection.ASC
            End If
            Dim ArrDummy As ArrayList = New ArrayList
            Dim ArrDummyheader As ArrayList = New ArrayList
            sHelper.SetSession("DataToDownload", ArrDummy)
            sHelper.SetSession("HeaderDataToDownload", ArrDummyheader)


            Dim diff As Integer = DateDiff(DateInterval.Month, icPeriodeFrom.Value, icPeriodeTo.Value)
            If diff <= 12 Then
                BindDepositA()
            Else
                MessageBox.Show("Periode melebihi 12 bulan.")
            End If

            'BindDepositA()
        End If

        If Not IsPostBack Then
            ViewState("currSortColumn") = "Dealer.DealerCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim ArrDummy As ArrayList = New ArrayList
        Dim arDummyHeader As ArrayList = New ArrayList
        sHelper.SetSession("DataToDownload", ArrDummy)
        sHelper.SetSession("HeaderDataToDownload", arDummyHeader)


        Dim diff As Integer = DateDiff(DateInterval.Month, icPeriodeFrom.Value, icPeriodeTo.Value)
        If diff <= 12 Then
            BindDepositA()
        Else
            MessageBox.Show("Periode melebihi 12 bulan.")
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        ToggleDisplay(False)
        ' BindDepositA()
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.DepositAView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Deposit A")
        End If
    End Sub
#End Region

    Private Function IsDBNull(ByVal obj As Object, ByVal replacement As Decimal) As Decimal
        If obj Is DBNull.Value Then
            Return replacement
        End If
        Return CType(obj, Decimal)
    End Function

    Private Sub dtlDepositA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtlDepositA.ItemDataBound
        Dim TotalSaldoAwal As Long = 0
        Dim TotalDebet As Long = 0
        Dim TotalKredit As Long = 0
        Dim TotalSaldoAkhir As Long = 0

        If e.Item.ItemType = ListItemType.Header Then
            Dim hypKodeDealer As HyperLink = CType(e.Item.FindControl("hypKodeDealer"), HyperLink)
            Dim lc As New LiteralControl
            lc.ID = "hypKodeDealer"

            hypKodeDealer.NavigateUrl = Page.GetPostBackClientHyperlink(lc, "Dealer.DealerCode")
            'hypKodeDealer.Attributes.Add("onclick", Page.GetPostBackClientHyperlink(lc, "Dealer.DealerCode"))
        End If

        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim oD As DepositA = CType(e.Item.DataItem, DepositA)


            Dim lblSaldoAwal As Label = CType(e.Item.FindControl("lblSaldoAwal"), Label)
            Dim lblDebet As Label = CType(e.Item.FindControl("lblDebet"), Label)
            Dim lblKredit As Label = CType(e.Item.FindControl("lblKredit"), Label)
            Dim lblSaldoAkhir As Label = CType(e.Item.FindControl("lblSaldoAkhir"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            'CR Split Dep A
            Dim lblProductCategoryHeader As Label = CType(e.Item.FindControl("lblProductCategoryHeader"), Label)
            GetAllTotal(lblDealerCode.Text, lblProductCategoryHeader.Text, TotalSaldoAwal, TotalDebet, TotalKredit, TotalSaldoAkhir)
            ' GetAllTotal(lblDealerCode.Text, lblProductCategoryHeader.Text, TotalSaldoAwal, TotalDebet, TotalKredit, TotalSaldoAkhir)

            lblSaldoAwal.Text = IIf(TotalSaldoAwal = 0, 0, TotalSaldoAwal.ToString("#,###"))
            lblDebet.Text = IIf(TotalDebet = 0, 0, TotalDebet.ToString("#,###"))
            lblKredit.Text = IIf(TotalKredit = 0, 0, TotalKredit.ToString("#,###"))
            lblSaldoAkhir.Text = IIf(TotalSaldoAkhir = 0, 0, TotalSaldoAkhir.ToString("#,###"))
            TotSA += TotalSaldoAwal
            TotD += TotalDebet
            TotK += TotalKredit
            TotSAkh += TotalSaldoAkhir

            Dim arHeader As New ArrayList

            Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                        DateTime.DaysInMonth(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month), _
                                        0, 0, 0)
            Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                        DateTime.DaysInMonth(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month), _
                                        0, 0, 0)
            ' While DateTime.op_LessThanOrEqual(dtStart, dtEnd)
            Dim depo As New DepositATemp
            depo.DealerCode = oD.Dealer.DealerCode
            depo.DealerName = oD.Dealer.DealerName
            depo.PoductCatecoryCode = lblProductCategoryHeader.Text
            depo.SaldoAwal = TotalSaldoAwal
            depo.Debet = TotalDebet
            depo.Kredit = TotalKredit
            depo.SaldoAkhir = TotalSaldoAkhir
            arHeader.Add(depo)
            '  dtStart = dtStart.AddMonths(1)
            ' End While

            Dim arrToDownloadHeader As ArrayList = sHelper.GetSession("HeaderDataToDownload")
            If arrToDownloadHeader Is Nothing Then
                arrToDownloadHeader = New ArrayList
            End If

            arrToDownloadHeader.AddRange(arHeader)
            sHelper.SetSession("HeaderDataToDownload", arrToDownloadHeader)

            Dim dtgDetail As DataGrid = CType(e.Item.FindControl("dtgDetail"), DataGrid)

            Dim arlDetails As New ArrayList
            'Dim ArlTemp As New ArrayList
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
                criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.Exact, oD.Dealer.DealerCode))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "TransactionDate", MatchType.GreaterOrEqual, dtPeriodFrom))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "TransactionDate", MatchType.Lesser, dtPeriodEnd))
                'CR Split Dep A
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "ProductCategory.Code", MatchType.Exact, lblProductCategoryHeader.Text))
                Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositA), "TransactionDate", Sort.SortDirection.DESC))

                'aggregateSum = New Aggregate(GetType(KTB.DNet.Domain.DepositA), "BeginingBalance", AggregateType.Sum)
                'val = New FinishUnit.DepositAFacade(User).RetrieveScalar(criterias, aggregateSum)
                Dim listDepositA As ArrayList = New FinishUnit.DepositAFacade(User).Retrieve(criterias, sortColl)
                Dim periodDebet As Decimal = 0
                Dim saldoAwal As Decimal = 0
                Dim saldoAkhir As Decimal = 0
                Dim periodKredit As Decimal = 0


                If listDepositA.Count > 0 Then
                    For Each item As DepositA In listDepositA
                        periodDebet += item.DebetAmount
                        periodKredit += item.CreditAmount
                    Next
                    saldoAwal = listDepositA(listDepositA.Count - 1).BeginingBalance
                    saldoAkhir = listDepositA(0).EndBalance
                End If


                If (saldoAwal = 0 And saldoAkhir = 0 And periodDebet = 0 And periodKredit = 0) Then
                    isDataExists = False
                End If
                'If Not isDataExists Then
                '    isDataExists = Not (val.Equals(DBNull.Value))
                'End If
                'Dim saldoAwal As Decimal = IsDBNull(val, 0)

                'Debet
                'criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.Exact, oD.Dealer.DealerCode))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "TransactionDate", MatchType.GreaterOrEqual, dtPeriodFrom))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "TransactionDate", MatchType.Lesser, dtPeriodEnd))

                'aggregateSum = New Aggregate(GetType(KTB.DNet.Domain.DepositA), "DebetAmount", AggregateType.Sum)

                'val = New FinishUnit.DepositAFacade(User).RetrieveScalar(criterias, aggregateSum)
                'If Not isDataExists Then
                '    isDataExists = Not (val.Equals(DBNull.Value))
                'End If
                'Dim periodDebet As Decimal = IsDBNull(val, 0)

                ''Kredit
                'criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.Exact, oD.Dealer.DealerCode))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "TransactionDate", MatchType.GreaterOrEqual, dtPeriodFrom))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "TransactionDate", MatchType.Lesser, dtPeriodEnd))

                'aggregateSum = New Aggregate(GetType(KTB.DNet.Domain.DepositA), "CreditAmount", AggregateType.Sum)

                'val = New FinishUnit.DepositAFacade(User).RetrieveScalar(criterias, aggregateSum)
                'If Not isDataExists Then
                '    isDataExists = Not (val.Equals(DBNull.Value))
                'End If
                'Dim periodKredit As Decimal = IsDBNull(val, 0)

                ''Saldo Akhir
                'criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.Exact, oD.Dealer.DealerCode))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "TransactionDate", MatchType.GreaterOrEqual, dtPeriodFrom))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "TransactionDate", MatchType.Lesser, dtPeriodEnd))

                'aggregateSum = New Aggregate(GetType(KTB.DNet.Domain.DepositA), "EndBalance", AggregateType.Sum)

                'val = New FinishUnit.DepositAFacade(User).RetrieveScalar(criterias, aggregateSum)
                'If Not isDataExists Then
                '    isDataExists = Not (val.Equals(DBNull.Value))
                'End If
                'Dim saldoAkhir As Decimal = IsDBNull(val, 0)


                If isDataExists Then
                    Dim saldoPerPeriod As New DepositAPerPeriode
                    ' Modified by Ikhsan, 20081209
                    ' Requested by Rina as Part of CR
                    ' Start ----

                    saldoPerPeriod.DealerCode = oD.Dealer.DealerCode
                    saldoPerPeriod.DealerName = oD.Dealer.DealerName

                    ' End ------


                    saldoPerPeriod.Periode = dtStart.ToString("MMMM")
                    saldoPerPeriod.SaldoAwal = saldoAwal
                    saldoPerPeriod.Debet = periodDebet
                    saldoPerPeriod.Kredit = periodKredit
                    saldoPerPeriod.SaldoAkhir = saldoAkhir
                    saldoPerPeriod.PoductCatecoryCode = lblProductCategoryHeader.Text
                    saldoPerPeriod.EnuPeriode = dtStart.ToString("yyyyMM")

                    arlDetails.Add(saldoPerPeriod)
                    'ArlTemp.Add(saldoPerPeriod)
                End If

                dtStart = dtStart.AddMonths(1)
            End While
            ' Start ----
            Dim arrToDownload As ArrayList = sHelper.GetSession("DataToDownload")
            'ArlTemp.AddRange(arrToDownload)
            arrToDownload.AddRange(arlDetails)
            sHelper.SetSession("DataToDownload", arrToDownload)
            ' End ------
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
            lblTotalSaldoAkhir.Text = IIf(TotSAkh = 0, 0, TotSAkh.ToString("#,###"))
        End If
    End Sub

    Private Sub dtlDepositA_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dtlDepositA.ItemCommand
        If (e.CommandName = "detail") Then
            ToggleDisplay(True)
            Dim StrProcuctCode As String = CType(e.Item.FindControl("lblProductCategoryHeader"), Label).Text
            BindDepositAForDetails(e.CommandArgument, StrProcuctCode)
            lblTotalDebetAll.Text = "Rp " & CType(e.Item.FindControl("lblDebet"), Label).Text
            lblTotalKreditAll.Text = "Rp " & CType(e.Item.FindControl("lblKredit"), Label).Text
        End If
    End Sub

    Private Class DepositAPerPeriode
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

    Private _lastTransactionDate As String = String.Empty
    Private _lastPeriode As String = String.Empty
    Private _lastDate As String = String.Empty
    Private Sub dtlDetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles dtlDetails.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim oDepositADetails As DepositADetail = CType(e.Item.DataItem, DepositADetail)

            Dim currentPeriod As String = oDepositADetails.TransactionDate.ToString("MMMM")
            Dim currentDate As String = oDepositADetails.TransactionDate.ToString("dd/MM/yyyy")
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

                Dim dtStart As DateTime = New DateTime(oDepositADetails.TransactionDate.Year, oDepositADetails.TransactionDate.Month, _
                                                1, 0, 0, 0)
                Dim dtEnd As DateTime = New DateTime(oDepositADetails.TransactionDate.Year, oDepositADetails.TransactionDate.Month, _
                                                DateTime.DaysInMonth(oDepositADetails.TransactionDate.Year, oDepositADetails.TransactionDate.Month), 0, 0, 0)
                dtEnd = dtEnd.AddDays(1)
                'Debet
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "DepositA.ID", MatchType.InSet, _selectedDepositAIDs))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "DepositA", MatchType.Exact, oDepositADetails.DepositA.ID))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "StatusDebet", MatchType.Exact, 1))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "TransactionDate", MatchType.GreaterOrEqual, dtStart))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "TransactionDate", MatchType.Lesser, dtEnd))

                Dim aggregateSum As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.DepositADetail), "Amount", AggregateType.Sum)

                Dim periodDebet As Decimal = IsDBNull(New FinishUnit.DepositADetailFacade(User).RetrieveScalar(criterias, aggregateSum), 0)

                cell = New TableCell
                cell.ForeColor = Color.White
                cell.HorizontalAlign = HorizontalAlign.Right
                cell.Font.Bold = True
                cell.Text = periodDebet.ToString("#,###")
                tblRow.Cells.Add(cell)

                'Kredit
                criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "DepositA", MatchType.Exact, oDepositADetails.DepositA.ID))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "DepositA.ID", MatchType.InSet, _selectedDepositAIDs))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "StatusDebet", MatchType.Exact, 0))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "TransactionDate", MatchType.GreaterOrEqual, dtStart))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "TransactionDate", MatchType.Lesser, dtEnd))

                aggregateSum = New Aggregate(GetType(KTB.DNet.Domain.DepositADetail), "Amount", AggregateType.Sum)

                Dim periodKredit As Decimal = IsDBNull(New FinishUnit.DepositADetailFacade(User).RetrieveScalar(criterias, aggregateSum), 0)

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

            If (oDepositADetails.StatusDebet = 0) Then
                Dim lblKredit As Label = CType(e.Item.FindControl("lblKredit"), Label)
                lblKredit.Text = oDepositADetails.Amount.ToString("#,###")
                TotalKredit += oDepositADetails.Amount
            Else
                Dim lblDebet As Label = CType(e.Item.FindControl("lblDebet"), Label)
                lblDebet.Text = oDepositADetails.Amount.ToString("#,###")
                TotalDebet += oDepositADetails.Amount
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

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDownload.Click
        Dim data As ArrayList = CType(sHelper.GetSession("DataToDownload"), ArrayList)
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub

    Private Sub btnDownloadHeader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadHeader.Click
        Dim data As ArrayList = CType(sHelper.GetSession("HeaderDataToDownload"), ArrayList)
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data, False, True)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList, Optional ByVal isDetail As Boolean = False, Optional ByVal isHeader As Boolean = False)
        Dim sFileName As String = "DepositA"
        If isHeader = True Then
            sFileName = "DepositAHeader"
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
                    WriteAllDepositADetail(sw, data)
                ElseIf isDetail = False And isHeader = False Then
                    WriteDepositAData(sw, data)
                End If

                If isHeader = True And isDetail = False Then
                    WriteDepositADataHeader(sw, data)
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

    Private Sub WriteDepositAData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder



        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Umum - Daftar Deposit A")
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

            Dim LDepA = From ObjDepositAPerPeriode As DepositAPerPeriode In data
                        Select ObjDepositAPerPeriode
                        Order By ObjDepositAPerPeriode.DealerCode Ascending, ObjDepositAPerPeriode.PoductCatecoryCode Ascending, ObjDepositAPerPeriode.EnuPeriode Ascending
            '' CR SPlit Dep A
            'For Each item As DepositAPerPeriode In data
            For Each item As DepositAPerPeriode In LDepA
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

    Private Sub WriteDepositADataHeader(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder



        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Umum - Daftar Deposit A Header")
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

            Dim LDepA = From ObjDepositAPerPeriode As DepositATemp In data
                        Select ObjDepositAPerPeriode
                        Order By ObjDepositAPerPeriode.DealerCode Ascending, ObjDepositAPerPeriode.PoductCatecoryCode Ascending
            '' CR SPlit Dep A
            'For Each item As DepositAPerPeriode In data
            For Each item As DepositATemp In LDepA
                itemLine.Remove(0, itemLine.Length)

                If DealerCode <> item.DealerCode.ToString OrElse ProductCategoryCode <> item.PoductCatecoryCode Then
                    'If ProductCategoryCode <> item.PoductCatecoryCode Then
                    itemLine.Append(item.DealerCode & tab)
                    itemLine.Append(item.PoductCatecoryCode & tab)
                    itemLine.Append(item.DealerName & tab)
                    itemLine.Append(Val(item.SaldoAwal).ToString & tab)
                    itemLine.Append(Val(item.Debet).ToString & tab)
                    itemLine.Append(Val(item.Kredit).ToString & tab)
                    itemLine.Append(Val(item.SaldoAkhir).ToString & tab)
                    'End If


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

    Private Sub BtnDownloadDtl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDownloadDtl.Click
        Dim data As ArrayList = CType(sHelper.GetSession("DetailsToDownload"), ArrayList)

        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownloadDetail(data)
        End If
    End Sub


    Private Sub DoDownloadDetail(ByVal data As ArrayList, Optional ByVal isAll As Boolean = False)
        Dim sFileName As String
        sFileName = "DepositADetail" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

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
                    WriteAllDepositADetail(sw, data)
                Else
                    WriteDepositADetail(sw, data)
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

    Private Sub WriteDepositADetail(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Umum - Daftar Deposit A Detail")
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

            For Each item As DepositADetail In data
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

    Private Sub WriteAllDepositADetail(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Umum - Daftar Deposit A Detail")
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

            'Dim i As Integer = 1
            'Dim DealerCode As String = ""
            For Each header As DepositA In data
                For Each item As DepositADetail In header.DepositADetails
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

    Private Sub BtnDownloadAllDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDownloadAllDetail.Click
        'GetDataAllDetails()
        'Dim data As ArrayList = CType(sHelper.GetSession("AllDetailsToDownload"), ArrayList)
        Dim data As ArrayList = CType(sHelper.GetSession("AllHeaderToDownload"), ArrayList)
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownloadDetail(data, True)
        End If
    End Sub



    Private Class DepositATemp

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
End Class
