#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.Service

#End Region

Namespace KTB.DNet.Parser
    Public Class FreeServicePartDetailParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private _arrFSPartDetail As ArrayList
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS"), Nothing)
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub
#End Region

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String
                Dim nError As Integer = 0
                Dim errMsgSummary As String = String.Empty

                _arrFSPartDetail = New ArrayList()

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind.Trim = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ParseHeader(line)
                        End If
                    Catch ex As Exception
                        nError += 1
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "FreeServicePartDetailParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.FreeServicePartDetailParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _arrFSPartDetail = Nothing
                        errMsgSummary = ex.Message & ";"
                    End Try
                Next

                If nError > 0 Then
                    Throw New Exception(errMsgSummary)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrFSPartDetail
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim facFSPartDetail As New FreeServicePartDetailFacade(user)
            For Each objFSPartDetail As FreeServicePartDetail In _arrFSPartDetail
                Try
                    If Not IsNothing(objFSPartDetail) Then
                        If objFSPartDetail.ErrorMessage = String.Empty Then
                            Dim IDData As Integer = 0
                            If facFSPartDetail.Insert(objFSPartDetail) < 0 Then
                                nError += 1
                            End If
                        End If
                    Else
                        Throw New Exception(objFSPartDetail.ErrorMessage)
                        nError += 1
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrFSPartDetail.Count.ToString(), "ws-worker", "FreeServicePartDetailParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.FreeServicePartDetailParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "FreeServicePartDetailParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.FreeServicePartDetailParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#Region "Custom Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String)
            Dim cols As String() = line.Split(MyBase.ColSeparator)

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                Dim objFSPartDetail As New FreeServicePartDetail
                Dim PDCode As String
                '1 FreeServiceID
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("Free Service ID can't be empty")
                Else
                    Try
                        Dim crtDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crtDealer.opAnd(New Criteria(GetType(FreeService), "ID", MatchType.Exact, PDCode))
                        Dim objFreeService As FreeService = New FreeServiceFacade(user).Retrieve(crtDealer)(0)
                        If Not IsNothing(objFreeService) Then
                            objFSPartDetail.FreeService = objFreeService
                        Else
                            Throw New Exception("Invalid FreeServiceID " & PDCode)
                        End If
                    Catch ex As Exception
                        writeError("FreeServiceID  error: " & ex.Message)
                    End Try
                End If

                ''2 PartNo
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    writeError("PartNo can't be empty")
                Else
                    objFSPartDetail.PartNo = PDCode
                End If

                '3 PartName
                PDCode = cols(3).Trim
                If PDCode = String.Empty Then
                    writeError("PartName can't be empty")
                Else
                    objFSPartDetail.PartName = PDCode
                End If

                '4 PartPrice
                PDCode = cols(4).Trim
                If PDCode = String.Empty Then
                    writeError("PartPrice can't be empty")
                Else
                    objFSPartDetail.PartPrice = PDCode
                End If

                '5 LabourPrice
                PDCode = cols(4).Trim
                If PDCode = String.Empty Then
                    writeError("LabourPrice can't be empty")
                Else
                    objFSPartDetail.LabourPrice = PDCode
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objFSPartDetail.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objFSPartDetail.LastUpdateBy = user.Identity.Name
                End If

                If Not IsNothing(objFSPartDetail) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objFSPartDetail.ErrorMessage = errorMessage.ToString()
                    _arrFSPartDetail.Add(objFSPartDetail)
                    objFSPartDetail = Nothing
                End If

            End If
        End Function

#End Region
    End Class
End Namespace
