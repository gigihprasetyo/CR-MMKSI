
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

    Public Class PQRPartsCodeBBFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PQRPartsCodeBBMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PQRPartsCodeBBMapper = MapperFactory.GetInstance.GetMapper(GetType(PQRPartsCodeBB).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PQRPartsCodeBB
            Return CType(m_PQRPartsCodeBBMapper.Retrieve(ID), PQRPartsCodeBB)
        End Function

        Public Function RetrieveByPQRID(ByVal PQRHeaderBBID As Integer) As PQRPartsCodeBB
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRPartsCodeBB), "PQRHeaderBBID", MatchType.Exact, PQRHeaderBBID))

            Dim partColl As ArrayList = m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias)
            If (partColl.Count > 0) Then
                Return CType(partColl(0), PQRPartsCodeBB)
            End If
            Return New PQRPartsCodeBB
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PQRPartsCodeBBMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRPartsCodeBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PQRPartsCodeBBMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRPartsCodeBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PQRPartsCodeBBMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRPartsCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PQRPartsCodeBB As ArrayList = m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias)
            Return _PQRPartsCodeBB
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRPartsCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRPartsCodeBBColl As ArrayList = m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PQRPartsCodeBBColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRPartsCodeBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRPartsCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRPartsCodeBBColl As ArrayList = m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return PQRPartsCodeBBColl
        End Function

        Public Function RetrieveActiveList(ByVal ExtModel As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRPartsCodeBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRPartsCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRPartsCodeBBColl As ArrayList = m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return PQRPartsCodeBBColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PQRPartsCodeBBColl As ArrayList = m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PQRPartsCodeBBColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(PQRPartsCodeBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PQRPartsCodeBBColl As ArrayList = m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias, sortColl)
            Return PQRPartsCodeBBColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(PQRPartsCodeBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PQRPartsCodeBBColl As ArrayList = m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PQRPartsCodeBBColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRPartsCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PQRPartsCodeBB), columnName, matchOperator, columnValue))
            Dim PQRPartsCodeBBColl As ArrayList = m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PQRPartsCodeBBColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRPartsCodeBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRPartsCodeBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PQRPartsCodeBB), columnName, matchOperator, columnValue))

            Return m_PQRPartsCodeBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_PQRPartsCodeBBMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal PQRHeaderBBID As integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRPartsCodeBB), "PQRHeaderBBID", MatchType.Exact, PQRHeaderBBID))
            Dim agg As Aggregate = New Aggregate(GetType(PQRPartsCodeBB), "PQRHeaderBBID", AggregateType.Count)
            Return CType(m_PQRPartsCodeBBMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        'Public Function InsertTransaction(ByVal objPQRPartsCodeBB As PQRPartsCodeBB, ByVal arrPQRDamageCodeBB As ArrayList, ByVal arrPQRParts As ArrayList, ByVal arrPQRAttachmentBBTop As ArrayList, ByVal arrPQRAttachmentBBBottom As ArrayList, ByVal arrPQRSolutionReferences As ArrayList) As Integer
        '    Dim returnValue As Integer = -1
        '    If (Me.IsTaskFree()) Then
        '        Try
        '            Me.SetTaskLocking()
        '            m_TransactionManager.AddInsert(objPQRPartsCodeBB, m_userPrincipal.Identity.Name)

        '            If arrPQRDamageCodeBB.Count > 0 Then
        '                For Each oPQRDamageCodeBB As PQRDamageCodeBB In arrPQRDamageCodeBB
        '                    oPQRDamageCodeBB.PQRPartsCodeBB = objPQRPartsCodeBB
        '                    m_TransactionManager.AddInsert(oPQRDamageCodeBB, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            If arrPQRParts.Count > 0 Then
        '                For Each oPQRParts As PQRPartsCodeBB In arrPQRParts
        '                    oPQRParts.PQRPartsCodeBB = objPQRPartsCodeBB
        '                    m_TransactionManager.AddInsert(oPQRParts, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            If arrPQRAttachmentBBTop.Count > 0 Then
        '                For Each oPQRAttachmentBBTop As PQRAttachmentBB In arrPQRAttachmentBBTop
        '                    oPQRAttachmentBBTop.PQRPartsCodeBB = objPQRPartsCodeBB
        '                    m_TransactionManager.AddInsert(oPQRAttachmentBBTop, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            'If arrPQRAttachmentBBBottom.Count > 0 Then
        '            '    For Each oPQRDamageCodeBB As PQRDamageCodeBB In arrPQRDamageCodeBB
        '            '        oPQRDamageCodeBB.PQRPartsCodeBB = objPQRPartsCodeBB
        '            '        m_TransactionManager.AddInsert(oPQRDamageCodeBB, m_userPrincipal.Identity.Name)
        '            '    Next
        '            'End If

        '            If arrPQRSolutionReferences.Count > 0 Then
        '                For Each oPQRSolutionReferences As PQRSolutionReferences In arrPQRSolutionReferences
        '                    oPQRSolutionReferences.PQRPartsCodeBB = objPQRPartsCodeBB
        '                    m_TransactionManager.AddInsert(oPQRSolutionReferences, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            m_TransactionManager.PerformTransaction()
        '            returnValue = objPQRPartsCodeBB.ID
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

        Public Function Insert(ByVal objDomain As PQRPartsCodeBB) As Integer
            Dim iReturn As Integer = -2
            Try
                m_PQRPartsCodeBBMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As PQRPartsCodeBB) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PQRPartsCodeBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As PQRPartsCodeBB)
            Dim nResult As Integer = -1
            Try
                nResult = CType(DBRowStatus.Deleted, Short)
                objDomain.RowStatus = nResult
                m_PQRPartsCodeBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As PQRPartsCodeBB)
            Try
                m_PQRPartsCodeBBMapper.Delete(objDomain)
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




