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

    Public Class JaminanFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_JaminanMapper As IMapper
        Private m_JaminanDetaailMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_JaminanMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.Jaminan).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.Jaminan))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.JaminanDetail))
        End Sub

#End Region

#Region "Retrieve"
        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_JaminanMapper.RetrieveScalar(agg, criterias)
        End Function
        Public Function Retrieve(ByVal ID As Integer) As Jaminan
            Return CType(m_JaminanMapper.Retrieve(ID), Jaminan)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_JaminanMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_JaminanMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_JaminanMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(Jaminan), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_JaminanMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(Jaminan), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_JaminanMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Jaminan As ArrayList = m_JaminanMapper.RetrieveByCriteria(criterias)
            Return _Jaminan
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim JaminanColl As ArrayList = m_JaminanMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return JaminanColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Jaminan), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim JaminanColl As ArrayList = m_JaminanMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return JaminanColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Jaminan), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_JaminanMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim JaminanColl As ArrayList = m_JaminanMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return JaminanColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim JaminanColl As ArrayList = m_JaminanMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(Jaminan), columnName, matchOperator, columnValue))
            Return JaminanColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(Jaminan), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Jaminan), columnName, matchOperator, columnValue))

            Return m_JaminanMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function GetExistingJaminan(ByVal objDealer As Dealer, ByVal transDate As Date) As Jaminan
            Dim sDate As Date = New Date(transDate.Year, transDate.Month, transDate.Day, 0, 0, 0)
            Dim eDate As Date = New Date(transDate.Year, transDate.Month, transDate.Day, 23, 59, 59)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Jaminan), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(Jaminan), "TransactionDate", MatchType.GreaterOrEqual, sDate))
            criterias.opAnd(New Criteria(GetType(Jaminan), "TransactionDate", MatchType.LesserOrEqual, eDate))
            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                Return CType(list(0), Jaminan)
            Else
                Return Nothing
            End If
        End Function

        Public Sub Update(ByVal objDomain As Jaminan)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As JaminanDetail In objDomain.JaminanDetails
                        item.Jaminan = objDomain
                        'm_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        If item.ID > 0 Then
                            item.Jaminan = objDomain
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Else
                            item.Jaminan = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        End If
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

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.Jaminan) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim oldDep As Jaminan '= GetExistingJaminan(objDomain.Dealer, objDomain.TransactionDate)
                    If oldDep Is Nothing Then
                        m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                        For Each item As JaminanDetail In objDomain.JaminanDetails
                            item.Jaminan = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        performTransaction = False
                        'For Each item As JaminanDetail In oldDep.JaminanDetails
                        '    m_TransactionManager.AddDelete(item)
                        'Next
                        'For Each item As JaminanDetail In objDomain.JaminanDetails
                        '    item.Jaminan = oldDep
                        '    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        'Next
                        'oldDep.BeginingBalance = objDomain.BeginingBalance
                        'oldDep.CreditAmount = objDomain.CreditAmount
                        'oldDep.Dealer = objDomain.Dealer
                        'oldDep.DebetAmount = objDomain.DebetAmount
                        'oldDep.EndBalance = objDomain.EndBalance
                        'oldDep.TransactionDate = objDomain.TransactionDate
                        'm_TransactionManager.AddUpdate(oldDep, m_userPrincipal.Identity.Name)
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
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.Jaminan) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.Jaminan).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.Jaminan).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is JaminanDetail) Then

                CType(InsertArg.DomainObject, JaminanDetail).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"


#End Region

        Public Function Delete(ByVal objDomain As Jaminan) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_JaminanMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As Jaminan) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_JaminanMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

    End Class

End Namespace