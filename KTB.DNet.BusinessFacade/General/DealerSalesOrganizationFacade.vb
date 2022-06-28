
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
'// Generated on 26/05/2020 - 23:29:59
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

    Public Class DealerSalesOrganizationFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DealerSalesOrganizationMapper As IMapper

        Private m_TransactionManager As TransactionManager
        Private m_PaymentMethodMapper As IMapper
        Private ID_Insert As Integer

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DealerSalesOrganizationMapper = MapperFactory.GetInstance.GetMapper(GetType(DealerSalesOrganization).ToString)
            Me.m_PaymentMethodMapper = MapperFactory.GetInstance.GetMapper(GetType(DealerPaymentMethod).ToString())
            Me.m_TransactionManager = New TransactionManager
            
        End Sub

        
#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Short) As DealerSalesOrganization
            Return CType(m_DealerSalesOrganizationMapper.Retrieve(ID), DealerSalesOrganization)
        End Function

        Public Function Retrieve(ByVal Code As String) As DealerSalesOrganization
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesOrganization), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerSalesOrganization), "DealerSalesOrganizationCode", MatchType.Exact, Code))

            Dim DealerSalesOrganizationColl As ArrayList = m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias)
            If (DealerSalesOrganizationColl.Count > 0) Then
                Return CType(DealerSalesOrganizationColl(0), DealerSalesOrganization)
            End If
            Return New DealerSalesOrganization
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DealerSalesOrganizationMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerSalesOrganization), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerSalesOrganizationMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerSalesOrganization), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerSalesOrganizationMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesOrganization), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DealerSalesOrganization As ArrayList = m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias)
            Return _DealerSalesOrganization
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesOrganization), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerSalesOrganizationColl As ArrayList = m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DealerSalesOrganizationColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DealerSalesOrganization), SortColumn, sortDirection))
            Dim DealerSalesOrganizationColl As ArrayList = m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerSalesOrganizationColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DealerSalesOrganizationColl As ArrayList = m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DealerSalesOrganizationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, sortColl As SortCollection) As ArrayList

            Dim DealerSalesOrganizationColl As ArrayList = m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerSalesOrganizationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerAdditional), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim DealerSalesOrganizationColl As ArrayList = m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerSalesOrganizationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesOrganization), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerSalesOrganizationColl As ArrayList = m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DealerSalesOrganization), columnName, matchOperator, columnValue))
            Return DealerSalesOrganizationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerSalesOrganization), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesOrganization), columnName, matchOperator, columnValue))

            Return m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesOrganization), "DealerSalesOrganizationCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DealerSalesOrganization), "DealerSalesOrganizationCode", AggregateType.Count)
            Return CType(m_DealerSalesOrganizationMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DealerSalesOrganization) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DealerSalesOrganizationMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function InsertFromWS(ByVal objDomain As DealerSalesOrganization) As Integer
            Dim iReturn As Integer = -2
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSalesOrganization), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(DealerSalesOrganization), "DealerID", MatchType.Exact, objDomain.DealerID))
                criterias.opAnd(New Criteria(GetType(DealerSalesOrganization), "SalesOrganizationCode", MatchType.Exact, objDomain.SalesOrganizationCode))
                criterias.opAnd(New Criteria(GetType(DealerSalesOrganization), "DistributionChannel", MatchType.Exact, objDomain.DistributionChannel))

                Dim DealerSalesOrganizationColl As ArrayList = m_DealerSalesOrganizationMapper.RetrieveByCriteria(criterias)
                If (DealerSalesOrganizationColl.Count > 0) Then
                    objDomain.ID = CType(DealerSalesOrganizationColl(0), DealerSalesOrganization).ID
                    iReturn = m_DealerSalesOrganizationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                Else
                    iReturn = m_DealerSalesOrganizationMapper.Insert(objDomain, m_userPrincipal.Identity.Name)

                End If
                If iReturn > -1 Then
                    For Each item As DealerPaymentMethod In objDomain.GetPaymentMethod()
                        item.DealerID = iReturn
                        m_PaymentMethodMapper.Insert(item, m_userPrincipal.Identity.Name)
                    Next
                End If

            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DealerSalesOrganization) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DealerSalesOrganizationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DealerSalesOrganization)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DealerSalesOrganizationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DealerSalesOrganization)
            Try
                m_DealerSalesOrganizationMapper.Delete(objDomain)
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

    End Class

End Namespace

