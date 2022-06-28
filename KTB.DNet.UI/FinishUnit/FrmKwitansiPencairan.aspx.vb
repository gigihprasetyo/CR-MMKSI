Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.Globalization
Public Class FrmKwitansiPencairan
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ltrlTotalTerbilang As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlDescription As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlTotalHarga As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlDealerPlace As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlCurrentDate As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlDealerName As System.Web.UI.WebControls.Literal
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents ltrNoKwitansi As System.Web.UI.WebControls.Literal
    Protected WithEvents lblTgl As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents ltrlInterest As System.Web.UI.WebControls.Literal
    Protected WithEvents lblInterest As System.Web.UI.WebControls.Label
    Protected WithEvents ltrlTax As System.Web.UI.WebControls.Literal
    Protected WithEvents lblTax As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitikTax As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitikInterest As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
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
    End Sub

#End Region

#Region "Custom Variable"
    Dim sHelper As New SessionHelper
    Dim dblTotalHarga As Double
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            'Dim id As Integer = CInt(Request.QueryString("id"))

            btnPrint.Attributes("onclick") = "PrintDocument();"

            Dim strIdTmp As String()
            Dim strIdAwal As String
            Dim objDepositAKuitansiPencairan As DepositAKuitansiPencairan
            If Not IsNothing(Request.QueryString("id")) Then
                'strIdTmp = Request.QueryString("id").Split(";")
                'strIdAwal = strIdTmp(0)
                strIdAwal = Request.QueryString("id")
            End If

            If strIdAwal <> "" Then
                objDepositAKuitansiPencairan = New DepositAKuitansiPencairanFacade(User).Retrieve(CType(strIdAwal, Integer))
            End If

            ' Menghasilkan 1 no Invoice dahulu, baru update yg lainnya, dgn no Invoice yg sama

            ' Generate Invoice
            ' If objSalesUnifHeaderOrder.InvoiceNo = "" Then            
            'End If

            ' set data
            MapToScreen(objDepositAKuitansiPencairan)
        End If
    End Sub

    Private Sub MapToScreen(ByVal objDomain As DepositAKuitansiPencairan)
        Dim cultureInfo As New cultureInfo("id-ID")
        btnPrint.Attributes("onclick") = "window.print();"
        Dim cf As New CommonFunction
        ltrNoKwitansi.Text = objDomain.ReceiptNumber
        ltrlDescription.Text = Server.HtmlDecode(objDomain.Description).Replace(Chr(13), "<BR>").Replace(Chr(10), "")
        Dim objDepositAPencairan As New DepositAPencairanH
        objDepositAPencairan = New DepositAPencairanHFacade(User).Retrieve(objDomain.NoReg)
        If objDepositAPencairan.ID > 0 Then
            If objDepositAPencairan.DealerBankAccount.ID > 0 Then
                ltrlNoRekening.Text = objDepositAPencairan.DealerBankAccount.BankAccount & " - " & objDepositAPencairan.DealerBankAccount.BankName
            End If
        End If
        ltrlDealerName.Text = objDomain.Dealer.DealerName
        'ltrlCurrentDate.Text = Date.Now.ToString("d MMMM yyyy", cultureInfo)
        If objDepositAPencairan.ID > 0 Then
            If objDomain.Type = 1 Or objDomain.Type = 3 Then
                dblTotalHarga = objDepositAPencairan.ApprovalAmount
            Else
                dblTotalHarga = objDepositAPencairan.DealerAmount
            End If
        End If
        

        'Dim totalHarga As Double = dblTotalHarga 'Math.Round(objDomain.TotalHarga + (0.1 * objDomain.TotalHarga))
        ltrlTotalHarga.Text = "Rp. " & dblTotalHarga.ToString("#,##0") 'totalHarga.ToString("#,##0")
        ltrlTotalTerbilang.Text = cf.Terbilang(dblTotalHarga)
        ltrlDealerPlace.Text = objDomain.Dealer.City.CityName
        lblDealerCode.Text = objDomain.Dealer.DealerCode & " - " & objDomain.ProductCategory.Code
        lblDealerName.Text = objDomain.Dealer.DealerName
        lblTgl.Text = Date.Now.ToString("d MMMM yyyy", cultureInfo)
        ltrlNameTTD.Text = objDomain.SignedBy
        ltrlJabatan.Text = objDomain.Jabatan
        lblDateCetak.Text = "( " & Date.Now.ToString("d MMMM yyyy", cultureInfo) & " )"
        If objDomain.Type = 4 Then
            lblInterest.Visible = True
            lblTax.Visible = True
            lblTitikInterest.Visible = True
            lblTitikTax.Visible = True
            ltrlInterest.Visible = True
            ltrlTax.Visible = True

            Dim objDepositA As New DepositAPencairanH
            objDepositA = New DepositAPencairanHFacade(User).Retrieve(objDomain.NoReg)
            If objDepositA.ID > 0 Then
                ltrlInterest.Text = "Rp. " & objDepositA.DepositAInterestH.InterestAmount.ToString("#,##0")
                ltrlTax.Text = "Rp. " & objDepositA.DepositAInterestH.TaxAmount.ToString("#,##0")
                lblTotal.Text = "Netto"
            End If
        ElseIf objDomain.Type = 1 And objDomain.DNNumber <> String.Empty Then
            lblInterest.Visible = True
            lblTax.Visible = True
            lblTitikInterest.Visible = True
            lblTitikTax.Visible = True
            ltrlInterest.Visible = True
            ltrlTax.Visible = True

            lblTotal.Text = "Biaya"
            'lblInterest.Text = "PPn (10%)"
            lblInterest.Text = String.Empty
            lblTax.Text = "Total"
            If objDepositAPencairan.ID > 0 Then
                Dim crit As New CriteriaComposite(New Criteria(GetType(DebitNote), "RowStatus", MatchType.Exact, 0))
                crit.opAnd(New Criteria(GetType(DebitNote), "Dealer.DealerCode", MatchType.Exact, objDepositAPencairan.Dealer.DealerCode))
                crit.opAnd(New Criteria(GetType(DebitNote), "DNNumber", MatchType.Exact, objDomain.DNNumber))
                Dim objDebitNote As DebitNote
                objDebitNote = CType(New DebitNoteFacade(User).Retrieve(crit)(0), DebitNote)
                If objDebitNote.ID > 0 Then
                    ltrlTotalHarga.Text = "Rp. " & objDebitNote.Amount.ToString("#,##0")
                    Dim dblPPn As Double
                    dblPPn = 0.1 * objDebitNote.Amount
                    'ltrlInterest.Text = "Rp. " & dblPPn.ToString("#,##0")
                    ltrlInterest.Text = String.Empty
                    ltrlTax.Text = "Rp. " & objDepositAPencairan.DealerAmount.ToString("#,##0")
                End If
            End If
        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Server.Transfer(sHelper.GetSession("BackKwitansi"))
    End Sub
End Class
