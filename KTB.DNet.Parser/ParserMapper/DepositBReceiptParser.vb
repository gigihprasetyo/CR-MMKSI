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
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.BusinessFacade.General
Imports KTB.DNET.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
#End Region

Namespace KTB.DNet.Parser
    Public Class DepositBReceiptParser
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
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBReceiptParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DepositBJVParser, BlockName)
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
                        objNew.TanggalPelunasan = item.TanggalPelunasan
                        Dim mm = New DepositBReceiptFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(objNew)
                        If objNew.ID > 0 Then
                            Dim objDomain As DepositBPencairanHeader = New DepositBPencairanHeader
                            Dim objFacade As DepositBPencairanHeaderFacade = New DepositBPencairanHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            objDomain = objFacade.Retrieve(item.DepositBPencairanHeader.ID)
                            If objDomain.ID > 0 Then
                                objDomain.Status = DepositBEnum.StatusPencairan.Cair
                                vResult = objFacade.Update(objDomain)
                            End If
                        End If
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBReceiptParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BenefitClaimParser, BlockName)
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

            Dim strTglPelunasan = String.Empty

            '100001;1900243915;20160715;CAIR;DBK1600018;2000171732
            'DealerCode;JVNumber;PaymentDate;Status;NoRekKuitansi;PaymentDocument

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
                    Case Is = 1 'JVNumber
                        If sTemp.Trim.Length > 0 Then
                            _DepositBReceipt.JVNumber = sTemp.Trim
                        Else
                            errorMessage.Append("Invalid JV Number" & Chr(13) & Chr(10))
                        End If
                        
                    Case Is = 2 'PaymentDate

                        Try
                            If sTemp.Trim.Length > 0 Then
                                strTglPelunasan = sTemp.Trim()
                                Dim vTgl As DateTime = New DateTime(sTemp.Substring(0, 4), CInt(sTemp.Substring(4, 2)), CInt(sTemp.Substring(6, 2)))
                                _DepositBReceipt.TanggalPelunasan = vTgl
                            End If

                        Catch ex As Exception
                            strTglPelunasan = ""
                            errorMessage.Append("Invalid Payment Date" & Chr(13) & Chr(10))
                        End Try

                    Case Is = 3 'Status

                    Case Is = 4 'NoRegKuitansi
                        If sTemp.Trim.Length > 0 Then
                            Try
                                Dim _NoRegKuitansi As String = sTemp.Trim
                                _DepositBReceipt = New DepositBReceiptFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByNoRegKuitansi(_NoRegKuitansi)
                                If Not IsNothing(_DepositBReceipt) Then
                                    _DepositBReceipt.NoRegKuitansi = sTemp.Trim
                                End If

                                If strTglPelunasan <> "" Then
                                    Dim vTgl As DateTime = New DateTime(strTglPelunasan.Substring(0, 4), CInt(strTglPelunasan.Substring(4, 2)), CInt(strTglPelunasan.Substring(6, 2)))
                                    _DepositBReceipt.TanggalPelunasan = vTgl
                                End If
                             
                            Catch ex As Exception
                                errorMessage.Append("Invalid No Reg. Kuitansi" & Chr(13) & Chr(10))
                                'Exit Select
                            End Try

                        Else
                            errorMessage.Append("No Reg. Kuitansi is Empty" & Chr(13) & Chr(10))
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