#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.PO

#End Region

Namespace KTB.DNet.Parser
    Public Class TOPSPPaymentOutstandingParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objTOPSPTransferOutstanding As TOPSPTransferOutstanding
        Private _arrTOPSPTransferOutstanding As ArrayList
#End Region


#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region


        Protected Overloads Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _arrTOPSPTransferOutstanding = New ArrayList()
                objTOPSPTransferOutstanding = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek TOPSPTransferOutstanding
                            objTOPSPTransferOutstanding = ParseHeader(line)
                            ' insert to array objek TOPSPTransferOutstanding
                            If Not IsNothing(objTOPSPTransferOutstanding) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then Throw New Exception(errorMessage.ToString())
                                _arrTOPSPTransferOutstanding.Add(objTOPSPTransferOutstanding)
                                objTOPSPTransferOutstanding = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "TOPSPTransferOutstandingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPTransferOutstandingParser, BlockName)
                        Throw New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        objTOPSPTransferOutstanding = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrTOPSPTransferOutstanding
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overloads Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facTOPSPTransferOutstanding As New TOPSPTransferOutstandingFacade(user)
            For Each objTOPSPTransferOutstanding As TOPSPTransferOutstanding In _arrTOPSPTransferOutstanding
                Try
                    If Not IsNothing(objTOPSPTransferOutstanding) Then
                        If objTOPSPTransferOutstanding.ErrorMessage = String.Empty Then
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferOutstanding), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(TOPSPTransferOutstanding), "Dealer.DealerCode", MatchType.Exact, objTOPSPTransferOutstanding.Dealer.DealerCode))
                            criterias.opAnd(New Criteria(GetType(TOPSPTransferOutstanding), "RegNumber", MatchType.Exact, objTOPSPTransferOutstanding.RegNumber))
                            Dim sortColl As SortCollection = New SortCollection
                            sortColl.Add(New Sort(GetType(TOPSPTransferOutstanding), "ID", Sort.SortDirection.DESC))
                            Dim TOPSPTransferOutstandingList As ArrayList = New TOPSPTransferOutstandingFacade(user).RetrieveByCriteria(criterias, sortColl)
                            If TOPSPTransferOutstandingList.Count > 0 Then
                                ' update data
                                Dim old As TOPSPTransferOutstanding = TOPSPTransferOutstandingList(0)
                                old.RefBank = objTOPSPTransferOutstanding.RefBank
                                old.Bank = objTOPSPTransferOutstanding.Bank
                                old.TransferAmount = objTOPSPTransferOutstanding.TransferAmount
                                old.TransferDate = objTOPSPTransferOutstanding.TransferDate
                                old.TRNo = objTOPSPTransferOutstanding.TRNo
                                old.IDTransaction = objTOPSPTransferOutstanding.IDTransaction
                                old.Narrative = objTOPSPTransferOutstanding.Narrative
                                If facTOPSPTransferOutstanding.Update(old) < 0 Then
                                    nError += 1
                                End If
                            Else
                                ' insert new data
                                If facTOPSPTransferOutstanding.Insert(objTOPSPTransferOutstanding) < 0 Then
                                    nError += 1
                                End If
                            End If
                        Else
                            Throw New Exception(objTOPSPTransferOutstanding.ErrorMessage)
                            nError += 1
                        End If

                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrTOPSPTransferOutstanding.Count.ToString(), "ws-worker", "TOPSPTransferOutstandingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPTransferOutstandingParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "TOPSPTransferOutstandingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.TOPSPTransferOutstandingParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As TOPSPTransferOutstanding
            'K;TOPSPPaymentOutstanding_[TimeStamp]\nH;DealerCode;RegNumber;Refbank;KodeBank;TransferAmount;TransferDate[ddmmyyyy]; Narrative; TRNo; TypeTransaksi
            'K;TOPSPPaymentOutstanding_20161011153924\nH;100001;200001211841;BLI-7336290010921;BTMU;971842646;09012021;DOMESTIC REMITTANCE INCOMING RMKS VA1;1400222057;1\n

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objTOPSPTransferOutstanding As New TOPSPTransferOutstanding
            'Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 DealerCode
                Dim DealerCode As String = cols(1).Trim
                Dim RegNumber As String = cols(2).Trim
                Dim Refbank As String = cols(3).Trim
                Dim KodeBank As String = cols(4).Trim
                Dim TransferAmount As String = cols(5).Trim
                Dim TransferDate As String = cols(6).Trim
                Dim Narrative As String = cols(7).Trim
                Dim TRNo As String = cols(8).Trim


                If DealerCode = String.Empty Then
                    writeError("Dealer Code can't be empty")
                Else
                    Try
                        Dim objDealer As Dealer = New DealerFacade(user).Retrieve(DealerCode)
                        If Not IsNothing(objDealer) AndAlso objDealer.ID > 0 Then
                            objTOPSPTransferOutstanding.Dealer = objDealer
                        Else
                            Throw New Exception("Invalid Dealer Code " & DealerCode)
                        End If
                    Catch ex As Exception
                        writeError("Dealer Code  error: " & ex.Message)
                    End Try
                End If

                Dim TransactionType As Short = 2
                If RegNumber.Trim.Length > 0 Then
                    TransactionType = 1
                End If

                ''2 RegNumber
                'If isTOP Then
                '    writeError("RegNumber Code can't be empty")
                'Else
                Try
                    objTOPSPTransferOutstanding.RegNumber = RegNumber
                Catch ex As Exception
                    writeError("RegNumber  error: " & ex.Message)
                End Try

                'End If

                '3 Refbank
                'If Refbank = String.Empty Then
                '    writeError("Refbank Code can't be empty")
                'Else
                Try
                    objTOPSPTransferOutstanding.RefBank = Refbank
                Catch ex As Exception
                    errorMessage.Append("Invalid Refbank")
                End Try
                'End If

                '4 KodeBank
                'If KodeBank = String.Empty Then
                '    writeError("Bank Code can't be empty")
                'Else
                If KodeBank <> String.Empty Then
                    Try
                        Dim objStdCode As StandardCode = New StandardCodeFacade(user).GetByCategoryValueCode("SAPBank", KodeBank)
                        If Not IsNothing(objStdCode) Then
                            KodeBank = objStdCode.ValueDesc
                        End If
                        Dim objBank As Bank = New BankFacade(user).Retrieve(KodeBank)
                        If Not IsNothing(objBank) AndAlso objBank.ID > 0 Then
                            objTOPSPTransferOutstanding.Bank = objBank
                        Else
                            Throw New Exception("Invalid Bank Code " & KodeBank)
                        End If
                    Catch ex As Exception
                        errorMessage.Append("Invalid Bank Code")
                    End Try
                End If
                'End If

                '5 TransferAmount
                If TransferAmount = String.Empty Then
                    writeError("TransferAmount can't be empty")
                Else
                    Try
                        objTOPSPTransferOutstanding.TransferAmount = MyBase.GetCurrency(TransferAmount)
                    Catch ex As Exception
                        writeError("TransferAmount error: " & ex.Message)
                    End Try
                End If


                '6 TransferDate
                If TransferDate = String.Empty Then
                    writeError("TransferDate can't be empty")
                Else
                    Try
                        objTOPSPTransferOutstanding.TransferDate = GetShortDate(TransferDate)
                    Catch ex As Exception
                        writeError("TransferDate error: " & ex.Message)
                    End Try
                End If

                '7 Narrative
                'If Narrative = String.Empty Then
                '    writeError("Narrative can't be empty")
                'Else
                Try
                    objTOPSPTransferOutstanding.Narrative = Narrative
                Catch ex As Exception
                    errorMessage.Append("Invalid Narrative")
                End Try
                'End If

                '8 TRNo
                If TRNo = String.Empty Then
                    writeError("TRNo can't be empty")
                Else
                    Try
                        objTOPSPTransferOutstanding.TRNo = TRNo
                    Catch ex As Exception
                        writeError("TRNo error: " & ex.Message)
                    End Try
                End If

                '9 TypeTransaksi
                'PDCode = cols(9).Trim
                'If PDCode = String.Empty Then
                '    writeError("TypeTransaksi can't be empty")
                'Else
                Try
                    objTOPSPTransferOutstanding.IDTransaction = TransactionType
                Catch ex As Exception
                    writeError("TypeTransaksi error: " & ex.Message)
                End Try
                'End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objTOPSPTransferOutstanding.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objTOPSPTransferOutstanding.LastUpdatedBy = "WS"
                End If
                End If

                Return objTOPSPTransferOutstanding
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
