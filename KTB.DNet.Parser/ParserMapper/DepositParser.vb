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

    Public Class DepositParser
        Inherits AbstractParser

#Region "Private Variables"

        Private _fileName As String
        Private _stream As StreamReader
        Private ErrorMessage As StringBuilder
        Private grammar As Regex
        Private _Deposit As Deposit  '-- Deposit header & its details

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
                    ParseDeposit(StreamLine & ";")
                    StreamLine = MyBase.NextLine(_stream).Trim()  '-- Detail line
                    While StreamLine <> ""
                        Try
                            ParseDepositLine(StreamLine & ";")
                        Catch ex As Exception
                            SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DepositParser, BlockName)
                            Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message)
                            ExceptionPolicy.HandleException(e, "Parser Policy")
                        End Try
                        StreamLine = MyBase.NextLine(_stream).Trim()  '-- Next detail line
                    End While
                End If
            Catch ex As Exception
                _Deposit = Nothing  '-- If any error occurs then set with Nothing
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DepositParser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message)
                ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try
            _stream.Close()  '-- Close & dispose stream
            _stream = Nothing
            Return _Deposit  '-- Successfully read deposit text file, so return it

        End Function

        Protected Overrides Function DoTransaction() As Integer
            '-- Insert or replace a deposit

            Try
                Dim _depositFacade As New DepositFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                _depositFacade.Insert(_Deposit)

            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DepositParser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message)
                ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try

        End Function
        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseDeposit(ByVal streamLine As String)
            '-- Parse deposit header

            _Deposit = New Deposit  '-- Create deposit
            ErrorMessage = New StringBuilder
            Dim sStart As Integer = 0
            Dim nCount As Integer = 0
            Dim sColumn As String

            For Each m As Match In grammar.Matches(streamLine)

                sColumn = streamLine.Substring(sStart, m.Index - sStart)
                sColumn = sColumn.Trim()  '-- Truncate leading & trailing spaces

                Select Case nCount
                    Case Is = 0  '-- Dealer
                        If sColumn.Length > 0 Then
                            Try
                                Dim _dealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sColumn)
                                If _dealer.ID > 0 Then
                                    _Deposit.Dealer = _dealer
                                Else
                                    ErrorMessage.Append("Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                                    'Throw New Exception("Dealer code tidak terdefinisi.")
                                End If
                            Catch ex As Exception
                                ErrorMessage.Append(ex.Message.ToString() & "Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                                'Throw New Exception("Dealer code tidak terdefinisi.")
                            End Try
                        Else
                            ErrorMessage.Append("Dealer code tidak terdefinisi." & Chr(13) & Chr(10))
                            'Throw New Exception("Dealer code kosong.")
                        End If

                    Case Is = 1  '-- Period (*** test domain constraint ***)
                        If sColumn.Length = 6 Then
                            _Deposit.Period = sColumn
                        Else
                            ErrorMessage.Append("Invalid Periode." & Chr(13) & Chr(10))
                            'Throw New Exception("Period invalid.")
                        End If

                    Case Is = 3  '-- Beginning balance
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _Deposit.BegBalance = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("Beginning balance tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Beginning balance tidak valid.")
                        End Try

                    Case Is = 4  '-- Total debit
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _Deposit.TotalDebit = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("Total debit tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Total debit tidak valid.")
                        End Try

                    Case Is = 5  '-- Total credit
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _Deposit.TotalCredit = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("Total Credit tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Total credit tidak valid.")
                        End Try

                    Case Is = 6  '-- Ending balance
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _Deposit.EndBalance = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("Ending balance tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Ending balance tidak valid.")
                        End Try

                        '--start anh 20140910 req by anq
                    Case Is = 7  '-- Available deposit
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _Deposit.AvailableDeposit = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("Available deposit tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Ending balance tidak valid.")
                        End Try
                    Case Is = 8  '-- Giro Receive
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _Deposit.GiroReceive = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("Giro Receive tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Ending balance tidak valid.")
                        End Try
                    Case Is = 9  '-- RO
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _Deposit.RO = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("RO tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Ending balance tidak valid.")
                        End Try
                    Case Is = 10  '-- Service
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _Deposit.Service = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("Service tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Ending balance tidak valid.")
                        End Try
                    Case Is = 11  '-- Inclearing
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _Deposit.InClearing = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("Inclearing tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Ending balance tidak valid.")
                        End Try
                        '--end anh 20140910 req by anq

                End Select

                sStart = m.Index + 1  '-- Next start index
                nCount += 1           '-- Next column
            Next
            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If
        End Sub

        Private Sub ParseDepositLine(ByVal streamLine As String)
            '-- Parse deposit line

            Dim _DepositLine As DepositLine = New DepositLine
            ErrorMessage = New StringBuilder
            Dim sStart As Integer = 0
            Dim nCount As Integer = 0
            Dim sColumn As String

            For Each m As Match In grammar.Matches(streamLine)

                sColumn = streamLine.Substring(sStart, m.Index - sStart)
                sColumn = sColumn.Trim()

                Select Case nCount
                    Case Is = 2  '-- PostingDate
                        If sColumn.Length > 0 Then
                            If sColumn = "00000000" Then  '-- Means empty date
                                sColumn = "01011753"
                            End If
                            Try
                                _DepositLine.PostingDate = New Date(sColumn.Substring(4, 4), sColumn.Substring(2, 2), sColumn.Substring(0, 2))
                            Catch ex As Exception
                                ErrorMessage.Append("Tanggal posting tidak valid." & Chr(13) & Chr(10))
                                'Throw New Exception("Tanggal posting tidak valid.")
                            End Try
                        Else
                            ErrorMessage.Append("Tanggal posting tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Tanggal posting kosong.")
                        End If

                    Case Is = 3  '-- Clearing date
                        If sColumn.Length > 0 Then
                            If sColumn = "00000000" Then  '-- Means empty date
                                sColumn = "01011753"
                            End If
                            Try
                                _DepositLine.ClearingDate = New Date(sColumn.Substring(4, 4), sColumn.Substring(2, 2), sColumn.Substring(0, 2))
                            Catch ex As Exception
                                ErrorMessage.Append("Tanggal Clearing tidak valid." & Chr(13) & Chr(10))
                                'Throw New Exception("Tanggal clearing tidak valid.")
                            End Try
                        Else
                            ErrorMessage.Append("Tanggal Clearing tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Tanggal clearing kosong.")
                        End If

                    Case Is = 4  '-- Debit
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _DepositLine.Debit = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("Debit tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Debit tidak valid.")
                        End Try

                    Case Is = 5  '-- Credit
                        If sColumn.Length = 0 Then
                            sColumn = "0"
                        End If
                        Try
                            _DepositLine.Credit = CType(sColumn, Decimal)
                        Catch ex As Exception
                            ErrorMessage.Append("Credit tidak valid." & Chr(13) & Chr(10))
                            'Throw New Exception("Credit tidak valid.")
                        End Try

                    Case Is = 6  '-- Document No
                        _DepositLine.DocumentNo = sColumn

                    Case Is = 7  '-- Reference no
                        _DepositLine.ReferenceNo = sColumn

                    Case Is = 8  '-- Invoice no
                        _DepositLine.InvoiceNo = sColumn

                    Case Is = 9  '-- Remark
                        _DepositLine.Remark = sColumn
                    Case Is = 10  '-- PaymentType

                        Select Case sColumn.Trim.ToUpper()
                            Case EnumDepositLine.PaymentType.TOP.ToString().ToUpper()
                                _DepositLine.PaymentType = CShort(EnumDepositLine.PaymentType.TOP)
                            Case Else
                                _DepositLine.PaymentType = CShort(EnumDepositLine.PaymentType.COD)
                        End Select
                        If sColumn.Trim() <> String.Empty Then

                        Else

                        End If


                End Select

                sStart = m.Index + 1  '-- Next start index
                nCount += 1           '-- Next column
            Next
            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If
            _DepositLine.Deposit = _Deposit
            _Deposit.DepositLines.Add(_DepositLine)
        End Sub

#End Region

    End Class

End Namespace
