Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility

Public Class FrmDPFleetCustomerDetail
    Inherits System.Web.UI.Page

    Private sessHelper As New SessionHelper
    Private _sessData As String = "FrmDPFleetCustomerDetail._sessData"
    Private _sessEdit As String = "FrmDPFleetCustomerDetail._sessEdit"
    Private _sessDelete As String = "FrmDPFleetCustomerDetail._sessDelete"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            sessHelper.SetSession(_sessDelete, New ArrayList)
            sessHelper.SetSession(_sessEdit, New ArrayList)

            If IsNothing(ViewState("FleetHeaderID")) Then
                ViewState("FleetHeaderID") = CInt(Request.QueryString("FleetHeaderID"))
            End If
            If IsNothing(ViewState("Mode")) Then
                ViewState("MODE") = Request.QueryString("MODE").ToString
            End If
            ViewState.Add("SortColFrmDPFleetCustomerDetail", "ID")
            ViewState.Add("SortDirFrmDPFleetCustomerDetail", Sort.SortDirection.DESC)
            BindDDLProvince()
            ControlMode(ViewState("MODE").ToString)
            LoadData()
            BindGrid(0)
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        'If IsNothing(sessHelper.GetSession(_sessDelete)) Then
        '    sessHelper.SetSession(_sessDelete, New ArrayList)
        'End If
        'If IsNothing(sessHelper.GetSession(_sessEdit)) Then
        '    sessHelper.SetSession(_sessEdit, New ArrayList)
        'End If

    End Sub

    Private Sub ControlMode(ByVal mode As String)
        Select Case mode
            Case "VIEW"
                lblFleetGroup.Visible = True
                lblTipe.Visible = True
                lblJenisUsaha.Visible = True
                lblKategori.Visible = True
                lblNama.Visible = True
                dtgMain.Columns(10).Visible = False
                btnCari.Visible = True
            Case "EDIT"
                txtFleetGroup.Visible = True
                ddlTipe.Visible = True
                ddlJenisUsaha.Visible = True
                ddlKategori.Visible = True
                txtNama.Visible = True
                btnSimpan.Visible = True
                trFilter1.Visible = False
                trFilter2.Visible = False
                trFilter3.Visible = False
                trFilter4.Visible = False
                BindDDLTipe()
                BindDDLJenisUsaha()
                BindDDLKategori()
        End Select
    End Sub

    Private Sub LoadData()
        Dim FleetHeader As FleetCustomerHeader = New FleetCustomerHeaderFacade(User).Retrieve(CInt(ViewState("FleetHeaderID")))
        lblKodeFleet.Text = FleetHeader.FleetCode
        Dim stdCode As ArrayList = New StandardCodeFacade(User).RetrieveByValueId(FleetHeader.FleetCustomerType, "EnumDiscountProposal.CustomerType")
        If stdCode.Count > 0 Then
            Dim vData As StandardCode = CType(stdCode(0), StandardCode)
            lblTipe.Text = vData.ValueDesc
            ddlTipe.SelectedValue = vData.ValueId
        Else
            lblTipe.Text = ""
            ddlTipe.SelectedIndex = 0
        End If

        stdCode = New StandardCodeFacade(User).RetrieveByValueId(FleetHeader.FleetCompanyCategory, "EnumDiscountProposal.FleetCategory")
        If stdCode.Count > 0 Then
            Dim vData As StandardCode = CType(stdCode(0), StandardCode)
            lblKategori.Text = vData.ValueDesc
            ddlKategori.SelectedValue = vData.ValueId
        Else
            lblKategori.Text = ""
            ddlKategori.SelectedIndex = 0
        End If

        lblNama.Text = FleetHeader.FleetCustomerName
        txtNama.Text = FleetHeader.FleetCustomerName

        lblFleetGroup.Text = FleetHeader.FleetCustomerGroupCompany
        txtFleetGroup.Text = FleetHeader.FleetCustomerGroupCompany

        lblJenisUsaha.Text = FleetHeader.BusinessSectorDetail.BusinessSectorHeader.BusinessSectorName & " - " & FleetHeader.BusinessSectorDetail.BusinessDomain
        ddlJenisUsaha.SelectedValue = FleetHeader.BusinessSectorDetail.ID

        lblTglPengajualFleet.Text = FleetHeader.CreatedTime.ToString("yyyy/MM/dd")
    End Sub

    Private Sub BindGrid(ByVal index As Integer)
        Dim crit As CriteriaComposite = getCriteria()
        Dim totalrow As Integer = 0
        Dim arlFleetCustomerDetail As ArrayList = New FleetCustomerDetailFacade(User).RetrieveActiveList(crit, index + 1,
                               dtgMain.PageSize,
                               totalrow,
                               CType(ViewState("SortColFrmDPFleetCustomerDetail"), String),
                               CType(ViewState("SortDirFrmDPFleetCustomerDetail"), Sort.SortDirection))


        If CType(sessHelper.GetSession(_sessEdit), ArrayList).Count > 0 Then
            For i As Integer = 0 To arlFleetCustomerDetail.Count - 1
                For Each j As FleetCustomerDetail In CType(sessHelper.GetSession(_sessEdit), ArrayList)
                    If arlFleetCustomerDetail(i).ID = j.ID Then
                        arlFleetCustomerDetail(i) = j
                    End If
                Next
            Next
        End If

        sessHelper.SetSession(_sessData, arlFleetCustomerDetail)
        dtgMain.DataSource = arlFleetCustomerDetail
        dtgMain.VirtualItemCount = totalrow
        dtgMain.DataBind()
    End Sub

    Private Function getCriteria() As CriteriaComposite
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(FleetCustomerDetail), "FleetCustomerHeader.ID", MatchType.Exact, CInt(ViewState("FleetHeaderID"))))

        If txtKodeDealer.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(FleetCustomerDetail), "Dealer.DealerCode", MatchType.[Partial], txtKodeDealer.Text.Trim))
        End If

        If ddlProvinsi.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(FleetCustomerDetail), "City.Province.ID", MatchType.Exact, ddlProvinsi.SelectedValue))
        End If
        If CType(sessHelper.GetSession(_sessDelete), ArrayList).Count > 0 Then

            Dim notID As String = String.Empty
            For Each i As FleetCustomerDetail In CType(sessHelper.GetSession(_sessDelete), ArrayList)
                If notID.Trim.Length > 0 Then
                    notID = notID & ", " & i.ID
                Else
                    notID = i.ID
                End If
            Next
            crit.opAnd(New Criteria(GetType(FleetCustomerDetail), "ID", MatchType.NotInSet, notID))
        End If
        Return crit
    End Function

    Private Sub BindDDLProvince()
        ddlProvinsi.Items.Clear()
        ddlProvinsi.Items.Add(New ListItem("Silahkan Pilih", -1))
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlProvince As ArrayList = New ProvinceFacade(User).Retrieve(criterias2)
        If Not IsNothing(arlProvince) AndAlso arlProvince.Count > 0 Then
            For Each prov As Province In arlProvince
                ddlProvinsi.Items.Add(New ListItem(prov.ProvinceName, prov.ID))
            Next
        End If
    End Sub

    Private Sub BindDDLTipe()
        ddlTipe.Items.Clear()
        ddlTipe.Items.Add(New ListItem("Silahkan Pilih", -1))
        Dim stdCode As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.CustomerType")
        If Not IsNothing(stdCode) AndAlso stdCode.Count > 0 Then
            For Each sCode As StandardCode In stdCode
                'If sCode.ValueId <> 0 Then
                ddlTipe.Items.Add(New ListItem(sCode.ValueDesc, sCode.ValueId))
                'End If
            Next
        End If
    End Sub

    Private Sub BindDDLJenisUsaha()
        ddlJenisUsaha.Items.Clear()
        ddlJenisUsaha.Items.Add(New ListItem("Silahkan Pilih", -1))
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessSectorDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlJenisUsaha As ArrayList = New BusinessSectorDetailFacade(User).Retrieve(criterias2)
        If Not IsNothing(arlJenisUsaha) AndAlso arlJenisUsaha.Count > 0 Then
            For Each jUsaha As BusinessSectorDetail In arlJenisUsaha
                ddlJenisUsaha.Items.Add(New ListItem(jUsaha.BusinessSectorHeader.BusinessSectorName & " - " & jUsaha.BusinessDomain, jUsaha.ID))
            Next
        End If
    End Sub

    Private Sub BindDDLKategori()
        ddlKategori.Items.Clear()
        ddlKategori.Items.Add(New ListItem("Silahkan Pilih", -1))
        Dim stdCode As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("EnumFleetMaster.CategoryFleet")
        If Not IsNothing(stdCode) AndAlso stdCode.Count > 0 Then
            For Each sCode As StandardCode In stdCode
                ddlKategori.Items.Add(New ListItem(sCode.ValueDesc, sCode.ValueId))
            Next
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmDPFleetCustomerList.aspx?IsBack=true")
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindGrid(0)
    End Sub

    Protected Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oFCD As FleetCustomerDetail = CType(e.Item.DataItem, FleetCustomerDetail)
            Dim lblGridTipeIdentitas As Label = CType(e.Item.FindControl("lblGridTipeIdentitas"), Label)
            Dim lblGridStatus As Label = CType(e.Item.FindControl("lblGridStatus"), Label)
            Dim stdCode As ArrayList = New StandardCodeFacade(User).RetrieveByValueId(oFCD.IdentityType, "EnumFleetmanagement.IdentityType")
            If stdCode.Count > 0 Then
                lblGridTipeIdentitas.Text = CType(stdCode(0), StandardCode).ValueDesc
            Else
                lblGridTipeIdentitas.Text = ""
            End If

            stdCode = New StandardCodeFacade(User).RetrieveByValueId(oFCD.FleetStatus, "EnumFleetMaster.Status")
            If stdCode.Count > 0 Then
                lblGridStatus.Text = CType(stdCode(0), StandardCode).ValueDesc
            Else
                lblGridStatus.Text = ""
            End If
        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            Dim oFCD As FleetCustomerDetail = CType(e.Item.DataItem, FleetCustomerDetail)
            Dim ddlGridKota As DropDownList = CType(e.Item.FindControl("ddlGridKota"), DropDownList)
            Dim ddlGridTipeIdentitas As DropDownList = CType(e.Item.FindControl("ddlGridTipeIdentitas"), DropDownList)
            Dim ddlGridStatus As DropDownList = CType(e.Item.FindControl("ddlGridStatus"), DropDownList)

            'Bind Kota
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))
            Dim arlCity As ArrayList = New CityFacade(User).Retrieve(crit)
            ddlGridKota.Items.Clear()
            If arlCity.Count > 0 Then
                ddlGridKota.DataSource = arlCity
                ddlGridKota.DataValueField = "ID"
                ddlGridKota.DataTextField = "CityName"
                ddlGridKota.DataBind()
            Else
                ddlGridKota.DataSource = New ArrayList
                ddlGridKota.DataBind()
            End If
            If Not IsNothing(oFCD.City) Then
                ddlGridKota.SelectedValue = oFCD.City.ID
            Else
                ddlGridKota.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                ddlGridKota.SelectedIndex = 0
            End If

            'Bind ID Type
            Dim arlStdCode As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("EnumFleetmanagement.IdentityType")
            If arlStdCode.Count > 0 Then
                ddlGridTipeIdentitas.DataSource = arlStdCode
                ddlGridTipeIdentitas.DataValueField = "ValueId"
                ddlGridTipeIdentitas.DataTextField = "ValueDesc"
                ddlGridTipeIdentitas.DataBind()
            Else
                ddlGridTipeIdentitas.DataSource = New ArrayList
                ddlGridTipeIdentitas.DataBind()
            End If
            If Not IsNothing(oFCD.IdentityType) Then
                ddlGridTipeIdentitas.SelectedValue = oFCD.IdentityType
            Else
                ddlGridTipeIdentitas.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                ddlGridTipeIdentitas.SelectedIndex = 0
            End If

            'Bind Status
            arlStdCode = New StandardCodeFacade(User).RetrieveByCategory("EnumFleetMaster.Status")
            If arlStdCode.Count > 0 Then
                ddlGridStatus.DataSource = arlStdCode
                ddlGridStatus.DataValueField = "ValueId"
                ddlGridStatus.DataTextField = "ValueDesc"
                ddlGridStatus.DataBind()
            Else
                ddlGridStatus.DataSource = New ArrayList
                ddlGridStatus.DataBind()
            End If
            If Not IsNothing(oFCD.FleetStatus) Then
                ddlGridStatus.SelectedValue = oFCD.FleetStatus
            Else
                ddlGridStatus.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                ddlGridStatus.SelectedIndex = 0
            End If
        End If
    End Sub

    Protected Sub dtgMain_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        Dim oFCD As FleetCustomerDetail = CType(CType(sessHelper.GetSession(_sessData), ArrayList)(e.Item.ItemIndex), FleetCustomerDetail)
        Dim nResult As Integer
        Select Case e.CommandName
            Case "Edit"
                dtgMain.EditItemIndex = e.Item.ItemIndex

            Case "Delete"
                DeleteDataDetail(oFCD, e.Item.ItemIndex)

            Case "Save"
                Dim ddlGridKota As DropDownList = CType(e.Item.FindControl("ddlGridKota"), DropDownList)
                Dim ddlGridTipeIdentitas As DropDownList = CType(e.Item.FindControl("ddlGridTipeIdentitas"), DropDownList)
                Dim ddlGridStatus As DropDownList = CType(e.Item.FindControl("ddlGridStatus"), DropDownList)
                Dim txtGridAlamat As TextBox = CType(e.Item.FindControl("txtGridAlamat"), TextBox)
                Dim txtGridNoIdentitas As TextBox = CType(e.Item.FindControl("txtGridNoIdentitas"), TextBox)
                Dim txtGridNoNPWP As TextBox = CType(e.Item.FindControl("txtGridNoNPWP"), TextBox)
                Dim kota As City = New CityFacade(User).Retrieve(CInt(ddlGridKota.SelectedValue))
                oFCD.City = kota
                oFCD.IdentityType = ddlGridTipeIdentitas.SelectedValue
                oFCD.FleetStatus = ddlGridStatus.SelectedValue
                oFCD.Address = txtGridAlamat.Text
                oFCD.IdentityNumber = txtGridNoIdentitas.Text
                oFCD.NPWPNo = txtGridNoNPWP.Text
                UpdateDataDetail(oFCD, e.Item.ItemIndex)

            Case "Cancel"
                dtgMain.EditItemIndex = -1

        End Select

        BindGrid(0)
    End Sub

    Private Sub DeleteDataDetail(ByVal oFCD As FleetCustomerDetail, ByVal index As Integer)
        If isUsedData(oFCD) Then
            MessageBox.Show("Fleet Customer Data sudah digunakan pada pengajuan Discount Proposal")
            Exit Sub
        End If
        Dim arlDelete As ArrayList = sessHelper.GetSession(_sessDelete)
        arlDelete.Add(oFCD)
        Dim arlData As ArrayList = sessHelper.GetSession(_sessData)
        arlData.RemoveAt(index)
        sessHelper.SetSession(_sessDelete, arlDelete)
        dtgMain.EditItemIndex = -1
    End Sub

    Private Sub UpdateDataDetail(ByVal oFCD As FleetCustomerDetail, ByVal index As Integer)
        Dim arlUpdate As ArrayList = sessHelper.GetSession(_sessEdit)
        arlUpdate.Add(oFCD)
        Dim arlData As ArrayList = sessHelper.GetSession(_sessData)
        arlData(index) = oFCD
        sessHelper.SetSession(_sessEdit, arlUpdate)
        dtgMain.EditItemIndex = -1
    End Sub

    Private Function isUsedData(ByVal objFCD As FleetCustomerDetail) As Boolean
        Dim arlDataDP As ArrayList = New DiscountProposalHeaderFacade(User).RetrieveByFleetCustomerDetail(objFCD)
        If arlDataDP.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim oFleetHeader As FleetCustomerHeader = New FleetCustomerHeaderFacade(User).Retrieve(CInt(ViewState("FleetHeaderID")))
        oFleetHeader.FleetCustomerType = ddlTipe.SelectedValue
        oFleetHeader.FleetCompanyCategory = ddlKategori.SelectedValue
        oFleetHeader.FleetCustomerName = txtNama.Text
        oFleetHeader.FleetCustomerGroupCompany = txtFleetGroup.Text
        oFleetHeader.BusinessSectorDetail = New BusinessSectorDetailFacade(User).Retrieve(CInt(ddlJenisUsaha.SelectedValue))
        Dim arrEdit As ArrayList = CType(sessHelper.GetSession(_sessEdit), ArrayList)
        If IsNothing(arrEdit) Then
            arrEdit = New ArrayList
        End If
        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(_sessDelete), ArrayList)
        If IsNothing(arrDelete) Then
            arrDelete = New ArrayList
        End If
        Dim nResult As Integer = New FleetCustomerHeaderFacade(User).UpdateTransactionDetail(oFleetHeader, arrEdit, arrDelete)
        If nResult <= 0 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If

        BindGrid(0)
    End Sub
End Class