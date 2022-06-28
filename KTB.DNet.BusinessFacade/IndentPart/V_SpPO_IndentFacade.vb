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
'// Generated on 8/2/2007 - 12:59:07 PM
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

#End Region

Namespace KTB.DNET.BusinessFacade.IndentPart


    Public Class V_SpPO_IndentFacade
        Inherits AbstractFacade


#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_V_SpPO_IndentMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_V_SpPO_IndentMapper = MapperFactory.GetInstance.GetMapper(GetType(V_SpPO_Indent).ToString)



        End Sub

#End Region

#Region "Retrieve"

        

        Public Function Retrieve(ByVal ID As Integer) As V_SpPO_Indent
            Return CType(m_V_SpPO_IndentMapper.Retrieve(ID), V_SpPO_Indent)
        End Function

        Public Function Retrieve(ByVal RequestNo As String) As V_SpPO_Indent
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SpPO_Indent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(V_SpPO_Indent), "RequestNo", MatchType.Exact, RequestNo))

            Dim V_SpPO_IndentColl As ArrayList = m_V_SpPO_IndentMapper.RetrieveByCriteria(criterias)
            If (V_SpPO_IndentColl.Count > 0) Then
                Return CType(V_SpPO_IndentColl(0), V_SpPO_Indent)
            End If
            Return New V_SpPO_Indent
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_V_SpPO_IndentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_V_SpPO_IndentMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_V_SpPO_IndentMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SpPO_Indent), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_SpPO_IndentMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SpPO_Indent), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_SpPO_IndentMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SpPO_Indent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _V_SpPO_Indent As ArrayList = m_V_SpPO_IndentMapper.RetrieveByCriteria(criterias)
            Return _V_SpPO_Indent
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SpPO_Indent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim V_SpPO_IndentColl As ArrayList = m_V_SpPO_IndentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_SpPO_IndentColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SpPO_Indent), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimReasonColl As ArrayList = m_V_SpPO_IndentMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ClaimReasonColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(V_SpPO_Indent), SortColumn, sortDirection))

            Dim V_SpPO_IndentColl As ArrayList = m_V_SpPO_IndentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return V_SpPO_IndentColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim V_SpPO_IndentColl As ArrayList = m_V_SpPO_IndentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return V_SpPO_IndentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SpPO_Indent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim V_SpPO_IndentColl As ArrayList = m_V_SpPO_IndentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(V_SpPO_Indent), columnName, matchOperator, columnValue))
            Return V_SpPO_IndentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SpPO_Indent), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SpPO_Indent), columnName, matchOperator, columnValue))

            Return m_V_SpPO_IndentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveActiveListByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(V_SpPO_Indent), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_SpPO_IndentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Custom Method"
        
#End Region

    End Class

End Namespace

