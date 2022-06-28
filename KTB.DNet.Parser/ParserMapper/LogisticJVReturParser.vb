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
    Public Class LogisticJVReturParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _aDHs As ArrayList
        Private _aDDs As ArrayList
        Private _aDRs As ArrayList
        Private _fileName As String
        Private _oDH As LogisticPPHHeader
        Private ErrorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

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
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "LogisticJVReturParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.LogisticDCParser, BlockName)
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
            Dim oDHFac As New LogisticPPHHeaderFacade(user)
            'Dim Idx As Integer = 0

            'For Idx = 0 To Idx < _aDHs.Count - 1
            For Each oDH As LogisticPPHHeader In _aDHs
                'Dim oDH As LogisticPPHHeader = _aDHs(Idx)
                Try
                    If (oDH.ID > 0) Then
                        oDH.ID = oDHFac.Update(oDH)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "LogisticJVReturParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.LogisticDCParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & oDH.NoReg & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try

            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            'Nothing TODO
        End Function

#End Region

#Region "Private Methods"

        Private Function ParserHeader(ByVal ValParser As String) As LogisticPPHHeader
            _oDH = New LogisticPPHHeader

            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim user As New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oLPPHFac As New LogisticPPHHeaderFacade(user)

            sStart = 0
            nCount = 0
            ErrorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0 'Dealer Code
                    Case Is = 1 'RegNo

                        Dim oLDN As LogisticPPHHeader = oLPPHFac.Retrieve(sTemp) 'TODO
                        If Not IsNothing(oLDN) AndAlso oLDN.ID > 0 Then
                            _oDH = oLDN
                        Else
                            ErrorMessage.Append("Invalid No Reg" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3 'ReturnAssignNumber 
                        _oDH.ReturnAssignNumber = sTemp
                        _oDH.Status = EnumPengembalianLogisticPPHStatus.PengembalianPPhStatus.Selesai
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