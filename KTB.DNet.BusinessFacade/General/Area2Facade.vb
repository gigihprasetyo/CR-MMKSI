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

Namespace KTB.DNet.BusinessFacade.General
    Public Class Area2Facade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_Area2Mapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_Area2Mapper = MapperFactory.GetInstance().GetMapper(GetType(Area2).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As Area2
            Return CType(m_Area2Mapper.Retrieve(ID), Area2)
        End Function

        Public Function Retrieve(ByVal Code As String) As Area2
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Area2), "AreaCode", MatchType.Exact, Code))

            Dim Area2Coll As ArrayList = m_Area2Mapper.RetrieveByCriteria(criterias)
            If (Area2Coll.Count > 0) Then
                Return CType(Area2Coll(0), Area2)
            End If
            Return New Area2
        End Function

        Public Function IsArea2Found(ByVal strArea2Code As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            criterias.opAnd(New Criteria(GetType(Area2), "Area2Code", MatchType.Exact, strArea2Code))
            Dim Area2Coll As ArrayList = m_Area2Mapper.RetrieveByCriteria(criterias)
            If (Area2Coll.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_Area2Mapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_Area2Mapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_Area2Mapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Area2), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_Area2Mapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Area2), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_Area2Mapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        'Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        '  ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
        '      Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '      Dim sortColl As SortCollection = New SortCollection

        '      If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
        '          sortColl.Add(New Sort(GetType(Area2), sortColumn, sortDirection))
        '      Else
        '          sortColl = Nothing
        '      End If

        '      Return m_Area2Mapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        '  End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Area2 As ArrayList = m_Area2Mapper.RetrieveByCriteria(criterias)
            Return _Area2
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Area2), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_Area2Mapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim Area2Coll As ArrayList = m_Area2Mapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return Area2Coll
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Area2), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim Area2Coll As ArrayList = m_Area2Mapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return Area2Coll
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Area2), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_Area2Mapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(Area2), SortColumn, sortDirection))
            Dim Area2Coll As ArrayList = m_Area2Mapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return Area2Coll
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim Area2Coll As ArrayList = m_Area2Mapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return Area2Coll
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Area2), columnName, matchOperator, columnValue))
            Dim Area2Coll As ArrayList = m_Area2Mapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return Area2Coll
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Area2), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), columnName, matchOperator, columnValue))

            Return m_Area2Mapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

       
        Public Function Insert(ByVal objDomain As Area2) As Integer
            Dim iReturn As Integer = -2
            Try
                m_Area2Mapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As Area2) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_Area2Mapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As Area2) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_Area2Mapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As Area2) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_Area2Mapper.Delete(objDomain)
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
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "AreaCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(Area2), "AreaCode", AggregateType.Count)

            Return CType(m_Area2Mapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

