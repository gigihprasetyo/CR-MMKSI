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

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Service
Imports System.Collections.Generic
Imports System.Reflection

#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class ServiceStandardTimeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_ServiceStandardTimeMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager



#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ServiceStandardTimeMapper = MapperFactory.GetInstance().GetMapper(GetType(ServiceStandardTime).ToString)
            Me.objTransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ServiceStandardTime
            Return CType(m_ServiceStandardTimeMapper.Retrieve(ID), ServiceStandardTime)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.ServiceStandardTime
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "StallCode", MatchType.Exact, String.Format("{0}", Code)))

            Dim ServiceStandardTimeColl As ArrayList = m_ServiceStandardTimeMapper.RetrieveByCriteria(criterias)
            If (ServiceStandardTimeColl.Count > 0) Then
                Return CType(ServiceStandardTimeColl(0), KTB.DNet.Domain.ServiceStandardTime)
            End If
            Return New KTB.DNet.Domain.ServiceStandardTime
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ServiceStandardTimeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ServiceStandardTimeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ServiceStandardTimeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceStandardTime), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceStandardTimeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceStandardTime), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceStandardTimeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceStandardTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ServiceStandardTime As ArrayList = m_ServiceStandardTimeMapper.RetrieveByCriteria(criterias)
            Return _ServiceStandardTime
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceStandardTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ServiceStandardTimeColl As ArrayList = m_ServiceStandardTimeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ServiceStandardTimeColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceStandardTime), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ServiceStandardTimeColl As ArrayList = m_ServiceStandardTimeMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ServiceStandardTimeColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList


            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceStandardTime), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceStandardTimeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ServiceStandardTimeColl As ArrayList = m_ServiceStandardTimeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ServiceStandardTimeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(ServiceStandardTime), sortColumn, sortDirection))
            Else
                'sortColl = Nothing
                sortColl.Add(New Sort(GetType(ServiceStandardTime), "ID", Sort.SortDirection.DESC))

            End If

            Dim ServiceStandardTimeColl As ArrayList = m_ServiceStandardTimeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ServiceStandardTimeColl
        End Function


        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceStandardTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ServiceStandardTime), columnName, matchOperator, columnValue))
            Dim ServiceStandardTimeColl As ArrayList = m_ServiceStandardTimeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ServiceStandardTimeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceStandardTime), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceStandardTime), columnName, matchOperator, columnValue))

            Return m_ServiceStandardTimeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Integer
            Return m_ServiceStandardTimeMapper.RetrieveScalar(aggr, crit)
        End Function

        Public Function GetServiceStandardTime(ByVal dealerId As Short, ByVal dt As DataTable) As DataSet
            Dim parameters As ArrayList = New ArrayList
            Dim str As String = "up_GetServiceStandardTime"
            parameters.Add(New SqlClient.SqlParameter("@DealerID", dealerId))

            Dim param As New SqlClient.SqlParameter()
            param.ParameterName = "@ServiceStandardTime"
            param.TypeName = "dbo.ServiceStandardTimeType"
            param.Value = dt
            param.SqlDbType = SqlDbType.Structured
            parameters.Add(param)

            Dim ds As DataSet = m_ServiceStandardTimeMapper.RetrieveDataSet(str, parameters)
            Return ds
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As ServiceStandardTime) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_ServiceStandardTimeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ServiceStandardTime) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ServiceStandardTimeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Sub Delete(ByVal objDomain As ServiceStandardTime)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ServiceStandardTimeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As ServiceStandardTime)
            Try
                m_ServiceStandardTimeMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal sID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceStandardTime), "ID", MatchType.Exact, sID))
            Dim agg As Aggregate = New Aggregate(GetType(ServiceStandardTime), "ID", AggregateType.Count)

            Return CType(m_ServiceStandardTimeMapper.RetrieveScalar(agg, crit), Integer)
        End Function
#End Region

#Region "Custom Method"
        'Public Function Calculate(ByVal DealerCode As String, ByVal AssistServiceTypeCode As String, ByVal ServiceTypeID As Integer, ByVal PeriodFrom As Date) As KTB.DNet.Domain.ServiceStandardTime
        '    Try
        '        Dim spName As String
        '        Dim Param As New List(Of SqlClient.SqlParameter)

        '        spName = "sp_UpdateServiceStandardTime"
        '        Param.Add(New SqlClient.SqlParameter("@DealerCode", DealerCode))
        '        Param.Add(New SqlClient.SqlParameter("@AssistServiceTypeCode", AssistServiceTypeCode))
        '        Param.Add(New SqlClient.SqlParameter("@ServiceTypeID", ServiceTypeID))
        '        Param.Add(New SqlClient.SqlParameter("@PeriodFrom", PeriodFrom))
        '        Param.Add(New SqlClient.SqlParameter("@LastUpdateBy", m_userPrincipal.Identity.Name))
        '        Dim lststallmaster = m_ServiceStandardTimeMapper.RetrieveSP(spName, New ArrayList(Param))
        '        Dim _results As Integer = m_DiscountProposalHeaderMapper.ExecuteSP(spName, arrParam)

        '        Return _results
        '        'If (lststallmaster.Count > 0) Then
        '        '    Return CType(lststallmaster(0), KTB.DNet.Domain.StallMaster)
        '        'End If
        '        'Dim arrlist As ArrayList = CType(lststallmaster(0), ArrayList)
        '        'Dim strRunNum As String = ""
        '        'For Each item As String In arrlist
        '        '    strRunNum = item(2).ToString()
        '        'Next
        '        'Return lststallmaster
        '        'Return CType(lststallmaster(0), KTB.DNet.Domain.StallMaster)
        '        Return New KTB.DNet.Domain.ServiceStandardTime
        '    Catch ex As Exception
        '        Dim ss As String = ex.Message()
        '    End Try

        'End Function

        Public Function calculate2(ByVal DealerCode As String, ByVal AssistServiceTypeCode As String, ByVal ServiceTypeID As Integer, ByVal PeriodFrom As Date) As Integer
            Dim spName As String = "sp_UpdateServiceStandardTime"
            Dim _param1 = New SqlClient.SqlParameter
            Dim _param2 = New SqlClient.SqlParameter
            Dim _param3 = New SqlClient.SqlParameter
            Dim _param4 = New SqlClient.SqlParameter
            Dim _param5 = New SqlClient.SqlParameter
            Dim arrParam As New ArrayList

            _param1.DbType = DbType.AnsiString
            _param1.Value = DealerCode
            _param1.ParameterName = "@DealerCode"
            arrParam.Add(_param1)

            _param2.DbType = DbType.AnsiString
            _param2.Value = AssistServiceTypeCode
            _param2.ParameterName = "@AssistServiceTypeCode"
            arrParam.Add(_param2)

            _param3.DbType = DbType.Int16
            _param3.Value = ServiceTypeID
            _param3.ParameterName = "@ServiceTypeID"
            arrParam.Add(_param3)

            _param4.DbType = DbType.Date
            _param4.Value = PeriodFrom
            _param4.ParameterName = "@PeriodFrom"
            arrParam.Add(_param4)

            _param5.DbType = DbType.AnsiString
            _param5.Value = m_userPrincipal.Identity.Name
            _param5.ParameterName = "@LastUpdatedBy"
            arrParam.Add(_param5)
            Dim _results As Integer = 0
            Try
                _results = m_ServiceStandardTimeMapper.ExecuteSP(spName, arrParam)
            Catch ex As Exception
                Dim s As String = ex.Message
                _results = -1
            End Try

            Return _results
        End Function
#End Region
    End Class
End Namespace

