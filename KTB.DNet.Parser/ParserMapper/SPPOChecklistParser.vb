#Region "Summary"
'// ===========================================================================		
'// Author Name   : Agus Pirnadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 18/10/2005
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser

    Public Class SPPOChecklistParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader      '-- Data file to parse
        Private _sparePartPO As SparePartPO  '-- Sparepart PO object
        Private _filename As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _filename = fileName
            'DoParseFixFormatFile(fileName, user)
            Try
                DoParseFixFormatFile(fileName, user)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPPOChecklistParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPPOChecklistParser, BlockName)
            End Try
        End Function

        Protected Overrides Function DoTransaction() As Integer
            '-- Update checklist status of all sparepartPO details
            Try
                Dim spPOFacade As New SparePartPOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                spPOFacade.UpdateSPPOChecklist(_sparePartPO)
            Catch ex As Exception
                Dim e As Exception = New Exception(_filename & Chr(13) & Chr(10) & "Update SPPO Header Checklist Status:" & _sparePartPO.PONumber & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPPOChecklistParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPPOChecklistParser, BlockName)
            End Try
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)

            Dim val As String = MyBase.NextLine(_Stream).Trim()
            If val <> "" Then
                ParsingSparepartPOHeader(val)  '-- First line is header

                If Not IsNothing(_sparePartPO) Then  '-- If SparepartPO exists
                    val = MyBase.NextLine(_Stream).Trim()
                    While val <> ""
                        Try
                            ParsingSparepartPODetail(val)  '-- Next subsequent lines are details
                        Catch ex As Exception
                            Dim e As Exception = New Exception(_filename & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        End Try
                        val = MyBase.NextLine(_Stream).Trim()
                    End While
                End If
            End If

            _Stream.Close()
            _Stream = Nothing

            Return _sparePartPO
        End Function

#End Region

#Region "Private Methods"

        Private Sub ParsingSparepartPOHeader(ByVal val As String)
            'Dim poNumber As String = val.Substring(1, 15).Trim()  '-- Get sparepart PO number
            Dim valList As String() = val.ToString.Split(";")
            Dim poNumber As String = Right(valList(0).Trim, valList(0).Trim.Length - 1)
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, poNumber))
                Dim SparepartPOs As ArrayList = New SparePartPOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                If SparepartPOs.Count > 0 Then
                    _sparePartPO = CType(SparepartPOs(0), SparePartPO)  '-- Assign sparepart PO
                    For Each sppoDetail As SparePartPODetail In _sparePartPO.SparePartPODetails
                        sppoDetail.CheckListStatus = "1"  '-- Default is "approved"
                    Next
                End If
            Catch ex As Exception
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try

            
        End Sub

        Private Sub ParsingSparepartPODetail(ByVal val As String)
            Dim _sparePartPODetail As SparePartPODetail = New SparePartPODetail
            'Dim partNumber As String = val.Substring(26, 15).Trim()
            Dim valList As String() = val.ToString.Split(";")
            Dim partNumber As String = valList(2).Trim
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SparePartPODetail), "SparePartMaster.PartNumber", MatchType.Exact, partNumber))
                criterias.opAnd(New Criteria(GetType(SparePartPODetail), "SparePartPO.PONumber", MatchType.Exact, _sparePartPO.PONumber))
                Dim SPPODetails As ArrayList = New SparePartPODetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                Dim sStopMark As String = "K" 'EnumStopMark.StopMark.K
                If valList.Length >= 6 AndAlso (valList(5).Trim.ToUpper = "K" OrElse valList(5).Trim.ToUpper = "X") Then sStopMark = valList(5)

                If SPPODetails.Count > 0 Then
                    _sparePartPODetail = CType(SPPODetails(0), SparePartPODetail)  '-- PO Detail rejected

                    For Each sppoDetail As SparePartPODetail In _sparePartPO.SparePartPODetails
                        If sppoDetail.ID = _sparePartPODetail.ID Then
                            '-- If PO detail rejected exists in PO Detail collection
                            '-- then reject this PO detail
                            sppoDetail.CheckListStatus = "0"
                            Dim StopMarkValue As Short
                            If sStopMark.Trim.ToUpper = "K" Then
                                StopMarkValue = EnumStopMark.StopMark.K
                            ElseIf sStopMark.Trim.ToUpper = "X" Then
                                StopMarkValue = EnumStopMark.StopMark.X
                            End If
                            sppoDetail.StopMark = StopMarkValue
                        End If
                    Next
                End If

            Catch ex As Exception
                Throw ex
            End Try

            
        End Sub

#End Region

    End Class

End Namespace