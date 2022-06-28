Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility

Public Class PopUpClassRegistration
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cfClassRegistration As FilterCompositeControl.CompositeFilter
    Protected WithEvents dtgClassRegistration As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
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


            If Not IsNothing(Request.QueryString("ClassCode")) Then
                Dim strClassCode As String = CType(Request.QueryString("ClassCode"), String)
                _sessHelper.SetSession("sessClassCode", strClassCode)
                InitialPage(Request.QueryString("ClassCode"))
            End If


        End If
    End Sub
    Private Sub InitialPage(ByVal strClassCode As String)
        ViewState("currSortColumn") = "RegistrationCode"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        BindDgClass(1, strClassCode)
    End Sub

    Private Sub dtgClassRegistration_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegistration.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub

    Private Sub BindDgClass(ByVal pageIndeks As Integer, ByVal ClassCode As String)
        Dim totalRow As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClass), "ClassCode", MatchType.Exact, ClassCode))

        criterias.opAnd(New Criteria(GetType(TrClass), "ClassCode", MatchType.Exact, ClassCode))
        Dim arrList As ArrayList = New TrClassFacade(User).Retrieve(criterias)
        Dim objTrClass As TrClass = New TrClass
        If arrList.Count > 0 Then
            objTrClass = CType(arrList(0), TrClass)

        End If

        Dim o As TrClassRegistration = New TrClassRegistration


        Dim criteriasClassReg As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasClassReg.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, objTrClass.ID))
        'criteriasClassReg.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Register, String)))

        If cfClassRegistration.ColumnName = "ALL" Then
            dtgClassRegistration.DataSource = New TrClassRegistrationFacade(User).RetrieveActiveList(pageIndeks, dtgClassRegistration.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), criteriasClassReg)
        Else
            dtgClassRegistration.DataSource = New TrClassRegistrationFacade(User).RetrieveWithOneCriteria2(pageIndeks, dtgClassRegistration.PageSize, totalRow, cfClassRegistration.ColumnName, cfClassRegistration.OperatorName, cfClassRegistration.KeyWord, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), objTrClass.ID)
        End If
        dtgClassRegistration.VirtualItemCount = totalRow
        dtgClassRegistration.DataBind()
    End Sub


    Private Sub dtgClassRegistration_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClassRegistration.PageIndexChanged
        dtgClassRegistration.CurrentPageIndex = e.NewPageIndex
        BindDgClass(e.NewPageIndex + 1, CType(_sessHelper.GetSession("sessClassCode"), String))
    End Sub

    Private Sub cfClassCourse_Filter(ByVal sender As Object, ByVal FilterArg As FilterCompositeControl.OnFilterArgs) Handles cfClassRegistration.Filter
        dtgClassRegistration.CurrentPageIndex = 0
        BindDgClass(1, CType(_sessHelper.GetSession("sessClassCode"), String))
    End Sub

    Private Sub dtgClassCourse_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClassRegistration.SortCommand
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

        BindDgClass(dtgClassRegistration.CurrentPageIndex + 1, CType(_sessHelper.GetSession("sessClassCode"), String))
    End Sub


End Class
