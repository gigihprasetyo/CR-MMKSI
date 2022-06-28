Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart

Public Class PopUpTOPSPPenaltyTransferActual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
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

                Dim Crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Crit.opAnd(New Criteria(GetType(TOPSPTransferPaymentDetail), "TOPSPTransferPayment.ID", MatchType.Exact, paymentID))
                Dim arlTOPSPTransferPaymentDetail As ArrayList = New TOPSPTransferPaymentDetailFacade(User).Retrieve(Crit)
                Dim arlTOPSPTransferPaymentPenalty As New ArrayList
                If Not IsNothing(arlTOPSPTransferPaymentDetail) AndAlso arlTOPSPTransferPaymentDetail.Count > 0 Then
                    For Each objPaymentDtl As TOPSPTransferPaymentDetail In arlTOPSPTransferPaymentDetail
                        If Not IsNothing(objPaymentDtl.TOPSPPenaltyDetails) AndAlso objPaymentDtl.TOPSPPenaltyDetails.Count > 0 Then
                            For Each objTOPSPDtl As TOPSPPenaltyDetail In objPaymentDtl.TOPSPPenaltyDetails
                                Dim oV_TOPSPTransferPaymentDetail As New V_TOPSPTransferPaymentDetail
                                oV_TOPSPTransferPaymentDetail.DealerCode = objPaymentDtl.TOPSPTransferPayment.Dealer.DealerCode
                                oV_TOPSPTransferPaymentDetail.BillingNumber = objPaymentDtl.SparePartBilling.BillingNumber
                                oV_TOPSPTransferPaymentDetail.DueDate = objPaymentDtl.TOPSPTransferPayment.DueDate
                                oV_TOPSPTransferPaymentDetail.Amount = objPaymentDtl.Amount
                                oV_TOPSPTransferPaymentDetail.ActualTransferDate = objTOPSPDtl.ActualTransferDate
                                oV_TOPSPTransferPaymentDetail.ActualTransferAmount = objTOPSPDtl.ActualTransferAmount
                                oV_TOPSPTransferPaymentDetail.PenaltyDays = objTOPSPDtl.PenaltyDays
                                oV_TOPSPTransferPaymentDetail.AmountPenalty = objTOPSPDtl.AmountPenalty
                                oV_TOPSPTransferPaymentDetail.PaymentType = objTOPSPDtl.PaymentType
                                arlTOPSPTransferPaymentPenalty.Add(oV_TOPSPTransferPaymentDetail)
                            Next
                        Else
                            Dim oV_TOPSPTransferPaymentDetail As New V_TOPSPTransferPaymentDetail
                            oV_TOPSPTransferPaymentDetail.DealerCode = objPaymentDtl.TOPSPTransferPayment.Dealer.DealerCode
                            oV_TOPSPTransferPaymentDetail.BillingNumber = objPaymentDtl.SparePartBilling.BillingNumber
                            oV_TOPSPTransferPaymentDetail.DueDate = objPaymentDtl.TOPSPTransferPayment.DueDate
                            oV_TOPSPTransferPaymentDetail.Amount = objPaymentDtl.Amount

                            paymentID = 0
                            Dim oTOPSPTransferActual As New TOPSPTransferActual
                            If Not IsNothing(Request.QueryString("TransferPaymentID")) Then
                                paymentID = CInt(Request.QueryString("TransferPaymentID"))
                            End If
                            If paymentID > 0 Then
                                Dim crite As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                actualCrit.opAnd(New Criteria(GetType(TOPSPTransferActual), "TOPSPTransferPayment.ID", MatchType.Exact, paymentID))
                                Dim sortColl As SortCollection = New SortCollection
                                sortColl.Add(New Sort(GetType(TOPSPTransferActual), "PostingDate", Sort.SortDirection.DESC))
                                Dim arrTopActual As ArrayList = New TOPSPTransferActualFacade(User).Retrieve(crite, sortColl)
                                If Not IsNothing(arrTopActual) AndAlso arrTopActual.Count > 0 Then
                                    oTOPSPTransferActual = CType(arrTopActual(0), TOPSPTransferActual)
                                    oV_TOPSPTransferPaymentDetail.ActualTransferDate = oTOPSPTransferActual.PostingDate.ToString("dd/MM/yyyy")
                                End If
                            End If
                            oV_TOPSPTransferPaymentDetail.ActualTransferAmount = Format(objPaymentDtl.Amount, "#,##0")
                            oV_TOPSPTransferPaymentDetail.PaymentType = 1  'Type VA
                            oV_TOPSPTransferPaymentDetail.PenaltyDays = 0
                            oV_TOPSPTransferPaymentDetail.AmountPenalty = 0
                            arlTOPSPTransferPaymentPenalty.Add(oV_TOPSPTransferPaymentDetail)
                        End If
                    Next
                End If
                dgDetailBilling.DataSource = arlTOPSPTransferPaymentPenalty
                dgDetailBilling.DataBind()
            End If
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

    Protected Sub dgDetailBilling_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oVTOPPaymentDetail As V_TOPSPTransferPaymentDetail = CType(e.Item.DataItem, V_TOPSPTransferPaymentDetail)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblBillingNumber As Label = CType(e.Item.FindControl("lblBillingNumber"), Label)
            Dim lblDueDate As Label = CType(e.Item.FindControl("lblDueDate"), Label)
            Dim lblTotalAmount As Label = CType(e.Item.FindControl("lblTotalAmount"), Label)
            Dim lblActualTransferDate As Label = CType(e.Item.FindControl("lblActualTransferDate"), Label)
            Dim lblActualTransferAmount As Label = CType(e.Item.FindControl("lblActualTransferAmount"), Label)
            Dim lblPaymentType As Label = CType(e.Item.FindControl("lblPaymentType"), Label)
            Dim lblPenaltyDays As Label = CType(e.Item.FindControl("lblPenaltyDays"), Label)
            Dim lblAmountPenalty As Label = CType(e.Item.FindControl("lblAmountPenalty"), Label)

            lblNo.Text = e.Item.ItemIndex + 1
            lblDealerCode.Text = oVTOPPaymentDetail.DealerCode
            lblBillingNumber.Text = oVTOPPaymentDetail.BillingNumber
            lblDueDate.Text = oVTOPPaymentDetail.DueDate.ToString("dd/MM/yyyy")
            lblTotalAmount.Text = Format(oVTOPPaymentDetail.Amount, "#,##0")
            lblActualTransferDate.Text = oVTOPPaymentDetail.ActualTransferDate.ToString("dd/MM/yyyy")
            lblActualTransferAmount.Text = Format(oVTOPPaymentDetail.ActualTransferAmount, "#,##0")
            Dim arlStandardCode As ArrayList = New StandardCodeFacade(User).RetrieveByValueId(oVTOPPaymentDetail.PaymentType.ToString(), "EnumTOPSPPenalty.TipePembayaran")
            If Not IsNothing(arlStandardCode) AndAlso arlStandardCode.Count > 0 Then
                Dim objStandardCode As StandardCode = CType(arlStandardCode(0), StandardCode)
                lblPaymentType.Text = objStandardCode.ValueDesc
            End If
            lblPenaltyDays.Text = oVTOPPaymentDetail.PenaltyDays
            lblAmountPenalty.Text = Format(oVTOPPaymentDetail.AmountPenalty, "#,##0")
        End If
    End Sub
End Class