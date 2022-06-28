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
Imports KTB.DNet.BusinessFacade.Service
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.Parser
    Public Class DepositCInterestParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _fileName As String
        Private _DepositC2InterestHeader As DepositC2InterestHeader
        Private _DepositC2InterestDetail As DepositC2InterestDetail
        Private errorMessage As StringBuilder
        Private _DCI As DCI
        Private _DCIs As ArrayList
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
                _DCIs = New ArrayList
                _DepositC2InterestHeader = Nothing
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If val.Substring(0, 1).ToUpper.Equals("H") Then
                            If Not IsNothing(_DCI) Then
                                If Not IsNothing(errorMessage) Then _DCI.ErrorMessage = errorMessage
                                _DCIs.Add(_DCI)  'customer header input text
                                _DCI = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _DCI = New DCI()
                            _DCI.DepositC2InterestHeader = ParseDepositC2InterestHeader(val + Delimited)
                        Else
                            _DepositC2InterestDetail = ParseDepositC2InterestDetail(val + Delimited, _DCI.DepositC2InterestHeader.ID)
                            _DCI.DepositC2InterestDetail.Add(_DepositC2InterestDetail)
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositC2Interest.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DepositC2InterestParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DCI = Nothing
                    End Try

                    val = MyBase.NextLine(stream)

                End While

                If Not IsNothing(_DepositC2InterestDetail) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _DCI.ErrorMessage = errorMessage
                    _DCIs.Add(_DCI)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try

            Return _DCIs
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oDCHFac As New DepositC2InterestHeaderFacade(user)
            Dim oDCDFac As New DepositC2InterestDetailFacade(user)
            For i As Integer = 0 To _DCIs.Count - 1
                _DCI = _DCIs(i)
                Dim arlDetail As New ArrayList
                If (Not IsNothing(_DCI.ErrorMessage) AndAlso _DCI.ErrorMessage.ToString() <> String.Empty) Then
                    Throw New Exception(_DCI.ErrorMessage.ToString())
                Else
                    Dim _objDepositC2InterestHeader As DepositC2InterestHeader
                    Dim _objDepositC2InterestDetail As DepositC2InterestDetail
                    If Not IsNothing(_DCI.DepositC2InterestHeader) AndAlso _DCI.DepositC2InterestHeader.ErrorMessage = "" Then
                        Try
                            _objDepositC2InterestHeader = IsHeaderExist(_DCI.DepositC2InterestHeader)
                            For Each _detail As DepositC2InterestDetail In _DCI.DepositC2InterestDetail
                                arlDetail.Add(_detail)
                            Next
                            If Not IsNothing(_objDepositC2InterestHeader) AndAlso (_objDepositC2InterestHeader.ID > 0) Then
                                _objDepositC2InterestHeader.InterestAmount = _DCI.DepositC2InterestHeader.InterestAmount
                                _objDepositC2InterestHeader.NettoAmount = _DCI.DepositC2InterestHeader.NettoAmount
                                _objDepositC2InterestHeader.TaxAmount = _DCI.DepositC2InterestHeader.TaxAmount
                                Dim j As Int16 = New DepositC2InterestHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).UpdateTransaction(_objDepositC2InterestHeader, arlDetail)
                            Else
                                Dim j As Int16 = New DepositC2InterestHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).InsertTransaction(_DCI.DepositC2InterestHeader, arlDetail)
                            End If
                        Catch ex As Exception
                            SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositC2Interest.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ContractParser)
                            Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _objDepositC2InterestHeader.Dealer.DealerCode & " - Period :" & _objDepositC2InterestHeader.Periode & Chr(13) & Chr(10) & ex.Message)
                            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        End Try
                    End If
                End If
            Next

            'For Each item As DepositC2InterestHeader In DepositC2InterestHeaders
            '    Try
            '        _objDepositC2InterestHeader = IsHeaderExist(item)
            '        If Not IsNothing(_objDepositC2InterestHeader) AndAlso (_objDepositC2InterestHeader.ID > 0) Then
            '            _objDepositC2InterestHeader.InterestAmount = item.InterestAmount
            '            _objDepositC2InterestHeader.NettoAmount = item.NettoAmount
            '            _objDepositC2InterestHeader.TaxAmount = item.TaxAmount

            '            'Set Detailnya rowstatus = -1 dulu
            '            'Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositC2InterestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '            'criterias.opAnd(New Criteria(GetType(DepositC2InterestDetail), "DepositC2InterestHeader.ID", MatchType.Exact, _objDepositC2InterestHeader.ID))
            '            'Dim objDepositC2InterestDetailList As ArrayList = New DepositC2InterestDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            '            'If objDepositC2InterestDetailList.Count > 0 Then
            '            '    Dim iDetailReturn As Integer
            '            '    For Each itemDetail As DepositC2InterestDetail In objDepositC2InterestDetailList
            '            '        itemDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
            '            '        iDetailReturn = New DepositC2InterestDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(itemDetail)
            '            '    Next
            '            'End If

            '            For Each _detail As DepositC2InterestDetail In _objDepositC2InterestHeader.DepositC2InterestDetails
            '                _detail.DepositC2InterestHeader = _objDepositC2InterestHeader
            '                _objDepositC2InterestDetail = _detail
            '            Next

            '            Dim i As Int16 = New DepositC2InterestHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).UpdateTransaction(_objDepositC2InterestHeader, _objDepositC2InterestDetail)
            '        Else
            '            Dim i As Int16 = New DepositC2InterestHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).InsertTransaction(item)

            '        End If
            '    Catch ex As Exception
            '        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositC2Interest.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ContractParser)
            '        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _objDepositC2InterestHeader.Dealer.DealerCode & " - Period :" & _objDepositC2InterestHeader.Periode & Chr(13) & Chr(10) & ex.Message)
            '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            '    End Try
            'Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return -1
        End Function


        Private Function IsHeaderExist(ByVal objDepositC2InterestHeader As DepositC2InterestHeader) As DepositC2InterestHeader

            Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositC2InterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositC2InterestHeader), "Dealer.ID", MatchType.Exact, objDepositC2InterestHeader.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(DepositC2InterestHeader), "ProductCategory.ID", MatchType.Exact, objDepositC2InterestHeader.ProductCategory.ID))
            criterias.opAnd(New Criteria(GetType(DepositC2InterestHeader), "Year", MatchType.Exact, objDepositC2InterestHeader.Year))
            criterias.opAnd(New Criteria(GetType(DepositC2InterestHeader), "Periode", MatchType.Exact, objDepositC2InterestHeader.Periode))


            Dim objDepositC2InterestHeaderList As ArrayList = New DepositC2InterestHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If objDepositC2InterestHeaderList.Count > 0 Then
                Return objDepositC2InterestHeaderList.Item(0)
            Else
                Return Nothing
            End If
        End Function

        Private Function IsDetailExist(ByVal objDepositC2InterestDetail As DepositC2InterestDetail) As DepositC2InterestDetail
            Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositC2InterestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositC2InterestDetail), "DepositC2InterestHeader.ID", MatchType.Exact, objDepositC2InterestDetail.DepositC2InterestHeader.ID))
            criterias.opAnd(New Criteria(GetType(DepositC2InterestDetail), "Year", MatchType.Exact, objDepositC2InterestDetail.Year))
            criterias.opAnd(New Criteria(GetType(DepositC2InterestDetail), "Month", MatchType.Exact, objDepositC2InterestDetail.Month))


            Dim objDepositC2InterestDetailList As ArrayList = New DepositC2InterestDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If objDepositC2InterestDetailList.Count > 0 Then
                Return objDepositC2InterestDetailList.Item(0)
            Else
                Return Nothing
            End If
        End Function
#End Region

#Region "Private Methods"

        Private Function ParseDepositC2InterestHeader(ByVal ValParser As String) As DepositC2InterestHeader
            _DepositC2InterestHeader = New DepositC2InterestHeader
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
                                _DepositC2InterestHeader.Dealer = _dealer
                            Else
                                errorMessage.Append("Invalid Kode Dealer" & Chr(13) & Chr(10))
                            End If
                        Else
                            errorMessage.Append("Invalid Kode Dealer" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        Try
                            _DepositC2InterestHeader.Year = Short.Parse(sTemp.Trim)
                        Catch ex As Exception
                            errorMessage.Append("Invalid Tahun" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 3
                        Try
                            _DepositC2InterestHeader.Periode = Short.Parse(sTemp.Trim)
                        Catch ex As Exception
                            errorMessage.Append("Invalid Period" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 4
                        Try
                            _DepositC2InterestHeader.InterestAmount = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
                        Catch ex As Exception
                            errorMessage.Append("Invalid Interest Amount" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 5
                        Try
                            _DepositC2InterestHeader.TaxAmount = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
                        Catch ex As Exception
                            errorMessage.Append("Invalid Tax Amount" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 6
                        Try
                            _DepositC2InterestHeader.NettoAmount = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
                        Catch ex As Exception
                            errorMessage.Append("Invalid Netto Amount" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 7
                        Try
                            Dim _Product As ProductCategory = New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If Not IsNothing(_Product) Then
                                _DepositC2InterestHeader.ProductCategory = _Product
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
            Return _DepositC2InterestHeader
        End Function

        Private Function ParseDepositC2InterestDetail(ByVal ValParser As String, ByVal _objDepositC2InterestHeaderID As Integer) As DepositC2InterestDetail
            Dim _ret As DepositC2InterestDetail = New DepositC2InterestDetail

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
                    Case Is = 2
                        Try
                            _ret.Year = Short.Parse(sTemp.Trim)
                        Catch ex As Exception
                            errorMessage.Append("Invalid Year" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 3
                        Try
                            _ret.Month = sTemp.Trim
                        Catch ex As Exception
                            errorMessage.Append("Invalid Month." & Chr(13) & Chr(10))
                        End Try
                    Case Is = 4
                        Try
                            _ret.InterestAmount = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
                        Catch ex As Exception
                            errorMessage.Append("Invalid Interest Amount." & Chr(13) & Chr(10))
                        End Try
                    Case Is = 5
                        Try
                            _ret.NettoAmount = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
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
            Return _ret
        End Function

        Private Class DCI

            Private _DepositC2InterestHeader As DepositC2InterestHeader
            Private _DepositC2InterestDetail As DepositC2InterestDetail
            Private _list As List(Of DepositC2InterestDetail)
            Private _errorMessage As StringBuilder

            Public Sub New()
                _list = New List(Of DepositC2InterestDetail)
            End Sub



            Public Property DepositC2InterestHeader() As DepositC2InterestHeader
                Get
                    Return _DepositC2InterestHeader
                End Get
                Set(value As DepositC2InterestHeader)
                    _DepositC2InterestHeader = value
                End Set
            End Property

            Public Property DepositC2InterestDetail() As List(Of DepositC2InterestDetail)
                Get
                    Return _list
                End Get
                Set(value As List(Of DepositC2InterestDetail))
                    _list = value
                End Set
            End Property


            Public Property ErrorMessage() As StringBuilder
                Get
                    Return _errorMessage
                End Get
                Set(value As StringBuilder)
                    _errorMessage = value
                End Set
            End Property
        End Class
#End Region

    End Class

End Namespace