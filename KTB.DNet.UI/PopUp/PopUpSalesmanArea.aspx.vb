Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class PopUpSalesmanArea
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtAreaCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtCity As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtAreaDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dgSalesmanArea As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden

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

        'Put user code to initialize the page here
        If Not IsPostBack Then
            Initialize()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgSalesmanArea.CurrentPageIndex = 0
        BindDataGrid(0)

    End Sub

    Private Sub dgSalesmanArea_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanArea.PageIndexChanged
        dgSalesmanArea.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanArea.CurrentPageIndex)
    End Sub

    Private Sub dgSalesmanArea_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanArea.SortCommand
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
        dgSalesmanArea.SelectedIndex = -1
        dgSalesmanArea.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanArea.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtAreaCode.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SalesmanArea), "AreaCode", MatchType.[Partial], txtAreaCode.Text.Replace("'", "").Trim))
        End If
        If txtAreaDesc.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SalesmanArea), "AreaDesc", MatchType.[Partial], txtAreaDesc.Text.Replace("'", "").Trim))
        End If
        If txtCity.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SalesmanArea), "City", MatchType.[Partial], txtCity.Text.Replace("'", "").Trim))
        End If
        arrList = New SalesmanAreaFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgSalesmanArea.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesmanArea.DataSource = arrList
        dgSalesmanArea.VirtualItemCount = totalRow
        dgSalesmanArea.DataBind()
    End Sub

    Private Sub Initialize()
        txtAreaCode.Text = ""
        txtAreaDesc.Text = ""
        txtCity.Text = ""
        ViewState("CurrentSortColumn") = "AreaCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim arrSalesmanAreaAssigned As ArrayList = New ArrayList
        For Each item As DataGridItem In dgSalesmanArea.Items

            Dim chk As CheckBox = item.FindControl("chkItemChecked")
            If chk.Checked Then
                Dim lblKodeArea As Label = item.FindControl("lblKodeArea")

                Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanHeader.ID", MatchType.Exact, sesshelper.GetSession("HeaderId")))
                criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanArea.AreaCode", MatchType.Exact, lblKodeArea.Text))

                Dim arr As ArrayList = New SalesmanAreaAssignFacade(User).RetrieveByCriteria(criterias)
                If arr.Count = 0 Then
                    Dim objSalesmanArea As SalesmanArea = New SalesmanAreaFacade(User).Retrieve(lblKodeArea.Text)
                    arrSalesmanAreaAssigned.Add(objSalesmanArea)
                End If

            End If

        Next



        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(sesshelper.GetSession("HeaderId")))
        Dim result As Integer = New SalesmanAreaAssignFacade(User).InsertTransaction(objSalesmanHeader, arrSalesmanAreaAssigned)
        'Todo session
        'Session("afterinsertarea") = "true"
        sesshelper.SetSession("afterinsertarea", "true")
        RegisterClientScriptBlock("WindowClose", "<script language=JavaScript>window.close();</script>")
    End Sub

    Private Sub dgSalesmanArea_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanArea.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arrList Is Nothing) Then
                Dim objArea As SalesmanArea
                objArea = arrList(e.Item.ItemIndex)

                Dim lblKodeArea As Label = e.Item.FindControl("lblKodeArea")
                lblKodeArea.Text = objArea.AreaCode

                Dim lblDescArea As Label = e.Item.FindControl("lblDescArea")
                lblDescArea.Text = objArea.AreaDesc


            End If
        End If
    End Sub
End Class
