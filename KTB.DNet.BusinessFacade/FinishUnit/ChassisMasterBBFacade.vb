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
'// Author Name   : Agus Soepriadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 10/10/2005 - 10:53:00 AM
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
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.service
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class ChassisMasterBBFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_ChassisMasterBBMapper As IMapper
        Private m_V_CMHelperMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EndCustomerMapper As IMapper
        Private m_CustomerMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ChassisMasterBBMapper = MapperFactory.GetInstance().GetMapper(GetType(ChassisMasterBB).ToString)
            Me.m_V_CMHelperMapper = MapperFactory.GetInstance().GetMapper(GetType(V_CMHelper).ToString)
            Me.m_EndCustomerMapper = MapperFactory.GetInstance().GetMapper(GetType(EndCustomer).ToString)
            Me.m_CustomerMapper = MapperFactory.GetInstance().GetMapper(GetType(Customer).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManagerInsert)
            Me.DomainTypeCollection.Add(GetType(ChassisMasterBB))
            Me.DomainTypeCollection.Add(GetType(EndCustomer))
            Me.DomainTypeCollection.Add(GetType(Customer))
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveByCompany(ByVal Code As String, ByVal CompanyCode As String) As ChassisMasterBB
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterBB), "Category.ProductCategory.Code", MatchType.Exact, CompanyCode))
            Dim ChassisMasterColl As ArrayList = m_ChassisMasterBBMapper.RetrieveByCriteria(criterias)
            If (ChassisMasterColl.Count > 0) Then
                Return CType(ChassisMasterColl(0), ChassisMasterBB)
            End If
            Return New ChassisMasterBB
        End Function

        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Integer
            Return CType(m_ChassisMasterBBMapper.RetrieveScalar(aggr, crit), Integer)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As ChassisMasterBB
            Return CType(m_ChassisMasterBBMapper.Retrieve(ID), ChassisMasterBB)
        End Function

        Public Function RetrieveCmHelper(ByVal ID As Integer) As V_CMHelper
            Return CType(m_V_CMHelperMapper.Retrieve(ID), V_CMHelper)
        End Function

        Public Function Retrieve(ByVal Code As String) As ChassisMasterBB
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ChassisMasterBBColl As ArrayList = m_ChassisMasterBBMapper.RetrieveByCriteria(criterias)
            If (ChassisMasterBBColl.Count > 0) Then
                Return CType(ChassisMasterBBColl(0), ChassisMasterBB)
            End If
            Return New ChassisMasterBB
        End Function

        Public Function Retrieve(ByVal nTypeID As Integer, ByVal sCode As DateTime) As ChassisMasterBB
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "VechileColor.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(ChassisMasterBB), "ValidFrom", MatchType.Exact, sCode))

            Dim ChassisMasterBBColl As ArrayList = m_ChassisMasterBBMapper.RetrieveByCriteria(crit)
            If (ChassisMasterBBColl.Count > 0) Then
                Return CType(ChassisMasterBBColl(0), ChassisMasterBB)
            End If
            Return New ChassisMasterBB
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ChassisMasterBBMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ChassisMasterBBMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMasterBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterBBMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveByChassisNumbers(ByVal _arrID As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, _arrID))

            Dim ChassisMasterBBColl As ArrayList = m_ChassisMasterBBMapper.RetrieveByCriteria(criterias)

            Return ChassisMasterBBColl
        End Function
        Public Function RetrieveList() As ArrayList
            Return m_ChassisMasterBBMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMasterBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterBBMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMasterBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterBBMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ChassisMasterBB As ArrayList = m_ChassisMasterBBMapper.RetrieveByCriteria(criterias)
            Return _ChassisMasterBB
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ChassisMasterBBColl As ArrayList = m_ChassisMasterBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ChassisMasterBBColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMasterBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ChassisMasterBBColl As ArrayList = m_ChassisMasterBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ChassisMasterBBColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim ChassisMasterBBColl As ArrayList = m_ChassisMasterBBMapper.RetrieveByCriteria(criterias)

            Return ChassisMasterBBColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterBB), columnName, matchOperator, columnValue))
            Dim ChassisMasterBBColl As ArrayList = m_ChassisMasterBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ChassisMasterBBColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMasterBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), columnName, matchOperator, columnValue))

            Return m_ChassisMasterBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManagerInsert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is EndCustomer) Then
                CType(InsertArg.DomainObject, EndCustomer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EndCustomer).MarkLoaded()
                'ElseIf (TypeOf InsertArg.DomainObject Is ChassisMasterBBProfile) Then
                '    CType(InsertArg.DomainObject, ChassisMasterBBProfile).ID = InsertArg.ID
                '    CType(InsertArg.DomainObject, KTB.DNet.Domain.ChassisMasterBBProfile).MarkLoaded()
                'ElseIf (TypeOf InsertArg.DomainObject Is ChassisMasterBBProfileHistory) Then
                '    CType(InsertArg.DomainObject, ChassisMasterBBProfileHistory).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is Customer) Then
                CType(InsertArg.DomainObject, Customer).ID = InsertArg.ID
            End If
        End Sub

        Public Function Insert(ByVal objDomain As ChassisMasterBB) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_ChassisMasterBBMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ChassisMasterBB) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ChassisMasterBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As ChassisMasterBB)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = objDomain.RowStatus

                m_ChassisMasterBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteDB(ByVal objDomain As ChassisMasterBB) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = objDomain.RowStatus
                m_ChassisMasterBBMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As ChassisMasterBB)
            Try
                m_ChassisMasterBBMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ChassisMasterBB), "ChassisNumber", AggregateType.Count)

            Return CType(m_ChassisMasterBBMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function UpdateTransaction(ByVal objDomainColl As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    For Each item As ChassisMasterBB In objDomainColl
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        If Not IsNothing(item.EndCustomer) Then
                            m_TransactionManager.AddUpdate(item.EndCustomer, m_userPrincipal.Identity.Name)
                        End If
                    Next

                    m_TransactionManager.PerformTransaction()
                    returnValue = 1

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

        Public Function SynchronizeSAPToDNET(ByVal objChassisMasterBB As ChassisMasterBB) As Integer
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim orgCus As Customer = objChassisMasterBB.EndCustomer.OriginalCustomer
                    If Not orgCus Is Nothing Then
                        If orgCus.ID > 0 Then
                            m_TransactionManager.AddUpdate(orgCus, m_userPrincipal.Identity.Name)
                            orgCus.MarkLoaded()
                            objChassisMasterBB.EndCustomer.Customer = orgCus
                        Else
                            Dim objCustDB As Customer = GetCustomer(orgCus.Code)
                            If objCustDB.ID > 0 Then
                                orgCus = objCustDB
                            Else
                                m_TransactionManager.AddInsert(orgCus, m_userPrincipal.Identity.Name)
                                orgCus.MarkLoaded()
                            End If
                            objChassisMasterBB.EndCustomer.Customer = orgCus
                        End If
                    Else
                        m_TransactionManager.AddUpdate(objChassisMasterBB.EndCustomer.Customer, m_userPrincipal.Identity.Name)
                    End If

                    If objChassisMasterBB.EndCustomer.ID = 0 Then
                        objChassisMasterBB.EndCustomer.Name1 = objChassisMasterBB.EndCustomer.Customer.Name1
                        m_TransactionManager.AddInsert(objChassisMasterBB.EndCustomer, m_userPrincipal.Identity.Name)
                    Else
                        objChassisMasterBB.EndCustomer.Name1 = objChassisMasterBB.EndCustomer.Customer.Name1
                        m_TransactionManager.AddUpdate(objChassisMasterBB.EndCustomer, m_userPrincipal.Identity.Name)
                    End If
                    objChassisMasterBB.FakturStatus = CType(CType(EnumChassisMaster.FakturStatus.Selesai, Short), String)
                    objChassisMasterBB.AlreadySaled = 0
                    m_TransactionManager.AddUpdate(objChassisMasterBB, m_userPrincipal.Identity.Name & "Step3")
                    If Not IsNothing(objChassisMasterBB.EndCustomer) AndAlso Not IsNothing(objChassisMasterBB.EndCustomer.Customer) AndAlso Not IsNothing(objChassisMasterBB.EndCustomer.Customer.MyCustomerRequest) AndAlso objChassisMasterBB.EndCustomer.Customer.MyCustomerRequest.ID > 0 Then
                        If objChassisMasterBB.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP Then
                            objChassisMasterBB.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.VerifiedMCP
                            m_TransactionManager.AddUpdate(objChassisMasterBB.EndCustomer.Customer.MyCustomerRequest, m_userPrincipal.Identity.Name)
                        End If
                    End If
                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
        End Function

        Public Function AddCustomerToChassisMasterBB(ByVal objEndCustomer As EndCustomer, ByVal objChassisMasterBB As ChassisMasterBB) As Integer
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objEndCustomer, m_userPrincipal.Identity.Name)
                    objChassisMasterBB.EndCustomer = objEndCustomer
                    objChassisMasterBB.FakturStatus = "0"
                    m_TransactionManager.AddUpdate(objChassisMasterBB, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    objEndCustomer.ID = 0
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If

            Return objEndCustomer.ID
        End Function

        Public Function IsAreaViolationFree(ByVal objChassisMasterBB As ChassisMasterBB) As Boolean
            Dim returnVal As Boolean = False
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerTerritory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(DealerTerritory), "Dealer.ID", MatchType.Exact, objChassisMasterBB.Dealer.ID))
            crit.opAnd(New Criteria(GetType(DealerTerritory), "City.ID", MatchType.Exact, objChassisMasterBB.EndCustomer.Customer.City.ID))
            Dim aggr As Aggregate = New Aggregate(GetType(DealerTerritory), "ID", AggregateType.Count)
            Dim fac As DealerTerritoryFacade = New DealerTerritoryFacade(m_userPrincipal)
            returnVal = (fac.RetrieveScalar(aggr, crit) = 1)
            Return returnVal
        End Function

        Public Function ValidateInvoice(ByVal objChassisMasterBB As ChassisMasterBB) As Integer

            Me.DomainTypeCollection.Add(GetType(ChassisMasterBB))
            Me.DomainTypeCollection.Add(GetType(EndCustomer))
            Dim iReturnValue As Integer = 0
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddUpdate(objChassisMasterBB, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objChassisMasterBB.EndCustomer, m_userPrincipal.Identity.Name)
                    Dim objCust As Customer = objChassisMasterBB.EndCustomer.Customer
                    If Not objCust Is Nothing Then
                        objCust.DeletionMark = 1
                        m_TransactionManager.AddUpdate(objCust, m_userPrincipal.Identity.Name)
                    End If
                    m_TransactionManager.PerformTransaction()
                    iReturnValue = 2

                Catch ex As Exception
                    iReturnValue = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return iReturnValue

        End Function

        Public Function CancelInvoiceValidation(ByVal objChassisMasterBB As ChassisMasterBB) As Integer
            Me.DomainTypeCollection.Add(GetType(ChassisMasterBB))
            Me.DomainTypeCollection.Add(GetType(EndCustomer))
            Dim iReturnValue As Integer = 0
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddUpdate(objChassisMasterBB, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objChassisMasterBB.EndCustomer, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    iReturnValue = 2

                Catch ex As Exception
                    iReturnValue = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return iReturnValue
        End Function

        'backbone
        'Public Function GetChassisMasterBBProfile(ByVal obj As ChassisMasterBB, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As ChassisMasterBBProfile
        '    Dim objFacade As Profile.ChassisMasterBBProfileFacade = New Profile.ChassisMasterBBProfileFacade(System.Threading.Thread.CurrentPrincipal)
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBBProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(ChassisMasterBBProfile), "ChassisMasterBB.ID", MatchType.Exact, obj.ID))
        '    criterias.opAnd(New Criteria(GetType(ChassisMasterBBProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
        '    criterias.opAnd(New Criteria(GetType(ChassisMasterBBProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))
        '    Dim objList As ArrayList = objFacade.Retrieve(criterias)
        '    If objList.Count > 0 Then
        '        Return CType(objList(0), ChassisMasterBBProfile)
        '    End If
        '    Return New ChassisMasterBBProfile
        'End Function


        Public Function GetCustomer(ByVal code As String) As Customer
            Dim objFacade As CustomerFacade = New CustomerFacade(System.Threading.Thread.CurrentPrincipal)
            Dim objCust As Customer = objFacade.RetrieveByCode(code)
            Return objCust
        End Function

        'backbone
        'Public Function InsertUpdateChassisMasterBBProfile(ByVal objDomain As KTB.DNet.Domain.ChassisMasterBB, ByVal objListProfile As ArrayList, ByVal objGroup As ProfileGroup) As Integer
        '    Dim returnValue As Integer = -1
        '    Dim isProfileChange As Boolean
        '    If (Me.IsTaskFree()) Then
        '        Try
        '            Me.SetTaskLocking()
        '            Dim performTransaction As Boolean = True
        '            Dim ObjMapper As IMapper

        '            isProfileChange = False
        '            For Each item As ChassisMasterBBProfile In objListProfile
        '                item.ChassisMasterBB = objDomain
        '                Dim oldProfile As ChassisMasterBBProfile = GetChassisMasterBBProfile(objDomain, objGroup, item.ProfileHeader)
        '                If oldProfile.ID > 0 Then
        '                    For Each items As ChassisMasterBBProfileHistory In item.ChassisMasterBBProfileHistorys
        '                        items.ChassisMasterBBProfile = oldProfile
        '                        m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
        '                    Next

        '                    If oldProfile.ProfileValue <> item.ProfileValue Then
        '                        isProfileChange = True
        '                    End If

        '                    oldProfile.ProfileValue = item.ProfileValue
        '                    m_TransactionManager.AddUpdate(oldProfile, m_userPrincipal.Identity.Name)
        '                Else
        '                    item.ChassisMasterBB = objDomain
        '                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
        '                    For Each items As ChassisMasterBBProfileHistory In item.ChassisMasterBBProfileHistorys
        '                        items.ChassisMasterBBProfile = item
        '                        m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
        '                    Next
        '                    oldProfile.ProfileValue = item.ProfileValue
        '                    isProfileChange = True
        '                End If

        '            Next

        '            If isProfileChange Then
        '                objDomain.LastUpdateProfile = Now
        '            End If

        '            m_TransactionManager.AddUpdate(objDomain.EndCustomer, m_userPrincipal.Identity.Name)
        '            m_TransactionManager.AddUpdate(objDomain.EndCustomer.Customer, m_userPrincipal.Identity.Name)
        '            m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
        '            If performTransaction Then
        '                m_TransactionManager.PerformTransaction()
        '                returnValue = 0
        '            End If
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
#End Region

#Region "Custom Method"

        Public Function RetrieveActiveListByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(ChassisMasterBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ChassisMasterBBMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function IsExist(ByVal ChassisNumber As String) As Boolean
            Dim bReturnValue As Boolean = False
            Dim objCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            objCriteria.opAnd(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, ChassisNumber))

            Dim objAggregate As Aggregate = New Aggregate(GetType(ChassisMasterBB), "ID", AggregateType.Count)

            bReturnValue = (m_ChassisMasterBBMapper.RetrieveScalar(objAggregate, objCriteria) = 1)

            Return bReturnValue
        End Function

        Public Function RetrieveByField(ByVal Field As String, ByVal Code As String) As ChassisMasterBB
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), Field, MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ChassisMasterBBColl As ArrayList = m_ChassisMasterBBMapper.RetrieveByCriteria(criterias)
            If (ChassisMasterBBColl.Count > 0) Then
                Return CType(ChassisMasterBBColl(0), ChassisMasterBB)
            End If
            Return New ChassisMasterBB
        End Function

#End Region

    End Class
End Namespace
