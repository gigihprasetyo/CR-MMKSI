#Region " Summary "
'---------------------------------------------------------'
'-- Program Code : FrmUploadSPPOExcel.aspx              --'
'-- Program Name : PEMESANAN-Upload Melalui File Excel  --'
'-- Description  :                                      --'
'---------------------------------------------------------'
'-- Programmer   : Agus Pirnadi                         --'
'-- Start Date   : Oct 17 2005                          --'
'-- Update By    :                                      --'
'-- Last Update  : Feb 23 2006                          --'
'---------------------------------------------------------'
'-- Copyright © 2005 by Intimedia                       --'
'---------------------------------------------------------'
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

#Region " .NET Namespace "
Imports System.IO
Imports System.Text
#End Region

Public Class FrmUploadSPPOExcel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents icOrderDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblTest As System.Web.UI.WebControls.Label
    Protected WithEvents dtgPODetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents hid_f As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents lblPOAmount As System.Web.UI.WebControls.Label
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents trPQRNo As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents txtPQRNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchPQRNo As System.Web.UI.WebControls.Label

    Protected WithEvents btnDownloadSample As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"
    Private _arlPODetail As ArrayList = New ArrayList
    Private _sesshelper As SessionHelper = New SessionHelper
    Private _sparePartPOTypeTOP As SparePartPOTypeTOP
#End Region

#Region " Custom Method "

    Private Sub BindDdlPaymentType()
        Dim dlr As Dealer = CType(Session("sessDealer"), Dealer)
        Dim spCa As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccountFacade(User).RetrieveByDealerCode(dlr.DealerCode)

        Dim oTopCA As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
        If Not IsNothing(oTopCA) Then
            Dim listOfPayments As ArrayList = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
            ddlTermOfPayment.DataSource = listOfPayments
            ddlTermOfPayment.DataValueField = "ID"
            ddlTermOfPayment.DataTextField = "Description"
            ddlTermOfPayment.DataBind()
            _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
            If _sparePartPOTypeTOP.IsTOP Then
                ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
                ViewState.Add("TOPDealer", True)
            Else
                If Not IsNothing(Request.QueryString("poid")) Then
                    ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
                    ViewState.Add("TOPDealer", True)
                Else
                    ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
                End If
            End If
        Else
            ddlTermOfPayment.Enabled = False
            ddlTermOfPayment.SelectedIndex = 0
        End If
    End Sub

    Private Sub BindPODetail()
        _arlPODetail = CType(Session("sessPODetail"), ArrayList)
        lblPOAmount.Text = String.Format("{0:#,##0}", CalculatePOAmount(_arlPODetail))

        '-- Check sparepart PO detail errors
        Dim bError As Boolean = False  '-- True if any error exists
        Dim strPNUnAct As String = ""

        Dim row As Integer = 0

        For Each PODetail As SparePartPODetail In _arlPODetail
            If Not IsNothing(PODetail.ErrorMessage) AndAlso PODetail.ErrorMessage <> String.Empty Then
                bError = True
                If Not IsNothing(PODetail.SparePartMaster) Then
                    strPNUnAct = strPNUnAct + PODetail.SparePartMaster.PartName + " "
                Else
                    Dim bb As Integer = 0
                End If

            End If
        Next

        '-- If any error exists then unable to save into DNet database
        ViewState.Add("COD", False)
        If bError OrElse dtgPODetail.EditItemIndex <> -1 Then
            btnSave.Enabled = False
            ViewState("COD") = True
        Else
            btnSave.Enabled = True
        End If

        If _arlPODetail.Count = 0 Then
            btnSave.Enabled = False
        End If

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

    Private Sub SetDtgPODetailItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        'Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblFPopUpSparePart"), Label)
        Dim lblPopUp As Label = CType(e.Item.FindControl("lblFPopUpSparePart"), Label)

        If ddlOrderType.SelectedValue = "P" Then    ''Jika type Ordernya dari Emergency PQR 
            Dim intPQRHeaderID As Integer = 0
            Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
            If Not IsNothing(objPQRHeader) Then
                intPQRHeaderID = objPQRHeader.ID
            End If
            lblPopUp.Attributes("onclick") = "ShowPopUpSparePart(" & intPQRHeaderID & ");"
            'lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSparePart.aspx?PQRHeaderID=" & CStr(intPQRHeaderID), "", 710, 700, "SparePart")
        Else
            lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSparePart.aspx", "", 510, 700, "SparePart")
        End If

    End Sub

    Private Sub SetDtgPODetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Add(lNum)

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
        End If
    End Sub

    Private Sub SetDtgPODetailItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Clear()
        e.Item.Cells(0).Controls.Add(lNum)
        'Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblEPopUpSparePart"), Label)
        Dim lblPopUp As Label = CType(e.Item.FindControl("lblEPopUpSparePart"), Label)
        If ddlOrderType.SelectedValue = "P" Then    ''Jika type Ordernya dari Emergency PQR 
            Dim intPQRHeaderID As Integer = 0
            Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
            If Not IsNothing(objPQRHeader) Then
                intPQRHeaderID = objPQRHeader.ID
            End If
            lblPopUp.Attributes("onclick") = "ShowPopUpSparePart(" & intPQRHeaderID & ");"
            'lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSparePart.aspx?PQRHeaderID=" & CStr(intPQRHeaderID), "", 710, 700, "SparePart")
        Else
            lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSparePart.aspx", "", 510, 700, "SparePart")
        End If
    End Sub

    Private Sub RenderPartItem(ByVal objpart As SparePartMaster, _
                               ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs, _
                               ByVal ctrlPartName As String, ByVal ctrlRetailPrice As String, _
                               ByVal ctrlAmount As String)

        Dim lblPartNumber As Label = CType(e.Item.FindControl(ctrlPartName), Label)
        Dim lblRetailPrice As Label = CType(e.Item.FindControl(ctrlRetailPrice), Label)
        Dim lblAmount As Label = CType(e.Item.FindControl(ctrlAmount), Label)

        lblPartNumber.Text = objpart.PartName
        lblRetailPrice.Text = String.Format("{0:###}", objpart.RetalPrice)
        lblAmount.Text = "0"
    End Sub

    Private Function PartIsExist(ByVal partNumber As String, ByVal arlPODetail As ArrayList) As Boolean
        For Each poDetail As SparePartPODetail In arlPODetail
            Try
                If poDetail.SparePartMaster.PartNumber.Trim().ToUpper() = partNumber.Trim().ToUpper() Then
                    Return True
                End If
            Catch ex As Exception
                '-- Does nothing! Test next record if any.
            End Try
        Next
        Return False
    End Function

    Private Function PartIsExist(ByVal partNumber As String, ByVal arlPODetail As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim i As Integer
        For i = 0 To arlPODetail.Count - 1
            If Not IsNothing(CType(arlPODetail(i), SparePartPODetail).SparePartMaster) AndAlso CType(arlPODetail(i), SparePartPODetail).SparePartMaster.PartNumber.Trim().ToUpper() = partNumber.Trim().ToUpper() AndAlso nIndeks <> i Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function GetPOHeader() As SparePartPO
        Dim objPO As SparePartPO = New SparePartPO
        objPO.PODate = icOrderDate.Value
        objPO.Dealer = CType(Session("sessDealer"), Dealer)
        objPO.OrderType = ddlOrderType.SelectedValue

        If ddlOrderType.SelectedValue = "P" Then    ''Jika type Ordernya dari Emergency PQR 
            Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
            If Not IsNothing(objPQRHeader) Then
                objPO.PQRHeader = objPQRHeader
            End If
        End If
        If ddlTermOfPayment.SelectedValue <> "" Then
            objPO.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(ddlTermOfPayment.SelectedValue, Integer))
        Else
            objPO.TermOfPayment = Nothing
        End If
        objPO.ProcessCode = ""
        Return objPO
    End Function

    Private Sub ViewMode(ByVal bMode As Boolean)
        dtgPODetail.ShowFooter = bMode
        dtgPODetail.Columns(6).Visible = bMode
        ddlOrderType.Enabled = bMode
        icOrderDate.Enabled = bMode
        btnSave.Enabled = bMode
        btnPrint.Enabled = Not bMode
        btnSubmit.Enabled = Not bMode
        btnCancel.Enabled = Not bMode

        'btnNew.Enabled = Not bMode
    End Sub

    Private Function DisplayTransactionResult(ByVal nID As Integer)
        Dim stt As Boolean = False
        Dim objPO As SparePartPO = New SparePartPOFacade(User).Retrieve(nID)
        If CType(ViewState("vsAccess"), String) = "edit" Then
            If objPO.ProcessCode = "" Then
                stt = True
            End If
        End If
        ViewMode(stt)
        If CType(ViewState("vsAccess"), String) = "edit" Then
            If objPO.ProcessCode = "C" Then
                btnSubmit.Enabled = False
                btnCancel.Enabled = False
            End If
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

        _arlPODetail = objPO.SparePartPODetails
        _sesshelper.SetSession("sessPODetail", _arlPODetail)
        _sesshelper.SetSession("sessPOHeader", objPO)

        BindPODetail()
    End Function

    Private Function EditPO() As Integer
        Dim ObjPO As SparePartPO = CType(Session("sessPOHeader"), SparePartPO)
        Return New SparePartPOFacade(User).UpdateSparePartPO(ObjPO, CType(Session("sessPODetail"), ArrayList))
    End Function

    Private Function InsertNewPO() As Integer
        Dim ObjPO As SparePartPO = GetPOHeader()
        Return New SparePartPOFacade(User).InsertSparePartPO(ObjPO, CType(Session("sessPODetail"), ArrayList))
    End Function

    Private Sub MergePODetail()
        Dim objPO As SparePartPO = New SparePartPOFacade(User).Retrieve(CType(Session("sessPOHeader"), SparePartPO).ID)
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

    Private Sub CreateFolder(ByVal folderName As String)
        Dim dirInfo As DirectoryInfo = New DirectoryInfo(folderName)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub

    Private Sub CreateTextFileForKTB()
        Dim FOLDER_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("DNetServerFolder") & txtPONumber.Text.Substring(1, 4)
        Dim FILE_NAME As String = FOLDER_NAME + "\" + txtPONumber.Text + IIf(ddlOrderType.SelectedValue = "E", ".EOD", ".DAT")
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _sapServer)
        Dim succes As Boolean = False
        Try
            succes = imp.Start
            If succes Then
                Dim objPO As SparePartPO = CType(Session("sessPOHeader"), SparePartPO)
                Dim oldCode As String
                Me.checkCurrentStatus(objPO)
                oldCode = objPO.ProcessCode
                objPO.ProcessCode = "S"
                objPO.SentPODate = Date.Today
                Dim nResult As Integer = New SparePartPOFacade(User).UpdateSPPOProcessCode(objPO)
                If nResult <> -1 Then
                    Dim oDHFac As New DataHistoryFacade(User)
                    oDHFac.LogSparePartPO(objPO.ID, oldCode, objPO.ProcessCode)
                    CreateFolder(FOLDER_NAME)
                    If File.Exists(FILE_NAME) Then
                        File.Delete(FILE_NAME)
                    End If
                    'If Not (objPO.OrderType.ToLower().Equals("r") OrElse objPO.OrderType.ToLower().Equals("z")) Then
                    If Not (objPO.OrderType.ToLower().Equals("z")) Then
                        Dim fs As FileStream = New FileStream(FILE_NAME, FileMode.CreateNew)
                        Dim w As StreamWriter = New StreamWriter(fs)

                        WritePOHeaderToFile(w)
                        WritePODetailToFile(w)

                        w.Close()
                        fs.Close()
                    End If

                    Dim fhelper As FileHelper = New FileHelper
                    imp.StopImpersonate()
                    imp = Nothing
                    MessageBox.Show(ChangeSPPOStatus("S"))
                    btnSubmit.Enabled = False
                Else
                    MessageBox.Show("Proses tidak berhasil, silahkan beberapa saat lagi.")
                End If
            Else
                MessageBox.Show("Gagal Login ke SAP Server.")
            End If

        Catch ex As Exception
            MessageBox.Show(SR.DataSendFail)
        End Try
    End Sub

    Private Sub WritePOHeaderToFile(ByRef w As StreamWriter)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim spPO As SparePartPO = CType(Session("SPartPO"), SparePartPO)
        Dim pad As Char = " "
        sbSetARecord.Append("T")
        sbSetARecord.Append(txtPONumber.Text.PadRight(15, pad))
        sbSetARecord.Append(Left(lblDealerName.Text, 25).PadRight(25, pad))
        'sbSetARecord.Append(lblDealerName.Text.PadRight(24, pad))
        sbSetARecord.Append(String.Format("{0:yyyyMMdd}", icOrderDate.Value))
        sbSetARecord.Append(CType(Session("sessPODetail"), ArrayList).Count.ToString.PadLeft(4, "0"))
        If Not IsNothing(spPO.TermOfPayment) And (spPO.OrderType = "R" Or spPO.OrderType = "I" Or spPO.OrderType = "Z") Then
            sbSetARecord.Append(spPO.TermOfPayment.TermOfPaymentCode)
        End If
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

    Private Sub checkCurrentStatus(ByVal spPO As SparePartPO)
        Dim oldStatus As String = spPO.ProcessCode
        spPO.SyncProcessCode()
        If spPO.ProcessCode.Trim <> oldStatus.Trim Then
            Session.Item("SPartPO") = spPO
            'MessageBox.Show("Proses Gagal. Data telah diupdate oleh user lain") '. Silahkan Refresh halaman terlebih dahulu")
            Me._sesshelper.SetSession("isRefresh", "1")
            Response.Redirect("FrmUploadSPPOExcel.aspx")
        End If
    End Sub

    Private Function ChangeSPPOStatus(ByVal stt As String) As String
        Dim objPO As SparePartPO = CType(Session("sessPOHeader"), SparePartPO)
        Me.checkCurrentStatus(objPO)
        Dim strReturnMess
        If stt = "S" Then
            Select Case objPO.ProcessCode
                Case "C" : strReturnMess = "Maaf: PO sudah dibatalkan"
                Case "P" : strReturnMess = "Maaf: PO sudah diproses"
                    'Case "S" : strReturnMess = "Maaf: PO sudah dikirim"
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

        Dim nResult As Integer = New SparePartPOFacade(User).Update(objPO)
        strReturnMess = "Proses Berhasil."
        If stt = "S" Then
            strReturnMess = "PO sudah dikirim"
            btnSubmit.Enabled = False
            btnCancel.Enabled = False
        End If
        'Else
        'strReturnMess = "PO sudah dibatalkan"
        'If oldstt = "S" OrElse oldstt = "P" Then
        '    strReturnMess = strReturnMess + ", Silahkan konfirmasi ke KTB"
        'End If
        'End If
        Return strReturnMess
    End Function

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

    Private Sub InitialEditPage(ByVal nPOID As Integer)
        ViewState.Add("vsAccess", "edit")
        btnCancel.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        BindOrderType()
        DisplayTransactionResult(nPOID)
    End Sub

    Private Sub BindOrderType()
        ddlOrderType.Items.Clear()
        For Each liOrderType As ListItem In LookUp.ArraySPOrderType
            ddlOrderType.Items.Insert(0, New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        ddlOrderType.DataBind()
    End Sub

    Private Function InitialPageSession() As Boolean 'ByVal nID As Integer)
        If Not IsNothing(Session("DEALER")) Then
            ViewState.Add("vsAccess", "insert")
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

            btnSubmit.Attributes.Add("OnClick", "return confirm('" & SR.SubmitConfirmation & "');")
            Return True
        End If
        Return False
    End Function

    Private Sub ActivateUserPrivilege()
        btnSave.Visible = False
        btnCancel.Visible = False
        btnSubmit.Visible = False
        btnNew.Visible = False
        btnPrint.Visible = False
        'if this modul isn't blocked, privileges can be implemented here
        If Not IsTransBlocked() Then
            '-- Assign privileges
            DataFile.Visible = SecurityProvider.Authorize(Context.User, SR.BrowseSPPPO_UploadExcel_Privilege)
            btnUpload.Visible = SecurityProvider.Authorize(Context.User, SR.UploadSPPPO_UploadExcel_Privilege)
            btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.SaveSPPPO_UploadExcel_Privilege)
            btnNew.Visible = SecurityProvider.Authorize(Context.User, SR.NewUploadViaExcel_Privilege)
            btnCancel.Visible = SecurityProvider.Authorize(Context.User, SR.CancelSPPPO_UploadExcel_Privilege)
            btnSubmit.Visible = SecurityProvider.Authorize(Context.User, SR.SubmitSPPPO_UploadExcel_Privilege)
        End If
    End Sub

#End Region

#Region " Event Handler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack() Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewSPPPO_UploadExcel_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Pemesanan-Upload Melalui File Excel")
            End If
            If Me._sesshelper.GetSession("isRefresh") = "1" Then
                MessageBox.Show("Proses Gagal. Data telah diupdate oleh user/windows lain")
            End If

            lblSearchPQRNo.Attributes("onclick") = "ShowPQRNoSelection();"
            Me._sesshelper.SetSession("isRefresh", "0")
            If IsTransBlocked() Then
                Server.Transfer("../FrmAccessDenied.aspx?mess=Saat%20ini%20Modul%20Entri%20Pesanan%20Anda%20dalam%20status%20dikunci")
            End If

            If Not IsNothing(Request.QueryString("poid")) Then
                InitialEditPage(CType(Request.QueryString("poID"), Integer))
            Else
                If InitialPageSession() Then
                    BindPODetail()

                Else
                    MessageBox.Show(SR.DataNotFound("Dealer"))
                End If
            End If

            ddlTermOfPayment.ClearSelection()
            _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
            ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))

            ddlTermOfPayment.Enabled = False
        Else
            If Request.Form("hid_f") = "1" Then
                ' MessageBox.Show("OK")
            Else
                ' MessageBox.Show("NO")
            End If

        End If
        'GeneralScript.InitPopUp(Me)
        ControlsScriptInjection()
        ActivateUserPrivilege()  '-- Assign privileges
    End Sub

    Protected Sub ddlOrderType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ddlOrderTypeT As DropDownList = CType(sender, DropDownList)
        _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
        If _sparePartPOTypeTOP.IsTOP Then
            ddlTermOfPayment.Enabled = True
            BindDdlPaymentType()
            'btnSave.Enabled = ValidateTOP(_sparePartPOTypeTOP.ID)
            ValidateTOP(_sparePartPOTypeTOP.ID)
        Else
            ddlTermOfPayment.ClearSelection()
            ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
            ddlTermOfPayment.Enabled = False
            'btnSave.Enabled = True
        End If

        If ddlOrderType.SelectedValue = "P" Then
            trPQRNo.Visible = True
            txtPQRNo.Enabled = True
            lblSearchPQRNo.Visible = True
        Else
            trPQRNo.Visible = False
            txtPQRNo.Text = ""
        End If
    End Sub

    Private Function ValidateTOP(ByVal typeTopId As Integer) As Boolean
        ' Check the dealer first is TOP or None TOP
        _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
        Dim dlr As Dealer = CType(Session("DEALER"), Dealer)
        Dim spCa As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccountFacade(User).RetrieveByDealerCode(dlr.DealerCode)
        If spCa.Status = 0 Then
            ddlTermOfPayment.Items.Clear()
            ddlTermOfPayment.Enabled = False
            ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
            ddlTermOfPayment.SelectedIndex = 0
            ViewState("COD") = True
            Return True
        Else
            Dim result As Boolean = True
            Dim counterTOP As Integer = 0
            Dim counterCOD As Integer = 0
            'Validate TOP
            _arlPODetail = CType(Session("sessPODetail"), ArrayList)
            For Each PODetail As SparePartPODetail In _arlPODetail
                'DATE : 2019-04-11
                'VAlidasi PODetail SparePartMaster Not Is Nothing
                'By : Didi
                If Not IsNothing(PODetail.SparePartMaster) Then
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
                End If
            Next
            If counterTOP > 0 And counterCOD > 0 Then
                MessageBox.Show("Sehubungan dengan TOP, material-material ini tidak bisa dibuat dalam PO ini")
                result = False
            ElseIf counterTOP = 0 And counterCOD > 0 Then
                ddlTermOfPayment.ClearSelection()
                ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
                ddlTermOfPayment.Enabled = False
            End If
            Return result
        End If

    End Function

    Private Sub ControlsScriptInjection()
        btnCancel.Attributes.Add("OnClick", "return confirm('" & SR.CancelConfirmation & "');")
        btnSubmit.Attributes.Add("OnClick", "return confirm('" & SR.SubmitConfirmation & "');")
    End Sub

    Private Function IsTransBlocked() As Boolean
        Dim nVal As Integer = New DealerFacade(User).ValidateBlockedTransactionControl(CType(Session("DEALER"), Dealer).ID, _
        CType(EnumDealerTransType.DealerTransKind.POSparePart, String))
        Return nVal > 0
    End Function

    Private Sub dtgPODetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPODetail.ItemDataBound
        If Not e.Item.ItemIndex = -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgPODetail.CurrentPageIndex * dtgPODetail.PageSize)
        End If
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

    Private Sub dtgPODetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPODetail.ItemCommand
        _arlPODetail = CType(Session("sessPODetail"), ArrayList)
        _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim txtPartNumber As TextBox = CType(e.Item.FindControl("txtFPartNumber"), TextBox)
                Dim txtQty As TextBox = CType(e.Item.FindControl("txtFQTY"), TextBox)
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
                If IsNothing(txtQty) OrElse txtQty.Text = String.Empty OrElse CType(txtQty.Text.Trim, Integer) <= 0 Then
                    MessageBox.Show("Jumlah Pesanan tidak boleh kosong/0")
                    If Not (IsNothing(objPart) OrElse objPart.ID = 0) Then
                        RenderPartItem(objPart, e, "lblFPartName", "lblFRetailPrice", "lblFPOAmount")
                    End If
                    Return
                End If

                If Not (IsNothing(objPart) Or objPart.ID = 0) Then
                    'New LOC 
                    'DATE : 2014 -08 -15
                    'On behalf VAlidasi I,E, & A
                    'By : Ali
                    If objPart.TypeCode.ToUpper = "I" OrElse objPart.TypeCode.ToUpper = "E" OrElse objPart.TypeCode = "X" Then
                        MessageBox.Show("Untuk Sparepart dengan Type I,E,X harap dipesan lewat menu Indent Part")
                        Return
                    End If

                    'DATE : 2019-04-11
                    'VAlidasi Type Code P
                    'By : Didi
                    If objPart.TypeCode.ToUpper = "P" Then
                        MessageBox.Show("Untuk Sparepart dengan Type P harap hubungi Customer Support")
                        Return
                    End If

                    If objPart.ActiveStatus <> CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short) Then
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
                            intPQRHeaderID = objPQRHeader.ID
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
                            MessageBox.Show("No.Part yang diinput tidak sesuai dengan nomor barang pada PQR")
                            Return
                        End If
                    End If

                    'Validate TOP
                    If _sparePartPOTypeTOP.IsTOP Then
                        Dim sparePartMasterTop As SparePartMasterTOP = New SparePartMasterTOPFacade(User).RetrieveBySPIDandTypeTOPID(objPart.ID, _sparePartPOTypeTOP.ID)

                        If Not IsNothing(sparePartMasterTop.SparePartPOTypeTOP) Then
                            If sparePartMasterTop.Status And sparePartMasterTop.SparePartPOTypeTOP.IsTOP And _arlPODetail.Count = 0 Then
                            ElseIf (Not sparePartMasterTop.Status Or Not sparePartMasterTop.SparePartPOTypeTOP.IsTOP) And _arlPODetail.Count > 0 Then
                                MessageBox.Show("Sehubungan dengan TOP material ini tidak bisa dibuat dalam PO ini")
                                Return
                            End If
                        End If
                    End If
                    ' END OF NEW LOC
                    If Not PartIsExist(txtPartNumber.Text, _arlPODetail) Then
                        Dim objPODetail As SparePartPODetail = New SparePartPODetail
                        objPODetail.Quantity = CType(IIf(txtQty.Text = "", "0", txtQty.Text), Integer)
                        objPODetail.SparePartMaster = objPart
                        objPODetail.CheckListStatus = String.Empty
                        objPODetail.RetailPrice = objPart.RetalPrice
                        _arlPODetail.Add(objPODetail)
                    Else
                        MessageBox.Show(SR.DataIsExist("Spare Part"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Spare Part"))
                End If
            Case "edit" 'Edit mode activated
                dtgPODetail.ShowFooter = False
                dtgPODetail.EditItemIndex = e.Item.ItemIndex
            Case "delete" 'Delete this datagrid item 
                _arlPODetail.RemoveAt(e.Item.ItemIndex)
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
                        intPQRHeaderID = objPQRHeader.ID
                    Else
                        MessageBox.Show("Nomor PQR tidak terdaftar")
                        Return
                    End If
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SparePartMaster), "TypeCode", MatchType.NotInSet, "'I','E','A'"))
                    If intPQRHeaderID > 0 Then
                        Dim strSQL As String = "SELECT Distinct SparePartMasterID FROM PQRPartsCode WHERE PQRHeaderID = " & intPQRHeaderID
                        criterias.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.InSet, "(" + strSQL + ")"))
                    End If
                    criterias.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.Exact, objPart.ID))
                    Dim arrSparePartMaster As ArrayList = New SparePartMasterFacade(User).Retrieve(criterias)
                    If Not IsNothing(arrSparePartMaster) AndAlso arrSparePartMaster.Count > 0 Then
                    Else
                        MessageBox.Show("No.Part yang diinput tidak sesuai dengan nomor barang pada PQR")
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
                    'New LOC 
                    'DATE : 2014 -08 -15
                    'On behalf VAlidasi I,E, & A
                    'By : Ali
                    If objPart.TypeCode.ToUpper = "I" OrElse objPart.TypeCode.ToUpper = "E" OrElse objPart.TypeCode.ToUpper = "X" OrElse objPart.TypeCode.ToUpper = "A" Then
                        MessageBox.Show("Untuk Sparepart dengan Type I,E,X dan A harap dipesan lewat menu Indent Part")
                        Return
                    End If

                    If objPart.TypeCode.ToUpper = "P" Then
                        MessageBox.Show("Untuk Sparepart dengan Type P harap hubungi Customer Support")
                        Return
                    End If

                    If objPart.ActiveStatus <> CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short) Then
                        MessageBox.Show("No.Part Tidak Aktif")
                        Return
                    End If
                    'Validate TOP
                    If _sparePartPOTypeTOP.IsTOP Then
                        Dim sparePartMasterTop As SparePartMasterTOP = New SparePartMasterTOPFacade(User).RetrieveBySPIDandTypeTOPID(objPart.ID, _sparePartPOTypeTOP.ID)

                        If Not IsNothing(sparePartMasterTop.SparePartPOTypeTOP) Then
                            If sparePartMasterTop.Status And sparePartMasterTop.SparePartPOTypeTOP.IsTOP And _arlPODetail.Count = 0 Then
                            ElseIf (Not sparePartMasterTop.Status Or Not sparePartMasterTop.SparePartPOTypeTOP.IsTOP) And _arlPODetail.Count > 0 Then
                                MessageBox.Show("Sehubungan dengan TOP material ini tidak bisa dibuat dalam PO ini")
                                Return
                            End If
                        End If
                    End If
                    ' END OF NEW LOC

                    If Not PartIsExist(txtPartNumber.Text, _arlPODetail, e.Item.ItemIndex) Then
                        Dim objPODetail As SparePartPODetail = CType(_arlPODetail(e.Item.ItemIndex), SparePartPODetail)
                        objPODetail.Quantity = CType(txtQty.Text, Integer)
                        objPODetail.SparePartMaster = objPart
                        objPODetail.RetailPrice = objPart.RetalPrice
                        objPODetail.ErrorMessage = ""  '-- Reset error message
                        dtgPODetail.EditItemIndex = -1
                        dtgPODetail.ShowFooter = True
                    Else
                        MessageBox.Show(SR.DataIsExist("Spare Part"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Spare Part"))
                End If
            Case "cancel" 'Cancel Update this datagrid item 
                dtgPODetail.EditItemIndex = -1
                dtgPODetail.ShowFooter = True
        End Select

        _sesshelper.SetSession("sessPODetail", _arlPODetail)
        BindPODetail()

        'Dim sparePartPOTypeTOP As SparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
        btnSave.Enabled = btnSave.Enabled And ValidateTOP(_sparePartPOTypeTOP.ID)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If ddlOrderType.SelectedValue = "I" Or ddlOrderType.SelectedValue = "K" Then
            MessageBox.Show("Type Indent Part atau Khusus tidak diperbolehkan")
            Exit Sub
        End If

        _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
        If ViewState("TOPDealer") AndAlso ddlTermOfPayment.SelectedIndex = 0 AndAlso ddlTermOfPayment.SelectedItem.Text <> _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description Then
            MessageBox.Show("Silahkan Pilih Cara Pembayaran")
            Exit Sub
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
            End If
        End If

        If CType(Session("sessPODetail"), ArrayList).Count > 0 Then

            Dim nResult As Integer
            Select Case CType(ViewState("vsAccess"), String)
                Case "insert"
                    If icOrderDate.Value <= DateTime.Now Then
                        nResult = InsertNewPO()
                        If nResult <> -1 Then
                            ViewState("vsAccess") = "edit"
                            DisplayTransactionResult(nResult)
                            btnSave.Enabled = False
                            MessageBox.Show(SR.SaveSuccess)
                        Else
                            MessageBox.Show(SR.SaveFail)
                        End If
                    Else
                        MessageBox.Show("Tanggal Order tidak valid")
                    End If
                Case "edit"

                    If Not IsNothing(Session("sessPOHeader")) Then
                        Dim objPOHeader As SparePartPO = New SparePartPOFacade(User).Retrieve(CType(Session("sessPOHeader"), SparePartPO).PONumber)
                        If Not IsNothing(objPOHeader) Then

                            'if PO Status updated by someone else during editing time, add by wdi 20161026
                            If objPOHeader.ProcessCode <> "" Then 'PO status is not NEW anymore
                                MessageBox.Show("Simpan data tidak berhasil, status PO sudah diubah")
                                Return
                            End If

                            _sesshelper.SetSession("sessPOHeader", objPOHeader)

                        End If

                    End If

                    MergePODetail()
                    nResult = EditPO()
                    If nResult <> -1 Then
                        MessageBox.Show(SR.SaveSuccess)
                        'Response.Redirect("../SparePart/frmSPPOList.aspx")
                    Else
                        MessageBox.Show(SR.SaveFail)
                    End If
            End Select
        Else
            MessageBox.Show(SR.GridIsEmpty("PO Detail"))
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ViewMode(True)
        txtPONumber.Text = "[Dibuat oleh sistem]"
        icOrderDate.Value = Date.Now
        ddlOrderType.SelectedValue = "E"
        ddlTermOfPayment.ClearSelection()
        BindDdlPaymentType()
        ddlTermOfPayment.Enabled = False
        _arlPODetail.Clear()
        _sesshelper.SetSession("sessPODetail", _arlPODetail)
        InitialPageSession()
        BindPODetail()
        txtPQRNo.Enabled = True
        lblSearchPQRNo.Visible = True

        btnUpload.Enabled = True  '-- Enable button <Upload> upon new data entry
        ddlOrderType_SelectedIndexChanged(Nothing, Nothing)

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
        Dim ObjDealer As Dealer = CType(Session("DEALER"), Dealer)

        If ddlOrderType.SelectedValue = "R" AndAlso ddlTermOfPayment.SelectedValue <> 1 Then
            '1
            Dim availCeiling As Double = New CommonFunction().IsTOPCeilingValid(CType(_sesshelper.GetSession("DEALER"), Dealer))
            Dim sumOrder As Double = getSumOrder()
            If sumOrder > availCeiling Then
                MessageBox.Show("Pengajuan anda ditolak dikarenakan total order melebihi sisa ceiling (RP. " & availCeiling.ToString("N0") & "). \n Silahkan melakukan order kembali dengan tidak melewati sisa ceiling yang tersedia.\n")
                Exit Sub
            End If

            '2
            Dim topDays As Integer = CInt(ddlTermOfPayment.SelectedValue)
            Dim calcDate As Date = DateAdd(DateInterval.Day, topDays, Date.Now)
            Dim dateDisplay As Integer = -1
            If Not New CommonFunction().IsTOPTransferControlValid(CType(_sesshelper.GetSession("DEALER"), Dealer), calcDate, dateDisplay) Then
                'MessageBox.Show("Pengajuan anda ditolak dikarenakan Validitas Jaminan sudah expired, silahkan hubungi Accounting Dealer atau MMKSI CCD")
                If dateDisplay > 0 Then
                    MessageBox.Show("Pengajuan anda ditolak dikarenakan jangka waktu TOP melebihi sisa validitas jaminan yang tersedia (" & dateDisplay & " Hari). Silahkan melakukan order kembali dengan tidak melewati sisa validitas jaminan yang tersedia.")
                Else
                    MessageBox.Show("Tidak ada data transfer control untuk credit account tersebut")
                End If
                Exit Sub
            End If

            '3
            If Not New CommonFunction().IsTOPValid(CType(_sesshelper.GetSession("DEALER"), Dealer), ViewState("NotComplete")) Then
                Dim strMess As String = "Pengajuan anda ditolak dikarenakan masih ada Outstanding TOP yang belum melakukan payment confirmation dan status payment sampai validasi No Reg.\n"
                If Not IsNothing(ViewState("NotComplete")) Then
                    strMess = strMess & ViewState("NotComplete") & ".\n"
                End If
                MessageBox.Show(strMess)
                Exit Sub
            End If
        End If

        Dim sMessage As String = ""
        sMessage = New OrderRestrictionFacade(User).isOrderRestricted(ObjDealer, ddlOrderType.SelectedValue)
        If sMessage.Length > 0 Then
            MessageBox.Show(sMessage)
        Else
            CreateTextFileForKTB()
            BindPODetail()
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            'cek maxFileSize first
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

            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                If imp.Start() Then

                    '-- Copy data file from client to server temporary folder
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)

                    imp.StopImpersonate()
                    imp = Nothing

                    '-- Declare & instantiate parser
                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                    Dim parser As IExcelParser = New UploadSPPOExcelParser(companyCode)

                    '-- Parse data file and store result into arraylist
                    Dim _arlPODetail As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1$]", "User"), ArrayList)
                    If Not IsNothing(_arlPODetail) AndAlso _arlPODetail.Count > 0 Then
                        If ddlOrderType.SelectedValue = "P" Then    ''Jika type Ordernya dari Emergency PQR 
                            Dim intPQRHeaderID As Integer = 0
                            If txtPQRNo.Text.Trim <> "" Then
                                Dim objPQRHeader As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
                                If Not IsNothing(objPQRHeader) AndAlso objPQRHeader.ID > 0 Then
                                    intPQRHeaderID = objPQRHeader.ID
                                    For Each objSPPODtl As SparePartPODetail In _arlPODetail
                                        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                        crit.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
                                        crit.opAnd(New Criteria(GetType(SparePartMaster), "TypeCode", MatchType.NotInSet, "'I','E','A'"))
                                        If intPQRHeaderID > 0 Then
                                            Dim strSQL As String = "SELECT Distinct SparePartMasterID FROM PQRPartsCode WHERE PQRHeaderID = " & intPQRHeaderID
                                            crit.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.InSet, "(" + strSQL + ")"))
                                        End If
                                        crit.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.Exact, objSPPODtl.SparePartMaster.ID))
                                        Dim arrSparePartMaster As ArrayList = New SparePartMasterFacade(User).Retrieve(crit)
                                        If Not IsNothing(arrSparePartMaster) AndAlso arrSparePartMaster.Count > 0 Then
                                        Else
                                            objSPPODtl.ErrorMessage &= "No. Part yang diinput tidak sesuai dengan No. Part pada PQR"
                                        End If
                                    Next
                                Else
                                    MessageBox.Show("Nomor PQR tidak ditemukan di DNet")
                                    Exit Sub
                                End If
                            Else
                                MessageBox.Show("Jika Type Ordernya Emergency PQR, Nomor PQR harus diisi")
                                Exit Sub
                            End If
                        End If
                    End If

                    '-- Save PO details to session
                    _sesshelper.SetSession("sessPODetail", _arlPODetail)

                    dtgPODetail.DataSource = Nothing  '-- Reset datagrid first
                    BindPODetail()  '-- Bind PO details to datagrid

                    _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
                    Dim resVal As Boolean = ValidateTOP(_sparePartPOTypeTOP.ID)
                    If Not ViewState("COD") Then
                        btnSave.Enabled = btnSave.Enabled And resVal
                    End If
                    btnUpload.Enabled = False  '-- Disable button <Upload> if successfully upload data
                End If

            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        MessageBox.Show(ChangeSPPOStatus("C"))
        btnCancel.Enabled = False
        BindPODetail()
        btnSave.Enabled = False
        btnSubmit.Enabled = False
        If CType(ViewState("vsAccess"), String) = "edit" Then
            Response.Redirect("../SparePart/frmSPPOList.aspx")
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        _arlPODetail = CType(Session("sessPODetail"), ArrayList)
        lblPOAmount.Text = String.Format("{0:#,##0}", CalculatePOAmount(_arlPODetail))

        dtgPODetail.DataSource = _arlPODetail
        dtgPODetail.DataBind()
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

    Private Sub txtPQRNo_TextChanged(sender As Object, e As EventArgs) Handles txtPQRNo.TextChanged
        Try
            BindPODetail()
        Catch
        End Try
    End Sub

#End Region

    Protected Sub btnDownloadSample_Click(sender As Object, e As EventArgs) Handles btnDownloadSample.Click
        Dim strName As String = "UploadSPPO.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Template\Parts\" & strName)
    End Sub
End Class
