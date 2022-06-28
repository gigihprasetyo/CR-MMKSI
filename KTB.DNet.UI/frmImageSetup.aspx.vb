Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Lib
Imports KTB.DNet.BusinessFacade.Helper
Imports Microsoft.Practices.EnterpriseLibrary.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security
Imports Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
Imports Microsoft.Practices.EnterpriseLibrary.Security.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database.Authentication.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Security.Database
Imports System.Text
Imports System.Web.Security
Imports KTB.DNet.Security
Imports System.Security.Principal
Public Class frmImageSetup
    Inherits System.Web.UI.Page
    'Private Const hashProvider As String = "SHA1Managed"

    Protected WithEvents txtHP As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNamaIbu As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPertanyaan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJawaban As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTanggalLahir As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPassword As System.Web.UI.WebControls.Label
    Protected WithEvents txtKataKunci As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Private ss As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents txtDeskripsiGambar As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDaftar As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents btnNewToken As System.Web.UI.WebControls.Button
    Protected WithEvents btnKirimKodeAktivasi As System.Web.UI.WebControls.Button
    Protected WithEvents CodeNumberTextBox As System.Web.UI.WebControls.TextBox
    Protected WithEvents MessageLabel As System.Web.UI.WebControls.Label
    Protected WithEvents Textbox7 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents photoView As System.Web.UI.WebControls.Image
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents btnPrevious As System.Web.UI.WebControls.Button
    Protected WithEvents btnNext As System.Web.UI.WebControls.Button
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents btnLogout As System.Web.UI.WebControls.Button
    Protected WithEvents photoSrc As System.Web.UI.HtmlControls.HtmlInputFile
    


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private sessionHelper As New SessionHelper
    Private objUser As UserInfo
    Private confContext As ConfigurationContext
    Private dbAuthenticationProvider As DbAuthenticationProviderData
    Private userRoleMgr As UserRoleManager
    Private hashProvider As IHashProvider
    Private identity As IIdentity
    Private objUserInfo As UserInfo

    Private Function GenerateRandomCode() As String
        Try
            Dim generator As RandomGenerator = New RandomGenerator
            Return generator.GenarateRandomCharacterOnly(5).ToUpper
        Catch ex As Exception
            Return "ACFSA"
        End Try
    End Function

    Private Sub FillObjUser()
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUser Is Nothing Then
            txtHP.Text = objUser.HandPhone
            txtEmail.Text = objUser.Email
            txtPertanyaan.Text = objUser.Question
            txtJawaban.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objUser.Answer, objUser.Question)
            If Not objUser.UserProfile Is Nothing Then
                txtTanggalLahir.Text = objUser.UserProfile.BirthDate
                txtNamaIbu.Text = objUser.UserProfile.MotherName
                txtDeskripsiGambar.Text = objUser.UserProfile.ImageDescription
            End If
        End If
    End Sub

    Private Sub LoadImageGuard()
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        Dim ImageId As Integer
        Dim i As Integer
        Dim idUser As String
        Dim listImage As ArrayList = New PhisingGuardImageFacade(User).RetrieveList("ID", Sort.SortDirection.DESC)
        ss.SetSession("IMAGE_GUARD", listImage)
        If Not objUser.UserProfile Is Nothing Then
            ImageId = objUser.UserProfile.ImageID
            For Each item As PhisingGuardImage In listImage
                i += 1
                If item.ID = ImageId Then
                    ss.SetSession("IMAGE_INDEX", i)
                    Exit For
                End If
            Next
        Else
            ss.SetSession("IMAGE_INDEX", 0)
        End If

        If ImageId > 0 Then
            idUser = ImageId
        Else
            idUser = CType(listImage(0), PhisingGuardImage).ID.ToString
        End If

        Me.photoView.ImageUrl = "WebResources/GetImage.aspx?id=" & idUser
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LoadImageGuard()
            InitilizeImageButton()
            FillObjUser()
            Session.Add("CaptchaImageText", GenerateRandomCode)

        Else
            If CodeNumberTextBox.Text = CStr(Session.Item("CaptchaImageText")) Then
                MessageLabel.Text = "Correct"
            Else
                MessageLabel.Text = "Tidak Correct"
                CodeNumberTextBox.Text = ""
                Session.Add("CaptchaImageText", GenerateRandomCode)
            End If
        End If
    End Sub

    Private Sub btnKirimKodeAktivasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKirimKodeAktivasi.Click
        SendingActivatedCode(3, 7)
    End Sub

    Private Sub SendingActivatedCode(ByVal x As Int16, ByVal y As Int16)
        Dim bingoHashing As HashingBingo = New HashingBingo(3, 7, 2)
        bingoHashing.GenerateBingo()
        Dim sms As String = bingoHashing.BingoSMS
        Dim listBingo As ArrayList = bingoHashing.Bingo
        If LogBingo(listBingo, x, y) Then
            SendMessage(sms)
        End If
    End Sub

    Private Function LogBingo(ByVal listBingo As ArrayList, ByVal x As Int16, ByVal y As Int16) As Boolean
        Dim result As Boolean = False
        Dim _bingo As KTB.DNet.Domain.Bingo = New KTB.DNet.Domain.Bingo
        _bingo.DimensiX = x
        _bingo.DimensiY = y
        _bingo.SerialNumber = New RandomGenerator().GenarateRandom(15)
        _bingo.Handphone = "999999999"
        For Each item As String() In listBingo
            Dim matrix As BingoMatrix = New BingoMatrix
            matrix.PosisiY = CType(item(0), String)
            matrix.PosisiX = CType(item(1), String)
            matrix.Code = CType(item(2), String)
            _bingo.BingoMatrixs.Add(matrix)
        Next
        Dim _bingoFacade As BingoFacade = New BingoFacade(User)
        Dim i As Int16 = _bingoFacade.Insert(_bingo)
        If i = 0 Then
            result = True
        End If
        Return result
    End Function

    Private Sub SendMessage(ByVal msg As String)
        'Send SMS
    End Sub

    Private Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Session.RemoveAll()
        FormsAuthentication.SignOut()
        RegisterStartupScript("OpenNewWindow", "<script>OpenNewWindow('login.aspx')</script>")
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        UploadImage()
    End Sub

    Private Sub UploadImage()
        Dim objPhisingGuardImage As PhisingGuardImage = New PhisingGuardImage
        Dim objPhisingGuardImageFacade As PhisingGuardImageFacade = New PhisingGuardImageFacade(User)
        Dim nResult = -1
        Dim imageFile As Byte()
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        Try
            imageFile = UploadFile()
            objPhisingGuardImage.Image = imageFile
            objPhisingGuardImage.ImageCode = "Uploded User Image"
            objPhisingGuardImage.Type = 0
            objPhisingGuardImage.UploadedUserID = objUser.ID

            nResult = objPhisingGuardImageFacade.Insert(objPhisingGuardImage)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                LoadImageGuard()
                MessageBox.Show(SR.SaveSuccess)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(TrTrainee.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= TrTrainee.MAX_PHOTO_SIZE) And (file.ContentLength > 0)

        Return (containImage AndAlso sizeValid)
    End Function

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
                        Throw New InvalidConstraintException(SR.DataNotFound("Image"))
                    End If
                    ReDim nResult(ReadCount)
                    Array.Copy(ByteRead, nResult, ReadCount)
                Else
                    Throw New DataException("Foto harus file gambar dan maksimum 20 KB")
                End If
            Catch
                Throw
            End Try
        End If

        Return nResult
    End Function

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        InitilizeImageButton()
        Dim index As Integer = ss.GetSession("IMAGE_INDEX")
        Dim listImage As ArrayList = CType(ss.GetSession("IMAGE_GUARD"), ArrayList)
        Dim newIndex As Integer = 0
        If index > 0 Then
            newIndex = index - 1
            ss.SetSession("IMAGE_INDEX", newIndex)
        End If
        If newIndex = 0 Then
            btnPrevious.Enabled = False
        End If
        Dim idUser As String = CType(listImage(newIndex), PhisingGuardImage).ID.ToString
        Me.photoView.ImageUrl = "WebResources/GetImage.aspx?id=" & idUser
    End Sub

    Private Sub InitilizeImageButton()
        Try
            Dim index As Integer = ss.GetSession("IMAGE_INDEX")
            Dim count As Integer = CType(ss.GetSession("IMAGE_GUARD"), ArrayList).Count
            btnPrevious.Enabled = True
            btnNext.Enabled = True
            If index = 0 Then
                btnPrevious.Enabled = False
            End If
            If index = count - 1 Then
                btnNext.Enabled = False
            End If
        Catch ex As Exception
            btnPrevious.Enabled = True
            btnNext.Enabled = True
        End Try
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        InitilizeImageButton()
        Dim index As Integer = ss.GetSession("IMAGE_INDEX")
        Dim listImage As ArrayList = CType(ss.GetSession("IMAGE_GUARD"), ArrayList)
        Dim newIndex As Integer = 0
        If index < listImage.Count - 1 Then
            newIndex = index + 1
            ss.SetSession("IMAGE_INDEX", newIndex)
        End If
        If newIndex = listImage.Count - 1 Then
            btnNext.Enabled = False
        End If
        Dim idUser As String = CType(listImage(newIndex), PhisingGuardImage).ID.ToString
        Me.photoView.ImageUrl = "WebResources/GetImage.aspx?id=" & idUser
    End Sub

    Private Sub btnDaftar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDaftar.Click
        RegisterUser()
    End Sub

    Private Sub RegisterUser()
        Dim objUser As UserInfo = PopulateUser()
        Dim objUserProfile As UserProfile = PopulateUserProfile()
        Dim _userInfoFacade As UserInfoFacade = New UserInfoFacade(User)
        Try
            Dim i As Integer = _userInfoFacade.RegisterUser(objUser, objUserProfile)
            If i = 1 And txtKataKunci.Text.Trim <> "" Then
                SavePassword(objUser, txtKataKunci.Text.Trim)
            End If
            MessageBox.Show("Register User succesfully")
        Catch ex As Exception
            MessageBox.Show("Register User Failed")
        End Try

    End Sub

    Private Function PopulateUser() As UserInfo
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        objUser.Email = txtEmail.Text.Trim
        objUser.HandPhone = txtHP.Text.Trim
        objUser.Question = txtPertanyaan.Text.Trim
        objUser.Answer = KTB.DNet.UI.Helper.DNetEncryption.SymmetricEncrypt(txtJawaban.Text.Trim, txtPertanyaan.Text.Trim)
        Return objUser
    End Function

    Private Function PopulateUserProfile() As UserProfile
        Dim imgList As ArrayList = CType(ss.GetSession("IMAGE_GUARD"), ArrayList)
        Dim index As Integer = ss.GetSession("IMAGE_INDEX")
        Dim objPhisingGuard As PhisingGuardImage = CType(imgList(index), PhisingGuardImage)
        Dim objUserProfile As UserProfile = New UserProfile
        objUserProfile.ImageID = objPhisingGuard.ID
        objUserProfile.ImageDescription = txtDeskripsiGambar.Text.Trim
        objUserProfile.MotherName = txtNamaIbu.Text.Trim
        objUserProfile.BirthDate = txtTanggalLahir.Text.Trim
        objUserProfile.RegistrationStatus = -1
        Dim RandGenerator As RandomGenerator = New RandomGenerator
        objUserProfile.ActivationCode = RandGenerator.GetRandomNumeric(8)
        objUserProfile.ActivationStatus = -1
        Return objUserProfile
    End Function

    Private Function IsPasswordValid(ByVal pwd As String) As Boolean
        Dim pwdBytes As Byte() = Encoding.Unicode.GetBytes(pwd)

        Dim authenticated As Boolean = SecurityProvider.Authenticate(User.Identity.Name, pwdBytes, identity)

        If authenticated Then
            Return True
        Else
            MessageBox.Show("Kata kunci lama salah...!!")
            Return False
        End If
    End Function

    Private Sub SavePassword(ByVal objUser As UserInfo, ByVal password As String)
        confContext = CType(ConfigurationManager.GetCurrentContext(), ConfigurationContext)
        Dim securitySetting As SecuritySettings = CType(confContext.GetConfiguration(SecuritySettings.SectionName), SecuritySettings)
        dbAuthenticationProvider = CType(securitySetting.AuthenticationProviders(0), DbAuthenticationProviderData)
        userRoleMgr = New UserRoleManager(dbAuthenticationProvider.Database, confContext)
        Dim hashProviderFac As HashProviderFactory = New HashProviderFactory(confContext)
        hashProvider = hashProviderFac.CreateHashProvider(dbAuthenticationProvider.HashProvider)
        Dim pwd As Byte() = hashProvider.CreateHash(Encoding.Unicode.GetBytes(password))
        Try
            Dim UserName As String = objUser.Dealer.ID.ToString.PadLeft(6, "0") & objUser.UserName
            userRoleMgr.ChangeUserPassword(UserName, pwd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        GotoMainMenu()
    End Sub

    Private Sub GotoMainMenu()
        Dim objDealer As Dealer = CType(ss.GetSession("DEALER"), Dealer)
        Dim UrlGeneral As String = "default_general.aspx?type=0"
        Dim UrlEULA As String = "frmUELA.aspx?type=0"
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlEULA + """)</script>")
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlGeneral + """)</script>")
        End If
    End Sub
End Class
