#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Buletin
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color


#End Region

#Region ".NET Base Class Namespace Imports"

Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmBuletinUpload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlParent As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMonthPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSubParent As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKeywords As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblUploadedBy As System.Web.UI.WebControls.Label
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtTemp As System.Web.UI.WebControls.TextBox
    Protected WithEvents icValidFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icValidTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents rfvTitle As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvDescription As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlYearPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnTambahMember As System.Web.UI.WebControls.Button
    Protected WithEvents dtgBuletinMember As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgBuletinGroupMember As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblIndikator As System.Web.UI.WebControls.Label
    Protected WithEvents btnRemoveFile As System.Web.UI.WebControls.Button
    Protected WithEvents lblLastUpdate As System.Web.UI.WebControls.Label
    Protected WithEvents btnTambahGroup As System.Web.UI.WebControls.Button
    Protected WithEvents lblMessage As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Private sHelper As New SessionHelper
    Dim arlBuletin As New ArrayList
    Private objBuletinMember As BuletinMember
    Private objBuletinGroupMember As BuletinGroupMember
    Private mUploadAccess As Boolean = False
#End Region

#Region "Event Handler"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.BuletinUpload_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=BULETIN - Upload Buletin")
        End If
        mUploadAccess = SecurityProvider.Authorize(Context.User, SR.BuletinUploadAddUser_Privilege)
        Me.btnUpload.Visible = SecurityProvider.Authorize(Context.User, SR.BuletinUpload_Privilege)
        Me.btnUpload.Attributes.Add("onclick", New CommonFunction().PreventDoubleClick(btnUpload))
        'Me.btnTambahMember.Enabled = SecurityProvider.Authorize(context.User, SR.BuletinUploadAddUser_Privilege)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If Not Page.IsPostBack Then
            PopulatePeriodMonth()
            PopulateYearPeriod()
            PopulateParent()
            PopulateSelectionCategory()
            ClearControl()

            If Not IsNothing(Request.QueryString("id")) Then
                ' update
                Dim idIsSelf As Integer = CInt(Request.QueryString("isSelf"))
                If idIsSelf = 0 Then
                    Me.btnBack.Visible = False
                Else
                    Me.btnBack.Visible = True
                End If

                ViewState.Add("id", Request.QueryString("id"))
                ViewState.Add("backurl", Request.QueryString("backurl"))
                Dim idBuletin As Integer = CInt(Request.QueryString("id"))
                'Me.btnTambahMember.Visible = True
                Me.btnTambahMember.Enabled = mUploadAccess
                'Me.btnTambahGroup.Visible = True
                Me.btnTambahGroup.Enabled = mUploadAccess
                btnUpload.Text = "Simpan"
                Me.btnTambahMember.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpForumMember.aspx?isBuletin=Yes" & "&id=" & idBuletin & "','',600,600,GetIdMemberSelection);")
                Me.btnTambahGroup.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpUserGroup.aspx?isBuletin=1" & "&id=" & idBuletin & "','',600,600,GetUserGroupSelection);")
                BuletinToControl(CInt(Request.QueryString("id")))
                sHelper.SetSession("SortCol", "UserGroup.Code")
                sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
                sHelper.SetSession("arlBuletin", arlBuletin)
                BindDataGridGroupMember(0)

            Else
                ' insert
                Me.btnBack.Visible = False
                Me.lblIndikator.Visible = False
                Me.btnRemoveFile.Visible = False
            End If
        Else
            If Not IsNothing(Request.QueryString("id")) Then
                'If txtTemp.Text = "1" Then
                'BindDataGridGroupMember(0)
                'End If
            End If
        End If

    End Sub

    Private Sub ddlParent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlParent.SelectedIndexChanged
        If ddlParent.SelectedValue <> "0" Then
            PopulateSelectionCategory()
        Else
            PopulateParent()
            PopulateSelectionCategory()
        End If
    End Sub

    Private Function IsInputValid() As String
        Dim sMsg As String = ""
        'If Me.txtYearPeriod.Text.Trim = String.Empty Then
        '    sMsg = sMsg & "Periode (tahun) wajib diisi\n"
        'End If
        If Me.txtTitle.Text.Trim = String.Empty Then
            sMsg = sMsg & "Nama wajib diisi\n"
        End If
        If Me.txtDescription.Text.Trim = String.Empty Then
            sMsg = sMsg & "Deskripsi wajib diisi\n"
        End If
        'If Me.txtKeywords.Text.Trim = String.Empty Then
        '    sMsg = sMsg & "Kata Kunci wajib diisi\n"
        'End If
        If Me.icValidFrom.Value > Me.icValidTo.Value Then
            sMsg = sMsg & "Tanggal Valid Awal tidak boleh lebih besar dari Tanggal Valid Akhir\n"
        End If
        If IsNothing(ViewState("id")) Then
            If Me.fileUpload.Value = String.Empty Then
                sMsg = sMsg & "File wajib dipilih\n"
            End If
        End If
        Return sMsg
    End Function

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If Not Page.IsValid Then  '-- Postback validation
            Exit Sub
        End If

        ' Modified by Ikhsan, 12 Agustus 2008
        ' Requested by Rina, as Part of CR
        ' To eliminate validatiod of Kata Kunci
        ' ------------------------------------------------------------------------
        'If txtKeywords.Text.Trim = String.Empty Then
        '    MessageBox.Show("Masukkan kata kunci")
        'Else
        ' ------------------------------------------------------------------------
        Dim sMsg As String = ""
        sMsg = IsInputValid()
        If sMsg = "" Then
            Dim objBuletin As Buletin
            objBuletin = ControlToBuletin()

            If Me.fileUpload.Value = String.Empty Then
                Try
                    Dim objBuletinFac As BuletinFacade = New BuletinFacade(User)
                    If objBuletinFac.Update(objBuletin) = -1 Then
                        MessageBox.Show(SR.UpdateFail)
                    Else
                        MessageBox.Show(SR.UpdateSucces)
                        Exit Sub
                        'ClearControl()
                    End If
                Catch ex As Exception
                    MessageBox.Show(SR.UpdateFail)
                End Try
            End If

            If Not IsNothing(objBuletin) Then
                Dim fInfo As New FileInfo((Me.fileUpload.PostedFile.FileName))

                Dim strFileName = fInfo.Name

                If strFileName.Length > 50 Then
                    lblMessage.Visible = True
                    Exit Sub
                Else
                    lblMessage.Visible = False
                End If

                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                Dim objBuletinCatFac As BuletinCategoryFacade = New BuletinCategoryFacade(User)

                If Not IsNothing(ViewState("id")) Then
                    ' update
                    Try
                        If imp.Start() Then
                            If CStr(ViewState("oldCategoryID")) <> Me.ddlSubParent.SelectedValue Then
                                ' kategori berubah
                                If (Not Me.fileUpload.PostedFile Is Nothing) And Me.fileUpload.PostedFile.ContentLength > 0 Then
                                    ' file diganti, hapus file lama, simpan file baru
                                    Dim CompletedFile As String = GetCompletePathOfOldFile()
                                    If New FileInfo(CompletedFile).Exists Then
                                        System.IO.File.Delete(CompletedFile)
                                    End If

                                    Dim NewFileLocation As String = GetCompletePathOfNewFile(objBuletin.BuletinCategory)

                                    If Not IO.Directory.Exists(NewFileLocation) Then
                                        IO.Directory.CreateDirectory(Path.GetDirectoryName(NewFileLocation))
                                    End If

                                    Dim objUpload As New UploadToWebServer
                                    objUpload.Upload(Me.fileUpload.PostedFile.InputStream, NewFileLocation)

                                    ' Copy To Another Server
                                    'CopytoAnOtherWebServer(New FileInfo(NewFileLocation), objBuletin.BuletinCategory)

                                Else
                                    ' file tidak diganti, move file tsb
                                    Dim NewFileLocation As String = GetCompletePathOfNewFile(objBuletin.BuletinCategory)
                                    System.IO.File.Move(GetCompletePathOfOldFile, NewFileLocation & Path.GetFileName(GetCompletePathOfOldFile))

                                    ' Copy To Another Server
                                    'CopytoAnOtherWebServer(New FileInfo(NewFileLocation), objBuletin.BuletinCategory)
                                End If
                            Else
                                ' kategori tetap
                                If (Not Me.fileUpload.PostedFile Is Nothing) And Me.fileUpload.PostedFile.ContentLength > 0 Then
                                    ' jika file diganti, hapus file lama, simpan file baru
                                    Dim CompletedFiles As String = GetCompletePathOfOldFile()
                                    If New FileInfo(CompletedFiles).Exists Then
                                        System.IO.File.Delete(CompletedFiles)
                                    End If

                                    Dim NewFileLocation As String = GetCompletePathOfNewFile(objBuletin.BuletinCategory)

                                    If Not IO.Directory.Exists(NewFileLocation) Then
                                        IO.Directory.CreateDirectory(Path.GetDirectoryName(NewFileLocation))
                                    End If

                                    Dim objUpload As New UploadToWebServer
                                    objUpload.Upload(Me.fileUpload.PostedFile.InputStream, NewFileLocation)

                                    ' Copy To Another Server
                                    'CopytoAnOtherWebServer(New FileInfo(NewFileLocation), objBuletin.BuletinCategory)

                                Else
                                    ' file tidak diganti, tidak melakukan apa-apa
                                End If
                            End If

                            imp.StopImpersonate()
                            imp = Nothing

                            Dim objBuletinFac As BuletinFacade = New BuletinFacade(User)
                            If objBuletinFac.Update(objBuletin) = -1 Then
                                MessageBox.Show(SR.UpdateFail)
                            Else
                                MessageBox.Show(SR.UpdateSucces)
                                'ClearControl()
                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show(SR.UpdateFail)
                    End Try
                Else
                    ' insert
                    Dim objUserInfo As UserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)

                    If (Not Me.fileUpload.PostedFile Is Nothing) And (Me.fileUpload.PostedFile.ContentLength > 0) Then
                        Try
                            If imp.Start() Then
                                Dim NewFileLocation As String = GetCompletePathOfNewFile(objBuletin.BuletinCategory)

                                If Not IO.Directory.Exists(NewFileLocation) Then
                                    IO.Directory.CreateDirectory(Path.GetDirectoryName(NewFileLocation))
                                End If

                                Dim objUpload As New UploadToWebServer
                                objUpload.Upload(Me.fileUpload.PostedFile.InputStream, NewFileLocation)

                                ' Copy To Another Server
                                'CopytoAnOtherWebServer(New FileInfo(NewFileLocation), objBuletin.BuletinCategory)

                                imp.StopImpersonate()
                                imp = Nothing

                                Dim objBuletinFac As BuletinFacade = New BuletinFacade(User)
                                Dim returnValID As Integer = objBuletinFac.Insert(objBuletin)
                                If returnValID = -1 Then
                                    MessageBox.Show(SR.UploadFail(Me.fileUpload.Value.Replace("\", "\\")))
                                Else
                                    MessageBox.Show(SR.UploadSucces(Me.fileUpload.Value.Replace("\", "\\")))
                                    ClearControl()

                                    Dim buletinID As Integer = returnValID
                                    Dim objBuletinAR As Buletin = New BuletinFacade(User).Retrieve(buletinID)

                                    Dim objBulHistory As New BuletinHistory
                                    objBulHistory.Buletin = objBuletinAR
                                    Dim iResult As Integer = New BuletinHistoryFacade(User).Insert(objBulHistory)

                                    'automatic register UserLogin
                                    'Dim buletinID As Integer = returnValID
                                    'Dim objBuletinAR As Buletin = New BuletinFacade(User).Retrieve(buletinID)

                                    'Dim objBulMember As New BuletinMember
                                    'objBulMember.Buletin = objBuletinAR
                                    'objBulMember.UserInfo = objUserInfo
                                    'Dim iResult As Integer = New BuletinMemberFacade(User).Insert(objBulMember, objBuletinAR, objUserInfo)

                                    'additional 
                                    Response.Redirect("FrmBuletinUpload.aspx?id=" & returnValID & "&isSelf=0", False)
                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show(SR.UploadFail(Me.fileUpload.Value.Replace("\", "\\")))
                        End Try
                    Else
                        MessageBox.Show(SR.FileNotSelected)
                    End If
                End If
            Else
                MessageBox.Show(SR.UploadFail(Me.fileUpload.Value.Replace("\", "\\")))
            End If

        Else
            MessageBox.Show(sMsg)
        End If
        'End If
    End Sub

    Private Sub CopytoAnOtherWebServer(ByVal finfo As FileInfo, ByVal objBuletinCategory As BuletinCategory)
        Dim helper As FileHelper = New FileHelper
        Dim otherFolder As String = GetCategoryFolder(objBuletinCategory)
        helper.TransferToListWebServer(finfo, otherFolder, True, "BuletinDirectory")
    End Sub


    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        'Server.Transfer(CStr(viewstate("backurl")))
        Response.Redirect("FrmBuletinManage.aspx?isBack=true")
        ClearControl()
    End Sub
#End Region

#Region "Custom Method"

    Private Sub ClearControl()
        'Me.txtYearPeriod.Text = String.Empty
        Me.ddlYearPeriod.SelectedIndex = 0
        Me.txtTitle.Text = String.Empty
        Me.txtDescription.Text = String.Empty
        Me.txtKeywords.Text = String.Empty
        Me.icValidFrom.Value = Date.Now
        Me.icValidTo.Value = Date.Now
        Me.ddlMonthPeriod.SelectedValue = Date.Now.Month.ToString
    End Sub

    Private Function GetCompletePathOfNewFile(ByVal objBuletinCategory As BuletinCategory) As String
        Dim SrcFile As String = Path.GetFileName(Me.fileUpload.PostedFile.FileName)
        Dim DestFolder As String = KTB.DNet.Lib.WebConfig.GetValue("BuletinDestFileDirectory")
        Dim DestFile As String = DestFolder & "\" & SrcFile
        Dim objBuletinCatFac As BuletinCategoryFacade = New BuletinCategoryFacade(User)

        Dim listParentCategory As ArrayList = objBuletinCatFac.RetrieveAllParentCategory(objBuletinCategory)
        If listParentCategory.Count > 0 Then
            For i As Integer = 0 To listParentCategory.Count - 1
                DestFolder = DestFolder & "\" & CType(listParentCategory.Item(listParentCategory.Count - 1 - i), BuletinCategory).Code
            Next
        End If
        DestFolder = DestFolder & "\" & objBuletinCategory.Code
        'Comment by heru for san
        'DestFolder = Server.MapPath("") & "\..\" & DestFolder
        DestFolder = KTB.DNet.Lib.WebConfig.GetValue("SAN") & DestFolder
        DestFile = DestFolder & "\" & SrcFile
        Return DestFile
    End Function

    Private Function GetCategoryFolder(ByVal objBuletinCategory As BuletinCategory) As String

        Dim DestFolder As String = String.Empty
        Dim objBuletinCatFac As BuletinCategoryFacade = New BuletinCategoryFacade(User)

        Dim listParentCategory As ArrayList = objBuletinCatFac.RetrieveAllParentCategory(objBuletinCategory)
        If listParentCategory.Count > 0 Then
            For i As Integer = 0 To listParentCategory.Count - 1
                If DestFolder = String.Empty Then
                    DestFolder = CType(listParentCategory.Item(listParentCategory.Count - 1 - i), BuletinCategory).Code
                Else
                    DestFolder = DestFolder & "\" & CType(listParentCategory.Item(listParentCategory.Count - 1 - i), BuletinCategory).Code
                End If
            Next
        End If
        DestFolder = DestFolder & "\" & objBuletinCategory.Code

        Return DestFolder
    End Function

    Private Function GetCompletePathOfOldFile() As String
        Dim objBuletinCatFac As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        Dim oldFile As String = KTB.DNet.Lib.WebConfig.GetValue("BuletinDestFileDirectory")
        Dim objOldBuletinCat As BuletinCategory

        objOldBuletinCat = objBuletinCatFac.Retrieve(CInt(ViewState("oldCategoryID")))

        Dim oldlistParentCategory As ArrayList = objBuletinCatFac.RetrieveAllParentCategory(objOldBuletinCat)
        If oldlistParentCategory.Count > 0 Then
            For i As Integer = 0 To oldlistParentCategory.Count - 1
                oldFile = oldFile & "\" & CType(oldlistParentCategory.Item(oldlistParentCategory.Count - 1 - i), BuletinCategory).Code
            Next
        End If

        oldFile = oldFile & "\" & objOldBuletinCat.Code
        oldFile = Server.MapPath("") & "\..\" & oldFile & "\" & CStr(ViewState("oldFileName"))

        Return oldFile
    End Function

    Private Function ControlToBuletin() As Buletin
        'add by Ery MN
        Dim objUserInfo As UserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim updModel As String = objUserInfo.Dealer.DealerCode & "-" & objUserInfo.UserName
        '--------------------------
        Dim objBuletin As Buletin
        Dim objBuletinCat As BuletinCategory

        Dim objBuletinFac As BuletinFacade
        Dim objBuletinCatFac As BuletinCategoryFacade

        If Not IsNothing(ViewState("id")) Then
            ' update
            objBuletinFac = New BuletinFacade(User)
            objBuletin = objBuletinFac.Retrieve(CInt(ViewState("id")))
            If Not IsNothing(objBuletin) Then
                If CStr(ViewState("oldCategoryID")) <> Me.ddlSubParent.SelectedValue Then
                    objBuletinCatFac = New BuletinCategoryFacade(User)
                    objBuletinCat = objBuletinCatFac.Retrieve(CInt(Me.ddlSubParent.SelectedValue))
                    objBuletinCat.MarkLoaded()
                    objBuletin.BuletinCategory = objBuletinCat
                End If
                If (Not Me.fileUpload.PostedFile Is Nothing) And Me.fileUpload.PostedFile.ContentLength > 0 Then
                    objBuletin.FileName = Path.GetFileName(Me.fileUpload.PostedFile.FileName)
                End If
            Else
                MessageBox.Show(SR.DataNotFound("Buletin"))
                Return Nothing
            End If
        Else
            ' insert
            objBuletin = New Buletin
            objBuletinCatFac = New BuletinCategoryFacade(User)
            objBuletinCat = objBuletinCatFac.Retrieve(CInt(Me.ddlSubParent.SelectedValue))
            If Not IsNothing(objBuletinCat) Then
                objBuletinCat.MarkLoaded()
                objBuletin.BuletinCategory = objBuletinCat
                objBuletin.FileName = Path.GetFileName(Me.fileUpload.PostedFile.FileName)
            Else
                MessageBox.Show(SR.DataNotFound("Kategori Buletin/Sub Kategori Buletin"))
                Return Nothing
            End If
        End If

        objBuletin.Title = Me.txtTitle.Text
        objBuletin.Description = Me.txtDescription.Text
        objBuletin.ValidFrom = Me.icValidFrom.Value
        objBuletin.ValidTo = Me.icValidTo.Value
        objBuletin.Keywords = Me.txtKeywords.Text
        objBuletin.Status = enumStatusBuletin.StatusBuletin.Aktif

        'update by Ery MN
        'objBuletin.UploadBy = User.Identity.Name
        objBuletin.UploadBy = updModel
        objBuletin.UploadDate = Date.Today

        objBuletin.PeriodMonth = CType(Me.ddlMonthPeriod.SelectedValue, Byte)
        'objBuletin.PeriodYear = CInt(Me.txtYearPeriod.Text)
        objBuletin.PeriodYear = CInt(Me.ddlYearPeriod.SelectedValue)
        objBuletin.RowStatus = 0

        Return objBuletin
    End Function

    Private Sub BuletinToControl(ByVal nBuletinID As Integer)
        Dim objBuletin As Buletin
        Dim objBuletinFac As BuletinFacade = New BuletinFacade(User)
        objBuletin = objBuletinFac.Retrieve(nBuletinID)

        Dim objBuletinCat As BuletinCategory
        Dim objBuletinCatFac As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        objBuletinCat = objBuletinCatFac.Retrieve(CInt(objBuletin.BuletinCategory.TopParent))
        Me.ddlParent.SelectedValue = objBuletinCat.ID.ToString


        PopulateSelectionCategory()
        Me.ddlSubParent.SelectedValue = CStr(objBuletin.BuletinCategory.ID)

        Me.txtTitle.Text = objBuletin.Title
        Me.txtDescription.Text = objBuletin.Description
        Me.icValidFrom.Value = objBuletin.ValidFrom
        Me.icValidTo.Value = objBuletin.ValidTo
        Me.txtKeywords.Text = objBuletin.Keywords
        Me.ddlMonthPeriod.SelectedValue = CStr(objBuletin.PeriodMonth)
        'Me.txtYearPeriod.Text = CStr(objBuletin.PeriodYear)
        Me.ddlYearPeriod.SelectedValue = CStr(objBuletin.PeriodYear)

        'update by Ery
        Me.lblUploadedBy.Text = objBuletin.UploadBy.ToString
        Me.lblLastUpdate.Text = objBuletin.UploadDate.ToString("dd/MM/yyyy")

        ViewState.Add("oldCategoryID", objBuletin.BuletinCategory.ID)
        ViewState.Add("oldFileName", objBuletin.FileName)

        'add indikator untuk mengetahui file ada apa tidak
        If objBuletin.FileName = "" Then
            lblIndikator.Visible = False
            btnRemoveFile.Enabled = False
        Else
            lblIndikator.Visible = True
            btnRemoveFile.Enabled = True
        End If
    End Sub

    'Private Sub BindDataGridMember(ByVal indexPage As Integer)
    '    Dim totalRow As Integer = 0
    '    If (indexPage >= 0) Then

    '        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '        'criterias.opAnd(New Criteria(GetType(BuletinGroupMember), "Buletin.ID", MatchType.Exact, CInt(ViewState("id"))))
    '        ''New ForumMemberFacade(User).RetrieveByCriteria(criterias, 1, dtgForumMember.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"))
    '        ''arlBuletin = New BuletinGroupMemberFacade(User).RetrieveActiveList(indexPage, dtgBuletinGroupMember.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"), criterias)
    '        ''dtgBuletinMember.DataSource = arlBuletin
    '        ''dtgBuletinMember.VirtualItemCount = totalRow
    '        ''dtgBuletinMember.DataBind()
    '        ''sHelper.SetSession("arlBuletin", arlBuletin)
    '        'If arlBuletin.Count > 0 Then
    '        '    'dtgBuletinGrouMember.Visible = True
    '        'End If
    '    End If
    'End Sub
    Private Sub BindDataGridGroupMember(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BuletinGroupMember), "Buletin.ID", MatchType.Exact, CInt(ViewState("id"))))
            'New ForumMemberFacade(User).RetrieveByCriteria(criterias, 1, dtgForumMember.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"))
            arlBuletin = New BuletinGroupMemberFacade(User).RetrieveActiveList(indexPage, dtgBuletinGroupMember.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"), criterias)
            dtgBuletinGroupMember.DataSource = arlBuletin
            dtgBuletinGroupMember.VirtualItemCount = totalRow
            dtgBuletinGroupMember.DataBind()
            sHelper.SetSession("arlBuletin", arlBuletin)
            If arlBuletin.Count > 0 Then
                dtgBuletinGroupMember.Visible = True
            End If
        End If
    End Sub

    Private Sub PopulatePeriodMonth()
        For Each item As ListItem In CType(LookUp.ArrayMonth, IEnumerable)
            Me.ddlMonthPeriod.Items.Add(item)
        Next
    End Sub

    Private Sub PopulateYearPeriod()
        Dim currentYear As Integer = Date.Now.Year
        Dim nextYear As Integer = Date.Now.Year + 1

        'Rina Request 18 Feb 08
        'ddlYearPeriod.Items.Add(currentYear)
        'ddlYearPeriod.Items.Add(nextYear)
        For i As Integer = 2004 To Date.Now.Year + 1
            ddlYearPeriod.Items.Add(i.ToString)
        Next

    End Sub

    Private Sub PopulateParent()
        ddlParent.Items.Clear()
        Dim org As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim _BuletinFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        Dim list As ArrayList = _BuletinFacade.RetrieveParentList(CInt(org.Title))
        Dim li As ListItem

        'add
        li = New ListItem
        li.Text = "Silahkan Pilih"
        li.Value = "0"
        ddlParent.Items.Add(li)

        If list.Count > 0 Then
            For Each item As BuletinCategory In list
                If item.Status = 0 Then
                    li = New ListItem
                    li.Text = item.Code
                    li.Value = item.ID.ToString
                    ddlParent.Items.Add(li)
                End If
            Next
        End If
    End Sub

    Private Sub PopulateSelectionCategory()
        ddlSubParent.Items.Clear()
        Dim org As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim _BuletinFacade As BuletinCategoryFacade = New BuletinCategoryFacade(User)
        If Not Me.ddlParent.SelectedValue.Trim = String.Empty Then
            Dim list As ArrayList = _BuletinFacade.PopulateListView(CInt(Me.ddlParent.SelectedValue), CInt(org.Title))
            Dim _item As ListItem
            Dim space As String = String.Empty

            _item = New ListItem
            _item.Text = "Silahkan Pilih"
            _item.Value = "0"
            ddlSubParent.Items.Add(_item)

            If list.Count > 0 Then
                For Each item As BuletinCategory In list
                    space = BuildLeadingSpace(item.Leveling)
                    _item = New ListItem
                    _item.Value = item.ID.ToString
                    _item.Text = space & item.Code
                    ddlSubParent.Items.Add(_item)
                    space = String.Empty
                Next
            End If
        End If
    End Sub



    Private Function BuildLeadingSpace(ByVal count As Integer) As String
        Dim space As String = String.Empty
        If count > 1 Then
            For i As Integer = 0 To count - 2
                ' space += " . . . "
                space += Chr(3) & Chr(3) & Chr(3)
            Next
            space = space & "-- "
        End If
        Return space
    End Function

    Protected Overrides Sub Render(ByVal writer As UI.HtmlTextWriter)
        Dim stream As MemoryStream = New MemoryStream
        Dim textW As TextWriter = New StreamWriter(stream)
        Dim newWriter As UI.HtmlTextWriter = New UI.HtmlTextWriter(textW)
        MyBase.Render(newWriter)
        newWriter.Close()
        textW.Close()
        Dim str As String = System.Text.Encoding.UTF8.GetString(stream.GetBuffer).Replace(Chr(3), "&nbsp;")

        'Dim intSubKategori As Integer = str.IndexOf("<select name=""ddlSubParent"" id=""ddlSubParent"">")
        'Dim strSubKategori As String = str.Substring(intSubKategori)
        'strSubKategori = strSubKategori.Substring(0, strSubKategori.IndexOf("</select>") + 9)
        'str.Replace(strSubKategori, "")
        'str.Insert(intSubKategori, strSubKategori.Replace(Chr(3), "&nbsp;"))

        writer.Flush()
        writer.Write(str)
    End Sub



#End Region

    Private Sub AssignPrivilege()
    End Sub

    'Private Sub dtgBuletinMember_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgBuletinMember.PageIndexChanged
    '    dtgBuletinMember.CurrentPageIndex = e.NewPageIndex
    '    BindDataGridMember(e.NewPageIndex + 1)
    'End Sub

    Private Sub dtgBuletinGroupMember_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgBuletinGroupMember.PageIndexChanged
        dtgBuletinGroupMember.CurrentPageIndex = e.NewPageIndex
        BindDataGridGroupMember(dtgBuletinGroupMember.CurrentPageIndex + 1)
    End Sub

    'Private Sub dtgBuletinMember_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgBuletinMember.SortCommand
    '    If e.SortExpression = sHelper.GetSession("SortCol") Then
    '        If sHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
    '            sHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
    '        Else
    '            sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
    '        End If
    '    End If
    '    sHelper.SetSession("SortCol", e.SortExpression)
    '    dtgBuletinMember.SelectedIndex = -1
    '    'dtgBuletinMember.CurrentPageIndex = 0
    '    BindDataGridMember(dtgBuletinMember.CurrentPageIndex)
    'End Sub

    Private Sub dtgBuletinGroupMember_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgBuletinGroupMember.SortCommand
        If e.SortExpression = sHelper.GetSession("SortCol") Then
            If sHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortCol", e.SortExpression)
        dtgBuletinGroupMember.SelectedIndex = -1
        dtgBuletinGroupMember.CurrentPageIndex = 0
        BindDataGridGroupMember(dtgBuletinGroupMember.CurrentPageIndex)
    End Sub

    'Private Sub dtgBuletinMember_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgBuletinMember.ItemCommand
    '    If e.CommandName = "Delete" Then
    '        If ViewState("id") <> String.Empty Then
    '            Dim objBuletinMemberFacade As BuletinMemberFacade = New BuletinMemberFacade(User)
    '            objBuletinMemberFacade.DeleteFromDB(objBuletinMemberFacade.Retrieve(CInt(e.CommandArgument)))
    '        End If

    '        arlBuletin = CType(sHelper.GetSession("arlBuletin"), ArrayList)
    '        arlBuletin.RemoveAt(e.Item.ItemIndex)

    '        sHelper.SetSession("arlBuletin", arlBuletin)
    '        BindDataGridMember(0)
    '        dtgBuletinMember.CurrentPageIndex = 0
    '    End If
    'End Sub

    Private Sub dtgBuletinGroupMember_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgBuletinGroupMember.ItemCommand
        If e.CommandName = "Delete" Then
            If ViewState("id") <> String.Empty Then
                Dim objBuletinGroupMemberFacade As BuletinGroupMemberFacade = New BuletinGroupMemberFacade(User)
                objBuletinGroupMemberFacade.DeleteFromDB(objBuletinGroupMemberFacade.Retrieve(CInt(e.CommandArgument)))
            End If

            arlBuletin = CType(sHelper.GetSession("arlBuletin"), ArrayList)
            arlBuletin.RemoveAt(e.Item.ItemIndex)

            sHelper.SetSession("arlBuletin", arlBuletin)
            BindDataGridGroupMember(0)
            dtgBuletinGroupMember.CurrentPageIndex = 0
        End If
    End Sub

    'Private Sub dtgBuletinMember_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBuletinMember.ItemDataBound
    '    If e.Item.ItemIndex <> -1 Then
    '        Dim lblNo As Label = CType(e.Item.FindControl("lblNO"), Label)
    '        Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
    '        lblNo.Text = CType(e.Item.ItemIndex + 1 + (dtgBuletinMember.CurrentPageIndex * dtgBuletinMember.PageSize), String)

    '        objBuletinMember = CType(arlBuletin(e.Item.ItemIndex), BuletinMember)

    '        Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
    '        _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
    '        _lbtnDelete.CommandArgument = objBuletinMember.ID.ToString
    '        _lbtnDelete.Visible = mUploadAccess
    '    End If
    'End Sub

    Private Sub dtgBuletinGroupMember_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBuletinGroupMember.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNO"), Label)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lblNo.Text = CType(e.Item.ItemIndex + 1 + (dtgBuletinGroupMember.CurrentPageIndex * dtgBuletinGroupMember.PageSize), String)

            objBuletinGroupMember = CType(arlBuletin(e.Item.ItemIndex), BuletinGroupMember)

            Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            _lbtnDelete.CommandArgument = objBuletinGroupMember.ID.ToString
            _lbtnDelete.Visible = mUploadAccess
        End If
    End Sub

    Private Sub btnRemoveFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFile.Click
        Dim idBuletin As Integer = CInt(Request.QueryString("id"))
        Dim objBuletin As Buletin
        Dim objBuletinFac As BuletinFacade = New BuletinFacade(User)
        objBuletin = objBuletinFac.Retrieve(idBuletin)
        objBuletin.FileName = ""

        If objBuletinFac.Update(objBuletin) <> -1 Then
            lblIndikator.Visible = False
            btnRemoveFile.Enabled = False
        End If
    End Sub

End Class
