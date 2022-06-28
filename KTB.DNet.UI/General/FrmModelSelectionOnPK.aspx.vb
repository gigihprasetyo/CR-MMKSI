Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain

Public Class FrmModelSelectionOnPK
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgVechileType As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents txtKodeTipe As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim category As String = "0"
    Dim subCategory As String = "-1"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsNothing(Request.QueryString("cat")) Then
            category = Request.QueryString("cat")
        End If
        If Not IsNothing(Request.QueryString("subCat")) Then
            subCategory = Request.QueryString("subCat")
        End If

        If Not Page.IsPostBack Then
            ViewState("SortCol") = "VechileTypeCode"
            ViewState("SortDir") = "ASC"

            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub dtgVechileType_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgVechileType.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgVechileType.CurrentPageIndex * dtgVechileType.PageSize)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(category, subCategory)
        If dtgVechileType.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub BindDataGrid(ByVal category As String, ByVal strSubCategory As String)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
        If category <> "0" Then
            Dim cat As Short = CInt(category)
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, cat))
        End If

        If strSubCategory <> "-1" Then
            Dim strSql As String = String.Format("" & vbCrLf &
                "SELECT A.VechileModelID FROM SubCategoryVehicleToModel AS A " & vbCrLf &
                "INNER JOIN SubCategoryVehicle B ON B.ID = A.SubCategoryVehicleID " & vbCrLf &
                "INNER JOIN VechileModel AS C ON C.ID = A.VechileModelID " & vbCrLf &
                "WHERE 1=1 " & vbCrLf &
                "AND A.RowStatus=0 " & vbCrLf &
                "AND B.RowStatus=0 " & vbCrLf &
                "AND C.RowStatus=0 " & vbCrLf &
                "AND A.SubCategoryVehicleID={0} ", strSubCategory)
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If txtKodeTipe.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, txtKodeTipe.Text.Trim))
        End If
        If txtDeskripsi.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(VechileType), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
        End If
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(VechileType), ViewState("SortCol").ToString(), [Enum].Parse(GetType(Sort.SortDirection), ViewState("SortDir").ToString())))

        dtgVechileType.DataSource = New VechileTypeFacade(User).Retrieve(criterias, sortColl)
        dtgVechileType.DataBind()
    End Sub
End Class
