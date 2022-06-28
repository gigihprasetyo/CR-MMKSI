
#Region "Code Disclaimer"

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
'// Copyright  2018
'// ---------------------
'// $History      : $
'// Generated on 24/09/2018 - 13:41:40
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

    Public Class RevisionPriceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_RevisionPriceMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_RevisionPriceMapper = MapperFactory.GetInstance.GetMapper(GetType(RevisionPrice).ToString)

            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As RevisionPrice
            Return CType(m_RevisionPriceMapper.Retrieve(ID), RevisionPrice)
        End Function

        Public Function Retrieve(ByVal Code As String) As RevisionPrice
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RevisionPrice), "RevisionPriceCode", MatchType.Exact, Code))

            Dim RevisionPriceColl As ArrayList = m_RevisionPriceMapper.RetrieveByCriteria(criterias)
            If (RevisionPriceColl.Count > 0) Then
                Return CType(RevisionPriceColl(0), RevisionPrice)
            End If
            Return New RevisionPrice
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_RevisionPriceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RevisionPriceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_RevisionPriceMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionPrice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionPriceMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionPrice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RevisionPriceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _RevisionPrice As ArrayList = m_RevisionPriceMapper.RetrieveByCriteria(criterias)
            Return _RevisionPrice
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RevisionPriceColl As ArrayList = m_RevisionPriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RevisionPriceColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(RevisionPrice), SortColumn, sortDirection))
            Dim RevisionPriceColl As ArrayList = m_RevisionPriceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return RevisionPriceColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim RevisionPriceColl As ArrayList = m_RevisionPriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return RevisionPriceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RevisionPriceColl As ArrayList = m_RevisionPriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(RevisionPrice), columnName, matchOperator, columnValue))
            Return RevisionPriceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RevisionPrice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPrice), columnName, matchOperator, columnValue))

            Return m_RevisionPriceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPrice), "RevisionPriceCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(RevisionPrice), "RevisionPriceCode", AggregateType.Count)
            Return CType(m_RevisionPriceMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As RevisionPrice) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_RevisionPriceMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As RevisionPrice) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_RevisionPriceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As RevisionPrice)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_RevisionPriceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As RevisionPrice)
            Try
                m_RevisionPriceMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Private Function ValidatePrice(ByVal Price As RevisionPrice) As RevisionPrice
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.RevisionPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(KTB.DNET.Domain.RevisionPrice), "ValidFrom", MatchType.Exact, Price.ValidFrom))
            criteria.opAnd(New Criteria(GetType(KTB.DNET.Domain.RevisionPrice), "Category.CategoryCode", MatchType.Exact, Price.Category.CategoryCode))
            criteria.opAnd(New Criteria(GetType(KTB.DNET.Domain.RevisionPrice), "RevisionType.RevisionCode", MatchType.Exact, Price.RevisionType.RevisionCode))
            Dim arlPrice As ArrayList = m_RevisionPriceMapper.RetrieveByCriteria(criteria)
            If arlPrice.Count > 0 Then
                Return CType(arlPrice(0), RevisionPrice)
            End If
            Return Nothing

        End Function

        Public Function InsertFromWebSevice(ByVal Price As RevisionPrice) As Short
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim Price_old As RevisionPrice = ValidatePrice(Price)
                    If IsNothing(Price_old) Then

                        m_TransactionManager.AddInsert(Price, m_userPrincipal.Identity.Name)
                    Else
                        Price_old.Category = Price.Category
                        Price_old.RevisionType = Price.RevisionType
                        Price_old.Amount = Price.Amount
                        Price_old.ValidFrom = Price.ValidFrom
                        Price_old.LastUpdateTime = Now
                        Price_old.LastUpdateBy = Price.LastUpdateBy

                        m_TransactionManager.AddUpdate(Price_old, m_userPrincipal.Identity.Name)

                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    'Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function
#End Region

#Region "Custom Method"
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RevisionPriceMapper.RetrieveByCriteria(criterias, sorts)
        End Function
#End Region

    End Class

End Namespace

