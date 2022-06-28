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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 12/19/2005 - 4:19:33 PM
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

    Public Class RoleFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_RoleMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_RoleMapper = MapperFactory.GetInstance.GetMapper(GetType(Role).ToString)
            Me.DomainTypeCollection.Add(GetType(Role))
            Me.DomainTypeCollection.Add(GetType(RoleOrganizationPrivilege))
            Me.m_TransactionManager = New TransactionManager
            AddHandler Me.m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As Role
            Return CType(m_RoleMapper.Retrieve(ID), Role)
        End Function

        Public Function Retrieve(ByVal Code As String) As Role
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Role), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Role), "RoleCode", MatchType.Exact, Code))

            Dim RoleColl As ArrayList = m_RoleMapper.RetrieveByCriteria(criterias)
            If (RoleColl.Count > 0) Then
                Return CType(RoleColl(0), Role)
            End If
            Return New Role
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_RoleMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RoleMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_RoleMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(Role), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RoleMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(Role), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RoleMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Role), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Role As ArrayList = m_RoleMapper.RetrieveByCriteria(criterias)
            Return _Role
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Role), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RoleColl As ArrayList = m_RoleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RoleColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim RoleColl As ArrayList = m_RoleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return RoleColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Role), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Role), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RoleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Role), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim RoleColl As ArrayList = m_RoleMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return RoleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Role), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RoleColl As ArrayList = m_RoleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(Role), columnName, matchOperator, columnValue))
            Return RoleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(Role), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Role), columnName, matchOperator, columnValue))

            Return m_RoleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal OrganizationID As Integer, ByVal RoleName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Role), "RoleName", MatchType.Exact, RoleName))
            crit.opAnd(New Criteria(GetType(Role), "Dealer.ID", MatchType.Exact, OrganizationID))
            Dim agg As Aggregate = New Aggregate(GetType(Role), "RoleName", AggregateType.Count)
            Return CType(m_RoleMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is Role) Then

                CType(InsertArg.DomainObject, Role).ID = InsertArg.ID
                CType(InsertArg.DomainObject, Role).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is RoleOrganizationPrivilege) Then

                CType(InsertArg.DomainObject, RoleOrganizationPrivilege).ID = InsertArg.ID

            End If

        End Sub

        Public Function Insert(ByVal objDomain As Role) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    _user = m_userPrincipal.Identity.Name
                    For Each item As RoleOrganizationPrivilege In objDomain.RoleOrganizationPrivileges
                        item.Role = objDomain
                        m_TransactionManager.AddInsert(item, _user)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
                    End If
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

        Public Function Update(ByVal ObjDomain As Role, ByVal newDetailList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    _user = m_userPrincipal.Identity.Name

                    If Not IsNothing(newDetailList) Then
                        PreprocessDetail(ObjDomain, newDetailList, _user)
                    End If
                    m_TransactionManager.AddUpdate(ObjDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
                    End If
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

        Private Sub PreprocessDetail(ByVal objDomain As Role, ByVal newDetail As ArrayList, ByVal User As String)

            Dim DeletedData As ArrayList = CleanUp(objDomain, newDetail)
            InsertNewData(objDomain, newDetail, DeletedData, User)

        End Sub

        Private Function CleanUp(ByRef objDomain As Role, ByVal newDetail As ArrayList) As ArrayList
            Dim WillBeDeleted As ArrayList = New ArrayList
            For Each oldData As RoleOrganizationPrivilege In objDomain.RoleOrganizationPrivileges
                If Not Found(oldData.OrganizationPrivilege.ID, newDetail) Then
                    m_TransactionManager.AddDelete(oldData)
                    WillBeDeleted.Add(oldData)
                End If
            Next
            'For Each deletedDt As RoleOrganizationPrivilege In WillBeDeleted
            '    objDomain.RoleOrganizationPrivileges.Remove(deletedDt)
            'Next
            Return WillBeDeleted
        End Function

        Private Sub InsertNewData(ByVal objDomain As Role, ByVal newDetail As ArrayList, ByVal deletedData As ArrayList, ByVal user As String)
            Dim arrDataInDomain As ArrayList = New ArrayList
            For Each dt As RoleOrganizationPrivilege In objDomain.RoleOrganizationPrivileges
                If Not deletedData.Contains(dt) Then
                    arrDataInDomain.Add(CStr(dt.OrganizationPrivilege.ID))
                End If
            Next

            For Each newData As String In newDetail
                If Not Found(CInt(newData), arrDataInDomain) Then
                    Dim objDetail As RoleOrganizationPrivilege = New RoleOrganizationPrivilege
                    objDetail.OrganizationPrivilege = New OrganizationPrivilegeFacade(m_userPrincipal).Retrieve(CInt(newData))
                    objDetail.Role = objDomain
                    objDomain.RoleOrganizationPrivileges.Add(objDetail)
                    m_TransactionManager.AddInsert(objDetail, user)
                End If
            Next
        End Sub

        Private Function Found(ByVal id As Integer, ByVal Data As ArrayList) As Boolean
            For Each dt As String In Data
                If dt = CStr(id) Then
                    Return True
                End If
            Next
            Return False
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace