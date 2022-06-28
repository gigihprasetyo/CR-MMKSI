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
'// Generated on 7/16/2007 - 11:33:42 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Text

#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTb.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.UserManagement
    Public Class UserGroupFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_UserGroupMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_UserGroupMapper = MapperFactory.GetInstance.GetMapper(GetType(UserGroup).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As UserGroup
            Return CType(m_UserGroupMapper.Retrieve(ID), UserGroup)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.UserGroup
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserGroup), "Code", MatchType.Exact, Code))

            Dim UserGroupColl As ArrayList = m_UserGroupMapper.RetrieveByCriteria(criterias)
            If (UserGroupColl.Count > 0) Then
                Return CType(UserGroupColl(0), UserGroup)
            End If
            Return New UserGroup
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_UserGroupMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_UserGroupMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_UserGroupMapper.RetrieveList
        End Function


        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserGroupMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserGroupMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _UserGroup As ArrayList = m_UserGroupMapper.RetrieveByCriteria(criterias)
            Return _UserGroup
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim UserGroupColl As ArrayList = m_UserGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return UserGroupColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(SortColumn)) And (Not SortColumn = "") Then
                sortColl.Add(New Sort(GetType(UserGroup), SortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim UserGroupColl As ArrayList = m_UserGroupMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return UserGroupColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(SortColumn)) And (Not SortColumn = "") Then
                sortColl.Add(New Sort(GetType(UserGroup), SortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim UserGroupColl As ArrayList = m_UserGroupMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return UserGroupColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim UserGroupColl As ArrayList = m_UserGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return UserGroupColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim UserGroupColl As ArrayList = m_UserGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(UserGroup), columnName, matchOperator, columnValue))
            Return UserGroupColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroup), columnName, matchOperator, columnValue))

            Return m_UserGroupMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCategoryID(ByVal CategoryID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserGroup), "UserGroupCategory.ID", MatchType.Exact, CategoryID))
            criterias.opAnd(New Criteria(GetType(UserGroup), "Status", MatchType.Exact, "1"))
            Return Me.m_UserGroupMapper.RetrieveByCriteria(criterias)
        End Function



#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String, ByVal IdEdit As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroup), "Code", MatchType.Exact, Code))
            If IdEdit <> 0 Then
                crit.opAnd(New Criteria(GetType(UserGroup), "ID", MatchType.No, IdEdit))
            End If

            Dim agg As Aggregate = New Aggregate(GetType(UserGroup), "Code", AggregateType.Count)
            Return CType(m_UserGroupMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateCode(ByVal objDomain As UserGroupMember, ByVal arlDataNeedChecked As ArrayList)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroupMember), "UserGroup.ID", MatchType.Exact, objDomain.UserGroup.ID))
            criterias.opAnd(New Criteria(GetType(UserGroupMember), "UserInfo.ID.", MatchType.Exact, objDomain.UserInfo.ID))
            Dim agg As Aggregate = New Aggregate(GetType(UserGroupMember), "UserInfo.ID", AggregateType.Count)
            Return CType(m_UserGroupMapper.RetrieveScalar(agg, criterias), Integer)
        End Function

        Public Function InsertTransaction(ByVal objUserGroup As UserGroup, ByVal arrUserGroupMember As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    objUserGroup.Description = objUserGroup.Description
                    m_TransactionManager.AddInsert(objUserGroup, m_userPrincipal.Identity.Name)

                    If arrUserGroupMember.Count > 0 Then
                        For Each objUserGroupMember As UserGroupMember In arrUserGroupMember
                            objUserGroupMember.UserGroup = objUserGroup
                            m_TransactionManager.AddInsert(objUserGroupMember, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = objUserGroup.ID
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

        Public Function Insert(ByVal objDomain As UserGroup) As Integer
            Dim iReturn As Integer = -2
            Try
                m_UserGroupMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                iReturn = 1
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function DeleteFromDB(ByVal objDomain As UserGroup) As Integer
            Try
                m_UserGroupMapper.Delete(objDomain)
                Return 1
            Catch ex As Exception
                Return -1
            End Try
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is UserGroup) Then
                CType(InsertArg.DomainObject, UserGroup).ID = InsertArg.ID
                CType(InsertArg.DomainObject, UserGroup).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is UserGroupMember) Then
                CType(InsertArg.DomainObject, UserGroupMember).ID = InsertArg.ID
            End If
        End Sub

        Public Function Update(ByVal objDomain As UserGroup) As Integer
            Dim nResult As Integer = -1
            Try
                'objDomain.Code = objDomain.Code
                'objDomain.Description = objDomain.Description
                nResult = m_UserGroupMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


#End Region

#Region "Custom Method"

        Public Function GetGroupCS() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserGroup), "Code", MatchType.InSet, "('CS_CSO','CS_SLS','CS_ASS')"))
            Return Me.m_UserGroupMapper.RetrieveByCriteria(criterias)
        End Function

#End Region

    End Class

End Namespace

