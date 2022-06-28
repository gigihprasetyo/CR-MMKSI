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
'// Generated on 8/14/2007 - 2:31:58 PM
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

    Public Class CompetitorBrandFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CompetitorBrandMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_CompetitorBrandMapper = MapperFactory.GetInstance.GetMapper(GetType(CompetitorBrand).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CompetitorBrand
            Return CType(m_CompetitorBrandMapper.Retrieve(ID), CompetitorBrand)
        End Function

        Public Function Retrieve(ByVal Code As String) As CompetitorBrand
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CompetitorBrand), "CompetitorBrandCode", MatchType.Exact, Code))

            Dim CompetitorBrandColl As ArrayList = m_CompetitorBrandMapper.RetrieveByCriteria(criterias)
            If (CompetitorBrandColl.Count > 0) Then
                Return CType(CompetitorBrandColl(0), CompetitorBrand)
            End If
            Return New CompetitorBrand
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CompetitorBrandMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CompetitorBrandMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CompetitorBrandMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CompetitorBrand), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CompetitorBrandMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CompetitorBrand), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CompetitorBrandMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _CompetitorBrand As ArrayList = m_CompetitorBrandMapper.RetrieveByCriteria(criterias)
            Return _CompetitorBrand
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CompetitorBrandColl As ArrayList = m_CompetitorBrandMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CompetitorBrandColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CompetitorBrandColl As ArrayList = m_CompetitorBrandMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CompetitorBrandColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(CompetitorBrand), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim CompetitorBrandColl As ArrayList = m_CompetitorBrandMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CompetitorBrandColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CompetitorBrandColl As ArrayList = m_CompetitorBrandMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(CompetitorBrand), columnName, matchOperator, columnValue))
            Return CompetitorBrandColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CompetitorBrand), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorBrand), columnName, matchOperator, columnValue))

            Return m_CompetitorBrandMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "CompetitorBrandCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(CompetitorBrand), "CompetitorBrandCode", AggregateType.Count)
            Return CType(m_CompetitorBrandMapper.RetrieveScalar(agg, crit), Integer)
        End Function
        Public Function Insert(ByVal objDomain As CompetitorBrand) As Integer
            Dim iReturn As Integer = 1
            Try
                m_CompetitorBrandMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As CompetitorBrand) As Integer
            Dim nResult As Integer = 1
            Try
                nResult = m_CompetitorBrandMapper.Update(objDomain, m_userPrincipal.Identity.Name)
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

#Region "Custom Method"

#End Region

    End Class

End Namespace

