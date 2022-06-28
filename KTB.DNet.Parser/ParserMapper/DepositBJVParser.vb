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
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Training

#End Region

Namespace KTB.DNet.Parser
    Public Class DepositBJVParser
        Inherits AbstractParser
#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private ArrDepositBReceipt As ArrayList
        Private _DepositBReceipt As DepositBReceipt
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
                ArrDepositBReceipt = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If Not _DepositBReceipt Is Nothing Then
                            ArrDepositBReceipt.Add(_DepositBReceipt)
                        End If
                        _DepositBReceipt = ParseDepositBReceipt(val + Delimited)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBJVParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DepositBJVParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DepositBReceipt = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While
                If Not _DepositBReceipt Is Nothing Then
                    ArrDepositBReceipt.Add(_DepositBReceipt)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return ArrDepositBReceipt
        End Function

        Protected Overrides Function DoTransaction() As Integer
            For Each item As DepositBReceipt In ArrDepositBReceipt
                Try
                    Dim vResult As Integer
                    Dim _DepositBReceiptFacade As DepositBReceiptFacade = New DepositBReceiptFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "DepositBPencairanHeader.Dealer.DealerCode", MatchType.Exact, item.DepositBPencairanHeader.Dealer.DealerCode))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "NoRegKuitansi", MatchType.Exact, item.NoRegKuitansi))

                    Dim arlDepositBReceipts As ArrayList = _DepositBReceiptFacade.Retrieve(criterias)

                    If arlDepositBReceipts.Count > 0 Then
                        Dim objNew As DepositBReceipt = CType(arlDepositBReceipts(0), DepositBReceipt)
                        'objNew.NoRegKuitansi
                        objNew.JVNumber = item.JVNumber

                        vResult = _DepositBReceiptFacade.Update(objNew)
                        'Else
                        '    vResult = _DepositBReceiptFacade.Insert(item)

                        If vResult <> -1 Then
                            Dim vStatusResult As Integer
                            Dim objDomain As DepositBPencairanHeader = New DepositBPencairanHeader
                            Dim objFacade As DepositBPencairanHeaderFacade = New DepositBPencairanHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            objDomain = objFacade.Retrieve(item.DepositBPencairanHeader.ID)
                            If objDomain.ID > 0 Then
                                objDomain.Status = DepositBEnum.StatusPencairan.Selesai
                                vStatusResult = objFacade.Update(objDomain)

                                If objDomain.TipePengajuan = DepositBEnum.TipePengajuan.KewajibanReguler Or _
                                    objDomain.TipePengajuan = DepositBEnum.TipePengajuan.Kewajiban_NonReguler Then
                                    Dim vStatusKewajibanResult As Integer = 0
                                    If Not IsNothing(objDomain.DepositBKewajibanHeader) Then
                                        Dim objKewajiban As DepositBKewajibanHeader = objDomain.DepositBKewajibanHeader
                                        objKewajiban.Status = DepositBEnum.StatusPengajuan.Selesai

                                        Dim objKewajibanFacade As DepositBKewajibanHeaderFacade = New DepositBKewajibanHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                        vStatusKewajibanResult = objKewajibanFacade.Update(objKewajiban)
                                    End If
                                End If

                                If Not IsNothing(objDomain.DepositBDebitNote) Then
                                    Dim func As New TrBillingHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                    func.UpdateFinishBill(objDomain.DepositBDebitNote, item.JVNumber)

                                End If
                            End If
                        End If
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBJVParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BenefitClaimParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.NoRegKuitansi & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
            Return 1
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseDepositBReceipt(ByVal ValParser As String) As DepositBReceipt
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            errorMessage = New StringBuilder
            _DepositBReceipt = New DepositBReceipt

            'Kodedealer;NoRegKuitansi;JVNumber

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
                                    '_DepositBReceipt.DepositBPencairanHeader.Dealer = _dealer
                                End If
                            Catch ex As Exception
                                errorMessage.Append("Dealer code not found" & Chr(13) & Chr(10))
                                Exit Select
                            End Try

                        Else
                            errorMessage.Append("Dealer code is Empty" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1 'NoReg Kuitansi
                        If sTemp.Trim.Length > 0 Then
                            Try
                                Dim _NoRegKuitansi As String = sTemp.Trim
                                _DepositBReceipt = New DepositBReceiptFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByNoRegKuitansi(_NoRegKuitansi)
                                If Not IsNothing(_DepositBReceipt) Then
                                    _DepositBReceipt.NoRegKuitansi = sTemp.Trim
                                End If

                            Catch ex As Exception
                                errorMessage.Append("Deposit B Receipt not found" & Chr(13) & Chr(10))
                            End Try

                        Else
                            errorMessage.Append("No Reg. Kuitansi is Empty" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2 'Amount
                        If sTemp.Trim.Length > 0 Then
                            _DepositBReceipt.JVNumber = sTemp.Trim
                        Else
                            errorMessage.Append("Invalid JV Number" & Chr(13) & Chr(10))
                        End If

                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If errorMessage.Length > 0 Then
                _DepositBReceipt = Nothing '
                Throw New Exception(errorMessage.ToString)
            End If
            Return _DepositBReceipt
        End Function

#End Region

    End Class

End Namespace