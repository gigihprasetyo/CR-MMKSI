
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
'// Generated on 20/02/2019 - 16:40:58
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

Namespace KTB.DNET.BusinessFacade.MDP

    Public Class MDPMasterVehicleHistoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MDPMasterVehicleHistoryMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MDPMasterVehicleHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(MDPMasterVehicleHistory).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MDPMasterVehicleHistory
            Return CType(m_MDPMasterVehicleHistoryMapper.Retrieve(ID), MDPMasterVehicleHistory)
        End Function

        Public Function Retrieve(ByVal Code As String) As MDPMasterVehicleHistory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicleHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MDPMasterVehicleHistory), "MDPMasterVehicleHistoryCode", MatchType.Exact, Code))

            Dim MDPMasterVehicleHistoryColl As ArrayList = m_MDPMasterVehicleHistoryMapper.RetrieveByCriteria(criterias)
            If (MDPMasterVehicleHistoryColl.Count > 0) Then
                Return CType(MDPMasterVehicleHistoryColl(0), MDPMasterVehicleHistory)
            End If
            Return New MDPMasterVehicleHistory
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MDPMasterVehicleHistoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MDPMasterVehicleHistoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MDPMasterVehicleHistoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPMasterVehicleHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MDPMasterVehicleHistoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPMasterVehicleHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MDPMasterVehicleHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicleHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MDPMasterVehicleHistory As ArrayList = m_MDPMasterVehicleHistoryMapper.RetrieveByCriteria(criterias)
            Return _MDPMasterVehicleHistory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicleHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MDPMasterVehicleHistoryColl As ArrayList = m_MDPMasterVehicleHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MDPMasterVehicleHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(SortColumn)) And (Not IsNothing(SortColumn)) Then
                sortColl.Add(New Search.Sort(GetType(MDPMasterVehicleHistory), SortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim MDPMasterVehicleHistoryColl As ArrayList = m_MDPMasterVehicleHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MDPMasterVehicleHistoryColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MDPMasterVehicleHistoryColl As ArrayList = m_MDPMasterVehicleHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MDPMasterVehicleHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicleHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MDPMasterVehicleHistoryColl As ArrayList = m_MDPMasterVehicleHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MDPMasterVehicleHistory), columnName, matchOperator, columnValue))
            Return MDPMasterVehicleHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPMasterVehicleHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicleHistory), columnName, matchOperator, columnValue))

            Return m_MDPMasterVehicleHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicleHistory), "MDPMasterVehicleHistoryCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MDPMasterVehicleHistory), "MDPMasterVehicleHistoryCode", AggregateType.Count)
            Return CType(m_MDPMasterVehicleHistoryMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As MDPMasterVehicleHistory) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_MDPMasterVehicleHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As MDPMasterVehicleHistory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MDPMasterVehicleHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As MDPMasterVehicleHistory)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_MDPMasterVehicleHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MDPMasterVehicleHistory)
            Try
                m_MDPMasterVehicleHistoryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

