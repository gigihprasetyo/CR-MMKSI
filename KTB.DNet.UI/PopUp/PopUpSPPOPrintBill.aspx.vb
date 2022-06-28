Imports Ktb.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports Ktb.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text
Imports System.Web.UI.WebControls

Public Class PopUpSPPOPrintBill
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtNoBill As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblTerbilang As System.Web.UI.WebControls.Label
    Protected WithEvents lblPoNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblCityDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblJumlah As System.Web.UI.WebControls.Label
    Protected WithEvents btnCetak As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If IsNothing(Request.QueryString("id")) Then Return

        Dim sppof As SparePartPOFacade = New SparePartPOFacade(User)
        Dim objsppo As SparePartPO = sppof.Retrieve(CInt(Request.QueryString("id")))
        If (IsNothing(objsppo)) Then Return

        lblDealerCode.Text = objsppo.Dealer.DealerCode
        lblJumlah.Text = objsppo.ItemAmount
        lblCityDate.Text = String.Format("{0}, {1}", objsppo.Dealer.City, DateTime.Now)
        lblPoNo.Text = objsppo.PONumber
        lblTerbilang.Text = New CommonFunction().Terbilang(CType(objsppo.ItemAmount, Double))
    End Sub

End Class
