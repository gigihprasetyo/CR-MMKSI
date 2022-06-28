
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
'// Generated on 3/14/2016 - 11:39:46 AM
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

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNET.BusinessFacade.Service

    Public Class DepositBInterestHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositBInterestHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DepositBInterestHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBInterestHeader).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.DepositBInterestHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.DepositBInterestDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DepositBInterestHeader
            Return CType(m_DepositBInterestHeaderMapper.Retrieve(ID), DepositBInterestHeader)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositBInterestHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositBInterestHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositBInterestHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBInterestHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBInterestHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBInterestHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBInterestHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBInterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositBInterestHeader As ArrayList = m_DepositBInterestHeaderMapper.RetrieveByCriteria(criterias)
            Return _DepositBInterestHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBInterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBInterestHeaderColl As ArrayList = m_DepositBInterestHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DepositBInterestHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBInterestHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositBInterestHeaderColl As ArrayList = m_DepositBInterestHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositBInterestHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBInterestHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositBInterestHeaderColl As ArrayList = m_DepositBInterestHeaderMapper.RetrieveByCriteria(Criterias, sortColl)
            Return DepositBInterestHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositBInterestHeaderColl As ArrayList = m_DepositBInterestHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositBInterestHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBInterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBInterestHeaderColl As ArrayList = m_DepositBInterestHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositBInterestHeader), columnName, matchOperator, columnValue))
            Return DepositBInterestHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBInterestHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBInterestHeader), columnName, matchOperator, columnValue))

            Return m_DepositBInterestHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"



        Public Function Insert(ByVal objDomain As DepositBInterestHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DepositBInterestHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DepositBInterestHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DepositBInterestHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DepositBInterestHeader)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DepositBInterestHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DepositBInterestHeader)
            Try
                m_DepositBInterestHeaderMapper.Delete(objDomain)
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

            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.DepositBInterestHeader) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.DepositBInterestHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.DepositBInterestHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.DepositBInterestDetail) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.DepositBInterestDetail).ID = InsertArg.ID
            End If

        End Sub

        Public Function InsertTransaction(ByVal objDomain As DepositBInterestHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If objDomain.DepositBInterestDetails.Count > 0 Then
                        For Each detail As DepositBInterestDetail In objDomain.DepositBInterestDetails
                            detail.DepositBInterestHeader = objDomain
                            m_TransactionManager.AddInsert(detail, m_userPrincipal.Identity.Name)
                        Next
                    End If

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

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNET.Domain.DepositBInterestHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each detail As DepositBInterestDetail In objDomain.DepositBInterestDetails

                        Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositBInterestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(DepositBInterestDetail), "DepositBInterestHeader.ID", MatchType.Exact, detail.DepositBInterestHeader.ID))
                        criterias.opAnd(New Criteria(GetType(DepositBInterestDetail), "Month", MatchType.Exact, detail.Month))
                        criterias.opAnd(New Criteria(GetType(DepositBInterestDetail), "Year", MatchType.Exact, detail.Year))


                        Dim objDepositBInterestDetailList As ArrayList = New DepositBInterestDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        Dim objDetail As DepositBInterestDetail = New DepositBInterestDetail
                        If objDepositBInterestDetailList.Count > 0 Then
                            objDetail = objDepositBInterestDetailList(0)
                        End If
                        If objDetail.ID > 0 Then
                            objDetail.InterestAmount = detail.InterestAmount
                            objDetail.NettoAmount = detail.NettoAmount
                            objDetail.RowStatus = CType(DBRowStatus.Active, Short)
                            m_TransactionManager.AddUpdate(objDetail, m_userPrincipal.Identity.Name)
                        Else
                            detail.DepositBInterestHeader = objDomain
                            m_TransactionManager.AddInsert(detail, m_userPrincipal.Identity.Name)
                        End If
                    Next
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

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
#End Region

    End Class

End Namespace

