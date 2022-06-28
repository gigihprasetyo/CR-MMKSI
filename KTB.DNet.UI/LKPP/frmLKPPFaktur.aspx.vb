Imports KTB.DNet.Domain
'Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.LKPP
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class frmLKPPFaktur
    Inherits System.Web.UI.Page
    Dim _sessHelper As New SessionHelper

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            dgFaktur.CurrentPageIndex = 0
            ViewState.Add("currentSortColumn", "FakturNumber")
            ViewState.Add("currentSortDirection", Sort.SortDirection.ASC)
            BindData(dgFaktur.CurrentPageIndex)
        End If
    End Sub

    Private Sub dgFaktur_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgFaktur.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objEndCust As EndCustomer = e.Item.DataItem
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatus.Text = EnumChassisMaster.FakturStatusDesc(objEndCust.ChassisMaster.FakturStatus)

            Dim lblOpenFakturDate As Label = CType(e.Item.FindControl("lblOpenFakturDate"), Label)
            If objEndCust.OpenFakturDate < New Date(1900, 1, 1) Then
                lblOpenFakturDate.Text = ""
            Else
                lblOpenFakturDate.Text = objEndCust.OpenFakturDate.ToString("dd/MM/yyyy")
            End If
        End If
    End Sub

    Private Sub dgFaktur_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgFaktur.PageIndexChanged
        dgFaktur.CurrentPageIndex = e.NewPageIndex
        BindData(dgFaktur.CurrentPageIndex)
    End Sub

    Private Sub dgFaktur_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgFaktur.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgFaktur.SelectedIndex = -1
        dgFaktur.CurrentPageIndex = 0
        bindGridSorting(dgFaktur.CurrentPageIndex)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub

    Private Sub BindData(ByVal currentPageIndex As Integer)
        Try
            Dim total As Integer = 0
            Dim _vehicleTypeCode As String = Request.QueryString("vehicleTypeCode")
            Dim _lkppID As String = Request.QueryString("lkppId")

            Dim o As New EndCustomer
            'o.Customer.ID
            Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EndCustomer), "LKPPHeader.ID", MatchType.Exact, CInt(_lkppID)))
            criterias.opAnd(New Criteria(GetType(EndCustomer), "ChassisMaster.VechileColor.VechileType.VechileTypeCode", MatchType.Exact, _vehicleTypeCode))
            criterias.opAnd(New Criteria(GetType(EndCustomer), "Customer.ID", MatchType.Greater, 0))

            Dim _result As ArrayList = New EndCustomerFacade(User).RetrieveByCriteria(criterias, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            _sessHelper.SetSession("SortViewFaktur", criterias)
            If _result.Count > 0 Then
                dgFaktur.DataSource = _result
                dgFaktur.VirtualItemCount = total
                dgFaktur.DataBind()
            Else
                Response.Write("<script language='javascript'>alert('Tidak ada faktur'); window.close();</script>")
            End If
        Catch ex As Exception
            Response.Write("<script language='javascript'>alert('Tidak ada faktur'); window.close();</script>")
        End Try

    End Sub

    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dgFaktur.DataSource = New EndCustomerFacade(User).RetrieveByCriteria(_sessHelper.GetSession("SortViewFaktur"), CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgFaktur.VirtualItemCount = totalRow
            dgFaktur.DataBind()
        End If

    End Sub

End Class