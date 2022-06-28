Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security


Public Class FrmLogisticCompany
    Inherits System.Web.UI.Page

    Private m_bLihatPrivilege As Boolean = False
    Private m_bInputPrivilege As Boolean = False
    Private _sesshelper As SessionHelper = New SessionHelper
    Dim oDealer As Dealer

    Private Sub CheckPrivilege()
        m_bLihatPrivilege = SecurityProvider.Authorize(Context.User, SR.CBUReturn_LogisticCompany_View_Privilage)
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.CBUReturn_LogisticCompany_Input_Privilage)

        If Not m_bLihatPrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Klaim Pengembalian Kendaraan - Logistic Company")
        End If
    End Sub

    Private Sub DisabledAllInput()
        txtAlamat.Enabled = False
        txtKode.Enabled = False
        txtNoTelp.Enabled = False
        txtVendorName.Enabled = False
        btnSimpan.Enabled = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        CheckPrivilege()

        If Not IsPostBack Then
            'BindDDLKota()

            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.DESC

            BindGrid(0)
            ViewState("Mode") = "New"
        End If
        lnkBtnPopUpKota.Attributes("onclick") = "ShowCitySelection()"

        If Not m_bInputPrivilege Then
            DisabledAllInput()
        End If
    End Sub

    'Private Sub BindDDLKota()
    '    Dim critCity As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    critCity.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))
    '    ddlKota.DataSource = New CityFacade(User).Retrieve(critCity)
    '    ddlKota.DataValueField = "ID"
    '    ddlKota.DataTextField = "CityName"
    '    ddlKota.DataBind()
    '    ddlKota.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    '    ddlKota.SelectedIndex = 0
    'End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim crit As CriteriaComposite = SearchCriteria()

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(ChassisMasterLogisticCompany), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        Dim listSource As ArrayList = New ChassisMasterLogisticCompanyFacade(User).Retrieve(crit, sortColl)

        If listSource.Count <> 0 Then
            CommonFunction.SortListControl(listSource, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(listSource, pageIndex, dgLogistic.PageSize)
            dgLogistic.CurrentPageIndex = pageIndex
            dgLogistic.DataSource = PagedList
            dgLogistic.VirtualItemCount = listSource.Count
            dgLogistic.DataBind()
        Else
            dgLogistic.DataSource = New ArrayList
            dgLogistic.VirtualItemCount = 0
            dgLogistic.CurrentPageIndex = 0
            dgLogistic.DataBind()
        End If
    End Sub

    Function SearchCriteria() As CriteriaComposite
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterLogisticCompany), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtVendorName.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "Name", MatchType.Partial, txtVendorName.Text.Trim))
        End If

        If txtKode.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "Kode", MatchType.Partial, txtKode.Text.Trim))
        End If

        If txtAlamat.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "Address", MatchType.Partial, txtAlamat.Text.Trim))
        End If

        If txtNoTelp.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "NoTelfon", MatchType.Exact, txtNoTelp.Text.Trim))
        End If

        If hdnKota.Value.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "City.CityCode", MatchType.Exact, hdnKota.Value.Trim))
        End If

        Return crit
    End Function


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGrid(0)
        If Not m_bInputPrivilege Then
            DisabledAllInput()
        End If
    End Sub

    Private Function isDuplicate(ByVal vendorName As String, ByVal kode As String) As Boolean
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterLogisticCompany), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "Name", MatchType.Exact, vendorName))
        crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "Kode", MatchType.Exact, kode))
        If ViewState("Mode") = "Edit" Then
            crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "ID", MatchType.No, hdnID.Value))
        End If
        Dim arlData As ArrayList = New ChassisMasterLogisticCompanyFacade(User).Retrieve(crit)
        If arlData.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Function isDuplicateVendorName(ByVal vendorName As String) As Boolean
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterLogisticCompany), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "Name", MatchType.Exact, vendorName))
        If ViewState("Mode") = "Edit" Then
            crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "ID", MatchType.No, hdnID.Value))
        End If
        Dim arlData As ArrayList = New ChassisMasterLogisticCompanyFacade(User).Retrieve(crit)
        If arlData.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Function isDuplicateKode(ByVal kode As String) As Boolean
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterLogisticCompany), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "Kode", MatchType.Exact, kode))
        If ViewState("Mode") = "Edit" Then
            crit.opAnd(New Criteria(GetType(ChassisMasterLogisticCompany), "ID", MatchType.No, hdnID.Value))
        End If
        Dim arlData As ArrayList = New ChassisMasterLogisticCompanyFacade(User).Retrieve(crit)
        If arlData.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim objLogistic As New ChassisMasterLogisticCompany

        If txtVendorName.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Vendor harus diisi")
            Exit Sub
        End If

        If txtKode.Text.Trim.Length = 0 Then
            MessageBox.Show("Short List harus diisi")
            Exit Sub
        End If

        If txtNoTelp.Text.Trim.Length = 0 Then
            txtNoTelp.Text = 0
        End If

        'If isDuplicateVendorName(txtVendorName.Text) Then
        '    MessageBox.Show("Nama vendor tesebut sudah ada")
        '    Exit Sub
        'End If

        If isDuplicateKode(txtKode.Text) Then
            MessageBox.Show("Short list tesebut sudah ada")
            Exit Sub
        End If

        If isDuplicate(txtVendorName.Text, txtKode.Text) Then
            MessageBox.Show("Nama vendor dengan Short list tesebut sudah ada")
            Exit Sub
        End If

        Dim nRes As Integer = 0
        If ViewState("Mode") = "New" Then
            objLogistic.Name = txtVendorName.Text.Trim
            objLogistic.Kode = txtKode.Text.Trim
            objLogistic.Address = txtAlamat.Text.Trim
            objLogistic.NoTelfon = txtNoTelp.Text.Trim
            objLogistic.City = New CityFacade(User).Retrieve(hdnKota.Value.Trim)
            'objLogistic.City = New CityFacade(User).Retrieve(CInt(ddlKota.SelectedValue))
            nRes = New ChassisMasterLogisticCompanyFacade(User).Insert(objLogistic)
        Else
            objLogistic = New ChassisMasterLogisticCompanyFacade(User).Retrieve(CInt(hdnID.Value))
            objLogistic.Name = txtVendorName.Text.Trim
            objLogistic.Kode = txtKode.Text.Trim
            objLogistic.Address = txtAlamat.Text.Trim
            objLogistic.NoTelfon = txtNoTelp.Text.Trim
            objLogistic.City = New CityFacade(User).Retrieve(hdnKota.Value.Trim)
            'objLogistic.City = New CityFacade(User).Retrieve(CInt(ddlKota.SelectedValue))
            nRes = New ChassisMasterLogisticCompanyFacade(User).Update(objLogistic)
        End If
        If nRes > 0 Then
            MessageBox.Show(SR.SaveSuccess)
            ClearInput()
            BindGrid(0)
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearInput()
        If Not m_bInputPrivilege Then
            DisabledAllInput()
        End If
        BindGrid(0)
    End Sub

    Private Sub ClearInput()
        txtVendorName.Text = String.Empty
        txtKode.Text = String.Empty
        txtAlamat.Text = String.Empty
        txtNoTelp.Text = String.Empty
        'ddlKota.SelectedIndex = 0
        hdnKota.Value = String.Empty
        txtKota.Text = String.Empty
        btnSearch.Enabled = True
        hdnID.Value = 0
        ViewState("Mode") = "New"
        AbleInput(True)
    End Sub

    Private Sub AbleInput(ByVal cond As Boolean)
        txtVendorName.Enabled = cond
        txtKode.Enabled = cond
        txtAlamat.Enabled = cond
        txtNoTelp.Enabled = cond
        'ddlKota.Enabled = cond
        txtKota.Enabled = cond
        btnSearch.Enabled = cond
        btnSimpan.Enabled = cond
    End Sub

    Protected Sub dgLogistic_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgLogistic.ItemCommand
        If e.Item.ItemType <> ListItemType.Pager AndAlso e.Item.ItemType <> ListItemType.Header Then
            Dim objLogistic As ChassisMasterLogisticCompany = New ChassisMasterLogisticCompanyFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
            Select Case e.CommandName
                Case "Edit"
                    hdnID.Value = objLogistic.ID
                    btnSearch.Enabled = False
                    txtVendorName.Text = objLogistic.Name
                    txtKode.Text = objLogistic.Kode
                    txtAlamat.Text = objLogistic.Address
                    txtNoTelp.Text = objLogistic.NoTelfon
                    If Not IsNothing(objLogistic.City) Then
                        'ddlKota.SelectedValue = objLogistic.City.ID
                        hdnKota.Value = objLogistic.City.CityCode
                        txtKota.Text = objLogistic.City.CityName
                    Else
                        'ddlKota.SelectedIndex = 0
                        hdnKota.Value = String.Empty
                        txtKota.Text = String.Empty
                    End If
                    ViewState("Mode") = "Edit"

                Case "View"
                    hdnID.Value = objLogistic.ID
                    txtVendorName.Text = objLogistic.Name
                    txtKode.Text = objLogistic.Kode
                    txtAlamat.Text = objLogistic.Address
                    txtNoTelp.Text = objLogistic.NoTelfon
                    If Not IsNothing(objLogistic.City) Then
                        hdnKota.Value = objLogistic.City.CityCode
                        txtKota.Text = objLogistic.City.CityName
                        'ddlKota.SelectedValue = objLogistic.City.ID
                        hdnKota.Value = objLogistic.City.CityCode
                        txtKota.Text = objLogistic.City.CityName
                    Else
                        'ddlKota.SelectedIndex = 0
                        hdnKota.Value = String.Empty
                        txtKota.Text = String.Empty
                    End If
                    AbleInput(False)

                Case "Delete"
                    DeleteData(objLogistic)
                    BindGrid(0)
            End Select
        End If
    End Sub

    Private Sub DeleteData(ByVal objLogistic As ChassisMasterLogisticCompany)
        objLogistic.RowStatus = CType(DBRowStatus.Deleted, Short)
        Dim nRes As Integer = New ChassisMasterLogisticCompanyFacade(User).Update(objLogistic)
        If nRes > 0 Then
            MessageBox.Show(SR.DeleteSucces)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
    End Sub

    Protected Sub dgLogistic_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgLogistic.PageIndexChanged
        dgLogistic.CurrentPageIndex = e.NewPageIndex
        ClearInput()
        BindGrid(e.NewPageIndex)
    End Sub

    Protected Sub dgLogistic_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgLogistic.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As ChassisMasterLogisticCompany = CType(e.Item.DataItem, ChassisMasterLogisticCompany)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dgLogistic.PageSize * dgLogistic.CurrentPageIndex) + e.Item.ItemIndex + 1
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnDelete.Attributes.Add("OnClick", "return confirm('Anda yakin mau menghapus " & oData.Kode & " ?');")
            If Not m_bInputPrivilege Then
                lbtnDelete.Visible = False
                lbtnEdit.Visible = False
            End If
        End If
    End Sub
End Class