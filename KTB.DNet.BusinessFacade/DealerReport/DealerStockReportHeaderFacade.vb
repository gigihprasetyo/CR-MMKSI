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
'// Copyright  2006
'// ---------------------
'// $History      : $
'// Generated on 1/6/2006 - 4:11:44 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.IO

#End Region

#Region "Custom Namespace"
Imports KTb.DNet.Domain
Imports KTb.DNet.Domain.Search
Imports KTb.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.DealerReport

    Public Class DealerStockReportHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DealerStockReportHeaderMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DealerStockReportHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.DealerStockReportHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As KTB.DNet.Domain.DealerStockReportHeader
            Return CType(m_DealerStockReportHeaderMapper.Retrieve(ID), KTB.DNet.Domain.DealerStockReportHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.DealerStockReportHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "DealerStockReportHeaderCode", MatchType.Exact, Code))

            Dim DealerStockReportHeaderColl As ArrayList = m_DealerStockReportHeaderMapper.RetrieveByCriteria(criterias)
            If (DealerStockReportHeaderColl.Count > 0) Then
                Return CType(DealerStockReportHeaderColl(0), KTB.DNet.Domain.DealerStockReportHeader)
            End If
            Return New KTB.DNet.Domain.DealerStockReportHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DealerStockReportHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DealerStockReportHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DealerStockReportHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.DealerStockReportHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerStockReportHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.DealerStockReportHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerStockReportHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DealerStockReportHeader As ArrayList = m_DealerStockReportHeaderMapper.RetrieveByCriteria(criterias)
            Return _DealerStockReportHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerStockReportHeaderColl As ArrayList = m_DealerStockReportHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DealerStockReportHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DealerStockReportHeaderColl As ArrayList = m_DealerStockReportHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DealerStockReportHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
           ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(DealerStockReportHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim DealerStockReportHeaderColl As ArrayList = m_DealerStockReportHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerStockReportHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerStockReportHeaderColl As ArrayList = m_DealerStockReportHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), columnName, matchOperator, columnValue))
            Return DealerStockReportHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.DealerStockReportHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), columnName, matchOperator, columnValue))

            Return m_DealerStockReportHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDealerStockReportHeader As KTB.DNet.Domain.DealerStockReportHeader) As Integer
            Dim nInsertedRow As Integer = -1
            Try
                nInsertedRow = m_DealerStockReportHeaderMapper.Insert(objDealerStockReportHeader, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Return ex.Message
            End Try
            Return nInsertedRow
        End Function

        Public Function Update(ByVal objDealerStockReportHeader As KTB.DNet.Domain.DealerStockReportHeader) As Integer
            Dim nUpdatedRow As Integer = -1
            Try
                nUpdatedRow = m_DealerStockReportHeaderMapper.Update(objDealerStockReportHeader, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Return ex.Message
            End Try
            Return nUpdatedRow
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

