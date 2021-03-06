#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class UpdatePaymentStatus
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents txtRegNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSoNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents cbxTanggalProses As System.Web.UI.WebControls.CheckBox
    Protected WithEvents calGyroDate1 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calGyroDate2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSlipNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents cbxTanggalGiro As System.Web.UI.WebControls.CheckBox
    Protected WithEvents calBaselineDate1 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calBaselineDate2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPurpose As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTOP As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSAPCreator As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblFactoring As System.Web.UI.WebControls.Label
    Protected WithEvents lblFactoringColon As System.Web.UI.WebControls.Label
    Protected WithEvents ddLFactoring As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlGyroType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatusDP As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlIsTransfered As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlRemarkStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTotalNilaiValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents dgDailyPayment As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlAction As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadDealer As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidate As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelValidate As System.Web.UI.WebControls.Button
    Protected WithEvents btnTranfer As System.Web.UI.WebControls.Button
    Protected WithEvents btnTransferUlang As System.Web.UI.WebControls.Button
    Protected WithEvents btnTransferAcc As System.Web.UI.WebControls.Button
    Protected WithEvents btnTransferAccUlang As System.Web.UI.WebControls.Button

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
    Private _arrDailyPayment As ArrayList
    Private _arrDailyPaymentTotal As ArrayList
    Private sessionHelper As New SessionHelper
    Private objDealer As Dealer
    Private _sessOldCalendarValue As String = "UpdatePaymentStatus.OldCalendarValue"
    Private CompanyCode As String = ""
#End Region

#Region "Custom Method"


    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList

        objSSPO.Add(txtKodeDealer.Text)
        objSSPO.Add(cbxTanggalProses.Checked)
        objSSPO.Add(calGyroDate1.Value)
        objSSPO.Add(calGyroDate2.Value)
        objSSPO.Add(cbxTanggalGiro.Checked)

        objSSPO.Add(calBaselineDate1.Value)
        objSSPO.Add(calBaselineDate2.Value)
        'objSSPO.Add(txtPoNumber.Text)
        objSSPO.Add(txtSlipNumber.Text)
        objSSPO.Add(ddlPurpose.SelectedIndex)
        objSSPO.Add(ddlStatusDP.SelectedIndex)

        objSSPO.Add(dgDailyPayment.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        objSSPO.Add(Me.ddlCategory.SelectedIndex)
        objSSPO.Add(Me.ddlTOP.SelectedIndex)

        objSSPO.Add(Me.ddLFactoring.SelectedIndex)
        objSSPO.Add(Me.txtSoNumber.Text)
        objSSPO.Add(Me.txtSAPCreator.Text)
        objSSPO.Add(Me.ddlGyroType.SelectedIndex)

        objSSPO.Add(Me.ddlRemarkStatus.SelectedIndex)

        sessionHelper.SetSession("SESSIONPAYMENTSTATUS", objSSPO)
    End Sub

    Private Sub GetSessionCriteria()
        Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONPAYMENTSTATUS")
        If Not objSSPO Is Nothing Then
            txtKodeDealer.Text = objSSPO.Item(0)
            cbxTanggalProses.Checked = objSSPO.Item(1)

            calGyroDate1.Value = objSSPO.Item(2)
            calGyroDate2.Value = objSSPO.Item(3)

            cbxTanggalGiro.Checked = objSSPO.Item(4)

            calBaselineDate1.Value = objSSPO.Item(5)
            calBaselineDate2.Value = objSSPO.Item(6)

            'txtPoNumber.Text = objSSPO.Item(7)
            txtSlipNumber.Text = objSSPO.Item(7)
            ddlPurpose.SelectedIndex = objSSPO.Item(8)
            ddlStatusDP.SelectedIndex = objSSPO.Item(9)
            dgDailyPayment.CurrentPageIndex = objSSPO.Item(10)
            ViewState("CurrentSortColumn") = objSSPO.Item(11)
            ViewState("CurrentSortDirect") = objSSPO.Item(12)

            Me.ddlCategory.SelectedIndex = objSSPO.Item(13)
            Me.ddlTOP.SelectedIndex = objSSPO.Item(14)
            Me.ddLFactoring.SelectedIndex = objSSPO.Item(15)
            Me.txtSoNumber.Text = objSSPO.Item(16)
            Me.txtSAPCreator.Text = objSSPO.Item(17)
            Me.ddlGyroType.SelectedIndex = objSSPO.Item(18)
            Me.ddlRemarkStatus.SelectedIndex = objSSPO.Item(19)

        End If
    End Sub


    Private Sub BindDataGrid(ByVal currentPageIndex As Integer, Optional ByVal IsRetrieveAll As Boolean = False)
        If cbxTanggalGiro.Checked = False AndAlso cbxTanggalProses.Checked = False Then
            MessageBox.Show("Tanggal Proses, Tanggal Jatuh Tempo atau Tanggal Efektif harus dipilih")
            Return
        End If
        Dim total As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'add by anh 20161002, req by yurike for payment trasfer
        'start by anh 20161002
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.IsTransfer", MatchType.Exact, 0))
        'end by anh 20161002

        If CType(Session("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB AndAlso Me.IsShowAcceleration = False Then
        Else
            'start  :by:dna;for:bowo;on:20111011;remark:remove this filter, confirmed by sriyono
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "IsReversed", MatchType.Exact, 0))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "IsCleared", MatchType.Exact, 0))
            'end    :by:dna;for:bowo;on:20111011;remark:remove this filter, confirmed by sriyono
        End If
        If ddLFactoring.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.IsFactoring", MatchType.Exact, ddLFactoring.SelectedValue))
        End If

        If ddlStatusDP.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "Status", MatchType.Exact, ddlStatusDP.SelectedValue))
        End If

        If txtSoNumber.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.SONumber", MatchType.StartsWith, Me.txtSoNumber.Text.Trim))
        End If

        If ddlGyroType.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "GyroType", MatchType.Exact, ddlGyroType.SelectedValue))
        End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        'If ddlStatusDP.SelectedIndex > 0 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "RejectStatus", MatchType.Exact, ddlStatusDP.SelectedValue))
        If txtKodeDealer.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        'If txtPoNumber.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.PONumber", MatchType.StartsWith, txtPoNumber.Text))
        If txtSlipNumber.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "SlipNumber", MatchType.Exact, txtSlipNumber.Text))
        If txtSAPCreator.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "SAPCreator", MatchType.[Partial], txtSAPCreator.Text))

        'If ddlTOP.SelectedValue = 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.TermOfPayment.TermOfPaymentValue", MatchType.Exact, 0))
        'ElseIf ddlTOP.SelectedValue = 1 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.TermOfPayment.TermOfPaymentValue", MatchType.No, 0))
        'End If
        If ddlTOP.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, CType(ddlTOP.SelectedValue, Short)))
        End If

        If cbxTanggalProses.Checked Then
            If CType(calGyroDate1.Value, Date) <= CType(calGyroDate2.Value, Date) Then
                Dim TanggalAwal As New DateTime(CInt(calGyroDate1.Value.Year), CInt(calGyroDate1.Value.Month), CInt(calGyroDate1.Value.Day), 0, 0, 0)
                Dim TanggalAkhir As New DateTime(CInt(calGyroDate2.Value.Year), CInt(calGyroDate2.Value.Month), CInt(calGyroDate2.Value.Day), 23, 59, 59)
                'Dim Time As TimeSpan = TanggalAkhir.Subtract(TanggalAwal)
                'If Time.Days <= 65 Then
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, Format(TanggalAkhir, "yyyy-MM-dd HH:mm:ss")))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.ReqAllocationDateTime", MatchType.Greater, tanggalawal.AddDays(-1).ToString("yyyy.MM.dd")))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.ReqAllocationDateTime", MatchType.Lesser, tanggalakhir.AddDays(1).ToString("yyyy.MM.dd")))

                'Start:Optimized
                If TanggalAwal.Year >= 2011 Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "ID", MatchType.Greater, 244156))
                End If
                'End:Optimized
                'Else
                '    MessageBox.Show("Periode Melebihi 65 Hari")
                'end if
            Else
                MessageBox.Show("Tanggal Mulai Harus Lebih Kecil Sama Dengan Tanggal Akhir")
                Return
            End If
        End If

        If cbxTanggalGiro.Checked Then
            If CType(calBaselineDate1.Value, Date) <= CType(calBaselineDate2.Value, Date) Then
                Dim TanggalAwal As New DateTime(CInt(calBaselineDate1.Value.Year), CInt(calBaselineDate1.Value.Month), CInt(calBaselineDate1.Value.Day), 0, 0, 0)
                Dim TanggalAkhir As New DateTime(CInt(calBaselineDate2.Value.Year), CInt(calBaselineDate2.Value.Month), CInt(calBaselineDate2.Value.Day), 23, 59, 59)
                'Dim Time As TimeSpan = TanggalAkhir.Subtract(TanggalAwal)
                'If Time.Days <= 65 Then
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "BaselineDate", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "BaselineDate", MatchType.LesserOrEqual, Format(TanggalAkhir, "yyyy-MM-dd HH:mm:ss")))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "BaselineDate", MatchType.Greater, tanggalawal.AddDays(-1).ToString("yyyy.MM.dd")))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "BaselineDate", MatchType.Lesser, TanggalAkhir.AddDays(1).ToString("yyyy.MM.dd")))

                'Else
                '    MessageBox.Show("Periode Melebihi 65 Hari")
                'end if 
            Else
                MessageBox.Show("Tanggal Mulai Harus Lebih Kecil Sama Dengan Tanggal Akhir")
                Return
            End If
        End If


        If ddlPurpose.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "PaymentPurpose.ID", MatchType.Exact, ddlPurpose.SelectedValue))
        If Me.txtRegNumber.Text.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "DailyPaymentHeader.RegNumber", MatchType.StartsWith, Me.txtRegNumber.Text.Trim))
        If Me.ddlCategory.SelectedValue <> -1 Then
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.ContractHeader.Category.ID", MatchType.Exact, CType(Me.ddlCategory.SelectedValue, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "POHeader.ContractHeader.Category.CategoryCode", MatchType.Exact, Me.ddlCategory.SelectedItem.Text))
        End If
        If Me.ddlIsTransfered.SelectedIndex <> 0 Then
            If CType(Me.ddlIsTransfered.SelectedValue, Short) = 1 Then
                criterias.opAnd(New Criteria(GetType(DailyPayment), "NumOfTransfered", MatchType.Greater, 0))
            ElseIf CType(Me.ddlIsTransfered.SelectedValue, Short) = 2 Then
                criterias.opAnd(New Criteria(GetType(DailyPayment), "NumOfTransfered", MatchType.Lesser, 1))
            End If
        End If
        If Me.ddlRemarkStatus.SelectedIndex <> 0 Then
            Dim RemarkStatus As Integer = Me.ddlRemarkStatus.SelectedValue
            If RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.Cleared OrElse RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.NotCleared Then
                criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.Exact, 0), "((", True)
                criterias.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.Exact, CType(Me.ddlRemarkStatus.SelectedValue, Integer)), ")", False)
                criterias.opOr(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.Exact, 1), "(", True)
                criterias.opAnd(New Criteria(GetType(DailyPayment), "RemarkStatus", MatchType.Exact, CType(Me.ddlRemarkStatus.SelectedValue, Integer)), "))", False)
            Else
                criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.Exact, 1))
                criterias.opAnd(New Criteria(GetType(DailyPayment), "RemarkStatus", MatchType.Exact, CType(Me.ddlRemarkStatus.SelectedValue, Integer)))
            End If
        End If

        Dim oDPFac As New DailyPaymentFacade(User)
        If Not IsRetrieveAll Then
            _arrDailyPayment = oDPFac.RetrieveActiveList(criterias, currentPageIndex + 1, dgDailyPayment.PageSize, _
                        total, CType(ViewState("CurrentSortColumn"), String), _
                        CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            Dim aggDP As Aggregate = New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
            'removed start  ;for:bowo;by:dna;on:20111213
            'criterias.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, 1))
            'criterias.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, 1))
            'removed end    ;for:bowo;by:dna;on:20111213


            Dim TotalAmount As Decimal = oDPFac.GetAggregateResult(aggDP, criterias)
            sessionHelper.SetSession("TotalAmount", TotalAmount)
            If _arrDailyPayment.Count = 0 Then
                btnDownload.Visible = False
                MessageBox.Show("Data Tidak Ditemukan")
            Else
                dgDailyPayment.DataSource = _arrDailyPayment
                btnDownload.Visible = True
            End If
            sessionHelper.SetSession("ArlDailyPayment", _arrDailyPayment)
            dgDailyPayment.VirtualItemCount = total
        Else
            '_arrDailyPaymentTotal = oDPFac.Retrieve(criterias)
            Dim oSorts As New SortCollection
            oSorts.Add(New Sort(GetType(DailyPayment), "POHeader.Dealer.DealerCode", Sort.SortDirection.ASC))
            oSorts.Add(New Sort(GetType(DailyPayment), "POHeader.PONumber", Sort.SortDirection.ASC))
            '_arrDailyPayment = oDPFac.RetrieveByCriteria(criterias, oSorts, currentPageIndex + 1, dgDailyPayment.PageSize, total)
            _arrDailyPaymentTotal = oDPFac.Retrieve(criterias, oSorts)
            sessionHelper.SetSession("ArlDailyPaymentAll", _arrDailyPaymentTotal)
        End If
        'End    :Factoring:group by Gyro
    End Sub

    Private Sub BindRemarkStatus()
        With Me.ddlRemarkStatus.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each oLI As ListItem In EnumPaymentRemarkStatus.GetList
                .Add(oLI)
            Next
        End With
    End Sub

    Private Function PopulateDailyTransferData() As ArrayList
        Dim IsAll As Boolean = Me.IsAllDataSelected
        Dim oExArgs As ArrayList

        If IsAll Then
            Me.BindDataGrid(Me.dgDailyPayment.CurrentPageIndex, True)
            oExArgs = sessionHelper.GetSession("ArlDailyPaymentAll")
        Else
            oExArgs = sessionHelper.GetSession("ArlDailyPayment")
        End If

        Return oExArgs
    End Function

    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Pembayaran")
        End If

        'Start  : RemainModule-Privilege
        If Not Request.Item("ShowAcceleration") Is Nothing Then
            If CType(Request.Item("ShowAcceleration"), String) = "1" Then
                If Not SecurityProvider.Authorize(Context.User, SR.Daftar_pembayaran_ceiling_privilege) Then
                    Response.Redirect("../frmAccessDenied.aspx?modulName=Sales Credit Control - Daftar Pembayaran")
                End If
            End If
        End If
        'End    : RemainModule-Privilege

        btnDownload.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranDownload_Privilege)
        btnProses.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranStatusTolakan_Privilege)
        ddlAction.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranStatusTolakan_Privilege)
        Label11.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranStatusTolakan_Privilege)
        txtSAPCreator.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranNomorAccounting_Privilege)

        Dim oD As Dealer = CType(Session("DEALER"), Dealer)
        Dim IsAllowTransfer As Boolean = (oD.Title = EnumDealerTittle.DealerTittle.KTB)
        Me.btnTranfer.Visible = False ' IsAllowTransfer
        Me.btnTransferUlang.Visible = False ' IsAllowTransfer
        Me.btnTransferAcc.Visible = False 'IsAllowTransfer
        Me.btnTransferAccUlang.Visible = False 'IsAllowTransfer

        btnDownloadDealer.Visible = SecurityProvider.Authorize(Context.User, SR.po_pembayaran_download_report_privilege)
        btnValidate.Visible = SecurityProvider.Authorize(Context.User, SR.po_pembayaran_validasi_privilege)
        btnCancelValidate.Visible = btnValidate.Visible

        If Not IsNothing(Request.Item("ShowAcceleration")) AndAlso Request.Item("ShowAcceleration") = "1" Then
            Me.btnTransferAcc.Visible = SecurityProvider.Authorize(Context.User, SR.po_pembayaran_transfer_percepatan_privilege)
            Me.btnTransferAccUlang.Visible = Me.btnTransferAcc.Visible
        Else
            Me.btnTranfer.Visible = SecurityProvider.Authorize(Context.User, SR.po_pembayaran_transfer_privilege)
            Me.btnTransferUlang.Visible = Me.btnTranfer.Visible
        End If
    End Sub

    Private Sub UpdateDPStatus(ByVal NewStatus As Short, ByVal OldStatus As Short)
        Dim arlToValidate As New ArrayList
        Dim sBatal As String = IIf(NewStatus = EnumPaymentStatus.PaymentStatus.Validasi, "", "Batal ")
        Dim IsAll As Boolean = Me.IsAllDataSelected
        Dim Idx As Integer
        Dim arlData As ArrayList
        Dim IsAllowToProcess As Boolean

        If IsAll Then
            Me.BindDataGrid(Me.dgDailyPayment.CurrentPageIndex, True)
            arlData = Me.sessionHelper.GetSession("ArlDailyPaymentAll")
        Else
            arlData = Me.sessionHelper.GetSession("ArlDailyPayment")
        End If

        For Idx = 0 To arlData.Count - 1
            If IsAll Or (Idx <= Me.dgDailyPayment.Items.Count - 1 AndAlso CType(Me.dgDailyPayment.Items(Idx).FindControl("chkExport"), CheckBox).Checked) Then
                Dim oDP As DailyPayment = CType(arlData(Idx), DailyPayment)
                If oDP.Status = OldStatus Then
                    CompanyCode = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                    If oDP.POHeader.ContractHeader.Category.ProductCategory.Code.Trim <> CompanyCode AndAlso CompanyCode = "MMC" Then
                        MessageBox.Show(sBatal & "Validasi Gagal, SO tidak terdapat pada Kategori Produk " & CompanyCode)
                        Exit Sub
                    Else
                        arlToValidate.Add(oDP)
                    End If
                Else
                    MessageBox.Show(sBatal & "Validasi Gagal, " & sBatal & "Validasi hanya untuk data dengan status " & EnumPaymentStatus.GetStringValue(OldStatus))
                    Exit Sub
                End If
            End If
        Next

        '_arrDailyPayment = sessionHelper.GetSession("ArlDailyPayment")
        'For Each di As DataGridItem In Me.dgDailyPayment.Items
        '    Dim chkExport As CheckBox = di.FindControl("chkExport")
        '    If chkExport.Checked Then
        '        Dim oDP As DailyPayment = CType(_arrDailyPayment(di.ItemIndex), DailyPayment)
        '        If oDP.Status = OldStatus Then
        '            arlToValidate.Add(oDP)
        '        Else
        '            MessageBox.Show(sBatal & "Validasi Gagal, " & sBatal & "Validasi hanya untuk data dengan status " & EnumPaymentStatus.GetStringValue(OldStatus))
        '            Exit Sub
        '        End If
        '    End If
        'Next

        Try
            Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
            Dim histFacade As New StatusChangeHistoryFacade(User)
            Dim arlHist As New ArrayList

            For Each oDP As DailyPayment In arlToValidate
                If NewStatus = EnumPaymentStatus.PaymentStatus.Validasi Then
                    Dim objHist As New StatusChangeHistory
                    objHist.DocumentType = LookUp.DocumentType.Gyro
                    objHist.DocumentRegNumber = oDP.ID
                    objHist.OldStatus = odp.Status
                    objHist.NewStatus = NewStatus
                    arlHist.Add(objHist)
                End If

                odp.Status = NewStatus
            Next
            oDPFac.Update(arlToValidate)
            histFacade.Insert(arlHist)

            MessageBox.Show(sBatal & "Validasi sukses")
        Catch ex As Exception
            MessageBox.Show(sBatal & "Validasi gagal")
        End Try
        Me.BindGrid()
    End Sub

    Private Function IsDataValidBasedValidation(ByRef arlToTransfer As ArrayList, Optional ByRef sErrMessage As String = "") As Boolean
        For Each oDP As DailyPayment In arlToTransfer

        Next
    End Function

    Private Function GetSOValue(ByVal poHeader As POHeader, ByVal purposed As PaymentPurpose) As Double
        Select Case purposed.ID
            Case 1
                Return poHeader.TotalHargaIT
            Case 2
                Return poHeader.TotalHargaPP
            Case 3
                Return poHeader.TotalHarga
            Case 6
                Return poHeader.TotalHarga + poHeader.TotalHargaPP
            Case 7
                Return poHeader.TotalHarga + poHeader.TotalHargaPP + poHeader.TotalHargaIT
        End Select
    End Function

    Private Function IsAllDataSelected() As Boolean
        For Each di As DataGridItem In Me.dgDailyPayment.Items
            If CType(di.FindControl("chkExport"), CheckBox).Checked = False Then Return False
        Next
        Return True
    End Function

    Private Function ConvertToGroupedRegNumber(ByVal aData As ArrayList, ByRef sError As String) As ArrayList
        Dim aDPH As New ArrayList
        Dim aDP As New ArrayList
        Dim nValidated As Integer

        sError = ""

        For Each oDP As DailyPayment In aData
            If Not IsExist(aDPH, oDP.DailyPaymentHeader.ID) Then
                aDPH.Add(oDP.DailyPaymentHeader)
            End If
        Next
        aDPH = CommonFunction.SortArraylist(aDPH, GetType(DailyPaymentHeader), "RegNumber", Sort.SortDirection.ASC)
        For Each oDPH As DailyPaymentHeader In aDPH
            nValidated = 0
            For Each oDP As DailyPayment In oDPH.DailyPayments
                If odp.Status = EnumPaymentStatus.PaymentStatus.Validasi Then nValidated += 1
                aDP.Add(odp)
            Next
            If nValidated = 0 Then
                sError = "Status Gyro " & oDPH.RegNumber & " bukan Validasi"
                Return aDP
            ElseIf nValidated < oDPH.DailyPayments.Count Then
                sError = (oDPH.DailyPayments.Count - nValidated).ToString & " dari " & oDPH.DailyPayments.Count.ToString & " Assignment dari Gyro" & oDPH.RegNumber & " tidak berstatus Validasi "
                Return aDP
            End If
        Next
        Return aDP
    End Function

    Private Function IsExist(ByRef aDPH As ArrayList, ByVal DPHID As Integer) As Boolean
        For Each oDPH As DailyPaymentHeader In aDPH
            If oDPH.ID = DPHID Then Return True
        Next
        Return False
    End Function

    Private Function GetDataToTransfer(ByVal sDataType As String, ByVal sTransType As String, ByRef sError As String) As ArrayList
        Dim arlToTransfer As New ArrayList
        Dim nChecked As Integer = 0
        Dim sErrorStatus As String = ""
        Dim sErrorSO As String = ""
        Dim sErrorNormal As String = ""
        Dim sErrorUlang As String = ""
        Dim IsValid As Boolean
        Dim IsAll As Boolean = Me.IsAllDataSelected
        Dim idx As Integer
        Dim arlData As ArrayList
        Dim aSents As New ArrayList
        Dim aNotSents As New ArrayList

        If IsAll Then
            Me.BindDataGrid(Me.dgDailyPayment.CurrentPageIndex, True)
            arlData = sessionHelper.GetSession("ArlDailyPaymentAll")
        Else
            arlData = sessionHelper.GetSession("ArlDailyPayment")
            Dim aTemp As New ArrayList
            For Each di As DataGridItem In Me.dgDailyPayment.Items
                Dim chkExport As CheckBox = di.FindControl("chkExport")
                If chkExport.Checked Then
                    aTemp.Add(arlData(di.ItemIndex))
                End If
            Next
            arlData = aTemp
        End If

        sError = ""
        arlData = ConvertToGroupedRegNumber(arlData, sError)
        If sError <> String.Empty Then
            Return New ArrayList
        End If

        sError = ""
        For Each oDP As DailyPayment In arlData
            IsValid = True

            If oDP.POHeader.SONumber = "" Then
                sError &= IIf(sError = "", "Transfer Gagal, SO belum didownload : ", ";") & oDP.POHeader.DealerPONumber
                IsValid = False
            End If

            CompanyCode = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            If oDP.POHeader.ContractHeader.Category.ProductCategory.Code.Trim <> CompanyCode AndAlso CompanyCode = "MMC" Then
                sError &= IIf(sError = "", "Transfer Gagal, SO tidak terdapat pada Kategori Produk " & CompanyCode & " : ", ";") & oDP.POHeader.DealerPONumber
                IsValid = False
            End If

            If IsValid Then
                Select Case sDataType.Trim.ToUpper
                    Case "Normal".ToUpper
                        If oDP.GyroType <> EnumGyroType.GyroType.Normal Then
                            sError = IIf(sError = "", "Transfer Gagal, Pembayaran Percepatan : ", ";") & oDP.POHeader.DealerPONumber
                            IsValid = False
                        Else
                            If sTransType.Trim.ToUpper = "Ulang".ToUpper Then
                                If oDP.NumOfTransfered < 1 Then
                                    'sError &= IIf(sError = "", "Transfer Gagal, Pembayaran Belum ditransfer: ", ";") & oDP.POHeader.DealerPONumber
                                    sError &= IIf(sError = "", "Transfer Gagal. Data yang belum pernah ditransfer tidak dapat dilakukan proses 'Transfer Ulang'. No Gyro :  ", ";") & oDP.SlipNumber
                                    IsValid = False
                                End If
                            ElseIf sTransType.Trim.ToUpper = "Normal".ToUpper Then
                                If oDP.NumOfTransfered > 0 Then
                                    sError &= IIf(sError = "", "Transfer Gagal. Data yang sudah pernah ditransfer, hanya dapat dilakukan proses 'Transfer Ulang'. No Gyro : ", ";") & oDP.SlipNumber
                                    IsValid = False
                                End If
                            End If
                        End If
                    Case "Accelerate".ToUpper
                        If oDP.GyroType = EnumGyroType.GyroType.Normal Then
                            sError &= IIf(sError = "", "Transfer Gagal, Pembayaran Normal : ", ";") & oDP.POHeader.DealerPONumber
                            IsValid = False
                        Else
                            If sTransType.Trim.ToUpper = "Ulang".ToUpper Then
                                If oDP.NumOfTransfered < 1 Then
                                    'sError &= IIf(sError = "", "Transfer Gagal, Pembayaran Belum ditransfer: ", ";") & oDP.POHeader.DealerPONumber
                                    sError &= IIf(sError = "", "Transfer Gagal. Data yang belum pernah ditransfer tidak dapat dilakukan proses 'Transfer Ulang'. No Gyro :  ", ";") & oDP.SlipNumber
                                    IsValid = False
                                End If
                            ElseIf sTransType.Trim.ToUpper = "Normal".ToUpper Then
                                If oDP.NumOfTransfered > 0 Then
                                    'sError &= IIf(sError = "", "Transfer Gagal, Pembayaran Sudah ditransfer: ", ";") & oDP.POHeader.DealerPONumber
                                    sError &= IIf(sError = "", "Transfer Gagal. Data yang sudah pernah ditransfer, hanya dapat dilakukan proses 'Transfer Ulang'. No Gyro : ", ";") & oDP.SlipNumber
                                    IsValid = False
                                End If
                            End If
                        End If
                End Select
            End If
            If IsValid = False Then Exit For
        Next

        If IsValid = False Then
            Return New ArrayList
        Else
            Return arlData
        End If
        'sError = ""
        'idx = 0
        'For idx = 0 To arlData.Count - 1
        '    If IsAll Or (idx <= Me.dgDailyPayment.Items.Count - 1 AndAlso CType(Me.dgDailyPayment.Items(idx).FindControl("chkExport"), CheckBox).Checked) Then
        '        Dim oDP As DailyPayment = CType(arlData(idx), DailyPayment)

        '        IsValid = True
        '        nChecked += 1
        '        If oDP.Status <> EnumPaymentStatus.PaymentStatus.Validasi Then
        '            sErrorStatus &= IIf(sErrorStatus = "", "Transfer Gagal, Status Pembayaran bukan Validasi: ", ";") & oDP.POHeader.DealerPONumber
        '            IsValid = False
        '        End If
        '        If IsValid AndAlso oDP.POHeader.SONumber = "" Then
        '            sErrorSO &= IIf(sErrorSO = "", "Transfer Gagal, SO belum didownload : ", ";") & oDP.POHeader.DealerPONumber
        '            IsValid = False
        '        End If
        '        If IsValid Then
        '            Select Case sDataType.Trim.ToUpper
        '                Case "Normal".ToUpper
        '                    If oDP.GyroType <> EnumGyroType.GyroType.Normal Then
        '                        'sErrorNormal &= IIf(sErrorNormal = "", "Transfer Gagal, Pembayaran Percepatan : ", ";") & oDP.POHeader.DealerPONumber
        '                        IsValid = False
        '                    Else
        '                        If sTransType.Trim.ToUpper = "Ulang".ToUpper Then
        '                            If oDP.NumOfTransfered < 1 Then
        '                                'sErrorUlang &= IIf(sErrorUlang = "", "Transfer Gagal, Pembayaran Belum ditransfer: ", ";") & oDP.POHeader.DealerPONumber
        '                                IsValid = False
        '                            End If
        '                        ElseIf sTransType.Trim.ToUpper = "Normal".ToUpper Then
        '                            If oDP.NumOfTransfered > 0 Then
        '                                'sErrorUlang &= IIf(sErrorUlang = "", "Transfer Gagal, Pembayaran Sudah ditransfer: ", ";") & oDP.POHeader.DealerPONumber
        '                                IsValid = False
        '                            End If
        '                        End If
        '                    End If
        '                Case "Accelerate".ToUpper
        '                    If oDP.GyroType = EnumGyroType.GyroType.Normal Then
        '                        'sErrorNormal &= IIf(sErrorNormal = "", "Transfer Gagal, Pembayaran Normal : ", ";") & oDP.POHeader.DealerPONumber
        '                        IsValid = False
        '                    Else
        '                        If sTransType.Trim.ToUpper = "Ulang".ToUpper Then
        '                            If oDP.NumOfTransfered < 1 Then
        '                                'sErrorUlang &= IIf(sErrorUlang = "", "Transfer Gagal, Pembayaran Belum ditransfer: ", ";") & oDP.POHeader.DealerPONumber
        '                                IsValid = False
        '                            End If
        '                        ElseIf sTransType.Trim.ToUpper = "Normal".ToUpper Then
        '                            If oDP.NumOfTransfered > 0 Then
        '                                'sErrorUlang &= IIf(sErrorUlang = "", "Transfer Gagal, Pembayaran Sudah ditransfer: ", ";") & oDP.POHeader.DealerPONumber
        '                                IsValid = False
        '                            End If
        '                        End If
        '                    End If
        '            End Select
        '        End If
        '        If IsValid Then arlToTransfer.Add(oDP)
        '    End If
        'Next

        'If sErrorStatus <> "" AndAlso sErrorSO <> "" Then
        '    sError = sErrorStatus & ". " & sErrorSO
        'ElseIf sErrorStatus <> "" AndAlso sErrorSO = "" Then
        '    sError = sErrorStatus
        'ElseIf sErrorStatus = "" AndAlso sErrorSO <> "" Then
        '    sError = sErrorSO
        'ElseIf sErrorStatus = "" AndAlso sErrorSO = "" Then
        '    If sErrorNormal <> "" AndAlso sErrorUlang <> "" Then
        '        sError = sErrorNormal & ". " & sErrorUlang
        '    ElseIf sErrorNormal <> "" AndAlso sErrorUlang = "" Then
        '        sError = sErrorNormal
        '    ElseIf sErrorNormal = "" AndAlso sErrorUlang <> "" Then
        '        sError = sErrorUlang
        '    End If
        'End If


        'If arlToTransfer.Count = 0 AndAlso nChecked = 0 Then
        '    sError = "Tidak ada data yang akan ditransfer"
        'End If
        'Return arlToTransfer
    End Function

    Private Sub TransferToSAP(ByVal sDataType As String, ByVal sTransType As String)
        Dim arlToTransfer As New ArrayList
        Dim sError As String = ""
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)


        arlToTransfer = Me.GetDataToTransfer(sDataType, sTransType, sError)
        If sError.Trim <> "" Then
            MessageBox.Show(sError)
            Exit Sub
        End If

        'Urut berdasarkan dealer,slipnumber,paymentpurpose,baselinedate (key Giro)
        If arlToTransfer.Count < 1 Then
            MessageBox.Show("Tidak ada data yg ditransfer")
            Exit Sub
        End If
        Dim _fileHelper As New FileHelper
        Dim str As FileInfo
        Dim PreFolder As String
        Dim oUI As UserInfo = CType(Me.sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)

        Try
            PreFolder = oUI.UserName
            str = _fileHelper.TransferGyroToSAP(sDataType, arlToTransfer, PreFolder)
            For Each oDP As DailyPayment In arlToTransfer
                oDP.NumOfTransfered += 1
                oDPFac.Update(oDP)
            Next
            MessageBox.Show(SR.UploadSucces(str.Name))
            Me.btnFind_Click(Nothing, Nothing) 'Update data from db'NumOfTransfered were changed
        Catch ex As Exception
            MessageBox.Show(SR.UploadFail(str.Name))
        End Try
        'Dim strId As String = ""
        'For Each item As DailyPayment In arlToTransfer
        '    strId &= "," & item.ID.ToString
        'Next
        'strId = Right(strId, strId.Length - 1)

        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "ID", MatchType.InSet, "(" & strId & ")"))
        'Dim oSorts As New SortCollection
        'oSorts.Add(New Sort(GetType(DailyPayment), "POHeader.Dealer.ID"))
        'oSorts.Add(New Sort(GetType(DailyPayment), "SlipNumber"))
        'oSorts.Add(New Sort(GetType(DailyPayment), "PaymentPurpose.ID"))
        'oSorts.Add(New Sort(GetType(DailyPayment), "BaselineDate"))

        'arlToTransfer = oDPFac.Retrieve(criterias, oSorts)

        'If arlToTransfer.Count > 0 Then
        '    Dim _fileHelper As New FileHelper
        '    Dim str As FileInfo
        '    Dim PreFolder As String
        '    Dim oUI As UserInfo = CType(Me.sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)

        '    Try
        '        PreFolder = oUI.UserName
        '        str = _fileHelper.TransferGyroToSAP(sDataType, arlToTransfer, PreFolder)
        '        For Each oDP As DailyPayment In arlToTransfer
        '            oDP.NumOfTransfered += 1
        '            oDPFac.Update(oDP)
        '        Next
        '        MessageBox.Show(SR.UploadSucces(str.Name))
        '        Me.btnFind_Click(Nothing, Nothing) 'Update data from db'NumOfTransfered were changed
        '    Catch ex As Exception
        '        MessageBox.Show(SR.UploadFail(str.Name))
        '    End Try
        'Else
        '    MessageBox.Show("Tidak Ada Data Gyro Untuk Proses Transfer Ke SAP")
        'End If
    End Sub

    Private Function IsShowAcceleration() As Boolean
        If Not Request.Item("ShowAcceleration") Is Nothing AndAlso CType(Request.Item("ShowAcceleration"), String) = "1" Then
            Return True
        End If
        Return False
    End Function

    Private Sub HandleCalendarError()
        Dim Cal1 As Date = Now, Cal2 As Date = Now, Cal3 As Date = Now, Cal4 As Date = Now
        Dim OldCal1 As Date = Now, OldCal2 As Date = Now, OldCal3 As Date = Now, OldCal4 As Date = Now
        Dim arlCal As ArrayList
        Dim IsHavingOldValue As Boolean = False

        If Not IsNothing(Me.sessionHelper.GetSession(_sessOldCalendarValue)) Then
            arlCal = CType(Me.sessionHelper.GetSession(_sessOldCalendarValue), ArrayList)
            OldCal1 = CType(arlCal(0), Date)
            OldCal2 = CType(arlCal(1), Date)
            OldCal3 = CType(arlCal(2), Date)
            OldCal4 = CType(arlCal(3), Date)
            IsHavingOldValue = True
        End If

        Try
            Cal1 = Me.calGyroDate1.Value
        Catch ex As Exception
            If IsHavingOldValue Then
                Cal1 = OldCal1
            Else
                Cal1 = DateSerial(1990, 1, 1)
            End If
        End Try
        Try
            Cal2 = Me.calGyroDate2.Value
        Catch ex As Exception
            If IsHavingOldValue Then
                Cal2 = OldCal2
            Else
                Cal2 = DateSerial(1990, 1, 1)
            End If
        End Try
        Try
            Cal3 = Me.calBaselineDate1.Value
        Catch ex As Exception
            If IsHavingOldValue Then
                Cal3 = OldCal3
            Else
                Cal3 = DateSerial(1990, 1, 1)
            End If
        End Try
        Try
            Cal4 = Me.calBaselineDate2.Value
        Catch ex As Exception
            If IsHavingOldValue Then
                Cal4 = OldCal4
            Else
                Cal4 = DateSerial(1990, 1, 1)
            End If
        End Try

        Me.calGyroDate1.Value = Cal1
        Me.calGyroDate2.Value = Cal2
        Me.calBaselineDate1.Value = Cal3
        Me.calBaselineDate2.Value = Cal4

        Me.calGyroDate1.Text = Cal1.ToString("dd/MM/yyyy")
        Me.calGyroDate2.Text = Cal2.ToString("dd/MM/yyyy")
        Me.calBaselineDate1.Text = Cal3.ToString("dd/MM/yyyy")
        Me.calBaselineDate2.Text = Cal4.ToString("dd/MM/yyyy")

        arlCal = New ArrayList
        arlCal.Add(Cal1)
        arlCal.Add(Cal2)
        arlCal.Add(Cal3)
        arlCal.Add(Cal4)
        Me.sessionHelper.SetSession(Me._sessOldCalendarValue, arlCal)
    End Sub

    Private Sub SaveCalendarValue()
        Exit Sub
        Dim arlCal As New ArrayList
        arlCal.Add(Me.calGyroDate1.Value)
        arlCal.Add(Me.calGyroDate2.Value)
        arlCal.Add(Me.calBaselineDate1.Value)
        arlCal.Add(Me.calBaselineDate2.Value)
        Me.sessionHelper.SetSession(Me._sessOldCalendarValue, arlCal)
    End Sub
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Page.Server.ScriptTimeout = 300
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        Dim IsVisibleTransfByPriv As Boolean = Me.btnTranfer.Visible
        Dim IsVisibleTransfAccByPriv As Boolean = Me.btnTransferAcc.Visible
        If Not IsPostBack Then
            btnDownload.Visible = False
            Bindddl()
            GetSessionCriteria()
            'BindGrid()'temporary 
            'Start    : RemainModule
            btnSimpan.Visible = False
            btnCancel.Visible = False
            'lblColonCreditAccount.Visible = False

            dgDailyPayment.Columns(12).Visible = False 'Selisih
            dgDailyPayment.Columns(13).Visible = False 'PPh

            dgDailyPayment.Columns(20).Visible = False
            dgDailyPayment.Columns(21).Visible = False
            dgDailyPayment.Columns(22).Visible = False
            dgDailyPayment.Columns(23).Visible = True
            Me.btnTransferAcc.Visible = False
            Me.btnTransferAccUlang.Visible = False
            If Not Request.Item("ShowAcceleration") Is Nothing Then
                If CType(Request.Item("ShowAcceleration"), String) = "1" Then
                    btnSimpan.Visible = True
                    btnCancel.Visible = True
                    'lblColonCreditAccount.Visible = True
                    dgDailyPayment.Columns(12).Visible = True 'Selisih
                    dgDailyPayment.Columns(13).Visible = True 'PPh

                    dgDailyPayment.Columns(20).Visible = True
                    dgDailyPayment.Columns(21).Visible = True
                    dgDailyPayment.Columns(22).Visible = True
                    dgDailyPayment.Columns(23).Visible = False

                    Me.btnTransferAcc.Visible = True
                    Me.btnTransferAccUlang.Visible = True
                    'Start  :Privilege
                    If Not SecurityProvider.Authorize(Context.User, SR.Daftar_pembayaran_ubah_status_privilege) Then
                        btnProses.Visible = False
                        btnSimpan.Visible = False
                    End If
                    'End    :Privilege
                End If
            End If
            'End    : RemainModule
            'Start  :Factoring;by:dna;for:yurike;on:20101118;handle KTB Side
            Dim oD As Dealer = Session.Item("DEALER")
            Dim IsVisibleByPrivilege As Boolean = btnValidate.Visible
            If oD.Title = EnumDealerTittle.DealerTittle.KTB Then
                btnValidate.Visible = IsVisibleByPrivilege 'True
                btnCancelValidate.Visible = IsVisibleByPrivilege 'True
                Me.btnTranfer.Visible = True
                Me.btnTransferUlang.Visible = True
            Else
                btnValidate.Visible = False
                btnCancelValidate.Visible = False
                Me.btnTranfer.Visible = False
                Me.btnTransferUlang.Visible = False
            End If '21
            'End    :Factoring;by:dna;for:yurike;on:20101118;handle KTB Side
            Me.btnTranfer.Visible = IsVisibleTransfByPriv
            Me.btnTransferUlang.Visible = Me.btnTranfer.Visible
            Me.btnTransferAcc.Visible = IsVisibleTransfAccByPriv
            Me.btnTransferAccUlang.Visible = Me.btnTransferAcc.Visible

            Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONPAYMENTSTATUS")
            If Not objSSPO Is Nothing Then
                Me.btnFind_Click(Nothing, Nothing)
            End If
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Dim IsImplementFactoring As Boolean = IIf(KTB.DNet.Lib.WebConfig.GetValue("ImpelementFactoring") = 1, True, False)
        If IsImplementFactoring Then
            Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(CType(Session("DEALER"), Dealer).ID, EnumDealerTransType.DealerTransKind.Factoring)
            If Not (Not IsNothing(oTC) AndAlso oTC.Status = 1) Then
                IsImplementFactoring = False
            End If
        End If
        lblFactoring.Visible = IsImplementFactoring
        lblFactoringColon.Visible = IsImplementFactoring
        ddLFactoring.Visible = IsImplementFactoring

        Dim OldCal1 As Date = Now, OldCal2 As Date = Now, OldCal3 As Date = Now, OldCal4 As Date = Now
        Dim arlCal As ArrayList

        If 1 = 2 AndAlso Not IsNothing(Me.sessionHelper.GetSession(_sessOldCalendarValue)) Then
            arlCal = CType(Me.sessionHelper.GetSession(_sessOldCalendarValue), ArrayList)
            OldCal1 = CType(arlCal(0), Date)
            OldCal2 = CType(arlCal(1), Date)
            OldCal3 = CType(arlCal(2), Date)
            OldCal4 = CType(arlCal(3), Date)

            Me.calGyroDate1.Value = OldCal1
            Me.calGyroDate2.Value = OldCal2
            Me.calBaselineDate1.Value = OldCal3
            Me.calBaselineDate2.Value = OldCal4

            Me.calGyroDate1.Text = OldCal1.ToString("dd/MM/yyyy")
            Me.calGyroDate2.Text = OldCal2.ToString("dd/MM/yyyy")
            Me.calBaselineDate1.Text = OldCal3.ToString("dd/MM/yyyy")
            Me.calBaselineDate2.Text = OldCal4.ToString("dd/MM/yyyy")
        End If
    End Sub

    Private Sub Bindddl()
        Dim listitemBlank = New ListItem("Silahkan Pilih", -1)


        Dim arrlistPurpose As ArrayList = New PaymentPurposeFacade(User).RetrieveActiveList()
        ddlPurpose.DataSource = arrlistPurpose
        ddlPurpose.DataTextField = "Description"
        ddlPurpose.DataValueField = "id"
        ddlPurpose.DataBind()
        ddlPurpose.Items.Insert(0, listitemBlank)

        ddlAction.DataSource = RejectStatusPayment.RetrieveRejectStatusPayment()
        ddlAction.DataTextField = "NameStatus"
        ddlAction.DataValueField = "ValStatus"
        ddlAction.DataBind()
        ddlAction.Items.Add(New ListItem("Batal", -1))

        ddlTOP.Items.Clear()
        ddlTOP.Items.Add(New ListItem("Silahkan Pilih", -1))
        ddlTOP.Items.Add(New ListItem(enumPaymentType.PaymentType.COD.ToString, enumPaymentType.PaymentType.COD))
        ddlTOP.Items.Add(New ListItem(enumPaymentType.PaymentType.TOP.ToString, enumPaymentType.PaymentType.TOP))
        ddlTOP.Items.Add(New ListItem(enumPaymentType.PaymentType.RTGS.ToString, enumPaymentType.PaymentType.RTGS))
        ddlTOP.SelectedValue = -1


        Dim aC As ArrayList = New PKHeaderFacade(User).RetrieveListCategory
        Me.ddlCategory.Items.Clear()
        Me.ddlCategory.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each oC As Category In aC
            Me.ddlCategory.Items.Add(New ListItem(oC.CategoryCode, oC.ID))
        Next
        Me.ddlCategory.SelectedValue = -1

        ddlStatusDP.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each item As ListItem In EnumPaymentStatus.GetList()
            ddlStatusDP.Items.Add(item)
        Next
        ddlStatusDP.SelectedIndex = 0

        ddlGyroType.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each item As ListItem In EnumGyroType.GetList()
            ddlGyroType.Items.Add(item)
        Next
        ddlGyroType.SelectedIndex = 0

        Me.ddlIsTransfered.Items.Clear()
        Me.ddlIsTransfered.Items.Add(New ListItem("Semua", 0))
        Me.ddlIsTransfered.Items.Add(New ListItem("Proses", 1))
        Me.ddlIsTransfered.Items.Add(New ListItem("Belum", 2))
        BindRemarkStatus()

    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "POHeader.PONumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub dgDailyPayment_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDailyPayment.SortCommand
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

        dgDailyPayment.SelectedIndex = -1
        dgDailyPayment.CurrentPageIndex = 0
        BindGrid()
    End Sub

    Private Sub dgDailyPayment_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDailyPayment.PageIndexChanged
        SaveCalendarValue()
        dgDailyPayment.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private Sub BindGrid()
        BindDataGrid(dgDailyPayment.CurrentPageIndex)
        dgDailyPayment.DataBind()
        TotalAmount()
    End Sub
    Private Sub TotalAmount()
        Dim Total As Decimal = 0
        Total = CType(sessionHelper.GetSession("TotalAmount"), Decimal)
        lblTotalNilaiValue.Text = FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        'If Not IsNothing(_arrDailyPaymentTotal) Then
        '    Dim tot As Double = 0
        '    For Each item As DailyPayment In _arrDailyPaymentTotal
        '        If item.IsReversed <> 1 And item.RejectStatus = RejectStatusPayment.RejectStatusEnum.Silahkan_Pilih Or item.RejectStatus = RejectStatusPayment.RejectStatusEnum.ReClearing Then
        '            tot += item.Amount
        '        End If
        '    Next
        '    lblTotalNilaiValue.Text = FormatNumber(tot, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'End If
    End Sub
    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        dgDailyPayment.CurrentPageIndex = 0
        BindGrid()
    End Sub

    Private Sub dgDailyPayment_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDailyPayment.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If Not e.Item.DataItem Is Nothing Then
                e.Item.DataItem.GetType().ToString()
                Dim RowValue As DailyPayment = CType(e.Item.DataItem, DailyPayment)
                Dim icAcceleratedDate As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("icAcceleratedDate")

                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                    Dim lbnNoRegPO As LinkButton = e.Item.FindControl("lbnNoRegPO")
                    Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
                    Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
                    Dim lblTglJatuhTempo As Label = e.Item.FindControl("lblTglJatuhTempo")
                    Dim lbtnDownload As LinkButton = e.Item.FindControl("lbtnDownload")
                    Dim lblFlow As Label = e.Item.FindControl("lblFlow")
                    Dim lblHistoryStatus As Label = e.Item.FindControl("lblHistoryStatus")
                    Dim lblSlipNumber As Label = e.Item.FindControl("lblSlipNumber")

                    If Not IsNothing(lblSlipNumber) Then
                        lblSlipNumber.Text = RowValue.SlipNumber
                        If RowValue.SlipNumber.Trim() = "" AndAlso Not IsNothing(RowValue.DailyPaymentHeader) Then
                            lblSlipNumber.Text = RowValue.DailyPaymentHeader.RegNumber
                        End If
                    End If
                    'lbnNoRegPO.Text = RowValue.POHeader.PONumber & IIf(RowValue.POHeader.IsFactoring = 1, "(F)", "")
                    lbnNoRegPO.Text = RowValue.POHeader.DealerPONumber & IIf(RowValue.POHeader.IsFactoring = 1, "(F)", "")
                    e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dgDailyPayment.PageSize * dgDailyPayment.CurrentPageIndex)).ToString
                    e.Item.Cells(3).Text = EnumPaymentStatus.GetStringValue(RowValue.Status)
                    e.Item.Cells(4).Text = RowValue.DailyPaymentHeader.RegNumber
                    e.Item.Cells(6).Text = IIf(RowValue.POHeader.SONumber.Trim <> "", RowValue.POHeader.SONumber, RowValue.POHeader.PONumber)
                    e.Item.Cells(7).Text = RowValue.PaymentPurpose.Description
                    If RowValue.DocDate.ToString("dd/MM/yyyy") = "01/01/1990" Then
                        e.Item.Cells(9).Text = ""
                    Else
                        e.Item.Cells(9).Text = RowValue.DocDate.ToString("dd/MM/yyyy")
                    End If
                    e.Item.Cells(11).Text = FormatNumber(RowValue.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Dim DiffInterest As Decimal = 0
                    Dim PPh As Decimal = 0
                    Dim IntRecalculation As Decimal = 0
                    If RowValue.GyroType = EnumGyroType.GyroType.Percepatan OrElse RowValue.GyroType = EnumGyroType.GyroType.Tolakan Then
                        Dim oDPOld As DailyPayment = RowValue.OldDailyPayment
                        If Not IsNothing(oDPOld) AndAlso oDPOld.ID > 0 Then

                            Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
                            oDPFac.SetInterestDiffOfAccelerate(oDPOld, DiffInterest, PPh, RowValue.GyroType, RowValue.BaselineDate, oDPOld.BaselineDate, IntRecalculation)
                            'DPOld,ref,ref,GyroTypeBaru,BaselineBaru,BaselineLama
                        End If
                    End If
                    e.Item.Cells(12).Text = FormatNumber(DiffInterest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) 'Selisih
                    'e.Item.Cells(13).Text = FormatNumber(PPh, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) 'Selisih 'PPh
                    e.Item.Cells(13).Text = FormatNumber((IntRecalculation / 0.85 * 0.15), 0, TriState.UseDefault, TriState.UseDefault, TriState.True) 'Selisih 'PPh

                    e.Item.Cells(14).Text = FormatNumber(GetSOValue(RowValue.POHeader, RowValue.PaymentPurpose), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    e.Item.Cells(15).Text = DateAdd(DateInterval.Day, RowValue.POHeader.TermOfPayment.TermOfPaymentValue, RowValue.POHeader.ReqAllocationDateTime)
                    e.Item.Cells(16).Text = RowValue.BaselineDate.Subtract(RowValue.DocDate).TotalDays.ToString
                    e.Item.Cells(18).Text = CType(RowValue.RejectStatus, RejectStatusPayment.RejectStatusEnum).ToString
                    If RowValue.RejectStatus = RejectStatusPayment.RejectStatusEnum.Replacing Or RowValue.RejectStatus = RejectStatusPayment.RejectStatusEnum.Reschedule Then
                        e.Item.BackColor = System.Drawing.Color.Tomato
                    ElseIf RowValue.RejectStatus = RejectStatusPayment.RejectStatusEnum.ReClearing Then
                        e.Item.BackColor = System.Drawing.Color.Yellow
                    Else
                        e.Item.Cells(18).Text = String.Empty
                    End If
                    If RowValue.AcceleratedGyro.ToString = "1" Then icAcceleratedDate.Enabled = False
                    e.Item.Cells(22).Text = IIf(RowValue.IsCleared = 1, "x", "")
                    e.Item.Cells(23).Text = IIf(RowValue.IsReversed = 1, "x", "")
                    Dim lblRemarkStatus As Label = e.Item.FindControl("lblRemarkStatus")
                    If RowValue.POHeader.IsFactoring = 1 Then
                        lblRemarkStatus.Text = EnumPaymentRemarkStatus.GetStringValue(RowValue.RemarkStatus)
                    Else
                        lblRemarkStatus.Text = IIf(RowValue.IsCleared = 1, "Cleared", "Not Cleared")
                    End If

                    Dim oD As Dealer = Session.Item("DEALER")
                    lbtnEdit.Visible = False
                    If RowValue.Status = EnumPaymentStatus.PaymentStatus.Baru Then
                        lbtnEdit.CommandName = "Edit"
                        lbtnEdit.Text = "<img src=""../images/edit.gif"" border=""0"" alt=""Edit"">"
                        lbtnEdit.Visible = True
                    ElseIf RowValue.Status = EnumPaymentStatus.PaymentStatus.Selesai Then
                        If oD.Title = EnumDealerTittle.DealerTittle.DEALER Then
                            lbtnEdit.CommandName = "Accelerate"
                            lbtnEdit.Text = "<img src=""../images/71.gif"" border=""0"" alt=""Percepatan"">"
                            lbtnEdit.Visible = True
                            'temporaray for yurike:domes
                            lbtnEdit.Visible = Not (RowValue.POHeader.IsFactoring = 1)
                        End If
                    End If

                    lbtnDelete.Visible = False 'Initiate

                    If RowValue.Status = EnumPaymentStatus.PaymentStatus.Baru Then
                        'If oD.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        'Dim DPOld As DailyPayment = RowValue.OldDailyPayment
                        'If Not IsNothing(DPOld) AndAlso DPOld.ID = 0 Then
                        '    lbtnDelete.Visible = True
                        'End If
                        'Else
                        '    lbtnDelete.Visible = False
                        'End If
                        lbtnDelete.Visible = True 'by:dna;for:yurike;on:mrs.neni's request;on:2013.05.17
                    Else
                        lbtnDelete.Visible = False
                    End If

                    If RowValue.AcceleratorID <> 0 Then
                        lbtnEdit.Visible = False
                    End If
                    lbtnDownload.Visible = Not (RowValue.POHeader.IsFactoring = 1)
                    If lbtnDownload.Visible AndAlso RowValue.Status <> KTB.DNet.Domain.EnumPaymentStatus.PaymentStatus.Selesai Then lbtnDownload.Visible = False
                    If lbtnEdit.Visible Then
                        If lbtnEdit.CommandName = "Edit" Then
                            lbtnEdit.Visible = SecurityProvider.Authorize(Context.User, SR.po_pembayaran_edit_privilege)
                        Else
                            lbtnEdit.Visible = SecurityProvider.Authorize(Context.User, SR.po_pembayaran_percepatan_privilege)
                        End If
                    End If
                    If lbtnDelete.Visible Then
                        lbtnDelete.Visible = SecurityProvider.Authorize(Context.User, SR.po_pembayaran_delete_privilege)
                    End If

                    lblFlow.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDocumentFlow.aspx?flow=PO_" & RowValue.POHeader.SONumber & "_" & RowValue.POHeader.PONumber, "", 500, 450, "ViewDailyPKFlow")
                    lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistoryFU.aspx?DocType=" & LookUp.DocumentType.Gyro & "&DocNumber=" & RowValue.ID, "", 400, 400, "DealerSelection")
                End If
            End If

        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim _fileHelper As New FileHelper
        Dim fileInfo1 As New FileInfo(Server.MapPath(""))

        Try
            Dim IsAcc As Boolean = False
            If Not IsNothing(Request.Item("ShowAcceleration")) AndAlso CType(Request.Item("ShowAcceleration"), String) = "1" Then
                IsAcc = True
            End If
            Dim str As FileInfo = _fileHelper.TransferDPtoSAP(PopulateDailyTransferData, fileInfo1, IsAcc)
            Response.Redirect("../Downloadlocal.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("DailyPaymentDestFileDirectory").ToString & "\" & str.Name)
        Catch ex As Exception
            MessageBox.Show("Gagal Download File.")
        End Try
    End Sub

    Private Sub dgDailyPayment_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDailyPayment.ItemCommand
        _arrDailyPayment = sessionHelper.GetSession("ArlDailyPayment")
        If e.CommandName = "PoDetail" Then
            sessionHelper.SetSession("PrevPage", Request.Url.ToString())
            SetSessionCriteria()
            Response.Redirect("../PO/PODetails.aspx?id=" & CType(_arrDailyPayment(e.Item.ItemIndex), DailyPayment).POHeader.ID)
        ElseIf e.CommandName.ToUpper = "download".ToUpper Then
            sessionHelper.SetSession("PrevPage", Request.Url.ToString())
            SetSessionCriteria()
            sessionHelper.SetSession("FrmCRFPrint.PageOpener", "../PO/UpdatePaymentStatus.aspx")
            Response.Redirect("../PO/FrmCRFPrint.aspx?id=" & CType(_arrDailyPayment(e.Item.ItemIndex), DailyPayment).ID)
        ElseIf e.CommandName.Trim.ToUpper = "Edit".ToUpper Then
            sessionHelper.SetSession("PrevPage", Request.Url.ToString())
            SetSessionCriteria()
            Dim sParams As String = ""
            If Not IsNothing(Request.Item("ShowAcceleration")) Then
                sParams = "?ShowAcceleration=" & CType(Request.Item("ShowAcceleration"), String)
            End If
            sessionHelper.SetSession("FrmEntryGyro.PageOpener", "../PO/UpdatePaymentStatus.aspx" & sParams)
            Dim oDP As DailyPayment = _arrDailyPayment(e.Item.ItemIndex)
            Response.Redirect("FrmEntryGyro.aspx?DPID=" & oDP.ID)
        ElseIf e.CommandName.Trim.ToUpper = "Accelerate".ToUpper Then
            sessionHelper.SetSession("PrevPage", Request.Url.ToString())
            SetSessionCriteria()
            Dim sParams As String = ""
            If Not IsNothing(Request.Item("ShowAcceleration")) Then
                sParams = "?ShowAcceleration=" & CType(Request.Item("ShowAcceleration"), String)
            End If
            sessionHelper.SetSession("FrmEntryGyro.PageOpener", "../PO/UpdatePaymentStatus.aspx" & sParams)
            Dim oDP As DailyPayment = _arrDailyPayment(e.Item.ItemIndex)
            Response.Redirect("FrmEntryGyro2.aspx?DPID=" & oDP.ID & "&IsAccelerate=1")
        ElseIf e.CommandName.Trim.ToUpper = "Del".ToUpper Then
            Dim oDP As DailyPayment = _arrDailyPayment(e.Item.ItemIndex)
            Dim fac As New DailyPaymentFacade(User)
            fac.Delete(odp)
            btnFind_Click(Nothing, Nothing)


        End If
    End Sub

    Private Function PopulatePayment(ByVal type As Integer) As ArrayList
        Dim item As DataGridItem
        Dim collPo As New ArrayList
        Dim payment As DailyPayment
        Dim paymentFacade As New DailyPaymentFacade(User)
        Dim Idx As Integer
        Dim IsAll As Boolean = Me.IsAllDataSelected
        Dim arlData As ArrayList

        If IsAll Then
            Me.BindDataGrid(Me.dgDailyPayment.CurrentPageIndex, True)
            arlData = Me.sessionHelper.GetSession("ArlDailyPaymentAll")
        Else
            arlData = Me.sessionHelper.GetSession("ArlDailyPayment")
        End If

        For Idx = 0 To arlData.Count - 1
            If IsAll Or (Idx <= Me.dgDailyPayment.Items.Count - 1 AndAlso CType(Me.dgDailyPayment.Items(Idx).FindControl("chkExport"), CheckBox).Checked) Then
                payment = CType(arlData(Idx), DailyPayment)
                If type > 0 Then
                    payment.RejectStatus = type
                Else
                    payment.RejectStatus = 0
                End If
                collPo.Add(payment)
            End If
        Next
        Return collPo

        'For Each item In Me.dgDailyPayment.Items
        '    If CType(item.FindControl("chkExport"), CheckBox).Checked Then
        '        Dim id As Integer = CInt(CType(item.FindControl("lblID"), Label).Text)
        '        payment = paymentFacade.Retrieve(id)
        '        If type > 0 Then
        '            payment.RejectStatus = type
        '        Else
        '            payment.RejectStatus = 0
        '        End If
        '        collPo.Add(payment)
        '    End If
        'Next
        'Return collPo
    End Function

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click
        If Me.ddlAction.SelectedIndex > 0 Then
            Dim al As ArrayList
            al = PopulatePayment(Me.ddlAction.SelectedValue)
            If al.Count > 0 Then
                Dim objDailyPaymentFacade As New DailyPaymentFacade(User)
                objDailyPaymentFacade.Update(al)
                BindGrid()
            Else
            End If
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim objDP As DailyPayment
        Dim IsUpdated As Boolean = False
        CompanyCode = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrSOError As String = ""
        Dim index As Integer = 0
        For Each di As DataGridItem In dgDailyPayment.Items
            Dim chkExport As CheckBox = di.FindControl("chkExport")
            Dim lblID As Label = di.FindControl("lblID")
            Dim icAcceleratedDate As KTB.DNet.WebCC.IntiCalendar = di.FindControl("icAcceleratedDate")
            Dim txtRemark As TextBox = di.FindControl("txtRemark")
            Dim arlTemp As ArrayList = New ArrayList

            If chkExport.Checked Then
                objDP = objDPFac.Retrieve(CType(lblID.Text, Integer))
                If (objDP.POHeader.ContractHeader.Category.ProductCategory.Code.Trim = CompanyCode OrElse CompanyCode = "MFTBC") Then
                    objDP.AcceleratedDate = Now
                    objDP.AcceleratedDate = icAcceleratedDate.Value
                    objDP.Remark = txtRemark.Text.Trim
                    objDP.AcceleratedGyro = 1 ' AcceleratedGyro
                    arlTemp.Clear()
                    arlTemp.Add(objDP)
                    objDPFac.Update(arlTemp)
                    chkExport.Checked = False
                    If IsUpdated = False Then IsUpdated = True
                Else
                    If (index > 0) Then
                        arrSOError += ", "
                    End If
                    arrSOError += objDP.POHeader.SONumber
                    index += 1
                End If
            End If
        Next
        If arrSOError.Trim.Length > 0 Then
            MessageBox.Show("SO " + arrSOError + " tidak terdapat pada Kategori Produk " & CompanyCode)
        End If
        If IsUpdated Then BindGrid()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim objDP As DailyPayment
        Dim IsUpdated As Boolean = False
        CompanyCode = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrSOError As String = ""
        Dim index As Integer = 0

        For Each di As DataGridItem In dgDailyPayment.Items
            Dim chkExport As CheckBox = di.FindControl("chkExport")
            Dim lblID As Label = di.FindControl("lblID")
            Dim icAcceleratedDate As KTB.DNet.WebCC.IntiCalendar = di.FindControl("icAcceleratedDate")
            Dim txtRemark As TextBox = di.FindControl("txtRemark")
            Dim arlTemp As ArrayList = New ArrayList

            If chkExport.Checked Then
                objDP = objDPFac.Retrieve(CType(lblID.Text, Integer))
                If (objDP.POHeader.ContractHeader.Category.ProductCategory.Code.Trim = CompanyCode OrElse CompanyCode = "MFTBC") Then
                    objDP.AcceleratedDate = DateSerial(1753, 1, 1) '1753-01-01 00:00:00.000 Now
                    'objDP.AcceleratedDate =  icAcceleratedDate.Value
                    objDP.Remark = txtRemark.Text.Trim
                    objDP.AcceleratedGyro = 0 '1= AcceleratedGyro
                    arlTemp.Clear()
                    arlTemp.Add(objDP)
                    objDPFac.Update(arlTemp)
                    chkExport.Checked = False
                    If IsUpdated = False Then IsUpdated = True
                Else
                    If (index > 0) Then
                        arrSOError += ", "
                    End If
                    arrSOError += objDP.POHeader.SONumber
                    index += 1
                End If
            End If
        Next
        If arrSOError.Trim.Length > 0 Then
            MessageBox.Show("SO " + arrSOError + " tidak terdapat pada Kategori Produk " & CompanyCode)
        End If
        If IsUpdated Then BindGrid()
    End Sub

    Private Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        UpdateDPStatus(EnumPaymentStatus.PaymentStatus.Validasi, EnumPaymentStatus.PaymentStatus.Baru)
    End Sub

    Private Sub btnCancelValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelValidate.Click
        UpdateDPStatus(EnumPaymentStatus.PaymentStatus.Baru, EnumPaymentStatus.PaymentStatus.Validasi)
    End Sub

    Private Sub btnTranfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTranfer.Click
        Me.TransferToSAP("Normal", "Normal")
    End Sub

    Private Sub btnTransferUlang_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransferUlang.Click
        Me.TransferToSAP("Normal", "Ulang")
    End Sub

    Private Sub btnTransferAcc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransferAcc.Click
        Me.TransferToSAP("Accelerate", "Normal")
    End Sub

    Private Sub btnTransferAccUlang_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransferAccUlang.Click
        Me.TransferToSAP("Accelerate", "Ulang")
    End Sub

    Private Sub btnDownloadDealer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadDealer.Click
        Dim _fileHelper As New FileHelper
        Dim fileInfo1 As New FileInfo(Server.MapPath(""))
        Dim arlData As ArrayList = PopulateDailyTransferData()
        If arlData.Count < 1 Then
            MessageBox.Show("Tidak ada data yang didownload")
            Exit Sub
        End If
        Try
            Dim str As FileInfo = _fileHelper.TransferDPByDealer(arlData, fileInfo1)
            Response.Redirect("../Downloadlocal.aspx?file=" & "DataTemp" & "\" & str.Name)
        Catch ex As Exception
            MessageBox.Show("Gagal Download File.")
        End Try
    End Sub

#End Region
    Protected Overrides Function SaveViewState() As Object
        Dim OriState As Object = MyBase.SaveViewState()

        Return OriState
    End Function
    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        Me.HandleCalendarError()
    End Sub

End Class