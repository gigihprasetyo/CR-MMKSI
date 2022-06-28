#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Web.UI.WebControls
#End Region

Public Class FrmListDocument
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtEventNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchEvent As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents dgListDocument As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtJadwal As System.Web.UI.WebControls.TextBox
    Protected WithEvents ICDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICSampai As KTB.DNet.WebCC.IntiCalendar

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private _sessHelper As New SessionHelper
    Private oDealer As Dealer
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            txtDealerCode.ReadOnly = True
        Else
            txtDealerCode.ReadOnly = False
        End If

        If Not IsPostBack() Then
            ViewState("CurrentSortColumn") = "DocumentCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            _sessHelper.SetSession("BINDSTATUS", "NOTHING")
            BindData(0)
        End If
        lblSearchDealer.Attributes("onclick") = "ShowDealerSelection();"
        lblSearchEvent.Attributes("onclick") = "ShowEventSelection();"
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If ICSampai.Value < ICDari.Value Then
            MessageBox.Show("Tanggal dari tidak boleh lebih kecil dari tanggal sampai.")
            Return
        End If
        BindResult(0)
        _sessHelper.SetSession("BINDSTATUS", "CARI")
    End Sub
    Private Sub BindData(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        arrList = New EventDocumentFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgListDocument.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgListDocument.DataSource = arrList
        dgListDocument.VirtualItemCount = totalRow
        dgListDocument.DataBind()

    End Sub
    Private Sub BindResult(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtEventNumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(EventDocument), "EventMaster.EventNo", MatchType.Exact, txtEventNumber.Text.Trim))
        End If

        If txtDealerCode.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(EventDocument), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
        End If

        If txtJadwal.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(EventDocument), "EventMaster.Period", MatchType.Exact, Integer.Parse(txtJadwal.Text.Trim)))
        End If

        criterias.opAnd(New Criteria(GetType(EventDocument), "CreatedTime", MatchType.GreaterOrEqual, ICDari.Value))
        criterias.opAnd(New Criteria(GetType(EventDocument), "CreatedTime", MatchType.LesserOrEqual, ICSampai.Value.AddDays(1)))

        Dim arrList As ArrayList = New EventDocumentFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgListDocument.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgListDocument.DataSource = arrList
        dgListDocument.VirtualItemCount = totalRow
        dgListDocument.DataBind()
        _sessHelper.SetSession("SortViewVC", criterias)
    End Sub
    Private Sub dgListDocument_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgListDocument.PageIndexChanged
        dgListDocument.CurrentPageIndex = e.NewPageIndex
        If CType(_sessHelper.GetSession("BINDSTATUS"), String) = "CARI" Then
            BindResult(dgListDocument.CurrentPageIndex)
        Else
            BindData(dgListDocument.CurrentPageIndex)
        End If
        ClearData()
    End Sub
    Private Sub ClearData()
        txtDealerCode.Text = String.Empty
        txtEventNumber.Text = String.Empty
        txtJadwal.Text = String.Empty
    End Sub
    Private Sub dgListDocument_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgListDocument.ItemCommand
        Select Case e.CommandName
            Case "Download"
                UpdateLastDownload(Integer.Parse(e.Item.Cells(0).Text))
        End Select
    End Sub
    Private Sub UpdateLastDownload(ByVal id As Integer)
        Dim obj As New EventDocument
        obj = New EventDocumentFacade(User).Retrieve(id)
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("EventDir") & "\Dokumen\" & obj.EventMaster.EventNo & "-" & obj.Dealer.DealerCode & "\" & obj.DocumentFile  '-- Destination file
        Response.Redirect("../Download.aspx?file=" & DestFile)

    End Sub
    Private Sub dgListDocument_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgListDocument.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgListDocument.SelectedIndex = -1
        dgListDocument.CurrentPageIndex = 0
        bindGridSorting(dgListDocument.CurrentPageIndex)
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            If CType(_sessHelper.GetSession("BINDSTATUS"), String) = "CARI" Then
                dgListDocument.DataSource = New EventDocumentFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("sortViewVC"), CriteriaComposite), indexPage + 1, dgListDocument.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            Else
                Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                dgListDocument.DataSource = New EventDocumentFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgListDocument.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            End If
            dgListDocument.VirtualItemCount = totalRow
            dgListDocument.DataBind()
        End If

    End Sub

    Private Sub dgListDocument_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgListDocument.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim obj As EventDocument = CType(e.Item.DataItem, EventDocument)
            Dim lblPeriode As Label = CType(e.Item.FindControl("lblPeriode"), Label)
            'remarked by ery
            'lblPeriode.Text = CType(obj.EventMaster.StartMonth, enumMonth.Month).ToString & " " & obj.EventMaster.Period & " s.d " & CType(obj.EventMaster.EndMonth, enumMonth.Month).ToString & " " & obj.EventMaster.Period
            lblPeriode.Text = New enumMonthGet().GetName(obj.EventMaster.StartMonth) & " " & obj.EventMaster.Period & " s.d " & New enumMonthGet().GetName(obj.EventMaster.EndMonth) & " " & obj.EventMaster.Period
        End If
    End Sub
End Class
