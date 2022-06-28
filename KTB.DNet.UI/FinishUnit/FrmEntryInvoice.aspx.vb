#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.LKPP
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile

#End Region

Public Class FrmEntryInvoice
    Inherits System.Web.UI.Page

    Private CVGroup As String = "cust_prf_cv"
    Private PCGroup As String = "cust_prf_pc"
    Private LCVGroup As String = "cust_prf_lcv"

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblChassisNumber As System.Web.UI.WebControls.Label
    Protected WithEvents pnlPC As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlLCV As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlCV As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlCVTab As System.Web.UI.WebControls.Panel
    'Protected WithEvents pnlPCTab As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPCTab As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents pnlLCVTab As System.Web.UI.WebControls.Panel
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblEngineNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblModelTypeColor As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents icInvoiceDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtRefChassisNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDODate As System.Web.UI.WebControls.Label
    Protected WithEvents lblDiscountAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblTOPayment As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPCVehicleOwnership As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPCVehiclePurpose As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPCOwnerAge As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPCMainUsage As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLCVVehicleOwnership As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLCVVehiclePurpose As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLCVVehicleBodyShape As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLCVCustomerBusiness As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLCVMainOperationArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCVVehicleOwnership As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCVVehiclePurpose As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCVVehicleBodyShape As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCVCustomerBusiness As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCVMainOperationArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtDisclaimer As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlPCPaymentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLCVPaymentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCVPaymentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnValidate As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelValidate As System.Web.UI.WebControls.Button
    Protected WithEvents lblCreatedBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblValidatedBy As System.Web.UI.WebControls.Label
    Protected WithEvents pnlEndCustomer As System.Web.UI.WebControls.Panel
    Protected WithEvents imgAddInfo As System.Web.UI.WebControls.Image
    Protected WithEvents chkPelanggaranWilayah As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlPWPaymentMethod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPWAmount As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPWBank As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPWNomorGiro As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkPembayaranPenalti As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlPPPaymentMethod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPPAmount As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPPBank As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPPNomorGiro As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkSuratReferensi As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtSRNomorSurat As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoMCP As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoLKPP As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoFleetReq As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAddInfo As System.Web.UI.WebControls.Label
    Protected WithEvents PWval1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents PPval1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents SRval1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblPCPaymentType As System.Web.UI.WebControls.Label
    Protected WithEvents lblPCVehicleOwnership As System.Web.UI.WebControls.Label
    Protected WithEvents lblPCVehiclePurpose As System.Web.UI.WebControls.Label
    Protected WithEvents lblPCOwnerAge As System.Web.UI.WebControls.Label
    Protected WithEvents lblPCMainUsage As System.Web.UI.WebControls.Label
    Protected WithEvents lblLCVPaymentType As System.Web.UI.WebControls.Label
    Protected WithEvents lblLCVVehicleOwnership As System.Web.UI.WebControls.Label
    Protected WithEvents lblLCVVehiclePurpose As System.Web.UI.WebControls.Label
    Protected WithEvents lblLCVVehicleBodyShape As System.Web.UI.WebControls.Label
    Protected WithEvents lblLCVCustomerBusiness As System.Web.UI.WebControls.Label
    Protected WithEvents lblLCVMainOperationArea As System.Web.UI.WebControls.Label
    Protected WithEvents lblCVPaymentType As System.Web.UI.WebControls.Label
    Protected WithEvents lblCVVehicleOwnership As System.Web.UI.WebControls.Label
    Protected WithEvents lblCVVehiclePurpose As System.Web.UI.WebControls.Label
    Protected WithEvents lblCVVehicleBodyShape As System.Web.UI.WebControls.Label
    Protected WithEvents lblCVCustomerBusiness As System.Web.UI.WebControls.Label
    Protected WithEvents lblCVMainOperationArea As System.Web.UI.WebControls.Label
    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents CompareValidator3 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator4 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator5 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator6 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator7 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator8 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator9 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator10 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator11 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator12 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator13 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator14 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator15 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator16 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator17 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator18 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator19 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents PWval0 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents PPval0 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents chkDisclaimerAgreement As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents lblNamaPesananKhusus As System.Web.UI.WebControls.Label
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents lblName3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKel As System.Web.UI.WebControls.Label
    Protected WithEvents lblKec As System.Web.UI.WebControls.Label
    Protected WithEvents lblProvince As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefChassisNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblInvoiceDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblAddress As System.Web.UI.WebControls.Label
    Protected WithEvents lblName1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblPWAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblPWBank As System.Web.UI.WebControls.Label
    Protected WithEvents lblPWNomorGiro As System.Web.UI.WebControls.Label
    Protected WithEvents lblPPPaymentMethod As System.Web.UI.WebControls.Label
    Protected WithEvents lblPPAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblPPBank As System.Web.UI.WebControls.Label
    Protected WithEvents lblPPNomorGiro As System.Web.UI.WebControls.Label
    Protected WithEvents lblSRNomorSurat As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoMCP As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoLKPP As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoFleet As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRegRequest As System.Web.UI.WebControls.Label

    Protected WithEvents lblPWPaymentMethod As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents pnlCustomerCode As System.Web.UI.WebControls.Panel
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents pnlInformasion As System.Web.UI.WebControls.Panel
    Protected WithEvents chkPrintProvinceOnInvoice As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblCetak As System.Web.UI.WebControls.Label
    Protected WithEvents lblCapCetak As System.Web.UI.WebControls.Label
    Protected WithEvents lblCustCode As System.Web.UI.WebControls.Label
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents CompareValidator21 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents lblPOSCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDelimeterValidateBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitleValidateBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Protected WithEvents lblPhone As System.Web.UI.WebControls.Label
    Protected WithEvents Phone As System.Web.UI.WebControls.Panel
    Protected WithEvents lblName2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKTP As System.Web.UI.WebControls.Label
    Protected WithEvents btnBindModel As System.Web.UI.WebControls.Button
    Protected WithEvents txtNewKindID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNewModelID As System.Web.UI.WebControls.TextBox
    Protected WithEvents hdnMCPConfirmation As System.Web.UI.HtmlControls.HtmlInputHidden
    'Protected WithEvents txtMCPConfirmation As System.Web.UI.WebControls.Label
    Protected WithEvents chkMCP As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtMCPConfirmation As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblMCPNumber As System.Web.UI.WebControls.Label

    Protected WithEvents chkLKPP As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkFleet As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtLKPPConfirmation As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblLKPPNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerBranch As Label
    Protected WithEvents btnDisableLeasing As System.Web.UI.WebControls.Button
    Protected WithEvents lblTmpFaktur As Label
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        initForm()
    End Sub

#End Region

#Region " Custom Variable Declaration "

    Private _sesshelper As SessionHelper = New SessionHelper
    Private _objEndCustomer As EndCustomer
    Private _objChassisMaster As ChassisMaster
    Private _objDealer As Dealer
    Private _sErrorMessage As String = ""
    Private _sSuccessMessage As String = ""
    Private _bIsUpdateNeeded As Boolean = False
    Private _bIsValidating As Boolean = False
    Private _ctlJenisKendaraan As String = ""
    Private _ctlModelKendaraan As String = ""
    Private _ctlLeasing As String = ""
    Private _ctlKaroseri As String = ""
    Dim alGroup As ArrayList = New ArrayList
#End Region

#Region " Custom Method "

    Private Sub RenderProfilePanel(ByVal objCM As ChassisMaster, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel, ByVal IsReadOnly As Boolean)
        'IsReadOnly = False
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(IsReadOnly)

        If Not objCM Is Nothing Then
            objRenderPanel.GeneratePanel(objCM.ID, objPanel, objGroup, profileType, User)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User)
        End If
    End Sub


    Private Sub DealerToControl()
        'Me.lblDealerName.Text = _objDealer.DealerName
        'Me.lblDealerCode.Text = _objDealer.DealerCode & " / " & _objDealer.SearchTerm1
    End Sub

    Private Sub ChassisMasterToControl()
        If Not _objChassisMaster Is Nothing Then

            If Not IsNothing(_objChassisMaster) Then
                If Not IsNothing(_objChassisMaster.EndCustomer) Then
                    If Not IsNothing(_objChassisMaster.EndCustomer.SPKFaktur) Then
                        If Not IsNothing(_objChassisMaster.EndCustomer.SPKFaktur.SPKHeader) Then

                            If Not IsNothing(_objChassisMaster.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader) Then
                                If Not IsNothing(_objChassisMaster.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch) Then
                                    lblDealerBranch.Text = _objChassisMaster.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.DealerBranchCode & " / " & _objChassisMaster.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.Term1
                                End If
                            End If
                        End If

                    End If

                End If

            End If
            If Not IsNothing(_objChassisMaster.EndCustomer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) AndAlso _
            _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah And _objChassisMaster.Category.CategoryCode = "CV" Then
                Me.txtMCPConfirmation.Text = "-1"
            Else
                Me.txtMCPConfirmation.Text = "1"
            End If

            'LKPP
            If Not IsNothing(_objChassisMaster.EndCustomer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) AndAlso _
           _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah And (_objChassisMaster.Category.CategoryCode.ToUpper() = "PC" OrElse _objChassisMaster.Category.CategoryCode.ToUpper() = "LCV") Then
                Me.txtLKPPConfirmation.Text = "-1"
            Else
                Me.txtLKPPConfirmation.Text = "1"
            End If
            'EndOfLKPP
            Me.lblChassisNumber.Text = _objChassisMaster.ChassisNumber
            Me.lblEngineNumber.Text = _objChassisMaster.EngineNumber
            Me.lblModelTypeColor.Text = _objChassisMaster.VechileColor.VechileType.VechileModel.VechileModelCode & " - " & _
                    _objChassisMaster.VechileColor.VechileType.Description & " " & _
                    _objChassisMaster.VechileColor.ColorIndName
            Me.lblStatus.Text = EnumChassisMaster.FakturStatusDesc(_objChassisMaster.FakturStatus)
            If Not _objChassisMaster.EndCustomer Is Nothing Then
                Me.lblNomorFaktur.Text = _objChassisMaster.EndCustomer.FakturNumber
            Else
                Me.lblNomorFaktur.Text = String.Empty
            End If
            Me.lblDODate.Text = Format(_objChassisMaster.DODate, "dd/MM/yyyy")
            Me.lblDiscountAmount.Text = String.Format("{0:#,###}", _objChassisMaster.DiscountAmount)
            Me.lblTOPayment.Text = _objChassisMaster.TermOfPayment.Description

            Me.lblDealerName.Text = _objChassisMaster.Dealer.DealerName
            Me.lblDealerCode.Text = _objChassisMaster.Dealer.DealerCode & " / " & _objChassisMaster.Dealer.SearchTerm1

            If _objChassisMaster.SONumber.Trim <> "" Then
                Dim objPOHeader As POHeader
                Dim objPOHeaderFac As POHeaderFacade = New POHeaderFacade(User)
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, _objChassisMaster.SONumber))
                Dim list As ArrayList
                list = objPOHeaderFac.Retrieve(crit)
                If (Not IsNothing(list) AndAlso list.Count > 0) Then
                    objPOHeader = CType(list.Item(0), POHeader)
                    If objPOHeader.ID > 0 Then
                        Dim objCont As ContractHeader
                        objCont = objPOHeader.ContractHeader
                        If (Not IsNothing(objCont)) AndAlso objCont.ID > 0 Then
                            Me.lblNamaPesananKhusus.Text = objCont.ProjectName
                        End If
                    End If
                End If
            End If

            ' Adding block vehicle code from web config
            If IsVehicleTypeBlocked(_objChassisMaster.VechileColor.VechileType.VechileTypeCode) Then
                MessageBox.Show("Pengajuan faktur untuk tipe " + _objChassisMaster.VechileColor.VechileType.VechileTypeCode _
                    + " sementara tidak dapat dilakukan.")
                'ViewState.Add("disabledSave", True)
                _sesshelper.SetSession("disabledSave", True)
            End If


        End If
    End Sub

    Private Function IsVehicleTypeBlocked(ByVal vehicleCode As String) As Boolean
        Dim Retval As Boolean = False

        Dim strBlock As String = KTB.DNet.Lib.WebConfig.GetValue("BLOCK_VEHICLE_CODE").ToString().Trim()
        If Not strBlock.Equals("") Then
            Dim sep As Char = ","
            Dim arrBlock As String() = strBlock.Split(sep)
            If arrBlock.Length > 0 Then
                For Each sBlock As String In arrBlock
                    If vehicleCode = sBlock.Trim() Then
                        Retval = True
                        Exit For
                    End If
                Next
            End If
        End If
        Return Retval
    End Function

    Private ReadOnly Property IsOpenInvoice() As Boolean
        Get
            _objChassisMaster = CType(GetFromSession("ChassisMaster"), ChassisMaster)
            Return _objChassisMaster.FakturStatus = CType(EnumChassisMaster.FakturStatus.Baru, String)
        End Get
    End Property
    Private Sub BindEndCustomerAdditionalInformationToControl()
        If _objEndCustomer.AreaViolationFlag = "X" Then
            Me.chkPelanggaranWilayah.Checked = True

            If IsOpenInvoice Then
                If _objEndCustomer.AreaViolationPaymentMethodID = 0 Then
                    Me.ddlPWPaymentMethod.SelectedIndex = 0
                Else
                    Me.ddlPWPaymentMethod.SelectedValue = _objEndCustomer.AreaViolationPaymentMethodID.ToString
                End If

                Me.txtPWAmount.Text = FormatNumber(_objEndCustomer.AreaViolationyAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.txtPWBank.Text = _objEndCustomer.AreaViolationBankName
                Me.txtPWNomorGiro.Text = _objEndCustomer.AreaViolationGyroNumber
            Else
                Dim objPaymentMethod As PaymentMethod = New PaymentMethodFacade(User).Retrieve(_objEndCustomer.AreaViolationPaymentMethodID)
                If Not objPaymentMethod Is Nothing Then
                    Me.lblPWPaymentMethod.Text = objPaymentMethod.Description
                Else
                    Me.lblPWPaymentMethod.Text = String.Empty
                End If

                Me.lblPWAmount.Text = FormatNumber(_objEndCustomer.AreaViolationyAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblPWBank.Text = _objEndCustomer.AreaViolationBankName
                Me.lblPWNomorGiro.Text = _objEndCustomer.AreaViolationGyroNumber
            End If

        End If
        If _objEndCustomer.PenaltyFlag = "X" Then
            Me.chkPembayaranPenalti.Checked = True
            If IsOpenInvoice Then
                If _objEndCustomer.PenaltyPaymentMethodID = 0 Then
                    Me.ddlPPPaymentMethod.SelectedIndex = 0
                Else
                    Me.ddlPPPaymentMethod.SelectedValue = _objEndCustomer.PenaltyPaymentMethodID.ToString
                End If

                Me.txtPPAmount.Text = FormatNumber(_objEndCustomer.PenaltyAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.txtPPBank.Text = _objEndCustomer.PenaltyBankName
                Me.txtPPNomorGiro.Text = _objEndCustomer.PenaltyGyroNumber
            Else
                Dim objPaymentMethod As PaymentMethod = New PaymentMethodFacade(User).Retrieve(_objEndCustomer.PenaltyPaymentMethodID)
                If Not objPaymentMethod Is Nothing Then
                    Me.lblPPPaymentMethod.Text = objPaymentMethod.Description
                Else
                    Me.lblPPPaymentMethod.Text = String.Empty
                End If

                Me.lblPPAmount.Text = FormatNumber(_objEndCustomer.PenaltyAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Me.lblPPBank.Text = _objEndCustomer.PenaltyBankName
                Me.lblPPNomorGiro.Text = _objEndCustomer.PenaltyGyroNumber
            End If

        End If
        If _objEndCustomer.ReferenceLetterFlag = "X" Then
            Me.chkSuratReferensi.Checked = True
            If IsOpenInvoice Then
                Me.txtSRNomorSurat.Text = _objEndCustomer.ReferenceLetter
            Else
                Me.lblSRNomorSurat.Text = _objEndCustomer.ReferenceLetter
            End If

        End If

        If Not _objChassisMaster.EndCustomer.MCPHeader Is Nothing Then
            If _objChassisMaster.EndCustomer.MCPHeader.ID > 0 Then
                Me.chkMCP.Checked = True
                Me.lblNoMCP.Text = _objChassisMaster.EndCustomer.MCPHeader.ReferenceNumber
                Me.txtNoMCP.Text = _objChassisMaster.EndCustomer.MCPHeader.ReferenceNumber
            End If
        Else
            Me.txtNoMCP.Text = String.Empty
        End If

        If Not _objEndCustomer.MCPHeader Is Nothing Then
            txtNoMCP.Text = _objEndCustomer.MCPHeader.ReferenceNumber
        End If

        'LKPP
        If Not _objChassisMaster.EndCustomer.LKPPHeader Is Nothing Then
            If _objChassisMaster.EndCustomer.LKPPHeader.ID > 0 Then
                Me.chkLKPP.Checked = True
                Me.lblNoLKPP.Text = _objChassisMaster.EndCustomer.LKPPHeader.ReferenceNumber
                Me.txtNoLKPP.Text = _objChassisMaster.EndCustomer.LKPPHeader.ReferenceNumber
            End If
        Else
            Me.txtNoLKPP.Text = String.Empty
        End If

        If Not _objEndCustomer.LKPPHeader Is Nothing Then
            If _objEndCustomer.LKPPHeader.ReferenceNumber.Length > 0 Then
                txtNoLKPP.Text = _objEndCustomer.LKPPHeader.ReferenceNumber
            Else
                MessageBox.Show("No LKPP kosong")
            End If

        End If
        'LKPP

        'Fleet, add by wdi 20161011
        Dim critFleetFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critFleetFaktur.opAnd(New Criteria(GetType(FleetFaktur), "ChassisMaster.ChassisNumber", MatchType.Exact, lblChassisNumber.Text))
        Dim arrFleetFaktur As ArrayList = New FleetFakturFacade(User).Retrieve(critFleetFaktur)

        Dim objFleetFaktur As FleetFaktur
        If arrFleetFaktur.Count > 0 Then
            objFleetFaktur = arrFleetFaktur(0)
        End If

        If Not IsNothing(objFleetFaktur) Then
            Me.chkFleet.Checked = True
            Me.lblNoFleet.Text = objFleetFaktur.FleetRequest.NoRegRequest
            Me.txtNoFleetReq.Text = objFleetFaktur.FleetRequest.NoRegRequest
        End If
        'Fleet
    End Sub

    Private Function InList(ByVal ddl As DropDownList, ByVal val As String) As Boolean
        Dim returnValue As Boolean = False
        For Each item As ListItem In ddl.Items
            If val = item.Value Then
                returnValue = True
                Exit For
            Else
                returnValue = False
            End If
        Next
        Return returnValue
    End Function

    Private Sub BindEndCustomerProfileToControl()
        If IsOpenInvoice Then
            'If Not _objEndCustomer.Customer Is Nothing Then

            Select Case _objChassisMaster.Category.CategoryCode
                Case "PC"
                    RenderProfilePanel(_objChassisMaster, New ProfileGroupFacade(User).Retrieve(PCGroup), EnumProfileType.ProfileType.CHASSISMASTER, pnlInformasion, False)

                Case "CV"
                    RenderProfilePanel(_objChassisMaster, New ProfileGroupFacade(User).Retrieve(CVGroup), EnumProfileType.ProfileType.CHASSISMASTER, pnlInformasion, False)

                Case "LCV"
                    RenderProfilePanel(_objChassisMaster, New ProfileGroupFacade(User).Retrieve(LCVGroup), EnumProfileType.ProfileType.CHASSISMASTER, pnlInformasion, False)

            End Select
            'End If
        Else
            Select Case _objChassisMaster.Category.CategoryCode
                Case "PC"
                    RenderProfilePanel(_objChassisMaster, New ProfileGroupFacade(User).Retrieve(PCGroup), EnumProfileType.ProfileType.CHASSISMASTER, pnlInformasion, True)

                Case "CV"
                    RenderProfilePanel(_objChassisMaster, New ProfileGroupFacade(User).Retrieve(CVGroup), EnumProfileType.ProfileType.CHASSISMASTER, pnlInformasion, True)

                Case "LCV"
                    RenderProfilePanel(_objChassisMaster, New ProfileGroupFacade(User).Retrieve(LCVGroup), EnumProfileType.ProfileType.CHASSISMASTER, pnlInformasion, True)

            End Select
        End If
    End Sub

    Private Sub HideAllEndCustomerLabel()
        Me.lblName1.Visible = False
        Me.lblName2.Visible = False
        Me.lblName3.Visible = False
        Me.lblAddress.Visible = False
        Me.lblKel.Visible = False
        Me.lblKec.Visible = False
        Me.lblPOSCode.Visible = False
        Me.lblProvince.Visible = False
        Me.lblKTP.Visible = False
        Me.lblCity.Visible = False
        Me.lblEmail.Visible = False
        Me.lblPhone.Visible = False
        Me.lblCetak.Visible = False
        Me.lblCapCetak.Visible = False
        Me.lblInvoiceDate.Visible = False
        Me.lblRefChassisNo.Visible = False
    End Sub

    Private Sub ShowAllEndCustomerLabel()
        Me.lblName1.Visible = True
        Me.lblName2.Visible = True
        Me.lblName3.Visible = True
        Me.lblAddress.Visible = True
        Me.lblKel.Visible = True
        Me.lblKec.Visible = True
        Me.lblPOSCode.Visible = True
        Me.lblProvince.Visible = True
        Me.lblKTP.Visible = True
        Me.lblCity.Visible = True
        Me.lblEmail.Visible = True
        Me.lblPhone.Visible = True
        Me.lblCetak.Visible = True
        Me.lblCapCetak.Visible = True
        Me.lblInvoiceDate.Visible = True
        Me.lblRefChassisNo.Visible = True
    End Sub

    Private Sub HideAllEndCustomerInputControl()
        Me.icInvoiceDate.Visible = False
        Me.txtRefChassisNumber.Visible = False
        'Me.txtCustomerName1.Visible = False
        'Me.txtCustomerName2.Visible = False
        'Me.txtCustomerName3.Visible = False
        'Me.txtCustomerAddress.Visible = False
        'Me.txtCustomerKel.Visible = False
        'Me.txtCustomerKec.Visible = False
        'Me.txtCustomerPOSCode.Visible = False
        'Me.ddlCustomerProvince.Visible = False
        'Me.ddlPreArea.Visible = False
        'Me.ddlCity.Visible = False
        'Me.txtEmail.Visible = False
        'Me.txtPhoneNumber.Visible = False
    End Sub

    Private Sub ShowAllEndCustomerInputControl()
        Me.txtRefChassisNumber.Visible = True
        'Me.txtCustomerName1.Visible = True
        'Me.txtCustomerName2.Visible = True
        'Me.txtCustomerName3.Visible = True
        'Me.txtCustomerAddress.Visible = True
        'Me.txtCustomerKel.Visible = True
        'Me.txtCustomerKec.Visible = True
        'Me.txtCustomerPOSCode.Visible = True
        'Me.ddlCustomerProvince.Visible = True
        'Me.ddlPreArea.Visible = True
        'Me.ddlCity.Visible = True
        'Me.txtEmail.Visible = True
        'Me.txtPhoneNumber.Visible = True

    End Sub

    Private Sub EndCustomerToControl()
        If Not _objEndCustomer.Customer Is Nothing Then
            If _objChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Baru Then
                pnlCustomerCode.Visible = True
                HideAllEndCustomerInputControl()
                ShowAllEndCustomerLabel()
                BindCustomer(_objEndCustomer.Customer)
                Me.icInvoiceDate.Value = _objEndCustomer.FakturDate
                Me.icInvoiceDate.Visible = True
                Me.lblValidatedBy.Text = ""
                Me.lblCustCode.Text = _objEndCustomer.Customer.Code
                Me.lblCustCode.Visible = True
                'Me.txtCustomerAddress.Text = _objEndCustomer.Customer.Alamat
                'Me.txtCustomerKec.Text = _objEndCustomer.Customer.Kecamatan
                'Me.txtCustomerKel.Text = _objEndCustomer.Customer.Kelurahan
                'Me.txtCustomerName1.Text = _objEndCustomer.Customer.Name1
                'Me.txtCustomerName2.Text = _objEndCustomer.Customer.Name2
                'Me.txtCustomerName3.Text = _objEndCustomer.Customer.Name3
                'Me.txtCustomerPOSCode.Text = _objEndCustomer.Customer.PostalCode
                'Me.txtEmail.Text = _objEndCustomer.Customer.Email
                'Me.txtPhoneNumber.Text = _objEndCustomer.Customer.PhoneNo
                Dim objRefChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
                Dim ObjRefChassisMaster As ChassisMaster
                ObjRefChassisMaster = objRefChassisMasterFacade.Retrieve(_objEndCustomer.RefChassisNumberID)
                If Not ObjRefChassisMaster Is Nothing Then
                    Me.txtRefChassisNumber.Text = ObjRefChassisMaster.ChassisNumber
                End If
                Me.icInvoiceDate.Value = _objEndCustomer.FakturDate

                If _objEndCustomer.Customer.PrintRegion = "X" Then
                    Me.chkPrintProvinceOnInvoice.Checked = True
                Else
                    Me.chkPrintProvinceOnInvoice.Checked = False
                End If

                CityToControl()

                Me.lblCreatedBy.Text = "<b>" & UserInfo.Convert(_objEndCustomer.SaveBy) & "</b> pada tanggal <b>" & _objEndCustomer.SaveTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
                Me.lblInvoiceDate.Visible = False
                Me.lblRefChassisNo.Visible = False


            Else
                HideAllEndCustomerInputControl()
                ShowAllEndCustomerLabel()
                BindCustomer(_objEndCustomer.Customer)
                pnlCustomerCode.Visible = True
                Me.lblCustCode.Visible = True
                Me.icInvoiceDate.Visible = False
                Me.lblCustCode.Text = _objEndCustomer.Customer.Code
                Me.lblInvoiceDate.Text = _objEndCustomer.FakturDate.ToString("dd/MM/yyyy")
                Dim objRefChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
                Dim ObjRefChassisMaster As ChassisMaster
                ObjRefChassisMaster = objRefChassisMasterFacade.Retrieve(_objEndCustomer.RefChassisNumberID)
                If Not ObjRefChassisMaster Is Nothing Then
                    Me.lblRefChassisNo.Text = ObjRefChassisMaster.ChassisNumber
                End If
                Me.lblCreatedBy.Text = "<b>" & UserInfo.Convert(_objEndCustomer.SaveBy) & "</b> pada tanggal <b>" & _objEndCustomer.SaveTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
                Me.lblValidatedBy.Text = IIf(_objEndCustomer.ValidateBy.Trim = "", "", "<b>" & UserInfo.Convert(_objEndCustomer.ValidateBy) & "</b> pada tanggal <b>" & _objEndCustomer.ValidateTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>")
            End If
        Else
            Dim objRefChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
            Dim ObjRefChassisMaster As ChassisMaster
            ObjRefChassisMaster = objRefChassisMasterFacade.Retrieve(_objEndCustomer.RefChassisNumberID)
            If Not ObjRefChassisMaster Is Nothing Then
                Me.txtRefChassisNumber.Text = ObjRefChassisMaster.ChassisNumber
            End If
            chkPrintProvinceOnInvoice.Visible = False
            ShowAllEndCustomerLabel()
            HideAllEndCustomerInputControl()
            Me.icInvoiceDate.Value = _objEndCustomer.FakturDate
            Me.icInvoiceDate.Visible = True
            Me.txtRefChassisNumber.Visible = True
            Me.lblValidatedBy.Text = ""
            Me.lblTitleValidateBy.Text = ""
        End If
        If _objEndCustomer.IsTemporary <> -1 Then
            lblTmpFaktur.Text = EnumEndCustomer.TemporaryFakturDesc(_objEndCustomer.IsTemporary.ToString())
        End If
        ' Additional Information
        BindEndCustomerAdditionalInformationToControl()

        ' Customer Profile
        'BindEndCustomerProfileToControl()
    End Sub

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            'Todo session
            Return Session(sObject)
        End If
    End Function

    Private Sub SetChassisMasterProfile(ByVal GroupCode As String)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile
        alGroup = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(GroupCode), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)
    End Sub

    Private Sub ControlToEndCustomer()
        _objChassisMaster = CType(GetFromSession("ChassisMaster"), ChassisMaster)
        _objEndCustomer = _objChassisMaster.EndCustomer

        If Not _objEndCustomer.Customer Is Nothing Then
            '_objEndCustomer.Customer.Name1 = Me.txtCustomerName1.Text.ToUpper
            '_objEndCustomer.Customer.Name2 = Me.txtCustomerName2.Text.ToUpper
            '_objEndCustomer.Customer.Name3 = Me.txtCustomerName3.Text.ToUpper
            '_objEndCustomer.Customer.Alamat = Me.txtCustomerAddress.Text.ToUpper
            '_objEndCustomer.Customer.Kelurahan = Me.txtCustomerKel.Text.ToUpper
            '_objEndCustomer.Customer.Kecamatan = Me.txtCustomerKec.Text.ToUpper
            '_objEndCustomer.Customer.Email = Me.txtEmail.Text.ToUpper
            '_objEndCustomer.Customer.PhoneNo = Me.txtPhoneNumber.Text

            If Me.chkPrintProvinceOnInvoice.Checked Then
                _objEndCustomer.Customer.PrintRegion = "X"
            Else
                _objEndCustomer.Customer.PrintRegion = ""
            End If
            '_objEndCustomer.Customer.PreArea = Me.ddlPreArea.SelectedValue
            '_objEndCustomer.Customer.City = New CityFacade(User).Retrieve(CType(Me.ddlCity.SelectedValue, Integer))
        Else
            If (Session("Customer") Is Nothing) Then
                _sErrorMessage = "Data customer belum ada"
                Exit Sub
            Else
                _objEndCustomer.Customer = CType(Session("Customer"), Customer)
            End If
        End If


        If Me.txtRefChassisNumber.Text.Trim <> "" Then
            Dim objChassisMaster As ChassisMaster
            objChassisMaster = New ChassisMasterFacade(User).Retrieve(Me.txtRefChassisNumber.Text.Trim)
            If objChassisMaster Is Nothing Or objChassisMaster.ID = 0 Then
                _sErrorMessage = _sErrorMessage & "Nomor Rangka Pengganti Tidak Terdaftar"
            Else
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, objChassisMaster.SONumber))
                Dim list As ArrayList = New POHeaderFacade(User).Retrieve(criterias)
                If (Not IsNothing(list) AndAlso list.Count > 0) Then
                    Dim objPOHeader As POHeader = CType(list.Item(0), POHeader)
                    If objPOHeader.ContractHeader.ProjectName <> String.Empty AndAlso lblNamaPesananKhusus.Text.Trim <> String.Empty Then
                        _sErrorMessage = _sErrorMessage & "Nomor Rangka Pengganti adalah Pesanan Khusus"
                    End If
                End If
                'If Not objChassisMaster.EndCustomer Is Nothing Then
                '    _sErrorMessage = _sErrorMessage & "Nomor Rangka Pengganti sudah digunakan"
                '    _objEndCustomer.RefChassisNumberID = Nothing
                'Else
                If _objEndCustomer.ID > 0 Then
                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(EndCustomer), "RefChassisNumberID", MatchType.Exact, objChassisMaster.ID))
                    crit.opAnd(New Criteria(GetType(EndCustomer), "ID", MatchType.No, _objEndCustomer.ID))

                    Dim aggr As Aggregate = New Aggregate(GetType(EndCustomer), "ID", AggregateType.Count)

                    Dim counter = 0
                    counter = New EndCustomerFacade(User).RetrieveScalar(crit, aggr)

                    If counter = 0 Then
                        _objEndCustomer.RefChassisNumberID = objChassisMaster.ID
                    Else
                        _sErrorMessage = _sErrorMessage & "Nomor Rangka Pengganti sudah digunakan"
                        _objEndCustomer.RefChassisNumberID = Nothing
                    End If
                Else
                    _objEndCustomer.RefChassisNumberID = objChassisMaster.ID
                End If
                'End If
            End If
        Else
            _objEndCustomer.RefChassisNumberID = Nothing
        End If
        _objEndCustomer.FakturDate = Me.icInvoiceDate.Value


        ' Additional Information
        If Me.chkPelanggaranWilayah.Checked Then
            _objEndCustomer.AreaViolationFlag = "X"
            _objEndCustomer.AreaViolationBankName = Me.txtPWBank.Text.ToUpper
            _objEndCustomer.AreaViolationGyroNumber = Me.txtPWNomorGiro.Text
            If Me.ddlPWPaymentMethod.SelectedIndex = 0 Then
                _sErrorMessage = _sErrorMessage & "Belum memilih Cara Pembayaran"
                _objEndCustomer.AreaViolationPaymentMethodID = CType(Me.ddlPWPaymentMethod.SelectedIndex, Integer)
            Else
                _objEndCustomer.AreaViolationPaymentMethodID = CType(Me.ddlPWPaymentMethod.SelectedValue, Integer)
            End If
            If Me.txtPWAmount.Text.Trim = String.Empty Then
                _sErrorMessage = "Biaya pelanggaran wilayah tidak boleh kosong"
                Exit Sub
            End If

            _objEndCustomer.AreaViolationyAmount = CType(IIf(Me.txtPWAmount.Text.Trim = String.Empty, "0", Me.txtPWAmount.Text.Trim), Decimal)
        Else
            _objEndCustomer.AreaViolationFlag = Nothing
            _objEndCustomer.AreaViolationBankName = Nothing
            _objEndCustomer.AreaViolationGyroNumber = Nothing
            _objEndCustomer.AreaViolationPaymentMethodID = Nothing
            _objEndCustomer.AreaViolationyAmount = Nothing
        End If

        If Me.chkPembayaranPenalti.Checked Then
            _objEndCustomer.PenaltyFlag = "X"
            _objEndCustomer.PenaltyBankName = Me.txtPPBank.Text.ToUpper
            _objEndCustomer.PenaltyGyroNumber = Me.txtPPNomorGiro.Text
            If Me.ddlPPPaymentMethod.SelectedIndex = 0 Then
                _sErrorMessage = _sErrorMessage & "Belum memilih Cara Pembayaran"
                _objEndCustomer.PenaltyPaymentMethodID = CType(Me.ddlPPPaymentMethod.SelectedIndex, Integer)
            Else
                _objEndCustomer.PenaltyPaymentMethodID = CType(Me.ddlPPPaymentMethod.SelectedValue, Integer)
            End If
            If Me.txtPPAmount.Text.Trim = String.Empty Then
                _sErrorMessage = "Biaya pembayaran penalti tidak boleh kosong"
                Exit Sub
            End If
            _objEndCustomer.PenaltyAmount = CType(IIf(Me.txtPPAmount.Text.Trim = String.Empty, "0", Me.txtPPAmount.Text.Trim), Decimal)
        Else
            _objEndCustomer.PenaltyFlag = Nothing
            _objEndCustomer.PenaltyBankName = Nothing
            _objEndCustomer.PenaltyGyroNumber = Nothing
            _objEndCustomer.PenaltyPaymentMethodID = Nothing
            _objEndCustomer.PenaltyAmount = Nothing
        End If

        If Me.chkSuratReferensi.Checked Then
            _objEndCustomer.ReferenceLetterFlag = "X"
            _objEndCustomer.ReferenceLetter = Me.txtSRNomorSurat.Text
        Else
            _objEndCustomer.ReferenceLetterFlag = Nothing
            _objEndCustomer.ReferenceLetter = Nothing
        End If

        If Me.chkMCP.Checked Then
            Dim ValExist As Boolean = False
            Try
                _objEndCustomer.MCPHeader = New MCPHeaderFacade(User).Retrieve(txtNoMCP.Text.Trim)

                ' ''Check Vehicle Type
                For Each ObjMcpDetail As MCPDetail In _objEndCustomer.MCPHeader.MCPDetails
                    If _objChassisMaster.VechileColor.VechileType.ID = ObjMcpDetail.VechileType.ID Then
                        ValExist = True
                        _objEndCustomer.LKPPHeader = Nothing
                        _objEndCustomer.LKPPStatus = Nothing
                        Exit For
                    End If
                Next
            Catch ex As Exception

            End Try


            If Not ValExist Then
                _sErrorMessage = "Tipe Kendaraan tidak terdaftar di MCP"
            End If

            If txtNoMCP.Text.Trim() = "" Then
                _sErrorMessage = "Nomor MCP Belum diisi"
            End If
        Else
            _objEndCustomer.MCPHeader = Nothing
        End If

        'LKPP
        If Me.chkLKPP.Checked Then
            Dim ValExist As Boolean = False

            ' ''Check Vehicle Type
            Try
                _objEndCustomer.LKPPHeader = New LKPPHeaderFacade(User).Retrieve(txtNoLKPP.Text.Trim)
                For Each ObjLkppDetail As LKPPDetail In _objEndCustomer.LKPPHeader.LKPPDetails
                    If _objChassisMaster.VechileColor.VechileType.ID = ObjLkppDetail.VechileType.ID Then
                        ValExist = True
                        _objEndCustomer.MCPHeader = Nothing
                        _objEndCustomer.MCPStatus = Nothing
                        Exit For
                    End If
                Next
            Catch ex As Exception

            End Try



            If Not ValExist Then
                _sErrorMessage = "Tipe Kendaraan tidak terdaftar di Pengadaan LKPP"
            End If

            If txtNoLKPP.Text.Trim() = "" Then
                _sErrorMessage = "Nomor LKPP Belum diisi"
            End If
        Else
            _objEndCustomer.LKPPHeader = Nothing
        End If
        'LKPP

        ' Customer Profile
        'Dim objCustomerProfileFacade As CustomerProfileFacade = New CustomerProfileFacade(User)
        Select Case _objChassisMaster.Category.CategoryCode
            Case "PC"
                SetChassisMasterProfile(PCGroup)
            Case "CV"
                SetChassisMasterProfile(CVGroup)
            Case "LCV"
                SetChassisMasterProfile(LCVGroup)
        End Select

        If Not _bIsValidating Then
            _objEndCustomer.SaveBy = User.Identity.Name
            _objEndCustomer.SaveTime = Date.Now
        End If
        _objChassisMaster.EndCustomer = _objEndCustomer
    End Sub

    Private Sub ClearCustomerInfo()
        Me.lblInvoiceDate.Text = String.Empty
        Me.lblRefChassisNo.Text = String.Empty
        Me.lblName1.Text = String.Empty
        Me.lblName2.Text = String.Empty
        Me.lblName3.Text = String.Empty
        Me.lblAddress.Text = String.Empty
        Me.lblKel.Text = String.Empty
        Me.lblKec.Text = String.Empty
        Me.lblPOSCode.Text = String.Empty
        Me.lblProvince.Text = String.Empty
        Me.lblKTP.Text = String.Empty
        Me.lblCity.Text = String.Empty
        Me.lblEmail.Text = String.Empty
        Me.lblPhone.Text = String.Empty
    End Sub

    Private Sub BindCustomer(ByVal objCust As Customer)
        Me.lblCustCode.Text = objCust.Code
        Me.lblName1.Text = objCust.Name1
        Me.lblName2.Text = objCust.Name2
        Me.lblName3.Text = objCust.Name3
        Me.lblAddress.Text = objCust.Alamat
        Me.lblKel.Text = objCust.Kelurahan
        Me.lblKec.Text = objCust.Kecamatan
        Me.lblPOSCode.Text = objCust.PostalCode
        If objCust.PrintRegion = "X" Then
            lblCetak.Text = "Ya"
        Else
            lblCetak.Text = "Tidak"
        End If
        Me.lblProvince.Text = objCust.City.Province.ProvinceName
        If Not IsNothing(objCust.MyCustomerRequest) Then
            Dim oCRP As CustomerRequestProfile = objCust.MyCustomerRequest.GetCustomerRequestProfile("NOKTP")
            If Not IsNothing(oCRP) Then
                Me.lblKTP.Text = oCRP.ProfileValue.ToString
            Else
                Me.lblKTP.Text = String.Empty
            End If
        Else
            Me.lblKTP.Text = String.Empty
        End If


        Me.lblCity.Text = objCust.PreArea + " " + objCust.City.CityName
        Me.lblEmail.Text = objCust.Email
        Me.lblPhone.Text = objCust.PhoneNo

    End Sub

    Private Function GetCustKTP(ByVal code As String) As String

    End Function

    Private Sub GetCustomerInfo(ByVal code As String)
        Dim objCustomerFacade As Service.CustomerFacade = New Service.CustomerFacade(User)
        Dim objCust As Customer = objCustomerFacade.Retrieve(code)
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        If objCust.ID > 0 Then
            If IsCustomerAvailaibleForLoginDealer(objCust, objDealer) Then
                'Todo session
                Session("Customer") = objCust
                BindCustomer(objCust)
            Else
                MessageBox.Show("Customer tidak terdaftar di dealer anda.")
            End If
        Else
            MessageBox.Show("Customer tidak ditemukan")
        End If
    End Sub

    Private Function IsCustomerAvailaibleForLoginDealer(ByVal objCust As Customer, ByVal loginDealer As Dealer) As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.ID", MatchType.Exact, objCust.ID))
        Dim objCustomerDealerFacade As Service.CustomerDealerFacade = New Service.CustomerDealerFacade(User)
        Dim objList As ArrayList = objCustomerDealerFacade.Retrieve(criterias)
        If objList.Count > 0 Then
            For Each item As CustomerDealer In objList
                If item.Dealer.ID = loginDealer.ID Then
                    Return True
                End If
            Next
        Else
            Return False
        End If
        Return False
    End Function

    Private Sub RecalculateMCP(ByVal ObjChasisMaster As ChassisMaster, ByVal EndcustomerID As Integer, Optional ByVal ObjBeforeMcpHeader As MCPHeader = Nothing, Optional ByVal ObjAfterMcpHeader As MCPHeader = Nothing)
        'Update MCP Before
        If Not ObjBeforeMcpHeader Is Nothing Then
            'Find MCP Detail
            Dim ObjMCPDetail As MCPDetail
            Dim objUnitRemain As Integer = 0
            Dim critMCP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critMCP.opAnd(New Criteria(GetType(MCPDetail), "VechileType.ID", MatchType.Exact, ObjChasisMaster.VechileColor.VechileType.ID))
            critMCP.opAnd(New Criteria(GetType(MCPDetail), "MCPHeader.ID", MatchType.Exact, ObjBeforeMcpHeader.ID))

            Dim ArrMCPDetail As ArrayList = New MCPDetailFacade(User).Retrieve(critMCP)
            If Not IsNothing(ArrMCPDetail) AndAlso ArrMCPDetail.Count > 0 Then
                ObjMCPDetail = CType(ArrMCPDetail(0), MCPDetail)

                'FInd Total Ussage
                Dim critChasis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critChasis.opAnd(New Criteria(GetType(EndCustomer), "ID", MatchType.Exact, EndcustomerID))
                critChasis.opAnd(New Criteria(GetType(EndCustomer), "ChasisMaster.VechileColor.VechileType.ID", MatchType.Exact, ObjChasisMaster.VechileColor.VechileType.ID))
                critChasis.opAnd(New Criteria(GetType(EndCustomer), "MCPDHeader.ID", MatchType.Exact, ObjBeforeMcpHeader.ID))

                Dim ArrEndCustomer As ArrayList = New EndCustomerFacade(User).Retrieve(critChasis)

                If Not IsNothing(ArrEndCustomer) AndAlso ArrEndCustomer.Count > 0 Then
                    objUnitRemain = ArrEndCustomer.Count
                End If


                ObjMCPDetail.UnitRemain = IIf(ObjMCPDetail.Unit - objUnitRemain < 0, 0, ObjMCPDetail.Unit - objUnitRemain)

                Dim nResult = New MCPDetailFacade(User).Update(ObjMCPDetail)

            End If



        End If

        'Update MCP After
        If Not ObjAfterMcpHeader Is Nothing Then
            'Find MCP Detail
            Dim ObjMCPDetail As MCPDetail
            Dim objUnitRemain As Integer = 0
            Dim critMCP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critMCP.opAnd(New Criteria(GetType(MCPDetail), "VechileType.ID", MatchType.Exact, ObjChasisMaster.VechileColor.VechileType.ID))
            critMCP.opAnd(New Criteria(GetType(MCPDetail), "MCPHeader.ID", MatchType.Exact, ObjAfterMcpHeader.ID))

            Dim ArrMCPDetail As ArrayList = New MCPDetailFacade(User).Retrieve(critMCP)
            If Not IsNothing(ArrMCPDetail) AndAlso ArrMCPDetail.Count > 0 Then
                ObjMCPDetail = CType(ArrMCPDetail(0), MCPDetail)

                'FInd Total Ussage
                Dim critChasis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critChasis.opAnd(New Criteria(GetType(EndCustomer), "ID", MatchType.Exact, EndcustomerID))
                critChasis.opAnd(New Criteria(GetType(EndCustomer), "ChasisMaster.VechileColor.VechileType.ID", MatchType.Exact, ObjChasisMaster.VechileColor.VechileType.ID))
                critChasis.opAnd(New Criteria(GetType(EndCustomer), "MCPDHeader.ID", MatchType.Exact, ObjAfterMcpHeader.ID))

                Dim ArrEndCustomer As ArrayList = New EndCustomerFacade(User).Retrieve(critChasis)

                If Not IsNothing(ArrEndCustomer) AndAlso ArrEndCustomer.Count > 0 Then
                    objUnitRemain = ArrEndCustomer.Count
                End If


                ObjMCPDetail.UnitRemain = IIf(ObjMCPDetail.Unit - objUnitRemain < 0, 0, ObjMCPDetail.Unit - objUnitRemain)

                Dim nResult = New MCPDetailFacade(User).Update(ObjMCPDetail)

            End If
        End If




    End Sub
    Private Function isGovermentType(ByVal parEndCustomer As EndCustomer) As Boolean
        Try
            If Not IsNothing(parEndCustomer.Customer) AndAlso Not IsNothing(parEndCustomer.Customer.MyCustomerRequest) Then
                Return parEndCustomer.Customer.MyCustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah
            Else
                Dim objGG = parEndCustomer.SPKFaktur.SPKHeader.SPKCustomer.TipeCustomer = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah
            End If
        Catch ex As Exception

        End Try


        Return False
    End Function

    Private Function isGovermentName(ByVal parEndCustomer As EndCustomer) As Boolean

        Try
            If Not IsNothing(parEndCustomer.Customer) AndAlso Not IsNothing(parEndCustomer.Customer.MyCustomerRequest) Then
                If Me.IsGovernmentInstitution(parEndCustomer.Customer.MyCustomerRequest.Name1) OrElse Me.IsGovernmentInstitution(parEndCustomer.Customer.MyCustomerRequest.Name2) Then
                    Return True
                End If
            Else
                Dim ObjName1 As String = parEndCustomer.SPKFaktur.SPKHeader.SPKCustomer.Name1
                Dim ObjName2 As String = parEndCustomer.SPKFaktur.SPKHeader.SPKCustomer.Name2

                If Me.IsGovernmentInstitution(ObjName1) OrElse Me.IsGovernmentInstitution(ObjName2) Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try


        Return False
    End Function

    Private Function IsGovernmentInstitution(ByVal sName As String) As Boolean
        Dim sMCPList() As String = KTB.DNet.Lib.WebConfig.GetValue("ListOfMCPName").Split(";")
        Dim i As Integer
        Dim sTemp As String = ""
        If sName.Trim() = String.Empty Then
            Return False
        End If
        For i = 0 To sMCPList.Length - 1
            sTemp = sMCPList(i).Trim
            If sTemp.IndexOf("*") >= 0 Then
                If sTemp.Split("*").Length > 2 Then

                    Dim dtValue As DataTable = New DataTable
                    Dim Wc() As String = sTemp.Split("*")

                    Dim Filter As String = String.Empty

                    dtValue.Columns.Add(New DataColumn("Value"))
                    Dim dr As DataRow = dtValue.NewRow()
                    dr(0) = sName
                    dtValue.Rows.Add(dr)

                    For F As Integer = 0 To Wc.Length - 1
                        If Wc(F) <> "" Then
                            If F = 0 Then
                                Filter = Filter & "Value LIKE '%" & Wc(F) + "%' "
                            Else
                                If Filter <> "" Then
                                    Filter = Filter & " AND Value LIKE '%" & Wc(F) & "%' "
                                Else
                                    Filter = Filter & "Value LIKE '%" & Wc(F) + "%' "
                                End If


                            End If

                        End If
                    Next
                    Dim drs As DataRow() = dtValue.Select(Filter)
                    If Not IsNothing(drs) AndAlso drs.Length > 0 Then Return True

                ElseIf sTemp.StartsWith("*") Then
                    If sName.EndsWith(sTemp.Replace("*", "")) Then Return True
                ElseIf sTemp.EndsWith("*") Then
                    If sName.StartsWith(sTemp.Replace("*", "")) Then Return True
                Else
                    If sName.StartsWith(sTemp.Substring(0, sTemp.IndexOf("*"))) _
                        AndAlso sName.EndsWith(sTemp.Substring(sTemp.IndexOf("*") + 1)) Then
                        Return True
                    End If
                End If
            Else
                If sName = sTemp Then Return True
            End If
        Next

        Return False
    End Function

#End Region

#Region " Event Handler "

    Private Sub initForm()
        _objChassisMaster = New ChassisMasterFacade(User).Retrieve(CType(Request.QueryString("ChassisMasterID"), Integer)) 'CType(GetFromSession("ChassisMaster"), ChassisMaster)
        BindEndCustomerProfileToControl()
    End Sub

    Private Sub InitiateAuthorization()

        If Not SecurityProvider.Authorize(Context.User, SR.FakturKendaraanPangajuanBuat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FAKTUR - Permohonan Pembukaan Faktur Kendaraan")
        End If
        Me.btnCancelValidate.Visible = SecurityProvider.Authorize(Context.User, SR.FakturKendaraanCancelValidate_Privilege)
        Me.btnValidate.Visible = SecurityProvider.Authorize(Context.User, SR.FakturKendaraanValidate_Privilege)
        Me.btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.FakturKendaraanSavePengajuan_Privilege)
        'If Not IsNothing(ViewState("disabledSave")) Then
        If Not IsNothing(_sesshelper.GetSession("disabledSave")) Then
            If btnSave.Visible Then
                '                If CType(ViewState("disabledSave"), Boolean) Then
                If CType(_sesshelper.GetSession("disabledSave"), Boolean) Then
                    btnSave.Enabled = False
                End If
            End If

            If btnValidate.Visible Then
                'If CType(ViewState("disabledSave"), Boolean) Then
                If CType(_sesshelper.GetSession("disabledSave"), Boolean) Then
                    btnValidate.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub LoadPage()
        _sesshelper.SetSession("disabledSave", False)
        _objDealer = CType(GetFromSession("DEALER"), Dealer)

        If Not _objChassisMaster Is Nothing Then
            _objEndCustomer = _objChassisMaster.EndCustomer
        Else
            Me.btnKembali.Attributes.Add("onclick", "DisclaimerAgreed = true;")
            Me.btnBindModel.Attributes.Add("onclick", "DisclaimerAgreed = true;")
            Me.btnCancel.Enabled = False
            Me.btnValidate.Enabled = False
            Me.btnSave.Enabled = False
            Me.btnCancelValidate.Enabled = False
            MessageBox.Show(SR.DataNotFound("Sasis"))
            Exit Sub
        End If
        ProvinceToControl()
        AdditionalInformationToControl()
        DealerToControl()
        ChassisMasterToControl()
        EndCustomerToControl()
        If IsOpenInvoice Then
            EnableAll()
        Else
            DisableAll()
        End If
        ToggleButton()
        chkPrintProvinceOnInvoice.Enabled = False
        If Not _objEndCustomer Is Nothing Then
            Dim objCustomer As Customer = _objEndCustomer.Customer
            If Not objCustomer Is Nothing Then
                If ((objCustomer.DeletionMark = "0" Or objCustomer.DeletionMark = 0) And (_objChassisMaster.FakturStatus = "0" Or _objChassisMaster.FakturStatus = String.Empty)) Then
                    chkPrintProvinceOnInvoice.Enabled = True
                End If
            End If
        End If




    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            'Me.txtMCPConfirmation.Text = "-1"
            LoadPage()
            GetRenderControl()
            ManageDDLControl()
            'Else
            '    If Me.txtMCPConfirmation.Text = "1" Then
            '        Me.btnValidate_Click(Nothing, Nothing)
            '    End If


            Dim ddlLeasing As DropDownList = pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))
            If Not IsNothing(ddlLeasing) Then

                btnDisableLeasing_Click(Me, Nothing)
            End If
        End If
        Me.btnSave.Attributes.Add("onclick", "ToggleDisclaimerAgreement();")
        Me.btnKembali.Attributes.Add("onclick", "DisclaimerAgreed = true;")
        Me.btnBindModel.Attributes.Add("onclick", "DisclaimerAgreed = true;")
        Me.btnValidate.Attributes.Add("onclick", "ToggleDisclaimerAgreement();")
        Me.btnCancelValidate.Attributes.Add("onclick", "DisclaimerAgreed = true;")
        Me.chkPelanggaranWilayah.Attributes.Add("onclick", "ToggleAdditionalInformation('chkPelanggaranWilayah');")
        Me.chkPembayaranPenalti.Attributes.Add("onclick", "ToggleAdditionalInformation('chkPembayaranPenalti');")
        Me.chkSuratReferensi.Attributes.Add("onclick", "ToggleAdditionalInformation('chkSuratReferensi');")
        Me.imgAddInfo.Attributes.Add("onclick", "MinimizeAddtionalInformation();")
        Me.chkDisclaimerAgreement.Attributes.Add("onclick", "ToggleDisclaimerAgreement();")
        Me.btnCancel.Attributes.Add("onclick", "DisclaimerAgreed = true; return confirm('" & SR.CancelConfirmation & "');")
        Me.chkMCP.Attributes.Add("onclick", "ToggleAdditionalInformation('chkMCP');")
        Me.lblMCPNumber.Attributes.Add("onClick", "ShowMCPSelection();")
        Me.txtNoMCP.Attributes.Add("readonly", "readonly")

        'LKPP
        Me.chkLKPP.Attributes.Add("onclick", "ToggleAdditionalInformation('chkLKPP');")
        Me.lblLKPPNumber.Attributes.Add("onClick", "ShowLKPPSelection();")
        Me.txtNoLKPP.Attributes.Add("readonly", "readonly")
        'LKPP

        'Fleet
        Me.chkFleet.Attributes.Add("onclick", "ToggleAdditionalInformation('chkFleet');")
        Me.lblNoRegRequest.Attributes.Add("onClick", "ShowFleetSelection();")
        Me.txtNoFleetReq.Attributes.Add("readonly", "readonly")
        'Fleet
    End Sub

    Private Sub GetRenderControl()
        Dim oPHFac As New ProfileHeaderFacade(User)
        Dim oPHJenis As ProfileHeader = oPHFac.Retrieve("CBU_JENISKEND")
        Dim oPHModel As ProfileHeader = oPHFac.Retrieve("CBU_MODELKEND")
        Dim oPHLeasing As ProfileHeader = oPHFac.Retrieve("CBU_LEASING")
        Dim oPHKaroseri As ProfileHeader = oPHFac.Retrieve("CBU_CARROSSERIE")

        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        oPG = oPGFac.Retrieve("cust_prf_" & _objChassisMaster.Category.CategoryCode.ToLower)
        Dim oPHTGFac As New ProfileHeaderToGroupFacade(User)
        Dim cCMP As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
        'cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileHeader.ID", MatchType.InSet, , oPH.ID))
        Dim oPHTGList As ArrayList = oPHTGFac.RetrieveByCriteria(cCMP)

        For Each phtg As ProfileHeaderToGroup In oPHTGList
            If phtg.ProfileHeader.ID = oPHJenis.ID Then
                _ctlJenisKendaraan = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlJenis", _ctlJenisKendaraan)
            ElseIf phtg.ProfileHeader.ID = oPHModel.ID Then
                _ctlModelKendaraan = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlModel", _ctlModelKendaraan)
            ElseIf phtg.ProfileHeader.ID = oPHLeasing.ID Then
                _ctlLeasing = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlLeasing", _ctlLeasing)
            ElseIf phtg.ProfileHeader.ID = oPHKaroseri.ID Then
                _ctlKaroseri = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                _sesshelper.SetSession("ddlKaroseri", _ctlKaroseri)
            End If
        Next

    End Sub

    Private Sub ManageDDLControl()
        'DDLIST + ProfileHeaderToGroup.ID + "_" + Profilegroup.ID

        Dim ddlJenis As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))

        Dim ddlLeasing As DropDownList
        Dim ddlKaroseri As DropDownList
        If _objChassisMaster.Category.CategoryCode = "PC" Or _objChassisMaster.Category.CategoryCode = "LCV" Then 'antisipasi data lama
            ddlLeasing = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))
            ddlKaroseri = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlKaroseri"), String))
        End If

        Dim oVKGFac As VehicleKindGroupFacade = New VehicleKindGroupFacade(User)

        Dim cVKG As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKindGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strGroup As String = ""
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind1 = 1 Then
            strGroup &= "2,"
        End If
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind2 = 1 Then
            strGroup &= "3,"
        End If
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind3 = 1 Then
            strGroup &= "4,"
        End If
        If _objChassisMaster.VechileColor.VechileType.IsVehicleKind4 = 1 Then
            strGroup &= "5,"
        End If

        Try
            If Not IsNothing(_objChassisMaster.VehicleKind) AndAlso _objChassisMaster.VehicleKind.ID = 1 Then
                strGroup &= "1,"
            End If
        Catch ex As Exception

        End Try


        strGroup = Left(strGroup, strGroup.Length - 1)
        cVKG.opAnd(New Criteria(GetType(VehicleKindGroup), "ID", MatchType.InSet, "(" & strGroup & ")"))

        Dim aVKG As ArrayList
        Dim oVKofCM As VehicleKind
        If Not IsNothing(_objChassisMaster.VehicleKind) AndAlso _objChassisMaster.VehicleKind.ID = 1 Then
            aVKG = oVKGFac.RetrieveList()
        Else
            aVKG = oVKGFac.Retrieve(cVKG)
        End If

        If Not IsNothing(ddlJenis) Then
            With ddlJenis.Items
                .Clear()
                For Each oVKG As VehicleKindGroup In aVKG
                    .Add(New ListItem(oVKG.Description, oVKG.ID))
                Next
            End With
        End If
        If Not IsNothing(_objChassisMaster) Then
            If Not IsNothing(_objChassisMaster.VehicleKind) Then
                If Not IsNothing(ddlJenis) Then
                    Dim isTypeValid As Boolean = False
                    For i As Integer = 0 To ddlJenis.Items.Count - 1
                        If ddlJenis.Items(i).Value = _objChassisMaster.VehicleKind.VehicleKindGroup.ID Then
                            isTypeValid = True
                            Exit For
                        End If
                    Next
                    If isTypeValid = True Then
                        ddlJenis.SelectedValue = _objChassisMaster.VehicleKind.VehicleKindGroup.ID
                        Me.txtNewKindID.Text = ddlJenis.SelectedValue
                    Else
                        MessageBox.Show("Jenis dan Model yang ada pilih sebelumnya tidak valid. Silahkan pilih Jenis dan Model yang sesuai")
                    End If
                End If
                BindDDLModel(True)
            End If
        End If
        If Not IsNothing(ddlJenis) And Not IsNothing(ddlModel) Then
            ddlJenis.Attributes.Add("OnChange", "RebindModel()")
            ddlModel.Attributes.Add("OnChange", "ChoosenModel()")
        End If
        'Me.btnKembali.Text = "Kembali."

        If Not IsNothing(ddlLeasing) Then
            Dim ddlPayment As DropDownList = GetDDLProfile("CBU_WAYPAID1")
            ddlPayment.Attributes.Add("OnChange", "DisableLeasing()")

            If ddlPayment.SelectedItem.Text.Trim = "TUNAI" Then
                ddlLeasing.SelectedIndex = 0
                ddlLeasing.Enabled = False
            ElseIf ddlPayment.SelectedItem.Text = "KREDIT" Then
                If ddlPayment.Enabled Then 'if ddlPayment disabled then ddlLeasing keep disabled also
                    ddlLeasing.Enabled = True
                End If
            End If
        End If

        '-- Start Invoice Revision, 02042018
        If Not IsNothing(ddlLeasing) Then
            Dim oPHFac As New ProfileHeaderFacade(User)
            Dim oPHLeasing As ProfileHeader = oPHFac.Retrieve("CBU_LEASING")
            Dim oLeasingFac As LeasingFacade = New LeasingFacade(User)
            Dim cLeasing As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cLeasing.opAnd(New Criteria(GetType(Leasing), "Status", MatchType.Exact, CType(1, Byte)))

            Dim aLeasing As ArrayList
            aLeasing = oLeasingFac.Retrieve(cLeasing)

            Dim strProfileValue As String = GetProfileValue(oPHLeasing)
            Dim intProfileID As Integer = 0

            With ddlLeasing.Items
                .Clear()
                For Each oLeasing As Leasing In aLeasing
                    .Add(New ListItem(oLeasing.LeasingName, oLeasing.LeasingCode))
                Next
            End With
            Dim listSilahkanPilih As ListItem = New ListItem("Silahkan Pilih", "")
            ddlLeasing.Items.Insert(0, listSilahkanPilih)

            Try
                ddlLeasing.SelectedValue = strProfileValue
            Catch ex As Exception

            End Try

        End If

        If Not IsNothing(ddlKaroseri) Then
            Dim oPHFac As New ProfileHeaderFacade(User)
            Dim oPHKaroseri As ProfileHeader = oPHFac.Retrieve("CBU_CARROSSERIE")
            Dim oKaroseriFac As KaroseriFacade = New KaroseriFacade(User)
            Dim cKaroseri As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Karoseri), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cKaroseri.opAnd(New Criteria(GetType(Karoseri), "Status", MatchType.Exact, CType(1, Byte)))

            Dim aKaroseri As ArrayList
            aKaroseri = oKaroseriFac.Retrieve(cKaroseri)

            Dim strProfileValue As String = GetProfileValue(oPHKaroseri)
            Dim intProfileID As Integer = 0

            With ddlKaroseri.Items
                .Clear()
                For Each oKaroseri As Karoseri In aKaroseri
                    .Add(New ListItem(oKaroseri.Name, oKaroseri.Code))
                    'If oKaroseri.ID.ToString() = strProfileValue Then intProfileID = oKaroseri.ID
                Next
            End With
            Dim listSilahkanPilih As ListItem = New ListItem("Silahkan Pilih", "")
            ddlKaroseri.Items.Insert(0, listSilahkanPilih)

            Try
                ddlKaroseri.SelectedValue = strProfileValue
            Catch ex As Exception

            End Try

        End If

    End Sub


    Private Function GetProfileValue(ByVal oPH As ProfileHeader) As String
        Dim objGroup As ProfileGroup
        Dim strProfileValue As String = ""

        Select Case _objChassisMaster.Category.CategoryCode
            Case "PC"
                objGroup = New ProfileGroupFacade(User).Retrieve(PCGroup)
            Case "CV"
                objGroup = New ProfileGroupFacade(User).Retrieve(CVGroup)
            Case "LCV"
                objGroup = New ProfileGroupFacade(User).Retrieve(LCVGroup)
        End Select

        Dim objFacade As ChassisMasterProfileFacade = New ChassisMasterProfileFacade(System.Threading.Thread.CurrentPrincipal)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
        criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
        criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
        Dim objListChassisMasterProfile As ArrayList = objFacade.Retrieve(criterias)
        If objListChassisMasterProfile.Count > 0 Then
            strProfileValue = CType(objListChassisMasterProfile(0), ChassisMasterProfile).ProfileValue
        End If

        Return strProfileValue
    End Function

    Private Sub BindDDLModel(Optional ByVal IsNotFromClient As Boolean = False)
        Dim ddlJenis As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim oVKFac As New VehicleKindFacade(User)
        Dim aVK As ArrayList

        If Not IsNothing(ddlModel) Then
            With ddlModel.Items
                .Clear()
                If CType(ddlJenis.SelectedValue, Integer) > 0 Then
                    'cVK.opAnd(New Criteria(GetType(VehicleKind), "VehicleKindGroup.ID", MatchType.Exact, CType(ddlJenis.SelectedValue, Integer)))
                    Dim strGroup As String = ""
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind1 = 1 Then
                        strGroup &= "2,"
                    End If
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind2 = 1 Then
                        strGroup &= "3,"
                    End If
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind3 = 1 Then
                        strGroup &= "4,"
                    End If
                    If _objChassisMaster.VechileColor.VechileType.IsVehicleKind4 = 1 Then
                        strGroup &= "5,"
                    End If

                    If Not IsNothing(_objChassisMaster.VehicleKind) AndAlso _objChassisMaster.VehicleKind.ID = 1 Then
                        strGroup &= "1,"
                    End If

                    strGroup = Left(strGroup, strGroup.Length - 1)

                    Dim cVK As New CriteriaComposite(New Criteria(GetType(VehicleKind), "VehicleKindGroup.ID", MatchType.InSet, "(" & strGroup & ")"))


                    aVK = oVKFac.Retrieve(cVK)

                    For Each oVK As VehicleKind In aVK
                        .Add(New ListItem(oVK.Description, oVK.ID))
                    Next

                    If IsNotFromClient AndAlso Not IsNothing(_objChassisMaster) AndAlso Not IsNothing(_objChassisMaster.VehicleKind) Then
                        Dim isTypeValid As Boolean = False
                        For i As Integer = 0 To ddlModel.Items.Count - 1
                            If ddlModel.Items(i).Value = _objChassisMaster.VehicleKind.ID Then
                                isTypeValid = True
                                Exit For
                            End If
                        Next
                        If isTypeValid = True Then
                            ddlModel.SelectedValue = _objChassisMaster.VehicleKind.ID.ToString
                        Else
                            MessageBox.Show("Jenis dan Model yang ada pilih sebelumnya tidak valid. Silahkan pilih Jenis dan Model yang sesuai")
                        End If

                    End If
                    Me.txtNewModelID.Text = ddlModel.SelectedValue
                End If
            End With

        End If

    End Sub

    Private Sub DisableAll()
        icInvoiceDate.Enabled = False
        txtRefChassisNumber.ReadOnly = True
        'txtCustomerName1.ReadOnly = True
        'txtCustomerName2.ReadOnly = True
        'txtCustomerName3.ReadOnly = True
        'txtCustomerAddress.ReadOnly = True
        'txtCustomerKel.ReadOnly = True
        'txtCustomerKec.ReadOnly = True
        'txtCustomerPOSCode.ReadOnly = True
        'ddlCustomerProvince.Enabled = False
        'chkPrintProvinceOnInvoice.Enabled = False
        'ddlPreArea.Enabled = False
        'ddlCity.Enabled = False
        'txtEmail.ReadOnly = True
        'txtPhoneNumber.ReadOnly = True

        chkPelanggaranWilayah.Enabled = False
        ddlPWPaymentMethod.Enabled = False
        txtPWAmount.ReadOnly = True
        txtPWBank.ReadOnly = True
        txtPWNomorGiro.ReadOnly = True

        chkPembayaranPenalti.Enabled = False
        ddlPPPaymentMethod.Enabled = False
        txtPPAmount.ReadOnly = True
        txtPPBank.ReadOnly = True
        txtPPNomorGiro.ReadOnly = True

        chkSuratReferensi.Enabled = False
        txtSRNomorSurat.ReadOnly = True

        chkMCP.Enabled = False
        txtNoMCP.ReadOnly = True

        'LKPP
        chkLKPP.Enabled = False
        txtNoLKPP.ReadOnly = True
        'LKPP

        'Fleet
        chkFleet.Enabled = False
        txtNoFleetReq.ReadOnly = True
        'Fleet

        Me.btnCancel.Visible = False
    End Sub

    Private Sub EnableAll()
        icInvoiceDate.Visible = True
        txtRefChassisNumber.Visible = True
        txtRefChassisNumber.ReadOnly = False
        'txtCustomerName1.ReadOnly = False
        'txtCustomerName2.ReadOnly = False
        'txtCustomerName3.ReadOnly = False
        'txtCustomerAddress.ReadOnly = False
        'txtCustomerKel.ReadOnly = False
        'txtCustomerKec.ReadOnly = False
        'txtCustomerPOSCode.ReadOnly = False
        'ddlCustomerProvince.Enabled = True
        chkPrintProvinceOnInvoice.Enabled = True
        chkPrintProvinceOnInvoice.Visible = True

        'ddlPreArea.Enabled = True
        'ddlCity.Enabled = True
        'txtEmail.ReadOnly = False
        'txtPhoneNumber.ReadOnly = False

        chkPelanggaranWilayah.Enabled = True
        ddlPWPaymentMethod.Enabled = True
        txtPWAmount.ReadOnly = False
        txtPWBank.ReadOnly = False
        txtPWNomorGiro.ReadOnly = False

        chkPembayaranPenalti.Enabled = True
        ddlPPPaymentMethod.Enabled = True
        txtPPAmount.ReadOnly = False
        txtPPBank.ReadOnly = False
        txtPPNomorGiro.ReadOnly = False

        chkSuratReferensi.Enabled = True
        txtSRNomorSurat.ReadOnly = False

        chkMCP.Enabled = True
        chkLKPP.Enabled = True
        chkFleet.Enabled = True

        Me.btnCancel.Visible = True

    End Sub

    Public Sub ToggleButton()
        If _objChassisMaster.FakturStatus.Trim = "" Then
            'If (CType(ViewState("disabledSave"), Boolean)) Then
            If (CType(_sesshelper.GetSession("disabledSave"), Boolean)) Then
                Me.btnSave.Enabled = False
            Else
                Me.btnSave.Enabled = True
            End If
            Me.btnValidate.Enabled = False
            Me.btnCancelValidate.Enabled = False
        ElseIf _objChassisMaster.FakturStatus = "0" Then
            'If (CType(ViewState("disabledSave"), Boolean)) Then
            If (CType(_sesshelper.GetSession("disabledSave"), Boolean)) Then
                Me.btnSave.Enabled = False
            Else
                Me.btnSave.Enabled = True
            End If
            If _objChassisMaster.EndCustomer Is Nothing Then
                Me.btnValidate.Enabled = False
            Else
                'Me.btnValidate.Enabled = True
                'If (CType(ViewState("disabledSave"), Boolean)) Then
                If (CType(_sesshelper.GetSession("disabledSave"), Boolean)) Then
                    Me.btnValidate.Enabled = False
                Else
                    Me.btnValidate.Enabled = True
                End If
            End If
            Me.btnCancelValidate.Enabled = False

        ElseIf _objChassisMaster.FakturStatus = "1" Then
            Me.btnSave.Enabled = False
            Me.btnValidate.Enabled = False
            Me.btnCancelValidate.Enabled = True
        Else
            Me.btnSave.Enabled = False
            Me.btnValidate.Enabled = False
            Me.btnCancelValidate.Enabled = False
        End If
    End Sub

    Private Sub SaveInvoice()


        If Format(Me.icInvoiceDate.Value, "yyyy/MM/dd") >= Format(Date.Now, "yyyy/MM/dd") Then
            ' Modified by Ikhsan, 25 September 2008
            ' Requested by Yurike and Rina
            ' To allow Invoice creation in 29/30 Sept 2008
            ' Start --------------------------------------------------------
            If Format(Me.icInvoiceDate.Value, "yyyy/MM/dd") <> "2008/09/29" And Format(Me.icInvoiceDate.Value, "yyyy/MM/dd") <> "2008/09/30" Then
                If IsValidTglFaktur() Then
                    ControlToEndCustomer()
                Else
                    _sErrorMessage = "Besok Hari Libur"
                End If
            End If
            ' End --------------------------------------------------------
            ControlToEndCustomer()
        Else
            _sErrorMessage = "Tanggal faktur seharusnya >= tanggal hari ini"
        End If
        Dim isMCP_Allowed As Boolean = True
        Dim _isGovermentType As Boolean = False
        Dim _isGovermentOwnerShip As Boolean = False
        Dim _isGovermentName As Boolean = False

        If (_sErrorMessage = "") Then
            'Dim objEndCustomerfacade As EndCustomerFacade = New EndCustomerFacade(User)
            ' Do Update
            'Check isGovermentType
            _isGovermentType = isGovermentType(_objChassisMaster.EndCustomer)
            _isGovermentName = isGovermentName(_objChassisMaster.EndCustomer)
            Dim nUpdatedRow As Integer = -1
            Select Case _objChassisMaster.Category.CategoryCode
                Case "PC"
                    'nUpdatedRow = New FinishUnit.ChassisMasterFacade(User).InsertUpdateChassisMasterProfile(_objChassisMaster, alGroup, New ProfileGroupFacade(User).Retrieve("cust_prf_pc"))
                    Dim objRenderPanel As RenderingProfile = New RenderingProfile
                    Dim PCList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(PCGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)

                    For Each item As ChassisMasterProfile In PCList
                        If (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 7) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            _isGovermentOwnerShip = True
                            'If _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.Status1 <> EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                            If txtNoLKPP.Text.Trim <> "" AndAlso chkLKPP.Checked Then
                                Dim isExistOnLKPP As Boolean = False
                                Dim lkppH As LKPPHeader = New LKPPHeaderFacade(User).Retrieve(txtNoLKPP.Text.Trim)
                                If Not IsNothing(lkppH) Then
                                    For Each lkppD As LKPPDetail In lkppH.LKPPDetails
                                        If _objChassisMaster.VechileColor.VechileType.ID = lkppD.VechileType.ID Then
                                            isExistOnLKPP = True
                                            _objChassisMaster.EndCustomer.LKPPHeader = lkppH
                                            _objChassisMaster.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                            If Not IsNothing(_objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) Then
                                                _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                            End If


                                            '_objEndCustomer.MCPHeader = mcpH
                                        End If
                                    Next
                                End If
                                If Not IsNothing(_objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) Then
                                    _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                End If

                                If isExistOnLKPP = False Then
                                    'MessageBox.Show("Nomor MCP tidak sesuai dengan tipe kendaraan yang dipilih.")
                                    _sErrorMessage = "Nomor LKPP tidak sesuai dengan tipe kendaraan yang dipilih."
                                End If
                            Else
                                isMCP_Allowed = False
                                'MessageBox.Show("Konsumen terdeteksi MCP, Nomor MCP harap diisi.")
                                _sErrorMessage = "Customer terdeteksi LKPP. Silahkan masukkan nomor LKPP"
                            End If
                            'Else
                            '_objChassisMaster.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP
                            'End If
                        ElseIf (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 7) AndAlso Not (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            If Not IsNothing(_objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) Then
                                _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NonLKPP
                            End If

                        End If
                    Next

                    '  Dim Tmp_ObjCustomer As EndCustomer = New EndCustomerFacade(User).Retrieve(_objChassisMaster.EndCustomer.ID)
                    'Checking Scenario LKPP
                    'C1 , C6
                    If _isGovermentType OrElse _isGovermentOwnerShip Then
                        If IsNothing(_objChassisMaster.EndCustomer.LKPPHeader) Then
                            isMCP_Allowed = False
                            _sErrorMessage = "Customer terdeteksi LKPP. Silahkan masukkan nomor LKPP"
                        Else
                            _objChassisMaster.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                        End If

                    ElseIf _isGovermentName Then
                        _objChassisMaster.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.IndicatedLKPP

                    ElseIf _isGovermentName = False AndAlso _isGovermentOwnerShip = False AndAlso _isGovermentType = False Then
                        If Not IsNothing(_objChassisMaster.EndCustomer.LKPPHeader) Then
                            isMCP_Allowed = False
                            _sErrorMessage = "LKPP Hanya diperuntukan untuk tipe pelanggan BUMN dan pemerintah"
                        Else
                            _objChassisMaster.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NonLKPP
                        End If
                    End If

                    If chkMCP.Checked AndAlso txtNoMCP.Text <> "" Then
                        isMCP_Allowed = False
                        _sErrorMessage = "MCP Hanya diperuntukan untuk tipe pelanggan BUMN & pemerintah serta Kendaraan CV"
                    End If


                    If isMCP_Allowed Then
                        nUpdatedRow = New FinishUnit.ChassisMasterFacade(User).InsertUpdateChassisMasterProfile(_objChassisMaster, alGroup, New ProfileGroupFacade(User).Retrieve(PCGroup))

                        '  RecalculateMCP(_objChassisMaster, Tmp_ObjCustomer.ID, Tmp_ObjCustomer.MCPHeader, _objChassisMaster.EndCustomer.MCPHeader)
                    End If

                Case "LCV"
                    'nUpdatedRow = New FinishUnit.ChassisMasterFacade(User).InsertUpdateChassisMasterProfile(_objChassisMaster, alGroup, New ProfileGroupFacade(User).Retrieve("cust_prf_lcv"))
                    Dim objRenderPanel As RenderingProfile = New RenderingProfile
                    Dim LCVList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(LCVGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)
                    For Each item As ChassisMasterProfile In LCVList
                        If (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 6) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            _isGovermentOwnerShip = True
                            'If _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.Status1 <> EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                            If txtNoLKPP.Text.Trim <> "" AndAlso chkLKPP.Checked Then
                                Dim isExistOnLKPP As Boolean = False
                                Dim lkppH As LKPPHeader = New LKPPHeaderFacade(User).Retrieve(txtNoLKPP.Text.Trim)
                                If Not IsNothing(lkppH) Then
                                    For Each lkppD As LKPPDetail In lkppH.LKPPDetails
                                        If _objChassisMaster.VechileColor.VechileType.ID = lkppD.VechileType.ID Then
                                            isExistOnLKPP = True
                                            _objChassisMaster.EndCustomer.LKPPHeader = lkppH
                                            If Not IsNothing(_objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) Then
                                                _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                            End If


                                            '_objEndCustomer.MCPHeader = mcpH
                                        End If
                                    Next
                                End If
                                If Not IsNothing(_objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) Then
                                    _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                End If

                                If isExistOnLKPP = False Then
                                    'MessageBox.Show("Nomor MCP tidak sesuai dengan tipe kendaraan yang dipilih.")
                                    _sErrorMessage = "Nomor LKPP tidak sesuai dengan tipe kendaraan yang dipilih."
                                End If
                            Else
                                isMCP_Allowed = False
                                'MessageBox.Show("Konsumen terdeteksi MCP, Nomor MCP harap diisi.")
                                _sErrorMessage = "Customer terdeteksi LKPP. Silahkan masukkan nomor LKPP"
                            End If
                            'Else
                            '_objChassisMaster.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP
                            'End If
                        ElseIf (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 6) AndAlso Not (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            If Not IsNothing(_objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) Then
                                _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NonLKPP
                            End If

                        End If
                    Next

                    '  Dim Tmp_ObjCustomer As EndCustomer = New EndCustomerFacade(User).Retrieve(_objChassisMaster.EndCustomer.ID)

                    'Checking Scenario LKPP
                    'C1 , C6
                    If _isGovermentType OrElse _isGovermentOwnerShip Then
                        If IsNothing(_objChassisMaster.EndCustomer.LKPPHeader) Then
                            isMCP_Allowed = False
                            _sErrorMessage = "Customer terdeteksi LKPP. Silahkan masukkan nomor LKPP"
                        Else
                            _objChassisMaster.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                        End If

                    ElseIf _isGovermentName Then
                        _objChassisMaster.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.IndicatedLKPP

                    ElseIf _isGovermentName = False AndAlso _isGovermentOwnerShip = False AndAlso _isGovermentType = False Then
                        If Not IsNothing(_objChassisMaster.EndCustomer.LKPPHeader) Then
                            isMCP_Allowed = False
                            _sErrorMessage = "LKPP Hanya diperuntukan untuk tipe pelanggan BUMN dan pemerintah"
                        Else
                            _objChassisMaster.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NonLKPP
                        End If
                    End If

                    If chkMCP.Checked AndAlso txtNoMCP.Text <> "" Then
                        isMCP_Allowed = False
                        _sErrorMessage = "MCP Hanya diperuntukan untuk tipe pelanggan BUMN & pemerintah serta Kendaraan CV"
                    End If

                    If isMCP_Allowed Then
                        nUpdatedRow = New FinishUnit.ChassisMasterFacade(User).InsertUpdateChassisMasterProfile(_objChassisMaster, alGroup, New ProfileGroupFacade(User).Retrieve(LCVGroup))

                        '  RecalculateMCP(_objChassisMaster, Tmp_ObjCustomer.ID, Tmp_ObjCustomer.MCPHeader, _objChassisMaster.EndCustomer.MCPHeader)
                    End If
                Case "CV"
                    'add by anh - validasi mcp - 20150908
                    Dim objRenderPanel As RenderingProfile = New RenderingProfile
                    Dim CVList As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(CVGroup), CType(EnumProfileType.ProfileType.CHASSISMASTER, Short), User)
                    For Each item As ChassisMasterProfile In CVList
                        If (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 5) AndAlso (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            'If _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.Status1 <> EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                            If txtNoMCP.Text.Trim <> "" AndAlso chkMCP.Checked Then
                                Dim isExistOnMCP As Boolean = False
                                Dim mcpH As MCPHeader = New MCPHeaderFacade(User).Retrieve(txtNoMCP.Text.Trim)
                                If Not IsNothing(mcpH) Then
                                    For Each mcpD As MCPDetail In mcpH.MCPDetails
                                        If _objChassisMaster.VechileColor.VechileType.ID = mcpD.VechileType.ID Then
                                            isExistOnMCP = True
                                            _objChassisMaster.EndCustomer.MCPHeader = mcpH
                                            If Not IsNothing(_objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) Then
                                                _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP
                                            End If


                                            '_objEndCustomer.MCPHeader = mcpH
                                        End If
                                    Next
                                End If
                                If Not IsNothing(_objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) Then
                                    _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP
                                End If

                                If isExistOnMCP = False Then
                                    'MessageBox.Show("Nomor MCP tidak sesuai dengan tipe kendaraan yang dipilih.")
                                    _sErrorMessage = "Nomor MCP tidak sesuai dengan tipe kendaraan yang dipilih."
                                End If
                            Else
                                isMCP_Allowed = False
                                'MessageBox.Show("Konsumen terdeteksi MCP, Nomor MCP harap diisi.")
                                _sErrorMessage = "Customer terdeteksi MCP. Silahkan masukkan nomor MCP"
                            End If
                            'Else
                            '_objChassisMaster.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP
                            'End If
                        ElseIf (item.ProfileHeader.ID = 5 And item.ProfileGroup.ID = 5) AndAlso Not (item.ProfileValue = "IP" Or item.ProfileValue = "BN") Then
                            If Not IsNothing(_objChassisMaster.EndCustomer.Customer) AndAlso Not IsNothing(_objChassisMaster.EndCustomer.Customer.MyCustomerRequest) Then
                                _objChassisMaster.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NonMCP
                            End If

                        End If
                    Next

                    '  Dim Tmp_ObjCustomer As EndCustomer = New EndCustomerFacade(User).Retrieve(_objChassisMaster.EndCustomer.ID)

                    If chkLKPP.Checked AndAlso txtNoLKPP.Text <> "" Then
                        isMCP_Allowed = False
                        _sErrorMessage = "LKPP Hanya diperuntukan untuk tipe pelanggan BUMN & pemerintah serta Kendaraan PC LCV"
                    End If

                    If isMCP_Allowed Then
                        nUpdatedRow = New FinishUnit.ChassisMasterFacade(User).InsertUpdateChassisMasterProfile(_objChassisMaster, alGroup, New ProfileGroupFacade(User).Retrieve(CVGroup))

                        '  RecalculateMCP(_objChassisMaster, Tmp_ObjCustomer.ID, Tmp_ObjCustomer.MCPHeader, _objChassisMaster.EndCustomer.MCPHeader)
                    End If
                    'end added 
                    'nUpdatedRow = New FinishUnit.ChassisMasterFacade(User).InsertUpdateChassisMasterProfile(_objChassisMaster, alGroup, New ProfileGroupFacade(User).Retrieve("cust_prf_cv"))


                    '--insert fleet request number, add by wdi 20161213
                    If txtNoFleetReq.Text.Trim <> "" Then
                        Dim critFleetRequest As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critFleetRequest.opAnd(New Criteria(GetType(FleetRequest), "NoRegRequest", MatchType.Exact, txtNoFleetReq.Text.Trim))
                        Dim arrFleetRequest As ArrayList = New FleetRequestFacade(User).Retrieve(critFleetRequest)

                        Dim oFleetRequest As FleetRequest
                        If arrFleetRequest.Count > 0 Then
                            oFleetRequest = arrFleetRequest(0)
                        End If

                        Dim intResult As Integer = 0
                        If Not IsNothing(oFleetRequest) Then
                            Dim critFleetFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critFleetFaktur.opAnd(New Criteria(GetType(FleetFaktur), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
                            Dim objArr As ArrayList = New ArrayList
                            objArr = New FleetFakturFacade(User).Retrieve(critFleetFaktur)
                            Dim objFleetFaktur As FleetFaktur
                            If Not IsNothing(objArr) AndAlso objArr.Count > 0 Then
                                objFleetFaktur = CType(objArr(0), FleetFaktur)
                            End If

                            If IsNothing(objFleetFaktur) Then               '-- insert
                                objFleetFaktur = New FleetFaktur
                                objFleetFaktur.ChassisMaster = CType(_objChassisMaster, ChassisMaster)
                                objFleetFaktur.FleetRequest = oFleetRequest
                                Dim oFleetFakturFacade As FleetFakturFacade = New FleetFakturFacade(User)
                                intResult = oFleetFakturFacade.Insert(objFleetFaktur)
                            Else                                            '--update
                                objFleetFaktur.FleetRequest = oFleetRequest
                                Dim oFleetFakturFacade As FleetFakturFacade = New FleetFakturFacade(User)
                                intResult = oFleetFakturFacade.Update(objFleetFaktur)
                            End If
                        Else
                            Dim critFleetFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critFleetFaktur.opAnd(New Criteria(GetType(FleetFaktur), "ChassisMaster.ID", MatchType.Exact, _objChassisMaster.ID))
                            Dim objFleetFaktur As FleetFaktur = New FleetFakturFacade(User).Retrieve(critFleetFaktur)(0)

                            If Not IsNothing(objFleetFaktur) Then           '--if exist then deleted
                                objFleetFaktur.FleetRequest = oFleetRequest
                                objFleetFaktur.RowStatus = -1
                                Dim oFleetFakturFacade As FleetFakturFacade = New FleetFakturFacade(User)
                                intResult = oFleetFakturFacade.Insert(objFleetFaktur)
                            End If
                        End If
                    End If

            End Select

            If isMCP_Allowed Then
                SaveVehicleKind()
            End If

            If nUpdatedRow = 0 Then
                _sSuccessMessage = SR.UpdateSucces()
            Else
                '_sErrorMessage = SR.UpdateFail
                'add by anh - validasi mcp - 20150908
                If _sErrorMessage = "" Then
                    _sErrorMessage = SR.UpdateFail
                End If
                'end added
            End If
        End If
    End Sub

    Private Function SaveVehicleKind() As Integer
        Dim ddlJenis As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim oCMFac As New ChassisMasterFacade(User)
        Dim sOK As Integer = -1



        _objChassisMaster.VehicleKind = New VehicleKind(CType(ddlModel.SelectedValue, Integer))
        If oCMFac.Update(_objChassisMaster) > 0 Then
            sOK = 0
            SaveChassisMasterProfile()
        End If
        Return sOK
    End Function
    Private Sub SaveChassisMasterProfile()
        Dim ddlJenis As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim ddlLeasing As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))
        Dim ddlKaroseri As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlKaroseri"), String))

        Dim oCM As ChassisMaster
        Dim oCMPFac As New ChassisMasterProfileFacade(User)
        Dim oCMP As ChassisMasterProfile
        Dim oPHFac As New ProfileHeaderFacade(User)
        Dim oPHJenis As ProfileHeader = oPHFac.Retrieve("CBU_JENISKEND")
        Dim oPHModel As ProfileHeader = oPHFac.Retrieve("CBU_MODELKEND")
        Dim oPHLeasing As ProfileHeader = oPHFac.Retrieve("CBU_LEASING")
        Dim oPHKaroseri As ProfileHeader = oPHFac.Retrieve("CBU_CARROSSERIE")
        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        Dim GroupCode As String = ""
        Dim aCM As ArrayList = CType(Session("sessCM"), ArrayList)
        Dim oVK As VehicleKind = New VehicleKindFacade(User).Retrieve(CType(ddlModel.SelectedValue, Integer))
        Dim oLeasing As Leasing = New LeasingFacade(User).GetLeasing(ddlLeasing.SelectedValue)
        Dim oKaroseri As Karoseri = New KaroseriFacade(User).GetKaroseri(ddlKaroseri.SelectedValue)

        oCM = _objChassisMaster

        GroupCode = "cust_prf_" & oCM.Category.CategoryCode.ToLower
        oPG = oPGFac.Retrieve(GroupCode)

        oCMP = GetCMProfile(oCM, oPG, oPHJenis)
        oCMP.ChassisMaster = oCM
        oCMP.ProfileGroup = oPG
        oCMP.ProfileHeader = oPHJenis
        oCMP.ProfileValue = oVK.VehicleKindGroup.Code
        If oCMP.ID < 1 Then
            oCMPFac.Insert(oCMP)
        Else
            oCMPFac.Update(oCMP)
        End If

        oCMP = GetCMProfile(oCM, oPG, oPHModel)
        oCMP.ChassisMaster = oCM
        oCMP.ProfileGroup = oPG
        oCMP.ProfileHeader = oPHModel
        oCMP.ProfileValue = oVK.Code
        If oCMP.ID < 1 Then
            oCMPFac.Insert(oCMP)
        Else
            oCMPFac.Update(oCMP)
        End If

        If Not IsNothing(oLeasing) Then
            oCMP = GetCMProfile(oCM, oPG, oPHLeasing)
            oCMP.ChassisMaster = oCM
            oCMP.ProfileGroup = oPG
            oCMP.ProfileHeader = oPHLeasing
            oCMP.ProfileValue = oLeasing.LeasingCode
            If oCMP.ID < 1 Then
                oCMPFac.Insert(oCMP)
            Else
                oCMPFac.Update(oCMP)
            End If
        End If

        If Not IsNothing(oKaroseri) Then
            oCMP = GetCMProfile(oCM, oPG, oPHKaroseri)
            oCMP.ChassisMaster = oCM
            oCMP.ProfileGroup = oPG
            oCMP.ProfileHeader = oPHKaroseri
            oCMP.ProfileValue = oKaroseri.Code
            If oCMP.ID < 1 Then
                oCMPFac.Insert(oCMP)
            Else
                oCMPFac.Update(oCMP)
            End If
        End If
    End Sub

    Private Function GetCMProfile(ByRef oCM As ChassisMaster, ByRef oPG As ProfileGroup, ByRef oPH As ProfileHeader) As ChassisMasterProfile
        Dim oCMPFac As New ChassisMasterProfileFacade(User)
        Dim cCMP As New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aCMP As ArrayList


        cCMP.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, oCM.ID))
        cCMP.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
        cCMP.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
        aCMP = oCMPFac.Retrieve(cCMP)
        If aCMP.Count > 0 Then
            Return CType(aCMP(0), ChassisMasterProfile)
        Else
            Return New ChassisMasterProfile
        End If
    End Function

    Private Function ValidateNomorReferensi(ByVal nomor As String) As Boolean
        Dim _invalidString As String = KTB.DNet.Lib.WebConfig.GetValue("InvalidCharacter")
        If nomor.Trim <> String.Empty Then
            Dim _firstChar As String = nomor.Substring(0, 1)
            For Each item As String In _invalidString
                If _firstChar = item Then
                    Return False
                End If
            Next
            Return True
        Else
            Return True
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Page.IsValid Then
            MessageBox.Show("Input data belum lengkap")
            Exit Sub
        End If
        If Not ValidateNomorReferensi(txtSRNomorSurat.Text.Trim) Then
            MessageBox.Show("Nomor Referensi tidak valid.")
            Exit Sub
        End If
        Dim ddlJenis As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlJenis"), String))
        Dim ddlModel As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlModel"), String))
        Dim ddlLeasing As DropDownList = Me.pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))


        If ddlLeasing.SelectedIndex = 0 Then
            Dim oPHFac As New ProfileHeaderFacade(User)
            Dim oPHPayment As ProfileHeader = oPHFac.Retrieve("CBU_WAYPAID1")

            Dim oPG As ProfileGroup
            Dim oPGFac As New ProfileGroupFacade(User)
            oPG = oPGFac.Retrieve("cust_prf_" & _objChassisMaster.Category.CategoryCode.ToLower)
            Dim oPHTGFac As New ProfileHeaderToGroupFacade(User)
            Dim cCMP As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
            Dim oPHTGList As ArrayList = oPHTGFac.RetrieveByCriteria(cCMP)

            Dim _ctlPayment As String = ""

            For Each phtg As ProfileHeaderToGroup In oPHTGList
                If phtg.ProfileHeader.ID = oPHPayment.ID Then
                    _ctlPayment = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
                End If
            Next

            Dim ddlPayment As DropDownList = Me.pnlInformasion.FindControl(_ctlPayment)
        End If

        Dim oVK As VehicleKind = New VehicleKindFacade(User).Retrieve(CType(ddlModel.SelectedValue, Integer))
        If oVK.VehicleKindGroupID <> ddlJenis.SelectedValue Then
            MessageBox.Show("Simpan gagal.\n Model kendaraan tidak sesuai dengan jenis kendaraan")
            Exit Sub
        End If


        SaveInvoice()
        If (_sErrorMessage <> "") Then
            MessageBox.Show(_sErrorMessage)
        Else
            MessageBox.Show(_sSuccessMessage)
            If IsOpenInvoice Then
                EnableAll()
            Else
                DisableAll()
            End If
            ToggleButton()
            Me.lblCreatedBy.Text = "<b>" & UserInfo.Convert(_objEndCustomer.SaveBy) & "</b> pada tanggal <b>" & _objEndCustomer.SaveTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
            LoadPage()
        End If

    End Sub

#End Region

    Private Function IsValidTglFaktur() As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, icInvoiceDate.Value.Day))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, icInvoiceDate.Value.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, icInvoiceDate.Value.Year))
        Dim arlNationalHoliday As ArrayList = New NationalHolidayFacade(User).RetrieveByCriteria(criterias)

        If arlNationalHoliday.Count > 0 Then
            Dim objTimeSpan As TimeSpan = icInvoiceDate.Value.Subtract(DateTime.Now)
            If objTimeSpan.Days >= 1 Then
                For i As Integer = 1 To objTimeSpan.Days
                    Dim criterias1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, icInvoiceDate.Value.AddDays(i * -1).Day))
                    criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayMonth", MatchType.Exact, icInvoiceDate.Value.AddDays(i * -1).Month))
                    criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayYear", MatchType.Exact, icInvoiceDate.Value.AddDays(i * -1).Year))
                    Dim arlNationalHoliday1 As ArrayList = New NationalHolidayFacade(User).RetrieveByCriteria(criterias1)
                    If arlNationalHoliday1.Count = 0 Then
                        Return True
                    End If
                Next
                Return False
            Else
                Return False
            End If
        Else
            Return True
        End If
    End Function

    Private Sub ToggleAdditionalInformationControl(ByVal value As Boolean)
        Me.lblPWAmount.Visible = value
        Me.lblPWBank.Visible = value
        Me.lblPWNomorGiro.Visible = value
        Me.lblPWPaymentMethod.Visible = value
        Me.lblPPAmount.Visible = value
        Me.lblPPBank.Visible = value
        Me.lblPPNomorGiro.Visible = value
        Me.lblPPPaymentMethod.Visible = value
        Me.lblSRNomorSurat.Visible = value
        Me.lblNoMCP.Visible = value
        Me.lblNoLKPP.Visible = value
        Me.lblNoFleet.Visible = value

        Me.ddlPWPaymentMethod.Visible = Not value
        Me.txtPWAmount.Visible = Not value
        Me.txtPWBank.Visible = Not value
        Me.txtPWNomorGiro.Visible = Not value
        Me.ddlPPPaymentMethod.Visible = Not value
        Me.txtPPAmount.Visible = Not value
        Me.txtPPBank.Visible = Not value
        Me.txtPPNomorGiro.Visible = Not value
        Me.txtSRNomorSurat.Visible = Not value
        Me.txtNoMCP.Visible = Not value
        Me.txtNoLKPP.Visible = Not value
        Me.txtNoFleetReq.Visible = Not value
        Me.lblMCPNumber.Visible = Not value
        Me.lblLKPPNumber.Visible = Not value
        Me.lblNoRegRequest.Visible = Not value

        '--add by wdi 20161213
        If _objChassisMaster.Category.CategoryCode <> "CV" Then
            '--PC or LCV then disable fleet
            lblNoRegRequest.Visible = False
            txtNoFleetReq.Text = ""
        End If
    End Sub

    Private Sub AdditionalInformationToControl()
        If IsOpenInvoice Then
            ToggleAdditionalInformationControl(False)
            Dim objPaymentMethodFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
            bindArrayListToDropDownList(Me.ddlPWPaymentMethod, objPaymentMethodFacade.RetrievePaymentMethodList)
            bindArrayListToDropDownList(Me.ddlPPPaymentMethod, objPaymentMethodFacade.RetrievePaymentMethodList)
        Else
            ToggleAdditionalInformationControl(True)
        End If
    End Sub

    Private Sub ProvinceToControl()
        Dim objProvinceFacade As ProvinceFacade = New ProvinceFacade(User)
        'Me.ddlCustomerProvince.DataSource = objProvinceFacade.RetrieveList("ProvinceName", Sort.SortDirection.ASC)
        'Me.ddlCustomerProvince.DataTextField = "ProvinceName"
        'Me.ddlCustomerProvince.DataValueField = "ID"
        'Me.ddlCustomerProvince.DataBind()
        'ddlCustomerProvince.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub CityToControl()
        'Me.ddlCity.Items.Clear()
        'If Me.ddlCustomerProvince.SelectedIndex > 0 Then
        '    Dim objCityFacade As CityFacade = New CityFacade(User)
        '    Dim objCreteria As CriteriaComposite = New CriteriaComposite( _
        '        New Criteria(GetType(City), "RowStatus", CType(DBRowStatus.Active, Short)))
        '    If IsOpenInvoice Then
        '        objCreteria.opAnd(New Criteria(GetType(City), "Status", "A"))
        '    End If
        '    If Me.ddlCustomerProvince.SelectedIndex > 0 Then
        '        objCreteria.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, CType(Me.ddlCustomerProvince.SelectedValue, Integer)))
        '    End If

        '    Me.ddlCity.DataSource = objCityFacade.Retrieve(objCreteria)
        '    Me.ddlCity.DataTextField = "CityName"
        '    Me.ddlCity.DataValueField = "ID"
        '    Me.ddlCity.DataBind()
        '    ddlCity.Items.Insert(0, "Silahkan Pilih")
        'Else
        '    ddlCity.Items.Insert(0, "Silahkan Pilih")
        'End If
    End Sub

    Private Sub bindArrayListToDropDownList(ByRef objDropDownList As DropDownList, ByVal objArrayList As ArrayList)
        objDropDownList.DataSource = objArrayList
        objDropDownList.DataTextField = "Description"
        objDropDownList.DataValueField = "ID"
        objDropDownList.DataBind()
        objDropDownList.Items.Insert(0, "Silahkan Pilih")
    End Sub
    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        If Not IsNothing(sender) AndAlso Not Page.IsValid Then
            MessageBox.Show("Input data belum lengkap")
            Exit Sub
        End If

        If lblNamaPesananKhusus.Text <> String.Empty Then
            If txtSRNomorSurat.Text = String.Empty Then
                MessageBox.Show("Pesanan Khusus : Surat Referensi harus diisi")
                Exit Sub
            End If
        End If
        If Me.txtMCPConfirmation.Text = "-1" Then
            MessageBox.Confirm("Apakah proses MCP data ini sudah selesai?", "txtMCPConfirmation")
            Exit Sub
        End If

        'LKPP
        'If Me.txtLKPPConfirmation.Text = "-1" Then
        '    MessageBox.Confirm("Apakah proses LKPP data ini sudah selesai?", "txtLKPPConfirmation")
        '    Exit Sub
        'End If
        'LKPP
        _bIsValidating = True
        SaveInvoice()
        Dim isValid As Boolean = True

        If _sErrorMessage = "" Then
            _objChassisMaster = GetFromSession("ChassisMaster")
            If _objChassisMaster.EndCustomer.Customer Is Nothing Then
                MessageBox.Show("Belum bisa divalidasi karena data konsumen masih kosong")
            Else
                _objEndCustomer = _objChassisMaster.EndCustomer
                Dim objChassisMasterFac As ChassisMasterFacade = New ChassisMasterFacade(User)

                If Not objChassisMasterFac.IsAreaViolationFree(_objChassisMaster) Then
                    If _objChassisMaster.EndCustomer.AreaViolationFlag = "X" Or _objChassisMaster.EndCustomer.ReferenceLetterFlag = "X" Then
                        isValid = True
                    Else
                        isValid = False
                        _sErrorMessage = _sErrorMessage & "Informasi Tambahan - Pelanggaran Wilayah atau Surat Referensi wajib diisi\n"
                    End If
                End If

                If _objChassisMaster.DiscountAmount > 0 Then
                    If _objChassisMaster.EndCustomer.PenaltyFlag = "X" Or _objChassisMaster.EndCustomer.ReferenceLetterFlag = "X" Then
                        isValid = True
                    Else
                        isValid = False
                        _sErrorMessage = _sErrorMessage & "Informasi Tambahan - Pembayaran Penalti atau Surat Referensi wajib diisi\n"
                    End If
                End If

                If isValid Then
                    _objChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Validasi ' "1"
                    _objChassisMaster.AlreadySaled = 2 'donas 20151209:set 'Sudah Terjual' in menu DealerStock, v_DealerStock
                    _objChassisMaster.AlreadySaledTime = DateTime.Now
                    _objChassisMaster.EndCustomer.ValidateBy = User.Identity.Name
                    _objChassisMaster.EndCustomer.ValidateTime = Date.Now
                    Dim iUpdated As Integer = 0
                    iUpdated = objChassisMasterFac.ValidateInvoice(_objChassisMaster)
                    If iUpdated = 2 Then
                        ToggleButton()
                        Me.lblValidatedBy.Text = "<b>" & UserInfo.Convert(User.Identity.Name) & "</b> pada tanggal <b>" & Date.Now.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
                        Me.lblStatus.Text = EnumChassisMaster.FakturStatusDesc(_objChassisMaster.FakturStatus)

                        'CustomerProfileToControl()
                        AdditionalInformationToControl()

                        EndCustomerToControl()

                        CityToControl()

                        If IsOpenInvoice Then
                            EnableAll()
                        Else
                            DisableAll()
                        End If

                        UpdateSpkHeaderStatus(_objEndCustomer.ID, True)
                        '---------------start indent system ' by rudi 20180103
                        'Dim objSPKHeader As SPKHeader = _objChassisMaster.EndCustomer.SPKFaktur.SPKHeader
                        'If Not objSPKHeader Is Nothing Then
                        '    Dim objUpdate As FinishUnit.SPKHeaderFacade = New FinishUnit.SPKHeaderFacade(User)
                        '    Dim objSPKHeaderUpdated As SPKHeader = objUpdate.Retrieve(objSPKHeader.ID)
                        '    If Not IsNothing(objSPKHeaderUpdated) Then
                        '        If objSPKHeader.SPKFakturs.Count > 0 Then
                        '            Dim qty As Integer = 0
                        '            For Each det As SPKDetail In objSPKHeader.SPKDetails
                        '                'Jika status spk detail tidak sama dengan reject dan batal
                        '                If det.Status <> 1 And det.Status <> 3 Then
                        '                    qty = qty + det.Quantity
                        '                End If
                        '            Next
                        '            If (objSPKHeader.SPKFakturs.Count = qty) Then
                        '                Dim iQtyC As Integer = 0
                        '                Dim iQtyNol As Integer = 0
                        '                For Each objSPKFaktur As SPKFaktur In objSPKHeader.SPKFakturs
                        '                    If Not objSPKFaktur.EndCustomer Is Nothing Then
                        '                        If Not objSPKFaktur.EndCustomer.ChassisMaster Is Nothing Then
                        '                            If objSPKFaktur.EndCustomer.ChassisMaster.FakturStatus.ToString <> "0" Then
                        '                                iQtyC = iQtyC + 1
                        '                            Else
                        '                                iQtyNol = iQtyNol + 1
                        '                            End If
                        '                        End If
                        '                    End If
                        '                Next

                        '                If (iQtyC = objSPKHeader.SPKFakturs.Count) Then

                        '                    Dim oldStatus As Integer = objSPKHeader.Status

                        '                    objSPKHeader.Status = EnumStatusSPK.Status.Selesai
                        '                    objUpdate = New FinishUnit.SPKHeaderFacade(User)
                        '                    objUpdate.Update(objSPKHeader)


                        '                    'Insert StatusChangeHistory
                        '                    Dim objNewStatus As New StatusChangeHistory
                        '                    objNewStatus.DocumentType = 6
                        '                    objNewStatus.DocumentRegNumber = objSPKHeader.SPKNumber
                        '                    objNewStatus.OldStatus = oldStatus
                        '                    objNewStatus.NewStatus = CInt(EnumStatusSPK.Status.Selesai)
                        '                    objNewStatus.RowStatus = 0

                        '                    Dim iReturn As Integer = New StatusChangeHistoryFacade(User).Insert(objNewStatus)
                        '                End If

                        '            End If
                        '        End If
                        '    End If
                        'End If
                        '--------------------

                        MessageBox.Show(SR.ValidateSucces())
                        _sesshelper.SetSession("ChassisMaster", _objChassisMaster)
                        _sesshelper.SetSession("FrmEntryInvoice_CalledBy", "FrmInvoiceReqList.aspx")
                        Server.Transfer("FrmEntryInvoice.aspx?ChassisMasterID=" & _objChassisMaster.ID)
                    Else
                        MessageBox.Show(SR.ValidateFail)
                    End If
                Else
                    MessageBox.Show(_sErrorMessage)
                End If
            End If
        Else
            MessageBox.Show(_sErrorMessage)
        End If
    End Sub

    Private Function IsCancelValidateValid(ByVal objChassis As ChassisMaster) As Boolean
        Dim _objChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(objChassis.ID)
        If _objChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Validasi Then
            Return True
        End If
        Return False
    End Function

    Private Sub btnCancelValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelValidate.Click
        _objChassisMaster = GetFromSession("ChassisMaster")
        If IsCancelValidateValid(_objChassisMaster) Then
            _objChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Baru
            _objChassisMaster.AlreadySaled = 0 'donas 20151209:set 'Belum Terjual' in menu DealerStock, v_DealerStock
            _objChassisMaster.AlreadySaledTime = DateTime.Now
            _objChassisMaster.EndCustomer.ValidateBy = String.Empty
            _objChassisMaster.EndCustomer.ValidateTime = Date.Parse("1/1/1753 12:00:00 AM")
            'must be checked
            _objEndCustomer = _objChassisMaster.EndCustomer
            Dim iUpdated As Integer = 0
            iUpdated = New ChassisMasterFacade(User).CancelInvoiceValidation(_objChassisMaster)
            If iUpdated = 2 Then

                ToggleButton()
                Me.lblStatus.Text = EnumChassisMaster.FakturStatusDesc(_objChassisMaster.FakturStatus)
                AdditionalInformationToControl()
                EndCustomerToControl()
                CityToControl()

                If IsOpenInvoice Then
                    EnableAll()
                Else
                    DisableAll()
                End If

                UpdateSpkHeaderStatus(_objEndCustomer.ID, False)

                '-------- add by rudi 2017/01/03, move logic from btnCancel
                'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(SPKFaktur), "EndCustomer.ID", MatchType.Exact, _objEndCustomer.ID))
                'Dim objSPKFakturList As ArrayList = New FinishUnit.SPKFakturFacade(User).Retrieve(criterias)
                'If objSPKFakturList.Count > 0 Then
                '    Dim objSPKFaktur As SPKFaktur = objSPKFakturList(0)

                '    Try
                '        Dim objSPKHeader As SPKHeader = New SPKHeader
                '        Dim objSPKFakturFacade As SPKFakturFacade = New SPKFakturFacade(User)
                '        Dim objSPKFaktur2 As SPKFaktur = objSPKFakturFacade.Retrieve(objSPKFaktur.ID)
                '        objSPKHeader = objSPKFaktur2.SPKHeader
                '        Dim status As EnumStatusSPK.Status
                '        status = objSPKHeader.Status
                '        Select Case status
                '            Case EnumStatusSPK.Status.Selesai
                '                Dim iQty As Integer = 0
                '                For Each detil As SPKDetail In objSPKHeader.SPKDetails
                '                    iQty = detil.Quantity + iQty
                '                Next

                '                Dim critHist As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '                critHist.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, 6))
                '                critHist.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, objSPKHeader.SPKNumber))
                '                critHist.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.Exact, CInt(EnumStatusSPK.Status.Selesai)))
                '                Dim sortColl As SortCollection = New SortCollection
                '                sortColl.Add(New Sort(GetType(StatusChangeHistory), "CreatedTime", Sort.SortDirection.DESC))

                '                Dim objStatusList As ArrayList = New StatusChangeHistoryFacade(User).Retrieve(critHist, sortColl)
                '                If Not IsNothing(objStatusList) AndAlso objStatusList.Count > 0 Then
                '                    Dim objStatus As StatusChangeHistory = CType(objStatusList(0), StatusChangeHistory)
                '                    objSPKHeader.Status = CInt(objStatus.OldStatus)
                '                    'Update SPKHeader
                '                    Dim objSPKHeaderFac As SPKHeaderFacade = New SPKHeaderFacade(User)
                '                    objSPKHeaderFac.Update(objSPKHeader)

                '                    'Insert StatusChangeHistory
                '                    Dim objNewStatus As New StatusChangeHistory
                '                    objNewStatus.DocumentType = 6
                '                    objNewStatus.DocumentRegNumber = objSPKHeader.SPKNumber
                '                    objNewStatus.OldStatus = CInt(EnumStatusSPK.Status.Selesai)
                '                    objNewStatus.NewStatus = CInt(objStatus.OldStatus)
                '                    objNewStatus.RowStatus = 0

                '                    Dim iReturn As Integer = New StatusChangeHistoryFacade(User).Insert(objNewStatus)
                '                End If
                '        End Select
                '    Catch ex As Exception
                '        MessageBox.Show("Update SPK atas Faktur tersebut, gagal.")
                '    End Try
                'End If
                ''------------------end add by rudi

                MessageBox.Show(SR.UpdateSucces)
                _sesshelper.SetSession("ChassisMaster", _objChassisMaster)
                _sesshelper.SetSession("FrmEntryInvoice_CalledBy", "FrmInvoiceReqList.aspx")
                Server.Transfer("FrmEntryInvoice.aspx?ChassisMasterID=" & _objChassisMaster.ID)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            MessageBox.Show("Proses batal tidak berhasil, karena data sudah di proses")
        End If

    End Sub

    Private Sub UpdateSpkHeaderStatus(ByVal endCustomerID As Integer, ByVal bValidate As Boolean)
        Try
            Dim spkHeaderData As SPKHeader = GetSpkHeader(endCustomerID)

            If Not IsSpkHeaderNeedToUpdate(spkHeaderData, endCustomerID, bValidate) Then
                Exit Sub
            Else
                UpdateSpkHeaderAndInsertStatusChangeHistory(spkHeaderData, bValidate)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub UpdateSpkHeaderAndInsertStatusChangeHistory(ByVal spkHeaderData As SPKHeader, ByVal bValidate As Boolean)
        Dim newStatus As String
        Dim oldStatus As String = spkHeaderData.Status

        If bValidate Then
            newStatus = EnumStatusSPK.Status.Selesai
        Else
            newStatus = GetPreviousStatus(spkHeaderData.SPKNumber)
        End If

        JustUpdateSpkHeader(spkHeaderData, newStatus)
        ' InsertStatusChangeHistory(spkHeaderData.SPKNumber, newStatus, oldStatus) 'sudah jalan saat execute updatespkheader
    End Sub

    Private Sub InsertStatusChangeHistory(ByVal spkNumber As String, ByVal newStatus As String, ByVal oldStatus As String)
        Try
            Dim objNewStatus As New StatusChangeHistory
            objNewStatus.DocumentType = 6
            objNewStatus.DocumentRegNumber = spkNumber
            objNewStatus.OldStatus = CInt(oldStatus)
            objNewStatus.NewStatus = CInt(newStatus)
            objNewStatus.RowStatus = 0

            Dim iReturn As Integer = New StatusChangeHistoryFacade(User).Insert(objNewStatus)
        Catch ex As Exception
            Throw New Exception("Gagal dalam menginput history")
        End Try
    End Sub

    Private Sub JustUpdateSpkHeader(ByVal spkHeaderData As SPKHeader, ByVal newStatus As String)
        Try
            spkHeaderData.Status = CInt(newStatus)

            Dim objSPKHeaderFac As SPKHeaderFacade = New SPKHeaderFacade(User)
            objSPKHeaderFac.Update(spkHeaderData)
        Catch ex As Exception
            Throw New Exception("Gagal dalam mengupdate status SPK")
        End Try

    End Sub

    Private Function GetPreviousStatus(ByVal spkNumber As String) As String
        Try
            Dim critHist As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critHist.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, 6))
            critHist.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, spkNumber))
            critHist.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.Exact, CInt(EnumStatusSPK.Status.Selesai)))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(StatusChangeHistory), "CreatedTime", Sort.SortDirection.DESC))

            Dim objStatusList As ArrayList = New StatusChangeHistoryFacade(User).Retrieve(critHist, sortColl)
            If Not IsNothing(objStatusList) AndAlso objStatusList.Count > 0 Then
                Dim objStatus As StatusChangeHistory = CType(objStatusList(0), StatusChangeHistory)
                Return objStatus.OldStatus.ToString()
            Else
                Throw New Exception("Tidak dapat menemukan history spk sebelumnya")
            End If
        Catch ex As Exception
            Throw New Exception("Tidak dapat menemukan history spk sebelumnya")
        End Try
    End Function

    Private Function IsSpkHeaderNeedToUpdate(ByVal spkHeaderData As SPKHeader, ByVal endCustomerID As Integer, ByVal bValidate As Boolean) As Boolean

        If bValidate = False And spkHeaderData.Status <> EnumStatusSPK.Status.Selesai Then
            Return False
        End If

        If bValidate = True And spkHeaderData.Status = EnumStatusSPK.Status.Selesai Then
            Return False
        End If

        If Not IsSpkQuantityValid(spkHeaderData, bValidate) Then
            Return False
        End If

        Return True
    End Function

    Private Function IsSpkQuantityValid(ByVal spkheaderData As SPKHeader, ByVal bValidate As Boolean) As Boolean
        Try
            Dim iQtySpk As Integer = GetQtySpk(spkheaderData)
            Dim iQtyFakturNormal As Integer = GetQtyFakturNormal(spkheaderData)
            Dim iQtyFakturRevisi As Integer = GetQtyFakturRevisi(spkheaderData)

            If bValidate = False Then
                If iQtySpk > (iQtyFakturNormal + iQtyFakturRevisi) Then
                    Return True
                Else
                    Return False
                End If
            Else
                If iQtySpk = (iQtyFakturNormal + iQtyFakturRevisi) Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw New Exception("Error dalam validasi quantity SPK")
        End Try


    End Function

    Private Function GetQtySpk(ByVal spkheaderData As SPKHeader) As Integer
        Dim result As Integer = 0

        For Each spkDetailData As SPKDetail In spkheaderData.SPKDetails
            result += spkDetailData.Quantity
        Next

        Return result
    End Function


    Private Function GetQtyFakturNormal(ByVal spkheaderData As SPKHeader) As Integer
        Dim result As Integer = 0

        For Each spkFakturData As SPKFaktur In spkheaderData.SPKFakturs
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.ID", MatchType.Exact, spkFakturData.EndCustomer.ID))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.No, 0)) ' <> baru
            Dim lstChassisMaster As ArrayList = New FinishUnit.ChassisMasterFacade(User).Retrieve(criterias)

            result += lstChassisMaster.Count
        Next

        Return result
    End Function

    Private Function GetQtyFakturRevisi(ByVal spkheaderData As SPKHeader) As Integer
        Dim result As Integer = 0

        For Each spkFakturData As SPKFaktur In spkheaderData.SPKFakturs
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionSPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionSPKFaktur), "SPKHeader.ID", MatchType.Exact, spkFakturData.SPKHeader.ID))
            Dim lstRevisionSPKFaktur As ArrayList = New RevisionSPKFakturFacade(User).Retrieve(criterias)

            For Each revisionSPKFakturData As RevisionSPKFaktur In lstRevisionSPKFaktur
                Dim criteriasRevision As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasRevision.opAnd(New Criteria(GetType(RevisionFaktur), "EndCustomer.ID", MatchType.Exact, revisionSPKFakturData.EndCustomer.ID))
                criteriasRevision.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.No, 0)) ' <> baru
                Dim lstRevisionFaktur As ArrayList = New RevisionFakturFacade(User).Retrieve(criteriasRevision)
                result += lstRevisionFaktur.Count
            Next
        Next
        Return result
    End Function

    Private Function GetSpkHeader(ByVal endCustomerID As Integer) As SPKHeader
        Dim result As New SPKHeader

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPKFaktur), "EndCustomer.ID", MatchType.Exact, endCustomerID))
        Dim objSPKFakturList As ArrayList = New FinishUnit.SPKFakturFacade(User).Retrieve(criterias)

        If objSPKFakturList.Count > 0 Then
            Dim objSPKFaktur As SPKFaktur = objSPKFakturList(0)

            Dim objSPKFakturFacade As SPKFakturFacade = New SPKFakturFacade(User)
            Dim objSPKFaktur2 As SPKFaktur = objSPKFakturFacade.Retrieve(objSPKFaktur.ID)
            result = objSPKFaktur2.SPKHeader

        End If

        Return result
    End Function

    Private Sub ddlCustomerProvince_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CityToControl()
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Dim url As String = CType(Session("FrmEntryInvoice_CalledBy"), String)
        If Not url Is Nothing Then
            Server.Transfer(url)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.icInvoiceDate.Value = Date.Now
        _objChassisMaster = CType(GetFromSession("ChassisMaster"), ChassisMaster)

        _objEndCustomer = _objChassisMaster.EndCustomer
        _objEndCustomer.Customer = Nothing

        _objEndCustomer.RefChassisNumberID = Nothing
        _objEndCustomer.FakturDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        _objEndCustomer.ValidateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        _objEndCustomer.SaveBy = User.Identity.Name
        _objEndCustomer.SaveTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        _objEndCustomer.MCPHeader = Nothing
        _objEndCustomer.LKPPHeader = Nothing
        _objEndCustomer.IsTemporary = -1

        Dim objFac As EndCustomerFacade = New EndCustomerFacade(User)
        If objFac.Update(_objEndCustomer) = 1 Then
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SPKFaktur), "EndCustomer.ID", MatchType.Exact, _objEndCustomer.ID))
                Dim objSPKFakturList As ArrayList = New FinishUnit.SPKFakturFacade(User).Retrieve(criterias)
                If objSPKFakturList.Count > 0 Then
                    Dim objSPKFaktur As SPKFaktur = objSPKFakturList(0)
                    objSPKFaktur.RowStatus = CType(DBRowStatus.Deleted, Short)
                    Dim objSPKFakturFacade As SPKFakturFacade = New SPKFakturFacade(User)
                    objSPKFakturFacade.Update(objSPKFaktur)

                    '    'start add by anh 201707 - indent system req by miyuki
                    '    'next harus dipindah ke sp
                    '    Try
                    '        Dim objSPKHeader As SPKHeader = New SPKHeader
                    '        objSPKHeader = objSPKFaktur.SPKHeader
                    '        Dim status As EnumStatusSPK.Status
                    '        status = objSPKHeader.Status
                    '        Select Case status
                    '            Case EnumStatusSPK.Status.Selesai
                    '                Dim iQty As Integer = 0
                    '                For Each detil As SPKDetail In objSPKHeader.SPKDetails
                    '                    iQty = detil.Quantity + iQty
                    '                Next
                    '                If objSPKHeader.SPKFakturs.Count < iQty Then
                    '                    Dim critHist As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    '                    critHist.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, 6))
                    '                    critHist.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, objSPKHeader.SPKNumber))
                    '                    critHist.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.Exact, CInt(EnumStatusSPK.Status.Selesai)))

                    '                    Dim sortColl As SortCollection = New SortCollection
                    '                    sortColl.Add(New Sort(GetType(StatusChangeHistory), "CreatedTime", Sort.SortDirection.DESC))

                    '                    Dim objStatusList As ArrayList = New StatusChangeHistoryFacade(User).Retrieve(critHist, sortColl)
                    '                    If Not IsNothing(objStatusList) AndAlso objStatusList.Count > 0 Then
                    '                        Dim objStatus As StatusChangeHistory = CType(objStatusList(0), StatusChangeHistory)
                    '                        objSPKHeader.Status = CInt(objStatus.OldStatus)
                    '                        'Update SPKHeader
                    '                        Dim objSPKHeaderFac As SPKHeaderFacade = New SPKHeaderFacade(User)
                    '                        objSPKHeaderFac.Update(objSPKHeader)

                    '                        'Insert StatusChangeHistory
                    '                        Dim objNewStatus As New StatusChangeHistory
                    '                        objNewStatus.DocumentType = 6
                    '                        objNewStatus.DocumentRegNumber = objSPKHeader.SPKNumber
                    '                        objNewStatus.OldStatus = CInt(EnumStatusSPK.Status.Selesai)
                    '                        objNewStatus.NewStatus = CInt(objStatus.OldStatus)
                    '                        objNewStatus.RowStatus = 0

                    '                        Dim iReturn As Integer = New StatusChangeHistoryFacade(User).Insert(objNewStatus)
                    '                    End If
                    '                End If
                    '        End Select
                    '    Catch ex As Exception
                    '        MessageBox.Show("Update SPK atas Faktur tersebut, gagal.")
                    '    End Try

                    '    'end add by anh 201707 - indent system req by miyuki
                End If

                'Update MCPDeatail ; add by anh 20150624 for yurike related to mcp
                '--Start
                If Not IsNothing(_objEndCustomer.MCPHeader) Then


                    'Dim critMCPDet As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MCPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'critMCPDet.opAnd(New Criteria(GetType(MCPDetail), "MCPHeader.ID", MatchType.Exact, _objEndCustomer.MCPHeader.ID))
                    'critMCPDet.opAnd(New Criteria(GetType(MCPDetail), "VechileType.ID", MatchType.Exact, _objEndCustomer.ChassisMaster.VechileColor.VechileType.ID))
                    'Dim objMCPDetail As MCPDetail = New MCPDetailFacade(User).Retrieve(critMCPDet)(0)
                    'If Not IsNothing(objMCPDetail) Then
                    '    If objMCPDetail.UnitRemain < objMCPDetail.Unit Then
                    '        objMCPDetail.UnitRemain = objMCPDetail.UnitRemain + 1
                    '    End If
                    '    Dim i As Integer = New MCPDetailFacade(User).Update(objMCPDetail)
                    'End If
                End If
                '--End

                'Update FleetRequest ; add by wdi 20161117 for isye 
                '--Start
                If Not IsNothing(_objChassisMaster.FleetFaktur) Then
                    Dim objFleetFaktur As FleetFaktur = _objChassisMaster.FleetFaktur
                    objFleetFaktur.RowStatus = CType(DBRowStatus.Deleted, Short)
                    Dim oFleetFakturFacade As FleetFakturFacade = New FleetFakturFacade(User)
                    oFleetFakturFacade.Insert(objFleetFaktur)
                End If
                '--End

                MessageBox.Show("Data berhasil dibatalkan")
            Catch ex As Exception
                'MessageBox.Show("Data gagal dibatalkan ")
            End Try
        Else
            MessageBox.Show("Data gagal dibatalkan ")
        End If

        Me.lblCreatedBy.Text = "<b>" & UserInfo.Convert(_objEndCustomer.SaveBy) & "</b> pada tanggal <b>" & _objEndCustomer.SaveTime.ToString("dd/MM/yyyy HH:mm:ss") & "</b>"
    End Sub

    Private Sub btnBindModel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBindModel.Click
        Me.BindDDLModel(False)
    End Sub

    Private Function GetDDLProfile(ByVal strPHCode As String) As DropDownList
        Dim oPHFac As New ProfileHeaderFacade(User)
        Dim oPHPayment As ProfileHeader = oPHFac.Retrieve("CBU_WAYPAID1")

        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        oPG = oPGFac.Retrieve("cust_prf_" & _objChassisMaster.Category.CategoryCode.ToLower)
        Dim oPHTGFac As New ProfileHeaderToGroupFacade(User)
        Dim cCMP As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cCMP.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
        Dim oPHTGList As ArrayList = oPHTGFac.RetrieveByCriteria(cCMP)

        Dim _ctlPayment As String = ""

        For Each phtg As ProfileHeaderToGroup In oPHTGList
            If phtg.ProfileHeader.ID = oPHPayment.ID Then
                _ctlPayment = "DDLIST" & phtg.ID.ToString & "_" & phtg.ProfileGroup.ID.ToString
            End If
        Next

        Return pnlInformasion.FindControl(_ctlPayment)
    End Function

#Region "Leasing & Karoseri"

    Protected Sub btnDisableLeasing_Click(sender As Object, e As EventArgs) Handles btnDisableLeasing.Click
        Dim ddlPayment As DropDownList = GetDDLProfile("CBU_WAYPAID1")

        Dim ddlLeasing As DropDownList = pnlInformasion.FindControl(CType(_sesshelper.GetSession("ddlLeasing"), String))

        If ddlPayment.SelectedItem.Text.Trim = "TUNAI" Then
            ddlLeasing.SelectedIndex = 0
            ddlLeasing.Enabled = False
        ElseIf ddlPayment.SelectedItem.Text = "KREDIT" Then
            ddlLeasing.Enabled = True
        End If
    End Sub

#End Region
End Class