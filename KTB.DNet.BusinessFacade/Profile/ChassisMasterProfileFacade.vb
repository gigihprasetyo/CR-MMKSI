

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
'// Generated on 7/18/2007 - 2:19:39 PM
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


#End Region

Namespace KTB.DNet.BusinessFacade.Profile

    Public Class ChassisMasterProfileFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ChassisMasterProfileMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ChassisMasterProfileMapper = MapperFactory.GetInstance.GetMapper(GetType(ChassisMasterProfile).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.ChassisMasterProfile))
        End Sub

#End Region

#Region "Retrieve"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.ChassisMasterProfile) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.ChassisMasterProfile).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.ChassisMasterProfile).MarkLoaded()
            End If
        End Sub

        Public Function Retrieve(ByVal ID As Integer) As ChassisMasterProfile
            Return CType(m_ChassisMasterProfileMapper.Retrieve(ID), ChassisMasterProfile)
        End Function

       

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ChassisMasterProfileMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ChassisMasterProfileMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ChassisMasterProfileMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMasterProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterProfileMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMasterProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterProfileMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ChassisMasterProfile As ArrayList = m_ChassisMasterProfileMapper.RetrieveByCriteria(criterias)
            Return _ChassisMasterProfile
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ChassisMasterProfileColl As ArrayList = m_ChassisMasterProfileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ChassisMasterProfileColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(ChassisMasterProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim ChassisMasterProfileColl As ArrayList = m_ChassisMasterProfileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ChassisMasterProfileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ChassisMasterProfileColl As ArrayList = m_ChassisMasterProfileMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), columnName, matchOperator, columnValue))
            Return ChassisMasterProfileColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMasterProfile), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), columnName, matchOperator, columnValue))

            Return m_ChassisMasterProfileMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Need To Add"

        

        Public Function Insert(ByVal objDomain As ChassisMasterProfile) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ChassisMasterProfileMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As ChassisMasterProfile) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ChassisMasterProfileMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function GetChassisMasterProfile(ByVal objChassisMasterProfile As ChassisMasterProfile, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As ChassisMasterProfile
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, objChassisMasterProfile.ChassisMaster.ID))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))

            Dim Coll As ArrayList = New ArrayList
            Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(ChassisMasterProfile).ToString)

            Coll = sProfileMapper.RetrieveByCriteria(criterias)
            If (Coll.Count > 0) Then
                Return CType(Coll(0), ChassisMasterProfile)
            End If
            Return New ChassisMasterProfile
        End Function

        'Private Function CompareExistingProfiles(ByVal objProfiles As ArrayList) As Boolean
        '    For Each item As ChassisMasterProfile In objProfiles
        '        Dim oldProfile As ChassisMasterProfile = GetChassisMasterProfile(item, item.ProfileGroup, item.ProfileHeader)
        '        If item.ProfileValue <> oldProfile.ProfileValue Then
        '            Return False
        '        End If
        '    Next
        '    Return True
        'End Function
        Public Function GetOldChassisMasterProfile(ByVal cm As ChassisMaster) As ArrayList
            Dim result As New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, cm.ID))
           

            Dim Coll As ArrayList = New ArrayList
            Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(ChassisMasterProfile).ToString)

            result = sProfileMapper.RetrieveByCriteria(criterias)


            Return result

        End Function

        Public Function Update(ByVal objDomains As ArrayList, ByVal objChassis As ChassisMaster) As Integer
            Dim returnValue As Integer = -1
            Dim isProfileChange As Boolean
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    If performTransaction Then

                        isProfileChange = False
                        Dim itemGP As New ProfileGroup

                        Dim res As New ArrayList
                        res = GetOldChassisMasterProfile(objChassis)
                        Dim isChangeGRoup As Boolean = False
                        If Not IsNothing(res) AndAlso res.Count > 0 Then

                            For Each cpg As ChassisMasterProfile In res

                                If cpg.ProfileGroup.ID <> CType(objDomains(0), ChassisMasterProfile).ProfileGroup.ID Then
                                    isChangeGRoup = True
                                    Exit For
                                End If
                            Next


                            If isChangeGRoup Then
                                For Each cmP As ChassisMasterProfile In res
                                    Dim sProfileMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(ChassisMasterProfile).ToString)
                                    cmP.RowStatus = CInt(DBRowStatus.Deleted)
                                    sProfileMapper.Update(cmP, m_userPrincipal.Identity.Name)
                                Next
                            End If
                        End If

                        For Each item As ChassisMasterProfile In objDomains
                            itemGP = item.ProfileGroup
                            Dim oldProfile As ChassisMasterProfile = GetChassisMasterProfile(item, item.ProfileGroup, item.ProfileHeader)
                            If oldProfile.ID > 0 Then
                                If oldProfile.ProfileValue <> item.ProfileValue Then
                                    isProfileChange = True
                                End If
                                oldProfile.ProfileValue = item.ProfileValue
                                For Each items As ChassisMasterProfileHistory In item.ChassisMasterProfileHistorys
                                    items.ChassisMasterProfile = oldProfile
                                    m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                                Next
                                m_TransactionManager.AddUpdate(oldProfile, m_userPrincipal.Identity.Name)
                            Else
                                isProfileChange = True
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                For Each itemss As ChassisMasterProfileHistory In item.ChassisMasterProfileHistorys
                                    itemss.ChassisMasterProfile = item
                                    m_TransactionManager.AddInsert(itemss, m_userPrincipal.Identity.Name)
                                Next
                            End If
                        Next

                        If isProfileChange Then
                            objChassis.LastUpdateProfile = Now
                        End If
                        m_TransactionManager.AddUpdate(objChassis, m_userPrincipal.Identity.Name)

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

        Public Function InsertWSM(ByVal objDomain As ChassisMasterProfile) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    If performTransaction Then
                        Dim IsChangedProfile As Boolean = False
                        Dim oldProfile As ChassisMasterProfile = GetChassisMasterProfile(objDomain, objDomain.ProfileGroup, objDomain.ProfileHeader)
                        If oldProfile.ID > 0 Then
                            If oldProfile.ProfileValue <> objDomain.ProfileValue Then
                                IsChangedProfile = True
                                oldProfile.ProfileValue = objDomain.ProfileValue
                                For Each item As ChassisMasterProfileHistory In objDomain.ChassisMasterProfileHistorys
                                    item.ChassisMasterProfile = oldProfile
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                Next
                                m_TransactionManager.AddUpdate(oldProfile, m_userPrincipal.Identity.Name)
                            End If
                        Else
                            m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                            For Each items As ChassisMasterProfileHistory In objDomain.ChassisMasterProfileHistorys
                                items.ChassisMasterProfile = objDomain
                                m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                        End If
                        Dim objChassis As ChassisMaster = objDomain.ChassisMaster
                        If IsChangedProfile Or objChassis.FakturStatus <> 4 Then
                            objChassis.LastUpdateProfile = Now
                            objChassis.FakturStatus = 4
                            'objChassis.AlreadySaled = 0 'remarks by anh 20160415 review by angga
                            objChassis.AlreadySaled = 2
                            m_TransactionManager.AddUpdate(objChassis, m_userPrincipal.Identity.Name)
                        End If
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


        Public Sub Delete(ByVal objDomain As ChassisMasterProfile)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ChassisMasterProfileMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As ChassisMasterProfile) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_ChassisMasterProfileMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function



#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace



