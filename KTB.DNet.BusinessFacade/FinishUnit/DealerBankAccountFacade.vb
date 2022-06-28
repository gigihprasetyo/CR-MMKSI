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
'// Generated on 12/4/2008 - 10:26:00 
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

Public Class DealerBankAccountFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DealerBankAccountMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DealerBankAccountMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.DealerBankAccount).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DealerBankAccount))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DealerBankAccount
            Return CType(m_DealerBankAccountMapper.Retrieve(ID), DealerBankAccount)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DealerBankAccountMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_DealerBankAccountMapper.RetrieveScalar(agg, criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DealerBankAccountMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DealerBankAccountMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DealerBankAccount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DealerBankAccountMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DealerBankAccount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DealerBankAccountMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DealerBankAccount As ArrayList = m_DealerBankAccountMapper.RetrieveByCriteria(criterias)
            Return _DealerBankAccount
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerBankAccountColl As ArrayList = m_DealerBankAccountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DealerBankAccountColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerBankAccount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DealerBankAccountColl As ArrayList = m_DealerBankAccountMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerBankAccountColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerBankAccount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerBankAccountMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DealerBankAccountColl As ArrayList = m_DealerBankAccountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DealerBankAccountColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerBankAccountColl As ArrayList = m_DealerBankAccountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DealerBankAccount), columnName, matchOperator, columnValue))
            Return DealerBankAccountColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DealerBankAccount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), columnName, matchOperator, columnValue))

            Return m_DealerBankAccountMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"
        Public Function GetExistingDealerBankAccount(ByVal objDealer As Dealer, ByVal BankAccount As String) As DealerBankAccount
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "BankAccount", MatchType.Exact, BankAccount))
            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                Return CType(list(0), DealerBankAccount)
            Else
                Return Nothing
            End If
        End Function

        Public Function GetExistingDealerBankAccountBankKey(ByVal objDealer As Dealer, ByVal BankAccount As String, ByVal BankKey As String) As DealerBankAccount
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "BankAccount", MatchType.Exact, BankAccount))
            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "BankKey", MatchType.Exact, BankKey))
            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                Return CType(list(0), DealerBankAccount)
            Else
                Return Nothing
            End If
        End Function

        Public Function UpdateExistingBankAccount(ByVal objDealer As Dealer) As Integer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
            Dim list As ArrayList = Me.Retrieve(criterias)
            Dim returnValue As Integer = -1
            'If (Me.IsTaskFree()) Then
            Try
                'Me.SetTaskLocking()
                Dim performTransaction As Boolean = True
                For Each item As DealerBankAccount In list
                    item.Status = CType(EnumMasterDealer.Status.NotActive, Short)
                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                Next

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDealer.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
                'Finally
                '    Me.RemoveTaskLocking()
            End Try
            'End If
        End Function


        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.DealerBankAccount) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim oldDep As DealerBankAccount = GetExistingDealerBankAccount(objDomain.Dealer, objDomain.BankAccount)
                    If oldDep Is Nothing Then
                        m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                        'item.DealerBankAccount = objDomain
                        'm_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Else
                        'm_TransactionManager.AddDelete(item)
                        'item.DealerBankAccount = oldDep
                        'm_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)

                        oldDep.Dealer = objDomain.Dealer
                        oldDep.BankKey = objDomain.BankKey
                        oldDep.BankAccount = objDomain.BankAccount
                        oldDep.BankName = objDomain.BankName
                        m_TransactionManager.AddUpdate(oldDep, m_userPrincipal.Identity.Name)
                    End If
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
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DealerBankAccount) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.DealerBankAccount).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DealerBankAccount).MarkLoaded()

            End If
        End Sub
#End Region

#Region "Custom Method"

        Public Function RetrieveByAccNo(ByVal AccNo As String, ByVal DealerCode As String) As DealerBankAccount
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "BankAccount", MatchType.Exact, AccNo))
            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "Dealer.DealerCode", MatchType.Exact, DealerCode))

            Dim DealerBankAccount As ArrayList = m_DealerBankAccountMapper.RetrieveByCriteria(criterias)
            If (DealerBankAccount.Count > 0) Then
                Return CType(DealerBankAccount(0), DealerBankAccount)
            End If
            Return New DealerBankAccount
        End Function

#End Region

End Class
End Namespace
