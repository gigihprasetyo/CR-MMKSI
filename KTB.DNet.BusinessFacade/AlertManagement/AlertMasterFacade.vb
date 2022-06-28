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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 9/26/2005 - 1:43:31 PM
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
Imports KTB.Dnet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.AlertManagement
    Public Class AlertMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AlertMasterMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.m_AlertMasterMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.AlertMaster).ToString)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.AlertMaster))
        End Sub

#End Region

#Region "Retrieve"
        Public Function Retrieve(ByVal ID As Integer) As AlertMaster
            Return CType(m_AlertMasterMapper.Retrieve(ID), AlertMaster)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AlertMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AlertMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AlertMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(AlertMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AlertMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(AlertMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AlertMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AlertMasteritColl As ArrayList = m_AlertMasterMapper.RetrieveByCriteria(criterias)
            Return m_AlertMasterMapper
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AlertMasteritColl As ArrayList = m_AlertMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return m_AlertMasterMapper
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AlertMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AlertMasterColl As ArrayList = m_AlertMasterMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AlertMasterColl
        End Function

        Public Function RetrieveBabitProposalActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim arlResult As ArrayList = m_AlertMasterMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return arlResult
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AlertMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AlertMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AlertMasterColl As ArrayList = m_AlertMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return AlertMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AlertMasterColl As ArrayList = m_AlertMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AlertMaster), columnName, matchOperator, columnValue))
            Return AlertMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(AlertMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertMaster), columnName, matchOperator, columnValue))

            Return m_AlertMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
#End Region

#Region "Custom Methods"
        Private Function GetCurrentDateTime() As DateTime
            Dim dtCurrent As DateTime = DateTime.Now  'New DateTime(2007, 10, 23, 11, 0, 0)
            dtCurrent = New DateTime(dtCurrent.Year, dtCurrent.Month, dtCurrent.Day, dtCurrent.Hour, dtCurrent.Minute, 0)

            Return dtCurrent
        End Function
        Private Function IsTransactionalAlert(ByVal alertData As AlertMaster) As Boolean
            If (alertData.AlertType = EnumAlertManagement.AlertManagementType.Transactional) Then
                Return True
            Else
                Return False
            End If

        End Function
        Private Function IsCurrentDateIsNationalHoliday(ByVal currentDateTime As DateTime)
            Dim holiday As NationalHoliday

            currentDateTime = New DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, 0, 0, 0)

            Dim cc As New KTB.DNet.Domain.Search.CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, 0))
            cc.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, currentDateTime))

            Dim holidayFacade As New KTB.DNet.BusinessFacade.General.NationalHolidayFacade(m_userPrincipal)
            Dim arlHolidays As ArrayList = holidayFacade.RetrieveByCriteria(cc)

            Return (arlHolidays.Count > 0)
        End Function
        Private Function IsIncludeHoliday(ByVal alertData As AlertMaster) As Boolean
            Return (IsTransactionalAlert(alertData) And alertData.IsIncludeHoliday)
        End Function
        Private Function DueAlert(ByVal alertData As AlertMaster) As Boolean
            Dim NULL_DATETIME As DateTime = New DateTime(1900, 1, 1, 0, 0, 0)

            Dim bIsDue As Boolean = False
            Dim currentDateTime As DateTime = GetCurrentDateTime()

            currentDateTime = New DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, 0, 0, 0)

            If IsTransactionalAlert(alertData) Then
                If IsCurrentDateIsNationalHoliday(currentDateTime) Then
                    bIsDue = IsIncludeHoliday(alertData)
                Else
                    bIsDue = True
                End If
                'ElseIf CType(alertData.AnnouncementAlertType, EnumAlertManagement.AnnouncementAlertType) = EnumAlertManagement.AnnouncementAlertType.OneTimeAlert Then
                '    bIsDue = (alertData.NextRunForAlertBox.Equals(NULL_DATETIME) And _
                '               alertData.NextRunForDashboard.Equals(NULL_DATETIME) And _
                '               alertData.NextRunForEmail.Equals(NULL_DATETIME) And _
                '               alertData.NextRunForSMS.Equals(NULL_DATETIME))
            Else
                bIsDue = True
            End If


            If bIsDue Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function RetrieveValidDashboardAlerts(ByVal objUserInfo As UserInfo, ByVal modulCategory As String) As ArrayList

            'Dim ccUserGroupMember As New KTB.DNet.Domain.Search.CriteriaComposite(New Criteria(GetType(UserGroupMember), "RowStatus", MatchType.Exact, 0))
            'ccUserGroupMember.opAnd(New Criteria(GetType(UserGroupMember), "UserInfo", MatchType.Exact, objUserInfo.ID))

            'Dim arlUserGroupMembers As ArrayList = New UserManagement.UserGroupMemberFacade(m_userPrincipal).Retrieve(ccUserGroupMember)

            'Dim userGroupIDs As String = String.Empty
            'For Each objMember As UserGroupMember In arlUserGroupMembers
            '    If userGroupIDs.Length > 0 Then
            '        userGroupIDs += ","
            '    End If
            '    userGroupIDs += objMember.UserGroup.ID.ToString()
            'Next

            'Dim alertMasterIDs As String = String.Empty
            'If userGroupIDs.Length > 0 Then
            '    userGroupIDs = "(" + userGroupIDs + ")"

            '    Dim ccAlertGroup As New KTB.DNet.Domain.Search.CriteriaComposite(New Criteria(GetType(AlertGroup), "RowStatus", MatchType.Exact, 0))
            '    ccAlertGroup.opAnd(New Criteria(GetType(AlertGroup), "UserGroup", MatchType.InSet, userGroupIDs))

            '    Dim arlAlertGroups As ArrayList = New AlertManagement.AlertGroupFacade(m_userPrincipal).Retrieve(ccAlertGroup)

            '    For Each grp As AlertGroup In arlAlertGroups
            '        If alertMasterIDs.Length > 0 Then
            '            alertMasterIDs += ","
            '        End If
            '        alertMasterIDs += grp.AlertMaster.ID.ToString
            '    Next
            'Else
            '    Return New ArrayList
            'End If

            'If alertMasterIDs.Length > 0 Then

            'alertMasterIDs = "(" + alertMasterIDs + ")"

            Dim cc As New KTB.DNet.Domain.Search.CriteriaComposite(New Criteria(GetType(AlertMaster), "RowStatus", MatchType.Exact, 0))
            Dim dtCurrent As DateTime = GetCurrentDateTime()
            Dim dtValid As DateTime = New DateTime(dtCurrent.Year, dtCurrent.Month, dtCurrent.Day, 0, 0, 0)

            'e.g. date valid: 1 jan 07 00:00 s/d 2 jan 07 23:59
            cc.opAnd(New Criteria(GetType(AlertMaster), "DateValidFrom", MatchType.LesserOrEqual, dtValid))
            cc.opAnd(New Criteria(GetType(AlertMaster), "DateValidTo", MatchType.GreaterOrEqual, dtValid))

            Dim timeStartFrom As DateTime = New DateTime(1900, 1, 1, dtCurrent.Hour, dtCurrent.Minute, 0)
            Dim timeStartTo As DateTime = New DateTime(1900, 1, 1, dtCurrent.Hour, dtCurrent.Minute, 0)
            timeStartTo = timeStartTo

            cc.opAnd(New Criteria(GetType(AlertMaster), "IsViaDashboard", MatchType.Exact, 1))

            cc.opAnd(New Criteria(GetType(AlertMaster), "TimeStartFrom", MatchType.LesserOrEqual, timeStartFrom), "((", True)
            cc.opAnd(New Criteria(GetType(AlertMaster), "TimeStartTo", MatchType.GreaterOrEqual, timeStartTo), "))", False)

            cc.opAnd(New Criteria(GetType(AlertMaster), "ID", MatchType.InSet, "(select alertmasterid from alertgroup where usergroupid in (select UserGroupID from usergroupmember where userid=" & objUserInfo.ID & "))"))

            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            Dim sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection
            sortColl.Add(New Sort(GetType(AlertMaster), "ID", sortDirection.DESC))

            'Dim arl As ArrayList = Retrieve(cc)
            Dim arl As ArrayList = Retrieve(cc, sortColl)
            For i As Integer = 0 To arl.Count - 1
                If i > arl.Count - 1 Then
                    Exit For
                End If

                Dim objAlert As AlertMaster = CType(arl(i), AlertMaster)

                If Not DueAlert(objAlert) Or (Not objAlert.AlertModul.AlertCategory.Description.Trim().ToLower() = modulCategory.Trim().ToLower()) Then
                    arl.RemoveAt(i)
                    i -= 1
                End If
            Next
            Return arl
            'Else
            'Return New ArrayList
            'End If
        End Function
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is AlertMaster) Then
                CType(InsertArg.DomainObject, AlertMaster).ID = InsertArg.ID
                CType(InsertArg.DomainObject, AlertMaster).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is AlertStatus) Then
                CType(InsertArg.DomainObject, AlertStatus).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is AlertSound) Then
                CType(InsertArg.DomainObject, AlertSound).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is AlertGroup) Then
                CType(InsertArg.DomainObject, AlertGroup).ID = InsertArg.ID
            End If
        End Sub
        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.AlertMaster, ByVal listStatuss As ArrayList, ByVal listGroups As ArrayList, ByVal listSounds As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    objDomain.NextRunForAlertBox = New DateTime(1900, 1, 1, 0, 0, 0)
                    objDomain.NextRunForDashboard = New DateTime(1900, 1, 1, 0, 0, 0)
                    objDomain.NextRunForEmail = New DateTime(1900, 1, 1, 0, 0, 0)
                    objDomain.NextRunForSMS = New DateTime(1900, 1, 1, 0, 0, 0)

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each status As AlertStatus In listStatuss
                        status.AlertMaster = objDomain
                        m_TransactionManager.AddInsert(status, m_userPrincipal.Identity.Name)
                    Next
                    For Each sound As AlertSound In listSounds
                        sound.AlertMaster = objDomain
                        m_TransactionManager.AddInsert(sound, m_userPrincipal.Identity.Name)
                    Next
                    For Each grp As AlertGroup In listGroups
                        grp.AlertMaster = objDomain
                        m_TransactionManager.AddInsert(grp, m_userPrincipal.Identity.Name)
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
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function
        Public Function Update(ByVal objDomain As KTB.DNet.Domain.AlertMaster, ByVal listStatuss As ArrayList, ByVal listGroups As ArrayList, ByVal listSounds As ArrayList) As Integer
            Return Update(objDomain, listStatuss, listGroups, listSounds, True)
        End Function

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.AlertMaster, ByVal listStatuss As ArrayList, ByVal listGroups As ArrayList, ByVal listSounds As ArrayList, ByVal IsResetAllNextRun As Boolean) As Integer
            Dim returnValue As Integer = -1
            Dim isExist As Boolean
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each OldStatus As AlertStatus In objDomain.AlertStatuss
                        isExist = False
                        For Each Status As AlertStatus In listStatuss
                            If OldStatus.Status = Status.Status Then
                                isExist = True
                                Exit For
                            End If
                        Next

                        If Not isExist Then
                            m_TransactionManager.AddDelete(oldStatus)
                        End If
                    Next

                    For Each Status As AlertStatus In listStatuss
                        isExist = False
                        For Each OldStatus As AlertStatus In objDomain.AlertStatuss
                            If Status.Status = OldStatus.Status Then
                                Status = OldStatus
                                isExist = True
                                Exit For
                            End If
                        Next

                        If isExist Then
                            m_TransactionManager.AddUpdate(Status, m_userPrincipal.Identity.Name)
                        Else
                            Status.AlertMaster = objDomain
                            m_TransactionManager.AddInsert(Status, m_userPrincipal.Identity.Name)
                        End If
                    Next


                    For Each OldGroup As AlertGroup In objDomain.AlertGroups
                        isExist = False
                        For Each Group As AlertGroup In listGroups
                            If OldGroup.UserGroup.ID = Group.UserGroup.ID Then
                                isExist = True
                                Exit For
                            End If
                        Next

                        If Not isExist Then
                            m_TransactionManager.AddDelete(OldGroup)
                        End If
                    Next

                    For Each Group As AlertGroup In listGroups
                        isExist = False
                        For Each OldGroup As AlertGroup In objDomain.AlertGroups
                            If Group.UserGroup.ID = OldGroup.UserGroup.ID Then
                                Group = OldGroup
                                isExist = True
                                Exit For
                            End If
                        Next

                        If isExist Then
                            m_TransactionManager.AddUpdate(Group, m_userPrincipal.Identity.Name)
                        Else
                            Group.AlertMaster = objDomain
                            m_TransactionManager.AddInsert(Group, m_userPrincipal.Identity.Name)
                        End If
                    Next

                    For Each Sound As AlertSound In listSounds
                        If Sound.ID <= 0 Then
                            Sound.AlertMaster = objDomain
                            m_TransactionManager.AddInsert(Sound, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddUpdate(Sound, m_userPrincipal.Identity.Name)
                        End If
                    Next

                    If IsResetAllNextRun Then
                        objDomain.NextRunForAlertBox = New DateTime(1900, 1, 1, 0, 0, 0)
                        objDomain.NextRunForDashboard = New DateTime(1900, 1, 1, 0, 0, 0)
                        objDomain.NextRunForEmail = New DateTime(1900, 1, 1, 0, 0, 0)
                        objDomain.NextRunForSMS = New DateTime(1900, 1, 1, 0, 0, 0)
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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


        Public Sub Delete(ByVal objDomain As KTB.DNet.Domain.AlertMaster)
            Dim nResult As Integer = -1
            Try
                'objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_AlertMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
#End Region

    End Class
End Namespace
