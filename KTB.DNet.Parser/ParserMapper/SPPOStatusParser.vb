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
Imports System.Text
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class SPPOStatusParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _sparePartPOStatus As SparePartPOStatus
        Private _POStatus As ArrayList
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private _sbMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _fileName = fileName
            Try
                DoParseFixFormatFile(fileName, user)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPPOStatusParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPPOStatusParser, BlockName)
            End Try
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If _POStatus.Count > 0 Then
                Dim spPOStatusFacade As SparePartPOStatusFacade
                Dim nResult As Integer
                For Each spPOStatus As SparePartPOStatus In _POStatus
                    Try
                        spPOStatusFacade = New SparePartPOStatusFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        nResult = spPOStatusFacade.InsertFromWindowSevice(spPOStatus)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SPPOStatusParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SPPOStatusParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & "Insert SP PO Status :" & spPOStatus.SparePartPO.PONumber & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    Finally
                        spPOStatusFacade = Nothing
                    End Try
                Next
                Return nResult
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Try
                _Stream = New StreamReader(fileName, True)
                _POStatus = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                Dim sStart As Integer
                Dim nCount As Integer
                Dim sTemp As String
                While (Not val = "")
                    val = val.Replace("''", " ")
                    sStart = 0
                    nCount = 0
                    Try
                        Dim indicator As String = val.Substring(0, 1)
                        If indicator = "H" Then
                            If Not _sparePartPOStatus Is Nothing Then
                                _POStatus.Add(_sparePartPOStatus)  'customer header input text
                            End If
                            _sparePartPOStatus = ParseSPPOHeaderNew(val)

                        Else
                            If Not IsNothing(_sparePartPOStatus) Then
                                ParseSPPODetailNew(val)
                            End If
                        End If
                    Catch ex As Exception
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try

                    val = MyBase.NextLine(_Stream)
                End While

                If Not _sparePartPOStatus Is Nothing Then
                    _POStatus.Add(_sparePartPOStatus)
                End If
                If Not _Stream Is Nothing Then
                    _Stream.Close()
                    _Stream = Nothing
                End If

            Catch e As Exception
                Throw e
            Finally
                If Not _Stream Is Nothing Then
                    _Stream.Close()
                    _Stream = Nothing
                End If
            End Try

            Return _POStatus
        End Function

#End Region

#Region "Private Methods"

        Private Function ParseSPPOHeaderNew(ByVal val As String) As SparePartPOStatus
            'H;PONUMBER;PODATE;SONUMBER;SODATE;STATUS;BILLINGNUMBER;BILLINGDATE;SOLDTOPARTY;DELIVERYDATE;DOCUMENTTYPE(B/N)
            Dim splittedVal As String()
            splittedVal = val.Split(";")
            Dim poNumber As String = splittedVal(1).Trim

            _sbMessage = New StringBuilder
            Try
                Dim sPPO As SparePartPO = New SparePartPOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrievePO(poNumber)
                If Not sPPO Is Nothing Then
                    If sPPO.ProcessCode = "S" OrElse sPPO.ProcessCode = "P" Then
                        Dim objSPPOStatus As New SparePartPOStatus
                        objSPPOStatus.SparePartPO = sPPO
                        objSPPOStatus.MarkLoaded()
                        objSPPOStatus.SONumber = splittedVal(3).Trim
                        Try
                            Dim soDate As String = splittedVal(4).Trim
                            objSPPOStatus.SODate = New Date(soDate.Substring(0, 4), soDate.Substring(4, 2), soDate.Substring(6, 2))
                        Catch ex As Exception
                            _sbMessage.Append("Invalid SO date" & Chr(13) & Chr(10))
                        End Try

                        objSPPOStatus.RowStatus = 0
                        If (splittedVal(7).Trim <> "0") Then
                            Try
                                Dim billDate As String = splittedVal(7).Trim
                                objSPPOStatus.BillingDate = New Date(billDate.Substring(0, 4), billDate.Substring(4, 2), billDate.Substring(6, 2))
                            Catch ex As Exception
                                _sbMessage.Append("Invalid Billing date" & Chr(13) & Chr(10))
                            End Try
                        End If

                        If (splittedVal(9) <> "0") Then
                            Try
                                Dim delDate As String = splittedVal(9)
                                objSPPOStatus.DeliveryDate = New Date(delDate.Substring(0, 4), delDate.Substring(4, 2), delDate.Substring(6, 2))
                            Catch ex As Exception
                                _sbMessage.Append("Invalid Delivery date" & Chr(13) & Chr(10))
                            End Try

                        End If
                        objSPPOStatus.BillingNumber = splittedVal(6).Trim()
                        objSPPOStatus.PackingStatus = splittedVal(5).Trim()

                        'Document type B or N on last item of splitted value
                        Dim documentType As String = splittedVal(splittedVal.Length - 1).ToUpper()
                        If documentType.StartsWith("B") Then
                            objSPPOStatus.DocumentType = "B"
                        ElseIf documentType.StartsWith("N") Then
                            objSPPOStatus.DocumentType = "N"
                        End If

                        objSPPOStatus.CreatedBy = "WSM"
                        If _sbMessage.Length > 0 Then
                            Throw New Exception(_sbMessage.ToString)
                        End If
                        Return objSPPOStatus
                    End If
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return Nothing

        End Function

        Private Function ParseSPPOHeader(ByVal val As String) As SparePartPOStatus
            Dim poNumber As String = val.Substring(1, 15).Trim()
            _sbMessage = New StringBuilder
            Try
                Dim sPPO As SparePartPO = New SparePartPOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(poNumber)
                If Not sPPO Is Nothing Then
                    If sPPO.ProcessCode = "S" OrElse sPPO.ProcessCode = "P" Then
                        Dim objSPPOStatus As New SparePartPOStatus
                        objSPPOStatus.SparePartPO = sPPO
                        objSPPOStatus.MarkLoaded()
                        objSPPOStatus.SONumber = val.Substring(26, 10).Trim()
                        Try
                            objSPPOStatus.SODate = New Date(val.Substring(37, 4), val.Substring(41, 2), val.Substring(43, 2))
                        Catch ex As Exception
                            _sbMessage.Append("Invalid SO date" & Chr(13) & Chr(10))
                        End Try

                        objSPPOStatus.RowStatus = 0
                        If val.Substring(59, 8) <> "00000000" Then
                            Try
                                objSPPOStatus.BillingDate = New Date(val.Substring(59, 4), val.Substring(63, 2), val.Substring(65, 2))
                            Catch ex As Exception
                                _sbMessage.Append("Invalid Billing date" & Chr(13) & Chr(10))
                            End Try

                        End If

                        If val.Substring(75, 8) <> "00000000" Then
                            Try
                                objSPPOStatus.DeliveryDate = New Date(val.Substring(75, 4), val.Substring(79, 2), val.Substring(81, 2))

                            Catch ex As Exception
                                _sbMessage.Append("Invalid Delivery date" & Chr(13) & Chr(10))
                            End Try

                        End If
                        objSPPOStatus.BillingNumber = val.Substring(48, 10).Trim()
                        objSPPOStatus.PackingStatus = val.Substring(46, 1).Trim()
                        objSPPOStatus.CreatedBy = "WSM"
                        If _sbMessage.Length > 0 Then
                            Throw New Exception(_sbMessage.ToString)
                        End If
                        Return objSPPOStatus
                    End If
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return Nothing

        End Function

        Private Sub ParseSPPODetailNew(ByVal val As String)
            'D;SONUMBER;MATERIALNUMBER;DESCRIPTION;SOQUANTITY;BILLINGQUANTITY;NETPRICE(BILLING);SOAMOUNT;BILLINGAMOUNT
            Dim splittedVal As String()
            splittedVal = val.Split(";")
            _sbMessage = New StringBuilder
            Try
                Dim sparePartPOStatusDetail As New SparePartPOStatusDetail
                Dim objSparePart As SparePartMaster = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(splittedVal(2).Trim)
                If Not IsNothing(objSparePart) AndAlso objSparePart.ID > 0 Then
                    sparePartPOStatusDetail.SparePartMaster = objSparePart
                    If IsNumeric(splittedVal(5).Trim()) Then
                        sparePartPOStatusDetail.BillingQuantity = CType(splittedVal(5).Trim(), Integer)
                    Else
                        _sbMessage.Append("Invalid Billing Quantity" & Chr(13) & Chr(10))
                    End If
                    If IsNumeric(splittedVal(4).Trim()) Then
                        sparePartPOStatusDetail.SOQuantity = CType(splittedVal(4).Trim(), Integer)
                    Else
                        _sbMessage.Append("Invalid SO Quantity" & Chr(13) & Chr(10))
                    End If
                    If IsNumeric(splittedVal(6).Trim()) Then
                        sparePartPOStatusDetail.NetPrice = CType(splittedVal(6).Trim(), Decimal)
                    Else
                        _sbMessage.Append("Invalid Net Price" & Chr(13) & Chr(10))
                    End If

                    If IsNumeric(splittedVal(7).Trim()) Then
                        sparePartPOStatusDetail.SOPrice = CType(splittedVal(7).Trim(), Decimal)
                    Else
                        _sbMessage.Append("Invalid SO Price" & Chr(13) & Chr(10))
                    End If
                    If IsNumeric(splittedVal(8).Trim()) Then
                        sparePartPOStatusDetail.BillingPrice = CType(splittedVal(8).Trim(), Decimal)
                    Else
                        _sbMessage.Append("Invalid Billing Price" & Chr(13) & Chr(10))
                    End If
                    sparePartPOStatusDetail.DONumber = splittedVal(9).Trim()
                    sparePartPOStatusDetail.RowStatus = 0
                    sparePartPOStatusDetail.CreatedBy = "WSM"
                Else
                    _sbMessage.Append("Invalid Sparepart PO Number:" & splittedVal(1).Trim() & Chr(13) & Chr(10))
                End If

                If _sbMessage.Length > 0 Then
                    Throw New Exception(_sbMessage.ToString)
                Else
                    _sparePartPOStatus.SparePartPOStatusDetails.Add(sparePartPOStatusDetail)
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub ParseSPPODetail(ByVal val As String)
            _sbMessage = New StringBuilder
            Try
                Dim sparePartPOStatusDetail As New sparePartPOStatusDetail
                Dim objSparePart As SparePartMaster = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(val.Substring(12, 20).Trim())
                If Not IsNothing(objSparePart) AndAlso objSparePart.ID > 0 Then
                    sparePartPOStatusDetail.SparePartMaster = objSparePart
                    Try
                        sparePartPOStatusDetail.BillingQuantity = CType(val.Substring(71, 6).Trim(), Integer)
                        sparePartPOStatusDetail.SOQuantity = CType(val.Substring(64, 6).Trim(), Integer)
                        sparePartPOStatusDetail.NetPrice = CType(val.Substring(78, 10).Trim(), Decimal)
                        sparePartPOStatusDetail.SOPrice = CType(val.Substring(89, 10).Trim(), Decimal)
                        sparePartPOStatusDetail.BillingPrice = CType(val.Substring(100, 10).Trim(), Decimal)
                    Catch ex As Exception
                        _sbMessage.Append("Invalid numeric format" & Chr(13) & Chr(10))
                    End Try
                    sparePartPOStatusDetail.RowStatus = 0
                    sparePartPOStatusDetail.CreatedBy = "WSM"
                Else
                    _sbMessage.Append("Invalid Sparepart PO Number:" & val.Substring(12, 20).Trim() & Chr(13) & Chr(10))
                End If

                If _sbMessage.Length > 0 Then
                    Throw New Exception(_sbMessage.ToString)
                Else
                    _sparePartPOStatus.SparePartPOStatusDetails.Add(sparePartPOStatusDetail)
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region "Public Properties"

        ReadOnly Property SparePartPurchaseOrder() As SparePartPOStatus
            Get
                Return _sparePartPOStatus
            End Get
        End Property

#End Region

    End Class

End Namespace