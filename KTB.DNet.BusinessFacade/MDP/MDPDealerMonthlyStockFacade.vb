
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
'// Generated on 21/11/2018 - 10:46:08
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

    Public Class MDPDealerMonthlyStockFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MDPDealerMonthlyStockMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MDPDealerMonthlyStockMapper = MapperFactory.GetInstance.GetMapper(GetType(MDPDealerMonthlyStock).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MDPDealerMonthlyStock
            Return CType(m_MDPDealerMonthlyStockMapper.Retrieve(ID), MDPDealerMonthlyStock)
        End Function

        Public Function Retrieve(ByVal Code As String) As MDPDealerMonthlyStock
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerMonthlyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), "MDPDealerMonthlyStockCode", MatchType.Exact, Code))

            Dim MDPDealerMonthlyStockColl As ArrayList = m_MDPDealerMonthlyStockMapper.RetrieveByCriteria(criterias)
            If (MDPDealerMonthlyStockColl.Count > 0) Then
                Return CType(MDPDealerMonthlyStockColl(0), MDPDealerMonthlyStock)
            End If
            Return New MDPDealerMonthlyStock
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MDPDealerMonthlyStockMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MDPDealerMonthlyStockMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MDPDealerMonthlyStockMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPDealerMonthlyStock), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MDPDealerMonthlyStockMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPDealerMonthlyStock), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MDPDealerMonthlyStockMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerMonthlyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MDPDealerMonthlyStock As ArrayList = m_MDPDealerMonthlyStockMapper.RetrieveByCriteria(criterias)
            Return _MDPDealerMonthlyStock
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerMonthlyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MDPDealerMonthlyStockColl As ArrayList = m_MDPDealerMonthlyStockMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MDPDealerMonthlyStockColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(MDPDealerMonthlyStock), SortColumn, sortDirection))
            Dim MDPDealerMonthlyStockColl As ArrayList = m_MDPDealerMonthlyStockMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MDPDealerMonthlyStockColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MDPDealerMonthlyStockColl As ArrayList = m_MDPDealerMonthlyStockMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MDPDealerMonthlyStockColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerMonthlyStock), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MDPDealerMonthlyStockColl As ArrayList = m_MDPDealerMonthlyStockMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MDPDealerMonthlyStock), columnName, matchOperator, columnValue))
            Return MDPDealerMonthlyStockColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPDealerMonthlyStock), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerMonthlyStock), columnName, matchOperator, columnValue))

            Return m_MDPDealerMonthlyStockMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPDealerMonthlyStock), "MDPDealerMonthlyStockCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MDPDealerMonthlyStock), "MDPDealerMonthlyStockCode", AggregateType.Count)
            Return CType(m_MDPDealerMonthlyStockMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function InserOrUpdatetWithTransactionManager(ByVal mDPPeriodStockList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            If Me.IsTaskFree Then
                Me.SetTaskLocking()
                Try
                    For Each mDPDealerMonthlyStock As MDPDealerMonthlyStock In mDPPeriodStockList
                        If mDPDealerMonthlyStock.ID = 0 Then
                            Me.m_TransactionManager.AddInsert(mDPDealerMonthlyStock, m_userPrincipal.Identity.Name)
                        Else
                            If (mDPDealerMonthlyStock.LastUpdatedBy.ToLower <> "not update") Then
                                Me.m_TransactionManager.AddUpdate(mDPDealerMonthlyStock, m_userPrincipal.Identity.Name)
                            End If
                            mDPDealerMonthlyStock.MarkLoaded()
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

        Public Function Insert(ByVal objDomain As MDPDealerMonthlyStock) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_MDPDealerMonthlyStockMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As MDPDealerMonthlyStock) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MDPDealerMonthlyStockMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As MDPDealerMonthlyStock)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_MDPDealerMonthlyStockMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MDPDealerMonthlyStock)
            Try
                m_MDPDealerMonthlyStockMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function RetrieveFromSP(ByVal PeriodMonth As Short, ByVal PeriodYear As Short, ByVal DealerID As Short, ByVal VehicleColorID As Short, ByVal ProductionYear As Short) As DataSet
            Dim _SQL As String = "EXEC [up_RetrieveMDPDealerRemainingStock]"
            _SQL += " @PeriodMonth = '" & PeriodMonth & "',"
            _SQL += " @PeriodYear = '" & PeriodYear & "',"
            _SQL += " @DealerID = '" & DealerID & "',"
            _SQL += " @VehicleColorID = '" & VehicleColorID & "',"
            _SQL += " @ProductionYear = '" & ProductionYear & "'"

            Return m_MDPDealerMonthlyStockMapper.RetrieveDataSet(_SQL)
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

