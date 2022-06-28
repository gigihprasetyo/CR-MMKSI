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

Namespace KTB.DNet.BusinessFacade.Tools
    Public Class BCPQueryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_BCPQueryMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_BCPQueryMapper = MapperFactory.GetInstance().GetMapper(GetType(BCPQuery).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BCPQuery
            Return CType(m_BCPQueryMapper.Retrieve(ID), BCPQuery)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BCPQueryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BCPQueryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BCPQuery), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Return m_BCPQueryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BCPQuery), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BCPQueryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal crit As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BCPQuery), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BCPQueryMapper.RetrieveByCriteria(crit, sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BCPQuery), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BCPQueryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BCPQuery), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _coll As ArrayList = m_BCPQueryMapper.RetrieveByCriteria(criterias)
            Return _coll
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BCPQuery), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BCPQueryColl As ArrayList = m_BCPQueryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BCPQueryColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BCPQuery), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim BCPQueryColl As ArrayList = m_BCPQueryMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BCPQueryColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BCPQuery), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_BCPQueryMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BCPQuery), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BCPQuery), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BCPQueryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BCPQueryColl As ArrayList = m_BCPQueryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BCPQueryColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, _
            ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(BCPQuery), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim BCPQueryColl As ArrayList = m_BCPQueryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BCPQueryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BCPQuery), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BCPQuery), columnName, matchOperator, columnValue))
            Dim BCPQueryColl As ArrayList = m_BCPQueryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BCPQueryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BCPQuery), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BCPQuery), columnName, matchOperator, columnValue))

            Return m_BCPQueryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As BCPQuery) As Integer
            Dim iReturn As Integer = -2
            Try
                m_BCPQueryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BCPQuery) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BCPQueryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BCPQuery)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BCPQueryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As BCPQuery) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_BCPQueryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveFromSP(ByVal SQLBase As String, ByVal SQLWhere As String, ByVal SQLOrder As String, ByVal fileName As String) As ArrayList
            Dim SQL As String = ""

            SQL = "exec SP_BCP '" & SQLBase & "', '" & SQLWhere & "', '" & SQLOrder & "', '" & fileName & "'"
            Return m_BCPQueryMapper.RetrieveSP(SQL)

        End Function

        Public Function RetrieveFromSP(ByVal SQLBase As String) As String
            Dim SQL As String = ""
            Dim result As String
            Dim arr As DataSet

            SQL = "exec " + SQLBase
            arr = m_BCPQueryMapper.RetrieveDataSet(SQL)
            result = arr.Tables(0).Rows(0).Item(0).ToString
            Return result

        End Function

        Public Function RetrieveFromSP_V3(ByVal SQLBase As String, ByVal SQLWhere As String, ByVal SQLOrder As String, ByVal fileName As String) As ArrayList
            Dim SQL As String = ""

            SQL = "exec SP_BCP_V3 '" & SQLBase & "', '" & SQLWhere & "', '" & SQLOrder & "', '" & fileName & "'"
            Return m_BCPQueryMapper.RetrieveSP(SQL)

        End Function

        Public Function RetrieveFromSP(ByVal SQLBase As String, ByVal SQLWhere As String, ByVal fileName As String) As ArrayList
            Dim SQL As String = ""

            SQL = "exec SP_BCP_V2 '" & SQLBase & "', '" & SQLWhere & "', '" & "" & "', '" & fileName & "'"
            Return m_BCPQueryMapper.RetrieveSP(SQL)

        End Function
#End Region

    End Class

End Namespace
