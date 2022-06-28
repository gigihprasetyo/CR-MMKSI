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
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.SparePart

#End Region

Namespace KTB.DNet.Parser

    Public Class TOPSPBillingBlockParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _topBlockStatusList As ArrayList

#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _topBlockStatusList = New ArrayList()

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorDetail Then

                            errorMessage = New StringBuilder()
                            Dim topBlockStatus As TOPBlockStatus
                            topBlockStatus = ParseSparePartPOStatus(line)
                            _topBlockStatusList.Add(topBlockStatus)
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "TOPSPBillingBlockParser.vb", "Parsing", "0", SourceName, WSMSyslogParameter.ParserType.TOPSPBillingBlockParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")

                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _topBlockStatusList
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)



            For Each topBlockStatus As TOPBlockStatus In _topBlockStatusList
                Try
                    Dim topBlockStatusFacade As New TOPBlockStatusFacade(user)
                    If Not IsNothing(topBlockStatus) Then
                        If topBlockStatus.ID = 0 Then
                            topBlockStatus.CreatedBy = user.Identity.Name
                            topBlockStatus.CreatedTime = DateTime.Now
                            topBlockStatusFacade.InsertWithTransactionManager(topBlockStatus, topBlockStatus.SparePartPOStatus)
                        Else
                            topBlockStatus.LastUpdateBy = user.Identity.Name
                            topBlockStatus.LastUpdateTime = DateTime.Now
                            topBlockStatusFacade.UpdateWithTransactionManager(topBlockStatus, topBlockStatus.SparePartPOStatus)
                        End If
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next


            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _topBlockStatusList.Count.ToString(), "ws-worker", "TOPSPBillingBlockParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPBillingBlockParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "TOPSPBillingBlockParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPBillingBlockParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseSparePartPOStatus(ByVal line As String) As TOPBlockStatus
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim sparePartPOStatus As New SparePartPOStatus
            Dim TOPBlockStatus As New TOPBlockStatus
            Dim sparePartPOStatusFacade As New SparePartPOStatusFacade(user)
            Dim topBlockStatusFacade As New TOPBlockStatusFacade(user)

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                Dim strData As String = String.Empty

                Try '1 SONumber - D-1
                    Dim PDCode As String = cols(1).Trim
                    strData = PDCode
                    If PDCode = String.Empty Then
                        Throw New Exception("Empty SO Number " & strData)
                    Else
                        'check existing sparepartpo
                        Dim sparePartPOStatusCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        sparePartPOStatusCriteria.opAnd(New Criteria(GetType(SparePartPOStatus), "SONumber", MatchType.Exact, PDCode))
                        Dim sparePartPOStatusExistingList As ArrayList = sparePartPOStatusFacade.RetrieveList(sparePartPOStatusCriteria)

                        If sparePartPOStatusExistingList.Count > 0 Then
                            If sparePartPOStatusExistingList.Count = 1 Then
                                sparePartPOStatus = sparePartPOStatusExistingList(0)

                            Else
                                writeError("Terdapat lebih dari 1 row untuk SO Number " & PDCode)
                            End If

                        Else
                            sparePartPOStatus.SONumber = PDCode
                            writeError(String.Format("sparePartPOStatus SO Number {0} tidak ada", PDCode))

                        End If

                        'check existing TOPBlockStatus
                        Dim topBlockStatusCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPBlockStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        topBlockStatusCriteria.opAnd(New Criteria(GetType(TOPBlockStatus), "SparePartPOStatus.SONumber", MatchType.Exact, PDCode))
                        Dim topBlockStatusExistingList As ArrayList = topBlockStatusFacade.Retrieve(topBlockStatusCriteria)

                        If topBlockStatusExistingList.Count > 0 Then
                            If topBlockStatusExistingList.Count = 1 Then
                                TOPBlockStatus = topBlockStatusExistingList(0)

                            Else
                                writeError("Terdapat lebih dari 1 row untuk SO Number " & PDCode)
                            End If

                        Else

                            TOPBlockStatus.MarkLoaded()

                        End If
                    End If
                Catch ex As Exception
                    writeError("SparePartPOStatus error: " & ex.Message)
                End Try

                '2 SODate/D-2

                Try
                    Dim PDCode As String = cols(2).Trim
                    If PDCode <> "" Then
                        Dim year As Integer = PDCode.Substring(0, 4)
                        Dim month As Integer = PDCode.Substring(4, 2)
                        Dim day As Integer = PDCode.Substring(6, 2)
                        Dim SODate As Date = New Date(year, month, day)

                        sparePartPOStatus.SODate = SODate
                    End If

                Catch ex As Exception
                    writeError("Region Desc error: " & ex.Message)
                End Try

            End If
            sparePartPOStatus.MarkLoaded()
            TOPBlockStatus.SparePartPOStatus = sparePartPOStatus
            TOPBlockStatus.RowStatus = 0
            TOPBlockStatus.Status = 0
            Return TOPBlockStatus
        End Function

#End Region

    End Class
End Namespace
