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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 9/26/2005 - 1:43:31 PM
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

Imports KTB.Dnet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.Dnet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic

#End Region

Namespace KTB.Dnet.BusinessFacade.PK

    Public Class PKProductionPlanFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PKProductionPlanMapper As IMapper
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PKProductionPlanMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.Dnet.Domain.PKProductionPlan).ToString)
            Me.DomainTypeCollection.Add(GetType(KTB.Dnet.Domain.PKProductionPlan))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PKProductionPlan
            Return CType(m_PKProductionPlanMapper.Retrieve(ID), PKProductionPlan)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PKProductionPlanMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PKProductionPlanMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PKProductionPlan), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_PKProductionPlanMapper.RetrieveByCriteria(criterias, sortColl)
        End Function


        Public Function Retrieve(ByVal Code As String) As PKProductionPlan
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PKProductionPlan), "VechileColor.ID", MatchType.Exact, CType(Code, Integer)))

            Dim PKProductionPlanColl As ArrayList = m_PKProductionPlanMapper.RetrieveByCriteria(criterias)
            If (PKProductionPlanColl.Count > 0) Then
                Return CType(PKProductionPlanColl(0), PKProductionPlan)
            End If
            Return New PKProductionPlan
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PKProductionPlanMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PKProductionPlan), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PKProductionPlanMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PKProductionPlan), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PKProductionPlanMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PKProductionPlan As ArrayList = m_PKProductionPlanMapper.RetrieveByCriteria(criterias)
            Return _PKProductionPlan
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PKHeaderColl As ArrayList = m_PKProductionPlanMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PKHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PKHeaderColl As ArrayList = m_PKProductionPlanMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PKHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PKProductionPlanColl As ArrayList = m_PKProductionPlanMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PKProductionPlan), columnName, matchOperator, columnValue))
            Return PKProductionPlanColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PKProductionPlan), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), columnName, matchOperator, columnValue))

            Return m_PKProductionPlanMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub Insert(ByVal objDomain As PKProductionPlan)
            Dim iReturn As Integer = -2
            Try
                m_PKProductionPlanMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try

        End Sub

        Public Sub Update(ByVal objDomain As PKProductionPlan)
            Try
                m_PKProductionPlanMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
#End Region

#Region "Custom Method"
        Public Function DoRetrieveDataSet(ByVal intVehicleColorID As Integer, ByVal intPeriodMonth As Integer, ByVal intPeriodYear As Integer, ByVal intProductionYear As Integer) As DataSet
            Dim strSql As String = String.Empty
            strSql = "exec sp_GetTotalDO " & intVehicleColorID & ", " & intPeriodMonth & ", " & intPeriodYear & ", " & intProductionYear
            Return m_PKProductionPlanMapper.RetrieveDataSet(strSql)
        End Function

        Public Function RetrieveRencanaProduksi(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection,
                                                ByVal PeriodMonth As Integer, ByVal PeriodYear As Integer, ByVal CategoryID As Integer,
                                                ByVal VechileModelID As String, ByVal VechileTypeID As Integer) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(PKProductionPlan), sortColumn, sortDirection))
            Else
                sortColl.Add(New Sort(GetType(PKProductionPlan), "ID", Sort.SortDirection.DESC))
            End If

            Dim spName As String = "sp_RetrieveRencanaProduksi"
            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@Sort ", sortColl.ToString()))
            Param.Add(New SqlClient.SqlParameter("@PeriodMonth", PeriodMonth))
            Param.Add(New SqlClient.SqlParameter("@PeriodYear", PeriodYear))
            Param.Add(New SqlClient.SqlParameter("@CategoryID", CategoryID))
            Param.Add(New SqlClient.SqlParameter("@VechileModelID", VechileModelID))
            Param.Add(New SqlClient.SqlParameter("@VechileTypeID", VechileTypeID))

            Dim arrRencanaProduksi As ArrayList = New ArrayList

            Dim dt As DataTable = m_PKProductionPlanMapper.RetrieveDataSet(spName, New ArrayList(Param)).Tables(0)
            For Each row As DataRow In dt.Rows
                Dim oPKProductionPlan As PKProductionPlan = New PKProductionPlan
                oPKProductionPlan.id = row("id").ToString
                oPKProductionPlan.PeriodMonth = row("PeriodMonth").ToString
                oPKProductionPlan.PeriodYear = row("PeriodYear").ToString
                oPKProductionPlan.ProductionYear = row("ProductionYear").ToString
                oPKProductionPlan.CarryOverPreviousQty = row("CarryOverPreviousQty").ToString
                oPKProductionPlan.PlanQty = row("PlanQty").ToString
                oPKProductionPlan.UnselledStock = row("UnselledStock").ToString
                oPKProductionPlan.AllocationQty = row("AllocationQty").ToString
                oPKProductionPlan.ReserveQty = row("ReserveQty").ToString
                oPKProductionPlan.RowStatus = row("RowStatus").ToString
                oPKProductionPlan.CreatedBy = row("CreatedBy").ToString
                oPKProductionPlan.CreatedTime = row("CreatedTime").ToString
                oPKProductionPlan.LasUpdateBy = row("LasUpdateBy").ToString
                oPKProductionPlan.LastUpdateTime = row("LastUpdateTime").ToString
                oPKProductionPlan.VechileColor = New VechileColor(CType(row("VehicleColorID").ToString, Integer))
                oPKProductionPlan.TotalBaruAndValidasi = row("TotalBaruAndValidasi").ToString
                oPKProductionPlan.TotalKonfirmasiAndTungguDiskon = row("TotalKonfirmasiAndTungguDiskon").ToString
                oPKProductionPlan.TotalReleaseAndAgree = row("TotalReleaseAndAgree").ToString
                oPKProductionPlan.TotalTolak = row("TotalTolak").ToString
                oPKProductionPlan.TotalSelesai = row("TotalSelesai").ToString
                oPKProductionPlan.TotalDO = row("TotalDO").ToString
                arrRencanaProduksi.Add(oPKProductionPlan)
            Next

            Return arrRencanaProduksi
        End Function

        Public Function RetrieveRencanaProduksi_DisplayDealerOrder(ByVal PeriodMonth As Integer, ByVal PeriodYear As Integer,
                                                ByVal MaterialNumber As String, ByVal ProductionYear As Integer) As ArrayList

            Dim spName As String = "sp_RetrieveRencanaProduksi_DisplayDealerOrder"
            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@PeriodMonth", PeriodMonth))
            Param.Add(New SqlClient.SqlParameter("@PeriodYear", PeriodYear))
            Param.Add(New SqlClient.SqlParameter("@MaterialNumber", MaterialNumber))
            Param.Add(New SqlClient.SqlParameter("@ProductionYear", ProductionYear))

            Dim arrRencanaProduksi As ArrayList = New ArrayList

            Dim dt As DataTable = m_PKProductionPlanMapper.RetrieveDataSet(spName, New ArrayList(Param)).Tables(0)
            For Each row As DataRow In dt.Rows
                Dim oPKProductionPlan As PKProductionPlan = New PKProductionPlan
                oPKProductionPlan.id = row("id").ToString
                oPKProductionPlan.PeriodMonth = row("PeriodMonth").ToString
                oPKProductionPlan.PeriodYear = row("PeriodYear").ToString
                oPKProductionPlan.ProductionYear = row("ProductionYear").ToString
                oPKProductionPlan.CarryOverPreviousQty = row("CarryOverPreviousQty").ToString
                oPKProductionPlan.PlanQty = row("PlanQty").ToString
                oPKProductionPlan.UnselledStock = row("UnselledStock").ToString
                oPKProductionPlan.AllocationQty = row("AllocationQty").ToString
                oPKProductionPlan.ReserveQty = row("ReserveQty").ToString
                oPKProductionPlan.RowStatus = row("RowStatus").ToString
                oPKProductionPlan.CreatedBy = row("CreatedBy").ToString
                oPKProductionPlan.CreatedTime = row("CreatedTime").ToString
                oPKProductionPlan.LasUpdateBy = row("LasUpdateBy").ToString
                oPKProductionPlan.LastUpdateTime = row("LastUpdateTime").ToString
                oPKProductionPlan.VechileColor = New VechileColor(CType(row("VehicleColorID").ToString, Integer))
                oPKProductionPlan.TotalBaruAndValidasi = row("TotalBaruAndValidasi").ToString
                oPKProductionPlan.TotalKonfirmasiAndTungguDiskon = row("TotalKonfirmasiAndTungguDiskon").ToString
                oPKProductionPlan.TotalReleaseAndAgree = row("TotalReleaseAndAgree").ToString
                oPKProductionPlan.TotalTolak = row("TotalTolak").ToString
                oPKProductionPlan.TotalSelesai = row("TotalSelesai").ToString
                arrRencanaProduksi.Add(oPKProductionPlan)
            Next

            Return arrRencanaProduksi
        End Function

#End Region

    End Class

End Namespace