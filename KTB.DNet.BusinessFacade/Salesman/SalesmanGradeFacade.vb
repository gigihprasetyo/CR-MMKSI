
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

Namespace KTB.DNet.BusinessFacade
    Public Class SalesmanGradeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_SalesmanGradeMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SalesmanGradeMapper = MapperFactory.GetInstance().GetMapper(GetType(SalesmanGrade).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SalesmanGrade
            Return CType(m_SalesmanGradeMapper.Retrieve(ID), SalesmanGrade)
        End Function



        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesmanGradeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SalesmanGradeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SalesmanGradeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanGrade), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanGradeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanGrade), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanGradeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanGrade), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SalesmanGrade As ArrayList = m_SalesmanGradeMapper.RetrieveByCriteria(criterias)
            Return _SalesmanGrade
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanGrade), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanGradeColl As ArrayList = m_SalesmanGradeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesmanGradeColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanGrade), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SalesmanGradeColl As ArrayList = m_SalesmanGradeMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SalesmanGradeColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanGrade), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_SalesmanGradeMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanGrade), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanGrade), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanGradeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SalesmanGradeColl As ArrayList = m_SalesmanGradeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesmanGradeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanGrade), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanGrade), columnName, matchOperator, columnValue))
            Dim SalesmanGradeColl As ArrayList = m_SalesmanGradeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesmanGradeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanGrade), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanGrade), columnName, matchOperator, columnValue))

            Return m_SalesmanGradeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As SalesmanGrade) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SalesmanGradeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SalesmanGrade) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SalesmanGradeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SalesmanGrade)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SalesmanGradeMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SalesmanGrade) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SalesmanGradeMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function ValidateCode(ByVal strSalesmanGradeCode As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanGrade), "EvaluationCode", MatchType.Exact, strSalesmanGradeCode))
            Dim agg As Aggregate = New Aggregate(GetType(SalesmanGrade), "EvaluationCode", AggregateType.Count)

            Return CType(m_SalesmanGradeMapper.RetrieveScalar(agg, crit), Integer)
        End Function


#End Region

#Region "Custom Method"
        Private Function GenerateDateCriteria(ByVal nDate As Date, ByVal startDate As Boolean) As DateTime
            Dim Hour As Integer
            Dim Minute As Integer
            Dim Second As Integer

            If startDate Then
                Hour = 0
                Minute = 0
                Second = 0
            Else
                Hour = 23
                Minute = 59
                Second = 59
            End If

            Return New DateTime(nDate.Year, nDate.Month, nDate.Day, Hour, Minute, Second)
        End Function

        Public Function LastYearGrade(ByVal salesmanCode As String) As SalesmanGrade
            Dim month As Integer = DateTime.Now.Month
            Dim year As Integer = DateTime.Now.Year

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanGrade), "RowStatus", _
                                                    MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanGrade), "SalesmanHeader.SalesmanCode", MatchType.Exact, salesmanCode))
            If month < 4 Then
                criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Year", MatchType.Exact, year - 2))
            Else
                criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Year", MatchType.Exact, year - 1))
            End If

            Dim arrEGrade As ArrayList = New StandardCodeFacade(m_userPrincipal).RetrieveByCategory("GradePeriode")
            Dim periodeMax As Integer = 0
            For Each i As StandardCode In arrEGrade
                If i.ValueId > periodeMax Then
                    periodeMax = i.ValueId
                End If
            Next
            criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Period", MatchType.Exact, periodeMax))

            Dim arrGrade As ArrayList = m_SalesmanGradeMapper.RetrieveByCriteria(criterias)
            If arrGrade.Count > 0 Then
                Return CType(arrGrade(0), SalesmanGrade)
            End If

            Return Nothing
        End Function

        Public Function LastGrade(ByVal salesmanCode As String) As SalesmanGrade
            Dim month As Integer = DateTime.Now.Month
            Dim year As Integer = DateTime.Now.Year

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanGrade), "RowStatus", _
                                                    MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanGrade), "SalesmanHeader.SalesmanCode", MatchType.Exact, salesmanCode))
            If month < 4 Then
                criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Year", MatchType.Exact, year - 1))
                criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Period", MatchType.Exact, 3))

            ElseIf month < 7 And month > 3 Then
                criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Year", MatchType.Exact, year - 1))
                criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Period", MatchType.Exact, 4))

            ElseIf month < 10 And month > 6 Then
                criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Year", MatchType.Exact, year))
                criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Period", MatchType.Exact, 1))

            Else
                criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Year", MatchType.Exact, year))
                criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Period", MatchType.Exact, 2))
            End If

            Dim arrGrade As ArrayList = m_SalesmanGradeMapper.RetrieveByCriteria(criterias)
            If arrGrade.Count > 0 Then
                Return CType(arrGrade(0), SalesmanGrade)
            End If

            Return Nothing
        End Function

        Public Function isAlready(ByVal salesmanCode As String, ByVal year As Integer, ByVal periode As Integer) As Boolean
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanGrade), "RowStatus", _
                                                    MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanGrade), "SalesmanHeader.SalesmanCode", MatchType.Exact, salesmanCode))
            criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Year", MatchType.Exact, year))
            criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Period", MatchType.Exact, periode))

            Dim arrGrade As ArrayList = m_SalesmanGradeMapper.RetrieveByCriteria(criterias)
            If arrGrade.Count > 0 Then
                Return True
            End If

            Return False
        End Function

        Public Function GradeByPeriod(ByVal salesmanCode As String, ByVal year As Integer, ByVal periode As Integer) As SalesmanGrade
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanGrade), "RowStatus", _
                                                    MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanGrade), "SalesmanHeader.SalesmanCode", MatchType.Exact, salesmanCode))
            criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Year", MatchType.Exact, year))
            criterias.opAnd(New Criteria(GetType(SalesmanGrade), "Period", MatchType.Exact, periode))

            Dim arrGrade As ArrayList = m_SalesmanGradeMapper.RetrieveByCriteria(criterias)
            If arrGrade.Count > 0 Then
                Return CType(arrGrade(0), SalesmanGrade)
            End If

            Return Nothing
        End Function
#End Region

    End Class

End Namespace
