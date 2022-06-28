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
'// Author Name   : Agus Soepriadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright � 2005 
'// ---------------------
'// $History      : $
'// Generated on 10/10/2005 - 10:53:00 AM
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
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.service
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class V_SPK_Salesman_DownloadFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_V_SPK_Salesman_DownloadMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_SPK_Salesman_DownloadMapper = MapperFactory.GetInstance().GetMapper(GetType(V_SPK_Salesman_Download).ToString)
            Me.DomainTypeCollection.Add(GetType(V_SPK_Salesman_Download))
        End Sub

#End Region

#Region "Retrieve"


        Public Function Retrieve(ByVal ID As Integer) As V_SPK_Salesman_Download
            Return CType(m_V_SPK_Salesman_DownloadMapper.Retrieve(ID), V_SPK_Salesman_Download)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_V_SPK_Salesman_DownloadMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_V_SPK_Salesman_DownloadMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SPK_Salesman_Download), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_SPK_Salesman_DownloadMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_V_SPK_Salesman_DownloadMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SPK_Salesman_Download), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_SPK_Salesman_DownloadMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SPK_Salesman_Download), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_SPK_Salesman_DownloadMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SPK_Salesman_Download), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim V_SPK_Salesman_DownloadList As ArrayList = m_V_SPK_Salesman_DownloadMapper.RetrieveByCriteria(criterias)
            Return V_SPK_Salesman_DownloadList
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SPK_Salesman_Download), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim V_SPK_Salesman_DownloadList As ArrayList = m_V_SPK_Salesman_DownloadMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_SPK_Salesman_DownloadList
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SPK_Salesman_Download), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_SPK_Salesman_DownloadMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim V_SPK_Salesman_DownloadList As ArrayList = m_V_SPK_Salesman_DownloadMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_SPK_Salesman_DownloadList
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim V_SPK_Salesman_DownloadList As ArrayList = m_V_SPK_Salesman_DownloadMapper.RetrieveByCriteria(criterias)

            Return V_SPK_Salesman_DownloadList
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SPK_Salesman_Download), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(V_SPK_Salesman_Download), columnName, matchOperator, columnValue))
            Dim V_SPK_Salesman_DownloadList As ArrayList = m_V_SPK_Salesman_DownloadMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_SPK_Salesman_DownloadList
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_SPK_Salesman_Download), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SPK_Salesman_Download), columnName, matchOperator, columnValue))

            Return m_V_SPK_Salesman_DownloadMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
