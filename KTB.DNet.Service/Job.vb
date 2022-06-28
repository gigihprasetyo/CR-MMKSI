
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Web.Mail
Imports System.Security.Principal
Imports System.Security.Cryptography

Imports KTB.DNet.Utility

Public MustInherit Class Job

    'Protected MustOverride Property NextSchedule() As DateTime
    Protected MustOverride ReadOnly Property ModuleName() As String
    Protected MustOverride Function executeJob() As Boolean
    Protected User As New System.Security.Principal.GenericPrincipal(New GenericIdentity("DNetService"), Nothing)


    '<!--
    'Freq [int]	=> 0:once;1:daily;2:weekly;3:monthly;4:CustomInNumOfDay
    'Date[date]	=> 2013/01/01 format=yyyy/MM/dd
    'Time[time]	=> 14:00	format=HHmm ->24based
    'Custom[int]=> 10	number of day
    'To[string]	=> doni-n@bsi.co.id;firman@bsi.co.id separated with ; 
    'CC[string]	=> doni-n@bsi.co.id;firman@bsi.co.id separated with ; 
    '-->

    Public Sub DoJob()
        Dim n As Integer = Math.Abs(DateDiff(DateInterval.Second, Me.ScheduleDate, Me.CurrentTime))

        '' LogHelper.WriteLog("DoJob - schedule checking.( " & Me.ModuleName & ", " & n.ToString() & " seconds) " & Me.ScheduleDate.ToString("yyyy.MM.dd HH:mm:ss") & " vs " & Me.CurrentTime.ToString("yyyy.MM.dd HH:mm:ss"))
        If n >= 0 AndAlso n <= 60 Then
            Me.executeJob()
        End If
    End Sub

    Protected ReadOnly Property EmailTo() As String
        Get
            Return KTB.DNet.Lib.WebConfig.GetValue(ModuleName & "To")
        End Get
    End Property

    Protected ReadOnly Property EmailCC() As String
        Get
            Return KTB.DNet.Lib.WebConfig.GetValue(ModuleName & "CC")
        End Get
    End Property

    Protected ReadOnly Property Frequency() As Library.Frequency
        Get
            Dim Freq As Library.Frequency = CType(KTB.DNet.Lib.WebConfig.GetValue(ModuleName & "Frequency"), Library.Frequency)

            Return Freq
        End Get
    End Property

    Protected ReadOnly Property NumOfFrequency() As Integer
        Get
            Dim nFreq As Integer = 0
            Try
                nFreq = CType(KTB.DNet.Lib.WebConfig.GetValue(ModuleName & "Custom"), Integer)
            Catch ex As Exception
                nFreq = 0
            End Try

            Return nFreq
        End Get
    End Property

    Protected ReadOnly Property ScheduleDate() As DateTime
        Get
            Dim Freq As Library.Frequency = Me.Frequency
            Dim sDate As String = KTB.DNet.Lib.WebConfig.GetValue(ModuleName & "Date")
            Dim sTime As String = KTB.DNet.Lib.WebConfig.GetValue(ModuleName & "Time")
            Dim nFreq As Integer = 0
            Dim nFreqM As Double
            Dim dt As DateTime
            Dim dtNext As DateTime

            If Not IsNothing(KTB.DNet.Lib.WebConfig.GetValue(ModuleName & "Custom")) Then
                Try
                    nFreq = CType(KTB.DNet.Lib.WebConfig.GetValue(ModuleName & "Custom"), Integer)
                Catch ex As Exception
                    nFreq = 0
                End Try
            End If

            If Not IsNothing(KTB.DNet.Lib.WebConfig.GetValue(ModuleName & "CMinute")) Then
                Try
                    nFreqM = CType(KTB.DNet.Lib.WebConfig.GetValue(ModuleName & "CMinute"), Double)
                Catch ex As Exception
                    nFreqM = 0
                End Try
            End If

            dt = New DateTime(CInt(sDate.Split("/")(0)) _
                , CInt(sDate.Split("/")(1)) _
                , CInt(sDate.Split("/")(2)) _
                , CInt(sTime.Split(":")(0)) _
                , CInt(sTime.Split(":")(1)) _
                , 0)
            Select Case Freq
                Case Library.Frequency.Once
                    dtNext = dt
                Case Library.Frequency.Daily
                    dtNext = New DateTime(Now.Year, Now.Month, Now.Day, dt.Hour, dt.Minute, dt.Second)
                Case Library.Frequency.Weekly
                    'not implemented yet 
                    dtNext = Now.AddYears(-100)
                Case Library.Frequency.Monthly
                    dtNext = New DateTime(Now.Year, Now.Month, Now.Day, dt.Hour, dt.Minute, dt.Second)
                Case Library.Frequency.CustomDay
                    Dim nDiff As Integer = DateDiff(DateInterval.Day, dt, Now)
                    Dim nAdd As Integer = nDiff Mod nFreq
                    dtNext = dt.AddDays(ndiff - nadd + nFreq)
                Case Library.Frequency.CustomHour
                    Dim nDiff As Integer = DateDiff(DateInterval.Hour, dt, Now)
                    Dim nAdd As Integer = nDiff Mod nFreq
                    dtNext = dt.AddHours(nDiff - nAdd + nFreq)
                Case Library.Frequency.CustomMinute
                    Dim nDiff As Integer = DateDiff(DateInterval.Minute, dt, Now)
                    Dim nTIme As Integer = 0.5 * 60
                    Dim nAdd As Integer = nDiff Mod nTIme
                    dtNext = dt.AddMinutes(nDiff - nAdd + nTIme)

                Case Else
                    dtNext = Now.AddYears(-100)
            End Select
            Return dtNext
        End Get
    End Property

    Protected ReadOnly Property CurrentTime() As DateTime
        Get
            Return New DateTime(Now.Year, Now.Month, Now.Day, Now.Hour, Now.Minute, Now.Second)
        End Get
    End Property

    Protected Sub SendEmail(ByVal EmailFile As String, ByVal sTo As String, ByVal sCC As String, ByVal sSubject As String, ByVal sMessage() As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailAdmin As String = KTB.DNet.Lib.WebConfig.GetValue("EmailAdmin")
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(EmailFile)
        Dim szEmailFormat As String = sr.ReadToEnd()
        sr.Close()
        Dim szEmailContent As String = String.Format(szEmailFormat, sMessage)
        If Not IsNothing(sCC) AndAlso sCC.Trim() <> "" AndAlso sCC.EndsWith(";") = False AndAlso Not IsNothing(emailAdmin) AndAlso emailAdmin <> "" Then
            emailAdmin = ";" & emailAdmin
        End If

        'ObjEmail.sendMail(sTo, sCC & emailAdmin, emailFrom, sSubject, Mail.MailFormat.Html, szEmailContent)
        ObjEmail.sendMail(sTo, sCC, emailAdmin, emailFrom, sSubject, MailFormat.Html, szEmailContent)
    End Sub

    Protected Sub SendEmail(ByVal sTo As String, ByVal sCC As String, ByVal sBCC As String, ByVal sSubject As String, ByVal sMessage As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailAdmin As String = KTB.DNet.Lib.WebConfig.GetValue("EmailAdmin")
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")


        'ObjEmail.sendMail(sTo, sCC & emailAdmin, emailFrom, sSubject, Mail.MailFormat.Html, szEmailContent)

        ObjEmail.sendMail(sTo, sCC, sBCC, emailFrom, sSubject, MailFormat.Html, sMessage)

    End Sub

End Class
