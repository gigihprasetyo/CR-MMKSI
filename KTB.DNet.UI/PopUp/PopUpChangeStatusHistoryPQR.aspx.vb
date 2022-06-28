Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility

Public Class PopUpChangeStatusHistoryPQR
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgStatusChangeHistory As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoPQR As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoPQRVal As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private objDomain As PQRHeader

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            objDomain = New PQRHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
            BindData()
        End If
    End Sub

    Private Sub BindData()
        lblNoPQRVal.Text = objDomain.PQRNo
        dtgStatusChangeHistory.DataSource = objDomain.PQRChangesHistorys
        dtgStatusChangeHistory.DataBind()
    End Sub

    Sub dtgStatusChangeHistory_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemIndex <> -1 Then
            Dim RowData As PQRChangesHistory = CType(e.Item.DataItem, PQRChangesHistory)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1

            Dim lblStatusLama As Label = CType(e.Item.FindControl("lblStatusLama"), Label)
            If RowData.OldStatus = "" Then
                lblStatusLama.Text = ""
            Else
                lblStatusLama.Text = CType(RowData.OldStatus, EnumPQR.PQRStatus).ToString
            End If

            Dim lblStatusBaru As Label = CType(e.Item.FindControl("lblStatusBaru"), Label)
            If RowData.NewStatus = "" Then
                lblStatusBaru.Text = ""
            Else
                lblStatusBaru.Text = CType(RowData.NewStatus, EnumPQR.PQRStatus).ToString
            End If

            Dim lblCreatedBy As Label = CType(e.Item.FindControl("lblCreatedBy"), Label)
            If RowData.CreatedBy = "" Then
                lblCreatedBy.Text = ""
            Else
                lblCreatedBy.Text = CommonFunction.FormatSavedUser(RowData.CreatedBy, User)
            End If
        End If
    End Sub
End Class
