Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility

Public Class FrmFSCampaignDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents btnBackTop As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblFSKind As System.Web.UI.WebControls.Label
    Protected WithEvents dtgDealer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgVehicle As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sessHelper As SessionHelper = New SessionHelper
    Private objFSCampaign As FSCampaign
    Private backURL As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Request.QueryString("backURL") Is Nothing AndAlso Request.QueryString("backURL") <> String.Empty Then
            backURL = Request.QueryString("backURL")
        Else
            backURL = CType(sessHelper.GetSession("backURL"), String)
        End If

        If Not IsPostBack Then
            InitiatePage()
        End If

    End Sub
    Private Sub InitiatePage()
        Dim oFSCampaign As FSCampaign = New FSCampaign
        Dim oFSCampaignFacade As FSCampaignFacade = New FSCampaignFacade(User)
        Dim idFSCampaign As Integer = CType(sessHelper.GetSession("vsFSCampaignID"), Integer)
        If idFSCampaign > 0 Then
            oFSCampaign = oFSCampaignFacade.Retrieve(idFSCampaign)
        End If
        If Not oFSCampaign Is Nothing Then
            lblDescription.Text = oFSCampaign.Description
            lblFSKind.Text = GetFSKindString(oFSCampaign)
            BindDataGrid(oFSCampaign)
        End If
    End Sub
    Private Function GetFSKindString(ByVal oFSCampaign As FSCampaign) As String
        Dim strFSKind As String = String.Empty
        Dim iCount As Integer = 0
        For Each oFSCampaignKind As FSCampaignKind In oFSCampaign.FSCampaignKinds
            iCount = iCount + 1
            If iCount = oFSCampaign.FSCampaignKinds.Count Then
                strFSKind &= oFSCampaignKind.FSKind.KindCode
                Exit For
            Else
                strFSKind &= oFSCampaignKind.FSKind.KindCode & ","
            End If
        Next
        Return strFSKind
    End Function

    Private Sub BindDataGrid(ByVal oFSCampaign As FSCampaign)
        BindDataDealer(oFSCampaign)
        BindDataVehicle(oFSCampaign)
    End Sub
    Private Sub BindDataDealer(ByVal oFSCampaign As FSCampaign)
        Dim oDealerFacade As DealerFacade = New DealerFacade(User)
        Dim arlDealer As ArrayList = New ArrayList

        Dim criterias As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, "(select dealercode from fscampaigndealer where rowstatus = 0 and campaignid=" & oFSCampaign.ID & ")"))
        arlDealer = oDealerFacade.Retrieve(criterias)
        dtgDealer.DataSource = arlDealer
        dtgDealer.DataBind()

    End Sub
    Private Sub BindDataVehicle(ByVal oFSCampaign As FSCampaign)

        dtgVehicle.DataSource = oFSCampaign.FSCampaignVehicles
        dtgVehicle.DataBind()

    End Sub

    Private Sub btnBackTop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackTop.Click
        sessHelper.RemoveSession("backURL")
        sessHelper.RemoveSession("vsFSCampaignID")
        Response.Redirect(backURL)
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        sessHelper.RemoveSession("backURL")
        sessHelper.RemoveSession("vsFSCampaignID")
        Response.Redirect(backURL)
    End Sub

    Private Sub dtgDealer_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealer.ItemDataBound
        Dim RowValue As Dealer = CType(e.Item.DataItem, Dealer)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNoDealer"), Label).Text = CType(e.Item.ItemIndex + 1, String)
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            If Not RowValue.City Is Nothing Then
                CType(e.Item.FindControl("lblKota"), Label).Text = RowValue.City.CityName
            Else
                CType(e.Item.FindControl("lblKota"), Label).Text = "-"
            End If
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            If Not RowValue.DealerGroup Is Nothing Then
                CType(e.Item.FindControl("lblGroup"), Label).Text = RowValue.DealerGroup.GroupName
            End If
        End If

    End Sub

    Private Sub dtgVehicle_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgVehicle.ItemDataBound
        Dim RowValue As FSCampaignVehicle = CType(e.Item.DataItem, FSCampaignVehicle)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNoVehicle"), Label).Text = CType(e.Item.ItemIndex + 1, String) 'CType(e.Item.ItemIndex + 1 + (dtgVehicle.CurrentPageIndex * dtgVehicle.PageSize), String)
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            If Not RowValue.VechileType Is Nothing Then
                CType(e.Item.FindControl("lblVehicleDesc"), Label).Text = RowValue.VechileType.Description
            Else
                CType(e.Item.FindControl("lblVehicleDesc"), Label).Text = "-"
            End If
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            If Not RowValue.VechileType Is Nothing Then
                CType(e.Item.FindControl("lblCategory"), Label).Text = RowValue.VechileType.Category.CategoryCode
            Else
                CType(e.Item.FindControl("lblCategory"), Label).Text = "-"
            End If
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            If Not RowValue.VechileType Is Nothing Then
                CType(e.Item.FindControl("lblCode"), Label).Text = RowValue.VechileType.VechileTypeCode
            Else
                CType(e.Item.FindControl("lblCode"), Label).Text = "-"
            End If
        End If

    End Sub
End Class
