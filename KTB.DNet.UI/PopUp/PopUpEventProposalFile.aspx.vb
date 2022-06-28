#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.WebCC
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region


Public Class PopUpEventProposalFile
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventName As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventDate As System.Web.UI.WebControls.Label
    Protected WithEvents dtgEventFile As System.Web.UI.WebControls.DataGrid
    Protected WithEvents img As System.Web.UI.WebControls.Image
    Protected WithEvents lblImg As System.Web.UI.WebControls.Label
    Protected WithEvents btnPrint As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private epf As EventProposalFacade
    Private eff As EventProposalFileFacade
    Private objDealer As Dealer
    Private objEp As EventProposal
    Dim _sesshelper As New SessionHelper
    Dim EPS As String = "EventProposalSessionFile"
    Dim DEALER As String = "DEALER"

#Region "function"

    Private Sub proposalEventDataToUI()
        If (IsNothing(Request.QueryString("id"))) Then
            MessageBox.Show("DEV ERR: id query string for EventProposalID is null")
            Return
        End If

        objEp = epf.Retrieve(CInt(Request.QueryString("id")))
        If (IsNothing(objEp)) Then
            MessageBox.Show("DEV ERR: EventProposal is null for ID=" & Request.QueryString("id"))
            Return
        End If

        _sesshelper.SetSession(EPS, objEp)
        lblTitle.Text = String.Format("{0} - Dokumentasi", objEp.EventParameter.ActivityType.ActivityName)
        lblEventName.Text = objEp.EventParameter.EventName
        lblEventDate.Text = objEp.ActivitySchedule.ToString("dd MMM yyyy")
        lblDealerCode.Text = objEp.Dealer.DealerCode
        lblDealerName.Text = objEp.Dealer.DealerName
        bindGrid()
    End Sub

    Private Sub bindGrid()
        Dim arl As ArrayList = New ArrayList

        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            arl = eff.RetriveEventProposalFiles(CInt(Request.QueryString("id")), False, EventProposalFile.EnumContentType.Foto)
        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            arl = eff.RetriveEventProposalFiles(CInt(Request.QueryString("id")), True, EventProposalFile.EnumContentType.Foto)
        End If

        dtgEventFile.DataSource = arl
        dtgEventFile.DataBind()
    End Sub

#End Region

#Region "event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        epf = New EventProposalFacade(User)
        eff = New EventProposalFileFacade(User)
        objDealer = _sesshelper.GetSession("DEALER")

        If (IsPostBack) Then
            Return
        End If

        proposalEventDataToUI()

    End Sub

    Private Sub dtgEventFile_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEventFile.ItemCommand
        If (e.CommandName = "ViewImage") Then
            Dim obj As EventProposalFile = eff.Retrieve(CInt(e.CommandArgument))

            Dim szFileName As String = KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory") & "\" & obj.FileName
            img.ImageUrl = String.Format("../Event/EventImageHandler.aspx?file={0}", szFileName)
            img.Visible = True
            dtgEventFile.SelectedIndex = e.Item.ItemIndex
            lblImg.Text = obj.FileName
            btnPrint.Attributes.Item("onclick") = "showPopUp('../Event/ImageViewer.aspx?file=" & szFileName.Replace("\", "|") & "','',550,650,'');"
        End If
    End Sub

    Private Sub dtgEventFile_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEventFile.SortCommand
        If CType(_sesshelper.GetSession("SortCol"), String) = e.SortExpression Then
            Select Case CType(_sesshelper.GetSession("SortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    _sesshelper.SetSession("SortDirect", Sort.SortDirection.DESC)

                Case Sort.SortDirection.DESC
                    _sesshelper.SetSession("SortDirect", Sort.SortDirection.ASC)
            End Select
        Else
            _sesshelper.SetSession("SortCol", e.SortExpression)
            _sesshelper.SetSession("SortDirect", Sort.SortDirection.ASC)
        End If
        bindGrid()
    End Sub

#End Region

End Class