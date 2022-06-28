Public Class FrmSettingMandatory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dgFiles.DataSource = New ArrayList
        dgFiles.DataBind()

        DataGrid1.DataSource = New ArrayList
        DataGrid1.DataBind()

        DataGrid2.DataSource = New ArrayList
        DataGrid2.DataBind()
    End Sub

End Class