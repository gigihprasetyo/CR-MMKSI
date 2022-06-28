#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility

#End Region

Public Class PopUpJobPositionSelected
    Inherits System.Web.UI.Page

    Dim helper As CommonFunction = New CommonFunction()

    Private ReadOnly Property GetEmployeeArea As String
        Get
            Return Request.QueryString("mode")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            ViewState("CurrentSortColumn") = "Code"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            helper.ClearData(Me.Page)
            BindDataGrid(0)
        End If
    End Sub


    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        dtgJobPosition.DataSource = New JobPositionFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, _
            dtgJobPosition.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgJobPosition.VirtualItemCount = totalRow
        dtgJobPosition.DataBind()
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite

        Dim criterias As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not String.IsNullOrEmpty(txtKode.Text) Then
            criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.[Partial], txtKode.Text))
        End If

        If Not String.IsNullOrEmpty(txtDeskripsi.Text) Then
            criterias.opAnd(New Criteria(GetType(JobPosition), "Description", MatchType.[Partial], txtDeskripsi.Text))
        End If
       
        Return criterias
    End Function

End Class