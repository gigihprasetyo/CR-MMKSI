#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region


Namespace KTB.DNet.Parser
    Public Class MaterialAllocationParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _MaterialAllocation As ArrayList
        Private _MaterialPromotionPeriod As MaterialPromotionPeriod
        Private Grammar As Regex
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private sBuilder As StringBuilder
        Private _sesHelper As New SessionHelper
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(";")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            _MaterialAllocation = New ArrayList
            _fileName = fileName

            Dim val As String = MyBase.NextLine(_Stream).Trim()

            ParseHMaterialAllocation(val + ";")

            'If Not IsNothing(_MaterialPromotionPeriod) Then
            val = MyBase.NextLine(_Stream)
            While (Not val = "")
                Try
                    ParseMaterialAllocation(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "MaterialAllocationParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.MaterialAllocationParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & ";" & Chr(13) & val & ";" & Chr(13) & ex.Message)
                End Try
                val = MyBase.NextLine(_Stream)
            End While

            'End If
            _Stream.Close()
            _Stream = Nothing
            If _MaterialAllocation.Count > 0 Then
                _sesHelper.SetSession("DetailAllocation", _MaterialAllocation)
            Else
                _sesHelper.SetSession("DetailAllocation", Nothing)
            End If
            Return _MaterialAllocation
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function
#End Region

#Region "Private Methods"

        Private Sub ParseMaterialAllocation(ByVal valParser As String)
            Dim _MaterialPromotionAllocation As MaterialPromotionAllocation = New MaterialPromotionAllocation
            Dim arrDetail As ArrayList = New ArrayList
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            sBuilder = New StringBuilder
            '---variable for teks file
            Dim _dealerCode As String = String.Empty
            Dim _ItemCode As String = String.Empty
            Dim _itemName As String = String.Empty
            Dim _itemStock As String = 0
            Dim _itemAlloc As String = 0

            Dim _formatteks As Boolean = True
            Dim _keyDetail As Boolean = True

            Dim namaBarang As String = String.Empty
            Dim stockBarang As String = String.Empty
            Dim alokasiQty As String = String.Empty

            For Each m As Match In Grammar.Matches(valParser)
                sTemp = valParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()

                Select Case (nCount)

                    Case Is = 0
                        'If sTemp.Length > 0 Then
                        '    If sTemp.ToLower <> "d" Then
                        '        sBuilder.Append("Key Detail tidak ada" & ";" & Chr(13))
                        '    End If
                        'Else
                        '    sBuilder.Append("Key Detail tidak valid" & ";" & Chr(13))
                        'End If
                        If sTemp.ToLower = "d" Then
                            _keyDetail = True
                        Else
                            _keyDetail = False
                        End If
                    Case Is = 1
                        'If sTemp.Length > 0 Then
                        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, sTemp))
                        'Dim al As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                        'If al.Count > 0 Then
                        '    _MaterialPromotionAllocation.Dealer = CType(al(0), Dealer)   '-- Dealer object
                        'Else
                        '    sBuilder.Append("Dealer tidak ditemukan" & ";" & Chr(13))
                        'End If
                        _dealerCode = sTemp
                        ' Else
                        ' sBuilder.Append("Kode Dealer tidak valid" & ";" & Chr(13))
                        'End If

                    Case Is = 2
                        'If sTemp.Length > 0 Then
                        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(MaterialPromotion), "GoodNo", MatchType.Exact, sTemp))
                        'Dim al As ArrayList = New MaterialPromotionFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                        'If al.Count > 0 Then
                        '    _MaterialPromotionAllocation.MaterialPromotion = CType(al(0), MaterialPromotion) '-- MaterialPromotion object
                        '    namaBarang = _MaterialPromotionAllocation.MaterialPromotion.Name
                        '    stockBarang = _MaterialPromotionAllocation.MaterialPromotion.Stock

                        '    '-- Check if this already exists in preceeding lines
                        '    If ExistItem(sTemp, _MaterialPromotionAllocation.Dealer.DealerCode) Then
                        '        _MaterialPromotionAllocation.ErrorMessage = "Kode Barang tidak ditemukan"
                        '    End If

                        'Else
                        '    sBuilder.Append("Kode Barang sudah Ada" & ";" & Chr(13))
                        'End If
                        _ItemCode = sTemp
                        'Else
                        '     sBuilder.Append("Kode Barang tidak valid" & ";" & Chr(13))
                        'End If

                    Case Is = 3
                        'If sTemp.Length > 0 Then
                        'If namaBarang.Trim.ToLower <> sTemp.Trim.ToLower Then
                        '    _MaterialPromotionAllocation.ErrorMessage = "Nama Barang tidak sama dengan Kode barang"
                        'End If
                        _itemName = sTemp
                        'Else
                        '    sBuilder.Append("Nama Barang tidak valid" & ";" & Chr(13))
                        'End If

                    Case Is = 4
                        'If sTemp.Length > 0 Then
                        'If stockBarang <> CInt(sTemp) Then
                        '    sBuilder.Append("Stok Barang tidak sama dengan dengan Real Stok" & ";" & Chr(13))
                        '    _MaterialPromotionAllocation.ErrorMessage = "Stok Barang tidak sama dengan dengan Real Stok"
                        'End If
                        _itemStock = sTemp
                        'Else
                        '   sBuilder.Append("Stok Barang tidak Valid" & ";" & Chr(13))
                        'End If

                    Case Is = 5
                        'If sTemp.Length > 0 Then
                        'If IsNumeric(sTemp) Then
                        '    _MaterialPromotionAllocation.Qty = CInt(sTemp)
                        'Else
                        '    sBuilder.Append("Jumlah Alokasi Barang harus angka" & ";" & Chr(13))
                        'End If
                        _itemAlloc = sTemp
                        'Else
                        '    sBuilder.Append("Jumlah Alokasi tidak valid" & ";" & Chr(13))
                        'End If

                    Case Else
                        'sBuilder.Append("Format Detail Tidak Valid" & ";" & Chr(13))
                        _formatteks = False

                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If IsNothing(_MaterialPromotionPeriod) Then
                arrDetail.Add(_dealerCode)
                arrDetail.Add(_ItemCode)
                arrDetail.Add(_itemName)
                arrDetail.Add(_itemStock)
                arrDetail.Add(_itemAlloc)
                sBuilder.Append("Data Periode Tidak Ada" & ";" & Chr(13))
                arrDetail.Add(sBuilder.ToString)
                _MaterialAllocation.Add(arrDetail)

            Else

                If Not _keyDetail Then
                    sBuilder.Append("Key Detail tidak valid" & ";" & Chr(13))
                End If

                '---cek dealer
                Try
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, _dealerCode))
                    Dim alDealer As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                    If Not (alDealer.Count > 0) Then
                        _sesHelper.SetSession("SaveStatus", False)
                        sBuilder.Append("Dealer tidak ditemukan" & ";" & Chr(13))
                    End If
                Catch ex As Exception
                    sBuilder.Append("Kode Dealer tidak Valid" & ";" & Chr(13))
                End Try

                '---cek Material 
                Dim criteriasMat As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasMat.opAnd(New Criteria(GetType(MaterialPromotion), "GoodNo", MatchType.Exact, _ItemCode))
                Try
                    Dim alMaterial As ArrayList = New MaterialPromotionFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasMat)

                    If Not (alMaterial.Count > 0) Then
                        sBuilder.Append("Kode Barang tidak ditemukan" & ";" & Chr(13))
                        'sBuilder.Append("Nama Barang tidak ditemukan" & ";" & Chr(13))
                        'sBuilder.Append("Stok Barang tidak ditemukan" & ";" & Chr(13))
                        'sBuilder.Append("Alokasi Barang tidak ditemukan" & ";" & Chr(13))
                        _sesHelper.SetSession("SaveStatus", False)
                        arrDetail.Add(_dealerCode)
                        arrDetail.Add(_ItemCode)
                        arrDetail.Add(_itemName)
                        arrDetail.Add(_itemStock)
                        arrDetail.Add(_itemAlloc)
                        arrDetail.Add(sBuilder.ToString)
                        _MaterialAllocation.Add(arrDetail)
                    Else
                        '--cek duplicate or not
                        '-if duplicate then
                        '----sBuilder.Append("Kode Barang Sudah Ada" & ";" & Chr(13))
                        '-end if

                        Dim _ItemMaterial As MaterialPromotion = CType(alMaterial(0), MaterialPromotion)
                        '---cek Nama barang
                        If (_itemName <> _ItemMaterial.Name) Then
                            sBuilder.Append("Nama Barang tidak sesuai dengan kode barang" & ";" & Chr(13))
                            _sesHelper.SetSession("SaveStatus", False)
                        End If

                        '---cek Stok

                        If (_itemStock <> _ItemMaterial.Stock) Then
                            sBuilder.Append("Stock Barang tidak sesuai dengan data" & ";" & Chr(13))
                            _sesHelper.SetSession("SaveStatus", False)
                            '_itemStock = _ItemMaterial.Stock

                        End If

                        If _itemAlloc > _ItemMaterial.Stock Then
                            sBuilder.Append("Kuantiti Alokasi tidak boleh lebih besar dari Stok " & ";" & Chr(13))
                            _sesHelper.SetSession("SaveStatus", False)
                        End If

                        '---cek allokasi
                        'Dim criteriasAlloc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criteriasAlloc.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "Dealer.DealerCode", MatchType.Exact, _dealerCode))
                        'criteriasAlloc.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotion.ID", MatchType.Exact, _ItemMaterial.ID))
                        'criteriasAlloc.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotionPeriod.ID", MatchType.Exact, CType(_sesHelper.GetSession("HeaderAllocation"), MaterialPromotionPeriod).ID))
                        'Dim arlAlloc As ArrayList = New MaterialPromotionAllocationFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasAlloc)
                        'If arlAlloc.Count > 0 Then

                        '    Dim ItemAllocation As MaterialPromotionAllocation = CType(arlAlloc(0), MaterialPromotionAllocation)
                        '    If (ItemAllocation.Qty.ToString <> _itemAlloc.ToString) Then
                        '        'sBuilder.Append("Alokasi tidak sesuai dengan data" & ";" & Chr(13))
                        '        _itemAlloc = ItemAllocation.Qty.ToString
                        '    End If
                        '    'ItemAllocation.ErrorMessage = sBuilder.ToString
                        '    'ItemAllocation.MaterialPromotionPeriod = CType(_sesHelper.GetSession("HeaderAllocation"), MaterialPromotionPeriod)
                        '    '_MaterialAllocation.Add(ItemAllocation)
                        'Else
                        '    sBuilder.Append("Alokasi yang sesuai Periode belum ada" & ";" & Chr(13))
                        'End If
                        arrDetail.Add(_dealerCode)
                        arrDetail.Add(_ItemCode)
                        arrDetail.Add(_itemName)
                        arrDetail.Add(_itemStock)
                        arrDetail.Add(_itemAlloc)
                        arrDetail.Add(sBuilder.ToString)
                        _MaterialAllocation.Add(arrDetail)

                    End If
                Catch ex As Exception
                    sBuilder.Append("Kode Barang tidak valid" & ";" & Chr(13))
                End Try
            End If

            

        End Sub

        Private Sub ParseHMaterialAllocation(ByVal valParser As String)
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            sBuilder = New StringBuilder

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            For Each m As Match In Grammar.Matches(valParser)
                sTemp = valParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Length > 0 Then
                            If sTemp.ToLower <> "h" Then
                                sBuilder.Append("Key Header Tidak Ada" & ";" & Chr(13))
                            End If
                        Else
                            sBuilder.Append("Key Header Tidak Valid" & ";" & Chr(13))
                        End If

                    Case Is = 1
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "StartMonth", MatchType.Exact, CInt(sTemp)))
                            Else
                                sBuilder.Append("Mulai Period - Bulan harus angka" & ";" & Chr(13))
                            End If
                        Else
                            sBuilder.Append("Mulai Period - Bulan tidak valid" & ";" & Chr(13))
                        End If

                    Case Is = 2
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "EndMonth", MatchType.Exact, CInt(sTemp)))
                            Else
                                sBuilder.Append("Akhir Period - Bulan harus angka" & ";" & Chr(13))
                            End If
                        Else
                            sBuilder.Append("Akhir Period - Bulan tidak valid" & ";" & Chr(13))
                        End If

                    Case Is = 3
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "YearPeriod", MatchType.Exact, CInt(sTemp)))
                            Else
                                sBuilder.Append("Tahun Periode harus angka" & ";" & Chr(13))
                            End If
                        Else
                            sBuilder.Append("Tahun Period tidak valid" & ";" & Chr(13))
                        End If

                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            Try
                Dim arlPeriod As ArrayList = New MaterialPromotionPeriodFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                If arlPeriod.Count > 0 Then
                    _sesHelper.SetSession("SaveStatus", True)
                    _MaterialPromotionPeriod = arlPeriod(0)
                    _MaterialPromotionPeriod.ErrorMessage = sBuilder.ToString
                    _sesHelper.SetSession("HeaderAllocation", _MaterialPromotionPeriod)
                Else
                    _sesHelper.SetSession("HeaderAllocation", Nothing)
                    _sesHelper.SetSession("SaveStatus", False)

                End If
            Catch ex As Exception
                sBuilder.Append("Data Header Tidak Valid" & ";" & Chr(13))
            End Try
        End Sub

        Private Function ExistItem(ByVal GoodNo As String, ByVal dealerCode As String)
            '-- Check if detail already exists in preceeding line(s)

            For Each objDetail As MaterialPromotionAllocation In _MaterialAllocation
                If Not IsNothing(objDetail.MaterialPromotion) Then

                    If (objDetail.MaterialPromotion.GoodNo.Trim.ToUpper = GoodNo.Trim.ToUpper) _
                        And (objDetail.Dealer.DealerCode.Trim.ToLower = dealerCode.Trim.ToLower) Then

                        Return True  '-- Exists
                    End If
                End If
            Next

            Return False  '-- Not exist
        End Function

#End Region

    End Class
End Namespace

