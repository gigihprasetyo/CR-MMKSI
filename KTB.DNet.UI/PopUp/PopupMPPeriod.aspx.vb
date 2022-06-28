#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility

#End Region

Public Class PopupMPPeriod
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgMPMaster As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnTest2 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region



#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If indexPage >= 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))


            'Dim arlMPMaster As ArrayList = New MaterialPromotionPeriodFacade(User).RetrieveActiveList(indexPage + 1, dtgMPMaster.PageSize, totalRow, CType(ViewState("SortColumn"), String), CType(ViewState("SortDirection"), Sort.SortDirection), criterias)
            Dim arlMPMaster As New ArrayList
            Dim objMPPeriodFacade As New MaterialPromotionPeriodFacade(User)
            arlMPMaster = objMPPeriodFacade.RetrieveActiveList(criterias, indexPage + 1, dtgMPMaster.PageSize, totalRow, CType(viewstate("SortColumn"), String), CType(viewstate("SortDirection"), Sort.SortDirection))

            If arlMPMaster.Count > 0 Then
                dtgMPMaster.DataSource = arlMPMaster
                btnChoose.Disabled = False
            Else
                dtgMPMaster.DataSource = New ArrayList
                btnChoose.Disabled = True
            End If

            dtgMPMaster.VirtualItemCount = totalRow

            If indexPage = 0 Then
                dtgMPMaster.CurrentPageIndex = 0
            End If

            dtgMPMaster.DataBind()
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            viewstate.Add("SortColumn", "PeriodName")
            viewstate.Add("SortDirection", Sort.SortDirection.ASC)
            dtgMPMaster.CurrentPageIndex = 0
            BindToGrid(dtgMPMaster.CurrentPageIndex)
        End If
        'dtgMPMaster.CurrentPageIndex = 0
        'BindToGrid(dtgMPMaster.CurrentPageIndex)
    End Sub

    
    Private Sub dtgMPMaster_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMPMaster.PageIndexChanged
        dtgMPMaster.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgMPMaster.CurrentPageIndex)
    End Sub

    Private Sub dtgMPMaster_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMPMaster.SortCommand
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
        BindToGrid(dtgMPMaster.CurrentPageIndex)
    End Sub
End Class
