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
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.FinishUnit

#End Region

Namespace KTB.DNet.Parser

    Public Class LogModelParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aVTHs As ArrayList
        Private _oVTH As VechileType
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _aVTHs = New ArrayList()
                _oVTH = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oVTH) Then
                                _aVTHs.Add(_oVTH)
                                _oVTH = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oVTH = ParseHeader(line)

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "LogModelParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LogModelParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oVTH = Nothing
                    End Try
                Next

                If Not IsNothing(_oVTH) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oVTH.ErrorMessage = errorMessage.ToString()
                    _aVTHs.Add(_oVTH)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try


            Return _aVTHs
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)


            Dim oVTFac As New VechileTypeFacade(user)

            For Each oVT As VechileType In _aVTHs
                Try
                    If Not IsNothing(oVT) AndAlso oVT.ID > 0 Then
                        If oVTFac.Update(oVT) < 1 Then
                            nError += 1
                        End If
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next


            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aVTHs.Count.ToString(), "ws-worker", "LogModelParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LogModelParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "LogModelParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.LogModelParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As VechileType
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oVT As New VechileType
            Dim oVTFac As New VechileTypeFacade(user)

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else

                Dim strData As String = String.Empty

                Try '1 VechileTypeCode
                    Dim PDCode As String = cols(1).Trim
                    strData = PDCode
                    If PDCode = String.Empty Then
                        Throw New Exception("Empty Vehicle Type Code " & strData)
                    Else
                        Dim ObjArr As VechileType = oVTFac.Retrieve(PDCode)
                        If Not IsNothing(ObjArr) AndAlso ObjArr.ID > 0 Then
                            oVT = ObjArr
                        Else
                            Throw New Exception("Invalid Vehicle Type Code " & strData)
                        End If

                    End If
                Catch ex As Exception
                    writeError("Vehicle Type  error: " & ex.Message)
                End Try

                '2 SAP Model
                If cols(2).Trim = String.Empty Then
                    writeError("SAP Model can't be empty")
                Else
                    Try ' Code
                        Dim PDCode As String = cols(2).Trim
                        oVT.SAPModel = PDCode
                    Catch ex As Exception
                        writeError("SAP Model error: " & ex.Message)
                    End Try

                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    oVT.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    oVT.LastUpdateBy = "WS"
                End If
            End If

            Return oVT
        End Function

#End Region

    End Class
End Namespace
