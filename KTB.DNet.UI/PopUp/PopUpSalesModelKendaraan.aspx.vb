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

Public Class PopUpSalesModelKendaraan
    Inherits System.Web.UI.Page

#Region "Variable"
#End Region

#Region "Custom Methode"

    Sub bindDataGrid()
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesVechileModel), "RowStatus", MatchType.Exact, 0))

        If ddlKategori.SelectedValue <> -1 Then
            crit.opAnd(New Criteria(GetType(SalesVechileModel), "Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        End If
        If ddlModel.SelectedValue <> -1 Then
            crit.opAnd(New Criteria(GetType(SalesVechileModel), "VechileModel.ID", MatchType.Exact, ddlModel.SelectedValue))
        End If
        If txtDeskripsiModel.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(SalesVechileModel), "NewVechileModelDesc", MatchType.Partial, txtDeskripsiModel.Text.Trim))
        End If

        Dim arrVecSalesModel As ArrayList = New SalesVechileModelFacade(User).Retrieve(crit)

        If arrVecSalesModel.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If

        'dtgSalesVechileModel.Columns(3).Visible = False

        dtgSalesVechileModel.DataSource = arrVecSalesModel
        dtgSalesVechileModel.DataBind()
    End Sub

    Protected Sub dtgSalesVechileModel_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgSalesVechileModel.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                Dim objSalesVechileModel As SalesVechileModel = New SalesVechileModelFacade(User).Retrieve(CShort(e.Item.Cells(5).Text))
                hdnSalesModelID.Value = objSalesVechileModel.ID
                ddlKategori.SelectedValue = objSalesVechileModel.Category.ID
                ddlKategori_SelectedIndexChanged(Nothing, Nothing)
                ddlModel.SelectedValue = objSalesVechileModel.VechileModel.ID
                txtDeskripsiModel.Text = objSalesVechileModel.NewVechileModelDesc
        End Select
    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKategori.SelectedIndexChanged
        If ddlKategori.SelectedIndex = 0 Then
            ddlModel.SelectedIndex = 0
            Exit Sub
        End If
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(Category), "ID", MatchType.Exact, ddlKategori.SelectedValue))
        Dim arrCat As ArrayList = New CategoryFacade(User).Retrieve(crits)
        Dim objCategory As New Category
        If arrCat.Count > 0 Then
            objCategory = CType(arrCat(0), Category)
        End If
        bindDdlModel(objCategory.ID)
    End Sub

    Sub bindDdlKategori()
        Dim arrCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList()
        ddlKategori.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each category As Category In arrCategory
            ddlKategori.Items.Add(New ListItem(category.CategoryCode, category.ID))
        Next
    End Sub

    Sub bindDdlModel(ByVal intCatID As Integer)
        ddlModel.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VechileModel), "Category.ID", MatchType.Exact, intCatID))
        Dim arrModel As ArrayList = New VechileModelFacade(User).Retrieve(criterias)
        ddlModel.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each model As VechileModel In arrModel
            ddlModel.Items.Add(New ListItem(model.IndDescription, model.ID))
        Next
    End Sub

#End Region


#Region "Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bindDdlKategori()
            bindDdlModel(ddlKategori.SelectedValue)
            bindDataGrid()
        End If
    End Sub

    Protected Sub dtgSalesVechileModel_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgSalesVechileModel.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim SalesVechileModel As SalesVechileModel = dtgSalesVechileModel.DataSource(e.Item.ItemIndex)

            Dim lblKategori As Label = CType(e.Item.FindControl("lblKategori"), Label)
            Dim lblModel As Label = CType(e.Item.FindControl("lblModel"), Label)
            Dim lblSalesDeskripsiModel As Label = CType(e.Item.FindControl("lblSalesDeskripsiModel"), Label)
            Dim lblID As Label = CType(e.Item.FindControl("ID"), Label)

            lblID.Text = SalesVechileModel.ID
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
            lblKategori.Text = SalesVechileModel.Category.CategoryCode
            lblModel.Text = SalesVechileModel.VechileModel.IndDescription
            lblSalesDeskripsiModel.Text = SalesVechileModel.NewVechileModelDesc
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindDataGrid()
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        hdnSalesModelID.Value = 0
        txtDeskripsiModel.Text = ""
        ddlKategori.SelectedValue = -1
        ddlModel.SelectedValue = -1
        bindDataGrid()
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim SalesVechileModelToInsert As SalesVechileModel = New SalesVechileModel()

        If ddlKategori.SelectedValue <> -1 Then
            SalesVechileModelToInsert.Category = New Category(ddlKategori.SelectedValue)
        Else
            MessageBox.Show("Silahkan pilih kategori terlebih dahulu")
            Exit Sub
        End If
        If ddlModel.SelectedValue <> -1 Then
            SalesVechileModelToInsert.VechileModel = New VechileModel(ddlModel.SelectedValue)
        Else
            MessageBox.Show("Silahkan pilih model terlebih dahulu")
            Exit Sub
        End If
        If txtDeskripsiModel.Text.Trim <> "" Then
            SalesVechileModelToInsert.NewVechileModelDesc = txtDeskripsiModel.Text
        Else
            MessageBox.Show("Silahkan isi deskripsi model terlebih dahulu")
            Exit Sub
        End If
        Dim alert As String = ""
        Dim result As Integer = 0
        If hdnSalesModelID.Value = 0 Then
            result = New SalesVechileModelFacade(User).Insert(SalesVechileModelToInsert)
            alert = "Data Insert Sales Model Berhasil di Simpan"
        Else
            SalesVechileModelToInsert.ID = hdnSalesModelID.Value
            result = New SalesVechileModelFacade(User).Update(SalesVechileModelToInsert)
            alert = "Data Sales Model Berhasil di Ubah"
        End If

        If result > 0 Then
            MessageBox.Show(alert)
        Else
            MessageBox.Show("Data Insert Sales Model Gagal di Simpan")
        End If

        txtDeskripsiModel.Text = ""
        ddlKategori.SelectedValue = -1
        ddlModel.SelectedValue = -1
        bindDataGrid()
    End Sub

#End Region

End Class