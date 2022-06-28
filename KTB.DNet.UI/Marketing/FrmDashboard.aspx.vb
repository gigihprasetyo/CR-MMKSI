#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper


#End Region
Public Class FrmDashboard
    Inherits System.Web.UI.Page

    Private sessionHelper As New SessionHelper

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        Dim configUrl As AppConfig = New AppConfigFacade(User).Retrieve("DashboardUrl")
        Dim configUrlProperties As AppConfig = New AppConfigFacade(User).Retrieve("DashboardUrlProperties")
        If Not IsNothing(configUrl) AndAlso configUrl.ID > 0 Then
            If Not IsNothing(objDealer) AndAlso objDealer.ID > 0 Then
                Dim url As String = configUrl.Value.Trim & objDealer.ID
                If Not IsNothing(configUrlProperties) AndAlso configUrlProperties.ID > 0 Then
                    Response.Write("<script language='javascript'> window.open('" + url + "', 'window',' " + configUrlProperties.Value.Trim + "');</script>")
                Else
                    Response.Write("<script language='javascript'> window.open('" + url + "', 'window','HEIGHT=600,WIDTH=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>")
                End If
            End If
        Else
            MessageBox.Show("Konfigurasi Url belum diisi. Mohon hubungi admin DNET.")
        End If
    End Sub

End Class