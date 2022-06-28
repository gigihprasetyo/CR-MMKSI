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
'// Generated on 8/2/2007 - 4:32:58 PM
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

Namespace KTB.DNet.BusinessFacade.Salesman

    Public Class SalesmanUnifDistributionFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SalesmanUnifDistributionMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SalesmanUnifDistributionMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesmanUnifDistribution).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SalesmanUnifDistribution
            Return CType(m_SalesmanUnifDistributionMapper.Retrieve(ID), SalesmanUnifDistribution)
        End Function

        Public Function Retrieve(ByVal Code As String) As SalesmanUnifDistribution
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifDistribution), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanUnifDistribution), "SalesmanUnifDistributionCode", MatchType.Exact, Code))

            Dim SalesmanUnifDistributionColl As ArrayList = m_SalesmanUnifDistributionMapper.RetrieveByCriteria(criterias)
            If (SalesmanUnifDistributionColl.Count > 0) Then
                Return CType(SalesmanUnifDistributionColl(0), SalesmanUnifDistribution)
            End If
            Return New SalesmanUnifDistribution
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesmanUnifDistributionMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SalesmanUnifDistributionMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SalesmanUnifDistributionMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUnifDistribution), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanUnifDistributionMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUnifDistribution), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanUnifDistributionMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifDistribution), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SalesmanUnifDistribution As ArrayList = m_SalesmanUnifDistributionMapper.RetrieveByCriteria(criterias)
            Return _SalesmanUnifDistribution
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifDistribution), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanUnifDistributionColl As ArrayList = m_SalesmanUnifDistributionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesmanUnifDistributionColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SalesmanUnifDistribution), SortColumn, sortDirection))
            Dim SalesmanUnifDistributionColl As ArrayList = m_SalesmanUnifDistributionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SalesmanUnifDistributionColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SalesmanUnifDistributionColl As ArrayList = m_SalesmanUnifDistributionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SalesmanUnifDistributionColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifDistribution), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanUnifDistributionColl As ArrayList = m_SalesmanUnifDistributionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SalesmanUnifDistribution), columnName, matchOperator, columnValue))
            Return SalesmanUnifDistributionColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanUnifDistribution), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifDistribution), columnName, matchOperator, columnValue))

            Return m_SalesmanUnifDistributionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifDistribution), "SalesmanUnifDistributionCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SalesmanUnifDistribution), "SalesmanUnifDistributionCode", AggregateType.Count)
            Return CType(m_SalesmanUnifDistributionMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateCode(ByVal Code As String, ByVal Id As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifDistribution), "SalesmanUnifDistributionCode", MatchType.Exact, Code))

            If Id <> 0 Then
                crit.opAnd(New Criteria(GetType(SalesmanUnifDistribution), "ID", MatchType.No, Id))
            End If

            Dim agg As Aggregate = New Aggregate(GetType(SalesmanUnifDistribution), "SalesmanUnifDistributionCode", AggregateType.Count)
            Return CType(m_SalesmanUnifDistributionMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SalesmanUnifDistribution) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SalesmanUnifDistributionMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SalesmanUnifDistribution) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SalesmanUnifDistributionMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As SalesmanUnifDistribution)
            Try
                m_SalesmanUnifDistributionMapper.Delete(objDomain)
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

