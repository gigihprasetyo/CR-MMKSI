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
Public Class TopicList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlForum As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtTopicDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPostingBy As System.Web.UI.WebControls.TextBox
    Protected WithEvents ctnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgTopicList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnNeTopic As System.Web.UI.WebControls.Button
    Protected WithEvents icTglCreate2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglCreate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblListTitle As System.Web.UI.WebControls.Label
    Protected WithEvents chkTgl As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
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

#Region "Custom Variable Declaration"
    Dim objForumCategoryFacade As ForumCategoryFacade = New ForumCategoryFacade(User)
    Dim objForumFacade As ForumFacade = New ForumFacade(User)
    Dim objForumLastReadPostFacade As ForumLastReadPostFacade = New ForumLastReadPostFacade(User)
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim ForumTopicList As ArrayList
    Dim ForumLastReadPostList As ArrayList
    Dim criterias As CriteriaComposite
    Dim objForumTopic As ForumTopic
    Dim objForumLastReadPost As ForumLastReadPost
    Private SeparatorKeywords As String = ","
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
        For Each item As ListItem In ddlCategory.Items
            If ddlCategory.Items(counter).Value <> objMarketingCategory.ID.ToString And ddlCategory.Items(counter).Value <> objGeneralCategory.ID.ToString Then
                ddlCategory.Items.RemoveAt(counter)
                GoTo StartRemove
            End If
            counter += 1
        Next


    End Sub
    Private Sub BindDropDown()
    End Sub
    Private Sub bindArrayListToDropDownList(ByRef objDropDownList As DropDownList, ByVal objArrayList As ArrayList, ByVal DataTextField As String)
        objDropDownList.DataSource = Nothing
        If Not IsNothing(objArrayList) Then
            objDropDownList.DataSource = objArrayList
            objDropDownList.DataTextField = DataTextField
            objDropDownList.DataValueField = "ID"
            objDropDownList.DataBind()
        End If
    End Sub
    Function CheckDate() As Boolean
        Dim nResult = 1
        If chkTgl.Checked Then
            If icTglCreate.Value > icTglCreate2.Value Then
                MessageBox.Show("Periode mulai harus lebih kecil dari selesai")
                nResult = 0
            End If
        End If
        Return nResult
    End Function
    Private Sub BindDatagrid(ByVal indexPage As Integer, ByVal isFirstTime As Boolean)

        Dim totalRow As Integer = 0
        dtgTopicList.CurrentPageIndex = indexPage
        If (indexPage >= 0) Then
            ForumTopicList = New ForumTopicFacade(User).RetrieveActiveList(indexPage + 1, dtgTopicList.PageSize, totalRow, sessHelp.GetSession("SortCol"), sessHelp.GetSession("SortDirection"), sessHelp.GetSession("crits"))
            dtgTopicList.DataSource = ForumTopicList
            dtgTopicList.VirtualItemCount = totalRow
            dtgTopicList.DataBind()
        End If

        If ForumTopicList.Count = 0 And IsPostBack Then
            MessageBox.Show(SR.DataNotFound("Data"))
        End If
        sessHelp.SetSession("TopicIndexPage", dtgTopicList.CurrentPageIndex)
        sessHelp.SetSession("TopicSortCol", sessHelp.GetSession("SortCol"))
        sessHelp.SetSession("TopicSortDirect", sessHelp.GetSession("SortDirection"))

    End Sub
    Private Sub CreateCriteria(ByVal isFirstTime As Boolean)

        criterias = New CriteriaComposite(New Criteria(GetType(ForumTopic), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Request.QueryString("Marketing") <> String.Empty Then
            If Request.QueryString("Marketing") = "1" Then
                Dim criteriax As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriax.opAnd(New Criteria(GetType(ForumCategory), "Category", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("MarketingForumCategory")), "(", True)
                criteriax.opOr(New Criteria(GetType(ForumCategory), "Category", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("GeneralForumCategory")), ")", False)

                Dim arlMarketingCategory As ArrayList = New ForumCategoryFacade(User).Retrieve(criteriax)

                Dim StrCategoryToInclude As String = ""

                For Each item As ForumCategory In arlMarketingCategory
                    StrCategoryToInclude = StrCategoryToInclude & item.ID.ToString & ","
                Next

                'Dim criteriay As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Forum), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'If StrCategoryToInclude <> "" Then
                '    StrCategoryToInclude = Left(StrCategoryToInclude, StrCategoryToInclude.Length - 1)
                '    criteriay.opAnd(New Criteria(GetType(Forum), "ForumCategory.ID", MatchType.InSet, "(" & StrCategoryToInclude & ")"))
                'Else
                '    criteriay.opAnd(New Criteria(GetType(Forum), "ForumCategory.ID", MatchType.InSet, "(0)"))
                'End If

                'Dim arlForum As ArrayList = New ForumFacade(User).Retrieve(criteriay)

                'Dim strMarketingForumID As String = ""
                'For Each itemForum As Forum In arlForum
                '    strMarketingForumID = strMarketingForumID & itemForum.ID.ToString & ","
                'Next

                'If strMarketingForumID <> "" Then
                '    strMarketingForumID = Left(strMarketingForumID, strMarketingForumID.Length - 1)
                '    criterias.opAnd(New Criteria(GetType(ForumTopic), "Forum.ID", MatchType.InSet, "(" & strMarketingForumID & ")"))
                'End If

                If StrCategoryToInclude <> "" Then
                    StrCategoryToInclude = Left(StrCategoryToInclude, StrCategoryToInclude.Length - 1)
                    criterias.opAnd(New Criteria(GetType(ForumTopic), "Forum.ForumCategory.ID", MatchType.InSet, "(" & StrCategoryToInclude & ")"))
                End If

            End If
        End If

        If Not isFirstTime Then
            If (Request.QueryString("id") <> "") Then
                criterias.opAnd(New Criteria(GetType(ForumTopic), "Forum.ID", MatchType.Exact, Request.QueryString("id")))
            Else
                If (ddlForum.SelectedIndex <> -1) Then
                    criterias.opAnd(New Criteria(GetType(ForumTopic), "Forum.ID", MatchType.Exact, ddlForum.SelectedValue.ToString))
                End If
            End If

            criterias.opAnd(New Criteria(GetType(ForumTopic), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If (txtTopicDescription.Text.ToString <> "") Then
                criterias.opAnd(New Criteria(GetType(ForumTopic), "Description", MatchType.[Partial], txtTopicDescription.Text.ToString))
            End If

            'TO DO : from where Keyords can get 
            'If (txtKeywords.Text.ToString <> "") Then
            '    criterias.opAnd(New Criteria(GetType(ForumTopic), "", MatchType.Exact, txtKeywords.Text.ToString))
            'End If
            If chkTgl.Checked Then
                criterias.opAnd(New Criteria(GetType(ForumTopic), "CreatedTime", MatchType.GreaterOrEqual, Format(icTglCreate.Value, "yyyy-MM-dd HH:mm:ss")))
                criterias.opAnd(New Criteria(GetType(ForumTopic), "CreatedTime", MatchType.LesserOrEqual, Format(icTglCreate2.Value.AddDays(1), "yyyy-MM-dd HH:mm:ss")))
            End If


            If (txtPostingBy.Text <> "") Then
                criterias.opAnd(New Criteria(GetType(ForumTopic), "UserEntry", MatchType.[Partial], txtPostingBy.Text))
            End If
        End If

        'Handle Forum tdk terbuka
        Dim strIDForumMember As String = ""
        Dim criteriaMember As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaMember.opAnd(New Criteria(GetType(ForumMember), "UserInfo.ID", MatchType.Exact, CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo).ID))
        Dim arlForumMember As ArrayList = New ForumMemberFacade(User).Retrieve(criteriaMember)
        For Each item As ForumMember In arlForumMember
            strIDForumMember = strIDForumMember & item.Forum.ID.ToString & ","
        Next

        If strIDForumMember = "" Then
            criterias.opAnd(New Criteria(GetType(ForumTopic), "Forum.Type", MatchType.Exact, 1))
        Else
            criterias.opAnd(New Criteria(GetType(ForumTopic), "Forum.Type", MatchType.Exact, 1), "(", True)
            strIDForumMember = Left(strIDForumMember, strIDForumMember.Length - 1)
            criterias.opOr(New Criteria(GetType(ForumTopic), "Forum.ID", MatchType.InSet, "(" & strIDForumMember & ")"), ")", False)
        End If

        sessHelp.SetSession("crits", criterias)


    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ForumListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Forum - Daftar Forum")
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            InitiateAuthorization()

            bindArrayListToDropDownList(ddlCategory, objForumCategoryFacade.RetrieveCategoryTypeList, "Category")
            ddlCategory.SelectedIndex = 0
            sessHelp.SetSession("SortCol", "CreatedTime")
            sessHelp.SetSession("SortDirection", Sort.SortDirection.DESC)

            ReadCriteria()

            If Request.QueryString("Marketing") <> String.Empty Then
                If Request.QueryString("Marketing") = "1" Then
                    lblTitle.Text = "Daftar Topik Forum Marketing"
                    viewstate.Add("Marketing", "1")
                    HandleMarketingForum()
                End If
            End If




            If sessHelp.GetSession("IdForumOnTopicList") <> String.Empty And sessHelp.GetSession("IdForumOnTopicList") <> "" Then
                Dim objForum As Forum = New ForumFacade(User).Retrieve(CInt(sessHelp.GetSession("IdForumOnTopicList")))
                ddlCategory.SelectedValue = objForum.ForumCategory.ID.ToString
                'ddlCategory_SelectedIndexChanged(Me, System.EventArgs.Empty)
                ddlForum.SelectedValue = objForum.ID.ToString
                sessHelp.RemoveSession("IdForumOnTopicList")
                CreateCriteria(False)
                BindDatagrid(0, False)
                SaveCriteria()
                Exit Sub
            End If

            If sessHelp.GetSession("IdForumInserted") <> String.Empty And sessHelp.GetSession("IdForumInserted") <> "" Then
                Dim objForum As Forum = New ForumFacade(User).Retrieve(CInt(sessHelp.GetSession("IdForumInserted")))
                ddlCategory.SelectedValue = objForum.ForumCategory.ID.ToString
                'ddlCategory_SelectedIndexChanged(Me, System.EventArgs.Empty)
                ddlForum.SelectedValue = objForum.ID.ToString
                sessHelp.RemoveSession("IdForumInserted")
                CreateCriteria(False)
                BindDatagrid(0, False)
                SaveCriteria()
                Exit Sub
            End If


            If Request.QueryString("IsBack") <> String.Empty Then
                If Request.QueryString("IsBack") = "1" Then
                    sessHelp.SetSession("SortCol", sessHelp.GetSession("TopicSortCol"))
                    sessHelp.SetSession("SortDirection", sessHelp.GetSession("TopicSortDirect"))
                    CreateCriteria(False)
                    BindDatagrid(sessHelp.GetSession("TopicIndexPage"), False)
                    SaveCriteria()
                    btnKembali.Visible = False
                    Exit Sub
                Else
                    BindDatagrid(0, False)
                    SaveCriteria()
                    btnKembali.Visible = False
                    Exit Sub
                End If
            Else
                CreateCriteria(False)
                BindDatagrid(0, False)
                SaveCriteria()
                btnKembali.Visible = False
                Exit Sub
            End If


            If (Request.QueryString.Count <> 0 And Request.QueryString("id") <> "") Then
                CreateCriteria(False)
                BindDatagrid(0, False)
                SaveCriteria()
                Exit Sub
            Else
                btnKembali.Enabled = False
            End If
            CreateCriteria(True)
            BindDatagrid(0, True)

            SaveCriteria()


        End If

    End Sub
    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        bindArrayListToDropDownList(ddlForum, objForumFacade.RetrieveOpenForumByCategoryID(CType(ddlCategory.SelectedValue, Integer)), "Title")
    End Sub
    Private Sub ctnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ctnSearch.Click
        SaveCriteria()
        If ddlForum.Items.Count = 0 Then
            MessageBox.Show("Maaf, category " & ddlCategory.SelectedItem.Text & " belum memiliki forum.")
            Return
        End If

        If chkTgl.Checked And icTglCreate.Value > icTglCreate2.Value Then
            MessageBox.Show("Tanggal awal harus lebih kecil dari tanggal akhir.")
            Return
        End If

        If CheckDate() Then
            dtgTopicList.CurrentPageIndex = 0
            lblListTitle.Text = "FORUM - " + ddlForum.SelectedItem.Text
            sessHelp.SetSession("SortCol", "LastUpdateTime")
            sessHelp.SetSession("SortDirection", Sort.SortDirection.DESC)
            CreateCriteria(False)
            BindDatagrid(0, False)
        End If
    End Sub
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("CategoryID", ddlCategory.SelectedValue.ToString)
        crits.Add("ForumID", ddlForum.SelectedValue.ToString)
        crits.Add("Description", txtTopicDescription.Text)
        If (chkTgl.Checked) Then
            crits.Add("chkTgl", "Y")
        Else
            crits.Add("chkTgl", "N")
        End If
        crits.Add("CreatedTimeGreaterOrEqual", icTglCreate.Value)
        crits.Add("CreatedTimeLesserOrEqual", icTglCreate.Value)
        crits.Add("UserEntry", txtPostingBy.Text)
        sessHelp.SetSession("TopicCriteria", crits)
    End Sub
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessHelp.GetSession("TopicCriteria"), Hashtable)

        If Not IsNothing(crits) Then
            ddlCategory.SelectedValue = CStr(crits.Item("CategoryID"))
            ddlCategory_SelectedIndexChanged(Me, System.EventArgs.Empty)
            ddlForum.SelectedValue = CStr(crits.Item("ForumID"))
            txtTopicDescription.Text = CStr(crits.Item("Description"))
            If (CStr(crits.Item("chkTgl")) = "Y") Then
                chkTgl.Checked = True
                icTglCreate.Value = CDate(crits.Item("CreatedTimeGreaterOrEqual"))
                icTglCreate2.Value = CDate(crits.Item("CreatedTimeLesserOrEqual"))
            End If
            txtPostingBy.Text = CStr(crits.Item("UserEntry"))
        Else
            ddlCategory_SelectedIndexChanged(Me, System.EventArgs.Empty)
        End If
    End Sub
    Private Sub dtgTopicList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgTopicList.PageIndexChanged
        dtgTopicList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgTopicList.CurrentPageIndex, False)
    End Sub
    Private Sub dtgTopicList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTopicList.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (ForumTopicList Is Nothing) Then
                objForumTopic = ForumTopicList(e.Item.ItemIndex)

                Dim lbtnTopic As LinkButton = CType(e.Item.FindControl("lbtnTopic"), LinkButton)
                lbtnTopic.Text = objForumTopic.Title.ToString
                lbtnTopic.CommandArgument = objForumTopic.ID

                Dim lblDescription As Label = CType(e.Item.FindControl("lblDescription"), Label)
                lblDescription.Text = objForumTopic.Description.ToString.Replace(CommonFunction.GetPageBreakFCKeditor, "<br>")

                Dim lblCreatedBy As Label = CType(e.Item.FindControl("lblCreatedBy"), Label)
                lblCreatedBy.Text = "Dibuat oleh : " + objForumTopic.UserEntry.ToString

                Dim lblLastPostingDate As Label = CType(e.Item.FindControl("lblLastPostingDate"), Label)
                lblLastPostingDate.Text = objForumTopic.LastUpdateTime.ToString

                Dim arlPost As ArrayList = objForumTopic.ForumPosts
                Dim oForumPost As ForumPost = arlPost(arlPost.Count - 1)
                Dim lblLastPostingBy As Label = CType(e.Item.FindControl("lblLastPostingBy"), Label)
                lblLastPostingBy.Text = "oleh : " + oForumPost.UserEntry.ToString

                Dim lblTanggapan As Label = CType(e.Item.FindControl("lblTanggapan"), Label)
                lblTanggapan.Text = objForumTopic.TotalPosting.ToString

                Dim lbtnWarning As LinkButton = CType(e.Item.FindControl("lbtnWarning"), LinkButton)
                lbtnWarning.CommandArgument = objForumTopic.ID

                criterias = New CriteriaComposite(New Criteria(GetType(ForumLastReadPost), "ForumTopic.ID", MatchType.Exact, objForumTopic.ID))
                Dim objUserInfo As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)

                criterias.opAnd(New Criteria(GetType(ForumLastReadPost), "UserInfo.ID", MatchType.Exact, objUserInfo.ID))

                ForumLastReadPostList = objForumLastReadPostFacade.Retrieve(criterias)


                If (ForumLastReadPostList.Count <> 0) Then
                    objForumLastReadPost = CType(ForumLastReadPostList(0), ForumLastReadPost)
                    If objForumLastReadPost.ForumPost.ID < CType(objForumTopic.ForumPosts(objForumTopic.ForumPosts.Count - 1), ForumPost).ID Then
                        lbtnWarning.Visible = True
                    Else
                        lbtnWarning.Visible = False
                    End If
                Else
                    lbtnWarning.Visible = True
                End If

            End If
        End If
    End Sub
    Private Sub btnNeTopic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNeTopic.Click
        'Dim objForum As Forum = New ForumFacade(User).Retrieve(CInt(ddlForum.SelectedValue))
        SaveCriteria()
        If ddlForum.Items.Count = 0 Then
            MessageBox.Show("Maaf, category " & ddlCategory.SelectedItem.Text & " belum memiliki forum.")
            Return
        End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ForumMember), "UserInfo.ID", MatchType.Exact, CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo).ID))
        criterias.opAnd(New Criteria(GetType(ForumMember), "Forum.ID", MatchType.Exact, ddlForum.SelectedValue))
        Dim arlForumMember As ArrayList = New ForumMemberFacade(User).Retrieve(criterias)

        If arlForumMember.Count = 0 Then
            MessageBox.Show("Anda tidak dapat membuat topik pada Forum " & ddlForum.SelectedItem.Text & ", karena belum menjadi member")
            Exit Sub
        End If

        If Request.QueryString("Marketing") <> String.Empty Then
            If Request.QueryString("Marketing") = "1" Then
                Response.Redirect("../Forum/FrmCreateTopic.aspx?idforum=" & ddlForum.SelectedValue & "&Marketing=1")
            Else
                Response.Redirect("../Forum/FrmCreateTopic.aspx?idforum=" & ddlForum.SelectedValue)
            End If
        Else
            Response.Redirect("../Forum/FrmCreateTopic.aspx?idforum=" & ddlForum.SelectedValue)
        End If

    End Sub
    Private Sub dtgTopicList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTopicList.ItemCommand
        If e.CommandName = "Topic" Then
            Dim objForumTopic As ForumTopic = New ForumTopicFacade(User).Retrieve(CInt(e.CommandArgument))
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumMember), "Forum.ID", MatchType.Exact, objForumTopic.Forum.ID))
            criterias.opAnd(New Criteria(GetType(ForumMember), "UserInfo.ID", MatchType.Exact, CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo).ID))

            Dim arlForumMember As ArrayList = New ForumMemberFacade(User).RetrieveByCriteria(criterias)

            sessHelp.SetSession("BackPage", "TopicList.aspx")
            If arlForumMember.Count > 0 Then
                If viewstate("Marketing") = "1" Then
                    Response.Redirect("../Forum/FrmReadMessage.aspx?ID=" & e.CommandArgument & "&Marketing=1")
                Else
                    Response.Redirect("../Forum/FrmReadMessage.aspx?ID=" & e.CommandArgument)
                End If

            Else
                MessageBox.Show("Anda bukan anggota forum " + objForumTopic.Forum.Title)
            End If

        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Redirect("../Forum/FrmCreateTopic.aspx?idforum=" & ddlForum.SelectedValue)
    End Sub
    Private Sub dtgTopicList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgTopicList.SortCommand
        If e.SortExpression = sessHelp.GetSession("SortCol") Then
            If sessHelp.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelp.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelp.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelp.SetSession("SortCol", e.SortExpression)
        dtgTopicList.SelectedIndex = -1
        dtgTopicList.CurrentPageIndex = 0
        BindDatagrid(0, False)
    End Sub
    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        If viewstate("Marketing") = "1" Then
            Response.Redirect("../Forum/FrmForumList.aspx?isback=1&Marketing=1")
        Else
            Response.Redirect("../Forum/FrmForumList.aspx?isback=1")
        End If

    End Sub
End Class
