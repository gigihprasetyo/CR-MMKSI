#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region


Public Class FrmCeilingBlockedList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtCreditAccount As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchCredit As System.Web.UI.WebControls.Label
    Protected WithEvents icReqDeliveryStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icReqDeliveryEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRegPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlPaymentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents imgStatus As System.Web.UI.WebControls.Image
    Protected WithEvents btnPass As System.Web.UI.WebControls.Button
    Protected WithEvents btnKonfirmasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnRelease As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpanGrid As System.Web.UI.WebControls.Button
    Protected WithEvents txtColorGreen As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTotalHargaTebus As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents Image2 As System.Web.UI.WebControls.Image
    Protected WithEvents Image3 As System.Web.UI.WebControls.Image
    Protected WithEvents ddlFactoring As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblFactoringColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblFactoring As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Private sHelper As SessionHelper = New SessionHelper

#End Region

#Region "Custom Method"

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Display_list_blocked_po_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Sales Credit Control - Ceiling Blocked List")
        End If
    End Sub


    Private Sub BindFactoring()
        Me.ddlFactoring.Items.Clear()
        Me.ddlFactoring.Items.Add(New ListItem("Silahkan Pilih", 2))
        Me.ddlFactoring.Items.Add(New ListItem("Factoring", 1))
        Me.ddlFactoring.Items.Add(New ListItem("Non Factoring", 0))

        Dim IsDSF As Boolean = (CType(Session.Item("DEALER"), Dealer).Title = CType(EnumDealerTittle.DealerTittle.LEASING, String)) '(CType(Session.Item("DEALER"), Dealer).DealerCode.Trim.ToUpper = "DSF")
        If IsDSF Then
            Me.ddlFactoring.SelectedValue = 1
            Me.ddlFactoring.Enabled = False
        Else
            Me.ddlFactoring.SelectedValue = 2
            Me.ddlFactoring.Enabled = True
        End If
    End Sub


    Private Sub Initialization()
        Dim objDealer As Dealer
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

        viewstate.Add("SQLSort.Field", "ContractHeader.Dealer.CreditAccount")
        ViewState.Add("SQLSort.Direction", Sort.SortDirection.ASC)

        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True, companyCode)
        ddlPaymentType.Items.Clear()
        ddlPaymentType.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each li As ListItem In enumPaymentType.GetList
            ddlPaymentType.Items.Add(li)
        Next
        ddlPaymentType.SelectedValue = -1

        BindFactoring()


        objDealer = sHelper.GetSession("DEALER")
        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            txtDealerCode.Text = objDealer.DealerCode
            txtCreditAccount.Text = objDealer.CreditAccount
            txtDealerCode.Enabled = False
            txtCreditAccount.Enabled = False
            lblSearchDealer.Visible = False
            lblSearchCredit.Visible = False
            btnPass.Visible = False
            btnKonfirmasi.Visible = False
            btnRelease.Visible = False

        End If
        If SecurityProvider.Authorize(context.User, SR.Display_list_blocked_po_privilege) Then
            Me.btnPass.Visible = False
            Me.btnKonfirmasi.Visible = False
            Me.btnRelease.Visible = False
            Me.btnSimpanGrid.Visible = False
            Me.btnBatal.Visible = False
            Me.dtgMain.Columns(14).Visible = False '14 Keterangan
        End If
        If SecurityProvider.Authorize(context.User, SR.Pass_ceiling_release_simpan_blocked_po_privilege) Then
            Me.btnPass.Visible = True
            Me.btnRelease.Visible = True
            Me.btnSimpanGrid.Visible = True
            Me.btnBatal.Visible = True
            btnPass.Enabled = True
            btnRelease.Enabled = True
            btnSimpanGrid.Enabled = True
            Me.btnBatal.Enabled = True
            Me.dtgMain.Columns(14).Visible = True

        Else
            btnPass.Enabled = False
            btnRelease.Enabled = False
            btnSimpanGrid.Enabled = False
        End If
        If SecurityProvider.Authorize(Context.User, SR.Konfirmasi_pass_ceiling_blocked_po_privilege) Then
            'btnKonfirmasi.Enabled = SecurityProvider.Authorize(Context.User, SR.Konfirmasi_pass_ceiling_blocked_po_privilege)
            btnKonfirmasi.Enabled = True
            btnKonfirmasi.Visible = True
        End If
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Silahkan Pilih", -1))
        ddlStatus.Items.Add(New ListItem("Blok", CType(enumPOBlockedStatus.POBlockedStatus.Blocked, Integer)))
        ddlStatus.Items.Add(New ListItem("Pass Ceiling", CType(enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed, Integer)))
        ddlStatus.Items.Add(New ListItem("Pass dan Konfirmasi", CType(enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed, Integer)))
        'enumPOBlockedStatus.POBlockedStatus
    End Sub

    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function

    Private Sub BindDTG(ByVal PgIdx As Integer)
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim crtPOH As CriteriaComposite
        Dim arlPOH As ArrayList = New ArrayList
        Dim objD As Dealer
        Dim TotalRow As Integer
        Dim aggTot As Aggregate
        Dim crtTot As CriteriaComposite '= New CriteriaComposite(New Criteria(GetType(V_RekapPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'Criteria Checking
        If txtDealerCode.Text.Trim <> "" And txtCreditAccount.Text.Trim <> "" Then
            objD = New DealerFacade(User).Retrieve(txtDealerCode.Text)
            If Not objD Is Nothing Then
                If objD.CreditAccount <> txtCreditAccount.Text Then
                    MessageBox.Show("Akun kredit dealer " & txtDealerCode.Text & " adalah " & objD.CreditAccount)
                    Exit Sub
                End If
            End If
        End If
        If icReqDeliveryStart.Value > icReqDeliveryEnd.Value Then
            MessageBox.Show("Periode Permintaan kirim salah")
            Exit Sub
        End If
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(icReqDeliveryStart.Value, "yyyy/MM/dd")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(icReqDeliveryEnd.Value, "yyyy/MM/dd")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "BlockedStatus", MatchType.InSet, "(" & enumPOBlockedStatus.POBlockedStatus.Blocked & "," & enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed & "," & enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed & ")"))
        If txtDealerCode.Text.Trim <> "" Then crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        If txtCreditAccount.Text.Trim <> "" Then crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, txtCreditAccount.Text))
        If txtPONumber.Text.Trim <> "" Then crtPOH.opAnd(New Criteria(GetType(POHeader), "DealerPONumber", MatchType.Exact, txtPONumber.Text))
        If txtRegPONumber.Text.Trim <> "" Then crtPOH.opAnd(New Criteria(GetType(POHeader), "PONumber", MatchType.Exact, txtRegPONumber.Text))
        If ddlPaymentType.SelectedValue <> -1 Then crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, ddlPaymentType.SelectedValue))
        If ddlStatus.SelectedValue <> -1 Then
            crtPOH.opAnd(New Criteria(GetType(POHeader), "BlockedStatus", MatchType.Exact, CType(ddlStatus.SelectedValue, Short)))
        End If
        If ddlFactoring.SelectedIndex <> 0 Then
            crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.Exact, CType(ddlFactoring.SelectedValue, Short)))
        End If
        'add by anh 20161002, req by yurike for payment trasfer
        'start by anh 20161002
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsTransfer", MatchType.Exact, 0))
        'end by anh 20161002

        Dim PCID As Short = Me.GetProductCategoryID()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        If PCID > 0 Then
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Category.ProductCategory.ID", MatchType.Exact, PCID))
        Else
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Category.ProductCategory.Code", MatchType.Exact, companyCode))
        End If
        'Criteria for Aggregrate
        crtTot = New CriteriaComposite(New Criteria(GetType(V_RekapPO), "POHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(icReqDeliveryStart.Value, "yyyy/MM/dd")))
        crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, Format(icReqDeliveryEnd.Value, "yyyy/MM/dd")))
        crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.BlockedStatus", MatchType.InSet, "(" & enumPOBlockedStatus.POBlockedStatus.Blocked & "," & enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed & "," & enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed & ")"))

        'add by anh 20161002, req by yurike for payment trasfer
        'start by anh 20161002
        crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.IsTransfer", MatchType.Exact, 0))
        'end by anh 20161002

        If txtDealerCode.Text.Trim <> "" Then crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.ContractHeader.Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        If txtCreditAccount.Text.Trim <> "" Then crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, txtCreditAccount.Text))
        If txtPONumber.Text.Trim <> "" Then crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.DealerPONumber", MatchType.Exact, txtPONumber.Text))
        If txtRegPONumber.Text.Trim <> "" Then crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.PONumber", MatchType.Exact, txtRegPONumber.Text))
        If ddlPaymentType.SelectedValue <> -1 Then crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, ddlPaymentType.SelectedValue))

        'without green dan non "Tahan DO" remark
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "", MatchType.Exact, ddlPaymentType.SelectedValue))

        arlPOH = objPOHFac.RetrieveActiveList(crtPOH, PgIdx + 1, dtgMain.PageSize, TotalRow, viewstate.Item("SQLSort.Field"), viewstate.Item("SQLSort.Direction"))

        sHelper.SetSession("arlPOHeader", arlPOH)
        dtgMain.CurrentPageIndex = PgIdx
        dtgMain.VirtualItemCount = TotalRow
        dtgMain.DataSource = arlPOH
        dtgMain.DataBind()

        'Total
        'objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed 
        'And objPOH.RemarkStatus <> enumPORemarkStatus.PORemarkStatus.TahanDO 

        aggTot = New Aggregate(GetType(V_RekapPO), "TotalHarga", AggregateType.Sum)
        Dim objVRPOFac As V_RekapPOFacade = New V_RekapPOFacade(User)
        Dim Total As Decimal = objVRPOFac.GetAggregateResult(aggTot, crtTot)
        Dim aggTotQty As New Aggregate(GetType(V_RekapPO), "TotalQuantity", AggregateType.Sum)
        Dim TotalQty As Integer = objVRPOFac.GetAggregateResult(aggTotQty, crtTot)

        crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.BlockedStatus", MatchType.Exact, CType(enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed, Short)))
        crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.RemarkStatus", MatchType.No, CType(enumPORemarkStatus.PORemarkStatus.TahanDO, Short)))
        'add by anh 20161002, req by yurike for payment trasfer
        'start by anh 20161002
        crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.IsTransfer", MatchType.Exact, 0))
        'end by anh 20161002

        If ddlFactoring.SelectedIndex <> 0 Then
            crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.IsFactoring", MatchType.Exact, CType(ddlFactoring.SelectedValue, Short)))
        End If
        If PCID > 0 Then
            crtTot.opAnd(New Criteria(GetType(V_RekapPO), "POHeader.ContractHeader.Category.ProductCategory.ID", MatchType.Exact, PCID))
        End If

        Total = Total - objVRPOFac.GetAggregateResult(aggTot, crtTot)
        TotalQty = TotalQty - objVRPOFac.GetAggregateResult(aggTotQty, crtTot)
        lblTotalHargaTebus.Text = FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Me.lblQuantity.Text = FormatNumber(TotalQty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        'Control Management
        For Each di As DataGridItem In dtgMain.Items
            Dim ddlRemarkStatus As DropDownList = di.FindControl("ddlRemarkStatus")
            Dim txtRemark As TextBox = di.FindControl("txtRemark")
            Dim lblTotal As Label = di.FindControl("lblTotal")

            If SecurityProvider.Authorize(context.User, SR.Display_list_blocked_po_privilege) Then
                ddlRemarkStatus.Enabled = False
                txtRemark.Enabled = False
            End If
            If SecurityProvider.Authorize(context.User, SR.Pass_ceiling_release_simpan_blocked_po_privilege) Then
                ddlRemarkStatus.Enabled = True
                txtRemark.Enabled = True
            End If
        Next
    End Sub

    Private Sub CreateSQLOrder(ByVal FieldName As String)
        If CType(viewstate.Item("SQLSort.Field"), String).ToUpper = FieldName.ToUpper Then
            If viewstate.Item("SQLSort.Direction") = Sort.SortDirection.ASC Then
                viewstate.Item("SQLSort.Direction") = Sort.SortDirection.DESC
            Else
                viewstate.Item("SQLSort.Direction") = Sort.SortDirection.ASC
            End If
        Else
            viewstate.Item("SQLSort.Field") = FieldName
            viewstate.Item("SQLSort.Direction") = Sort.SortDirection.ASC
        End If
    End Sub


    Private Function IsAnyRedStatusInCheckedItem() As Boolean
        For Each di As DataGridItem In dtgMain.Items
            Dim chkItem As CheckBox = di.FindControl("chkItem")
            Dim objPOH As POHeader = CType(CType(sHelper.GetSession("arlPOHeader"), ArrayList)(di.ItemIndex), POHeader)

            If chkItem.Checked And objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.Blocked Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function IsAnyYellowStatusInCheckedItem() As Boolean
        For Each di As DataGridItem In dtgMain.Items
            Dim chkItem As CheckBox = di.FindControl("chkItem")
            Dim objPOH As POHeader = CType(CType(sHelper.GetSession("arlPOHeader"), ArrayList)(di.ItemIndex), POHeader)

            If chkItem.Checked And objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function IsAnyHoldDOStatusInCheckedItem() As Boolean
        For Each di As DataGridItem In dtgMain.Items
            Dim ddlRemarkStatus As DropDownList = di.FindControl("ddlRemarkStatus")
            Dim chkItem As CheckBox = di.FindControl("chkItem")
            'Dim chkRemark As CheckBox = di.FindControl("chkRemark")
            Dim objPOH As POHeader = CType(CType(sHelper.GetSession("arlPOHeader"), ArrayList)(di.ItemIndex), POHeader)

            If chkItem.Checked And objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed And ddlRemarkStatus.SelectedValue = enumPORemarkStatus.PORemarkStatus.TahanDO Then
                Return True
            End If
            'If chkItem.Checked And objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed And chkRemark.Checked Then  'chkremark.Checked=Tahan DO
            '    Return True
            'End If
        Next
        Return False
    End Function

    Private Sub BindDdlRemark(ByRef ddlRemarkStatus As DropDownList)
        With ddlRemarkStatus.Items
            .Clear()
            .Add(New ListItem("Silihkan pilih", -1))
            For Each li As ListItem In (New enumPORemarkStatus).GetList()
                .Add(li)
            Next
        End With

    End Sub

#Region "AvailableCeiling"

    Private Function IsQualifiedPO(ByVal objPOH As POHeader) As Boolean
        Dim objCMFac As CreditMasterFacade = New CreditMasterFacade(User)
        Dim objCM As CreditMaster
        Dim AvCeiling As Decimal
        Dim TotalPO As Decimal = GetTotalPO(objPOH)
        Dim PaymentType As Short
        Dim objSCM As sp_CreditMaster
        Dim arlTemp As ArrayList = New ArrayList
        Dim TotalLiquefied As Decimal = 0
        Dim TotalAcceleratedGyro As Decimal = 0


        PaymentType = objPOH.TermOfPayment.PaymentType
        'Credit Ceiling
        objSCM = GetCeilingCredit(objPOH.ContractHeader.Category.ProductCategory, objPOH.ContractHeader.Dealer.CreditAccount, PaymentType)
        AvCeiling = (objSCM.Plafon - objSCM.OutStanding)
        'Proposed PO
        AvCeiling = AvCeiling - objSCM.ProposedPO
        'Liquefied and Accelerated Gyro
        objCM = objCMFac.Retrieve(objPOH.ContractHeader.Category.ProductCategory, objPOH.ContractHeader.Dealer.CreditAccount, PaymentType)
        TotalLiquefied = 0
        TotalAcceleratedGyro = 0
        For Each objDealer As Dealer In objCM.Dealers
            arlTemp = GetDealerPO(objDealer, PaymentType)
            TotalLiquefied += arlTemp(0)
            TotalAcceleratedGyro += arlTemp(1)
        Next
        AvCeiling = AvCeiling + TotalLiquefied + TotalAcceleratedGyro
        If TotalPO > AvCeiling Then
            'MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
            Return False
        Else
            Return True
        End If

    End Function

    Private Function GetTotalPO(ByVal objPOH As POHeader) As Decimal
        Dim Total As Decimal = 0
        Total = objPOH.TotalPODetail()
        'For Each objPOD As PODetail In objPOH.PODetails
        '    If objPOH.Status = 0 Or objPOH.Status = 2 Or objPOH.Status = 4 Then
        '        Total = Total + (objPOD.Price * objPOD.ReqQty)
        '    Else
        '        Total = Total + (objPOD.Price * objPOD.AllocQty)
        '    End If
        'Next
        Return Total
    End Function

    Private Function GetCeilingCredit(ByVal PC As ProductCategory, ByVal CreditAccount As String, ByVal PaymentType As Short) As sp_CreditMaster
        Dim objSCMFac As sp_CreditMasterFacade = New sp_CreditMasterFacade(User)
        Dim arlSCM As ArrayList
        Dim objSCM As sp_CreditMaster
        Dim crtSCM As CriteriaComposite
        Dim ReportDate As Date = icReqDeliveryStart.Value
        Dim ReqDelDate As Date = icReqDeliveryEnd.Value

        crtSCM = New CriteriaComposite(New Criteria(GetType(sp_CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "CreditAccount", MatchType.Exact, CreditAccount))
        crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "PaymentType", MatchType.Exact, PaymentType))
        arlSCM = objSCMFac.RetrieveFromSP(PC, ReportDate, ReqDelDate, crtSCM)
        If arlSCM.Count > 0 Then
            Return CType(arlSCM(0), sp_CreditMaster)
        Else
            Return Nothing
        End If
    End Function

    Private Function GetDealerPO(ByVal objDealer As Dealer, ByVal PaymentType As Short) As ArrayList
        Dim TotalLiquefied As Decimal = 0
        Dim TotalAcceleratedGyro As Decimal = 0
        Dim arlResult As ArrayList = New ArrayList
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim crtPOH As CriteriaComposite
        Dim arlPOH As ArrayList = New ArrayList
        Dim EffectiveDate As Date
        Dim tmpDate As Date
        Dim nTOPDays As Integer
        Dim ReportDate As Date = icReqDeliveryStart.Value
        Dim ReqDelDate As Date = icReqDeliveryEnd.Value

        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, objDealer.ID))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        arlPOH = objPOHFac.Retrieve(crtPOH)

        For Each objPOH As POHeader In arlPOH
            'Total TotalLiquefied 
            'Start  :Optimize EffectiveDate calculation;By:DoniN;20100331
            'If IsHavingGyro(objPOH) Then
            '    EffectiveDate = CType(objPOH.DailyPayments(0), DailyPayment).EffectiveDate
            'Else
            '    If objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
            '        nTOPDays = CType(objPOH.TermOfPayment.TermOfPaymentCode.Substring(1), Integer)
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, nTOPDays + 1)
            '    ElseIf objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.COD Then
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, 0 + 2)
            '    ElseIf objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS Then
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, 0)
            '    End If
            'End If
            EffectiveDate = IIf(objPOH.IsHavingGyro, CType(objPOH.DailyPayments(0), DailyPayment).EffectiveDate, objPOH.EffectiveDate)
            'End	:Optimize EffectiveDate calculation;By:DoniN;20100331
            If EffectiveDate >= ReportDate And EffectiveDate <= ReqDelDate Then
                TotalLiquefied += objPOH.TotalPODetail()
            End If
            'End Total TotalLiquefied
            'Total TotalAcceleratedGyro
            If objPOH.DailyPayments.Count > 0 Then
                For Each objDP As DailyPayment In objPOH.DailyPayments
                    If objDP.AcceleratedGyro = 1 Then
                        If (objDP.AcceleratedDate >= Format(ReportDate, "MM/dd/yyyy") And objDP.AcceleratedDate <= Format(ReqDelDate, "MM/dd/yyyy")) Then
                            TotalAcceleratedGyro += objPOH.TotalPODetail
                        End If
                    End If
                Next
            End If
            'End Total PO 3
        Next
        arlResult.Add(TotalLiquefied)
        arlResult.Add(TotalAcceleratedGyro)
        Return arlResult
    End Function

    Private Function AddWorkingDay(ByVal StateDate As Date, ByVal nAdded As Integer, Optional ByVal IsBackWard As Boolean = False) As Date
        Dim objNHFac As NationalHolidayFacade = New NationalHolidayFacade(User)
        Dim crtNH As CriteriaComposite
        Dim rslDate As Date
        Dim IsHoliday As Boolean = True
        Dim arlNH As ArrayList = New ArrayList
        Dim i As Integer = 0

        rslDate = StateDate
        For i = 1 To Math.Abs(nAdded)
            rslDate = rslDate.AddDays(IIf(IsBackWard, -1, 1))
            IsHoliday = True
            While IsHoliday = True
                crtNH = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, rslDate))
                arlNH = objNHFac.Retrieve(crtNH)
                If arlNH.Count < 1 Then
                    IsHoliday = False
                Else
                    rslDate = rslDate.AddDays(IIf(IsBackWard = False, 1, -1))
                End If
            End While
        Next
        'rslDate = StateDate.AddDays(nAdded)
        'While IsHoliday = True
        '    crtNH = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, rslDate))
        '    arlNH = objNHFac.Retrieve(crtNH)
        '    If arlNH.Count < 1 Then
        '        IsHoliday = False
        '    Else
        '        rslDate = rslDate.AddDays(IIf(IsBackWard = False, 1, -1))
        '    End If
        'End While
        Return rslDate
    End Function

    Private Function IsHavingGyro(ByRef objPOH As POHeader) As Boolean
        Dim Rsl As Boolean = True

        If objPOH.DailyPayments.Count < 1 Then
            Rsl = False
        Else
            For Each objDP As DailyPayment In objPOH.DailyPayments
                If (objDP.PaymentPurpose.ID = 3 Or objDP.PaymentPurpose.ID = 6) And (objDP.RejectStatus = 0 And objDP.IsCleared = 0 And objDP.IsReversed = 0 And objDP.Status = CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)) Then
                    Return True
                Else
                    Rsl = False
                End If
            Next
        End If
        Return Rsl
    End Function

#End Region

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            ActivateUserPrivilege()
            Initialization()
        End If
        Dim IsImplementFactoring As Boolean = IIf(KTB.DNet.Lib.WebConfig.GetValue("ImpelementFactoring") = 1, True, False)
         If IsImplementFactoring Then
            Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(CType(Session("DEALER"), Dealer).ID, EnumDealerTransType.DealerTransKind.Factoring)

            If Not (Not IsNothing(oTC) AndAlso oTC.Status = 1) Then
                IsImplementFactoring = False
            End If
        End If
        Me.lblFactoring.Visible = IsImplementFactoring
        Me.lblFactoringColon.Visible = IsImplementFactoring
        Me.ddlFactoring.Visible = IsImplementFactoring
        Dim ColIdx As Integer = CommonFunction.GetColumnIndexOfDTG(Me.dtgMain, "Factoring")
        If ColIdx >= 0 Then Me.dtgMain.Columns(ColIdx).Visible = IsImplementFactoring
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindDTG(0)
    End Sub

    Private Sub dtgMain_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMain.SortCommand
        CreateSQLOrder(e.SortExpression)
    End Sub

    Private Sub dtgMain_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMain.PageIndexChanged
        BindDTG(e.NewPageIndex)
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objPOH As POHeader = CType(CType(sHelper.GetSession("arlPOHeader"), ArrayList)(e.Item.ItemIndex), POHeader)
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim imgStatus As Object
            Dim lblReqDeliveryDate As Label = e.Item.FindControl("lblReqDeliveryDate")
            Dim lblPaymentType As Label = e.Item.FindControl("lblPaymentType")
            Dim lblAllocationDate As Label = e.Item.FindControl("lblAllocationDate")
            Dim lblChangedDate As Label = e.Item.FindControl("lblChangedDate")
            Dim lblChangedBy As Label = e.Item.FindControl("lblChangedBy")
            Dim lblTotal As Label = e.Item.FindControl("lblTotal")
            Dim chkRemark As CheckBox = e.Item.FindControl("chkRemark")
            Dim txtRemark As TextBox = e.Item.FindControl("txtRemark")
            Dim ddlRemarkStatus As DropDownList = e.Item.FindControl("ddlRemarkStatus")
            Dim lblFactoring As Label = e.Item.FindControl("lblFactoring")

            BindDdlRemark(ddlRemarkStatus)

            imgStatus = CType(e.Item.FindControl("imgStatus"), Object)
            lblNo.Text = dtgMain.PageSize * dtgMain.CurrentPageIndex + (e.Item.ItemIndex + 1)

            If objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.Blocked Then
                CType(imgStatus, System.Web.UI.WebControls.Image).ImageUrl = "../Images/red.gif"
            ElseIf objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed Then
                CType(imgStatus, System.Web.UI.WebControls.Image).ImageUrl = "../Images/yellow.gif"
            ElseIf objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed Then
                CType(imgStatus, System.Web.UI.WebControls.Image).ImageUrl = "../Images/green.gif"
                'If objPOH.Remark.Trim.ToUpper = "TAHAN DO" Then
                '    e.Item.BackColor = Color.Yellow
                '    txtRegPONumber.Enabled = False
                'End If
                'chkRemark.Enabled = False
            End If
            lblReqDeliveryDate.Text = Format(objPOH.ReqAllocationDateTime, "dd/MM/yyyy")
            lblPaymentType.Text = enumPaymentType.GetStringValue(objPOH.TermOfPayment.PaymentType)
            If objPOH.ChangedBy = "" Then
                lblAllocationDate.Text = ""
                lblChangedBy.Text = ""
                lblChangedDate.Text = ""
            Else
                lblAllocationDate.Text = Format(objPOH.LastReqAllocationDateTime, "dd/MM/yyyy")
                lblChangedBy.Text = objPOH.ChangedBy
                lblChangedDate.Text = Format(objPOH.ChangedTime, "dd/MM/yyyy hh:mm:ss")
            End If
            lblTotal.Text = FormatNumber(objPOH.TotalHarga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            ddlRemarkStatus.SelectedValue = IIf(objPOH.RemarkStatus > 0, objPOH.RemarkStatus, -1)
            txtRemark.Text = objPOH.Remark
            If ddlRemarkStatus.SelectedValue = enumPORemarkStatus.PORemarkStatus.TahanDO Then
                ddlRemarkStatus.Enabled = False
                txtRemark.Enabled = False
                e.Item.BackColor = txtColorGreen.BackColor '  Color.Green
            End If
            'If objPOH.Remark.ToUpper.Trim = "TAHAN DO" Then
            '    chkRemark.Checked = True
            '    txtRemark.Enabled = False
            'Else
            '    chkRemark.Checked = False
            '    txtRemark.Enabled = True
            'End If
            If objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed And objPOH.RemarkStatus <> enumPORemarkStatus.PORemarkStatus.TahanDO Then
                e.Item.Visible = False
            End If
            lblFactoring.Text = IIf(objPOH.IsFactoring = 1, "Ya", "Tidak")
            chkRemark.Attributes.Add("OnClick", "ToggleRemark('dtgMain__ctl" & (e.Item.ItemIndex + 3) & "_chkRemark','dtgMain__ctl" & (e.Item.ItemIndex + 3) & "_txtRemark');")
        End If
    End Sub

    Private Sub btnPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPass.Click
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim objPOH As POHeader
        Dim nUpdated As Integer = 0

        If Not IsAnyRedStatusInCheckedItem() Then
            MessageBox.Show("Pass Ceiling hanya untuk PO yang diblok (Merah)")
            Exit Sub
        End If

        For Each di As DataGridItem In dtgMain.Items
            Dim chkItem As CheckBox = di.FindControl("chkItem")
            Dim chkRemark As CheckBox = di.FindControl("chkRemark")
            Dim txtRemark As TextBox = CType(di.FindControl("txtRemark"), TextBox)

            objPOH = CType(CType(sHelper.GetSession("arlPOHeader"), ArrayList)(di.ItemIndex), POHeader)

            If chkItem.Checked Then
                If objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.Blocked Then
                    objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed
                End If
                Dim ddlRemarkStatus As DropDownList = di.FindControl("ddlRemarkStatus")
                If ddlRemarkStatus.SelectedValue <> -1 Then
                    objPOH.RemarkStatus = ddlRemarkStatus.SelectedValue
                End If
                objPOH.Remark = txtRemark.Text
                '
                'If chkRemark.Checked Then
                '    objPOH.Remark = "Tahan DO"
                'Else
                '    objPOH.Remark = txtRemark.Text
                'End If

                objPOHFac.Update(objPOH)
                nUpdated += 1
            End If
        Next
        If nUpdated > 0 Then BindDTG(dtgMain.CurrentPageIndex)
    End Sub

    Private Sub btnKonfirmasi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKonfirmasi.Click
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim objPOH As POHeader
        Dim nUpdated As Integer = 0
        Dim TempDate As Date = Now


        If Not IsAnyYellowStatusInCheckedItem() Then
            MessageBox.Show("Konfirmasi hanya untuk PO yang sudah di 'Pass Ceiling'(Kuning)")
            Exit Sub
        End If

        For Each di As DataGridItem In dtgMain.Items
            Dim chkItem As CheckBox = di.FindControl("chkItem")
            Dim chkRemark As CheckBox = di.FindControl("chkRemark")
            Dim txtRemark As TextBox = CType(di.FindControl("txtRemark"), TextBox)
            objPOH = CType(CType(sHelper.GetSession("arlPOHeader"), ArrayList)(di.ItemIndex), POHeader)

            If chkItem.Checked Then
                If objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed Then
                    objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed
                    If Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") < Format(Now, "yyyy.MM.dd") And objPOH.Status = enumStatusPO.Status.Baru Then
                        TempDate = objPOH.ReqAllocationDateTime
                        objPOH.LastReqAllocationDateTime = TempDate
                        objPOH.ReqAllocationDateTime = Format(Now, "yyyy.MM.dd hh:mm:ss")
                        objPOH.ChangedBy = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo).UserName
                        objPOH.ChangedTime = Format(Now, "yyyy.MM.dd hh:mm:ss")
                    End If
                End If

                Dim ddlRemarkStatus As DropDownList = di.FindControl("ddlRemarkStatus")
                If ddlRemarkStatus.SelectedValue <> -1 Then
                    objPOH.RemarkStatus = ddlRemarkStatus.SelectedValue
                End If
                objPOH.Remark = txtRemark.Text

                'If chkRemark.Checked Then
                '    objPOH.Remark = "Tahan DO"
                'Else
                '    objPOH.Remark = txtRemark.Text
                'End If
                objPOHFac.Update(objPOH)
                nUpdated += 1
            End If
        Next
        If nUpdated > 0 Then BindDTG(dtgMain.CurrentPageIndex)
    End Sub

    Private Sub btnRelease_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim objPOH As POHeader
        Dim nUpdated As Integer = 0


        If Not IsAnyHoldDOStatusInCheckedItem() Then
            MessageBox.Show("Tidak ada data dengan 'Tahan DO'")
            Exit Sub
        End If
        For Each di As DataGridItem In dtgMain.Items
            Dim chkItem As CheckBox = di.FindControl("chkItem")
            Dim chkRemark As CheckBox = di.FindControl("chkRemark")
            Dim txtRemark As TextBox = CType(di.FindControl("txtRemark"), TextBox)
            objPOH = CType(CType(sHelper.GetSession("arlPOHeader"), ArrayList)(di.ItemIndex), POHeader)

            If chkItem.Checked Then
                'If objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.Blocked Or objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed Then
                '    objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed
                'End If
                'If chkRemark.Checked Then
                '    objPOH.Remark = "Tahan DO"
                'Else
                '    objPOH.Remark = txtRemark.Text
                'End If
                'objPOH.Remark = ""
                'Dim ddlRemarkStatus As DropDownList = di.FindControl("ddlRemarkStatus")
                'If ddlRemarkStatus.SelectedValue <> -1 Then
                '    objPOH.RemarkStatus = -1 ddlRemarkStatus.SelectedValue
                'End If
                objPOH.RemarkStatus = -1
                objPOH.Remark = txtRemark.Text

                objPOHFac.Update(objPOH)
                nUpdated += 1
            End If
        Next
        If nUpdated > 0 Then BindDTG(dtgMain.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Dim objPOH As POHeader
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim nUpdated As Integer = 0

        For Each di As DataGridItem In dtgMain.Items
            Dim chkItem As CheckBox = di.FindControl("chkItem")
            Dim chkRemark As CheckBox = di.FindControl("chkRemark")
            Dim txtRemark As TextBox = CType(di.FindControl("txtRemark"), TextBox)
            objPOH = CType(CType(sHelper.GetSession("arlPOHeader"), ArrayList)(di.ItemIndex), POHeader)

            If chkItem.Checked And objPOH.BlockedStatus <> enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed Then
                objPOH.Remark = "Batal"
                objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.Blocked
                objPOHFac.Update(objPOH)
                nUpdated += 1
            End If
        Next
        If nUpdated > 0 Then BindDTG(dtgMain.CurrentPageIndex)
    End Sub

    Private Sub btnSimpanGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpanGrid.Click
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim nUpdated As Integer = 0
        Dim nChecked As Integer = 0

        Try
            For Each di As DataGridItem In dtgMain.Items
                Dim chkItem As CheckBox = di.FindControl("chkItem")
                If chkitem.Checked Then nChecked += 1
            Next
            For Each di As DataGridItem In dtgMain.Items
                Dim txtRemark As TextBox = di.FindControl("txtRemark")
                Dim ddlRemarkStatus As DropDownList = di.FindControl("ddlRemarkStatus")
                Dim objPOH As POHeader = CType(CType(sHelper.GetSession("arlPOHeader"), ArrayList)(di.ItemIndex), POHeader)
                Dim chkItem As CheckBox = di.FindControl("chkItem")
                If chkItem.Checked Then
                    If ddlRemarkStatus.SelectedValue > -1 Then
                        objPOH.RemarkStatus = ddlRemarkStatus.SelectedValue
                    End If
                    objPOH.Remark = txtRemark.Text
                    objPOHFac.Update(objPOH)
                    nUpdated += 1
                End If
            Next


        Catch ex As Exception
            If nUpdated = 0 Then
                MessageBox.Show("Data gagal tersimpan")
                Exit Sub
            Else
                If nUpdated < nChecked Then
                    MessageBox.Show("beberapa data gagal tersimpan")
                    Exit Sub
                End If
            End If
        End Try

        If nChecked <> 0 And nUpdated = nChecked Then
            MessageBox.Show("Data berhasil disimpan")
        End If

    End Sub

#End Region
End Class
