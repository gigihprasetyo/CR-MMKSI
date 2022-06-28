#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class FreeServiceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_FreeServiceMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager

        'Start  :CR-Remove for Type=TU00
        'Dim objType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TU00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}
        Private _restrictedType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}
        'End    :CR-Remove for Type=TU00

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_FreeServiceMapper = MapperFactory.GetInstance().GetMapper(GetType(FreeService).ToString)
            Me.objTransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FreeService
            Return CType(m_FreeServiceMapper.Retrieve(ID), FreeService)
        End Function
        Public Function Retrieve(ByVal ID As String) As FreeService
            Return CType(m_FreeServiceMapper.Retrieve(Convert.ToInt32(ID)), FreeService)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FreeServiceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FreeServiceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FreeServiceMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FreeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FreeServiceMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FreeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FreeServiceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FreeService As ArrayList = m_FreeServiceMapper.RetrieveByCriteria(criterias)
            Return _FreeService
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FreeServiceColl As ArrayList = m_FreeServiceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FreeServiceColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FreeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FreeServiceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FreeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim FreeServiceColl As ArrayList = m_FreeServiceMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return FreeServiceColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColl As SortCollection) As ArrayList
            Dim FreeServiceColl As ArrayList = m_FreeServiceMapper.RetrieveByCriteria(criterias, sortColl)
            Return FreeServiceColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FreeServiceColl As ArrayList = m_FreeServiceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FreeServiceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FreeService), columnName, matchOperator, columnValue))
            Dim FreeServiceColl As ArrayList = m_FreeServiceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FreeServiceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FreeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), columnName, matchOperator, columnValue))

            Return m_FreeServiceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As FreeService) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_FreeServiceMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As FreeService) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FreeServiceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        'Public Function IsTypeAllowed(ByVal oFS As FreeService) As Boolean
        '    Dim i As Integer

        '    For i = 0 To _restrictedType.Length - 1
        '        If oFS.FSKind.KindCode.Trim.ToLower = _restrictedType(i).Trim.ToLower Then
        '            Return False
        '        End If
        '    Next
        '    Return True
        'End Function



        Public Function UpdateFSCollection(ByVal arrFS As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim flagGagal As Integer = 0
            'coding sementara untuk type promotion
            'Start  :CR-Remove for Type=TU00
            'Dim objType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TU00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}
            Dim objType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}
            'End    :CR-Remove for Type=TU00
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrFS.Count > 0 Then
                        For Each objFS As FreeService In arrFS

                            ''coding sementara untuk cari type
                            'Dim flagAda As Integer = 0
                            'Dim flagOK As Integer = 0
                            'For i As Integer = 0 To objType.Length - 1
                            '    If objType(i) = objFS.ChassisMaster.VechileColor.VechileType.VechileTypeCode Then
                            '        flagAda = 1
                            '        Exit For
                            '    End If
                            'Next

                            'If flagAda = 1 Then
                            '    If objFS.FSKind.ID = 3 Or objFS.FSKind.ID = 4 Or objFS.FSKind.ID = 5 Then
                            '        If objFS.ReleaseDate.ToString("yyyyMMdd") > "20090219" Then
                            '            flagOK = 1
                            '            flagGagal = 1
                            '        End If

                            '    End If
                            '    If objFS.FSKind.ID = 6 Then
                            '        'Check FeDescription in (FE7*,FE8*)
                            '        'GetVehicleType
                            '        Dim theCultureInfo As IFormatProvider = New System.Globalization.CultureInfo("id-ID", True)

                            '        If objFS.Dealer.DealerCode = "100016" Then
                            '            Dim _strVehicleType As String = objFS.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper()
                            '            If _strVehicleType = "FE7" Or _strVehicleType = "FE8" Then
                            '                Dim _dateFrom As DateTime = DateTime.ParseExact("01/02/2010", "dd/MM/yyyy", theCultureInfo)
                            '                Dim _dateTo As DateTime = DateTime.ParseExact("30/06/2010", "dd/MM/yyyy", theCultureInfo)

                            '                If (objFS.ChassisMaster.EndCustomer.OpenFakturDate >= _dateFrom And objFS.ChassisMaster.EndCustomer.OpenFakturDate <= _dateTo) Then
                            '                    flagOK = 0
                            '                    flagGagal = 0
                            '                Else
                            '                    flagOK = 1
                            '                    flagGagal = 1
                            '                End If
                            '            Else
                            '                flagOK = 1
                            '                flagGagal = 1
                            '            End If
                            '        Else
                            '            flagOK = 1
                            '            flagGagal = 1
                            '        End If
                            '    End If
                            'End If

                            'If flagOK = 0 Then
                            '    'coding asli
                            '    If objFS.Status = CType(EnumFSStatus.FSStatus.Proses, String) Then
                            '        objTransactionManager.AddUpdate(objFS, m_userPrincipal.Identity.Name)
                            '    End If

                            'End If
                            If objFS.Status = CType(EnumFSStatus.FSStatus.Proses, String) Then
                                objTransactionManager.AddUpdate(objFS, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If
                    objTransactionManager.PerformTransaction()
                    returnValue = flagGagal

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function


        Public Sub Delete(ByVal objDomain As FreeService)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FreeServiceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As FreeService)
            Try
                m_FreeServiceMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal sID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "ID", MatchType.Exact, sID))
            Dim agg As Aggregate = New Aggregate(GetType(FreeService), "ID", AggregateType.Count)

            Return CType(m_FreeServiceMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function IsAllowFreeService(ByVal objFreeService As FreeService) As Boolean
            If ChassisException(True, objFreeService) = True _
                And IsAllowFSCampaign(objFreeService) = True Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function VehicleTypeException(ByVal objFreeService As FreeService) As Boolean
            Dim objType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}
            'End    :CR - Temporary allowing TU00 :Rina
            Dim isAllow As Boolean = True
            For i As Integer = 0 To objType.Length - 1
                If objFreeService.ChassisMaster.VechileColor.VechileType.VechileTypeCode = objType(i) And (objFreeService.FSKind.KindCode = "3" Or objFreeService.FSKind.KindCode = "4" Or objFreeService.FSKind.KindCode = "5") Then
                    isAllow = False
                    'MessageBox.Show("Simpan gagal, Chassis " & objFreeService.ChassisMaster.ChassisNumber & " tidak mendapat kupon Free Service")
                    Exit For
                End If
            Next
            Return isAllow
        End Function

        Public Function ChassisException(ByVal IsAllowInsert As Boolean, ByRef objFreeService As FreeService) As Boolean
            'Start  :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
            If IsAllowInsert = True _
            AndAlso (objFreeService.FSKind.KindCode = "3" _
            OrElse objFreeService.FSKind.KindCode = "4" _
            OrElse objFreeService.FSKind.KindCode = "5" _
            OrElse objFreeService.FSKind.KindCode = "6" _
            OrElse objFreeService.FSKind.KindCode = "7") Then
                Dim sForbiddenCMs() As String = {"MHMFE71P1AK018514", "MHMFE73P2AK014642", "MHMFE73P2AK014643", "MHMFE73P2AK014715", "MHMFE73P2AK014760"}
                For i As Integer = 0 To sForbiddenCMs.Length - 1
                    If objFreeService.ChassisMaster.ChassisNumber.Trim.ToUpper = sForbiddenCMs(i).Trim.ToUpper Then
                        IsAllowInsert = False
                        'MessageBox.Show("Simpan gagal, Chassis " & objFreeService.ChassisMaster.ChassisNumber & " tidak mendapat kupon Free Service")
                        Exit For
                    End If
                Next
            End If
            Return IsAllowInsert
            'End    :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
        End Function

        Public Function IsAllowFSCampaign(ByVal objFreeService As FreeService) As Boolean
            Dim vReturn As Boolean = False

            ''Additional bypas

            Dim ObjFsChassisCampaign As FSChassisCampaign = New FSChassisCampaign

            Dim ObjIsByPass As Boolean = False

            Dim criteriasFsChassisCampaign As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasFsChassisCampaign.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "ChassisMaster.ChassisNumber", MatchType.Exact, objFreeService.ChassisMaster.ChassisNumber))
            criteriasFsChassisCampaign.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "FSKind.ID", MatchType.Exact, objFreeService.FSKind.ID))

            Dim arrFsChassisCampaign As ArrayList = New FSChassisCampaignFacade(m_userPrincipal).Retrieve(criteriasFsChassisCampaign)
            ObjFsChassisCampaign.IsAllow = False
            If Not IsNothing(arrFsChassisCampaign) AndAlso arrFsChassisCampaign.Count > 0 Then
                ObjIsByPass = True
                ObjFsChassisCampaign = CType(arrFsChassisCampaign(0), FSChassisCampaign)
            End If

            If ObjIsByPass Then
                Return ObjFsChassisCampaign.IsAllow
            End If

            ''End of Bypass modul


            Dim arlFSCampaign As ArrayList = New ArrayList
            Dim objFSCampaignFacade As FSCampaignFacade = New FSCampaignFacade(m_userPrincipal)
            arlFSCampaign = objFSCampaignFacade.RetrieveFSCampaign()
            If arlFSCampaign.Count > 0 Then

                For Each objFSCampaign As FSCampaign In arlFSCampaign
                    'Dealer checking
                    Dim bDealer As Boolean = True
                    If objFSCampaign.DealerChecked = True Then
                        bDealer = False
                        For Each objFSCampaignDealer As FSCampaignDealer In objFSCampaign.FSCampaignDealers
                            If objFSCampaignDealer.DealerCode = objFreeService.Dealer.DealerCode Then
                                bDealer = True
                            End If
                        Next
                    End If

                    'FSKind checking
                    Dim bFSKind As Boolean = True
                    If objFSCampaign.FSTypeChecked = True Then
                        bFSKind = False
                        For Each objFSCampaignKind As FSCampaignKind In objFSCampaign.FSCampaignKinds
                            If objFSCampaignKind.FSKind.KindCode = objFreeService.FSKind.KindCode Then
                                bFSKind = True
                            End If
                        Next
                    End If

                    'VehicleType checking
                    Dim bVehicle As Boolean = True
                    If objFSCampaign.VehicleTypeChecked = True Then
                        bVehicle = False
                        For Each objFSCampaignVehicle As FSCampaignVehicle In objFSCampaign.FSCampaignVehicles
                            If objFSCampaignVehicle.VechileType.ID = objFreeService.ChassisMaster.VechileColor.VechileType.ID Then
                                bVehicle = True
                            End If
                        Next
                    End If

                    ''Faktur Validation checking
                    'Dim bFaktur As Boolean = True
                    'If objFSCampaign.FakturDateChecked = True Then
                    '    bFaktur = False
                    '    If Not IsNothing(objFreeService.ChassisMaster.EndCustomer) Then
                    '        If objFSCampaign.DateFrom <= objFreeService.ChassisMaster.EndCustomer.ValidateTime _
                    '                                   And objFSCampaign.DateTo >= objFreeService.ChassisMaster.EndCustomer.ValidateTime Then
                    '            bFaktur = True
                    '        End If
                    '    Else
                    '        bFaktur = True
                    '    End If
                    'End If

                    'Req by Doni 18 Feb 2022
                    Dim bPKTDate As Boolean = True
                    If objFSCampaign.PKTDateChecked = True Then
                        bPKTDate = False
                        If Not IsNothing(objFreeService.ChassisMaster.ChassisMasterPKT) Then
                            If objFSCampaign.PKTDateFrom <= objFreeService.ChassisMaster.ChassisMasterPKT.PKTDate _
                                And objFSCampaign.PKTDateTo >= objFreeService.ChassisMaster.ChassisMasterPKT.PKTDate Then
                                bPKTDate = True
                            End If
                        Else
                            bPKTDate = True
                        End If
                    End If

                    'Dim bFaktur As Boolean = True
                    'If objFSCampaign.FakturDateChecked = True Then
                    '    bFaktur = False
                    '    If Not IsNothing(objFreeService.ChassisMaster.EndCustomer) Then
                    '        If objFSCampaign.DateFrom <= objFreeService.ChassisMaster.EndCustomer.ValidateTime _
                    '                                   And objFSCampaign.DateTo >= objFreeService.ChassisMaster.EndCustomer.ValidateTime Then
                    '            bFaktur = True
                    '        End If
                    '    Else
                    '        bFaktur = True
                    '    End If
                    'End If

                    'Combine value above
                    'If bDealer And bFSKind And bVehicle And bFaktur Then
                    If bDealer And bFSKind And bVehicle And bPKTDate Then
                        vReturn = True
                        Exit For
                    End If
                Next
            End If
            Return vReturn
        End Function

        Private Function retriveSP(ByVal listPar As List(Of Object)) As ArrayList
            Dim strChassisnumber As String = ""
            Dim strQuery As String
            Try
                strQuery = "Exec up_RetrieveFreeService_Service_Reminder "
                With listPar
                    If Not String.IsNullOrEmpty(.Item(0)) Then 'Chassisnumber
                        strQuery = strQuery + " .Item(0) ,"
                    Else
                        strQuery = strQuery + " '' ,"
                    End If

                    If Not String.IsNullOrEmpty(.Item(1)) Then 'openfakturdate from
                        strQuery = strQuery + " .Item(1) ,"
                    End If
                End With
            Catch ex As Exception

            End Try
            Return New ArrayList
        End Function

        Public Function RetrieveMSP(ByVal fs As FreeService) As DataSet
            Dim strSQL As String = "Select [mx].RegNumber " _
                                   & "FROM [dbo].[FreeService] [a] " _
                                   & "INNER JOIN [dbo].[ChassisMaster] [cm] ON [cm].[ID] = [a].[ChassisMasterID] " _
                                   & "INNER JOIN [dbo].[MSPExMappingtoFSKind] [fk] ON [a].[FSKindID] = [fk].[FSKindID] " _
                                   & "INNER JOIN [dbo].[FSKind] [fks] ON [fks].[ID] = [a].[FSKindID] " _
                                   & "OUTER APPLY " _
                                   & "( " _
                                   & "SELECT TOP 1 " _
                                   & "[mx].[RegNumber], [mx].[CreatedTime] [ValidFrom], [mx].[ValidDateTo] " _
                                   & "FROM [dbo].[MSPExRegistration] [mx] " _
                                   & "WHERE 1 = 1 " _
                                   & "AND [mx].[RowStatus] = 0 " _
                                   & "AND [mx].[ChassisMasterID] = [a].[ChassisMasterID] " _
                                   & "AND [a].[ServiceDate] BETWEEN [mx].[CreatedTime] AND [mx].[ValidDateTo] " _
                                   & "order by id desc " _
                                   & ") [mx] " _
                                   & "WHERE [a].[RowStatus] = 0 " _
                                   & "AND [a].[ChassisMasterID] = " & fs.ChassisMaster.ID & " " _
                                   & "AND a.ID = " & fs.ID & " "
            Return m_FreeServiceMapper.RetrieveDataSet(strSQL)
        End Function
#End Region

    End Class

End Namespace
