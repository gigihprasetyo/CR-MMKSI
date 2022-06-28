Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class PopUpDocFlowEqpPo
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

        Dim veqppof As v_EquipPOFacade = New v_EquipPOFacade(User)
        Dim arlEqpPo As ArrayList = New ArrayList

        If (Not IsNothing(Request.QueryString("ponumber"))) Then
            Dim ponumber As String = Request.QueryString("ponumber")
            Dim objSearch As v_EquipPOSearch = New v_EquipPOSearch
            Dim arlTmp As ArrayList = New ArrayList
            arlTmp.Add(ponumber)
            objSearch.arlSPPONo = arlTmp
            objSearch.dtmFrom = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, Date)
            objSearch.dtmTo = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, Date)
            arlEqpPo = veqppof.RetrieveSearch(objSearch)
            Dim obj As IndentPartHeader = New KTB.DNet.BusinessFacade.IndentPart.IndentPartHeaderFacade(User).Retrieve(ponumber)
            If (Not IsNothing(obj)) Then
                lblHeader.Text = String.Format("Indent Part Reg # - {0} -> {1} unit", ponumber, obj.TotalQuantity)
            End If
            dtgItem.Columns(0).Visible = False
            dtgItem.Columns(2).Visible = False
        ElseIf (Not IsNothing(Request.QueryString("estimationnumber"))) Then
            Dim estimationnumber As String = Request.QueryString("estimationnumber")
            Dim objSearch As v_EquipPOSearch = New v_EquipPOSearch
            Dim arlTmp As ArrayList = New ArrayList
            arlTmp.Add(estimationnumber)
            objSearch.arlEstNo = arlTmp
            objSearch.dtmFrom = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, Date)
            objSearch.dtmTo = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, Date)
            arlEqpPo = veqppof.RetrieveSearch(objSearch)
            Dim obj As EstimationEquipHeader = New KTB.DNet.BusinessFacade.IndentPartEquipment.EstimationEquipHeaderFacade(User).Retrieve(estimationnumber)
            If (Not IsNothing(obj)) Then
                lblHeader.Text = String.Format("Estimation Reg # - {0} -> {1} unit", estimationnumber, obj.TotalQty)
            End If
            dtgItem.Columns(1).Visible = False
            dtgItem.Columns(3).Visible = False
        End If

        dtgItem.DataSource = arlEqpPo
        dtgItem.DataBind()
    End Sub

End Class
