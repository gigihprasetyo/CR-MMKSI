
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 08/10/2019 - 17:01:11
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


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class BabitBudgetHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitBudgetHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitBudgetHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitBudgetHeader).ToString)

 
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitBudgetHeader
            Return CType(m_BabitBudgetHeaderMapper.Retrieve(ID), BabitBudgetHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitBudgetHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitBudgetHeader), "BabitBudgetHeaderCode", MatchType.Exact, Code))

            Dim BabitBudgetHeaderColl As ArrayList = m_BabitBudgetHeaderMapper.RetrieveByCriteria(criterias)
            If (BabitBudgetHeaderColl.Count > 0) Then
                Return CType(BabitBudgetHeaderColl(0), BabitBudgetHeader)
            End If
            Return New BabitBudgetHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitBudgetHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitBudgetHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitBudgetHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitBudgetHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitBudgetHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitBudgetHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitBudgetHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitBudgetHeader As ArrayList = m_BabitBudgetHeaderMapper.RetrieveByCriteria(criterias)
            Return _BabitBudgetHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitBudgetHeaderColl As ArrayList = m_BabitBudgetHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitBudgetHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitBudgetHeader), SortColumn, sortDirection))
            Dim BabitBudgetHeaderColl As ArrayList = m_BabitBudgetHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitBudgetHeaderColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitBudgetHeaderColl As ArrayList = m_BabitBudgetHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitBudgetHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitBudgetHeaderColl As ArrayList = m_BabitBudgetHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitBudgetHeader), columnName, matchOperator, columnValue))
            Return BabitBudgetHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitBudgetHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), columnName, matchOperator, columnValue))

            Return m_BabitBudgetHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), "BabitBudgetHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitBudgetHeader), "BabitBudgetHeaderCode", AggregateType.Count)
            Return CType(m_BabitBudgetHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitBudgetHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitBudgetHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitBudgetHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitBudgetHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitBudgetHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitBudgetHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitBudgetHeader)
            Try
                m_BabitBudgetHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function GetDataAllocation(ByVal regNumber As String, ByVal dtePeriodStart As DateTime) As ArrayList
            Dim strSQL As String = String.Empty
            strSQL += "select distinct "
            strSQL += "a.ID "
            strSQL += "from BabitBudgetHeader a "
            strSQL += "join BabitHeader b on a.DealerID = b.DealerID and year(b.PeriodStart) = a.YearPeriod  "
            strSQL += "and month(b.PeriodStart) IN (select value from funcListToTableInt( "
            strSQL += "case when a.QuarterPeriod = 1 then "
            strSQL += "'04,05,06' "
            strSQL += "when a.QuarterPeriod = 2 then "
            strSQL += "'07,08,09' "
            strSQL += "when a.QuarterPeriod = 3 then "
            strSQL += "'10,11,12' "
            strSQL += "when a.QuarterPeriod = 4 then "
            strSQL += "'01,02,03' "
            strSQL += "else "
            strSQL += "'' "
            strSQL += "end ,',')) and b.RowStatus = 0 "
            strSQL += "join Category c on a.CategoryID = c.ID and c.RowStatus = 0 "
            strSQL += "left join SubCategoryVehicle d on a.SubCategoryVehicleID = d.ID and d.RowStatus = 0 "
            strSQL += "where a.RowStatus = 0 "
            strSQL += "and b.BabitRegNumber = '" & regNumber & "'"
            strSQL += "union "
            strSQL += "select distinct a.ID "
            strSQL += "from BabitBudgetHeader a "
            strSQL += "where a.RowStatus = 0 "
            strSQL += "and a.DealerID Is null "
            strSQL += "and a.CategoryID is null "
            strSQL += "and a.YearPeriod = year('" & dtePeriodStart.ToString("yyyy/MM/dd") & "')  "
            strSQL += "and month('" & dtePeriodStart.ToString("yyyy/MM/dd") & "') IN ("
            strSQL += "Select value "
            strSQL += "from funcListToTableInt("
            strSQL += "case when a.QuarterPeriod = 1 then '04,05,06' "
            strSQL += "when a.QuarterPeriod = 2 then '07,08,09' "
            strSQL += "when a.QuarterPeriod = 3 then '10,11,12' "
            strSQL += "when a.QuarterPeriod = 4 then '01,02,03' "
            strSQL += "else '' end,','))  "

            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitBudgetHeader), "ID", MatchType.InSet, "(" & strSQL & ")"))
            Dim arlBabitBudgetHeader As ArrayList = m_BabitBudgetHeaderMapper.RetrieveByCriteria(criterias)
            If (Not IsNothing(arlBabitBudgetHeader) AndAlso arlBabitBudgetHeader.Count > 0) Then
                Return arlBabitBudgetHeader
            End If

            Return New ArrayList
        End Function

        Public Function GetDataAllocationByDealer(ByVal _dealerID As String, ByVal dtePeriodStart As DateTime) As ArrayList
            Dim strSQL As String = String.Empty
            strSQL += "select distinct "
            strSQL += "a.ID "
            strSQL += "from BabitBudgetHeader a "            
            strSQL += "join Category c on a.CategoryID = c.ID and c.RowStatus = 0 "
            strSQL += "left join SubCategoryVehicle d on a.SubCategoryVehicleID = d.ID and d.RowStatus = 0 "
            strSQL += "where a.RowStatus = 0 "
            strSQL += "and a.DealerID = '" & _dealerID & "'"
            strSQL += "and a.YearPeriod = year('" & dtePeriodStart.ToString("yyyy/MM/dd") & "')  "
            strSQL += "and month('" & dtePeriodStart.ToString("yyyy/MM/dd") & "') IN (select value from funcListToTableInt( "
            strSQL += "case when a.QuarterPeriod = 1 then "
            strSQL += "'04,05,06' "
            strSQL += "when a.QuarterPeriod = 2 then "
            strSQL += "'07,08,09' "
            strSQL += "when a.QuarterPeriod = 3 then "
            strSQL += "'10,11,12' "
            strSQL += "when a.QuarterPeriod = 4 then "
            strSQL += "'01,02,03' "
            strSQL += "else "
            strSQL += "'' "
            strSQL += "end ,',')) "
            strSQL += "union "
            strSQL += "select distinct a.ID "
            strSQL += "from BabitBudgetHeader a "
            strSQL += "where a.RowStatus = 0 "
            strSQL += "and a.DealerID Is null "
            strSQL += "and a.CategoryID is null "
            strSQL += "and a.YearPeriod = year('" & dtePeriodStart.ToString("yyyy/MM/dd") & "')  "
            strSQL += "and month('" & dtePeriodStart.ToString("yyyy/MM/dd") & "') IN ("
            strSQL += "Select value "
            strSQL += "from funcListToTableInt("
            strSQL += "case when a.QuarterPeriod = 1 then '04,05,06' "
            strSQL += "when a.QuarterPeriod = 2 then '07,08,09' "
            strSQL += "when a.QuarterPeriod = 3 then '10,11,12' "
            strSQL += "when a.QuarterPeriod = 4 then '01,02,03' "
            strSQL += "else '' end,','))  "

            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitBudgetHeader), "ID", MatchType.InSet, "(" & strSQL & ")"))
            Dim arlBabitBudgetHeader As ArrayList = m_BabitBudgetHeaderMapper.RetrieveByCriteria(criterias)
            If (Not IsNothing(arlBabitBudgetHeader) AndAlso arlBabitBudgetHeader.Count > 0) Then
                Return arlBabitBudgetHeader
            End If

            Return New ArrayList
        End Function

#End Region

    End Class

End Namespace

