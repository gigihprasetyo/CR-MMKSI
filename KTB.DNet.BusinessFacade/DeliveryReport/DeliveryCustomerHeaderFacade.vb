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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.DeliveryReport
    Public Class DeliveryCustomerHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_Mapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_Mapper = MapperFactory.GetInstance().GetMapper(GetType(DeliveryCustomerHeader).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DeliveryCustomerHeader
            Return CType(m_Mapper.Retrieve(ID), DeliveryCustomerHeader)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_Mapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_Mapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_Mapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_Mapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal crit As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_Mapper.RetrieveByCriteria(crit, sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_Mapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DCColl As ArrayList = m_Mapper.RetrieveByCriteria(criterias)
            Return DCColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DCColl As ArrayList = m_Mapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DCColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DCColl As ArrayList = m_Mapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DCColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_Mapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_Mapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DCColl As ArrayList = m_Mapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DCColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), columnName, matchOperator, columnValue))
            Dim DCColl As ArrayList = m_Mapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DCColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), columnName, matchOperator, columnValue))

            Return m_Mapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As DeliveryCustomerHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                m_Mapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DeliveryCustomerHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_Mapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DeliveryCustomerHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_Mapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As DeliveryCustomerHeader) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_Mapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DeliveryCustomerHeader) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DeliveryCustomerHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DeliveryCustomerHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DeliveryCustomerDetail) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DeliveryCustomerDetail).ID = InsertArg.ID
            End If

        End Sub



        Public Function InsertTransaction(ByVal objDeliveryCustomerHeader As DeliveryCustomerHeader, ByVal arrDeliveryCustomerDetails As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()


                    m_TransactionManager.AddInsert(objDeliveryCustomerHeader, m_userPrincipal.Identity.Name)

                    If arrDeliveryCustomerDetails.Count > 0 Then
                        For Each item As DeliveryCustomerDetail In arrDeliveryCustomerDetails
                            item.DeliveryCustomerHeader = objDeliveryCustomerHeader
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    End If


                    m_TransactionManager.PerformTransaction()
                    returnValue = objDeliveryCustomerHeader.ID
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
        Public Sub DeleteTransaction(ByVal objDomain As DeliveryCustomerHeader)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)

                For Each item As DeliveryCustomerDetail In objDomain.DeliveryCustomerDetails
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next

                UpdateTransaction(objDomain, objDomain.DeliveryCustomerDetails)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


        Public Function UpdateTransaction(ByVal objDomain As DeliveryCustomerHeader, ByVal arrDeliveryCustomerDetails As ArrayList) As Integer
            Dim returnValue As Short = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrDeliveryCustomerDetails.Count > 0 Then

                        For Each item As DeliveryCustomerDetail In arrDeliveryCustomerDetails
                            item.DeliveryCustomerHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If
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
            Return returnValue
        End Function
        Public Function UpdateTransaction(ByVal objDomain As DeliveryCustomerHeader, ByVal arrDeliveryCustomerDetails As ArrayList, ByVal arrDeliveryCustomerDetailDeleted As ArrayList) As Integer
            Dim returnValue As Short = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper


                    If arrDeliveryCustomerDetailDeleted.Count > 0 Then
                        For Each item As DeliveryCustomerDetail In arrDeliveryCustomerDetailDeleted
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDeliveryCustomerDetails.Count > 0 Then
                        For Each item As DeliveryCustomerDetail In arrDeliveryCustomerDetails
                            item.DeliveryCustomerHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If

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
            Return returnValue
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
