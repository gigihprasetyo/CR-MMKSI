Imports Ktb.DNet.BusinessFacade
Imports Ktb.DNet.BusinessFacade.SparePart
Imports Ktb.DNet.Domain
Imports Ktb.DNet.Domain.Search
Imports Ktb.DNet.Utility
Imports KTB.DNet.Security


Public Class FrmSparePartMasterAlt
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
        '--exclude  this privilege from Asra (BA)
        'InitiateAuthorization()
        If Not IsPostBack Then
            'Put user code to initialize the page here
            SetLabelSparePartAlt()
            initiatePage()
        End If
    End Sub

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ViewSparePartMasterAltPage_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Informasi Pengguna Spare Part Alternatif")
        End If
        'Me.btnFind.Visible = SecurityProvider.Authorize(context.User, SR.SearchSPPO_Status_Privilege)


        '_isShowDetailAllowed = SecurityProvider.Authorize(context.User, SR.ViewSPPO_StatusDetail_Privilege)
        'If _isPrintAllowed = False And _isShowDetailAllowed = False Then
        '    Me.dtgPOStatus.Columns(7).Visible = False
        'End If
    End Sub

    Private Sub SetLabelSparePartAlt()
        lblSparePartAlt.Text = CType(Request.QueryString("NoSparePartAlt"), String)
        ViewState("NoSparePartAlt") = CType(Request.QueryString("NoSparePartAlt"), String)
    End Sub

    Private Sub initiatePage()
        ViewState("currSortColumn") = "PartNumber"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        BindDtgSparePartAlt(1)
    End Sub

    Private Sub dtgSparePartAlt_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSparePartAlt.PageIndexChanged
        dtgSparePartAlt.SelectedIndex = -1
        dtgSparePartAlt.CurrentPageIndex = e.NewPageIndex
        BindDtgSparePartAlt(dtgSparePartAlt.CurrentPageIndex)
        'ClearData()
    End Sub

    Private Sub dtgSparePartAlt_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSparePartAlt.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.DESC
        End If
        BindDtgSparePartAlt(dtgSparePartAlt.CurrentPageIndex + 1)
    End Sub

    Private Sub dtgSparePartAlt_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSparePartAlt.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(1).Text = CType(e.Item.ItemIndex + 1 + (dtgSparePartAlt.CurrentPageIndex * dtgSparePartAlt.PageSize), String)
            Dim objSparePartAlt As SparePartMaster = CType(arrSparePartAlt(e.Item.ItemIndex), SparePartMaster)

            'If objSparePartAlt.ModelCode.Length > 0 Then
            '    e.Item.Cells(4).Text = objSparePartAlt.ModelCode.Substring(3)
            'End If

            'stock
            If objSparePartAlt.Stock = 0 Then
                e.Item.Cells(5).Text = "Tidak Ada"
            ElseIf objSparePartAlt.Stock > 0 Then
                e.Item.Cells(5).Text = "Ada"
            End If
            'stock
        End If
    End Sub

    'Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
    '    Session.Add("Back", True)
    '    Response.Redirect("../SparePart/FrmSparePartMaster.aspx")
    'End Sub

    Private Sub BindDtgSparePartAlt()
        arrSparePartAlt = New SparePartMasterFacade(User).RetrieveWithOneCriteria(1, dtgSparePartAlt.PageSize, 0, "AltPartNumber", MatchType.Exact, ViewState("NoSparePartAlt"), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        dtgSparePartAlt.DataSource = arrSparePartAlt
        dtgSparePartAlt.DataBind()
    End Sub

    Private Sub BindDtgSparePartAlt(ByVal pageIndex As Integer)
        Try
            Dim totalRow As Integer = 0
            If (pageIndex >= 0) Then
                'dtgSparePartMaster.DataSource = New SparePartMasterFacade(User).RetrieveActiveList(pageIndeks, dtgSparePartMaster.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
                'arrSparePartAlt = CType(Session.Item("NoSparePartAlt"), ArrayList)
                'dtgSparePartAlt.DataSource = arrSparePartAlt
                arrSparePartAlt = New SparePartMasterFacade(User).RetrieveWithOneCriteria(pageIndex, dtgSparePartAlt.PageSize, totalRow, "AltPartNumber", MatchType.Exact, ViewState("NoSparePartAlt"), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
                dtgSparePartAlt.DataSource = arrSparePartAlt
                dtgSparePartAlt.VirtualItemCount = totalRow
                dtgSparePartAlt.DataBind()
            End If
        Catch ex As Exception
            MessageBox.Show(SR.ViewFail)
        End Try
        
    End Sub
End Class
