Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper

Public Class PopUpClassSelectionMany
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgClassCourse As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cfClassCourse As FilterCompositeControl.CompositeFilter

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private _sessHelper As SessionHelper = New SessionHelper

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            InitialPage()
            If Not IsNothing(Request.QueryString("Year")) Then
                'Dim str_tmp As String = CType(Request.QueryString("Year"), String).Trim()
                Dim dummyParam As String() = CType(Request.QueryString("Year"), String).Trim().Split(";")
                If dummyParam.Length > 0 Then
                    If Not dummyParam(0) = String.Empty Then
                        'MessageBox.Show(dummyParam(0))
                        _sessHelper.SetSession("SessTahun", dummyParam(0))
                    End If
                End If
                If dummyParam.Length > 1 Then
                    If Not dummyParam(1) = String.Empty Then
                        'MessageBox.Show(dummyParam(1))
                        _sessHelper.SetSession("SessCourseId", dummyParam(1))
                    End If
                End If
            End If

            If Not IsNothing(Request.QueryString("areaid")) Then
                _sessHelper.SetSession("AreaID", Request.QueryString("areaid"))
            End If

            If Not IsNothing(Request.QueryString("FiscalYear")) Then
                _sessHelper.SetSession("FiscalYear", Request.QueryString("FiscalYear"))
            End If

            If Not IsNothing(Request.QueryString("CourseCode")) Then
                If Not String.IsNullorEmpty(Request.QueryString("CourseCode").ToString()) Then
                    _sessHelper.SetSession("CourseCode", Request.QueryString("CourseCode"))
                End If
            End If
            BindDgClass(1)

        End If

    End Sub

    Private Sub InitialPage()
        ViewState("currSortColumn") = "ClassCode"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindClass()
        dtgClassCourse.DataSource = New TrClassFacade(User).RetrieveActiveList()
        dtgClassCourse.DataBind()
    End Sub

    Private Sub dtgClassCourse_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassCourse.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            'e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub

    Private Sub BindDgClass(ByVal pageIndeks As Integer)
        Dim totalRow As Integer = 0
        Dim critClass As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not _sessHelper.GetSession("SessCourseId") Is Nothing Then
            critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.ID", MatchType.Exact, CType(_sessHelper.GetSession("SessCourseId"), Integer)))
        End If

        If Not _sessHelper.GetSession("CourseCode") Is Nothing Then
            Dim courseCode As String = CType(_sessHelper.GetSession("CourseCode"), String)
            critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.CourseCode", MatchType.InSet, "('" & courseCode.Replace(";", "','") & "')"))
        End If

        If Not _sessHelper.GetSession("SessTahun") Is Nothing Then
            Dim startDate As Date = New DateTime(CType(_sessHelper.GetSession("SessTahun"), Integer), 12, Date.DaysInMonth(CType(_sessHelper.GetSession("SessTahun"), Integer), 12), 23, 59, 59)
            Dim finishDate As Date = New DateTime(CType(_sessHelper.GetSession("SessTahun"), Integer), 1, 1, 0, 0, 0)
            critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "StartDate", MatchType.LesserOrEqual, startDate))
            critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "FinishDate", MatchType.GreaterOrEqual, finishDate))

        End If

        If Not _sessHelper.GetSession("AreaID") Is Nothing Then
            critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.JobPositionCategory.AreaID", MatchType.Exact, _sessHelper.GetSession("AreaID").ToString()))
        End If

        If Not _sessHelper.GetSession("FiscalYear") Is Nothing Then
            critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "FiscalYear", MatchType.Exact, _sessHelper.GetSession("FiscalYear").ToString()))
        End If

        If cfClassCourse.ColumnName = "ALL" Then
            dtgClassCourse.DataSource = New TrClassFacade(User).RetrieveActiveList(critClass, pageIndeks, dtgClassCourse.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        Else
            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critClass.opAnd(New Criteria(GetType(TrClass), cfClassCourse.ColumnName, cfClassCourse.OperatorName, cfClassCourse.KeyWord))
            dtgClassCourse.DataSource = New TrClassFacade(User).RetrieveWithOneCriteria(critClass, pageIndeks, dtgClassCourse.PageSize, totalRow, cfClassCourse.ColumnName, cfClassCourse.OperatorName, cfClassCourse.KeyWord, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        End If
        dtgClassCourse.VirtualItemCount = totalRow
        dtgClassCourse.DataBind()
    End Sub

    Private Sub dtgClassCourse_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClassCourse.PageIndexChanged
        dtgClassCourse.CurrentPageIndex = e.NewPageIndex
        BindDgClass(e.NewPageIndex + 1)
    End Sub

    Private Sub cfClassCourse_Filter(ByVal sender As Object, ByVal FilterArg As FilterCompositeControl.OnFilterArgs) Handles cfClassCourse.Filter
        dtgClassCourse.CurrentPageIndex = 0
        BindDgClass(1)
    End Sub

    Private Sub dtgClassCourse_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClassCourse.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select

        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        BindDgClass(dtgClassCourse.CurrentPageIndex + 1)
    End Sub

End Class
