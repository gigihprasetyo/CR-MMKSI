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
'// Generated on 9/26/2005 - 2:38:25 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Web.Mail
Imports System.Text

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.UserInfo
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.DataMapper
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.General

Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class PQRHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PQRHeaderMapper As IMapper
        Private m_PQRChangesHistoryMapper As IMapper
        Private m_TransactionManager As TransactionManager
        Private msgMail As New MailMessage
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_PQRHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(PQRHeader).ToString)
            Me.m_PQRChangesHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(PQRChangesHistory).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PQRHeader
            Return CType(m_PQRHeaderMapper.Retrieve(ID), PQRHeader)
        End Function

        Public Function Retrieve(ByVal PQRNo As String) As PQRHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "PQRNo", MatchType.Exact, PQRNo))

            Dim partColl As ArrayList = m_PQRHeaderMapper.RetrieveByCriteria(criterias)
            If (partColl.Count > 0) Then
                Return CType(partColl(0), PQRHeader)
            End If
            Return New PQRHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PQRHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PQRHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PQRHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PQRHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PQRHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PQRHeader As ArrayList = m_PQRHeaderMapper.RetrieveByCriteria(criterias)
            Return _PQRHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRHeaderColl As ArrayList = m_PQRHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PQRHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRHeaderColl As ArrayList = m_PQRHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return PQRHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal ExtModel As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PQRHeaderColl As ArrayList = m_PQRHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return PQRHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PQRHeaderColl As ArrayList = m_PQRHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PQRHeaderColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim PQRHeaderColl As ArrayList = m_PQRHeaderMapper.RetrieveByCriteria(criterias)
            Return PQRHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(PQRHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PQRHeaderColl As ArrayList = m_PQRHeaderMapper.RetrieveByCriteria(criterias, sortColl)
            Return PQRHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(PQRHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PQRHeaderColl As ArrayList = m_PQRHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PQRHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PQRHeader), columnName, matchOperator, columnValue))
            Dim PQRHeaderColl As ArrayList = m_PQRHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PQRHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PQRHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PQRHeader), columnName, matchOperator, columnValue))

            Return m_PQRHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_PQRHeaderMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sorts As ICollection) As ArrayList
            Return m_PQRHeaderMapper.RetrieveByCriteria(Criterias, sorts)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function RetrieveListCategory() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Me.m_PQRHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(Category).ToString)
            Return m_PQRHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveListType() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Me.m_PQRHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(VechileColor).ToString)
            Return m_PQRHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function ValidateCode(ByVal PQRNo As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "PQRNo", MatchType.Exact, PQRNo))
            Dim agg As Aggregate = New Aggregate(GetType(PQRHeader), "PQRNo", AggregateType.Count)
            Return CType(m_PQRHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function InsertTransaction(ByVal objPQRHeader As PQRHeader, ByVal arrPQRDamageCode As ArrayList, ByVal arrPQRParts As ArrayList, ByVal arrPQRAttachmentTop As ArrayList, ByVal arrPQRAttachmentBottom As ArrayList, ByVal ProfileCollection1 As ArrayList, ByVal ProfileCollection2 As ArrayList, ByVal arrPQRQRS As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objPQRHeader, m_userPrincipal.Identity.Name)

                    Dim objHistory As New PQRChangesHistory
                    objHistory.PQRHeader = objPQRHeader
                    objHistory.OldStatus = ""
                    objHistory.NewStatus = objPQRHeader.RowStatus
                    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)

                    If arrPQRDamageCode.Count > 0 Then
                        For Each oPQRDamageCode As PQRDamageCode In arrPQRDamageCode
                            oPQRDamageCode.PQRHeader = objPQRHeader
                            m_TransactionManager.AddInsert(oPQRDamageCode, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrPQRParts.Count > 0 Then
                        For Each oPQRParts As PQRPartsCode In arrPQRParts
                            oPQRParts.PQRHeader = objPQRHeader
                            m_TransactionManager.AddInsert(oPQRParts, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrPQRAttachmentTop.Count > 0 Then
                        For Each oPQRAttachmentTop As PQRAttachment In arrPQRAttachmentTop
                            oPQRAttachmentTop.PQRHeader = objPQRHeader
                            m_TransactionManager.AddInsert(oPQRAttachmentTop, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrPQRAttachmentBottom.Count > 0 Then
                        For Each oPQRAttachmentBottom As PQRAttachment In arrPQRAttachmentBottom
                            oPQRAttachmentBottom.PQRHeader = objPQRHeader
                            m_TransactionManager.AddInsert(oPQRAttachmentBottom, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrPQRQRS.Count > 0 Then
                        For Each oPQRQRS As PQRQRS In arrPQRQRS
                            oPQRQRS.PQRHeader = objPQRHeader
                            m_TransactionManager.AddInsert(oPQRQRS, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If ProfileCollection1.Count > 0 Then
                        'Modified from me
                        For Each item As PQRProfile In ProfileCollection1
                            item.PQRHeader = objPQRHeader
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)

                            Dim NewProfileHistory As New PQRProfileHistory
                            NewProfileHistory.PQRProfile = item
                            NewProfileHistory.ProvileValue = item.ProfileValue
                            m_TransactionManager.AddInsert(NewProfileHistory, m_userPrincipal.Identity.Name)
                        Next

                        ' From Salesman header
                        'For Each item As PQRProfile In ProfileCollection1
                        '    item.PQRHeader = objPQRHeader
                        '    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        '    For Each items As PQRProfileHistory In item.PQRProfileHistorys
                        '        items.PQRProfile = item
                        '        m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        '    Next
                        'Next

                    End If

                    If ProfileCollection2.Count > 0 Then
                        'Modified from me
                        For Each item As PQRProfile In ProfileCollection2
                            item.PQRHeader = objPQRHeader
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)

                            Dim NewProfileHistory As New PQRProfileHistory
                            NewProfileHistory.PQRProfile = item
                            NewProfileHistory.ProvileValue = item.ProfileValue
                            m_TransactionManager.AddInsert(NewProfileHistory, m_userPrincipal.Identity.Name)
                        Next

                        ' From Salesman header
                        'For Each item As PQRProfile In ProfileCollection2
                        '    item.PQRHeader = objPQRHeader
                        '    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        '    For Each items As PQRProfileHistory In item.PQRProfileHistorys
                        '        items.PQRProfile = item
                        '        m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        '    Next
                        'Next

                    End If

                    'If arrPQRSolutionReferences.Count > 0 Then
                    '    For Each oPQRSolutionReferences As PQRSolutionReferences In arrPQRSolutionReferences
                    '        oPQRSolutionReferences.PQRHeader = objPQRHeader
                    '        m_TransactionManager.AddInsert(oPQRSolutionReferences, m_userPrincipal.Identity.Name)
                    '    Next
                    'End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objPQRHeader.ID
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

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PQRHeader) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PQRDamageCode) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRDamageCode).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PQRPartsCode) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRPartsCode).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PQRAttachment) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRAttachment).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PQRProfile) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRProfile).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRProfile).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PQRProfileHistory) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PQRProfileHistory).ID = InsertArg.ID
            End If

        End Sub

        'Public Function Insert(ByVal objDomain As PQRHeader) As Integer
        '    Dim iReturn As Integer = -2
        '    Try
        '        m_PQRHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        Dim s As String = ex.Message
        '        iReturn = -1
        '    End Try
        '    Return iReturn

        'End Function

        Private Function Update(ByVal objDomain As PQRHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PQRHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteTransaction(ByVal objDomain As PQRHeader)
            Dim ErrMessage As String = String.Empty
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)

                For Each item As PQRDamageCode In objDomain.PQRDamageCodes
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next

                For Each item As PQRPartsCode In objDomain.PQRPartsCodes
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next

                For Each item As PQRAttachment In objDomain.PQRAttachments
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next

                For Each item As PQRAdditionalInfo In objDomain.PQRAdditionalInfos
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next

                For Each item As PQRQRS In objDomain.PQRQRSs
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next

                For Each item As PQRSolutionReferences In objDomain.PQRSolutionReferencess
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next

                UpdateTransaction(objDomain, objDomain.PQRDamageCodes, objDomain.PQRPartsCodes, objDomain.PQRAttachments, objDomain.PQRQRSs, ErrMessage)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function UpdateTransaction(ByVal objDomain As PQRHeader, ByVal arrPQRDamageCode As ArrayList, ByVal arrDeletedPQRDamageCode As ArrayList, ByVal arrPQRParts As ArrayList, ByVal arrDeletedPQRParts As ArrayList, ByVal arrPQRAttachmentTop As ArrayList, ByVal arrPQRAttachmentBottom As ArrayList, ByVal arrDeletedPQRAttachment As ArrayList, ByVal ProfileCollection1 As ArrayList, ByVal ProfileCollection2 As ArrayList, ByVal objGroup1 As ProfileGroup, ByVal objGroup2 As ProfileGroup, ByVal arrPQRQRS As ArrayList, ByVal arrDeletedPQRQRS As ArrayList, ByRef ErrMsg As String) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If IsStatusChanges(objDomain) Then
                        ErrMsg = "Status Dokumen PQR telah berubah. Proses dibatalkan."
                        Return returnValue
                    End If


                    If arrDeletedPQRDamageCode.Count > 0 Then
                        For Each item As PQRDamageCode In arrDeletedPQRDamageCode
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDeletedPQRParts.Count > 0 Then
                        For Each item As PQRPartsCode In arrDeletedPQRParts
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDeletedPQRAttachment.Count > 0 Then
                        For Each item As PQRAttachment In arrDeletedPQRAttachment
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDeletedPQRQRS.Count > 0 Then
                        For Each item As PQRQRS In arrDeletedPQRQRS
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrPQRDamageCode.Count > 0 Then

                        For Each item As PQRDamageCode In arrPQRDamageCode
                            item.PQRHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If


                    If arrPQRParts.Count > 0 Then
                        For Each item As PQRPartsCode In arrPQRParts
                            item.PQRHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If

                    If arrPQRAttachmentTop.Count > 0 Then
                        For Each item As PQRAttachment In arrPQRAttachmentTop
                            item.PQRHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arrPQRAttachmentBottom.Count > 0 Then
                        For Each item As PQRAttachment In arrPQRAttachmentBottom

                            item.PQRHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If


                    If arrPQRQRS.Count > 0 Then

                        For Each item As PQRQRS In arrPQRQRS
                            item.PQRHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If

                    If ProfileCollection1.Count > 0 Then
                        'Modified from me 
                        For Each item As PQRProfile In ProfileCollection1
                            Dim oldProfile1 As PQRProfile = GetPQRProfile(objDomain, objGroup1, item.ProfileHeader)
                            If oldProfile1.ID > 0 Then
                                'For Each items As PQRProfileHistory In oldProfile1.PQRProfileHistorys
                                '    items.PQRProfile = oldProfile1
                                '    m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)
                                'Next

                                Dim NewProfileHistory As New PQRProfileHistory
                                NewProfileHistory.PQRProfile = oldProfile1
                                NewProfileHistory.ProvileValue = oldProfile1.ProfileValue
                                m_TransactionManager.AddInsert(NewProfileHistory, m_userPrincipal.Identity.Name)

                                oldProfile1.ProfileValue = item.ProfileValue
                                m_TransactionManager.AddUpdate(oldProfile1, m_userPrincipal.Identity.Name)

                            Else
                                'item.MarkLoaded()
                                item.PQRHeader = objDomain
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)

                                Dim NewProfileHistory As New PQRProfileHistory
                                NewProfileHistory.PQRProfile = item
                                NewProfileHistory.ProvileValue = item.ProfileValue
                                m_TransactionManager.AddInsert(NewProfileHistory, m_userPrincipal.Identity.Name)

                            End If
                        Next

                        ' From salesman
                        'For Each item As PQRProfile In ProfileCollection1
                        '    item.PQRHeader = objDomain
                        '    Dim oldProfile1 As PQRProfile = GetPQRProfile(objDomain, objGroup1, item.ProfileHeader)
                        '    If oldProfile1.ID > 0 Then
                        '        For Each items As PQRProfileHistory In item.PQRProfileHistorys
                        '            items.PQRProfile = oldProfile1
                        '            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        '        Next
                        '        oldProfile1.ProfileValue = item.ProfileValue
                        '        m_TransactionManager.AddUpdate(oldProfile1, m_userPrincipal.Identity.Name)
                        '    Else
                        '        item.PQRHeader = objDomain
                        '        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        '        For Each items As PQRProfileHistory In item.PQRProfileHistorys
                        '            items.PQRProfile = item
                        '            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        '        Next
                        '        oldProfile1.ProfileValue = item.ProfileValue
                        '    End If

                        'Next

                    End If

                    If ProfileCollection2.Count > 0 Then
                        'Modified from me 
                        For Each item As PQRProfile In ProfileCollection2
                            Dim oldProfile1 As PQRProfile = GetPQRProfile(objDomain, objGroup2, item.ProfileHeader)
                            If oldProfile1.ID > 0 Then
                                'For Each items As PQRProfileHistory In oldProfile1.PQRProfileHistorys
                                '    items.PQRProfile = oldProfile1
                                '    m_TransactionManager.AddUpdate(items, m_userPrincipal.Identity.Name)
                                'Next

                                Dim NewProfileHistory As New PQRProfileHistory
                                NewProfileHistory.PQRProfile = oldProfile1
                                NewProfileHistory.ProvileValue = oldProfile1.ProfileValue
                                m_TransactionManager.AddInsert(NewProfileHistory, m_userPrincipal.Identity.Name)

                                oldProfile1.ProfileValue = item.ProfileValue
                                m_TransactionManager.AddUpdate(oldProfile1, m_userPrincipal.Identity.Name)

                            Else
                                'item.MarkLoaded()
                                item.PQRHeader = objDomain
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)

                                Dim NewProfileHistory As New PQRProfileHistory
                                NewProfileHistory.PQRProfile = item
                                NewProfileHistory.ProvileValue = item.ProfileValue
                                m_TransactionManager.AddInsert(NewProfileHistory, m_userPrincipal.Identity.Name)

                            End If
                        Next

                        ' From salesman
                        'For Each item As PQRProfile In ProfileCollection2
                        '    item.PQRHeader = objDomain
                        '    Dim oldProfile1 As PQRProfile = GetPQRProfile(objDomain, objGroup2, item.ProfileHeader)
                        '    If oldProfile1.ID > 0 Then
                        '        For Each items As PQRProfileHistory In item.PQRProfileHistorys
                        '            items.PQRProfile = oldProfile1
                        '            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        '        Next
                        '        oldProfile1.ProfileValue = item.ProfileValue
                        '        m_TransactionManager.AddUpdate(oldProfile1, m_userPrincipal.Identity.Name)
                        '    Else
                        '        item.PQRHeader = objDomain
                        '        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        '        For Each items As PQRProfileHistory In item.PQRProfileHistorys
                        '            items.PQRProfile = item
                        '            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        '        Next
                        '        oldProfile1.ProfileValue = item.ProfileValue
                        '    End If

                        'Next

                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    'For i As Integer = 0 To objDomain.PKDetail.Count - 1
                    'CType(objDomain.PKDetail(i), PKDetail).PKHeader = objDomain
                    'm_TransactionManager.AddInsert(objDomain.PKDetail(i), m_userPrincipal.Identity.Name)
                    'Next
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

        Public Function UpdateTransaction(ByVal objDomain As PQRHeader, ByVal arrPQRDamageCode As ArrayList, ByVal arrPQRParts As ArrayList, ByVal arrPQRAttachment As ArrayList, ByVal arrPQRQRS As ArrayList, ByRef ErrMsg As String) As Integer
            Dim returnValue As Short = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If IsStatusChanges(objDomain) Then
                        ErrMsg = "Status Dokumen PQR telah berubah. Proses dibatalkan."
                        Return returnValue
                    End If

                    'For Each item As PKPayment In objDomain.PKPayments
                    '    item.PKHeader = objDomain
                    '    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    'Next

                    If arrPQRDamageCode.Count > 0 Then

                        For Each item As PQRDamageCode In arrPQRDamageCode
                            item.PQRHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If


                    If arrPQRParts.Count > 0 Then
                        For Each item As PQRPartsCode In arrPQRParts
                            item.PQRHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If

                    If arrPQRAttachment.Count > 0 Then
                        For Each item As PQRAttachment In arrPQRAttachment

                            item.PQRHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arrPQRQRS.Count > 0 Then

                        For Each item As PQRQRS In arrPQRQRS
                            item.PQRHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If


                    'For Each item As PQRAdditionalInfo In objDomain.PQRAdditionalInfos
                    '    item.PQRHeader = objDomain
                    '    If item.ID <> 0 Then
                    '        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    '    Else
                    '        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    '    End If

                    'Next
                    'For Each item As PQRSolutionReferences In objDomain.PQRSolutionReferencess
                    '    item.PQRHeader = objDomain
                    '    If item.ID <> 0 Then
                    '        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    '    Else
                    '        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    '    End If

                    'Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    'For i As Integer = 0 To objDomain.PKDetail.Count - 1
                    'CType(objDomain.PKDetail(i), PKDetail).PKHeader = objDomain
                    'm_TransactionManager.AddInsert(objDomain.PKDetail(i), m_userPrincipal.Identity.Name)
                    'Next
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

        Public Function GetPQRProfile(ByVal objPQRHeader As PQRHeader, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As PQRProfile
            Dim objFacade As Profile.PQRProfileFacade = New Profile.PQRProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PQRProfile), "PQRHeader.ID", MatchType.Exact, objPQRHeader.ID))
            criterias.opAnd(New Criteria(GetType(PQRProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(PQRProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))
            Dim objListPQRProfile As ArrayList = objFacade.Retrieve(criterias)
            If objListPQRProfile.Count > 0 Then
                Return CType(objListPQRProfile(0), PQRProfile)
            End If
            Return New PQRProfile
        End Function

        'Public Sub DeleteFromDB(ByVal objDomain As PQRHeader)
        '    Try
        '        m_PQRHeaderMapper.Delete(objDomain)
        '    Catch ex As Exception
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '        If rethrow Then
        '            Throw
        '        End If
        '    End Try
        'End Sub


#End Region

#Region "Custom Method"

        Private Function IsStatusChanges(ByVal objDomain As PQRHeader) As Boolean
            If objDomain.ID > 0 Then
                Dim CurrPQRHeader As PQRHeader = m_PQRHeaderMapper.Retrieve(objDomain.ID)
                If objDomain.RowStatus <> CurrPQRHeader.RowStatus Then
                    Return True
                End If
            End If
            Return False
        End Function

        Public Function UbahStatusPQRDocument(ByVal listObjDomain As ArrayList, ByVal NewStatus As EnumPQR.PQRStatus, ByRef ErrMsg As String, Optional ByVal DefaultEmailPIC As String = "") As Integer
            Dim result As Integer = -1

            For Each item As PQRHeader In listObjDomain
                If IsStatusChanges(item) Then
                    ErrMsg = "Status Dokumen PQR no : " & item.PQRNo & " telah berubah. Proses dibatalkan."
                    Return result
                End If
            Next

            For Each item As PQRHeader In listObjDomain
                If NewStatus = EnumPQR.PQRStatus.Selesai Then
                    If item.Bobot = 0 Then
                        ErrMsg = "Bobot PQR no : " & item.PQRNo & " masih 0. Proses selesai di batalkan"
                        Return result
                    End If
                End If
            Next

            Select Case NewStatus
                Case EnumPQR.PQRStatus.Batal
                    For Each objDomain As PQRHeader In listObjDomain
                        result = BatalPQRDocument(objDomain)
                        If result = -1 Then Exit For
                    Next
                Case EnumPQR.PQRStatus.Baru
                    For Each objDomain As PQRHeader In listObjDomain
                        result = BaruPQRDocument(objDomain)
                        If result = -1 Then Exit For
                    Next
                Case EnumPQR.PQRStatus.Validasi
                    For Each objDomain As PQRHeader In listObjDomain
                        result = ValidasiPQRDocument(objDomain)
                        If result = -1 Then Exit For
                    Next
                Case EnumPQR.PQRStatus.Proses
                    For Each objDomain As PQRHeader In listObjDomain
                        result = ProsesPQRDocument(objDomain)
                        If result = -1 Then Exit For
                    Next
                Case EnumPQR.PQRStatus.Rilis
                    For Each objDomain As PQRHeader In listObjDomain
                        result = RilisPQRDocument(objDomain)
                        If result = -1 Then Exit For
                    Next
                Case EnumPQR.PQRStatus.Selesai
                    For Each objDomain As PQRHeader In listObjDomain
                        result = SelesaiPQRDocument(objDomain, DefaultEmailPIC)
                        If result = -1 Then
                            Exit For
                        End If
                    Next
            End Select

            Return result
        End Function
        Public Function UbahStatusPQRDocument(ByVal ObjDomain As PQRHeader, ByVal NewStatus As EnumPQR.PQRStatus, ByRef ErrMsg As String, Optional ByVal DefaultEmailPIC As String = "") As Integer
            Dim result As Integer = -1

            If IsStatusChanges(ObjDomain) Then
                ErrMsg = "Status Dokumen PQR telah berubah. Proses dibatalkan."
                Return result
            End If

            If NewStatus = EnumPQR.PQRStatus.Selesai Then
                If ObjDomain.Bobot = 0 Then
                    ErrMsg = "Bobot PQR masih 0. Proses selesai di batalkan"
                    Return result
                End If
            End If

            Select Case NewStatus
                Case EnumPQR.PQRStatus.Batal
                    result = BatalPQRDocument(ObjDomain)
                Case EnumPQR.PQRStatus.Baru
                    result = BaruPQRDocument(ObjDomain)
                Case EnumPQR.PQRStatus.Validasi
                    result = ValidasiPQRDocument(ObjDomain)
                Case EnumPQR.PQRStatus.Proses
                    result = ProsesPQRDocument(ObjDomain)
                Case EnumPQR.PQRStatus.Rilis
                    result = RilisPQRDocument(ObjDomain)
                Case EnumPQR.PQRStatus.Selesai
                    result = SelesaiPQRDocument(ObjDomain, DefaultEmailPIC)
            End Select

            Return result
        End Function

        Public Function BatalPQRDocument(ByVal objDomain As PQRHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim objHistory As New PQRChangesHistory
                    objHistory.PQRHeader = objDomain
                    objHistory.OldStatus = objDomain.RowStatus
                    objHistory.NewStatus = EnumPQR.PQRStatus.Batal
                    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)

                    objDomain.RowStatus = EnumPQR.PQRStatus.Batal
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
        Public Function BaruPQRDocument(ByVal objDomain As PQRHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim objHistory As New PQRChangesHistory
                    objHistory.PQRHeader = objDomain
                    objHistory.OldStatus = objDomain.RowStatus
                    objHistory.NewStatus = EnumPQR.PQRStatus.Baru
                    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)

                    If objDomain.RowStatus = EnumPQR.PQRStatus.Validasi Then
                        objDomain.ValidationTime = New DateTime(1753, 1, 1)
                    End If
                    objDomain.RowStatus = EnumPQR.PQRStatus.Baru
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
        Public Function ValidasiPQRDocument(ByVal objDomain As PQRHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim objHistory As New PQRChangesHistory
                    objHistory.PQRHeader = objDomain
                    objHistory.OldStatus = objDomain.RowStatus
                    objHistory.NewStatus = EnumPQR.PQRStatus.Validasi
                    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)

                    If objDomain.RowStatus = EnumPQR.PQRStatus.Proses Then
                        objDomain.ConfirmBy = ""
                        objDomain.ConfirmTime = New DateTime(1753, 1, 1)
                    End If

                    objDomain.ValidationTime = DateTime.Now
                    objDomain.RowStatus = EnumPQR.PQRStatus.Validasi
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
        Public Function ProsesPQRDocument(ByVal objDomain As PQRHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim objHistory As New PQRChangesHistory
                    objHistory.PQRHeader = objDomain
                    objHistory.OldStatus = objDomain.RowStatus
                    objHistory.NewStatus = EnumPQR.PQRStatus.Proses
                    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)


                    If objDomain.RowStatus = EnumPQR.PQRStatus.Rilis Then
                        objDomain.Bobot = 0
                        objDomain.ReleaseBy = ""
                        objDomain.RealeseTime = New DateTime(1753, 1, 1)
                        objDomain.IntervalProcess = New DateTime(1753, 1, 1)
                    End If

                    objDomain.ConfirmBy = m_userPrincipal.Identity.Name
                    objDomain.ConfirmTime = DateTime.Now
                    objDomain.RowStatus = EnumPQR.PQRStatus.Proses
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
        Public Function RilisPQRDocument(ByVal objDomain As PQRHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim objHistory As New PQRChangesHistory
                    objHistory.PQRHeader = objDomain
                    objHistory.OldStatus = objDomain.RowStatus
                    objHistory.NewStatus = EnumPQR.PQRStatus.Rilis
                    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)

                    objDomain.ReleaseBy = m_userPrincipal.Identity.Name
                    objDomain.RealeseTime = DateTime.Now
                    Dim ts As TimeSpan = objDomain.RealeseTime.Subtract(objDomain.ValidationTime)
                    objDomain.IntervalProcess = New DateTime(1753, 1, 1, ts.Hours, ts.Minutes, ts.Seconds)
                    objDomain.RowStatus = EnumPQR.PQRStatus.Rilis
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
        Public Function SelesaiPQRDocument(ByVal objDomain As PQRHeader, Optional ByVal DefaultEmailPIC As String = "") As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim objHistory As New PQRChangesHistory
                    objHistory.PQRHeader = objDomain
                    objHistory.OldStatus = objDomain.RowStatus
                    objHistory.NewStatus = EnumPQR.PQRStatus.Selesai
                    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)

                    objDomain.FinishBy = m_userPrincipal.Identity.Name
                    objDomain.FinishDate = DateTime.Now
                    objDomain.RowStatus = EnumPQR.PQRStatus.Selesai
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    'for email notification
                    Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
                    Dim msgTitle As String = "Penjelasan MMKSI pada PQR D-Net No: " & objDomain.PQRNo
                    Dim msgFormat As String = MessageContent(objDomain)
                    Dim emailTo As String = GetEmailId(objDomain, 2, DefaultEmailPIC)
                    Dim emailCC As String = GetEmailId(objDomain, 3, DefaultEmailPIC)
                    If (GetSecondCC(objDomain.CreatedBy.Substring(0, 6)) <> "") Then
                        emailCC = emailCC & ";" & GetSecondCC(objDomain.CreatedBy.Substring(0, 6))
                    End If
                    Dim emailFrom As String = GetEmailId(objDomain, 1, DefaultEmailPIC)
                    '-----------------------------------
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID

                        Try
                            ' Kirim email ke created user, format akan di berikan nanti
                            SendEMail(emailTo, emailCC, "", emailFrom, msgTitle, MailFormat.Html, msgFormat, smtp)
                        Catch exx As Exception

                        End Try
                       
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

        Private Sub SendEMail(ByVal sendTo As String, ByVal ccTo1 As String, ByVal ccTo2 As String, ByVal from As String, ByVal subject As String, ByVal format As MailFormat, ByVal body As String, ByVal smtp As String)
            Try
                msgMail.To = sendTo
                Dim cc As String = String.Empty

                If ccTo1.Trim <> "" Then
                    cc = ccTo1
                End If

                If Not IsNothing(ccTo2) Then
                    If ccTo2.Trim <> "" Then
                        cc += ";" & ccTo2
                    End If
                End If

                msgMail.Cc = cc
                msgMail.From = from
                msgMail.Subject = subject
                msgMail.BodyFormat = format
                msgMail.Body = body
                'msgMail.Priority = _priority
                SmtpMail.SmtpServer = smtp
                SmtpMail.Send(msgMail)

            Catch ex As Exception

            End Try
        End Sub
        Private Function MessageContent(ByVal objDomain As PQRHeader) As String
            Dim sb As StringBuilder = New StringBuilder
            sb.Append("<table>")
            sb.Append("<tr><td colspan=2>Kepada</td></tr>")
            sb.Append("<tr><td colspan=2>Bapak/Ibu</td></tr>")
            sb.Append("<tr><td colspan=2>Service manager<br><br></td></tr>")
            sb.Append("<tr><td colspan=2>Kami telah menyelesaikan PQR No: <strong>" & objDomain.PQRNo & _
                    " </strong>yang diajukan pada tanggal " & objDomain.CreatedTime.ToString("dd MMMM yyyy") & _
                    ".<br></td></tr>")
            sb.Append("<tr><td colspan=2>Untuk lebih detail, silahkan dilihat di PQR D-Net.<br><br></td></tr>")
            sb.Append("<tr><td colspan=2>Terima Kasih<br><br><br></td></tr>")
            sb.Append("<tr><td></td>")
            sb.Append("<td align=center>Service Department<br></td></tr>")
            sb.Append("<tr><td></td>")
            sb.Append("<td align=center>PT Mitsubishi Motors Krama Yudha Sales Indonesia</td></tr>")
            sb.Append("<table>")
            Return sb.ToString
        End Function
        Private Function GetEmailId(ByVal objDomain As PQRHeader, ByVal userType As Integer, Optional ByVal DefaultEmailPIC As String = "") As String
            Dim emailID As String
            Dim sID As String
            Dim sUser As String
            Dim objUserInfo As UserInfo
            Try
                Select Case userType
                    Case 1  'user rilis
                        sID = objDomain.ReleaseBy.Substring(0, 6)
                        sUser = objDomain.ReleaseBy.Substring(6)
                    Case 2  'user create
                        sID = objDomain.CreatedBy.Substring(0, 6)
                        sUser = objDomain.CreatedBy.Substring(6)
                    Case 3  'user selesai
                        sID = objDomain.FinishBy.Substring(0, 6)
                        sUser = objDomain.FinishBy.Substring(6)
                End Select

                Dim crits As New CriteriaComposite(New Criteria(GetType(UserInfo), "Dealer.ID", MatchType.Exact, sID))
                crits.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, sUser))
                'objUserInfo = New UserInfoFacade(m_userPrincipal).Retrieve(crits)(0)
                Dim objUIFac As UserInfoFacade = New UserInfoFacade(m_userPrincipal)
                Dim arlUI As New ArrayList
                arlUI = objUIFac.Retrieve(crits)
                If arlUI.Count > 0 Then
                    objUserInfo = CType(arlUI(0), UserInfo)
                    emailID = objUserInfo.Email
                Else
                    emailID = DefaultEmailPIC
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Null Data")
                If rethrow Then
                    Throw
                End If
            End Try

            Return emailID
        End Function
        Private Function GetSecondCC(ByVal sID As String)
            Dim nSecondCC As String
            Dim crits As New CriteriaComposite(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(BusinessArea), "Dealer.ID", MatchType.Exact, sID.Trim))
            crits.opAnd(New Criteria(GetType(BusinessArea), "Kind", MatchType.Exact, 1))
            Dim arlBArea As ArrayList = New BusinessAreaFacade(m_userPrincipal).Retrieve(crits)
            Dim objBArea As BusinessArea
            If arlBArea.Count > 0 Then
                objBArea = arlBArea(0)
            Else
                nSecondCC = ""
            End If
            If objBArea.Email = "" Then
                nSecondCC = ""
            Else
                nSecondCC = objBArea.Email
            End If

            Return nSecondCC
        End Function

        'add method rudi
        Public Function RetrieveDataset(ByVal spString As String) As DataSet
            Dim ds As New DataSet

            ds = m_PQRHeaderMapper.RetrieveDataSet(spString)

            Return ds
        End Function

#End Region

    End Class

End Namespace




