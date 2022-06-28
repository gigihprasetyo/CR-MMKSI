#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.MDP
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

Public Class FrmMDPMasterVehicle
    Inherits System.Web.UI.Page

#Region " Private Variables "
    Private sessHelp As SessionHelper = New SessionHelper
    Private arrMasterVehicle As ArrayList
#End Region

#Region "Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckUserPrivilege()
        If Not IsPostBack Then
            BindDropDownList()  '-- Bind dropdownlists at first time
            'CommonFunction.BindVehicleSubCategoryToDDL(ddlSubCategory, ddlCategory.SelectedItem.Text)

            '-- Init sorting column and sort direction default
            ViewState("currSortColumn") = ""
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGridMasterVehicle(0)
    End Sub

    Protected Sub dtgMasterVehicle_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMasterVehicle.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim objMDPMV As MDPMasterVehicle
            If IsNothing(arrMasterVehicle) Then
                arrMasterVehicle = sessHelp.GetSession("MVData")
            End If
            If Not IsNothing(arrMasterVehicle) Then
                objMDPMV = CType(arrMasterVehicle(e.Item.ItemIndex), MDPMasterVehicle)

                Dim lblStatus As Label = e.Item.FindControl("lblStatus")
                Dim lblTglUpdate As Label = e.Item.FindControl("lblTglUpdate")

                lblTglUpdate.Text = objMDPMV.LastUpdateTime.ToString("dd/MM/yyyy")

                If objMDPMV.Status = 1 Then
                    lblStatus.Text = "Active"
                Else
                    lblStatus.Text = "Non Active"
                End If
            End If

            Dim lblNo As Label = e.Item.FindControl("lblNo")
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgMasterVehicle.CurrentPageIndex * dtgMasterVehicle.PageSize)

            Dim lnkbtnPopUp As LinkButton = CType(e.Item.FindControl("lnkbtnPopUp"), LinkButton)
            lnkbtnPopUp.Attributes("OnClick") = "showPopUp('../PopUp/PopUpMDPMasterVehicleHistory.aspx?ID=" & e.Item.Cells(1).Text & "&MaterialDescription=" & objMDPMV.VehicleColor.MaterialDescription & "','',500,760,'');"
            'lnkbtnPopUp.Attributes("OnClick") = GeneralScript.GetPopUpEventReference( _
            '    "../PopUp/PopUpMDPMasterVehicleHistory.aspx?ID=" & e.Item.Cells(1).Text, "", 500, 760, "null")
        End If
    End Sub


    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        'CommonFunction.BindVehicleSubCategoryToDDL(ddlSubCategory, ddlCategory.SelectedItem.Text)
        If ddlCategory.SelectedIndex > 0 Then
            BindDDLSubCategory()
            BindVehicleType(True)
        Else
            ddlSubCategory.Items.Clear()
            ddlSubCategory.Items.Add(New ListItem("Silahkan pilih", -1))
        End If
    End Sub

    Protected Sub ddlSubCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        BindVehicleType(False)
    End Sub

    Protected Sub dtgMasterVehicle_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgMasterVehicle.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        dtgMasterVehicle.SelectedIndex = -1
        dtgMasterVehicle.CurrentPageIndex = 0
        BindGridMasterVehicle(dtgMasterVehicle.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom Method"
    Private Sub BindDropDownList()
        '-- Bind Status dropdownlist
        ddlStatus.Items.Clear()
        Dim Activeitem As New ListItem("Aktif", "1")
        ddlStatus.Items.Add(Activeitem)
        Dim Deleteitem As New ListItem("Non-Aktif", "0")
        ddlStatus.Items.Add(Deleteitem)

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

        ddlType.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
        ddlSubCategory.Items.Add(New ListItem("Silahkan pilih", -1))
    End Sub

    Private Sub BindVehicleType(ByVal IsClearAll As Boolean)
        ddlType.Items.Clear()
        If ddlSubCategory.SelectedValue <> -1 And Not IsClearAll Then
            '-- Vehicle criteria & sort
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.SubCategoryVehicleToModel.SubCategoryVehicle.ID", MatchType.Exact, ddlSubCategory.SelectedValue))
            'vehicletype.vehiclemodel.sub
            '-- Type still active
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "VechileType")

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))  '-- Sort by Vechile type code

            '-- Bind Vehicle type dropdownlist
            ddlType.DataSource = New VechileTypeFacade(User).RetrieveByCriteria(criterias, sortColl)
            ddlType.DataTextField = "VechileTypeCode"
            ddlType.DataValueField = "VechileTypeCode"
            ddlType.DataBind()
        End If
        ddlType.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

    End Sub

    Private Sub BindGridMasterVehicle(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(MDPMasterVehicle), "Status", MatchType.Exact, ddlStatus.SelectedValue))

        If ddlCategory.SelectedValue <> "" Then  '-- Category
            criterias.opAnd(New Criteria(GetType(MDPMasterVehicle), "VehicleColor.VechileType.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlSubCategory.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(MDPMasterVehicle), "VehicleColor.VechileType.VechileModel.SubCategoryVehicleToModel.SubCategoryVehicle.ID", MatchType.Exact, ddlSubCategory.SelectedValue))
        End If
        If ddlType.SelectedValue <> "" Then  '-- Vechile type
            criterias.opAnd(New Criteria(GetType(MDPMasterVehicle), "VehicleColor.VechileType.VechileTypeCode", MatchType.Exact, ddlType.SelectedValue))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MDPMasterVehicle), "VehicleColor.VechileType.Category.CategoryCode", Sort.SortDirection.ASC))
        sortColl.Add(New Sort(GetType(MDPMasterVehicle), "VehicleColor.VechileType.VechileTypeCode", Sort.SortDirection.ASC))
        sortColl.Add(New Sort(GetType(MDPMasterVehicle), "VehicleColor.ColorCode", Sort.SortDirection.ASC))

        arrMasterVehicle = New MDPMasterVehicleFacade(User).RetrieveActiveList(currentPageIndex + 1, _
                                            dtgMasterVehicle.PageSize, total, CType(ViewState("CurrentSortColumn"), String), _
                                            CType(ViewState("CurrentSortDirect"), Sort.SortDirection), criterias)

        dtgMasterVehicle.DataSource = arrMasterVehicle
        dtgMasterVehicle.VirtualItemCount = total

        dtgMasterVehicle.DataBind()
        If arrMasterVehicle.Count = 0 Then
            MessageBox.Show("Data Tidak Ditemukan")
        End If
        sessHelp.SetSession("MVData", arrMasterVehicle)
    End Sub

    Private Sub BindDDLSubCategory()
        ddlSubCategory.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))

        Dim arrSubCategoryVehicle As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(criterias)

        ddlSubCategory.Items.Add(New ListItem("Silahkan pilih", -1))
        ddlSubCategory.DataSource = arrSubCategoryVehicle
        ddlSubCategory.DataTextField = "Name"
        ddlSubCategory.DataValueField = "ID"
        ddlSubCategory.DataBind()
    End Sub

    Private Sub CheckUserPrivilege()
        If (Not SecurityProvider.Authorize(Context.User, SR.MDP_Master_Kendaraan_Privilege)) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Monthly Delivery Planning - MDP Master Kendaraan")
        End If
    End Sub

#End Region


End Class