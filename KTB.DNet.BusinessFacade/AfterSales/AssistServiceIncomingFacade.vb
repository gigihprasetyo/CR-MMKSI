
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
'// Generated on 1/17/2018 - 9:28:32 AM
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic


#End Region

Namespace KTB.DNET.BusinessFacade.AfterSales

    Public Class AssistServiceIncomingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AssistServiceIncomingMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_AssistServiceIncomingMapper = MapperFactory.GetInstance.GetMapper(GetType(AssistServiceIncoming).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AssistServiceIncoming
            Return CType(m_AssistServiceIncomingMapper.Retrieve(ID), AssistServiceIncoming)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AssistServiceIncomingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AssistServiceIncomingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AssistServiceIncomingMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistServiceIncoming), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AssistServiceIncomingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistServiceIncoming), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AssistServiceIncomingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistServiceIncoming), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AssistServiceIncoming As ArrayList = m_AssistServiceIncomingMapper.RetrieveByCriteria(criterias)
            Return _AssistServiceIncoming
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistServiceIncoming), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AssistServiceIncomingColl As ArrayList = m_AssistServiceIncomingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AssistServiceIncomingColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AssistServiceIncomingColl As ArrayList = m_AssistServiceIncomingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return AssistServiceIncomingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistServiceIncoming), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AssistServiceIncomingColl As ArrayList = m_AssistServiceIncomingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AssistServiceIncoming), columnName, matchOperator, columnValue))
            Return AssistServiceIncomingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistServiceIncoming), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistServiceIncoming), columnName, matchOperator, columnValue))

            Return m_AssistServiceIncomingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As AssistServiceIncoming) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_AssistServiceIncomingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As AssistServiceIncoming) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AssistServiceIncomingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As AssistServiceIncoming)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_AssistServiceIncomingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As AssistServiceIncoming)
            Try
                m_AssistServiceIncomingMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistServiceIncoming), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim svcincomingColl As ArrayList = m_AssistServiceIncomingMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return svcincomingColl
        End Function

        Public Function RetrieveCustomPagingBySP(ByVal criterias As ICriteria, ByVal pageNumber As Integer,
                                                 ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String,
                                                 ByVal sortTable As Type, ByVal sortDirection As Sort.SortDirection,
                                                 ByVal isFromDaftarUpload As Boolean) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(sortTable, sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim spName As String
            Dim Param As New List(Of SqlClient.SqlParameter)

            spName = "sp_RetrieveAssistServiceIncomingCustom"
            Param.Add(New SqlClient.SqlParameter("@Where", criterias.ToString()))
            Param.Add(New SqlClient.SqlParameter("@Sort ", sortColl.ToString()))
            Param.Add(New SqlClient.SqlParameter("@PageNumber", pageNumber))
            Param.Add(New SqlClient.SqlParameter("@PageSize", pageSize))
            Param.Add(New SqlClient.SqlParameter("@isFromDaftarUpload", isFromDaftarUpload))
            ' New ArrayList(Param)
            Return m_AssistServiceIncomingMapper.RetrieveCustomPagingBySP(spName, New ArrayList(Param), pageNumber, pageSize, totalRow)
        End Function

        Public Function DownloadDataBySP(ByVal criterias As ICriteria, ByVal sortColl As SortCollection, ByVal isFromDaftarUpload As Boolean) As ArrayList
            Dim spName As String
            Dim Param As New List(Of SqlClient.SqlParameter)

            spName = "sp_RetrieveAssistServiceIncomingCustom_Download"
            Param.Add(New SqlClient.SqlParameter("@Where", criterias.ToString()))
            Param.Add(New SqlClient.SqlParameter("@Sort ", sortColl.ToString()))
            Param.Add(New SqlClient.SqlParameter("@isFromDaftarUpload", isFromDaftarUpload))
            ' New ArrayList(Param)
            Return m_AssistServiceIncomingMapper.RetrieveSP(spName, New ArrayList(Param))
        End Function

#End Region

    End Class

End Namespace

