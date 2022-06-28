#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
#End Region

Namespace KTB.DNet.Parser
    Public Class DepositBDebitNoteParser
        Inherits AbstractParser
#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private ArrDepositBDebitNote As ArrayList
        Private _DepositBDebitNote As DepositBDebitNote
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
                ArrDepositBDebitNote = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If Not _DepositBDebitNote Is Nothing Then
                            ArrDepositBDebitNote.Add(_DepositBDebitNote)
                        End If
                        _DepositBDebitNote = ParseDepositBDebitNote(val + Delimited)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBDebitNoteParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DepositBDebitNoteParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DepositBDebitNote = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While
                If Not _DepositBDebitNote Is Nothing Then
                    ArrDepositBDebitNote.Add(_DepositBDebitNote)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return ArrDepositBDebitNote
        End Function

        Protected Overrides Function DoTransaction() As Integer
            For Each item As DepositBDebitNote In ArrDepositBDebitNote
                Try
                    Dim vResult As Integer
                    Dim iUser As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
                    Dim iBilling As TrBillingHeader = New TrBillingHeaderFacade(iUser).Retrieve(item.DNNumber)

                    If iBilling.ID > 0 Then
                        Continue For
                    End If

                    Dim _DepositBDebitNoteFacade As New DepositBDebitNoteFacade(iUser)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "Dealer.DealerCode", MatchType.Exact, item.Dealer.DealerCode))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "ProductCategory.Code", MatchType.Exact, item.ProductCategory.Code))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "DNNumber", MatchType.Exact, item.DNNumber))

                    Dim arlDepositBDebitNotes As ArrayList = _DepositBDebitNoteFacade.Retrieve(criterias)

                    If arlDepositBDebitNotes.Count > 0 Then
                        Dim objNew As DepositBDebitNote = CType(arlDepositBDebitNotes(0), DepositBDebitNote)
                        objNew.Amount = item.Amount
                        objNew.Assignment = item.Assignment
                        objNew.Description = item.Description

                        vResult = _DepositBDebitNoteFacade.Update(objNew)
                    Else
                        vResult = _DepositBDebitNoteFacade.Insert(item)
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBDebitNoteParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BenefitClaimParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.DNNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
            Return 1
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseDepositBDebitNote(ByVal ValParser As String) As DepositBDebitNote
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            errorMessage = New StringBuilder
            _DepositBDebitNote = New DepositBDebitNote

            '100169;1800026545;66700334;ACCRUED NOV 2015;MMC-Dealer Identity PT. BMM - Wahid Hasyim;11032016;MMC
            'Kodedealer;docnumber;amount;assignment;text;postingdate;mmc/mftbc

            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0 'Dealer Code
                        If sTemp.Trim.Length > 0 Then
                            Try
                                Dim dealerCode As String = sTemp.Trim
                                _dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(dealerCode)
                                If Not IsNothing(_dealer) Then
                                    _DepositBDebitNote.Dealer = _dealer
                                End If
                            Catch ex As Exception
                                errorMessage.Append("Dealer code not found" & Chr(13) & Chr(10))
                                Exit Select
                            End Try

                        Else
                            errorMessage.Append("Dealer code is Empty" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1 'DNN Number
                        If sTemp.Trim.Length > 0 Then
                            Try
                                'Dim dnNumber As String = sTemp.Trim
                                '_DepositBDebitNote = New DepositBDebitNoteFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(dnNumber)
                                _DepositBDebitNote.DNNumber = sTemp.Trim

                            Catch ex As Exception
                                errorMessage.Append("Invalid Debit Note Number" & Chr(13) & Chr(10))
                                'Exit Select
                            End Try

                        Else
                            errorMessage.Append("DNNumber is Empty" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2 'Amount
                        If sTemp.Trim.Length > 0 Then
                            Try
                                _DepositBDebitNote.Amount = CDec(sTemp.Trim)
                            Catch ex As Exception
                                errorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                                Exit Select
                            End Try

                        Else
                            _DepositBDebitNote.Amount = 0
                        End If
                    Case Is = 3 'Assignment
                        If sTemp.Trim.Length > 0 Then
                            Try
                                _DepositBDebitNote.Assignment = sTemp.Trim
                            Catch ex As Exception
                                errorMessage.Append("Invalid assignment" & Chr(13) & Chr(10))
                                Exit Select
                            End Try
                        End If
                    Case Is = 4 'Description
                        If sTemp.Trim.Length > 0 Then
                            Try
                                _DepositBDebitNote.Description = sTemp.Trim
                            Catch ex As Exception
                                errorMessage.Append("Invalid Description" & Chr(13) & Chr(10))
                                Exit Select
                            End Try
                        End If
                    Case Is = 5 'PostingDate
                        If sTemp.Trim.Length > 0 Then
                            Try
                                Dim objDate As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2), 0, 0, 0)
                                _DepositBDebitNote.PostingDate = objDate
                            Catch ex As Exception
                                errorMessage.Append("Invalid Posting Date" & Chr(13) & Chr(10))
                                Exit Select
                            End Try
                        End If
                    Case Is = 6 'ProductCategory
                        If sTemp.Trim.Length > 0 Then
                            Try
                                Dim _Product As ProductCategory = New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                                If Not IsNothing(_Product) Then
                                    _DepositBDebitNote.ProductCategory = _Product
                                Else
                                    errorMessage.Append("Invalid Product Category" & Chr(13) & Chr(10))
                                End If
                            Catch ex As Exception
                                errorMessage.Append("Invalid Product Category" & Chr(13) & Chr(10))
                            End Try
                        Else
                            errorMessage.Append("Product Category is Empty" & Chr(13) & Chr(10))
                        End If
                        
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If errorMessage.Length > 0 Then
                _DepositBDebitNote = Nothing '
                Throw New Exception(errorMessage.ToString)
            End If
            Return _DepositBDebitNote
        End Function

#End Region

    End Class

End Namespace