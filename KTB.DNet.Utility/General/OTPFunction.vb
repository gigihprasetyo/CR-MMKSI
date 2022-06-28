#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/11/2005 - 11:10:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System.Net.Http
Imports System
Imports System.Web
Imports System.Threading.Tasks
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Reflection
Imports System.Configuration
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.BusinessForum
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Training
Imports System.Security.Principal
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.Sparepart
'Imports KTB.DNet.DataMapper.Framework
'Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.Collections.Generic
Imports System.Collections
Imports KTB.DNet.BusinessFacade.PageHelper
Imports KTB.DNet.Lib
Imports Microsoft.VisualBasic.ApplicationServices

Imports System.Net
Imports Newtonsoft.Json
Imports System.Linq
#End Region

Namespace KTB.DNet.Utility

    Public Class OTPFunction
        Const OTPInterval As Integer = 5
        Private UserNameOTP As String
        Private PasswordOTP As String
        Private URLAPI As String
        Private ClintIDOTP As String
        Private strProx As String
        Private strPorxPort As String

        Private _boolReturn As Boolean = False
        Public Property boolReturn As Boolean
            Get
                Return _boolReturn
            End Get
            Set(ByVal value As Boolean)
                _boolReturn = value
            End Set
        End Property

        Public Shared Function GenerateOTPCode() As String
            Dim alphabets As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            Dim small_alphabets As String = "abcdefghijklmnopqrstuvwxyz"
            Dim numbers As String = "1234567890"

            Dim characters As String = numbers

            characters += Convert.ToString(alphabets) & numbers

            Dim length As Integer = Integer.Parse(6)
            Dim otp As String = String.Empty
            For i As Integer = 0 To length - 1
                Dim character As String = String.Empty
                Do
                    Dim index As Integer = New Random().Next(0, characters.Length)
                    character = characters.ToCharArray()(index).ToString()
                Loop While otp.IndexOf(character) <> -1
                otp += character
            Next
            Return otp
        End Function

        Public Shared Sub SendSMS(ByVal hp As String, ByVal message As String)
            Sms.Sendto(hp, message)
        End Sub

        Public Shared Function PopulateUser(ByVal strPhone As String) As UserInfo
            Dim ss As SessionHelper = New SessionHelper
            Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
            Dim result As Integer

            If Not objUser Is Nothing Then

                objUser.HandPhone = strPhone

                result = New UserInfoFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing))).Update(objUser)
                If result > 0 Then
                    Return objUser
                End If
            End If
            Return Nothing
        End Function

        'untuk reset password
        Public Sub generateCodeOTP(ByVal strPhone As String, ByVal objUser As UserInfo)
            Dim sess As SessionHelper = New SessionHelper
            Dim otpgenerator As New OTPFunction
            Dim OTPCode As String = GenerateOTPCode()
            Dim strOTP As String

            Try
                'objUser = PopulateUser(strPhone)
                'If objUser IsNot Nothing Then

                strOTP = "Kode OTP D-NET MMKSI Anda : "
                strOTP = strOTP + OTPCode + vbCrLf
                strOTP = strOTP + "Silahkan Masukan Kode ini di Layar OTP D-NET." + vbCrLf
                strOTP = strOTP + " Kode OTP Valid s/d "
                strOTP = strOTP + DateAdd(DateInterval.Minute, OTPInterval, Now).ToString("dd/MMMM/yyyy HH:mm:ss")

                'SendSMS(strPhone, strOTP)

                Task.Run(Function() SendSMSAPIOTP(strPhone, strOTP)).Wait()

                If (boolReturn) Then

                    Dim objOTPFac As New OTPFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
                    Dim objOTP As New KTB.DNet.Domain.OTP

                    With objOTP
                        .UserInfoID = objUser.ID
                        .ProcessType = enumSMS.ContentMessage.ResetTokenProcess
                        .NumberDestination = objUser.HandPhone
                        .ChallengeCode = OTPCode
                        .SMSValue = strOTP
                        .Status = 1
                        .RowStatus = CType(DBRowStatus.Active, Short)
                    End With

                    Dim result As Integer

                    result = objOTPFac.UpdateStatusOTP(objUser.ID, objUser.HandPhone)

                    result = objOTPFac.Insert(objOTP)

                    If result <= 0 Then

                    Else
                        objOTP.ID = result
                        sess.SetSession("OTPLog", objOTP)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Update Nomor Telpon Gagal")
            End Try

        End Sub

        'untuk reset token
        Public Sub generateCodeOTP(ByVal strPhone As String)
            'Dim objUser As UserInfo
            Dim otpgenerator As New OTPFunction
            Dim OTPCode As String = GenerateOTPCode()
            Dim strOTP As String
            Dim ss As SessionHelper = New SessionHelper
            Dim objUser As UserInfo = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)

            Try
                'objUser = PopulateUser(strPhone)
                'If objUser IsNot Nothing Then

                strOTP = "Kode OTP D-NET MMKSI Anda : "
                strOTP = strOTP + OTPCode + vbCrLf
                strOTP = strOTP + "Silahkan Masukan Kode ini di Layar OTP D-NET." + vbCrLf
                strOTP = strOTP + " Kode OTP Valid s/d "
                strOTP = strOTP + DateAdd(DateInterval.Minute, OTPInterval, Now).ToString("dd/MMMM/yyyy HH:mm:ss")

                'SendSMS(strPhone, strOTP)

                Task.Run(Function() SendSMSAPIOTP(strPhone, strOTP)).Wait()

                If (boolReturn) Then

                    Dim objOTPFac As New OTPFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
                    Dim objOTP As New KTB.DNet.Domain.OTP

                    With objOTP
                        .UserInfoID = objUser.ID
                        .ProcessType = enumSMS.ContentMessage.ResetTokenProcess
                        .NumberDestination = objUser.HandPhone
                        .ChallengeCode = OTPCode
                        .SMSValue = strOTP
                        .Status = 1
                        .RowStatus = CType(DBRowStatus.Active, Short)
                    End With

                    Dim result As Integer

                    result = objOTPFac.UpdateStatusOTP(objUser.ID, objUser.HandPhone)

                    result = objOTPFac.Insert(objOTP)

                    If result <= 0 Then

                    Else
                        objOTP.ID = result
                        If CType(ss.GetSession("OTPLog"), KTB.DNet.Domain.OTP) IsNot Nothing Then
                            MessageBox.Show("Kode OTP Baru Sudah Dikirim Ulang")
                        End If
                        ss.SetSession("OTPLog", objOTP)

                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Reset Token Gagal")
            End Try

        End Sub

        'untuk ganti nomor HP
        Public Sub generateCodeOTPChangeNoPhone(ByVal strPhone As String)
            Dim ss As SessionHelper = New SessionHelper
            Dim otpgenerator As New OTPFunction
            Dim OTPCode As String = GenerateOTPCode()
            Dim objUser As UserInfo
            Dim strOTP As String

            Try
                objUser = CType(ss.GetSession("LOGINUSERINFO"), UserInfo)
                objUser.HandPhone = strPhone
                ss.SetSession("LOGINUSERINFO", objUser)
                'If objUser IsNot Nothing Then

                strOTP = "Kode OTP D-NET MMKSI Anda : "
                strOTP = strOTP + OTPCode + vbCrLf
                strOTP = strOTP + "Silahkan Masukan Kode ini di Layar OTP D-NET." + vbCrLf
                strOTP = strOTP + " Kode OTP Valid s/d "
                strOTP = strOTP + DateAdd(DateInterval.Minute, OTPInterval, Now).ToString("dd/MMMM/yyyy HH:mm:ss")

                'SendSMS(strPhone, strOTP)
                Task.Run(Function() SendSMSAPIOTP(strPhone, strOTP)).Wait()

                If (boolReturn) Then

                    Dim objOTPFac As New OTPFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
                    Dim objOTP As New KTB.DNet.Domain.OTP

                    With objOTP
                        .UserInfoID = objUser.ID
                        .ProcessType = enumSMS.ContentMessage.ChangePhoneNumber
                        .NumberDestination = strPhone
                        .ChallengeCode = OTPCode
                        .SMSValue = strOTP
                        .Status = 1
                        .RowStatus = CType(DBRowStatus.Active, Short)
                    End With

                    Dim result As Integer

                    result = objOTPFac.UpdateStatusOTP(objUser.ID, objUser.HandPhone)

                    result = objOTPFac.Insert(objOTP)

                    If result <= 0 Then

                    Else
                        objOTP.ID = result
                        ss.SetSession("OTPLog", objOTP)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Update Nomor Telpon Gagal")
            End Try

        End Sub

        Private Sub getAppConfig(Optional ByVal isOTP As Boolean = False)


            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Partial, "SMSGateWay"))

            Dim arrData As New List(Of AppConfig)
            arrData = New AppConfigFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)).Retrieve(criterias).Cast(Of AppConfig).ToList()


            If isOTP Then
                UserNameOTP = (From val As AppConfig In arrData
                                         Where val.Name.ToUpper().Equals("SMSGATEWAY.USERNAMEOTP")
                                         Select val.Value).FirstOrDefault()
                PasswordOTP = (From val As AppConfig In arrData
                                     Where val.Name.ToUpper().Equals("SMSGATEWAY.PASSWORDOTP")
                                     Select val.Value).FirstOrDefault()
                ClintIDOTP = (From val As AppConfig In arrData
                                     Where val.Name.ToUpper().Equals("SMSGATEWAY.CLIENTIDOTP")
                                     Select val.Value).FirstOrDefault()


            Else
                UserNameOTP = (From val As AppConfig In arrData
                                       Where val.Name.ToUpper().Equals("SMSGATEWAY.USERNAME")
                                       Select val.Value).FirstOrDefault()
                PasswordOTP = (From val As AppConfig In arrData
                                     Where val.Name.ToUpper().Equals("SMSGATEWAY.PASSWORD")
                                     Select val.Value).FirstOrDefault()
                ClintIDOTP = (From val As AppConfig In arrData
                                     Where val.Name.ToUpper().Equals("SMSGATEWAY.CLIENTID")
                                     Select val.Value).FirstOrDefault()
            End If

            URLAPI = (From val As AppConfig In arrData
                                      Where val.Name.ToUpper().Equals("SMSGATEWAY.URLAPI")
                                      Select val.Value).FirstOrDefault()
            strProx = (From val As AppConfig In arrData
                                 Where val.Name.ToUpper().Equals("SMSGATEWAY.PROXY")
                                 Select val.Value).FirstOrDefault()
            strPorxPort = (From val As AppConfig In arrData
                                 Where val.Name.ToUpper().Equals("SMSGATEWAY.PROXYPORT")
                                 Select val.Value).FirstOrDefault()
            
        End Sub

        Private Async Function SendSMSAPI(ByVal strPhone As String, ByVal strMSG As String) As task

            Try
                getAppConfig()
                Dim nvc As New List(Of KeyValuePair(Of String, String))

                nvc.Add(New KeyValuePair(Of String, String)("UserName", UserNameOTP))
                nvc.Add(New KeyValuePair(Of String, String)("Password", PasswordOTP))
                nvc.Add(New KeyValuePair(Of String, String)("DestinationNo", strPhone))
                nvc.Add(New KeyValuePair(Of String, String)("BodyMessage", strMSG))
                nvc.Add(New KeyValuePair(Of String, String)("ClientID", ClintIDOTP))

                Dim client As New System.Net.Http.HttpClient()

                If Not String.IsNullOrEmpty(strProx) Or Not String.IsNullOrEmpty(strPorxPort) Then
                    Dim wp As System.Net.WebProxy = New System.Net.WebProxy(strProx, CInt(strPorxPort))
                    Dim httpClientHandler As New System.Net.Http.HttpClientHandler()
                    httpClientHandler.Proxy = wp

                    client = New System.Net.Http.HttpClient(handler:=httpClientHandler, disposeHandler:=True)

                End If

                Dim req = New System.Net.Http.HttpRequestMessage(HttpMethod.Post, URLAPI) With {.Content = New FormUrlEncodedContent(nvc)}
                Dim res = Await client.SendAsync(req)

                Dim result As String = res.Content.ReadAsStringAsync.Result()
                If (res.IsSuccessStatusCode And Not String.IsNullOrEmpty(result)) Then
                    If (result.Substring(result.IndexOf("status") + 12, 2)).ToUpper = "OK" Then
                        boolReturn = True
                        Return
                    End If
                End If

            Catch ex As Exception
                Return
            End Try

        End Function

        Private Async Function SendSMSAPIOTP(ByVal strPhone As String, ByVal strMSG As String) As task

            Try
                getAppConfig(True)
                Dim nvc As New List(Of KeyValuePair(Of String, String))

                nvc.Add(New KeyValuePair(Of String, String)("UserName", UserNameOTP))
                nvc.Add(New KeyValuePair(Of String, String)("Password", PasswordOTP))
                nvc.Add(New KeyValuePair(Of String, String)("DestinationNo", strPhone))
                nvc.Add(New KeyValuePair(Of String, String)("BodyMessage", strMSG))
                nvc.Add(New KeyValuePair(Of String, String)("ClientID", ClintIDOTP))

                Dim client As New System.Net.Http.HttpClient()


                If Not String.IsNullOrEmpty(strProx) Or Not String.IsNullOrEmpty(strPorxPort) Then
                    Dim wp As System.Net.WebProxy = New System.Net.WebProxy(strProx, CInt(strPorxPort))
                    Dim httpClientHandler As New System.Net.Http.HttpClientHandler()
                    httpClientHandler.Proxy = wp

                    client = New System.Net.Http.HttpClient(handler:=httpClientHandler, disposeHandler:=True)

                End If

                Dim req = New System.Net.Http.HttpRequestMessage(HttpMethod.Post, URLAPI) With {.Content = New FormUrlEncodedContent(nvc)}
                Dim res = Await client.SendAsync(req)

                Dim result As String = res.Content.ReadAsStringAsync.Result()
                If (res.IsSuccessStatusCode And Not String.IsNullOrEmpty(result)) Then
                    If (result.Substring(result.IndexOf("status") + 12, 2)).ToUpper = "OK" Then
                        boolReturn = True
                        Return
                    End If
                End If

            Catch ex As Exception
                Return
            End Try

        End Function

        ' ini nanti digunakan untuk ganti password 
        Public Sub generateCodeOTP(objUserInfo As UserInfo)

            'Dim objUser As UserInfo
            Dim otpgenerator As New OTPFunction
            Dim OTPCode As String = GenerateOTPCode()
            Dim strOTP As String
            Dim result As Integer
            Dim sess As SessionHelper = New SessionHelper

            Try
                strOTP = "Kode OTP D-NET MMKSI Anda : "
                strOTP = strOTP + OTPCode + vbCrLf
                strOTP = strOTP + "Silahkan Masukan Kode ini di Layar OTP D-NET." + vbCrLf
                strOTP = strOTP + " Kode OTP Valid s/d "
                strOTP = strOTP + DateAdd(DateInterval.Minute, OTPInterval, Now).ToString("dd/MMMM/yyyy HH:mm:ss")

                Dim objOTPFac As New OTPFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
                Dim objOTP As New KTB.DNet.Domain.OTP

                With objOTP
                    .UserInfoID = objUserInfo.ID
                    .ProcessType = enumSMS.ContentMessage.ResetPasswordProcess

                    If Not (String.IsNullOrEmpty(objUserInfo.HandPhone)) Then
                        .NumberDestination = objUserInfo.HandPhone
                    Else
                        .NumberDestination = objUserInfo.Telephone
                    End If

                    .ChallengeCode = OTPCode
                    .SMSValue = strOTP
                    .Status = 1
                    .RowStatus = CType(DBRowStatus.Active, Short)
                End With

                ' fungsin ini untuk meng non aktif kan segala OTP Code
                result = objOTPFac.UpdateStatusOTP(objUserInfo.ID, objOTP.NumberDestination)

                result = objOTPFac.Insert(objOTP)

                If result > 0 Then
                    'SendSMS(objOTP.NumberDestination, strOTP)

                    Task.Run(Function() SendSMSAPIOTP(objOTP.NumberDestination, strOTP)).Wait()

                    If (boolReturn) Then
                        objOTP.ID = result
                        sess.SetSession("OTPLog", objOTP)
                        MessageBox.Show("Kode OTP Baru Sudah Dikirim Ulang")
                    End If
                End If

            Catch ex As Exception
                If result < 1 Then
                    MessageBox.Show("Pengiriman Kode OTP Gagal")
                End If
            End Try

        End Sub

        'ini untuk user registrasi aktivasi
        Public Function func_generateCodeOTP(ByVal strPhone As String) As Integer
            Dim ss As SessionHelper = New SessionHelper
            Dim otpgenerator As New OTPFunction
            Dim OTPCode As String = GenerateOTPCode()
            Dim objUser As UserInfo
            Dim strOTP As String

            Try
                objUser = PopulateUser(strPhone)
                If objUser IsNot Nothing Then

                    strOTP = "Kode OTP D-NET MMKSI Anda : "
                    strOTP = strOTP + OTPCode + vbCrLf
                    strOTP = strOTP + "Silahkan Masukan Kode ini di Layar OTP D-NET." + vbCrLf
                    strOTP = strOTP + " Kode OTP Valid s/d "
                    strOTP = strOTP + DateAdd(DateInterval.Minute, OTPInterval, Now).ToString("dd/MMMM/yyyy HH:mm:ss")

                    'SendSMS(strPhone, strOTP)
                    Task.Run(Function() SendSMSAPIOTP(strPhone, strOTP)).Wait()

                    If (boolReturn) Then
                        Dim objOTPFac As New OTPFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
                        Dim objOTP As New KTB.DNet.Domain.OTP

                        With objOTP
                            .UserInfoID = objUser.ID
                            .ProcessType = enumSMS.ContentMessage.ActivationCodeNotification
                            .NumberDestination = objUser.HandPhone
                            .ChallengeCode = OTPCode
                            .SMSValue = strOTP
                            .Status = 1
                            .RowStatus = CType(DBRowStatus.Active, Short)
                        End With

                        Dim result As Integer
                        result = objOTPFac.UpdateStatusOTP(objUser.ID, strPhone)
                        result = objOTPFac.Insert(objOTP)

                        If result <= 0 Then
                            Return 0
                        Else
                            objOTP.ID = result
                            ss.SetSession("OTPLog", objOTP)
                            Return result
                        End If
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Update Nomor Telpon Gagal")
            End Try

        End Function

        Public Function func_generateCodeOTP(ByVal strPhone As String, ByVal userID As Integer) As Integer
            Dim otpgenerator As New OTPFunction
            Dim OTPCode As String = GenerateOTPCode()
            Dim strOTP As String
            Dim ss As SessionHelper = New SessionHelper

            Try


                strOTP = "Kode OTP D-NET MMKSI Anda : "
                strOTP = strOTP + OTPCode + vbCrLf
                strOTP = strOTP + "Silahkan Masukan Kode ini di Layar OTP D-NET." + vbCrLf
                strOTP = strOTP + " Kode OTP Valid s/d "
                strOTP = strOTP + DateAdd(DateInterval.Minute, OTPInterval, Now).ToString("dd/MMMM/yyyy HH:mm:ss")


                'SendSMS(strPhone, strOTP)

                Task.Run(Function() SendSMSAPIOTP(strPhone, strOTP)).Wait()

                If (boolReturn) Then

                    Dim objOTPFac As New OTPFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
                    Dim objOTP As New KTB.DNet.Domain.OTP

                    With objOTP
                        .UserInfoID = userID
                        .ProcessType = enumSMS.ContentMessage.ActivationCodeNotification
                        .NumberDestination = strPhone
                        .ChallengeCode = OTPCode
                        .SMSValue = strOTP
                        .Status = 1
                        .RowStatus = CType(DBRowStatus.Active, Short)
                    End With

                    Dim result As Integer
                    result = objOTPFac.UpdateStatusOTP(userID, strPhone)
                    result = objOTPFac.Insert(objOTP)

                    If result <= 0 Then
                        Return 0
                    Else
                        objOTP.ID = result
                        If CType(ss.GetSession("OTPLog"), KTB.DNet.Domain.OTP) Is Nothing Then
                        Else
                            ss.SetSession("OTPLog", objOTP)
                            MessageBox.Show("Kode OTP Baru Sudah Dikirim Ulang")
                            Return result
                        End If

                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Update Nomor Telpon Gagal")
            End Try

        End Function


        'untuk resend OTP by Dnet PIC
        Public Sub generateCodeOTP(ByVal userId As Integer, ByVal strPhone As String, ByVal intProcessType As Integer)
            'Dim objUser As UserInfo
            Dim otpgenerator As New OTPFunction
            Dim OTPCode As String = GenerateOTPCode()
            Dim strOTP As String
            Dim ss As SessionHelper = New SessionHelper

            Try
                strOTP = "Kode OTP D-NET MMKSI Anda : "
                strOTP = strOTP + OTPCode + vbCrLf
                strOTP = strOTP + "Silahkan Masukan Kode ini di Layar OTP D-NET." + vbCrLf
                strOTP = strOTP + " Kode OTP Valid s/d "
                strOTP = strOTP + DateAdd(DateInterval.Minute, OTPInterval, Now).ToString("dd/MMMM/yyyy HH:mm:ss")

                'SendSMS(strPhone, strOTP)

                Task.Run(Function() SendSMSAPIOTP(strPhone, strOTP)).Wait()

                If (boolReturn) Then

                    Dim objOTPFac As New OTPFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))
                    Dim objOTP As New KTB.DNet.Domain.OTP

                    With objOTP
                        .UserInfoID = userId
                        .ProcessType = intProcessType
                        .NumberDestination = strPhone
                        .ChallengeCode = OTPCode
                        .SMSValue = strOTP
                        .Status = 1
                        .RowStatus = CType(DBRowStatus.Active, Short)
                    End With

                    Dim result As Integer

                    result = objOTPFac.UpdateStatusOTP(userId, strPhone)

                    result = objOTPFac.Insert(objOTP)

                    If result <= 0 Then

                    Else
                        objOTP.ID = result
                        ss.SetSession("OTPLog", objOTP)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Update Nomor Telpon Gagal")
            End Try

        End Sub

        Public Shared Sub ExtendCodeOTP(objOTP As KTB.DNet.Domain.OTP)
            'Dim objUser As UserInfo
            Dim otpgenerator As New OTPFunction
            Dim OTPCode As String = objOTP.ChallengeCode
            Dim strOTP As String
            Dim strPhone As String = objOTP.NumberDestination

            Try

                strOTP = "Kode OTP D-NET MMKSI Anda : "
                strOTP = strOTP + OTPCode + vbCrLf
                strOTP = strOTP + "Silahkan Masukan Kode ini di Layar OTP D-NET." + vbCrLf
                strOTP = strOTP + " Kode OTP Valid s/d "
                strOTP = strOTP + DateAdd(DateInterval.Minute, OTPInterval, Now).ToString("dd/MMMM/yyyy HH:mm:ss")

                SendSMS(strPhone, strOTP)

                Dim objOTPFac As New OTPFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("dnet"), Nothing)))

                With objOTP
                    .ValidUntil = DateAdd(DateInterval.Minute, OTPInterval, objOTP.ValidUntil)
                End With

                Dim result As Integer

                result = objOTPFac.Update(objOTP)

                If result <= 0 Then
                    MessageBox.Show("Perpanjangan Masa Validitas Kode OTP Gagal")
                Else
                    MessageBox.Show("Perpanjangan Masa Validitas Kode OTP Berhasil")
                End If

            Catch ex As Exception
                MessageBox.Show("Update Nomor Telpon Gagal")
            End Try
        End Sub

        Sub SendSMSNotif(HP As String, msg As String)
            Try
                Task.Run(Function() SendSMSAPI(HP, msg)).Wait()

                If (boolReturn) Then

                End If

            Catch ex As Exception

            End Try
        End Sub

    End Class
End Namespace
