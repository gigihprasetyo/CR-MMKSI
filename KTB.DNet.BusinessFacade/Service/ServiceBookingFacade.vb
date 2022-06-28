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

#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class ServiceBookingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_ServiceBookingMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ServiceBookingMapper = MapperFactory.GetInstance().GetMapper(GetType(ServiceBooking).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(ServiceBooking))
            Me.DomainTypeCollection.Add(GetType(CustomerCaseResponse))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ServiceBooking
            Return CType(m_ServiceBookingMapper.Retrieve(ID), ServiceBooking)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.ServiceBooking
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceBooking), "ServiceBookingCode", MatchType.Exact, Code))

            Dim ServiceBookingColl As ArrayList = m_ServiceBookingMapper.RetrieveByCriteria(criterias)
            If (ServiceBookingColl.Count > 0) Then
                Return CType(ServiceBookingColl(0), KTB.DNet.Domain.ServiceBooking)
            End If
            Return New KTB.DNet.Domain.ServiceBooking
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ServiceBookingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ServiceBookingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ServiceBookingMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceBooking), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceBookingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceBooking), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceBookingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ServiceBooking As ArrayList = m_ServiceBookingMapper.RetrieveByCriteria(criterias)
            Return _ServiceBooking
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ServiceBookingColl As ArrayList = m_ServiceBookingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ServiceBookingColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceBooking), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ServiceBookingColl As ArrayList = m_ServiceBookingMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ServiceBookingColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList


            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceBooking), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceBookingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ServiceBookingColl As ArrayList = m_ServiceBookingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ServiceBookingColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(ServiceBooking), sortColumn, sortDirection))
            Else
                'sortColl = Nothing
                sortColl.Add(New Sort(GetType(ServiceBooking), "ID", Sort.SortDirection.DESC))

            End If

            Dim ServiceBookingColl As ArrayList = m_ServiceBookingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ServiceBookingColl
        End Function


        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceBooking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ServiceBooking), columnName, matchOperator, columnValue))
            Dim ServiceBookingColl As ArrayList = m_ServiceBookingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ServiceBookingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceBooking), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceBooking), columnName, matchOperator, columnValue))

            Return m_ServiceBookingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Integer
            Return m_ServiceBookingMapper.RetrieveScalar(aggr, crit)
        End Function

        Public Function IsBooked(ByVal serviceBookingId As Integer, ByVal dealerId As Short, ByVal stallCode As String, _
                                                    ByVal startTime As DateTime, ByVal endTime As DateTime) As Boolean

            Dim parameters As ArrayList = New ArrayList
            Dim str As String = "up_IsValidServiceBooking"
            parameters.Add(New SqlClient.SqlParameter("@ServiceBookingID", serviceBookingId))
            parameters.Add(New SqlClient.SqlParameter("@DealerID", dealerId))
            parameters.Add(New SqlClient.SqlParameter("@StallCode", stallCode))
            parameters.Add(New SqlClient.SqlParameter("@StartTime", startTime))
            parameters.Add(New SqlClient.SqlParameter("@EndTime", endTime))

            Dim ds As DataSet = m_ServiceBookingMapper.RetrieveDataSet(str, parameters)
            Return Not IsNothing(ds) AndAlso ds.Tables(0).Rows.Count > 0
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is ServiceBooking) Then
                CType(InsertArg.DomainObject, ServiceBooking).ID = InsertArg.ID
                CType(InsertArg.DomainObject, ServiceBooking).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is CustomerCaseResponse) Then
                CType(InsertArg.DomainObject, CustomerCaseResponse).ID = InsertArg.ID
                CType(InsertArg.DomainObject, CustomerCaseResponse).MarkLoaded()
            End If
        End Sub

        Public Function Insert(ByVal objDomain As ServiceBooking) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_ServiceBookingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ServiceBooking) As Integer
            Dim nResult As Integer = -1
            Try
                m_ServiceBookingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                nResult = objDomain.ID
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Sub Delete(ByVal objDomain As ServiceBooking)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ServiceBookingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As ServiceBooking)
            Try
                m_ServiceBookingMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal sID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceBooking), "ID", MatchType.Exact, sID))
            Dim agg As Aggregate = New Aggregate(GetType(ServiceBooking), "ID", AggregateType.Count)

            Return CType(m_ServiceBookingMapper.RetrieveScalar(agg, crit), Integer)
        End Function
#End Region

#Region "Custom Method"
        Public Function UpdateStatus(ByVal ID As Integer) As Integer
            Try
                Dim spName As String
                Dim Param As New List(Of SqlClient.SqlParameter)
                Dim nResult As Integer = 0
                spName = "up_UpdateStatusServiceBooking"
                Param.Add(New SqlClient.SqlParameter("@ID", ID))
                Param.Add(New SqlClient.SqlParameter("@LastUpdatedBy", m_userPrincipal.Identity.Name))
                Dim lstservicebooking = m_ServiceBookingMapper.RetrieveSP(spName, New ArrayList(Param))
                Return nResult
                'If (lstservicebooking. > 0) Then
                '    Return lstservicebooking  'CType(lstservicebooking(0), KTB.DNet.Domain.ServiceBooking)
                'End If
                ''Dim arrlist As ArrayList = CType(lststallmaster(0), ArrayList)
                ''Dim strRunNum As String = ""
                ''For Each item As String In arrlist
                ''    strRunNum = item(2).ToString()
                ''Next
                ''Return lststallmaster
                ''Return CType(lststallmaster(0), KTB.DNet.Domain.StallMaster)
                'Return New KTB.DNet.Domain.ServiceBooking
            Catch ex As Exception
                Dim ss As String = ex.Message()
            End Try

        End Function

        Public Function Insert(ByVal objDomain As ServiceBooking, ByVal activities As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                m_TransactionManager.AddInsert(objDomain, _user)
                For Each objActivity As ServiceBookingActivity In activities
                    objActivity.ServiceBooking = objDomain
                    m_TransactionManager.AddInsert(objActivity, _user)
                Next

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function

        Public Function Insert(ByVal objDomain As ServiceBooking, ByVal activities As ArrayList, ByVal srFU As ServiceReminderFollowUp) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                m_TransactionManager.AddInsert(objDomain, _user)

                For Each objActivity As ServiceBookingActivity In activities
                    objActivity.ServiceBooking = objDomain
                    m_TransactionManager.AddInsert(objActivity, _user)
                Next

                Dim status As Integer = srFU.ServiceReminder.Status
                If status <> EnumGlobalServiceReminder.ServiceReminderStatus.CSMMKSI _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Finish _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Archive Then
                    srFU.ServiceBooking = objDomain
                    m_TransactionManager.AddInsert(srFU, _user)

                    Dim sr As ServiceReminder = srFU.ServiceReminder
                    sr.Status = EnumGlobalServiceReminder.ServiceReminderStatus.InProgress
                    sr.BookingDate = srFU.BookingDate.Date
                    sr.BookingTime = srFU.BookingDate.ToString("HH:mm:ss")

                    If srFU.BookingDate.Date > sr.MaxFUDealerDate.Date Then
                        sr.MaxFUDealerDate = srFU.BookingDate.Date.AddDays(1)
                    End If

                    m_TransactionManager.AddUpdate(sr, _user)
                End If

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function

        Public Function Insert(ByVal objDomain As ServiceBooking, ByVal activities As ArrayList, ByVal srFU As ServiceReminderFollowUp, ByVal ccResponse As CustomerCaseResponse) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                m_TransactionManager.AddInsert(objDomain, _user)

                For Each objActivity As ServiceBookingActivity In activities
                    objActivity.ServiceBooking = objDomain
                    m_TransactionManager.AddInsert(objActivity, _user)
                Next

                Dim status As Integer = srFU.ServiceReminder.Status
                If status <> EnumGlobalServiceReminder.ServiceReminderStatus.CSMMKSI _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Finish _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Archive Then

                    srFU.ServiceBooking = objDomain
                    m_TransactionManager.AddInsert(srFU, _user)

                    Dim sr As ServiceReminder = srFU.ServiceReminder
                    sr.Status = EnumGlobalServiceReminder.ServiceReminderStatus.InProgress
                    sr.BookingDate = srFU.BookingDate.Date
                    sr.BookingTime = srFU.BookingDate.ToString("HH:mm:ss")

                    If srFU.BookingDate.Date > sr.MaxFUDealerDate.Date Then
                        sr.MaxFUDealerDate = srFU.BookingDate.Date.AddDays(1)
                    End If

                    m_TransactionManager.AddUpdate(sr, _user)
                End If

                If Not ccResponse.CustomerCase.Status = EnumCustomerCaseResponse.CustomerCaseResponse.Closed Then
                    ccResponse.ServiceBooking = objDomain
                    m_TransactionManager.AddInsert(ccResponse, _user)

                    Dim cc As CustomerCase = ccResponse.CustomerCase
                    cc.Status = ccResponse.Status
                    m_TransactionManager.AddUpdate(cc, _user)
                End If

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function

        Public Function Insert(ByVal objDomain As ServiceBooking, ByVal activities As ArrayList, ByVal ccResponse As CustomerCaseResponse) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                m_TransactionManager.AddInsert(objDomain, _user)

                For Each objActivity As ServiceBookingActivity In activities
                    objActivity.ServiceBooking = objDomain
                    m_TransactionManager.AddInsert(objActivity, _user)
                Next

                If Not ccResponse.CustomerCase.Status = EnumCustomerCaseResponse.CustomerCaseResponse.Closed Then
                    ccResponse.ServiceBooking = objDomain
                    m_TransactionManager.AddInsert(ccResponse, _user)

                    Dim cc As CustomerCase = ccResponse.CustomerCase
                    cc.Status = ccResponse.Status
                    m_TransactionManager.AddUpdate(cc, _user)
                End If

                Dim srFU As ServiceReminderFollowUp = objDomain.ServiceReminderFollowUp
                If Not IsNothing(srFU) Then
                    Dim status As Integer = srFU.ServiceReminder.Status
                    'Dim currBookingDate As DateTime = IIf(objDomain.WorkingTimeStart > objDomain.IncomingDateStart, objDomain.IncomingDateStart, objDomain.WorkingTimeStart)
                    If status <> EnumGlobalServiceReminder.ServiceReminderStatus.CSMMKSI _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Finish _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Archive _
                        And srFU.BookingDate <> objDomain.WorkingTimeStart Then

                        Dim item As ServiceReminderFollowUp = New ServiceReminderFollowUp
                        item.BookingDate = objDomain.WorkingTimeStart
                        item.FollowUpAction = New StandardCodeFacade(m_userPrincipal).GetByCategoryValue("ServiceBooking.GSR.Response", "4").ValueDesc
                        item.FollowUpStatus = EnumGlobalServiceReminder.ServiceReminderFollowUpStatus.InProgress
                        item.FollowUpDate = DateTime.Now
                        item.ServiceBooking = objDomain
                        item.ServiceReminder = srFU.ServiceReminder

                        m_TransactionManager.AddInsert(item, _user)

                        Dim sr As ServiceReminder = srFU.ServiceReminder
                        sr.Status = EnumGlobalServiceReminder.ServiceReminderStatus.InProgress

                        sr.BookingDate = srFU.BookingDate.Date
                        sr.BookingTime = srFU.BookingDate.ToString("HH:mm:ss")

                        If srFU.BookingDate.Date > sr.MaxFUDealerDate.Date Then
                            sr.MaxFUDealerDate = srFU.BookingDate.Date.AddDays(1)
                        End If

                        m_TransactionManager.AddUpdate(sr, _user)
                    End If
                End If

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function

        Public Function Update(ByVal objDomain As ServiceBooking, ByVal activities As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                For Each objActivity As ServiceBookingActivity In activities
                    objActivity.ServiceBooking = objDomain
                    If objActivity.ID = 0 Then
                        m_TransactionManager.AddInsert(objActivity, _user)
                    Else
                        m_TransactionManager.AddUpdate(objActivity, _user)
                    End If
                Next

                For Each item As ServiceBookingActivity In objDomain.ServiceBookingActivities
                    Dim isExist As Boolean = False
                    For Each itemExist As ServiceBookingActivity In activities
                        If item.ID = itemExist.ID Then
                            isExist = True
                            Exit For
                        End If
                    Next

                    If Not isExist Then
                        item.RowStatus = CShort(DBRowStatus.Deleted)
                        m_TransactionManager.AddUpdate(item, _user)
                    End If
                Next

                'Check : is there service reminder follow up or not ?
                Dim srFU As ServiceReminderFollowUp = objDomain.ServiceReminderFollowUp
                If Not IsNothing(srFU) Then
                    Dim status As Integer = srFU.ServiceReminder.Status
                    'Dim currBookingDate As DateTime = IIf(objDomain.WorkingTimeStart > objDomain.IncomingDateStart, objDomain.IncomingDateStart, objDomain.WorkingTimeStart)
                    If status <> EnumGlobalServiceReminder.ServiceReminderStatus.CSMMKSI _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Finish _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Archive _
                        And srFU.BookingDate <> objDomain.WorkingTimeStart Then

                        Dim item As ServiceReminderFollowUp = New ServiceReminderFollowUp
                        item.BookingDate = objDomain.WorkingTimeStart
                        item.FollowUpAction = New StandardCodeFacade(m_userPrincipal).GetByCategoryValue("ServiceBooking.GSR.Response", "4").ValueDesc
                        item.FollowUpStatus = EnumGlobalServiceReminder.ServiceReminderFollowUpStatus.InProgress
                        item.FollowUpDate = DateTime.Now
                        item.ServiceBooking = objDomain
                        item.ServiceReminder = srFU.ServiceReminder

                        m_TransactionManager.AddInsert(item, _user)

                        Dim sr As ServiceReminder = srFU.ServiceReminder
                        sr.Status = EnumGlobalServiceReminder.ServiceReminderStatus.InProgress

                        m_TransactionManager.AddUpdate(sr, _user)
                    End If
                End If

                m_TransactionManager.AddUpdate(objDomain, _user)

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function

        Public Function Update(ByVal objDomain As ServiceBooking, ByVal activities As ArrayList, ByVal ccResponse As CustomerCaseResponse) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                For Each objActivity As ServiceBookingActivity In activities
                    objActivity.ServiceBooking = objDomain
                    If objActivity.ID = 0 Then
                        m_TransactionManager.AddInsert(objActivity, _user)
                    Else
                        m_TransactionManager.AddUpdate(objActivity, _user)
                    End If
                Next

                For Each item As ServiceBookingActivity In objDomain.ServiceBookingActivities
                    Dim isExist As Boolean = False
                    For Each itemExist As ServiceBookingActivity In activities
                        If item.ID = itemExist.ID Then
                            isExist = True
                            Exit For
                        End If
                    Next

                    If Not isExist Then
                        item.RowStatus = CShort(DBRowStatus.Deleted)
                        m_TransactionManager.AddUpdate(item, _user)
                    End If
                Next

                If Not ccResponse.CustomerCase.Status = EnumCustomerCaseResponse.CustomerCaseResponse.Closed Then
                    If ccResponse.ID = 0 Then
                        ccResponse.ServiceBooking = objDomain
                        m_TransactionManager.AddInsert(ccResponse, _user)

                        Dim cc As CustomerCase = ccResponse.CustomerCase
                        cc.Status = ccResponse.Status
                        m_TransactionManager.AddUpdate(cc, _user)
                    End If
                End If

                Dim srFU As ServiceReminderFollowUp = objDomain.ServiceReminderFollowUp
                If Not IsNothing(srFU) Then
                    Dim status As Integer = srFU.ServiceReminder.Status
                    'Dim currBookingDate As DateTime = IIf(objDomain.WorkingTimeStart > objDomain.IncomingDateStart, objDomain.IncomingDateStart, objDomain.WorkingTimeStart)
                    If status <> EnumGlobalServiceReminder.ServiceReminderStatus.CSMMKSI _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Finish _
                        And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Archive _
                        And srFU.BookingDate <> objDomain.WorkingTimeStart Then

                        Dim item As ServiceReminderFollowUp = New ServiceReminderFollowUp
                        item.BookingDate = objDomain.WorkingTimeStart
                        item.FollowUpAction = New StandardCodeFacade(m_userPrincipal).GetByCategoryValue("ServiceBooking.GSR.Response", "4").ValueDesc
                        item.FollowUpStatus = EnumGlobalServiceReminder.ServiceReminderFollowUpStatus.InProgress
                        item.FollowUpDate = DateTime.Now
                        item.ServiceBooking = objDomain
                        item.ServiceReminder = srFU.ServiceReminder

                        m_TransactionManager.AddInsert(item, _user)

                        Dim sr As ServiceReminder = srFU.ServiceReminder
                        sr.Status = EnumGlobalServiceReminder.ServiceReminderStatus.InProgress
                        sr.BookingDate = srFU.BookingDate.Date
                        sr.BookingTime = srFU.BookingDate.ToString("HH:mm:ss")

                        If srFU.BookingDate.Date > sr.MaxFUDealerDate.Date Then
                            sr.MaxFUDealerDate = srFU.BookingDate.Date.AddDays(1)
                        End If

                        m_TransactionManager.AddUpdate(sr, _user)
                    End If
                End If

                m_TransactionManager.AddUpdate(objDomain, _user)

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function

        Public Function Update(ByVal objDomain As ServiceBooking, ByVal activities As ArrayList, ByVal srFU As ServiceReminderFollowUp) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                If Not IsNothing(activities) Then
                    For Each objActivity As ServiceBookingActivity In activities
                        objActivity.ServiceBooking = objDomain
                        If objActivity.ID = 0 Then
                            m_TransactionManager.AddInsert(objActivity, _user)
                        Else
                            m_TransactionManager.AddUpdate(objActivity, _user)
                        End If
                    Next

                    For Each item As ServiceBookingActivity In objDomain.ServiceBookingActivities
                        Dim isExist As Boolean = False
                        For Each itemExist As ServiceBookingActivity In activities
                            If item.ID = itemExist.ID Then
                                isExist = True
                                Exit For
                            End If
                        Next

                        If Not isExist Then
                            item.RowStatus = CShort(DBRowStatus.Deleted)
                            m_TransactionManager.AddUpdate(item, _user)
                        End If
                    Next
                End If

                If srFU.ID = 0 Then
                    Dim status As Integer = srFU.ServiceReminder.Status
                    If status <> EnumGlobalServiceReminder.ServiceReminderStatus.CSMMKSI _
                            And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Finish _
                            And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Archive Then
                        srFU.ServiceBooking = objDomain
                        m_TransactionManager.AddInsert(srFU, _user)

                        Dim sr As ServiceReminder = srFU.ServiceReminder
                        sr.Status = EnumGlobalServiceReminder.ServiceReminderStatus.InProgress
                        sr.BookingDate = srFU.BookingDate.Date
                        sr.BookingTime = srFU.BookingDate.ToString("HH:mm:ss")

                        If srFU.BookingDate.Date > sr.MaxFUDealerDate.Date Then
                            sr.MaxFUDealerDate = srFU.BookingDate.Date.AddDays(1)
                        End If

                        m_TransactionManager.AddUpdate(sr, _user)
                    End If
                End If

                m_TransactionManager.AddUpdate(objDomain, _user)

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function

        Public Function Update(ByVal objDomain As ServiceBooking, ByVal activities As ArrayList, ByVal srFU As ServiceReminderFollowUp, ByVal ccResponse As CustomerCaseResponse) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                For Each objActivity As ServiceBookingActivity In activities
                    objActivity.ServiceBooking = objDomain
                    If objActivity.ID = 0 Then
                        m_TransactionManager.AddInsert(objActivity, _user)
                    Else
                        m_TransactionManager.AddUpdate(objActivity, _user)
                    End If
                Next

                For Each item As ServiceBookingActivity In objDomain.ServiceBookingActivities
                    Dim isExist As Boolean = False
                    For Each itemExist As ServiceBookingActivity In activities
                        If item.ID = itemExist.ID Then
                            isExist = True
                            Exit For
                        End If
                    Next

                    If Not isExist Then
                        item.RowStatus = CShort(DBRowStatus.Deleted)
                        m_TransactionManager.AddUpdate(item, _user)
                    End If
                Next

                If srFU.ID = 0 Then
                    Dim status As Integer = srFU.ServiceReminder.Status
                    If status <> EnumGlobalServiceReminder.ServiceReminderStatus.CSMMKSI _
                            And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Finish _
                            And status <> EnumGlobalServiceReminder.ServiceReminderStatus.Archive Then
                        srFU.ServiceBooking = objDomain
                        m_TransactionManager.AddInsert(srFU, _user)

                        Dim sr As ServiceReminder = srFU.ServiceReminder
                        sr.Status = EnumGlobalServiceReminder.ServiceReminderStatus.InProgress
                        sr.BookingDate = srFU.BookingDate.Date
                        sr.BookingTime = srFU.BookingDate.ToString("HH:mm:ss")

                        If srFU.BookingDate.Date > sr.MaxFUDealerDate.Date Then
                            sr.MaxFUDealerDate = srFU.BookingDate.Date.AddDays(1)
                        End If

                        m_TransactionManager.AddUpdate(sr, _user)
                    End If
                End If

                If Not ccResponse.CustomerCase.Status = EnumCustomerCaseResponse.CustomerCaseResponse.Closed Then
                    If ccResponse.ID = 0 Then
                        ccResponse.ServiceBooking = objDomain
                        m_TransactionManager.AddInsert(ccResponse, _user)
                        'Else
                        '    m_TransactionManager.AddUpdate(ccResponse, _user)
                    End If

                    Dim cc As CustomerCase = ccResponse.CustomerCase
                    cc.Status = ccResponse.Status
                    m_TransactionManager.AddUpdate(cc, _user)
                End If

                m_TransactionManager.AddUpdate(objDomain, _user)

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function
#End Region
    End Class
End Namespace

