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
'// Generated on 10/12/2005 - 8:26:12 AM
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

Namespace KTB.DNet.BusinessFacade.General

    Public Class TermOfPaymentFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TermOfPaymentMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TermOfPaymentMapper = MapperFactory.GetInstance.GetMapper(GetType(TermOfPayment).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TermOfPayment
            Return CType(m_TermOfPaymentMapper.Retrieve(ID), TermOfPayment)
        End Function



        Public Function RetrieveScalar(ByVal Criterias As ICriteria, ByVal aggregate As Aggregate) As Object
            Dim obj As Object = m_TermOfPaymentMapper.RetrieveScalar(aggregate, Criterias)
            If obj Is DBNull.Value Then
                Return Nothing
            Else
                Return obj
            End If
        End Function


        Public Function Retrieve(ByVal Code As String) As TermOfPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TermOfPayment), "TermOfPaymentCode", MatchType.Exact, Code))

            Dim TermOfPaymentColl As ArrayList = m_TermOfPaymentMapper.RetrieveByCriteria(criterias)
            If (TermOfPaymentColl.Count > 0) Then
                Return CType(TermOfPaymentColl(0), TermOfPayment)
            End If
            Return New TermOfPayment
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TermOfPaymentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TermOfPaymentMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TermOfPaymentMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(TermOfPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TermOfPaymentMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(TermOfPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TermOfPaymentMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
            If (Not IsNothing("TermOfPaymentCode")) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(TermOfPayment), "TermOfPaymentCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If

            Dim _TermOfPayment As ArrayList = m_TermOfPaymentMapper.RetrieveByCriteria(criterias, sortColl)
            Return _TermOfPayment
        End Function

        Public Function RetrieveActivePaymentTypeList() As ArrayList
            Dim SQL As String = String.Empty

            SQL = "select * from TermOfPayment tp inner join paymenttype pt on pt.id = tp.PaymentType where tp.rowstatus = " & CType(DBRowStatus.Active, Short)

            Return m_TermOfPaymentMapper.RetrieveSP(SQL)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TermOfPaymentColl As ArrayList = m_TermOfPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TermOfPaymentColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TermOfPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TermOfPaymentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TermOfPaymentColl As ArrayList = m_TermOfPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TermOfPaymentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TermOfPaymentColl As ArrayList = m_TermOfPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TermOfPayment), columnName, matchOperator, columnValue))
            Return TermOfPaymentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(TermOfPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), columnName, matchOperator, columnValue))

            Return m_TermOfPaymentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TermOfPayment), "TermOfPaymentCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TermOfPayment), "TermOfPaymentCode", AggregateType.Count)
            Return CType(m_TermOfPaymentMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TermOfPayment) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TermOfPaymentMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TermOfPayment) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TermOfPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TermOfPayment)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TermOfPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TermOfPayment)
            Try
                m_TermOfPaymentMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


        Public Function RetrieveFromSP(ByVal PaymentType As Integer, ByVal Kelipatan As Integer, ByVal TopValue As Integer) As ArrayList
            Dim SQL As String = String.Empty

            SQL = "select * from  [dbo].[FNI_GetTOPDataChoose] ( " & PaymentType & "," & Kelipatan & "," & TopValue & ")"

            Return m_TermOfPaymentMapper.RetrieveSP(SQL)
            'm_BenefitMasterHeaderMapper.RetrieveScalar()
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

