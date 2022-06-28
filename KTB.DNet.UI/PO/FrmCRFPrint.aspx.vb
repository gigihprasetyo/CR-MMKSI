#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
#End Region

Public Class FrmCRFPrint
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblReceiptNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblReceivedFrom As System.Web.UI.WebControls.Label
    Protected WithEvents lblSumOf As System.Web.UI.WebControls.Label
    Protected WithEvents lblPaymentOf As System.Web.UI.WebControls.Label
    Protected WithEvents lblAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblGyroNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblValueDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblSONumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblCreatedBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblCreatedDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblRemark1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblRemark2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button

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

    Private _PageOpener As String = "../PO/UpdatePaymentStatus.aspx"
    Private _sHelper As New SessionHelper
#End Region

#Region "Custom Methods"

    Private Sub Initialization()
        If Not IsNothing(Me._sHelper.GetSession("FrmCRFPrint.PageOpener")) Then
            Me._PageOpener = CType(Me._sHelper.GetSession("FrmCRFPrint.PageOpener"), String)
        End If
        ShowData(CType(Request.Item("id"), Integer))
    End Sub

    Private Sub ShowData(ByVal DPID As Integer)
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim oDP As DailyPayment
        Dim oCF As New CommonFunction
        Dim str As String = ""

        oDP = oDPFac.Retrieve(DPID)
        If oDP Is Nothing Then
            MessageBox.Show("Data tidak ditemukan")
            Exit Sub
        Else
            Dim htmTable As HtmlTable = GetHTMLTable(oDP)
            Dim arlDP As ArrayList = CType(_sHelper.GetSession("arlDPWithSameReceiptNumber"), ArrayList)
            Dim Total As Decimal = 0

            For Each objDP As DailyPayment In arlDP
                Total += objDP.Amount
            Next
            _sHelper.RemoveSession("arlDPWithSameReceiptNumber")
            lblReceiptNo.Text = oDP.ReceiptNumber
            lblReceivedFrom.Text = oDP.POHeader.ContractHeader.Dealer.DealerName & ", " & oDP.POHeader.ContractHeader.Dealer.DealerCode
            lblSumOf.Text = oCF.Terbilang_Eng(Total).ToUpper & " RUPIAHS" 'oDP.Amount) 
            lblPaymentOf.Text = oDP.Reason
            'lblAmount.Text = FormatNumber(oDP.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " /IDR"
            lblAmount.Text = FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " /IDR"
            lblGyroNo.Text = oDP.SlipNumber
            lblValueDate.Text = Format(oDP.BaselineDate, "dd.MM.yyyy")
            'lblSONumber.Text = oDP.POHeader.SONumber
            lblSONumber.Controls.Add(htmTable)

            lblCreatedBy.Text = oDP.PIC
            lblCreatedDate.Text = IIf(oDP.EntryDate < DateSerial(1990, 1, 1), "", Format(oDP.EntryDate, "dd.MM.yyyy"))
            str = "Dokumen ini merupakan bagian tidak terpisahkan dari Akta Perjanjian Jual Beli "
            str &= "No. " & oDP.POHeader.ContractHeader.Dealer.AgreementNo
            str &= ", " & Format(oDP.POHeader.ContractHeader.Dealer.AgreementDate, "dd.MM.yyyy")
            str &= ", Notaris " & GetSPAName(oDP.POHeader.ContractHeader.Dealer.SPANumber)
            str &= ", dan setiap perubahan dan/atau pembaharuannya di kemudian hari"
            lblRemark1.Text = str
            str = "Dokumen ini dibuat dalam bentuk elektronik dan diperlakukan sebagai alat bukti yang sah meskipun tidak ditandatangani oleh pihak PT Mitsubishi Motors Krama Yudha Sales Indonesia"
            lblRemark2.Text = str

        End If
    End Sub

    Private Function GetHTMLTable(ByVal objDP As DailyPayment) As HtmlTable
        Dim htmTable As HtmlTable = New HtmlTable
        Dim htmTR As HtmlTableRow
        Dim htmTD As HtmlTableCell
        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim crtDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlDP As New ArrayList
        Dim nRow As Integer, nCol As Integer = 5
        Dim nAdd As Integer = 0
        Dim i As Integer, j As Integer

        crtDP.opAnd(New Criteria(GetType(DailyPayment), "ReceiptNumber", MatchType.Exact, objDP.ReceiptNumber))
        arlDP = objDPFac.Retrieve(crtDP)
        _sHelper.SetSession("arlDPWithSameReceiptNumber", arlDP)
        If arlDP.Count > 25 Then
            nAdd = IIf(arlDP.Count Mod nCol = 0, 0, 1)
            nRow = CType(arlDP.Count / nCol, Integer) + nAdd
            For i = 0 To nRow - 1
                htmTR = New HtmlTableRow
                For j = 0 To nCol - 1
                    htmTD = New HtmlTableCell
                    If (i * nCol + j) <= arlDP.Count - 1 Then
                        htmTD.InnerHtml = "- " & CType(arlDP(i * nCol + j), DailyPayment).POHeader.SONumber
                    Else
                        htmTD.InnerHtml = " "
                    End If
                    htmTR.Cells.Add(htmTD)
                Next
                htmTable.Rows.Add(htmTR)
            Next
            If nAdd <> 0 And arlDP.Count > nCol Then
                htmTR = New HtmlTableRow
                For j = 0 To nAdd - 1
                    htmTD = New HtmlTableCell
                    If (nRow * nCol + j) <= arlDP.Count - 1 Then
                        htmTD.InnerHtml = "- " & CType(arlDP(nRow * nCol + j), DailyPayment).POHeader.SONumber
                    Else
                        htmTD.InnerHtml = " "
                    End If
                    htmTR.Cells.Add(htmTD)
                Next
                htmTable.Rows.Add(htmTR)
            End If
        Else
            nCol = 5
            nRow = 5
            htmTable.Width = "100%"
            For i = 0 To nRow - 1
                htmTR = New HtmlTableRow
                For j = 0 To nCol - 1
                    htmTD = New HtmlTableCell
                    htmTD.Width = CInt(100 / nCol).ToString & "%"
                    htmTD.InnerHtml = ""
                    htmTR.Cells.Add(htmTD)
                Next
                htmTable.Rows.Add(htmTR)
            Next
            Dim idx As Integer = 0
            For i = 0 To nCol - 1
                For j = 0 To nRow - 1
                    If idx < arlDP.Count Then
                        htmTable.Rows(j).Cells(i).InnerHtml = "- " & CType(arlDP(idx), DailyPayment).POHeader.SONumber
                    Else
                        htmTable.Rows(j).Cells(i).InnerHtml = " "
                    End If
                    idx += 1
                Next
            Next
        End If

        Return htmTable
    End Function

    Private Function GetSPAName(ByVal sSPANumber As String) As String
        Dim i As Integer = 0
        Dim Rsl As String = ""

        sSPANumber = sSPANumber.Trim.Replace("Akta Notaris", "")
        For Each c As Char In sSPANumber
            If Not IsNumeric(Convert.ToString(c)) Then
                Rsl &= Convert.ToString(c)
            End If
        Next
        Return Rsl.Trim
    End Function
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Initialization()
        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect(Me._PageOpener)
    End Sub

#End Region
End Class

