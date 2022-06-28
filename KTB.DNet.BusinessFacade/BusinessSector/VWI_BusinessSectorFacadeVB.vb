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
'// Copyright  2017
'// ---------------------
'// $History      : $
'// Generated on 8/25/2017 - 2:50:17 PM
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class VWI_BusinessSectorFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_VWI_BusinessSectorMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_VWI_BusinessSectorMapper = MapperFactory.GetInstance.GetMapper(GetType(VWI_BusinessSector).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As VWI_BusinessSector
            Return CType(m_VWI_BusinessSectorMapper.Retrieve(ID), VWI_BusinessSector)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VWI_BusinessSectorMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VWI_BusinessSectorMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VWI_BusinessSectorMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_BusinessSector), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_BusinessSectorMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_BusinessSector), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_BusinessSectorMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VWI_BusinessSectorColl As ArrayList = m_VWI_BusinessSectorMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return VWI_BusinessSectorColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim VWI_BusinessSectorColl As ArrayList = m_VWI_BusinessSectorMapper.RetrieveByCriteria(criterias)
            Return VWI_BusinessSectorColl
        End Function

        Public Function GetVWI_BusinessSectorByName(ByVal name As String) As VWI_BusinessSector
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_BusinessSector), "BusinessName", MatchType.Exact, name))
            Dim VWI_BusinessSectorColl As ArrayList = m_VWI_BusinessSectorMapper.RetrieveByCriteria(criterias)
            If VWI_BusinessSectorColl.Count > 0 Then
                Return CType(VWI_BusinessSectorColl(0), VWI_BusinessSector)
            Else
                Return Nothing
            End If
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(VWI_BusinessSector), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim VWI_BusinessSectorColl As ArrayList = m_VWI_BusinessSectorMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return VWI_BusinessSectorColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_BusinessSector), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_BusinessSector), columnName, matchOperator, columnValue))

            Return m_VWI_BusinessSectorMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region


#Region "Custom Method"
        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_BusinessSector), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(VWI_BusinessSector), "Code", AggregateType.Count)

            Return CType(m_VWI_BusinessSectorMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateSequence(ByVal code As String, ByVal sequence As String, Optional isCreate As Boolean = True) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_BusinessSector), "Sequence", MatchType.Exact, sequence))
            If (isCreate = False) Then
                crit.opAnd(New Criteria(GetType(VWI_BusinessSector), "Code", MatchType.No, code))
            End If

            Dim agg As Aggregate = New Aggregate(GetType(VWI_BusinessSector), "Sequence", AggregateType.Count)

            Return CType(m_VWI_BusinessSectorMapper.RetrieveScalar(agg, crit), Integer)
        End Function
#End Region

    End Class

End Namespace

