Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.IndentPart
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports Ktb.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessValidation.Helpers

Public Class FrmPoEstimationEquip
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorTanggalPO As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents dtgIPDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnCetak As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnClose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents hdnValNew As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents icOrderDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents icConfirmDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlPaymentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpPONo As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblnDay As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPPN As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalTagihan As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim _arlIPDetail As ArrayList = New ArrayList
    Dim _historyFacade As StatusChangeHistoryFacade = New StatusChangeHistoryFacade(User)
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Dim _sesshelper As New SessionHelper
    Dim objDealer As Dealer
    Dim _state As Boolean
    Dim _blnDealer As Boolean
    Dim _indentPHID As Integer
    Dim _isPopup As Integer = 0
    Dim bCekSendPriv As Boolean = SecurityProvider.Authorize(context.User, SR.PengajuanPartIndentPartCreate_Privilege)
    Const TEMP_EMAIL_APPROVED = "../IndentPartEquipment/EmailTemplateApprovedSPPOSimple.htm"
    Const TEMP_EMAIL_DEPOSIT_B = "../IndentPartEquipment/EmailTemplateDepositeBSPPOSimple.htm"
    ''' <summary>
    ''' Hardcode Set 1 (COD)
    ''' </summary>
    ''' <remarks></remarks>
    Const _ConstTOPID As Integer = 1
#Region "event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        _state = Convert.ToBoolean(Request.QueryString("View"))
        If Not _state Then
            InitiateAuthorization()
        End If

        objDealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            'InitiateAuthorization()
            lblDealerCode.Text = objDealer.DealerCode
            lblDealerTerm.Text = objDealer.SearchTerm2
            lblPopUpPONo.Attributes("onclick") = "showPopUp('../PopUp/PopUpEstimationEquip.aspx?IsMultiple=false&DealerID=" & objDealer.ID & "', '', 500, 600,PONOSelection);"
            'lblPopUpPONo.Attributes("onclick") = "showPopUp('../PopUp/PopUpEstimationEquip.aspx?DealerID=" & objDealer.ID & "', '', 500, 600,PONOSelection);"
            dtgIPDetail.ShowFooter = True
            _blnDealer = True
        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            'InitiateAuthorization()
            lblPopUpPONo.Attributes("onclick") = "showPopUp('../PopUp/PopUpEstimationEquip.aspx?IsMultiple=false', '', 500, 600,PONOSelection);"
            'lblPopUpPONo.Attributes("onclick") = "showPopUp('../PopUp/PopUpEstimationEquip.aspx', '', 500, 600,PONOSelection);"
            dtgIPDetail.ShowFooter = False
            _blnDealer = False
            btnValidasi.Enabled = False
        End If

        _indentPHID = Request.QueryString("SPPOID")
        _isPopup = CInt(Request.QueryString("isPopUp"))

        If _indentPHID = 0 Or _isPopup = 1 Then
            btnCancel.Enabled = False
        Else
            btnFind.Enabled = False
        End If

        If _isPopup <> 1 Then
            btnClose.Visible = False
        Else
            btnClose.Visible = True
            btnCancel.Enabled = False
        End If

        If Not bCekSendPriv Then
            btnValidasi.Enabled = False
        End If

        If IsPostBack Then
            If _indentPHID = 0 Then
                If CType(ViewState("vsSave"), String) = "false" Then
                    If Request.Form("hdnValNew") = "1" Then
                        btnNew_Click(Nothing, Nothing)
                    End If
                End If
            End If
            Return
        End If
        viewstate.Add("TotalTagihan", 0)
        'if ! postback
        BindDropDown()
        ViewState("CurrentSortColumn") = "SparePartMaster.PartNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        If _indentPHID > 0 Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                btnCetak.Disabled = False
                btnSave.Enabled = False
            ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                btnDelete.Enabled = False
                btnValidasi.Enabled = False
                btnSave.Enabled = True
            End If

            Dim obIPH As IndentPartHeader = New IndentPartHeaderFacade(User).Retrieve(_indentPHID)
            ViewState.Add("vsAccess", "edit")
            DisplayTransactionResult(_indentPHID)
            DetailOnly(True)
            txtNoPO.Enabled = False
            lblPopUpPONo.Enabled = False
            dtgIPDetail.Columns(2).Visible = False
        Else
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                btnCetak.Disabled = True
            End If

            If InitialPageSession() Then
                BindIPDetail()
                btnDelete.Enabled = False
                btnValidasi.Enabled = False
                btnNew.Enabled = False
                btnCancel.Visible = False
                btnSave.Enabled = False
            End If
        End If
        btnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dibatalkan?');")
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim f_est As EstimationEquipHeaderFacade = New EstimationEquipHeaderFacade(User)
        Dim iptmp As ArrayList = New ArrayList
        Dim i As Integer = 0
        Dim szEst As String = ""

        If (txtNoPO.Text = "") Then
            Dim al As ArrayList = f_est.RetrieveByDealer(objDealer.ID)
            For Each objH As EstimationEquipHeader In al
                szEst += objH.EstimationNumber + ";"
            Next
            If (szEst.Length > 1) Then
                szEst = szEst.Substring(0, szEst.Length - 1)
            End If
        Else
            szEst = txtNoPO.Text
        End If
        Dim szEstNums As String() = szEst.Split(";")

        For Each szEstNo As String In szEstNums
            Dim objEstH As EstimationEquipHeader = f_est.Retrieve(szEstNo)
            If IsNothing(objEstH) Or objEstH.ID = 0 Then
                MessageBox.Show("Nomor Estimasi '" & szEstNo & "' Tidak Ditemukan")
                Return
            End If
            'Start  :Bug:by:dna;for:Yurike;on:20100705;remark:wrong enumeration
            'StartOldScript
            For Each objEstItem As EstimationEquipDetail In objEstH.EstimationEquipDetails
                Dim objSPPODetail As IndentPartDetail = New IndentPartDetail
                objSPPODetail.ID = objEstItem.ID
                objSPPODetail.Qty = objEstItem.EstimationUnit
                objSPPODetail.Price = objEstItem.Harga
                objSPPODetail.SparePartMaster = objEstItem.SparePartMaster

                If (objEstItem.Status = EnumEstimationEquipStatus.EstimationEquipStatusDetail.BelumKonfirmasi) Then
                    'objSPPODetail.Description = SPPODetailEstimateStatus.BelumKonfirmasi
                    'iptmp.Add(objSPPODetail)
                ElseIf (objEstItem.Status = EnumEstimationEquipStatus.EstimationEquipStatusDetail.Konfirmasi_BelumOrder) Then
                    If (objEstItem.ConfirmedDate = icConfirmDate.Value) Then
                        If (DateDiff(DateInterval.Day, objEstItem.ConfirmedDate, DateTime.Now) > CType(lblnDay.Text, Integer)) Then
                            objSPPODetail.Description = SPPODetailEstimateStatus.BelumKonfirmasi
                        Else
                            objSPPODetail.Description = SPPODetailEstimateStatus.Konfirmasi_BelumOrder
                        End If
                        i += 1
                        iptmp.Add(objSPPODetail)
                    End If
                ElseIf (objEstItem.Status = EnumEstimationEquipStatus.EstimationEquipStatusDetail.Konfirmasi_SudahOrder) Then
                    If (objEstItem.ConfirmedDate = icConfirmDate.Value) Then
                        objSPPODetail.Description = SPPODetailEstimateStatus.Konfirmasi_SudahOrder
                        iptmp.Add(objSPPODetail)
                    End If
                End If
            Next
            'EndOldScript
            'Start  :Bug:by:dna;for:Yurike;on:20100705;remark:wrong enumeration
        Next
        'Start  :RemainModule-Sort by Description 
        Dim arlSortedIPD As New ArrayList
        For Each oIPD As IndentPartDetail In iptmp
            If oIPD.Description = SPPODetailEstimateStatus.BelumKonfirmasi Then 'objSppoDetail.Description = SPPODetailEstimateStatus.BelumKonfirmasi  
                oIPD.RowStatus = 2 'Red
            ElseIf oIPD.Description = SPPODetailEstimateStatus.Konfirmasi_BelumOrder Then
                oIPD.RowStatus = 0 'Green
            ElseIf oIPD.Description = SPPODetailEstimateStatus.Konfirmasi_SudahOrder Then
                oIPD.RowStatus = 1 'Yellow
            ElseIf oIPD.Description = "" Then
                oIPD.RowStatus = 1 'Yellow
            End If
            arlSortedIPD.Add(oIPD)
        Next
        arlSortedIPD = CommonFunction.SortArraylist(arlSortedIPD, GetType(IndentPartDetail), "RowStatus", Sort.SortDirection.ASC)
        iptmp = New ArrayList
        For Each oIPD As IndentPartDetail In arlSortedIPD
            oipd.RowStatus = 0
            iptmp.Add(oipd)
        Next
        'SPPODetailEstimateStatus
        'End    :RemainModule-Sort by Description 
        _sesshelper.SetSession("sessIPDetail", iptmp)
        BindIPDetail()
        'Start  :Refresh Datagrid condition
        dtgIPDetail.EditItemIndex = -1
        If _blnDealer Then
            dtgIPDetail.ShowFooter = True
            btnSave.Enabled = True
        Else
            dtgIPDetail.ShowFooter = False
        End If
        'End    :Refresh Datagrid condition

        If (i > 0) Then
            btnNew.Enabled = True
            btnSave.Enabled = True
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (Convert.ToInt32(ddlPaymentType.SelectedValue) = EnumEstimationEquipStatus.EstimationEquipmentPaymentType.Silakan_Pilih) Then
            MessageBox.Show("Type Pembayaran Belum Dipilih")
            Exit Sub
        End If

        If CType(Session("sessIPDetail"), ArrayList).Count > 0 Then
            Dim nResult As Integer

            Select Case CType(ViewState("vsAccess"), String)
                Case "insert"
                    Try
                        Dim i As Integer = 0
                        For Each item As DataGridItem In dtgIPDetail.Items
                            Dim chk As CheckBox = item.FindControl("chkItemChecked")
                            If chk.Checked Then
                                i += 1
                            End If
                        Next
                        If (i = 0) Then
                            MessageBox.Show("Belum Ada Equipment Indent Part Yang Dipilih")
                            Exit Sub
                        End If

                        nResult = InsertNewSPPOPart()

                        If nResult <> -1 Then
                            DisplayTransactionResult(nResult)
                            ViewState.Add("vsAccess", "edit")
                            If CType(ViewState("vsSave"), String) = "false" Then
                                MessageBox.Show(SR.SaveSuccess & " : No PO = " & txtPONumber.Text)
                                btnFind.Enabled = False
                            Else
                                MessageBox.Show(SR.SaveSuccess)
                                MessageBox.Show("Harap Lakukan Proses Kirim")

                            End If
                            ViewState.Add("vsSave", "true")

                            btnDelete.Enabled = True
                            If Not IsNothing(Request.QueryString("SPPOID")) Then
                                btnNew.Enabled = False
                                btnValidasi.Enabled = False
                            Else
                                btnValidasi.Enabled = bCekSendPriv
                                btnNew.Enabled = True
                            End If

                            btnCetak.Disabled = False
                            btnValidasi.Enabled = True
                        Else
                            MessageBox.Show(SR.SaveFail)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Gagal simpan Spare Part PO Equipment Part " & ex.Message)
                        Return
                    End Try

                Case "edit"
                    MergeIPDetail(CType(Session("sessIPHeader"), IndentPartHeader).ID)
                    Try
                        nResult = EditIP()
                        If nResult <> -1 Then
                            DisplayTransactionResult(nResult)
                            MessageBox.Show(SR.SaveSuccess)
                            ViewState.Add("vsSave", "true")
                            btnFind.Enabled = False
                        Else
                            MessageBox.Show(SR.SaveFail)
                        End If
                    Catch ex1 As Exception
                        MessageBox.Show("Gagal simpan Indent Part PO " & ex1.Message)
                        Return
                    End Try
            End Select

        Else
            MessageBox.Show(SR.GridIsEmpty("Estimation Equipment Part Detail"))
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        btnCetak.Disabled = True
        _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)
        If CType(ViewState("vsSave"), String) = "new" AndAlso _arlIPDetail.Count > 0 Then
            ViewState.Add("vsSave", "false")
            MessageBox.Confirm("Data belum disimpan. Apakah anda akan membuat pengajuan baru?", "hdnValNew")
        Else
            ViewState.Add("vsAccess", "insert")
            ViewState.Add("vsSave", "new")

            RemoveAllSession()
            txtPONumber.Text = ""
            lblStatusValue.Text = "Baru"

            icOrderDate.Value = Date.Now
            icConfirmDate.Value = Date.Now
            _arlIPDetail.Clear()
            _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
            BindIPDetail()
            btnDelete.Enabled = False
            btnNew.Enabled = False
            btnSave.Enabled = False
            btnValidasi.Enabled = False
            btnCancel.Enabled = False
            txtNoPO.Text = ""
            lblTotalAmount.Text = "Rp. 0"
        End If
        dtgIPDetail.Columns(dtgIPDetail.Columns.Count - 1).Visible = True
        dtgIPDetail.ShowFooter = True
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../IndentPartEquipment/FrmPoListEstimationEquip.aspx", True)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim obj As IndentPartHeader
        obj = CType(Session("sessIPHeader"), IndentPartHeader)

        objDealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        UpdateStatus(obj, EnumIndentPartEquipStatus.EnumUpdateStatusKTB.Batal)
        Exit Sub

        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            If obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Kirim Then
                MessageBox.Show("Status " & obj.StatusDealerDesc & " Tidak Dapat Diubah statusnya menjadi batal")
                Exit Sub
            ElseIf obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal Then
                MessageBox.Show("Status " & obj.StatusDealerDesc & " Tidak Dapat Diubah statusnya menjadi batal")
                Exit Sub
            Else
                obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal
            End If
        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            If obj.Status = EnumIndentPartStatus.IndentPartStatusKTB.Baru Then
                MessageBox.Show("Status " & obj.StatusKTBDesc & " Tidak Dapat Diubah statusnya menjadi batal")
                Exit Sub
            ElseIf obj.Status = EnumIndentPartStatus.IndentPartStatusKTB.Batal_Order Then
                MessageBox.Show("Status " & obj.StatusKTBDesc & " Tidak Dapat Diubah statusnya menjadi batal")
                Exit Sub
            Else
                obj.Status = EnumIndentPartStatus.IndentPartStatusKTB.Batal_Order
            End If
        End If

        'obj.CancelRequestBy = User.Identity.Name

        Dim hf As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        Dim result As Integer = hf.Update(obj)
        'hf.RecordStatusChangeHistory(obj, EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru)
        _sesshelper.SetSession("sessIPHeader", obj)
        lblStatusValue.Text = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal.ToString

        MessageBox.Show("No Pengajuan " & txtPONumber.Text & " Telah Dibatalkan")

        btnDelete.Enabled = False
        btnValidasi.Enabled = False
        btnSave.Enabled = False
        dtgIPDetail.ShowFooter = False
        BindIPDetail()

        For Each item As DataGridItem In dtgIPDetail.Items
            Dim lbtnEdit As LinkButton = item.FindControl("lbtnEdit")
            Dim lbtnPopUpText As LinkButton = item.FindControl("lbtnPopUpText")
            Dim lbtnDelete As LinkButton = item.FindControl("lbtnDelete")
            If Not IsNothing(lbtnDelete) Then
                lbtnDelete.Visible = False
            End If
            If Not IsNothing(lbtnPopUpText) Then
                lbtnPopUpText.Visible = False
            End If
            If Not IsNothing(lbtnEdit) Then
                lbtnEdit.Visible = False
            End If
        Next

    End Sub

    Private Sub btnValidasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidasi.Click
        Dim obj As IndentPartHeader
        obj = CType(Session("sessIPHeader"), IndentPartHeader)
        If (IsNothing(obj)) Then
            MessageBox.Show("DEV ERR: IndentPartHeader Object is null")
        End If

        objDealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        'If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
        '    If obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Kirim Then
        '        MessageBox.Show("Status " & obj.StatusDealerDesc & " Tidak Dapat Diubah statusnya menjadi kirim")
        '        Exit Sub
        '    ElseIf obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal Then
        '        MessageBox.Show("Status " & obj.StatusDealerDesc & " Tidak Dapat Diubah statusnya menjadi kirim")
        '        Exit Sub
        '    ElseIf obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal_Order Then
        '        MessageBox.Show("Status " & obj.StatusDealerDesc & " Tidak Dapat Diubah statusnya menjadi kirim")
        '        Exit Sub
        '    Else
        '        If (CInt(ddlPaymentType.SelectedValue) = EnumEstimationEquipStatus.EstimationEquipmentPaymentType.Deposit_C) Then
        '            obj.Status = EnumIndentPartStatus.IndentPartStatus.RILIS
        '            sendDeposit_C(obj)
        '        Else
        '            obj.Status = EnumIndentPartStatus.IndentPartStatus.SENT
        '            If CInt(ddlPaymentType.SelectedValue) = EnumEstimationEquipStatus.EstimationEquipmentPaymentType.Deposit_B Then
        '                sendDeposit_B(obj)
        '            End If
        '        End If
        '    End If
        'Else
        '    MessageBox.Show("DEV ERR: MMKSI Tidak Bisa Mengubah Status BARU menjadi KIRIM")
        '    Exit Sub
        'End If

        Dim strMessage As String = ""
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            Dim NewDealerStatus As Integer
            Dim NewKTBStatus As Integer
            If EnumIndentPartEquipStatus.IsValidUpdateDealer(EnumIndentPartEquipStatus.EnumUpdateStatusDealer.Kirim, obj.Status, NewDealerStatus, obj.StatusKTB, NewKTBStatus, obj.PaymentType, strMessage) Then
                obj.StatusKTB = NewKTBStatus
                obj.Status = NewDealerStatus
                If obj.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B Then
                    sendDeposit_B(obj)
                ElseIf obj.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_C Then
                    sendDeposit_C(obj)
                End If
            Else
                MessageBox.Show(strMessage)
                Exit Sub
            End If
        Else
            MessageBox.Show("DEV ERR: MMKSI Tidak Bisa Mengubah Status BARU menjadi KIRIM")
            Exit Sub
        End If

        Dim hf As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        obj.PaymentType = CByte(ddlPaymentType.SelectedValue)
        Dim result As Integer = hf.Update(obj)

        hf.RecordStatusChangeHistory(obj, EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru)
        _sesshelper.SetSession("sessIPHeader", obj)
        MessageBox.Show("No Pengajuan " & txtPONumber.Text & " Telah Dikirim")
        lblStatusValue.Text = EnumIndentPartEquipStatus.GetStatusDealerDesc(obj.Status) 'obj.StatusDealerDesc
        btnValidasi.Enabled = False
        btnSave.Enabled = False
        btnDelete.Enabled = False
        icConfirmDate.Enabled = False
        dtgIPDetail.ShowFooter = False
        BindIPDetail()
    End Sub

    Private Sub dtgIPDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgIPDetail.ItemCommand
        _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)

        Select Case e.CommandName
            Case "edit" 'Edit mode activated
                dtgIPDetail.ShowFooter = False
                btnSave.Enabled = False
                dtgIPDetail.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Try
                    Dim objIPDetail As IndentPartDetail = _arlIPDetail(e.Item.ItemIndex)
                    If objipdetail.ID > 0 Then
                        Dim result As Integer = New IndentPartDetailFacade(User).DeleteFromDB(objIPDetail)
                        If result = -1 Then
                            MessageBox.Show(SR.DeleteFail)
                            Exit Sub
                        End If
                    End If
                    _arlIPDetail.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception
                End Try

            Case "save" 'Update this datagrid item       
                Dim objIPDetail As IndentPartDetail = _arlIPDetail(e.Item.ItemIndex)
                Dim txtQty As TextBox = CType(e.Item.FindControl("txtEQTY"), TextBox)
                Dim lblEQTY As Label = CType(e.Item.FindControl("lblEQTY"), Label)
                Dim nResult As Integer
                If Not _blnDealer Then
                    txtQty.Text = lblEQTY.Text
                End If
                If IsNothing(txtQty) OrElse txtQty.Text = String.Empty OrElse CType(txtQty.Text.Trim, Integer) <= 0 Then
                    MessageBox.Show("Unit Pesanan tidak boleh kosong/0")
                    Return
                End If

                'If Not PartIsExist(objIPDetail.SparePartMaster.PartNumber.ToUpper(), _arlIPDetail, e.Item.ItemIndex) Then
                objIPDetail.Qty = CType(txtQty.Text, Integer)
                objDealer = _sesshelper.GetSession("DEALER")
                If CType(ViewState("vsAccess"), String) = "edit" Then
                    objIPDetail.IndentPartHeader = CType(Session("sessIPHeader"), IndentPartHeader)
                    nResult = New IndentPartDetailFacade(User).Update(objIPDetail)

                    Dim edf As EstimationEquipDetailFacade = New EstimationEquipDetailFacade(User)
                    Dim objEqpDetail As EstimationEquipDetail = edf.RetrieveByEstimationNumberAndPartNumber(txtNoPO.Text, objIPDetail.SparePartMaster.PartNumber)
                    If (CInt(txtQty.Text) > objEqpDetail.EstimationUnit) Then
                        MessageBox.Show("Qty tidak boleh lebih besar dari unit estimasi")
                        Return
                    End If

                End If
                dtgIPDetail.EditItemIndex = -1
                If _blnDealer Then
                    dtgIPDetail.ShowFooter = True
                    btnSave.Enabled = True
                Else
                    ' KTB hanya bs update pricenya saja
                    dtgIPDetail.ShowFooter = False
                    btnSave.Enabled = False
                End If
                'Else
                '    MessageBox.Show(SR.DataIsExist("Spare Part"))
                'End If

            Case "cancel" 'Cancel Update this datagrid item 
                dtgIPDetail.EditItemIndex = -1
                If _blnDealer Then
                    dtgIPDetail.ShowFooter = True
                    btnSave.Enabled = True
                Else
                    dtgIPDetail.ShowFooter = False
                End If

        End Select
        _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        BindIPDetail()
    End Sub

    Private Sub dtgIPDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgIPDetail.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            SetDtgIPDetailItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetDtgIPDetailItemEdit(e)
        End If
    End Sub

    Private Sub dtgIPDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgIPDetail.SortCommand
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

        Dim arlToSort As ArrayList = _sesshelper.GetSession("sessIPDetail")

        CommonFunction.SortArraylist(arlToSort, GetType(EstimationEquipDetail), ViewState("CurrentSortColumn"), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        BindIPDetail()
    End Sub

#End Region

#Region "private function"

    Private Sub InitiateAuthorization()
        objDealer = CType(_sesshelper.GetSession("DEALER"), Dealer)

        If Not IsNothing(Request.QueryString("SPPOID")) Then
            If CType(Request.QueryString("SPPOID"), Integer) > 0 Then
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    If Not SecurityProvider.Authorize(Context.User, SR.Ubah_status_pengajuan_order_indent_part_equipment_privilege) Then
                        Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Indent Part Equipment - Pengajuan Order")
                    End If
                End If
            End If
        ElseIf Not SecurityProvider.Authorize(Context.User, SR.Buat_pengajuan_order_indent_part_equipment_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Indent Part Equipment - Ubah Pengajuan Order")
        End If

        btnValidasi.Visible = False
        If SecurityProvider.Authorize(context.User, SR.Kirim_pengajuan_order_indent_part_equipment_privilege) Then
            btnValidasi.Visible = True
        End If
    End Sub

    Private Function DisplayTransactionResult(ByVal nID As Integer)
        Dim stt As Boolean = False
        Dim objIndentPartH As IndentPartHeader = New IndentPartHeaderFacade(User).Retrieve(nID)

        ViewState("messCancelIndent") = "Yakin Pembatalan PESANAN akan dibatalkan ?"

        txtPONumber.Text = objIndentPartH.RequestNo
        lblDealerCode.Text = objIndentPartH.Dealer.DealerCode
        lblDealerName.Text = objIndentPartH.Dealer.DealerName
        lblDealerTerm.Text = objIndentPartH.Dealer.SearchTerm2
        ddlPaymentType.SelectedValue = objIndentPartH.PaymentType.ToString()
        lblStatusValue.Text = EnumIndentPartEquipStatus.GetStatusDealerDesc(objIndentPartH.Status) ' objIndentPartH.StatusDealerDesc

        'icConfirmDate.Value = objIndentPartH.KTBConfirmedDate

        objDealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            If objIndentPartH.Status = EnumIndentPartStatus.IndentPartStatus.BARU Then
                btnValidasi.Enabled = bCekSendPriv
                btnDelete.Enabled = True
                icConfirmDate.Enabled = False
            Else
                dtgIPDetail.Columns(7).Visible = False
                btnValidasi.Enabled = False
                btnDelete.Enabled = False
                ddlPaymentType.Enabled = False
                icConfirmDate.Enabled = False
            End If
        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            If objIndentPartH.Status = EnumIndentPartStatus.IndentPartStatus.SENT Then
                btnDelete.Enabled = True
            Else
                btnDelete.Enabled = False
                dtgIPDetail.Columns(7).Visible = False
            End If
            ddlPaymentType.Enabled = False
            btnSave.Enabled = False
            btnValidasi.Enabled = False
            icConfirmDate.Enabled = False
        End If

        Dim objUserInfo As UserInfo = CType(_sesshelper.GetSession("LOGINUSERINFO"), UserInfo)

        icOrderDate.Value = objIndentPartH.CreatedTime  'String.Format("{0:dd/MM/yyyy}", objIndentPartH.PODate)
        _arlIPDetail = objIndentPartH.IndentPartDetails
        txtNoPO.Text = New EstimationEquipPOFacade(User).RetrieveEstimationNumber(_arlIPDetail)
        _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        _sesshelper.SetSession("sessIPHeader", objIndentPartH)
        ViewState.Add("vsAccess", "edit")
        BindIPDetail()
    End Function

    Private Sub BindIPDetail()
        viewstate.Item("TotalTagihan") = 0
        _arlIPDetail = CType(_sesshelper.GetSession("sessIPDetail"), ArrayList)
        dtgIPDetail.DataSource = CType(Session("sessIPDetail"), ArrayList)
        dtgIPDetail.DataBind()
        dtgIPDetail.Columns(7).Visible = False
        calculateTotalAmount(_arlIPDetail)
    End Sub

    Private Sub DetailOnly(ByVal bval As Boolean)
        btnNew.Enabled = False
        'btnDelete.Enabled = False

        If Not _state Then
            ' dari Dealer
            'If _indentPHID <> 0 Then
            '    btnDelete.Enabled = True
            'End If
        Else
            ' dari KTB
            btnSave.Enabled = False
            dtgIPDetail.ShowFooter = False
            'btnDelete.Enabled = False
        End If

    End Sub

    Private Function InitialPageSession() As Boolean
        If Not IsNothing(Session("DEALER")) Then
            ViewState.Add("vsAccess", "insert")
            ViewState.Add("vsSave", "new")
            _sesshelper.SetSession("sessDealer", Session("DEALER")) 'New DealerFacade(User).Retrieve(nID))
            If IsNothing(Session("sessIPDetail")) Then
                _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
            Else
                _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)
            End If
            lblDealerCode.Text = CType(Session("sessDealer"), Dealer).DealerCode
            lblDealerName.Text = CType(Session("sessDealer"), Dealer).DealerName
            lblDealerTerm.Text = CType(Session("sessDealer"), Dealer).SearchTerm2
            lblStatusValue.Text = "Baru"
            Return True
        End If
        Return False
    End Function

    Private Function GetEED(ByVal ipd As IndentPartDetail) As EstimationEquipDetail
        Dim Sql As String = ""
        Dim objEEDFac As EstimationEquipDetailFacade = New EstimationEquipDetailFacade(User)
        Dim crtEED As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), "RowStatus", CType(DBRowStatus.Active, Short)))
        Dim objEED As EstimationEquipDetail
        Dim arlEED As New ArrayList


        Sql &= " select distinct(EstimationEquipDetailID)    "
        Sql &= " from EstimationEquipPO eepo   "
        Sql &= " where 1=1   "
        Sql &= "  and eepo.IndentPartDetailID in (  "
        Sql &= "   select ipd.ID   "
        Sql &= "   from IndentPartDetail ipd "
        Sql &= "   where 1=1 "
        Sql &= "    and ipd.IndentPartHeaderID=" & ipd.IndentPartHeader.ID
        Sql &= "    and ipd.SparePartMasterID=" & ipd.SparePartMaster.ID
        Sql &= " )"


        crtEED.opAnd(New Criteria(GetType(EstimationEquipDetail), "ID", MatchType.InSet, "(" & Sql & ")"))
        arlEED = objEEDFac.Retrieve(crtEED)
        If arlEED.Count > 0 Then
            Return CType(arlEED(0), EstimationEquipDetail)
        Else
            Dim oEED As EstimationEquipDetail = New EstimationEquipDetail
            oEED.ID = -1
            oEED.Harga = ipd.Price
            oEED.ConfirmedDate = Now
            oEED.Discount = 0
            Return oEED  ' New IndentPartDetailFacade(User).Retrieve(ipd.ID)
        End If

    End Function

    Private Sub SetDtgIPDetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objSppoDetail As IndentPartDetail = e.Item.DataItem
        Dim tmpStr As String = ""
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(3).Text = (e.Item.ItemIndex + 1).ToString

        tmpStr &= IIf(tmpStr.Trim = "", "", ";") & objSppoDetail.ID

        Dim lblTotalAmount As Label = CType(e.Item.FindControl("lblJumlah"), Label)
        Dim lblHarga As Label = CType(e.Item.FindControl("lblHarga"), Label)
        Dim price As Decimal = objSppoDetail.Price
        Dim qty As Decimal = objSppoDetail.Qty
        Dim totalAmount As Decimal = price * qty

        tmpStr &= IIf(tmpStr.Trim = "", "", ";") & "IPD.Price=" & objSppoDetail.Price & ",IPD.Qty=" & objSppoDetail.Qty
        'Start  :RemainModule-IndentPart:taking price & discount from EED:dna:20091203
        Dim objEED As EstimationEquipDetail '= GetEED(objSppoDetail)
        Dim Discount As Decimal = 0
        Dim TotalAfterDiscount As Decimal = 0
        If CType(ViewState("vsAccess"), String) = "insert" Then
            objEED = New EstimationEquipDetailFacade(User).Retrieve(objSppoDetail.ID)
        Else 'If CType(ViewState("vsAccess"), String) = "edit" Then
            objEED = GetEED(objSppoDetail)
        End If

        If Not IsNothing(objEED) Then
            Me.icConfirmDate.Value = objEED.ConfirmedDate
            price = objEED.Harga
            totalAmount = (price * qty)
            Discount = objEED.Discount
            TotalAfterDiscount = (price * qty) - objEED.Discount / 100 * (price * qty)
            tmpStr &= IIf(tmpStr.Trim = "", "", ";") & "EED.ID=" & objEED.ID & "EED.Harga=" & objEED.Harga & ",EED.Discount=" & objEED.Discount

        End If
        ViewState.Item("TotalTagihan") = CType(ViewState.Item("TotalTagihan"), Decimal) + TotalAfterDiscount
        'Dim txtRemark As TextBox = New TextBox
        'txtRemark.ID = "txtRemark"
        'txtRemark.Text = tmpStr
        'e.Item.Cells(3).Controls.Add(txtRemark)
        CType(e.Item.FindControl("lblDiscount"), Label).Text = FormatNumber(Discount, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        CType(e.Item.FindControl("lblTagihan"), Label).Text = FormatNumber(TotalAfterDiscount, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

        'End    :RemainModule-IndentPart:taking price & discount from EED:dna:20091203
        lblHarga.Text = FormatNumber(price, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTotalAmount.Text = FormatNumber(totalAmount, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

        Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
        Dim imgIndikator As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgIndikator"), System.Web.UI.WebControls.Image)
        Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
        Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

        Dim lblEstimationNumber As Label = CType(e.Item.FindControl("lblEstimationNumber"), Label)
        If (IsNothing(objSppoDetail.IndentPartHeader)) Then
            Dim objEstDetail As EstimationEquipDetail = New EstimationEquipDetailFacade(User).Retrieve(objSppoDetail.ID)
            If (Not IsNothing(objEstDetail)) Then
                If (Not IsNothing(objEstDetail.EstimationEquipHeader)) Then
                    If (Not IsNothing(objEstDetail.EstimationEquipHeader.EstimationNumber)) Then
                        lblEstimationNumber.Text = objEstDetail.EstimationEquipHeader.EstimationNumber
                    End If
                End If
            End If
        Else
            If e.Item.ItemIndex = 0 Then
                _sesshelper.SetSession("FrmPoEstimationEquip.v_EquipPO", Nothing)
            End If
            If IsNothing(_sesshelper.GetSession("FrmPoEstimationEquip.v_EquipPO")) Then
                Dim objEquipPOTemp As v_EquipPO = New v_EquipPOFacade(User).Retrieve(objSppoDetail.IndentPartHeader.ID)
                _sesshelper.SetSession("FrmPoEstimationEquip.v_EquipPO", objEquipPOTemp)
            End If
            'If Not IsNothing(objEED) Then
            '    Dim lblRemain As Label = e.Item.FindControl("lblRemain")
            '    If Not IsNothing(lblRemain) Then
            '        lblRemain.Text = objEED.EstimationUnit.ToString()
            '    End If
            'End If
            'Dim objEquipPO As v_EquipPO = CType(Me._sesshelper.GetSession("FrmPoEstimationEquip.v_EquipPO"), v_EquipPO) ' New v_EquipPOFacade(User).Retrieve(objSppoDetail.IndentPartHeader.ID)
            'If (Not IsNothing(objEquipPO)) Then
            '    lblEstimationNumber.Text = objEquipPO.EstimationNumber
            '    Dim lblRemain As Label = e.Item.FindControl("lblRemain")
            '    If Not IsNothing(lblRemain) Then
            '        lblRemain.Text = objEquipPO.SisaQty.ToString()
            '    End If
            'End If
        End If

        Dim lblRemain As Label = e.Item.FindControl("lblRemain")
        If Not IsNothing(lblRemain) AndAlso Not IsNothing(objSppoDetail) Then
            'lblRemain.Text = (objSppoDetail.Qty - objSppoDetail.AllocationQty).ToString()
            lblRemain.Text = (objSppoDetail.SisaQty).ToString()
        Else
            lblRemain.Text = "0"
        End If
        If (objSppoDetail.Description = SPPODetailEstimateStatus.BelumKonfirmasi) Then
            imgIndikator.ImageUrl = "../images/red.gif"
            chkItemChecked.Visible = False
        ElseIf (objSppoDetail.Description = SPPODetailEstimateStatus.Konfirmasi_SudahOrder) Then
            imgIndikator.ImageUrl = "../images/yellow.gif"
            chkItemChecked.Visible = False
            lbtnEdit.Visible = False
            lbtnDelete.Visible = False
        ElseIf (objSppoDetail.Description = SPPODetailEstimateStatus.Konfirmasi_BelumOrder) Then
            imgIndikator.ImageUrl = "../images/green.gif"
            chkItemChecked.Visible = True
        ElseIf (objSppoDetail.Description = "") Then
            imgIndikator.ImageUrl = "../images/yellow.gif"
            chkItemChecked.Visible = False
            lbtnEdit.Visible = False
        End If

        'If (Not _blnDealer) Then
        '    lbtnEdit.Visible = False
        '    lbtnDelete.Visible = False
        'End If

    End Sub

    Private Sub SetDtgIPDetailItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objIndentPartDetail As IndentPartDetail = e.Item.DataItem
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(3).Controls.Clear()
        e.Item.Cells(3).Controls.Add(lNum)

        Dim txtQty As TextBox = CType(e.Item.FindControl("txtEQTY"), TextBox)
        Dim lblEQTY As Label = CType(e.Item.FindControl("lblEQTY"), Label)

        If _blnDealer Then
            txtQty.Enabled = True
            lblEQTY.Visible = False
        Else
            txtQty.Enabled = False
            lblEQTY.Visible = True
        End If

        Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
        Dim imgIndikator As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgIndikator"), System.Web.UI.WebControls.Image)
        chkItemChecked.Visible = False
        imgIndikator.Visible = False

    End Sub

    Private Function PartIsExist(ByVal partNumber As String, ByVal arlIPDetail As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each ipDetail As IndentPartDetail In arlIPDetail
            If ipDetail.SparePartMaster.PartNumber.Trim().ToUpper() = partNumber.Trim().ToUpper() Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Private Function PartIsExist(ByVal partNumber As String, ByVal arlIPDetail As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim objTmp As IndentPartDetail = CType(arlIPDetail(nIndeks), IndentPartDetail)
        For i As Integer = 0 To arlIPDetail.Count - 1
            Dim obj As IndentPartDetail = CType(arlIPDetail(i), IndentPartDetail)
            If Not IsNothing(obj) Then
                If Not IsNothing(obj.SparePartMaster) Then
                    If obj.SparePartMaster.PartNumber.Trim().ToUpper() = partNumber.Trim().ToUpper() AndAlso nIndeks <> i Then
                        If (objTmp.Description = obj.Description) Then
                            Return True
                        End If
                    End If
                End If
            End If
        Next
        Return False
    End Function

    Private Sub RemoveAllSession()
        _sesshelper.RemoveSession("sessIPHeader")
        _sesshelper.RemoveSession("sessIPDetail")
        lblTotalAmount.Text = "Rp. 0"
    End Sub

    Private Function cekValiditas() As Boolean
        cekValiditas = False
        If (dtgIPDetail.Items.Count <= 0) Then Return False
        Return True
    End Function

    Private Function InsertNewSPPOPart() As Integer
        Dim eqpDetailF As EstimationEquipDetailFacade = New EstimationEquipDetailFacade(User)

        Dim ObjIndent As IndentPartHeader = GetIndentHeader()
        'ObjIndent.Status = EnumIndentPartStatus.IndentPartStatusDealer.Baru
        ObjIndent.Status = EnumIndentPartEquipStatus.EnumStatusDealer.Baru
        ObjIndent.MaterialType = EnumMaterialType.MaterialType.Equipment
        ObjIndent.RequestDate = icOrderDate.Value
        ObjIndent.PaymentType = CByte(ddlPaymentType.SelectedValue)
        ObjIndent.RowStatus = DBRowStatus.Active
        ObjIndent.TermOfPayment = New TermOfPayment(_ConstTOPID)
        Dim arlPoDetail As ArrayList = New ArrayList
        Dim arlEqpDetail As ArrayList = New ArrayList

        For Each item As DataGridItem In dtgIPDetail.Items
            Dim chk As CheckBox = item.FindControl("chkItemChecked")
            If chk.Checked Then
                Dim intEstEquipDetail As Integer = CType(item.Cells(1).Text, Integer)
                Dim lblEstimationUnit As Label = CType(item.FindControl("lblEstimationUnit"), Label)
                Dim objEqpDetail As EstimationEquipDetail = eqpDetailF.Retrieve(intEstEquipDetail)

                Dim objSPPODetail As IndentPartDetail = New IndentPartDetail
                objSPPODetail.Qty = Convert.ToInt32(lblEstimationUnit.Text)
                objSPPODetail.Price = objEqpDetail.Harga
                objSPPODetail.SparePartMaster = objEqpDetail.SparePartMaster
                objSPPODetail.Description = SPPODetailEstimateStatus.Konfirmasi_SudahOrder
                objSPPODetail.RowStatus = DBRowStatus.Active
                arlPoDetail.Add(objSPPODetail)

                objEqpDetail.EstimationUnit = Convert.ToInt32(lblEstimationUnit.Text)
                objEqpDetail.Status = EnumEstimationEquipStatus.EstimationEquipStatusDetail.Konfirmasi_SudahOrder
                arlEqpDetail.Add(objEqpDetail)
            End If
        Next


        'Start  :RemainModule-IndentPart/CR-sum the same item
        Dim arlNewIPD As New ArrayList
        Dim IsExist As Boolean = False
        Dim idx As Integer
        Dim oNewIPD As IndentPartDetail

        For Each oOldIPD As IndentPartDetail In arlPoDetail
            IsExist = False
            For idx = 0 To arlNewIPD.Count - 1
                oNewIPD = CType(arlNewIPD(idx), IndentPartDetail)
                If oOldIPD.SparePartMaster.PartNumber = oNewIPD.SparePartMaster.PartNumber Then
                    oNewIPD.Qty += oOldIPD.Qty - oOldIPD.SisaQty
                    arlNewIPD.Item(idx) = oNewIPD
                    IsExist = True
                    Exit For
                End If
            Next
            If Not IsExist Then
                arlNewIPD.Add(oOldIPD)
            End If
        Next
        arlPoDetail = arlNewIPD
        'End    :RemainModule-IndentPart/CR-sum the same item


        '===Check wheter item has been ordered before, add by wdi 20161021
        Dim strPartNumExist As String = ""
        Dim arrTmp As ArrayList

        IsExist = False
        For Each item As DataGridItem In dtgIPDetail.Items
            Dim chk As CheckBox = item.FindControl("chkItemChecked")
            If chk.Checked Then
                Dim intEstEquipDetail As Integer = CType(item.Cells(1).Text, Integer)
                arrTmp = New EstimationEquipPOFacade(User).RetrieveByEstimationEquipDetailID(intEstEquipDetail)

                If arrTmp.Count > 0 Then
                    For Each objEEP As EstimationEquipPO In arrTmp
                        If objEEP.IndentPartDetail.IndentPartHeader.Status <> 3 Then
                            strPartNumExist = strPartNumExist & CType(item.FindControl("Label3"), Label).Text & ","
                            IsExist = True
                            Exit For
                        End If
                    Next
                End If
            End If
        Next

        If IsExist Then
            strPartNumExist = Left(strPartNumExist, Len(strPartNumExist) - 1)
            MessageBox.Show("Item berikut sudah pernah diajukan : " & strPartNumExist)
            Return -1
        End If
        '===End: Check wheter item has been ordered before


        Dim sppoF As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        Dim iResult As Integer = sppoF.InsertIndentPartheader(ObjIndent, arlPoDetail)

        If iResult = -1 Then
            MessageBox.Show("Simpan Pengajuan/Order gagal")
            Return -1
        End If

        Dim eqpPoF As EstimationEquipPOFacade = New EstimationEquipPOFacade(User)
        Dim sppodetailF As IndentPartDetailFacade = New IndentPartDetailFacade(User)

        For Each objEqpDetail As EstimationEquipDetail In arlEqpDetail
            Dim objEqpPO As EstimationEquipPO = New EstimationEquipPO
            objEqpPO.EstimationEquipDetail = objEqpDetail
            objEqpPO.IndentPartDetail = sppodetailF.RetrieveByHeaderID_SPMasterID(iResult, objEqpDetail.SparePartMaster.ID)
            objEqpPO.RowStatus = DBRowStatus.Active
            eqpPoF.Insert(objEqpPO)
            eqpDetailF.Update(objEqpDetail) 'it's dependence of IPH & IPD Transaction (Insertion)
        Next

        Dim objPOInfo As EquipSPPOInfo = New EquipSPPOInfo
        Dim poinfof As EquipSPPOInfoFacade = New EquipSPPOInfoFacade(User)
        objPOInfo.IndentPartHeader = ObjIndent
        objPOInfo.Note = ""
        objPOInfo.Description = ""
        poinfof.Insert(objPOInfo)

        _arlIPDetail = arlPoDetail
        _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        _sesshelper.SetSession("sessIPHeader", ObjIndent)
        Return iResult
    End Function

    Private Function GetIndentHeader() As IndentPartHeader
        Dim ObjIndent As New IndentPartHeader
        If IsNothing(_sesshelper.GetSession("sessIPHeader")) Then
            ObjIndent = New IndentPartHeader
            ObjIndent.Dealer = CType(Session("sessDealer"), Dealer)
            _sesshelper.SetSession("sessIPHeader", ObjIndent)
        Else
            ObjIndent = CType(Session("sessIPHeader"), IndentPartHeader)
        End If
        Return ObjIndent
    End Function

    Private Sub MergeIPDetail(ByVal nID As Integer)
        Dim objIPH As IndentPartHeader = New IndentPartHeaderFacade(User).Retrieve(nID)
        _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)
        For Each objIPDetailOrig As IndentPartDetail In objIPH.IndentPartDetails
            objIPDetailOrig.RowStatus = DBRowStatus.Deleted
        Next

        Dim found As Boolean
        For Each objIPDetail As IndentPartDetail In _arlIPDetail
            found = False
            For Each objIPDetailOrig As IndentPartDetail In objIPH.IndentPartDetails
                If objIPDetail.SparePartMaster.PartNumber.Trim().ToUpper() = objIPDetailOrig.SparePartMaster.PartNumber.Trim().ToUpper() Then
                    objIPDetailOrig.RowStatus = DBRowStatus.Active
                    objIPDetailOrig.Qty = objIPDetail.Qty
                    found = True
                    Exit For
                End If
                found = False
            Next
            If Not found Then
                objIPH.IndentPartDetails.Add(objIPDetail)
            End If
        Next
        _sesshelper.SetSession("sessIPDetail", objIPH.IndentPartDetails)
    End Sub

    Private Function EditIP() As Integer
        _indentPHID = Request.QueryString("SPPOID")
        Dim ObjIndent As IndentPartHeader = CType(_sesshelper.GetSession("sessIPHeader"), IndentPartHeader)
        Dim sppof As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        If (_indentPHID <> 0) Then
            ObjIndent = sppof.Retrieve(_indentPHID)
            _indentPHID = ObjIndent.ID
        End If

        ObjIndent.RequestDate = icOrderDate.Value
        ObjIndent.PaymentType = Convert.ToByte(ddlPaymentType.SelectedValue)
        ObjIndent.RowStatus = DBRowStatus.Active
        ObjIndent.TermOfPayment = New TermOfPayment(_ConstTOPID)
        'Dim oldStatus As Integer = ObjIndent.Status

        'Start  :RemainModule-IndentPart/CR-sum the same item
        Dim arlOldIPD As ArrayList = CType(Session("sessIPDetail"), ArrayList)
        Dim arlNewIPD As New ArrayList
        Dim idx As Integer
        Dim IsExist As Boolean = False
        Dim oNewIPD As IndentPartDetail
        For Each oOldIPD As IndentPartDetail In arlOldIPD 'arlPoDetail
            IsExist = False
            For idx = 0 To arlNewIPD.Count - 1
                oNewIPD = CType(arlNewIPD(idx), IndentPartDetail)
                If oOldIPD.SparePartMaster.PartNumber = oNewIPD.SparePartMaster.PartNumber Then
                    oNewIPD.Qty += oOldIPD.Qty - oOldIPD.SisaQty
                    arlNewIPD.Item(idx) = oNewIPD
                    IsExist = True
                    Exit For
                End If
            Next
            If Not IsExist Then
                arlNewIPD.Add(oOldIPD)
            End If
        Next
        arlOldIPD = arlNewIPD
        _sesshelper.SetSession("sessIPDetail", arlOldIPD)
        'End    :RemainModule-IndentPart/CR-sum the same item


        Dim iResult As Integer = sppof.UpdateIndentPartheader(ObjIndent, CType(Session("sessIPDetail"), ArrayList))
        'sppof.RecordStatusChangeHistory(ObjIndent, oldStatus)
        _sesshelper.SetSession("sessIPHeader", ObjIndent)
        _sesshelper.SetSession("sessIPDetail", ObjIndent.IndentPartDetails)
        _arlIPDetail = ObjIndent.IndentPartDetails

        Return iResult
    End Function

    Private Sub calculateTotalAmount(ByVal arldetail As ArrayList)
        Dim decTotal As Decimal = 0
        Dim decPPN As Decimal = 0

        'lblTagihan
        'For Each objTmpDetail As IndentPartDetail In arldetail
        '    'decTotal += (objTmpDetail.Qty * objTmpDetail.Price)
        '    Dim oEED As EstimationEquipDetail = GetEED(objTmpDetail)
        '    decTotal += (objTmpDetail.Qty * oEED.Harga) - ((oEED.Discount / 100) * (objTmpDetail.Qty * oEED.Harga))
        'Next
        'For Each di As DataGrid In Me.dtgIPDetail.Items
        '    decTotal += CType(CType(di.FindControl("lblTagihan"), Label).Text, Decimal)
        'Next
        'For Each di As DataGridItem In dtgIPDetail.Items
        '    Dim lblTagihan As Label = di.FindControl("lblTagihan")
        '    decTotal += CType(lblTagihan.Text, Decimal)
        'Next
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(icOrderDate.Value, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
        decTotal = CType(viewstate.Item("TotalTagihan"), Decimal)
        'decPPN = 0.1 * decTotal
        decPPN = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=decTotal)
        lblTotalAmount.Text = "Rp. " & FormatNumber(decTotal, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTotalPPN.Text = "Rp. " & FormatNumber(decPPN, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTotalTagihan.Text = "Rp. " & FormatNumber((decTotal + decPPN), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
    End Sub

    Private Sub BindDropDown()
        CommonFunction.BindFromEnum("IndentPartPaymentType", ddlPaymentType, Me.User, False, "NameType", "ValType")
    End Sub

    Public Function sendDeposit_C(ByVal objSppo As IndentPartHeader)
        Dim oDealer As Dealer = _sesshelper.GetSession("DEALER")
        Dim euf As EquipUserFacade = New EquipUserFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim mail As DNetMail = New DNetMail(smtp)
        Dim szTos As String '= euf.CreateEmailToString(EquipUser.EquipUserGroup.Deposit_C)   ' .Pengajuan_Estimasi)  'approved 
        Dim szCcs As String '= euf.CreateEmailCcString(EquipUser.EquipUserGroup.Deposit_C)   ' .Pengajuan_Estimasi) ' .Approved)
        Dim szItems As String = ""
        Dim i As Integer = 0
        Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")
        Dim Total As Decimal = 0
        Dim ItemPrice As Decimal = 0
        Dim objEED As EstimationEquipDetail
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objSppo.RequestDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Deposit_C, szTos, szCcs, oDealer)
        For Each objItem As IndentPartDetail In objSppo.IndentPartDetails
            If objItem.RowStatus = CType(DBRowStatus.Active, Short) AndAlso (objItem.Description.Trim = String.Empty OrElse objItem.Description = SPPODetailEstimateStatus.Konfirmasi_SudahOrder) Then
                objEED = GetEED(objItem)
                i += 1
                szItems += "<tr>"
                szItems += String.Format("<td>{0}</td>", i)
                szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartNumber)
                szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartName)
                szItems += String.Format("<td>{0}</td>", IIf(objItem.SparePartMaster.SupplierCode = "", "-", objItem.SparePartMaster.SupplierCode))
                szItems += String.Format("<td align='center'>{0}</td>", objItem.Qty)
                szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Harga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
                szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Discount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
                ItemPrice = (objItem.Qty * objEED.Harga) - objEED.Discount / 100 * (objItem.Qty * objEED.Harga)
                szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(ItemPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
                szItems += "</tr>"
                Total += ItemPrice
            End If
        Next

        'Dim TotalPPN As Decimal = 0.1 * Total
        Dim TotalPPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=Total)
        szItems += "<tr>"
        szItems += "<td colspan='6' rowspan='3'>. &nbsp;</td>"
        szItems += "<td><b>Total Order</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>PPN</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>Total Tagihan</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(Total + TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        'Dim szFormats() As String = {objSppo.Dealer.DealerCode, objSppo.Dealer.DealerName, Format(objSppo.RequestDate, "dd/MM/yyyy"), objSppo.RequestNo, String.Format("Rp.{0}", FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)), "Rp." & FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True), "Rp." & FormatNumber(TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True), "Rp." & FormatNumber(Total + TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True), szItems}

        Dim szFormats() As String = {objSppo.Dealer.DealerCode _
        , objSppo.Dealer.DealerName _
        , objSppo.Dealer.City.CityName _
        , Format(objSppo.RequestDate, "dd/MM/yyyy") _
        , objSppo.RequestNo _
        , szItems}
        mail.sendMail(Server.MapPath(TEMP_EMAIL_APPROVED), szTos, szCcs, "[MMKSI-DNet] Parts - Pengajuan Order Dep.C - " & objSppo.RequestNo & " - " & objSppo.Dealer.DealerCode, szFormats)
    End Function

    Private Function sendDeposit_B_Old(ByVal objSppo As IndentPartHeader)
        Dim oDealer As Dealer = _sesshelper.GetSession("DEALER")
        Dim euf As EquipUserFacade = New EquipUserFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim mail As DNetMail = New DNetMail(smtp)
        Dim szTos As String '= euf.CreateEmailToString(EquipUser.EquipUserGroup.Deposit_B)
        Dim szCcs As String '= euf.CreateEmailCcString(EquipUser.EquipUserGroup.Deposit_B)
        Dim szItems As String = ""
        Dim i As Integer = 0
        Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")
        Dim Total As Decimal = 0
        Dim ItemPrice As Decimal = 0
        Dim objEED As EstimationEquipDetail
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objSppo.RequestDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Deposit_B, szTos, szCcs, oDealer)
        For Each objItem As IndentPartDetail In objSppo.IndentPartDetails
            If objItem.Description.Trim = String.Empty OrElse objItem.Description = SPPODetailEstimateStatus.Konfirmasi_SudahOrder Then
                objEED = GetEED(objItem)
                i += 1
                szItems += "<tr>"
                szItems += String.Format("<td>{0}</td>", i)
                szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartNumber)
                szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartName)
                szItems += String.Format("<td>{0}</td>", IIf(objItem.SparePartMaster.SupplierCode = "", "-", objItem.SparePartMaster.SupplierCode))
                szItems += String.Format("<td align='center'>{0}</td>", objItem.Qty)
                szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Harga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
                szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Discount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
                ItemPrice = (objItem.Qty * objEED.Harga) - objEED.Discount / 100 * (objItem.Qty * objEED.Harga)
                szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(ItemPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
                szItems += "</tr>"
                Total += ItemPrice
            End If
        Next
        'Dim TotalPPN As Decimal = 0.1 * Total
        Dim TotalPPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=Total)
        'Dim szFormats() As String = {objSppo.Dealer.DealerCode, objSppo.Dealer.DealerName, Format(objSppo.RequestDate, "dd/MM/yyyy"), objSppo.RequestNo, String.Format("Rp.{0}", FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)), "Rp." & FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True), "Rp." & FormatNumber(TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True), "Rp." & FormatNumber(Total + TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True), szItems}
        Dim szFormats() As String = {objSppo.Dealer.DealerCode _
       , objSppo.Dealer.DealerName _
       , Format(objSppo.RequestDate, "dd/MM/yyyy") _
       , objSppo.RequestNo _
       , szItems}
        'mail.sendMail(Server.MapPath("../IndentPartEquipment/EmailTemplateDepositeBSPPOSimple.htm"), szTos, szCcs, objSppo.RequestNo, szFormats)
        mail.sendMail(Server.MapPath(TEMP_EMAIL_DEPOSIT_B), szTos, szCcs, "[MMKSI-DNet] Parts - Pengajuan Order Deposit B - " & objSppo.RequestNo, szFormats)
    End Function

    Public Function sendDeposit_B(ByVal objSppo As IndentPartHeader)
        Dim oDealer As Dealer = _sesshelper.GetSession("DEALER")
        Dim euf As EquipUserFacade = New EquipUserFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim mail As DNetMail = New DNetMail(smtp)
        Dim szTos As String '= euf.CreateEmailToString(EquipUser.EquipUserGroup.Deposit_B)
        Dim szCcs As String '= euf.CreateEmailCcString(EquipUser.EquipUserGroup.Deposit_B)
        Dim szItems As String = ""
        Dim i As Integer = 0
        Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")
        Dim Total As Decimal = 0
        Dim ItemPrice As Decimal = 0
        Dim objEED As EstimationEquipDetail
        Dim ConfirmDate As Date

        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objSppo.RequestDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Deposit_B, szTos, szCcs, objSppo.Dealer)
        For Each objItem As IndentPartDetail In objSppo.IndentPartDetails
            If objItem.RowStatus = CType(DBRowStatus.Active, Short) AndAlso (objItem.Description.Trim = String.Empty OrElse objItem.Description = SPPODetailEstimateStatus.Konfirmasi_SudahOrder) Then
                objEED = GetEED(objItem)
                ConfirmDate = objEED.ConfirmedDate
                i += 1
                szItems += "<tr>"
                szItems += String.Format("<td>{0}</td>", i)
                szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartNumber)
                szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartName)
                szItems += String.Format("<td>{0}</td>", IIf(objItem.SparePartMaster.SupplierCode = "", "-", objItem.SparePartMaster.SupplierCode))
                szItems += String.Format("<td align='center'>{0}</td>", objItem.Qty)
                szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Harga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
                szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Discount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
                ItemPrice = (objItem.Qty * objEED.Harga) - objEED.Discount / 100 * (objItem.Qty * objEED.Harga)
                szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(ItemPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
                szItems += "</tr>"
                Total += ItemPrice
            End If
        Next
        'Dim TotalPPN As Decimal = 0.1 * Total
        Dim TotalPPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=Total)
        szItems += "<tr>"
        szItems += "<td colspan='6' rowspan='3'>. &nbsp;</td>"
        szItems += "<td><b>Total Order</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>PPN</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>Total Tagihan</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(Total + TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        'Dim szFormats() As String = {objSppo.Dealer.DealerCode _
        ', objSppo.Dealer.DealerName _
        ', Format(objSppo.RequestDate, "dd/MM/yyyy") _
        ', objSppo.RequestNo _
        ', String.Format("Rp.{0}", FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)) _
        ', "Rp." & FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) _
        ', "Rp." & FormatNumber(TotalPPN, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) _
        ', "Rp." & FormatNumber(Total + TotalPPN, 2, TriState.UseDefault, TriState.UseDefault, TriState.True) _
        ', szItems}
        Dim sCC As String = ""

        Dim EmailIndentCC1 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC1")
        Dim EmailIndentCC2 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC2")
        Dim EmailIndentCC3 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC3")
        Dim EmailIndentCC4 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC4")
        Dim EmailIndentCC5 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC5")

        sCC &= "<tr><td>" & EmailIndentCC1 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC2 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC3 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC4 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC5 & "</td></tr>"


        Dim sCatatan As String = ""
        sCatatan &= "<tr><td width='10px'></td><td></td></tr>"
        sCatatan &= "<tr><td colspan='2'><b>Catatan Penting:</b></td></tr>" 'objSppo.RequestDate
        sCatatan &= "<tr><td>-</td><td>Untuk pembayaran melalui Deposit B. Kwitansi paling lambat diterima oleh MMKSI-Technical Service pada " & ConfirmDate.AddDays(14).ToString("dd MMM yyyy") & "</td></tr>"
        sCatatan &= "<tr><td>&nbsp;</td><td></td></tr>"
        sCatatan &= "<tr><td colspan='2'>Dengan Persyaratan : </td></tr>"
        sCatatan &= "<tr><td>1.</td><td>Surat Pengajuan Deposit B Dealer</td></tr>"
        'sCatatan &= "<tr><td>2.</td><td>Surat Persetujuan KTB</td></tr>"
        sCatatan &= "<tr><td>2.</td><td>Kwitansi Asli</td></tr>"
        sCatatan &= "<tr><td>3.</td><td>Print Out Pengajuan Order D-NET</td></tr>"
        Dim szFormats() As String = {objSppo.Dealer.DealerCode _
        , objSppo.Dealer.DealerName _
        , objSppo.Dealer.City.CityName _
        , Format(objSppo.RequestDate, "dd/MM/yyyy") _
        , objSppo.RequestNo _
        , szItems _
        , sCC _
        , sCatatan}
        mail.sendMail(Server.MapPath("../IndentPartEquipment/EmailTemplateDepositeBSPPOSimple.htm"), szTos, szCcs, "[MMKSI-DNet] Parts - Pengajuan Order Dep.B - " & objSppo.RequestNo & " - " & objSppo.Dealer.DealerCode, szFormats)
    End Function

    Private Sub UpdateStatus(ByVal objIPH As IndentPartHeader, ByVal nUpdateStatusKTB As Integer)
        Dim objIPHFac As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        Dim NewKTBStatus As Integer
        Dim NewDealerStatus As Integer
        Dim strMessage As String = ""
        Dim OldStatus As Integer

        If EnumIndentPartEquipStatus.IsValidUpdateKTB(nUpdateStatusKTB, objIPH.StatusKTB, NewKTBStatus, objIPH.Status, NewDealerStatus, objIPH.PaymentType, strMessage) Then
            OldStatus = objIPH.Status
            objIPH.StatusKTB = NewKTBStatus
            objIPH.Status = NewDealerStatus
            objIPHFac.Update(objIPH)
            objIPHFac.RecordStatusChangeHistory(objIPH, OldStatus)
        Else
            MessageBox.Show(strMessage)
        End If
    End Sub

#End Region

End Class
