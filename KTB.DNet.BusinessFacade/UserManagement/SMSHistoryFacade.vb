 
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
Imports System.Text

#End Region

Namespace KTB.DNet.BusinessFacade.UserManagement
    Public Class SMSHistoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_SMSHistoryMapper As IMapper
        Private m_SMSHistoryPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager
        Private m_InstanceName As String
#End Region

#Region "Constructor"

        Public Sub New(ByVal SMSHistoryPrincipal As IPrincipal)
            Me.m_SMSHistoryPrincipal = SMSHistoryPrincipal
            Me.m_SMSHistoryMapper = MapperFactory.GetInstance().GetMapper(GetType(SMSHistory).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

        Public Sub New(ByVal SMSHistoryPrincipal As IPrincipal, ByVal instanceName As String)
            Me.m_InstanceName = instanceName
            Me.m_SMSHistoryPrincipal = SMSHistoryPrincipal
            Me.m_SMSHistoryMapper = MapperFactory.GetInstance().GetMapper(GetType(SMSHistory).ToString, instanceName)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SMSHistory
            Return CType(m_SMSHistoryMapper.Retrieve(ID), SMSHistory)
        End Function

        Public Function Retrieve(ByVal Code As String) As SMSHistory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SMSHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SMSHistory), "UserName", MatchType.Exact, Code))

            Dim SMSHistoryColl As ArrayList = m_SMSHistoryMapper.RetrieveByCriteria(criterias)
            If (SMSHistoryColl.Count > 0) Then
                Return CType(SMSHistoryColl(0), SMSHistory)
            End If
            Return New SMSHistory
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SMSHistoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SMSHistoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SMSHistoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SMSHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SMSHistoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SMSHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SMSHistoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SMSHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SMSHistory As ArrayList = m_SMSHistoryMapper.RetrieveByCriteria(criterias)
            Return _SMSHistory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SMSHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SMSHistoryColl As ArrayList = m_SMSHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SMSHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SMSHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SMSHistoryColl As ArrayList = m_SMSHistoryMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SMSHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SMSHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SMSHistoryColl As ArrayList = m_SMSHistoryMapper.RetrieveByCriteria(Criterias, sortColl)
            Return SMSHistoryColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SMSHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SMSHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SMSHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SMSHistoryColl As ArrayList = m_SMSHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SMSHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SMSHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SMSHistory), columnName, matchOperator, columnValue))
            Dim SMSHistoryColl As ArrayList = m_SMSHistoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SMSHistoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SMSHistory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SMSHistory), columnName, matchOperator, columnValue))

            Return m_SMSHistoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As SMSHistory) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SMSHistoryMapper.Insert(objDomain, "test")
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SMSHistory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SMSHistoryMapper.Update(objDomain, m_SMSHistoryPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SMSHistory)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SMSHistoryMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SMSHistory) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SMSHistoryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function ValidateCode(ByVal dealerID As Integer, ByVal userName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SMSHistory), "UserName", MatchType.Exact, userName))
            crit.opAnd(New Criteria(GetType(SMSHistory), "Dealer.ID", MatchType.Exact, dealerID))

            Dim agg As Aggregate = New Aggregate(GetType(SMSHistory), "UserName", AggregateType.Count)

            Return CType(m_SMSHistoryMapper.RetrieveScalar(agg, crit), Integer)
        End Function
        Public Function UpdateUserRole(ByVal userRoleList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If userRoleList.Count > 0 Then
                        For Each objUserRole As UserRole In userRoleList
                            m_TransactionManager.AddInsert(objUserRole, m_SMSHistoryPrincipal.Identity.Name)
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
#End Region

#Region "Message Format/Other"
        Public Function GetContentMessage(ByVal type As Integer, Optional ByVal oLogin As Object = Nothing) As String
            Dim sb As New StringBuilder
            Dim _strEnter As String = Chr(13) & Chr(10)
            Dim _strDelimiter As String = ":"
            Dim _strHeader As String = "KTB-DNET"
            Dim _strBingo As String = ""

            Select Case type
                Case enumSMS.ContentMessage.ActivationCodeNotification 'UserProfile
                    Dim oUserProfile As UserProfile
                    sb.Append(_strHeader)
                    sb.Append(_strEnter)
                    sb.Append("Kode Aktifasi")
                    sb.Append(_strDelimiter)
                    sb.Append(oUserProfile.ActivationCode)
                    sb.Append(_strEnter)
                    sb.Append("Lakukan Aktifasi Paling Lambat")
                    sb.Append(_strDelimiter)
                    sb.Append("DD/MM/YYYY HH:MM:SS")
                Case enumSMS.ContentMessage.ResetPasswordSuccess 'UserInfo
                    Dim oUserInfo As UserInfo
                    sb.Append(_strHeader)
                    sb.Append(_strEnter)
                    sb.Append("Reset Password Sukses")
                    sb.Append(_strEnter)
                    sb.Append("Kode Organisasi")
                    sb.Append(_strDelimiter)
                    sb.Append("")
                    sb.Append(_strEnter)
                    sb.Append("Nama User")
                    sb.Append(_strDelimiter)
                    sb.Append(oUserInfo.UserName)
                    sb.Append(_strEnter)
                    sb.Append("Kata Kunci")
                    sb.Append(_strDelimiter)
                    sb.Append(oUserInfo.Password)
                Case enumSMS.ContentMessage.BingoCardNotification
                    Dim oBingo As KTB.DNet.Domain.Bingo
                    sb.Append(_strHeader)
                    sb.Append(_strEnter)
                    sb.Append("Nomor Serial")
                    sb.Append(_strDelimiter)
                    sb.Append(oBingo.SerialNumber)
                    sb.Append(_strEnter)
                    sb.Append("Valid Sampai")
                    sb.Append(_strDelimiter)
                    sb.Append("MM/DD/YY")
                    sb.Append(_strEnter)
                    sb.Append("Bingo Card")
                    sb.Append(_strDelimiter)
                    sb.Append(_strEnter)
                    sb.Append(_strBingo)
                Case enumSMS.ContentMessage.ResetBingoSuccess
                    Dim oBingo As KTB.DNet.Domain.Bingo
                    sb.Append(_strHeader)
                    sb.Append(_strEnter)
                    sb.Append("Reset Bingo Card Berhasil")
                    sb.Append(_strEnter)
                    sb.Append("Nomor Serial")
                    sb.Append(_strDelimiter)
                    sb.Append(obingo.SerialNumber)
                    sb.Append(_strEnter)
                    sb.Append("Valid Sampai")
                    sb.Append(_strDelimiter)
                    sb.Append("MM/DD/YY")
                    sb.Append(_strEnter)
                    sb.Append("Bingo Card")
                    sb.Append(_strDelimiter)
                    sb.Append(_strEnter)
                    sb.Append(_strBingo)
                Case enumSMS.ContentMessage.ActivationFail
                    sb.Append(_strHeader)
                    sb.Append(_strEnter)
                    sb.Append("Aktifasi Gagal")
                Case enumSMS.ContentMessage.ResetPasswordFail
                    sb.Append(_strHeader)
                    sb.Append(_strEnter)
                    sb.Append("Reset Password Gagal")
                Case enumSMS.ContentMessage.ResetBingoFail
                    sb.Append(_strHeader)
                    sb.Append(_strEnter)
                    sb.Append("Reset Bingo Card Gagal")
            End Select

            Return sb.ToString
        End Function

#End Region

#Region "Custom Method"

        Public Function Retrieve(ByVal _UserName As String, ByVal _DealerCode As String) As SMSHistory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SMSHistory), "UserName", MatchType.Exact, _UserName))
            criterias.opAnd(New Criteria(GetType(SMSHistory), "Dealer.DealerCode", MatchType.Exact, _DealerCode))

            Dim SMSHistoryColl As ArrayList = m_SMSHistoryMapper.RetrieveByCriteria(criterias)
            If (SMSHistoryColl.Count > 0) Then
                Return CType(SMSHistoryColl(0), SMSHistory)
            End If
            Return New SMSHistory
        End Function

        '------------------------------------ Sms Scheduler Function --------------------------------------

        Public Function RetriveUserProfilebyUserId(ByVal Userid As Integer) As UserProfile
            Dim objFacade As UserProfileFacade
            If Not IsNothing(Me.m_InstanceName) AndAlso Me.m_InstanceName <> String.Empty Then
                objFacade = New UserProfileFacade(Nothing, Me.m_InstanceName)
            Else
                objFacade = New UserProfileFacade(Nothing)
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserProfile), "UserID", MatchType.Exact, Userid))
            Dim ArlUserProfile As ArrayList = objFacade.Retrieve(criterias)
            If ArlUserProfile.Count > 0 Then
                Return CType(ArlUserProfile(0), UserProfile)
            Else
                Return Nothing
            End If
        End Function

        Public Function RetriveUserProfileByTempActivation(ByVal tempActivationCode As String) As UserProfile
            Dim objFacade As UserProfileFacade
            If Not IsNothing(Me.m_InstanceName) AndAlso Me.m_InstanceName <> String.Empty Then
                objFacade = New UserProfileFacade(Nothing, Me.m_InstanceName)
            Else
                objFacade = New UserProfileFacade(Nothing)
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserProfile), "TempActivationCode", MatchType.Exact, tempActivationCode))
            Dim ArlUserProfile As ArrayList = objFacade.Retrieve(criterias)
            If ArlUserProfile.Count > 0 Then
                Return CType(ArlUserProfile(0), UserProfile)
            Else
                Return Nothing
            End If
        End Function

        Public Function RetriveUserProfilebyTempActivationCode(ByVal tempActivationCode As String) As ArrayList
            Dim objFacade As UserProfileFacade
            If Not IsNothing(Me.m_InstanceName) AndAlso Me.m_InstanceName <> String.Empty Then
                objFacade = New UserProfileFacade(Nothing, Me.m_InstanceName)
            Else
                objFacade = New UserProfileFacade(Nothing)
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserProfile), "TempActivationCode", MatchType.Exact, tempActivationCode))
            Dim ArlUserProfile As ArrayList = objFacade.Retrieve(criterias)
            If ArlUserProfile.Count > 0 Then
                Return ArlUserProfile
            Else
                Return Nothing
            End If
        End Function

        Public Function RetriveUserProfilebyTransitionCode(ByVal TransitionCode As String) As UserProfile
            Dim objFacade As UserProfileFacade
            If Not IsNothing(Me.m_InstanceName) AndAlso Me.m_InstanceName <> String.Empty Then
                objFacade = New UserProfileFacade(Nothing, Me.m_InstanceName)
            Else
                objFacade = New UserProfileFacade(Nothing)
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserProfile), "TransitionActivationCode", MatchType.Exact, TransitionCode))
            Dim ArlUserProfile As ArrayList = objFacade.Retrieve(criterias)
            If ArlUserProfile.Count > 0 Then
                Return ArlUserProfile(0)
            Else
                Return Nothing
            End If
        End Function

        Public Function RetriveUserProfilebyActivationCode(ByVal ActivationCode As String) As UserProfile
            Dim objFacade As UserProfileFacade
            If Not IsNothing(Me.m_InstanceName) AndAlso Me.m_InstanceName <> String.Empty Then
                objFacade = New UserProfileFacade(Nothing, Me.m_InstanceName)
            Else
                objFacade = New UserProfileFacade(Nothing)
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserProfile), "ActivationCode", MatchType.Exact, ActivationCode))
            Dim ArlUserProfile As ArrayList = objFacade.Retrieve(criterias)
            If ArlUserProfile.Count > 0 Then
                Return CType(ArlUserProfile(0), UserProfile)
            Else
                Return Nothing
            End If
        End Function

        Public Function RetriveUserInfo(ByVal kode_org As String, ByVal username As String, ByVal activationcode As String) As UserInfo
            Dim objFacade As UserInfoFacade
            If Not IsNothing(Me.m_InstanceName) AndAlso Me.m_InstanceName <> String.Empty Then
                objFacade = New UserInfoFacade(Nothing, Me.m_InstanceName)
            Else
                objFacade = New UserInfoFacade(Nothing)
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "Dealer.DealerCode", MatchType.Exact, kode_org))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "UserName", MatchType.Exact, username.ToLower))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "UserProfile.ActivationCode", MatchType.Exact, activationcode))
            Dim ArlUserInfo As ArrayList = objFacade.Retrieve(criterias)
            If ArlUserInfo.Count > 0 Then
                Return CType(ArlUserInfo(0), UserInfo)
            Else
                Return Nothing
            End If
        End Function



#End Region

    End Class

End Namespace
