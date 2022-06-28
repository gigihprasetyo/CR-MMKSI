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
'// Generated on 8/20/2007 - 1:34:24 PM
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

Namespace KTB.DNet.BusinessFacade.Event

    Public Class EventInfoFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EventInfoMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EventInfoMapper = MapperFactory.GetInstance.GetMapper(GetType(EventInfo).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(EventInfo))
            Me.DomainTypeCollection.Add(GetType(EventSales))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As EventInfo
            Return CType(m_EventInfoMapper.Retrieve(ID), EventInfo)
        End Function

        Public Function Retrieve(ByVal Code As String) As EventInfo
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventInfo), "EventRequestNo", MatchType.Exact, Code))

            Dim arr As ArrayList = m_EventInfoMapper.RetrieveByCriteria(criterias)
            If (arr.Count > 0) Then
                Return CType(arr(0), EventInfo)
            End If
            Return New EventInfo
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EventInfoMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EventInfoMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EventInfoMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventInfoMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventInfoMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EventInfo As ArrayList = m_EventInfoMapper.RetrieveByCriteria(criterias)
            Return _EventInfo
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventInfoColl As ArrayList = m_EventInfoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventInfoColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EventInfoColl As ArrayList = m_EventInfoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EventInfoColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim EventInfoColl As ArrayList = m_EventInfoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return EventInfoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventInfoColl As ArrayList = m_EventInfoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EventInfo), columnName, matchOperator, columnValue))
            Return EventInfoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventInfo), columnName, matchOperator, columnValue))

            Return m_EventInfoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As EventInfo) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_EventInfoMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function InsertTransaction(ByVal objDomain As EventInfo) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                Catch ex As Exception
                    returnValue = -1
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

        Public Sub DeleteFromDB(ByVal objDomain As EventInfo)
            Try
                m_EventInfoMapper.Delete(objDomain)

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Update(ByVal objDomain As EventInfo) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_EventInfoMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Function InsertEventSales(ByVal objDomain As KTB.DNet.Domain.EventSales, ByVal objEventSales As EventSales) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objEventSales, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objEventSales.ID
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

        Public Function UpdateEventInfo(ByVal objDomain As KTB.DNet.Domain.EventInfo, ByVal arrEventSales As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrEventSales.Count > 0 Then
                        For Each objEventSales As EventSales In arrEventSales
                            If IsNothing(New EventSalesFacade(Me.m_userPrincipal).ValidateItem(objDomain.ID, objEventSales.VechileType.VechileTypeCode)) Then
                                objEventSales.EventInfo = objDomain
                                m_TransactionManager.AddInsert(objEventSales, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddUpdate(objEventSales, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        Public Function UpdateEventInfoForApproval(ByVal arrl As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrl.Count > 0 Then
                        For Each objEventInfo As EventInfo In arrl
                            m_TransactionManager.AddUpdate(objEventInfo, m_userPrincipal.Identity.Name)
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


        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is EventInfo) Then
                CType(InsertArg.DomainObject, EventInfo).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EventInfo).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is EventSales) Then
                CType(InsertArg.DomainObject, EventSales).ID = InsertArg.ID
            End If
        End Sub
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

