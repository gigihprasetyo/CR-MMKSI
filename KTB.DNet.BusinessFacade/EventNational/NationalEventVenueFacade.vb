
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 27/05/2019 - 9:53:00
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class NationalEventVenueFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_NationalEventVenueMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_NationalEventVenueMapper = MapperFactory.GetInstance.GetMapper(GetType(NationalEventVenue).ToString)
            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As NationalEventVenue
            Return CType(m_NationalEventVenueMapper.Retrieve(ID), NationalEventVenue)
        End Function

        Public Function Retrieve(ByVal Code As String) As NationalEventVenue
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventVenue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(NationalEventVenue), "VenueName", MatchType.Exact, Code))

            Dim NationalEventVenueColl As ArrayList = m_NationalEventVenueMapper.RetrieveByCriteria(criterias)
            If (NationalEventVenueColl.Count > 0) Then
                Return CType(NationalEventVenueColl(0), NationalEventVenue)
            End If
            Return New NationalEventVenue
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_NationalEventVenueMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_NationalEventVenueMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_NationalEventVenueMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(NationalEventVenue), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_NationalEventVenueMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(NationalEventVenue), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_NationalEventVenueMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventVenue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _NationalEventVenue As ArrayList = m_NationalEventVenueMapper.RetrieveByCriteria(criterias)
            Return _NationalEventVenue
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventVenue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim NationalEventVenueColl As ArrayList = m_NationalEventVenueMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return NationalEventVenueColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(NationalEventVenue), SortColumn, sortDirection))
            Dim NationalEventVenueColl As ArrayList = m_NationalEventVenueMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return NationalEventVenueColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_NationalEventVenueMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim NationalEventVenueColl As ArrayList = m_NationalEventVenueMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return NationalEventVenueColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventVenue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim NationalEventVenueColl As ArrayList = m_NationalEventVenueMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(NationalEventVenue), columnName, matchOperator, columnValue))
            Return NationalEventVenueColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(NationalEventVenue), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventVenue), columnName, matchOperator, columnValue))

            Return m_NationalEventVenueMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventVenue), "VenueName", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(NationalEventVenue), "VenueName", AggregateType.Count)
            Return CType(m_NationalEventVenueMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As NationalEventVenue) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_NationalEventVenueMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As NationalEventVenue) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_NationalEventVenueMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As NationalEventVenue)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_NationalEventVenueMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As NationalEventVenue)
            Try
                m_NationalEventVenueMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Function UpdateTransaction(ByVal arrCheckedHeader As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrCheckedHeader.Count > 0 Then
                        For Each oNationalEventVenue As NationalEventVenue In arrCheckedHeader
                            m_TransactionManager.AddUpdate(oNationalEventVenue, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function
#End Region

    End Class

End Namespace

