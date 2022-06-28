Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI

Public Class FrmPrintTrTrainee
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTraineeName As System.Web.UI.WebControls.Label
    Protected WithEvents photoView As System.Web.UI.WebControls.Image
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents dtgCourseClass As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCetak As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblJobPosition As System.Web.UI.WebControls.Label
    Protected WithEvents lblEducationLevel As System.Web.UI.WebControls.Label
    Protected WithEvents lblShirtSize As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sHTrainee As SessionHelper = New SessionHelper
    Dim objTrainee As TrTrainee
    Dim sessDealer As Dealer


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objTrainee = sHTrainee.GetSession("veTrainee")

        If IsNothing(objTrainee) Then
            Response.Redirect("../Login.aspx#Expired")
        End If
        InitiatePage()
        FillFormFromObject(objTrainee)
        BindDataToPage()
    End Sub

    Private Sub InitiatePage()
        sHTrainee.RemoveSession("dtRepeater")

        Dim objEnumTrTrainee = New EnumTrTrainee

        ViewState("CurrentSortColumn") = "TrClass.TrCourse.CourseCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDataToPage()
        LoadRepeater()
    End Sub

    Private Sub FillFormFromObject(ByVal objTrainee As TrTrainee)
        Me.lblTraineeName.Text = objTrainee.Name
        Me.lblDealerName.Text = objTrainee.Dealer.DealerName
        Me.lblCity.Text = objTrainee.Dealer.City.CityName
        Me.lblStartDate.Text = objTrainee.StartWorkingDate.ToShortDateString
        Me.lblJobPosition.Text = objTrainee.JobPosition
        Me.lblEducationLevel.Text = objTrainee.EducationLevel
        Me.lblShirtSize.Text = objTrainee.ShirtSize
        Me.lblStatus.Text = New EnumTrTrainee().StatusByIndex(objTrainee.Status)
        Me.photoView.ImageUrl = "../WebResources/GetPhoto.aspx?id=" + objTrainee.ID.ToString
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("./FrmDetailSiswa.aspx?category=ass")
    End Sub

    Private Sub LoadRepeater()

        Dim ds As ArrayList = sHTrainee.GetSession("dtRepeater")

        If IsNothing(ds) Then

            ds = objTrainee.TrClassRegistrations
            CleanUpUngraduatedClass(ds)
            sHTrainee.SetSession("dtRepeater", ds)
        End If

        If Not IsNothing(ds) Then
            Dim isAsc As Boolean = ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Dim iCmp As IComparer = New ListComparer(isAsc, ViewState("CurrentSortColumn"))
            ds.Sort(iCmp)
        End If

        dtgCourseClass.DataSource = ds
        dtgCourseClass.DataBind()

    End Sub

    Private Sub CleanUpUngraduatedClass(ByVal classes As ArrayList)
        If Not IsNothing(classes) Then
            For idx As Integer = classes.Count - 1 To 0 Step -1
                Dim item As TrClassRegistration = classes(idx)
                If item.Status <> EnumTrClassRegistration.DataStatusType.Pass Then
                    classes.Remove(item)
                End If
            Next
        End If
    End Sub

    Private Function ConvertSortedListToDataTable(ByVal slInput As SortedList) As DataTable
        Dim nResult As DataTable = New DataTable

        Dim dtCol As DataColumn = New DataColumn("Category", GetType(String))
        nResult.Columns.Add(dtCol)
        dtCol = New DataColumn("ClassList", GetType(String))
        nResult.Columns.Add(dtCol)

        Dim dtRow As DataRow
        For idx As Integer = 0 To slInput.Count - 1
            dtRow = nResult.NewRow()
            dtRow(0) = slInput.GetKey(idx)
            dtRow(1) = slInput.GetByIndex(idx)
            nResult.Rows.Add(dtRow)
        Next
        Return nResult
    End Function

    Private Sub dtgCourseClass_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCourseClass.SortCommand
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

        dtgCourseClass.SelectedIndex = -1
        dtgCourseClass.CurrentPageIndex = 0

        BindDataToPage()
    End Sub

    Private Sub dtgCourseClass_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCourseClass.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not e.Item.DataItem Is Nothing Then
                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgCourseClass.CurrentPageIndex * dtgCourseClass.PageSize)
                Dim RowValue As TrClassRegistration = CType(e.Item.DataItem, TrClassRegistration)

                Dim hlClass As HyperLink = CType(e.Item.FindControl("hlClass"), HyperLink)

                If Not IsNothing(RowValue.TrClass) And Not IsNothing(hlClass) Then
                    Dim actionValue As String = "popUpClassInformation('" + RowValue.TrClass.ClassCode + "');"
                    hlClass.Text = RowValue.TrClass.ClassCode
                    hlClass.NavigateUrl = "javascript:" + actionValue
                End If
            End If
        End If
    End Sub

End Class
