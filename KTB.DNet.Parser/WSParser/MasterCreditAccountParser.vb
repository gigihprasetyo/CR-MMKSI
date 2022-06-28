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
Imports KTB.DNet.BusinessFacade.Profile
#End Region

Namespace KTB.DNet.Parser

    Public Class MasterCreditAccountParser
        Inherits AbstractParser


#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objDealer As Dealer
        Private _arrCreditAccount As ArrayList

        Private intNo As Short = 0
        Const chrSplitDel As String = "||"
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub
#End Region

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _arrCreditAccount = New ArrayList()
                objDealer = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek Dealer Group
                            objDealer = ParseHeader(line)
                            ' insert to array objek Dealer Group
                            If Not IsNothing(objDealer) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objDealer.ErrorMessage = errorMessage.ToString()
                                _arrCreditAccount.Add(objDealer)
                                objDealer = Nothing
                            End If
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MasterCreditAccountParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterCreditAccountParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _arrCreditAccount = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrCreditAccount
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oDealerFacade As New DealerFacade(user)
            'Dim objDealerCA As Dealer = New Dealer
            intNo = 0

            For Each objDealer As Dealer In _arrCreditAccount
                Try
                    If Not IsNothing(objDealer) Then
                        If objDealer.ErrorMessage = String.Empty Then
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, objDealer.DealerCode))
                            Dim oDealerOldList As ArrayList = New DealerFacade(user).Retrieve(criterias)

                            If oDealerOldList.Count > 0 Then
                                Dim oDealerOld As Dealer = CType(oDealerOldList(0), Dealer)
                                'objDealerCA = New DealerFacade(user).Retrieve(objDealer.CreditAccount)
                                If oDealerOld.DealerCode <> objDealer.DealerCode OrElse
                                    oDealerOld.CreditAccount <> objDealer.CreditAccount OrElse
                                    IsNothing(oDealerOld.MainDealer) OrElse
                                    oDealerOld.RowStatus <> objDealer.RowStatus Then

                                    ''--- Process Update Data
                                    oDealerOld.DealerCode = objDealer.DealerCode
                                    oDealerOld.CreditAccount = objDealer.CreditAccount
                                    oDealerOld.MainDealer = oDealerOld
                                    oDealerOld.RowStatus = DBRowStatus.Active
                                    If oDealerFacade.Update(oDealerOld) < 0 Then
                                        nError += 1
                                        Throw New Exception("Proses Update gagal untuk Dealer Code: " & objDealer.DealerCode)
                                    End If
                                End If
                            Else
                                Throw New Exception("Dealer Code " & objDealer.DealerCode & " is not valid")
                            End If
                        Else
                            Throw New Exception(objDealer.ErrorMessage)
                        End If
                    End If

                Catch ex As Exception
                    If ex.Message.Length > 0 Then
                        Dim exMsg() As String = ex.Message.ToString().Split(chrSplitDel.ToCharArray, StringSplitOptions.RemoveEmptyEntries)
                        If exMsg.Length > 0 Then
                            For Each strmsg As String In exMsg
                                If strmsg.Trim <> "" Then
                                    If Not sMsg.ToString().Trim().Contains(strmsg.Trim) Then
                                        intNo += 1
                                        sMsg &= Chr(13) & intNo.ToString & ". " & strmsg.Trim
                                    End If
                                End If
                            Next
                        End If
                    End If

                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrCreditAccount.Count.ToString(), "ws-worker", "MasterCreditAccountParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterCreditAccountParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MasterCreditAccountParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterCreditAccountParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

#Region "Private Methods"
        Private Sub writeError(str As String)
            If errorMessage.Length = 0 Then
                errorMessage.Append(Chr(13) & str.Trim & ";")
            Else
                errorMessage.Append(Chr(13) & chrSplitDel & str.Trim & ";")
            End If
        End Sub

        Private Function ParseHeader(ByVal line As String) As Dealer
            ' K;MASTERCREDITACCOUNT_timestamp\nH;StringH-1;StringH-2;\n
            ' K;MASTERCREDITACCOUNT_20180810112801\nH;100001;100001\nH;100002;100002\nH;100003;100003\n

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objDealer As New Dealer

            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 Dealer Code
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("Dealer Code can't be empty")
                Else
                    Dim objDealer2 As Dealer = New DealerFacade(user).Retrieve(PDCode)
                    If Not IsNothing(objDealer2) AndAlso objDealer2.ID > 0 Then
                        objDealer.DealerCode = objDealer2.DealerCode
                    Else
                        writeError("Dealer Code: " & PDCode & " doesn't exist")
                    End If
                End If

                '2 Group Name
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    objDealer.CreditAccount = "000000"
                Else
                    objDealer.CreditAccount = PDCode.Trim()
                End If
            End If

            Return objDealer
        End Function
#End Region

    End Class
End Namespace