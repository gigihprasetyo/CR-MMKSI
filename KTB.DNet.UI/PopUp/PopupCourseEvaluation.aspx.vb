Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility

Public Class PopupCourseEvaluation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents cfCourseEval As FilterCompositeControl.CompositeFilter
    Protected WithEvents dtgCourseEval As System.Web.UI.WebControls.DataGrid
    Private _sessHelper As SessionHelper = New SessionHelper

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("RegNo")) Then
                If (Request.QueryString("RegNo")) <> "" Then
                    Dim strRegNo As String = CType(Request.QueryString("RegNo"), String)
                    Try
                        Dim i As Integer = CInt(strRegNo.Trim())
                    Catch
                        MessageBox.Show("Nomor Registrasi tidak boleh karakter.")
                        Exit Sub
                    End Try
                    _sessHelper.SetSession("sess_strRegNo", strRegNo)
                    InitialPage(Request.QueryString("RegNo"))
                End If
            End If
        End If
    End Sub
    Private Sub InitialPage(ByVal strRegNo As String)
        ViewState("currSortColumn") = "Type"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        BindDgEvaluation(1, strRegNo)
    End Sub

    Private Sub dtgClassRegistration_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCourseEval.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub

    Private Sub BindDgEvaluation(ByVal pageIndeks As Integer, ByVal strRegNo As String)
        Dim totalRow As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If strRegNo <> "" Then
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "ID", MatchType.Exact, strRegNo))
        End If

        Dim arrList As ArrayList = New TrClassRegistrationFacade(User).Retrieve(criterias)
        Dim objTrClassReg As TrClassRegistration = New TrClassRegistration
        If arrList.Count > 0 Then

            objTrClassReg = CType(arrList(0), TrClassRegistration)

            Dim critCourseEval As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If Not IsNothing(objTrClassReg) Then
                critCourseEval.opAnd(New Criteria(GetType(TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objTrClassReg.TrClass.TrCourse.ID))
            End If


            If cfCourseEval.ColumnName = "ALL" Then
                dtgCourseEval.DataSource = New TrCourseEvaluationFacade(User).RetrieveActiveList(pageIndeks, dtgCourseEval.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), critCourseEval)
            Else
                dtgCourseEval.DataSource = New TrCourseEvaluationFacade(User).RetrieveWithOneCriteria2(pageIndeks, dtgCourseEval.PageSize, totalRow, cfCourseEval.ColumnName, cfCourseEval.OperatorName, cfCourseEval.KeyWord, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
            End If

            dtgCourseEval.VirtualItemCount = totalRow
            dtgCourseEval.DataBind()
        End If
    End Sub


    Private Sub dtgClassRegistration_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCourseEval.PageIndexChanged
        dtgCourseEval.CurrentPageIndex = e.NewPageIndex
        BindDgEvaluation(e.NewPageIndex + 1, CType(_sessHelper.GetSession("sess_strRegNo"), String))
    End Sub

    Private Sub cfClassCourse_Filter(ByVal sender As Object, ByVal FilterArg As FilterCompositeControl.OnFilterArgs) Handles cfCourseEval.Filter
        dtgCourseEval.CurrentPageIndex = 0
        BindDgEvaluation(1, CType(_sessHelper.GetSession("sess_strRegNo"), String))
    End Sub

    Private Sub dtgClassCourse_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCourseEval.SortCommand
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

        BindDgEvaluation(dtgCourseEval.CurrentPageIndex + 1, CType(_sessHelper.GetSession("sess_strRegNo"), String))
    End Sub



End Class



