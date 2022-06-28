
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

    Public Class SparePartDOParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aDOHs As ArrayList
        Private _oDOH As SparePartDO
        Private _oDOD As SparePartDODetail
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

                _aDOHs = New ArrayList()
                _oDOH = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oDOH) Then
                                _aDOHs.Add(_oDOH)
                                _oDOH = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oDOH = ParseHeader(line)
                        ElseIf ind = MyBase.IndicatorDetail Then
                            If Not (IsNothing(_oDOH) OrElse Not IsNothing(_oDOH.ErrorMessage)) Then
                                _oDOD = ParseDetail(line, _oDOH)
                                If Not IsNothing(_oDOD) Then
                                    _oDOD.SparePartDO = _oDOH
                                    _oDOH.SparePartDODetails.Add(_oDOD)
                                    If Not IsNothing(_oDOD.ErrorMessage) AndAlso _oDOD.ErrorMessage.Trim <> String.Empty Then
                                        _oDOH.ErrorMessage = _oDOH.ErrorMessage & ";" & _oDOD.ErrorMessage
                                    End If
                                Else
                                    ''remarks karena untuk so no picking memamng tidak ada di dnet
                                    'If Not IsNothing(_oDOD.ErrorMessage) Then
                                    '    _oDOH.ErrorMessage = _oDOH.ErrorMessage & ";" & "there are SO Number not found"
                                    'End If
                                    'Exit For ' gak ketemua so langsung keluar
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparePartDOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDOParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oDOH = Nothing
                    End Try
                Next

                If Not IsNothing(_oDOH) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oDOH.ErrorMessage = errorMessage.ToString()
                    _aDOHs.Add(_oDOH)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Dim aDatas As New ArrayList
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For i As Integer = 0 To _aDOHs.Count - 1
                _oDOH = CType(_aDOHs(i), SparePartDO)
                If Not IsNothing(_oDOH.ErrorMessage) AndAlso _oDOH.ErrorMessage.Trim() <> String.Empty Then
                    If nError = 0 Then
                        SysLogParameter.LogErrorToSyslog("Start : Log Error of SparePartDOParser", "ws-worker", "SparePartDOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDOParser, BlockName)
                    End If
                    sMsg = sMsg & _oDOH.ErrorMessage.Trim() & ";"
                    nError += 1
                    SysLogParameter.LogErrorToSyslog(_oDOH.ErrorMessage, "ws-worker", "SparePartDOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDOParser, BlockName)
                    'Else
                    '    aDatas.Add(_oDOH)
                End If
                aDatas.Add(_oDOH)
            Next
            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _aDOHs.Count.ToString() & " Data", "ws-worker", "SparePartDOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDOParser, BlockName)
                SysLogParameter.LogErrorToSyslog("End : Log Error of SparePartDOParser", "ws-worker", "SparePartDOParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartDOParser, BlockName)

                Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _aDOHs.Count.ToString() & " Data. Message : " & sMsg)
                'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                'Throw e
            End If
            _aDOHs = New ArrayList()
            _aDOHs = aDatas

            Return _aDOHs
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As SparePartDOFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For Each objSparePartDO As SparePartDO In _aDOHs
                Try

                    If Not IsNothing(objSparePartDO.ErrorMessage) AndAlso objSparePartDO.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objSparePartDO.ErrorMessage.ToString() & ";"
                    Else
                        doFacade = New SparePartDOFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        doFacade.InsertFromWebSevice(objSparePartDO)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "SparePartDOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objSparePartDO.DONumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aDOHs.Count.ToString(), "ws-worker", "SparePartDOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparePartDOParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As SparePartDO
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objSparePartDO As New SparePartDO
            Dim objSparePartDOFac As New SparePartDOFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length <> 8 Then
                writeError("Invalid Header Format")
            Else
                '0 DO Number
                If cols(1).Trim = String.Empty Then
                    writeError("DO Number can't be empty")
                Else
                    objSparePartDO.DONumber = cols(1).Trim
                End If

                '1 DealerCode
                If cols(2).Trim = String.Empty Then
                    writeError("Dealer code can't be empty")
                Else
                    Dim objDealerFacade As DealerFacade = New DealerFacade(user)
                    Dim objDealer As Dealer = objDealerFacade.Retrieve(cols(2).Trim())
                    If (Not IsNothing(objDealer) AndAlso objDealer.ID > 0) Then
                        objSparePartDO.Dealer = objDealer
                    Else
                        writeError("Invalid dealer code :" & cols(2).Trim())
                    End If
                End If

                Dim blankDate As DateTime = New DateTime(1900, 1, 1, 0, 0, 0)

                If cols(3).Trim <> String.Empty AndAlso Len(cols(3).Trim) = 14 Then
                    objSparePartDO.DoDate = MyBase.GetDateLong(cols(3))
                Else
                    objSparePartDO.DoDate = blankDate
                End If
                If cols(4).Trim <> String.Empty AndAlso Len(cols(4).Trim) = 14 Then
                    objSparePartDO.EstmationDeliveryDate = MyBase.GetDateLong(cols(4))
                Else
                    objSparePartDO.EstmationDeliveryDate = blankDate
                End If
                If cols(5).Trim <> String.Empty AndAlso Len(cols(5).Trim) = 14 Then
                    objSparePartDO.PickingDate = MyBase.GetDateLong(cols(5))
                Else
                    objSparePartDO.PickingDate = blankDate
                End If
                If cols(6).Trim <> String.Empty AndAlso Len(cols(6).Trim) = 14 Then
                    objSparePartDO.PackingDate = MyBase.GetDateLong(cols(6))
                Else
                    objSparePartDO.PackingDate = blankDate
                End If
                If cols(7).Trim <> String.Empty AndAlso Len(cols(7).Trim) = 14 Then
                    objSparePartDO.GoodIssueDate = MyBase.GetDateLong(cols(7))
                Else
                    objSparePartDO.GoodIssueDate = blankDate
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(objSparePartDO) Then objSparePartDO = New SparePartDO()
                    objSparePartDO.ErrorMessage = errorMessage.ToString()
                Else
                    objSparePartDO.CreatedBy = "SAP"
                End If
            End If

            Return objSparePartDO
        End Function

        Private Function ParseDetail(ByVal line As String, ByVal _sparePartDO As SparePartDO) As SparePartDODetail
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim cols As String() = line.Split(MyBase.ColSeparator)

            _oDOD = New SparePartDODetail

            If (cols.Length <> 7) Then
                errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
            Else
                'DO Item No
                Try
                    If cols(1).Trim <> String.Empty Then
                        _oDOD.ItemNoDO = MyBase.GetNumber(cols(1).Trim)
                    End If
                Catch ex As Exception
                    writeError("Invalid Quantity")
                End Try

                Try
                    'SO
                    If cols(2).Trim = String.Empty Then
                        writeError("Sales Order Can't Be Empty")
                    Else
                        Dim SONUmber As String = cols(2).Trim
                        Dim objEstFacade As SparePartPOEstimateFacade = New SparePartPOEstimateFacade(user)
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOEstimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOEstimate), "SONumber", MatchType.Exact, SONUmber))
                        Dim arlSO As ArrayList = objEstFacade.Retrieve(criterias)
                        If arlSO.Count > 0 Then
                            Dim objSO As SparePartPOEstimate = CType(arlSO(0), SparePartPOEstimate)
                            _oDOD.SparePartPOEstimate = objSO
                        Else
                            'writeError("Sales Order Number Not found")
                            Return Nothing
                        End If

                    End If

                Catch ex As Exception
                    writeError("Sales Order Error : " & ex.Message)
                End Try
                
                'SO Item No
                Try
                    If cols(3).Trim <> String.Empty Then
                        _oDOD.ItemNoSO = MyBase.GetNumber(cols(3).Trim)
                    End If
                Catch ex As Exception
                    writeError("Invalid Item SO :" & cols(3).Trim)
                End Try

                Try
                    'SparepartMaster
                    If cols(4).Trim = String.Empty Then
                        writeError("Spare part number Can't Be Empty")
                    Else
                        Dim SparePartNumber As String = cols(4).Trim
                        Dim objSparePartMasterFacade As SparePartMasterFacade = New SparePartMasterFacade(user)
                        Dim objSparePartMaster As SparePartMaster = objSparePartMasterFacade.Retrieve(SparePartNumber)
                        If Not IsNothing(objSparePartMaster) AndAlso objSparePartMaster.ID > 0 Then
                            _oDOD.SparePartMaster = objSparePartMaster
                        Else
                            writeError("Invalid Spare Part Number :" & SparePartNumber)
                        End If
                    End If
                Catch ex As Exception
                    writeError("Get Spare Part Master - Failed")
                End Try
                
                'cols(5) = Material Description

                'Quantity
                Try
                    If cols(6).Trim <> String.Empty Then
                        _oDOD.Qty = MyBase.GetNumber(cols(6).Trim)
                    End If
                Catch ex As Exception
                    writeError("Invalid Quantity")
                End Try

            End If

            If Not IsNothing(errorMessage) Then
                _oDOD.ErrorMessage = errorMessage.ToString()
            End If

            Return _oDOD
        End Function

#End Region

    End Class
End Namespace
