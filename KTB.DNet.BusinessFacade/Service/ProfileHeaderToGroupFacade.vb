 

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

Namespace KTB.DNet.BusinessFacade.Service

    Public Class ProfileHeaderToGroupFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ProfileHeaderToGroupMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ProfileHeaderToGroupMapper = MapperFactory.GetInstance.GetMapper(GetType(ProfileHeaderToGroup).ToString)
            Me.DomainTypeCollection.Add(GetType(ProfileHeaderToGroup))
            Me.m_TransactionManager = New TransactionManager
            AddHandler Me.m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ProfileHeaderToGroup
            Return CType(m_ProfileHeaderToGroupMapper.Retrieve(ID), ProfileHeaderToGroup)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ProfileHeaderToGroupMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ProfileHeaderToGroupMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ProfileHeaderToGroupMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(ProfileHeaderToGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProfileHeaderToGroupMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(ProfileHeaderToGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProfileHeaderToGroupMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ProfileHeaderToGroup As ArrayList = m_ProfileHeaderToGroupMapper.RetrieveByCriteria(criterias)
            Return _ProfileHeaderToGroup
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ProfileHeaderToGroupColl As ArrayList = m_ProfileHeaderToGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ProfileHeaderToGroupColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ProfileHeaderToGroupColl As ArrayList = m_ProfileHeaderToGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ProfileHeaderToGroupColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim ProfileHeaderToGroupColl As ArrayList = m_ProfileHeaderToGroupMapper.RetrieveByCriteria(criterias)
            Return ProfileHeaderToGroupColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, _
            ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(ProfileHeaderToGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim ProfileHeaderToGroupColl As ArrayList = m_ProfileHeaderToGroupMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ProfileHeaderToGroupColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ProfileHeaderToGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProfileHeaderToGroupMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ProfileHeaderToGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ProfileHeaderToGroupColl As ArrayList = m_ProfileHeaderToGroupMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ProfileHeaderToGroupColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ProfileHeaderToGroupColl As ArrayList = m_ProfileHeaderToGroupMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), columnName, matchOperator, columnValue))
            Return ProfileHeaderToGroupColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(ProfileHeaderToGroup), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), columnName, matchOperator, columnValue))

            Return m_ProfileHeaderToGroupMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function IsProfileHeaderToGroupFound(ByVal ProfileHeaderToGroupCode As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "Code", MatchType.Exact, ProfileHeaderToGroupCode))
            Dim ProfileHeaderToGroupColl As ArrayList = m_ProfileHeaderToGroupMapper.RetrieveByCriteria(criterias)
            If (ProfileHeaderToGroupColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal OrganizationID As Integer, ByVal ProfileHeaderToGroupName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "ProfileHeaderToGroupName", MatchType.Exact, ProfileHeaderToGroupName))
            crit.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "Dealer.ID", MatchType.Exact, OrganizationID))
            Dim agg As Aggregate = New Aggregate(GetType(ProfileHeaderToGroup), "ProfileHeaderToGroupName", AggregateType.Count)
            Return CType(m_ProfileHeaderToGroupMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateHeaderAssign(ByVal HeaderIDCode As Integer) As Boolean
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileHeader.ID", MatchType.Exact, HeaderIDCode))
            Dim ProfileHeaderToGroupColl As ArrayList = m_ProfileHeaderToGroupMapper.RetrieveByCriteria(crit)
            If ProfileHeaderToGroupColl.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is ProfileHeaderToGroup) Then

                CType(InsertArg.DomainObject, ProfileHeaderToGroup).ID = InsertArg.ID
                CType(InsertArg.DomainObject, ProfileHeaderToGroup).MarkLoaded()


            End If

        End Sub
        Public Function Insert(ByVal objGroup As ProfileGroup, ByVal objListDomain As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String = m_userPrincipal.Identity.Name

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim existingHeaderList As ArrayList = objGroup.ProfileHeaderToGroups
                    If existingHeaderList.Count > 0 Then
                        For Each item As ProfileHeaderToGroup In existingHeaderList
                            m_TransactionManager.AddDelete(item)
                        Next
                    End If

                    If objListDomain.Count > 0 Then
                        For Each objDomain As ProfileHeaderToGroup In objListDomain
                            m_TransactionManager.AddInsert(objDomain, _user)
                        Next
                    End If
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
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
            Return objListDomain.Count
        End Function

        Public Function Insert(ByVal objDomain As ProfileHeaderToGroup) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    _user = m_userPrincipal.Identity.Name
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

        Public Function Insert(ByVal arlDomain As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim nReturn As Integer
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    If Not arlDomain Is Nothing Then
                        If arlDomain.Count > 0 Then
                            For Each item As ProfileHeaderToGroup In arlDomain
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                nReturn = item.ID
                            Next
                        End If
                    End If
                    
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = nReturn
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

        Public Function Update(ByVal ObjDomain As ProfileHeaderToGroup, ByVal newDetailList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    _user = m_userPrincipal.Identity.Name

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

        Public Function Update(ByVal objDomain As ProfileHeaderToGroup) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ProfileHeaderToGroupMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Private Function Found(ByVal id As Integer, ByVal Data As ArrayList) As Boolean
            For Each dt As String In Data
                If dt = CStr(id) Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Sub Delete(ByVal objDomain As ProfileHeaderToGroup)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ProfileHeaderToGroupMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteDB(ByVal objDomain As ProfileHeaderToGroup) As Integer
            Dim nResult As Integer = -1
            Try
                m_ProfileHeaderToGroupMapper.Delete(objDomain)
                nResult = 1
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

#End Region

    End Class

End Namespace