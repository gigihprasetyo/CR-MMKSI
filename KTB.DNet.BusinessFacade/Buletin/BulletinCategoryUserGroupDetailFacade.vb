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
'// PURInvoiceSE       : Enter summary here after generation.
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

#End Region

Namespace KTB.DNet.BusinessFacade.PO

    Public Class BulletinCategoryUserGroupDetailFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BulletinCategoryUserGroupDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BulletinCategoryUserGroupDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(BulletinCategoryUserGroupDetail).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BulletinCategoryUserGroupDetail
            Return CType(m_BulletinCategoryUserGroupDetailMapper.Retrieve(ID), BulletinCategoryUserGroupDetail)
        End Function

        Public Function RetrieveByUser(ByVal ID As Integer) As BulletinCategoryUserGroupDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BulletinCategoryUserGroupDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BulletinCategoryUserGroupDetail), "UserGroupID", MatchType.Exact, ID))

            Dim BulletinCategoryUserGroupDetailColl As ArrayList = m_BulletinCategoryUserGroupDetailMapper.RetrieveByCriteria(criterias)
            If (BulletinCategoryUserGroupDetailColl.Count > 0) Then
                Return CType(BulletinCategoryUserGroupDetailColl(0), BulletinCategoryUserGroupDetail)
            End If
            Return New BulletinCategoryUserGroupDetail
        End Function

        Public Function RetrieveByBulletinCategory(ByVal ID As Integer) As BulletinCategoryUserGroupDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BulletinCategoryUserGroupDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BulletinCategoryUserGroupDetail), "BuletinCategoryID", MatchType.Exact, ID))

            Dim BulletinCategoryUserGroupDetailColl As ArrayList = m_BulletinCategoryUserGroupDetailMapper.RetrieveByCriteria(criterias)
            If (BulletinCategoryUserGroupDetailColl.Count > 0) Then
                Return CType(BulletinCategoryUserGroupDetailColl(0), BulletinCategoryUserGroupDetail)
            End If
            Return New BulletinCategoryUserGroupDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BulletinCategoryUserGroupDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BulletinCategoryUserGroupDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BulletinCategoryUserGroupDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BulletinCategoryUserGroupDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BulletinCategoryUserGroupDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BulletinCategoryUserGroupDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BulletinCategoryUserGroupDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BulletinCategoryUserGroupDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BulletinCategoryUserGroupDetail As ArrayList = m_BulletinCategoryUserGroupDetailMapper.RetrieveByCriteria(criterias)
            Return _BulletinCategoryUserGroupDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BulletinCategoryUserGroupDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BulletinCategoryUserGroupDetailColl As ArrayList = m_BulletinCategoryUserGroupDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BulletinCategoryUserGroupDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BulletinCategoryUserGroupDetailColl As ArrayList = m_BulletinCategoryUserGroupDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BulletinCategoryUserGroupDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BulletinCategoryUserGroupDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BulletinCategoryUserGroupDetailColl As ArrayList = m_BulletinCategoryUserGroupDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BulletinCategoryUserGroupDetail), columnName, matchOperator, columnValue))
            Return BulletinCategoryUserGroupDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.domain.Search.SortCollection = New KTB.DNet.domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.domain.Search.Sort(GetType(BulletinCategoryUserGroupDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BulletinCategoryUserGroupDetail), columnName, matchOperator, columnValue))

            Return m_BulletinCategoryUserGroupDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BulletinCategoryUserGroupDetail), "BulletinCategoryUserGroupDetailCode", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(BulletinCategoryUserGroupDetail), "BulletinCategoryUserGroupDetailCode", AggregateType.Count)
        '    Return CType(m_BulletinCategoryUserGroupDetailMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

        Public Sub Update(ByVal objDomain As BulletinCategoryUserGroupDetail)
            Try
                m_BulletinCategoryUserGroupDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Invoicelicy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Delete(ByVal objDomain As BulletinCategoryUserGroupDetail)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BulletinCategoryUserGroupDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Invoicelicy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        'Public Function RetrieveReqQty(ByVal Code As String, ByVal MNumber As String, ByVal intDate As Short, ByVal intMonth As Short, ByVal intYear As Short) As ArrayList

        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BulletinCategoryUserGroupDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(BulletinCategoryUserGroupDetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, MNumber))
        '    criterias.opAnd(New Criteria(GetType(BulletinCategoryUserGroupDetail), "InvoiceHeader.ContractHeader.Dealer.DealerCode", MatchType.Exact, Code))
        '    criterias.opAnd(New Criteria(GetType(BulletinCategoryUserGroupDetail), "InvoiceHeader.ReqAllocationDate", MatchType.Exact, intDate))
        '    criterias.opAnd(New Criteria(GetType(BulletinCategoryUserGroupDetail), "InvoiceHeader.ReqAllocationMonth", MatchType.Exact, intMonth))
        '    criterias.opAnd(New Criteria(GetType(BulletinCategoryUserGroupDetail), "InvoiceHeader.ReqAllocationYear", MatchType.Exact, intYear))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BulletinCategoryUserGroupDetail), "InvoiceHeader.Status", MatchType.InSet, "(" & CType(enumStatusInvoice.Status.Baru, Integer) & "," & CType(enumStatusInvoice.Status.Konfirmasi, Integer) & "," & CType(enumStatusInvoice.Status.Rilis, Integer) & "," & CType(enumStatusInvoice.Status.Selesai, Integer) & "," & CType(enumStatusInvoice.Status.Setuju, Integer) & "," & CType(enumStatusInvoice.Status.Tidak_Setuju, Integer) & ")"))

        '    Dim BulletinCategoryUserGroupDetailColl As ArrayList = m_BulletinCategoryUserGroupDetailMapper.RetrieveByCriteria(criterias)
        '    'If (BulletinCategoryUserGroupDetailColl.Count > 0) Then
        '    '    Return CType(BulletinCategoryUserGroupDetailColl(0), BulletinCategoryUserGroupDetail)
        '    'End If
        '    Return BulletinCategoryUserGroupDetailColl

        'End Function

        'Public Function RetrieveInvoiceHeader(ByVal id As Integer) As BulletinCategoryUserGroupDetail
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BulletinCategoryUserGroupDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(BulletinCategoryUserGroupDetail), "InvoiceHeader.ID", MatchType.Exact, id))

        '    Dim BulletinCategoryUserGroupDetailColl As ArrayList = m_BulletinCategoryUserGroupDetailMapper.RetrieveByCriteria(criterias)
        '    If (BulletinCategoryUserGroupDetailColl.Count > 0) Then
        '        Return CType(BulletinCategoryUserGroupDetailColl(0), BulletinCategoryUserGroupDetail)
        '    End If
        '    Return New BulletinCategoryUserGroupDetail
        'End Function

#End Region

    End Class

End Namespace