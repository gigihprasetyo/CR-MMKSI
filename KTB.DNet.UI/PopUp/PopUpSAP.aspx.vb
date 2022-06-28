#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.SAP

#End Region


Public Class PopUpSAP
    Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents dtgSAPList As System.Web.UI.WebControls.DataGrid
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


#Region "Variable Deklaration"
    ' Dim oDealer As Dealer
    Dim sesHelper As SessionHelper = New SessionHelper
    Private ArlSAPdata As ArrayList
#End Region



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            displayData(0)
            If dtgSAPList.Items.Count > 0 Then
                btnChoose.Disabled = False
            Else
                btnChoose.Disabled = True
            End If
        End If
    End Sub

    Private Sub displayData(ByVal currentPageIndex As Integer)
        Dim curDate As Date = Now
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(SAPPeriod), "EndConfirmedDate", MatchType.GreaterOrEqual, curDate))

        ArlSAPdata = New SAPPeriodFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgSAPList.PageSize, _
              total, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If (ArlSAPdata.Count > 0) Then
            dtgSAPList.DataSource = ArlSAPdata
            dtgSAPList.VirtualItemCount = total
            dtgSAPList.DataBind()
        Else
            MessageBox.Show(SR.DataNotFound("PERIODE SAP"))
            '
            Exit Sub
        End If
    End Sub

    Private Sub dtgSAPList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSAPList.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub

    Private Sub dtgSAPList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSAPList.SortCommand
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
        displayData(dtgSAPList.CurrentPageIndex)
    End Sub
End Class
