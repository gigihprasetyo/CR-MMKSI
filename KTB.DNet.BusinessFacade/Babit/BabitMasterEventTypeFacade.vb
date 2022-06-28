
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
'// Generated on 29/05/2019 - 14:35:22
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

    Public Class BabitMasterEventTypeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitMasterEventTypeMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitMasterEventTypeMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitMasterEventType).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitMasterEventType
            Return CType(m_BabitMasterEventTypeMapper.Retrieve(ID), BabitMasterEventType)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitMasterEventType
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "Status", MatchType.Exact, 1))
            criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "TypeCode", MatchType.Exact, Code))

            Dim BabitMasterEventTypeColl As ArrayList = m_BabitMasterEventTypeMapper.RetrieveByCriteria(criterias)
            If (BabitMasterEventTypeColl.Count > 0) Then
                Return CType(BabitMasterEventTypeColl(0), BabitMasterEventType)
            End If
            Return New BabitMasterEventType
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitMasterEventTypeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitMasterEventTypeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitMasterEventTypeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitMasterEventType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitMasterEventTypeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitMasterEventType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitMasterEventTypeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitMasterEventType As ArrayList = m_BabitMasterEventTypeMapper.RetrieveByCriteria(criterias)
            Return _BabitMasterEventType
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitMasterEventTypeColl As ArrayList = m_BabitMasterEventTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitMasterEventTypeColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitMasterEventType), SortColumn, sortDirection))
            Dim BabitMasterEventTypeColl As ArrayList = m_BabitMasterEventTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitMasterEventTypeColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitMasterEventTypeColl As ArrayList = m_BabitMasterEventTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitMasterEventTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitMasterEventTypeColl As ArrayList = m_BabitMasterEventTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitMasterEventType), columnName, matchOperator, columnValue))
            Return BabitMasterEventTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitMasterEventType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), columnName, matchOperator, columnValue))

            Return m_BabitMasterEventTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "TypeCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitMasterEventType), "TypeCode", AggregateType.Count)
            Return CType(m_BabitMasterEventTypeMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitMasterEventType) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitMasterEventTypeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitMasterEventType) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitMasterEventTypeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitMasterEventType)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitMasterEventTypeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitMasterEventType)
            Try
                m_BabitMasterEventTypeMapper.Delete(objDomain)
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

