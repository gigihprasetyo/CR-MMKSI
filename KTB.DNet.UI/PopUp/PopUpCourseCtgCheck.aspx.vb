#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports GlobalExtensions
#End Region

Public Class PopUpCourseCtgCheck
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
            
        End If
    End Sub

    Private Sub InitiatePage()
        FillDropDownJobPosition()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub FillDropDownJobPosition()
        ddlJobPositionCategory.Items.Clear()

        If String.IsNullOrEmpty(Request.QueryString("category")) Then
            Dim listItem As New ListItem("Silakan Pilih", "-1")
            ddlJobPositionCategory.Items.Add(listItem)
            rowCategory.Visible = False
            Exit Sub
        End If

        Select Case Request.QueryString("category").ToString()
            Case String.Empty
                Dim listItem As New ListItem("Silakan Pilih", "-1")
                ddlJobPositionCategory.Items.Add(listItem)
            Case "sales"
                Dim listItem As New ListItem("Sales", "1")
                ddlJobPositionCategory.Items.Add(listItem)
            Case "ass"
                Dim listItem As New ListItem("Silakan Pilih", "-1")
                ddlJobPositionCategory.Items.Add(listItem)

                listItem = New ListItem("Sparepart", "0")
                ddlJobPositionCategory.Items.Add(listItem)

                listItem = New ListItem("Service", "2")
                ddlJobPositionCategory.Items.Add(listItem)

                listItem = New ListItem("Body Paint", "3")
                ddlJobPositionCategory.Items.Add(listItem)
            Case "cs"
                Dim listItem As New ListItem("Customer Satisfaction", "4")
                ddlJobPositionCategory.Items.Add(listItem)
        End Select

    End Sub

    Private Sub ClearData()
        Me.txtKodeKategori.Text = String.Empty
        Me.txtNamaKategori.Text = String.Empty
        ddlStatus.SelectedIndex = 0
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourseCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeKategori.IsNotEmpty Then
            criterias.opAnd(New Criteria(GetType(TrCourseCategory), "Code", MatchType.[Partial], txtKodeKategori.Text))
        End If
        If txtNamaKategori.IsNotEmpty Then
            criterias.opAnd(New Criteria(GetType(TrCourseCategory), "Description", MatchType.[Partial], txtNamaKategori.Text))
        End If

        If ddlJobPositionCategory.IsSelected Then
            criterias.opAnd(New Criteria(GetType(TrCourseCategory), "JobPositionCategory.ID", MatchType.Exact, ddlJobPositionCategory.SelectedValue))
        Else
            Dim ctgIn As String = String.Empty
            For Each item As ListItem In ddlJobPositionCategory.Items
                If Not item.Value.Equals("-1") Then
                    ctgIn = ctgIn & item.Value & ", "
                End If
            Next
            ctgIn = String.Format("({0})", ctgIn.Remove(ctgIn.Length - 2))
            criterias.opAnd(New Criteria(GetType(TrCourseCategory), "JobPositionCategory.ID", MatchType.InSet, ctgIn))
        End If

        criterias.opAnd(New Criteria(GetType(TrCourseCategory), "Status", MatchType.Exact, CType(EnumTrDataStatus.DataStatusType.Active, String)))

        Return criterias
    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        dtgCourseSelection.DataSource = New TrCourseCategoryFacade(User).RetrieveActiveList(indexPage + 1, dtgCourseSelection.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection), _
            CriteriaSearch())
        dtgCourseSelection.VirtualItemCount = totalRow
        dtgCourseSelection.DataBind()
    End Sub

    Private Sub dtgDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCourseSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As TrCourseCategory = CType(e.Item.DataItem, TrCourseCategory)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                Dim lblMandatory As Label = CType(e.Item.FindControl("lblMandatory"), Label)

                If Not IsNothing(RowValue.Status) Then
                    If RowValue.Status = "1" Then
                        lblStatus.Text = "Aktif"
                    Else
                        lblStatus.Text = "Tidak Aktif"
                    End If
                End If

                If Not IsNothing(RowValue.IsMandatory) Then
                    If RowValue.IsMandatory Then
                        lblMandatory.Text = "Wajib"
                    Else
                        lblMandatory.Text = "Tidak Wajib"
                    End If

                End If

            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)

        If dtgCourseSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub


    Private Sub dtgCourseSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCourseSelection.PageIndexChanged
        dtgCourseSelection.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgCourseSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgCourseSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCourseSelection.SortCommand
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

        dtgCourseSelection.CurrentPageIndex = 0
        BindDataGrid(dtgCourseSelection.CurrentPageIndex)
    End Sub
End Class
