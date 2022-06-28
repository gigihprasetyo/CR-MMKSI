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

Namespace KTB.DNet.BusinessFacade.Transfer
    Public Class TransferPaymentDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_TransferPaymentDetailMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_TransferPaymentDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(TransferPaymentDetail).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TransferPaymentDetail
            Return CType(m_TransferPaymentDetailMapper.Retrieve(ID), TransferPaymentDetail)
        End Function

        Public Function Retrieve(ByVal TransferPaymentDetailCode As String) As TransferPaymentDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TransferPaymentDetail), "TransferPaymentDetailCode", MatchType.Exact, TransferPaymentDetailCode))

            Dim TransferPaymentDetailColl As ArrayList = m_TransferPaymentDetailMapper.RetrieveByCriteria(criterias)
            If (TransferPaymentDetailColl.Count > 0) Then
                Return CType(TransferPaymentDetailColl(0), TransferPaymentDetail)
            End If
            Return New TransferPaymentDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TransferPaymentDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TransferPaymentDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TransferPaymentDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferPaymentDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferPaymentDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferPaymentDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferPaymentDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("TransferPaymentDetailCode")) Then
                sortColl.Add(New Sort(GetType(TransferPaymentDetail), "TransferPaymentDetailCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _TransferPaymentDetail As ArrayList = m_TransferPaymentDetailMapper.RetrieveByCriteria(criterias, sortColl)
            Return _TransferPaymentDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TransferPaymentDetailColl As ArrayList = m_TransferPaymentDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferPaymentDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TransferPaymentDetailColl As ArrayList = m_TransferPaymentDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferPaymentDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TransferPaymentDetail), columnName, matchOperator, columnValue))
            Dim TransferPaymentDetailColl As ArrayList = m_TransferPaymentDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferPaymentDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferPaymentDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferPaymentDetail), columnName, matchOperator, columnValue))

            Return m_TransferPaymentDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferPaymentDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferPaymentDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As TransferPaymentDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TransferPaymentDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TransferPaymentDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TransferPaymentDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TransferPaymentDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TransferPaymentDetailMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TransferPaymentDetail)
            Try
                m_TransferPaymentDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferPaymentDetail), "TransferPaymentDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TransferPaymentDetail), "TransferPaymentDetailCode", AggregateType.Count)

            Return CType(m_TransferPaymentDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TransferPaymentDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferPaymentDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TransferPaymentDetailColl As ArrayList = m_TransferPaymentDetailMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TransferPaymentDetailColl
        End Function

        Public Function isLCExists(ByVal paymentPurposeCode As String, ByVal aTPDs As ArrayList) As String
            If (paymentPurposeCode <> "LC") Then

            For Each oTPD As TransferPaymentDetail In aTPDs

                Dim salesOrder As SalesOrder = oTPD.SalesOrder

                If salesOrder.POHeader.PODestination.ID <> 1 Then '1 adalah pengiriman dari Dealer, yang dicek hanya yg dr MMKSI

                    'cari di salesorderduedate apakah ada yang LC untuk sales order tersebut
                    Dim getSalesOrderLCQuery As String = "SELECT * FROM SalesOrderDueDate A INNER JOIN PaymentPurpose B ON B.ID = A.PaymentPurposeID AND A.SalesOrderID = " + oTPD.SalesOrder.ID.ToString() + " AND B.PaymentPurposeCode = 'LC'"
                    Dim ds As DataSet = RetrieveSp(getSalesOrderLCQuery)
                    If ds.Tables(0).Rows.Count <> 0 Then
                        'cari dari transferpayment yang sudah LC apakah sudah dicreate
                        Dim criteriaTransfer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriaTransfer.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransferPaymentDetail), "SalesOrder.ID", MatchType.Exact, oTPD.SalesOrder.ID))
                        criteriaTransfer.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransferPaymentDetail), "TransferPayment.PaymentPurpose.PaymentPurposeCode", MatchType.Exact, "LC"))
                        Dim sortCollTransfer As SortCollection = New SortCollection
                        sortCollTransfer.Add(New Sort(GetType(TransferPaymentDetail), "ID", Sort.SortDirection.DESC))

                        Dim transferPaymentDetail As ArrayList = m_TransferPaymentDetailMapper.RetrieveByCriteria(criteriaTransfer, sortCollTransfer)

                        If IsNothing(transferPaymentDetail) Or transferPaymentDetail.Count = 0 Then
                            Return salesOrder.SONumber
                        End If

                    End If
                End If

            Next
            End If
            Return ""
        End Function

        Public Function RetrieveSp(str As String) As DataSet
            Return m_TransferPaymentDetailMapper.RetrieveDataSet(str)
        End Function

#End Region

    End Class

End Namespace
