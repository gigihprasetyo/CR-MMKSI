Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls


#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmReplyTopic
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LblForum As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtUserID As System.Web.UI.WebControls.TextBox
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents LblTopic As System.Web.UI.WebControls.Label
    Protected WithEvents txtDescription As CKEditor.NET.CKEditorControl
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

#Region " Private Variables"
    Dim sessHelp As KTB.DNet.Utility.SessionHelper = New KTB.DNet.Utility.SessionHelper
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            If Request.QueryString("Marketing") <> String.Empty Then
                If Request.QueryString("Marketing") = "1" Then
                    viewstate.Add("Marketing", "1")
                End If
            End If

            Dim objForumTopic As ForumTopic = New ForumTopicFacade(User).Retrieve(CInt(Request.QueryString("idtopic")))
            LblForum.Text = objForumTopic.Forum.ForumCategory.Category & " - " & objForumTopic.Forum.Title
            LblTopic.Text = objForumTopic.Title
            viewstate.Add("TopicID", objForumTopic.ID.ToString)
        End If

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim objforumpost As ForumPost = New ForumPost

        Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        objforumpost.UserInfo = objUserInfo
        objforumpost.ForumTopic = New ForumTopicFacade(User).Retrieve(CInt(Request.QueryString("idtopic")))

        objforumpost.Description = txtDescription.Text
        If fileUpload.Value <> "" OrElse fileUpload.Value <> Nothing Then
            Dim finfo2 As New FileInfo(fileUpload.PostedFile.FileName)
            objforumpost.Attachment = finfo2.Name
        Else
            objforumpost.Attachment = ""
        End If
        objforumpost.isHeader = 0
        objforumpost.UserEntry = objUserInfo.Dealer.DealerCode & "-" & objUserInfo.UserName

        Dim ForumPostId As Integer = New ForumPostFacade(User).InsertTransaction(objforumpost)
        btnSimpan.Enabled = False


        If fileUpload.Value <> "" OrElse fileUpload.Value <> Nothing Then
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

            If fileUpload.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            Else
                Dim SrcFile As String = Path.GetFileName(fileUpload.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ForumAttnDir") & "\Forum" & Request.QueryString("idtopic") & "\" & ForumPostId & SrcFile   '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        fileUpload.PostedFile.SaveAs(DestFile)
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try

                If viewstate("Marketing") = "1" Then
                    Response.Redirect("FrmReadMessage.aspx?Marketing=1&id=" + Request.QueryString("idtopic") + "&afterreply=1")
                Else
                    Response.Redirect("FrmReadMessage.aspx?id=" + Request.QueryString("idtopic") + "&afterreply=1")
                End If
            End If
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If viewstate("Marketing") = "1" Then
            Response.Redirect("../Forum/FrmReadMessage.aspx?isback=1&Marketing=1&ID=" & viewstate("TopicID"))
        Else
            Response.Redirect("../Forum/FrmReadMessage.aspx?isback=1&ID=" & viewstate("TopicID"))
        End If
    End Sub
End Class
