
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
'// Copyright  2018
'// ---------------------
'// $History      : $
'// Generated on 27/11/2018 - 10:01:05
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

Namespace KTB.DNET.BusinessFacade.MDP

    Public Class MDPDealerDailyStockFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MDPDealerDailyStockMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MDPDealerDailyStockMapper = MapperFactory.GetInstance.GetMapper(GetType(MDPDealerDailyStock).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MDPDealerDailyStock
            Return CType(m_MDPDealerDailyStockMapper.Retrieve(ID), MDPDealerDailyStock)
        End Function

        Public Function RetrieveMaxPeriodeDate(ByVal endDate As Integer) As ArrayList 'MDPDealerDailyStock
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerDailyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MDPDealerDailyStock), "PeriodeDate", MatchType.LesserOrEqual, endDate))

            Dim MDPDealerDailyStockColl As ArrayList = m_MDPDealerDailyStockMapper.RetrieveByCriteria(criterias)
            'If (MDPDealerDailyStockColl.Count > 0) Then
            '    Return CType(MDPDealerDailyStockColl(0), MDPDealerDailyStock)
            'End If
            Return MDPDealerDailyStockColl 'New MDPDealerDailyStock
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MDPDealerDailyStockMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MDPDealerDailyStockMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MDPDealerDailyStockMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPDealerDailyStock), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MDPDealerDailyStockMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPDealerDailyStock), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MDPDealerDailyStockMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerDailyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MDPDealerDailyStock As ArrayList = m_MDPDealerDailyStockMapper.RetrieveByCriteria(criterias)
            Return _MDPDealerDailyStock
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerDailyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MDPDealerDailyStockColl As ArrayList = m_MDPDealerDailyStockMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MDPDealerDailyStockColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(MDPDealerDailyStock), SortColumn, sortDirection))
            Dim MDPDealerDailyStockColl As ArrayList = m_MDPDealerDailyStockMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MDPDealerDailyStockColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MDPDealerDailyStockColl As ArrayList = m_MDPDealerDailyStockMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MDPDealerDailyStockColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerDailyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MDPDealerDailyStockColl As ArrayList = m_MDPDealerDailyStockMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MDPDealerDailyStock), columnName, matchOperator, columnValue))
            Return MDPDealerDailyStockColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPDealerDailyStock), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerDailyStock), columnName, matchOperator, columnValue))

            Return m_MDPDealerDailyStockMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerDailyStock), "MDPDealerDailyStockCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MDPDealerDailyStock), "MDPDealerDailyStockCode", AggregateType.Count)
            Return CType(m_MDPDealerDailyStockMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function InserOrUpdatetWithTransactionManager(ByVal mDPDailyStockList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            If Me.IsTaskFree Then
                Me.SetTaskLocking()
                Try
                    For Each mDPDealerDailyStock As MDPDealerDailyStock In mDPDailyStockList
                        If mDPDealerDailyStock.ID = 0 Then
                            Me.m_TransactionManager.AddInsert(mDPDealerDailyStock, m_userPrincipal.Identity.Name)
                        Else
                            If (mDPDealerDailyStock.LastUpdatedBy.ToLower <> "not update") Then
                                Me.m_TransactionManager.AddUpdate(mDPDealerDailyStock, m_userPrincipal.Identity.Name)
                            End If
                            mDPDealerDailyStock.MarkLoaded()
                        End If
                    Next
                    Me.m_TransactionManager.PerformTransaction()
                    result = 1
                Catch ex As Exception
                    Throw ex
                    result = -1
                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If

            Return result

        End Function

        Public Function Insert(ByVal objDomain As MDPDealerDailyStock) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_MDPDealerDailyStockMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As MDPDealerDailyStock) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MDPDealerDailyStockMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As MDPDealerDailyStock)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_MDPDealerDailyStockMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MDPDealerDailyStock)
            Try
                m_MDPDealerDailyStockMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function RetrieveFromSP(ByVal ProductionYear As Short, ByVal PeriodMonth As Short, ByVal PeriodYear As Short, ByVal DealerID As Short, ByVal QueryWhere As String) As DataSet
            Dim _SQL As String = "EXEC [up_RetrieveListMDPDealerDailyStock]"
            _SQL += " @ProductionYear = '" & ProductionYear & "',"
            _SQL += " @PeriodMonth = '" & PeriodMonth & "',"
            _SQL += " @PeriodYear = '" & PeriodYear & "',"
            _SQL += " @DealerID = '" & DealerID & "',"
            _SQL += " @QueryWhere = '" & QueryWhere & "'"

            Return m_MDPDealerDailyStockMapper.RetrieveDataSet(_SQL)
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

