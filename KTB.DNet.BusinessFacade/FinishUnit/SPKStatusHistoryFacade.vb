
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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 9/26/2005 - 2:38:25 PM
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

Namespace KTB.DNET.BusinessFacade

    Public Class SPKStatusHistoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SPKStatusHistoryMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SPKStatusHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(SPKStatusHistory).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SPKStatusHistory
            Return CType(m_SPKStatusHistoryMapper.Retrieve(ID), SPKStatusHistory)
        End Function

        Public Function RetrieveBySPKID(ByVal SPKHeaderID As Integer) As SPKStatusHistory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKStatusHistory), "SPKHeaderID", MatchType.Exact, SPKHeaderID))

            Dim partColl As ArrayList = m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias)
            If (partColl.Count > 0) Then
                Return CType(partColl(0), SPKStatusHistory)
            End If
            Return New SPKStatusHistory
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SPKStatusHistoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKStatusHistoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKStatusHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SPKStatusHistory As ArrayList = m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias)
            Return _SPKStatusHistory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKStatusHistoryColl As ArrayList = m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPKStatusHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKStatusHistoryColl As ArrayList = m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SPKStatusHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal ExtModel As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKStatusHistoryColl As ArrayList = m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SPKStatusHistoryColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SPKStatusHistoryColl As ArrayList = m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SPKStatusHistoryColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(SPKStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SPKStatusHistoryColl As ArrayList = m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias, sortColl)
            Return SPKStatusHistoryColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(SPKStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SPKStatusHistoryColl As ArrayList = m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPKStatusHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKStatusHistory), columnName, matchOperator, columnValue))
            Dim SPKStatusHistoryColl As ArrayList = m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SPKStatusHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKStatusHistory), columnName, matchOperator, columnValue))

            Return m_SPKStatusHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_SPKStatusHistoryMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal SPKHeaderID As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKStatusHistory), "SPKHeaderID", MatchType.Exact, SPKHeaderID))
            Dim agg As Aggregate = New Aggregate(GetType(SPKStatusHistory), "SPKHeaderID", AggregateType.Count)
            Return CType(m_SPKStatusHistoryMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        'Public Function InsertTransaction(ByVal objSPKStatusHistory As SPKStatusHistory, ByVal arrSPKDamageCode As ArrayList, ByVal arrSPKParts As ArrayList, ByVal arrSPKAttachmentTop As ArrayList, ByVal arrSPKAttachmentBottom As ArrayList, ByVal arrSPKSolutionReferences As ArrayList) As Integer
        '    Dim returnValue As Integer = -1
        '    If (Me.IsTaskFree()) Then
        '        Try
        '            Me.SetTaskLocking()
        '            m_TransactionManager.AddInsert(objSPKStatusHistory, m_userPrincipal.Identity.Name)

        '            If arrSPKDamageCode.Count > 0 Then
        '                For Each oSPKDamageCode As SPKDamageCode In arrSPKDamageCode
        '                    oSPKDamageCode.SPKStatusHistory = objSPKStatusHistory
        '                    m_TransactionManager.AddInsert(oSPKDamageCode, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            If arrSPKParts.Count > 0 Then
        '                For Each oSPKParts As SPKPartsCode In arrSPKParts
        '                    oSPKParts.SPKStatusHistory = objSPKStatusHistory
        '                    m_TransactionManager.AddInsert(oSPKParts, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            If arrSPKAttachmentTop.Count > 0 Then
        '                For Each oSPKAttachmentTop As SPKAttachment In arrSPKAttachmentTop
        '                    oSPKAttachmentTop.SPKStatusHistory = objSPKStatusHistory
        '                    m_TransactionManager.AddInsert(oSPKAttachmentTop, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            'If arrSPKAttachmentBottom.Count > 0 Then
        '            '    For Each oSPKDamageCode As SPKDamageCode In arrSPKDamageCode
        '            '        oSPKDamageCode.SPKStatusHistory = objSPKStatusHistory
        '            '        m_TransactionManager.AddInsert(oSPKDamageCode, m_userPrincipal.Identity.Name)
        '            '    Next
        '            'End If

        '            If arrSPKSolutionReferences.Count > 0 Then
        '                For Each oSPKSolutionReferences As SPKSolutionReferences In arrSPKSolutionReferences
        '                    oSPKSolutionReferences.SPKStatusHistory = objSPKStatusHistory
        '                    m_TransactionManager.AddInsert(oSPKSolutionReferences, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            m_TransactionManager.PerformTransaction()
        '            returnValue = objSPKStatusHistory.ID
        '        Catch ex As Exception
        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '            If rethrow Then
        '                Throw
        '            End If
        '        Finally
        '            Me.RemoveTaskLocking()
        '        End Try
        '    End If
        '    Return returnValue
        'End Function

        Public Function Insert(ByVal objDomain As SPKStatusHistory) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SPKStatusHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SPKStatusHistory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPKStatusHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SPKStatusHistory)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SPKStatusHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SPKStatusHistory)
            Try
                m_SPKStatusHistoryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


#End Region

#Region "Custom Method"
#End Region

    End Class

End Namespace




