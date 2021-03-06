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
'// Copyright ? 2005 
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
    Public Class UserRoleFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_UserRoleMapper As IMapper
        Private m_UserRolePrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager


#End Region

#Region "Constructor"

        Public Sub New(ByVal UserRolePrincipal As IPrincipal)
            Me.m_UserRolePrincipal = UserRolePrincipal
            Me.m_UserRoleMapper = MapperFactory.GetInstance().GetMapper(GetType(UserRole).ToString)
            Me.m_TransactionManager = New TransactionManager

            Me.DomainTypeCollection.Add(GetType(UserRole))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As UserRole
            Return CType(m_UserRoleMapper.Retrieve(ID), UserRole)
        End Function

        Public Function Retrieve(ByVal Code As String) As UserRole
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserRole), "UserRoleName", MatchType.Exact, Code))

            Dim UserRoleColl As ArrayList = m_UserRoleMapper.RetrieveByCriteria(criterias)
            If (UserRoleColl.Count > 0) Then
                Return CType(UserRoleColl(0), UserRole)
            End If
            Return New UserRole
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_UserRoleMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_UserRoleMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_UserRoleMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserRole), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserRoleMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserRole), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserRoleMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _UserRole As ArrayList = m_UserRoleMapper.RetrieveByCriteria(criterias)
            Return _UserRole
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim UserRoleColl As ArrayList = m_UserRoleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return UserRoleColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserRole), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim UserRoleColl As ArrayList = m_UserRoleMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return UserRoleColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserRole), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim UserRoleColl As ArrayList = m_UserRoleMapper.RetrieveByCriteria(Criterias, sortColl)
            Return UserRoleColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserRole), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserRoleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim UserRoleColl As ArrayList = m_UserRoleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return UserRoleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserRole), columnName, matchOperator, columnValue))
            Dim UserRoleColl As ArrayList = m_UserRoleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return UserRoleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserRole), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), columnName, matchOperator, columnValue))

            Return m_UserRoleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As UserRole) As Integer
            Dim iReturn As Integer = -2
            Try
                m_UserRoleMapper.Insert(objDomain, m_UserRolePrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As UserRole) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_UserRoleMapper.Update(objDomain, m_UserRolePrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As UserRole)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_UserRoleMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As UserRole) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_UserRoleMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateUserRole(ByVal userRoleList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If userRoleList.Count > 0 Then
                        For Each objUserRole As UserRole In userRoleList
                            If objUserRole.RowStatus = DBRowStatus.Active Then
                                m_TransactionManager.AddInsert(objUserRole, m_UserRolePrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddDelete(objUserRole)
                            End If
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
        Public Function InsertUserRole(ByVal userRoleList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If userRoleList.Count > 0 Then
                        For Each objUserRole As UserRole In userRoleList
                            If objUserRole.RowStatus = DBRowStatus.Active Then
                                m_TransactionManager.AddInsert(objUserRole, m_UserRolePrincipal.Identity.Name)
                            End If
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


        'Public Function ValidateCode(ByVal dealerID As Integer, ByVal userName As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), "UserName", MatchType.Exact, userName))
        '    crit.opAnd(New Criteria(GetType(UserRole), "Dealer.ID", MatchType.Exact, dealerID))

        '    Dim agg As Aggregate = New Aggregate(GetType(UserRole), "UserName", AggregateType.Count)

        '    Return CType(m_UserRoleMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
