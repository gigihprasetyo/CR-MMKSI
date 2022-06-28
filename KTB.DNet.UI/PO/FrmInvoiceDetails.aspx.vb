#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

Public Class FrmInvoiceDetails
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCodeValue As System.Web.UI.WebControls.Label
    Protected WithEvents label66 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCityValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNameValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblContractNumberValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRegPO As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoInvoice As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisMOValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDailyPONumberValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblReqAllocValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTermOfPaymentValue As System.Web.UI.WebControls.Label
    Protected WithEvents dtgDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglInvoice As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoSO As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPengajuanPO As System.Web.UI.WebControls.Label
    Protected WithEvents label As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesOrgValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents lblProductYearValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents lblProjectNameValue As System.Web.UI.WebControls.Label
    Protected WithEvents label24 As System.Web.UI.WebControls.Label
    Protected WithEvents Total As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrderTypeValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalAmountValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Dim objVDetailInvoice As VDetailInvoice
    Dim objVInvoice As VInvoice
    Dim arlInvoiceDetail = New ArrayList
    Dim objInvoiceDetail = New VSumDetailInvoice
    'Dim objInvoiceHeader = New InvoiceHeader
    Dim objVechileColor = New VechileColor
    'Dim objPODetail As PODetail    
    Dim TotalHarga As Double = 0
    Dim TotalPPH As Double = 0
    Dim TotalInterest As Double = 0
    Dim criterias As CriteriaComposite
    Private sessionHelper As New sessionHelper
    Dim dblTotalHargaTebus As Double
#End Region

#Region "Custom Method"

    Private Sub GetVInvoice()
        'Dim VInvoiceid As String = Request.QueryString("id")
        'objVDetailInvoice = New VDetailInvoiceFacade(User).Retrieve(CInt(VInvoiceid))
        'objVInvoice = New VInvoiceFacade(User).Retrieve(CInt(VInvoiceid))
        'criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.InvoiceDetail), "InvoiceHeader.ID", MatchType.Exact, VInvoiceid))
        'arlInvoiceDetail = New InvoiceDetailFacade(User).Retrieve(criterias)
    End Sub

    Private Sub BindHeaderToForm()
        lblDealerCodeValue.Text = objVDetailInvoice.DealerCode & "/" & objVDetailInvoice.SearchTerm1
        lblNameValue.Text = objVDetailInvoice.DealerName
        lblNoInvoice.Text = objVInvoice.InvoiceNumber
        lblTglInvoice.Text = objVInvoice.InvoiceDate
        lblDailyPONumberValue.Text = objVDetailInvoice.DealerPONumber
        lblNoSO.Text = objVDetailInvoice.SONumber
        lblTglPengajuanPO.Text = Format(objVDetailInvoice.TglPengajuan, "dd/MM/yyyy")
        lblReqAllocValue.Text = Format(objVDetailInvoice.ReqAllocationDateTime, "dd/MM/yyyy")
        lblTermOfPaymentValue.Text = objVDetailInvoice.Description
        lblCityValue.Text = objVDetailInvoice.CityName
        lblContractNumberValue.Text = objVDetailInvoice.ContractNumber
        lblJenisMOValue.Text = CType(objVDetailInvoice.ContractType, enumOrderType.OrderType).ToString
        lblSalesOrgValue.Text = objVDetailInvoice.CategoryCode
        lblProductYearValue.Text = objVDetailInvoice.ProductionYear
        lblProjectNameValue.Text = objVDetailInvoice.ProjectName
        lblOrderTypeValue.Text = CType(objVDetailInvoice.POType, LookUp.EnumJenisOrder).ToString

    End Sub

    Private Sub BindDetailToGrid()
        dtgDetail.DataSource = arlInvoiceDetail
        dtgDetail.DataBind()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            GetVInvoice()
            BindHeaderToForm()
            dblTotalHargaTebus = 0
            BindDetailToGrid()
            TotalHargaTebus()
            If Not SecurityProvider.Authorize(Context.User, SR.PengajuanPOView_Detail) Then
                Response.Redirect("../frmAccessDenied.aspx?modulName=Detail PO")
            End If
        End If
    End Sub

    Private Sub dtgDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDetail.ItemDataBound
        'If Not (arlInvoiceDetail Is Nothing) Then
        '    If Not (e.Item.ItemIndex = -1) Then
        '        objInvoiceDetail = CType(arlInvoiceDetail(e.Item.ItemIndex), InvoiceDetail)
        '        e.Item.Cells(0).Text = objInvoiceDetail.ID
        '        e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgDetail.PageSize * dtgDetail.CurrentPageIndex)).ToString
        '        e.Item.Cells(2).Text = objInvoiceDetail.VechileColor.MaterialNumber
        '        e.Item.Cells(3).Text = objInvoiceDetail.VechileColor.MaterialDescription
        '        e.Item.Cells(4).Text = objInvoiceDetail.BilledQty.ToString
        '        e.Item.Cells(5).Text = FormatNumber(objInvoiceDetail.ItemAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        '        TotalHarga += CType(e.Item.Cells(5).Text, Double)
        '        e.Item.Cells(6).Text = FormatNumber(objInvoiceDetail.PPH22, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        '        TotalPPH += CType(e.Item.Cells(6).Text, Double)
        '        e.Item.Cells(7).Text = FormatNumber(objInvoiceDetail.Interest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        '        TotalInterest += CType(e.Item.Cells(7).Text, Double)
        '        Dim DblSubTotal As Double
        '        DblSubTotal = objInvoiceDetail.ItemAmount + objInvoiceDetail.PPH22 + objInvoiceDetail.Interest
        '        e.Item.Cells(8).Text = FormatNumber(DblSubTotal, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        '        dblTotalHargaTebus = dblTotalHargaTebus + DblSubTotal

        '    End If
        'End If

        'If e.Item.ItemType = ListItemType.Footer Then
        '    ' e.Item.Cells(3).Text = "Sub Total : "
        '    e.Item.Cells(5).Text = FormatNumber(TotalHarga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        '    e.Item.Cells(6).Text = FormatNumber(TotalPPH, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        '    e.Item.Cells(7).Text = FormatNumber(TotalInterest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        'End If

    End Sub

    Private Sub TotalHargaTebus()
        'If Not IsNothing(arlInvoiceDetail) Then
        '    Dim tot As Double = 0           
        '    For Each item As InvoiceDetail In arlInvoiceDetail
        '        tot += item.ItemAmount
        '        'totQty += item.TotalQuantity
        '    Next
        lblTotalAmountValue.Text = FormatNumber(dblTotalHargaTebus, 0, , , TriState.UseDefault)
        'lblQuantity.Text = FormatNumber(totQty, 0, , , TriState.UseDefault) & " Unit"
        'End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub
End Class
