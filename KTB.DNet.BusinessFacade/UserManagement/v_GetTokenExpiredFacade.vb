
#Region "Code Disclaimer"
'Copyright PT. Berlian Sistem Indonesia (BSI) @2009

'BSI grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by BSI for you 
'(ii) or in solutions that are developed in join development between you and BSI.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009
'// ---------------------
'// $History      : $
'// Generated on 12/9/2009 - 10:10:45 AM
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


Namespace KTB.DNet.BusinessFacade.UserManagement

    Public Class v_GetTokenExpiredFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_v_GetTokenExpiredMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New()
            Me.m_v_GetTokenExpiredMapper = MapperFactory.GetInstance.GetMapper(GetType(v_GetTokenExpired).ToString)

        End Sub
        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_v_GetTokenExpiredMapper = MapperFactory.GetInstance.GetMapper(GetType(v_GetTokenExpired).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As v_GetTokenExpired
            Return CType(m_v_GetTokenExpiredMapper.Retrieve(ID), v_GetTokenExpired)
        End Function

        Public Function Retrieve(ByVal Code As String) As v_GetTokenExpired
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_GetTokenExpired), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(v_GetTokenExpired), "v_GetTokenExpiredCode", MatchType.Exact, Code))

            Dim v_GetTokenExpiredColl As ArrayList = m_v_GetTokenExpiredMapper.RetrieveByCriteria(criterias)
            If (v_GetTokenExpiredColl.Count > 0) Then
                Return CType(v_GetTokenExpiredColl(0), v_GetTokenExpired)
            End If
            Return New v_GetTokenExpired
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_v_GetTokenExpiredMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_v_GetTokenExpiredMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_v_GetTokenExpiredMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_GetTokenExpired), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_v_GetTokenExpiredMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_GetTokenExpired), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_v_GetTokenExpiredMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_GetTokenExpired), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _v_GetTokenExpired As ArrayList = m_v_GetTokenExpiredMapper.RetrieveByCriteria(criterias)
            Return _v_GetTokenExpired
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_GetTokenExpired), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim v_GetTokenExpiredColl As ArrayList = m_v_GetTokenExpiredMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return v_GetTokenExpiredColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim v_GetTokenExpiredColl As ArrayList = m_v_GetTokenExpiredMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return v_GetTokenExpiredColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_GetTokenExpired), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim v_GetTokenExpiredColl As ArrayList = m_v_GetTokenExpiredMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(v_GetTokenExpired), columnName, matchOperator, columnValue))
            Return v_GetTokenExpiredColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(v_GetTokenExpired), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_GetTokenExpired), columnName, matchOperator, columnValue))

            Return m_v_GetTokenExpiredMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_GetTokenExpired), "v_GetTokenExpiredCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(v_GetTokenExpired), "v_GetTokenExpiredCode", AggregateType.Count)
            Return CType(m_v_GetTokenExpiredMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


