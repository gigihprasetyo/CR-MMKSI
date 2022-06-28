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
'// Author Name   : Ariwibawa
'// PURPOSE       : Facade for Page Event - Parameter Event
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2009 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2009 - 11:26:00 AM
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

Namespace KTB.DNet.BusinessFacade.Event
    Public Class EventProposalFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_EventProposalMapper As IMapper
        Private m_EventProposalDetailMapper As IMapper
        Private m_V_EventProposalAgreementMapper As IMapper
        Private m_EventProposalHistoryMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_EventProposalMapper = MapperFactory.GetInstance().GetMapper(GetType(EventProposal).ToString)
            Me.m_EventProposalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(EventProposalDetail).ToString)
            Me.m_V_EventProposalAgreementMapper = MapperFactory.GetInstance().GetMapper(GetType(V_EventProposalAgreement).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.m_EventProposalHistoryMapper = MapperFactory.GetInstance().GetMapper(GetType(EventProposalHistoryAgreement).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As EventProposal
            Return CType(m_EventProposalMapper.Retrieve(ID), EventProposal)
        End Function

        Public Function RetrieveBy_Dealer_ActivityType_EventParameter(ByVal intEventProposalID As Integer, ByVal intDealerID As Integer, ByVal intActivityID As Integer, ByVal intEventParameterID As Integer) As ArrayList
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposal), "RowStatus", MatchType.Exact, CInt(DBRowStatus.Active)))
            crits.opAnd(New Criteria(GetType(EventProposal), "ID", MatchType.No, intEventProposalID))
            crits.opAnd(New Criteria(GetType(EventProposal), "Dealer", MatchType.Exact, intDealerID))
            crits.opAnd(New Criteria(GetType(EventProposal), "ActivityType", MatchType.Exact, intActivityID))
            crits.opAnd(New Criteria(GetType(EventProposal), "EventParameter", MatchType.Exact, intEventParameterID))
            Dim al As ArrayList = Retrieve(crits)
            Return al
        End Function

        Public Function IsProposalExists(ByVal intEventProposalID As Integer, ByVal intDealerID As Integer, ByVal intActivityID As Integer, ByVal intEventParameterID As Integer) As Boolean
            Dim al As ArrayList = RetrieveBy_Dealer_ActivityType_EventParameter(intEventProposalID, intDealerID, intActivityID, intEventParameterID)
            If (IsNothing(al) Or al.Count <= 0) Then
                Return False
            End If
            Return True
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EventProposalMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EventProposalMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EventProposalMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventProposalMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventProposalMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EventProposal As ArrayList = m_EventProposalMapper.RetrieveByCriteria(criterias)
            Return _EventProposal
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EventProposalColl As ArrayList = m_EventProposalMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventProposalColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim EventProposalColl As ArrayList = m_EventProposalMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return EventProposalColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_EventProposalMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EventProposalMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EventProposalColl As ArrayList = m_EventProposalMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventProposalColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventProposal), columnName, matchOperator, columnValue))
            Dim EventProposalColl As ArrayList = m_EventProposalMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EventProposalColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventProposal), columnName, matchOperator, columnValue))

            Return m_EventProposalMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function InsertOrUpdate(ByVal objEventProposal As EventProposal, _
            ByVal objEventProposalDetail As ArrayList, ByVal objEventProposalDetailsDelete As ArrayList, _
            ByVal objHistory As EventProposalHistory) As Integer
            Dim iReturn As Integer = -1
            If MyBase.IsTaskFree Then
                Try
                    MyBase.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    If objEventProposal.ID > 0 Then
                        m_TransactionManager.AddUpdate(objEventProposal, m_userPrincipal.Identity.Name)
                    Else
                        objEventProposal.ID = m_EventProposalMapper.Insert(objEventProposal, m_userPrincipal.Identity.Name)
                    End If
                    objEventProposal.MarkLoaded()
                    For Each objDomain As V_EventProposalDetail In objEventProposalDetail
                        If objDomain.ID <> -1 Then
                            If objDomain.ID > 0 Then
                                m_TransactionManager.AddUpdate(DomainMapping(objDomain), m_userPrincipal.Identity.Name)
                            Else
                                objdomain.EventProposalID = objEventProposal.ID
                                objdomain.EventProposal = objEventProposal
                                m_TransactionManager.AddInsert(DomainMapping(objDomain), m_userPrincipal.Identity.Name)
                            End If
                        End If
                    Next
                    For Each objDomain As V_EventProposalDetail In objEventProposalDetailsDelete
                        m_TransactionManager.AddDelete(DomainMapping(objDomain))
                    Next
                    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        iReturn = objEventProposal.EventProposalDetails.Count + 1 + objEventProposalDetailsDelete.Count
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    MyBase.RemoveTaskLocking()
                End Try
            End If
            Return iReturn
        End Function

        Public Function Insert(ByVal objDomain As EventProposal) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_EventProposalMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As EventProposal, ByVal objHistory As EventProposalHistoryAgreement) As Int32
            Dim iReturn As Integer = -1
            If MyBase.IsTaskFree Then
                Try
                    MyBase.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        iReturn = 2
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    MyBase.RemoveTaskLocking()
                End Try
            End If
            Return iReturn
        End Function

        Public Function Update(ByVal objCollection As ArrayList) As Integer
            Dim iReturn As Integer = -1
            If MyBase.IsTaskFree Then
                Try
                    MyBase.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    For Each objDomain As EventProposal In objCollection
                        m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        iReturn = objCollection.Count
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    MyBase.RemoveTaskLocking()
                End Try
            End If
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As EventProposal) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_EventProposalMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Function Delete(ByVal objDomain As EventProposal) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_EventProposalMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As EventProposal) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_EventProposalMapper.Delete(objDomain)
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
        Private Function GenerateDateCriteria(ByVal nDate As Date, ByVal startDate As Boolean) As DateTime
            Dim Hour As Integer
            Dim Minute As Integer
            Dim Second As Integer

            If startDate Then
                Hour = 0
                Minute = 0
                Second = 0
            Else
                Hour = 23
                Minute = 59
                Second = 59
            End If

            Return New DateTime(nDate.Year, nDate.Month, nDate.Day, Hour, Minute, Second)
        End Function
        Private Function DomainMapping(ByVal viewProps As V_EventProposalDetail) As EventProposalDetail
            Dim evProp As New EventProposalDetail(viewProps.ID)
            evProp.CreatedBy = viewProps.CreatedBy
            evProp.CreatedTime = viewProps.CreatedTime
            evProp.Description = viewProps.Description
            evProp.ErrorMessage = viewProps.ErrorMessage
            evProp.EventActivityType = viewProps.EventActivityType
            evProp.EventProposal = viewProps.EventProposal
            evProp.Item = viewProps.Item
            evProp.LastUpdateBy = viewProps.LastUpdateBy
            evProp.LastUpdateTime = viewProps.LastUpdateTime
            evProp.Quantity = viewProps.Quantity
            evProp.RowStatus = viewProps.RowStatus
            evProp.UnitCost = viewProps.UnitCost
            evProp.VechileType = viewProps.VechileType
            Return evProp
        End Function
        Public Function RetrieveDetail(ByVal eventProposalID As Int32, ByVal groupCode As EnumEventActivityType.EventActivityTypeGroupCode, _
            ByVal SortColumn As String, ByVal SortDirec As Sort.SortDirection) As ArrayList
            Dim crits As New CriteriaComposite(New Criteria(GetType(EventProposalDetail), "RowStatus", _
                CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(EventProposalDetail), "EventProposal", eventProposalID))
            crits.opAnd(New Criteria(GetType(EventProposalDetail), "EventActivityType.EventActivityTypeGroupCode", _
                groupCode.ToString))
            Dim SortCol As New SortCollection
            SortCol.Add(New Sort(GetType(EventProposalDetail), SortColumn, SortDirec))
            Return m_EventProposalDetailMapper.RetrieveByCriteria(crits, SortCol)
        End Function
        Public Function RetrieveAgreement(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, _
            ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim SortCol As New SortCollection
            SortCol.Add(New Sort(GetType(V_EventProposalAgreement), sortColumn, sortDirection))
            Return m_V_EventProposalAgreementMapper.RetrieveByCriteria(Criterias, SortCol, _
                pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveAgreement(ByVal id As Int32) As V_EventProposalAgreement
            Return m_V_EventProposalAgreementMapper.Retrieve(id)
        End Function
        Public Function RetrieveAgreement(ByVal Criterias As CriteriaComposite, ByVal sortColl As SortCollection) As ArrayList
            Return m_V_EventProposalAgreementMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function
#End Region

    End Class
End Namespace