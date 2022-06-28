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
'// Generated on 11/09/2005 - 9:04:49 AM
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

Namespace KTB.DNet.BusinessFacade.SparePart

    Public Class PartIncidentalPriorityDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PartIncidentalPriorityDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PartIncidentalPriorityDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PartIncidentalPriorityDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PartIncidentalPriorityDetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PartIncidentalPriorityDetail
            Return CType(m_PartIncidentalPriorityDetailMapper.Retrieve(ID), PartIncidentalPriorityDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As PartIncidentalPriorityDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalPriorityDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PartIncidentalPriorityDetail), "TypeCode", MatchType.Exact, Code))

            Dim PartIncidentalPriorityDetailColl As ArrayList = m_PartIncidentalPriorityDetailMapper.RetrieveByCriteria(criterias)
            If (PartIncidentalPriorityDetailColl.Count > 0) Then
                Return CType(PartIncidentalPriorityDetailColl(0), PartIncidentalPriorityDetail)
            End If
            Return New PartIncidentalPriorityDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PartIncidentalPriorityDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PartIncidentalPriorityDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PartIncidentalPriorityDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PartIncidentalPriorityDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PartIncidentalPriorityDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PartIncidentalPriorityDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PartIncidentalPriorityDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PartIncidentalPriorityDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PartIncidentalPriorityDetailColl As ArrayList = m_PartIncidentalPriorityDetailMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PartIncidentalPriorityDetailColl
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalPriorityDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PartIncidentalPriorityDetail As ArrayList = m_PartIncidentalPriorityDetailMapper.RetrieveByCriteria(criterias)
            Return _PartIncidentalPriorityDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalPriorityDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PartIncidentalPriorityDetailColl As ArrayList = m_PartIncidentalPriorityDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PartIncidentalPriorityDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PartIncidentalPriorityDetailColl As ArrayList = m_PartIncidentalPriorityDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PartIncidentalPriorityDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalPriorityDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PartIncidentalPriorityDetailColl As ArrayList = m_PartIncidentalPriorityDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PartIncidentalPriorityDetail), columnName, matchOperator, columnValue))
            Return PartIncidentalPriorityDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PartIncidentalPriorityDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalPriorityDetail), columnName, matchOperator, columnValue))

            Return m_PartIncidentalPriorityDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As PartIncidentalPriorityDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                m_PartIncidentalPriorityDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub Update(ByVal objDomain As PartIncidentalPriorityDetail)
            Try
                m_PartIncidentalPriorityDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub




        Public Sub DeleteFromDB(ByVal objDomain As PartIncidentalPriorityDetail)
            Try
                m_PartIncidentalPriorityDetailMapper.Delete(objDomain)
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