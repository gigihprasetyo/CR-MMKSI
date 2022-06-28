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
    Public Class LaborMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_LaborMasterMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_LaborMasterMapper = MapperFactory.GetInstance().GetMapper(GetType(LaborMaster).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As LaborMaster
            Return CType(m_LaborMasterMapper.Retrieve(ID), LaborMaster)
        End Function

        Public Function Retrieve(ByVal Code As String) As LaborMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, Code))

            Dim LaborMasterColl As ArrayList = m_LaborMasterMapper.RetrieveByCriteria(criterias)
            If (LaborMasterColl.Count > 0) Then
                Return CType(LaborMasterColl(0), LaborMaster)
            End If
            Return New LaborMaster
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_LaborMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_LaborMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_LaborMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LaborMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LaborMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LaborMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LaborMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _LaborMaster As ArrayList = m_LaborMasterMapper.RetrieveByCriteria(criterias)
            Return _LaborMaster
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LaborMasterColl As ArrayList = m_LaborMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LaborMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim LaborMasterColl As ArrayList = m_LaborMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LaborMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(LaborMaster), columnName, matchOperator, columnValue))
            Dim LaborMasterColl As ArrayList = m_LaborMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LaborMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LaborMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), columnName, matchOperator, columnValue))

            Return m_LaborMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveWithOneCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LaborMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), columnName, matchOperator, columnValue))

            Return m_LaborMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortTable As Type, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(LaborMaster), "VechileType.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(sortTable, sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LaborMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal crit As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortTable As Type, ByVal sortDirection As Sort.SortDirection) As ArrayList

            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(LaborMaster), "VechileType.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(sortTable, sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LaborMasterMapper.RetrieveByCriteria(crit, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LaborMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LaborMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_LaborMasterMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As LaborMaster) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_LaborMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As LaborMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_LaborMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
            End Try
            Return nResult
        End Function




        Public Sub Delete(ByVal objDomain As LaborMaster)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_LaborMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As LaborMaster) As Integer
            Dim count As Integer
            Try
                count = m_LaborMasterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                count = 0
            End Try
            Return count
        End Function

        Public Function ValidateCode(ByVal sLaborCode As String, ByVal sWorkCode As String, ByVal sVehicleTypeID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, sLaborCode))
            crit.opAnd(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, sWorkCode))
            crit.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", MatchType.Exact, CType(sVehicleTypeID, Integer)))

            Dim agg As Aggregate = New Aggregate(GetType(LaborMaster), "ID", AggregateType.Count)

            Dim count As Integer = -1
            Try
                count = CType(m_LaborMasterMapper.RetrieveScalar(agg, crit), Integer)
            Catch ex As Exception
                count = -1
            End Try
            Return count
        End Function

#End Region

#Region "Custom Method"
        Public Function IsExist(ByVal crit As CriteriaComposite) As Boolean
            Dim aggr As Aggregate = New Aggregate(GetType(LaborMaster), "ID", AggregateType.Count)
            Return (m_LaborMasterMapper.RetrieveScalar(aggr, crit) > 0)
        End Function

        Public Function RetrieveByPrimaryKey(ByVal sLaborCode As String, ByVal sWorkCode As String, ByVal sVehicleTypeID As String) As LaborMaster
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, sLaborCode))
            crit.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, sWorkCode))
            crit.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", MatchType.Exact, CType(sVehicleTypeID, Integer)))
            Dim lb As LaborMaster
            Try
                Dim list As ArrayList = m_LaborMasterMapper.RetrieveByCriteria(crit)
                If list.Count > 0 Then
                    lb = CType(list(0), LaborMaster)
                Else
                    lb = New LaborMaster
                End If
            Catch ex As Exception
                lb = New LaborMaster
            End Try
            Return lb
        End Function

#End Region

    End Class

End Namespace


