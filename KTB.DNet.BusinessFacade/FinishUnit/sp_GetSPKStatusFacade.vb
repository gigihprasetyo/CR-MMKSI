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

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class sp_GetSPKStatusFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_sp_GetSPKStatusMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_sp_GetSPKStatusMapper = MapperFactory.GetInstance().GetMapper(GetType(sp_GetSPKStatus).ToString)
        End Sub

#End Region

#Region "Retrieve"
        Public Function RetrieveFromSP(ByVal dealerID As Integer) As ArrayList
            Dim SQL As String
            Dim aSAPs As ArrayList
            Dim oSAP As sp_GetSPKStatus

            SQL = "exec sp_GetSPKStatus " & dealerID & ""
            aSAPs = m_sp_GetSPKStatusMapper.RetrieveSP(SQL)
            Return aSAPs
        End Function

        Public Function Retrieve(ByVal ID As Integer) As sp_GetSPKStatus
            Return CType(m_sp_GetSPKStatusMapper.Retrieve(ID), sp_GetSPKStatus)
        End Function

        Public Function Retrieve(ByVal status As String) As sp_GetSPKStatus
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetSPKStatus), "Status", MatchType.Exact, status))

            Dim sp_GetSPKStatusColl As ArrayList = m_sp_GetSPKStatusMapper.RetrieveByCriteria(criterias)
            If (sp_GetSPKStatusColl.Count > 0) Then
                Return CType(sp_GetSPKStatusColl(0), sp_GetSPKStatus)
            End If
            Return New sp_GetSPKStatus
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_sp_GetSPKStatusMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_GetSPKStatusMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_sp_GetSPKStatusMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_GetSPKStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_GetSPKStatusMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_GetSPKStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_GetSPKStatusMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        'Public Function RetrieveActiveList() As ArrayList
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetSPKStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Dim sortColl As SortCollection = New SortCollection
        '    If (Not IsNothing("sp_GetSPKStatusCode")) Then
        '        sortColl.Add(New Sort(GetType(sp_GetSPKStatus), "sp_GetSPKStatusCode", Sort.SortDirection.ASC))
        '    Else
        '        sortColl = Nothing
        '    End If
        '    Dim _sp_GetSPKStatus As ArrayList = m_sp_GetSPKStatusMapper.RetrieveByCriteria(criterias, sortColl)
        '    Return _sp_GetSPKStatus
        'End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetSPKStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sp_GetSPKStatusColl As ArrayList = m_sp_GetSPKStatusMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_GetSPKStatusColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim sp_GetSPKStatusColl As ArrayList = m_sp_GetSPKStatusMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_GetSPKStatusColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetSPKStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_GetSPKStatus), columnName, matchOperator, columnValue))
            Dim sp_GetSPKStatusColl As ArrayList = m_sp_GetSPKStatusMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_GetSPKStatusColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_GetSPKStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetSPKStatus), columnName, matchOperator, columnValue))

            Return m_sp_GetSPKStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        'Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        'ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetSPKStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '    Dim sortColl As SortCollection = New SortCollection

        '    If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
        '        sortColl.Add(New Sort(GetType(sp_GetSPKStatus), sortColumn, sortDirection))
        '    Else
        '        sortColl = Nothing
        '    End If

        '    Return m_sp_GetSPKStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        'End Function

#End Region

#Region "Transaction/Other Public Method"

        'Public Function Insert(ByVal objDomain As sp_GetSPKStatus) As Integer
        '    Dim iReturn As Integer = -2
        '    Try
        '        iReturn = m_sp_GetSPKStatusMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        Dim s As String = ex.Message
        '        iReturn = -1
        '    End Try
        '    Return iReturn

        'End Function

        'Public Function Update(ByVal objDomain As sp_GetSPKStatus) As Integer
        '    Dim nResult As Integer = -1
        '    Try
        '        nResult = m_sp_GetSPKStatusMapper.Update(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        nResult = -1
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '        If rethrow Then
        '            Throw
        '        End If
        '    End Try
        '    Return nResult
        'End Function

        'Public Sub Delete(ByVal objDomain As sp_GetSPKStatus)
        '    Dim nResult As Integer = -1
        '    Try
        '        nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
        '        m_sp_GetSPKStatusMapper.Delete(objDomain)
        '    Catch ex As Exception
        '        nResult = -1
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '        If rethrow Then
        '            Throw
        '        End If

        '    End Try
        'End Sub

        'Public Sub DeleteFromDB(ByVal objDomain As sp_GetSPKStatus)
        '    Try
        '        m_sp_GetSPKStatusMapper.Delete(objDomain)
        '    Catch ex As Exception
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '        If rethrow Then
        '            Throw
        '        End If
        '    End Try
        'End Sub

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetSPKStatus), "sp_GetSPKStatusCode", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(sp_GetSPKStatus), "sp_GetSPKStatusCode", AggregateType.Count)

        '    Return CType(m_sp_GetSPKStatusMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_GetSPKStatusMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_GetSPKStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim sp_GetSPKStatusColl As ArrayList = m_sp_GetSPKStatusMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return sp_GetSPKStatusColl
        End Function

#End Region

    End Class

End Namespace
