#Region "Custom NameSpace Import"
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
#End Region


Public Class FrmKodeWarna
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgColor As System.Web.UI.WebControls.DataGrid
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

    Private PKtype As String
    Private arrayListVehicleColor As ArrayList


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim Model As String = Request.QueryString("type")
        Dim assemblyyear As Integer = Request.QueryString("assemblyyear")
        PKtype = Request.QueryString("pktype")
        If PKtype = "0" Then
            LblKeterangan.Text = "* ZZZZ : Kode Pilihan Warna Khusus"
        End If
        If Not IsPostBack Then
            If assemblyyear = 0 Then
                GetData(Model)
            Else
                GetData(Model, assemblyyear)
            End If
        End If
    End Sub

    Private Sub GetData(ByVal Model As String)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, Model))
        criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.VechileColor), "Status", MatchType.No, "x"))
        If PKtype <> "2" Then
            criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.VechileColor), "SpecialFlag", MatchType.No, "x"))
        End If
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("ColorCode")) Then
            sortColl.Add(New Sort(GetType(VechileColor), "ColorCode", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If
        arrayListVehicleColor = New FinishUnit.VechileColorFacade(User).Retrieve(criterias, sortColl)
        If PKtype = "0" Then
            Dim objVehicleColorZZZZ As VechileColor = New FinishUnit.VechileColorFacade(User).Retrieve("ZZZZ")
            arrayListVehicleColor.Add(objVehicleColorZZZZ)
        End If
        dtgColor.DataSource = arrayListVehicleColor
        dtgColor.DataBind()
    End Sub

    Private Sub GetData(ByVal Model As String, ByVal assemblyyear As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strSql As String = String.Format(
            String.Join(
            Environment.NewLine,
"Select A.VehicleColorID",
"FROM VechileColorIsActiveOnPK AS A with (nolock) ",
"WHERE ",
"(",
"	A.RowStatus = 0",
"	AND A.ProductionYear = {0}",
"	AND A.Status = 1",
")"
), assemblyyear)

        criterias.opAnd(New Criteria(GetType(VechileColor), "ID", MatchType.InSet, "(" & strSql & ")"))
        criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, Model))
        criterias.opAnd(New Criteria(GetType(VechileColor), "Status", MatchType.No, "X"))
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("ColorCode")) Then
            sortColl.Add(New Sort(GetType(VechileColor), "ColorCode", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If
        arrayListVehicleColor = New FinishUnit.VechileColorFacade(User).RetrieveByCriteria(criterias, sortColl)
        If pktype = "0" Then
            Dim objVehicleColorZZZZ As VechileColor = New FinishUnit.VechileColorFacade(User).Retrieve("ZZZZ")
            arrayListVehicleColor.Add(objVehicleColorZZZZ)
        End If
        dtgColor.DataSource = arrayListVehicleColor
        dtgColor.DataBind()
    End Sub

    Private Sub dtgColor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgColor.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgColor.CurrentPageIndex * dtgColor.PageSize)
            e.Item.Cells(2).Controls.Add(rdbChoice)
            If PKtype = "0" Then
                If e.Item.ItemIndex = arrayListVehicleColor.Count - 1 Then
                    Dim textBox As LiteralControl = New LiteralControl("<INPUT type=Text name=""textBox"">")
                    e.Item.Cells(4).Controls.Add(textBox)
                End If
            End If
        End If
    End Sub
End Class
