#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmSPPODetail
    Inherits System.Web.UI.Page


#Region "Custom Variable Declaration"
    Private _arlPODetail As ArrayList = New ArrayList
    Private _sesshelper As SessionHelper = New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub InitialEditPage(ByVal nPOID As Integer)
        'If Not IsNothing(Session("DEALER")) Then
        BindOrderType()
        DisplayTransactionResult(nPOID)
        'Else
        '    Response.Redirect("../SessionExpired.htm")
        'End If
    End Sub

    Private Sub BindOrderType()
        ddlOrderType.Items.Clear()
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer
            ddlOrderType.Items.Insert(0, New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        ddlOrderType.DataBind()
    End Sub


    Private Sub BindPODetail()
        _arlPODetail = Session("FrmSPPODetail.sessPODetail")
        lblTotPOAmount.Text = String.Format("{0:#,##0}", CalculatePOAmount(_arlPODetail))
        dtgPODetail.DataSource = _arlPODetail
        dtgPODetail.DataBind()
    End Sub

    Private Function CalculatePOAmount(ByVal arlPODetail As ArrayList) As Decimal
        Dim nPOAmount As Decimal = 0
        For Each objPODetail As SparePartPODetail In arlPODetail
            nPOAmount = nPOAmount + objPODetail.Amount
        Next
        Return (nPOAmount)
    End Function




    Private Function DisplayTransactionResult(ByVal nID As Integer)
        Dim stt As Boolean = False
        Dim objPO As SparePartPO = New SparePartPOFacade(User).Retrieve(nID)
        ddlOrderType.Enabled = False
        'icOrderDate.Enabled = False

        txtPONumber.Text = objPO.PONumber
        lblDealerCode.Text = objPO.Dealer.DealerCode
        lblDealerName.Text = objPO.Dealer.DealerName
        lblDealerTerm.Text = objPO.Dealer.SearchTerm2
        If Not IsNothing(objPO.TermOfPayment) Then
            lblCaraPembayaran.Text = objPO.TermOfPayment.Description
        End If
        LblPODate.Text = String.Format("{0:dd/MM/yyyy}", objPO.PODate)
        'icOrderDate.Value = objPO.PODate 'String.Format("{0:dd/MM/yyyy}", objPO.PODate)
        ddlOrderType.SelectedValue = objPO.OrderType
        _arlPODetail = objPO.SparePartPODetails
        _sesshelper.SetSession("FrmSPPODetail.sessPODetail", _arlPODetail)
        BindPODetail()
    End Function

    Private Function EditPO() As Integer
        Dim ObjPO As SparePartPO = CType(Session("sessPOHeader"), SparePartPO)
        Return New SparePartPOFacade(User).UpdateSparePartPO(ObjPO, CType(Session("FrmSPPODetail.sessPODetail"), ArrayList))
    End Function

    Private Sub WriteSPPOData(ByRef sw As StreamWriter, ByVal objSPStatus As SparePartPO)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

        '-- Read SPPO header
        'Dim objSPStatus As SparePartPOStatus = CType(_sessHelper.GetSession("sesSPPOStatus"), SparePartPOStatus)
        'kode dealer; nama dealer; jenis order; nomor pesanan - tanggal; nomor penjualan ktb - tanggal; nomor faktur - tanggal; total nilai tagihan (rp)
        If Not IsNothing(objSPStatus) Then
            itemLine.Remove(0, itemLine.Length)       '-- Empty line
            itemLine.Append("Kode Dealer" & tab & ": ")
            'itemLine.Append(objSPStatus.SparePartPO.Dealer.DealerCode)  '-- Kode dealer
            itemLine.Append(objSPStatus.Dealer.DealerCode)  '-- Kode dealer
            sw.WriteLine(itemLine.ToString())         '-- Write to file

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Nama Dealer" & tab & ": ")
            'itemLine.Append(objSPStatus.SparePartPO.Dealer.DealerName & " / " & objSPStatus.SparePartPO.Dealer.SearchTerm2)  '-- Nama dealer
            itemLine.Append(objSPStatus.Dealer.DealerName & " / " & objSPStatus.Dealer.SearchTerm2)  '-- Nama dealer
            sw.WriteLine(itemLine.ToString())    '-- Write to file

            itemLine.Remove(0, itemLine.Length)   '-- Empty line
            itemLine.Append("Tipe Order" & tab & ": ")
            itemLine.Append(objSPStatus.OrderTypeDesc)  '-- Tipe order
            sw.WriteLine(itemLine.ToString())     '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Nomor /Tanggal PO" & tab & ": ")
            itemLine.Append(objSPStatus.PONumber & " - " & objSPStatus.PODate & tab)  '-- PO number
            sw.WriteLine(itemLine.ToString())      '-- Write to file


            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Nilai Pemesanan" & tab & ": ")
            itemLine.Append(lblTotPOAmount.Text)  '-- total Amount
            sw.WriteLine(itemLine.ToString())      '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Cara Pembayaran" & tab & ": ")
            itemLine.Append(lblCaraPembayaran.Text)  '-- total Cara Pembayaran
            sw.WriteLine(itemLine.ToString())      '-- Write to file
        End If

        '-- Read SPPO detail

        _arlPODetail = Session("FrmSPPODetail.sessPODetail")

        If Not IsNothing(_arlPODetail) AndAlso _arlPODetail.Count <> 0 Then

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            sw.WriteLine(itemLine.ToString())    '-- Write blank line

            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Nomor Barang" & tab)  '-- Part number
            itemLine.Append("Nama Barang" & tab)   '-- Part name
            itemLine.Append("Jumlah" & tab)      '-- Quantity
            itemLine.Append("Harga Eceran (Rp)" & tab) '-- Retail price
            itemLine.Append("Total Harga (Rp)" & tab)  '-- Total price
            sw.WriteLine(itemLine.ToString())      '-- Write header


            For Each sppoLine As SparePartPODetail In _arlPODetail

                itemLine.Remove(0, itemLine.Length)  '-- Empty line

                itemLine.Append(sppoLine.SparePartMaster.PartNumber & tab)  '-- Part number
                itemLine.Append(sppoLine.SparePartMaster.PartName & tab)    '-- Part name
                itemLine.Append(sppoLine.Quantity & tab)                  '-- Jumlah Pesanan
                itemLine.Append(FormatNumber(sppoLine.RetailPrice, 0, TriState.False, TriState.False, TriState.False).ToString() & tab)                    '-- Retail price
                itemLine.Append(FormatNumber(sppoLine.Amount, 0, TriState.False, TriState.False, TriState.False).ToString() & tab)                    '-- Retail price

                sw.WriteLine(itemLine.ToString())  '-- Write Deposit line
            Next

        End If

    End Sub


    Private Sub download(ByVal poid As Integer)

        'Dim objSPStatus As SparePartPOStatus = New SparePartPOStatusFacade(User).Retrieve(idSPPOStatus)
        Dim objSPPO As SparePartPO = New SparePartPOFacade(User).Retrieve(poid)

        Dim sFileName As String  '-- File name
        If Not IsNothing(objSPPO) Then
            sFileName = "Status" & objSPPO.PONumber   '-- Set file name as "Status" + "PO number".xls
        Else
            sFileName = "SPPOStatus"  '-- Dummy file name
        End If
        sFileName = sFileName & "_" & DateTime.Now.ToString("yyyyMMddhhmmss")
        '-- Temp file must be a randomly named file!
        Dim SPPOStatusData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SPPOStatusData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SPPOStatusData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteSPPOData(sw, objSPPO)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show(ex.Message) '"Download data gagal")
        End Try
    End Sub





#End Region


#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Me._sesshelper.GetSession("frmPOStatus.PrevPage") Is Nothing Then
            txtUrlToBack.Text = ""
        Else
            txtUrlToBack.Text = _sesshelper.GetSession("frmPOStatus.PrevPage")
        End If
        If Not IsPostBack() Then
            If Not IsNothing(Request.QueryString("poid")) Then
                InitialEditPage(CType(Request.QueryString("poID"), Integer))
            Else
                btnDownload.Enabled = False
            End If

        End If
    End Sub

    Private Sub dtgPODetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPODetail.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            SetDtgPODetailItem(e)
        End If

    End Sub


    Private Sub SetDtgPODetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Add(lNum)
    End Sub




    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
             ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        pCompletelist.Sort(objListComparer)

    End Sub

    Private Sub dtgPODetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPODetail.SortCommand
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

        Dim arlCompletelist As ArrayList = Session("FrmSPPODetail.sessPODetail")
        If Not arlCompletelist Is Nothing Then
            SortListControl(arlCompletelist, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            _sesshelper.SetSession("FrmSPPODetail.sessPODetail", arlCompletelist)
            BindPODetail()
        End If
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        download(CType(Request.QueryString("poID"), Integer))
    End Sub

#End Region

End Class