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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 8/27/2007 - 1:13:09 PM
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
Imports ktb.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.ShowroomAudit

    Public Class AuditScheduleFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AuditScheduleMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_AuditScheduleMapper = MapperFactory.GetInstance.GetMapper(GetType(AuditSchedule).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(AuditSchedule))
            Me.DomainTypeCollection.Add(GetType(AuditScheduleAuditor))
            Me.DomainTypeCollection.Add(GetType(AuditScheduleDealer))

            'AddHandler m_TransactionManager.Update, New TransactionManager.OnUpdateEventHandler(AddressOf m_TransactionManager_Update)
            'Me.DomainTypeCollection.Add(GetType(AuditSchedule))
            'Me.DomainTypeCollection.Add(GetType(AuditScheduleAuditor))
            'Me.DomainTypeCollection.Add(GetType(AuditScheduleDealer))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AuditSchedule
            Return CType(m_AuditScheduleMapper.Retrieve(ID), AuditSchedule)
        End Function

        Public Function Retrieve(ByVal Code As String) As AuditSchedule
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AuditSchedule), "AuditScheduleCode", MatchType.Exact, Code))

            Dim AuditScheduleColl As ArrayList = m_AuditScheduleMapper.RetrieveByCriteria(criterias)
            If (AuditScheduleColl.Count > 0) Then
                Return CType(AuditScheduleColl(0), AuditSchedule)
            End If
            Return New AuditSchedule
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AuditScheduleMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AuditScheduleMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AuditScheduleMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditSchedule), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AuditScheduleMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditSchedule), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AuditScheduleMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AuditSchedule As ArrayList = m_AuditScheduleMapper.RetrieveByCriteria(criterias)
            Return _AuditSchedule
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AuditScheduleColl As ArrayList = m_AuditScheduleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AuditScheduleColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AuditScheduleColl As ArrayList = m_AuditScheduleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return AuditScheduleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AuditScheduleColl As ArrayList = m_AuditScheduleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AuditSchedule), columnName, matchOperator, columnValue))
            Return AuditScheduleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditSchedule), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), columnName, matchOperator, columnValue))

            Return m_AuditScheduleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "AuditScheduleCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(AuditSchedule), "AuditScheduleCode", AggregateType.Count)
            Return CType(m_AuditScheduleMapper.RetrieveScalar(agg, crit), Integer)
        End Function


        Public Function InsertSchedule(ByVal objDomain As KTB.DNet.Domain.AuditSchedule, ByVal arrAuditor As ArrayList, ByVal arrdeatailDealer As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If arrAuditor.Count > 0 Then
                        For Each objAuditor As AuditScheduleAuditor In arrAuditor
                            objAuditor.AuditSchedule = objDomain
                            m_TransactionManager.AddInsert(objAuditor, m_userPrincipal.Identity.Name)
                        Next
                    End If
                   
                    If arrdeatailDealer.Count > 0 Then
                        For Each objDetailDealer As AuditScheduleDealer In arrdeatailDealer
                            objDetailDealer.AuditSchedule = objDomain
                            m_TransactionManager.AddInsert(objDetailDealer, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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


        Public Function UpdateSchedule(ByVal arrAuditSchedule As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    'm_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    'If arrAuditor.Count > 0 Then
                    '    For Each objAuditor As AuditScheduleAuditor In arrAuditor
                    '        objAuditor.AuditSchedule = objDomain
                    '        m_TransactionManager.AddInsert(objAuditor, m_userPrincipal.Identity.Name)
                    '    Next
                    'End If

                    'If arrdeatailDealer.Count > 0 Then
                    '    For Each objDetailDealer As AuditScheduleDealer In arrdeatailDealer
                    '        objDetailDealer.AuditSchedule = objDomain
                    '        m_TransactionManager.AddInsert(objDetailDealer, m_userPrincipal.Identity.Name)
                    '    Next
                    'End If
                    Dim itemSchedule As AuditSchedule

                    For idx As Integer = 0 To arrAuditSchedule.Count - 1
                        itemSchedule = arrAuditSchedule(idx)
                        m_TransactionManager.AddUpdate(itemSchedule, m_userPrincipal.Identity.Name)

                    Next
                    m_TransactionManager.PerformTransaction()
                    returnValue = itemSchedule.ID
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

        Public Function UpdateSchedule(ByVal objDomain As KTB.DNet.Domain.AuditSchedule, ByVal arrAuditor As ArrayList, ByVal arrdeatailDealer As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                   

                    If arrdeatailDealer.Count > 0 Then
                        For Each objDetailDealer As AuditScheduleDealer In arrdeatailDealer
                            If objDetailDealer.ID = 0 Then
                                objDetailDealer.AuditSchedule = objDomain
                                m_TransactionManager.AddInsert(objDetailDealer, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddUpdate(objDetailDealer, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arrAuditor.Count > 0 Then
                        For Each objAuditor As AuditScheduleAuditor In arrAuditor
                            If objAuditor.ID = 0 Then
                                objAuditor.AuditSchedule = objDomain
                                m_TransactionManager.AddInsert(objAuditor, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddUpdate(objAuditor, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        Public Function TransactionOfSchedule(ByVal objDomain As KTB.DNet.Domain.AuditSchedule, ByVal arrAuditor As ArrayList, ByVal arrdeatailDealer As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim arlCurrent As ArrayList = New ArrayList
            Dim arlCurrentScheduleDealer As ArrayList = New ArrayList

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    '---Auditor
                    For Each objAuditor As AuditScheduleAuditor In objDomain.AuditScheduleAuditors
                        objAuditor.RowStatus = DBRowStatus.Deleted
                        arlCurrent.Add(objAuditor)
                    Next
                    If arrAuditor.Count > 0 Then
                        For Each itemUpdate As AuditScheduleAuditor In arrAuditor
                            If itemUpdate.ID > 0 Then
                                For i As Integer = 0 To arlCurrent.Count - 1
                                    Dim item As AuditScheduleAuditor = arlCurrent(i)
                                    If item.ID = itemUpdate.ID Then
                                        item.Dealer = itemUpdate.Dealer
                                        item.AuditorType = itemUpdate.AuditorType
                                        item.Auditor = itemUpdate.Auditor
                                        item.StartDate = itemUpdate.StartDate
                                        item.EndDate = itemUpdate.EndDate
                                        arlCurrent(i) = item
                                    End If
                                Next
                            Else
                                arlCurrent.Add(itemUpdate)
                            End If
                        Next
                        For Each itemAuditor As AuditScheduleAuditor In arlCurrent
                            itemAuditor.AuditSchedule = objDomain
                            If itemAuditor.ID = 0 Then
                                m_TransactionManager.AddInsert(itemAuditor, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddUpdate(itemAuditor, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    '---ScheduleDealer
                    arlCurrent = New ArrayList
                    For Each objDealer As AuditScheduleDealer In objDomain.AuditScheduleDealers
                        objDealer.RowStatus = DBRowStatus.Deleted
                        arlCurrent.Add(objDealer)
                    Next

                    If arrdeatailDealer.Count > 0 Then
                        For Each itemUpdate As AuditScheduleDealer In arrdeatailDealer
                            If itemUpdate.ID > 0 Then
                                For i As Integer = 0 To arlCurrent.Count - 1
                                    Dim item As AuditScheduleDealer = arlCurrent(i)
                                    If item.ID = itemUpdate.ID Then
                                        item.RowStatus = DBRowStatus.Active
                                        item.Dealer = itemUpdate.Dealer
                                        arlCurrent(i) = item
                                        Exit For
                                    End If
                                Next
                            Else
                                arlCurrent.Add(itemUpdate)
                            End If
                        Next
                        For Each itemDealer As AuditScheduleDealer In arlCurrent
                            itemDealer.AuditSchedule = objDomain
                            If itemDealer.ID = 0 Then
                                m_TransactionManager.AddInsert(itemDealer, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddUpdate(itemDealer, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID

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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is AuditSchedule) Then
                CType(InsertArg.DomainObject, AuditSchedule).ID = InsertArg.ID
                CType(InsertArg.DomainObject, AuditSchedule).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is AuditScheduleAuditor) Then
                CType(InsertArg.DomainObject, AuditScheduleAuditor).ID = InsertArg.ID

            ElseIf (TypeOf InsertArg.DomainObject Is AuditScheduleDealer) Then
                CType(InsertArg.DomainObject, AuditScheduleDealer).ID = InsertArg.ID
            End If
        End Sub

        'Private Sub m_TransactionManager_Update(ByVal sender As Object, ByVal UpdateArg As TransactionManager.OnUpdateArgs)

        '    If (TypeOf UpdateArg.DomainObject Is AuditSchedule) Then
        '        CType(UpdateArg.DomainObject, AuditSchedule).ID = UpdateArg.ID
        '        CType(UpdateArg.DomainObject, AuditSchedule).MarkLoaded()

        '    ElseIf (TypeOf UpdateArg.DomainObject Is AuditScheduleAuditor) Then
        '        CType(UpdateArg.DomainObject, AuditScheduleAuditor).ID = UpdateArg.ID

        '    ElseIf (TypeOf UpdateArg.DomainObject Is AuditScheduleDealer) Then
        '        CType(UpdateArg.DomainObject, AuditScheduleDealer).ID = UpdateArg.ID
        '    End If
        'End Sub
#End Region

#Region "Custom Method"
       
#End Region

    End Class

End Namespace

