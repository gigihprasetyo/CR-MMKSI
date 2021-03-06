#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility

#End Region

Public Class PopUpChassisMasterSelection
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

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            'Add ProductCategory
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode.Trim.ToUpper))

            If Val(hdnIndent.Value) = 0 Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.Exact, "0"))
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "StockStatus", MatchType.No, "X"))
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "StockDealer", MatchType.Exact, LoginDealer.ID))

            End If

            If Val(hdnIndent.Value) = 2 Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.Exact, "4"))
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.ID", MatchType.No, 0))
            End If


            If txtNoRangka.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Partial, txtNoRangka.Text))
            End If

            If txtMoMesin.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "EngineNumber", MatchType.Partial, txtMoMesin.Text))
            End If

            Dim arlChassisMaster As ArrayList = New ChassisMasterFacade(User).RetrieveActiveList(indexPage + 1, dtgChassisMaster.PageSize, totalRow, CType(ViewState("SortColumn"), String), CType(ViewState("SortDirection"), Sort.SortDirection), criterias)

            If arlChassisMaster.Count > 0 Then
                dtgChassisMaster.DataSource = arlChassisMaster
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
            hdnIndent.Value = Val(Request.QueryString("Indent"))
            If Val(hdnIndent.Value) = 0 Then
                btnChooseIndent.Visible = False
                btnChoose.Visible = True
            Else
                btnChooseIndent.Visible = True
                btnChoose.Visible = False
            End If

            viewstate.Add("SortColumn", "ChassisNumber")
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
        ViewState.Add("SortColumn", e.SortExpression)
        BindToGrid(dtgChassisMaster.CurrentPageIndex)

    End Sub

    Private Sub dtgChassisMaster_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgChassisMaster.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim RowData As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)
            Dim lblStockStatus As Label = CType(e.Item.Cells(1).FindControl("lblStockStatus"), Label)
            If RowData.StockStatus.Trim = "X" Then
                lblStockStatus.Text = "X"
            Else
                lblStockStatus.Text = ""
            End If

            Dim lblTypeColor As Label = CType(e.Item.Cells(1).FindControl("lblTypeColor"), Label)
            If Val(hdnIndent.Value) = 1 Then
                lblTypeColor.Text = ";" & RowData.VechileTypeAndColor
            End If

        End If

    End Sub
End Class
