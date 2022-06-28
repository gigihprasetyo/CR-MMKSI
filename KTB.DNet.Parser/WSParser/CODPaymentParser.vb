Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text.RegularExpressions
Imports KTB.DNet.Domain
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade
Imports System.Security.Principal
Imports KTB.DNet.Domain.Search

Public Class CODPaymentParser
    Inherits AbstractParser

#Region "Private Variables"
    Private _Stream As StreamReader
    Private Grammar As Regex
    Private _fileName As String
    Private errorMessage As StringBuilder

    Private _CODPayments As ArrayList
    Private _CODPayment As CODPayment
#End Region

#Region "Constructors/Destructors/Finalizers"

    Public Sub New()
        Grammar = MyBase.GetGrammarParser()
    End Sub

#End Region

    'K: CODPaymentList_[Timestamp]\nH;DealerCode;SalesOrderNo;DeliveryNo;OrderType;SODate(yyyymmdd);RetailAmount;DepositC2Amount;PPNAmount;Total;RODeposit
    Protected Overrides Function DoParse(KeyName As String, Content As String) As Object
        Try
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String

            _CODPayments = New ArrayList()
            _CODPayment = Nothing

            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        _CODPayment = Nothing

                        errorMessage = New StringBuilder()
                        _CODPayment = ParseHeader(line)

                        If Not IsNothing(_CODPayment) Then
                            _CODPayments.Add(_CODPayment)
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "CODPaymentParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.CODPaymentParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

        Catch ex As Exception
            Throw ex
        Finally

        End Try

        Dim aDatas As New ArrayList
        Dim nError As Integer = 0
        Dim sMsg As String = ""

        If nError > 0 Then
            SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _CODPayments.Count.ToString() & " Data", "ws-worker", "CODPaymentParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.CODPaymentParser, BlockName)
            SysLogParameter.LogErrorToSyslog("End : Log Error of CODPaymentParser", "ws-worker", "CODPaymentParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.CODPaymentParser, BlockName)

            Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _CODPayments.Count.ToString() & " Data. Message : " & sMsg)
        End If

        Return _CODPayments
    End Function

    Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
        Return Nothing
    End Function

    Protected Overrides Function DoTransaction() As Integer
        Dim objCODPaymentFacade As CODPaymentFacade
        Dim nError As Integer = 0
        Dim sMsg As String = ""

        DeleteData()

        For Each objCODPayment As CODPayment In _CODPayments
            Try

                If Not IsNothing(objCODPayment.ErrorMessage) AndAlso objCODPayment.ErrorMessage <> "" Then
                    nError += 1
                    sMsg = sMsg & objCODPayment.ErrorMessage.ToString() & ";"
                Else
                    objCODPaymentFacade = New CODPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    objCODPaymentFacade.Insert(objCODPayment)
                End If
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "CODPaymentParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objCODPayment.DeliveryNo & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try
        Next

        If nError > 0 Then
            SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _CODPayments.Count.ToString(), "ws-worker", "CODPaymentParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.CODPaymentParser, BlockName)
            SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "CODPaymentParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.CODPaymentParser, BlockName)
            Dim e As Exception = New Exception(sMsg)
            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
        End If
        Return 0
    End Function

#Region "Private Methods"
    Private Sub writeError(str As String)
        errorMessage.Append(str & Chr(13) & Chr(10))
    End Sub

    Private Function ParseHeader(ByVal line As String) As CODPayment
        Dim cols As String() = line.Split(MyBase.ColSeparator)
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)

        Dim objCODPayment As New CODPayment
        Dim objCODPaymentFacade As New CODPaymentFacade(user)

        'K: CODPaymentList_[Timestamp]\nH;DealerCode;SalesOrderNo;DeliveryNo;OrderType;SODate(yyyymmdd);RetailAmount;DepositC2Amount;PPNAmount;Total;RODeposit

        errorMessage = New StringBuilder()
        If cols.Length <> 11 Then
            writeError("Invalid Header Format")
        Else
            If cols(1).Trim = String.Empty Then
                writeError("DealerCode can't be empty")
            ElseIf cols(2).Trim = String.Empty Then
                writeError("SalesOrderNo can't be empty")
            ElseIf cols(3).Trim = String.Empty Then
                writeError("DeliveryNo can't be empty")
            ElseIf cols(4).Trim = String.Empty Then
                writeError("OrderType can't be empty")
            ElseIf cols(5).Trim = String.Empty Then
                writeError("SODate can't be empty")
            ElseIf cols(6).Trim = String.Empty Then
                writeError("RetailAmount can't be empty")
            ElseIf cols(7).Trim = String.Empty Then
                writeError("DepositC2Amount can't be empty")
            ElseIf cols(8).Trim = String.Empty Then
                writeError("PPNAmount can't be empty")
            ElseIf cols(9).Trim = String.Empty Then
                writeError("Total can't be empty")
            ElseIf cols(10).Trim = String.Empty Then
                writeError("RODeposit can't be empty")
            Else
                Try
                    Dim vDealerCode As String = cols(1).Trim
                    Dim vSalesOrderNo As String = cols(2).Trim
                    Dim vDeliveryNo As String = cols(3).Trim
                    Dim vOrderType As String = cols(4).Trim

                    Dim Thn As Integer = CInt(Strings.Left(cols(5).Trim, 4))
                    Dim Bln As Integer = CInt(cols(5).Trim.Substring(4, 2))
                    Dim Tgl As Integer = CInt(cols(5).Trim.Substring(6, 2))
                    Dim vSODate As Date = New DateTime(Thn, Bln, Tgl)

                    Dim vRetailAmount As Decimal = CDec(cols(6).Trim)
                    Dim vDepositC2Amount As Decimal = CDec(cols(7).Trim)
                    Dim vPPNAmount As Decimal = CDec(cols(8).Trim)
                    Dim vTotal As Decimal = CDec(cols(9).Trim)
                    Dim vRODeposit As Decimal = CDec(cols(10).Trim)


                    objCODPaymentFacade = New CODPaymentFacade(user)
                    objCODPayment.DealerCode = vDealerCode
                    objCODPayment.SalesOrderNo = vSalesOrderNo
                    objCODPayment.DeliveryNo = vDeliveryNo
                    objCODPayment.OrderType = vOrderType
                    objCODPayment.SODate = vSODate
                    objCODPayment.RetailAmount = vRetailAmount
                    objCODPayment.PPNAmount = vPPNAmount
                    objCODPayment.DepositC2Amount = vDepositC2Amount
                    objCODPayment.Total = vTotal
                    objCODPayment.RODeposit = vRODeposit

                Catch ex As Exception
                    writeError("COD Outstanding error: " & ex.Message)
                End Try
            End If

            If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                If IsNothing(objCODPayment) Then objCODPayment = New CODPayment()
                objCODPayment.ErrorMessage = errorMessage.ToString()
            End If
        End If

        Return objCODPayment
    End Function

    Private Sub DeleteData()
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CODPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlList As ArrayList = New CODPaymentFacade(user).Retrieve(crit)
        For Each item As CODPayment In arlList
            item.RowStatus = CType(DBRowStatus.Deleted, Short)
            Dim i = New CODPaymentFacade(user).Update(item)
        Next
    End Sub

#End Region

End Class
