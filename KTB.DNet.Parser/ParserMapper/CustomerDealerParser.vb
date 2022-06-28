 

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
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser

    Public Class CustomerDealerParser
        Inherits AbstractParser

#Region "Private Variables"

        Private _fileName As String
        Private _stream As StreamReader
        Private ErrorMessage As StringBuilder
        Private grammar As Regex
        Private _CustomerDealer As CustomerDealer
        Private _CustomerDealers As ArrayList
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _stream = New StreamReader(fileName, True)
            _CustomerDealers = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_stream).Trim()
            While (Not val = "")
                Try
                    ParseCustomerDealer(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "CustomerDealerParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CustomerDealerParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_stream)
            End While
            _stream.Close()
            _stream = Nothing
            Return _CustomerDealers
        End Function

        Protected Overrides Function DoTransaction() As Integer
            For Each item As CustomerDealer In _CustomerDealers
                Try
                    Dim objCustomerDealerFacade As CustomerDealerFacade = New CustomerDealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    objCustomerDealerFacade.Insert(item)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "CustomerDealerParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CustomerDealerParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.Dealer.DealerCode & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseCustomerDealer(ByVal streamLine As String)
            _CustomerDealer = New CustomerDealer
            ErrorMessage = New StringBuilder
            Dim sStart As Integer = 0
            Dim nCount As Integer = 0
            Dim sColumn As String
            For Each m As Match In grammar.Matches(streamLine)
                sColumn = streamLine.Substring(sStart, m.Index - sStart)
                sColumn = sColumn.Trim()
                Select Case nCount
                    Case Is = 0
                        If sColumn.Length > 0 Then
                            Try
                                Dim _dealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sColumn)
                                If _dealer.ID > 0 Then
                                    _CustomerDealer.Dealer = _dealer
                                Else
                                    ErrorMessage.Append("Dealer tidak terdefinisi." & Chr(13) & Chr(10))
                                End If
                            Catch
                                ErrorMessage.Append("Dealer Master tidak terdefinisi." & Chr(13) & Chr(10))
                            End Try
                        Else
                            ErrorMessage.Append("Dealer Master tidak terdefinisi." & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        If sColumn.Length > 0 Then
                            Dim _customer As KTB.DNet.Domain.Customer = New CustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sColumn)
                            If _customer.Id > 0 Then
                                _CustomerDealer.Customer = _customer
                            Else
                                ErrorMessage.Append("Customer tidak ditemukan" & Chr(13) & Chr(10))
                            End If
                        Else
                            ErrorMessage.Append("Customer tidak ditemukan" & Chr(13) & Chr(10))
                        End If
 
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            Else
                _CustomerDealers.Add(_CustomerDealer)
            End If
        End Sub
#End Region

    End Class

End Namespace
