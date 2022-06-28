#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class JVCairParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _JVs As ArrayList
        Private _prevStatus As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private _fileName As String
        Private ErrorMessage As StringBuilder
        Private _InitialStatuKuitansi As Integer = -1
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _fileName = fileName
            _Stream = New StreamReader(fileName, True)
            _JVs = New ArrayList
            _prevStatus = New ArrayList()
            Dim val As String = MyBase.NextLine(_Stream).Trim()

            While (Not val = "")
                Try
                    ParseJV(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "JVCairParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.JVCairParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _JVs
        End Function

        Protected Overrides Function DoTransaction() As Integer
            'TO DO call facade to insert using transaction
            Dim _DepositKuitansiFacade As DepositAKuitansiPencairanFacade
            Dim _DepositAPencairanHFacade As New DepositAPencairanHFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
            Dim _oldStatus As Integer = 0
            Dim _pos As Integer = 0
            For Each item As DepositAKuitansiPencairan In _JVs
                _pos = _pos + 1

                Try
                    'Update Status to Cair
                    _DepositKuitansiFacade = New DepositAKuitansiPencairanFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
                    _DepositKuitansiFacade.Update(item)

                    'Insert History
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "NoReg", MatchType.Exact, item.NoReg))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Dealer.ID", MatchType.Exact, item.Dealer.ID))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim arlDepositApencairanH As ArrayList = _DepositAPencairanHFacade.Retrieve(criterias)

                    If _prevStatus.Count > 0 Then
                        Dim nResultKuitansi As Integer = InsertHistory(item.NoSurat, CInt(_prevStatus(_pos - 1)), item.Status, 2)
                    End If

                   


                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "JVCairParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.JVCairParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.ReceiptNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

        End Function

        Public Function InsertHistory(ByVal NoSurat As String, ByVal OldStatus As Integer, ByVal NewStatus As Integer, ByVal DocType As Integer)
            If OldStatus = NewStatus Then
                Return 1
            End If
            Dim objHistoryDepositAPencairan As DepositAStatusHistory = New DepositAStatusHistory
            objHistoryDepositAPencairan.DocNumber = NoSurat
            objHistoryDepositAPencairan.OldStatus = OldStatus
            objHistoryDepositAPencairan.NewStatus = NewStatus
            objHistoryDepositAPencairan.DocType = DocType
            Dim nResult As Integer = -1
            nResult = New DepositAStatusHistoryFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))).Insert(objHistoryDepositAPencairan)
            Return nResult

        End Function
        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseJV(ByVal ValParser As String)
            Dim objDepositAKuitansiPencairan As DepositAKuitansiPencairan = New DepositAKuitansiPencairan
            Dim objDepositAPencairanH As New DepositAPencairanH
            Dim strKodeDealer As String = String.Empty
            Dim strNoReg As String = String.Empty
            Dim strStatus As String = String.Empty

            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            ErrorMessage = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Trim <> String.Empty Then
                            strKodeDealer = sTemp.Trim
                        Else
                            ErrorMessage.Append("Invalid Kode Dealer" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If sTemp.Trim <> String.Empty Then
                            strStatus = sTemp.Trim
                        Else
                            ErrorMessage.Append("Invalid Status" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        If sTemp.Trim <> String.Empty Then
                            strNoReg = sTemp.Trim
                        Else
                            ErrorMessage.Append("Invalid NoReg" & Chr(13) & Chr(10))
                        End If
                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next

            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If

            If strNoReg <> String.Empty AndAlso strStatus <> String.Empty AndAlso strStatus.ToLower().Trim().Equals(EnumDepositA.StatusKuitansi.Cair.ToString().ToLower()) Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "NoReg", MatchType.Exact, strNoReg))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "Dealer.DealerCode", MatchType.Exact, strKodeDealer))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "Status", MatchType.Exact, CInt(EnumDepositA.StatusKuitansi.Selesai)))
                Dim arrDepositKuitansi As ArrayList = New DepositAKuitansiPencairanFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))).Retrieve(criterias)
                If arrDepositKuitansi.Count > 0 Then
                    objDepositAKuitansiPencairan = New DepositAKuitansiPencairan
                    objDepositAKuitansiPencairan = arrDepositKuitansi(0)
                    _prevStatus.Add(objDepositAKuitansiPencairan.Status)
                    objDepositAKuitansiPencairan.Status = CInt(EnumDepositA.StatusKuitansi.Cair)
                    _JVs.Add(objDepositAKuitansiPencairan)

                End If
            End If
        End Sub
#End Region
    End Class
End Namespace