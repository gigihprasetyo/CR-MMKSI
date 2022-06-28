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
#End Region

Namespace KTB.DNet.Parser
    Public Class DepositAParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private DepositAS As ArrayList
        Private DepositADetails As ArrayList
        Private _fileName As String
        Private _DepositA As DepositA
        Private _DepositADetail As DepositADetail
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
                DepositAS = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If val.Substring(0, 1).ToUpper.Equals("H") Then
                            If Not _DepositA Is Nothing Then
                                DepositAS.Add(_DepositA)
                            End If
                            _DepositA = ParseDepositA(val + delimited)
                        Else
                            If Not _DepositA Is Nothing Then
                                ParseDepositADetail(val + delimited, _DepositA)
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositAParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DepositAParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DepositA = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While
                If Not _DepositA Is Nothing Then
                    DepositAS.Add(_DepositA)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return DepositAS
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim _objDepositA As DepositA
            For Each item As DepositA In DepositAS
                Try
                    Dim i As Integer = New DepositAFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(item)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositAParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DepositAParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _objDepositA.Dealer.DealerCode & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseDepositA(ByVal ValParser As String) As DepositA
            _DepositA = New DepositA
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
                                _DepositA.Dealer = objDealer
                            Else
                                errorMessage.Append("Dealer tidak valid" & Chr(13) & Chr(10))
                            End If
                        Else
                            errorMessage.Append("Dealer tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If sTemp.Length = 8 Then
                            Try
                                Dim objDate As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2), 0, 0, 0)
                                _DepositA.TransactionDate = objDate
                            Catch ex As Exception
                                errorMessage.Append("Transaction Date tidak valid" & Chr(13) & Chr(10))
                            End Try
                        Else
                            errorMessage.Append("Transaction Date tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If IsNumeric(sTemp) Then
                            _DepositA.BeginingBalance = CDec(sTemp)
                        Else
                            errorMessage.Append("Saldo Awal tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        If IsNumeric(sTemp) Then
                            _DepositA.DebetAmount = CDec(sTemp)
                        Else
                            errorMessage.Append("Debet tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 5
                        If IsNumeric(sTemp) Then
                            _DepositA.CreditAmount = CDec(sTemp)
                        Else
                            errorMessage.Append("Credit tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 6
                        If IsNumeric(sTemp) Then
                            _DepositA.EndBalance = CDec(sTemp)
                        Else
                            errorMessage.Append("Saldo Akhir tidak valid" & Chr(13) & Chr(10))
                        End If

                    Case Is = 7
                        If sTemp.Trim.Length > 0 Then
                            Dim objProductCategory As ProductCategory = RetrieveProductCategory(sTemp.Trim)

                            If Not IsNothing(ObjPRoductCategory) AndAlso ObjPRoductCategory.ID > 0 Then
                                _DepositA.ProductCategory = ObjPRoductCategory
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
            Return _DepositA
        End Function

        Private Sub ParseDepositADetail(ByVal ValParser As String, ByVal _objDepositA As DepositA)
            _DepositADetail = New DepositADetail
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
                                Dim objDate As Date = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2), 0, 0, 0)
                                _DepositADetail.TransactionDate = objDate
                            Catch ex As Exception
                                errorMessage.Append("Transaction Date tidak valid" & Chr(13) & Chr(10))
                            End Try
                        Else
                            errorMessage.Append("Transaction Date tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        Try
                            _DepositADetail.Tipe = sTemp.Trim
                            If sTemp.ToUpper = "INVOICE" Or sTemp.ToUpper = "TRANSFER" Then
                                _DepositADetail.StatusDebet = 0
                            Else
                                _DepositADetail.StatusDebet = 1
                            End If
                        Catch ex As Exception
                            errorMessage.Append("Invalid Line Item" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 3
                        If IsNumeric(sTemp) Then
                            If CDec(sTemp) < 0 Then
                                If _DepositADetail.StatusDebet = 0 Then
                                    _DepositADetail.StatusDebet = 1
                                Else
                                    _DepositADetail.StatusDebet = 0
                                End If
                            End If
                            _DepositADetail.Amount = System.Math.Abs(CDec(sTemp))
                        Else
                            errorMessage.Append("Amount tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        _DepositADetail.Reff = sTemp.Trim
                    Case Is = 5
                        _DepositADetail.Description = sTemp.Trim
                    Case Is = 6
                        _DepositADetail.DocumentNumber = sTemp.Trim
                    Case Else
                        'Do Nothing else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            Else
                _objDepositA.DepositADetails.Add(_DepositADetail)
            End If
        End Sub

        Private Function RetrieveProductCategory(ByVal code As String) As ProductCategory
            Dim _ProductCategoryFacade As ProductCategoryFacade = New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return _ProductCategoryFacade.Retrieve(code.Trim())
        End Function
#End Region

    End Class

End Namespace