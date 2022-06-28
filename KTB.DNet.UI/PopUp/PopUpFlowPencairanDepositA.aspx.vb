Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class PopUpFlowPencairanDepositA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblItem As System.Web.UI.WebControls.Label

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        LoadFlowPencairan()
    End Sub

    Private Sub LoadFlowPencairan()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "Dealer.ID", MatchType.Exact, CInt(Request.QueryString("DealerID"))))
        Dim NoReg As String = Request.QueryString("NoReg")
        criterias.opAnd(New Criteria(GetType(DepositAPencairanH), "NoReg", MatchType.Exact, NoReg))
        Dim _arrList As New ArrayList
        _arrList = New DepositAPencairanHFacade(User).Retrieve(criterias)
        Dim objDepositAPencairanH As DepositAPencairanH = _arrList(0)
        Dim NoSurat As String = Request.QueryString("NoSurat")
        lblItem.Text = "Surat Pengajuan Pencairan -> " & NoSurat & " -> created date : " & Format(objDepositAPencairanH.CreatedTime, "MM/dd/yyyy") & " -> updated date : " & Format(objDepositAPencairanH.LastUpdateTime, "MM/dd/yyyy")

    End Sub
End Class
