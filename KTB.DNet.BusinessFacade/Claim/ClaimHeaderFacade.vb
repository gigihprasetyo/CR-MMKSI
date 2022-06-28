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
'// Generated on 7/16/2007 - 11:06:38 AM
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
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Claim

    Public Class ClaimHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ClaimHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ClaimHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(ClaimHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ClaimHeader
            Return CType(m_ClaimHeaderMapper.Retrieve(ID), ClaimHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As ClaimHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ClaimHeader), "ClaimHeaderCode", MatchType.Exact, Code))

            Dim ClaimHeaderColl As ArrayList = m_ClaimHeaderMapper.RetrieveByCriteria(criterias)
            If (ClaimHeaderColl.Count > 0) Then
                Return CType(ClaimHeaderColl(0), ClaimHeader)
            End If
            Return New ClaimHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ClaimHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ClaimHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ClaimHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ClaimHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ClaimHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ClaimHeader As ArrayList = m_ClaimHeaderMapper.RetrieveByCriteria(criterias)
            Return _ClaimHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ClaimHeaderColl As ArrayList = m_ClaimHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ClaimHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ClaimHeaderColl As ArrayList = m_ClaimHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ClaimHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ClaimHeaderColl As ArrayList = m_ClaimHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ClaimHeader), columnName, matchOperator, columnValue))
            Return ClaimHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), columnName, matchOperator, columnValue))

            Return m_ClaimHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimHeaderColl As ArrayList = m_ClaimHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ClaimHeaderColl
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As ClaimHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ClaimHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub DeleteFromDB(ByVal objDomain As ClaimHeader)
            Try
                m_ClaimHeaderMapper.Delete(objDomain)

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteTransaction(ByVal objDomain As ClaimHeader)
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    For Each item As ClaimDetail In objDomain.ClaimDetails
                        m_TransactionManager.AddDelete(item)
                    Next

                    For Each item As ClaimStatusHistory In objDomain.ClaimStatusHistorys
                        m_TransactionManager.AddDelete(item)
                    Next

                    m_TransactionManager.AddDelete(objDomain)

                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
        End Sub


        Public Function Update(ByVal objDomain As ClaimHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ClaimHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateClaimHeader(ByVal arrClaimHeader As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrClaimHeader.Count > 0 Then
                        For Each objCH As ClaimHeader In arrClaimHeader
                            For Each items As ClaimDetail In objCH.ClaimDetails
                                m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)
                            Next
                            If objCH.DeliveryDate < New Date(1900, 1, 1) Then
                                objCH.DeliveryDate = New Date(1900, 1, 1)
                            End If
                            m_TransactionManager.AddUpdate(objCH, m_userPrincipal.Identity.Name)
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

        Public Function UpdateClaimHeader(ByVal objHeader As ClaimHeader, ByVal objHistory As ClaimStatusHistory) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If Not IsNothing(objHistory) Then
                        m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                    End If
                    For Each items As ClaimDetail In objHeader.ClaimDetails
                        m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)
                    Next
                    If objHeader.DeliveryDate < New Date(1900, 1, 1) Then
                        objHeader.DeliveryDate = New Date(1900, 1, 1)
                    End If
                    m_TransactionManager.AddUpdate(objHeader, m_userPrincipal.Identity.Name)

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

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "ClaimHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ClaimHeader), "ClaimHeaderCode", AggregateType.Count)
            Return CType(m_ClaimHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function GetBlankProgressID() As Integer
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "ClaimProgress.Progress", MatchType.Exact, ""))
            Dim arlProgress As ArrayList = m_ClaimHeaderMapper.RetrieveByCriteria(crits)
            Dim objClaimHeader As ClaimHeader = arlProgress(0)

            Return objClaimHeader.ClaimProgress.ID
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is ClaimStatusHistory) Then
                CType(InsertArg.DomainObject, ClaimStatusHistory).ID = InsertArg.ID
                CType(InsertArg.DomainObject, ClaimStatusHistory).MarkLoaded()
                'ElseIf (TypeOf InsertArg.DomainObject Is ClaimDetail) Then
                '    CType(InsertArg.DomainObject, ClaimDetail).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

