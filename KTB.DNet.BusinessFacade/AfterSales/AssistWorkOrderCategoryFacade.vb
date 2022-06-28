
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
'// Copyright  2017
'// ---------------------
'// $History      : $
'// Generated on 12/7/2017 - 2:39:45 PM
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

Namespace KTB.DNET.BusinessFacade.AfterSales

    Public Class AssistWorkOrderCategoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AssistWorkOrderCategoryMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_AssistWorkOrderCategoryMapper = MapperFactory.GetInstance.GetMapper(GetType(AssistWorkOrderCategory).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AssistWorkOrderCategory
            Return CType(m_AssistWorkOrderCategoryMapper.Retrieve(ID), AssistWorkOrderCategory)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AssistWorkOrderCategoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AssistWorkOrderCategoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AssistWorkOrderCategoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistWorkOrderCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AssistWorkOrderCategoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistWorkOrderCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AssistWorkOrderCategoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistWorkOrderCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AssistWorkOrderCategory As ArrayList = m_AssistWorkOrderCategoryMapper.RetrieveByCriteria(criterias)
            Return _AssistWorkOrderCategory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistWorkOrderCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AssistWorkOrderCategoryColl As ArrayList = m_AssistWorkOrderCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AssistWorkOrderCategoryColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AssistWorkOrderCategoryColl As ArrayList = m_AssistWorkOrderCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return AssistWorkOrderCategoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistWorkOrderCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AssistWorkOrderCategoryColl As ArrayList = m_AssistWorkOrderCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AssistWorkOrderCategory), columnName, matchOperator, columnValue))
            Return AssistWorkOrderCategoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistWorkOrderCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistWorkOrderCategory), columnName, matchOperator, columnValue))

            Return m_AssistWorkOrderCategoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As AssistWorkOrderCategory) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_AssistWorkOrderCategoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As AssistWorkOrderCategory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AssistWorkOrderCategoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As AssistWorkOrderCategory)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_AssistWorkOrderCategoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As AssistWorkOrderCategory)
            Try
                m_AssistWorkOrderCategoryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function ValidateCode(ByVal category As String, ByVal type As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistWorkOrderCategory), "WorkOrderCategory", MatchType.Exact, category))
            crit.opAnd(New Criteria(GetType(KTB.DNET.Domain.AssistWorkOrderCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(KTB.DNET.Domain.AssistWorkOrderCategory), "AssistWorkOrderType.ID", MatchType.Exact, type))
            Dim agg As Aggregate = New Aggregate(GetType(AssistWorkOrderCategory), "WorkOrderCategory", AggregateType.Count)

            Return CType(m_AssistWorkOrderCategoryMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AssistWorkOrderCategoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim list As ArrayList = m_AssistWorkOrderCategoryMapper.RetrieveByCriteria(criterias)
            Return list
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistWorkOrderCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AssistSalesChannelColl As ArrayList = m_AssistWorkOrderCategoryMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)

            Return AssistSalesChannelColl
        End Function

        Public Function UpdateStatusList(ByVal list As ArrayList, ByVal status As Int32) As Boolean
            Try
                For Each cat As AssistWorkOrderCategory In list
                    cat.Status = status
                    m_AssistWorkOrderCategoryMapper.Update(cat, m_userPrincipal.Identity.Name)
                Next
                Return True
            Catch ex As Exception
                Dim err As String = ex.Message
                Return False
            End Try
        End Function

#End Region

    End Class

End Namespace

