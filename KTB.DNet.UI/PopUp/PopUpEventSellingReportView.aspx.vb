#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class PopUpEventSellingReportView
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgEvent As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Property GetSortColumn() As String
        Get
            If IsNothing(ViewState("SortColumn")) Then
                ViewState("SortColumn") = "ID"
            End If
            Return ViewState("SortColumn")
        End Get
        Set(ByVal Value As String)
            ViewState("SortColumn") = Value
        End Set
    End Property

    Private Property GetSortDirection() As Sort.SortDirection
        Get
            If IsNothing(ViewState("SortDirection")) Then
                ViewState("SortDirection") = Sort.SortDirection.ASC
            End If
            Return ViewState("SortDirection")
        End Get
        Set(ByVal Value As Sort.SortDirection)
            ViewState("SortDirection") = Value
        End Set
    End Property

    Private ReadOnly Property GetEventReport() As ArrayList
        Get
            Dim ParamID As Int32 = Request.QueryString("id")
            Dim objFacade As New EventLaporanPenjualanFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(EventLaporanPenjualan), _
                "RowStatus", CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(EventLaporanPenjualan), "EventParameter", ParamID))
            Return objFacade.RetrieveActiveList(crit, dtgEvent.CurrentPageIndex + 1, dtgEvent.PageSize, _
                dtgEvent.VirtualItemCount, GetSortColumn, GetSortDirection)
        End Get
    End Property

    Private Sub BindGrid()
        dtgEvent.DataSource = GetEventReport
        dtgEvent.DataBind()
    End Sub


    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        BindGrid()
    End Sub

    Private Sub dtgEvent_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEvent.PageIndexChanged
        dtgEvent.CurrentPageIndex = e.NewPageIndex
    End Sub

    Private Sub dtgEvent_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEvent.SortCommand
        If GetSortColumn = e.SortExpression Then
            Select Case GetSortDirection
                Case Sort.SortDirection.ASC
                    GetSortDirection = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    GetSortDirection = Sort.SortDirection.ASC
            End Select
        Else
            GetSortColumn = e.SortExpression
            GetSortDirection = Sort.SortDirection.ASC
        End If
    End Sub
End Class