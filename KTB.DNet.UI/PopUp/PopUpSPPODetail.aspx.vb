#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class PopUpSPPODetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgPODetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotPOAmount As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack1 As System.Web.UI.WebControls.Button
    Protected WithEvents LblPODate As System.Web.UI.WebControls.Label
    Protected WithEvents lblTOP As System.Web.UI.WebControls.Label


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
    Private _arlPODetail As ArrayList = New ArrayList
    Private _sesshelper As SessionHelper = New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub InitialEditPage(ByVal nPOID As Integer)
        'If Not IsNothing(Session("DEALER")) Then
        BindOrderType()
        DisplayTransactionResult(nPOID)
        'Else
        '    Response.Redirect("../SessionExpired.htm")
        'End If
    End Sub

    Private Sub BindOrderType()
        ddlOrderType.Items.Clear()
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer
            ddlOrderType.Items.Insert(0, New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        ddlOrderType.DataBind()
    End Sub


    Private Sub BindPODetail()
        _arlPODetail = Session("sessPODetail")
        lblTotPOAmount.Text = String.Format("{0:#,##0}", CalculatePOAmount(_arlPODetail))
        dtgPODetail.DataSource = _arlPODetail
        dtgPODetail.DataBind()
    End Sub

    Private Function CalculatePOAmount(ByVal arlPODetail As ArrayList) As Decimal
        Dim nPOAmount As Decimal = 0
        For Each objPODetail As SparePartPODetail In arlPODetail
            nPOAmount = nPOAmount + objPODetail.Amount
        Next
        Return (nPOAmount)
    End Function




    Private Function DisplayTransactionResult(ByVal nID As Integer)
        Dim stt As Boolean = False
        Dim objPO As SparePartPO = New SparePartPOFacade(User).Retrieve(nID)
        ddlOrderType.Enabled = False
        'icOrderDate.Enabled = False

        txtPONumber.Text = objPO.PONumber
        lblDealerCode.Text = objPO.Dealer.DealerCode
        lblDealerName.Text = objPO.Dealer.DealerName
        lblDealerTerm.Text = objPO.Dealer.SearchTerm2
        LblPODate.Text = String.Format("{0:dd/MM/yyyy}", objPO.PODate)
        'icOrderDate.Value = objPO.PODate 'String.Format("{0:dd/MM/yyyy}", objPO.PODate)
        ddlOrderType.SelectedValue = objPO.OrderType
        If Not IsNothing(objPO.TermOfPayment) Then
            lblTOP.Text = objPO.TermOfPayment.Description
        End If
        _arlPODetail = objPO.SparePartPODetails
        _sesshelper.SetSession("sessPODetail", _arlPODetail)
        BindPODetail()
    End Function

    Private Function EditPO() As Integer
        Dim ObjPO As SparePartPO = CType(Session("sessPOHeader"), SparePartPO)
        Return New SparePartPOFacade(User).UpdateSparePartPO(ObjPO, CType(Session("sessPODetail"), ArrayList))
    End Function






#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack() Then
            If Not IsNothing(Request.QueryString("poid")) Then
                InitialEditPage(CType(Request.QueryString("poID"), Integer))
            End If

        End If
    End Sub

    Private Sub dtgPODetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPODetail.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            SetDtgPODetailItem(e)
        End If

    End Sub


    Private Sub SetDtgPODetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Add(lNum)
    End Sub




    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
             ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        pCompletelist.Sort(objListComparer)

    End Sub

    Private Sub dtgPODetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPODetail.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        Dim arlCompletelist As ArrayList = Session("sessPODetail")
        If Not arlCompletelist Is Nothing Then
            SortListControl(arlCompletelist, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            _sesshelper.SetSession("sessPODetail", arlCompletelist)
            BindPODetail()
        End If
    End Sub

#End Region



End Class