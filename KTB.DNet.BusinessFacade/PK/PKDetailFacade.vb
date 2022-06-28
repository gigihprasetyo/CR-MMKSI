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
'// Generated on 9/26/2005 - 1:07:25 PM
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
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Linq

#End Region

Namespace KTB.Dnet.BusinessFacade.PK

    Public Class PKDetailFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PKDetailMapper As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PKDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PKDetail).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PKDetail
            Return CType(m_PKDetailMapper.Retrieve(ID), PKDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As PKDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PKDetail), "PKDetailCode", MatchType.Exact, Code))

            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias)
            If (PKDetailColl.Count > 0) Then
                Return CType(PKDetailColl(0), PKDetail)
            End If
            Return New PKDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PKDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PKDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PKDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_PKDetailMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PKDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PKDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PKDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PKDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PKDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PKDetail As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias)
            Return _PKDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PKDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PKDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PKDetailColl
        End Function
        Public Function RetrieveByCriteria(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PKDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(Criterias, sortColl)
            Return PKDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PKDetailColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("ResponseQty")) And (Not IsNothing("ResponseQty")) Then
                sortColl.Add(New Sort(GetType(PKDetail), "ResponseQty", Sort.SortDirection.DESC))
            Else
                sortColl = Nothing
            End If
            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias, sortColl)
            Return PKDetailColl

        End Function
        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PKDetail), columnName, matchOperator, columnValue))
            Return PKDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PKDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), columnName, matchOperator, columnValue))

            Return m_PKDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_PKDetailMapper.RetrieveScalar(agg, criterias)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As PKDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_PKDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Sub Update(ByVal objDomain As PKDetail)
            Try
                m_PKDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Update(ByVal ArrList As ArrayList)
            Try
                For Each objDomain As PKDetail In ArrList
                    m_PKDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                Next
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Delete(ByVal objDomain As PKDetail)
            Try
                m_PKDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "PKDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PKDetail), "PKDetailCode", AggregateType.Count)
            Return CType(m_PKDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
        Public Function UpdateResponseQtyAsAllocationQty(Data_VechileColorID As String, OrderType As Integer, ProductionYear As Integer, RequestPeriodeMonth As Integer, RequestPeriodeYear As Integer) As Boolean
            Dim dtbs As New DataSet
            Dim strSPName As String = "up_UpdateVechileType_ResponseQty  "
            Dim Param As New List(Of SqlClient.SqlParameter)
            Dim result As Integer = 0

            Param.Add(New SqlClient.SqlParameter("@Data_VechileColorID ", Data_VechileColorID))
            Param.Add(New SqlClient.SqlParameter("@OrderType", OrderType))
            Param.Add(New SqlClient.SqlParameter("@ProductionYear ", ProductionYear))
            Param.Add(New SqlClient.SqlParameter("@RequestPeriodeMonth", RequestPeriodeMonth))
            Param.Add(New SqlClient.SqlParameter("@RequestPeriodeYear", RequestPeriodeYear))

            dtbs = m_PKDetailMapper.RetrieveDataSet(strSPName, New ArrayList(Param))

            If dtbs.Tables.Count > 0 Then
                If dtbs.Tables(0).Rows.Count > 0 Then
                    result = CType(dtbs.Tables(0).Rows(0)("RESULT"), Integer)
                    Return result = 1
                Else
                    Return False
                End If

            Else
                Return False
            End If

        End Function
#End Region

    End Class

End Namespace