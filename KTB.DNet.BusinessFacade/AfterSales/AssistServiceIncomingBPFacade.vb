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
'// Copyright  2021
'// ---------------------
'// $History      : $
'// Generated on 3/24/2021 - 10:25:27 AM
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
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class AssistServiceIncomingBPFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AssistServiceIncomingBPMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_AssistServiceIncomingBPMapper = MapperFactory.GetInstance.GetMapper(GetType(AssistServiceIncomingBP).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AssistServiceIncomingBP
            Return CType(m_AssistServiceIncomingBPMapper.Retrieve(ID), AssistServiceIncomingBP)
        End Function

        Public Function Retrieve(ByVal Code As String) As AssistServiceIncomingBP
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistServiceIncomingBP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AssistServiceIncomingBP), "AssistServiceIncomingBPCode", MatchType.Exact, Code))

            Dim AssistServiceIncomingBPColl As ArrayList = m_AssistServiceIncomingBPMapper.RetrieveByCriteria(criterias)
            If (AssistServiceIncomingBPColl.Count > 0) Then
                Return CType(AssistServiceIncomingBPColl(0), AssistServiceIncomingBP)
            End If
            Return New AssistServiceIncomingBP
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AssistServiceIncomingBPMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AssistServiceIncomingBPMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AssistServiceIncomingBPMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistServiceIncomingBP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AssistServiceIncomingBPMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistServiceIncomingBP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AssistServiceIncomingBPMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistServiceIncomingBP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AssistServiceIncomingBP As ArrayList = m_AssistServiceIncomingBPMapper.RetrieveByCriteria(criterias)
            Return _AssistServiceIncomingBP
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistServiceIncomingBP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AssistServiceIncomingBPColl As ArrayList = m_AssistServiceIncomingBPMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AssistServiceIncomingBPColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(AssistServiceIncomingBP), SortColumn, sortDirection))
            Dim AssistServiceIncomingBPColl As ArrayList = m_AssistServiceIncomingBPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AssistServiceIncomingBPColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistServiceIncomingBP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim arrResult As ArrayList = m_AssistServiceIncomingBPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return arrResult
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AssistServiceIncomingBPColl As ArrayList = m_AssistServiceIncomingBPMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return AssistServiceIncomingBPColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistServiceIncomingBP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AssistServiceIncomingBPColl As ArrayList = m_AssistServiceIncomingBPMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AssistServiceIncomingBP), columnName, matchOperator, columnValue))
            Return AssistServiceIncomingBPColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AssistServiceIncomingBP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistServiceIncomingBP), columnName, matchOperator, columnValue))

            Return m_AssistServiceIncomingBPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AssistServiceIncomingBP), "AssistServiceIncomingBPCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(AssistServiceIncomingBP), "AssistServiceIncomingBPCode", AggregateType.Count)
            Return CType(m_AssistServiceIncomingBPMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As AssistServiceIncomingBP) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_AssistServiceIncomingBPMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As AssistServiceIncomingBP) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AssistServiceIncomingBPMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As AssistServiceIncomingBP)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_AssistServiceIncomingBPMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As AssistServiceIncomingBP)
            Try
                m_AssistServiceIncomingBPMapper.Delete(objDomain)
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
