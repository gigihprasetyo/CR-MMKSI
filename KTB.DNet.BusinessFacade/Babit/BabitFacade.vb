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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 9/26/2005 - 1:43:31 PM
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
Imports KTB.Dnet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.BabitSalesComm

    Public Class BabitFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitMapper As IMapper
        Private m_BabitAllocationMapper As IMapper
        Private m_DealerMapper As IMapper
        Private m_TransactionManager As TransactionManager
        Private m_BabitPaymentMapper As IMapper
        Private m_BabitProposalMapper As IMapper
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_BabitMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.Babit).ToString)
            Me.m_BabitAllocationMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.BabitAllocation).ToString)
            Me.m_BabitPaymentMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.BabitPayment).ToString)
            Me.m_BabitProposalMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.BabitProposal).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.Babit))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BabitAllocation))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BabitPayment))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BabitProposal))
        End Sub

#End Region

#Region "Retrieve"

#Region "Babit Allocation"
        Public Function RetrieveBabitAllocation(ByVal ID As Integer) As BabitAllocation
            Return CType(m_BabitAllocationMapper.Retrieve(ID), BabitAllocation)
        End Function
        Public Function RetrieveBabitAllocation(ByVal Code As String) As BabitAllocation
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitAllocation), "NoPerjanjian", MatchType.Exact, Code))

            Dim BabitAllocColl As ArrayList = m_BabitAllocationMapper.RetrieveByCriteria(criterias)
            If (BabitAllocColl.Count > 0) Then
                Return CType(BabitAllocColl(0), BabitAllocation)
            End If
            Return New BabitAllocation
        End Function

        Public Function RetrieveBabitPayments(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitPaymentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveBabitAllocation(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitAllocationMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveBabitProposal(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitProposalMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveBabitAllocation(ByVal criterias As ICriteria, ByVal SortColumn As String, ByVal sortDirection As String) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(BabitAllocation), SortColumn, sortDirection))

            Return m_BabitAllocationMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveBabitPayments(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim arlResult As ArrayList = m_BabitPaymentMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return arlResult
        End Function

        Public Function RetrieveBabitAlocationByNoPerjanjian(ByVal NoPerjanjian As String) As BabitAllocation
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitAllocation), "NoPerjanjian", MatchType.Exact, NoPerjanjian))

            Dim BabitAllocationColl As ArrayList = m_BabitAllocationMapper.RetrieveByCriteria(criterias)
            If (BabitAllocationColl.Count > 0) Then
                Return CType(BabitAllocationColl(0), BabitAllocation)
            End If
            Return New BabitAllocation
        End Function
        Public Function RetrieveBabitAllocationByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(BabitAllocation), SortColumn, sortDirection))

            Dim BabitAllocationColl As ArrayList = m_BabitAllocationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitAllocationColl
        End Function
        Public Function RetrieveListBabitAllocation(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitAllocationMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function UpdateBabitAllocation(ByVal objDomain As BabitAllocation) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitAllocationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function
        Public Function UpdateTransactionBabitAllocation(ByVal arrBabitAllocation As ArrayList) As Integer
            'return value if success is 1, not the id created 
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrBabitAllocation.Count > 0 Then
                        For Each item As BabitAllocation In arrBabitAllocation
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                m_TransactionManager.AddUpdate(item.Babit, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item.Babit, m_userPrincipal.Identity.Name)
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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
        Public Function InsertTransactionBabitAllocation(ByVal arrBabitAllocation As ArrayList) As Integer
            'return value if success is 1, not the id created 
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrBabitAllocation.Count > 0 Then
                        For Each item As BabitAllocation In arrBabitAllocation
                            m_TransactionManager.AddInsert(item.Babit, m_userPrincipal.Identity.Name)
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

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

        Public Sub DeleteBabitAllocation(ByVal objDomain As BabitAllocation)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                'For Each item As BabitAllocation In objDomain.BabitAllocations
                '    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                'Next
                UpdateBabitAllocation(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


#End Region

        Public Function Retrieve(ByVal ID As Integer) As Babit
            Return CType(m_BabitMapper.Retrieve(ID), Babit)
        End Function

        Public Function Retrieve(ByVal Code As String) As Babit
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Babit), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Babit), "PKNumber", MatchType.Exact, Code))

            Dim BabitColl As ArrayList = m_BabitMapper.RetrieveByCriteria(criterias)
            If (BabitColl.Count > 0) Then
                Return CType(BabitColl(0), Babit)
            End If
            Return New Babit
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(Babit), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(Babit), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Babit), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Babit As ArrayList = m_BabitMapper.RetrieveByCriteria(criterias)
            Return _Babit
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Babit), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitColl As ArrayList = m_BabitMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Babit), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim BabitColl As ArrayList = m_BabitMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitColl
        End Function

        Public Function RetrieveBabitProposalActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim arlResult As ArrayList = m_BabitProposalMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return arlResult
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Babit), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Babit), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitColl As ArrayList = m_BabitMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Babit), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitColl As ArrayList = m_BabitMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(Babit), columnName, matchOperator, columnValue))
            Return BabitColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(Babit), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Babit), columnName, matchOperator, columnValue))

            Return m_BabitMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function



#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Babit), "BabitCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(Babit), "BabitCode", AggregateType.Count)
            Return CType(m_BabitMapper.RetrieveScalar(agg, crit), Integer)
        End Function



        Public Sub Update(ByVal objDomain As Babit)
            Try
                m_BabitMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.Babit) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each item As BabitAllocation In objDomain.BabitAllocations
                        item.Babit = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.Babit) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.Babit).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.Babit).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is BabitAllocation) Then

                CType(InsertArg.DomainObject, BabitAllocation).ID = InsertArg.ID

            End If

        End Sub

        Public Sub Delete(ByVal objDomain As Babit)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                For Each item As BabitAllocation In objDomain.BabitAllocations
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next
                UpdateTransaction(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.Babit) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As BabitAllocation In objDomain.BabitAllocations
                        item.Babit = objDomain
                        If item.ID <> 0 Then
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
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

#End Region

#Region "Custom Method"
        Public Function RilisBabitAllocation(ByVal BabitAllocationList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    For Each item As BabitAllocation In BabitAllocationList
                        item.Status = EnumBabit.BabitAllocationStatus.Rilis
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


#End Region

    End Class

End Namespace
