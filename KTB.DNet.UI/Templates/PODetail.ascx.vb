Public Class PODetailx
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgPODetail As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub PODetail_DataBind(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.DataBinding
        'if an InvalidCastException occurs in either of the next two lines, 
        'please make sure that you've set the TemplateDataMode to Table (because you want nested grids)
        Dim dgi As DataGridItem = CType(Me.BindingContainer, DataGridItem)
        If (Not (TypeOf dgi.DataItem Is DataSet)) Then
            Throw New ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the HierarGrid declaration")
        End If
        Dim ds As DataSet = CType(dgi.DataItem, DataSet)
        dgPODetail.DataSource = ds
        dgPODetail.DataMember = "PODetail"
        dgPODetail.DataBind()
    End Sub

    'Private Sub dgContractDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgContractDetail.ItemDataBound
    '    Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

    '    If Not e.Item.DataItem Is Nothing Then
    '        e.Item.DataItem.GetType().ToString()
    '        Dim RowValue As DataRowView = CType(e.Item.DataItem, DataRowView)
    '        If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

    '            Dim lblMaterialNumber As Label = CType(e.Item.FindControl("lblMaterialNumber"), Label)
    '            Dim lblMaterialDescription As Label = CType(e.Item.FindControl("lblMaterialDescription"), Label)
    '            Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
    '            Dim lblAmountString As Label = CType(e.Item.FindControl("lblAmountString"), Label)
    '            Dim lblPPh22 As Label = CType(e.Item.FindControl("lblPPh22"), Label)
    '            Dim lblPPh22String As Label = CType(e.Item.FindControl("lblPPh22String"), Label)

    '            Dim vc As VechileColor = New VechileColorFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(RowValue("VehicleColorID"))
    '            Dim cd As KTB.DNet.Domain.ContractDetail = New ContractDetailFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(RowValue("ID"))

    '            lblAmountString.Text = cd.Amount * lblAmount.Text
    '            lblPPh22String.Text = cd.TargetQty * lblPPh22.Text
    '            lblMaterialNumber.Text = vc.MaterialNumber
    '            lblMaterialDescription.Text = vc.MaterialDescription
    '        End If
    '    End If

    'End Sub

End Class