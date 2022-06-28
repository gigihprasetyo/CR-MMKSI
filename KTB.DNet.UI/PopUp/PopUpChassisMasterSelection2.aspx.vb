#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility

#End Region

Public Class PopUpChassisMasterSelection2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtMoMesin As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgChassisMaster As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNoRangka As System.Web.UI.WebControls.TextBox
    Protected WithEvents hdnIndent As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnChooseIndent As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents hdnPqrNo As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Private Variable"
    Dim _sessHelper As New SessionHelper

#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim LoginDealer As Dealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim totalRow As Integer = 0

        If indexPage >= 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "RecallCategory.RecallRegNo", MatchType.Exact, hdnPqrNo.Value))

            If txtNoRangka.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ChassisNo", MatchType.[Partial], txtNoRangka.Text))
            End If

            Dim arlRecallCM As ArrayList = New RecallChassisMasterFacade(User).RetrieveActiveList(indexPage + 1, dtgChassisMaster.PageSize, totalRow, CType(ViewState("SortColumn"), String), CType(ViewState("SortDirection"), Sort.SortDirection), criterias)
            If arlRecallCM.Count > 0 Then
                dtgChassisMaster.DataSource = arlRecallCM
                btnChoose.Disabled = False
            Else
                dtgChassisMaster.DataSource = New ArrayList
                btnChoose.Disabled = True
            End If

            dtgChassisMaster.VirtualItemCount = totalRow

            If indexPage = 0 Then
                dtgChassisMaster.CurrentPageIndex = 0
            End If

            dtgChassisMaster.DataBind()
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            hdnPqrNo.Value = Request.QueryString("pqrNo")
            btnChoose.Visible = True
            ViewState.Add("SortColumn", "ChassisNo")
            viewstate.Add("SortDirection", Sort.SortDirection.ASC)

            dtgChassisMaster.CurrentPageIndex = 0
            BindToGrid(dtgChassisMaster.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgChassisMaster.CurrentPageIndex = 0
        BindToGrid(dtgChassisMaster.CurrentPageIndex)
    End Sub

    Private Sub dtgChassisMaster_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgChassisMaster.PageIndexChanged
        dtgChassisMaster.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgChassisMaster.CurrentPageIndex)
    End Sub

    Private Sub dtgChassisMaster_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgChassisMaster.SortCommand
        If e.SortExpression = CType(ViewState("SortColumn"), String) Then
            Select Case CType(ViewState("SortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("SortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("SortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("SortColumn") = e.SortExpression
            ViewState("SortDirection") = Sort.SortDirection.ASC
        End If
        viewstate.Add("SortColumn", e.SortExpression)
        BindToGrid(dtgChassisMaster.CurrentPageIndex)

    End Sub

End Class

