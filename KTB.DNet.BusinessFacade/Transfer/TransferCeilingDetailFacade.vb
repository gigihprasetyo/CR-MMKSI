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

Namespace KTB.DNet.BusinessFacade.Transfer
    Public Class TransferCeilingDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_TransferCeilingDetailMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_TransferCeilingDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(TransferCeilingDetail).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TransferCeilingDetail
            Return CType(m_TransferCeilingDetailMapper.Retrieve(ID), TransferCeilingDetail)
        End Function

        Public Function Retrieve(ByVal TransferCeilingDetailCode As String) As TransferCeilingDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeilingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TransferCeilingDetail), "TransferCeilingDetailCode", MatchType.Exact, TransferCeilingDetailCode))

            Dim TransferCeilingDetailColl As ArrayList = m_TransferCeilingDetailMapper.RetrieveByCriteria(criterias)
            If (TransferCeilingDetailColl.Count > 0) Then
                Return CType(TransferCeilingDetailColl(0), TransferCeilingDetail)
            End If
            Return New TransferCeilingDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TransferCeilingDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TransferCeilingDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TransferCeilingDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferCeilingDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferCeilingDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferCeilingDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferCeilingDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeilingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("TransferCeilingDetailCode")) Then
                sortColl.Add(New Sort(GetType(TransferCeilingDetail), "TransferCeilingDetailCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _TransferCeilingDetail As ArrayList = m_TransferCeilingDetailMapper.RetrieveByCriteria(criterias, sortColl)
            Return _TransferCeilingDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeilingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TransferCeilingDetailColl As ArrayList = m_TransferCeilingDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferCeilingDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TransferCeilingDetailColl As ArrayList = m_TransferCeilingDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferCeilingDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeilingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TransferCeilingDetail), columnName, matchOperator, columnValue))
            Dim TransferCeilingDetailColl As ArrayList = m_TransferCeilingDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferCeilingDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferCeilingDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeilingDetail), columnName, matchOperator, columnValue))

            Return m_TransferCeilingDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeilingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferCeilingDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferCeilingDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As TransferCeilingDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TransferCeilingDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TransferCeilingDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TransferCeilingDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TransferCeilingDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TransferCeilingDetailMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TransferCeilingDetail)
            Try
                m_TransferCeilingDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeilingDetail), "TransferCeilingDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TransferCeilingDetail), "TransferCeilingDetailCode", AggregateType.Count)

            Return CType(m_TransferCeilingDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function


        Public Function ExecuteSP(ByVal SQL As String) As Boolean
            'If SQL.IndexOf("exec ") < 0 Then
            '    SQL = SQL.Replace("'", "''")
            '    SQL = "exec SPHelper '" & SQL & "'"
            'End If
            Return Me.m_TransferCeilingDetailMapper.ExecuteSP(SQL)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TransferCeilingDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferCeilingDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TransferCeilingDetailColl As ArrayList = m_TransferCeilingDetailMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TransferCeilingDetailColl
        End Function

#End Region

    End Class

End Namespace
