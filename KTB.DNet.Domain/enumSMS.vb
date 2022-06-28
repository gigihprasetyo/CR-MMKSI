Imports System.Text
Imports System.Configuration


Namespace KTB.DNet.Domain

    Public Class enumSMS

        Public Enum enumStatusSMS
            BelumProses = 0
            Terkirim = 1
            Gagal = 2
        End Enum


        Public Enum ContentMessage
            ActivationFail = 0
            ResetPasswordSuccess = 1
            ResetPasswordFail = 2
            ResetBingoSuccess = 3
            ResetBingoFail = 4
            ActivationCodeNotification = 5
            BingoCardNotification = 6
            ProcessFail = 7
            LoginFail = 8
            TokenFail = 9
            WrongFormat = 10
            ResetPasswordProcess = 11
            ResetTokenProcess = 12
            ChangePhoneNumber = 13
        End Enum



        Public Shared Function GetContentMessage(ByVal type As Integer, Optional ByVal oLogin As Object = Nothing, _
                                                 Optional ByVal serial As String = "", Optional ByVal strbingo As String = "", _
                                                 Optional ByVal _strExpiredBingoCount As String = "", _
                                                 Optional ByVal newPassword As String = "", _
                                                 Optional ByVal companyCodeKey As String = "") As String
            Dim sb As New StringBuilder
            Dim _strEnter As String = Chr(13) & Chr(10)
            Dim _strSpace As String = " "
            Dim _strDelimiter As String = ":"
            Dim strHeader As String = String.Empty
            Dim ExpiredToken As Integer
            Dim companyCode As String
            If companyCodeKey = "" Then
                Try
                    'companyCode = AppConfig.GetValue("CompanyCode").ToString()
                    companyCode = AppConfig.GetValue("SmsKeyCode").ToString()
                Catch ex As Exception
                    companyCode = String.Empty
                End Try
            Else
                companyCode = companyCodeKey
            End If
            Try
                strHeader = AppConfig.GetValue("HeaderKTB").ToString()
            Catch ex As Exception
                strHeader = "D-NET"
            End Try

            If companyCode <> String.Empty Then
                strHeader = strHeader & " " & companyCode
            End If

            Try
                ExpiredToken = CInt(AppConfig.GetValue("ExpiredCount").Trim)
            Catch ex As Exception
                ExpiredToken = -1
            End Try

            Select Case type
                Case enumSMS.ContentMessage.ActivationCodeNotification
                    Dim obj As UserProfile = CType(oLogin, UserProfile)
                    Dim expirationActivation As Integer = 1
                    Try
                        expirationActivation = CInt(AppConfig.GetValue("ActivationCodeExpiredTime").ToString().Trim)
                    Catch ex As Exception
                        expirationActivation = 1
                    End Try

                    Dim activateDate As Date = obj.ActivationSentTime.AddDays(expirationActivation)
                    If companyCode = String.Empty Then
                        'sb.Append("Kode Aktivasi ")
                        sb.Append("Aktivasi ")
                    Else
                        'sb.Append(companyCode & " Kode Aktivasi ")
                        sb.Append(companyCode & " Aktivasi ")
                    End If

                    If obj.TransitionActivationCode <> String.Empty Then
                        sb.Append(obj.TransitionActivationCode)
                    Else
                        If obj.TempActivationCode <> String.Empty Then
                            sb.Append(obj.TempActivationCode)
                        Else
                            sb.Append(obj.ActivationCode)
                        End If
                    End If
                    sb.Append(_strEnter)
                    If obj.TempActivationCode.Trim <> String.Empty Or obj.TransitionActivationCode.Trim <> String.Empty Then
                        If companyCode = String.Empty Then
                            sb.Append("Reply seluruh SMS ini atau ketik AKTIVASI kode_aktivasi sebelum")
                        Else
                            sb.Append("Reply seluruh SMS ini atau ketik " & companyCode & " AKTIVASI kode_aktivasi sebelum")
                        End If
                        sb.Append(_strSpace)
                        sb.Append(_strDelimiter)
                        sb.Append(_strEnter)
                        sb.Append(activateDate.ToString("dd-MMM-yyyy HH:mm:ss"))
                        sb.Append(" WIB")
                    End If

                Case enumSMS.ContentMessage.BingoCardNotification
                    Dim obj As UserProfile = CType(oLogin, UserProfile)
                    Dim activateDate As Date = System.DateTime.Now.AddDays(_strExpiredBingoCount)
                    sb.Append("Serial # " & strHeader & ":" & serial)
                    sb.Append(_strEnter)
                    sb.Append("Token Anda")
                    sb.Append(_strDelimiter)
                    sb.Append(_strEnter)
                    sb.Append(strbingo)
                    sb.Append(_strEnter)
                    If ExpiredToken >= 0 Then
                        sb.Append("Valid s/d")
                        sb.Append(_strDelimiter)
                        sb.Append(_strSpace)
                        sb.Append(activateDate.ToString("dd-MMM-yyyy HH:mm"))
                        sb.Append(" WIB")
                    End If

                Case enumSMS.ContentMessage.ResetPasswordSuccess
                    Dim obj As Object = CType(oLogin, UserInfo)
                    Dim strUserAndDealer As String = ""
                    If Not obj Is Nothing Then
                        strUserAndDealer = String.Concat(obj.UserName, " & ", obj.Dealer.DealerCode, " ")
                    End If
                    sb.Append("Reset Kata Kunci Untuk Kode Organisasi: " + obj.Dealer.DealerCode + " & Nama: " + obj.UserName + " Berhasil!")
                    'sb.Append(strUserAndDealer)
                    'sb.Append("Berhasil!")
                    sb.Append(_strEnter)
                    sb.Append("Kata Kunci: ")
                    sb.Append(newPassword)
                    sb.Append(_strEnter)
                    sb.Append("Segera login ke " & strHeader & " dan ganti Kata Kunci Anda")

                    '============================================
                Case enumSMS.ContentMessage.ResetBingoSuccess
                    Dim obj As UserProfile = CType(oLogin, UserProfile)
                    Dim activateDate As Date = System.DateTime.Now.AddDays(_strExpiredBingoCount)
                    sb.Append("Reset sukses")
                    sb.Append(_strEnter)
                    sb.Append("Serial #" & strHeader & ":" & serial)
                    sb.Append(_strEnter)
                    sb.Append("Token")
                    sb.Append(_strDelimiter)
                    sb.Append(_strEnter)
                    sb.Append(strbingo)
                    If ExpiredToken >= 0 Then
                        sb.Append(_strEnter)
                        sb.Append("Valid s/d")
                        sb.Append(_strDelimiter)
                        sb.Append(_strSpace)
                        sb.Append(activateDate.ToString("dd-MMM-yyyy HH:mm"))
                        sb.Append(" WIB")
                    End If

                Case enumSMS.ContentMessage.ActivationFail
                    sb.Append(strHeader)
                    sb.Append(_strEnter)
                    sb.Append("Aktivasi gagal. Mohon login ke " & strHeader & " untuk mendapatkan kode aktivasi Anda yang baru")

                Case enumSMS.ContentMessage.ResetPasswordFail
                    sb.Append("Reset Password Gagal.")
                    sb.Append(_strEnter)
                    sb.Append("Kirim ulang SMS")
                    sb.Append(_strEnter)
                    If companyCode = String.Empty Then
                        sb.Append("(RESET PASSWORD kode_org#nama_login#kode_aktivasi) untuk mendapatkan password baru")
                    Else
                        sb.Append("(" & companyCode & " RESET PASSWORD kode_org#nama_login#kode_aktivasi) untuk mendapatkan password baru")
                    End If

                Case enumSMS.ContentMessage.ResetBingoFail
                    sb.Append("Reset Token Gagal.")
                    sb.Append(_strEnter)
                    sb.Append("Kirim ulang SMS")
                    sb.Append(_strEnter)
                    If companyCode = String.Empty Then
                        sb.Append("(RESET TOKEN kode_org#nama_login#kode_aktivasi) atau login kembali ke " & strHeader & " untuk mendapatkan Token baru")
                    Else
                        sb.Append("(" & companyCode & " RESET TOKEN kode_org#nama_login#kode_aktivasi) atau login kembali ke " & strHeader & " untuk mendapatkan Token baru")
                    End If

                Case enumSMS.ContentMessage.ProcessFail
                    sb.Append(strHeader)
                    sb.Append(_strEnter)
                    sb.Append("Proses Gagal")

                Case enumSMS.ContentMessage.WrongFormat
                    sb.Append(strHeader)
                    sb.Append(_strEnter)
                    sb.Append("Format SMS salah.")

            End Select

            Return sb.ToString
        End Function

        Public Shared Function GetContentEmail(ByVal type As Integer, Optional ByVal oLogin As Object = Nothing, _
                                                 Optional ByVal serial As String = "", Optional ByVal strbingo As String = "", _
                                                 Optional ByVal _strExpiredBingoCount As String = "", _
                                                 Optional ByVal newPassword As String = "", _
                                                 Optional ByVal companyCodeKey As String = "") As String
            Dim sb As New StringBuilder
            Dim strHeader As String = AppConfig.GetValue("HeaderKTB").ToString()
            Dim companyCode As String
            If companyCodeKey = "" Then
                Try
                    'companyCode = AppConfig.GetValue("CompanyCode").ToString()
                    companyCode = AppConfig.GetValue("SmsKeyCode").ToString()
                Catch ex As Exception
                    companyCode = String.Empty
                End Try
            Else
                companyCode = companyCodeKey
            End If

            If companyCode <> String.Empty Then
                strHeader = strHeader & " " & companyCode
            End If

            Select Case type

                Case enumSMS.ContentMessage.ActivationCodeNotification
                    Dim obj As UserProfile = CType(oLogin, UserProfile)
                    Dim expirationActivation As Integer = 1
                    Try
                        expirationActivation = CInt(AppConfig.GetValue("ActivationCodeExpiredTime").ToString().Trim)
                    Catch ex As Exception
                        expirationActivation = 1
                    End Try

                    Dim activateDate As Date = obj.ActivationSentTime.AddDays(expirationActivation)
                    sb.Append("<html><table><tr><td><b>" & strHeader & "</td><tr><table><tr><td> Kode Aktivasi Anda </td> <td>:</td><td>")
                    If obj.TempActivationCode = String.Empty Then
                        sb.Append(obj.ActivationCode)
                    Else
                        sb.Append(obj.TempActivationCode)
                    End If
                    If obj.TempActivationCode <> String.Empty Then
                        sb.Append("</td><tr><tr><td> Silahkan Aktivasi menggunakan Handphone sebelum </td><td>:</td><td>")
                        sb.Append(activateDate.ToString("dd-MMM-yyyy HH:mm:ss"))
                        sb.Append(" WIB")
                    End If
                    sb.Append("</td><tr></table></tabel><html>")

                Case enumSMS.ContentMessage.BingoCardNotification
                    Dim obj As UserProfile = CType(oLogin, UserProfile)
                    Dim activateDate As Date = System.DateTime.Now.AddDays(_strExpiredBingoCount)
                    sb.Append("<html><table><tr><td><b>Serial # ")
                    sb.Append(strHeader & " - ")
                    sb.Append(serial)
                    sb.Append("</b></td></tr><tr><td>[Valid Sampai : ")
                    sb.Append(activateDate.ToString("dd-MMM-yyyy HH:mm:ss"))
                    sb.Append("] WIB </td></tr><tr><td>Token Anda :</td></tr><tr><td>")
                    sb.Append(strbingo.ToString)
                    sb.Append("</td></tr></table></html>")

                Case enumSMS.ContentMessage.ResetPasswordSuccess
                    Dim obj As Object = Nothing
                    sb.Append("<html><table><tr><td><b>" & strHeader & "</b></td></tr><tr><td><br><br><b>Reset Password Berhasil</b></td></tr><tr><td><br>Password Anda :</td></tr><tr><td>")
                    sb.Append(newPassword)
                    sb.Append("</td></tr><tr><td><br>Segera login ke " & strHeader & " dan ganti password anda</td><tr></table></html>")

                Case enumSMS.ContentMessage.ResetBingoSuccess
                    Dim obj As UserProfile = CType(oLogin, UserProfile)
                    Dim activateDate As Date = System.DateTime.Now.AddDays(_strExpiredBingoCount)
                    sb.Append("<html><table><tr><td><b>Reset Token Berhasil</td></tr><tr><td><b>Serial # ")
                    sb.Append(strHeader & " - ")
                    sb.Append(serial)
                    sb.Append("</b><td></tr><tr><td>[Valid Sampai : ")
                    sb.Append(activateDate.ToString("dd-MMM-yyyy HH:mm:ss") & "]")
                    sb.Append("] WIB </td></td></tr><tr><td>Token Anda :</td></tr><tr><tr><td>")
                    sb.Append(strbingo)
                    sb.Append("</td></tr></table></html>")

                Case enumSMS.ContentMessage.ProcessFail
                    sb.Append("<html><table><tr><td><b>" & strHeader & "</b><td></tr><tr><td>Send SMS Fail</td></tr><tr><td><i>Please Check Your SMS Gateway</i></td></tr><tr><td><br>")
                    sb.Append(System.DateTime.Now.ToString("dd-MMMMM-yyyy HH:mm:ss"))
                    sb.Append("</td></tr></table></html>")

                    '======================================
                Case enumSMS.ContentMessage.LoginFail
                    Dim obj As UserInfo = CType(oLogin, UserInfo)
                    sb.Append("<html><table><tr><td><b>" & strHeader & "</b><td></tr><tr><td>Login Gagal</td></tr><tr><td>Akses Login ")
                    sb.Append(obj.UserName & "-" & obj.Dealer.DealerCode)
                    sb.Append(" user Telah Terkunci</td></tr><tr><td><br>")
                    sb.Append(System.DateTime.Now.ToString("dd-MMMMM-yyyy HH:mm:ss"))
                    sb.Append("WIB </td></tr></table></html>")

                Case enumSMS.ContentMessage.TokenFail
                    Dim obj As UserInfo = CType(oLogin, UserInfo)
                    sb.Append("<html><table><tr><td><b>" & strHeader & "</b><td></tr><tr><td>Login Gagal</td></tr><tr><td><i>")
                    sb.Append(obj.UserName & "-" & obj.Dealer.DealerCode)
                    sb.Append(" Token Telah Terkunci</i></td></tr><tr><td><br>")
                    sb.Append(System.DateTime.Now.ToString("dd-MMMMM-yyyy HH:mm:ss"))
                    sb.Append("WIB </td></tr></table></html>")

                Case enumSMS.ContentMessage.ActivationFail
                Case enumSMS.ContentMessage.ResetPasswordFail
                Case enumSMS.ContentMessage.ResetBingoFail

            End Select

            Return sb.ToString
        End Function
    End Class

End Namespace