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

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class ProductCategoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_ProductCategoryMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_ProductCategoryMapper = MapperFactory.GetInstance().GetMapper(GetType(ProductCategory).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ProductCategory
            Return CType(m_ProductCategoryMapper.Retrieve(ID), ProductCategory)
        End Function

        Public Function Retrieve(ByVal Code As String) As ProductCategory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProductCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ProductCategory), "Code", MatchType.Exact, Code))

            Dim ProductCategoryColl As ArrayList = m_ProductCategoryMapper.RetrieveByCriteria(criterias)
            If (ProductCategoryColl.Count > 0) Then
                Return CType(ProductCategoryColl(0), ProductCategory)
            End If
            Return New ProductCategory
        End Function

        Public Function RetrieveByName(ByVal NameCode As String) As ProductCategory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProductCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ProductCategory), "Name", MatchType.Exact, NameCode))

            Dim ProductCategoryColl As ArrayList = m_ProductCategoryMapper.RetrieveByCriteria(criterias)
            If (ProductCategoryColl.Count > 0) Then
                Return CType(ProductCategoryColl(0), ProductCategory)
            End If
            Return New ProductCategory
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ProductCategoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ProductCategoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ProductCategoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ProductCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProductCategoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ProductCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProductCategoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(Optional ByVal companyCode As String = "") As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProductCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If companyCode.Trim <> "" Then
                criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, companyCode))
            End If
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("Code")) Then
                sortColl.Add(New Sort(GetType(ProductCategory), "Code", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _ProductCategory As ArrayList = m_ProductCategoryMapper.RetrieveByCriteria(criterias, sortColl)
            Return _ProductCategory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, Optional ByVal companyCode As String = "") As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProductCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If companyCode.Trim <> "" Then
                criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, companyCode))
            End If
            Dim ProductCategoryColl As ArrayList = m_ProductCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ProductCategoryColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ProductCategoryColl As ArrayList = m_ProductCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ProductCategoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, Optional ByVal companyCode As String = "") As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProductCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If companyCode.Trim <> "" Then
                criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, companyCode))
            End If

            criterias.opAnd(New Criteria(GetType(ProductCategory), columnName, matchOperator, columnValue))
            Dim ProductCategoryColl As ArrayList = m_ProductCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ProductCategoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ProductCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProductCategory), columnName, matchOperator, columnValue))

            Return m_ProductCategoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, Optional ByVal companyCode As String = "") As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProductCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If companyCode.Trim <> "" Then
                criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, companyCode))
            End If

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ProductCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProductCategoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As ProductCategory) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_ProductCategoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ProductCategory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ProductCategoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As ProductCategory)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ProductCategoryMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As ProductCategory)
            Try
                m_ProductCategoryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProductCategory), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ProductCategory), "Code", AggregateType.Count)

            Return CType(m_ProductCategoryMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ProductCategoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ProductCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ProductCategoryColl As ArrayList = m_ProductCategoryMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ProductCategoryColl
        End Function

#End Region

    End Class

End Namespace
