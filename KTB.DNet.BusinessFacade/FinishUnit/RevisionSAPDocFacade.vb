﻿
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
'// Generated on 9/5/2018 - 9:58:42 AM
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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class RevisionSAPDocFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_RevisionSAPDocMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_RevisionSAPDocMapper = MapperFactory.GetInstance.GetMapper(GetType(RevisionSAPDoc).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As RevisionSAPDoc
            Return CType(m_RevisionSAPDocMapper.Retrieve(ID), RevisionSAPDoc)
        End Function

        Public Function Retrieve(ByVal DCNumber As String) As RevisionSAPDoc
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionSAPDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionSAPDoc), "DebitChargeNo", MatchType.Exact, DCNumber))

            Dim arrRevisionSAPDoc As ArrayList = m_RevisionSAPDocMapper.RetrieveByCriteria(criterias)
            If (arrRevisionSAPDoc.Count > 0) Then
                Return CType(arrRevisionSAPDoc(0), RevisionSAPDoc)
            End If
            Return New RevisionSAPDoc
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_RevisionSAPDocMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RevisionSAPDocMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_RevisionSAPDocMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionSAPDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionSAPDocMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionSAPDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionSAPDocMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionSAPDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _RevisionSAPDoc As ArrayList = m_RevisionSAPDocMapper.RetrieveByCriteria(criterias)
            Return _RevisionSAPDoc
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionSAPDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RevisionSAPDocColl As ArrayList = m_RevisionSAPDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RevisionSAPDocColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim RevisionSAPDocColl As ArrayList = m_RevisionSAPDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return RevisionSAPDocColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionSAPDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RevisionSAPDocColl As ArrayList = m_RevisionSAPDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(RevisionSAPDoc), columnName, matchOperator, columnValue))
            Return RevisionSAPDocColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionSAPDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionSAPDoc), columnName, matchOperator, columnValue))

            Return m_RevisionSAPDocMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As RevisionSAPDoc) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_RevisionSAPDocMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As RevisionSAPDoc) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_RevisionSAPDocMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As RevisionSAPDoc)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_RevisionSAPDocMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As RevisionSAPDoc)
            Try
                m_RevisionSAPDocMapper.Delete(objDomain)
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

