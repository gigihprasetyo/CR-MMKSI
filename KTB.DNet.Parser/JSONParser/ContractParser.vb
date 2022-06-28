#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports Newtonsoft.Json
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser

    Public Class ContractParserJson
        Inherits AbstractParser

#Region "Private Variables"
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
            Grammar = MyBase.GetGrammarParser()
        End Sub
#End Region

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim ContractH As KTB.DNet.Parser.Domain.ContractJson = JsonConvert.DeserializeObject(Of KTB.DNet.Parser.Domain.ContractJson)(Content)
                ContractHeaders = New ArrayList
                Try
                    _ContractHeader = ParseContractHeader(ContractH)

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "json-worker", "ContractParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ContractParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    _ContractHeader = Nothing
                End Try

                If Not _ContractHeader Is Nothing Then
                    ContractHeaders.Add(_ContractHeader)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ContractHeaders
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim _objContractHeader As ContractHeader

            For Each item As ContractHeader In ContractHeaders
                Try
                    _objContractHeader = IsInsert(item)
                    If _objContractHeader Is Nothing Then
                        Dim i As Int16 = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing)).Insert(item)
                        UpdatePKResponseQty(item)
                    Else
                        If _objContractHeader.RowStatus = DBRowStatus.Deleted Then
                            Dim i As Int16 = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing)).Delete(_objContractHeader)
                            Dim x As Int16 = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing)).Insert(item)
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
                                Dim _contractFacade As ContractHeaderFacade = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
                                _objContractHeader.SPLNumber = item.SPLNumber
                                _objContractHeader.FreeIntIndicator = item.FreeIntIndicator
                                _objContractHeader.FreePPh22Indicator = item.FreePPh22Indicator
                                _objContractHeader.ProjectName = item.ProjectName
                                _objContractHeader.Purpose = item.Purpose
                                _contractFacade.UpdateTransaction(_objContractHeader)
                                UpdatePKResponseQty(item)
                            Else
                                Throw New System.Exception("O/C status in D-NET is not locked. Update failed.")
                            End If
                        End If
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "WSJson-worker", "ContractParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ContractParser)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _objContractHeader.ContractNumber & Chr(13) & Chr(10) & ex.Message)
                    If ex.Message.Substring(0, 3) = "O/C" Then
                        Throw New System.Exception("O/C status in D-NET is not locked. Update failed.")
                    End If
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
        End Function

#Region "Private Methods"

        Private Function IsInsert(ByVal objContractHeader As ContractHeader) As ContractHeader
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractNumber", MatchType.Exact, objContractHeader.ContractNumber))
            criterias.opAnd(New Criteria(GetType(ContractHeader), "RowStatus", MatchType.Exact, 0))
            Dim objContractHeaderList As ArrayList = New ContractHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing)).Retrieve(criterias)
            If objContractHeaderList.Count < 1 Then
                Return Nothing
            Else
                Return objContractHeaderList.Item(0)
            End If
        End Function

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

        Private Function ParseContractHeader(ByVal contract As KTB.DNet.Parser.Domain.ContractJson) As ContractHeader
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(PKHeader), "PKNumber", MatchType.Exact, contract.PKNumber))
            Dim arrPKHeader As ArrayList = New PKHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing)).Retrieve(crit)

            If Not IsNothing(arrPKHeader) AndAlso arrPKHeader.Count > 0 Then
                Dim pKHeader As PKHeader = CType(arrPKHeader(0), PKHeader)
                Dim jsonContractPeriod As DateTime = New Date(contract.ContractPeriod.Substring(4, 4), contract.ContractPeriod.Substring(2, 2), contract.ContractPeriod.Substring(0, 2))
                Dim jsonPricingPeriod As DateTime = New Date(contract.ContractPricingPeriod.Substring(4, 4), contract.ContractPricingPeriod.Substring(2, 2), contract.ContractPricingPeriod.Substring(0, 2))
                Dim jsonPricingPeriodByPKHeader As DateTime = New Date(pKHeader.PricingPeriodeYear, pKHeader.PricingPeriodeMonth, pKHeader.PricingPeriodeDay)

                Dim contractHeader As ContractHeader = New ContractHeader()
                contractHeader.ContractNumber = contract.ContractNo
                contractHeader.PKHeader = pKHeader
                contractHeader.PKNumber = pKHeader.PKNumber
                contractHeader.DealerPKNumber = pKHeader.DealerPKNumber
                contractHeader.Dealer = pKHeader.Dealer
                contractHeader.Category = pKHeader.Category
                contractHeader.ContractType = pKHeader.OrderType
                contractHeader.Purpose = pKHeader.Purpose
                contractHeader.ProductionYear = pKHeader.ProductionYear
                contractHeader.FreePPh22Indicator = pKHeader.FreePPh22Indicator
                contractHeader.SPLNumber = pKHeader.SPLNumber
                contractHeader.FreeIntIndicator = pKHeader.FreeIntIndicator
                contractHeader.ContractPeriodDay = jsonContractPeriod.Day
                contractHeader.ContractPeriodMonth = jsonContractPeriod.Month
                contractHeader.ContractPeriodYear = jsonContractPeriod.Year
                contractHeader.PricePeriodDay = jsonPricingPeriod.Day
                contractHeader.PricePeriodMonth = jsonPricingPeriod.Month
                contractHeader.PricePeriodYear = jsonPricingPeriod.Year
                contractHeader.PricingPeriod = jsonPricingPeriod
                contractHeader.RefContractNumber = contract.RefContract
                contractHeader.ProjectName = contract.Description

                If Not IsNothing(contract.Detail) Then
                    For Each contractDet As KTB.DNet.Parser.Domain.ContractDetailJson In contract.Detail
                        Dim arrPKDetail As ArrayList = getPKDetails(contractDet.MaterialCode, pKHeader.PKNumber)
                        If pKHeader.PKDetails.Count > 0 And arrPKDetail.Count > 0 Then
                            Dim detail As PKDetail = CType(arrPKDetail(0), PKDetail)
                            Dim contractDetail As ContractDetail = New ContractDetail()
                            contractDetail.LineItem = contractDet.LineItem
                            contractDetail.PKDetail = detail
                            contractDetail.VechileColor = detail.VechileColor
                            contractDetail.TargetQty = contractDet.ContractQty
                            contractDetail.Amount = detail.ResponseAmount
                            contractDetail.PPh22 = detail.ResponsePPh22
                            Dim isInvalidDiscount = False
                            Dim temp As Decimal = 0
                            If contractDet.PriceDiff = "0" Or contractDet.PriceDiff = "" Then
                                contractDetail.Discount = detail.ResponseDiscount
                            Else
                                If validateDiscount(detail.ID) Then
                                    contractDetail.Discount = detail.ResponseDiscount
                                Else
                                    isInvalidDiscount = True
                                    validateNegativePrice(contractDet.PriceDiff, temp)
                                End If
                            End If
                            contractDetail.SalesSurcharge = detail.ResponseSalesSurcharge

                            Dim _Price As Price = RetrieveCurrentPrice(contractDetail.VechileColor.ID, contractHeader.PricingPeriod, contractHeader.Dealer.ID)
                            If Not _Price Is Nothing Then
                                If isInvalidDiscount And temp <> 0 Then
                                    Dim oldPrice As Price = RetrieveCurrentPrice(contractDetail.VechileColor.ID, jsonPricingPeriodByPKHeader, contractHeader.Dealer.ID)
                                    contractDetail.Discount = (detail.ResponseDiscount / (1 + ((oldPrice.PPN_BM + oldPrice.PPN) / 100)) + temp) * (1 + ((_Price.PPN_BM + _Price.PPN) / 100))
                                    'contractDetail.Discount = contractDetail.Discount * (1 + ((_Price.PPN_BM + _Price.PPN) / 100))
                                End If
                                contractDetail.Amount = Calculation.CountPKVehiclePrice(contractDetail.Discount, _Price.PPN_BM, _Price.PPN, _Price.BasePrice, _Price.OptionPrice, contractDetail.SalesSurcharge)
                                contractDetail.PPh22 = Calculation.CountPKPPh22(_Price.BasePrice, contractDetail.Discount, _Price.PPN_BM, _Price.PPN, _Price.PPh22)
                            Else
                                contractDetail.Amount = 0
                                contractDetail.PPh22 = 0
                            End If

                            contractHeader.ContractDetails.Add(contractDetail)
                        End If
                    Next
                End If

                Return contractHeader
            End If

            Return Nothing
        End Function

        Private Function RetrieveCurrentPrice(ByVal vehicleColorID As Integer, ByVal pricingPeriode As Date, ByVal DealerID As Integer) As Price
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, vehicleColorID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, DealerID))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.ASC))
            Dim objPriceArrayList As ArrayList = New PriceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing)).Retrieve(criterias, sortColl)
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

        Private Function getPKDetails(ByVal materialNo As String, ByVal pkNo As String) As ArrayList
            Dim arrPKDetail As ArrayList = New ArrayList()
            Dim rawData As DataSet = New PKHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing)).RetrieveSp("exec sp_PKHeader_getPKDetail @MaterialNo='" & materialNo & "',@PKNo='" & pkNo & "'")
            If rawData.Tables.Count > 0 Then
                If rawData.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In rawData.Tables(0).Rows
                        Dim id As Integer = CInt(row("ID"))
                        Dim pkDetail As PKDetail = New PKDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing)).Retrieve(id)
                        arrPKDetail.Add(pkDetail)
                    Next
                End If
            End If

            Return arrPKDetail
        End Function

        Private Function validateDiscount(ByVal pKDetailID As Integer) As Boolean
            Dim strSQL As String = "select a.ID " &
                                    "from PKdetail a " &
                                    "JOIN pkdetailtodiscount b on b.PKDetailID = a.ID " &
                                    "JOIN SPLDetailtoSPL c on c.ID = b.SPLDetailtoSPLID " &
                                    "JOIN DiscountMaster d on d.ID = c.DiscountMasterID " &
                                    "where a.ID =" & pKDetailID &
                                    "AND d.Category ='Price Difference'"

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(PKDetail), "ID", MatchType.InSet, "(" & strSQL & ")"))
            Dim arrPKDetail As ArrayList = New PKDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing)).Retrieve(crit)
            Dim result As Boolean = False

            If Not IsNothing(arrPKDetail) Then
                If arrPKDetail.Count = 0 Then
                    result = False
                Else
                    result = True
                End If
            Else
                result = False
            End If

            Return result
        End Function

        Private Function validateNegativePrice(ByVal priceStr As String, ByRef price As Decimal) As Boolean
            Dim ln As Integer = priceStr.Length
            If priceStr(ln - 1) = "-" Then
                price = Math.Abs(CDec(priceStr.Substring(0, ln - 1)))
                Return True
            Else
                price = CDec(priceStr)
                Return False
            End If
        End Function

#End Region

    End Class

End Namespace
