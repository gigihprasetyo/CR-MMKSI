
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
'// Generated on 5/27/2016 - 7:13:56 PM
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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade.Service

    Public Class DepositBStatusHistoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositBStatusHistoryMapper As IMapper

        Private m_TransactionManager As TransactionManager

        Private m_DepositBPencairanHeaderMapper As IMapper
        Private m_DepositBDebitNoteMapper As IMapper
        Private m_DepositBInterestHeaderMapper As IMapper
        Private m_DepositBKewajibanHeaderMapper As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DepositBStatusHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBStatusHistory).ToString)

            Me.m_DepositBPencairanHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBPencairanHeader).ToString)
            Me.m_DepositBDebitNoteMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBDebitNote).ToString)
            Me.m_DepositBInterestHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBInterestHeader).ToString)
            Me.m_DepositBKewajibanHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBKewajibanHeader).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DepositBStatusHistory
            Return CType(m_DepositBStatusHistoryMapper.Retrieve(ID), DepositBStatusHistory)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositBStatusHistoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositBStatusHistoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositBStatusHistoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBStatusHistoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBStatusHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositBStatusHistory As ArrayList = m_DepositBStatusHistoryMapper.RetrieveByCriteria(criterias)
            Return _DepositBStatusHistory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBStatusHistoryColl As ArrayList = m_DepositBStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DepositBStatusHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositBStatusHistoryColl As ArrayList = m_DepositBStatusHistoryMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositBStatusHistoryColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositBStatusHistoryColl As ArrayList = m_DepositBStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositBStatusHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBStatusHistoryColl As ArrayList = m_DepositBStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositBStatusHistory), columnName, matchOperator, columnValue))
            Return DepositBStatusHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBStatusHistory), columnName, matchOperator, columnValue))

            Return m_DepositBStatusHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As DepositBStatusHistory) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DepositBStatusHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DepositBStatusHistory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DepositBStatusHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DepositBStatusHistory)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DepositBStatusHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DepositBStatusHistory)
            Try
                m_DepositBStatusHistoryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


        Public Function SaveHistoricalPencairan(ByVal objDepositBPencairanHeader As DepositBPencairanHeader, _
                                       ByVal iNewStatus As Integer, _
                                       ByVal iOldStatus As Integer) As Integer
            Dim iReturn As Integer = -1
            Try
                Dim objDomain As DepositBStatusHistory = New DepositBStatusHistory
                objDomain.StatusType = DepositBEnum.StatusType.Pencairan
                objDomain.DepositBPencairanHeader = objDepositBPencairanHeader
                objDomain.NewStatus = iNewStatus
                objDomain.OldStatus = iOldStatus

                iReturn = m_DepositBStatusHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Dim nResult As Integer
        End Function

        Public Function SaveHistoricalPengajuan(ByVal statusType As Integer, _
                                                ByVal id As Integer, _
                                                ByVal iNewStatus As Integer, _
                                                ByVal iOldStatus As Integer) As Integer
            Dim iReturn As Integer = -1
            Try
                Dim objDomain As DepositBStatusHistory = New DepositBStatusHistory
                objDomain.StatusType = statusType
                Select Case statusType
                    Case 1
                        'Dim objDepositPencairanHeader As DepositBPencairanHeader = New DepositBPencairanHeaderFacade(m_userPrincipal.Identity.Name).Retrieve(id)
                        Dim objDepositPencairanHeader As DepositBPencairanHeader = m_DepositBPencairanHeaderMapper.Retrieve(id)
                        objDomain.DepositBPencairanHeader = objDepositPencairanHeader
                    Case 2
                        'Dim objDepositBDebitNote As DepositBDebitNote = New DepositBDebitNoteFacade(m_userPrincipal.Identity.Name).Retrieve(id)
                        Dim objDepositBDebitNote As DepositBDebitNote = m_DepositBDebitNoteMapper.Retrieve(id)
                        objDomain.DepositBDebitNote = objDepositBDebitNote
                    Case 3
                        'Dim objDepositBInterest As DepositBInterestHeader = New DepositBInterestHeaderFacade(m_userPrincipal.Identity.Name).Retrieve(id)
                        Dim objDepositBInterest As DepositBInterestHeader = m_DepositBInterestHeaderMapper.Retrieve(id)
                        objDomain.DepositBInterestHeader = objDepositBInterest
                    Case 4
                        'Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = New DepositBKewajibanHeaderFacade(m_userPrincipal.Identity.Name).Retrieve(id)
                        Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = m_DepositBKewajibanHeaderMapper.Retrieve(id)
                        objDomain.DepositBKewajibanHeader = objDepositBKewajibanHeader
                End Select
                objDomain.NewStatus = iNewStatus
                objDomain.OldStatus = iOldStatus

                iReturn = m_DepositBStatusHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Dim nResult As Integer
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

