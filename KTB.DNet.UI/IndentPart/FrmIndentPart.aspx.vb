#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.IndentPart
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.Linq
Imports System.Data.DataSetExtensions
#End Region

Public Class FrmIndentPart
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents lblNomorTanggalPO As System.Web.UI.WebControls.Label
    Protected WithEvents lblMaterialType As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMaterialType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents icOrderDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents dtgIPDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnValidasi As System.Web.UI.WebControls.Button
    Protected WithEvents hdnValNew As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnValDel As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPaymentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCetak As HtmlControls.HtmlInputButton
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents TxtChassisNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents LblTypeWarna As System.Web.UI.WebControls.Label
    Protected WithEvents LabelTOP As System.Web.UI.WebControls.Label
    Protected WithEvents DdlDesc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchChassis As System.Web.UI.WebControls.Label
    Protected WithEvents lntnCheckChassis As System.Web.UI.WebControls.LinkButton
    Protected WithEvents hdnTypeWarna As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnClose As System.Web.UI.HtmlControls.HtmlInputButton
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


#Region "Custom Variable Declaration"
    Private _sesshelper As New SessionHelper
    Private _arlIPDetail As ArrayList = New ArrayList
    Private _sparePartPOTypeTOP As SparePartPOTypeTOP
    Dim _indentPHID As Integer
    Dim _state As Boolean
    Dim _blnDealer As Boolean
    Private _arlDetailProcess As ArrayList
    Dim objDealer As Dealer
    Private _isPopup As Integer = 0

    Private ubahPrivilege As Boolean = SecurityProvider.Authorize(Context.User, SR.UbahDetailIndentPart_Privilege)
    Private hapusPrivilege As Boolean = SecurityProvider.Authorize(Context.User, SR.HapusDetailIndentPart_Privilege)
    Private tambahPrivilege As Boolean = SecurityProvider.Authorize(Context.User, SR.TambahDetailIndentPart_Privilege)
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve("I")
        _isPopup = Val(Request.QueryString("IsPopup"))
        _state = Convert.ToBoolean(Request.QueryString("View"))

        LblTypeWarna.Text = hdnTypeWarna.Value
        SetChassisBehaviour()
        objDealer = _sesshelper.GetSession("DEALER")

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            InitiateAuthorization()
        End If

        'state= true -> dr KTB, hanya bs update pricenya saja
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            _state = False
            dtgIPDetail.ShowFooter = False
            'btnCetak.Visible = False
            DdlDesc.Enabled = False
            TxtChassisNo.Enabled = False
        Else
            dtgIPDetail.ShowFooter = tambahPrivilege
            btnCetak.Visible = True
        End If

        _indentPHID = Request.QueryString("IndentPartHeaderID")

        If _isPopup = 1 Then
            _state = True
        End If

        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            _blnDealer = True
        Else
            _blnDealer = False
        End If

        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "SparePartMaster.PartNumber"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            LabelTOP.Visible = False
            If _indentPHID > 0 Then
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    btnCetak.Disabled = False
                End If
                If _state Then
                    ' menonaktifkan aksi kolom
                    'dtgIPDetail.Columns(6).Visible = False
                End If
                Dim obIPH As IndentPartHeader = New IndentPartHeaderFacade(User).Retrieve(_indentPHID)
                ViewState.Add("vsAccess", "edit")
                BindDropDown()
                Dim topType As String = ValidateAndReturnTypeTOP(_sparePartPOTypeTOP.ID, obIPH.IndentPartDetails)
                If topType = "COD" Then
                    ddlTermOfPayment.Items.Clear()
                    ddlTermOfPayment.Enabled = False
                    ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
                    ddlTermOfPayment.SelectedIndex = 0
                    ViewState("COD") = True
                ElseIf topType = "TOP" Then
                    BindDdlPaymentType()
                End If
                DisplayTransactionResult(_indentPHID)
                DdlDesc.SelectedValue = obIPH.DescID
                If Not IsNothing(obIPH.TermOfPayment) Then
                    LabelTOP.Text = obIPH.TermOfPayment.Description
                End If
                TxtChassisNo.Text = obIPH.ChassisNumber
                If TxtChassisNo.Text.Trim <> "" Then
                    Dim objChassis As ChassisMaster = New ChassisMasterFacade(User).Retrieve(TxtChassisNo.Text.Trim)
                    If objChassis.ID <> 0 Then
                        LblTypeWarna.Text = objChassis.VechileType & " - " & objChassis.VechileColor.ColorCode & " " & objChassis.VechileColor.ColorIndName
                        hdnTypeWarna.Value = objChassis.VechileType & " - " & objChassis.VechileColor.ColorCode & " " & objChassis.VechileColor.ColorIndName
                    End If
                End If
                ddlMaterialType.SelectedValue = obIPH.MaterialType
                If Not IsNothing(obIPH.TermOfPayment) Then
                    ddlTermOfPayment.SelectedValue = obIPH.TermOfPayment.ID.ToString()
                End If

                DetailOnly(True)
                ddlMaterialType.Enabled = False

                'If _state Then
                '    btnDelete.Enabled = False
                '    ddlMaterialType.Enabled = False
                'Else
                '    btnDelete.Enabled = True
                'End If

                'mod by ery
                'refer bug 1086, 1083
                If obIPH.Status = EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi _
                    AndAlso objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    If _state Then
                        DdlDesc.Enabled = False
                        TxtChassisNo.Enabled = False
                        'btnSave.Enabled = True
                        btnSave.Enabled = False
                        ddlPaymentType.Enabled = False
                        ddlPaymentType.Items(0).Text = "Silakan Pilih"
                    Else
                        dtgIPDetail.ShowFooter = False
                        dtgIPDetail.Columns(6).Visible = False
                        ddlPaymentType.Enabled = True
                        ddlPaymentType.Items(0).Text = "Silakan Pilih"
                    End If
                End If
                'btnSave.Enabled = False
                'end bug 1086,1083

                'bug 719
                If CByte(ddlMaterialType.SelectedValue) <> EnumMaterialType.MaterialType.Tools Then
                    ddlPaymentType.Enabled = False
                    ddlPaymentType.Items(0).Text = ""
                End If

                If obIPH.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal Then
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnValidasi.Enabled = False
                End If

                If obIPH.Status <> EnumIndentPartStatus.IndentPartStatusDealer.Baru Then
                    DdlDesc.Enabled = False
                    TxtChassisNo.Enabled = False
                    ddlPaymentType.Enabled = False
                    ddlPaymentType.Visible = False
                    ddlTermOfPayment.Visible = False
                    LabelTOP.Visible = True
                End If

                If obIPH.Status > EnumIndentPartStatus.IndentPartStatusDealer.Kirim Then
                    btnDelete.Enabled = False
                    btnValidasi.Enabled = False
                End If


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
                    BindDdlPaymentType()
                    btnDelete.Enabled = False
                    btnValidasi.Enabled = False
                    btnNew.Enabled = False
                    btnCancel.Visible = False
                    ddlPaymentType.Enabled = False
                    ddlPaymentType.Items(0).Text = ""
                    btnSave.Enabled = False
                End If
            End If
            btnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dibatalkan?');")
            'btnNew.Attributes.Add("OnClick", "return confirm('Simpan Data Terlebih Dahulu ?');")
            'btnSave.Enabled = bCekStatusEditDetailDealerPriv

        Else
            If _indentPHID = 0 Then
                If CType(ViewState("vsSave"), String) = "false" Then
                    'Btn New bukan untuk menyimpan data bug 1564
                    If Request.Form("hdnValNew") = "1" Then
                        btnNew_Click(Nothing, Nothing)
                        'btnSave_Click(Nothing, Nothing)
                        'hdnValNew.Value = "0"
                        'Else
                        'ViewState.Add("vsSave", "true")
                    End If
                    'btnNew_Click(Nothing, Nothing)
                End If
            End If
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

    Private Function ValidatePart(ByVal e As DataGridCommandEventArgs) As Boolean
        Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtFPartNumber"), TextBox)
        If ddlMaterialType.SelectedIndex <> 0 Then
            Dim tempCode As String = ddlMaterialType.SelectedValue
            Select Case ddlMaterialType.SelectedValue
                Case 1 'Parts
                    tempCode = "I"
                Case 2 'Tools
                    tempCode = "E"
                Case 3 'Accessories
                    tempCode = "A"
                Case 4 'Oil
                    tempCode = "O"
            End Select
            Dim objSPMaster As SparePartMaster = New SparePartMasterFacade(User).Retrieve(txtPartNumber.Text)
            If objSPMaster.TypeCode = tempCode Then
                Return True
            Else
                MessageBox.Show("No.Barang " & objSPMaster.PartNumber & " tidak cocok dengan\n " & _
                         "Tipe Barang yang dipilih")
                Return False
            End If
        Else
            MessageBox.Show("Tipe barang belum dipilih")
            Return False
        End If
    End Function

    Private Sub dtgIPDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgIPDetail.ItemCommand
        _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)

        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                If ValidatePart(e) Then
                    Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtFPartNumber"), TextBox)
                    Dim txtQty As TextBox = CType(e.Item.FindControl("txtFQTY"), TextBox)
                    Dim txtAddPriceNew As TextBox = CType(e.Item.FindControl("txtAddPrice"), TextBox)

                    Dim objPart As SparePartMaster
                    If IsNothing(txtPartNumber) OrElse txtPartNumber.Text = String.Empty Then
                        MessageBox.Show("No.Part masih kosong")
                        Return
                    Else
                        objPart = New SparePartMasterFacade(User).Retrieve(txtPartNumber.Text.Trim().ToUpper())
                    End If
                    If objPart.RetalPrice <> Val(txtAddPriceNew.Text.Replace(".", "")) Then
                        MessageBox.Show("Harga tidak sama dengan data master Sparepart. Silakan pilih Sparepart melalui tombol popup.")
                        Return
                    End If
                    If IsNothing(txtQty) OrElse txtQty.Text = String.Empty OrElse CType(txtQty.Text.Trim, Integer) <= 0 Then
                        MessageBox.Show("Unit Pesanan tidak boleh kosong/0")
                        If Not (IsNothing(objPart) OrElse objPart.ID = 0) Then
                            RenderPartItem(objPart, e)
                        End If
                        Return
                    End If

                    ' btnSubmit.Enabled = False
                    If Not (IsNothing(objPart) OrElse objPart.ID = 0) Then
                        If CekPriv(ddlMaterialType.SelectedValue) = True Then
                            If Not PartIsExist(objPart.PartNumber, _arlIPDetail) Then
                                Dim objIPDetail As IndentPartDetail = New IndentPartDetail
                                objIPDetail.Qty = CType(IIf(txtQty.Text = "", "0", txtQty.Text), Integer)
                                objIPDetail.SparePartMaster = objPart
                                objIPDetail.Price = CType(IIf(txtAddPriceNew.Text = "", "0", txtAddPriceNew.Text), Decimal)
                                _arlIPDetail.Add(objIPDetail)
                                If CType(ViewState("vsAccess"), String) = "edit" Then
                                    objIPDetail.IndentPartHeader = CType(Session("sessIPHeader"), IndentPartHeader)
                                    Dim nResult As Integer = New IndentPartDetailFacade(User).Insert(objIPDetail)
                                End If
                                If Not IsNothing(Request.QueryString("IndentPartHeaderID")) Then
                                    btnNew.Enabled = False
                                Else
                                    btnNew.Enabled = True
                                End If
                                Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")

                                icOrderDate.Enabled = False
                                ddlMaterialType.Enabled = False
                            Else
                                MessageBox.Show(SR.DataIsExist("Spare Part"))
                            End If
                        Else
                            MessageBox.Show("Anda tidak memiliki hak akses untuk menambah " & ddlMaterialType.SelectedItem.Text)
                        End If
                    Else
                        MessageBox.Show(SR.DataNotFound("Spare Part"))
                    End If
                End If

            Case "edit" 'Edit mode activated
                dtgIPDetail.ShowFooter = False
                btnSave.Enabled = False
                dtgIPDetail.EditItemIndex = e.Item.ItemIndex


            Case "delete" 'Delete this datagrid item 
                Try

                    Dim objIPDetail As IndentPartDetail = _arlIPDetail(e.Item.ItemIndex)
                    If objIPDetail.ID > 0 Then
                        Dim result As Integer = New IndentPartDetailFacade(User).DeleteFromDB(objIPDetail)
                        If result = -1 Then
                            MessageBox.Show(SR.DeleteFail)
                            Exit Sub
                        End If
                    End If
                    _arlIPDetail.RemoveAt(e.Item.ItemIndex)
                    '_sesshelper.SetSession("sessIPDetail", _arlIPDetail)
                    If (_arlIPDetail.Count = 0) Then
                        'icOrderDate.Enabled = True
                        ddlMaterialType.Enabled = True
                    End If

                    BindDdlPaymentType()
                    If ViewState("COD") = False Then
                        ddlTermOfPayment.Enabled = True
                    End If
                    'dtgIPDetail.DataSource = _arlIPDetail
                    'dtgIPDetail.DataBind()

                    'Exit Sub

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
                    objPart = New SparePartMasterFacade(User).Retrieve(txtPartNumber.Text)
                End If
                If _blnDealer Then
                    If objPart.RetalPrice <> Val(txtEditPriceTempx.Text.Replace(".", "")) Then
                        MessageBox.Show("Harga tidak sama dengan data master Sparepart. Silakan pilih Sparepart melalui tombol popup.")
                        Return
                    End If
                End If
                If IsNothing(txtQty) OrElse txtQty.Text = String.Empty OrElse CType(txtQty.Text.Trim, Integer) <= 0 Then
                    MessageBox.Show("Unit Pesanan tidak boleh kosong/0")
                    Return
                    If Not (IsNothing(objPart) OrElse objPart.ID = 0) Then
                        RenderPartItem(objPart, e)
                    End If
                    Return
                End If

                If Not (IsNothing(objPart) Or objPart.ID = 0) Then
                    If Not PartIsExist(txtPartNumber.Text.Trim().ToUpper(), _arlIPDetail, e.Item.ItemIndex) Then
                        Dim objIPDetail As IndentPartDetail = CType(_arlIPDetail(e.Item.ItemIndex), IndentPartDetail)
                        objIPDetail.Qty = CType(txtQty.Text, Integer)
                        objIPDetail.SparePartMaster = objPart

                        objDealer = _sesshelper.GetSession("DEALER")

                        'state= true -> dr KTB, hanya bs update pricenya saja
                        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                            objIPDetail.Price = CType(IIf(txtEditPriceNew.Text = "", "0", txtEditPriceNew.Text), Decimal)
                        Else
                            objIPDetail.Price = CType(IIf(txtEditPriceTempx.Text = "", "0", txtEditPriceTempx.Text), Decimal)
                        End If

                        If CType(ViewState("vsAccess"), String) = "edit" Then
                            objIPDetail.IndentPartHeader = CType(Session("sessIPHeader"), IndentPartHeader)
                            nResult = New IndentPartDetailFacade(User).Update(objIPDetail)
                        End If
                        dtgIPDetail.EditItemIndex = -1
                        If _blnDealer Then
                            dtgIPDetail.ShowFooter = tambahPrivilege
                            btnSave.Enabled = True
                        Else
                            ' KTB hanya bs update pricenya saja
                            dtgIPDetail.ShowFooter = False
                            btnSave.Enabled = False
                        End If
                    Else
                        MessageBox.Show(SR.DataIsExist("Spare Part"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Spare Part"))
                End If


            Case "cancel" 'Cancel Update this datagrid item 
                dtgIPDetail.EditItemIndex = -1
                If _blnDealer Then
                    dtgIPDetail.ShowFooter = tambahPrivilege
                    btnSave.Enabled = True
                Else
                    dtgIPDetail.ShowFooter = False
                End If

        End Select
        _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        BindIPDetail()
        Dim bValidBtn As Boolean = False
        _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve("I")
        If _sparePartPOTypeTOP.IsTOP Then
            bValidBtn = ValidateTOP(_sparePartPOTypeTOP.ID, _arlIPDetail)
            btnSave.Enabled = bValidBtn
            btnValidasi.Enabled = btnValidasi.Enabled And bValidBtn
        End If
    End Sub

    Private Function ValidateAndReturnTypeTOP(ByVal typeTopId As Integer, ByVal spdetails As ArrayList) As String

        Dim result As String = ""
        Dim counterTOP As Integer = 0
        Dim counterCOD As Integer = 0

        For Each PODetail As IndentPartDetail In spdetails
            Dim sparePartMasterTop As SparePartMasterTOP = New SparePartMasterTOPFacade(User).RetrieveBySPIDandTypeTOPID(PODetail.SparePartMaster.ID, typeTopId)

            If Not IsNothing(sparePartMasterTop.SparePartPOTypeTOP) Then
                If sparePartMasterTop.Status And sparePartMasterTop.SparePartPOTypeTOP.IsTOP Then
                    counterTOP = counterTOP + 1
                ElseIf (Not sparePartMasterTop.Status Or Not sparePartMasterTop.SparePartPOTypeTOP.IsTOP) Then
                    counterCOD = counterCOD + 1
                End If
            Else
                counterCOD = counterCOD + 1
            End If
        Next
        If counterTOP > 0 And counterCOD > 0 Then
            MessageBox.Show("Sehubungan dengan TOP, material-material ini tidak bisa dibuat dalam PO ini")
            result = "False"
        ElseIf counterTOP > 0 And counterCOD = 0 Then
            result = "TOP"
        ElseIf counterTOP = 0 And counterCOD > 0 Then
            ddlTermOfPayment.ClearSelection()
            _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve("I")
            ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
            ddlTermOfPayment.Enabled = False
            result = "COD"
        End If
        Return result
    End Function

    Private Function ValidateTOP(ByVal typeTopId As Integer, ByVal spdetails As ArrayList) As Boolean
        'If Not ddlTermOfPayment.Enabled Then
        '    Return True
        'End If
        'Dim isCOD As Boolean = True
        Dim result As Boolean = True
        Dim counterTOP As Integer = 0
        Dim counterCOD As Integer = 0
        'Validate TOP
        '_arlPODetail = CType(Session("sessPODetail"), ArrayList)

        If CType(ViewState("AllowTOP"), Boolean) = False Then
            Return True
        End If

        For Each PODetail As IndentPartDetail In spdetails
            Dim sparePartMasterTop As SparePartMasterTOP = New SparePartMasterTOPFacade(User).RetrieveBySPIDandTypeTOPID(PODetail.SparePartMaster.ID, typeTopId)

            If Not IsNothing(sparePartMasterTop.SparePartPOTypeTOP) Then
                If sparePartMasterTop.Status And sparePartMasterTop.SparePartPOTypeTOP.IsTOP Then
                    counterTOP = counterTOP + 1
                ElseIf (Not sparePartMasterTop.Status Or Not sparePartMasterTop.SparePartPOTypeTOP.IsTOP) Then
                    counterCOD = counterCOD + 1
                End If
            Else
                counterCOD = counterCOD + 1
            End If
        Next
        If counterTOP > 0 And counterCOD > 0 Then
            MessageBox.Show("Sehubungan dengan TOP, material-material ini tidak bisa dibuat dalam PO ini")
            result = False
        ElseIf counterTOP = 0 And counterCOD > 0 Then
            ddlTermOfPayment.ClearSelection()
            _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve("I")
            ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
            ddlTermOfPayment.Enabled = False
        End If
        Return result
    End Function

    Private Sub ddlMaterialType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMaterialType.SelectedIndexChanged
        'BindIPDetail()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        'Try
        '    btnSave_Click(Nothing, Nothing)
        '    icOrderDate.Enabled = True
        '    ViewState.Add("vsAccess", "insert")
        '    ViewState.Add("vsSave", "new")
        '    RemoveAllSession()
        '    txtPONumber.Text = "[Generated]"
        '    icOrderDate.Value = Date.Now
        '    ddlMaterialType.SelectedIndex = 0
        '    _arlIPDetail.Clear()
        '    _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        '    InitialPageSession()
        '    BindIPDetail()
        '    btnDelete.Enabled = False
        '    btnNew.Enabled = False
        '    btnValidasi.Enabled = False
        'Catch ex As Exception
        '    MessageBox.Show("Gagal Simpan Data !")
        'End Try
        'If lblStatusValue.Text = "Validasi" Then
        '    ViewState.Add("vsAccess", "insert")
        '    ViewState.Add("vsSave", "new")

        '    RemoveAllSession()
        '    txtPONumber.Text = "[Generated]"
        '    icOrderDate.Value = Date.Now
        '    ddlMaterialType.SelectedIndex = 0
        '    _arlIPDetail.Clear()
        '    _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        '    'InitialPageSession()
        '    BindIPDetail()
        '    btnDelete.Enabled = False
        '    btnNew.Enabled = True
        '    btnSave.Enabled = True
        '    btnValidasi.Enabled = False
        'Else
        btnCetak.Disabled = True
        _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)
        If CType(ViewState("vsSave"), String) = "new" AndAlso _arlIPDetail.Count > 0 Then
            ViewState.Add("vsSave", "false")
            MessageBox.Confirm("Data belum disimpan. Apakah anda akan membuat pengajuan baru?", "hdnValNew")

        Else
            ' ViewMode(True)
            'btnCancel.Enabled = False
            'btnSubmit.Enabled = False
            'ddlOrderType.Enabled = True
            'chkRequestForCanceled.Visible = False
            'If trySaveData() Then
            ViewState.Add("vsAccess", "insert")
            ViewState.Add("vsSave", "new")

            RemoveAllSession()
            txtPONumber.Text = "[Dibuat oleh sistem]"
            lblStatusValue.Text = "Baru"
            TxtChassisNo.Text = ""
            LblTypeWarna.Text = ""
            DdlDesc.SelectedIndex = 0
            TxtChassisNo.BackColor = System.Drawing.Color.White

            icOrderDate.Value = Date.Now
            ddlMaterialType.SelectedIndex = 0
            'icOrderDate.Enabled = True
            ddlMaterialType.Enabled = True
            'ddlPaymentType.Enabled = True
            _arlIPDetail.Clear()
            _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
            'InitialPageSession()
            BindIPDetail()
            BindDdlPaymentType()
            ddlTermOfPayment.Enabled = True
            btnDelete.Enabled = False
            btnNew.Enabled = False
            btnSave.Enabled = True
            btnValidasi.Enabled = False
            btnCancel.Enabled = False
            'End If

            '    End If
            If ViewState("COD") Then
                ddlTermOfPayment.Enabled = False
            End If
        End If
        dtgIPDetail.Columns(dtgIPDetail.Columns.Count - 1).Visible = True
        dtgIPDetail.ShowFooter = tambahPrivilege

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If CType(Session("sessIPDetail"), ArrayList).Count > 0 Then
            Dim nResult As Integer
            If cekValiditas() Then
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

                            Else
                                MessageBox.Show(SR.SaveFail)
                            End If
                        Catch ex As Exception
                            MessageBox.Show("Gagal simpan Indent Part PO " & ex.Message)
                            Return
                        End Try


                    Case "edit"
                        'refer bug 1086

                        Dim idIPH As Integer = 0
                        If Val(Request.QueryString("IndentPartHeaderID")) > 0 Then
                            idIPH = Request.QueryString("IndentPartHeaderID")
                        Else
                            If txtPONumber.Text <> "" Then
                                idIPH = New IndentPartHeaderFacade(User).Retrieve(txtPONumber.Text).ID
                            End If
                        End If
                        'Dim idIPH As Integer = Request.QueryString("IndentPartHeaderID")
                        Dim objIPH As IndentPartHeader = New IndentPartHeaderFacade(User).Retrieve(idIPH)

                        If Not IsNothing(objIPH) Then
                            objIPH.DescID = DdlDesc.SelectedValue
                            objIPH.ChassisNumber = TxtChassisNo.Text.Trim
                            objIPH.MaterialType = CInt(ddlMaterialType.SelectedValue)
                            If objIPH.Status = EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi Then
                                objIPH.PaymentType = ddlPaymentType.SelectedValue
                            End If
                            If ddlTermOfPayment.SelectedValue <> "" Then
                                objIPH.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(ddlTermOfPayment.SelectedValue, Integer))
                            Else
                                objIPH.TermOfPayment = Nothing
                            End If
                            nResult = New IndentPartHeaderFacade(User).Update(objIPH)
                            MessageBox.Show(SR.SaveSuccess)
                            If objIPH.Status <> EnumIndentPartStatus.IndentPartStatusDealer.Baru Then
                                dtgIPDetail.ShowFooter = False
                            End If
                        Else
                            If Not IsNothing(_indentPHID) Then
                                ' update header, case payment type
                                Dim objIndentPartHeader As IndentPartHeader = New IndentPartHeaderFacade(User).Retrieve(_indentPHID)
                                objIndentPartHeader.ChassisNumber = TxtChassisNo.Text.Trim
                                objIndentPartHeader.DescID = DdlDesc.SelectedValue

                                If Not IsNothing(objIndentPartHeader) Then
                                    objIndentPartHeader.PaymentType = ddlPaymentType.SelectedValue
                                    nResult = New IndentPartHeaderFacade(User).Update(objIndentPartHeader)
                                End If
                                MessageBox.Show(SR.SaveSuccess)
                                Exit Sub
                            End If
                            MergeIPDetail(CType(Session("sessIPHeader"), IndentPartHeader).ID)

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


                        End If

                End Select
            Else
                If (hdnValNew.Value = "1") Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    'MessageBox.Show("Tipe Barang Tidak Valid")
                End If
            End If

        Else
            MessageBox.Show(SR.GridIsEmpty("PO Detail"))
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("../IndentPart/FrmListIndentPart.aspx", True)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim obj As IndentPartHeader
        obj = CType(Session("sessIPHeader"), IndentPartHeader)


        If obj.Status > EnumIndentPartStatus.IndentPartStatusDealer.Kirim Then
            MessageBox.Show("Status " & obj.StatusDealerDesc & " Tidak Dapat Diubah statusnya menjadi batal")
            Exit Sub
        End If

        obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Batal
        obj.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.BelumValidasi

        Dim result As Integer = New IndentPartHeaderFacade(User).Update(obj)
        _sesshelper.SetSession("sessIPHeader", obj)
        lblStatusValue.Text = EnumIndentPartStatus.IndentPartStatusDealer.Batal.ToString

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
            lbtnDelete.Visible = False
            lbtnPopUpText.Visible = False
            lbtnEdit.Visible = False
        Next


        'Try
        '    Dim objFacade As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)

        '    '--------------DELETE --> set RowStatus = -1
        '    'If objFacade.Delete(CType(_sesshelper.GetSession("sessIPHeader"), IndentPartHeader)) > 0 Then
        '    '    MessageBox.Show(SR.DeleteSucces)
        '    '    If _indentPHID > 0 Then
        '    '        btnCancel_Click(Nothing, Nothing)
        '    '    End If
        '    'End If
        '    '-----------------------------------

        '    '-----Delete Permanent from DB
        '    Dim objIPH As IndentPartHeader = CType(_sesshelper.GetSession("sessIPHeader"), IndentPartHeader)
        '    If objFacade.DeleteIndentPartHeader(objIPH, objIPH.IndentPartDetails) > 0 Then
        '        MessageBox.Show(SR.DeleteSucces)
        '        If Not IsNothing(ViewState("vsSave")) Then
        '            ViewState.Add("vsAccess", "insert")
        '            ViewState.Add("vsSave", "new")
        '            RemoveAllSession()
        '            txtPONumber.Text = "[Generated]"
        '            icOrderDate.Value = Date.Now
        '            ddlMaterialType.SelectedIndex = 0
        '            _arlIPDetail.Clear()
        '            _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        '            InitialPageSession()
        '            BindIPDetail()
        '            btnDelete.Enabled = False
        '            btnNew.Enabled = False
        '            btnValidasi.Enabled = False
        '            ' btnCancel_Click(Nothing, Nothing)
        '        Else
        '            btnCancel_Click(Nothing, Nothing)
        '        End If

        '    Else
        '        MessageBox.Show(SR.DeleteFail)
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show("Gagal Hapus Pesanan Indent  " & ex.Message)
        '    Return
        'End Try

    End Sub

    Private Sub btnValidasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidasi.Click

        Dim obj As IndentPartHeader
        obj = CType(Session("sessIPHeader"), IndentPartHeader)

        If obj.Status > EnumIndentPartStatus.IndentPartStatusDealer.Kirim Then
            MessageBox.Show("Status " & obj.StatusDealerDesc & " Tidak Dapat Diubah statusnya menjadi kirim")
            Exit Sub
        End If

        Dim objIndentPartH As IndentPartHeader = New IndentPartHeaderFacade(User).Retrieve(obj.ID)

        If ddlTermOfPayment.SelectedValue = "" Then
            MessageBox.Show("Cara Pembayaran harus diisi")
            Exit Sub
        End If

        If Not IsNothing(objIndentPartH.TermOfPayment) Then
            If CType(ddlTermOfPayment.SelectedValue, Integer) <> objIndentPartH.TermOfPayment.ID Then
                MessageBox.Show("Terjadi perubahan cara pembayaran. Silahkan klik simpan terlebih dahulu.")
                Exit Sub
            End If
        Else
            MessageBox.Show("Terjadi perubahan cara pembayaran. Silahkan klik simpan terlebih dahulu.")
            Exit Sub
        End If

        If obj.MaterialType = 3 Then
            'update ststus for autogenerate PO jika tipe : accesories
            obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Selesai
            obj.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Selesai
        Else
            obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Kirim
            obj.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Baru
        End If
        obj.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(ddlTermOfPayment.SelectedValue, Integer))
        Dim result As Integer = New IndentPartHeaderFacade(User).Update(obj)
        _sesshelper.SetSession("sessIPHeader", obj)

        If obj.MaterialType = 3 Then
            GeneratePO(txtPONumber.Text)
        End If

        lblStatusValue.Text = EnumIndentPartStatus.IndentPartStatusDealer.Kirim.ToString
        ddlTermOfPayment.Enabled = False
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

        'Dim fcd As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        'Dim obj As IndentPartHeader
        'Dim nResult As Integer = 0

        'Try
        '    'btnSave_Click(Nothing, Nothing)
        '    If Not IsNothing(Session("sessIPHeader")) Then

        '        obj = CType(Session("sessIPHeader"), IndentPartHeader)

        '        'Check Rule to Change The status 
        '        If obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Validasi Then
        '            MessageBox.Show("Status Sudah Validasi")
        '        Else
        '            If fcd.AllowChangeStatus(obj, obj.Status, EnumIndentPartStatus.IndentPartStatusDealer.Validasi) Then
        '                obj.Status = EnumIndentPartStatus.IndentPartStatusDealer.Validasi
        '                obj.MaterialType = ddlMaterialType.SelectedValue
        '                _sesshelper.SetSession("sessIPHeader", obj)

        '                nResult = fcd.Update(obj)
        '                If nResult > 0 Then
        '                    _sesshelper.SetSession("sessIPDetail", obj.IndentPartDetails)
        '                    lblStatusValue.Text = "Validasi"
        '                    btnSave.Enabled = False
        '                    btnValidasi.Enabled = False
        '                    btnNew.Enabled = True
        '                    dtgIPDetail.Columns(dtgIPDetail.Columns.Count - 1).Visible = False
        '                    dtgIPDetail.ShowFooter = False

        '                    MessageBox.Show("Proses Validasi Berhasil")
        '                Else
        '                    MessageBox.Show("Proses Validasi Gagal")
        '                End If
        '            Else
        '                MessageBox.Show("Proses Validasi Tidak Diperbolehkan")
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show("Proses Validasi Gagal")
        'End Try
    End Sub

#End Region

#Region "Custom Method"

    Private Sub BindDdlPaymentType()
        _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve("I")
        If Not _sparePartPOTypeTOP.IsTOP Then
            ddlTermOfPayment.Enabled = False
            Exit Sub
        End If

        Dim dlr As Dealer = CType(Session("DEALER"), Dealer)
        Dim spCa As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccountFacade(User).RetrieveByDealerCode(dlr.DealerCode)

        If spCa.Status = 0 Then
            ViewState.Add("AllowTOP", False)
        Else
            ViewState.Add("AllowTOP", True)
        End If

        Dim listOfPayments As ArrayList

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            listOfPayments = New TermOfPaymentFacade(User).RetrieveActivePaymentTypeList()
        Else
            Dim oTopCA As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
            If Not IsNothing(oTopCA) Then
                listOfPayments = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
            End If
        End If

        ddlTermOfPayment.DataSource = listOfPayments
        ddlTermOfPayment.DataValueField = "ID"
        ddlTermOfPayment.DataTextField = "Description"
        ddlTermOfPayment.DataBind()
        ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
        Dim sesCod As Boolean = False

        If IsNothing(_sesshelper.GetSession("COD")) Then
            ViewState.Add("COD", False)
        Else
            sesCod = True
        End If
        If IsNothing(listOfPayments) _
            OrElse spCa.Status = 0 _
            OrElse sesCod Then
            ddlTermOfPayment.Items.Clear()
            ddlTermOfPayment.Enabled = False
            ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
            ddlTermOfPayment.SelectedIndex = 0
            ViewState("COD") = True
        End If
        _sesshelper.RemoveSession("COD")
    End Sub

    Private Function InitialPageSession() As Boolean 'ByVal nID As Integer)
        'btnBack.Visible = False
        If Not IsNothing(Session("DEALER")) Then
            ViewState.Add("vsAccess", "insert")
            ViewState.Add("vsSave", "new")
            _sesshelper.SetSession("sessDealer", Session("DEALER")) 'New DealerFacade(User).Retrieve(nID))
            If IsNothing(Session("sessIPDetail")) Then
                _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
            Else
                _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)
            End If
            ddlMaterialType.SelectedValue = 0
            lblDealerCode.Text = CType(Session("sessDealer"), Dealer).DealerCode
            lblDealerName.Text = CType(Session("sessDealer"), Dealer).DealerName
            lblDealerTerm.Text = CType(Session("sessDealer"), Dealer).SearchTerm2
            BindDropDown()
            lblStatusValue.Text = "Baru"
            Return True
        End If
        Return False
    End Function

    Private Sub BindMaterialType()
        Dim arl As ArrayList = New EnumMaterialType().RetrieveType()
        ddlMaterialType.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
        For Each imat As EnumMaterial In arl
            ddlMaterialType.Items.Add(New ListItem(imat.NameType, imat.ValType.ToString))
        Next
        ddlMaterialType.SelectedIndex = -1
    End Sub

    Private Sub BindDropDown()
        BindMaterialType()
        CommonFunction.BindFromEnum("IndentPartPaymentType", ddlPaymentType, Me.User, False, "NameType", "ValType")

        DdlDesc.DataValueField = "ID"
        DdlDesc.DataTextField = "Name"
        DdlDesc.DataSource = EnumIndentDesc.RetrieveIndentDesc
        DdlDesc.DataBind()
        DdlDesc.SelectedIndex = 0
    End Sub

    Private Sub BindIPDetail()
        _arlIPDetail = CType(Session("sessIPDetail"), ArrayList)
        dtgIPDetail.DataSource = CType(Session("sessIPDetail"), ArrayList)
        dtgIPDetail.DataBind()
    End Sub

    Private Sub SetDtgIPDetailItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objIndentPartDetail As IndentPartDetail = e.Item.DataItem
        'Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblFPopUpSparePart"), Label)
        'lblPopUp.Attributes("onclick") = "ShowPPKodeBarangSelection();"

        Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtFPartNumber"), TextBox)
        Dim lblFPopUpSparePartNew As Label = CType(e.Item.Cells(1).FindControl("lblFPopUpSparePart"), Label)
        lblFPopUpSparePartNew.Attributes("onclick") = "ShowPPKodeBarangSelection(0);"

        Dim txtQty As TextBox = CType(e.Item.FindControl("txtFQTY"), TextBox)

        Dim txtAddPriceNew As TextBox = CType(e.Item.FindControl("txtAddPrice"), TextBox)
        txtAddPriceNew.Text = ""


        'txtPartNumber.Attributes.Add("readonly", "readonly")
        'txtPartNumber.Attributes.Remove("readonly")
        If _blnDealer Then
            'txtPartNumber.ReadOnly = False
            'lblFPopUpSparePartNew.Enabled = True
            'txtQty.ReadOnly = False
            'txtAddPriceNew.ReadOnly = True

            txtPartNumber.Attributes.Remove("readonly")
            lblFPopUpSparePartNew.Enabled = True
            txtQty.Attributes.Remove("readonly")
            txtAddPriceNew.Attributes.Add("readonly", "readonly")

        Else
            'txtPartNumber.ReadOnly = True
            'lblFPopUpSparePartNew.Enabled = False
            'txtQty.ReadOnly = True
            'txtAddPriceNew.ReadOnly = False

            txtPartNumber.Attributes.Add("readonly", "readonly")
            lblFPopUpSparePartNew.Enabled = False
            txtQty.Attributes.Add("readonly", "readonly")
            txtAddPriceNew.Attributes.Remove("readonly")
        End If
    End Sub

    Private Sub SetDtgIPDetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objIndentPartDetail As IndentPartDetail = e.Item.DataItem

        If Not IsNothing(e.Item.FindControl("lbtnEdit")) Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = ubahPrivilege
        End If


        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Text = (e.Item.ItemIndex + 1).ToString

        Dim lbtnDeleteNew As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
        If Not IsNothing(lbtnDeleteNew) Then
            'lbtnDeleteNew.Attributes.Add("OnClick", New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dibatalkan?"))
            'lbtnDeleteNew.Attributes.Add("OnClick", New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dibatalkan?"))
            'If Not _blnDealer Then
            '    ' KTB hanya bs edit price saja
            '    lbtnDeleteNew.Visible = False
            'End If
            lbtnDeleteNew.Visible = hapusPrivilege
        End If

        Dim _iDDetail As IndentPartDetail = CType(e.Item.DataItem, IndentPartDetail)

        Dim lnkTextPopUp As LinkButton = CType(e.Item.FindControl("lbtnPopUpText"), LinkButton)

        'If ddlMaterialType.SelectedIndex = 3 Then
        If Not IsNothing(lnkTextPopUp) And Not IsNothing(_iDDetail) Then
            lnkTextPopUp.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpIndentDescription.aspx?ID=" & _iDDetail.ID & "', '', 400, 400);return false;")
            lnkTextPopUp.Visible = True
        End If

        'Else
        'lnkTextPopUp.Visible = False
        'End If

        If _indentPHID = 0 Then
            lnkTextPopUp.Visible = False
        End If


        Dim lblPriceNew As Label = CType(e.Item.FindControl("lblPrice"), Label)
        If Not IsNothing(lblPriceNew) And Not IsNothing(_iDDetail) Then
            lblPriceNew.Text = objIndentPartDetail.Price.ToString("#,##0")
        End If


        'For Each _iDDetail As IndentPartDetail In CType(e.Item.DataItem, IndentPartDetail)
        '    Dim lnkTextPopUp As LinkButton = CType(e.Item.FindControl("lbtnPopUpText"), LinkButton)
        '    lnkTextPopUp.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpIndentDescription.aspx?ID=" & _iDDetail.ID & "', '', 400, 400);return false;")
        '    If ddlMaterialType.SelectedIndex = 3 Then
        '        lnkTextPopUp.Visible = True
        '    Else
        '        lnkTextPopUp.Visible = False
        '    End If
        'Next

        objDealer = _sesshelper.GetSession("DEALER")
        Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
        Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
        If _state Then
            lbtnDelete.Visible = False
            lbtnEdit.Visible = False

        Else
            Select Case ddlMaterialType.SelectedValue
                Case EnumMaterialType.MaterialType.Parts
                    If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        If lbtnDelete.Visible = True Then lbtnDelete.Visible = bCekPartPriv
                        If lbtnEdit.Visible = True Then lbtnEdit.Visible = bCekPartPriv
                    Else
                        If lbtnEdit.Visible = True Then lbtnEdit.Visible = bCekStatusEditPriv
                    End If
                Case EnumMaterialType.MaterialType.Tools

                    If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        If lbtnDelete.Visible = True Then lbtnDelete.Visible = bCekToolsPriv
                        If lbtnEdit.Visible = True Then lbtnEdit.Visible = bCekToolsPriv
                    Else
                        'If _state Then
                        '    If lbtnEdit.Visible = True Then lbtnEdit.Visible = bCekToolsPriv
                        'Else
                        If lbtnEdit.Visible = True Then lbtnEdit.Visible = bCekStatusEditPriv
                        'End If

                    End If
                Case EnumMaterialType.MaterialType.Accessories
                    If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        If lbtnDelete.Visible = True Then lbtnDelete.Visible = bCekAccessPriv
                        If lbtnEdit.Visible = True Then lbtnEdit.Visible = bCekAccessPriv
                    Else
                        If lbtnEdit.Visible = True Then lbtnEdit.Visible = bCekStatusEditPriv
                    End If
            End Select
        End If


        'bug 719
        If _blnDealer Then

        Else
            'kTB hanya bisa edit harga bila tipe=tools dan status=baru
            If CByte(ddlMaterialType.SelectedValue) <> EnumMaterialType.MaterialType.Tools Or objIndentPartDetail.IndentPartHeader.StatusKTB <> EnumIndentPartStatus.IndentPartStatusKTB.Baru Then
                'Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
                If Not IsNothing(lbtnEdit) Then
                    lbtnEdit.Visible = False
                End If
            End If

        End If

        Dim lbtnPopUpText As LinkButton = CType(e.Item.FindControl("lbtnPopUpText"), LinkButton)
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lbtnPopUpText.Visible = False
        Else
            If lbtnPopUpText.Visible = True Then
                lbtnPopUpText.Visible = bCekKTBNotePriv
            End If
        End If

        If _isPopup = 1 Then
            lbtnPopUpText.Visible = False
        End If

    End Sub

    Private Sub SetDtgIPDetailItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objIndentPartDetail As IndentPartDetail = e.Item.DataItem
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Clear()
        e.Item.Cells(0).Controls.Add(lNum)

        'Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblEPopUpSparePart"), Label)
        'lblPopUp.Attributes("onclick") = "ShowPPKodeBarangSelection();"

        Dim txtEditPriceNew As TextBox = CType(e.Item.FindControl("txtEditPrice"), TextBox)
        Dim txtEditPriceTemp As TextBox = CType(e.Item.FindControl("txtEditPriceTemp"), TextBox)
        txtEditPriceNew.Text = objIndentPartDetail.Price.ToString("#,##0")
        txtEditPriceTemp.Text = objIndentPartDetail.Price.ToString("#,##0")

        Dim lblEditPrice As Label = CType(e.Item.FindControl("lblEditPrice"), Label)
        lblEditPrice.Text = objIndentPartDetail.Price.ToString("#,##0")

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

    Private Sub RenderPartItem(ByVal objpart As SparePartMaster, _
    ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim lblPartName As Label = CType(e.Item.FindControl("lblFPartName"), Label)
        Dim lblModelCode As Label = CType(e.Item.FindControl("lblFModel"), Label)
        lblPartName.Text = objpart.PartName
        lblModelCode.Text = objpart.ModelCode
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
        Dim i As Integer
        Dim bResult As Boolean = False
        For i = 0 To arlIPDetail.Count - 1
            If CType(arlIPDetail(i), IndentPartDetail).SparePartMaster.PartNumber.Trim().ToUpper() = partNumber.Trim().ToUpper() AndAlso nIndeks <> i Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Private Sub RemoveAllSession()
        _sesshelper.RemoveSession("sessIPHeader")
        _sesshelper.RemoveSession("sessIPDetail")
    End Sub

    Private Function InsertNewIndentPart() As Integer
        Dim ObjIndent As IndentPartHeader = GetIndentHeader()
        Return New IndentPartHeaderFacade(User).InsertIndentPartheader(ObjIndent, CType(Session("sessIPDetail"), ArrayList))

    End Function

    Private Function EditIP() As Integer
        Dim ObjIPH As IndentPartHeader = CType(Session("sessIPHeader"), IndentPartHeader)
        Return New IndentPartHeaderFacade(User).UpdateIndentPartheader(ObjIPH, CType(Session("sessIPDetail"), ArrayList))
    End Function

    Private Function DisplayTransactionResult(ByVal nID As Integer)
        Dim stt As Boolean = False
        Dim objIndentPartH As IndentPartHeader = New IndentPartHeaderFacade(User).Retrieve(nID)

        ViewState("messCancelIndent") = "Yakin Pembatalan PESANAN akan dibatalkan ?"

        txtPONumber.Text = objIndentPartH.RequestNo
        lblDealerCode.Text = objIndentPartH.Dealer.DealerCode
        lblDealerName.Text = objIndentPartH.Dealer.DealerName
        lblDealerTerm.Text = objIndentPartH.Dealer.SearchTerm2
        If objIndentPartH.Status = EnumIndentPartStatus.IndentPartStatusDealer.Baru Then
            btnValidasi.Enabled = bCekSendPriv
        Else
            btnValidasi.Enabled = False
        End If

        'refer bug 1084
        Dim objUserInfo As UserInfo = CType(_sesshelper.GetSession("LOGINUSERINFO"), UserInfo)
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblStatusValue.Text = objIndentPartH.StatusDealerDesc
        Else
            lblStatusValue.Text = objIndentPartH.StatusKTBDesc
        End If
        '--end refer bug 1084

        icOrderDate.Value = objIndentPartH.RequestDate  'String.Format("{0:dd/MM/yyyy}", objIndentPartH.PODate)
        ddlMaterialType.SelectedValue = objIndentPartH.MaterialType.ToString
        ddlPaymentType.SelectedValue = objIndentPartH.PaymentType.ToString
        If Not IsNothing(objIndentPartH.TermOfPayment) Then
            ddlTermOfPayment.SelectedValue = objIndentPartH.TermOfPayment.ID.ToString()
        End If
        _arlIPDetail = objIndentPartH.IndentPartDetails
        _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        _sesshelper.SetSession("sessIPHeader", objIndentPartH)
        ViewState.Add("vsAccess", "edit")
        BindIPDetail()

        If objIndentPartH.MaterialType = EnumMaterialType.MaterialType.Tools And _
        (objIndentPartH.Status = EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi Or objIndentPartH.Status = EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi) And _
        _blnDealer Then
            If _state Then
                ddlPaymentType.Enabled = False
            Else
                ddlPaymentType.Enabled = True
            End If
            ddlPaymentType.Items(0).Text = "Silakan Pilih"
        Else
            ddlPaymentType.Enabled = False
            ddlPaymentType.Items(0).Text = ""
        End If
    End Function

    Private Function GetIndentHeader() As IndentPartHeader
        Dim ObjIndent As New IndentPartHeader
        If IsNothing(Session("sessIPHeader")) Then
            ObjIndent = New IndentPartHeader
            ObjIndent.ChassisNumber = TxtChassisNo.Text.Trim
            ObjIndent.DescID = DdlDesc.SelectedValue
            ObjIndent.RequestDate = icOrderDate.Value
            ObjIndent.Dealer = CType(Session("sessDealer"), Dealer)
            ObjIndent.MaterialType = CInt(ddlMaterialType.SelectedValue)
            ObjIndent.StatusKTB = Nothing
            If ddlTermOfPayment.SelectedValue <> "" Then
                ObjIndent.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(ddlTermOfPayment.SelectedValue, Integer))
            Else
                ObjIndent.TermOfPayment = Nothing
            End If
            _sesshelper.SetSession("sessIPHeader", ObjIndent)
        Else
            ObjIndent = CType(Session("sessIPHeader"), IndentPartHeader)
            ObjIndent.StatusKTB = Nothing
        End If
        Return ObjIndent
    End Function

    Private Function cekValiditas() As Boolean
        cekValiditas = False
        'If icOrderDate.Value >= DateTime.Now Then
        '    MessageBox.Show("Tanggal Order Tidak Valid")
        'End If

        If ddlMaterialType.SelectedIndex < 1 Then
            Return False

        End If

        If CInt(DdlDesc.SelectedValue) = EnumIndentDesc.IndentDesc.Silakan_Pilih Then
            MessageBox.Show("Keterangan harus dipilih")
            Return False
        ElseIf CInt(DdlDesc.SelectedValue) = EnumIndentDesc.IndentDesc.Pasang_or_Stamping Then
            If TxtChassisNo.Text.Trim = "" Then
                If ddlMaterialType.SelectedValue = EnumMaterialType.MaterialType.Accessories Then
                    MessageBox.Show("Untuk keterangan 'Pasang' pada Type 'Accessories' anda harus menentukan chassis")
                    Return False
                End If

            Else
                If Not IsChassisExist() Then
                    MessageBox.Show("Nomor chassis tidak terdaftar")
                    Return False
                End If
            End If
        ElseIf CInt(DdlDesc.SelectedValue) = EnumIndentDesc.IndentDesc.Kirim Then
            If TxtChassisNo.Text.Trim <> "" Then
                If Not IsChassisExist() Then
                    MessageBox.Show("Nomor chassis tidak terdaftar")
                    Return False
                End If
            End If
        End If

        If ddlTermOfPayment.SelectedValue.Trim = "" Then
            MessageBox.Show("Cara pembayaran belum diisi")
            Return False
        End If

        Return True

    End Function

    Private Function IsChassisExist() As Boolean
        Dim objChassis As ChassisMaster = New ChassisMasterFacade(User).Retrieve(TxtChassisNo.Text.Trim)
        If objChassis.ID = 0 Then
            Return False
        Else
            Dim strCompanyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            If objChassis.Category.ProductCategory.Code.Trim.ToUpper = strCompanyCode.Trim.ToUpper Then
                Return True
            Else
                Return False
            End If
        End If
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

    Private Sub DetailOnly(ByVal bval As Boolean)
        txtPONumber.Enabled = Not bval
        ddlMaterialType.Enabled = Not bval
        'icOrderDate.Enabled = Not bval
        btnNew.Enabled = False
        'btnValidasi.Enabled = False
        btnDelete.Enabled = False

        If Not _state Then
            ' dari Dealer
            'btnSave.Enabled = bCekStatusEditDetailDealerPriv
            If _indentPHID <> 0 Then
                btnDelete.Enabled = True
            End If
        Else
            ' dari KTB
            btnSave.Enabled = False
            'dtgIPDetail.Columns(5).Visible = False
            dtgIPDetail.ShowFooter = False
            ddlTermOfPayment.Enabled = False
            btnDelete.Enabled = False
            ddlMaterialType.Enabled = False
        End If

    End Sub

    Private Sub showNote(ByVal bval As Boolean)
        Dim dtGrid As DataGrid = dtgIPDetail
        Dim e As System.Web.UI.WebControls.DataGridItemEventArgs
        If ddlMaterialType.SelectedValue = 3 Then
            For Each itm As DataGridItemCollection In dtGrid.Items
                e.Item.FindControl("Linkbutton1").Visible = bval
            Next
        Else
            For Each itm As DataGridItemCollection In dtGrid.Items
                e.Item.FindControl("Linkbutton1").Visible = bval
            Next
        End If

    End Sub

    Private Function trySaveData() As Boolean
        Dim nresult As Integer = 0

        If CType(Session("sessIPDetail"), ArrayList).Count > 0 Then
            If cekValiditas() Then
                Try
                    nresult = InsertNewIndentPart()
                    If nresult > 0 Then
                        Dim objIPSuccess As IndentPartHeader
                        objIPSuccess = New IndentPartHeaderFacade(User).Retrieve(nresult)
                        MessageBox.Show("Data Indent Part dengan No. " & objIPSuccess.RequestNo & " Telah Disimpan")
                        Return True
                    Else
                        MessageBox.Show(SR.SaveFail)
                        Return False
                    End If
                Catch ex As Exception
                    MessageBox.Show("Gagal simpan Indent Part PO " & ex.Message)
                    Return False
                End Try
            Else
                Return False
            End If
        Else
            MessageBox.Show(SR.GridIsEmpty("PO Detail"))
            Return False
        End If
    End Function

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        _indentPHID = Request.QueryString("IndentPartHeaderID")
        If _indentPHID = 0 Then
            If Not SecurityProvider.Authorize(Context.User, SR.PengajuanPartIndentPartCreate_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=INDENT PART - Pengajuan")
            End If
        ElseIf _state Then
            If Not SecurityProvider.Authorize(Context.User, SR.PengajuanIndentPartView_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=INDENT PART - View Pengajuan")
            End If
        Else
            If Not (SecurityProvider.Authorize(Context.User, SR.StatusListIndentPartEdit_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.StatusListIndentPartEditDetail_Privilege)) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=INDENT PART - Ubah Pengajuan")
            End If
        End If
    End Sub

    Dim bCekPartPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.PengajuanPartIndentPartCreate_Privilege)
    Dim bCekToolsPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.PengajuanToolsIndentPartCreate_Privilege)
    Dim bCekAccessPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.PengajuanAksesorisIndentPartCreate_Privilege)
    Dim bCekSendPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.PengajuanIndentPartKirim_Privilege)
    Dim bCekKTBNotePriv As Boolean = SecurityProvider.Authorize(Context.User, SR.Edit_popup_daftar_status_indent_part_Privilege)

    Private Function CekPriv(ByVal tipe As Integer) As Boolean
        Select Case tipe
            Case EnumMaterialType.MaterialType.Parts 'Parts
                If bCekPartPriv Then
                    btnSave.Enabled = True
                    If _indentPHID <> 0 Then
                        btnDelete.Enabled = True
                    End If
                    btnNew.Enabled = True
                    Return True
                Else
                    Return False
                End If
            Case EnumMaterialType.MaterialType.Tools 'Tools
                If bCekToolsPriv Then
                    btnSave.Enabled = True
                    If _indentPHID <> 0 Then
                        btnDelete.Enabled = True
                    End If
                    btnNew.Enabled = True
                    Return True
                Else
                    Return False
                End If
            Case EnumMaterialType.MaterialType.Accessories 'Accessories
                If bCekAccessPriv Then
                    btnSave.Enabled = True
                    If _indentPHID <> 0 Then
                        btnDelete.Enabled = True
                    End If
                    btnNew.Enabled = True
                    Return True
                Else
                    Return False
                End If
        End Select
    End Function
    'dari dealer
    Dim bCekStatusEditDetailDealerPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListIndentPartEditDetail_Privilege)

    'dari KTB
    Dim bCekStatusEditPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.StatusListIndentPartEdit_Privilege)
#End Region

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

        CommonFunction.SortArraylist(arlToSort, GetType(IndentPartDetail), ViewState("CurrentSortColumn"), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        BindIPDetail()
    End Sub


    Private Sub lntnCheckChassis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lntnCheckChassis.Click
        If TxtChassisNo.Text.Trim <> "" Then
            If IsChassisExist() Then
                TxtChassisNo.BackColor = System.Drawing.Color.White
            Else
                TxtChassisNo.BackColor = System.Drawing.Color.Red
                LblTypeWarna.Text = ""
            End If
        End If
    End Sub

    Private Sub SetChassisBehaviour()
        If DdlDesc.Items.Count > 0 Then
            If DdlDesc.SelectedValue = "1" Then
                lntnCheckChassis.Style.Add("display", "")
                lblSearchChassis.Style.Add("display", "")
            Else
                lntnCheckChassis.Style.Add("display", "none")
                lblSearchChassis.Style.Add("display", "none")
            End If
        Else
            lntnCheckChassis.Style.Add("display", "none")
            lblSearchChassis.Style.Add("display", "none")
        End If

    End Sub


    Private Sub GeneratePO(ByVal RequestNo As String)
        Dim objIPDetailfacade As IndentPartDetailFacade = New IndentPartDetailFacade(User)
        Dim arlToUpdate As ArrayList = New ArrayList()
        Dim arlIPDetail As ArrayList = New ArrayList()

        Dim criteriasIPH As CriteriaComposite
        criteriasIPH = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasIPH.opAnd(New Criteria(GetType(IndentPartHeader), "RequestNo", MatchType.InSet, "('" & RequestNo & "')"))

        Dim IndentPartHeaderData As ArrayList = New IndentPartHeaderFacade(User).Retrieve(criteriasIPH)

        If IndentPartHeaderData.Count > 0 Then
            Dim StrIndentPartHeaderID As String = String.Empty
            For Each objIPH As IndentPartHeader In IndentPartHeaderData
                StrIndentPartHeaderID += objIPH.ID.ToString() + ","
            Next
            If StrIndentPartHeaderID.Length > 0 Then
                StrIndentPartHeaderID = Left(StrIndentPartHeaderID, StrIndentPartHeaderID.Length - 1)
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.ID", MatchType.InSet, "(" & StrIndentPartHeaderID & ")"))
            arlIPDetail = objIPDetailfacade.RetrieveByCriteria(criterias, "IndentPartHeader.Dealer.DealerCode", Sort.SortDirection.ASC)

            Dim arlIPDetailFilter = New System.Collections.ArrayList((From item As IndentPartDetail In arlIPDetail.OfType(Of IndentPartDetail)()
                    Where item.SisaQty > 0
                    Select item).ToList())

            For Each arrIPD As IndentPartDetail In arlIPDetailFilter 'arlIPDetail
                arrIPD.AllocationQty = arrIPD.SisaQty
                arlToUpdate.Add(arrIPD)
            Next
        End If

        Dim rslt As Integer = New IndentPartDetailFacade(User).UpdateAlokasi(arlToUpdate)

        If rslt > 0 Then
            Dim Retval As String = objIPDetailfacade.GeneratePO(arlToUpdate) '(arlIPDetail')
            If Retval <> "" Then
                MessageBox.Show("Generate PO Dengan No " & Retval & " Berhasil")
            End If
        Else
            MessageBox.Show("Gagal update data")
        End If

    End Sub

End Class
