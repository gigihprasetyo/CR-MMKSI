#Region "Custom Namespace Imports"
Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.BusinessFacade.Service
Imports KTB.DNET.BusinessFacade.SparePart
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade.Helper
Imports KTB.DNET.UI.Helper
Imports KTB.DNET.Utility
Imports KTB.DNET.Parser
Imports KTB.DNET.Security
#End Region
#Region " .NET Namespace "
Imports System.IO
Imports System.Text
#End Region

Public Class FrmUploadPemesanan
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblDataErr As System.Web.UI.WebControls.Label
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents dgSparePart As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblErrorMsg As System.Web.UI.WebControls.Label
    'Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList

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
    Private _iSeqNum As Integer  '-- Sequence number
    Private _Dealer As Dealer    '-- Dealer object
    Private _sessHelper As SessionHelper = New SessionHelper
    Private _arlSparePartPO As ArrayList = New ArrayList
    Dim dt As DateTime = DateTime.Now
    Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
#End Region

#Region "Event handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        Page.Server.ScriptTimeout = 600



        If Not IsPostBack Then
            BindOrderType()
            'ddlTermOfPayment.Enabled = False
            ViewState("CurrentSortColumn") = ""
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            ViewState.Add("IsErrorInData", True)
            dgSparePart.DataSource = New ArrayList
            dgSparePart.DataBind()
        End If
    End Sub

    Private Sub dgSparePart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSparePart.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Try
                '-- No
                e.Item.Cells(0).Text = (dgSparePart.CurrentPageIndex * dgSparePart.PageSize + e.Item.ItemIndex + 1).ToString
                e.Item.Cells(0).Text = e.Item.Cells(0).Text & "<DIV style=""WIDTH: 100%"">&nbsp;</DIV>"
                e.Item.Cells(0).VerticalAlign = VerticalAlign.Top

                Dim topDesc As String = ""
                Dim oSP As SparePartPO = CType(e.Item.DataItem, SparePartPO)
                Dim top As String = ""

                Dim sparePartPOTypeTOP As SparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
                If sparePartPOTypeTOP.IsTOP Then
                    top = ValidateTOP(oSP.SparePartPODetails, sparePartPOTypeTOP.ID)
                End If
                If Not IsNothing(oSP.TermOfPayment) Then
                    If top = "Mixed" And oSP.TermOfPayment.ID <> 1 Then
                        oSP.ErrorMessage = "Sehubungan dengan TOP, material-material ini tidak bisa dibuat dalam PO ini"
                        btnSave.Enabled = False
                    ElseIf top = "TOP" Then
                        If Not IsNothing(oSP) Then
                            If Not IsNothing(oSP.TermOfPayment) Then
                                topDesc = oSP.TermOfPayment.Description
                            End If
                        End If
                    Else
                        topDesc = "COD"
                    End If
                    If oSP.TermOfPayment.ID <> 1 And topDesc = "COD" Then
                        oSP.ErrorMessage = "Cara pembayaran tidak sesuai dengan part"
                        btnSave.Enabled = False
                    End If

                End If
                

                
                '--dealercode
                If Not IsNothing(oSP) Then
                    If Not IsNothing(oSP.Dealer) Then
                        e.Item.Cells(1).Text = oSP.Dealer.DealerCode
                    Else
                        e.Item.Cells(1).Text = "N/A"
                    End If
                Else
                    e.Item.Cells(1).Text = "N/A"
                End If
                e.Item.Cells(1).VerticalAlign = VerticalAlign.Top
                '-- PO Date
                If Not IsNothing(oSP) Then
                    e.Item.Cells(2).Text = oSP.DeliveryDate
                Else
                    e.Item.Cells(2).Text = "N/A"
                End If
                e.Item.Cells(2).VerticalAlign = VerticalAlign.Top

                '-- Picking Ticket
                If Not IsNothing(oSP) Then
                    e.Item.Cells(3).Text = oSP.PickingTicket
                Else
                    e.Item.Cells(3).Text = "N/A"
                End If
                e.Item.Cells(3).VerticalAlign = VerticalAlign.Top

                '-- Cara Pembayaran
                If Not IsNothing(oSP) And Not top = "" And Not IsNothing(oSP.TermOfPayment) Then
                    e.Item.Cells(4).Text = oSP.TermOfPayment.Description
                Else
                    e.Item.Cells(4).Text = topDesc
                End If
                e.Item.Cells(4).VerticalAlign = VerticalAlign.Top

                '-- Pesan
                If Not IsNothing(oSP) Then
                    e.Item.Cells(5).Text = oSP.ErrorMessage
                Else
                    e.Item.Cells(5).Text = " - "
                End If
                e.Item.Cells(5).VerticalAlign = VerticalAlign.Top

                Dim aCls As New ArrayList
                Dim oCls As ClsSPDetail
                For Each oSPPOD As SparePartPODetail In oSP.SparePartPODetails
                    oCls = New ClsSPDetail
                    If Not oSPPOD.SparePartMaster Is Nothing Then
                        oCls.PartNumber = oSPPOD.SparePartMaster.PartNumber
                        oCls.Price = oSPPOD.SparePartMaster.RetalPrice
                    Else
                        oCls.PartNumber = " - "
                        oCls.Price = 0
                    End If
                    oCls.Quantity = oSPPOD.Quantity
                    oCls.ErrorMessage = oSPPOD.ErrorMessage
                    aCls.Add(oCls)
                Next

                If oSP.SparePartPODetails.Count > 0 Then
                    Dim dtgSPPODetails As DataGrid = New DataGrid
                    dtgSPPODetails.Width = Unit.Percentage(100)
                    dtgSPPODetails.BorderWidth = Unit.Pixel(1)
                    dtgSPPODetails.CellPadding = 2
                    dtgSPPODetails.CellSpacing = 0
                    dtgSPPODetails.GridLines = GridLines.Both
                    dtgSPPODetails.BorderColor = Color.FromName("Black")
                    dtgSPPODetails.HeaderStyle.BackColor = Color.FromName("White")
                    dtgSPPODetails.HeaderStyle.ForeColor = Color.FromName("Black")
                    dtgSPPODetails.HeaderStyle.Font.Bold = True
                    dtgSPPODetails.HeaderStyle.Font.Size = FontUnit.XSmall
                    dtgSPPODetails.ItemStyle.Font.Name = "Verdana"
                    dtgSPPODetails.ItemStyle.Font.Size = FontUnit.XSmall
                    dtgSPPODetails.AlternatingItemStyle.BackColor = Color.FromName("Gainsboro")
                    dtgSPPODetails.AutoGenerateColumns = False

                    Dim _boundColumn As BoundColumn = New BoundColumn
                    _boundColumn.HeaderText = "Part Number"
                    _boundColumn.DataField = "PartNumber"
                    _boundColumn.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Percentage(25)
                    _boundColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    _boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Left
                    dtgSPPODetails.Columns.Add(_boundColumn)

                    'Quantity
                    _boundColumn = New BoundColumn
                    _boundColumn.HeaderText = "Qty"
                    _boundColumn.DataField = "Quantity"
                    _boundColumn.DataFormatString = "{0:#,##0}"
                    _boundColumn.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Percentage(8)
                    _boundColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    _boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                    dtgSPPODetails.Columns.Add(_boundColumn)

                    'Price
                    _boundColumn = New BoundColumn
                    _boundColumn.HeaderText = "Harga"
                    _boundColumn.DataField = "Price"
                    _boundColumn.DataFormatString = "{0:#,##0}"
                    _boundColumn.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Percentage(15)
                    _boundColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    _boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                    dtgSPPODetails.Columns.Add(_boundColumn)

                    'TotalPrice
                    _boundColumn = New BoundColumn
                    _boundColumn.HeaderText = "Total Harga"
                    _boundColumn.DataField = "TotalPrice"
                    _boundColumn.DataFormatString = "{0:#,##0}"
                    _boundColumn.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Percentage(20)
                    _boundColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    _boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                    dtgSPPODetails.Columns.Add(_boundColumn)

                    'Keterangan
                    _boundColumn = New BoundColumn
                    _boundColumn.HeaderText = "Keterangan"
                    _boundColumn.DataField = "ErrorMessage"
                    _boundColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    _boundColumn.ItemStyle.ForeColor = Color.Red
                    dtgSPPODetails.Columns.Add(_boundColumn)

                    dtgSPPODetails.DataSource = aCls
                    dtgSPPODetails.DataBind()

                    e.Item.Cells(6).Controls.Add(dtgSPPODetails)
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            'cek maxFileSize first
            Dim maxFileSize As Integer = CInt(KTB.DNET.Lib.WebConfig.GetValue("MaximumFileSize"))

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
            Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNET.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                If imp.Start() Then

                    '-- Copy data file from client to server temporary folder
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)

                    imp.StopImpersonate()
                    imp = Nothing

                    '-- Declare & instantiate parser
                    Dim parser As IExcelParser = New UploadSparePartPemesanan

                    '-- Parse data file and store result into arraylist
                    Dim _arlSparePartPO As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1$]", "User"), ArrayList)

                    '-- Save PO details to session
                    _sessHelper.SetSession("sessSparePartPO", _arlSparePartPO)

                    dgSparePart.DataSource = Nothing  '-- Reset datagrid first
                    BindPODetail()  '-- Bind PO details to datagrid

                    btnUpload.Enabled = False  '-- Disable button <Upload> if successfully upload data

                End If

            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim nResult As Integer
        Dim err As String = String.Empty
        'Dim _arlNewSPPO As New ArrayList

        _arlSparePartPO = CType(Session("sessSparePartPO"), ArrayList)

        For Each sp As SparePartPO In _arlSparePartPO
            sp.OrderType = ddlOrderType.SelectedValue
            'If ddlTermOfPayment.SelectedValue <> "" Then
            '    sp.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(CType(ddlTermOfPayment.SelectedValue, Integer))

            '    Dim criterias As New CriteriaComposite(New Criteria(GetType(TOPCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    criterias.opAnd(New Criteria(GetType(TOPCreditAccount), "Dealer.DealerCode", MatchType.Exact, sp.Dealer.DealerCode))
            '    Dim topAccountList As ArrayList = New TOPCreditAccountFacade(User).Retrieve(criterias)
            '    If topAccountList.Count > 0 Then
            '        Dim acc As TOPCreditAccount = topAccountList(0)
            '        If sp.TermOfPayment.TermOfPaymentValue > acc.TermOfPayment.TermOfPaymentValue Then
            '            MessageBox.Show("TOP " + sp.TermOfPayment.Description + " yang diset melebihi TOP untuk dealer " + sp.Dealer.DealerCode + " yaitu " + acc.TermOfPayment.Description)
            '            dgSparePart.DataSource = _arlSparePartPO
            '            dgSparePart.DataBind()
            '            Return
            '        End If
            '    End If
            'Else
            '    sp.TermOfPayment = Nothing
            'End If

            nResult = InserSparePart(sp)
            If nResult = -1 Then
                err &= sp.PickingTicket & ", "
                'Else
                '    Dim objSPPO As SparePartPO = New SparePartPOFacade(User).Retrieve(sp.ID)
                '    _arlNewSPPO.Add(objSPPO)
            End If
        Next
        If err.Trim <> String.Empty Then
            MessageBox.Show("Picking Ticket " & err & SR.SaveFail)
        Else
            btnSave.Enabled = False
            MessageBox.Show(SR.SaveSuccess)
            'dipindah ke daftar pesanan dealer
            'CreateTextFileForKTB(_arlNewSPPO)

        End If
        dgSparePart.DataSource = _arlSparePartPO
        dgSparePart.DataBind()
    End Sub
#End Region

#Region "Custom"


    Private Function ValidateTOP(ByVal arlPODetail As ArrayList, ByVal SparePartPOTypeTOPID As Integer) As String
        'Dim isCOD As Boolean = True
        Dim result As String = ""
        Dim counterTOP As Integer = 0
        Dim counterCOD As Integer = 0
        'Validate TOP
        '_arlPODetail = CType(Session("sessPODetail"), ArrayList)
        For Each PODetail As SparePartPODetail In arlPODetail
            Dim sparePartMasterTop As SparePartMasterTOP = New SparePartMasterTOPFacade(User).RetrieveBySPIDandTypeTOPID(PODetail.SparePartMaster.ID, SparePartPOTypeTOPID)

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
            'MessageBox.Show("Sehubungan dengan TOP, material-material ini tidak bisa dibuat dalam PO ini")
            result = "Mixed"
        ElseIf counterTOP = 0 And counterCOD > 0 Then
            result = "COD"
        ElseIf counterTOP > 0 And counterCOD = 0 Then
            result = "TOP"
            'ddlTermOfPayment.ClearSelection()
            'ddlTermOfPayment.Items.Insert(0, New ListItem("COD", "1")) 'hardcoded temp
            'ddlTermOfPayment.Enabled = False
        End If
        Return result
    End Function

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.BuatUploadPemesananOthers_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Spare Part - Upload Pemesanan")
        End If
        'Me.btnSave.Visible = SecurityProvider.Authorize(context.User, SR.WSCUploadSave_Privilege)
    End Sub

    Protected Sub DownloadSampleButton_Click(sender As Object, e As EventArgs)
        Dim strName As String = "Upload_othersRO.xls"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Template\Parts\" & strName)
    End Sub

    Private Sub BindPODetail()
        _arlSparePartPO = CType(Session("sessSparePartPO"), ArrayList)
        Dim HeaderNo As Integer = 1
        Dim DetailNo As Integer = 1
        Dim errMsg1 As String = ""

        'Check top status

        Dim sparePartPOTypeTOP As SparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
        'If Not sparePartPOTypeTOP.IsTOP Then
        '    Return ""
        'End If

        '-- Check sparepart PO detail errors
        Dim bError As Boolean = False  '-- True if any error exists
        For Each spPO As SparePartPO In _arlSparePartPO
            If Not sparePartPOTypeTOP.IsTOP Then
                spPO.TermOfPayment = New TermOfPaymentFacade(User).Retrieve(sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID) 'COD hardcoded temp, no time 
            ElseIf IsNothing(spPO.TermOfPayment) Then
                spPO.ErrorMessage += "Term of Payment tidak valid"
            Else
                Dim listOfPayments As ArrayList = New ArrayList
                Dim spCa As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccountFacade(User).RetrieveByDealerCode(spPO.Dealer.DealerCode)

                If spCa.Status = 0 And spPO.TermOfPayment.ID <> sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID Then 'Dealer NonTop , termofpaymentid bukan nontop
                    bError = True
                    spPO.ErrorMessage = "Cara Pembayaran tidak sesuai dengan TOP Dealer."
                    'ElseIf spCa.Status = 1 And spPO.TermOfPayment.ID = 1 Then 'Dealer Top, termofpaymentid non top
                    '    bError = True
                    '    spPO.ErrorMessage = "Cara Pembayaran tidak sesuai dengan TOP Dealer."
                ElseIf spCa.Status = 1 And spCa.TermOfPaymentID = 1 And spPO.TermOfPayment.ID <> sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID Then 'Dealer NonTop , termofpaymentid bukan nontop
                    bError = True
                    spPO.ErrorMessage = "Cara Pembayaran tidak sesuai dengan TOP Dealer."
                ElseIf spCa.Status = 0 And spPO.TermOfPayment.ID = sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID Then
                    Continue For
                End If


                'If spPO.TermOfPayment.ID = 1 Then
                '    ' spPO.TermOfPayment = Nothing
                '    bError = True
                '    spPO.ErrorMessage = "Cara Pembayaran di file tidak sesuai dengan TOP Dealer."
                'End If

                'validasi untuk melihat jiga order typenya TOP dan parts nya TOP / COD

                Dim SPPOTValidation As DataTable
                Dim exist As Boolean = True
                For Each sppodtl As SparePartPODetail In spPO.SparePartPODetails
                    SPPOTValidation = New SparePartPODetailFacade(User).RetrieveSparePartPOTypeTOP_Validation(sppodtl.SparePartMaster.PartNumber, sparePartPOTypeTOP.ID)
                    If SPPOTValidation.Rows.Count = 0 Then

                        'bError = False
                        exist = False
                        Dim oTopCA As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
                        If Not IsNothing(oTopCA) Then
                            listOfPayments = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
                        End If
                        For Each item As TermOfPayment In listOfPayments

                            If item.TermOfPaymentCode = spPO.TermOfPayment.TermOfPaymentCode Then
                                exist = True
                                spPO.TermOfPayment = item
                            End If

                        Next
                    Else
                        If SPPOTValidation.Rows(0).Item(1) = SPPOTValidation.Rows(0).Item(4) Then

                            'If spPO.TermOfPayment.ID = sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID Then  'Dealer Top, termofpaymentid non top

                            '    'bError = False
                            '    'exist = False
                            '    'Dim oTopCA As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
                            '    'If Not IsNothing(oTopCA) Then
                            '    '    listOfPayments = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
                            '    'End If
                            '    'For Each item As TermOfPayment In listOfPayments
                            '    '    If Not IsNothing(spPO.TermOfPayment) Then
                            '    '        If item.TermOfPaymentCode = spPO.TermOfPayment.TermOfPaymentCode Then
                            '    '            exist = True
                            '    '            spPO.TermOfPayment = item
                            '    '        End If
                            '    '    End If
                            '    'Next

                            '    bError = True
                            '    spPO.ErrorMessage = "Cara Pembayaran tidak sesuai dengan TOP Dealer."
                            'Else

                            'bError = False
                            exist = False
                            Dim oTopCA As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
                            If Not IsNothing(oTopCA) Then
                                listOfPayments = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
                            End If
                            For Each item As TermOfPayment In listOfPayments

                                If item.TermOfPaymentCode = spPO.TermOfPayment.TermOfPaymentCode Then
                                    exist = True
                                    spPO.TermOfPayment = item
                                End If
                            Next

                            'End If


                    End If
                    End If
                Next

                If Not exist Then
                    'spPO.TermOfPayment = Nothing
                    bError = True
                    spPO.ErrorMessage = "Cara Pembayaran tidak sesuai dengan kelipatan TOP Dealer."
                    'errMsg1 = errMsg1 + "<br>Terjadi error pada HeaderNo : " + HeaderNo.ToString + " <br> Dengan Pesan Error : " + spPO.ErrorMessage + "<br>"
                End If
            End If
            DetailNo = 1
            If spPO.ErrorMessage <> String.Empty Then
                bError = True
                errMsg1 = errMsg1 + "<br>Terjadi error pada HeaderNo : " + HeaderNo.ToString + " <br> Dengan Pesan Error : " + spPO.ErrorMessage + "<br>"
                'Exit For
            End If
            For Each spPOD As SparePartPODetail In spPO.SparePartPODetails
                If spPOD.ErrorMessage <> String.Empty Then
                    errMsg1 = errMsg1 + "<br> Terjadi error pada HeaderNo : " + HeaderNo.ToString + " dan Detail No : " + DetailNo.ToString + "<br> Dengan Pesan Error : " + spPOD.ErrorMessage
                    bError = True
                    'Exit For
                End If
                DetailNo = 1 + DetailNo
            Next
            HeaderNo = HeaderNo + 1
        Next

        '-- If any error exists then unable to save into DNet database
        If bError = True Then
            btnSave.Enabled = False
            lblErrorMsg.Text = errMsg1
            'If HeaderNo > dgSparePart.PageSize Then
            lblErrorMsg.Visible = True
            'End If
        Else
            Me.btnSave.Enabled = SecurityProvider.Authorize(Context.User, SR.BuatUploadPemesananOthers_Privilege)
            'btnSave.Enabled = True
        End If

        dgSparePart.DataSource = _arlSparePartPO
        dgSparePart.DataBind()
    End Sub

    Private Sub BindDdlPaymentType()
        Dim dlr As Dealer = CType(Session("DEALER"), Dealer)
        Dim spCa As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccountFacade(User).RetrieveByDealerCode(dlr.DealerCode)
        Dim listOfPayments As ArrayList

        If dlr.Title = EnumDealerTittle.DealerTittle.KTB Then
            listOfPayments = New TermOfPaymentFacade(User).RetrieveActivePaymentTypeList()
        Else
            Dim oTopCA As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
            If Not IsNothing(oTopCA) Then
                listOfPayments = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
            End If
        End If

        'ddlTermOfPayment.DataSource = listOfPayments
        'ddlTermOfPayment.DataValueField = "ID"
        'ddlTermOfPayment.DataTextField = "Description"
        'ddlTermOfPayment.DataBind()
        'ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
    End Sub

    Protected Sub ddlOrderType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim ddlOrderTypeT As DropDownList = CType(sender, DropDownList)
        'If ddlOrderType.SelectedValue = "Z" Then
        '    ddlTermOfPayment.Enabled = True
        '    BindDdlPaymentType()
        'Else
        '    ddlTermOfPayment.ClearSelection()
        '    ddlTermOfPayment.Items.Insert(0, New ListItem("", ""))
        '    ddlTermOfPayment.Enabled = False
        'End If
    End Sub

    Private Sub BindOrderType()
        ddlOrderType.Items.Clear()
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTB
            ddlOrderType.Items.Insert(0, New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        ddlOrderType.DataBind()
    End Sub

    Private Function InserSparePart(ByVal objSparePartPO As SparePartPO) As Integer
        Return New SparePartPOFacade(User).InsertSparePartPO(objSparePartPO, objSparePartPO.SparePartPODetails)
    End Function

    Private Sub CreateTextFileForKTB(ByVal _arlSparePartPO As ArrayList)
        Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNET.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _sapServer)
        Dim succes As Boolean = False
        Try
            succes = imp.Start
            If succes Then
                For Each objPO As SparePartPO In _arlSparePartPO
                    Dim FOLDER_NAME As String = KTB.DNET.Lib.WebConfig.GetValue("DNetServerFolder") & objPO.PONumber.Substring(1, 4)
                    Dim FILE_NAME As String = FOLDER_NAME + "\" + objPO.PONumber + ".SPC"
                    objPO.ProcessCode = "S"
                    Dim nResult As Integer = New SparePartPOFacade(User).UpdateSPPOProcessCode(objPO)
                    If nResult <> -1 Then
                        CreateFolder(FOLDER_NAME)
                        If System.IO.File.Exists(FILE_NAME) Then
                            System.IO.File.Delete(FILE_NAME)
                        End If
                        Dim fs As System.IO.FileStream = New System.IO.FileStream(FILE_NAME, System.IO.FileMode.CreateNew)
                        Dim w As System.IO.StreamWriter = New System.IO.StreamWriter(fs)

                        WritePOHeaderToFile(w, objPO)
                        WritePODetailToFile(w, objPO)

                        w.Close()
                        fs.Close()
                    Else
                        MessageBox.Show("Proses tidak berhasil, silahkan beberapa saat lagi.")
                    End If
                Next
                imp.StopImpersonate()
                imp = Nothing
                'MessageBox.Show(ChangeSPPOStatus("S"))
            Else
                MessageBox.Show("Gagal Login ke SAP Server.")
            End If

        Catch ex As Exception
            MessageBox.Show(SR.DataSendFail & ex.Message)
        End Try
    End Sub

    Private Sub CreateFolder(ByVal folderName As String)
        Dim dirInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(folderName)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub

    Private Sub WritePOHeaderToFile(ByRef w As System.IO.StreamWriter, ByVal objSPO As SparePartPO)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        sbSetARecord.Append("T")
        sbSetARecord.Append(objSPO.PONumber.PadRight(15, pad))
        sbSetARecord.Append(Left(objSPO.Dealer.DealerName, 25).PadRight(25, pad))
        sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objSPO.PODate))
        sbSetARecord.Append(objSPO.SparePartPODetails.Count.ToString.PadLeft(4, "0"))
        sbSetARecord.Append("0".ToString.PadLeft(22, "0"))
        sbSetARecord.Append(String.Format("{0:yyyyMMdd}", New Date().Now))
        If objSPO.PickingTicket.Length > 15 Then
            sbSetARecord.Append(objSPO.PickingTicket.Substring(0, 30))
        Else
            sbSetARecord.Append(objSPO.PickingTicket)
        End If

        w.WriteLine(sbSetARecord.ToString)
    End Sub

    Private Function WritePODetailToFile(ByRef w As System.IO.StreamWriter, ByVal objSPO As SparePartPO)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        Dim indek As Integer = 0
        For Each objPODetail As SparePartPODetail In objSPO.SparePartPODetails
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

#End Region

#Region "Private Class"
    Private Class ClsSPDetail
        Private _price As Decimal
        Private _totalPrice As Decimal
        Private _quantity As Decimal
        Private _partNumber As String
        Private _errorMessage As String

        Public Property Price() As Decimal
            Get
                Return _price
            End Get
            Set(ByVal Value As Decimal)
                _price = Value
            End Set
        End Property

        Public Property Quantity() As Decimal
            Get
                Return _quantity
            End Get
            Set(ByVal Value As Decimal)
                _quantity = Value
            End Set
        End Property

        Public Property TotalPrice() As Decimal
            Get
                _totalPrice = Price * Quantity
                Return _totalPrice
            End Get
            Set(ByVal Value As Decimal)
                _totalPrice = Value
            End Set
        End Property

        Public Property PartNumber() As String
            Get
                Return _partNumber
            End Get
            Set(ByVal Value As String)
                _partNumber = Value
            End Set
        End Property

        Public Property ErrorMessage() As String
            Get
                Return _errorMessage
            End Get
            Set(ByVal Value As String)
                _errorMessage = Value
            End Set
        End Property

    End Class
#End Region

End Class

