Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade

Public Class PopUpChangeStatusHistoryPKT
    Inherits System.Web.UI.Page

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
        lblJenisDokumenValue.Text = CType(Request.QueryString("DocType"), LookUp.DocumentType).ToString & " (Penyerahan Kendaraan Terpadu)"
        lblNoRegDokumenValue.Text = Request.QueryString("Chassis")
    End Sub

    Private Sub RetrieveData()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKTChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKTChangeHistory), "DocType", MatchType.Exact, CInt(Request.QueryString("DocType"))))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKTChangeHistory), "DocNumber", MatchType.Exact, lblNoRegDokumenValue.Text))
        arrListStatusChangeHistory = New PKTChangeHistoryFacade(User).Retrieve(criterias)
        BindToGrid()
    End Sub

    Private Sub BindToGrid()
        dtgStatusChangeHistory.DataSource = arrListStatusChangeHistory
        dtgStatusChangeHistory.DataBind()
    End Sub

    Sub dtgStatusChangeHistory_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        'If e.Item.ItemIndex <> -1 Then
        '    If CInt(Request.QueryString("DocType")) = LookUp.DocumentType.PKT Then
        '        If arrListStatusChangeHistory(e.Item.ItemIndex).OldStatus <> -1 Then
        '            e.Item.Cells(2).Text = CType(arrListStatusChangeHistory(e.Item.ItemIndex).OldStatus, EnumStatusSPK.Status).ToString
        '        Else
        '            e.Item.Cells(2).Text = ""
        '        End If
        '        e.Item.Cells(3).Text = CType(arrListStatusChangeHistory(e.Item.ItemIndex).NewStatus, EnumStatusSPK.Status).ToString
        '        e.Item.Cells(5).Text = UserInfo.Convert(arrListStatusChangeHistory(e.Item.ItemIndex).CreatedBy)
        '    End If
        'End If
    End Sub
End Class
