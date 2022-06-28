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

    Public Class TOPSPBillingDepositParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _TOPSPBillingDepositList As ArrayList

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

                _TOPSPBillingDepositList = New ArrayList()

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then

                            errorMessage = New StringBuilder()
                            Dim _TOPSPDeposit As New TOPSPDeposit
                            _TOPSPDeposit = ParseTOPSPDeposit(line)
                            If Not IsNothing(_TOPSPDeposit) Then
                                _TOPSPBillingDepositList.Add(_TOPSPDeposit)
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "TOPSPBillingDepositParser.vb", "Parsing", "0", SourceName, WSMSyslogParameter.ParserType.TOPSPBillingDepositParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")

                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _TOPSPBillingDepositList
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)



            For Each topBlockStatus As TOPSPDeposit In _TOPSPBillingDepositList
                Try
                    If Not IsNothing(topBlockStatus.ErrorMessage) AndAlso topBlockStatus.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & topBlockStatus.ErrorMessage.ToString() & ";"
                    Else
                        Dim topBlockStatusFacade As New TOPSPDepositFacade(user)
                        topBlockStatusFacade.Merge(topBlockStatus)
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next


            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _TOPSPBillingDepositList.Count.ToString(), "ws-worker", "TOPSPBillingDepositParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPBillingDepositParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "TOPSPBillingDepositParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPBillingDepositParser, BlockName)
                Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _TOPSPBillingDepositList.Count.ToString() & " Data. Message : " & sMsg)
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

        Private Function ParseTOPSPDeposit(ByVal line As String) As TOPSPDeposit
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim _SparePartBilling As New SparePartBilling
            Dim _TOPSPDeposit As New TOPSPDeposit
            Dim _Dealer As New Dealer

            errorMessage = New StringBuilder()
            If cols.Length <> 7 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                Dim strData As String = String.Empty


                '2 Kode Deler
                If cols(2).Trim = String.Empty Then
                    errorMessage.Append("Dealer COde empty" & Chr(13) & Chr(10))
                Else
                    Dim cVCA As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aVCAs As ArrayList
                    cVCA.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, cols(2)))
                    aVCAs = (New DealerFacade(user)).Retrieve(cVCA)

                    If (aVCAs.Count = 0) Then
                        errorMessage.Append("Dealer COde is not Found!" & Chr(13) & Chr(10))
                    Else
                        _Dealer = CType(aVCAs(0), Dealer)
                    End If

                End If

                Try '1 Billing Number - D-1
                    Dim PDCode As String = cols(1).Trim
                    strData = PDCode
                    If PDCode = String.Empty Then
                        Throw New Exception("Empty Billing Number " & strData)
                    Else
                        'check existing sparepartpo
                        Dim sparePartPOStatusCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartBilling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        sparePartPOStatusCriteria.opAnd(New Criteria(GetType(SparePartBilling), "BillingNumber", MatchType.Exact, PDCode))
                        sparePartPOStatusCriteria.opAnd(New Criteria(GetType(SparePartBilling), "Dealer.ID", MatchType.Exact, _Dealer.ID))
                        Dim sparePartPOStatusExistingList As ArrayList = New SparePartBillingFacade(user).Retrieve(sparePartPOStatusCriteria)

                        If Not IsNothing(sparePartPOStatusExistingList) AndAlso sparePartPOStatusExistingList.Count > 0 Then

                            _SparePartBilling = sparePartPOStatusExistingList(0)

                        Else
                            writeError("Billing Not Found " & PDCode)
                        End If

                    End If


                    Try
                        _TOPSPDeposit.AmountC2 = MyBase.GetCurrency(cols(6))
                    Catch ex As Exception
                        _TOPSPDeposit.AmountC2 = 0
                        writeError("Invalid Amount")
                    End Try

                Catch ex As Exception
                    writeError("TOP Sparepart DEposit error: " & ex.Message)
                End Try



            End If
            _TOPSPDeposit.ErrorMessage = errorMessage.ToString()
            _TOPSPDeposit.SparePartBilling = _SparePartBilling

            Return _TOPSPDeposit
        End Function

#End Region

    End Class
End Namespace
