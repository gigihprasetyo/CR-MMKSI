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
'// Generated on 7/16/2007 - 11:06:38 AM
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

Namespace KTB.DNet.BusinessFacade.Sparepart

    Public Class PendingOrderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PendingOrderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PendingOrderMapper = MapperFactory.GetInstance.GetMapper(GetType(PendingOrder).ToString)
            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PendingOrder
            Return CType(m_PendingOrderMapper.Retrieve(ID), PendingOrder)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PendingOrderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PendingOrderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PendingOrderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PendingOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PendingOrderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PendingOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PendingOrderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PendingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PendingOrder As ArrayList = m_PendingOrderMapper.RetrieveByCriteria(criterias)
            Return _PendingOrder
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PendingOrderColl As ArrayList = m_PendingOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PendingOrderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PendingOrderColl As ArrayList = m_PendingOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PendingOrderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PendingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PendingOrderColl As ArrayList = m_PendingOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PendingOrder), columnName, matchOperator, columnValue))
            Return PendingOrderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PendingOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PendingOrder), columnName, matchOperator, columnValue))

            Return m_PendingOrderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PendingOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PendingOrderColl As ArrayList = m_PendingOrderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PendingOrderColl
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Private Function GetPendingOrder(ByVal objPending As PendingOrder) As PendingOrder
            Dim sDate As Date = New Date(objPending.IssueDate.Year, objPending.IssueDate.Month, objPending.IssueDate.Day, 0, 0, 0)
            Dim eDate As Date = New Date(objPending.IssueDate.Year, objPending.IssueDate.Month, objPending.IssueDate.Day, 23, 59, 59)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PendingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PendingOrder), "Dealer.ID", MatchType.Exact, objPending.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(PendingOrder), "SparePartPO.PONumber", MatchType.Exact, objPending.SparePartPO.PONumber))
            criterias.opAnd(New Criteria(GetType(PendingOrder), "TotalAmount", MatchType.Exact, objPending.TotalAmount))
            criterias.opAnd(New Criteria(GetType(PendingOrder), "IssueDate", MatchType.GreaterOrEqual, sDate))
            criterias.opAnd(New Criteria(GetType(PendingOrder), "IssueDate", MatchType.LesserOrEqual, eDate))
            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                Return list(0)
            End If
            Return New PendingOrder
        End Function

        Public Function Insert(ByVal objDomain As PendingOrder) As Integer
            Dim iReturn As Integer = -2
            Try
                m_PendingOrderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function InsertFromWSM(ByVal objDomain As PendingOrder) As Integer
            Dim iReturn As Integer = -2
            Try
                Dim oldPendingOrder As PendingOrder = GetPendingOrder(objDomain)
                If oldPendingOrder.ID > 0 Then
                    oldPendingOrder.BillingNumber = objDomain.BillingNumber
                    oldPendingOrder.DepositC2 = objDomain.DepositC2
                    oldPendingOrder.PPN = objDomain.PPN
                    oldPendingOrder.Retail = objDomain.Retail
                    m_PendingOrderMapper.Update(oldPendingOrder, m_userPrincipal.Identity.Name)
                Else
                    m_PendingOrderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                End If
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub DeleteFromDB(ByVal objDomain As PendingOrder)
            Try
                m_PendingOrderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Update(ByVal objDomain As PendingOrder) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PendingOrderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeletePendingOrders(ByVal objDomains As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                   
                    For Each item As PendingOrder In objDomains
                        'item.RowStatus = DBRowStatus.Deleted
                        'm_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        m_TransactionManager.AddDelete(item)
                    Next
                 
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

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

