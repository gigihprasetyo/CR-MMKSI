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
'// Generated on 8/3/2005 - 3:58:00 PM
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
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Profile

#End Region


Namespace KTB.DNet.BusinessFacade.LKPP


    Public Class LKPPDetailFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_LKPPDetailMapper As IMapper


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_LKPPDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(LKPPDetail).ToString)



        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As LKPPDetail
            Return CType(m_LKPPDetailMapper.Retrieve(ID), LKPPDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As LKPPDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(LKPPDetail), "LKPPDetailCode", MatchType.Exact, Code))

            Dim LKPPDetailColl As ArrayList = m_LKPPDetailMapper.RetrieveByCriteria(criterias)
            If (LKPPDetailColl.Count > 0) Then
                Return CType(LKPPDetailColl(0), LKPPDetail)
            End If
            Return New LKPPDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_LKPPDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_LKPPDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_LKPPDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LKPPDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LKPPDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LKPPDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LKPPDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _LKPPDetail As ArrayList = m_LKPPDetailMapper.RetrieveByCriteria(criterias)
            Return _LKPPDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LKPPDetailColl As ArrayList = m_LKPPDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LKPPDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim LKPPDetailColl As ArrayList = m_LKPPDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return LKPPDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LKPPDetailColl As ArrayList = m_LKPPDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(LKPPDetail), columnName, matchOperator, columnValue))
            Return LKPPDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LKPPDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPDetail), columnName, matchOperator, columnValue))

            Return m_LKPPDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPDetail), "LKPPDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(LKPPDetail), "LKPPDetailCode", AggregateType.Count)
            Return CType(m_LKPPDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
        Public Function RetrieveByHeaderAndVtype(ByVal LKPPHeaderReferenceNumber As String, ByVal VehicleTypeID As Integer) As LKPPDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(LKPPDetail), "LKPPHeader.ReferenceNumber", MatchType.Exact, LKPPHeaderReferenceNumber))
            criterias.opAnd(New Criteria(GetType(LKPPDetail), "VechileType.ID", MatchType.Exact, VehicleTypeID))

            Dim LKPPDetailColl As ArrayList = m_LKPPDetailMapper.RetrieveByCriteria(criterias)
            If (LKPPDetailColl.Count > 0) Then
                Return CType(LKPPDetailColl(0), LKPPDetail)
            End If
            Return New LKPPDetail
        End Function
#End Region

    End Class

End Namespace

