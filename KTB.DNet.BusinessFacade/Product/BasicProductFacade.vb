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

Namespace KTB.DNet.BusinessFacade.Product
    Public Class BasicProductFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_BasicProductMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_BasicProductMapper = MapperFactory.GetInstance().GetMapper(GetType(BasicProduct).ToString)
            Me.objTransactionManager = New TransactionManager

            Me.DomainTypeCollection.Add(GetType(BasicProduct))
        End Sub

#End Region

#Region "Private Method"

        Private Function GenerateCancelCriterias(ByVal arrItems As ArrayList) As CriteriaComposite
            Dim criterias As CriteriaComposite = Nothing

            For i As Integer = 0 To arrItems.Count
                If criterias Is Nothing Then
                    criterias = New CriteriaComposite(New Criteria(GetType(BasicProduct), "ID", MatchType.Exact, CType(arrItems(i), Integer)))
                Else
                    criterias.opOr(New Criteria(GetType(BasicProduct), "ID", MatchType.Exact, CType(arrItems(i), Integer)))
                End If
            Next
            Return criterias
        End Function

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BasicProduct
            Return CType(m_BasicProductMapper.Retrieve(ID), BasicProduct)
        End Function

        Public Function Retrieve(ByVal Code As String) As BasicProduct
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BasicProduct), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BasicProduct), "BasicProductCode", MatchType.Exact, Code))

            Dim BasicProductColl As ArrayList = m_BasicProductMapper.RetrieveByCriteria(criterias)
            If (BasicProductColl.Count > 0) Then
                Return CType(BasicProductColl(0), BasicProduct)
            End If
            Return New BasicProduct
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BasicProductMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BasicProductMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BasicProductMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BasicProduct), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BasicProductMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BasicProduct), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BasicProductMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BasicProduct), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BasicProduct As ArrayList = m_BasicProductMapper.RetrieveByCriteria(criterias)
            Return _BasicProduct
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BasicProduct), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BasicProductColl As ArrayList = m_BasicProductMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BasicProductColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BasicProductColl As ArrayList = m_BasicProductMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BasicProductColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BasicProduct), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BasicProduct), columnName, matchOperator, columnValue))
            Dim BasicProductColl As ArrayList = m_BasicProductMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BasicProductColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BasicProduct), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BasicProduct), columnName, matchOperator, columnValue))

            Return m_BasicProductMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub Insert(ByVal objDomain As BasicProduct)
            Dim iReturn As Integer = -2
            Try
                m_BasicProductMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try

        End Sub

        Public Sub Update(ByVal objDomain As BasicProduct)
            Try
                m_BasicProductMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Delete(ByVal objDomain As BasicProduct)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BasicProductMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BasicProduct)
            Try
                m_BasicProductMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BasicProduct), "BasicProductCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BasicProduct), "BasicProductCode", AggregateType.Count)

            Return CType(m_BasicProductMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace