Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class PopUpDocFlowEqpSPPOIndent
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents dtgItem As System.Web.UI.WebControls.DataGrid

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Dim veqppof As v_EquipSPPOIndentFacade = New v_EquipSPPOIndentFacade(User)
        Dim arlEqpPo As ArrayList = New ArrayList

        If (Not IsNothing(Request.QueryString("ponumber"))) Then
            Dim ponumber As String = Request.QueryString("ponumber")
            arlEqpPo = veqppof.RetrieveByPONumber(Request.QueryString("ponumber"))
            If (Not IsNothing(arlEqpPo) And arlEqpPo.Count > 0) Then
                Dim obj As SparePartPO = New KTB.DNet.BusinessFacade.SparePart.SparePartPOFacade(User).Retrieve(CType(arlEqpPo(0), v_EquipSPPOIndent).PONumber)
                If (Not IsNothing(obj)) Then
                    lblHeader.Text = String.Format("PO Reg # - {0} -> {1} unit", obj.PONumber, obj.ItemQuantity)
                    dtgItem.DataSource = arlEqpPo
                    dtgItem.DataBind()
                End If
            End If
        End If
    End Sub

    Private Sub dtgItem_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgItem.ItemDataBound
        If (IsNothing(e.Item.DataItem)) Then Return

        Dim objEqp As v_EquipSPPOIndent = CType(e.Item.DataItem, v_EquipSPPOIndent)
        Dim obj As SparePartPO = New KTB.DNet.BusinessFacade.SparePart.SparePartPOFacade(User).Retrieve(objEqp.PONumber)
        Dim lblTotalItem As Label = CType(e.Item.FindControl("lblTotalItem"), Label)
        lblTotalItem.Text = obj.ItemQuantity.ToString()

    End Sub
End Class
