#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class InvoiceParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _stream As StreamReader
        Private _grammar As Regex
        Private _ListData As ArrayList
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            _grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _stream = New StreamReader(fileName, True)
            _ListData = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_stream).Trim()
            While (Not val = "")
                Try
                    ParseEndCustomer(val)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "InvoiceParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.InvoiceParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_stream)
            End While

            If Not _stream Is Nothing Then
                _stream.Close()
                _stream = Nothing
            End If
            Return _ListData
        End Function

        Private Sub ParseEndCustomer(ByVal ValParser As String)
            Dim objEndCustomer As EndCustomer
            Dim objChassisMaster As ChassisMaster
            Dim objRevisionFaktur As RevisionFaktur = New RevisionFaktur
            Dim splittedVal As String()
            Dim errorSb As StringBuilder = New StringBuilder
            splittedVal = ValParser.Split(";")
            Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim objRevisionFakturFacade As RevisionFakturFacade = New RevisionFakturFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim revNumber As String = String.Empty
            Dim isRevision As Boolean = False

            If splittedVal(splittedVal.Length - 1).Trim <> String.Empty Then
                revNumber = splittedVal(splittedVal.Length - 1).Trim
                If revNumber <> String.Empty And revNumber <> "0" Then
                    isRevision = True
                End If
            End If

            ' check if the data is revision faktur, incase there is no revnumber on the file
            Dim critRevFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critRevFaktur.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.ChassisNumber", MatchType.Exact, splittedVal(0).Trim))
            If revNumber <> String.Empty And revNumber <> "0" Then
                critRevFaktur.opAnd(New Criteria(GetType(RevisionFaktur), "RegNumber", MatchType.Exact, revNumber))
            End If

            critRevFaktur.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.Exact, CType(EnumDNET.enumFakturKendaraanRev.Proses, Short)), "(", True)
            critRevFaktur.opOr(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.Exact, CType(EnumDNET.enumFakturKendaraanRev.Selesai, Short)), ")", False)

            Dim objListRevisionFaktur As ArrayList = objRevisionFakturFacade.Retrieve(critRevFaktur)
            If objListRevisionFaktur.Count > 0 Then
                isRevision = True
                revNumber = CType(objListRevisionFaktur(0), RevisionFaktur).RegNumber
            Else
                isRevision = False
            End If

            If Not isRevision Then
                objChassisMaster = objChassisMasterFacade.Retrieve(splittedVal(0).Trim)
                If Not objChassisMaster Is Nothing Then
                    If objChassisMaster.ID > 0 Then
                        objChassisMaster.FakturStatus = CType(CType(EnumChassisMaster.FakturStatus.Selesai, Short), String)

                        objChassisMaster.AlreadySaled = 2
                        If CInt(objChassisMaster.AlreadySaledTime.ToString("yyyy")) < 1900 Then
                            objChassisMaster.AlreadySaledTime = New Date(Date.Now.Year, _
                                    Date.Now.Month, _
                                   Date.Now.Day)
                        End If

                    Else
                        objChassisMaster = New ChassisMaster
                        errorSb.Append("Nomor rangka tidak terdaftar" & Chr(13) & Chr(10))
                    End If
                Else
                    objChassisMaster = New ChassisMaster
                    errorSb.Append("Nomor rangka tidak terdaftar" & Chr(13) & Chr(10))
                End If

                If objChassisMaster.EndCustomer Is Nothing Then
                    objEndCustomer = New EndCustomer
                    objEndCustomer.MarkLoaded()
                    objChassisMaster.EndCustomer = objEndCustomer
                Else
                    objEndCustomer = objChassisMaster.EndCustomer
                End If
            Else
                ' Get Invoice revision
                Dim criterias As New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RegNumber", MatchType.Exact, revNumber))
                Dim InvoiceRevisionList As ArrayList = objRevisionFakturFacade.Retrieve(criterias)
                objRevisionFaktur = InvoiceRevisionList(0)

                '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                'farid Additional 20190709 
                If objRevisionFaktur.OldEndCustomer.IsTemporary = 1 Then
                    objChassisMaster = objChassisMasterFacade.Retrieve(splittedVal(0).Trim)
                    If Not objChassisMaster Is Nothing Then
                        If objChassisMaster.ID > 0 Then
                            objChassisMaster.FakturStatus = CType(CType(EnumChassisMaster.FakturStatus.Selesai, Short), String)
                        End If
                    End If
                End If
                '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                If Not IsNothing(objRevisionFaktur) Then
                    objEndCustomer = objRevisionFaktur.EndCustomer
                    objChassisMaster = objRevisionFaktur.ChassisMaster
                Else
                    objEndCustomer = New EndCustomer
                    errorSb.Append("Rev Number tidak valid" & Chr(13) & Chr(10))
                End If
            End If


            If (objChassisMasterFacade.IsExist(splittedVal(1).Trim())) Then
                objEndCustomer.RefChassisNumberID = objChassisMasterFacade.Retrieve(splittedVal(1).Trim()).ID
            End If

            If Not splittedVal(2).Trim = "" Then
                Dim splittedDate As String()
                splittedDate = splittedVal(2).Trim().Split("/")
                Try
                    objEndCustomer.FakturDate = New Date(CType(splittedDate(2).Trim, Integer), _
                                CType(splittedDate(1).Trim, Integer), _
                                CType(splittedDate(0).Trim, Integer))
                Catch ex As Exception
                    errorSb.Append("Tanggal Faktur tidak valid" & Chr(13) & Chr(10))
                End Try
            Else
                errorSb.Append("Tanggal Faktur tidak valid" & Chr(13) & Chr(10))
            End If

            objEndCustomer.FakturNumber = splittedVal(3).Trim()

            If splittedVal(4).Trim() = "" Then
                errorSb.Append("Customer Code Kosong" & Chr(13) & Chr(10))
            Else
                Dim objCustomer As Customer = New CustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(splittedVal(27).Trim())
                If objCustomer.ID > 0 Then
                    objCustomer.Code = splittedVal(27).Trim()
                    objCustomer.Name1 = splittedVal(16).Trim()
                    objCustomer.Name2 = splittedVal(17).Trim()
                    objCustomer.Name3 = splittedVal(18).Trim()
                    objCustomer.Alamat = splittedVal(19).Trim()
                    objCustomer.Kelurahan = splittedVal(20).Trim()
                    objCustomer.Kecamatan = splittedVal(21).Trim()
                    objCustomer.PostalCode = splittedVal(22).Trim()
                    Dim _city As City = New CityFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(splittedVal(23).Trim())
                    If Not _city Is Nothing Then
                        If _city.ID > 0 Then
                            objCustomer.City = _city
                        Else
                            errorSb.Append("City tidak boleh kosong" & Chr(13) & Chr(10))
                        End If
                    Else
                        errorSb.Append("City tidak boleh kosong" & Chr(13) & Chr(10))
                    End If
                    objCustomer.PrintRegion = splittedVal(24).Trim()
                    objCustomer.Email = splittedVal(25).Trim()
                    objCustomer.PhoneNo = splittedVal(26).Trim()
                    objCustomer.MarkLoaded()
                    objEndCustomer.Customer = objCustomer

                Else
                    'errorSb.Append("Customer Code tidak ditemukan" & Chr(13) & Chr(10))
                    Dim orgCust As Customer = New Customer
                    orgCust.Code = splittedVal(27).Trim()
                    orgCust.Name1 = splittedVal(16).Trim()
                    orgCust.Name2 = splittedVal(17).Trim()
                    orgCust.Name3 = splittedVal(18).Trim()
                    orgCust.Alamat = splittedVal(19).Trim()
                    orgCust.Kelurahan = splittedVal(20).Trim()
                    orgCust.Kecamatan = splittedVal(21).Trim()
                    orgCust.PostalCode = splittedVal(22).Trim()
                    Dim _city As City = New CityFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(splittedVal(23).Trim())
                    If Not _city Is Nothing Then
                        If _city.ID > 0 Then
                            orgCust.City = _city
                        Else
                            errorSb.Append("City tidak boleh kosong" & Chr(13) & Chr(10))
                        End If
                    Else
                        errorSb.Append("City tidak boleh kosong" & Chr(13) & Chr(10))
                    End If
                    orgCust.PrintRegion = splittedVal(24).Trim()
                    orgCust.Email = splittedVal(25).Trim()
                    orgCust.PhoneNo = splittedVal(26).Trim()
                    objEndCustomer.OriginalCustomer = orgCust
                End If
                Dim _cleansingCust As Customer = New CustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(splittedVal(4).Trim())
                If _cleansingCust.ID > 0 Then
                    objEndCustomer.CleansingCustomerCode = _cleansingCust.ID
                End If

            End If

            If splittedVal(6).Trim() = "" Or splittedVal(6).Trim() = "0" Then
                objEndCustomer.AreaViolationFlag = ""
                objEndCustomer.AreaViolationPaymentMethodID = Nothing
                objEndCustomer.AreaViolationyAmount = Nothing
                objEndCustomer.AreaViolationBankName = Nothing
                objEndCustomer.AreaViolationGyroNumber = Nothing
            Else
                objEndCustomer.AreaViolationFlag = "X"

                If splittedVal(5).Trim().Length > 0 Then
                    Dim objAreaVioPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    Dim objAreaVioPatMeth As PaymentMethod = objAreaVioPayMethFacade.Retrieve(splittedVal(5).Trim())
                    If Not IsNothing(objAreaVioPatMeth) Then
                        objEndCustomer.AreaViolationPaymentMethodID = objAreaVioPatMeth.ID
                    Else
                        errorSb.Append("Tipe Pembayaran(Pelanggaran Wilayah) tidak valid" & Chr(13) & Chr(10))
                    End If
                End If
                Try
                    objEndCustomer.AreaViolationyAmount = CType(splittedVal(6).Trim(), Decimal)
                Catch ex As Exception
                    objEndCustomer.AreaViolationyAmount = 0
                End Try
                objEndCustomer.AreaViolationBankName = splittedVal(7).Trim()
                objEndCustomer.AreaViolationGyroNumber = splittedVal(8).Trim()
            End If

            If splittedVal.Length > 7 Then
                If splittedVal(10).Trim() = "" Or splittedVal(10).Trim() = "0" Then
                    objEndCustomer.PenaltyFlag = ""
                    objEndCustomer.PenaltyPaymentMethodID = Nothing
                    objEndCustomer.PenaltyAmount = Nothing
                    objEndCustomer.PenaltyBankName = Nothing
                    objEndCustomer.PenaltyGyroNumber = Nothing
                Else
                    objEndCustomer.PenaltyFlag = "X"
                    If splittedVal(9).Trim().Length > 0 Then
                        Dim objPenaltyPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        Dim objPenaltyPatMeth As PaymentMethod = objPenaltyPayMethFacade.Retrieve(splittedVal(9).Trim())
                        If Not IsNothing(objPenaltyPatMeth) Then
                            objEndCustomer.PenaltyPaymentMethodID = objPenaltyPatMeth.ID
                        Else
                            errorSb.Append("Tipe Pembayaran(Pembayaran Penalti) tidak valid" & Chr(13) & Chr(10))
                        End If
                    End If
                    Try
                        objEndCustomer.PenaltyAmount = CType(splittedVal(10).Trim(), Decimal)
                    Catch ex As Exception
                        objEndCustomer.PenaltyAmount = 0
                    End Try
                    objEndCustomer.PenaltyBankName = splittedVal(11).Trim()
                    objEndCustomer.PenaltyGyroNumber = splittedVal(12).Trim()
                End If

                If splittedVal(13).Trim() = "" Then
                    objEndCustomer.ReferenceLetterFlag = ""
                    objEndCustomer.ReferenceLetter = Nothing
                Else
                    objEndCustomer.ReferenceLetterFlag = "X"
                    objEndCustomer.ReferenceLetter = splittedVal(13).Trim()
                End If

                If Not splittedVal(14).Trim = "" Then
                    Dim splittedDate As String()
                    splittedDate = splittedVal(14).Trim().Split("/")
                    Try
                        objEndCustomer.PrintedTime = New Date(CType(splittedDate(2).Trim, Integer), _
                                    CType(splittedDate(1).Trim, Integer), _
                                    CType(splittedDate(0).Trim, Integer))
                    Catch ex As Exception
                        errorSb.Append("Tanggal Cetak tidak valid" & Chr(13) & Chr(10))
                    End Try
                Else
                    errorSb.Append("Tanggal Cetak tidak valid" & Chr(13) & Chr(10))
                End If

                Try
                    If Not splittedVal(15).Trim = "" Then
                        Dim tempStrDate As String = splittedVal(15).Trim
                        Dim splittedDateFaktur As String()
                        splittedDateFaktur = tempStrDate.Split("/")
                        Dim tempDate As Date = New Date(CType(splittedDateFaktur(2).Trim, Integer), _
                                                            CType(splittedDateFaktur(1).Trim, Integer), _
                                                            CType(splittedDateFaktur(0).Trim, Integer))
                        objEndCustomer.OpenFakturDate = tempDate
                        objEndCustomer.ConfirmTime = tempDate
                        objEndCustomer.ConfirmBy = "WSM"
                    End If
                Catch ex As Exception
                    'Do Nothing
                End Try
                'kolom 16 dan seterusnya karena CR
                If splittedVal.Length >= 30 Then
                    Dim oCRP As CustomerRequestProfile

                    If splittedVal(28).Trim <> "" Then 'NOTELP
                        Try
                            oCRP = objEndCustomer.Customer.MyCustomerRequest.GetCustomerRequestProfile("NOTELP")
                        Catch ex As Exception
                            oCRP = Nothing
                        End Try
                        If Not IsNothing(oCRP) AndAlso oCRP.ID > 0 Then
                            oCRP.ProfileValue = splittedVal(28).Trim
                            objEndCustomer.Customer.MyCustomerRequest.UpdateCustomerRequestProfile(oCRP)
                        End If
                    Else
                        'start  : uncomment by Donas:20140707, requested by Angga G from Mr. Yoseph
                        'errorSb.Append("No Telepon tidak boleh kosong" & Chr(13) & Chr(10))
                        'end    : uncomment by Donas:20140707, requested by Angga G from Mr. Yoseph
                    End If

                    If splittedVal(29).Trim <> "" Then 'KTP
                        Try
                            oCRP = objEndCustomer.Customer.MyCustomerRequest.GetCustomerRequestProfile("NOKTP")
                        Catch ex As Exception
                            oCRP = Nothing
                        End Try
                        If Not IsNothing(oCRP) AndAlso oCRP.ID > 0 Then
                            oCRP.ProfileValue = splittedVal(29).Trim
                            objEndCustomer.Customer.MyCustomerRequest.UpdateCustomerRequestProfile(oCRP)
                        End If
                    Else
                        errorSb.Append("No KTP/TDP tidak boleh kosong" & Chr(13) & Chr(10))
                    End If
                End If
            End If
            If splittedVal.Length > 32 Then
                Dim sRemark1 As String = splittedVal(30).Trim
                Dim sRemark2 As String = splittedVal(31).Trim
                objEndCustomer.Remark1 = sRemark1
                objEndCustomer.Remark2 = sRemark2
            End If
            objEndCustomer.PrintedBy = "WSM"

            'ValidateTime'
            If splittedVal(32).Trim.Length > 0 AndAlso Not splittedVal(32).Trim.ToString = "00000000" And objEndCustomer.ValidateTime = "1753-01-01 00:00:00.000" Then
                objEndCustomer.ValidateTime = New Date(CType(Left(splittedVal(32).Trim, 4), Integer), _
                                CType(Mid(splittedVal(32).Trim, 5, 2), Integer), _
                                CType(Right(splittedVal(32).Trim, 2), Integer))
                objEndCustomer.ValidateBy = "WSM"
            End If

            If errorSb.Length > 0 Then
                Throw New Exception(errorSb.ToString)
            Else
                If isRevision Then
                    If objRevisionFaktur.ChassisMaster.ChassisNumber = objChassisMaster.ChassisNumber Then
                        objChassisMaster.EndCustomer = objEndCustomer
                        objRevisionFaktur.ChassisMaster = objChassisMaster
                        _ListData.Add(objRevisionFaktur)
                    Else
                        errorSb.Append("Chassis Number tidak sesuai dengan data revisi" & Chr(13) & Chr(10))
                    End If
                Else
                    _ListData.Add(objChassisMaster)
                End If
            End If
        End Sub

        Private Sub ParseEndCustomerOld(ByVal ValParser As String)
            'Dim objEndCustomer As EndCustomer
            'Dim objChassisMaster As ChassisMaster
            'Dim splittedVal As String()
            'Dim errorSb As StringBuilder = New StringBuilder
            'splittedVal = ValParser.Split(";")

            '' COLUMN 0 '
            'Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            'objChassisMaster = objChassisMasterFacade.Retrieve(splittedVal(0).Trim)
            'If Not objChassisMaster Is Nothing Then
            '    objChassisMaster.FakturStatus = CType(EnumChassisMaster.FakturStatus.Selesai, String)
            'Else
            '    objChassisMaster = New ChassisMaster
            '    errorSb.Append("Nomor rangka tidak terdaftar" & Chr(13) & Chr(10))
            'End If

            'If objChassisMaster.EndCustomer Is Nothing Then
            '    objEndCustomer = New EndCustomer
            '    objEndCustomer.MarkLoaded()
            '    objChassisMaster.EndCustomer = objEndCustomer
            'Else
            '    objEndCustomer = objChassisMaster.EndCustomer
            'End If

            '' COLUMN 1 '
            'If (objChassisMasterFacade.IsExist(splittedVal(1).Trim())) Then
            '    objEndCustomer.RefChassisNumberID = objChassisMasterFacade.Retrieve(splittedVal(2).Trim()).ID
            'End If

            '' COLUMN 2 '
            'If Not splittedVal(2).Trim = "" Then
            '    Dim splittedDate As String()
            '    splittedDate = splittedVal(2).Trim().Split("/")
            '    Try
            '        objEndCustomer.FakturDate = New Date(CType(splittedDate(2).Trim, Integer), _
            '                    CType(splittedDate(1).Trim, Integer), _
            '                    CType(splittedDate(0).Trim, Integer))
            '    Catch ex As Exception
            '        errorSb.Append("Tanggal Faktur tidak valid" & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    errorSb.Append("Tanggal Faktur tidak valid" & Chr(13) & Chr(10))
            'End If

            '' COLUMN 3 '
            'objEndCustomer.FakturNumber = splittedVal(3).Trim()

            '' COLUMN 4 '
            'objEndCustomer.Name1 = splittedVal(4).Trim()
            'If splittedVal(4).Trim() = "" Then
            '    errorSb.Append("Nama1 tidak boleh kosong" & Chr(13) & Chr(10))
            'End If

            '' COLUMN 5 '
            'objEndCustomer.Name2 = splittedVal(5).Trim()

            '' COLUMN 6 '
            'objEndCustomer.Name3 = splittedVal(6).Trim()

            '' COLUMN 7 '
            'objEndCustomer.Alamat = splittedVal(7).Trim()
            'If splittedVal(7).Trim() = "" Then
            '    errorSb.Append("Alamat tidak boleh kosong" & Chr(13) & Chr(10))
            'End If

            '' COLUMN 8 '
            'objEndCustomer.Kelurahan = splittedVal(8).Trim()

            '' COLUMN 9 '
            'objEndCustomer.Kecamatan = splittedVal(9).Trim()

            '' COLUMN 10 '
            'objEndCustomer.PostalCode = splittedVal(10).Trim()


            '' COLUMN 11 '
            'Dim objCityFacade As CityFacade = New CityFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            'Dim objCity As City = objCityFacade.Retrieve(splittedVal(11).Trim())
            'If Not objCity Is Nothing Then
            '    objEndCustomer.City = objCity
            'Else
            '    errorSb.Append("Kode kota tidak ditemukan" & Chr(13) & Chr(10))
            'End If

            '' COLUMN 12 '
            'If splittedVal(12).Trim().Length > 0 Then
            '    objEndCustomer.PrintRegion = "X"
            'Else
            '    objEndCustomer.PrintRegion = String.Empty
            'End If

            '' COLUMN 13 '
            'objEndCustomer.Email = splittedVal(13).Trim()

            '' COLUMN 14 '
            'objEndCustomer.Phone = splittedVal(14).Trim()

            '' COLUMN 15 '
            'If Not splittedVal(15).Trim() = "" Then
            '    Dim objPaymentTypeFacade As PaymentTypeFacade = New PaymentTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '    Dim objPaymentType As PaymentType = objPaymentTypeFacade.Retrieve(splittedVal(15).Trim())
            '    If Not objPaymentType Is Nothing Then
            '        objEndCustomer.PaymentType = objPaymentType
            '    Else
            '        errorSb.Append("Cara pembayaran salah" & Chr(13) & Chr(10))
            '    End If
            'End If

            '' COLUMN 16 '
            'If Not splittedVal(16).Trim() = "" Then
            '    Dim objVehicleOwnershipFacade As VehicleOwnershipFacade = New VehicleOwnershipFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '    Dim objVehicleOwnership As VehicleOwnership = objVehicleOwnershipFacade.Retrieve(splittedVal(16).Trim())
            '    If Not objVehicleOwnership Is Nothing Then
            '        objEndCustomer.VehicleOwnership = objVehicleOwnership
            '    Else
            '        errorSb.Append("Kode kepemilikan kendaraan salah" & Chr(13) & Chr(10))
            '    End If
            'End If

            '' COLUMN 17 '
            'If Not splittedVal(17).Trim() = "" Then
            '    Dim objVehiclePurposeFacade As VehiclePurposeFacade = New VehiclePurposeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '    Dim objVehiclePurpose As VehiclePurpose = objVehiclePurposeFacade.Retrieve(splittedVal(17).Trim())
            '    If Not objVehiclePurpose Is Nothing Then
            '        objEndCustomer.VehiclePurpose = objVehiclePurpose
            '    Else
            '        errorSb.Append("Kode kendaraan sebagai salah" & Chr(13) & Chr(10))
            '    End If
            'End If

            '' COLUMN 18
            'If Not splittedVal(18).Trim() = "" Then
            '    Dim objVehicleBodyShapeFacade As VehicleBodyShapeFacade = New VehicleBodyShapeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '    Dim objVehicleBodyShapeCV As VehicleBodyShape = objVehicleBodyShapeFacade.Retrieve(splittedVal(18).Trim(), "CV")
            '    If Not objVehicleBodyShapeCV Is Nothing Then
            '        objEndCustomer.VehicleBodyShape = objVehicleBodyShapeCV
            '    Else
            '        errorSb.Append("Kode body CV salah" & Chr(13) & Chr(10))
            '    End If
            'End If

            '' COLUMN 19 ' tambahin category LCV
            'If Not splittedVal(19).Trim() = "" Then
            '    Dim objVehicleBodyShapeFacade As VehicleBodyShapeFacade = New VehicleBodyShapeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '    Dim objVehicleBodyShapeLCV As VehicleBodyShape = objVehicleBodyShapeFacade.Retrieve(splittedVal(19).Trim(), "LCV")
            '    If Not objVehicleBodyShapeLCV Is Nothing Then
            '        objEndCustomer.VehicleBodyShape = objVehicleBodyShapeLCV
            '    Else
            '        errorSb.Append("Kode body LCV salah" & Chr(13) & Chr(10))
            '    End If
            'End If

            '' COLUMN 20 '
            'If Not splittedVal(20).Trim() = "" Then
            '    Dim objCustomerBusinessFacade As CustomerBusinessFacade = New CustomerBusinessFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '    Dim objCustomerBusiness As CustomerBusiness = objCustomerBusinessFacade.Retrieve(splittedVal(20).Trim())
            '    If Not objCustomerBusiness Is Nothing Then
            '        objEndCustomer.CustomerBusiness = objCustomerBusiness
            '    Else
            '        errorSb.Append("Kode bidang usaha konsumen salah" & Chr(13) & Chr(10))
            '    End If
            'End If

            '' COLUMN 21 '
            'If Not splittedVal(21).Trim() = "" Then
            '    Dim objMainOperationAreaFacade As MainOperationAreaFacade = New MainOperationAreaFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '    Dim objMainOperationArea As MainOperationArea = objMainOperationAreaFacade.Retrieve(splittedVal(21).Trim())
            '    If Not objMainOperationArea Is Nothing Then
            '        objEndCustomer.MainOperationArea = objMainOperationArea
            '    Else
            '        errorSb.Append("Kode daerah operasi salah" & Chr(13) & Chr(10))
            '    End If
            'End If

            '' COLUMN 22 '
            'If Not splittedVal(22).Trim() = "" Then
            '    Dim objOwnerAgeFacade As OwnerAgeFacade = New OwnerAgeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '    Dim objOwnerAge As OwnerAge = objOwnerAgeFacade.Retrieve(splittedVal(22).Trim())
            '    If Not objOwnerAge Is Nothing Then
            '        objEndCustomer.OwnerAge = objOwnerAge
            '    Else
            '        errorSb.Append("Kode usia pemilik salah" & Chr(13) & Chr(10))
            '    End If
            'End If

            '' COLUMN 23 '
            'If Not splittedVal(23).Trim() = "" Then
            '    Dim objMainUsageFacade As MainUsageFacade = New MainUsageFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '    Dim objMainUsage As MainUsage = objMainUsageFacade.Retrieve(splittedVal(23).Trim())
            '    If Not objMainUsage Is Nothing Then
            '        objEndCustomer.MainUsage = objMainUsage
            '    Else
            '        errorSb.Append("Kode pengguanaan utama salah" & Chr(13) & Chr(10))
            '    End If
            'End If

            '' COLUMN 24,25,26,27 '
            '' kalau kolom amount utk area violation ada isi, maka dianggap terjadi area violation
            'If splittedVal(25).Trim() = "" Or splittedVal(25).Trim() = "0" Then
            '    objEndCustomer.AreaViolationFlag = ""
            '    objEndCustomer.AreaViolationPaymentMethodID = Nothing
            '    objEndCustomer.AreaViolationyAmount = Nothing
            '    objEndCustomer.AreaViolationBankName = Nothing
            '    objEndCustomer.AreaViolationGyroNumber = Nothing
            'Else
            '    objEndCustomer.AreaViolationFlag = "X"

            '     If splittedVal(24).Trim().Length > 0 Then
            '        Dim objAreaVioPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '        Dim objAreaVioPatMeth As PaymentMethod = objAreaVioPayMethFacade.Retrieve(splittedVal(24).Trim())
            '        If Not IsNothing(objAreaVioPatMeth) Then
            '            objEndCustomer.AreaViolationPaymentMethodID = objAreaVioPatMeth.ID
            '        Else
            '            errorSb.Append("Tipe Pembayaran(Pelanggaran Wilayah) tidak valid" & Chr(13) & Chr(10))
            '        End If
            '    End If
            '    Try
            '        objEndCustomer.AreaViolationyAmount = CType(splittedVal(25).Trim(), Decimal)
            '    Catch ex As Exception
            '        objEndCustomer.AreaViolationyAmount = 0
            '    End Try
            '    objEndCustomer.AreaViolationBankName = splittedVal(26).Trim()
            '    objEndCustomer.AreaViolationGyroNumber = splittedVal(27).Trim()
            'End If

            '' COLUMN 28,29,30,31 '
            'If splittedVal.Length > 26 Then
            '    If splittedVal(29).Trim() = "" Or splittedVal(29).Trim() = "0" Then
            '        objEndCustomer.PenaltyFlag = ""
            '        objEndCustomer.PenaltyPaymentMethodID = Nothing
            '        objEndCustomer.PenaltyAmount = Nothing
            '        objEndCustomer.PenaltyBankName = Nothing
            '        objEndCustomer.PenaltyGyroNumber = Nothing
            '    Else
            '        objEndCustomer.PenaltyFlag = "X"
            '         If splittedVal(28).Trim().Length > 0 Then
            '            Dim objPenaltyPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '            Dim objPenaltyPatMeth As PaymentMethod = objPenaltyPayMethFacade.Retrieve(splittedVal(28).Trim())
            '            If Not IsNothing(objPenaltyPatMeth) Then
            '                objEndCustomer.PenaltyPaymentMethodID = objPenaltyPatMeth.ID
            '            Else
            '                errorSb.Append("Tipe Pembayaran(Pembayaran Penalti) tidak valid" & Chr(13) & Chr(10))
            '            End If
            '        End If
            '        Try
            '            objEndCustomer.PenaltyAmount = CType(splittedVal(29).Trim(), Decimal)
            '        Catch ex As Exception
            '            objEndCustomer.PenaltyAmount = 0
            '        End Try
            '        objEndCustomer.PenaltyBankName = splittedVal(30).Trim()
            '        objEndCustomer.PenaltyGyroNumber = splittedVal(31).Trim()
            '    End If

            '    ' COLUMN 32 '
            '    If splittedVal(32).Trim() = "" Then
            '        objEndCustomer.ReferenceLetterFlag = ""
            '        objEndCustomer.ReferenceLetter = Nothing
            '    Else
            '        objEndCustomer.ReferenceLetterFlag = "X"
            '        objEndCustomer.ReferenceLetter = splittedVal(32).Trim()
            '    End If

            '    ' COLUMN 33 '
            '    If Not splittedVal(33).Trim = "" Then
            '        Dim splittedDate As String()
            '        splittedDate = splittedVal(33).Trim().Split("/")
            '        Try
            '            objEndCustomer.PrintedTime = New Date(CType(splittedDate(2).Trim, Integer), _
            '                        CType(splittedDate(1).Trim, Integer), _
            '                        CType(splittedDate(0).Trim, Integer))
            '        Catch ex As Exception
            '            errorSb.Append("Tanggal Cetak tidak valid" & Chr(13) & Chr(10))
            '        End Try
            '    Else
            '        errorSb.Append("Tanggal Cetak tidak valid" & Chr(13) & Chr(10))
            '    End If

            '    Try
            '        If Not splittedVal(34).Trim = "" Then
            '            Dim tempStrDate As String = splittedVal(34).Trim
            '            Dim splittedDateFaktur As String()
            '            splittedDateFaktur = tempStrDate.Split("/")
            '            Dim tempDate As Date = New Date(CType(splittedDateFaktur(2).Trim, Integer), _
            '                                                CType(splittedDateFaktur(1).Trim, Integer), _
            '                                                CType(splittedDateFaktur(0).Trim, Integer))
            '            objEndCustomer.OpenFakturDate = tempDate
            '            objEndCustomer.ConfirmTime = tempDate
            '            objEndCustomer.ConfirmBy = "WSM"
            '        End If
            '    Catch ex As Exception
            '        'Do Nothing
            '    End Try

            'End If

            'objEndCustomer.PrintedBy = "WSM"

            'If errorSb.Length > 0 Then
            '    Throw New Exception(errorSb.ToString)
            'Else
            '    _ListChassisMaster.Add(objChassisMaster)
            'End If

        End Sub

        Public Function getDate(ByVal sDate As String) As DateTime
            Dim dtReturnValue As DateTime = Nothing
            If sDate.Trim <> "" Then
                Dim splittedDate As String()
                splittedDate = sDate.Trim().Split("/")
                dtReturnValue = New Date(CType(splittedDate(2).Trim, Integer), _
                            CType(splittedDate(1).Trim, Integer), _
                            CType(splittedDate(0).Trim, Integer))
            End If
            Return dtReturnValue
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If _ListData.Count > 0 Then
                Dim objRevisionFakturFacade As RevisionFakturFacade
                Dim objChassisMasterFacade As ChassisMasterFacade
                For Each item As Object In _ListData
                    If item.GetType() Is GetType(RevisionFaktur) Then
                        Try
                            objRevisionFakturFacade = New RevisionFakturFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM-Faktur2"), Nothing))
                            objRevisionFakturFacade.SynchronizeSAPToDNET(item)
                        Catch ex1 As Exception
                            SysLogParameter.LogErrorToSyslog(ex1.Message.ToString, "Invoice", "InvoiceParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.InvoiceParser, BlockName)
                            Dim e1 As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.ChassisMaster.ChassisNumber & Chr(13) & Chr(10) & ex1.Message)
                            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e1, "Parser Policy")
                        Finally
                            objRevisionFakturFacade = Nothing
                        End Try
                    ElseIf item.GetType() Is GetType(ChassisMaster) Then
                        Try
                            'Try
                            '    objChassisFacade = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM-Faktur1"), Nothing))
                            '    item.FakturStatus = CType(CType(EnumChassisMaster.FakturStatus.Selesai, Short), String)
                            '    objChassisFacade.Update(item)
                            'Catch ex As Exception
                            'End Try
                            objChassisMasterFacade = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM-Faktur2"), Nothing))
                            objChassisMasterFacade.SynchronizeSAPToDNET(item)
                        Catch ex1 As Exception
                            SysLogParameter.LogErrorToSyslog(ex1.Message.ToString, "Invoice", "InvoiceParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.InvoiceParser, BlockName)
                            Dim e1 As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.ChassisNumber & Chr(13) & Chr(10) & ex1.Message)
                            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e1, "Parser Policy")
                        Finally
                            objChassisMasterFacade = Nothing
                        End Try
                    End If
                Next
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function
    End Class
End Namespace

