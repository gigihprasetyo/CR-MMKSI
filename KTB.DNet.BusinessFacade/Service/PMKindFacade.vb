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

Namespace KTB.DNet.BusinessFacade.Service
    Public Class PMKindFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_PMKindMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PMKindMapper = MapperFactory.GetInstance().GetMapper(GetType(PMKind).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PMKind
            Return CType(m_PMKindMapper.Retrieve(ID), PMKind)
        End Function

        Public Function Retrieve(ByVal strPMKindCode As String) As PMKind
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PMKind), "KindCode", MatchType.Exact, strPMKindCode))
            Dim PMKindColl As ArrayList = m_PMKindMapper.RetrieveByCriteria(criterias)
            If (PMKindColl.Count > 0) Then
                Return CType(PMKindColl(0), PMKind)
            Else
                Return New PMKind
            End If
        End Function

        Public Function IsPMKindFound(ByVal strPMKindCode As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            criterias.opAnd(New Criteria(GetType(PMKind), "KindCode", MatchType.Exact, strPMKindCode))
            Dim PMKindColl As ArrayList = m_PMKindMapper.RetrieveByCriteria(criterias)
            If (PMKindColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult

        End Function
        Public Function IsPMKindFound(ByVal standKM As Integer) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            criterias.opAnd(New Criteria(GetType(PMKind), "KM", MatchType.GreaterOrEqual, standKM))
            Dim PMKindColl As ArrayList = m_PMKindMapper.RetrieveByCriteria(criterias)
            If (PMKindColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult

        End Function

        Public Function IsPMKindFoundForMSP(ByVal standKM As Integer) As Boolean
            Dim spName As String = "up_RetrievePMKind_forMSP"
            Dim param As New ArrayList
            Dim sl As New SqlClient.SqlParameter
            sl.ParameterName = "@KM"

            sl.Value = standKM
            param.Add(sl)
            Dim PMKindColl As ArrayList = m_PMKindMapper.RetrieveSP(spName, param)
            If Not IsNothing(PMKindColl) AndAlso (PMKindColl.Count > 0) Then

                Return True

            End If
            Return False

        End Function

        Public Function PMKindForMSP(ByVal standKM As Integer) As PMKind
            Dim spName As String = "up_RetrievePMKind_forMSP"
            Dim param As New ArrayList
            Dim sl As New SqlClient.SqlParameter
            sl.ParameterName = "@KM"

            sl.Value = standKM
            param.Add(sl)
            Dim PMKindColl As ArrayList = m_PMKindMapper.RetrieveSP(spName, param)
            If Not IsNothing(PMKindColl) AndAlso (PMKindColl.Count > 0) Then

                Return CType(PMKindColl(0), PMKind)

            End If
            Return Nothing

        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PMKindMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PMKindMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PMKindMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PMKind), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PMKindMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PMKind), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PMKindMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PMKind), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PMKindMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PMKind As ArrayList = m_PMKindMapper.RetrieveByCriteria(criterias)
            Return _PMKind
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PMKindColl As ArrayList = m_PMKindMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PMKindColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PMKind), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PMKindMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PMKindColl As ArrayList = m_PMKindMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PMKindColl
        End Function
        Public Function RetrievePMKind(ByVal standKm As Integer) As PMKind
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(PMKind), "KM", MatchType.LesserOrEqual, standKm))
            criterias.opAnd(New Criteria(GetType(PMKind), "KM", MatchType.GreaterOrEqual, standKm))
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing("KM")) And (Not IsNothing("KM")) Then
                sortColl.Add(New Sort(GetType(PMKind), "KM", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim PMKindColl As ArrayList = m_PMKindMapper.RetrieveByCriteria(criterias, sortColl)
            If (PMKindColl.Count > 0) Then
                'Dim temp As New PMKind
                'Dim i As Integer = 0
                'For Each item As PMKind In PMKindColl
                '    If i = 0 Then
                '        temp = item
                '    End If
                '    If i <> 0 And item.KM > temp.KM Then
                '        temp = item
                '    End If
                '    i = i + 1
                'Next
                Return CType(PMKindColl(0), PMKind)
            Else
                Return New PMKind
            End If
        End Function
        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PMKind), columnName, matchOperator, columnValue))
            Dim PMKindColl As ArrayList = m_PMKindMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PMKindColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PMKind), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), columnName, matchOperator, columnValue))

            Return m_PMKindMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As PMKind) As Integer
            Dim iReturn As Integer = -2
            Try
                m_PMKindMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As PMKind) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PMKindMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As PMKind) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_PMKindMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As PMKind) As Integer
            Dim nResult As Integer = 1
            Try
                m_PMKindMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "KindCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PMKind), "KindCode", AggregateType.Count)

            Return CType(m_PMKindMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
