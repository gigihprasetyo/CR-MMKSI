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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 7/26/2007 - 11:51:06 AM
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
Imports KTB.DNet.BusinessFacade.Profile
#End Region

Namespace KTB.DNet.BusinessFacade.Service

    Public Class CustomerDealerFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CustomerDealerMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_CustomerDealerMapper = MapperFactory.GetInstance.GetMapper(GetType(CustomerDealer).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CustomerDealer
            Return CType(m_CustomerDealerMapper.Retrieve(ID), CustomerDealer)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CustomerDealerMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CustomerDealerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CustomerDealerMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerDealerMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CustomerDealerMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _CustomerDealer As ArrayList = m_CustomerDealerMapper.RetrieveByCriteria(criterias)
            Return _CustomerDealer
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CustomerDealerColl As ArrayList = m_CustomerDealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CustomerDealerColl
        End Function
        Public Function RetrieveByRefCode(ByVal RefCode As String, ByVal _DealerID As Integer) As Customer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.Code", MatchType.Exact, RefCode))
            criterias.opAnd(New Criteria(GetType(CustomerDealer), "Dealer.ID", MatchType.Exact, _DealerID))

            Dim CustomerColl As ArrayList = m_CustomerDealerMapper.RetrieveByCriteria(criterias)
            If (CustomerColl.Count > 0) Then
                Return CType(CustomerColl(0), CustomerDealer).Customer
            End If
            Return New Customer
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CustomerDealerColl As ArrayList = m_CustomerDealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CustomerDealerColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim CustomerDealerColl As ArrayList = m_CustomerDealerMapper.RetrieveByCriteria(criterias)
            Return CustomerDealerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CustomerDealerColl As ArrayList = m_CustomerDealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(CustomerDealer), columnName, matchOperator, columnValue))
            Return CustomerDealerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CustomerDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), columnName, matchOperator, columnValue))

            Return m_CustomerDealerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(CustomerDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim CustomerDealerColl As ArrayList = m_CustomerDealerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CustomerDealerColl
        End Function
#End Region

#Region "CUD"
      

        Public Sub DeleteFromDB(ByVal objDomain As CustomerDealer)
            Try
                m_CustomerDealerMapper.Delete(objDomain)

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Update(ByVal objDomain As CustomerDealer) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_CustomerDealerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function GetCustomerDealer(ByVal cust As KTB.DNet.Domain.Customer, ByVal objDealer As Dealer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerDealer), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.ID", MatchType.Exact, cust.ID))
            Return Me.Retrieve(criterias)
        End Function

        Public Function Insert(ByVal objDomain As CustomerDealer) As Integer
            Dim iReturn As Integer = -2
            Try
                If GetCustomerDealer(objDomain.Customer, objDomain.Dealer).Count = 0 Then
                    m_CustomerDealerMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                End If
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

