Imports System.Net
Imports System.Web.Http
Imports KTB.DNet.Parser
Imports System.Net.Http
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports System.IO

Imports System.Linq
Imports System.Data
Imports System.Text
Imports System.Security.Principal
Imports System.Web
Imports System.Web.Mvc


Public Class CommonHelper

    ''' <summary>
    ''' Log Data To DB
    ''' </summary>
    ''' <param name="str">Body Value</param>
    ''' <param name="msg">Parser Message</param>
    ''' <param name="srcIP">IP Address of Submitter</param>
    ''' <remarks></remarks>
    Public Shared Sub LogDB(ByVal str As String, ByVal msg As MessageModel, ByVal srcIP As String, ByVal cr As DateTime)
        Dim User As New System.Security.Principal.GenericPrincipal(New GenericIdentity("WebService"), Nothing)
        Dim ObjF As WsLogFacade = New WsLogFacade(User)
        Dim ObjWslog As WsLog = New WsLog
        ObjWslog.Body = str
        ObjWslog.CreatedTime = cr
        ObjWslog.Status = msg.Status.ToString()
        ObjWslog.Message = msg.Message.ToString()
        ObjWslog.Source = srcIP
        ObjF.Insert(ObjWslog)
    End Sub


    ''' <summary>
    ''' Log Data To DB
    ''' </summary>
    ''' <param name="str">Body Value</param>
    ''' <param name="msg">Parser Message</param>
    ''' <param name="srcIP">IP Address of Submitter</param>
    ''' <remarks></remarks>
    Public Shared Sub JSONLogDB(ByVal str As String, ByVal msg As MessageModel, ByVal keyName As String, ByVal srcIP As String, ByVal cr As DateTime)
        Dim User As System.Security.Principal.GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WebService"), Nothing)
        Dim ObjF As WsJSONLogFacade = New WsJSONLogFacade(User)
        Dim ObjWslog As WsJSONLog = New WsJSONLog()
        ObjWslog.Body = str
        ObjWslog.CreatedTime = cr
        ObjWslog.Status = msg.Status.ToString()
        ObjWslog.Message = msg.Message.ToString()
        ObjWslog.KeyName = keyName
        ObjWslog.Source = srcIP
        ObjF.Insert(ObjWslog)
    End Sub


    ''' <summary>
    ''' Log Data To Txt File
    ''' </summary>
    ''' <param name="str">Body Value</param>
    ''' <param name="msg">Parser Message</param>
    ''' <remarks>Please Add AppKey "LogPathTxt"</remarks>
    Public Shared Sub LogTxt(ByVal str As String, ByVal msg As MessageModel)
        Try
            Dim StrPath As String = System.Configuration.ConfigurationManager.AppSettings("LogPathTxt")
            If StrPath.Trim = "" Then
                Return
            End If
            Using sw As StreamWriter = File.AppendText(StrPath)
                sw.WriteLine("Start Log : " & Now.ToString("yyyyMMddHHmmss"))
                sw.WriteLine("Data : ")
                sw.WriteLine(str)
                sw.WriteLine("Status : ")
                sw.WriteLine(msg.Status & ";" & msg.Message)
                sw.WriteLine("End Log")
            End Using
        Catch ex As Exception

        End Try
       
    End Sub

    ''' <summary>
    ''' IP Addres Filtering
    ''' </summary>
    ''' <param name="strAddress">IP Address of Submitter</param>
    ''' <returns>Booelan</returns>
    ''' <remarks>Please Add AppKey "AllowedAddress"</remarks>
    Public Shared Function IsAllowed(ByVal strAddress As String, Optional ByVal ResendPassword As String = "") As Boolean
        Dim StrAllowed As String = System.Configuration.ConfigurationManager.AppSettings("AllowedAddress")
        Dim StrResendPassword As String = System.Configuration.ConfigurationManager.AppSettings("ResendPassword")

        'If StrAllowed.Trim() = "" Then
        '    Return True
        'End If

        If ResendPassword <> "" Then
            If ResendPassword = StrResendPassword Then
                Return True
            Else
                Return False
            End If
        End If

        If StrAllowed.Trim() = "" Then
            Return True
        End If


        For Each OBJS As String In StrAllowed.Split(New Char() {";"c})
            If OBJS.Trim() = strAddress Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Shared Function IsWSJSONAllowed(ByVal strAddress As String, Optional ByVal ResendPassword As String = "") As Boolean
        Dim StrAllowed As String = System.Configuration.ConfigurationManager.AppSettings("AllowedAddress")
        Dim StrResendPassword As String = System.Configuration.ConfigurationManager.AppSettings("WSJSONPassword")

        'If StrAllowed.Trim() = "" Then
        '    Return True
        'End If

        If ResendPassword <> "" Then
            If ResendPassword = StrResendPassword Then
                Return True
            Else
                Return False
            End If
        End If

        If StrAllowed.Trim() = "" Then
            Return True
        End If


        For Each OBJS As String In StrAllowed.Split(New Char() {";"c})
            If OBJS.Trim() = strAddress Then
                Return True
            End If
        Next

        Return False
    End Function


End Class




