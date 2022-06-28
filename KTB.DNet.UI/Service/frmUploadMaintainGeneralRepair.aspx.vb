Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports OfficeOpenXml
Imports System.Security.Principal
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Linq
Imports System.Globalization

#End Region


Public Class frmUploadMaintainGeneralRepair
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
   
    Protected WithEvents icTglPengajuanFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    'Protected WithEvents pnlSearch As System.Web.UI.WebControls.Panel
    'Protected WithEvents pnlDetails As System.Web.UI.WebControls.Panel
    'Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    'Protected WithEvents lblTotalDebetAll As System.Web.UI.WebControls.Label
    'Protected WithEvents lblTotalKreditAll As System.Web.UI.WebControls.Label
    Protected WithEvents icPeriodeFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPeriodeTo As KTB.DNet.WebCC.IntiCalendar
    'Protected WithEvents dtlDepositA As System.Web.UI.WebControls.DataList
    'Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    'Protected WithEvents dtlDetails As System.Web.UI.WebControls.Repeater
    'Protected WithEvents BtnDownloadDtl As System.Web.UI.WebControls.Button
    Protected WithEvents BtnDownloadAllDetail As System.Web.UI.WebControls.Button
    Protected WithEvents BtnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents BtnDownloadHeader As System.Web.UI.WebControls.Button
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgDetail As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents lblDealerDetail As System.Web.UI.WebControls.Label
    'Protected WithEvents lblProdukDetail As System.Web.UI.WebControls.Label

    '    Protected WithEvents Button1 As System.Web.UI.WebControls.Button


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private sessHelper As SessionHelper = New SessionHelper

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.

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
    'Private sessHelper As SessionHelper = New SessionHelper
    Dim currentPage As Integer

#End Region

#Region "Custom Method"

    Private Sub AddPeriodCriteria(ByVal criterias As CriteriaComposite, ByVal ColumnName As String)
        Dim dtStart As DateTime = New DateTime(2020, 12, 3, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(2021, 12, 3, 0, 0, 0)
        dtEnd = dtEnd.AddDays(1)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), ColumnName, MatchType.GreaterOrEqual, dtStart))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), ColumnName, MatchType.Lesser, dtEnd))
    End Sub

    Private Sub GetAllTotal(ByVal DealerCode As String, ByRef TotalSaldo As Long, ByRef TotalDebet As Long, ByRef TotalKredit As Long, ByRef TotalSaldoAkhir As Long)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        AddPeriodCriteria(criterias, "TransactionDate")
        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositA), "TransactionDate", Sort.SortDirection.DESC))

        'arl = New FinishUnit.DepositAFacade(User).Retrieve(criterias, sortColl)
        'If arl.Count > 0 Then
        '    For Each item As DepositA In arl
        '        TotalDebet += item.DebetAmount
        '        TotalKredit += item.CreditAmount
        '    Next
        '    TotalSaldo = arl(arl.Count - 1).BeginingBalance
        '    TotalSaldoAkhir = arl(0).EndBalance
        'End If
    End Sub

    Private Sub GetAllTotal(ByVal DealerCode As String, ByVal ProductCategoryCode As String, ByRef TotalSaldo As Long, ByRef TotalDebet As Long, ByRef TotalKredit As Long, ByRef TotalSaldoAkhir As Long)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "ProductCategory.Code", MatchType.Exact, ProductCategoryCode))
        AddPeriodCriteria(criterias, "TransactionDate")
        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositA), "TransactionDate", Sort.SortDirection.DESC))

        'arl = New FinishUnit.DepositAFacade(User).Retrieve(criterias, sortColl)
        'If arl.Count > 0 Then
        '    For Each item As DepositA In arl
        '        TotalDebet += item.DebetAmount
        '        TotalKredit += item.CreditAmount
        '    Next
        '    TotalSaldo = arl(arl.Count - 1).BeginingBalance
        '    TotalSaldoAkhir = arl(0).EndBalance
        'End If
    End Sub

    Private Sub ReadData()
       

        Dim dtServiceTemplate As DataSet = New ServiceTemplateGRLaborFacade(User).RetieveListOfTemplateService()
        Dim dtGRLabor As DataTable = dtServiceTemplate.Tables(0)
        Dim dtGRLaborDetail As DataTable = dtServiceTemplate.Tables(1)

        '-- Store InvoiceReqList into session for later use
        sessHelper.SetSession("ServiceTemplateGRLabor", dtGRLabor)
        sessHelper.SetSession("ServiceTemplateGRLaborDetail", dtGRLaborDetail)

    End Sub

    Private Sub SetDownload()
        
        ' mengambil data yang dibutuhkan
        Dim arrServiceTemplateGRLabor As DataTable = CType(sessHelper.GetSession("ServiceTemplateGRLabor"), DataTable)
        Dim arrServiceTemplateGRLaborDetail As DataTable = CType(sessHelper.GetSession("ServiceTemplateGRLaborDetail"), DataTable)
        'Dim propertiesinfo As PropertyInfo() = arrServiceBooking(0).GetType().GetProperties()

        

        If arrServiceTemplateGRLabor.Rows.Count > 0 Then
            CreateExcel("ServiceTemplateGRLabor", arrServiceTemplateGRLabor, arrServiceTemplateGRLaborDetail)
        End If
    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal DataGRLabor As DataTable, ByVal DataGRLaborDetail As DataTable)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, "Sheet1")
            Dim ws1 As ExcelWorksheet = CreateSheet(pck, "Sheet2")

            'Sheet1
            ws.Cells("A1").Value = "No"
            Dim columnSheet1 As Integer = 1
            For Each column As DataColumn In DataGRLabor.Columns
                columnSheet1 += 1
                ws.Cells(1, columnSheet1).Value = column.ColumnName
            Next

            'ws.Cells("B1").Value = "Variant"
            'ws.Cells("C1").Value = "Jenis Service"
            'ws.Cells("D1").Value = "Kind Code"
            'ws.Cells("E1").Value = "Jasa Service"
            'ws.Cells("F1").Value = "Durasi Service"
            'ws.Cells("G1").Value = "Mulai Berlaku"
            'ws.Cells("G2").Style.Numberformat.Format = "yyyy-mm-dd"
            'ws.Cells("H1").Value = "Lc/hour"

            'Sheet2
            ws1.Cells("A1").Value = "No"
            Dim columnSheet2 As Integer = 1
            For Each column As DataColumn In DataGRLaborDetail.Columns
                columnSheet2 += 1
                ws1.Cells(1, columnSheet2).Value = column.ColumnName
            Next
            'ws1.Cells("B1").Value = "Variant"
            'ws1.Cells("C1").Value = "Jenis Service"
            'ws1.Cells("D1").Value = "Kind Code"
            'ws1.Cells("E1").Value = "Nama Sparepart"
            'ws1.Cells("F1").Value = "Kode Sparepart"
            'ws1.Cells("G1").Value = "Harga Satuan"
            'ws1.Cells("H1").Value = "Jumlah"

            'Dim standardCodeStatusClaimList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusClaim").Cast(Of  _
            '                                    StandardCode).ToList()
            'Dim standardCodeStatusProsesReturList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusProsesRetur").Cast(Of  _
            'StandardCode).ToList()
            'Dim strJenisServis As String = ""
            'Dim strAssist As String = ""
            'Dim strJenisKegiatan As String = ""
            Dim rowSheet1 As Integer = 2
            Dim rowSheet2 As Integer = 2

            For Each dRow As DataRow In DataGRLabor.Rows
                columnSheet1 = 1
                ws.Cells(rowSheet1, columnSheet1).SetValue((rowSheet1 - 1).ToString())
                For Each dColumn As DataColumn In DataGRLabor.Columns
                    columnSheet1 += 1
                    ws.Cells(rowSheet1, columnSheet1).SetValue(dRow(dColumn.ColumnName).ToString())
                Next
                rowSheet1 += 1
            Next


            For Each dRow As DataRow In DataGRLaborDetail.Rows
                columnSheet2 = 1
                ws1.Cells(rowSheet2, columnSheet2).SetValue((rowSheet2 - 1).ToString())
                For Each dColumn As DataColumn In DataGRLaborDetail.Columns
                    columnSheet2 += 1
                    ws1.Cells(rowSheet2, columnSheet2).SetValue(dRow(dColumn.ColumnName).ToString())
                Next
                rowSheet2 += 1
            Next
            

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xlsx")
        End Using

    End Sub

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=""{0}"";", fileName))
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

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

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim arrGRGabor As ArrayList = CType(sessHelper.GetSession("GRGabor"), ArrayList)

        If arrGRGabor.Count <> 0 Then
            ' SortListControl(arrAllocationRealTimeService, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrGRGabor, indexPage, dtlDepositA.PageSize)
            dtlDepositA.DataSource = PagedList
            dtlDepositA.VirtualItemCount = arrGRGabor.Count()
            dtlDepositA.DataBind()
        Else
            dtlDepositA.DataSource = New ArrayList
            dtlDepositA.VirtualItemCount = 0
            dtlDepositA.CurrentPageIndex = 0
            dtlDepositA.DataBind()
        End If
    End Sub

    Sub BindDepositA()
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Dim objDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        'Dim txtKodeDealer As New TextBox
        'txtKodeDealer.Text = "100572"
        'If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    If (txtKodeDealer.Text.Trim() <> String.Empty) Then
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        '    End If
        'Else
        '    If (txtKodeDealer.Text.Trim() <> String.Empty) Then
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        '    Else
        '        MessageBox.Show("Tentukan dealer terlebih dahulu")
        '        Exit Sub
        '    End If
        'End If


        'If CInt(ddlProductCategory.SelectedValue) > 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositA), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        'End If
        'AddPeriodCriteria(criterias, "TransactionDate")

        'Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        'If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
        '    sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositA), ViewState("currSortColumn"), ViewState("currSortDirection")))
        '    If ViewState("currSortColumn").ToString().Contains("DealerCode") Then
        '        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositA), "ProductCategory.Code", ViewState("currSortDirection")))
        '    End If
        'Else
        '    sortColl = Nothing
        'End If

        'arlDepositA = New FinishUnit.DepositAFacade(User).Retrieve(criterias, sortColl)
        'If arlDepositA.Count > 0 Then
        '    sHelper.SetSession("AllHeaderToDownload", arlDepositA)
        'End If

        'Dim DealerCode As String = String.Empty
        ''For Each item As DepositA In arlDepositA
        ''    If (Not IsExist(item.Dealer.DealerCode, item.ProductCategory.Code, arlDepositAFilter)) Then
        ''        arlDepositAFilter.Add(item)
        ''    End If
        ''Next
        'arlDepositAFilter.Add(arlDepositA(0))
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRLabor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arrGRGabor As ArrayList = New ServiceTemplateGRLaborFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
        sHelper.SetSession("GRGabor", arrGRGabor)
        If (arrGRGabor.Count > 0) Then
            dtlDepositA.Visible = True
            dtlDepositA.DataSource = arrGRGabor
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

        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositADetail), "DepositA.Dealer.DealerCode", Sort.SortDirection.ASC))
        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositADetail), "TransactionDate", Sort.SortDirection.ASC))
        'Dim arlAllDepositDetails As ArrayList = New FinishUnit.DepositADetailFacade(User).Retrieve(criterias, sortColl)

        'sHelper.SetSession("AllDetailsToDownload", arlAllDepositDetails)


    End Sub

    Private Sub InitData()
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        If Not objDealer Is Nothing Then
            If Not SecurityProvider.Authorize(Context.User, SR.MaintainGeneralRepair_Input_Privilage) Then
                btnSearch.Visible = False
            End If
        End If
    End Sub

    Private _selectedDepositAIDs As String
    

    Sub BindDepositADetails(ByVal ID As Integer, ByVal dg As DataGrid)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositADetail), "DepositA.ID", MatchType.Exact, ID))

        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositADetail), "StatusDebet", Sort.SortDirection.ASC))
        'arlDepositDetails = New FinishUnit.DepositADetailFacade(User).Retrieve(criterias, sortColl)
        AddHandler dg.ItemDataBound, AddressOf ItemDataBound
        'dg.DataSource = arlDepositDetails
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



    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function


#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'mod by Ery
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        If Not IsPostBack Then
            ActiveUserPrivilage()
            InitData()
            BindDepositA()
            dtlDepositA.CurrentPageIndex = 0
            BindDatagrid(dtlDepositA.CurrentPageIndex)
        End If
    End Sub

    Private Sub Detail_ReloadPage(sender As Object, e As EventArgs)
        BindDepositA()
        dtlDepositA.CurrentPageIndex = 0
        BindDatagrid(dtlDepositA.CurrentPageIndex)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'Dim ArrDummy As ArrayList = New ArrayList
        'Dim arDummyHeader As ArrayList = New ArrayList
        'sHelper.SetSession("DataToDownload", ArrDummy)
        'sHelper.SetSession("HeaderDataToDownload", arDummyHeader)


        'Dim diff As Integer = DateDiff(DateInterval.Month, icPeriodeFrom.Value, icPeriodeTo.Value)
        'If diff <= 12 Then
        '    BindDepositA()
        'Else
        '    MessageBox.Show("Periode melebihi 12 bulan.")
        'End If
        'BindDepositA()
        RegisterStartupScript("Open", String.Format("<script>ShowPopUpUpload();</script>"))
        Return
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub ActiveUserPrivilage()
        If Not SecurityProvider.Authorize(Context.User, SR.MaintainGeneralRepair_View_Privilage) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName= Service - Maintain General Repair")
        End If
    End Sub
#End Region

    Private Function IsDBNull(ByVal obj As Object, ByVal replacement As Decimal) As Decimal
        If obj Is DBNull.Value Then
            Return replacement
        End If
        Return CType(obj, Decimal)
    End Function


    Protected Sub LnkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles LnkDownloadTemplate.Click
        ReadData()
        SetDownload()
    End Sub
    Private Sub dtlDepositA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtlDepositA.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Try
                'Dim objContact As CcContact = e.Item.DataItem

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lbJenisService As Label = CType(e.Item.FindControl("lbJenisService"), Label)
                Dim lbKindCode As Label = CType(e.Item.FindControl("lbKindCode"), Label)
                Dim lblGRKindID As Label = CType(e.Item.FindControl("lblGRKindID"), Label)
                Dim lbJasaService As Label = CType(e.Item.FindControl("lbJasaService"), Label)
                Dim lbDurasiService As Label = CType(e.Item.FindControl("lbDurasiService"), Label)
                Dim lbMulaiBerlaku As Label = CType(e.Item.FindControl("lbMulaiBerlaku"), Label)
                Dim IDGRGabor As Label = CType(e.Item.FindControl("IDGRGabor"), Label)
                lblGRKindID.Visible = False
                IDGRGabor.Visible = False
                Dim dt As Date = Date.Parse(lbMulaiBerlaku.Text)
                'DateTime.TryParseExact(lbMulaiBerlaku.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dt)
                lbMulaiBerlaku.Text = dt.ToString("dd/MM/yyyy")

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.GRKind), "ID", MatchType.Exact, lblGRKindID.Text))

                Dim arrGRKind As ArrayList = New GRKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                If arrGRKind.Count > 0 Then
                    Dim itemGRKind As GRKind = arrGRKind.Item(0)
                    lbJenisService.Text = itemGRKind.KindDescription
                    lbKindCode.Text = itemGRKind.KindCode
                Else
                    lbJenisService.Text = String.Empty
                    lbKindCode.Text = String.Empty
                End If

                If lbJasaService.Text = "0" Then
                    lbJasaService.Text = String.Empty
                End If
                If lbDurasiService.Text = "0" Then
                    lbDurasiService.Text = String.Empty
                End If
                If lbMulaiBerlaku.Text = "01/01/1753 00.00.00" Then
                    lbMulaiBerlaku.Text = String.Empty
                End If
                lblNo.Text = (dtlDepositA.CurrentPageIndex * dtlDepositA.PageSize + e.Item.ItemIndex + 1).ToString

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

    End Sub
    Public Sub GetDetailData(ByVal GRGaborId As String)
        Try
            Dim IdGRGabor As Integer = CType(GRGaborId, Integer)

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRPartDetail), "ServiceTemplateGRLaborID", MatchType.Exact, IdGRGabor))
            Dim arrGRGaborPartDetail As ArrayList = New ServiceTemplateGRPartDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            Dim arlDetails As New ArrayList
            For Each itemDetail As ServiceTemplateGRPartDetail In arrGRGaborPartDetail
                Dim dataBindGRGaborPartDetail As New GRGaborPartDetail
                dataBindGRGaborPartDetail.ServiceTemplateGRLaborID = itemDetail.ServiceTemplateGRLaborID
                dataBindGRGaborPartDetail.SparePartMasterID = itemDetail.SparePartMasterID
                criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartMaster), "ID", MatchType.Exact, itemDetail.SparePartMasterID))
                Dim arrsparePart As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                If arrsparePart.Count > 0 Then
                    Dim sparePart As SparePartMaster = arrsparePart.Item(0)
                    dataBindGRGaborPartDetail.KodeSparePart = sparePart.PartNumber
                    dataBindGRGaborPartDetail.NamaSparePart = sparePart.PartName
                    dataBindGRGaborPartDetail.HargaSatuan = sparePart.RetalPrice
                End If
                dataBindGRGaborPartDetail.JumlahUnit = itemDetail.PartQuantity
                arlDetails.Add(dataBindGRGaborPartDetail)
            Next
            dtgDetail.DataSource = arlDetails
            dtgDetail.DataBind()
            'Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dtlDepositA_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtlDepositA.ItemCommand
        If (e.CommandName = "detail") Then
            Dim Id As Label = CType(e.Item.FindControl("IDGRGabor"), Label)
            RegisterStartupScript("Open", String.Format("<script>PopupDetail({0});</script>", Id.Text))
        End If
    End Sub
    Private Sub dtlDepositA_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtlDepositA.PageIndexChanged
        '-- Change datagrid page

        dtlDepositA.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(e.NewPageIndex)

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

    Private Class GRGaborPartDetail
        Private _serviceTemplateGRLaborID As Integer
        Public Property ServiceTemplateGRLaborID As Integer
            Get
                Return _serviceTemplateGRLaborID
            End Get
            Set(ByVal value As Integer)
                _serviceTemplateGRLaborID = value
            End Set
        End Property
        Private _sparePartMasterID As Integer
        Public Property SparePartMasterID As Integer
            Get
                Return _sparePartMasterID
            End Get
            Set(ByVal value As Integer)
                _sparePartMasterID = value
            End Set
        End Property
        Private _NamaSparePart As String = String.Empty
        Public Property NamaSparePart() As String
            Get
                Return _NamaSparePart
            End Get
            Set(ByVal Value As String)
                _NamaSparePart = Value
            End Set
        End Property

        Private _KodeSparePart As String = String.Empty
        Public Property KodeSparePart() As String
            Get
                Return _KodeSparePart
            End Get
            Set(ByVal Value As String)
                _KodeSparePart = Value
            End Set
        End Property

        Private _HargaSatuan As Decimal
        Public Property HargaSatuan() As String
            Get
                Return _HargaSatuan
            End Get
            Set(ByVal Value As String)
                _HargaSatuan = Value
            End Set
        End Property

        Private _JumlahUnit As Decimal
        Public Property JumlahUnit() As Decimal
            Get
                Return _JumlahUnit
            End Get
            Set(ByVal Value As Decimal)
                _JumlahUnit = Value
            End Set
        End Property
    End Class

    Private _lastTransactionDate As String = String.Empty
    Private _lastPeriode As String = String.Empty
    Private _lastDate As String = String.Empty
    Private ItemDetaiIndex As Integer


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
            imp.StopImpersonate()
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
            'itemLine.Append(lblDealerDetail.Text & " - " & lblProdukDetail.Text)
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

    Private Sub dtlDepositA_Load(sender As Object, e As EventArgs) Handles dtlDepositA.Load

    End Sub

    Protected Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Detail_ReloadPage(sender, e)
    End Sub
End Class
