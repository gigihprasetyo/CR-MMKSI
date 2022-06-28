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

#End Region

Public Class FrmInputBabitEventReportReceipt
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"

    Dim objDealer As New Dealer
    Dim objLoginUser As UserInfo
    Dim objBabitEventReportReceipt As BabitEventReportReceipt
    Dim objBabitEventReportHeader As BabitEventReportHeader

    Const sessBabitEventReportHeader As String = "sessDataBabitEventReportHeader"
    Const sessBabitEventReportReceipt As String = "sessDataBabitEventReportReceipt"

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

    Private Sub LoadDataBabitEventReportReceipt(intBabitEventReportReceiptID As Integer)
        objBabitEventReportReceipt = New BabitEventReportReceiptFacade(User).Retrieve(intBabitEventReportReceiptID)
        If Not IsNothing(objBabitEventReportReceipt) Then
            sesHelper.SetSession(sessBabitEventReportReceipt, objBabitEventReportReceipt)
            sesHelper.SetSession(sessBabitEventReportHeader, objBabitEventReportReceipt.BabitEventReportHeader)

            objDealer = objBabitEventReportReceipt.BabitEventReportHeader.Dealer
            sesHelper.SetSession("FrmInputBabitEventReportReceipt.DEALER", objDealer)

            txtReceiptNo.Text = objBabitEventReportReceipt.ReceiptNo
            icReceiptDate.Value = objBabitEventReportReceipt.ReceiptDate
            'txtFakturPajakNo.Text = objBabitEventReportReceipt.FakturPajakNo
            Dim delimiter() As Char = {".", "-"}
            If objBabitEventReportReceipt.FakturPajakNo.Contains(".") And objBabitEventReportReceipt.FakturPajakNo.Contains("-") Then
                Dim noFaktur() As String = objBabitEventReportReceipt.FakturPajakNo.Split(delimiter)
                If noFaktur(0).Count > 0 And noFaktur(1).Count > 0 And noFaktur(2).Count > 0 And noFaktur(3).Count > 0 Then
                    txtNomorFaktur1.Text = noFaktur(0)
                    txtNomorFaktur2.Text = noFaktur(1)
                    txtNomorFaktur3.Text = noFaktur(2)
                    txtNomorFaktur4.Text = noFaktur(3)
                End If
            Else
                If Not objBabitEventReportReceipt.BabitEventReportHeader.Status = 0 Or Not objBabitEventReportReceipt.BabitEventReportHeader.Status = 1 Then
                    lblNomorFakturOld.Text = objBabitEventReportReceipt.FakturPajakNo
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
            icFakturPajakDate.Value = objBabitEventReportReceipt.FakturPajakDate

            txtClaimAmount.Text = objBabitEventReportReceipt.ClaimAmount.ToString("#,##0")
            txtVATTotal.Text = objBabitEventReportReceipt.VATTotal.ToString("#,##0")
            txtPPHTotal.Text = objBabitEventReportReceipt.PPHTotal.ToString("#,##0")
            hdnTotalReceiptAmount.Value = objBabitEventReportReceipt.TotalReceiptAmount
            txtTotalReceiptAmount.Text = (objBabitEventReportReceipt.TotalReceiptAmount + objBabitEventReportReceipt.PPHTotal).ToString("#,##0")

            lblTujuanPembayaran.Text = "Untuk Pembayaran Event " + objBabitEventReportReceipt.BabitEventReportHeader.BabitEventProposalHeader.EventProposalName + " - Periode " + objBabitEventReportReceipt.BabitEventReportHeader.PeriodStart.ToString("dd/MM/yyyy") + " - " + objBabitEventReportReceipt.BabitEventReportHeader.PeriodStart.ToString("dd/MM/yyyy")

            lblNoRegEvent.Text = objBabitEventReportReceipt.BabitEventReportHeader.BabitEventProposalHeader.EventRegNumber
            txtSignName.Text = objBabitEventReportReceipt.SignName
            txtSignPosition.Text = objBabitEventReportReceipt.SignPosition
            If Not IsNothing(objBabitEventReportReceipt.DealerBankAccount) Then
                ddlNoRek.SelectedValue = objBabitEventReportReceipt.DealerBankAccount.ID
            End If

            If objBabitEventReportReceipt.Status <> 0 Then
                visibleSave = False
            End If
        End If
    End Sub

    Private Sub LoadDataBabitEventReportHeader(intBabitEventReportHeaderID As Integer)
        objBabitEventReportHeader = New BabitEventReportHeaderFacade(User).Retrieve(intBabitEventReportHeaderID)
        If Not IsNothing(objBabitEventReportHeader) Then
            sesHelper.SetSession(sessBabitEventReportHeader, objBabitEventReportHeader)

            objDealer = objBabitEventReportHeader.Dealer
            sesHelper.SetSession("FrmInputBabitEventReportReceipt.DEALER", objDealer)

            Dim dblClaimAmount As Decimal = 0
            Dim dblVATTotal As Decimal = 0
            Dim dblPPHTotal As Decimal = 0
            Dim dblTotalReceiptAmount As Decimal = 0

            dblClaimAmount = objBabitEventReportHeader.ApprovedBudget
            dblVATTotal = (dblClaimAmount * 10) / 100
            dblPPHTotal = (dblClaimAmount * 2) / 100

            Dim objAppConfig As New AppConfig
            Dim criterias As New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, "BabitDealerBebasPPh"))
            Dim arrConfig As ArrayList = New AppConfigFacade(User).Retrieve(criterias)
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

            txtClaimAmount.Text = dblClaimAmount.ToString("#,##0")
            txtVATTotal.Text = dblVATTotal.ToString("#,##0")
            txtPPHTotal.Text = dblPPHTotal.ToString("#,##0")

            hdnTotalReceiptAmount.Value = dblTotalReceiptAmount
            txtTotalReceiptAmount.Text = (dblTotalReceiptAmount + dblPPHTotal).ToString("#,##0")

            lblTujuanPembayaran.Text = "Untuk Pembayaran Event " + objBabitEventReportHeader.BabitEventProposalHeader.EventProposalName + " - Periode " + objBabitEventReportHeader.PeriodStart.ToString("dd/MM/yyyy") + " - " + objBabitEventReportHeader.PeriodStart.ToString("dd/MM/yyyy")

            lblNoRegEvent.Text = objBabitEventReportHeader.BabitEventProposalHeader.EventRegNumber
        End If
    End Sub

    Private Sub ClearAll()
        hdnBabitEventReportHeaderID.Value = ""

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
        If ddlNoRek.SelectedIndex = 0 Then
            sb.Append("Nomor Rekening harus Diisi\n")
        End If

        Dim noFaktur As String = txtNomorFaktur1.Text.Trim & "." & txtNomorFaktur2.Text.Trim & "-" & txtNomorFaktur3.Text.Trim & "." & txtNomorFaktur4.Text.Trim
        If Not noFaktur.Length = 18 Then
            sb.Append("No. Faktur Pajak Belum Sesuai\n")
        End If

        Return sb.ToString()
    End Function

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Mode = "New" Then
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Laporan_Buat_Kuitansi_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - INPUT KUITANSI")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Event_Kuitansi_Detail_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - INPUT KUITANSI")
            End If
        End If
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
            btnBack.Visible = True
        End If
        If Mode = "New" Then  ' Login as Dealer
            objDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = CType(sesHelper.GetSession("FrmInputBabitEventReportReceipt.DEALER"), Dealer)
        End If
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Dim arlBabitEventReportReceipt As New ArrayList
        If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
            IsDealer = False
        Else
            IsDealer = True
        End If

        If (Not IsPostBack) Then
            InitiateAuthorization()
            ClearAll()

            If Not IsNothing(Request.QueryString("BabitEventReportHeaderID")) Then
                hdnBabitEventReportHeaderID.Value = Request.QueryString("BabitEventReportHeaderID")
                LoadDataBabitEventReportHeader(hdnBabitEventReportHeaderID.Value)
            End If

            GetDealerData(objDealer)
            getBankAccount()

            If Not IsNothing(Request.QueryString("BabitEventReportReceiptID")) Then
                hdnBabitEventReportReceiptID.Value = Request.QueryString("BabitEventReportReceiptID")
                LoadDataBabitEventReportReceipt(hdnBabitEventReportReceiptID.Value)
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitEventReportReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitEventReportReceipt), "BabitEventReportHeader.ID", MatchType.Exact, hdnBabitEventReportHeaderID.Value))
                arlBabitEventReportReceipt = New BabitEventReportReceiptFacade(User).Retrieve(criterias)
                If Not IsNothing(arlBabitEventReportReceipt) AndAlso arlBabitEventReportReceipt.Count > 0 Then
                    Dim objBabitEventReportReceipt As BabitEventReportReceipt = CType(arlBabitEventReportReceipt(0), BabitEventReportReceipt)
                    hdnBabitEventReportReceiptID.Value = objBabitEventReportReceipt.ID
                    LoadDataBabitEventReportReceipt(hdnBabitEventReportReceiptID.Value)
                End If
            End If
            If hdnBabitEventReportReceiptID.Value.Trim <> String.Empty AndAlso hdnBabitEventReportReceiptID.Value.Trim <> "0" Then
                btnCetak.Visible = True
            End If
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

        Dim noFaktur As String = txtNomorFaktur1.Text.Trim & "." & txtNomorFaktur2.Text.Trim & "-" & txtNomorFaktur3.Text.Trim & "." & txtNomorFaktur4.Text.Trim

        Dim _oDealer As Dealer = sesHelper.GetSession("FrmInputBabitEventReportReceipt.DEALER")
        objBabitEventReportHeader = sesHelper.GetSession(sessBabitEventReportHeader)

        Dim strEventRegNumber As String = String.Empty
        If Mode = "New" Then
            objBabitEventReportReceipt = New BabitEventReportReceipt
        Else
            objBabitEventReportReceipt = CType(sesHelper.GetSession(sessBabitEventReportReceipt), BabitEventReportReceipt)
        End If
        objBabitEventReportReceipt.BabitEventReportHeader = objBabitEventReportHeader
        objBabitEventReportReceipt.ReceiptNo = txtReceiptNo.Text
        objBabitEventReportReceipt.ReceiptDate = icReceiptDate.Value
        'objBabitEventReportReceipt.FakturPajakNo = txtFakturPajakNo.Text
        objBabitEventReportReceipt.FakturPajakNo = noFaktur
        objBabitEventReportReceipt.FakturPajakDate = icFakturPajakDate.Value
        objBabitEventReportReceipt.ClaimAmount = txtClaimAmount.Text
        objBabitEventReportReceipt.VATTotal = txtVATTotal.Text
        objBabitEventReportReceipt.PPHTotal = txtPPHTotal.Text
        objBabitEventReportReceipt.TotalReceiptAmount = hdnTotalReceiptAmount.Value
        objBabitEventReportReceipt.SignName = txtSignName.Text
        objBabitEventReportReceipt.SignPosition = txtSignPosition.Text
        objBabitEventReportReceipt.DealerBankAccount = New DealerBankAccountFacade(User).Retrieve(CInt(ddlNoRek.SelectedValue))

        Dim _result As Integer = 0
        Dim strJs As String = String.Empty
        If Mode = "New" Then
            _result = New BabitEventReportReceiptFacade(User).Insert(objBabitEventReportReceipt)
        Else
            _result = New BabitEventReportReceiptFacade(User).Update(objBabitEventReportReceipt)
        End If
        If _result > 0 Then
            ClearAll()
            strJs = "alert('Simpan Data Berhasil');"
            If Not IsNothing(Request.QueryString("From")) Then
                strJs += "window.location = '../Babit/FrmBabitEventReportReceiptList.aspx'"
            Else
                strJs += "window.location = '../Babit/FrmBabitEventReportList.aspx'"
            End If
        Else
            strJs = "alert('Simpan Data Gagal');"
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If Not IsNothing(Request.QueryString("From")) Then
            Response.Redirect("~/Babit/FrmBabitEventReportReceiptList.aspx")
        Else
            Response.Redirect("~/Babit/FrmBabitEventReportList.aspx")
        End If
    End Sub

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        If Mode <> "New" Then
            If hdnBabitEventReportReceiptID.Value.Trim <> String.Empty AndAlso hdnBabitEventReportReceiptID.Value.Trim <> "0" Then
                Dim objBabitEventReportReceipt As BabitEventReportReceipt = New BabitEventReportReceiptFacade(User).Retrieve(CInt(hdnBabitEventReportReceiptID.Value))
                If Not IsNothing(objBabitEventReportReceipt) AndAlso objBabitEventReportReceipt.ID > 0 Then
                    If Not IsNothing(Request.QueryString("From")) Then
                        Server.Transfer("../Babit/FrmBabitEventKuitansi.aspx?id=" & objBabitEventReportReceipt.ID & "&Mode=" & Mode & "&From=" & Request.QueryString("From").ToString())
                    Else
                        Server.Transfer("../Babit/FrmBabitEventKuitansi.aspx?id=" & objBabitEventReportReceipt.ID & "&Mode=" & Mode)
                    End If
                End If
            End If
        Else
            MessageBox.Show("Harap Input Kuitansi dahulu.")
        End If
    End Sub

    Private Sub getBankAccount()
        With ddlNoRek
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each i As DealerBankAccount In objDealer.DealerBankAccounts
                .Items.Add(New ListItem(i.BankName & " / " & i.BankAccount, i.ID))
            Next
        End With
        ddlNoRek.SelectedIndex = 0
    End Sub
#End Region

End Class
