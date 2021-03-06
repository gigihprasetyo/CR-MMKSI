Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class PopUpSalesmanMultiple
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    'Protected WithEvents dgSalesman As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents txtSalesmanCode As System.Web.UI.WebControls.TextBox
    'Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    'Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtNoKTP As System.Web.UI.WebControls.TextBox

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
            ViewState.Add("SalesmanCode", CStr(Request.QueryString("SalesmanCode")))

            Initialize()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub dgSalesman_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesman.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As SalesmanHeader = CType(e.Item.DataItem, SalesmanHeader)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim strSalesmanCode As String() = CType(ViewState("SalesmanCode"), String).Split(";")
                Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)

                For Each SalesCode As String In strSalesmanCode
                    If SalesCode = RowValue.SalesmanCode Then
                        chkItemChecked.Checked = True
                        Exit For
                    End If
                Next

            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgSalesman.CurrentPageIndex = 0
        BindDataGrid(0)

    End Sub

    Private Sub dgSalesman_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dgSalesman.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesman.CurrentPageIndex)
    End Sub

    Private Sub dgSalesman_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs)
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
        dgSalesman.SelectedIndex = -1
        dgSalesman.CurrentPageIndex = 0
        BindDataGrid(dgSalesman.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim objDealer As Dealer = (CType(sesshelper.GetSession("Dealer"), Dealer))
        Dim positionId As Integer = Request.QueryString("PositionID")
        Dim oDealerSalesman As String = Request.QueryString("DealerSalesman")
        Dim isPosision As String = "1"
        If Not IsNothing(Request.QueryString("IsPosition")) Then
            isPosision = Request.QueryString("IsPosition")
        End If


        If oDealerSalesman.Length > 0 Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                objDealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(oDealerSalesman)
            End If
        End If

        'Salesman and salesman counter treated as a same level
        If positionId = 2 Then
            positionId = 3
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.No, ""))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.InSet, "(2,4)"))
        'If isPosision = "1" Then
        '    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.Greater, positionId))
        '    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.LesserOrEqual, positionId + 2))
        'End If
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.No, CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String)))
        'special for salesman unit
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, 1))
        If txtSalesmanCode.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.[Partial], txtSalesmanCode.Text))
        End If
        If txtName.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Name", MatchType.[Partial], txtName.Text))
        End If

        If txtNoKTP.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "NoKTP.ProfileHeader.ID", MatchType.Exact, 29))
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "NoKTP.ProfileGroup.ID", MatchType.Exact, 13))
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "NoKTP.ProfileValue", MatchType.Partial, txtNoKTP.Text))
        End If


        arrList = New SalesmanHeaderFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgSalesman.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesman.DataSource = arrList
        dgSalesman.VirtualItemCount = totalRow
        dgSalesman.DataBind()
    End Sub

    Private Sub Initialize()

        ViewState("CurrentSortColumn") = "SalesmanCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Insert")
    End Sub


    Private Sub dgSalesman_PageIndexChanged1(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesman.PageIndexChanged
        dgSalesman.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesman.CurrentPageIndex)
    End Sub

End Class