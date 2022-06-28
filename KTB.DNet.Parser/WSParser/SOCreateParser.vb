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
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Transfer

#End Region

Namespace KTB.DNet.Parser

    Public Class SOCreateParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aPOHs As ArrayList
        Private _oPOH As POHeader
        Private _oPOD As PODetail
        Private _SOList As ArrayList
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

                _aPOHs = New ArrayList()
                _oPOH = Nothing
                _SOList = New ArrayList()

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
                                    _oPOD.POHeader = _oPOH
                                    _oPOH.PODetails.Add(_oPOD)
                                    If Not IsNothing(_oPOD.ErrorMessage) AndAlso _oPOD.ErrorMessage.Trim <> String.Empty Then
                                        _oPOH.ErrorMessage = _oPOH.ErrorMessage & ";" & _oPOD.ErrorMessage
                                    End If
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SOCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
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
                _oPOH = CType(_aPOHs(i), POHeader)
                If Not IsNothing(_oPOH.ErrorMessage) AndAlso _oPOH.ErrorMessage.Trim() <> String.Empty Then
                    If nError = 0 Then
                        SysLogParameter.LogErrorToSyslog("Start : Log Error of SOCreateParser", "ws-worker", "SOCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    End If
                    sMsg = sMsg & _oPOH.ErrorMessage.Trim() & ";"
                    nError += 1
                    SysLogParameter.LogErrorToSyslog(_oPOH.ErrorMessage, "ws-worker", "SOCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Else
                    aDatas.Add(_oPOH)
                End If
            Next
            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _aPOHs.Count.ToString() & " Data", "ws-worker", "SOCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                SysLogParameter.LogErrorToSyslog("End : Log Error of SOCreateParser", "ws-worker", "SOCreateParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)

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

            Dim poFacade As POHeaderFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim objSalesOrder As SalesOrder = New SalesOrder
            For Each objPOHeader As POHeader In _aPOHs
                Try
                    For Each oSO As SalesOrder In _SOList
                        If objPOHeader.SONumber = oSO.SONumber Then
                            objSalesOrder = oSO
                        End If
                    Next
                    If Not IsNothing(objPOHeader.ErrorMessage) AndAlso objPOHeader.ErrorMessage <> "" Then
                        nError += 1
                        sMsg = sMsg & objPOHeader.ErrorMessage.ToString() & ";"
                    Else
                        poFacade = New POHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        poFacade.SynchronizeToSAP(objPOHeader, objSalesOrder, fromWS:=True)
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "SOCreateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objPOHeader.PONumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aPOHs.Count.ToString(), "ws-worker", "SOCreateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SOCreateParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
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

        Private Function ParseHeader(ByVal line As String) As POHeader
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim oPOH As New POHeader
            Dim oPOHFac As New POHeaderFacade(user)
            Dim oSO As New SalesOrder()

            errorMessage = New StringBuilder()
            If cols.Length <> 11 Then
                writeError("Invalid Header Format")
            Else
                'PO Number
                If cols(1).Trim() = String.Empty Then
                    writeError("PO Number can't be Empty")
                Else
                    oPOH = oPOHFac.Retrieve(cols(1))
                End If
                If IsNothing(oPOH) OrElse oPOH.ID < 1 Then
                    writeError("Invalid PO Number!")
                Else
                    'SO Number
                    If cols(2).Trim() = String.Empty Then
                        writeError("SO Number Can't be Empty")
                    Else
                        oPOH.SONumber = cols(2).Trim
                        oSO.SONumber = cols(2).Trim()
                    End If
                    'Document Type
                    oSO.SOType = cols(3).Trim()
                    'Document Date
                    oSO.SODate = MyBase.GetDateShort(cols(4))
                    If oSO.SODate.Year = 1900 Then
                        writeError("Invalid SO Date")
                    End If
                    'Create Date
                    'note use, for PO created in SAP
                    'Amount VH
                    Try
                        oSO.TotalVH = MyBase.GetCurrency(cols(6))
                    Catch ex As Exception
                        oSO.TotalVH = 0
                        writeError("Invalid Total VH Amount")
                    End Try

                    'Amount PP
                    Try
                        oSO.TotalPP = MyBase.GetCurrency(cols(7))
                    Catch ex As Exception
                        oSO.TotalPP = 0
                        writeError("Invalid Total PP Amount")
                    End Try

                    'Amount IT
                    Try
                        oSO.TotalIT = MyBase.GetCurrency(cols(8))
                    Catch ex As Exception
                        oSO.TotalIT = 0
                        writeError("Invalid Total IT Amount")
                    End Try

                    oSO.PaymentRef = cols(9)


                    '@FreePPh22Indicator dari sap 1 is Free ,0 Not Frree

                    Try
                        Dim FreePPh As Integer = CInt(cols(10))
                        oPOH.FreePPh22Indicator = IIf(FreePPh = 0 AndAlso oSO.TotalPP > 0, 1, 0)
                    Catch ex As Exception
                        oSO.TotalPP = 0
                        writeError("Invalid FreePPh22Indicator")
                    End Try




                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(oPOH) Then oPOH = New POHeader()
                    oPOH.ErrorMessage = errorMessage.ToString()
                Else
                    oSO.Amount = oSO.TotalVH + oSO.TotalPP + oSO.TotalIT
                    oPOH.CreatedBy = "SAP"
                    oSO.SONumber = oPOH.SONumber
                    oSO.POHeader = oPOH
                    oPOH.SalesOrders.Add(oSO)
                End If
            End If

            _SOList.Add(oSO)
            Return oPOH
        End Function

        Private Function ParseDetail(ByVal line As String) As PODetail
            Dim cols As String() = line.Split(MyBase.ColSeparator)

            _oPOD = New PODetail

            If (cols.Length <> 3) Then
                errorMessage.Append("Invalid Detail Format" & Chr(13) & Chr(10))
            Else
                'Material Number
                If cols(1).Trim = String.Empty Then
                    writeError("Material Number Can't Be Empty")
                Else
                    If Not IsNothing(_oPOH) AndAlso _oPOH.ID > 0 AndAlso Not IsNothing(_oPOH.ContractHeader) Then
                        Dim matNum As String = cols(1)
                        Dim objCD As ContractDetail = RetrieveContractDetail(_oPOH.ContractHeader.ContractNumber, matNum)
                        If Not objCD Is Nothing Then
                            _oPOD.LineItem = objCD.LineItem
                            _oPOD.Price = objCD.Amount
                            _oPOD.Discount = objCD.Discount
                            _oPOD.ContractDetail = objCD
                        Else
                            writeError("Invalid Material Number")
                        End If
                    End If
                End If
                'Qty
                Try
                    _oPOD.ReqQty = CType(MyBase.GetCurrency(cols(2)), Integer)
                    _oPOD.AllocQty = _oPOD.ReqQty
                Catch ex As Exception
                    writeError("Invalid Quantity")
                    _oPOD.ReqQty = 0
                    _oPOD.AllocQty = _oPOD.ReqQty
                End Try
                If Not IsNothing(errorMessage) Then
                    _oPOD.ErrorMessage = errorMessage.ToString()
                End If
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
