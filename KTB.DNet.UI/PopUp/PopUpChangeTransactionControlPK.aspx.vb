Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade

Public Class PopUpChangeTransactionControlPK
    Inherits System.Web.UI.Page


    Private TransactionControlPKID As Integer
    Dim ArlTcHistory As ArrayList
    Dim objTC As TransactionControlPK
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            BindHeader()
            RetrieveData()
        End If
    End Sub

    Private Sub BindHeader()
        'TransactionControlPKID = CInt(Request.QueryString("id"))
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransactionControlPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControlPK), "ID", MatchType.Exact, CInt(Request.QueryString("id"))))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControlPK), "Kind", MatchType.Exact, CInt(Request.QueryString("TransType"))))
        'Dim list As ArrayList = New TransactionControlPKFacade(User).Retrieve(criterias)
        'If (Not IsNothing(list) AndAlso list.Count > 0) Then
        '    objTC = CType(list.Item(0), TransactionControlPK)
        'End If
        objTC = New TransactionControlPKFacade(User).Retrieve(CInt(Request.QueryString("id")))

        If Not objTC Is Nothing Then
            lblDealerValue.Text = objTC.Dealer.DealerCode
            lblTipeTransaksiValue.Text = CType(objTC.Kind, EnumDealerTransType.DealerTransKind).ToString()
            TransactionControlPKID = objTC.ID
        End If

    End Sub

    Private Sub RetrieveData()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransactionControlPKHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControlPKHistory), "TransactionControlPK.ID", MatchType.Exact, TransactionControlPKID))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(TransactionControlPKHistory), "CreatedTime", Sort.SortDirection.DESC))
        ArlTcHistory = New TransactionControlPKHistoryFacade(User).Retrieve(criterias, sortColl)
        BindToGrid()
    End Sub

    Private Sub BindToGrid()
        dtgStatusChangeHistory.DataSource = ArlTcHistory
        dtgStatusChangeHistory.DataBind()
    End Sub

    Sub dtgStatusChangeHistory_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemIndex <> -1 Then
            Dim objTcHistory As TransactionControlPKHistory = ArlTcHistory(e.Item.ItemIndex)
            If objTcHistory.StatusFrom <> -1 Then
                e.Item.Cells(2).Text = CType(objTcHistory.StatusFrom, EnumDealerStatus.DealerStatus).ToString()
            End If
            e.Item.Cells(3).Text = CType(objTcHistory.StatusTo, EnumDealerStatus.DealerStatus).ToString()
            e.Item.Cells(5).Text = UserInfo.Convert(ArlTcHistory(e.Item.ItemIndex).CreatedBy)
        End If
    End Sub
End Class
