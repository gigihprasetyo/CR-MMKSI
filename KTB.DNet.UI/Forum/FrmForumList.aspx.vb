#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region


Public Class FrmForumList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKey As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStaus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCreateBy As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPostingList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icTglCreate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dtgForumList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icTglCreate2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkTanggal As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblTitleForum As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label

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
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim ForumList As ArrayList
    Dim criterias As CriteriaComposite
    Dim objForum As Forum
    Private SeparatorKeywords As String = ","
    'Private isFirstLoad As Boolean
#End Region

#Region "Custom Method"
    Private Sub HandleMarketingForum()
        Dim objMarketingCategory As ForumCategory
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumCategory), "Category", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("MarketingForumCategory")))
        Dim arlMarketingCategory As ArrayList = New ForumCategoryFacade(User).Retrieve(criterias)
        If arlMarketingCategory.Count > 0 Then
            objMarketingCategory = CType(arlMarketingCategory(0), ForumCategory)
        Else
            objMarketingCategory = New ForumCategory
        End If

        Dim objGeneralCategory As ForumCategory
        Dim criteriax As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumCategory), "Category", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("GeneralForumCategory")))
        Dim arlGeneralCategory As ArrayList = New ForumCategoryFacade(User).Retrieve(criteriax)
        If arlGeneralCategory.Count > 0 Then
            objGeneralCategory = CType(arlGeneralCategory(0), ForumCategory)
        Else
            objGeneralCategory = New ForumCategory
        End If



        Dim counter As Integer
StartRemove:
        counter = 0
        For Each item As ListItem In ddlKategori.Items
            If ddlKategori.Items(counter).Value <> objMarketingCategory.ID.ToString And ddlKategori.Items(counter).Value <> objGeneralCategory.ID.ToString Then
                ddlKategori.Items.RemoveAt(counter)
                GoTo StartRemove
            End If
            counter += 1
        Next



        'ddlKategori.SelectedValue = objForumCategory.ID
        'ddlKategori.Enabled = False
    End Sub
    Private Sub CreateCriteria(ByVal _isFirstLoad As Boolean)

        criterias = New CriteriaComposite(New Criteria(GetType(Forum), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If (ddlKategori.SelectedIndex <> 0) Then
            criterias.opAnd(New Criteria(GetType(Forum), "ForumCategory.ID", MatchType.Exact, ddlKategori.SelectedValue.ToString))
        End If


        'If _isFirstLoad Then
        'Else
        If (txtTitle.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(Forum), "Title", MatchType.[Partial], txtTitle.Text))
        End If
        If (txtKey.Text.ToString <> "") Then
            criterias.opAnd(New Criteria(GetType(Forum), "Description", MatchType.[Partial], txtKey.Text.ToString))
        End If

        If chkTanggal.Checked Then
            criterias.opAnd(New Criteria(GetType(Forum), "CreatedTime", MatchType.GreaterOrEqual, Format(icTglCreate.Value, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(Forum), "CreatedTime", MatchType.LesserOrEqual, Format(icTglCreate2.Value.AddDays(1), "yyyy-MM-dd HH:mm:ss")))
        End If

        If (txtCreateBy.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(Forum), "UserEntry", MatchType.[Partial], txtCreateBy.Text))
        End If
        criterias.opAnd(New Criteria(GetType(Forum), "Status", MatchType.Exact, ddlStaus.SelectedValue.ToString))
        'End If

        'TO DO : 
        '1 :Untuk(Forum) 'Tidak Terbuka' hanya boleh ditampilkan di daftar forum untuk membernya saja. 
        '2 :Forum untuk semua kondisi (terbuka/tertutup) harus bisa di akses oleh Member forum
        'Get Assigned Forum

        Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        Dim criteriaAssigned As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaAssigned.opAnd(New Criteria(GetType(ForumMember), "UserInfo.ID", MatchType.Exact, objUser.ID))

        Dim arlAssigned As ArrayList = New ForumMemberFacade(User).Retrieve(criteriaAssigned)
        Dim strForumIDToInclude As String = ""
        For Each itemAssigned As ForumMember In arlAssigned
            'Todo Inset
            strForumIDToInclude = strForumIDToInclude & itemAssigned.Forum.ID.ToString & ","
        Next

        If strForumIDToInclude <> "" Then
            criterias.opAnd(New Criteria(GetType(Forum), "Type", MatchType.Exact, "1"), "(", True)
            criterias.opOr(New Criteria(GetType(Forum), "Type", MatchType.Exact, "0"), "(", True)
            strForumIDToInclude = Left(strForumIDToInclude, strForumIDToInclude.Length - 1)
            criterias.opAnd(New Criteria(GetType(Forum), "ID", MatchType.InSet, "(" & strForumIDToInclude & ")"), "))", False)
        End If

        'If Request.QueryString("Marketing") <> String.Empty Then
        '    criterias.opAnd(New Criteria(GetType(Forum), "ForumCategory.Category", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("MarketingForumCategory")))
        'End If

        sessHelp.SetSession("critsx", criterias)

    End Sub
    Private Sub BindDatagrid(ByVal indexPage As Integer)

        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            ForumList = New ForumFacade(User).RetrieveActiveList(indexPage + 1, dtgForumList.PageSize, totalRow, sessHelp.GetSession("SortCol"), sessHelp.GetSession("SortDirection"), sessHelp.GetSession("critsx"))
            dtgForumList.DataSource = ForumList
            dtgForumList.VirtualItemCount = totalRow
            dtgForumList.CurrentPageIndex = indexPage
            dtgForumList.DataBind()
        End If

        If ForumList.Count = 0 And IsPostBack Then
            MessageBox.Show(SR.DataNotFound("Data"))
        End If

        sessHelp.SetSession("ForumIndexPage", dtgForumList.CurrentPageIndex)
        sessHelp.SetSession("ForumSortCol", sessHelp.GetSession("SortCol"))
        sessHelp.SetSession("ForumSortDirect", sessHelp.GetSession("SortDirection"))

    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ForumListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Forum - Daftar Forum")
        End If
    End Sub

    Private CekCmdButton As Boolean
    Private Function CheckCmdBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ForumNewCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    'privilege marketing
    Private CekCmdButtonForumMKT As Boolean = SecurityProvider.Authorize(context.User, SR.Forum_baru_marketing_buat_privilege)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Request.QueryString("Marketing") = "1" Then
            InitiateAuthorization()
            CekCmdButton = CheckCmdBtnPriv()
        End If

        If Not IsPostBack Then
            'cek apakah yang login dealer/ktb
            Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
            If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                dtgForumList.Columns(1).Visible = False
            Else
                dtgForumList.Columns(1).Visible = True
            End If

            viewstate.Item("isFirstLoad") = True
            CommonFunction.BindForumCategory(ddlKategori, User, True)
            sessHelp.SetSession("SortCol", "CreatedTime")
            sessHelp.SetSession("SortDirection", Sort.SortDirection.DESC)

            If Request.QueryString("Marketing") <> String.Empty Then
                If Request.QueryString("Marketing") = "1" Then
                    lblTitle.Text = "Daftar Forum Marketing"
                    HandleMarketingForum()
                End If
            End If
            ReadCriteria()

            If Request.QueryString("IsBack") <> String.Empty Then
                If Request.QueryString("IsBack") = "1" Then
                    sessHelp.SetSession("SortCol", sessHelp.GetSession("ForumSortCol"))
                    sessHelp.SetSession("SortDirection", sessHelp.GetSession("ForumSortDirect"))
                    BindDatagrid(sessHelp.GetSession("ForumIndexPage"))

                Else
                    btnSearch_Click(Me, System.EventArgs.Empty)
                End If
            Else
                btnSearch_Click(Me, System.EventArgs.Empty)
            End If
        Else
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SaveCriteria()
        dtgForumList.CurrentPageIndex = 0
        sessHelp.SetSession("SortCol", "CreatedTime")
        sessHelp.SetSession("SortDirection", Sort.SortDirection.DESC)
        viewstate.Item("isFirstLoad") = False
        CreateCriteria(viewstate.Item("isFirstLoad"))
        BindDatagrid(0)     '-- Bind page-1
    End Sub
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("ForumCategoryID", ddlKategori.SelectedValue.ToString)
        crits.Add("Title", txtTitle.Text)
        crits.Add("Description", txtKey.Text)
        If (chkTanggal.Checked) Then
            crits.Add("chkTanggal", "Y")
        Else
            crits.Add("chkTanggal", "N")
        End If
        crits.Add("CreatedTimeGreaterOrEqual", icTglCreate.Value)
        crits.Add("CreatedTimeLesserOrEqual", icTglCreate2.Value)
        crits.Add("UserEntry", txtCreateBy.Text)
        crits.Add("Status", ddlStaus.SelectedValue.ToString)
        'crits.Add("Page",dtgForumList

        sessHelp.SetSession("ForumCriteria", crits)

    End Sub
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessHelp.GetSession("ForumCriteria"), Hashtable)

        If Not IsNothing(crits) Then
            ddlKategori.SelectedValue = CStr(crits.Item("ForumCategoryID"))
            txtTitle.Text = CStr(crits.Item("Title"))
            txtKey.Text = CStr(crits.Item("Description"))
            If (CStr(crits.Item("chkTanggal")) = "Y") Then
                chkTanggal.Checked = True
                icTglCreate.Value = CDate(crits.Item("CreatedTimeGreaterOrEqual"))
                icTglCreate2.Value = CDate(crits.Item("CreatedTimeLesserOrEqual"))
            End If
            txtCreateBy.Text = CStr(crits.Item("UserEntry"))
            ddlStaus.SelectedValue = CStr(crits.Item("Status"))
        End If
    End Sub
    Private Sub dtgForumList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgForumList.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (ForumList Is Nothing) Then
                objForum = ForumList(e.Item.ItemIndex)

                Dim lblTitleForum As LinkButton = CType(e.Item.FindControl("lblTitleForum"), LinkButton)
                lblTitleForum.Text = objForum.Title.ToString
                lblTitleForum.CommandName = "viewtopiclist"
                lblTitleForum.CommandArgument = objForum.ID.ToString

                Dim lblDescription As Label = CType(e.Item.FindControl("lblDescription"), Label)
                lblDescription.Text = objForum.Description.ToString.Replace(CommonFunction.GetPageBreakFCKeditor, "<br>")


                Dim lblDate As Label = CType(e.Item.FindControl("lblDate"), Label)
                lblDate.Text = objForum.CreatedTime.ToString

                Dim lblBy As Label = CType(e.Item.FindControl("lblBy"), Label)
                lblBy.Text = "oleh : " + objForum.UserEntry.ToString

                Dim lblTopic As Label = CType(e.Item.FindControl("lblTopic"), Label)
                lblTopic.Text = objForum.TotalTopik.ToString

                Dim lblPosting As Label = CType(e.Item.FindControl("lblPosting"), Label)
                lblPosting.Text = objForum.TotalPosting.ToString

                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                lbtnEdit.CommandArgument = objForum.ID

                'cek privilege
                If Not Request.QueryString("Marketing") = "1" Then
                    If CekCmdButton = False Then
                        lbtnEdit.Visible = False
                    Else
                        lbtnEdit.Visible = True
                    End If
                Else
                    lbtnEdit.Visible = CekCmdButtonForumMKT
                End If


            End If
        End If
    End Sub
    Private Sub dtgForumList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgForumList.ItemCommand
        If e.CommandName = "Edit" Then
            Response.Redirect("../Forum/FrmManageForum.aspx?ID=" & e.CommandArgument)
            'ElseIf e.CommandName = "Forum" Then
            '    Response.Redirect("../Forum/TopicList.aspx?ID=" & e.CommandArgument)
        ElseIf e.CommandName = "viewtopiclist" Then
            sessHelp.SetSession("IdForumOnTopicList", e.CommandArgument)
            Dim oFrm As Forum = New ForumFacade(User).Retrieve(CType(e.CommandArgument, Integer))

            If oFrm.Status = 0 Then
                MessageBox.Show("Anda tidak dapat melihat data forum yang tidak aktif")
                Exit Sub
            End If
            sessHelp.SetSession("BackPage", "FrmForumList.aspx")

            Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ForumMember), "Forum.ID", MatchType.Exact, oFrm.ID.ToString))
            criterias.opAnd(New Criteria(GetType(ForumMember), "UserInfo.ID", MatchType.Exact, objUser.ID.ToString))
            Dim arlMember As ArrayList = New ForumMemberFacade(User).Retrieve(criterias)

            If oFrm.Type = 0 And arlMember.Count = 0 Then
                MessageBox.Show("Sifat forum tidak terbuka, dan anda bukan anggota forum ini")
            Else
                If Request.QueryString("Marketing") <> String.Empty Then
                    If Request.QueryString("Marketing") = "1" Then
                        Response.Redirect("../Forum/TopicList.aspx?Marketing=1")
                    Else
                        Response.Redirect("../Forum/TopicList.aspx")
                    End If
                Else
                    Response.Redirect("../Forum/TopicList.aspx")
                End If
            End If
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Redirect("../Forum/FrmManageForum.aspx")
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Redirect("../Forum/FrmCreateTopic.aspx?&idforum=2")
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Redirect("../Forum/FrmReplyTopic.aspx?&idtopic=2")
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Redirect("../Forum/FrmCreateTopic.aspx?&idtopic=2")
    End Sub
    Private Sub dtgForumList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgForumList.SortCommand
        If e.SortExpression = sessHelp.GetSession("SortCol") Then
            If sessHelp.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelp.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelp.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelp.SetSession("SortCol", e.SortExpression)
        dtgForumList.SelectedIndex = -1
        dtgForumList.CurrentPageIndex = 0
        BindDatagrid(0)
    End Sub
    Private Sub dtgForumList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgForumList.PageIndexChanged
        dtgForumList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgForumList.CurrentPageIndex)
    End Sub
End Class

