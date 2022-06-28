#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Linq
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmSPOutstandingOrderDetailExtendBO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgExtendBO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private sessFrmSPOutstandingOrderDetailExtendBO As String = "FrmSPOutstandingOrderDetailExtendBO"

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
            sessHelper.SetSession("POOutstanding", objPOOutstanding)
        Else
            sessHelper.SetSession("POOutstanding", Nothing)
        End If
    End Sub

    Private Sub retrieveDetails(ByVal pageIndex As Integer)
        Dim arlSPO As New ArrayList
        If GetFromSession("POOutstanding") Is Nothing Then
            arlSPO = New ArrayList
        Else
            objPOOutstanding = CType(GetFromSession("POOutstanding"), SparePartOutstandingOrder)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.ID", MatchType.Exact, objPOOutstanding.ID))
            arlSPO = New SparePartOutstandingOrderDetailFacade(User).RetrieveByCriteria(criterias, pageIndex, dgExtendBO.PageSize, totalRow)
        End If
        arlSPO = New System.Collections.ArrayList((From item As SparePartOutstandingOrderDetail In arlSPO.OfType(Of SparePartOutstandingOrderDetail)()
                                                    Order By item.SparePartOutstandingOrder.SparePartPO.PONumber, item.PartNumber
                                            Select item).ToList())

        Dim strPONumber As String = String.Empty
        Dim strPartNumber As String = String.Empty
        arrList = New ArrayList
        For Each obj As SparePartOutstandingOrderDetail In arlSPO
            If obj.SparePartOutstandingOrder.SparePartPO.PONumber <> strPONumber OrElse obj.PartNumber <> strPartNumber Then
                strPONumber = obj.SparePartOutstandingOrder.SparePartPO.PONumber
                strPartNumber = obj.PartNumber
                arrList.Add(obj)
            End If
        Next
        sessHelper.SetSession(sessFrmSPOutstandingOrderDetailExtendBO, arrList)
    End Sub

    Private Sub BindDG(ByVal pageIndex As Integer)
        retrieveDetails(pageIndex)
        If arrList.Count > 0 Then
            CalculatePOEstimateAmount(arrList)
            dgExtendBO.DataSource = arrList
            dgExtendBO.VirtualItemCount = totalRow
            dgExtendBO.DataBind()
        Else
            dgExtendBO.DataSource = arrList
            dgExtendBO.VirtualItemCount = 0
            dgExtendBO.DataBind()
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

    Private Sub dgExtendBO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgExtendBO.ItemDataBound
        If e.Item.ItemIndex > -1 Then
            Dim obj As SparePartOutstandingOrderDetail = CType(e.Item.DataItem, SparePartOutstandingOrderDetail)
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dgExtendBO.PageSize * dgExtendBO.CurrentPageIndex)).ToString
            Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
            If obj.Status = 0 OrElse obj.Status = 2 Then   'Belum diperpanjang atau Tidak di perpanjang
                chkSelect.Checked = False
            Else
                'Diperpanjang
                chkSelect.Checked = True
            End If
            If obj.Status = 1 OrElse obj.IsTransfer = 1 Then   'jika diperpanjang atau sudah ditransfer
                chkSelect.Enabled = False
            Else
                'Belum diperpanjang atau Tidak di perpanjang
                chkSelect.Enabled = True
            End If
        End If
    End Sub

    Private Sub dgExtendBO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgExtendBO.PageIndexChanged
        dgExtendBO.CurrentPageIndex = e.NewPageIndex
        BindDG(e.NewPageIndex + 1)
    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        If dgExtendBO.Items.Count = 0 Then
            MessageBox.Show("Tidak ada data yang di perpanjang")
            Exit Sub
        End If
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession(sessFrmSPOutstandingOrderDetailExtendBO), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer = 0
        For Each dtgItem As DataGridItem In dgExtendBO.Items
            nIndeks = dtgItem.ItemIndex
            Dim objSPPODtl As SparePartOutstandingOrderDetail = CType(arrGrid(nIndeks), SparePartOutstandingOrderDetail)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.ID", MatchType.Exact, objSPPODtl.SparePartOutstandingOrder.ID))
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "PartNumber", MatchType.Exact, objSPPODtl.PartNumber))
            Dim arlSPO As ArrayList = New SparePartOutstandingOrderDetailFacade(User).Retrieve(criterias)
            If Not IsNothing(arlSPO) AndAlso arlSPO.Count > 0 Then
                For Each objSPO As SparePartOutstandingOrderDetail In arlSPO
                    If CType(dtgItem.Cells(1).FindControl("chkSelect"), CheckBox).Checked Then
                        objSPO.Status = 1  'Status di perpanjang
                    Else
                        objSPO.Status = 2   'Status di tidak perpanjang
                    End If
                    arlCheckedItem.Add(objSPO)
                Next
            End If
        Next
        Dim _result = -1
        Dim ArrUpdateError As New ArrayList
        For Each item As SparePartOutstandingOrderDetail In arlCheckedItem
            _result = New SparePartOutstandingOrderDetailFacade(User).Update(item)
            If _result = -1 Then
                ArrUpdateError.Add(item)
            End If
        Next

        If ArrUpdateError.Count = arlCheckedItem.Count Then
            MessageBox.Show("Proses Gagal")
        Else
            If ArrUpdateError.Count = 0 Then
                Dim code As String = "<script>refreshingParent();</script>"
                RegisterStartupScript("OpenWindow", code)
            Else
                Dim strPONumber As String = String.Empty
                For Each item As SparePartOutstandingOrderDetail In ArrUpdateError
                    If strPONumber = "" Then
                        strPONumber = item.SparePartOutstandingOrder.SparePartPO.PONumber
                    Else
                        strPONumber += ", " & item.SparePartOutstandingOrder.SparePartPO.PONumber
                    End If
                Next
                Dim strMsg As String = ""
                strMsg = "Proses Perpanjangan Back Order Sukses.\nProses yang gagal adalah Nomor Pesanan:\n"
                strMsg += strPONumber
                MessageBox.Show(strMsg)
            End If
        End If
        BindDG(0)
    End Sub

#End Region

#Region "Class"
    Private Class SparePartOutstandingOrderDetailExtendBO
        Private _pONumber As String = String.Empty
        Private _partNumber As String = String.Empty
        Private _partName As String = String.Empty
        Private _orderQty As Integer = 0
        Private _allocationQty As Integer = 0
        Private _openQty As Integer = 0
        Private _validTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property

        Public Property PartNumber As String
            Get
                Return _partNumber
            End Get
            Set(ByVal value As String)
                _partNumber = value
            End Set
        End Property

        Public Property PartName As String
            Get
                Return _partName
            End Get
            Set(ByVal value As String)
                _partName = value
            End Set
        End Property

        Public Property OrderQty As Integer
            Get
                Return _orderQty
            End Get
            Set(ByVal value As Integer)
                _orderQty = value
            End Set
        End Property

        Public Property AllocationQty As Integer
            Get
                Return _allocationQty
            End Get
            Set(ByVal value As Integer)
                _allocationQty = value
            End Set
        End Property

        Public Property OpenQty As Integer
            Get
                Return _openQty
            End Get
            Set(ByVal value As Integer)
                _openQty = value
            End Set
        End Property

        Public Property ValidTo As DateTime
            Get
                Return _validTo
            End Get
            Set(ByVal value As DateTime)
                _validTo = value
            End Set
        End Property
    End Class
#End Region

End Class