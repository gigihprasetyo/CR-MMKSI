#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Reflection
#End Region

Public Class FrmEntrySparePartPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents icOrderDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dtgPODetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents hid_f As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotPOAmount As System.Web.UI.WebControls.Label
    Protected WithEvents hid_History As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnBack1 As System.Web.UI.WebControls.Button
    Protected WithEvents hid_back As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents chkRequestForCanceled As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnBack As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblReqbatalKTB As System.Web.UI.WebControls.Label
    Protected WithEvents btnDnload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents trPQRNo As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents txtPQRNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchPQRNo As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private ubahPrivilege As Boolean = SecurityProvider.Authorize(Context.User, SR.UbahDetailPesanan_Privilege)
    Private hapusPrivilege As Boolean = SecurityProvider.Authorize(Context.User, SR.HapusDetailPesanan_Privilege)
    Private tambahPrivilege As Boolean = SecurityProvider.Authorize(Context.User, SR.TambahDetailPesanan_Privilege)

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private _arlPODetail As ArrayList = New ArrayList
    Private _sesshelper As SessionHelper = New SessionHelper
    Private _sparePartPOTypeTOP As SparePartPOTypeTOP
#End Region

#Region "Custom Method"

    Private Function ConvertDayOfWeek(ByVal day As String) As Integer
        If day = "Monday" Then
        ElseIf day = "Monday" Then

        End If
    End Function

    Private Function isOrderRestricted() As Boolean
        Dim ObjDealer As Dealer = CType(Session("sessDealer"), Dealer)
        Dim _OrderRestrictionFacade As OrderRestrictionFacade = New OrderRestrictionFacade(User)
        Dim arrList As ArrayList = New ArrayList
        Dim tDateFrom As DateTime
        Dim tDateTo As DateTime
        'Dim tDateNow As DateTime = New DateTime(2006, 1, 1, 0, 0, 0)
        Dim sTimeFrom() As String
        Dim sTimeTo() As String
        Dim sHour As Integer
        Dim eHour As Integer
        Dim sMinute As Integer
        Dim eMinute As Integer
        Dim sSecond As Integer
        Dim eSecond As Integer
        'tDateNow.AddHours(CType(DateTime.Now.TimeOfDay.Hours, Double))
        'tDateNow.AddMinutes(CType(DateTime.Now.TimeOfDay.Minutes, Double))
        'tDateNow.AddSeconds(CType(DateTime.Now.TimeOfDay.Seconds, Double))

        Dim criterias As New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(OrderRestriction), "IsActive", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(OrderRestriction), "Dealer.DealerCode", MatchType.Exact, ObjDealer.DealerCode))
        criterias.opAnd(New Criteria(GetType(OrderRestriction), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
        'criterias.opAnd(New Criteria(GetType(OrderRestriction), "DateFrom", MatchType.LesserOrEqual, Format(icOrderDate.Value, "yyyy/MM/dd")))
        'criterias.opAnd(New Criteria(GetType(OrderRestriction), "DateTo", MatchType.GreaterOrEqual, Format(icOrderDate.Value, "yyyy/MM/dd")))
        criterias.opAnd(New Criteria(GetType(OrderRestriction), "DateFrom", MatchType.LesserOrEqual, Format(DateTime.Now, "yyyy/MM/dd")))
        criterias.opAnd(New Criteria(GetType(OrderRestriction), "DateTo", MatchType.GreaterOrEqual, Format(DateTime.Now, "yyyy/MM/dd")))

        arrList = _OrderRestrictionFacade.Retrieve(criterias)
        If arrList.Count > 0 Then
            Return True
        End If

        criterias = New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(OrderRestriction), "IsActive", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(OrderRestriction), "Dealer.DealerCode", MatchType.Exact, ObjDealer.DealerCode))
        criterias.opAnd(New Criteria(GetType(OrderRestriction), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
        criterias.opAnd(New Criteria(GetType(OrderRestriction), "Days", MatchType.Exact, CType(DateTime.Now.DayOfWeek, Integer)))

        arrList = _OrderRestrictionFacade.Retrieve(criterias)

        If arrList.Count > 0 Then
            For i As Integer = 0 To arrList.Count - 1
                sTimeFrom = CType(arrList(i), OrderRestriction).TimeFrom.Split(":")
                tDateFrom = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, sTimeFrom(0), sTimeFrom(1), sTimeFrom(2))

                'tDateFrom.AddHours(CType(sTimeFrom(0), Double))
                'tDateFrom.AddMinutes(CType(sTimeFrom(1), Double))
                'tDateFrom.AddSeconds(CType(sTimeFrom(2), Double))

                sTimeTo = CType(arrList(i), OrderRestriction).TimeTO.Split(":")
                tDateTo = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, sTimeTo(0), sTimeTo(1), sTimeTo(2))
                'tDateTo.AddHours(CType(sTimeTo(0), Double))
                'tDateTo.AddMinutes(CType(sTimeTo(1), Double))
                'tDateTo.AddSeconds(CType(sTimeTo(2), Double))

                If tDateFrom <= DateTime.Now And DateTime.Now <= tDateTo Then
                    Return True
                End If
            Next i
        End If
        Return False
    End Function

    Private Sub InitialEditPage(ByVal nPOID As Integer)
        btnBack.Visible = True
        If Not IsNothing(Session("DEALER")) Then
            ViewState.Add("vsAccess", "edit")
            ViewState.Add("vsSave", "new")
            BindOrderType()
            DisplayTransactionResult(nPOID)
        End If
    End Sub

    Private Sub BindDdlPaymentType()
        _sesshelper.SetSession("sessDealer", Session("DEALER"))
        Dim dlr As Dealer = CType(Session("sessDealer"), Dealer)
        Dim spCa As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccountFacade(User).RetrieveByDealerCode(dlr.DealerCode)

        If spCa.Status Then
            Dim oTopCA As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
            If Not IsNothing(oTopCA) Then
                Dim listOfPayments As ArrayList = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
                ddlTermOfPayment.Items.Clear()
                ddlTermOfPayment.ClearSelection()
                ddlTermOfPayment.SelectedValue = Nothing
                ddlTermOfPayment.DataSource = listOfPayments
                ddlTermOfPayment.DataValueField = "ID"
                ddlTermOfPayment.DataTextField = "Description"
                ddlTermOfPayment.DataBind()

                _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
                If _sparePartPOTypeTOP.IsTOP Then
                    ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
                Else
                    If Not IsNothing(Request.QueryString("poid")) Then
                        ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
                    Else
                        ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
                    End If
                End If
            End If
            ViewState.Add("AllowTOP", False)
        Else
            ViewState.Add("AllowTOP", True)
            ddlTermOfPayment.ClearSelection()
            If Not IsNothing(_sparePartPOTypeTOP) Then
                If Not IsNothing(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP) Then
                    ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
                Else
                    ddlTermOfPayment.Items.Insert(0, New ListItem("COD", "1"))
                End If
            Else
                ddlTermOfPayment.Items.Insert(0, New ListItem("COD", "1"))
            End If
            ddlTermOfPayment.Enabled = False
        End If
    End Sub

    Private Sub BindOrderType()
        ddlOrderType.Items.Clear()
        ''Handle Indent Edit/View
        If Val(Request.QueryString("poID")) > 0 Then
            Dim objPO As SparePartPO = New SparePartPOFacade(User).Retrieve(CType(Request.QueryString("poID"), Integer))
            If objPO.OrderType = "Z" Or objPO.OrderType = "Y" Then
                For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer
                    ddlOrderType.Items.Insert(0, New ListItem(liOrderType.Text, liOrderType.Value))
                Next
            Else
                For Each liOrderType As ListItem In LookUp.ArraySPOrderType
                    ddlOrderType.Items.Insert(0, New ListItem(liOrderType.Text, liOrderType.Value))
                Next
            End If
        Else
            For Each liOrderType As ListItem In LookUp.ArraySPOrderType
                ddlOrderType.Items.Insert(0, New ListItem(liOrderType.Text, liOrderType.Value))
            Next
        End If
        ddlOrderType.DataBind()
    End Sub

    Private Sub ControlsScriptInjection()
        btnCancel.Attributes.Add("OnClick", "return confirm('" & SR.CancelConfirmation & "');")
        btnSubmit.Attributes.Add("OnClick", "return confirm('" & SR.SubmitConfirmation & "');")
        ddlTermOfPayment.Attributes.Add("OnChange", "SetKirimDisable();")
        txtPONumber.Attributes.Add("OnChange", "SetKirimDisable();")
        chkRequestForCanceled.Attributes.Add("OnClick", "if ( ! confirm('" & CType(ViewState("messCancelKTB"), String) & "')) return false;")
    End Sub
    Private Function InitialPageSession() As Boolean 'ByVal nID As Integer)
        btnBack.Visible = False
        If Not IsNothing(Session("DEALER")) Then
            ViewState.Add("vsAccess", "insert")
            ViewState.Add("vsSave", "new")
            _sesshelper.SetSession("sessDealer", Session("DEALER")) 'New DealerFacade(User).Retrieve(nID))
            If IsNothing(Session("sessPODetail")) Then
                _sesshelper.SetSession("sessPODetail", _arlPODetail)
            Else
                _arlPODetail = CType(Session("sessPODetail"), ArrayList)
            End If
            BindOrderType()
            ddlOrderType.SelectedValue = "E"
            _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
            lblDealerCode.Text = CType(Session("sessDealer"), Dealer).DealerCode
            lblDealerName.Text = CType(Session("sessDealer"), Dealer).DealerName
            lblDealerTerm.Text = CType(Session("sessDealer"), Dealer).SearchTerm2
            If Not IsNothing(Session("sessPOHeader")) Then
                DisplayTransactionResult(CType(Session("sessPOHeader"), SparePartPO).ID)
            End If
            Return True
        End If
        Return False
    End Function

    Private Sub BindPODetail()
        _arlPODetail = CType(Session("sessPODetail"), ArrayList)
        lblTotPOAmount.Text = String.Format("{0:#,##0}", CalculatePOAmount(_arlPODetail))
        dtgPODetail.DataSource = _arlPODetail
        dtgPODetail.DataBind()
        If _arlPODetail.Count > 0 Then
            txtPQRNo.Enabled = False
            lblSearchPQRNo.Visible = False
        Else
            txtPQRNo.Enabled = True
            lblSearchPQRNo.Visible = True
        End If
    End Sub

    Private Function CalculatePOAmount(ByVal arlPODetail As ArrayList) As Decimal
        Dim nPOAmount As Decimal = 0
        For Each objPODetail As SparePartPODetail In arlPODetail
            nPOAmount = nPOAmount + objPODetail.Amount
        Next
        Return (nPOAmount)
    End Function

    Private Function PartIsExist(ByVal partNumber As String, ByVal arlPODetail As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each poDetail As SparePartPODetail In arlPODetail
            If poDetail.SparePartMaster.PartNumber.Trim().ToUpper() = partNumber.Trim().ToUpper() Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function
    Private Function PartIsExist(ByVal partNumber As String, ByVal arlPODetail As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim i As Integer
        Dim bResult As Boolean = False
        For i = 0 To arlPODetail.Count - 1
            If CType(arlPODetail(i), SparePartPODetail).SparePartMaster.PartNumber.Trim().ToUpper() = partNumber.Trim().ToUpper() AndAlso nIndeks <> i Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Private Function GetPOHeader() As SparePartPO
        Dim objPO As SparePartPO
        If IsNothing(Session("sessPOHeader")) Then
            objPO = New SparePartPO
            objPO.PODate = icOrderDate.Value
            objPO.Dealer = CType(Session("sessDealer"), Dealer)
            objPO.OrderType = ddlOrderType.SelectedValue

            If ddlOrderType.SelectedValue = "P" Then    ''Jika type Ordernya dari Emergency PQR 
                Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
                If Not IsNothing(objPQRHeader) Then
                    If objPQRHeader.ID > 0 Then
                        objPO.PQRHeader = objPQRHeader
                    End If
                End If
            End If

            If ddlTermOfPayment.SelectedValue <> "" Then
                objPO.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(ddlTermOfPayment.SelectedValue, Integer))
            Else
                objPO.TermOfPayment = Nothing
            End If

            _sesshelper.SetSession("sessPOHeader", objPO)
        Else
            objPO = CType(Session("sessPOHeader"), SparePartPO)
        End If
        Return objPO
    End Function

    Private Sub ViewMode(ByVal bMode As Boolean)
        If tambahPrivilege Then
            dtgPODetail.ShowFooter = bMode
        Else
            dtgPODetail.ShowFooter = False
        End If

        If (ubahPrivilege OrElse hapusPrivilege OrElse tambahPrivilege) Then
            dtgPODetail.Columns(6).Visible = bMode
        Else
            dtgPODetail.Columns(6).Visible = False
        End If

        btnSave.Enabled = bMode
        btnPrint.Enabled = Not bMode
    End Sub

    Private Sub ViewMode2(ByVal bMode As Boolean)
        If tambahPrivilege Then
            dtgPODetail.ShowFooter = bMode
        Else
            dtgPODetail.ShowFooter = False
        End If

        If (ubahPrivilege OrElse hapusPrivilege) Then
            dtgPODetail.Columns(6).Visible = bMode
        Else
            dtgPODetail.Columns(6).Visible = False
        End If

        btnSave.Enabled = bMode
        ' btnPrint.Enabled = Not bMode
        btnSubmit.Enabled = bMode

    End Sub

    Private Function DisplayTransactionResult(ByVal nID As Integer)
        Dim stt As Boolean = False
        Dim objPO As SparePartPO = New SparePartPOFacade(User).Retrieve(nID)
        ddlOrderType.Enabled = False
        icOrderDate.Enabled = False
        ViewState("messCancelKTB") = "Yakin Pembatalan PESANAN akan dibatalkan ?"
        If objPO.ProcessCode = "" Then 'PO status is NEW
            stt = (objPO.PONumber.Substring(5, 3) = "162" OrElse objPO.PONumber.Substring(5, 3) = "161")
            btnCancel.Enabled = True
            btnSubmit.Enabled = True

        ElseIf objPO.ProcessCode = "C" Then 'PO status is Canceled
            btnSubmit.Enabled = False
            btnCancel.Enabled = False
        Else
            If objPO.ProcessCode = "S" Then  'PO status is Submited
                btnSubmit.Enabled = Left(objPO.PONumber, 1) = "R"
                stt = objPO.PONumber.Substring(5, 3) = "161"

                '---Request from BA 060117
                'If objPO.CancelRequestBy = String.Empty OrElse Left(objPO.CancelRequestBy, 1) = "-" Then
                '    If objPO.CancelRequestBy <> String.Empty Then
                '        chkRequestForCanceled.Text = "Permintaan Pembatalan dibatalkan oleh :" & objPO.CancelRequestBy.Trim.Substring(7, objPO.CancelRequestBy.Trim.Length - 7).ToUpper
                '    End If
                '    ViewState("messCancelKTB") = "Yakin PESANAN ini akan dibatalkan ?"
                '    chkRequestForCanceled.Checked = False
                'Else
                '    chkRequestForCanceled.Checked = True
                '    chkRequestForCanceled.Text = "Permintaan Pembatalan oleh :" & objPO.CancelRequestBy.Trim.Substring(6, objPO.CancelRequestBy.Trim.Length - 6).ToUpper
                'End If

                'chkRequestForCanceled.Visible = True
            ElseIf objPO.ProcessCode = "X" Then 'PO status is canceled by KTB
                'If objPO.CancelRequestBy.Trim.Length > 6 Then
                '    lblReqbatalKTB.Text = "Pesanan sudah dibatalkan KTB atas permintaan " & objPO.CancelRequestBy.Trim.Substring(6, objPO.CancelRequestBy.Trim.Length - 6).ToUpper
                'End If
                btnSubmit.Enabled = False
            End If

            btnCancel.Enabled = False


        End If

        'End If
        ViewMode(stt)

        If (objPO.ProcessCode = "S") Then
            ViewMode2(False)
        End If


        txtPONumber.Text = objPO.PONumber
        lblDealerCode.Text = objPO.Dealer.DealerCode
        lblDealerName.Text = objPO.Dealer.DealerName
        lblDealerTerm.Text = objPO.Dealer.SearchTerm2
        icOrderDate.Value = objPO.PODate 'String.Format("{0:dd/MM/yyyy}", objPO.PODate)
        ddlOrderType.SelectedValue = objPO.OrderType
        ddlOrderType_SelectedIndexChanged(Nothing, Nothing)
        If objPO.OrderType = "P" Then
            If Not IsNothing(objPO.PQRHeader) Then
                txtPQRNo.Text = objPO.PQRHeader.PQRNo
            End If
        End If
        _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
        If Not IsNothing(objPO.TermOfPayment) Then
            Dim optionExist As ListItem = ddlTermOfPayment.Items.FindByValue(objPO.TermOfPayment.ID)
            If Not IsNothing(optionExist) Then
                ddlTermOfPayment.SelectedValue = objPO.TermOfPayment.ID
            End If
        End If

        _arlPODetail = objPO.SparePartPODetails
        _sesshelper.SetSession("sessPODetail", _arlPODetail)
        _sesshelper.SetSession("sessPOHeader", objPO)
        ViewState.Add("vsAccess", "edit")
        BindPODetail()
        If _sparePartPOTypeTOP.IsTOP Then
            ddlTermOfPayment.Enabled = True
        End If
        If ValidateTOP(_sparePartPOTypeTOP.ID) And ddlTermOfPayment.SelectedValue = "1" Then
            ddlTermOfPayment.Enabled = False
        End If
    End Function

    Private Function EditPO() As Integer
        Dim ObjPO As SparePartPO = CType(Session("sessPOHeader"), SparePartPO)

        If ddlTermOfPayment.SelectedValue <> "" Then
            ObjPO.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(ddlTermOfPayment.SelectedValue, Integer))
        Else
            ObjPO.TermOfPayment = Nothing
        End If

        Return New SparePartPOFacade(User).UpdateSparePartPO(ObjPO, CType(Session("sessPODetail"), ArrayList))
    End Function

    Private Function InsertNewPO() As Integer
        Dim ObjPO As SparePartPO = GetPOHeader()
        Return New SparePartPOFacade(User).InsertSparePartPO(ObjPO, CType(Session("sessPODetail"), ArrayList))
    End Function

    Private Sub MergePODetail(ByVal nID As Integer)
        Dim objPO As SparePartPO = New SparePartPOFacade(User).Retrieve(nID)
        _sesshelper.SetSession("sessPOHeader", objPO)
        _arlPODetail = CType(Session("sessPODetail"), ArrayList)
        For Each objPODetailOrig As SparePartPODetail In objPO.SparePartPODetails
            objPODetailOrig.RowStatus = DBRowStatus.Deleted
        Next

        Dim found As Boolean
        For Each objPODetail As SparePartPODetail In _arlPODetail
            found = False
            For Each objPODetailOrig As SparePartPODetail In objPO.SparePartPODetails
                If objPODetail.SparePartMaster.PartNumber.Trim().ToUpper() = objPODetailOrig.SparePartMaster.PartNumber.Trim().ToUpper() Then
                    objPODetailOrig.RowStatus = DBRowStatus.Active
                    objPODetailOrig.Quantity = objPODetail.Quantity
                    objPODetailOrig.RetailPrice = objPODetail.RetailPrice
                    found = True
                    Exit For
                End If
                found = False
            Next
            If Not found Then
                objPO.SparePartPODetails.Add(objPODetail)
            End If
        Next
        _sesshelper.SetSession("sessPODetail", objPO.SparePartPODetails)

    End Sub

    Private Sub RemoveAllSession()
        _sesshelper.RemoveSession("sessPOHeader")
        _sesshelper.RemoveSession("sessPODetail")
        _sesshelper.RemoveSession("isRefresh")
    End Sub

    Private Sub CreateFolder(ByVal folderName As String)
        Dim dirInfo As DirectoryInfo = New DirectoryInfo(folderName)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub

    Private Sub CreateTextFileForKTB()
        Dim FOLDER_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("DNetServerFolder") & txtPONumber.Text.Substring(1, 4)
        Dim strExt As String = String.Empty
        'Emergency (E)
        'Emergency Without Backorder (X) 
        'Emergency PQR (P)

        If ddlOrderType.SelectedValue = "E" OrElse
            ddlOrderType.SelectedValue = "X" OrElse
            ddlOrderType.SelectedValue = "P" Then
            strExt = ".EOD"
        Else
            strExt = ".DAT"
        End If

        Dim FILE_NAME As String = FOLDER_NAME + "\" + txtPONumber.Text + strExt
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _sapServer)
        Dim succes As Boolean = False
        Dim msg As String = String.Empty
        Dim topCode As String
        Try
            succes = imp.Start()
            If succes Then
                Dim objPO As SparePartPO = CType(Session("sessPOHeader"), SparePartPO)
                Dim OldCode As String
                Me.checkCurrentStatus(objPO)
                objPO = New SparePartPOFacade(User).Retrieve(objPO.ID)
                If Not IsNothing(objPO.TermOfPayment) And (objPO.OrderType = "R" Or objPO.OrderType = "I" Or objPO.OrderType = "Z") Then
                    topCode = objPO.TermOfPayment.TermOfPaymentCode
                ElseIf objPO.OrderType = "R" Or objPO.OrderType = "I" Or objPO.OrderType = "Z" Then
                    _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
                    objPO.TermOfPayment = _sparePartPOTypeTOP.TermOfPaymentIDNotTOP
                    topCode = _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.TermOfPaymentCode
                End If
                OldCode = objPO.ProcessCode
                objPO.ProcessCode = "S"
                objPO.SentPODate = Date.Today
                Dim nResult As Integer = New SparePartPOFacade(User).UpdateSPPOProcessCode(objPO)
                If nResult <> -1 Then
                    Dim oDHFac As New DataHistoryFacade(User)
                    oDHFac.LogSparePartPO(objPO.ID, OldCode, objPO.ProcessCode)
                    CreateFolder(FOLDER_NAME)
                    If File.Exists(FILE_NAME) Then
                        File.Delete(FILE_NAME)
                    End If

                    'Order Type R dan Z tidak dibuatkan disini, tapi untuk tipe lain.
                    'If Not (objPO.OrderType.ToLower().Equals("r") OrElse objPO.OrderType.ToLower().Equals("z")) Then
                    If Not (objPO.OrderType.ToLower().Equals("z")) Then
                        Dim fs As FileStream = New FileStream(FILE_NAME, FileMode.CreateNew)
                        Dim w As StreamWriter = New StreamWriter(fs)
                        WritePOHeaderToFile(w, topCode)
                        WritePODetailToFile(w)
                        w.Close()
                        fs.Close()
                    End If

                    imp.StopImpersonate()
                    imp = Nothing
                    msg = ChangeSPPOStatus("S")
                    MessageBox.Show(msg)
                Else
                    MessageBox.Show("Proses tidak berhasil coba beberapa saat lagi.")
                End If

            Else
                MessageBox.Show("Gagal Login ke SAP Server.")
            End If
        Catch ex As Exception
            MessageBox.Show(SR.DataSendFail)
        End Try
    End Sub

    Private Sub checkCurrentStatus(ByVal spPO As SparePartPO)
        Dim oldStatus As String = spPO.ProcessCode
        spPO.SyncProcessCode()
        spPO.CancelRequestBy = spPO.ProcessCode.Trim & ":" & oldStatus.Trim
        Dim oBus As New SparePartPOFacade(User)
        oBus.Update(spPO)
        If spPO.ProcessCode.Trim <> oldStatus.Trim Then
            Session.Item("SPartPO") = spPO
            'MessageBox.Show("Proses Gagal. Data telah diupdate oleh user lain") '. Silahkan Refresh halaman terlebih dahulu")
            Me._sesshelper.SetSession("isRefresh", "1")
            Response.Redirect("FrmEntrySparePartPO.aspx")
        End If
    End Sub
    Private Function ChangeSPPOStatus(ByVal stt As String) As String
        Dim objPO As SparePartPO = CType(Session("sessPOHeader"), SparePartPO)

        Dim strReturnMess
        If stt = "S" Then
            Select Case objPO.ProcessCode
                Case "C" : strReturnMess = "Maaf: " & SR.DataCanceled("PO SP ")
                Case "P" : strReturnMess = "Maaf: " & SR.DataProcessed("PO SP ")
                    'Case "S" : strReturnMess = "Maaf: " & SR.DataCanceled("PO SP ")
            End Select
        End If
        Dim oldstt As String = objPO.ProcessCode
        objPO.ProcessCode = stt
        If stt = "S" Then
            objPO.SentPODate = Date.Today
            'If Not (objPO.OrderType = "R" OrElse objPO.OrderType = "Z") Then
            If Not (objPO.OrderType = "Z") Then
                objPO.IsTransfer = 1
            End If
        End If


        Dim nResult As Integer = New SparePartPOFacade(User).UpdateSPPOProcessCode(objPO)

        If stt = "S" Then
            strReturnMess = SR.DataSubmited("PO SP")
        Else
            strReturnMess = SR.DataCanceled("PO SP")
            If oldstt = "S" OrElse oldstt = "P" Then
                strReturnMess = strReturnMess + ", Silahkan konfirmasi ke MMKSI"
            End If
        End If
        DisplayTransactionResult(objPO.ID)
        Return strReturnMess
    End Function

    Private Sub WritePOHeaderToFile(ByRef w As StreamWriter, ByRef strTOPCode As String)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        sbSetARecord.Append("T")
        sbSetARecord.Append(txtPONumber.Text.PadRight(15, pad))
        sbSetARecord.Append(Left(lblDealerName.Text, 25).PadRight(25, pad))
        sbSetARecord.Append(String.Format("{0:yyyyMMdd}", icOrderDate.Value))
        sbSetARecord.Append(CType(Session("sessPODetail"), ArrayList).Count.ToString.PadLeft(4, "0"))
        sbSetARecord.Append(strTOPCode)
        w.WriteLine(sbSetARecord.ToString)
    End Sub

    Private Function WritePODetailToFile(ByRef w As StreamWriter)
        _arlPODetail = CType(Session("sessPODetail"), ArrayList)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        Dim indek As Integer = 0
        For Each objPODetail As SparePartPODetail In _arlPODetail
            indek = indek + 1
            sbSetARecord.Remove(0, sbSetARecord.Length)
            sbSetARecord.Append("D")
            sbSetARecord.Append(objPODetail.SparePartPO.PONumber.PadRight(15, pad))
            sbSetARecord.Append(objPODetail.SparePartMaster.PartNumber.PadRight(20, pad))
            sbSetARecord.Append(objPODetail.Quantity.ToString.PadLeft(5, "0"))
            sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objPODetail.SparePartPO.PODate)) '(objPODetail.SparePartPO.PODate.ToString.Format("{0:yyyyMMdd}"))
            sbSetARecord.Append(indek.ToString.PadLeft(4, "0"))
            w.WriteLine(sbSetARecord.ToString)
        Next
    End Function

    Private Sub chkRequestForCanceled_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRequestForCanceled.CheckedChanged
        Dim ObjPO As SparePartPO = CType(Session("sessPOHeader"), SparePartPO)
        If ObjPO.ProcessCode = "S" Then
            If chkRequestForCanceled.Checked Then
                ObjPO.CancelRequestBy = User.Identity.Name
            Else
                ObjPO.CancelRequestBy = "-" & User.Identity.Name.Trim
            End If
            Dim nResult As Integer = New SparePartPOFacade(User).Update(ObjPO)
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                DisplayTransactionResult(ObjPO.ID)
                MessageBox.Show(SR.UpdateSucces)

            End If
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        btnSave.Visible = False
        btnCancel.Visible = False
        btnSubmit.Visible = False
        btnNew.Visible = False
        btnPrint.Visible = False
        chkRequestForCanceled.Enabled = False
        'if this modul isn't blocked, privileges can be implemented here
        If Not IsTransBlocked() Then
            'Implementation of authorization privilege here
            btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.SaveSPPO_Privilege)
            btnCancel.Visible = SecurityProvider.Authorize(Context.User, SR.CancelSPPO_Privilege)
            btnSubmit.Visible = SecurityProvider.Authorize(Context.User, SR.SubmitSPPO_Privilege)
            If Not IsNothing(Request.QueryString("isDetail")) Then
                btnSubmit.Enabled = Not CType(Request.QueryString("isDetail"), Boolean)
            Else
                btnSubmit.Enabled = SecurityProvider.Authorize(Context.User, SR.SubmitSPPO_Privilege)
            End If
            btnNew.Visible = SecurityProvider.Authorize(Context.User, SR.NewSPPO_Privilege)
            chkRequestForCanceled.Enabled = SecurityProvider.Authorize(Context.User, SR.CancelSPPO_ChkBox_Privilege)
        End If
    End Sub

    Private Sub WriteSPPOData(ByRef sw As StreamWriter)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

        '-- Read SPPO header
        Dim oSPPO As SparePartPO = CType(_sesshelper.GetSession("sessPOHeader"), SparePartPO)

        If Not IsNothing(oSPPO) Then
            itemLine.Remove(0, itemLine.Length)       '-- Empty line
            itemLine.Append("Kode Dealer:" & tab)
            itemLine.Append(oSPPO.Dealer.DealerCode)  '-- Kode dealer
            sw.WriteLine(itemLine.ToString())         '-- Write to file

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Nama Dealer:" & tab)
            itemLine.Append(oSPPO.Dealer.DealerName & " / " & oSPPO.Dealer.SearchTerm2)  '-- Nama dealer
            sw.WriteLine(itemLine.ToString())    '-- Write to file

            itemLine.Remove(0, itemLine.Length)   '-- Empty line
            itemLine.Append("Tipe Order:" & tab)
            itemLine.Append(oSPPO.OrderTypeDesc)  '-- Tipe order
            sw.WriteLine(itemLine.ToString())     '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("SPPO No:" & tab)
            itemLine.Append(oSPPO.PONumber & tab)  '-- PO number
            sw.WriteLine(itemLine.ToString())      '-- Write to file

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Tgl. SPPO:" & tab)
            itemLine.Append(IIf(Format(oSPPO.PODate, "dd/MM/yyyy") <> "01/01/1753", _
                                Format(oSPPO.PODate, "dd/MM/yyyy") & tab, tab))  '-- PO date
            sw.WriteLine(itemLine.ToString())    '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Cara Pembayaran:" & tab)
            itemLine.Append(oSPPO.TermOfPayment.Description & tab)  '-- Cara Pembayaran
            sw.WriteLine(itemLine.ToString())      '-- Write to file
        End If

        '-- Read SPPO detail
        Dim arPODetail As ArrayList = CType(_sesshelper.GetSession("sessPODetail"), ArrayList)

        If Not IsNothing(arPODetail) AndAlso arPODetail.Count <> 0 Then

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Nilai Pemesanan:" & tab)
            itemLine.Append(Decimal.Round(CalculatePOAmount(arPODetail), 2) & tab)  '-- PO date
            sw.WriteLine(itemLine.ToString())    '-- Write to file

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            sw.WriteLine(itemLine.ToString())    '-- Write blank line

            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("No. Barang" & tab)  '-- Part number
            itemLine.Append("Nama Brg." & tab)   '-- Part name
            itemLine.Append("Jumlah" & tab)      '-- Quantity
            itemLine.Append("Harga Eceran" & tab)  '-- Retail price
            itemLine.Append("Total Harga")         '-- Amount
            sw.WriteLine(itemLine.ToString())      '-- Write header

            For Each sppoLine As SparePartPODetail In arPODetail

                itemLine.Remove(0, itemLine.Length)  '-- Empty line

                itemLine.Append(Chr(39) & sppoLine.SparePartMaster.PartNumber.Trim & tab)  '-- Part number
                itemLine.Append(sppoLine.SparePartMaster.PartName & tab)    '-- Part name
                itemLine.Append(Decimal.Round(sppoLine.Quantity, 2) & tab) '-- Quantity
                itemLine.Append(Decimal.Round(sppoLine.RetailPrice, 2) & tab) '-- Retail price
                itemLine.Append(Decimal.Round(sppoLine.Amount, 2) & tab) '-- Total harga
                'itemLine.Append(String.Format("{0:#,##0}", Decimal.Round(sppoLine.Quantity, 2)) & tab) '-- Quantity
                'itemLine.Append(String.Format("{0:#,##0}", Decimal.Round(sppoLine.RetailPrice, 2)) & tab) '-- Retail price
                'itemLine.Append(String.Format("{0:#,##0}", Decimal.Round(sppoLine.Amount, 2)) & tab) '-- Total harga

                sw.WriteLine(itemLine.ToString())  '-- Write Deposit line
            Next

        End If

    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack() Then
            ddlTermOfPayment.Enabled = False
            RemoveAllSession()
            lblSearchPQRNo.Attributes("onclick") = "ShowPQRNoSelection();"

            If Not SecurityProvider.Authorize(Context.User, SR.ViewSPPO_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Pemesanan")
            End If
            If Me._sesshelper.GetSession("isRefresh") = "1" Then
                MessageBox.Show("Proses Gagal. Data telah diupdate oleh user/windows lain")
            End If
            Me._sesshelper.SetSession("isRefresh", "0")
            If Not IsNothing(Request.QueryString("poID")) Then
                '-- Added by Agus Pirnadi
                btnDnload.Visible = True
                BindDdlPaymentType()
                InitialEditPage(CType(Request.QueryString("poID"), Integer))
                _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
                ValidateTOP(_sparePartPOTypeTOP.ID)
            Else
                If Not SecurityProvider.Authorize(Context.User, SR.NewSPPO_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=Pemesanan")
                End If
                '-- Added by Agus Pirnadi
                btnDnload.Visible = False

                If IsTransBlocked() Then
                    Server.Transfer("../FrmAccessDenied.aspx?mess=Saat%20ini%20Modul%20Entri%20Pesanan%20Anda%20dalam%20status%20dikunci")
                End If
                If InitialPageSession() Then
                    BindPODetail()
                End If
                BindDdlPaymentType()
            End If

            If Me._sesshelper.GetSession("isRefresh") = "1" Then
                MessageBox.Show("Proses Gagal. Data telah diupdate oleh user/windows lain")
            End If
            Me._sesshelper.SetSession("isRefresh", "0")
        Else
            If CType(ViewState("vsSave"), String) = "false" Then
                If Request.Form("hid_f") = "1" Then
                    hid_f.Value = "0"
                    btnSave_Click(Nothing, Nothing)
                Else
                    ViewState.Add("vsSave", "true")
                End If

                btnNew_Click(Nothing, Nothing)
            End If
            hid_History.Value = CType(CType(hid_History.Value, Integer) + 1, String)
        End If
        ControlsScriptInjection()
        ActivateUserPrivilege()


    End Sub

    Private Sub btnDnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDnload.Click
        '-- Download data in datagrid to excel file

        '-- Read SPPO header
        Dim oSPPO As SparePartPO = CType(_sesshelper.GetSession("sessPOHeader"), SparePartPO)

        Dim sFileName As String  '-- File name
        If Not IsNothing(oSPPO) Then
            sFileName = oSPPO.PONumber  '-- Set file name as "PO number".xls
        Else
            sFileName = "SPPOList"  '-- Dummy file name
        End If

        '-- Temp file must be a randomly named file!
        Dim SPPOData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

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

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try

    End Sub

    Private Function IsTransBlocked() As Boolean
        Dim nVal As Integer = New DealerFacade(User).ValidateBlockedTransactionControl(CType(Session("DEALER"), Dealer).ID, _
        CType(EnumDealerTransType.DealerTransKind.POSparePart, String))
        Return nVal > 0
    End Function

    Private Sub dtgPODetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPODetail.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            SetDtgPODetailItemFooter(e)
        End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            SetDtgPODetailItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetDtgPODetailItemEdit(e)
        End If
    End Sub

    Private Sub SetDtgPODetailItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblFPopUpSparePart"), Label)

        Dim intPQRHeaderID As Integer = 0
        If ddlOrderType.SelectedValue = "P" Then    ''Jika type Ordernya dari Emergency PQR 

            Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
            If Not IsNothing(objPQRHeader) Then
                intPQRHeaderID = objPQRHeader.ID
            End If
            lblPopUp.Attributes("onclick") = "ShowPopUpSparePart(" & intPQRHeaderID & ");"
            'lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSparePart.aspx?PQRHeaderID=" & CStr(intPQRHeaderID), "", 710, 700, "SparePart")
        Else
            'lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSparePart.aspx", "", 510, 700, "SparePart")
            lblPopUp.Attributes("onclick") = "ShowPopUpSparePart(" & intPQRHeaderID & ");"
        End If
        'Dim lbtnAdd As LinkButton = CType(e.Item.Cells(1).FindControl("lbtnAdd"), LinkButton)
        'If Not lbtnAdd Is Nothing Then
        '    lbtnAdd.Attributes("onclick") = "disableBackButton();"
        'End If
    End Sub

    Private Sub SetDtgPODetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Add(lNum)

        'If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
        '    CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        'End If

        If Not IsNothing(e.Item.FindControl("lbtnEdit")) Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = ubahPrivilege
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = hapusPrivilege
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
        End If
    End Sub

    Private Sub SetDtgPODetailItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Clear()
        e.Item.Cells(0).Controls.Add(lNum)
        Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblEPopUpSparePart"), Label)

        Dim intPQRHeaderID As Integer = 0
        If ddlOrderType.SelectedValue = "P" Then    ''Jika type Ordernya dari Emergency PQR 

            Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
            If Not IsNothing(objPQRHeader) Then
                intPQRHeaderID = objPQRHeader.ID
            End If
            lblPopUp.Attributes("onclick") = "ShowPopUpSparePart(" & intPQRHeaderID & ");"
            'lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSparePart.aspx?PQRHeaderID=" & CStr(intPQRHeaderID), "", 710, 700, "SparePart")
        Else
            lblPopUp.Attributes("onclick") = "ShowPopUpSparePart(" & intPQRHeaderID & ");"
            'lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSparePart.aspx", "", 510, 700, "SparePart")
        End If

    End Sub

    Private Function InsertPODetail(ByVal objPODetail As SparePartPODetail) As Integer
        Dim rValue As Integer = -1
        If txtPONumber.Text.Trim = "[Dibuat oleh sistem]" Then
            rValue = New SparePartPOFacade(User).InsertSparePartPO(GetPOHeader(), objPODetail)
            If rValue <> -1 Then
                Dim objPO As SparePartPO = New SparePartPOFacade(User).Retrieve(rValue)
                _sesshelper.SetSession("sessPOHeader", objPO)
                txtPONumber.Text = objPO.PONumber
                rValue = objPO.SparePartPODetails(0).ID
            End If
        Else
            objPODetail.SparePartPO = GetPOHeader()
            rValue = New SparePartPOFacade(User).InsertSparePartPODetail(objPODetail.SparePartPO, objPODetail)
        End If
        Return rValue
    End Function

    Private Function UpdatePODetail(ByVal objPODetail As SparePartPODetail) As Boolean
        Dim rValue As Integer = -1
        objPODetail.SparePartPO = GetPOHeader()
        rValue = New SparePartPOFacade(User).UpdateSparePartPO(objPODetail.SparePartPO, objPODetail)
        Return rValue > 0
    End Function

    Private Function DeletePODetail(ByVal objPODetail As SparePartPODetail) As Boolean
        Dim rValue As Integer = -1
        objPODetail.SparePartPO = GetPOHeader()
        objPODetail.RowStatus = DBRowStatus.Deleted
        rValue = New SparePartPODetailFacade(User).DeleteFromDB(objPODetail)
        Return rValue <> -1
    End Function

    Private Sub RenderPartItem(ByVal objpart As SparePartMaster, _
    ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs, _
    ByVal ctrlPartName As String, ByVal ctrlRetailPrice As String, ByVal ctrlAmount As String)
        Dim lblPartNumber As Label = CType(e.Item.FindControl(ctrlPartName), Label)
        Dim lblRetailPrice As Label = CType(e.Item.FindControl(ctrlRetailPrice), Label)
        Dim lblAmount As Label = CType(e.Item.FindControl(ctrlAmount), Label)
        lblPartNumber.Text = objpart.PartName
        lblRetailPrice.Text = String.Format("{0:###}", objpart.RetalPrice)
        lblAmount.Text = "0"
    End Sub

    Private Sub dtgPODetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPODetail.ItemCommand
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        _arlPODetail = CType(Session("sessPODetail"), ArrayList)
        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid


                Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtFPartNumber"), TextBox)

                Dim txtQty As TextBox = CType(e.Item.FindControl("txtFQTY"), TextBox)
                Dim objPart As SparePartMaster
                If IsNothing(txtPartNumber) OrElse txtPartNumber.Text = String.Empty Then
                    MessageBox.Show("No.Part masih kosong")
                    Return
                Else
                    objPart = New SparePartMasterFacade(User).Retrieve(txtPartNumber.Text.Trim().ToUpper())
                    If IsNothing(objPart) Then
                        MessageBox.Show("No.Part tidak terdaftar")
                        Return
                    Else
                        If objPart.ID <= 0 Then
                            MessageBox.Show("No.Part tidak terdaftar")
                            Return
                        End If
                    End If
                    'Validation For UnActive Sparepart Master
                    If CType(objPart.ActiveStatus, Short) = 1 Then
                        MessageBox.Show("No.Part Tidak Aktif")
                        Return
                    End If
                    If ddlOrderType.SelectedValue = "P" Then    ''Jika type Ordernya dari Emergency PQR 
                        If txtPQRNo.Text.Trim = "" Then
                            MessageBox.Show("Nomor PQR harap diisi")
                            Return
                        End If
                        Dim intPQRHeaderID As Integer = 0
                        Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
                        If Not IsNothing(objPQRHeader) Then
                            If objPQRHeader.ID > 0 Then
                                intPQRHeaderID = objPQRHeader.ID
                            Else
                                MessageBox.Show("Nomor PQR tidak terdaftar")
                                Return
                            End If
                        Else
                            MessageBox.Show("Nomor PQR tidak terdaftar")
                            Return
                        End If
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SparePartMaster), "TypeCode", MatchType.NotInSet, "'I','E'"))
                        If intPQRHeaderID > 0 Then
                            Dim strSQL As String = "SELECT Distinct SparePartMasterID FROM PQRPartsCode WHERE PQRHeaderID = " & intPQRHeaderID
                            criterias.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.InSet, "(" + strSQL + ")"))
                        End If
                        criterias.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.Exact, objPart.ID))
                        Dim arrSparePartMaster As ArrayList = New SparePartMasterFacade(User).Retrieve(criterias)
                        If Not IsNothing(arrSparePartMaster) AndAlso arrSparePartMaster.Count > 0 Then
                        Else
                            MessageBox.Show("No.Part yang diinput tidak sesuai dengan No.Part pada PQR")
                            Return
                        End If
                    End If
                End If
                If IsNothing(txtQty) OrElse txtQty.Text = String.Empty OrElse CType(txtQty.Text.Trim, Integer) <= 0 Then
                    MessageBox.Show("Jumlah Pesanan tidak boleh kosong/0")
                    If Not (IsNothing(objPart) OrElse objPart.ID = 0) Then
                        RenderPartItem(objPart, e, "lblFPartName", "lblFRetailPrice", "lblFPOAmount")
                    End If
                    Return
                End If

                btnSubmit.Enabled = False
                If Not (IsNothing(objPart) OrElse objPart.ID = 0) Then

                    If objPart.ProductCategory.Code.Trim.ToUpper <> companyCode.Trim.ToUpper Then
                        MessageBox.Show("Sparepart tidak ada untuk produk " & companyCode)
                        Return
                    End If

                    If objPart.TypeCode = "I" Or objPart.TypeCode = "E" Or objPart.TypeCode = "X" Or objPart.TypeCode = "A" Then
                        MessageBox.Show("Untuk Sparepart dengan Type I,E,X dan A harap dipesan lewat menu Indent Part")
                        Return
                    End If

                    'start add by anh 2017-02-01 req by Maria Anna
                    If objPart.TypeCode = "P" Then
                        MessageBox.Show("Untuk Sparepart dengan Type P harap hubungi Customer Support")
                        Return
                    End If
                    'end add by anh 2017-02-01 req by Maria Anna

                    If Not PartIsExist(objPart.PartNumber, _arlPODetail) Then
                        ddlOrderType.Enabled = False
                        Dim objPODetail As SparePartPODetail = New SparePartPODetail
                        objPODetail.Quantity = CType(IIf(txtQty.Text = "", "0", txtQty.Text), Integer)
                        objPODetail.SparePartMaster = objPart
                        objPODetail.CheckListStatus = String.Empty
                        objPODetail.RetailPrice = objPart.RetalPrice
                        _arlPODetail.Add(objPODetail)
                        If CType(ViewState("vsAccess"), String) = "edit" Then
                            objPODetail.SparePartPO = CType(Session("sessPOHeader"), SparePartPO)
                            Dim nResult As Integer = New SparePartPODetailFacade(User).Insert(objPODetail)
                        End If
                        Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")
                    Else
                        MessageBox.Show(SR.DataIsExist("Spare Part"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Spare Part"))
                End If
            Case "edit" 'Edit mode activated
                dtgPODetail.ShowFooter = False
                btnSubmit.Enabled = False
                btnSave.Enabled = False
                dtgPODetail.EditItemIndex = e.Item.ItemIndex
            Case "delete" 'Delete this datagrid item 
                Try
                    _arlPODetail.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception

                End Try
                btnSubmit.Enabled = False
            Case "save" 'Update this datagrid item                 
                Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtEPartNumber"), TextBox)
                Dim txtQty As TextBox = CType(e.Item.FindControl("txtEQTY"), TextBox)
                Dim objPart As SparePartMaster
                If IsNothing(txtPartNumber) OrElse txtPartNumber.Text = String.Empty Then
                    MessageBox.Show("No.Part masih kosong")
                    Return
                Else
                    objPart = New SparePartMasterFacade(User).Retrieve(txtPartNumber.Text)
                    If IsNothing(objPart) Then
                        MessageBox.Show("No.Part tidak terdaftar")
                        Return
                    Else
                        If objPart.ID <= 0 Then
                            MessageBox.Show("No.Part tidak terdaftar")
                            Return
                        End If
                    End If
                End If
                If ddlOrderType.SelectedValue = "P" Then    ''Jika type Ordernya dari Emergency PQR 
                    If txtPQRNo.Text.Trim = "" Then
                        MessageBox.Show("Nomor PQR harap diisi")
                        Return
                    End If
                    Dim intPQRHeaderID As Integer = 0
                    Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
                    If Not IsNothing(objPQRHeader) Then
                        If objPQRHeader.ID > 0 Then
                            intPQRHeaderID = objPQRHeader.ID
                        Else
                            MessageBox.Show("Nomor PQR tidak terdaftar")
                            Return
                        End If
                    Else
                        MessageBox.Show("Nomor PQR tidak terdaftar")
                        Return
                    End If
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SparePartMaster), "TypeCode", MatchType.NotInSet, "'I','E'"))
                    If intPQRHeaderID > 0 Then
                        Dim strSQL As String = "SELECT Distinct SparePartMasterID FROM PQRPartsCode WHERE PQRHeaderID = " & intPQRHeaderID
                        criterias.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.InSet, "(" + strSQL + ")"))
                    End If
                    criterias.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.Exact, objPart.ID))
                    Dim arrSparePartMaster As ArrayList = New SparePartMasterFacade(User).Retrieve(criterias)
                    If Not IsNothing(arrSparePartMaster) AndAlso arrSparePartMaster.Count > 0 Then
                    Else
                        MessageBox.Show("No.Part yang diinput tidak sesuai dengan No.Part pada PQR")
                        Return
                    End If
                End If

                If IsNothing(txtQty) OrElse txtQty.Text = String.Empty OrElse CType(txtQty.Text.Trim, Integer) <= 0 Then
                    MessageBox.Show("Jumlah Pesanan tidak boleh kosong/0")
                    If Not (IsNothing(objPart) OrElse objPart.ID = 0) Then
                        RenderPartItem(objPart, e, "lblEPartName", "lblERetailPrice", "lblEPOAmount")
                    End If
                    Return
                End If

                If Not (IsNothing(objPart) Or objPart.ID = 0) Then
                    If objPart.TypeCode = "I" Or objPart.TypeCode = "E" Or objPart.TypeCode = "X" Then
                        MessageBox.Show("Untuk Sparepart dengan Type I,E,X harap dipesan lewat menu Indent Part")
                        Return
                    End If
                    'start add by anh 2017-02-01 req by Maria Anna
                    If objPart.TypeCode = "P" Then
                        MessageBox.Show("Untuk Sparepart dengan Type P harap hubungi Customer Support")
                        Return
                    End If
                    'end add by anh 2017-02-01 req by Maria Anna
                    If Not PartIsExist(txtPartNumber.Text.Trim().ToUpper(), _arlPODetail, e.Item.ItemIndex) Then
                        Dim objPODetail As SparePartPODetail = CType(_arlPODetail(e.Item.ItemIndex), SparePartPODetail)
                        objPODetail.Quantity = CType(txtQty.Text, Integer)
                        objPODetail.SparePartMaster = objPart
                        objPODetail.RetailPrice = objPart.RetalPrice
                        If CType(ViewState("vsAccess"), String) = "edit" Then
                            objPODetail.SparePartPO = CType(Session("sessPOHeader"), SparePartPO)
                            Dim nResult As Integer = New SparePartPODetailFacade(User).Update(objPODetail)
                        End If
                        dtgPODetail.EditItemIndex = -1
                        dtgPODetail.ShowFooter = tambahPrivilege
                        btnSave.Enabled = True
                    Else
                        MessageBox.Show(SR.DataIsExist("Spare Part"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Spare Part"))
                End If
            Case "cancel" 'Cancel Update this datagrid item 
                dtgPODetail.EditItemIndex = -1
                dtgPODetail.ShowFooter = tambahPrivilege
                btnSave.Enabled = True

        End Select

        Dim stataus As Boolean = True
        Dim sparePartPOTypeTOP As SparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
        If sparePartPOTypeTOP.IsTOP Then
            stataus = ValidateTOP(sparePartPOTypeTOP.ID)
            btnSave.Enabled = stataus

            If CType(_sesshelper.GetSession("sessPODetail"), ArrayList).Count > 0 Then
                btnSave.Enabled = True
            End If

        End If

        If stataus Then
            _sesshelper.SetSession("sessPODetail", _arlPODetail)
            BindPODetail()
        End If
    End Sub

    Private Function ValidateTOP(ByVal typeTopId As Integer) As Boolean
        _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
        'If Not ddlTermOfPayment.Enabled Then
        '    Return True
        'End If
        'Dim isCOD As Boolean = True
        Dim result As Boolean = True
        Dim counterTOP As Integer = 0
        Dim counterCOD As Integer = 0
        Dim PartTypeTOP As Short = 100 ' initial for status COD is nothing
        'Validate TOP

        If CType(ViewState("AllowTOP"), Boolean) = True Then
            Return True
        End If

        _arlPODetail = CType(Session("sessPODetail"), ArrayList)

        For Each PODetail As SparePartPODetail In _arlPODetail
            Dim sparePartMasterTop As SparePartMasterTOP = New SparePartMasterTOPFacade(User).RetrieveBySPIDandTypeTOPID(PODetail.SparePartMaster.ID, typeTopId)

            ' Farid Additionla for mix parts
            If PartTypeTOP = 100 Then
                PartTypeTOP = CType(sparePartMasterTop.Status, Short)
            Else
                If PartTypeTOP <> CType(sparePartMasterTop.Status, Short) Then
                    counterTOP = counterTOP + 1
                    counterCOD = counterCOD + 1
                End If
            End If

            If Not IsNothing(sparePartMasterTop.SparePartPOTypeTOP) Then

                If sparePartMasterTop.Status = False Then
                    ddlTermOfPayment.ClearSelection()
                    ddlTermOfPayment.Items.Insert(0, New ListItem("COD", "1")) 'hardcoded temp
                    ddlTermOfPayment.Enabled = False
                    ViewState.Add("IsCOD", True)
                End If

                'If sparePartMasterTop.Status = False And sparePartMasterTop.SparePartPOTypeTOP.IsTOP Then
                '    counterTOP = counterTOP + 1
                'ElseIf (Not sparePartMasterTop.Status Or Not sparePartMasterTop.SparePartPOTypeTOP.IsTOP) Then
                '    counterCOD = counterCOD + 1
                'End If
            Else
                counterCOD = counterCOD + 1
            End If
        Next
        If counterTOP > 0 And counterCOD > 0 Then
            MessageBox.Show("Sehubungan dengan TOP, material-material ini tidak bisa dibuat dalam PO ini")
            result = False
        ElseIf counterTOP = 0 And counterCOD > 0 Then
            ddlTermOfPayment.ClearSelection()
            ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID)) 'hardcoded temp
            ddlTermOfPayment.Enabled = False
        Else

        End If
        Return result
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim LineError As Integer = 0
        Try
            _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
            If Not IsNothing(ddlOrderType.SelectedValue) And _sparePartPOTypeTOP.IsTOP Then
                If ddlTermOfPayment.SelectedValue = "" Then
                    MessageBox.Show("Cara pembayaran harus dipilih")
                    Exit Sub
                End If

                If Not ValidateTOP(_sparePartPOTypeTOP.ID) Then
                    btnSubmit.Enabled = False
                    Exit Sub
                End If
            End If

            If ddlOrderType.SelectedValue = "P" Then    ''Jika type Ordernya dari Emergency PQR 
                If txtPQRNo.Text.Trim = "" Then
                    MessageBox.Show("Nomor PQR harap diisi")
                    Return
                End If
                Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
                If IsNothing(objPQRHeader) Then
                    MessageBox.Show("Nomor PQR tidak terdaftar")
                    Return
                Else
                    If objPQRHeader.ID = 0 Then
                        MessageBox.Show("Nomor PQR tidak terdaftar")
                        Return
                    End If
                End If
            End If

            If ddlOrderType.SelectedValue = "I" Or ddlOrderType.SelectedValue = "K" Then
                LineError = 1
                MessageBox.Show("Type Indent Part atau Khusus tidak diperbolehkan")
                Exit Sub
            End If
            LineError = 2
            If Not IsNothing(Session("sessPODetail")) AndAlso CType(Session("sessPODetail"), ArrayList).Count > 0 Then
                LineError = 3
                Dim nResult As Integer
                Select Case CType(ViewState("vsAccess"), String)
                    Case "insert"
                        LineError = 4
                        If icOrderDate.Value <= DateTime.Now Then
                            Try
                                LineError = 5
                                nResult = InsertNewPO()
                            Catch ex As Exception
                                LineError = 6
                                MessageBox.Show("Gagal simpan Spare part PO " & ex.Message)
                                Return
                            End Try
                            LineError = 7
                            If nResult <> -1 Then
                                LineError = 8
                                DisplayTransactionResult(nResult)
                                LineError = 9
                                ViewState.Add("vsAccess", "edit")
                                ViewState.Add("vsSave", "true")
                                MessageBox.Show(SR.SaveSuccess)
                            Else
                                MessageBox.Show(SR.SaveFail)
                            End If
                        Else
                            MessageBox.Show("Tanggal Order tidak valid")
                        End If
                    Case "edit"
                        Dim strPOID = CType(Session("sessPOHeader"), SparePartPO).ID
                        LineError = 10
                        MergePODetail(strPOID)
                        LineError = 11

                        Dim objPO As SparePartPO = New SparePartPOFacade(User).Retrieve(strPOID)
                        If objPO.ProcessCode <> "" OrElse objPO.PONumber <> txtPONumber.Text Then 'PO status is not NEW anymore
                            MessageBox.Show("Simpan data tidak berhasil, status PO sudah diubah")
                            Return
                        End If

                        Try
                            nResult = EditPO()
                            LineError = 12
                        Catch ex1 As Exception
                            LineError = 13
                            MessageBox.Show("Gagal simpan Spare part PO " & ex1.Message)
                            Return
                        End Try

                        LineError = 14
                        If nResult <> -1 Then
                            LineError = 15
                            DisplayTransactionResult(nResult)
                            LineError = 16
                            MessageBox.Show(SR.SaveSuccess)
                            ViewState.Add("vsSave", "true")
                        Else
                            MessageBox.Show(SR.SaveFail)
                        End If
                End Select
            Else
                MessageBox.Show(SR.GridIsEmpty("PO Detail"))
            End If
        Catch ex As Exception
            Response.Write("<h2>Error: Mohon capture pesan error ini dan kirimkan ke Admin D-Net.</h2> Line Error=" & LineError.ToString & "<br>Original Error=" & ex.Message)
            'MessageBox.Show("<h2>Error: M ohon capture pesan error ini dan kirimkan ke Admin D-Net.</h2> Line Error=" & LineError.ToString)
        End Try


    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        RemoveQueryString()

        _arlPODetail = CType(Session("sessPODetail"), ArrayList)
        btnBack.Visible = False

        If CType(ViewState("vsSave"), String) = "new" AndAlso _arlPODetail.Count > 0 Then
            ViewState.Add("vsSave", "false")
            MessageBox.Confirm("Data mau disimpan dulu?", "hid_f")
        Else
            ViewMode(True)
            btnCancel.Enabled = False
            btnSubmit.Enabled = False
            ddlOrderType.Enabled = True
            txtPQRNo.Enabled = True
            lblSearchPQRNo.Visible = True
            icOrderDate.Enabled = True
            chkRequestForCanceled.Visible = False
            ViewState.Add("vsAccess", "insert")
            ViewState.Add("vsSave", "new")
            RemoveAllSession()
            txtPONumber.Text = "[Dibuat oleh sistem]"
            icOrderDate.Value = Date.Now
            ddlOrderType.SelectedValue = "E"
            _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
            ddlTermOfPayment.ClearSelection()
            ddlTermOfPayment.Enabled = False
            _arlPODetail.Clear()
            _sesshelper.SetSession("sessPODetail", _arlPODetail)
            InitialPageSession()
            BindPODetail()
            BindDdlPaymentType()
            btnBack.Visible = False
            btnDnload.Visible = False
            ddlOrderType_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Function getSumOrder() As Double
        Dim _return As Double = 0
        For Each item As DataGridItem In dtgPODetail.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim orderAmount As Label = CType(item.FindControl("lblPOAmount"), Label)
                _return = _return + CDbl(orderAmount.Text)
            End If
        Next
        Return _return
    End Function


    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim ssHelper As SessionHelper = New SessionHelper
        Dim ObjDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)

        _arlPODetail = CType(Session("sessPODetail"), ArrayList)
        If IsNothing(_arlPODetail) OrElse (Not IsNothing(_arlPODetail) AndAlso _arlPODetail.Count = 0) Then
            MessageBox.Show(SR.GridIsEmpty("PO Detail"))
            Exit Sub
        End If

        If ddlOrderType.SelectedValue = "R" AndAlso ddlTermOfPayment.SelectedValue <> 1 Then
            '1
            Dim availCeiling As Double = New CommonFunction().IsTOPCeilingValid(ObjDealer)
            Dim sumOrder As Double = getSumOrder()
            If sumOrder > availCeiling Then
                MessageBox.Show("Pengajuan anda ditolak dikarenakan total order melebihi sisa ceiling (RP. " & availCeiling.ToString("N0") & "). \n Silahkan melakukan order kembali dengan tidak melewati sisa ceiling yang tersedia.\n")
                Exit Sub
            End If

            '2
            Dim topDays As Integer = CInt(ddlTermOfPayment.SelectedValue)
            Dim calcDate As Date = DateAdd(DateInterval.Day, topDays, Date.Now)
            Dim dateDisplay As Integer = -1
            If Not New CommonFunction().IsTOPTransferControlValid(ObjDealer, calcDate, dateDisplay) Then
                'MessageBox.Show("Pengajuan anda ditolak dikarenakan Validitas Jaminan sudah expired, silahkan hubungi Accounting Dealer atau MMKSI CCD")
                If dateDisplay >= 0 Then
                    MessageBox.Show("Pengajuan anda ditolak dikarenakan jangka waktu TOP melebihi sisa validitas jaminan yang tersedia (" & dateDisplay & " Hari). Silahkan melakukan order kembali dengan tidak melewati sisa validitas jaminan yang tersedia.")
                Else
                    MessageBox.Show("Tidak ada data transfer control untuk credit account tersebut")
                End If
                Exit Sub
            End If

            '3
            If Not New CommonFunction().IsTOPValid(ObjDealer, ViewState("NotComplete")) Then
                Dim strMess As String = "Pengajuan anda ditolak dikarenakan masih ada Outstanding TOP yang belum melakukan payment confirmation dan status payment sampai validasi No Reg.\n"
                If Not IsNothing(ViewState("NotComplete")) Then
                    strMess = strMess & ViewState("NotComplete") & ".\n"
                End If
                MessageBox.Show(strMess)
                Exit Sub
            End If
        End If

        If Not ObjDealer Is Nothing Then
            Dim sMessage As String = ""
            sMessage = New OrderRestrictionFacade(User).isOrderRestricted(ObjDealer, ddlOrderType.SelectedValue)

            If ddlOrderType.SelectedValue = "R" And ddlTermOfPayment.SelectedValue = "" Then
                MessageBox.Show("Cara Pembayaran harus diisi")
                Exit Sub
            End If

            If sMessage.Length > 0 Then
                MessageBox.Show(sMessage)
            Else
                If Not IsNothing(Session("sessPOHeader")) AndAlso CType(Session("sessPOHeader"), SparePartPO).PONumber <> txtPONumber.Text Then
                    MessageBox.Show("Data Tidak Valid, Silahkan Refresh Browser Anda")

                Else
                    CreateTextFileForKTB()
                End If

            End If
        Else
            Response.Redirect("../login.aspx")
        End If
        BindPODetail()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        BindPODetail()
    End Sub

    Protected Sub ddlOrderType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlOrderType.SelectedIndexChanged
        Dim ddlOrderTypeT As DropDownList = CType(sender, DropDownList)

        _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
        If Not IsNothing(ddlOrderType.SelectedValue) And _sparePartPOTypeTOP.IsTOP Then
            ddlTermOfPayment.Enabled = True
            BindDdlPaymentType()
        Else
            ddlTermOfPayment.ClearSelection()
            ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
            ddlTermOfPayment.Enabled = False
        End If

        If ddlOrderType.SelectedValue = "P" Then
            trPQRNo.visible = True
            txtPQRNo.Enabled = True
            lblSearchPQRNo.Visible = True
        Else
            trPQRNo.Visible = False
            txtPQRNo.Text = ""
            txtPQRNo_TextChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim objPOHeader As SparePartPO = New SparePartPOFacade(User).Retrieve(CType(Session("sessPOHeader"), SparePartPO).ID)
        Me.checkCurrentStatus(objPOHeader)
        If Not IsOKStatus(objPOHeader.ProcessCode) Then
            Return
        End If
        MessageBox.Show(ChangeSPPOStatus("C"))
    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
             ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        pCompletelist.Sort(objListComparer)

    End Sub

    Private Sub dtgPODetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPODetail.SortCommand
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

        Dim arlCompletelist As ArrayList = Session("sessPODetail")
        If Not arlCompletelist Is Nothing Then
            SortListControl(arlCompletelist, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            _sesshelper.SetSession("sessPODetail", arlCompletelist)
            BindPODetail()
        End If
    End Sub


    Private Function IsOKStatus(ByVal StatusCode As String) As Boolean

        Dim result As Boolean = True

        Dim oDealer As Dealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Return True
        End If

        If StatusCode.ToUpper.Trim <> "" Then
            MessageBox.Show("Data PO dengan status " & GetStatusDesc(StatusCode) & " tidak dapat diubah")
            result = False
            btnCancel.Enabled = False
            btnSubmit.Enabled = False
            dtgPODetail.ShowFooter = False
            dtgPODetail.Columns(dtgPODetail.Columns.Count - 1).Visible = False

        End If

        Return result

    End Function

    Private Function GetStatusDesc(ByVal StatusCode As String) As String
        StatusCode = StatusCode.ToUpper.Trim
        Select Case StatusCode
            Case ""
                Return ""
            Case "C"
                Return "Batal"
            Case "S"
                Return "Kirim"
            Case "P"
                Return "Proses"
            Case "T"
                Return "Tolak"
            Case "X"
                Return "Batal MMKSI"

        End Select
    End Function

    Sub RemoveQueryString()
        Dim isreadonly As PropertyInfo = GetType(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance Or BindingFlags.NonPublic)

        ' make collection editable
        isreadonly.SetValue(Me.Request.QueryString, False, Nothing)

        ' remove
        Me.Request.QueryString.Remove("poID")
        Me.Request.QueryString.Remove("isDetail")

    End Sub

    Private Sub txtPQRNo_TextChanged(sender As Object, e As EventArgs) Handles txtPQRNo.TextChanged
        Try
            BindPODetail()
        Catch
        End Try
    End Sub
#End Region

End Class
