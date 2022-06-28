Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class FrmPKHeaderSPL
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dg_PKDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim _sessHelper As New SessionHelper
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            dg_PKDetail.CurrentPageIndex = 0
            ViewState.Add("currentSortColumn", "PKHeader.PKNumber")
            viewstate.Add("currentSortDirection", Sort.SortDirection.ASC)
            BindData(dg_PKDetail.CurrentPageIndex)
        End If
    End Sub
    Private Sub BindData(ByVal currentPageIndex As Integer)
        Try
            Dim total As Integer = 0
            Dim obj As New SPLDetail
            Dim _splnumber As String = Request.QueryString("_splnumber")
            Dim _kodetipe As String = Request.QueryString("_kodetipe")
            Dim _periodmonth As Integer = Request.QueryString("_periodemonth")
            Dim _periodyear As Integer = Request.QueryString("_periodeyear")

            Dim criterias As New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.SPLNumber", MatchType.Exact, _splnumber))
            criterias.opAnd(New Criteria(GetType(PKDetail), "VehicleTypeCode", MatchType.Exact, _kodetipe))
            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, _periodmonth))
            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, _periodyear))
            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) _
                & "," & CType(enumStatusPK.Status.Selesai, Integer) & "," & CType(enumStatusPK.Status.Konfirmasi, Integer) & "," & CType(enumStatusPK.Status.Setuju, Integer) & ")"))

            Dim _result As ArrayList = New PKDetailFacade(User).RetrieveByCriteria(criterias, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            _sessHelper.SetSession("SortViewPK", criterias)
            If _result.Count > 0 Then
                dg_PKDetail.DataSource = _result
                dg_PKDetail.VirtualItemCount = total
                dg_PKDetail.DataBind()
            Else
                Response.Write("<script language='javascript'>alert('PK Referensi tidak ada'); window.close();</script>")
            End If
        Catch ex As Exception
            Response.Write("<script language='javascript'>alert('PK Referensi tidak ada'); window.close();</script>")
        End Try

    End Sub

    Private Sub dg_PKDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dg_PKDetail.ItemDataBound
        'Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        'lblNo.Text = e.Item.ItemIndex + 1 + (dg_PKDetail.CurrentPageIndex * dg_PKDetail.PageSize)
    End Sub

    Private Sub dg_PKDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_PKDetail.PageIndexChanged
        dg_PKDetail.CurrentPageIndex = e.NewPageIndex
        BindData(dg_PKDetail.CurrentPageIndex)
    End Sub

    Private Sub dg_PKDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dg_PKDetail.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dg_PKDetail.SelectedIndex = -1
        dg_PKDetail.CurrentPageIndex = 0
        bindGridSorting(dg_PKDetail.CurrentPageIndex)
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dg_PKDetail.DataSource = New PKDetailFacade(User).RetrieveByCriteria(_sessHelper.GetSession("SortViewPK"), CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dg_PKDetail.VirtualItemCount = totalRow
            dg_PKDetail.DataBind()
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub
End Class
