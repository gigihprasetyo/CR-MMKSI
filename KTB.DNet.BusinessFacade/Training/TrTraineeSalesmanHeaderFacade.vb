
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

Namespace KTB.DNet.BusinessFacade.Training
    Public Class TrTraineeSalesmanHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_TrTraineeSalesmanHeaderMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DataTrainingWajib As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_TrTraineeSalesmanHeaderMapper = MapperFactory.GetInstance().GetMapper(GetType(TrTraineeSalesmanHeader).ToString)
            Me.m_DataTrainingWajib = MapperFactory.GetInstance().GetMapper(GetType(DataTrainingWajib).ToString)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrTraineeSalesmanHeader
            Return CType(m_TrTraineeSalesmanHeaderMapper.Retrieve(ID), TrTraineeSalesmanHeader)
        End Function



        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrTraineeSalesmanHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrTraineeSalesmanHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrTraineeSalesmanHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeSalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrTraineeSalesmanHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeSalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrTraineeSalesmanHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrTraineeSalesmanHeader As ArrayList = m_TrTraineeSalesmanHeaderMapper.RetrieveByCriteria(criterias)
            Return _TrTraineeSalesmanHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrTraineeSalesmanHeaderColl As ArrayList = m_TrTraineeSalesmanHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrTraineeSalesmanHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeSalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrTraineeSalesmanHeaderColl As ArrayList = m_TrTraineeSalesmanHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrTraineeSalesmanHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeSalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_TrTraineeSalesmanHeaderMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeSalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrTraineeSalesmanHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrTraineeSalesmanHeaderColl As ArrayList = m_TrTraineeSalesmanHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrTraineeSalesmanHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), columnName, matchOperator, columnValue))
            Dim TrTraineeSalesmanHeaderColl As ArrayList = m_TrTraineeSalesmanHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrTraineeSalesmanHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTraineeSalesmanHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), columnName, matchOperator, columnValue))

            Return m_TrTraineeSalesmanHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function GetDataTrainingWajib(ByVal traineeID As Integer) As ArrayList
            Dim arrParam As ArrayList = New ArrayList()
            Dim param As SqlClient.SqlParameter = New SqlClient.SqlParameter("@TraineeID", traineeID)
            arrParam.Add(param)

            Return m_DataTrainingWajib.RetrieveSP("sp_GetStatusKelulusan", arrParam)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As TrTraineeSalesmanHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                m_TrTraineeSalesmanHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TrTraineeSalesmanHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrTraineeSalesmanHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TrTraineeSalesmanHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TrTraineeSalesmanHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As TrTraineeSalesmanHeader) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_TrTraineeSalesmanHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function ValidateCode(ByVal strTrTraineeSalesmanHeaderCode As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), "EvaluationCode", MatchType.Exact, strTrTraineeSalesmanHeaderCode))
            Dim agg As Aggregate = New Aggregate(GetType(TrTraineeSalesmanHeader), "EvaluationCode", AggregateType.Count)

            Return CType(m_TrTraineeSalesmanHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateTrainee(ByVal objDomain As TrTraineeSalesmanHeader) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "TrTrainee.ID", MatchType.Exact, objDomain.TrTrainee.ID))
            crit.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "JobPositionAreaID", MatchType.Exact, objDomain.JobPositionAreaID))

            Dim agg As Aggregate = New Aggregate(GetType(TrTraineeSalesmanHeader), "Name", AggregateType.Count)

            Return CType(m_TrTraineeSalesmanHeaderMapper.RetrieveScalar(agg, crit), Integer)
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

       
#End Region

    End Class

End Namespace
