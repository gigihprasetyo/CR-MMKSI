Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General

Public Class PopUpChangeStatusHistoryMSPRegistration
    Inherits System.Web.UI.Page

    Private arrListStatusChangeHistory As ArrayList

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            BindHeader()
            RetrieveData()
        End If
    End Sub

    Private Sub BindHeader()
        Dim objMSPRegHistory As MSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CType(Request.QueryString("DocNumber"), Integer))
        lblMSPType.Text = objMSPRegHistory.MSPMaster.MSPType.Description
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
            If arrListStatusChangeHistory(e.Item.ItemIndex).OldStatus <> -1 Then
                e.Item.Cells(2).Text = CType(arrListStatusChangeHistory(e.Item.ItemIndex).OldStatus, EnumStatusMSP.Status).ToString
            Else
                e.Item.Cells(2).Text = ""
            End If
            e.Item.Cells(3).Text = CType(arrListStatusChangeHistory(e.Item.ItemIndex).NewStatus, EnumStatusMSP.Status).ToString
            e.Item.Cells(5).Text = UserInfo.Convert(arrListStatusChangeHistory(e.Item.ItemIndex).CreatedBy).Replace("Invalid Dealer-", "")
        End If
    End Sub

End Class