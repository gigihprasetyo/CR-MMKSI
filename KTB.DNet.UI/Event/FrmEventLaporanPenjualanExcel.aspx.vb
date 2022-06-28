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

Public Class FrmEventLaporanPenjualanExcel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgExcel As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblNamaKegiatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private redColor() As Int32 = New Int32() {255, 255, 204}
    Private greColor() As Int32 = New Int32() {255, 204, 255}
    Private bluColor() As Int32 = New Int32() {0, 0, 255}

    Public Class DomainBind
        Public DealerID As Int32 = 0
        Private _dealerCode As String = ""
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal Value As String)
                _dealerCode = Value
            End Set
        End Property
        Private _dealerName As String = ""
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal Value As String)
                _dealerName = Value
            End Set
        End Property
        Public ColumnValue As New Hashtable
    End Class
    Private columns As New Hashtable
    Public ReadOnly Property GetNamaKegiatan() As String
        Get
            If Int32.Parse(Request.QueryString("EventParameterId")) > -1 Then
                Dim objFacade As New EventParameterFacade(User)
                Dim eParam As EventParameter = objFacade.Retrieve(Int32.Parse(Request.QueryString("EventParameterId")))
                Return eParam.EventName
            End If
        End Get
    End Property
    Public ReadOnly Property IsGroup() As Boolean
        Get
            If Request.QueryString("Type") Is Nothing Then
                Throw New Exception("DEV ERR: no type supplied")
            End If
            Return Request.QueryString("Type") = "group"
        End Get
    End Property
    Public ReadOnly Property GetPeriode() As String
        Get
            If Int32.Parse(Request.QueryString("EventParameterId")) > -1 Then
                Dim objFacade As New EventParameterFacade(User)
                Dim eParam As EventParameter = objFacade.Retrieve(Int32.Parse(Request.QueryString("EventParameterId")))
                Return String.Format("Periode: {0} - {1}", eParam.EventDateStart.ToString("dd MMM"), eParam.EventDateEnd.ToString("dd MMM yyyy"))
            Else
                Return "Period: "
            End If
        End Get
    End Property
    Private sesHelper As New SessionHelper
    Private Const DataSourceName As String = "EventSellingReportSourceExcel"
    Private Property GridDataSource() As ArrayList
        Get
            If IsNothing(sesHelper.GetSession(DataSourceName)) Then
                sesHelper.SetSession(DataSourceName, New ArrayList)
            End If
            Return sesHelper.GetSession(DataSourceName)
        End Get
        Set(ByVal Value As ArrayList)
            sesHelper.SetSession(DataSourceName, Value)
        End Set
    End Property
    Private colColor As New Hashtable
    Private lastColor As Int32 = 0
    Private Function BuiltDataSource() As ArrayList
        Dim tempHash As New Hashtable
        Dim arrSource As ArrayList
        If IsGroup Then
            arrSource = CommonFunction.SortArraylist(GridDataSource, GetType(V_EventLaporanPenjualanGroupDealer), _
                "VechileType.Category.CategoryCode", Sort.SortDirection.ASC)
            For Each lap As V_EventLaporanPenjualanGroupDealer In GridDataSource
                If Not columns.ContainsKey(lap.VechileType.ID) Then
                    Dim bColumn As New BoundColumn
                    bColumn.HeaderText = lap.VechileType.Description
                    dtgExcel.Columns.Add(bColumn)
                    columns.Add(lap.VechileType.ID, dtgExcel.Columns.Count - 1)
                    If colColor.ContainsKey(lap.VechileType.Category.ID) Then
                        bColumn.HeaderStyle.BackColor = colColor(lap.VechileType.Category.ID)
                    Else
                        If lastColor < 3 Then
                            bcolumn.HeaderStyle.BackColor = Color.FromArgb(redColor(lastColor), greColor(lastColor), bluColor(lastColor))
                            colColor.Add(lap.VechileType.Category.ID, bcolumn.HeaderStyle.BackColor)
                            lastColor = lastColor + 1
                        End If
                    End If
                End If
                Dim dtBind As DomainBind
                If tempHash.ContainsKey(lap.Dealer.ID) Then
                    dtBind = DirectCast(tempHash(lap.Dealer.ID), DomainBind)
                Else
                    dtBind = New DomainBind
                    dtBind.DealerName = lap.Dealer.GroupName
                    dtBind.DealerID = lap.Dealer.ID
                    tempHash.Add(lap.Dealer.ID, dtBind)
                End If
                If dtBind.ColumnValue.ContainsKey(lap.VechileType.ID) Then
                    dtBind.ColumnValue(lap.VechileType.ID) = dtBind.ColumnValue(lap.VechileType.ID) + lap.Jumlah
                Else
                    dtBind.ColumnValue.Add(lap.VechileType.ID, lap.Jumlah)
                End If
            Next
        Else
            arrSource = CommonFunction.SortArraylist(GridDataSource, GetType(EventLaporanPenjualan), _
                "VechileType.Category.CategoryCode", Sort.SortDirection.ASC)
            For Each lap As EventLaporanPenjualan In GridDataSource
                If Not columns.ContainsKey(lap.VechileType.ID) Then
                    Dim bColumn As New BoundColumn
                    bColumn.HeaderText = lap.VechileType.Description
                    dtgExcel.Columns.Add(bColumn)
                    columns.Add(lap.VechileType.ID, dtgExcel.Columns.Count - 1)
                    If colColor.ContainsKey(lap.VechileType.Category.ID) Then
                        bColumn.HeaderStyle.BackColor = colColor(lap.VechileType.Category.ID)
                    Else
                        If lastColor < 3 Then
                            bcolumn.HeaderStyle.BackColor = Color.FromArgb(redColor(lastColor), greColor(lastColor), bluColor(lastColor))
                            colColor.Add(lap.VechileType.Category.ID, bcolumn.HeaderStyle.BackColor)
                            lastColor = lastColor + 1
                        End If
                    End If
                End If
                Dim dtBind As DomainBind
                If tempHash.ContainsKey(lap.Dealer.ID) Then
                    dtBind = DirectCast(tempHash(lap.Dealer.ID), DomainBind)
                Else
                    dtBind = New DomainBind
                    dtBind.DealerName = lap.Dealer.DealerName
                    dtBind.DealerID = lap.Dealer.ID
                    dtbind.DealerCode = lap.Dealer.DealerCode
                    tempHash.Add(lap.Dealer.ID, dtBind)
                End If
                If dtBind.ColumnValue.ContainsKey(lap.VechileType.ID) Then
                    dtBind.ColumnValue(lap.VechileType.ID) = dtBind.ColumnValue(lap.VechileType.ID) + lap.Jumlah
                Else
                    dtBind.ColumnValue.Add(lap.VechileType.ID, lap.Jumlah)
                End If
            Next
        End If
        Dim newArray As New ArrayList
        For Each db As DomainBind In tempHash.Values
            newArray.Add(db)
        Next
        Return newArray
    End Function
    Private Sub BindGrid()
        dtgExcel.Columns(1).Visible = Not IsGroup
        If IsGroup Then
            dtgExcel.Columns(2).HeaderText = "Group Name"
        Else
            dtgExcel.Columns(2).HeaderText = "Dealer Name"
        End If
        dtgExcel.DataSource = BuiltDataSource()
        dtgExcel.DataBind()
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BindGrid()
        Response.ContentType = "application/x-download"
        Response.AddHeader("Content-Disposition", "attachment;filename=""EventLaporanPenjualan.xls""")
    End Sub
    Private columSum As New Hashtable
    Private Sub dtgExcel_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgExcel.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header AndAlso e.Item.ItemType <> ListItemType.Footer Then
            e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dtgExcel.PageSize * dtgExcel.CurrentPageIndex)).ToString
            Dim data As DomainBind = DirectCast(e.Item.DataItem, DomainBind)
            Dim lblDealerCode As Label = e.Item.FindControl("lblDealerCode")
            lblDealerCode.Text = data.DealerCode
            For Each vehId As Integer In data.ColumnValue.Keys
                e.Item.Cells(columns(vehId)).Text = data.ColumnValue(vehId)
                If columSum.ContainsKey(columns(vehId)) Then
                    columSum(columns(vehId)) = columSum(columns(vehId)) + data.ColumnValue(vehId)
                Else
                    columSum.Add(columns(vehId), data.ColumnValue(vehId))
                End If
            Next
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            For Each val As Int32 In columSum.Keys
                e.Item.Cells(val).Text = columSum(val)
            Next
        End If
    End Sub
End Class