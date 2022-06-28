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
Imports System.Collections.Generic
Imports System.Linq

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Training
    Public Class TrTraineeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_TrTraineeMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DataTrainingWajib As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_TrTraineeMapper = MapperFactory.GetInstance().GetMapper(GetType(TrTrainee).ToString)
            Me.m_DataTrainingWajib = MapperFactory.GetInstance().GetMapper(GetType(DataTrainingWajib).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrTrainee
            Return CType(m_TrTraineeMapper.Retrieve(ID), TrTrainee)
        End Function



        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrTraineeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrTraineeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrTraineeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTrainee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrTraineeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTrainee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrTraineeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrTrainee As ArrayList = m_TrTraineeMapper.RetrieveByCriteria(criterias)
            Return _TrTrainee
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrTraineeColl As ArrayList = m_TrTraineeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrTraineeColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTrainee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrTraineeColl As ArrayList = m_TrTraineeMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrTraineeColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTrainee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_TrTraineeMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTrainee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrTraineeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrTraineeColl As ArrayList = m_TrTraineeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrTraineeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrTrainee), columnName, matchOperator, columnValue))
            Dim TrTraineeColl As ArrayList = m_TrTraineeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrTraineeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrTrainee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), columnName, matchOperator, columnValue))

            Return m_TrTraineeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function GetDataTrainingWajib(ByVal traineeID As Integer, Optional ByVal areaID As Integer = 2) As ArrayList
            Dim arrParam As ArrayList = New ArrayList()
            Dim param As SqlClient.SqlParameter = New SqlClient.SqlParameter("@TraineeID", traineeID)
            arrParam.Add(param)
            Dim param2 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@AreaID", areaID)
            arrParam.Add(param2)

            Return m_DataTrainingWajib.RetrieveSP("sp_GetStatusKelulusan", arrParam)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As TrTrainee) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TrTraineeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TrTrainee, Optional ByVal AreaId As Integer = 0) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If AreaId <> 0 Then
                        Dim existingTTH As TrTraineeSalesmanHeader = objDomain.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) x.JobPositionAreaID = AreaId)
                        If Not existingTTH Is Nothing Then
                            existingTTH.Status = objDomain.Status
                            m_TransactionManager.AddUpdate(existingTTH, m_userPrincipal.Identity.Name)
                        End If
                    End If

                    Dim dataLog As TrTraineeDataLog = MappingLogFromOldTrTrainee(objDomain)

                    m_TransactionManager.AddInsert(dataLog, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                  

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
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
        End Function

        Public Sub Delete(ByVal objDomain As TrTrainee)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TrTraineeMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As TrTrainee) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_TrTraineeMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function ValidateCode(ByVal strTrTraineeCode As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "EvaluationCode", MatchType.Exact, strTrTraineeCode))
            Dim agg As Aggregate = New Aggregate(GetType(TrTrainee), "EvaluationCode", AggregateType.Count)

            Return CType(m_TrTraineeMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateTrainee(ByVal objDomain As TrTrainee) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(TrTrainee), "Name", MatchType.Exact, objDomain.Name))
            crit.opAnd(New Criteria(GetType(TrTrainee), "Dealer.ID", MatchType.Exact, objDomain.Dealer.ID))
            crit.opAnd(New Criteria(GetType(TrTrainee), "StartWorkingDate", MatchType.GreaterOrEqual, GenerateDateCriteria(objDomain.StartWorkingDate, True)))
            crit.opAnd(New Criteria(GetType(TrTrainee), "StartWorkingDate", MatchType.LesserOrEqual, GenerateDateCriteria(objDomain.StartWorkingDate, False)))

            Dim agg As Aggregate = New Aggregate(GetType(TrTrainee), "Name", AggregateType.Count)

            Return CType(m_TrTraineeMapper.RetrieveScalar(agg, crit), Integer)
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

        Public Function GetTrTraineeByKTP(ByVal noKTP As String) As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(TrTrainee), "NoKTP", MatchType.Exact, noKTP))
            Return m_TrTraineeMapper.RetrieveByCriteria(crit)
        End Function

        Public Function IsTraineeInDealer(ByVal traineeId As Integer, ByVal dealerId As Integer) As Boolean

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(TrTrainee), "ID", MatchType.Exact, traineeId))
            crit.opAnd(New Criteria(GetType(TrTrainee), "Dealer.ID", MatchType.Exact, dealerId))

            Dim arlResult As ArrayList = m_TrTraineeMapper.RetrieveByCriteria(crit)

            If arlResult.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

#End Region

        Private Function MappingLogFromOldTrTrainee(objDomain As TrTrainee) As TrTraineeDataLog
            Dim log As New TrTraineeDataLog
            Dim oldData As TrTrainee = Me.Retrieve(objDomain.ID)
            log.TrTrainee = oldData
            log.Name = oldData.Name
            log.SalesmanHeader = oldData.SalesmanHeader
            log.Dealer = oldData.Dealer
            log.DealerBranch = oldData.DealerBranch
            log.BirthDate = oldData.BirthDate
            log.Gender = oldData.Gender
            log.NoKTP = oldData.NoKTP
            log.Email = oldData.Email
            log.StartWorkingDate = oldData.StartWorkingDate
            log.Status = oldData.Status
            log.JobPositionCode = oldData.JobPosition
            log.Photo = oldData.Photo
            log.ShirtSize = oldData.ShirtSize
            log.RowStatus = 0

            Return log
        End Function

    End Class

End Namespace
