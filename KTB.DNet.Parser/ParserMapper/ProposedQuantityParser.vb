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
    Public Class ProposedQuantityParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private ProposedQuantities As ArrayList
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
            Dim dtValid As DateTime = Now
            Try
                Dim sDate As String = String.Empty
                Dim iStart As Integer
                Try
                    'file sample : propose_data20120418104922.txt
                    sDate = fileName.Substring("propose_data".Length)
                    dtValid = DateSerial(sDate.Substring(0, 4) _
                        , sDate.Substring(4, 2) _
                        , sDate.Substring(6, 2))
                    dtValid = CType(dtValid.ToString("yyyy/MM/dd 00:00:00"), DateTime)
                    dtValid.AddHours(sDate.Substring(8, 2))
                    dtValid.AddMinutes(sDate.Substring(10, 2))
                    dtValid.AddSeconds(sDate.Substring(12, 2))
                Catch ex As Exception
                    dtValid = Now
                End Try

                _fileName = fileName
                _Stream = New StreamReader(fileName, True)
                ProposedQuantities = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                While (Not val = "")
                    Try
                        ParseProposedQuantity(val + delimited)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ProposedQuantityParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ProposedQuantityParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While
            Finally
                _Stream.Close()
                _Stream = Nothing
                For Each oP As PPQty In ProposedQuantities
                    oP.ValidatedTime = dtValid
                Next
            End Try
            Return ProposedQuantities
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim poColl As ArrayList
            Dim _ppQtyFacade As New PPQtyFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            If ProposedQuantities.Count > 0 Then
                For Each Deleteditem As PPQty In ProposedQuantities
                    Try

                        _ppQtyFacade.DeletedPPQty(Deleteditem)

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ProposedQuantityParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ProposedQuantityParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & Deleteditem.MaterialNumber & ":" & Deleteditem.DealerCode & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        Return 1
                    End Try
                Next

                _ppQtyFacade = New PPQtyFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Try
                    _ppQtyFacade.SynchronizeQuantity(ProposedQuantities)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ProposedQuantityParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ProposedQuantityParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                'For Each item As PPQty In ProposedQuantities
                '    _ppQtyFacade = New PPQtyFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                '    Try
                '        _ppQtyFacade.SynchronizeQuantity(item)
                '    Catch ex As Exception
                '        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ProposedQuantityParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ProposedQuantityParser, BlockName)
                '        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.MaterialNumber & ":" & item.DealerCode & Chr(13) & Chr(10) & ex.Message)
                '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                '    End Try
                'Next
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseProposedQuantity(ByVal ValParser As String)
            Dim _ppqty As PPQty = New PPQty
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim i As Integer = 0
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        _ppqty.PeriodeDate = sTemp.Trim.Substring(0, 2)
                        _ppqty.PeriodeMonth = sTemp.Trim.Substring(2, 2)
                        _ppqty.PeriodeYear = sTemp.Trim.Substring(4, 4)
                    Case Is = 1
                        _ppqty.DealerCode = sTemp
                    Case Is = 2
                        _ppqty.MaterialNumber = sTemp
                    Case Is = 3
                        _ppqty.AllocationQty = sTemp
                    Case Is = 4
                        _ppqty.ProductionYear = sTemp
                    Case Else
                End Select
                sStart = m.Index + 1
                nCount += 1
                If i = 0 AndAlso 1 = 2 Then 'remove this logic, by:dna;For:yurike;On:20110730;KTB Masok..
                    'Dim objNationalHolidayFacade As New NationalHolidayFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    If (New NationalHolidayFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).IsActiveDateExist(_ppqty.PeriodeYear, _ppqty.PeriodeMonth, _ppqty.PeriodeDate) <> 0) Then
                        Throw New Exception("Hari ini hari libur!!")
                        Exit Sub
                    End If
                End If
                i += 1
            Next
            ProposedQuantities.Add(_ppqty)

        End Sub

#End Region

#Region "Public Properties"

        ReadOnly Property ProposedQuantityCollection() As ArrayList
            Get
                Return ProposedQuantities
            End Get
        End Property

#End Region

    End Class
End Namespace