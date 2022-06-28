#Region "Custom Namespace Imports"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Utility
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text

#End Region

Public Class FrmSPPOBillingRecap
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgSPPOBillingRecap As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTotalBilling As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents grid As System.Web.UI.WebControls.DataGrid
    Protected WithEvents LinkButton1 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents ddlMonthTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CompareValidator1 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents lblGrandBillingAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblGrandTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblGrandPPN As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label

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

    Private _Dealer As Dealer
    Private dt As DateTime = DateTime.Now
    Private sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    Private _BillingAmountSum As Decimal = 0
    Private _PPNSum As Decimal = 0
    Private _TotalBillingSum As Decimal = 0
    Private _BillingAmountSumAllPage As Decimal = 0
    Private _PPNSumAllPage As Decimal = 0
    Private _TotalBillingSumAllPage As Decimal = 0

#End Region

    Private Sub InitiateAuthorization()
        Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If Not SecurityProvider.Authorize(context.User, SR.ViewSPPO_Recapitulation_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=REKAPITULASI - Rekapitulasi Pembelian")
                End If
                '--exclude  this privilege from Asra (BA)
                'Me.btnCari.Visible = SecurityProvider.Authorize(context.User, SR.SearchSPPO_Recapitulation_Privilege)
                Me.btnDownload.Visible = SecurityProvider.Authorize(context.User, SR.DownloadSPPO_Recapitulation_Privilege)
            Else
                If Not SecurityProvider.Authorize(context.User, SR.ENHRekapitulasiPembelian_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=REKAPITULASI - Rekapitulasi Pembelian")
                End If
            End If
        End If

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        btnDownload.Enabled = False
        InitiateAuthorization()
        If Not IsNothing(Request.Form("txtDealerName")) Then
            Me.lblDealerName.Text = Request.Form("txtDealerName").ToString
        End If
        If Not IsPostBack Then
            BindHeader()
            ViewState("currSortColumn") = "BillingDate"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindDDLOrderType()
            BindDDLMonth()
            BindDDLYear()
            Me.ddlOrderType.SelectedValue = "A"
            Me.ddlMonth.SelectedValue = CStr(Date.Now.Month)
            Me.ddlMonthTo.SelectedValue = CStr(Date.Now.Month)
            Me.ddlYear.SelectedValue = CStr(Date.Now.Year)
            BindDataGrid(1)
        End If
        txtKodeDealer.Attributes.Add("readonly", "readonly")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub BindHeader()
        checkDealer()
        'Me.lblDealerCode.Text = _Dealer.DealerCode
        'Me.txtKodeDealer.Text = _Dealer.DealerCode
        'Me.lblDealerName.Text = _Dealer.DealerName
        'txtDealerName.Text = _Dealer.DealerName
    End Sub

    Private Sub BindDDLOrderType()
        Me.ddlOrderType.Items.Add(New ListItem("Semua", "A"))
        Me.ddlOrderType.Items.Add(New ListItem("Emergency", "E"))
        Me.ddlOrderType.Items.Add(New ListItem("Regular", "R"))
        Me.ddlOrderType.Items.Add(New ListItem("Retur", "T"))
        Me.ddlOrderType.Items.Add(New ListItem("KhsusuRetur", "K"))
    End Sub

    Private Sub BindDDLMonth()
        'Dim listItemBlank As ListItem = New ListItem("Silahkan Pilih", -1) '-- dummy item denoting ALL
        'ddlMonth.Items.Add(listItemBlank)
        For Each item As ListItem In LookUp.ArrayMonth
            ddlMonth.Items.Add(item)
        Next
        For Each item As ListItem In LookUp.ArrayMonth
            ddlMonthTo.Items.Add(item)
        Next
    End Sub

    Private Sub BindDDLYear()
        'Dim listItemBlank As ListItem = New ListItem("Silahkan Pilih", -1) '-- dummy item denoting ALL
        'ddlYear.Items.Add(listItemBlank)
        For Each item As ListItem In LookUp.ArrayYearWithValue(True, 1, 1, DateTime.Now.Year.ToString)
            ddlYear.Items.Add(item)
        Next
    End Sub

    Private Sub checkDealer()
        If Session("DEALER") Is Nothing Then
            'Response.Redirect("..\SessionExpired.htm")
        Else
            _Dealer = Session("DEALER")
        End If
    End Sub

    Private Sub BindDataGridForDownload(ByRef grid As DataGrid)
        checkDealer()

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOBillingRecap), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Me.ddlOrderType.SelectedValue <> "A" Then
            crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "OrderType", MatchType.Exact, Me.ddlOrderType.SelectedValue))
        End If

        'crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "Dealer.ID", MatchType.Exact, CType(Session("DEALER"), Dealer).ID))
        'If txtKodeDealer.Text.Trim() <> "" Then
        crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text.Trim()))
        'End If

        If Me.ddlMonth.SelectedValue <= Me.ddlMonthTo.SelectedValue Then
            crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "PeriodMonth", MatchType.GreaterOrEqual, CType(Me.ddlMonth.SelectedValue, Integer)))
            crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "PeriodMonth", MatchType.LesserOrEqual, CType(Me.ddlMonthTo.SelectedValue, Integer)))
        End If

        crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "PeriodYear", MatchType.Exact, CType(Me.ddlYear.SelectedValue, Integer)))

        Dim objSPPOBillingFacade As SparePartPOBillingFacade
        objSPPOBillingFacade = New SparePartPOBillingFacade(User)
        Dim list As ArrayList = objSPPOBillingFacade.Retrieve(crit)

        Dim result As Integer
        If Not list Is Nothing Then
            If list.Count < 1 Then
                result = 0
            Else
                result = list.Count
            End If
        End If
        If result >= 1 Then
            grid.DataSource = list
        Else
            grid.DataSource = New ArrayList
        End If
        grid.DataBind()
    End Sub

    Private Sub BindDataGrid(ByVal pageIndex As Integer)
        checkDealer()

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOBillingRecap), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Me.ddlOrderType.SelectedValue <> "A" Then
            crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "OrderType", MatchType.Exact, Me.ddlOrderType.SelectedValue))
        End If
        'crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "Dealer.ID", MatchType.Exact, CType(Session("DEALER"), Dealer).ID))
        crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text.Trim()))

        If CInt(Me.ddlMonth.SelectedValue.Trim) <= CInt(Me.ddlMonthTo.SelectedValue.Trim) Then
            crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "PeriodMonth", MatchType.GreaterOrEqual, CType(Me.ddlMonth.SelectedValue, Integer)))
            crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "PeriodMonth", MatchType.LesserOrEqual, CType(Me.ddlMonthTo.SelectedValue, Integer)))
        End If

        crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "PeriodYear", MatchType.Exact, CType(Me.ddlYear.SelectedValue, Integer)))

        Dim objSPPOBillingFacade As SparePartPOBillingFacade
        Dim result As Integer = 0
        objSPPOBillingFacade = New SparePartPOBillingFacade(User)
        'Dim list As ArrayList = objSPPOBillingFacade.Retrieve(crit)
        Dim list As ArrayList = objSPPOBillingFacade.RetrieveActiveListByCriteria(crit, pageIndex, dtgSPPOBillingRecap.PageSize, result, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))

        Dim aggr As Aggregate
        aggr = New Aggregate(GetType(SparePartPOBillingRecap), "BillingAmount", AggregateType.Sum)
        _BillingAmountSumAllPage = objSPPOBillingFacade.RetrieveScalar(crit, aggr)

        aggr = New Aggregate(GetType(SparePartPOBillingRecap), "PPN", AggregateType.Sum)
        _PPNSumAllPage = objSPPOBillingFacade.RetrieveScalar(crit, aggr)

        _TotalBillingSumAllPage = _BillingAmountSumAllPage + _PPNSumAllPage

        Me.lblGrandBillingAmount.Text = _BillingAmountSumAllPage.ToString("#,##0")
        Me.lblGrandPPN.Text = "PPN (Rp) : " & _PPNSumAllPage.ToString("#,##0")
        Me.lblGrandTotal.Text = "Grand Total (Rp) : " & _TotalBillingSumAllPage.ToString("#,##0")

        If list.Count > 0 Then
            btnDownload.Enabled = True
        Else
            btnDownload.Enabled = False
        End If
        If result >= 1 Then
            Me.dtgSPPOBillingRecap.DataSource = list
        Else
            Me.dtgSPPOBillingRecap.DataSource = New ArrayList
        End If
        dtgSPPOBillingRecap.CurrentPageIndex = pageIndex - 1
        Me.dtgSPPOBillingRecap.VirtualItemCount = result
        Me.dtgSPPOBillingRecap.DataBind()
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If Not Page.IsValid Then
            Exit Sub
        End If

        BindDataGrid(1)
    End Sub

    Private Sub WriteSPPOData(ByRef sw As StreamWriter)

        '''Response.ContentType = "application/vnd.excel"
        '''Response.AddHeader("Content-Disposition", "attachment;filename=SPPOBillingRecap" & sSuffix & ".xls") ' & finfo.Name)
        '''Dim writer As HtmlTextWriter = New HtmlTextWriter(Response.Output)
        ''''Me.dtgSPPOBillingRecap.RenderControl(writer)

        '''For Each col As DataGridColumn In Me.dtgSPPOBillingRecap.Columns
        '''    col.HeaderStyle.BackColor = Color.Black
        '''    col.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        '''    grid.Columns.Add(col)
        '''Next

        '''BindDataGridForDownload(grid)
        '''grid.Visible = True
        '''grid.RenderControl(writer)
        '''Response.End()

        Dim writer As HtmlTextWriter = New HtmlTextWriter(sw)

        For Each col As DataGridColumn In Me.dtgSPPOBillingRecap.Columns
            col.HeaderStyle.BackColor = Color.Black
            col.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            grid.Columns.Add(col)
        Next

        BindDataGridForDownload(grid)
        grid.Visible = True
        grid.RenderControl(writer)

        writer.Flush()

    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        If Not Page.IsValid Then
            Exit Sub
        End If

        '-- Temp file must be a randomly named file!
        Dim sFileName As String = "SPPOBillingRecap" & sSuffix & ".xls"
        Dim SPPOData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then
                Dim finfo As FileInfo = New FileInfo(SPPOData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SPPOData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteSPPOData(sw)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing
            End If
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName)
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try



    End Sub

    Private Sub dtgSPPOBillingRecap_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPPOBillingRecap.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim amountWithPPN As Decimal
            Dim recap As SparePartPOBillingRecap = CType(e.Item.DataItem, SparePartPOBillingRecap)
            Dim SPPOStatFac As SparePartPOStatusFacade = New SparePartPOStatusFacade(User)
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPPOStat As SparePartPOStatus

            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (Me.dtgSPPOBillingRecap.PageSize * Me.dtgSPPOBillingRecap.CurrentPageIndex)
            crit.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.Exact, recap.BillingNumber))
            SPPOStat = SPPOStatFac.Retrieve(crit)

            If recap.OrderType.Trim = "T" Then
                e.Item.Cells(1).Text = "MMKSI reff."
            Else
                If Not SPPOStat Is Nothing Then
                    e.Item.Cells(1).Text = SPPOStat.SparePartPO.PONumber
                Else
                    e.Item.Cells(1).Text = "Tidak Ditemukan"
                End If
            End If

            e.Item.Cells(2).Text = recap.BillingNumber
            e.Item.Cells(3).Text = recap.BillingDate.ToString("dd/MM/yyyy")
            'e.Item.Cells(4).Text = IIf(recap.OrderType.Trim = "T", (recap.BillingAmount * -1).ToString("#,##0"), recap.BillingAmount.ToString("#,##0"))
            e.Item.Cells(4).Text = recap.BillingAmount.ToString("#,##0")
            'e.Item.Cells(5).Text = IIf(recap.OrderType.Trim = "T", (recap.PPN * -1).ToString("#,##0"), recap.PPN.ToString("#,##0"))
            e.Item.Cells(5).Text = recap.PPN.ToString("#,##0")
            'amountWithPPN = IIf(recap.OrderType.Trim = "T", (recap.BillingAmount + recap.PPN) * -1, recap.BillingAmount + recap.PPN)
            amountWithPPN = recap.BillingAmount + recap.PPN
            '_BillingAmountSum = _BillingAmountSum + IIf(recap.OrderType.Trim = "T", recap.BillingAmount * -1, recap.BillingAmount)
            _BillingAmountSum = _BillingAmountSum + recap.BillingAmount
            '_PPNSum = _PPNSum + IIf(recap.OrderType.Trim = "T", recap.PPN * -1, recap.PPN)
            _PPNSum = _PPNSum + recap.PPN
            _TotalBillingSum = _TotalBillingSum + amountWithPPN
            e.Item.Cells(6).Text = amountWithPPN.ToString("#,##0")
            e.Item.Cells(7).Text = IIf(recap.OrderType.Trim = "T", recap.OrderTypeDesc.Substring(0, recap.OrderTypeDesc.Length - 1), recap.OrderTypeDesc)

        End If
        If e.Item.ItemType = ListItemType.Footer Then
            CType(e.Item.Cells(4).FindControl("lblBillingAmountSum"), Label).Text = _BillingAmountSum.ToString("#,##0")
            CType(e.Item.Cells(5).FindControl("lblPPNSum"), Label).Text = _PPNSum.ToString("#,##0")
            CType(e.Item.Cells(6).FindControl("lblTotalBillingSum"), Label).Text = _TotalBillingSum.ToString("#,##0")
        End If
    End Sub

    Private Sub grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grid.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim amountWithPPN As Decimal
            Dim recap As SparePartPOBillingRecap = CType(e.Item.DataItem, SparePartPOBillingRecap)
            Dim SPPOStatFac As SparePartPOStatusFacade = New SparePartPOStatusFacade(User)
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPPOStat As SparePartPOStatus

            e.Item.Cells(0).Text = e.Item.ItemIndex + 1
            crit.opAnd(New Criteria(GetType(SparePartPOStatus), "BillingNumber", MatchType.Exact, recap.BillingNumber))
            SPPOStat = SPPOStatFac.Retrieve(crit)
            If recap.OrderType.Trim = "T" Then
                e.Item.Cells(1).Text = "MMKSI reff."
            Else
                If Not SPPOStat Is Nothing Then
                    e.Item.Cells(1).Text = SPPOStat.SparePartPO.PONumber
                Else
                    e.Item.Cells(1).Text = "Tidak Ditemukan"
                End If
            End If
            e.Item.Cells(2).Text = recap.BillingNumber
            e.Item.Cells(3).Text = recap.BillingDate.ToString("dd/MM/yyyy")
            'e.Item.Cells(4).Text = IIf(recap.OrderType.Trim = "T", (recap.BillingAmount * -1).ToString("#0"), recap.BillingAmount.ToString("#0"))
            'e.Item.Cells(5).Text = IIf(recap.OrderType.Trim = "T", (recap.PPN * -1).ToString("#0"), recap.PPN.ToString("#0"))
            'amountWithPPN = IIf(recap.OrderType.Trim = "T", (recap.BillingAmount + recap.PPN) * -1, recap.BillingAmount + recap.PPN)
            '_BillingAmountSum = _BillingAmountSum + IIf(recap.OrderType.Trim = "T", recap.BillingAmount * -1, recap.BillingAmount)
            '_PPNSum = _PPNSum + IIf(recap.OrderType.Trim = "T", recap.PPN * -1, recap.PPN)
            e.Item.Cells(4).Text = recap.BillingAmount.ToString("#0")
            e.Item.Cells(5).Text = recap.PPN.ToString("#0")
            amountWithPPN = recap.BillingAmount + recap.PPN
            _BillingAmountSum = _BillingAmountSum + recap.BillingAmount
            _PPNSum = _PPNSum + recap.PPN
            _TotalBillingSum = _TotalBillingSum + amountWithPPN
            e.Item.Cells(6).Text = amountWithPPN.ToString("#0")
            e.Item.Cells(7).Text = IIf(recap.OrderType.Trim = "T", recap.OrderTypeDesc.Substring(0, recap.OrderTypeDesc.Length - 1), recap.OrderTypeDesc)

        End If
        'If e.Item.ItemType = ListItemType.Footer Then
        '    CType(e.Item.Cells(4).FindControl("lblBillingAmountSum"), Label).Text = _BillingAmountSum.ToString("#,##0")
        '    CType(e.Item.Cells(5).FindControl("lblPPNSum"), Label).Text = _PPNSum.ToString("#,##0")
        '    CType(e.Item.Cells(6).FindControl("lblTotalBillingSum"), Label).Text = _TotalBillingSum.ToString("#,##0")
        'End If
    End Sub

    Private Sub dtgSPPOBillingRecap_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSPPOBillingRecap.PageIndexChanged
        dtgSPPOBillingRecap.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(e.NewPageIndex + 1)
    End Sub

    Private Sub dtgSPPOBillingRecap_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSPPOBillingRecap.SortCommand
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
        dtgSPPOBillingRecap.CurrentPageIndex = 0
        BindDataGrid(dtgSPPOBillingRecap.CurrentPageIndex + 1)
    End Sub

    Private Sub LinkButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("../WindowServiceTest.aspx")
    End Sub
End Class
