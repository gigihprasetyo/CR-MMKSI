Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General

Public Class PopUpChangeStatusHistory
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblJenisDokumen As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRegDokumen As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisDokumenValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRegDokumenValue As System.Web.UI.WebControls.Label
    Protected WithEvents dtgStatusChangeHistory As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private arrListStatusChangeHistory As ArrayList

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            BindHeader()
            RetrieveData()
        End If
    End Sub

    Private Sub BindHeader()
        lblJenisDokumenValue.Text = CType(Request.QueryString("DocType"), LookUp.DocumentType).ToString
        lblNoRegDokumenValue.Text = Request.QueryString("DocNumber")
    End Sub

    Private Sub RetrieveData()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StatusChangeHistory), "DocumentType", MatchType.Exact, CInt(Request.QueryString("DocType"))))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, Request.QueryString("DocNumber")))
        arrListStatusChangeHistory = New StatusChangeHistoryFacade(User).Retrieve(criterias)
        BindToGrid()
    End Sub

    Private Sub BindToGrid()
        dtgStatusChangeHistory.DataSource = arrListStatusChangeHistory
        dtgStatusChangeHistory.DataBind()
    End Sub

    Sub dtgStatusChangeHistory_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemIndex <> -1 Then
            If CInt(Request.QueryString("DocType")) = LookUp.DocumentType.Surat_Pesanan_Kendaraan Then
                If arrListStatusChangeHistory(e.Item.ItemIndex).OldStatus <> -1 Then
                    e.Item.Cells(2).Text = CType(arrListStatusChangeHistory(e.Item.ItemIndex).OldStatus, EnumStatusSPK.Status).ToString
                Else
                    e.Item.Cells(2).Text = ""
                End If
                e.Item.Cells(3).Text = CType(arrListStatusChangeHistory(e.Item.ItemIndex).NewStatus, EnumStatusSPK.Status).ToString
                e.Item.Cells(5).Text = UserInfo.Convert(arrListStatusChangeHistory(e.Item.ItemIndex).CreatedBy)
            End If
        End If
    End Sub
End Class
