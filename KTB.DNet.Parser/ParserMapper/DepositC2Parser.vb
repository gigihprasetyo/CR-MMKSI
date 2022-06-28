#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Text
#End Region

#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser

    Public Class DepositC2Parser
        Inherits AbstractParser

#Region "Private Variables"

        Private _fileName As String
        Private _stream As StreamReader
        Private ErrorMessage As StringBuilder
        Private grammar As Regex
        Private _DepositC2 As DepositC2  '-- Deposit header & its details

#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object

            Try
                _fileName = fileName  '-- The file that gets parsed
                _stream = New StreamReader(fileName, True)  '-- Open stream

                Dim StreamLine As String = MyBase.NextLine(_stream).Trim()  '-- Header line

                If StreamLine <> "" Then

                    ParseDepositC2(StreamLine & ";")
                    StreamLine = MyBase.NextLine(_stream).Trim()  '-- Detail line

                    Do While StreamLine <> ""

                        ParseDepositC2Line(StreamLine & ";")
                        StreamLine = MyBase.NextLine(_stream).Trim()  '-- Next detail line
                    Loop
                End If

                _stream.Close()  '-- Close & dispose stream
                _stream = Nothing

            Catch ex As Exception
                _DepositC2 = Nothing  '-- If any error occurs then set with Nothing
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositC2Parser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DepositC2Parser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message)
                ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try

            Return _DepositC2  '-- Successfully read deposit text file, so return it

        End Function

        Protected Overrides Function DoTransaction() As Integer
            '-- Insert or replace a deposit

            Try
                Dim _depositFacade As New DepositC2Facade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                _depositFacade.Insert(_DepositC2)

            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositC2Parser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DepositC2Parser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message)
                ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try

        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseDepositC2(ByVal streamLine As String)
            '-- Parse deposit header

            _DepositC2 = New DepositC2  '-- Create deposit

            Dim sStart As Integer = 0
            Dim nCount As Integer = 0
            Dim sColumn As String
            ErrorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(streamLine)

                sColumn = streamLine.Substring(sStart, m.Index - sStart)
                sColumn = sColumn.Trim()  '-- Truncate leading & trailing spaces

                Select Case nCount
                    Case Is = 0  '-- Dealer
                        If sColumn.Length > 0 Then
                            Try
                                Dim _dealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sColumn)
                                If _dealer.ID > 0 Then
                                    _DepositC2.Dealer = _dealer
                                Else
                                    ErrorMessage.Append("Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                                    'Throw New Exception("Dealer code tidak terdefinisi.")
                                End If
                            Catch
                                ErrorMessage.Append("Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                                'Throw New Exception("Dealer code tidak terdefinisi.")
                            End Try
                        Else
                            ErrorMessage.Append("Dealer code Kosong." & Chr(13) & Chr(10))
                            'Throw New Exception("Dealer code kosong.")
                        End If

                    Case Is = 1  '-- Period (*** test domain constraint ***)
                        If sColumn.Length = 6 Then
                            _DepositC2.Period = sColumn
                        Else
                            ErrorMessage.Append("Invalid Periode." & Chr(13) & Chr(10))
                            'Throw New Exception("Period invalid.")
                        End If
                End Select

                sStart = m.Index + 1  '-- Next start index
                nCount += 1           '-- Next column
            Next
            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If
        End Sub

        Private Sub ParseDepositC2Line(ByVal streamLine As String)
            '-- Parse deposit line

            Dim _DepositC2Line As DepositC2Line = New DepositC2Line
            ErrorMessage = New StringBuilder
            Dim sStart As Integer = 0
            Dim nCount As Integer = 0
            Dim sColumn As String

            For Each m As Match In grammar.Matches(streamLine)

                sColumn = streamLine.Substring(sStart, m.Index - sStart)
                sColumn = sColumn.Trim()

                Select Case nCount
                    Case Is = 0  '-- Document date
                        If sColumn.Length > 0 Then
                            If sColumn = "00000000" Then  '-- Means empty date
                                sColumn = "01011753"
                            End If
                            Try
                                _DepositC2Line.DocumentDate = New Date(sColumn.Substring(4, 4), sColumn.Substring(2, 2), sColumn.Substring(0, 2))
                            Catch ex As Exception
                                ErrorMessage.Append("Tanggal dokumen tidak valid." & Chr(13) & Chr(10))
                                'Throw New Exception("Tanggal dokumen tidak valid.")
                            End Try
                        Else
                            ErrorMessage.Append("Tanggal dokumen Kosong." & Chr(13) & Chr(10))
                            'Throw New Exception("Tanggal dokumen kosong.")
                        End If

                    Case Is = 1  '-- Document no
                        _DepositC2Line.DocumentNo = sColumn

                    Case Is = 2  '-- DepositC2
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _DepositC2Line.DepositC2Amnt = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("Nilai depositC2 tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Nilai depositC2 tidak valid.")
                        End Try

                End Select

                sStart = m.Index + 1  '-- Next start index
                nCount += 1           '-- Next column
            Next
            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If
            _DepositC2Line.DepositC2 = _DepositC2  '-- Assign its parent first
            _DepositC2.DepositC2Lines.Add(_DepositC2Line)  '-- Add DepositC2 line
        End Sub

#End Region

    End Class

End Namespace
