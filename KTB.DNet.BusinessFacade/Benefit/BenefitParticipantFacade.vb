
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
'// Copyright  2015
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 11:25:11 AM
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

Namespace KTB.DNet.BusinessFacade.Benefit

    Public Class BenefitParticipantFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BenefitParticipantMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BenefitParticipantMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitParticipant).ToString)



        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BenefitParticipant
            Return CType(m_BenefitParticipantMapper.Retrieve(ID), BenefitParticipant)
        End Function

        Public Function Retrieve(ByVal Code As String) As BenefitParticipant
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitParticipant), "BenefitParticipantCode", MatchType.Exact, Code))

            Dim BenefitParticipantColl As ArrayList = m_BenefitParticipantMapper.RetrieveByCriteria(criterias)
            If (BenefitParticipantColl.Count > 0) Then
                Return CType(BenefitParticipantColl(0), BenefitParticipant)
            End If
            Return New BenefitParticipant
        End Function

        Public Function Retrieve(ByVal name As String, ByVal ktp As String, ByVal alamat As String) As BenefitParticipant
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitParticipant), "Nama", MatchType.Exact, name))
            criterias.opAnd(New Criteria(GetType(BenefitParticipant), "KTP", MatchType.Exact, ktp))
            criterias.opAnd(New Criteria(GetType(BenefitParticipant), "Alamat", MatchType.Exact, alamat))

            Dim BenefitParticipantColl As ArrayList = m_BenefitParticipantMapper.RetrieveByCriteria(criterias)
            If (BenefitParticipantColl.Count > 0) Then
                Return CType(BenefitParticipantColl(0), BenefitParticipant)
            End If
            Return New BenefitParticipant
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BenefitParticipantMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BenefitParticipantMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BenefitParticipantMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitParticipant), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitParticipantMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitParticipant), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitParticipantMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BenefitParticipant As ArrayList = m_BenefitParticipantMapper.RetrieveByCriteria(criterias)
            Return _BenefitParticipant
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitParticipantColl As ArrayList = m_BenefitParticipantMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BenefitParticipantColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BenefitParticipantColl As ArrayList = m_BenefitParticipantMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BenefitParticipantColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitParticipantColl As ArrayList = m_BenefitParticipantMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BenefitParticipant), columnName, matchOperator, columnValue))
            Return BenefitParticipantColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitParticipant), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitParticipant), columnName, matchOperator, columnValue))

            Return m_BenefitParticipantMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitParticipant), "BenefitParticipantCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BenefitParticipant), "BenefitParticipantCode", AggregateType.Count)
            Return CType(m_BenefitParticipantMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

