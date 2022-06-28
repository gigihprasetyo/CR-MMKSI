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

    Public Class MasterTOPParser
        Inherits AbstractParser


#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objTermOfPayment As TermOfPayment
        Private _arrTermOfPayment As ArrayList

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

                _arrTermOfPayment = New ArrayList()
                objTermOfPayment = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek TOP
                            objTermOfPayment = ParseHeader(line)
                            ' insert to array objek TOP
                            If Not IsNothing(objTermOfPayment) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objTermOfPayment.ErrorMessage = errorMessage.ToString()
                                _arrTermOfPayment.Add(objTermOfPayment)
                                objTermOfPayment = Nothing
                            End If
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MasterTOPParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterTermOfPaymentParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _arrTermOfPayment = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrTermOfPayment
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oTermOfPaymentFacade As New TermOfPaymentFacade(user)
            intNo = 0

            If Not IsNothing(_arrTermOfPayment) AndAlso _arrTermOfPayment.Count > 0 Then
                'Dim criterias0 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'Dim arrTermOfPaymentOldList1 As ArrayList = New TermOfPaymentFacade(user).Retrieve(criterias0)
                'If Not IsNothing(arrTermOfPaymentOldList1) AndAlso arrTermOfPaymentOldList1.Count > 0 Then
                '    For Each objTermOfPaymentOld1 As TermOfPayment In arrTermOfPaymentOldList1
                '        objTermOfPaymentOld1.RowStatus = DBRowStatus.Deleted
                '        If oTermOfPaymentFacade.Update(objTermOfPaymentOld1) < 0 Then
                '            nError += 1
                '        End If
                '    Next
                'End If

                For Each objTermOfPayment As TermOfPayment In _arrTermOfPayment
                    Try
                        If Not IsNothing(objTermOfPayment) Then
                            If objTermOfPayment.ErrorMessage = String.Empty Then
                                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias.opAnd(New Criteria(GetType(TermOfPayment), "TermOfPaymentCode", MatchType.Exact, objTermOfPayment.TermOfPaymentCode))
                                Dim objTOPOldList As ArrayList = New TermOfPaymentFacade(user).Retrieve(criterias)

                                If Not IsNothing(objTOPOldList) AndAlso objTOPOldList.Count > 0 Then
                                    Dim objTOPOld As TermOfPayment = CType(objTOPOldList(0), TermOfPayment)
                                    If objTOPOld.TermOfPaymentCode.Trim <> objTermOfPayment.TermOfPaymentCode.Trim OrElse
                                        objTOPOld.TermOfPaymentValue <> objTermOfPayment.TermOfPaymentValue OrElse
                                        objTOPOld.Description.Trim <> objTermOfPayment.Description.Trim OrElse
                                        objTOPOld.RowStatus <> objTermOfPayment.RowStatus Then

                                        ''--- Process Update Data
                                        objTOPOld.TermOfPaymentCode = objTermOfPayment.TermOfPaymentCode
                                        objTOPOld.TermOfPaymentValue = objTermOfPayment.TermOfPaymentValue
                                        objTOPOld.PaymentType = 2
                                        objTOPOld.Description = objTermOfPayment.Description.Trim
                                        objTOPOld.RowStatus = DBRowStatus.Active
                                        If oTermOfPaymentFacade.Update(objTOPOld) < 0 Then
                                            nError += 1
                                            Throw New Exception("Proses Update gagal untuk TOP Code: " & objTermOfPayment.TermOfPaymentCode)
                                        End If
                                    End If

                                Else
                                    If oTermOfPaymentFacade.Insert(objTermOfPayment) < 0 Then
                                        nError += 1
                                        Throw New Exception("Proses Insert gagal untuk TOP Code: " & objTermOfPayment.TermOfPaymentCode)
                                    End If
                                End If
                            Else
                                Throw New Exception(objTermOfPayment.ErrorMessage)
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
            End If

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrTermOfPayment.Count.ToString(), "ws-worker", "MasterTOPParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterTermOfPaymentParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MasterTOPParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterTermOfPaymentParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As TermOfPayment
            ' K;MASTERTOP_timestamp\nH;StringH-1;StringH-2;StringH-3\n
            ' K;MASTERTOP_20180810112801\nH;Z001;1;TOP 1 Hari\nH;Z002;2;TOP 2 Hari\n

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objTermOfPayment As New TermOfPayment

            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 Term Of Payment Code
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("Term Of Payment Code can't be empty")
                Else
                    objTermOfPayment.TermOfPaymentCode = PDCode.Trim()
                End If

                '2 Term Of Payment Value
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    writeError("Term Of Payment Value can't be empty")
                Else
                    Dim parsedValue As Integer
                    If Not Integer.TryParse(PDCode, parsedValue) Then
                        writeError("Term Of Payment Value is a number only")
                    Else
                        If parsedValue < 0 Then
                            writeError("Term Of Payment Value is a positif number only")
                        Else
                            objTermOfPayment.TermOfPaymentValue = PDCode.Trim()
                        End If
                    End If
                End If

                '3 Description
                PDCode = cols(3).Trim
                If PDCode = String.Empty Then
                    writeError("Description can't be empty")
                Else
                    objTermOfPayment.Description = PDCode.Trim()
                End If

                objTermOfPayment.PaymentType = 2
                objTermOfPayment.RowStatus = 0

            End If

            Return objTermOfPayment
        End Function
#End Region

    End Class
End Namespace