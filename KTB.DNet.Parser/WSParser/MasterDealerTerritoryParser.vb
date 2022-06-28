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
Imports KTB.DNet.Utility

#End Region

Namespace KTB.DNet.Parser

    Public Class MasterDealerTerritoryParser
        Inherits AbstractParser


#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private objDealerTerritory As DealerTerritory
        Private _arrDealerTerritory As ArrayList

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

                _arrDealerTerritory = New ArrayList()
                objDealerTerritory = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            errorMessage = New StringBuilder()
                            ' create objek TOP
                            objDealerTerritory = ParseHeader(line)
                            ' insert to array objek TOP
                            If Not IsNothing(objDealerTerritory) Then
                                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then objDealerTerritory.ErrorMessage = errorMessage.ToString()
                                _arrDealerTerritory.Add(objDealerTerritory)
                                objDealerTerritory = Nothing
                            End If
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "MasterDealerTerritoryParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterDealerTerritoryParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _arrDealerTerritory = Nothing
                    End Try
                Next

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Return _arrDealerTerritory
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oDealerTerritoryFacade As New DealerTerritoryFacade(user)
            Dim intDealerID As Integer = 0
            intNo = 0

            If Not IsNothing(_arrDealerTerritory) AndAlso _arrDealerTerritory.Count > 0 Then

                CommonFunction.SortListControl(_arrDealerTerritory, "Dealer.ID", Sort.SortDirection.ASC)

                For Each objDealerTerritory As DealerTerritory In _arrDealerTerritory
                    Try
                        If Not IsNothing(objDealerTerritory) Then
                            If intDealerID <> objDealerTerritory.Dealer.ID Then
                                intDealerID = objDealerTerritory.Dealer.ID
                                Dim criterias0 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerTerritory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias0.opAnd(New Criteria(GetType(DealerTerritory), "Dealer.ID", MatchType.Exact, objDealerTerritory.Dealer.ID))

                                Dim arrDealerTerritoryOldList1 As ArrayList = New DealerTerritoryFacade(user).Retrieve(criterias0)
                                If Not IsNothing(arrDealerTerritoryOldList1) AndAlso arrDealerTerritoryOldList1.Count > 0 Then
                                    For Each objDealerTerritoryOld1 As DealerTerritory In arrDealerTerritoryOldList1
                                        objDealerTerritoryOld1.RowStatus = DBRowStatus.Deleted
                                        If oDealerTerritoryFacade.Update(objDealerTerritoryOld1) < 0 Then
                                            nError += 1
                                        End If
                                    Next
                                End If
                            End If

                            If objDealerTerritory.ErrorMessage = String.Empty Then
                                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerTerritory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Deleted, Short)))
                                criterias.opAnd(New Criteria(GetType(DealerTerritory), "Dealer.ID", MatchType.Exact, objDealerTerritory.Dealer.ID))
                                criterias.opAnd(New Criteria(GetType(DealerTerritory), "City.ID", MatchType.Exact, objDealerTerritory.City.ID))
                                Dim arrDealerTerritoryOldList2 As ArrayList = New DealerTerritoryFacade(user).Retrieve(criterias)

                                If arrDealerTerritoryOldList2.Count > 0 Then
                                    Dim objDealerTerritoryOld As DealerTerritory = CType(arrDealerTerritoryOldList2(0), DealerTerritory)
                                    If objDealerTerritoryOld.Dealer.ID <> objDealerTerritory.Dealer.ID OrElse
                                        objDealerTerritoryOld.City.ID <> objDealerTerritory.City.ID OrElse
                                        objDealerTerritoryOld.RowStatus <> objDealerTerritory.RowStatus Then
                                        ''--- Process Update Data
                                        objDealerTerritoryOld.Dealer = objDealerTerritory.Dealer
                                        objDealerTerritoryOld.City = objDealerTerritory.City
                                        objDealerTerritoryOld.RowStatus = DBRowStatus.Active
                                        If oDealerTerritoryFacade.Update(objDealerTerritoryOld) < 0 Then
                                            nError += 1
                                            Throw New Exception("Proses Update gagal untuk Dealer Code: " & objDealerTerritory.Dealer.DealerCode & " dan City Code: " & objDealerTerritory.City.CityCode)
                                        End If
                                    End If

                                Else
                                    If oDealerTerritoryFacade.Insert(objDealerTerritory) < 0 Then
                                        nError += 1
                                        Throw New Exception("Proses Insert gagal untuk Dealer Code: " & objDealerTerritory.Dealer.DealerCode & " dan City Code: " & objDealerTerritory.City.CityCode)
                                    End If
                                End If
                            Else
                                Throw New Exception(objDealerTerritory.ErrorMessage)
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
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _arrDealerTerritory.Count.ToString(), "ws-worker", "MasterDealerTerritoryParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterDealerTerritoryParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "MasterDealerTerritoryParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MasterDealerTerritoryParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As DealerTerritory
            ' K;MASTERDEALERTERRITORY_timestamp\nH;StringH-1;StringH-2;\n
            ' K;MASTERDEALERTERRITORY_20180810112801\nH;100001;BLBDG;\nH;100001;BLDPS;\n

            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objDealerTerritory As New DealerTerritory

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
                    Dim objDealer As Dealer = New DealerFacade(user).Retrieve(PDCode)
                    If Not IsNothing(objDealer) AndAlso objDealer.ID > 0 Then
                        objDealerTerritory.Dealer = objDealer
                    Else
                        writeError("Dealer Code: " & PDCode & " doesn't exist")
                    End If
                End If

                '2 City Code
                PDCode = cols(2).Trim
                If PDCode = String.Empty Then
                    writeError("City Code can't be empty")
                Else
                    Dim objCity As City = New CityFacade(user).Retrieve(PDCode)
                    If Not IsNothing(objCity) AndAlso objCity.ID > 0 Then
                        objDealerTerritory.City = objCity
                    Else
                        writeError("City Code: " & PDCode & " doesn't exist")
                    End If
                End If

                objDealerTerritory.RowStatus = CType(DBRowStatus.Active, Short)

            End If

            Return objDealerTerritory
        End Function
#End Region

    End Class
End Namespace