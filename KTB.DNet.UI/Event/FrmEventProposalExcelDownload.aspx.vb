#Region " Custom Namespace Imports "
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.BusinessFacade.FinishUnit
#End Region

#Region " .NET Base Class Namespace Imports "

#End Region

Public Class FrmEventProposalExcelDownload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents dtgExcel As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgAgreement As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSubTitle As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Constants "

#End Region

#Region " Private Variables "
    Private ReadOnly Property IsUserDealer() As Boolean
        Get
            Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")
            Return Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB
        End Get
    End Property
    Private ReadOnly Property Mode() As String
        Get
            Return Request.QueryString("Mode")
        End Get
    End Property
    Private ReadOnly Property IdColl() As String
        Get
            Return Request.QueryString("idin")
        End Get
    End Property
    Private ReadOnly Property NamaKegiatan() As String
        Get
            Return Request.QueryString("NameKegiatan")
        End Get
    End Property
#End Region

#Region " Custom Method "
    Private Sub BindGrid()
        Select Case Mode
            Case "Excel"
                dtgAgreement.Visible = False
                BindGridExcel()
            Case "Agreement"
                dtgExcel.Visible = False
                lblSubTitle.Text = NamaKegiatan
                lblTitle.Text = "Nama Kegiatan"
                BindGridAgreement()
        End Select
    End Sub
    Private Sub BindGridExcel()
        Dim objFacade As New EventProposalExcelFacade(User)
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_EventProposalExcel), "EventProposalID", MatchType.InSet, IdColl))
        Dim sortCol As New SortCollection
        sortCol.Add(New Sort(GetType(V_EventProposalExcel), "EventProposalID"))
        dtgExcel.DataSource = objFacade.Retrieve(crits, sortCol)
        dtgExcel.DataBind()
    End Sub
    Private Sub BindGridAgreement()
        Dim objFacade As New EventProposalFacade(User)
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_EventProposalAgreement), "ID", MatchType.InSet, IdColl))
        Dim sortCol As New SortCollection
        sortCol.Add(New Sort(GetType(V_EventProposalAgreement), "ID"))
        dtgAgreement.DataSource = objFacade.RetrieveAgreement(crits, sortCol)
        dtgAgreement.DataBind()
    End Sub
#End Region

#Region " Event Handler "
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BindGrid()
        Response.ContentType = "application/x-download"
        Response.AddHeader("Content-Disposition", "attachment;filename=""EventProposal.xls""")
    End Sub
    Private Sub dtgExcel_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles _
        dtgExcel.ItemDataBound, dtgAgreement.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header AndAlso e.Item.ItemType <> ListItemType.Footer Then
            e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dtgExcel.PageSize * dtgExcel.CurrentPageIndex)).ToString
        End If
    End Sub
#End Region

End Class