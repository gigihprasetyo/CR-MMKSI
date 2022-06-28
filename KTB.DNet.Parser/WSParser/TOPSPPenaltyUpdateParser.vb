Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text.RegularExpressions
Imports KTB.DNet.Domain
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports System.Security.Principal
Imports KTB.DNet.Domain.Search

Public Class TOPSPPenaltyUpdateParser
    Inherits AbstractParser

#Region "Private Variables"
    Private _Stream As StreamReader
    Private Grammar As Regex
    Private _fileName As String
    Private errorMessage As StringBuilder

    Private _arlTOPSPPenaltyUpdates As ArrayList
    Private _TOPSPPenaltyUpdate As TOPSPPenalty
#End Region

#Region "Constructors/Destructors/Finalizers"

    Public Sub New()
        Grammar = MyBase.GetGrammarParser()
    End Sub

#End Region

    'K;TOPSPPenaltyUpdate_[Timestamps]\nH;DebitMemoNo; AccountingDocNo;Amountpayment;PaymentDate
    Protected Overrides Function DoParse(KeyName As String, Content As String) As Object
        Try
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String

            _arlTOPSPPenaltyUpdates = New ArrayList()
            _TOPSPPenaltyUpdate = Nothing

            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        _TOPSPPenaltyUpdate = Nothing

                        errorMessage = New StringBuilder()
                        _TOPSPPenaltyUpdate = ParseHeader(line)

                        If Not IsNothing(_TOPSPPenaltyUpdate) Then
                            _arlTOPSPPenaltyUpdates.Add(_TOPSPPenaltyUpdate)
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "TOPSPPenaltyUpdateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPPenaltyUpdateParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

        Catch ex As Exception
            Throw ex
        Finally
        End Try

        Return _arlTOPSPPenaltyUpdates
    End Function

    Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
        Return Nothing
    End Function

    Protected Overrides Function DoTransaction() As Integer
        Dim objTOPSPPenaltyFacade As TOPSPPenaltyFacade
        Dim nError As Integer = 0
        Dim sMsg As String = ""

        For Each objTOPSPPenaltyUpdate As TOPSPPenalty In _arlTOPSPPenaltyUpdates
            Try
                If Not IsNothing(objTOPSPPenaltyUpdate.ErrorMessage) AndAlso objTOPSPPenaltyUpdate.ErrorMessage <> "" Then
                    nError += 1
                    sMsg = sMsg & objTOPSPPenaltyUpdate.ErrorMessage.ToString()
                Else
                    objTOPSPPenaltyFacade = New TOPSPPenaltyFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS TOPSPPenaltyUpdate"), Nothing))
                    objTOPSPPenaltyFacade.Update(objTOPSPPenaltyUpdate)
                End If

            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "TOPSPPenaltyUpdateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objTOPSPPenaltyUpdate.TOPSPTransferPayment.RegNumber & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try
        Next

        If nError > 0 Then
            SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arlTOPSPPenaltyUpdates.Count.ToString(), "ws-worker", "TOPSPPenaltyUpdateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPPenaltyUpdateParser, BlockName)
            SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "TOPSPPenaltyUpdateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPPenaltyUpdateParser, BlockName)
            Throw New Exception(sMsg)
        End If
        Return 0
    End Function

#Region "Private Methods"
    Private Sub writeError(str As String)
        errorMessage.Append(str & Chr(13) & Chr(10))
    End Sub

    Private Function ParseHeader(ByVal line As String) As TOPSPPenalty
        Dim cols As String() = line.Split(MyBase.ColSeparator)
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)

        Dim objTOPSPPenaltyUpdate As New TOPSPPenalty
        Dim objTOPSPPenaltyFacade As New TOPSPPenaltyFacade(user)

        'K;TOPSPPenaltyUpdate_[Timestamps]\nH;DebitMemoNo;AccountingDocNo;Amountpayment;PaymentDated;ClearingDocNumber
        'K;TOPSPPenaltyUpdate_[202005180600]\nH;18800044327;18001;500;20200518;18001

        errorMessage = New StringBuilder()
        If cols.Length <> 7 Then
            writeError("Invalid Header Format")
        Else
            If cols(1).Trim = String.Empty Then
                writeError("Debit Memo Number can't be empty")
            ElseIf cols(2).Trim = String.Empty Then
                writeError("Accounting Document Number can't be empty")
            ElseIf cols(3).Trim = String.Empty Then
                writeError("Amount Payment can't be empty")
            ElseIf cols(4).Trim = String.Empty Then
                writeError("Payment Date can't be empty")
                'ElseIf cols(5).Trim = String.Empty Then
                'writeError("Clearing Document Number can't be empty")
            Else
                Try
                    Dim vDebitMemoNo As String = cols(1).Trim
                    Dim vAccountingDocNo As String = cols(2).Trim
                    Dim vAmountpayment As String = cols(3).Trim

                    Dim Thn As Integer = 0
                    Dim Bln As Integer = 0
                    Dim Tgl As Integer = 0
                    Dim vPaymentDate As Date = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                    If cols(4).Trim <> "" Then
                        Thn = CInt(Strings.Left(cols(4).Trim, 4))
                        Bln = CInt(cols(4).Trim.Substring(4, 2))
                        Tgl = CInt(cols(4).Trim.Substring(6, 2))
                        vPaymentDate = New DateTime(Thn, Bln, Tgl)
                    End If
                    Dim vClearingDocNumber As String = cols(5).Trim
                    Dim vMessage As String = cols(6).Trim

                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(TOPSPPenalty), "DebitMemoNumber", MatchType.Exact, vDebitMemoNo))
                    Dim arlList As ArrayList = New TOPSPPenaltyFacade(user).Retrieve(crit)
                    If Not IsNothing(arlList) AndAlso arlList.Count > 0 Then
                        objTOPSPPenaltyUpdate = CType(arlList(0), TOPSPPenalty)
                        objTOPSPPenaltyUpdate.DebitMemoNumber = vDebitMemoNo
                        objTOPSPPenaltyUpdate.AccountingNumber = vAccountingDocNo
                        objTOPSPPenaltyUpdate.ClearingNumber = vClearingDocNumber

                        If vClearingDocNumber.Trim <> "" Then
                            objTOPSPPenaltyUpdate.AmountPayment = CDec(vAmountpayment)
                            If objTOPSPPenaltyUpdate.AmountPayment >= objTOPSPPenaltyUpdate.Amount Then
                                objTOPSPPenaltyUpdate.StatusPenalty = 1  '-- Selesai
                            End If
                            objTOPSPPenaltyUpdate.PaymentDate = vPaymentDate
                        End If
                        objTOPSPPenaltyUpdate.Message = vMessage
                    Else
                        writeError("Debit Memo Number doesn't exist in database")
                    End If

                Catch ex As Exception
                    writeError("TOP SparePart Penalty error: " & ex.Message)
                End Try
            End If
        End If
        If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
            If IsNothing(objTOPSPPenaltyUpdate) Then objTOPSPPenaltyUpdate = New TOPSPPenalty()
            objTOPSPPenaltyUpdate.ErrorMessage = errorMessage.ToString()
        End If

        Return objTOPSPPenaltyUpdate
    End Function

#End Region

End Class
