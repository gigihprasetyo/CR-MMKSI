#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.SAP
Imports System.IO
Imports System.Configuration
Imports KTB.DNet.Lib
Imports System.Linq
Imports System.Collections.Generic

Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmSPKHeader
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKategori As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKondisiPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesmanCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnKonsumen As System.Web.UI.WebControls.Button
    Protected WithEvents lblLevelSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblJabatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblDibuatOleh As System.Web.UI.WebControls.Label
    Protected WithEvents dtgPesananKendaraan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoSPK As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSPKOpenDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblShowSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents lblNamaSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents ddlInvoiceYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlDeliveryYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents txtNomorPesanan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSPKReferenceNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlDeliveryMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlInvoiceMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTotalUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHarga As System.Web.UI.WebControls.Label
    Protected WithEvents hideKodeWarna As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hideKodeBody As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ddlKategoriEdit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hideCategory As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents txtUrlToBack As System.Web.UI.WebControls.TextBox
    Protected WithEvents hideKategori As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hideKodeTipe As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblValidasiOleh As System.Web.UI.WebControls.Label
    Protected WithEvents lblValidateDate As System.Web.UI.WebControls.Label
    Protected WithEvents hideNamaWarna As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnProfile As System.Web.UI.WebControls.Button
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblEvidenceFile As System.Web.UI.WebControls.Label
    Protected WithEvents btnDeleteFile As System.Web.UI.WebControls.ImageButton
    Protected WithEvents icDealerSPKDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblPopUpEvent As System.Web.UI.WebControls.Label
    Protected WithEvents txtCampaignName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEventTypeID As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlBabitEventType As System.Web.UI.WebControls.DropDownList

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
    Private objSPKHeader As KTB.DNet.Domain.SPKHeader
    Private objSPKDetail As KTB.DNet.Domain.SPKDetail
    Private objVechileType As KTB.DNet.Domain.VechileType
    'Private objSPKStatusHistory As KTB.DNet.Domain.SPKStatusHistory
    Private Mode As enumMode.Mode
    Private SPKID As Integer
    Private IsBackPage As Boolean
    Private DealerCode As String
    Private objDealer As Dealer
    Private objUser As UserInfo
    Private sessionHelper As New SessionHelper
    Private _vstSPKHeader As String = "_vstSPKHeader"
    Private _sessTypeExpander As String = "TypeExpander"

#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If sessionHelper.GetSession("PrevPage") Is Nothing Then
            txtUrlToBack.Text = ""
        Else
            txtUrlToBack.Text = sessionHelper.GetSession("PrevPage")
        End If
        UserPrivilege()
        objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        objUser = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)

        If Not IsPostBack Then
            ViewState("SPKDMS") = False
            lblPopUpEvent.Attributes("onclick") = "ShowPPEventDealerSelection();"

            If Not Request.Item("sessionName") Is Nothing Then
                If IsNothing(ViewState.Item(Me._vstSPKHeader)) Then
                    ViewState.Add(Me._vstSPKHeader, Request.Item("sessionName"))
                Else
                    ViewState.Item(Me._vstSPKHeader) = Request.Item("sessionName")
                End If
            Else
                If IsNothing(ViewState.Item(Me._vstSPKHeader)) Then
                    ViewState.Add(Me._vstSPKHeader, "FrmSPKHeader.SPKHeader" & Now.ToString("yyyyMMddhhmmssfff"))
                Else
                    ViewState.Item(Me._vstSPKHeader) = "FrmSPKHeader.SPKHeader" & Now.ToString("yyyyMMddhhmmssfff")
                End If
            End If

            'objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
            Mode = CType(Request.QueryString("Mode"), enumMode.Mode)
            ViewState("Mode") = Mode
            IsBackPage = IIf(Request.QueryString("isBack") = 1, True, False)
            SetMode()
            RetrieveMaster()
            CheckDealerSystems()
            BindDataToPage()
            If CekVechileModelExpander() Then
                dtgPesananKendaraan.ShowFooter = False
            End If

            BindDdlBabitEventType(ddlBabitEventType, txtCampaignName, lblPopUpEvent)
            If txtCampaignName.Text.Trim <> "" Then
                Dim _oDealer As Dealer
                If IsNothing(objSPKHeader) OrElse objSPKHeader.ID = 0 Then
                    objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
                    If Not IsNothing(objSPKHeader) Then
                        _oDealer = objSPKHeader.Dealer
                    Else
                        _oDealer = objDealer
                    End If
                Else
                    _oDealer = objSPKHeader.Dealer
                End If
                If IsNothing(_oDealer) Then _oDealer = New Dealer

                Dim oBabitHeader As New BabitHeader
                Dim dsBabitEvent As DataSet = New DataSet
                dsBabitEvent = New BabitHeaderFacade(User).RetrieveFromSPByPopUp(_oDealer.ID, txtCampaignName.Text, "", Nothing, Nothing, False, "")
                If dsBabitEvent.Tables.Count > 0 Then
                    If dsBabitEvent.Tables(0).Rows.Count > 0 Then
                        txtCampaignName.Visible = True
                        lblPopUpEvent.Visible = True
                        ddlBabitEventType.SelectedValue = "0"  '--- Babit & Event
                    Else
                        txtCampaignName.Visible = True
                        lblPopUpEvent.Visible = False
                        ddlBabitEventType.SelectedValue = "1"   '--- Lain - lain
                    End If
                Else
                    txtCampaignName.Visible = True
                    lblPopUpEvent.Visible = False
                    ddlBabitEventType.SelectedValue = "1"        '--- Lain - lain
                End If
            Else
                txtCampaignName.Visible = False
                lblPopUpEvent.Visible = False
                ddlBabitEventType.SelectedIndex = 0     '---Silahkan Pilih
            End If
            If Mode = enumMode.Mode.ViewMode Then
                ddlBabitEventType.Enabled = False
                txtCampaignName.Enabled = False
                lblPopUpEvent.Visible = False
                btnDeleteFile.Visible = False
            End If
        End If
        CheckDealerSystems()
        SalesmenInfo()

        lblShowSalesman.Attributes("onClick") = "ShowSalesmanSelection();"
        txtSalesmanCode.Attributes.Add("readonly", "readonly")
        If CBool(ViewState("SPKDMS")) Then
            dtgPesananKendaraan.ShowFooter = False
        End If

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        'Validasi File
        Dim isStatusSPKSelesai As Boolean = False
        Dim isFileValidation As Boolean = False
        Dim isEvidenceMandatory As String = "0"
        Dim objfacade As AppConfigFacade = New AppConfigFacade(User)
        Dim objappconfig As AppConfig = objfacade.Retrieve("SPKEvidence")
        If Not IsNothing(objappconfig) AndAlso objappconfig.ID > 0 Then
            isEvidenceMandatory = objappconfig.Value.Trim
        End If
        'If isEvidenceMandatory = "1" Then
        '    isFileValidation = FileValidation()
        '    If Not isFileValidation Then
        '        Exit Sub
        '    End If
        'End If
        '
        If DataFile.Visible = True AndAlso (DataFile.PostedFile.FileName <> String.Empty) Then
            isFileValidation = FileSizeChecking()
            If isFileValidation = False Then
                Exit Sub
            End If
        End If

        ReCheckMCPStatus()
        If ValidateSave() Then
            objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))

            If IsNothing(_arrNewType) Then
                _arrNewType = GetNewType()
            End If
            Dim _isNewType As Boolean = False
            If _arrNewType.Count > 0 Then
                For Each item As SPKDetail In objSPKHeader.SPKDetails
                    For Each _newType As VechileType In _arrNewType
                        If (item.VehicleTypeCode.ToString.ToUpper = _newType.VechileTypeCode.Trim.ToUpper) Then
                            _isNewType = True
                        End If
                    Next
                Next
                If _isNewType AndAlso (isEvidenceMandatory = "1") Then
                    If DataFile.Visible = True AndAlso (DataFile.PostedFile.FileName = String.Empty) Then
                        MessageBox.Show("Lampiran Bukti SPK harus diisi")
                        Exit Sub
                    End If
                End If
            End If

            If Not (objSPKHeader.SPKDetails.Count = 0) Then
                Mode = ViewState("Mode")
                BindDataToObject()
                If Mode = enumMode.Mode.NewItemMode Then
                    'SaveToDatabase()
                    Dim result As String = String.Empty
                    Dim rndGen As RandomGenerator = New RandomGenerator
                    result = rndGen.GetActivationCode(8)
                    objSPKHeader.ValidationKey = result.ToUpper

                    Dim int As Integer = New SPKHeaderFacade(User).Insert(objSPKHeader)

                    If int > 0 Then
                        objSPKHeader = New SPKHeaderFacade(User).Retrieve(int)
                        sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader)
                        RecordStatusChangeHistory(objSPKHeader, CType(EnumStatusSPK.Status.Awal, Integer))
                        If isFileValidation Then
                            If DataFile.Visible = True AndAlso (DataFile.PostedFile.FileName <> String.Empty) Then
                                CopyFileToServer(DataFile, objSPKHeader)
                            End If
                        End If
                        Mode = enumMode.Mode.EditMode
                        ViewState("Mode") = Mode

                        'Update SAPCustomer Status --> DEAL/SPK
                        If Not IsNothing(objSPKHeader.SPKCustomer.SAPCustomer) Then
                            Dim objSAPCustomer As SAPCustomer = objSPKHeader.SPKCustomer.SAPCustomer
                            objSAPCustomer.Status = EnumSAPCustomerStatus.SAPCustomerStatus.Deal_SPK
                            Dim iReturn As Integer = New SAPCustomerFacade(User).Update(objSAPCustomer)
                        End If

                        SetButtonEditMode()
                        BindHeaderToForm()
                        BindDetailToGrid()
                        MessageBox.Show("Data Berhasil Disimpan, Silahkan Lanjutkan Mengisi Data Konsumen Faktur")
                        Response.Redirect("FrmSPKHeaderProfile.aspx?Id=" & int & "&Mode=2")
                    End If
                Else
                    'start add by anh - indent system 201707
                    'update status spkheader, depend on spkfaktur againts qty spk detail
                    'update again 2017/01/05 by rudi
                    Dim iQtyH As Integer = 0
                    Dim iQtyD As Integer = 0
                    Dim iQtyBatal As Integer = 0
                    Dim iQtyFinish As Integer = 0
                    Dim iAllQtySPKDetails As Integer = 0
                    For Each objSPKDetail As SPKDetail In objSPKHeader.SPKDetails
                        If objSPKDetail.Status = 1 Then
                            iQtyBatal = iQtyBatal + 1
                        Else
                            iQtyH = objSPKDetail.Quantity
                            If Not IsNothing(objSPKDetail.VechileColor.VechileType) Then
                                If Not IsNothing(objSPKHeader.SPKFakturs) Then
                                    For Each objSPKFaktur As SPKFaktur In objSPKHeader.SPKFakturs
                                        If Not objSPKFaktur.EndCustomer Is Nothing Then
                                            If Not objSPKFaktur.EndCustomer.ChassisMaster Is Nothing Then
                                                If Not objSPKFaktur.EndCustomer.ChassisMaster.VechileColor Is Nothing Then
                                                    If Not objSPKFaktur.EndCustomer.ChassisMaster.VechileColor.VechileType Is Nothing Then
                                                        If objSPKFaktur.EndCustomer.ChassisMaster.VechileColor.VechileType.ID = objSPKDetail.VechileColor.VechileType.ID Then
                                                            If objSPKFaktur.EndCustomer.ChassisMaster.FakturStatus <> "0" Then
                                                                iQtyD = iQtyD + 1
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                            If iQtyH = iQtyD Then
                                iQtyFinish = iQtyFinish + 1
                            End If
                        End If
                    Next
                    iAllQtySPKDetails = iQtyBatal + iQtyFinish

                    If (objSPKHeader.SPKDetails.Count > 0) AndAlso (iAllQtySPKDetails = objSPKHeader.SPKDetails.Count) Then
                        Dim oldStatus As Integer = objSPKHeader.Status
                        objSPKHeader.Status = CInt(EnumStatusSPK.Status.Selesai)
                        RecordStatusChangeHistory(objSPKHeader, oldStatus, CInt(EnumStatusSPK.Status.Selesai))
                        isStatusSPKSelesai = True
                    End If
                    If (objSPKHeader.SPKDetails.Count > 0) Then
                        Dim iBatal As Integer = 0
                        For Each _detail As SPKDetail In objSPKHeader.SPKDetails
                            If _detail.Status = 1 Then
                                iBatal = iBatal + 1
                            End If
                        Next
                        If (iBatal = objSPKHeader.SPKDetails.Count) Then
                            Dim oldStatus As Integer = objSPKHeader.Status
                            objSPKHeader.Status = CInt(EnumStatusSPK.Status.Batal)
                            RecordStatusChangeHistory(objSPKHeader, oldStatus, CInt(EnumStatusSPK.Status.Batal))
                        End If
                    End If
                    'end add by anh - indent system 201707 'oo

                    Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
                    Dim ireturn As Integer = objSPKHeaderFacade.UpdateTransaction(objSPKHeader)
                    If ireturn > 0 Then
                        If isFileValidation Then
                            If DataFile.Visible = True AndAlso (DataFile.PostedFile.FileName <> String.Empty) Then
                                CopyFileToServer(DataFile, objSPKHeader)
                            End If
                        End If
                        MessageBox.Show("Data Berhasil Disimpan")
                        If isStatusSPKSelesai Then Response.Redirect("FrmSPKDaftar.aspx")
                    Else
                        MessageBox.Show("Data Gagal Disimpan")
                    End If

                    sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader)
                    BindHeaderToForm()
                    BindDetailToGrid()
                End If

                btnSimpan.Enabled = False
                btnKonsumen.Enabled = False
                btnProfile.Enabled = True
                btnDeleteFile.Visible = False
            Else
                MessageBox.Show("Belum ada Pesanan Kendaraan")
            End If
            dtgPesananKendaraan.ShowFooter = False
        End If
    End Sub

    Private Sub btnKonsumen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKonsumen.Click
        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        'SalesmenInfo()
        Mode = ViewState("Mode")
        If Not IsNothing(objSPKHeader) Then
            If objSPKHeader.SPKDetails.Count > 0 Then
                If Mode = enumMode.Mode.NewItemMode Then
                    If Not IsNothing(Request.QueryString("CustId")) Then
                        Response.Redirect("FrmSPKCustomer.aspx?Mode=" & CType(Mode, Integer) & "&spkHeader=" & ViewState.Item(Me._vstSPKHeader) & "&CustId=" & Request.QueryString("CustId") & "&EmailMandatory=" & EmailMandatory())
                    Else
                        Response.Redirect("FrmSPKCustomer.aspx?Mode=" & CType(Mode, Integer) & "&spkHeader=" & ViewState.Item(Me._vstSPKHeader) & "&EmailMandatory=" & EmailMandatory())
                    End If
                Else
                    Response.Redirect("FrmSPKCustomer.aspx?Id=" & CType(Request.QueryString("Id"), Integer) & "&Mode=" & CType(Mode, Integer) & "&spkHeader=" & ViewState.Item(Me._vstSPKHeader) & "&EmailMandatory=" & EmailMandatory())
                End If
            Else
                lblError.Text = "Tentukan kendaraan yang akan di pesan"
            End If
        Else
            lblError.Text = "Tentukan kendaraan yang akan di pesan -"
        End If
    End Sub

    Private Function EmailMandatory() As Boolean
        For Each item As DataGridItem In dtgPesananKendaraan.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim lblViewKodeModelGrid As Label = CType(item.FindControl("lblViewKodeModel"), Label)
                If lblViewKodeModelGrid.Text.Trim.Contains("NI") OrElse lblViewKodeModelGrid.Text.Trim.Contains("NJ") OrElse lblViewKodeModelGrid.Text.Trim.Contains("NK") Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Sub dtgPesananKendaraan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPesananKendaraan.ItemDataBound

        If e.Item.ItemType = ListItemType.Footer Then
            SetDtgPesananKendaraanItemFooter(e)
        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetDtgPesananKendaraanItemEdit(e)
        End If
        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))

        Dim ddlKategori As DropDownList
        Dim ddlTambahan As DropDownList
        If (e.Item.ItemType = ListItemType.Footer) OrElse (e.Item.ItemType = ListItemType.EditItem) Then
            If e.Item.ItemType = ListItemType.Footer Then
                ddlKategori = CType(e.Item.FindControl("ddlFooterKategori"), DropDownList)
                ddlTambahan = CType(e.Item.FindControl("ddlFooterTambahan"), DropDownList)
            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                ddlKategori = CType(e.Item.FindControl("ddlEditKategori"), DropDownList)
                ddlTambahan = CType(e.Item.FindControl("ddlEditTambahan"), DropDownList)
            End If

            '--------add start by rudi
            Dim lbtnAdd As LinkButton
            lbtnAdd = CType(e.Item.FindControl("lbtnAdd"), LinkButton)
            If Not objSPKHeader.Dealer Is Nothing Then
                If objSPKHeader.Dealer.ID <> objDealer.ID Then
                    lbtnAdd.Visible = False
                End If
            End If
            '--------

            If Not IsNothing(ddlKategori) Then
                Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                Dim arrayListCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
                Dim blankItem As New ListItem("Silahkan Pilih", 0)
                ddlKategori.Items.Add(blankItem)
                For Each item As Category In arrayListCategory
                    Dim listItem As New ListItem(item.CategoryCode, item.ID)
                    listItem.Selected = False
                    ddlKategori.Items.Add(listItem)
                Next
                ddlKategori.Attributes("onclick") = "CategoryChanged(this);"
            End If

            Dim arrayListAdditional As ArrayList = New EnumSPKAdditional().RetrieveSPKAdditional()
            For Each item As EnumSPKAdditionalParts In arrayListAdditional
                Dim listItem As New ListItem(item.NameParts, item.ValParts)
                listItem.Selected = False
                ddlTambahan.Items.Add(listItem)
            Next
            If ddlTambahan.Items.Count > 0 Then
                ddlTambahan.SelectedIndex = 2 '-> Default List tambahan diubah menjadi 'Tidak Ada' 2017/12/27 by Miyuki
            Else
                ddlTambahan.ClearSelection()
            End If

            Dim objSAPCustomer As SAPCustomer
            If Not IsNothing(Request.QueryString("CustId")) AndAlso Request.QueryString("CustId").ToString().Trim() <> "" Then
                Dim objSAPCustomerFacade As SAPCustomerFacade = New SAPCustomerFacade(User)
                objSAPCustomer = objSAPCustomerFacade.Retrieve(CInt(Request.QueryString("CustId")))
            End If
            If Not IsNothing(objSAPCustomer) AndAlso objSPKHeader.SPKDetails.Count = 0 Then
                If Not IsNothing(objSAPCustomer.VechileType) Then
                    ddlKategori.SelectedValue = objSAPCustomer.VechileType.Category.ID
                End If
            End If

            If Not IsNothing(objSAPCustomer) Then
                Dim txtFooterKodeModel As TextBox
                Dim txtFooterUnit As TextBox
                If e.Item.ItemType = ListItemType.Footer Then
                    txtFooterKodeModel = CType(e.Item.FindControl("txtFooterKodeModel"), TextBox)
                    txtFooterUnit = CType(e.Item.FindControl("txtFooterUnit"), TextBox)
                End If
                If e.Item.ItemType = ListItemType.EditItem Then
                    txtFooterKodeModel = CType(e.Item.FindControl("txtEditKodeModel"), TextBox)
                    txtFooterUnit = CType(e.Item.FindControl("txtEditUnit"), TextBox)
                End If

                If objSPKHeader.SPKDetails.Count = 0 Then
                    If Not IsNothing(objSAPCustomer.VechileType) Then
                        e.Item.Cells(1).Text = objSAPCustomer.VechileType.Description
                        If Not IsNothing(txtFooterKodeModel) Then
                            txtFooterKodeModel.Text = objSAPCustomer.VechileType.VechileTypeCode
                        End If
                        If Not IsNothing(txtFooterUnit) Then
                            txtFooterUnit.Text = objSAPCustomer.Qty
                        End If
                    End If
                End If
            End If

        End If

        If Not (objSPKHeader.SPKDetails.Count = 0 Or e.Item.ItemIndex = -1) Then 'Or e.Item.ItemIndex = -1
            objSPKDetail = objSPKHeader.SPKDetails(e.Item.ItemIndex)

            Dim ddlRejectedReason As DropDownList = CType(e.Item.FindControl("ddlRejectedReason"), DropDownList)
            If Not IsNothing(ddlRejectedReason) Then
                BindDDLRejectedReason(ddlRejectedReason, objSPKDetail.RejectedReason)
                ddlRejectedReason.SelectedValue = objSPKDetail.RejectedReason
            End If

            If Not IsNothing(objSPKDetail.VechileColor) Then
                If (objSPKDetail.VechileColor.ColorCode = "ZZZZ") Then
                    e.Item.Cells(1).Text = objSPKDetail.VechileColor.ColorIndName
                Else
                    e.Item.Cells(1).Text = objSPKDetail.VechileColor.MaterialDescription.ToString
                End If
            End If

            If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                'If e.Item.ItemType = ListItemType.Footer Then
                '    ddlKategori = CType(e.Item.FindControl("ddlFooterKategori"), DropDownList)
                '    ddlTambahan = CType(e.Item.FindControl("ddlFooterTambahan"), DropDownList)
                'ElseIf e.Item.ItemType = ListItemType.EditItem Then
                '    ddlKategori = CType(e.Item.FindControl("ddlEditKategori"), DropDownList)
                '    ddlTambahan = CType(e.Item.FindControl("ddlEditTambahan"), DropDownList)
                'End If
                ddlKategori.SelectedValue = objSPKDetail.Category.ID
                If IsNothing(objSPKDetail.Additional) Then
                    ddlTambahan.SelectedIndex = 2 '-> Default List tambahan diubah menjadi 'Tidak Ada' 2017/12/27 by Miyuki
                Else
                    ddlTambahan.SelectedValue = CStr(objSPKDetail.Additional)
                End If

                Dim txtEEventTypeID As TextBox = CType(e.Item.FindControl("txtEEventTypeID"), TextBox)
                Dim ddlEBabitEventType As DropDownList = CType(e.Item.FindControl("ddlEBabitEventType"), DropDownList)
                Dim txtECampaignName As TextBox = CType(e.Item.FindControl("txtECampaignName"), TextBox)
                Dim lblEPopUpEvent As Label = CType(e.Item.FindControl("lblEPopUpEvent"), Label)
                If Not IsNothing(txtECampaignName) Then
                    txtECampaignName.Text = objSPKDetail.CampaignName
                    txtEEventTypeID.Text = objSPKDetail.EventType
                    If txtECampaignName.Text.Trim <> "" Then
                        'default
                        txtECampaignName.Visible = True
                        lblEPopUpEvent.Visible = False
                        ddlEBabitEventType.SelectedValue = "1"   '--- Lain - lain

                        Dim _oDealer As Dealer
                        If Not IsNothing(objSPKDetail.SPKHeader) Then
                            If Not IsNothing(objSPKDetail.SPKHeader.Dealer) Then
                                _oDealer = objSPKDetail.SPKHeader.Dealer
                            End If
                        Else
                            _oDealer = objUser.Dealer
                        End If
                        If IsNothing(_oDealer) Then _oDealer = New Dealer

                        Dim oBabitHeader As New BabitHeader
                        Dim dsBabitEvent As DataSet = New DataSet
                        dsBabitEvent = New BabitHeaderFacade(User).RetrieveFromSPByPopUp(_oDealer.ID, txtECampaignName.Text, "", Nothing, Nothing, False, "")
                        If dsBabitEvent.Tables.Count > 0 Then
                            If dsBabitEvent.Tables(0).Rows.Count > 0 Then
                                txtECampaignName.Visible = True
                                lblEPopUpEvent.Visible = True
                                ddlEBabitEventType.SelectedValue = "0"  '--- Babit & Event
                            End If
                        End If
                    Else
                        txtECampaignName.Visible = False
                        lblEPopUpEvent.Visible = False
                        ddlEBabitEventType.SelectedIndex = 0     '---Silahkan Pilih
                    End If
                End If

                '--------add start by rudi
                If objSPKHeader.Dealer.ID <> objDealer.ID Then
                    e.Item.Cells(13).Visible = False
                    e.Item.Cells(14).Visible = False
                    e.Item.Cells(15).Visible = False
                End If
                '--------
            ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim RowValue As SPKDetail = CType(e.Item.DataItem, SPKDetail)
                Dim lblTambahan As Label = CType(e.Item.FindControl("lblTambahan"), Label)
                Dim lbtnBatal As LinkButton = CType(e.Item.FindControl("lbtnBatal"), LinkButton)

                Dim msg As String = String.Empty
                If IsSPKMatching(RowValue, msg) Then
                    lbtnBatal.Attributes.Add("onclick", String.Format("javascript: return ShowConfirm('{0}');", msg))
                End If

                Dim EnumTambahan As EnumSPKAdditional.SPKAdditionalParts = objSPKDetail.Additional
                lblTambahan.Text = EnumTambahan.ToString
                If objSPKDetail.Status = 1 Then
                    ddlRejectedReason.Enabled = False
                    lbtnBatal.Visible = False
                Else
                    ddlRejectedReason.Enabled = True
                    lbtnBatal.Visible = True
                End If
                Dim imgConsumentFaktur As HtmlImage = e.Item.FindControl("imgConsumentFaktur")
                If Not IsNothing(imgConsumentFaktur) Then

                    If Not IsNothing(ViewState("Mode")) AndAlso CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.ViewMode Then
                        imgConsumentFaktur.Src = "../images/detail.gif"
                        imgConsumentFaktur.Alt = "Daftar Konsumen Faktur"
                    End If

                    If Not IsNothing(ViewState("Mode")) AndAlso CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.EditMode AndAlso objSPKDetail.SPKDetailCustomers.Count > 0 Then
                        imgConsumentFaktur.Src = "../images/edit.gif"
                        imgConsumentFaktur.Alt = "Daftar Konsumen Faktur"
                    End If


                    If Not IsNothing(ViewState("Mode")) AndAlso CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.EditMode AndAlso objSPKDetail.SPKDetailCustomers.Count = 0 Then
                        imgConsumentFaktur.Src = "../images/add.gif"
                        imgConsumentFaktur.Alt = "Tambah Konsumen Faktur"
                    End If

                End If
                If CBool(ViewState("SPKDMS")) Then
                    lbtnBatal.Visible = False
                    e.Item.Cells(14).Visible = False
                    e.Item.Cells(13).Visible = False
                    e.Item.Cells(15).Visible = False
                End If
                Dim lblCampaignName As Label = CType(e.Item.FindControl("lblCampaignName"), Label)
                If Not IsNothing(lblCampaignName) Then
                    lblCampaignName.Text = objSPKDetail.CampaignName
                End If
            End If

            'e.Item.Cells(7).Text = FormatNumber(CType(objSPKDetail.Amount, Long), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            e.Item.Cells(10).Text = FormatNumber(CType(objSPKDetail.TotalAmount, Long), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            'objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
            'If Not IsNothing(objSPKHeader.Dealer) Then
            '    e.Item.Cells(13).Visible = False '--edit,update,save
            '    e.Item.Cells(14).Visible = False '--delete
            '    e.Item.Cells(15).Visible = False '--batal
            'End If

            If objSPKHeader.Dealer.ID = objDealer.ID Then
                If Not ((objSPKHeader.Status = EnumStatusSPK.Status.Selesai) OrElse ((objSPKHeader.Status = EnumStatusSPK.Status.Batal))) Then
                    If objSPKDetail.Status = 1 Then
                        e.Item.Cells(13).Visible = False
                        e.Item.Cells(15).Visible = False
                    Else
                        e.Item.Cells(13).Visible = True
                        e.Item.Cells(15).Visible = SetButtonEditDetail(objSPKDetail.ID)
                    End If
                    e.Item.Cells(14).Visible = False 'SetButtonEditDetail(objSPKDetail.ID)
                End If

                If CBool(ViewState("SPKDMS")) Then

                    e.Item.Cells(14).Visible = False
                    e.Item.Cells(13).Visible = False
                    e.Item.Cells(15).Visible = False
                End If

            Else
                Dim lbtnAdd As LinkButton
                Dim lbtnBatal As LinkButton = CType(e.Item.FindControl("lbtnBatal"), LinkButton)
                If e.Item.ItemType = ListItemType.Footer Then
                    lbtnAdd = CType(e.Item.FindControl("lbtnAdd"), LinkButton)
                End If
                '--------add start by rudi
                If objSPKHeader.Dealer.ID <> objDealer.ID Then
                    e.Item.Cells(13).Visible = False
                    e.Item.Cells(14).Visible = False
                    e.Item.Cells(15).Visible = False
                    e.Item.Cells(16).Visible = False

                    If e.Item.ItemType = ListItemType.Footer Then
                        lbtnAdd.Visible = False
                    End If
                End If
                '--------
            End If

            If e.Item.ItemType = ListItemType.EditItem Then
                Dim iQtyD As Integer = 0
                If Not IsNothing(objSPKHeader.SPKFakturs) Then
                    For Each objSPKFaktur As SPKFaktur In objSPKHeader.SPKFakturs
                        If objSPKFaktur.RowStatus = CType(DBRowStatus.Active, Short) Then
                            If Not objSPKFaktur.EndCustomer.ChassisMaster Is Nothing Then
                                If objSPKFaktur.EndCustomer.ChassisMaster.VechileColor.VechileType.ID = objSPKDetail.VechileColor.VechileType.ID Then
                                    iQtyD = iQtyD + 1
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    If iQtyD > 0 Then
                        If e.Item.ItemType = ListItemType.EditItem Then
                            Dim txtEditKodeModel As TextBox = CType(e.Item.FindControl("txtEditKodeModel"), TextBox)
                            Dim lblEditKodeModel As Label = CType(e.Item.FindControl("lblEditKodeModel"), Label)
                            Dim txtEditKodeWarna As TextBox = CType(e.Item.FindControl("txtEditKodeWarna"), TextBox)
                            Dim lblEditKodeWarna As Label = CType(e.Item.FindControl("lblEditKodeWarna"), Label)
                            Dim txtEditKodeBody As TextBox = CType(e.Item.FindControl("txtEditKodeBody"), TextBox)
                            Dim lblEditKodeBody As Label = CType(e.Item.FindControl("lblEditKodeBody"), Label)
                            ddlKategori.Enabled = False
                            ddlTambahan.Enabled = False
                            txtEditKodeModel.Enabled = False
                            lblEditKodeModel.Visible = False
                            txtEditKodeWarna.Enabled = False
                            lblEditKodeWarna.Visible = False
                            txtEditKodeBody.Enabled = False
                            lblEditKodeBody.Visible = False
                        End If
                    End If
                End If
            End If

        End If

    End Sub

    Sub dtgPesananKendaraan_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        Select Case (e.CommandName)
            Case "Add"
                AddCommand(e)
            Case "Batal"
                BatalCommand(e)
            Case "Delete"
                Dim lShouldReturn As Boolean
                DeleteCommand(e, lShouldReturn)
                If lShouldReturn Then
                    Return
                End If

            Case "AddFaktur"
                AddFaktur(e)
        End Select
    End Sub

    Sub dtgPesananKendaraan_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        dtgPesananKendaraan.ShowFooter = False
        dtgPesananKendaraan.EditItemIndex = CInt(e.Item.ItemIndex)
        BindDetailToGrid()
        btnSimpan.Enabled = False
    End Sub

    Sub dtgPesananKendaraan_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If CekVechileModelExpander() Then
            dtgPesananKendaraan.ShowFooter = False
        Else
            dtgPesananKendaraan.ShowFooter = True
        End If
        dtgPesananKendaraan.EditItemIndex = -1
        BindDetailToGrid()
        btnSimpan.Enabled = True
    End Sub

    Sub dtgPesananKendaraan_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If
        UpdateCommand(E)
    End Sub

    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If
        If txtSalesmanCode.Text = String.Empty Then
            lblError.Text = "Error : Kode salesman tidak boleh kosong"
            Return
        End If
        SalesmenInfo()
        BindDataToObject()

        Dim ddl0 As DropDownList = e.Item.FindControl("ddlFooterKategori")
        Dim ddl1 As DropDownList = e.Item.FindControl("ddlFooterTambahan")
        Dim txt1 As TextBox = e.Item.FindControl("txtFooterKodeModel")
        Dim txt2 As TextBox = e.Item.FindControl("txtFooterKodeWarna")
        Dim txt3 As TextBox = e.Item.FindControl("txtFooterKodeBody")
        Dim txt4 As TextBox = e.Item.FindControl("txtFooterUnit")
        Dim txt5 As TextBox = e.Item.FindControl("txtFooterHarga")
        Dim txt6 As TextBox = e.Item.FindControl("txtFooterRemarks")
        Dim ddlRejectedReason As DropDownList = CType(e.Item.FindControl("ddlRejectedReason"), DropDownList)
        Dim lblKodeBody As Label = e.Item.FindControl("lblFooterKodeBody")
        Dim txtFCampaignName As TextBox = e.Item.FindControl("txtFCampaignName")
        Dim txtFEventTypeID As TextBox = e.Item.FindControl("txtFEventTypeID")
        Dim ddlFBabitEventType As DropDownList = CType(e.Item.FindControl("ddlFBabitEventType"), DropDownList)

        If ddlFBabitEventType.SelectedValue = "0" Then   'Tipe Babit & Event
            If txtFCampaignName.Text.Trim <> "" Then
                Dim arr As ArrayList = GetDataEvent(txtFCampaignName.Text.Trim)
                If arr.Count > 0 Then
                    Dim objBabithdr As BabitHeader = CType(arr(0), BabitHeader)
                    If Not IsNothing(objBabithdr) Then
                        txtFEventTypeID.Text = objBabithdr.EventTypeID
                    Else
                        MessageBox.Show("No Reg Campaign : " & txtFCampaignName.Text & " tidak valid")
                        txtFEventTypeID.Text = ""
                        txtFCampaignName.Text = ""
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("No Reg Campaign : " & txtFCampaignName.Text & " tidak valid")
                    txtFEventTypeID.Text = ""
                    txtFCampaignName.Text = ""
                    Exit Sub
                End If
            End If
        End If

        If ddl0.SelectedIndex = 1 Then
            txt3.Visible = False
            lblKodeBody.Visible = False
        End If
        If (ddl0.SelectedItem.Text <> "PC") And (txt3.Text = String.Empty) Then
            lblError.Text = "Kode body harap dipilih"
            Exit Sub
        End If
        If (ddl1.SelectedValue.ToString <> "2") And (txt6.Text = String.Empty) Then
            lblError.Text = "Kolom remarks harap diisi"
            Exit Sub
        End If

        If (ValidateItem(txt1.Text.ToUpper, txt2.Text.ToUpper, ddl0.SelectedItem.Text.ToString, txt4.Text, txt5.Text) And ValidateDuplication(txt1.Text.ToUpper, txt2.Text.ToUpper, "Add", -1)) Then
            objSPKHeader = sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader))
            objSPKDetail = New KTB.DNet.Domain.SPKDetail
            objSPKDetail.VehicleTypeCode = txt1.Text.ToUpper
            objSPKDetail.VehicleColorCode = txt2.Text.ToUpper
            objSPKDetail.VehicleColorName = New VechileColorFacade(User).Retrieve(objSPKDetail.VehicleColorCode.ToString).ColorEngName

            Dim criterias As New CriteriaComposite(New Criteria(GetType(ProfileDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ProfileDetail), "Code", MatchType.Exact, txt3.Text))
            Dim prfHeader As New ProfileHeader
            Select Case ddl0.SelectedItem.Text
                Case "PC"
                    prfHeader = Nothing
                Case "CV"
                    prfHeader = New ProfileHeaderFacade(User).Retrieve("CBU_BODYTYPE1")
                Case "LCV"
                    prfHeader = New ProfileHeaderFacade(User).Retrieve("CBU_BODYTYPELCV1")
            End Select
            If IsNothing(prfHeader) Then
                objSPKDetail.ProfileDetail = Nothing
            Else
                criterias.opAnd(New Criteria(GetType(ProfileDetail), "ProfileHeader.ID", MatchType.Exact, prfHeader.ID))
                objSPKDetail.ProfileDetail = CType(New ProfileDetailFacade(User).Retrieve(criterias)(0), ProfileDetail)
            End If

            objSPKDetail.Quantity = CType(txt4.Text, Integer)
            objSPKDetail.Amount = 0 'CType(txt5.Text, Decimal)
            objSPKDetail.TotalAmount = objSPKDetail.Quantity * objSPKDetail.Amount
            objSPKDetail.Additional = CInt(ddl1.SelectedValue)
            objSPKDetail.Remarks = txt6.Text.Trim
            objSPKDetail.Status = 0 ' Aktif

            If (ddl0.SelectedValue <> -1) Then
                objSPKDetail.Category = New CategoryFacade(User).Retrieve(CType(ddl0.SelectedValue, Integer))
            End If
            If (objSPKDetail.VehicleColorCode <> "ZZZZ") Then
                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias2.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.Exact, txt2.Text))
                criterias2.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, txt1.Text))
                objSPKDetail.VechileColor = New VechileColorFacade(User).Retrieve(criterias2)(0)
            Else
                Dim criterias1 As New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.Exact, "ZZZZ"))
                Dim objVehicleColor As VechileColor = New VechileColorFacade(User).Retrieve(criterias1)(0)
                objSPKDetail.VechileColor = New VechileColorFacade(User).Retrieve(objVehicleColor.ID)
            End If

            'start --indent system 20170703
            'dalam 1 SPK model baru tidak boleh digabung dengan model lain
            'If objSPKHeader.SPKDetails.Count >= 1 Then
            If Not NewTypeValidation(txt1.Text.ToUpper, txt2.Text.ToUpper) Then
                Exit Sub
            End If
            'End If

            'end --indent system 20170703

            objSPKDetail.EventType = IIf(txtFEventTypeID.Text.Trim = "", 0, txtFEventTypeID.Text.Trim)
            objSPKDetail.CampaignName = txtFCampaignName.Text

            objSPKHeader.SPKDetails.Add(objSPKDetail)
            sessionHelper.SetSession(Me.ViewState.Item(Me._vstSPKHeader), objSPKHeader)

        Else
            Exit Sub
        End If

        BindDataToPage()
        'SetButtonEditMode()

    End Sub


    Private Sub BatalCommand(ByVal e As DataGridCommandEventArgs)
        Dim ddlRejectedReason As DropDownList = CType(e.Item.FindControl("ddlRejectedReason"), DropDownList)
        Dim lblCampaignName As Label = CType(e.Item.FindControl("lblCampaignName"), Label)
        If ddlRejectedReason Is Nothing Then
            Return
        End If

        If ddlRejectedReason.SelectedValue = String.Empty Then
            lblError.Text = "Error : Isi alasan pembatalan"
            MessageBox.Show("Error : Isi alasan pembatalan")
            Return
        End If
        Dim cntStatusNoBatal As Integer = 0
        If objSPKHeader.SPKDetails.Count > 0 Then
            For Each spkDtl As SPKDetail In objSPKHeader.SPKDetails
                If spkDtl.Status <> 1 And spkDtl.Status <> 3 Then
                    cntStatusNoBatal = cntStatusNoBatal + 1
                End If
            Next
            If cntStatusNoBatal = 1 Then
                MessageBox.Show("Batal Pengajuan tidak dapat dilakukan, dokumen SPK minimal harus memiliki 1 data unit")
                Return
            End If
        End If

        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        objSPKDetail = objSPKHeader.SPKDetails(e.Item.ItemIndex)

        objSPKDetail.Status = 1 ' Aktif
        objSPKDetail.RejectedReason = ddlRejectedReason.SelectedValue
        objSPKDetail.CampaignName = lblCampaignName.Text

        sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader)
        dtgPesananKendaraan.EditItemIndex = -1
        BindDetailToGrid()
        Mode = ViewState("Mode")
        If (Mode = enumMode.Mode.EditMode) Then
            Dim _spkDetailFacade As New SPKDetailFacade(User)
            objSPKDetail = objSPKHeader.SPKDetails(e.Item.ItemIndex)
            objSPKDetail.SPKHeader = objSPKHeader
            _spkDetailFacade.Update(objSPKDetail)
        End If

    End Sub

    '-- start add by rudi
    Private Function CekVechileModelExpander()
        sessionHelper.SetSession(ViewState.Item(Me._sessTypeExpander), "")
        Dim arrListStatusChangeHistory As ArrayList
        Dim strSPKStatusTU As String = "(" +
                                        "'" + CInt(EnumStatusSPK.Status.Tunggu_Unit).ToString() + "')" '+
        '    ",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_I).ToString() + "'" +
        '    ",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_II).ToString() + "'" +
        '    ",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_III).ToString() + "'" +
        '    ",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_IV).ToString() + "'" +
        '    ",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_V).ToString() + "'" +
        '    ",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_VI).ToString() + "'" +
        '    ",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_VII).ToString() + "'" +
        '")"

        Dim criteriasSCH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasSCH.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, CInt(LookUp.DocumentType.Surat_Pesanan_Kendaraan)))
        criteriasSCH.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, lblNoSPK.Text.Trim()))
        criteriasSCH.opAnd(New Criteria(GetType(StatusChangeHistory), "OldStatus", MatchType.InSet, strSPKStatusTU))
        criteriasSCH.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.InSet, strSPKStatusTU))
        Dim objStatusList As ArrayList = New StatusChangeHistoryFacade(User).Retrieve(criteriasSCH)

        If (Not objStatusList Is Nothing) AndAlso objStatusList.Count > 0 Then
            objSPKDetail = objSPKHeader.SPKDetails(0)

            Dim strsql As String = ""
            strsql = "select ID from VechileModel "
            strsql += "where VechileModelCode = 'RN' "

            Dim criteriasVT3 As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasVT3.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
            criteriasVT3.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, objSPKDetail.VehicleTypeCode))
            criteriasVT3.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strsql & ")"))
            Dim objVechileType2 As ArrayList = New VechileTypeFacade(User).Retrieve(criteriasVT3)

            If Not IsNothing(objVechileType2) Then
                If objVechileType2.Count > 0 Then
                    sessionHelper.SetSession(ViewState.Item(Me._sessTypeExpander), "RN")
                    Return True
                    Exit Function
                End If
            End If
        End If

        Return False
    End Function
    '-- end add by rudi

    Private Sub UpdateCommand(ByVal E As DataGridCommandEventArgs)

        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        SalesmenInfo()

        Dim ddl0 As DropDownList = E.Item.FindControl("ddlEditKategori")
        Dim ddl1 As DropDownList = E.Item.FindControl("ddlEditTambahan")
        Dim txt1 As TextBox = E.Item.FindControl("txtEditKodeModel")
        Dim txt2 As TextBox = E.Item.FindControl("txtEditKodeWarna")
        Dim txt3 As TextBox = E.Item.FindControl("txtEditKodeBody")
        Dim txt4 As TextBox = E.Item.FindControl("txtEditUnit")
        Dim txt5 As TextBox = E.Item.FindControl("txtEditHarga")
        Dim txt6 As TextBox = E.Item.FindControl("txtEditRemarks")
        Dim ddlRejectedReason As DropDownList = CType(E.Item.FindControl("ddlRejectedReason"), DropDownList)
        Dim txtECampaignName As TextBox = E.Item.FindControl("txtECampaignName")
        Dim txtEEventTypeID As TextBox = E.Item.FindControl("txtEEventTypeID")
        Dim ddlEBabitEventType As DropDownList = CType(E.Item.FindControl("ddlEBabitEventType"), DropDownList)

        If (ddl0.SelectedItem.Text <> "PC") And (txt3.Text = String.Empty) Then
            lblError.Text = "Kode body harap dipilih"
            Exit Sub
        End If
        If (ddl1.SelectedValue.ToString <> "2") And (txt6.Text = String.Empty) Then
            lblError.Text = "Kolom remarks harap diisi"
            Exit Sub
        End If

        If ddlEBabitEventType.SelectedValue = "0" Then   'Tipe Babit & Event
            If txtECampaignName.Text.Trim <> "" Then
                Dim arr As ArrayList = GetDataEvent(txtECampaignName.Text.Trim)
                If arr.Count > 0 Then
                    Dim objBabithdr As BabitHeader = CType(arr(0), BabitHeader)
                    If Not IsNothing(objBabithdr) Then
                        txtEEventTypeID.Text = objBabithdr.EventTypeID
                    Else
                        MessageBox.Show("No Reg Campaign : " & txtECampaignName.Text & " tidak valid")
                        txtEEventTypeID.Text = ""
                        txtECampaignName.Text = ""
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("No Reg Campaign : " & txtECampaignName.Text & " tidak valid")
                    txtEEventTypeID.Text = ""
                    txtECampaignName.Text = ""
                    Exit Sub
                End If
            End If
        End If

        'Start Changes 2017/11/22---
        Dim arrListStatusChangeHistory As ArrayList
        Dim strStatusTungguUnit As String = "(" +
                                                    "'" + CInt(EnumStatusSPK.Status.Tunggu_Unit).ToString() + "')" '+
        '",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_I).ToString() + "'" +
        '",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_II).ToString() + "'" +
        '",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_III).ToString() + "'" +
        '",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_IV).ToString() + "'" +
        '",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_V).ToString() + "'" +
        '",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_VI).ToString() + "'" +
        '",'" + CInt(EnumStatusSPK.Status.Tunggu_Unit_VII).ToString() + "'" +
        '")"

        Dim criteriasSCH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasSCH.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, CInt(LookUp.DocumentType.Surat_Pesanan_Kendaraan)))
        criteriasSCH.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, lblNoSPK.Text.Trim()))
        criteriasSCH.opAnd(New Criteria(GetType(StatusChangeHistory), "OldStatus", MatchType.InSet, strStatusTungguUnit))
        criteriasSCH.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.InSet, strStatusTungguUnit))
        Dim objStatusList As ArrayList = New StatusChangeHistoryFacade(User).Retrieve(criteriasSCH)

        If (Not objStatusList Is Nothing) AndAlso objStatusList.Count > 0 Then
            objSPKDetail = objSPKHeader.SPKDetails(E.Item.ItemIndex)

            Dim criteriasVT2 As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasVT2.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
            criteriasVT2.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, objSPKDetail.VehicleTypeCode))
            Dim objVechileType As ArrayList = New VechileTypeFacade(User).Retrieve(criteriasVT2)

            If objVechileType.Count > 0 Then
                Dim criteriasVT As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasVT.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
                criteriasVT.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.Exact, objVechileType(0).VechileModel.ID))
                criteriasVT.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, txt1.Text.Trim().ToUpper))
                Dim ArrVehicleType As ArrayList = New VechileTypeFacade(User).Retrieve(criteriasVT)
                If Not IsNothing(ArrVehicleType) AndAlso (ArrVehicleType.Count = 0) Then
                    lblError.Text = "Kode Tipe tidak sama dengan Model sebelumnya"
                    Exit Sub
                End If
            End If

            '-- start add by rudi
            Dim strModelType As String = sessionHelper.GetSession(ViewState.Item(Me._sessTypeExpander))
            If strModelType = "RN" Then
                Dim hdnEditUnit As HiddenField = CType(E.Item.FindControl("hdnEditUnit"), HiddenField)
                If CInt(txt4.Text) > CInt(hdnEditUnit.Value) Then
                    MessageBox.Show("Maksimum qty " & hdnEditUnit.Value)
                    Exit Sub
                End If
            End If
            '-- end add by rudi
        End If
        'End Changes 2017/11/22---


        If (ValidateItem(txt1.Text.ToUpper, txt2.Text.ToUpper, ddl0.SelectedItem.Text.ToString, txt4.Text, txt5.Text) And ValidateDuplication(txt1.Text.ToUpper, txt2.Text.ToUpper, "Edit", E.Item.ItemIndex)) Then
            objSPKDetail = objSPKHeader.SPKDetails(E.Item.ItemIndex)
            objSPKDetail.VehicleTypeCode = txt1.Text.ToUpper
            objSPKDetail.VehicleColorCode = txt2.Text.ToUpper
            objSPKDetail.VehicleColorName = New VechileColorFacade(User).Retrieve(objSPKDetail.VehicleColorCode.ToString).ColorEngName

            Dim criterias As New CriteriaComposite(New Criteria(GetType(ProfileDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ProfileDetail), "Code", MatchType.Exact, txt3.Text))
            Dim prfHeader As New ProfileHeader
            Select Case ddl0.SelectedItem.Text
                Case "PC"
                    prfHeader = Nothing
                Case "CV"
                    prfHeader = New ProfileHeaderFacade(User).Retrieve("CBU_BODYTYPE1")
                Case "LCV"
                    prfHeader = New ProfileHeaderFacade(User).Retrieve("CBU_BODYTYPELCV1")
            End Select
            If IsNothing(prfHeader) Then
                objSPKDetail.ProfileDetail = Nothing
            Else
                criterias.opAnd(New Criteria(GetType(ProfileDetail), "ProfileHeader.ID", MatchType.Exact, prfHeader.ID))
                objSPKDetail.ProfileDetail = New ProfileDetailFacade(User).Retrieve(criterias)(0)
            End If

            'objSPKDetail.Quantity = CType(txt4.Text, Integer)
            objSPKDetail.Amount = 0 'CType(txt5.Text, Decimal)
            objSPKDetail.TotalAmount = objSPKDetail.Quantity * objSPKDetail.Amount
            objSPKDetail.Additional = CInt(ddl1.SelectedValue)
            objSPKDetail.Remarks = txt6.Text.Trim
            objSPKDetail.Status = 0 ' Aktif
            objSPKDetail.RejectedReason = ddlRejectedReason.SelectedValue

            If (ddl0.SelectedValue <> -1) Then
                objSPKDetail.Category = New CategoryFacade(User).Retrieve(CType(ddl0.SelectedValue, Integer))
            End If

            If (objSPKDetail.VehicleColorCode <> "ZZZZ") Then
                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias2.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.Exact, txt2.Text))
                criterias2.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, txt1.Text))
                objSPKDetail.VechileColor = New VechileColorFacade(User).Retrieve(criterias2)(0)
            Else
                Dim criterias1 As New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.Exact, "ZZZZ"))
                Dim objVehicleColor As VechileColor = New VechileColorFacade(User).Retrieve(criterias1)(0)
                objSPKDetail.VechileColor = New VechileColorFacade(User).Retrieve(objVehicleColor.ID)
            End If

            'start --indent system 20170703
            'If objSPKHeader.SPKDetails.Count >= 1 Then
            If Not NewTypeValidation(txt1.Text.ToUpper, txt2.Text.ToUpper) Then
                Exit Sub
            End If
            'End If
            Dim errEditQty As Boolean = False
            If objSPKHeader.SPKFakturs.Count > 0 Then
                Dim iQtyAssigned As Integer = 0
                For Each faktur As SPKFaktur In objSPKHeader.SPKFakturs
                    If Not IsNothing(faktur.EndCustomer.ChassisMaster) Then
                        If faktur.EndCustomer.ChassisMaster.VechileType <> "" AndAlso objSPKDetail.VehicleTypeCode = faktur.EndCustomer.ChassisMaster.VechileType Then
                            '---- add changes code by rudi 23/01/2018
                            If objSPKDetail.VechileColor.ID = faktur.EndCustomer.ChassisMaster.VechileColor.ID Then
                                iQtyAssigned = iQtyAssigned + 1
                            End If
                            '----- end
                        End If
                    End If
                Next
                If iQtyAssigned > 0 AndAlso CType(txt4.Text, Integer) < iQtyAssigned Then
                    MessageBox.Show("Jumlah quantity tidak boleh < dari jumlah yang sudah dibuat permohonan faktur. \n SPK untuk tipe ini sudah dibuat faktur sejumlah " & iQtyAssigned.ToString)
                    errEditQty = True
                End If
            End If

            If errEditQty = False Then
                objSPKDetail.CampaignName = txtECampaignName.Text
                objSPKDetail.EventType = IIf(txtEEventTypeID.Text.Trim = "", 0, txtEEventTypeID.Text.Trim)
                objSPKDetail.Quantity = CType(txt4.Text, Integer)
                sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader)
                dtgPesananKendaraan.EditItemIndex = -1
                BindDetailToGrid()
                Mode = ViewState("Mode")
                If (Mode = enumMode.Mode.EditMode) Then
                    Dim _spkDetailFacade As New SPKDetailFacade(User)
                    objSPKDetail = objSPKHeader.SPKDetails(E.Item.ItemIndex)
                    objSPKDetail.SPKHeader = objSPKHeader
                    _spkDetailFacade.Update(objSPKDetail)
                End If
                If CekVechileModelExpander() Then
                    dtgPesananKendaraan.ShowFooter = False
                Else
                    dtgPesananKendaraan.ShowFooter = True
                End If
                btnSimpan.Enabled = True
            Else
                BindDetailToGrid()
                dtgPesananKendaraan.ShowFooter = False
                dtgPesananKendaraan.EditItemIndex = CInt(E.Item.ItemIndex)
                btnSimpan.Enabled = False
            End If
            'end --indent system 20170703

        End If
    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs, ByRef shouldReturn As Boolean)
        shouldReturn = False

        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        objSPKDetail = objSPKHeader.SPKDetails(e.Item.ItemIndex)

        'start - update by anh 201707 indent system
        'If objSPKDetail.ID > 0 Then
        '    Dim _spkDetailFacade As SPKDetailFacade = New SPKDetailFacade(User)
        '    objSPKDetail = _spkDetailFacade.Retrieve(objSPKDetail.ID)
        '    If Not IsNothing(objSPKDetail) Then
        '        _spkDetailFacade.Delete(objSPKDetail)
        '    End If

        'End If
        ''objSPKHeader.SPKDetails.Remove(objSPKHeader.SPKDetails.Item(e.Item.ItemIndex))
        'objSPKHeader.SPKDetails.Remove(objSPKDetail)
        'sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader)
        'dtgPesananKendaraan.EditItemIndex = -1
        'BindDataToPage()

        'end - update by anh 201707 indent system


        'objSPKDetail.RowStatus = CType(DBRowStatus.Deleted, Short)

        Mode = ViewState("Mode")
        If (Mode = enumMode.Mode.EditMode) Then
            If (objSPKHeader.SPKDetails.Count <> 1) Then
                Dim _spkDetailFacade As New SPKDetailFacade(User)
                objSPKDetail = objSPKHeader.SPKDetails(e.Item.ItemIndex)
                objSPKDetail.SPKHeader = objSPKHeader
                _spkDetailFacade.Delete(objSPKDetail)
                objSPKHeader.SPKDetails.Remove(objSPKDetail)
            Else
                MessageBox.Show("SPK Header Harus memiliki minimal 1 SPK Detail")
                shouldReturn = True : Exit Sub
            End If
        End If

        sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader)
        BindDataToPage()

    End Sub

    Private Sub SetDtgPesananKendaraanItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblFooterKodeModel As Label = CType(e.Item.FindControl("lblFooterKodeModel"), Label)
        lblFooterKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection(this);"
        Dim lblFooterKodeWarna As Label = CType(e.Item.FindControl("lblFooterKodeWarna"), Label)
        lblFooterKodeWarna.Attributes("onclick") = "ShowPPKodeWarnaSelection(this);"
        Dim lblFooterKodeBody As Label = CType(e.Item.FindControl("lblFooterKodeBody"), Label)
        lblFooterKodeBody.Attributes("onclick") = "ShowPPKodeBodySelection(this);"

        Dim lblFPopUpEvent As Label = CType(e.Item.FindControl("lblFPopUpEvent"), Label)
        lblFPopUpEvent.Attributes("onclick") = "ShowPPGridEventDealerSelection(this, 0);"

        Dim txtFCampaignName As TextBox = CType(e.Item.FindControl("txtFCampaignName"), TextBox)
        Dim ddlFBabitEventType As DropDownList = CType(e.Item.FindControl("ddlFBabitEventType"), DropDownList)
        BindDdlBabitEventType(ddlFBabitEventType, txtFCampaignName, lblFPopUpEvent)

        txtFCampaignName.Visible = False
        lblFPopUpEvent.Visible = False
        ddlFBabitEventType.SelectedIndex = 0     '---Silahkan Pilih
    End Sub

    Private Sub SetDtgPesananKendaraanItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblEditKodeModel As Label = CType(e.Item.FindControl("lblEditKodeModel"), Label)
        lblEditKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection(this);"
        Dim lblEditKodeWarna As Label = CType(e.Item.FindControl("lblEditKodeWarna"), Label)
        lblEditKodeWarna.Attributes("onclick") = "ShowPPKodeWarnaSelection(this);"
        Dim lblEditKodeBody As Label = CType(e.Item.FindControl("lblEditKodeBody"), Label)
        lblEditKodeBody.Attributes("onclick") = "ShowPPKodeBodySelection(this);"

        Dim lblEPopUpEvent As Label = CType(e.Item.FindControl("lblEPopUpEvent"), Label)
        lblEPopUpEvent.Attributes("onclick") = "ShowPPGridEventDealerSelection(this, 1);"

        Dim txtECampaignName As TextBox = CType(e.Item.FindControl("txtECampaignName"), TextBox)
        Dim ddlEBabitEventType As DropDownList = CType(e.Item.FindControl("ddlEBabitEventType"), DropDownList)
        BindDdlBabitEventType(ddlEBabitEventType, txtECampaignName, lblEPopUpEvent)
    End Sub


    Private Sub btnProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProfile.Click
        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        Mode = ViewState("Mode")
        If Not IsNothing(objSPKHeader) Then
            If objSPKHeader.SPKDetails.Count > 0 Then
                'If Mode = enumMode.Mode.NewItemMode Then
                '    Response.Redirect("FrmSPKMasterProfile.aspx?Mode=" & CType(Mode, Integer) & "&spkHeader=" & viewstate.Item(Me._vstSPKHeader))
                'Else
                Response.Redirect("FrmSPKMasterProfile.aspx?Id=" & objSPKHeader.ID & "&Mode=" & CType(Mode, Integer) & "&spkHeader=" & ViewState.Item(Me._vstSPKHeader))
                'End If
            Else
                lblError.Text = "Tentukan kendaraan yang akan di pesan"
            End If
        Else
            lblError.Text = "Tentukan kendaraan yang akan di pesan -"
        End If
    End Sub

    Protected Sub btnDeleteFile_Click(sender As Object, e As ImageClickEventArgs) Handles btnDeleteFile.Click
        Try
            objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
            If Not IsNothing(objSPKHeader) Then
                objSPKHeader.EvidenceFile = String.Empty
                sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader)

                'Dim objFacade As SPKHeaderFacade = New SPKHeaderFacade(User)
                'objFacade.Update(objSPKHeader)

                BindHeaderToForm()
                btnSimpan.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("Gagal hapus dokumen SPK")
        End Try

    End Sub

#End Region

#Region "Custom method"

    Private Sub UserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Buat_spk_lihat_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Surat Pesanan Kendaraan")
        End If
        btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.Buat_spk_simpan_privilege)
        'btnKonsumen.Visible = SecurityProvider.Authorize(Context.User, SR.Buat_spk_simpan_privilege) 'konsumen_SPK_Simpan
    End Sub

    Private Sub SalesmenInfo()
        If txtSalesmanCode.Text.Trim <> String.Empty Then
            Dim sales As SalesmanHeader = New SalesmanHeaderFacade(User).RetrieveByCode(txtSalesmanCode.Text.ToString())
            If sales.ID > 0 Then
                lblNamaSalesman.Text = sales.Name
                If Not sales.SalesmanLevel Is Nothing Then
                    lblLevelSalesman.Text = sales.SalesmanLevel.Description
                Else
                    lblLevelSalesman.Text = String.Empty
                End If
                If Not sales.JobPosition Is Nothing Then
                    lblJabatan.Text = sales.JobPosition.Description
                Else
                    lblJabatan.Text = String.Empty
                End If
            Else
                lblNamaSalesman.Text = String.Empty
                lblLevelSalesman.Text = String.Empty
                lblJabatan.Text = String.Empty
            End If
        End If
    End Sub

    Private Sub RetrieveMaster()

        '--DropDownList Faktur Month
        Try
            ddlDeliveryMonth.Items.Clear()
            ddlDeliveryMonth.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayMonth()
                item.Selected = False
                ddlDeliveryMonth.Items.Add(item)
            Next
            ddlDeliveryMonth.ClearSelection()
            ddlDeliveryMonth.SelectedIndex = CType(Format(DateTime.Now, "MM").ToString, Integer)
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlFakturMonth, silahkan kirim error ini ke dnet admin")
        End Try

        '--DropDownList Faktur Year
        Try
            ddlDeliveryYear.Items.Clear()
            ddlDeliveryYear.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayYear(True, 2, 8, Date.Now.Year.ToString)
                item.Selected = False
                ddlDeliveryYear.Items.Add(item)
            Next
            ddlDeliveryYear.ClearSelection()
            ddlDeliveryYear.SelectedValue = Format(DateTime.Now, "yyyy").ToString
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlFakturYear, silahkan kirim error ini ke dnet admin")
        End Try

        '--DropDownList ddlInvoiceMonth
        Try
            ddlInvoiceMonth.Items.Clear()
            ddlInvoiceMonth.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayMonth()
                item.Selected = False
                ddlInvoiceMonth.Items.Add(item)
            Next
            ddlInvoiceMonth.ClearSelection()
            ddlInvoiceMonth.SelectedIndex = CType(Format(DateTime.Now, "MM").ToString, Integer)
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlFakturMonth, silahkan kirim error ini ke dnet admin")
        End Try

        '--DropDownList ddlInvoiceYear
        Try
            ddlInvoiceYear.Items.Clear()
            ddlInvoiceYear.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayYear(True, 2, 8, Date.Now.Year.ToString)
                item.Selected = False
                ddlInvoiceYear.Items.Add(item)
            Next
            ddlInvoiceYear.ClearSelection()
            ddlInvoiceYear.SelectedValue = Format(DateTime.Now, "yyyy").ToString
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlFakturYear, silahkan kirim error ini ke dnet admin")
        End Try



    End Sub

    Private Sub SetMode()
        Dim varIColl As Integer = dtgPesananKendaraan.Columns.Count - 1
        dtgPesananKendaraan.Columns(varIColl).Visible = False
        If Mode = enumMode.Mode.NewItemMode Then 'If SPKNumber = String.Empty Then
            SetButtonNewMode()
            If IsBackPage = False Then
                sessionHelper.RemoveSession(ViewState.Item(Me._vstSPKHeader))
                sessionHelper.RemoveSession("SPKCustomer")
            End If
        ElseIf Mode = enumMode.Mode.EditMode Then
            SearchSPKHeaderAndDetail()
            SetButtonEditMode()
            'dtgPesananKendaraan.Columns(varIColl).Visible = True
        ElseIf Mode = enumMode.Mode.ViewMode Then
            SearchSPKHeaderAndDetail()
            SetButtonViewMode()
            'dtgPesananKendaraan.Columns(varIColl).Visible = True
        End If
    End Sub

    Private Sub SetButtonNewMode()
        btnKonsumen.Enabled = True
        If IsBackPage Then
            Dim objSPKHeader As SPKHeader = sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader))
            If Not IsNothing(objSPKHeader) Then
                Dim objSPKCustomer As SPKCustomer = objSPKHeader.SPKCustomer
                If Not IsCustomerValid(objSPKCustomer) Then
                    btnSimpan.Enabled = False
                    btnProfile.Enabled = False
                Else
                    If objSPKHeader.SPKFakturs.Count > 0 Then
                        btnKonsumen.Enabled = False
                    Else
                        btnKonsumen.Enabled = True
                    End If
                    btnSimpan.Enabled = True
                    btnProfile.Enabled = False
                End If
            End If
        Else
            btnSimpan.Enabled = False
            btnProfile.Enabled = False
        End If
    End Sub

    Private Sub SetButtonEditMode()
        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        objDealer = sessionHelper.GetSession("DEALER")
        'If (objSPKHeader.Status = EnumStatusSPK.Status.Closed.ToString) OrElse (IsExpired()) OrElse ((Not (objSPKHeader.Dealer Is Nothing)) AndAlso (objSPKHeader.Dealer.ID <> objDealerSession.ID)) Then
        If Not IsNothing(objSPKHeader) Then
            'If (objSPKHeader.Status = EnumStatusSPK.Status.Closed.ToString) Then
            '    btnSimpan.Enabled = False
            '    btnKonsumen.Enabled = False
            '    btnProfile.Enabled = False
            'Else
            If objSPKHeader.ID <> 0 AndAlso objSPKHeader.Dealer.ID = objDealer.ID Then
                btnSimpan.Enabled = True
                btnProfile.Enabled = True
                'If objSPKHeader.SPKFakturs.Count > 0 Then
                '    btnKonsumen.Enabled = False
                'Else
                btnKonsumen.Enabled = True
                'End If
            Else
                btnSimpan.Enabled = False
                btnKonsumen.Enabled = False
                btnProfile.Enabled = False
            End If
            'End If
        End If
    End Sub

    Private Function SetButtonEditDetail(ByVal spkDetailID) As Boolean
        Dim vReturn As Boolean = True
        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        Try
            For Each detail As SPKDetail In objSPKHeader.SPKDetails
                If detail.ID = spkDetailID Then
                    If objSPKHeader.SPKFakturs.Count > 0 Then
                        For Each faktur As SPKFaktur In objSPKHeader.SPKFakturs
                            If detail.VechileColor.ID = faktur.EndCustomer.ChassisMaster.VechileColor.ID _
                                AndAlso detail.VehicleColorCode = faktur.EndCustomer.ChassisMaster.VechileColor.ColorCode Then
                                vReturn = False
                                Return vReturn
                            End If
                        Next
                    Else
                        vReturn = True
                    End If
                End If
            Next
            Return vReturn
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub SetButtonViewMode()
        ddlDeliveryMonth.Enabled = False
        ddlDeliveryYear.Enabled = False
        ddlInvoiceMonth.Enabled = False
        ddlInvoiceYear.Enabled = False
        txtNomorPesanan.Enabled = False
        txtSalesmanCode.Enabled = False
        ddlBabitEventType.Enabled = False
        txtCampaignName.Enabled = False
        lblPopUpEvent.Visible = False
        lblShowSalesman.Visible = False
        btnDeleteFile.Visible = False
        dtgPesananKendaraan.ShowFooter = False
        dtgPesananKendaraan.Enabled = False
        btnSimpan.Enabled = False
        'btnKonsumen.Enabled = False
        btnProfile.Enabled = False
    End Sub

    Private Function IsCustomerValid(ByVal objSpkCustomer As SPKCustomer) As Boolean
        Dim _return As Boolean = True
        Try
            If objSpkCustomer.Name1 = String.Empty Then
                _return = False
            End If
        Catch ex As Exception
            _return = False
        End Try

        Return _return
    End Function

    Private Function IsExpired() As Boolean
        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        If objSPKHeader.ID <> 0 Then
            'Dim PKDate As New DateTime(CInt(objSPKHeader.RequestPeriodeYear), CInt(objSPKHeader.RequestPeriodeMonth), 1, 0, 0, 0)
            'Dim DateNow As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0)
            'If (PKDate < DateNow) Then
            '    Return True
            'Else
            '    Return False
            'End If
            Return True
        Else
            Return False
        End If
    End Function

    Private Function CopyFileToServer(ByVal DataFile As HtmlInputFile, ByVal _objSPKHeader As SPKHeader) As Boolean
        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) And (DataFile.PostedFile.ContentLength < 3000000) Then
            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName) '-- Source file name
            Dim objFileName As String 'Saved filename on repository
            If objSPKHeader.SPKNumber = "" Then
                objFileName = _objSPKHeader.Dealer.DealerCode & "_" & _objSPKHeader.ID & "_" & SrcFile.Substring(SrcFile.Length - 4)
            Else
                objFileName = _objSPKHeader.Dealer.DealerCode & "_" & _objSPKHeader.SPKNumber & "_" & SrcFile.Substring(SrcFile.Length - 4)
            End If
            'Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SPKFileDirectory") & "\" & objFileName
            Dim strFolderName As String = Date.Now.Year.ToString
            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SPKFileDirectory") & "\" & strFolderName & "\" & SrcFile
            Dim DestFullFilePath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & DestFile

            Dim fileInfoDestination As New FileInfo(DestFullFilePath)
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim success As Boolean = False
            Dim helper As FileHelper = New FileHelper
            Try
                success = imp.Start()
                If success Then
                    If Not fileInfoDestination.Exists Then
                        fileInfoDestination.Directory.Create()
                    End If
                    DataFile.PostedFile.SaveAs(DestFullFilePath)
                    imp.StopImpersonate()
                    imp = Nothing
                    UpdateFileToDatabase(DestFile, _objSPKHeader)
                    Return True
                End If
            Catch ex As Exception
                MessageBox.Show("Error simpan file SPK. Harap hubungi administrator.")
            End Try
        End If
        Return False
    End Function

    Private Sub UpdateFileToDatabase(ByVal DestFile As String, ByVal _objSPKHeader As SPKHeader)
        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
        _objSPKHeader.EvidenceFile = DestFile
        Dim ireturn As Integer = objSPKHeaderFacade.UpdateTransaction(_objSPKHeader)
    End Sub

    Private Sub BindDataToObject()
        If Not IsNothing(sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))) Then
            If Not IsNothing(objDealer) Then
                objSPKHeader.Dealer = sessionHelper.GetSession("DEALER")
            Else
                objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
            End If
            If objSPKHeader.Status = String.Empty Then
                objSPKHeader.Status = EnumStatusSPK.Status.Awal
            End If
            objSPKHeader.SPKNumber = lblNoSPK.Text
            objSPKHeader.DealerSPKNumber = txtNomorPesanan.Text
            objSPKHeader.SPKReferenceNumber = txtSPKReferenceNumber.Text
            If txtSalesmanCode.Text.Trim <> String.Empty Then
                objSPKHeader.SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanCode.Text.ToString())
                objSPKHeader.DealerBranch = objSPKHeader.SalesmanHeader.DealerBranch
            Else
                objSPKHeader.SalesmanHeader = Nothing
            End If

            objSPKHeader.CampaignName = txtCampaignName.Text
            objSPKHeader.EventType = IIf(txtEventTypeID.Text.Trim = "", 0, txtEventTypeID.Text.Trim)

            'objSPKHeader.Category = New CategoryFacade(User).Retrieve(ddlKategori.SelectedItem.ToString())

            Dim deliveryDate As Date = DateSerial(ddlDeliveryYear.SelectedValue, ddlDeliveryMonth.SelectedValue, 1)
            objSPKHeader.PlanDeliveryMonth = deliveryDate.Month
            objSPKHeader.PlanDeliveryYear = deliveryDate.Year
            objSPKHeader.PlanDeliveryDate = deliveryDate

            Dim invoiceDate As Date = DateSerial(ddlInvoiceYear.SelectedValue, ddlInvoiceMonth.SelectedValue, 1)
            objSPKHeader.PlanInvoiceMonth = invoiceDate.Month
            objSPKHeader.PlanInvoiceYear = invoiceDate.Year
            objSPKHeader.PlanInvoiceDate = invoiceDate
            objSPKHeader.DealerSPKDate = icDealerSPKDate.Value
            objSPKHeader.CreatedTime = Date.Now
        End If
    End Sub

    Private Sub SearchSPKHeaderAndDetail()
        If Not Request.QueryString("Id") Is Nothing Then
            Dim spkHeaderID As Integer = CType(Request.QueryString("Id"), Integer)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKHeader), "ID", MatchType.Exact, CType(Request.QueryString("Id"), Integer)))
            Dim objSPKHeader1 As SPKHeader = New SPKHeaderFacade(User).Retrieve(criterias)(0)
            sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader1)
        Else
            sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), Nothing)
        End If
    End Sub

    Private Function ValidateSave() As Boolean
        Dim _return As Boolean = True

        Dim deliveryDate As DateTime = DateSerial(ddlDeliveryYear.SelectedValue, ddlDeliveryMonth.SelectedValue, 1)
        Dim invoiceDate As DateTime = DateSerial(ddlInvoiceYear.SelectedValue, ddlInvoiceMonth.SelectedValue, 1)
        If invoiceDate < deliveryDate Then
            lblError.Text = "Tanggal faktur tidak boleh lebih kecil dari tanggal pengiriman"
            _return = False
        End If
        If txtSalesmanCode.Text.Trim = String.Empty Then
            lblError.Text = "Kode salesman tidak boleh kosong"
            _return = False
        End If

        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        BindDataToObject()
        Try
            If objSPKHeader.SPKCustomer Is Nothing Then
                MessageBox.Show("Data Konsumen belum disimpan !")
                _return = False
            End If
        Catch ex As Exception
            MessageBox.Show("Data Konsumen belum disimpan !")
            _return = False
        End Try

        Try
            If Not String.IsNullorEmpty(objSPKHeader.SPKReferenceNumber.Trim()) Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, objSPKHeader.SPKReferenceNumber))
                Dim spkList As ArrayList = New SPKHeaderFacade(User).Retrieve(criterias)
                If spkList.Count = 0 Then
                    MessageBox.Show("Data Nomor SPK Referensi tidak ditemukan !")
                    _return = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Nomor SPK Reference tidak valid !")
            _return = False
        End Try

        Return _return
    End Function

    Function GetDataEvent(EventRegNumber As String) As ArrayList
        Dim _oDealer As Dealer
        If IsNothing(objSPKHeader) OrElse objSPKHeader.ID = 0 Then
            objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
            If Not IsNothing(objSPKHeader) Then
                _oDealer = objSPKHeader.Dealer
            Else
                _oDealer = objDealer
            End If
        Else
            _oDealer = objSPKHeader.Dealer
        End If
        If IsNothing(_oDealer) Then _oDealer = New Dealer

        Dim dsBabitEvent As DataSet = New DataSet
        dsBabitEvent = New BabitHeaderFacade(User).RetrieveFromSPByPopUp(_oDealer.ID, EventRegNumber, "", Nothing, Nothing, False, "")

        Dim _babitHeader As New BabitHeader
        Dim arrBabitEvent As New ArrayList
        Dim row As DataRow
        Dim i As Integer = 0
        For i = 0 To dsBabitEvent.Tables(0).Rows.Count - 1
            row = dsBabitEvent.Tables(0).Rows(i)
            Try
                _babitHeader = New BabitHeader
                _babitHeader.ID = row("ID")
                _babitHeader.BabitRegNumber = row("BabitRegNumber")
                _babitHeader.BabitMasterEventType = New BabitMasterEventTypeFacade(User).Retrieve(CInt(row("BabitMasterEventTypeID")))
                _babitHeader.BabitDealerNumber = row("BabitDealerNumber")
                _babitHeader.PeriodStart = row("PeriodStart")
                _babitHeader.PeriodEnd = row("PeriodEnd")
                _babitHeader.EventTypeID = row("EventTypeID")
                arrBabitEvent.Add(_babitHeader)

            Catch ex As Exception
            End Try
        Next

        Return arrBabitEvent

    End Function

    Private Sub BindDataToPage()
        If IsNothing(sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))) Then
            ClearAllFields()
            objSPKHeader = New KTB.DNet.Domain.SPKHeader
            If Not IsNothing(Request.QueryString("CustId")) Then
                Dim objSAPCustomer As SAPCustomer
                Dim objSAPCustomerFacade As SAPCustomerFacade = New SAPCustomerFacade(User)
                objSAPCustomer = objSAPCustomerFacade.Retrieve(CInt(Request.QueryString("CustId")))
                If Not IsNothing(objSAPCustomer) Then
                    If Not IsNothing(objSAPCustomer.SalesmanHeader) Then
                        txtSalesmanCode.Text = objSAPCustomer.SalesmanHeader.SalesmanCode
                        lblNamaSalesman.Text = objSAPCustomer.SalesmanHeader.Name
                        lblLevelSalesman.Text = objSAPCustomer.SalesmanHeader.SalesmanLevel.Description
                        lblJabatan.Text = objSAPCustomer.SalesmanHeader.JobPosition.Description
                    End If
                    txtCampaignName.Text = objSAPCustomer.CampaignName
                    txtCampaignName_TextChanged(Nothing, Nothing)
                End If
            End If
            sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader)
        Else
            objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
            BindHeaderToForm()
        End If
        BindDetailToGrid()
    End Sub

    Private Sub BindDetailToGrid()
        dtgPesananKendaraan.DataSource = objSPKHeader.SPKDetails
        dtgPesananKendaraan.DataBind()
        Dim strCalculation As String = CalculateUnitAndAmount(objSPKHeader)
        Dim arr() As String = strCalculation.Split(";")
        Dim unit As Integer = CType(arr(0).Trim, Integer)
        Dim amount As Double = CType(arr(1).Trim, Double)
        lblTotalUnit.Text = FormatNumber(unit, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " Unit"
        lblTotalHarga.Text = "Rp " & FormatNumber(amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)


    End Sub

    Private Function CalculateUnitAndAmount(ByVal obj As SPKHeader) As String
        Dim unit As Integer = 0
        Dim amount As Decimal = 0
        For Each item As SPKDetail In obj.SPKDetails
            If Not (item.Status = 1) Then ' 0 -> status aktif
                unit += item.Quantity
                amount += item.TotalAmount
            End If
        Next
        Return unit.ToString + ";" + amount.ToString
    End Function

    Private Sub ClearAllFields()
        'objDealer = sessionHelper.GetSession("DEALER")
        'objUser = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not IsNothing(objDealer) Then
            lblDealer.Text = objDealer.DealerCode & " / " & objDealer.SearchTerm1
        End If
        If Not IsNothing(objUser) Then
            lblDibuatOleh.Text = objUser.UserName
        End If
        lblNoSPK.Text = String.Empty
        lblStatus.Text = EnumStatusSPK.Status.Awal.ToString
        'ddlKategori.SelectedIndex = 0
        lblNamaSalesman.Text = String.Empty
        lblLevelSalesman.Text = String.Empty
        lblJabatan.Text = String.Empty
        lblSPKOpenDate.Text = Date.Now.ToString("dd/MM/yyyy")

        'start indent system by anh 201707
        lblEvidenceFile.Visible = False
        btnDeleteFile.Visible = False
        DataFile.Visible = True
        'end indent system by anh 201707

        txtEventTypeID.Text = ""
        If ddlBabitEventType.Items.Count > 0 Then
            ddlBabitEventType.SelectedIndex = 0
        End If
        txtCampaignName.Text = ""
    End Sub

    Private Sub BindHeaderToForm()
        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        'objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        'objUser = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not IsNothing(sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))) Then
            lblDealer.Text = objSPKHeader.Dealer.DealerCode & " / " & objSPKHeader.Dealer.SearchTerm1
            lblNoSPK.Text = objSPKHeader.SPKNumber
            If objSPKHeader.Status <> String.Empty Then
                lblStatus.Text = CType(objSPKHeader.Status, EnumStatusSPK.Status).ToString
            Else
                lblStatus.Text = EnumStatusSPK.Status.Awal.ToString
            End If
            icDealerSPKDate.Value = objSPKHeader.DealerSPKDate
            ddlInvoiceMonth.SelectedValue = objSPKHeader.PlanInvoiceMonth
            ddlInvoiceYear.SelectedValue = objSPKHeader.PlanInvoiceYear
            ddlDeliveryMonth.SelectedValue = objSPKHeader.PlanDeliveryMonth
            ddlDeliveryYear.SelectedValue = objSPKHeader.PlanDeliveryYear
            txtNomorPesanan.Text = objSPKHeader.DealerSPKNumber
            txtSPKReferenceNumber.Text = objSPKHeader.SPKReferenceNumber
            txtSalesmanCode.Text = objSPKHeader.SalesmanHeader.SalesmanCode
            lblNamaSalesman.Text = objSPKHeader.SalesmanHeader.Name
            lblLevelSalesman.Text = objSPKHeader.SalesmanHeader.SalesmanLevel.Description
            lblJabatan.Text = objSPKHeader.SalesmanHeader.JobPosition.Description
            lblSPKOpenDate.Text = IIf(objSPKHeader.CreatedTime < New Date(1900, 1, 1), String.Empty, objSPKHeader.CreatedTime.ToString("dd/MM/yyyy"))
            If objSPKHeader.CampaignName <> "" Then
                txtCampaignName.Text = objSPKHeader.CampaignName
            Else
                If Not IsNothing(objSPKHeader.SPKCustomer) Then
                    If Not IsNothing(objSPKHeader.SPKCustomer.SAPCustomer) Then
                        txtCampaignName.Text = objSPKHeader.SPKCustomer.SAPCustomer.CampaignName
                    End If
                End If
            End If
            txtCampaignName_TextChanged(Nothing, Nothing)

            'If Not IsNothing(objUser) Then
            '    lblDibuatOleh.Text = objUser.UserName
            'End If
            If CInt(IIf(objSPKHeader.Status = String.Empty, "0", objSPKHeader.Status)) = EnumStatusSPK.Status.Awal Then
                If (CInt(objDealer.Title) = EnumDealerTittle.DealerTittle.KTB) Then
                    lblShowSalesman.Visible = False
                Else
                    lblShowSalesman.Visible = True
                End If
            Else
                lblShowSalesman.Visible = False
            End If

            'Start Indent system by anh 201707
            If objSPKHeader.EvidenceFile.Trim <> "" Then
                Dim evFile() As String = objSPKHeader.EvidenceFile.Trim.Split("\")
                lblEvidenceFile.Visible = True
                'lblEvidenceFile.Text = objSPKHeader.EvidenceFile.Trim
                lblEvidenceFile.Text = evFile(evFile.Length - 1)
                btnDeleteFile.Visible = True
                DataFile.Visible = False
            Else
                lblEvidenceFile.Text = ""
                lblEvidenceFile.Visible = False
                btnDeleteFile.Visible = False
                DataFile.Visible = True
            End If

            'End Indent system by anh 201707

        Else
            ClearAllFields()
        End If

    End Sub

    Private Function ValidateItem(ByVal kodeModel As String, ByVal kodeWarna As String, ByVal typeBody As String, ByVal Unit As String, ByVal harga As String) As Boolean
        Dim iReturn As Boolean = True
        If (kodeWarna = String.Empty Or kodeModel = String.Empty Or Unit = String.Empty) Then 'Or harga = String.Empty
            lblError.Text = "Error : Kode Tipe, Kode Warna dan Unit (Permintaan Dealer) tidak boleh kosong"
            If txtSalesmanCode.Text = String.Empty Then
                lblError.Text = "Error : Kode salesman tidak boleh kosong"
            End If
            iReturn = False
        Else
            'If CDec(harga.Trim) < 49999999 Then
            '    lblError.Text = "Error : Harga yang dimasukkan salah"
            '    iReturn = False
            'End If
            Dim criterias1 As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias1.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
            criterias1.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, kodeModel))
            Dim ArrVehicleType As ArrayList = New VechileTypeFacade(User).Retrieve(criterias1)
            If (ArrVehicleType.Count = 0 OrElse (Not IsNothing(ArrVehicleType) AndAlso ArrVehicleType.Count > 0 AndAlso CType(ArrVehicleType(0), VechileType).Category.CategoryCode.ToUpper() <> typeBody.ToUpper())) Then
                lblError.Text = "Error : Kode Tipe dan kategori tidak cocok"
                iReturn = False
            Else
                If (kodeWarna <> "ZZZZ" And kodeWarna <> "zzzz") Then
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.Exact, kodeWarna.ToString))
                    criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, kodeModel.ToString))
                    criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.Status", MatchType.No, "X"))
                    criterias.opAnd(New Criteria(GetType(VechileColor), "Status", MatchType.No, "x"))
                    criterias.opAnd(New Criteria(GetType(VechileColor), "SpecialFlag", MatchType.No, "x"))
                    Dim ArrListVechileColor As ArrayList = New VechileColorFacade(User).Retrieve(criterias)
                    If (ArrListVechileColor Is Nothing) OrElse (ArrListVechileColor.Count = 0) Then
                        lblError.Text = "Error : Kode Warna dan Kode Tipe tidak cocok"
                        iReturn = False
                    End If
                End If
            End If
        End If
        Return iReturn
    End Function

    Private Function ValidateDuplication(ByVal kodeModel As String, ByVal kodeWarna As String, ByVal Mode As String, ByVal Rowindex As Integer) As Boolean
        objSPKHeader = sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader))
        If Not objSPKHeader Is Nothing Then
            If (Mode = "Add") Then
                For Each item As SPKDetail In objSPKHeader.SPKDetails
                    If kodeWarna <> "ZZZZ" Then
                        If (item.VehicleTypeCode.ToString = kodeModel And item.VechileColor.ColorCode.ToString = kodeWarna) Then
                            lblError.Text = "Error : Duplikasi KodeTipe dan KodeWarna"
                            Return False
                        End If
                    End If
                Next
            Else
                Dim i As Integer = 0
                For Each item As SPKDetail In objSPKHeader.SPKDetails
                    If kodeWarna <> "ZZZZ" Then
                        If (item.VehicleTypeCode.ToString = kodeModel And item.VechileColor.ColorCode.ToString = kodeWarna) Then
                            If i <> Rowindex Then
                                lblError.Text = "Error : Duplikasi KodeTipe dan KodeWarna"
                                Return False
                            End If
                        End If
                    End If
                    i = i + 1
                Next
            End If
        End If

        Return True
    End Function

    Private Function IsSPKMatching(ByVal sPKDet As SPKDetail, ByRef msg As String) As Boolean
        Dim oExArgs As New System.Collections.ArrayList
        Dim obSPKChassisFacade As New SPKChassisFacade(User)
        Dim criterias As CriteriaComposite

        criterias = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CShort(DBRowStatus.Active)))
        criterias.opAnd(New Criteria(GetType(SPKChassis), "SPKDetail.ID", MatchType.Exact, sPKDet.ID))
        criterias.opAnd(New Criteria(GetType(SPKChassis), "MatchingType", MatchType.InSet, "(1,3)"))
        oExArgs = obSPKChassisFacade.Retrieve(criterias, "ID", Sort.SortDirection.DESC)
        If oExArgs.Count > 0 Then
            Dim _spk As SPKChassis = DirectCast(oExArgs(0), SPKChassis)
            msg = String.Format("Nomor SPK : {0} dengan Kode Tipe : {1}, Kode Warna : {2} sudah proses matching. Apakah anda yakin ingin membatalkan ?", _
                                sPKDet.SPKHeader.SPKNumber, sPKDet.VehicleTypeCode, sPKDet.VehicleColorCode)
        End If

        Return oExArgs.Count > 0
    End Function
    'Private Function ValidationNewModel(ByVal modelCode As String) As Boolean
    '    If IsNothing(objSPKHeader) Then
    '        objSPKHeader = sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader))
    '    End If

    '    Dim vReturn As Boolean = True
    '    Dim bModelNew As Boolean = IsNewModel(modelCode)
    '    Dim iModelNew As Integer = 0
    '    Dim iModelElse As Integer = 0
    '    For Each item As SPKDetail In objSPKHeader.SPKDetails
    '        If IsNewModel(item.VechileColor.VechileType.VechileModel.VechileModelCode.ToUpper) Then
    '            iModelNew = iModelNew + 1
    '        Else
    '            iModelElse = iModelElse + 1
    '        End If
    '    Next
    '    If (bModelNew And iModelElse > 0) Then
    '        vReturn = False
    '    End If
    '    If (Not bModelNew And iModelNew > 0) Then
    '        vReturn = False
    '    End If
    '    Return vReturn

    'End Function

    Private _arrNewType As ArrayList
    Private _arrNewModel As ArrayList

    Private Function GetNewType() As ArrayList
        If IsNothing(_arrNewType) Then
            Dim objfacade As AppConfigFacade = New AppConfigFacade(User)
            Dim objappconfig As AppConfig = objfacade.Retrieve("SPKModelCodeFilter")
            If Not IsNothing(objappconfig) Then
                Dim modeCode() As String = objappconfig.Value.Trim.Split(";")
                Dim strCode As String = ""
                For iCode As Integer = 0 To modeCode.Length - 1
                    If strCode = "" Then
                        strCode = "'" & modeCode(iCode) & "'"
                    Else
                        strCode = strCode & ",'" & modeCode(iCode).ToString & "'"
                    End If
                Next
                strCode = "(" & strCode & ")"

                Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.VechileModelCode", MatchType.InSet, strCode))
                _arrNewType = New VechileTypeFacade(User).Retrieve(criterias)

                Return _arrNewType
            End If
        End If
    End Function

    Private Function GetNewModelCode() As ArrayList
        If IsNothing(_arrNewModel) Then
            Dim objfacade As AppConfigFacade = New AppConfigFacade(User)
            Dim objappconfig As AppConfig = objfacade.Retrieve("SPKModelCodeFilter")
            If Not IsNothing(objappconfig) Then
                _arrNewModel = New ArrayList
                Dim modeCode() As String = objappconfig.Value.Trim.Split(";")
                For iCode As Integer = 0 To modeCode.Length - 1
                    _arrNewModel.Add(modeCode(iCode))
                Next

                Return _arrNewModel
            End If
        End If
    End Function

    Private Function IsNewModel(ByVal modelCode As String) As Boolean
        If IsNothing(_arrNewModel) Then
            _arrNewModel = GetNewModelCode()
        End If
        Dim vReturn As Boolean = False
        For Each newModel As String In _arrNewModel
            If modelCode.ToUpper = newModel.ToUpper Then
                vReturn = True
                Exit For
            End If
        Next
        Return vReturn
    End Function

    Private Function NewTypeValidation(ByVal tipe As String, ByVal warna As String) As Boolean
        Dim vReturn As Boolean = True
        Try
            Dim objfacade As AppConfigFacade = New AppConfigFacade(User)
            Dim objappconfig As AppConfig = objfacade.Retrieve("SPKValidation")
            If Not IsNothing(objappconfig) AndAlso (objappconfig.ID > 0 AndAlso objappconfig.Value = "1") Then
                If IsNothing(_arrNewType) Then
                    _arrNewType = GetNewType()
                End If
                Dim isNewType As Boolean = False
                If _arrNewType.Count > 0 Then
                    For Each newType As VechileType In _arrNewType
                        If newType.VechileTypeCode = tipe Then
                            isNewType = True
                            Exit For
                        End If
                    Next
                End If
                If isNewType Then
                    If Not TypeValidation(tipe, warna, isNewType) Then
                        vReturn = False
                    End If

                    If Not LastMonthTransaction() Then
                        vReturn = False
                    End If
                Else
                    If Not TypeValidation(tipe, warna, isNewType) Then
                        vReturn = False
                    End If
                End If
            End If
        Catch ex As Exception
            'MessageBox.Show("Error validasi tipe kendaraan." & ex.Message & " - " & ex.InnerException.ToString)
        End Try
        Return vReturn
    End Function

    Private Function TypeValidation(ByVal kodeTipe As String, ByVal kodeWarna As String, ByVal isNewType As Boolean) As Boolean
        Dim vReturn As Boolean = True
        Try
            objSPKHeader = sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader))
            If Not objSPKHeader Is Nothing Then
                Dim iModelNew As Integer = 0
                Dim iModelElse As Integer = 0

                For Each item As SPKDetail In objSPKHeader.SPKDetails
                    If Not IsNothing(item.VechileColor.VechileType) Then
                        If IsNewModel(item.VechileColor.VechileType.VechileModel.VechileModelCode.ToUpper) Then
                            iModelNew = iModelNew + 1
                        Else
                            iModelElse = iModelElse + 1
                        End If
                    Else
                        Dim criterias1 As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias1.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, item.VehicleTypeCode))
                        Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(criterias1)(0)
                        If Not IsNothing(objVechileType) Then
                            If IsNewModel(objVechileType.VechileModel.VechileModelCode.ToUpper) Then
                                iModelNew = iModelNew + 1
                            Else
                                iModelElse = iModelElse + 1
                            End If
                        Else
                            If _arrNewType.Count > 0 Then
                                For Each newType As VechileType In _arrNewType
                                    If IsNewModel(newType.VechileModel.VechileModelCode.ToUpper) Then
                                        iModelNew = iModelNew + 1
                                    Else
                                        iModelElse = iModelElse + 1
                                    End If
                                Next
                            End If
                        End If
                    End If
                Next

                'If objSPKHeader.SPKDetails.Count > 0 _
                '            AndAlso ((iModelNew > 0 And (iModelNew < objSPKHeader.SPKDetails.Count)) _
                '            Or ((iModelElse > 0) And (iModelElse < objSPKHeader.SPKDetails.Count))) Then

                'End If

                If ((isNewType = True) And (iModelElse > 0)) _
                    OrElse ((isNewType = False) And (iModelNew > 0)) Then

                    MessageBox.Show("Error : SPK untuk tipe Xpander tidak boleh digabung dengan tipe lain.")
                    vReturn = False
                End If
            End If
        Catch ex As Exception
            'MessageBox.Show("Error validasi tipe kendaraan")
        End Try
        Return vReturn
    End Function

    Private Function FileValidation() As Boolean
        Dim vReturn As Boolean = True
        objSPKHeader = sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader))
        If Not objSPKHeader Is Nothing Then
            If IsNothing(_arrNewType) Then
                _arrNewType = GetNewType()
            End If
            Dim _isNewType As Boolean = False
            If _arrNewType.Count > 0 Then
                For Each item As SPKDetail In objSPKHeader.SPKDetails
                    For Each _newType As VechileType In _arrNewType
                        If (item.VehicleTypeCode.ToString.ToUpper = _newType.VechileTypeCode.Trim.ToUpper) Then
                            'If objSPKHeader.EvidenceFile.Trim <> "" Then
                            _isNewType = True
                            'End If
                        End If
                    Next
                Next
                If _isNewType Then
                    If DataFile.Visible = True AndAlso (DataFile.PostedFile.FileName = String.Empty) Then
                        MessageBox.Show("Lampiran Bukti SPK harus diisi")
                        Return False
                    Else
                        vReturn = FileSizeChecking()
                    End If
                End If
            End If
        End If
        Return vReturn

    End Function

    Private Function FileSizeChecking()
        Dim vReturn As Boolean = True
        Dim objfacade As AppConfigFacade = New AppConfigFacade(User)
        Dim maxFileConfig As AppConfig = objfacade.Retrieve("SPKFileSize")
        If Not IsNothing(maxFileConfig) Then
            If DataFile.Visible = True AndAlso (DataFile.PostedFile.FileName <> String.Empty) Then
                If DataFile.PostedFile.ContentLength > CType(maxFileConfig.Value, Integer) Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi 1 MB")
                    vReturn = False
                End If
                If (Not DataFile.PostedFile.FileName.ToLower().EndsWith(".jpg") And _
    Not DataFile.PostedFile.FileName.ToLower().EndsWith(".jpeg") And _
    Not DataFile.PostedFile.FileName.ToLower().EndsWith(".bmp") And _
    Not DataFile.PostedFile.FileName.ToLower().EndsWith(".pdf") And _
    Not DataFile.PostedFile.FileName.ToLower().EndsWith(".png")) Then
                    MessageBox.Show("File foto harus berekstensi .jpg, .jpeg, .bmp, .png, .pdf")
                    vReturn = False
                End If
            End If
        End If
        Return vReturn
    End Function

    'start --indent system 20170703
    Private Function LastMonthTransaction() As Boolean
        'Penambahan blocking alert saat create SPK yaitu jika terdapat status SPK pada bulan sebelumnya dengan status :
        '- Awal
        '- Pending Konsumen
        '- Tunggu Unit, 1, 2, 3, 4, 5
        'dan last update status nya tidak sama dengan bulan berjalan, maka create SPK tidak dapat dilakukan
        '*Validasi hanya berlaku untuk SPK yang akan di create dengan type RN dan SPK yang dilihat pada bulan sebelumnya juga adalah SPK dengan pembelian unit RN
        Dim vReturn As Boolean = True
        Try

            Dim dealerID As Integer = CType(sessionHelper.GetSession("DEALER"), Dealer).ID

            Dim objGetSPKFacade As sp_GetSPKStatusFacade = New sp_GetSPKStatusFacade(User)
            Dim arl As ArrayList = objGetSPKFacade.RetrieveFromSP(dealerID)
            Dim errMsg As String = String.Empty

            If arl.Count > 0 Then
                For Each item As sp_GetSPKStatus In arl
                    errMsg = errMsg & item.Values.ToString & " " & EnumStatusSPK.GetStringValueStatus(CInt(item.Status)) & "\n"
                Next
                If errMsg.Trim <> String.Empty Then
                    MessageBox.Show("Anda memiliki status SPK tipe Xpander yang belum terupdate yang terdiri dari : \n" & errMsg)
                    vReturn = False
                End If
            End If
        Catch ex As Exception
            'MessageBox.Show("Error checking las transaction")
        End Try


        Return vReturn
    End Function
    'end --indent system 20170703

    Private Sub SaveToDatabase()
        Dim int As Integer = New SPKHeaderFacade(User).Insert(objSPKHeader)
        If int > 0 Then
            MessageBox.Show("Data Berhasil Disimpan")
            objSPKHeader = New SPKHeaderFacade(User).Retrieve(int)
            sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader)
            'RecordStatusChangeHistory(objSPKHeader, CType(EnumStatusSPK.Status.Awal, Integer))
            'BindHeaderToForm()
            'BindDetailToGrid()
            'Response.Redirect("FrmSPKHeaderProfile.aspx?Id=" & int & "&Mode=2")
        End If
    End Sub

    Private Sub RecordStatusChangeHistory(ByVal _objSPKHeader As SPKHeader, ByVal newStatus As Integer)
        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Surat_Pesanan_Kendaraan), _objSPKHeader.SPKNumber, Nothing, newStatus)
    End Sub

    Private Sub RecordStatusChangeHistory(ByVal _objSPKHeader As SPKHeader, ByVal oldStatus As Integer, ByVal newStatus As Integer)
        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Surat_Pesanan_Kendaraan), _objSPKHeader.SPKNumber, oldStatus, newStatus)
    End Sub

    Private Sub ReCheckMCPStatus()
        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        Dim IsCV As Boolean = False

        For Each oSPKD As SPKDetail In objSPKHeader.SPKDetails
            If oSPKD.Category.CategoryCode = "CV" Then
                IsCV = True
                Exit For
            End If
        Next
        If objSPKHeader.SPKCustomer.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP AndAlso IsCV = False Then
            objSPKHeader.SPKCustomer.MCPStatus = EnumMCPStatus.MCPStatus.NonMCP
        End If
        sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader)
    End Sub

    Private Sub BindDdlBabitEventType(ByVal ddl As DropDownList, ByVal _txtCampaignName As TextBox, ByVal _lblPopUpEvent As Label)
        With ddl
            .Items.Add(New ListItem("Silahkan Pilih", "-1"))
            .Items.Add(New ListItem("Babit dan Event", "0"))
            .Items.Add(New ListItem("Lain - lain", "1"))
            .SelectedIndex = 0
        End With
        _txtCampaignName.Visible = False
        _lblPopUpEvent.Visible = False
    End Sub

    Private Sub BindDDLRejectedReason(ddl As DropDownList, strRejectReason As String)
        ''--DropDownList Rejected Reason
        Try
            ddl.Items.Clear()
            Dim al2 As ArrayList = New enumRejectedReason().RetrieveRejectedReason(strRejectReason)
            ddl.DataSource = al2
            ddl.DataTextField = "Desc"
            ddl.DataValueField = "Code"
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("Silahkan Pilih", ""))

            ddl.Items.Remove(ddl.Items.FindByValue(enumRejectedReason.RejectedReason.Batal_Otomatis))

        Catch ex As Exception
            MessageBox.Show("Error Binding DropDownList Rejected Reason, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub

    Private Sub CheckDealerSystems()
        If Not IsNothing(sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))) Then
            objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        End If

        If Not SecurityProvider.Authorize(Context.User, SR.Buat_spk_simpan_privilege) Then
            Dim criteriasDealerSystems As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasDealerSystems.opAnd(New Criteria(GetType(DealerSystems), "Dealer.ID", MatchType.Exact, objDealer.ID))
            Dim DealerSystemsFacade As DealerSystemsFacade = New DealerSystemsFacade(User)
            Dim arlDealerSystem As ArrayList = DealerSystemsFacade.Retrieve(criteriasDealerSystems)
            For Each objDealerSystem As DealerSystems In arlDealerSystem
                If objDealerSystem.isSPKDNET Then
                Else
                    If Not objSPKHeader Is Nothing Then
                        If CType(objSPKHeader.CreatedTime, Date) < objDealerSystem.GoLiveDate Then
                            btnSimpan.Visible = True
                            btnKonsumen.Visible = True
                        Else
                            ViewState("SPKDMS") = True
                        End If
                    End If
                End If
            Next
        End If
    End Sub


    Private Sub AddFaktur(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If
        If txtSalesmanCode.Text = String.Empty Then
            lblError.Text = "Error : Kode salesman tidak boleh kosong"
            Return
        End If

        Dim _SpkH As SPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        If Not IsNothing(_SpkH) AndAlso Not IsNothing(_SpkH.SPKCustomer) Then
            objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
            'SalesmenInfo()
            Mode = ViewState("Mode")
            Dim varCustID As Integer = 0
            If Not IsNothing(objSPKHeader.SPKCustomer) AndAlso Not IsNothing(objSPKHeader.SPKCustomer.SAPCustomer) Then
                varCustID = objSPKHeader.SPKCustomer.SAPCustomer.ID
            End If
            If Not IsNothing(objSPKHeader) Then
                If objSPKHeader.SPKDetails.Count > 0 Then

                    If CType(objSPKHeader.SPKDetails(e.Item.ItemIndex), SPKDetail).ID = 0 Then
                        lblError.Text = "Silahkan Simpan data kendaraan yang akan di pesan terlebih dahulu"
                        Exit Sub
                    End If
                    If Mode = enumMode.Mode.NewItemMode Then
                        If varCustID > 0 Then
                            Response.Redirect("FrmSPKDetailCustomers.aspx?Mode=" & CType(Mode, Integer) & "&spkHeader=" & ViewState.Item(Me._vstSPKHeader) & "&CustId=" & varCustID.ToString() & "&spkDetailIdx=" & CType(objSPKHeader.SPKDetails(e.Item.ItemIndex), SPKDetail).ID.ToString() & "&SPKDetailID=" & CType(objSPKHeader.SPKDetails(e.Item.ItemIndex), SPKDetail).ID.ToString())
                        Else
                            Response.Redirect("FrmSPKDetailCustomers.aspx?Mode=" & CType(Mode, Integer) & "&spkHeader=" & ViewState.Item(Me._vstSPKHeader) & "&EmailMandatory=" & EmailMandatory())
                        End If
                    Else
                        Response.Redirect("FrmSPKDetailCustomers.aspx?Id=" & CType(Request.QueryString("Id"), Integer) & "&Mode=" & CType(Mode, Integer) & "&spkHeader=" & ViewState.Item(Me._vstSPKHeader) & "&spkDetailIdx=" & CType(objSPKHeader.SPKDetails(e.Item.ItemIndex), SPKDetail).ID.ToString() & "&SPKDetailID=" & CType(objSPKHeader.SPKDetails(e.Item.ItemIndex), SPKDetail).ID.ToString())
                    End If
                Else
                    lblError.Text = "Tentukan kendaraan yang akan di pesan"
                End If
            Else
                lblError.Text = "Tentukan kendaraan yang akan di pesan -"
            End If

        Else

            lblError.Text = "Error : Silahkan Mengisi Data Konsumen & Kendaraan"
        End If

        'SetButtonEditMode()

    End Sub

    Private Sub txtCampaignName_TextChanged(sender As Object, e As EventArgs) Handles txtCampaignName.TextChanged
        If ddlBabitEventType.SelectedValue = "0" Then   'Tipe Babit & Event
            If txtCampaignName.Text.Trim <> "" Then
                Dim arr As ArrayList = GetDataEvent(txtCampaignName.Text.Trim)
                If arr.Count > 0 Then
                    Dim objBabithdr As BabitHeader = CType(arr(0), BabitHeader)
                    If Not IsNothing(objBabithdr) Then
                        txtEventTypeID.Text = objBabithdr.EventTypeID
                    Else
                        MessageBox.Show("No Reg Campaign : " & txtCampaignName.Text & " tidak valid")
                        txtEventTypeID.Text = ""
                        txtCampaignName.Text = ""
                        txtCampaignName.Focus()
                    End If
                Else
                    MessageBox.Show("No Reg Campaign : " & txtCampaignName.Text & " tidak valid")
                    txtEventTypeID.Text = ""
                    txtCampaignName.Text = ""
                    txtCampaignName.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub ddlBabitEventType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBabitEventType.SelectedIndexChanged
        txtCampaignName.Text = ""
        If ddlBabitEventType.SelectedIndex = 0 Then
            txtCampaignName.Visible = False
            lblPopUpEvent.Visible = False
        ElseIf ddlBabitEventType.SelectedValue = "0" Then
            txtCampaignName.Visible = True
            lblPopUpEvent.Visible = True
        Else
            txtCampaignName.Visible = True
            lblPopUpEvent.Visible = False
        End If
    End Sub

    Public Sub ddlFBabitEventType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlFBabitEventType As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFBabitEventType.Parent.Parent

        Dim txtFCampaignName As TextBox
        Dim lblFPopUpEvent As Label
        If gridItem.DataSetIndex > -1 Then
            txtFCampaignName = gridItem.FindControl("txtECampaignName")
            lblFPopUpEvent = gridItem.FindControl("lblEPopUpEvent")
        Else
            txtFCampaignName = gridItem.FindControl("txtFCampaignName")
            lblFPopUpEvent = gridItem.FindControl("lblFPopUpEvent")
        End If

        txtFCampaignName.Text = ""
        If ddlFBabitEventType.SelectedIndex = 0 Then    'Silahkan Pilih
            txtFCampaignName.Visible = False
            lblFPopUpEvent.Visible = False
        ElseIf ddlFBabitEventType.SelectedValue = "0" Then  'Babit & Event
            txtFCampaignName.Visible = True
            lblFPopUpEvent.Visible = True
        Else                                            'Lain-lain
            txtFCampaignName.Visible = True
            lblFPopUpEvent.Visible = False
        End If
    End Sub

#End Region


End Class
