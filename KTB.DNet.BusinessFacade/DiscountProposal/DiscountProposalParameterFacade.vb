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
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 7/15/2020 - 2:18:46 PM
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

Imports KTB.DNET.Domain.Search.Domain
Imports KTB.DNET.Domain.Search.Domain.Search
Imports KTB.DNET.Domain.Search.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.DataMapper.Framework
Imports KTB.DNET.Domain

#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class DiscountProposalParameterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DiscountProposalParameterMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DiscountProposalParameterMapper = MapperFactory.GetInstance.GetMapper(GetType(DiscountProposalParameter).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DiscountProposalParameter
            Return CType(m_DiscountProposalParameterMapper.Retrieve(ID), DiscountProposalParameter)
        End Function

        Public Function Retrieve(ByVal Code As String) As DiscountProposalParameter
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "DiscountProposalParameterCode", MatchType.Exact, Code))

            Dim DiscountProposalParameterColl As ArrayList = m_DiscountProposalParameterMapper.RetrieveByCriteria(criterias)
            If (DiscountProposalParameterColl.Count > 0) Then
                Return CType(DiscountProposalParameterColl(0), DiscountProposalParameter)
            End If
            Return New DiscountProposalParameter
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DiscountProposalParameterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DiscountProposalParameterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DiscountProposalParameterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DiscountProposalParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalParameterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DiscountProposalParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalParameterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DiscountProposalParameter As ArrayList = m_DiscountProposalParameterMapper.RetrieveByCriteria(criterias)
            Return _DiscountProposalParameter
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalParameterColl As ArrayList = m_DiscountProposalParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DiscountProposalParameterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DiscountProposalParameterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DiscountProposalParameterColl As ArrayList = m_DiscountProposalParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DiscountProposalParameterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalParameterColl As ArrayList = m_DiscountProposalParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), columnName, matchOperator, columnValue))
            Return DiscountProposalParameterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DiscountProposalParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), columnName, matchOperator, columnValue))

            Return m_DiscountProposalParameterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), "DiscountProposalParameterCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DiscountProposalParameter), "DiscountProposalParameterCode", AggregateType.Count)
            Return CType(m_DiscountProposalParameterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DiscountProposalParameter) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DiscountProposalParameterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DiscountProposalParameter) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DiscountProposalParameterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
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
