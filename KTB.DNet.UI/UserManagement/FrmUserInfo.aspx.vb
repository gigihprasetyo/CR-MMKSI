Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security
Imports Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
Imports Microsoft.Practices.EnterpriseLibrary.Security.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database.Authentication.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database
Imports System.Text


Public Class FrmUserInfo
    Inherits System.Web.UI.Page
    Private stringToEncrypt As String
    Private encryptedContents As Byte()
    Private hashProvider As IHashProvider
    Private DealerID As Integer
    Protected WithEvents txtKonfirmasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgUserRole1 As System.Web.UI.WebControls.DataGrid
    Dim RoleSelection As Integer
    Protected WithEvents TxtError As System.Web.UI.WebControls.Label
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RequiredFieldValidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator9 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtkodeorg As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBranchCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeOrg As System.Web.UI.WebControls.Label
    Protected WithEvents txtNamaOrg As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator10 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator3 As System.Web.UI.WebControls.RegularExpressionValidator
    Private sessionHelper As New sessionHelper
    Private objDealer As Dealer
    Private RoleNew As ArrayList = New ArrayList
    Protected WithEvents RegularExpressionValidator4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator5 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator7 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents TxtNamaDepan As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtNamablk As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtPosisi As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtTlp As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmailAdd As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnReset As System.Web.UI.WebControls.Button
    Protected WithEvents RegularExpressionValidator6 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents icBirthday As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkDspNotification As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icTglLahir As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlPosition As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNamaOrg As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaSubOrg As System.Web.UI.WebControls.Label
    Private sessDealer As Dealer

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents txtUserName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFirstName As System.Web.UI.WebControls.TextBox
    Private confContext As ConfigurationContext
    Private dbAuthenticationProvider As DbAuthenticationProviderData
    Private userRoleMgr As UserRoleManager
    Protected WithEvents DdlStat As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CompareValidator2 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtPertanyaan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJawaban As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator7 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub LogTosyslog(ByVal message As String, ByVal sbMsg As String, ByVal rslMsg As String, Optional ByVal mdlmsg As String = "web-security", Optional ByVal sbAction As String = "view")
        Dim strLog As Boolean = KTB.DNet.Lib.WebConfig.GetValue("EnableSyslog")
        If strLog Then
            Dim objSyslog As SyslogParameter = New SyslogParameter(User)
            Dim m As KTB.DNet.Lib.SysLogXMLMessage = New KTB.DNet.Lib.SysLogXMLMessage
            m.Action = sbAction.ToLower
            m.SubBlockName = sbMsg.ToLower
            m.FullMessage = message.ToLower
            m.ModuleName = mdlmsg.ToLower
            m.Pages = HttpContext.Current.Request.Url.LocalPath
            m.RemoteIPAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
            m.StatusResult = rslMsg.ToLower
            m.Status = KTB.DNet.[Lib].DNetLogFormatStatus.Direct
            m.BlockName = "user-management"


            Try
                m.UserName = IIf(User.Identity.Name.Length > 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name).ToLower
            Catch ex As Exception
                m.UserName = "Wb-Usr"
            End Try
            m.Dealer = CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode.ToLower
            objSyslog.LogError(m)
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sessDealer = sessionHelper.GetSession("DEALER")
        ActivateUserPrivilege()
        If Not IsPostBack Then
            Me.lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            txtkodeorg.Attributes.Add("readonly", "readonly")
        End If
        If sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            DealerPageLoad()
            'InitiatePage()
        ElseIf sessDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Me.txtkodeorg.Attributes.Add("onblur", "FindDealer()")
            KtbPageLoad()
            'InitiatePage()
        ElseIf sessDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
            LeasingPageLoad()
        End If
    End Sub
    Private Sub ActivateUserPrivilege()
        If Not IsNothing(sessDealer) Then
            If sessDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If Not SecurityProvider.Authorize(Context.User, SR.AdminCreateNewUserKTB_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - User Baru")
                End If
                Return
            ElseIf sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER OrElse sessDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                If Not SecurityProvider.Authorize(Context.User, SR.AdminCreateNewUserDealer_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - User Baru")
                End If
                Return
            End If
        End If
        Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - User Baru")
    End Sub
    Private Sub DealerPageLoad()
        objDealer = sessDealer
        If Not IsPostBack Then
            Me.txtkodeorg.ReadOnly = True
            Me.lblSearchDealer.Visible = False
            DealerID = objDealer.ID
            InitiatePage()
            BindDatagrid()
            BindDDLStat()
            BindDropDownLists()
            ClearData()
        Else
            ListingCheckedData()
        End If
    End Sub
    Private Sub KtbPageLoad()
        If Not IsPostBack Then
            KtbInitiatePage()
            Me.txtkodeorg.ReadOnly = False
            Me.lblSearchDealer.Visible = True
            DealerCodeChanged()
            BindDDLStat()
            BindDropDownLists()
        Else
            objDealer = sessionHelper.GetSession("objDealer")
            ListingCheckedData()
            DealerID = objDealer.ID
            If Not IsNothing(objDealer) Then
                BindDatagrid()
            End If
        End If
    End Sub
    Private Sub LeasingPageLoad()
        objDealer = sessDealer
        If Not IsPostBack Then
            Me.txtkodeorg.ReadOnly = True
            Me.lblSearchDealer.Visible = False
            DealerID = objDealer.ID
            InitiatePage()
            BindDatagrid()
            BindDDLStat()
            BindDropDownLists()
            ClearData()
        Else
            ListingCheckedData()
        End If
    End Sub
    Private Sub KtbInitiatePage()
        Me.txtkodeorg.Text = ""
        Me.txtkodeorg.AutoPostBack = True
        Me.lblNamaOrg.Text = ""

    End Sub
    Private Sub InitiatePage()
        Me.txtkodeorg.Text = objDealer.DealerCode
        Me.txtkodeorg.AutoPostBack = False
        Me.lblNamaOrg.Text = objDealer.DealerName
        sessionHelper.RemoveSession("checkedData")
    End Sub

    Private Sub DealerCodeChanged()
        objDealer = New DealerFacade(User).Retrieve(Me.txtkodeorg.Text)
        If Not IsNothing(objDealer) Then
            sessionHelper.SetSession("objDealer", objDealer)
            Me.lblNamaOrg.Text = objDealer.DealerName
            If Me.IsPostBack Then
                If objDealer.DealerName = "" Then
                    Dim DealerNotFoundID As String = Me.txtkodeorg.Text
                    MessageBox.Show(SR.DataNotFound(DealerNotFoundID))
                End If
            End If
            Me.txtkodeorg.AutoPostBack = True
            DealerID = objDealer.ID
            BindDatagrid()
            ClearData()
        Else
            Dim DealerNotFoundID As String = Me.txtkodeorg.Text
            MessageBox.Show(SR.DataNotFound(DealerNotFoundID))
        End If
    End Sub

    Private Sub ListingCheckedData()
        sessionHelper.RemoveSession("checkedData")
        Dim listOfCheckedData As ArrayList = CType(sessionHelper.GetSession("checkedData"), ArrayList)
        If IsNothing(listOfCheckedData) Then
            listOfCheckedData = New ArrayList
        End If
        For Each item As DataGridItem In dtgUserRole1.Items
            If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                Dim cbItem As CheckBox = CType(item.FindControl("cbItem"), CheckBox)
                If cbItem.Checked Then
                    If Not listOfCheckedData.Contains(item.Cells(0).Text) Then
                        listOfCheckedData.Add(item.Cells(0).Text)
                    End If
                Else
                    If listOfCheckedData.Contains(item.Cells(0).Text) Then
                        listOfCheckedData.Remove(item.Cells(0).Text)
                    End If
                End If
            End If
        Next
        sessionHelper.SetSession("checkedData", listOfCheckedData)
    End Sub

    Sub BindDDLStat()
        Dim objUserStat = New EnumUserStatus
        Dim UserStat As ArrayList
        If sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            UserStat = objUserStat.RetrieveNewOnly()
        ElseIf sessDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
            UserStat = objUserStat.RetrieveNewOnly()
        Else
            UserStat = objUserStat.Retrieve()
        End If
        DdlStat.DataSource = UserStat
        DdlStat.DataTextField = "NameTitle"
        DdlStat.DataValueField = "ValTitle"
        DdlStat.DataBind()
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

    Private Sub BindDatagrid()
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Role), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Role), "Dealer.ID", MatchType.Exact, DealerID))
        'Dim roleCollection As ArrayList = New RoleFacade(User).RetrieveActiveList(criterias, 1, 1000000, 0, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
        Dim roleCollection As ArrayList = New RoleFacade(User).Retrieve(criterias)
        sessionHelper.SetSession("ROLELIST", roleCollection)
        Me.dtgUserRole1.DataSource = roleCollection
        Me.dtgUserRole1.DataBind()
    End Sub

    Private Sub ShortBindDatagrid()

        Dim roleColl As ArrayList = CType(sessionHelper.GetSession("ROLELIST"), ArrayList)
        SortListControl(roleColl, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
        Me.dtgUserRole1.DataSource = roleColl
        Me.dtgUserRole1.DataBind()
    End Sub

    Private Function CheckUserRoleExist(ByVal Criterias As ICriteria)
        Dim RoleList As ArrayList
        Dim ObjUserRole As New UserRoleFacade(User)
        RoleList = ObjUserRole.Retrieve(Criterias)
        Return RoleList
    End Function

    Private Sub GetUserType()
        Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).GetDealer(Me.txtkodeorg.Text)
        Dim objDealerBranch As New Dealer
        If Not String.IsNullorEmpty(txtBranchCode.Text.Trim) Then
            objDealerBranch = New KTB.DNet.BusinessFacade.General.DealerFacade(User).GetDealer(Me.txtBranchCode.Text)
        End If

        Dim objNewUserInfo As UserInfo = New UserInfoFacade(User).Retrieve(txtUserName.Text, objDealer.DealerCode)
        objNewUserInfo.Dealer = objDealer
        objNewUserInfo.DealerBranch = objDealerBranch
        objNewUserInfo.UserName = txtUserName.Text
        objNewUserInfo.Question = Me.txtPertanyaan.Text
        objNewUserInfo.Answer = DNetEncryption.SymmetricEncrypt(Me.txtJawaban.Text.Trim, Me.txtPertanyaan.Text)
        objNewUserInfo.FirstName = Me.TxtNamaDepan.Text
        objNewUserInfo.LastName = Me.TxtNamablk.Text
        'objNewUserInfo.JobPositionOld = Me.TxtPosisi.Text
        Try
            objNewUserInfo.JobPosition = New JobPositionFacade(User).Retrieve(CType(ddlPosition.SelectedValue, Integer))
        Catch ex As Exception
        End Try
        objNewUserInfo.Telephone = Me.TxtTlp.Text
        objNewUserInfo.Email = Me.txtEmailAdd.Text
        objNewUserInfo.HandPhone = Me.txtHP.Text
        objNewUserInfo.UserStatus = Me.DdlStat.SelectedItem.Value
        objNewUserInfo.LoginFlag = DBRowStatus.Active
        objNewUserInfo.Birthday = Me.icTglLahir.Value
        If Me.chkDspNotification.Checked Then
            objNewUserInfo.MessageNotification = EnumMsgNotification.MsgNotification.Active  'aktif
        Else
            objNewUserInfo.MessageNotification = EnumMsgNotification.MsgNotification.NotActive 'non aktif
        End If
        objNewUserInfo.EmailValidation = EnumEmailValidation.EmailValidation.isNotEmailValid
        objNewUserInfo.CreatedBy = User.Identity.Name
        objNewUserInfo.LastUpdateBy = User.Identity.Name
        objNewUserInfo.RowStatus = "0"
        objNewUserInfo.CreatedTime = Now
        objNewUserInfo.TokenAlertTime = New DateTime(1900, 1, 1)
        sessionHelper.SetSession("UserData", objNewUserInfo)
    End Sub
    Private Function DataGridCheck() As Integer
        Dim nresult As Integer = -1
        For Each item As DataGridItem In dtgUserRole1.Items
            Dim cbItem As CheckBox = CType(item.FindControl("cbItem"), CheckBox)
            If cbItem.Checked Then
                nresult = 1
            End If
        Next
        Return nresult
    End Function

    Private Sub SendEmail(ByVal emailTo As String, ByVal emailFrom As String, ByVal contentEmail As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim objEmail As DNetMail = New DNetMail(smtp)
        objEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Email Validation", Mail.MailFormat.Text, contentEmail)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If IsNothing(Session("Dealer")) = False Then
            If Not Page.IsValid Then
                Return
            End If
            Dim checkedData As ArrayList = CType(sessionHelper.GetSession("checkedData"), ArrayList)
            If checkedData.Count = 0 Then
                Me.TxtError.Text = "Silakan pilih minimal 1 role"
                Me.TxtError.Font.Bold = True
                Return
            End If

            If txtUserName.Text.Trim.Length > 14 Then
                Me.TxtError.Text = "UserName tidak boleh lebih dari 14 karakter"
                Me.TxtError.Font.Bold = True
                Return
            End If

            Dim ObjRoleInsert As New KTB.DNet.BusinessFacade.UserManagement.UserInfoFacade(User)
            Dim nResult As Integer = -1
            GetUserType()
            Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).GetDealer(Me.txtkodeorg.Text)
            Dim objUserInfo As UserInfo = CType(Session("UserData"), UserInfo)

            confContext = CType(ConfigurationManager.GetCurrentContext(), ConfigurationContext)
            Dim securitySetting As SecuritySettings = CType(confContext.GetConfiguration(SecuritySettings.SectionName), SecuritySettings)
            dbAuthenticationProvider = CType(securitySetting.AuthenticationProviders(0), DbAuthenticationProviderData)
            userRoleMgr = New UserRoleManager(dbAuthenticationProvider.Database, confContext)
            Dim hashProviderFac As HashProviderFactory = New HashProviderFactory(confContext)
            hashProvider = hashProviderFac.CreateHashProvider(dbAuthenticationProvider.HashProvider)
            Dim pwd As Byte() = hashProvider.CreateHash(Encoding.Unicode.GetBytes(txtPassword.Text))
            If userRoleMgr.CreateUser(objDealer.ID.ToString.PadLeft(6, "0") & objUserInfo.UserName.Trim, pwd) = False Then
                Me.TxtError.Text = "Nama User Sudah dipakai / terdaftar!"
                Me.TxtError.Font.Bold = True
                MessageBox.Show(SR.SaveFail)
            Else
                Dim objNewUserInfo As UserInfo = New UserInfoFacade(User).Retrieve(txtUserName.Text, objDealer.DealerCode)
                Dim ObjUpdate As UserInfoFacade = New KTB.DNet.BusinessFacade.UserManagement.UserInfoFacade(User)
                Dim UserRoleList As ArrayList = New ArrayList
                If IsNothing(objNewUserInfo) = False Then
                    objUserInfo.ID = objNewUserInfo.ID
                    sessionHelper.SetSession("UserData", objUserInfo)
                    If (objNewUserInfo.TokenAlertTime.Year < 2000) Then
                        objUserInfo.TokenAlertTime = New Date(1900, 1, 1)
                    Else
                        objUserInfo.TokenAlertTime = objNewUserInfo.TokenAlertTime
                    End If

                    nResult = ObjUpdate.Update(objUserInfo)
                    If nResult <> -1 Then
                        Dim AllExistingRole As Integer
                        Dim ObjUserRole As New KTB.DNet.BusinessFacade.UserManagement.UserRoleFacade(User)
                        If Not IsNothing(checkedData) Then
                            For Each item As Integer In checkedData
                                UserRoleList.Add(CreateUserRole(item))
                            Next
                        End If
                        Dim ObjRole As UserRole = New UserRole
                        Dim ObjUserRoleUpdate As New KTB.DNet.BusinessFacade.UserManagement.UserRoleFacade(User)
                        nResult = ObjUserRoleUpdate.InsertUserRole(UserRoleList)
                        If nResult = 1 Then
                            MessageBox.Show(SR.SaveSuccess)

                            LogTosyslog("user " & objUserInfo.Dealer.DealerCode & "-" & objUserInfo.UserName & " berhasil di-create oleh " & CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode & "-" & CType(Session("LOGINUSERINFO"), UserInfo).UserName, "user-create-admin", "sucess")
                            LogTosyslog("role added for user " & objUserInfo.Dealer.DealerCode & "-" & objUserInfo.UserName & " are : " & PopulateRoleName(UserRoleList), "user-role-create", "success")

                            'Todo Syslog Role

                            'send an email
                            Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
                            Dim emailTo As String = txtEmailAdd.Text.Trim
                            Dim emailMsg As String = "Demi keamanan dan kenyamanan anda, lakukan validasi email anda pada saat login"
                            SendEmail(emailTo, emailFrom, emailMsg)

                            ClearData()
                            If sessDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                                ClearHeader()
                                ClearDataGrid()
                            End If
                        End If
                    Else
                        ObjUpdate.Delete(objNewUserInfo)
                    End If
                Else
                    MessageBox.Show(SR.SaveFail)
                    ClearData()
                End If
            End If
        End If
    End Sub

    Private Function PopulateRoleName(ByVal UserRoleList As ArrayList) As String
        Dim strRoleName As String = ""
        For Each item As UserRole In UserRoleList
            strRoleName += item.Role.RoleName & ","
        Next
        Return strRoleName

    End Function

    Private Function CreateUserRole(ByVal roleID As Integer) As UserRole
        Dim objUserRole As UserRole = New UserRole
        objUserRole.Role = New RoleFacade(User).Retrieve(roleID)
        objUserRole.UserInfo = CType(Session("UserData"), UserInfo)
        objUserRole.RowStatus = DBRowStatus.Active
        Return objUserRole
    End Function
    Private Sub ClearData()
        Me.txtUserName.Text = String.Empty
        Me.txtPassword.Text = String.Empty
        Me.txtKonfirmasi.Text = String.Empty
        Me.txtPertanyaan.Text = String.Empty
        Me.txtJawaban.Text = String.Empty
        Me.TxtNamaDepan.Text = String.Empty
        Me.TxtNamablk.Text = String.Empty
        Me.TxtTlp.Text = String.Empty

        'Me.TxtPosisi.Text = String.Empty
        ddlPosition.SelectedIndex = -1

        Me.txtEmailAdd.Text = String.Empty
        Me.txtHP.Text = String.Empty
        Me.DdlStat.SelectedIndex = 0
        Me.TxtError.Text = String.Empty
        Me.chkDspNotification.Checked = False
        Me.icTglLahir.Value = Date.Now
    End Sub
    Sub ClearDataGrid()
        Me.dtgUserRole1.DataSource = ""
        Me.dtgUserRole1.DataBind()
    End Sub
    Sub ClearHeader()
        Me.txtkodeorg.Text = String.Empty
        Me.lblNamaOrg.Text = String.Empty
    End Sub
    Private Sub BindDropDownLists()
        sessDealer = sessionHelper.GetSession("DEALER")
        If sessDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
            CommonFunction.BindJobPosition(ddlPosition, Me.User, True)
        Else
            CommonFunction.BindJobPosition(ddlPosition, Me.User, False)
        End If
    End Sub
    Private Sub txtkodeorg_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtkodeorg.TextChanged
        DealerCodeChanged()
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ClearData()
        'ClearDataGrid()
    End Sub
    Private Sub dtgUserRole1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgUserRole1.SortCommand
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
        ShortBindDatagrid() '--
    End Sub

    Private Sub dtgUserRole1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUserRole1.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgUserRole1.CurrentPageIndex * dtgUserRole1.PageSize)
        End If
    End Sub
End Class

