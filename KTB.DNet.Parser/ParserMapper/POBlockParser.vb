
#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Threading.Thread
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class POBlockParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private POHeaders As ArrayList
        Private Grammar As Regex
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                _fileName = fileName
                _Stream = New StreamReader(fileName, True)
                POHeaders = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                While (Not val = "")
                    Try
                        ParsePOHeader(val + delimited)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "POBlockParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.POBlockParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While
            Finally
                _Stream.Close()
                _Stream = Nothing
            End Try
            Return POHeaders
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim _poFacade As POHeaderFacade
            If POHeaders.Count > 0 Then
                For Each item As POHeader In POHeaders
                    _poFacade = New POHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    Try
                        _poFacade.UpdateSingleDomainTransaction(item)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "POBlockParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.POBlockParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.PONumber & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                Next
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParsePOHeader(ByVal ValParser As String)
            Dim _poHeader As POHeader = New POHeader
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
                        Dim _regPONumber = sTemp
                        Dim poFacade As POHeaderFacade = New POHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        Dim po As POHeader = poFacade.Retrieve(_regPONumber)
                        If Not po Is Nothing Then
                            _poHeader = po
                            _poHeader.Status = enumStatusPO.Status.DiBlok
                            For Each item As PODetail In _poHeader.PODetails
                                item.AllocQty = 0
                            Next
                        End If
                    Case Else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            POHeaders.Add(_poHeader)
        End Sub

#End Region

#Region "Public Properties"

        ReadOnly Property POHeaderCollection() As ArrayList
            Get
                Return POHeaders
            End Get
        End Property

#End Region

    End Class
End Namespace
