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
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

Namespace KTB.DNet.Parser
    Public Class MSPExtPaymentParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objMSPExPayment As MSPExPayment
        Private _arrMSPExPayment As ArrayList
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

                _arrMSPExPayment = New ArrayList()
                objMSPExPayment = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek MSPExMaster
                            objMSPExPayment = ParseHeader(line)
                            ' insert to array objek MSPExMaster
                            If Not IsNothing(objMSPExPayment) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objMSPExPayment.ErrorMessage = errorMessage.ToString()
                                _arrMSPExPayment.Add(objMSPExPayment)
                                objMSPExPayment = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MSPExMasterParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExMasterParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        objMSPExPayment = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrMSPExPayment
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facMSPExPayment As New MSPExPaymentFacade(user)
            Dim facMSPExRegistration As New MSPExRegistrationFacade(user)
            Dim result As Integer = -1
            For Each objMSPExPayment As MSPExPayment In _arrMSPExPayment
                Try
                    If objMSPExPayment.ID <> 0 Then
                        Dim oldStatus As Short = objMSPExPayment.Status
                        objMSPExPayment.Status = EnumMSPEx.MSPExStatusPayment.Selesai
                        result = facMSPExPayment.Update(objMSPExPayment)
                        Dim hisRes As Integer = New StatusChangeHistoryFacade(user).Insert(22, objMSPExPayment.RegNumber, oldStatus, objMSPExPayment.Status)
                    Else
                        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPayment), "RowStatus", MatchType.Exact, 0))
                        crit.opAnd(New Criteria(GetType(MSPExPayment), "RegNumber", MatchType.Exact, objMSPExPayment.RegNumber))
                        Dim tempObj As MSPExPayment = facMSPExPayment.Retrieve(crit)(0)
                        tempObj.ClearingTotalAmount = objMSPExPayment.ClearingTotalAmount
                        tempObj.ClearingDate = objMSPExPayment.ClearingDate
                        tempObj.TRNumber = objMSPExPayment.TRNumber
                        tempObj.Status = EnumMSPEx.MSPExStatusPayment.Selesai
                        result = facMSPExPayment.Update(tempObj)
                        Dim hisRes As Integer = New StatusChangeHistoryFacade(user).Insert(22, tempObj.RegNumber, objMSPExPayment.Status, tempObj.Status)
                    End If
                    If result < 1 Then
                        Throw New Exception("Gagal Update MSP Extended Payment")
                    End If

                    Dim strSQL As String = "select a.MSPExRegistrationID from MSPExPaymentDetail a join mspexpayment b on b.id=a.MSPExPaymentID where b.RegNumber='" & objMSPExPayment.RegNumber & "'"
                    Dim crit1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, 0))
                    crit1.opAnd(New Criteria(GetType(MSPExRegistration), "ID", MatchType.InSet, "(" & strSQL & ")"))
                    Dim arrMSPExReg As ArrayList = facMSPExRegistration.Retrieve(crit1)
                    If arrMSPExReg.Count > 0 Then
                        For Each item As MSPExRegistration In arrMSPExReg
                            Dim oldStatus As Short = item.Status
                            item.Status = EnumMSPEx.MSPExStatus.Selesai
                            result = New MSPExRegistrationFacade(user).Update(item)
                            If result < 1 Then
                                Throw New Exception("Gagal Update MSP Extended Registration")
                            End If

                            Dim hisRes As Integer = New StatusChangeHistoryFacade(user).Insert(1, item.RegNumber, oldStatus, item.Status)
                        Next
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrMSPExPayment.Count.ToString(), "ws-worker", "MSPExtPaymentParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExMasterParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MSPExtPaymentParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPExMasterParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object

        End Function
#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As MSPExPayment
            'H;PaymentRegNumber;Total_Amount;Tgl_Clearing[DDMMYYYY]; TR_Number\n
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim _objMSPExPayment As New MSPExPayment

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 RegNumber
                Dim regNumber As String = cols(1).Trim
                If regNumber = String.Empty Then
                    Throw New Exception("Invalid RegNumber")
                Else
                    Try
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(MSPExPayment), "RegNumber", MatchType.Exact, regNumber))
                        Dim arrObj As ArrayList = New MSPExPaymentFacade(user).Retrieve(crt)
                        If arrObj.Count > 0 Then
                            _objMSPExPayment = CType(arrObj(0), MSPExPayment)
                        Else
                            Throw New Exception("Invalid RegNumber")
                        End If
                    Catch ex As Exception
                        writeError("RegNumber  error: " & ex.Message)
                    End Try
                End If

                '2 Clearing total amount
                _objMSPExPayment.ClearingTotalAmount = CDec(cols(2).Trim)

                '3 clearingDate
                Try
                    _objMSPExPayment.ClearingDate = GetShortDate(cols(3))
                Catch ex As Exception
                    errorMessage.Append("Invalid Start Date")
                End Try

                '4 TRNumber
                _objMSPExPayment.TRNumber = cols(4).Trim





                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    _objMSPExPayment.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    _objMSPExPayment.LastUpdateBy = "WS"
                End If
            End If

            Return _objMSPExPayment
        End Function

        Protected Function GetShortDate(ByVal str As String) As Date
            Dim dt As Date 'ddMMYYYY

            Try
                dt = New Date(Integer.Parse(str.Substring(4, 4)), Integer.Parse(str.Substring(2, 2)), Integer.Parse(str.Substring(0, 2)))
            Catch ex As Exception
                dt = DateSerial(1900, 1, 1)
            End Try
            Return dt
        End Function

#End Region

    End Class
End Namespace
