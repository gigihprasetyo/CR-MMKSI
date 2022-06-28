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
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.General

    Public Class NationalHolidayFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_NationalHolidayMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_NationalHolidayMapper = MapperFactory.GetInstance().GetMapper(GetType(NationalHoliday).ToString)
        End Sub

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As NationalHoliday) As Integer
            Dim iReturn As Integer = -2
            Try
                m_NationalHolidayMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As NationalHoliday) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_NationalHolidayMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As NationalHoliday)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_NationalHolidayMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As NationalHoliday)
            Try
                m_NationalHolidayMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "NationalHolidayCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(NationalHoliday), "NationalHolidayCode", AggregateType.Count)

            Return CType(m_NationalHolidayMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As NationalHoliday
            Return CType(m_NationalHolidayMapper.Retrieve(ID), NationalHoliday)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_NationalHolidayMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_NationalHolidayMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_NationalHolidayMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(NationalHoliday), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_NationalHolidayMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(NationalHoliday), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_NationalHolidayMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _NationalHoliday As ArrayList = m_NationalHolidayMapper.RetrieveByCriteria(criterias)
            Return _NationalHoliday
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim NationalHolidayColl As ArrayList = m_NationalHolidayMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return NationalHolidayColl
        End Function


        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(NationalHoliday), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim NationalHolidayColl As ArrayList = m_NationalHolidayMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return NationalHolidayColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
             ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(NationalHoliday), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_NationalHolidayMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim NationalHolidayColl As ArrayList = m_NationalHolidayMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return NationalHolidayColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim NationalHolidayColl As ArrayList = m_NationalHolidayMapper.RetrieveByCriteria(criterias)
            Return NationalHolidayColl
        End Function


        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), columnName, matchOperator, columnValue))
            Dim NationalHolidayColl As ArrayList = m_NationalHolidayMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return NationalHolidayColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(NationalHoliday), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), columnName, matchOperator, columnValue))

            Return m_NationalHolidayMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Custom Method"

        Public Function IsActiveDateExist(ByVal year As Integer, ByVal month As Integer, ByVal day As Integer) As Integer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDate", MatchType.Exact, day))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayMonth", MatchType.Exact, month))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayYear", MatchType.Exact, year))
            Dim agg As Aggregate = New Aggregate(GetType(NationalHoliday), "HolidayDateTime", AggregateType.Count)
            Return CType(m_NationalHolidayMapper.RetrieveScalar(agg, criterias), Integer)
        End Function

        Public Function RetrieveNextDay(ByVal currentDate As Date) As Date
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDate", MatchType.Exact, currentDate.Day))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayMonth", MatchType.Exact, currentDate.Month))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayYear", MatchType.Exact, currentDate.Year))
            Dim NationalColl As ArrayList = Me.RetrieveByCriteria(criterias)
            Dim count = NationalColl.Count
            Dim nextDay As Date = currentDate
            While count > 0
                Dim _natDay As NationalHoliday = CType(NationalColl(0), NationalHoliday)
                nextDay = New Date(_natDay.HolidayYear, _natDay.HolidayMonth, _natDay.HolidayDate)
                nextDay = nextDay.AddDays(1)
                nextDay = RetrieveNextDay(nextDay)
                count = 0
            End While
            Return nextDay
        End Function

        Public Function RetrieveDayBefore(ByVal currentDate As Date) As Date
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDate", MatchType.Exact, currentDate.AddDays(-1).Day))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayMonth", MatchType.Exact, currentDate.Month))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayYear", MatchType.Exact, currentDate.Year))
            Dim NationalColl As ArrayList = Me.RetrieveByCriteria(criterias)
            Dim count = NationalColl.Count
            Dim DayBefore As Date = currentDate.AddDays(-1)
            While count > 0
                Dim _nationalHoliday As NationalHoliday = CType(NationalColl(0), NationalHoliday)
                DayBefore = RetrieveDayBefore(_nationalHoliday.HolidayDateTime)
                count = 0
            End While
            Return DayBefore
        End Function

#End Region

    End Class

End Namespace