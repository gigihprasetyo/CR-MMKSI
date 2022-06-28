
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
'// Generated on 03/10/2019 - 14:05:27
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
Imports System.Collections.Generic


#End Region


Namespace KTB.DNet.BusinessFacade
    Public Class BabitMarketingBoxFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitMarketingBoxMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitMarketingBoxMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitMarketingBox).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Long) As BabitMarketingBox
            Return CType(m_BabitMarketingBoxMapper.Retrieve(ID), BabitMarketingBox)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitMarketingBox
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMarketingBox), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitMarketingBox), "BabitMarketingBoxCode", MatchType.Exact, Code))

            Dim BabitMarketingBoxColl As ArrayList = m_BabitMarketingBoxMapper.RetrieveByCriteria(criterias)
            If (BabitMarketingBoxColl.Count > 0) Then
                Return CType(BabitMarketingBoxColl(0), BabitMarketingBox)
            End If
            Return New BabitMarketingBox
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitMarketingBoxMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitMarketingBoxMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitMarketingBoxMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitMarketingBox), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitMarketingBoxMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitMarketingBox), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitMarketingBoxMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMarketingBox), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitMarketingBox As ArrayList = m_BabitMarketingBoxMapper.RetrieveByCriteria(criterias)
            Return _BabitMarketingBox
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMarketingBox), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitMarketingBoxColl As ArrayList = m_BabitMarketingBoxMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitMarketingBoxColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitMarketingBox), SortColumn, sortDirection))
            Dim BabitMarketingBoxColl As ArrayList = m_BabitMarketingBoxMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitMarketingBoxColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitMarketingBoxColl As ArrayList = m_BabitMarketingBoxMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitMarketingBoxColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMarketingBox), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitMarketingBoxColl As ArrayList = m_BabitMarketingBoxMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitMarketingBox), columnName, matchOperator, columnValue))
            Return BabitMarketingBoxColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitMarketingBox), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMarketingBox), columnName, matchOperator, columnValue))

            Return m_BabitMarketingBoxMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMarketingBox), "BabitMarketingBoxCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitMarketingBox), "BabitMarketingBoxCode", AggregateType.Count)
            Return CType(m_BabitMarketingBoxMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitMarketingBox) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitMarketingBoxMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitMarketingBox) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitMarketingBoxMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitMarketingBox)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitMarketingBoxMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitMarketingBox)
            Try
                m_BabitMarketingBoxMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function getLastModifiedDateFile(dealercode As String, babittype As String) As ArrayList
            Dim strQuery As String = String.Format("Exec getLastModifiedDateFile @dealercode='{0}' , @babittype = '{1}'", dealercode, babittype)
            Dim arr As ArrayList = New ArrayList
            Return m_BabitMarketingBoxMapper.RetrieveSP(strQuery)
        End Function

        Public Function InsertMarbox(strQuery As String) As Boolean
            Dim arr As ArrayList
            arr = m_BabitMarketingBoxMapper.RetrieveSP(strQuery)
            If arr IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        End Function


        Public Function InsertMarbox2(SubmissionID As String, StartDate As DateTime, EndDate As DateTime, NamaEvent As String, TipeEvent As String, DealerCode As String, EventLocation As String, FileTimeLastModified As DateTime) As Boolean
            Dim arr As ArrayList
            Dim strQuery As String = "InsertMarboxFromSharePoint"

            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@submissionID ", SubmissionID))
            Param.Add(New SqlClient.SqlParameter("@StartDate", StartDate))
            Param.Add(New SqlClient.SqlParameter("@EndDate ", EndDate))
            Param.Add(New SqlClient.SqlParameter("@EventName", NamaEvent))
            Param.Add(New SqlClient.SqlParameter("@TipeEvent ", TipeEvent))
            Param.Add(New SqlClient.SqlParameter("@DealerCode", DealerCode))
            Param.Add(New SqlClient.SqlParameter("@EventLocation ", EventLocation))
            Param.Add(New SqlClient.SqlParameter("@FileTimeLastModified", FileTimeLastModified))



            arr = m_BabitMarketingBoxMapper.RetrieveSP(strQuery, New ArrayList(Param))
            If arr IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        End Function
#End Region

    End Class

End Namespace

