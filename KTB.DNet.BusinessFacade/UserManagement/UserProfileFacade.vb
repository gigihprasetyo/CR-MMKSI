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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.UserManagement
    Public Class UserProfileFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_UserProfileMapper As IMapper
        Private m_UserProfilePrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal UserProfilePrincipal As IPrincipal)
            Me.m_UserProfilePrincipal = UserProfilePrincipal
            Me.m_UserProfileMapper = MapperFactory.GetInstance().GetMapper(GetType(UserProfile).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

        Public Sub New(ByVal UserProfilePrincipal As IPrincipal, ByVal instanceName As String)
            Me.m_UserProfilePrincipal = UserProfilePrincipal
            Me.m_UserProfileMapper = MapperFactory.GetInstance().GetMapper(GetType(UserProfile).ToString, instanceName)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As UserProfile
            Return CType(m_UserProfileMapper.Retrieve(ID), UserProfile)
        End Function

        Public Function Retrieve(ByVal _UserID As Integer, ByVal _str As String) As UserProfile
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserProfile), "UserID", MatchType.Exact, _UserID))
            criterias.opAnd(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim UserProfileColl As ArrayList = m_UserProfileMapper.RetrieveByCriteria(criterias)
            If (UserProfileColl.Count > 0) Then
                Return CType(UserProfileColl(0), UserProfile)
            End If
            Return New UserProfile
        End Function

        Public Function Retrieve(ByVal Code As String) As UserProfile
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserProfile), "UserName", MatchType.Exact, Code))

            Dim UserProfileColl As ArrayList = m_UserProfileMapper.RetrieveByCriteria(criterias)
            If (UserProfileColl.Count > 0) Then
                Return CType(UserProfileColl(0), UserProfile)
            End If
            Return New UserProfile
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_UserProfileMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_UserProfileMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_UserProfileMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserProfileMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserProfileMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _UserProfile As ArrayList = m_UserProfileMapper.RetrieveByCriteria(criterias)
            Return _UserProfile
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim UserProfileColl As ArrayList = m_UserProfileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return UserProfileColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim UserProfileColl As ArrayList = m_UserProfileMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return UserProfileColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim UserProfileColl As ArrayList = m_UserProfileMapper.RetrieveByCriteria(Criterias, sortColl)
            Return UserProfileColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserProfileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim UserProfileColl As ArrayList = m_UserProfileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return UserProfileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserProfile), columnName, matchOperator, columnValue))
            Dim UserProfileColl As ArrayList = m_UserProfileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return UserProfileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserProfile), columnName, matchOperator, columnValue))

            Return m_UserProfileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As UserProfile) As Integer
            Dim iReturn As Integer = -2
            Try
                m_UserProfileMapper.Insert(objDomain, m_UserProfilePrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As UserProfile) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_UserProfileMapper.Update(objDomain, m_UserProfilePrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Update(ByVal objDomain As UserProfile, ByVal user As String) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_UserProfileMapper.Update(objDomain, user)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As UserProfile)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_UserProfileMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As UserProfile) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_UserProfileMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function ValidateCode(ByVal dealerID As Integer, ByVal userName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserProfile), "UserName", MatchType.Exact, userName))
            crit.opAnd(New Criteria(GetType(UserProfile), "Dealer.ID", MatchType.Exact, dealerID))

            Dim agg As Aggregate = New Aggregate(GetType(UserProfile), "UserName", AggregateType.Count)

            Return CType(m_UserProfileMapper.RetrieveScalar(agg, crit), Integer)
        End Function
        Public Function UpdateUserRole(ByVal userRoleList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If userRoleList.Count > 0 Then
                        For Each objUserRole As UserRole In userRoleList
                            m_TransactionManager.AddInsert(objUserRole, m_UserProfilePrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
                Catch ex As Exception
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
#End Region

#Region "Custom Method"

        Public Function Retrieve(ByVal _UserName As String, ByVal _DealerCode As String) As UserProfile
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserProfile), "UserName", MatchType.Exact, _UserName))
            criterias.opAnd(New Criteria(GetType(UserProfile), "Dealer.DealerCode", MatchType.Exact, _DealerCode))

            Dim UserProfileColl As ArrayList = m_UserProfileMapper.RetrieveByCriteria(criterias)
            If (UserProfileColl.Count > 0) Then
                Return CType(UserProfileColl(0), UserProfile)
            End If
            Return New UserProfile
        End Function

#End Region

    End Class

End Namespace
