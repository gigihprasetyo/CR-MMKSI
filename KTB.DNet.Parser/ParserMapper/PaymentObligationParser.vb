#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.OnlinePayment
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser

    Public Class PaymentObligationParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private PaymentObligations As ArrayList
        Private Grammar As Regex
        Private errorMessage As StringBuilder
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            _fileName = fileName
            PaymentObligations = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                Try
                    ParsePaymentObligation(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PaymentObligationParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PaymentObligationParser, BlockName)
                    Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")

                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return PaymentObligations
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If PaymentObligations.Count > 0 Then
                Dim objFacade As PaymentObligationFacade
                For Each item As PaymentObligation In PaymentObligations
                    Try
                        objFacade = New PaymentObligationFacade(System.Threading.Thread.CurrentPrincipal)
                        objFacade.InsertFromService(item)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PaymentObligationParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PaymentObligationParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.Assignment & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")

                    End Try
                Next
            End If
            Return 0


        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParsePaymentObligation(ByVal ValParser As String)
            Dim _PaymentObligation As PaymentObligation = New PaymentObligation
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            errorMessage = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                        If Not objDealer Is Nothing Then
                            If objDealer.ID > 0 Then
                                _PaymentObligation.Dealer = objDealer

                            Else
                                errorMessage.Append("Invalid Dealer" & Chr(13) & Chr(10))
                            End If
                        Else
                            errorMessage.Append("Invalid Dealer" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        _PaymentObligation.Assignment = sTemp.Trim
                    Case Is = 2
                        Dim assType As PaymentAssignmentType = New PaymentAssignmentTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                        If Not assType Is Nothing Then
                            If assType.ID > 0 Then
                                _PaymentObligation.PaymentAssignmentType = assType
                            Else
                                errorMessage.Append("Invalid Assignment" & Chr(13) & Chr(10))
                            End If
                        Else
                            errorMessage.Append("Invalid Assignment" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        Dim assType As PaymentObligationType = New PaymentObligationTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                        If Not assType Is Nothing Then
                            If assType.ID > 0 Then
                                _PaymentObligation.PaymentObligationType = assType
                            Else
                                errorMessage.Append("Invalid Pay Type" & Chr(13) & Chr(10))
                            End If
                        Else
                            errorMessage.Append("Invalid Pay Type" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        If IsNumeric(sTemp.Trim) Then
                            _PaymentObligation.Sequence = sTemp.Trim
                        Else
                            errorMessage.Append("Invalid Sequence" & Chr(13) & Chr(10))
                        End If
                    Case Is = 5
                        Try
                            Dim _date As Date = New Date(sTemp.Trim.Substring(4, 4), sTemp.Trim.Substring(2, 2), sTemp.Trim.Substring(0, 2))
                            _PaymentObligation.DocDate = _date
                        Catch ex As Exception
                            errorMessage.Append("Invalid Docdate" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 6
                        Try
                            Dim _date As Date = New Date(sTemp.Trim.Substring(4, 4), sTemp.Trim.Substring(2, 2), sTemp.Trim.Substring(0, 2))
                            _PaymentObligation.DueDate = _date
                        Catch ex As Exception
                            errorMessage.Append("Invalid Duedate" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 7
                        If IsNumeric(sTemp.Trim) Then
                            _PaymentObligation.Amount = sTemp.Trim
                        Else
                            errorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                        End If
                    Case Is = 8
                        _PaymentObligation.Description = sTemp.Trim
                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If
            If _PaymentObligation.DocDate <> _PaymentObligation.DueDate Then
                _PaymentObligation.IsTOP = 0
            Else
                _PaymentObligation.IsTOP = 1
            End If
            'todo add transdue date
            _PaymentObligation.TransactionDueDate = _PaymentObligation.DueDate.AddDays(_PaymentObligation.Dealer.DueDate)
            _PaymentObligation.SourceDocument = EnumOnlinePayment.SourceDocument.SAP
            _PaymentObligation.SourceDocument = EnumOnlinePayment.StatusOnlinePayment.Baru
            PaymentObligations.Add(_PaymentObligation)
        End Sub

#End Region

    End Class

End Namespace
