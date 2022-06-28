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
'// Generated on 8/20/2007 - 1:34:24 PM
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

Namespace KTB.DNet.BusinessFacade.Event

    Public Class EventMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EventMasterMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EventMasterMapper = MapperFactory.GetInstance.GetMapper(GetType(EventMaster).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As EventMaster
            Return CType(m_EventMasterMapper.Retrieve(ID), EventMaster)
        End Function

        Public Function Retrieve(ByVal Code As String) As EventMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventMaster), "EventNo", MatchType.Exact, Code))

            Dim EventMasterColl As ArrayList = m_EventMasterMapper.RetrieveByCriteria(criterias)
            If (EventMasterColl.Count > 0) Then
                Return CType(EventMasterColl(0), EventMaster)
            End If
            Return New EventMaster
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EventMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EventMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EventMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EventMaster As ArrayList = m_EventMasterMapper.RetrieveByCriteria(criterias)
            Return _EventMaster
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(EventMaster), SortColumn, sortDirection))

            Dim EventMasterColl As ArrayList = m_EventMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return EventMasterColl
        End Function

        'Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Dim EventMasterColl As ArrayList = m_EventMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

        '    Return EventMasterColl
        'End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EventMasterColl As ArrayList = m_EventMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EventMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(EventMaster), SortColumn, sortDirection))

            Dim EventMasterColl As ArrayList = m_EventMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return EventMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventMasterColl As ArrayList = m_EventMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EventMaster), columnName, matchOperator, columnValue))
            Return EventMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventMaster), columnName, matchOperator, columnValue))

            Return m_EventMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Period As Integer, ByVal StartMonth As Integer, ByVal EndMonth As Integer, ByVal IdEdit As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventMaster), "Period", MatchType.Exact, Period))
            crit.opAnd(New Criteria(GetType(EventMaster), "StartMonth", MatchType.Exact, StartMonth))
            crit.opAnd(New Criteria(GetType(EventMaster), "EndMonth", MatchType.Exact, EndMonth))
            If IdEdit <> 0 Then
                crit.opAnd(New Criteria(GetType(EventMaster), "ID", MatchType.No, IdEdit))
            End If
            Dim agg As Aggregate = New Aggregate(GetType(EventMaster), "Period", AggregateType.Count)
            Return CType(m_EventMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateCode(ByVal Period As Integer, ByVal StartMonth As Integer, ByVal EndMonth As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventMaster), "Period", MatchType.Exact, Period))
            crit.opAnd(New Criteria(GetType(EventMaster), "StartMonth", MatchType.Exact, StartMonth))
            crit.opAnd(New Criteria(GetType(EventMaster), "EndMonth", MatchType.Exact, EndMonth))
            Dim agg As Aggregate = New Aggregate(GetType(EventMaster), "Period", AggregateType.Count)
            Return CType(m_EventMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
        Public Function Insert(ByVal objDomain As EventMaster) As Integer
            Dim iReturn As Integer = -2
            Try
                '--modify by ronny
                iReturn = m_EventMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                '---end modify
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub DeleteFromDB(ByVal objDomain As EventMaster)
            Try
                m_EventMasterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Update(ByVal objDomain As EventMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_EventMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
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

    End Class

End Namespace

