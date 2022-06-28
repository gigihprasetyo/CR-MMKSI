
#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmVehicleKind
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"
    Private _sessHelper As New SessionHelper
    Private _sessData As String = "FrmVehicleKind.Data"
#End Region

#Region "Internal Enum"
    Private Enum VehicleTypeStatus
        Aktif = 1
        Tidak_Aktif = 2
        Semua = 3
    End Enum
#End Region

#Region "Methods"

    Private Sub Initialization()
        BindDDL()
    End Sub

    Private Sub BindDDL()
        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

        Dim _arrStatus As ArrayList = New EnumVehicleTypeStatus().RetrieveVehicleTypeStatus()
        ddlStatus.DataSource = _arrStatus
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Semua", -1))

        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub

    Private Sub BindDTG()
        Dim oVT As VechileType
        Dim oVC As VechileColor


        Dim criterias As CriteriaComposite

        '-- Status
        criterias = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlCategory.SelectedValue <> "" Then  '-- Category
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlSubCategory.Items.Count > 0 AndAlso ddlSubCategory.SelectedValue <> "-1" Then
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "VechileType")

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

        End If

        '-- Color still active
        If ddlStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, ddlStatus.SelectedValue.ToString))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))

        '-- Retrieve price list
        Dim TempList As ArrayList = New VechileTypeFacade(User).RetrieveByCriteria(criterias, sortColl)
        Me._sessHelper.SetSession(_sessData, TempList)
        Me.dtgMain.DataSource = TempList
        Me.dtgMain.DataBind()
    End Sub

#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Initialization()
        End If
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        BindDTG()
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim arlData As ArrayList = CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)
            Dim oVT As VechileType = CType(arlData(e.Item.ItemIndex), VechileType)
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblVehicleTypeCode As Label = e.Item.FindControl("lblVehicleTypeCode")
            Dim lblVehicleName As Label = e.Item.FindControl("lblVehicleName")
            Dim chkVehicleKind1 As CheckBox = e.Item.FindControl("chkVehicleKind1")
            Dim chkVehicleKind2 As CheckBox = e.Item.FindControl("chkVehicleKind2")
            Dim chkVehicleKind3 As CheckBox = e.Item.FindControl("chkVehicleKind3")
            Dim chkVehicleKind4 As CheckBox = e.Item.FindControl("chkVehicleKind4")

            lblNo.Text = (e.Item.ItemIndex + 1).ToString
            lblVehicleTypeCode.Text = oVT.VechileTypeCode
            lblVehicleName.Text = oVT.Description
            chkVehicleKind1.Checked = (oVT.IsVehicleKind1 = 1)
            chkVehicleKind2.Checked = (oVT.IsVehicleKind2 = 1)
            chkVehicleKind3.Checked = (oVT.IsVehicleKind3 = 1)
            chkVehicleKind4.Checked = (oVT.IsVehicleKind4 = 1)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim arlData As ArrayList = CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)
        Dim arlToUpdate As New ArrayList
        Dim oVT As VechileType
        Dim oVTFac As New VechileTypeFacade(User)
        Dim nFailed As Integer = 0

        For Each di As DataGridItem In Me.dtgMain.Items
            Dim chkVehicleKind1 As CheckBox = di.FindControl("chkVehicleKind1")
            Dim chkVehicleKind2 As CheckBox = di.FindControl("chkVehicleKind2")
            Dim chkVehicleKind3 As CheckBox = di.FindControl("chkVehicleKind3")
            Dim chkVehicleKind4 As CheckBox = di.FindControl("chkVehicleKind4")

            oVT = CType(arlData(di.ItemIndex), VechileType)
            oVT.IsVehicleKind1 = IIf(chkVehicleKind1.Checked, 1, 0)
            oVT.IsVehicleKind2 = IIf(chkVehicleKind2.Checked, 1, 0)
            oVT.IsVehicleKind3 = IIf(chkVehicleKind3.Checked, 1, 0)
            oVT.IsVehicleKind4 = IIf(chkVehicleKind4.Checked, 1, 0)
            If oVTFac.Update(oVT) = -1 Then
                nFailed += 1
            End If
        Next

        If nFailed > 0 Then
            If Me.dtgMain.Items.Count = nFailed Then
                MessageBox.Show("Data Gagal disimpan")
            Else
                MessageBox.Show("Data berhasil disimpan dan " & nFailed.ToString & " gagal.")
            End If
        Else
            MessageBox.Show("Data Berhasil disimpan")
        End If
        Me.BindDTG()
    End Sub

#End Region
End Class
