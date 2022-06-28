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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 7/27/2007 - 9:51:42 AM
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

Imports KTb.DNet.Domain
Imports KTb.DNet.Domain.Search
Imports KTb.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTb.DNet.BusinessFacade.Claim
    Public Class ClaimStatusHistoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ClaimStatusHistoryMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ClaimStatusHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.ClaimStatusHistory).ToString)
            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As KTB.DNet.Domain.ClaimStatusHistory
            Return CType(m_ClaimStatusHistoryMapper.Retrieve(ID), KTB.DNet.Domain.ClaimStatusHistory)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.ClaimStatusHistory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), "ClaimStatusHistoyCode", MatchType.Exact, Code))

            Dim ClaimStatusHistoyColl As ArrayList = m_ClaimStatusHistoryMapper.RetrieveByCriteria(criterias)
            If (ClaimStatusHistoyColl.Count > 0) Then
                Return CType(ClaimStatusHistoyColl(0), KTB.DNet.Domain.ClaimStatusHistory)
            End If
            Return New KTB.DNet.Domain.ClaimStatusHistory
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ClaimStatusHistoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ClaimStatusHistoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ClaimStatusHistoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.ClaimStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ClaimStatusHistoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.ClaimStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ClaimStatusHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ClaimStatusHistoy As ArrayList = m_ClaimStatusHistoryMapper.RetrieveByCriteria(criterias)
            Return _ClaimStatusHistoy
        End Function

        Public Function RetrieveActiveListHeader() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), "IsHeader", MatchType.Exact, "1"))
            Dim _ClaimStatusHistoy As ArrayList = m_ClaimStatusHistoryMapper.RetrieveByCriteria(criterias)
            Return _ClaimStatusHistoy
        End Function

        Public Function RetrieveActiveListDetail() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), "IsHeader", MatchType.Exact, "0"))
            Dim _ClaimStatusHistoy As ArrayList = m_ClaimStatusHistoryMapper.RetrieveByCriteria(criterias)
            Return _ClaimStatusHistoy
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ClaimStatusHistoyColl As ArrayList = m_ClaimStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ClaimStatusHistoyColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.ClaimStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimStatusHistoyColl As ArrayList = m_ClaimStatusHistoryMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ClaimStatusHistoyColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.ClaimStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ClaimStatusHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ClaimStatusHistoyColl As ArrayList = m_ClaimStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ClaimStatusHistoyColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ClaimStatusHistoyColl As ArrayList = m_ClaimStatusHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), columnName, matchOperator, columnValue))
            Return ClaimStatusHistoyColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.ClaimStatusHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), columnName, matchOperator, columnValue))

            Return m_ClaimStatusHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ClaimStatusHistory), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.ClaimStatusHistory), "Code", AggregateType.Count)
            Return CType(m_ClaimStatusHistoryMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.ClaimStatusHistory) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ClaimStatusHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.ClaimStatusHistory) As Integer
            Dim iReturn As Integer = -1
            Try
                m_ClaimStatusHistoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                iReturn = objDomain.ID
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function


        Public Function UpdateCHTransaction(ByVal arrCHHistory As ArrayList, ByVal newStatus As Byte, ByVal oldStatus As Byte) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrCHHistory.Count > 0 Then
                        For Each objClaimStatusHistory As ClaimStatusHistory In arrCHHistory
                            Dim objClaimHeader As ClaimHeader = objClaimStatusHistory.ClaimHeader
                            objClaimHeader.StatusKTB = newStatus

                            'See UseCase for status mapping
                            Select Case objClaimHeader.StatusKTB
                                Case EnumClaimProgress.ClaimProgressKTB.Baru
                                    objClaimHeader.Status = EnumClaimStatus.ClaimStatus.Dikirim
                                Case EnumClaimProgress.ClaimProgressKTB.Diproses
                                    objClaimHeader.Status = EnumClaimStatus.ClaimStatus.Proses
                                Case EnumClaimProgress.ClaimProgressKTB.Selesai
                                    objClaimHeader.Status = EnumClaimStatus.ClaimStatus.Selesai
                                Case EnumClaimProgress.ClaimProgressKTB.Complete_Selesai
                                    objClaimHeader.Status = EnumClaimStatus.ClaimStatus.Complete_Selesai
                            End Select

                            m_TransactionManager.AddUpdate(objClaimHeader, m_userPrincipal.Identity.Name)

                            objClaimStatusHistory.Keterangan = ""
                            objClaimStatusHistory.NewStatus = newStatus
                            objClaimStatusHistory.Status = oldStatus
                            m_TransactionManager.AddInsert(objClaimStatusHistory, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function UpdateCHTransaction(ByVal arrCHHistory As ArrayList, ByVal objClaimProgress As ClaimProgress) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrCHHistory.Count > 0 Then
                        For Each objClaimStatusHistory As ClaimStatusHistory In arrCHHistory
                            Dim objClaimHeader As ClaimHeader = objClaimStatusHistory.ClaimHeader
                            objClaimHeader.ClaimProgress = objClaimProgress
                            objClaimHeader.StatusKTB = objClaimStatusHistory.Status

                            'See UseCase for status mapping
                            Select Case objClaimHeader.StatusKTB
                                Case EnumClaimProgress.ClaimProgressKTB.Baru
                                    objClaimHeader.Status = EnumClaimStatus.ClaimStatus.Dikirim
                                Case EnumClaimProgress.ClaimProgressKTB.Diproses
                                    objClaimHeader.Status = EnumClaimStatus.ClaimStatus.Proses
                                Case EnumClaimProgress.ClaimProgressKTB.Selesai
                                    objClaimHeader.Status = EnumClaimStatus.ClaimStatus.Selesai
                                Case EnumClaimProgress.ClaimProgressKTB.Complete_Selesai
                                    objClaimHeader.Status = EnumClaimStatus.ClaimStatus.Complete_Selesai

                            End Select

                            m_TransactionManager.AddUpdate(objClaimHeader, m_userPrincipal.Identity.Name)

                            objClaimStatusHistory.Keterangan = ""
                            m_TransactionManager.AddInsert(objClaimStatusHistory, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As KTB.DNet.Domain.ClaimStatusHistory)
            Try
                m_ClaimStatusHistoryMapper.Delete(objDomain)
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
