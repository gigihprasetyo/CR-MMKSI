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
'// Generated on 11/09/2005 - 9:04:49 AM
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
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Service

    Public Class EquipmentSalesHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EquipmentSalesHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EquipmentSalesHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(EquipmentSalesHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.EquipmentSalesHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.EquipmentSalesDetail))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.EquipmentSalesPayment))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As EquipmentSalesHeader
            Return CType(m_EquipmentSalesHeaderMapper.Retrieve(ID), EquipmentSalesHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As EquipmentSalesHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EquipmentSalesHeader), "RegPONumber", MatchType.Exact, Code))

            Dim EquipmentSalesHeaderColl As ArrayList = m_EquipmentSalesHeaderMapper.RetrieveByCriteria(criterias)
            If (EquipmentSalesHeaderColl.Count > 0) Then
                Return CType(EquipmentSalesHeaderColl(0), EquipmentSalesHeader)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EquipmentSalesHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EquipmentSalesHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EquipmentSalesHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(EquipmentSalesHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EquipmentSalesHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(EquipmentSalesHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EquipmentSalesHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipmentSalesHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim EquipmentSalesHeaderColl As ArrayList = m_EquipmentSalesHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return EquipmentSalesHeaderColl
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EquipmentSalesHeader As ArrayList = m_EquipmentSalesHeaderMapper.RetrieveByCriteria(criterias)
            Return _EquipmentSalesHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EquipmentSalesHeaderColl As ArrayList = m_EquipmentSalesHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EquipmentSalesHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EquipmentSalesHeaderColl As ArrayList = m_EquipmentSalesHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EquipmentSalesHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EquipmentSalesHeaderColl As ArrayList = m_EquipmentSalesHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EquipmentSalesHeader), columnName, matchOperator, columnValue))
            Return EquipmentSalesHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(EquipmentSalesHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesHeader), columnName, matchOperator, columnValue))

            Return m_EquipmentSalesHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesHeader), "EquipmentSalesHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(EquipmentSalesHeader), "EquipmentSalesHeaderCode", AggregateType.Count)
            Return CType(m_EquipmentSalesHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Sub Update(ByVal objDomain As EquipmentSalesHeader)
            Try
                m_EquipmentSalesHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.EquipmentSalesHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each item As EquipmentSalesDetail In objDomain.EquipmentSalesDetails
                        item.EquipmentSalesHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    'For Each item As EquipmentSalesPayment In objDomain.EquipmentSalesPayments
                    '    item.EquipmentSalesHeader = objDomain
                    '    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    'Next

                    'For i As Integer = 0 To objDomain.EquipmentSalesDetail.Count - 1
                    'CType(objDomain.EquipmentSalesDetail(i), EquipmentSalesDetail).EquipmentSalesHeader = objDomain
                    'm_TransactionManager.AddInsert(objDomain.EquipmentSalesDetail(i), m_userPrincipal.Identity.Name)
                    'Next
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

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.EquipmentSalesHeader) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.EquipmentSalesHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.EquipmentSalesHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is EquipmentSalesDetail) Then

                CType(InsertArg.DomainObject, EquipmentSalesDetail).ID = InsertArg.ID

                'ElseIf (TypeOf InsertArg.DomainObject Is EquipmentSalesPayment) Then

                '    CType(InsertArg.DomainObject, EquipmentSalesPayment).ID = InsertArg.ID

            End If

        End Sub

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.EquipmentSalesHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    'For Each item As EquipmentSalesPayment In objDomain.EquipmentSalesPayments
                    '    item.EquipmentSalesHeader = objDomain
                    '    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    'Next

                    For Each item As EquipmentSalesDetail In objDomain.EquipmentSalesDetails
                        item.EquipmentSalesHeader = objDomain
                        If item.ID <> 0 Then
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        End If

                    Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    'For i As Integer = 0 To objDomain.EquipmentSalesDetail.Count - 1
                    'CType(objDomain.EquipmentSalesDetail(i), EquipmentSalesDetail).EquipmentSalesHeader = objDomain
                    'm_TransactionManager.AddInsert(objDomain.EquipmentSalesDetail(i), m_userPrincipal.Identity.Name)
                    'Next
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

        Public Function UpdateTransaction(ByVal eqColl As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    'For Each item As EquipmentSalesPayment In objDomain.EquipmentSalesPayments
                    '    item.EquipmentSalesHeader = objDomain
                    '    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    'Next
                    For Each objDomain As EquipmentSalesHeader In eqColl
                        For Each item As EquipmentSalesDetail In objDomain.EquipmentSalesDetails
                            item.EquipmentSalesHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                        m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    Next

                    'For i As Integer = 0 To objDomain.EquipmentSalesDetail.Count - 1
                    'CType(objDomain.EquipmentSalesDetail(i), EquipmentSalesDetail).EquipmentSalesHeader = objDomain
                    'm_TransactionManager.AddInsert(objDomain.EquipmentSalesDetail(i), m_userPrincipal.Identity.Name)
                    'Next
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

        Public Sub Delete(ByVal objDomain As EquipmentSalesHeader)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                For Each item As EquipmentSalesDetail In objDomain.EquipmentSalesDetails
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next
                UpdateTransaction(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function UpdateEquipmentStatus(ByVal eqColl As ArrayList)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As EquipmentSalesHeader In eqColl
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next

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