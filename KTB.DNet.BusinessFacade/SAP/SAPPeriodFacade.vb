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
'// Generated on 9/5/2007 - 4:20:56 PM
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

Namespace KTB.DNet.BusinessFacade.SAP

    Public Class SAPPeriodFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SAPPeriodMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SAPPeriodMapper = MapperFactory.GetInstance.GetMapper(GetType(SAPPeriod).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SAPPeriod
            Return CType(m_SAPPeriodMapper.Retrieve(ID), SAPPeriod)
        End Function

        Public Function Retrieve(ByVal Code As String) As SAPPeriod
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SAPPeriod), "SAPNumber", MatchType.Exact, Code))

            Dim SAPPeriodColl As ArrayList = m_SAPPeriodMapper.RetrieveByCriteria(criterias)
            If (SAPPeriodColl.Count > 0) Then
                Return CType(SAPPeriodColl(0), SAPPeriod)
            End If
            Return New SAPPeriod
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SAPPeriodMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SAPPeriodMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SAPPeriodMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPPeriod), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SAPPeriodMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPPeriod), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SAPPeriodMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SAPPeriod As ArrayList = m_SAPPeriodMapper.RetrieveByCriteria(criterias)
            Return _SAPPeriod
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SAPPeriodColl As ArrayList = m_SAPPeriodMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SAPPeriodColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(SAPPeriod), SortColumn, sortDirection))

            Dim SAPPeriodColl As ArrayList = m_SAPPeriodMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SAPPeriodColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SAPPeriodColl As ArrayList = m_SAPPeriodMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SAPPeriodColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SAPPeriodColl As ArrayList = m_SAPPeriodMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SAPPeriod), columnName, matchOperator, columnValue))
            Return SAPPeriodColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPPeriod), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPPeriod), columnName, matchOperator, columnValue))

            Return m_SAPPeriodMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection)
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPPeriod), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SAPPeriodColl As ArrayList = m_SAPPeriodMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SAPPeriodColl
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPPeriod), "SAPPeriodCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SAPPeriod), "SAPPeriodCode", AggregateType.Count)
            Return CType(m_SAPPeriodMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateCode(ByVal Code As String, ByVal IdEdit As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPPeriod), "SAPNumber", MatchType.Exact, Code))
            If IdEdit <> 0 Then

                crit.opAnd(New Criteria(GetType(SAPPeriod), "ID", MatchType.No, IdEdit))

            End If
            Dim agg As Aggregate = New Aggregate(GetType(SAPPeriod), "SAPNumber", AggregateType.Count)
            Return CType(m_SAPPeriodMapper.RetrieveScalar(agg, crit), Integer)
        End Function
        Public Function Insert(ByVal objDomain As SAPPeriod) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SAPPeriodMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function
        Public Function InsertSAPPeriod(ByVal objDomain As KTB.DNet.Domain.SAPPeriod, ByVal arrSAPRegister As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If arrSAPRegister.Count > 0 Then
                        For Each objSAPRegister As SAPRegister In arrSAPRegister
                            objSAPRegister.SAPPeriod = objDomain
                            m_TransactionManager.AddInsert(objSAPRegister, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SAPPeriod) Then
                CType(InsertArg.DomainObject, SAPPeriod).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SAPPeriod).MarkLoaded()
            End If
        End Sub

        Public Function Update(ByVal objDomain As SAPPeriod) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SAPPeriodMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As SAPPeriod)
            Try
                m_SAPPeriodMapper.Delete(objDomain)
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

