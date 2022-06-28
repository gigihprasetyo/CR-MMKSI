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
'// Author Name   : Ariwibawa
'// PURPOSE       : Facade for Page Event - Parameter Event
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2009 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2009 - 11:26:00 AM
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

Namespace KTB.DNet.BusinessFacade.Event
    Public Class EventProposalExcelFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_V_EventProposalExcelMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_EventProposalExcelMapper = MapperFactory.GetInstance().GetMapper(GetType(V_EventProposalExcel).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As V_EventProposalExcel
            Return CType(m_V_EventProposalExcelMapper.Retrieve(ID), V_EventProposalExcel)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_V_EventProposalExcelMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_V_EventProposalExcelMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_V_EventProposalExcelMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_EventProposalExcel), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_EventProposalExcelMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_EventProposalExcel), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_EventProposalExcelMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_EventProposalExcel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _V_EventProposalExcel As ArrayList = m_V_EventProposalExcelMapper.RetrieveByCriteria(criterias)
            Return _V_EventProposalExcel
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_EventProposalExcel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim V_EventProposalExcelColl As ArrayList = m_V_EventProposalExcelMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_EventProposalExcelColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_EventProposalExcel), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim V_EventProposalExcelColl As ArrayList = m_V_EventProposalExcelMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return V_EventProposalExcelColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_EventProposalExcel), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_V_EventProposalExcelMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_EventProposalExcel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_EventProposalExcel), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_EventProposalExcelMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim V_EventProposalExcelColl As ArrayList = m_V_EventProposalExcelMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_EventProposalExcelColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_EventProposalExcel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(V_EventProposalExcel), columnName, matchOperator, columnValue))
            Dim V_EventProposalExcelColl As ArrayList = m_V_EventProposalExcelMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_EventProposalExcelColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_EventProposalExcel), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_EventProposalExcel), columnName, matchOperator, columnValue))

            Return m_V_EventProposalExcelMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As V_EventProposalExcel) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_V_EventProposalExcelMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As V_EventProposalExcel) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_V_EventProposalExcelMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As V_EventProposalExcel) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_V_EventProposalExcelMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nresult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As V_EventProposalExcel) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_V_EventProposalExcelMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region

#Region "Custom Method"
        Private Function GenerateDateCriteria(ByVal nDate As Date, ByVal startDate As Boolean) As DateTime
            Dim Hour As Integer
            Dim Minute As Integer
            Dim Second As Integer

            If startDate Then
                Hour = 0
                Minute = 0
                Second = 0
            Else
                Hour = 23
                Minute = 59
                Second = 59
            End If

            Return New DateTime(nDate.Year, nDate.Month, nDate.Day, Hour, Minute, Second)
        End Function
#End Region

    End Class
End Namespace