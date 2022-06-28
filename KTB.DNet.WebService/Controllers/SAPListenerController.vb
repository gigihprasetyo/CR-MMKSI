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

Public Class SAPListenerController
    Inherits ApiController ' System.Web.Mvc.Controller

    '
    ' GET: /SAPListener
    Public Function SendData() As MessageModel '<FromBody()> 'ByVal req As HttpRequestMessage
        Dim _msg As New MessageModel
        Dim _err As String = "Success "
        Dim str As String = Request.Content.ReadAsStringAsync().Result()
        Dim _SourceAddress = HttpContext.Current.Request.UserHostAddress
        Dim strByPass As String = ""

        Dim headers = Request.Headers

        If headers.Contains("x-pass-header") Then
            strByPass = headers.GetValues("x-pass-header").First()
        End If
        Dim d As DateTime = DateTime.Now

        Try
            'IP Filtering
            If Not CommonHelper.IsAllowed(_SourceAddress, strByPass) Then
                Throw New Exception("Forbidden Source Address")
            End If

            'Data Processing
            Dim objWs As RestFullWorker = New RestFullWorker()

            _msg.Status = objWs.WSProses(str, _err)
            _msg.Message = _err
        Catch ex As Exception
            _msg.Status = False
            _msg.Message = ex.Message.ToString()
        End Try

        ''Log 
        Try
            CommonHelper.LogDB(str, _msg, _SourceAddress, d)
        Catch ex As Exception
            _msg.Message = _SourceAddress & " ; " & ex.Message & " ; " & _msg.Message
            CommonHelper.LogTxt(str, _msg)
        End Try

        Return _msg
    End Function




End Class