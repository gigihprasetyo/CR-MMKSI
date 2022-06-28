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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class ReplecementPartMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_ReplecementPartMasterMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ReplecementPartMasterMapper = MapperFactory.GetInstance().GetMapper(GetType(ReplecementPartMaster).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ReplecementPartMaster
            Return CType(m_ReplecementPartMasterMapper.Retrieve(ID), ReplecementPartMaster)
        End Function

        Public Function Retrieve(ByVal Code As String) As ReplecementPartMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ReplecementPartMaster), "Code", MatchType.Exact, Code))
            Dim ReplecementPartMasterColl As ArrayList = m_ReplecementPartMasterMapper.RetrieveByCriteria(criterias)
            If (ReplecementPartMasterColl.Count > 0) Then
                Return CType(ReplecementPartMasterColl(0), ReplecementPartMaster)
            End If
            Return New ReplecementPartMaster
        End Function

        Public Function IsReplecementPartMasterFound(ByVal strReplecementPartMasterCode As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ReplecementPartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            criterias.opAnd(New Criteria(GetType(ReplecementPartMaster), "ReplecementPartMasterCode", MatchType.Exact, strReplecementPartMasterCode))
            Dim ReplecementPartMasterColl As ArrayList = m_ReplecementPartMasterMapper.RetrieveByCriteria(criterias)
            If (ReplecementPartMasterColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ReplecementPartMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ReplecementPartMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ReplecementPartMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ReplecementPartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ReplecementPartMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ReplecementPartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ReplecementPartMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ReplecementPartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ReplecementPartMaster As ArrayList = m_ReplecementPartMasterMapper.RetrieveByCriteria(criterias)
            Return _ReplecementPartMaster
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ReplecementPartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ReplecementPartMasterColl As ArrayList = m_ReplecementPartMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ReplecementPartMasterColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ReplecementPartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ReplecementPartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ReplecementPartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ReplecementPartMasterColl As ArrayList = m_ReplecementPartMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ReplecementPartMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ReplecementPartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ReplecementPartMaster), columnName, matchOperator, columnValue))
            Dim ReplecementPartMasterColl As ArrayList = m_ReplecementPartMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ReplecementPartMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ReplecementPartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ReplecementPartMaster), columnName, matchOperator, columnValue))

            Return m_ReplecementPartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As ReplecementPartMaster) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ReplecementPartMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ReplecementPartMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ReplecementPartMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As ReplecementPartMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ReplecementPartMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As ReplecementPartMaster) As Integer
            Dim nResult As Integer = 1
            Try
                m_ReplecementPartMasterMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ReplecementPartMaster), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ReplecementPartMaster), "Code", AggregateType.Count)

            Return CType(m_ReplecementPartMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


