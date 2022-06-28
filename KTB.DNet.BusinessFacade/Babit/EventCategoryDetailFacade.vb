
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 14/05/2019 - 16:02:40
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class EventCategoryDetailFacade
        Inherits AbstractFacade
#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EventCategoryDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EventCategoryDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(EventCategoryDetail).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(EventCategoryDetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As EventCategoryDetail
            Return CType(m_EventCategoryDetailMapper.Retrieve(ID), EventCategoryDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As EventCategoryDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventCategoryDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventCategoryDetail), "EventCategoryDetailCode", MatchType.Exact, Code))

            Dim EventCategoryDetailColl As ArrayList = m_EventCategoryDetailMapper.RetrieveByCriteria(criterias)
            If (EventCategoryDetailColl.Count > 0) Then
                Return CType(EventCategoryDetailColl(0), EventCategoryDetail)
            End If
            Return New EventCategoryDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EventCategoryDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EventCategoryDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EventCategoryDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventCategoryDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventCategoryDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventCategoryDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventCategoryDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventCategoryDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EventCategoryDetail As ArrayList = m_EventCategoryDetailMapper.RetrieveByCriteria(criterias)
            Return _EventCategoryDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventCategoryDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventCategoryDetailColl As ArrayList = m_EventCategoryDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventCategoryDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(EventCategoryDetail), SortColumn, sortDirection))
            Dim EventCategoryDetailColl As ArrayList = m_EventCategoryDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return EventCategoryDetailColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EventCategoryDetailColl As ArrayList = m_EventCategoryDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EventCategoryDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventCategoryDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventCategoryDetailColl As ArrayList = m_EventCategoryDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EventCategoryDetail), columnName, matchOperator, columnValue))
            Return EventCategoryDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventCategoryDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventCategoryDetail), columnName, matchOperator, columnValue))

            Return m_EventCategoryDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventCategoryDetail), "EventCategoryDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(EventCategoryDetail), "EventCategoryDetailCode", AggregateType.Count)
            Return CType(m_EventCategoryDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As EventCategoryDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_EventCategoryDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As EventCategoryDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_EventCategoryDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As EventCategoryDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_EventCategoryDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As EventCategoryDetail)
            Try
                m_EventCategoryDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is EventCategoryHeader) Then
                CType(InsertArg.DomainObject, EventCategoryHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EventCategoryHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is EventCategoryDetail) Then
                CType(InsertArg.DomainObject, EventCategoryDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal objHeader As EventCategoryHeader, ByVal arrDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objHeader, m_userPrincipal.Identity.Name)

                    If arrDetail.Count > 0 Then
                        For Each oDetails As EventCategoryDetail In arrDetail
                            'oDetails.EventCategoryHeader = objHeader
                            m_TransactionManager.AddInsert(oDetails, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objHeader.ID
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

        Public Function UpdateTransaction(ByVal objHeader As EventCategoryHeader, ByVal arrDetail As ArrayList, ByVal arrDeletedDetail As ArrayList, ByVal arrFiles As ArrayList, ByVal arrDeletedFiles As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrDeletedDetail.Count > 0 Then
                        For Each item As EventCategoryDetail In arrDeletedDetail
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDeletedFiles.Count > 0 Then
                        For Each item As EventCategoryDetail In arrDeletedFiles
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDetail.Count > 0 Then
                        For Each item As EventCategoryDetail In arrDetail
                            'item.EventCategoryHeader = objHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arrFiles.Count > 0 Then
                        For Each item As EventCategoryDetail In arrFiles
                            'item.EventCategoryHeader = objHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If

                    m_TransactionManager.AddUpdate(objHeader, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objHeader.ID
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
#End Region

    End Class

End Namespace

