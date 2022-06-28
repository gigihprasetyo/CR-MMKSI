
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 08/05/2019 - 7:55:50
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
Imports System.Collections.Generic


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class BabitHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitHeader).ToString)
            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitHeader
            Return CType(m_BabitHeaderMapper.Retrieve(ID), BabitHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitHeader), "BabitRegNumber", MatchType.Exact, Code))

            Dim BabitHeaderColl As ArrayList = m_BabitHeaderMapper.RetrieveByCriteria(criterias)
            If (BabitHeaderColl.Count > 0) Then
                Return CType(BabitHeaderColl(0), BabitHeader)
            End If
            Return New BabitHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, param As String) As List(Of BabitHeader)
            Dim arr As ArrayList
            arr = m_BabitHeaderMapper.RetrieveByCriteria(criterias)
            Dim list As New List(Of BabitHeader)
            list.AddRange(CType(arr.ToArray(GetType(BabitHeader)), BabitHeader()))

            For Each row As BabitHeader In arr
                list.Add(row)
            Next
            Return list
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitHeader As ArrayList = m_BabitHeaderMapper.RetrieveByCriteria(criterias)
            Return _BabitHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitHeaderColl As ArrayList = m_BabitHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitHeader), SortColumn, sortDirection))
            Dim BabitHeaderColl As ArrayList = m_BabitHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitHeaderColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitHeaderColl As ArrayList = m_BabitHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitHeaderColl As ArrayList = m_BabitHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitHeader), columnName, matchOperator, columnValue))
            Return BabitHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), columnName, matchOperator, columnValue))

            Return m_BabitHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "BabitHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitHeader), "BabitHeaderCode", AggregateType.Count)
            Return CType(m_BabitHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitHeader)
            Try
                m_BabitHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function UpdateTransaction(ByVal babitHeader As BabitHeader, ByVal babitDealerAllocation As BabitDealerAllocation, ByVal arrBabaitDocument As ArrayList) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If babitDealerAllocation.ID <> 0 Then
                        m_TransactionManager.AddUpdate(babitDealerAllocation, m_userPrincipal.Identity.Name)
                    End If
                    If arrBabaitDocument.Count > 0 Then
                        For Each _BabitDocument As BabitApprovalDocument In arrBabaitDocument
                            m_TransactionManager.AddInsert(_BabitDocument, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.AddUpdate(babitHeader, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = -2
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

        Public Function UpdateTransaction(ByVal babitHeader As BabitHeader, ByVal arrBabitDealerAllocation As ArrayList, ByVal arrBabaitDocument As ArrayList) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrBabitDealerAllocation.Count > 0 Then
                        For Each _BabitDealerAllocation As BabitDealerAllocation In arrBabitDealerAllocation
                            m_TransactionManager.AddUpdate(_BabitDealerAllocation, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arrBabaitDocument.Count > 0 Then
                        For Each _BabitDocument As BabitApprovalDocument In arrBabaitDocument
                            m_TransactionManager.AddInsert(_BabitDocument, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.AddUpdate(babitHeader, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = -2
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

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sorts As ICollection) As ArrayList
            Return m_BabitHeaderMapper.RetrieveByCriteria(Criterias, sorts)
        End Function

        Public Function RetrieveFromSPByPopUp(ByVal DealerID As Integer, ByVal EventRegNumber As String, ByVal EventName As String, ByVal EventDateFrom As DateTime, ByVal EventDateTo As DateTime, chkTanggalCheck As Boolean, BabitMasterEventTypeID As String) As DataSet
            Dim _strSQL As String = "EXEC [up_RetrieveListPopUpBabitEventProposal]"
            _strSQL += " @EventRegNumber = '" & EventRegNumber.Trim & "',"
            _strSQL += " @EventName = '" & EventName.Trim & "',"
            _strSQL += " @DealerID = " & DealerID & ","
            _strSQL += " @BabitMasterEventTypeID = '" & BabitMasterEventTypeID.Trim & "',"
            If chkTanggalCheck Then
                _strSQL += " @EventDateFrom = '" & Format(EventDateFrom, "yyyy/MM/dd") & "',"
                _strSQL += " @EventDateTo = '" & Format(EventDateTo, "yyyy/MM/dd") & "'"
            Else
                _strSQL += " @EventDateFrom = null,"
                _strSQL += " @EventDateTo = null"
            End If

            Return m_BabitHeaderMapper.RetrieveDataSet(_strSQL)
        End Function

        Public Function RetrieveFromSPSPK(ByVal BabitRegNumber As String) As DataSet
            Dim _strSQL As String = "EXEC [up_RetrieveListSPKByBabitRegNumber]"
            _strSQL += " @BabitRegNumber = '" & BabitRegNumber & "'"

            Return m_BabitHeaderMapper.RetrieveDataSet(_strSQL)
        End Function

        Public Function RetrieveFromSPSPKProspek(ByVal BabitRegNumber As String) As DataSet
            Dim _strSQL As String = "EXEC [up_RetrieveListSPKProspekByBabitRegNumber]"
            _strSQL += " @BabitRegNumber = '" & BabitRegNumber & "'"

            Return m_BabitHeaderMapper.RetrieveDataSet(_strSQL)
        End Function
#End Region

        Function UpdateTransaction(ByVal arrCheckedHeader As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrCheckedHeader.Count > 0 Then
                        For Each oBabitHeader As BabitHeader In arrCheckedHeader
                            oBabitHeader.BabitStatus = 4
                            m_TransactionManager.AddUpdate(oBabitHeader, m_userPrincipal.Identity.Name)
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

        Public Function DoRetrieveSP(ByVal strSql As String) As ArrayList
            Return m_BabitHeaderMapper.RetrieveSP(strSql)
        End Function

    End Class

End Namespace

