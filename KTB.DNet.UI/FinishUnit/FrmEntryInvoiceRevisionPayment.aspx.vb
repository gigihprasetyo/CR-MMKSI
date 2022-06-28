Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.IO
Imports System.Globalization
Imports KTB.DNet.UI.Helper

Public Class FrmEntryInvoiceRevisionPayment
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ltrDealerCode As System.Web.UI.WebControls.Literal
    'Protected WithEvents ddlPaymentType As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser.Dealer.Title = "1" Then
            IsKTB = True
        Else
            IsKTB = False
        End If

        IsTransferPriv = SecurityProvider.Authorize(Context.User, SR.RevisiFakturPembayaranTransfer_Privilege)
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Dim sesHelper As SessionHelper = New SessionHelper
    Dim oDealer As Dealer

    Dim IsTransferPriv As Boolean = False
    Private objRevisionPaymentHeader As RevisionPaymentHeader
    Private objRevisionPaymentDetail As RevisionPaymentDetail
    Private arrRevisionPaymentDetails As ArrayList
    Private Mode As enumMode.Mode
    Dim myculture As CultureInfo = New CultureInfo("ID-id")
    Private IsKTB As Boolean
    Private strSessRevisionPaymentHeader As String = "sessRevisionPaymentHeader"
    Private strSessRevisionPaymentDetails As String = "sessRevisionPaymentDetails"
    Private strSessChassisMasterDDL As String = "sessChassisMasterDDL"
#End Region

#Region "Custom Method"

    Sub fillDataDealer(ByVal oD As Dealer)
        ltrDealerCode.Text = String.Format("{0} / {1}", oD.CreditAccount.ToString(), oD.DealerName)
    End Sub

    Private Sub BindDdlKategori()
        ddlKategori.Items.Clear()

        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        Dim sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection
        sortColl.Add(New Sort(GetType(Category), "ID", Sort.SortDirection.ASC))

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrCategory As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, companyCode))
        arrCategory = New CategoryFacade(User).Retrieve(criterias, sortColl)

        For Each item As Category In arrCategory
            ddlKategori.Items.Insert(0, New ListItem(item.CategoryCode, item.ID))
        Next
        ddlKategori.SelectedIndex = 0

    End Sub

    Private Sub BindDdlPaymentType(Optional ByVal AllowTransfer As Boolean = True)
        ddlPaymentType.Items.Clear()
        'ddlPaymentType.Items.Add(New ListItem("Silahkan Pilih", 0))
        For Each li As ListItem In enumPaymentTypeRevision.GetList(AllowTransfer)
            ddlPaymentType.Items.Add(li)
        Next
    End Sub

    Sub BindDdlBankName(ByVal ddl As DropDownList)
        Dim oBank As ArrayList
        oBank = New PO.BankFacade(User).RetrieveList("BankName", Sort.SortDirection.ASC)
        ddl.DataTextField = "BankName"
        ddl.DataValueField = "ID"
        ddl.DataSource = oBank
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
    End Sub

    Private Sub BindDDLRevisionType(ByRef objDropDownList As DropDownList)
        Dim objRevisionTypeFacade As RevisionTypeFacade = New RevisionTypeFacade(User)
        objDropDownList.DataSource = objRevisionTypeFacade.RetrieveActiveList()
        objDropDownList.DataTextField = "Description"
        objDropDownList.DataValueField = "ID"
        objDropDownList.DataBind()
        objDropDownList.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Function GenerateRegNumberRevision() As String
        Dim strRegNoRevision As String = vbEmpty
        Dim strSeqNumber As String = String.Empty
        Dim strSql As String = String.Empty

        strSql = "SELECT Top 1 RevisionPaymentHeader.RegNumber "
        strSql += "FROM RevisionPaymentHeader with (nolock) "
        strSql += "WHERE(RevisionPaymentHeader.RowStatus = 0) "
        strSql += "AND LEFT(Year(getdate()),2) + SUBSTRING(RegNumber, 7, 2) = Year(getdate()) "
        strSql += "ORDER BY Right(RevisionPaymentHeader.RegNumber, 4) DESC "

        Dim arrRevisionPaymentHeader As ArrayList = New RevisionPaymentHeaderFacade(User).DoRetrieveArrayList(strSql)
        If arrRevisionPaymentHeader.Count > 0 Then
            Dim oRevisionPaymentHeader As RevisionPaymentHeader = CType(arrRevisionPaymentHeader(0), RevisionPaymentHeader)
            strSeqNumber = Right("0000" + CType(CType(Right(oRevisionPaymentHeader.RegNumber, 4), Integer) + 1, String), 4)
        Else
            strSeqNumber = "0001"
        End If
        strRegNoRevision = "3" + Mid(oDealer.DealerCode, 2, oDealer.DealerCode.Length).ToString + Right(DatePart(DateInterval.Year, DateTime.Now), 2).ToString + strSeqNumber

        Return strRegNoRevision
    End Function

    Function GetBankCode() As String
        Dim strBankCode As String = String.Empty
        If ddlBankName.SelectedIndex = 0 Then Return strBankCode

        Dim oBank As Bank
        oBank = New PO.BankFacade(User).Retrieve(CType(ddlBankName.SelectedValue, Integer))
        strBankCode = oBank.BankCode

        Return strBankCode
    End Function

    Private Sub SetRevisionPaymentHeaderByInputedData()
        objRevisionPaymentHeader.Dealer = oDealer
        objRevisionPaymentHeader.PaymentType = ddlPaymentType.SelectedValue
        Mode = ViewState("Mode")
        If (Mode = enumMode.Mode.EditMode) Then
            objRevisionPaymentHeader.RegNumber = lblRegNumber.Text
        End If
        objRevisionPaymentHeader.RevisionPaymentDocID = 0
        If ddlPaymentType.SelectedValue = enumPaymentTypeRevision.PaymentType.Gyro Then
            objRevisionPaymentHeader.SlipNumber = String.Format("{0} {1}", GetBankCode(), txtGiroNo.Text.Trim)
        End If
        objRevisionPaymentHeader.GyroDate = icTglGiro.Value

        objRevisionPaymentHeader.TotalAmount = CType(lblTotalAmount.Text.Replace("Rp. ", "").Replace(".", ""), Decimal)
        'objRevisionPaymentHeader.Status = EnumStatusRevisionPayment.Status.Validasi
        objRevisionPaymentHeader.Status = EnumStatusRevisionPayment.Status.Proses
        objRevisionPaymentHeader.EvidencePath = ""
        objRevisionPaymentHeader.ActualPaymentDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        objRevisionPaymentHeader.ActualPaymentAmount = 0
        objRevisionPaymentHeader.AccDocNumber = ""
    End Sub

    Private Sub BindDataToPage()
        If IsNothing(sesHelper.GetSession(strSessRevisionPaymentHeader)) Then
            objRevisionPaymentHeader = New KTB.DNet.Domain.RevisionPaymentHeader
            sesHelper.SetSession(strSessRevisionPaymentHeader, objRevisionPaymentHeader)
            sesHelper.SetSession(strSessRevisionPaymentDetails, objRevisionPaymentHeader.RevisionPaymentDetails)

            ClearAllFields()
            ddlRevisionType_SelectedIndexChanged(Nothing, Nothing)
        Else
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode OrElse Mode = enumMode.Mode.ViewMode) Then
                BindHeaderToForm()
            End If
        End If
        BindDetailToGrid()
    End Sub

    Private Sub ClearAllFields()
        lblRegNumber.Text = "[Auto Generated]"
        ddlRevisionType.SelectedIndex = 0
        lblTotalRangka.Text = 0
        lblTotalAmount.Text = 0
        ddlKategori.SelectedIndex = 0
        ddlPaymentType.SelectedIndex = 0
        ddlBankName.SelectedIndex = 0
        txtGiroNo.Text = String.Empty
        icTglGiro.Value = DateTime.Now
        tdBankName1.Visible = False
        tdBankName2.Visible = False
        tdBankName3.Visible = False

        tdGiroNo1.Visible = False
        tdGiroNo2.Visible = False
        tdGiroNo3.Visible = False

        tdTglGiro1.Visible = False
        tdTglGiro2.Visible = False
        tdTglGiro3.Visible = False

        Dim arrChassisMaster As ArrayList = New ArrayList
        sesHelper.SetSession(strSessChassisMasterDDL, arrChassisMaster)
    End Sub

    Function GetSumTotalRangka() As Integer
        objRevisionPaymentHeader = sesHelper.GetSession(strSessRevisionPaymentHeader)
        Dim intTotal As Integer = 0
        If Not IsNothing(objRevisionPaymentHeader) And Not IsNothing(arrRevisionPaymentDetails) Then
            intTotal = arrRevisionPaymentDetails.Count
        End If
        Return intTotal
    End Function

    Function GetSumTotalAmount() As Decimal
        objRevisionPaymentHeader = sesHelper.GetSession(strSessRevisionPaymentHeader)
        Dim intTotal As Decimal = 0
        If Not IsNothing(objRevisionPaymentHeader) Then
            For Each item As DataGridItem In dtgEntryInvRevPayment.Items
                If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                    Dim lblAmount As Label = CType(item.FindControl("lblAmount"), Label)
                    If (Not IsNothing(lblAmount) And lblAmount.Visible = True) Then
                        If lblAmount.Text.Trim = "" Then lblAmount.Text = "0"
                        intTotal += CType(lblAmount.Text.Trim, Decimal)
                    End If
                End If
            Next
        End If
        Return intTotal
    End Function

    Private Sub ViewModeToForm()
        ddlKategori.Enabled = False
        ddlPaymentType.Enabled = False
        ddlBankName.Enabled = False
        ddlRevisionType.Enabled = False
        txtGiroNo.Enabled = False
        icTglGiro.Enabled = False
        dtgEntryInvRevPayment.Columns(dtgEntryInvRevPayment.Columns.Count - 3).Visible = False
        dtgEntryInvRevPayment.ShowFooter = False
        btnSave.Visible = False
        btnTransferCancel.Visible = False
    End Sub

    Private Sub EditModeToForm()
        ddlPaymentType.Enabled = True
        ddlBankName.Enabled = True
        ddlKategori.Enabled = True
        ddlRevisionType.Enabled = True
        txtGiroNo.Enabled = True
        icTglGiro.Enabled = True
        If IsKTB Then
            btnSave.Visible = False
            If objRevisionPaymentHeader.Status = New EnumDNET().enumPaymentFakturKendaraanRev.Selesai Then
                ViewModeToForm()
                btnTransferCancel.Visible = IsTransferPriv
                'dtgEntryInvRevPayment.Columns(dtgEntryInvRevPayment.Columns.Count - 1).Visible = IsTransferPriv
                dtgEntryInvRevPayment.Columns(dtgEntryInvRevPayment.Columns.Count - 2).Visible = IsTransferPriv
                dtgEntryInvRevPayment.Columns(dtgEntryInvRevPayment.Columns.Count - 3).Visible = IsTransferPriv
            Else
                btnTransferCancel.Visible = False
                dtgEntryInvRevPayment.Columns(dtgEntryInvRevPayment.Columns.Count - 1).Visible = False
                dtgEntryInvRevPayment.Columns(dtgEntryInvRevPayment.Columns.Count - 2).Visible = False
                dtgEntryInvRevPayment.Columns(dtgEntryInvRevPayment.Columns.Count - 3).Visible = False
            End If
            dtgEntryInvRevPayment.ShowFooter = False
        Else
            btnSave.Visible = CekBtnPriv()
            btnTransferCancel.Visible = False
            dtgEntryInvRevPayment.ShowFooter = True
            dtgEntryInvRevPayment.Columns(dtgEntryInvRevPayment.Columns.Count - 1).Visible = False
            dtgEntryInvRevPayment.Columns(dtgEntryInvRevPayment.Columns.Count - 2).Visible = False
        End If

        btnBack.Visible = False
        If Not IsNothing(Request.QueryString("id")) Then
            If CType(Request.QueryString("id"), Integer) > 0 Then
                btnBack.Visible = True
            End If
        End If
    End Sub

    Private Sub BindHeaderToForm()
        objRevisionPaymentHeader = sesHelper.GetSession(strSessRevisionPaymentHeader)
        arrRevisionPaymentDetails = sesHelper.GetSession(strSessRevisionPaymentDetails)

        fillDataDealer(objRevisionPaymentHeader.Dealer)
        lblRegNumber.Text = objRevisionPaymentHeader.RegNumber
        lblTotalRangka.Text = GetSumTotalRangka()
        lblTotalAmount.Text = "Rp. " + GetSumTotalAmount().ToString("#,##0")
        If CType(objRevisionPaymentHeader.PaymentType, enumPaymentTypeRevision.PaymentType) = enumPaymentTypeRevision.PaymentType.Transfer Then
            BindDdlPaymentType(True)
        End If


        ddlPaymentType.SelectedValue = CInt(objRevisionPaymentHeader.PaymentType)


        If ddlPaymentType.SelectedValue = enumPaymentTypeRevision.PaymentType.Gyro Then
            Dim strSlipNumber() As String = objRevisionPaymentHeader.SlipNumber.Split(" ")
            Dim oBank As Bank
            oBank = New PO.BankFacade(User).Retrieve(strSlipNumber(0).ToString)
            If Not IsNothing(oBank) Then
                ddlBankName.SelectedValue = oBank.ID
                Dim strGiroNo As String = String.Empty
                If objRevisionPaymentHeader.SlipNumber.Trim.IndexOf(Chr(32)) <> -1 Then
                    strGiroNo = Trim(objRevisionPaymentHeader.SlipNumber.Substring(Len(strSlipNumber(0).ToString)))
                    txtGiroNo.Text = strGiroNo
                End If
            End If
        End If
        icTglGiro.Value = objRevisionPaymentHeader.GyroDate
        ddlPaymentType_SelectedIndexChanged(Nothing, Nothing)

        If arrRevisionPaymentDetails.Count > 0 Then
            Dim intRevisionTypeID As Integer = 0, blnDiffRevisionTypeID As Boolean = False
            For Each item As RevisionPaymentDetail In arrRevisionPaymentDetails
                If intRevisionTypeID = 0 Then
                    intRevisionTypeID = item.RevisionFaktur.RevisionTypeID
                End If
                If (item.RevisionFaktur.RevisionTypeID <> intRevisionTypeID) Then
                    blnDiffRevisionTypeID = True
                    Exit For
                End If
            Next
            objRevisionPaymentDetail = CType(arrRevisionPaymentDetails(0), RevisionPaymentDetail)
            If blnDiffRevisionTypeID = False Then
                ddlRevisionType.SelectedValue = objRevisionPaymentDetail.RevisionFaktur.RevisionTypeID
            Else
                ddlRevisionType.SelectedIndex = 0
            End If

            ddlKategori.SelectedValue = objRevisionPaymentDetail.RevisionFaktur.ChassisMaster.Category.ID
        End If
        ddlRevisionType_SelectedIndexChanged(Nothing, Nothing)

    End Sub

    Private Sub BindDetailToGrid()
        arrRevisionPaymentDetails = sesHelper.GetSession(strSessRevisionPaymentDetails)
        dtgEntryInvRevPayment.DataSource = arrRevisionPaymentDetails
        dtgEntryInvRevPayment.DataBind()

        lblTotalRangka.Text = GetSumTotalRangka()
        lblTotalAmount.Text = "Rp. " + GetSumTotalAmount().ToString("#,##0")
    End Sub

    Private Sub SetButtonNewMode()
        btnSave.Enabled = True
    End Sub

    Private Function ValidateItem(ByVal chassisMasterID As Integer) As Boolean
        If (chassisMasterID = 0) Then
            MessageBox.Show("Warning : Nomor Rangka tidak boleh kosong")
            Return False
        End If
        Return True
    End Function

    Private Function ValidateDuplication(ByVal revisionFakturID As Integer, ByVal Mode As String, ByVal Rowindex As Integer) As Boolean
        Try
            If (Mode = "Add") Then
                For Each item As RevisionPaymentDetail In arrRevisionPaymentDetails
                    If (item.RevisionFaktur.ID = revisionFakturID) Then
                        MessageBox.Show("Error : Duplikasi Nomor Rangka")
                        Return False
                    End If
                Next
            Else
                Dim i As Integer = 0
                For Each item As RevisionPaymentDetail In arrRevisionPaymentDetails
                    If (item.RevisionFaktur.ID = revisionFakturID) Then
                        If i <> Rowindex Then
                            MessageBox.Show("Error : Duplikasi Nomor Rangka")
                            Return False
                        End If
                    End If
                    i = i + 1
                Next
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Error : Nomor Rangka tidak ditemukan.")
            Return False
        End Try
    End Function

#End Region

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.RevisiFakturPembayaranInput_Privilege) Then
            If Not SecurityProvider.Authorize(Context.User, SR.RevisiFakturPembayaranLihat_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=REVISI FAKTUR - INPUT PEMBAYARAN")
            End If
        End If
    End Sub

    Private Function CekBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.RevisiFakturPembayaranInput_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        oDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        ViewState("Mode") = Request.QueryString("mode")

        If IsNothing(ViewState("Mode")) Then
            ViewState("Mode") = enumMode.Mode.NewItemMode
        End If
        Dim id As Integer = 0
        Try
            id = CInt(Request.QueryString("id"))
        Catch
        End Try

        If Not IsPostBack Then
            sesHelper.RemoveSession(strSessRevisionPaymentHeader)
            sesHelper.RemoveSession(strSessRevisionPaymentDetails)
            sesHelper.RemoveSession(strSessChassisMasterDDL)

            BindDdlKategori()

            BindDdlPaymentType(Not (Now >= InitLockTransfer()))
            BindDDLRevisionType(ddlRevisionType)
            BindDdlBankName(ddlBankName)

            ViewState("currSortColumn") = "RevisionFaktur.ChassisMaster.ChassisNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            Dim strCalledByForm As String = sesHelper.GetSession("FrmEntryInvoiceRevisionPayment_CalledBy")
            If strCalledByForm = "FrmDaftarInvoiceRevisionPayment.aspx" Then
                btnBack.Visible = True

                objRevisionPaymentHeader = New RevisionPaymentHeaderFacade(User).Retrieve(id)
                sesHelper.SetSession(strSessRevisionPaymentHeader, objRevisionPaymentHeader)
                sesHelper.SetSession(strSessRevisionPaymentDetails, objRevisionPaymentHeader.RevisionPaymentDetails)
            Else
                objRevisionPaymentHeader = New RevisionPaymentHeader
                sesHelper.SetSession(strSessRevisionPaymentHeader, objRevisionPaymentHeader)
                sesHelper.SetSession(strSessRevisionPaymentDetails, objRevisionPaymentHeader.RevisionPaymentDetails)
                ClearAllFields()
                ddlRevisionType_SelectedIndexChanged(Nothing, Nothing)

            End If

            fillDataDealer(oDealer)
            BindDataToPage()

            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.ViewMode) Then
                ViewModeToForm()
            ElseIf (Mode = enumMode.Mode.EditMode OrElse Mode = enumMode.Mode.NewItemMode) Then
                EditModeToForm()
            End If
        End If

        ddlPaymentType_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub dtgEntryInvRevPayment_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryInvRevPayment.ItemCommand
        objRevisionPaymentHeader = sesHelper.GetSession(strSessRevisionPaymentHeader)
        arrRevisionPaymentDetails = sesHelper.GetSession(strSessRevisionPaymentDetails)

        Dim objRevisionPaymentDetailFacade As RevisionPaymentDetailFacade
        Select Case (e.CommandName)
            Case "Add"
                Dim isCancel As Boolean = False
                Dim strCancelReason As String = String.Empty

                If Not Page.IsValid Then
                    Return
                End If

                Dim ddlNoRangka As DropDownList = CType(e.Item.FindControl("ddlNoRangkaF"), DropDownList)
                Dim txtCancelReason As TextBox = CType(e.Item.FindControl("txtCancelReason"), TextBox)
                Dim lbtnActive As LinkButton = CType(e.Item.FindControl("lbtnActive"), LinkButton)
                Dim lbtnInactive As LinkButton = CType(e.Item.FindControl("lbtnInactive"), LinkButton)
                If ddlNoRangka.SelectedIndex = 0 Then
                    MessageBox.Show("Nomor Rangka masih kosong")
                    Return
                End If
                If Not IsNothing(txtCancelReason) Then
                    If txtCancelReason.Visible = True Then
                        isCancel = True
                        strCancelReason = txtCancelReason.Text
                    End If
                End If
                If ValidateDuplication(ddlNoRangka.SelectedValue, "Add", e.Item.ItemIndex) Then
                    objRevisionPaymentDetail = CreateRevisionPaymentDetail(ddlNoRangka.SelectedValue, strCancelReason, isCancel)
                Else
                    Exit Sub
                End If
                arrRevisionPaymentDetails.Add(objRevisionPaymentDetail)
                sesHelper.SetSession(strSessRevisionPaymentDetails, arrRevisionPaymentDetails)
                BindDetailToGrid()

            Case "NoRangkaChangedF"
                Dim ddlNoRangkaF As DropDownList = e.Item.FindControl("ddlNoRangkaF")
                Dim lblCategoryCodeF As Label = e.Item.FindControl("lblCategoryCodeF")
                Dim lblDealerCodeF As Label = e.Item.FindControl("lblDealerCodeF")
                Dim lblCustomerNameF As Label = e.Item.FindControl("lblCustomerNameF")
                Dim lblRegNumberF As Label = e.Item.FindControl("lblRegNumberF")
                Dim lblDebitChargeNoF As Label = e.Item.FindControl("lblDebitChargeNoF")
                Dim lblAmountF As Label = e.Item.FindControl("lblAmountF")

                Dim oRevFaktur As RevisionFaktur = New RevisionFakturFacade(User).Retrieve(CType(ddlNoRangkaF.SelectedValue, Integer))
                If Not IsNothing(oRevFaktur) AndAlso oRevFaktur.ID > 0 Then
                    Dim strDebitChargeNo As String = String.Empty
                    Dim dblAmount As Double = 0
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(RevisionSAPDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(RevisionSAPDoc), "RevisionFaktur.ID", MatchType.Exact, oRevFaktur.ID))
                    Dim arrRevisionSAPDoc As ArrayList = New RevisionSAPDocFacade(User).Retrieve(criterias)
                    If Not IsNothing(arrRevisionSAPDoc) Then
                        Dim oRevisionSAPDoc As RevisionSAPDoc = CType(arrRevisionSAPDoc(0), RevisionSAPDoc)
                        strDebitChargeNo = oRevisionSAPDoc.DebitChargeNo
                        dblAmount = CType(arrRevisionSAPDoc(0), RevisionSAPDoc).DCAmount
                    End If

                    'Dim criterias2 As New CriteriaComposite(New Criteria(GetType(RevisionPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'criterias2.opAnd(New Criteria(GetType(RevisionPrice), "RevisionType.ID", MatchType.Exact, oRevFaktur.RevisionType.ID))
                    'criterias2.opAnd(New Criteria(GetType(RevisionPrice), "Category.ID", MatchType.Exact, oRevFaktur.ChassisMaster.Category.ID))
                    'Dim arrRevisionPrice As ArrayList = New RevisionPriceFacade(User).Retrieve(criterias2)
                    'If Not IsNothing(arrRevisionPrice) Then
                    '    Dim oRevisionPrice As RevisionPrice = CType(arrRevisionPrice(0), RevisionPrice)
                    '    dblAmount = oRevisionPrice.Amount
                    'End If

                    lblCategoryCodeF.Text = oRevFaktur.ChassisMaster.Category.CategoryCode
                    'lblDealerCodeF.Text = oRevFaktur.ChassisMaster.Dealer.DealerCode

                    lblCustomerNameF.Text = oRevFaktur.EndCustomer.Customer.Name1
                    lblRegNumberF.Text = oRevFaktur.RegNumber
                    lblDebitChargeNoF.Text = strDebitChargeNo
                    lblAmountF.Text = dblAmount
                    lblAmountF.Text = IIf(lblAmountF.Text.Trim = "0", "0", CDbl(lblAmountF.Text).ToString("#,#", myculture))
                Else
                    lblCategoryCodeF.Text = ""
                    lblDealerCodeF.Text = ""
                    lblCustomerNameF.Text = ""
                    lblRegNumberF.Text = ""
                    lblDebitChargeNoF.Text = ""
                    lblAmountF.Text = 0
                End If

            Case "Delete"
                arrRevisionPaymentDetails.Remove(arrRevisionPaymentDetails.Item(e.Item.ItemIndex))
                sesHelper.SetSession(strSessRevisionPaymentDetails, arrRevisionPaymentDetails)

                BindDetailToGrid()
                Mode = ViewState("Mode")
                dtgEntryInvRevPayment.ShowFooter = True

            Case "Active"
                Dim oRevisionPaymentDetail As RevisionPaymentDetail = arrRevisionPaymentDetails.Item(e.Item.ItemIndex)
                oRevisionPaymentDetail.IsCancel = 0
                Dim nResult As Integer = New RevisionPaymentDetailFacade(User).Update(oRevisionPaymentDetail)
                If nResult <> -1 Then
                    '--Retrieve and set ulang Header
                    objRevisionPaymentHeader = New RevisionPaymentHeaderFacade(User).Retrieve(oRevisionPaymentDetail.RevisionPaymentHeader.ID)
                    sesHelper.SetSession(strSessRevisionPaymentHeader, objRevisionPaymentHeader)
                End If
                BindDataToPage()

            Case "Inactive"
                Dim oRevisionPaymentDetail As RevisionPaymentDetail = arrRevisionPaymentDetails.Item(e.Item.ItemIndex)
                oRevisionPaymentDetail.IsCancel = 1
                Dim nResult As Integer = New RevisionPaymentDetailFacade(User).Update(oRevisionPaymentDetail)
                If nResult <> -1 Then
                    '--Retrieve and set ulang Header
                    objRevisionPaymentHeader = New RevisionPaymentHeaderFacade(User).Retrieve(oRevisionPaymentDetail.RevisionPaymentHeader.ID)
                    sesHelper.SetSession(strSessRevisionPaymentHeader, objRevisionPaymentHeader)
                End If
                BindDataToPage()

            Case "SaveCancelReason"
                Dim txtCancelReason As TextBox = e.Item.FindControl("txtCancelReason")
                Dim oRevisionPaymentDetail As RevisionPaymentDetail = arrRevisionPaymentDetails.Item(e.Item.ItemIndex)
                oRevisionPaymentDetail.CancelReason = txtCancelReason.Text.Trim
                Dim nResult As Integer = New RevisionPaymentDetailFacade(User).Update(oRevisionPaymentDetail)
                If nResult <> -1 Then
                    '--Retrieve and set ulang Header
                    objRevisionPaymentHeader = New RevisionPaymentHeaderFacade(User).Retrieve(oRevisionPaymentDetail.RevisionPaymentHeader.ID)
                    sesHelper.SetSession(strSessRevisionPaymentHeader, objRevisionPaymentHeader)
                    MessageBox.Show(SR.SaveSuccess)
                End If
                BindDataToPage()

            Case "DealerCodeChangedF"
                Dim ddlNoRangkaF As DropDownList = e.Item.FindControl("ddlNoRangkaF")
                Dim ddlDealerCodeF As DropDownList = e.Item.FindControl("ddlDealerCodeF")
                BindDDLChassisNumber(ddlNoRangkaF, ddlDealerCodeF.SelectedValue)
        End Select

    End Sub

    Function CreateRevisionPaymentDetail(ByVal _revisionFakturID As Integer, ByVal reasonCancel As String, ByVal isCancel As Boolean) As RevisionPaymentDetail
        Dim oRevPaymentDetail As RevisionPaymentDetail = New RevisionPaymentDetail
        Dim oRevisionFaktur As RevisionFaktur
        Dim oRevisionSAPDoc As RevisionSAPDoc
        Dim criterias As New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ID", MatchType.Exact, _revisionFakturID))
        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.Exact, CType(EnumDNET.enumFakturKendaraanRev.Konfirmasi, Short)))
        Dim arrRevisionFaktur As ArrayList = New RevisionFakturFacade(User).Retrieve(criterias)
        If arrRevisionFaktur.Count > 0 Then
            oRevisionFaktur = CType(arrRevisionFaktur(0), RevisionFaktur)

            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(RevisionSAPDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(RevisionSAPDoc), "RevisionFaktur.ID", MatchType.Exact, oRevisionFaktur.ID))
            Dim arrRevisionSAPDoc As ArrayList = New RevisionSAPDocFacade(User).Retrieve(criterias2)
            If arrRevisionSAPDoc.Count > 0 Then
                oRevisionSAPDoc = CType(arrRevisionSAPDoc(0), RevisionSAPDoc)
            End If
        End If
        oRevPaymentDetail.RevisionPaymentHeader = objRevisionPaymentHeader
        oRevPaymentDetail.RevisionFaktur = oRevisionFaktur
        oRevPaymentDetail.RevisionSAPDoc = oRevisionSAPDoc
        oRevPaymentDetail.IsCancel = isCancel
        oRevPaymentDetail.CancelReason = reasonCancel
        oRevPaymentDetail.ChassisNumber = oRevisionFaktur.ChassisMaster.ChassisNumber

        Return oRevPaymentDetail
    End Function

    Private Sub dtgEntryInvRevPayment_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEntryInvRevPayment.ItemDataBound
        objRevisionPaymentHeader = sesHelper.GetSession(strSessRevisionPaymentHeader)
        arrRevisionPaymentDetails = sesHelper.GetSession(strSessRevisionPaymentDetails)

        If e.Item.ItemType = ListItemType.Footer Then
            'Dim ddl As DropDownList = CType(e.Item.FindControl("ddlNoRangkaF"), DropDownList)
            Dim ddl2 As DropDownList = CType(e.Item.FindControl("ddlDealerCodeF"), DropDownList)
            'BindDDLChassisNumber(ddl, ddl2)
            BindDDLDealer(ddl2)

        ElseIf (e.Item.ItemIndex >= 0) Then
            objRevisionPaymentDetail = arrRevisionPaymentDetails(e.Item.ItemIndex)
            Dim lblNoRangka As Label = CType(e.Item.FindControl("lblNoRangka"), Label)
            Dim lblCategoryCode As Label = CType(e.Item.FindControl("lblCategoryCode"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblCustomerName As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
            Dim lblRegNumber As Label = CType(e.Item.FindControl("lblRegNumber"), Label)
            Dim lblDebitChargeNo As Label = CType(e.Item.FindControl("lblDebitChargeNo"), Label)
            Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
            Dim lbtnActive As LinkButton = CType(e.Item.FindControl("lbtnActive"), LinkButton)
            Dim lbtnInactive As LinkButton = CType(e.Item.FindControl("lbtnInactive"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim txtCancelReason As TextBox = CType(e.Item.FindControl("txtCancelReason"), TextBox)
            Dim lbtnSaveCancelReason As LinkButton = CType(e.Item.FindControl("lbtnSaveCancelReason"), LinkButton)
            Dim lblTRNumber As Label = CType(e.Item.FindControl("lblTRNumber"), Label)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.Exact, objRevisionPaymentDetail.RevisionFaktur.ChassisMaster.ID))
            Dim arrChassisMaster As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
            If arrChassisMaster.Count > 0 Then
                lblNoRangka.Text = CType(arrChassisMaster(0), ChassisMaster).ChassisNumber
            End If
            lblCategoryCode.Text = objRevisionPaymentDetail.RevisionFaktur.ChassisMaster.Category.CategoryCode
            lblDealerCode.Text = objRevisionPaymentDetail.RevisionFaktur.ChassisMaster.Dealer.DealerCode
            lblCustomerName.Text = objRevisionPaymentDetail.RevisionFaktur.EndCustomer.Customer.Name1
            lblRegNumber.Text = objRevisionPaymentDetail.RevisionFaktur.RegNumber
            lblDebitChargeNo.Text = objRevisionPaymentDetail.RevisionSAPDoc.DebitChargeNo
            Dim iraccDocNum As IRAccDocNumber = New IRAccDocNumberFacade(User).RetrieveByDebitChargeNo(objRevisionPaymentDetail.RevisionSAPDoc.DebitChargeNo)
            If iraccDocNum.ID > 0 Then
                lblTRNumber.Text = iraccDocNum.TRNo
            End If

            Dim dblAmount As Double = 0
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(RevisionSAPDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(RevisionSAPDoc), "RevisionFaktur.ID", MatchType.Exact, objRevisionPaymentDetail.RevisionFaktur.ID))
            Dim arrRevisionSAPDoc As ArrayList = New RevisionSAPDocFacade(User).Retrieve(criterias2)
            If arrRevisionSAPDoc.Count > 0 Then
                dblAmount = CType(arrRevisionSAPDoc(0), RevisionSAPDoc).DCAmount
            End If

            lblAmount.Text = IIf(dblAmount = 0, "0", CDbl(dblAmount).ToString("#,#", myculture))
            txtCancelReason.Text = objRevisionPaymentDetail.CancelReason

            If objRevisionPaymentHeader.Status = New EnumDNET().enumPaymentFakturKendaraanRev.Baru Then
                lbtnDelete.Visible = True
            Else
                lbtnDelete.Visible = False
            End If
            lbtnActive.Visible = True
            lbtnInactive.Visible = True
            If objRevisionPaymentDetail.IsCancel = 0 Then
                lbtnActive.Visible = False
                lbtnSaveCancelReason.Visible = False
                txtCancelReason.Visible = False
                lbtnInactive.Visible = True

            ElseIf objRevisionPaymentDetail.IsCancel = 1 Then
                lbtnInactive.Visible = False
                lbtnSaveCancelReason.Visible = True
                txtCancelReason.Visible = True
                lbtnActive.Visible = True
            End If

            If Not IsKTB Then
                lbtnActive.Visible = False
                lbtnInactive.Visible = False
            Else
                lbtnDelete.Visible = False
            End If

        End If

        If Not (arrRevisionPaymentDetails.Count = 0 Or e.Item.ItemIndex = -1) Then
            objRevisionPaymentDetail = arrRevisionPaymentDetails(e.Item.ItemIndex)
            Dim lbtnHapus As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lbtnHapus.Attributes("onclick") = "return confirm('Yakin akan hapus record ini?');"
        End If
    End Sub

    Private Sub dtgEntryInvRevPayment_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEntryInvRevPayment.SortCommand
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
        BindDetailToGrid()
    End Sub

    Private Function InitLockTransfer() As DateTime
        Dim dt As DateTime = DateTime.Now.AddYears(5)

        Try
            Dim strVal As String = ""
            strVal = KTB.DNet.Lib.WebConfig.GetString("LockTransferRev")
            If strVal <> "" Then
                dt = Convert.ToDateTime(strVal)
            End If
        Catch ex As Exception

        End Try

        Return dt
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Mode = ViewState("Mode")

        'If ddlPaymentType.SelectedIndex = 0 Then
        '    MessageBox.Show("Harap isi Jenis Pembayaran terlebih dahulu")
        '    Exit Sub
        'End If
        If ddlPaymentType.SelectedValue = enumPaymentTypeRevision.PaymentType.Gyro Then
            If ddlBankName.SelectedIndex = 0 Then
                MessageBox.Show("Harap isi Nama Bank terlebih dahulu")
                Exit Sub
            End If
            If txtGiroNo.Text.Trim = "" Then
                MessageBox.Show("Harap isi Nomor Giro terlebih dahulu")
                Exit Sub
            End If
        End If



        If ddlPaymentType.SelectedValue = enumPaymentTypeRevision.PaymentType.Transfer AndAlso Now >= InitLockTransfer() Then
            MessageBox.Show("Pembayaran Transfer Sudah tidak diperbolehkan")
            Exit Sub
        End If


        Dim objRevisionPaymentHeaderCurr As RevisionPaymentHeader
        objRevisionPaymentHeader = sesHelper.GetSession(strSessRevisionPaymentHeader)
        If Mode = enumMode.Mode.EditMode Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(RevisionPaymentHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionPaymentHeader), "ID", MatchType.Exact, objRevisionPaymentHeader.ID))
            Dim arrRevisionPaymentHeader As ArrayList = New RevisionPaymentHeaderFacade(User).Retrieve(criterias)
            If arrRevisionPaymentHeader.Count > 0 Then
                objRevisionPaymentHeaderCurr = CType(arrRevisionPaymentHeader(0), RevisionPaymentHeader)
            End If
        End If
        If IsNothing(objRevisionPaymentHeader) Then
            MessageBox.Show("Harap isi data Revisi Pembayaran terlebih dahulu")
            Exit Sub
        End If

        arrRevisionPaymentDetails = sesHelper.GetSession(strSessRevisionPaymentDetails)
        If arrRevisionPaymentDetails.Count > 0 Then
            'For Each item As RevisionPaymentDetail In arrRevisionPaymentDetail
            '    If item.RevisionFaktur.RevisionType.ID <> ddlRevisionType.SelectedValue Then
            '        MessageBox.Show("Nomor Rangka : " & item.RevisionFaktur.ChassisMaster.ChassisNumber & " tidak sesuai dengan Tipe Revisi : " & ddlRevisionType.SelectedItem.Text)
            '        Exit Sub
            '    End If
            'Next

            SetRevisionPaymentHeaderByInputedData()
            Dim nResult As Integer
            Dim strRegNo As String = ""

            Try
                If Mode = enumMode.Mode.NewItemMode Then
                    nResult = New RevisionPaymentDetailFacade(User).InsertRevisionPaymentHeaderDetail(objRevisionPaymentHeader, arrRevisionPaymentDetails)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    nResult = New RevisionPaymentDetailFacade(User).UpdateRevisionPaymentHeaderDetail(objRevisionPaymentHeader, objRevisionPaymentHeaderCurr, arrRevisionPaymentDetails)
                End If


                Dim objInserted As RevisionPaymentHeader = New RevisionPaymentHeaderFacade(User).Retrieve(nResult)
                ValidateAndTransfer(objInserted)

                strRegNo = objInserted.RegNumber
                lblRegNumber.Text = strRegNo
            Catch ex As Exception
                MessageBox.Show("Gagal simpan revisi pembayaran " & ex.Message)
                Return
            End Try
            If nResult <> -1 Then
                Dim strJS As String
                strJS = "<script language=JavaScript> "
                strJS += "alert('Nomor Registrasi " & strRegNo & " berhasil disimpan dan diproses.'); "
                If Not IsNothing(Request.QueryString("id")) Then
                    If CType(Request.QueryString("id"), Integer) > 0 Then
                        strJS += "window.location.href='FrmDaftarInvoiceRevisionPayment.aspx'; "
                    End If
                Else
                    strJS += "window.location.href='FrmEntryInvoiceRevisionPayment.aspx'; "
                End If
                strJS += "</script>"

                Page.RegisterStartupScript("test", strJS)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        Else
            MessageBox.Show("Data kendaraan harus diisi")
        End If
    End Sub


#End Region

    Function IsExistChassisNumberInSession(ByVal strChassisNo As String) As Boolean
        arrRevisionPaymentDetails = sesHelper.GetSession(strSessRevisionPaymentDetails)
        For Each item As RevisionPaymentDetail In arrRevisionPaymentDetails
            If item.ChassisNumber = strChassisNo Then
                Return True
            End If
        Next
        Return False
    End Function

    Sub BindDDLChassisNumber(ddl As DropDownList, DealerCode As String)
        objRevisionPaymentHeader = sesHelper.GetSession(strSessRevisionPaymentHeader)
        Mode = ViewState("Mode")

        ddl.Items.Clear()
        Dim arrRevisionFaktur As ArrayList = CType(sesHelper.GetSession(strSessChassisMasterDDL), ArrayList)
        For Each item As RevisionFaktur In arrRevisionFaktur
            Dim blnDiffHeaderID As Boolean = False
            For Each oRevDtl As RevisionPaymentDetail In item.RevisionPaymentDetails
                If objRevisionPaymentHeader.ID <> oRevDtl.RevisionPaymentHeader.ID Then
                    blnDiffHeaderID = True
                    Exit For
                End If
            Next
            If item.ChassisMaster.Dealer.DealerCode = DealerCode Then
                If (item.RevisionPaymentDetails.Count = 0) OrElse (Mode = enumMode.Mode.EditMode) Then
                    If blnDiffHeaderID = False Then
                        If oDealer.CreditAccount = item.ChassisMaster.Dealer.CreditAccount Then
                            If Not IsExistChassisNumberInSession(item.ChassisMaster.ChassisNumber) Then
                                ddl.Items.Insert(0, New ListItem(item.ChassisMaster.ChassisNumber, item.ID))
                            End If
                        End If
                    End If
                End If
            End If
        Next
        ddl.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
        ddl.SelectedIndex = 0
    End Sub

    Sub BindDDLDealer(ddl As DropDownList)
        objRevisionPaymentHeader = sesHelper.GetSession(strSessRevisionPaymentHeader)
        Mode = ViewState("Mode")

        ddl.Items.Clear()
        Dim arrRevisionFaktur As ArrayList = CType(sesHelper.GetSession(strSessChassisMasterDDL), ArrayList)
        For Each item As RevisionFaktur In arrRevisionFaktur
            Dim blnDiffHeaderID As Boolean = False
            For Each oRevDtl As RevisionPaymentDetail In item.RevisionPaymentDetails
                If objRevisionPaymentHeader.ID <> oRevDtl.RevisionPaymentHeader.ID Then
                    blnDiffHeaderID = True
                    Exit For
                End If
            Next
            If (item.RevisionPaymentDetails.Count = 0) OrElse (Mode = enumMode.Mode.EditMode) Then
                If blnDiffHeaderID = False Then
                    If oDealer.CreditAccount = item.ChassisMaster.Dealer.CreditAccount Then
                        If Not IsExistChassisNumberInSession(item.ChassisMaster.ChassisNumber) Then
                            If Not ddl.Items.Contains(New ListItem(item.ChassisMaster.Dealer.DealerCode, item.ChassisMaster.Dealer.DealerCode)) Then
                                ddl.Items.Insert(0, New ListItem(item.ChassisMaster.Dealer.DealerCode, item.ChassisMaster.Dealer.DealerCode))
                            End If
                        End If
                    End If
                End If
            End If
        Next
        ddl.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
        ddl.SelectedIndex = 0
    End Sub

    Private Sub ddlRevisionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRevisionType.SelectedIndexChanged
        SetSessionChassisMasterDDL()
        If ddlRevisionType.SelectedIndex <> 0 Then
            FilterDataGridBaseOnRevisionType()
        End If
        BindDetailToGrid()
    End Sub

    Private Sub SetSessionChassisMasterDDL()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrRevisionFaktur As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlRevisionType.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionType.ID", MatchType.Exact, ddlRevisionType.SelectedValue))
        End If
        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.Exact, CType(EnumDNET.enumFakturKendaraanRev.Konfirmasi, Short)))
        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "IsPay", MatchType.Exact, CType(EnumDNET.enumPaymentOption.Bayar, Short)))
        Dim sqlRF As String = "select distinct RevisionFakturID from RevisionSAPDoc (noLock) where rowstatus = 0"
        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ID", MatchType.InSet, "(" & sqlRF & ")"))
        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.Dealer.CreditAccount", MatchType.Exact, oDealer.CreditAccount))
        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.Category.ProductCategory.Code", MatchType.Exact, companyCode))
        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.Category.ID", MatchType.Exact, ddlKategori.SelectedValue))

        arrRevisionFaktur = New RevisionFakturFacade(User).Retrieve(criterias)
        sesHelper.SetSession(strSessChassisMasterDDL, arrRevisionFaktur)
    End Sub

    Private Sub FilterDataGridBaseOnRevisionType()
        Dim objRevPaymentDtl As New RevisionPaymentDetail
        arrRevisionPaymentDetails = sesHelper.GetSession(strSessRevisionPaymentDetails)
        If arrRevisionPaymentDetails.Count > 0 Then
            For i As Integer = arrRevisionPaymentDetails.Count - 1 To 0 Step -1
                objRevPaymentDtl = CType(arrRevisionPaymentDetails(i), RevisionPaymentDetail)
                If objRevPaymentDtl.RevisionFaktur.RevisionType.ID <> ddlRevisionType.SelectedValue Then
                    arrRevisionPaymentDetails.Remove(arrRevisionPaymentDetails.Item(i))
                End If
            Next
            sesHelper.SetSession(strSessRevisionPaymentDetails, arrRevisionPaymentDetails)
        End If
    End Sub

    Private Sub FilterDataGridBaseOnCategory()
        Dim objRevPaymentDtl As New RevisionPaymentDetail
        arrRevisionPaymentDetails = sesHelper.GetSession(strSessRevisionPaymentDetails)
        If arrRevisionPaymentDetails.Count > 0 Then
            For i As Integer = arrRevisionPaymentDetails.Count - 1 To 0 Step -1
                objRevPaymentDtl = CType(arrRevisionPaymentDetails(i), RevisionPaymentDetail)
                If objRevPaymentDtl.RevisionFaktur.ChassisMaster.Category.CategoryCode <> ddlKategori.SelectedItem.Text Then
                    arrRevisionPaymentDetails.Remove(arrRevisionPaymentDetails.Item(i))
                End If
            Next
            sesHelper.SetSession(strSessRevisionPaymentDetails, arrRevisionPaymentDetails)
        End If
    End Sub

    Private Sub ddlPaymentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentType.SelectedIndexChanged
        If ddlPaymentType.SelectedValue = enumPaymentTypeRevision.PaymentType.Gyro Then
            tdBankName1.Visible = True
            tdBankName2.Visible = True
            tdBankName3.Visible = True

            tdGiroNo1.Visible = True
            tdGiroNo2.Visible = True
            tdGiroNo3.Visible = True

            tdTglGiro1.InnerText = "Tgl Gyro"
            tdTglGiro1.Visible = True
            tdTglGiro2.Visible = True
            tdTglGiro3.Visible = True

            If icTglGiro.Value = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                icTglGiro.Value = DateTime.Now
            End If
        Else
            tdBankName1.Visible = False
            tdBankName2.Visible = False
            tdBankName3.Visible = False

            tdGiroNo1.Visible = False
            tdGiroNo2.Visible = False
            tdGiroNo3.Visible = False

            tdTglGiro1.InnerText = "Tgl Transfer"
            tdTglGiro1.Visible = True
            tdTglGiro2.Visible = True
            tdTglGiro3.Visible = True

            ddlBankName.SelectedIndex = 0
            txtGiroNo.Text = ""
            If icTglGiro.Value = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                icTglGiro.Value = DateTime.Now
            End If
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmDaftarInvoiceRevisionPayment.aspx")
    End Sub

    Private Sub btnTransferCancel_Click(sender As Object, e As EventArgs) Handles btnTransferCancel.Click
        Dim arl As ArrayList = New ArrayList
        Dim oRevisionPaymentDetail As RevisionPaymentDetail
        Dim arrDataCancel As ArrayList = New ArrayList

        For Each item As DataGridItem In dtgEntryInvRevPayment.Items
            Dim strErrMsg As String
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim lbtnInactive As LinkButton = CType(item.FindControl("lbtnInactive"), LinkButton)
                Dim lbtnActive As LinkButton = CType(item.FindControl("lbtnActive"), LinkButton)
                Dim txtCancelReason As TextBox = CType(item.FindControl("txtCancelReason"), TextBox)
                Dim hdnID As HiddenField = CType(item.FindControl("hdnID"), HiddenField)

                If (Not IsNothing(txtCancelReason) And txtCancelReason.Visible = True) Then
                    oRevisionPaymentDetail = New RevisionPaymentDetail
                    oRevisionPaymentDetail = New RevisionPaymentDetailFacade(User).Retrieve(Convert.ToInt32(hdnID.Value))
                    If oRevisionPaymentDetail.RevisionPaymentHeader.Status = New EnumDNET().enumPaymentFakturKendaraanRev.Selesai Then
                        arrDataCancel.Add(oRevisionPaymentDetail)
                    End If
                End If
            End If

        Next

        If arrDataCancel.Count > 0 Then
            Try
                Me.TransferCancelToSAP(arrDataCancel)
            Catch ex As Exception
                MessageBox.Show("Transfer Cancel Gagal")
                Return
            End Try
            MessageBox.Show("Transfer Cancel Sukses")
            BindDataToPage()
        Else
            MessageBox.Show("Tidak ada data yang akan diproses")
        End If
    End Sub

    Private Sub TransferCancelToSAP(ByVal arlToCancelTransfer As ArrayList)
        Dim oFH As New FileHelper()

        Dim PreFolder As String
        Dim str As FileInfo
        Try
            PreFolder = "IR"
            str = oFH.TransferIRCancelToSAP("Normal", arlToCancelTransfer, PreFolder)
        Catch ex As Exception
            MessageBox.Show(SR.UploadFail(str.Name))
        End Try
    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKategori.SelectedIndexChanged
        SetSessionChassisMasterDDL()
        FilterDataGridBaseOnCategory()
        BindDetailToGrid()
    End Sub

    Private Sub ValidateAndTransfer(objRevisionPaymentHeaderCurr As RevisionPaymentHeader)
        Dim arl As New ArrayList
        Dim strMessage As String = String.Empty
        arl.Add(objRevisionPaymentHeaderCurr)
        If (New RevisionPaymentHeaderFacade(User).UpdateRevisionPaymentHeaders(arl) = 1) Then
            If arl.Count > 0 Then
                Try
                    Me.TransferVAToSAP(arl)
                Catch ex As Exception
                    strMessage = ", Transfer data Virtual Account ke SAP Gagal"
                End Try
            End If
            MessageBox.Show(SR.UpdateSucces & strMessage)
        Else
            MessageBox.Show(SR.UpdateFail)
        End If
    End Sub

    Private Sub TransferVAToSAP(ByVal arlToTransfer As ArrayList)
        If arlToTransfer.Count < 1 Then
            MessageBox.Show("Tidak ada data yg ditransfer")
            Exit Sub
        End If

        Dim _fileHelper As New FileHelper()
        Dim str As FileInfo
        Dim PreFolder As String = "IRTransferPayment"
        Try
            str = _fileHelper.TransferVAIRToSAP(arlToTransfer, PreFolder)
        Catch ex As Exception
            MessageBox.Show(SR.UploadFail(str.Name))
        End Try
    End Sub

End Class