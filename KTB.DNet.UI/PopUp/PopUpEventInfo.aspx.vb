Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Event

Public Class PopUpEventInfo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtRequestEventNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTypeEvent As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtLokasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgEventInfo As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnCancel As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents icEventDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEventDateFrom As KTB.DNet.WebCC.IntiCalendar

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Method"
    Private Sub BindEventType()
        ddlTypeEvent.Items.Clear()
        ddlTypeEvent.DataSource = New EventTypeFacade(User).RetrieveActiveList()
        ddlTypeEvent.DataTextField = "Description"
        ddlTypeEvent.DataValueField = "ID"
        ddlTypeEvent.DataBind()
        ddlTypeEvent.Items.Insert(0, New ListItem("Pilih Semua", -1))
    End Sub
    Private Sub BindStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add("Pilih Semua")
        ddlStatus.Items.Add("Baru")
        ddlStatus.Items.Add("Konfirmasi")
        ddlStatus.Items.Add("Realisasi")
    End Sub
    Private Sub ClearData()
        Me.txtRequestEventNo.Text = String.Empty
        Me.txtLokasi.Text = String.Empty
        ddlTypeEvent.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
    End Sub
    Private Sub BindGrid(ByVal PageIndex As Integer)
        Dim totalRow As Integer = 0
        dgEventInfo.DataSource = New EventInfoFacade(User).RetrieveByCriteria(CriteriaSearch(), PageIndex + 1, _
            dgEventInfo.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgEventInfo.VirtualItemCount = totalRow
        dgEventInfo.DataBind()

        If dgEventInfo.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub
    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(EventInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtRequestEventNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(EventInfo), "EventRequestNo", MatchType.[Partial], txtRequestEventNo.Text))
        End If

        If txtLokasi.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(EventInfo), "Location", MatchType.[Partial], txtLokasi.Text))
        End If

        If ddlTypeEvent.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(EventInfo), "EventType.ID", MatchType.Exact, ddlTypeEvent.SelectedValue))
        End If

        Select Case ddlStatus.SelectedIndex
            Case 1
                criterias.opAnd(New Criteria(GetType(EventInfo), "IsConfirmed", MatchType.Exact, 0))
                criterias.opAnd(New Criteria(GetType(EventInfo), "IsRealization", MatchType.Exact, 0))
            Case 2
                criterias.opAnd(New Criteria(GetType(EventInfo), "IsConfirmed", MatchType.Exact, 1))
            Case 3
                criterias.opAnd(New Criteria(GetType(EventInfo), "IsRealization", MatchType.Exact, 1))
        End Select

        criterias.opAnd(New Criteria(GetType(EventInfo), "DateStart", MatchType.GreaterOrEqual, icEventDateFrom.Value))
        criterias.opAnd(New Criteria(GetType(EventInfo), "DateEnd", MatchType.Lesser, icEventDateFrom.Value.AddDays(1)))

        Return criterias
    End Function
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            ClearData()
            BindEventType()
            BindStatus()
            ViewState("CurrentSortColumn") = "EventRequestNo"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            BindGrid(0)
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindGrid(0)
    End Sub
    Private Sub dgEventInfo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEventInfo.ItemDataBound
        If e.Item.ItemIndex >= 0 Then

            Dim RowValue As EventInfo = CType(e.Item.DataItem, EventInfo)

            Dim lblEventType As Label = CType(e.Item.FindControl("lblEventType"), Label)
            lblEventType.Text = RowValue.EventType.Description


            Dim chkConfirmed As CheckBox = CType(e.Item.FindControl("chkConfirm"), CheckBox)
            Dim chkReal As CheckBox = CType(e.Item.FindControl("chkReal"), CheckBox)

            If RowValue.IsConfirmed = 0 Then
                chkConfirmed.Checked = False
            ElseIf RowValue.IsConfirmed = 1 Then
                chkConfirmed.Checked = True
            End If

            If RowValue.IsRealization = 0 Then
                chkReal.Checked = False
            ElseIf RowValue.IsRealization = 1 Then
                chkReal.Checked = True
            End If

            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelection"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If
    End Sub
    Private Sub dgEventInfo_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgEventInfo.PageIndexChanged
        dgEventInfo.CurrentPageIndex = e.NewPageIndex
        BindGrid(dgEventInfo.CurrentPageIndex)
    End Sub
    Private Sub dgEventInfo_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgEventInfo.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        dgEventInfo.CurrentPageIndex = 0
        BindGrid(dgEventInfo.CurrentPageIndex)
    End Sub
#End Region

End Class
