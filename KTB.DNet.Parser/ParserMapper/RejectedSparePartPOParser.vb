
#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade
#End Region

Namespace KTB.DNet.Parser

    Public Class RejectedSparePartPOParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _RejectedPOColl As ArrayList
        Private Grammar As Regex
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            _RejectedPOColl = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                'val.Replace(";", String.Empty)
                'ParseRejectedPO(val + ";")
                Try
                    val.Replace(";", String.Empty)
                    ParseRejectedPO(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "RejectedSparePartPOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.RejectedSparePartPOParser, BlockName)
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _RejectedPOColl
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim sppoFacade As SparePartPOFacade
            If _RejectedPOColl.Count > 0 Then
                'sppoFacade = New SparePartPOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                'sppoFacade.UpdateSparePartPO(_RejectedPOColl, "T")
                Try
                    sppoFacade = New SparePartPOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    sppoFacade.UpdateSparePartPO(_RejectedPOColl, "T")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "RejectedSparePartPOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.RejectedSparePartPOParser, BlockName)
                End Try
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseRejectedPO(ByVal ValParser As String)
            Dim _SPPO As SparePartPO
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        _SPPO = New SparePartPOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                    Case Else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If Not _SPPO Is Nothing Then
                _RejectedPOColl.Add(_SPPO)
            End If
        End Sub

#End Region

    End Class

End Namespace