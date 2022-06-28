Imports KTB.DNet.Utility
Imports KTB.DNet.Domain

Public Class FrmDownloadErrorSPAF
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgUploadData As System.Web.UI.WebControls.DataGrid
    Protected WithEvents rptData As System.Web.UI.WebControls.Repeater

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        BindData()
        DownloadPage()
    End Sub

    Private Sub DownloadPage()

        Response.Clear()
        'Response.Buffer = True
        Response.ContentType = "application/x-download"
        Response.Charset = ""
        Me.EnableViewState = False
        Dim oStringWriter As System.IO.StringWriter = New System.IO.StringWriter
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriter)
        Me.Controls.Clear() 'ClearControls(dgUploadData)
        dgUploadData.RenderControl(oHtmlTextWriter)
        Response.Write(oStringWriter.ToString())

        Dim _guid As String = Guid.NewGuid().ToString()
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("ErrorSPAF" & _guid.ToUpper.Substring(0, 5) & ".xls").Append("""").ToString()
        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
        'Response.End()


        'Dim _guid As String = Guid.NewGuid().ToString()
        'Response.ContentType = "application/x-download"
        'Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("ErrorSPAF" & _guid & ".xls").Append("""").ToString()
        'Response.AddHeader("Content-Disposition", _
        '    New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Sub

    Private Sub BindData()
        Dim sHelper As New SessionHelper
        Dim objData As ArrayList = CType(sHelper.GetSession("DataUpload"), ArrayList)
        If Not (objData Is Nothing) Then
            Me.dgUploadData.DataSource = objData
            Me.dgUploadData.DataBind()

        End If
        sHelper.RemoveSession("DataUpload")
    End Sub

    Private Sub dgUploadData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUploadData.ItemDataBound

        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
        End If


    End Sub


End Class
