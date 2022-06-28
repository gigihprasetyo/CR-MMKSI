Imports Ktb.DNet.BusinessFacade
Imports Ktb.DNet.BusinessFacade.SparePart
Imports Ktb.DNet.Domain
Imports Ktb.DNet.Domain.Search
Imports Ktb.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.SAP


Public Class FrmSUBPartMasterSAP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgSparePartAlt As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSparePartAlt As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private _NoSparePartAlt As String = 0
    Private arrSparePartAlt As ArrayList = New ArrayList



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            'Put user code to initialize the page here
            SetLabelSparePartAlt()
            BindDataGridAltPart()
        End If
    End Sub

    Private Sub SetLabelSparePartAlt()
        lblSparePartAlt.Text = CType(Request.QueryString("NoSparePartAlt"), String)
        ViewState("NoSparePartAlt") = CType(Request.QueryString("NoSparePartAlt"), String)
    End Sub

    Private Function GetBlockMaterial(ByVal code As String) As String
        Dim _SettingBlockMaterialFacade As SettingBlockMaterialFacade = New SettingBlockMaterialFacade(User)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SettingBlockMaterial), "Code", MatchType.Exact, code.ToUpper))
        Dim pesanList As ArrayList = _SettingBlockMaterialFacade.Retrieve(criterias)
        If pesanList.Count > 0 Then
            Return CType(pesanList(0), SettingBlockMaterial).Description
        Else
            Return String.Empty
        End If
    End Function

    Private Function PopulateAltPartMasterList(ByVal oAltPartMaster As ZSPST0028_02) As SparePartMaster
        Dim oPartMaster As SparePartMaster = New SparePartMaster
        oPartMaster.PartNumber = oAltPartMaster.Matnr1
        oPartMaster.PartName = oAltPartMaster.MAKTX
        oPartMaster.ModelCode = oAltPartMaster.MATKL
        oPartMaster.AltPartNumber = oAltPartMaster.Matnr2
        oPartMaster.RetalPrice = CType(oAltPartMaster.RTLPR, Decimal)
        oPartMaster.StockSAP = IIf(CType(oAltPartMaster.STOCK, String).ToUpper = "AVAILABLE", "Ada", "Tidak Ada")




        oPartMaster.Pesan = GetBlockMaterial(oAltPartMaster.NORMT)
        If oPartMaster.Pesan = String.Empty Then
            oPartMaster.StockSAP = CType(oAltPartMaster.STOCK, String)
            oPartMaster.RetalPrice = CType(oAltPartMaster.RTLPR, Decimal)
        Else
            oPartMaster.StockSAP = oPartMaster.Pesan
            oPartMaster.RetalPrice = 0
        End If
        'oPartMaster.MaxStock = oAltPartMaster.Rqqty




        Return oPartMaster
    End Function

    Private Function ExistOnList(ByVal strPartNumber As String, ByVal strAltPartNumber As String) As Boolean
        For Each objItem As SparePartMaster In arrSparePartAlt
            If objItem.PartNumber = strPartNumber AndAlso objItem.AltPartNumber = strAltPartNumber Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub BindDataGridAltPart()
        Dim arlAltPartMaster As ArrayList = CType(Session("sessSAP_AltPartMaster"), ArrayList)
        '--Reset arraylist of materials
        arrSparePartAlt.Clear()
        '-- get SAP result to arraylist
        For Each oAltPartMaster As ZSPST0028_02 In arlAltPartMaster
            If CType(oAltPartMaster.Matnr2, String).Trim.ToUpper = lblSparePartAlt.Text.Trim.ToUpper Then
                If Not ExistOnList(CType(oAltPartMaster.Matnr1, String).Trim.ToUpper, CType(oAltPartMaster.Matnr2, String).Trim.ToUpper) Then
                    Dim oPartMaster As SparePartMaster = PopulateAltPartMasterList(oAltPartMaster)
                    arrSparePartAlt.Add(oPartMaster)
                End If

            End If
        Next
        dtgSparePartAlt.DataSource = arrSparePartAlt
        dtgSparePartAlt.DataBind()
    End Sub



    Private Sub dtgSparePartAlt_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSparePartAlt.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(1).Text = CType(e.Item.ItemIndex + 1 + (dtgSparePartAlt.CurrentPageIndex * dtgSparePartAlt.PageSize), String)
        End If
    End Sub

End Class
