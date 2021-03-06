
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
'// Generated on 03/07/2018 - 2:47:17 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Collections.Generic
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

    Public Class SFSparepartHistoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SFSparepartHistoryMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SFSparepartHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(SFSparepartHistory).ToString)
            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SFSparepartHistory
            Return CType(m_SFSparepartHistoryMapper.Retrieve(ID), SFSparepartHistory)
        End Function

        Public Function Retrieve(ByVal Code As String) As SFSparepartHistory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFSparepartHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SFSparepartHistory), "KeyID", MatchType.Exact, Code))

            Dim SFSparepartHistoryColl As ArrayList = m_SFSparepartHistoryMapper.RetrieveByCriteria(criterias)
            If (SFSparepartHistoryColl.Count > 0) Then
                Return CType(SFSparepartHistoryColl(0), SFSparepartHistory)
            End If
            Return New SFSparepartHistory
        End Function

        Public Function RetrieveList(ByVal Code As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFSparepartHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SFSparepartHistory), "KeyID", MatchType.Exact, Code))

            Return m_SFSparepartHistoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SFSparepartHistoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SFSparepartHistoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SFSparepartHistoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFSparepartHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SFSparepartHistoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFSparepartHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SFSparepartHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFSparepartHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SFSparepartHistory As ArrayList = m_SFSparepartHistoryMapper.RetrieveByCriteria(criterias)
            Return _SFSparepartHistory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFSparepartHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SFSparepartHistoryColl As ArrayList = m_SFSparepartHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SFSparepartHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SFSparepartHistory), SortColumn, sortDirection))
            Dim SFSparepartHistoryColl As ArrayList = m_SFSparepartHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SFSparepartHistoryColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SFSparepartHistoryColl As ArrayList = m_SFSparepartHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SFSparepartHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFSparepartHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SFSparepartHistoryColl As ArrayList = m_SFSparepartHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SFSparepartHistory), columnName, matchOperator, columnValue))
            Return SFSparepartHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SFSparepartHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFSparepartHistory), columnName, matchOperator, columnValue))

            Return m_SFSparepartHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetieveListOfItemToSend(ByVal flag As Integer) As ArrayList
            Dim result As New ArrayList

            result = m_SFSparepartHistoryMapper.RetrieveSP(String.Format("[sp_SF_ParamSparepartHistory_Retrieve] {0}", flag))
            If Not IsNothing(result) Then
                Return result
            Else
                Return New ArrayList
            End If


        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SFSparepartHistory), "SFSparepartHistoryCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SFSparepartHistory), "SFSparepartHistoryCode", AggregateType.Count)
            Return CType(m_SFSparepartHistoryMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SFSparepartHistory) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SFSparepartHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SFSparepartHistory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SFSparepartHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SFSparepartHistory)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SFSparepartHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SFSparepartHistory)
            Try
                m_SFSparepartHistoryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function UpdateBatch(ByVal objDomainList As List(Of SFSparepartHistory)) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                For Each objDomain As SFSparepartHistory In objDomainList
                    m_TransactionManager.AddUpdate(objDomain, _user)
                Next

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = 0
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function

        Public Function RetrieveSp(str As String) As DataSet
            Return m_SFSparepartHistoryMapper.RetrieveDataSet(str)
        End Function
#End Region

    End Class

End Namespace

