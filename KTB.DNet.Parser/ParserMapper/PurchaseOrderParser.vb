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
    Public Class PurchaseOrderParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private PurchaseOrderHeaders As ArrayList
        Private PurchaseOrderDetails As ArrayList
        Private SalesOrders As ArrayList
        Private _fileName As String
        Private _PurchaseOrderHeader As POHeader 'Header
        Private _SalesOrder As SalesOrder
        Private _PurchaseOrderDetail As PODetail
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
                PurchaseOrderHeaders = New ArrayList
                Dim val As String = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If val.Substring(0, 1).ToUpper.Equals("H") Then
                            If Not _PurchaseOrderHeader Is Nothing Then
                                PurchaseOrderHeaders.Add(_PurchaseOrderHeader)  'customer header input text
                            End If
                            _PurchaseOrderHeader = ParsePurchaseOrderHeader(val + delimited)

                        Else
                            If Not _PurchaseOrderHeader Is Nothing Then
                                ParsePurchaseOrderDetail(val + delimited, _PurchaseOrderHeader) 'Order detail input
                            End If
                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PurchaseOrderParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PurchaseOrderParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _PurchaseOrderHeader = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While

                If Not _PurchaseOrderHeader Is Nothing Then
                    PurchaseOrderHeaders.Add(_PurchaseOrderHeader)
                End If
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return PurchaseOrderHeaders

        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim poFacade As POHeaderFacade
            For Each objPOHeader As POHeader In PurchaseOrderHeaders
                Try
                    poFacade = New POHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    poFacade.SynchronizeToSAP(objPOHeader)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PurchaseOrderParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objPOHeader.PONumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            'Nothing TODO
        End Function

#End Region

#Region "Private Methods"

        Private Function ParsePurchaseOrderHeader(ByVal ValParser As String) As POHeader
            _PurchaseOrderHeader = New POHeader           
            _SalesOrder = New SalesOrder           
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
                    Case Is = 1
                        If sTemp <> "" Then
                            _PurchaseOrderHeader.SONumber = sTemp
                        Else
                            ErrorMessage.Append("Invalid SO Number" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        Dim ContractList As ArrayList = RetrieveContract(sTemp)
                        If ContractList.Count > 0 Then
                            _PurchaseOrderHeader.ContractHeader = CType(ContractList.Item(0), ContractHeader)
                        End If
                    Case Is = 3
                        _PurchaseOrderHeader.DealerPONumber = sTemp
                    Case Is = 4
                        If sTemp <> "" Then
                            Try
                                Dim str As String = sTemp
                                Dim _date As Date = New Date(str.Substring(4, 4), str.Substring(2, 2), str.Substring(0, 2))
                                _PurchaseOrderHeader.CreatedTime = _date
                                '_PurchaseOrderHeader.ReqAllocationDate = _date.Day
                                '_PurchaseOrderHeader.ReqAllocationYear = _date.Year
                                '_PurchaseOrderHeader.ReqAllocationMonth = _date.Month
                                '_PurchaseOrderHeader.ReqAllocationDateTime = _date
                                _PurchaseOrderHeader.ReleaseDate = _date.Day
                                _PurchaseOrderHeader.ReleaseYear = _date.Year
                                _PurchaseOrderHeader.ReleaseMonth = _date.Month
                            Catch ex As Exception
                                ErrorMessage.Append("Invalid Date" & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case Is = 5
                        Dim _top As TermOfPayment = RetrieveTermOfPayment(sTemp)
                        If Not _top Is Nothing Then
                            If _top.ID > 0 Then
                                _PurchaseOrderHeader.TermOfPayment = _top
                            Else
                                ErrorMessage.Append("Invalid Term Of Payment" & Chr(13) & Chr(10))
                            End If
                        End If
                    Case Is = 6
                        _PurchaseOrderHeader.PONumber = sTemp
                        Dim objPOHeader As POHeader = New POHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                        If Not objPOHeader Is Nothing Then
                            _PurchaseOrderHeader.ReleaseDate = objPOHeader.ReleaseDate
                            _PurchaseOrderHeader.ReleaseYear = objPOHeader.ReleaseYear
                            _PurchaseOrderHeader.ReleaseMonth = objPOHeader.ReleaseMonth
                        End If
                    Case Is = 7
                        If sTemp <> "" Then
                            _PurchaseOrderHeader.IsFactoring = 0
                            If sTemp.ToUpper = "ZA51" Then
                                _PurchaseOrderHeader.POType = LookUp.EnumJenisOrder.Harian
                                _SalesOrder.SOType = sTemp.Trim.ToUpper
                            Else
                                If sTemp.ToUpper = "ZA52" Then
                                    _PurchaseOrderHeader.POType = LookUp.EnumJenisOrder.Tambahan
                                    _SalesOrder.SOType = sTemp.Trim.ToUpper
                                Else
                                    'Start  :Factoring;by:dna;on:20101104
                                    'Start  :Ori
                                    '_SalesOrder.SOType = sTemp.Trim.ToUpper
                                    ''ErrorMessage.Append("Invalid POType" & Chr(13) & Chr(10))
                                    'End    :Ori
                                    _SalesOrder.SOType = sTemp.Trim.ToUpper
                                    _PurchaseOrderHeader.POType = LookUp.EnumJenisOrder.Harian
                                    If IsFactoringBasedOnSOType(sTemp) Then
                                        _PurchaseOrderHeader.IsFactoring = 1
                                    End If
                                    'End    :Factoring;by:dna;on:20101104
                                End If
                            End If
                        End If
                    Case Is = 8
                        If sTemp <> "" Then
                            Dim reqDate As DateTime = New Date(sTemp.Substring(4, 2), sTemp.Substring(2, 2), sTemp.Substring(0, 2), 0, 0, 0)
                            Try
                                reqDate = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2), 0, 0, 0)
                                _PurchaseOrderHeader.ReqAllocationDate = reqDate.Day
                                _PurchaseOrderHeader.ReqAllocationYear = reqDate.Year
                                _PurchaseOrderHeader.ReqAllocationMonth = reqDate.Month
                                _PurchaseOrderHeader.ReqAllocationDateTime = reqDate
                            Catch ex As Exception
                                ErrorMessage.Append("Invalid Req Dev Date" & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case Is = 9
                        If sTemp <> "" Then
                            _SalesOrder.Amount = CDec(sTemp.Trim)
                        End If
                    Case Is = 10
                        If sTemp <> "" Then
                            Dim soDate As DateTime = New Date(sTemp.Substring(4, 2), sTemp.Substring(2, 2), sTemp.Substring(0, 2), 0, 0, 0)
                            Try
                                soDate = New Date(sTemp.Substring(4, 4), sTemp.Substring(2, 2), sTemp.Substring(0, 2), 0, 0, 0)
                                _SalesOrder.SODate = soDate
                            Catch ex As Exception
                                ErrorMessage.Append("Invalid SO Date" & Chr(13) & Chr(10))
                            End Try
                        End If
                    Case Is = 11
                        If sTemp <> "" Then
                            _SalesOrder.PaymentRef = sTemp.Trim
                        End If
                    Case Is = 12
                        If sTemp.ToUpper = "X" Then
                            _PurchaseOrderHeader.FreePPh22Indicator = 0
                        Else
                            _PurchaseOrderHeader.FreePPh22Indicator = 1
                        End If
                    Case Is = 13
                        Try
                            _SalesOrder.CashPayment = CType(sTemp.Trim, Decimal)
                        Catch ex As Exception
                            _SalesOrder.CashPayment = 0
                        End Try
                    Case Else
                        'do nothing
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If _SalesOrder.SOType <> "ZA5F" And _SalesOrder.SOType <> "ZA5W" Then
                If _PurchaseOrderHeader.ContractHeader Is Nothing Then
                    ErrorMessage.Append("Invalid Contract Number" & Chr(13) & Chr(10))
                End If
                If _PurchaseOrderHeader.ReleaseDate = 0 Then
                    ErrorMessage.Append("Invalid Date" & Chr(13) & Chr(10))
                End If
            End If

            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If

            _PurchaseOrderHeader.CreatedBy = "SAP"
            _SalesOrder.SONumber = _PurchaseOrderHeader.SONumber
            _SalesOrder.POHeader = _PurchaseOrderHeader
            _PurchaseOrderHeader.SalesOrders.Add(_SalesOrder)
            Return _PurchaseOrderHeader
        End Function

        Private Sub ParsePurchaseOrderDetail(ByVal ValParser As String, ByVal _objPurchaseOrderHeader As POHeader)
            _PurchaseOrderDetail = New PODetail
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim materialNumber As String
            Dim ContractNumber As String
            ErrorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        ContractNumber = sTemp
                    Case Is = 8
                        If sTemp <> "" Then
                            If Not _objPurchaseOrderHeader.ContractHeader Is Nothing Then
                                materialNumber = sTemp
                                Dim objCD As ContractDetail = RetrieveContractDetail(_objPurchaseOrderHeader.ContractHeader.ContractNumber, materialNumber)
                                If Not objCD Is Nothing Then
                                    _PurchaseOrderDetail.LineItem = objCD.LineItem
                                    _PurchaseOrderDetail.Price = objCD.Amount
                                    _PurchaseOrderDetail.Discount = objCD.Discount
                                    _PurchaseOrderDetail.ContractDetail = objCD
                                Else
                                    ErrorMessage.Append("Invalid Material Number" & Chr(13) & Chr(10))
                                End If
                            End If                          
                        Else
                            ErrorMessage.Append("Invalid Material Number" & Chr(13) & Chr(10))
                        End If
                    Case Is = 9
                        Try
                            _PurchaseOrderDetail.ReqQty = sTemp
                            _PurchaseOrderDetail.AllocQty = sTemp
                        Catch ex As Exception
                            ErrorMessage.Append("Invaid Quantity " & Chr(13) & Chr(10))
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
            'Dim _contractDetail As ContractDetail = RetrieveContractDetail(_PurchaseOrderHeader.ContractHeader.ContractNumber, materialNumber)
            'If Not _contractDetail Is Nothing Then
            '    _PurchaseOrderDetail.ContractDetail = _contractDetail
            'End If
            _PurchaseOrderDetail.CreatedBy = "SAP"
            _objPurchaseOrderHeader.PODetails.Add(_PurchaseOrderDetail)
        End Sub

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

        Private Function GetContractDetail(ByVal contractID As Integer, ByVal colorID As Integer) As ContractDetail
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.ID", MatchType.Exact, contractID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "VechileColor.ID", MatchType.Exact, colorID))
            Dim objList As ArrayList = New ContractDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If objList.Count > 0 Then
                Return CType(objList.Item(0), ContractDetail)
            Else
                Return Nothing
            End If

        End Function

        Private Function GetContractDetail(ByRef oCH As ContractHeader, ByVal ColorID As Integer) As ContractDetail
            For Each oCD As ContractDetail In oCH.ContractDetails
                If oCD.VechileColor.ID = ColorID Then
                    Return oCD
                End If
            Next
            Return Nothing
        End Function

        Private Function RetrieveDealer(ByVal code As String) As Dealer
            Return New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(code)
        End Function

        Private Function RetrieveTermOfPayment(ByVal code As String) As TermOfPayment
            Return New TermOfPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(code)
        End Function

        Private Function IsFactoringBasedOnSOType(ByVal sSOType As String) As Boolean
            Dim sList() As String = {"ZF51", "ZF52", "ZF5F", "ZF5W"}
            'SO     :"ZF51","ZF52","ZF5F","ZF5W"
            'Billing:"ZF71","ZF72","ZF7G","ZF7Z"
            'DO     :"ZF61","ZF6F","ZF6I"
            Dim i As Integer = 0

            For i = 0 To sList.Length - 1
                If sSOType.Trim.ToUpper = sList(i).Trim.ToUpper Then
                    Return True
                End If
            Next
            Return False
        End Function
#End Region

    End Class

End Namespace