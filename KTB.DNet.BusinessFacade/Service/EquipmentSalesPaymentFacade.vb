
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
'// Generated on 9/26/2005 - 1:07:25 PM
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

Imports KTB.Dnet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.Dnet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.Dnet.BusinessFacade.Service

    Public Class EquipmentSalesPaymentFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EquipmentSalesPaymentMapper As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EquipmentSalesPaymentMapper = MapperFactory.GetInstance.GetMapper(GetType(EquipmentSalesPayment).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As EquipmentSalesPayment
            Return CType(m_EquipmentSalesPaymentMapper.Retrieve(ID), EquipmentSalesPayment)
        End Function

        Public Function Retrieve(ByVal Code As String) As EquipmentSalesPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EquipmentSalesPayment), "KwitansiNumber", MatchType.Exact, Code))

            Dim EquipmentSalesPaymentColl As ArrayList = m_EquipmentSalesPaymentMapper.RetrieveByCriteria(criterias)
            If (EquipmentSalesPaymentColl.Count > 0) Then
                Return CType(EquipmentSalesPaymentColl(0), EquipmentSalesPayment)
            End If
            Return New EquipmentSalesPayment
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EquipmentSalesPaymentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EquipmentSalesPaymentMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipmentSalesPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_EquipmentSalesPaymentMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EquipmentSalesPaymentMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(EquipmentSalesPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EquipmentSalesPaymentMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(EquipmentSalesPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EquipmentSalesPaymentMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EquipmentSalesPayment As ArrayList = m_EquipmentSalesPaymentMapper.RetrieveByCriteria(criterias)
            Return _EquipmentSalesPayment
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EquipmentSalesPaymentColl As ArrayList = m_EquipmentSalesPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EquipmentSalesPaymentColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EquipmentSalesPaymentColl As ArrayList = m_EquipmentSalesPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EquipmentSalesPaymentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EquipmentSalesPaymentColl As ArrayList = m_EquipmentSalesPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EquipmentSalesPayment), columnName, matchOperator, columnValue))
            Return EquipmentSalesPaymentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(EquipmentSalesPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesPayment), columnName, matchOperator, columnValue))

            Return m_EquipmentSalesPaymentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub Insert(ByVal objDomain As EquipmentSalesPayment)
            Dim iReturn As Integer = -2
            Try
                m_EquipmentSalesPaymentMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try

        End Sub

        Public Sub Update(ByVal objDomain As EquipmentSalesPayment)
            Try
                m_EquipmentSalesPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Delete(ByVal objDomain As EquipmentSalesPayment)
            Try
                m_EquipmentSalesPaymentMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesPayment), "EquipmentSalesPaymentCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(EquipmentSalesPayment), "EquipmentSalesPaymentCode", AggregateType.Count)
            Return CType(m_EquipmentSalesPaymentMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
