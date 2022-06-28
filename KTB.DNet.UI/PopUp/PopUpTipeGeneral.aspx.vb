Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports System.Drawing.Color
Imports System
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports System.Linq
Imports System.Collections.Generic

Public Class PopUpTipeGeneral
    Inherits System.Web.UI.Page

#Region "Variable"
#End Region

#Region "Custom Methode"

    Sub bindDataGrid()
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileTypeGeneral), "RowStatus", MatchType.Exact, 0))

        If ddlModel.SelectedValue <> -1 Then
            crit.opAnd(New Criteria(GetType(VechileTypeGeneral), "SubCategoryVehicle", MatchType.Exact, ddlModel.SelectedValue))
        End If
        If txtNamaTipeGeneral.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(VechileTypeGeneral), "Name", MatchType.Partial, txtNamaTipeGeneral.Text.Trim))
        End If
        If ddlStatus.SelectedValue <> -1 Then
            crit.opAnd(New Criteria(GetType(VechileTypeGeneral), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        Dim arrVecTypeGeneral As ArrayList = New VechileTypeGeneralFacade(User).Retrieve(crit)

        If arrVecTypeGeneral.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If

        'dtgVechileTypeGeneral.Columns(3).Visible = False

        dtgVechileTypeGeneral.DataSource = arrVecTypeGeneral
        dtgVechileTypeGeneral.DataBind()
    End Sub

    Sub bindDdlModel()
        Dim subcat As String = Request.QueryString("subCategory")
        Dim subCatVehicle As SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CShort(subcat))
        ddlModel.Items.Add(New ListItem(subCatVehicle.Name, subCatVehicle.ID))
        ddlModel.SelectedValue = subcat
    End Sub

    Sub bindDdlStatus()
        ddlStatus.Items.Add(New ListItem("Silahkan pilih", -1))
        ddlStatus.Items.Add(New ListItem("Aktif", 1))
        ddlStatus.Items.Add(New ListItem("Tidak aktif", 0))
    End Sub

#End Region


#Region "Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bindDdlModel()
            bindDdlStatus()
            bindDataGrid()
        End If
    End Sub

    Protected Sub dtgVechileTypeGeneral_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgVechileTypeGeneral.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim vechileTypeGeneral As VechileTypeGeneral = dtgVechileTypeGeneral.DataSource(e.Item.ItemIndex)

            Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblID As Label = CType(e.Item.FindControl("ID"), Label)

            lblID.Text = vechileTypeGeneral.ID
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
            lblName.Text = vechileTypeGeneral.Name
            If vechileTypeGeneral.Status = 1 Then
                lblStatus.Text = "Aktif"
            Else
                lblStatus.Text = "Tidak Aktif"
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindDataGrid()
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        txtNamaTipeGeneral.Text = ""
        ddlStatus.SelectedValue = -1
        bindDataGrid()
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim vechileTypeGeneralToInsert As VechileTypeGeneral = New VechileTypeGeneral()

        If ddlModel.SelectedValue <> -1 Then
            vechileTypeGeneralToInsert.SubCategoryVehicle = New SubCategoryVehicle(ddlModel.SelectedValue)
        Else
            MessageBox.Show("Silahkan pilih model terlebih dahulu")
            Exit Sub
        End If
        If txtNamaTipeGeneral.Text.Trim <> "" Then
            vechileTypeGeneralToInsert.Name = txtNamaTipeGeneral.Text
        Else
            MessageBox.Show("Silahkan isi nama tipe general terlebih dahulu")
            Exit Sub
        End If
        If ddlStatus.SelectedValue <> -1 Then
            vechileTypeGeneralToInsert.Status = ddlStatus.SelectedValue
        Else
            MessageBox.Show("Silahkan pilih status terlebih dahulu")
            Exit Sub
        End If

        Dim result = New VechileTypeGeneralFacade(User).Insert(vechileTypeGeneralToInsert)
        If result > 0 Then
            MessageBox.Show("Insert tipe general berhasil")
        Else
            MessageBox.Show("Insert gagal")
        End If

        txtNamaTipeGeneral.Text = ""
        ddlStatus.SelectedValue = -1
        bindDataGrid()
    End Sub

#End Region
End Class