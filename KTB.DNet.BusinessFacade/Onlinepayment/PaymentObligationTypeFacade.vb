
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
'// Generated on 8/14/2007 - 2:31:36 PM
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

Namespace KTB.DNet.BusinessFacade.OnlinePayment

    Public Class PaymentObligationTypeFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PaymentObligationTypeTypeMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PaymentObligationTypeTypeMapper = MapperFactory.GetInstance.GetMapper(GetType(PaymentObligationType).ToString)
            m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PaymentObligationType
            Return CType(m_PaymentObligationTypeTypeMapper.Retrieve(ID), PaymentObligationType)
        End Function

        Public Function Retrieve(ByVal code As String) As PaymentObligationType
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligationType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PaymentObligationType), "Code", MatchType.Exact, code))
            Dim PaymentObligationTypeColl As ArrayList = m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias)
            If (PaymentObligationTypeColl.Count > 0) Then
                Return CType(PaymentObligationTypeColl(0), PaymentObligationType)
            End If
            Return New PaymentObligationType
        End Function

        Public Function RetrieveAllStatus(ByVal code As String) As PaymentObligationType
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligationType), "Code", MatchType.Exact, code))
            Dim PaymentObligationTypeColl As ArrayList = m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias)
            If (PaymentObligationTypeColl.Count > 0) Then
                Return CType(PaymentObligationTypeColl(0), PaymentObligationType)
            End If
            Return New PaymentObligationType
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PaymentObligationTypeTypeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PaymentObligationType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_PaymentObligationTypeTypeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PaymentObligationType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_PaymentObligationTypeTypeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligationType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PaymentObligationType As ArrayList = m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias)
            Return _PaymentObligationType
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligationType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PaymentObligationTypeColl As ArrayList = m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PaymentObligationTypeColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PaymentObligationType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligationType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PaymentObligationTypeColl As ArrayList = m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PaymentObligationTypeColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PaymentObligationTypeColl As ArrayList = m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PaymentObligationTypeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim PaymentObligationTypeColl As ArrayList = m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias)
            Return PaymentObligationTypeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(PaymentObligationType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim CompetitorBrandColl As ArrayList = m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CompetitorBrandColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligationType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PaymentObligationTypeColl As ArrayList = m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PaymentObligationType), columnName, matchOperator, columnValue))
            Return PaymentObligationTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PaymentObligationType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligationType), columnName, matchOperator, columnValue))
            Return m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligationType), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PaymentObligationType), "Code", AggregateType.Count)
            Return CType(m_PaymentObligationTypeTypeMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As PaymentObligationType) As Integer
            Dim iReturn As Integer = 1
            Try
                m_PaymentObligationTypeTypeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As PaymentObligationType) As Integer
            Dim nResult As Integer = 1
            Try
                nResult = m_PaymentObligationTypeTypeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As PaymentObligationType)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_PaymentObligationTypeTypeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function Retrieve(ByVal Code As String, ByVal merk As String) As PaymentObligationType
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligationType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PaymentObligationType), "Code", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(PaymentObligationType), "CompetitorBrand.Code", MatchType.Exact, merk))
            Dim PaymentObligationTypeColl As ArrayList = m_PaymentObligationTypeTypeMapper.RetrieveByCriteria(criterias)
            If (PaymentObligationTypeColl.Count > 0) Then
                Return CType(PaymentObligationTypeColl(0), PaymentObligationType)
            End If
            Return New PaymentObligationType
        End Function



#End Region

    End Class

End Namespace



