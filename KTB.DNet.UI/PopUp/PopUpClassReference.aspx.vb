#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.Utility
#End Region

Public Class PopUpClassReference
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

    Protected countChk As Integer = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ClassCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub


    Public Function CriteriaSearch() As CriteriaComposite
        Dim CourseCode As String = Request.QueryString("coursecode")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClass), "TrCourse.CourseCode", MatchType.Exact, CourseCode))

        criterias.opAnd(New Criteria(GetType(TrClass), "Status", MatchType.Exact, CType(EnumTrDataStatus.DataStatusType.Active, String)))

        Return criterias
    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        dtgClassReference.DataSource = New TrClassFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dtgClassReference.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgClassReference.VirtualItemCount = totalRow
        dtgClassReference.DataBind()
    End Sub

    Private Sub dtgDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassReference.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As TrClass = CType(e.Item.DataItem, TrClass)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

                If Not IsNothing(RowValue.Status) Then
                    If RowValue.Status = "1" Then
                        lblStatus.Text = "Aktif"
                    Else
                        lblStatus.Text = "Tidak Aktif"
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dtgClassreference_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClassReference.PageIndexChanged
        dtgClassReference.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgClassReference.CurrentPageIndex)
    End Sub

    Private Sub dtgClassreference_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClassReference.SortCommand
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

        dtgClassReference.CurrentPageIndex = 0
        BindDataGrid(dtgClassReference.CurrentPageIndex)
    End Sub
End Class
