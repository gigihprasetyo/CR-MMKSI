#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit

#End Region

Public Class PopUpCustomerSelectionOne
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtCustName As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgCustomerSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCity As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnPilih As System.Web.UI.WebControls.Button
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

#Region " custom Declaration "
    Private objCustomer As Customer
    Private sessHelper As New SessionHelper
#End Region

#Region " Custom Method "

    Private Sub ClearData()
        Me.txtCity.Text = String.Empty
        Me.txtCustName.Text = String.Empty
    End Sub

#End Region

#Region " Event Hendler "
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            sessHelper.SetSession("SortCol", "Code")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            ClearData()
            'BindSearch(0)
        End If
    End Sub
    Public Sub BindSearch(ByVal indexPage As Integer)
        Dim totalRow As Integer
        Dim arrList As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not txtCity.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "City.CityName", MatchType.[Partial], txtCity.Text))
        End If

        If Not txtCustName.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "Name1", MatchType.[Partial], txtCustName.Text))
        End If

        If Not IsNothing(Request.QueryString("FilterLoginDealer")) And Request.QueryString("FilterLoginDealer") <> String.Empty Then
            If Request.QueryString("FilterLoginDealer") = "True" Then
                'Dim CustomerIDColl As String = String.Empty
                'Dim arr As New ArrayList
                'Dim cr As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'cr.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerDealer), "Dealer.ID", MatchType.Exact, CType(sessHelper.GetSession("DEALER"), Dealer).ID))

                'arr = New CustomerDealerFacade(User).Retrieve(cr)
                'If arr.Count > 0 Then
                '    For Each item As CustomerDealer In arr
                '        CustomerIDColl += item.Customer.ID.ToString & ","
                '    Next
                '    CustomerIDColl = CustomerIDColl.Substring(0, CustomerIDColl.Length - 1)
                'Else
                '    CustomerIDColl = "0"
                'End If
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "ID", MatchType.InSet, "(" & CustomerIDColl & ")"))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "ID", MatchType.InSet, "(" & "select NewCustomerID from customerdealer where RowStatus = 0 and dealerid= " & CType(sessHelper.GetSession("DEALER"), Dealer).ID.ToString & ")"))
            End If
        End If
        ViewState("Data") = "SPK"
        'Dim arrList As ArrayList = New CustomerFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgCustomerSelection.PageSize, totalRow, sessHelper.GetSession("SortCol"), CType(sessHelper.GetSession("SortDirection"), Sort.SortDirection))
        Dim SPKNumber As String = Request.QueryString("SPKNumber")
        Dim NoChassis As String = Request.QueryString("Chassis")
        If Not String.IsNullorEmpty(SPKNumber) And Not String.IsNullorEmpty(NoChassis) Then
            arrList = New SPKDetailCustomerFacade(User).GetSPKDetailCustomerByChassis(SPKNumber, NoChassis, txtCity.Text, txtCustName.Text)
        ElseIf Not String.IsNullorEmpty(SPKNumber) And String.IsNullorEmpty(NoChassis) Then
            arrList = New SPKDetailCustomerFacade(User).GetSPKDetailCustomer(SPKNumber, txtCity.Text, txtCustName.Text)
        Else

            If 1 = 1 Then
                ViewState("Data") = "Customer"
                arrList = New CustomerFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgCustomerSelection.PageSize, totalRow, sessHelper.GetSession("SortCol"), CType(sessHelper.GetSession("SortDirection"), Sort.SortDirection))
            End If


        End If



        dtgCustomerSelection.VirtualItemCount = totalRow
        dtgCustomerSelection.DataSource = arrList
        sessHelper.SetSession("sessCustomer", arrList)
        dtgCustomerSelection.DataBind()
    End Sub

    Private Sub dtgCustomerSelection_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCustomerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            If ViewState("Data") = "SPK" Then
                BinDSPKDetailCustomer(e, ItemType)
            Else
                BindCUstomer(e, ItemType)
            End If
        End If
    End Sub
    Private Sub BindCUstomer(ByVal e As DataGridItemEventArgs, ByVal ItemType As ListItemType)
        e.Item.DataItem.GetType().ToString()
        Dim RowValue As Customer = CType(e.Item.DataItem, Customer)
        Dim txtNama As Label = e.Item.FindControl("txtNama")
        txtNama.Text = RowValue.Name1 + RowValue.Name2
        Dim lblNoKTP As Label = e.Item.FindControl("lblNoKTP")
        If Not IsNothing(RowValue.MyCustomerRequest) Then
            If Not IsNothing(RowValue.MyCustomerRequest.GetCustomerRequestProfile("NOKTP")) Then
                lblNoKTP.Text = RowValue.MyCustomerRequest.GetCustomerRequestProfile("NOKTP").ProfileValue
            End If
        Else
            lblNoKTP.Text = String.Empty
        End If
        If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub

    Private Sub BinDSPKDetailCustomer(ByVal e As DataGridItemEventArgs, ByVal ItemType As ListItemType)
        dtgCustomerSelection.Columns(7).Visible = True
        e.Item.DataItem.GetType().ToString()
        Dim RowValue As SPKDetailCustomer = CType(e.Item.DataItem, SPKDetailCustomer)
        Dim txtNama As Label = e.Item.FindControl("txtNama")
        txtNama.Text = RowValue.Name1 + RowValue.Name2
        Dim lblNoKTP As Label = e.Item.FindControl("lblNoKTP")
        Dim lblModelTipeWarna As Label = e.Item.FindControl("lblModelTipeWarna")
        Dim lblSPKDetailID As Label = e.Item.FindControl("lblSPKDetailID")
        Dim ProfileHeaderCode As String = "NOKTP"
        Dim ProfileGroupCode As String = String.Empty

        If RowValue.Status = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perorangan Then
            ProfileGroupCode = "cust_dbs_2"
        ElseIf RowValue.Status = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perusahaan Then
            ProfileGroupCode = "cust_dbs_3"
        ElseIf RowValue.Status = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.BUMN_Pemerintah Then
            ProfileGroupCode = "cust_dbs_4"
        ElseIf RowValue.Status = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Lainnya Then
            ProfileGroupCode = "cust_dbs_5"
        End If

        lblModelTipeWarna.Text = RowValue.SPKDetail.VechileColor.MaterialDescription
        Dim oProfileHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(ProfileHeaderCode)
        Dim oProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(ProfileGroupCode)
        Dim oSPKDetailCustomerProfile As SPKDetailCustomerProfile = New SPKDetailCustomerFacade(User).GetSPKDetailCustomerProfiles(RowValue, oProfileGroup, oProfileHeader)
        If Not IsNothing(RowValue) Then
            If Not IsNothing(oSPKDetailCustomerProfile) Then
                lblNoKTP.Text = oSPKDetailCustomerProfile.ProfileValue
            End If
        Else
            lblNoKTP.Text = String.Empty
        End If
        If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If

        lblSPKDetailID.Text = RowValue.DMSSPKDetailNo

    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch(dtgCustomerSelection.CurrentPageIndex)
        If dtgCustomerSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub
    'Private Sub btnPilih_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPilih.Click
    '    Dim bcheck As Boolean = False
    '    Dim success As Boolean = False

    '    dtgCustomerSelection.DataSource = CType(Session("sessCustomer"), ArrayList)
    '    For Each dtgItem As DataGridItem In dtgCustomerSelection.Items
    '        If CType(dtgItem.Cells(0).FindControl("radio"), RadioButton).Checked Then
    '            bcheck = True
    '            Exit For
    '        End If
    '    Next

    '    'Try

    '    If bcheck Then
    '        If Not GetCheckedItem() Is Nothing Then
    '            Dim objCust As Customer = GetCheckedItem()
    '            Dim returnValue As String = objCust.Code + ";" + objCust.Name1 + objCust.Name2 + ";" + objCust.Name3 + ";" + objCust.Alamat + ";" + objCust.Kelurahan + ";" + objCust.Kecamatan + ";" + objCust.PostalCode + ";" + objCust.City.CityName + ";" + objCust.City.Province.ProvinceName + ";" + objCust.Email + ";" + objCust.PhoneNo
    '            Response.Write("<script language='javascript'>window.returnValue='" + returnValue + "';window.close();</script>")
    '        Else
    '            MessageBox.Show("Pilih kode konsumen")
    '        End If
    '    Else
    '        MessageBox.Show("Pilih kode konsumen")
    '    End If
    'End Sub
    'Private Function GetCheckedItem() As Customer
    '    dtgCustomerSelection.DataSource = CType(Session("sessCustomer"), ArrayList)
    '    Dim arlCheckedItem As ArrayList = New ArrayList
    '    Dim nIndeks As Integer
    '    For Each dtgItem As DataGridItem In dtgCustomerSelection.Items
    '        nIndeks = dtgItem.ItemIndex
    '        Dim objCM As Customer = CType(CType(dtgCustomerSelection.DataSource, ArrayList)(nIndeks), Customer)
    '        If CType(dtgItem.Cells(0).FindControl("radio"), RadioButton).Checked Then
    '            arlCheckedItem.Add(objCM)
    '        End If
    '    Next
    '    Dim objCustomer As Customer
    '    If arlCheckedItem.Count > 0 Then
    '        objCustomer = CType(arlCheckedItem(0), Customer)
    '    Else
    '        objCustomer = New Customer
    '    End If
    '    Return objCustomer
    'End Function
    Private Sub dtgCustomerSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCustomerSelection.PageIndexChanged
        dtgCustomerSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch(dtgCustomerSelection.CurrentPageIndex)
    End Sub
    Private Sub dtgCustomerSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCustomerSelection.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortCol") Then
            If sessHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortCol", e.SortExpression)
        dtgCustomerSelection.SelectedIndex = -1
        dtgCustomerSelection.CurrentPageIndex = 0
        BindSearch(dtgCustomerSelection.CurrentPageIndex)

    End Sub
#End Region

End Class

