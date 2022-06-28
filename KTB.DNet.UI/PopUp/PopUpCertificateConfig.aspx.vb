#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports GlobalExtensions
#End Region

Public Class PopUpCertificateConfig
    Inherits System.Web.UI.Page

    Private helpers As New TrainingHelpers(Me.Page)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            btnSearch_Click(sender, e)
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CreateCriteria()
        BindingDataGrid()
    End Sub

    Private Sub CreateCriteria()
        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrCertificateConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtNama.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(TrCertificateConfig), "NamaTTD", MatchType.[Partial], txtNama.Text))
        End If

        If txtJabatan.IsNotEmpty Then
            criteria.opAnd(New Criteria(GetType(TrCertificateConfig), "JabatanTTD", MatchType.[Partial], txtJabatan.Text))
        End If
        helpers.SetSession("criterias", criteria)

    End Sub

    Private Sub BindingDataGrid(Optional ByVal PageIndex As Integer = 0)
        Dim func As New TrCertificateConfigFacade(Me.User)
        Dim totalRow As Integer = 0
        Dim crits As CriteriaComposite = CType(helpers.GetSession("criterias"), CriteriaComposite)
        dtgCertificate.DataSource = New TrCertificateConfigFacade(User).RetrieveActiveList(PageIndex + 1, _
                                dtgCertificate.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                                CType(ViewState("CurrentSortDirect"), Sort.SortDirection), crits)
        dtgCertificate.VirtualItemCount = totalRow
        dtgCertificate.DataBind()

        If dtgCertificate.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgCertificate_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgCertificate.ItemDataBound
        If e.IsRowItems Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub

    Private Sub dtgCertificate_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgCertificate.PageIndexChanged
        BindingDataGrid(e.NewPageIndex)
    End Sub

    Private Sub dtgCertificate_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgCertificate.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        BindingDataGrid()
    End Sub
End Class