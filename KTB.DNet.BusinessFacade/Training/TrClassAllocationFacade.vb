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
'// Generated on 11/14/2005 - 10:42:45 AM
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

Namespace KTB.DNet.BusinessFacade.Training
    Public Class TrClassAllocationFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrClassAllocationMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_TrClassAllocationMapper = MapperFactory.GetInstance.GetMapper(GetType(TrClassAllocation).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.TrClass))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.Dealer))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrClassAllocation
            Return CType(m_TrClassAllocationMapper.Retrieve(ID), TrClassAllocation)
        End Function

        Public Function Retrieve(ByVal Code As String) As TrClassAllocation
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClassAllocationCode", MatchType.Exact, Code))

            Dim TrClassAllocationColl As ArrayList = m_TrClassAllocationMapper.RetrieveByCriteria(criterias)
            If (TrClassAllocationColl.Count > 0) Then
                Return CType(TrClassAllocationColl(0), TrClassAllocation)
            End If
            Return New TrClassAllocation
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrClassAllocationMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrClassAllocationMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrClassAllocationMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrClassAllocationMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrClassAllocationMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrClassAllocation As ArrayList = m_TrClassAllocationMapper.RetrieveByCriteria(criterias)
            Return _TrClassAllocation
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrClassAllocationColl As ArrayList = m_TrClassAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrClassAllocationColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrClassColl As ArrayList = m_TrClassAllocationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return TrClassColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim TrClassAllocationColl As ArrayList = m_TrClassAllocationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrClassAllocationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim TrClassAllocationColl As ArrayList = m_TrClassAllocationMapper.RetrieveByCriteria(criterias, sortColl)
            Return TrClassAllocationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrClassAllocationColl As ArrayList = m_TrClassAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrClassAllocationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrClassAllocationColl As ArrayList = m_TrClassAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrClassAllocation), columnName, matchOperator, columnValue))
            Return TrClassAllocationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), columnName, matchOperator, columnValue))

            Return m_TrClassAllocationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByClassCode(ByVal ClassCode As String) As ArrayList
            Dim ClassAllocationColl As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ClassCode", MatchType.Exact, ClassCode))
            ClassAllocationColl = m_TrClassAllocationMapper.RetrieveByCriteria(criterias)
            If ClassAllocationColl.Count > 0 Then
                Return ClassAllocationColl
            End If
            Return New ArrayList
        End Function

        Public Function RetrieveScalar(ByVal Criterias As ICriteria, ByVal aggregate As Aggregate) As Integer
            Dim obj As Object = m_TrClassAllocationMapper.RetrieveScalar(aggregate, Criterias)
            If obj Is DBNull.Value Then
                Return 0
            Else
                Return CInt(obj)
            End If
        End Function


#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal classCode As String, ByVal dealerCode As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "TrClass.ClassCode", MatchType.Exact, classCode))
            crit.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer.DealerCode", MatchType.Exact, dealerCode))

            Dim agg As Aggregate = New Aggregate(GetType(TrClassAllocation), "ID", AggregateType.Count)

            Return CType(m_TrClassAllocationMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TrClassAllocation) As Integer
            Dim iReturn As Integer = -2
            Try
                m_TrClassAllocationMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As TrClassAllocation) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrClassAllocationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As TrClassAllocation) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrClassAllocationMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        'Private Function DealerInClassCriteria(ByVal ClassID As Integer) As CriteriaComposite
        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", _
        '        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, ClassID))
        '    Return criterias
        'End Function

        'Private Function GetAllAllocationDealer(ByVal ClassID As Integer) As ArrayList
        '    Dim arlAllocation As ArrayList = Retrieve(DealerInClassCriteria(ClassID))
        '    Return arlAllocation
        'End Function

        Public Function UpdateAllocation(ByVal AllAllocationDealerColl As ArrayList, _
            ByVal AllocationToProcessColl As ArrayList, ByVal ClassToAllocated As TrClass) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    '1.Get all data in db (dealer) that already allocated to current class
                    'Dim arlAllAlocationDealerColl As ArrayList = _
                    '    GetAllAllocationDealer(ClassToAllocated.ID)

                    '2.Delete that data
                    'For Each objAllAllocationDealer As TrClassAllocation In AllAllocationDealerColl
                    '    m_TransactionManager.AddDelete(objAllAllocationDealer)
                    'Next

                    '3.Insert to new data
                    For Each objClassAllocation As TrClassAllocation In AllocationToProcessColl
                        If objClassAllocation.ID > 0 Then
                            m_TransactionManager.AddUpdate(objClassAllocation, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(objClassAllocation, m_userPrincipal.Identity.Name)
                        End If

                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                    End If
                    returnValue = 1
                Catch ex As Exception
                    returnValue = -1
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

        Public Function GetAggregateResult(ByVal aggregate As IAggregate, ByVal criteria As ICriteria) As Decimal
            Dim result As Object = m_TrClassAllocationMapper.RetrieveScalar(aggregate, criteria)
            If result Is System.DBNull.Value Then
                Return 0
            Else
                Return CType(result, Decimal)
            End If
        End Function

#End Region

#Region "Custom Method"
        Public Function RetrieveTrClassAllocationByTraineeID(ByVal traineeID As Integer) As ArrayList
            Dim SQL As String
            If traineeID > 0 Then
                SQL = "exec sp_GetClassAllocationByTrainee " & traineeID
            End If

            Return m_TrClassAllocationMapper.RetrieveSP(SQL)
        End Function
#End Region

    End Class

End Namespace



