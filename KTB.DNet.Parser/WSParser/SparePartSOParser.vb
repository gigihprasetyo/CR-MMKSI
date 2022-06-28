#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.Parser

    Public Class SparePartSOParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _estHeaders As ArrayList
        Private _oDOH As SparePartPOEstimate
        Private _estDetail As SparePartPOEstimateDetail
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _estHeaders = New ArrayList()
                _oDOH = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oDOH) Then
                                _estHeaders.Add(_oDOH)
                                _oDOH = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oDOH = ParseHeader(line)
                        ElseIf ind = MyBase.IndicatorDetail Then
                            If Not (IsNothing(_oDOH) OrElse Not IsNothing(_oDOH.ErrorMessage)) Then
                                _estDetail = ParseDetail(line, _oDOH)
                                If Not IsNothing(_estDetail) Then
                                    _estDetail.SparePartPOEStimate = _oDOH
                                    _oDOH.SparePartPOEStimateDetails.Add(_estDetail)
                                    If Not IsNothing(_estDetail.ErrorMessage) AndAlso _estDetail.ErrorMessage.Trim <> String.Empty Then
                                        _oDOH.ErrorMessage = _oDOH.ErrorMessage & ";" & _estDetail.ErrorMessage
                                    End If
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparePartPOEStimateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartSOParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oDOH = Nothing
                    End Try
                Next

                If Not IsNothing(_oDOH) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oDOH.ErrorMessage = errorMessage.ToString()
                    _estHeaders.Add(_oDOH)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Dim aDatas As New ArrayList
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For i As Integer = 0 To _estHeaders.Count - 1
                _oDOH = CType(_estHeaders(i), SparePartPOEStimate)
                If Not IsNothing(_oDOH.ErrorMessage) AndAlso _oDOH.ErrorMessage.Trim() <> String.Empty Then
                    If nError = 0 Then
                        SysLogParameter.LogErrorToSyslog("Start : Log Error of SparePartSOParser", "ws-worker", "SparePartSOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartSOParser, BlockName)
                    End If
                    sMsg = sMsg & _oDOH.ErrorMessage.Trim() & ";"
                    nError += 1
                    SysLogParameter.LogErrorToSyslog(_oDOH.ErrorMessage, "ws-worker", "SparePartSOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartSOParser, BlockName)
                    'Else
                    'aDatas.Add(_oDOH)
                End If
                aDatas.Add(_oDOH)
            Next
            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _estHeaders.Count.ToString() & " Data", "ws-worker", "SparePartSOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartSOParser, BlockName)
                SysLogParameter.LogErrorToSyslog("End : Log Error of SparePartSOParser", "ws-worker", "SparePartSOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartSOParser, BlockName)

                Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _estHeaders.Count.ToString() & " Data. Message : " & sMsg)
                'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                'Throw e
            End If
            _estHeaders = New ArrayList()
            _estHeaders = aDatas

            Return _estHeaders
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As SparePartPOEStimateFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each objSparePartPOEStimate As SparePartPOEStimate In _estHeaders
                Try

                    If Not IsNothing(objSparePartPOEStimate.ErrorMessage) AndAlso objSparePartPOEStimate.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objSparePartPOEStimate.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New SparePartPOEStimateFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.InsertFromWebSevice(objSparePartPOEStimate)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "SparePartSOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objSparePartPOEStimate.SONumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _estHeaders.Count.ToString(), "ws-worker", "SparePartSOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartSOParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparePartSOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartSOParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As SparePartPOEstimate
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objSparePartPOEStimate As New SparePartPOEStimate
            Dim objSparePartPOEStimateFac As New SparePartPOEStimateFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length <> 6 Then
                writeError("Invalid Header Format")
            Else
                '1 PO Number
                If cols(1).Trim = String.Empty Then
                    writeError("DO Number can't be empty")
                Else
                    Try
                        Dim _poNumber As String = cols(1).Trim
                        Dim objSparepartPO As SparePartPO = New SparePartPOFacade(user).Retrieve(_poNumber)
                        If Not IsNothing(objSparepartPO) Then
                            objSparePartPOEStimate.SparePartPO = objSparepartPO
                        Else
                            writeError("Purchase Order not found")
                        End If

                    Catch ex As Exception
                        writeError("Purchase Order not found")
                    End Try

                End If

                '2 SONumber
                If cols(2).Trim = String.Empty Then
                    writeError("SO Number can't be empty")
                Else
                    objSparePartPOEStimate.SONumber = cols(2).Trim
                End If

                Dim blankDate As DateTime = New DateTime(1900, 1, 1, 0, 0, 0)
                'SODate
                If cols(3).Trim <> String.Empty AndAlso Len(cols(3).Trim) = 14 Then
                    objSparePartPOEStimate.SODate = MyBase.GetDateLong(cols(3))
                Else
                    objSparePartPOEStimate.SODate = blankDate
                End If
                'DeliveryDate
                If cols(4).Trim <> String.Empty AndAlso Len(cols(4).Trim) = 14 Then
                    objSparePartPOEStimate.DeliveryDate = MyBase.GetDateLong(cols(4))
                Else
                    objSparePartPOEStimate.DeliveryDate = blankDate
                End If
                'DocumentType
                If cols(5).Trim = String.Empty Then
                    'writeError("Document type can't be empty")
                Else
                    objSparePartPOEStimate.DocumentType = cols(5).Trim
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(objSparePartPOEStimate) Then objSparePartPOEStimate = New SparePartPOEstimate()
                    objSparePartPOEStimate.ErrorMessage = errorMessage.ToString()
                Else
                    objSparePartPOEStimate.CreatedBy = "SAP"
                End If
            End If

            Return objSparePartPOEStimate
        End Function

        Private Function ParseDetail(ByVal line As String, ByVal _SparePartPOEStimate As SparePartPOEstimate) As SparePartPOEstimateDetail
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim cols As String() = line.Split(MyBase.ColSeparator)

            _estDetail = New SparePartPOEStimateDetail

            If (cols.Length <> 11) Then
                errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
            Else
                'SONUmber
                Try
                    If cols(1).Trim <> String.Empty Then

                    End If
                Catch ex As Exception
                End Try

                Try
                    'SparepartMaster
                    If cols(2).Trim = String.Empty Then
                        writeError("Spare part number Can't Be Empty")
                    Else
                        Dim SparePartNumber As String = cols(2).Trim
                        _estDetail.PartNumber = SparePartNumber
                    End If
                Catch ex As Exception
                    'writeError("Get Spare Part Master - Failed")
                End Try

                'SO Item No
                Try
                    If cols(3).Trim <> String.Empty Then
                        _estDetail.PartName = cols(3).Trim
                    End If
                Catch ex As Exception
                    'writeError("Empty Part Name")
                End Try

                Try
                    'Order Qty
                    If cols(4).Trim <> String.Empty Then
                        _estDetail.OrderQty = MyBase.GetNumber(cols(4).Trim)
                    Else
                        _estDetail.OrderQty = 0
                    End If
                Catch ex As Exception
                    'writeError("Order qty empty")
                End Try

                Try
                    'Alloc Qty
                    If cols(5).Trim <> String.Empty Then
                        _estDetail.AllocQty = MyBase.GetNumber(cols(5).Trim)
                    Else
                        _estDetail.AllocQty = 0
                    End If
                Catch ex As Exception
                    'writeError("Alloc qty empty")
                End Try

                'Allocation Quantity
                Try
                    If cols(6).Trim <> String.Empty Then
                        _estDetail.AllocationQty = MyBase.GetNumber(cols(6).Trim)
                    Else
                        _estDetail.AllocationQty = 0
                    End If
                Catch ex As Exception
                    'writeError("Allocation Quantity Failed")
                End Try
                'Open Qty
                Try
                    If cols(7).Trim <> String.Empty Then
                        _estDetail.OpenQty = MyBase.GetNumber(cols(7).Trim)
                    Else
                        _estDetail.OpenQty = 0
                    End If
                Catch ex As Exception
                    'writeError("Open Quantity Failed")
                End Try

                'Retail Price
                Try
                    If cols(8).Trim <> String.Empty Then
                        _estDetail.RetailPrice = MyBase.GetCurrency(cols(8).Trim)
                    Else
                        _estDetail.RetailPrice = 0
                    End If
                Catch ex As Exception
                    'writeError("Open Quantity Failed")
                End Try
                'Alternate Part Number
                Try
                    If cols(9).Trim <> String.Empty Then
                        Dim SparePartNumber As String = cols(9).Trim
                        _estDetail.AltPartNumber = cols(9).Trim
                    End If
                Catch ex As Exception
                    'writeError("Get Spare Part Master - Failed")
                End Try
                'Discount
                Try
                    If cols(10).Trim <> String.Empty Then
                        _estDetail.Discount = MyBase.GetNumber(cols(10).Trim)
                    End If
                Catch ex As Exception
                    'writeError("Get Spare Part Master - Failed")
                End Try
            End If

            If Not IsNothing(errorMessage) Then
                _estDetail.ErrorMessage = errorMessage.ToString()
            End If

            Return _estDetail
        End Function

#End Region

    End Class
End Namespace
