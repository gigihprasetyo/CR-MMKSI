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
'// Generated on 11/14/2005 - 10:42:45 AM
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

Namespace KTB.DNet.BusinessFacade.Training
    Public Class TrBillingDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrBillingDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_TrBillingDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(TrBillingDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrBillingDetail
            Return CType(m_TrBillingDetailMapper.Retrieve(ID), TrBillingDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As TrBillingDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBillingDetail), "TrBillingDetailCode", MatchType.Exact, Code))

            Dim TrBillingDetailColl As ArrayList = m_TrBillingDetailMapper.RetrieveByCriteria(criterias)
            If (TrBillingDetailColl.Count > 0) Then
                Return CType(TrBillingDetailColl(0), TrBillingDetail)
            End If
            Return New TrBillingDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrBillingDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrBillingDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrBillingDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrBillingDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrBillingDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrBillingDetail As ArrayList = m_TrBillingDetailMapper.RetrieveByCriteria(criterias)
            Return _TrBillingDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrBillingDetailColl As ArrayList = m_TrBillingDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrBillingDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrClassColl As ArrayList = m_TrBillingDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return TrClassColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim TrBillingDetailColl As ArrayList = m_TrBillingDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrBillingDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim TrBillingDetailColl As ArrayList = m_TrBillingDetailMapper.RetrieveByCriteria(criterias, sortColl)
            Return TrBillingDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrBillingDetailColl As ArrayList = m_TrBillingDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrBillingDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrBillingDetailColl As ArrayList = m_TrBillingDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrBillingDetail), columnName, matchOperator, columnValue))
            Return TrBillingDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), columnName, matchOperator, columnValue))

            Return m_TrBillingDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByBookingID(ByVal bookingID As Integer) As TrBillingDetail
            Dim ClassAllocationColl As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBillingDetail), "TrBookingCourse.ID", MatchType.Exact, bookingID))
            ClassAllocationColl = m_TrBillingDetailMapper.RetrieveByCriteria(criterias)
            If ClassAllocationColl.Count > 0 Then
                Return CType(ClassAllocationColl(0), TrBillingDetail)
            End If
            Return New TrBillingDetail()
        End Function

        Public Function RetrieveScalar(ByVal Criterias As ICriteria, ByVal aggregate As Aggregate) As Integer
            Dim obj As Object = m_TrBillingDetailMapper.RetrieveScalar(aggregate, Criterias)
            If obj Is DBNull.Value Then
                Return 0
            Else
                Return CInt(obj)
            End If
        End Function

        Public Function IsValidTagihan(ByVal dealerID As Integer) As Boolean
            Try
                Dim arrParam As ArrayList = New ArrayList()
                Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@DealerID", dealerID)
                arrParam.Add(param1)

                Dim obj As DataTable = m_TrBillingDetailMapper.RetrieveDataSet("GetDataSiswaBerbayar", arrParam).Tables(0)
                If CInt(obj.Rows(0)(0)) = 1 Then
                    Return True
                End If

                Return False
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal classCode As String, ByVal dealerCode As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "TrClass.ClassCode", MatchType.Exact, classCode))
            crit.opAnd(New Criteria(GetType(TrBillingDetail), "Dealer.DealerCode", MatchType.Exact, dealerCode))

            Dim agg As Aggregate = New Aggregate(GetType(TrBillingDetail), "ID", AggregateType.Count)

            Return CType(m_TrBillingDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TrBillingDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                m_TrBillingDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As TrBillingDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrBillingDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As TrBillingDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrBillingDetailMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Function GetAggregateResult(ByVal aggregate As IAggregate, ByVal criteria As ICriteria) As Decimal
            Dim result As Object = m_TrBillingDetailMapper.RetrieveScalar(aggregate, criteria)
            If result Is System.DBNull.Value Then
                Return 0
            Else
                Return CType(result, Decimal)
            End If
        End Function

#End Region

#Region "Custom Method"
        Public Function RetrieveTrBillingDetailByTraineeID(ByVal traineeID As Integer) As ArrayList
            Dim SQL As String
            If traineeID > 0 Then
                SQL = "exec sp_GetClassAllocationByTrainee " & traineeID
            End If

            Return m_TrBillingDetailMapper.RetrieveSP(SQL)
        End Function
#End Region

    End Class

End Namespace



