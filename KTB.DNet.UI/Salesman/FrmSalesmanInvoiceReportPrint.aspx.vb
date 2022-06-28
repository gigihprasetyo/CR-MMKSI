Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.Globalization

Public Class FrmSalesmanInvoiceReportPrint
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ltrlTotalTerbilang As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlDescription As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlNote As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlCurrentDate As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlDealerName As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlTotalHarga As System.Web.UI.WebControls.Literal
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents ltrNoKwitansi As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlDealerPlace As System.Web.UI.WebControls.Literal

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


#Region "Custom Method"
    Private Sub MapToScreen(ByVal objDomain As SalesmanUniformOrderHeader)
        Dim cultureInfo As New cultureInfo("id-ID")
        btnPrint.Attributes("onclick") = "window.print();"
        Dim cf As New CommonFunction
        ltrNoKwitansi.Text = objDomain.InvoiceNo
        ltrlDescription.Text = Server.HtmlDecode(objDomain.Description).Replace(Chr(13), "<BR>").Replace(Chr(10), "")
        ltrlDealerName.Text = objDomain.Dealer.DealerName
        ltrlCurrentDate.Text = Date.Now.ToString("d MMMM yyyy", cultureInfo)
        ltrlNote.Text = objDomain.Note
        'Dim totalHarga As Double = dblTotalHarga 'Math.Round(objDomain.TotalHarga + (0.1 * objDomain.TotalHarga))
        ltrlTotalHarga.Text = dblTotalHarga.ToString("#,##0") 'totalHarga.ToString("#,##0")
        ltrlTotalTerbilang.Text = cf.Terbilang(dblTotalHarga)
        ltrlDealerPlace.Text = objDomain.Dealer.City.CityName
    End Sub

    Private Sub GenerateInvoice(ByRef objDomain As SalesmanUniformOrderHeader)
        ' objDomain.InvoiceNo = "request"
        ' Dim result As Integer = New SalesmanUniformOrderHeaderFacade(User).Update(objDomain)
        'objDomain = New SalesmanUniformOrderHeaderFacade(User).Retrieve(objDomain.ID)
        ' melakukan update secara banyak
        UpdateGenerateInvoice(objDomain.InvoiceNo)
    End Sub

    Private Sub UpdateGenerateInvoice(ByVal strInvoiceNo As String)
        If Not IsNothing(sHelper.GetSession("arrSalesmanUniformOrderHeader")) Then
            Dim arrHeader As ArrayList = CType(sHelper.GetSession("arrSalesmanUniformOrderHeader"), ArrayList)
            dblTotalHarga = 0
            For Each item As SalesmanUniformOrderHeader In arrHeader
                item.InvoiceNo = strInvoiceNo
                dblTotalHarga = dblTotalHarga + Math.Round(item.TotalHarga + (0.1 * item.TotalHarga))
            Next
            Dim result As Integer = New SalesmanUniformOrderHeaderFacade(User).Update(arrHeader)
        End If
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            'Dim id As Integer = CInt(Request.QueryString("id"))


            Dim strIdTmp As String()
            Dim strIdAwal As String
            Dim objSalesUnifHeaderOrder As SalesmanUniformOrderHeader
            If Not IsNothing(Request.QueryString("id")) Then
                strIdTmp = Request.QueryString("id").Split(";")
                strIdAwal = strIdTmp(0)
            End If

            If strIdAwal <> "" Then
                objSalesUnifHeaderOrder = New SalesmanUniformOrderHeaderFacade(User).Retrieve(CType(strIdAwal, Integer))
            End If

            ' Menghasilkan 1 no Invoice dahulu, baru update yg lainnya, dgn no Invoice yg sama

            ' Generate Invoice
            ' If objSalesUnifHeaderOrder.InvoiceNo = "" Then
            GenerateInvoice(objSalesUnifHeaderOrder)
            'End If

        ' set data
        MapToScreen(objSalesUnifHeaderOrder)
        End If
    End Sub

End Class
