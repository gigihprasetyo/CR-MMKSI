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
'// Generated on 9/26/2005 - 1:07:25 PM
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

Imports KTB.Dnet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.Dnet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.Dnet.BusinessFacade.UserManagement
    Public Class PCPhisingGuardFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PCPhisingGuardMapper As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PCPhisingGuardMapper = MapperFactory.GetInstance.GetMapper(GetType(PCPhisingGuard).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PCPhisingGuard
            Return CType(m_PCPhisingGuardMapper.Retrieve(ID), PCPhisingGuard)
        End Function

        Public Function Retrieve(ByVal Code As String) As PCPhisingGuard
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PCPhisingGuard), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PCPhisingGuard), "PCPhisingGuardCode", MatchType.Exact, Code))

            Dim PCPhisingGuardColl As ArrayList = m_PCPhisingGuardMapper.RetrieveByCriteria(criterias)
            If (PCPhisingGuardColl.Count > 0) Then
                Return CType(PCPhisingGuardColl(0), PCPhisingGuard)
            End If
            Return New PCPhisingGuard
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PCPhisingGuardMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PCPhisingGuardMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PCPhisingGuard), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_PCPhisingGuardMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PCPhisingGuardMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PCPhisingGuard), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PCPhisingGuardMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PCPhisingGuard), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PCPhisingGuardMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PCPhisingGuard), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PCPhisingGuard As ArrayList = m_PCPhisingGuardMapper.RetrieveByCriteria(criterias)
            Return _PCPhisingGuard
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PCPhisingGuard), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PCPhisingGuardColl As ArrayList = m_PCPhisingGuardMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PCPhisingGuardColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PCPhisingGuardColl As ArrayList = m_PCPhisingGuardMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PCPhisingGuardColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PCPhisingGuard), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PCPhisingGuardColl As ArrayList = m_PCPhisingGuardMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PCPhisingGuard), columnName, matchOperator, columnValue))
            Return PCPhisingGuardColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PCPhisingGuard), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PCPhisingGuard), columnName, matchOperator, columnValue))

            Return m_PCPhisingGuardMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As PCPhisingGuard) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_PCPhisingGuardMapper.Insert(objDomain, "System")
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Sub Update(ByVal objDomain As PCPhisingGuard)
            Try
                m_PCPhisingGuardMapper.Update(objDomain, "System")
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Update(ByVal ArrList As ArrayList)
            Try
                For Each objDomain As PCPhisingGuard In ArrList
                    m_PCPhisingGuardMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                Next
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Delete(ByVal objDomain As PCPhisingGuard)
            Try
                m_PCPhisingGuardMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PCPhisingGuard), "PCPhisingGuardCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PCPhisingGuard), "PCPhisingGuardCode", AggregateType.Count)
            Return CType(m_PCPhisingGuardMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace