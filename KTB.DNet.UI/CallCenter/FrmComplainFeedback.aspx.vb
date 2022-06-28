#Region "DNet Namespace Imports"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmComplainFeedback
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnShow As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents dgFeedback As System.Web.UI.WebControls.DataGrid
    Protected WithEvents valDealer As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private declaration"
    Private _sessHelper As New SessionHelper
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Authorization()
        If Not IsPostBack Then
            lblSearchDealer.Attributes("onclick") = "ShowDealerSelection();"
            GetStatusList()
            Dim objDealer As Dealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtDealerCode.ReadOnly = True
            End If
            Dim crit As Hashtable
            crit = CType(Session("CriteriaFrmComplainFeedback"), Hashtable)
            ddlStatus.ClearSelection()
            If Not crit Is Nothing Then
                txtDealerCode.Text = CStr(crit.Item("DealerCode"))
                lblDealerName.Text = CStr(crit.Item("DealerName"))
                ddlStatus.SelectedValue = CInt(crit.Item("Status"))
                dgFeedback.CurrentPageIndex = CInt(crit.Item("PageIndex"))
                BindingData(dgFeedback.CurrentPageIndex)
            End If
        Else
            GetDealer()
        End If
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        ShowReport()
    End Sub

    Private Sub dgFeedback_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgFeedback.ItemCommand
        Dim lblID As Label = e.Item.FindControl("lblID")
        _sessHelper.SetSession("ComplainDetail", "FrmComplainFeedback.aspx")
        StoreCriteria()
        If e.CommandName = "View" Then
            Response.Redirect("FrmComplainResponse.aspx?Id=" & lblID.Text & "&Mode=View")
        ElseIf e.CommandName = "Edit" Then
            Response.Redirect("FrmComplainResponse.aspx?Id=" & lblID.Text & "&Mode=Edit")
        ElseIf e.CommandName = "Delete" Then
        End If

    End Sub

    Private Sub dgFeedback_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgFeedback.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objFeedback As V_CcComplainFollowUp = e.Item.DataItem

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dgFeedback.CurrentPageIndex * dgFeedback.PageSize + e.Item.ItemIndex + 1).ToString()

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatus.Text = GetStringValue(objFeedback.Status)

            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim objDealer As Dealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                lbtnDelete.Visible = False
            End If
        End If
    End Sub

    Private Sub dgFeedback_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgFeedback.PageIndexChanged
        dgFeedback.CurrentPageIndex = e.NewPageIndex
        BindingData(dgFeedback.CurrentPageIndex)
    End Sub

    Private Sub dgFeedback_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgFeedback.SortCommand
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

        dgFeedback.SelectedIndex = -1
        dgFeedback.CurrentPageIndex = 0
        BindingData(dgFeedback.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.cc_akses_feedback_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Customer Satisfaction")
        End If
    End Sub

    Private Sub GetDealer()
        If txtDealerCode.Text.Trim <> "" Then
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
            If Not objDealer Is Nothing Then
                txtDealerCode.Text = objDealer.DealerCode
                lblDealerName.Text = objDealer.DealerName
                _sessHelper.SetSession("OBJDEALER", objDealer)
            End If
        End If
    End Sub

    Private Sub GetStatusList()
        ddlStatus.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each item As ListItem In GetList()
            ddlStatus.Items.Add(item)
        Next
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub ShowReport()
        ViewState("CurrentSortColumn") = "DealerName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        dgFeedback.CurrentPageIndex = 0
        BindingData(dgFeedback.CurrentPageIndex)
    End Sub

    Private Sub BindingData(ByVal idxPage As Integer)
        If (idxPage >= 0) Then

            Dim objDealer As New Dealer
            If txtDealerCode.Text.Trim <> "" Then
                objDealer = CType(_sessHelper.GetSession("ObjDEALER"), Dealer)
                If objDealer Is Nothing Then
                    objDealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
                End If
            Else
                objDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
            End If

            Dim criterias As New CriteriaComposite(New Criteria(GetType(V_CcComplainFollowUp), "DealerID", MatchType.Exact, objDealer.ID))
            If ddlStatus.SelectedValue <> -1 Then
                criterias.opAnd(New Criteria(GetType(V_CcComplainFollowUp), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Integer)))
            End If


            Dim totalRow As Integer = 0
            Dim arlFeedback As ArrayList = New V_CcComplainFollowUpFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgFeedback.PageSize, totalRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
            _sessHelper.SetSession("ARRFEEDBACK", arlFeedback)
            dgFeedback.DataSource = arlFeedback
            dgFeedback.VirtualItemCount = totalRow
            dgFeedback.DataBind()
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("DealerCode", txtDealerCode.Text)
        crit.Add("DealerName", lblDealerName.Text)
        crit.Add("Status", ddlStatus.SelectedValue)
        crit.Add("PageIndex", dgFeedback.CurrentPageIndex)
        _sessHelper.SetSession("CriteriaFrmComplainFeedback", crit)  '-- Store in session
    End Sub

#End Region

#Region "Enum"
    Public Enum FeedbackStatus
        InProgress = 0
        UnFinished = 1
        Completed = 2
    End Enum

    Public Shared Function GetStringValue(ByVal iStatus As Integer) As String
        Dim str As String = ""
        If iStatus = FeedbackStatus.InProgress Then str = FeedbackStatus.InProgress.ToString
        If iStatus = FeedbackStatus.UnFinished Then str = FeedbackStatus.UnFinished.ToString
        If iStatus = FeedbackStatus.Completed Then str = FeedbackStatus.Completed.ToString
        Return str
    End Function

    Public Shared Function GetEnumValue(ByVal sFeedbackStatus As String) As Integer
        Dim Rsl As Integer = 0
        If sFeedbackStatus.ToUpper = FeedbackStatus.InProgress.ToString.ToUpper Then Rsl = FeedbackStatus.InProgress
        If sFeedbackStatus.ToUpper = FeedbackStatus.UnFinished.ToString.ToUpper Then Rsl = FeedbackStatus.UnFinished
        If sFeedbackStatus.ToUpper = FeedbackStatus.Completed.ToString.ToUpper Then Rsl = FeedbackStatus.Completed
        Return Rsl
    End Function

    Public Shared Function GetList() As ArrayList
        Dim arl As ArrayList = New ArrayList

        arl.Add(New ListItem(FeedbackStatus.InProgress.ToString, FeedbackStatus.InProgress))
        arl.Add(New ListItem(FeedbackStatus.UnFinished.ToString, FeedbackStatus.UnFinished))
        arl.Add(New ListItem(FeedbackStatus.Completed.ToString, FeedbackStatus.Completed))
        Return arl
    End Function
#End Region

End Class
