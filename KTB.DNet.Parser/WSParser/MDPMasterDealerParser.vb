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
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.MDP
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

Namespace KTB.DNet.Parser
    Public Class MDPMasterDealerParser
        Inherits AbstractParser
#Region "Private Variables"
        Private arrMDPMasterDealer As ArrayList
        Private Grammar As Regex
        Private errorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String
            Dim objMDPMasterDealer As MDPMasterDealer = Nothing
            arrMDPMasterDealer = New ArrayList


            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        objMDPMasterDealer = Nothing
                        errorMessage = New StringBuilder()
                        ' create objek MDP Master Dealer
                        objMDPMasterDealer = ParseHeader(line)
                        ' insert to array objek MDP Master Dealer
                        If Not IsNothing(objMDPMasterDealer) Then
                            If errorMessage.ToString() <> String.Empty Then
                                objMDPMasterDealer.ErrorMessage = errorMessage.ToString()
                            End If

                            arrMDPMasterDealer.Add(objMDPMasterDealer)
                        End If
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MDPMasterDealerParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPPeriodStockParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    Throw e
                End Try
            Next
            Return arrMDPMasterDealer
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim _MDPMasterDealerFacade As New MDPMasterDealerFacade(user)
            Dim _MDPMasterDealerList As New ArrayList

            ' loop MDPMasterDealer array
            For Each _MDPMasterDealer As MDPMasterDealer In arrMDPMasterDealer
                Try
                    If _MDPMasterDealer.ErrorMessage = String.Empty AndAlso IsNothing(_MDPMasterDealer.ErrorMessage) Then
                        _MDPMasterDealerList.Add(_MDPMasterDealer)
                    Else
                        nError += 1
                        sMsg &= _MDPMasterDealer.ErrorMessage & ";"
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If _MDPMasterDealerFacade.InserOrUpdatetWithTransactionManager(_MDPMasterDealerList) < 0 Then
                nError += 1
            Else
                'Insert MDPMasterDealerHistory
                Dim _MDPMasterDealerHistory As New MDPMasterDealerHistory
                Dim _MDPMasterDealerHistoryFacade As New MDPMasterDealerHistoryFacade(user)

                For Each item As MDPMasterDealer In _MDPMasterDealerList
                    If item.ID <> 0 And item.LastUpdateBy.ToLower <> "not update" Then
                        _MDPMasterDealerHistory.MDPMasterDealer = item
                        _MDPMasterDealerHistory.StatusFrom = item.StatusOld
                        _MDPMasterDealerHistory.StatusTo = item.Status
                        _MDPMasterDealerHistory.CreatedBy = item.CreatedBy
                        _MDPMasterDealerHistoryFacade.Insert(_MDPMasterDealerHistory)
                    End If
                Next
            End If

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _MDPMasterDealerList.Count.ToString(), "ws-worker", "MDPMasterDealerParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPMasterDealerParser, BlockName)

                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MDPMasterDealerParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MDPMasterDealerParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As MDPMasterDealer
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objMDPMasterDealer As New MDPMasterDealer

            Dim PDCode As String
            Dim UPTCode As Boolean = False

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else

                'Dealer Code / H-1
                PDCode = cols(1).Trim
                Dim objDealer As New Dealer
                If PDCode = String.Empty Then
                    writeError("Dealer Code can't be empty")
                Else
                    objDealer = New DealerFacade(user).Retrieve(PDCode)
                End If

                PDCode = cols(2).Trim
                Dim status As Short

                If PDCode.ToUpper = "X" Then
                    status = 1
                Else
                    status = 0
                End If

                'Check MDPMasterDealer
                Dim MDPMasterDealerCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                MDPMasterDealerCriteria.opAnd(New Criteria(GetType(MDPMasterDealer), "Dealer.ID", MatchType.Exact, objDealer.ID))
                Dim MDPMasterDealerList As ArrayList = New MDPMasterDealerFacade(user).Retrieve(MDPMasterDealerCriteria)

                If MDPMasterDealerList.Count > 0 Then
                    objMDPMasterDealer = MDPMasterDealerList(0)
                    If objMDPMasterDealer.Dealer.ID <> objDealer.ID Then
                        objMDPMasterDealer.Dealer = objDealer
                        UPTCode = True
                    End If
                Else
                    If objDealer.ID <> 0 Then
                        objMDPMasterDealer.Dealer = objDealer
                        UPTCode = True
                    Else
                        writeError("Dealer Code Not Found " & cols(1).Trim)
                    End If
                End If

                objMDPMasterDealer.StatusOld = objMDPMasterDealer.Status
                If objMDPMasterDealer.Status <> status Then
                    objMDPMasterDealer.Status = status
                    UPTCode = True
                End If

                If (UPTCode) Then
                    objMDPMasterDealer.LastUpdateBy = user.Identity.Name
                    objMDPMasterDealer.LastUpdateTime = DateTime.Now
                Else
                    objMDPMasterDealer.LastUpdateBy = "Not Update"
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objMDPMasterDealer.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                End If
            End If

            Return objMDPMasterDealer

        End Function

#End Region

#Region "Public Properties"



#End Region

    End Class

End Namespace