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
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class up_RetrieveFreeService_Service_ReminderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_up_RetrieveFreeService_Service_ReminderMapper As IMapper
        'Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            'Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_up_RetrieveFreeService_Service_ReminderMapper = MapperFactory.GetInstance().GetMapper(GetType(up_RetrieveFreeService_Service_Reminder).ToString)
        End Sub

#End Region

#Region "Retrieve"


        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(up_RetrieveFreeService_Service_Reminder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(up_RetrieveFreeService_Service_Reminder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(up_RetrieveFreeService_Service_Reminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FreeService As ArrayList = m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveByCriteria(criterias)
            Return _FreeService
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FreeServiceColl As ArrayList = m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FreeServiceColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(up_RetrieveFreeService_Service_Reminder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer,
                                           ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(up_RetrieveFreeService_Service_Reminder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim FreeServiceColl As ArrayList = m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return FreeServiceColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FreeServiceColl As ArrayList = m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FreeServiceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(up_RetrieveFreeService_Service_Reminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(up_RetrieveFreeService_Service_Reminder), columnName, matchOperator, columnValue))
            Dim FreeServiceColl As ArrayList = m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FreeServiceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(up_RetrieveFreeService_Service_Reminder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(up_RetrieveFreeService_Service_Reminder), columnName, matchOperator, columnValue))

            Return m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function retrieve_SP(ByVal par As String) As ArrayList
            Dim query As String = " exec up_RetrieveFreeService_Service_Reminde " & par
            Return m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveSP(query)
        End Function

        Public Function retrieve_SP(ByVal dealerID As Integer,
                                    ByVal fskindid As Integer,
                                    ByVal strchassisnumber As String,
                                    ByVal strOpenFakturFrom As String,
                                    ByVal strOpenFakturto As String,
                                    ByVal strPKTDateFrom As String,
                                    ByVal strPKTDateTo As String,
                                    ByVal pageNumber As Integer,
                                    ByVal pageSize As Integer,
                                    ByRef totalRow As Integer,
                                    ByVal sortColumn As String,
                                    ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            Dim spName As String
            Dim Param As New List(Of SqlClient.SqlParameter)
            Dim arrl As ArrayList

            spName = "up_RetrieveFreeService_Service_Reminder_paging"
            Param.Add(New SqlClient.SqlParameter("@chassisnumber", strchassisnumber))
            Param.Add(New SqlClient.SqlParameter("@dealerID", dealerID))
            Param.Add(New SqlClient.SqlParameter("@parSortColumn", sortColumn))
            Param.Add(New SqlClient.SqlParameter("@parDirection", sortDirection.ToString))

            If Not String.IsNullOrEmpty(strOpenFakturFrom) Then
                Param.Add(New SqlClient.SqlParameter("@openfakturdatefrom", strOpenFakturFrom))
            End If

            If Not String.IsNullOrEmpty(strOpenFakturto) Then
                Param.Add(New SqlClient.SqlParameter("@openfakturdateto", strOpenFakturto))
            End If

            If Not String.IsNullOrEmpty(strPKTDateTo) Then
                Param.Add(New SqlClient.SqlParameter("@pktDateto", strPKTDateTo))
            End If

            If Not String.IsNullOrEmpty(strPKTDateFrom) Then
                Param.Add(New SqlClient.SqlParameter("@pktDatefrom", strPKTDateFrom))
            End If

            If fskindid > 0 Then
                Param.Add(New SqlClient.SqlParameter("@FSKindID", fskindid))
            End If

            'Param.Add(New SqlClient.SqlParameter("@Sort ", sortColl.ToString()))
            Param.Add(New SqlClient.SqlParameter("@PageNumber", pageNumber))
            Param.Add(New SqlClient.SqlParameter("@PageSize", pageSize))
            ' New ArrayList(Param)

            arrl = m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveCustomPagingBySP(spName, New ArrayList(Param), pageNumber, pageSize, totalRow)

            If arrl Is Nothing OrElse arrl.Count = 0 Then
                arrl = New ArrayList
            End If

            Return arrl
        End Function

        Public Function retrieve_SP_ALL(ByVal dealerID As Integer,
                                    ByVal fskindid As Integer,
                                    ByVal strchassisnumber As String,
                                    ByVal strOpenFakturFrom As String,
                                    ByVal strOpenFakturto As String,
                                    ByVal strPKTDateFrom As String,
                                    ByVal strPKTDateTo As String,
                                    ByVal pageNumber As Integer,
                                    ByVal pageSize As Integer,
                                    ByRef totalRow As Integer,
                                    ByVal sortColumn As String,
                                    ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            Dim spName As String
            Dim Param As New List(Of SqlClient.SqlParameter)

            spName = "up_RetrieveFreeService_Service_Reminder"
            Param.Add(New SqlClient.SqlParameter("@chassisnumber", strchassisnumber))
            Param.Add(New SqlClient.SqlParameter("@dealerID", dealerID))
            Param.Add(New SqlClient.SqlParameter("@parSortColumn", sortColumn))
            Param.Add(New SqlClient.SqlParameter("@parDirection", sortDirection.ToString))

            If Not String.IsNullOrEmpty(strOpenFakturFrom) Then
                Param.Add(New SqlClient.SqlParameter("@openfakturdatefrom", strOpenFakturFrom))
            End If

            If Not String.IsNullOrEmpty(strOpenFakturto) Then
                Param.Add(New SqlClient.SqlParameter("@openfakturdateto", strOpenFakturto))
            End If

            If Not String.IsNullOrEmpty(strPKTDateTo) Then
                Param.Add(New SqlClient.SqlParameter("@pktDateto", strPKTDateTo))
            End If

            If Not String.IsNullOrEmpty(strPKTDateFrom) Then
                Param.Add(New SqlClient.SqlParameter("@pktDatefrom", strPKTDateFrom))
            End If

            If fskindid > 0 Then
                Param.Add(New SqlClient.SqlParameter("@FSKindID", fskindid))
            End If

            'Param.Add(New SqlClient.SqlParameter("@Sort ", sortColl.ToString()))
            Param.Add(New SqlClient.SqlParameter("@PageNumber", pageNumber))
            Param.Add(New SqlClient.SqlParameter("@PageSize", pageSize))
            ' New ArrayList(Param)
            Return m_up_RetrieveFreeService_Service_ReminderMapper.RetrieveCustomPagingBySP(spName, New ArrayList(Param), pageNumber, pageSize, totalRow)
        End Function


#End Region

    End Class

End Namespace
