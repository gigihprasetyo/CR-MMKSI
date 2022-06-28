Imports System.Web.Mail
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

    Public Sub sendMail(ByVal sendTo As String, ByVal ccTo1 As String, ByVal ccTo2 As String, ByVal from As String, ByVal WscTeam As String, ByVal subject As String, ByVal format As MailFormat, ByVal body As String)
        Try
            msgMail.To = sendTo
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

            msgMail.Cc = cc
            msgMail.From = from
            msgMail.Subject = subject
            msgMail.BodyFormat = format
            msgMail.Body = body
            msgMail.Priority = _priority
            SmtpMail.SmtpServer = _smtpServer
            SmtpMail.Send(msgMail)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub sendMail(ByVal sendTo As String, ByVal ccTo1 As String, ByVal bccTo As String, ByVal from As String, ByVal subject As String, ByVal format As MailFormat, ByVal body As String)
        Try
            msgMail.To = sendTo
            Dim cc As String = String.Empty
            Dim bcc As String = String.Empty

            If ccTo1.Trim <> "" Then
                cc = ccTo1
            End If


            If Not IsNothing(bccTo) Then
                bcc = bccTo
            End If

            msgMail.Cc = cc
            msgMail.Bcc = bcc
            msgMail.From = from
            msgMail.Subject = subject
            msgMail.BodyFormat = format
            msgMail.Body = body
            msgMail.Priority = _priority
            SmtpMail.SmtpServer = _smtpServer
            SmtpMail.Send(msgMail)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub sendMail(ByVal sendTo As String, ByVal ccTo1 As String, ByVal ccTo2 As String, ByVal from As String, ByVal WscTeam As String, ByVal subject As String, ByVal format As MailFormat, ByVal body As String, ByVal attachments As ArrayList)
        Try
            msgMail.To = sendTo
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

            msgMail.Cc = cc
            msgMail.From = from
            msgMail.Subject = subject
            msgMail.BodyFormat = format
            msgMail.Body = body
            msgMail.Priority = _priority
            For Each item As String In attachments
                Dim attch As MailAttachment = New MailAttachment(item)
                msgMail.Attachments.Add(attch)
            Next
            SmtpMail.SmtpServer = _smtpServer
            SmtpMail.Send(msgMail)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub sendMail(ByVal sendTo As String, ByVal ccTo As String, ByVal from As String, ByVal subject As String, ByVal format As MailFormat, ByVal body As String)
        Try
            msgMail.To = sendTo
            Dim cc As String = String.Empty
            If ccTo.Trim <> "" Then
                cc = ccTo
            End If
            msgMail.Cc = cc
            msgMail.From = from
            msgMail.Subject = subject
            msgMail.BodyFormat = format
            msgMail.Body = body
            msgMail.Priority = _priority
            SmtpMail.SmtpServer = _smtpServer
            SmtpMail.Send(msgMail)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub sendMailTraining(ByVal sendTo As String, ByVal ccTo As String, ByVal from As String, ByVal subject As String, ByVal format As MailFormat, ByVal body As String)
        Try
            Dim EmailTo As String = String.Empty
            If Not IsNothing(sendTo) Then
                msgMail.To = sendTo
            Else
                msgMail.To = EmailTo
            End If

            Dim cc As String = String.Empty
            If Not IsNothing(ccTo) Then
                msgMail.Cc = ccTo
            Else
                msgMail.Cc = cc
            End If

            msgMail.From = from
            msgMail.Subject = subject
            msgMail.BodyFormat = format
            msgMail.Body = body
            msgMail.Priority = _priority
            SmtpMail.SmtpServer = _smtpServer
            SmtpMail.Send(msgMail)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub sendMail(ByVal sendTo As String, ByVal ccTo As String, ByVal from As String, ByVal subject As String, ByVal format As MailFormat, ByVal body As String, ByVal attachments As ArrayList)
        Try
            msgMail.To = sendTo
            Dim cc As String = String.Empty
            If ccTo.Trim <> "" Then
                cc = ccTo
            End If
            msgMail.Cc = cc
            msgMail.From = from
            msgMail.Subject = subject
            msgMail.BodyFormat = format
            msgMail.Body = body
            msgMail.Priority = _priority
            For Each item As MailAttachment In attachments
                msgMail.Attachments.Add(item)
            Next

            SmtpMail.SmtpServer = _smtpServer
            SmtpMail.Send(msgMail)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub sendMail(ByVal pathTemplateFile As String, ByVal szTos As String, ByVal szCcs As String, ByVal szSubject As String, ByVal szFormatValues() As String)
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(pathTemplateFile)
        Dim szEmailFormat As String = sr.ReadToEnd()
        sr.Close()
        Dim szEmailContent As String = String.Format(szEmailFormat, szFormatValues)
        If ((szTos <> "" Or szCcs <> "") And emailFrom <> "") Then
            Me.sendMail(szTos, szCcs, emailFrom, szSubject, MailFormat.Html, szEmailContent)
        End If
    End Sub


   
End Class
