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
'// Copyright  2018
'// ---------------------
'// $History      : $
'// Generated on 4/23/2018 - 8:46:40 AM
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

    Public Class WSCParameterVehicleFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_WSCParameterVehicleMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_WSCParameterVehicleMapper = MapperFactory.GetInstance.GetMapper(GetType(WSCParameterVehicle).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As WSCParameterVehicle
            Return CType(m_WSCParameterVehicleMapper.Retrieve(ID), WSCParameterVehicle)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_WSCParameterVehicleMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_WSCParameterVehicleMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_WSCParameterVehicleMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(WSCParameterVehicle), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_WSCParameterVehicleMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(WSCParameterVehicle), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_WSCParameterVehicleMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _WSCParameterVehicle As ArrayList = m_WSCParameterVehicleMapper.RetrieveByCriteria(criterias)
            Return _WSCParameterVehicle
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim WSCParameterVehicleColl As ArrayList = m_WSCParameterVehicleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return WSCParameterVehicleColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim WSCParameterVehicleColl As ArrayList = m_WSCParameterVehicleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return WSCParameterVehicleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim WSCParameterVehicleColl As ArrayList = m_WSCParameterVehicleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(WSCParameterVehicle), columnName, matchOperator, columnValue))
            Return WSCParameterVehicleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(WSCParameterVehicle), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), columnName, matchOperator, columnValue))

            Return m_WSCParameterVehicleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As WSCParameterVehicle) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_WSCParameterVehicleMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As WSCParameterVehicle) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_WSCParameterVehicleMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As WSCParameterVehicle)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_WSCParameterVehicleMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As WSCParameterVehicle)
            Try
                m_WSCParameterVehicleMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ExistVehicleType(ByVal HeaderID As String, ByVal VechileID As String, ByVal inclRowStatus As Boolean) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ID", MatchType.Exact, HeaderID))
            crit.opAnd(New Criteria(GetType(WSCParameterVehicle), "VechileType.ID", MatchType.Exact, VechileID))
            If inclRowStatus Then
                crit.opAnd(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            End If
            Dim agg As Aggregate = New Aggregate(GetType(WSCParameterVehicle), "ID", AggregateType.Count)
            Return CType(m_WSCParameterVehicleMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

