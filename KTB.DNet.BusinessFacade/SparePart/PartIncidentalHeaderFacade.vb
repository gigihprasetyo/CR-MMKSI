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

Namespace KTB.DNet.BusinessFacade.SparePart

    Public Class PartIncidentalHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PartIncidentalHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PartIncidentalHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(PartIncidentalHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PartIncidentalHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PartIncidentalDetail))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.EquipmentSalesPayment))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PartIncidentalHeader
            Return CType(m_PartIncidentalHeaderMapper.Retrieve(ID), PartIncidentalHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As PartIncidentalHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PartIncidentalHeader), "RegPONumber", MatchType.Exact, Code))

            Dim PartIncidentalHeaderColl As ArrayList = m_PartIncidentalHeaderMapper.RetrieveByCriteria(criterias)
            If (PartIncidentalHeaderColl.Count > 0) Then
                Return CType(PartIncidentalHeaderColl(0), PartIncidentalHeader)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PartIncidentalHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PartIncidentalHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PartIncidentalHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PartIncidentalHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PartIncidentalHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PartIncidentalHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PartIncidentalHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PartIncidentalHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PartIncidentalHeaderColl As ArrayList = m_PartIncidentalHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PartIncidentalHeaderColl
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PartIncidentalHeader As ArrayList = m_PartIncidentalHeaderMapper.RetrieveByCriteria(criterias)
            Return _PartIncidentalHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PartIncidentalHeaderColl As ArrayList = m_PartIncidentalHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PartIncidentalHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PartIncidentalHeaderColl As ArrayList = m_PartIncidentalHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PartIncidentalHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PartIncidentalHeaderColl As ArrayList = m_PartIncidentalHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PartIncidentalHeader), columnName, matchOperator, columnValue))
            Return PartIncidentalHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PartIncidentalHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalHeader), columnName, matchOperator, columnValue))

            Return m_PartIncidentalHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalHeader), "PartIncidentalHeaderCode", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(PartIncidentalHeader), "PartIncidentalHeaderCode", AggregateType.Count)
        '    Return CType(m_PartIncidentalHeaderMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

        Public Function ValidateNoSurat(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalHeader), "DealerMailNumber", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PartIncidentalHeader), "DealerMailNumber", AggregateType.Count)
            Return CType(m_PartIncidentalHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Sub Update(ByVal objDomain As PartIncidentalHeader)
            Try
                m_PartIncidentalHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.PartIncidentalHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each item As PartIncidentalDetail In objDomain.PartIncidentalDetails
                        item.PartIncidentalHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    'For Each item As EquipmentSalesPayment In objDomain.EquipmentSalesPayments
                    '    item.PartIncidentalHeader = objDomain
                    '    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    'Next

                    'For i As Integer = 0 To objDomain.PartIncidentalDetail.Count - 1
                    'CType(objDomain.PartIncidentalDetail(i), PartIncidentalDetail).PartIncidentalHeader = objDomain
                    'm_TransactionManager.AddInsert(objDomain.PartIncidentalDetail(i), m_userPrincipal.Identity.Name)
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

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PartIncidentalHeader) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.PartIncidentalHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PartIncidentalHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is PartIncidentalDetail) Then

                CType(InsertArg.DomainObject, PartIncidentalDetail).ID = InsertArg.ID

                'ElseIf (TypeOf InsertArg.DomainObject Is EquipmentSalesPayment) Then

                '    CType(InsertArg.DomainObject, EquipmentSalesPayment).ID = InsertArg.ID

            End If

        End Sub

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.PartIncidentalHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    'For Each item As EquipmentSalesPayment In objDomain.EquipmentSalesPayments
                    '    item.PartIncidentalHeader = objDomain
                    '    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    'Next

                    For Each item As PartIncidentalDetail In objDomain.PartIncidentalDetails
                        item.PartIncidentalHeader = objDomain
                        If item.ID <> 0 Then
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        End If

                    Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    'For i As Integer = 0 To objDomain.PartIncidentalDetail.Count - 1
                    'CType(objDomain.PartIncidentalDetail(i), PartIncidentalDetail).PartIncidentalHeader = objDomain
                    'm_TransactionManager.AddInsert(objDomain.PartIncidentalDetail(i), m_userPrincipal.Identity.Name)
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

        Public Sub Delete(ByVal objDomain As PartIncidentalHeader)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                For Each item As PartIncidentalDetail In objDomain.PartIncidentalDetails
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

        Public Sub Delete(ByVal arrList As ArrayList)
            Try
                For Each item As PartIncidentalHeader In arrList
                    m_PartIncidentalHeaderMapper.Delete(item)
                Next
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As PartIncidentalHeader)
            Try
                m_PartIncidentalHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"


        Public Function UpdatePartInsidentialStatus(ByVal eqColl As ArrayList)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As PartIncidentalHeader In eqColl
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