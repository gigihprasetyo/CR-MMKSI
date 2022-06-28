#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK

#End Region

Namespace KTB.DNet.Parser

    Public Class ProductionPlanParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private ProductionPlans As ArrayList
        Private Grammar As Regex
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoTransaction() As Integer
            If ProductionPlans.Count > 0 Then
                'Do Business - Like saveTo Database
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                _Stream = New StreamReader(fileName, True)
                ProductionPlans = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                While (Not val = "")
                    'ParseProductionPlan(val + delimited)
                    Try
                        ParseProductionPlan(val + delimited)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ProductionPlanParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ProductionPlanParser, BlockName)
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While
            Finally
                _Stream.Close()
                _Stream = Nothing
            End Try
            Return ProductionPlans
        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseProductionPlan(ByVal ValParser As String)
            Dim _ProductionPlan As PKProductionPlan = New PKProductionPlan
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
                    Case Is = 0
                        Try
                            Dim periode As Integer = Convert.ToInt32(sTemp)
                            If periode > 0 And periode < 13 Then
                                _ProductionPlan.PeriodMonth = sTemp
                            Else
                                _ProductionPlan.ErrorMessage = "Periode Tidak Valid"
                            End If
                        Catch ex As Exception
                            _ProductionPlan.ErrorMessage = "Periode Tidak Valid"
                        End Try
                    Case Is = 1
                        Try
                            If sTemp.Length = 4 Then
                                _ProductionPlan.PeriodYear = Convert.ToInt32(sTemp)
                                If _ProductionPlan.PeriodYear < DateTime.Now.Year Then
                                    _ProductionPlan.ErrorMessage = "Periode Alokasi Kadaluarsa"
                                ElseIf _ProductionPlan.PeriodYear = DateTime.Now.Year Then
                                    If _ProductionPlan.PeriodMonth < DateTime.Now.Month Then
                                        _ProductionPlan.ErrorMessage = "Periode Alokasi Kadaluarsa"
                                    End If
                                End If
                            Else
                                _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Periode Tidak Valid"
                            End If
                        Catch ex As Exception
                            _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Periode Tidak Valid"
                        End Try
                    Case Is = 2
                        If sTemp.Length = 4 Then
                            _ProductionPlan.ProductionYear = sTemp
                        Else
                            _ProductionPlan.ErrorMessage = "Tahun Produksi tidak valid"
                        End If
                    Case Is = 3
                        If sTemp.Length = 8 Then
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "MaterialNumber", MatchType.Exact, sTemp))
                            Dim ArrList = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If ArrList.Count > 0 Then
                                Dim vc As VechileColor
                                vc = CType(ArrList(0), VechileColor)
                                Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

                                If vc.VechileType.ProductCategory.Code.Trim <> companyCode Then
                                    _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Tipe tidak terdapat pada Kategori Produk " & companyCode
                                Else
                                    _ProductionPlan.VechileColor = ArrList(0)
                                End If

                            Else
                                'Dim vc As VechileColor = New VechileColor
                                'vc.MaterialNumber = sTemp
                                '_ProductionPlan.VechileColor = vc
                                _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Model tidak ada (" & sTemp & ")"
                            End If
                        Else
                            'Dim vc As VechileColor = New VechileColor
                            ' vc.MaterialNumber = sTemp
                            _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Model tidak ada(" & sTemp & ")"
                            '_ProductionPlan.VechileColor = vc
                        End If
                    Case Is = 4
                        Try
                            Dim qty As Integer = Convert.ToInt32(sTemp)
                            If qty >= 0 Then
                                _ProductionPlan.PlanQty = qty
                            Else
                                _ProductionPlan.PlanQty = qty
                                _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Quantity Plan < 0"
                            End If
                        Catch ex As Exception
                            _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Rencana Tidak Valid"
                        End Try
                    Case Is = 5
                        Try
                            Dim qty1 As Integer = Convert.ToInt32(sTemp)
                            If qty1 >= 0 Then
                                _ProductionPlan.CarryOverPreviousQty = qty1
                            Else
                                _ProductionPlan.CarryOverPreviousQty = qty1
                                _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Quantity Sisa < 0"
                            End If
                        Catch ex As Exception
                            _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Sisa Tidak Valid"
                        End Try
                    Case Is = 6
                        Try
                            Dim qty2 As Integer = Convert.ToInt32(sTemp)
                            If qty2 >= 0 Then
                                _ProductionPlan.UnselledStock = qty2
                            Else
                                _ProductionPlan.UnselledStock = qty2
                                _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Quantity tidak jual < 0"
                            End If
                          
                        Catch ex As Exception
                            _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Stok tidak jual Tidak Valid"
                        End Try
                    Case Else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If (_ProductionPlan.PlanQty + _ProductionPlan.CarryOverPreviousQty - _ProductionPlan.UnselledStock) < 0 Then
                _ProductionPlan.ErrorMessage = _ProductionPlan.ErrorMessage & ";" & "Quantity Tidak Valid"
            End If

            If Not _ProductionPlan.VechileColor Is Nothing Then
                Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(PKProductionPlan), "VechileColor.ID", MatchType.Exact, _ProductionPlan.VechileColor.ID))
                criterias1.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodMonth", MatchType.Exact, _ProductionPlan.PeriodMonth))
                criterias1.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodYear", MatchType.Exact, _ProductionPlan.PeriodYear))
                criterias1.opAnd(New Criteria(GetType(PKProductionPlan), "ProductionYear", MatchType.Exact, _ProductionPlan.ProductionYear))
                Dim arrListProductionPlan As ArrayList = New PKProductionPlanFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias1)
                If arrListProductionPlan.Count <> 0 Then
                    If CInt(arrListProductionPlan(0).TotalAlokasi) > CInt(_ProductionPlan.PlanQty) + CInt(_ProductionPlan.CarryOverPreviousQty) - CInt(_ProductionPlan.UnselledStock) Then
                        _ProductionPlan.ErrorMessage = "Rencana Stok < Alokasi"
                    End If
                End If
                For Each item As PKProductionPlan In ProductionPlans
                    If Not item.VechileColor Is Nothing Then
                        If item.PeriodMonth = _ProductionPlan.PeriodMonth Then
                            If item.PeriodYear = _ProductionPlan.PeriodYear Then
                                If item.VechileColor.ID = _ProductionPlan.VechileColor.ID Then
                                    If item.ProductionYear = _ProductionPlan.ProductionYear Then
                                        item.ErrorMessage = "Duplikasi Perencanaan Produksi"
                                        _ProductionPlan.ErrorMessage = "Duplikasi Perencanaan Produksi"
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            ProductionPlans.Add(_ProductionPlan)
        End Sub

#End Region

#Region "Public Properties"

        ReadOnly Property ProductionPlanCollection() As ArrayList
            Get
                Return ProductionPlans
            End Get
        End Property

#End Region

    End Class
End Namespace