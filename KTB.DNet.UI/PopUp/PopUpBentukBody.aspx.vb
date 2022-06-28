#Region "Custom NameSpace Import"
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
#End Region

Public Class PopUpBentukBody
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgBody As System.Web.UI.WebControls.DataGrid
    Protected WithEvents LblKeterangan As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            Dim category As String = Request.QueryString("cat")
            BindDataGrid(category)
        End If
    End Sub

    Private Sub BindDataGrid(ByVal category As String)
        Dim CollVehicleBody As New ArrayList
        Dim kode As String = String.Empty
        If IsNumeric(category) Then
            Dim cat As Short = CInt(category)
            If cat > 1 Then
                Select Case cat
                    Case 3
                        kode = "CBU_BODYTYPE1"
                    Case 2
                        kode = "CBU_BODYTYPELCV1"
                End Select
                Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(ProfileDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New criteria(GetType(ProfileDetail), "ProfileHeader.Code", MatchType.Exact, kode))
                CollVehicleBody = New ProfileDetailFacade(User).Retrieve(criteria)
            End If
        End If

        dtgBody.DataSource = CollVehicleBody
        dtgBody.DataBind()
    End Sub

    Private Sub dtgBody_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBody.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgBody.CurrentPageIndex * dtgBody.PageSize)
            e.Item.Cells(2).Controls.Add(rdbChoice)
        End If
    End Sub
End Class
