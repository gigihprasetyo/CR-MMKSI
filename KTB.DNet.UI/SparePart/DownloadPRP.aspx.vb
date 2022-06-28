Imports KTB.DNet.Utility

Namespace KTB.DNet.UI.SparePart

    Public Class DownloadPRP
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents dtgData As System.Web.UI.WebControls.DataGrid

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Dim sHPrp As SessionHelper = New SessionHelper

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        End Sub

        '--- uncomment it if it's needed ---'
        ''Converts a DataTable to Tab Delimited Format
        'Private Function ConvertDtToTDF(ByVal _
        '  dt As DataTable) As String
        '    Dim dr As DataRow, ary() As Object, i As Integer
        '    Dim iCol As Integer

        '    'Output Column Headers
        '    For iCol = 0 To dt.Columns.Count - 1
        '        Response.Write(dt.Columns(iCol).ToString & vbTab)
        '    Next
        '    Response.Write(vbCrLf)

        '    'Output Data
        '    For Each dr In dt.Rows
        '        ary = dr.ItemArray
        '        For i = 0 To UBound(ary)
        '            Response.Write(ary(i).ToString & vbTab)
        '        Next
        '        Response.Write(vbCrLf)
        '    Next
        'End Function

        Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
            Dim filename As String = CStr(sHPrp.GetSession("dspFilename"))
            Dim displayMode As String = CStr(sHPrp.GetSession("DisplayPRP"))
            Dim dt As DataTable = CType(sHPrp.GetSession("excelDataTable"), DataTable)
            If IsNothing(dt) Or IsNothing(filename) Or IsNothing(displayMode) Then
                Response.Redirect(Request.UrlReferrer.ToString)
            End If

            Response.ContentType = "application/x-download"
            Dim uploadFilename As String = New System.Text.StringBuilder("""").Append(System.IO.Path.GetFileNameWithoutExtension(filename)). _
                Append(" - ").Append(displayMode).Append(".xls").Append("""").ToString

            Response.AddHeader("Content-Disposition", _
                New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)

            '--- use this if need to write as separated tab ---'
            'Response.Write(ConvertDtToTDF(dt))

            '--- rich view using datagrid ---'
            dtgData.DataSource = dt
            dtgData.DataBind()
            dtgData.RenderControl(writer)

        End Sub
    End Class
End Namespace
