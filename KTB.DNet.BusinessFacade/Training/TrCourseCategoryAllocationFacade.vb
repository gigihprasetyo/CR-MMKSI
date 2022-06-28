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
    Public Class TrCourseCategoryAllocationFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrCourseCategoryAllocationMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_TrCourseCategoryAllocationMapper = MapperFactory.GetInstance.GetMapper(GetType(TrCourseCategoryAllocation).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.TrClass))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.Dealer))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrCourseCategoryAllocation
            Return CType(m_TrCourseCategoryAllocationMapper.Retrieve(ID), TrCourseCategoryAllocation)
        End Function

        Public Function Retrieve(ByVal Code As String) As TrCourseCategoryAllocation
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrCourseCategoryAllocation), "TrCourseCategoryAllocationCode", MatchType.Exact, Code))

            Dim TrCourseCategoryAllocationColl As ArrayList = m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias)
            If (TrCourseCategoryAllocationColl.Count > 0) Then
                Return CType(TrCourseCategoryAllocationColl(0), TrCourseCategoryAllocation)
            End If
            Return New TrCourseCategoryAllocation
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrCourseCategoryAllocationMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCourseCategoryAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrCourseCategoryAllocationMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCourseCategoryAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrCourseCategoryAllocationMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrCourseCategoryAllocation As ArrayList = m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias)
            Return _TrCourseCategoryAllocation
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrCourseCategoryAllocationColl As ArrayList = m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrCourseCategoryAllocationColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCourseCategoryAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrClassColl As ArrayList = m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return TrClassColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCourseCategoryAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim TrCourseCategoryAllocationColl As ArrayList = m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrCourseCategoryAllocationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCourseCategoryAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim TrCourseCategoryAllocationColl As ArrayList = m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias, sortColl)
            Return TrCourseCategoryAllocationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrCourseCategoryAllocationColl As ArrayList = m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrCourseCategoryAllocationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrCourseCategoryAllocationColl As ArrayList = m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrCourseCategoryAllocation), columnName, matchOperator, columnValue))
            Return TrCourseCategoryAllocationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCourseCategoryAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryAllocation), columnName, matchOperator, columnValue))

            Return m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByClassCode(ByVal ClassCode As String) As ArrayList
            Dim ClassAllocationColl As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrCourseCategoryAllocation), "TrClass.ClassCode", MatchType.Exact, ClassCode))
            ClassAllocationColl = m_TrCourseCategoryAllocationMapper.RetrieveByCriteria(criterias)
            If ClassAllocationColl.Count > 0 Then
                Return ClassAllocationColl
            End If
            Return New ArrayList
        End Function

        Public Function RetrieveScalar(ByVal Criterias As ICriteria, ByVal aggregate As Aggregate) As Integer
            Dim obj As Object = m_TrCourseCategoryAllocationMapper.RetrieveScalar(aggregate, Criterias)
            If obj Is DBNull.Value Then
                Return 0
            Else
                Return CInt(obj)
            End If
        End Function


#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal courseCategoryCode As String, ByVal dealerCode As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryAllocation), "TrCourseCategory.Code", MatchType.Exact, courseCategoryCode))
            crit.opAnd(New Criteria(GetType(TrCourseCategoryAllocation), "Dealer.DealerCode", MatchType.Exact, dealerCode))

            Dim agg As Aggregate = New Aggregate(GetType(TrCourseCategoryAllocation), "ID", AggregateType.Count)

            Return CType(m_TrCourseCategoryAllocationMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TrCourseCategoryAllocation) As Integer
            Dim iReturn As Integer = -2
            Try
                m_TrCourseCategoryAllocationMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As TrCourseCategoryAllocation) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrCourseCategoryAllocationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As TrCourseCategoryAllocation) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrCourseCategoryAllocationMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateAllocation(ByVal AllAllocationDealerColl As ArrayList, _
            ByVal AllocationToProcessColl As ArrayList, ByVal ClassToAllocated As TrClass) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    For Each objClassAllocation As TrCourseCategoryAllocation In AllocationToProcessColl
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
            Dim result As Object = m_TrCourseCategoryAllocationMapper.RetrieveScalar(aggregate, criteria)
            If result Is System.DBNull.Value Then
                Return 0
            Else
                Return CType(result, Decimal)
            End If
        End Function

#End Region

#Region "Custom Method"
        Public Function RetrieveAllocation(ByVal area As Integer, ByVal dealer As String, ByVal code As String) As ArrayList
            Dim paramSQL As ArrayList = New ArrayList()
            Dim par As SqlClient.SqlParameter = New SqlClient.SqlParameter("@Area", area)
            paramSQL.Add(par)

            Dim par2 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@DealerCode", dealer)
            paramSQL.Add(par2)

            If Not String.IsNullOrEmpty(code) Then
                Dim par3 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@TrCourseCategoryID", code)
                paramSQL.Add(par3)
            End If

            Return m_TrCourseCategoryAllocationMapper.RetrieveSP("sp_GetCourseCategoryAllocation", paramSQL)
        End Function
        
#End Region

    End Class

End Namespace



