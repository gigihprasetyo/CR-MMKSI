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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 7/19/2007 - 11:56:00 AM
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

    Public Class UserGroupMemberFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_UserGroupMemberMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_UserGroupMemberMapper = MapperFactory.GetInstance.GetMapper(GetType(UserGroupMember).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As UserGroupMember
            Return CType(m_UserGroupMemberMapper.Retrieve(ID), UserGroupMember)
        End Function

        Public Function Retrieve(ByVal Code As String) As UserGroupMember
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserGroupMember), "UserGroupMemberCode", MatchType.Exact, Code))

            Dim UserGroupMemberColl As ArrayList = m_UserGroupMemberMapper.RetrieveByCriteria(criterias)
            If (UserGroupMemberColl.Count > 0) Then
                Return CType(UserGroupMemberColl(0), UserGroupMember)
            End If
            Return New UserGroupMember
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_UserGroupMemberMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_UserGroupMemberMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_UserGroupMemberMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserGroupMember), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserGroupMemberMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserGroupMember), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserGroupMemberMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(UserGroupMember), SortColumn, sortDirection))

            Dim UserGroupMemberColl As ArrayList = m_UserGroupMemberMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return UserGroupMemberColl
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _UserGroupMember As ArrayList = m_UserGroupMemberMapper.RetrieveByCriteria(criterias)
            Return _UserGroupMember
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim UserGroupMemberColl As ArrayList = m_UserGroupMemberMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return UserGroupMemberColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim UserGroupMemberColl As ArrayList = m_UserGroupMemberMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return UserGroupMemberColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroupMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim UserGroupMemberColl As ArrayList = m_UserGroupMemberMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(UserGroupMember), columnName, matchOperator, columnValue))
            Return UserGroupMemberColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserGroupMember), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroupMember), columnName, matchOperator, columnValue))

            Return m_UserGroupMemberMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroupMember), "UserGroupMemberCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(UserGroupMember), "UserGroupMemberCode", AggregateType.Count)
            Return CType(m_UserGroupMemberMapper.RetrieveScalar(agg, crit), Integer)
        End Function
		
		Public Sub DeleteFromDB(ByVal objDomain As UserGroupMember)
            Try
                m_UserGroupMemberMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
		
        Public Function Update(ByVal objDomain As UserGroupMember) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_UserGroupMemberMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Private Function isIDExistinCollection(ByVal id As Integer, ByVal list As ArrayList) As Boolean
            For Each item As UserGroupMember In list
                If item.ID = id Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function Update(ByVal objDomainCollection As ArrayList, ByVal objUserGroup As UserGroup) As Integer
            Dim nResult As Integer = -1
            If (Me.IsTaskFree) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    If objDomainCollection.Count > 0 Then
                        Dim oldUserGroupMember As ArrayList = objUserGroup.UserGroupMembers
                        For Each oldItem As UserGroupMember In oldUserGroupMember
                            If Not (isIDExistinCollection(oldItem.ID, objDomainCollection)) Then
                                m_TransactionManager.AddDelete(oldItem)
                            End If
                        Next
                        For Each item As UserGroupMember In objDomainCollection
                            If Not (isIDExistinCollection(item.ID, oldUserGroupMember)) Then
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        nResult = objUserGroup.ID
                    End If
                Catch ex As Exception
                    nResult = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            
            Return nResult
        End Function

        Public Function Insert(ByVal objDomain As UserGroupMember) As Integer
            Dim iReturn As Integer = -2
            Try
                m_UserGroupMemberMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function
#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is UserGroup) Then
                CType(InsertArg.DomainObject, UserGroup).ID = InsertArg.ID
                CType(InsertArg.DomainObject, UserGroup).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is UserGroupMember) Then
                CType(InsertArg.DomainObject, UserGroupMember).ID = InsertArg.ID
            End If
        End Sub
#End Region

    End Class

End Namespace

