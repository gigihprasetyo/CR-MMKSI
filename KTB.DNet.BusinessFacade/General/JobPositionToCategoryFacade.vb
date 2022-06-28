
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 30/04/2019 - 12:42:53
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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class JobPositionToCategoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_JobPositionToCategoryMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_JobPositionToCategoryMapper = MapperFactory.GetInstance.GetMapper(GetType(JobPositionToCategory).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As JobPositionToCategory
            Return CType(m_JobPositionToCategoryMapper.Retrieve(ID), JobPositionToCategory)
        End Function

        Public Function Retrieve(ByVal Code As String) As JobPositionToCategory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(JobPositionToCategory), "JobPositionToCategoryCode", MatchType.Exact, Code))

            Dim JobPositionToCategoryColl As ArrayList = m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias)
            If (JobPositionToCategoryColl.Count > 0) Then
                Return CType(JobPositionToCategoryColl(0), JobPositionToCategory)
            End If
            Return New JobPositionToCategory
        End Function

        Public Function RetrieveByCategory(ByVal category As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(JobPositionToCategory), "JobPositionCategory.ID", MatchType.Exact, category))

            Return m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias)

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_JobPositionToCategoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(JobPositionToCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_JobPositionToCategoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(JobPositionToCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_JobPositionToCategoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _JobPositionToCategory As ArrayList = m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias)
            Return _JobPositionToCategory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim JobPositionToCategoryColl As ArrayList = m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return JobPositionToCategoryColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(JobPositionToCategory), SortColumn, sortDirection))
            Dim JobPositionToCategoryColl As ArrayList = m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return JobPositionToCategoryColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim JobPositionToCategoryColl As ArrayList = m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return JobPositionToCategoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim JobPositionToCategoryColl As ArrayList = m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(JobPositionToCategory), columnName, matchOperator, columnValue))
            Return JobPositionToCategoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(JobPositionToCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), columnName, matchOperator, columnValue))

            Return m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "JobPositionToCategoryCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(JobPositionToCategory), "JobPositionToCategoryCode", AggregateType.Count)
            Return CType(m_JobPositionToCategoryMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As JobPositionToCategory) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_JobPositionToCategoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As JobPositionToCategory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_JobPositionToCategoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As JobPositionToCategory)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_JobPositionToCategoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As JobPositionToCategory)
            Try
                m_JobPositionToCategoryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveSparepartJobPosition(ByVal DealerID As Integer) As ArrayList
            Return m_JobPositionToCategoryMapper.RetrieveSP("exec SP_GetJobPositionForSparepart " & DealerID & "")
        End Function

        Public Function RetrieveByCategoryID(ByVal categoryID As Integer) As String
            Dim result As String = String.Empty
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(JobPositionToCategory), "JobPositionCategory.ID", MatchType.Exact, categoryID))

            Dim JobPositionToCategoryColl As ArrayList = m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias)
            If (JobPositionToCategoryColl.Count > 0) Then
                result += "("
                For Each iJob As JobPositionToCategory In JobPositionToCategoryColl
                    result += iJob.JobPosition.ID.ToString + ", "
                Next
                result = result.Remove(result.Length - 2, 2) + ")"
            Else
                result = "(0)"
            End If

            Return result
        End Function

        Public Function RetrieveByArrCategoryID(ByVal arrCategoryID As String) As String
            Dim result As String = String.Empty
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(JobPositionToCategory), "JobPositionCategory.ID", MatchType.InSet, arrCategoryID))

            Dim JobPositionToCategoryColl As ArrayList = m_JobPositionToCategoryMapper.RetrieveByCriteria(criterias)
            If (JobPositionToCategoryColl.Count > 0) Then
                result += "("
                For Each iJob As JobPositionToCategory In JobPositionToCategoryColl
                    result += iJob.JobPosition.ID.ToString + ", "
                Next
                result = result.Remove(result.Length - 2, 2) + ")"
            Else
                result = "(0)"
            End If

            Return result
        End Function
#End Region

    End Class

End Namespace

