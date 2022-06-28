#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.BusinessForum

#End Region

Public Class FrmBannedWord
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents valCondition As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgForumBannedWord As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtBannedWord As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtReplacement As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator

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
    Private arlForumBannedWord As ArrayList
    Private objForumBannedWord As ForumBannedWord
    Private sHelper As SessionHelper = New SessionHelper

#End Region

#Region "Custom Method"
    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            arlForumBannedWord = New ForumBannedWordFacade(User).RetrieveActiveList(indexPage + 1, dtgForumBannedWord.PageSize, totalRow, sHelper.GetSession("SortColBannedWord"), sHelper.GetSession("SortDirectionBannedWord"))
            dtgForumBannedWord.DataSource = arlForumBannedWord
            dtgForumBannedWord.VirtualItemCount = totalRow
            If totalRow <= dtgForumBannedWord.PageSize * (dtgForumBannedWord.CurrentPageIndex) Then
                If dtgForumBannedWord.CurrentPageIndex <> 0 Then
                    dtgForumBannedWord.CurrentPageIndex = dtgForumBannedWord.CurrentPageIndex - 1
                    BindDataGrid(dtgForumBannedWord.CurrentPageIndex)
                End If
            End If
            dtgForumBannedWord.DataBind()
        End If
    End Sub
    Private Sub ClearData()
        txtBannedWord.Text = ""
        txtBannedWord.ReadOnly = False
        txtReplacement.Text = ""
        txtReplacement.ReadOnly = False
        ViewState.Add("vsProcess", "Insert")
        dtgForumBannedWord.SelectedIndex = -1
        btnSimpan.Enabled = True
    End Sub
    Private Sub DeleteForumBannedWord(ByVal nID As Integer)
        txtBannedWord.ReadOnly = False
        txtReplacement.ReadOnly = False
        Dim iRecordCount As Integer = 0
        Dim objForumBannedWord As ForumBannedWord = New ForumBannedWordFacade(User).Retrieve(nID)
        Dim objForumBannedWordFacade As ForumBannedWordFacade = New ForumBannedWordFacade(User)

        objForumBannedWordFacade.DeleteFromDB(objForumBannedWord)
        BindDataGrid(dtgForumBannedWord.CurrentPageIndex)
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ForumBanWordView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Forum - Filter Kata")
        End If
    End Sub

    Private cmdButtonPriv As Boolean
    Private Function CekCmdBtnPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ForumBanWordEdit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        cmdButtonPriv = CekCmdBtnPrivilege()
        If Not IsPostBack Then
            ClearData()
            sHelper.SetSession("SortColBannedWord", "BannedWord")
            sHelper.SetSession("SortDirectionBannedWord", Sort.SortDirection.ASC)
            BindDataGrid(0)

            If cmdButtonPriv = False Then
                btnSimpan.Enabled = False
                btnBatal.Enabled = False
            Else
                btnSimpan.Enabled = True
                btnBatal.Enabled = True
            End If
        End If

    End Sub
    Private Sub dtgForumBannedWord_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgForumBannedWord.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arlForumBannedWord Is Nothing) Then
                objForumBannedWord = arlForumBannedWord(e.Item.ItemIndex)
                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgForumBannedWord.CurrentPageIndex * dtgForumBannedWord.PageSize)

                Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                _lbtnDelete.CommandArgument = objForumBannedWord.ID

                Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                _lbtnEdit.CommandArgument = objForumBannedWord.ID

                Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
                lbtnView.CommandArgument = objForumBannedWord.ID

                If cmdButtonPriv = False Then
                    _lbtnDelete.Visible = False
                    _lbtnEdit.Visible = False
                Else
                    _lbtnDelete.Visible = True
                    _lbtnEdit.Visible = True
                End If
            End If
        End If

    End Sub
    Private Sub dtgForumBannedWord_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgForumBannedWord.ItemCommand
        If e.CommandName = "Delete" Then
            DeleteForumBannedWord(e.CommandArgument)
        ElseIf e.CommandName = "Edit" Then           '-- Edit/Update Condition
            txtBannedWord.ReadOnly = False
            txtReplacement.ReadOnly = False
            ViewState.Add("vsProcess", "Edit")
            dtgForumBannedWord.SelectedIndex = e.Item.ItemIndex
            Dim objForumBannedWord As ForumBannedWord = New ForumBannedWordFacade(User).Retrieve(CInt(e.CommandArgument))
            sHelper.SetSession("objedit", objForumBannedWord)
            txtBannedWord.Text = objForumBannedWord.BannedWord
            txtReplacement.Text = objForumBannedWord.Replacement
            btnSimpan.Enabled = True
        ElseIf e.CommandName = "View" Then
            Dim objForumBannedWord As ForumBannedWord = New ForumBannedWordFacade(User).Retrieve(CInt(e.CommandArgument))
            txtBannedWord.Text = objForumBannedWord.BannedWord
            txtBannedWord.ReadOnly = True
            txtReplacement.Text = objForumBannedWord.Replacement
            txtReplacement.ReadOnly = True
            btnSimpan.Enabled = False
            dtgForumBannedWord.SelectedIndex = e.Item.ItemIndex

        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        'Check Code
        Dim Idedit As Integer
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            Idedit = 0
        ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
            Idedit = CType(sHelper.GetSession("objedit"), ForumBannedWord).ID
        End If
        Dim codeIsValid As Integer = New ForumBannedWordFacade(User).ValidateCode(txtBannedWord.Text.Trim, Idedit)

        If codeIsValid > 0 Then
            MessageBox.Show("Kode Sudah Ada !!")
            Return
        End If

        'Transaction
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then  '-- If Condition is Insert

            Dim ObjForumBannedWord As ForumBannedWord = New ForumBannedWord
            ObjForumBannedWord.BannedWord = txtBannedWord.Text.Trim
            ObjForumBannedWord.Replacement = txtReplacement.Text.Trim
            nResult = New ForumBannedWordFacade(User).Insert(ObjForumBannedWord)
        ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
            Dim ObjForumBannedWord As ForumBannedWord = CType(sHelper.GetSession("objedit"), ForumBannedWord)
            ObjForumBannedWord.BannedWord = txtBannedWord.Text.Trim
            ObjForumBannedWord.Replacement = txtReplacement.Text.Trim
            nResult = New ForumBannedWordFacade(User).Update(ObjForumBannedWord)
        End If

        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If

        BindDataGrid(dtgForumBannedWord.CurrentPageIndex)
        ClearData()
    End Sub
    Private Sub dtgForumBannedWord_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgForumBannedWord.SortCommand
        If e.SortExpression = sHelper.GetSession("SortColBannedWord") Then
            If sHelper.GetSession("SortDirectionBannedWord") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirectionBannedWord", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirectionBannedWord", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortColBannedWord", e.SortExpression)
        dtgForumBannedWord.SelectedIndex = -1
        'dtgForumBannedWord.CurrentPageIndex = 0
        BindDataGrid(dtgForumBannedWord.CurrentPageIndex)

    End Sub
    Private Sub dtgForumBannedWord_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgForumBannedWord.PageIndexChanged
        dtgForumBannedWord.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgForumBannedWord.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub
End Class
