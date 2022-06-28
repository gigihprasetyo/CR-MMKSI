
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
'// Generated on 14/05/2019 - 16:06:35
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
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class V_BabitMasterRetailTargetFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_V_BabitMasterRetailTargetMapper As IMapper
        Private m_BabitBudgetHeaderMapper As IMapper


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_V_BabitMasterRetailTargetMapper = MapperFactory.GetInstance.GetMapper(GetType(V_BabitMasterRetailTarget).ToString)
            Me.m_BabitBudgetHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitBudgetHeader).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As V_BabitMasterRetailTarget
            Return CType(m_V_BabitMasterRetailTargetMapper.Retrieve(ID), V_BabitMasterRetailTarget)
        End Function

        Public Function Retrieve(ByVal Code As String) As V_BabitMasterRetailTarget
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(V_BabitMasterRetailTarget), "V_BabitMasterRetailTargetCode", MatchType.Exact, Code))

            Dim V_BabitMasterRetailTargetColl As ArrayList = m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias)
            If (V_BabitMasterRetailTargetColl.Count > 0) Then
                Return CType(V_BabitMasterRetailTargetColl(0), V_BabitMasterRetailTarget)
            End If
            Return New V_BabitMasterRetailTarget
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_V_BabitMasterRetailTargetMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_BabitMasterRetailTarget), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_BabitMasterRetailTargetMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_BabitMasterRetailTarget), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_BabitMasterRetailTargetMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _V_BabitMasterRetailTarget As ArrayList = m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias)
            Return _V_BabitMasterRetailTarget
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim V_BabitMasterRetailTargetColl As ArrayList = m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_BabitMasterRetailTargetColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(V_BabitMasterRetailTarget), SortColumn, sortDirection))
            Dim V_BabitMasterRetailTargetColl As ArrayList = m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return V_BabitMasterRetailTargetColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim V_BabitMasterRetailTargetColl As ArrayList = m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return V_BabitMasterRetailTargetColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim V_BabitMasterRetailTargetColl As ArrayList = m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(V_BabitMasterRetailTarget), columnName, matchOperator, columnValue))
            Return V_BabitMasterRetailTargetColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_BabitMasterRetailTarget), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_BabitMasterRetailTarget), columnName, matchOperator, columnValue))

            Return m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_BabitMasterRetailTarget), "V_BabitMasterRetailTargetCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(V_BabitMasterRetailTarget), "V_BabitMasterRetailTargetCode", AggregateType.Count)
            Return CType(m_V_BabitMasterRetailTargetMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As V_BabitMasterRetailTarget) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_V_BabitMasterRetailTargetMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As V_BabitMasterRetailTarget) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_V_BabitMasterRetailTargetMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As V_BabitMasterRetailTarget) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_V_BabitMasterRetailTargetMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As V_BabitMasterRetailTarget)
            Try
                m_V_BabitMasterRetailTargetMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveAllocationBudget(ByVal strAllocationBabit As String, ByVal strNoReg As String) As ArrayList
            Dim strSQL As String = String.Empty
            strSQL = "select distinct a.ID "
            strSQL += "from V_BabitMasterRetailTarget a "
            strSQL += "join BabitHeader bh on bh.DealerID = a.DealerID and bh.RowStatus = 0 "
            strSQL += "join Category ctg on a.CategoryID = ctg.ID and ctg.RowStatus = 0 "
            strSQL += "left join BabitDealerAllocation bda on bh.id = bda.BabitHeaderID and bda.RowStatus = 0 "
            strSQL += "and bda.BabitCategory = '" & strAllocationBabit & "' "
            strSQL += "outer apply("
            strSQL += "select categoryID, max(ID) as ID "
            strSQL += "from SubCategoryVehicle scv "
            strSQL += "where scv.RowStatus = 0 "
            strSQL += "and (case when a.SubCategoryVehicleID <> 0 then scv.ID else scv.CategoryID end) "
            strSQL += "= (case when a.SubCategoryVehicleID <> 0 then a.SubCategoryVehicleID else a.CategoryID end) "
            strSQL += "group by categoryID "
            strSQL += ") scv1 "
            strSQL += "join SubCategoryVehicle scv2 on scv1.ID = scv2.ID "
            strSQL += "where 1 = 1"
            strSQL += "and bh.BabitRegNumber = '" & strNoReg & "' "
            strSQL += "and month(bh.PeriodStart) IN (select value from funcListToTableInt(a.QuarterPeriodText,',')) "
            strSQL += "and year(bh.PeriodStart) = a.YearPeriod 	"
            'strSQL += "and (case when a.SubCategoryVehicleID = 0 then ctg.CategoryCode else scv2.Name end) = '" & strAllocationBabit & "'"

            Dim crits1 As New CriteriaComposite(New Criteria(GetType(V_BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits1.opAnd(New Criteria(GetType(V_BabitMasterRetailTarget), "ID", MatchType.InSet, "(" & strSQL & ")"))
            Dim arrView As ArrayList = m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(crits1)
            If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                Return arrView
            End If

            Return New ArrayList
        End Function

        Public Function RetrieveAllocationBudgetByDealer(ByVal dealerID As String, ByVal dtePeriodStart As DateTime) As ArrayList
            Dim strSQL As String = String.Empty
            strSQL = "select distinct a.ID "
            strSQL += "from V_BabitMasterRetailTarget a "
            strSQL += "where 1 = 1 "
            strSQL += "and a.DealerID = " & dealerID & " "
            strSQL += "and a.YearPeriod = year('" & dtePeriodStart.ToString("yyyy/MM/dd") & "') "
            strSQL += "and month('" & dtePeriodStart.ToString("yyyy/MM/dd") & "') IN ("
            strSQL += "Select value "
            strSQL += "from funcListToTableInt("
            strSQL += "case when a.QuarterPeriod = 1 then '04,05,06' "
            strSQL += "when a.QuarterPeriod = 2 then '07,08,09' "
            strSQL += "when a.QuarterPeriod = 3 then '10,11,12' "
            strSQL += "when a.QuarterPeriod = 4 then '01,02,03' "
            strSQL += "else '' end,',')"
            strSQL += ") "

            Dim crits1 As New CriteriaComposite(New Criteria(GetType(V_BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits1.opAnd(New Criteria(GetType(V_BabitMasterRetailTarget), "ID", MatchType.InSet, "(" & strSQL & ")"))
            Dim arrView As ArrayList = m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(crits1)
            If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                Return arrView
            End If

            Return New ArrayList
        End Function

        Public Function RetrieveAllocationBudgetByDealer_old(ByVal strAllocationBabit As String, ByVal dealerID As String, ByVal intCategoryID As Integer,
                                                         ByVal intSubCategoryVehicleID As Integer, ByVal dtePeriodStart As DateTime) As ArrayList
            Dim strSQL As String = String.Empty
            strSQL = "select distinct a.ID "
            strSQL += "from V_BabitMasterRetailTarget a "
            strSQL += "join BabitHeader bh on bh.DealerID = a.DealerID and bh.RowStatus = 0 "
            strSQL += "join Category ctg on a.CategoryID = ctg.ID and ctg.RowStatus = 0 "
            strSQL += "left join BabitDealerAllocation bda on bh.id = bda.BabitHeaderID and bda.RowStatus = 0 "
            strSQL += "and bda.BabitCategory = '" & strAllocationBabit & "' "
            strSQL += "outer apply("
            strSQL += "select categoryID, max(ID) as ID "
            strSQL += "from SubCategoryVehicle scv "
            strSQL += "where scv.RowStatus = 0 "
            strSQL += "and (case when a.SubCategoryVehicleID <> 0 then scv.ID else scv.CategoryID end) "
            strSQL += "= (case when a.SubCategoryVehicleID <> 0 then a.SubCategoryVehicleID else a.CategoryID end) "
            strSQL += "group by categoryID "
            strSQL += ") scv1 "
            strSQL += "join SubCategoryVehicle scv2 on scv1.ID = scv2.ID "
            strSQL += "where 1 = 1 "
            strSQL += "and a.SubCategoryVehicleID = " & intSubCategoryVehicleID & " "
            strSQL += "AND ctg.ID = " & intCategoryID & " "
            strSQL += "and bh.DealerID = " & dealerID & " "
            strSQL += "and month(bh.PeriodStart) IN (select value from funcListToTableInt(a.QuarterPeriodText,',')) "
            strSQL += "and year(bh.PeriodStart) = a.YearPeriod 	"
            strSQL += "AND a.YearPeriod = year('" & dtePeriodStart.ToString("yyyy/MM/dd") & "') "
            strSQL += "and month('" & dtePeriodStart.ToString("yyyy/MM/dd") & "') IN ( "       '--" & dtePeriodStart.ToString("yyyy/MM/dd") &') IN (
            strSQL += "Select value "
            strSQL += "from funcListToTableInt("
            strSQL += "case when a.QuarterPeriod = 1 then '04,05,06' "
            strSQL += "when a.QuarterPeriod = 2 then '07,08,09' "
            strSQL += "when a.QuarterPeriod = 3 then '10,11,12' "
            strSQL += "when a.QuarterPeriod = 4 then '01,02,03' "
            strSQL += "else '' end,',')"
            strSQL += ") "

            'strSQL += "and (case when a.SubCategoryVehicleID = 0 then ctg.CategoryCode else scv2.Name end) = '" & strAllocationBabit & "'"

            Dim crits1 As New CriteriaComposite(New Criteria(GetType(V_BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits1.opAnd(New Criteria(GetType(V_BabitMasterRetailTarget), "ID", MatchType.InSet, "(" & strSQL & ")"))
            Dim arrView As ArrayList = m_V_BabitMasterRetailTargetMapper.RetrieveByCriteria(crits1)
            If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                Return arrView
            End If

            Return New ArrayList
        End Function
#End Region

    End Class

End Namespace


