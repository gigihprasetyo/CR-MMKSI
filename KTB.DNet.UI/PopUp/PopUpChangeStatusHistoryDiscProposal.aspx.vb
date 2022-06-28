Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.General

Public Class PopUpChangeStatusHistoryDiscProposal
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label

    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

        InitializeComponent()
    End Sub

#End Region

    Private objDomain As DiscountProposalHeader
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
            Dim RowData As StatusChangeHistory = CType(e.Item.DataItem, StatusChangeHistory)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1

            Dim lblStatusLama As Label = CType(e.Item.FindControl("lblStatusLama"), Label)
            If RowData.OldStatus.ToString = "" Then
                lblStatusLama.Text = ""
            Else
                lblStatusLama.Text = CommonFunction.GetEnumDescription(RowData.OldStatus, "EnumDiscountProposal.Status")
            End If

            Dim lblStatusBaru As Label = CType(e.Item.FindControl("lblStatusBaru"), Label)
            If RowData.NewStatus.ToString = "" Then
                lblStatusBaru.Text = ""
            Else
                lblStatusBaru.Text = CommonFunction.GetEnumDescription(RowData.NewStatus, "EnumDiscountProposal.Status")
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