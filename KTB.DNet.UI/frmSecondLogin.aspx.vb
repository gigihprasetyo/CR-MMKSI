Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Lib
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.PageHelper
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
Imports System.Configuration

Public Class frmSecondLogin
    Inherits System.Web.UI.Page
   
    Protected WithEvents txtHP As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNamaIbu As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPertanyaan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJawaban As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPertanyaan1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPertanyaan2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPertanyaan3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPertanyaan4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPertanyaan5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPertanyaan1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtPertanyaan2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPertanyaan3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPertanyaan4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPertanyaan5 As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator7 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator8 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator9 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator10 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator11 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator12 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator13 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator14 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents icTglLahir As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ImgCaptcha As System.Web.UI.WebControls.Image
    Protected WithEvents imgNext As System.Web.UI.WebControls.ImageButton
    Protected WithEvents imgBack As System.Web.UI.WebControls.ImageButton
    Protected WithEvents LnkTerms As System.Web.UI.WebControls.LinkButton

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
    Protected WithEvents CodeNumberTextBox As System.Web.UI.WebControls.TextBox
    Protected WithEvents Textbox7 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents photoView As System.Web.UI.WebControls.Image
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
    Private success As Boolean = False
    Private expiredBingoCount As String = KTB.DNet.Lib.WebConfig.GetValue("ExpiredCount").ToString
    Private x As String = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiX").ToString
    Private y As String = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiY").ToString
    Private w As String = KTB.DNet.Lib.WebConfig.GetValue("BingoWidth").ToString


    Private Function GenerateRandomCode() As String
        Try
            Dim generator As RandomGenerator = New RandomGenerator
            Return generator.GenarateRandomCharacterOnly(5).ToUpper
        Catch ex As Exception
            Return "XYFSE"
        End Try
    End Function

    Private Sub FillObjUser()
        Dim objTempUserProfile As UserProfile = CType(Session.Item("TEMPORARYUSERPROFILE"), UserProfile)
        Dim objTempUser As UserInfo = CType(Session.Item("TEMPORARYUSER"), UserInfo)
        If Not objTempUser Is Nothing Then
            txtHP.Text = objTempUser.HandPhone
            txtEmail.Text = objTempUser.Email
            txtPertanyaan.Text = objTempUser.Question
            txtJawaban.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objTempUser.Answer, objTempUser.Question)
            If Not objTempUserProfile Is Nothing Then
                icTglLahir.Value = objTempUserProfile.BirthDate
                txtNamaIbu.Text = objTempUserProfile.MotherName
                txtDeskripsiGambar.Text = objTempUserProfile.ImageDescription
                txtPertanyaan1.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objTempUserProfile.Answer1, objTempUserProfile.Question1)
                txtPertanyaan2.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objTempUserProfile.Answer2, objTempUserProfile.Question2)
                txtPertanyaan3.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objTempUserProfile.Answer3, objTempUserProfile.Question3)
                txtPertanyaan4.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objTempUserProfile.Answer4, objTempUserProfile.Question4)
                txtPertanyaan5.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objTempUserProfile.Answer5, objTempUserProfile.Question5)
                lblPertanyaan1.Text = objTempUserProfile.Question1
                lblPertanyaan2.Text = objTempUserProfile.Question2
                lblPertanyaan3.Text = objTempUserProfile.Question3
                lblPertanyaan4.Text = objTempUserProfile.Question4
                lblPertanyaan5.Text = objTempUserProfile.Question5
            Else
                lblPertanyaan1.Text = KTB.DNet.Lib.WebConfig.GetValue("Question1").ToString
                lblPertanyaan2.Text = KTB.DNet.Lib.WebConfig.GetValue("Question2").ToString
                lblPertanyaan3.Text = KTB.DNet.Lib.WebConfig.GetValue("Question3").ToString
                lblPertanyaan4.Text = KTB.DNet.Lib.WebConfig.GetValue("Question4").ToString
                lblPertanyaan5.Text = KTB.DNet.Lib.WebConfig.GetValue("Question5").ToString
            End If
        Else
            Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
            If Not objUser Is Nothing Then
                txtHP.Text = objUser.HandPhone
                txtEmail.Text = objUser.Email
                txtPertanyaan.Text = objUser.Question
                txtJawaban.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objUser.Answer, objUser.Question)
                If Not objUser.UserProfile Is Nothing Then
                    icTglLahir.Value = objUser.UserProfile.BirthDate
                    txtNamaIbu.Text = objUser.UserProfile.MotherName
                    txtDeskripsiGambar.Text = objUser.UserProfile.ImageDescription
                    txtPertanyaan1.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objUser.UserProfile.Answer1, objUser.UserProfile.Question1)
                    txtPertanyaan2.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objUser.UserProfile.Answer2, objUser.UserProfile.Question2)
                    txtPertanyaan3.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objUser.UserProfile.Answer3, objUser.UserProfile.Question3)
                    txtPertanyaan4.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objUser.UserProfile.Answer4, objUser.UserProfile.Question4)
                    txtPertanyaan5.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objUser.UserProfile.Answer5, objUser.UserProfile.Question5)
                    lblPertanyaan1.Text = objUser.UserProfile.Question1
                    lblPertanyaan2.Text = objUser.UserProfile.Question2
                    lblPertanyaan3.Text = objUser.UserProfile.Question3
                    lblPertanyaan4.Text = objUser.UserProfile.Question4
                    lblPertanyaan5.Text = objUser.UserProfile.Question5
                Else
                    lblPertanyaan1.Text = KTB.DNet.Lib.WebConfig.GetValue("Question1").ToString
                    lblPertanyaan2.Text = KTB.DNet.Lib.WebConfig.GetValue("Question2").ToString
                    lblPertanyaan3.Text = KTB.DNet.Lib.WebConfig.GetValue("Question3").ToString
                    lblPertanyaan4.Text = KTB.DNet.Lib.WebConfig.GetValue("Question4").ToString
                    lblPertanyaan5.Text = KTB.DNet.Lib.WebConfig.GetValue("Question5").ToString
                End If
            End If
        End If

    End Sub

    Private Function LoadGeneralImage() As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PhisingGuardImage), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PhisingGuardImage), "Type", MatchType.Exact, CInt(EnumSE.ImageType.General)))
        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PhisingGuardImage), "ID", Sort.SortDirection.DESC))
        Return New PhisingGuardImageFacade(User).Retrieve(criterias, sortColl)
    End Function

    Private Function LoadPrivateImage(ByVal id As Integer) As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PhisingGuardImage), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PhisingGuardImage), "UploadedUserID", MatchType.Exact, id))
        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PhisingGuardImage), "ID", Sort.SortDirection.ASC))
        Return New PhisingGuardImageFacade(User).Retrieve(criterias, sortColl)
    End Function

    Private Function GenerateImgRandom(ByVal min As Integer, ByVal max As Integer, ByVal count As Integer) As ArrayList
        Randomize(Now.Ticks)
        Dim temp As Integer
        Dim list As ArrayList = New ArrayList
        list.Add(CInt(Int((max * Rnd(Now.Ticks) + min))))
        For i As Integer = 0 To count - 2
            Do
                temp = CInt(Int((max * Rnd()) + min))
            Loop Until Not list.Contains(temp)
            list.Add(temp)
        Next
        Return list
    End Function

    Private Sub LoadImageGuard()
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser Is Nothing Then
            Response.Redirect("login.aspx")
        End If
        Dim imgCount As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("PhisingGuardImageCount").Trim)
        Dim ImageId As Integer
        Dim i As Integer
        Dim idUser As String = "0"
        Dim listImage As ArrayList = LoadGeneralImage()
        If imgCount < listImage.Count Then
            Dim list As ArrayList = GenerateImgRandom(0, listImage.Count - 1, imgCount)
            Dim newListImage As ArrayList = New ArrayList
            For Each item As Integer In list
                newListImage.Add(listImage(item))
            Next
            listImage = newListImage
        End If

        Dim privateListImage As ArrayList = LoadPrivateImage(objUser.ID)
        For Each item As PhisingGuardImage In privateListImage
            listImage.Add(item)
        Next
        Session.Add("IMAGE_GUARD", listImage)

        If listImage.Count > 0 Then
            If Not objUser.UserProfile Is Nothing Then
                ImageId = objUser.UserProfile.ImageID
                For Each item As PhisingGuardImage In listImage
                    i += 1
                    If item.ID = ImageId Then
                        Session.Add("IMAGE_INDEX", i - 1)
                        Exit For
                    End If
                Next
            Else
                Session.Add("IMAGE_INDEX", listImage.Count - 1)
            End If

            If ImageId > 0 Then
                idUser = ImageId
            Else
                Try
                    idUser = CType(listImage(listImage.Count - 1), PhisingGuardImage).ID.ToString
                Catch ex As Exception
                    idUser = 0
                    MessageBox.Show("Index lost")
                End Try
            End If
        Else
            btnNext.Enabled = False
            btnPrevious.Enabled = False
        End If
        InitilizeImageButton()
        Me.photoView.ImageUrl = "WebResources/GetImage.aspx?id=" & idUser
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim isExpired As String = String.Empty
            isExpired = Request.QueryString.Item("isExpired")
            If isExpired = "true" Then
                MessageBox.Show("Maaf hingga saat ini anda belum mengaktivasi nomer HP Anda, Silahkan daftar ulang untuk mendapatkan kode aktivasi baru, lalu aktivasi account Anda dengan mengikuti pesan pada SMS aktivasi tersebut.")
            End If
            LoadImageGuard()
            FillObjUser()
            Session.Add("CaptchaImageText", GenerateRandomCode)
            ImgCaptcha.ImageUrl = "JpegImage.aspx"

            Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
            If objUser Is Nothing Then
                objUser = CType(ss.GetSession("TEMPORARYUSER"), UserInfo)
            End If
            If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER.LEASING Then
                LnkTerms.Attributes.Add("onclick", "window.open('euladsf.html','disclaimer','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=700,height=500'); return false;")
            Else
                LnkTerms.Attributes.Add("onclick", "window.open('eula2.html','disclaimer','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=700,height=500'); return false;")
            End If
        Else

        End If
        btnLogout.Attributes.Add("onclick", "return confirm('Anda yakin akan keluar dari halaman ini ?');")
    End Sub

    Private Sub btnKirimKodeAktivasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            SendActivateCode()
            MessageBox.Show("Token baru berhasil dikirimkan ke HP anda")
        Catch ex As Exception
            MessageBox.Show("Proses Token baru tidak berhasil")
        End Try
    End Sub

    Private Sub SendActivateCode()
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUser Is Nothing Then
            If Not objUser.UserProfile Is Nothing Then
                If (objUser.HandPhone.Length > 0) And (objUser.UserProfile.ActivationCode.Length > 0) Then
                    SendSMS(objUser.HandPhone, objUser.UserProfile.ActivationCode)
                Else
                    MessageBox.Show("Handphone atau kode aktivasi tidak valid")
                End If
            End If
        End If
    End Sub

    Private Function LogBingo(ByVal listBingo As ArrayList, ByVal x As Int16, ByVal y As Int16) As Boolean
        Dim result As Boolean = False
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser Is Nothing Then
            Return False
        End If
        Dim _bingo As KTB.DNet.Domain.Bingo = New KTB.DNet.Domain.Bingo
        _bingo.DimensiX = x
        _bingo.DimensiY = y
        _bingo.SerialNumber = New RandomGenerator().GenarateRandom(15)
        _bingo.Handphone = objUser.HandPhone
        _bingo.ExpiredCount = 30
        For Each item As String() In listBingo
            Dim matrix As BingoMatrix = New BingoMatrix
            matrix.PosisiY = CType(item(0), String)
            matrix.PosisiX = CType(item(1), String)
            matrix.Code = CType(item(2), String)
            _bingo.BingoMatrixs.Add(matrix)
        Next
        Dim _bingoFacade As BingoFacade = New BingoFacade(User)
        Dim i As Int16 = _bingoFacade.Insert(_bingo, objUser.UserProfile, User.Identity.Name, True)
        If i = 0 Then
            result = True
        End If
        Return result
    End Function

    Private Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Session.RemoveAll()
        FormsAuthentication.SignOut()
        Response.Redirect("login.aspx")
        'RegisterStartupScript("OpenNewWindow", "<script>OpenNewWindow('')</script>")
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        UploadImage()
    End Sub

    Private Sub UploadImage()
        Dim objPhisingGuardImage As PhisingGuardImage = New PhisingGuardImage
        Dim objPhisingGuardImageFacade As PhisingGuardImageFacade = New PhisingGuardImageFacade(User)
        Dim imageFile As Byte()
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser Is Nothing Then
            MessageBox.Show("Gagal Upload Image, Session ada sudah habis, silahkan login ulang.")
            Return
        End If
        Try
            Dim files As Byte() = UploadFile()
            If Not files Is Nothing Then
                imageFile = files
                objPhisingGuardImage.Image = imageFile
                objPhisingGuardImage.ImageCode = "Uploded User Image"
                objPhisingGuardImage.Type = EnumSE.ImageType.Personal
                objPhisingGuardImage.UploadedUserID = objUser.ID
                Dim existingList As ArrayList = LoadPrivateImage(objUser.ID)
                If existingList.Count > 0 Then
                    Dim oldObj As PhisingGuardImage = CType(existingList(0), PhisingGuardImage)
                    oldObj.Image = imageFile
                    objPhisingGuardImageFacade.Update(oldObj)
                Else
                    objPhisingGuardImageFacade.Insert(objPhisingGuardImage)
                End If
                LoadImageGuard()
            Else
                MessageBox.Show("File belum di pilih/tidak valid")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim photoSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("PhotoSize"))
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(TrTrainee.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= photoSize) And (file.ContentLength > 0)
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
                    Dim ByteRead(PhisingGuardImage.MAX_PHOTO_SIZE) As Byte
                    Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, PhisingGuardImage.MAX_PHOTO_SIZE)
                    If ReadCount = 0 Then
                        Throw New InvalidConstraintException(SR.DataNotFound("Image"))
                    End If
                    ReDim nResult(ReadCount)
                    Array.Copy(ByteRead, nResult, ReadCount)
                Else
                    Throw New DataException("Foto harus file gambar dan maksimum 30KB")
                End If
            Catch
                Throw
            End Try
        End If
        Return nResult
    End Function

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        Dim index As Integer = Session.Item("IMAGE_INDEX")
        Dim listImage As ArrayList = CType(Session.Item("IMAGE_GUARD"), ArrayList)
        Dim newIndex As Integer = 0
        newIndex = index - 1
        If newIndex <= 0 Then
            newIndex = listImage.Count - 1
        End If
        Session.Add("IMAGE_INDEX", newIndex)
        Dim idUser As String = "0"
        Try
            idUser = CType(listImage(newIndex), PhisingGuardImage).ID.ToString
        Catch ex As Exception
            MessageBox.Show("Index lost")
        End Try
        InitilizeImageButton()
        Me.photoView.ImageUrl = "WebResources/GetImage.aspx?id=" & idUser
    End Sub

    Private Sub InitilizeImageButton()
        'Try
        '    Dim index As Integer = Session.Item("IMAGE_INDEX")
        '    Dim count As Integer = CType(Session.Item("IMAGE_GUARD"), ArrayList).Count
        '    If count < 1 Then
        '        btnNext.Enabled = False
        '        btnPrevious.Enabled = False
        '    Else
        '        btnPrevious.Enabled = True
        '        btnNext.Enabled = True
        '        If index = 0 Then
        '            btnPrevious.Enabled = False
        '        End If

        '        If index = count - 1 Then
        '            btnNext.Enabled = False
        '        End If
        '    End If
        'Catch ex As Exception
        '    btnPrevious.Enabled = True
        '    btnNext.Enabled = True
        'End Try
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim index As Integer = Session.Item("IMAGE_INDEX")
        Dim listImage As ArrayList = CType(Session.Item("IMAGE_GUARD"), ArrayList)
        Dim newIndex As Integer = 0
        newIndex = index + 1
        If newIndex = listImage.Count Then
            newIndex = 0
        End If
        InitilizeImageButton()
        Session.Add("IMAGE_INDEX", newIndex)
        Dim idUser As String
        Try
            idUser = CType(listImage(newIndex), PhisingGuardImage).ID.ToString
        Catch ex As Exception
            idUser = "0"
            MessageBox.Show("Index lost")
        End Try
        Me.photoView.ImageUrl = "WebResources/GetImage.aspx?id=" & idUser
    End Sub

    Private Sub btnDaftar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDaftar.Click
        Dim catchaText As String = String.Empty
        catchaText = CStr(Session.Item("CaptchaImageText"))
        If CodeNumberTextBox.Text.ToUpper.Trim <> catchaText.Trim.ToUpper Then
            Session.Add("CaptchaImageText", GenerateRandomCode)
            MessageBox.Show("Kode Captcha yang anda masukan salah")
        End If
        If Not Sms.IsSMSGatewayLive Then
            MessageBox.Show("Data Anda saat ini tidak dapat diproses, Silahkan coba lagi atau hubungi D-NET Admin.")
            Return
        End If

        If txtHP.Text.Trim = String.Empty Then
            MessageBox.Show("Nomor Handphone tidak boleh kosong")
            Return
        End If

        Dim oldUserInfo As UserInfo = GetActivationCode(txtHP.Text)
        Dim actCode As String = String.Empty
        If oldUserInfo.ID > 0 Then
            actCode = oldUserInfo.UserProfile.ActivationCode
            If actCode = String.Empty Then
                actCode = oldUserInfo.UserProfile.TempActivationCode
            End If
            RegisterUser(actCode, oldUserInfo)
        Else
            RegisterUser(String.Empty, New UserInfo)
        End If

        'Session.Remove("TEMPORARYUSER")
        'Session.Remove("TEMPORARYUSERPROFILE")
        'Session.Remove("ACTCODE")
        'Session.Remove("BINGO")
        'Session.Remove("IMAGE")


    End Sub

    Private Sub SendSMS(ByVal hp As String, ByVal message As String)
        'Sms.Sendto(hp, message)

        Dim otpfunc As New OTPFunction

        otpfunc.SendSMSNotif(hp, message)
        If (Not otpfunc.boolReturn) Then
           
        End If
    End Sub

    Private Sub RegisterUser(ByVal ActCode As String, ByVal oldUser As UserInfo)
        Dim isBingoLost As Boolean = False
        Dim objUser As UserInfo = PopulateUser()
        Dim objUserProfile As UserProfile = PopulateUserProfile()
        If IsNothing(objUserProfile) Then
            Return
        End If
        Dim _userInfoFacade As UserInfoFacade = New UserInfoFacade(User)
        Dim i As Integer = -1
        Try
            If ActCode = String.Empty Then
                objUserProfile.TempActivationCode = GenerateActivationCode()
                objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Pending
                objUserProfile.ActivationCode = objUserProfile.TempActivationCode
                objUserProfile.ActivationSentTime = Now
                'objUserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register
                i = _userInfoFacade.RegisterUser(objUser, objUserProfile)
                'TODO OPEN REMARK 

                'farid Modification 23 Juli 2018
                '----------------------------------------------------------------------------------------------------------------------------------------------------------
                'SendSMS(objUser.HandPhone, enumSMS.GetContentMessage(enumSMS.ContentMessage.ActivationCodeNotification, objUserProfile))

                Dim otpgenerator As New OTPFunction
                Dim OTPCode As String = otpgenerator.GenerateOTPCode

                Dim result As Integer = otpgenerator.func_generateCodeOTP(objUser.HandPhone)

                If (Not otpgenerator.boolReturn) Then
                    MessageBox.Show("Pengiriman SMS Gagal Silakan Hubungi Administrator Anda")
                    Return
                End If

                If result <= 0 Then

                Else
                    objUser = New UserInfoFacade(User).Retrieve(objUser.ID)
                    ss.SetSession("LOGINUSERINFO", objUser)
                    Session.Remove("IMAGE_GUARD")
                    Session.Remove("IMAGE_INDEX")

                    Dim strMess As String = "Account Anda sudah didaftarkan silahkan cek dan segera balas kode aktivasi yang dikirimkan ke HP Anda"
                    'Response.Redirect("frmForgetPasswordConfirmation.aspx?msg2Login=" & strMess)
                    'Server.Transfer("FrmUserActivationOTP.aspx", False)
                    ss.SetSession("OTPReload", False)
                    Response.Redirect("FrmUserActivationOTP.aspx?Proses=RegistrasiUser")
                End If

                '----------------------------------------------------------------------------------------------------------------------------------------------------------

                

            Else

                objUser = New UserInfoFacade(User).Retrieve(objUser.ID)
                objUser.HandPhone = txtHP.Text
                objUser.Email = txtEmail.Text
                ss.SetSession("LOGINUSERINFO", objUser)

                objUserProfile.TempActivationCode = String.Empty
                objUserProfile.ActivationCode = ActCode
                'objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active
                objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Pending
                objUserProfile.ActivationSentTime = oldUser.UserProfile.ActivationSentTime
                objUserProfile.Bingo = oldUser.UserProfile.Bingo
                'objUserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register
                Session.Add("TEMPORARYUSER", objUser)
                Session.Add("TEMPORARYUSERPROFILE", objUserProfile)
                Session.Add("ACTCODE", ActCode)
                Session.Add("BINGO", oldUser.UserProfile.Bingo)
                Session.Add("IMAGE", objUserProfile.ImageID)

                Dim tempProfile As UserProfile = objUserProfile
                tempProfile.TempActivationCode = String.Empty
                tempProfile.ActivationCode = String.Empty
                tempProfile.ActivationStatus = EnumSE.ActivationCodeStatus.NotActive
                tempProfile.ActivationSentTime = oldUser.UserProfile.ActivationSentTime
                tempProfile.Bingo = Nothing
                tempProfile.RegistrationStatus = EnumSE.RegistrationStatus.NotRegister
                i = _userInfoFacade.RegisterUserWithoutStatus(objUser, tempProfile)


                Session.Remove("IMAGE_GUARD")
                Session.Remove("IMAGE_INDEX")

                'farid Modification 23 Juli 2018
                '----------------------------------------------------------------------------------------------------------------------------------------------------------
                'SendSMS(objUser.HandPhone, enumSMS.GetContentMessage(enumSMS.ContentMessage.ActivationCodeNotification, objUserProfile))

                Dim otpgenerator As New OTPFunction
                Dim OTPCode As String = otpgenerator.GenerateOTPCode

                Dim result As Integer = otpgenerator.func_generateCodeOTP(objUser.HandPhone)

                If (Not otpgenerator.boolReturn) Then
                    MessageBox.Show("Pengiriman SMS Gagal Silakan Hubungi Administrator Anda")
                    Return
                End If

                If result <= 0 Then

                Else
                    objUser = New UserInfoFacade(User).Retrieve(objUser.ID)
                    ss.SetSession("LOGINUSERINFO", objUser)
                    Session.Remove("IMAGE_GUARD")
                    Session.Remove("IMAGE_INDEX")

                    Dim strMess As String = "Account Anda sudah didaftarkan silahkan cek dan segera balas kode aktivasi yang dikirimkan ke HP Anda"
                    'Response.Redirect("frmForgetPasswordConfirmation.aspx?msg2Login=" & strMess)
                    'Server.Transfer("FrmUserActivationOTP.aspx", False)
                    ss.SetSession("OTPReload", False)
                    Response.Redirect("FrmUserActivationOTP.aspx?Proses=RegistrasiUser", True)
                End If

                'MessageBox.Show("Berhasil")
                'Response.Redirect("frmActCodeVerification.aspx")
            End If

        Catch ex As Exception
            MessageBox.Show("Register User Failed")
        End Try
    End Sub

    Private Function IsUserProfileRegistered(ByVal hp As String) As Boolean
        Dim oldActivateCode As UserInfo = GetActivationCode(hp)
        If Not oldActivateCode.UserProfile Is Nothing Then
            If oldActivateCode.UserProfile.ActivationCode = String.Empty Then
                If oldActivateCode.UserProfile.TempActivationCode = String.Empty Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return True
        End If
    End Function

    Private Function PopulateUser() As UserInfo
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUser Is Nothing Then
            objUser.Email = txtEmail.Text.Trim
            objUser.HandPhone = txtHP.Text.Trim
            objUser.Question = txtPertanyaan.Text.Trim
            objUser.Answer = KTB.DNet.UI.Helper.DNetEncryption.SymmetricEncrypt(txtJawaban.Text.Trim, txtPertanyaan.Text.Trim)
            Return objUser
        End If
        Return Nothing
    End Function

    Private Function PopulateUserProfile() As UserProfile
        Dim imgList As ArrayList = CType(ss.GetSession("IMAGE_GUARD"), ArrayList)
        Dim index As Integer = ss.GetSession("IMAGE_INDEX")

        If index >= 0 Then
            Dim objPhisingGuard As PhisingGuardImage = CType(imgList(index), PhisingGuardImage)
            Dim objUserProfile As UserProfile = New UserProfile
            objUserProfile.ImageID = objPhisingGuard.ID
            objUserProfile.ImageDescription = txtDeskripsiGambar.Text.Trim
            objUserProfile.MotherName = txtNamaIbu.Text.Trim
            objUserProfile.BirthDate = icTglLahir.Value
            'objUserProfile.RegistrationStatus = -1
            'Dim RandGenerator As RandomGenerator = New RandomGenerator
            'objUserProfile.ActivationCode = RandGenerator.GetRandomNumeric(8)
            objUserProfile.ActivationStatus = -1
            objUserProfile.SessionID = HttpContext.Current.Session.SessionID
            objUserProfile.Question1 = lblPertanyaan1.Text
            objUserProfile.Question2 = lblPertanyaan2.Text
            objUserProfile.Question3 = lblPertanyaan3.Text
            objUserProfile.Question4 = lblPertanyaan4.Text
            objUserProfile.Question5 = lblPertanyaan5.Text

            objUserProfile.Answer1 = KTB.DNet.UI.Helper.DNetEncryption.SymmetricEncrypt(txtPertanyaan1.Text, objUserProfile.Question1)
            objUserProfile.Answer2 = KTB.DNet.UI.Helper.DNetEncryption.SymmetricEncrypt(txtPertanyaan2.Text, objUserProfile.Question2)
            objUserProfile.Answer3 = KTB.DNet.UI.Helper.DNetEncryption.SymmetricEncrypt(txtPertanyaan3.Text, objUserProfile.Question3)
            objUserProfile.Answer4 = KTB.DNet.UI.Helper.DNetEncryption.SymmetricEncrypt(txtPertanyaan4.Text, objUserProfile.Question4)
            objUserProfile.Answer5 = KTB.DNet.UI.Helper.DNetEncryption.SymmetricEncrypt(txtPertanyaan5.Text, objUserProfile.Question5)
            Return objUserProfile
        Else
            MessageBox.Show("Anda belum mengupload gambar")
            Return Nothing
        End If
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GotoMainMenu()
    End Sub

    Private Sub GotoMainMenu()
        Dim objDealer As Dealer = CType(ss.GetSession("DEALER"), Dealer)
        Dim UrlGeneral As String = "default_general.aspx?type=0"
        Dim UrlEULA As String = "frmUELA.aspx?type=0"
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Response.Redirect(UrlEULA)
            'RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlEULA + """)</script>")
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Response.Redirect(UrlGeneral)
            'RegisterStartupScript("OpenWindow", "<script>OpenFullScreenWindow(""" + UrlGeneral + """)</script>")
        End If
    End Sub

    Private Function GetActivationCode(ByVal hp As String) As UserInfo
        Dim result As UserInfo = New UserInfo
        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserInfo), "HandPhone", MatchType.Exact, hp))
        Dim userFacade As UserInfoFacade = New UserInfoFacade(User)
        Dim list As ArrayList = userFacade.Retrieve(criterias)
        For Each item As UserInfo In list
            If Not item.UserProfile Is Nothing Then
                'If item.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register And item.UserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active Then
                If item.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register Then
                    result = item
                    Exit For
                End If
            End If
        Next
        Return result
    End Function

    Private Function GenerateActivationCode() As String
        Dim count As Int16 = 1
        Dim result As String = String.Empty
        While count = 1
            Dim rndGen As RandomGenerator = New RandomGenerator
            result = rndGen.GetActivationCode(8)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(UserProfile), "ActivationCode", MatchType.Exact, result))
            criterias.opOr(New Criteria(GetType(UserProfile), "TempActivationCode", MatchType.Exact, result))
            criterias.opAnd(New Criteria(GetType(UserProfile), "TransitionActivationCode", MatchType.Exact, result))
            Dim userFacade As UserProfileFacade = New UserProfileFacade(User)
            Dim list As ArrayList = userFacade.Retrieve(criterias)
            If list.Count = 0 Then
                count += 1
            End If
        End While
        Return result
    End Function

    Private Sub SendingBingoCode(ByVal x As Int16, ByVal y As Int16)
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        Dim bingoHashing As HashingBingo = New HashingBingo(x, y, w)
        Dim sn = SerialValidation()
        bingoHashing.GenerateBingo()
        Dim sms As String = bingoHashing.BingoSMS
        Dim ContentSMS As String = enumSMS.GetContentMessage(enumSMS.ContentMessage.BingoCardNotification, objUser.UserProfile, sn, sms, expiredBingoCount)
        Dim listBingo As ArrayList = bingoHashing.Bingo
        If LogBingo(listBingo, x, y, sn) Then
            SendMessage(ContentSMS)
        End If
    End Sub

    Private Function LogBingo(ByVal listBingo As ArrayList, ByVal x As Int16, ByVal y As Int16, ByVal sn As String) As Boolean
        Dim result As Boolean = False
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)

        If objUser Is Nothing Then
            Return False
        End If

        Dim _bingo As KTB.DNet.Domain.Bingo = New KTB.DNet.Domain.Bingo
        _bingo.DimensiX = x
        _bingo.DimensiY = y
        _bingo.SerialNumber = sn
        _bingo.Handphone = objUser.HandPhone
        _bingo.ExpiredCount = expiredBingoCount
        For Each item As String() In listBingo
            Dim matrix As BingoMatrix = New BingoMatrix
            matrix.PosisiY = CType(item(0), String)
            matrix.PosisiX = CType(item(1), String)
            matrix.Code = CType(item(2), String)
            _bingo.BingoMatrixs.Add(matrix)
        Next
        Dim _bingoFacade As BingoFacade = New BingoFacade(User)
        Dim i As Int16 = _bingoFacade.Insert(_bingo, objUser.UserProfile, User.Identity.Name, True)
        If i = 0 Then
            result = True
        End If
        Return result
    End Function

    Private Sub SendMessage(ByVal msg As String)
        Dim HP As String = String.Empty
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUser Is Nothing Then
            If objUser.HandPhone <> String.Empty Then
                HP = objUser.HandPhone
            End If
        End If
        If HP <> String.Empty Then
            Dim otpfunc As New OTPFunction

            otpfunc.SendSMSNotif(HP, msg)
            If (Not otpfunc.boolReturn) Then
                'MessageBox.Show("Pengiriman SMS Gagal. Silahkan Menghubungi Administrator Anda")
                'Return
            End If
        End If
    End Sub

    Private Function SerialValidation() As String
        Dim _sn As String
        Dim random As RandomGenerator = New RandomGenerator
        Dim fBingo As BingoFacade = New BingoFacade(User)
        Do
            _sn = random.GetRandomNumeric(8)
        Loop Until fBingo.RetrieveBingoForValidation(_sn) = True
        Return _sn
    End Function

    Private Sub imgNext_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNext.Click
        Dim index As Integer = Session.Item("IMAGE_INDEX")
        Dim listImage As ArrayList = CType(Session.Item("IMAGE_GUARD"), ArrayList)
        Dim newIndex As Integer = 0
        newIndex = index + 1
        If newIndex = listImage.Count Then
            newIndex = 0
        End If
        InitilizeImageButton()
        Session.Add("IMAGE_INDEX", newIndex)
        Dim idUser As String
        Try
            idUser = CType(listImage(newIndex), PhisingGuardImage).ID.ToString
        Catch ex As Exception
            idUser = "0"
            MessageBox.Show("Index lost")
        End Try
        Me.photoView.ImageUrl = "WebResources/GetImage.aspx?id=" & idUser
    End Sub

    Private Sub imgBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBack.Click
        Dim index As Integer = Session.Item("IMAGE_INDEX")
        Dim listImage As ArrayList = CType(Session.Item("IMAGE_GUARD"), ArrayList)
        Dim newIndex As Integer = 0
        newIndex = index - 1
        If newIndex <= 0 Then
            newIndex = listImage.Count - 1
        End If
        Session.Add("IMAGE_INDEX", newIndex)
        Dim idUser As String = "0"
        Try
            idUser = CType(listImage(newIndex), PhisingGuardImage).ID.ToString
        Catch ex As Exception
            MessageBox.Show("Index lost")
        End Try
        InitilizeImageButton()
        Me.photoView.ImageUrl = "WebResources/GetImage.aspx?id=" & idUser
    End Sub
End Class
