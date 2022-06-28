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
Imports KTB.DNet.BusinessFacade
Imports KTB.DNET.BusinessFacade.Service
#End Region

Namespace KTB.DNet.Parser
    Public Class DepositBInterestParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private DepositBInterestHeaders As ArrayList
        Private DepositBInterestDetails As ArrayList
        Private _fileName As String
        Private _DepositBInterestHeader As DepositBInterestHeader
        Private _DepositBInterestDetail As DepositBInterestDetail
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
                DepositBInterestHeaders = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If val.Substring(0, 1).ToUpper.Equals("H") Then
                            If Not _DepositBInterestHeader Is Nothing Then
                                DepositBInterestHeaders.Add(_DepositBInterestHeader)  'customer header input text
                            End If
                            _DepositBInterestHeader = ParseDepositBInterestHeader(val + Delimited)
                        Else
                            If Not _DepositBInterestHeader Is Nothing Then
                                ParseDepositBInterestDetail(val + Delimited, _DepositBInterestHeader)  'Order detail input
                            End If
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBInterest.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DepositBInterestParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DepositBInterestHeader = Nothing
                    End Try

                    val = MyBase.NextLine(stream)

                End While

                If Not _DepositBInterestHeader Is Nothing Then
                    DepositBInterestHeaders.Add(_DepositBInterestHeader)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return DepositBInterestHeaders

        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim _objDepositBInterestHeader As DepositBInterestHeader

            For Each item As DepositBInterestHeader In DepositBInterestHeaders
                Try
                    _objDepositBInterestHeader = IsHeaderExist(item)
                    If Not IsNothing(_objDepositBInterestHeader) AndAlso (_objDepositBInterestHeader.ID > 0) Then
                        _objDepositBInterestHeader.InterestAmount = item.InterestAmount
                        _objDepositBInterestHeader.NettoAmount = item.NettoAmount
                        _objDepositBInterestHeader.TaxAmount = item.TaxAmount

                        'Set Detailnya rowstatus = -1 dulu
                        'Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositBInterestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(DepositBInterestDetail), "DepositBInterestHeader.ID", MatchType.Exact, _objDepositBInterestHeader.ID))
                        'Dim objDepositBInterestDetailList As ArrayList = New DepositBInterestDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        'If objDepositBInterestDetailList.Count > 0 Then
                        '    Dim iDetailReturn As Integer
                        '    For Each itemDetail As DepositBInterestDetail In objDepositBInterestDetailList
                        '        itemDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                        '        iDetailReturn = New DepositBInterestDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(itemDetail)
                        '    Next
                        'End If

                        For Each _detail As DepositBInterestDetail In item.DepositBInterestDetails
                            _detail.DepositBInterestHeader = _objDepositBInterestHeader
                            _objDepositBInterestHeader.DepositBInterestDetails.Add(_detail)
                        Next

                        Dim i As Int16 = New DepositBInterestHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).UpdateTransaction(_objDepositBInterestHeader)
                    Else
                        Dim i As Int16 = New DepositBInterestHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).InsertTransaction(item)

                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBInterest.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ContractParser)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _objDepositBInterestHeader.Dealer.DealerCode & " - Period :" & _objDepositBInterestHeader.Periode & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return -1
        End Function


        Private Function IsHeaderExist(ByVal objDepositBInterestHeader As DepositBInterestHeader) As DepositBInterestHeader

            Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositBInterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositBInterestHeader), "Dealer.ID", MatchType.Exact, objDepositBInterestHeader.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(DepositBInterestHeader), "ProductCategory.ID", MatchType.Exact, objDepositBInterestHeader.ProductCategory.ID))
            criterias.opAnd(New Criteria(GetType(DepositBInterestHeader), "Year", MatchType.Exact, objDepositBInterestHeader.Year))
            criterias.opAnd(New Criteria(GetType(DepositBInterestHeader), "Periode", MatchType.Exact, objDepositBInterestHeader.Periode))


            Dim objDepositBInterestHeaderList As ArrayList = New DepositBInterestHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If objDepositBInterestHeaderList.Count > 0 Then
                Return objDepositBInterestHeaderList.Item(0)
            Else
                Return Nothing
            End If
        End Function

        Private Function IsDetailExist(ByVal objDepositBInterestDetail As DepositBInterestDetail) As DepositBInterestDetail
            Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositBInterestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositBInterestDetail), "DepositBInterestHeader.ID", MatchType.Exact, objDepositBInterestDetail.DepositBInterestHeader.ID))
            criterias.opAnd(New Criteria(GetType(DepositBInterestDetail), "Year", MatchType.Exact, objDepositBInterestDetail.Year))
            criterias.opAnd(New Criteria(GetType(DepositBInterestDetail), "Month", MatchType.Exact, objDepositBInterestDetail.Month))


            Dim objDepositBInterestDetailList As ArrayList = New DepositBInterestDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If objDepositBInterestDetailList.Count > 0 Then
                Return objDepositBInterestDetailList.Item(0)
            Else
                Return Nothing
            End If
        End Function
#End Region

#Region "Private Methods"

        Private Function ParseDepositBInterestHeader(ByVal ValParser As String) As DepositBInterestHeader
            _DepositBInterestHeader = New DepositBInterestHeader
            errorMessage = New StringBuilder
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String

            sStart = 0
            nCount = 0

            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)

                    Case Is = 1
                        If sTemp.Trim.Length > 0 Then
                            Dim _dealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If Not IsNothing(_dealer) Then
                                _DepositBInterestHeader.Dealer = _dealer
                            Else
                                errorMessage.Append("Invalid Kode Dealer" & Chr(13) & Chr(10))
                            End If
                        Else
                            errorMessage.Append("Invalid Kode Dealer" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        Try
                            _DepositBInterestHeader.Year = Short.Parse(sTemp.Trim)
                        Catch ex As Exception
                            errorMessage.Append("Invalid Tahun" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 3
                        Try
                            _DepositBInterestHeader.Periode = Short.Parse(sTemp.Trim)
                        Catch ex As Exception
                            errorMessage.Append("Invalid Period" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 4
                        Try
                            _DepositBInterestHeader.InterestAmount = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
                        Catch ex As Exception
                            errorMessage.Append("Invalid Interest Amount" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 5
                        Try
                            _DepositBInterestHeader.TaxAmount = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
                        Catch ex As Exception
                            errorMessage.Append("Invalid Tax Amount" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 6
                        Try
                            _DepositBInterestHeader.NettoAmount = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
                        Catch ex As Exception
                            errorMessage.Append("Invalid Netto Amount" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 7
                        Try
                            Dim _Product As ProductCategory = New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If Not IsNothing(_Product) Then
                                _DepositBInterestHeader.ProductCategory = _Product
                            Else
                                errorMessage.Append("Invalid Kategori Produk" & Chr(13) & Chr(10))
                            End If
                        Catch ex As Exception
                            errorMessage.Append("Invalid Kategori Produk" & Chr(13) & Chr(10))
                        End Try
                    Case Else
                        'do nothing
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If

            Return _DepositBInterestHeader
        End Function

        Private Sub ParseDepositBInterestDetail(ByVal ValParser As String, ByVal _objDepositBInterestHeader As DepositBInterestHeader)
            _DepositBInterestDetail = New DepositBInterestDetail
            errorMessage = New StringBuilder

            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            'D;KodeDealer;Year;Month;InterestAmount;Netto
            sStart = 0
            nCount = 0

            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        'Try
                        '    Dim _dealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                        '    If Not IsNothing(_dealer) Then
                        '        _DepositBInterestDetail.Dealer = _dealer
                        '    Else
                        '        errorMessage.Append("Invalid Kode Dealer" & Chr(13) & Chr(10))
                        '    End If
                        'Catch ex As Exception
                        '    errorMessage.Append("Invalid Kode Dealer" & Chr(13) & Chr(10))
                        'End Try
                    Case Is = 2
                        Try
                            _DepositBInterestDetail.Year = Short.Parse(sTemp.Trim)
                        Catch ex As Exception
                            errorMessage.Append("Invalid Year" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 3
                        Try
                            _DepositBInterestDetail.Month = sTemp.Trim
                        Catch ex As Exception
                            errorMessage.Append("Invalid Month." & Chr(13) & Chr(10))
                        End Try
                    Case Is = 4
                        Try
                            _DepositBInterestDetail.InterestAmount = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
                        Catch ex As Exception
                            errorMessage.Append("Invalid Interest Amount." & Chr(13) & Chr(10))
                        End Try
                    Case Is = 5
                        Try
                            _DepositBInterestDetail.NettoAmount = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
                        Catch ex As Exception
                            errorMessage.Append("Invalid Netto Amount." & Chr(13) & Chr(10))
                        End Try
                    Case Else
                        'Do Nothing else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If

            _objDepositBInterestHeader.DepositBInterestDetails.Add(_DepositBInterestDetail)

        End Sub


#End Region

    End Class

End Namespace