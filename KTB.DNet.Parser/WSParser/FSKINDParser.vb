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
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.Profile
#End Region

Namespace KTB.DNet.Parser
    Public Class FSKINDParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private oFSKind As FSKind
        Private arrFSKind As ArrayList
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub
#End Region
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++
#Region "Protected Methods"
        Protected Overloads Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                arrFSKind = New ArrayList()
                oFSKind = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            oFSKind = ParseHeader(line)
                            If Not IsNothing(oFSKind) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then oFSKind.ErrorMessage = errorMessage.ToString()
                                arrFSKind.Add(oFSKind)
                                oFSKind = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "FSKINDParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.FSKINDParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        oFSKind = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return arrFSKind
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overloads Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facFSKind As New FSKindFacade(user)
            For Each objFSKind As FSKind In arrFSKind
                Try
                    If Not IsNothing(objFSKind) Then
                        If objFSKind.ErrorMessage = String.Empty Then
                            If objFSKind.ID = 0 Then
                                If facFSKind.Insert(objFSKind) < 0 Then
                                    nError += 1
                                End If
                            Else
                                If facFSKind.Update(objFSKind) < 0 Then
                                    nError += 1
                                End If
                            End If
                        Else
                            Throw New Exception(objFSKind.ErrorMessage)
                            nError += 1
                        End If

                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & arrFSKind.Count.ToString(), "ws-worker", "FSKINDParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.FSKINDParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "FSKINDParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.FSKINDParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#End Region


#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As FSKind
            'H;Kode;KM;Deskripsi;Status
            'H;11A;51000;FS 11A (50.000 KM);0


            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim _oFSKind As New FSKind

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                If Not ExistFSKindCode(cols(1).Trim, _oFSKind) Then
                    '1 Kode
                    Try
                        _oFSKind.KindCode = cols(1).Trim
                    Catch ex As Exception
                        Throw New Exception("Invalid Kode")
                    End Try
                End If

                '2 KM
                Try
                    _oFSKind.KM = CInt(cols(2).Trim)
                Catch ex As Exception
                    Throw New Exception("Invalid KM")
                End Try

                '3 Deskripsi
                Try
                    _oFSKind.KindDescription = cols(3).Trim
                Catch ex As Exception
                    Throw New Exception("Invalid Deskripsi")
                End Try


                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    _oFSKind.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    _oFSKind.LastUpdateBy = "WS"
                End If
            End If

            Return _oFSKind
        End Function

        Private Function ExistFSKindCode(ByVal kindCode As String, ByRef refFSKind As FSKind) As Boolean
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oFSKind As FSKind = New FSKindFacade(user).Retrieve(kindCode)
            If oFSKind.ID > 0 Then
                refFSKind = oFSKind
                Return True
            End If
            Return False
        End Function
#End Region
    End Class
End Namespace
