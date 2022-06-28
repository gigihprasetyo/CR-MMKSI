#Region "Summary"
'// ===========================================================================		
'// Author Name   : 
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2005
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser
    Public Class SPPendingOrderParser
        Inherits AbstractParser

#Region "Private Constants"
        Private Const DELIMITER As String = " "
        Private Const INDEX_PO_NUMBER As Integer = 2
        Private Const INDEX_SO_NUMBER As Integer = 3
        Private Const INDEX_SO_DATE As Integer = 4
        Private Const INDEX_AMOUNT As Integer = 5
        Private Const INDEX_TAX As Integer = 6
        Private Const INDEX_TOTAL As Integer = 7

#End Region

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _sparePartPendingOrder As SparePartPendingOrder
        Private _PendingOrders As ArrayList
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private ErrorMessages As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            'DoParseFixFormatFile(fileName, user)
            Try
                DoParseFixFormatFile(fileName, user)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPPendingOrderParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPPendingOrderParser, BlockName)
            End Try

        End Function

        Protected Overrides Function DoTransaction() As Integer
            If _PendingOrders.Count > 0 Then
                For Each spPOHeader As SparePartPendingOrder In _PendingOrders
                    Try
                        Dim nResult = New SparePartPendingOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).InsertFromWindowSevice(spPOHeader)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPPendingOrderParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPPendingOrderParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & spPOHeader.SONumber & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                Next
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            _fileName = fileName
            Try
                _Stream = New StreamReader(fileName, True)
                _PendingOrders = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                Dim sStart As Integer
                Dim nCount As Integer
                While (Not val = "")
                    Try
                        sStart = 0
                        nCount = 0
                        If Not _sparePartPendingOrder Is Nothing Then
                            _PendingOrders.Add(_sparePartPendingOrder)
                        End If
                        _sparePartPendingOrder = ParseSPPOHeader(val)


                    Catch ex As Exception
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While

                If Not _sparePartPendingOrder Is Nothing Then
                    _PendingOrders.Add(_sparePartPendingOrder)
                End If
            Finally
                _Stream.Close()
                _Stream = Nothing
            End Try
            Return _PendingOrders
        End Function

#End Region

#Region "Private Methods"

        Private Function ParseSPPOHeader(ByVal val As String) As SparePartPendingOrder
            ErrorMessages = New StringBuilder
            Dim allVal As String() = val.Split(DELIMITER.ToCharArray, StringSplitOptions.RemoveEmptyEntries)

            Dim poNumber As String = allVal.GetValue(INDEX_PO_NUMBER)
            Dim sPPO As SparePartPO = New SparePartPOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(poNumber)
            If Not IsNothing(sPPO) Then
                Dim objSPPOPendingOrder As New SparePartPendingOrder
                objSPPOPendingOrder.SparePartPO = sPPO
                objSPPOPendingOrder.SONumber = allVal.GetValue(INDEX_SO_NUMBER).Trim()
                objSPPOPendingOrder.DocumentType = "N" 'Document type B=Back order; N=Normal
                objSPPOPendingOrder.Amount = allVal.GetValue(INDEX_AMOUNT)
                objSPPOPendingOrder.Tax = allVal.GetValue(INDEX_TAX)
                objSPPOPendingOrder.Total = allVal.GetValue(INDEX_TOTAL)
                Try
                    Dim dateValue = allVal.GetValue(INDEX_SO_DATE)
                    objSPPOPendingOrder.SODate = New Date(dateValue.Substring(0, 4), dateValue.Substring(4, 2), dateValue.Substring(6, 2))
                Catch ex As Exception
                    ErrorMessages.Append("Invalid SO Date" & Chr(13) & Chr(10))
                End Try
                objSPPOPendingOrder.RowStatus = 0
                objSPPOPendingOrder.ErrorMessage = ""
                objSPPOPendingOrder.CreatedBy = "WSM"

                If ErrorMessages.Length > 0 Then
                    Throw New Exception(ErrorMessages.ToString)
                Else
                    Return objSPPOPendingOrder
                End If
            End If
            Return Nothing
        End Function



#End Region

#Region "Public Properties"

        ReadOnly Property SparePartPurchaseOrder() As SparePartPendingOrder
            Get
                Return _sparePartPendingOrder
            End Get
        End Property

#End Region

    End Class

End Namespace