Imports System.IO
Imports System.Threading
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.Globalization
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade

Public Class FrmMonthlyDocumentActualTransfer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim id As String = Request.QueryString("IDmonth")
            hdID.Value = id
            LoadData(id)
        End If
    End Sub

    Private Sub LoadData(ByVal id As Integer)
        Dim objHeader As MonthlyDocument = New MonthlyDocumentFacade(User).Retrieve(id)
        If Not IsNothing(objHeader) AndAlso objHeader.ID.ToString() <> "" Then
            lblDealerCode.Text = objHeader.Dealer.DealerCode + " - " + objHeader.Dealer.DealerName
            lblNoAccounting.Text = objHeader.AccountingNo
            lblTglTransfer.Text = objHeader.TransferDate
            lblBank.Text = objHeader.NameofBank
            lblNoRekening.Text = objHeader.AccountNumberBank
            lblTotalTransfer.Text = objHeader.AmountTransfer
        End If

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("../Service/frmMonthlyDocument.aspx?")
    End Sub
End Class