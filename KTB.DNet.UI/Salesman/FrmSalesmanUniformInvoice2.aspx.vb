Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class FrmSalesmanUniformInvoice2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNote As System.Web.UI.WebControls.TextBox
    Protected WithEvents ltrlKodePSeragam As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrlDeskripsi As System.Web.UI.WebControls.Literal
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtKwitansi As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarai"
    Dim sHelper As New SessionHelper
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            'Dim id As Integer = CInt(Request.QueryString("id"))
            'Dim StrIdTmp As String()
            'Dim strIdAwal As String
            '' hanya mengambil data awalnya saja
            'If Not IsNothing(Request.QueryString("id")) Then
            '    StrIdTmp = Request.QueryString("id").Split(";")
            '    strIdAwal = StrIdTmp(0)
            'End If

            If Not IsNothing(Request.QueryString("id")) Then
                'Dim objSalesmanUnifOrderHeader As SalesmanUniformOrderHeader = New SalesmanUniformOrderHeaderFacade(User).Retrieve(CType(strIdAwal, Integer))
                'ltrlKodePSeragam.Text = objSalesmanUnifOrderHeader.SalesmanUnifDistribution.SalesmanUnifDistributionCode
                'ltrlDeskripsi.Text = objSalesmanUnifOrderHeader.SalesmanUnifDistribution.Description
                'ltrlNoOrder.Text = objSalesmanUnifOrderHeader.OrderNumber
                Dim arrHeader As ArrayList
                Dim criterias As CriteriaComposite
                criterias = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "ID", MatchType.InSet, CommonFunction.GetStrValue(Request.QueryString("id"), ";", ",")))
                arrHeader = New SalesmanUniformOrderHeaderFacade(User).Retrieve(criterias)

                If arrHeader.Count > 0 Then
                    For Each itemHeader As SalesmanUniformOrderHeader In arrHeader
                        ltrlKodePSeragam.Text = itemHeader.SalesmanUnifDistribution.SalesmanUnifDistributionCode
                        ltrlDeskripsi.Text = itemHeader.SalesmanUnifDistribution.Description
                        txtKwitansi.Text = itemHeader.InvoiceNo
                        Exit For
                    Next
                End If

                If txtKwitansi.Text <> String.Empty Then
                    txtKwitansi.ReadOnly = True
                Else
                    txtKwitansi.ReadOnly = False
                End If

                sHelper.SetSession("arrSalesmanUniformOrderHeader", arrHeader)
                ' 05-Oct-2007   Deddy H     change request,refer to bug 792
                txtDescription.Text = KTB.DNet.Lib.WebConfig.GetValue("InvWord01") & Year(Now) & "."
                txtNote.Text = KTB.DNet.Lib.WebConfig.GetValue("InvWord02")
            End If
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtDescription.Text <> String.Empty Then
            Dim arrHeader As ArrayList = CType(sHelper.GetSession("arrSalesmanUniformOrderHeader"), ArrayList)
            Dim strDesc As String = Server.HtmlEncode(txtDescription.Text)
            Dim strNote As String = txtNote.Text

            If txtKwitansi.Text = String.Empty Then
                MessageBox.Show("Silahkan isi no kwintasinya")
                Return
            End If

            For Each item As SalesmanUniformOrderHeader In arrHeader
                item.Description = strDesc
                item.Note = strNote
                item.InvoiceNo = txtKwitansi.Text
            Next

            ' lalu mengupdate kumpulan header yang bersangkutan
            Try
                Dim iresult As Integer = New SalesmanUniformOrderHeaderFacade(User).Update(arrHeader)
                If iresult <> -1 Then
                    If Not IsNothing(Request.QueryString("id")) Then
                        Response.Redirect("FrmSalesmanInvoiceReportPrint.aspx?id=" & Request.QueryString("id"))
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MessageBox.Show("Silahkan isi Field Untuk Pembayaran")
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        sHelper.SetSession("ModeBack", True)
        Response.Redirect("FrmSalesmanUniformInvoice.aspx")
    End Sub
End Class
