#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Text
Imports System.Linq
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

    Public Class FreeServisParser
        Inherits AbstractParser

#Region "Private Variables"
        Private errMessage As String
        Private status As String
        Private _Stream As StreamReader
        Private FreeServis As ArrayList
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
            FreeServis = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()

            While (Not val = "")
                'ParseFreeServis(val + delimited)
                Try
                    ParseFreeServis(val + Delimited)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "FreeServisParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.FreeServisParser, BlockName)
                End Try

                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return FreeServis
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If FreeServis.Count > 0 Then
                'Do Business - Like save to database
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"
        '-- Reza
        Private Function getMatch(ByVal ChassisNumber As String, ByVal EngineNumber As String) As Boolean
            Dim _match As Boolean = False

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _strChassisNumber = ChassisNumber.Trim()
            Dim _strEngineNumber = EngineNumber.Trim()
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, _strChassisNumber))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, _strEngineNumber))
            Dim ChassisColl As ArrayList = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If ChassisColl.Count > 0 Then
                _match = True
            End If

            Return _match
        End Function

        Private Function alreadyPM(ByVal fsType As String, ByVal chassis As String) As Boolean
            Dim _rets As Boolean = True
            Dim _fsType As Integer
            Dim arlistFSKind As ArrayList
            Dim criteriasFSKind As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasFSKind.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, fsType))
            arlistFSKind = New FSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasFSKind)
            For Each item As FSKind In arlistFSKind
                _fsType = item.FSType
            Next

            If _fsType = 2 Then
                Dim arlPMHeader As ArrayList
                Dim criteriasPMHeader As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasPMHeader.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, chassis))
                arlPMHeader = New PMHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasPMHeader)
                If arlPMHeader.Count > 0 Then
                    '_rets = False
                    _rets = True '<< Cek PMHeader yang Labor
                Else
                    '_rets = True
                    _rets = False
                    'cek ada apa ngga di pm header kalo ada bisa insert
                End If
            End If

            Return _rets
        End Function

        Private Function IsExistCode(ByVal ChassisID As String, ByVal FSKindCode As String) As Boolean
            Dim UserFacade As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            'Periksa agar tidak ada key ganda 
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, ChassisID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.KindCode", MatchType.Exact, FSKindCode))
            Dim TestExist As ArrayList = New FreeServiceFacade(UserFacade).Retrieve(criterias)
            If TestExist.Count > 0 Then
                Return False
            Else
                Return True
            End If
        End Function
        '-- Reza

        Private Sub ParseFreeServis(ByVal ValParser As String)
            Dim _FreeServis As FreeService = New FreeService
            Dim _EndCustomer As EndCustomer = New EndCustomer
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            Dim mUser As IPrincipal
            Dim nDefaultKM As Integer = 50000
            Dim StrDealerFSTxtCode As String = String.Empty
            Dim ObjDealerTmp As Dealer = New Dealer
            Dim Rangka As ChassisMaster
            Dim objFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(mUser)

            'Dim strDealerCode As String = String.Empty --0
            Dim strChasis As String = String.Empty '--1
            Dim strEngine As String = String.Empty '--2 'reza
            Dim strFSKind As String = String.Empty '--3
            Dim strTglService As String = String.Empty '--4
            Dim strTglJual As String = String.Empty '--5
            Dim strKm As String = String.Empty '--6
            Dim strVisitType As String = String.Empty '--7

            Dim allowed2xPerMountChassis As New ArrayList()


            Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
            sStart = 0
            nCount = 0

            Dim critDealer As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critDealer.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
            Dim ArryListDealer As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critDealer)
            If ArryListDealer.Count > 0 Then
                ObjDealerTmp = CType(ArryListDealer(0), Dealer)
            End If

            Dim NumberOfColumn As Integer = GetNumberOfColumn(ValParser, Delimited)
            If NumberOfColumn < 8 Then
                If _FreeServis.ErrorMessage = "" Then
                    _FreeServis.ErrorMessage = "Format Data tidak lengkap"
                Else
                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage & ";<BR>Format Data tidak lengkap"
                End If
            Else
                For Each m As Match In Grammar.Matches(ValParser)
                    sTemp = ValParser.Substring(sStart, m.Index - sStart)
                    sTemp = sTemp.Trim("""")
                    sTemp = sTemp.Trim()
                    Select Case (nCount)
                        Case Is = 0
                            StrDealerFSTxtCode = sTemp.Trim
                            '_sessHelper.SetSession("sessFSDealerCode", StrDealerFSCode)
                            If sTemp.Trim <> strDealerCode Then
                                If _FreeServis.ErrorMessage = "" Then
                                    _FreeServis.ErrorMessage = "Kode Dealer Tidak Cocok"
                                Else
                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Kode Dealer Tidak Cocok"
                                End If
                            Else
                                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, sTemp.Trim))
                                Dim ArryList As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                                If ArryList.Count > 0 Then
                                    For Each ObjDealer As Dealer In ArryList
                                        _FreeServis.Dealer = ObjDealer
                                    Next
                                End If
                            End If

                        Case Is = 1
                            strChasis = sTemp.Trim
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, sTemp.Trim))
                            Dim ArryList As ArrayList = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If ArryList.Count > 0 Then
                                Rangka = CType(ArryList(0), ChassisMaster)
                                For Each ObjChassisMaster As ChassisMaster In ArryList

                                    'tambahan change request UAT
                                    If ObjDealerTmp.ID = ObjChassisMaster.Dealer.ID Then
                                        'periksa chassis udah PDI belum
                                        Dim critIsPDI As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                        critIsPDI.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.ID", MatchType.Exact, ObjChassisMaster.ID))
                                        critIsPDI.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Selesai, String)))
                                        Dim ArrIsPDI As ArrayList = New PDIFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critIsPDI)
                                        If ArrIsPDI.Count > 0 Then 'sudah PDI
                                            _FreeServis.ChassisMaster = ObjChassisMaster
                                        Else
                                            If _FreeServis.ErrorMessage = "" Then
                                                _FreeServis.ErrorMessage = "No. Rangka belum PDI"
                                            Else
                                                _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + " ;<br> No. Rangka belum PDI"
                                            End If
                                            _FreeServis.ChassisNumberMsg = sTemp.Trim
                                        End If
                                    Else
                                        _FreeServis.ChassisMaster = ObjChassisMaster
                                    End If

                                    'akhir dari tambahan change request UAT
                                Next

                                Dim oChassisMaster As ChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                                Dim fsKindCode As String = ValParser.Split(",")(3)
                                If Not MSPExtendedKindCode(fsKindCode, oChassisMaster) Then
                                    'start penambahan validasi FS tdk bisa digunakan 2x, tdk boleh ambil kode fs yang angka depannya telah digunakan (ex. telah save 7A, maka tdk boleh ambil 7D) dan duration. CR Doni GC
                                    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.KindCode", MatchType.Exact, fsKindCode))
                                    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, sTemp.Trim))
                                    Dim arlFS As ArrayList = New FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critFS)
                                    'Non MSP Ext FS
                                    If arlFS.Count > 0 Then
                                        If _FreeServis.ErrorMessage = "" Then
                                            _FreeServis.ErrorMessage = "Kode Jenis Free Servis telah digunakan, harap pilih kode jenis free servis lain"
                                        Else
                                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Kode Jenis Free Servis telah digunakan, harap pilih kode jenis free servis lain"
                                        End If
                                    End If
                                Else
                                    'MSP Ext FS
                                    Dim critMSPEx As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    critMSPEx.opAnd(New Criteria(GetType(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, sTemp.Trim))

                                    Dim sortColl As SortCollection = New SortCollection
                                    sortColl.Add(New Sort(GetType(MSPExRegistration), "ID", Sort.SortDirection.DESC))

                                    Dim arrMSPExReg As ArrayList = New MSPExRegistrationFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByCriteria(critMSPEx, sortColl)

                                    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.KindCode", MatchType.Partial, fsKindCode.Substring(0, 2)))
                                    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, sTemp.Trim))
                                    Dim sortCollFS As SortCollection = New SortCollection
                                    sortCollFS.Add(New Sort(GetType(FreeService), "ID", Sort.SortDirection.DESC))
                                    Dim arlFS As ArrayList = New FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByCriteria(critFS, sortCollFS)

                                    Dim kmTO As String = ValParser.Split(",")(6)
                                    Dim MaxPMType As Integer = 0
                                    If arrMSPExReg.Count > 0 Then
                                        Dim oMSPExReg As MSPExRegistration = CType(arrMSPExReg(0), MSPExRegistration)
                                        If oMSPExReg.Status <> CType(EnumMSPEx.MSPExStatus.Selesai, Short) Then
                                            If _FreeServis.ErrorMessage = "" Then
                                                _FreeServis.ErrorMessage = "Data tidak dapat disimpan karena Status MSP Extended belum Selesai"
                                            Else
                                                _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Data tidak dapat disimpan karena Status MSP Extended belum Selesai"
                                            End If
                                        Else
                                            If Not oMSPExReg.ValidDateTo.Date >= Date.Now Then
                                                If _FreeServis.ErrorMessage = "" Then
                                                    _FreeServis.ErrorMessage = "Tanggal Service melebihi batas maksimum paket " & oMSPExReg.ValidDateTo.Date
                                                Else
                                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Tanggal Service melebihi batas maksimum paket " & oMSPExReg.ValidDateTo.Date
                                                End If
                                            End If
                                            If Not oMSPExReg.ValidKMTo >= kmTO Then
                                                If _FreeServis.ErrorMessage = "" Then
                                                    _FreeServis.ErrorMessage = "Jarak tempuh melebihi batas maksimum paket " & oMSPExReg.ValidKMTo
                                                Else
                                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Jarak tempuh melebihi batas maksimum paket " & oMSPExReg.ValidKMTo
                                                End If
                                            End If
                                            If Not OverUsedPM(oMSPExReg, arlFS, MaxPMType) Then
                                                If _FreeServis.ErrorMessage = "" Then
                                                    _FreeServis.ErrorMessage = "Data tidak dapat disimpan karena MSP Extended sudah Tidak Aktif"
                                                Else
                                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Data tidak dapat disimpan karena MSP Extended sudah Tidak Aktif"
                                                End If
                                            End If
                                            If Not FSKindForMSPExType(fsKindCode, oMSPExReg) Then
                                                If _FreeServis.ErrorMessage = "" Then
                                                    _FreeServis.ErrorMessage = "Kendaraan tidak berhak menggunakan Kode Jenis Free Servis " & fsKindCode
                                                Else
                                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Kendaraan tidak berhak menggunakan Kode Jenis Free Servis " & fsKindCode
                                                End If
                                            End If
                                        End If
                                    Else
                                        If _FreeServis.ErrorMessage = "" Then
                                            _FreeServis.ErrorMessage = "Kendaraan tidak mendapatkan Extended Service"
                                        Else
                                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Kendaraan tidak mendapatkan Extended Service"
                                        End If
                                    End If
                                End If

                                Dim critFSKindCode As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critFSKindCode.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, sTemp.Trim))

                                Dim arlFSKindCode As ArrayList = New FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critFSKindCode)
                                For Each oFS As FreeService In arlFSKindCode
                                    If Regex.Replace(oFS.FSKind.KindCode, "[^0-9]", "") = Regex.Replace(fsKindCode, "[^0-9]", "") Then
                                        If _FreeServis.ErrorMessage = "" Then
                                            _FreeServis.ErrorMessage = "Anda tidak bisa memilih kode free service " + fsKindCode + " karena telah menggunakan kode free service " + oFS.FSKind.KindCode
                                        Else
                                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Anda tidak bisa memilih kode free service " + fsKindCode + " karena telah menggunakan kode free service " + oFS.FSKind.KindCode
                                        End If
                                    End If
                                Next

                                Dim oDatePembanding As Date

                                If Not IsNothing(oChassisMaster) Then
                                    Dim critCMPKT As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    critCMPKT.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterPKT), "ChassisMaster.ChassisNumber", MatchType.Exact, sTemp.Trim))

                                    Dim arlCMPKT As New ArrayList
                                    arlCMPKT = New ChassisMasterPKTFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critCMPKT)
                                    Dim isSoldDealer As Boolean
                                    isSoldDealer = (oChassisMaster.Dealer.ID = ObjDealerTmp.ID)

                                    If 1 = 1 Then
                                        Dim YearDurationVal As Integer = 2019
                                        Dim MonthDurationVal As Integer = 9
                                        Dim critAppConf As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                        critAppConf.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "Name", MatchType.InSet, "(" + "'YearFSDurationValidation','MonthFSDurationValidation'" + ")"))
                                        Dim srtAppConf As New SortCollection
                                        srtAppConf.Add(New Sort(GetType(AppConfig), "ID", Sort.SortDirection.ASC))

                                        Dim arlAppConf As ArrayList = New AppConfigFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critAppConf, srtAppConf)
                                        If arlAppConf.Count > 0 Then
                                            MonthDurationVal = arlAppConf(0).Value
                                            YearDurationVal = arlAppConf(1).Value
                                        End If
                                        Dim oCMPKT As ChassisMasterPKT
                                        If isSoldDealer Then
                                            If arlCMPKT.Count = 0 Then
                                                If _FreeServis.ErrorMessage = "" Then
                                                    _FreeServis.ErrorMessage = "Nomor Chassis belum memiliki tanggal PKT"
                                                Else
                                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Nomor Chassis belum memiliki tanggal PKT"
                                                End If
                                            End If

                                        End If

                                        If arlCMPKT.Count = 0 Then
                                            If Not IsNothing(oChassisMaster.EndCustomer) AndAlso oChassisMaster.EndCustomer.FakturDate.Year > 1900 Then
                                                oDatePembanding = oChassisMaster.EndCustomer.FakturDate
                                            ElseIf Not IsNothing(oChassisMaster.EndCustomer) AndAlso oChassisMaster.EndCustomer.OpenFakturDate.Year > 1900 Then
                                                oDatePembanding = oChassisMaster.EndCustomer.OpenFakturDate
                                            Else
                                                If _FreeServis.ErrorMessage = "" Then
                                                    _FreeServis.ErrorMessage = "Nomor Chassis belum Open Faktur"
                                                Else
                                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Nomor Chassis belum Open Faktur"
                                                End If
                                            End If
                                        Else
                                            oCMPKT = CType(arlCMPKT(0), ChassisMasterPKT)
                                            If (oCMPKT.PKTDate.Year = YearDurationVal AndAlso oCMPKT.PKTDate.Month < MonthDurationVal) OrElse oCMPKT.PKTDate.Year < YearDurationVal Then
                                                oDatePembanding = oChassisMaster.EndCustomer.FakturDate
                                            Else
                                                oDatePembanding = oCMPKT.PKTDate
                                            End If
                                        End If

                                        Dim critFSKindOnVT As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                        critFSKindOnVT.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKindCode))
                                        critFSKindOnVT.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "VechileType.ID", MatchType.Exact, oChassisMaster.VechileColor.VechileType.ID))

                                        Dim arlFSKindOnVT As ArrayList = New FSKindOnVechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critFSKindOnVT)


                                        critFSKindCode.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.KindCode", MatchType.Exact, fsKindCode))
                                        Dim sortcritFSKindCode As New SortCollection
                                        sortcritFSKindCode.Add(New Sort(GetType(FreeService), "ID", Sort.SortDirection.DESC))
                                        Dim arlFSSpan As ArrayList = New FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critFSKindCode, sortcritFSKindCode)

                                        If arlFSKindOnVT.Count > 0 Then
                                            Dim oFSKindOnVT As FSKindOnVechileType = CType(arlFSKindOnVT(0), FSKindOnVechileType)
                                            Dim ts As TimeSpan = DateTime.Now.Subtract(oDatePembanding)
                                            If arlFSSpan.Count > 0 Then
                                                ts = CType(arlFSSpan(0), FreeService).ServiceDate.Subtract(oDatePembanding)
                                            End If
                                            Dim DayDifference As Integer = Convert.ToInt32(ts.Days)
                                            If DayDifference > oFSKindOnVT.Duration Then
                                                If _FreeServis.ErrorMessage = "" Then
                                                    _FreeServis.ErrorMessage = "Tanggal service melebihi tanggal yang seharusnya"
                                                Else
                                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Tanggal service melebihi tanggal yang seharusnya"
                                                End If
                                            End If
                                        End If
                                    End If
                                Else
                                    If _FreeServis.ErrorMessage = "" Then
                                        _FreeServis.ErrorMessage = "Nomor Chassis belum memiliki tanggal PKT"
                                    Else
                                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Nomor Chassis belum memiliki tanggal PKT"
                                    End If
                                End If
                                'end penambahan validasi FS tdk bisa digunakan 2x, tdk boleh ambil kode fs yang angka depannya telah digunakan (ex. telah save 7A, maka tdk boleh ambil 7D) dan duration. CR Doni GC
                            Else
                                If _FreeServis.ErrorMessage = "" Then
                                    _FreeServis.ErrorMessage = "No Rangka Tidak Terdaftar"
                                Else
                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> No Rangka Tidak Terdaftar"
                                End If
                                _FreeServis.ChassisNumberMsg = sTemp.Trim
                            End If
                        Case Is = 2
                            strEngine = sTemp.Trim
                            If sTemp.Trim <> "" Then
                                If Not getMatch(strChasis, strEngine) Then
                                    If _FreeServis.ErrorMessage = "" Then
                                        _FreeServis.ErrorMessage = "Nomor Mesin Tidak Sesuai"
                                    Else
                                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Nomor Mesin Tidak Sesuai"
                                    End If
                                    _FreeServis.EngineNumberMsg = sTemp.Trim
                                End If
                            Else
                                If _FreeServis.ErrorMessage = "" Then
                                    _FreeServis.ErrorMessage = "Nomor Mesin Kosong"
                                Else
                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Nomor Mesin Kosong"
                                End If
                                _FreeServis.EngineNumberMsg = sTemp.Trim
                            End If
                        Case Is = 3
                            strFSKind = sTemp.Trim
                            If sTemp.Trim <> "" Then
                                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, sTemp.Trim))
                                Dim ArryList = New FSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                                If ArryList.Count > 0 Then
                                    For Each ObjFSKind As FSKind In ArryList
                                        _FreeServis.FSKind = ObjFSKind
                                        If Not Rangka Is Nothing Then
                                            If ValidateFSKindOnVehicleType(ObjFSKind, Rangka) Then
                                                _sessHelper.SetSession("sessFSKindKM", ObjFSKind.KM)
                                            Else
                                                _sessHelper.SetSession("sessFSKindKM", nDefaultKM)
                                                If _FreeServis.ErrorMessage = "" Then
                                                    _FreeServis.ErrorMessage = "Jenis FS Tidak Terdaftar"
                                                Else
                                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Jenis FS Tidak Terdaftar"
                                                End If
                                                _FreeServis.FSKindMsg = sTemp.Trim
                                            End If
                                        Else
                                            _FreeServis.ErrorMessage = "Chassis tidak terdaftar"
                                        End If
                                    Next
                                Else
                                    _sessHelper.SetSession("sessFSKindKM", nDefaultKM)
                                    If _FreeServis.ErrorMessage = "" Then
                                        _FreeServis.ErrorMessage = "Jenis FS Tidak Terdaftar"
                                    Else
                                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Jenis FS Tidak Terdaftar"
                                    End If
                                    _FreeServis.FSKindMsg = sTemp.Trim
                                End If
                            Else
                                If _FreeServis.ErrorMessage = "" Then
                                    _FreeServis.ErrorMessage = "Jenis FS Kosong"
                                Else
                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Jenis FS Kosong"
                                End If
                                _FreeServis.FSKindMsg = sTemp.Trim


                            End If

                        Case Is = 4
                            strTglService = sTemp.Trim
                            If Len(sTemp.Trim) <> 8 Then
                                If _FreeServis.ErrorMessage = "" Then
                                    _FreeServis.ErrorMessage = "Format Tgl Servis Salah"
                                Else
                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Format Tgl Servis Salah"
                                End If

                                If Len(sTemp.Trim) = 0 Then
                                    _FreeServis.FSDateMsg = New Date(1900, 1, 1)
                                Else
                                    _FreeServis.FSDateMsg = sTemp.Trim
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
                                    If _FreeServis.ErrorMessage = "" Then
                                        _FreeServis.ErrorMessage = sTemp & " Format Tgl Servis Salah"
                                    Else
                                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br>" & sTemp & " Format Tgl Servis Salah"
                                    End If
                                    _FreeServis.FSDateMsg = sTemp.Trim
                                Else
                                    If CType(tgl, Date) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
                                       CType(tgl, Date) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then

                                        If _FreeServis.ErrorMessage = "" Then
                                            _FreeServis.ErrorMessage = "Format tanggal service salah"
                                        Else
                                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Format tanggal service salah"
                                        End If
                                        _FreeServis.FSDateMsg = sTemp.Trim
                                    Else
                                        _FreeServis.ServiceDate = tgl
                                        _FreeServis.FSDateMsg = ""
                                        Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                        critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.GreaterOrEqual, tgl.AddDays(-30)))
                                        critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.Lesser, New DateTime(tgl.Year, tgl.Month, tgl.Day)))
                                        critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, _FreeServis.ChassisMaster.ChassisNumber))
                                        Dim srt As New SortCollection()
                                        srt.Add(New Sort(GetType(FreeService), "ServiceDate", Sort.SortDirection.DESC))
                                        Dim arlFS As ArrayList = New FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critFS, srt)
                                        If arlFS.Count > 0 Then
                                            If validateMultipleServiceInMounth_Retrieve(arlFS, FreeServis, _FreeServis) Then
                                                'pass
                                            Else
                                                attachErrorMessage(_FreeServis, "Chassis " + _FreeServis.ChassisMaster.ChassisNumber + " sudah mengajukan 1x service dalam waktu kurang dari 30 hari tanggal free service!!")
                                            End If
                                        Else
                                            'validasi untuk double upload list
                                            validateMultipleServiceInMounth(FreeServis, _FreeServis)
                                        End If

                                    End If
                                End If
                            End If


                        Case Is = 5
                            strTglJual = sTemp.Trim
                            If Len(sTemp.Trim) <> 8 Then
                                If sTemp.Trim = "" Then
                                    'Jika dealer FS dan Dealer Penjualan sama maka tanggal jual harus ada
                                    'Dim objDealerFS As Dealer = New Dealer
                                    Dim objDealerSold As Dealer = New Dealer
                                    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                                    If Not IsNothing(_FreeServis.ChassisMaster) Then
                                        Dim strChassisNumber = _FreeServis.ChassisMaster.ChassisNumber
                                        Dim critDealerSold As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                        critDealerSold.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, _FreeServis.ChassisMaster.Dealer.ID))
                                        Dim DealerCollSold As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critDealerSold)
                                        If DealerCollSold.Count > 0 Then
                                            objDealerSold = DealerCollSold(0)
                                        End If
                                    End If

                                    If Not IsNothing(objDealerSold) Then
                                        If objDealerSold.DealerCode = StrDealerFSTxtCode Then
                                            If _FreeServis.ErrorMessage = "" Then
                                                _FreeServis.ErrorMessage = "Tanggal Jual Harus Diisi (Dealer FS = Dealer Penjualan)"
                                            Else
                                                _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Tanggal Jual Harus Diisi (Dealer FS = Dealer Penjualan)"
                                            End If
                                        Else
                                            _FreeServis.SoldDate = New Date(1900, 1, 1)
                                        End If
                                        _FreeServis.SoldDate = New Date(1900, 1, 1)
                                    End If

                                    If Len(sTemp.Trim) = 0 Then
                                        _FreeServis.SoldDateMsg = New Date(1900, 1, 1)
                                    Else
                                        _FreeServis.SoldDateMsg = sTemp.Trim
                                    End If


                                Else
                                    If _FreeServis.ErrorMessage = "" Then
                                        _FreeServis.ErrorMessage = sTemp & " Format Tgl Penjualan Salah"
                                    Else
                                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br>" & sTemp & " Format Tgl Penjualan Salah"
                                    End If
                                    _FreeServis.SoldDateMsg = sTemp.Trim
                                End If

                            Else 'Len(sTemp.Trim) = 8
                                'Dim tgl As String = sTemp.Substring(2, 2).ToString & "-" & sTemp.Substring(0, 2) & "-" & sTemp.Substring(4, 4)
                                'yang jadi dipakai adalah setting tanggal indonesia
                                'Dim tgl As String = sTemp.Substring(0, 2).ToString & "-" & sTemp.Substring(2, 2) & "-" & sTemp.Substring(4, 4)
                                Dim tgl As Date
                                Try
                                    tgl = New Date(CInt(sTemp.Substring(4, 4)), CInt(sTemp.Substring(2, 2)), CInt(sTemp.Substring(0, 2)))
                                Catch
                                    If _FreeServis.ErrorMessage = "" Then
                                        _FreeServis.ErrorMessage = sTemp & "Format Tgl Penjualan Salah"
                                    Else
                                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br>" & sTemp & " Format Tgl Penjualan Salah"
                                    End If
                                    _FreeServis.SoldDateMsg = sTemp.Trim
                                End Try

                                If sTemp.Trim() = "" Then
                                    If _FreeServis.ErrorMessage = "" Then
                                        _FreeServis.ErrorMessage = sTemp & "Tgl Penjualan Kosong"
                                    Else
                                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br>" & sTemp & "Tgl Penjualan Kosong"
                                    End If
                                End If

                                If Not IsDate(tgl) Then
                                    If _FreeServis.ErrorMessage = "" Then
                                        _FreeServis.ErrorMessage = sTemp & "Format Tgl Penjualan Salah"
                                    Else
                                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br>" & sTemp & " Format Tgl Penjualan Salah"
                                    End If
                                    _FreeServis.SoldDateMsg = sTemp.Trim
                                Else

                                    If CType(tgl, Date) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
                                       CType(tgl, Date) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
                                        If _FreeServis.ErrorMessage = "" Then
                                            _FreeServis.ErrorMessage = "Format tanggal jual salah"
                                        Else
                                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Format tanggal jual salah"
                                        End If
                                        _FreeServis.SoldDateMsg = sTemp.Trim
                                    Else
                                        _FreeServis.SoldDate = tgl
                                        _FreeServis.SoldDateMsg = ""
                                    End If
                                End If
                            End If

                        Case Is = 6
                            strKm = sTemp.Trim
                            Dim bIsNumeric As Boolean = True

                            If sTemp.Trim = "" Then
                                If _FreeServis.ErrorMessage = "" Then
                                    _FreeServis.ErrorMessage = "Jarak Tempuh Tidak Boleh Kosong"
                                Else
                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Jarak Tempuh Tidak Boleh Kosong"
                                End If
                                _FreeServis.MileAgeMsg = sTemp.Trim
                                _FreeServis.MileAge = 0
                                bIsNumeric = False
                            Else
                                Dim i As Integer
                                For i = 0 To Len(sTemp.Trim) - 1
                                    If Not IsNumeric(sTemp.Trim.Chars(i)) Then
                                        bIsNumeric = False
                                        Exit For
                                    End If
                                Next
                                If Not bIsNumeric Then
                                    If _FreeServis.ErrorMessage = "" Then
                                        _FreeServis.ErrorMessage = "Jarak Tempuh Harus Diisi Angka"
                                    Else
                                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Jarak Tempuh Harus Diisi Angka"
                                    End If
                                    _FreeServis.MileAgeMsg = sTemp.Trim
                                End If
                            End If

                            If bIsNumeric Then
                                _FreeServis.MileAge = CType(sTemp.Trim, Integer)
                                If _FreeServis.MileAge < 1 Then
                                    If _FreeServis.ErrorMessage = "" Then
                                        _FreeServis.ErrorMessage = "Jarak Tempuh Harus Diisi"
                                    Else
                                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Jarak Tempuh Harus Diisi"
                                    End If
                                Else
                                    Dim nKM As Integer = CType(_sessHelper.GetSession("sessFSKindKM"), Integer)
                                    If _FreeServis.MileAge > nKM Then
                                        If _FreeServis.ErrorMessage = "" Then
                                            _FreeServis.ErrorMessage = "Jarak Tempuh Melebihi Batas"
                                        Else
                                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Jarak Tempuh Melebihi Batas"
                                        End If
                                    End If
                                End If
                            End If

                        Case Is = 7
                            strVisitType = sTemp.Trim
                            _FreeServis.VisitType = sTemp.Trim
                            If Not IsNothing(_FreeServis.FSKind) Then
                                If _FreeServis.FSKind.FSType = 2 Then
                                    If strVisitType = "" Then
                                        If _FreeServis.ErrorMessage = "" Then
                                            _FreeServis.ErrorMessage = "Tipe Visit Belum di input"
                                        Else
                                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Tipe Visit Belum di input"
                                        End If
                                    End If
                                Else
                                    If strVisitType.ToUpper <> "WI" AndAlso strVisitType.ToUpper <> "BO" Then
                                        If _FreeServis.ErrorMessage = "" Then
                                            _FreeServis.ErrorMessage = "Tipe Visit Salah"
                                        Else
                                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Tipe Visit Salah"
                                        End If
                                    End If
                                End If
                            End If

                        Case Is = 8
                            If Not String.IsNullorEmpty(sTemp.Trim) Then
                                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, sTemp.Trim))
                                Dim ArryList As ArrayList = New DealerBranchFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                                If ArryList.Count > 0 Then
                                    For Each ObjDealerBranch As DealerBranch In ArryList
                                        If ObjDealerBranch.Dealer.DealerCode = strDealerCode Then
                                            _FreeServis.DealerBranch = ObjDealerBranch
                                        Else
                                            If _FreeServis.ErrorMessage = "" Then
                                                _FreeServis.ErrorMessage = "Kode Cabang Tidak Cocok"
                                            Else
                                                _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Kode Cabang Tidak Cocok"
                                            End If
                                            _FreeServis.DealerBranchCodeMsg = sTemp.Trim
                                        End If
                                    Next
                                Else
                                    If _FreeServis.ErrorMessage = "" Then
                                        _FreeServis.ErrorMessage = "Kode Cabang Tidak Terdaftar"
                                    Else
                                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Kode Cabang Tidak Terdaftar"
                                    End If
                                    _FreeServis.DealerBranchCodeMsg = sTemp.Trim
                                End If
                            End If
                        Case Is = 9
                            If Not String.IsNullorEmpty(sTemp.Trim) Then
                                _FreeServis.WorkOrderNumber = sTemp.Trim
                            End If
                    End Select
                    sStart = m.Index + 1
                    nCount += 1
                Next
                _FreeServis.Status = EnumFSStatus.FSStatus.Baru
                If IsDate(_FreeServis.SoldDate) And IsDate(_FreeServis.ServiceDate) Then
                    If _FreeServis.SoldDate > _FreeServis.ServiceDate Then
                        If _FreeServis.ErrorMessage = "" Then
                            _FreeServis.ErrorMessage = "Tanggal Penjualan melebihi tanggal Service"
                        Else
                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Tanggal Penjualan melebihi Tanggal Service"
                        End If
                    End If
                    If Not _FreeServis.ServiceDate <= Now Then
                        If _FreeServis.ErrorMessage = "" Then
                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage & "<br>" & "Tanggal Service melebihi hari ini"
                        Else
                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Tanggal Service melebihi hari ini"

                        End If
                    End If
                End If

                'add validation by reza; req miyuki
                If Not alreadyPM(strFSKind, strChasis) Then
                    If _FreeServis.ErrorMessage = "" Then
                        _FreeServis.ErrorMessage = "Nomor Rangka tidak memiliki data PM"
                    Else
                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Nomor Rangka tidak memiliki data PM"
                    End If
                End If

                If Not IsExistCode(strChasis, strFSKind) Then
                    If _FreeServis.ErrorMessage = "" Then
                        _FreeServis.ErrorMessage = "No. Rangka dengan jenis servis tersebut sudah ada"
                    Else
                        _FreeServis.ErrorMessage = _FreeServis.ErrorMessage + ";<br> Nomor Rangka tidak memiliki data PM"
                    End If
                End If
                'end

                'add validation by anh; reg by rna; 20100823
                If Not (strFSKind = "1" Or strFSKind = "2") Then
                    Try
                        If Not _FreeServis.ChassisMaster Is Nothing Then
                            If Not objFreeServiceFacade.IsAllowFreeService(_FreeServis) Then
                                If _FreeServis.ErrorMessage = "" Then
                                    _FreeServis.ErrorMessage = _FreeServis.ErrorMessage & "<br>" & " Kendaraan tidak berhak mendapatkan free service"
                                Else
                                    _FreeServis.ErrorMessage = "Kendaraan tidak berhak mendapatkan free service"
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        If _FreeServis.ErrorMessage = "" Then
                            _FreeServis.ErrorMessage = _FreeServis.ErrorMessage & "<br>" & " Kendaraan tidak berhak mendapatkan free service"
                        Else
                            _FreeServis.ErrorMessage = "Kendaraan tidak berhak mendapatkan free service"
                        End If
                    End Try

                End If


                'end added
            End If

            FreeServis.Add(_FreeServis)
        End Sub

        Private Function GetPMCount(ByVal chassisNumber As String, ByVal oMSPExType As MSPExType) As Integer
            Dim FSKindID As String = "0"
            For Each item As MSPExMappingtoFSKind In GetMaxPM(oMSPExType)
                If FSKindID.Length = 0 Then
                    FSKindID = item.FSKind.ID
                Else
                    FSKindID = FSKindID & "," & item.FSKind.ID
                End If
            Next
            Dim critFS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critFS.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber.Trim))
            critFS.opAnd(New Criteria(GetType(FreeService), "FSKind.ID", MatchType.InSet, "(" & FSKindID & ")"))
            Dim arlFS As ArrayList = New FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critFS)

            If arlFS.Count > 0 Then
                Dim groupedArr = From a As FreeService In arlFS
                                 Group By a.ChassisMaster.ID Into Group
                                 Select Group

                Return groupedArr.Count
            End If
            Return 0
        End Function

        Private Function GetMaxPM(ByVal oMSPExType As MSPExType) As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExMappingtoFSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(MSPExMappingtoFSKind), "MSPExType.ID", MatchType.Exact, oMSPExType.ID))
            Dim arlMaxPM As ArrayList = New MSPExMappingtoFSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(crit)
            If arlMaxPM.Count > 0 Then
                Return arlMaxPM
            End If
            Return New ArrayList
        End Function

        Private Function GetVechileInformationSystem(ByVal code As String)
            Dim userPrinciple As IPrincipal
            Dim VInformationSystemfacade As New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim _vechileType As ChassisMaster = VInformationSystemfacade.Retrieve(code)
            Return _vechileType
        End Function

        Private Function ValidateFSKindOnVehicleType(ByVal objFreeService As FreeService) As Boolean
            Try
                Dim VechileTypeID As Integer = objFreeService.ChassisMaster.VechileColor.VechileType.ID
                Dim FSKindID As Integer = objFreeService.FSKind.ID
                Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, FSKindID))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, VechileTypeID))

                Return New FSKindOnVechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critComp).Count > 0
            Catch e As Exception
                Return False
            End Try
        End Function

        Private Function ValidateFSKindOnVehicleType(ByVal _fsKind As FSKind, ByVal _chMaster As ChassisMaster) As Boolean
            Try
                Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, _fsKind.ID))
                critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, _chMaster.VechileColor.VechileType.ID))

                Return New FSKindOnVechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critComp).Count > 0
            Catch e As Exception
                Return False
            End Try
        End Function

        Private Function ValidateLBUMBengkulu(ByVal _dealerCode As String, ByVal _chmaster As ChassisMaster, ByVal _fsKind As String) As Boolean
            Dim vReturn As Boolean = False
            If _fsKind = "6" _
                AndAlso (_chmaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE7" _
                Or _chmaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE8") _
                Then
                If _dealerCode = "100016" _
                    AndAlso (_chmaster.EndCustomer.OpenFakturDate >= DateSerial(2010, 2, 1) _
                    And _chmaster.EndCustomer.OpenFakturDate <= DateSerial(2010, 6, 30)) _
                    Then
                    vReturn = True
                End If
            End If
            Return vReturn
        End Function

        Private Function OverUsedPM(ByVal oMSPExReg As MSPExRegistration, ByVal arrFS As ArrayList, ByRef MaxPMType As Integer) As Boolean
            MaxPMType = GetMaxPM(oMSPExReg.MSPExMaster.MSPExType).Count
            Dim arrMSPExReg As ArrayList = New MSPExRegistrationFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveArrChassisNumber(oMSPExReg.ChassisMaster.ChassisNumber)
            Dim MaxPM As Integer = MaxPMType * arrMSPExReg.Count
            If arrFS.Count > MaxPM Then
                Return False
            End If
            Return True
        End Function

        Private Function MSPExtendedKindCode(ByVal fsKindCode As String, ByVal oChassisMaster As ChassisMaster) As Boolean
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSType", MatchType.InSet, "(" & EnumFSKind.FSType.Extended2 & "," & EnumFSKind.FSType.Extended4 & "," & EnumFSKind.FSType.Extended6 & ")"))
            crit.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKindCode))
            crit.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, oChassisMaster.VechileColor.VechileType.ID))
            Dim arlData As ArrayList = New FSKindOnVechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(crit)
            If arlData.Count > 0 Then
                Return True
            End If
            Return False
        End Function

        Private Function FSKindForMSPExType(ByVal fsKindCode As String, ByVal oMSPExRegistration As MSPExRegistration) As Boolean
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExMappingtoFSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(MSPExMappingtoFSKind), "MSPExType.ID", MatchType.Exact, oMSPExRegistration.MSPExMaster.MSPExType.ID))
            crit.opAnd(New Criteria(GetType(MSPExMappingtoFSKind), "FSKind.KindCode", MatchType.Exact, fsKindCode))
            Dim arlData As ArrayList = New MSPExMappingtoFSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(crit)
            If arlData.Count > 0 Then
                Return True
            End If
            Return False
        End Function

        Private Function validateMultipleServiceInMounth_Retrieve(ByVal arrFSFromTable As ArrayList, ByVal arrFSUpload As ArrayList, ByRef oFS As FreeService)
            Dim result As Boolean = False
            Dim is2XService As Boolean = False
            'Dim stdCodeCrit As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, 0))
            'stdCodeCrit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "FreeService.1MonthLimitException"))
            'stdCodeCrit.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, 1))
            'Dim value As String() = CType(New StandardCodeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(stdCodeCrit)(0), StandardCode).ValueCode.Split(";")

            'cek apakat model kendaraan terdaftar dalam model yang diperbolehkan 2x servis dalam 30 hari
            'Dim modelID As Integer = CType(arrFSFromTable(0), FreeService).ChassisMaster.VechileColor.VechileType.VechileModel.ID
            Dim cat As Integer = CType(arrFSFromTable(0), FreeService).ChassisMaster.VechileColor.VechileType.VechileModel.Category.ID
            'For Each s As String In value
            '    If s = modelID Then
            '        is2XService = True
            '    End If
            'Next
            If cat = 2 Then
                is2XService = True
            End If

            'jika tidak termasuk, maka false
            If Not is2XService Then
                Return False
            End If

            If arrFSFromTable.Count >= 2 Then   'jika sudah ada 2 record di tabel (record di-retrieve pada 30 hari terakhir), maka tidak boleh ada pengajuan lagi
                attachErrorMessage(oFS, "Chassis " + oFS.ChassisMaster.ChassisNumber + "sudah mengajukan service 2x pada bulan ini!!")
            Else    'jika belum 2x maka cek jumlah recordnya dan jumlah pengajuan melalui file upload
                If arrFSFromTable.Count = 1 Then    'baru ada 1 di tabel
                    Dim oFSTbl As FreeService = CType(arrFSFromTable(0), FreeService)
                    If oFSTbl.ServiceDate.AddDays(14) > oFS.ServiceDate Then 'cek dengan data di tabel
                        attachErrorMessage(oFS, "Chassis " + oFS.ChassisMaster.ChassisNumber + " tipe LCV.  Tanggal Service kurang dari 14 Hari dari tanggal service terakhir.")
                        Return True
                    End If
                    'cek apakah sudah ada chassis valid di list upload sebelumnya. tidak boleh ada valid chassis di list upload karena hanya diperbolehkan upload 1 (1 data sudah ada di tabel)
                    Dim cnt As Integer = countExistChassis(arrFSUpload, oFS)
                    If cnt > 0 Then
                        attachErrorMessage(oFS, "Chassis " + oFS.ChassisMaster.ChassisNumber + " sudah 1 record dan 1 ada file upload yang valid!!")
                        Return True
                    End If
                End If
            End If
            Return True
        End Function

        Private Sub validateMultipleServiceInMounth(ByVal arrFSUpload As ArrayList, ByRef oFS As FreeService)
            'harus dicek untuk PC / LVC. jika PC maka jarak minimal pengajuan 30 hari, LVC 14 hari
            For Each f As FreeService In arrFSUpload
                If IsNothing(f.ErrorMessage) OrElse f.ErrorMessage = String.Empty Then      'chassis valid
                    If f.ChassisMaster.ChassisNumber = oFS.ChassisMaster.ChassisNumber Then     ' jika sudah ada chassis yang sama di row sebelumnya
                        Dim cat As Integer = oFS.ChassisMaster.VechileColor.VechileType.VechileModel.Category.ID
                        If cat = 1 Then     'jika PC maka jarak 30 hari
                            If Not validateHariPengajuan(f, oFS, 30) Then
                                attachErrorMessage(oFS, "Chassis " + oFS.ChassisMaster.ChassisNumber + " kategori PC. Tanggal Service kurang dari 30 Hari dari tanggal service terakhir.")
                                Return
                            End If
                        ElseIf cat = 2 Then     'jika LVC maka 14 hari
                            If Not validateHariPengajuan(f, oFS, 14) Then
                                attachErrorMessage(oFS, "Chassis " + oFS.ChassisMaster.ChassisNumber + " kategori LVC. Tanggal Service kurang dari 14 Hari dari tanggal service terakhir.")
                                Return
                            End If
                        End If
                    End If
                End If
            Next
        End Sub

        Private Function countExistChassis(ByVal arr As ArrayList, ByVal fsCheck As FreeService) As Integer
            Dim counter As Integer = 0
            For Each fs As FreeService In arr
                If Not IsNothing(fs.ChassisMaster) And Not IsNothing(fsCheck.ChassisMaster) Then
                    If fs.ChassisMaster.ChassisNumber = fsCheck.ChassisMaster.ChassisNumber And fs.ErrorMessage = String.Empty Then
                        counter = counter + 1
                    End If
                End If
            Next

            Return counter
        End Function

        Private Sub attachErrorMessage(ByRef oFS As FreeService, ByVal msg As String)
            If oFS.ErrorMessage = "" Then
                oFS.ErrorMessage = msg
            Else
                oFS.ErrorMessage = oFS.ErrorMessage + ";<br>" + msg
            End If
        End Sub

        Private Function validateHariPengajuan(ByRef oFSUpload As FreeService, ByVal oFSCheck As FreeService, ByVal hari As Integer)
            If oFSUpload.ServiceDate > oFSCheck.ServiceDate Then
                If oFSCheck.ServiceDate.AddDays(hari) >= oFSUpload.ServiceDate Then
                    If hari = 14 Then
                        attachErrorMessage(oFSUpload, "Chassis " + oFSUpload.ChassisMaster.ChassisNumber + " kategori LCV. Tanggal Service kurang dari 14 Hari dari tanggal service terakhir.")
                    Else
                        attachErrorMessage(oFSUpload, "Chassis " + oFSUpload.ChassisMaster.ChassisNumber + " kategori PC. Tanggal Service kurang dari 30 Hari dari tanggal service terakhir.")
                    End If
                    Return True
                Else
                    Return True
                End If
            ElseIf oFSUpload.ServiceDate < oFSCheck.ServiceDate Then
                If oFSUpload.ServiceDate.AddDays(hari) >= oFSCheck.ServiceDate Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If
            Return True
        End Function
#End Region

#Region "Public method"
        Public Function IsAllowToSave() As Boolean
            If errMessage = String.Empty Then
                Return True
            Else
                Return False
            End If
        End Function
#End Region


    End Class

End Namespace