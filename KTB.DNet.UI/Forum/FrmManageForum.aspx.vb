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

Public Class FrmManageForum
    Inherits System.Web.UI.Page
#Region "Custom Variable Declaration"
    Private objForum As Forum
    Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgForumMember As System.Web.UI.WebControls.DataGrid
    Private arlForum As ArrayList

    Private arlForumMember As ArrayList = New ArrayList
    Private objForumMember As ForumMember
    Protected WithEvents txtUserID As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents txtDescription As CKEditor.NET.CKEditorControl
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Private sHelper As SessionHelper = New SessionHelper

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlForum As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        If Request.QueryString("Marketing") <> String.Empty Then
            If Request.QueryString("Marketing") = "1" Then
                HandleMarketingForum()
            End If
        End If

    End Sub

#End Region

#Region "Custom Method"
    Private Sub DisplayData(ByVal forumID As Integer)
        Dim objForum As Forum = New ForumFacade(User).Retrieve(forumID)
        txtTitle.Text = objForum.Title

        txtDescription.Text = objForum.Description



        ddlKategori.SelectedValue = objForum.ForumCategory.ID
        ddlForum.SelectedValue = objForum.Type
        ddlStatus.SelectedValue = objForum.Status

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ForumMember), "Forum.ID", MatchType.Exact, forumID))

        arlForumMember = New ForumMemberFacade(User).RetrieveByCriteria(criterias, 1, 100, 100)
        sHelper.SetSession("SessArlMember", arlForumMember)
        sHelper.SetSession("PageIndex", "0")
        BindDataGridMember(0)
    End Sub
    Private Function UpdateForum() As Boolean
        Dim objForumFacade As ForumFacade = New ForumFacade(User)
        Dim objForumCategoryFacade As ForumCategoryFacade = New ForumCategoryFacade(User)
        Dim objForum As Forum = New ForumFacade(User).Retrieve(CInt(Request.QueryString("id")))
        Dim nResult As Integer

        If objForumFacade.ValidateCode(txtTitle.Text, CInt(Request.QueryString("id"))) > 0 Then
            MessageBox.Show(SR.DataIsExist("Forum"))
            Return False
        End If
        objForum.ForumCategory = objForumCategoryFacade.Retrieve(CType(ddlKategori.SelectedValue, Integer))
        objForum.Title = txtTitle.Text
        objForum.Description = txtDescription.Text
        objForum.Type = ddlForum.SelectedValue
        objForum.Status = ddlStatus.SelectedValue
        objForum.UserEntry = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo).Dealer.DealerCode + CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo).UserName

        nResult = New ForumFacade(User).Update(objForum)
        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
            Return False
        Else
            MessageBox.Show(SR.SaveSuccess)
            Return True
        End If

    End Function
    Private Function InsertForum() As Boolean
        Dim sesshelper As SessionHelper = New SessionHelper
        Dim objForumFacade As ForumFacade = New ForumFacade(User)
        Dim objForumCategoryFacade As ForumCategoryFacade = New ForumCategoryFacade(User)
        Dim objForum As Forum = New Forum
        Dim nResult As Integer
        Dim objUser As UserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)

        If objForumFacade.ValidateCode(txtTitle.Text, 0) = 0 Then
            objForum.ForumCategory = objForumCategoryFacade.Retrieve(CType(ddlKategori.SelectedValue, Integer))
            objForum.Title = txtTitle.Text
            objForum.Description = txtDescription.Text
            objForum.Type = ddlForum.SelectedValue
            objForum.Status = ddlStatus.SelectedValue
            objForum.UserEntry = objUser.Dealer.DealerCode & "-" & objUser.UserName

            'arlForumMember = sHelper.GetSession("SessArlMember")

            nResult = New ForumFacade(User).InsertTransaction(objForum, CType(sesshelper.GetSession("LOGINUSERINFO"), UserInfo))
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
                Return False
            Else
                MessageBox.Show(SR.SaveSuccess)
                If Request.QueryString("Marketing") = "1" Then
                    Response.Redirect("FrmManageForum.aspx?Marketing=1&id=" & nResult.ToString & "&isInsert=true")
                Else
                    Response.Redirect("FrmManageForum.aspx?id=" & nResult.ToString & "&isInsert=true")
                End If

                Return True
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Forum"))
            Return False
        End If
    End Function
    Public Sub ClearData()
        ddlKategori.SelectedIndex = 0
        txtTitle.Text = String.Empty
        txtDescription.Text = String.Empty
        ddlForum.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0

    End Sub
    Private Sub BindDataGridMember(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ForumMember), "Forum.ID", MatchType.Exact, Request.QueryString("id")))
            arlForumMember = New ForumMemberFacade(User).RetrieveActiveList(indexPage, dtgForumMember.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"), criterias)
            dtgForumMember.DataSource = arlForumMember
            dtgForumMember.VirtualItemCount = totalRow
            dtgForumMember.DataBind()
            sHelper.SetSession("SessArlMember", arlForumMember)
        End If
    End Sub

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

    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ForumNewView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Forum - Daftar Forum")
        End If
    End Sub

    Private CekCmdButton As Boolean = SecurityProvider.Authorize(context.User, SR.ForumNewCreate_Privilege)
    Private CekNewMemberBtn As Boolean = SecurityProvider.Authorize(context.User, SR.ForumNewMember_Privilege)
            
    'privilege for Forum Marketing
    Private Sub InitiatePrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.Forum_baru_marketing_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FORUM - Forum Marketing Baru")
        End If
    End Sub

    Private CekCmdButtonForumMKT As Boolean = SecurityProvider.Authorize(context.User, SR.Forum_baru_marketing_buat_privilege)
    Private CekNewMemberBtnForumMKT As Boolean = SecurityProvider.Authorize(context.User, SR.Forum_baru_marketing_member_privilege)

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Request.QueryString("Marketing") = "1" Then
            InitiatePrivilege()
        Else
            InitiateAuthorization()
        End If

        If Not IsPostBack Then

            CommonFunction.BindForumCategory(ddlKategori, User, True)
            ClearData()
            sHelper.SetSession("SortCol", "UserInfo.Dealer.DealerCode")
            sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            sHelper.SetSession("SessArlMember", arlForumMember)
            'btnTambahMember.Attributes("onclick") = "ShowPopUpForumMember();"
            If Request.QueryString("id") <> String.Empty Then 'Edit
                DisplayData(CInt(Request.QueryString("id")))
            Else
                btnBack.Visible = False
                'btnTambahMember.Enabled = False
                btnChoose.Disabled = True
                dtgForumMember.DataSource = New ArrayList
                dtgForumMember.DataBind()
            End If

            'For Popup
            sHelper.SetSession("SortColPopUp", "UserName")
            sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)

            If Request.QueryString("Marketing") <> String.Empty Then
                If Request.QueryString("Marketing") = "1" Then
                    lblTitle.Text = "Forum Marketing Baru"
                    HandleMarketingForum()
                End If
            End If


            If Request.QueryString("isInsert") <> String.Empty And Request.QueryString("isInsert") = "true" Then
                btnBack.Visible = False
                MessageBox.Show(SR.SaveSuccess & " Silakan entry data member untuk forum ini, atau klik simpan untuk mengubah data.")
            End If
        Else
            'Postback from jscript
            If Request("__EVENTARGUMENT") = "AddMember" Then
                btnTambahMember_Click(Me, System.EventArgs.Empty)
            End If
        End If

        'cek privilege button
        If Request.QueryString("Marketing") = "1" Then
            btnSimpan.Enabled = CekCmdButtonForumMKT
            btnBatal.Enabled = CekCmdButtonForumMKT
            btnChoose.Visible = CekNewMemberBtnForumMKT
        Else
            btnSimpan.Enabled = CekCmdButton
            btnBatal.Enabled = CekCmdButton
            btnChoose.Visible = CekNewMemberBtn
        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        If txtTitle.Text = "" Then
            MessageBox.Show("Lengkapi Data")
            Return
        End If
        Dim success As Boolean

        If (ddlKategori.SelectedItem.Value <> "") Then
            If Request.QueryString("id") = String.Empty Then
                success = InsertForum()
            Else
                success = UpdateForum()
            End If
        Else
            MessageBox.Show("Pilih Kategori Forum!")
        End If
        '        If success Then
        'Response.Redirect("../Forum/FrmForumList.aspx")
        'End If
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        If Request.QueryString("Marketing") = "1" Then
            Response.Redirect("FrmManageForum.aspx?Marketing=1")
        Else
            Response.Redirect("FrmManageForum.aspx")
        End If


    End Sub

    Private Sub btnTambahMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If txtUserID.Text = "" Then
            Exit Sub
        End If

        arlForumMember = sHelper.GetSession("SessArlMember")


        Dim Parameter As String = txtUserID.Text
        Dim ArrayParam() As String = Parameter.Split(";".ToCharArray(), 100)
        Dim i As Integer
        For i = 0 To ArrayParam.Length - 1
            Dim UserArray() As String = ArrayParam(i).Split("-".ToCharArray(), 2)
            Dim objUserInfo As UserInfo
            objUserInfo = New UserInfoFacade(User).RetrievebyUserNameAndDealerCode(UserArray(1), UserArray(0))
            Dim objForumMember As ForumMember = New ForumMember
            objForumMember.UserInfo = objUserInfo

            If Request.QueryString("id") <> String.Empty Then
                objForumMember.Forum = New ForumFacade(User).Retrieve(CInt(Request.QueryString("id")))

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ForumMember), "Forum.ID", MatchType.Exact, Request.QueryString("id")))
                criterias.opAnd(New Criteria(GetType(ForumMember), "UserInfo.ID", MatchType.Exact, objUserInfo.ID))

                Dim arrlist As ArrayList = New ForumMemberFacade(User).Retrieve(criterias)
                If arrlist.Count = 0 Then
                    Dim result As Integer = New ForumMemberFacade(User).InsertTransaction(objForumMember)
                    objForumMember = New ForumMemberFacade(User).Retrieve(result)
                End If
            End If

            arlForumMember.Add(objForumMember)
        Next

        sHelper.SetSession("SessArlMember", arlForumMember)

        BindDataGridMember(CInt(sHelper.GetSession("PageIndex")))

        txtUserID.Text = ""
    End Sub
    Private Sub dtgForumMember_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgForumMember.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            objForumMember = arlForumMember(e.Item.ItemIndex)
            Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            _lblNo.Text = e.Item.ItemIndex + 1 + (dtgForumMember.CurrentPageIndex * dtgForumMember.PageSize)

            Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            _lbtnDelete.CommandArgument = objForumMember.ID

            If Request.QueryString("Marketing") = "1" Then
                _lbtnDelete.Visible = CekNewMemberBtnForumMKT
            Else
                _lbtnDelete.Visible = CekNewMemberBtn
            End If
        End If

    End Sub
    Private Sub dtgForumMember_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgForumMember.ItemCommand
        If e.CommandName = "Delete" Then
            If Request.QueryString("id") <> String.Empty Then
                Dim objForumMemberFacade As ForumMemberFacade = New ForumMemberFacade(User)
                objForumMemberFacade.DeleteFromDB(objForumMemberFacade.Retrieve(CInt(e.CommandArgument)))
            End If

            arlForumMember = sHelper.GetSession("SessArlMember")
            arlForumMember.RemoveAt(e.Item.ItemIndex)

            sHelper.SetSession("SessArlMember", arlForumMember)
            BindDataGridMember(0)
            dtgForumMember.CurrentPageIndex = 0
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Redirect("../Forum/FrmForumList.aspx?isback=1")
    End Sub
    Private Sub dtgForumMember_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgForumMember.SortCommand
        If Request.QueryString("id") <> String.Empty Then
            If e.SortExpression = sHelper.GetSession("SortCol") Then
                If sHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                    sHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
                Else
                    sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
                End If
            End If
            sHelper.SetSession("SortCol", e.SortExpression)
            dtgForumMember.SelectedIndex = -1
            dtgForumMember.CurrentPageIndex = 0
            BindDataGridMember(CInt(sHelper.GetSession("PageIndex")))
        End If
    End Sub
    Private Sub dtgForumMember_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgForumMember.PageIndexChanged
        dtgForumMember.CurrentPageIndex = e.NewPageIndex
        sHelper.SetSession("PageIndex", e.NewPageIndex + 1)
        BindDataGridMember(CInt(sHelper.GetSession("PageIndex")))
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../Forum/FrmForumList.aspx?isBack=1")
    End Sub


End Class

