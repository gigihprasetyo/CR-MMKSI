Imports System.IO
Imports System.Text

Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports Ktb.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Parser
Imports KTB.DNet.Lib
Imports KTB.DNet.SAP
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessValidation.Helpers


Public Class FrmDetailEstimationEquip
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
    Protected WithEvents lblTotalAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents dtgIPDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icOrderDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnConfirm As System.Web.UI.WebControls.Button
    Protected WithEvents btnUpdatePrice As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtPartNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpanRemark As System.Web.UI.WebControls.Button

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

    Dim EmailIndentRecipient As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentRecipient")
    Dim EmailIndentRecipientCC As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentRecipientCC")
    Dim EmailIndentCC1 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC1")
    Dim EmailIndentCC2 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC2")
    Dim EmailIndentCC3 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC3")
    Dim EmailIndentCC4 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC4")
    Dim EmailIndentCC5 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC5")
    Dim EmailIndentSignedName As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentSignedName")
    Dim EmailIndentSignedJob As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentSignedJob")
    Private _sessConfirmedEEDs As String = "FrmDetailEstimationEquip._sessConfirmedEEDs"
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

#End Region

#Region "event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            viewstate.Add("FirstLoaded", "1")
        Else
            viewstate.Add("FirstLoaded", "0")
        End If

        objDealer = _sesshelper.GetSession("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            InitiateAuthorization()
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            _state = False
            dtgIPDetail.ShowFooter = False
            btnSimpanRemark.Visible = True
        Else
            dtgIPDetail.ShowFooter = True
            btnSimpanRemark.Visible = False
        End If

        _indentPHID = Request.QueryString("EstimationEquipHeaderID")
        _state = Convert.ToBoolean(Request.QueryString("View"))

        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            _blnDealer = True
        Else
            _blnDealer = False
        End If

        If _indentPHID = 0 Or _isPopup = 1 Then

        End If

        If _isPopup <> 1 Then

        End If

        If Not bCekSendPriv Then

        End If

        If IsPostBack Then
            If _indentPHID = 0 Then
                If CType(ViewState("vsSave"), String) = "false" Then
                    If Request.Form("hdnValNew") = "1" Then

                    End If
                End If
            End If
            Return
        End If

        'if ! postback
        ViewState("CurrentSortColumn") = "SparePartMaster.PartNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        If _indentPHID > 0 Then
            Dim obIPH As EstimationEquipHeader = New EstimationEquipHeaderFacade(User).Retrieve(_indentPHID)
            ViewState.Add("vsAccess", "edit")
            DisplayTransactionResult(_indentPHID)
            DetailOnly(True)

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                btnConfirm.Visible = True
                btnUpdatePrice.Visible = True
            End If
        Else
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                btnConfirm.Visible = False
                btnUpdatePrice.Visible = False
            End If

            If InitialPageSession() Then
                BindIPDetail()
            End If
        End If
    End Sub

    Private Sub dtgIPDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgIPDetail.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            SetDtgIPDetailItem(e)
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

    Private Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        'If Not isHeaderValid() Then Exit Sub

        Dim edf As EstimationEquipDetailFacade = New EstimationEquipDetailFacade(User)
        Dim arl As ArrayList = _sesshelper.GetSession("sessIPDetail")
        Dim index As Integer = 0
        Dim confirmedEEDs As New ArrayList
        For Each item As DataGridItem In dtgIPDetail.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chk.Checked Then
                Dim ipd As EstimationEquipDetail = arl(index)
                ipd.ConfirmedDate = DateTime.Now
                ipd.Status = EnumEstimationEquipStatus.EstimationEquipStatusDetail.Konfirmasi_BelumOrder
                edf.Update(ipd)
                confirmedEEDs.Add(ipd)
            End If
            index += 1
        Next
        If confirmedEEDs.Count = 0 Then
            MessageBox.Show("Data sudah di konfirmasi")
            Return
        End If
        Me._sesshelper.SetSession(Me._sessConfirmedEEDs, confirmedEEDs)
        Dim ehf As EstimationEquipHeaderFacade = New EstimationEquipHeaderFacade(User)
        Dim objIndentPartH As EstimationEquipHeader = GetIndentHeader()
        Dim totalConfirmed As Integer = 0
        For Each item As EstimationEquipDetail In objIndentPartH.EstimationEquipDetails
            If (item.ConfirmedDate <> System.Data.SqlTypes.SqlDateTime.MinValue.Value) Then
                totalConfirmed += 1
            End If
        Next

        Dim oldStatus As Integer = objIndentPartH.Status

        If (totalConfirmed = 0) Then
            MessageBox.Show("Belum ada konfirmasi yang dipilih")
            Exit Sub
        ElseIf (totalConfirmed < CType(_sesshelper.GetSession("originalData"), ArrayList).Count) Then ' dtgIPDetail.Items.Count) Then 
            objIndentPartH.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Konfirmasi_Sebagian
        ElseIf (totalConfirmed = CType(_sesshelper.GetSession("originalData"), ArrayList).Count) Then ' dtgIPDetail.Items.Count) Then
            objIndentPartH.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Selesai
        End If

        ehf.Update(objIndentPartH)
        ehf.RecordStatusChangeHistory(objIndentPartH, oldStatus)
        _sesshelper.SetSession("sessIPDetail", arl)
        _sesshelper.SetSession("sessIPHeader", objIndentPartH)
        SendEmail(objIndentPartH)

        If objIndentPartH.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
            objDealer = _sesshelper.GetSession("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                lblStatusValue.Text = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru.ToString()
            End If
        Else
            lblStatusValue.Text = objIndentPartH.StatusDesc
        End If
        BindIPDetail()
        MessageBox.Show("Konfirmasi berhasil")
    End Sub

    Private Sub btnUpdatePrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdatePrice.Click
        'If Not isHeaderValid() Then Exit Sub

        'Start  :RemainModule-IndentPart:update From SAP:by dna:2001201
        'Dim arlItem As ArrayList = _sesshelper.GetSession("sessIPDetail")
        'For Each item As EstimationEquipDetail In arlItem
        '    item.Harga = item.SparePartMaster.RetalPrice
        'Next
        '_sesshelper.SetSession("sessIPDetail", arlItem)
        'UpdatePrice()
        UpdatePrice2()
        'End    :RemainModule-IndentPart:update From SAP:by dna:2001201
        btnConfirm.Enabled = True
        Dim objIndentPartH As EstimationEquipHeader = GetIndentHeader()
        'SendEmail(objIndentPartH)
        BindIPDetail()
        MessageBox.Show("Update harga berhasil")
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../IndentPartEquipment/FrmListEstimationEquip.aspx?isBack=True")
    End Sub

#End Region

#Region "private function"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanPartIndentPartCreate_Privilege) Then
            'Server.Transfer("../FrmAccessDenied.aspx?modulName=Estimation Equip Part - Pengajuan")
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
        If objIndentPartH.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
            objDealer = _sesshelper.GetSession("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                lblStatusValue.Text = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru.ToString()
            End If
        Else
            lblStatusValue.Text = objIndentPartH.StatusDesc
        End If

        Dim objUserInfo As UserInfo = CType(_sesshelper.GetSession("LOGINUSERINFO"), UserInfo)

        icOrderDate.Value = objIndentPartH.CreatedTime  'String.Format("{0:dd/MM/yyyy}", objIndentPartH.PODate)
        _arlIPDetail = objIndentPartH.EstimationEquipDetails
        _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        _sesshelper.SetSession("sessIPHeader", objIndentPartH)
        ViewState.Add("vsAccess", "edit")

        If (objIndentPartH.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Selesai) Then
            btnUpdatePrice.Enabled = False
            dtgIPDetail.Columns(1).Visible = False
        End If

        BindIPDetail()
    End Function

    Private Function GetPartNumberString(ByVal strSource As String) As String
        Dim str As String
        Dim strSplit() As String
        Dim strRsl As String = ""
        Dim IdxLine As Integer = -1
        Dim i As Integer

        str = strSource ' Convert.ToInt32(Convert.ToChar(txtTest.Text.Substring(6, 1)))
        str &= " " & Convert.ToString(Convert.ToChar(10))
        For Each c As Char In str
            If Convert.ToInt32(c) = 10 Then
                strRsl &= "#"
            Else
                strRsl &= Convert.ToString(c)
            End If
        Next
        'strRsl &= "#"
        strSplit = strRsl.Split("#")
        strRsl = ""
        For i = 0 To strSplit.Length - 1
            If strSplit(i).Trim.Length > 1 Then
                strRsl &= strSplit(i).Substring(0, strSplit(i).Length - 1) & ";"
            End If
        Next
        Return strRsl
        'strSplit = strRsl.Split(";")
        'For i = 0 To strSplit.Length - 1
        '    str = strSplit(i)
        'Next
    End Function
    Private Function FilterByPartNumber(ByVal arlIPD As ArrayList) As ArrayList
        'EstimationEquipDetail
        If txtPartNumber.Text.Trim <> "" Then
            Dim strOri As String = GetPartNumberString(txtPartNumber.Text)
            Dim str() As String
            Dim arlResult As New ArrayList

            str = strOri.Split(";")
            Dim IsExisting As Boolean
            For Each oEED As EstimationEquipDetail In arlIPD
                IsExisting = False
                For i As Integer = 0 To str.Length - 1
                    If oEED.SparePartMaster.PartNumber.Trim.ToUpper = str(i).Trim.ToUpper Then
                        IsExisting = True
                        Exit For
                    End If
                Next
                If IsExisting Then
                    arlResult.Add(oEED)
                End If
            Next
            Return arlResult
            Exit Function
        End If
        Return arlIPD
    End Function

    Private Sub BindIPDetail()
        If CType(viewstate.Item("FirstLoaded"), String) = "1" Then
            _sesshelper.SetSession("originalData", CType(Session("sessIPDetail"), ArrayList))
        End If
        _arlIPDetail = FilterByPartNumber(CType(_sesshelper.GetSession("originalData"), ArrayList))
        _sesshelper.SetSession("sessIPDetail", _arlIPDetail)
        dtgIPDetail.DataSource = CType(Session("sessIPDetail"), ArrayList)
        dtgIPDetail.DataBind()
        calculateTotalAmount()

        '_arlIPDetail = CType(Session("sessIPDetail"), ArrayList)
        '_arlIPDetail = FilterByPartNumber(_arlIPDetail)
        'dtgIPDetail.DataSource = _arlIPDetail ' CType(Session("sessIPDetail"), ArrayList)
        'dtgIPDetail.DataBind()
        'calculateTotalAmount()
    End Sub

    Private Sub DetailOnly(ByVal bval As Boolean)
        If Not _state Then
            ' dari Dealer
            If _indentPHID <> 0 Then
            End If
        Else
            ' dari KTB
            dtgIPDetail.ShowFooter = False
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

    Private Sub SetDtgIPDetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objIndentPartDetail As EstimationEquipDetail = e.Item.DataItem
        Dim tmpStr As String = ""
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(2).Text = (e.Item.ItemIndex + 1).ToString


        Dim _iDDetail As EstimationEquipDetail = CType(e.Item.DataItem, EstimationEquipDetail)

        Dim lblTotalAmount As Label = CType(e.Item.FindControl("lblJumlah"), Label)

        Dim price As Decimal = objIndentPartDetail.Harga
        Dim qty As Decimal = objIndentPartDetail.EstimationUnit
        Dim Total As Decimal = price * qty
        Dim totalAmount As Decimal = Total - ((objIndentPartDetail.Discount / 100) * Total)
        lblTotalAmount.Text = totalAmount.ToString("#,##0")

        objDealer = _sesshelper.GetSession("DEALER")

        If (objIndentPartDetail.ConfirmedDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value) Then
            e.Item.FindControl("lblConfirmedDate").Visible = False
            e.Item.FindControl("chkItemChecked").Visible = True
        Else
            e.Item.FindControl("lblConfirmedDate").Visible = True
            e.Item.FindControl("chkItemChecked").Visible = False
            CType(e.Item.FindControl("lblConfirmedDate"), Label).Text = objIndentPartDetail.ConfirmedDate.ToString("dd MMM yyyy")
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
        Dim i As Integer
        Dim bResult As Boolean = False
        For i = 0 To arlIPDetail.Count - 1
            If CType(arlIPDetail(i), EstimationEquipDetail).SparePartMaster.PartNumber.Trim().ToUpper() = partNumber.Trim().ToUpper() AndAlso nIndeks <> i Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
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

    Private Sub calculateTotalAmount()
        Dim arldetail As ArrayList = _sesshelper.GetSession("sessIPDetail")
        Dim decTotal As Decimal = 0
        For Each objTmpDetail As EstimationEquipDetail In arldetail
            decTotal += (objTmpDetail.EstimationUnit * objTmpDetail.Harga)
        Next
        lblTotalAmount.Text = "Rp. " + decTotal.ToString("#,##0")
    End Sub

    'Private Function isHeaderValid() As Boolean
    '    Dim objIndentPartH As EstimationEquipHeader = GetIndentHeader()
    '    If (DateTime.Now.Day - objIndentPartH.CreatedTime.Day) > 7 Then
    '        MessageBox.Show("Estimasi ini tidak dapat diproses karena sudah lebih dari 7 hari")
    '        Return False
    '    End If
    '    Return True
    'End Function

    Public Shared Function GetEquipDealerEmail(ByVal GroupType As Short, ByRef objD As Dealer) As String
        Dim objEUFac As EquipUserFacade = New EquipUserFacade(HttpContext.Current.User)
        Dim crtEU As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlEU As ArrayList
        Dim objEU As EquipUser
        Dim sEmail As String = ""

        crtEU.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, GroupType))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.Exact, objD.DealerCode))
        arlEU = objEUFac.Retrieve(crtEU)
        If arlEU.Count > 0 Then
            sEmail = CType(arlEU(0), EquipUser).Email
        Else
            sEmail = ""
        End If

        Return sEmail
    End Function

    Private Sub SendEmail(ByVal oEEH As EstimationEquipHeader)
        Dim dtConfirm As Date = New Date(1900, 1, 1)
        Dim Amount = 0, TotalAmount As Decimal = 0, TotalPPN = 0
        Dim TotalUnit As Integer = 0, Seq = 1
        Dim sbTable As New StringBuilder
        Dim aEEDs As ArrayList

        aEEDs = Me._sesshelper.GetSession(Me._sessConfirmedEEDs)

        For Each oEED As EstimationEquipDetail In aEEDs ' oEEH.EstimationEquipDetails
            If oEED.ConfirmedDate.ToString("yyyyMMdd") = Now.ToString("yyyyMMdd") Then
                Amount = (oEED.EstimationUnit * oEED.Harga) - (oEED.Discount / 100 * (oEED.EstimationUnit * oEED.Harga))
                TotalAmount += Amount
                TotalUnit += oEED.EstimationUnit
                dtConfirm = oEED.ConfirmedDate
                sbTable.Append("<tr>" & _
                    "<td>" & Seq.ToString() & "</td>" & _
                    "<td>" & oEED.SparePartMaster.PartNumber & "</td>" & _
                    "<td>" & oEED.SparePartMaster.PartName & "</td>" & _
                    "<td>" & oEED.EstimationUnit.ToString() & "</td>" & _
                    "<td>" & FormatNumber(oEED.Harga, 0, TriState.False, TriState.False, TriState.True) & "</td>" & _
                    "<td>" & FormatNumber(oEED.Harga * oEED.EstimationUnit, 0, TriState.False, TriState.False, TriState.True) & "</td>" & _
                    "<td>" & oEED.SparePartMaster.AltPartNumber & "</td>" & _
                    "<td>" & FormatNumber((oEED.Discount / 100) * (oEED.EstimationUnit * oEED.Harga), 0, TriState.False, TriState.False, TriState.True) & "</td>" & _
                    "<td>" & FormatNumber(Amount, 0, TriState.False, TriState.False, TriState.True) & "</td>" & _
                    "</tr>")
                Seq = Seq + 1
            End If
        Next
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(dtConfirm, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
        'TotalPPN = 0.1 * TotalAmount
        TotalPPN = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=TotalAmount)

        Dim strValues() As String = {oEEH.Dealer.DealerCode _
            , oEEH.Dealer.DealerName _
            , oEEH.Dealer.City.CityName _
            , oEEH.EstimationNumber _
            , dtConfirm.ToString("dd MMM yyyy") _
            , oEEH.CreatedTime.ToString("dd MMM yyyy") _
            , FormatNumber(TotalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) _
            , FormatNumber(TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) _
            , FormatNumber(TotalAmount + TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) _
            , sbTable.ToString() _
            , FormatNumber(TotalAmount + TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) _
            , Now.ToString("dd MMM yyyy") _
            , Now.AddDays(14).ToString("dd MMM yyyy") _
            , Now.AddDays(14).ToString("dd MMM yyyy")}

        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim oEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Konfirmasi_Harga_Estimasi, EmailIndentRecipient, EmailIndentRecipientCC, oEEH.Dealer)

        oEmail.sendMail(Server.MapPath("../IndentPartEquipment/EmailTemplateKonfirmasi.htm"), EmailIndentRecipient, EmailIndentRecipientCC, "[MMKSI-DNet] Parts - Konfirmasi - " & oEEH.EstimationNumber & " - " & oEEH.Dealer.DealerCode, strValues)

    End Sub

    Private Function GenerateEmail(ByVal itemToEmail As EstimationEquipHeader) As String

        Dim TotalItem As Integer = 0
        Dim TotalAmount As Decimal = 0
        Dim TotalPPN As Decimal = 0
        Dim dtConfirm As Date = New Date(1900, 1, 1)
        For Each itemDetail As EstimationEquipDetail In itemToEmail.EstimationEquipDetails
            If Format(itemDetail.ConfirmedDate, "yyyy.MM.dd") = Format(Now, "yyyy.MM.dd") Then  ' > DateSerial(1900, 1, 1) Then
                TotalItem += itemdetail.EstimationUnit
                TotalAmount += (itemDetail.EstimationUnit * itemDetail.Harga) - (itemDetail.Discount / 100 * (itemDetail.EstimationUnit * itemDetail.Harga))
                dtConfirm = itemDetail.ConfirmedDate
            End If
        Next
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(dtConfirm, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
        'TotalPPN = 0.1 * TotalAmount
        TotalPPN = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=TotalAmount)

        Dim sb As StringBuilder = New StringBuilder("")
        sb.Append("<FONT face=Arial size=1>")
        sb.Append("<table width=700>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 align=center><b>Konfirmasi Estimasi Indent Part</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50>")
        sb.Append("Dengan hormat,&nbsp;")
        sb.Append("<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Estimasi Indent Part Yang Telah Di Estimasi ")
        sb.Append("</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=10></td>")
        sb.Append("</tr>")


        sb.Append("<tr>")
        sb.Append("<td width=120>Kode Dealer</td>")
        sb.Append("<td width=10>:</td>")
        sb.Append("<td width=350>" & itemToEmail.Dealer.DealerCode & "</td>")
        sb.Append("<td width=140>Nilai Tagihan</td>")
        sb.Append("<td width=50>:</td>")
        sb.Append("<td width=160 align=right>" & "Rp. " & FormatNumber(TotalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>")
        sb.Append("</tr>")

        sb.Append("<tr>")
        sb.Append("<td>Nama Dealer</td>")
        sb.Append("<td>:</td>")
        sb.Append("<td valign=top>" & itemToEmail.Dealer.DealerName & "</td>")
        sb.Append("<td>PPN</td>")
        sb.Append("<td>:</td>")
        sb.Append("<td align=right>Rp. " & FormatNumber(TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>")
        sb.Append("</tr>")


        sb.Append("<tr>")
        sb.Append("<td>Kota</td>")
        sb.Append("<td>:</td>")
        sb.Append("<td valign=top>" & itemToEmail.Dealer.City.CityName & "</td>")
        sb.Append("<td>Total Tagihan</td>")
        sb.Append("<td>:</td>")
        sb.Append("<td align='right'> Rp. " & FormatNumber((TotalAmount + TotalPPN), 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>")
        sb.Append("</tr>")

        sb.Append("<tr>")
        sb.Append("<td>No. Estimasi</td>")
        sb.Append("<td>:</td>")
        sb.Append("<td valign=top>" & itemToEmail.EstimationNumber & "</td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("</tr>")

        sb.Append("<tr>")
        sb.Append("<td>Tgl Konfirmasi</td>")
        sb.Append("<td>:</td>")
        sb.Append("<td>" & Format(Now, "dd/MM/yyyy") & "</td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("</tr>")

        sb.Append("<tr>")
        sb.Append("<td>Tgl Pengajuan</td>")
        sb.Append("<td>:</td>")
        sb.Append("<td>" & Format(itemToEmail.CreatedTime, "dd/MM/yyyy") & "</td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("</tr>")


        sb.Append("<tr>")
        sb.Append("<td height=50 colspan=6 align=center><hr><b>DAFTAR PART ORDER</b></td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("<table border=1 width=700 cellpadding=0>")
        sb.Append("<tr>")
        sb.Append("<td width=30>No</td>")
        sb.Append("<td width=100>No Barang</td>")
        sb.Append("<td width=295>Nama Barang</td>")
        sb.Append("<td width=50>Jumlah</td>")
        sb.Append("<td width=100>Harga/Pc</td>")
        sb.Append("<td width=125>Total</td>")
        sb.Append("<td width=100>No Pengganti</td>")
        sb.Append("<td width=125>Diskon</td>")
        sb.Append("<td width=125>Tagihan</td>")
        sb.Append("</tr>")




        Dim counter As Integer = 0
        Dim TmpTotal As Decimal = 0
        For Each itemDetail As EstimationEquipDetail In itemToEmail.EstimationEquipDetails
            If Format(itemDetail.ConfirmedDate, "yyyy.MM.dd") = Format(Now, "yyyy.MM.dd") Then  ' DateSerial(1900, 1, 1) Then
                counter += 1
                TmpTotal = itemDetail.Harga * itemDetail.EstimationUnit
                sb.Append("<tr>")
                sb.Append("<td>" & counter.ToString & "</td>")
                sb.Append("<td>" & itemDetail.SparePartMaster.PartNumber & "</td>")
                sb.Append("<td>" & itemDetail.SparePartMaster.PartName & "</td>")
                sb.Append("<td align=right>" & itemDetail.EstimationUnit.ToString & "</td>")
                sb.Append("<td align=right>" & FormatNumber(itemDetail.Harga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>")
                sb.Append("<td align=right>" & FormatNumber(TmpTotal, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>")
                sb.Append("<td>" & IIf(itemDetail.SparePartMaster.AltPartNumber = "", "-", itemDetail.SparePartMaster.AltPartNumber) & "</td>")
                sb.Append("<td align=right>" & itemDetail.Discount & "</td>")
                sb.Append("<td align=right>" & FormatNumber((TmpTotal - (itemDetail.Discount / 100 * TmpTotal)), 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>")
                sb.Append("</tr>")
            End If
        Next
        sb.Append("</table>")

        sb.Append("<table width=700>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td align=right>Total Amount :</td>")
        sb.Append("<td>" & FormatNumber(TotalItem, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>") 'itemToEmail.TotalAmount.ToString("#,##0") & "</td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("<br>")
        sb.Append("<table  width=700 >")
        sb.Append("<tr>")
        sb.Append("<td colspan=5>Terima kasih atas perhatian dan kerjasamanya.</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td height=30 width=30></td>")
        sb.Append("<td width=100></td>")
        sb.Append("<td width=295></td>")
        sb.Append("<td width=50></td>")
        sb.Append("<td width=225 align=center></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td align=center>Jakarta, " & Today.ToString("dd MMMM yyyy") & "</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td height=60 colspan=5></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td align=center><u>" & EmailIndentSignedName & "</u></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td></td>")
        sb.Append("<td align=center>" & EmailIndentSignedJob & "</td>")
        sb.Append("</tr>")

        sb.Append("</table>")
        sb.Append("</font>")

        sb.Append("<table cellspacing=1 cellpadding=1 border=0 width='100%'>")
        'sb.Append("<tr><td>CC :</td></tr>")
        'sb.Append("<tr><td>" & EmailIndentCC1 & "</td></tr>")
        'sb.Append("<tr><td>" & EmailIndentCC2 & "</td></tr>")
        'sb.Append("<tr><td>" & EmailIndentCC3 & "</td></tr>")
        'sb.Append("<tr><td>" & EmailIndentCC4 & "</td></tr>")
        'sb.Append("<tr><td>" & EmailIndentCC5 & "</td></tr>")
        sb.Append("<tr><td>&nbsp;</td></tr>")
        sb.Append("<tr><td style='color:blue;'><b>Catatan Penting:</b></td></tr>")
        sb.Append("<tr><td style='color:blue;'>Segera lakukan Pengajuan Order di DNET, dengan status <u>kirim</u> paling lambat " & Now.AddDays(14).ToString("dd MMM yyyy") & "</td></tr>")
        sb.Append("<tr><td style='color:blue;'>Untuk pembayaran melalui deposit B, kwitansi paling lambat diterima oleh MMKSI-Service Planning Dept. pada " & Now.AddDays(14).ToString("dd MMM yyyy") & "</td></tr>")
        sb.Append("</table>")

        Return sb.ToString

    End Function

    Private Sub SendEmail_Before20130910(ByVal objHeaderToEmail As EstimationEquipHeader)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        Dim valueEmail As String = GenerateEmail(objHeaderToEmail)


        objDealer = _sesshelper.GetSession("DEALER")
        'Start  :RemModule-IndentPart->Change Recipient from web.config to table EquipUser
        'SetIndentRecipient(EmailIndentRecipient, EmailIndentRecipientCC, objHeaderToEmail.Dealer)
        'End    :RemModule-IndentPart->Change Recipient from web.config to table EquipUser
        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Konfirmasi_Harga_Estimasi, EmailIndentRecipient, EmailIndentRecipientCC, objHeaderToEmail.Dealer)
        'ObjEmail.sendMail(EmailIndentRecipient, objDealer.Email, emailFrom, "KTB-DNET", Mail.MailFormat.Html, valueEmail)

        ObjEmail.sendMail(EmailIndentRecipient, objDealer.Email & IIf(EmailIndentRecipientCC.Trim = "", "", ";" & EmailIndentRecipientCC), emailFrom, "[MMKSI-DNet] Parts - Konfirmasi - " & objHeaderToEmail.EstimationNumber & " - " & objHeaderToEmail.Dealer.DealerCode, Mail.MailFormat.Html, valueEmail)

    End Sub

    Private Function SetIndentRecipient(ByRef sTo As String, ByRef sCC As String, ByRef objDealer As Dealer) As String

        Dim objEUFac As EquipUserFacade = New EquipUserFacade(User)
        Dim crtEU As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlEU As New ArrayList

        sTo = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentRecipient")
        sCC = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentRecipientCC")

        crtEU.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.Exact, objDealer.DealerCode))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, CType(EquipUser.EquipUserGroup.Konfirmasi_Harga_Estimasi, Short)))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, CType(EquipUser.EquipUserTipe.TO_SENT, Integer).ToString))
        arlEU = objEUFac.Retrieve(crtEU)
        If arlEU.Count > 0 Then sTo = CType(arlEU(0), EquipUser).Email
        If arlEU.Count > 0 Then sTo = ""
        For Each oEU As EquipUser In arlEU
            sTo &= IIf(sTo.Trim = "", "", ";") & oEU.Email
        Next

        crtEU = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.No, objDealer.DealerCode))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, CType(EquipUser.EquipUserGroup.Konfirmasi_Harga_Estimasi, Short)))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, CType(EquipUser.EquipUserTipe.TO_SENT, Integer).ToString))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "Email", MatchType.EndsWith, "@ktb.co.id"))
        arlEU = objEUFac.Retrieve(crtEU)
        For Each oEU As EquipUser In arlEU
            sTo &= IIf(sTo.Trim = "", "", ";") & oEU.Email
        Next

        crtEU = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.Exact, objDealer.DealerCode))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, CType(EquipUser.EquipUserGroup.Konfirmasi_Harga_Estimasi, Short)))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, CType(EquipUser.EquipUserTipe.CC_TO, Integer).ToString))
        arlEU = objEUFac.Retrieve(crtEU)
        If arlEU.Count > 0 Then sCC = CType(arlEU(0), EquipUser).Email
        crtEU = New CriteriaComposite(New Criteria(GetType(EquipUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "UserName", MatchType.No, objDealer.DealerCode))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "GroupType", MatchType.Exact, CType(EquipUser.EquipUserGroup.Konfirmasi_Harga_Estimasi, Short)))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "Tipe", MatchType.Exact, CType(EquipUser.EquipUserTipe.CC_TO, Integer).ToString))
        crtEU.opAnd(New Criteria(GetType(EquipUser), "Email", MatchType.[Partial], "@ktb.co.id"))
        arlEU = objEUFac.Retrieve(crtEU)
        For Each oEU As EquipUser In arlEU
            sCC &= IIf(sCC.Trim = "", "", ";") & oEU.Email
        Next
    End Function

    'Private Sub SendSMS(ByVal objHeader As EstimationEquipHeader)
    '    objDealer = _sesshelper.GetSession("DEALER")

    'End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindIPDetail()
    End Sub

    Private Sub btnSimpanRemark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpanRemark.Click
        Dim IsValidToSave As Boolean = False
        Dim arlIPDetail As ArrayList = CType(Me._sesshelper.GetSession("sessIPDetail"), ArrayList)

        For Each di As DataGridItem In dtgIPDetail.Items
            Dim chkItemChecked As CheckBox = di.FindControl("chkItemChecked")
            If chkItemChecked.Checked Then
                IsValidToSave = True
                Exit For
            End If
        Next
        If IsValidToSave Then
            Dim oEEDFac As EstimationEquipDetailFacade = New EstimationEquipDetailFacade(User)
            Dim oEED As EstimationEquipDetail

            For Each di As DataGridItem In dtgIPDetail.Items
                Dim chkItemChecked As CheckBox = di.FindControl("chkItemChecked")
                Dim txtRemark As TextBox = di.FindControl("txtRemark")
                Dim ID As Integer = CType(di.Cells(0).Text, Integer)
                If chkItemChecked.Checked Then
                    oEED = oEEDFac.Retrieve(ID)
                    oEED.Remark = txtRemark.Text.Trim
                    oEEDFac.Update(oEED)

                    arlIPDetail(di.ItemIndex) = oEED
                End If
            Next
            _sesshelper.SetSession("arlIPDetail", arlIPDetail)
        End If

    End Sub
    'Start  :RemainModule-IndentPart:update From SAP:by dna:2001201
    Private Sub UpdatePrice()
        'Dim arlEED As ArrayList = _sesshelper.GetSession("sessIPDetail")
        'Dim arlEEDRsl As New ArrayList
        'Dim arlSPM As ArrayList
        'Dim strCon As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionString")
        'Dim objSAPDnet As SAPDNet = New SAPDNet(strCon)
        'Dim arlSAPRsl As ArrayList
        'Dim tblZSPST0028_01 As ZSPST0028_01Table
        'Dim objSPM As SparePartMaster

        'For Each objEED As EstimationEquipDetail In arlEED
        '    arlSPM = New ArrayList
        '    objEED.SparePartMaster.PartNumber = "MW028255"
        '    objEED.EstimationEquipHeader.Dealer.DealerCode = "100001"
        '    arlSPM.Add(objEED.SparePartMaster)
        '    arlSAPRsl = objSAPDnet.CheckMaterial(arlSPM)
        '    If arlSAPRsl.Count = 0 Then
        '        objEED.Harga = objEED.SparePartMaster.RetalPrice
        '    Else
        '        tblZSPST0028_01 = arlSAPRsl(0)
        '        objSPM = PopulateSparePartMasterList(tblZSPST0028_01(0))
        '        objEED.Harga = objSPM.RetalPrice
        '    End If
        '    arlEEDRsl.Add(objEED)
        'Next
        '_sesshelper.SetSession("sessIPDetail", arlEEDRsl)

    End Sub

    Private Sub UpdatePrice2()
        Dim arlEED As ArrayList = _sesshelper.GetSession("sessIPDetail")
        Dim arlEEDRsl As New ArrayList
        Dim arlSPM As ArrayList
        Dim strCon As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionString")
        Dim objSAPDnet As SAPDNet = New SAPDNet(strCon)
        Dim arlSAPRsl As ArrayList
        Dim objSPM As SparePartMaster
        'Dim objIOT As ZKTB_INQ_OUTTable
        Dim objIO As ZKTB_INQ_OUT
        Dim objEEDFac As EstimationEquipDetailFacade = New EstimationEquipDetailFacade(User)

        For Each objEED As EstimationEquipDetail In arlEED
            arlSPM = New ArrayList
            arlSPM.Add(objEED)
            arlSAPRsl = objSAPDnet.GetMaterialPrice(arlSPM)
            If arlSAPRsl.Count = 0 Then
                objEED.Harga = objEED.SparePartMaster.RetalPrice
            Else
                'objIOT = arlSAPRsl(1)
                If arlSAPRsl.Count > 0 Then
                    objIO = arlSAPRsl(0)
                    objEED.Harga = objIO.Retail_Price
                    objEED.Discount = Math.Abs(objIO.Discount)
                Else
                    objEED.Harga = objEED.SparePartMaster.RetalPrice
                End If
            End If
            objEEDFac.Update(objEED)
            arlEEDRsl.Add(objEED)
        Next
        _sesshelper.SetSession("sessIPDetail", arlEEDRsl)
    End Sub
    'End    :RemainModule-IndentPart:update From SAP:by dna:2001201

#End Region

End Class

