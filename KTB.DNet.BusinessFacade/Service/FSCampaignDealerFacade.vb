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

Namespace KTB.DNet.BusinessFacade.Service
    Public Class FSCampaignDealerFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_FSCampaignDealerMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_FSCampaignDealerMapper = MapperFactory.GetInstance.GetMapper(GetType(FSCampaignDealer).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FSCampaignDealer
            Return CType(m_FSCampaignDealerMapper.Retrieve(ID), FSCampaignDealer)
        End Function

        Public Function RetrieveList(ByVal fsCampaignID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FSCampaignDealer), "FSCampaign.ID", MatchType.Exact, fsCampaignID))
            Dim FSCampaignDealerColl As ArrayList = m_FSCampaignDealerMapper.RetrieveByCriteria(criterias)
            If (FSCampaignDealerColl.Count > 0) Then
                Return FSCampaignDealerColl
            Else
                Return New ArrayList
            End If
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FSCampaignDealerMapper.RetrieveByCriteria(criterias)
        End Function
        Public Function Retrieve(ByVal fsCampaign As FSCampaign, ByVal strDealerCode As String) As FSCampaignDealer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignDealer), "FSCampaign.ID", MatchType.Exact, fsCampaign.ID))
            criterias.opAnd(New Criteria(GetType(FSCampaignDealer), "DealerCode", MatchType.Exact, strDealerCode))
            Dim FSCampaignDealerColl As ArrayList = m_FSCampaignDealerMapper.RetrieveByCriteria(criterias)
            If (FSCampaignDealerColl.Count > 0) Then
                Return FSCampaignDealerColl(0)
            Else
                Return Nothing
            End If
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FSCampaignDealerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FSCampaignDealerMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignDealerMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignDealerMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FSCampaignDealer As ArrayList = m_FSCampaignDealerMapper.RetrieveByCriteria(criterias)
            Return _FSCampaignDealer
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FSCampaignDealerColl As ArrayList = m_FSCampaignDealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignDealerColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignDealerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FSCampaignDealerColl As ArrayList = m_FSCampaignDealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignDealerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FSCampaignDealer), columnName, matchOperator, columnValue))
            Dim FSCampaignDealerColl As ArrayList = m_FSCampaignDealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignDealerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignDealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignDealer), columnName, matchOperator, columnValue))

            Return m_FSCampaignDealerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As FSCampaignDealer) As Integer
            Dim iReturn As Integer = -2
            Try
                m_FSCampaignDealerMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function InsertTransaction(ByVal objStrDealer As ArrayList, ByVal fsCampaign As FSCampaign) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If objStrDealer.Count > 0 Then
                        For Each item As String In objStrDealer
                            Dim fSCampaignDealer As fSCampaignDealer = New fSCampaignDealer
                            fSCampaignDealer.FSCampaign = fsCampaign
                            fSCampaignDealer.DealerCode = item
                            fSCampaignDealer.RowStatus = 0
                            m_TransactionManager.AddInsert(fSCampaignDealer, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        objStrDealer = New ArrayList
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = -2
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

        Public Function UpdateTransaction(ByVal arlDealer As ArrayList, ByVal fsCampaign As FSCampaign) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arlDealer.Count > 0 Then
                        For Each strDealer As String In arlDealer
                            Dim fsCampaignDealer As FSCampaignDealer = New FSCampaignDealer
                            fsCampaignDealer = Me.Retrieve(fsCampaign, strDealer)
                            If fsCampaignDealer Is Nothing Then
                                fsCampaignDealer = New fsCampaignDealer
                                fsCampaignDealer.FSCampaign = fsCampaign
                                fsCampaignDealer.DealerCode = strDealer
                                fsCampaignDealer.RowStatus = 0
                                m_TransactionManager.AddInsert(fsCampaignDealer, m_userPrincipal.Identity.Name)
                            Else
                                fsCampaignDealer.RowStatus = 0
                                fsCampaignDealer.LastUpdateBy = m_userPrincipal.Identity.Name
                                fsCampaignDealer.LastUpdatedTime = Date.Now
                                m_TransactionManager.AddUpdate(fsCampaignDealer, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = -2
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

        Public Function DeleteTransaction(ByVal fsCampaign As FSCampaign) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim arlFSCampaignDealer As ArrayList = New ArrayList
                    arlFSCampaignDealer = Me.RetrieveList(fsCampaign.ID)

                    For Each objFSCampaignDealer As FSCampaignDealer In arlFSCampaignDealer

                        objFSCampaignDealer.RowStatus = 1
                        objFSCampaignDealer.LastUpdateBy = m_userPrincipal.Identity.Name
                        objFSCampaignDealer.LastUpdatedTime = Date.Now
                        m_TransactionManager.AddUpdate(objFSCampaignDealer, m_userPrincipal.Identity.Name)
                    Next
                    m_TransactionManager.PerformTransaction()
                    returnValue = -2
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

        Public Function DeleteTransaction(ByVal objFSCampaignDealer As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If objFSCampaignDealer.Count > 0 Then
                        For Each item As FSCampaignDealer In objFSCampaignDealer
                            item.RowStatus = 1
                            item.LastUpdateBy = m_userPrincipal.Identity.Name
                            item.LastUpdatedTime = Date.Now
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        objFSCampaignDealer = New ArrayList
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = -2
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

        Public Function Update(ByVal objDomain As FSCampaignDealer) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FSCampaignDealerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As FSCampaignDealer) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FSCampaignDealerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As FSCampaignDealer) As Integer
            Dim nResult As Integer = 1
            Try
                m_FSCampaignDealerMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is FSCampaignDealer) Then
                CType(InsertArg.DomainObject, FSCampaignDealer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, FSCampaignDealer).MarkLoaded()

            End If
        End Sub
#End Region

    End Class

End Namespace
