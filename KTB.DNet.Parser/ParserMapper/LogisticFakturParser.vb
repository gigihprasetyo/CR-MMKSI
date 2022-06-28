#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.PO
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser
    Public Class LogisticFakturParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _aDHs As ArrayList
        Private _aDDs As ArrayList
        Private _aDRs As ArrayList
        Private _fileName As String
        Private _oDH As LogisticFee
        Private _oDR As SalesOrder
        Private _oDD As LogisticDCDetail
        Private ErrorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Private Class ParsedData
            Public DealerCode As String
            Public DebitMemoNo As String
            Public LogisticDN As LogisticDN
            Public FakturNo As String
            Public PostingDate As Date
        End Class

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                _fileName = fileName
                stream = New StreamReader(fileName, True)
                _aDHs = New ArrayList
                Dim val As String = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        _oDH = ParserHeader(val + Delimited)
                        If Not IsNothing(_oDH) Then
                            _aDHs.Add(_oDH)
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "LogisticFakturParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.LogisticDCParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oDH = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While

            Finally

                stream.Close()
                stream = Nothing
            End Try
            Return _aDHs

        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim user As New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oDHFac As New LogisticFeeFacade(user)
            Dim oDHOld As LogisticFee = New LogisticFee
            'Dim Idx As Integer = 0

            'For Idx = 0 To Idx < _aDHs.Count - 1
            'Dim oDH As LogisticFee = _aDHs(Idx)
            For Each oDH As LogisticFee In _aDHs
                Try

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.LogisticFee), "LogisticDN.ID", MatchType.Exact, oDH.LogisticDN.ID))
                    Dim sortColl As SortCollection = New SortCollection
                    sortColl.Add(New Sort(GetType(LogisticFee), "ID", Sort.SortDirection.DESC))
                    Dim dnArray As ArrayList = New LogisticFeeFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))).RetrieveByCriteria(criterias, sortColl)
                    Dim logisticFee As LogisticFee
                    If dnArray.Count > 0 Then
                        logisticFee = dnArray(0)
                    End If

                    oDHOld = logisticFee
                    If Not IsNothing(oDHOld) AndAlso oDHOld.ID > 0 Then
                        oDHOld.FakturPajakNo = oDH.FakturPajakNo
                        oDHFac.Update(oDHOld)
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "LogisticFakturParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.LogisticDCParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & oDH.FakturPajakNo & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try

            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            'Nothing TODO
        End Function

#End Region

#Region "Private Methods"

        Private Function ParserHeader(ByVal ValParser As String) As LogisticFee
            _oDH = New LogisticFee

            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim user As New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oLDNFac As New LogisticDNFacade(user)

            sStart = 0
            nCount = 0
            ErrorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0 'Dealer Code
                    Case Is = 1 'DNNumber
                        Dim oLDN As LogisticDN = oLDNFac.Retrieve(sTemp)
                        If Not IsNothing(oLDN) AndAlso oLDN.ID > 0 Then
                            _oDH.LogisticDN = oLDN
                        Else
                            ErrorMessage.Append("Invalid Debit Memo Number" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2 'No Faktur Pajak

                        _oDH.FakturPajakNo = sTemp
                    Case Else
                        'do nothing
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If

            _oDH.CreatedBy = "SAP"
            Return _oDH
        End Function


#End Region

    End Class

End Namespace