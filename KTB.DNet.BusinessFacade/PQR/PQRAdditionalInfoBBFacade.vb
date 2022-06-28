
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

    Public Class PQRAdditionalInfoBBFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PQRAdditionalInfoBBMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PQRAdditionalInfoBBMapper = MapperFactory.GetInstance.GetMapper(GetType(PQRAdditionalInfoBB).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PQRAdditionalInfoBB
            Return CType(m_PQRAdditionalInfoBBMapper.Retrieve(ID), PQRAdditionalInfoBB)
        End Function

        Public Function RetrieveByPQRID(ByVal PQRHeaderBBID As Integer) As PQRAdditionalInfoBB
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRAdditionalInfoBB), "PQRHeaderBBID", MatchType.Exact, PQRHeaderBBID))

            Dim partColl As ArrayList = m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias)
            If (partColl.Count > 0) Then
                Return CType(partColl(0), PQRAdditionalInfoBB)
            End If
            Return New PQRAdditionalInfoBB
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PQRAdditionalInfoBBMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRAdditionalInfoBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PQRAdditionalInfoBBMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRAdditionalInfoBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PQRAdditionalInfoBBMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRAdditionalInfoBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PQRAdditionalInfoBB As ArrayList = m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias)
            Return _PQRAdditionalInfoBB
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRAdditionalInfoBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRAdditionalInfoBBColl As ArrayList = m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PQRAdditionalInfoBBColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRAdditionalInfoBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRAdditionalInfoBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRAdditionalInfoBBColl As ArrayList = m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return PQRAdditionalInfoBBColl
        End Function

        Public Function RetrieveActiveList(ByVal ExtModel As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRAdditionalInfoBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRAdditionalInfoBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRAdditionalInfoBBColl As ArrayList = m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return PQRAdditionalInfoBBColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PQRAdditionalInfoBBColl As ArrayList = m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PQRAdditionalInfoBBColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(PQRAdditionalInfoBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PQRAdditionalInfoBBColl As ArrayList = m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias, sortColl)
            Return PQRAdditionalInfoBBColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(PQRAdditionalInfoBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PQRAdditionalInfoBBColl As ArrayList = m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PQRAdditionalInfoBBColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRAdditionalInfoBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PQRAdditionalInfoBB), columnName, matchOperator, columnValue))
            Dim PQRAdditionalInfoBBColl As ArrayList = m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PQRAdditionalInfoBBColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRAdditionalInfoBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRAdditionalInfoBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PQRAdditionalInfoBB), columnName, matchOperator, columnValue))

            Return m_PQRAdditionalInfoBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_PQRAdditionalInfoBBMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal PQRHeaderBBID As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRAdditionalInfoBB), "PQRHeaderBBID", MatchType.Exact, PQRHeaderBBID))
            Dim agg As Aggregate = New Aggregate(GetType(PQRAdditionalInfoBB), "PQRHeaderBBID", AggregateType.Count)
            Return CType(m_PQRAdditionalInfoBBMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        'Public Function InsertTransaction(ByVal objPQRAdditionalInfoBB As PQRAdditionalInfoBB, ByVal arrPQRDamageCodeBB As ArrayList, ByVal arrPQRParts As ArrayList, ByVal arrPQRAttachmentBBTop As ArrayList, ByVal arrPQRAttachmentBBBottom As ArrayList, ByVal arrPQRSolutionReferences As ArrayList) As Integer
        '    Dim returnValue As Integer = -1
        '    If (Me.IsTaskFree()) Then
        '        Try
        '            Me.SetTaskLocking()
        '            m_TransactionManager.AddInsert(objPQRAdditionalInfoBB, m_userPrincipal.Identity.Name)

        '            If arrPQRDamageCodeBB.Count > 0 Then
        '                For Each oPQRDamageCodeBB As PQRDamageCodeBB In arrPQRDamageCodeBB
        '                    oPQRDamageCodeBB.PQRAdditionalInfoBB = objPQRAdditionalInfoBB
        '                    m_TransactionManager.AddInsert(oPQRDamageCodeBB, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            If arrPQRParts.Count > 0 Then
        '                For Each oPQRParts As PQRPartsCodeBB In arrPQRParts
        '                    oPQRParts.PQRAdditionalInfoBB = objPQRAdditionalInfoBB
        '                    m_TransactionManager.AddInsert(oPQRParts, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            If arrPQRAttachmentBBTop.Count > 0 Then
        '                For Each oPQRAttachmentBBTop As PQRAttachmentBB In arrPQRAttachmentBBTop
        '                    oPQRAttachmentBBTop.PQRAdditionalInfoBB = objPQRAdditionalInfoBB
        '                    m_TransactionManager.AddInsert(oPQRAttachmentBBTop, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            'If arrPQRAttachmentBBBottom.Count > 0 Then
        '            '    For Each oPQRDamageCodeBB As PQRDamageCodeBB In arrPQRDamageCodeBB
        '            '        oPQRDamageCodeBB.PQRAdditionalInfoBB = objPQRAdditionalInfoBB
        '            '        m_TransactionManager.AddInsert(oPQRDamageCodeBB, m_userPrincipal.Identity.Name)
        '            '    Next
        '            'End If

        '            If arrPQRSolutionReferences.Count > 0 Then
        '                For Each oPQRSolutionReferences As PQRSolutionReferences In arrPQRSolutionReferences
        '                    oPQRSolutionReferences.PQRAdditionalInfoBB = objPQRAdditionalInfoBB
        '                    m_TransactionManager.AddInsert(oPQRSolutionReferences, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            m_TransactionManager.PerformTransaction()
        '            returnValue = objPQRAdditionalInfoBB.ID
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

        Public Function Insert(ByVal objDomain As PQRAdditionalInfoBB) As Integer
            Dim iReturn As Integer = -2
            Try
                m_PQRAdditionalInfoBBMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As PQRAdditionalInfoBB) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PQRAdditionalInfoBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As PQRAdditionalInfoBB)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_PQRAdditionalInfoBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As PQRAdditionalInfoBB)
            Try
                m_PQRAdditionalInfoBBMapper.Delete(objDomain)
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




