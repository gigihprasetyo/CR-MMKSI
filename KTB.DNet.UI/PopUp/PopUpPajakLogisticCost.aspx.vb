Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.Globalization
Imports System.Text

Public Class PopUpPajakLogisticCost
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents lblKantorPajak As System.Web.UI.WebControls.Label
    Protected WithEvents ltlNPWP1 As System.Web.UI.WebControls.Literal
    Protected WithEvents lblNama2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat As System.Web.UI.WebControls.Label
    Protected WithEvents lblLokasi1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLokasi2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNilai2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTerbilang As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggal As System.Web.UI.WebControls.Label
    Protected WithEvents ltlNPWP2 As System.Web.UI.WebControls.Literal
    Protected WithEvents lblPejabat As System.Web.UI.WebControls.Label
    Protected WithEvents lblNilai1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomor As System.Web.UI.WebControls.Label
    Protected WithEvents lblJabatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtKota As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtPejabat As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtJabatan As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtKtrPajak As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnCetak As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblpphreturn As Label
    Protected WithEvents lblPhhdiPotong As Label
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private objPFRH As LogisticPPHHeader
    Private sessHelper As New SessionHelper

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            'btnPrint.Attributes("onclick") = "PrintDocument();"
            If Not IsNothing(Request.QueryString("id")) Then
                'Dim objPFRH As ParkingFeeReturnHeader
                objPFRH = New LogisticPPHHeaderFacade(User).Retrieve(CType(Request.QueryString("id"), Integer))
                sessHelper.RemoveSession("PFRHFAKTURPAJAK")
                If Not IsNothing(objPFRH) AndAlso objPFRH.ID > 0 Then
                    sessHelper.SetSession("PFRHFAKTURPAJAK", objPFRH)
                    MapToScreen(objPFRH)
                    txtKtrPajak.Value = objPFRH.KantorPajak
                    txtKota.Value = objPFRH.NamaKota
                    txtPejabat.Value = objPFRH.Pejabat
                    txtJabatan.Value = objPFRH.Jabatan
                    lblKantorPajak.Text = objPFRH.KantorPajak
                    lblKota.Text = objPFRH.NamaKota
                    lblPejabat.Text = objPFRH.Pejabat
                    lblJabatan.Text = objPFRH.Jabatan

                    If objPFRH.Status <> EnumPengembalianLogisticPPHStatus.PengembalianPPhStatus.Baru Then
                        txtKtrPajak.Disabled = True
                        txtKota.Disabled = True
                        txtPejabat.Disabled = True
                        txtJabatan.Disabled = True
                    End If
                End If
            End If
        End If
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtKtrPajak.Disabled = True
            txtKota.Disabled = True
            txtPejabat.Disabled = True
            txtJabatan.Disabled = True
            btnCetak.Disabled = True
        End If
    End Sub


    Private Sub MapToScreen(ByVal objPFRH As LogisticPPHHeader)
        Dim cultureInfo As New CultureInfo("id-ID")
        'btnPrint.Attributes("onclick") = "window.print();"

        lblNomor.Text = "Nomor : " & objPFRH.BuktiPotongNumber
        lblNilai1.Text = objPFRH.TotalAmount.ToString("#,##0") '100% dari total amount logisticfee confirm by BA
        lblNilai2.Text = (objPFRH.PPHAmount).ToString("#,##0")
        lblTanggal.Text = ", " & Date.Now.ToString("dd MMMM yyyy")
        lblpphreturn.Text = lblNilai1.Text
        lblPhhdiPotong.Text = lblNilai2.Text
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPajak), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(DealerPajak), "Dealer.ID", MatchType.Exact, objPFRH.Dealer.ID))
        Dim objList As ArrayList = New DealerPajakFacade(User).Retrieve(crits)
        If objList.Count > 0 Then
            Dim objDealerPajak As DealerPajak = objList(0)
            If objDealerPajak.ID > 0 Then
                'lblLetterNumber.Text = objPFRH.ID & "/WSD-MD/" & objPFRH.CreatedTime.Month.ToString & "/" & objPFRH.CreatedTime.Year.ToString
                'lblKantorPajak.Text = objDealerPajak.KPP.ToUpper
                ltlNPWP2.Text = SetLiteralNPWP(objDealerPajak)
                'lblNama2.Text = objDealerPajak.Dealer.DealerName
                Dim str As String = objDealerPajak.Dealer.DealerName
                str = str.Replace(",", ".")
                Dim i As Integer = str.IndexOf(".")
                Dim str1 As String = str.Substring(0, i)
                Dim str2 As String = str.Substring(i + 1, str.Trim.Length - i - 1)
                If str2.EndsWith(".") Then
                    lblNama2.Text = str2.Trim & " " & str1
                Else
                    lblNama2.Text = str2.Trim & ". " & str1
                End If

                ltlNPWP2.Text = SetLiteralNPWP(objDealerPajak)
                'lblNama2.Text = objDealerPajak.Dealer.DealerName
            End If
        Else
            MessageBox.Show("Data pajak belum ada. Hubungi Administrator")
        End If

        Dim cf As New CommonFunction
        Dim strTerbilang As String = " " & cf.TerbilangCamelCase(objPFRH.PPHAmount)
        lblTerbilang.Text = strTerbilang


    End Sub

    Private Function SetLiteralNPWP(ByVal objPajak As DealerPajak) As String

        Dim sb As StringBuilder = New StringBuilder("")

        Dim strNPWP As String = objPajak.NPWP
        If strNPWP.Length = 20 Then
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(0, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(1, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(2, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(3, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(4, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(5, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(6, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(7, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(8, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(9, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(10, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(11, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(12, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(13, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(14, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(15, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(16, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(17, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid; BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: #000000'>" & strNPWP.Substring(18, 1) & "</td>")
            sb.Append("<td style='TEXT-ALIGN: center; BORDER-TOP-WIDTH: thin; BORDER-RIGHT: #000000 thin solid; BORDER-LEFT: #000000 thin solid; WIDTH: 20px; BORDER-TOP: #000000 thin solid; BORDER-TOP-COLOR: #000000; BORDER-BOTTOM: #000000 thin solid'>" & strNPWP.Substring(19, 1) & "</td>")
        Else
            MessageBox.Show("Format NPWP tidak sesuai")
        End If

        Return sb.ToString

    End Function

    Private Sub btnSimpan_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.ServerClick
        objPFRH = CType(sessHelper.GetSession("PFRHFAKTURPAJAK"), LogisticPPHHeader)
        If Not IsNothing(objPFRH) AndAlso objPFRH.ID > 0 Then
            objPFRH.KantorPajak = txtKtrPajak.Value
            objPFRH.NamaKota = txtKota.Value
            objPFRH.Pejabat = txtPejabat.Value
            objPFRH.Jabatan = txtJabatan.Value
            Dim iReturn As Integer = New KTB.DNet.BusinessFacade.FinishUnit.LogisticPPHHeaderFacade(User).Update(objPFRH)
            lblKantorPajak.Text = txtKtrPajak.Value
            lblKota.Text = txtKota.Value
            lblPejabat.Text = txtPejabat.Value
            lblJabatan.Text = txtJabatan.Value

        End If
    End Sub
End Class
