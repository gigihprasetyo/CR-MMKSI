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
#End Region

Namespace KTB.DNet.Parser
    Public Class PriceParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Prices As ArrayList
        Private Grammar As Regex
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            Prices = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                'ParsePrice(val + ";")
                Try
                    ParsePrice(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "PriceParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.PriceParser, BlockName)
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return Prices
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If Prices.Count > 0 Then
                'Do Business - Like saveTo Database
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParsePrice(ByVal ValParser As String)
            Dim _Price As Price = New Price
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0

            For Each m As Match In Grammar.Matches(ValParser)

                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()

                Select Case (nCount)
                    Case Is = 0  '-- Material number
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
                                        _Price.VechileColor = vc
                                    End If
                                Catch ex As Exception
                                    _Price.ErrorMessage = _Price.ErrorMessage & "Material tidak terdaftar;"
                                End Try
                            End If
                        End If
                    Case Is = 1 '--dealercode
                        If sTemp.Length = 0 Then
                            '_Price.ErrorMessage = _Price.ErrorMessage & "Dealer Tidak Valid;"
                            Dim oDealer As New Dealer
                            oDealer.ID = -1
                            oDealer.DealerCode = "*"
                            oDealer.MarkLoaded()
                            _Price.Dealer = oDealer
                        Else
                            Dim oD As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp)
                            If IsNothing(oD) OrElse oD.ID < 1 Then
                                _Price.ErrorMessage = _Price.ErrorMessage & "Kode Dealer Tidak Valid;"
                            Else
                                _Price.Dealer = oD
                            End If
                        End If
                    Case Is = 2  '-- Valid from
                        If sTemp.Length = 0 Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "Tanggal tidak Valid;"
                        Else
                            Try
                                _Price.ValidFrom = New Date(sTemp.Substring(6, 4), sTemp.Substring(3, 2), sTemp.Substring(0, 2))
                            Catch ex As Exception
                                _Price.ErrorMessage = _Price.ErrorMessage & "Tanggal tidak Valid;"
                            End Try
                        End If
                    Case Is = 3  '-- Base price
                        If sTemp.Length = 0 Or Not isValidNumeric(sTemp) Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "Base price tidak Valid;"
                        Else
                            _Price.BasePrice = CDbl(sTemp)
                        End If
                    Case Is = 4  '-- Option price
                        If sTemp.Length = 0 Or Not isValidNumeric(sTemp) Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "Option price tidak Valid;"
                        Else
                            _Price.OptionPrice = CDbl(sTemp)
                        End If
                    Case Is = 5  '-- PPnBM
                        If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "PPN BM tidak Valid;"
                        Else
                            _Price.PPN_BM = CDbl(sTemp)
                        End If
                    Case Is = 6  '-- PPN
                        If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "PPN tidak Valid;"
                        Else
                            _Price.PPN = CDbl(sTemp)
                        End If
                    Case Is = 7  '-- PPh22
                        If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "PPh22 tidak Valid;"
                        Else
                            _Price.PPh22 = CDbl(sTemp)
                        End If
                    Case Is = 8 '-- Interest
                        If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "Interest tidak Valid;"
                        Else
                            _Price.Interest = CDbl(sTemp)
                        End If
                    Case Is = 9  '-- PPh23
                        If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "PPh23 tidak Valid;"
                        Else
                            _Price.PPh23 = CDbl(sTemp)
                        End If
                    Case Is = 10  '-- Status
                        If sTemp.Length = 1 Then
                            _Price.Status = sTemp
                        Else
                            _Price.ErrorMessage = _Price.ErrorMessage & "Status tidak Valid;"
                        End If
                    Case Is = 11  '-- DiscountReward
                        If sTemp.Length <> 0 AndAlso Not isValidPersen(sTemp) Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "Discount Reward tidak Valid;"
                        ElseIf sTemp.Length <> 0 AndAlso isValidPersen(sTemp) Then
                            _Price.DiscountReward = CDbl(sTemp)
                        Else
                            _Price.DiscountReward = 0

                        End If
                        '' END CR Sirkular Rewards
                    Case Is = 12  '-- Factoring Interest
                        If sTemp.Length = 0 Or Not isValidPersen(sTemp) Then
                            _Price.ErrorMessage = _Price.ErrorMessage & "Factoring Interest tidak Valid;"
                        Else
                            _Price.FactoringInt = CDbl(sTemp)
                        End If

                        '' CR Sirkular Rewards
                        '' by : ali Akbar
                        '' 2014-09-24
                    Case Else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            Prices.Add(_Price)

        End Sub

        Private Function IsVehicleColorExist(ByVal materialNumber As String) As Boolean
            '-- Check if material number exists in VechileColor table

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "MaterialNumber", MatchType.Exact, materialNumber))
            Dim _vc As ArrayList = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

            Return _vc.Count <> 0
        End Function

        Private Function GetVehicleColor(ByVal materialNumber As String) As VechileColor
            '-- Get vechileColor object of materialNumber

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "MaterialNumber", MatchType.Exact, materialNumber))
            Dim _vc As ArrayList = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

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

#End Region

#Region "Public Properties"

        ReadOnly Property PriceCollection() As ArrayList
            Get
                Return Prices
            End Get
        End Property

#End Region

    End Class

End Namespace
