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

    Public Class DepositAInterestParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private DepositAInterestHs As ArrayList
        Private DepositAInterestDs As ArrayList
        Private _fileName As String
        Private _DepositAInterestH As DepositAInterestH
        Private _DepositAInterestD As DepositAInterestD
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
                DepositAInterestHs = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                While (Not val = "")
                    Try
                        If val.Substring(0, 1).ToUpper.Equals("H") Then
                            If Not _DepositAInterestH Is Nothing Then
                                DepositAInterestHs.Add(_DepositAInterestH)
                            End If
                            _DepositAInterestH = ParseDepositAInterestH(val + delimited)
                        Else
                            If Not _DepositAInterestH Is Nothing Then
                                ParseDepositAInterestD(val + delimited, _DepositAInterestH)
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositAInterestParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DepositAInterestParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DepositAInterestH = Nothing
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While
                If Not _DepositAInterestH Is Nothing Then
                    DepositAInterestHs.Add(_DepositAInterestH)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                _Stream.Close()
                _Stream = Nothing
            End Try
            Return DepositAInterestHs
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nResult As Integer = 0
            For Each _DepositAInterestH As DepositAInterestH In DepositAInterestHs
                If Not _DepositAInterestH Is Nothing Then
                    Dim _DepositAInterestHFacade As DepositAInterestHFacade = New DepositAInterestHFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    Try
                        '_DepositAInterestHFacade.SynchronizeDepositAInterestH(_DepositAInterestH)
                        nResult = _DepositAInterestHFacade.Insert(_DepositAInterestH)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositAInterestParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.DepositAInterestParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & "Insert SP :" & _DepositAInterestH.Dealer.ID & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        nResult = -1
                    End Try
                End If
            Next
            Return nResult
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseDepositAInterestH(ByVal ValParser As String) As DepositAInterestH
            _DepositAInterestH = New DepositAInterestH
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
                    Case Is = 1
                        If sTemp.Trim.Length > 0 Then
                            'Mapping dari DealerCode menjadi DealerID
                            Dim objDealer As Dealer = RetrieveDealer(sTemp.Trim)
                            If objDealer.ID > 0 Then
                                _DepositAInterestH.Dealer = objDealer
                            Else
                                Throw New Exception("Dealer Tidak Ketemu")
                            End If
                            '_DepositAInterestH.Dealer.ID = sTemp.Trim
                        Else
                            errorMessage.Append("Invalid Dealer Code" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If IsNumeric(sTemp.Trim) Then
                            _DepositAInterestH.Year = CInt(sTemp.Trim)
                        Else
                            errorMessage.Append("Invalid Year" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If IsNumeric(sTemp.Trim) Then
                            _DepositAInterestH.Periode = CInt(sTemp.Trim)
                        Else
                            errorMessage.Append("Invalid Periode" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        If IsNumeric(sTemp.Trim) Then
                            _DepositAInterestH.InterestAmount = CDec(sTemp.Trim)
                        Else
                            errorMessage.Append("Invalid Interest Amount" & Chr(13) & Chr(10))
                        End If
                    Case Is = 5
                        If IsNumeric(sTemp.Trim) Then
                            _DepositAInterestH.TaxAmount = CDec(sTemp.Trim)
                        Else
                            errorMessage.Append("Invalid Tax Amount" & Chr(13) & Chr(10))
                        End If
                    Case Is = 6
                        If IsNumeric(sTemp.Trim) Then
                            _DepositAInterestH.NettoAmount = CDec(sTemp.Trim)
                        Else
                            errorMessage.Append("Invalid Netto Amount" & Chr(13) & Chr(10))
                        End If
                    Case Is = 7
                        If sTemp.Trim.Length > 0 Then
                            'Mapping dari ProductCategoryCode menjadi ProductCategoryID
                            Dim objProductCategory As ProductCategory = RetrieveProductCategory(sTemp.Trim)
                            If objProductCategory.ID > 0 Then
                                _DepositAInterestH.ProductCategory = objProductCategory
                            Else
                                Throw New Exception("ProductCategory Tidak Ketemu")
                            End If

                        Else
                            errorMessage.Append("Invalid ProductCategory Code" & Chr(13) & Chr(10))
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
            Return _DepositAInterestH
        End Function

        Private Sub ParseDepositAInterestD(ByVal ValParser As String, ByVal _objDepositAInterestH As DepositAInterestH)
            _DepositAInterestD = New DepositAInterestD
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            errorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        If sTemp.Trim.Length > 0 Then
                            _DepositAInterestD.DealerCode = sTemp.Trim
                        Else
                            errorMessage.Append("Invalid Dealer Code" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If IsNumeric(sTemp.Trim) Then
                            _DepositAInterestD.Year = CInt(sTemp.Trim)
                        Else
                            errorMessage.Append("Invalid Year" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If sTemp.Trim.Length > 0 Then
                            _DepositAInterestD.Month = sTemp.Trim
                        Else
                            errorMessage.Append("Invalid Month" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        If IsNumeric(sTemp.Trim) Then
                            _DepositAInterestD.InterestAmount = CDec(sTemp.Trim)
                        Else
                            errorMessage.Append("Invalid Interest Amount" & Chr(13) & Chr(10))
                        End If
                    Case Is = 5
                        If IsNumeric(sTemp.Trim) Then
                            _DepositAInterestD.NettoAmount = CDec(sTemp.Trim)
                        Else
                            'errorMessage.Append("Invalid Netto Amount" & Chr(13) & Chr(10))
                            'di kosongkan dari SAP
                            _DepositAInterestD.NettoAmount = 0
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
                _objDepositAInterestH.DepositAInterestDs.Add(_DepositAInterestD)
            End If
        End Sub

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
