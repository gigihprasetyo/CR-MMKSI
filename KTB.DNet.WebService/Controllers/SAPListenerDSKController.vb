Imports System.Net
Imports System.Web.Http
Imports KTB.DNet.Parser

Public Class SAPListenerDSKController
    Inherits ApiController

    Public Function SendData() As MessageModel '<FromBody()> 'ByVal req As HttpRequestMessage
        Dim _msg As New MessageModel
        _msg.Status = False

        Dim _err As String = "Success "
        _msg.Message = _err
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

            Dim arrData As Array = str.Split("\n")
            If arrData.Length < 300 Then
                'Data Processing
                Dim objWs As RestFullWorker = New RestFullWorker()

                _msg.Status = objWs.WSProses(str, _err)
                _msg.Message = _err
                Try
                    CommonHelper.LogDB(str, _msg, _SourceAddress, d)
                Catch ex As Exception
                    _msg.Message = _SourceAddress & " ; " & ex.Message & " ; " & _msg.Message
                    CommonHelper.LogTxt(str, _msg)
                End Try
            Else
                Dim ttt = New System.Threading.Tasks.Task(Sub()
                                                              Dim objWs As RestFullWorker = New RestFullWorker()

                                                              _msg.Status = objWs.WSProses(str, _err)
                                                              _msg.Message = _err

                                                              Try
                                                                  CommonHelper.LogDB(str, _msg, _SourceAddress, d)
                                                              Catch ex As Exception
                                                                  _msg.Message = _SourceAddress & " ; " & ex.Message & " ; " & _msg.Message
                                                                  CommonHelper.LogTxt(str, _msg)
                                                              End Try

                                                          End Sub)
                ttt.Start()
            End If
        Catch ex As Exception
            _msg.Status = False
            _msg.Message = ex.Message.ToString()
            'Finally

            '    _msg.Message = _err
            '    ''Log 

        End Try

        Return _msg
    End Function
End Class
