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
    Public Class RedemptionStatusFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_RedemptionStatusMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_RedemptionStatusMapper = MapperFactory.GetInstance().GetMapper(GetType(RedemptionStatus).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As RedemptionStatus
            Return CType(m_RedemptionStatusMapper.Retrieve(ID), RedemptionStatus)
        End Function

        Public Function Retrieve(ByVal RedemptionStatusCode As String) As RedemptionStatus
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RedemptionStatus), "RedemptionStatusCode", MatchType.Exact, RedemptionStatusCode))

            Dim RedemptionStatusColl As ArrayList = m_RedemptionStatusMapper.RetrieveByCriteria(criterias)
            If (RedemptionStatusColl.Count > 0) Then
                Return CType(RedemptionStatusColl(0), RedemptionStatus)
            End If
            Return New RedemptionStatus
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_RedemptionStatusMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RedemptionStatusMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_RedemptionStatusMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RedemptionStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RedemptionStatusMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RedemptionStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RedemptionStatusMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("RedemptionStatusCode")) Then
                sortColl.Add(New Sort(GetType(RedemptionStatus), "RedemptionStatusCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _RedemptionStatus As ArrayList = m_RedemptionStatusMapper.RetrieveByCriteria(criterias, sortColl)
            Return _RedemptionStatus
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RedemptionStatusColl As ArrayList = m_RedemptionStatusMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RedemptionStatusColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim RedemptionStatusColl As ArrayList = m_RedemptionStatusMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RedemptionStatusColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RedemptionStatus), columnName, matchOperator, columnValue))
            Dim RedemptionStatusColl As ArrayList = m_RedemptionStatusMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RedemptionStatusColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RedemptionStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionStatus), columnName, matchOperator, columnValue))

            Return m_RedemptionStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RedemptionStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RedemptionStatusMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As RedemptionStatus) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_RedemptionStatusMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As RedemptionStatus) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_RedemptionStatusMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As RedemptionStatus)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_RedemptionStatusMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As RedemptionStatus)
            Try
                m_RedemptionStatusMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionStatus), "RedemptionStatusCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(RedemptionStatus), "RedemptionStatusCode", AggregateType.Count)

            Return CType(m_RedemptionStatusMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RedemptionStatusMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RedemptionStatus), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim RedemptionStatusColl As ArrayList = m_RedemptionStatusMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return RedemptionStatusColl
        End Function

        Public Function IsStatusOpen(ByVal PeriodMonth As DateTime, ByVal CategoryCode As String, ByVal SubCategoryCode As String) As Boolean
            Dim cRS As New CriteriaComposite(New Criteria(GetType(RedemptionStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aRSs As ArrayList
            Dim dtStart As Date = DateSerial(PeriodMonth.Year, PeriodMonth.Month, 1)
            Dim dtEnd As Date = DateSerial(PeriodMonth.Year, PeriodMonth.Month, 1).AddMonths(1)
            Dim oRS As RedemptionStatus
            Dim IsOpen As Boolean = True

            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "PeriodDate", MatchType.GreaterOrEqual, dtStart))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "PeriodDate", MatchType.Lesser, dtEnd))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "CategoryCode", MatchType.Exact, CategoryCode))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "SubCategoryCode", MatchType.Exact, SubCategoryCode))

            aRSs = m_RedemptionStatusMapper.RetrieveByCriteria(cRS)

            If aRSs.Count > 0 Then
                oRS = CType(aRSs(0), RedemptionStatus)
                If oRS.Status = 2 Then IsOpen = False
                '0:fREE;1:STILL IN DISTRIBUTING PROCESS;2:SAVED/CLOSED
                'IsOpen = (oRS.Status = 0)
            End If

            Return IsOpen
        End Function

        Public Function IsInProcess(ByVal PeriodMonth As DateTime, ByVal CategoryCode As String, ByVal SubCategoryCode As String) As Boolean
            Dim cRS As New CriteriaComposite(New Criteria(GetType(RedemptionStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aRSs As ArrayList
            Dim dtStart As Date = DateSerial(PeriodMonth.Year, PeriodMonth.Month, 1)
            Dim dtEnd As Date = DateSerial(PeriodMonth.Year, PeriodMonth.Month, 1).AddMonths(1)
            Dim oRS As RedemptionStatus
            Dim IsProcessing As Boolean = False

            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "PeriodDate", MatchType.GreaterOrEqual, dtStart))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "PeriodDate", MatchType.Lesser, dtEnd))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "CategoryCode", MatchType.Exact, CategoryCode))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "SubCategoryCode", MatchType.Exact, SubCategoryCode))

            aRSs = m_RedemptionStatusMapper.RetrieveByCriteria(cRS)

            If aRSs.Count > 0 Then
                oRS = CType(aRSs(0), RedemptionStatus)
                If oRS.Status = 1 Then IsProcessing = True
                '0:fREE;1:STILL IN DISTRIBUTING PROCESS;2:SAVED/CLOSED

            End If

            Return IsProcessing
        End Function

        Public Function GetCurrentStatus(ByVal PeriodMonth As DateTime, ByVal CategoryCode As String, ByVal SubCategoryCode As String) As Integer
            Dim cRS As New CriteriaComposite(New Criteria(GetType(RedemptionStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aRSs As ArrayList
            Dim dtStart As Date = DateSerial(PeriodMonth.Year, PeriodMonth.Month, 1)
            Dim dtEnd As Date = DateSerial(PeriodMonth.Year, PeriodMonth.Month, 1).AddMonths(1)
            Dim oRS As New RedemptionStatus
            Dim IsProcessing As Boolean = False
            Dim IsAllowed As Boolean = True

            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "PeriodDate", MatchType.GreaterOrEqual, dtStart))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "PeriodDate", MatchType.Lesser, dtEnd))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "CategoryCode", MatchType.Exact, CategoryCode))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "SubCategoryCode", MatchType.Exact, SubCategoryCode))

            aRSs = m_RedemptionStatusMapper.RetrieveByCriteria(cRS)
            If aRSs.Count > 0 Then oRS = aRSs(0)
            Return oRS.Status
        End Function

        Public Function IsAllowToUpdateStatus(ByVal PeriodMonth As DateTime, ByVal CategoryCode As String, ByVal SubCategoryCode As String, ByVal ProcessStatus As Integer, ByRef oRS As RedemptionStatus) As Boolean
            Dim cRS As New CriteriaComposite(New Criteria(GetType(RedemptionStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aRSs As ArrayList
            Dim dtStart As Date = DateSerial(PeriodMonth.Year, PeriodMonth.Month, 1)
            Dim dtEnd As Date = DateSerial(PeriodMonth.Year, PeriodMonth.Month, 1).AddMonths(1)
            'Dim oRS As New RedemptionStatus
            Dim IsProcessing As Boolean = False
            Dim IsAllowed As Boolean = True

            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "PeriodDate", MatchType.GreaterOrEqual, dtStart))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "PeriodDate", MatchType.Lesser, dtEnd))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "CategoryCode", MatchType.Exact, CategoryCode))
            cRS.opAnd(New Criteria(GetType(RedemptionStatus), "SubCategoryCode", MatchType.Exact, SubCategoryCode))

            aRSs = m_RedemptionStatusMapper.RetrieveByCriteria(cRS)

            If aRSs.Count > 0 Then
                oRS = aRSs(0)
                If ProcessStatus = 0 AndAlso (oRS.Status = 1 OrElse oRS.Status = 3) Then
                    IsAllowed = False
                ElseIf ProcessStatus = 1 AndAlso (oRS.Status = 3) Then
                    IsAllowed = False
                ElseIf ProcessStatus = 2 AndAlso (oRS.Status = 3) Then
                    IsAllowed = False
                ElseIf ProcessStatus = 3 AndAlso (oRS.Status = 1) Then
                    IsAllowed = False
                End If
            Else
                oRS = New RedemptionStatus
            End If
            Return IsAllowed
        End Function

        Public Function SetInProcessStatus(ByVal PeriodMonth As DateTime, ByVal CategoryCode As String, ByVal SubCategoryCode As String, ByVal ProcessStatus As Integer) As Boolean
            Dim oRS As RedemptionStatus
            Dim iReturn As Integer = -2

            If Me.IsAllowToUpdateStatus(PeriodMonth, CategoryCode, SubCategoryCode, ProcessStatus, oRS) = True Then
                If Not IsNothing(oRS) AndAlso oRS.ID > 0 Then
                    oRS.Status = ProcessStatus
                    iReturn = m_RedemptionStatusMapper.Update(oRS, m_userPrincipal.Identity.Name)
                Else
                    oRS = New RedemptionStatus
                    oRS.PeriodDate = DateSerial(PeriodMonth.Year, PeriodMonth.Month, 1)
                    oRS.CategoryCode = CategoryCode
                    oRS.SubCategoryCode = SubCategoryCode
                    oRS.Status = ProcessStatus
                    iReturn = m_RedemptionStatusMapper.Insert(oRS, m_userPrincipal.Identity.Name)
                End If
            Else
                iReturn = -1
            End If
            Return (iReturn > 0)
        End Function


#End Region

    End Class

End Namespace
