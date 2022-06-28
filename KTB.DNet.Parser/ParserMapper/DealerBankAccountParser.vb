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
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser

    Public Class DealerBankAccountParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private DealerBankAccounts As ArrayList
        Private _fileName As String
        Private _DealerBankAccount As DealerBankAccount
        Private errorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                _Stream = New StreamReader(fileName, True)
                _fileName = fileName
                DealerBankAccounts = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                While (Not val = "")
                    Try
                        If Not _DealerBankAccount Is Nothing Then
                            DealerBankAccounts.Add(_DealerBankAccount)
                        End If
                        _DealerBankAccount = ParseDealerBankAccount(val + delimited)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DealerBankAccountParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DealerBankAccountParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DealerBankAccount = Nothing
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While
                If Not _DealerBankAccount Is Nothing Then
                    DealerBankAccounts.Add(_DealerBankAccount)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                _Stream.Close()
                _Stream = Nothing
            End Try
            Return DealerBankAccounts
        End Function

        Protected Overrides Function DoTransaction() As Integer
            For Each item As DealerBankAccount In DealerBankAccounts
                Try
                    Dim i As Int16 = New DealerBankAccountFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(item)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DealerBankAccountParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DealerBankAccountParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.Dealer.ID & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")

                End Try
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseDealerBankAccount(ByVal ValParser As String) As DealerBankAccount
            _DealerBankAccount = New DealerBankAccount
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            errorMessage = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Trim.Length > 0 Then
                            'Mapping dari DealerCode menjadi DealerID
                            Dim objDealer As Dealer = RetrieveDealer(sTemp.Trim)
                            If objDealer.ID > 0 Then
                                _DealerBankAccount.Dealer = objDealer
                            Else
                                Throw New Exception("Dealer Tidak Ketemu")
                            End If
                        Else
                            errorMessage.Append("Invalid Dealer Code" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        If sTemp.Length > 0 Then
                            _DealerBankAccount.BankKey = sTemp.Trim
                        Else
                            errorMessage.Append("Invalid Bank Key" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If sTemp.Length > 0 Then
                            _DealerBankAccount.BankAccount = sTemp.Trim 'CDec(sTemp.Trim)
                        Else
                            errorMessage.Append("Invalid Bank Account" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If sTemp.Length > 0 Then
                            _DealerBankAccount.BankName = sTemp.Trim
                        Else
                            errorMessage.Append("Invalid Bank Name" & Chr(13) & Chr(10))
                        End If
                    Case Else
                        'Do Nothing
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If
            Return _DealerBankAccount
        End Function

        Private Function RetrieveDealer(ByVal code As String) As Dealer
            Dim _dealerFacade As DealerFacade = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return _dealerFacade.Retrieve(code)
        End Function

#End Region

    End Class
End Namespace
