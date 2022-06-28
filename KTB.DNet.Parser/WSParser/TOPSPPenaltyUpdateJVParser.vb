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

Public Class TOPSPPenaltyUpdateJVParser
    Inherits AbstractParser

#Region "Private Variables"
    Private _Stream As StreamReader
    Private Grammar As Regex
    Private _fileName As String
    Private errorMessage As StringBuilder

    Private _arlTOPSPPenaltyUpdateJVs As ArrayList
    Private _TOPSPPenaltyUpdateJV As TOPSPPenalty
#End Region

#Region "Constructors/Destructors/Finalizers"

    Public Sub New()
        Grammar = MyBase.GetGrammarParser()
    End Sub

#End Region

    'WS SAP to D-Net : Update JV Number Pengembalian PPh
    'K;TOPSPPENALTYJV_[TimeStamp]\nH;NoRegPengembalianPPh;JVNumber\n
    Protected Overrides Function DoParse(KeyName As String, Content As String) As Object
        Try
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String

            _arlTOPSPPenaltyUpdateJVs = New ArrayList()
            _TOPSPPenaltyUpdateJV = Nothing

            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        _TOPSPPenaltyUpdateJV = Nothing

                        errorMessage = New StringBuilder()
                        _TOPSPPenaltyUpdateJV = ParseHeader(line)

                        If Not IsNothing(_TOPSPPenaltyUpdateJV) Then
                            _arlTOPSPPenaltyUpdateJVs.Add(_TOPSPPenaltyUpdateJV)
                        End If
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "TOPSPPenaltyUpdateJVParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPPenaltyUpdateJVParser, BlockName)
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
            SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _arlTOPSPPenaltyUpdateJVs.Count.ToString() & " Data", "ws-worker", "TOPSPPenaltyUpdateJVParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPPenaltyUpdateJVParser, BlockName)
            SysLogParameter.LogErrorToSyslog("End : Log Error of TOPSPPenaltyUpdateJVParser", "ws-worker", "TOPSPPenaltyUpdateJVParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPPenaltyUpdateJVParser, BlockName)

            Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _arlTOPSPPenaltyUpdateJVs.Count.ToString() & " Data. Message : " & sMsg)
        End If

        Return _arlTOPSPPenaltyUpdateJVs
    End Function

    Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
        Return Nothing
    End Function

    Protected Overrides Function DoTransaction() As Integer
        Dim objTOPSPPenaltyFacade As TOPSPPenaltyFacade
        Dim nError As Integer = 0
        Dim sMsg As String = ""

        For Each objTOPSPPenaltyUpdateJV As TOPSPPenalty In _arlTOPSPPenaltyUpdateJVs
            Try
                If Not IsNothing(objTOPSPPenaltyUpdateJV.ErrorMessage) AndAlso objTOPSPPenaltyUpdateJV.ErrorMessage <> "" Then
                    nError += 1
                    sMsg = sMsg & objTOPSPPenaltyUpdateJV.ErrorMessage.ToString() & ";"
                Else
                    objTOPSPPenaltyFacade = New TOPSPPenaltyFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS_TOPSPPENALTYJV"), Nothing))
                    objTOPSPPenaltyFacade.Update(objTOPSPPenaltyUpdateJV)
                End If

            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "TOPSPPenaltyUpdateJVParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objTOPSPPenaltyUpdateJV.TOPSPTransferPayment.RegNumber & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try
        Next

        If nError > 0 Then
            SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arlTOPSPPenaltyUpdateJVs.Count.ToString(), "ws-worker", "TOPSPPenaltyUpdateJVParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPPenaltyUpdateJVParser, BlockName)
            SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "TOPSPPenaltyUpdateJVParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPPenaltyUpdateJVParser, BlockName)
            Dim e As Exception = New Exception(sMsg)
            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
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

        Dim objTOPSPPenaltyUpdateJV As New TOPSPPenalty
        Dim objTOPSPPenaltyFacade As New TOPSPPenaltyFacade(user)

        'K;TOPSPPENALTYJV_[TimeStamp]\nH;NoRegPengembalianPPh;JVNumber\n        
        'K;TOPSPPENALTYJV_[202005180600]\nH;NoRegPengembalianPPh;JVNumber\n
        errorMessage = New StringBuilder()
        If cols.Length <> 3 Then
            writeError("Invalid Header Format")
        Else
            If cols(1).Trim = String.Empty Then
                writeError("NoRegPengembalianPPh can't be empty")
            ElseIf cols(2).Trim = String.Empty Then
                writeError("JVNumber can't be empty")
            Else
                Try
                    Dim vNoRegPengembalianPPh As String = cols(1).Trim
                    Dim vJVNumber As String = cols(2).Trim
                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(TOPSPPenalty), "NoRegPengembalian", MatchType.Exact, vNoRegPengembalianPPh))
                    Dim arlList As ArrayList = New TOPSPPenaltyFacade(user).Retrieve(crit)
                    If Not IsNothing(arlList) AndAlso arlList.Count > 0 Then
                        objTOPSPPenaltyUpdateJV = CType(arlList(0), TOPSPPenalty)
                        objTOPSPPenaltyUpdateJV.NoRegPengembalian = vNoRegPengembalianPPh
                        objTOPSPPenaltyUpdateJV.JVNumber = vJVNumber
                        objTOPSPPenaltyUpdateJV.StatusPengembalian = 3  '-- Selesai
                    End If

                Catch ex As Exception
                    writeError("Update JV Number error: " & ex.Message)
                End Try
            End If

            If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                If IsNothing(objTOPSPPenaltyUpdateJV) Then objTOPSPPenaltyUpdateJV = New TOPSPPenalty()
                objTOPSPPenaltyUpdateJV.ErrorMessage = errorMessage.ToString()
            End If
        End If

        Return objTOPSPPenaltyUpdateJV
    End Function


#End Region

End Class
