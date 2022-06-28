Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class PopUpRefDocumentPQR
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents dgPQR As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtPQRNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private arrList As New ArrayList
    Private sesshelper As SessionHelper = New SessionHelper

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ViewState("ClaimType") = Request.QueryString("ClaimType")
        'Put user code to initialize the page here
        If Not IsPostBack Then
            Initialize()
            ViewState("CurrentSortColumn") = "CreatedTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            dgPQR.CurrentPageIndex = 0
            BindDataGrid(dgPQR.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgPQR.CurrentPageIndex = 0
        BindDataGrid(dgPQR.CurrentPageIndex)

    End Sub

    Private Sub dgPQR_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPQR.PageIndexChanged
        dgPQR.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgPQR.CurrentPageIndex)
    End Sub

    Private Sub dgPQR_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPQR.SortCommand
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
        dgPQR.SelectedIndex = -1
        BindDataGrid(dgPQR.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        'Dim str As String = "select pqr from wscheader where rowstatus = 0"
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

        If ViewState("ClaimType") = "Z6" Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.NotInSet, "'" & CType(DBRowStatus.Deleted, Short) & "','" & EnumPQR.PQRStatus.Batal & "'"))
            'criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "PQRNo", MatchType.NotInSet, str))
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "ChassisMasterBB.Category.ProductCategory.Code", MatchType.Exact, companyCode))
            criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "Dealer.ID", MatchType.Exact, CType(sesshelper.GetSession("DEALER"), Dealer).ID))
            If txtPQRNo.Text.Trim <> "" Then
                criterias.opAnd(New Criteria(GetType(PQRHeaderBB), "PQRNo", MatchType.Partial, txtPQRNo.Text))
            End If

            arrList = New PQRHeaderBBFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgPQR.PageSize, totalRow, _
                            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        Else
            Dim criterias As New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.NotInSet, "'" & CType(DBRowStatus.Deleted, Short) & "','" & EnumPQR.PQRStatus.Batal & "'"))
            'criterias.opAnd(New Criteria(GetType(PQRHeader), "PQRNo", MatchType.NotInSet, str))
            If ViewState("ClaimType") = "ZA" Then
                criterias.opAnd(New Criteria(GetType(PQRHeader), "PQRType", MatchType.InSet, "(" & CType(EnumPQRType.PQRType.SparePart, Short) & "," & CType(EnumPQRType.PQRType.Accessories, Short) & ")"))
            ElseIf ViewState("ClaimType") = "ZB" Then
                criterias.opAnd(New Criteria(GetType(PQRHeader), "PQRType", MatchType.Exact, CType(EnumPQRType.PQRType.PQR_ESP, Short)))
            Else
                criterias.opAnd(New Criteria(GetType(PQRHeader), "PQRType", MatchType.Exact, CType(EnumPQRType.PQRType.PQR_WSC, Short)))
            End If
            criterias.opAnd(New Criteria(GetType(PQRHeader), "ChassisMaster.Category.ProductCategory.Code", MatchType.Exact, companyCode))
            criterias.opAnd(New Criteria(GetType(PQRHeader), "Dealer.ID", MatchType.Exact, CType(sesshelper.GetSession("DEALER"), Dealer).ID))
            If txtPQRNo.Text.Trim <> "" Then
                criterias.opAnd(New Criteria(GetType(PQRHeader), "PQRNo", MatchType.Partial, txtPQRNo.Text))
            End If

            arrList = New PQRHeaderFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgPQR.PageSize, totalRow, _
                            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        End If

        dgPQR.DataSource = arrList
        dgPQR.VirtualItemCount = totalRow
        dgPQR.DataBind()
    End Sub

    Private Sub Initialize()

        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Insert")
    End Sub


    Private Sub dgPQR_PageIndexChanged1(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPQR.PageIndexChanged
        dgPQR.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgPQR.CurrentPageIndex)
    End Sub
End Class
