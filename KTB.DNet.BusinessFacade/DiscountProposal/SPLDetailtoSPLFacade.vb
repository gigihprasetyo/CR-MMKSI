
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
'// Generated on 23/06/2020 - 10:24:28
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
Imports System.Collections.Generic


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class SPLDetailtoSPLFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SPLDetailtoSPLMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SPLDetailtoSPLMapper = MapperFactory.GetInstance.GetMapper(GetType(SPLDetailtoSPL).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SPLDetailtoSPL
            Return CType(m_SPLDetailtoSPLMapper.Retrieve(ID), SPLDetailtoSPL)
        End Function

        Public Function Retrieve(ByVal Code As String) As SPLDetailtoSPL
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPLDetailtoSPL), "SPLDetailtoSPLCode", MatchType.Exact, Code))

            Dim SPLDetailtoSPLColl As ArrayList = m_SPLDetailtoSPLMapper.RetrieveByCriteria(criterias)
            If (SPLDetailtoSPLColl.Count > 0) Then
                Return CType(SPLDetailtoSPLColl(0), SPLDetailtoSPL)
            End If
            Return New SPLDetailtoSPL
        End Function

        Public Function RetrieveBySPLDetail(ByVal SPLDetailID As Integer) As List(Of SPLDetailtoSPL)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPLDetailtoSPL), "SPLDetail.ID", MatchType.Exact, SPLDetailID))

            Dim SPLDetailtoSPLColl As ArrayList = m_SPLDetailtoSPLMapper.RetrieveByCriteria(criterias)
            Dim result As New List(Of SPLDetailtoSPL)

            For Each drWLog As SPLDetailtoSPL In SPLDetailtoSPLColl
                result.Add(drWLog)
            Next

            Return result
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPLDetailtoSPLMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPLDetailtoSPLMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SPLDetailtoSPLMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPLDetailtoSPL), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPLDetailtoSPLMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPLDetailtoSPL), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPLDetailtoSPLMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SPLDetailtoSPL As ArrayList = m_SPLDetailtoSPLMapper.RetrieveByCriteria(criterias)
            Return _SPLDetailtoSPL
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPLDetailtoSPL), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SPLDetailtoSPLColl As ArrayList = m_SPLDetailtoSPLMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPLDetailtoSPLColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPLDetailtoSPLColl As ArrayList = m_SPLDetailtoSPLMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPLDetailtoSPLColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SPLDetailtoSPL), SortColumn, sortDirection))
            Dim SPLDetailtoSPLColl As ArrayList = m_SPLDetailtoSPLMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPLDetailtoSPLColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SPLDetailtoSPLColl As ArrayList = m_SPLDetailtoSPLMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SPLDetailtoSPLColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPLDetailtoSPLColl As ArrayList = m_SPLDetailtoSPLMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SPLDetailtoSPL), columnName, matchOperator, columnValue))
            Return SPLDetailtoSPLColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPLDetailtoSPL), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetailtoSPL), columnName, matchOperator, columnValue))

            Return m_SPLDetailtoSPLMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetailtoSPL), "SPLDetailtoSPLCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SPLDetailtoSPL), "SPLDetailtoSPLCode", AggregateType.Count)
            Return CType(m_SPLDetailtoSPLMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SPLDetailtoSPL) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SPLDetailtoSPLMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SPLDetailtoSPL) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPLDetailtoSPLMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As SPLDetailtoSPL) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_SPLDetailtoSPLMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As SPLDetailtoSPL)
            Try
                m_SPLDetailtoSPLMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveFromSP(ByVal strSPLID As Integer) As ArrayList
            Dim Sql As String = "exec sp_DP_GetSPLDetailtoSPLWithTotal " & strSPLID
            Return m_SPLDetailtoSPLMapper.RetrieveSP(Sql)
        End Function

#End Region

    End Class

End Namespace

