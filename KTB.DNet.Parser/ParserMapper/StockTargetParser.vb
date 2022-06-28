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

    Public Class StockTargetParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private StockTargets As ArrayList
        Private Grammar As Regex
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoTransaction() As Integer
            If StockTargets.Count > 0 Then
                'Do Business - Like saveTo Database
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                _Stream = New StreamReader(fileName, True)
                StockTargets = New ArrayList
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                While (Not val = "")
                    'ParseStockTarget(val + delimited)
                    Try
                        ParseStockTarget(val + delimited)
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "StockTargetParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.StockTargetParser, BlockName)
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While
            Finally
                _Stream.Close()
                _Stream = Nothing
            End Try
            Return StockTargets
        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseStockTarget(ByVal ValParser As String)
            Dim _StockTarget As StockTarget = New StockTarget
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
                            'Dim periode As Integer = Convert.ToInt32(sTemp)
                            'If periode > 0 And periode < 13 Then
                            '    _StockTarget.PeriodMonth = sTemp
                            'Else
                            '    _StockTarget.ErrorMessage = "Periode Tidak Valid"
                            'End If
                        Catch ex As Exception
                            _StockTarget.ErrorMessage = "Periode Tidak Valid"
                        End Try
                    Case Is = 1
                        Try
                            If sTemp.Length = 4 Then
                                '_StockTarget.PeriodYear = Convert.ToInt32(sTemp)
                                'If _StockTarget.PeriodYear < DateTime.Now.Year Then
                                '    _StockTarget.ErrorMessage = "Periode Alokasi Kadaluarsa"
                                'ElseIf _StockTarget.PeriodYear = DateTime.Now.Year Then
                                '    If _StockTarget.PeriodMonth < DateTime.Now.Month Then
                                '        _StockTarget.ErrorMessage = "Periode Alokasi Kadaluarsa"
                                '    End If
                                'End If
                            Else
                                _StockTarget.ErrorMessage = _StockTarget.ErrorMessage & ";" & "Periode Tidak Valid"
                            End If
                        Catch ex As Exception
                            _StockTarget.ErrorMessage = _StockTarget.ErrorMessage & ";" & "Periode Tidak Valid"
                        End Try
                    Case Is = 2
                        If sTemp.Length = 4 Then
                            '_StockTarget.ProductionYear = sTemp
                        Else
                            _StockTarget.ErrorMessage = "Tahun Produksi tidak valid"
                        End If
                    Case Is = 3
                        If sTemp.Length = 8 Then
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "MaterialNumber", MatchType.Exact, sTemp))
                            Dim ArrList = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If ArrList.Count > 0 Then
                                '_StockTarget.VechileColor = ArrList(0)
                            Else
                                'Dim vc As VechileColor = New VechileColor
                                'vc.MaterialNumber = sTemp
                                '_StockTarget.VechileColor = vc
                                _StockTarget.ErrorMessage = _StockTarget.ErrorMessage & ";" & "Model tidak ada (" & sTemp & ")"
                            End If
                        Else
                            'Dim vc As VechileColor = New VechileColor
                            ' vc.MaterialNumber = sTemp
                            _StockTarget.ErrorMessage = _StockTarget.ErrorMessage & ";" & "Model tidak ada(" & sTemp & ")"
                            '_StockTarget.VechileColor = vc
                        End If
                    Case Is = 4
                        Try
                            Dim qty As Integer = Convert.ToInt32(sTemp)
                            If qty >= 0 Then
                                '_StockTarget.PlanQty = qty
                            Else
                                '_StockTarget.PlanQty = qty
                                _StockTarget.ErrorMessage = _StockTarget.ErrorMessage & ";" & "Quantity Plan < 0"
                            End If
                        Catch ex As Exception
                            _StockTarget.ErrorMessage = _StockTarget.ErrorMessage & ";" & "Rencana Tidak Valid"
                        End Try
                    Case Is = 5
                        Try
                            Dim qty1 As Integer = Convert.ToInt32(sTemp)
                            If qty1 >= 0 Then
                                '_StockTarget.CarryOverPreviousQty = qty1
                            Else
                                '_StockTarget.CarryOverPreviousQty = qty1
                                _StockTarget.ErrorMessage = _StockTarget.ErrorMessage & ";" & "Quantity Sisa < 0"
                            End If
                        Catch ex As Exception
                            _StockTarget.ErrorMessage = _StockTarget.ErrorMessage & ";" & "Sisa Tidak Valid"
                        End Try
                    Case Is = 6
                        Try
                            Dim qty2 As Integer = Convert.ToInt32(sTemp)
                            If qty2 >= 0 Then
                                '_StockTarget.UnselledStock = qty2
                            Else
                                '_StockTarget.UnselledStock = qty2
                                _StockTarget.ErrorMessage = _StockTarget.ErrorMessage & ";" & "Quantity tidak jual < 0"
                            End If

                        Catch ex As Exception
                            _StockTarget.ErrorMessage = _StockTarget.ErrorMessage & ";" & "Stok tidak jual Tidak Valid"
                        End Try
                    Case Else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            'If (_StockTarget.PlanQty + _StockTarget.CarryOverPreviousQty - _StockTarget.UnselledStock) < 0 Then
            '    _StockTarget.ErrorMessage = _StockTarget.ErrorMessage & ";" & "Quantity Tidak Valid"
            'End If

            'If Not _StockTarget.VechileColor Is Nothing Then
            '    Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StockTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    criterias1.opAnd(New Criteria(GetType(StockTarget), "VechileColor.ID", MatchType.Exact, _StockTarget.VechileColor.ID))
            '    criterias1.opAnd(New Criteria(GetType(StockTarget), "PeriodMonth", MatchType.Exact, _StockTarget.PeriodMonth))
            '    criterias1.opAnd(New Criteria(GetType(StockTarget), "PeriodYear", MatchType.Exact, _StockTarget.PeriodYear))
            '    criterias1.opAnd(New Criteria(GetType(StockTarget), "ProductionYear", MatchType.Exact, _StockTarget.ProductionYear))
            '    Dim arrListStockTarget As ArrayList = New StockTargetFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias1)
            '    If arrListStockTarget.Count <> 0 Then
            '        If CInt(arrListStockTarget(0).TotalAlokasi) > CInt(_StockTarget.PlanQty) + CInt(_StockTarget.CarryOverPreviousQty) - CInt(_StockTarget.UnselledStock) Then
            '            _StockTarget.ErrorMessage = "Rencana Stok < Alokasi"
            '        End If
            '    End If
            '    For Each item As StockTarget In StockTargets
            '        If Not item.VechileColor Is Nothing Then
            '            If item.PeriodMonth = _StockTarget.PeriodMonth Then
            '                If item.PeriodYear = _StockTarget.PeriodYear Then
            '                    If item.VechileColor.ID = _StockTarget.VechileColor.ID Then
            '                        If item.ProductionYear = _StockTarget.ProductionYear Then
            '                            item.ErrorMessage = "Duplikasi Perencanaan Produksi"
            '                            _StockTarget.ErrorMessage = "Duplikasi Perencanaan Produksi"
            '                        End If
            '                    End If
            '                End If
            '            End If
            '        End If
            '    Next
            'End If
            'StockTargets.Add(_StockTarget)
        End Sub

#End Region

#Region "Public Properties"

        ReadOnly Property StockTargetCollection() As ArrayList
            Get
                Return StockTargets
            End Get
        End Property

#End Region

    End Class
End Namespace