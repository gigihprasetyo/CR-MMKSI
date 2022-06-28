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
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessValidation.Helpers

#End Region

Public Class FrmInputBabitReportReceipt
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"

    Dim objDealer As New Dealer
    Dim objLoginUser As UserInfo
    Dim objBabitReportReceipt As BabitReportReceipt
    Dim objBabitReportHeader As BabitReportHeader
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

    Const sessBabitReportHeader As String = "sessDataBabitReportHeader"
    Const sessBabitReportReceipt As String = "sessDataBabitReportReceipt"

    Private sesHelper As New SessionHelper
    Private intItemIndex As Integer = 0
    Dim IsDealer As Boolean = False
    Dim Mode As String = "New"
    Dim visibleSave As Boolean = True

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property
#End Region

#Region "Custom Methods"

    Private Sub LoadDataBabitReportReceipt(intBabitReportReceiptID As Integer)
        objBabitReportReceipt = New BabitReportReceiptFacade(User).Retrieve(intBabitReportReceiptID)
        If Not IsNothing(objBabitReportReceipt) Then
            sesHelper.SetSession(sessBabitReportReceipt, objBabitReportReceipt)
            sesHelper.SetSession(sessBabitReportHeader, objBabitReportReceipt.BabitReportHeader)

            objDealer = objBabitReportReceipt.BabitReportHeader.Dealer
            sesHelper.SetSession("FrmInputBabitReportReceipt.DEALER", objDealer)

            txtReceiptNo.Text = objBabitReportReceipt.ReceiptNo
            icReceiptDate.Value = objBabitReportReceipt.ReceiptDate
            'txtFakturPajakNo.Text = objBabitReportReceipt.FakturPajakNo
            Dim delimiter() As Char = {".", "-"}
            If objBabitReportReceipt.FakturPajakNo.Contains(".") And objBabitReportReceipt.FakturPajakNo.Contains("-") Then
                Dim noFaktur() As String = objBabitReportReceipt.FakturPajakNo.Split(delimiter)
                If noFaktur(0).Count > 0 And noFaktur(1).Count > 0 And noFaktur(2).Count > 0 And noFaktur(3).Count > 0 Then
                    txtNomorFaktur1.Text = noFaktur(0)
                    txtNomorFaktur2.Text = noFaktur(1)
                    txtNomorFaktur3.Text = noFaktur(2)
                    txtNomorFaktur4.Text = noFaktur(3)
                End If
            Else
                If Not objBabitReportReceipt.BabitReportHeader.BabitReportStatus = 0 Or Not objBabitReportReceipt.BabitReportHeader.BabitReportStatus = 1 Then
                    lblNomorFakturOld.Text = objBabitReportReceipt.FakturPajakNo
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
            icFakturPajakDate.Value = objBabitReportReceipt.FakturPajakDate

            If txtClaimAmount.Text.Trim = "" OrElse txtClaimAmount.Text.Trim = "0" Then
                txtClaimAmount.Text = objBabitReportReceipt.ClaimAmount.ToString("#,##0")
            End If
            txtVATTotal.Text = objBabitReportReceipt.VATTotal.ToString("#,##0")
            txtPPHTotal.Text = objBabitReportReceipt.PPHTotal.ToString("#,##0")
            txtTotalReceiptAmount.Text = (objBabitReportReceipt.TotalReceiptAmount + objBabitReportReceipt.PPHTotal).ToString("#,##0")
            hdnTotalReceiptAmount.Value = objBabitReportReceipt.TotalReceiptAmount

            lblTujuanPembayaran.Text = "Untuk Pembayaran BABIT " + objBabitReportReceipt.BabitReportHeader.BabitHeader.BabitRegNumber + " / " + objBabitReportReceipt.BabitReportHeader.BabitHeader.BabitDealerNumber
            lblNoRegBabit.Text = objBabitReportReceipt.BabitReportHeader.BabitHeader.BabitRegNumber
            txtSignName.Text = objBabitReportReceipt.SignName
            txtSignPosition.Text = objBabitReportReceipt.SignPosition

            getBankAccount()
            If Not IsNothing(objBabitReportReceipt.DealerBankAccount) Then
                ddlNoRek.SelectedValue = objBabitReportReceipt.DealerBankAccount.ID
            End If

            If objBabitReportReceipt.Status <> 0 Then
                visibleSave = False
            End If
        End If
    End Sub

    Private Sub LoadDataBabitReportHeader(intBabitReportHeaderID As Integer)
        objBabitReportHeader = New BabitReportHeaderFacade(User).Retrieve(intBabitReportHeaderID)
        If Not IsNothing(objBabitReportHeader) Then
            sesHelper.SetSession(sessBabitReportHeader, objBabitReportHeader)

            objDealer = objBabitReportHeader.Dealer
            sesHelper.SetSession("FrmInputBabitReportReceipt.DEALER", objDealer)
            getBankAccount()

            Dim dblClaimAmount As Decimal = 0
            Dim dblVATTotal As Decimal = 0
            Dim dblPPHTotal As Decimal = 0
            Dim dblTotalReceiptAmount As Decimal = 0

            Dim arlBabitDealerAllocation As New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, objBabitReportHeader.BabitHeader.ID))
            arlBabitDealerAllocation = New BabitDealerAllocationFacade(User).Retrieve(criterias)
            If Not IsNothing(arlBabitDealerAllocation) Then
                If arlBabitDealerAllocation.Count > 0 Then
                    For Each BDA As BabitDealerAllocation In arlBabitDealerAllocation
                        Dim objBabitDealerAllocation As BabitDealerAllocation = BDA
                        dblClaimAmount = dblClaimAmount + objBabitDealerAllocation.SubsidyAmount
                    Next

                    Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(icFakturPajakDate.Value, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_2").ValueId)
                    Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(icFakturPajakDate.Value, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

                    'dblVATTotal = (dblClaimAmount * 10) / 100
                    'dblPPHTotal = (dblClaimAmount * 2) / 100

                    dblVATTotal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=dblClaimAmount)
                    dblPPHTotal = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=dblClaimAmount)

                    Dim objAppConfig As New AppConfig
                    Dim criterias2 As New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, "BabitDealerBebasPPh"))
                    Dim arrConfig As ArrayList = New AppConfigFacade(User).Retrieve(criterias2)
                    If Not IsNothing(arrConfig) AndAlso arrConfig.Count > 0 Then
                        objAppConfig = CType(arrConfig(0), AppConfig)
                        Dim values() As String = objAppConfig.Value.Split(";")
                        For Each str As String In values
                            If objDealer.DealerCode = str Then
                                dblPPHTotal = 0
                                Exit For
                            End If
                        Next
                    End If
                    dblTotalReceiptAmount = dblClaimAmount + dblVATTotal - dblPPHTotal
                End If
            End If
            txtClaimAmount.Text = dblClaimAmount.ToString("#,##0")
            txtVATTotal.Text = dblVATTotal.ToString("#,##0")
            txtPPHTotal.Text = dblPPHTotal.ToString("#,##0")
            hdnTotalReceiptAmount.Value = dblTotalReceiptAmount.ToString("#,##0")
            txtTotalReceiptAmount.Text = (dblTotalReceiptAmount + dblPPHTotal).ToString("#,##0")

            lblTujuanPembayaran.Text = "Untuk Pembayaran BABIT " + objBabitReportHeader.BabitHeader.BabitRegNumber + " / " + objBabitReportHeader.BabitHeader.BabitDealerNumber
            lblNoRegBabit.Text = objBabitReportHeader.BabitHeader.BabitRegNumber
        End If
    End Sub

    Private Sub ClearAll()
        hdnBabitReportHeaderID.Value = ""

        txtReceiptNo.Text = ""
        icReceiptDate.Value = Date.Now
        'txtFakturPajakNo.Text = ""
        txtNomorFaktur1.Text = ""
        txtNomorFaktur2.Text = ""
        txtNomorFaktur3.Text = ""
        txtNomorFaktur4.Text = ""
        icFakturPajakDate.Value = Date.Now

        txtClaimAmount.Text = 0
        txtVATTotal.Text = 0
        txtPPHTotal.Text = 0
        hdnTotalReceiptAmount.Value = 0
        txtTotalReceiptAmount.Text = 0
        txtSignName.Text = ""
        txtSignPosition.Text = ""
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Sub GetDealerData(ByVal oDealer As Dealer)
        lblDealerCode.Text = oDealer.DealerCode
        lblDealerName.Text = oDealer.DealerName
    End Sub

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If txtReceiptNo.Text.Trim = "" Then
            sb.Append("Nomor Kuitansi harus Diisi\n")
        End If
        If txtNomorFaktur1.Text.Trim = "" Or txtNomorFaktur2.Text = "" Or txtNomorFaktur3.Text.Trim = "" Or txtNomorFaktur4.Text.Trim = "" Then
            sb.Append("Nomor Faktur Pajak harus Diisi\n")
        End If
        If txtSignName.Text.Trim = "" Then
            sb.Append("Nama Tanda Tangan harus Diisi\n")
        End If
        If txtSignPosition.Text.Trim = "" Then
            sb.Append("Jabatan Tanda Tangan harus Diisi\n")
        End If

        Dim noFaktur As String = txtNomorFaktur1.Text.Trim & "." & txtNomorFaktur2.Text.Trim & "-" & txtNomorFaktur3.Text.Trim & "." & txtNomorFaktur4.Text.Trim
        If Not noFaktur.Length = 18 Then
            sb.Append("No. Faktur Pajak Belum Sesuai\n")
        End If

        If ddlNoRek.Items.Count > 0 Then
            If ddlNoRek.SelectedIndex = 0 Then
                sb.Append("Nomor Rekening harus Diisi\n")
            End If
        End If

        Return sb.ToString()
    End Function

    Private Sub CalculationPPNPPH()
        Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(icFakturPajakDate.Value, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_2").ValueId)
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(icFakturPajakDate.Value, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        Dim dblClaimAmount As Decimal = CDec(txtClaimAmount.Text)
        Dim dblVATTotal As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=dblClaimAmount)
        Dim dblPPHTotal As Decimal = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=dblClaimAmount)
        Dim dblTotalReceiptAmount As Decimal = dblClaimAmount + dblVATTotal - dblPPHTotal

        txtVATTotal.Text = dblVATTotal.ToString("#,##0")
        txtPPHTotal.Text = dblPPHTotal.ToString("#,##0")
        hdnTotalReceiptAmount.Value = dblTotalReceiptAmount.ToString("#,##0")
        txtTotalReceiptAmount.Text = (dblTotalReceiptAmount + dblPPHTotal).ToString("#,##0")
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Mode = "New" Then
            If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Laporan_Buat_Kuitansi_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT KUITANSI")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_BabitKuitansi_Detail_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT KUITANSI")
            End If
        End If
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
            btnBack.Visible = True
        End If
        If Mode = "New" Then  ' Login as Dealer
            objDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = CType(sesHelper.GetSession("FrmInputBabitReportReceipt.DEALER"), Dealer)
        End If
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)

        Dim arlBabitReportReceipt As New ArrayList
        If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
            IsDealer = False
        Else
            IsDealer = True
        End If

        If (Not IsPostBack) Then
            InitiateAuthorization()
            ClearAll()
            objDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
            getBankAccount()

            If Not IsNothing(Request.QueryString("BabitReportHeaderID")) Then
                hdnBabitReportHeaderID.Value = Request.QueryString("BabitReportHeaderID")
                LoadDataBabitReportHeader(hdnBabitReportHeaderID.Value)
            End If

            GetDealerData(objDealer)
            getBankAccount()

            If Not IsNothing(Request.QueryString("BabitReportReceiptID")) Then
                hdnBabitReportReceiptID.Value = Request.QueryString("BabitReportReceiptID")
                LoadDataBabitReportReceipt(hdnBabitReportReceiptID.Value)
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitReportReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitReportReceipt), "BabitReportHeader.ID", MatchType.Exact, hdnBabitReportHeaderID.Value))
                arlBabitReportReceipt = New BabitReportReceiptFacade(User).Retrieve(criterias)
                If Not IsNothing(arlBabitReportReceipt) AndAlso arlBabitReportReceipt.Count > 0 Then
                    Dim objBabitReportReceipt As BabitReportReceipt = CType(arlBabitReportReceipt(0), BabitReportReceipt)
                    hdnBabitReportReceiptID.Value = objBabitReportReceipt.ID
                    LoadDataBabitReportReceipt(hdnBabitReportReceiptID.Value)
                End If
            End If
            If hdnBabitReportReceiptID.Value.Trim <> String.Empty AndAlso hdnBabitReportReceiptID.Value.Trim <> "0" Then
                btnCetak.Visible = True
            End If
            GetDealerData(objDealer)
            ModeControl()
            '---Harcode 
            txtClaimAmount.Enabled = False
            txtVATTotal.Enabled = False
            txtPPHTotal.Enabled = False
            txtTotalReceiptAmount.Enabled = False
            '-------
        End If
    End Sub

    Private Sub ModeControl()
        If Mode = "Detail" Then
            txtReceiptNo.Enabled = False
            'txtFakturPajakNo.Enabled = False
            txtNomorFaktur1.Enabled = False
            txtNomorFaktur2.Enabled = False
            txtNomorFaktur3.Enabled = False
            txtNomorFaktur4.Enabled = False
            icFakturPajakDate.Enabled = False
            icReceiptDate.Enabled = False
            txtSignName.Enabled = False
            txtSignPosition.Enabled = False
            ddlNoRek.Enabled = False
            btnSave.Enabled = False
            btnCalculate.Visible = False
        Else
            txtReceiptNo.Enabled = True
            'txtFakturPajakNo.Enabled = True
            txtNomorFaktur1.Enabled = True
            txtNomorFaktur2.Enabled = True
            txtNomorFaktur3.Enabled = True
            txtNomorFaktur4.Enabled = True
            icFakturPajakDate.Enabled = True
            icReceiptDate.Enabled = True
            txtSignName.Enabled = True
            txtSignPosition.Enabled = True
            ddlNoRek.Enabled = True
            btnSave.Enabled = True
            btnCalculate.Visible = True
        End If

        If Not visibleSave Then
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If

        CalculationPPNPPH()
        Dim noFaktur As String = txtNomorFaktur1.Text.Trim & "." & txtNomorFaktur2.Text.Trim & "-" & txtNomorFaktur3.Text.Trim & "." & txtNomorFaktur4.Text.Trim

        Dim _oDealer As Dealer = sesHelper.GetSession("FrmInputBabitReportReceipt.DEALER")
        objBabitReportHeader = sesHelper.GetSession(sessBabitReportHeader)

        Dim strEventRegNumber As String = String.Empty
        If Mode = "New" Then
            objBabitReportReceipt = New BabitReportReceipt
        Else
            objBabitReportReceipt = CType(sesHelper.GetSession(sessBabitReportReceipt), BabitReportReceipt)
        End If
        objBabitReportReceipt.BabitReportHeader = objBabitReportHeader
        objBabitReportReceipt.ReceiptNo = txtReceiptNo.Text
        objBabitReportReceipt.ReceiptDate = icReceiptDate.Value
        'objBabitReportReceipt.FakturPajakNo = txtFakturPajakNo.Text
        objBabitReportReceipt.FakturPajakNo = noFaktur
        objBabitReportReceipt.FakturPajakDate = icFakturPajakDate.Value
        objBabitReportReceipt.ClaimAmount = txtClaimAmount.Text
        objBabitReportReceipt.VATTotal = txtVATTotal.Text
        objBabitReportReceipt.PPHTotal = txtPPHTotal.Text
        objBabitReportReceipt.TotalReceiptAmount = hdnTotalReceiptAmount.Value
        objBabitReportReceipt.SignName = txtSignName.Text
        objBabitReportReceipt.SignPosition = txtSignPosition.Text
        objBabitReportReceipt.DealerBankAccount = New DealerBankAccountFacade(User).Retrieve(CInt(ddlNoRek.SelectedValue))

        Dim _result As Integer = 0
        Dim strJs As String = String.Empty
        If Mode = "New" Then
            _result = New BabitReportReceiptFacade(User).Insert(objBabitReportReceipt)
        Else
            _result = New BabitReportReceiptFacade(User).Update(objBabitReportReceipt)
        End If
        If _result > 0 Then
            'MessageBox.Show("Simpan Data Berhasil")
            ClearAll()
            strJs = "alert('Simpan Data Berhasil');"
            If Not IsNothing(Request.QueryString("From")) Then
                strJs += "window.location = '../Babit/FrmBabitReportReceiptList.aspx'"
            Else
                strJs += "window.location = '../Babit/FrmBabitReportEventList.aspx'"
            End If
        Else
            'MessageBox.Show("Simpan Gagal")
            strJs = "alert('Simpan Data Gagal');"
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If Not IsNothing(Request.QueryString("From")) Then
            Response.Redirect("~/Babit/FrmBabitReportReceiptList.aspx")
        Else
            Response.Redirect("~/Babit/FrmBabitReportEventList.aspx")
        End If
    End Sub

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        If Mode <> "New" Then
            If hdnBabitReportReceiptID.Value.Trim <> String.Empty AndAlso hdnBabitReportReceiptID.Value.Trim <> "0" Then
                Dim objBabitReportReceipt As BabitReportReceipt = New BabitReportReceiptFacade(User).Retrieve(CInt(hdnBabitReportReceiptID.Value))
                If Not IsNothing(objBabitReportReceipt) AndAlso objBabitReportReceipt.ID > 0 Then
                    If Not IsNothing(Request.QueryString("From")) Then
                        Server.Transfer("../Babit/FrmBabitKuitansi.aspx?id=" & objBabitReportReceipt.ID & "&Mode=" & Mode & "&From=" & Request.QueryString("From").ToString())
                    Else
                        Server.Transfer("../Babit/FrmBabitKuitansi.aspx?id=" & objBabitReportReceipt.ID & "&Mode=" & Mode)
                    End If
                End If
            End If
        Else
            MessageBox.Show("Input Kuitansi dahulu.")
        End If
    End Sub

    Private Sub getBankAccount()
        With ddlNoRek
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            If Mode = "Detail" Then
                For Each i As DealerBankAccount In objDealer.DealerBankAccounts
                    If i.BeneficiaryName.Trim <> "" Then
                        .Items.Add(New ListItem(i.BankName & " / " & i.BankAccount, i.ID))
                    End If
                Next
            Else
                For Each i As DealerBankAccount In objDealer.DealerActiveBankAccounts
                    If i.BeneficiaryName.Trim <> "" Then
                        .Items.Add(New ListItem(i.BankName & " / " & i.BankAccount, i.ID))
                    End If
                Next
            End If
        End With
        ddlNoRek.SelectedIndex = 0
    End Sub

    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs)
        CalculationPPNPPH()
    End Sub
#End Region

End Class
