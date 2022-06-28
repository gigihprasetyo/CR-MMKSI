
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
'// Copyright  2015
'// ---------------------
'// $History      : $
'// Generated on 11/24/2015 - 11:15:19 AM
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
Imports KTB.DNet.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.DataMapper.Framework


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class StockActualFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_StockActualMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_StockActualMapper = MapperFactory.GetInstance.GetMapper(GetType(StockActual).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As StockActual
            Return CType(m_StockActualMapper.Retrieve(ID), StockActual)
        End Function

        Public Function Retrieve(ByVal Code As String) As StockActual
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StockActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StockActual), "StockActualCode", MatchType.Exact, Code))

            Dim StockActualColl As ArrayList = m_StockActualMapper.RetrieveByCriteria(criterias)
            If (StockActualColl.Count > 0) Then
                Return CType(StockActualColl(0), StockActual)
            End If
            Return New StockActual
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_StockActualMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_StockActualMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StockActual), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_StockActualMapper.RetrieveByCriteria(criterias, sortColl)
        End Function


        Public Function RetrieveList() As ArrayList
            Return m_StockActualMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StockActual), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_StockActualMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StockActual), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_StockActualMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StockActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _StockActual As ArrayList = m_StockActualMapper.RetrieveByCriteria(criterias)
            Return _StockActual
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StockActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim StockActualColl As ArrayList = m_StockActualMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return StockActualColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim StockActualColl As ArrayList = m_StockActualMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return StockActualColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StockActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim StockActualColl As ArrayList = m_StockActualMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(StockActual), columnName, matchOperator, columnValue))
            Return StockActualColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StockActual), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StockActual), columnName, matchOperator, columnValue))

            Return m_StockActualMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StockActual), "StockActualCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(StockActual), "StockActualCode", AggregateType.Count)
            Return CType(m_StockActualMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As StockActual) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_StockActualMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As StockActual) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_StockActualMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As StockActual)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_StockActualMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As StockActual)
            Try
                m_StockActualMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Sub ForcePopulateData(ByRef stockActual As StockActual)
            stockActual = CType(m_StockActualMapper, DataMapper.StockActualMapper).PopulateData(stockActual)
        End Sub
#End Region

    End Class

End Namespace

