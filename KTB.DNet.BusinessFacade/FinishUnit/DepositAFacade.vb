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
'// Generated on 9/26/2005 - 1:43:31 PM
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

    Public Class DepositAFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositAMapper As IMapper
        Private m_DepositADetaailMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DepositAMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.DepositA).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DepositA))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DepositADetail))
        End Sub

#End Region

#Region "Retrieve"
        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_DepositAMapper.RetrieveScalar(agg, criterias)
        End Function
        Public Function Retrieve(ByVal ID As Integer) As DepositA
            Return CType(m_DepositAMapper.Retrieve(ID), DepositA)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositAMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositAMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositAMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositA), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DepositAMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositA), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DepositAMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositA As ArrayList = m_DepositAMapper.RetrieveByCriteria(criterias)
            Return _DepositA
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAColl As ArrayList = m_DepositAMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DepositAColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositA), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositAColl As ArrayList = m_DepositAMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositAColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositA), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositAMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositAColl As ArrayList = m_DepositAMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositAColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAColl As ArrayList = m_DepositAMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositA), columnName, matchOperator, columnValue))
            Return DepositAColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositA), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositA), columnName, matchOperator, columnValue))

            Return m_DepositAMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function GetExistingDepositA(ByVal objDealer As Dealer, ByVal transDate As Date) As DepositA
            Dim sDate As Date = New Date(transDate.Year, transDate.Month, transDate.Day, 0, 0, 0)
            Dim eDate As Date = New Date(transDate.Year, transDate.Month, transDate.Day, 23, 59, 59)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositA), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(DepositA), "TransactionDate", MatchType.GreaterOrEqual, sDate))
            criterias.opAnd(New Criteria(GetType(DepositA), "TransactionDate", MatchType.LesserOrEqual, eDate))
            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                Return CType(list(0), DepositA)
            Else
                Return Nothing
            End If
        End Function

        'CR Split Deposit  A
        Public Function GetExistingDepositA(ByVal objDealer As Dealer, ByVal transDate As Date, ByVal ProductCode As String) As DepositA
            Dim sDate As Date = New Date(transDate.Year, transDate.Month, transDate.Day, 0, 0, 0)
            Dim eDate As Date = New Date(transDate.Year, transDate.Month, transDate.Day, 23, 59, 59)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositA), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositA), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(DepositA), "TransactionDate", MatchType.GreaterOrEqual, sDate))
            criterias.opAnd(New Criteria(GetType(DepositA), "TransactionDate", MatchType.LesserOrEqual, eDate))
            criterias.opAnd(New Criteria(GetType(DepositA), "ProductCategory.Code", MatchType.Exact, ProductCode))
            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                Return CType(list(0), DepositA)
            Else
                Return Nothing
            End If
        End Function

        Public Sub Update(ByVal objDomain As DepositA)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As DepositADetail In objDomain.DepositADetails
                        item.DepositA = objDomain
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

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

        End Sub

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.DepositA) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim oldDep As DepositA = GetExistingDepositA(objDomain.Dealer, objDomain.TransactionDate, objDomain.ProductCategory.Code)
                    If oldDep Is Nothing Then
                        m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                        For Each item As DepositADetail In objDomain.DepositADetails
                            item.DepositA = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        For Each item As DepositADetail In oldDep.DepositADetails
                            m_TransactionManager.AddDelete(item)
                        Next
                        For Each item As DepositADetail In objDomain.DepositADetails
                            item.DepositA = oldDep
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                        oldDep.BeginingBalance = objDomain.BeginingBalance
                        oldDep.CreditAmount = objDomain.CreditAmount
                        oldDep.Dealer = objDomain.Dealer
                        oldDep.DebetAmount = objDomain.DebetAmount
                        oldDep.EndBalance = objDomain.EndBalance
                        oldDep.TransactionDate = objDomain.TransactionDate
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
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DepositA) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.DepositA).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DepositA).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DepositADetail) Then

                CType(InsertArg.DomainObject, DepositADetail).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"


#End Region

    End Class

End Namespace