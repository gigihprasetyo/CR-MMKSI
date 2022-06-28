 
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
'// Generated on 10/5/2005 - 3:23:28 PM
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

Namespace KTB.DNet.BusinessFacade.OnlinePayment

    Public Class PaymentAssignmentTypeFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PaymentAssignmentTypeMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PaymentAssignmentTypeMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.PaymentAssignmentType).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PaymentAssignmentType))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PaymentAssignmentTypeReff))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PaymentAssignmentType
            Return CType(m_PaymentAssignmentTypeMapper.Retrieve(ID), PaymentAssignmentType)
        End Function

        Public Function Retrieve(ByVal Code As String) As PaymentAssignmentType
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PaymentAssignmentType), "Code", MatchType.Exact, Code))

            Dim PaymentAssignmentTypeColl As ArrayList = m_PaymentAssignmentTypeMapper.RetrieveByCriteria(criterias)
            If (PaymentAssignmentTypeColl.Count > 0) Then
                Return CType(PaymentAssignmentTypeColl(0), PaymentAssignmentType)
            End If
            Return New PaymentAssignmentType
        End Function

        
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PaymentAssignmentTypeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PaymentAssignmentTypeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PaymentAssignmentTypeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PaymentAssignmentType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PaymentAssignmentTypeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PaymentAssignmentType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PaymentAssignmentTypeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveListDDL() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PaymentAssignmentType), "Status", MatchType.Exact, CType(EnumOnlinePayment.OLStatus.Active, Short)))
            Dim _PaymentAssignmentType As ArrayList = m_PaymentAssignmentTypeMapper.RetrieveByCriteria(criterias)
            Return _PaymentAssignmentType
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PaymentAssignmentType As ArrayList = m_PaymentAssignmentTypeMapper.RetrieveByCriteria(criterias)
            Return _PaymentAssignmentType
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PaymentAssignmentTypeColl As ArrayList = m_PaymentAssignmentTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PaymentAssignmentTypeColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PaymentAssignmentType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PaymentAssignmentTypeColl As ArrayList = m_PaymentAssignmentTypeMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PaymentAssignmentTypeColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PaymentAssignmentType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PaymentAssignmentTypeColl As ArrayList = m_PaymentAssignmentTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PaymentAssignmentTypeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PaymentAssignmentTypeColl As ArrayList = m_PaymentAssignmentTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PaymentAssignmentTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PaymentAssignmentTypeColl As ArrayList = m_PaymentAssignmentTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PaymentAssignmentType), columnName, matchOperator, columnValue))
            Return PaymentAssignmentTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PaymentAssignmentType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), columnName, matchOperator, columnValue))

            Return m_PaymentAssignmentTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As PaymentAssignmentType) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PaymentAssignmentTypeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
                nResult = -1
            End Try
            Return nResult
        End Function

        Public Function Update(ByVal objDomain As PaymentAssignmentType) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PaymentAssignmentTypeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
                nResult = -1
            End Try
            Return nResult
        End Function

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PaymentAssignmentType), "Code", AggregateType.Count)
            Return CType(m_PaymentAssignmentTypeMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function DeleteTransaction(ByVal objDomain As KTB.DNet.Domain.PaymentAssignmentType) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As PaymentAssignmentTypeReff In objDomain.PaymentAssignmentTypeReffs
                        item.PaymentAssignmentType = objDomain
                        m_TransactionManager.AddDelete(item)
                    Next
                    m_TransactionManager.AddDelete(objDomain)

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


        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.PaymentAssignmentType) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As PaymentAssignmentTypeReff In objDomain.PaymentAssignmentTypeReffs
                        item.PaymentAssignmentType = objDomain
                        m_TransactionManager.AddUpdate(item, "SAP")
                    Next
                    m_TransactionManager.AddUpdate(objDomain, "SAP")

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

        Public Function InsertTransaction(ByVal objDomain As KTB.DNet.Domain.PaymentAssignmentType) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try

                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    For Each item As PaymentAssignmentTypeReff In objDomain.PaymentAssignmentTypeReffs
                        item.PaymentAssignmentType = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

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

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PaymentAssignmentType) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.PaymentAssignmentType).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PaymentAssignmentType).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is PaymentAssignmentTypeReff) Then

                CType(InsertArg.DomainObject, PaymentAssignmentTypeReff).ID = InsertArg.ID

            End If

        End Sub

        Public Function RetrieveIDDealer(ByVal id As Integer) As PaymentAssignmentType
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PaymentAssignmentType), "Dealer.ID", MatchType.Exact, id))

            Dim PaymentAssignmentTypeColl As ArrayList = m_PaymentAssignmentTypeMapper.RetrieveByCriteria(criterias)
            If (PaymentAssignmentTypeColl.Count > 0) Then
                Return CType(PaymentAssignmentTypeColl(0), PaymentAssignmentType)
            End If
            Return New PaymentAssignmentType
        End Function
#End Region

    End Class

End Namespace