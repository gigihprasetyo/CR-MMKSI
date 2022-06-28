#Region "DLL"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile

#End Region

Public Class FrmDealerBranchEntry
    Inherits System.Web.UI.Page

#Region "Var"
    Private sesHelper As New SessionHelper
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        CehckUserPrivilege()

        If Not IsPostBack Then
            txtDealerCode.Attributes.Add("ReadOnly", "ReadOnly")
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelectionOne();"
            If IsNothing(Request.QueryString("ID")) Then
                Init()

                ViewState("Proses") = "Insert"
                btnBack.Visible = False
                btnReset.Visible = False
            Else

                BindDdlStatus()
                BindDdlType()
                btnBack.Visible = True

                If Request.QueryString("Proses").ToString().ToLower() = "edit" Then
                    Dim obj As DealerBranch = New DealerBranchFacade(User).Retrieve(CInt(Request.QueryString("ID")))

                    Me.BindDBToUI(obj)
                    ViewState("Proses") = "Edit"
                    btnReset.Visible = True
                Else
                    ViewState("Proses") = "View"
                    btnSave.Visible = False
                    btnReset.Visible = False

                    Dim obj As DealerBranch = New DealerBranchFacade(User).Retrieve(CInt(Request.QueryString("ID")))

                    Me.BindDBToUI(obj)

                End If
            End If

        End If


    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Save()

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("../General/FrmDealerBranchList2.aspx?From=Entry")
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim obj As DealerBranch = New DealerBranchFacade(User).Retrieve(CInt(ViewState("ID")))

        Me.BindDBToUI(obj)
    End Sub

    Protected Sub ddlMainArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMainArea.SelectedIndexChanged
        BindDdlArea1()
        BindDdlArea2()
    End Sub

    Protected Sub ddlArea1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlArea1.SelectedIndexChanged
        BindDdlArea2()
    End Sub

    Protected Sub ddlProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvince.SelectedIndexChanged
        Dim nTotRow As Integer = 0
        Dim nPageNumber As Integer = 1
        Dim nPageSize As Integer = 50
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A")) 'A = Aktif; X = Tidak Aktif
        criterias.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, CType(ddlProvince.SelectedValue, Integer)))
        ddlCity.Enabled = True
        ddlCity.DataSource = New CityFacade(User).Retrieve(criterias)
        ddlCity.DataTextField = "CityName"
        ddlCity.DataValueField = "ID"
        ddlCity.DataBind()
        ddlCity.Items.Insert(0, New ListItem("", "0"))
    End Sub

    Protected Sub cbSalesUnit_CheckedChanged(sender As Object, e As EventArgs) Handles cbSalesUnit.CheckedChanged
        EnablePanel(pnlSales, cbSalesUnit.Checked)
    End Sub

    Protected Sub cbService_CheckedChanged(sender As Object, e As EventArgs) Handles cbService.CheckedChanged
        EnablePanel(pnlService, cbService.Checked)
    End Sub

    Protected Sub cbSpareParts_CheckedChanged(sender As Object, e As EventArgs) Handles cbSpareParts.CheckedChanged
        EnablePanel(pnlSpareparts, cbSpareParts.Checked)
    End Sub

#Region "Custom Method"
    Private Sub Init()
        BindDdlMainArea()
        BindDdlArea1()
        BindDdlArea2()
        BindDdlProvince()
        BindDdlCity()
        BindDdlStatus()
        BindDdlType()
    End Sub

    Private Sub CehckUserPrivilege()


        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)

        If (objDealer.Title <> EnumDealerTittle.DealerTittle.KTB) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=General - Cabang Dealer")

        Else
            If Not SecurityProvider.Authorize(Context.User, SR.DealerBranch_Input_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=General - Cabang Dealer, input_daftar_DealerBranch")
            End If
        End If

        'm_bChangeFreePPh22_Privilege = SecurityProvider.Authorize(Context.User, SR.ENHAdminMaintainFreePPh22_Privilege)

    End Sub

    Private Sub BindDdlMainArea()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlMainArea.DataSource = New MainAreaFacade(User).RetrieveActiveList(criterias, "Description", Sort.SortDirection.ASC)
        ddlMainArea.DataTextField = "Description"
        ddlMainArea.DataValueField = "ID"
        ddlMainArea.DataBind()
        ddlMainArea.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    End Sub

    Private Sub BindDdlArea1()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area1), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Area1), "MainArea.ID", MatchType.Exact, CType(ddlMainArea.SelectedValue, Integer)))
        ddlArea1.DataSource = New Area1Facade(User).RetrieveActiveList(criterias, "Description", Sort.SortDirection.ASC)
        ddlArea1.DataTextField = "Description"
        ddlArea1.DataValueField = "ID"
        ddlArea1.DataBind()
        ddlArea1.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    End Sub

    Private Sub BindDdlArea2()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Area2), "Area1.ID", MatchType.Exact, CType(ddlArea1.SelectedValue, Integer)))
        ddlArea2.DataSource = New Area2Facade(User).RetrieveActiveList(criterias, "Description", Sort.SortDirection.ASC)
        ddlArea2.DataTextField = "Description"
        ddlArea2.DataValueField = "ID"
        ddlArea2.DataBind()
        ddlArea2.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    End Sub

    Private Sub BindDdlCity()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A")) 'A = Aktif; X = Tidak Aktif
        ddlCity.DataSource = New CityFacade(User).RetrieveActiveList(criterias, "CityName", Sort.SortDirection.ASC)
        ddlCity.DataTextField = "CityName"
        ddlCity.DataValueField = "ID"
        ddlCity.DataBind()
        ddlCity.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    End Sub

    Private Sub BindDdlProvince()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlProvince.DataSource = New ProvinceFacade(User).RetrieveActiveList(criterias, "ProvinceName", Sort.SortDirection.ASC)
        ddlProvince.DataTextField = "ProvinceName"
        ddlProvince.DataValueField = "ID"
        ddlProvince.DataBind()
        ddlProvince.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    End Sub

    Private Sub BindDdlStatus()
        Dim listStatus As New EnumDealerBranchStatus
        Dim al As ArrayList = listStatus.RetrieveStatus
        For Each item As EnumDealerBranch In al
            ddlStatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next

    End Sub

    Private Sub BindDdlType()
        Dim listStatus As New EnumDealerBranchType
        Dim al As ArrayList = listStatus.RetrieveStatus
        For Each item As EnumDealerBranch In al
            ddlTYpe.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next

    End Sub

    Private Sub EnablePanel(ByRef objPanel As Panel, ByVal toEnable As Boolean)

        For Each objTextBox As Control In objPanel.Controls
            If TypeOf objTextBox Is TextBox Then
                If Not toEnable Then
                    CType(objTextBox, TextBox).Text = String.Empty
                End If
            End If
        Next

        objPanel.Enabled = toEnable
    End Sub

    Private Function IsValid(ByVal isInsert As Boolean) As Boolean

        If Not cbSalesUnit.Checked AndAlso Not cbService.Checked AndAlso Not cbSpareParts.Checked Then

            MessageBox.Show("Ares Bisnis harus terisi salah satu")
            cbSalesUnit.Focus()
            Return False
        End If
        If txtDealerCode.Text.Trim() = "" Then
            MessageBox.Show("kode Dealer Harap di isi")
            txtDealerCode.Focus()
            Return False
        End If


        If txtName.Text.Trim() = "" Then
            MessageBox.Show("Nama Cabang Harap di isi")
            txtName.Focus()
            Return False
        End If

        If txtBranchCode.Text.Trim() = "" Then
            MessageBox.Show("kode cabang Harap di isi")
            txtBranchCode.Focus()
            Return False
        End If


        Dim strVal As String = ddlTYpe.SelectedValue.ToString()

        Select Case CType(CInt(ddlTYpe.SelectedValue), EnumDealerBranchType.BranchType)
            Case EnumDealerBranchType.BranchType.Outlet
                If Strings.Left(txtBranchCode.Text.Trim(), 1) <> "2" Then
                    MessageBox.Show("Kode Outlet, harus berawalan 2")
                    txtBranchCode.Focus()
                    Return False

                End If
            Case EnumDealerBranchType.BranchType.TemporaryOutlet
                If Strings.Left(txtBranchCode.Text.Trim(), 1) <> "3" Then
                    MessageBox.Show("Kode Temporary Outlet, harus berawalan 3")
                    txtBranchCode.Focus()
                    Return False

                End If
        End Select

        'CHek BranchCode
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, txtBranchCode.Text.Trim()))
        If Not isInsert Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "ID", MatchType.No, CInt(ViewState("ID"))))
        End If

        Dim IsExist As New ArrayList
        IsExist = New DealerBranchFacade(User).Retrieve(criterias)

        If Not IsNothing(IsExist) AndAlso IsExist.Count > 0 Then
            MessageBox.Show("Kode Cabang sudah terpakai")
            txtBranchCode.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub Save()


        If ViewState("Proses") = "Insert" Then
            If Not IsValid(True) Then
                Return
            End If

            Try


                Dim objDealerBranch As New DealerBranch

                objDealerBranch.TypeBranch = ddlTYpe.SelectedValue
                Try
                    objDealerBranch.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
                Catch ex As Exception
                    MessageBox.Show("Dealer Tidak terdaftar")
                End Try

                objDealerBranch.DealerBranchCode = txtBranchCode.Text.Trim()
                objDealerBranch.Name = txtName.Text
                objDealerBranch.Term1 = txtSearch1.Text
                objDealerBranch.Term2 = txtSearch2.Text
                objDealerBranch.TypeBranch = ddlTYpe.SelectedValue.ToString()
                If ddlMainArea.SelectedValue <> "" Then
                    objDealerBranch.MainArea = New MainAreaFacade(User).Retrieve(CInt(ddlMainArea.SelectedValue))
                End If
                If ddlArea1.SelectedValue <> "" Then
                    objDealerBranch.Area1 = New Area1Facade(User).Retrieve(CInt(ddlArea1.SelectedValue))
                End If
                If ddlArea2.SelectedValue <> "" Then
                    objDealerBranch.Area2 = New Area2Facade(User).Retrieve(CInt(ddlArea2.SelectedValue))
                End If
                If ddlProvince.SelectedValue <> "" Then
                    objDealerBranch.Province = New ProvinceFacade(User).Retrieve(CInt(ddlProvince.SelectedValue))
                End If
                If ddlCity.SelectedValue <> "" Then
                    objDealerBranch.City = New CityFacade(User).Retrieve(CInt(ddlCity.SelectedValue))
                End If

                objDealerBranch.ZipCode = txtPostCode.Text.Trim()
                objDealerBranch.Address = txtAddress.Text.Trim()
                objDealerBranch.Phone = txtTelpArea.Text.Trim().Replace("-", "") & "-" & txtTelpNo.Text.Trim().Replace("-", "")
                objDealerBranch.Fax = txtFaxArea.Text.Trim().Replace("-", "") & "-" & txtFaxNo.Text.Trim().Replace("-", "")
                objDealerBranch.Website = txtWeb.Text.Trim()
                objDealerBranch.Email = txtEmailAdd.Text.Trim()
                objDealerBranch.BranchAssignmentNo = txtBranhAssigment.Text
                objDealerBranch.BranchAssignmentDate = icTglPersetujuan.Value.Date
                objDealerBranch.Status = ddlStatus.SelectedValue
                objDealerBranch.SalesUnitFlag = IIf(cbSalesUnit.Checked, 1, 0)
                objDealerBranch.ServiceFlag = IIf(cbService.Checked, 1, 0)
                objDealerBranch.SparepartFlag = IIf(cbSpareParts.Checked, 1, 0)

                Dim listBuseinesBranch As New ArrayList

                If cbSalesUnit.Checked Then
                    Dim objDBA As New DealerBranchBusinessArea
                    objDBA.ContactPerson = txtContactPerson1.Text
                    objDBA.HP = txtHP1.Text
                    objDBA.DepHeadPIC = txtDeptHead1.Text.Trim()
                    objDBA.SectionHeadPIC = txtSectionHead1.Text.Trim()
                    objDBA.SalesACPIC = txtSalesAC1.Text.Trim()
                    objDBA.Email = txtEmail1.Text
                    objDBA.Kind = CType(EnumDealerBranchTransKind.DealerTransKind.SalesUnit, Integer)
                    listBuseinesBranch.Add(objDBA)
                End If
                If cbService.Checked Then
                    Dim objDBA As New DealerBranchBusinessArea
                    objDBA.ContactPerson = txtContactPerson2.Text
                    objDBA.HP = txtHP2.Text
                    objDBA.DepHeadPIC = txtDeptHead2.Text.Trim()
                    objDBA.SectionHeadPIC = txtSectionHead2.Text.Trim()
                    objDBA.SalesACPIC = txtSalesAC2.Text.Trim()
                    objDBA.Email = txtEmail2.Text
                    objDBA.Kind = CType(EnumDealerBranchTransKind.DealerTransKind.ServiceUnit, Integer)
                    listBuseinesBranch.Add(objDBA)
                End If
                If cbSpareParts.Checked Then
                    Dim objDBA As New DealerBranchBusinessArea
                    objDBA.ContactPerson = txtContactPerson3.Text
                    objDBA.HP = txtHP3.Text
                    objDBA.DepHeadPIC = txtDeptHead3.Text.Trim()
                    objDBA.SectionHeadPIC = txtSectionHead3.Text.Trim()
                    objDBA.SalesACPIC = txtSalesAC3.Text.Trim()
                    objDBA.Email = txtEmail3.Text
                    objDBA.Kind = CType(EnumDealerBranchTransKind.DealerTransKind.SparePartUnit, Integer)
                    listBuseinesBranch.Add(objDBA)
                End If

                Dim drData As New DealerBranchFacade(User)

                Dim x As Integer = drData.Insert(objDealerBranch, listBuseinesBranch)

                If x > 0 Then
                    ViewState("Proses") = "Edit"
                    ViewState("ID") = x

                    MessageBox.Show(SR.SaveSuccess)


                Else
                    MessageBox.Show(SR.SaveFail)
                End If
            Catch ex As Exception
                MessageBox.Show(SR.SaveFail & " " & ex.Message.ToString())
            End Try
        ElseIf ViewState("Proses") = "Edit" Then
            If Not IsValid(False) Then
                Return
            End If
            Try

                Dim drData As New DealerBranchFacade(User)

                Dim objDealerBranch As New DealerBranch
                objDealerBranch = drData.Retrieve(CInt(ViewState("ID")))
                objDealerBranch.TypeBranch = ddlTYpe.SelectedValue.ToString()
                objDealerBranch.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
                objDealerBranch.DealerBranchCode = txtBranchCode.Text.Trim()
                objDealerBranch.Name = txtName.Text
                objDealerBranch.Term1 = txtSearch1.Text
                objDealerBranch.Term2 = txtSearch2.Text
                objDealerBranch.Email = txtEmailAdd.Text.Trim()
                If ddlMainArea.SelectedValue <> "" Then
                    objDealerBranch.MainArea = New MainAreaFacade(User).Retrieve(CInt(ddlMainArea.SelectedValue))
                End If
                If ddlArea1.SelectedValue <> "" Then
                    objDealerBranch.Area1 = New Area1Facade(User).Retrieve(CInt(ddlArea1.SelectedValue))
                End If
                If ddlArea2.SelectedValue <> "" Then
                    objDealerBranch.Area2 = New Area2Facade(User).Retrieve(CInt(ddlArea2.SelectedValue))
                End If
                If ddlProvince.SelectedValue <> "" Then
                    objDealerBranch.Province = New ProvinceFacade(User).Retrieve(CInt(ddlProvince.SelectedValue))
                End If
                If ddlCity.SelectedValue <> "" Then
                    objDealerBranch.City = New CityFacade(User).Retrieve(CInt(ddlCity.SelectedValue))
                End If
                objDealerBranch.ZipCode = txtPostCode.Text.Trim()
                objDealerBranch.Address = txtAddress.Text.Trim()
                objDealerBranch.Phone = txtTelpArea.Text.Trim().Replace("-", "") & "-" & txtTelpNo.Text.Trim().Replace("-", "")
                objDealerBranch.Fax = txtFaxArea.Text.Trim().Replace("-", "") & "-" & txtFaxNo.Text.Trim().Replace("-", "")
                objDealerBranch.Website = txtWeb.Text.Trim()
                objDealerBranch.BranchAssignmentNo = txtBranhAssigment.Text
                objDealerBranch.BranchAssignmentDate = icTglPersetujuan.Value.Date
                objDealerBranch.Status = ddlStatus.SelectedValue
                objDealerBranch.SalesUnitFlag = IIf(cbSalesUnit.Checked, 1, 0)
                objDealerBranch.ServiceFlag = IIf(cbService.Checked, 1, 0)
                objDealerBranch.SparepartFlag = IIf(cbSpareParts.Checked, 1, 0)

                Dim listBuseinesBranch As New ArrayList

                If cbSalesUnit.Checked Then
                    Dim objDBA As New DealerBranchBusinessArea
                    objDBA.ContactPerson = txtContactPerson1.Text
                    objDBA.HP = txtHP1.Text
                    objDBA.DepHeadPIC = txtDeptHead1.Text.Trim()
                    objDBA.SectionHeadPIC = txtSectionHead1.Text.Trim()
                    objDBA.SalesACPIC = txtSalesAC1.Text.Trim()
                    objDBA.Email = txtEmail1.Text
                    objDBA.Kind = CType(EnumDealerBranchTransKind.DealerTransKind.SalesUnit, Integer)
                    listBuseinesBranch.Add(objDBA)
                End If
                If cbService.Checked Then
                    Dim objDBA As New DealerBranchBusinessArea
                    objDBA.ContactPerson = txtContactPerson2.Text
                    objDBA.HP = txtHP2.Text
                    objDBA.DepHeadPIC = txtDeptHead2.Text.Trim()
                    objDBA.SectionHeadPIC = txtSectionHead2.Text.Trim()
                    objDBA.SalesACPIC = txtSalesAC2.Text.Trim()
                    objDBA.Email = txtEmail2.Text
                    objDBA.Kind = CType(EnumDealerBranchTransKind.DealerTransKind.ServiceUnit, Integer)
                    listBuseinesBranch.Add(objDBA)
                End If
                If cbSpareParts.Checked Then
                    Dim objDBA As New DealerBranchBusinessArea
                    objDBA.ContactPerson = txtContactPerson3.Text
                    objDBA.HP = txtHP3.Text
                    objDBA.DepHeadPIC = txtDeptHead3.Text.Trim()
                    objDBA.SectionHeadPIC = txtSectionHead3.Text.Trim()
                    objDBA.SalesACPIC = txtSalesAC3.Text.Trim()
                    objDBA.Email = txtEmail3.Text
                    objDBA.Kind = CType(EnumDealerBranchTransKind.DealerTransKind.SparePartUnit, Integer)
                    listBuseinesBranch.Add(objDBA)
                End If

                Dim x As Integer = drData.Update(objDealerBranch, listBuseinesBranch)

                If x > 0 Then
                    MessageBox.Show(SR.SaveSuccess)
                Else
                    MessageBox.Show(SR.SaveFail)
                End If
            Catch ex As Exception
                MessageBox.Show(SR.SaveFail & " " & ex.Message.ToString())
            End Try



        End If
    End Sub

    Private Sub BindDBToUI(ByVal objDealerBranch As DealerBranch)
        ViewState("ID") = objDealerBranch.ID

        ddlTYpe.SelectedValue = objDealerBranch.TypeBranch
        txtDealerCode.Text = objDealerBranch.Dealer.DealerCode
        txtBranchCode.Text = objDealerBranch.DealerBranchCode
        txtName.Text = objDealerBranch.Name
        txtSearch1.Text = objDealerBranch.Term1
        txtSearch2.Text = objDealerBranch.Term2
        LblLastChange.Text = objDealerBranch.LastUpdateTime.ToString("yyyy/MM/dd HH:mm")
        Try
            BindDdlMainArea()
            ddlMainArea.SelectedValue = objDealerBranch.MainArea.ID
        Catch ex As Exception

        End Try

        Try
            BindDdlArea1()
            ddlArea1.SelectedValue = objDealerBranch.Area1.ID
        Catch ex As Exception

        End Try

        Try
            BindDdlArea2()
            ddlArea2.SelectedValue = objDealerBranch.Area2.ID
        Catch ex As Exception

        End Try
        Try
            BindDdlProvince()
            ddlProvince.SelectedValue = objDealerBranch.Province.ID
        Catch ex As Exception

        End Try

        Try
            BindDdlCity()
            ddlCity.SelectedValue = objDealerBranch.City.ID
        Catch ex As Exception

        End Try

        txtPostCode.Text = objDealerBranch.ZipCode
        txtAddress.Text = objDealerBranch.Address
        txtTelpArea.Text = objDealerBranch.extPhone
        txtTelpNo.Text = objDealerBranch.noPhone
        txtFaxArea.Text = objDealerBranch.extFax
        txtFaxNo.Text = objDealerBranch.nofax
        txtWeb.Text = objDealerBranch.Website
        txtEmailAdd.Text = objDealerBranch.Email
        txtBranhAssigment.Text = objDealerBranch.BranchAssignmentNo
        icTglPersetujuan.Value = objDealerBranch.BranchAssignmentDate
        ddlStatus.SelectedValue = objDealerBranch.Status

        cbSalesUnit.Checked = objDealerBranch.SalesUnitFlag

        cbService.Checked = objDealerBranch.ServiceFlag

        cbSpareParts.Checked = objDealerBranch.SparepartFlag


        For Each objDBA As DealerBranchBusinessArea In objDealerBranch.DealerBranchBusinesAreas
            If objDBA.Kind = CType(EnumDealerBranchTransKind.DealerTransKind.SalesUnit, Integer) Then
                txtContactPerson1.Text = objDBA.ContactPerson
                txtHP1.Text = objDBA.HP
                txtDeptHead1.Text = objDBA.DepHeadPIC
                txtSectionHead1.Text = objDBA.SectionHeadPIC
                txtSalesAC1.Text = objDBA.SalesACPIC
                lblLastUpdate1.Text = objDBA.LastUpdateTime.ToString("yyy/MM/dd HH:mm")
                txtEmail1.Text = objDBA.Email
            ElseIf objDBA.Kind = CType(EnumDealerBranchTransKind.DealerTransKind.ServiceUnit, Integer).ToString Then
                txtContactPerson2.Text = objDBA.ContactPerson
                txtHP2.Text = objDBA.HP
                txtDeptHead2.Text = objDBA.DepHeadPIC
                txtSectionHead2.Text = objDBA.SectionHeadPIC
                txtSalesAC2.Text = objDBA.SalesACPIC
                lblLastUpdate2.Text = objDBA.LastUpdateTime.ToString("yyy/MM/dd HH:mm")
                txtEmail2.Text = objDBA.Email
            ElseIf objDBA.Kind = CType(EnumDealerBranchTransKind.DealerTransKind.SparePartUnit, Integer).ToString Then

                txtContactPerson3.Text = objDBA.ContactPerson
                txtHP3.Text = objDBA.HP
                txtDeptHead3.Text = objDBA.DepHeadPIC
                txtSectionHead3.Text = objDBA.SectionHeadPIC
                txtSalesAC3.Text = objDBA.SalesACPIC
                txtEmail3.Text = objDBA.Email
                lblLastUpdate3.Text = objDBA.LastUpdateTime.ToString("yyy/MM/dd HH:mm")
            End If
        Next

        EnablePanel(pnlSales, cbSalesUnit.Checked)
        EnablePanel(pnlService, cbService.Checked)
        EnablePanel(pnlSpareparts, cbSpareParts.Checked)

    End Sub
#End Region

End Class