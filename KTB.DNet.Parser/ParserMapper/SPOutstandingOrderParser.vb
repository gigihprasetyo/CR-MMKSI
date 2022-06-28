#Region "Summary"
'// ===========================================================================		
'// Author Name   : Heru
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
Imports KTB.DNet.BusinessFacade

#End Region

Namespace KTB.DNet.Parser
    Public Class SPOutstandingOrderParser
        Inherits AbstractParser

#Region "Private Constants"
        Private Const DELIMITER As String = Chr(9)
        Private Const INDEX_ORDER_TYPE As Integer = 0 'R E or etc
        Private Const INDEX_ROWTYPE As Integer = 2 'H or L

        Private Const INDEX_H_PO_NUMBER As Integer = 3
        Private Const INDEX_H_SO_NUMBER As Integer = 4
        Private Const INDEX_H_PO_DATE As Integer = 5
        Private Const INDEX_H_VALID_TO As Integer = 6

        Private Const INDEX_L_SO_NUMBER As Integer = 3
        Private Const INDEX_L_SPAREPART_NUMBER As Integer = 4
        Private Const INDEX_L_SPAREPART_DESC As Integer = 5
        Private Const INDEX_L_INQUIRY_QTY As Integer = 6
        Private Const INDEX_L_ALLOCATION_QTY As Integer = 7
        Private Const INDEX_L_OPEN_QTY As Integer = 8
        Private Const INDEX_L_ALLOCATION_AMT As Integer = 9
        Private Const INDEX_L_OPEN_AMT As Integer = 10
        Private Const INDEX_L_SUBSTITUTE As Integer = 11
        Private Const INDEX_L_ESTIMATEFILLQTY As Integer = 12
        Private Const INDEX_L_ESTIMATEFILLDATE As Integer = 13

#End Region

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _sparePartOutstandingOrder As SparePartOutstandingOrder
        Private _PoOutstanding As ArrayList
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
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPOutstansdingOrderParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPOutstansdingOrderParser, BlockName)
            End Try

        End Function

        Protected Overrides Function DoTransaction() As Integer
            If _PoOutstanding.Count > 0 Then
                For Each spPOHeader As SparePartOutstandingOrder In _PoOutstanding
                    Try
                        Dim nResult = New SparePartOutstandingOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).InsertFromWindowSevice(spPOHeader)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPOutstansdingOrderParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPOutstansdingOrderParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & ex.Message)
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
                _PoOutstanding = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                Dim sStart As Integer
                Dim nCount As Integer
                Dim sTemp As String
                While (Not val = "")
                    Try
                        sStart = 0
                        nCount = 0
                        'Dim allVal As String() = val.Split(DELIMITER.ToCharArray, StringSplitOptions.RemoveEmptyEntries)
                        Dim allVal As String() = val.Split(DELIMITER.ToCharArray, StringSplitOptions.None)
                        Dim indicator As String = allVal.GetValue(INDEX_ROWTYPE)
                        If indicator = "H" Then
                            If Not _sparePartOutstandingOrder Is Nothing Then
                                _PoOutstanding.Add(_sparePartOutstandingOrder)  'customer header input text
                            End If
                            _sparePartOutstandingOrder = ParseSPPOHeader(allVal)

                        Else
                            If Not IsNothing(_sparePartOutstandingOrder) Then
                                ParseSPPODetail(allVal)   'Order detail input
                            End If
                        End If

                    Catch ex As Exception
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While

                If Not _sparePartOutstandingOrder Is Nothing Then
                    _PoOutstanding.Add(_sparePartOutstandingOrder)
                End If
            Finally
                _Stream.Close()
                _Stream = Nothing
            End Try
            Return _PoOutstanding
        End Function

#End Region

#Region "Private Methods"

        Private Function ParseSPPOHeader(ByVal val As String()) As SparePartOutstandingOrder
            ErrorMessages = New StringBuilder
            Dim poNumber As String = val.GetValue(INDEX_H_PO_NUMBER).Trim()
            Dim sPPO As SparePartPO = New SparePartPOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(poNumber)
            If Not IsNothing(sPPO) Then
                If sPPO.ProcessCode = "S" OrElse sPPO.ProcessCode = "P" Then
                    Dim objSPOutstanding As New SparePartOutstandingOrder
                    objSPOutstanding.SparePartPO = sPPO
                    objSPOutstanding.RowStatus = 0
                    objSPOutstanding.DocumentType = "B"
                    Try
                        objSPOutstanding.OrderType = val.GetValue(INDEX_ORDER_TYPE).Trim().ToCharArray().GetValue(1)

                    Catch ex As Exception
                        ErrorMessages.Append("Invalid Order type" & Chr(13) & Chr(10))

                    End Try
                    Try
                        Dim adate As String() = val.GetValue(INDEX_H_PO_DATE).ToString().Split(".")
                        objSPOutstanding.PODate = New Date(adate.GetValue(2),
                                                          adate.GetValue(1),
                                                          adate.GetValue(0))
                    Catch ex As Exception

                        ErrorMessages.Append("Invalid Header PO Date" & Chr(13) & Chr(10))
                    End Try
                    Try
                        Dim adate As String() = val.GetValue(INDEX_H_VALID_TO).ToString().Split(".")
                        objSPOutstanding.ValidTo = New Date(adate.GetValue(2),
                                                       adate.GetValue(1),
                                                      adate.GetValue(0))

                    Catch ex As Exception
                        ErrorMessages.Append("Invalid Header Valid to Date" & Chr(13) & Chr(10))

                    End Try

                    objSPOutstanding.ErrorMessage = ""
                    objSPOutstanding.CreatedBy = "WSM"


                    If ErrorMessages.Length > 0 Then
                        Throw New Exception(ErrorMessages.ToString)
                    Else
                        Return objSPOutstanding
                    End If
                End If
            End If
            Return Nothing
        End Function

        Private Sub ParseSPPODetail(ByVal val As String())
            ErrorMessages = New StringBuilder
            Dim spareParttOutstandingOrderDetail As New SparePartOutstandingOrderDetail
            spareParttOutstandingOrderDetail.PartNumber = val.GetValue(INDEX_L_SPAREPART_NUMBER).Trim()
            spareParttOutstandingOrderDetail.PartName = val.GetValue(INDEX_L_SPAREPART_DESC).Trim()
            If (val.Length > 11) Then 'has substitute, else doesn't have it
                spareParttOutstandingOrderDetail.SubtitutePartNumber = val.GetValue(INDEX_L_SUBSTITUTE).Trim()
            Else
                spareParttOutstandingOrderDetail.SubtitutePartNumber = ""
            End If
            spareParttOutstandingOrderDetail.RowStatus = 0

            Try
                spareParttOutstandingOrderDetail.OrderQty = CType(val.GetValue(INDEX_L_INQUIRY_QTY).Trim(), Integer)
            Catch ex As Exception
                ErrorMessages.Append("Invalid Order Qty" & Chr(13) & Chr(10))
            End Try
            Try
                spareParttOutstandingOrderDetail.AllocationQty = CType(val.GetValue(INDEX_L_ALLOCATION_QTY).Trim(), Integer)
            Catch ex As Exception
                ErrorMessages.Append("Invalid Allocation Qty" & Chr(13) & Chr(10))
            End Try
            Try
                spareParttOutstandingOrderDetail.OpenQty = CType(val.GetValue(INDEX_L_OPEN_QTY).Trim(), Integer)
            Catch ex As Exception
                ErrorMessages.Append("Invalid Open Qty" & Chr(13) & Chr(10))
            End Try
            Try
                spareParttOutstandingOrderDetail.OpenAmount = CType(val.GetValue(INDEX_L_OPEN_AMT).Trim(), Decimal)
            Catch ex As Exception
                ErrorMessages.Append("Invalid Open Amount" & Chr(13) & Chr(10))
            End Try
            Try
                spareParttOutstandingOrderDetail.AllocationAmount = CType(val.GetValue(INDEX_L_ALLOCATION_AMT).Trim(), Decimal)
            Catch ex As Exception
                ErrorMessages.Append("Invalid Allocation Amount" & Chr(13) & Chr(10))
            End Try

            If (val.Length > 12) Then 'has estimatefillqty, else doesn't have it
                spareParttOutstandingOrderDetail.EstimateFillQty = val.GetValue(INDEX_L_ESTIMATEFILLQTY).Trim()
            Else
                spareParttOutstandingOrderDetail.EstimateFillQty = 0
            End If
            If (val.Length > 13) Then 'has estimatefilldate, else doesn't have it
                Try
                    Dim adate As String() = val.GetValue(INDEX_L_ESTIMATEFILLDATE).ToString().Split(".")
                    spareParttOutstandingOrderDetail.EstimateFillDate = New Date(adate.GetValue(2),
                                                                                  adate.GetValue(1),
                                                                                  adate.GetValue(0))
                Catch ex As Exception
                    ErrorMessages.Append("Invalid Estimate Fill Date" & Chr(13) & Chr(10))
                End Try
            Else
                spareParttOutstandingOrderDetail.EstimateFillDate = Nothing
            End If

            spareParttOutstandingOrderDetail.CreatedBy = "WSM"

            If ErrorMessages.Length > 0 Then
                Throw New Exception(ErrorMessages.ToString)
            Else
                _sparePartOutstandingOrder.SparePartOutstandingOrderDetails.Add(spareParttOutstandingOrderDetail)
            End If
        End Sub

#End Region

#Region "Public Properties"

        ReadOnly Property SparePartPurchaseOrder() As SparePartOutstandingOrder
            Get
                Return _sparePartOutstandingOrder
            End Get
        End Property

#End Region

    End Class

End Namespace