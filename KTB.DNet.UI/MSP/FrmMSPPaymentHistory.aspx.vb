Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic

Public Class FrmMSPPaymentHistory
    Inherits System.Web.UI.Page

    Private _sessHelper As New SessionHelper
    Private _strSessMSPTransferPaymentID As String = "MSPTrfPaymentID"
    Private objMSPTransferPayment As New MSPTransferPayment
    Private objMSPTransferPaymentHistory As New MSPTransferPaymentHistory
    Private arr As New ArrayList
    Dim crt As CriteriaComposite

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FillForm()
        End If
    End Sub

    Private Sub FillForm()
        Dim mspTrfPaymentID As Integer = _sessHelper.GetSession(_strSessMSPTransferPaymentID)
        crt = New CriteriaComposite(New Criteria(GetType(MSPTransferPaymentHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt.opAnd(New Criteria(GetType(MSPTransferPaymentHistory), "MSPTransferPayment.ID", MatchType.Exact, mspTrfPaymentID))
        arr = New MSPTransferPaymentHistoryFacade(User).Retrieve(crt)
        lblPaymentRegNumber.Text = CType(arr(0), MSPTransferPaymentHistory).MSPTransferPayment.RegNumber
        lblCreditAccount.Text = CType(arr(0), MSPTransferPaymentHistory).MSPTransferPayment.Dealer.CreditAccount

        ' bind data grid
        dtgMSPTrfHistory.DataSource = arr
        dtgMSPTrfHistory.DataBind()

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmMSPPaymentList.aspx")
    End Sub

    Private Sub dtgMSPTrfHistory_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPTrfHistory.ItemDataBound
        ' set lblNo
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1
        End If

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim rowValue As MSPTransferPaymentHistory = CType(e.Item.DataItem, MSPTransferPaymentHistory)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then
               
                ' lblTrasferDate
                Dim lblTrasferDate As Label = CType(e.Item.FindControl("lblTrasferDate"), Label)
                If Not IsNothing(lblTrasferDate) Then
                    lblTrasferDate.Text = rowValue.TransferDate.ToString("dd/MM/yyyy")
                End If

                ' lblAmount
                Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
                If Not IsNothing(lblAmount) Then
                    lblAmount.Text = rowValue.Amount.ToString("C")
                End If

            End If
        End If
    End Sub


End Class