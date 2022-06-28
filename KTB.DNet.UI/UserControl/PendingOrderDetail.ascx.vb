Imports KTB.DNet.Domain

Public Class PendingOrderDetail
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgPendingOrderDetail As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim _arl As ArrayList

    Public Property Data() As arraylist
        Get
            Return _arl
        End Get
        Set(ByVal Value As ArrayList)
            _arl = Value
            If (isLoaded) Then
                BindGrid()
            End If
        End Set
    End Property

    Sub BindGrid()
        If (_arl.Count > 0) Then
            dgPendingOrderDetail.Visible = True
            dgPendingOrderDetail.DataSource = _arl
            dgPendingOrderDetail.DataBind()
        Else
            dgPendingOrderDetail.Visible = False
        End If
    End Sub

    Private isLoaded As Boolean = False

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isLoaded = True
        BindGrid()
    End Sub

    Private Sub dgPendingOrderDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPendingOrderDetail.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim oPOD As PendingOrder = CType(e.Item.DataItem, PendingOrder)
            Dim lblOrderType As Label = CType(e.Item.FindControl("lblOrderType"), Label)

            If (oPOD.SparePartPO.OrderType = "E") Then
                lblOrderType.Text = "Emergency"
            ElseIf (oPOD.SparePartPO.OrderType = "R") Then
                lblOrderType.Text = "Regular"
            ElseIf (oPOD.SparePartPO.OrderType = "K") Then
                lblOrderType.Text = "Khusus"
            End If
        End If
    End Sub
End Class
