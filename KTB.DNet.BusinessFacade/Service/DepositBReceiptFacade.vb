
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
'// Copyright  2016
'// ---------------------
'// $History      : $
'// Generated on 3/14/2016 - 11:42:16 AM
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

Namespace KTB.DNET.BusinessFacade.Service

    Public Class DepositBReceiptFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositBReceiptMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DepositBReceiptMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBReceipt).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DepositBReceipt
            Return CType(m_DepositBReceiptMapper.Retrieve(ID), DepositBReceipt)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositBReceiptMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositBReceiptMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositBReceiptMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBReceiptMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBReceiptMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositBReceipt As ArrayList = m_DepositBReceiptMapper.RetrieveByCriteria(criterias)
            Return _DepositBReceipt
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositAKuitansiPencairanColl As ArrayList = m_DepositBReceiptMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositAKuitansiPencairanColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBReceiptColl As ArrayList = m_DepositBReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DepositBReceiptColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositBReceiptColl As ArrayList = m_DepositBReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositBReceiptColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBReceiptColl As ArrayList = m_DepositBReceiptMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositBReceipt), columnName, matchOperator, columnValue))
            Return DepositBReceiptColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBReceipt), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBReceipt), columnName, matchOperator, columnValue))

            Return m_DepositBReceiptMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As DepositBReceipt) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_DepositBReceiptMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DepositBReceipt) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DepositBReceiptMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DepositBReceipt)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DepositBReceiptMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DepositBReceipt)
            Try
                m_DepositBReceiptMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveByNoRegPencairanHeader(ByVal NoReg As String) As DepositBReceipt

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositBReceipt), "DepositBPencairanHeader.NoReg", MatchType.Exact, NoReg))

            Dim DepositBReceiptColl As ArrayList = m_DepositBReceiptMapper.RetrieveByCriteria(criterias)
            If (DepositBReceiptColl.Count > 0) Then
                Return CType(DepositBReceiptColl(0), DepositBReceipt)
            End If
            Return New DepositBReceipt

        End Function

        Public Function RetrieveByNoRegKuitansi(ByVal NoRegKuitansi As String) As DepositBReceipt
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositBReceipt), "NoRegKuitansi", MatchType.Exact, NoRegKuitansi))

            Dim DepositBReceiptColl As ArrayList = m_DepositBReceiptMapper.RetrieveByCriteria(criterias)
            If (DepositBReceiptColl.Count > 0) Then
                Return CType(DepositBReceiptColl(0), DepositBReceipt)
            End If
            Return New DepositBReceipt

        End Function

        Public Function RetrieveByPencairanHeader(ByVal DepositBPencairanHeaderID As Integer) As DepositBReceipt
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositBReceipt), "DepositBPencairanHeader.ID", MatchType.Exact, DepositBPencairanHeaderID))
            Dim DepositBReceiptColl As ArrayList = m_DepositBReceiptMapper.RetrieveByCriteria(criterias)
            If (DepositBReceiptColl.Count > 0) Then
                Return CType(DepositBReceiptColl(0), DepositBReceipt)
            End If
            Return New DepositBReceipt
        End Function
#End Region

    End Class

End Namespace

