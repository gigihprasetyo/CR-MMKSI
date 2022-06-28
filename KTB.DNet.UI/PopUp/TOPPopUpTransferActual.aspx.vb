Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart

Public Class TOPPopUpTransferActual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim paymentID As Integer = 0
        If Not IsNothing(Request.QueryString("TransferPaymentID")) Then
            paymentID = CInt(Request.QueryString("TransferPaymentID"))
        End If
        If paymentID <> 0 Then
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.ID", MatchType.Exact, paymentID))
            Dim oTOPDetail As ArrayList = New TOPSPTransferPaymentDetailFacade(User).Retrieve(criteria)
            If oTOPDetail.Count > 0 Then
                Dim paymentDetail As TOPSPTransferPaymentDetail = oTOPDetail(0)
                lblDeaelerCode.Text = paymentDetail.TOPSPTransferPayment.Dealer.DealerCode
                lblNamaDealer.Text = paymentDetail.TOPSPTransferPayment.Dealer.DealerName
                lblNoreg.Text = paymentDetail.TOPSPTransferPayment.RegNumber
                Dim total As Double = 0
                For Each detail As TOPSPTransferPaymentDetail In oTOPDetail
                    total = total + detail.Amount
                Next
                lblTOtalAmount.Text = total.ToString("N0")
                lblTotalTransfer.Text = CDbl(paymentDetail.TOPSPTransferPayment.TransferAmount).ToString("N0")

            End If
            Dim actualCrit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            actualCrit.opAnd(New Criteria(GetType(TOPSPTransferActual), "TOPSPTransferPayment.ID", MatchType.Exact, paymentID))
            Dim oTopActual As ArrayList = New TOPSPTransferActualFacade(User).Retrieve(actualCrit)
            dtgCustomerSelection.DataSource = oTopActual
            dtgCustomerSelection.DataBind()
        End If

    End Sub

    Protected Sub dtgCustomerSelection_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oTOP As TOPSPTransferActual = CType(e.Item.DataItem, TOPSPTransferActual)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblRefbank As Label = CType(e.Item.FindControl("lblRefbank"), Label)
            Dim lblPostingDate As Label = CType(e.Item.FindControl("lblPostingDate"), Label)
            Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
            lblNo.Text = e.Item.ItemIndex + 1
            lblRefbank.Text = oTOP.RefTransferBank
            lblPostingDate.Text = oTOP.PostingDate
            lblAmount.Text = oTOP.Amount.ToString("N0")
        End If
    End Sub
End Class