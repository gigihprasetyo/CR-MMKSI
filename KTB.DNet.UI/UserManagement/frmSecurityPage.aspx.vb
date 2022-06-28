#Region "Imports"
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
Imports System.Configuration
Imports System.Configuration.ConfigurationSettings
#End Region

Public Class frmSecurityPage
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
    Protected WithEvents txtKodeAktivasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKodeActivate As System.Web.UI.WebControls.Label
    Protected WithEvents lblKataKunciLama As System.Web.UI.WebControls.Label
    Protected WithEvents Label22 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKataKunciLama As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKataKunciBaru As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKataKunciBaru As System.Web.UI.WebControls.TextBox
    'Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblKonfirmasiKataKunciBaru As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKonfirmasiKataKunciBaru As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rowKataKunciLama As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents icTglLahir As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator7 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator8 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator9 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator10 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator11 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator12 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator13 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator14 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents imgStar As System.Web.UI.HtmlControls.HtmlImage
    Protected WithEvents captchaImg As System.Web.UI.WebControls.Image
    Protected WithEvents CodeNumberTextBox As System.Web.UI.WebControls.TextBox
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents imgBack As System.Web.UI.WebControls.ImageButton
    Protected WithEvents imgNext As System.Web.UI.WebControls.ImageButton
    Protected WithEvents lblGantiHP As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblChangeHP As System.Web.UI.WebControls.Label
    Protected WithEvents lblPertanyaanNo4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblHPbaru As System.Web.UI.WebControls.Label
    Protected WithEvents btnCancelReg As System.Web.UI.WebControls.Button
    Protected WithEvents lbtOTP As System.Web.UI.WebControls.LinkButton
    Protected WithEvents isActivate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ckhIsActivated As System.Web.UI.WebControls.CheckBox
    Protected WithEvents CHKIsActive As System.Web.UI.WebControls.CheckBox
    Protected WithEvents LabelStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusActivated As System.Web.UI.WebControls.Label
    Protected WithEvents lblActStatus As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator15 As System.Web.UI.WebControls.RequiredFieldValidator



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
    Protected WithEvents Textbox7 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents photoView As System.Web.UI.WebControls.Image
    Protected WithEvents btnPrevious As System.Web.UI.WebControls.Button
    Protected WithEvents btnNext As System.Web.UI.WebControls.Button
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents photoSrc As System.Web.UI.HtmlControls.HtmlInputFile



    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variable"
    Private sessionHelper As New sessionHelper
    Private objUser As UserInfo
    Private confContext As ConfigurationContext
    Private dbAuthenticationProvider As DbAuthenticationProviderData
    Private userRoleMgr As UserRoleManager
    Private hashProvider As IHashProvider
    Private identity As IIdentity
    Private objUserInfo As UserInfo
    Private success As Boolean = False
    Private IsMerging As Boolean = False
    Private IsOnlyUpdatedProfile As Boolean = False

    Dim expiredBingoCount As String = KTB.DNet.Lib.WebConfig.GetValue("ExpiredCount").ToString
    Private ss As sessionHelper = New sessionHelper

#End Region

#Region "Private Function"

    Private Function GenerateRandomCode() As String
        Try
            Dim generator As RandomGenerator = New RandomGenerator
            Return generator.GenarateRandomCharacterOnly(5).ToUpper

        Catch ex As Exception
            Return "ACFDE"
        End Try
    End Function

    Private Function IsInTransitionProgress(ByVal hp As String) As Boolean
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUser Is Nothing Then
            If Not objUser.UserProfile Is Nothing Then
                If objUser.UserProfile.TransitionHP.Trim.ToUpper = hp.Trim.ToUpper Then
                    MessageBox.Show("HP yang anda masukan sedang dalam proses transisi, silahkan isi no HP lainnya.")
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Private Sub FillObjUser()
        btnCancelReg.Visible = False
        imgStar.Visible = False
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUser Is Nothing Then
            txtHP.Text = objUser.HandPhone
            txtEmail.Text = objUser.Email
            txtPertanyaan.Text = objUser.Question
            txtJawaban.Text = KTB.DNet.UI.Helper.DNetEncryption.SymmetricDecrypt(objUser.Answer, objUser.Question)
            If Not objUser.UserProfile Is Nothing Then
                icTglLahir.Value = objUser.UserProfile.BirthDate
                txtNamaIbu.Text = objUser.UserProfile.MotherName

                If Not IsNothing(objUser.UserProfile.Bingo) Then
                    Dim t1 As DateTime = objUser.UserProfile.Bingo.BingoValidUntil
                    Dim t2 As DateTime = System.DateTime.Now
                    If objUser.UserProfile.Bingo.ExpiredCount < 0 Then
                        imgStar.Visible = False
                    Else
                        If DateTime.Compare(t1, t2) >= 0 Then
                            Dim BingoReminderDay = KTB.DNet.Lib.WebConfig.GetValue("BingoReminderDay")
                            Dim diff1 As System.TimeSpan
                            diff1 = t1.Subtract(t2)
                            Dim days As Integer = diff1.Days + 1
                            Dim LoginMode As String = String.Empty
                            LoginMode = KTB.DNet.Lib.WebConfig.GetValue("LoginMode")
                            If LoginMode = "LIVE" Then
                                If days <= BingoReminderDay Then
                                    imgStar.Visible = True
                                Else
                                    imgStar.Visible = False
                                End If
                            End If
                        End If
                    End If

                End If

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
                If objUser.UserProfile.ActivationStatus <> EnumSE.ActivationCodeStatus.Active Then
                    btnNewToken.Enabled = False
                    btnKirimKodeAktivasi.Enabled = False
                End If
                If objUser.UserProfile.TransitionHP.Trim = String.Empty Then
                    btnCancelReg.Visible = False
                    lblChangeHP.Visible = False
                Else
                    btnCancelReg.Visible = True
                    lblChangeHP.Visible = True
                    lblChangeHP.Text = "--> " & objUser.UserProfile.TransitionHP.Trim
                End If
            Else
                lblPertanyaan1.Text = KTB.DNet.Lib.WebConfig.GetValue("Question1").ToString
                lblPertanyaan2.Text = KTB.DNet.Lib.WebConfig.GetValue("Question2").ToString
                lblPertanyaan3.Text = KTB.DNet.Lib.WebConfig.GetValue("Question3").ToString
                lblPertanyaan4.Text = KTB.DNet.Lib.WebConfig.GetValue("Question4").ToString
                lblPertanyaan5.Text = KTB.DNet.Lib.WebConfig.GetValue("Question5").ToString
            End If
        End If
    End Sub

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
        criterias.opAnd(New Criteria(GetType(PhisingGuardImage), "UploadedUserID", MatchType.Exact, id))
        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PhisingGuardImage), "ID", Sort.SortDirection.ASC))
        Return New PhisingGuardImageFacade(User).Retrieve(criterias, sortColl)
    End Function

    Private Sub LoadImageGuard(ByVal id As Integer)
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser Is Nothing Then
            MessageBox.Show("Session Anda sudah habis silahkan login kembali")
            Return
        End If
        Dim imgCount As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("PhisingGuardImageCount").Trim)
        Dim ImageId As Integer
        Dim i As Integer = 0
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
        If listImage.Count < 1 Then
            Return
        End If


        If Not objUser.UserProfile Is Nothing Then
            ImageId = objUser.UserProfile.ImageID
            For Each item As PhisingGuardImage In listImage
                i += 1
                If item.ID = ImageId Then
                    Session.Add("IMAGE_INDEX", i - 1)
                    Exit For
                End If
            Next
            If ImageId > 0 Then
                idUser = ImageId
            Else
                Try
                    idUser = CType(listImage(listImage.Count - 1), PhisingGuardImage).ID.ToString
                Catch ex As Exception
                    MessageBox.Show("Index Lost")
                End Try
            End If

            If id > 0 Then
                Dim ind As Integer = GetIndex(id, listImage)
                Session.Add("IMAGE_INDEX", ind)
                Me.photoView.ImageUrl = "../WebResources/GetImage.aspx?id=" & id
            Else
                Dim ind As Integer = GetIndex(idUser, listImage)
                Session.Add("IMAGE_INDEX", listImage.Count - 1)
                Me.photoView.ImageUrl = "../WebResources/GetImage.aspx?id=" & idUser
            End If
        Else
            Try
                idUser = CType(listImage(listImage.Count - 1), PhisingGuardImage).ID.ToString
                Me.photoView.ImageUrl = "../WebResources/GetImage.aspx?id=" & idUser
            Catch ex As Exception
                MessageBox.Show("Index Lost")
            End Try
            Session.Add("IMAGE_INDEX", listImage.Count - 1)
        End If
    End Sub

    Function GetIndex(ByVal imageID As Integer, ByVal ImgList As ArrayList) As Integer
        Dim i As Integer = 0
        For Each item As PhisingGuardImage In ImgList
            If item.ID = imageID Then
                Return i
            End If
            i = +1
        Next
        Return 0
    End Function

    Private Sub LoadingPage()
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        Dim currUser As UserInfo = New UserInfoFacade(User).Retrieve(objUser.ID)
        Dim id As Integer = 0
        If Not currUser Is Nothing Then
            objUser.HandPhone = currUser.HandPhone
            ss.SetSession("LOGINUSERINFO", objUser)
            If Not currUser.UserProfile Is Nothing Then
                If currUser.UserProfile.ID > 0 Then
                    If currUser.UserProfile.ImageID > 0 Then
                        id = currUser.UserProfile.ImageID
                    End If
                End If
            End If

        End If

        initButton()
        FillObjUser()
        Session.Add("CaptchaImageText", GenerateRandomCode)
        captchaImg.ImageUrl = "../JpegImage.aspx"
    End Sub


    Private Sub RefreshCaptch()
        CodeNumberTextBox.Text = String.Empty
        Session.Add("CaptchaImageText", GenerateRandomCode)
        captchaImg.ImageUrl = "../JpegImage.aspx"
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            LoadingPage()
            LoadImageGuard(0)
        End If
        btnNewToken.Attributes.Add("onclick", "return confirm('Anda yakin akan mereset token ?');")
        btnKirimKodeAktivasi.Attributes.Add("onclick", "return confirm('Anda yakin akan mengirim kode aktivasi ?');")
    End Sub

    Private Sub btnKirimKodeAktivasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKirimKodeAktivasi.Click
        If Not IsCaptchaValid() Then
            Return
        End If
        Try
            If Sms.IsSMSGatewayLive Then
                SendActivateCode()
                MessageBox.Show("Kode Aktivasi sedang dikirimkan ke HP anda")
                LogTosyslog(" activation code has been sent to user handphone ", "activation-sent", "success", "web-security", "broadcast")
                'LogTosyslog("Kode Aktivasi sedang dikirimkan ke HP anda", "security-change-user", "success", "sms-security")

            Else
                MessageBox.Show("Data Anda saat ini tidak dapat diproses, Silahkan coba lagi atau hubungi D-NET Admin.")
                LogTosyslog(" SMS gateway is down, please check the device ", "sms-sent-not", "failed", "web-device", "listen")

            End If
        Catch ex As Exception
            MessageBox.Show("Kode Aktivasi tidak berhasil di kirimkan")
            LogTosyslog(" SMS gateway cant be contacted due to unknown problem ", "sms-sent-not", "failed", "web-device", "listen")

        End Try
        Session.Add("CaptchaImageText", GenerateRandomCode)
        captchaImg.ImageUrl = "../JpegImage.aspx"
        CodeNumberTextBox.Text = String.Empty
    End Sub


    Private Sub SendActivateCode()
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUser Is Nothing Then
            If Not objUser.UserProfile Is Nothing Then
                If (objUser.HandPhone.Length > 0) And (objUser.UserProfile.ActivationCode.Length > 0) Then


                    'Dim ContentSMS As String = enumSMS.GetContentMessage(enumSMS.ContentMessage.ActivationCodeNotification, objUser.UserProfile)

                    Dim actCode As String
                    If objUser.UserProfile.ActivationCode <> String.Empty Then
                        actCode = objUser.UserProfile.ActivationCode
                    Else
                        actCode = objUser.UserProfile.TempActivationCode
                    End If
                    Dim kode As String = "D-NET"
                    'Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("SmsKeyCode")
                    Try
                        kode = KTB.DNet.Lib.WebConfig.GetValue("HeaderKTB").ToString
                    Catch ex As Exception
                        kode = "D-NET"
                    End Try

                    kode = kode + " " + companyCode
                    ' Modified by Ikhsan, 20081008
                    ' Requested by Decky
                    ' Enable the ability to send the activation code by SMS
                    ' -----------------------------------------------------------
                    Dim ContentSMS As String = "Berikut ini adalah kode aktivasi Anda yang terdaftar di " & kode & "  adalah " & actCode

                    'SendSMS(objUser.HandPhone, ContentSMS)
                    If objUser.UserProfile.TransitionHP.Length > 0 Then
                        SendSMS(objUser.UserProfile.TransitionHP, ContentSMS)
                    Else
                        SendSMS(objUser.HandPhone, ContentSMS)
                    End If
                    ' -----------------------------------------------------------

                Else
                    MessageBox.Show("Handphone atau kode aktivasi tidak valid")
                End If
            End If
        End If
    End Sub

    Private Sub SendingBingoCode(ByVal x As Int16, ByVal y As Int16)
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser Is Nothing Then
            MessageBox.Show("Session anda sudah habis, Silahkan Login Ulang")
            Return
        End If
        If Sms.IsSMSGatewayLive Then
            Dim bingoHashing As HashingBingo = New HashingBingo(3, 7, 2)
            Dim sn = SerialValidation()
            bingoHashing.GenerateBingo()
            Dim smsBingo As String = bingoHashing.BingoSMS
            Dim ContentSMS As String = enumSMS.GetContentMessage(enumSMS.ContentMessage.BingoCardNotification, objUser.UserProfile, sn, smsBingo, expiredBingoCount)
            Dim listBingo As ArrayList = bingoHashing.Bingo
            If LogBingo(listBingo, x, y, sn) Then
                SendMessage(ContentSMS)
            End If
        Else
            MessageBox.Show("Data Anda saat ini tidak dapat diproses, Silahkan coba lagi atau hubungi D-NET Admin.")
        End If
    End Sub

    Private Function LogBingo(ByVal listBingo As ArrayList, ByVal x As Int16, ByVal y As Int16, ByVal sn As String) As Boolean
        Dim result As Boolean = False
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
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
        Dim i As Int16 = _bingoFacade.Insert(_bingo, objUser.UserProfile, User.Identity.Name, False)
        If i = 0 Then
            result = True
        End If
        Return result
    End Function

    Private Sub SendMessage(ByVal msg As String)
        Dim HP As String = ""
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUser Is Nothing Then
            If objUser.HandPhone <> String.Empty Then
                HP = objUser.HandPhone
            End If
        End If
        If HP.Length > 0 Then
            'Sms.Sendto(HP, msg)

            Dim otpfunc As New OTPFunction

            otpfunc.SendSMSNotif(HP, msg)
            If (Not otpfunc.boolReturn) Then

            End If
        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        UploadImage()
    End Sub

    Private Sub LoadImageIDForUpload(ByVal id As Integer)
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser Is Nothing Then
            MessageBox.Show("Session Anda sudah habis silahkan login kembali")
            Return
        End If
        Dim imgCount As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("PhisingGuardImageCount").Trim)
        Dim ImageId As Integer
        Dim i As Integer = 0
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
        If listImage.Count < 1 Then
            Return
        End If
        Try
            idUser = CType(listImage(listImage.Count - 1), PhisingGuardImage).ID.ToString
            Me.photoView.ImageUrl = "../WebResources/GetImage.aspx?id=" & idUser
        Catch ex As Exception
            MessageBox.Show("Index Lost")
        End Try
        Session.Add("IMAGE_INDEX", listImage.Count - 1)
    End Sub

    Private Sub UploadImage()
        Dim objPhisingGuardImage As PhisingGuardImage = New PhisingGuardImage
        Dim objPhisingGuardImageFacade As PhisingGuardImageFacade = New PhisingGuardImageFacade(User)
        Dim imageFile As Byte()
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser Is Nothing Then
            MessageBox.Show("Session anda sudah habis, Silahkan Login Kembali")
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
                Dim oldObj As PhisingGuardImage = New PhisingGuardImage
                If existingList.Count > 0 Then
                    oldObj = CType(existingList(0), PhisingGuardImage)
                    oldObj.Image = imageFile
                    objPhisingGuardImageFacade.Update(oldObj)
                Else
                    objPhisingGuardImageFacade.Insert(objPhisingGuardImage)
                End If

                LoadImageIDForUpload(oldObj.ID)
            Else
                MessageBox.Show("File belum di pilih/tidak valid")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(TrTrainee.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= PhisingGuardImage.MAX_PHOTO_SIZE) And (file.ContentLength > 0)
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
        Dim index As Integer = ss.GetSession("IMAGE_INDEX")
        Dim listImage As ArrayList = CType(ss.GetSession("IMAGE_GUARD"), ArrayList)
        Dim newIndex As Integer = 0
        newIndex = index - 1
        If newIndex <= 0 Then
            newIndex = listImage.Count - 1
        End If
        Session.Add("IMAGE_INDEX", newIndex)
        Dim idUser As String
        Try
            idUser = CType(listImage(newIndex), PhisingGuardImage).ID.ToString
        Catch ex As Exception
            idUser = "0"
            MessageBox.Show("index lost")
        End Try
        'InitilizeImageButton()
        Me.photoView.ImageUrl = "../WebResources/GetImage.aspx?id=" & idUser
    End Sub

    Private Sub InitilizeImageButton()
        Try
            Dim index As Integer = Session.Item("IMAGE_INDEX")
            Dim count As Integer = CType(Session.Item("IMAGE_GUARD"), ArrayList).Count
            btnPrevious.Enabled = True
            btnNext.Enabled = True
            If index = 0 Then
                btnPrevious.Enabled = False
            End If

            If index = count - 1 Then
                btnNext.Enabled = False
            End If
            If count < 1 Then
                btnPrevious.Enabled = False
                btnNext.Enabled = False
            End If
        Catch ex As Exception
            btnPrevious.Enabled = True
            btnNext.Enabled = True
        End Try
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim index As Integer = Session.Item("IMAGE_INDEX")
        Dim listImage As ArrayList = CType(Session.Item("IMAGE_GUARD"), ArrayList)
        Dim newIndex As Integer = 0
        If index < listImage.Count - 1 Then
            newIndex = index + 1
        End If
        If newIndex = listImage.Count Then
            newIndex = 0
        End If
        'InitilizeImageButton()
        Session.Add("IMAGE_INDEX", newIndex)
        Dim idUser As String = "0"
        Try
            idUser = CType(listImage(newIndex), PhisingGuardImage).ID.ToString
        Catch ex As Exception
            MessageBox.Show("index lost")
        End Try
        Me.photoView.ImageUrl = "../WebResources/GetImage.aspx?id=" & idUser
    End Sub

    Private Function IsuserInExistingHPList(ByVal id As Integer, ByVal hp As String) As Boolean
        Dim al As ArrayList = GetActivationCodeList(hp)
        For Each item As UserInfo In al
            If id = item.ID Then
                Return True
                Exit For
            End If
        Next
        Return False
    End Function

    Private Function IsCaptchaValid() As Boolean
        If CodeNumberTextBox.Text.ToUpper.Trim <> CStr(Session.Item("CaptchaImageText")).Trim.ToUpper Then
            Session.Add("CaptchaImageText", GenerateRandomCode)
            MessageBox.Show("Kode Captcha yang anda masukan salah")
            Return False
        End If
        Return True
    End Function

    Private Function isHPServer(ByVal noHP As String) As Boolean
        Dim hpServer As String = KTB.DNet.Lib.WebConfig.GetValue("HPSERVER")
        If noHP.Trim.ToUpper = hpServer.Trim.ToUpper Then
            MessageBox.Show("HP yang anda masukan adalah HP Server.")
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsImageValid() As Boolean
        Dim imgList As ArrayList = CType(Session.Item("IMAGE_GUARD"), ArrayList)
        If Not imgList Is Nothing Then
            If imgList.Count > 0 Then
                Return True
            End If
        End If
        MessageBox.Show("Belum ada Gambar Yang di Upload.")
        Return False
    End Function



    Private Sub btnDaftar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDaftar.Click
        If Not bDataValid() Then
            RefreshCaptch()
            Return
        End If
        If Not IsCaptchaValid() Then
            RefreshCaptch()
            Return
        End If

        If isHPServer(txtHP.Text) Then
            RefreshCaptch()
            Return
        End If

        If IsInTransitionProgress(txtHP.Text) Then
            RefreshCaptch()
            Return
        End If

        If Not IsImageValid() Then
            Return
        End If
        'Temporary comment by Firman
        'If Not Sms.IsSMSGatewayLive Then
        '    MessageBox.Show("Data Anda saat ini tidak dapat diproses, Silahkan coba lagi atau hubungi D-NET Admin.")
        '    Return
        'End If

        If txtHP.Text.Trim = String.Empty Then
            MessageBox.Show("Nomor Handphone tidak boleh kosong")
            Return
        End If

        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        Dim oldKodeAktivasiUser As UserInfo = GetActivationCode(txtHP.Text.Trim)
        Dim succes As Boolean = False
        If oldKodeAktivasiUser.ID > 0 Then
            If IsuserInExistingHPList(objUser.ID, txtHP.Text.Trim) Then
                IsOnlyUpdatedProfile = True
                RegisterUser(oldKodeAktivasiUser.UserProfile.ActivationCode, oldKodeAktivasiUser)
            Else
                If txtKodeAktivasi.Text <> String.Empty Then
                    If txtKodeAktivasi.Text.Trim.ToUpper = oldKodeAktivasiUser.UserProfile.ActivationCode.Trim.ToUpper Or txtKodeAktivasi.Text.Trim.ToUpper = oldKodeAktivasiUser.UserProfile.TransitionActivationCode.Trim.ToUpper Then
                        IsMerging = True
                        RegisterUser(txtKodeAktivasi.Text.Trim, oldKodeAktivasiUser)
                        succes = True
                    Else
                        MessageBox.Show("Kode Aktivasi tidak valid")
                    End If
                Else
                    InitEntryActivateCode(True)
                    MessageBox.Show("Silahkan Masukan Kode Aktivasi nomor HP baru yang ingin Anda daftarkan!")
                End If
            End If
        Else
            RegisterUser(String.Empty, New UserInfo)

        End If
        RefreshCaptch()
        If succes Then
            LoadingPage()
        End If
        CodeNumberTextBox.Text = String.Empty
    End Sub

    Private Sub RegisterUser(ByVal ActCode As String, ByVal oldUser As UserInfo)
        Dim isBingoLost As Boolean = False
        Dim objUser As UserInfo = PopulateUser()
        Dim objUserProfile As UserProfile = PopulateUserProfile()
        If objUserProfile Is Nothing Then
            success = False
            Return
        End If
        Dim _userInfoFacade As UserInfoFacade = New UserInfoFacade(User)
        Dim isPasswordtrue As Boolean
        Dim isFirstReg As Boolean = False
        If CheckTextBoxPassword(isPasswordtrue) Then
            Try
                If ActCode = String.Empty Then
                    Dim currUser As UserInfo = New UserInfoFacade(User).Retrieve(objUser.ID)
                    If Not currUser.UserProfile Is Nothing Then
                        If (currUser.UserProfile.ID > 0) And ((currUser.UserProfile.ActivationCode.Trim <> String.Empty) Or (currUser.UserProfile.TempActivationCode.Trim <> String.Empty)) Then
                            If Not currUser.UserProfile Is Nothing Then
                                ActCode = currUser.UserProfile.ActivationCode
                                objUserProfile.Bingo = currUser.UserProfile.Bingo
                                objUserProfile.TempActivationCode = currUser.UserProfile.TempActivationCode
                            End If
                            If Not currUser Is Nothing Then
                                objUser.HandPhone = currUser.HandPhone
                            End If
                            If (txtHP.Text.Trim.ToUpper <> objUser.HandPhone.Trim.ToUpper) Then
                                If (txtHP.Text.Trim.ToUpper <> objUserProfile.TransitionHP) Then
                                    objUserProfile.ActivationCode = ActCode
                                    objUserProfile.TransitionHP = txtHP.Text.Trim
                                    objUserProfile.TransitionProcessDate = Now
                                    objUserProfile.ActivationSentTime = Now
                                    objUserProfile.TransitionActivationCode = GenerateActivationCode()
                                    objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active
                                    objUserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register
                                    Try
                                        SendSMS(objUserProfile.TransitionHP, enumSMS.GetContentMessage(enumSMS.ContentMessage.ActivationCodeNotification, objUserProfile))
                                        LogTosyslog(" Activation code has been sent to user new handphone ", "activation-sent", "success", "web-security", "broadcast")
                                    Catch ex As Exception
                                        LogTosyslog(" Activation code cant be sent to user new handphone", "sms-sent-not", "failed", "web-device", "listen")
                                        MessageBox.Show(ex.Message)
                                        Return
                                    End Try
                                Else
                                    objUserProfile.ActivationCode = currUser.UserProfile.ActivationCode
                                    objUserProfile.TempActivationCode = currUser.UserProfile.TempActivationCode

                                    objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active
                                    objUserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register
                                End If
                            Else
                                objUserProfile.ActivationCode = currUser.UserProfile.ActivationCode
                                objUserProfile.TempActivationCode = currUser.UserProfile.TempActivationCode
                                objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active
                                objUserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register
                            End If
                        Else
                            isFirstReg = True
                            objUserProfile.TempActivationCode = GenerateActivationCode()
                            objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Pending
                            objUserProfile.ActivationCode = String.Empty
                            objUserProfile.ActivationSentTime = Now
                            objUserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register
                            Try
                                LogTosyslog2("User " & User.Identity.Name & " Update Mobile Phone")
                                'SendSMS(objUser.HandPhone, enumSMS.GetContentMessage(enumSMS.ContentMessage.ActivationCodeNotification, objUserProfile))
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                                Return
                            End Try
                        End If
                    Else
                        isFirstReg = True
                        objUserProfile.TempActivationCode = GenerateActivationCode()
                        objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Pending
                        objUserProfile.ActivationCode = String.Empty
                        objUserProfile.ActivationSentTime = Now
                        objUserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register
                        Try
                            LogTosyslog2("User " & User.Identity.Name & " Update Mobile Phone")
                            'SendSMS(objUser.HandPhone, enumSMS.GetContentMessage(enumSMS.ContentMessage.ActivationCodeNotification, objUserProfile))
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            Return
                        End Try
                    End If

                Else
                    objUserProfile.TempActivationCode = oldUser.UserProfile.TempActivationCode
                    objUserProfile.ActivationCode = ActCode
                    objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active
                    objUserProfile.ActivationSentTime = oldUser.UserProfile.ActivationSentTime
                    objUserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register
                    objUserProfile.Bingo = oldUser.UserProfile.Bingo
                End If

                Dim objUserInitial As UserInfo = _userInfoFacade.Retrieve(objUser.ID)
                Dim objUserProfileInitial As UserProfile = objUserInitial.UserProfile

                Dim i As Integer = _userInfoFacade.RegisterUser(objUser, objUserProfile)
                objUser = New UserInfoFacade(User).Retrieve(objUser.ID)

                Dim prefixMsg As String = "informasi "
                Dim sufixMsg As String = " telah diubah "
                If i <> -1 Then

                    If objUserInitial.Birthday <> objUser.Birthday Then
                        LogTosyslog(prefixMsg & "birthday user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg, "birthday-updated", "success", "web-security", "edit")
                    End If
                    If objUserInitial.Email <> objUser.Email Then
                        LogTosyslog(prefixMsg & "email user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg, "email-updated", "success", "web-security", "edit")
                    End If
                    If objUserInitial.HandPhone.Trim <> txtHP.Text.Trim Then
                        LogTosyslog(prefixMsg & "handphone user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg & " menjadi " & txtHP.Text.Trim, "handphone-updated", "success", "web-security", "edit")
                    End If
                    If objUserProfileInitial.MotherName <> objUserProfile.MotherName Then
                        LogTosyslog(prefixMsg & "mothername user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg, "mothername-updated", "success", "web-security", "edit")


                    End If
                    If objUserInitial.Question <> objUser.Question Then
                        LogTosyslog(prefixMsg & " secret question user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg, "secretq-updated", "success", "web-security", "edit")


                    End If
                    If objUserInitial.Answer <> objUser.Answer Then
                        LogTosyslog(prefixMsg & " secret answer user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg, "secreta-updated", "success", "web-security", "edit")


                    End If
                    If objUserProfileInitial.Answer1 <> objUserProfile.Answer1 Then
                        LogTosyslog(prefixMsg & "Answer1 user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg, "answer1-updated", "success", "web-security", "edit")


                    End If
                    If objUserProfileInitial.Answer2 <> objUserProfile.Answer2 Then
                        LogTosyslog(prefixMsg & "Answer2 user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg, "answer2-updated", "success", "web-security", "edit")


                    End If
                    If objUserProfileInitial.Answer3 <> objUserProfile.Answer3 Then
                        LogTosyslog(prefixMsg & "Answer3 user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg, "answer3-updated", "success", "web-security", "edit")


                    End If
                    If objUserProfileInitial.Answer4 <> objUserProfile.Answer4 Then
                        LogTosyslog(prefixMsg & "Answer4 user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg, "answer4-updated", "success", "web-security", "edit")


                    End If
                    If objUserProfileInitial.Answer5 <> objUserProfile.Answer5 Then
                        LogTosyslog(prefixMsg & "Answer5 user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg, "answer5-updated", "success", "web-security", "edit")


                    End If
                    If objUserProfileInitial.ImageDescription <> objUserProfile.ImageDescription Then
                        LogTosyslog(prefixMsg & "ImageDescription user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg & " menjadi " & objUserProfile.ImageDescription, "imagedesc-updated", "success", "web-security", "edit")

                    End If
                    If objUserProfileInitial.ImageID <> objUserProfile.ImageID Then
                        LogTosyslog(prefixMsg & "ImageID user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg & " menjadi " & objUserProfile.ImageID, "imageid-updated", "success", "web-security", "edit")

                    End If
                End If


                ss.SetSession("LOGINUSERINFO", objUser)
                If i <> -1 And isPasswordtrue = True Then
                    SavePassword()
                    LogTosyslog(prefixMsg & "new password user " & objUser.Dealer.DealerCode & "-" & objUser.UserName & sufixMsg, "password-updated", "success", "web-security", "edit")

                End If

                success = True
                If IsMerging Then
                    Try
                        UpdateUserProfile(objUser.ID)
                    Catch ex As Exception

                    End Try
                    Dim msg As String = "Sekarang anda dapat menggunakan Token yang ada di  handphone anda " & objUser.HandPhone & " untuk login ke dalam D-NET. Terima kasih"
                    success = True
                    MessageBox.Show(msg)
                Else
                    If IsOnlyUpdatedProfile Then
                        MessageBox.Show("Update Profile Berhasil.")
                    Else
                        If isFirstReg Then
                            MessageBox.Show("Selamat no HP anda sudah didaftarkan, Silahkan aktivasi HP anda dengan cara mengikuti petunjuk pada pesan SMS yang anda terima.")
                        Else
                            MessageBox.Show("Update Profile Berhasil. Untuk Sementara anda dapat login menggunakan HP lama, segera aktivasi HP baru Anda, jika dalam 24 jam tidak diaktifkan maka perubahan No HP anda di Batalkan dan No HP lama anda tetap berlaku.")
                        End If
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Register User Gagal " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub UpdateUserProfile(ByVal id As Integer)
        Dim currentUser As UserInfo = New UserInfoFacade(User).Retrieve(id)
        Dim currentUserProfile As UserProfile = currentUser.UserProfile
        currentUserProfile.TransitionHP = String.Empty
        currentUserProfile.TransitionActivationCode = String.Empty
        Dim x As Integer = New UserProfileFacade(User).Update(currentUserProfile)
        'LogTosyslog2("User " & User.Identity.Name & " Update User Profile")
    End Sub

    Private Sub InitEntryActivateCode(ByVal active As Boolean)
        lblKodeActivate.Visible = active
        txtKodeAktivasi.Visible = active
    End Sub

    Private Sub SendSMS(ByVal hp As String, ByVal message As String)
        'Sms.Sendto(hp, message)

        Dim otpfunc As New OTPFunction

        otpfunc.SendSMSNotif(hp, message)
        If (Not otpfunc.boolReturn) Then

        End If
    End Sub

    Private Sub RegisterUser(ByVal ActCode As String)

        Dim objUser As UserInfo = PopulateUser()
        Dim objUserProfile As UserProfile = PopulateUserProfile()
        Dim _userInfoFacade As UserInfoFacade = New UserInfoFacade(User)
        Dim status, isPasswordtrue As Boolean

        If CheckTextBoxPassword(isPasswordtrue) Then  ' Check Textbox and validasi old password 

            If IsUserProfileRegistered(txtHP.Text.Trim) Or (ActCode <> String.Empty) Then
                Try
                    If ActCode = String.Empty Then
                        objUserProfile.TempActivationCode = GenerateActivationCode()
                        objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Pending
                        objUserProfile.ActivationCode = String.Empty
                        objUserProfile.ActivationSentTime = Now
                    Else
                        objUserProfile.TempActivationCode = String.Empty
                        objUserProfile.ActivationCode = ActCode
                        objUserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active
                    End If

                    objUserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register

                    Dim i As Integer = _userInfoFacade.RegisterUser(objUser, objUserProfile)




                    If i <> -1 And isPasswordtrue = True Then
                        SavePassword()
                    End If

                    If ActCode = String.Empty Then
                        'SendSMS(objUser.HandPhone, enumSMS.GetContentMessage(enumSMS.ContentMessage.ActivationCodeNotification, objUserProfile))
                    End If
                    success = True

                Catch ex As Exception
                    MessageBox.Show("Register User Failed")
                End Try
            Else
                InitEntryActivateCode(True)
            End If
        Else
            MessageBox.Show("Simpan Password Gagal")
        End If
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
        If objUser Is Nothing Then
            MessageBox.Show("Session anda sudah habis, Silahkan login kembali")
            Return Nothing
        End If
        objUser.Email = txtEmail.Text.Trim
        objUser.HandPhone = txtHP.Text.Trim
        objUser.Question = txtPertanyaan.Text.Trim
        objUser.Answer = KTB.DNet.UI.Helper.DNetEncryption.SymmetricEncrypt(txtJawaban.Text.Trim, txtPertanyaan.Text.Trim)
        Return objUser
    End Function

    Private Function PopulateUserProfile() As UserProfile
        Dim imgList As ArrayList = CType(Session.Item("IMAGE_GUARD"), ArrayList)
        Dim index As Integer = Session.Item("IMAGE_INDEX")
        Dim objPhisingGuard As PhisingGuardImage
        Try
            objPhisingGuard = CType(imgList(index), PhisingGuardImage)
        Catch ex As Exception
            MessageBox.Show("Silahkan Refesh Halaman ini atau Tekan F5.")
            Return Nothing
        End Try


        Dim objUserProfile As UserProfile = New UserProfile
        objUserProfile.ImageID = objPhisingGuard.ID
        objUserProfile.ImageDescription = txtDeskripsiGambar.Text.Trim
        objUserProfile.MotherName = txtNamaIbu.Text.Trim
        objUserProfile.BirthDate = icTglLahir.Value
        objUserProfile.RegistrationStatus = -1
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
    End Function

    Private Sub initButton()
        Dim LoginMode As String = String.Empty
        LoginMode = KTB.DNet.Lib.WebConfig.GetValue("LoginMode")
        If Not LoginMode.Trim.ToUpper = "LIVE" Then
            btnKirimKodeAktivasi.Visible = False
            btnNewToken.Visible = False
        Else
            btnKirimKodeAktivasi.Visible = True
            btnNewToken.Visible = True
        End If
        CodeNumberTextBox.Text = String.Empty
        InitEntryActivateCode(False)
    End Sub

    Private Function GetActivationCode(ByVal hp As String) As UserInfo
        Dim result As UserInfo = New UserInfo
        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserInfo), "HandPhone", MatchType.Exact, hp))
        criterias.opOr(New Criteria(GetType(UserInfo), "UserProfile.TransitionHP", MatchType.Exact, hp))
        Dim userFacade As UserInfoFacade = New UserInfoFacade(User)
        Dim list As ArrayList = userFacade.Retrieve(criterias)
        For Each item As UserInfo In list
            If Not item.UserProfile Is Nothing Then
                If item.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register Then
                    result = item
                    Exit For
                End If
            End If
        Next
        Return result
    End Function

    Private Function GetActivationCodeList(ByVal hp As String) As ArrayList
        Dim al As ArrayList = New ArrayList
        Dim result As UserInfo = New UserInfo
        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserInfo), "HandPhone", MatchType.Exact, hp))
        Dim userFacade As UserInfoFacade = New UserInfoFacade(User)
        Dim list As ArrayList = userFacade.Retrieve(criterias)
        For Each item As UserInfo In list
            If Not item.UserProfile Is Nothing Then
                'If item.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register And item.UserProfile.ActivationStatus = EnumSE.ActivationCodeStatus.Active Then
                If item.UserProfile.RegistrationStatus = EnumSE.RegistrationStatus.Register Then
                    al.Add(item)
                End If
            End If
        Next
        Return al
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

    Private Sub btnNewToken_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewToken.Click
        If Not IsCaptchaValid() Then
            Return
        End If

        Dim X As String = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiX").ToString
        Dim Y As String = KTB.DNet.Lib.WebConfig.GetValue("BingoDimensiY").ToString
        Try
            SendingBingoCode(X, Y)
            MessageBox.Show("Token baru berhasil dikirimkan ke HP anda")
            LogTosyslog("new token has been sent to user HP", "token-sent", "success", "web-security", "edit")
        Catch ex As Exception
            MessageBox.Show("Token baru tidak berhasil dikirimkan ke HP anda")
            LogTosyslog("new token has been sent to user HP", "token-sent-not", "failed", "web-security", "read")
            LogTosyslog("new token has been sent to user HP", "sms-sent-not", "failed", "web-device", "listen")

        End Try
        Session.Add("CaptchaImageText", GenerateRandomCode)
        captchaImg.ImageUrl = "../JpegImage.aspx"
        CodeNumberTextBox.Text = String.Empty
    End Sub

    Private Function CheckTextBoxPassword(ByRef IsPassword As Boolean) As Boolean
        Dim objUserInfo As UserInfo = CType(Session.Item("LOGINUSERINFO"), UserInfo)
        If txtKataKunciLama.Text = "" And txtKataKunciBaru.Text = "" And txtKonfirmasiKataKunciBaru.Text = "" Then
            Return True
        ElseIf txtKataKunciLama.Text <> "" And txtKataKunciBaru.Text <> "" And txtKonfirmasiKataKunciBaru.Text <> "" Then
            If txtKataKunciBaru.Text = txtKonfirmasiKataKunciBaru.Text Then
                If Not IsPasswordValid() Then
                    MessageBox.Show("Kata kunci lama salah...!!")
                    CountPasswordFailed(objUserInfo)
                Else
                    IsPassword = True
                    Return True
                End If
            Else
                MessageBox.Show("Kata kunci baru tidak sama dengan kata kunci konfirmasi...!!")
                CountPasswordFailed(objUserInfo)
                Return False
            End If
        Else
            MessageBox.Show("Password tidak valid...!!")
            CountPasswordFailed(objUserInfo)
            Return False
        End If



    End Function

    Private Sub LogOut()
        Session.RemoveAll()
        FormsAuthentication.SignOut()
        RegisterStartupScript("OpenNewWindow", "<script>OpenNewWindow('login.aspx')</script>")
    End Sub

    Private Sub CountPasswordFailed(ByVal objUserInfo As UserInfo)
        If Not objUserInfo Is Nothing Then
            Dim objSessionHelper As sessionHelper = New sessionHelper
            Dim count As Integer = 0
            Try
                count = IIf(Not objSessionHelper.GetSession("PWDCOUNT") Is Nothing, _
                            CInt(objSessionHelper.GetSession("PWDCOUNT")), 0)
                count += 1
                If count = 5 Then
                    LockUser(objUserInfo)
                    count = 0
                    LogOut()
                End If
                objSessionHelper.SetSession("PWDCOUNT", count)
            Catch ex As Exception
                count = 0
            End Try
        End If
        'LogTosyslog2("User " & User.Identity.Name & " Gagal Login  ")
    End Sub

    Private Sub LockUser(ByVal objUser As UserInfo)
        If Not objUser Is Nothing Then
            Dim objLockUser As LockUser = New LockUser
            objLockUser.StartLock = Now
            objLockUser.UserID = objUser.ID
            Dim lockTime As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("LoginLock"))
            objLockUser.FinishLock = objLockUser.StartLock.AddMinutes(lockTime)
            objLockUser.IPAddress = HttpContext.Current.Request.UserHostAddress
            Dim objLockUserFacade As LockUserFacade = New LockUserFacade(User)
            Dim i As Integer = objLockUserFacade.Insert(objLockUser)

            '----------Send Email------------'
            Dim emailTo As String = KTB.DNet.Lib.WebConfig.GetValue("EmailAdminDNET")
            Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
            Dim msgEmailContent As String = enumSMS.GetContentEmail(enumSMS.ContentMessage.LoginFail, objUser).ToString
            SendEmail(emailTo, emailFrom, msgEmailContent)
            'LogTosyslog2("User " & User.Identity.Name & " Terkunci  ")
            LogTosyslog2(msgEmailContent)

        End If
    End Sub

    Private Sub SendEmail(ByVal emailTo As String, ByVal emailFrom As String, ByVal contentEmail As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet]", Mail.MailFormat.Html, contentEmail)
    End Sub

    Private Function IsPasswordValid() As Boolean
        Dim pwdBytes As Byte() = Encoding.Unicode.GetBytes(txtKataKunciLama.Text)

        Dim authenticated As Boolean = SecurityProvider.Authenticate(User.Identity.Name, pwdBytes, identity)

        If authenticated Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SavePassword()
        confContext = CType(ConfigurationManager.GetCurrentContext(), ConfigurationContext)
        Dim securitySetting As SecuritySettings = CType(confContext.GetConfiguration(SecuritySettings.SectionName), SecuritySettings)
        dbAuthenticationProvider = CType(securitySetting.AuthenticationProviders(0), DbAuthenticationProviderData)
        userRoleMgr = New UserRoleManager(dbAuthenticationProvider.Database, confContext)
        Dim hashProviderFac As HashProviderFactory = New HashProviderFactory(confContext)
        hashProvider = hashProviderFac.CreateHashProvider(dbAuthenticationProvider.HashProvider)
        Dim pwd As Byte() = hashProvider.CreateHash(Encoding.Unicode.GetBytes(txtKataKunciBaru.Text))
        Try
            Dim UserName As String
            If Request.QueryString("id") = String.Empty Then
                UserName = User.Identity.Name
            Else
                objUser = sessionHelper.GetSession("User")
                UserName = objUser.Dealer.ID.ToString.PadLeft(6, "0") & objUser.UserName
            End If
            userRoleMgr.ChangeUserPassword(UserName, pwd)
            LogTosyslog2("User " & User.Identity.Name & " Update User Password")
        Catch ex As Exception
            Throw ex
        End Try
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

    Private Function bDataValid() As Boolean
        If txtDeskripsiGambar.Text.Length > 8 Then
            MessageBox.Show("Deskripsi gambar maks. 8 karakter!")
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub imgNext_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNext.Click
        Dim index As Integer = Session.Item("IMAGE_INDEX")
        Dim listImage As ArrayList = CType(Session.Item("IMAGE_GUARD"), ArrayList)
        Dim newIndex As Integer = 0
        If index < listImage.Count - 1 Then
            newIndex = index + 1
        End If
        If newIndex = listImage.Count Then
            newIndex = 0
        End If
        'InitilizeImageButton()
        Session.Add("IMAGE_INDEX", newIndex)
        Dim idUser As String = "0"
        Try
            idUser = CType(listImage(newIndex), PhisingGuardImage).ID.ToString
        Catch ex As Exception
            MessageBox.Show("index lost")
        End Try
        Me.photoView.ImageUrl = "../WebResources/GetImage.aspx?id=" & idUser
    End Sub

    Private Sub imgBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBack.Click
        Dim index As Integer = ss.GetSession("IMAGE_INDEX")
        Dim listImage As ArrayList = CType(ss.GetSession("IMAGE_GUARD"), ArrayList)
        Dim newIndex As Integer = 0
        newIndex = index - 1
        If newIndex <= 0 Then
            newIndex = listImage.Count - 1
        End If
        Session.Add("IMAGE_INDEX", newIndex)
        Dim idUser As String
        Try
            idUser = CType(listImage(newIndex), PhisingGuardImage).ID.ToString
        Catch ex As Exception
            idUser = "0"
            MessageBox.Show("index lost")
        End Try
        'InitilizeImageButton()
        Me.photoView.ImageUrl = "../WebResources/GetImage.aspx?id=" & idUser
    End Sub

#End Region

    Private Sub lbtOTP_Click(sender As Object, e As EventArgs) Handles lbtOTP.Click
        Response.Redirect("~/FrmUserChangePhoneNoOTP.aspx", True)
    End Sub

    Private Sub CheckingPendingUpdate(ByVal objUserInfo As UserInfo)
        Dim objUserProfile As UserProfile = objUserInfo.UserProfile
        If Not objUserProfile Is Nothing Then
            If objUserProfile.TransitionHP.Trim <> String.Empty And objUserProfile.TransitionActivationCode.Trim <> String.Empty Then
                objUserProfile.TransitionHP = String.Empty
                objUserProfile.TransitionActivationCode = String.Empty
                objUserProfile.TransitionProcessDate = Now
                Try
                    Dim i As Integer = New UserProfileFacade(User).Update(objUserProfile)
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub btnCancelReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelReg.Click
        If Not IsCaptchaValid() Then
            Return
        End If
        Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUser Is Nothing Then
            Try

                CheckingPendingUpdate(objUser)
                LoadingPage()
                MessageBox.Show("Pembatalan perubahan HP berhasil")
                'LogTosyslog("Pembatalan perubahan HP berhasil", "info-change-user", "success")
                LogTosyslog("handphone transition is being cancelled by user", "transition-cancelled", "success", "web-security", "reset")

            Catch ex As Exception
                MessageBox.Show("Pembatalan perubahan HP tidak berhasil")
                'LogTosyslog("Pembatalan perubahan HP tidak berhasil", "info-change-user", "failed")
                LogTosyslog("handphone transition is being cancelled by user but system refused", "transition-cancelled-not", "failed", "web-security", "read")

            End Try
        End If
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender

    End Sub

    Private Sub LogTosyslog2(ByVal message As String)
        Dim strLog As Boolean = KTB.DNet.Lib.WebConfig.GetValue("EnableSyslog")
        If strLog Then
            Dim objSyslog As SyslogParameter = New SyslogParameter(User)
            Dim m As KTB.DNet.Lib.SysLogXMLMessage = New KTB.DNet.Lib.SysLogXMLMessage
            m.Action = "Change User Security Info"
            m.SubBlockName = "User Management"
            m.FullMessage = message
            m.ModuleName = "General"
            m.Pages = HttpContext.Current.Request.Url.LocalPath
            m.RemoteIPAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
            m.StatusResult = "Sukses"
            m.Status = KTB.DNet.[Lib].DNetLogFormatStatus.Direct
            m.BlockName = "Role"

            Try
                m.UserName = IIf(User.Identity.Name.Length >= 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name)
            Catch ex As Exception
                m.UserName = "Wb-Usr"
            End Try
            objSyslog.LogError(m)
        End If
    End Sub



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
            m.BlockName = "private-data"


            Try
                m.UserName = IIf(User.Identity.Name.Length > 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name).ToLower
            Catch ex As Exception
                m.UserName = "Wb-Usr"
            End Try
            m.Dealer = CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode.ToLower
            objSyslog.LogError(m)
        End If
    End Sub


    '    Private Sub LogTosyslog(ByVal message As String, ByVal sbMsg As String, ByVal rslMsg As String)
    '        Dim strLog As Boolean = KTB.DNet.Lib.WebConfig.GetValue("EnableSyslog")
    '        If strLog Then
    '            Dim objSyslog As SyslogParameter = New SyslogParameter(User)
    '            Dim m As KTB.DNet.Lib.SysLogXMLMessage = New KTB.DNet.Lib.SysLogXMLMessage
    '            m.Action = "reset"
    '            m.SubBlockName = "user management"
    '            m.FullMessage = message.ToLower
    '            m.ModuleName = "web-sysadm"
    '            m.Pages = HttpContext.Current.Request.Url.LocalPath
    '            m.RemoteIPAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
    '            m.StatusResult = rslMsg.ToLower
    '            m.Status = KTB.DNet.[Lib].DNetLogFormatStatus.Direct
    '            m.BlockName = sbMsg.ToLower
    '            m.UserName = IIf(User.Identity.Name.Length >= 6, Right(User.Identity.Name, User.Identity.Name.Length - 6), User.Identity.Name).ToLower
    '            m.Dealer = CType(Session("LOGINUSERINFO"), UserInfo).Dealer.DealerCode.ToLower
    '            objSyslog.LogError(m)
    '        End If
    '    End Sub

  
End Class
