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

#End Region

Namespace KTB.DNet.BusinessFacade.PO

    Public Class PODetailFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PODetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PODetail
            Return CType(m_PODetailMapper.Retrieve(ID), PODetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As PODetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PODetail), "PODetailCode", MatchType.Exact, Code))

            Dim PODetailColl As ArrayList = m_PODetailMapper.RetrieveByCriteria(criterias)
            If (PODetailColl.Count > 0) Then
                Return CType(PODetailColl(0), PODetail)
            End If
            Return New PODetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PODetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PODetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PODetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PODetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PODetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PODetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PODetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PODetail As ArrayList = m_PODetailMapper.RetrieveByCriteria(criterias)
            Return _PODetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PODetailColl As ArrayList = m_PODetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PODetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PODetailColl As ArrayList = m_PODetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PODetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PODetailColl As ArrayList = m_PODetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PODetail), columnName, matchOperator, columnValue))
            Return PODetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.domain.Search.SortCollection = New KTB.DNet.domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.domain.Search.Sort(GetType(PODetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), columnName, matchOperator, columnValue))

            Return m_PODetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "PODetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PODetail), "PODetailCode", AggregateType.Count)
            Return CType(m_PODetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Sub Update(ByVal objDomain As PODetail)
            Try
                m_PODetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Delete(ByVal objDomain As PODetail)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_PODetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        'Public Function RetrieveReqQty(ByVal Code As String, ByVal MNumber As String, ByVal intDate As Short, ByVal intMonth As Short, ByVal intYear As Short) As ArrayList

        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, MNumber))
        '    criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ContractHeader.Dealer.DealerCode", MatchType.Exact, Code))
        '    criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDate", MatchType.Exact, intDate))
        '    criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationMonth", MatchType.Exact, intMonth))
        '    criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationYear", MatchType.Exact, intYear))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Baru, Integer) & "," & CType(enumStatusPO.Status.Konfirmasi, Integer) & "," & CType(enumStatusPO.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & "," & CType(enumStatusPO.Status.Tidak_Setuju, Integer) & ")"))

        '    Dim PODetailColl As ArrayList = m_PODetailMapper.RetrieveByCriteria(criterias)
        '    'If (PODetailColl.Count > 0) Then
        '    '    Return CType(PODetailColl(0), PODetail)
        '    'End If
        '    Return PODetailColl

        'End Function

        'Public Function RetrievePOHeader(ByVal id As Integer) As PODetail
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ID", MatchType.Exact, id))

        '    Dim PODetailColl As ArrayList = m_PODetailMapper.RetrieveByCriteria(criterias)
        '    If (PODetailColl.Count > 0) Then
        '        Return CType(PODetailColl(0), PODetail)
        '    End If
        '    Return New PODetail
        'End Function

        Public Function GetContractDetail(ByVal oPOD As PODetail) As ContractDetail
            For Each oCD As ContractDetail In oPOD.POHeader.ContractHeader.ContractDetails
                If oCD.LineItem = oPOD.LineItem Then
                    Return oCD
                End If
            Next
            Return New ContractDetail
        End Function
#End Region

        Public Function RetrieveAggregate(ByVal crit As CriteriaComposite, ByVal agg As Aggregate) As Object
            Dim Total As Decimal = 0
            Try
                Total = CType(m_PODetailMapper.RetrieveScalar(agg, crit), Object)
            Catch ex As Exception
                Total = 0
            End Try
            Return Total '  CType(m_DailyPaymentMapper.RetrieveScalar(agg, crit), Object)
        End Function

    End Class

End Namespace