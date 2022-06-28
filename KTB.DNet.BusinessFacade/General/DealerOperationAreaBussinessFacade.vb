'Public Class DealerOperationAreaBussinessFacade

'End Class



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
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 26/05/2020 - 23:29:16
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
Imports System.Collections.Generic
Imports System.Linq

#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class DealerOperationAreaBussinessFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DealerOperationAreaBussinessMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DealerOperationAreaBussinessMapper = MapperFactory.GetInstance.GetMapper(GetType(DealerOperationAreaBussiness).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DealerOperationAreaBussiness
            Return CType(m_DealerOperationAreaBussinessMapper.Retrieve(ID), DealerOperationAreaBussiness)
        End Function

        Public Function Retrieve(ByVal Code As String) As DealerOperationAreaBussiness
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerOperationAreaBussiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerOperationAreaBussiness), "DealerOperationAreaBussinessCode", MatchType.Exact, Code))

            Dim DealerOperationAreaBussinessColl As ArrayList = m_DealerOperationAreaBussinessMapper.RetrieveByCriteria(criterias)
            If (DealerOperationAreaBussinessColl.Count > 0) Then
                Return CType(DealerOperationAreaBussinessColl(0), DealerOperationAreaBussiness)
            End If
            Return New DealerOperationAreaBussiness
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DealerOperationAreaBussinessMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DealerOperationAreaBussinessMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DealerOperationAreaBussinessMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerOperationAreaBussiness), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerOperationAreaBussinessMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerOperationAreaBussiness), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerOperationAreaBussinessMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerOperationAreaBussiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DealerOperationAreaBussiness As ArrayList = m_DealerOperationAreaBussinessMapper.RetrieveByCriteria(criterias)
            Return _DealerOperationAreaBussiness
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerOperationAreaBussiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerOperationAreaBussinessColl As ArrayList = m_DealerOperationAreaBussinessMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DealerOperationAreaBussinessColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DealerOperationAreaBussiness), SortColumn, sortDirection))
            Dim DealerOperationAreaBussinessColl As ArrayList = m_DealerOperationAreaBussinessMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerOperationAreaBussinessColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DealerOperationAreaBussinessColl As ArrayList = m_DealerOperationAreaBussinessMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DealerOperationAreaBussinessColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerOperationAreaBussiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerOperationAreaBussinessColl As ArrayList = m_DealerOperationAreaBussinessMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DealerOperationAreaBussiness), columnName, matchOperator, columnValue))
            Return DealerOperationAreaBussinessColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerOperationAreaBussiness), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerOperationAreaBussiness), columnName, matchOperator, columnValue))

            Return m_DealerOperationAreaBussinessMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerOperationAreaBussiness), "DealerOperationAreaBussinessCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DealerOperationAreaBussiness), "DealerOperationAreaBussinessCode", AggregateType.Count)
            Return CType(m_DealerOperationAreaBussinessMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DealerOperationAreaBussiness) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DealerOperationAreaBussinessMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DealerOperationAreaBussiness) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DealerOperationAreaBussinessMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DealerOperationAreaBussiness)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DealerOperationAreaBussinessMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DealerOperationAreaBussiness)
            Try
                m_DealerOperationAreaBussinessMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrievebyDealerID(ByVal ID As Integer) As List(Of DealerOperationAreaBussiness)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerOperationAreaBussiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerOperationAreaBussiness), "DealerID", MatchType.Exact, ID))

            Dim DealerOperationAreaBussinessColl As ArrayList = m_DealerOperationAreaBussinessMapper.RetrieveByCriteria(criterias)
            If (DealerOperationAreaBussinessColl.Count > 0) Then
                Return DealerOperationAreaBussinessColl.Cast(Of DealerOperationAreaBussiness).ToList()
            End If
            Return New List(Of DealerOperationAreaBussiness)
        End Function

#End Region

    End Class

End Namespace
