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


Public Class FrmCreateTopic
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtUserID As System.Web.UI.WebControls.TextBox
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents LblForum As System.Web.UI.WebControls.Label
    Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
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
            If Request.QueryString("idtopic") = String.Empty Then
                Dim objForum As Forum = New ForumFacade(User).Retrieve(CInt(Request.QueryString("idforum")))
                LblForum.Text = objForum.ForumCategory.Category & " - " & objForum.Title
            Else
                Dim objForumTopic As ForumTopic = New ForumTopicFacade(User).Retrieve(CInt(Request.QueryString("idtopic")))
                LblForum.Text = objForumTopic.Forum.ForumCategory.Category & " - " & objForumTopic.Forum.Title
                txtTitle.Text = objForumTopic.Title

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPost), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ForumPost), "ForumTopic.Id", MatchType.Exact, CInt(Request.QueryString("idtopic"))))
                criterias.opAnd(New Criteria(GetType(ForumPost), "isHeader", MatchType.Exact, 1))

                Dim objForumPostHeader As ForumPost = New ForumPostFacade(User).RetrieveByCriteria(criterias, 1, 1, 1)(0)
                txtDescription.Text = objForumPostHeader.Description
            End If
        End If


    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim ForumTopicId As Integer
        If Request.QueryString("idtopic") = String.Empty Then
            Dim objforumtopic As ForumTopic = New ForumTopic
            Dim objforumpost As ForumPost = New ForumPost

            Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
            objforumpost.UserInfo = objUserInfo
            objforumpost.ForumTopic = objforumtopic
            objforumtopic.Forum = New ForumFacade(User).Retrieve(CInt(Request.QueryString("idforum")))
            objforumtopic.Title = txtTitle.Text
            objforumtopic.Description = txtDescription.Text
            objforumtopic.UserEntry = objUserInfo.Dealer.DealerCode & "-" & objUserInfo.UserName

            objforumpost.Description = txtDescription.Text
            objforumpost.UserEntry = objUserInfo.Dealer.DealerCode & "-" & objUserInfo.UserName

            If fileUpload.Value <> "" OrElse fileUpload.Value <> Nothing Then
                Dim finfo2 As New FileInfo(fileUpload.PostedFile.FileName)
                objforumpost.Attachment = finfo2.Name
                objforumtopic.Attachment = finfo2.Name
            Else
                objforumpost.Attachment = ""
                objforumtopic.Attachment = ""
            End If

            objforumpost.isHeader = 1

            Dim objLastReadPost As ForumLastReadPost = New ForumLastReadPost
            objLastReadPost.UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
            objLastReadPost.ForumTopic = objforumtopic
            objLastReadPost.ForumPost = objforumpost

            ForumTopicId = New ForumTopicFacade(User).InsertTransaction(objforumtopic, objforumpost, objLastReadPost)

        Else
            Dim objforumtopic As ForumTopic = New ForumTopicFacade(User).Retrieve(CInt(Request.QueryString("idtopic")))

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPost), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ForumPost), "ForumTopic.Id", MatchType.Exact, CInt(Request.QueryString("idtopic"))))
            criterias.opAnd(New Criteria(GetType(ForumPost), "isHeader", MatchType.Exact, 1))
            Dim objForumPost As ForumPost = New ForumPostFacade(User).RetrieveByCriteria(criterias, 1, 1, 1)(0)

            objforumpost.ForumTopic = objforumtopic
            objforumtopic.Title = txtTitle.Text
            objforumtopic.Description = txtDescription.Text
            objForumPost.Description = txtDescription.Text

            If fileUpload.Value <> "" OrElse fileUpload.Value <> Nothing Then
                Dim finfo2 As New FileInfo(fileUpload.PostedFile.FileName)
                objforumpost.Attachment = finfo2.Name
                objforumtopic.Attachment = finfo2.Name
            Else
                objforumpost.Attachment = ""
                objforumtopic.Attachment = ""
            End If

            objforumpost.isHeader = 1

            ForumTopicId = New ForumTopicFacade(User).UpdateTransaction(objforumtopic, objforumpost)

        End If

        If fileUpload.Value <> "" OrElse fileUpload.Value <> Nothing Then
            'cek maxFileSize first
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
            If fileUpload.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            Else
                Dim SrcFile As String = Path.GetFileName(fileUpload.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ForumAttnDir") & "\Forum" & ForumTopicId & "\Hdr" & SrcFile   '-- Destination file
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
            End If
        End If

        If Request.QueryString("Marketing") <> String.Empty Then
            If Request.QueryString("Marketing") = "1" Then
                Response.Redirect("TopicList.aspx?Marketing=1&isback=1")
            Else
                Response.Redirect("TopicList.aspx?isback=1")
            End If
        Else
            Response.Redirect("TopicList.aspx?isback=1")
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../Forum/TopicList.aspx?isback=1")
    End Sub
End Class
