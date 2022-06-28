Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.City

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class PopUpCityMultiple
    Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
            ViewState.Add("CityCode", CStr(Request.QueryString("CityCode")))

            Initialize()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub dgCity_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCity.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As City = CType(e.Item.DataItem, City)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim strCityCode As String() = CType(ViewState("CityCode"), String).Split(";")
                Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)

                For Each SalesCode As String In strCityCode
                    If SalesCode = RowValue.CityCode Then
                        chkItemChecked.Checked = True
                        Exit For
                    End If
                Next

            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgCity.CurrentPageIndex = 0
        BindDataGrid(0)

    End Sub

    Private Sub dgCity_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dgCity.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgCity.CurrentPageIndex)
    End Sub

    Private Sub dgCity_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs)
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
        dgCity.SelectedIndex = -1
        dgCity.CurrentPageIndex = 0
        BindDataGrid(dgCity.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.No, "X"))
        
        If txtProvinsi.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(City), "Province.ProvinceName", MatchType.[Partial], txtProvinsi.Text))
        End If

        If txtKota.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(City), "CityName", MatchType.[Partial], txtKota.Text))
        End If

        arrList = New KTB.DNet.BusinessFacade.General.CityFacade(User).RetrieveActiveList(criterias, idxPage + 1, dgCity.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgCity.DataSource = arrList
        dgCity.VirtualItemCount = totalRow
        dgCity.DataBind()
    End Sub

    Private Sub Initialize()

        ViewState("CurrentSortColumn") = "CityName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Insert")
    End Sub


    Private Sub dgCity_PageIndexChanged1(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgCity.PageIndexChanged
        dgCity.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgCity.CurrentPageIndex)
    End Sub

End Class