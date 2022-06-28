#Region " Summary "
'--------------------------------------------------------'
'-- Program Code : FrmUploadSPPOText.aspx              --'
'-- Program Name : PEMESANAN-Upload Melalui File Text  --'
'-- Description  :                                     --'
'--------------------------------------------------------'
'-- Programmer   : Agus Pirnadi                        --'
'-- Start Date   : Oct 10 2005                         --'
'-- Update By    :                                     --'
'-- Last Update  : Dec 21 2005                         --'
'--------------------------------------------------------'
'-- Copyright © 2005 by Intimedia                      --'
'--------------------------------------------------------'
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

Public Class FrmUploadSPPOText
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dgSPPODetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrderType As System.Web.UI.WebControls.Label
    Protected WithEvents lblPODate As System.Web.UI.WebControls.Label
    Protected WithEvents lblPONo As System.Web.UI.WebControls.Label
    Protected WithEvents lblTypeCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents lblPOTotAmnt As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTerm2 As System.Web.UI.WebControls.Label
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents hid_f As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblPOError As System.Web.UI.WebControls.Label
    Protected WithEvents lblPQRError As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents trPQRNo As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lblPQRNo As System.Web.UI.WebControls.Label

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

#Region " Private Variables "
    Private _Dealer As Dealer    '-- Dealer object
    Private _sessHelper As SessionHelper = New SessionHelper
    Private _sparePartPOTypeTOP As SparePartPOTypeTOP
#End Region

#Region " Custom Method "

    Private Sub BindDdlPaymentType()
        Dim dlr As Dealer = CType(Session("DEALER"), Dealer)
        Dim spCa As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccountFacade(User).RetrieveByDealerCode(dlr.DealerCode)

        If spCa.Status = True Then
            ViewState.Add("AllowTOP", False)
        End If

        If spCa.Status = 0 Then
            ViewState.Add("AllowTOP", True)
            ddlTermOfPayment.ClearSelection()
            If Not IsNothing(_sparePartPOTypeTOP) Then
                ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
            End If
            ddlTermOfPayment.Enabled = False
            Exit Sub
        End If

        Dim oTopCA As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
        If Not IsNothing(oTopCA) Then
            Dim listOfPayments As ArrayList = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
            ddlTermOfPayment.DataSource = listOfPayments
            ddlTermOfPayment.DataValueField = "ID"
            ddlTermOfPayment.DataTextField = "Description"
            ddlTermOfPayment.DataBind()
            ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
        End If
    End Sub

    Private Sub InitPage()
        '-- Initialize page

        lblDealerCode.Text = String.Empty
        lblDealerName.Text = String.Empty
        lblSearchTerm2.Text = String.Empty

        lblError.Text = String.Empty
        lblPOError.Text = String.Empty
        lblPQRError.Text = String.Empty

        lblOrderType.Text = String.Empty
        lblTypeCode.Text = String.Empty

        lblPONo.Text = String.Empty
        lblPODate.Text = String.Empty

        lblPOTotAmnt.Text = String.Empty

        btnUpload.Enabled = True
        btnCancel.Enabled = False
        btnSave.Enabled = False
        btnPrint.Enabled = False
        btnSubmit.Enabled = False

        dgSPPODetail.DataSource = Nothing
        dgSPPODetail.DataBind()

    End Sub

    Private Sub DisplayDealer()
        If Not IsNothing(Session("DEALER")) Then
            _Dealer = CType(Session("DEALER"), Dealer)
            lblDealerCode.Text = _Dealer.DealerCode
            lblDealerName.Text = _Dealer.DealerName
            lblSearchTerm2.Text = _Dealer.SearchTerm2
        End If
    End Sub

    Private Function bPQRExists(ByVal strPQRNo As String) As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.InSet, "(3,4)"))
        criterias.opAnd(New Criteria(GetType(PQRHeader), "PQRType", MatchType.InSet, "(0,2,3)"))
        criterias.opAnd(New Criteria(GetType(PQRHeader), "Dealer.DealerCode", MatchType.Exact, CType(Session("DEALER"), Dealer).DealerCode))
        criterias.opAnd(New Criteria(GetType(PQRHeader), "PQRNo", MatchType.Exact, strPQRNo))

        Dim _arrPQRHeader As ArrayList = New PQRHeaderFacade(User).Retrieve(criterias)
        Return _arrPQRHeader.Count <> 0

    End Function

    Private Function bPOExists(ByVal SPartPO As SparePartPO) As Boolean
        '-- Check to see if the Sparepart PO header already exists

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, SPartPO.PONumber))
        criterias.opAnd(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim _sppo As ArrayList = New SparePartPOFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
        Return _sppo.Count <> 0

    End Function

    Private Function isExist(ByVal objSPartPO As SparePartPO) As Boolean
        '-- Check to see if the Sparepart PO header already exists

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, objSPartPO.PONumber))
        Dim _sppo As ArrayList = New SparePartPOFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
        Return _sppo.Count <> 0

    End Function

    Private Sub CreateFolder(ByVal folderName As String)
        Dim dirInfo As DirectoryInfo = New DirectoryInfo(folderName)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub

    Private Sub WritePOHeaderToFile(ByRef w As StreamWriter)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim spPO As SparePartPO = CType(Session("SPartPO"), SparePartPO)
        Dim pad As Char = " "
        sbSetARecord.Append("T")
        sbSetARecord.Append(lblPONo.Text.PadRight(15, pad))
        sbSetARecord.Append(Left(lblDealerName.Text, 25).PadRight(25, pad))
        'sbSetARecord.Append(lblDealerName.Text.PadRight(25, pad))
        sbSetARecord.Append(Right(lblPODate.Text, 4) & lblPODate.Text.Substring(3, 2) & Left(lblPODate.Text, 2))
        sbSetARecord.Append(spPO.SparePartPODetails.Count.ToString.PadLeft(4, "0"))

        If Not IsNothing(spPO.TermOfPayment) And (spPO.OrderType = "R" Or spPO.OrderType = "I" Or spPO.OrderType = "Z") Then
            sbSetARecord.Append(spPO.TermOfPayment.TermOfPaymentCode)
        End If
        w.WriteLine(sbSetARecord.ToString)
    End Sub

    Private Function WritePODetailToFile(ByRef w As StreamWriter)
        Dim _arlPODetail As ArrayList = CType(Session("SPartPO"), SparePartPO).SparePartPODetails
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

    Private Sub CreateTextFileForKTB()
        Dim FOLDER_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("DNetServerFolder") & lblPONo.Text.Substring(1, 4)
        Dim FILE_NAME As String = FOLDER_NAME + "\" + lblPONo.Text + IIf(lblOrderType.Text = "Emergency", ".EOD", ".DAT")

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _sapServer)
        Dim succes As Boolean = False

        Try
            succes = imp.Start
            If succes Then
                Dim objPO As SparePartPO = CType(Session("SPartPO"), SparePartPO)
                Dim _spPOFacade As New SparePartPOFacade(System.Threading.Thread.CurrentPrincipal)
                Dim oldCode As String

                Me.checkCurrentStatus(objPO)

                _spPOFacade.AssignPOHeaderID(objPO)
                oldCode = objPO.ProcessCode
                objPO.ProcessCode = "S"
                Dim nResult As Integer = New SparePartPOFacade(User).UpdateSPPOProcessCode(objPO)
                If nResult <> -1 Then
                    Dim oDHFac As New DataHistoryFacade(User)
                    oDHFac.LogSparePartPO(objPO.ID, oldCode, objPO.ProcessCode)

                    CreateFolder(FOLDER_NAME)
                    If File.Exists(FILE_NAME) Then
                        File.Delete(FILE_NAME)
                    End If
                    'If objPO.OrderType <> "R" Then
                    Dim fs As FileStream = New FileStream(FILE_NAME, FileMode.CreateNew)
                    Dim w As StreamWriter = New StreamWriter(fs)

                    WritePOHeaderToFile(w)
                    WritePODetailToFile(w)

                    w.Close()
                    fs.Close()
                    'End If

                    Dim fhelper As FileHelper = New FileHelper
                    imp.StopImpersonate()
                    imp = Nothing
                    Dim spPO As SparePartPO = CType(Session("SPartPO"), SparePartPO)

                    If ddlTermOfPayment.SelectedValue <> "" Then
                        objPO.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(ddlTermOfPayment.SelectedValue, Integer))
                    Else
                        objPO.TermOfPayment = Nothing
                    End If
                    '-- Retrieve and assign PO header's ID
                    Dim spPOFacade As New SparePartPOFacade(System.Threading.Thread.CurrentPrincipal)
                    spPOFacade.AssignPOHeaderID(spPO)

                    '-- Change the flag SparePartPO.ProcessCode of the corresponding PO to "S-Submit"
                    oldCode = spPO.ProcessCode
                    spPO.ProcessCode = "S"  '-- "S-Submit"
                    If spPO.ProcessCode = "S" Then
                        spPO.SentPODate = Date.Today
                        'If Not (spPO.OrderType = "R" OrElse spPO.OrderType = "Z") Then
                        If Not (spPO.OrderType = "Z") Then
                            spPO.IsTransfer = 1
                        End If
                    End If
                    spPOFacade.Update(spPO)

                    'oDHFac.LogSparePartPO(objPO.ID, OldCode, objPO.ProcessCode)

                    btnCancel.Enabled = False  '-- Disable <Cancel> button
                    MessageBox.Show(SR.DataSendSucces)
                    btnSubmit.Enabled = False
                Else
                    MessageBox.Show("Proses tidak berhasil coba beberapa saat lagi.")
                End If

            Else
                MessageBox.Show("Gagal Login ke SAP Server.")
            End If

        Catch ex As Exception
            MessageBox.Show(SR.DataSendFail)
            'Dim errMess As String = ex.Message
        End Try
    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        pCompletelist.Sort(objListComparer)

    End Sub

    Private Sub ActivateUserPrivilege()
        btnSave.Visible = False
        btnCancel.Visible = False
        btnSubmit.Visible = False
        btnNew.Visible = False
        btnPrint.Visible = False
        DataFile.Visible = False
        btnUpload.Visible = False
        'if this modul isn't blocked, privileges can be implemented here
        If Not IsTransBlocked() Then
            '-- Assign privileges
            DataFile.Visible = SecurityProvider.Authorize(Context.User, SR.BrowseSPPPO_UploadText_Privilege)
            btnUpload.Visible = SecurityProvider.Authorize(Context.User, SR.UploadSPPPO_UploadText_Privilege)
            btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.SaveSPPPO_UploadText_Privilege)
            btnCancel.Visible = SecurityProvider.Authorize(Context.User, SR.CancelSPPPO_UploadText_Privilege)
            btnSubmit.Visible = SecurityProvider.Authorize(Context.User, SR.SubmitSPPPO_UploadText_Privilege)
            btnNew.Visible = SecurityProvider.Authorize(Context.User, SR.NewSPPPO_UploadText_Privilege)
        End If
    End Sub

#End Region

#Region " EventHandler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewSPPPO_UploadText_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Pemesanan-Upload Melalui File Text")
            End If
            If Me._sessHelper.GetSession("isRefresh") = "1" Then
                MessageBox.Show("Proses Gagal. Data telah diupdate oleh user/windows lain")
            End If
            Me._sessHelper.SetSession("isRefresh", "0")
            If IsTransBlocked() Then
                Server.Transfer("../FrmAccessDenied.aspx?mess=Saat%20ini%20Modul%20Entri%20Pesanan%20Anda%20dalam%20status%20dikunci")
            End If

            InitPage()  '-- Initialize page
            DisplayDealer()  '-- Display dealer from login
            BindDdlPaymentType()
        Else
            If btnSave.Enabled Then
                If Request.Form("hid_f") = "1" Then   '-- The effect of clicking button <Baru>
                    hid_f.Value = ""
                    btnSave_Click(Nothing, Nothing)
                    InitPage()  '-- Initialize page
                    DisplayDealer()  '-- Display dealer from login
                ElseIf Request.Form("hid_f") = "0" Then
                    btnSave.Enabled = False
                    btnNew_Click(Nothing, Nothing)
                End If
            End If
        End If
        ControlsScriptInjection()
        ActivateUserPrivilege()  '-- Assign privileges
    End Sub

    Private Sub ControlsScriptInjection()
        btnCancel.Attributes.Add("OnClick", "return confirm('" & SR.CancelConfirmation & "');")
        btnSubmit.Attributes.Add("OnClick", "return confirm('" & SR.SubmitConfirmation & "');")
    End Sub

    Private Function IsTransBlocked() As Boolean
        Dim nVal As Integer = New DealerFacade(User).ValidateBlockedTransactionControl(CType(Session("DEALER"), Dealer).ID, _
        CType(EnumDealerTransType.DealerTransKind.POSparePart, String))
        Return nVal > 0
    End Function

    Private Sub BindDataPO()
        Dim sshelper As SessionHelper = New SessionHelper
        Dim SPartPO As SparePartPO = CType(sshelper.GetSession("SPartPO"), SparePartPO)
        If Not SPartPO Is Nothing Then
            dgSPPODetail.DataSource = SPartPO.SparePartPODetails
            dgSPPODetail.DataBind()
        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        '-- Upload data from text file

        If Not Page.IsValid Then  '-- Postback validation
            Exit Sub
        End If

        InitPage()  '-- Initialize page

        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            'cek maxFileFirst
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

            If DataFile.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            End If

            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)   '-- Source file name
            Dim DestFile As String = Server.MapPath("") & "\..\DataFile\" & SrcFile  '-- Destination file
            Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temporary file

            _sessHelper.SetSession("DestFile", DestFile)  '-- Store Destination file path into session
            _sessHelper.SetSession("TempFile", TempFile)  '-- Store Temporary file path into session

            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                If imp.Start() Then

                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)

                    imp.StopImpersonate()
                    imp = Nothing
                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                    Dim parser As SparePartPOParser = New SparePartPOParser(companyCode)  '-- Instantiate parser

                    '-- Parse data file and store then result into SparePartPO object
                    Dim SPartPO As SparePartPO = CType(parser.ParseFixFormatFile(TempFile, "User"), SparePartPO)

                    'Dim bError As Boolean = False  '-- True if any error exists
                    Dim bError As Integer = 0  '-- True if any error exists
                    If SPartPO.ErrorMessage <> String.Empty Then
                        lblError.Text = SPartPO.ErrorMessage  '-- Display header error
                        bError = bError + 1
                    Else
                        '-- Check dealer correctness: login dealer must match input text file's dealer
                        _Dealer = CType(Session("DEALER"), Dealer)
                        If _Dealer.DealerCode <> SPartPO.Dealer.DealerCode Then
                            lblError.Text = ";Login dealer berbeda"
                            bError = bError + 1
                        End If

                        lblDealerCode.Text = SPartPO.Dealer.DealerCode
                        lblDealerName.Text = SPartPO.Dealer.DealerName
                        lblSearchTerm2.Text = SPartPO.Dealer.SearchTerm2

                        lblOrderType.Text = SPPOOrderType.OrderType(SPartPO.OrderType)
                        If SPartPO.OrderType.Trim = "P" Then
                            trPQRNo.Visible = True
                            If IsNothing(SPartPO.PQRHeader) Then
                                lblPQRError.Text = "Nomor PQR belum diisi;"
                                bError = bError + 1
                            End If
                            lblPQRNo.Text = SPartPO.PQRHeader.PQRNo
                            '-- Check if this PQR number isExist
                            If Not bPQRExists(lblPQRNo.Text.Trim) Then
                                lblPQRError.Text &= "Nomor PQR tidak ada di DNet;"
                                bError = bError + 1
                            End If
                        Else
                            trPQRNo.Visible = False
                            lblPQRNo.Text = ""
                            lblPQRError.Text = ""
                        End If
                        lblTypeCode.Text = SPartPO.OrderType
                        lblPONo.Text = SPartPO.PONumber
                        lblPODate.Text = Format(SPartPO.PODate, "dd/MM/yyyy")

                        _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(SPartPO.OrderType)
                        If Not IsNothing(_sparePartPOTypeTOP) And _sparePartPOTypeTOP.IsTOP Then
                            BindDdlPaymentType()
                            'Validate TOP
                            Dim isTopValid = ValidateTOP(_sparePartPOTypeTOP.ID, SPartPO.SparePartPODetails)
                            'btnSave.Enabled = isTopValid
                            If Not isTopValid Then
                                bError = bError + 1
                            End If
                        Else
                            ddlTermOfPayment.Items.Clear()
                            ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
                            ddlTermOfPayment.Enabled = False
                        End If

                        '-- Check if this PO number already exists in DNet
                        If bPOExists(SPartPO) Then
                            lblPOError.Text = "Nomor PO sudah ada di DNet"
                            'remarks by anh 20170202 'hasil uat, tidak boleh no po yg sama di upload.
                            'If SPartPO.ProcessCode <> "" Then
                            bError = bError + 1
                            'End If
                        End If

                        '-- Check sparepart PO detail errors
                        For Each PODetail As SparePartPODetail In SPartPO.SparePartPODetails
                            If PODetail.ErrorMessage <> String.Empty Then
                                bError = bError + 1
                                Exit For
                            End If
                        Next
                    End If

                    _sessHelper.SetSession("SPartPO", SPartPO)  '-- Store sparepart PO into session
                    If bError = 0 Then
                        btnSave.Enabled = True  '-- Enable <Save> button
                    Else
                        btnSave.Enabled = False
                    End If

                    '-- Bind PO details to datagrid
                    dgSPPODetail.DataSource = SPartPO.SparePartPODetails
                    dgSPPODetail.DataBind()

                    '-- Calculate PO's total amount
                    Dim nilaiPO As Double = 0
                    For Each PODetail As SparePartPODetail In SPartPO.SparePartPODetails
                        nilaiPO += PODetail.Amount  '-- Sum up amounts
                    Next
                    lblPOTotAmnt.Text = Format(nilaiPO, "#,##0")  '-- Display total amount

                End If

            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If

    End Sub

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

        If CType(ViewState("AllowTOP"), Boolean) = True Then
            Return True
        End If

        For Each PODetail As SparePartPODetail In spdetails
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
            ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
            ddlTermOfPayment.Enabled = False
        End If
        Return result
    End Function

    Private Sub checkCurrentStatus(ByVal spPO As SparePartPO)
        Dim oldStatus As String = spPO.ProcessCode
        spPO.SyncProcessCode()
        If spPO.ProcessCode.Trim <> oldStatus.Trim Then
            Session.Item("SPartPO") = spPO
            'MessageBox.Show("Proses Gagal. Data telah diupdate oleh user lain") '. Silahkan Refresh halaman terlebih dahulu")
            Me._sessHelper.SetSession("isRefresh", "1")
            Response.Redirect("FrmUploadSPPOText.aspx")
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        '-- Retrieve Sparepart PO from session
        Dim spPO As SparePartPO = CType(Session("SPartPO"), SparePartPO)
        Me.checkCurrentStatus(spPO)
        '-- Retrieve and assign PO header's ID
        Dim spPOFacade As New SparePartPOFacade(System.Threading.Thread.CurrentPrincipal)
        spPOFacade.AssignPOHeaderID(spPO)

        '-- Change the flag SparePartPO.ProcessCode of the corresponding PO to "C-Cancel"
        spPO.ProcessCode = "C"  '-- "C-Cancel"
        spPOFacade.Update(spPO)

        btnCancel.Enabled = False  '-- Disable <Cancel> button
        btnSubmit.Enabled = False  '-- Disable <Submit> button
        MessageBox.Show(SR.DataCanceled("PO SP"))

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        '-- Insert or update Sparepart PO header & detail

        '-- Retrieve Sparepart PO from session

        If dgSPPODetail.Items.Count = 0 Then
            MessageBox.Show("Harus Ada SparePart Detail")
            Return
        End If


        Dim spPO As SparePartPO = CType(Session("SPartPO"), SparePartPO)


        If ddlTermOfPayment.SelectedValue <> "" Then
            spPO.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(ddlTermOfPayment.SelectedValue, Integer))
        Else
            MessageBox.Show("Cara Pembayaran belum dipilih")
            Return
        End If

        Dim objPo As SparePartPO = New SparePartPOFacade(User).Retrieve(spPO.PONumber)
        If Not IsNothing(objPo) Then
            If objPo.ID > 0 Then

                'if PO Status updated by someone else during editing time, add by wdi 20161026
                If objPo.ProcessCode <> "" Then 'PO status is not NEW anymore
                    MessageBox.Show("Simpan data tidak berhasil, status PO sudah diubah")
                    Return
                End If

                spPO.ProcessCode = objPo.ProcessCode
                _sessHelper.SetSession("SPartPO", spPO)

            End If

        End If

        '-- Process sparepart PO
        Dim spPOFacade As New SparePartPOFacade(System.Threading.Thread.CurrentPrincipal)
        Dim Status As Integer = spPOFacade.InsertAndOrUpdateSPPO(spPO)

        btnSave.Enabled = False  '-- Disable <Save> button
        If Status = 1 Or Status = 2 Then
            btnCancel.Enabled = True  '-- Enable <Cancel> button
            btnSubmit.Enabled = True  '-- Enable <Submit> button
            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show(SR.SaveFail)
        End If

    End Sub

    Private Function getSumOrder() As Double
        Dim _return As Double = 0
        For Each item As DataGridItem In dgSPPODetail.Items()
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim orderAmount As Label = CType(item.FindControl("lblPOAmount"), Label)
                _return = _return + CDbl(orderAmount.Text)
            End If
        Next
        Return _return
    End Function

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim ObjDealer As Dealer = CType(Session("DEALER"), Dealer)
        If ddlTermOfPayment.SelectedValue <> 1 Then
            '1
            'Dim availCeiling As Double = New CommonFunction().IsTOPCeilingValid(CType(_sessHelper.GetSession("DEALER"), Dealer))
            'Dim sumOrder As Double = getSumOrder()
            'If sumOrder > availCeiling Then
            '    MessageBox.Show("Pengajuan anda ditolak dikarenakan total order melebihi sisa ceiling (RP. " & availCeiling.ToString("N0") & "). \n Silahkan melakukan order kembali dengan tidak melewati sisa ceiling yang tersedia.\n")
            '    Exit Sub
            'End If

            '2
            Dim topDays As Integer = CInt(ddlTermOfPayment.SelectedValue)
            Dim calcDate As Date = DateAdd(DateInterval.Day, topDays, Date.Now)
            Dim dateDisplay As Integer = -1
            'If Not New CommonFunction().IsTOPTransferControlValid(CType(_sessHelper.GetSession("DEALER"), Dealer), calcDate, dateDisplay) Then
            '    'MessageBox.Show("Pengajuan anda ditolak dikarenakan Validitas Jaminan sudah expired, silahkan hubungi Accounting Dealer atau MMKSI CCD")
            '    If dateDisplay > 0 Then
            '        MessageBox.Show("Pengajuan anda ditolak dikarenakan jangka waktu TOP melebihi sisa validitas jaminan yang tersedia (" & dateDisplay & " Hari). Silahkan melakukan order kembali dengan tidak melewati sisa validitas jaminan yang tersedia.")
            '    Else
            '        MessageBox.Show("Tidak ada data transfer control untuk credit account tersebut")
            '    End If
            '    Exit Sub
            'End If

            '3
            'If Not New CommonFunction().IsTOPValid(CType(_sessHelper.GetSession("DEALER"), Dealer), ViewState("NotComplete")) Then
            '    Dim strMess As String = "Pengajuan anda ditolak dikarenakan masih ada Outstanding TOP yang belum melakukan payment confirmation dan status payment sampai validasi No Reg.\n"
            '    If Not IsNothing(ViewState("NotComplete")) Then
            '        strMess = strMess & ViewState("NotComplete") & ".\n"
            '    End If
            '    MessageBox.Show(strMess)
            '    Exit Sub
            'End If
        End If

        Dim sMessage As String = ""
        If lblOrderType.Text.Trim() = "Emergency" Then
            sMessage = New OrderRestrictionFacade(User).isOrderRestricted(ObjDealer, "E")
        ElseIf lblOrderType.Text.Trim() = "Regular" Then
            sMessage = New OrderRestrictionFacade(User).isOrderRestricted(ObjDealer, "R")
        End If
        If sMessage.Length > 0 Then
            MessageBox.Show(sMessage)
        Else
            CreateTextFileForKTB()
        End If
        BindDataPO()
    End Sub

    Private Sub dgSPPODetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSPPODetail.ItemDataBound
        '-- Handle databinding of template columns

        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then

            Dim RowValue As SparePartPODetail = CType(e.Item.DataItem, SparePartPODetail)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgSPPODetail.CurrentPageIndex * dgSPPODetail.PageSize)
            End If

        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        If btnSave.Enabled Then
            MessageBox.Confirm("Data mau disimpan dulu?", "hid_f")
        Else
            InitPage()  '-- Initialize page
            DisplayDealer()  '-- Display dealer from login
            hid_f.Value = ""
        End If
    End Sub

    Private Sub dgSPPODetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSPPODetail.SortCommand
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

        Dim arlCompletelist As ArrayList = CType(Session("SPartPO"), SparePartPO).SparePartPODetails

        If Not arlCompletelist Is Nothing Then
            SortListControl(arlCompletelist, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            dgSPPODetail.DataSource = arlCompletelist
            dgSPPODetail.DataBind()
            '_sesshelper.SetSession("sessPODetail", arlCompletelist)
            'BindPODetail()
        End If
    End Sub

    Protected Sub btnDownloadSample_Click(sender As Object, e As EventArgs) Handles btnDownloadSample.Click
        Dim strName As String = "UploadSPPO.txt"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Template\Parts\" & strName)
    End Sub


#End Region
End Class
