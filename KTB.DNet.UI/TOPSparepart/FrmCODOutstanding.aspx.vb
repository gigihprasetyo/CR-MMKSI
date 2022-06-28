#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.IO
Imports KTB.DNet.Parser
Imports System.Text

#End Region
Public Class FrmCODOutstanding
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
        txtBillingNumber.Text = String.Empty
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
            dtgARList.DataSource = arlTmp
            dtgARList.VirtualItemCount = totalRow
            dtgARList.DataBind()
            If (IsNothing(dtgARList.Items) Or dtgARList.Items.Count = 0) Then
                MessageBox.Show("Data tidak ditemukan")
            End If
            _SessionHelper.SetSession("FrmCODOutstanding.arlTmp", arlTmp)
        End If
    End Sub

    Private Function getData(ByRef TotRow As Integer) As ArrayList
        'Dim oEUFac = New EquipUserFacade(User)
        Dim cEU As New CriteriaComposite(New Criteria(GetType(CODOutstanding), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cEU.opAnd(New Criteria(GetType(CODOutstanding), "PaymentType", MatchType.Exact, "Z000"))
        Dim sEU As New SortCollection
        Dim aEUs As ArrayList

        If ObjDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (Me.txtKodeDealer.Text.Trim <> String.Empty) Then
                cEU.opAnd(New Criteria(GetType(CODOutstanding), "DealerCode", MatchType.[Partial], Me.txtKodeDealer.Text.Trim()))
            End If
        Else
            cEU.opAnd(New Criteria(GetType(CODOutstanding), "DealerCode", MatchType.[Partial], Me.lblDealerCode.Text.Trim()))
        End If

        If (Me.txtBillingNumber.Text.Trim <> String.Empty) Then
            cEU.opAnd(New Criteria(GetType(CODOutstanding), "BillingNumber", MatchType.[Partial], Me.txtBillingNumber.Text.Trim()))
        End If

        If (Me.txtDeliveryOrder.Text.Trim <> String.Empty) Then
            cEU.opAnd(New Criteria(GetType(CODOutstanding), "DONumber", MatchType.[Partial], Me.txtDeliveryOrder.Text.Trim()))
        End If

        cEU.opAnd(New Criteria(GetType(CODOutstanding), "BillingDate", MatchType.GreaterOrEqual, icBillingDateFrom.Value))
        cEU.opAnd(New Criteria(GetType(CODOutstanding), "BillingDate", MatchType.LesserOrEqual, icBillingDateTo.Value))

        _SessionHelper.SetSession("FrmCODOutstanding.Criteria", cEU)
        aEUs = New CODOutstandingFacade(User).RetrieveActiveList(cEU, dtgARList.CurrentPageIndex + 1, dtgARList.PageSize, TotRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))

        Return aEUs
    End Function

    Protected Sub dtgARList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgARList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1 + (dtgARList.CurrentPageIndex * dtgARList.PageSize)).ToString())
            e.Item.Cells(0).Controls.Add(lNum)

            Dim lblDealerCodeGrid As Label = CType(e.Item.FindControl("lblDealerCodeGrid"), Label)
            Dim lblTipePembayaran As Label = CType(e.Item.FindControl("lblTipePembayaran"), Label)
            Dim lblNomorDO As Label = CType(e.Item.FindControl("lblNomorDO"), Label)
            Dim lblNomorBilling As Label = CType(e.Item.FindControl("lblNomorBilling"), Label)
            Dim lblTglBilling As Label = CType(e.Item.FindControl("lblTglBilling"), Label)
            Dim lblTglPembuatan As Label = CType(e.Item.FindControl("lblTglPembuatan"), Label)
            Dim lblNetAmount As Label = CType(e.Item.FindControl("lblNetAmount"), Label)
            Dim lblTaxAmount As Label = CType(e.Item.FindControl("lblTaxAmount"), Label)
            Dim lblC2Amount As Label = CType(e.Item.FindControl("lblC2Amount"), Label)
            Dim lblTotal As Label = CType(e.Item.FindControl("lblTotal"), Label)

            Dim rowValue As CODOutstanding = CType(e.Item.DataItem, CODOutstanding)
            lblDealerCodeGrid.Text = rowValue.DealerCode
            lblTipePembayaran.Text = rowValue.PaymentType
            lblNomorDO.Text = rowValue.DONumber
            lblNomorBilling.Text = rowValue.BillingNumber
            lblTglBilling.Text = rowValue.BillingDate.ToString("dd/MM/yyyy")
            lblTglPembuatan.Text = rowValue.CreatedTime.ToString("dd/MM/yyyy")
            lblNetAmount.Text = rowValue.NetAmount.ToString("N0")
            lblTaxAmount.Text = rowValue.TaxAmount.ToString("N0")
            lblC2Amount.Text = rowValue.C2Amount.ToString("N0")
            lblTotal.Text = rowValue.Total.ToString("N0")
        End If
    End Sub

    Protected Sub btnDnload_Click(sender As Object, e As EventArgs) Handles btnDnload.Click
        Dim data As ArrayList = New CODOutstandingFacade(User).Retrieve(CType(_SessionHelper.GetSession("FrmCODOutstanding.Criteria"), CriteriaComposite))
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList, Optional ByVal isDetail As Boolean = False, Optional ByVal isHeader As Boolean = False)
        Dim sFileName As String = "CODOutstanding"

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
            itemLine.Append("TOP Spare Part - COD Outstanding")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Tipe Pembayaran" & tab)
            itemLine.Append("Nomor DO" & tab)
            itemLine.Append("Nomor Billing" & tab)
            itemLine.Append("Tanggal Billing" & tab)
            itemLine.Append("Tanggal Pembuatan" & tab)
            itemLine.Append("Net Amount" & tab)
            itemLine.Append("Tax Amount" & tab)
            itemLine.Append("C2 Amount" & tab)
            itemLine.Append("Total" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim No As Integer = 0
            For Each item As CODOutstanding In data
                itemLine.Remove(0, itemLine.Length)
                No += 1
                itemLine.Append(No)
                itemLine.Append(item.DealerCode & tab)
                itemLine.Append(item.PaymentType & tab)
                itemLine.Append(item.DONumber & tab)
                itemLine.Append(item.BillingNumber & tab)
                itemLine.Append(item.BillingDate.ToString("dd/MM/yyyy") & tab)
                itemLine.Append(item.CreatedTime.ToString("dd/MM/yyyy") & tab)
                itemLine.Append(CDbl(item.NetAmount) & tab)
                itemLine.Append(CDbl(item.TaxAmount) & tab)
                itemLine.Append(CDbl(item.C2Amount) & tab)
                itemLine.Append(CDbl(item.Total) & tab)


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