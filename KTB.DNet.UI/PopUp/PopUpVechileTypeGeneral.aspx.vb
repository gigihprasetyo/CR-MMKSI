#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade

#End Region

Public Class PopUpVechileTypeGeneral
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton

    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgTipeGeneral As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hdnSubCategoryVehicleID As System.Web.UI.WebControls.HiddenField

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variables Declaration"
    Private sessHelper As New SessionHelper
    Private objDealer As New Dealer
#End Region

#Region "Custom Method"
    Private Sub ClearData()
        Me.txtName.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        dtgTipeGeneral.DataSource = New VechileTypeGeneralFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, _
            dtgTipeGeneral.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgTipeGeneral.VirtualItemCount = totalRow
        dtgTipeGeneral.DataBind()
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileTypeGeneral), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VechileTypeGeneral), "Status", MatchType.Exact, 1))

        If hdnSubCategoryVehicleID.value <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileTypeGeneral), "SubCategoryVehicle.ID", MatchType.Exact, hdnSubCategoryVehicleID.value))
        End If

        If txtName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileTypeGeneral), "Name", MatchType.[Partial], txtName.Text))
        End If

        Return criterias
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        hdnSubCategoryVehicleID.Value = Request.QueryString("SubCategoryVehicleID")

        If Not Page.IsPostBack Then
            InitiatePage()
            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
        If dtgTipeGeneral.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgTipeGeneral_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTipeGeneral.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgTipeGeneral.CurrentPageIndex * dtgTipeGeneral.PageSize)
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim objVechileTypeGeneral As VechileTypeGeneral = New VechileTypeGeneralFacade(User).Retrieve(CShort(lblID.Text))
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Try
                Dim strStatus As String = String.Empty
                If objVechileTypeGeneral.Status = 1 Then
                    strStatus = "Aktif"
                ElseIf objVechileTypeGeneral.Status = 0 Then
                    strStatus = "Tidak Aktif"
                Else
                    strStatus = ""
                End If
                lblStatus.Text = strStatus
            Catch
            End Try
        End If
    End Sub

    Private Sub dtgPosisi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgTipeGeneral.PageIndexChanged
        dtgTipeGeneral.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgTipeGeneral.CurrentPageIndex)
    End Sub

    Private Sub dtgPosisi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgTipeGeneral.SortCommand
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
        dtgTipeGeneral.CurrentPageIndex = 0
        BindDataGrid(dtgTipeGeneral.CurrentPageIndex)
    End Sub

End Class
