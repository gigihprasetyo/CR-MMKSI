
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
'// Copyright  2015
'// ---------------------
'// $History      : $
'// Generated on 12/11/2015 - 8:54:29 AM
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

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


Imports Microsoft.CSharp
Imports System.CodeDom.Compiler
Imports System.Reflection
#End Region

Namespace KTB.DNet.BusinessFacade.Benefit

    Public Class BenefitMasterHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BenefitMasterHeaderMapper As IMapper
        Private m_BenefitMasterDealerMapper As IMapper

        Private m_BenefitMasterDetailMapper As IMapper
        Private m_BenefitMasterLeasingMapper As IMapper
        Private m_BenefitMasterVehicleMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BenefitMasterHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitMasterHeader).ToString)

            Me.m_BenefitMasterDealerMapper = MapperFactory.GetInstance().GetMapper(GetType(BenefitMasterDealer).ToString)

            Me.m_BenefitMasterDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(BenefitMasterDetail).ToString)
            Me.m_BenefitMasterLeasingMapper = MapperFactory.GetInstance().GetMapper(GetType(BenefitMasterLeasing).ToString)
            Me.m_BenefitMasterVehicleMapper = MapperFactory.GetInstance().GetMapper(GetType(BenefitMasterVehicleType).ToString)

            Me.m_TransactionManager = New TransactionManager

            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BenefitMasterHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BenefitMasterDetail))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BenefitMasterDealer))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BenefitMasterLeasing))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BenefitMasterVehicleType))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BenefitMasterHeader
            Return CType(m_BenefitMasterHeaderMapper.Retrieve(ID), BenefitMasterHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As BenefitMasterHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "BenefitMasterHeaderCode", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "BenefitRegNo", MatchType.Exact, Code))

            Dim BenefitMasterHeaderColl As ArrayList = m_BenefitMasterHeaderMapper.RetrieveByCriteria(criterias)
            If (BenefitMasterHeaderColl.Count > 0) Then
                Return CType(BenefitMasterHeaderColl(0), BenefitMasterHeader)
            End If
            Return New BenefitMasterHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BenefitMasterHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BenefitMasterHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BenefitMasterHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitMasterHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitMasterHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitMasterHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitMasterHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sorts As ICollection) As ArrayList
            Return m_BenefitMasterHeaderMapper.RetrieveByCriteria(Criterias, sorts)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BenefitMasterHeader As ArrayList = m_BenefitMasterHeaderMapper.RetrieveByCriteria(criterias)
            Return _BenefitMasterHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitMasterHeaderColl As ArrayList = m_BenefitMasterHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BenefitMasterHeaderColl
        End Function

        

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BenefitMasterHeaderColl As ArrayList = m_BenefitMasterHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BenefitMasterHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitMasterHeaderColl As ArrayList = m_BenefitMasterHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), columnName, matchOperator, columnValue))
            Return BenefitMasterHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitMasterHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), columnName, matchOperator, columnValue))

            Return m_BenefitMasterHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "BenefitMasterHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BenefitMasterHeader), "BenefitMasterHeaderCode", AggregateType.Count)
            Return CType(m_BenefitMasterHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"



        Public Function Insert(ByVal objDomain As BenefitMasterHeader, ByVal objDetails As ArrayList, ByVal dealerList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)



                    For Each item As BenefitMasterDealer In dealerList
                        item.BenefitMasterHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    If objDetails.Count > 0 Then
                        For Each items As BenefitMasterDetail In objDetails
                            items.BenefitMasterHeader = objDomain
                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)


                            If items.BenefitMasterLeasings.Count > 0 Then
                                For Each items1 As BenefitMasterLeasing In items.BenefitMasterLeasings
                                    items1.BenefitMasterDetail = items
                                    m_TransactionManager.AddInsert(items1, m_userPrincipal.Identity.Name)
                                Next
                            End If

                            If items.BenefitMasterVehicleTypes.Count > 0 Then
                                For Each items2 As BenefitMasterVehicleType In items.BenefitMasterVehicleTypes
                                    items2.BenefitMasterDetail = items
                                    m_TransactionManager.AddInsert(items2, m_userPrincipal.Identity.Name)
                                Next
                            End If


                        Next
                    End If


                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()

                        ' returnValue = 0
                        returnValue = objDomain.ID
                    End If
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

        
        Public Function Update(ByVal objDomain As BenefitMasterHeader, ByVal arrDetail As ArrayList, _
                               ByVal arrDealer As ArrayList) As Integer
            Dim returnValue As Integer = -1


            Dim ariddetailOld As ArrayList = New ArrayList
            Dim aridleasingOld As ArrayList = New ArrayList
            Dim aridvehicleOld As ArrayList = New ArrayList

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()



                    If Not IsNothing(objDomain.BenefitMasterDetails) AndAlso objDomain.BenefitMasterDetails.Count > 0 Then
                        For Each objDetail As BenefitMasterDetail In objDomain.BenefitMasterDetails
                            'm_TransactionManager.AddDelete(objDetail)
                            Dim statusDetail As Boolean = False
                            For Each objDetailList As BenefitMasterDetail In arrDetail
                                If objDetail.FormulaID = objDetailList.FormulaID Then
                                    statusDetail = True
                                    objDetail.MaxClaim = objDetailList.MaxClaim
                                    objDetail.Description = objDetailList.Description
                                    objDetail.Amount = objDetailList.Amount
                                    objDetail.FakturValidationStart = objDetailList.FakturValidationStart
                                    objDetail.FakturValidationEnd = objDetailList.FakturValidationEnd
                                    objDetail.FakturOpenStart = objDetailList.FakturOpenStart
                                    objDetail.FakturOpenEnd = objDetailList.FakturOpenEnd
                                    objDetail.BenefitType.ID = objDetailList.BenefitType.ID
                                    objDetail.AssyYear = objDetailList.AssyYear
                                    'update BenefitMasterDetail
                                    m_TransactionManager.AddUpdate(objDetail, m_userPrincipal.Identity.Name)

                                    'delete leasing related by benefitmasterdetail
                                    If objDetail.BenefitMasterLeasings.Count > 0 Then
                                        For Each items1a As BenefitMasterLeasing In objDetail.BenefitMasterLeasings
                                            'm_TransactionManager.AddDelete(items1a)
                                            items1a.RowStatus = -1
                                            m_TransactionManager.AddUpdate(items1a, m_userPrincipal.Identity.Name)
                                        Next
                                    End If

                                    'delete vehicleType related by benefitmasterdetail
                                    If objDetail.BenefitMasterVehicleTypes.Count > 0 Then
                                        For Each items2a As BenefitMasterVehicleType In objDetail.BenefitMasterVehicleTypes
                                            'm_TransactionManager.AddDelete(items2a)
                                            items2a.RowStatus = -1
                                            m_TransactionManager.AddUpdate(items2a, m_userPrincipal.Identity.Name)
                                        Next
                                    End If

                                    Exit For
                                End If
                            Next

                            If statusDetail = False Then
                                'delete BenefitMasterDetail
                                'check BenefitClaimDetail
                                Dim _arrListClaim As New ArrayList
                                Dim criteriaClaim As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criteriaClaim.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitMasterDetail.ID", MatchType.Exact, CInt(objDetail.ID)))
                                _arrListClaim = New BenefitClaimDetailsFacade(m_userPrincipal).Retrieve(criteriaClaim)

                                If (Not IsNothing(_arrListClaim) AndAlso _arrListClaim.Count < 1) Then
                                    objDetail.RowStatus = -1
                                    m_TransactionManager.AddUpdate(objDetail, m_userPrincipal.Identity.Name)
                                End If
                                'end check BenefitClaimDetail

                                If objDetail.BenefitMasterLeasings.Count > 0 Then
                                    For Each items1a As BenefitMasterLeasing In objDetail.BenefitMasterLeasings
                                        'm_TransactionManager.AddDelete(items1)
                                        items1a.RowStatus = -1
                                        m_TransactionManager.AddUpdate(items1a, m_userPrincipal.Identity.Name)

                                    Next
                                End If

                                If objDetail.BenefitMasterVehicleTypes.Count > 0 Then
                                    For Each items2a As BenefitMasterVehicleType In objDetail.BenefitMasterVehicleTypes
                                        'm_TransactionManager.AddDelete(items2)
                                        items2a.RowStatus = -1
                                        m_TransactionManager.AddUpdate(items2a, m_userPrincipal.Identity.Name)
                                    Next
                                End If


                            End If
                        Next
                    End If


                    If Not IsNothing(objDomain.BenefitMasterDealers) AndAlso objDomain.BenefitMasterDealers.Count > 0 Then
                        For Each objDetail As BenefitMasterDealer In objDomain.BenefitMasterDealers
                            'm_TransactionManager.AddDelete(objDetail)
                            objDetail.RowStatus = -1
                            m_TransactionManager.AddUpdate(objDetail, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDetail.Count > 0 Then
                        'Update SPLDetail
                        For Each objDetail As BenefitMasterDetail In arrDetail
                            Dim statusDetail As Boolean = False
                            For Each objDetailData As BenefitMasterDetail In objDomain.BenefitMasterDetails
                                If objDetail.FormulaID = objDetailData.FormulaID Then
                                    statusDetail = True

                                    'insert new leasing related by benefitmasterdetail
                                    If objDetail.BenefitMasterLeasings.Count > 0 Then
                                        For Each items1b As BenefitMasterLeasing In objDetail.BenefitMasterLeasings
                                            items1b.BenefitMasterDetail = objDetailData
                                            items1b.RowStatus = 0
                                            m_TransactionManager.AddInsert(items1b, m_userPrincipal.Identity.Name)
                                        Next
                                    End If

                                    'insert new vehicleType related by benefitmasterdetail
                                    If objDetail.BenefitMasterVehicleTypes.Count > 0 Then
                                        For Each items2b As BenefitMasterVehicleType In objDetail.BenefitMasterVehicleTypes
                                            items2b.BenefitMasterDetail = objDetailData
                                            items2b.RowStatus = 0
                                            m_TransactionManager.AddInsert(items2b, m_userPrincipal.Identity.Name)
                                        Next
                                    End If
                                    Exit For
                                End If

                            Next
                            If statusDetail = False Then
                                'insert new BenefitMasterDetail
                                objDetail.BenefitMasterHeader = objDomain
                                objDetail.RowStatus = 0

                                m_TransactionManager.AddInsert(objDetail, m_userPrincipal.Identity.Name)

                                If objDetail.BenefitMasterLeasings.Count > 0 Then
                                    For Each items1b As BenefitMasterLeasing In objDetail.BenefitMasterLeasings
                                        items1b.BenefitMasterDetail = objDetail
                                        items1b.RowStatus = 0
                                        m_TransactionManager.AddInsert(items1b, m_userPrincipal.Identity.Name)
                                    Next
                                End If

                                If objDetail.BenefitMasterVehicleTypes.Count > 0 Then
                                    For Each items2b As BenefitMasterVehicleType In objDetail.BenefitMasterVehicleTypes
                                        items2b.BenefitMasterDetail = objDetail
                                        items2b.RowStatus = 0
                                        m_TransactionManager.AddInsert(items2b, m_userPrincipal.Identity.Name)
                                    Next
                                End If

                            End If
                        Next
                    End If

                    If arrDealer.Count > 0 Then
                        'UpdateDealer
                        For Each itemaa As BenefitMasterDealer In arrDealer
                            itemaa.BenefitMasterHeader = objDomain
                            itemaa.RowStatus = 0
                            m_TransactionManager.AddInsert(itemaa, m_userPrincipal.Identity.Name)
                        Next
                    End If


                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    ' returnValue = m_BenefitMasterHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        'Public Function Update(ByVal objDomain As BenefitMasterHeader, ByVal arrDetail As ArrayList, _
        '                      ByVal arrDealer As ArrayList) As Integer
        '    Dim returnValue As Integer = -1


        '    Dim ariddetailOld As ArrayList = New ArrayList
        '    Dim aridleasingOld As ArrayList = New ArrayList
        '    Dim aridvehicleOld As ArrayList = New ArrayList
        '    Dim ariddealerOld As ArrayList = New ArrayList

        '    Dim ariddetail As ArrayList = New ArrayList
        '    Dim aridleasing As ArrayList = New ArrayList
        '    Dim aridvehicle As ArrayList = New ArrayList
        '    Dim ariddealer As ArrayList = New ArrayList

        '    Dim objBenefitMasterHeader As BenefitMasterHeader

        '    If (Me.IsTaskFree()) Then
        '        Try
        '            Me.SetTaskLocking()


        '            objBenefitMasterHeader = Retrieve(objDomain.ID)
        '            If Not objBenefitMasterHeader.BenefitMasterDetails Is Nothing Then
        '                For Each items As BenefitMasterDetail In objBenefitMasterHeader.BenefitMasterDetails
        '                    For Each items1 As BenefitMasterLeasing In items.BenefitMasterLeasings
        '                        aridleasingOld.Add(items1.ID)
        '                    Next
        '                    For Each items1 As BenefitMasterVehicleType In items.BenefitMasterVehicleTypes
        '                        aridvehicleOld.Add(items1.ID)
        '                    Next
        '                    ariddetailOld.Add(items.ID)
        '                Next
        '            End If
        '            If Not objBenefitMasterHeader.BenefitMasterDealers Is Nothing Then
        '                For Each items As BenefitMasterDealer In objBenefitMasterHeader.BenefitMasterDealers
        '                    ariddealerOld.Add(items.ID)
        '                Next
        '            End If


        '            Dim idHeader As Integer = m_BenefitMasterHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)

        '            If objDomain.BenefitMasterDetails.Count > 0 Then

        '                For Each items As BenefitMasterDetail In objDomain.BenefitMasterDetails
        '                    items.RowStatus = -1
        '                    Dim idDetail As Integer = m_BenefitMasterDetailMapper.Update(items, m_userPrincipal.Identity.Name)
        '                    For Each items1 As BenefitMasterLeasing In items.BenefitMasterLeasings
        '                        items1.RowStatus = -1
        '                        Dim idleasing As Integer = m_BenefitMasterLeasingMapper.Update(items1, m_userPrincipal.Identity.Name)
        '                    Next
        '                    For Each items1 As BenefitMasterVehicleType In items.BenefitMasterVehicleTypes
        '                        items1.RowStatus = -1
        '                        Dim idvehicle As Integer = m_BenefitMasterVehicleMapper.Update(items, m_userPrincipal.Identity.Name)
        '                    Next

        '                Next
        '            End If

        '            If objDomain.BenefitMasterDealers.Count > 0 Then

        '                For Each items As BenefitMasterDealer In objDomain.BenefitMasterDealers
        '                    items.RowStatus = -1
        '                    Dim idDealer As Integer = m_BenefitMasterDealerMapper.Update(items, m_userPrincipal.Identity.Name)

        '                Next
        '            End If



        '            If arrDetail.Count > 0 Then
        '                For Each items As BenefitMasterDetail In arrDetail

        '                    For Each items1 As BenefitMasterLeasing In items.BenefitMasterLeasings
        '                        items1.BenefitMasterDetail = items
        '                        Dim idleasing As Integer = m_BenefitMasterLeasingMapper.Insert(items1, m_userPrincipal.Identity.Name)
        '                        aridleasing.Add(idleasing)
        '                    Next

        '                    For Each items1 As BenefitMasterVehicleType In items.BenefitMasterVehicleTypes
        '                        items1.BenefitMasterDetail = items
        '                        Dim idvehicle As Integer = m_BenefitMasterLeasingMapper.Insert(items1, m_userPrincipal.Identity.Name)
        '                        aridvehicle.Add(idvehicle)
        '                    Next

        '                    Dim idDetail As Integer = m_BenefitMasterDetailMapper.Insert(items.BenefitMasterLeasings, m_userPrincipal.Identity.Name)
        '                    ariddetail.Add(idDetail)
        '                Next
        '            End If

        '            If arrDealer.Count > 0 Then
        '                For Each items As BenefitMasterDealer In arrDealer
        '                    items.BenefitMasterHeader = objDomain
        '                    Dim idDetail As Integer = m_BenefitMasterDetailMapper.Insert(items, m_userPrincipal.Identity.Name)
        '                    ariddetail.Add(idDetail)
        '                Next
        '            End If




        '            m_TransactionManager.PerformTransaction()
        '            returnValue = objDomain.ID
        '        Catch ex As Exception
        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '            If rethrow Then
        '                Throw
        '            End If
        '        Finally
        '            Me.RemoveTaskLocking()
        '        End Try
        '    End If
        '    Return returnValue
        'End Function

        Public Function Delete(ByVal objDomain As BenefitMasterHeader) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not IsNothing(objDomain.BenefitMasterDetails) AndAlso objDomain.BenefitMasterDetails.Count > 0 Then
                        For Each objDetail As BenefitMasterDetail In objDomain.BenefitMasterDetails
                            objDetail.RowStatus = -1
                            m_TransactionManager.AddUpdate(objDetail, m_userPrincipal.Identity.Name)

                            If objDetail.BenefitMasterLeasings.Count > 0 Then
                                For Each items1a As BenefitMasterLeasing In objDetail.BenefitMasterLeasings
                                    items1a.RowStatus = -1
                                    m_TransactionManager.AddUpdate(items1a, m_userPrincipal.Identity.Name)

                                Next
                            End If

                            If objDetail.BenefitMasterVehicleTypes.Count > 0 Then
                                For Each items2a As BenefitMasterVehicleType In objDetail.BenefitMasterVehicleTypes
                                    items2a.RowStatus = -1
                                    m_TransactionManager.AddUpdate(items2a, m_userPrincipal.Identity.Name)
                                Next
                            End If

                        Next
                    End If


                    If Not IsNothing(objDomain.BenefitMasterDealers) AndAlso objDomain.BenefitMasterDealers.Count > 0 Then
                        For Each objDetail As BenefitMasterDealer In objDomain.BenefitMasterDealers

                            objDetail.RowStatus = -1
                            m_TransactionManager.AddUpdate(objDetail, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    objDomain.RowStatus = -1
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    ' returnValue = m_BenefitMasterHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        Public Function Insert(ByVal objDomain As BenefitMasterHeader) As Integer
            Dim iReturn As Integer = -2
            Try

                m_BenefitMasterHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function RetrieveActiveList(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias..opAnd(New Criteria(GetType(AlertMaster), "BenefitMasterHeader.Benefitregno", MatchType.[Partial], txtCodeDealer.Text.Trim))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BenefitMasterHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim BenefitMasterHeaderColl As ArrayList = m_BenefitMasterHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return BenefitMasterHeaderColl
        End Function

        Public Function RetrieveFromSP(ByVal TransactionType As Integer _
            , ByVal PageIndex As Integer, ByVal PageSize As Integer, ByVal IsFactoring As Short _
            , ByVal NewNormal As Integer, ByVal NewFactoring As Integer _
            , ByVal DealerIDs As String, ByVal ProvinceIDs As String _
            , ByVal CategoryID As Integer, ByVal VechileTypeIDs As String _
            , ByVal MaxTOPDay As Integer _
            , ByVal TotalRow As Integer, ByVal ExecutedBy As String _
            ) As ArrayList
            Dim SQL As String = String.Empty

            SQL = "exec dbo].[ufn_CreateSalesCampaignNumber] ( " & TransactionType & "," & PageIndex & "," & PageSize & "," & IsFactoring _
                & "," & NewNormal.ToString & "," & NewFactoring.ToString _
                & ",'" & DealerIDs & "','" & ProvinceIDs & "'" _
                & "," & CategoryID & ",'" & VechileTypeIDs & "'," & MaxTOPDay & "," & TotalRow & " ,'" & ExecutedBy & "'"

            Return m_BenefitMasterHeaderMapper.RetrieveSP(SQL)
            'm_BenefitMasterHeaderMapper.RetrieveScalar()
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.BenefitMasterHeader) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.BenefitMasterHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.BenefitMasterHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is BenefitMasterDetail) Then
                CType(InsertArg.DomainObject, BenefitMasterDetail).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is BenefitMasterLeasing) Then
                CType(InsertArg.DomainObject, BenefitMasterLeasing).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is BenefitMasterDealer) Then
                CType(InsertArg.DomainObject, BenefitMasterDealer).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is BenefitMasterVehicleType) Then
                CType(InsertArg.DomainObject, BenefitMasterVehicleType).ID = InsertArg.ID
            End If
        End Sub

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList

            Dim BenefitMasterHeaderColl As ArrayList = m_BenefitMasterHeaderMapper.RetrieveByCriteria(criterias)

            Return BenefitMasterHeaderColl
        End Function

        Private Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
            Dim Generator As System.Random = New System.Random()
            Return Generator.Next(Min, Max)
        End Function

        Public Function Evaluate(ByVal MathExpression As String) As Decimal

            Dim tempMathExpression As String

            Dim constAlfabet As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

            ' MathExpression = "A&B.A&C.B&A&D"

            tempMathExpression = MathExpression.Replace(" ", "").Replace(",", "")
            Dim values As String() = tempMathExpression.Split({"."}, StringSplitOptions.RemoveEmptyEntries)
            'Dim values As String() = MathExpression.Split({"."}, StringSplitOptions.RemoveEmptyEntries)

            Dim result As Decimal = 2
            Dim ca As Integer
            Dim forSort As New ArrayList
            Dim tempStringSort As String = ""
            For i As Integer = 0 To values.Length - 1
                If Not values(i) = "" Then
                    Dim blokFormula As String = values(i)

                    Dim index As Integer = 0
                    Dim tempForSort As New ArrayList
                    For Each c As Char In blokFormula
                        If Not c = "&" Then
                            tempForSort.Add(c)
                            Dim count As Short = blokFormula.Split(c).Length - 1
                            If count > 1 Then
                                Return -2
                            End If
                        End If
                        ca = index Mod 2

                        If index > 0 Then
                            If ca = 1 Then 'jika genap
                                If Not c = "&" Then
                                    result = 0
                                End If
                            Else
                                If Not constAlfabet.Contains(c) = True Then
                                    result = 0
                                End If
                            End If
                        End If

                        index = index + 1
                    Next
                    tempForSort.Sort()
                    tempStringSort = ""
                    For Each c As String In tempForSort
                        tempStringSort = tempStringSort & c
                    Next
                    forSort.Add(tempStringSort)
                End If
            Next

            If Not result = 0 Then
                Dim string1 As String = ""
                Dim string2 As String = ""
                For i As Integer = 0 To forSort.Count - 1
                    For j As Integer = 0 To forSort.Count - 1
                        string1 = forSort(i).ToString
                        string2 = forSort(j).ToString
                        If Not i = j Then
                            For Each c As Char In string2
                                string1 = string1.Replace(c, "1")
                            Next
                            If string1.Split("1").Length - 1 = string2.Length Then
                                result = -1
                            End If
                        End If
                    Next
                Next
            End If

            Return result
        End Function

        Public Function Evaluate(ByVal MathExpression As String, ByVal constAlfabet As String) As Decimal

            Dim tempMathExpression As String

            'Dim constAlfabet As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            Dim arrConstAlfabet As String() = constAlfabet.Split(";")

            ' MathExpression = "A&B.A&C.B&A&D"

            tempMathExpression = MathExpression.Replace(" ", "").Replace(",", "")
            Dim values As String() = tempMathExpression.Split({"."}, StringSplitOptions.RemoveEmptyEntries)
            
            Dim result As Decimal = 2
            Dim ca As Integer
            Dim forSort As New ArrayList
            Dim tempStringSort As String = ""
            For i As Integer = 0 To values.Length - 1
                If Not values(i) = "" Then
                    Dim blokFormula As String = values(i)
                    Dim arrBlokFormula As ArrayList = New ArrayList
                    Dim tempArrBlokFormula As ArrayList = New ArrayList(blokFormula.Split(";"))
                    For Each a As String In tempArrBlokFormula
                        If Not a = "" Then
                            arrBlokFormula.Add(a)
                        End If
                    Next
                    Dim index As Integer = 0
                    Dim tempForSort As New ArrayList
                    For Each c As String In arrBlokFormula
                        If Not c = "&" Then
                            tempForSort.Add(c)
                            Dim temp As String = removeAll(blokFormula, ";")
                            Dim temp2 As String = String.Empty
                            For Each a As String In temp
                                If Not a = "" Then
                                    temp2 += a
                                End If
                            Next
                            Dim temp1 As String() = temp2.Split(New String() {c}, System.StringSplitOptions.None)
                            Dim count As Short = temp1.Length - 1
                            If count > 1 Then
                                Return -2
                            End If
                        End If
                        ca = index Mod 2

                        If index > 0 Then
                            If ca = 1 Then 'jika genap
                                If Not c = "&" Then
                                    result = 0
                                End If
                            Else
                                If Not Contains(arrConstAlfabet, c.ToString()) = True Then
                                    result = 0
                                End If
                            End If
                        End If

                        index = index + 1
                    Next
                    tempForSort.Sort()
                    tempStringSort = ""
                    For Each c As String In tempForSort
                        tempStringSort = tempStringSort & c
                    Next
                    forSort.Add(tempStringSort)
                End If
            Next

            If Not result = 0 Then
                Dim string1 As String = ""
                Dim string2 As String = ""
                For i As Integer = 0 To forSort.Count - 1
                    For j As Integer = 0 To forSort.Count - 1
                        string1 = forSort(i).ToString
                        string2 = forSort(j).ToString
                        If Not i = j Then
                            'For Each c As Char In string2
                            '    string1 = string1.Replace(c, "1")
                            'Next
                            'If string1.Split("1").Length - 1 = string2.Length Then
                            '    result = -1
                            'End If
                            If string1 = string2 Then
                                result = -1
                            End If
                        End If
                    Next
                Next
            End If

            Return result
        End Function

        Private Function Contains(ByVal arrString As String(), ByVal str As String) As Boolean
            For Each s As String In arrString
                If s = str Then
                    Return True
                End If
            Next
            Return False
        End Function

        Private Function removeAll(ByVal str As String, ByVal chr As Char) As String
            Dim result As String = String.Empty
            For Each c As Char In str
                If Not c = chr Then
                    result += c
                End If
            Next

            Return result
        End Function

        Public Function UpdateStatus(ByVal items As BenefitMasterHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)


                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If

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

        Public Function BindGridDSFClaim(ByVal selectedClaimID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sqlCmd As String = "select distinct c.ID from BenefitType a "
            sqlCmd += "inner join BenefitMasterDetail b on a.ID = b.BenefitTypeID "
            sqlCmd += "inner join BenefitMasterHeader c on c.ID = b.BenefitMasterHeaderID "
            sqlCmd += "inner join BenefitMasterLeasing d on d.BenefitMasterDetailID = b.ID "
            sqlCmd += "inner join LeasingCompany e on e.ID = d.LeasingCompanyID "
            sqlCmd += "where 1=1 "
            sqlCmd += "and a.RowStatus = 0 "
            sqlCmd += "and b.RowStatus = 0 "
            sqlCmd += "and c.RowStatus = 0 "
            sqlCmd += "and d.RowStatus = 0 "
            sqlCmd += "and e.RowStatus = 0 "
            sqlCmd += "and LeasingBox = 1 "
            sqlCmd += "and e.ID = 1 "
            sqlCmd += String.Format("and a.ID = {0} ", selectedClaimID)
            criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "ID", MatchType.InSet, "(" & sqlCmd & ")"))
            Return m_BenefitMasterHeaderMapper.RetrieveByCriteria(criterias)
        End Function




        'Public Function Evaluate(ByVal MathExpression As String) As Decimal

        '    Dim tempMathExpression As String

        '    For Each c As Char In MathExpression.Replace(",", "")
        '        ' Count c
        '        If Not c = "" And Not c = "&" And Not c = "|" _
        '            And Not c = "(" And Not c = ")" And Not c = "." Then
        '            tempMathExpression = tempMathExpression & GetRandom(1, 10).ToString
        '        Else
        '            If c = "&" Then
        '                tempMathExpression = tempMathExpression & "+"
        '            ElseIf c = "|" Then
        '                tempMathExpression = tempMathExpression & "*"
        '            Else
        '                tempMathExpression = tempMathExpression & c
        '            End If

        '        End If
        '    Next

        '    Dim values As String() = tempMathExpression.Split({"."}, StringSplitOptions.RemoveEmptyEntries)
        '    Dim result As Decimal = 0
        '    For i As Integer = 0 To values.Length - 1
        '        If Not values(i) = "" Then
        '            Try
        '                Dim codeProvider As CSharpCodeProvider = New CSharpCodeProvider()
        '                Dim compilerParameters As CompilerParameters = New CompilerParameters
        '                compilerParameters.GenerateExecutable = False
        '                compilerParameters.GenerateInMemory = False


        '                Dim tmpModuleSource As String = "namespace ns{"
        '                tmpModuleSource = tmpModuleSource & "using System;"
        '                tmpModuleSource = tmpModuleSource & "class class1{"
        '                tmpModuleSource = tmpModuleSource & "    public static decimal Eval(){"
        '                tmpModuleSource = tmpModuleSource & "          return Convert.ToDecimal(" & values(i) & ");"
        '                tmpModuleSource = tmpModuleSource & "     }"
        '                tmpModuleSource = tmpModuleSource & "}} "

        '                Dim CompilerResults As CompilerResults = codeProvider.CompileAssemblyFromSource(compilerParameters, tmpModuleSource)

        '                If CompilerResults.Errors.Count > 0 Then
        '                    result = 0
        '                    Exit For
        '                Else
        '                    Dim Methinfo As MethodInfo = CompilerResults.CompiledAssembly.GetType("ns.class1").GetMethod("Eval")
        '                    result = Convert.ToDecimal(Methinfo.Invoke(Nothing, Nothing))
        '                End If

        '            Catch ex As Exception
        '                result = 0
        '                Exit For
        '            End Try
        '        End If
        '    Next

        '    Return result
        'End Function

#End Region

    End Class

End Namespace

