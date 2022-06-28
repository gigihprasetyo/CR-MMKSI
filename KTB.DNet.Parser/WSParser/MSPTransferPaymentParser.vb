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
    Public Class MSPTransferPaymentParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objMSPTrfPaymentHistory As MSPTransferPaymentHistory
        Private _arrMSPTrfPaymentHistory As ArrayList
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
                _arrMSPTrfPaymentHistory = New ArrayList()
                objMSPTrfPaymentHistory = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try

                        errorMessage = New StringBuilder()
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            objMSPTrfPaymentHistory = ParseHeader(line)
                        ElseIf ind = MyBase.IndicatorDetail Then
                            objMSPTrfPaymentHistory = ParseHeader(line, False)

                            ' insert to array objek MSP TrfPayment
                            If Not IsNothing(objMSPTrfPaymentHistory) Then
                                If objMSPTrfPaymentHistory.ErrorMessage = String.Empty Then
                                    Dim newObjMSPTrfPaymentHistory As New MSPTransferPaymentHistory
                                    newObjMSPTrfPaymentHistory.MSPTransferPayment = objMSPTrfPaymentHistory.MSPTransferPayment
                                    newObjMSPTrfPaymentHistory.TransferDate = objMSPTrfPaymentHistory.TransferDate
                                    newObjMSPTrfPaymentHistory.Amount = objMSPTrfPaymentHistory.Amount
                                    _arrMSPTrfPaymentHistory.Add(newObjMSPTrfPaymentHistory)
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MSPTransferPaymentParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPTransferPayment, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        objMSPTrfPaymentHistory = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrMSPTrfPaymentHistory
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facMSPMaster As New MSPMasterFacade(user)
            For Each itemNew As MSPTransferPaymentHistory In _arrMSPTrfPaymentHistory
                Try
                    Dim vResult As Integer
                    Dim _facMSPTrfPaymentHistory As MSPTransferPaymentHistoryFacade = New MSPTransferPaymentHistoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPTransferPaymentHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crt.opAnd(New Criteria(GetType(MSPTransferPaymentHistory), "MSPTransferPayment.RegNumber", MatchType.Exact, itemNew.MSPTransferPayment.RegNumber))

                    ' get history tranfer based on Payment Registration number
                    Dim totalAmountTrf As Decimal = 0
                    Dim arrObjOld As ArrayList = _facMSPTrfPaymentHistory.Retrieve(crt)
                    If arrObjOld.Count > 0 Then
                        For Each itemHistory As MSPTransferPaymentHistory In arrObjOld
                            ' get total yang sudah ditransfer
                            totalAmountTrf += itemHistory.Amount
                        Next
                    End If

                    ' add history amount transfer dengan new amount transfer
                    totalAmountTrf += itemNew.Amount

                    Dim crtTrfPayment As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtTrfPayment.opAnd(New Criteria(GetType(MSPTransferPayment), "RegNumber", MatchType.Exact, itemNew.MSPTransferPayment.RegNumber))

                    Dim arrObjMSPTrfPayment As ArrayList = New MSPTransferPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(crtTrfPayment)

                    If arrObjMSPTrfPayment.Count > 0 Then
                        Dim objMSPTrfPayment As MSPTransferPayment = CType(arrObjMSPTrfPayment(0), MSPTransferPayment)
                        'If objMSPTrfPayment.TotalAmount >= totalAmountTrf Then
                        ' insert history payment
                        Dim objNew As New MSPTransferPaymentHistory
                        objNew.MSPTransferPayment = itemNew.MSPTransferPayment
                        objNew.Amount = itemNew.Amount
                        objNew.TransferDate = itemNew.TransferDate

                        vResult = New MSPTransferPaymentHistoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(objNew)

                        If vResult > 0 Then
                            objMSPTrfPayment.ActualTransferDate = itemNew.TransferDate
                            objMSPTrfPayment.TotalActualAmount = totalAmountTrf

                            If objMSPTrfPayment.TotalAmount <= totalAmountTrf Then
                                objMSPTrfPayment.Status = EnumStatusMSP.Status.Selesai
                            End If

                            vResult = New MSPTransferPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Update(objMSPTrfPayment)
                            If vResult = -1 Then
                                Throw New Exception("Gagal update status MSP Transfer payment.")
                            End If
                        Else
                            Throw New Exception("Gagal insert history pembayaran MSP.")
                        End If
                        'Else
                        '    ' jika jumlah transfer melebihi dari total amount yang dibuat maka set to error
                        '    Throw New Exception("Data Transfer Payment Registration Number " & itemNew.MSPTransferPayment.RegNumber & " & transfer date " & itemNew.TransferDate & " dijumlahkan dengan history tranfer melebihi dari total amount yang harus dibayar.")
                        'End If
                    Else
                        Throw New Exception("Data MSP Transfer Payment tidak ada dengan Payment Registration Number " & itemNew.MSPTransferPayment.RegNumber & ".")
                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrMSPTrfPaymentHistory.Count.ToString(), "ws-worker", "MSPTransferPaymentParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPMasterParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MSPTransferPaymentParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPTransferPayment, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function
#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String, Optional ByVal IsHeader As Boolean = True) As MSPTransferPaymentHistory
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            errorMessage = New StringBuilder()

            If IsHeader Then
                If cols.Length <> 4 Then
                    errorMessage.Append("Invalid Header Format" & Chr(13) & Chr(10))
                Else
                    'RegNumber
                    If cols(1).Trim = String.Empty Then
                        errorMessage.Append("Reg Number Empty" & Chr(13) & Chr(10))
                    Else
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(MSPTransferPayment), "RegNumber", MatchType.Exact, cols(1).Trim))
                        crt.opAnd(New Criteria(GetType(MSPTransferPayment), "Status", MatchType.Exact, CInt(EnumStatusMSP.Status.Proses)))
                        Dim arr As ArrayList = New MSPTransferPaymentFacade(user).Retrieve(crt)

                        Dim objMSPTransferPayment As MSPTransferPayment = Nothing
                        If arr.Count > 0 Then
                            objMSPTransferPayment = CType(arr(0), MSPTransferPayment)
                        End If

                        If (IsNothing(objMSPTransferPayment)) Then
                            errorMessage.Append("Reg Number is not Found!" & Chr(13) & Chr(10))
                        Else
                            objMSPTrfPaymentHistory = New MSPTransferPaymentHistory
                            objMSPTrfPaymentHistory.MSPTransferPayment = objMSPTransferPayment
                        End If
                    End If

                    'Actual TransferDate
                    If cols(3).Trim() = String.Empty Then
                        errorMessage.Append("Actual Transfer Date" & Chr(13) & Chr(10))
                    Else
                        Dim sTemp As String = cols(3).Trim()
                        If sTemp.Length > 0 Then
                            Try
                                Dim Thn As Integer = CInt(Strings.Left(sTemp, 4))
                                Dim Bln As Integer = CInt(sTemp.Substring(4, 2))
                                Dim Tgl As Integer = CInt(sTemp.Substring(6, 2))
                                objMSPTrfPaymentHistory.TransferDate = New DateTime(Thn, Bln, Tgl)
                            Catch ex As Exception
                                errorMessage.Append("Invalid ActualTrfDate" & Chr(13) & Chr(10))
                            End Try

                        Else
                            errorMessage.Append("Invalid ActualTrfDate" & Chr(13) & Chr(10))
                        End If
                    End If

                End If
            Else
                ' get detail of txt file
                If (cols.Length <> 3) Then
                    errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
                Else

                    If cols(1).Trim = String.Empty Then
                        errorMessage.Append("Debit Charge No Empty" & Chr(13) & Chr(10))
                    Else
                        Dim crtSO As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPDC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crtSO.opAnd(New Criteria(GetType(MSPDC), "DebitChargeNo", MatchType.Exact, cols(1).Trim))
                        If (New MSPDCFacade(user).Retrieve(crtSO).Count = 0) Then
                            errorMessage.Append("Debit Charge No is not Found!" & Chr(13) & Chr(10))
                        End If

                    End If

                    If cols(2).Trim = String.Empty Then
                        errorMessage.Append("Actual Amount Empty" & Chr(13) & Chr(10))
                    Else
                        Dim sTemp As String = cols(2).Trim
                        If sTemp.Trim.Length > 0 Then
                            If IsNumeric(sTemp.Trim) Then
                                objMSPTrfPaymentHistory.Amount = CDec(sTemp.Trim)
                            Else
                                errorMessage.Append("Invalid TotalActualAmount" & Chr(13) & Chr(10))
                            End If
                        Else
                            errorMessage.Append("Invalid TotalActualAmount" & Chr(13) & Chr(10))
                        End If
                    End If

                End If
            End If

            objMSPTrfPaymentHistory.ErrorMessage = errorMessage.ToString()
            Return objMSPTrfPaymentHistory
        End Function

#End Region

    End Class
End Namespace