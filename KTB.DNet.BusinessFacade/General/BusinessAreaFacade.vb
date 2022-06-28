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
    Public Class BusinessAreaFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_BusinessAreaMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_BusinessAreaMapper = MapperFactory.GetInstance().GetMapper(GetType(BusinessArea).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BusinessArea
            Return CType(m_BusinessAreaMapper.Retrieve(ID), BusinessArea)
        End Function

        Public Function Retrieve(ByVal Code As String) As BusinessArea
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BusinessArea), "BusinessAreaCode", MatchType.Exact, Code))

            Dim BusinessAreaColl As ArrayList = m_BusinessAreaMapper.RetrieveByCriteria(criterias)
            If (BusinessAreaColl.Count > 0) Then
                Return CType(BusinessAreaColl(0), BusinessArea)
            End If
            Return New BusinessArea
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BusinessAreaMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BusinessAreaMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BusinessAreaMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BusinessArea), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BusinessAreaMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BusinessArea), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BusinessAreaMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BusinessArea As ArrayList = m_BusinessAreaMapper.RetrieveByCriteria(criterias)
            Return _BusinessArea
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BusinessAreaColl As ArrayList = m_BusinessAreaMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BusinessAreaColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BusinessAreaColl As ArrayList = m_BusinessAreaMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BusinessAreaColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BusinessArea), columnName, matchOperator, columnValue))
            Dim BusinessAreaColl As ArrayList = m_BusinessAreaMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BusinessAreaColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BusinessArea), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessArea), columnName, matchOperator, columnValue))

            Return m_BusinessAreaMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As BusinessArea) As Integer
            Dim iReturn As Integer = -2
            Try
                m_BusinessAreaMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function InsertFromWS(ByVal objDomain As BusinessArea) As Integer
            Dim iReturn As Integer = -2
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessArea), "Dealer.ID", MatchType.Exact, objDomain.Dealer.ID))
                criterias.opAnd(New Criteria(GetType(BusinessArea), "Kind", MatchType.Exact, objDomain.Kind))
                criterias.opAnd(New Criteria(GetType(BusinessArea), "Position", MatchType.Exact, objDomain.Position))

                Dim BusinessAreaColl As ArrayList = m_BusinessAreaMapper.RetrieveByCriteria(criterias)
                If (BusinessAreaColl.Count > 0) Then
                    objDomain.ID = CType(BusinessAreaColl(0), BusinessArea).ID
                    iReturn = m_BusinessAreaMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                Else
                    iReturn = m_BusinessAreaMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
                End If

            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function


        Public Function Update(ByVal objDomain As BusinessArea) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BusinessAreaMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BusinessArea)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BusinessAreaMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BusinessArea)
            Try
                m_BusinessAreaMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessArea), "BusinessAreaCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BusinessArea), "BusinessAreaCode", AggregateType.Count)

            Return CType(m_BusinessAreaMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

