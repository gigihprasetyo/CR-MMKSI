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
'// Copyright ? 2005 
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
    Public Class StatusChangeHistoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_StatusChangeHistoryMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_StatusChangeHistoryMapper = MapperFactory.GetInstance().GetMapper(GetType(StatusChangeHistory).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As StatusChangeHistory
            Return CType(m_StatusChangeHistoryMapper.Retrieve(ID), StatusChangeHistory)
        End Function

        'Public Function IsStatusChangeHistoryFound(ByVal strStatusChangeHistoryCode As String) As Boolean
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Dim bResult As Boolean = False
        '    criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "StatusChangeHistoryCode", MatchType.Exact, strStatusChangeHistoryCode))
        '    Dim StatusChangeHistoryColl As ArrayList = m_StatusChangeHistoryMapper.RetrieveByCriteria(criterias)
        '    If (StatusChangeHistoryColl.Count > 0) Then
        '        bResult = True
        '    Else
        '    End If
        '    Return bResult

        'End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_StatusChangeHistoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_StatusChangeHistoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_StatusChangeHistoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_StatusChangeHistoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_StatusChangeHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _StatusChangeHistory As ArrayList = m_StatusChangeHistoryMapper.RetrieveByCriteria(criterias)
            Return _StatusChangeHistory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim StatusChangeHistoryColl As ArrayList = m_StatusChangeHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return StatusChangeHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_StatusChangeHistoryMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim StatusChangeHistoryColl As ArrayList = m_StatusChangeHistoryMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return StatusChangeHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_StatusChangeHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim StatusChangeHistoryColl As ArrayList = m_StatusChangeHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return StatusChangeHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), columnName, matchOperator, columnValue))
            Dim StatusChangeHistoryColl As ArrayList = m_StatusChangeHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return StatusChangeHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(StatusChangeHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), columnName, matchOperator, columnValue))

            Return m_StatusChangeHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As StatusChangeHistory) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_StatusChangeHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Insert(ByVal arl As ArrayList) As Integer
            Dim iReturn As Integer = -2
            Try
                For Each objHist As StatusChangeHistory In arl
                    m_StatusChangeHistoryMapper.Insert(objHist, m_userPrincipal.Identity.Name)
                Next

            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return arl.Count

        End Function

        Public Function Insert(ByVal DocumentType As Integer, ByVal DocumentNumber As String, ByVal oldStatus As Integer, ByVal Newstatus As Integer) As Integer
            Dim iReturn As Integer = -2
            Dim objDomain As New StatusChangeHistory
            objDomain.DocumentType = DocumentType
            objDomain.DocumentRegNumber = DocumentNumber
            objDomain.OldStatus = oldStatus
            objDomain.NewStatus = Newstatus
            Try
                m_StatusChangeHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As StatusChangeHistory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_StatusChangeHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Sub Delete(ByVal objDomain As StatusChangeHistory)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_StatusChangeHistoryMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As StatusChangeHistory)
            Try
                m_StatusChangeHistoryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "AreaCode", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(StatusChangeHistory), "AreaCode", AggregateType.Count)

        '    Return CType(m_StatusChangeHistoryMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByDocumentRegNumber(ByVal DocumentRegNumber As String, ByVal DocType As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.InSet, "(" & DocType & ")"))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, DocumentRegNumber))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(StatusChangeHistory), "id", Sort.SortDirection.ASC))
            Return m_StatusChangeHistoryMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

#End Region

    End Class

End Namespace

