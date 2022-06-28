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
'// Generated on 8/3/2005 - 10:53:00 AM
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

Namespace KTB.DNet.BusinessFacade.General
    Public Class StatusChangeHistorySendEmailFlagFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_StatusChangeHistorySendEmailFlagMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_StatusChangeHistorySendEmailFlagMapper = MapperFactory.GetInstance().GetMapper(GetType(StatusChangeHistorySendEmailFlag).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As StatusChangeHistorySendEmailFlag
            Return CType(m_StatusChangeHistorySendEmailFlagMapper.Retrieve(ID), StatusChangeHistorySendEmailFlag)
        End Function

        'Public Function IsStatusChangeHistorySendEmailFlagFound(ByVal strStatusChangeHistorySendEmailFlagCode As String) As Boolean
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistorySendEmailFlag), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Dim bResult As Boolean = False
        '    criterias.opAnd(New Criteria(GetType(StatusChangeHistorySendEmailFlag), "StatusChangeHistorySendEmailFlagCode", MatchType.Exact, strStatusChangeHistorySendEmailFlagCode))
        '    Dim StatusChangeHistorySendEmailFlagColl As ArrayList = m_StatusChangeHistorySendEmailFlagMapper.RetrieveByCriteria(criterias)
        '    If (StatusChangeHistorySendEmailFlagColl.Count > 0) Then
        '        bResult = True
        '    Else
        '    End If
        '    Return bResult

        'End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_StatusChangeHistorySendEmailFlagMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_StatusChangeHistorySendEmailFlagMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_StatusChangeHistorySendEmailFlagMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistorySendEmailFlag), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_StatusChangeHistorySendEmailFlagMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistorySendEmailFlag), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_StatusChangeHistorySendEmailFlagMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistorySendEmailFlag), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _StatusChangeHistorySendEmailFlag As ArrayList = m_StatusChangeHistorySendEmailFlagMapper.RetrieveByCriteria(criterias)
            Return _StatusChangeHistorySendEmailFlag
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistorySendEmailFlag), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim StatusChangeHistorySendEmailFlagColl As ArrayList = m_StatusChangeHistorySendEmailFlagMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return StatusChangeHistorySendEmailFlagColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistorySendEmailFlag), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_StatusChangeHistorySendEmailFlagMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistorySendEmailFlag), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim StatusChangeHistorySendEmailFlagColl As ArrayList = m_StatusChangeHistorySendEmailFlagMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return StatusChangeHistorySendEmailFlagColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistorySendEmailFlag), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistorySendEmailFlag), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_StatusChangeHistorySendEmailFlagMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim StatusChangeHistorySendEmailFlagColl As ArrayList = m_StatusChangeHistorySendEmailFlagMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return StatusChangeHistorySendEmailFlagColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistorySendEmailFlag), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistorySendEmailFlag), columnName, matchOperator, columnValue))
            Dim StatusChangeHistorySendEmailFlagColl As ArrayList = m_StatusChangeHistorySendEmailFlagMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return StatusChangeHistorySendEmailFlagColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistorySendEmailFlag), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistorySendEmailFlag), columnName, matchOperator, columnValue))

            Return m_StatusChangeHistorySendEmailFlagMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As StatusChangeHistorySendEmailFlag) As Integer
            Dim iReturn As Integer = -2
            Try
                m_StatusChangeHistorySendEmailFlagMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Insert(ByVal arl As ArrayList) As Integer
            Dim iReturn As Integer = -2
            Try
                For Each objHist As StatusChangeHistorySendEmailFlag In arl
                    m_StatusChangeHistorySendEmailFlagMapper.Insert(objHist, m_userPrincipal.Identity.Name)
                Next

            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return arl.Count

        End Function

        Public Function Insert(ByVal objStatusChangeHistory As StatusChangeHistory, ByVal IsSendEmail As Integer) As Integer
            Dim iReturn As Integer = -2
            Dim objDomain As New StatusChangeHistorySendEmailFlag
            objDomain.StatusChangeHistory = objStatusChangeHistory
            objDomain.IsSendEmail = IsSendEmail
            Try
                m_StatusChangeHistorySendEmailFlagMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As StatusChangeHistorySendEmailFlag) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_StatusChangeHistorySendEmailFlagMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Sub Delete(ByVal objDomain As StatusChangeHistorySendEmailFlag)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_StatusChangeHistorySendEmailFlagMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As StatusChangeHistorySendEmailFlag)
            Try
                m_StatusChangeHistorySendEmailFlagMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistorySendEmailFlag), "AreaCode", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(StatusChangeHistorySendEmailFlag), "AreaCode", AggregateType.Count)

        '    Return CType(m_StatusChangeHistorySendEmailFlagMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

