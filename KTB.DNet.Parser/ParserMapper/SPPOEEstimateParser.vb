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
#End Region

Namespace KTB.DNet.Parser
    Public Class SPPOEEstimateParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _sparePartPOEstimate As SparePartPOEstimate
        Private _PoEstimates As ArrayList
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private ErrorMessages As stringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            DocumentType = "N"
        End Sub

#End Region

#Region "Protected Methods"

        Property DocumentType As String

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            'DoParseFixFormatFile(fileName, user)
            Try
                DoParseFixFormatFile(fileName, user)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPPOEEstimateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPPOEEstimateParser, BlockName)
            End Try

        End Function

        Protected Overrides Function DoTransaction() As Integer
            If _PoEstimates.Count > 0 Then
                For Each spPOHeader As SparePartPOEstimate In _PoEstimates
                    Try
                        Dim nResult = New SparePartPOEstimateFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).InsertFromWindowSevice(spPOHeader)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPPOEEstimateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPPOEEstimateParser, BlockName)
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
                _PoEstimates = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                Dim sStart As Integer
                Dim nCount As Integer
                Dim sTemp As String
                While (Not val = "")
                    Try
                        sStart = 0
                        nCount = 0
                        Dim indicator As String = val.Substring(9, 1)
                        If indicator = "H" Then
                            If Not _sparePartPOEstimate Is Nothing Then
                                _PoEstimates.Add(_sparePartPOEstimate)  'customer header input text
                            End If
                            _sparePartPOEstimate = ParseSPPOHeader(val)

                        Else
                            If Not IsNothing(_sparePartPOEstimate) Then
                                ParseSPPODetail(val)   'Order detail input
                            End If
                        End If

                    Catch ex As Exception
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While

                If Not _sparePartPOEstimate Is Nothing Then
                    _PoEstimates.Add(_sparePartPOEstimate)
                End If
            Finally
                _Stream.Close()
                _Stream = Nothing
            End Try
            Return _PoEstimates
        End Function

#End Region

#Region "Private Methods"

        Private Function ParseSPPOHeader(ByVal val As String) As SparePartPOEstimate
            ErrorMessages = New StringBuilder
            Dim poNumber As String = val.Substring(10, 15).Trim()
            Dim sPPO As SparePartPO = New SparePartPOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrievePO(poNumber)
            If Not IsNothing(sPPO) Then
                If sPPO.ProcessCode = "S" OrElse sPPO.ProcessCode = "P" Then
                    Dim objSPPOEstimate As New SparePartPOEstimate
                    objSPPOEstimate.SparePartPO = sPPO
                    objSPPOEstimate.SONumber = val.Substring(26, 10).Trim()
                    objSPPOEstimate.DocumentType = DocumentType
                    If val.Substring(0, 3).Trim() = "PEO" Then
                        Try
                            objSPPOEstimate.SODate = New Date(val.Substring(102, 4), val.Substring(106, 2), val.Substring(108, 2))
                        Catch ex As Exception
                            ErrorMessages.Append("Invalid PEO SO Date" & Chr(13) & Chr(10))
                        End Try
                        objSPPOEstimate.RowStatus = 0
                        Try
                            objSPPOEstimate.DeliveryDate = New Date(val.Substring(111, 4), val.Substring(115, 2), val.Substring(117, 2))
                        Catch ex As Exception
                            ErrorMessages.Append("Invalid PEO Delivery Date" & Chr(13) & Chr(10))
                        End Try
                        objSPPOEstimate.ErrorMessage = ""
                        objSPPOEstimate.CreatedBy = "WSM"
                    Else
                        Try
                            objSPPOEstimate.SODate = New Date(val.Substring(109, 4), val.Substring(113, 2), val.Substring(115, 2))
                        Catch ex As Exception
                            ErrorMessages.Append("Invalid RO SO Date" & Chr(13) & Chr(10))
                        End Try
                        objSPPOEstimate.RowStatus = 0
                        Try
                            objSPPOEstimate.DeliveryDate = New Date(val.Substring(120, 4), val.Substring(124, 2), val.Substring(126, 2))
                        Catch ex As Exception
                            ErrorMessages.Append("Invalid RO Delivery Date" & Chr(13) & Chr(10))
                        End Try
                        objSPPOEstimate.ErrorMessage = ""
                        objSPPOEstimate.CreatedBy = "WSM"
                    End If
                    If ErrorMessages.Length > 0 Then
                        Throw New Exception(ErrorMessages.ToString)
                    Else
                        Return objSPPOEstimate
                    End If
                End If
            End If
            Return Nothing
        End Function

        Private Sub ParseSPPODetail(ByVal val As String)
            val = val.Replace("''", " ")
            ErrorMessages = New StringBuilder
            Dim sparePartPOEstimateDetail As New sparePartPOEstimateDetail
            sparePartPOEstimateDetail.PartNumber = val.Substring(37, 15).Trim()
            sparePartPOEstimateDetail.PartName = val.Substring(53, 15).Trim()
            Try
                sparePartPOEstimateDetail.RetailPrice = CType(val.Substring(97, 10).Trim(), Decimal)
            Catch ex As Exception
                ErrorMessages.Append("Invalid Retail Price" & Chr(13) & Chr(10))
            End Try
            Try
                sparePartPOEstimateDetail.OrderQty = CType(val.Substring(69, 6).Trim(), Integer)
            Catch ex As Exception
                ErrorMessages.Append("Invalid Order Qty" & Chr(13) & Chr(10))
            End Try
            Try
                sparePartPOEstimateDetail.AllocationQty = CType(val.Substring(76, 6).Trim(), Integer)
            Catch ex As Exception
                ErrorMessages.Append("Invalid Allocation Qty" & Chr(13) & Chr(10))
            End Try
            Try
                sparePartPOEstimateDetail.OpenQty = CType(val.Substring(83, 6).Trim(), Integer)
            Catch ex As Exception
                ErrorMessages.Append("Invalid Open Qty" & Chr(13) & Chr(10))
            End Try
            Try
                sparePartPOEstimateDetail.AllocQty = CType(val.Substring(90, 6).Trim(), Integer)
            Catch ex As Exception
                ErrorMessages.Append("Invalid Alloc Qty" & Chr(13) & Chr(10))
            End Try
            sparePartPOEstimateDetail.AltPartNumber = val.Substring(120, 15).Trim()
            Try
                sparePartPOEstimateDetail.Discount = CType(val.Substring(143, 4).Trim(), Decimal)
            Catch ex As Exception
                ErrorMessages.Append("Invalid Discount" & Chr(13) & Chr(10))
            End Try
            sparePartPOEstimateDetail.CreatedBy = "WSM"

            If ErrorMessages.Length > 0 Then
                Throw New Exception(ErrorMessages.ToString)
            Else
                _sparePartPOEstimate.SparePartPOEstimateDetails.Add(sparePartPOEstimateDetail)
            End If
        End Sub

#End Region

#Region "Public Properties"

        ReadOnly Property SparePartPurchaseOrder() As SparePartPOEstimate
            Get
                Return _sparePartPOEstimate
            End Get
        End Property

#End Region

    End Class

End Namespace