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
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
#End Region

Namespace KTB.DNet.Parser
    Public Class MSPDurationPMKindParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objMSPDurationPMKind As MSPDurationPMKind
        Private _arrMSPDurationPMKind As ArrayList
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

                _arrMSPDurationPMKind = New ArrayList()
                objMSPDurationPMKind = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek MSPDurationPMKind
                            objMSPDurationPMKind = ParseHeader(line)
                            ' insert to array objek MSPDurationPMKind
                            If Not IsNothing(objMSPDurationPMKind) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objMSPDurationPMKind.ErrorMessage = errorMessage.ToString()
                                _arrMSPDurationPMKind.Add(objMSPDurationPMKind)
                                objMSPDurationPMKind = Nothing
                            End If

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MSPDurationPMKindParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPDurationPMKindParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        objMSPDurationPMKind = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrMSPDurationPMKind
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim facMSPDurationPMKind As New MSPDurationPMKindFacade(user)
            For Each item As MSPDurationPMKind In _arrMSPDurationPMKind
                Try
                    If Not IsNothing(item) Then
                        If item.ErrorMessage = String.Empty Then
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPDurationPMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(MSPDurationPMKind), "PMKindCode", MatchType.Exact, item.PMKindCode))
                            criterias.opAnd(New Criteria(GetType(MSPDurationPMKind), "Duration", MatchType.Exact, item.Duration))

                            Dim sortColl As SortCollection = New SortCollection
                            sortColl.Add(New Sort(GetType(MSPDurationPMKind), "ID", Sort.SortDirection.DESC))
                            Dim MSPDurationPMKindList As ArrayList = New MSPDurationPMKindFacade(user).RetrieveByCriteria(criterias, sortColl)
                            ' update data jika hanya ada perubahan status
                            If MSPDurationPMKindList.Count > 0 Then
                                Dim old As MSPDurationPMKind = MSPDurationPMKindList(0)
                                old.RowStatus = item.RowStatus

                                If facMSPDurationPMKind.Update(old) < 0 Then
                                    nError += 1
                                End If
                            Else
                                ' insert new data
                                If facMSPDurationPMKind.Insert(item) < 0 Then
                                    nError += 1
                                End If
                            End If
                        Else
                            Throw New Exception(objMSPDurationPMKind.ErrorMessage)
                            nError += 1
                        End If

                    End If
                Catch ex As Exception
                    sMsg &= ex.Message.ToString() & ";"
                    nError += 1
                End Try

            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrMSPDurationPMKind.Count.ToString(), "ws-worker", "MSPDurationPMKindParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPDurationPMKindParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MSPMasterParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPDurationPMKindParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As MSPDurationPMKind
            ' K;MSPDurationPMKind_TimeStamp\nH;Duration;KindCode;RowStatus
            ' K;MSPDurationPMKind_TimeStamp\nH;3;06;0
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            objMSPDurationPMKind = New MSPDurationPMKind

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 Duration
                If cols(1).Trim = String.Empty Then
                    writeError("Duration can't be empty")
                Else
                    objMSPDurationPMKind.Duration = cols(1).Trim
                End If

                '2 KindCode
                If cols(2).Trim = String.Empty Then
                    Throw New Exception("Kind Code can't be empty")
                Else
                    Try
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(PMKind), "KindCode", MatchType.Exact, cols(2).Trim))
                        Dim objPMKind As PMKind = New PMKindFacade(user).Retrieve(crt)(0)
                        If Not IsNothing(objPMKind) AndAlso objPMKind.ID > 0 Then
                            objMSPDurationPMKind.PMKindCode = objPMKind.KindCode
                        Else
                            Throw New Exception("Invalid Kind Code " & cols(2).Trim)
                        End If
                    Catch ex As Exception
                        writeError("KInd Code  error: " & ex.Message)
                    End Try
                End If

                '4 RowStatus
                Dim rowStatus As Integer = -1
                If cols(3).Trim = String.Empty Then
                    rowStatus = 0
                End If

                Try
                    objMSPDurationPMKind.RowStatus = CType(rowStatus, Short)
                Catch ex As Exception
                    writeError("Status must be integer or set to blank if rowstatus is active (-1=InActive)")
                End Try

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    objMSPDurationPMKind.ErrorMessage = errorMessage.ToString() & vbCrLf & line
                Else
                    objMSPDurationPMKind.LastUpdateBy = "WS"
                End If
            End If

            Return objMSPDurationPMKind
        End Function
#End Region

    End Class
End Namespace
