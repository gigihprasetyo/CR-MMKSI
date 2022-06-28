#Region "Lib"
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
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.WebService
#End Region

Public Class SAPUploadController
    Inherits ApiController

    Public Function Post() As HttpResponseMessage
        Dim result As HttpResponseMessage = Nothing
        Dim httpRequest = HttpContext.Current.Request

        Dim str As String = Request.Content.ReadAsStringAsync().Result()
        Dim _SourceAddress = HttpContext.Current.Request.UserHostAddress
        Dim strByPass As String = ""
        Dim strModuleFile As String = ""
        Dim headers = Request.Headers
        Dim isAuth As Boolean = True

        Dim varResult = New List(Of ResultModel)

        If headers.Contains("x-pass-header") Then
            strByPass = headers.GetValues("x-pass-header").First()
        End If

        If headers.Contains("x-doc-header") Then
            strModuleFile = headers.GetValues("x-doc-header").First()
        End If
        Try
            'IP Filtering
            If Not CommonHelper.IsAllowed(_SourceAddress, strByPass) OrElse strByPass = "" Then
                Throw New Exception("Forbidden Source Address")
            End If
        Catch ex As Exception
            Dim res As New ResultModel
            res.Status = -1
            res.Message = String.Format("[Unauthorized : '{0}']", ex.Message.ToString())
            varResult.Add(res)
            result = Request.CreateResponse(HttpStatusCode.Unauthorized, varResult)
            isAuth = False
        End Try


        Try
            'Document Filtering
            If strModuleFile = "" Then
                Throw New Exception("Please Define Document Type")
            End If
        Catch ex As Exception
            Dim res As New ResultModel
            res.Status = -1
            res.Message = String.Format("[Invalid Header Doc : '{0}']", ex.Message.ToString())
            varResult.Add(res)
            result = Request.CreateResponse(HttpStatusCode.NotAcceptable, varResult)
            isAuth = False
        End Try





        If isAuth Then
            If (httpRequest.Files.Count > 0) Then
                For Each file As String In httpRequest.Files
                    Dim res As New ResultModel

                    Dim vProses As New FileModel
                    Try
                        Dim postedFile = httpRequest.Files(file)
                        Dim filePath = HttpContext.Current.Server.MapPath(("~/App_Data/" + postedFile.FileName))
                        Dim fi As FileInfo = New FileInfo(filePath)

                        postedFile.SaveAs(filePath)

                        vProses.DocType = strModuleFile

                        res.Status = 1
                        res.Message = String.Format("[OK : '{0}']", filePath)
                    Catch ex As Exception
                        res.Status = 0
                        res.Message = String.Format("[Failed : '{0}']", ex.Message.ToString())
                    End Try
                    varResult.Add(res)
                Next
                result = Request.CreateResponse(HttpStatusCode.Created, varResult)
            Else
                Dim res As New ResultModel
                res.Status = 0
                res.Message = String.Format("[Failed : '{0}']", "No Files")
                varResult.Add(res)
                result = Request.CreateResponse(HttpStatusCode.BadRequest, varResult)
            End If
        End If

        'Try
        '    CommonHelper.LogDB(str, "", _SourceAddress)
        'Catch ex As Exception
        '    _msg.Message = _SourceAddress & " ; " & ex.Message & " ; " & _msg.Message
        '    CommonHelper.LogTxt(str, _msg)
        'End Try



        Return result
    End Function
End Class
