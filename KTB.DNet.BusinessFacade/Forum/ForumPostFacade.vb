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
'// Generated on 7/16/2007 - 11:37:09 AM
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

Imports KTb.DNet.Domain
Imports KTb.DNet.Domain.Search
Imports KTb.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.BusinessForum

    Public Class ForumPostFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ForumPostMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ForumPostMapper = MapperFactory.GetInstance.GetMapper(GetType(ForumPost).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ForumPost
            Return CType(m_ForumPostMapper.Retrieve(ID), ForumPost)
        End Function

        Public Function Retrieve(ByVal Code As String) As ForumPost
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPost), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ForumPost), "ForumPostCode", MatchType.Exact, Code))

            Dim ForumPostColl As ArrayList = m_ForumPostMapper.RetrieveByCriteria(criterias)
            If (ForumPostColl.Count > 0) Then
                Return CType(ForumPostColl(0), ForumPost)
            End If
            Return New ForumPost
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ForumPostMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ForumPostMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ForumPostMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ForumPost), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ForumPostMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ForumPost), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ForumPostMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPost), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ForumPost As ArrayList = m_ForumPostMapper.RetrieveByCriteria(criterias)
            Return _ForumPost
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPost), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ForumPostColl As ArrayList = m_ForumPostMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ForumPostColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ForumPostColl As ArrayList = m_ForumPostMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ForumPostColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPost), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ForumPostColl As ArrayList = m_ForumPostMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ForumPost), columnName, matchOperator, columnValue))
            Return ForumPostColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ForumPost), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPost), columnName, matchOperator, columnValue))

            Return m_ForumPostMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(ForumPost), SortColumn, sortDirection))
            Dim ForumPostColl As ArrayList = m_ForumPostMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ForumPostColl
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ForumPost), "ForumPostCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ForumPost), "ForumPostCode", AggregateType.Count)
            Return CType(m_ForumPostMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As ForumPost) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ForumPostMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function InsertTransaction(ByVal objForumPost As ForumPost) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim objForumTopic As ForumTopic = objForumPost.ForumTopic
                    objForumTopic.LastPostDate = Date.Now
                    m_TransactionManager.AddUpdate(objForumTopic, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddInsert(objForumPost, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objForumPost.ID
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


        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is ForumPost) Then
                CType(InsertArg.DomainObject, ForumPost).ID = InsertArg.ID
                CType(InsertArg.DomainObject, ForumPost).MarkLoaded()

            End If
        End Sub


#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

