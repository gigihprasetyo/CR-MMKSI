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

Public Class TOPSPPenaltyParser
    Inherits AbstractParser

#Region "Private Variables"
    Private _Stream As StreamReader
    Private Grammar As Regex
    Private _fileName As String
    Private errorMessage As StringBuilder

    Private _TOPSPPenaltys As ArrayList
    Private _TOPSPPenalty As TOPSPPenalty
    Private _TOPSPPenaltyDetail As TOPSPPenaltyDetail
#End Region

#Region "Constructors/Destructors/Finalizers"

    Public Sub New()
        Grammar = MyBase.GetGrammarParser()
    End Sub

#End Region

    Protected Overrides Function DoParse(KeyName As String, Content As String) As Object
        Dim lines As String() = MyBase.GetLines(Content)
        Dim ind As String
        Dim line As String

        _TOPSPPenaltys = New ArrayList()
        _TOPSPPenalty = Nothing

        For i As Integer = 0 To lines.Length - 1
            Try
                line = lines(i)

                ind = line.Split(MyBase.ColSeparator)(0)
                If ind = MyBase.IndicatorHeader Then
                    _TOPSPPenalty = Nothing

                    errorMessage = New StringBuilder()
                    _TOPSPPenalty = ParseHeader(line)

                    If Not IsNothing(_TOPSPPenalty) Then
                        _TOPSPPenaltys.Add(_TOPSPPenalty)
                    End If
                ElseIf ind = MyBase.IndicatorDetail Then
                    If IsNothing(_TOPSPPenalty) OrElse Not IsNothing(_TOPSPPenalty.ErrorMessage) Then
                    Else
                        _TOPSPPenaltyDetail = ParseDetail(line)

                        If Not IsNothing(_TOPSPPenaltyDetail) Then
                            _TOPSPPenaltyDetail.TOPSPPenalty = _TOPSPPenalty
                            _TOPSPPenalty.TOPSPPenaltyDetails.Add(_TOPSPPenaltyDetail)
                            If Not IsNothing(_TOPSPPenaltyDetail.ErrorMessage) AndAlso _TOPSPPenaltyDetail.ErrorMessage.Trim <> String.Empty Then
                                _TOPSPPenalty.ErrorMessage = _TOPSPPenalty.ErrorMessage & ";" & _TOPSPPenaltyDetail.ErrorMessage
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "TOPSPPenaltyParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPPenaltyParser, BlockName)
                Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try
        Next
        Return _TOPSPPenaltys
    End Function

    Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
        Return Nothing
    End Function

    Protected Overrides Function DoTransaction() As Integer
        Dim objTOPSPPenaltyFacade As TOPSPPenaltyFacade
        Dim nError As Integer = 0
        Dim sMsg As String = ""

        For Each objTOPSPPenalty As TOPSPPenalty In _TOPSPPenaltys
            Try
                If Not IsNothing(objTOPSPPenalty.ErrorMessage) AndAlso objTOPSPPenalty.ErrorMessage <> "" Then
                    nError += 1
                    sMsg = sMsg & objTOPSPPenalty.ErrorMessage.ToString()
                Else
                    objTOPSPPenaltyFacade = New TOPSPPenaltyFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS TOPSPPenalty"), Nothing))
                    objTOPSPPenaltyFacade.InsertFromWebSevice(objTOPSPPenalty)
                End If

            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "TOPSPPenaltyParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objTOPSPPenalty.TOPSPTransferPayment.RegNumber & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try
        Next

        If nError > 0 Then
            SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _TOPSPPenaltys.Count.ToString(), "ws-worker", "TOPSPPenaltyParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPPenaltyParser, BlockName)
            SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "TOPSPPenaltyParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPPenaltyParser, BlockName)
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

        Dim objTOPSPPenalty As New TOPSPPenalty
        Dim objTOPSPPenaltyFacade As New TOPSPPenaltyFacade(user)

        'K;TOPSPPenalty_[Timestamps]\nH;DealerCode;RegNumber;AmountPenalty;DebitMemoDate(yyyymmdd);DebitMemoNo
        'K;TOPSPPenalty_202005180600\nH;100001;256890000123;500;20200518;18800044327
        'K;TOPSPPenalty_20201218081951\nH;100009;200009190005;71346;20201218;88123123133\nD;7820122965;22123131;1214400;20190218;31;6072\nD;7820122964;22123132;6679200;20190218;31;33396\n
        'K;TOPSPPenalty_20210118152340\nH;100026;200014200707;7674;20210118;2890009913\nD;7880029469;1400163615;3542000;20200604;1;7674;1\nH;100679;200014200714;4023;20210118;2890009914\nD;7820211746;0101438600;1856800;20200605;1;4023;3\n

        errorMessage = New StringBuilder()
        If cols.Length <> 6 Then
            writeError("Invalid Header Format")
        Else
            If cols(1).Trim = String.Empty Then
                writeError("DealerCode can't be empty")
            ElseIf cols(2).Trim = String.Empty Then
                writeError("RegNumber can't be empty")
            ElseIf cols(3).Trim = String.Empty Then
                writeError("AmountPenalty can't be empty")
            ElseIf cols(4).Trim = String.Empty Then
                writeError("DebitMemoDate can't be empty")
            ElseIf cols(5).Trim = String.Empty Then
                writeError("DebitMemoNumber can't be empty")
            Else
                Try
                    Dim _validation As Boolean = True
                    Dim vDealerCode As String = cols(1).Trim
                    Dim vRegNumber As String = cols(2).Trim
                    Dim vAmountPenalty As Decimal = CDec(cols(3).Trim)

                    Dim Thn As Integer = CInt(Strings.Left(cols(4).Trim, 4))
                    Dim Bln As Integer = CInt(cols(4).Trim.Substring(4, 2))
                    Dim Tgl As Integer = CInt(cols(4).Trim.Substring(6, 2))
                    Dim vDebitMemoDate As Date = New DateTime(Thn, Bln, Tgl)
                    Dim vDebitMemoNumber As String = cols(5).Trim

                    Dim oDealer As Dealer = New DealerFacade(user).Retrieve(vDealerCode)
                    If IsNothing(oDealer) OrElse (Not IsNothing(oDealer) And oDealer.ID = 0) Then
                        writeError("DealerCode doesn't exist in database")
                        _validation = False
                    End If
                    Dim oTOPSPTransferPayment As TOPSPTransferPayment = New TOPSPTransferPaymentFacade(user).Retrieve(vRegNumber)
                    If IsNothing(oTOPSPTransferPayment) Then
                        writeError("RegNumber doesn't exist in database")
                        _validation = False
                    End If

                    If _validation Then
                        objTOPSPPenaltyFacade = New TOPSPPenaltyFacade(user)
                        'objTOPSPPenalty = GetDataTOPSPPenalty(vDealerCode, vRegNumber, vDebitMemoNumber)
                        objTOPSPPenalty.Dealer = New DealerFacade(user).Retrieve(vDealerCode)
                        objTOPSPPenalty.TOPSPTransferPayment = New TOPSPTransferPaymentFacade(user).Retrieve(vRegNumber)
                        objTOPSPPenalty.Amount = vAmountPenalty
                        objTOPSPPenalty.DebitMemoDate = vDebitMemoDate
                        objTOPSPPenalty.DebitMemoNumber = vDebitMemoNumber
                        If objTOPSPPenalty.ID = 0 Then
                            objTOPSPPenalty.StatusPenalty = 0  '--Proses
                        End If
                    End If

                Catch ex As Exception
                    writeError("TOP SparePart Penalty error: " & ex.Message)
                End Try
            End If
        End If
        If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
            If IsNothing(objTOPSPPenalty) Then objTOPSPPenalty = New TOPSPPenalty()
            objTOPSPPenalty.ErrorMessage = errorMessage.ToString()
        End If

        Return objTOPSPPenalty
    End Function

    Private Function ParseDetail(ByVal line As String) As TOPSPPenaltyDetail
        Dim cols As String() = line.Split(MyBase.ColSeparator)

        _TOPSPPenaltyDetail = New TOPSPPenaltyDetail

        If (cols.Length <> 8) Then
            writeError("Invalid Detail Format")
        Else
            'D;BillingNumber;AccountingDocNo;ActualTransfer;ActualTransferDate(yyyymmdd);PenaltyDays;AmountPenalty;PaymentType
            'D;7820122964;22123132;6679200;20190218;31;33396;1\n

            '1. BillingNumber
            If cols(1).Trim = String.Empty Then
                writeError("Billing Number Can't Be Empty")
            Else
                Dim objSPBill As New SparePartBilling
                Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartBilling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(SparePartBilling), "BillingNumber", MatchType.Exact, cols(1).Trim))
                Dim arlList As ArrayList = New SparePartBillingFacade(user).Retrieve(crit)
                If Not IsNothing(arlList) AndAlso arlList.Count > 0 Then
                    objSPBill = CType(arlList(0), SparePartBilling)
                Else
                    writeError("Invalid Billing Number")
                End If
                _TOPSPPenaltyDetail.SparePartBilling = objSPBill
            End If

            '2. AccountingDocNo
            If cols(2).Trim = String.Empty Then
                writeError("AccountingDoc Number Can't Be Empty")
            Else
                _TOPSPPenaltyDetail.AccountingDocNo = cols(2).Trim
            End If

            '3. ActualTransfer
            If cols(3).Trim = String.Empty Then
                writeError("Actual Transfer Amount Can't Be Empty")
            Else
                Try
                    _TOPSPPenaltyDetail.ActualTransferAmount = CType(cols(3), Double)
                Catch ex As Exception
                    _TOPSPPenaltyDetail.ActualTransferAmount = 0
                    writeError("Invalid Actual Transfer Amount")
                End Try
            End If

            '4. ActualTransferDate(yyyymmdd)
            If cols(4).Trim() = String.Empty Then
                writeError("Actual Transfer Date Can't Be Empty")
            Else
                Dim sTemp As String = cols(4).Trim()
                If sTemp.Length > 0 Then
                    Try
                        Dim Thn As Integer = CInt(Strings.Left(sTemp, 4))
                        Dim Bln As Integer = CInt(sTemp.Substring(4, 2))
                        Dim Tgl As Integer = CInt(sTemp.Substring(6, 2))
                        _TOPSPPenaltyDetail.ActualTransferDate = New DateTime(Thn, Bln, Tgl)
                    Catch ex As Exception
                        writeError("Invalid Actual Transfer Date")
                    End Try
                Else
                    writeError("Invalid Actual Transfer Date")
                End If
            End If

            '5. PenaltyDays
            If cols(5).Trim = String.Empty Then
                writeError("Penalty Days Can't Be Empty")
            Else
                Try
                    _TOPSPPenaltyDetail.PenaltyDays = CType(cols(5), Integer)
                Catch ex As Exception
                    _TOPSPPenaltyDetail.PenaltyDays = 0
                    writeError("Invalid Penalty Days")
                End Try
            End If

            '6. AmountPenalty
            If cols(6).Trim = String.Empty Then
                writeError("Amount Penalty Can't Be Empty")
            Else
                Try
                    _TOPSPPenaltyDetail.AmountPenalty = CType(cols(6), Double)
                Catch ex As Exception
                    _TOPSPPenaltyDetail.AmountPenalty = 0
                    writeError("Invalid Amount Penalty")
                End Try
            End If

            '7. PaymentType
            If cols(7).Trim = String.Empty Then
                writeError("Payment Type Can't Be Empty")
            Else
                Try
                    Dim objSC As New StandardCode
                    Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumTOPSPPenalty.TipePembayaran"))
                    crit.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, CType(cols(7), Integer)))
                    Dim arlList As ArrayList = New StandardCodeFacade(user).Retrieve(crit)
                    If Not IsNothing(arlList) AndAlso arlList.Count > 0 Then
                        objSC = CType(arlList(0), StandardCode)
                    End If
                    If objSC.ID > 0 Then
                        _TOPSPPenaltyDetail.PaymentType = CType(cols(7), Integer)
                    Else
                        _TOPSPPenaltyDetail.PaymentType = 0
                        writeError("Invalid Payment Type")
                    End If
                Catch ex As Exception
                    _TOPSPPenaltyDetail.PaymentType = 0
                    writeError("Invalid Payment Type")
                End Try
            End If
        End If

        If Not IsNothing(errorMessage) Then
            _TOPSPPenaltyDetail.ErrorMessage = errorMessage.ToString()
        End If

        Return _TOPSPPenaltyDetail
    End Function

    Private Function GetDataTOPSPPenalty(ByVal vDealerCode As String, ByVal vRegNumber As String, ByVal vDebitMemoNo As String) As TOPSPPenalty
        Dim obj As New TOPSPPenalty
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(TOPSPPenalty), "Dealer.DealerCode", MatchType.Exact, vDealerCode))
        crit.opAnd(New Criteria(GetType(TOPSPPenalty), "TOPSPTransferPayment.RegNumber", MatchType.Exact, vRegNumber))
        crit.opAnd(New Criteria(GetType(TOPSPPenalty), "DebitMemoNumber", MatchType.Exact, vDebitMemoNo))
        Dim arlList As ArrayList = New TOPSPPenaltyFacade(user).Retrieve(crit)
        If Not IsNothing(arlList) AndAlso arlList.Count > 0 Then
            obj = CType(arlList(0), TOPSPPenalty)
        End If
        Return obj
    End Function

#End Region

End Class
