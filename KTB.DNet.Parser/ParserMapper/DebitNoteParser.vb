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

    Public Class DebitNoteParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private DebitNotes As ArrayList
        Private _fileName As String
        Private _DebitNote As DebitNote
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
                DebitNotes = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                While (Not val = "")
                    Try
                        If Not _DebitNote Is Nothing Then
                            DebitNotes.Add(_DebitNote)
                        End If
                        _DebitNote = ParseDebitNote(val + delimited)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DebitNoteParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DebitNoteParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DebitNote = Nothing
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While
                If Not _DebitNote Is Nothing Then
                    DebitNotes.Add(_DebitNote)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                _Stream.Close()
                _Stream = Nothing
            End Try
            Return DebitNotes
        End Function

        Protected Overrides Function DoTransaction() As Integer
            For Each item As DebitNote In DebitNotes
                Try
                    Dim i As Int16 = New DebitNoteFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(item)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DebitNoteParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DebitNoteParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.Dealer.ID & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")

                End Try
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseDebitNote(ByVal ValParser As String) As DebitNote
            _DebitNote = New DebitNote
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
                                _DebitNote.Dealer = objDealer
                            Else
                                Throw New Exception("Dealer Tidak Ketemu")
                            End If
                        Else
                            errorMessage.Append("Invalid Dealer Code" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        If sTemp.Length > 0 Then
                            _DebitNote.DNNumber = sTemp.Trim
                        Else
                            'errorMessage.Append("Invalid Debit Note Number" & Chr(13) & Chr(10))
                            'dibolehkan kosong karena hubungan OR dengan assignment
                            _DebitNote.DNNumber = String.Empty
                        End If
                    Case Is = 2
                        If IsNumeric(sTemp.Trim) Then
                            _DebitNote.Amount = CDec(sTemp.Trim)
                        Else
                            errorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If sTemp.Length > 0 Then
                            _DebitNote.Assignment = sTemp.Trim
                        Else
                            'errorMessage.Append("Invalid Assignment / SO ID" & Chr(13) & Chr(10))
                            _DebitNote.Assignment = String.Empty
                        End If
                    Case Is = 4
                        If sTemp.Length > 0 Then
                            _DebitNote.Description = sTemp.Trim
                        Else
                            'errorMessage.Append("Invalid Description" & Chr(13) & Chr(10))
                            _DebitNote.Description = String.Empty
                        End If
                    Case Is = 5
                        If sTemp.Length = 8 Then
                            Try
                                Dim objDate As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2), 0, 0, 0)
                                _DebitNote.PostingDate = objDate
                            Catch ex As Exception
                                errorMessage.Append("Invalid Posting format Date" & Chr(13) & Chr(10))
                            End Try
                        Else
                            errorMessage.Append("Invalid Posting Date length" & Chr(13) & Chr(10))
                        End If
                    Case Is = 6
                        If sTemp.Trim.Length > 0 Then
                            'Mapping dari DealerCode menjadi DealerID
                            Dim objProductCategory As ProductCategory = RetrieveProductCategory(sTemp.Trim)
                            If objProductCategory.ID > 0 Then
                                _DebitNote.ProductCategory = objProductCategory
                            Else
                                Throw New Exception("Product Tidak Ketemu")
                            End If
                        Else
                            errorMessage.Append("Invalid Product Code" & Chr(13) & Chr(10))
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
            Return _DebitNote
        End Function

        Private Function RetrieveDealer(ByVal code As String) As Dealer
            Dim _dealerFacade As DealerFacade = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return _dealerFacade.Retrieve(code)
        End Function

        Private Function RetrieveProductCategory(ByVal code As String) As ProductCategory
            Dim _ProductCategoryFacade As ProductCategoryFacade = New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return _ProductCategoryFacade.Retrieve(code.Trim())
        End Function
#End Region

    End Class
End Namespace
