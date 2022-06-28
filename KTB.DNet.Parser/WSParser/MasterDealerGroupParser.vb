﻿#Region ".NET Base Class Namespace Imports"
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

    Public Class MasterDealerGroupParser
        Inherits AbstractParser


#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objDealerGroup As DealerGroup
        Private _arrDealerGroup As ArrayList

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

                _arrDealerGroup = New ArrayList()
                objDealerGroup = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek Dealer Group
                            objDealerGroup = ParseHeader(line)
                            ' insert to array objek Dealer Group
                            If Not IsNothing(objDealerGroup) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objDealerGroup.ErrorMessage = errorMessage.ToString()
                                _arrDealerGroup.Add(objDealerGroup)
                                objDealerGroup = Nothing
                            End If
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MasterDealerGroupParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterDealerGroupParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _arrDealerGroup = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrDealerGroup
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oDealerGroupFacade As New DealerGroupFacade(user)
            intNo = 0

            If Not IsNothing(_arrDealerGroup) AndAlso _arrDealerGroup.Count > 0 Then
                'Dim criterias0 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'Dim arrDealerGroupOldList1 As ArrayList = New DealerGroupFacade(user).Retrieve(criterias0)
                'If Not IsNothing(arrDealerGroupOldList1) AndAlso arrDealerGroupOldList1.Count > 0 Then
                '    For Each objDealerGroupOld1 As DealerGroup In arrDealerGroupOldList1
                '        objDealerGroupOld1.RowStatus = DBRowStatus.Deleted
                '        If oDealerGroupFacade.Update(objDealerGroupOld1) < 0 Then
                '            nError += 1
                '        End If
                '    Next
                'End If

                For Each objDealerGroup As DealerGroup In _arrDealerGroup
                    Try
                        If Not IsNothing(objDealerGroup) Then
                            If objDealerGroup.ErrorMessage = String.Empty Then
                                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Deleted, Short)))
                                criterias.opAnd(New Criteria(GetType(DealerGroup), "DealerGroupCode", MatchType.Exact, objDealerGroup.DealerGroupCode))
                                Dim DealerGroupOldList As ArrayList = New DealerGroupFacade(user).Retrieve(criterias)

                                If Not IsNothing(DealerGroupOldList) AndAlso DealerGroupOldList.Count > 0 Then
                                    Dim DealerGroupOld As DealerGroup = CType(DealerGroupOldList(0), DealerGroup)
                                    If DealerGroupOld.DealerGroupCode.Trim <> objDealerGroup.DealerGroupCode.Trim OrElse
                                        DealerGroupOld.GroupName.Trim <> objDealerGroup.GroupName.Trim OrElse
                                        DealerGroupOld.RowStatus <> objDealerGroup.RowStatus Then

                                        ''--- Process Update Data
                                        DealerGroupOld.DealerGroupCode = objDealerGroup.DealerGroupCode
                                        DealerGroupOld.GroupName = objDealerGroup.GroupName
                                        DealerGroupOld.RowStatus = DBRowStatus.Active
                                        If oDealerGroupFacade.Update(DealerGroupOld) < 0 Then
                                            nError += 1
                                            Throw New Exception("Proses Update gagal untuk Dealer Group Code: " & objDealerGroup.DealerGroupCode)
                                        End If
                                    End If
                                Else
                                    If oDealerGroupFacade.Insert(objDealerGroup) < 0 Then
                                        nError += 1
                                        Throw New Exception("Proses Insert gagal untuk Dealer Group Code: " & objDealerGroup.DealerGroupCode)
                                    End If
                                End If
                            Else
                                Throw New Exception(objDealerGroup.ErrorMessage)
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
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrDealerGroup.Count.ToString(), "ws-worker", "MasterDealerGroupParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterDealerGroupParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MasterDealerGroupParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterDealerGroupParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As DealerGroup
            ' K;MASTERDEALERGROUP_timestamp\nH;StringH-1;StringH-2;\n
            ' K;MASTERDEALERGROUP_20180810112801\nH;01; SUMATERA GROUP\nH;02;LBUM\nH;03;SERDAM\n

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objDealerGroup As New DealerGroup

            Dim PDCode As String

            errorMessage = New StringBuilder()
            If cols.Length = 0 Then ' validasi colom Count
                writeError("Invalid Header Format")
            Else
                '1 Dealer Group Code
                PDCode = cols(1).Trim
                If PDCode = String.Empty Then
                    writeError("Dealer Group Code can't be empty")
                Else
                    objDealerGroup.DealerGroupCode = PDCode.Trim()
                End If

                '2 Group Name
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    writeError("Group Name can't be empty")
                Else
                    objDealerGroup.GroupName = PDCode.Trim()
                End If
            End If

            objDealerGroup.RowStatus = 0

            Return objDealerGroup
        End Function
#End Region

    End Class
End Namespace