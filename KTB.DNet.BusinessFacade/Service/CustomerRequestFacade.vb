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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 7/18/2007 - 2:19:39 PM
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
Imports KTB.DNet.BusinessFacade.Profile

#End Region

Namespace KTB.DNet.BusinessFacade.Service

    Public Class CustomerRequestFacade

        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CustomerRequestMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_CustomerRequestMapper = MapperFactory.GetInstance.GetMapper(GetType(CustomerRequest).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.CustomerRequest))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.CustomerRequestProfile))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.CustomerRequestProfileHistory))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CustomerRequest
            Return CType(m_CustomerRequestMapper.Retrieve(ID), CustomerRequest)
        End Function

        Public Function Retrieve(ByVal Code As String) As CustomerRequest
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "CustomerRequestCode", MatchType.Exact, Code))

            Dim CustomerRequestColl As ArrayList = m_CustomerRequestMapper.RetrieveByCriteria(criterias)
            If (CustomerRequestColl.Count > 0) Then
                Return CType(CustomerRequestColl(0), CustomerRequest)
            End If
            Return New CustomerRequest
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CustomerRequestMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CustomerRequestMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CustomerRequestMapper.RetrieveList
        End Function
        Public Function RetrieveCodeDesc(ByVal Code As String) As CustomerRequest
            Dim sortColl As SortCollection = New SortCollection
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "CustomerCode", MatchType.Exact, Code))
            sortColl.Add(New Sort(GetType(CustomerRequest), "ProcessDate", Sort.SortDirection.DESC))
            Dim CustomerRequestColl As ArrayList = m_CustomerRequestMapper.RetrieveByCriteria(criterias, sortColl)
            If (CustomerRequestColl.Count > 0) Then
                Return CType(CustomerRequestColl(0), CustomerRequest)
            End If
            Return New CustomerRequest
        End Function
        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerRequest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerRequestMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerRequest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerRequestMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _CustomerRequest As ArrayList = m_CustomerRequestMapper.RetrieveByCriteria(criterias)
            Return _CustomerRequest
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CustomerRequestColl As ArrayList = m_CustomerRequestMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CustomerRequestColl
        End Function

        Public Function RetrieveByRefRequestNo(ByVal RequestNo As String, ByVal _DealerID As Integer) As CustomerRequest
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "RequestNo", MatchType.Exact, RequestNo))
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "Dealer.ID", MatchType.Exact, _DealerID))

            Dim CustomerRequestColl As ArrayList = m_CustomerRequestMapper.RetrieveByCriteria(criterias)
            If (CustomerRequestColl.Count > 0) Then
                Return CType(CustomerRequestColl(0), CustomerRequest)
            End If
            Return New CustomerRequest
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(CustomerRequest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim CustomerRequestColl As ArrayList = m_CustomerRequestMapper.RetrieveByCriteria(criterias, sortColl)
            Return CustomerRequestColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(CustomerRequest), sortColumn, sortDirection))
            Else
                sortColl.Add(New Sort(GetType(CustomerRequest), "CustomerCode", sortDirection.DESC))
            End If

            Dim CustomerRequestColl As ArrayList = m_CustomerRequestMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CustomerRequestColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CustomerRequestColl As ArrayList = m_CustomerRequestMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(CustomerRequest), columnName, matchOperator, columnValue))
            Return CustomerRequestColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerRequest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), columnName, matchOperator, columnValue))

            Return m_CustomerRequestMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Need To Add"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "CustomerRequestCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(CustomerRequest), "CustomerRequestCode", AggregateType.Count)
            Return CType(m_CustomerRequestMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function AssignedNewRequest(ByVal objCR As KTB.DNet.Domain.CustomerRequest, ByVal objCD As KTB.DNet.Domain.CustomerDealer) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim CD As New ArrayList
                    Dim facade As CustomerDealerFacade = New CustomerDealerFacade(m_userPrincipal)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.ID", MatchType.Exact, objCD.Customer.ID))
                    criterias.opAnd(New Criteria(GetType(CustomerDealer), "Dealer.ID", MatchType.Exact, objCD.Dealer.ID))

                    CD = facade.Retrieve(criterias)
                    If (CD.Count > 0) Then
                        m_TransactionManager.AddUpdate(objCD, m_userPrincipal.Identity.Name)
                    Else
                        m_TransactionManager.AddInsert(objCD, m_userPrincipal.Identity.Name)
                    End If
                    m_TransactionManager.AddUpdate(objCR, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objCR.ID
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

        Public Function UpdateRevisiRequest(ByVal objCR As KTB.DNet.Domain.CustomerRequest, ByVal arlAdd As ArrayList, ByVal arlUpdate As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arlAdd.Count > 0 Then
                        For Each objCPAdd As CustomerProfile In arlAdd
                            m_TransactionManager.AddInsert(objCPAdd, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arlUpdate.Count > 0 Then
                        For Each objCPUpdate As CustomerProfile In arlUpdate
                            m_TransactionManager.AddUpdate(objCPUpdate, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objCR, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objCR.ID
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

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.CustomerRequest, ByVal objListProfile As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each item As CustomerRequestProfile In objListProfile
                        item.CustomerRequest = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        For Each items As CustomerRequestProfileHistory In item.CustomerRequestProfileHistorys
                            items.CustomerRequestProfile = item
                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        Next
                    Next
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
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

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="objDomain"></param>
        ''' <param name="objListProfile"></param>
        ''' <param name="pForSPKDetailCustomer">Set True if objListProfile is SPKDetailCustomerProfile</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InsertFromSPKCustomer(ByVal objDomain As KTB.DNet.Domain.CustomerRequest, ByVal objListProfile As ArrayList, Optional ByVal pForSPKDetailCustomer As Boolean = False) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If pForSPKDetailCustomer Then
                        For Each item As SPKDetailCustomerProfile In objListProfile
                            Dim _custReqProfile As New CustomerRequestProfile
                            _custReqProfile.CustomerRequest = objDomain
                            _custReqProfile.ProfileGroup = item.ProfileGroup
                            _custReqProfile.ProfileHeader = item.ProfileHeader
                            _custReqProfile.ProfileValue = item.ProfileValue

                            m_TransactionManager.AddInsert(_custReqProfile, m_userPrincipal.Identity.Name)

                        Next
                    Else
                        For Each item As SPKCustomerProfile In objListProfile
                            Dim _custReqProfile As New CustomerRequestProfile
                            _custReqProfile.CustomerRequest = objDomain
                            _custReqProfile.ProfileGroup = item.ProfileGroup
                            _custReqProfile.ProfileHeader = item.ProfileHeader
                            _custReqProfile.ProfileValue = item.ProfileValue

                            m_TransactionManager.AddInsert(_custReqProfile, m_userPrincipal.Identity.Name)

                        Next
                    End If
                  
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
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

        Public Function GetCustomerRequestProfile(ByVal objCustomerRequest As CustomerRequest, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As CustomerRequestProfile
            Dim objFacade As CustomerRequestProfileFacade = New CustomerRequestProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequestProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerRequestProfile), "CustomerRequest.ID", MatchType.Exact, objCustomerRequest.ID))
            criterias.opAnd(New Criteria(GetType(CustomerRequestProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(CustomerRequestProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))
            Dim objListCustomerRequestProfile As ArrayList = objFacade.Retrieve(criterias)
            If objListCustomerRequestProfile.Count > 0 Then
                Return CType(objListCustomerRequestProfile(0), CustomerRequestProfile)
            End If
            Return New CustomerRequestProfile
        End Function


        Public Function Update(ByVal objDomain As KTB.DNet.Domain.CustomerRequest, ByVal objListProfile1 As ArrayList, ByVal objListProfile2 As ArrayList, ByVal objListProfile3 As ArrayList, ByVal objListProfile4 As ArrayList, ByVal objListProfile5 As ArrayList, ByVal objGroup1 As ProfileGroup, ByVal objGroup2 As ProfileGroup, ByVal objGroup3 As ProfileGroup, ByVal objGroup4 As ProfileGroup, ByVal objGroup5 As ProfileGroup) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    For Each item As CustomerRequestProfile In objListProfile1
                        item.CustomerRequest = objDomain
                        Dim oldProfile1 As CustomerRequestProfile = GetCustomerRequestProfile(objDomain, objGroup1, item.ProfileHeader)
                        If oldProfile1.ID > 0 Then
                            For Each items As CustomerRequestProfileHistory In item.CustomerRequestProfileHistorys
                                items.CustomerRequestProfile = oldProfile1
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile1.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile1, m_userPrincipal.Identity.Name)
                        Else
                            item.CustomerRequest = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            For Each items As CustomerRequestProfileHistory In item.CustomerRequestProfileHistorys
                                items.CustomerRequestProfile = item
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile1.ProfileValue = item.ProfileValue
                        End If

                    Next


                    For Each item As CustomerRequestProfile In objListProfile2
                        item.CustomerRequest = objDomain
                        Dim oldProfile2 As CustomerRequestProfile = GetCustomerRequestProfile(objDomain, objGroup2, item.ProfileHeader)
                        If oldProfile2.ID > 0 Then
                            For Each items As CustomerRequestProfileHistory In item.CustomerRequestProfileHistorys
                                items.CustomerRequestProfile = oldProfile2
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile2.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile2, m_userPrincipal.Identity.Name)
                        Else
                            item.CustomerRequest = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            For Each items As CustomerRequestProfileHistory In item.CustomerRequestProfileHistorys
                                items.CustomerRequestProfile = item
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile2.ProfileValue = item.ProfileValue
                        End If

                    Next


                    For Each item As CustomerRequestProfile In objListProfile3
                        item.CustomerRequest = objDomain
                        Dim oldProfile3 As CustomerRequestProfile = GetCustomerRequestProfile(objDomain, objGroup3, item.ProfileHeader)
                        If oldProfile3.ID > 0 Then
                            For Each items As CustomerRequestProfileHistory In item.CustomerRequestProfileHistorys
                                items.CustomerRequestProfile = oldProfile3
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile3.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile3, m_userPrincipal.Identity.Name)
                        Else
                            item.CustomerRequest = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            For Each items As CustomerRequestProfileHistory In item.CustomerRequestProfileHistorys
                                items.CustomerRequestProfile = item
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile3.ProfileValue = item.ProfileValue
                        End If
                    Next

                    For Each item As CustomerRequestProfile In objListProfile4
                        item.CustomerRequest = objDomain
                        Dim oldProfile4 As CustomerRequestProfile = GetCustomerRequestProfile(objDomain, objGroup4, item.ProfileHeader)
                        If oldProfile4.ID > 0 Then
                            For Each items As CustomerRequestProfileHistory In item.CustomerRequestProfileHistorys
                                items.CustomerRequestProfile = oldProfile4
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile4.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile4, m_userPrincipal.Identity.Name)
                        Else
                            item.CustomerRequest = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            For Each items As CustomerRequestProfileHistory In item.CustomerRequestProfileHistorys
                                items.CustomerRequestProfile = item
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile4.ProfileValue = item.ProfileValue
                        End If
                    Next

                    For Each item As CustomerRequestProfile In objListProfile5
                        item.CustomerRequest = objDomain
                        Dim oldProfile5 As CustomerRequestProfile = GetCustomerRequestProfile(objDomain, objGroup5, item.ProfileHeader)
                        If oldProfile5.ID > 0 Then
                            For Each items As CustomerRequestProfileHistory In item.CustomerRequestProfileHistorys
                                items.CustomerRequestProfile = oldProfile5
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile5.ProfileValue = item.ProfileValue
                            m_TransactionManager.AddUpdate(oldProfile5, m_userPrincipal.Identity.Name)
                        Else
                            item.CustomerRequest = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            For Each items As CustomerRequestProfileHistory In item.CustomerRequestProfileHistorys
                                items.CustomerRequestProfile = item
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile5.ProfileValue = item.ProfileValue
                        End If
                    Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
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

        Public Function UpdateTransaction(ByVal objDomainColl As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each item As CustomerRequest In objDomainColl
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
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

        Public Function Insert(ByVal objDomain As CustomerRequest) As Integer
            Dim iReturn As Integer = -2
            Try
                m_CustomerRequestMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                iReturn = objDomain.ID
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As CustomerRequest) As Integer
            Dim nResult As Integer = -1
            Try
                m_CustomerRequestMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                nResult = objDomain.ID
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As CustomerRequest)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_CustomerRequestMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As CustomerRequest) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_CustomerRequestMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        '24/Jul/2007 Deddy H    keperluan aggregate value
        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Decimal
            Dim returnValue As Decimal = 0
            Try
                returnValue = CType(m_CustomerRequestMapper.RetrieveScalar(aggr, crit), Decimal)
            Catch ex As Exception
                returnValue = 0
            End Try
            Return returnValue
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.CustomerRequest) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.CustomerRequest).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.CustomerRequest).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is CustomerRequestProfile) Then
                CType(InsertArg.DomainObject, CustomerRequestProfile).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.CustomerRequestProfile).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is CustomerRequestProfileHistory) Then
                CType(InsertArg.DomainObject, CustomerRequestProfileHistory).ID = InsertArg.ID
            End If
        End Sub
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


