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
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class ContractParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private ContractHeaders As ArrayList
        Private ContractDetails As ArrayList
        Private _fileName As String
        Private _ContractHeader As ContractHeader 'Header
        Private _ContractDetail As ContractDetail
        Private errorMessage As StringBuilder
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
                Dim val As String
                ContractHeaders = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If val.Substring(0, 1).ToUpper.Equals("H") Then
                            If Not _ContractHeader Is Nothing Then
                                ContractHeaders.Add(_ContractHeader)  'customer header input text
                            End If
                            _ContractHeader = ParseContractHeader(val + delimited)
                        Else
                            If Not _ContractHeader Is Nothing Then
                                ParseContractDetail(val + Delimited, _ContractHeader)  'Order detail input
                            End If
                        End If
                      
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ContractParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ContractParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _ContractHeader = Nothing
                    End Try

                    val = MyBase.NextLine(stream)

                End While

                If Not _ContractHeader Is Nothing Then
                    ContractHeaders.Add(_ContractHeader)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return ContractHeaders

        End Function

        Private Sub UpdatePKResponseQty(ByVal item As ContractHeader)
            Dim objPkDetailFacade As New PKDetailFacade(System.Threading.Thread.CurrentPrincipal)
            For Each contractDetailItem As ContractDetail In item.ContractDetails
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.PKNumber", MatchType.Exact, item.PKNumber))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, item.ContractPeriodMonth))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, item.ContractPeriodYear))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.ID", MatchType.Exact, contractDetailItem.VechileColor.ID))
                Dim _pkDetailColl As ArrayList = objPkDetailFacade.Retrieve(criterias)
                If _pkDetailColl.Count > 0 Then
                    Dim _pkDetil As PKDetail = _pkDetailColl(0)
                    _pkDetil.ResponseQty = contractDetailItem.TargetQty
                    objPkDetailFacade.Update(_pkDetil)
                End If
            Next
        End Sub

        Protected Overrides Function DoTransaction() As Integer
            Dim _objContractHeader As ContractHeader

            For Each item As ContractHeader In ContractHeaders
                Try
                    _objContractHeader = IsInsert(item)
                    If _objContractHeader Is Nothing Then
                        Dim i As Int16 = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(item)
                        UpdatePKResponseQty(item)
                    Else
                        If _objContractHeader.RowStatus = DBRowStatus.Deleted Then
                            Dim i As Int16 = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Delete(_objContractHeader)
                            Dim x As Int16 = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Insert(item)
                            UpdatePKResponseQty(item)
                        Else
                            If _objContractHeader.Status.ToUpper = "LOCK" Then
                                For Each itemDetail As ContractDetail In _objContractHeader.ContractDetails
                                    Dim newQty As Integer = IsUpdateContractDetail(itemDetail, item)
                                    If newQty >= 0 Then
                                        itemDetail.TargetQty = newQty
                                    End If
                                    For Each oCDFromFile As ContractDetail In item.ContractDetails
                                        If oCDFromFile.LineItem = itemDetail.LineItem Then
                                            itemDetail.Amount = oCDFromFile.Amount
                                            itemDetail.PPh22 = oCDFromFile.PPh22
                                            itemDetail.Discount = oCDFromFile.Discount
                                        End If
                                    Next
                                Next
                                'TO DO LOCK harus di update by material number dan yang di update quantity
                                'Tapi cek dulu QuantityOld-SisaUnit >= new Quantity
                                Dim _contractFacade As ContractHeaderFacade = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                _objContractHeader.SPLNumber = item.SPLNumber
                                _objContractHeader.FreeIntIndicator = item.FreeIntIndicator
                                _objContractHeader.FreePPh22Indicator = item.FreePPh22Indicator
                                _objContractHeader.ProjectName = item.ProjectName
                                _objContractHeader.Purpose = item.Purpose
                                _contractFacade.UpdateTransaction(_objContractHeader)
                                UpdatePKResponseQty(item)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ContractParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ContractParser)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _objContractHeader.ContractNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function IsUpdateContractDetail(ByVal ContractItem As ContractDetail, ByVal objContractHeader As ContractHeader) As Integer
            Dim newQuantity As Integer = -1
            For Each item As ContractDetail In objContractHeader.ContractDetails
                If (IsNothing(item.ContractHeader)) Then
                    objContractHeader.MarkLoaded()
                    item.ContractHeader = objContractHeader
                End If
                If ContractItem.LineItem = item.LineItem Then
                    Dim i As Integer = item.TargetQty - (ContractItem.TargetQty - ContractItem.SisaUnit)
                    If i >= 0 Then
                        newQuantity = item.TargetQty
                        Return newQuantity
                    End If
                End If
            Next
            Return newQuantity
        End Function

        Private Function ParseContractHeader(ByVal ValParser As String) As ContractHeader
            _ContractHeader = New ContractHeader
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            Dim _dealer As New Dealer
            Dim _Category As New Category
            errorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 1
                        If sTemp.Trim.Length > 0 Then
                            _ContractHeader.ContractNumber = sTemp.Trim
                        Else
                            'Throw New Exception("Invalid Contract Number")
                            errorMessage.Append("Invalid Contract Number" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        _ContractHeader.PKNumber = sTemp.Trim
                        'Try
                        '    Dim pk As PKHeader = New PKHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                        '    _ContractHeader.PKHeader = pk
                        'Catch ex As Exception
                        '    'Throw New Exception("Pk Header Not Found")
                        'End Try
                    Case Is = 3
                        _ContractHeader.DealerPKNumber = sTemp.Trim
                    Case Is = 4
                        Try
                            _ContractHeader.ContractPeriodDay = sTemp.Trim.Substring(0, 2)
                            _ContractHeader.ContractPeriodMonth = sTemp.Trim.Substring(2, 2)
                            _ContractHeader.ContractPeriodYear = sTemp.Trim.Substring(4, 4)
                        Catch ex As Exception
                            _ContractHeader.ContractPeriodDay = 1
                            _ContractHeader.ContractPeriodMonth = 1
                            _ContractHeader.ContractPeriodYear = 1900
                        End Try
                    Case Is = 5
                        Try
                            Dim oDFac As New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            _dealer = oDFac.Retrieve(sTemp.Trim) ' New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If _dealer.ID > 0 Then
                                _ContractHeader.Dealer = _dealer
                                _ContractHeader.FreePPh22Indicator = _dealer.FreePPh22Indicator
                            Else
                                errorMessage.Append("Invalid Dealer" & Chr(13) & Chr(10))
                                'Throw New Exception("Invalid Dealer")
                            End If
                        Catch ex As Exception
                            errorMessage.Append("Invalid Dealer" & Chr(13) & Chr(10))
                            'Throw New Exception("Invalid Dealer")
                        End Try
                    Case Is = 6
                        'find id to db
                        Try
                            _Category = New CategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If _Category.ID > 0 Then
                                _ContractHeader.Category = _Category
                            Else
                                'Throw New Exception("Invalid category")
                                errorMessage.Append("Invalid Category" & Chr(13) & Chr(10))
                            End If
                        Catch ex As Exception
                            'Throw ex
                            errorMessage.Append(ex.Message.ToString & Chr(13) & Chr(10))
                        End Try
                    Case Is = 7
                        'bulanan =za21 tahunan za22
                        If sTemp.Trim.ToUpper = "ZA21" Then
                            _ContractHeader.ContractType = 0
                        Else
                            If sTemp.Trim.ToUpper = "ZA22" Then
                                _ContractHeader.ContractType = 1
                            Else
                                errorMessage.Append("Jenis Pesanan Invalid." & Chr(13) & Chr(10))
                                'Throw New Exception("Jenis Pesanan Invalid.")
                            End If
                        End If
                    Case Is = 8
                        If sTemp.Trim.ToUpper = "P" Then
                            'p project kalau blank non project
                            _ContractHeader.Purpose = 0
                        Else
                            _ContractHeader.Purpose = 1
                        End If
                    Case Is = 9
                        _ContractHeader.ProjectName = sTemp.Trim
                    Case Is = 10
                        Try
                            If sTemp.Trim.Length = 0 Then
                                _ContractHeader.ProductionYear = Now.Year
                            Else
                                _ContractHeader.ProductionYear = sTemp.Trim
                            End If
                        Catch ex As Exception
                            errorMessage.Append("Invalid Production Year" & Chr(13) & Chr(10))
                            'Throw New Exception("Invalid Production Year")
                        End Try
                    Case Is = 11
                        Dim str As String = sTemp.Trim
                        Dim _date As Date = New Date(str.Substring(4, 4), str.Substring(2, 2), str.Substring(0, 2))
                        Try
                            _ContractHeader.PricingPeriod = _date

                            Try
                                _ContractHeader.PricePeriodDay = _date.Day
                                _ContractHeader.PricePeriodMonth = _date.Month
                                _ContractHeader.PricePeriodYear = _date.Year
                            Catch ex As Exception
                                _ContractHeader.PricePeriodDay = 1
                                _ContractHeader.PricePeriodMonth = 1
                                _ContractHeader.PricePeriodYear = 1900
                            End Try
                        Catch ex As Exception
                            '  Throw New Exception("Invalid Pricing Date.")
                            errorMessage.Append("Invalid Pricing Date" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 12
                        Dim str As String = sTemp.Trim
                        Try
                            _ContractHeader.FreeIntIndicator = str
                        Catch ex As Exception
                            errorMessage.Append("Invalid FreeIntIndicator" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 13
                        'Start  :RemainModule-DailyPO:Remove FreePPh22Indicator
                        '    Dim str As String = sTemp.Trim
                        '    Try
                        '        _ContractHeader.FreePPh22Indicator = str
                        '    Catch ex As Exception
                        '        errorMessage.Append("Invalid FreePPh22Indicator" & Chr(13) & Chr(10))
                        '    End Try
                        'Case Is = 14
                        'End    :RemainModule-DailyPO:Remove FreePPh22Indicator
                        Dim str As String = sTemp.Trim
                        If str <> String.Empty Then
                            Dim objSPL As SPL = New SPLFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(str)
                            If objSPL Is Nothing Then
                                'errorMessage.Append("Invalid SPLNumber" & Chr(13) & Chr(10))
                                _ContractHeader.SPLNumber = ""
                            Else
                                _ContractHeader.SPLNumber = str
                            End If
                        End If
                    Case Is = 20
                        Dim refOCNumber As String = sTemp
                        If sTemp.Trim <> "" Then
                            Dim oCH As ContractHeader = New ContractHeaderFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(refOCNumber)
                            If IsNothing(oCH) OrElse oCH.ID < 1 Then
                                errorMessage.Append("Invalid Ref Contract Number : " & refOCNumber & Chr(13) & Chr(10))
                            Else
                                _ContractHeader.RefContractNumber = oCH.ContractNumber
                            End If
                        End If
                    Case Else
                        'do nothing
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If

            Return _ContractHeader
        End Function

        Private Sub ParseContractDetail(ByVal ValParser As String, ByVal _objContractHeader As ContractHeader)
            _ContractDetail = New ContractDetail
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            'Dim _pkDetail As New PKDetail
            errorMessage = New StringBuilder
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 15
                        Try
                            _ContractDetail.LineItem = sTemp.Trim
                        Catch ex As Exception
                            errorMessage.Append("Invalid Line Item" & Chr(13) & Chr(10))

                        End Try
                    Case Is = 17
                        Try
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "MaterialNumber", MatchType.Exact, sTemp.Trim))
                            Dim al As ArrayList = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If al.Count > 0 Then
                                Dim vc As VechileColor = al.Item(0)
                                If vc.ID > 0 Then
                                    _ContractDetail.VechileColor = vc
                                Else
                                    errorMessage.Append("Invalid Material Number" & Chr(13) & Chr(10))

                                End If
                            Else
                                errorMessage.Append("Invalid Material Number" & Chr(13) & Chr(10))
                                'Throw New Exception("Invalid Material Number")
                            End If
                        Catch ex As Exception
                            errorMessage.Append("Invalid Material Number" & Chr(13) & Chr(10))

                        End Try
                    Case Is = 18
                        Try
                            _ContractDetail.TargetQty = sTemp.Trim
                        Catch ex As Exception
                            errorMessage.Append("Invalid Target Quantity." & Chr(13) & Chr(10))

                        End Try
                    Case Is = 19
                        Try
                            _ContractDetail.Discount = Convert.ToInt64(sTemp.Trim)
                        Catch ex As Exception
                            _ContractDetail.Discount = 0
                        End Try
                    Case Is = 20
                        Try
                            _ContractDetail.SalesSurcharge = Convert.ToInt64(sTemp.Trim)
                        Catch ex As Exception
                            _ContractDetail.SalesSurcharge = 0
                        End Try
                    Case Is = 21
                        Try
                            _ContractDetail.GuaranteeAmount = CType(sTemp.Trim, Double)
                        Catch ex As Exception
                            _ContractDetail.GuaranteeAmount = 0
                        End Try
                    Case Else
                        'Do Nothing else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If
            Dim _Price As Price = RetrieveCurrentPrice(_ContractDetail.VechileColor.ID, _objContractHeader.PricingPeriod, _objContractHeader.Dealer.ID)
            If Not _Price Is Nothing Then
                _ContractDetail.Amount = Calculation.CountPKVehiclePrice(_ContractDetail.Discount, _Price.PPN_BM, _Price.PPN, _Price.BasePrice, _Price.OptionPrice, _ContractDetail.SalesSurcharge)
                _ContractDetail.PPh22 = Calculation.CountPKPPh22(_Price.BasePrice, _ContractDetail.Discount, _Price.PPN_BM, _Price.PPN, _Price.PPh22)
            Else
                _ContractDetail.Amount = 0
                _ContractDetail.PPh22 = 0
            End If
            _objContractHeader.ContractDetails.Add(_ContractDetail)
        End Sub

        Private Function RetrieveCurrentPrice(ByVal vehicleColorID As Integer, ByVal pricingPeriode As Date, ByVal DealerID As Integer) As Price
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, vehicleColorID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, DealerID))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.ASC))
            Dim objPriceArrayList As ArrayList = New PriceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias, sortColl)
            If (objPriceArrayList.Count <> 0) Then
                Dim objPrice As Price = CType(objPriceArrayList(0), Price)
                For Each item As Price In objPriceArrayList
                    If item.ValidFrom <= pricingPeriode Then
                        objPrice = item
                    End If
                Next
                Return objPrice
            End If
        End Function

        Private Function IsInsert(ByVal objContractHeader As ContractHeader) As ContractHeader
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractNumber", MatchType.Exact, objContractHeader.ContractNumber))
            Dim objContractHeaderList As ArrayList = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If objContractHeaderList.Count < 1 Then
                Return Nothing
            Else
                Return objContractHeaderList.Item(0)
            End If
        End Function



#End Region

    End Class

End Namespace