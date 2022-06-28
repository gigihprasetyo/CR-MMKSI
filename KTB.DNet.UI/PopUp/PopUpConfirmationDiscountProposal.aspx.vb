Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.enumMode
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class PopUpConfirmationDiscountProposal
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents btnYes As System.Web.UI.WebControls.Button
    Protected WithEvents btnNo As System.Web.UI.WebControls.Button
    Protected WithEvents lblPesan As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private returnValue As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strMsg As String = ""
        Dim strFCHID As String = Request.QueryString("fchID")
        Dim strDealerID As String = Request.QueryString("dealerID")
        If Not IsPostBack Then
            Dim objFleetCustomerDetail As New FleetCustomerDetail
            Dim criterias1 As New CriteriaComposite(New Criteria(GetType(FleetCustomerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias1.opAnd(New Criteria(GetType(FleetCustomerDetail), "Dealer.ID", MatchType.Exact, strDealerID))
            criterias1.opAnd(New Criteria(GetType(FleetCustomerDetail), "FleetCustomerHeader.ID", MatchType.Exact, strFCHID))
            Dim arlFleetCustomerDetail As ArrayList = New FleetCustomerDetailFacade(User).Retrieve(criterias1)
            If IsNothing(arlFleetCustomerDetail) Then arlFleetCustomerDetail = New ArrayList
            If Not IsNothing(arlFleetCustomerDetail) AndAlso arlFleetCustomerDetail.Count > 0 Then
                objFleetCustomerDetail = CommonFunction.SortListControl(arlFleetCustomerDetail, "CreatedTime", Sort.SortDirection.DESC)(0)
                strMsg = "Data Fleet sudah terdaftar di sistem dengan Nama: [" & objFleetCustomerDetail.FleetCustomerHeader.FleetCustomerName & "] dan NIB/NIK : [" & objFleetCustomerDetail.IdentityNumber & "]<br><br>" & _
                                   "Apakah tetap akan membuat data Fleet baru ?"
            End If
            lblPesan.Text = strMsg
        End If
    End Sub

    Private Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        returnValue = 1
        Response.Write("<script language='javascript'>window.returnValue='" + returnValue + "';window.close();</script>")
    End Sub

    Private Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        returnValue = 0
        Response.Write("<script language='javascript'>window.returnValue='" + returnValue + "';window.close()</script>")
    End Sub
End Class
