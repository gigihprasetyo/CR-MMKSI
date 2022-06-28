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

#End Region

Namespace KTB.DNet.BusinessFacade.UserManagement
    Public Class UserInfoFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_UserInfoMapper As IMapper
        Private m_UserInfoPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal UserInfoPrincipal As IPrincipal)
            Me.m_UserInfoPrincipal = UserInfoPrincipal
            Me.m_UserInfoMapper = MapperFactory.GetInstance().GetMapper(GetType(UserInfo).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.UserInfo))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.UserProfile))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.LockUser))
        End Sub

        Public Sub New(ByVal UserInfoPrincipal As IPrincipal, ByVal instanceName As String)
            Me.m_UserInfoPrincipal = UserInfoPrincipal
            Me.m_UserInfoMapper = MapperFactory.GetInstance().GetMapper(GetType(UserInfo).ToString, instanceName)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.UserInfo))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.UserProfile))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.LockUser))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As UserInfo
            Return CType(m_UserInfoMapper.Retrieve(ID), UserInfo)
        End Function

        Public Function Retrieve(ByVal Code As String) As UserInfo
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, Code))

            Dim UserInfoColl As ArrayList = m_UserInfoMapper.RetrieveByCriteria(criterias)
            If (UserInfoColl.Count > 0) Then
                Return CType(UserInfoColl(0), UserInfo)
            End If
            Return New UserInfo
        End Function

        Public Function RetrievebyUserNameAndDealerCode(ByVal UserName As String, ByVal DealerCode As String) As UserInfo
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, UserName))
            criterias.opAnd(New Criteria(GetType(UserInfo), "Dealer.DealerCode", MatchType.Exact, DealerCode))

            Dim UserInfoColl As ArrayList = m_UserInfoMapper.RetrieveByCriteria(criterias)
            If (UserInfoColl.Count > 0) Then
                Return CType(UserInfoColl(0), UserInfo)
            End If
            Return New UserInfo
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_UserInfoMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_UserInfoMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_UserInfoMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserInfoMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserInfoMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _UserInfo As ArrayList = m_UserInfoMapper.RetrieveByCriteria(criterias)
            Return _UserInfo
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim UserInfoColl As ArrayList = m_UserInfoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return UserInfoColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim UserInfoColl As ArrayList = m_UserInfoMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return UserInfoColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim UserInfoColl As ArrayList = m_UserInfoMapper.RetrieveByCriteria(Criterias, sortColl)
            Return UserInfoColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_UserInfoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(UserInfo), SortColumn, sortDirection))

            Dim ClaimGoodConditionColl As ArrayList = m_UserInfoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return ClaimGoodConditionColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(UserInfo), SortColumn, sortDirection))

            Dim ClaimGoodConditionColl As ArrayList = m_UserInfoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return ClaimGoodConditionColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim UserInfoColl As ArrayList = m_UserInfoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return UserInfoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserInfo), columnName, matchOperator, columnValue))
            Dim UserInfoColl As ArrayList = m_UserInfoMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return UserInfoColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(UserInfo), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), columnName, matchOperator, columnValue))

            Return m_UserInfoMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As UserInfo) As Integer
            Dim iReturn As Integer = -2
            Try
                m_UserInfoMapper.Insert(objDomain, m_UserInfoPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As UserInfo) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_UserInfoMapper.Update(objDomain, m_UserInfoPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Update(ByVal objDomain As UserInfo, ByVal UserName As String) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_UserInfoMapper.Update(objDomain, "System")
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As UserInfo)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_UserInfoMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As UserInfo) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_UserInfoMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function ValidateCode(ByVal dealerID As Integer, ByVal userName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, userName))
            crit.opAnd(New Criteria(GetType(UserInfo), "Dealer.ID", MatchType.Exact, dealerID))

            Dim agg As Aggregate = New Aggregate(GetType(UserInfo), "UserName", AggregateType.Count)

            Return CType(m_UserInfoMapper.RetrieveScalar(agg, crit), Integer)
        End Function
        Public Function UpdateUserRole(ByVal userRoleList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If userRoleList.Count > 0 Then
                        For Each objUserRole As UserRole In userRoleList
                            m_TransactionManager.AddInsert(objUserRole, m_UserInfoPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
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


        Public Function GetExistingUser(ByVal objUser As UserInfo) As UserInfo
            If Not objUser Is Nothing Then
                Dim objUserInfo As UserInfo = Retrieve(objUser.ID)
                Return objUserInfo
            Else
                Return Nothing
            End If
        End Function


        Public Function RegisterUser(ByVal objUser As UserInfo, ByVal objUserProfile As UserProfile) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim oldUserInfo As UserInfo = GetExistingUser(objUser)
                    If (Not oldUserInfo.UserProfile Is Nothing) Then
                        If oldUserInfo.UserProfile.ID > 0 Then
                            Dim oldUserProfile As UserProfile = oldUserInfo.UserProfile
                            oldUserProfile.ImageID = objUserProfile.ImageID
                            oldUserProfile.ImageDescription = objUserProfile.ImageDescription
                            oldUserProfile.MotherName = objUserProfile.MotherName
                            oldUserProfile.BirthDate = objUserProfile.BirthDate
                            oldUserProfile.ActivationStatus = objUserProfile.ActivationStatus
                            oldUserProfile.RegistrationStatus = objUserProfile.RegistrationStatus
                            oldUserProfile.Question1 = objUserProfile.Question1
                            oldUserProfile.Question2 = objUserProfile.Question2
                            oldUserProfile.Question3 = objUserProfile.Question3
                            oldUserProfile.Question4 = objUserProfile.Question4
                            oldUserProfile.Question5 = objUserProfile.Question5
                            oldUserProfile.ActivationSentTime = Now
                            oldUserProfile.Answer1 = objUserProfile.Answer1
                            oldUserProfile.Answer2 = objUserProfile.Answer2
                            oldUserProfile.Answer3 = objUserProfile.Answer3
                            oldUserProfile.Answer4 = objUserProfile.Answer4
                            oldUserProfile.Answer5 = objUserProfile.Answer5
                            oldUserProfile.TempActivationCode = objUserProfile.TempActivationCode
                            oldUserProfile.ActivationCode = objUserProfile.ActivationCode
                            If objUserProfile.TransitionHP <> String.Empty Then
                                oldUserProfile.TransitionHP = objUserProfile.TransitionHP
                                oldUserProfile.TransitionProcessDate = objUserProfile.TransitionProcessDate
                                oldUserProfile.TransitionActivationCode = objUserProfile.TransitionActivationCode
                            End If
                            oldUserProfile.Bingo = objUserProfile.Bingo
                            If Not oldUserProfile.Bingo Is Nothing Then
                                If oldUserProfile.Bingo.ID > 0 Then
                                    oldUserProfile.TempActivationCode = ""
                                    oldUserProfile.ActivationCode = objUserProfile.ActivationCode
                                End If
                            End If
                            m_TransactionManager.AddUpdate(oldUserProfile, m_UserInfoPrincipal.Identity.Name)
                        Else
                            objUserProfile.UserID = objUser.ID
                            m_TransactionManager.AddInsert(objUserProfile, m_UserInfoPrincipal.Identity.Name)
                        End If
                    Else
                        objUserProfile.UserID = objUser.ID
                        objUserProfile.UserInfo = objUser
                        m_TransactionManager.AddInsert(objUserProfile, m_UserInfoPrincipal.Identity.Name)
                    End If
                    m_TransactionManager.AddUpdate(objUser, m_UserInfoPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
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


        Public Function RegisterSecurityUser(ByVal objUser As UserInfo, ByVal objUserProfile As UserProfile) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim oldUserInfo As UserInfo = GetExistingUser(objUser)
                    If (Not oldUserInfo.UserProfile Is Nothing) Then
                        If oldUserInfo.UserProfile.ID > 0 Then
                            Dim oldUserProfile As UserProfile = oldUserInfo.UserProfile
                            oldUserProfile.ImageID = objUserProfile.ImageID
                            oldUserProfile.ImageDescription = objUserProfile.ImageDescription
                            oldUserProfile.MotherName = objUserProfile.MotherName
                            oldUserProfile.BirthDate = objUserProfile.BirthDate
                            oldUserProfile.ActivationStatus = objUserProfile.ActivationStatus
                            oldUserProfile.RegistrationStatus = objUserProfile.RegistrationStatus
                            oldUserProfile.Question1 = objUserProfile.Question1
                            oldUserProfile.Question2 = objUserProfile.Question2
                            oldUserProfile.Question3 = objUserProfile.Question3
                            oldUserProfile.Question4 = objUserProfile.Question4
                            oldUserProfile.Question5 = objUserProfile.Question5
                            oldUserProfile.ActivationSentTime = Now
                            oldUserProfile.Answer1 = objUserProfile.Answer1
                            oldUserProfile.Answer2 = objUserProfile.Answer2
                            oldUserProfile.Answer3 = objUserProfile.Answer3
                            oldUserProfile.Answer4 = objUserProfile.Answer4
                            oldUserProfile.Answer5 = objUserProfile.Answer5
                            oldUserProfile.TempActivationCode = objUserProfile.TempActivationCode
                            oldUserProfile.ActivationCode = objUserProfile.ActivationCode
                            oldUserProfile.TransitionHP = objUserProfile.TransitionHP
                            oldUserProfile.TransitionProcessDate = objUserProfile.TransitionProcessDate
                            oldUserProfile.TransitionActivationCode = objUserProfile.TransitionActivationCode
                            oldUserProfile.Bingo = objUserProfile.Bingo
                            If Not oldUserProfile.Bingo Is Nothing Then
                                If oldUserProfile.Bingo.ID > 0 Then
                                    oldUserProfile.TempActivationCode = ""
                                    oldUserProfile.ActivationCode = objUserProfile.ActivationCode
                                End If
                            End If
                            m_TransactionManager.AddUpdate(oldUserProfile, m_UserInfoPrincipal.Identity.Name)
                        Else
                            objUserProfile.UserID = objUser.ID
                            m_TransactionManager.AddInsert(objUserProfile, m_UserInfoPrincipal.Identity.Name)
                        End If
                    Else
                        objUserProfile.UserID = objUser.ID
                        objUserProfile.UserInfo = objUser
                        m_TransactionManager.AddInsert(objUserProfile, m_UserInfoPrincipal.Identity.Name)
                    End If
                    m_TransactionManager.AddUpdate(objUser, m_UserInfoPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
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


        Public Function RegisterUserWithoutStatus(ByVal objUser As UserInfo, ByVal objUserProfile As UserProfile) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim oldUserInfo As UserInfo = GetExistingUser(objUser)
                    If (Not oldUserInfo.UserProfile Is Nothing) Then
                        If oldUserInfo.UserProfile.ID > 0 Then
                            Dim oldUserProfile As UserProfile = oldUserInfo.UserProfile
                            oldUserProfile.ImageID = objUserProfile.ImageID
                            oldUserProfile.ImageDescription = objUserProfile.ImageDescription
                            oldUserProfile.MotherName = objUserProfile.MotherName
                            oldUserProfile.BirthDate = objUserProfile.BirthDate
                            oldUserProfile.Question1 = objUserProfile.Question1
                            oldUserProfile.Question2 = objUserProfile.Question2
                            oldUserProfile.Question3 = objUserProfile.Question3
                            oldUserProfile.Question4 = objUserProfile.Question4
                            oldUserProfile.Question5 = objUserProfile.Question5
                            oldUserProfile.ActivationSentTime = Now
                            oldUserProfile.Answer1 = objUserProfile.Answer1
                            oldUserProfile.Answer2 = objUserProfile.Answer2
                            oldUserProfile.Answer3 = objUserProfile.Answer3
                            oldUserProfile.Answer4 = objUserProfile.Answer4
                            oldUserProfile.Answer5 = objUserProfile.Answer5
                            m_TransactionManager.AddUpdate(oldUserProfile, m_UserInfoPrincipal.Identity.Name)
                        Else
                            objUserProfile.UserID = objUser.ID
                            m_TransactionManager.AddInsert(objUserProfile, m_UserInfoPrincipal.Identity.Name)
                        End If
                    Else
                        objUserProfile.UserID = objUser.ID
                        objUserProfile.UserInfo = objUser
                        m_TransactionManager.AddInsert(objUserProfile, m_UserInfoPrincipal.Identity.Name)
                    End If
                    m_TransactionManager.AddUpdate(objUser, m_UserInfoPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
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

#End Region

#Region "Custom Method"

        Public Function Retrieve(ByVal _UserName As String, ByVal _DealerCode As String) As UserInfo
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, _UserName))
            criterias.opAnd(New Criteria(GetType(UserInfo), "Dealer.DealerCode", MatchType.Exact, _DealerCode))
            'criterias.opAnd(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim UserInfoColl As ArrayList = m_UserInfoMapper.RetrieveByCriteria(criterias)
            If (UserInfoColl.Count > 0) Then
                Return CType(UserInfoColl(0), UserInfo)
            End If
            Return New UserInfo
        End Function

#End Region

    End Class

End Namespace
