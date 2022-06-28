Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Public Class PopUpConfirmationPengajuanFaktur
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
    Private sessHelper As New SessionHelper
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            sessHelper.SetSession("ADD", Nothing)
            Dim NoRangka As String = CType(Session("NoRangka"), String)
            lblPesan.Text = "Nomor rangka " & NoRangka & " sudah terdaftar untuk Kode Konsumen " & IsValidChassisMasterII(NoRangka) & ". Anda yakin ingin ganti kode konsumen?"
        End If
    End Sub
    Private Function IsValidChassisMasterII(ByVal ChassisNumber As String) As String
        Dim CM As New ChassisMaster
        CM = New FinishUnit.ChassisMasterFacade(User).Retrieve(ChassisNumber)
        Dim CMs As ArrayList = New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.Exact, CType(EnumChassisMaster.FakturStatus.Baru, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, ChassisNumber))
        CMs = New FinishUnit.ChassisMasterFacade(User).Retrieve(criterias)
        If (CMs.Count > 0) Then
            Return CType(CMs(0), ChassisMaster).EndCustomer.Customer.Code()
        End If
        Return String.Empty
    End Function

    Private Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        sessHelper.SetSession("ADD", True)
        'sessHelper.SetSession("MODE", "update")
        Response.Redirect(CType(Session("PrevPage"), String))
    End Sub

    Private Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Response.Redirect(CType(Session("PrevPage"), String))
    End Sub
End Class
