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
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.Profile
#End Region

Namespace KTB.DNet.Parser
    Public Class BillingAPOutstandingParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private oMonthlyDocument As MonthlyDocument
        Private arrMonthlyDocument As ArrayList
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub
#End Region

#Region "Protected Methods"
        Protected Overloads Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                arrMonthlyDocument = New ArrayList()
                oMonthlyDocument = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            oMonthlyDocument = ParseHeader(line)
                            If Not IsNothing(oMonthlyDocument) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then oMonthlyDocument.ErrorMessage = errorMessage.ToString()
                                arrMonthlyDocument.Add(oMonthlyDocument)
                                oMonthlyDocument = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "BillingAPOutstandingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BillingAPOutstandingParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        oMonthlyDocument = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return arrMonthlyDocument
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overloads Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facMonthlyDocument As New MonthlyDocumentFacade(user)
            For Each objMonthlyDocument As MonthlyDocument In arrMonthlyDocument
                Try
                    If Not IsNothing(objMonthlyDocument) Then
                        If objMonthlyDocument.ErrorMessage = String.Empty Then
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MonthlyDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(MonthlyDocument), "AccountingNo", MatchType.Exact, objMonthlyDocument.AccountingNo))
                            criterias.opAnd(New Criteria(GetType(MonthlyDocument), "BillingDate", MatchType.Exact, objMonthlyDocument.BillingDate))
                            Dim sortColl As SortCollection = New SortCollection
                            sortColl.Add(New Sort(GetType(MonthlyDocument), "id", Sort.SortDirection.DESC))
                            Dim MonthlyDocumentList As ArrayList = New MonthlyDocumentFacade(user).Retrieve(criterias, sortColl)
                            If MonthlyDocumentList.Count > 0 Then
                                For i As Integer = 0 To MonthlyDocumentList.Count - 1
                                    Dim old As MonthlyDocument = MonthlyDocumentList(i)
                                    old.BillingDate = objMonthlyDocument.BillingDate
                                    old.AccountingNo = objMonthlyDocument.AccountingNo
                                    old.TransferDate = objMonthlyDocument.TransferDate
                                    old.SettlementDate = objMonthlyDocument.SettlementDate
                                    old.ActualTransferDate = objMonthlyDocument.ActualTransferDate

                                    If objMonthlyDocument.TaxNo <> String.Empty Then
                                        old.TaxNo = objMonthlyDocument.TaxNo
                                    End If
                                    If objMonthlyDocument.ParkedName <> String.Empty Then
                                        old.ParkedName = objMonthlyDocument.ParkedName
                                    End If
                                    If objMonthlyDocument.Amount <> 0 Then
                                        old.Amount = objMonthlyDocument.Amount
                                    End If
                                    If objMonthlyDocument.Currencies <> String.Empty Then
                                        old.Currencies = objMonthlyDocument.Currencies
                                    End If
                                    If objMonthlyDocument.Description <> String.Empty Then
                                        old.Description = objMonthlyDocument.Description
                                    End If
                                    If objMonthlyDocument.NoClearing <> 0 Then
                                        old.NoClearing = objMonthlyDocument.NoClearing
                                    End If

                                    If facMonthlyDocument.Update(old) < 0 Then
                                        nError += 1
                                    End If

                                Next
                                'Else
                                '    If facMonthlyDocument.Insert(objMonthlyDocument) < 0 Then
                                '        nError += 1
                                '    End If
                            End If
                        Else
                            Throw New Exception(objMonthlyDocument.ErrorMessage)
                            nError += 1
                        End If

                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & arrMonthlyDocument.Count.ToString(), "ws-worker", "BillingAPOutstandingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BillingAPOutstandingParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "BillingAPOutstandingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BillingAPOutstandingParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#End Region


#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As MonthlyDocument
            'H;Billingdate[yyyymmdd];accountingNo;TaxNo;BaseLineDate[yyyymmdd];ParkedName;Amount; Curr;Text; clearing;settlementDate[yyyymmdd];ActualTransfer[yyyymmdd]
            'H;20210922;0908235117;10.006-21.168235117;20211005;Putra Samudra;10000;IDR;Warranty Claim Juli 2021;2000092873;20210701;20210701;

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim _oMonthlyDocument As New MonthlyDocument
            Dim _oStandardCode As New StandardCode
            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 billingdate
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("Billing Date can't be empty")
                Else
                    Try
                        _oMonthlyDocument.BillingDate = GetShortDate(PDCode)
                    Catch ex As Exception
                        writeError("Invalid Billing Date")
                    End Try
                End If

                '2 AccountingNo
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    writeError("Accounting No can't be empty")
                Else
                    Try
                        _oMonthlyDocument.AccountingNo = PDCode
                    Catch ex As Exception
                        writeError("Invalid Accounting No")
                    End Try
                End If

                '3 TaxNo
                PDCode = cols(3).Trim
                If PDCode <> String.Empty Then
                    _oMonthlyDocument.TaxNo = PDCode
                End If

                'If PDCode = String.Empty Then
                '    writeError("Tax No can't be empty")
                'Else
                '    Try
                '        _oMonthlyDocument.TaxNo = PDCode
                '    Catch ex As Exception
                '        writeError("Invalid Tax No")
                '    End Try
                'End If

                '4 BaseLine
                PDCode = cols(4).Trim
                If PDCode = String.Empty Then
                    writeError("Base Line Date can't be empty")
                Else
                    Try
                        _oMonthlyDocument.TransferDate = GetShortDate(PDCode)
                    Catch ex As Exception
                        writeError("Invalid Base Line Date")
                    End Try
                End If

                '5 ParkedName
                PDCode = cols(5).Trim
                If PDCode <> String.Empty Then
                    _oMonthlyDocument.ParkedName = PDCode
                End If

                'If PDCode = String.Empty Then
                '    writeError("Parked Name can't be empty")
                'Else
                '    Try
                '        _oMonthlyDocument.ParkedName = PDCode
                '    Catch ex As Exception
                '        writeError("Invalid Parked Name")
                '    End Try
                'End If

                '6 Amount
                PDCode = cols(6).Trim
                If PDCode <> String.Empty Then
                    _oMonthlyDocument.Amount = MyBase.GetCurrency(PDCode)
                End If

                'If PDCode = String.Empty Then
                '    writeError("Amount can't be empty")
                'Else
                '    Try
                '        _oMonthlyDocument.Amount = MyBase.GetCurrency(PDCode)
                '    Catch ex As Exception
                '        writeError("Amount error: " & ex.Message)
                '    End Try
                'End If

                '7 Currencies
                PDCode = cols(7).Trim
                If PDCode <> String.Empty Then
                    _oMonthlyDocument.Currencies = PDCode
                End If
                'If PDCode = String.Empty Then
                '    writeError("Currencies can't be empty")
                'Else
                '    Try
                '        _oMonthlyDocument.Currencies = PDCode
                '    Catch ex As Exception
                '        writeError("Invalid Currencies")
                '    End Try
                'End If

                '8 Description/text
                PDCode = cols(8).Trim
                If PDCode <> String.Empty Then
                    _oMonthlyDocument.Description = PDCode
                End If

                'If PDCode = String.Empty Then
                '    writeError("Text can't be empty")
                'Else
                '    Try
                '        _oMonthlyDocument.Description = PDCode
                '    Catch ex As Exception
                '        writeError("Invalid Text")
                '    End Try
                'End If

                '9 Clearing
                PDCode = cols(9).Trim
                If PDCode <> String.Empty Then
                    _oMonthlyDocument.NoClearing = CInt(PDCode)
                End If

                'If PDCode = String.Empty Then
                '    writeError("NoClearing can't be empty")
                'Else
                '    Try
                '        _oMonthlyDocument.NoClearing = PDCode
                '    Catch ex As Exception
                '        writeError("Invalid NoClearing")
                '    End Try
                'End If

                '10 settlementdate
                PDCode = cols(10).Trim
                If PDCode = String.Empty Then
                    writeError("Settlement Date can't be empty")
                Else
                    Try
                        _oMonthlyDocument.SettlementDate = GetShortDate(PDCode)
                    Catch ex As Exception
                        writeError("Invalid Settlement Date")
                    End Try
                End If

                '11 ActualTransfer
                PDCode = cols(11).Trim
                If PDCode = String.Empty Then
                    writeError("Actual Transfer can't be empty")
                Else
                    Try
                        _oMonthlyDocument.ActualTransferDate = GetShortDate(PDCode)
                    Catch ex As Exception
                        writeError("Invalid Actual Transfer")
                    End Try
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    _oMonthlyDocument.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    _oMonthlyDocument.LastUpdateBy = "WS"
                End If
            End If

            Return _oMonthlyDocument
        End Function

        Protected Function GetShortDate(ByVal str As String) As Date
            Dim dt As Date 'YYYYMMdd
            Try
                dt = New Date(Integer.Parse(str.Substring(0, 4)), Integer.Parse(str.Substring(4, 2)), Integer.Parse(str.Substring(6, 2)))
            Catch ex As Exception
                dt = DateSerial(1900, 1, 1)
            End Try
            Return dt
        End Function


#End Region

    End Class
End Namespace
