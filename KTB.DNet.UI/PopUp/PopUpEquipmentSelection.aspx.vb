
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade

#End Region

Public Class PopUpEquipmentSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents txtEquipmentNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSpecification As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgEquipmentList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgBOMUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private EquipmentlistArrayList As ArrayList
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("Kind") = Request.QueryString("Kind")
        End If
    End Sub

    Private Sub dgEquipmentList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgEquipmentList.PageIndexChanged
        dgEquipmentList.SelectedIndex = -1
        dgEquipmentList.CurrentPageIndex = e.NewPageIndex
        BindDataToGrid(dgEquipmentList.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom Method"

    Private Sub BindDataToGrid(ByVal indexpage As Integer)
        Dim totalrow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Status", MatchType.Exact, CType(EquipmentMasterStatus.EquipmentMasterStatusEnum.Aktive, Short)))
        If Not (ViewState("Kind")) Is Nothing Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Kind", MatchType.Exact, ViewState("Kind")))
        End If
        If txtEquipmentNumber.Text <> "" Then criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.EquipmentMaster), "EquipmentNumber", MatchType.StartsWith, txtEquipmentNumber.Text))
        If txtSpecification.Text <> "" Then
            Dim strSpec() As String = txtSpecification.Text.Split(";")
            For i As Integer = 0 To strSpec.Length - 1
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Specification", MatchType.[Partial], strSpec(i)))
            Next
        End If
        If txtDescription.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Description", MatchType.[Partial], txtDescription.Text))

        EquipmentlistArrayList = New EquipmentMasterFacade(User).RetrieveByCriteria(criterias, indexpage + 1, dgEquipmentList.PageSize, totalrow)
        If EquipmentlistArrayList.Count <> 0 Then
            dgEquipmentList.PagerStyle.Visible = True
            dgEquipmentList.VirtualItemCount = totalrow
            btnChoose.Disabled = False
        Else
            dgEquipmentList.PagerStyle.Visible = False
        End If
        dgEquipmentList.DataSource = EquipmentlistArrayList
        dgEquipmentList.DataBind()

    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataToGrid(dgEquipmentList.CurrentPageIndex)
    End Sub

    Private Sub dgEquipmentList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEquipmentList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not e.Item.DataItem Is Nothing Then
                Dim arlEquipmentMaster As New ArrayList
                arlEquipmentMaster.Add(e.Item.DataItem)
                Dim dtgEquipmentMaster As DataGrid = e.Item.FindControl("dtgHeader")
                dtgEquipmentMaster.DataSource = arlEquipmentMaster
                dtgEquipmentMaster.DataBind()
                If e.Item.ItemIndex <> 0 Then
                    dtgEquipmentMaster.ShowHeader = False
                End If
                Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.HeaderBOM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.HeaderBOM), "EquipmentMaster.ID", MatchType.Exact, CType(EquipmentlistArrayList(e.Item.ItemIndex), EquipmentMaster).ID))
                Dim arlHeaderBom As ArrayList = New BOMMaintenanceFacade(User).Retrieve(criterias)
                If arlHeaderBom.Count <> 0 Then
                    If arlHeaderBom(0).DetailBOMs.Count > 0 Then
                        Dim dtgBOMDetails As DataGrid = New DataGrid
                        dtgBOMDetails.Width = Unit.Percentage(90)
                        dtgBOMDetails.BorderWidth = Unit.Pixel(1)
                        dtgBOMDetails.CellPadding = 2
                        dtgBOMDetails.CellSpacing = 0
                        dtgBOMDetails.GridLines = GridLines.Horizontal
                        dtgBOMDetails.BorderColor = Color.FromName("Black")
                        dtgBOMDetails.HeaderStyle.BackColor = Color.FromName("#AAAAAA")
                        dtgBOMDetails.HeaderStyle.ForeColor = Color.FromName("Black")
                        dtgBOMDetails.HeaderStyle.Font.Bold = True
                        dtgBOMDetails.HeaderStyle.Font.Size = FontUnit.XSmall
                        dtgBOMDetails.ItemStyle.BackColor = Color.FromName("#DDDDDD")
                        dtgBOMDetails.ItemStyle.Font.Name = "Verdana"
                        dtgBOMDetails.ItemStyle.Font.Size = FontUnit.XSmall
                        'dtgBOMDetails.AlternatingItemStyle.BackColor = Color.FromName("Gainsboro")
                        dtgBOMDetails.AutoGenerateColumns = False

                        'Number
                        Dim _boundColumn As BoundColumn = New BoundColumn
                        _boundColumn.HeaderText = "Nama Komponen"
                        _boundColumn.DataField = "EquipmentNumber"
                        dtgBOMDetails.Columns.Add(_boundColumn)


                        'Description
                        _boundColumn = New BoundColumn
                        _boundColumn.HeaderText = "Deskripsi"
                        _boundColumn.DataField = "EquipmentDescription"
                        dtgBOMDetails.Columns.Add(_boundColumn)

                        'Quantity
                        _boundColumn = New BoundColumn
                        _boundColumn.HeaderText = "Quantity"
                        _boundColumn.DataField = "Quantity"
                        dtgBOMDetails.Columns.Add(_boundColumn)

                        'dtgBOMDetails.Columns.Add(_boundColumn)
                        dtgBOMDetails.DataSource = arlHeaderBom(0).DetailBOMs
                        dtgBOMDetails.DataBind()
                        e.Item.Cells(1).Controls.Add(dtgBOMDetails)
                    End If
                End If
            End If
        End If

        'If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
        '    Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
        '    e.Item.Cells(1).Controls.Add(rdbChoice)
        'End If
    End Sub

    Sub dtgMaster_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If e.Item.ItemIndex <> -1 Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub


#End Region

End Class
