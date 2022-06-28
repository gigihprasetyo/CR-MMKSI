#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 3/16/2019 - 10:53:00 AM
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

Namespace KTB.DNet.BusinessFacade.PK
    Public Class sp_GetPKAllocationFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_sp_GetPKAllocationMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_sp_GetPKAllocationMapper = MapperFactory.GetInstance().GetMapper(GetType(sp_GetPKAllocation).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As sp_GetPKAllocation
            Return CType(m_sp_GetPKAllocationMapper.Retrieve(ID), sp_GetPKAllocation)
        End Function

        Public Function Retrieve(ByVal DealerCode As String, ByVal RequestPeriodeMonth As Integer, ByVal RequestPeriodeYear As Integer) As sp_GetPKAllocation
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetPKAllocation), "DealerCode", MatchType.Exact, DealerCode))
            criterias.opAnd(New Criteria(GetType(sp_GetPKAllocation), "CreditAccount", MatchType.Exact, RequestPeriodeMonth))
            criterias.opAnd(New Criteria(GetType(sp_GetPKAllocation), "PaymentType", MatchType.Exact, RequestPeriodeYear))

            Dim sp_GetPKAllocationColl As ArrayList = m_sp_GetPKAllocationMapper.RetrieveByCriteria(criterias)
            If (sp_GetPKAllocationColl.Count > 0) Then
                Return CType(sp_GetPKAllocationColl(0), sp_GetPKAllocation)
            End If
            Return New sp_GetPKAllocation
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_sp_GetPKAllocationMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_GetPKAllocationMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_sp_GetPKAllocationMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_GetPKAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_GetPKAllocationMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_GetPKAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_GetPKAllocationMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetPKAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("sp_GetPKAllocationCode")) Then
                sortColl.Add(New Sort(GetType(sp_GetPKAllocation), "sp_GetPKAllocationCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _sp_GetPKAllocation As ArrayList = m_sp_GetPKAllocationMapper.RetrieveByCriteria(criterias, sortColl)
            Return _sp_GetPKAllocation
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetPKAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sp_GetPKAllocationColl As ArrayList = m_sp_GetPKAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_GetPKAllocationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim sp_GetPKAllocationColl As ArrayList = m_sp_GetPKAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_GetPKAllocationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetPKAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(sp_GetPKAllocation), columnName, matchOperator, columnValue))
            Dim sp_GetPKAllocationColl As ArrayList = m_sp_GetPKAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return sp_GetPKAllocationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_GetPKAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetPKAllocation), columnName, matchOperator, columnValue))

            Return m_sp_GetPKAllocationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetPKAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_GetPKAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_sp_GetPKAllocationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As sp_GetPKAllocation) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_sp_GetPKAllocationMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As sp_GetPKAllocation) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_sp_GetPKAllocationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As sp_GetPKAllocation)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_sp_GetPKAllocationMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As sp_GetPKAllocation)
            Try
                m_sp_GetPKAllocationMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_GetPKAllocation), "sp_GetPKAllocationCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(sp_GetPKAllocation), "sp_GetPKAllocationCode", AggregateType.Count)

            Return CType(m_sp_GetPKAllocationMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function RetrieveFromSP(ByVal DealerCode As String, ByVal RequestPeriodeMonth As Integer, ByVal RequestPeriodeYear As Integer, ByVal SubCategoryVehicleID As Integer, ByVal FreeDays As String) As ArrayList
            Dim SQL As String
            Dim SQLOpt As String = ""
            Dim SQLWhere As String
            Dim SQLOrder As String

            SQL = "exec sp_GetPKAllocation " & "'" & DealerCode.ToString() & "'" & ", " & RequestPeriodeMonth & ", " & RequestPeriodeYear & ", " & SubCategoryVehicleID & ", " & "'" & FreeDays & "'"

            Return m_sp_GetPKAllocationMapper.RetrieveSP(SQL)
        End Function

        Public Function UpdateFreeDays(ByVal VehicleColorID As String, ByVal RequestPeriodeMonth As Integer, ByVal RequestPeriodeYear As Integer, ByVal DealerID As Integer, ByVal VehicleModelID As Integer)
            Dim SQL As String

            SQL = "exec sp_SetFreedays " & VehicleColorID & ", " & RequestPeriodeMonth & ", " & RequestPeriodeYear & ", " & DealerID

            m_sp_GetPKAllocationMapper.ExecuteSP(SQL)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_sp_GetPKAllocationMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(sp_GetPKAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim sp_GetPKAllocationColl As ArrayList = m_sp_GetPKAllocationMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return sp_GetPKAllocationColl
        End Function

#End Region


    End Class
End Namespace

