Imports System.Collections.Generic

Public Class PopUpErrorExcel
    Inherits System.Web.UI.Page
    Dim helpers As TrainingHelpers = New TrainingHelpers(Me.Page)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            If helpers.GetSession("namaFile") IsNot Nothing Then
                txtNamaFile.Text = CType(helpers.GetSession("namaFile"), String)
            End If

            If helpers.GetSession("dataError") IsNot Nothing Then
                dgErrorExcel.DataSource = CType(helpers.GetSession("dataError"), List(Of ErrorExcelUpload))
                dgErrorExcel.DataBind()
            End If

        End If
    End Sub

End Class