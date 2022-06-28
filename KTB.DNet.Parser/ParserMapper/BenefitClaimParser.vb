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
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Benefit

#End Region

Namespace KTB.DNet.Parser
    Public Class BenefitClaimParser
        Inherits AbstractParser
#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private BenefitClaimHeaders As ArrayList
        Private BenefitClaimDetails As ArrayList
        Private _fileName As String
        Private _benefitClaimHeader As BenefitClaimHeader  'Header
        Private _benefitClaimDetails As BenefitClaimDetails
        Private errorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                _fileName = fileName
                Dim val As String
                BenefitClaimHeaders = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If Not _benefitClaimHeader Is Nothing Then
                            BenefitClaimHeaders.Add(_benefitClaimHeader)  'customer header input text
                        End If
                        _benefitClaimHeader = ParseBenefitClaimHeader(val + Delimited)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "BenefitClaimParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BenefitClaimParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _benefitClaimHeader = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While
                If Not _benefitClaimHeader Is Nothing Then
                    BenefitClaimHeaders.Add(_benefitClaimHeader)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return BenefitClaimHeaders
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim _objBenefitClaimHeader As BenefitClaimHeader = Nothing
            For Each item As BenefitClaimHeader In BenefitClaimHeaders
                Try
                    Dim _benefitClaimHeaderFacade As BenefitClaimHeaderFacade = New BenefitClaimHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                    _objBenefitClaimHeader = _benefitClaimHeaderFacade.RetrieveByRegNumber(item.ClaimRegNo)
                    If _objBenefitClaimHeader Is Nothing Then
                        errorMessage.Append("Claim is not found" & Chr(13) & Chr(10))
                    Else
                        _benefitClaimHeaderFacade.SynchronizeSAPToDNET(item, _objBenefitClaimHeader)
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "BenefitClaimParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BenefitClaimParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _objBenefitClaimHeader.ID & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
            Return 1
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseBenefitClaimHeader(ByVal ValParser As String) As BenefitClaimHeader
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            errorMessage = New StringBuilder
            _benefitClaimHeader = Nothing
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0 'Dealer Code
                        'If sTemp.Trim.Length > 0 Then
                        '    Try
                        '        Dim dealerCode As String = sTemp.Trim
                        '        _dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(dealerCode)

                        '    Catch ex As Exception
                        '        errorMessage.Append("Dealer code not found" & Chr(13) & Chr(10))
                        '        Exit Select
                        '    End Try

                        'Else
                        '    errorMessage.Append("Dealer code is Empty" & Chr(13) & Chr(10))
                        'End If

                    Case Is = 1 'Claim Number
                        If sTemp.Trim.Length > 0 Then
                            Try
                                Dim claiNumber As String = sTemp.Trim
                                _benefitClaimHeader = New BenefitClaimHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByRegNumber(claiNumber)

                                If Not IsNothing(_dealer) Then
                                    _benefitClaimHeader.Dealer = _dealer
                                End If
                            Catch ex As Exception
                                errorMessage.Append("Claim No not found" & Chr(13) & Chr(10))
                                Exit Select
                            End Try

                        Else
                            errorMessage.Append("Claim No is Empty" & Chr(13) & Chr(10))
                        End If

                    Case Is = 2 'JV Number
                        If sTemp.Trim.Length > 0 Then
                            Try
                                Dim jvNumber As String = sTemp.Trim
                                If Not IsNothing(_benefitClaimHeader) Then
                                    _benefitClaimHeader.JVNumber = jvNumber
                                End If
                            Catch ex As Exception
                                errorMessage.Append("JV Number not found" & Chr(13) & Chr(10))
                                Exit Select
                            End Try

                        Else
                            errorMessage.Append("JV Number is Empty" & Chr(13) & Chr(10))
                        End If

                    Case Is = 3 'Actual Payment Date
                        If sTemp.Trim.Length > 0 Then
                            Try
                                Dim _actualPaymentDate2 As New DateTime
                                Dim _actualPaymentDate As String = sTemp.Trim
                                If _actualPaymentDate.Trim <> "" AndAlso _actualPaymentDate <> 0 Then
                                    If Len(Left(_actualPaymentDate, 4)) <> 4 Then
                                        errorMessage.Append("Actual Payment Date not valid" & Chr(13) & Chr(10))
                                        Exit Select
                                    End If
                                    If Len(Mid(_actualPaymentDate, 5, 2)) <> 2 Then
                                        errorMessage.Append("Actual Payment Date not valid" & Chr(13) & Chr(10))
                                        Exit Select
                                    End If
                                    If Len(Mid(_actualPaymentDate, 7, 2)) <> 2 Then
                                        errorMessage.Append("Actual Payment Date not valid" & Chr(13) & Chr(10))
                                        Exit Select
                                    End If
                                    Try
                                        _actualPaymentDate2 = New DateTime(Left(_actualPaymentDate, 4), Mid(_actualPaymentDate, 5, 2), Mid(_actualPaymentDate, 7, 2), 0, 0, 0)
                                    Catch ex As Exception
                                        errorMessage.Append("Actual Payment Date not valid" & Chr(13) & Chr(10))
                                        Exit Select
                                    End Try
                                End If

                                If Not IsNothing(_benefitClaimHeader) Then
                                    _benefitClaimHeader.ActualPaymentDate = _actualPaymentDate
                                End If

                            Catch ex As Exception
                                errorMessage.Append("Actual Payment Date not found" & Chr(13) & Chr(10))
                                Exit Select
                            End Try

                        Else
                            errorMessage.Append("Actual Payment Date is Empty" & Chr(13) & Chr(10))
                        End If

                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If errorMessage.Length > 0 Then
                _benefitClaimHeader = Nothing 'Add By Dna on 20130201 For (error in textfile:condition before the update-> row-1 valid, row-2 invalid, then row-1 will be update by row-2)
                Throw New Exception(errorMessage.ToString)
            End If
            Return _benefitClaimHeader
        End Function

#End Region

    End Class

End Namespace