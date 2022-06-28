#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.PO
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser
    Public Class LogisticDCParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _aDHs As ArrayList
        Private _aDDs As ArrayList
        Private _aDRs As ArrayList
        Private _fileName As String
        Private _oDH As LogisticDCHeader
        Private _oDR As SalesOrder
        Private _oDD As LogisticDCDetail
        Private ErrorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                _fileName = fileName
                stream = New StreamReader(fileName, True)
                _aDHs = New ArrayList
                _aDRs = New ArrayList
                Dim val As String = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If val.Substring(0, 1).ToUpper.Equals("H") Then
                            If Not _oDH Is Nothing Then

                                If Not IsNothing(_oDR) Then
                                    _aDRs.Add(_oDR)
                                    _oDH.SalesOrder = _oDR
                                End If
                                _aDHs.Add(_oDH)
                            End If
                            _oDH = ParserHeader(val + Delimited)
                        Else
                            If Not _oDH Is Nothing Then
                                ParseDetail(val + Delimited, _oDH)
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "LogisticDCParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.LogisticDCParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oDH = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While

                If Not _oDH Is Nothing Then
                    _oDH.SalesOrder = _oDR
                    _aDHs.Add(_oDH)
                    '_aDRs.Add(_oDR)
                End If
            Finally

                stream.Close()
                stream = Nothing
            End Try
            Return _aDHs

        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim user As New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oDHFac As New LogisticDCHeaderFacade(user)
            Dim oDDFac As New LogisticDCDetailFacade(user)
            Dim oDRFac As New SalesOrderFacade(user)
            Dim oDHOld As LogisticDCHeader
            Dim oDR As SalesOrder
            Dim Idx As Integer = 0

            For Each oDH As LogisticDCHeader In _aDHs  'For Idx = 0 To Idx <= _aDHs.Count - 1 
                'Dim oDH As LogisticDCHeader = _aDHs(Idx) 'baru
                oDR = oDH.SalesOrder
                Try
                    'insert/update LogisticDCHeader & LogisticDCDetail
                    oDHOld = oDHFac.Retrieve(oDH.DebitChargeNo) 'lama
                    If Not IsNothing(oDHOld) AndAlso oDHOld.ID > 0 Then 'updating
                        oDHOld.PODestination = oDH.PODestination
                        oDHOld.TotalLogisticCost = oDH.TotalLogisticCost
                        oDHOld.DCType = oDH.DCType

                        Dim result As Integer = oDHFac.Update(oDHOld)
                        If result > 0 Then
                            'updating 
                            For Each oDDNew As LogisticDCDetail In oDH.LogisticDCDetails
                                oDDNew.LogisticDCHeader = oDHOld
                                Dim isExist As Boolean = False
                                For Each oDDOld As LogisticDCDetail In oDHOld.LogisticDCDetails
                                    If oDDOld.SAPModel = oDDNew.SAPModel Then
                                        isExist = True

                                        oDDOld.Quantity = oDDNew.Quantity
                                        oDDFac.Update(oDDOld)
                                        'Remark biar update semua detail
                                        Exit For
                                    End If
                                Next
                                If isExist = False Then
                                    oDDFac.Insert(oDDNew)
                                End If
                            Next
                            'delete or cuekin
                            For Each oDDOld As LogisticDCDetail In oDHOld.LogisticDCDetails
                                Dim isExist As Boolean = False
                                For Each oDDNew As LogisticDCDetail In oDH.LogisticDCDetails
                                    If oDDOld.SAPModel = oDDNew.SAPModel Then
                                        isExist = True
                                        Exit For
                                    End If
                                Next
                                If isExist = False Then
                                    oDDFac.Delete(oDDOld) 'TODO : confirm to BA
                                End If
                            Next

                        End If
                    Else 'inserting
                        oDH.ID = oDHFac.Insert(oDH)
                        For Each oDCD As LogisticDCDetail In oDH.LogisticDCDetails
                            oDCD.LogisticDCHeader = oDH

                            oDDFac.Insert(oDCD)
                        Next
                        'detail belum ada
                        oDHOld = oDH
                    End If

                    'Update SO or SOes
                    'check salesorder, if different (by design, it won't)
                    'update to NULL for previous SO
                    'update to this Logistic ID for this SONumber

                    If oDHOld.ID > 0 Then
                        'oDR = oDRFac.Retrieve(CType(_aDRs(Idx), SalesOrder).ID)
                        oDR.LogisticDCHeader = oDHOld
                        oDRFac.Update(oDR)
                    End If


                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "LogisticDCParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.LogisticDCParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & oDH.DebitChargeNo & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try

            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            'Nothing TODO
        End Function

#End Region

#Region "Private Methods"

        Private Function ParserHeader(ByVal ValParser As String) As LogisticDCHeader
            _oDH = New LogisticDCHeader
            _oDR = New SalesOrder
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim user As New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oRDFac As New SalesOrderFacade(user)
            Dim oPODFac As New PODestinationFacade(user)

            sStart = 0
            nCount = 0
            ErrorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1 'DebitChargeNo
                        If sTemp <> "" Then
                            _oDH.DebitChargeNo = sTemp
                        Else
                            ErrorMessage.Append("Invalid Debit Charge Number" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2 'SONumber
                        Dim oDR As SalesOrder = oRDFac.Retrieve(sTemp)

                        If Not IsNothing(oDR) AndAlso oDR.ID > 0 Then
                            _oDR = oDR
                        Else
                            ErrorMessage.Append("Sales Order not exists")
                        End If
                    Case Is = 3 'DocType
                        _oDH.DCType = sTemp
                        'If _oDH.DCType.Trim.ToUpper() <> "Z090" Then  'TODO : Confirm to BA First
                        '    ErrorMessage.Append("Invalid Document Type" & Chr(13) & Chr(10))
                        'End If
                    Case Is = 4 'DestinationCode
                        Dim oPOD As PODestination = oPODFac.Retrieve(sTemp.TrimStart("0"c))
                        If Not IsNothing(oPOD) AndAlso oPOD.ID > 0 Then
                            _oDH.PODestination = oPOD
                        Else
                            ErrorMessage.Append("Invalid Destination Code" & Chr(13) & Chr(10))
                        End If
                    Case Is = 5 'Total AMount(+PPN)
                        Try
                            _oDH.TotalLogisticCost = Decimal.Parse(sTemp)
                        Catch ex As Exception
                            _oDH.TotalLogisticCost = 0
                            ErrorMessage.Append("Invalid Total Amount" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 6 'Created On
                        'Unused
                    Case Else
                        'do nothing
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If

            _oDH.CreatedBy = "SAP"
            Return _oDH
        End Function

        Private Sub ParseDetail(ByVal ValParser As String, ByVal oDH As LogisticDCHeader)
            _oDD = New LogisticDCDetail

            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0

            ErrorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1 'Debit Charge Number
                        If sTemp.Trim.ToUpper() <> _oDH.DebitChargeNo Then
                            ErrorMessage.Append("Invalid Debit Charge Number" & Chr(13) & Chr(10))
                        Else
                            _oDD.logisticDCHeader = _oDH
                        End If
                    Case Is = 2 'SAP Model
                        _oDD.SAPModel = sTemp
                    Case Is = 3 'Qty
                        Try
                            _oDD.Quantity = CType(sTemp, Integer)
                        Catch ex As Exception
                            _oDD.Quantity = 0
                            ErrorMessage.Append("Invalid Quantity" & Chr(13) & Chr(10))
                        End Try
                    Case Else
                        'Do Nothing else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If
            _oDD.CreatedBy = "SAP"
            _oDH.LogisticDCDetails.Add(_oDD)
        End Sub


#End Region

    End Class

End Namespace