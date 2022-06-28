
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
'// Generated on 20/06/2019 - 8:47:49
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

Namespace KTB.DNET.BusinessFacade

    Public Class TrBookingCourseFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrBookingCourseMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TrBookingCourseMapper = MapperFactory.GetInstance.GetMapper(GetType(TrBookingCourse).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrBookingCourse
            Return CType(m_TrBookingCourseMapper.Retrieve(ID), TrBookingCourse)
        End Function

        Public Function Retrieve(ByVal Code As String) As TrBookingCourse
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Code", MatchType.Exact, Code))

            Dim TrBookingCourseColl As ArrayList = m_TrBookingCourseMapper.RetrieveByCriteria(criterias)
            If (TrBookingCourseColl.Count > 0) Then
                Return CType(TrBookingCourseColl(0), TrBookingCourse)
            End If
            Return New TrBookingCourse
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrBookingCourseMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrBookingCourseMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrBookingCourseMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBookingCourse), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrBookingCourseMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBookingCourse), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrBookingCourseMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrBookingCourse As ArrayList = m_TrBookingCourseMapper.RetrieveByCriteria(criterias)
            Return _TrBookingCourse
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrBookingCourseColl As ArrayList = m_TrBookingCourseMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrBookingCourseColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(TrBookingCourse), SortColumn, sortDirection))
            Dim TrBookingCourseColl As ArrayList = m_TrBookingCourseMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrBookingCourseColl
        End Function


        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBookingCourse), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrCourseColl As ArrayList = m_TrBookingCourseMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrCourseColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrBookingCourseColl As ArrayList = m_TrBookingCourseMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrBookingCourseColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrBookingCourseColl As ArrayList = m_TrBookingCourseMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrBookingCourse), columnName, matchOperator, columnValue))
            Return TrBookingCourseColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBookingCourse), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBookingCourse), columnName, matchOperator, columnValue))

            Return m_TrBookingCourseMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"
        Public Function GetCourseRegistrationState(ByVal traineeID As Integer, ByVal courseID As Integer, ByVal fiscalYear As String, Optional ByVal startDate As DateTime = Nothing) As String
            Dim arrParam As ArrayList = New ArrayList()
            Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@TraineeID", traineeID)
            arrParam.Add(param1)
            Dim param2 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@CourseID", courseID)
            arrParam.Add(param2)
            Dim param3 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@FiscalYear", fiscalYear)
            arrParam.Add(param3)
            If Not IsNothing(startDate) Then
                Dim param4 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@StartDate", startDate)
                arrParam.Add(param4)
            End If

            Dim dataTable As DataTable = m_TrBookingCourseMapper.RetrieveDataSet("Get_CourseRegistrationState", arrParam).Tables(0)

            Return dataTable.Rows(0)(0).ToString()
        End Function


        Public Function GetSaldoDepositB(ByVal dealerID As Integer, ByVal year As Integer, ByVal productCategoryID As Integer) As Double
            Dim arrParam As ArrayList = New ArrayList()
            Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@DealerID", dealerID)
            arrParam.Add(param1)
            Dim param2 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@Year", year)
            arrParam.Add(param2)
            Dim param3 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@ProductCategoryID", productCategoryID)
            arrParam.Add(param3)
            Dim dataTable As DataTable = m_TrBookingCourseMapper.RetrieveDataSet("Get_SaldoDepositB", arrParam).Tables(0)

            Return Double.Parse(dataTable.Rows(0)(3).ToString())
        End Function

        Public Function Get_SaldoDepositB(ByVal dealerID As Integer, ByVal year As Integer, ByVal productCategoryID As Integer) As SaldoDepositB
            Dim arrParam As ArrayList = New ArrayList()
            Dim result As SaldoDepositB = New SaldoDepositB()

            Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@DealerID", dealerID)
            arrParam.Add(param1)
            Dim param2 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@Year", year)
            arrParam.Add(param2)
            Dim param3 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@ProductCategoryID", productCategoryID)
            arrParam.Add(param3)
            Dim dataTable As DataTable = m_TrBookingCourseMapper.RetrieveDataSet("Get_SaldoDepositB", arrParam).Tables(0)

            If dataTable.Rows.Count > 0 Then
                result.SDepositB = Double.Parse(dataTable.Rows(0)(0).ToString())
                result.Plafon = Double.Parse(dataTable.Rows(0)(1).ToString())
                result.TotalPengajuan = Double.Parse(dataTable.Rows(0)(2).ToString())
                result.SisaSaldo = Double.Parse(dataTable.Rows(0)(3).ToString())
            End If

            Return result
        End Function

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "TrBookingCourseCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TrBookingCourse), "TrBookingCourseCode", AggregateType.Count)
            Return CType(m_TrBookingCourseMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function RetrieveScalar(ByVal crit As ICriteria, agregation As IAggregate) As Integer
            Return CType(m_TrBookingCourseMapper.RetrieveScalar(agregation, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TrBookingCourse) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TrBookingCourseMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TrBookingCourse) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrBookingCourseMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TrBookingCourse)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TrBookingCourseMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TrBookingCourse)
            Try
                m_TrBookingCourseMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function GetAggregateResult(ByVal aggregate As IAggregate, ByVal criteria As ICriteria) As Decimal
            Dim result As Object = m_TrBookingCourseMapper.RetrieveScalar(aggregate, criteria)
            If result Is System.DBNull.Value Then
                Return 0
            Else
                Return CType(result, Decimal)
            End If
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

