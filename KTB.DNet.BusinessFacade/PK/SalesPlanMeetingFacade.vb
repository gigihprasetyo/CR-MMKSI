
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
'// Copyright  2015
'// ---------------------
'// $History      : $
'// Generated on 11/19/2015 - 11:20:55 AM
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

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.DataMapper.Framework

#End Region

Namespace KTB.DNet.BusinessFacade

    Public Class SalesPlanMeetingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SalesPlanMeetingMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SalesPlanMeetingMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesPlanMeeting).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SalesPlanMeeting
            Return CType(m_SalesPlanMeetingMapper.Retrieve(ID), SalesPlanMeeting)
        End Function

        Public Function Retrieve(ByVal Code As String) As SalesPlanMeeting
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesPlanMeeting), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesPlanMeeting), "SalesPlanMeetingCode", MatchType.Exact, Code))

            Dim SalesPlanMeetingColl As ArrayList = m_SalesPlanMeetingMapper.RetrieveByCriteria(criterias)
            If (SalesPlanMeetingColl.Count > 0) Then
                Return CType(SalesPlanMeetingColl(0), SalesPlanMeeting)
            End If
            Return New SalesPlanMeeting
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesPlanMeetingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SalesPlanMeetingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesPlanMeeting), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_SalesPlanMeetingMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SalesPlanMeetingMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesPlanMeeting), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesPlanMeetingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesPlanMeeting), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesPlanMeetingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesPlanMeeting), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SalesPlanMeeting As ArrayList = m_SalesPlanMeetingMapper.RetrieveByCriteria(criterias)
            Return _SalesPlanMeeting
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesPlanMeeting), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesPlanMeetingColl As ArrayList = m_SalesPlanMeetingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesPlanMeetingColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SalesPlanMeetingColl As ArrayList = m_SalesPlanMeetingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SalesPlanMeetingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesPlanMeeting), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesPlanMeetingColl As ArrayList = m_SalesPlanMeetingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SalesPlanMeeting), columnName, matchOperator, columnValue))
            Return SalesPlanMeetingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesPlanMeeting), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesPlanMeeting), columnName, matchOperator, columnValue))

            Return m_SalesPlanMeetingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesPlanMeeting), "SalesPlanMeetingCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SalesPlanMeeting), "SalesPlanMeetingCode", AggregateType.Count)
            Return CType(m_SalesPlanMeetingMapper.RetrieveScalar(agg, crit), Integer)
        End Function
        Public Function Insert(ByVal objDomain As SalesPlanMeeting) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SalesPlanMeetingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SalesPlanMeeting) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SalesPlanMeetingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SalesPlanMeeting)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_SalesPlanMeetingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SalesPlanMeeting)
            Try
                m_SalesPlanMeetingMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

