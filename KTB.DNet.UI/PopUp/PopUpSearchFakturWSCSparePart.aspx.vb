Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility

Public Class PopUpSearchFakturWSCSparePart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fakturNumber As String = Request.QueryString("FakturNumber")
        Dim dealerCode As String = Request.QueryString("DealerCode")
        Dim wscDetailFacade As WSCDetailFacade = New WSCDetailFacade(User)
        Dim ds As DataSet = wscDetailFacade.RetrieveSP(fakturNumber, dealerCode)
        Dim dt1 As DataTable
        If Not IsNothing(ds) AndAlso ds.Tables.Count >= 1 Then
            dt1 = ds.Tables(0)
            dtgParts.DataSource = dt1
            dtgParts.DataBind()
        Else
            MessageBox.Show("Error Pada Faktur Data.")
        End If

    End Sub

End Class