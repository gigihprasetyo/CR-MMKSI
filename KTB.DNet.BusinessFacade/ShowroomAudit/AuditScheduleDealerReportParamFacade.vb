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
'// Generated on 7/16/2007 - 2:31:06 PM
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
Namespace KTB.DNet.BusinessFacade.ShowroomAudit
    Public Class AuditScheduleDealerReportParamFacade
        Inherits AbstractFacade


#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AuditScheduleDealerReportFormMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_AuditScheduleDealerReportFormMapper = MapperFactory.GetInstance.GetMapper(GetType(AuditScheduleDealerReportForm).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AuditScheduleDealerReportForm
            Return CType(m_AuditScheduleDealerReportFormMapper.Retrieve(ID), AuditScheduleDealerReportForm)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AuditScheduleDealerReportFormMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AuditScheduleDealerReportFormMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AuditScheduleDealerReportFormMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditScheduleDealerReportForm), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AuditScheduleDealerReportFormMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditScheduleDealerReportForm), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AuditScheduleDealerReportFormMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealerReportForm), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AuditScheduleDealerReportForm As ArrayList = m_AuditScheduleDealerReportFormMapper.RetrieveByCriteria(criterias)
            Return _AuditScheduleDealerReportForm
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealerReportForm), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AuditScheduleDealerReportColl As ArrayList = m_AuditScheduleDealerReportFormMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AuditScheduleDealerReportColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(AuditScheduleDealerReportForm), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim AuditScheduleDealerReportColl As ArrayList = m_AuditScheduleDealerReportFormMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AuditScheduleDealerReportColl


            'Dim AuditScheduleDealerReportColl As ArrayList = m_AuditScheduleDealerReportMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            'Return AuditScheduleDealerReportColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealerReportForm), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AuditScheduleDealerReportColl As ArrayList = m_AuditScheduleDealerReportFormMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AuditScheduleDealerReportForm), columnName, matchOperator, columnValue))
            Return AuditScheduleDealerReportColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditScheduleDealerReportForm), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealerReportForm), columnName, matchOperator, columnValue))

            Return m_AuditScheduleDealerReportFormMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"





#End Region
    End Class
End Namespace
