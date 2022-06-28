Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper

Public Class FrmRevisionPriceDetail
    Inherits System.Web.UI.Page

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Revisis_Faktur_Master_Revision_Price_Lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Revisi Faktur - Master Revision Price")
        End If

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then
            ViewModel(Request.QueryString!id)
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Short)
        Dim objDomain As RevisionPrice = New RevisionPriceFacade(User).Retrieve(nID)
        'Todo session
        If Not IsNothing(objDomain) Then
            lblCategory.Text = objDomain.Category.CategoryCode
            lblRevisi.Text = objDomain.RevisionType.Description
            lblAmount.Text = String.Format("{0:N0}", objDomain.Amount)
            lblValid.Text = objDomain.ValidFrom.ToString("dd/MM/yyyy")
        End If
    End Sub
End Class