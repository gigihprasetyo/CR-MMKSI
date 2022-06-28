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

    Public Class BabitParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objBabitReportJv As BabitReportJV
        Private objBabitEventReportJV As BabitEventReportJV
        Private _arrBabitReportJv As ArrayList
        Private _arrBabitEventReportJv As ArrayList

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

                _arrBabitReportJv = New ArrayList()
                _arrBabitEventReportJv = New ArrayList()
                objBabitReportJv = Nothing
                objBabitEventReportJV = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek TOP
                            ParseHeader(line, objBabitReportJv, objBabitEventReportJV)
                            ' insert to array objek TOP
                            If Not IsNothing(objBabitReportJv) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objBabitReportJv.ErrorMessage = errorMessage.ToString()
                                If objBabitReportJv.ID <> 0 Then
                                    _arrBabitReportJv.Add(objBabitReportJv)
                                End If
                                objBabitReportJv = Nothing
                            End If


                            If Not IsNothing(objBabitEventReportJV) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objBabitEventReportJV.ErrorMessage = errorMessage.ToString()
                                If objBabitEventReportJV.ID <> 0 Then
                                    _arrBabitEventReportJv.Add(objBabitEventReportJV)
                                End If
                                objBabitEventReportJV = Nothing
                            End If
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "BabitParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BabitParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _arrBabitReportJv = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            If _arrBabitReportJv.Count > 0 Then
                Return _arrBabitReportJv
            Else
                Return _arrBabitEventReportJv
            End If
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oBabitReportJVFacade As New BabitReportJVFacade(user)
            Dim oBabitEventReportJVFacade As New BabitEventReportJVFacade(user)
            intNo = 0

            If Not IsNothing(_arrBabitReportJv) AndAlso _arrBabitReportJv.Count > 0 Then
                For Each objBabitReportJv As BabitReportJV In _arrBabitReportJv
                    Try
                        'TODO
                        oBabitReportJVFacade.Update(objBabitReportJv)
                        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(BabitReportJVtoReceipt), "BabitReportJV.ID", MatchType.Exact, objBabitReportJv.ID))
                        Dim arrBJTR As ArrayList = New BabitReportJVtoReceiptFacade(user).Retrieve(crit)
                        For Each BJTR As BabitReportJVtoReceipt In arrBJTR
                            Dim Receipt As BabitReportReceipt = BJTR.BabitReportReceipt
                            Receipt.Status = 4
                            Dim _result As Integer = New BabitReportReceiptFacade(user).Update(Receipt)
                        Next
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



            If Not IsNothing(_arrBabitEventReportJv) AndAlso _arrBabitEventReportJv.Count > 0 Then
                For Each objBabitEventReportJv As BabitEventReportJV In _arrBabitEventReportJv
                    Try
                        'TODO
                        oBabitEventReportJVFacade.Update(objBabitEventReportJv)
                        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportJVtoReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(BabitEventReportJVtoReceipt), "BabitEventReportJV.ID", MatchType.Exact, objBabitEventReportJv.ID))
                        Dim arrBJTR As ArrayList = New BabitEventReportJVtoReceiptFacade(user).Retrieve(crit)
                        For Each BJTR As BabitEventReportJVtoReceipt In arrBJTR
                            Dim Receipt As BabitEventReportReceipt = BJTR.BabitEventReportReceipt
                            Receipt.Status = 4
                            Dim _result As Integer = New BabitEventReportReceiptFacade(user).Update(Receipt)
                        Next
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
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrBabitReportJv.Count.ToString(), "ws-worker", "BabitParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BabitParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "BabitParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.BabitParser, BlockName)
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

        Private Sub ParseHeader(ByVal line As String, ByRef oBabitReportJv As BabitReportJV, ByRef oBabitEventReportJv As BabitEventReportJV)
            'K;SBABITJV_Timestamp\nH;Text1;Text2;Text3;Text4\n
            'Text1 BabitReportJV.RegNumber
            'Text2 BabitReportJV.NoJV
            'Text3 BabitReportJV.TglProses
            'Text4 BabitReportJV.TglPencairan
            'K;SBABITJV_20191014092500\nH;JVB1900001;1900070911;20191014;\n

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objBabitReportJv As New BabitReportJV
            Dim objBabitEventReportJV As New BabitEventReportJV
            Dim Babit As Boolean = True

            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 RegNumber
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("RegNumber can't be empty")
                Else
                    If PDCode.Contains("JVB") Then
                        objBabitReportJv = New BabitReportJVFacade(user).Retrieve(PDCode)

                        If objBabitReportJv.ID = 0 Then
                            Exit Sub
                        End If
                    Else
                        Babit = False
                        objBabitEventReportJV = New BabitEventReportJVFacade(user).Retrieve(PDCode)

                        If objBabitEventReportJV.ID = 0 Then
                            Exit Sub
                        End If
                    End If
                End If

                '2 NoJV
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    writeError("NoJV can't be empty")
                Else
                    If Babit Then
                        objBabitReportJv.NoJV = PDCode.Trim()
                    Else
                        objBabitEventReportJV.NoJV = PDCode.Trim()
                    End If
                End If

                '3 TglProses
                PDCode = cols(3).Trim
                If PDCode = String.Empty Then
                    writeError("TglProses can't be empty")
                Else
                    Dim strTglProses As String = ""
                    If Babit Then
                        Try
                            If PDCode.Trim.Length > 0 Then
                                strTglProses = PDCode.Trim()
                                Dim vTgl As DateTime = New DateTime(PDCode.Substring(0, 4), CInt(PDCode.Substring(4, 2)), CInt(PDCode.Substring(6, 2)))
                                objBabitReportJv.TglProses = vTgl
                            End If

                        Catch ex As Exception
                            strTglProses = ""
                            errorMessage.Append("Invalid TglProses" & Chr(13) & Chr(10))
                        End Try
                    Else
                        Try
                            If PDCode.Trim.Length > 0 Then
                                strTglProses = PDCode.Trim()
                                Dim vTgl As DateTime = New DateTime(PDCode.Substring(0, 4), CInt(PDCode.Substring(4, 2)), CInt(PDCode.Substring(6, 2)))
                                objBabitEventReportJV.TglProses = vTgl
                            End If

                        Catch ex As Exception
                            strTglProses = ""
                            errorMessage.Append("Invalid TglProses" & Chr(13) & Chr(10))
                        End Try
                    End If
                End If

                '4 TglPencairan
                PDCode = cols(4).Trim
                If Not PDCode = String.Empty Then
                    'writeError("TglPencairan can't be empty")
                    'Else
                    Dim strTglPencairan As String = ""
                    If Babit Then
                        Try
                            If PDCode.Trim.Length > 0 Then
                                strTglPencairan = PDCode.Trim()
                                Dim vTgl As DateTime = New DateTime(PDCode.Substring(0, 4), CInt(PDCode.Substring(4, 2)), CInt(PDCode.Substring(6, 2)))
                                objBabitReportJv.TglPencairan = vTgl
                                objBabitReportJv.Status = 2
                            End If

                        Catch ex As Exception
                            strTglPencairan = ""
                            'errorMessage.Append("Invalid TglPencairan" & Chr(13) & Chr(10))
                        End Try
                    Else
                        Try
                            If PDCode.Trim.Length > 0 Then
                                strTglPencairan = PDCode.Trim()
                                Dim vTgl As DateTime = New DateTime(PDCode.Substring(0, 4), CInt(PDCode.Substring(4, 2)), CInt(PDCode.Substring(6, 2)))
                                objBabitEventReportJV.TglPencairan = vTgl
                                objBabitEventReportJV.Status = 2
                            End If

                        Catch ex As Exception
                            strTglPencairan = ""
                            'errorMessage.Append("Invalid TglPencairan" & Chr(13) & Chr(10))
                        End Try
                    End If
                End If

            End If

            oBabitReportJv = objBabitReportJv
            oBabitEventReportJv = objBabitEventReportJV
        End Sub
#End Region

    End Class

End Namespace
