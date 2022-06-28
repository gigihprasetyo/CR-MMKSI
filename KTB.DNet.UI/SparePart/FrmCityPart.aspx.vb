Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Public Class FrmCityPart
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCityName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtCityCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlProvince As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCity As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgCityPart As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private _sessCriteria As String = "FrmCityPart._sessCriteria"
    Private _sessControlState As String = "FrmCityPart._sessControlState"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim _sessHelper As New SessionHelper

#Region "Cek Privilege"

    Private Sub UserPrivilege()
        '-- Set user privileges

        '-- Get the session
        If Not IsNothing(_sessHelper.GetSession("DEALER")) Then
            Dim objDealer As Dealer = CType(_sessHelper.GetSession("DEALER"), Dealer)

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                '-- As KTB user
                If Not SecurityProvider.Authorize(Context.User, SR.CityPart_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=PARTSHOP - Entry Daftar Kota")
                End If
                Return

            End If
        End If

        '-- If not match then sent to Access Denied page.
        Server.Transfer("../FrmAccessDenied.aspx?modulName=PARTSHOP - Entry Daftar Kota")
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserPrivilege()
        If Not IsPostBack() Then
            ViewState("CurrentSortColumn") = "CityCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            ViewState.Add("vsproses", "Insert")
            BindDDL()
            BindDataGrid(dgCityPart.CurrentPageIndex)

        End If
    End Sub

    Public Sub BindDataGrid(ByVal idx As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(CityPart), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not ((ddlProvince.SelectedValue = "Invalid") Or (ddlProvince.SelectedValue = "")) Then
            criterias.opAnd(New Criteria(GetType(CityPart), "Province.ID", MatchType.Exact, ddlProvince.SelectedValue.Trim))
        End If

        If Not ((ddlCity.SelectedValue = "Invalid") Or (ddlCity.SelectedValue = "")) Then
            criterias.opAnd(New Criteria(GetType(CityPart), "City.ID", MatchType.Exact, ddlCity.SelectedValue.Trim))
        End If

        If txtCityCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(CityPart), "CityCode", MatchType.Partial, txtCityCode.Text.Trim))
        End If

        If txtCityName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(CityPart), "CityName", MatchType.Partial, txtCityName.Text.Trim))
        End If

        SaveCriteria()
        _sessHelper.SetSession(_sessCriteria, criterias)
        arrList = New CityPartFacade(User).RetrieveByCriteria(criterias, idx + 1, dgCityPart.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgCityPart.DataSource = arrList

        If arrList.Count > 0 Then
            dgCityPart.VirtualItemCount = totalRow
        Else
            dgCityPart.VirtualItemCount = 0
            dgCityPart.CurrentPageIndex = 0
        End If

        dgCityPart.DataBind()
    End Sub

    Private Sub BindDDL()
        Dim _arr As ArrayList = New ProvinceFacade(User).RetrieveActiveList

        For Each item As Province In _arr
            Dim _list As New ListItem(item.ProvinceCode & " - " & item.ProvinceName, item.ID)
            ddlProvince.Items.Add(_list)
        Next
        Dim _lists As New ListItem("Pilih Provinsi", "Invalid")
        _lists.Selected = True
        ddlProvince.Items.Insert(0, _lists)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim objCityPart As New CityPart

        If Not CType(ViewState("vsproses"), String) = "Insert" Then
            objCityPart = CType(Session.Item("vsCityPart"), CityPart)
        End If
        Dim n As Integer = -1
        If CType(ViewState("vsproses"), String) = "Insert" Then
            If txtCityCode.Text <> String.Empty Then
                If CheckValidation(txtCityCode.Text, objCityPart) = True Then
                    objCityPart.CityName = txtCityName.Text
                    objCityPart.Province = New ProvinceFacade(User).Retrieve(CInt(ddlProvince.SelectedValue))
                    objCityPart.City = New CityFacade(User).Retrieve(CInt(ddlCity.SelectedValue))
                    objCityPart.CityCode = txtCityCode.Text
                    n = New CityPartFacade(User).Insert(objCityPart)
                    If n >= 0 Then
                        MessageBox.Show("Simpan sukses")
                        txtCityCode.Enabled = True
                        ClearData()
                        dgCityPart.SelectedIndex = -1
                        BindDataGrid(dgCityPart.CurrentPageIndex)
                    Else
                        MessageBox.Show("Simpan gagal")
                    End If
                End If
            Else
                MessageBox.Show("Input data terlebih dahulu")
            End If
        Else
            objCityPart.CityName = txtCityName.Text
            objCityPart.Province = New ProvinceFacade(User).Retrieve(CInt(ddlProvince.SelectedValue))
            objCityPart.City = New CityFacade(User).Retrieve(CInt(ddlCity.SelectedValue))
            'objCityPart.Code = txtCode.Text
            n = New CityPartFacade(User).Update(objCityPart)
            If n >= 0 Then
                txtCityCode.Enabled = True
                MessageBox.Show("Update data sukses")
                ReadCriteria()
                dgCityPart.SelectedIndex = -1

                BindDataGrid(dgCityPart.CurrentPageIndex)

            Else
                MessageBox.Show("Update data gagal")
            End If
            ViewState.Add("vsproses", "Insert")
        End If
    End Sub

    Private Sub dgCityPart_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgCityPart.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                ViewState.Add("vsproses", "Edit")
                setControlUpdate(Integer.Parse(e.Item.Cells(0).Text))
            Case "Delete"
                deleteData(Integer.Parse(e.Item.Cells(0).Text))
        End Select
    End Sub
    Private Sub setControlUpdate(ByVal id As Integer)
        Dim objCityPart As CityPart = New CityPartFacade(User).Retrieve(id)
        txtCityCode.Text = objCityPart.CityCode
        txtCityCode.Enabled = False
        txtCityName.Text = objCityPart.CityName
        ddlProvince.SelectedValue = objCityPart.Province.ID
        ddlProvince.Enabled = False
        CommonFunction.BindCity(ddlCity, User, True, ddlProvince.SelectedValue, False)
        If (Not IsNothing(objCityPart.City)) Then
            Dim item As ListItem = ddlCity.Items.FindByValue(objCityPart.City.ID)
            If Not IsNothing(item) Then
                ddlCity.SelectedValue = objCityPart.City.ID
                ddlCity.Enabled = False
            Else
                ClearData()
                MessageBox.Show("Tidak ada kota " & objCityPart.City.CityName & "di provinsi " & objCityPart.Province.ProvinceName)
                Exit Sub
            End If
        Else
            ddlCity.Enabled = True
        End If

        btnCari.Enabled = False
        'Todo session
        Session.Add("vsCityPart", objCityPart)
    End Sub
    Private Sub deleteData(ByVal id As Integer)
        Try
            Dim objCityPartFacade As New CityPartFacade(User)
            Dim objCityPart As CityPart = objCityPartFacade.Retrieve(id)
            If objCityPart.PartShops.Count > 0 Then
                MessageBox.Show("Data telah di Referensi dalam transaksi.")
            Else
                objCityPartFacade.DeleteFromDB(objCityPart)
                BindDataGrid(dgCityPart.CurrentPageIndex)
            End If
        Catch
            MessageBox.Show(SR.DeleteFail)
        End Try
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dgCityPart.DataSource = New CityPartFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession(_sessCriteria), CriteriaComposite), indexPage + 1, dgCityPart.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirection"), Sort.SortDirection))
            dgCityPart.VirtualItemCount = totalRow
            dgCityPart.DataBind()
        End If

    End Sub

    Private Sub dgCityPart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgCityPart.PageIndexChanged
        dgCityPart.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgCityPart.CurrentPageIndex)
    End Sub

    Private Sub dgCityPart_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgCityPart.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirection") = Sort.SortDirection.DESC
        End If

        dgCityPart.SelectedIndex = -1
        dgCityPart.CurrentPageIndex = 0
        bindGridSorting(dgCityPart.CurrentPageIndex)
    End Sub
    Private Sub ClearData()
        txtCityName.Text = String.Empty
        txtCityCode.Text = String.Empty
        txtCityCode.Enabled = True
        ddlCity.Enabled = True
        ddlProvince.Enabled = True
        ddlProvince.SelectedValue = "Invalid"
        ddlCity.Items.Clear()
        btnCari.Enabled = True
    End Sub
    Private Function CheckValidation(ByVal Code As String, ByVal obj As CityPart) As Boolean
        Dim bcheck As Boolean = True
        If txtCityCode.Text.Trim = String.Empty Then
            bcheck = False
            MessageBox.Show("Kode Kota masih kosong")
        End If
        If txtCityName.Text.Trim = String.Empty Then
            bcheck = False
            MessageBox.Show("Nama Kota masih kosong")
        End If
        If ddlProvince.SelectedValue = "Invalid" Then
            bcheck = False
            MessageBox.Show("Provinsi belum dipilih")
        End If
        Dim objTmp As CityPart = New CityPartFacade(User).Retrieve(Code)
        If Not IsNothing(objTmp) Then
            bcheck = False
            MessageBox.Show("Kode Kota sudah ada")
        End If
        Dim objTmp2 As CityPart = New CityPartFacade(User).Retrieve(CInt(ddlCity.SelectedValue), True)
        If Not IsNothing(objTmp2) Then
            bcheck = False
            MessageBox.Show("Kota Part untuk Kota " + ddlCity.SelectedItem.Text + " sudah ada")
        End If

        Return bcheck
    End Function

    Private Sub dgCityPart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCityPart.ItemDataBound
        Dim objCityPart As CityPart = e.Item.DataItem
        If Not e.Item.DataItem Is Nothing Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgCityPart.CurrentPageIndex * dgCityPart.PageSize)

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        ClearData()
        ViewState.Add("vsproses", "Insert")
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        dgCityPart.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub


    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("ddlProvince", ddlProvince.SelectedValue.ToString)
        crits.Add("ddlCity", ddlCity.SelectedValue.ToString)
        crits.Add("txtCityCode", txtCityCode.Text)
        crits.Add("txtCityName", txtCityName.Text)
        _sessHelper.SetSession(_sessControlState, crits)
    End Sub

    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(_sessHelper.GetSession(_sessControlState), Hashtable)

        If Not IsNothing(crits) Then
            ddlProvince.SelectedValue = CStr(crits.Item("ddlProvince"))
            ddlCity.SelectedValue = CStr(crits.Item("ddlCity"))
            txtCityCode.Text = CStr(crits.Item("txtCityCode"))
            txtCityName.Text = CStr(crits.Item("txtCityName"))
        End If
    End Sub

    Protected Sub ddlProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvince.SelectedIndexChanged
        If (ddlProvince.SelectedValue = "Invalid") Or (ddlProvince.SelectedValue = "") Then
            ddlCity.Items.Clear()
            ddlCity.Items.Add(New ListItem("Silahkah Pilih", ""))
        Else
            CommonFunction.BindCity(ddlCity, User, True, ddlProvince.SelectedValue, False)
        End If
    End Sub
End Class
