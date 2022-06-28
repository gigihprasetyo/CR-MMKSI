
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
'// Generated on 25/06/2020 - 10:36:51
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

    Public Class SparePartDueDateNotificationFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartDueDateNotificationMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartDueDateNotificationMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartDueDateNotification).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartDueDateNotification
            Return CType(m_SparePartDueDateNotificationMapper.Retrieve(ID), SparePartDueDateNotification)
        End Function

        Public Function Retrieve(ByVal Code As String) As SparePartDueDateNotification
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDueDateNotification), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartDueDateNotification), "SparePartDueDateNotificationCode", MatchType.Exact, Code))

            Dim SparePartDueDateNotificationColl As ArrayList = m_SparePartDueDateNotificationMapper.RetrieveByCriteria(criterias)
            If (SparePartDueDateNotificationColl.Count > 0) Then
                Return CType(SparePartDueDateNotificationColl(0), SparePartDueDateNotification)
            End If
            Return New SparePartDueDateNotification
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartDueDateNotificationMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartDueDateNotificationMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartDueDateNotificationMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartDueDateNotification), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartDueDateNotificationMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartDueDateNotification), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartDueDateNotificationMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDueDateNotification), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartDueDateNotification As ArrayList = m_SparePartDueDateNotificationMapper.RetrieveByCriteria(criterias)
            Return _SparePartDueDateNotification
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDueDateNotification), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartDueDateNotificationColl As ArrayList = m_SparePartDueDateNotificationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartDueDateNotificationColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SparePartDueDateNotification), SortColumn, sortDirection))
            Dim SparePartDueDateNotificationColl As ArrayList = m_SparePartDueDateNotificationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SparePartDueDateNotificationColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartDueDateNotificationColl As ArrayList = m_SparePartDueDateNotificationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartDueDateNotificationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDueDateNotification), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartDueDateNotificationColl As ArrayList = m_SparePartDueDateNotificationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartDueDateNotification), columnName, matchOperator, columnValue))
            Return SparePartDueDateNotificationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartDueDateNotification), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDueDateNotification), columnName, matchOperator, columnValue))

            Return m_SparePartDueDateNotificationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDueDateNotification), "SparePartDueDateNotificationCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SparePartDueDateNotification), "SparePartDueDateNotificationCode", AggregateType.Count)
            Return CType(m_SparePartDueDateNotificationMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SparePartDueDateNotification) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SparePartDueDateNotificationMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartDueDateNotification) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartDueDateNotificationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartDueDateNotification)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartDueDateNotificationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartDueDateNotification)
            Try
                m_SparePartDueDateNotificationMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Function GetNotifForConfirmation(dt As Date) As DataSet
            Dim ds As New DataSet
            Dim sp As String = "sp_SparePartDueDateNotification_GetAllBilling"
            Dim param As New ArrayList


            Dim sl As New SqlClient.SqlParameter
            sl.ParameterName = "@BilingDate"

            sl.Value = dt
            param.Add(sl)


            ds = m_SparePartDueDateNotificationMapper.RetrieveDataSet(sp, param)

            Return ds
        End Function

        Public Function GetNotifForDueDate(ByVal dt As DateTime) As DataSet
            Dim ds As New DataSet
            Dim sp As String = "sp_SparePartDueDateNotification_GetDueDate"
            Dim param As New ArrayList


            Dim sl As New SqlClient.SqlParameter
            sl.ParameterName = "@Date"

            sl.Value = dt
            param.Add(sl)


            ds = m_SparePartDueDateNotificationMapper.RetrieveDataSet(sp, param)

            Return ds
        End Function
#End Region

    End Class

End Namespace

