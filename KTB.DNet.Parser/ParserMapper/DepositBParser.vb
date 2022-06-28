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
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade

#End Region

Namespace KTB.DNet.Parser
    Public Class DepositBParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private DepositBHeaders As ArrayList
        Private DepositBDetails As ArrayList
        Private _fileName As String
        Private _DepositBHeader As DepositBHeader
        Private _DepositBDetail As DepositBDetail
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
                DepositBHeaders = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If val.Substring(0, 1).ToUpper.Equals("H") Then
                            If Not _DepositBHeader Is Nothing Then
                                DepositBHeaders.Add(_DepositBHeader)
                            End If
                            _DepositBHeader = ParseDepositBHeader(val + Delimited)
                        Else
                            If Not _DepositBHeader Is Nothing Then
                                ParseDepositBDetail(val + Delimited, _DepositBHeader)
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DepositBParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DepositBHeader = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While
                If Not _DepositBHeader Is Nothing Then
                    DepositBHeaders.Add(_DepositBHeader)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return DepositBHeaders
        End Function

        Protected Overrides Function DoTransaction() As Integer

            For Each item As DepositBHeader In DepositBHeaders
                Try
                    'Cek is exist
                    Dim _objDepositBHeader As DepositBHeader

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "Dealer.DealerCode", MatchType.Exact, item.Dealer.DealerCode))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "ProductCategory.Code", MatchType.Exact, item.ProductCategory.Code))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.Exact, item.TransactionDate))

                    Dim _DepositBHeaderFacade As DepositBHeaderFacade = New DepositBHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    Dim arlDepositBHeaders As ArrayList = _DepositBHeaderFacade.Retrieve(criterias)

                    If arlDepositBHeaders.Count > 0 Then
                        _objDepositBHeader = CType(arlDepositBHeaders(0), DepositBHeader)

                        If (Not IsNothing(_objDepositBHeader) AndAlso _objDepositBHeader.ID > 0) Then
                            'Dim criteriasDet As New CriteriaComposite(New Criteria(GetType(DepositBDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            'criteriasDet.opAnd(New Criteria(GetType(DepositBDetail), "DepositBHeader.ID", MatchType.Exact, _objDepositBHeader.ID))

                            'Dim objDepositBDetailList As ArrayList = New DepositBDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasDet)
                            'If objDepositBDetailList.Count > 0 Then
                            '    Dim iDetailReturn As Integer
                            '    For Each itemDetail As DepositBDetail In objDepositBDetailList
                            '        itemDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                            '        iDetailReturn = New DepositBDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(itemDetail)
                            '    Next
                            'End If

                            Dim isChange As New isChangeFacade

                            If isChange.ISchangeDepositB(item, _objDepositBHeader) Then

                                _objDepositBHeader.BeginingBalance = item.BeginingBalance
                                _objDepositBHeader.EndBalance = item.EndBalance
                                _objDepositBHeader.DebetAmount = item.DebetAmount
                                _objDepositBHeader.CreditAmount = item.CreditAmount

                            End If

                            For Each _detailDB As DepositBDetail In _objDepositBHeader.DepositBDetails
                                Dim isExist As Boolean = False
                                For Each _detail As DepositBDetail In item.DepositBDetails
                                    If _detail.DocumentNumber = _detailDB.DocumentNumber Then
                                        isExist = True

                                        If isChange.ISchangeDepositBDetail(_detail, _detailDB) Then

                                            _detailDB.Amount = _detail.Amount
                                            _detailDB.Description = _detail.Description
                                            _detailDB.Reff = _detail.Reff
                                            _detailDB.Tipe = _detail.Tipe
                                            _detailDB.StatusDebet = _detail.StatusDebet
                                            _detailDB.TransactionDate = _detail.TransactionDate
                                            Exit For
                                        End If
                                    End If
                                Next
                                If isExist = False Then
                                    _detailDB.RowStatus = CType(DBRowStatus.Deleted, Short)
                                End If
                            Next

                            For Each _detail As DepositBDetail In item.DepositBDetails
                                Dim isExist As Boolean = False
                                For Each _detailDB As DepositBDetail In _objDepositBHeader.DepositBDetails
                                    If _detail.DocumentNumber = _detailDB.DocumentNumber Then
                                        isExist = True
                                        Exit For
                                    End If
                                Next
                                If isExist = False Then
                                    _detail.DepositBHeader = _objDepositBHeader
                                    _objDepositBHeader.DepositBDetails.Add(_detail)
                                End If
                            Next

                            
                        End If
                        Dim i As Integer = New DepositBHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).UpdateTransaction(_objDepositBHeader)
                    Else
                        Dim i As Integer = New DepositBHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).InsertTransaction(item)
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DepositBParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.Dealer.DealerCode & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function


        'Private Function IsInsert(ByVal item As DepositBHeader) As DepositBHeader

        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "Dealer.DealerCode", MatchType.Exact, item.Dealer.DealerCode))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "ProductCategory.Code", MatchType.Exact, item.ProductCategory.Code))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.Exact, item.TransactionDate))

        '    Dim _DepositBHeaderFacade As DepositBHeaderFacade = New DepositBHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
        '    Dim arlDepositBHeaders As ArrayList = _DepositBHeaderFacade.Retrieve(criterias)

        '    If arlDepositBHeaders.Count > 0 Then
        '        Return arlDepositBHeaders.Item(0)
        '    Else
        '        Return Nothing
        '    End If
        'End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return True
        End Function

#End Region

#Region "Private Methods"

        Private Function ParseDepositBHeader(ByVal ValParser As String) As DepositBHeader
            _DepositBHeader = New DepositBHeader
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            Dim _Category As New Category
            errorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        If sTemp.Trim.Length > 0 Then
                            Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If objDealer.ID > 0 Then
                                _DepositBHeader.Dealer = objDealer
                            Else
                                errorMessage.Append("Dealer tidak valid" & Chr(13) & Chr(10))
                            End If
                        Else
                            errorMessage.Append("Dealer tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If sTemp.Length = 6 Then
                            Try
                                Dim objDate As Date = New Date(sTemp.Substring(0, 4), sTemp.Substring(4, 2), "01", 0, 0, 0)
                                _DepositBHeader.TransactionDate = objDate
                            Catch ex As Exception
                                errorMessage.Append("Transaction Date tidak valid" & Chr(13) & Chr(10))
                            End Try
                        Else
                            errorMessage.Append("Transaction Date tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If IsNumeric(sTemp) Then
                            _DepositBHeader.BeginingBalance = CDec(sTemp)
                        Else
                            errorMessage.Append("Saldo Awal tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        If IsNumeric(sTemp) Then
                            _DepositBHeader.DebetAmount = CDec(sTemp)
                        Else
                            errorMessage.Append("Debet tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 5
                        If IsNumeric(sTemp) Then
                            _DepositBHeader.CreditAmount = CDec(sTemp)
                        Else
                            errorMessage.Append("Credit tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 6
                        If IsNumeric(sTemp) Then
                            _DepositBHeader.EndBalance = CDec(sTemp)
                        Else
                            errorMessage.Append("Saldo Akhir tidak valid" & Chr(13) & Chr(10))
                        End If

                    Case Is = 7
                        If sTemp.Trim.Length > 0 Then
                            Dim objProductCategory As ProductCategory = RetrieveProductCategory(sTemp.Trim)

                            If Not IsNothing(objProductCategory) AndAlso objProductCategory.ID > 0 Then
                                _DepositBHeader.ProductCategory = objProductCategory
                            Else
                                errorMessage.Append("Produk tidak valid" & Chr(13) & Chr(10))
                            End If
                        Else
                            errorMessage.Append("Produk tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Else
                        'do nothing
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If
            Return _DepositBHeader
        End Function

        Private Sub ParseDepositBDetail(ByVal ValParser As String, ByVal _objDepositB As DepositBHeader)
            _DepositBDetail = New DepositBDetail
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            'Dim _pkDetail As New PKDetail
            errorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        If sTemp.Length = 8 Then
                            Try
                                Dim objDate As Date = New Date(sTemp.Substring(0, 4), sTemp.Substring(4, 2), sTemp.Substring(6, 2), 0, 0, 0)
                                _DepositBDetail.TransactionDate = objDate
                            Catch ex As Exception
                                errorMessage.Append("Transaction Date tidak valid" & Chr(13) & Chr(10))
                            End Try
                        Else
                            errorMessage.Append("Transaction Date tidak valid" & Chr(13) & Chr(10))
                        End If
                        'Case Is = 2
                        '    Try
                        '        _DepositBDetail.Tipe = sTemp.Trim
                        '        If sTemp.ToUpper = "INVOICE" Or sTemp.ToUpper = "TRANSFER" Then
                        '            _DepositBDetail.StatusDebet = 0
                        '        Else
                        '            _DepositBDetail.StatusDebet = 1
                        '        End If
                        '    Catch ex As Exception
                        '        errorMessage.Append("Invalid Line Item" & Chr(13) & Chr(10))
                        '    End Try
                        'Case Is = 3
                        '    If IsNumeric(sTemp) Then
                        '        If CDec(sTemp) < 0 Then
                        '            If _DepositBDetail.StatusDebet = 0 Then
                        '                _DepositBDetail.StatusDebet = 1
                        '            Else
                        '                _DepositBDetail.StatusDebet = 0
                        '            End If
                        '        End If
                        '        _DepositBDetail.Amount = System.Math.Abs(CDec(sTemp))
                        '    Else
                        '        errorMessage.Append("Amount tidak valid" & Chr(13) & Chr(10))
                        '    End If
                    Case Is = 3
                        _DepositBDetail.Reff = sTemp.Trim
                    Case Is = 4
                        _DepositBDetail.Tipe = sTemp.Trim
                    Case Is = 5
                        _DepositBDetail.DocumentNumber = sTemp.Trim
                    Case Is = 6
                        If IsNumeric(sTemp) Then
                            _DepositBDetail.Amount = CDec(sTemp)
                        Else
                            errorMessage.Append("Amount tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 7
                        _DepositBDetail.Description = sTemp.Trim
                    Case Is = 8
                        If IsNumeric(sTemp) Then
                            _DepositBDetail.StatusDebet = System.Math.Abs(CDec(sTemp))
                        Else
                            errorMessage.Append("Status D/K tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Else
                        'Do Nothing else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            Else
                _objDepositB.DepositBDetails.Add(_DepositBDetail)
            End If
        End Sub

        Private Function RetrieveProductCategory(ByVal code As String) As ProductCategory
            Dim _ProductCategoryFacade As ProductCategoryFacade = New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return _ProductCategoryFacade.Retrieve(code.Trim())
        End Function
#End Region

    End Class

End Namespace