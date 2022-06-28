Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text.RegularExpressions
Imports KTB.DNet.Domain
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade
Imports System.Security.Principal
Imports KTB.DNet.Domain.Search

Public Class CODOutstandingParser
    Inherits AbstractParser

#Region "Private Variables"
    Private _Stream As StreamReader
    Private Grammar As Regex
    Private _fileName As String
    Private errorMessage As StringBuilder

    Private _CODOutstandings As ArrayList
    Private _CODOutstanding As CODOutstanding
#End Region

#Region "Constructors/Destructors/Finalizers"

    Public Sub New()
        Grammar = MyBase.GetGrammarParser()
    End Sub

#End Region

    'K: CODOutstanding_[Timestamp]\nH;DealerCode;TipePembayaran;NomorDO;NomorBilling;TanggalBilling(yyyymmdd);TanggalPembuatan(yyyymmdd);NetAmount;TaxAmount;C2Amount;Total
    Protected Overrides Function DoParse(KeyName As String, Content As String) As Object
        Try
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String

            _CODOutstandings = New ArrayList()
            _CODOutstanding = Nothing

            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        _CODOutstanding = Nothing

                        errorMessage = New StringBuilder()
                        _CODOutstanding = ParseHeader(line)

                        If Not IsNothing(_CODOutstanding) Then
                            _CODOutstandings.Add(_CODOutstanding)
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "CODOutstandingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.CODOutstandingParser, BlockName)
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
            SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _CODOutstandings.Count.ToString() & " Data", "ws-worker", "CODOutstandingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.CODOutstandingParser, BlockName)
            SysLogParameter.LogErrorToSyslog("End : Log Error of CODOutstandingParser", "ws-worker", "CODOutstandingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.CODOutstandingParser, BlockName)

            Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _CODOutstandings.Count.ToString() & " Data. Message : " & sMsg)
        End If

        Return _CODOutstandings
    End Function

    Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
        Return Nothing
    End Function

    Protected Overrides Function DoTransaction() As Integer
        Dim objCODOutstandingFacade As CODOutstandingFacade
        Dim nError As Integer = 0
        Dim sMsg As String = ""

        DeleteData()

        For Each objCODOutstanding As CODOutstanding In _CODOutstandings
            Try

                If Not IsNothing(objCODOutstanding.ErrorMessage) AndAlso objCODOutstanding.ErrorMessage <> "" Then
                    nError += 1
                    sMsg = sMsg & objCODOutstanding.ErrorMessage.ToString() & ";"
                Else
                    objCODOutstandingFacade = New CODOutstandingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    objCODOutstandingFacade.Insert(objCODOutstanding)
                End If
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "CODOutstandingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objCODOutstanding.BillingNumber & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try
        Next

        If nError > 0 Then
            SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _CODOutstandings.Count.ToString(), "ws-worker", "CODOutstandingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.CODOutstandingParser, BlockName)
            SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "CODOutstandingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.CODOutstandingParser, BlockName)
            Dim e As Exception = New Exception(sMsg)
            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
        End If
        Return 0
    End Function

#Region "Private Methods"
    Private Sub writeError(str As String)
        errorMessage.Append(str & Chr(13) & Chr(10))
    End Sub

    Private Function ParseHeader(ByVal line As String) As CODOutstanding
        Dim cols As String() = line.Split(MyBase.ColSeparator)
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)

        Dim objCODOutstanding As New CODOutstanding
        Dim objCODOutstandingFacade As New CODOutstandingFacade(user)

        errorMessage = New StringBuilder()
        If cols.Length <> 11 Then
            writeError("Invalid Header Format")
        Else
            If cols(1).Trim = String.Empty Then
                writeError("DealerCode can't be empty")
            ElseIf cols(2).Trim = String.Empty Then
                writeError("TipePembayaran can't be empty")
            ElseIf cols(3).Trim = String.Empty Then
                writeError("NomorDO can't be empty")
            ElseIf cols(4).Trim = String.Empty Then
                writeError("NomorBilling can't be empty")
            ElseIf cols(5).Trim = String.Empty Then
                writeError("TanggalBilling can't be empty")
            ElseIf cols(6).Trim = String.Empty Then
                writeError("TanggalPembuatan can't be empty")
            ElseIf cols(7).Trim = String.Empty Then
                writeError("NetAmount can't be empty")
            ElseIf cols(8).Trim = String.Empty Then
                writeError("TaxAmount can't be empty")
            ElseIf cols(9).Trim = String.Empty Then
                writeError("C2Amount can't be empty")
            ElseIf cols(10).Trim = String.Empty Then
                writeError("Total can't be empty")
            Else
                Try
                    Dim vDealerCode As String = cols(1).Trim
                    Dim vTipePembayaran As String = cols(2).Trim
                    Dim vNomorDO As String = cols(3).Trim
                    Dim vNomorBilling As String = cols(4).Trim

                    Dim Thn As Integer = CInt(Strings.Left(cols(5).Trim, 4))
                    Dim Bln As Integer = CInt(cols(5).Trim.Substring(4, 2))
                    Dim Tgl As Integer = CInt(cols(5).Trim.Substring(6, 2))
                    Dim vTanggalBilling As Date = New DateTime(Thn, Bln, Tgl)

                    Dim ThnTP As Integer = CInt(Strings.Left(cols(6).Trim, 4))
                    Dim BlnTP As Integer = CInt(cols(6).Trim.Substring(4, 2))
                    Dim TglTP As Integer = CInt(cols(6).Trim.Substring(6, 2))
                    Dim vTanggalPembuatan As Date = New DateTime(Thn, Bln, Tgl)

                    Dim vNetAmount As Decimal = CDec(cols(7).Trim)
                    Dim vTaxAmount As Decimal = CDec(cols(8).Trim)
                    Dim vC2Amount As Decimal = CDec(cols(9).Trim)
                    Dim vTotal As Decimal = CDec(cols(10).Trim)


                    objCODOutstandingFacade = New CODOutstandingFacade(user)
                    objCODOutstanding.DealerCode = vDealerCode
                    objCODOutstanding.PaymentType = vTipePembayaran
                    objCODOutstanding.DONumber = vNomorDO
                    objCODOutstanding.BillingNumber = vNomorBilling
                    objCODOutstanding.BIllingDate = vTanggalBilling
                    objCODOutstanding.BillingCreateDate = vTanggalPembuatan
                    objCODOutstanding.NetAmount = vNetAmount
                    objCODOutstanding.TaxAmount = vTaxAmount
                    objCODOutstanding.C2Amount = vC2Amount
                    objCODOutstanding.Total = vTotal

                Catch ex As Exception
                    writeError("COD Outstanding error: " & ex.Message)
                End Try
            End If

            If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                If IsNothing(objCODOutstanding) Then objCODOutstanding = New CODOutstanding()
                objCODOutstanding.ErrorMessage = errorMessage.ToString()
            End If
        End If

        Return objCODOutstanding
    End Function

    Private Sub DeleteData()
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CODOutstanding), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlList As ArrayList = New CODOutstandingFacade(user).Retrieve(crit)
        For Each item As CODOutstanding In arlList
            item.RowStatus = CType(DBRowStatus.Deleted, Short)
            Dim i = New CODOutstandingFacade(user).Update(item)
        Next
    End Sub

#End Region

End Class
