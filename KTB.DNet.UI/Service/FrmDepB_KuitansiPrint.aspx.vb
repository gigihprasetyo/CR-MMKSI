

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.Globalization
Public Class FrmDepB_KuitansiPrint
    Inherits System.Web.UI.Page

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
            Dim objDepositBReceipt As DepositBReceipt
            If Not IsNothing(Request.QueryString("id")) Then
                'strIdTmp = Request.QueryString("id").Split(";")
                'strIdAwal = strIdTmp(0)
                strIdAwal = Request.QueryString("id")
            End If

            If strIdAwal <> "" Then
                objDepositBReceipt = New DepositBReceiptFacade(User).Retrieve(CType(strIdAwal, Integer))
            End If

            ' Menghasilkan 1 no Invoice dahulu, baru update yg lainnya, dgn no Invoice yg sama

            ' Generate Invoice
            ' If objSalesUnifHeaderOrder.InvoiceNo = "" Then            
            'End If

            ' set data
            MapToScreen(objDepositBReceipt)

        End If
    End Sub

    Private Sub MapToScreen(ByVal objDomain As DepositBReceipt)
        Dim cultureInfo As New cultureInfo("id-ID")
        btnPrint.Attributes("onclick") = "window.print();"
        Dim cf As New CommonFunction
        ltrNoKwitansi.Text = objDomain.NomorKuitansi
        ltrlDescription.Text = Server.HtmlDecode(objDomain.Keterangan).Replace(Chr(13), "<BR>").Replace(Chr(10), "")
        Dim objDepositAPencairan As New DepositBPencairanHeader
        objDepositAPencairan = New DepositBPencairanHeaderFacade(User).Retrieve(objDomain.DepositBPencairanHeader.NoReg)
        If objDepositAPencairan.ID > 0 Then
            If objDepositAPencairan.DealerBankAccount.ID > 0 Then
                ltrlNoRekening.Text = objDepositAPencairan.DealerBankAccount.BankAccount & " - " & objDepositAPencairan.DealerBankAccount.BankName
            End If

            'If (objDomain.DepositBPencairanHeader.TipePengajuan <> DepositBEnum.TipePengajuan.Offset_SP) Then
            '    dblTotalHarga = objDepositAPencairan.ApprovalAmount
            'Else
            '    dblTotalHarga = objDepositAPencairan.DealerAmount
            'End If
            Dim selectedTipe As DepositBEnum.TipePengajuan = CType([Enum].Parse(GetType(DepositBEnum.TipePengajuan), objDomain.DepositBPencairanHeader.TipePengajuan), DepositBEnum.TipePengajuan)
            Select Case selectedTipe
                Case DepositBEnum.TipePengajuan.Interest
                    dblTotalHarga = objDepositAPencairan.DepositBInterestHeader.NettoAmount
                Case DepositBEnum.TipePengajuan.Offset_SP
                    dblTotalHarga = objDepositAPencairan.ApprovalAmount
                Case Else
                    dblTotalHarga = objDepositAPencairan.DealerAmount
            End Select
        End If
        ltrlDealerName.Text = objDomain.DepositBPencairanHeader.Dealer.DealerName
        'ltrlCurrentDate.Text = Date.Now.ToString("d MMMM yyyy", cultureInfo)

        'Dim totalHarga As Double = dblTotalHarga 'Math.Round(objDomain.TotalHarga + (0.1 * objDomain.TotalHarga))
        ltrlTotalHarga.Text = "Rp. " & dblTotalHarga.ToString("#,##0") 'totalHarga.ToString("#,##0")
        ltrlTotalTerbilang.Text = cf.Terbilang(dblTotalHarga)
        ltrlDealerPlace.Text = objDomain.DepositBPencairanHeader.Dealer.City.CityName
        lblDealerCode.Text = objDomain.DepositBPencairanHeader.Dealer.DealerCode & " - " & objDomain.DepositBPencairanHeader.ProductCategory.Code
        lblDealerName.Text = objDomain.DepositBPencairanHeader.Dealer.DealerName
        lblTgl.Text = Date.Now.ToString("d MMMM yyyy", cultureInfo)
        ltrlNameTTD.Text = objDomain.NamaPejabat
        ltrlJabatan.Text = objDomain.Jabatan
        lblDateCetak.Text = "( " & Date.Now.ToString("d MMMM yyyy", cultureInfo) & " )"

        CreateBarcode(objDomain)

        If objDomain.DepositBPencairanHeader.TipePengajuan = DepositBEnum.TipePengajuan.Interest Then
            lblInterest.Visible = True
            lblTax.Visible = True
            lblTitikInterest.Visible = True
            lblTitikTax.Visible = True
            ltrlInterest.Visible = True
            ltrlTax.Visible = True

            Dim objDepositA As New DepositBPencairanHeader
            objDepositA = New DepositBPencairanHeaderFacade(User).Retrieve(objDomain.DepositBPencairanHeader.NoReg)
            If objDepositA.ID > 0 Then
                ltrlInterest.Text = "Rp. " & objDepositA.DepositBInterestHeader.InterestAmount.ToString("#,##0")
                ltrlTax.Text = "Rp. " & objDepositA.DepositBInterestHeader.TaxAmount.ToString("#,##0")
                lblTotal.Text = "Netto"
                ltrlTotalHarga.Text = "Rp. " & objDepositA.DepositBInterestHeader.NettoAmount.ToString("#,##0")
            End If
        ElseIf objDomain.DepositBPencairanHeader.TipePengajuan = DepositBEnum.TipePengajuan.ProjectService Then
            
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
                Dim objDebitNote As DebitNote
                If Not IsNothing(objDomain.DepositBPencairanHeader.DepositBDebitNote) Then
                    If objDomain.DepositBPencairanHeader.DepositBDebitNote.DNNumber <> String.Empty Then
                        objDebitNote = New DebitNoteFacade(User).Retrieve(objDomain.DepositBPencairanHeader.DepositBDebitNote.DNNumber)
                        If objDebitNote.ID > 0 Then
                            ltrlTotalHarga.Text = "Rp. " & objDebitNote.Amount.ToString("#,##0")
                            Dim dblPPn As Double
                            dblPPn = 0.1 * objDebitNote.Amount
                            'ltrlInterest.Text = "Rp. " & dblPPn.ToString("#,##0")
                            ltrlInterest.Text = String.Empty
                            ltrlTax.Text = "Rp. " & dblPPn.ToString("#,##0")
                        End If
                    End If
                End If

            End If
        End If

    End Sub

    Private Sub CreateBarcode(ByVal objDepositBReceipt As DepositBReceipt)
        Dim strImageURL As String = "../WebResources/GenerateBarcodeImage.aspx?" & _
            "d=" & Server.UrlEncode(objDepositBReceipt.NoRegKuitansi) & "&h=150" & "&w=300"

        BarcodeImage.ImageUrl = strImageURL
        'BarcodeImage.Width = 300
        'BarcodeImage.Height = 150
        BarcodeImage.Visible = True
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        'If Not IsNothing(sHelper.GetSession("BackKwitansiDepositB")) Then
        Server.Transfer(sHelper.GetSession("BackKwitansiDepositB"))
        'Else
        '    Server.Transfer("../Service/FrmDepB_KuitansiPencairan.aspx")
        'End If
    End Sub
End Class
