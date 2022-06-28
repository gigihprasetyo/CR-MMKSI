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
'// Generated on 12/20/2005 - 2:09:42 PM
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

    Public Class OrganizationPrivilegeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_OrganizationPrivilegeMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_OrganizationPrivilegeMapper = MapperFactory.GetInstance.GetMapper(GetType(OrganizationPrivilege).ToString)
            Me.DomainTypeCollection.Add(GetType(OrganizationPrivilege))
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As OrganizationPrivilege
            Return CType(m_OrganizationPrivilegeMapper.Retrieve(ID), OrganizationPrivilege)
        End Function

        Public Function Retrieve(ByVal Code As String) As OrganizationPrivilege
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrganizationPrivilege), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(OrganizationPrivilege), "OrganizationPrivilegeCode", MatchType.Exact, Code))

            Dim OrganizationPrivilegeColl As ArrayList = m_OrganizationPrivilegeMapper.RetrieveByCriteria(criterias)
            If (OrganizationPrivilegeColl.Count > 0) Then
                Return CType(OrganizationPrivilegeColl(0), OrganizationPrivilege)
            End If
            Return New OrganizationPrivilege
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_OrganizationPrivilegeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_OrganizationPrivilegeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_OrganizationPrivilegeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(OrganizationPrivilege), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_OrganizationPrivilegeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(OrganizationPrivilege), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_OrganizationPrivilegeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveByCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection, _
            ByVal criterias As ICriteria) As ArrayList

            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(OrganizationPrivilege), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_OrganizationPrivilegeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrganizationPrivilege), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _OrganizationPrivilege As ArrayList = m_OrganizationPrivilegeMapper.RetrieveByCriteria(criterias)
            Return _OrganizationPrivilege
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrganizationPrivilege), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim OrganizationPrivilegeColl As ArrayList = m_OrganizationPrivilegeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return OrganizationPrivilegeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim OrganizationPrivilegeColl As ArrayList = m_OrganizationPrivilegeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return OrganizationPrivilegeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrganizationPrivilege), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim OrganizationPrivilegeColl As ArrayList = m_OrganizationPrivilegeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(OrganizationPrivilege), columnName, matchOperator, columnValue))
            Return OrganizationPrivilegeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OrganizationPrivilege), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrganizationPrivilege), columnName, matchOperator, columnValue))

            Return m_OrganizationPrivilegeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function Retrieve(ByVal OrganizationID As Integer, ByVal PrivilegeID As Integer) As ArrayList
            Dim Org As OrganizationPrivilege
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrganizationPrivilege), "Privilege.ID", MatchType.Exact, PrivilegeID))
            crit.opAnd(New Criteria(GetType(OrganizationPrivilege), "Dealer.ID", MatchType.Exact, OrganizationID))

            Return m_OrganizationPrivilegeMapper.RetrieveByCriteria(crit)
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrganizationPrivilege), "OrganizationPrivilegeCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(OrganizationPrivilege), "OrganizationPrivilegeCode", AggregateType.Count)
            Return CType(m_OrganizationPrivilegeMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        'Modified so it can run as transaction parallel with insert
        Public Sub DeleteFromDB(ByVal objDomain As OrganizationPrivilege)
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    'm_OrganizationPrivilegeMapper.Delete(objDomain)
                    m_TransactionManager.AddDelete(objDomain)
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
        End Sub

        'Public Sub Update(ByVal objDomain As OrganizationPrivilege)
        '    Try
        '        m_OrganizationPrivilegeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '        If rethrow Then
        '            Throw
        '        End If
        '    End Try
        'End Sub

        Public Sub Insert(ByVal objDomain As OrganizationPrivilege)
            Dim nReturnValue As Integer = 0
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                Catch ex As Exception
                    nReturnValue = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If

        End Sub

        Public Sub PerformTransaction(ByVal objDealer As Dealer, ByVal newDetail As ArrayList)
            Dim nReturnValue As Integer = 0
            Try
                Me.SetTaskLocking()

                Dim CritComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrganizationPrivilege), "RowStatus", MatchType.Exact, CInt(DBRowStatus.Active)))
                CritComp.opAnd(New Criteria(GetType(OrganizationPrivilege), "Dealer.ID", MatchType.Exact, objDealer.ID))
                Dim objOrganizationPrivileges As ArrayList = New OrganizationPrivilegeFacade(m_userPrincipal).Retrieve(CritComp)

                CleanUp(objOrganizationPrivileges, newDetail)
                InsertNewData(objDealer, objOrganizationPrivileges, newDetail)

                m_TransactionManager.PerformTransaction()
            Catch ex As Exception
                nReturnValue = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            Finally
                Me.RemoveTaskLocking()
            End Try
        End Sub

        Private Sub CleanUp(ByVal objDomain As ArrayList, ByVal newDetail As ArrayList)
            Dim WillBeDeleted As ArrayList = New ArrayList
            For Each oldData As OrganizationPrivilege In objDomain
                If Not Found(oldData.Privilege.ID, newDetail) Then
                    m_TransactionManager.AddDelete(oldData)
                    WillBeDeleted.Add(oldData)
                End If
            Next
            For Each deletedDt As OrganizationPrivilege In WillBeDeleted
                objDomain.Remove(deletedDt)
            Next
        End Sub

        Private Sub InsertNewData(ByVal nDealer As Dealer, ByVal objDomain As ArrayList, ByVal newDetail As ArrayList)
            Dim arrDataInDomain As ArrayList = New ArrayList
            For Each dt As OrganizationPrivilege In objDomain
                arrDataInDomain.Add(CStr(dt.Privilege.ID))
            Next

            For Each newData As String In newDetail
                If Not Found(CInt(newData), arrDataInDomain) Then
                    InsertOP(nDealer, CInt(newData))
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

        Private Sub InsertOP(ByVal objDealer As Dealer, ByVal PrivilegeID As Integer)
            Dim objDomain As OrganizationPrivilege
            Dim objFacade As OrganizationPrivilegeFacade = New OrganizationPrivilegeFacade(m_userPrincipal)
            objDomain = New OrganizationPrivilege
            objDomain.Dealer = objDealer
            objDomain.Privilege = New PrivilegeFacade(m_userPrincipal).Retrieve(PrivilegeID)
            m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
        End Sub
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

