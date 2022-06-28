#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2008

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
'// Copyright  2008
'// ---------------------
'// $History      : $
'// Generated on 12/24/2008 - 09:56:00
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

Imports KTB.Dnet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.Dnet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit

Public Class DepositAStatusHistoryFacade
        Inherits AbstractFacade
    
#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositAStatusHistoryMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DepositAStatusHistoryMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.DepositAStatusHistory).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DepositAStatusHistory))
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_DepositAStatusHistoryMapper.RetrieveScalar(agg, criterias)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As DepositAStatusHistory
            Return CType(m_DepositAStatusHistoryMapper.Retrieve(ID), DepositAStatusHistory)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositAStatusHistoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositAStatusHistoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositAStatusHistoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DepositAStatusHistoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DepositAStatusHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositAStatusHistory As ArrayList = m_DepositAStatusHistoryMapper.RetrieveByCriteria(criterias)
            Return _DepositAStatusHistory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAStatusHistoryColl As ArrayList = m_DepositAStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DepositAStatusHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositAStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositAStatusHistoryColl As ArrayList = m_DepositAStatusHistoryMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositAStatusHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositAStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositAStatusHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositAStatusHistoryColl As ArrayList = m_DepositAStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositAStatusHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAStatusHistoryColl As ArrayList = m_DepositAStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositAStatusHistory), columnName, matchOperator, columnValue))
            Return DepositAStatusHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAStatusHistory), columnName, matchOperator, columnValue))

            Return m_DepositAStatusHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Public Function GetExistingDepositAStatusHistory(ByVal DocNumber As String) As DepositAStatusHistory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositAStatusHistory), "DocNumber", MatchType.Exact, DocNumber))
            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                Return CType(list(0), DepositAStatusHistory)
            Else
                Return Nothing
            End If
        End Function

        'Public Sub Update(ByVal objDomain As DepositAStatusHistory)
        '    Dim returnValue As Integer = -1
        '    If (Me.IsTaskFree()) Then
        '        Try
        '            Me.SetTaskLocking()
        '            Dim performTransaction As Boolean = True
        '            Dim ObjMapper As IMapper

        '            m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

        '            If performTransaction Then
        '                m_TransactionManager.PerformTransaction()
        '                returnValue = objDomain.ID
        '            End If
        '        Catch ex As Exception
        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '            If rethrow Then
        '                Throw
        '            End If
        '        Finally
        '            Me.RemoveTaskLocking()
        '        End Try
        '    End If

        'End Sub

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.DepositAStatusHistory) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    'Dim oldDep As DepositAStatusHistory = GetExistingDepositAStatusHistory(objDomain.DocNumber)
                    Dim oldDep As DepositAStatusHistory = Nothing
                    If oldDep Is Nothing Then
                        m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    Else
                        oldDep.DocNumber = objDomain.DocNumber
                        oldDep.DocType = objDomain.DocType
                        oldDep.OldStatus = objDomain.OldStatus
                        oldDep.NewStatus = objDomain.NewStatus
                        If objDomain.DocNumber = oldDep.DocNumber And oldDep.OldStatus = objDomain.OldStatus Then
                            m_TransactionManager.AddUpdate(oldDep, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(oldDep, m_userPrincipal.Identity.Name)
                        End If
                    End If
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        If objDomain.ID > 0 Then
                            returnValue = objDomain.ID
                        Else
                            returnValue = oldDep.ID
                        End If
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
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DepositAStatusHistory) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DepositAStatusHistory).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DepositAStatusHistory).MarkLoaded()
            End If
        End Sub
#End Region

#Region "Custom Method"
#End Region

End Class
End Namespace
