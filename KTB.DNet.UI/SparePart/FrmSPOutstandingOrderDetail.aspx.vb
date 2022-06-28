#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmSPOutstandingOrderDetail
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
    Protected WithEvents lblCaraPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents lblPO As System.Web.UI.WebControls.Label
    Protected WithEvents lblDocumentType As System.Web.UI.WebControls.Label
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
    Private objPOHead As SparePartPO
    Private objPOOutstanding As SparePartOutstandingOrder = New SparePartOutstandingOrder
    Private objPOOutstandingDetail As SparePartOutstandingOrderDetail
    Private sessHelper As SessionHelper = New SessionHelper
    Private arrList As ArrayList = New ArrayList
    Private totalRow As Integer = 0
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
            Dim crits As New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(SparePartOutstandingOrder), "SparePartPO.ID", MatchType.Exact, nPOID))
            Dim arlList As ArrayList = New SparePartOutstandingOrderFacade(User).Retrieve(crits)
            If arlList.Count <> 0 Then
                objPOOutstanding = arlList(0)
            Else
                objPOOutstanding = Nothing
            End If
        Else
            objPOOutstanding = New SparePartOutstandingOrderFacade(User).Retrieve(nPOID)
        End If

        If Not objPOOutstanding Is Nothing Then
            lblDealerCode.Text = objPOOutstanding.SparePartPO.Dealer.DealerCode
            lblDealerName.Text = objPOOutstanding.SparePartPO.Dealer.DealerName
            lblDealerTerm.Text = objPOOutstanding.SparePartPO.Dealer.SearchTerm2

            lblOrderType.Text = objPOOutstanding.SparePartPO.OrderTypeDesc
            lblPO.Text = objPOOutstanding.SparePartPO.PONumber + " - " + objPOOutstanding.SparePartPO.PODate

            If Not IsNothing(objPOOutstanding.SparePartPO.TermOfPayment) Then
                lblCaraPembayaran.Text = objPOOutstanding.SparePartPO.TermOfPayment.Description
            End If

            For Each liOrderType As ListItem In LookUp.ArraySPDocumentTypeKTBDealer 'LookUp.ArraySPOrderType
                If objPOOutstanding.DocumentType.Equals(liOrderType.Value) Then
                    lblDocumentType.Text = liOrderType.Text
                    Exit For
                End If
            Next
            'lblSO.Text = objPOOutstanding.SONumber '+ " - " + objPOOutstanding.PODate
            sessHelper.SetSession("POOutstanding", objPOOutstanding)
        Else
            sessHelper.SetSession("POOutstanding", Nothing)
        End If
    End Sub

    Private Function CalculatePOEstimateAmount(ByVal arlPODetail As ArrayList) As Decimal
        Dim nPOAmount As Decimal = 0
        For Each objPOEstimateDetail As SparePartOutstandingOrderDetail In arlPODetail
            If IsNothing(objPOEstimateDetail.SparePart) Then
                objPOEstimateDetail.SparePart = New SparePartMasterFacade(User).Retrieve(objPOEstimateDetail.PartNumber)
            End If
            nPOAmount = nPOAmount + (objPOEstimateDetail.AllocationQty * objPOEstimateDetail.SparePart.RetalPrice)
        Next
        Return (nPOAmount)
    End Function
    Private Sub retrieveDetails(ByVal pageIndex As Integer)

        If GetFromSession("POOutstanding") Is Nothing Then
            arrList = New ArrayList
        Else
            objPOOutstanding = CType(GetFromSession("POOutstanding"), SparePartOutstandingOrder)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.ID", MatchType.Exact, objPOOutstanding.ID))
            arrList = New SparePartOutstandingOrderDetailFacade(User).RetrieveByCriteria(criterias, pageIndex, dgEstimateDetail.PageSize, totalRow)
        End If

    End Sub

    Private Sub BindDG(ByVal pageIndex As Integer)
        retrieveDetails(pageIndex)
        If arrList.Count > 0 Then
            CalculatePOEstimateAmount(arrList)
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
            If Request.QueryString("isFromPO") = "Yes" Then
                ViewState.Add("FromPendingOrder", "Yes")
                nPOID = CType(Request.QueryString("POID"), Integer)
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
        End If
    End Sub

    Private Sub dgEstimateDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgEstimateDetail.PageIndexChanged
        dgEstimateDetail.CurrentPageIndex = e.NewPageIndex
        BindDG(e.NewPageIndex + 1)
    End Sub

#End Region

End Class