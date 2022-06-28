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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 8/14/2007 - 2:31:58 PM
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

Namespace KTB.DNet.BusinessFacade.CallCenter

    Public Class CcVehicleCategoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CcVehicleCategoryFacadeMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_CcVehicleCategoryFacadeMapper = MapperFactory.GetInstance.GetMapper(GetType(CcVehicleCategory).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CcVehicleCategory
            Return CType(m_CcVehicleCategoryFacadeMapper.Retrieve(ID), CcVehicleCategory)
        End Function

        Public Function Retrieve(ByVal Code As String) As CcVehicleCategory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcVehicleCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CcVehicleCategory), "Code", MatchType.Exact, Code))

            Dim CcVehicleCategoryColl As ArrayList = m_CcVehicleCategoryFacadeMapper.RetrieveByCriteria(criterias)
            If (CcVehicleCategoryColl.Count > 0) Then
                Return CType(CcVehicleCategoryColl(0), CcVehicleCategory)
            End If
            Return New CcVehicleCategory
        End Function

        Public Function RetrieveByProductCategory(ByVal ProductCategoryID As Integer) As CcVehicleCategory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcVehicleCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CcVehicleCategory), "ProductCategory.ID", MatchType.Exact, ProductCategoryID))

            Dim CcVehicleCategoryColl As ArrayList = m_CcVehicleCategoryFacadeMapper.RetrieveByCriteria(criterias)
            If (CcVehicleCategoryColl.Count > 0) Then
                Return CType(CcVehicleCategoryColl(0), CcVehicleCategory)
            End If
            Return New CcVehicleCategory
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CcVehicleCategoryFacadeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CcVehicleCategoryFacadeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CcVehicleCategoryFacadeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcVehicleCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CcVehicleCategoryFacadeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcVehicleCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CcVehicleCategoryFacadeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(Optional ByVal companyCode As String = "") As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcVehicleCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Select Case companyCode.Trim.ToUpper
                Case "MMC"
                    criterias.opAnd(New Criteria(GetType(CcVehicleCategory), "ProductCategory.ID", MatchType.Exact, 1))
                Case "MFTBC"
                    criterias.opAnd(New Criteria(GetType(CcVehicleCategory), "ProductCategory.ID", MatchType.Exact, 2))
            End Select
            Dim _CcVehicleCategoryFacade As ArrayList = m_CcVehicleCategoryFacadeMapper.RetrieveByCriteria(criterias)
            Return _CcVehicleCategoryFacade
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcVehicleCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CcVehicleCategoryFacadeColl As ArrayList = m_CcVehicleCategoryFacadeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CcVehicleCategoryFacadeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CcVehicleCategoryFacadeColl As ArrayList = m_CcVehicleCategoryFacadeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CcVehicleCategoryFacadeColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim CcVehicleCategoryFacadeColl As ArrayList = m_CcVehicleCategoryFacadeMapper.RetrieveByCriteria(criterias)
            Return CcVehicleCategoryFacadeColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(CcVehicleCategoryFacade), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim CcVehicleCategoryFacadeColl As ArrayList = m_CcVehicleCategoryFacadeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CcVehicleCategoryFacadeColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcVehicleCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CcVehicleCategoryColl As ArrayList = m_CcVehicleCategoryFacadeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(CcVehicleCategory), columnName, matchOperator, columnValue))
            Return CcVehicleCategoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcVehicleCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcVehicleCategory), columnName, matchOperator, columnValue))

            Return m_CcVehicleCategoryFacadeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As CcVehicleCategory) As Integer
            Dim iReturn As Integer = 1
            Try
                m_CcVehicleCategoryFacadeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As CcVehicleCategory) As Integer
            Dim nResult As Integer = 1
            Try
                nResult = m_CcVehicleCategoryFacadeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace


