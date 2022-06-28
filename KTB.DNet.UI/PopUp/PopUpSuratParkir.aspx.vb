Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.Globalization
Imports System.Text
Public Class PopUpSuratParkir
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents lblCreatedTime As System.Web.UI.WebControls.Label
    Protected WithEvents lblLetterNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents lblContactPerson As System.Web.UI.WebControls.Label
    Protected WithEvents lblHeader1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblHeader2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents lblBiayaTerbilang As System.Web.UI.WebControls.Label
    Protected WithEvents ltrTable As System.Web.UI.WebControls.Literal

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            btnPrint.Attributes("onclick") = "PrintDocument();"
            If Not IsNothing(Request.QueryString("id")) Then
                Dim objPF As ParkingFee
                objPF = New ParkingFeeFacade(User).Retrieve(CType(Request.QueryString("id"), Integer))
                MapToScreen(objPF)
            End If
        End If
    End Sub

    Private Sub MapToScreen(ByVal objPF As ParkingFee)
        Dim cultureInfo As New cultureInfo("id-ID")
        btnPrint.Attributes("onclick") = "window.print();"

        Dim critInfo As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ParkingFeeDealerInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critInfo.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFeeDealerInfo), "Dealer.ID", MatchType.Exact, objPF.Dealer.ID))
        Dim objPFDI As ParkingFeeDealerInfo

        Try
            objPFDI = New ParkingFeeDealerInfoFacade(User).Retrieve(critInfo)(0)
        Catch ex As Exception
            objPFDI = New ParkingFeeDealerInfo
        End Try

        If objPFDI.ID > 0 Then
            'lblLetterNumber.Text = objPF.ID & "/WSD-MD/" & objPF.CreatedTime.Month.ToString & "/" & objPF.CreatedTime.Year.ToString
            lblLetterNumber.Text = objPF.LetterNumber
            lblCreatedTime.Text = "Jakarta, " & objPF.CreatedTime.ToString("d MMMM yyyy", cultureInfo)
            lblDealerName.Text = objPF.Dealer.DealerName
            If Not IsNothing(objPFDI) Then
                lblAlamat.Text = objPFDI.Address
                lblKota.Text = objPFDI.City
                lblContactPerson.Text = objPFDI.Owners
            Else
                lblAlamat.Text = objPF.Dealer.Address
                lblKota.Text = objPF.Dealer.City.CityName
                lblContactPerson.Text = "-"
            End If
            ltrTable.Text = SetLiteralTable(objPF)
            lblHeader2.Text = EnumParkingFeePeriod.GetStringValue(objPF.Periode, objPF.Year)
            lblPeriode.Text = lblHeader2.Text

        Else
            MessageBox.Show("Informasi Dealer Pada Surat Penalty Parkir Belum Lengkap, Mohon Agar Menghubungi MMKSI")
            ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "window.close();", True)
            'Informasi Dealer Pada Surat Penalty Parkir Belum Lengkap, Mohon Agar Menghubungi MMKSI
        End If

    End Sub

    Private Function SetLiteralTable(ByVal objPF As ParkingFee) As String
        Dim cf As New CommonFunction
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Dealer.DealerGroup.ID", MatchType.Exact, objPF.Dealer.DealerGroup.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Year", MatchType.Exact, objPF.Year))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Periode", MatchType.Exact, objPF.Periode))
        If objPF.Dealer.DealerGroup.ID = 21 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Dealer.ID", MatchType.Exact, objPF.Dealer.ID))
        End If
        'Dim arlPF As ArrayList = New ParkingFeeFacade(User).Retrieve(criterias)
        
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(ParkingFee), "Dealer.DealerCode", Sort.SortDirection.ASC))
        Dim arlPF As ArrayList = New ParkingFeeFacade(User).Retrieve(criterias, sortColl)

        'Dim aggr As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.ParkingFee), "Amount", AggregateType.Sum)
        'Dim vInvoice As Decimal = New ParkingFeeFacade(User).RetrieveScalar(criterias, aggr)

        Dim sb As StringBuilder = New StringBuilder("")
        Dim iCount As Integer = 0
        Dim dAmount As Double = 0
        sb.Append("<table width='80%' border='1' BorderColor='#000000' cellSpacing='0' cellPadding='0'>")
        sb.Append("<TBODY>")
        sb.Append("<tr>")
        sb.Append("<td width='5%' align='center' style='HEIGHT: 29px'><STRONG>NO.</STRONG></td>")
        sb.Append("<td width='60%' align='center' style='HEIGHT: 29px'><STRONG>KETERANGAN</STRONG></td>")
        sb.Append("<td align='center' style='HEIGHT: 29px'><STRONG>JUMLAH</STRONG></td>")
        sb.Append("</tr>")
        Dim objDlr As New Dealer
        For Each item As ParkingFee In arlPF
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Dealer.DealerGroup.ID", MatchType.Exact, item.Dealer.DealerGroup.ID))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Year", MatchType.Exact, item.Year))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Periode", MatchType.Exact, item.Periode))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ParkingFee), "Dealer.ID", MatchType.Exact, item.Dealer.ID))

            Dim sortColl2 As SortCollection = New SortCollection
            sortColl2.Add(New Sort(GetType(ParkingFee), "Category.ID", Sort.SortDirection.ASC))
            Dim arlPF2 As ArrayList = New ParkingFeeFacade(User).Retrieve(criterias2, sortColl2)

            Dim aggr As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.ParkingFee), "Amount", AggregateType.Sum)
            Dim vInvoice As Decimal = New ParkingFeeFacade(User).RetrieveScalar(criterias2, aggr)

            dAmount = dAmount + item.Amount
            If objDlr.ID <> item.Dealer.ID Then
                iCount = iCount + 1
                sb.Append("<tr>")
                sb.Append("<td width='5%' align='center'>" & iCount.ToString & "</td>")
                'sb.Append("<td width='60%'>" & item.Dealer.DealerName & " - " & item.Dealer.City.CityName & "</asp:Label></td>")
                sb.Append("<td width='60%'>" & item.Dealer.DealerCode & " - " & item.Dealer.SearchTerm1 & "</asp:Label></td>")
                'sb.Append("<td align='right'>" & item.Amount.ToString("#,##0") & "</asp:Label></td>")
                sb.Append("<td align='right'>" & vInvoice.ToString("#,##0") & "</asp:Label></td>")
                sb.Append("</tr>")
                objDlr = item.Dealer
            End If
        Next
        sb.Append("<tr>")
        sb.Append("<td width='5%'>.</td>")
        sb.Append("<td width='60%'><STRONG>Grand Total</STRONG></td>")
        sb.Append("<td align='right'><STRONG>" & dAmount.ToString("#,##0") & "</asp:Label></STRONG></td>")
        sb.Append("</tr>")
        sb.Append("</TBODY>")
        sb.Append("</table>")

        Dim strTerbilang As String = "Rp. " & dAmount.ToString("#,##0")
        strTerbilang = strTerbilang & " (" & cf.TerbilangCamelCase(dAmount) & ") "
        lblBiayaTerbilang.Text = strTerbilang

        Return sb.ToString

    End Function
End Class
