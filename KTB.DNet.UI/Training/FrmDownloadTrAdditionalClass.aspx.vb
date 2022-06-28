#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
#End Region

Public Class FrmDownloadTrAdditionalClass
    Inherits System.Web.UI.Page

    Dim sHdownLoad As SessionHelper = New SessionHelper
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Response.ContentType = "application/x-download"

        'Response.AddHeader("Content-Disposition", _
        '    New System.Text.StringBuilder("attachment;filename=").Append("Daftar Kelas.xls").ToString)
        'dtgDwnload.DataSource() = New TrClassFacade(User).Retrieve(CType(sHdownLoad.GetSession("searchRes"), CriteriaComposite))
        'dtgDwnload.DataBind()
    End Sub
    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        Response.ContentType = "application/x-download"

        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append("Daftar Kelas.xls").ToString)
        If Not sHdownLoad.GetSession("trAdditionalClassCrit") Is Nothing Then
            dtgDwnload.DataSource() = New TrAdditionalClassFacade(User).Retrieve(CType(sHdownLoad.GetSession("trAdditionalClassCrit"), CriteriaComposite))
            dtgDwnload.DataBind()
        End If


        dtgDwnload.RenderControl(writer)

    End Sub
    Private Sub dtgDwnload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDwnload.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As TrAdditionalClass = CType(e.Item.DataItem, TrAdditionalClass)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgDwnload.CurrentPageIndex * dtgDwnload.PageSize)

            '-- get city
            Dim lblKategori As Label = CType(e.Item.FindControl("lblKategori"), Label)
            If Not IsNothing(RowValue.TrCourse) Then
                lblKategori.Text = RowValue.TrCourse.CourseCode
            End If

            Dim lblClassType As Label = CType(e.Item.FindControl("lblClassType"), Label)
            If Not String.IsNullOrEmpty(RowValue.ClassType) Then
                lblClassType.Text = EnumTrClass.GetStringValueClassType(RowValue.ClassType)
            End If

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatus.Text = EnumTrAdditionalClass.GetStringValueClassType(RowValue.Status)


        End If
    End Sub
End Class

