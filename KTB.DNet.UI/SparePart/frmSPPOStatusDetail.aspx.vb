#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports Ktb.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
#End Region

#Region "DotNet Namespace"
Imports System.IO
Imports System.Text
#End Region

Public Class frmSPPOStatusDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents dtgSPPOStatusDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblOrderType As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrder As System.Web.UI.WebControls.Label
    Protected WithEvents lblKTB As System.Web.UI.WebControls.Label
    Protected WithEvents lblInvoice As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalNilaiTagihan As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private _nSPPOStatusID As Integer = 0
    Private _nDealerID As Integer = 0
    Private _objSPPOStatus As SparePartPOStatus
    Private _objSPPOStatusDetail As SparePartPOStatusDetail
    Private _objSPPOStatusDetails As ArrayList
    Private _nTotalNilaiTagihan As Decimal = 0
#End Region

#Region "Custom Method"

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Private Sub GetSPPOStatus()
        _objSPPOStatus = New SparePartPOStatusFacade(User).Retrieve(_nSPPOStatusID)
        If _objSPPOStatus Is Nothing Then
            _sessHelper.SetSession("sesSPPOStatus", Nothing)
        Else
            _sessHelper.SetSession("sesSPPOStatus", _objSPPOStatus)
        End If
    End Sub

    Private Sub BindSPPOStatusHeader()

        If GetFromSession("sesSPPOStatus") Is Nothing Then
            Me.lblDealerCode.Text = String.Empty
            Me.lblDealerName.Text = String.Empty
            Me.lblDealerTerm.Text = String.Empty
            Me.lblOrderType.Text = String.Empty
            Me.lblOrder.Text = String.Empty
            Me.lblKTB.Text = String.Empty
            Me.lblInvoice.Text = String.Empty

        Else
            _objSPPOStatus = CType(Session("sesSPPOStatus"), SparePartPOStatus)
            If CType(Session("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                Me.lblDealerCode.Text = CType(Session("DEALER"), Dealer).DealerCode
                Me.lblDealerName.Text = CType(Session("DEALER"), Dealer).DealerName
                Me.lblDealerTerm.Text = CType(Session("DEALER"), Dealer).SearchTerm2
            Else
                Me.lblDealerCode.Text = _objSPPOStatus.SparePartPO.Dealer.DealerCode
                Me.lblDealerName.Text = _objSPPOStatus.SparePartPO.Dealer.DealerName
                Me.lblDealerTerm.Text = _objSPPOStatus.SparePartPO.Dealer.SearchTerm2
            End If

            Me.lblOrderType.Text = _objSPPOStatus.SparePartPO.OrderTypeDesc
            Me.lblOrder.Text = _objSPPOStatus.SparePartPO.PONumber & " - " & _
            Format(_objSPPOStatus.SparePartPO.PODate, "dd/MM/yyyy")

            Try
                Me.lblKTB.Text = _objSPPOStatus.SONumber & " - " & _
                         Format(_objSPPOStatus.SODate, "dd/MM/yyyy")
            Catch ex As Exception
                Me.lblKTB.Text = ""
            End Try
            Try
                Me.lblInvoice.Text = _objSPPOStatus.BillingNumber & " - " & _
                                                 IIf(Format(_objSPPOStatus.BillingDate, "dd/MM/yyyy") = "01/01/1753", _
                                                     "", Format(_objSPPOStatus.BillingDate, "dd/MM/yyyy"))
            Catch ex As Exception
                Me.lblInvoice.Text = ""
            End Try
        End If
    End Sub

    Private Sub BindTodtgPOStatusDetail(ByVal pageIndex As Integer)
        If GetFromSession("sesSPPOStatus") Is Nothing Then

            _objSPPOStatusDetails = New ArrayList
            If _objSPPOStatusDetails.Count > 0 Then
                dtgSPPOStatusDetail.DataSource = _objSPPOStatusDetails
                dtgSPPOStatusDetail.VirtualItemCount = 0
                dtgSPPOStatusDetail.DataBind()
                MessageBox.Show(SR.ViewFail)
            End If
        Else
            _objSPPOStatus = CType(Session("sesSPPOStatus"), SparePartPOStatus)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.ID", MatchType.Exact, _objSPPOStatus.ID))
            Dim totalRow As Integer = 0
            Dim aggr As Aggregate = New Aggregate(GetType(SparePartPOStatusDetail), "BillingPrice", AggregateType.Sum)

            _nTotalNilaiTagihan = New SparePartPOStatusDetailFacade(User).RetrieveScalar(criterias, aggr)
            Me.lblTotalNilaiTagihan.Text = String.Format("{0:#,##0}", _nTotalNilaiTagihan)

            _objSPPOStatusDetails = New SparePartPOStatusDetailFacade(User).RetrieveActiveList(criterias, pageIndex, dtgSPPOStatusDetail.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
            If _objSPPOStatusDetails.Count > 0 Then
                dtgSPPOStatusDetail.DataSource = _objSPPOStatusDetails
                dtgSPPOStatusDetail.VirtualItemCount = totalRow
                dtgSPPOStatusDetail.DataBind()
            Else
                dtgSPPOStatusDetail.DataSource = New ArrayList
                dtgSPPOStatusDetail.VirtualItemCount = 0
                dtgSPPOStatusDetail.DataBind()
                MessageBox.Show(SR.ViewFail)
            End If
        End If

    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            ViewState("currSortColumn") = ""
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            _nSPPOStatusID = CType(Request.QueryString("SPPOStatusID"), Integer)
            _nDealerID = CType(Session("DEALER"), Dealer).ID

            If Not IsNothing(Request.QueryString("SPPOStatusID")) Then
                If Not IsNothing(Session("DEALER")) Then
                    GetSPPOStatus()
                    BindSPPOStatusHeader()
                    BindTodtgPOStatusDetail(1)
                End If
            End If
        End If
    End Sub

    Private Sub dtgSPPOStatusDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPPOStatusDetail.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgSPPOStatusDetail.PageSize * (dtgSPPOStatusDetail.CurrentPageIndex))).ToString
        End If
    End Sub

    Private Sub dtgSPPOStatusDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSPPOStatusDetail.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        dtgSPPOStatusDetail.CurrentPageIndex = 0
        BindTodtgPOStatusDetail(dtgSPPOStatusDetail.CurrentPageIndex + 1)
    End Sub

    Private Sub dtgSPPOStatusDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSPPOStatusDetail.PageIndexChanged
        dtgSPPOStatusDetail.CurrentPageIndex = e.NewPageIndex
        BindTodtgPOStatusDetail(e.NewPageIndex + 1)
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        _sessHelper.RemoveSession("sesSPPO")
    End Sub

#End Region



End Class