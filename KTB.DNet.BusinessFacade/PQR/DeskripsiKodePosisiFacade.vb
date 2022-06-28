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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 7/26/2007 - 3:28:51 PM
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
Imports KTB.DNet.DataMapper
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade

    Public Class DeskripsiKodePosisiFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DeskripsiKodePosisiMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DeskripsiKodePosisiMapper = MapperFactory.GetInstance.GetMapper(GetType(DeskripsiKodePosisi).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DeskripsiKodePosisi
            Return CType(m_DeskripsiKodePosisiMapper.Retrieve(ID), DeskripsiKodePosisi)
        End Function

        Public Function Retrieve(ByVal Code As String) As DeskripsiKodePosisi
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "DeskripsiKodePosisiCode", MatchType.Exact, Code))

            Dim DeskripsiKodePosisiColl As ArrayList = m_DeskripsiKodePosisiMapper.RetrieveByCriteria(criterias)
            If (DeskripsiKodePosisiColl.Count > 0) Then
                Return CType(DeskripsiKodePosisiColl(0), DeskripsiKodePosisi)
            End If
            Return New DeskripsiKodePosisi
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DeskripsiKodePosisiMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DeskripsiKodePosisiMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DeskripsiKodePosisiMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeskripsiKodePosisi), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DeskripsiKodePosisiMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeskripsiKodePosisi), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DeskripsiKodePosisiMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DeskripsiKodePosisi As ArrayList = m_DeskripsiKodePosisiMapper.RetrieveByCriteria(criterias)
            Return _DeskripsiKodePosisi
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DeskripsiKodePosisiColl As ArrayList = m_DeskripsiKodePosisiMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DeskripsiKodePosisiColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DeskripsiKodePosisiColl As ArrayList = m_DeskripsiKodePosisiMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DeskripsiKodePosisiColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DeskripsiKodePosisiColl As ArrayList = m_DeskripsiKodePosisiMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), columnName, matchOperator, columnValue))
            Return DeskripsiKodePosisiColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeskripsiKodePosisi), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), columnName, matchOperator, columnValue))

            Return m_DeskripsiKodePosisiMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "DeskripsiKodePosisiCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DeskripsiKodePosisi), "DeskripsiKodePosisiCode", AggregateType.Count)
            Return CType(m_DeskripsiKodePosisiMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

