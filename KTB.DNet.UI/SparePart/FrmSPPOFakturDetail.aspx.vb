#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
#End Region

#Region "DotNet Namespace"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmSPPOFakturDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents dtgSPPOStatusDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblOrderType As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrder As System.Web.UI.WebControls.Label
    Protected WithEvents lblDO As System.Web.UI.WebControls.Label
    Protected WithEvents lblInvoice As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblCaraPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalNilaiTagihan As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownloadData As System.Web.UI.WebControls.Button

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private _billingID As Integer = 0
    Private _poID As Integer = 0
    Private _nDealerID As Integer = 0
    Private _objPO As SparePartPO
    Private _objBilling As SparePartBilling
    Private _objBillingDetail As SparePartBillingDetail
    Private _objBillingDetails As ArrayList
    Private _objBillingDetails2 As ArrayList
    Private _nTotalNilaiTagihan As Decimal = 0
#End Region

#Region "Custom Method"

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Private Sub GetSPPOStatus()
        _objBilling = New SparePartBillingFacade(User).Retrieve(_billingID)
        If _objBilling Is Nothing Then
            _sessHelper.SetSession("sessBilling", Nothing)
        Else
            _sessHelper.SetSession("sessBilling", _objBilling)
        End If
    End Sub

    Private Sub BindSPPOStatusHeader()

        If GetFromSession("sessBilling") Is Nothing Then
            Me.lblDealerCode.Text = String.Empty
            Me.lblCaraPembayaran.Text = String.Empty
            Me.lblDealerName.Text = String.Empty
            Me.lblDealerTerm.Text = String.Empty
            Me.lblOrderType.Text = String.Empty
            Me.lblOrder.Text = String.Empty
            Me.lblDO.Text = String.Empty
            Me.lblInvoice.Text = String.Empty

        Else
            _objBilling = CType(Session("sessBilling"), SparePartBilling)
            If CType(Session("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                Me.lblDealerCode.Text = CType(Session("DEALER"), Dealer).DealerCode
                Me.lblDealerName.Text = CType(Session("DEALER"), Dealer).DealerName
                Me.lblDealerTerm.Text = CType(Session("DEALER"), Dealer).SearchTerm2
            Else
                Me.lblDealerCode.Text = _objBilling.Dealer.DealerCode
                Me.lblDealerName.Text = _objBilling.Dealer.DealerName
                Me.lblDealerTerm.Text = _objBilling.Dealer.SearchTerm2
            End If

            _poID = CType(Request.QueryString("POID"), Integer)
            Dim objPO As SparePartPO = New SparePartPOFacade(User).Retrieve(_poID)

            If Not IsNothing(objPO) Then
                Me.lblOrderType.Text = objPO.OrderTypeDesc

                If Not IsNothing(objPO.TermOfPayment) Then
                    Me.lblCaraPembayaran.Text = objPO.TermOfPayment.Description
                End If
                Me.lblOrder.Text = objPO.PONumber & " - " & Format(objPO.PODate, "dd/MM/yyyy")
            End If

            If Not IsNothing(Request.QueryString("DOID")) Then
                Dim objSparepartDO As SparePartDO = New SparePartDOFacade(User).Retrieve(CType(Request.QueryString("DOID"), Integer))
                If Not IsNothing(objSparepartDO) Then
                    Me.lblDO.Text = objSparepartDO.DONumber & " - " & Format(objSparepartDO.DoDate, "dd/MM/yyyy")
                End If

            End If

            Try
                Me.lblInvoice.Text = _objBilling.BillingNumber & " - " & _
                                                 IIf(Format(_objBilling.BillingDate, "dd/MM/yyyy") = "01/01/1753", _
                                                     "", Format(_objBilling.BillingDate, "dd/MM/yyyy"))
            Catch ex As Exception
                Me.lblInvoice.Text = ""
            End Try
        End If
    End Sub

    Private Sub BindTodtgPOStatusDetail(ByVal pageIndex As Integer)
        If GetFromSession("sessBilling") Is Nothing Then

            _objBillingDetails = New ArrayList
            If _objBillingDetails.Count > 0 Then
                dtgSPPOStatusDetail.DataSource = _objBillingDetails
                dtgSPPOStatusDetail.VirtualItemCount = 0
                dtgSPPOStatusDetail.DataBind()
                MessageBox.Show(SR.ViewFail)
            End If
        Else
            _objBilling = CType(Session("sessBilling"), SparePartBilling)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartBillingDetail), "SparePartBilling.ID", MatchType.Exact, _objBilling.ID))
            Dim totalRow As Integer = 0

            'Dim aggr As Aggregate = New Aggregate(GetType(SparePartBillingDetail), "BillingPrice", AggregateType.Sum)
            ''_nTotalNilaiTagihan = New SparePartBillingDetailFacade(User).RetrieveScalar(criterias, aggr)

            Me.lblTotalNilaiTagihan.Text = String.Format("{0:#,##0}", _objBilling.TotalAmount)

            '-- Sorted by
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(SparePartBillingDetail), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

            '_objBillingDetails = New SparePartBillingDetailFacade(User).RetrieveActiveList(criterias, pageIndex + 1, dtgSPPOStatusDetail.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))

            _objBillingDetails = New SparePartBillingDetailFacade(User).Retrieve(criterias, sortColl)

            _sessHelper.SetSession("frmSPPOFakturDetail_objBillingDetails", _objBillingDetails)

            If _objBillingDetails.Count > 0 Then
                dtgSPPOStatusDetail.DataSource = _objBillingDetails
                dtgSPPOStatusDetail.VirtualItemCount = totalRow
                dtgSPPOStatusDetail.DataBind()
                'If _objBillingDetails.Count > dtgSPPOStatusDetail.PageSize Then
                '    _objBillingDetails2 = New SparePartBillingDetailFacade(User).Retrieve(criterias)
                '    _sessHelper.SetSession("frmSPPOFakturDetail_objBillingDetails", _objBillingDetails2)
                'Else
                '    _sessHelper.SetSession("frmSPPOFakturDetail_objBillingDetails", _objBillingDetails)
                'End If
            Else
                dtgSPPOStatusDetail.DataSource = New ArrayList
                dtgSPPOStatusDetail.VirtualItemCount = 0
                dtgSPPOStatusDetail.DataBind()
                MessageBox.Show(SR.ViewFail)
            End If
        End If

    End Sub




    Private Sub WriteSPPOFakturData(ByRef sw As StreamWriter, ByVal objSPFaktur As SparePartBilling)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

        '-- Read SPPO header
        If Not IsNothing(objSPFaktur) Then
            itemLine.Remove(0, itemLine.Length)       '-- Empty line
            itemLine.Append("Kode Dealer" & tab & ": ")
            itemLine.Append(lblDealerCode.Text)
            sw.WriteLine(itemLine.ToString())         '-- Write to file

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Nama Dealer" & tab & ": ")
            itemLine.Append(lblDealerName.Text & " / " & lblDealerTerm.Text)  '-- Nama dealer
            sw.WriteLine(itemLine.ToString())    '-- Write to file

            itemLine.Remove(0, itemLine.Length)   '-- Empty line
            itemLine.Append("Tipe Order" & tab & ": ")
            itemLine.Append(lblOrderType.Text)  '-- Tipe order
            sw.WriteLine(itemLine.ToString())     '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Nomor Pesanan - Tanggal" & tab & ": ")
            itemLine.Append(lblOrder.Text & tab)  '-- PO number
            sw.WriteLine(itemLine.ToString())      '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Nomor DO MMKSI - Tanggal" & tab & ": ")
            itemLine.Append(lblDO.Text & tab)  '-- DO number
            sw.WriteLine(itemLine.ToString())      '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Nomor Faktur - Tanggal" & tab & ": ")
            itemLine.Append(lblInvoice.Text & tab)  '-- 
            sw.WriteLine(itemLine.ToString())      '-- Write to file


            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Total Nilai Tagihan (Rp)" & tab & ": ")
            itemLine.Append(FormatNumber(lblTotalNilaiTagihan.Text, 0))  '-- total Amount
            sw.WriteLine(itemLine.ToString())      '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Cara Pembayaran" & tab & ": ")
            itemLine.Append(lblCaraPembayaran.Text)  '-- total Amount
            sw.WriteLine(itemLine.ToString())      '-- Write to file
        End If

        '-- Read SPPO detail

        _objBillingDetails = _sessHelper.GetSession("frmSPPOFakturDetail_objBillingDetails")

        If Not IsNothing(_objBillingDetails) AndAlso _objBillingDetails.Count <> 0 Then

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            sw.WriteLine(itemLine.ToString())    '-- Write blank line

            '-- Write column header
            itemLine.Remove(0, itemLine.Length)     '-- Empty line
            itemLine.Append("Nomor Barang" & tab)   '-- Part number
            itemLine.Append("Nama Barang" & tab)    '-- Part name
            itemLine.Append("Jumlah Pesanan" & tab)      '-- Quantity
            itemLine.Append("Jumlah Pemenuhan" & tab)    '-- Jumlah Pemenuhan
            'itemLine.Append("Harga Jual KTB (Rp)" & tab) '-- Retail price
            itemLine.Append("Nilai Pesanan (Rp)" & tab)  '-- Total price
            itemLine.Append("Nilai PPn (Rp)" & tab)  '-- Total price
            itemLine.Append("Nilai Tagihan (Rp)" & tab)  '-- Total price
            sw.WriteLine(itemLine.ToString())      '-- Write header


            For Each sppoLine As SparePartBillingDetail In _objBillingDetails

                itemLine.Remove(0, itemLine.Length)  '-- Empty line

                itemLine.Append(sppoLine.SparePartDODetail.SparePartMaster.PartNumber & tab)  '-- Part number
                itemLine.Append(sppoLine.SparePartDODetail.SparePartMaster.PartName & tab)    '-- Part name
                'itemLine.Append(sppoLine.SparePartDODetail.SparePartPOEstimate.SparePartPO.ItemQuantity & tab)                  '-- Jumlah Pesanan
                'itemLine.Append(sppoLine.SparePartDODetail.Qty & tab)                  '-- Jumlah Pemenuhan
                itemLine.Append(FormatNumber(sppoLine.SparePartDODetail.Qty, 0).ToString() & tab)    '-- Nilai Tagihan
                itemLine.Append(FormatNumber(sppoLine.Quantity, 0, TriState.False, TriState.False, TriState.False).ToString() & tab)
                itemLine.Append(FormatNumber(sppoLine.ItemPrice, 0, TriState.False, TriState.False, TriState.False).ToString() & tab)
                itemLine.Append(FormatNumber(sppoLine.Tax, 0, TriState.False, TriState.False, TriState.False).ToString() & tab)
                itemLine.Append(FormatNumber(sppoLine.TotalPrice, 0, TriState.False, TriState.False, TriState.False).ToString() & tab)

                sw.WriteLine(itemLine.ToString())  '-- Write Deposit line
            Next

        End If

    End Sub


    Private Sub DownloadData(ByVal idSPPOFaktur As Integer)

        Dim objSPFaktur As SparePartBilling = New SparePartBillingFacade(User).Retrieve(idSPPOFaktur)

        Dim sFileName As String  '-- File name
        If Not IsNothing(objSPFaktur) Then
            sFileName = "SPPOFaktur" & objSPFaktur.BillingNumber   '-- Set file name as "Status" + "PO number".xls
        Else
            sFileName = "SPPOFaktur"  '-- Dummy file name
        End If
        sFileName = sFileName & "_" & DateTime.Now.ToString("yyyyMMddhhmmss")
        '-- Temp file must be a randomly named file!
        Dim SPPOFakturData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SPPOFakturData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SPPOFakturData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteSPPOFakturData(sw, objSPFaktur)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- DownloadData invoice data to client!
            'Response.Redirect("../DownloadDatalocal.aspx?file=DataTemp\" & sFileName & ".xls")
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show(ex.Message) '"DownloadData data gagal")
        End Try
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            _billingID = CType(Request.QueryString("BillingID"), Integer)
            _poID = CType(Request.QueryString("POID"), Integer)
            _nDealerID = CType(Session("DEALER"), Dealer).ID

            If Not IsNothing(Request.QueryString("BillingID")) Then
                If Not IsNothing(Session("DEALER")) Then
                    GetSPPOStatus()
                    BindSPPOStatusHeader()
                    BindTodtgPOStatusDetail(0)
                End If
            End If
        End If
    End Sub

    Private Sub dtgSPPOStatusDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPPOStatusDetail.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgSPPOStatusDetail.PageSize * (dtgSPPOStatusDetail.CurrentPageIndex))).ToString
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objPO As SparePartBillingDetail = CType(CType(dtgSPPOStatusDetail.DataSource, ArrayList).Item(e.Item.ItemIndex), SparePartBillingDetail)
            Dim lblNomorBarang As Label = CType(e.Item.FindControl("lblNomorBarang"), Label)
            lblNomorBarang.Text = objPO.SparePartDODetail.SparePartMaster.PartNumber
        End If
    End Sub

    Private Sub dtgSPPOStatusDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSPPOStatusDetail.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        dtgSPPOStatusDetail.CurrentPageIndex = 0
        BindTodtgPOStatusDetail(dtgSPPOStatusDetail.CurrentPageIndex)
    End Sub

    Private Sub dtgSPPOStatusDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSPPOStatusDetail.PageIndexChanged
        dtgSPPOStatusDetail.CurrentPageIndex = e.NewPageIndex
        BindTodtgPOStatusDetail(dtgSPPOStatusDetail.CurrentPageIndex)
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        _sessHelper.RemoveSession("sessBilling")
    End Sub

    Protected Sub btnDownloadData_Click(sender As Object, e As EventArgs) Handles btnDownloadData.Click
        DownloadData(CType(Request.QueryString("BillingID"), Integer))
    End Sub

#End Region


End Class