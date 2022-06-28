
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
'// Copyright  2016
'// ---------------------
'// $History      : $
'// Generated on 3/14/2016 - 11:41:49 AM
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

Namespace KTB.DNET.BusinessFacade.Service

    Public Class DepositBPlafonFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositBPlafonMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DepositBPlafonMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBPlafon).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.DepositBPlafon))

        End Sub

#End Region

#Region "Retrieve"
        Public Function RetrieveByDealerID(ByVal DealerID As Short, ByVal PeriodePlafon As Short) As KTB.DNET.Domain.DepositBPlafon
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.DepositBPlafon), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.DepositBPlafon), "Dealer.ID", MatchType.Exact, DealerID))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.DepositBPlafon), "PeriodePlafon", MatchType.Exact, PeriodePlafon))

            Dim DealerColl As ArrayList = m_DepositBPlafonMapper.RetrieveByCriteria(criterias)
            If (DealerColl.Count > 0) Then
                Return CType(DealerColl(0), KTB.DNET.Domain.DepositBPlafon)
            End If
            Return New KTB.DNET.Domain.DepositBPlafon
        End Function

        Public Function Retrieve(ByVal ID As Integer) As DepositBPlafon
            Return CType(m_DepositBPlafonMapper.Retrieve(ID), DepositBPlafon)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositBPlafonMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositBPlafonMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositBPlafonMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBPlafon), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBPlafonMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBPlafon), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBPlafonMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPlafon), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositBPlafon As ArrayList = m_DepositBPlafonMapper.RetrieveByCriteria(criterias)
            Return _DepositBPlafon
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPlafon), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBPlafonColl As ArrayList = m_DepositBPlafonMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DepositBPlafonColl
        End Function


        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBPlafon), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositBPlafonColl As ArrayList = m_DepositBPlafonMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositBPlafonColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositBPlafonColl As ArrayList = m_DepositBPlafonMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositBPlafonColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPlafon), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBPlafonColl As ArrayList = m_DepositBPlafonMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositBPlafon), columnName, matchOperator, columnValue))
            Return DepositBPlafonColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBPlafon), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBPlafon), columnName, matchOperator, columnValue))

            Return m_DepositBPlafonMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function InsertByList(ByVal ArlDepositBPlafons As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If ArlDepositBPlafons.Count > 0 Then
                        For Each items As DepositBPlafon In ArlDepositBPlafons
                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
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

        Public Function Insert(ByVal objDomain As DepositBPlafon) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DepositBPlafonMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DepositBPlafon) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DepositBPlafonMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DepositBPlafon)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DepositBPlafonMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DepositBPlafon)
            Try
                m_DepositBPlafonMapper.Delete(objDomain)
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
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.DepositBPlafon) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.DepositBPlafon).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.DepositBPlafon).MarkLoaded()
            End If
        End Sub
#End Region

    End Class

End Namespace

