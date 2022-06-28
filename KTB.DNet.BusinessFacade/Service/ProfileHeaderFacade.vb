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
'// Generated on 7/16/2007 - 11:55:53 AM
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

Namespace KTB.DNet.BusinessFacade.Service

    Public Class ProfileHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ProfileHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ProfileHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(ProfileHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.ProfileHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.ProfileDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ProfileHeader
            Return CType(m_ProfileHeaderMapper.Retrieve(ID), ProfileHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As ProfileHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ProfileHeader), "Code", MatchType.Exact, Code))

            Dim ProfileHeaderColl As ArrayList = m_ProfileHeaderMapper.RetrieveByCriteria(criterias)
            If (ProfileHeaderColl.Count > 0) Then
                Return CType(ProfileHeaderColl(0), ProfileHeader)
            End If
            Return New ProfileHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ProfileHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ProfileHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ProfileHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(ProfileHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProfileHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(ProfileHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProfileHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ProfileHeader), "Status", MatchType.Exact, CType(EnumStatusProfile.StatusMode.Active, Short)))
            Dim _ProfileHeader As ArrayList = m_ProfileHeaderMapper.RetrieveByCriteria(criterias)
            Return _ProfileHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ProfileHeaderColl As ArrayList = m_ProfileHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ProfileHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ProfileHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ProfileHeaderColl As ArrayList = m_ProfileHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ProfileHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ProfileHeaderColl As ArrayList = m_ProfileHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ProfileHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ProfileHeaderColl As ArrayList = m_ProfileHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ProfileHeader), columnName, matchOperator, columnValue))
            Return ProfileHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(ProfileHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeader), columnName, matchOperator, columnValue))

            Return m_ProfileHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim criteria1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeader), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ProfileHeader), "Code", AggregateType.Count)
            Return CType(m_ProfileHeaderMapper.RetrieveScalar(agg, criteria1), Integer)
        End Function

        Public Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.ProfileHeader) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.ProfileHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.ProfileHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is ProfileDetail) Then
                CType(InsertArg.DomainObject, ProfileDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function Insert(ByVal objDomain As ProfileHeader, ByVal objDomainDetail As ArrayList)
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If Not objDomainDetail Is Nothing Then
                        If objDomainDetail.Count > 0 Then
                            For Each item As ProfileDetail In objDomainDetail
                                item.ProfileHeader = objDomain
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    Else
                        objDomainDetail = New ArrayList
                    End If
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = objDomain.ID
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

            Return returnVal
        End Function

        Public Function Update(ByVal objDomain As ProfileHeader, ByVal objDomainDetails As ArrayList, ByVal objDetailsDeleted As ArrayList)
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    If Not objDetailsDeleted Is Nothing Then
                        If objDetailsDeleted.Count > 0 Then
                            For Each item As ProfileDetail In objDetailsDeleted
                                If item.ID > 0 Then
                                    m_TransactionManager.AddDelete(item)
                                End If
                            Next
                        End If
                    End If
                    If Not objDomainDetails Is Nothing Then
                        If objDomainDetails.Count > 0 Then
                            For Each item As ProfileDetail In objDomainDetails
                                If item.RowStatus <> DBRowStatus.Active Then
                                    item.ProfileHeader = objDomain
                                    item.RowStatus = DBRowStatus.Active
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = objDomain.ID
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
            Return returnVal
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

