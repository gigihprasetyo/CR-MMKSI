#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Transfer
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Parser
Imports System.Text
#End Region



Public Class frmPopUpTransferActual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Not IsNothing(Page.Request.QueryString("TransferPaymentID")) AndAlso Page.Request.QueryString("TransferPaymentID").ToString() <> "" Then
                Dim pay As New TransferPayment

                pay = New TransferPaymentFacade(User).Retrieve(CInt(Page.Request.QueryString("TransferPaymentID")))
                lblDeaelerCode.Text = pay.Dealer.DealerCode
                lblNamaDealer.Text = pay.Dealer.DealerName
                lblNoreg.Text = pay.RegNumber
                lblTOtalAmount.Text = pay.TransferAmount.ToString("#,##0")
                lblTotalTransfer.Text = pay.TotalActualAmount.ToString("#,##0")
                lblPaymentPurspose.Text = pay.PaymentPurpose.PaymentPurposeCode

                Dim oTPFac As New TransferActualFacade(User)
                Dim aTPs As New ArrayList
                Dim cTP As New CriteriaComposite(New Criteria(GetType(TransferActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim sTP As New SortCollection()

                sTP.Add(New Sort(GetType(TransferActual), "PostingDate", Sort.SortDirection.DESC))
            
                cTP.opAnd(New Criteria(GetType(TransferActual), "TransferPayment.ID", MatchType.Exact, Page.Request.QueryString("TransferPaymentID")))

                aTPs = oTPFac.Retrieve(cTP, sTP)

                If Not IsNothing(aTPs) AndAlso aTPs.Count > 0 Then
                    dtgCustomerSelection.DataSource = aTPs
                    dtgCustomerSelection.DataBind()

                Else
                    dtgCustomerSelection.DataSource = New ArrayList
                    dtgCustomerSelection.DataBind()

                End If
             

            End If
        End If
    End Sub

    Private Sub dtgCustomerSelection_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgCustomerSelection.ItemDataBound

        If Not IsNothing(e.Item.DataItem) Then
            Dim obTA As New TransferActual

            obTA = e.Item.DataItem
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            lblNo.Text = (e.Item.ItemIndex + 1).ToString()

            Dim lblRefbank As Label = e.Item.FindControl("lblRefbank")
            lblRefbank.Text = obTA.RefTransferbank


            Dim lblPostingDate As Label = e.Item.FindControl("lblPostingDate")
            lblPostingDate.Text = obTA.PostingDate.ToString("dd/MM/yyyy")



            Dim lblAmount As Label = e.Item.FindControl("lblAmount")
            lblAmount.Text = obTA.Amount.ToString("#,##0")
        End If
    End Sub
End Class