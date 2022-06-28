Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.Globalization

Public Class FrmBabitEventKuitansi
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ltrlTotalTerbilang As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlDescription As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlDealerPlace As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlCurrentDate As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlDealerName As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrEventRegNumber As System.Web.UI.WebControls.Literal
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents ltrNoKwitansi As System.Web.UI.WebControls.Literal
    Protected WithEvents lblTgl As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents ltrlClaimAmount As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlVATTotal As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlPPHTotal As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlTotalReceiptAmount As System.Web.UI.WebControls.Literal
    Protected WithEvents lblTitikClaim As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitikPPN As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitikPPh As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitikTotal As System.Web.UI.WebControls.Label
    Protected WithEvents ltrlNameTTD As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlJabatan As System.Web.UI.WebControls.Literal
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents lblDateCetak As System.Web.UI.WebControls.Label
    Protected WithEvents ltrlNoRekening As System.Web.UI.WebControls.Literal

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
        End If
    End Sub

#End Region

#Region "Custom Variable"
    Dim sHelper As New SessionHelper
    Dim dblTotalHarga As Double
    Dim Mode As String = "New"
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            'Dim id As Integer = CInt(Request.QueryString("id"))

            btnPrint.Attributes("onclick") = "PrintDocument();"

            Dim strIdTmp As String()
            Dim strIdAwal As String
            Dim objBabitEventReportReceipt As BabitEventReportReceipt
            If Not IsNothing(Request.QueryString("id")) Then
                'strIdTmp = Request.QueryString("id").Split(";")
                'strIdAwal = strIdTmp(0)
                strIdAwal = Request.QueryString("id")
            End If

            If strIdAwal <> "" Then
                objBabitEventReportReceipt = New BabitEventReportReceiptFacade(User).Retrieve(CType(strIdAwal, Integer))
            End If

            ' Menghasilkan 1 no Invoice dahulu, baru update yg lainnya, dgn no Invoice yg sama

            ' Generate Invoice
            ' If objSalesUnifHeaderOrder.InvoiceNo = "" Then            
            'End If

            ' set data
            MapToScreen(objBabitEventReportReceipt)
        End If
    End Sub

    Private Sub MapToScreen(ByVal objDomain As BabitEventReportReceipt)
        Dim cultureInfo As New CultureInfo("id-ID")
        btnPrint.Attributes("onclick") = "window.print();"
        Dim cf As New CommonFunction

        ltrNoKwitansi.Text = objDomain.ReceiptNo

        Dim strTujuanPembayaran As String = "Untuk Pembayaran Event " + objDomain.BabitEventReportHeader.BabitEventProposalHeader.EventProposalName + " - Periode " + objDomain.BabitEventReportHeader.PeriodStart.ToString("dd/MM/yyyy") + " - " + objDomain.BabitEventReportHeader.PeriodStart.ToString("dd/MM/yyyy")

        ltrEventRegNumber.Text = objDomain.BabitEventReportHeader.BabitEventProposalHeader.EventRegNumber
        ltrlDescription.Text = Server.HtmlDecode(strTujuanPembayaran).Replace(Chr(13), "<BR>").Replace(Chr(10), "")
        If Not IsNothing(objDomain.DealerBankAccount) AndAlso objDomain.DealerBankAccount.ID > 0 Then
            ltrlNoRekening.Text = objDomain.DealerBankAccount.BankAccount & " - " & objDomain.DealerBankAccount.BankName
        End If

        ltrlDealerName.Text = objDomain.BabitEventReportHeader.Dealer.DealerName

        Dim dblClaimAmount As Double = objDomain.ClaimAmount
        Dim dblVATTotal As Double = objDomain.VATTotal
        Dim dblPPHTotal As Double = objDomain.PPHTotal
        Dim dblTotalReceiptAmount As Double = objDomain.TotalReceiptAmount + objDomain.PPHTotal

        ltrlClaimAmount.Text = "Rp. " & dblClaimAmount.ToString("#,##0")
        ltrlVATTotal.Text = "Rp. " & dblVATTotal.ToString("#,##0")
        ltrlPPHTotal.Text = "Rp. " & dblPPHTotal.ToString("#,##0")
        ltrlTotalReceiptAmount.Text = "Rp. " & dblTotalReceiptAmount.ToString("#,##0")

        ltrlTotalTerbilang.Text = IIf(dblTotalReceiptAmount = 0, 0, cf.Terbilang(dblTotalReceiptAmount))
        ltrlDealerPlace.Text = objDomain.BabitEventReportHeader.Dealer.City.CityName
        lblDealerCode.Text = objDomain.BabitEventReportHeader.Dealer.DealerCode & " - MMC"
        lblDealerName.Text = objDomain.BabitEventReportHeader.Dealer.DealerName
        lblTgl.Text = Date.Now.ToString("d MMMM yyyy", cultureInfo)
        ltrlNameTTD.Text = objDomain.SignName
        ltrlJabatan.Text = objDomain.SignPosition
        lblDateCetak.Text = "( " & Date.Now.ToString("d MMMM yyyy", cultureInfo) & " )"
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Dim strIdAwal As String = String.Empty
        Dim objBabitEventReportReceipt As BabitEventReportReceipt
        If Not IsNothing(Request.QueryString("id")) Then
            strIdAwal = Request.QueryString("id")
        End If
        If Not IsNothing(Request.QueryString("From")) Then
            Server.Transfer("../Babit/FrmInputBabitEventReportReceipt.aspx?BabitEventReportReceiptID=" & strIdAwal & "&Mode=" & Mode & "&From=" & Request.QueryString("From").ToString())
        Else
            Server.Transfer("../Babit/FrmInputBabitEventReportReceipt.aspx?BabitEventReportReceiptID=" & strIdAwal & "&Mode=" & Mode)
        End If
    End Sub
End Class
