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

    Public Class AllocateFSKINDParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private oFSKindOnVechileType As FSKindOnVechileType
        Private arrFSKindOnVechileType As ArrayList
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

                arrFSKindOnVechileType = New ArrayList()
                oFSKindOnVechileType = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            oFSKindOnVechileType = ParseHeader(line)
                            If Not IsNothing(oFSKindOnVechileType) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then oFSKindOnVechileType.ErrorMessage = errorMessage.ToString()
                                arrFSKindOnVechileType.Add(oFSKindOnVechileType)
                                oFSKindOnVechileType = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "AllocateFSKINDParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.AllocateFSKINDParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        oFSKindOnVechileType = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return arrFSKindOnVechileType
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overloads Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facFSKindOnVechileType As New FSKindOnVechileTypeFacade(user)
            For Each objFSKindOnVechileType As FSKindOnVechileType In arrFSKindOnVechileType
                Try
                    If Not IsNothing(objFSKindOnVechileType) Then
                        If objFSKindOnVechileType.ErrorMessage = String.Empty Then
                            If objFSKindOnVechileType.ID = 0 Then
                                If facFSKindOnVechileType.Insert(objFSKindOnVechileType) < 0 Then
                                    nError += 1
                                End If
                            Else
                                If facFSKindOnVechileType.Update(objFSKindOnVechileType) < 0 Then
                                    nError += 1
                                End If
                            End If
                        Else
                            Throw New Exception(objFSKindOnVechileType.ErrorMessage)
                            nError += 1
                        End If

                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & arrFSKindOnVechileType.Count.ToString(), "ws-worker", "AllocateFSKINDParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.AllocateFSKINDParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "AllocateFSKINDParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.AllocateFSKINDParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As FSKindOnVechileType
            'H;TipeKendaraan;JenisFreeService;TypeFS,Durasi
            'H;NA01;5A;Reguler;1580 

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim _oFSKindOnVechileType As New FSKindOnVechileType

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                Dim TipeKendaraan As String = cols(1).Trim
                Dim JenisFreeService As String = cols(2).Trim
                Dim TypeFS As String = cols(3).Trim
                If Not ExistFSKindOnVechileType(TipeKendaraan, JenisFreeService, _oFSKindOnVechileType) Then

                    '1 TipeKendaraan
                    If TipeKendaraan = String.Empty Then
                        writeError("Invalid TipeKendaraan")
                    Else
                        Try
                            Dim oVechileType As VechileType = New VechileTypeFacade(user).Retrieve(TipeKendaraan)
                            If oVechileType.ID > 0 Then
                                _oFSKindOnVechileType.VechileType = oVechileType
                            Else
                                writeError("Invalid TipeKendaraan")
                            End If
                        Catch ex As Exception
                            writeError("TipeKendaraan  error: " & ex.Message)
                        End Try
                    End If

                    '2 JenisFreeService
                    If JenisFreeService = String.Empty Then
                        writeError("Invalid JenisFreeService")
                    Else
                        Try
                            Dim oFSKind As FSKind = New FSKindFacade(user).Retrieve(JenisFreeService)
                            If oFSKind.ID > 0 Then
                                _oFSKindOnVechileType.FSKind = oFSKind
                            Else
                                writeError("Invalid JenisFreeService")
                            End If
                        Catch ex As Exception
                            writeError("JenisFreeService  error: " & ex.Message)
                        End Try
                    End If

                    '3 TypeFS
                    If TypeFS = String.Empty Then
                        writeError("Invalid TypeFS")
                    Else
                        Try
                            Dim oStandardCode As StandardCode = New StandardCodeFacade(user).GetByCategoryValueCode("FSType", TypeFS)
                            If oStandardCode.ID > 0 Then
                                If TypeFS = "E01" Then

                                    If _oFSKindOnVechileType.FSKind.KindCode.StartsWith("2") Then
                                        _oFSKindOnVechileType.FSType = CType(EnumFSKind.FSType.Extended2, Short)

                                    ElseIf _oFSKindOnVechileType.FSKind.KindCode.StartsWith("4") Then
                                        _oFSKindOnVechileType.FSType = CType(EnumFSKind.FSType.Extended4, Short)

                                    ElseIf _oFSKindOnVechileType.FSKind.KindCode.StartsWith("6") Then
                                        _oFSKindOnVechileType.FSType = CType(EnumFSKind.FSType.Extended6, Short)

                                    End If

                                Else
                                    _oFSKindOnVechileType.FSType = oStandardCode.ValueId
                                End If
                            Else
                                writeError("Invalid TypeFS")
                            End If
                        Catch ex As Exception
                            writeError("TypeFS  error: " & ex.Message)
                        End Try
                    End If
                End If

                '4 Durasi
                Try
                    _oFSKindOnVechileType.Duration = CInt(cols(4).Trim)
                Catch ex As Exception
                    writeError("Invalid Durasi")
                End Try



                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    _oFSKindOnVechileType.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    _oFSKindOnVechileType.LastUpdateBy = "WS"
                End If
            End If

            Return _oFSKindOnVechileType
        End Function

        Private Function ExistFSKindOnVechileType(ByVal TipeKendaraan As String, ByVal JenisFreeService As String, ByRef oFSKindOnVechileType As FSKindOnVechileType) As Boolean
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.VechileTypeCode", MatchType.Exact, TipeKendaraan))
            crit.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, JenisFreeService))
            'crit.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSType", MatchType.Exact, TypeFS))
            Dim arlFSKindOnVechileType As ArrayList = New FSKindOnVechileTypeFacade(user).Retrieve(crit)
            If arlFSKindOnVechileType.Count > 0 Then
                oFSKindOnVechileType = CType(arlFSKindOnVechileType(0), FSKindOnVechileType)
                Return True
            End If
            Return False
        End Function
#End Region

    End Class
End Namespace
