#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports OfficeOpenXml
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Linq
#End Region

Public Class FrmSPOutstandingOrder
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
    Private nDealerID As Integer
    Private sessHelper As SessionHelper = New SessionHelper
    Private ArrList As ArrayList = New ArrayList
    Private totalRow As Integer = 0
    Private _isShowDetailAllowed As Boolean = False
    Private _sessData As String = "FrmSPOutstandingOrder.Data"
    Private _sessCriteriaData As String = "FrmSPOutstandingOrder.CriteriaData"
#End Region

#Region "Custom Method"

    Private Sub BindDdlPaymentType()
        If Not IsNothing(Session("DEALER")) Then
            sessHelper.SetSession("sesDealer", Session("DEALER"))
        Else
            'Response.Redirect("../SessionExpired.htm")
        End If
        Dim dlr As Dealer = CType(Session("sesDealer"), Dealer)
        Dim spCa As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccountFacade(User).RetrieveByDealerCode(dlr.DealerCode)
        Dim oTopCA As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
        Dim listOfPayments As ArrayList = New ArrayList
        If (dlr.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            If Not IsNothing(oTopCA) Then
                listOfPayments = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
            End If
        Else
            Dim criteriaTOP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            listOfPayments = New TermOfPaymentFacade(User).Retrieve(criteriaTOP)
        End If

        ddlTermOfPayment.DataSource = listOfPayments
        ddlTermOfPayment.DataValueField = "ID"
        ddlTermOfPayment.DataTextField = "Description"
        ddlTermOfPayment.DataBind()
        ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
    End Sub

    Protected Sub cmbOrderType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ddlOrderTypeT As DropDownList = CType(sender, DropDownList)
        If cmbOrderTye.SelectedValue = "R" OrElse cmbOrderTye.SelectedValue = "I" OrElse cmbOrderTye.SelectedValue = "Z" Then
            ddlTermOfPayment.Enabled = True
            BindDdlPaymentType()
        Else
            ddlTermOfPayment.ClearSelection()
            ddlTermOfPayment.Items.Insert(0, New ListItem("", ""))
            ddlTermOfPayment.Enabled = False
        End If
    End Sub

    Private Sub GetDealer()
        'Dim objDealer As Dealer = New DealerFacade(User).Retrieve(nDealerID)
        If Not IsNothing(Session("DEALER")) Then
            sessHelper.SetSession("sesDealer", Session("DEALER"))
        Else
            'Response.Redirect("../SessionExpired.htm")
        End If
    End Sub

    Private Sub GetOrderType()
        cmbOrderTye.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer 'LookUp.ArraySPOrderType
            cmbOrderTye.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        cmbOrderTye.DataBind()
    End Sub

    Private Sub GetDocumentType()
        cmbDocumentType.Items.Add(New ListItem("Semua", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPDocumentTypeKTBDealer 'LookUp.ArraySPOrderType
            cmbDocumentType.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        cmbDocumentType.DataBind()
    End Sub

    Private Sub RetrieveHeader()
        GetDealer()
        Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblDealerCode.Text = objDealer.DealerCode
            lblDealerName.Text = objDealer.DealerName
            lblDealerTerm.Text = objDealer.SearchTerm2
        Else
            lblDealerCode.Text = ""
            lblDealerName.Text = ""
            lblDealerTerm.Text = ""
        End If
        GetOrderType()
        GetDocumentType()
        'cmdDownload.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpDownloadOutstanding.aspx?term2=" & objDealer.SearchTerm2.ToUpper.Trim & "','',400,400,Outstanding);")
    End Sub

    Private Sub RetrieveDetails(ByVal pageIndex As Integer)
        If ccPODateEnd.Value >= ccPODateStart.Value Then
            If txtDealerCode.Text.Trim <> "" Then
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
                lblDealerName.Text = objDealer.DealerName
                lblDealerTerm.Text = objDealer.SearchTerm2
                'Else
                '    lblDealerName.Text = String.Empty
                '    lblDealerTerm.Text = String.Empty
            End If
            FindData(pageIndex)
            ArrList = CType(sessHelper.GetSession(Me._sessData), ArrayList)
            If ArrList.Count > 0 Then
                dgSPOutstanding.DataSource = ArrList
                dgSPOutstanding.VirtualItemCount = totalRow
                dgSPOutstanding.DataBind()

            Else
                dgSPOutstanding.DataSource = New ArrayList
                dgSPOutstanding.VirtualItemCount = 0
                dgSPOutstanding.DataBind()
                If IsPostBack Then
                    MessageBox.Show(SR.DataNotFound("Data"))
                End If
            End If
        Else
            MessageBox.Show(SR.InvalidRangeDate)
        End If
    End Sub

    Private Sub FindData(ByVal pageIndex As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If org.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.SparePartPO.Dealer.ID", MatchType.Exact, objDealer.ID))
        ElseIf (Not (String.IsNullorEmpty(txtDealerCode.Text.Replace(";", "','")))) Then
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.SparePartPO.Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))

        End If
        If txtNomorPesanan.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.SparePartPO.PONumber", MatchType.[Partial], txtNomorPesanan.Text.Trim))
        End If
        If Not ddlTermOfPayment.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.SparePartPO.TermOfPayment.ID", MatchType.Exact, ddlTermOfPayment.SelectedValue))
        End If


        If cmbOrderTye.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.SparePartPO.OrderType", MatchType.Exact, cmbOrderTye.SelectedValue))
        If cmbDocumentType.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.DocumentType", MatchType.Exact, cmbDocumentType.SelectedValue))
        criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.SparePartPO.PODate", MatchType.GreaterOrEqual, Format(ccPODateStart.Value, "yyyy/MM/dd")))
        criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.SparePartPO.PODate", MatchType.LesserOrEqual, Format(ccPODateEnd.Value, "yyyy/MM/dd")))

        If cbValidTo.Checked Then
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.ValidTo", MatchType.GreaterOrEqual, Format(ccValidToStart.Value, "yyyy/MM/dd")))
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.ValidTo", MatchType.LesserOrEqual, Format(ccValidToEnd.Value, "yyyy/MM/dd")))
        End If
        If txtPartNumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "PartNumber", MatchType.Exact, txtPartNumber.Text.Trim()))
        End If
        Me.sessHelper.SetSession(Me._sessCriteriaData, criterias)

        Dim ArrListBeforeToday As New ArrayList
        Dim ArrListAfterToday As New ArrayList
        Dim ArrListFinal As New ArrayList
        ArrList = New SparePartOutstandingOrderDetailFacade(User).RetrieveActiveList(pageIndex, dgSPOutstanding.PageSize, totalRow, sessHelper.GetSession("SortCol"), sessHelper.GetSession("SortDirection"), criterias)
        Dim ArrList2 = From obj As SparePartOutstandingOrderDetail In ArrList
                            Order By obj.SparePartOutstandingOrder.ValidTo
                            Select obj
        For Each objSPOutDtl As SparePartOutstandingOrderDetail In ArrList2
            If objSPOutDtl.SparePartOutstandingOrder.ValidTo < Date.Now.ToShortDateString Then
                ArrListBeforeToday.Add(objSPOutDtl)
            Else
                ArrListAfterToday.Add(objSPOutDtl)
            End If
        Next
        ArrListFinal.AddRange(ArrListAfterToday)
        ArrListFinal.AddRange(ArrListBeforeToday)
        If pageIndex <= 1 And ArrListFinal.Count < dgSPOutstanding.PageSize Then
            totalRow = ArrListFinal.Count
        End If
        Me.sessHelper.SetSession(Me._sessData, ArrListFinal)

    End Sub

    Private Function CalculatePOOutstandingAmount(ByVal arlPODetail As ArrayList) As Decimal
        Dim nPOAmount As Decimal = 0
        For Each objPOOutstandingDetail As SparePartOutstandingOrderDetail In arlPODetail
            If IsNothing(objPOOutstandingDetail.SparePart) Then
                objPOOutstandingDetail.SparePart = New SparePartMasterFacade(User).Retrieve(objPOOutstandingDetail.PartNumber)
            End If
            nPOAmount = nPOAmount + (objPOOutstandingDetail.AllocationQty * objPOOutstandingDetail.SparePart.RetalPrice)
        Next
        Return (nPOAmount)
    End Function

    Private Function GetNumericOnly(ByVal OriMoney As Decimal) As String
        Return FormatNumber(OriMoney, 0, TriState.False, TriState.UseDefault, TriState.False)
    End Function



#End Region

#Region "EventHandler"

    Private Sub InitiateAuthorization()

        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If org.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewSPPO_Status_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Outstanding Order")
            End If
            _isShowDetailAllowed = SecurityProvider.Authorize(Context.User, SR.ViewSPPO_StatusDetail_Privilege)

            txtDealerCode.Visible = False
            lblSearchDealer.Visible = False
            lblDealerCode.Visible = True
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.ENHStatusPemesananKTB_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Outstanding Order")
            End If

            txtDealerCode.Visible = True
            lblSearchDealer.Visible = True
            lblDealerCode.Visible = False
        End If


    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        'Put user code to initialize the page here
        If Not IsPostBack Then
            sessHelper.SetSession("SortCol", "CreatedTime")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)

            lblSearchSparePart.Attributes("onclick") = "ShowPopUpSparePart()"
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            btnGetDealer.Style("display") = "none"
            RetrieveHeader()
            RetrieveDetails(1)
            'BindDdlPaymentType()
            ddlTermOfPayment.Enabled = False
        End If


    End Sub

    Private Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If (txtDealerCode.Text.Trim <> "") Then
            sessHelper.SetSession("SortCol", "CreatedTime")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
        Else

            sessHelper.SetSession("SortCol", "SparePartOutstandingOrder.SparePartPO.Dealer.DealerCode")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
        End If
        dgSPOutstanding.CurrentPageIndex = 0
        RetrieveDetails(1)
    End Sub

    Private Sub dgSPOutstanding_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSPOutstanding.ItemDataBound
        Dim objPOOutstandingDetail As SparePartOutstandingOrderDetail

        If e.Item.ItemIndex > -1 Then
            objPOOutstandingDetail = CType(ArrList(e.Item.ItemIndex), SparePartOutstandingOrderDetail)

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSPOutstanding.PageSize * dgSPOutstanding.CurrentPageIndex)
            e.Item.Cells(2).Text = objPOOutstandingDetail.SparePartOutstandingOrder.SparePartPO.Dealer.DealerCode

            For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer 'LookUp.ArraySPOrderType
                If objPOOutstandingDetail.SparePartOutstandingOrder.OrderType.Equals(liOrderType.Value) Then
                    e.Item.Cells(3).Text = liOrderType.Text
                    Exit For
                End If
            Next
            For Each liOrderType As ListItem In LookUp.ArraySPDocumentTypeKTBDealer 'LookUp.ArraySPOrderType
                If objPOOutstandingDetail.SparePartOutstandingOrder.DocumentType.Equals(liOrderType.Value) Then
                    e.Item.Cells(4).Text = liOrderType.Text
                    Exit For
                End If
            Next

        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            SetDGOutstandingDetailItem(e)
        End If
    End Sub

    Private Sub SetDGOutstandingDetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        '_isShowDetailAllowed = SecurityProvider.Authorize(Context.User, SR.ViewSPPO_StatusDetail_Privilege)
        'If (_isShowDetailAllowed) Then
        Dim obj As SparePartOutstandingOrderDetail = CType(e.Item.DataItem, SparePartOutstandingOrderDetail)

        CType(e.Item.FindControl("lblPONo"), LinkButton).Attributes("href") = "javascript:{var x =" + GeneralScript.GetPopUpEventReference("../SparePart/FrmSPOutstandingOrderDetail.aspx?POID=" + obj.SparePartOutstandingOrder.ID.ToString + "", "", 600, 800, "Outstanding") + "}"
        CType(e.Item.FindControl("lbtnExtend"), LinkButton).Attributes("href") = "javascript:{var x =" + GeneralScript.GetPopUpEventReference("../SparePart/FrmSPOutstandingOrderDetailExtendBO.aspx?POID=" + obj.SparePartOutstandingOrder.ID.ToString + "", "", 400, 700, "Outstanding") + "}"
        'End If
        CType(e.Item.FindControl("lblStatus"), Label).Text = CType(New StandardCodeFacade(User).RetrieveByValueId(obj.Status, "EnumSPOutstandingOrder.Status")(0), StandardCode).ValueDesc

        Dim lbtnExtend As LinkButton = CType(e.Item.FindControl("lbtnExtend"), LinkButton)
        lbtnExtend.Visible = False
        Dim intNumberOfDay As Integer = DateDiff(DateInterval.Day, CType(Date.Now.ToString("dd/MM/yyyy"), Date), CType(obj.SparePartOutstandingOrder.ValidTo.ToString("dd/MM/yyyy"), Date)) + 1
        If intNumberOfDay > 0 AndAlso intNumberOfDay <= 7 Then
            If org.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If obj.IsTransfer = 0 AndAlso obj.Status <> 1 AndAlso obj.SparePartOutstandingOrder.SparePartPO.OrderType <> "X" Then
                    e.Item.BackColor = Color.Red
                    lbtnExtend.Visible = True
                End If
            End If
        End If
        If org.Title = EnumDealerTittle.DealerTittle.KTB Then
            'CType(e.Item.FindControl("lblDetail"), Label).Attributes("onclick") = GeneralScript.GetPopUpEventReference("../SparePart/FrmSPOutstandingOrderDetail.aspx?POID=" + e.Item.Cells(0).Text + "", "", 600, 800, "Outstanding")
        End If

        Dim lblEstimateFillDate As Label = CType(e.Item.FindControl("lblEstimateFillDate"), Label)
        If obj.EstimateFillDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
            lblEstimateFillDate.Text = obj.EstimateFillDate.ToString("dd/MM/yyyy")
        End If
        Dim lblKeterangan As Label = CType(e.Item.FindControl("lblKeterangan"), Label)
        If obj.EstimateFillDate.ToString("yyyy-MM-dd") = "9999-12-31" AndAlso obj.EstimateFillQty = 0 Then
            lblKeterangan.Text = "Silahkan menghubungi After Sales Coordinator MMKSI"
        End If
    End Sub

    Private Sub dgSPOutstanding_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSPOutstanding.PageIndexChanged
        dgSPOutstanding.CurrentPageIndex = e.NewPageIndex
        RetrieveDetails(e.NewPageIndex + 1)
    End Sub

    Private Sub dgSPOutstanding_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSPOutstanding.ItemCommand

    End Sub

    Private Sub btnGetDealer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDealer.Click
        If txtDealerCode.Text.Length > 0 Then
            Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
            lblDealerName.Text = ObjDealer.DealerName
            lblDealerTerm.Text = ObjDealer.SearchTerm2
        End If
    End Sub

#End Region

    Private Sub dgSPOutstanding_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSPOutstanding.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortCol") Then
            If sessHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortCol", e.SortExpression)
        dgSPOutstanding.SelectedIndex = -1
        dgSPOutstanding.CurrentPageIndex = 0
        RetrieveDetails(0)
    End Sub

    Private Sub btnDownloadExcel_Click(sender As Object, e As EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        If dgSPOutstanding.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        ' mengambil All data 
        Dim ArrListBeforeToday As New ArrayList
        Dim ArrListAfterToday As New ArrayList
        Dim ArrListALL As New ArrayList
        Dim ArrListSortir As New ArrayList
        Dim Criteria As CriteriaComposite = CType(sessHelper.GetSession(Me._sessCriteriaData), CriteriaComposite)
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(SparePartOutstandingOrderDetail), sessHelper.GetSession("SortCol"), sessHelper.GetSession("SortDirection")))
        ArrListALL = New SparePartOutstandingOrderDetailFacade(User).Retrieve(Criteria, sortCol)
        Dim ArrList2 = From obj As SparePartOutstandingOrderDetail In ArrListALL
                            Order By obj.SparePartOutstandingOrder.ValidTo
                            Select obj
        For Each objSPOutDtl As SparePartOutstandingOrderDetail In ArrList2
            If objSPOutDtl.SparePartOutstandingOrder.ValidTo < Date.Now.ToShortDateString Then
                ArrListBeforeToday.Add(objSPOutDtl)
            Else
                ArrListAfterToday.Add(objSPOutDtl)
            End If
        Next
        ArrListSortir.AddRange(ArrListAfterToday)
        ArrListSortir.AddRange(ArrListBeforeToday)

        If ArrListSortir.Count > 0 Then
            CreateExcel("DAFTAR_OUTSTANDING_ORDER", ArrListSortir)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Kode Dealer"
            ws.Cells("C3").Value = "Jenis Order"
            ws.Cells("D3").Value = "Tipe Dokumen"
            ws.Cells("E3").Value = "Nomor Pesanan"
            ws.Cells("F3").Value = "Cara Pembayaran"
            ws.Cells("G3").Value = "Tanggal Pesanan"
            ws.Cells("H3").Value = "Nomor Barang"
            ws.Cells("I3").Value = "Nama Barang"
            ws.Cells("J3").Value = "Jumlah Pesanan"
            ws.Cells("K3").Value = "Jumlah Sudah Dialokasi"
            ws.Cells("L3").Value = "Jumlah Belum Dialokasi"
            ws.Cells("M3").Value = "Qty Estimasi Pemenuhan"
            ws.Cells("N3").Value = "Tanggal Estimasi Pemenuhan"
            ws.Cells("O3").Value = "Keterangan"
            ws.Cells("P3").Value = "Berlaku s/d"
            ws.Cells("Q3").Value = "Status"

            For i As Integer = 0 To Data.Count - 1
                Dim item As SparePartOutstandingOrderDetail = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = item.SparePartOutstandingOrder.SparePartPO.Dealer.DealerCode
                ws.Cells(i + 4, 3).Value = item.SparePartOutstandingOrder.OrderType
                ws.Cells(i + 4, 4).Value = item.SparePartOutstandingOrder.DocumentType
                ws.Cells(i + 4, 5).Value = item.SparePartOutstandingOrder.SparePartPO.PONumber
                ws.Cells(i + 4, 6).Value = item.SparePartOutstandingOrder.SparePartPO.TermOfPayment.Description
                ws.Cells(i + 4, 7).Value = item.SparePartOutstandingOrder.SparePartPO.PODate.ToString("dd/MM/yyyy")
                ws.Cells(i + 4, 8).Value = item.PartNumber
                ws.Cells(i + 4, 9).Value = item.PartName
                ws.Cells(i + 4, 10).Value = item.OrderQty
                ws.Cells(i + 4, 11).Value = item.AllocationQty
                ws.Cells(i + 4, 12).Value = item.OpenQty
                ws.Cells(i + 4, 13).Value = item.EstimateFillQty
                ws.Cells(i + 4, 14).Value = item.EstimateFillDate.ToString("dd/MM/yyyy")
                If item.EstimateFillDate.ToString("yyyy-MM-dd") = "9999-12-31" AndAlso item.EstimateFillQty = 0 Then
                    ws.Cells(i + 4, 15).Value = "Silahkan menghubungi After Sales Coordinator MMKSI"
                End If
                ws.Cells(i + 4, 16).Value = item.SparePartOutstandingOrder.ValidTo.ToString("dd/MM/yyyy")
                ws.Cells(i + 4, 17).Value = CType(New StandardCodeFacade(User).RetrieveByValueId(item.Status, "EnumSPOutstandingOrder.Status")(0), StandardCode).ValueDesc
            Next

            CreateExcelFile(pck, FileName & " [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "].xls")
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
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()
    End Sub

#Region "Testing GenerateTextFiletoSAP"
    'Protected Sub btnSendToSAP_Click(sender As Object, e As EventArgs) Handles btnSendToSAP.Click
    '    Dim strErrMsg As String = ""
    '    Dim arlSentItems As New ArrayList
    '    arlSentItems = CType(sessHelper.GetSession(Me._sessData), ArrayList)
    '    If Not IsNothing(arlSentItems) AndAlso arlSentItems.Count > 0 Then
    '        Dim arrAfterValidasi As New ArrayList
    '        For Each obj As SparePartOutstandingOrderDetail In arlSentItems
    '            If obj.Status = 1 OrElse obj.Status = 2 Then
    '                arrAfterValidasi.Add(obj)
    '            End If
    '        Next
    '        If arrAfterValidasi.Count = 0 Then
    '            MessageBox.Show("Tidak ada data yang diupload ke SAP")
    '        Else
    '            strErrMsg = GenerateTextFiletoSAP(arrAfterValidasi)
    '            If strErrMsg.Replace("-", "") = "" Then
    '                MessageBox.Show("Data berhasil diupload ke SAP")
    '            End If
    '        End If
    '    Else
    '        MessageBox.Show(SR.DataProcessNotFound("Sparepart PO", "kirim"))
    '    End If
    'End Sub

    'Public Function GenerateTextFiletoSAP(arrSuccess As ArrayList) As String
    '    Dim lines As New StringBuilder
    '    Dim collStatus As Integer() = {1, 2}
    '    Dim _arrSPODtl = From ob As SparePartOutstandingOrderDetail In arrSuccess
    '                         Where collStatus.Contains(ob.Status) _
    '                         And ob.SparePartOutstandingOrder.ValidTo.ToString("dd/MM/yyyy") = Now.ToString("dd/MM/yyyy") _
    '                         And ob.IsTransfer = 0
    '                      Order By ob.SparePartOutstandingOrder.SparePartPO.PONumber
    '                      Select ob

    '    Dim separator As String = ";"
    '    Dim index As Integer = 0

    '    Dim strErrMsg As String = ""
    '    Dim strErrMsg2 As String = ""
    '    Dim strSearchTerm2 As String = ""
    '    Dim strPONumber As String = ""
    '    For Each obj As SparePartOutstandingOrderDetail In _arrSPODtl
    '        Dim line As New System.Text.StringBuilder
    '        If strPONumber <> obj.SparePartOutstandingOrder.SparePartPO.PONumber Then
    '            If lines.ToString().Trim <> "" AndAlso strPONumber <> "" Then
    '                strErrMsg = DoSendSAP(lines, strPONumber, strSearchTerm2)
    '                If strErrMsg <> "" Then
    '                    If strErrMsg2 = "" Then
    '                        strErrMsg2 = "-" & strErrMsg
    '                    Else
    '                        strErrMsg2 += "\n-" & strErrMsg
    '                    End If
    '                End If
    '            End If
    '            strSearchTerm2 = obj.SparePartOutstandingOrder.SparePartPO.Dealer.SearchTerm2
    '            strPONumber = obj.SparePartOutstandingOrder.SparePartPO.PONumber

    '            lines = New StringBuilder
    '            line.Append("H")
    '            line.Append(separator)
    '            line.Append(obj.SparePartOutstandingOrder.SparePartPO.PONumber)
    '            line.Append(vbNewLine)
    '        End If

    '        line.Append("D")
    '        line.Append(separator)
    '        line.Append(obj.PartNumber)
    '        line.Append(separator)
    '        line.Append(If(obj.Status = 2, "X", ""))
    '        line.Append(vbNewLine)
    '        lines.Append(line)
    '    Next

    '    If lines.ToString().Trim <> "" AndAlso strPONumber <> "" Then
    '        strErrMsg = DoSendSAP(lines, strPONumber, strSearchTerm2)
    '        If strErrMsg <> "" Then
    '            If strErrMsg2 = "" Then
    '                strErrMsg2 = "-" & strErrMsg
    '            Else
    '                strErrMsg2 += "\n-" & strErrMsg
    '            End If
    '        Else
    '            Dim result As Integer = 0
    '            For Each obj As SparePartOutstandingOrderDetail In _arrSPODtl
    '                obj.IsTransfer = 1
    '                result = New SparePartOutstandingOrderDetailFacade(User).Update(obj)
    '            Next
    '        End If
    '    End If

    '    Return strErrMsg2
    'End Function

    'Function DoSendSAP(ByVal lines As StringBuilder, ByVal strPONumber As String, ByVal strSearchTerm2 As String) As String
    '    Dim errMess As String = ""
    '    Dim FileDataPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\Sparepart\" & strSearchTerm2 & "\" & strPONumber & "ExtendBO.txt"

    '    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
    '    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
    '    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
    '    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
    '    imp.Start()
    '    Try
    '        Dim dirInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(Path.GetDirectoryName(FileDataPath))
    '        If Not dirInfo.Exists Then
    '            dirInfo.Create()
    '        End If
    '        If System.IO.File.Exists(FileDataPath) Then
    '            System.IO.File.Delete(FileDataPath)
    '        End If
    '        Dim fs As FileStream = New FileStream(FileDataPath, FileMode.CreateNew)
    '        Dim sw As StreamWriter = New StreamWriter(fs)

    '        sw.WriteLine(lines.ToString)
    '        sw.Close()
    '        fs.Close()

    '        imp.StopImpersonate()
    '        imp = Nothing

    '    Catch ex As Exception
    '        errMess = ex.Message
    '    End Try

    '    Return errMess
    'End Function
#End Region

End Class