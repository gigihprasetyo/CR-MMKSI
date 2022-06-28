Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain

Public Class FrmModelSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgVechileType As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private KodeModel As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            If Not IsNothing(Request.QueryString("Prod")) AndAlso Request.QueryString("Prod").ToString() <> "" Then
                Dim category As String = Request.QueryString("Prod")
                BindDataGrid2(CInt(category))
            Else
                Dim category As String = Request.QueryString("cat")
                Dim assemblyyear As String = Request.QueryString("assemblyyear")
                Dim modelID As String = String.Empty
                Try
                    modelID = Request.QueryString("modelID")
                Catch
                End Try

                BindDataGrid(category, assemblyyear, modelID)
            End If
           
            'BindDataGrid("2")
            'Todo session
            Session("KodeModel") = KodeModel
        End If

    End Sub

    Private Sub BindDataGrid(ByVal category As String)
        Dim CollVehicleModel As ArrayList
        If IsNumeric(category) Then
            Dim cat As Short = CInt(category)
            CollVehicleModel = New VechileTypeFacade(User).RetrieveModelList(cat)
        Else
            CollVehicleModel = New VechileTypeFacade(User).RetrieveActiveSortList()
        End If
        dtgVechileType.DataSource = CollVehicleModel
        dtgVechileType.DataBind()
    End Sub

    Private Sub BindDataGrid(ByVal category As String, ByVal assemblyyear As Integer, ByVal strModelID As String)
        Dim CollVehicleModel As ArrayList
        Dim _modelID As Short = CInt(strModelID)
        If IsNumeric(category) Then
            Dim cat As Short = CInt(category)

            CollVehicleModel = New VechileTypeFacade(User).RetrieveModelList(cat, assemblyyear, _modelID)
        Else
            CollVehicleModel = New VechileTypeFacade(User).RetrieveActiveSortList(_modelID)
        End If
        dtgVechileType.DataSource = CollVehicleModel
        dtgVechileType.DataBind()
    End Sub

    Private Sub BindDataGrid2(ByVal category As Integer)
        Dim CollVehicleModel As ArrayList
        CollVehicleModel = New VechileTypeFacade(User).RetrieveModelProductList(category)

        dtgVechileType.DataSource = CollVehicleModel
        dtgVechileType.DataBind()
    End Sub

    Private Sub dtgVechileType_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgVechileType.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgVechileType.CurrentPageIndex * dtgVechileType.PageSize)
            e.Item.Cells(2).Controls.Add(rdbChoice)
        End If
    End Sub

End Class
