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
#End Region

Namespace KTB.DNet.Parser
    Public Class MSPRegistrationSOParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private arr As ArrayList
        Private _objMSPDC As MSPDC
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
                arr = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = String.Empty)
                    Try
                        _objMSPDC = ParseMSPDC(val + Delimited)
                        If Not _objMSPDC Is Nothing Then
                            arr.Add(_objMSPDC)
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "MSPRegistrationSOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPDC, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _objMSPDC = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While
               
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return arr
        End Function

        Protected Overrides Function DoTransaction() As Integer
            For Each item As MSPDC In arr
                Try
                    Dim vResult As Integer
                    Dim _facMSDC As MSPDCFacade = New MSPDCFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPDC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crt.opAnd(New Criteria(GetType(MSPDC), "DebitChargeNo", MatchType.Exact, item.DebitChargeNo))
                    crt.opAnd(New Criteria(GetType(MSPDC), "MSPRegistrationHistory.ID", MatchType.Exact, item.MSPRegistrationHistory.ID))

                    Dim arrObjOld As ArrayList = _facMSDC.Retrieve(crt)
                    If arrObjOld.Count > 0 Then
                        Dim objNew As MSPDC = CType(arrObjOld(0), MSPDC)
                        objNew.Amount = item.Amount
                        objNew.TOPNo = item.TOPNo
                        objNew.FileName = item.FileName
                        objNew.DocumentDate = item.DocumentDate

                        vResult = New MSPDCFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(objNew)
                    Else
                        Dim objNew As New MSPDC
                        objNew.MSPRegistrationHistory = item.MSPRegistrationHistory
                        objNew.DebitChargeNo = item.DebitChargeNo
                        objNew.Amount = item.Amount
                        objNew.TOPNo = item.TOPNo
                        objNew.FileName = item.FileName
                        objNew.DocumentDate = item.DocumentDate

                        vResult = New MSPDCFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(objNew)
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "MSPRegistrationSOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.MSPDC, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.DebitChargeNo & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
            Return 1
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseMSPDC(ByVal ValParser As String) As MSPDC
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            errorMessage = New StringBuilder
            _objMSPDC = Nothing

            'H;5590000003;MSP0000002;Z000;0000100001;14745500;yyyymmdd
            'H; SONumber; MSPCode; TOPNo; DealerCode; AmountTotal = harga awal 85% + ppn 10%; RequestDate

            Dim path As String = KTB.DNet.Lib.WebConfig.GetValue("MSP_REGISTRATION_DC") & "\fudcsp_"
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Trim = "H" Then
                            _objMSPDC = New MSPDC
                        End If
                    Case Is = 1 'SO Number
                        If sTemp.Trim.Length > 0 Then
                            _objMSPDC.DebitChargeNo = sTemp.Trim
                            _objMSPDC.FileName = path & sTemp.Trim & ".pdf"

                            ' Set MSP Registration History ID
                            Dim crtRegHistory As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crtRegHistory.opAnd(New Criteria(GetType(MSPRegistrationHistory), "DebitChargeNo", MatchType.Exact, sTemp.Trim))
                            Dim arrRegHIstory As ArrayList = New MSPRegistrationHistoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(crtRegHistory)

                            If arrRegHIstory.Count > 0 Then
                                _objMSPDC.MSPRegistrationHistory = CType(arrRegHIstory(0), MSPRegistrationHistory)
                            Else
                                errorMessage.Append("Debit Charge Number " & sTemp & " tidak ada di DNet")
                            End If
                        Else
                            errorMessage.Append("Debit Charge Number is Empty" & Chr(13) & Chr(10))
                        End If
                    
                    Case Is = 3 'TOPNo
                        If sTemp.Trim.Length > 0 Then
                            _objMSPDC.TOPNo = sTemp.Trim
                        Else
                            errorMessage.Append("TOP Number is Empty" & Chr(13) & Chr(10))
                        End If

                    Case Is = 5 'Amount total
                        If sTemp.Trim.Length > 0 Then
                            _objMSPDC.Amount = sTemp.Trim
                        Else
                            errorMessage.Append("Amount total is Empty" & Chr(13) & Chr(10))
                        End If

                    Case Is = 6 'docuent date
                        If sTemp.Trim.Length > 0 Then
                            Try
                                _objMSPDC.DocumentDate = MyBase.GetDateShort(sTemp)
                            Catch ex As Exception
                                errorMessage.Append("Invalid Document Date")
                            End Try
                        Else
                            errorMessage.Append("Document date is Empty" & Chr(13) & Chr(10))
                        End If

                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If errorMessage.Length > 0 Then
                _objMSPDC = Nothing
                Throw New Exception(errorMessage.ToString)
            End If
            Return _objMSPDC
        End Function

#End Region
    End Class
End Namespace

