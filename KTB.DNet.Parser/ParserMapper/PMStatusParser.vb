#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class PMStatusParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _PMStatusHeaders As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private sBuilder As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(";")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            _PMStatusHeaders = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                Try
                    ParsePMStatus(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PMStatusParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PMStatusParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _PMStatusHeaders
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim objPMHeader As PMHeader
            Dim isChange As New IsChangeFacade
            For Each item As PMHeader In _PMStatusHeaders
                Try
                    Dim _pmHeaderFacade As PMHeaderFacade = New PMHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                    objPMHeader = New PMHeader
                    objPMHeader = getPMHeader(item.ChassisMaster.ID, item.Dealer.ID, item.StandKM, item.ServiceDate)

                    If objPMHeader IsNot Nothing Then
                        If objPMHeader.Dealer.ID = item.Dealer.ID Then
                            _pmHeaderFacade.UpdatePMStatus(item)
                        Else
                            Throw New Exception("Invalid Data, ChasisNumber Identical but Dealer Different, Dealer Existing : " & objPMHeader.Dealer.DealerCode & " ,New Dealer : " & item.Dealer.DealerCode)
                        End If
                    Else
                        'need confirmation to BA... Farid Additional 20180314
                        '_pmHeaderFacade.Insert(item)
                    End If
                    _pmHeaderFacade.UpdatePMStatus(item)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PMStatusParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PMStatusParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.ID & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function getPMHeader(ByVal ChassisID As Integer, ByVal DealerID As Integer, ByVal StandKM As Integer, ByVal ServiceDate As Date) As PMHeader
            Dim criPMHeader As New CriteriaComposite(New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criPMHeader.opAnd(New Criteria(GetType(PMHeader), "StandKM", MatchType.Exact, CType(StandKM, Integer)))
            criPMHeader.opAnd(New Criteria(GetType(PMHeader), "ServiceDate", MatchType.Exact, ServiceDate))
            criPMHeader.opAnd(New Criteria(GetType(PMHeader), "Dealer.ID", MatchType.Exact, CType(DealerID, Integer)))
            criPMHeader.opAnd(New Criteria(GetType(PMHeader), "ChassisMaster.ID", MatchType.Exact, CType(ChassisID, Integer)))

            Dim objPMHeader As PMHeaderFacade = New PMHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim arlPMHeader As ArrayList = objPMHeader.Retrieve(criPMHeader)
            If arlPMHeader.Count > 0 Then
                Return CType(arlPMHeader(0), PMHeader)
            Else
                Return Nothing
            End If

        End Function


        Private Sub ParsePMStatus(ByVal ValParser As String)
            Dim _PMHeader As PMHeader = New PMHeader
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            sBuilder = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Length > 0 Then
                            Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If Not objDealer Is Nothing And objDealer.ID > 0 Then
                                _PMHeader.Dealer = objDealer
                            Else
                                sBuilder.Append("Kode Dealer tidak ditemukan" & Chr(13) & Chr(10))
                            End If
                        Else
                            sBuilder.Append("Kode Dealer tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        If sTemp.Length > 0 Then
                            Dim objChassisMaster As ChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If Not objChassisMaster Is Nothing And objChassisMaster.ID > 0 Then
                                _PMHeader.ChassisMaster = objChassisMaster
                            Else
                                sBuilder.Append("No Chassis tidak ditemukan" & Chr(13) & Chr(10))
                            End If
                        Else
                            sBuilder.Append("No Chassis tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If sTemp.Length = 8 Then
                            _PMHeader.ServiceDate = New DateTime(CInt(sTemp.Substring(4, 4)), CInt(sTemp.Substring(2, 2)), CInt(sTemp.Substring(0, 2)))
                        Else
                            sBuilder.Append("Tgl PM tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                If CInt(sTemp) >= 0 Then
                                    _PMHeader.StandKM = CInt(sTemp)
                                Else
                                    sBuilder.Append("Stand KM harus lebih besar dari 0" & Chr(13) & Chr(10))
                                End If
                            Else
                                sBuilder.Append("Stand KM harus angka" & Chr(13) & Chr(10))
                            End If
                        Else
                            sBuilder.Append("Stand KM tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        If sTemp.Length > 0 Then
                            For Each item As String In sTemp.Split("-")
                                Dim obJRepMaster As ReplecementPartMaster = New ReplecementPartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                                If obJRepMaster.ID > 0 Then
                                    Dim objDetail As PMDetail = New PMDetail
                                    objDetail.PMHeader = _PMHeader
                                    objDetail.ReplecementPartMaster = obJRepMaster
                                    _PMHeader.PMDetails.Add(objDetail)
                                End If
                            Next
                        End If
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            _PMHeader.ErrorMessage = sBuilder.ToString
            If _PMHeader.ErrorMessage.Length = 0 Then
                _PMStatusHeaders.Add(_PMHeader)
            End If
        End Sub
#End Region
    End Class
End Namespace

