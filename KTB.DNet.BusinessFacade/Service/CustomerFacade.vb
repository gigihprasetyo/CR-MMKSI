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
'// Generated on 7/26/2007 - 11:51:06 AM
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

    Public Class CustomerFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CustomerMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"
        Dim m_FleetCustomerHeaderMapper As Object

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_CustomerMapper = MapperFactory.GetInstance.GetMapper(GetType(Customer).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.Customer))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.CustomerDealer))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As Customer
            Return CType(m_CustomerMapper.Retrieve(ID), Customer)
        End Function

        Public Function RetrieveCustReq(ByVal Code As String) As Customer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), "Code", MatchType.Exact, Code))
            Dim CustomerColl As ArrayList = m_CustomerMapper.RetrieveByCriteria(criterias)
            If (CustomerColl.Count > 0) Then
                Return CType(CustomerColl(0), Customer)
            End If
            Return New Customer
        End Function


        Public Function Retrieve(ByVal Code As String) As Customer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Customer), "Code", MatchType.Exact, Code))

            Dim CustomerColl As ArrayList = m_CustomerMapper.RetrieveByCriteria(criterias)
            If (CustomerColl.Count > 0) Then
                Return CType(CustomerColl(0), Customer)
            End If
            Return New Customer
        End Function



        Public Function RetrieveByCode(ByVal Code As String) As Customer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), "Code", MatchType.Exact, Code))
            Dim CustomerColl As ArrayList = m_CustomerMapper.RetrieveByCriteria(criterias)
            If (CustomerColl.Count > 0) Then
                Return CType(CustomerColl(0), Customer)
            End If
            Return New Customer
        End Function
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CustomerMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CustomerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CustomerMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Customer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Customer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Customer As ArrayList = m_CustomerMapper.RetrieveByCriteria(criterias)
            Return _Customer
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CustomerColl As ArrayList = m_CustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CustomerColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_CustomerMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CustomerColl As ArrayList = m_CustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CustomerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CustomerColl As ArrayList = m_CustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(Customer), columnName, matchOperator, columnValue))
            Return CustomerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Customer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), columnName, matchOperator, columnValue))

            Return m_CustomerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(Customer), sortColumn, sortDirection))
            Else
                'sortColl = Nothing
                sortColl.Add(New Sort(GetType(Customer), "Code", Sort.SortDirection.DESC))

            End If

            Dim CustomerColl As ArrayList = m_CustomerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CustomerColl
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), "CustomerCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(Customer), "CustomerCode", AggregateType.Count)
            Return CType(m_CustomerMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.Customer) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.Customer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.Customer).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is CustomerDealer) Then
                CType(InsertArg.DomainObject, CustomerDealer).ID = InsertArg.ID
            End If


        End Sub

#End Region

#Region "Custom Method"

        Private Function GetCustomerDealer(ByVal objDealer As Dealer, ByVal objCustomer As Customer) As CustomerDealer
            Dim objCustmerDealerFacade As CustomerDealerFacade = New CustomerDealerFacade(m_userPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.ID", MatchType.Exact, objCustomer.ID))
            criterias.opAnd(New Criteria(GetType(CustomerDealer), "Dealer.ID", MatchType.Exact, objDealer.ID))
            Dim list As ArrayList = objCustmerDealerFacade.Retrieve(criterias)
            If list.Count > 0 Then
                Return CType(list(0), CustomerDealer)
            Else
                Return Nothing
            End If
        End Function

        Public Function Insert(ByVal objDomain As Customer) As Long
            Dim returnVal As Long = -1
            Dim _user As String
            If (Me.IsTaskFree() OrElse 1 = 1) Then
                Try
                    '  Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    Dim oldCustomer As Customer = Me.RetrieveCustReq(objDomain.Code)
                    Dim objCustDealer As CustomerDealer = New CustomerDealer
                    If oldCustomer.ID > 0 Then

                        oldCustomer.IsChangedWSM = False

                        If oldCustomer.Code <> objDomain.Code Then
                            oldCustomer.Code = objDomain.Code
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.Name1 <> objDomain.Name1 Then
                            oldCustomer.Name1 = objDomain.Name1
                            oldCustomer.IsChangedWSM = True
                        End If


                        If oldCustomer.Name2 <> objDomain.Name2 Then
                            oldCustomer.Name2 = objDomain.Name2
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.Name3 <> objDomain.Name3 Then
                            oldCustomer.Name3 = objDomain.Name3
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.Alamat <> objDomain.Alamat Then
                            oldCustomer.Alamat = objDomain.Alamat
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.Kelurahan <> objDomain.Kelurahan Then
                            oldCustomer.Kelurahan = objDomain.Kelurahan
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.Kecamatan <> objDomain.Kecamatan Then
                            oldCustomer.Kecamatan = objDomain.Kecamatan
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.PostalCode <> objDomain.PostalCode Then
                            oldCustomer.PostalCode = objDomain.PostalCode
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.City.ID <> objDomain.City.ID Then
                            oldCustomer.City = objDomain.City
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.PreArea <> objDomain.PreArea Then
                            oldCustomer.PreArea = objDomain.PreArea
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.City.ID <> objDomain.City.ID Then
                            oldCustomer.City = objDomain.City
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.Email <> objDomain.Email Then
                            oldCustomer.Email = objDomain.Email
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.PhoneNo <> objDomain.PhoneNo Then
                            oldCustomer.PhoneNo = objDomain.PhoneNo
                            oldCustomer.IsChangedWSM = True
                        End If

                        If oldCustomer.PrintRegion <> objDomain.PrintRegion Then
                            oldCustomer.PrintRegion = objDomain.PrintRegion
                            oldCustomer.IsChangedWSM = True
                        End If


                        objCustDealer.Customer = oldCustomer
                        If objDomain.RowStatus = DBRowStatus.Deleted Then
                            If oldCustomer.RowStatus <> objDomain.RowStatus Then
                                oldCustomer.RowStatus = objDomain.RowStatus
                                oldCustomer.IsChangedWSM = True
                            End If
                            If oldCustomer.IsChangedWSM Then
                                m_TransactionManager.AddUpdate(oldCustomer, m_userPrincipal.Identity.Name)
                            End If
                        Else
                            If oldCustomer.RowStatus = DBRowStatus.Deleted Then
                                If oldCustomer.RowStatus <> objDomain.RowStatus Then
                                    oldCustomer.RowStatus = DBRowStatus.Active
                                    oldCustomer.IsChangedWSM = True
                                End If
                                If oldCustomer.IsChangedWSM Then
                                    m_TransactionManager.AddUpdate(oldCustomer, m_userPrincipal.Identity.Name)
                                End If
                            Else
                                'confirm by peggy
                                'If oldCustomer.Status = EnumStatusCustomer.ReadyForUpdate.Yes Then
                                '    oldCustomer.Status = EnumStatusCustomer.ReadyForUpdate.No
                                '    m_TransactionManager.AddUpdate(oldCustomer, m_userPrincipal.Identity.Name)
                                'End If
                                If oldCustomer.IsChangedWSM Then
                                    m_CustomerMapper.Update(oldCustomer, m_userPrincipal.Identity.Name)
                                    'm_TransactionManager.AddUpdate(oldCustomer, m_userPrincipal.Identity.Name)
                                End If
                            End If
                        End If
                    Else
                        If objDomain.RowStatus <> DBRowStatus.Deleted Then
                            objDomain.MarkLoaded()
                            objCustDealer.Customer = objDomain
                            objDomain.Status = EnumStatusCustomer.ReadyForUpdate.No
                            'm_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                            Dim xID As Int32 = m_CustomerMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                            objDomain.ID = xID
                        End If
                    End If

                    Dim custReq As CustomerRequest = objDomain.MyCustomerRequest
                    If Not custReq Is Nothing Then
                        If custReq.ID > 0 Then
                            custReq.IsChangedWSM = False

                            If custReq.CustomerCode <> objDomain.Code Then
                                custReq.CustomerCode = objDomain.Code
                                custReq.IsChangedWSM = True
                            End If

                            If custReq.Status <> EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Selesai Then
                                custReq.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Selesai
                                custReq.IsChangedWSM = True
                            End If

                            Dim objCustomerDealer As CustomerDealer
                            If oldCustomer.ID > 0 Then
                                objCustomerDealer = GetCustomerDealer(custReq.Dealer, oldCustomer)
                                If objCustomerDealer Is Nothing Then
                                    Dim obj As CustomerDealer = New CustomerDealer
                                    obj.Customer = oldCustomer
                                    obj.Dealer = custReq.Dealer
                                    m_TransactionManager.AddInsert(obj, m_userPrincipal.Identity.Name)
                                End If
                            Else
                                objCustomerDealer = New CustomerDealer
                                objCustomerDealer.Customer = objDomain
                                objCustomerDealer.Dealer = custReq.Dealer
                                m_TransactionManager.AddInsert(objCustomerDealer, m_userPrincipal.Identity.Name)
                            End If
                            If custReq.IsChangedWSM Then
                                m_TransactionManager.AddUpdate(custReq, m_userPrincipal.Identity.Name)
                            End If
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = objDomain.ID
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
            Return returnVal
        End Function

        Public Function UpdateList(ByVal objDomains As ArrayList) As Integer
            Dim returnVal As Short = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    For Each item As Customer In objDomains
                        item.Status = EnumStatusCustomer.ReadyForUpdate.Yes
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
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
            Return returnVal

        End Function


        Public Function DeleteList(ByVal objDomains As ArrayList) As Integer
            Dim returnVal As Short = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    For Each item As Customer In objDomains
                        item.RowStatus = DBRowStatus.Deleted
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
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
            Return returnVal

        End Function
#End Region

    End Class

End Namespace
