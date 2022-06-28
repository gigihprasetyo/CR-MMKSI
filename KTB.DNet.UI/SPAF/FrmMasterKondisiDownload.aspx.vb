Imports KTB.DNet.Utility

Public Class FrmMasterKondisiDownload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgMasterKondisi As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub BindGrid()
        Dim sesHelper As New SessionHelper
        dgMasterKondisi.DataSource = sesHelper.GetSession("sessMasterKondisiDB2")
        dgMasterKondisi.DataBind()
    End Sub

    Private Sub DownloadPage()
        Response.Clear()
        'Response.Buffer = True
        Response.Charset = ""
        Me.EnableViewState = False
        Dim oStringWriter As System.IO.StringWriter = New System.IO.StringWriter
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriter)
        Me.Controls.Clear() 'ClearControls(dgUploadData)
        dgMasterKondisi.RenderControl(oHtmlTextWriter)
        Response.Write(oStringWriter.ToString())

        Response.ContentType = "application/x-dgMasterKondisi"
        Response.AddHeader("Content-Disposition", "attachment;filename=""MasterKondisi.xls""")
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BindGrid()
        DownloadPage()
    End Sub

    Private Sub dgMasterKondisi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgMasterKondisi.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dgMasterKondisi.PageSize * dgMasterKondisi.CurrentPageIndex)).ToString
        End If
    End Sub
End Class