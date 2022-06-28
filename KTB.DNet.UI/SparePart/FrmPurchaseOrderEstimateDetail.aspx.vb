#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessValidation.Helpers
#End Region

Public Class FrmPurchaseOrderEstimateDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSchedule As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPO As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrderType As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents cmdPrint As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotAllocAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotTax As System.Web.UI.WebControls.Label
    Protected WithEvents dgEstimateDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSO As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents lblGrandAmount As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDepositC2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents lblCaraPembayaran As System.Web.UI.WebControls.Label

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
    Private nPOID As Integer = 0
    Private sPOID As String = "0"
    Private objPOHead As SparePartPO
    Private objPOEstimate As SparePartPOEstimate = New SparePartPOEstimate
    Private objPOEstimateDetail As SparePartPOEstimateDetail
    Private sessHelper As SessionHelper = New SessionHelper
    Private arrList As ArrayList = New ArrayList
    Private totalRow As Integer = 0
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
#End Region

#Region "Custom Method"

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Private Sub retrieveHeader()
        If ViewState("FromPendingOrder") = "Yes" Then
            Dim crits As New CriteriaComposite(New Criteria(GetType(SparePartPOEstimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'crits.opAnd(New Criteria(GetType(SparePartPOEstimate), "SparePartPO.ID", MatchType.Exact, nPOID))
            crits.opAnd(New Criteria(GetType(SparePartPOEstimate), "SONumber", MatchType.Exact, sPOID))
            Dim arlList As ArrayList = New SparePartPOEstimateFacade(User).Retrieve(crits)
            If arlList.Count <> 0 Then
                objPOEstimate = arlList(0)
            Else
                objPOEstimate = Nothing
            End If
        Else
            objPOEstimate = New SparePartPOEstimateFacade(User).Retrieve(nPOID)
        End If

        If Not objPOEstimate Is Nothing Then
            lblDealerCode.Text = objPOEstimate.SparePartPO.Dealer.DealerCode
            If Not IsNothing(objPOEstimate.SparePartPO.TermOfPayment) Then
                lblCaraPembayaran.Text = objPOEstimate.SparePartPO.TermOfPayment.Description
            End If
            lblDealerName.Text = objPOEstimate.SparePartPO.Dealer.DealerName
            lblDealerTerm.Text = objPOEstimate.SparePartPO.Dealer.SearchTerm2

            lblOrderType.Text = objPOEstimate.SparePartPO.OrderTypeDesc
            lblPO.Text = objPOEstimate.SparePartPO.PONumber + " - " + objPOEstimate.SparePartPO.PODate
            lblSO.Text = objPOEstimate.SONumber + " - " + objPOEstimate.SODate
            lblSchedule.Text = Format(objPOEstimate.DeliveryDate, "dd/MM/yyyy")
            lblTotAllocAmount.Text = String.Format("{0:#,##0}", objPOEstimate.POEstimateAmount)
            Dim ppnFromDbPPNMaster = CalcHelper.GetPPNMasterByTaxTypeId(objPOEstimate.SODate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
            Dim PPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromDbPPNMaster, dpp:=lblTotAllocAmount.Text)
            lblTotTax.Text = String.Format("{0:#,##0}", PPN)
            lblDepositC2.Text = IIf(objPOEstimate.SparePartPO.OrderType = "R", String.Format("{0:#,##0}", (CDec(lblTotAllocAmount.Text) * 0.03)), 0)
            lblGrandAmount.Text = String.Format("{0:#,##0}", (CDec(lblTotAllocAmount.Text) + CDec(lblTotTax.Text) + CDec(lblDepositC2.Text)))
            sessHelper.SetSession("POEstimate", objPOEstimate)
        Else
            sessHelper.SetSession("POEstimate", Nothing)
        End If
    End Sub

    Private Sub retrieveDetails(ByVal pageIndex As Integer)

        If GetFromSession("POEstimate") Is Nothing Then
            arrList = New ArrayList
        Else
            objPOEstimate = CType(GetFromSession("POEstimate"), SparePartPOEstimate)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOEstimateDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPOEstimateDetail), "SparePartPOEstimate.ID", MatchType.Exact, objPOEstimate.ID))

            Dim oSortColl As New SortCollection
            oSortColl.Add(New Sort(GetType(SparePartPOEstimateDetail), sessHelper.GetSession("SortCols"), sessHelper.GetSession("SortDirections")))

            arrList = New SparePartPOEstimateDetailFacade(User).RetrieveByCriteria(criterias, pageIndex, dgEstimateDetail.PageSize, totalRow, oSortColl)
        End If

    End Sub

    Private Sub BindDG(ByVal pageIndex As Integer)
        retrieveDetails(pageIndex)
        If arrList.Count > 0 Then
            dgEstimateDetail.DataSource = arrList
            dgEstimateDetail.VirtualItemCount = totalRow
            dgEstimateDetail.DataBind()
        Else
            dgEstimateDetail.DataSource = arrList
            dgEstimateDetail.VirtualItemCount = 0
            dgEstimateDetail.DataBind()
            MessageBox.Show(SR.ViewFail)
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            sessHelper.SetSession("SortCols", "PartNumber")
            sessHelper.SetSession("SortDirections", Sort.SortDirection.DESC)
            If Request.QueryString("isFromPO") = "Yes" Then
                ViewState.Add("FromPendingOrder", "Yes")
                sPOID = Request.QueryString("POID").ToString().Trim() 'CType(Request.QueryString("POID"), Integer)
                retrieveHeader()
                BindDG(1)
            Else
                If Not IsNothing(Request.QueryString("POID")) Then
                    nPOID = CType(Request.QueryString("POID"), Integer)
                    retrieveHeader()
                    BindDG(1)
                End If
            End If
        End If
    End Sub

    Private Sub dgEstimateDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEstimateDetail.ItemDataBound
        If e.Item.ItemIndex > -1 Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dgEstimateDetail.PageSize * dgEstimateDetail.CurrentPageIndex)).ToString

            Dim objSparePartPOEstimateDetail As SparePartPOEstimateDetail
            objSparePartPOEstimateDetail = CType(arrList(e.Item.ItemIndex), SparePartPOEstimateDetail)
            If objSparePartPOEstimateDetail.SparePartPOEstimate.DocumentType = "N" Then
                dgEstimateDetail.Columns(6).Visible = False
                dgEstimateDetail.Columns(7).Visible = False
            End If
            'e.Item.Cells(6).Text = Math.Abs(objSparePartPOEstimateDetail.OrderQty - objSparePartPOEstimateDetail.AllocationQty)
            'e.Item.Cells(7).Text = objSparePartPOEstimateDetail.OpenQty

            'Qty-1 = Order Qty dari Inquiry = 1
            'Qty-2 = Adjustment  Qty  = nilai absolute atas selisih antara order qty dgn allocation qty  = 1 – 0 = 1
            'Qty-3 = Receive Qty = Qty-1 = 1
            'Qty-4 = Allocation Qty = qty yg dialokasikan di SO =0

            'SparePartPOEstimateDetail.OrderQty = CType(Val.Substring(69, 6).Trim(), Integer)
            'SparePartPOEstimateDetail.AllocationQty = CType(Val.Substring(76, 6).Trim(), Integer)
            'SparePartPOEstimateDetail.OpenQty = CType(Val.Substring(83, 6).Trim(), Integer)
            'SparePartPOEstimateDetail.AllocQty = CType(Val.Substring(90, 6).Trim(), Integer)

        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim tempPOEstimate As String
        objPOEstimate = CType(Session("POEstimate"), SparePartPOEstimate)
        tempPOEstimate = New SparePartPOEstimateFacade(User).UpdatePOEstimateSync(objPOEstimate)
        If tempPOEstimate = String.Empty Then
            MessageBox.Show("Data Sparepart PO berhasil diupdate!")
        ElseIf tempPOEstimate Like "Transaction Error" Then
            MessageBox.Show("Transaksi Gagal!")
        Else
            MessageBox.Show("Sparepart dengan nomor " + tempPOEstimate + " tidak ada!")

        End If

        sessHelper.RemoveSession("POEstimate")
    End Sub

    Private Sub dgEstimateDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgEstimateDetail.PageIndexChanged
        dgEstimateDetail.CurrentPageIndex = e.NewPageIndex
        BindDG(e.NewPageIndex + 1)
    End Sub

#End Region

    Private Sub dgEstimateDetail_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgEstimateDetail.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortCols") Then
            If sessHelper.GetSession("SortDirections") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirections", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirections", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortCols", e.SortExpression)
        dgEstimateDetail.SelectedIndex = -1
        dgEstimateDetail.CurrentPageIndex = 0
        BindDG(dgEstimateDetail.CurrentPageIndex)
    End Sub
End Class