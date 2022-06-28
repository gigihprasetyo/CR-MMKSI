Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class FrmMessageShow
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtForumPMMsg As CKEditor.NET.CKEditorControl
    Protected WithEvents btnSend As System.Web.UI.WebControls.Button
    Protected WithEvents btnReplay As System.Web.UI.WebControls.Button
    Protected WithEvents btnDel As System.Web.UI.WebControls.Button
    Protected WithEvents txtTempField As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnResend As System.Web.UI.WebControls.Button
    Protected WithEvents lblUserReplay As System.Web.UI.WebControls.Label
    Protected WithEvents lblViewMsg As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sHelper As New SessionHelper

#Region "Custom Method"
    Private Sub MapToControl(ByVal qsID As Integer)
        Dim objForumPM As ForumPM = New ForumPMFacade(User).Retrieve(qsID)
        txtSubject.Text = objForumPM.Subject
        'txtForumPMMsg.Value = objForumPM.Message
        lblViewMsg.Text = objForumPM.Message

        If CInt(viewstate("ModeView")) = 1 Then
            Dim userFrom As Integer = CInt(sHelper.GetSession("UserFrom"))
            Dim objUserInfo As UserInfo = New UserInfoFacade(User).Retrieve(userFrom)
            lblUserReplay.Text = objUserInfo.UserName
            txtTempField.Text = objuserinfo.Dealer.DealerCode
        Else
            Dim userSent As Integer = CInt(sHelper.GetSession("UserSentTo"))
            Dim objUserInfo As UserInfo = New UserInfoFacade(User).Retrieve(userSent)
            lblUserReplay.Text = objUserInfo.UserName
            txtTempField.Text = objuserinfo.Dealer.DealerCode
        End If

        viewstate.Add("IDxxx", qsID)
    End Sub

    Private Sub MapReplyControl(ByVal objDomain As ForumPM)
        btnSend.Visible = True
        txtSubject.Text = "Re: " & objDomain.Subject
        Dim objUserInfo As UserInfo = New UserInfoFacade(User).Retrieve(CInt(objDomain.UserFrom))
        lblUserReplay.Text = objUserInfo.UserName
        txtTempField.Text = objUserInfo.Dealer.DealerCode
        txtForumPMMsg.Text = "<br>----------------------------------------------------<br>" & _
                "<strong>Subject: " & objDomain.Subject & "</strong><br>" & _
                "From: " & objUserInfo.UserName & "<br> Pesan: <br>" & objDomain.Message
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim modeView As Integer = sHelper.GetSession("TipePesan")
        viewstate.Add("ModeView", modeView)
        'QS for view Message
        Dim qsID As Integer = CInt(Request.QueryString("ID"))
        viewstate.Add("QSView", qsID)

        If Not IsPostBack Then
            'query string for reply
            Dim idx As Integer = Request.QueryString("idxxx")
            Dim isReply As String = Request.QueryString("isReply")
            If Not isReply = "true" Then
                If CInt(viewstate("ModeView")) = 2 Then
                    btnReplay.Visible = False
                    btnResend.Visible = True
                Else
                    btnReplay.Visible = True
                    btnResend.Visible = False
                End If

                btnSend.Visible = False
                btnDel.Visible = True
                lblViewMsg.Visible = True
                txtForumPMMsg.Visible = False
                lblTitle.Text = "Baca Pesan"
                MapToControl(qsID)
            Else
                lblTitle.Text = "Balas Pesan"
                btnSend.Visible = True
                btnResend.Visible = False
                btnDel.Visible = False
                btnReplay.Visible = False
                lblViewMsg.Visible = False
                txtForumPMMsg.Visible = True
                Dim objForumPM As ForumPM = New ForumPMFacade(User).Retrieve(idx)
                MapReplyControl(objForumPM)
            End If
        End If
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim objUserInfoCurr As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        Dim objForumPM As New ForumPM

        If txtSubject.Text = "" Then
            KTB.DNet.Utility.MessageBox.Show("Masukkan judul Subject")
            'MsgBox("Masukkan judul Subject")
            Exit Sub
        Else
            objForumPM.Subject = txtSubject.Text
            objForumPM.Message = txtForumPMMsg.Text
            objForumPM.UserFrom = objUserInfoCurr.ID

            Dim objUserInfo As UserInfo = New UserInfoFacade(User).RetrievebyUserNameAndDealerCode(lblUserReplay.Text.Trim, txtTempField.Text.Trim)
            objForumPM.UserInfo = objUserInfo

            If (New ForumPMFacade(User).Insert(objForumPM) <> -1) Then
                Response.Redirect("FrmForumPMInbox.aspx?isBack=2")
            Else
                MessageBox.Show("Pesan gagal dikirim")
            End If
        End If
    End Sub

    Private Sub btnReplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplay.Click
        Dim idxxx As Integer = CInt(viewstate("IDxxx"))
        Response.Redirect("FrmMessageShow.aspx?isReply=true&idxxx=" & idxxx)
    End Sub

    Private Sub btnResend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResend.Click
        Dim objUserInfoCurr As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        Dim objForumPM As New ForumPM

        If txtSubject.Text = "" Then
            MsgBox("Masukkan judul Subject")
            Exit Sub
        Else
            objForumPM.Subject = txtSubject.Text
            objForumPM.Message = lblViewMsg.Text ' txtForumPMMsg.Text
            objForumPM.UserFrom = objUserInfoCurr.ID

            Dim objUserInfo As UserInfo = New UserInfoFacade(User).RetrievebyUserNameAndDealerCode(lblUserReplay.Text.Trim, txtTempField.Text.Trim)
            objForumPM.UserInfo = objUserInfo

            If (New ForumPMFacade(User).Insert(objForumPM) <> -1) Then
                Response.Redirect("FrmForumPMInbox.aspx?isBack=2")
            Else
                MessageBox.Show("Pesan gagal dikirim")
            End If
        End If
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Dim idDel As Integer = CInt(viewstate("QSView"))
        Dim tipePesan As Integer = CInt(sHelper.GetSession("TipePesan"))

        Dim objDomain As ForumPM = New ForumPMFacade(User).Retrieve(idDel)
        If tipePesan = 1 Then
            objDomain.isDeletedInbox = 1
        Else
            objDomain.isDeletedOutBox = 1
        End If

        If (New ForumPMFacade(User).Update(objDomain) <> -1) Then
            Response.Redirect("FrmForumPMInbox.aspx")
        Else
            MessageBox.Show("Hapus pesan gagal")
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmForumPMInbox.aspx?isBack=1")
    End Sub
End Class
