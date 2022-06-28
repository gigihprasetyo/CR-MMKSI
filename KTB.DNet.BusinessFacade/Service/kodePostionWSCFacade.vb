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
    Public Class KodePostionWSCFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_KodePostionWSCMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager



#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_KodePostionWSCMapper = MapperFactory.GetInstance().GetMapper(GetType(KodePostionWSC).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As KodePostionWSC
            Return CType(m_KodePostionWSCMapper.Retrieve(ID), KodePostionWSC)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_KodePostionWSCMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_KodePostionWSCMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_KodePostionWSCMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KodePostionWSC), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_KodePostionWSCMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KodePostionWSC), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_KodePostionWSCMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _KodePostionWSC As ArrayList = m_KodePostionWSCMapper.RetrieveByCriteria(criterias)
            Return _KodePostionWSC
        End Function

        

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim KodePostionWSCColl As ArrayList = m_KodePostionWSCMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return KodePostionWSCColl
        End Function

        Public Function RetrieveActiveList(ByVal criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim KodePostionWSCColl As ArrayList = m_KodePostionWSCMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return KodePostionWSCColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim KodePostionWSCColl As ArrayList = m_KodePostionWSCMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return KodePostionWSCColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KodePostionWSC), columnName, matchOperator, columnValue))
            Dim KodePostionWSCColl As ArrayList = m_KodePostionWSCMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return KodePostionWSCColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KodePostionWSC), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KodePostionWSC), columnName, matchOperator, columnValue))

            Return m_KodePostionWSCMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KodePostionWSC), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_KodePostionWSCMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveActiveList(ByVal criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KodePostionWSC), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_KodePostionWSCMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_KodePostionWSCMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As KodePostionWSC) As Integer
            Dim iReturn As Integer = -2
            Try
                m_KodePostionWSCMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As KodePostionWSC) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_KodePostionWSCMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
            End Try
            Return nResult
        End Function




        Public Sub Delete(ByVal objDomain As KodePostionWSC)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_KodePostionWSCMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As KodePostionWSC) As Integer
            Dim count As Integer = 0
            Try
                count = m_KodePostionWSCMapper.Delete(objDomain)
            Catch ex As Exception
                count = 0
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
            End Try
            Return count
        End Function

        Public Function ValidateCode(ByVal sCatagory As String, ByVal sCode As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "PositionCode", MatchType.Exact, sCode))
            crit.opAnd(New Criteria(GetType(KodePostionWSC), "PositionCategory", MatchType.Exact, sCatagory))
            Dim agg As Aggregate = New Aggregate(GetType(KodePostionWSC), "PositionCode", AggregateType.Count)

            Dim count As Integer = -1
            Try
                count = CType(m_KodePostionWSCMapper.RetrieveScalar(agg, crit), Integer)
            Catch ex As Exception
                count = -1
            End Try
            Return count
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


