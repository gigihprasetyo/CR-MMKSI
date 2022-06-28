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
'// Generated on 10/7/2005 - 1:28:25 PM
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

Namespace KTB.DNet.BusinessFacade.Service

    Public Class WSCEvidenceBBFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_WSCEvidenceBBMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_WSCEvidenceBBMapper = MapperFactory.GetInstance.GetMapper(GetType(WSCEvidenceBB).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As WSCEvidenceBB
            Return CType(m_WSCEvidenceBBMapper.Retrieve(ID), WSCEvidenceBB)
        End Function

        Public Function Retrieve(ByVal Code As String) As WSCEvidenceBB
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCEvidenceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCEvidenceBB), "PODetailCode", MatchType.Exact, Code))

            Dim PODetailColl As ArrayList = m_WSCEvidenceBBMapper.RetrieveByCriteria(criterias)
            If (PODetailColl.Count > 0) Then
                Return CType(PODetailColl(0), WSCEvidenceBB)
            End If
            Return New WSCEvidenceBB
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_WSCEvidenceBBMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_WSCEvidenceBBMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_WSCEvidenceBBMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(WSCEvidenceBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_WSCEvidenceBBMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(WSCEvidenceBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_WSCEvidenceBBMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCEvidenceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PODetail As ArrayList = m_WSCEvidenceBBMapper.RetrieveByCriteria(criterias)
            Return _PODetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCEvidenceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PODetailColl As ArrayList = m_WSCEvidenceBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PODetailColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(WSCEvidenceBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim WSCEvidenceBBColl As ArrayList = m_WSCEvidenceBBMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return WSCEvidenceBBColl
        End Function



        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PODetailColl As ArrayList = m_WSCEvidenceBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PODetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCEvidenceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PODetailColl As ArrayList = m_WSCEvidenceBBMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(WSCEvidenceBB), columnName, matchOperator, columnValue))
            Return PODetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.domain.Search.SortCollection = New KTB.DNet.domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.domain.Search.Sort(GetType(WSCEvidenceBB), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCEvidenceBB), columnName, matchOperator, columnValue))

            Return m_WSCEvidenceBBMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub Update(ByVal objDomain As WSCEvidenceBB)
            Try
                m_WSCEvidenceBBMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Insert(ByVal objDomain As WSCEvidenceBB)
            Dim iReturn As Integer = -2
            Try
                m_WSCEvidenceBBMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try

        End Sub

        Public Function DeleteFromDB(ByVal objDomain As WSCEvidenceBB) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_WSCEvidenceBBMapper.Delete(objDomain)
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

#End Region

    End Class

End Namespace

