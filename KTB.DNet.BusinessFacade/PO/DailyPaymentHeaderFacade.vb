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

Namespace KTB.DNet.BusinessFacade.PO
    Public Class DailyPaymentHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_DailyPaymentHeaderMapper As IMapper
        Private m_DailyPaymentMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_TransactionManager = New TransactionManager
            Me.m_DailyPaymentHeaderMapper = MapperFactory.GetInstance().GetMapper(GetType(DailyPaymentHeader).ToString)
            Me.m_DailyPaymentMapper = MapperFactory.GetInstance().GetMapper(GetType(DailyPayment).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DailyPaymentHeader
            Return CType(m_DailyPaymentHeaderMapper.Retrieve(ID), DailyPaymentHeader)
        End Function

        Public Function Retrieve(ByVal DailyPaymentHeaderNumber As String) As DailyPaymentHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPaymentHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DailyPaymentHeader), "RegNumber", MatchType.Exact, DailyPaymentHeaderNumber))

            Dim DailyPaymentHeaderColl As ArrayList = m_DailyPaymentHeaderMapper.RetrieveByCriteria(criterias)
            If (DailyPaymentHeaderColl.Count > 0) Then
                Return CType(DailyPaymentHeaderColl(0), DailyPaymentHeader)
            End If
            Return New DailyPaymentHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DailyPaymentHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DailyPaymentHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DailyPaymentHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DailyPaymentHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DailyPaymentHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DailyPaymentHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DailyPaymentHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPaymentHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("DailyPaymentHeaderCode")) Then
                sortColl.Add(New Sort(GetType(DailyPaymentHeader), "DailyPaymentHeaderCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _DailyPaymentHeader As ArrayList = m_DailyPaymentHeaderMapper.RetrieveByCriteria(criterias, sortColl)
            Return _DailyPaymentHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPaymentHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DailyPaymentHeaderColl As ArrayList = m_DailyPaymentHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DailyPaymentHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DailyPaymentHeaderColl As ArrayList = m_DailyPaymentHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DailyPaymentHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPaymentHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DailyPaymentHeader), columnName, matchOperator, columnValue))
            Dim DailyPaymentHeaderColl As ArrayList = m_DailyPaymentHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DailyPaymentHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DailyPaymentHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPaymentHeader), columnName, matchOperator, columnValue))

            Return m_DailyPaymentHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPaymentHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DailyPaymentHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DailyPaymentHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function InsertByDP(ByVal oDP As DailyPayment) As Integer
            Dim oDPH As New dailyPaymentHeader
            oDPH.DailyPayments.Add(oDP)
            Return Me.Insert(oDPH)
        End Function

        Public Function Insert(ByVal objDomain As DailyPaymentHeader) As Integer
            Dim ID As Integer

            ID = InsertDPH(objDomain)
            If ID > 0 Then
                objDomain.ID = ID
                For Each oDP As DailyPayment In objDomain.DailyPayments
                    oDP.DailyPaymentHeader = objDomain
                    oDP.ID = m_DailyPaymentMapper.Insert(oDP, m_userPrincipal.Identity.Name)
                Next
            End If
            Return ID

            Exit Function
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If m_userPrincipal.Identity.Name = "" Then
                        _user = "SAP"
                    Else
                        _user = m_userPrincipal.Identity.Name
                    End If
                    For Each item As DailyPayment In objDomain.DailyPayments
                        item.DailyPaymentHeader = objDomain
                        'item.DailyPaymentHeader.MarkLoaded()
                        m_TransactionManager.AddInsert(item, _user)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
                    End If
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

            'Dim iReturn As Integer = -2
            'Try
            '    iReturn = m_DailyPaymentHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            'Catch ex As Exception
            '    Dim s As String = ex.Message
            '    iReturn = -1
            'End Try
            'Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DailyPaymentHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DailyPaymentHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DailyPaymentHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DailyPaymentHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DailyPaymentHeader)
            Try
                m_DailyPaymentHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal DailyPaymentHeaderNumber As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPaymentHeader), "DailyPaymentHeaderNumber", MatchType.Exact, DailyPaymentHeaderNumber))
            Dim agg As Aggregate = New Aggregate(GetType(DailyPaymentHeader), "DailyPaymentHeaderNumber", AggregateType.Count)

            Return CType(m_DailyPaymentHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DailyPaymentHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DailyPaymentHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DailyPaymentHeaderColl As ArrayList = m_DailyPaymentHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DailyPaymentHeaderColl
        End Function

        Private Function InsertDPH(ByVal objDomain As DailyPaymentHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DailyPaymentHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function
#End Region

    End Class

End Namespace
