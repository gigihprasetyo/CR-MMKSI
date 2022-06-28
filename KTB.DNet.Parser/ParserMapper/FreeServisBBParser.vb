#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
#End Region

Namespace KTB.DNet.Parser

    Public Class FreeServisBBParser
        Inherits AbstractParser

#Region "Private Variables"
        Private status As String
        Private _Stream As StreamReader
        Private FreeServisBB As ArrayList
        Private Grammar As Regex
        Private _sessHelper As SessionHelper = New SessionHelper
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(",")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            FreeServisBB = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()

            While (Not val = "")
                'ParseFreeServisBB(val + delimited)
                Try
                    ParseFreeServisBB(val + delimited)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "FreeServisBBParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.FreeServisParser, BlockName)
                End Try

                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return FreeServisBB
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If FreeServisBB.Count > 0 Then
                'Do Business - Like save to database
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseFreeServisBB(ByVal ValParser As String)
            Dim _FreeServisBB As FreeServiceBB = New FreeServiceBB
            Dim _EndCustomer As EndCustomer = New EndCustomer
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim nDefaultKM As Integer = 50000
            Dim StrDealerFSTxtCode As String = String.Empty
            Dim ObjDealerTmp As Dealer = New Dealer
            Dim Rangka As ChassisMasterBB

            'Dim strDealerCode As String = String.Empty --0
            Dim strDealerBranchCode As String = String.Empty
            Dim strChasis As String = String.Empty '--1
            Dim strEngine As String = String.Empty '--2 'reza
            Dim strFSKind As String = String.Empty '--3
            Dim strTglService As String = String.Empty '--4
            Dim strTglJual As String = String.Empty '--5
            Dim strKm As String = String.Empty '--6
            Dim strVisitType As String = String.Empty '--7
            Dim strWONumber As String = String.Empty

            Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
            sStart = 0
            nCount = 0

            Dim critDealer As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critDealer.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
            Dim ArryListDealer As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critDealer)
            If ArryListDealer.Count > 0 Then
                ObjDealerTmp = CType(ArryListDealer(0), Dealer)
            End If
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0 'Kode Dealer
                        StrDealerFSTxtCode = sTemp.Trim
                        '_sessHelper.SetSession("sessFSDealerCode", StrDealerFSCode)
                        If sTemp.Trim <> strDealerCode Then
                            If _FreeServisBB.ErrorMessage = "" Then
                                _FreeServisBB.ErrorMessage = "Kode Dealer Tidak Cocok"
                            Else
                                _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Kode Dealer Tidak Cocok"
                            End If
                        Else
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, sTemp.Trim))
                            Dim ArryList As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If ArryList.Count > 0 Then
                                For Each ObjDealer As Dealer In ArryList
                                    _FreeServisBB.Dealer = ObjDealer
                                Next
                            End If
                        End If
                        
                    Case Is = 1 'Nomor Rangka
                        strChasis = sTemp.Trim
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, strChasis))
                        Dim ArryList As ArrayList = New ChassisMasterBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        If ArryList.Count > 0 Then
                            Rangka = CType(ArryList(0), ChassisMasterBB)
                            For Each ObjChassisMasterBB As ChassisMasterBB In ArryList

                                'tambahan change request UAT
                                'Start  :by:dna;on:20111017;for:angga;remark:remove this validator for FS Special
                                'If ObjDealerTmp.ID = ObjChassisMasterBB.Dealer.ID Then
                                '    'periksa chassis udah PDI belum
                                '    Dim critIsPDI As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                '    critIsPDI.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMasterBB.ID", MatchType.Exact, ObjChassisMasterBB.ID))
                                '    critIsPDI.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Selesai, String)))
                                '    Dim ArrIsPDI As ArrayList = New PDIFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critIsPDI)
                                '    If ArrIsPDI.Count > 0 Then 'sudah PDI
                                '        _FreeServisBB.chassismasterBB = ObjChassisMasterBB
                                '    Else
                                '        If _FreeServisBB.ErrorMessage = "" Then
                                '            _FreeServisBB.ErrorMessage = "No. Rangka belum PDI"
                                '        Else
                                '            _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + " ;<br> No. Rangka belum PDI"
                                '        End If
                                '        _FreeServisBB.ChassisNumberMsg = sTemp.Trim
                                '    End If
                                'Else
                                '    _FreeServisBB.chassismasterBB = ObjChassisMasterBB
                                'End If
                                'End    :by:dna;on:20111017;for:angga;remark:remove this validator for FS Special
                                _FreeServisBB.ChassisMasterBB = ObjChassisMasterBB

                                'akhir dari tambahan change request UAT
                            Next
                        Else
                            If _FreeServisBB.ErrorMessage = "" Then
                                _FreeServisBB.ErrorMessage = "No Rangka tidak terdaftar di FS Special"
                            Else
                                _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br>No Rangka tidak terdaftar di FS Special"
                            End If
                            _FreeServisBB.ChassisNumberMsg = strChasis
                        End If
                    Case Is = 2 'No Mesin
                        strEngine = sTemp.Trim
                        If sTemp.Trim <> "" Then
                            If Not getMatch(strChasis, strEngine) Then
                                If _FreeServisBB.ErrorMessage = "" Then
                                    _FreeServisBB.ErrorMessage = "Nomor Mesin Tidak Sesuai"
                                Else
                                    _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Nomor Mesin Tidak Sesuai"
                                End If
                                _FreeServisBB.EngineNumberMsg = strEngine
                            End If
                        Else
                            If _FreeServisBB.ErrorMessage = "" Then
                                _FreeServisBB.ErrorMessage = "Nomor Mesin Kosong"
                            Else
                                _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Nomor Mesin Kosong"
                            End If
                            _FreeServisBB.EngineNumberMsg = strEngine
                        End If
                    Case Is = 3 'FS Kind
                        strFSKind = sTemp.Trim
                        If strFSKind <> "" Then
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, strFSKind))
                            Dim ArryList = New FSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If ArryList.Count > 0 Then
                                For Each ObjFSKind As FSKind In ArryList
                                    _FreeServisBB.FSKind = ObjFSKind
                                    If Not Rangka Is Nothing Then
                                        If ValidateFSKindOnVehicleType(ObjFSKind, Rangka) Then
                                            _sessHelper.SetSession("sessFSKindKM", ObjFSKind.KM)
                                        Else
                                            _sessHelper.SetSession("sessFSKindKM", nDefaultKM)
                                            If _FreeServisBB.ErrorMessage = "" Then
                                                _FreeServisBB.ErrorMessage = "Jenis FS Tidak Terdaftar"
                                            Else
                                                _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Jenis FS Tidak Terdaftar"
                                            End If
                                            _FreeServisBB.FSKindMsg = strFSKind
                                        End If
                                    End If
                                Next
                            Else
                                _sessHelper.SetSession("sessFSKindKM", nDefaultKM)
                                If _FreeServisBB.ErrorMessage = "" Then
                                    _FreeServisBB.ErrorMessage = "Jenis FS Tidak Terdaftar"
                                Else
                                    _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Jenis FS Tidak Terdaftar"
                                End If
                                _FreeServisBB.FSKindMsg = strFSKind
                            End If
                        Else
                            If _FreeServisBB.ErrorMessage = "" Then
                                _FreeServisBB.ErrorMessage = "Jenis FS Kosong"
                            Else
                                _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Jenis FS Kosong"
                            End If
                            _FreeServisBB.FSKindMsg = strFSKind


                        End If
                    Case Is = 4 'Tanggal FS
                        'Dim tgl As String
                        strTglService = sTemp.Trim
                        If Len(strTglService) <> 8 Then
                            If _FreeServisBB.ErrorMessage = "" Then
                                _FreeServisBB.ErrorMessage = "Format Tgl Servis Salah"
                            Else
                                _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Format Tgl Servis Salah"
                            End If

                            If Len(strTglService) = 0 Then
                                _FreeServisBB.FSDateMsg = New Date(1900, 1, 1)
                            Else
                                _FreeServisBB.FSDateMsg = strTglService
                            End If

                        Else
                            'tgl = sTemp.Substring(2, 2).ToString & "-" & sTemp.Substring(0, 2) & "-" & sTemp.Substring(4, 4)
                            'yang jadi dipakai adalah setting tanggal indonesia
                            'tgl = sTemp.Substring(0, 2).ToString & "-" & sTemp.Substring(2, 2) & "-" & sTemp.Substring(4, 4)
                            Dim tgl As Date

                            Try
                                tgl = New Date(CInt(sTemp.Substring(4, 4)), CInt(sTemp.Substring(2, 2)), CInt(sTemp.Substring(0, 2)))
                            Catch ex As Exception

                            End Try

                            If Not IsDate(tgl) Then
                                If _FreeServisBB.ErrorMessage = "" Then
                                    _FreeServisBB.ErrorMessage = sTemp & " Format Tgl Servis Salah"
                                Else
                                    _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br>" & sTemp & " Format Tgl Servis Salah"
                                End If
                                _FreeServisBB.FSDateMsg = strTglService
                            Else
                                If CType(tgl, Date) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
                                   CType(tgl, Date) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then

                                    If _FreeServisBB.ErrorMessage = "" Then
                                        _FreeServisBB.ErrorMessage = "Format tanggal service salah"
                                    Else
                                        _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Format tanggal service salah"
                                    End If
                                    _FreeServisBB.FSDateMsg = strTglService
                                Else
                                    _FreeServisBB.ServiceDate = tgl
                                    _FreeServisBB.FSDateMsg = ""
                                End If
                            End If
                        End If
                    Case Is = 5 'Tanggal Penjualan
                        'Dim tgl As String
                        strTglJual = sTemp.Trim
                        If Len(strTglJual) <> 8 Then
                            If strTglJual = "" Then
                                'Jika dealer FS dan Dealer Penjualan sama maka tanggal jual harus ada
                                'Dim objDealerFS As Dealer = New Dealer
                                Dim objDealerSold As Dealer = New Dealer
                                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                                'start  :by:dna;for:angga;on:20111018;remark:remove solddate validation
                                'If Not IsNothing(_FreeServisBB.chassismasterBB) Then
                                '    Dim strChassisNumber = _FreeServisBB.chassismasterBB.ChassisNumber
                                '    Dim critDealerSold As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                '    critDealerSold.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, _FreeServisBB.chassismasterBB.Dealer.ID))
                                '    Dim DealerCollSold As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critDealerSold)
                                '    If DealerCollSold.Count > 0 Then
                                '        objDealerSold = DealerCollSold(0)
                                '    End If
                                'End If

                                'If Not IsNothing(objDealerSold) Then
                                '    If objDealerSold.DealerCode = StrDealerFSTxtCode Then
                                '        If _FreeServisBB.ErrorMessage = "" Then
                                '            _FreeServisBB.ErrorMessage = "Tanggal Jual Harus Diisi (Dealer FS = Dealer Penjualan)"
                                '        Else
                                '            _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Tanggal Jual Harus Diisi (Dealer FS = Dealer Penjualan)"
                                '        End If
                                '    Else
                                '        _FreeServisBB.SoldDate = New Date(1900, 1, 1)
                                '    End If
                                '    _FreeServisBB.SoldDate = New Date(1900, 1, 1)
                                'End If
                                'end    :by:dna;for:angga;on:20111018;remark:remove solddate validation
                                If Len(strTglJual) = 0 Then
                                    _FreeServisBB.SoldDateMsg = New Date(1900, 1, 1)
                                Else
                                    _FreeServisBB.SoldDateMsg = strTglJual
                                End If


                            Else
                                If _FreeServisBB.ErrorMessage = "" Then
                                    _FreeServisBB.ErrorMessage = sTemp & " Format Tgl Penjualan Salah"
                                Else
                                    _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br>" & sTemp & " Format Tgl Penjualan Salah"
                                End If
                                _FreeServisBB.SoldDateMsg = strTglJual
                            End If

                        Else 'Len(sTemp.Trim) = 8
                            'Dim tgl As String = sTemp.Substring(2, 2).ToString & "-" & sTemp.Substring(0, 2) & "-" & sTemp.Substring(4, 4)
                            'yang jadi dipakai adalah setting tanggal indonesia
                            'Dim tgl As String = sTemp.Substring(0, 2).ToString & "-" & sTemp.Substring(2, 2) & "-" & sTemp.Substring(4, 4)
                            Dim tgl As Date
                            Try
                                tgl = New Date(CInt(sTemp.Substring(4, 4)), CInt(sTemp.Substring(2, 2)), CInt(sTemp.Substring(0, 2)))
                            Catch

                            End Try

                            If Not IsDate(tgl) Then
                                If _FreeServisBB.ErrorMessage = "" Then
                                    _FreeServisBB.ErrorMessage = sTemp & "Format Tgl Penjualan Salah"
                                Else
                                    _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br>" & sTemp & " Format Tgl Penjualan Salah"
                                End If
                                _FreeServisBB.SoldDateMsg = strTglJual
                            Else

                                If CType(tgl, Date) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
                                   CType(tgl, Date) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
                                    If _FreeServisBB.ErrorMessage = "" Then
                                        _FreeServisBB.ErrorMessage = "Format tanggal jual salah"
                                    Else
                                        _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Format tanggal jual salah"
                                    End If
                                    _FreeServisBB.SoldDateMsg = strTglJual
                                Else
                                    _FreeServisBB.SoldDate = tgl
                                    _FreeServisBB.SoldDateMsg = strTglJual
                                End If
                            End If
                        End If

                    Case Is = 6 'Jarak Tempuh
                        Dim bIsNumeric As Boolean = True
                        strKm = sTemp.Trim
                        If strKm = "" Then
                            If _FreeServisBB.ErrorMessage = "" Then
                                _FreeServisBB.ErrorMessage = "Jarak Tempuh Tidak Boleh Kosong"
                            Else
                                _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Jarak Tempuh Tidak Boleh Kosong"
                            End If
                            _FreeServisBB.MileAgeMsg = strKm
                            _FreeServisBB.MileAge = 0
                            bIsNumeric = False
                        Else
                            Dim i As Integer
                            For i = 0 To Len(strKm) - 1
                                If Not IsNumeric(strKm.Chars(i)) Then
                                    bIsNumeric = False
                                    Exit For
                                End If
                            Next
                            If Not bIsNumeric Then
                                If _FreeServisBB.ErrorMessage = "" Then
                                    _FreeServisBB.ErrorMessage = "Jarak Tempuh Harus Diisi Angka"
                                Else
                                    _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Jarak Tempuh Harus Diisi Angka"
                                End If
                                _FreeServisBB.MileAgeMsg = strKm
                            End If
                        End If

                        If bIsNumeric Then
                            _FreeServisBB.MileAge = CType(strKm, Integer)
                            If _FreeServisBB.MileAge < 1 Then
                                If _FreeServisBB.ErrorMessage = "" Then
                                    _FreeServisBB.ErrorMessage = "Jarak Tempuh Harus Diisi"
                                Else
                                    _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Jarak Tempuh Harus Diisi"
                                End If
                            Else
                                Dim nKM As Integer = CType(_sessHelper.GetSession("sessFSKindKM"), Integer)
                                If _FreeServisBB.MileAge > nKM Then
                                    If _FreeServisBB.ErrorMessage = "" Then
                                        _FreeServisBB.ErrorMessage = "Jarak Tempuh Melebihi Batas"
                                    Else
                                        _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Jarak Tempuh Melebihi Batas"
                                    End If
                                End If
                            End If
                        End If

                    Case Is = 7 'Visit Type
                        strVisitType = sTemp.Trim
                        _FreeServisBB.VisitType = strVisitType
                        If Not IsNothing(_FreeServisBB.FSKind) Then
                            If _FreeServisBB.FSKind.FSType = 2 Then
                                If strVisitType = "" Then
                                    If _FreeServisBB.ErrorMessage = "" Then
                                        _FreeServisBB.ErrorMessage = "Tipe Visit Belum di input"
                                    Else
                                        _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Tipe Visit Belum di input"
                                    End If
                                End If
                            Else
                                If strVisitType.ToUpper <> "WI" AndAlso strVisitType.ToUpper <> "BO" Then
                                    If _FreeServisBB.ErrorMessage = "" Then
                                        _FreeServisBB.ErrorMessage = "Tipe Visit Salah"
                                    Else
                                        _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Tipe Visit Salah"
                                    End If
                                End If
                            End If
                        End If

                    Case Is = 8 'Kode Cabang
                        If (Not String.IsNullOrEmpty(sTemp.Trim)) Then
                            strDealerBranchCode = sTemp.Trim
                            Dim criteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, strDealerBranchCode.Trim))
                            Dim dealerBranches As ArrayList = New DealerBranchFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteria)

                            If (dealerBranches.Count > 0) Then
                                _FreeServisBB.DealerBranch = dealerBranches(0)
                            Else
                                If _FreeServisBB.ErrorMessage = "" Then
                                    _FreeServisBB.ErrorMessage = "Kode Dealer Branch Tidak Cocok"
                                Else
                                    _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Kode Dealer Branch Tidak Cocok"
                                End If
                            End If
                        End If

                    Case Is = 9 'WO Number
                        strWONumber = sTemp.Trim
                        If (Not String.IsNullOrEmpty(strWONumber)) Then
                            _FreeServisBB.WorkOrderNumber = strWONumber
                        End If
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            Dim NumberOfColumn As Integer = GetNumberOfColumn(ValParser, Delimited)
            If NumberOfColumn < 6 Then
                If _FreeServisBB.ErrorMessage = "" Then
                    _FreeServisBB.ErrorMessage = "Format Data tidak lengkap"
                Else
                    _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage & ";<BR>Format Data tidak lengkap"
                End If
            End If

            _FreeServisBB.Status = EnumFSStatus.FSStatus.Baru
            If IsDate(_FreeServisBB.SoldDate) And IsDate(_FreeServisBB.ServiceDate) Then
                If _FreeServisBB.SoldDate > _FreeServisBB.ServiceDate Then
                    If _FreeServisBB.ErrorMessage = "" Then
                        _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage & "<br>" & "Tanggal Penjualan melebihi tanggal Service"
                    Else
                        _FreeServisBB.ErrorMessage = "Tanggal Penjualan melebihi Tanggal Service"
                    End If
                End If


                If Not _FreeServisBB.ServiceDate <= Now Then
                    If _FreeServisBB.ErrorMessage = "" Then
                        _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage & "<br>" & "Tanggal Service melebihi hari ini"
                    Else
                        _FreeServisBB.ErrorMessage = "Tanggal Service melebihi hari ini"

                    End If
                End If
            End If
            'add validation by reza; req miyuki
            If Not IsExistCode(strChasis, strFSKind) Then
                If _FreeServisBB.ErrorMessage = "" Then
                    _FreeServisBB.ErrorMessage = "No. Rangka dengan jenis servis tersebut sudah ada"
                Else
                    _FreeServisBB.ErrorMessage = _FreeServisBB.ErrorMessage + ";<br> Nomor Rangka tidak memiliki data PM"
                End If
            End If
            'end
            FreeServisBB.Add(_FreeServisBB)
        End Sub

        Private Function GetVechileInformationSystem(ByVal code As String)
            Dim userPrinciple As IPrincipal
            Dim VInformationSystemfacade As New ChassisMasterBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim _vechileType As ChassisMasterBB = VInformationSystemfacade.Retrieve(code)
            Return _vechileType
        End Function

        Private Function ValidateFSKindOnVehicleType(ByVal objFreeServiceBB As FreeServiceBB) As Boolean
            Try
                Dim VechileTypeID As Integer = objFreeServiceBB.chassismasterBB.VechileColor.VechileType.ID
                Dim FSKindID As Integer = objFreeServiceBB.FSKind.ID
                Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, FSKindID))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, VechileTypeID))

                Return New FSKindOnVechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critComp).Count > 0
            Catch e As Exception
                Return False
            End Try
        End Function

        Private Function ValidateFSKindOnVehicleType(ByVal _fsKind As FSKind, ByVal _chMaster As ChassisMasterBB) As Boolean
            Try
                Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, _fsKind.ID))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, _chMaster.VechileColor.VechileType.ID))

                Return New FSKindOnVechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critComp).Count > 0
            Catch e As Exception
                Return False
            End Try
        End Function

#End Region

        Private Function getMatch(strChasis As String, strEngine As String) As Boolean
            Dim _match As Boolean = False

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _strChassisNumber = strChasis.Trim()
            Dim _strEngineNumber = strEngine.Trim()
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, _strChassisNumber))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "EngineNumber", MatchType.Exact, _strEngineNumber))
            Dim ChassisColl As ArrayList = New ChassisMasterBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If ChassisColl.Count > 0 Then
                _match = True
            End If

            Return _match
        End Function

        Private Function IsExistCode(ByVal ChassisID As String, ByVal FSKindCode As String) As Boolean
            Dim UserFacade As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            'Periksa agar tidak ada key ganda 
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "ChassisMasterBB.ChassisNumber", MatchType.Exact, ChassisID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "FSKind.KindCode", MatchType.Exact, FSKindCode))
            Dim TestExist As ArrayList = New FreeServiceBBFacade(UserFacade).Retrieve(criterias)
            If TestExist.Count > 0 Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

End Namespace