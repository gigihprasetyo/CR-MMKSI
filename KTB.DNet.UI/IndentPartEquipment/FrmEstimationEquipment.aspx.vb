Imports System.IO
Imports System.Text

Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.IndentPart
Imports Ktb.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Parser

Public Class FrmEstimationEquipment
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
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
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents btnCetak As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnClose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents icOrderDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblTotalAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblUploadlbl As System.Web.UI.WebControls.Label
    Protected WithEvents lblUploadDotlbl As System.Web.UI.WebControls.Label
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents pnlUpload As System.Web.UI.WebControls.Panel
    Protected WithEvents RegUpload As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents hdnValNew As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "private variable"

    Dim _arlIPDetail As ArrayList = New ArrayList
    Dim _sesshelper As New SessionHelper
    Dim objDealer As Dealer
    Dim _state As Boolean
    Dim _blnDealer As Boolean
    Dim _indentPHID As Integer
    Dim _isPopup As Integer = 0
    Dim bCekSendPriv As Boolean = SecurityProvider.Authorize(context.User, SR.PengajuanIndentPartKirim_Privilege)

#End Region

#Region "event handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        objDealer = _sesshelper.GetSession("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            InitiateAuthorization()
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            _state = False
            dtgIPDetail.ShowFooter = False
        Else
            dtgIPDetail.ShowFooter = True
            btnCetak.Visible = True
        End If

        _indentPHID = Request.QueryString("EstimationEquipHeaderID")
        _state = Convert.ToBoolean(Request.QueryString("View"))

        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            _blnDealer = True
        Else
            _blnDealer = False
        End If

        If _indentPHID = 0 Or _isPopup = 1 Then
            btnCancel.Enabled = False
        End If

        If _isPopup <> 1 Then
            btnClose.Visible = False
        End If

        If Not bCekSendPriv Then
            btnValidasi.Enabled = False
        End If

        '!!!!!! excel upload mode
        checkExcelUpload()
        '!!!!!!

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

        'if ! postback
        ViewState("CurrentSortColumn") = "SparePartMaster.PartNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        If _indentPHID > 0 Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                btnCetak.Disabled = False
            End If
            Dim obIPH As EstimationEquipHeader = New EstimationEquipHeaderFacade(User).Retrieve(_indentPHID)
            ViewState.Add("vsAccess", "edit")
            DisplayTransactionResult(_indentPHID)
            DetailOnly(True)

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                btnDelete.Enabled = False
                btnValidasi.Enabled = False
                btnSave.Enabled = False
            End If
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

    Private Sub dtgIPDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgIPDetail.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            SetDtgIPDetailItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetDtgIPDetailItemEdit(e)
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            SetDtgIPDetailItemFooter(e)
        End If
    End Sub

    Private Sub dtgIPDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgIPDetail.ItemCommand
        _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)

        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtFPartNumber"), TextBox)
                Dim txtQty As TextBox = CType(e.Item.FindControl("txtFQTY"), TextBox)
                Dim txtAddPriceNew As TextBox = CType(e.Item.FindControl("txtAddPrice"), TextBox)


                Dim objPart As SparePartMaster
                If IsNothing(txtPartNumber) OrElse txtPartNumber.Text = String.Empty Then
                    MessageBox.Show("No.Part masih kosong")
                    Return
                Else
                    objPart = New SparePartMasterFacade(User).RetrieveByCodeAndType(txtPartNumber.Text.Trim().ToUpper(), EnumEstimationEquipStatus.TYPE_EQUIPMENT)
                End If
                'Start  :RemainModule-IndentPart:only active part could be included in this transaction
                If objpart.ActiveStatus <> EnumSparePartActiveStatus.SparePartActiveStatus.Active Then
                    MessageBox.Show("No. Part sudah tidak aktif")
                    Return
                End If
                'End    :RemainModule-IndentPart:only active part could be included in this transaction
                'If objPart.RetalPrice <> Val(txtAddPriceNew.Text.Replace(".", "")) Then
                '    If (txtAddPriceNew.Text <> "" And txtAddPriceNew.Text <> "0" And Val(txtAddPriceNew.Text.Replace(".", "")) <> 0) Then
                '        MessageBox.Show("Harga tidak sama dengan data master Sparepart. Silakan pilih Sparepart melalui tombol popup atau kosongkan harga, atau isi dengan angka 0.")
                '        Return
                '    Else
                '        txtAddPriceNew.Text = objPart.RetalPrice
                '    End If
                'End If
                txtAddPriceNew.Text = objPart.RetalPrice
                If IsNothing(txtQty) OrElse txtQty.Text = String.Empty OrElse CType(txtQty.Text.Trim, Integer) <= 0 Then
                    MessageBox.Show("Unit Pesanan tidak boleh kosong/0")
                    If Not (IsNothing(objPart) OrElse objPart.ID = 0) Then
                        RenderPartItem(objPart, e)
                    End If
                    Return
                End If

                If Not (IsNothing(objPart) OrElse objPart.ID = 0) Then
                    If Not PartIsExist(objPart.PartNumber, _arlIPDetail) Then
                        Dim objIPDetail As EstimationEquipDetail = New EstimationEquipDetail
                        objIPDetail.EstimationUnit = CType(IIf(txtQty.Text = "", "0", txtQty.Text), Integer)
                        objIPDetail.SparePartMaster = objPart
                        objIPDetail.Harga = CType(IIf(txtAddPriceNew.Text = "", "0", txtAddPriceNew.Text), Decimal)
                        _arlIPDetail.Add(objIPDetail)

                        calculateTotalAmount(_arlIPDetail)

                        If CType(ViewState("vsAccess"), String) = "edit" Then
                            objIPDetail.EstimationEquipHeader = CType(Session("sessIPHeader"), EstimationEquipHeader)
                            Dim nResult As Integer = New EstimationEquipDetailFacade(User).Insert(objIPDetail)
                        End If
                        If Not IsNothing(Request.QueryString("IndentPartHeaderID")) Then
                            btnNew.Enabled = False
                            btnSave.Enabled = False
                        Else
                            btnNew.Enabled = True
                            btnSave.Enabled = True
                        End If
                        Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")
                        icOrderDate.Enabled = False
                    Else
                        MessageBox.Show(SR.DataIsExist("Spare Part"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Spare Part"))
                End If

            Case "edit" 'Edit mode activated
                dtgIPDetail.ShowFooter = False
                btnSave.Enabled = False
                dtgIPDetail.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Try
                    Dim objIPDetail As EstimationEquipDetail = _arlIPDetail(e.Item.ItemIndex)
                    If objipdetail.ID > 0 Then
                        Dim result As Integer = New EstimationEquipDetailFacade(User).DeleteFromDB(objIPDetail)
                        If result = -1 Then
                            MessageBox.Show(SR.DeleteFail)
                            Exit Sub
                        End If
                    End If
                    _arlIPDetail.RemoveAt(e.Item.ItemIndex)
                    btnSave.Enabled = cekValiditas()
                Catch ex As Exception
                End Try

            Case "save" 'Update this datagrid item                 
                Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtEPartNumber"), TextBox)
                Dim txtQty As TextBox = CType(e.Item.FindControl("txtEQTY"), TextBox)
                Dim txtEditPriceNew As TextBox = CType(e.Item.FindControl("txtEditPrice"), TextBox)
                Dim txtEditPriceTempx As TextBox = CType(e.Item.FindControl("txtEditPriceTemp"), TextBox)
                Dim lblEPopUpSparePartNew As Label = CType(e.Item.FindControl("lblEPopUpSparePart"), Label)
                Dim lblEPartNumber As Label = CType(e.Item.FindControl("lblEPartNumber"), Label)
                Dim lblEQTY As Label = CType(e.Item.FindControl("lblEQTY"), Label)
                Dim lblEditPrice As Label = CType(e.Item.FindControl("lblEditPrice"), Label)

                Dim nResult As Integer

                If Not _blnDealer Then
                    ' case KTB
                    txtPartNumber.Text = lblEPartNumber.Text
                    txtQty.Text = lblEQTY.Text
                End If

                Dim objPart As SparePartMaster
                If IsNothing(txtPartNumber) OrElse txtPartNumber.Text = String.Empty Then
                    MessageBox.Show("No.Part masih kosong")
                    Return
                Else
                    objPart = New SparePartMasterFacade(User).RetrieveByCodeAndType(txtPartNumber.Text.Trim().ToUpper(), EnumEstimationEquipStatus.TYPE_EQUIPMENT)
                End If
                'Start  :RemainModule-IndentPart:only active part could be included in this transaction
                If objpart.ActiveStatus <> EnumSparePartActiveStatus.SparePartActiveStatus.Active Then
                    MessageBox.Show("No. Part sudah tidak aktif")
                    Return
                End If
                'End    :RemainModule-IndentPart:only active part could be included in this transaction 
                'If _blnDealer Then
                '    If objpart.RetalPrice <> Val(txtEditPriceTempx.Text.Replace(".", "")) Then
                '        MessageBox.Show("Harga tidak sama dengan data master Sparepart. Silakan pilih Sparepart melalui tombol popup.")
                '        Return
                '    End If
                'End If
                txtEditPriceTempx.Text = objPart.RetalPrice
                If IsNothing(txtQty) OrElse txtQty.text = String.Empty OrElse CType(txtQty.text.Trim, Integer) <= 0 Then
                    MessageBox.Show("Unit Pesanan tidak boleh kosong/0")
                    Return
                    If Not (IsNothing(objPart) OrElse objPart.ID = 0) Then
                        RenderPartItem(objPart, e)
                    End If
                    Return
                End If

                If Not (IsNothing(objPart) Or objPart.ID = 0) Then
                    If Not PartIsExist(txtPartNumber.Text.Trim().ToUpper(), _arlIPDetail, e.Item.ItemIndex) Then
                        Dim objIPDetail As EstimationEquipDetail = CType(_arlIPDetail(e.Item.ItemIndex), EstimationEquipDetail)
                        objIPDetail.EstimationUnit = CType(txtQty.Text, Integer)
                        objIPDetail.SparePartMaster = objPart

                        objDealer = _sesshelper.GetSession("DEALER")

                        'state= true -> dr KTB, hanya bs update pricenya saja
                        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                            objIPDetail.Harga = CType(IIf(txtEditPriceNew.Text = "", "0", txtEditPriceNew.Text), Decimal)
                        Else
                            objIPDetail.Harga = CType(IIf(txtEditPriceTempx.Text = "", "0", txtEditPriceTempx.Text), Decimal)
                        End If

                        If CType(ViewState("vsAccess"), String) = "edit" Then
                            objIPDetail.EstimationEquipHeader = CType(Session("sessIPHeader"), EstimationEquipHeader)
                            nResult = New EstimationEquipDetailFacade(User).Update(objIPDetail)
                        End If
                        dtgIPDetail.EditItemIndex = -1
                        If _blnDealer Then
                            dtgIPDetail.ShowFooter = True
                        Else
                            ' KTB hanya bs update pricenya saja
                            dtgIPDetail.ShowFooter = False
                        End If
                        btnSave.Enabled = cekValiditas()
                        objIPDetail.ErrorMessage = ""
                    Else
                        MessageBox.Show(SR.DataIsExist("Spare Part"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Spare Part"))
                End If

            Case "cancel" 'Cancel Update this datagrid item 
                dtgIPDetail.EditItemIndex = -1
                If _blnDealer Then
                    dtgIPDetail.ShowFooter = True
                Else
                    dtgIPDetail.ShowFooter = False
                End If
                btnSave.Enabled = cekValiditas()

        End Select
        _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        BindIPDetail()

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

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        btnCetak.Disabled = True
        btnUpload.Enabled = True
        _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)
        If CType(ViewState("vsSave"), String) = "new" AndAlso _arlIPDetail.Count > 0 Then
            ViewState.Add("vsSave", "false")
            MessageBox.Confirm("Data belum disimpan. Apakah anda akan membuat pengajuan baru?", "hdnValNew")
        Else
            ViewState.Add("vsAccess", "insert")
            ViewState.Add("vsSave", "new")

            RemoveAllSession()
            txtPONumber.Text = "[Dibuat oleh sistem]"
            lblStatusValue.Text = "Baru"

            icOrderDate.Value = Date.Now
            _arlIPDetail.Clear()
            _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
            BindIPDetail()
            btnDelete.Enabled = False
            btnNew.Enabled = False
            btnSave.Enabled = False
            btnValidasi.Enabled = False
            btnCancel.Enabled = False
        End If
        dtgIPDetail.Columns(dtgIPDetail.Columns.Count - 1).Visible = True
        dtgIPDetail.ShowFooter = True
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If CType(Session("sessIPDetail"), ArrayList).Count > 0 Then
            If Not cekValiditas() Then
                MessageBox.Show("Data belum valid seluruhnya, mohon periksa kembali")
                Return
            End If

            Dim nResult As Integer

            Select Case CType(ViewState("vsAccess"), String)
                Case "insert"
                    Try
                        nResult = InsertNewIndentPart()

                        If nResult <> -1 Then
                            DisplayTransactionResult(nResult)
                            ViewState.Add("vsAccess", "edit")
                            If CType(ViewState("vsSave"), String) = "false" Then
                                MessageBox.Show(SR.SaveSuccess & " : No PO = " & txtPONumber.Text)
                            Else
                                MessageBox.Show(SR.SaveSuccess)
                            End If
                            ViewState.Add("vsSave", "true")

                            btnDelete.Enabled = True
                            If Not IsNothing(Request.QueryString("IndentPartHeaderID")) Then
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
                        MessageBox.Show("Gagal simpan Estimation Equipment Part " & ex.Message)
                        Return
                    End Try

                Case "edit"
                    'refer bug 1086

                    Dim idIPH As Integer = 0
                    If Val(Request.QueryString("IndentPartHeaderID")) > 0 Then
                        idIPH = Request.QueryString("IndentPartHeaderID")
                    Else
                        If txtPONumber.Text <> "" Then
                            idIPH = New EstimationEquipHeaderFacade(User).Retrieve(txtPONumber.Text).ID
                        End If
                    End If
                    Dim objIPH As EstimationEquipHeader = New EstimationEquipHeaderFacade(User).Retrieve(idIPH)

                    nResult = New EstimationEquipHeaderFacade(User).Update(objIPH)
                    MessageBox.Show(SR.SaveSuccess)
                    If objIPH.Status <> EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru Then
                        dtgIPDetail.ShowFooter = False
                        btnValidasi.Enabled = True
                    End If
                    If Not IsNothing(_indentPHID) Then
                        ' update header, case payment type
                        Dim objEstimationEquipHeader As EstimationEquipHeader = New EstimationEquipHeaderFacade(User).Retrieve(_indentPHID)
                        Exit Sub
                    End If
                    MergeIPDetail(CType(Session("sessIPHeader"), EstimationEquipHeader).ID)

                    Try
                        nResult = EditIP()
                        If nResult <> -1 Then
                            DisplayTransactionResult(nResult)
                            MessageBox.Show(SR.SaveSuccess)
                            ViewState.Add("vsSave", "true")
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

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim obj As EstimationEquipHeader
        obj = CType(Session("sessIPHeader"), EstimationEquipHeader)

        If obj.Status > EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
            MessageBox.Show("Status " & obj.StatusDesc & " Tidak Dapat Diubah statusnya menjadi batal")
            Exit Sub
        End If

        obj.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal

        Dim hf As EstimationEquipHeaderFacade = New EstimationEquipHeaderFacade(User)
        Dim result As Integer = hf.Update(obj)
        hf.RecordStatusChangeHistory(obj, EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru)
        _sesshelper.SetSession("sessIPHeader", obj)
        lblStatusValue.Text = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal.ToString

        MessageBox.Show("No Pengajuan " & txtPONumber.Text & " Telah Dibatalkan")

        'bug 1088
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

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../IndentPartEquipment/FrmListEstimationEquip.aspx", True)
    End Sub

    Private Sub btnValidasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidasi.Click
        Dim obj As EstimationEquipHeader
        obj = CType(Session("sessIPHeader"), EstimationEquipHeader)

        If obj.Status > EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
            MessageBox.Show("Status " & obj.StatusDesc & " Tidak Dapat Diubah statusnya menjadi kirim")
            Exit Sub
        End If

        obj.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim

        Dim hf As EstimationEquipHeaderFacade = New EstimationEquipHeaderFacade(User)
        Dim result As Integer = hf.Update(obj)
        hf.RecordStatusChangeHistory(obj, EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru)
        _sesshelper.SetSession("sessIPHeader", obj)

        lblStatusValue.Text = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim.ToString

        MessageBox.Show("No Pengajuan " & txtPONumber.Text & " Telah Dikirim")

        btnValidasi.Enabled = False
        btnSave.Enabled = False
        btnDelete.Enabled = False
        dtgIPDetail.ShowFooter = False
        BindIPDetail()

        For Each item As DataGridItem In dtgIPDetail.Items
            Dim lbtnEdit As LinkButton = item.FindControl("lbtnEdit")
            Dim lbtnPopUpText As LinkButton = item.FindControl("lbtnPopUpText")
            Dim lbtnDelete As LinkButton = item.FindControl("lbtnDelete")
            lbtnDelete.Visible = False
            lbtnPopUpText.Visible = False
            lbtnEdit.Visible = False
        Next
        SendEmailEstimasi(obj)
    End Sub

#End Region

#Region "private function"

    Public Sub SendEmailEstimasi(ByVal objEstimationHeader As EstimationEquipHeader)
        Dim oDealer As Dealer = _sesshelper.GetSession("DEALER")
        Dim euf As EquipUserFacade = New EquipUserFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim mail As DNetMail = New DNetMail(smtp)
        Dim szTos As String '= euf.CreateEmailToString(EquipUser.EquipUserGroup.Pengajuan_Estimasi)
        Dim szCcs As String '= euf.CreateEmailCcString(EquipUser.EquipUserGroup.Pengajuan_Estimasi)
        Dim szItems As String = ""

        Dim objIPHFac As IndentPartHeaderfacade = New IndentPartHeaderfacade(User)
        Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")
        Dim DecTotal As Decimal = 0
        Dim RowNum As Integer = 0
        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Pengajuan_Estimasi, szTos, szCcs, objEstimationHeader.Dealer)
        For Each objItem As EstimationEquipDetail In objEstimationHeader.EstimationEquipDetails
            RowNum += 1
            szItems += "<tr>"
            szItems += String.Format("<td>{0}</td>", RowNum)
            szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartNumber)
            szItems += String.Format("<td>{0}</td>", objItem.SparePartMaster.PartName)
            szItems += String.Format("<td>{0}</td>", IIf(objItem.SparePartMaster.SupplierCode.Trim = "", "-", objItem.SparePartMaster.SupplierCode.Trim))
            szItems += String.Format("<td align='center'>{0}</td>", objItem.EstimationUnit)
            szItems += String.Format("<td align='right'>{0}</td>", FormatNumber(objItem.Harga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))  ' objItem.Harga.ToString("##,###.00", myculture))
            szItems += String.Format("<td align='right'>{0}</td>", FormatNumber((objItem.EstimationUnit * objItem.Harga), 0, TriState.UseDefault, TriState.UseDefault, TriState.True))  '(objItem.EstimationUnit * objItem.Harga).ToString("##,###.00", myculture))
            szItems += "</tr>"
            DecTotal += (objItem.EstimationUnit * objItem.Harga)
            For Each objEEPO As EstimationEquipPO In objItem.EstimationEquipPO
                Dim objIPH As IndentPartHeader = objEEPO.IndentPartDetail.IndentPartHeader
                If objIPH.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_C Then
                    If objIPH.Status = EnumIndentPartStatus.IndentPartStatusDealer.Kirim Then
                        objIPH.Status = EnumIndentPartStatus.IndentPartStatusKTB.Rilis
                    End If
                    objIPHFac.Update(objIPH)
                ElseIf objIPH.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B Then
                    If objIPH.Status = EnumIndentPartStatus.IndentPartStatusDealer.Kirim Then
                        objIPH.Status = EnumIndentPartStatus.IndentPartStatusKTB.Baru
                    End If
                    objIPHFac.Update(objIPH)
                End If
            Next
        Next
        Dim szFormats() As String = {oDealer.DealerCode, oDealer.DealerName, oDealer.City.CityName, objEstimationHeader.CreatedTime.ToString("dd/MM/yyyy"), objEstimationHeader.EstimationNumber, szItems, DecTotal.ToString("##,###.00", myculture)}
        'mail.sendMail(Server.MapPath("../IndentPartEquipment/EmailTemplateEstimationRequest.htm"), szTos, szCcs,  "Pengajuan Estimasi Spare Part Equipment", szFormats)
        Dim strSubjest As String = "[MMKSI-DNet]"
        mail.sendMail(Server.MapPath("../IndentPartEquipment/EmailTemplateEstimationRequest.htm"), szTos, szCcs, strSubjest & " Parts - Pengajuan Estimasi - " & objEstimationHeader.EstimationNumber & " - " & objEstimationHeader.Dealer.DealerCode, szFormats)

    End Sub

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanPartIndentPartCreate_Privilege) Then
            'Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Indent Part Equipment - Pengajuan")
        End If
    End Sub

    Private Function DisplayTransactionResult(ByVal nID As Integer)
        Dim stt As Boolean = False
        Dim objIndentPartH As EstimationEquipHeader = New EstimationEquipHeaderFacade(User).Retrieve(nID)

        ViewState("messCancelIndent") = "Yakin Pembatalan PESANAN akan dibatalkan ?"

        txtPONumber.Text = objIndentPartH.EstimationNumber
        lblDealerCode.Text = objIndentPartH.Dealer.DealerCode
        lblDealerName.Text = objIndentPartH.Dealer.DealerName
        lblDealerTerm.Text = objIndentPartH.Dealer.SearchTerm2
        If objIndentPartH.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru Then
            btnValidasi.Enabled = bCekSendPriv
        Else
            btnValidasi.Enabled = False
        End If

        Dim objUserInfo As UserInfo = CType(_sesshelper.GetSession("LOGINUSERINFO"), UserInfo)
        lblStatusValue.Text = objIndentPartH.StatusDesc

        icOrderDate.Value = objIndentPartH.CreatedTime  'String.Format("{0:dd/MM/yyyy}", objIndentPartH.PODate)
        _arlIPDetail = objIndentPartH.EstimationEquipDetails
        _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        _sesshelper.SetSession("sessIPHeader", objIndentPartH)
        ViewState.Add("vsAccess", "edit")
        BindIPDetail()
    End Function

    Private Sub BindIPDetail()
        _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)
        dtgIPDetail.DataSource = CType(Session("sessIPDetail"), ArrayList)
        calculateTotalAmount(_arlIPDetail)
        dtgIPDetail.DataBind()
    End Sub

    Private Sub DetailOnly(ByVal bval As Boolean)
        btnNew.Enabled = False
        btnDelete.Enabled = False

        If Not _state Then
            ' dari Dealer
            If _indentPHID <> 0 Then
                btnDelete.Enabled = True
            End If
        Else
            ' dari KTB
            btnSave.Enabled = False
            dtgIPDetail.ShowFooter = False
            btnDelete.Enabled = False
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

    Private Function CekPriv(ByVal tipe As Integer) As Boolean
        Return True
    End Function

    Private Sub SetDtgIPDetailItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objIndentPartDetail As IndentPartDetail = e.Item.DataItem

        Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtFPartNumber"), TextBox)
        Dim lblFPopUpSparePartNew As Label = CType(e.Item.Cells(1).FindControl("lblFPopUpSparePart"), Label)
        lblFPopUpSparePartNew.Attributes("onclick") = "ShowPPKodeBarangSelection(0);"

        Dim txtQty As TextBox = CType(e.Item.FindControl("txtFQTY"), TextBox)

        Dim txtAddPriceNew As TextBox = CType(e.Item.FindControl("txtAddPrice"), TextBox)
        txtAddPriceNew.Text = ""

        If _blnDealer Then
            txtPartNumber.ReadOnly = False
            lblFPopUpSparePartNew.Enabled = True
            txtQty.ReadOnly = False
            txtAddPriceNew.ReadOnly = True
        Else
            txtPartNumber.ReadOnly = True
            lblFPopUpSparePartNew.Enabled = False
            txtQty.ReadOnly = True
            txtAddPriceNew.ReadOnly = False
        End If
    End Sub

    Private Sub SetDtgIPDetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objIndentPartDetail As EstimationEquipDetail = e.Item.DataItem

        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Text = (e.Item.ItemIndex + 1).ToString

        Dim lbtnDeleteNew As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
        If Not IsNothing(lbtnDeleteNew) Then
            If Not _blnDealer Then
                ' KTB hanya bs edit price saja
                lbtnDeleteNew.Visible = False
            End If
        End If

        Dim _iDDetail As EstimationEquipDetail = CType(e.Item.DataItem, EstimationEquipDetail)

        Dim lnkTextPopUp As LinkButton = CType(e.Item.FindControl("lbtnPopUpText"), LinkButton)

        If Not IsNothing(lnkTextPopUp) And Not IsNothing(_iDDetail) Then
            lnkTextPopUp.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpIndentDescription.aspx?ID=" & _iDDetail.ID & "', '', 400, 400);return false;")
            lnkTextPopUp.Visible = True
        End If

        If _indentPHID = 0 Then
            lnkTextPopUp.Visible = False
        End If

        Dim lblPriceNew As Label = CType(e.Item.FindControl("lblPrice"), Label)
        If Not IsNothing(lblPriceNew) And Not IsNothing(_iDDetail) Then
            lblPriceNew.Text = objIndentPartDetail.Harga.ToString("#,##0")
        End If

        Dim lblTotalAmount As Label = CType(e.Item.FindControl("lblJumlah"), Label)
        Dim lblPrice As Label = CType(e.Item.FindControl("lblPrice"), Label)
        Dim lblEstimationUnit As Label = CType(e.Item.FindControl("lblEstimationUnit"), Label)

        Dim price As Decimal = CType(lblPrice.Text, Decimal)
        Dim qty As Decimal = CType(lblEstimationUnit.Text, Decimal)
        Dim totalAmount As Decimal = price * qty
        lblTotalAmount.Text = totalAmount.ToString("#,##0")

        objDealer = _sesshelper.GetSession("DEALER")
        Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
        Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
        If _state Then
            lbtnDelete.Visible = False
            lbtnEdit.Visible = False
        End If

        Dim lbtnPopUpText As LinkButton = CType(e.Item.FindControl("lbtnPopUpText"), LinkButton)
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lbtnPopUpText.Visible = False
        End If

        If _isPopup = 1 Then
            lbtnPopUpText.Visible = False
        End If

    End Sub

    Private Sub SetDtgIPDetailItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objIndentPartDetail As EstimationEquipDetail = e.Item.DataItem
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Clear()
        e.Item.Cells(0).Controls.Add(lNum)

        Dim txtEditPriceNew As TextBox = CType(e.Item.FindControl("txtEditPrice"), TextBox)
        Dim txtEditPriceTemp As TextBox = CType(e.Item.FindControl("txtEditPriceTemp"), TextBox)
        txtEditPriceNew.Text = "0" 'objIndentPartDetail.Harga.ToString("#,##0")
        txtEditPriceTemp.Text = "0" 'objIndentPartDetail.Harga.ToString("#,##0")

        Dim lblEditPrice As Label = CType(e.Item.FindControl("lblEditPrice"), Label)
        lblEditPrice.Text = "0" 'objIndentPartDetail.Harga.ToString("#,##0")

        Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtEPartNumber"), TextBox)
        Dim txtQty As TextBox = CType(e.Item.FindControl("txtEQTY"), TextBox)

        Dim lblEPopUpSparePartNew As Label = CType(e.Item.FindControl("lblEPopUpSparePart"), Label)
        lblEPopUpSparePartNew.Attributes("onclick") = "ShowPPKodeBarangSelection(1);"

        Dim lblEPartNumber As Label = CType(e.Item.FindControl("lblEPartNumber"), Label)
        Dim lblEQTY As Label = CType(e.Item.FindControl("lblEQTY"), Label)

        If _blnDealer Then
            txtPartNumber.Visible = True
            txtQty.Visible = True
            lblEPopUpSparePartNew.Visible = True
            lblEPartNumber.Visible = False
            lblEQTY.Visible = False
            lblEditPrice.Visible = True
            txtEditPriceNew.Visible = False
        Else
            ' case KTB
            ' harga ditentukan by KTB
            txtPartNumber.Visible = False
            txtQty.Visible = False
            lblEPopUpSparePartNew.Visible = False
            lblEPartNumber.Visible = True
            lblEQTY.Visible = True
            lblEditPrice.Visible = False
            txtEditPriceNew.Visible = True
        End If

    End Sub

    Private Sub RenderPartItem(ByVal objpart As SparePartMaster, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim lblPartName As Label = CType(e.Item.FindControl("lblFPartName"), Label)
        Dim lblModelCode As Label = CType(e.Item.FindControl("lblFModel"), Label)
        lblPartName.Text = objpart.PartName
        lblModelCode.Text = objpart.ModelCode
    End Sub

    Private Function PartIsExist(ByVal partNumber As String, ByVal arlIPDetail As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each ipDetail As EstimationEquipDetail In arlIPDetail
            If ipDetail.SparePartMaster.PartNumber.Trim().ToUpper() = partNumber.Trim().ToUpper() Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Private Function PartIsExist(ByVal partNumber As String, ByVal arlIPDetail As ArrayList, ByVal nIndeks As Integer) As Boolean
        For i As Integer = 0 To arlIPDetail.Count - 1
            Dim obj As EstimationEquipDetail = CType(arlIPDetail(i), EstimationEquipDetail)
            If Not IsNothing(obj) Then
                If Not IsNothing(obj.SparePartMaster) Then
                    If obj.SparePartMaster.PartNumber.Trim().ToUpper() = partNumber.Trim().ToUpper() AndAlso nIndeks <> i Then
                        Return True
                    End If
                End If
            End If
        Next
        Return False
    End Function

    Private Sub RemoveAllSession()
        _sesshelper.RemoveSession("sessIPHeader")
        _sesshelper.RemoveSession("sessIPDetail")
        lblTotalAmount.Text = "0"
    End Sub

    Private Function cekValiditas() As Boolean
        If (dtgIPDetail.Items.Count <= 0) Then Return False
        Dim arlIPDetail As ArrayList = _sesshelper.GetSession("sessIPDetail")
        If IsNothing(arlIPDetail) Then Return False

        For i As Integer = 0 To arlIPDetail.Count - 1
            Dim obj As EstimationEquipDetail = CType(arlIPDetail(i), EstimationEquipDetail)
            If Not IsNothing(obj) Then
                If IsNothing(obj.SparePartMaster) Then
                    Return False
                Else
                    If (obj.EstimationUnit = 0) Then
                        Return False
                        'Start  :RemainModule-IndentPart:Only Active Part
                    Else
                        If obj.SparePartMaster.ActiveStatus <> EnumSparePartActiveStatus.SparePartActiveStatus.Active Then
                            Return False
                        End If
                        'End    :RemainModule-IndentPart:Only Active Part
                    End If
                End If
            Else
                Return False
            End If
        Next
        Return True
    End Function

    Private Function InsertNewIndentPart() As Integer
        Dim ObjIndent As EstimationEquipHeader = GetIndentHeader()
        Return New EstimationEquipHeaderFacade(User).InsertEstimationEquipHeader(ObjIndent, CType(Session("sessIPDetail"), ArrayList))
    End Function

    Private Function GetIndentHeader() As EstimationEquipHeader
        Dim ObjIndent As New EstimationEquipHeader
        If IsNothing(Session("sessIPHeader")) Then
            ObjIndent = New EstimationEquipHeader
            ObjIndent.Dealer = CType(Session("sessDealer"), Dealer)
            ObjIndent.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru
            _sesshelper.SetSession("sessIPHeader", ObjIndent)
        Else
            ObjIndent = CType(Session("sessIPHeader"), EstimationEquipHeader)
        End If
        Return ObjIndent
    End Function

    Private Sub MergeIPDetail(ByVal nID As Integer)
        Dim objIPH As EstimationEquipHeader = New EstimationEquipHeaderFacade(User).Retrieve(nID)
        _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)
        For Each objIPDetailOrig As EstimationEquipDetail In objIPH.EstimationEquipDetails
            objIPDetailOrig.RowStatus = DBRowStatus.Deleted
        Next

        Dim found As Boolean
        For Each objIPDetail As EstimationEquipDetail In _arlIPDetail
            found = False
            For Each objIPDetailOrig As EstimationEquipDetail In objIPH.EstimationEquipDetails
                If objIPDetail.SparePartMaster.PartNumber.Trim().ToUpper() = objIPDetailOrig.SparePartMaster.PartNumber.Trim().ToUpper() Then
                    objIPDetailOrig.RowStatus = DBRowStatus.Active
                    objIPDetailOrig.EstimationUnit = objIPDetail.EstimationUnit
                    found = True
                    Exit For
                End If
                found = False
            Next
            If Not found Then
                objIPH.EstimationEquipDetails.Add(objIPDetail)
            End If
        Next
        _sesshelper.SetSession("sessIPDetail", objIPH.EstimationEquipDetails)
    End Sub

    Private Function EditIP() As Integer
        Dim ObjIPH As EstimationEquipHeader = CType(Session("sessIPHeader"), EstimationEquipHeader)
        Return New EstimationEquipHeaderFacade(User).UpdateEstimationEquipHeader(ObjIPH, CType(Session("sessIPDetail"), ArrayList))
    End Function

    Private Sub calculateTotalAmount(ByVal arldetail As ArrayList)
        Dim decTotal As Decimal = 0
        For Each objTmpDetail As EstimationEquipDetail In arldetail
            decTotal += (objTmpDetail.EstimationUnit * objTmpDetail.Harga)
        Next
        lblTotalAmount.Text = "Rp. " + decTotal.ToString("#,##0")
    End Sub

    Private Function IsPartNumberActive(ByVal PartNumber As String) As Boolean
        Dim objSPMFac As SparePartMasterFacade = New SparePartMasterFacade(User)
        Dim crtSPM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlSPM As New ArrayList

        crtSPM.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, PartNumber))
        crtSPM.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
        arlSPM = objSPMFac.Retrieve(crtSPM)
        Return IIf(arlSPM.Count > 0, True, False)
    End Function

    Private Sub CheckUserPrivilege()
        If SecurityProvider.Authorize(context.User, SR.Buat_permintaan_estimasi_indent_part_equipment_privilege) _
        Or SecurityProvider.Authorize(context.User, SR.Buat_upload_estimasi_indent_part_equipment_privilege) Then
        Else
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Permintaan Estimasi")
        End If

        If SecurityProvider.Authorize(context.User, SR.Buat_upload_estimasi_indent_part_equipment_privilege) _
        Or SecurityProvider.Authorize(context.User, SR.Buat_permintaan_estimasi_indent_part_equipment_privilege) Then
            btnNew.Visible = True
            btnSave.Visible = True
            btnCancel.Visible = True
            btnCetak.Visible = True
        Else
            btnNew.Visible = False
            btnSave.Visible = False
            btnCancel.Visible = False
            btnCetak.Visible = False
        End If
        If SecurityProvider.Authorize(context.User, SR.Kirim_permintaan_estimasi_indent_part_equipment_privilege) Then
            btnValidasi.Visible = True
        Else
            btnValidasi.Visible = False
        End If
    End Sub

#End Region


#Region "Upload Excel"

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If (DataFile.PostedFile Is Nothing) Or (DataFile.PostedFile.ContentLength <= 0) Then
            MessageBox.Show(SR.FileNotSelected)
            Exit Sub
        End If

        Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
        If DataFile.PostedFile.ContentLength > maxFileSize Then
            MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
            Exit Sub
        End If

        Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)   '-- Source file name
        Dim DestFile As String = Server.MapPath("") & "\..\DataFile\" & SrcFile  '-- Destination file
        Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temporary file

        'Todo session
        Session.Add("DestFile", DestFile)  '-- Store Destination file path into session
        'Todo session
        Session.Add("TempFile", TempFile)  '-- Store Temporary file path into session

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        If imp.Start() Then
            Dim objUpload As New UploadToWebServer
            objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)
        End If
        Dim parser As IExcelParser = New UploadIndentPartEquipmentExcelParser
        Dim arlTmpDetail = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1$]", "User"), ArrayList)

        '-- Save IP details to session
        _sesshelper.SetSession("sessIPDetail", arlTmpDetail)

        dtgIPDetail.DataSource = Nothing  '-- Reset datagrid first
        BindIPDetail()  '-- Bind details to datagrid

        btnUpload.Enabled = False  '-- Disable button <Upload> if successfully upload data

        calculateTotalAmount(arlTmpDetail)

        Dim isHasErrors As Boolean = False
        For Each objTmpDetail As EstimationEquipDetail In arlTmpDetail
            If objTmpDetail.ErrorMessage <> "" Then
                isHasErrors = True
                Exit For
            End If
        Next
        If Not isHasErrors Then
            btnSave.Enabled = True
        End If

        btnNew.Enabled = True
        imp.StopImpersonate()
        imp = Nothing
    End Sub

    Private Function isExcelUploadMode() As Boolean
        If IsNothing(Page.Request.QueryString("up")) Then Return False
        Return Convert.ToBoolean(Page.Request.QueryString("up"))
    End Function

    Private Sub checkExcelUpload()
        If Not isExcelUploadMode() Then Exit Sub

        lblUploadlbl.Visible = True
        lblUploadDotlbl.Visible = True
        pnlUpload.Visible = True
        lblTitle.Text = "INDENT PART - Upload Estimasi Indent Part Equipment"
        dtgIPDetail.Columns(8).Visible = True
    End Sub

#End Region

End Class
