#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessValidation.Helpers
#End Region

Public Class FrmReceipt
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoClaim As System.Web.UI.WebControls.Label


    Protected WithEvents lblTanggalFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents lblKet As System.Web.UI.WebControls.Label

    Protected WithEvents lblJumlah As System.Web.UI.WebControls.Label
    Protected WithEvents icTglKuitansi As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglFaktur As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtAmount As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAmountReduksi As System.Web.UI.WebControls.TextBox

    Protected WithEvents txtNoKuitansi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

    Protected WithEvents hfIdHeader As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents btnCalculate As System.Web.UI.WebControls.Button

    'Protected WithEvents txtNomorFaktur As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNomorFaktur1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNomorFaktur2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNomorFaktur3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNomorFaktur4 As System.Web.UI.WebControls.TextBox

    Protected WithEvents lblNomorFaktur1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorFaktur2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorFaktur3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorFakturOld As System.Web.UI.WebControls.Label

    Protected WithEvents btnCetak As System.Web.UI.WebControls.Button
    Protected WithEvents lblNoKuitansi As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents lblAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglKuitansi As System.Web.UI.WebControls.Label

    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button


    Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox

    Protected WithEvents panelInputan As System.Web.UI.WebControls.Panel
    Protected WithEvents panelCetak As System.Web.UI.WebControls.Panel

    Protected WithEvents lblTerbilang As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label

    Protected WithEvents lblCodeDealer1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer1 As System.Web.UI.WebControls.Label

    Protected WithEvents lblNamaDealer2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblKet1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerBankAccount As System.Web.UI.WebControls.Label
    Protected WithEvents btnKembali1 As System.Web.UI.WebControls.Button
    Protected WithEvents lblRemarks As System.Web.UI.WebControls.Label

    Protected WithEvents hfPpn As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hfPph As System.Web.UI.WebControls.HiddenField


    Protected WithEvents lblNoClaimPrint As System.Web.UI.WebControls.Label
    Protected WithEvents hfNoClaimPrint As System.Web.UI.WebControls.HiddenField

    Protected WithEvents ddlDealerBankAccount As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private sessHelper As New SessionHelper
    Dim arlBCDNew As New ArrayList
    Dim arrBenefitClaimDeductedHistory As New ArrayList

    Private objBenefitClaimReceipt As New BenefitClaimReceipt
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

#Region "Private Property"
    Private Property SesType() As EnumAlertManagement.AlertManagementType
        Get
            Return CType(sessHelper.GetSession("ListAlertMasterType"), EnumAlertManagement.AlertManagementType)
        End Get
        Set(ByVal Value As EnumAlertManagement.AlertManagementType)
            sessHelper.SetSession("ListAlertMasterType", Value)
        End Set
    End Property
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ALERT MANAGEMENT - Alert Managemen List")
        End If
    End Sub

    Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege)
#End Region

    Private Sub RetrieveDealer()
        Dim objSessionHelper As New SessionHelper
        Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then

            If Not objDealer.DealerGroup Is Nothing Then 'untuk dealer side
                lblCodeDealer.Text = objDealer.DealerCode
                lblNamaDealer.Text = objDealer.DealerName
            Else
                btnSimpan.Visible = False
            End If

        Else

        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'InitiateAuthorization()
        If Not IsPostBack Then

            BindDataGrid()

            RetrieveDealer()

            icTglKuitansi.Value = Now.Date
            If Not Request.QueryString("justview") Is Nothing Then
                btnSimpan.Visible = False
                btnCalculate.Visible = False
            End If
        End If
    End Sub



    Private Sub BindDataGrid()
        Dim id As String = Request.QueryString("id") 'id claimheader

        'Dim idchassis As String = Request.QueryString("idchassis")
        'Dim norecom As String = Request.QueryString("norecom")
        Dim objBenefitClaimHeader As BenefitClaimHeader = New BenefitClaimHeaderFacade(User).Retrieve(CInt(id))
        If Not id Is Nothing Then

            hfIdHeader.Value = id

            If Not objBenefitClaimHeader Is Nothing Then
                Dim idDealer As String = ""
                Dim namaDealer As String = ""
                Dim dealerID As Integer = 0
                'For Each el As BenefitMasterDealer In objBenefitClaimHeader.BenefitMasterDetail.BenefitMasterHeader.BenefitMasterDealers
                '    idDealer += el.Dealer.DealerCode
                '    namaDealer += el.Dealer.DealerName
                'Next
                idDealer = objBenefitClaimHeader.Dealer.DealerCode
                namaDealer = objBenefitClaimHeader.Dealer.DealerName
                dealerID = objBenefitClaimHeader.Dealer.ID

                lblCity.Text = objBenefitClaimHeader.Dealer.City.CityName

                Dim cutOffDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                For Each el As BenefitClaimReceipt In objBenefitClaimHeader.BenefitClaimReceipts
                    txtName.Text = el.Name
                    txtTitle.Text = el.Title
                    icTglFaktur.Value = el.FakturPajakDate
                    cutOffDate = el.FakturPajakDate
                Next

                If cutOffDate.Year < 1900 Then
                    cutOffDate = objBenefitClaimHeader.ClaimDate
                End If

                lblCodeDealer.Text = idDealer

                lblNamaDealer.Text = namaDealer

                lblNoClaim.Text = objBenefitClaimHeader.ClaimRegNo
                Dim temp1 As String = ""
                Dim temp2 As String = ""
                Dim objFakturdate As DateTime
                Dim tempAmount As Decimal = 0, tempamount1 As Decimal = 0
                Dim CountUnit As Integer = 0
                For Each el As BenefitClaimDetails In objBenefitClaimHeader.BenefitClaimDetailss
                    If Not el.ChassisMaster Is Nothing And Not el.ChassisMaster.EndCustomer Is Nothing Then
                        '  temp1 += el.ChassisMaster.EndCustomer.FakturNumber
                        ' temp2 += el.ChassisMaster.EndCustomer.FakturDate.ToString("dd/MM/yyyy")
                        temp1 = el.ChassisMaster.EndCustomer.FakturNumber
                        temp2 = el.ChassisMaster.EndCustomer.FakturDate.ToString("dd/MM/yyyy")
                        objFakturdate = el.ChassisMaster.EndCustomer.FakturDate
                    End If
                    tempAmount = tempAmount + el.BenefitMasterDetail.Amount
                    CountUnit = CountUnit + 1
                Next
                tempamount1 = tempAmount
                ViewState("TotalAmount") = tempAmount
                Dim total As Decimal = tempamount1
                If Not tempAmount = 0 Then
                    'tempAmount = tempAmount + (0.14999999999999999 * tempamount1)
                    'If objBenefitClaimHeader.BenefitType.Name.ToLower.Contains("leasin") = True Then
                    '    tempAmount = tempAmount + (0.14999999999999999 * tempamount1)
                    'Else
                    '    tempAmount = tempAmount + (0.02 * tempamount1)
                    'End If

                    Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(cutOffDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
                    Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(cutOffDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

                    Dim nilaiPph As Decimal = 0
                    If objBenefitClaimHeader.BenefitType.LeasingBox = 1 Then
                        'nilaiPph = Math.Round(((tempamount1 / (1 - 0.15)) - tempamount1))
                        nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pph, total:=tempamount1)
                        hfPph.Value = nilaiPph.ToString
                        total = total + nilaiPph
                        'hfPpn.Value = Math.Round((0.1 * (nilaiPph + tempamount1))).ToString("#,##0.00")
                        hfPpn.Value = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=total)
                        'total = Math.Round(total + (0.1 * (nilaiPph + tempamount1)))
                        total += CDec(hfPpn.Value)
                        ' ElseIf objDomain.BenefitType.Name.ToLower.Contains("cashbac") = True Then
                    Else
                        'nilaiPph = Math.Round(((tempamount1 / (1 - 0.15)) - tempamount1))
                        'nilaiPph = Math.Round(0.15 * tempamount1)
                        nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=tempamount1)
                        hfPph.Value = nilaiPph.ToString()
                        hfPpn.Value = 0 'Math.Round(0.1 * tempamount1).ToString("#,##0.00")
                        total = Math.Round(total) 'Math.Round(total + (0.1 * tempamount1))
                    End If
                End If

                txtAmount.Text = total.ToString("#,##0.00")
                Dim dblAmountDeducted As Double = GetAmountByDeducted(objBenefitClaimHeader)
                If dblAmountDeducted > 0 Then
                    txtAmountReduksi.Text = dblAmountDeducted.ToString("#,##0.00")
                Else
                    txtAmountReduksi.Text = total.ToString("#,##0.00")
                End If

                hfNoClaimPrint.Value = objBenefitClaimHeader.ClaimRegNo


                lblKet.Text = "Untuk pembayaran sales campaign " & objBenefitClaimHeader.BenefitType.Name & _
                    " sebanyak " & CountUnit & " unit."

                'lblNomorFaktur.Text = temp1
                lblTanggalFaktur.Text = temp2
                'icTglFaktur.Value = objFakturdate
                FillDDLDealerBankAccount(dealerID)

                If objBenefitClaimHeader.Status = 0 Then  'Status Baru
                    btnCetak.Visible = False
                Else
                    btnCetak.Visible = True
                End If
            End If
        End If

        Dim objBenefitClaimReceipt1 As BenefitClaimReceipt = New BenefitClaimReceiptFacade(User).RetrieveByClaimHeader(id)
        If Not IsNothing(objBenefitClaimReceipt1) AndAlso objBenefitClaimReceipt1.ID > 0 Then
            txtNoKuitansi.Text = objBenefitClaimReceipt1.ReceiptNo
            'txtNomorFaktur.Text = objBenefitClaimReceipt1.FakturPajakNo
            Dim delimiter() As Char = {".", "-"}
            If objBenefitClaimReceipt1.FakturPajakNo.Contains(".") And objBenefitClaimReceipt1.FakturPajakNo.Contains("-") Then
                Dim noFaktur() As String = objBenefitClaimReceipt1.FakturPajakNo.Split(delimiter)
                If noFaktur(0).Count > 0 And noFaktur(1).Count > 0 And noFaktur(2).Count > 0 And noFaktur(3).Count > 0 Then
                    txtNomorFaktur1.Text = noFaktur(0)
                    txtNomorFaktur2.Text = noFaktur(1)
                    txtNomorFaktur3.Text = noFaktur(2)
                    txtNomorFaktur4.Text = noFaktur(3)
                End If
            Else
                If Not objBenefitClaimHeader.Status = 0 Or Not objBenefitClaimHeader.Status = 1 Then
                    lblNomorFakturOld.Text = objBenefitClaimReceipt1.FakturPajakNo
                    txtNomorFaktur1.Visible = False
                    txtNomorFaktur2.Visible = False
                    txtNomorFaktur3.Visible = False
                    txtNomorFaktur4.Visible = False
                    lblNomorFaktur1.Visible = False
                    lblNomorFaktur2.Visible = False
                    lblNomorFaktur3.Visible = False
                    lblNomorFakturOld.Visible = True
                Else
                    txtNomorFaktur1.Visible = True
                    txtNomorFaktur2.Visible = True
                    txtNomorFaktur3.Visible = True
                    txtNomorFaktur4.Visible = True
                    lblNomorFaktur1.Visible = True
                    lblNomorFaktur2.Visible = True
                    lblNomorFaktur3.Visible = True
                    lblNomorFakturOld.Visible = False
                End If
            End If
            ' txtAmount.Text = objBenefitClaimReceipt1.ReceiptAmount.ToString("#,##0.00")
            icTglKuitansi.Value = objBenefitClaimReceipt1.ReceiptDate
            txtName.Text = objBenefitClaimReceipt1.Name
            txtTitle.Text = objBenefitClaimReceipt1.Title
            If Not IsNothing(objBenefitClaimReceipt1.DealerBankAccount) Then
                ddlDealerBankAccount.SelectedValue = objBenefitClaimReceipt1.DealerBankAccount.ID
            End If
        End If

    End Sub

    Private Sub FillDDLDealerBankAccount(ByVal dealerID As Integer)
        ddlDealerBankAccount.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DealerBankAccount), "Dealer.ID", MatchType.Exact, dealerID))
        If Request.QueryString("justview") Is Nothing Then
            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "Status", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        End If
        Dim list As ArrayList = New DealerBankAccountFacade(User).Retrieve(criterias)
        For Each i As DealerBankAccount In list
            ddlDealerBankAccount.Items.Add(New ListItem(i.BankName + " / " + i.BankAccount, i.ID))
        Next
    End Sub

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        'Dim noFaktur As String = txtNomorFaktur1.Text.Trim & "." & txtNomorFaktur2.Text.Trim & "-" & txtNomorFaktur3.Text.Trim & "." & txtNomorFaktur4.Text.Trim
        'If Not noFaktur.Length = 18 Then
        '    sb.Append("No. Faktur Pajak Belum Sesuai\n")
        'End If

        'If txtNoKuitansi.Text.Trim = "" Then
        '    sb.Append("Nomor Kuitansi harus Diisi\n")
        'End If
        'If txtNomorFaktur1.Text.Trim = "" Or txtNomorFaktur2.Text = "" Or txtNomorFaktur3.Text.Trim = "" Or txtNomorFaktur4.Text.Trim = "" Then
        '    sb.Append("Nomor Faktur Pajak harus Diisi\n")
        'End If
        'If txtName.Text.Trim = "" Then
        '    sb.Append("Nama Tanda Tangan harus Diisi\n")
        'End If
        'If txtTitle.Text.Trim = "" Then
        '    sb.Append("Jabatan Tanda Tangan harus Diisi\n")
        'End If

        Return sb.ToString()
    End Function

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If

        Dim noFaktur As String = "00.000-00.00000000" 'txtNomorFaktur1.Text.Trim & "." & txtNomorFaktur2.Text.Trim & "-" & txtNomorFaktur3.Text.Trim & "." & txtNomorFaktur4.Text.Trim
        If Not hfIdHeader.Value = "" Then
            If Not noFaktur = "" And Not txtNoKuitansi.Text = "" Then
                CalculationPPNPPH()

                Dim objHeader As BenefitClaimHeader = New BenefitClaimHeaderFacade(User).Retrieve(CInt(hfIdHeader.Value))
                Dim objBenefitClaimReceipt1 As BenefitClaimReceipt = New BenefitClaimReceiptFacade(User).RetrieveByClaimHeader(hfIdHeader.Value)
                Dim objDealerBankAccount As DealerBankAccount = New DealerBankAccountFacade(User).Retrieve(CInt(ddlDealerBankAccount.SelectedValue))

                objBenefitClaimReceipt1.BenefitClaimHeader = objHeader
                objBenefitClaimReceipt1.ReceiptNo = txtNoKuitansi.Text
                'objBenefitClaimReceipt1.FakturPajakNo = txtNomorFaktur.Text
                objBenefitClaimReceipt1.FakturPajakNo = noFaktur
                objBenefitClaimReceipt1.ReceiptDate = icTglKuitansi.Value
                objBenefitClaimReceipt1.ReceiptAmount = CDec(txtAmount.Text.Replace(".", ""))
                objBenefitClaimReceipt1.ReceiptAmountDeducted = CDec(txtAmountReduksi.Text.Replace(".", ""))
                objBenefitClaimReceipt1.PPHTotal = CDec(hfPph.Value)
                objBenefitClaimReceipt1.FakturPajakDate = icTglFaktur.Value
                'objBenefitClaimReceipt1.VATTotal = CDec(0.1 * (objBenefitClaimReceipt1.ReceiptAmount + objBenefitClaimReceipt1.PPHTotal))
                objBenefitClaimReceipt1.VATTotal = CDec(hfPpn.Value)

                objBenefitClaimReceipt1.Name = txtName.Text
                objBenefitClaimReceipt1.Title = txtTitle.Text
                objBenefitClaimReceipt1.DealerBankAccount = objDealerBankAccount
                'objBenefitClaimReceipt1.DealerBankAccountID = CInt(ddlDealerBankAccount.SelectedValue)
                Dim n As Integer = -1
                If Not objBenefitClaimReceipt1.ID = Nothing Then
                    n = New BenefitClaimReceiptFacade(User).UpdateReceipt(objBenefitClaimReceipt1)
                Else
                    GetArraylistDeducted(objHeader)
                    n = New BenefitClaimReceiptFacade(User).InsertReceipt(objBenefitClaimReceipt1, arrBenefitClaimDeductedHistory, arlBCDNew)
                End If

                If n = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show("Simpan Berhasil")
                    '   Response.Redirect("FrmBenefitClaimList.aspx")

                End If
            Else
                If noFaktur = "" Then
                    MessageBox.Show("Isi Faktur Pajak")
                ElseIf txtNoKuitansi.Text = "" Then
                    MessageBox.Show("Isi Nomor Kuitansi")
                Else
                    MessageBox.Show("Isi Faktur Pajak dan Nomor Kuitansi")
                End If
            End If


        End If

    End Sub

    Private Sub GetArraylistDeducted(ByVal objHeader As BenefitClaimHeader)
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
        Dim objAppConfig As AppConfig = objAppConfigFacade.Retrieve("SalesCampaignDeductedBenefitType")
        If Not IsNothing(objAppConfig) Then
            Dim strVal() As String = objAppConfig.Value.ToString().Split(";")
            For Each strCode As String In strVal
                If strCode = objHeader.BenefitType.ID Then
                    arrBenefitClaimDeductedHistory = New ArrayList

                    Dim objBenefitClaimDeductedFacade As New BenefitClaimDeductedFacade(User)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDeducted), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(BenefitClaimDeducted), "BenefitClaimHeader.Dealer.ID", MatchType.Exact, objDealer.ID))
                    Dim sortColl As SortCollection = New SortCollection
                    sortColl.Add(New Sort(GetType(BenefitClaimDeducted), "CreatedTime", CType(Sort.SortDirection.ASC, Sort.SortDirection)))

                    arlBCDNew = New ArrayList
                    Dim objBenefitClaimDeductedHistory As New BenefitClaimDeductedHistory
                    Dim arlBCD As ArrayList = objBenefitClaimDeductedFacade.Retrieve(criterias, sortColl)
                    If Not IsNothing(arlBCD) AndAlso arlBCD.Count > 0 Then
                        Dim dblUsageBefore As Double = 0
                        Dim dblUsage As Double = IIf(txtAmount.Text.Trim = "", 0, txtAmount.Text)
                        For Each obj As BenefitClaimDeducted In arlBCD
                            If obj.RemainAmount > 0 Then
                                If dblUsage > obj.RemainAmount Then
                                    dblUsageBefore = obj.RemainAmount
                                    dblUsage = dblUsage - obj.RemainAmount
                                    obj.RemainAmount = 0
                                Else
                                    dblUsageBefore = dblUsage
                                    obj.RemainAmount = obj.RemainAmount - dblUsage
                                    dblUsage = 0
                                End If

                                objBenefitClaimDeductedHistory = New BenefitClaimDeductedHistory
                                objBenefitClaimDeductedHistory.BenefitClaimDeducted = obj
                                objBenefitClaimDeductedHistory.BenefitClaimHeader = objHeader
                                objBenefitClaimDeductedHistory.AmountDeducted = dblUsageBefore
                                arrBenefitClaimDeductedHistory.Add(objBenefitClaimDeductedHistory)

                                arlBCDNew.Add(obj)
                                If dblUsage = 0 Then Exit For
                            End If
                        Next
                    End If

                    Exit For
                End If
            Next
        End If
    End Sub

    Private Function GetAmountByDeducted(ByVal objHeader As BenefitClaimHeader) As Decimal
        Dim decAmountByDeducted As Decimal = txtAmount.Text
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
        Dim objAppConfig As AppConfig = objAppConfigFacade.Retrieve("SalesCampaignDeductedBenefitType")
        If Not IsNothing(objAppConfig) Then
            Dim strVal() As String = objAppConfig.Value.ToString().Split(";")
            For Each strCode As String In strVal
                If strCode = objHeader.BenefitType.ID Then
                    Dim objBenefitClaimDeductedFacade As New BenefitClaimDeductedFacade(User)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDeducted), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(BenefitClaimDeducted), "BenefitClaimHeader.Dealer.ID", MatchType.Exact, objDealer.ID))
                    Dim sortColl As SortCollection = New SortCollection
                    sortColl.Add(New Sort(GetType(BenefitClaimDeducted), "CreatedTime", CType(Sort.SortDirection.ASC, Sort.SortDirection)))

                    Dim dblTotAmountDeducted As Double = 0
                    Dim dblUsage As Double = 0
                    Dim objBenefitClaimDeductedHistory As New BenefitClaimDeductedHistory
                    Dim arlBCD As ArrayList = objBenefitClaimDeductedFacade.Retrieve(criterias, sortColl)
                    If Not IsNothing(arlBCD) AndAlso arlBCD.Count > 0 Then
                        For Each objDeduct As BenefitClaimDeducted In arlBCD
                            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDeductedHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crit.opAnd(New Criteria(GetType(BenefitClaimDeductedHistory), "BenefitClaimHeader.ID", MatchType.Exact, objHeader.ID))
                            crit.opAnd(New Criteria(GetType(BenefitClaimDeductedHistory), "BenefitClaimDeducted.ID", MatchType.Exact, objDeduct.ID))
                            Dim aggregateSum As Aggregate = New Aggregate(GetType(BenefitClaimDeductedHistory), "AmountDeducted", AggregateType.Sum)
                            Dim decAmountDeducted As Decimal = IsDBNull(New BenefitClaimDeductedHistoryFacade(User).RetrieveScalar(crit, aggregateSum), 0)

                            dblTotAmountDeducted += decAmountDeducted

                            If objDeduct.RemainAmount > 0 Then
                                dblUsage += objDeduct.RemainAmount
                            End If
                        Next
                    End If

                    dblUsage += dblTotAmountDeducted
                    If dblUsage > decAmountByDeducted Then
                        decAmountByDeducted = 0
                    Else
                        decAmountByDeducted -= dblUsage
                    End If

                    Exit For
                End If
            Next
        End If

        Return decAmountByDeducted
    End Function

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        If (btnSimpan.Visible = True) Or (Not Request.QueryString("justview") Is Nothing And btnSimpan.Visible = False) Then
            If Not IsNothing(Request.QueryString("redirectFrom")) Then
                Response.Redirect(Request.QueryString("redirectFrom").ToString)
            Else
                Response.Redirect("FrmBenefitClaimList.aspx")
            End If
        Else
            txtNoKuitansi.Visible = True
            'txtNomorFaktur.Visible = txtNoKuitansi.Visible
            txtNomorFaktur1.Visible = txtNoKuitansi.Visible
            lblNomorFaktur1.Visible = txtNoKuitansi.Visible
            txtNomorFaktur2.Visible = txtNoKuitansi.Visible
            lblNomorFaktur2.Visible = txtNoKuitansi.Visible
            txtNomorFaktur3.Visible = txtNoKuitansi.Visible
            lblNomorFaktur3.Visible = txtNoKuitansi.Visible
            txtNomorFaktur4.Visible = txtNoKuitansi.Visible
            txtAmount.Visible = txtNoKuitansi.Visible
            txtAmountReduksi.Visible = txtNoKuitansi.Visible
            icTglKuitansi.Visible = txtNoKuitansi.Visible
            btnSimpan.Visible = txtNoKuitansi.Visible
            btnCetak.Visible = txtNoKuitansi.Visible
            lblNoKuitansi.Visible = Not txtNoKuitansi.Visible
            lblNomorFaktur.Visible = lblNoKuitansi.Visible
            lblAmount.Visible = lblNoKuitansi.Visible
            lblTglKuitansi.Visible = lblNoKuitansi.Visible
            btnPrint.Visible = lblNoKuitansi.Visible

            lblNoClaimPrint.Visible = lblNoKuitansi.Visible
        End If



    End Sub

    Private Sub btnKembali1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali1.Click
        panelCetak.Visible = False
        panelInputan.Visible = Not panelCetak.Visible



    End Sub

    Private Sub btnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCetak.Click
        lblNoKuitansi.Text = txtNoKuitansi.Text


        lblNoClaimPrint.Text = hfNoClaimPrint.Value

        lblAmount.Text = txtAmountReduksi.Text.ToString()
        lblTglKuitansi.Text = Format(icTglKuitansi.Value, "dd-MM-yyyy")

        Dim cf As New CommonFunction

        ' lblTerbilang.Text = New BenefitClaimReceiptFacade(User).Terbilang(CInt(lblAmount.Text.Replace(".", "")))
        'lblTerbilang.Text = New BenefitClaimReceiptFacade(User).Terbilang(CInt(lblAmount.Text.Replace(".", "")))
        lblTerbilang.Text = cf.Terbilang(CInt(lblAmount.Text.Replace(".", "")))
        'lblTerbilang.Text = lblTerbilang.Text & " Rupiah"

        lblCodeDealer1.Text = lblCodeDealer.Text
        lblNamaDealer1.Text = lblNamaDealer.Text
        lblNamaDealer2.Text = lblNamaDealer1.Text

        lblName.Text = txtName.Text
        lblTitle.Text = txtTitle.Text

        lblKet1.Text = lblKet.Text

        Dim objBenefitClaimReceipt1 As BenefitClaimReceipt = New BenefitClaimReceiptFacade(User).RetrieveByClaimHeader(hfIdHeader.Value)
        If Not IsNothing(objBenefitClaimReceipt1.DealerBankAccount) Then
            Dim sArr() As String = ddlDealerBankAccount.SelectedItem.Text.Split("/")
            lblDealerBankAccount.Text = sArr(1) + " - " + sArr(0)
        End If

        Dim objBenefitClaimDetails As New BenefitClaimDetails
        Dim objBenefitClaimHeader As BenefitClaimHeader = New BenefitClaimHeaderFacade(User).Retrieve(CInt(hfIdHeader.Value))
        If Not IsNothing(objBenefitClaimHeader) Then
            If Not IsNothing(objBenefitClaimHeader.BenefitClaimDetailss) AndAlso objBenefitClaimHeader.BenefitClaimDetailss.Count > 0 Then
                objBenefitClaimDetails = CType(objBenefitClaimHeader.BenefitClaimDetailss(0), BenefitClaimDetails)
                If Not IsNothing(objBenefitClaimDetails) Then
                    If Not IsNothing(objBenefitClaimDetails.BenefitMasterDetail) Then
                        If Not IsNothing(objBenefitClaimDetails.BenefitMasterDetail.BenefitMasterHeader) Then
                            lblRemarks.Text = objBenefitClaimDetails.BenefitMasterDetail.BenefitMasterHeader.Remarks
                        End If
                    End If
                End If
            End If
        End If

        panelCetak.Visible = True
        panelInputan.Visible = Not panelCetak.Visible
    End Sub

    Private Function IsDBNull(ByVal obj As Object, ByVal replacement As Decimal) As Decimal
        If obj Is DBNull.Value Then
            Return replacement
        End If
        Return CType(obj, Decimal)
    End Function

    Private Sub CalculationPPNPPH()
        Dim id As String = Request.QueryString("id") 'id claimheader
        Dim objBenefitClaimHeader As BenefitClaimHeader = New BenefitClaimHeaderFacade(User).Retrieve(CInt(id))

        Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(icTglFaktur.Value, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_2").ValueId)
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(icTglFaktur.Value, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        Dim nilaiPph As Decimal = 0
        Dim total As Decimal = CDec(ViewState("TotalAmount"))

        If objBenefitClaimHeader.BenefitType.LeasingBox = 1 Then
            nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pph, total:=total)
            hfPph.Value = nilaiPph.ToString
            total = total + nilaiPph
            hfPpn.Value = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=total)
            total += CDec(hfPpn.Value)
        Else
            nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=total)
            hfPph.Value = nilaiPph.ToString()
            hfPpn.Value = 0
            total = Math.Round(total)
        End If

        txtAmount.Text = total.ToString("#,##0.00")
        Dim dblAmountDeducted As Double = GetAmountByDeducted(objBenefitClaimHeader)
        If dblAmountDeducted > 0 Then
            txtAmountReduksi.Text = dblAmountDeducted.ToString("#,##0.00")
        Else
            txtAmountReduksi.Text = total.ToString("#,##0.00")
        End If
    End Sub

    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs)
        CalculationPPNPPH()
    End Sub
End Class
