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
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports System.Threading
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Transfer

#End Region

Namespace KTB.DNet.Parser

    Public Class DCCreateParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aPOHs As ArrayList
        Private _oPOH As LogisticDCHeader
        Private _oPOD As LogisticDCDetail
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Threading.Thread.Sleep(5000) 'Delay 5 detik for WSM supaya SO turun duluan
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _aPOHs = New ArrayList()
                _oPOH = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oPOH) Then
                                _aPOHs.Add(_oPOH)
                                _oPOH = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oPOH = ParseHeader(line)
                        ElseIf ind = MyBase.IndicatorDetail Then
                            If IsNothing(_oPOH) OrElse Not IsNothing(_oPOH.ErrorMessage) Then
                            Else
                                _oPOD = ParseDetail(line)

                                If Not IsNothing(_oPOD) Then
                                    _oPOD.LogisticDCHeader = _oPOH
                                    _oPOH.LogisticDCDetails.Add(_oPOD)
                                    If Not IsNothing(_oPOD.ErrorMessage) AndAlso _oPOD.ErrorMessage.Trim <> String.Empty Then
                                        _oPOH.ErrorMessage = _oPOH.ErrorMessage & ";" & _oPOD.ErrorMessage
                                    End If
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "DCCreateParser.vb", "Parsing", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oPOH = Nothing
                    End Try
                Next

                If Not IsNothing(_oPOH) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oPOH.ErrorMessage = errorMessage.ToString()
                    _aPOHs.Add(_oPOH)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            'return only valid PO
            Dim aDatas As New ArrayList
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For i As Integer = 0 To _aPOHs.Count - 1
                _oPOH = CType(_aPOHs(i), LogisticDCHeader)
                If Not IsNothing(_oPOH.ErrorMessage) AndAlso _oPOH.ErrorMessage.Trim() <> String.Empty Then
                    If nError = 0 Then
                        SysLogParameter.LogErrorToSyslog("Start : Log Error of DCCreateParser", "ws-worker", "DCCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    End If
                    sMsg = sMsg & _oPOH.ErrorMessage.Trim() & ";"
                    nError += 1
                    SysLogParameter.LogErrorToSyslog(_oPOH.ErrorMessage, "ws-worker", "DCCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Else
                    aDatas.Add(_oPOH)
                End If
            Next
            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _aPOHs.Count.ToString() & " Data", "ws-worker", "DCCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                SysLogParameter.LogErrorToSyslog("End : Log Error of DCCreateParser", "ws-worker", "DCCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)

                Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _aPOHs.Count.ToString() & " Data. Message : " & sMsg)
                'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            _aPOHs = New ArrayList()
            _aPOHs = aDatas

            Return _aPOHs
        End Function


        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim poFacade As New LogisticDCHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing))
            Dim oLDCDFac As New LogisticDCDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing))
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim oLDCOld As LogisticDCHeader

            For Each oLDC As LogisticDCHeader In _aPOHs
                Try

                    If Not IsNothing(oLDC.ErrorMessage) AndAlso oLDC.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & oLDC.ErrorMessage.ToString() & ";"
                    Else

                        'TODO LOGIC
                        oLDCOld = poFacade.Retrieve(oLDC.DebitChargeNo)
                        If Not IsNothing(oLDCOld) AndAlso oLDCOld.ID > 0 Then 'update
                            oLDCOld.PODestination = oLDC.PODestination
                            oLDCOld.TotalLogisticCost = oLDC.TotalLogisticCost
                            oLDCOld.DCType = oLDC.DCType
                            oLDCOld.POBlockedStatus = oLDC.POBlockedStatus

                            Dim result As Integer = poFacade.Update(oLDCOld)

                            If result > 0 Then
                                'updating 
                                For Each oDDNew As LogisticDCDetail In oLDC.LogisticDCDetails
                                    oDDNew.LogisticDCHeader = oLDCOld
                                    Dim isExist As Boolean = False
                                    For Each oDDOld As LogisticDCDetail In oLDCOld.LogisticDCDetails
                                        If oDDOld.SAPModel = oDDNew.SAPModel Then
                                            isExist = True

                                            oDDOld.Quantity = oDDNew.Quantity
                                            oLDCDFac.Update(oDDOld)
                                            Exit For
                                        End If
                                    Next
                                    If isExist = False Then
                                        oLDCDFac.Insert(oDDNew)
                                    End If
                                Next
                                'delete or cuekin
                                For Each oDDOld As LogisticDCDetail In oLDCOld.LogisticDCDetails
                                    Dim isExist As Boolean = False
                                    For Each oDDNew As LogisticDCDetail In oLDC.LogisticDCDetails
                                        If oDDOld.SAPModel = oDDNew.SAPModel Then
                                            isExist = True
                                            Exit For
                                        End If
                                    Next
                                    If isExist = False Then
                                        oLDCDFac.Delete(oDDOld) 'TODO : confirm to BA
                                    End If
                                Next
                                oLDCOld.SalesOrder = oLDC.SalesOrder
                            End If

                        Else 'insert
                            oLDC.ID = poFacade.Insert(oLDC)
                            If oLDC.ID > 0 Then
                                oLDCOld = oLDC
                                For Each oLDCD As LogisticDCDetail In oLDC.LogisticDCDetails
                                    oLDCD.LogisticDCHeader = oLDC
                                    oLDCDFac.Insert(oLDCD)
                                Next

                            End If
                        End If


                        If oLDCOld.ID > 0 Then
                            'oDR = oDRFac.Retrieve(CType(_aDRs(Idx), SalesOrder).ID)
                            Dim oSO As SalesOrder
                            Dim oSOFac As New SalesOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing))

                            oSO = oSOFac.Retrieve(oLDCOld.SalesOrder.SONumber)

                            If Not IsNothing(oSO) Then

                                oSO.LogisticDCHeader = oLDCOld
                                oSOFac.Update(oSO)

                                If Not IsNothing(oLDCOld.POBlockedStatus) Then
                                    If oLDCOld.POBlockedStatus.Trim.ToUpper = "X" Then
                                        Dim oPOH As POHeader = oSO.POHeader
                                        Dim oPOHFac As New POHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing))

                                        oPOH.Status = CType(enumStatusPO.Status.DiBlok, Short).ToString 'refer to enum
                                        oPOHFac.Update(oPOH)

                                        Dim oPODFac As New PODetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing))
                                        For Each podetail As PODetail In oPOH.PODetails
                                            podetail.AllocQty = 0
                                            oPODFac.Update(podetail)
                                        Next

                                    End If
                                End If
                            End If

                        End If

                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "DCCreateParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & oLDC.DebitChargeNo & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aPOHs.Count.ToString(), "ws-worker", "DCCreateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "DCCreateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
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

        Private Function ParseHeader(ByVal line As String) As LogisticDCHeader
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oPOH As New LogisticDCHeader
            Dim oPOHFac As New LogisticDCHeaderFacade(user)
            Dim oSO As New SalesOrder
            Dim oSOFac As New SalesOrderFacade(user)
            Dim oDestFac As New PODestinationFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length < 8 Or cols.Length > 9 Then 'dengan blok = 9, tanpa blok = 8
                writeError("Invalid Header Format")
            Else
                '1 SO Number
                If cols(1).Trim() = String.Empty Then
                    writeError("SO Number can't be Empty")
                Else
                    oSO = oSOFac.Retrieve(cols(1).Trim)
                    If Not IsNothing(oSO) Then
                        oPOH.SalesOrder = oSO
                    Else
                        writeError("Sales Order not exist")
                    End If
                End If

                If Not (Not IsNothing(oSO) AndAlso oSO.ID > 0) Then
                    writeError("Invalid SO Number!")
                Else
                    '2 DC Number
                    If cols(2).Trim() = String.Empty Then
                        writeError("DC Number Can't be Empty")
                    Else
                        oPOH.DebitChargeNo = cols(2).Trim
                    End If
                    '3 PODestinaton
                    Dim oPOD As PODestination = oDestFac.Retrieve(cols(3).TrimStart("0"c))
                    If Not IsNothing(oPOD) AndAlso oPOD.ID > 0 Then
                        oPOH.PODestination = oPOD
                    Else
                        writeError("Invalid Destination Code")
                    End If

                    '4 Document Type
                    oPOH.DCType = cols(4).Trim()
                    '5 Document Date
                    '6 Create Date
                    'note use, for PO created in SAP Cols(5)
                    'Amount VH
                    Try
                        oPOH.TotalLogisticCost = MyBase.GetCurrency(cols(7))
                    Catch ex As Exception
                        oPOH.TotalLogisticCost = 0
                        writeError("Invalid Total Logistic Cost")
                    End Try
                    '8. PO Blocked
                    If cols.Length = 9 Then
                        oPOH.POBlockedStatus = cols(8)
                    End If

                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(oPOH) Then oPOH = New LogisticDCHeader()
                    oPOH.ErrorMessage = errorMessage.ToString()
                Else
                    oPOH.CreatedBy = "SAP"
                End If
            End If

            Return oPOH
        End Function

        Private Function ParseDetail(ByVal line As String) As LogisticDCDetail
            Dim cols As String() = line.Split(MyBase.ColSeparator)

            _oPOD = New LogisticDCDetail

            If (cols.Length > 4) Or (cols.Length < 3) Then '3 : tanpa block, 4 : dengan block
                errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
            Else
                'Material Number
                If cols(1).Trim <> String.Empty Then
                    _oPOD.SAPModel = cols(1).Trim()
                End If
                'Qty
                Try
                    _oPOD.Quantity = CType(cols(2), Integer)
                Catch ex As Exception
                    _oPOD.Quantity = 0
                    errorMessage.Append("Invalid Quantity" & Chr(13) & Chr(10))
                End Try
                If Not IsNothing(errorMessage) Then
                    _oPOD.ErrorMessage = errorMessage.ToString()
                End If
                '4. POBlockStatus
                'do nothing
            End If
            Return _oPOD
        End Function


        Private Function RetrieveContract(ByVal _ContractNumber As String) As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractNumber", MatchType.Exact, _ContractNumber))
            Dim objContractList As ArrayList = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            Return objContractList
        End Function


        Private Function RetrieveContractDetail(ByVal _ContractNumber As String, ByVal materialNumber As String) As ContractDetail
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "MaterialNumber", MatchType.Exact, materialNumber))
            Dim objVColorList As ArrayList = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            Dim objContractList As ArrayList = RetrieveContract(_ContractNumber)
            If objVColorList.Count > 0 And objContractList.Count > 0 Then
                For Each oCD As ContractDetail In CType(objContractList(0), ContractHeader).ContractDetails
                    If oCD.VechileColor.MaterialNumber = materialNumber Then
                        Return oCD
                    End If
                Next
                Return Nothing
                'Dim contractID As Integer = CType(objContractList.Item(0), ContractHeader).ID
                'Dim vecID As Integer = CType(objVColorList.Item(0), VechileColor).ID
                'Dim _contractDetail As ContractDetail = RetrieveContractDetail(CType(objContractList.Item(0), ContractHeader).ContractNumber, CType(objVColorList.Item(0), VechileColor).MaterialNumber)  'GetContractDetail(CType(objContractList.Item(0), ContractHeader), vecID) ' GetContractDetail(contractID, vecID)

                'If Not _contractDetail Is Nothing Then
                '    Return _contractDetail
                'Else
                '    Return Nothing
                'End If
            Else
                Return Nothing
            End If
        End Function


#End Region

    End Class
End Namespace
