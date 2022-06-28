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
'// Copyright  2006
'// ---------------------
'// $History      : $
'// Generated on 1/19/2006 - 10:13:31 AM
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

#End Region

Namespace KTB.DNet.BusinessFacade

    Public Class PRPUserEmailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PRPUserEmailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PRPUserEmailMapper = MapperFactory.GetInstance.GetMapper(GetType(PRPUserEmail).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PRPUserEmail
            Return CType(m_PRPUserEmailMapper.Retrieve(ID), PRPUserEmail)
        End Function

        Public Function Retrieve(ByVal Code As String) As PRPUserEmail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PRPUserEmail), "PRPUserEmailCode", MatchType.Exact, Code))

            Dim PRPUserEmailColl As ArrayList = m_PRPUserEmailMapper.RetrieveByCriteria(criterias)
            If (PRPUserEmailColl.Count > 0) Then
                Return CType(PRPUserEmailColl(0), PRPUserEmail)
            End If
            Return New PRPUserEmail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PRPUserEmailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PRPUserEmailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PRPUserEmailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PRPUserEmail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PRPUserEmailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PRPUserEmail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PRPUserEmailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PRPUserEmail As ArrayList = m_PRPUserEmailMapper.RetrieveByCriteria(criterias)
            Return _PRPUserEmail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PRPUserEmailColl As ArrayList = m_PRPUserEmailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PRPUserEmailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PRPUserEmailColl As ArrayList = m_PRPUserEmailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PRPUserEmailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As System.Collections.ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByVal totalrow As Integer)
            Dim PRPUserEmailColl As ArrayList = m_PRPUserEmailMapper.RetrieveByCriteria(criterias, sorts, pageNumber, pageSize, totalrow)
            Return PRPUserEmailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PRPUserEmailColl As ArrayList = m_PRPUserEmailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PRPUserEmail), columnName, matchOperator, columnValue))
            Return PRPUserEmailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PRPUserEmail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPUserEmail), columnName, matchOperator, columnValue))

            Return m_PRPUserEmailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateName(ByVal Name As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PRPUserEmail), "UserName", MatchType.Exact, Name))
            Dim agg As Aggregate = New Aggregate(GetType(PRPUserEmail), "UserName", AggregateType.Count)
            Return CType(m_PRPUserEmailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal obj As Object)
            Dim nResult As Integer = -2
            Try
                nResult = m_PRPUserEmailMapper.Insert(obj, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                nResult = -1
            End Try
            Return nResult
        End Function

        Public Function Update(ByVal obj As Object)
            Dim nResult As Integer = -2
            Try
                nResult = m_PRPUserEmailMapper.Update(obj, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                nResult = -1
            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal obj As Object)
            Dim nResult As Integer = -2
            Try
                nResult = m_PRPUserEmailMapper.Delete(obj)
            Catch ex As Exception
                Dim s As String = ex.Message
                nResult = -1
            End Try
            Return nResult
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

