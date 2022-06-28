
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
'// Copyright  2010
'// ---------------------
'// $History      : $
'// Generated on 3/4/2010 - 11:14:48 AM
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
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNET.DataMapper
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class CustomerStatusHistoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CustomerStatusHistoryMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_CustomerStatusHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(CustomerStatusHistory).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CustomerStatusHistory
            Return CType(m_CustomerStatusHistoryMapper.Retrieve(ID), CustomerStatusHistory)
        End Function

        Public Function Retrieve(ByVal Code As String) As CustomerStatusHistory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerStatusHistory), "CustomerStatusHistoryCode", MatchType.Exact, Code))

            Dim CustomerStatusHistoryColl As ArrayList = m_CustomerStatusHistoryMapper.RetrieveByCriteria(criterias)
            If (CustomerStatusHistoryColl.Count > 0) Then
                Return CType(CustomerStatusHistoryColl(0), CustomerStatusHistory)
            End If
            Return New CustomerStatusHistory
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CustomerStatusHistoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CustomerStatusHistoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CustomerStatusHistoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerStatusHistoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerStatusHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _CustomerStatusHistory As ArrayList = m_CustomerStatusHistoryMapper.RetrieveByCriteria(criterias)
            Return _CustomerStatusHistory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CustomerStatusHistoryColl As ArrayList = m_CustomerStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CustomerStatusHistoryColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CustomerStatusHistoryColl As ArrayList = m_CustomerStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CustomerStatusHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CustomerStatusHistoryColl As ArrayList = m_CustomerStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(CustomerStatusHistory), columnName, matchOperator, columnValue))
            Return CustomerStatusHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerStatusHistory), columnName, matchOperator, columnValue))

            Return m_CustomerStatusHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerStatusHistory), "CustomerStatusHistoryCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(CustomerStatusHistory), "CustomerStatusHistoryCode", AggregateType.Count)
            Return CType(m_CustomerStatusHistoryMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As CustomerStatusHistory) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_CustomerStatusHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                'Dim s As String = ex.Message
                'iReturn = -1
                Throw New System.ApplicationException(ex.Message)

            End Try
            Return iReturn

        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

