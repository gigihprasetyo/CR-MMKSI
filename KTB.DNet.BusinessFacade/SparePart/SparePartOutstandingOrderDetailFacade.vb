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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 9/26/2005 - 2:38:25 PM
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
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class SparePartOutstandingOrderDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartOutstandingOrderDetailMapper As IMapper



#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartOutstandingOrderDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartOutstandingOrderDetail).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartOutstandingOrderDetail
            Return CType(m_SparePartOutstandingOrderDetailMapper.Retrieve(ID), SparePartOutstandingOrderDetail)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartOutstandingOrderDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartOutstandingOrderDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartOutstandingOrderDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartOutstandingOrderDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartOutstandingOrderDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartOutstandingOrderDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartOutstandingOrderDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartOutstandingOrderDetail As ArrayList = m_SparePartOutstandingOrderDetailMapper.RetrieveByCriteria(criterias)
            Return _SparePartOutstandingOrderDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartOutstandingOrderDetailColl As ArrayList = m_SparePartOutstandingOrderDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartOutstandingOrderDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(SparePartOutstandingOrderDetail), SortColumn, sortDirection))

            If SortColumn.ToUpper() = "SparePartOutstandingOrder.SparePartPO.TermOfPayment.ID".ToUpper() Then
                Dim sSQL As String = GetRetrieveSpSortByTOP(criterias, sortColl, pageNumber, pageSize, totalRow)
                Dim result As ArrayList = m_SparePartOutstandingOrderDetailMapper.RetrieveSP(sSQL)
                totalRow = GetRowCount(criterias)
                Return result
            End If

            Dim SparePartOutstandingOrderDetailColl As ArrayList = m_SparePartOutstandingOrderDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartOutstandingOrderDetailColl
        End Function

        Private Function GetRetrieveSpSortByTOP(ByVal Criterias As ICriteria, ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As String
            Dim sSQL As String = "EXEC up_PagingQuery "
            sSQL &= "@Table = N'SparePartOutstandingOrderDetail', "
            sSQL &= "@PK = N'ID', "
            sSQL &= "@PageSize = " & pageSize & ", "
            sSQL &= "@PageNumber = " & pageNumber & ", "
            sSQL &= "@Filter = N' INNER JOIN SparePartOutstandingOrder ON SparePartOutstandingOrderDetail.SparePartOutstandingOrderID = SparePartOutstandingOrder.ID INNER JOIN SparePartPO ON SparePartOutstandingOrder.SparePartPOID = SparePartPO.ID LEFT JOIN TermOfPayment ON SparePartPO.TermOfPaymentID = TermOfPayment.ID "
            sSQL &= Criterias.ToString().Replace("'", "''").Replace("INNER JOIN SparePartOutstandingOrder ON SparePartOutstandingOrderDetail.SparePartOutstandingOrderID = SparePartOutstandingOrder.ID INNER JOIN SparePartPO ON SparePartOutstandingOrder.SparePartPOID = SparePartPO.ID", "")
            sSQL &= "', @Sort = N'" & Sorts.ToString() & "'"

            Return sSQL
        End Function

        Private Function GetRowCount(ByVal Criterias As ICriteria) As Integer
            Dim agg As Aggregate = New Aggregate(GetType(SparePartOutstandingOrderDetail), "ID", AggregateType.Count)

            Return CType(m_SparePartOutstandingOrderDetailMapper.RetrieveScalar(agg, Criterias), Integer)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartOutstandingOrderDetailColl As ArrayList = m_SparePartOutstandingOrderDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartOutstandingOrderDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartOutstandingOrderDetailColl As ArrayList = m_SparePartOutstandingOrderDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), columnName, matchOperator, columnValue))
            Return SparePartOutstandingOrderDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartOutstandingOrderDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrderDetail), columnName, matchOperator, columnValue))

            Return m_SparePartOutstandingOrderDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOEstimate), "SparePartPOEstimateCode", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(SparePartPOEstimate), "SparePartPOEstimateCode", AggregateType.Count)
        '    Return CType(m_SparePartPOEstimateMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

        Public Function Insert(ByVal objDomain As SparePartOutstandingOrderDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SparePartOutstandingOrderDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartOutstandingOrderDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartOutstandingOrderDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartOutstandingOrderDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartOutstandingOrderDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartPO)
            Try
                m_SparePartOutstandingOrderDetailMapper.Delete(objDomain)
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
