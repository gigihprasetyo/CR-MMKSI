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
'// Generated on 8/24/2007 - 3:10:15 PM
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
Imports ktb.dnet.datamapper
Imports KTB.DNet.DataMapper.Framework
Imports ktb.dnet.businessfacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.ShowroomAudit

    Public Class AuditParameterPhotoFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AuditParameterPhotoMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_AuditParameterPhotoMapper = MapperFactory.GetInstance.GetMapper(GetType(AuditParameterPhoto).ToString)
            m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AuditParameterPhoto
            Return CType(m_AuditParameterPhotoMapper.Retrieve(ID), AuditParameterPhoto)
        End Function

        Public Function Retrieve(ByVal Code As String) As AuditParameterPhoto
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameterPhoto), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AuditParameterPhoto), "AuditParameterPhotoCode", MatchType.Exact, Code))

            Dim AuditParameterPhotoColl As ArrayList = m_AuditParameterPhotoMapper.RetrieveByCriteria(criterias)
            If (AuditParameterPhotoColl.Count > 0) Then
                Return CType(AuditParameterPhotoColl(0), AuditParameterPhoto)
            End If
            Return New AuditParameterPhoto
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AuditParameterPhotoMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AuditParameterPhotoMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AuditParameterPhotoMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditParameterPhoto), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AuditParameterPhotoMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditParameterPhoto), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AuditParameterPhotoMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameterPhoto), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AuditParameterPhoto As ArrayList = m_AuditParameterPhotoMapper.RetrieveByCriteria(criterias)
            Return _AuditParameterPhoto
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameterPhoto), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AuditParameterPhotoColl As ArrayList = m_AuditParameterPhotoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AuditParameterPhotoColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AuditParameterPhotoColl As ArrayList = m_AuditParameterPhotoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return AuditParameterPhotoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameterPhoto), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AuditParameterPhotoColl As ArrayList = m_AuditParameterPhotoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AuditParameterPhoto), columnName, matchOperator, columnValue))
            Return AuditParameterPhotoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditParameterPhoto), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameterPhoto), columnName, matchOperator, columnValue))

            Return m_AuditParameterPhotoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameterPhoto), "AuditParameterPhotoCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(AuditParameterPhoto), "AuditParameterPhotoCode", AggregateType.Count)
            Return CType(m_AuditParameterPhotoMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As AuditParameterPhoto) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AuditParameterPhotoMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As AuditParameterPhoto)
            Try
                m_AuditParameterPhotoMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Update(ByVal objDomain As AuditParameterPhoto) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AuditParameterPhotoMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

