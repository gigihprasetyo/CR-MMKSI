#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
#End Region

Namespace KTB.DNet.Parser

    Public Class SAPTestingParser
        Inherits AbstractParser

#Region "Private Variables"
        Private status As String
        Private _Stream As StreamReader
        Private PDI As ArrayList
        Private Grammar As Regex
        Private _sessHelper As SessionHelper = New SessionHelper
        Private _fileName
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(",")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            '_Stream = New StreamReader(fileName, True)
            'PDI = New ArrayList
            '_fileName = fileName
            'Dim val As String = MyBase.NextLine(_Stream).Trim()
            '   While (Not val = "")
            'ParsePDI(val + delimited)
            Try
                Throw New Exception("SAP Testing Succes")
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SAPTestingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PDIParser, BlockName)
            End Try
            'Val = MyBase.NextLine(_Stream)
            'End While
            '_Stream.Close()
            '_Stream = Nothing
            Return New Object
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Try
                Throw New Exception("SAP Testing Succes")
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SAPTestingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PDIParser, BlockName)
            End Try
            'If PDI.Count > 0 Then
            '    'Do Business - Like save to database
            'End If
            Return 1
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Try
                Throw New Exception("SAP Testing Succes")
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SAPTestingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PDIParser, BlockName)
            End Try
        End Function

#End Region

#Region "Private Methods"

        
#End Region

    End Class

End Namespace