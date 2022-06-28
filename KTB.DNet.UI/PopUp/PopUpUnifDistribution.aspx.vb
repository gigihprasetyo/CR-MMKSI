Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility

Public Class PopUpUnifDistribution
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgUnifDistribution As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtOrderNo As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim criterias As CriteriaComposite
#End Region

#Region "Custom Method"
    Private Sub BindToGrid()
        CreateCriteria()
        Dim sortColumn As String
        Dim sortDirection As Sort.SortDirection

        sortColumn = CType(viewstate("SortColUI"), String)
        sortDirection = CType(viewstate("SortDirectionUI"), Sort.SortDirection)

        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
            sortColl.Add(New Sort(GetType(SalesmanUniform), sortColumn, sortDirection))
        Else
            sortColl = Nothing
        End If

        Dim arlSalemanUnif As ArrayList = New SalesmanUniformFacade(User).Retrieve(criterias, sortColl)
        If arlSalemanUnif.Count > 0 Then
            dtgUnifDistribution.DataSource = arlSalemanUnif
        Else
            dtgUnifDistribution.DataSource = New ArrayList
        End If

        dtgUnifDistribution.DataBind()
    End Sub

    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtOrderNo.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.SalesmanUnifDistributionCode", MatchType.[Partial], txtOrderNo.Text))
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            Initialize()
        End If
    End Sub

    Private Sub Initialize()
        viewstate.Add("SortColUI", "SalesmanUnifDistribution.SalesmanUnifDistributionCode")
        viewstate.Add("SortDirectionUI", Sort.SortDirection.ASC)
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindToGrid()
    End Sub

    Private Sub dtgUnifDistribution_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgUnifDistribution.SortCommand
        If e.SortExpression = viewstate("SortColUI") Then
            If viewstate("SortDirectionUI") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirectionUI", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirectionUI", Sort.SortDirection.ASC)
            End If
        Else
            ViewState("SortDirectionUI") = Sort.SortDirection.ASC
        End If
        ViewState("SortColUI") = e.SortExpression

        dtgUnifDistribution.SelectedIndex = -1
        BindToGrid()
    End Sub
End Class
