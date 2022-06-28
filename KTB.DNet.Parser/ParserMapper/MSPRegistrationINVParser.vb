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
    Public Class MSPRegistrationINVParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private arr As ArrayList
        Private _objMSPDM As MSPDM
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
                        _objMSPDM = ParseMSPDM(val + Delimited)
                        If Not _objMSPDM Is Nothing Then
                            arr.Add(_objMSPDM)
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "MSPRegistrationINVParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPDM, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _objMSPDM = Nothing
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
            For Each item As MSPDM In arr
                Try
                    Dim vResult As Integer
                    Dim _facMSPDM As MSPDMFacade = New MSPDMFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPDM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crt.opAnd(New Criteria(GetType(MSPDM), "MSPDC.DebitChargeNo", MatchType.Exact, item.MSPDC.DebitChargeNo))

                    Dim arrObjOld As ArrayList = _facMSPDM.Retrieve(crt)
                    If arrObjOld.Count > 0 Then
                        Dim objNew As MSPDM = CType(arrObjOld(0), MSPDM)
                        objNew.Amount = item.Amount
                        objNew.DocType = item.DocType
                        objNew.DocumentDate = item.DocumentDate
                        objNew.MSPDC = item.MSPDC
                        objNew.FileName = item.FileName
                        objNew.DebitMemoNo = item.DebitMemoNo

                        vResult = New MSPDMFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(objNew)
                    Else
                        Dim objNew As New MSPDM
                        objNew.Amount = item.Amount
                        objNew.DocType = item.DocType
                        objNew.DocumentDate = item.DocumentDate
                        objNew.MSPDC = item.MSPDC
                        objNew.DebitMemoNo = item.DebitMemoNo
                        objNew.FileName = item.FileName

                        vResult = New MSPDMFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(objNew)
                    End If

                    crt = New CriteriaComposite(New Criteria(GetType(MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crt.opAnd(New Criteria(GetType(MSPRegistrationHistory), "ID", MatchType.Exact, item.MSPDC.MSPRegistrationHistory.ID))
                    crt.opAnd(New Criteria(GetType(MSPRegistrationHistory), "Status", MatchType.Exact, CInt(EnumStatusMSP.Status.Proses)))

                    Dim facMSPRegHistory As MSPRegistrationHistoryFacade = New MSPRegistrationHistoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                    Dim arrMSPRegHistory As ArrayList = facMSPRegHistory.Retrieve(crt)
                    If arrMSPRegHistory.Count > 0 Then
                        Dim objMSPRegHistory As MSPRegistrationHistory = CType(arrMSPRegHistory(0), MSPRegistrationHistory)
                        objMSPRegHistory.Status = EnumStatusMSP.Status.Selesai

                        facMSPRegHistory.Update(objMSPRegHistory)
                        ' add to history status
                        Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.MSP_Registration), objMSPRegHistory.ID, CInt(EnumStatusMSP.Status.Proses), objMSPRegHistory.Status)
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "MSPRegistrationINVParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.MSPDM, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.MSPDC.DebitChargeNo & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
            Return 1
        End Function

        Protected Function UpdateMSPRegHistoryStatus() As Boolean
            Dim bool As Boolean = False

            Return bool
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseMSPDM(ByVal ValParser As String) As MSPDM
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            errorMessage = New StringBuilder
            _objMSPDM = Nothing

            'H;5890000003;20171228;ZA91;14745500;5590000003
            'dm no;tgl;doctype;amount 85%;dc no

            Dim path As String = KTB.DNet.Lib.WebConfig.GetValue("MSP_REGISTRATION_DM") & "\fudnsp_"
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Trim.Length > 0 Then
                            If sTemp = "H" Then
                                _objMSPDM = New MSPDM
                            End If
                        End If
                    Case Is = 1 'debit memo Number
                        If sTemp.Trim.Length > 0 Then
                            _objMSPDM.DebitMemoNo = sTemp.Trim
                            _objMSPDM.FileName = path & sTemp.Trim & ".pdf"
                        Else
                            errorMessage.Append("Debit Memo Number is Empty" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2 'docuent date
                        If sTemp.Trim.Length > 0 Then
                            Try
                                _objMSPDM.DocumentDate = MyBase.GetDateShort(sTemp)
                            Catch ex As Exception
                                errorMessage.Append("Invalid Dokumen Date")
                            End Try
                        Else
                            errorMessage.Append("Document date is Empty" & Chr(13) & Chr(10))
                        End If

                    Case Is = 3 'DocType
                        If sTemp.Trim.Length > 0 Then
                            _objMSPDM.DocType = sTemp.Trim
                        Else
                            errorMessage.Append("Document Type is Empty" & Chr(13) & Chr(10))
                        End If

                    Case Is = 4 'Amount 
                        If sTemp.Trim.Length > 0 Then
                            _objMSPDM.Amount = sTemp.Trim
                        Else
                            errorMessage.Append("Amount is Empty" & Chr(13) & Chr(10))
                        End If
                    Case Is = 5 'debit charge no 
                        If sTemp.Trim.Length > 0 Then
                            Dim objMSPDC As MSPDC = New MSPDCFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                            If objMSPDC.ID > 0 Then
                                _objMSPDM.MSPDC = objMSPDC
                            Else
                                errorMessage.Append("Debit charge no " & sTemp & " tidak ada di DNet")
                            End If

                        Else
                            errorMessage.Append("Debit Charge is Empty" & Chr(13) & Chr(10))
                        End If

                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If errorMessage.Length > 0 Then
                _objMSPDM = Nothing
                Throw New Exception(errorMessage.ToString)
            End If
            Return _objMSPDM
        End Function

#End Region
    End Class
End Namespace

