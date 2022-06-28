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
'// Copyright  2021
'// ---------------------
'// $History      : $
'// Generated on 8/9/2021 - 10:31:25 AM
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

    Public Class VW_SalesOrderInterestFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SalesOrderInterestMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SalesOrderInterestMapper = MapperFactory.GetInstance.GetMapper(GetType(VW_SalesOrderInterest).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As VW_SalesOrderInterest
            Return CType(m_SalesOrderInterestMapper.Retrieve(ID), VW_SalesOrderInterest)
        End Function

        Public Function Retrieve(ByVal Code As String) As VW_SalesOrderInterest
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VW_SalesOrderInterest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "SalesOrderInterestCode", MatchType.Exact, Code))

            Dim SalesOrderInterestColl As ArrayList = m_SalesOrderInterestMapper.RetrieveByCriteria(criterias)
            If (SalesOrderInterestColl.Count > 0) Then
                Return CType(SalesOrderInterestColl(0), VW_SalesOrderInterest)
            End If
            Return New VW_SalesOrderInterest
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesOrderInterestMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SalesOrderInterestMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SalesOrderInterestMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VW_SalesOrderInterest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesOrderInterestMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VW_SalesOrderInterest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesOrderInterestMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VW_SalesOrderInterest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SalesOrderInterest As ArrayList = m_SalesOrderInterestMapper.RetrieveByCriteria(criterias)
            Return _SalesOrderInterest
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VW_SalesOrderInterest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesOrderInterestColl As ArrayList = m_SalesOrderInterestMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesOrderInterestColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(VW_SalesOrderInterest), SortColumn, sortDirection))
            Dim SalesOrderInterestColl As ArrayList = m_SalesOrderInterestMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SalesOrderInterestColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SalesOrderInterestColl As ArrayList = m_SalesOrderInterestMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SalesOrderInterestColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VW_SalesOrderInterest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesOrderInterestColl As ArrayList = m_SalesOrderInterestMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(VW_SalesOrderInterest), columnName, matchOperator, columnValue))
            Return SalesOrderInterestColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VW_SalesOrderInterest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VW_SalesOrderInterest), columnName, matchOperator, columnValue))

            Return m_SalesOrderInterestMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VW_SalesOrderInterest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim vw_SOInterestColl As ArrayList = m_SalesOrderInterestMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return vw_SOInterestColl
        End Function

        Public Function RetrieveBySONumber(ByVal SONumber As String) As VW_SalesOrderInterest
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VW_SalesOrderInterest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VW_SalesOrderInterest), "SONumber", MatchType.Exact, SONumber))

            Dim SalesOrderInterestColl As ArrayList = m_SalesOrderInterestMapper.RetrieveByCriteria(criterias)
            If (SalesOrderInterestColl.Count > 0) Then
                Return CType(SalesOrderInterestColl(0), VW_SalesOrderInterest)
            End If
            Return New VW_SalesOrderInterest
        End Function


#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesOrderInterest), "SalesOrderInterestCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SalesOrderInterest), "SalesOrderInterestCode", AggregateType.Count)
            Return CType(m_SalesOrderInterestMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SalesOrderInterest) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SalesOrderInterestMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SalesOrderInterest) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SalesOrderInterestMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SalesOrderInterest)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SalesOrderInterestMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SalesOrderInterest)
            Try
                m_SalesOrderInterestMapper.Delete(objDomain)
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
