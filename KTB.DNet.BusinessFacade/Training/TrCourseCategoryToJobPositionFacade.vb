
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
'// Generated on 20/06/2019 - 8:50:25
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class TrCourseCategoryToJobPositionFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrCourseCategoryToJobPositionMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TrCourseCategoryToJobPositionMapper = MapperFactory.GetInstance.GetMapper(GetType(TrCourseCategoryToJobPosition).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrCourseCategoryToJobPosition
            Return CType(m_TrCourseCategoryToJobPositionMapper.Retrieve(ID), TrCourseCategoryToJobPosition)
        End Function

        Public Function Retrieve(ByVal Code As String) As TrCourseCategoryToJobPosition
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryToJobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrCourseCategoryToJobPosition), "TrCourseCategoryToJobPositionCode", MatchType.Exact, Code))

            Dim TrCourseCategoryToJobPositionColl As ArrayList = m_TrCourseCategoryToJobPositionMapper.RetrieveByCriteria(criterias)
            If (TrCourseCategoryToJobPositionColl.Count > 0) Then
                Return CType(TrCourseCategoryToJobPositionColl(0), TrCourseCategoryToJobPosition)
            End If
            Return New TrCourseCategoryToJobPosition
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrCourseCategoryToJobPositionMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrCourseCategoryToJobPositionMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrCourseCategoryToJobPositionMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCourseCategoryToJobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrCourseCategoryToJobPositionMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCourseCategoryToJobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrCourseCategoryToJobPositionMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryToJobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrCourseCategoryToJobPosition As ArrayList = m_TrCourseCategoryToJobPositionMapper.RetrieveByCriteria(criterias)
            Return _TrCourseCategoryToJobPosition
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryToJobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrCourseCategoryToJobPositionColl As ArrayList = m_TrCourseCategoryToJobPositionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrCourseCategoryToJobPositionColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(TrCourseCategoryToJobPosition), SortColumn, sortDirection))
            Dim TrCourseCategoryToJobPositionColl As ArrayList = m_TrCourseCategoryToJobPositionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrCourseCategoryToJobPositionColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrCourseCategoryToJobPositionColl As ArrayList = m_TrCourseCategoryToJobPositionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrCourseCategoryToJobPositionColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryToJobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrCourseCategoryToJobPositionColl As ArrayList = m_TrCourseCategoryToJobPositionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrCourseCategoryToJobPosition), columnName, matchOperator, columnValue))
            Return TrCourseCategoryToJobPositionColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCourseCategoryToJobPosition), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryToJobPosition), columnName, matchOperator, columnValue))

            Return m_TrCourseCategoryToJobPositionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryToJobPosition), "TrCourseCategoryToJobPositionCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TrCourseCategoryToJobPosition), "TrCourseCategoryToJobPositionCode", AggregateType.Count)
            Return CType(m_TrCourseCategoryToJobPositionMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TrCourseCategoryToJobPosition) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TrCourseCategoryToJobPositionMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TrCourseCategoryToJobPosition) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrCourseCategoryToJobPositionMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TrCourseCategoryToJobPosition)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TrCourseCategoryToJobPositionMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TrCourseCategoryToJobPosition)
            Try
                m_TrCourseCategoryToJobPositionMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

