Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls


#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmReadMessage
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnReply As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMessagePosting As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblListTitle As System.Web.UI.WebControls.Label
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
#Region "Custom Variable Declaration"
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim ForumPostList As ArrayList
    Dim criterias As CriteriaComposite
    Dim objForumPost As ForumPost
#End Region

#Region "Custom Method"
    Private Sub CreateCriteria()
        If Request.QueryString("ID") <> "" Then
            criterias = New CriteriaComposite(New Criteria(GetType(ForumPost), "ForumTopic.ID", MatchType.Exact, Request.QueryString("ID").ToString))
        End If
    End Sub
    Private Sub BindDatagrid(ByVal indexPage As Integer)

        CreateCriteria()
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            ForumPostList = New ForumPostFacade(User).RetrieveActiveList(indexPage, dtgMessagePosting.PageSize, totalRow, sessHelp.GetSession("SortCol"), sessHelp.GetSession("SortDirection"), criterias)
            dtgMessagePosting.DataSource = ForumPostList
            dtgMessagePosting.VirtualItemCount = totalRow
            dtgMessagePosting.DataBind()
            'Manage ForumLastReadPost

            If ForumPostList.Count > 0 And indexPage = dtgMessagePosting.PageCount - 1 Then


                Dim objForumLastReadPostFacade As ForumLastReadPostFacade = New ForumLastReadPostFacade(User)
                Dim objLastPost As ForumPost = CType(ForumPostList(ForumPostList.Count - 1), ForumPost)
                Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)

                criterias = New CriteriaComposite(New Criteria(GetType(ForumLastReadPost), "ForumTopic.ID", MatchType.Exact, Request.QueryString("ID").ToString))
                criterias.opAnd(New Criteria(GetType(ForumLastReadPost), "UserInfo.ID", MatchType.Exact, objUserInfo.ID.ToString))

                Dim arlForumLastReadPost As ArrayList = objForumLastReadPostFacade.Retrieve(criterias)

                Dim objForumLastReadPost As ForumLastReadPost
                If arlForumLastReadPost.Count = 0 Then
                    objForumLastReadPost = New ForumLastReadPost
                    objForumLastReadPost.UserInfo = objUserInfo
                    objForumLastReadPost.ForumTopic = New ForumTopicFacade(User).Retrieve(CInt(Request.QueryString("ID")))
                    objForumLastReadPost.ForumPost = objLastPost
                    objForumLastReadPostFacade.Insert(objForumLastReadPost)

                Else
                    objForumLastReadPost = arlForumLastReadPost(0)
                    objForumLastReadPost.UserInfo = objUserInfo
                    objForumLastReadPost.ForumTopic = New ForumTopicFacade(User).Retrieve(CInt(Request.QueryString("ID")))
                    objForumLastReadPost.ForumPost = objLastPost
                    objForumLastReadPostFacade.Update(objForumLastReadPost)
                End If
            End If
        End If
        If ForumPostList.Count = 0 And IsPostBack Then
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

        sessHelp.SetSession("PostIndexPage", dtgMessagePosting.CurrentPageIndex)
        sessHelp.SetSession("PostSortCol", sessHelp.GetSession("SortCol"))
        sessHelp.SetSession("PostSortDirect", sessHelp.GetSession("SortDirection"))

    End Sub
#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then

            If Request.QueryString("Marketing") <> String.Empty Then
                If Request.QueryString("Marketing") = "1" Then
                    viewstate.Add("Marketing", "1")
                End If
            End If

            Dim objForumTopic As ForumTopic = New ForumTopicFacade(User).Retrieve(CInt(Request.QueryString("ID")))
            lblListTitle.Text = "FORUM - " + objForumTopic.Forum.Title + " - " + objForumTopic.Title
            lblDate.Text = DateTime.Now
            sessHelp.SetSession("SortCol", "CreatedTime")
            sessHelp.SetSession("SortDirection", Sort.SortDirection.ASC)

            If Request.QueryString("afterreply") = String.Empty Then
                If Request.QueryString("IsBack") <> String.Empty Then
                    If Request.QueryString("IsBack") = "1" Then
                        sessHelp.SetSession("SortCol", sessHelp.GetSession("PostSortCol"))
                        sessHelp.SetSession("SortDirection", sessHelp.GetSession("PostSortDirect"))
                        BindDatagrid(sessHelp.GetSession("PostIndexPage"))
                    Else
                        BindDatagrid(0)
                    End If
                Else
                    BindDatagrid(0)
                End If
            Else
                BindDatagrid(0)
                BindDatagrid(dtgMessagePosting.PageCount - 1)
            End If
        End If

    End Sub
    Private Sub dtgMessagePosting_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMessagePosting.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (ForumPostList Is Nothing) Then
                objForumPost = ForumPostList(e.Item.ItemIndex)

                Dim lblUser As Label = CType(e.Item.FindControl("lblUser"), Label)
                lblUser.Text = objForumPost.UserEntry.ToString

                Dim lblTittle As Label = CType(e.Item.FindControl("lblTittle"), Label)
                lblTittle.Text = objForumPost.ForumTopic.Title.ToString

                Dim lblMessagePosting As Label = CType(e.Item.FindControl("lblMessagePosting"), Label)
                lblMessagePosting.Text = objForumPost.Description.ToString.Replace(CommonFunction.GetPageBreakFCKeditor, "<br>")

                Dim lblJoinDate As Label = CType(e.Item.FindControl("lblJoinDate"), Label)
                lblJoinDate.Text = "Tgl Posting: " & objForumPost.CreatedTime.ToString("dd/MM/yyyy")

                Dim lblPosting As Label = CType(e.Item.FindControl("lblPosting"), Label)
                lblPosting.Text = "Posting: " & objForumPost.UserInfo.TotalPosting.ToString()


                Dim lbtnFileName As LinkButton = CType(e.Item.FindControl("lbtnFileName"), LinkButton)
                lbtnFileName.CommandName = "download"

                If objForumPost.Attachment = "" Then
                    lbtnFileName.Visible = False
                Else
                    If objForumPost.isHeader = 1 Then
                        lbtnFileName.CommandArgument = "Hdr" + objForumPost.Attachment
                    Else
                        lbtnFileName.CommandArgument = objForumPost.ID.ToString + objForumPost.Attachment
                    End If
                End If



                Dim lbtnImage As Image = CType(e.Item.FindControl("lbtnImage"), Image)
                lbtnImage.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & objForumPost.UserInfo.ID & "&type=" & "UserInfo"

            End If
        End If
    End Sub
    Private Sub btnReply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReply.Click

        If viewstate("Marketing") = "1" Then
            Response.Redirect("FrmReplyTopic.aspx?Marketing=1&idtopic=" + Request.QueryString("ID").ToString)
        Else
            Response.Redirect("FrmReplyTopic.aspx?idtopic=" + Request.QueryString("ID").ToString)
        End If

    End Sub
    Private Sub dtgMessagePosting_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMessagePosting.SortCommand
        If e.SortExpression = sessHelp.GetSession("SortCol") Then
            If sessHelp.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelp.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelp.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelp.SetSession("SortCol", e.SortExpression)
        dtgMessagePosting.SelectedIndex = -1
        dtgMessagePosting.CurrentPageIndex = 0
        BindDatagrid(0)
    End Sub
    Private Sub dtgMessagePosting_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMessagePosting.ItemCommand
        If e.CommandName = "download" Then
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("ForumAttnDir") & "\Forum" & Request.QueryString("id") & "\" & e.CommandArgument
            Response.Redirect("../Download.aspx?file=" & PathFile)
        End If
    End Sub
    Private Sub dtgMessagePosting_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMessagePosting.PageIndexChanged
        dtgMessagePosting.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(e.NewPageIndex + 1)
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        If viewstate("Marketing") = "1" Then
            Response.Redirect("../Forum/TopicList.aspx?isback=1&Marketing=1")
        Else
            Response.Redirect("../Forum/TopicList.aspx?isback=1")
        End If


    End Sub
End Class
