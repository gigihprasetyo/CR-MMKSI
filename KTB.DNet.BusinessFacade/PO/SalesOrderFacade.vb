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
'// Generated on 10/7/2005 - 1:28:25 PM
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
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.BusinessFacade.PO

    Public Class SalesOrderFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SalesOrderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SalesOrderMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesOrder).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SalesOrder
            Return CType(m_SalesOrderMapper.Retrieve(ID), SalesOrder)
        End Function

        Public Function Retrieve(ByVal Code As String) As SalesOrder
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesOrder), "SONumber", MatchType.Exact, Code))

            Dim SalesOrderColl As ArrayList = m_SalesOrderMapper.RetrieveByCriteria(criterias)
            If (SalesOrderColl.Count > 0) Then
                Return CType(SalesOrderColl(0), SalesOrder)
            End If
            Return New SalesOrder
        End Function


        Public Function Retrieve(ByVal Code As String, ByVal strCOmpanyCOde As String) As SalesOrder
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesOrder), "SONumber", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(SalesOrder), "POHeader.ContractHeader.Category.ProductCategory.Code", MatchType.Exact, strCOmpanyCOde))

            Dim SalesOrderColl As ArrayList = m_SalesOrderMapper.RetrieveByCriteria(criterias)
            If (SalesOrderColl.Count > 0) Then
                Return CType(SalesOrderColl(0), SalesOrder)
            End If
            Return New SalesOrder
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesOrderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SalesOrderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SalesOrderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(SalesOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesOrderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(SalesOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesOrderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SalesOrder As ArrayList = m_SalesOrderMapper.RetrieveByCriteria(criterias)
            Return _SalesOrder
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesOrderColl As ArrayList = m_SalesOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesOrderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SalesOrderColl As ArrayList = m_SalesOrderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SalesOrderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SalesOrderColl As ArrayList = m_SalesOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SalesOrderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesOrderColl As ArrayList = m_SalesOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SalesOrder), columnName, matchOperator, columnValue))
            Return SalesOrderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(SalesOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesOrder), columnName, matchOperator, columnValue))

            Return m_SalesOrderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesOrder), "SalesOrderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SalesOrder), "SalesOrderCode", AggregateType.Count)
            Return CType(m_SalesOrderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SalesOrder) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SalesOrderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub Update(ByVal objDomain As SalesOrder)
            Try
                m_SalesOrderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Delete(ByVal objDomain As SalesOrder)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SalesOrderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function RetrieveSp(str As String) As DataSet
            Return m_SalesOrderMapper.RetrieveDataSet(str)
        End Function


        'Public Function RetrieveReqQty(ByVal Code As String, ByVal MNumber As String, ByVal intDate As Short, ByVal intMonth As Short, ByVal intYear As Short) As ArrayList

        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(SalesOrder), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, MNumber))
        '    criterias.opAnd(New Criteria(GetType(SalesOrder), "POHeader.ContractHeader.Dealer.DealerCode", MatchType.Exact, Code))
        '    criterias.opAnd(New Criteria(GetType(SalesOrder), "POHeader.ReqAllocationDate", MatchType.Exact, intDate))
        '    criterias.opAnd(New Criteria(GetType(SalesOrder), "POHeader.ReqAllocationMonth", MatchType.Exact, intMonth))
        '    criterias.opAnd(New Criteria(GetType(SalesOrder), "POHeader.ReqAllocationYear", MatchType.Exact, intYear))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Baru, Integer) & "," & CType(enumStatusPO.Status.Konfirmasi, Integer) & "," & CType(enumStatusPO.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & "," & CType(enumStatusPO.Status.Tidak_Setuju, Integer) & ")"))

        '    Dim SalesOrderColl As ArrayList = m_SalesOrderMapper.RetrieveByCriteria(criterias)
        '    'If (SalesOrderColl.Count > 0) Then
        '    '    Return CType(SalesOrderColl(0), SalesOrder)
        '    'End If
        '    Return SalesOrderColl

        'End Function

        'Public Function RetrievePOHeader(ByVal id As Integer) As SalesOrder
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(SalesOrder), "POHeader.ID", MatchType.Exact, id))

        '    Dim SalesOrderColl As ArrayList = m_SalesOrderMapper.RetrieveByCriteria(criterias)
        '    If (SalesOrderColl.Count > 0) Then
        '        Return CType(SalesOrderColl(0), SalesOrder)
        '    End If
        '    Return New SalesOrder
        'End Function

        Public Function CheckSalesOrder() As DataTable
            Dim spName As String
            Dim Param As New List(Of SqlClient.SqlParameter)

            'Dim DateParam As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day, 0, 0, 0)
            spName = "sp_CheckTotalVH_SalesOrder"
            'Param.Add(New SqlClient.SqlParameter("@SOCreated", DateParam))

            Dim arr As DataSet
            'arr = m_SalesOrderMapper.RetrieveDataSet(spName, New ArrayList(Param))
            arr = m_SalesOrderMapper.RetrieveDataSet(spName)
            If arr.Tables.Count > 0 Then
                Return arr.Tables(0)
            Else
                Return Nothing
            End If
        End Function

#End Region

    End Class

End Namespace