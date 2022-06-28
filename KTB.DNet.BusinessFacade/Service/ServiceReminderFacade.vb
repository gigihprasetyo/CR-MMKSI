
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
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 03/07/2020 - 12:25:14
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

Namespace KTB.DNET.BusinessFacade.Service

    Public Class ServiceReminderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ServiceReminderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ServiceReminderMapper = MapperFactory.GetInstance.GetMapper(GetType(ServiceReminder).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ServiceReminder
            Return CType(m_ServiceReminderMapper.Retrieve(ID), ServiceReminder)
        End Function

        Public Function Retrieve(ByVal Code As String) As ServiceReminder
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ServiceReminder), "ServiceReminderCode", MatchType.Exact, Code))

            Dim ServiceReminderColl As ArrayList = m_ServiceReminderMapper.RetrieveByCriteria(criterias)
            If (ServiceReminderColl.Count > 0) Then
                Return CType(ServiceReminderColl(0), ServiceReminder)
            End If
            Return New ServiceReminder
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ServiceReminderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ServiceReminderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ServiceReminderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceReminder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceReminderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceReminder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceReminderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ServiceReminder As ArrayList = m_ServiceReminderMapper.RetrieveByCriteria(criterias)
            Return _ServiceReminder
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ServiceReminderColl As ArrayList = m_ServiceReminderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ServiceReminderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(ServiceReminder), SortColumn, sortDirection))
            Dim ServiceReminderColl As ArrayList = m_ServiceReminderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ServiceReminderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceReminder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ServiceReminderColl As ArrayList = m_ServiceReminderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ServiceReminderColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ServiceReminderColl As ArrayList = m_ServiceReminderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ServiceReminderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ServiceReminderColl As ArrayList = m_ServiceReminderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ServiceReminder), columnName, matchOperator, columnValue))
            Return ServiceReminderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceReminder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), columnName, matchOperator, columnValue))

            Return m_ServiceReminderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "ServiceReminderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ServiceReminder), "ServiceReminderCode", AggregateType.Count)
            Return CType(m_ServiceReminderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As ServiceReminder) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_ServiceReminderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ServiceReminder) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ServiceReminderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As ServiceReminder)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ServiceReminderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As ServiceReminder)
            Try
                m_ServiceReminderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveSp(str As String) As DataSet
            Return m_ServiceReminderMapper.RetrieveDataSet(str)
        End Function
        Public Function GSRRilisFS(ByVal ID As Integer) As Integer
            Dim spName As String = "sp_GSRRilisFS"
            Dim _param1 = New SqlClient.SqlParameter
            Dim _param2 = New SqlClient.SqlParameter
            ' Dim _param3 = New SqlClient.SqlParameter
            Dim arrParam As New ArrayList

            _param1.DbType = DbType.Int32
            _param1.Value = ID
            _param1.ParameterName = "@ID"
            arrParam.Add(_param1)

            _param2.DbType = DbType.AnsiString
            _param2.Value = m_userPrincipal.Identity.Name
            _param2.ParameterName = "@LastUpdatedBy"
            arrParam.Add(_param2)

            '_param3.DbType = DbType.Int16
            '_param3.Value = DealerID
            '_param3.ParameterName = "@DealerID"
            'arrParam.Add(_param3)
            Dim _results As Integer = 0
            Try
                _results = m_ServiceReminderMapper.ExecuteSP(spName, arrParam)
            Catch ex As Exception
                Dim s As String = ex.Message
                _results = -1
            End Try

            Return _results
        End Function

        Public Function GSRRilisPM(ByVal ID As Integer) As Integer
            Dim spName As String = "sp_GSRRilisPM"
            Dim _param1 = New SqlClient.SqlParameter
            Dim _param2 = New SqlClient.SqlParameter
            ' Dim _param3 = New SqlClient.SqlParameter
            Dim arrParam As New ArrayList

            _param1.DbType = DbType.Int32
            _param1.Value = ID
            _param1.ParameterName = "@ID"
            arrParam.Add(_param1)

            _param2.DbType = DbType.AnsiString
            _param2.Value = m_userPrincipal.Identity.Name
            _param2.ParameterName = "@LastUpdatedBy"
            arrParam.Add(_param2)

            '_param3.DbType = DbType.Int16
            '_param3.Value = DealerID
            '_param3.ParameterName = "@DealerID"
            'arrParam.Add(_param3)
            Dim _results As Integer = 0
            Try
                _results = m_ServiceReminderMapper.ExecuteSP(spName, arrParam)
            Catch ex As Exception
                Dim s As String = ex.Message
                _results = -1
            End Try

            Return _results
        End Function

        Public Function GSRRilisSIU(ByVal ID As Integer) As Integer
            Dim spName As String = "sp_GSRRilisSIU"
            Dim _param1 = New SqlClient.SqlParameter
            Dim _param2 = New SqlClient.SqlParameter
            ' Dim _param3 = New SqlClient.SqlParameter
            Dim arrParam As New ArrayList

            _param1.DbType = DbType.Int32
            _param1.Value = ID
            _param1.ParameterName = "@ID"
            arrParam.Add(_param1)

            _param2.DbType = DbType.AnsiString
            _param2.Value = m_userPrincipal.Identity.Name
            _param2.ParameterName = "@LastUpdatedBy"
            arrParam.Add(_param2)

            '_param3.DbType = DbType.Int16
            '_param3.Value = DealerID
            '_param3.ParameterName = "@DealerID"
            'arrParam.Add(_param3)
            Dim _results As Integer = 0
            Try
                _results = m_ServiceReminderMapper.ExecuteSP(spName, arrParam)
            Catch ex As Exception
                Dim s As String = ex.Message
                _results = -1
            End Try

            Return _results
        End Function

        Public Function RetrieveServiceReminderDownload(ByVal criterias As ICriteria) As ArrayList
            Dim whereCondition As String = criterias.ToString().Replace("{", "").Replace("}", "")
            Dim arrParam As ArrayList = New ArrayList()
            Dim arrResult As ArrayList = New ArrayList()

            Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@WhereCondition", whereCondition)
            arrParam.Add(param1)
            arrResult = m_ServiceReminderMapper.RetrieveSP("sp_GetServiceReminder_ForDownload", arrParam)
            Return arrResult
        End Function

        Public Function RetrieveServiceReminderDownloadSP(ByVal crit As ICriteria) As DataSet
            Dim strCrit As String = crit.ToString().Replace("'", "''")
            Return m_ServiceReminderMapper.RetrieveDataSet("EXEC sp_GetServiceReminder_ForDownload @whereCondition='" & strCrit & "'")
        End Function

        Public Function GetPersona(ByVal ChasssisNumber As String) As DataSet
            Try
                'Dim spName As String
                'Dim Param As New List(Of SqlClient.SqlParameter)
                'Dim strPersona As String
                'spName = "sp_GetPersona"
                'Param.Add(New SqlClient.SqlParameter("@ChassisNumber", ChasssisNumber))
                'Dim lstPersona = m_ServiceReminderMapper.RetrieveSP(spName, New ArrayList(Param))
                'If (lstPersona.Count > 0) Then
                '    strPersona = CType(lstPersona(0), String)
                'End If

                'Dim strCrit As String = crit.ToString().Replace("'", "''")
                Return m_ServiceReminderMapper.RetrieveDataSet("EXEC sp_GetPersona @ChassisNumber='" & ChasssisNumber & "'")





                'Dim arrlist As ArrayList = CType(lststallmaster(0), ArrayList)
                'Dim strRunNum As String = ""
                'For Each item As String In arrlist
                '    strRunNum = item(2).ToString()
                'Next
                'Return lststallmaster
                'Return CType(lststallmaster(0), KTB.DNet.Domain.StallMaster)
                'Return New KTB.DNET.Domain.StallMaster
                'Return lstPersona.
            Catch ex As Exception
                Dim ss As String = ex.Message()
            End Try

        End Function

#End Region

    End Class

End Namespace

