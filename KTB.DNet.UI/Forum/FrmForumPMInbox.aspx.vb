Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security

Public Class FrmForumPMInbox
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbtnInbox As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnOutbox As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnCompose As System.Web.UI.WebControls.LinkButton
    Protected WithEvents dtgForumList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents dtgForumPM As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Enum"
    Private Enum MsgType
        Inbox = 1
        Outbox = 2
    End Enum
#End Region

#Region "Private Declaration"
    Private qsBack As Integer
    Private criterias As CriteriaComposite
    Private sHelper As New SessionHelper
#End Region

#Region "Custom Method"
    Private Sub BindGrid(ByVal indexPage As Integer, ByVal tipePesan As Integer)
        Dim arlList As ArrayList
        Dim totalRow As Integer

        If indexPage >= 0 Then
            CreateCriteria(tipePesan)
            arlList = New ForumPMFacade(User).RetrieveActiveList(indexPage + 1, dtgForumPM.PageSize, totalRow, viewstate("SortColForumPM"), viewstate("SortDirForumPM"), criterias)

            If Not arlList Is Nothing Then
                If arlList.Count > 0 Then
                    dtgForumPM.DataSource = arlList
                    dtgForumPM.VirtualItemCount = totalRow
                Else
                    dtgForumPM.DataSource = New ArrayList
                End If
            End If

            dtgForumPM.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria(ByVal tipePesan As Integer)
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        criterias = New CriteriaComposite(New Criteria(GetType(ForumPM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If tipePesan = MsgType.Inbox Then
            criterias.opAnd(New Criteria(GetType(ForumPM), "UserInfo.ID", MatchType.Exact, objUserInfo.ID))
            criterias.opAnd(New Criteria(GetType(ForumPM), "isDeletedInbox", MatchType.Exact, 0))
        Else
            criterias.opAnd(New Criteria(GetType(ForumPM), "UserFrom", MatchType.Exact, objUserInfo.ID))
            criterias.opAnd(New Criteria(GetType(ForumPM), "isDeletedOutBox", MatchType.Exact, 0))
        End If
    End Sub

    Private Sub CustomizeGrid(ByVal MsgType As Integer)
        Select Case MsgType
            Case 1  'inbox
                dtgForumPM.Columns(3).Visible = True
                dtgForumPM.Columns(4).Visible = False
                dtgForumPM.Columns(5).HeaderText = "Received"
            Case 2  'outbox
                dtgForumPM.Columns(3).Visible = False
                dtgForumPM.Columns(4).Visible = True
                dtgForumPM.Columns(5).HeaderText = "Sent"
        End Select
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ForumPMView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Forum - Private Message")
        End If
    End Sub

    Private CekCmdButton As Boolean
    Private Function CekCmdBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ForumPMCompose_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Viewstate.Add("SortColForumPM", "CreatedTime")
        Viewstate.Add("SortDirForumPM", Sort.SortDirection.DESC)

        InitiateAuthorization()
        CekCmdButton = CekCmdBtnPriv()
        If CekCmdButton = False Then
            lbtnCompose.Enabled = False
        Else
            lbtnCompose.Enabled = True
        End If
        If Not IsPostBack Then
            'qsBack = Request.QueryString("isBack")
            qsBack = sHelper.GetSession("TipePesan")
            'sHelper.SetSession("TipePesan", MsgType.Inbox)
            If qsBack = MsgType.Outbox Then
                CustomizeGrid(MsgType.Outbox)
                BindGrid(0, MsgType.Outbox)
                sHelper.SetSession("TipePesan", MsgType.Outbox)
            Else
                CustomizeGrid(MsgType.Inbox)
                BindGrid(0, MsgType.Inbox)
                sHelper.SetSession("TipePesan", MsgType.Inbox)
            End If

        End If
    End Sub

    Private Sub lbtnOutbox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnOutbox.Click
        sHelper.SetSession("TipePesan", MsgType.Outbox)
        Response.Redirect("FrmForumPMInbox.aspx?isBack=" & MsgType.Outbox)
    End Sub

    Private Sub lbtnInbox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnInbox.Click
        sHelper.SetSession("TipePesan", MsgType.Inbox)
        Response.Redirect("FrmForumPMInbox.aspx?isBack=" & MsgType.Inbox)
    End Sub

    Private Sub dtgForumPM_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgForumPM.ItemDataBound
        Dim modeView As Integer = CInt(sHelper.GetSession("TipePesan"))
        If e.Item.ItemIndex <> -1 Then
            Dim lblFrom As Label = CType(e.Item.FindControl("lblFrom"), Label)
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblSentTo As Label = CType(e.Item.FindControl("lblSentTo"), Label)
            Dim lblIsRead As Label = CType(e.Item.FindControl("lblIsRead"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dtgForumPM.PageSize * dtgForumPM.CurrentPageIndex)

            Dim objForumPM As ForumPM = New ForumPMFacade(User).Retrieve(CInt(lblID.Text))
            Dim lbtnSubject As LinkButton = CType(e.Item.FindControl("lbtnSubject"), LinkButton)
            lbtnSubject.CommandArgument = lblID.Text & ";" & lblSentTo.Text & ";" & lblFrom.Text

            If objForumPM.isRead = 0 Then
                lbtnSubject.Text = "<strong>" & objForumPM.Subject & "</strong>"
            End If
            If lblFrom.Text.Trim <> "" Then
                Dim objUserInfo As UserInfo = New UserInfoFacade(User).Retrieve(CInt(lblFrom.Text))
                lblFrom.Text = objUserInfo.UserName
            Else
                lblSentTo.Text = String.Empty
            End If
            
            If lblSentTo.Text.Trim <> "" Then
                Dim objUserInfoSent As UserInfo = New UserInfoFacade(User).Retrieve(CInt(lblSentTo.Text))
                lblSentTo.Text = objUserInfoSent.UserName
            Else
                lblSentTo.Text = String.Empty
            End If
            
        End If
    End Sub

    Private Sub dtgForumPM_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgForumPM.PageIndexChanged
        dtgForumPM.CurrentPageIndex = e.NewPageIndex
        BindGrid(dtgForumPM.CurrentPageIndex, CInt(viewstate("TipePesan")))
    End Sub

    Private Sub dtgForumPM_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgForumPM.SortCommand
        If e.SortExpression = viewstate("SortColForumPM") Then
            If viewstate("SortDirForumPM") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirForumPM", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirForumPM", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColForumPM", e.SortExpression)
        BindGrid(dtgForumPM.CurrentPageIndex, CInt(viewstate("TipePesan")))
    End Sub

    Private Sub lbtnCompose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnCompose.Click
        Response.Redirect("FrmSendMessage.aspx")
    End Sub

    Private Sub dtgForumPM_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgForumPM.ItemCommand
        If e.CommandName = "ShowMSG" Then
            Dim lbtnSubject As LinkButton = CType(e.Item.FindControl("lbtnSubject"), LinkButton)
            Dim cmdArg() As String = lbtnSubject.CommandArgument.ToString.Split(";")
            sHelper.SetSession("UserSentTo", cmdArg(1))
            sHelper.SetSession("UserFrom", cmdArg(2))

            'update status isRead dari 0 ke 1
            Dim objDomain As ForumPM = New ForumPMFacade(User).Retrieve(CInt(cmdArg(0)))
            If CInt(sHelper.GetSession("TipePesan")) = MsgType.Inbox Then
                objDomain.isRead = 1
            End If

            Dim iresult As Integer = New ForumPMFacade(User).Update(objDomain)

            Response.Redirect("frmMessageShow.aspx?ID=" & cmdArg(0))
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objForumPM As ForumPM
        Dim arlList As New ArrayList
        Dim modePM As Integer = CInt(sHelper.GetSession("TipePesan"))
        For Each item As DataGridItem In dtgForumPM.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chk.Checked Then
                Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                objForumPM = New ForumPMFacade(User).Retrieve(CInt(lblID.Text))
                arlList.Add(objForumPM)
            End If
        Next

        If arlList.Count > 0 Then
            If (New ForumPMFacade(User).Update(arlList, modePM) <> -1) Then
                Response.Redirect("FrmForumPMInbox.aspx?isBack=" & MsgType.Outbox)
            Else
                MessageBox.Show("Hapus pesan gagal")
            End If
        End If
    End Sub
End Class
