#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Configuration

#End Region

Public Class MarketingRedirector
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ForumCategory), "Category", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("MarketingForumCategory")))

        Dim arlForumCategory As ArrayList = New ForumCategoryFacade(User).Retrieve(criterias)

        If arlForumCategory.Count = 0 Then
            Response.Write("Maaf Forum Marketing Belum Tersedia")
        Else
            If Request.QueryString("Menu") <> String.Empty Then
                If Request.QueryString("Menu") = "ForumManage" Then
                    Response.Redirect("FrmForumList.aspx?Marketing=1")
                ElseIf Request.QueryString("Menu") = "DnetForum" Then
                    Response.Redirect("TopicList.aspx?Marketing=1")
                End If
            End If
        End If


    End Sub

End Class
