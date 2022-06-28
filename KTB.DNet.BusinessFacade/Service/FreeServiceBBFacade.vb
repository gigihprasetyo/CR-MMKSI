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

#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class FreeServiceBBFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_FreeServiceBBMapper As IMapper
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
            Me.m_FreeServiceBBMapper = MapperFactory.GetInstance().GetMapper(GetType(FreeServiceBB).ToString)
            Me.objTransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FreeServiceBB
            Return CType(m_FreeServiceBBMapper.Retrieve(ID), FreeServiceBB)
        End Function

        Public Function Retrieve(ByVal ID As String) As FreeServiceBB
            Return CType(m_FreeServiceBBMapper.Retrieve(Convert.ToInt32(ID)), FreeServiceBB)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FreeServiceBBMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FreeServiceBBMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FreeServiceBBMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FreeServiceBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FreeServiceBBMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FreeServiceBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FreeServiceBBMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeServiceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FreeServiceBB As ArrayList = m_FreeServiceBBMapper.RetrieveByCriteria(criterias)
            Return _FreeServiceBB
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeServiceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FreeServiceBBColl As ArrayList = m_FreeServiceBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FreeServiceBBColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FreeServiceBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FreeServiceBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FreeServiceBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim FreeServiceBBColl As ArrayList = m_FreeServiceBBMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return FreeServiceBBColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FreeServiceBBColl As ArrayList = m_FreeServiceBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FreeServiceBBColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeServiceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FreeServiceBB), columnName, matchOperator, columnValue))
            Dim FreeServiceBBColl As ArrayList = m_FreeServiceBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FreeServiceBBColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FreeServiceBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeServiceBB), columnName, matchOperator, columnValue))

            Return m_FreeServiceBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As FreeServiceBB) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_FreeServiceBBMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As FreeServiceBB) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FreeServiceBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        'Public Function IsTypeAllowed(ByVal oFS As FreeServiceBB) As Boolean
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
                        For Each objFS As FreeServiceBB In arrFS

                            ''coding sementara untuk cari type
                            'Dim flagAda As Integer = 0
                            'Dim flagOK As Integer = 0
                            'For i As Integer = 0 To objType.Length - 1
                            '    If objType(i) = objFS.ChassisMasterBB.VechileColor.VechileType.VechileTypeCode Then
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
                            '            Dim _strVehicleType As String = objFS.ChassisMasterBB.VechileColor.VechileType.Description.Substring(0, 3).ToUpper()
                            '            If _strVehicleType = "FE7" Or _strVehicleType = "FE8" Then
                            '                Dim _dateFrom As DateTime = DateTime.ParseExact("01/02/2010", "dd/MM/yyyy", theCultureInfo)
                            '                Dim _dateTo As DateTime = DateTime.ParseExact("30/06/2010", "dd/MM/yyyy", theCultureInfo)

                            '                If (objFS.ChassisMasterBB.EndCustomer.OpenFakturDate >= _dateFrom And objFS.ChassisMasterBB.EndCustomer.OpenFakturDate <= _dateTo) Then
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


        Public Sub Delete(ByVal objDomain As FreeServiceBB)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FreeServiceBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As FreeServiceBB)
            Try
                m_FreeServiceBBMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal sID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeServiceBB), "ID", MatchType.Exact, sID))
            Dim agg As Aggregate = New Aggregate(GetType(FreeServiceBB), "ID", AggregateType.Count)

            Return CType(m_FreeServiceBBMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function IsAllowFreeServiceBB(ByVal objFreeServiceBB As FreeServiceBB) As Boolean
            If ChassisException(True, objFreeServiceBB) = True _
                And IsAllowFSCampaign(objFreeServiceBB) = True Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function VehicleTypeException(ByVal objFreeServiceBB As FreeServiceBB) As Boolean
            Dim objType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}
            'End    :CR - Temporary allowing TU00 :Rina
            Dim isAllow As Boolean = True
            For i As Integer = 0 To objType.Length - 1
                If objFreeServiceBB.ChassisMasterBB.VechileColor.VechileType.VechileTypeCode = objType(i) And (objFreeServiceBB.FSKind.KindCode = "3" Or objFreeServiceBB.FSKind.KindCode = "4" Or objFreeServiceBB.FSKind.KindCode = "5") Then
                    isAllow = False
                    'MessageBox.Show("Simpan gagal, Chassis " & objFreeServiceBB.ChassisMasterBB.ChassisNumber & " tidak mendapat kupon Free Service")
                    Exit For
                End If
            Next
            Return isAllow
        End Function

        Public Function ChassisException(ByVal IsAllowInsert As Boolean, ByRef objFreeServiceBB As FreeServiceBB) As Boolean
            'Start  :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
            If IsAllowInsert = True _
            AndAlso (objFreeServiceBB.FSKind.KindCode = "3" _
            OrElse objFreeServiceBB.FSKind.KindCode = "4" _
            OrElse objFreeServiceBB.FSKind.KindCode = "5" _
            OrElse objFreeServiceBB.FSKind.KindCode = "6" _
            OrElse objFreeServiceBB.FSKind.KindCode = "7") Then
                Dim sForbiddenCMs() As String = {"MHMFE71P1AK018514", "MHMFE73P2AK014642", "MHMFE73P2AK014643", "MHMFE73P2AK014715", "MHMFE73P2AK014760"}
                For i As Integer = 0 To sForbiddenCMs.Length - 1
                    If objFreeServiceBB.ChassisMasterBB.ChassisNumber.Trim.ToUpper = sForbiddenCMs(i).Trim.ToUpper Then
                        IsAllowInsert = False
                        'MessageBox.Show("Simpan gagal, Chassis " & objFreeServiceBB.ChassisMasterBB.ChassisNumber & " tidak mendapat kupon Free Service")
                        Exit For
                    End If
                Next
            End If
            Return IsAllowInsert
            'End    :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
        End Function

        Public Function IsAllowFSCampaign(ByVal objFreeServiceBB As FreeServiceBB) As Boolean
            Dim vReturn As Boolean = False
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
                            If objFSCampaignDealer.DealerCode = objFreeServiceBB.Dealer.DealerCode Then
                                bDealer = True
                            End If
                        Next
                    End If

                    'FSKind checking
                    Dim bFSKind As Boolean = True
                    If objFSCampaign.FSTypeChecked = True Then
                        bFSKind = False
                        For Each objFSCampaignKind As FSCampaignKind In objFSCampaign.FSCampaignKinds
                            If objFSCampaignKind.FSKind.KindCode = objFreeServiceBB.FSKind.KindCode Then
                                bFSKind = True
                            End If
                        Next
                    End If

                    'VehicleType checking
                    Dim bVehicle As Boolean = True
                    If objFSCampaign.VehicleTypeChecked = True Then
                        bVehicle = False
                        For Each objFSCampaignVehicle As FSCampaignVehicle In objFSCampaign.FSCampaignVehicles
                            If objFSCampaignVehicle.VechileType.ID = objFreeServiceBB.ChassisMasterBB.VechileColor.VechileType.ID Then
                                bVehicle = True
                            End If
                        Next
                    End If

                    'Faktur Validation checking
                    Dim bFaktur As Boolean = True
                    If objFSCampaign.FakturDateChecked = True Then
                        bFaktur = False
                        If Not IsNothing(objFreeServiceBB.ChassisMasterBB.EndCustomer) Then
                            If objFSCampaign.DateFrom <= objFreeServiceBB.ChassisMasterBB.EndCustomer.ValidateTime _
                           And objFSCampaign.DateTo >= objFreeServiceBB.ChassisMasterBB.EndCustomer.ValidateTime Then
                                bFaktur = True
                            End If
                        Else
                            bFaktur = True
                        End If
                    End If

                    'Combine value above
                    If bDealer And bFSKind And bVehicle And bFaktur Then
                        vReturn = True
                        Exit For
                    End If
                Next
            End If
            Return vReturn
        End Function
#End Region

    End Class

End Namespace
