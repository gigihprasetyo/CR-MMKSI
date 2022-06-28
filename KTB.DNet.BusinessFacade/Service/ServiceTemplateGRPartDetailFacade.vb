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
'// Generated on 12/1/2021 - 5:39:39 PM
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

    Public Class ServiceTemplateGRPartDetailFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ServiceTemplateGRPartDetailMapper As IMapper


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ServiceTemplateGRPartDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(ServiceTemplateGRPartDetail).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ServiceTemplateGRPartDetail
            Return CType(m_ServiceTemplateGRPartDetailMapper.Retrieve(ID), ServiceTemplateGRPartDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As ServiceTemplateGRPartDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceTemplateGRPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ServiceTemplateGRPartDetail), "ServiceTemplateGRPartDetailCode", MatchType.Exact, Code))

            Dim ServiceTemplateGRPartDetailColl As ArrayList = m_ServiceTemplateGRPartDetailMapper.RetrieveByCriteria(criterias)
            If (ServiceTemplateGRPartDetailColl.Count > 0) Then
                Return CType(ServiceTemplateGRPartDetailColl(0), ServiceTemplateGRPartDetail)
            End If
            Return New ServiceTemplateGRPartDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ServiceTemplateGRPartDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ServiceTemplateGRPartDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ServiceTemplateGRPartDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceTemplateGRPartDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceTemplateGRPartDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceTemplateGRPartDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceTemplateGRPartDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceTemplateGRPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ServiceTemplateGRPartDetail As ArrayList = m_ServiceTemplateGRPartDetailMapper.RetrieveByCriteria(criterias)
            Return _ServiceTemplateGRPartDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceTemplateGRPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ServiceTemplateGRPartDetailColl As ArrayList = m_ServiceTemplateGRPartDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ServiceTemplateGRPartDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(ServiceTemplateGRPartDetail), SortColumn, sortDirection))
            Dim ServiceTemplateGRPartDetailColl As ArrayList = m_ServiceTemplateGRPartDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ServiceTemplateGRPartDetailColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ServiceTemplateGRPartDetailColl As ArrayList = m_ServiceTemplateGRPartDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ServiceTemplateGRPartDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceTemplateGRPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ServiceTemplateGRPartDetailColl As ArrayList = m_ServiceTemplateGRPartDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ServiceTemplateGRPartDetail), columnName, matchOperator, columnValue))
            Return ServiceTemplateGRPartDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceTemplateGRPartDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceTemplateGRPartDetail), columnName, matchOperator, columnValue))

            Return m_ServiceTemplateGRPartDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceTemplateGRPartDetail), "ServiceTemplateGRPartDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ServiceTemplateGRPartDetail), "ServiceTemplateGRPartDetailCode", AggregateType.Count)
            Return CType(m_ServiceTemplateGRPartDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As ServiceTemplateGRPartDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_ServiceTemplateGRPartDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ServiceTemplateGRPartDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ServiceTemplateGRPartDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As ServiceTemplateGRPartDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ServiceTemplateGRPartDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As ServiceTemplateGRPartDetail)
            Try
                m_ServiceTemplateGRPartDetailMapper.Delete(objDomain)
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
