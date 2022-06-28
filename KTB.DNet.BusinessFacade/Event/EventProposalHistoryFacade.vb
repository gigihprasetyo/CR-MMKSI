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
'// Author Name   : Ariwibawa
'// PURPOSE       : Facade for Page Event - Event Proposal History
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright � 2009 
'// ---------------------
'// $History      : $
'// Generated on 9/09/2009 - 08:26:00 AM
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

Namespace KTB.DNet.BusinessFacade.Event
    Public Class EventProposalHistoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_EventProposalHistoryMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_EventProposalHistoryMapper = MapperFactory.GetInstance().GetMapper(GetType(EventProposalHistory).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As EventProposalHistory
            Return CType(m_EventProposalHistoryMapper.Retrieve(ID), EventProposalHistory)
        End Function



        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EventProposalHistoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EventProposalHistoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EventProposalHistoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventProposalHistoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventProposalHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EventProposalHistory As ArrayList = m_EventProposalHistoryMapper.RetrieveByCriteria(criterias)
            Return _EventProposalHistory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventProposalHistoryColl As ArrayList = m_EventProposalHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventProposalHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim EventProposalHistoryColl As ArrayList = m_EventProposalHistoryMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return EventProposalHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_EventProposalHistoryMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventProposalHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EventProposalHistoryColl As ArrayList = m_EventProposalHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventProposalHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventProposalHistory), columnName, matchOperator, columnValue))
            Dim EventProposalHistoryColl As ArrayList = m_EventProposalHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventProposalHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposalHistory), columnName, matchOperator, columnValue))

            Return m_EventProposalHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"
        Public Function Insert(ByVal objDomain As EventProposalHistory) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_EventProposalHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As EventProposalHistory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_EventProposalHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As EventProposalHistory) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_EventProposalHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As EventProposalHistory) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_EventProposalHistoryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region

    End Class
End Namespace