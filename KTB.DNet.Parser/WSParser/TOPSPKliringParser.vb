Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text.RegularExpressions
Imports KTB.DNet.Domain
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Sparepart
Imports System.Security.Principal
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade

Public Class TOPSPKliringParser
    Inherits AbstractParser

#Region "Private Variables"
    Private _Stream As StreamReader
    Private Grammar As Regex
    Private _fileName As String
    Private errorMessage As StringBuilder

    Private _TOPSPAccountingNos As ArrayList
    Private _TOPSPAccountingNo As TOPSPAccountingNo
#End Region

#Region "Constructors/Destructors/Finalizers"

    Public Sub New()
        Grammar = MyBase.GetGrammarParser()
    End Sub

#End Region
    'K;TOPSPKliring_[TimeStamps]\nH;RegNumber;TglKliring;AmountKliring;TRNo
    Protected Overrides Function DoParse(KeyName As String, Content As String) As Object
        Try
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String

            _TOPSPAccountingNos = New ArrayList()
            _TOPSPAccountingNo = Nothing

            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        _TOPSPAccountingNo = Nothing

                        errorMessage = New StringBuilder()
                        _TOPSPAccountingNo = ParseHeader(line)

                        If Not IsNothing(_TOPSPAccountingNo) Then
                            _TOPSPAccountingNos.Add(_TOPSPAccountingNo)
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "TOPSPKliringParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPKliringParser, BlockName)
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
            SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _TOPSPAccountingNos.Count.ToString() & " Data", "ws-worker", "TOPSPKliringParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPKliringParser, BlockName)
            SysLogParameter.LogErrorToSyslog("End : Log Error of TOPSPKliringParser", "ws-worker", "TOPSPKliringParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPKliringParser, BlockName)

            Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _TOPSPAccountingNos.Count.ToString() & " Data. Message : " & sMsg)
        End If

        Return _TOPSPAccountingNos
    End Function

    Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object

    End Function

    Protected Overrides Function DoTransaction() As Integer
        Dim objTOPSPAccountingNoFacade As TOPSPAccountingNoFacade
        Dim nError As Integer = 0
        Dim sMsg As String = ""

        For Each objTOPSPAccountingNo As TOPSPAccountingNo In _TOPSPAccountingNos
            Try

                If Not IsNothing(objTOPSPAccountingNo.ErrorMessage) AndAlso objTOPSPAccountingNo.ErrorMessage <> "" Then
                    nError += 1
                    sMsg = sMsg & objTOPSPAccountingNo.ErrorMessage.ToString() & ";"
                Else
                    objTOPSPAccountingNoFacade = New TOPSPAccountingNoFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    objTOPSPAccountingNoFacade.Insert(objTOPSPAccountingNo)
                End If
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "TOPSPKliringParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objTOPSPAccountingNo.TOPSPTransferPayment.RegNumber & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try
        Next

        If nError > 0 Then
            SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _TOPSPAccountingNos.Count.ToString(), "ws-worker", "TOPSPKliringParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
            SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "TOPSPKliringParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
            Dim e As Exception = New Exception(sMsg)
            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
        End If
        Return 0
    End Function

#Region "Private Methods"
    Private Sub writeError(str As String)
        errorMessage.Append(str & Chr(13) & Chr(10))
    End Sub

    Private Function ParseHeader(ByVal line As String) As TOPSPAccountingNo
        Dim cols As String() = line.Split(MyBase.ColSeparator)
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)

        Dim objTOPSPAccountingNo As New TOPSPAccountingNo
        Dim objTOPSPAccountingNoFacade As New TOPSPAccountingNoFacade(user)

        errorMessage = New StringBuilder()
        If cols.Length <> 5 Then
            writeError("Invalid Header Format")
        Else
            If cols(1).Trim = String.Empty Then
                writeError("Reg Number can't be empty")
            ElseIf cols(2).Trim = String.Empty Then
                writeError("Kliring Date can't be empty")
            ElseIf cols(3).Trim = String.Empty Then
                writeError("Kliring Amount can't be empty")
            ElseIf cols(4).Trim = String.Empty Then
                writeError("TR Number can't be empty")
            Else
                Try
                    Dim vRegNumber As String = cols(1).Trim
                    If SudahAdaData(vRegNumber) Then
                        Return Nothing
                    End If
                    Dim Thn As Integer = CInt(Strings.Left(cols(2).Trim, 4))
                    Dim Bln As Integer = CInt(cols(2).Trim.Substring(4, 2))
                    Dim Tgl As Integer = CInt(cols(2).Trim.Substring(6, 2))
                    Dim vKliringDate As Date = New DateTime(Thn, Bln, Tgl)

                    Dim vKliringAmount As Decimal = CDec(cols(3).Trim)
                    Dim vTRNo As String = cols(4).Trim

                    objTOPSPAccountingNoFacade = New TOPSPAccountingNoFacade(user)

                    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TOPSPTransferPayment), "RegNumber", MatchType.Exact, vRegNumber))
                    Dim arlTOPSPTransferPayment As ArrayList = New TOPSPTransferPaymentFacade(user).Retrieve(criterias)

                    If arlTOPSPTransferPayment.Count > 0 Then
                        objTOPSPAccountingNo.TOPSPTransferPayment = arlTOPSPTransferPayment(0)
                        objTOPSPAccountingNo.KliringDate = vKliringDate
                        objTOPSPAccountingNo.KliringAmount = vKliringAmount
                        objTOPSPAccountingNo.TRNo = vTRNo
                    Else
                        writeError("There is no TOP Transfer Payment with Regnumber : " & vRegNumber & Environment.NewLine)
                    End If
                Catch ex As Exception
                    writeError("TOP Kliring error: " & ex.Message)
                End Try
            End If

            If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                If IsNothing(objTOPSPAccountingNo) Then objTOPSPAccountingNo = New TOPSPAccountingNo()
                objTOPSPAccountingNo.ErrorMessage = errorMessage.ToString()
            End If
        End If

        Return objTOPSPAccountingNo
    End Function

#End Region

    Private Function SudahAdaData(vRegNumber As String) As Boolean
        Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
        Dim topSPAccNo As TOPSPAccountingNo = New TOPSPAccountingNoFacade(user).Retrieve(vRegNumber)
        If topSPAccNo.ID > 0 Then
            Return True
        End If
        Return False
    End Function

End Class
