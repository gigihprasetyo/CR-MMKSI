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
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 7/17/2020 - 10:58:19 AM
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

    Public Class DepositC2InterestHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositC2InterestHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DepositC2InterestHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositC2InterestHeader).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.DepositC2InterestHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.DepositC2InterestDetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DepositC2InterestHeader
            Return CType(m_DepositC2InterestHeaderMapper.Retrieve(ID), DepositC2InterestHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As DepositC2InterestHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2InterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositC2InterestHeader), "DepositC2InterestHeaderCode", MatchType.Exact, Code))

            Dim DepositC2InterestHeaderColl As ArrayList = m_DepositC2InterestHeaderMapper.RetrieveByCriteria(criterias)
            If (DepositC2InterestHeaderColl.Count > 0) Then
                Return CType(DepositC2InterestHeaderColl(0), DepositC2InterestHeader)
            End If
            Return New DepositC2InterestHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositC2InterestHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositC2InterestHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositC2InterestHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositC2InterestHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositC2InterestHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositC2InterestHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositC2InterestHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2InterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositC2InterestHeader As ArrayList = m_DepositC2InterestHeaderMapper.RetrieveByCriteria(criterias)
            Return _DepositC2InterestHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2InterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositC2InterestHeaderColl As ArrayList = m_DepositC2InterestHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DepositC2InterestHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositC2InterestHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositC2InterestHeaderColl As ArrayList = m_DepositC2InterestHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositC2InterestHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositC2InterestHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositC2InterestHeaderColl As ArrayList = m_DepositC2InterestHeaderMapper.RetrieveByCriteria(Criterias, sortColl)
            Return DepositC2InterestHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DepositC2InterestHeader), SortColumn, sortDirection))
            Dim DepositC2InterestHeaderColl As ArrayList = m_DepositC2InterestHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositC2InterestHeaderColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositC2InterestHeaderColl As ArrayList = m_DepositC2InterestHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositC2InterestHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2InterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositC2InterestHeaderColl As ArrayList = m_DepositC2InterestHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositC2InterestHeader), columnName, matchOperator, columnValue))
            Return DepositC2InterestHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositC2InterestHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2InterestHeader), columnName, matchOperator, columnValue))

            Return m_DepositC2InterestHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2InterestHeader), "DepositC2InterestHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DepositC2InterestHeader), "DepositC2InterestHeaderCode", AggregateType.Count)
            Return CType(m_DepositC2InterestHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DepositC2InterestHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DepositC2InterestHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DepositC2InterestHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DepositC2InterestHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DepositC2InterestHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DepositC2InterestHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DepositC2InterestHeader)
            Try
                m_DepositC2InterestHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

#End Region

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.DepositC2InterestHeader) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.DepositC2InterestHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.DepositC2InterestHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.DepositC2InterestDetail) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.DepositC2InterestDetail).ID = InsertArg.ID
            End If

        End Sub

        Public Function InsertTransaction(ByVal objDomain As DepositC2InterestHeader, ByVal objDomainDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If objDomainDetail.Count > 0 Then
                        For Each detail As DepositC2InterestDetail In objDomainDetail
                            detail.DepositC2InterestHeader = objDomain
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

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNET.Domain.DepositC2InterestHeader, ByVal objDomainDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each detail As DepositC2InterestDetail In objDomainDetail

                        Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositC2InterestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(DepositC2InterestDetail), "DepositC2InterestHeader.ID", MatchType.Exact, objDomain.ID))
                        criterias.opAnd(New Criteria(GetType(DepositC2InterestDetail), "Month", MatchType.Exact, "'" & detail.Month & "'"))
                        criterias.opAnd(New Criteria(GetType(DepositC2InterestDetail), "Year", MatchType.Exact, detail.Year))


                        Dim objDepositC2InterestDetailList As ArrayList = New DepositC2InterestDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        Dim objDetail As DepositC2InterestDetail = New DepositC2InterestDetail
                        If objDepositC2InterestDetailList.Count > 0 Then
                            objDetail = objDepositC2InterestDetailList(0)
                        End If
                        If objDetail.ID > 0 Then
                            objDetail.InterestAmount = detail.InterestAmount
                            objDetail.NettoAmount = detail.NettoAmount
                            objDetail.RowStatus = CType(DBRowStatus.Active, Short)
                            m_TransactionManager.AddUpdate(objDetail, m_userPrincipal.Identity.Name)
                        Else
                            detail.DepositC2InterestHeader = objDomain
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
    End Class

End Namespace
