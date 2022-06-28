Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.Helper
Imports Microsoft.Practices.EnterpriseLibrary.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security
Imports Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
Imports Microsoft.Practices.EnterpriseLibrary.Security.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database.Authentication.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database
Imports System.Text
Imports KTB.DNet.Security
Imports System.Security.Principal


Public Class frmUserInfoDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeOrganisasi As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubOrgCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubOrgName As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeOrganisasiValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaOrganisasi As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaOrganisasiValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblID As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblIDValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaLogin As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaLoginValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKataKunciBaru As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKataKunciBaru As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKonfirmasiKataKunciBaru As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKonfirmasiKataKunciBaru As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPertanyaan As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents txtPertanyaan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblJawaban As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents txtJawaban As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNamaDepan As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNamaDepan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNamaBelakang As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNamaBelakang As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPosisi As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents txtPosisi As System.Web.UI.WebControls.TextBox
    Protected WithEvents lbTelepon As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents txtTelepon As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblHP As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label
    Protected WithEvents txtHP As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents Label21 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents CompareValidator1 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents btnReset As System.Web.UI.WebControls.Button
    Protected WithEvents btnLanjut As System.Web.UI.WebControls.Button
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents dtgRole As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblKataKunciLama As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKataKunciLama As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlAdditionalInformation As System.Web.UI.WebControls.Panel
    Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
    Protected WithEvents divAdditionalInformation As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents rowKataKunciLama As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents Label22 As System.Web.UI.WebControls.Label
    Protected WithEvents opJawaban As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents rowKonfimasiKataKunci As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents rowPertanyaan As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents rowKatakunciBaru As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lbl As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmailValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblHPValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPosition As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbDeletePhoto As System.Web.UI.WebControls.CheckBox
    Protected WithEvents photoSrc As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents Label23 As System.Web.UI.WebControls.Label
    Protected WithEvents photoView As System.Web.UI.WebControls.Image

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sessionHelper As New sessionHelper
    Private objUser As UserInfo
    Private confContext As ConfigurationContext
    Private dbAuthenticationProvider As DbAuthenticationProviderData
    Private userRoleMgr As UserRoleManager
    Private hashProvider As IHashProvider
    Private identity As IIdentity

#Region "Cek Privilege"
    Dim bCekDDLPriv As Boolean = SecurityProvider.Authorize(context.User, SR.Posisi_jabatan_lihat_Privilege)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            BindDropDownLists()
            If Request.QueryString("type") = "0" Then 'From Login Page... New User
                SetNewUserMode()
            ElseIf Request.QueryString("type") = "1" Then ' From Forget Password Page
                SetForgotPasswordMode()
            ElseIf Request.QueryString("type") = "3" Then 'From Edit By KTB
                SetEditUserInfoByKTBMode()
            Else 'From Ubah Data
                SetEditUserInfoMode()
            End If
            If Request.QueryString("id") = String.Empty Then
                RetrieveDealerInfo()
                RetrieveUserInfoFromSession()
            Else
                RetrieveUserInfoFromQueryString()
                RetrieveDealerInfoFromQueryString()
            End If
        End If
        SetPasswordcontrol()
        ddlPosition.Enabled = bCekDDLPriv
    End Sub

    Private Sub SetPasswordcontrol()
        rowKataKunciLama.Visible = False
        rowKatakunciBaru.Visible = False
        rowKonfimasiKataKunci.Visible = False
        rowPertanyaan.Visible = False
        opJawaban.Visible = False
        txtHP.ReadOnly = True
        txtEmail.ReadOnly = True
    End Sub

    Private Sub SetEditUserInfoMode()
        divAdditionalInformation.Visible = False
        btnKembali.Visible = False
        btnSimpan.Visible = False
        btnReset.Visible = False
        btnLanjut.Visible = False
    End Sub

    Private Sub SetNewUserMode()
        divAdditionalInformation.Visible = True
        btnKembali.Visible = False
        btnSimpan.Visible = False
        btnSave.Visible = False
    End Sub

    Private Sub SetEditUserInfoByKTBMode()
        rowKataKunciLama.Visible = False
        divAdditionalInformation.Visible = False
        btnReset.Visible = False
        btnLanjut.Visible = False
        btnSave.Visible = False
        opJawaban.Visible = False
    End Sub

    Private Sub SetForgotPasswordMode()
        RequiredFieldValidator1.Enabled = True
        RequiredFieldValidator2.Enabled = True
        Label9.Visible = True
        Label1.Visible = True
        rowKataKunciLama.Visible = False
        divAdditionalInformation.Visible = False
        btnReset.Visible = False
        btnLanjut.Visible = False
        btnSave.Visible = False
    End Sub

    Private Sub RetrieveUserInfoFromSession()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "UserName", MatchType.Exact, User.Identity.Name.Substring(6)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "Dealer.ID", MatchType.Exact, CInt(User.Identity.Name.Substring(0, 6))))
        Dim ArlUserInfo As ArrayList = New UserInfoFacade(User).Retrieve(criterias)
        objUser = ArlUserInfo(0)
        sessionHelper.SetSession("User", objUser)
        BindHeaderToForm()
        BindDetailToGrid()
    End Sub

    Private Sub RetrieveUserInfoFromQueryString()
        objUser = New UserInfoFacade(User).Retrieve(CInt(Request.QueryString("id")))
        sessionHelper.SetSession("User", objUser)
        BindHeaderToForm()
        BindDetailToGrid()
    End Sub

    Private Sub BindHeaderToForm()
        'txtPertanyaan.Text = objUser.Question
        'If Not objUser.Answer Is Nothing Then
        '    txtJawaban.Text = DNetEncryption.SymmetricDecrypt(objUser.Answer, objUser.Question)
        'End If
        txtNamaDepan.Text = objUser.FirstName
        txtNamaBelakang.Text = objUser.LastName

        'txtPosisi.Text = objUser.JobPositionOld
        Try
            ddlPosition.SelectedValue = objUser.JobPosition.ID
        Catch ex As Exception
            ddlPosition.SelectedIndex = -1
        End Try


        txtTelepon.Text = objUser.Telephone
        lblEmailValue.Text = objUser.Email
        lblHPValue.Text = objUser.HandPhone
        txtEmail.Text = objUser.Email
        txtHP.Text = objUser.HandPhone
        lblStatusValue.Text = CType(objUser.UserStatus, EnumUserStatus.UserStatus).ToString()
        photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & objUser.ID & "&type=" & "UserInfo"

    End Sub

    Private Sub BindDetailToGrid()
        sessionHelper.SetSession("RoleList", objUser.UserRoles)
        dtgRole.DataSource = objUser.UserRoles
        dtgRole.DataBind()
    End Sub

    Private Sub ShortBindDetailToGrid()
        Dim _role As ArrayList = CType(sessionHelper.GetSession("RoleList"), ArrayList)
        SortListControl(_role, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        dtgRole.DataSource = _role
        dtgRole.DataBind()
    End Sub

    Private Sub RetrieveDealerInfoFromQueryString()
        'Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        objUser = sessionHelper.GetSession("User")
        lblKodeOrganisasiValue.Text = objUser.Dealer.DealerCode & " / " & objUser.Dealer.SearchTerm1
        lblNamaOrganisasiValue.Text = objUser.Dealer.DealerName
        lblIDValue.Text = objUser.Dealer.ID
        lblNamaLoginValue.Text = objUser.UserName
        If Not IsNothing(objUser.DealerBranch) AndAlso objUser.DealerBranch.ID > 0 Then
            lblSubOrgCode.Text = objUser.DealerBranch.DealerCode
            lblSubOrgName.Text = objUser.DealerBranch.SearchTerm1
        End If

    End Sub

    Private Sub RetrieveDealerInfo()
        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        Dim objUserInfo As UserInfo = sessionHelper.GetSession("LOGINUSERINFO")
        lblKodeOrganisasiValue.Text = objDealer.DealerCode & " / " & objDealer.SearchTerm1
        lblNamaOrganisasiValue.Text = objDealer.DealerName
        lblIDValue.Text = objDealer.ID
        lblNamaLoginValue.Text = objUserInfo.UserName
        If Not IsNothing(objUser) AndAlso Not IsNothing(objUser.DealerBranch) AndAlso objUser.DealerBranch.ID > 0 Then
            lblSubOrgCode.Text = objUser.DealerBranch.DealerCode
            lblSubOrgName.Text = objUser.DealerBranch.SearchTerm1
        End If
    End Sub

    Sub dtgRole_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs)

    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                               ByVal SortDirection As Integer)
        '-- Sort arraylist

        If SortColumn.Trim <> "" Then
            Dim isASC As Boolean = (SortDirection = Sort.SortDirection.ASC)  '-- Is sorted ascending?
            Dim objListComparer As IComparer = New ListComparer(isASC, SortColumn)
            pCompletelist.Sort(objListComparer)
        End If

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        objUser = sessionHelper.GetSession("User")
        BindHeaderToForm()
        BindDetailToGrid()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Try
            SaveInfoToDb()
            'Response.Redirect("../welcome.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SaveInfoToDb()
        If Not Page.IsValid Then
            Return
        End If
        objUser = CType(sessionHelper.GetSession("User"), UserInfo)
        BindHeaderToObject()
        objUser.LoginFlag = "1"
        Try
            SaveToDatabase()
            MessageBox.Show(SR.SaveSuccess)
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
    End Sub

    'Private Sub SavePassword()
    '    confContext = CType(ConfigurationManager.GetCurrentContext(), ConfigurationContext)
    '    Dim securitySetting As SecuritySettings = CType(confContext.GetConfiguration(SecuritySettings.SectionName), SecuritySettings)
    '    dbAuthenticationProvider = CType(securitySetting.AuthenticationProviders(0), DbAuthenticationProviderData)
    '    userRoleMgr = New UserRoleManager(dbAuthenticationProvider.Database, confContext)
    '    Dim hashProviderFac As HashProviderFactory = New HashProviderFactory(confContext)
    '    hashProvider = hashProviderFac.CreateHashProvider(dbAuthenticationProvider.HashProvider)
    '    Dim pwd As Byte() = hashProvider.CreateHash(Encoding.Unicode.GetBytes(txtKataKunciBaru.Text))
    '    Try
    '        Dim UserName As String
    '        If Request.QueryString("id") = String.Empty Then
    '            UserName = User.Identity.Name
    '        Else
    '            objUser = sessionHelper.GetSession("User")
    '            UserName = objUser.Dealer.ID.ToString.PadLeft(6, "0") & objUser.UserName
    '        End If
    '        userRoleMgr.ChangeUserPassword(UserName, pwd)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub SaveToDatabase()
        'If txtKataKunciBaru.Text <> String.Empty Then
        '    SavePassword()
        'End If
        Dim objUserInfoFacade As New UserInfoFacade(User)
        objUserInfoFacade.Update(objUser)
    End Sub

    Private Sub BindHeaderToObject()
        Dim imageFile As Byte()
        'objUser.Question = txtPertanyaan.Text
        'objUser.Answer = DNetEncryption.SymmetricEncrypt(txtJawaban.Text.Trim, txtPertanyaan.Text.Trim)

        If (cbDeletePhoto.Checked = False) Then
            If ((photoSrc.PostedFile.ContentLength = 0) And Not IsNothing(objUser.Picture)) Then
                imageFile = CType(sessionHelper.GetSession("User"), UserInfo).Picture
            Else
                imageFile = UploadFile()
            End If
        End If

        objUser.FirstName = txtNamaDepan.Text
        objUser.LastName = txtNamaBelakang.Text

        'objUser.JobPositionOld = txtPosisi.Text
        Try
            objUser.JobPosition = New JobPositionFacade(User).Retrieve(CType(ddlPosition.SelectedValue, Integer))
        Catch ex As Exception
        End Try

        objUser.Telephone = txtTelepon.Text
        objUser.Email = txtEmail.Text
        objUser.HandPhone = txtHP.Text
        objUser.Picture = imageFile
    End Sub
    Private Function UploadFile() As Byte()
        Dim nResult() As Byte

        If photoSrc.Value = "" Then
            Return Nothing
        End If

        If Not (photoSrc.PostedFile Is Nothing) Then
            Try
                If IsValidPhoto(photoSrc.PostedFile) Then
                    Dim inStream As System.IO.Stream = photoSrc.PostedFile.InputStream()
                    Dim ByteRead(TrTrainee.MAX_PHOTO_SIZE) As Byte
                    Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, TrTrainee.MAX_PHOTO_SIZE)
                    If ReadCount = 0 Then
                        Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
                    End If
                    ReDim nResult(ReadCount)
                    Array.Copy(ByteRead, nResult, ReadCount)
                Else
                    'Throw New DataException("Foto harus file gambar dan maksimum 20 KB")
                    MessageBox.Show("Foto harus file gambar dan maksimum " & TrTrainee.MAX_PHOTO_SIZE / 1024 & " KB")
                End If
            Catch
                'Throw
            End Try
        End If

        Return nResult
    End Function
    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(TrTrainee.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= TrTrainee.MAX_PHOTO_SIZE)
        Return (containImage AndAlso sizeValid)
    End Function

    Private Sub btnLanjut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLanjut.Click
        'If IsPasswordValid() Then
        Try
            SaveInfoToDb()
            Response.Redirect("../welcome.aspx")
        Catch ex As Exception

        End Try
        'End If
    End Sub

    'Private Function IsPasswordValid() As Boolean
    '    Dim pwdBytes As Byte() = Encoding.Unicode.GetBytes(txtKataKunciLama.Text)

    '    Dim authenticated As Boolean = SecurityProvider.Authenticate(User.Identity.Name, pwdBytes, identity)

    '    If authenticated Then
    '        Return True
    '    Else
    '        MessageBox.Show("Kata kunci lama salah...!!")
    '        Return False
    '    End If
    'End Function

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        If (Request.QueryString("id") = String.Empty) Then
            Response.Redirect("../welcome.aspx")
        Else
            Response.Redirect("../UserManagement/FrmUserList.aspx?Reread=True")
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If IsPasswordValid() Then
        Try
            SaveInfoToDb()
        Catch ex As Exception

        End Try
        'End If
    End Sub

    Private Sub dtgRole_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgRole.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        'BindDatagrid()
        ShortBindDetailToGrid() '--
    End Sub
    Private Sub BindDropDownLists()
        CommonFunction.BindJobPosition(ddlPosition, Me.User, False)
    End Sub



End Class
