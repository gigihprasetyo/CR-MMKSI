Imports System.Net.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Configuration

Public Class DNetMail
    Private msgMail As MailMessage
    Private _smtpServer As String
    Private _priority As Integer

    Public Sub New(ByVal smtpServer As String)
        msgMail = New MailMessage
        'msgMail.Priority = MailPriority.High
        _smtpServer = smtpServer
    End Sub

    Property Priority() As Integer
        Get
            Return _priority
        End Get
        Set(ByVal Value As Integer)
            _priority = Value
        End Set
    End Property

    ''' <summary>
    ''' Send1
    ''' </summary>
    ''' <param name="sendTo"></param>
    ''' <param name="ccTo1"></param>
    ''' <param name="ccTo2"></param>
    ''' <param name="from"></param>
    ''' <param name="WscTeam"></param>
    ''' <param name="subject"></param>
    ''' <param name="format"></param>
    ''' <param name="body"></param>
    ''' <remarks></remarks>
    Public Sub sendMail(ByVal sendTo As String, ByVal ccTo1 As String, ByVal ccTo2 As String, ByVal from As String, ByVal WscTeam As String, ByVal subject As String, ByVal format As Web.Mail.MailFormat, ByVal body As String)
        Try


            For Each _to As String In sendTo.Split(";".ToCharArray())
                If (_to.Trim().Length > 1) Then
                    msgMail.To.Add(_to)
                End If

            Next

            Dim cc As String = String.Empty

            If ccTo1.Trim <> "" Then
                cc = ccTo1
            End If

            If Not IsNothing(ccTo2) Then
                If ccTo2.Trim <> "" Then
                    cc += ";" & ccTo2
                End If
            End If

            If Not IsNothing(WscTeam) Then
                cc += ";" & WscTeam
            End If
            For Each _cc As String In cc.Split(";".ToCharArray())
                If (_cc.Trim().Length > 1) Then
                    msgMail.CC.Add(_cc)
                End If

            Next

            'msgMail.From = from

            msgMail.From = New MailAddress(from)

            msgMail.Subject = subject
            msgMail.IsBodyHtml = IIf(format = Web.Mail.MailFormat.Html, True, False)
            msgMail.Body = body
            msgMail.Priority = _priority
            Dim SmtpMail As New SmtpClient()
            SmtpMail.Host = _smtpServer
            SmtpMail.Send(msgMail)

            'SmtpMail.SmtpServer = _smtpServer
            'SmtpMail.Send(msgMail)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    ''' <summary>
    ''' Send2
    ''' </summary>
    ''' <param name="sendTo"></param>
    ''' <param name="ccTo1"></param>
    ''' <param name="bccTo"></param>
    ''' <param name="from"></param>
    ''' <param name="subject"></param>
    ''' <param name="format"></param>
    ''' <param name="body"></param>
    ''' <remarks></remarks>
    Public Sub sendMail(ByVal sendTo As String, ByVal ccTo1 As String, ByVal bccTo As String, ByVal from As String, ByVal subject As String, ByVal format As Web.Mail.MailFormat, ByVal body As String)
        Try


            For Each _to As String In sendTo.Split(";".ToCharArray())
                If (_to.Trim().Length > 1) Then
                    msgMail.To.Add(_to)
                End If

            Next
            Dim cc As String = String.Empty
            Dim bcc As String = String.Empty

            If ccTo1.Trim <> "" Then
                cc = ccTo1
            End If


            If Not IsNothing(bccTo) Then
                bcc = bccTo
            End If

            ' msgMail.CC = cc

            For Each _cc As String In cc.Split(";".ToCharArray())
                If (_cc.Trim().Length > 1) Then
                    msgMail.CC.Add(_cc)
                End If

            Next

            ''msgMail.Bcc = bcc

            For Each _bcc As String In bcc.Split(";".ToCharArray())
                If (_bcc.Trim().Length > 1) Then
                    msgMail.Bcc.Add(_bcc)
                End If

            Next

            ' msgMail.From = from
            msgMail.From = New MailAddress(from)
            msgMail.Subject = subject
            msgMail.IsBodyHtml = IIf(format = Web.Mail.MailFormat.Html, True, False)
            msgMail.Body = body
            msgMail.Priority = _priority

            Dim SmtpMail As New SmtpClient()
            SmtpMail.Host = _smtpServer
            SmtpMail.Send(msgMail)
            'SmtpMail.SmtpServer = _smtpServer
            'SmtpMail.Send(msgMail)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Send 3
    ''' </summary>
    ''' <param name="sendTo"></param>
    ''' <param name="ccTo1"></param>
    ''' <param name="ccTo2"></param>
    ''' <param name="from"></param>
    ''' <param name="WscTeam"></param>
    ''' <param name="subject"></param>
    ''' <param name="format"></param>
    ''' <param name="body"></param>
    ''' <param name="attachments"></param>
    ''' <remarks></remarks>
    Public Sub sendMail(ByVal sendTo As String, ByVal ccTo1 As String, ByVal ccTo2 As String, ByVal from As String, ByVal WscTeam As String, ByVal subject As String, ByVal format As Web.Mail.MailFormat, ByVal body As String, ByVal attachments As ArrayList)
        Try
            ' msgMail.To = sendTo

            For Each _to As String In sendTo.Split(";".ToCharArray())
                If (_to.Trim().Length > 1) Then
                    msgMail.To.Add(_to)
                End If

            Next

            Dim cc As String = String.Empty

            If ccTo1.Trim <> "" Then
                cc = ccTo1
            End If

            If Not IsNothing(ccTo2) Then
                If ccTo2.Trim <> "" Then
                    cc += ";" & ccTo2
                End If
            End If

            If Not IsNothing(WscTeam) Then
                cc += ";" & WscTeam
            End If

            ' msgMail.CC = cc

            For Each _cc As String In cc.Split(";".ToCharArray())
                If (_cc.Trim().Length > 1) Then
                    msgMail.CC.Add(_cc)
                End If

            Next

            ' msgMail.From = from
            msgMail.From = New MailAddress(from)

            msgMail.Subject = subject
            ' msgMail.From = from
            msgMail.From = New MailAddress(from)
            msgMail.IsBodyHtml = IIf(format = Web.Mail.MailFormat.Html, True, False)
            msgMail.Body = body
            msgMail.Priority = _priority
            'For Each item As String In attachments
            '    Dim attch As MailAttachment = New MailAttachment(item)
            '    msgMail.Attachments.Add(attch)
            'Next

            For Each item As String In attachments
                Dim attch As Attachment = New Attachment(item)
                msgMail.Attachments.Add(attch)
            Next

            Dim SmtpMail As New SmtpClient()
            SmtpMail.Host = _smtpServer
            SmtpMail.Send(msgMail)
            'SmtpMail.SmtpServer = _smtpServer
            'SmtpMail.Send(msgMail)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Send 4
    ''' </summary>
    ''' <param name="sendTo"></param>
    ''' <param name="ccTo"></param>
    ''' <param name="from"></param>
    ''' <param name="subject"></param>
    ''' <param name="format"></param>
    ''' <param name="body"></param>
    ''' <remarks></remarks>
    Public Sub sendMail(ByVal sendTo As String, ByVal ccTo As String, ByVal from As String, ByVal subject As String, ByVal format As Web.Mail.MailFormat, ByVal body As String)
        Try
            ' msgMail.To = sendTo
            For Each _to As String In sendTo.Split(";".ToCharArray())
                If (_to.Trim().Length > 1) Then
                    msgMail.To.Add(_to)
                End If
            Next

            Dim cc As String = String.Empty
            If ccTo.Trim <> "" Then
                cc = ccTo
            End If
            ' msgMail.CC = cc

            For Each _cc As String In cc.Split(";".ToCharArray())
                If (_cc.Trim().Length > 1) Then
                    msgMail.CC.Add(_cc)
                End If

            Next
            ' msgMail.From = from
            msgMail.From = New MailAddress(from)
            msgMail.Subject = subject
            msgMail.IsBodyHtml = IIf(format = Web.Mail.MailFormat.Html, True, False)
            msgMail.Body = body
            msgMail.Priority = _priority
            Dim SmtpMail As New SmtpClient()
            SmtpMail.Host = _smtpServer
            SmtpMail.Send(msgMail)
            'SmtpMail.SmtpServer = _smtpServer
            'SmtpMail.Send(msgMail)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' send 5
    ''' </summary>
    ''' <param name="sendTo"></param>
    ''' <param name="ccTo"></param>
    ''' <param name="from"></param>
    ''' <param name="subject"></param>
    ''' <param name="format"></param>
    ''' <param name="body"></param>
    ''' <remarks></remarks>
    Public Sub sendMailTraining(ByVal sendTo As String, ByVal ccTo As String, ByVal from As String, ByVal subject As String, ByVal format As Web.Mail.MailFormat, ByVal body As String)
        Try
            Dim EmailTo As String = String.Empty
            If Not IsNothing(sendTo) Then
                For Each _to As String In sendTo.Split(";".ToCharArray())
                    If (_to.Trim().Length > 1) Then
                        msgMail.To.Add(_to)
                    End If
                Next
            Else
                ' msgMail.To = EmailTo
                For Each _to As String In EmailTo.Split(";".ToCharArray())
                    If (_to.Trim().Length > 1) Then
                        msgMail.To.Add(_to)
                    End If
                Next
            End If

            Dim cc As String = String.Empty
            If Not IsNothing(ccTo) Then
                '  msgMail.CC = ccTo

                For Each _cc As String In ccTo.Split(";".ToCharArray())
                    If (_cc.Trim().Length > 1) Then
                        msgMail.CC.Add(_cc)
                    End If

                Next

            Else
                ' msgMail.CC = cc
            End If

            ' msgMail.From = from
            msgMail.From = New MailAddress(from)
            msgMail.Subject = subject
            msgMail.IsBodyHtml = IIf(format = Web.Mail.MailFormat.Html, True, False)
            msgMail.Body = body
            msgMail.Priority = _priority

            Dim SmtpMail As New SmtpClient()
            SmtpMail.Host = _smtpServer
            SmtpMail.Send(msgMail)
            'SmtpMail.SmtpServer = _smtpServer
            'SmtpMail.Send(msgMail)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    ''' <summary>
    ''' Send 6
    ''' </summary>
    ''' <param name="sendTo"></param>
    ''' <param name="ccTo"></param>
    ''' <param name="from"></param>
    ''' <param name="subject"></param>
    ''' <param name="format"></param>
    ''' <param name="body"></param>
    ''' <param name="attachments"></param>
    ''' <remarks></remarks>
    Public Sub sendMail(ByVal sendTo As String, ByVal ccTo As String, ByVal from As String, ByVal subject As String, ByVal format As Web.Mail.MailFormat, ByVal body As String, ByVal attachments As ArrayList)
        Try
            '' msgMail.To = sendTo

            For Each _to As String In sendTo.Split(";".ToCharArray())
                If (_to.Trim().Length > 1) Then
                    msgMail.To.Add(_to)
                End If
            Next

            Dim cc As String = String.Empty
            If ccTo.Trim <> "" Then
                cc = ccTo
            End If
            ''msgMail.CC = cc
            For Each _cc As String In cc.Split(";".ToCharArray())
                If (_cc.Trim().Length > 1) Then
                    msgMail.CC.Add(_cc)
                End If

            Next

            'msgMail.From = from
            msgMail.From = New MailAddress(from)
            msgMail.Subject = subject
            msgMail.IsBodyHtml = IIf(format = Web.Mail.MailFormat.Html, True, False)
            msgMail.Body = body
            msgMail.Priority = _priority
            'For Each item As MailAttachment In attachments
            '    msgMail.Attachments.Add(item)
            'Next
            For Each item As String In attachments
                Dim attch As Attachment = New Attachment(item)
                msgMail.Attachments.Add(attch)
            Next

            'SmtpMail.SmtpServer = _smtpServer
            'SmtpMail.Send(msgMail)
            Dim SmtpMail As New SmtpClient()
            SmtpMail.Host = _smtpServer
            SmtpMail.Send(msgMail)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub sendMail(ByVal pathTemplateFile As String, ByVal szTos As String, ByVal szCcs As String, ByVal szSubject As String, ByVal szFormatValues() As String)
        Dim emailFrom As String = ConfigurationManager.AppSettings("EmailFrom")
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(pathTemplateFile)
        Dim szEmailFormat As String = sr.ReadToEnd()
        sr.Close()
        Dim szEmailContent As String = String.Format(szEmailFormat, szFormatValues)
        If ((szTos <> "" Or szCcs <> "") And emailFrom <> "") Then
            Me.sendMail(szTos, szCcs, emailFrom, szSubject, Web.Mail.MailFormat.Html, szEmailContent)
        End If
    End Sub


End Class
