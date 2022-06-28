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

Public Class FrmComplainResponse
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgFeedback As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblIDComplain As System.Web.UI.WebControls.Label
    Protected WithEvents lblConsumer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSurveyDate As System.Web.UI.WebControls.Label
    Protected WithEvents ltrComplain As System.Web.UI.WebControls.Literal
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblError As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        InitData()
    End Sub

#End Region

#Region " Custom Variable Declaration "

    Private _sesshelper As SessionHelper = New SessionHelper
    Private _objVCF As V_CcComplainFollowUp
    Private _objCF As CcComplainFollowup
    Private _objCFDetail As CcComplainFollowupDetail
    Private _objDealer As Dealer
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            BindDataComplain()
            SetDataGridMode()
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim url As String = CType(Session("ComplainDetail"), String)
        If Not url Is Nothing Then
            Server.Transfer(url)
        End If
    End Sub

    Private Sub dgFeedback_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgFeedback.ItemDataBound
        If Request.QueryString("Mode") = "View" Then
            e.Item.Cells(4).Visible = False
            e.Item.Cells(5).Visible = False
            e.Item.Cells(6).Visible = False
        ElseIf Request.QueryString("Mode") = "Edit" Then
            e.Item.Cells(4).Visible = True
            e.Item.Cells(5).Visible = True
            e.Item.Cells(6).Visible = True
        End If

        If (e.Item.ItemType = ListItemType.Footer) OrElse (e.Item.ItemType = ListItemType.EditItem) Then
            Dim ddlStatus As DropDownList
            If e.Item.ItemType = ListItemType.Footer Then
                ddlStatus = CType(e.Item.FindControl("ddlFooterStatus"), DropDownList)
            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                ddlStatus = CType(e.Item.FindControl("ddlEditStatus"), DropDownList)
            End If

            Dim arrayListAdditional As ArrayList = New EnumSPKAdditional().RetrieveSPKAdditional()
            ddlStatus.Items.Add(New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In GetList()
                ddlStatus.Items.Add(item)
            Next
            If ddlStatus.Items.Count > 0 Then
                ddlStatus.SelectedIndex = 0
            Else
                ddlStatus.ClearSelection()
            End If
        Else
            If Not e.Item.DataItem Is Nothing Then
                Dim objFeedback As CcComplainFollowupDetail = e.Item.DataItem

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (dgFeedback.CurrentPageIndex * dgFeedback.PageSize + e.Item.ItemIndex + 1).ToString()

                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatus.Text = GetStringValue(objFeedback.Status)

                'Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                'Dim objDealer As Dealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
                'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                '    lbtnDelete.Visible = False
                'End If
            End If
        End If

    End Sub

    Private Sub dgFeedback_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgFeedback.ItemCommand
        Select Case (e.CommandName)
            Case "Add"
                AddCommand(e)
            Case "Delete"
                DeleteCommand(e)
        End Select
    End Sub

    Private Sub dgFeedback_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgFeedback.UpdateCommand
        UpdateCommand(e)
    End Sub

    Private Sub dgFeedback_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgFeedback.EditCommand
        dgFeedback.ShowFooter = False
        dgFeedback.EditItemIndex = CInt(e.Item.ItemIndex)
        BindaDataComplainDetail(_objVCF)
    End Sub

    Private Sub dgFeedback_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgFeedback.CancelCommand
        dgFeedback.ShowFooter = True
        dgFeedback.EditItemIndex = -1
        BindaDataComplainDetail(_objVCF)
    End Sub
#End Region

#Region "Custom"

    Private Sub InitData()
        _objVCF = New V_CcComplainFollowUpFacade(User).Retrieve(CType(Request.QueryString("Id"), Integer))
    End Sub

    Private Sub BindDataComplain()
        If Not _objVCF Is Nothing Then
            _objDealer = New DealerFacade(User).Retrieve(_objVCF.DealerID)
            If Not _objDealer Is Nothing Then
                lblDealer.Text = _objDealer.DealerCode & " / " & _objDealer.DealerName
            Else
                lblDealer.Text = String.Empty
            End If
            lblIDComplain.Text = _objVCF.ID.ToString
            lblConsumer.Text = _objVCF.ConsumerName
            lblSurveyDate.Text = String.Format("dd MMM yyyy", _objVCF.TglSurvey)
            ltrComplain.Text = _objVCF.complain
            BindaDataComplainDetail(_objVCF)
        End If
    End Sub

    Private Sub BindaDataComplainDetail(ByVal oVCF As V_CcComplainFollowUp)
        _objCF = New CcComplainFollowupFacade(User).Retrieve(oVCF.ID)
        dgFeedback.DataSource = _objCF.CcComplainFollowupDetails
        dgFeedback.DataBind()
    End Sub

    Private Sub SetDataGridMode()
        If Request.QueryString("Mode") = "View" Then
            dgFeedback.ShowFooter = False
            dgFeedback.Enabled = True
        ElseIf Request.QueryString("Mode") = "Edit" Then
            dgFeedback.ShowFooter = True
            dgFeedback.Enabled = True
        End If
    End Sub

    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)

        Dim ddlFooterStatus As DropDownList = e.Item.FindControl("ddlFooterStatus")
        Dim txtFooterNotes As TextBox = e.Item.FindControl("txtFooterNotes")
        
        If (ddlFooterStatus.SelectedValue = -1) Then
            lblError.Text = "Status harap dipilih"
            Exit Sub
        End If

        If (txtFooterNotes.Text.Trim = "") Then
            lblError.Text = "Tanggapan harap diisi"
            Exit Sub
        End If
        Try
            _objCF = New CcComplainFollowupFacade(User).Retrieve(_objVCF.ID)
            _objCFDetail = New CcComplainFollowupDetail
            _objCFDetail.CcComplainFollowup = _objCF
            _objCFDetail.Note = txtFooterNotes.Text
            _objCFDetail.Status = ddlFooterStatus.SelectedValue
            Dim iReturn As Integer = New CcComplainFollowupDetailFacade(User).Insert(_objCFDetail)
            If iReturn > 0 Then
                BindaDataComplainDetail(_objVCF)
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
        
    End Sub

    Private Sub UpdateCommand(ByVal E As DataGridCommandEventArgs)
        Dim ddlEditStatus As DropDownList = E.Item.FindControl("ddlEditStatus")
        Dim txtEditNotes As TextBox = E.Item.FindControl("txtEditNotes")

        If (ddlEditStatus.SelectedValue = -1) Then
            lblError.Text = "Status harap dipilih"
            Exit Sub
        End If

        If (txtEditNotes.Text.Trim = "") Then
            lblError.Text = "Tanggapan harap diisi"
            Exit Sub
        End If
        Try
            _objCF = New CcComplainFollowupFacade(User).Retrieve(_objVCF.ID)
            Dim _objCFDetailFacade As New CcComplainFollowupDetailFacade(User)
            _objCFDetail = New CcComplainFollowupDetail
            _objCFDetail = _objCF.CcComplainFollowupDetails(E.Item.ItemIndex)
            _objCFDetail.CcComplainFollowup = _objCF
            _objCFDetail.Note = txtEditNotes.Text
            _objCFDetail.Status = ddlEditStatus.SelectedValue
            Dim iReturn As Integer = _objCFDetailFacade.Update(_objCFDetail)
            If iReturn <> -1 Then
                MessageBox.Show(SR.UpdateSucces)
                _objCF.Status = ddlEditStatus.SelectedValue
                Dim iRtn As Integer = New CcComplainFollowupFacade(User).Update(_objCF)
                dgFeedback.EditItemIndex = -1
                dgFeedback.ShowFooter = True
                BindaDataComplainDetail(_objVCF)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs)
        Try
            Dim _objCFDetailFacade As New CcComplainFollowupDetailFacade(User)
            _objCFDetail = New CcComplainFollowupDetail
            _objCFDetail = _objCF.CcComplainFollowupDetails(e.Item.ItemIndex)
            Dim iReturn As Integer = _objCFDetailFacade.Update(_objCFDetail)
            If iReturn <> -1 Then
                MessageBox.Show(SR.DeleteSucces)
                BindaDataComplainDetail(_objVCF)
            Else
                MessageBox.Show(SR.DeleteSucces)
            End If
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try

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
