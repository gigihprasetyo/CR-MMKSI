#Region "Custom Namespace Imports"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

#Region "DotNet Namespace"
Imports System.IO
Imports System.Text
#End Region

Public Class frmSPPOStatus
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlJenisOrder As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPackingStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgPOStatus As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents icPODate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icSODate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icSODateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGetDealer As System.Web.UI.WebControls.Button
    Protected WithEvents cmbDocumentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFakturNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkPODate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkSODate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlCeilingStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Custom Variable Declaration "
    Private _nDealerID As Integer
    Private _sessHelper As SessionHelper = New SessionHelper
    Private _ListPOStatus As ArrayList = New ArrayList
    Private _isPrintAllowed As Boolean = False
    Private _isShowDetailAllowed As Boolean = False
#End Region

#Region " Custom Method "

    Private Sub BindTodtgPOStatus(ByVal pageIndex As Integer)

        If txtDealerCode.Text.Trim <> "" Then
            Dim objDealerFind As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
            lblDealerName.Text = objDealerFind.DealerName
            lblDealerTerm.Text = objDealerFind.SearchTerm2
        Else
            If txtDealerCode.Visible Then
                lblDealerName.Text = String.Empty
                lblDealerTerm.Text = String.Empty
            End If
        End If

        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SparePartFlow), "POID", MatchType.Greater, 1))
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "DealerCode", MatchType.Exact, CType(Session("DEALER"), Dealer).DealerCode))
        ElseIf (txtDealerCode.Text.Trim <> "") Then
            criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If

        If chkPODate.Checked Then
            If icPODate.Value <= icPODateTo.Value Then
                criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "PODate", MatchType.GreaterOrEqual, Format(icPODate.Value, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "PODate", MatchType.LesserOrEqual, Format(icPODateTo.Value, "yyyy/MM/dd")))
            Else
                criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "PODate", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "PODate", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                icPODate.Value = Date.Now
                icPODateTo.Value = Date.Now
            End If
        End If

        If chkSODate.Checked Then
            If icSODate.Value <= icSODateTo.Value Then
                criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "SoDate", MatchType.GreaterOrEqual, Format(icSODate.Value, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "SoDate", MatchType.LesserOrEqual, Format(icSODateTo.Value, "yyyy/MM/dd")))
            Else
                criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "SoDate", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "SoDate", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
                icSODate.Value = Date.Now
                icSODateTo.Value = Date.Now
            End If
        End If

        If txtPONumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "PONumber", MatchType.[Partial], txtPONumber.Text.Trim))
        End If

        If txtSONumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "SONumber", MatchType.[Partial], txtSONumber.Text.Trim))
        End If

        If txtDONumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "DONumber", MatchType.[Partial], txtDONumber.Text.Trim))
        End If

        If txtFakturNumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "BillingNumber", MatchType.[Partial], txtFakturNumber.Text.Trim))
        End If

        If Not ddlCeilingStatus.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SparePartFlow), "TOPCeilingStatus", MatchType.Exact, ddlCeilingStatus.SelectedValue))
        End If

        If Not ddlTermOfPayment.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SparePartFlow), "TermOfPaymentID", MatchType.Exact, ddlTermOfPayment.SelectedValue))
        End If

        If ddlJenisOrder.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "OrderType", MatchType.Exact, ddlJenisOrder.SelectedValue))

        If cmbDocumentType.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "DocumentType", MatchType.Exact, cmbDocumentType.SelectedValue))

        If ddlPackingStatus.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "Status", MatchType.Exact, ddlPackingStatus.SelectedValue))

        Dim Crt As String = criterias.ToString()
        Dim sorrt As String = CType(ViewState("currSortColumn"), String)
        Dim totalRow As Integer = 0
        '_ListPOStatus = New V_SparePartFlowFacade(User).RetrieveActiveList(criterias, pageIndex + 1, dtgPOStatus.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortTable"), System.Type), CType(ViewState("currSortDirection"), Sort.SortDirection))

        _ListPOStatus = New V_SparePartFlowFacade(User).RetrieveCustomPagingBySP(criterias, pageIndex + 1, dtgPOStatus.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortTable"), System.Type), CType(ViewState("currSortDirection"), Sort.SortDirection))

        'Dim _newArl As New ArrayList
        'If ddlPackingStatus.SelectedValue <> 99 Then
        '    For Each item As V_SparePartFlow In _ListPOStatus
        '        If item.PackingStatusID = CType(ddlPackingStatus.SelectedValue, Integer) Then
        '            _newArl.Add(item)
        '        End If
        '    Next
        'Else
        '    _newArl = _ListPOStatus
        'End If
        If _ListPOStatus.Count > 0 Then
            dtgPOStatus.DataSource = _ListPOStatus
            dtgPOStatus.VirtualItemCount = totalRow
            dtgPOStatus.DataBind()
        Else
            dtgPOStatus.DataSource = New ArrayList
            dtgPOStatus.VirtualItemCount = 0
            dtgPOStatus.DataBind()
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If

    End Sub


    Private Sub BindHeader()
        Dim ObjMainDealer As Dealer = CType(Session("DEALER"), Dealer)
        If ObjMainDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblDealerCode.Text = ObjMainDealer.DealerCode
            lblDealerName.Text = ObjMainDealer.DealerName
            lblDealerTerm.Text = ObjMainDealer.SearchTerm2
        Else
            lblDealerCode.Text = ""
            lblDealerName.Text = ""
            lblDealerTerm.Text = ""
        End If

        BindJenisOrder()
        BindStatus()
    End Sub

    Private Sub BindJenisOrder()
        ddlJenisOrder.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer
            ddlJenisOrder.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next

    End Sub

    Private Sub BindStatus()
        Dim arl As ArrayList = EnumSparePartStatus.RetriveSparePartStatus(True)
        ddlPackingStatus.DataTextField = "NameStatus"
        ddlPackingStatus.DataValueField = "ValStatus"
        ddlPackingStatus.DataSource = arl
        ddlPackingStatus.DataBind()

    End Sub


    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If Not (chkPODate.Checked) And Not (chkSODate.Checked) Then
            MessageBox.Show("Tentukan tanggal pesanan atau tanggal penjualan MMKSI")
            Exit Sub
        End If

        If chkPODate.Checked Then
            Dim tglPODari As DateTime = New DateTime(Me.icPODate.Value.Year, Me.icPODate.Value.Month, Me.icPODate.Value.Day)
            Dim tglPOSampai As DateTime = New DateTime(Me.icPODateTo.Value.Year, Me.icPODateTo.Value.Month, Me.icPODateTo.Value.Day)
            If tglPOSampai >= tglPODari Then
                Dim Time As TimeSpan = tglPOSampai.Subtract(tglPODari)
                If Time.Days > 65 Then
                    MessageBox.Show("Periode PO Melebihi 65 Hari")
                    Exit Sub
                End If
            Else
                MessageBox.Show("Tanggal akhir > tanggal awal")
            End If
        End If

        If chkSODate.Checked Then
            Dim tglSODari As DateTime = New DateTime(Me.icSODate.Value.Year, Me.icSODate.Value.Month, Me.icSODate.Value.Day)
            Dim tglSOSampai As DateTime = New DateTime(Me.icSODateTo.Value.Year, Me.icSODateTo.Value.Month, Me.icSODateTo.Value.Day)
            If tglSOSampai >= tglSODari Then
                Dim Time As TimeSpan = tglSOSampai.Subtract(tglSODari)
                If Time.Days > 65 Then
                    MessageBox.Show("Periode SO Melebihi 65 Hari")
                    Exit Sub
                End If
            Else
                MessageBox.Show("Tanggal akhir > tanggal awal")
            End If
        End If


        If Not (chkPODate.Checked) And Not (chkSODate.Checked) Then
            MessageBox.Show("Tentukan tanggal pesanan atau tanggal penjualan MMKSI")
            Exit Sub
        End If

        If (txtDealerCode.Text.Trim <> "") Then
            ViewState("currSortColumn") = "POID"
            ViewState("currSortTable") = GetType(V_SparePartFlow)
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        Else

            ViewState("currSortColumn") = "POID"
            ViewState("currSortTable") = GetType(V_SparePartFlow)
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        dtgPOStatus.CurrentPageIndex = 0
        BindTodtgPOStatus(dtgPOStatus.CurrentPageIndex)
    End Sub

    'Private Function CalculatePOAmount(ByVal arlPODetail As ArrayList) As Decimal
    '    Dim nPOAmount As Decimal = 0
    '    For Each objSPStatusDetail As V_SparePartFlowDetail In arlPODetail
    '        nPOAmount = nPOAmount + objSPStatusDetail.BillingPrice
    '    Next
    '    Return (nPOAmount)
    'End Function

    'Private Sub WriteSPPOData(ByRef sw As StreamWriter, ByVal objSPStatus As V_SparePartFlow)

    '    Dim tab As Char = Chr(9)  '-- Separator character <Tab>
    '    Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

    '    '-- Read SPPO header
    '    'Dim objSPStatus As V_SparePartFlow = CType(_sessHelper.GetSession("sesSPPOStatus"), V_SparePartFlow)
    '    'kode dealer; nama dealer; jenis order; nomor pesanan - tanggal; nomor penjualan ktb - tanggal; nomor faktur - tanggal; total nilai tagihan (rp)
    '    If Not IsNothing(objSPStatus) Then
    '        itemLine.Remove(0, itemLine.Length)       '-- Empty line
    '        itemLine.Append("Kode Dealer:" & tab)
    '        itemLine.Append(objSPStatus.SparePartPO.Dealer.DealerCode)  '-- Kode dealer
    '        sw.WriteLine(itemLine.ToString())         '-- Write to file

    '        itemLine.Remove(0, itemLine.Length)  '-- Empty line
    '        itemLine.Append("Nama Dealer:" & tab)
    '        itemLine.Append(objSPStatus.SparePartPO.Dealer.DealerName & " / " & objSPStatus.SparePartPO.Dealer.SearchTerm2)  '-- Nama dealer
    '        sw.WriteLine(itemLine.ToString())    '-- Write to file

    '        itemLine.Remove(0, itemLine.Length)   '-- Empty line
    '        itemLine.Append("Tipe Order:" & tab)
    '        itemLine.Append(objSPStatus.SparePartPO.OrderTypeDesc)  '-- Tipe order
    '        sw.WriteLine(itemLine.ToString())     '-- Write to file

    '        itemLine.Remove(0, itemLine.Length)    '-- Empty line
    '        itemLine.Append("No Pesanan - Tanggal:" & tab)
    '        itemLine.Append(objSPStatus.SparePartPO.PONumber & " - " & objSPStatus.SparePartPO.PODate & tab)  '-- PO number
    '        sw.WriteLine(itemLine.ToString())      '-- Write to file



    '        itemLine.Remove(0, itemLine.Length)    '-- Empty line
    '        itemLine.Append("No Penjualan KTB - Tanggal:" & tab)

    '        If IsNothing(objSPStatus.SparePartPO) = False AndAlso IsNothing(objSPStatus.SparePartPO.SparePartPOEstimate) = False Then
    '            itemLine.Append(objSPStatus.SparePartPO.SparePartPOEstimate.SONumber & " - " & Format(objSPStatus.SparePartPO.SparePartPOEstimate.SODate, "dd/MM/yyyy"))  '-- KTB Number
    '        Else
    '            itemLine.Append("" & " - " & "")  '-- KTB Number
    '        End If
    '        sw.WriteLine(itemLine.ToString())      '-- Write to file

    '        itemLine.Remove(0, itemLine.Length)    '-- Empty line
    '        itemLine.Append("No Faktur - Tanggal:" & tab)
    '        itemLine.Append(objSPStatus.BillingNumber & " - " & IIf(Format(objSPStatus.BillingDate, "dd/MM/yyyy") = "01/01/1753", "", Format(objSPStatus.BillingDate, "dd/MM/yyyy")))  '-- Faktur Number
    '        sw.WriteLine(itemLine.ToString())      '-- Write to file

    '    End If

    '    '-- Read SPPO detail
    '    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_SparePartFlowDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SparePartFlowDetail), "V_SparePartFlow.ID", MatchType.Exact, objSPStatus.ID))

    '    Dim arSPStatusDetail As ArrayList = New V_SparePartFlowDetailFacade(User).RetrieveList(criterias)

    '    If Not IsNothing(arSPStatusDetail) AndAlso arSPStatusDetail.Count <> 0 Then

    '        itemLine.Remove(0, itemLine.Length)  '-- Empty line
    '        itemLine.Append("Total Nilai Tagihan (Rp):" & tab)
    '        itemLine.Append(CalculatePOAmount(arSPStatusDetail))  '-- Billing Amount
    '        sw.WriteLine(itemLine.ToString())    '-- Write to file

    '        itemLine.Remove(0, itemLine.Length)  '-- Empty line
    '        sw.WriteLine(itemLine.ToString())    '-- Write blank line

    '        '-- Write column header
    '        itemLine.Remove(0, itemLine.Length)  '-- Empty line
    '        itemLine.Append("No. Barang" & tab)  '-- Part number
    '        itemLine.Append("Nama Brg." & tab)   '-- Part name
    '        itemLine.Append("Jumlah Pesanan" & tab)      '-- Quantity
    '        itemLine.Append("Jumlah Dipenuhi" & tab)      '-- Quantity
    '        itemLine.Append("Harga Jual KTB (Rp)" & tab)  '-- Retail price
    '        itemLine.Append("Nilai Pesanan (Rp)" & tab)  '-- Retail price
    '        itemLine.Append("Nilai Tagihan (Rp)" & tab)  '-- Retail price
    '        sw.WriteLine(itemLine.ToString())      '-- Write header

    '        For Each sppoLine As V_SparePartFlowDetail In arSPStatusDetail

    '            itemLine.Remove(0, itemLine.Length)  '-- Empty line

    '            itemLine.Append(Chr(39) & sppoLine.SparePartMaster.PartNumber & tab)  '-- Part number
    '            itemLine.Append(sppoLine.SparePartMaster.PartName & tab)    '-- Part name
    '            itemLine.Append(sppoLine.SOQuantity & tab)                  '-- Jumlah Pesanan
    '            itemLine.Append(sppoLine.BillingQuantity & tab)             '-- Jumlah Dipenuhi
    '            itemLine.Append(sppoLine.NetPrice & tab)                    '-- Harga Jual KTB
    '            itemLine.Append(sppoLine.SOPrice & tab)                     '-- Nilai Pesanan
    '            itemLine.Append(sppoLine.BillingPrice & tab)                '-- Nilai Tagihan

    '            sw.WriteLine(itemLine.ToString())  '-- Write Deposit line
    '        Next

    '    End If

    'End Sub

    'Private Sub download(ByVal idSPPOStatus As Integer)
    '    Dim objSPStatus As V_SparePartFlow = New V_SparePartFlowFacade(User).Retrieve(idSPPOStatus)

    '    Dim sFileName As String  '-- File name
    '    If Not IsNothing(objSPStatus) Then
    '        sFileName = "Status" & objSPStatus.PONumber   '-- Set file name as "Status" + "PO number".xls
    '    Else
    '        sFileName = "SPPOStatus"  '-- Dummy file name
    '    End If

    '    '-- Temp file must be a randomly named file!
    '    Dim SPPOStatusData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

    '    '-- Impersonation to manipulate file in server
    '    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
    '    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
    '    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
    '    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

    '    Try
    '        If imp.Start() Then

    '            Dim finfo As FileInfo = New FileInfo(SPPOStatusData)
    '            If finfo.Exists Then
    '                finfo.Delete()  '-- Delete temp file if exists
    '            End If

    '            '-- Create file stream
    '            Dim fs As FileStream = New FileStream(SPPOStatusData, FileMode.CreateNew)
    '            '-- Create stream writer
    '            Dim sw As StreamWriter = New StreamWriter(fs)

    '            '-- Write data to temp file
    '            'WriteSPPOData(sw, objSPStatus)

    '            sw.Close()
    '            fs.Close()

    '            imp.StopImpersonate()
    '            imp = Nothing

    '        End If

    '        '-- Download invoice data to client!
    '        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

    '    Catch ex As Exception
    '        MessageBox.Show("Download data gagal")
    '    End Try
    'End Sub

    Private Sub GetDocumentType()
        cmbDocumentType.Items.Add(New ListItem("Semua", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPDocumentTypeKTBDealer
            cmbDocumentType.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        cmbDocumentType.DataBind()
    End Sub
#End Region

#Region " Event Handler"

    Private Sub InitiateAuthorization()
        Dim ObjMainDealer As Dealer = CType(Session("DEALER"), Dealer)

        If ObjMainDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewSPPO_Status_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Status Pesanan")
            End If
            '--exclude  this privilege from Asra (BA)
            'Me.btnFind.Visible = SecurityProvider.Authorize(context.User, SR.SearchSPPO_Status_Privilege)

            _isShowDetailAllowed = SecurityProvider.Authorize(Context.User, SR.ViewSPPO_StatusDetail_Privilege)
            If _isPrintAllowed = False And _isShowDetailAllowed = False Then
                Me.dtgPOStatus.Columns(7).Visible = False
            End If

            txtDealerCode.Visible = False
            lblSearchDealer.Visible = False
            lblDealerCode.Visible = True

        Else
            If Not SecurityProvider.Authorize(Context.User, SR.ENHStatusPemesananKTB_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Status Pesanan")
            End If

            txtDealerCode.Visible = True
            lblSearchDealer.Visible = True
            lblDealerCode.Visible = False
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        Page.Server.ScriptTimeout = 900
        If Not IsPostBack Then
            BindDdlPaymentType()
            BindDdlCeilingStatus()
            icPODate.Value = Date.Today.AddDays(-1)
            icPODateTo.Value = Date.Today
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
            btnGetDealer.Style("display") = "none"
            ViewState("currSortColumn") = "POID"
            ViewState("currSortTable") = GetType(V_SparePartFlow)
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            GetDocumentType()
            BindHeader()
            dtgPOStatus.DataSource = New ArrayList
            dtgPOStatus.DataBind()
            If GetSessionCriteria() Then
                BindTodtgPOStatus(dtgPOStatus.CurrentPageIndex)
            End If

        End If
    End Sub

    Sub dtgPOStatus_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not e.Item.DataItem Is Nothing Then
            Dim objSparePartFlow As V_SparePartFlow
            objSparePartFlow = CType(_ListPOStatus(e.Item.ItemIndex), V_SparePartFlow)
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgPOStatus.PageSize * dtgPOStatus.CurrentPageIndex)).ToString
            e.Item.Cells(2).Text = objSparePartFlow.DealerCode

            Dim lnkPONumber As LinkButton = e.Item.FindControl("lnkPONumber")
            If objSparePartFlow.POID > 0 Then
                lnkPONumber.Visible = True
                lnkPONumber.Text = objSparePartFlow.PONumber
                lnkPONumber.Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                                "../SparePart/FrmSPPODetail.aspx?poid=" & objSparePartFlow.POID.ToString(), "", 600, 800, "null")
                '"../PopUp/PopUpSPPODetail.aspx?poid=" & objSparePartFlow.POID.ToString(), "", 600, 800, "null")
            End If
            Dim lnkSONumber As LinkButton = e.Item.FindControl("lnkSONumber")
            If objSparePartFlow.SOID > 0 Then
                lnkSONumber.Visible = True
                lnkSONumber.Text = objSparePartFlow.SONumber
                lnkSONumber.Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                    "../SparePart/FrmSPSODetail.aspx?SOID=" & objSparePartFlow.SOID.ToString(), "", 600, 800, "null")
                '"../SparePart/FrmPurchaseOrderEstimateDetail.aspx?SOID=" & objSparePartFlow.SOID.ToString(), "", 600, 800, "null")
                '
            End If
            Dim lnkDONumber As LinkButton = e.Item.FindControl("lnkDONumber")
            If objSparePartFlow.DOID > 0 Then
                lnkDONumber.Visible = True
                lnkDONumber.Text = objSparePartFlow.DONumber
                'lnkDONumber.Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                '        "../SparePart/frmSPDODetail.aspx?DOID=" & objSparePartFlow.DOID.ToString() + "&POID=" & objSparePartFlow.POID.ToString(), "", 600, 800, "null")
            End If
            Dim lnkBillingNumber As LinkButton = e.Item.FindControl("lnkBillingNumber")
            If objSparePartFlow.BillingID > 0 Then
                lnkBillingNumber.Visible = True
                lnkBillingNumber.Text = objSparePartFlow.BillingNumber
                lnkBillingNumber.Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                        "../SparePart/frmSPPOFakturDetail.aspx?BillingID=" & objSparePartFlow.BillingID.ToString() + "&POID=" & objSparePartFlow.POID.ToString() + "&DOID=" & objSparePartFlow.DOID.ToString(), "", 600, 800, "null")
            End If
            Dim lblStatus As Label = e.Item.FindControl("lblStatus")
            lblStatus.Text = EnumSparePartStatus.RetrieveName(objSparePartFlow.Status)

            Dim lblTOPCeilingStatus As Label = e.Item.FindControl("lblTOPCeilingStatus")
            If objSparePartFlow.TOPCeilingStatus = "Block" Then
                lblTOPCeilingStatus.Text = objSparePartFlow.TOPCeilingStatus
            Else
                Dim poBilling As SparePartBillingDetailFacade = New SparePartBillingDetailFacade(User)
                Dim poBillCrit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartBillingDetail), "SparePartDODetail.SparePartPOEstimate.SparePartPO.PONumber", MatchType.Exact, objSparePartFlow.PONumber))
                poBillCrit.opAnd(New Criteria(GetType(SparePartBillingDetail), "SparePartDODetail.SparePartPOEstimate.SparePartPO.TermOfPayment.ID", MatchType.No, 1))
                Dim poBillingData As Integer = poBilling.Retrieve(poBillCrit).Count
                If poBillingData > 0 Then
                    lblTOPCeilingStatus.Text = "OK"
                End If
            End If

            Dim imgStatus As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgStatus"), System.Web.UI.WebControls.Image)
            Select Case objSparePartFlow.Status
                Case EnumSparePartStatus.SparePartStatus.Delivery_Partial, EnumSparePartStatus.SparePartStatus.Delivery_Complete
                    Select Case GetStatus(objSparePartFlow)
                        Case 1
                            'e.Item.BackColor = Color.LightYellow
                            imgStatus.ToolTip = ""
                            imgStatus.ImageUrl = "../images/yellowbox.jpg"
                        Case 2
                            'e.Item.BackColor = Color.MediumVioletRed
                            imgStatus.ToolTip = ""
                            imgStatus.ImageUrl = "../images/redbox.jpg"
                    End Select
                Case EnumSparePartStatus.SparePartStatus.Received_Partial, EnumSparePartStatus.SparePartStatus.Received_Complete
                    'e.Item.BackColor = Color.LightGreen
                    imgStatus.ToolTip = ""
                    imgStatus.ImageUrl = "../images/greenbox.jpg"
                Case Else
                    imgStatus.ToolTip = ""
                    imgStatus.ImageUrl = "../images/whitebox.jpg"
            End Select

            e.Item.Cells(5).Text = Format(objSparePartFlow.PODate, "dd/MM/yyyy")
            e.Item.Cells(6).Text = objSparePartFlow.GetStrDate(objSparePartFlow.POSendDate, "dd/MM/yyyy") ' Format(objSparePartFlow.DeliveryDate, "dd/MM/yyyy")


            For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer 'jenis order
                If objSparePartFlow.OrderType.Equals(liOrderType.Value) Then
                    e.Item.Cells(3).Text = liOrderType.Text
                    Exit For
                End If
            Next
            For Each liOrderType As ListItem In LookUp.ArraySPDocumentTypeKTBDealer 'tipe dokumen
                If objSparePartFlow.DocumentType.Equals(liOrderType.Value) Then
                    e.Item.Cells(4).Text = liOrderType.Text
                    Exit For
                End If
            Next
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            SetSPPODetailButton(e)
        End If
    End Sub

    Private Function GetStatus(ByVal flow As V_SparePartFlow) As Integer
        Dim iReturn As Integer = 0
        Try
            Dim obj As New DealerLeadTime
            'obj.TransactionType
            Dim critLead As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerLeadTime), "Dealer.ID", MatchType.Exact, flow.DealerID))
            If ((Left(flow.OrderType, 1) = "R") Or (Left(flow.OrderType, 1) = "Z")) Then
                critLead.opAnd(New Criteria(GetType(DealerLeadTime), "TransactionType", MatchType.Exact, 0)) ' RO
            Else
                critLead.opAnd(New Criteria(GetType(DealerLeadTime), "TransactionType", MatchType.Exact, 1)) ' EO
            End If
            Dim objLead As DealerLeadTime
            Dim arlLead As ArrayList = New DealerLeadTimeFacade(User).Retrieve(critLead)
            If arlLead.Count > 0 Then
                objLead = CType(arlLead(0), DealerLeadTime)
            End If

            Dim strSql As String = ""
            strSql += " Select p.SparePartDOExpeditionID"
            strSql += " from SparePartPackingDetail pdet, SparePartPacking p"
            strSql += " where 1 = 1 And pdet.SparePartPackingID = p.ID And pdet.SparePartDOID = " & flow.DOID

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDOExpedition), "ID", MatchType.InSet, "(" & strSql & ")"))
            'criterias.opAnd(New Criteria(GetType(SparePartDOExpedition), "SparePartDO.ID", MatchType.Exact, doID))
            Dim arlExp As ArrayList = New SparePartDOExpeditionFacade(User).Retrieve(criterias)
            If arlExp.Count > 0 Then
                If Not IsNothing(objLead) AndAlso objLead.ID > 0 Then
                    For Each exp As SparePartDOExpedition In arlExp
                        Dim etaDate As DateTime = exp.ATD.AddDays(objLead.Value)
                        etaDate = New DateTime(etaDate.Year, etaDate.Month, etaDate.Day, 0, 0, 0)
                        Dim currDate As DateTime = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, 0, 0, 0)
                        If currDate <= etaDate Then
                            Return 1 ' < ETA
                        End If
                        If currDate > etaDate Then
                            Return 2 ' > ETA
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
        Return iReturn
    End Function

    Private Sub BindDdlPaymentType()
        Dim listOfPayments As ArrayList = New TermOfPaymentFacade(User).RetrieveActivePaymentTypeList()
        ddlTermOfPayment.DataSource = listOfPayments
        ddlTermOfPayment.DataValueField = "ID"
        ddlTermOfPayment.DataTextField = "Description"
        ddlTermOfPayment.DataBind()
        ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
    End Sub

    Private Sub BindDdlCeilingStatus()
        Dim listOfCeilingStatus As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("TOPCeilingStatus")
        ddlCeilingStatus.DataSource = listOfCeilingStatus
        ddlCeilingStatus.DataValueField = "ValueDesc"
        ddlCeilingStatus.DataTextField = "ValueDesc"
        ddlCeilingStatus.DataBind()
        ddlCeilingStatus.Items.Insert(0, New ListItem("Pilih Ceiling Status", ""))
    End Sub

    Private Sub SetSPPODetailButton(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not (_isPrintAllowed = False And _isShowDetailAllowed = False) Then
            CType(e.Item.FindControl("lblDetail"), Label).Visible = _isShowDetailAllowed
            CType(e.Item.FindControl("lblDetail"), Label).Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                    "../SparePart/frmSPPOStatusDetail.aspx?SPPOStatusID=" & e.Item.Cells(0).Text + "", "", 600, 800, "null")
            CType(e.Item.FindControl("lnkPrint"), LinkButton).Visible = _isPrintAllowed

        End If

        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If org.Title = EnumDealerTittle.DealerTittle.KTB Then
            CType(e.Item.FindControl("lblDetail"), Label).Visible = True
            CType(e.Item.FindControl("lblDetail"), Label).Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                    "../SparePart/frmSPPOStatusDetail.aspx?SPPOStatusID=" & e.Item.Cells(0).Text + "", "", 600, 800, "null")
            CType(e.Item.FindControl("lnkPrint"), LinkButton).Visible = False
            CType(e.Item.FindControl("lnkDownload"), LinkButton).Visible = False
        End If

    End Sub

    Private Sub dtgPOStatus_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPOStatus.SortCommand
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
            ViewState("currSortTable") = GetType(V_SparePartFlow)
            'Select Case CType(ViewState("currSortColumn"), String)
            '    Case "PONumber", "PODate", "OrderType"
            '        ViewState("currSortTable") = GetType(V_SparePartFlow)
            '    Case "SONumber", "PackingStatus", "DeliveryDate", "DocumentType"
            '        ViewState("currSortTable") = GetType(V_SparePartFlow)
            'End Select

        End If
        dtgPOStatus.CurrentPageIndex = 0
        BindTodtgPOStatus(dtgPOStatus.CurrentPageIndex)
    End Sub

    Private Sub dtgPOStatus_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPOStatus.PageIndexChanged
        dtgPOStatus.CurrentPageIndex = e.NewPageIndex
        BindTodtgPOStatus(dtgPOStatus.CurrentPageIndex)
    End Sub

    Private Sub dtgPOStatus_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPOStatus.ItemCommand
        Me._sessHelper.SetSession("frmPOStatus.PrevPage", Request.Url.ToString())
        SetSessionCriteria()
        If e.CommandName = "Download" Then
            'download(e.Item.Cells(0).Text)
            'ElseIf e.CommandName = "PODetail" Then
            '    Response.Redirect("FrmSPPODetail.aspx?POID=" & e.Item.Cells(0).Text)
            ''    Response.Redirect("../PopUp/PopUpSPPODetail.aspx?POID=" & e.Item.Cells(0).Text)
            'ElseIf e.CommandName = "SODetail" Then
            '    Response.Redirect("FrmPurchaseOrderEstimateDetail.aspx?SOID=" & e.Item.Cells(14).Text)
        ElseIf e.CommandName = "DODetail" Then
            Response.Redirect("FrmSPDODetail.aspx?DOID=" & e.Item.Cells(17).Text & "&POID=" & e.Item.Cells(0).Text)
            'ElseIf e.CommandName = "BillingDetail" Then
            '    Response.Redirect("frmSPPOFakturDetail.aspx?BillingID=" & e.Item.Cells(16).Text & "&POID=" & e.Item.Cells(0).Text & "&DOID=" & e.Item.Cells(15).Text)
        End If
    End Sub

    Private Sub btnGetDealer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDealer.Click
        If txtDealerCode.Text.Length > 0 Then
            Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
            lblDealerName.Text = ObjDealer.DealerName
            lblDealerTerm.Text = ObjDealer.SearchTerm2
        End If
    End Sub

#End Region

    Private Sub SetSessionCriteria()
        Dim objFlowList As ArrayList = New ArrayList
        objFlowList.Add(txtDealerCode.Text.Trim) '0
        objFlowList.Add(ddlJenisOrder.SelectedValue) '1
        objFlowList.Add(ddlPackingStatus.SelectedValue) '2
        objFlowList.Add(cmbDocumentType.SelectedValue) '3
        objFlowList.Add(chkPODate.Checked) '4
        objFlowList.Add(icPODate.Value) '5
        objFlowList.Add(icPODateTo.Value) '6
        objFlowList.Add(chkSODate.Checked) '7
        objFlowList.Add(icSODate.Value) '8
        objFlowList.Add(icSODateTo.Value) '9
        objFlowList.Add(txtPONumber.Text.Trim) '10
        objFlowList.Add(txtSONumber.Text.Trim) '11
        objFlowList.Add(txtDONumber.Text.Trim) '12
        objFlowList.Add(txtFakturNumber.Text.Trim) '13
        objFlowList.Add(CType(ViewState("CurrentSortColumn"), String)) '14
        objFlowList.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection)) '15

        _sessHelper.SetSession("FrmSPPOStatus.SessCriteria", objFlowList)
    End Sub


    Private Function GetSessionCriteria() As Boolean
        Dim objFlowList As ArrayList = _sessHelper.GetSession("FrmSPPOStatus.SessCriteria")
        If Not objFlowList Is Nothing Then
            txtDealerCode.Text = objFlowList.Item(0)
            ddlJenisOrder.SelectedValue = objFlowList.Item(1)
            ddlPackingStatus.SelectedValue = objFlowList.Item(2)
            cmbDocumentType.SelectedValue = objFlowList.Item(3)
            chkPODate.Checked = objFlowList.Item(4)
            icPODate.Value = objFlowList.Item(5)
            icPODateTo.Value = objFlowList.Item(6)
            chkSODate.Checked = objFlowList.Item(7)
            icSODate.Value = objFlowList.Item(8)
            icSODateTo.Value = objFlowList.Item(9)
            txtPONumber.Text = objFlowList.Item(10)
            txtSONumber.Text = objFlowList.Item(11)
            txtDONumber.Text = objFlowList.Item(12)
            txtFakturNumber.Text = objFlowList.Item(13)
            ViewState("CurrentSortColumn") = objFlowList.Item(14)
            ViewState("CurrentSortDirect") = objFlowList.Item(15)
            Return True
        End If
        Return False
    End Function
End Class