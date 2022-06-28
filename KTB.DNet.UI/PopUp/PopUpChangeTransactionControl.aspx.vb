Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General

Public Class PopUpChangeTransactionControl
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipeTransaksi As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipeTransaksiValue As System.Web.UI.WebControls.Label
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

    Private transactionControlID As Integer
    Dim ArlTcHistory As ArrayList
    Dim objTC As TransactionControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            BindHeader()
            RetrieveData()
        End If
    End Sub

    Private Sub BindHeader()
        'transactionControlID = CInt(Request.QueryString("id"))
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Dealer.ID", MatchType.Exact, CInt(Request.QueryString("id"))))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Kind", MatchType.Exact, CInt(Request.QueryString("TransType"))))
        objTC = New DealerFacade(User).RetrieveTransactionControlByCriteria(criterias)
        If Not objTC Is Nothing Then
            lblDealerValue.Text = objTC.Dealer.DealerCode
            lblTipeTransaksiValue.Text = CType(objTC.Kind, EnumDealerTransType.DealerTransKind).ToString()
            transactionControlID = objTC.ID
        End If

    End Sub

    Private Sub RetrieveData()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransactionControlHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControlHistory), "TransactionControl.ID", MatchType.Exact, transactionControlID))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(TransactionControlHistory), "CreatedTime", Sort.SortDirection.DESC))
        ArlTcHistory = New TransactionControlHistoryFacade(User).Retrieve(criterias, sortColl)
        BindToGrid()
    End Sub

    Private Sub BindToGrid()
        dtgStatusChangeHistory.DataSource = ArlTcHistory
        dtgStatusChangeHistory.DataBind()
    End Sub

    Sub dtgStatusChangeHistory_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemIndex <> -1 Then
            Dim objTcHistory As TransactionControlHistory = ArlTcHistory(e.Item.ItemIndex)
            If objTcHistory.StatusFrom <> -1 Then
                e.Item.Cells(2).Text = CType(objTcHistory.StatusFrom, EnumDealerStatus.DealerStatus).ToString()
            End If
            e.Item.Cells(3).Text = CType(objTcHistory.StatusTo, EnumDealerStatus.DealerStatus).ToString()
            e.Item.Cells(5).Text = UserInfo.Convert(ArlTcHistory(e.Item.ItemIndex).CreatedBy)
        End If
    End Sub
End Class
