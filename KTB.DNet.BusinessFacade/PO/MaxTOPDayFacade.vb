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

Namespace KTB.DNet.BusinessFacade.PO
    Public Class MaxTOPDayFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_MaxTOPDayMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_MaxTOPDayMapper = MapperFactory.GetInstance().GetMapper(GetType(MaxTOPDay).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MaxTOPDay
            Return CType(m_MaxTOPDayMapper.Retrieve(ID), MaxTOPDay)
        End Function

        Public Function Retrieve(ByVal MaxTOPDayCode As String) As MaxTOPDay
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MaxTOPDay), "MaxTOPDayCode", MatchType.Exact, MaxTOPDayCode))

            Dim MaxTOPDayColl As ArrayList = m_MaxTOPDayMapper.RetrieveByCriteria(criterias)
            If (MaxTOPDayColl.Count > 0) Then
                Return CType(MaxTOPDayColl(0), MaxTOPDay)
            End If
            Return New MaxTOPDay
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MaxTOPDayMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MaxTOPDayMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MaxTOPDayMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaxTOPDay), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaxTOPDayMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaxTOPDay), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaxTOPDayMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("MaxTOPDayCode")) Then
                sortColl.Add(New Sort(GetType(MaxTOPDay), "MaxTOPDayCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _MaxTOPDay As ArrayList = m_MaxTOPDayMapper.RetrieveByCriteria(criterias, sortColl)
            Return _MaxTOPDay
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MaxTOPDayColl As ArrayList = m_MaxTOPDayMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MaxTOPDayColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MaxTOPDayColl As ArrayList = m_MaxTOPDayMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MaxTOPDayColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MaxTOPDay), columnName, matchOperator, columnValue))
            Dim MaxTOPDayColl As ArrayList = m_MaxTOPDayMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MaxTOPDayColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaxTOPDay), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaxTOPDay), columnName, matchOperator, columnValue))

            Return m_MaxTOPDayMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaxTOPDay), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaxTOPDayMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As MaxTOPDay) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_MaxTOPDayMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As MaxTOPDay) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MaxTOPDayMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As MaxTOPDay)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_MaxTOPDayMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MaxTOPDay)
            Try
                m_MaxTOPDayMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaxTOPDay), "MaxTOPDayCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MaxTOPDay), "MaxTOPDayCode", AggregateType.Count)

            Return CType(m_MaxTOPDayMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MaxTOPDayMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaxTOPDay), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim MaxTOPDayColl As ArrayList = m_MaxTOPDayMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MaxTOPDayColl
        End Function

        Public Function GetMinTOPDays(ByVal DealerID As Integer, ByVal VechileTypeID As Integer, ByVal IsFactoring As Boolean) As Integer
            Dim cMTD As New CriteriaComposite(New Criteria(GetType(MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active.Active, Short)))
            Dim aMTDs As ArrayList
            Dim MaxTOP As Integer = 0

            cMTD.opAnd(New Criteria(GetType(MaxTOPDay), "DealerID", MatchType.Exact, DealerID))
            cMTD.opAnd(New Criteria(GetType(MaxTOPDay), "VechileTypeID", MatchType.Exact, VechileTypeID))
            aMTDs = m_MaxTOPDayMapper.RetrieveByCriteria(cMTD)
            If aMTDs.Count > 0 Then
                MaxTOP = IIf(IsFactoring, CType(aMTDs(0), MaxTOPDay).Factoring, CType(aMTDs(0), MaxTOPDay).Normal)
            End If
            Return MaxTOP
        End Function

#End Region

    End Class

End Namespace
