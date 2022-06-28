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

Public Class FrmSendMessage
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchUser As System.Web.UI.WebControls.Label
    Protected WithEvents txtTempField As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtForumPMMsg As CKEditor.NET.CKEditorControl
    Protected WithEvents btnSend As System.Web.UI.WebControls.Button
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

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ForumPMCompose_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Forum - Private Message")
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not Page.IsPostBack Then
            txtUserName.Attributes.Add("readonly", "readonly")
        End If
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim objUserInfoCurr As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        Dim objForumPM As New ForumPM

        If txtSubject.Text = "" Then
            ' MsgBox("Masukkan judul Subject")
            KTB.DNet.Utility.MessageBox.Show("Masukkan judul Subject")
            Exit Sub
        Else
            objForumPM.Subject = txtSubject.Text
            objForumPM.Message = txtForumPMMsg.Text
            objForumPM.UserFrom = objUserInfoCurr.ID

            Dim objUserInfo As UserInfo = New UserInfoFacade(User).RetrievebyUserNameAndDealerCode(txtUserName.Text.Trim, txtTempField.Text.Trim)
            objForumPM.UserInfo = objUserInfo

            If (New ForumPMFacade(User).Insert(objForumPM) <> -1) Then
                Response.Redirect("FrmForumPMInbox.aspx?isBack=2")
            Else
                MessageBox.Show("Pesan gagal dikirim")
            End If
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmForumPMInbox.aspx?isBack=1")
    End Sub
End Class
