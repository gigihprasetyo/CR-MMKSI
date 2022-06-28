#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Text

#End Region

Namespace KTB.DNet.Parser
    Public Class VehiclePriceParser
        Inherits AbstractParser

#Region "Private Variables"
        Private priceList As ArrayList
        Private Grammar As Regex
        Private errorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Dim lines As String() = MyBase.GetLines(Content)
            Dim ind As String
            Dim line As String
            'Dim price As Price = Nothing
            Dim price As sp_Price = Nothing
            priceList = New ArrayList

            For i As Integer = 0 To lines.Length - 1
                Try
                    line = lines(i)

                    ind = line.Split(MyBase.ColSeparator)(0)
                    If ind = MyBase.IndicatorHeader Then
                        errorMessage = New StringBuilder()
                        ' create objek mspmaster
                        price = ParsePrice(line)
                        ' insert to array objek MSPMaster
                        If Not IsNothing(price) Then
                            If errorMessage.ToString() <> String.Empty Then
                                price.ErrorMessage = errorMessage.ToString()
                            End If

                            priceList.Add(price)
                        End If
                    End If

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "VehiclePriceParser.vb", "Parsing", KTB.DNet.Lib.DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPMasterParser, BlockName)
                    Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")

                End Try
            Next

            Return priceList
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim priceListUpdateOrInsert As New ArrayList
            If priceList.Count > 0 Then

                ' loop price array
                For Each price As sp_Price In priceList
                    Try
                        If price.ErrorMessage = String.Empty AndAlso IsNothing(price.ErrorMessage) Then
                            priceListUpdateOrInsert.Add(price)
                        End If
                    Catch ex As Exception
                        sMsg &= ex.Message.ToString() & ";"
                        nError += 1
                    End Try
                Next
                If nError = 0 Then
                    Dim facPrice As sp_PriceFacade = New sp_PriceFacade(user)
                    If facPrice.InsertOrUpdateWithTransactionManager(priceListUpdateOrInsert) < 0 Then
                        nError += 1
                    End If
                Else
                    Throw New Exception(sMsg)
                End If


            End If

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & priceList.Count.ToString(), "ws-worker", "VehiclePriceParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.VehiclePriceParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "VehiclePriceParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.VehiclePriceParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

#End Region

#Region "Private Methods"

        Private Function ParsePrice(ByVal ValParser As String) As sp_Price
            Dim _Price As sp_Price = New sp_Price
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim outputDec As Double
            Dim _strErrorMessageIn As String = String.Empty
            Dim _strErrorMessageOut As String = String.Empty

            sStart = 0
            nCount = 0
            Dim x As MatchCollection = Grammar.Matches(ValParser)
            For Each m As String In ValParser.Split(MyBase.ColSeparator)
                'For Each m As Match In Grammar.Matches(ValParser)

                _strErrorMessageIn = ""
                _strErrorMessageOut = ""
                sTemp = m
                'sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()

                outputDec = 0
                Select Case (nCount)
                    Case Is = 1  '-- Material number
                        If sTemp.Length = 0 Or sTemp.Length > 8 Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "Material tidak Valid;"
                        Else
                            If Not IsVehicleColorExist(sTemp) Then
                                _Price.ErrorMessage = _Price.ErrorMessage & "Material tidak terdaftar;"
                            Else
                                Try
                                    Dim vc As VechileColor = GetVehicleColor(sTemp)
                                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

                                    If vc.VechileType.ProductCategory.Code.Trim <> companyCode Then
                                        _Price.ErrorMessage = _Price.ErrorMessage & "Tipe tidak terdapat pada Kategori Produk " & companyCode & ";"
                                    Else
                                        _Price.VechileColorID = vc.ID
                                    End If
                                Catch ex As Exception
                                    _Price.ErrorMessage = _Price.ErrorMessage & "Material tidak terdaftar;"
                                End Try
                            End If
                        End If
                    Case Is = 2 '--dealercode
                        If sTemp.Length <> 0 Then
                            Dim dealerCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, sTemp))
                            Dim dealerList As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(dealerCriteria)
                            If dealerList.Count = 0 Then
                                _Price.ErrorMessage = _Price.ErrorMessage & "Kode Dealer Tidak Valid;"
                            Else
                                _Price.DealerCode = CType(dealerList(0), Dealer).DealerCode
                            End If
                        End If
                    Case Is = 3  '-- Valid from
                        If sTemp.Length = 0 Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "Tanggal tidak Valid;"
                        Else
                            'Try
                            '    _Price.ValidFrom = New Date(sTemp.Substring(6, 4), sTemp.Substring(3, 2), sTemp.Substring(0, 2))
                            'Catch ex As Exception
                            '    _Price.ErrorMessage = _Price.ErrorMessage & "Tanggal tidak Valid;"
                            'End Try
                            Dim theCultureInfo As IFormatProvider = New System.Globalization.CultureInfo("en-US", True)
                            Try
                                _Price.ValidFrom = DateTime.ParseExact(sTemp, "yyyyMMdd", theCultureInfo)
                            Catch
                                _Price.ErrorMessage = _Price.ErrorMessage & "Tanggal tidak Valid;"
                            End Try
                        End If

                    Case Is = 4  '-- Base price
                        If sTemp.Length = 0 Or Not isValidNumeric(sTemp) Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "Base price tidak Valid;"
                        Else
                            _Price.BasePrice = CDbl(sTemp)
                        End If
                    Case Is = 5  '-- Option price
                        If sTemp.Length = 0 Or Not isValidNumeric(sTemp) Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "Option price tidak Valid;"
                        Else
                            _Price.OptionPrice = CDbl(sTemp)
                        End If
                    Case Is = 6  '-- PPnBM
                        'If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                        '    _Price.ErrorMessage = _Price.ErrorMessage & "PPN BM tidak Valid;"
                        'Else
                        '    _Price.PPN_BM = CDbl(sTemp)
                        'End If
                        If sTemp.Length > 0 Then
                            _strErrorMessageIn = "PPN BM tidak Valid;"
                            _Price.PPN_BM = convertValidPersen(sTemp, _strErrorMessageIn, _strErrorMessageOut)
                            If _strErrorMessageOut.Trim <> "" Then
                                _Price.ErrorMessage = _Price.ErrorMessage & _strErrorMessageOut
                            End If
                        Else
                            _Price.PPN_BM = 0
                        End If

                    Case Is = 7  '-- PPN
                        'If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                        '    _Price.ErrorMessage = _Price.ErrorMessage & "PPN tidak Valid;"
                        'Else
                        '    _Price.PPN = CDbl(sTemp)
                        'End If
                        If sTemp.Length > 0 Then
                            _strErrorMessageIn = "PPN tidak Valid;"
                            _Price.PPN = convertValidPersen(sTemp, _strErrorMessageIn, _strErrorMessageOut)
                            If _strErrorMessageOut.Trim <> "" Then
                                _Price.ErrorMessage = _Price.ErrorMessage & _strErrorMessageOut
                            End If
                        Else
                            _Price.PPN = 0
                        End If

                    Case Is = 8  '-- PPh22
                        'If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                        '    _Price.ErrorMessage = _Price.ErrorMessage & "PPh22 tidak Valid;"
                        'Else
                        '    _Price.PPh22 = CDbl(sTemp)
                        'End If
                        If sTemp.Length > 0 Then
                            _strErrorMessageIn = "PPh22 tidak Valid;"
                            _Price.PPh22 = convertValidPersen(sTemp, _strErrorMessageIn, _strErrorMessageOut)
                            If _strErrorMessageOut.Trim <> "" Then
                                _Price.ErrorMessage = _Price.ErrorMessage & _strErrorMessageOut
                            End If
                        Else
                            _Price.PPh22 = 0
                        End If

                    Case Is = 9 '-- Interest
                        'If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                        '    _Price.ErrorMessage = _Price.ErrorMessage & "Interest tidak Valid;"
                        'Else
                        '    _Price.Interest = CDbl(sTemp)
                        'End If
                        If sTemp.Length > 0 Then
                            _strErrorMessageIn = "Interest tidak Valid;"
                            _Price.Interest = convertValidPersen(sTemp, _strErrorMessageIn, _strErrorMessageOut)
                            If _strErrorMessageOut.Trim <> "" Then
                                _Price.ErrorMessage = _Price.ErrorMessage & _strErrorMessageOut
                            End If
                        Else
                            _Price.Interest = 0
                        End If

                    Case Is = 10  '-- PPh23
                        'If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                        '    _Price.ErrorMessage = _Price.ErrorMessage & "PPh23 tidak Valid;"
                        'Else
                        '    _Price.PPh23 = CDbl(sTemp)
                        'End If
                        If sTemp.Length > 0 Then
                            _strErrorMessageIn = "PPh23 tidak Valid;"
                            _Price.PPh23 = convertValidPersen(sTemp, _strErrorMessageIn, _strErrorMessageOut)
                            If _strErrorMessageOut.Trim <> "" Then
                                _Price.ErrorMessage = _Price.ErrorMessage & _strErrorMessageOut
                            End If
                        Else
                            _Price.PPh23 = 0
                        End If

                    Case Is = 11  '-- Status
                        'If sTemp.Length = 1 Then
                        If sTemp.Length = 0 Then
                            _Price.Status = "1"
                        Else
                            _Price.Status = "0"
                            'Else
                            '    _Price.ErrorMessage = _Price.ErrorMessage & "Status tidak Valid;"
                        End If

                    Case Is = 13  '-- Factoring Interest
                        'If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                        '    _Price.ErrorMessage = _Price.ErrorMessage & "Factoring Interest tidak Valid;"
                        'Else
                        '    _Price.FactoringInt = CDbl(sTemp)
                        'End If
                        If sTemp.Length > 0 Then
                            _strErrorMessageIn = "Factoring Interest tidak Valid;"
                            _Price.FactoringInt = convertValidPersen(sTemp, _strErrorMessageIn, _strErrorMessageOut)
                            If _strErrorMessageOut.Trim <> "" Then
                                _Price.ErrorMessage = _Price.ErrorMessage & _strErrorMessageOut
                            End If
                        Else
                            _Price.FactoringInt = 0
                        End If

                        '' CR Sirkular Rewards
                        '' by : ali Akbar
                        '' 2014-09-24
                    Case Is = 12  '-- DiscountReward
                        'If sTemp.Length <> 0 AndAlso Not isValidPersen(sTemp) Then
                        '    _Price.ErrorMessage = _Price.ErrorMessage & "Discount Reward tidak Valid;"
                        'ElseIf sTemp.Length <> 0 AndAlso isValidPersen(sTemp) Then
                        '    _Price.DiscountReward = CDbl(sTemp)
                        'Else
                        '    _Price.DiscountReward = 0
                        'End If

                        If sTemp.Length > 0 Then
                            _strErrorMessageIn = "Discount Reward tidak Valid;"
                            _Price.DiscountReward = convertValidPersen(sTemp, _strErrorMessageIn, _strErrorMessageOut)
                            If _strErrorMessageOut.Trim <> "" Then
                                _Price.ErrorMessage = _Price.ErrorMessage & _strErrorMessageOut
                            End If
                        Else
                            _Price.DiscountReward = 0
                        End If
                        '' END CR Sirkular Rewards
                    Case Else
                End Select
                'sStart = m.Index + 1
                nCount += 1
            Next
            Return _Price

        End Function

        Private Function IsVehicleColorExist(ByVal materialNumber As String) As Boolean
            '-- Check if material number exists in VechileColor table

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "MaterialNumber", MatchType.Exact, materialNumber))
            criterias.opAnd(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, 0))
            Dim _vc As ArrayList = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

            Return _vc.Count <> 0
        End Function

        Private Function GetVehicleColor(ByVal materialNumber As String) As VechileColor
            '-- Get vechileColor object of materialNumber

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "MaterialNumber", MatchType.Exact, materialNumber))
            criterias.opAnd(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, 0))
            Dim _vc As ArrayList = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(criterias)

            Return _vc(0)
        End Function

        Private Function isValidNumeric(ByVal stemp As String) As Boolean
            '-- Validate numeric field.
            '-- If stemp is a numeric and its value >= 0 then return True else return False
            Try
                Dim x As Long = CLng(stemp)
                If x >= 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Private Function isValidPersen(ByVal stemp As String) As Boolean
            '-- Validate percentage field.
            '-- If stemp is a numeric and its value 0 <= x <= 100 then return True else return False
            Try
                Dim x As Long = CLng(stemp)
                If 0 <= x And x <= 100 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Private Function convertValidPersen(ByVal stemp As String, ByVal strErrorMessageIn As String, ByRef strErrorMessageOut As String) As Double
            '-- Validate percentage field.
            Dim outputDec As Double = 0
            strErrorMessageOut = String.Empty
            Try
                outputDec = stemp.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)
                Return outputDec
            Catch
                strErrorMessageOut = strErrorMessageIn
            End Try
        End Function

#End Region

#Region "Public Properties"

        ReadOnly Property PriceCollection() As ArrayList
            Get
                Return priceList
            End Get
        End Property

#End Region

    End Class

End Namespace
