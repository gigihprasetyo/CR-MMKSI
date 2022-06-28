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
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
#End Region

Namespace KTB.DNet.Parser
    Public Class WSCBBParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _WSCHeaderBBs As ArrayList ' Merupakan WSC Header yg akan ditarik ke UI
        Private _Dealer As KTB.DNet.Domain.Dealer ' Akan diisi dengan kode dealer session
        Private _isErrorExist As Boolean
#End Region

#Region "Protected Methods"
        Private Function IsAllLaborInfoEmpty(ByVal LaborCode As String, ByVal WorkCode As String, ByVal Qty As String) As Boolean
            Return (LaborCode.Trim = "" And WorkCode.Trim = "" And Qty.Trim = "")
        End Function

        Private Function IsEmpty(ByVal strValue As String) As Boolean
            Return (strValue.Trim = "")
        End Function

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Dim ObjStreamReader As StreamReader
            Try
                ObjStreamReader = New StreamReader(fileName, True)
                _WSCHeaderBBs = New ArrayList
                Dim row As String = ObjStreamReader.ReadLine
                Dim sStart As Integer
                Dim nCount As Integer
                Dim sTemp As String
                Dim CurrentWSCHeaderBBCode As String = ""
                Dim objCriteria As CriteriaComposite
                Dim objAgregate As Aggregate

                While row <> "" Or (Not row Is Nothing)
                    Dim objWSCHeaderBB As WSCHeaderBB
                    Dim objWSCDetailBB As WSCDetailBB
                    Dim arrOfSpllitedRow() As String

                    row = row.Trim

                    If row.Length <= 1 Then
                        objWSCHeaderBB = New WSCHeaderBB
                        objWSCHeaderBB.ErrorMessage = "Data tidak valid"
                        _WSCHeaderBBs.Add(objWSCHeaderBB)
                    Else
                        row = row.Replace(""", """, ";")
                        row = row.Replace(""" ,""", ";")
                        row = row.Replace(""",""", ";")
                        row = row.Replace(""",", ";")
                        row = row.Replace(",""", ";")
                        row = row.Replace("""", "")
                        arrOfSpllitedRow = row.Split(";")


                        If arrOfSpllitedRow.Length < 21 Then
                            objWSCHeaderBB = New WSCHeaderBB
                            objWSCHeaderBB.ErrorMessage = "Format Data tidak lengkap, perhatikan tanda (" & """" & ")" 'Data tidak valid" 
                            _WSCHeaderBBs.Add(objWSCHeaderBB)
                        Else
                            If CurrentWSCHeaderBBCode <> (arrOfSpllitedRow(1) & arrOfSpllitedRow(2)) Then
                                CurrentWSCHeaderBBCode = (arrOfSpllitedRow(1) & arrOfSpllitedRow(2))
                                objWSCHeaderBB = New WSCHeaderBB

                                If arrOfSpllitedRow(1) <> _Dealer.DealerCode Then
                                    If arrOfSpllitedRow(1) = "" Then
                                        objWSCHeaderBB.ErrorMessage &= ";Kode dealer tidak boleh kosong" & "<br />"
                                    Else
                                        objWSCHeaderBB.ErrorMessage &= ";Kode dealer tidak cocok" & "<br />"
                                    End If
                                    Dim objDealer As Dealer
                                    objDealer = New Dealer
                                    objDealer.DealerCode = arrOfSpllitedRow(1)
                                    objDealer.MarkLoaded()
                                    objWSCHeaderBB.Dealer = objDealer
                                Else
                                    _Dealer.MarkLoaded()
                                    objWSCHeaderBB.Dealer = _Dealer
                                End If

                                objWSCHeaderBB.ClaimType = arrOfSpllitedRow(0)
                                If arrOfSpllitedRow(0) <> "Z6" Then
                                    objWSCHeaderBB.ErrorMessage &= ";Tipe Claim tidak valid" & "<br />"
                                End If

                                objWSCHeaderBB.ClaimNumber = arrOfSpllitedRow(2)
                                If arrOfSpllitedRow(2) = "" Then
                                    objWSCHeaderBB.ErrorMessage &= ";No. Claim tidak boleh kosong" & "<br />"
                                End If
                                If arrOfSpllitedRow(2).Length <> 6 Then
                                    objWSCHeaderBB.ErrorMessage &= ";No. Claim tidak valid" & "<br />"
                                End If

                                Dim objWSCHeaderBBFacade As WSCHeaderBBFacade = New WSCHeaderBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                'objCriteria = New CriteriaComposite(New Criteria(GetType(WSCHeaderBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                objCriteria = New CriteriaComposite(New Criteria(GetType(WSCHeaderBB), "ClaimNumber", MatchType.Exact, arrOfSpllitedRow(2)))
                                objCriteria.opAnd(New Criteria(GetType(WSCHeaderBB), "Dealer.DealerCode", MatchType.Exact, arrOfSpllitedRow(1)))

                                objAgregate = New Aggregate(GetType(WSCHeaderBB), "ID", AggregateType.Count)

                                If objWSCHeaderBBFacade.RetrieveScalar(objAgregate, objCriteria) > 0 Then
                                    objWSCHeaderBB.ErrorMessage &= ";No. Claim sudah terdaftar" & "<br />"
                                End If

                                If IsInList(_WSCHeaderBBs, objWSCHeaderBB) Then
                                    objWSCHeaderBB.ErrorMessage &= ";Duplikasi No. Claim dalam file" & "<br />"
                                End If

                                objWSCHeaderBB.RefClaimNumber = arrOfSpllitedRow(3)

                                If arrOfSpllitedRow(4) = "" Then
                                    objWSCHeaderBB.ErrorMessage &= ";No. Rangka tidak boleh kosong" & "<br />"
                                    Dim objChassisMasterBB As ChassisMasterBB
                                    objChassisMasterBB = New ChassisMasterBB
                                    objChassisMasterBB.ChassisNumber = arrOfSpllitedRow(4)
                                    objWSCHeaderBB.ChassisMasterBB = objChassisMasterBB
                                Else
                                    Dim objChassisMasterBBFacade As KTB.DNet.BusinessFacade.FinishUnit.ChassisMasterBBFacade
                                    objChassisMasterBBFacade = New KTB.DNet.BusinessFacade.FinishUnit.ChassisMasterBBFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                                    objCriteria = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    objCriteria.opAnd(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, arrOfSpllitedRow(4)))
                                    objCriteria.opAnd(New Criteria(GetType(ChassisMasterBB), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
                                    Dim ListChassisMasterBB As ArrayList
                                    ListChassisMasterBB = objChassisMasterBBFacade.Retrieve(objCriteria)
                                    If ListChassisMasterBB Is Nothing OrElse ListChassisMasterBB.Count < 1 Then
                                        objWSCHeaderBB.ErrorMessage &= ";No. Rangka tidak terdaftar" & "<br />"
                                        Dim objChassisMasterBB As ChassisMasterBB
                                        objChassisMasterBB = New ChassisMasterBB
                                        objChassisMasterBB.ChassisNumber = arrOfSpllitedRow(4)
                                        objChassisMasterBB.MarkLoaded()
                                        objWSCHeaderBB.ChassisMasterBB = objChassisMasterBB
                                    Else
                                        objWSCHeaderBB.ChassisMasterBB = CType(ListChassisMasterBB.Item(0), ChassisMasterBB)
                                        objWSCHeaderBB.ChassisMasterBB.MarkLoaded()
                                        If Not objWSCHeaderBB.ChassisMasterBB.Dealer Is Nothing Then
                                            If objWSCHeaderBB.ChassisMasterBB.Dealer.DealerCode = objWSCHeaderBB.Dealer.DealerCode Then
                                                Dim PDIFacade As PDIFacade = New PDIFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                                crit.opAnd(New Criteria(GetType(PDI), "ChassisMasterBB.ID", MatchType.Exact, objWSCHeaderBB.ChassisMasterBB.ID))
                                                Dim aggr As Aggregate = New Aggregate(GetType(PDI), "ID", AggregateType.Count)
                                                If PDIFacade.RetrieveScalar(crit, aggr) = 0 Then
                                                    objWSCHeaderBB.ErrorMessage &= ";Nomor rangka belum PDI" & "<br />"
                                                End If
                                            End If
                                        End If
                                    End If
                                End If


                                If arrOfSpllitedRow(5) = "" Then
                                    objWSCHeaderBB.ErrorMessage &= ";Tgl proses tidak boleh kosong" & "<br />"
                                Else
                                    Dim isServiceDateValid As Boolean = True
                                    Try
                                        objWSCHeaderBB.ServiceDate = New Date(CType(arrOfSpllitedRow(5).Substring(4, 2), Integer) + 2000, _
                                                CType(arrOfSpllitedRow(5).Substring(2, 2), Integer), _
                                                CType(arrOfSpllitedRow(5).Substring(0, 2), Integer))
                                    Catch ex As Exception
                                        objWSCHeaderBB.ErrorMessage &= "Tgl Claim tidak valid, seharusnya dd/mm/yy" & "<br />"
                                        objWSCHeaderBB.StringServiceDate = arrOfSpllitedRow(5).Substring(0, 2) & "/" & _
                                            arrOfSpllitedRow(5).Substring(2, 2) & "/" & _
                                            CType(CType(arrOfSpllitedRow(5).Substring(4, 2), Integer) + 2000, String)
                                        isServiceDateValid = False
                                    End Try
                                    If isServiceDateValid Then
                                        objWSCHeaderBB.StringServiceDate = String.Format("{0:dd/MM/yyyy}", objWSCHeaderBB.ServiceDate)
                                    End If
                                End If

                                If arrOfSpllitedRow(6).Trim = "" Then
                                    objWSCHeaderBB.ErrorMessage &= ";Jarak tempuh tidak boleh kosong" & "<br />"
                                    objWSCHeaderBB.Miliage = 0
                                Else
                                    Try
                                        objWSCHeaderBB.Miliage = CType(arrOfSpllitedRow(6).Trim, Integer)

                                        'Add by anh, Sept 28,2010 CR by Rna (belum masa naik)
                                        'Add rule :
                                        '1. Untuk WSC yang diajukan dealer dengan kilometer <2001, akan diterima bila sudah melakukan Free Service 1 (FS1)
                                        '2. Untuk WSC yang diajukan dealer dengan kilometer <6001, akan diterima bila sudah melakukan Free Service(FS2)
                                        'Dim objFreeServiceFacade As KTB.DNet.BusinessFacade.Service.FreeServiceFacade
                                        'objFreeServiceFacade = New KTB.DNet.BusinessFacade.Service.FreeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                                        'objCriteria = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                        'objCriteria.opAnd(New Criteria(GetType(FreeService), "ChassisMasterBB.ChassisNumber", MatchType.Exact, arrOfSpllitedRow(4)))

                                        'Dim ListFreeService As ArrayList
                                        'ListFreeService = objFreeServiceFacade.Retrieve(objCriteria)
                                        'Dim isMileAllow As Boolean = False
                                        'Dim strMileMessage As String
                                        'If Not ListFreeService Is Nothing OrElse ListFreeService.Count > 0 Then
                                        '    If objWSCHeaderBB.Miliage < 2001 Then
                                        '        For Each oFS As FreeService In ListFreeService
                                        '            If oFS.FSKind.KindCode = 1 Then
                                        '                isMileAllow = True
                                        '                Exit For
                                        '            End If
                                        '        Next
                                        '        strMileMessage = "FS 1"
                                        '        If isMileAllow = False Then
                                        '            objWSCHeaderBB.ErrorMessage &= ";Jarak tempuh " & objWSCHeaderBB.Miliage.ToString & " belum melakukan " & strMileMessage & "<br />"
                                        '        End If
                                        '    End If
                                        '    If objWSCHeaderBB.Miliage >= 2001 And objWSCHeaderBB.Miliage < 6001 Then
                                        '        For Each oFS As FreeService In ListFreeService
                                        '            If oFS.FSKind.KindCode = 2 Then
                                        '                isMileAllow = True
                                        '                Exit For
                                        '            End If
                                        '        Next
                                        '        strMileMessage = "FS 2"
                                        '        If isMileAllow = False Then
                                        '            objWSCHeaderBB.ErrorMessage &= ";Jarak tempuh " & objWSCHeaderBB.Miliage.ToString & " belum melakukan " & strMileMessage & "<br />"
                                        '        End If
                                        '    End If
                                        'End If

                                        'end rule
                                    Catch ex As Exception
                                        objWSCHeaderBB.ErrorMessage &= ";Jarak tempuh (" & arrOfSpllitedRow(6).Trim & ") tidak valid" & "<br />"
                                    End Try
                                    If objWSCHeaderBB.Miliage < 0 Then
                                        objWSCHeaderBB.ErrorMessage &= ";Jarak tempuh kurang dari 0" & "<br />"
                                    End If
                                End If


                                ' Modified by Anh, August 18, 200
                                ' Requested by Rina A, as Part of CR
                                ' To add validation for labourcode XEE999, confirmed to Tri Septi, that the XEE999 is only have Z2 and Z4as it ClaimtType.
                                ' Start--------------------------------------------------------------

                                'Dim oPQRHeader As PQRHeader = New PQRHeader
                                'Dim objPQRHeaderFacade As KTB.DNet.BusinessFacade.PQRHeaderFacade
                                'objPQRHeaderFacade = New KTB.DNet.BusinessFacade.PQRHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                'oPQRHeader = objPQRHeaderFacade.Retrieve(arrOfSpllitedRow(7).Trim)

                                'If Not (objWSCHeaderBB.ClaimType = "Z4" Or (objWSCHeaderBB.ClaimType = "Z2" And arrOfSpllitedRow(17).Trim = "XEE999")) Then
                                '    If oPQRHeader.PQRNo.Trim() = String.Empty Then
                                '        objWSCHeaderBB.ErrorMessage &= ";Nomor PQR Tidak Ditemukan2" & "<br />"
                                '    End If
                                'End If


                                ' End----------------------------------------------------------------
                                objWSCHeaderBB.PQR = arrOfSpllitedRow(7)
                                objWSCHeaderBB.PQRStatus = arrOfSpllitedRow(8)

                                ' Modified by Ikhsan, 28 Agustus 2008
                                ' Requested by Rina A, as Part of CR
                                ' To add validation for labourcode XEE999, confirmed to Rury, that the XEE999 is only have Z2 as it ClaimtType.
                                ' Start--------------------------------------------------------------

                                If 1 = 2 AndAlso objWSCHeaderBB.ClaimType = "Z2" Then
                                    If arrOfSpllitedRow(9).Trim = "" Then
                                        ' start additional Line -----------------------------
                                        ' Wait for approval
                                        If arrOfSpllitedRow(17).Trim <> "XEE999" Then
                                            'end additional line -----------------------------
                                            objWSCHeaderBB.ErrorMessage &= ";kode a tidak boleh kosong" & "<br />"
                                        End If
                                    End If
                                    If arrOfSpllitedRow(10).Trim = "" Then
                                        ' start additional Line -----------------------------
                                        ' Wait for approval
                                        If arrOfSpllitedRow(17).Trim <> "XEE999" Then
                                            'end additional Line -----------------------------
                                            objWSCHeaderBB.ErrorMessage &= ";Kode B tidak boleh kosong" & "<br />"
                                        End If
                                    End If
                                    If arrOfSpllitedRow(11).Trim = "" Then
                                        ' start additional Line -----------------------------
                                        ' Wait for approval
                                        If arrOfSpllitedRow(17).Trim <> "XEE999" Then
                                            'end additional Line -----------------------------
                                            objWSCHeaderBB.ErrorMessage &= ";Kode C tidak boleh kosong" & "<br />"
                                        End If
                                    End If
                                End If
                                ' End----------------------------------------------------------------

                                '--------------------------------------------'
                                '-- Modified by Agus Pirnadi (22 Feb 2006) --'
                                '--------------------------------------------'
                                '-- If claim type = "Z4" then CodeA, CodeB, and CodeC
                                '-- all must be empty strings, otherwise create error message.
                                '--
                                If 1 = 2 AndAlso objWSCHeaderBB.ClaimType = "Z4" Then
                                    If objWSCHeaderBB.CodeA <> "" Or objWSCHeaderBB.CodeB <> "" Or _
                                       objWSCHeaderBB.CodeC <> "" Then
                                        objWSCHeaderBB.ErrorMessage &= ";Code A,B,C harus kosong"
                                    End If
                                End If
                                '-- end of modification

                                objWSCHeaderBB.CodeA = arrOfSpllitedRow(9)
                                objWSCHeaderBB.CodeB = arrOfSpllitedRow(10)
                                objWSCHeaderBB.CodeC = arrOfSpllitedRow(11)

                                Dim nCounter As Integer
                                For nCounter = 9 To 11
                                    If arrOfSpllitedRow(nCounter) <> "" Then
                                        Dim objPositionWSCFacade As PositionWSCFacade
                                        objPositionWSCFacade = New PositionWSCFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                        objCriteria = New CriteriaComposite( _
                                                        New Criteria(GetType(PositionWSC), "PositionCategory", MatchType.Exact, Chr(56 + nCounter)))
                                        objCriteria.opAnd(New Criteria(GetType(PositionWSC), "PositionCode", MatchType.Exact, arrOfSpllitedRow(nCounter)))
                                        objAgregate = New Aggregate(GetType(PositionWSC), "ID", AggregateType.Count)
                                        If (objPositionWSCFacade.RetrieveScalar(objAgregate, objCriteria) < 1) Then
                                            objWSCHeaderBB.ErrorMessage &= ";Kode " & arrOfSpllitedRow(nCounter) & " tidak terdaftar di kategori " & Chr(56 + nCounter) & "<br />"
                                        End If
                                    End If
                                Next


                                objWSCHeaderBB.Description = arrOfSpllitedRow(12)
                                objWSCHeaderBB.EvidencePhoto = arrOfSpllitedRow(13).ToUpper
                                objWSCHeaderBB.EvidenceInvoice = arrOfSpllitedRow(14).ToUpper
                                objWSCHeaderBB.EvidenceDmgPart = arrOfSpllitedRow(15).ToUpper
                                objWSCHeaderBB.ClaimStatus = ""
                                objWSCHeaderBB.Status = CStr(enumStatusWSC.Status.Baru)

                                _WSCHeaderBBs.Add(objWSCHeaderBB)
                            End If
                            objWSCDetailBB = New WSCDetailBB
                            objWSCDetailBB.WSCHeaderBB = objWSCHeaderBB
                            If arrOfSpllitedRow(16) = "L" Then
                                objWSCDetailBB.WSCType = "L"

                                If IsEmpty(arrOfSpllitedRow(17)) Then
                                    objWSCHeaderBB.ErrorMessage &= ";Kode Posisi, Kode Kerja dan Unit tidak boleh kosong" & "<br />"
                                End If
                                If IsEmpty(arrOfSpllitedRow(18)) Then
                                    objWSCHeaderBB.ErrorMessage &= ";Kode Kerja tidak boleh kosong" & "<br />"
                                End If
                                If IsEmpty(arrOfSpllitedRow(19)) Then
                                    objWSCHeaderBB.ErrorMessage &= ";Unit tidak boleh kosong" & "<br />"
                                End If

                                Dim objLaborMasterFacade As New LaborMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                objCriteria = New CriteriaComposite( _
                                                New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, arrOfSpllitedRow(17)))
                                objCriteria.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, arrOfSpllitedRow(18)))
                                objCriteria.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", MatchType.Exact, objWSCHeaderBB.ChassisMasterBB.VechileColor.VechileType.ID))
                                'objCriteria.opAnd(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                                Dim ListLaborMaster As ArrayList
                                ListLaborMaster = objLaborMasterFacade.Retrieve(objCriteria)

                                If ListLaborMaster Is Nothing OrElse ListLaborMaster.Count < 1 Then
                                    If arrOfSpllitedRow(18) <> "99" Then
                                        'objWSCHeaderBB.ErrorMessage &= ";Kode Kerja " & arrOfSpllitedRow(18) & " tidak terdaftar untuk Kode Posisi " & arrOfSpllitedRow(17) & "<br />"
                                        objWSCHeaderBB.ErrorMessage &= ";Kode Posisi " & arrOfSpllitedRow(17) & " Kode kerja " & arrOfSpllitedRow(18) & " dan Tipe " & objWSCHeaderBB.ChassisMasterBB.VechileColor.VechileType.VechileTypeCode & " tidak terdaftar" & "<br />"
                                    Else
                                        If arrOfSpllitedRow(17) <> "XEE999" Then
                                            objWSCHeaderBB.ErrorMessage &= ";Kode Posisi " & arrOfSpllitedRow(17) & " Kode kerja " & arrOfSpllitedRow(18) & " dan Tipe " & objWSCHeaderBB.ChassisMasterBB.VechileColor.VechileType.VechileTypeCode & " tidak terdaftar" & "<br />"
                                        Else
                                            Dim objLaborMaster As LaborMaster
                                            Dim iResult As Integer = 0
                                            objLaborMaster = New LaborMaster
                                            objLaborMaster.LaborCode = arrOfSpllitedRow(17)
                                            objLaborMaster.WorkCode = arrOfSpllitedRow(18)
                                            objLaborMaster.VechileType = objWSCHeaderBB.ChassisMasterBB.VechileColor.VechileType
                                            iResult = objLaborMasterFacade.Insert(objLaborMaster)
                                            If iResult > 0 Then
                                                Dim newObjLaborMaster As New LaborMaster
                                                newObjLaborMaster = objLaborMasterFacade.Retrieve(iResult)
                                                objWSCDetailBB.LaborMaster = newObjLaborMaster
                                            Else
                                                objWSCHeaderBB.ErrorMessage &= ";Kode Kerja " & arrOfSpllitedRow(18) & " tidak terdaftar untuk Kode Posisi " & arrOfSpllitedRow(17) & "<br />"
                                            End If
                                        End If
                                    End If
                                Else
                                    'If 1 = 2 AndAlso arrOfSpllitedRow(18) <> "99" Then
                                    '    objWSCHeaderBB.ErrorMessage &= ";Kode Posisi " & arrOfSpllitedRow(17) & " Kode kerja " & arrOfSpllitedRow(18) & " dan Tipe " & objWSCHeaderBB.ChassisMasterBB.VechileColor.VechileType.VechileTypeCode & " tidak terdaftar" & "<br />"
                                    'ElseIf 1 = 2 Then
                                    '    If arrOfSpllitedRow(17) <> "XEE999" Then
                                    '        objWSCHeaderBB.ErrorMessage &= ";Kode Posisi " & arrOfSpllitedRow(17) & " Kode kerja " & arrOfSpllitedRow(18) & " dan Tipe " & objWSCHeaderBB.ChassisMasterBB.VechileColor.VechileType.VechileTypeCode & " tidak terdaftar" & "<br />"
                                    '    End If
                                    'End If
                                    Dim ExistingLabor As LaborMaster = CType(ListLaborMaster.Item(0), LaborMaster)
                                    objWSCDetailBB.LaborMaster = ExistingLabor
                                    'Tambahan Enhancement
                                    If ExistingLabor.RowStatus <> 0 Then
                                        objWSCHeaderBB.ErrorMessage &= ";Kode Posisi " & arrOfSpllitedRow(17) & " " & arrOfSpllitedRow(18) & "  tidak Aktif" & "<br />"
                                    End If
                                End If

                                If arrOfSpllitedRow(19) = "" Then
                                    objWSCDetailBB.Quantity = 0
                                    If IsEmpty(arrOfSpllitedRow(19)) Then
                                        objWSCHeaderBB.ErrorMessage &= ";Unit pada Kode Posisi " & arrOfSpllitedRow(17) & " Kode Kerja " & arrOfSpllitedRow(18) & " tidak boleh kosong" & "<br />"
                                    End If
                                Else
                                    Try
                                        If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "," Then
                                            objWSCDetailBB.Quantity = CType(arrOfSpllitedRow(19), Decimal)
                                        Else
                                            Dim temp As String
                                            temp = arrOfSpllitedRow(19)
                                            temp = temp.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)
                                            objWSCDetailBB.Quantity = CType(temp, Decimal)
                                        End If
                                    Catch ex As Exception
                                        objWSCHeaderBB.ErrorMessage &= ";Qty tidak berupa angka" & "<br />"
                                    End Try

                                End If

                                Try
                                    arrOfSpllitedRow(20) = arrOfSpllitedRow(20).Trim
                                    If arrOfSpllitedRow(20) = "" Then
                                        If arrOfSpllitedRow(18) = "99" Then
                                            objWSCHeaderBB.ErrorMessage &= ";Labor Amount tidak boleh kosong" & "<br />"
                                        Else
                                            objWSCDetailBB.PartPrice = 0
                                        End If

                                    Else
                                        objWSCDetailBB.PartPrice = CType(arrOfSpllitedRow(20), Decimal)
                                    End If
                                Catch ex As Exception
                                    objWSCHeaderBB.ErrorMessage &= ";Labor Amount tidak berupa angka" & "<br />"
                                End Try

                                'Enhancement
                                'If objWSCHeaderBB.ClaimType = "Z2" AndAlso objWSCDetailBB.LaborMaster.LaborCode.Substring(0, 2).ToUpper <> "XE" Then
                                If 1 = 2 AndAlso objWSCHeaderBB.ClaimType = "Z2" AndAlso arrOfSpllitedRow(17).Substring(0, 2).ToUpper <> "XE" Then
                                    Dim KodePostionWSCFacade As New KodePostionWSCFacade(System.Threading.Thread.CurrentPrincipal)
                                    Dim objCriteriaA As New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "Code", MatchType.Exact, arrOfSpllitedRow(17).Substring(0, 2)))
                                    objCriteriaA.opAnd(New Criteria(GetType(KodePostionWSC), "PostionCode", MatchType.Exact, objWSCHeaderBB.CodeA))
                                    objCriteriaA.opAnd(New Criteria(GetType(KodePostionWSC), "CategoryCode", MatchType.Exact, "A"))
                                    objCriteriaA.opAnd(New Criteria(GetType(KodePostionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    Dim arlA As ArrayList = KodePostionWSCFacade.Retrieve(objCriteriaA)
                                    If arlA.Count = 0 Then
                                        objWSCHeaderBB.ErrorMessage &= ";Kode A " & objWSCHeaderBB.CodeA & " tidak valid untuk posisi " & arrOfSpllitedRow(17).Substring(0, 2) & "<br />"
                                    End If
                                    Dim objCriteriaB As New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "Code", MatchType.Exact, arrOfSpllitedRow(17).Substring(0, 2)))
                                    objCriteriaB.opAnd(New Criteria(GetType(KodePostionWSC), "PostionCode", MatchType.Exact, objWSCHeaderBB.CodeB))
                                    objCriteriaB.opAnd(New Criteria(GetType(KodePostionWSC), "CategoryCode", MatchType.Exact, "B"))
                                    objCriteriaB.opAnd(New Criteria(GetType(KodePostionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    Dim arlB As ArrayList = KodePostionWSCFacade.Retrieve(objCriteriaB)
                                    If arlB.Count = 0 Then
                                        objWSCHeaderBB.ErrorMessage &= ";Kode B " & objWSCHeaderBB.CodeB & " tidak valid untuk posisi " & arrOfSpllitedRow(17).Substring(0, 2) & "<br />"
                                    End If
                                End If
                            ElseIf arrOfSpllitedRow(16) = "P" Then
                                objWSCDetailBB.WSCType = "P"
                                Dim objSparePartMasterFacade As SparePartMasterFacade
                                objSparePartMasterFacade = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                objCriteria = New CriteriaComposite( _
                                                New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, arrOfSpllitedRow(17)))

                                Dim ListSparePartMaster As ArrayList
                                ListSparePartMaster = objSparePartMasterFacade.Retrieve(objCriteria)
                                If ListSparePartMaster Is Nothing Or ListSparePartMaster.Count < 1 Then
                                    objWSCHeaderBB.ErrorMessage &= ";Kode Part " & arrOfSpllitedRow(17) & " tidak terdaftar" & "<br />"
                                    Dim objSparePartMaster As SparePartMaster
                                    objSparePartMaster = New SparePartMaster
                                    objSparePartMaster.PartNumber = arrOfSpllitedRow(17)

                                    objWSCDetailBB.SparePartMaster = objSparePartMaster
                                Else
                                    objWSCDetailBB.SparePartMaster = CType(ListSparePartMaster.Item(0), SparePartMaster)
                                End If
                                'if partnumber = "NPN7" , amount must be > 0
                                If arrOfSpllitedRow(17).Trim.ToUpper = "NPN7" And (arrOfSpllitedRow(19).Trim = "0" Or arrOfSpllitedRow(19).Trim = "") Then
                                    objWSCHeaderBB.ErrorMessage &= ";Kode Part NPN7 harus ada amount" & "<br />"
                                End If
                                Try
                                    arrOfSpllitedRow(18) = arrOfSpllitedRow(18).Trim
                                    If arrOfSpllitedRow(18) = "" Then
                                        objWSCDetailBB.Quantity = 0
                                    Else
                                        If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "," Then
                                            objWSCDetailBB.Quantity = CType(arrOfSpllitedRow(18), Decimal)
                                        Else
                                            Dim temp As String
                                            temp = arrOfSpllitedRow(18)
                                            temp = temp.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)
                                            objWSCDetailBB.Quantity = CType(temp, Decimal)
                                        End If
                                    End If
                                Catch ex As Exception
                                    objWSCHeaderBB.ErrorMessage &= ";Qty tidak berupa angka" & "<br />"
                                End Try
                                Try
                                    arrOfSpllitedRow(19) = arrOfSpllitedRow(19).Trim
                                    If arrOfSpllitedRow(19) = "" Then
                                        objWSCDetailBB.PartPrice = 0
                                    Else
                                        objWSCDetailBB.PartPrice = CType(arrOfSpllitedRow(19), Decimal)
                                    End If
                                Catch ex As Exception
                                    objWSCHeaderBB.ErrorMessage &= ";Labor Amount tidak berupa angka" & "<br />"
                                End Try

                                'penambahan flag main part
                                Try
                                    arrOfSpllitedRow(21) = arrOfSpllitedRow(21).Trim
                                    If arrOfSpllitedRow(21) = "X" Or arrOfSpllitedRow(21) = "x" Then
                                        objWSCDetailBB.MainPart = 1
                                    Else
                                        objWSCDetailBB.MainPart = 0
                                    End If
                                Catch ex As Exception
                                    objWSCHeaderBB.ErrorMessage &= ";Format data tidak lengkap" & "<br />"
                                End Try
                            Else
                                objWSCHeaderBB.ErrorMessage &= ";Tipe Detail bukan (L/P)" & "<br />"
                            End If
                            objWSCHeaderBB.wSCDetailBBs.Add(objWSCDetailBB)
                        End If
                    End If

                    If _isErrorExist = False And objWSCHeaderBB.ErrorMessage <> "" Then
                        _isErrorExist = True
                    End If

                    row = ObjStreamReader.ReadLine
                End While
                'anh 20111110
                For Each _WSCHeaderBB As WSCHeaderBB In _WSCHeaderBBs
                    If _WSCHeaderBB.ClaimType = "Z2" Then
                        Dim iMainPart As Integer = 0
                        Dim errMainPart As String = ""
                        For Each _WSCDetailBB As WSCDetailBB In _WSCHeaderBB.wSCDetailBBs
                            If _WSCDetailBB.WSCType = "L" Then
                                If Not _WSCDetailBB.LaborMaster Is Nothing Then
                                    If _WSCDetailBB.LaborMaster.LaborCode = "XEE999" Then
                                        Dim is110000Exist As Boolean = False
                                        For Each _WSCDetailBB2 As WSCDetailBB In _WSCHeaderBB.wSCDetailBBs
                                            If _WSCDetailBB2.WSCType = "L" Then
                                                If Not _WSCDetailBB2.LaborMaster Is Nothing Then
                                                    If _WSCDetailBB2.LaborMaster.LaborCode = "110000" Then
                                                        is110000Exist = True
                                                        Exit For
                                                    End If
                                                End If
                                            End If
                                        Next
                                        If is110000Exist Then
                                            iMainPart = iMainPart
                                        Else
                                            iMainPart = 1
                                            Exit For
                                        End If
                                    ElseIf _WSCDetailBB.LaborMaster.LaborCode = "110000" Then
                                        iMainPart = iMainPart
                                    End If
                                End If
                            ElseIf _WSCDetailBB.WSCType = "P" Then
                                If _WSCDetailBB.MainPart = 1 Then
                                    iMainPart = iMainPart + 1
                                End If
                            End If
                            'If _WSCDetailBB.WSCType = "P" Then
                            '    If _WSCDetailBB.MainPart = 1 Then
                            '        iMainPart = iMainPart + 1
                            '    End If
                            'End If
                        Next

                        If iMainPart = 0 Then
                            Dim isPartExist As Boolean = False
                            For Each det As WSCDetailBB In _WSCHeaderBB.wSCDetailBBs
                                If det.WSCType = "P" Then
                                    isPartExist = True
                                End If
                            Next
                            If isPartExist Then
                                errMainPart = ";Tidak ada Main Part yang dipilih" & "<br />"
                            End If
                        ElseIf iMainPart > 1 Then
                            errMainPart = ";Main Part lebih dari 1" & "<br />"
                        End If
                        _WSCHeaderBB.ErrorMessage = _WSCHeaderBB.ErrorMessage & errMainPart
                        If _isErrorExist = False And _WSCHeaderBB.ErrorMessage <> "" Then
                            _isErrorExist = True
                        End If
                    End If
                Next
                'end anh 20111110
            Catch ex As Exception
                _isErrorExist = True
            Finally
                ObjStreamReader.Close()
                ObjStreamReader = Nothing
            End Try

            Return _WSCHeaderBBs
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Return -1
        End Function

#End Region

        Private Function IsInList(ByVal List As ArrayList, ByVal checkVal As WSCHeaderBB) As Boolean
            Dim bReturnValue As Boolean = False
            If List.Count > 0 Then
                For Each item As WSCHeaderBB In List
                    If item.ClaimNumber = checkVal.ClaimNumber And item.Dealer.DealerCode = checkVal.Dealer.DealerCode Then
                        bReturnValue = True
                        Exit For
                    End If
                Next
            End If
            Return bReturnValue
        End Function

#Region "Public Properties"

        Public ReadOnly Property IsErrorExist() As Boolean
            Get
                Return _isErrorExist
            End Get
        End Property

        Public Property Dealer() As Dealer
            Get
                Return _Dealer
            End Get
            Set(ByVal Value As Dealer)
                _Dealer = Value
            End Set
        End Property

        Public ReadOnly Property WSCHeaderBBs() As ArrayList
            Get
                Return _WSCHeaderBBs
            End Get
        End Property

#End Region

    End Class
End Namespace