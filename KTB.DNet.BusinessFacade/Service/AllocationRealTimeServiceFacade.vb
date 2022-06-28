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

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Service
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class AllocationRealTimeServiceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_AllocationRealTimeServiceMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager



#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_AllocationRealTimeServiceMapper = MapperFactory.GetInstance().GetMapper(GetType(AllocationRealTimeService).ToString)
            Me.objTransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AllocationRealTimeService
            Return CType(m_AllocationRealTimeServiceMapper.Retrieve(ID), AllocationRealTimeService)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.AllocationRealTimeService
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AllocationRealTimeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AllocationRealTimeService), "StallCode", MatchType.Exact, Code))

            Dim AllocationRealTimeServiceColl As ArrayList = m_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias)
            If (AllocationRealTimeServiceColl.Count > 0) Then
                Return CType(AllocationRealTimeServiceColl(0), KTB.DNet.Domain.AllocationRealTimeService)
            End If
            Return New KTB.DNet.Domain.AllocationRealTimeService
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AllocationRealTimeServiceMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AllocationRealTimeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AllocationRealTimeServiceMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AllocationRealTimeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AllocationRealTimeServiceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AllocationRealTimeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AllocationRealTimeService As ArrayList = m_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias)
            Return _AllocationRealTimeService
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AllocationRealTimeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AllocationRealTimeServiceColl As ArrayList = m_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AllocationRealTimeServiceColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AllocationRealTimeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AllocationRealTimeServiceColl As ArrayList = m_AllocationRealTimeServiceMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AllocationRealTimeServiceColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList


            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AllocationRealTimeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AllocationRealTimeServiceColl As ArrayList = m_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AllocationRealTimeServiceColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(AllocationRealTimeService), sortColumn, sortDirection))
            Else
                'sortColl = Nothing
                sortColl.Add(New Sort(GetType(AllocationRealTimeService), "ID", Sort.SortDirection.DESC))

            End If

            Dim AllocationRealTimeServiceColl As ArrayList = m_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AllocationRealTimeServiceColl
        End Function


        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AllocationRealTimeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AllocationRealTimeService), columnName, matchOperator, columnValue))
            Dim AllocationRealTimeServiceColl As ArrayList = m_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AllocationRealTimeServiceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AllocationRealTimeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AllocationRealTimeService), columnName, matchOperator, columnValue))

            Return m_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Integer
            Return m_AllocationRealTimeServiceMapper.RetrieveScalar(aggr, crit)
        End Function

        
#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As AllocationRealTimeService) As Integer
            Dim iReturn As Integer = -2
            Try
                m_AllocationRealTimeServiceMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As AllocationRealTimeService) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AllocationRealTimeServiceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Sub Delete(ByVal objDomain As AllocationRealTimeService)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_AllocationRealTimeServiceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As AllocationRealTimeService)
            Try
                m_AllocationRealTimeServiceMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal sID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AllocationRealTimeService), "ID", MatchType.Exact, sID))
            Dim agg As Aggregate = New Aggregate(GetType(AllocationRealTimeService), "ID", AggregateType.Count)

            Return CType(m_AllocationRealTimeServiceMapper.RetrieveScalar(agg, crit), Integer)
        End Function
#End Region

#Region "Custom Method"
        
#End Region

    End Class

End Namespace

