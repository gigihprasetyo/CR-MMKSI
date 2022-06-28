#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.IO
Imports KTB.DNet.Parser
Imports System.Text

#End Region
Public Class FrmCODPayment
    Inherits System.Web.UI.Page

    Private _SessionHelper As SessionHelper = New SessionHelper
    Private ObjDealer As Dealer = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ObjDealer = CType(_SessionHelper.GetSession("DEALER"), Dealer)
        If Not IsPostBack Then
            InitiatePage()
            If ObjDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                txtKodeDealer.Visible = False
                lblDealerCode.Visible = True
                lblDealerCodePopUp.Visible = False
                lblDealerCode.Text = ObjDealer.DealerCode
            Else
                txtKodeDealer.Visible = True
                lblDealerCode.Visible = False
                lblDealerCodePopUp.Visible = True
            End If
            bindDataGrid(0)
        End If
    End Sub

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()
        txtKodeDealer.Text = String.Empty
        txtSONumber.Text = String.Empty
        txtDeliveryOrder.Text = String.Empty
        Dim arlNew As New ArrayList
        dtgARList.DataSource = arlNew
        dtgARList.DataBind()
    End Sub

    Private Sub bindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            'get prerequire data
            Dim arlTmp As ArrayList = Me.getData(totalRow)
            If ObjDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                dtgARList.ShowFooter = False
                roDepositTR.Visible = False
            End If
            _SessionHelper.SetSession("FrmCODPayment.arlTmp", arlTmp)
            dtgARList.DataSource = arlTmp
            dtgARList.VirtualItemCount = totalRow
            dtgARList.DataBind()
            If (IsNothing(dtgARList.Items) Or dtgARList.Items.Count = 0) Then
                MessageBox.Show("Data tidak ditemukan")
            End If
        End If
    End Sub

    Private Function getData(ByRef TotRow As Integer) As ArrayList
        'Dim oEUFac = New EquipUserFacade(User)
        Dim cEU As New CriteriaComposite(New Criteria(GetType(CODPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'cEU.opAnd(New Criteria(GetType(CODPayment), "TermOfPayment.ID", MatchType.Exact, 1))
        Dim sEU As New SortCollection
        Dim aEUs As ArrayList

        If ObjDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (Me.txtKodeDealer.Text.Trim <> String.Empty) Then
                cEU.opAnd(New Criteria(GetType(CODPayment), "DealerCode", MatchType.[Partial], Me.txtKodeDealer.Text.Trim()))
            End If
        Else
            cEU.opAnd(New Criteria(GetType(CODPayment), "DealerCode", MatchType.[Partial], Me.lblDealerCode.Text.Trim()))
        End If

        If (Me.txtSONumber.Text.Trim <> String.Empty) Then
            cEU.opAnd(New Criteria(GetType(CODPayment), "SalesOrderNo", MatchType.[Partial], Me.txtSONumber.Text.Trim()))
        End If

        If (Me.txtDeliveryOrder.Text.Trim <> String.Empty) Then
            cEU.opAnd(New Criteria(GetType(CODPayment), "DeliveryNo", MatchType.[Partial], Me.txtDeliveryOrder.Text.Trim()))
        End If

        cEU.opAnd(New Criteria(GetType(CODPayment), "SODate", MatchType.GreaterOrEqual, icSODateFrom.Value))
        cEU.opAnd(New Criteria(GetType(CODPayment), "SODate", MatchType.LesserOrEqual, icSODateTo.Value))

        _SessionHelper.SetSession("FrmCODPayment.Criteria", cEU)
        aEUs = New CODPaymentFacade(User).RetrieveActiveList(cEU, dtgARList.CurrentPageIndex + 1, dtgARList.PageSize, TotRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))

        Return aEUs
    End Function

    Protected Sub dtgARList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgARList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1 + (dtgARList.CurrentPageIndex * dtgARList.PageSize)).ToString())
            e.Item.Cells(0).Controls.Add(lNum)

            Dim lblDealerCodeGrid As Label = CType(e.Item.FindControl("lblDealerCodeGrid"), Label)
            Dim lblSalesOrder As Label = CType(e.Item.FindControl("lblSalesOrder"), Label)
            Dim lblDeliveryNo As Label = CType(e.Item.FindControl("lblDeliveryNo"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblOrderType As Label = CType(e.Item.FindControl("lblOrderType"), Label)
            Dim lblSODate As Label = CType(e.Item.FindControl("lblSODate"), Label)
            Dim lblRetail As Label = CType(e.Item.FindControl("lblRetail"), Label)
            Dim lblDepositC2 As Label = CType(e.Item.FindControl("lblDepositC2"), Label)
            Dim lblPPN As Label = CType(e.Item.FindControl("lblPPN"), Label)
            Dim lblTotal As Label = CType(e.Item.FindControl("lblTotal"), Label)
            Dim lblRODeposit As Label = CType(e.Item.FindControl("lblRODeposit"), Label)

            Dim rowValue As CODPayment = CType(e.Item.DataItem, CODPayment)
            lblDealerCodeGrid.Text = rowValue.DealerCode
            lblSalesOrder.Text = rowValue.SalesOrderNo
            lblDeliveryNo.Text = rowValue.DeliveryNo
            lblDealerName.Text = New DealerFacade(User).Retrieve(rowValue.DealerCode).DealerName
            lblOrderType.Text = rowValue.OrderType
            lblSODate.Text = rowValue.SODate.ToString("dd/MM/yyyy")
            lblRetail.Text = rowValue.RetailAmount.ToString("N0")
            lblDepositC2.Text = rowValue.DepositC2Amount.ToString("N0")
            lblPPN.Text = rowValue.PPNAmount.ToString("N0")
            lblTotal.Text = rowValue.Total.ToString("N0")
            lblRODeposit.Text = rowValue.RODeposit.ToString("N0")

            lblJMLDepositRO.Text = "Rp. " & rowValue.RODeposit.ToString("N0")
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            If ObjDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                If Not IsNothing(_SessionHelper.GetSession("FrmCODPayment.arlTmp")) Then

                    Dim lblTotalText As Label = CType(e.Item.FindControl("lblTotalText"), Label)
                    Dim lblTotalRetail As Label = CType(e.Item.FindControl("lblTotalRetail"), Label)
                    Dim lblTotalDeposit As Label = CType(e.Item.FindControl("lblTotalDeposit"), Label)
                    Dim lblTotalPPN As Label = CType(e.Item.FindControl("lblTotalPPN"), Label)
                    Dim lblTotalTotal As Label = CType(e.Item.FindControl("lblTotalTotal"), Label)
                    Dim lblTotalRODeposit As Label = CType(e.Item.FindControl("lblTotalRODeposit"), Label)

                    Dim arlTotal As ArrayList = CType(_SessionHelper.GetSession("FrmCODPayment.arlTmp"), ArrayList)
                    Dim tRetail As Decimal = 0
                    Dim tDeposit As Decimal = 0
                    Dim tPPN As Decimal = 0
                    Dim tTotal As Decimal = 0
                    Dim tRODeposit As Decimal = 0
                    If arlTotal.Count > 0 Then
                        For Each i As CODPayment In arlTotal
                            tRetail += i.RetailAmount
                            tDeposit += i.DepositC2Amount
                            tPPN += i.PPNAmount
                            tTotal += i.Total
                            tRODeposit += i.RODeposit
                        Next

                        lblTotalText.Text = "Total"
                        lblTotalRetail.Text = tRetail.ToString("N0")
                        lblTotalDeposit.Text = tDeposit.ToString("N0")
                        lblTotalPPN.Text = tPPN.ToString("N0")
                        lblTotalTotal.Text = tTotal.ToString("N0")
                        lblTotalRODeposit.Text = tRODeposit.ToString("N0")
                    End If
                End If
            End If
        End If

    End Sub

    Protected Sub btnDnload_Click(sender As Object, e As EventArgs) Handles btnDnload.Click
        Dim data As ArrayList = New CODPaymentFacade(User).Retrieve(CType(_SessionHelper.GetSession("FrmCODPayment.Criteria"), CriteriaComposite))
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList, Optional ByVal isDetail As Boolean = False, Optional ByVal isHeader As Boolean = False)
        Dim sFileName As String = "CODPaymentList"

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

                WriteData(sw, data)

                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal " + ex.Message)
        End Try
    End Sub

    Private Sub WriteData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("TOP Spare Part - COD Payment List")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Sales Order" & tab)
            itemLine.Append("Delivery Order" & tab)
            itemLine.Append("Dealer Name" & tab)
            itemLine.Append("Order Type" & tab)
            itemLine.Append("Issue/SO Date" & tab)
            itemLine.Append("Retail" & tab)
            itemLine.Append("Deposit C2" & tab)
            itemLine.Append("PPN" & tab)
            itemLine.Append("Total" & tab)
            itemLine.Append("RO Deposit" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim Nomor As Integer = 0
            For Each item As CODPayment In data
                itemLine.Remove(0, itemLine.Length)
                Nomor += 1
                itemLine.Append(Nomor.ToString & tab)
                itemLine.Append(item.DealerCode & tab)
                itemLine.Append(item.SalesOrderNo & tab)
                itemLine.Append(item.DeliveryNo & tab)
                itemLine.Append(New DealerFacade(User).Retrieve(item.DealerCode).DealerName & tab)
                itemLine.Append(item.OrderType & tab)
                itemLine.Append(item.SODate.ToString("dd/MM/yyyy") & tab)
                itemLine.Append(CDbl(item.RetailAmount) & tab)
                itemLine.Append(CDbl(item.DepositC2Amount) & tab)
                itemLine.Append(CDbl(item.PPNAmount) & tab)
                itemLine.Append(CDbl(item.Total) & tab)
                itemLine.Append(CDbl(item.RODeposit) & tab)


                sw.WriteLine(itemLine.ToString())
            Next
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        dtgARList.CurrentPageIndex = 0
        bindDataGrid(dtgARList.CurrentPageIndex)
    End Sub

    Protected Sub dtgARList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgARList.PageIndexChanged
        dtgARList.CurrentPageIndex = e.NewPageIndex
        bindDataGrid(e.NewPageIndex)
    End Sub

End Class