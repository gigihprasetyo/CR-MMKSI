
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
'// Author Name   : Agus Pirnadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright � 2005 
'// ---------------------
'// $History      : $
'// Generated on 29/9/2005 - 10:53:00 AM
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
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class SPLDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_SPLDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SPLDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(SPLDetail).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SPLDetail
            Return CType(m_SPLDetailMapper.Retrieve(ID), SPLDetail)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPLDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPLDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SPLDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPLDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPLDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPLDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPLDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SPLDetail As ArrayList = m_SPLDetailMapper.RetrieveByCriteria(criterias)
            Return _SPLDetail
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPLDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SPLColl As ArrayList = m_SPLDetailMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPLColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPLDetailColl As ArrayList = m_SPLDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPLDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SPLDetailColl As ArrayList = m_SPLDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPLDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPLDetail), columnName, matchOperator, columnValue))
            Dim SPLDetailColl As ArrayList = m_SPLDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPLDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPLDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), columnName, matchOperator, columnValue))

            Return m_SPLDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As SPLDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SPLDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SPLDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPLDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SPLDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SPLDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SPLDetail)
            Try
                m_SPLDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal nTypeID As Integer, ByVal dValidFrom As DateTime) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "VechileColor.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(SPLDetail), "ValidFrom", MatchType.Exact, dValidFrom))

            Dim agg As Aggregate = New Aggregate(GetType(SPLDetail), "ValidFrom", AggregateType.Count)

            Return CType(m_SPLDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Return m_SPLDetailMapper.RetrieveByCriteria(criterias, sorts, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPLDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function GetSPLDataToExtend() As List(Of SPLDetail)
            Dim result As New List(Of SPLDetail)

            Dim dt As ArrayList = m_SPLDetailMapper.RetrieveSP("up_GetSPLDataToExtend")
            For Each drWLog As SPLDetail In dt
                result.Add(drWLog)
            Next

            Return result
        End Function

#End Region

    End Class
End Namespace
