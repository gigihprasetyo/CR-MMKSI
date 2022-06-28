
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

Namespace KTB.DNet.BusinessFacade.BabitSalesComm

    Public Class BabitProposalFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitProposalMapper As IMapper
        Private m_PameranDisplayMapper As IMapper
        Private m_BabitProposalHistoryMapper As IMapper
        Private m_BPEvent As IMapper
        Private m_BPIklan As IMapper
        Private m_BPPameran As IMapper
        Private m_BabitPayment As IMapper
        Private m_EventActivity As IMapper
        Private m_PameranDisplay As IMapper
        Private m_DealerMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_BabitProposalMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.BabitProposal).ToString)
            Me.m_PameranDisplayMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.PameranDisplay).ToString)
            Me.m_BabitProposalHistoryMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.BabitProposalHistory).ToString)
            Me.m_BPIklan = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.BPIklan).ToString)
            Me.m_EventActivity = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.EventActivity).ToString)
            Me.m_BabitPayment = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.BabitPayment).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BabitProposal))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BabitProposalHistory))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BPEvent))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BPPameran))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BPIklan))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BabitPayment))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.EventActivity))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PameranDisplay))
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrievePameranDisplayByPameranID(ByVal PameranID As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PameranDisplay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PameranDisplay), "BPPameran.ID", MatchType.Exact, PameranID))

            Dim PameranDisplayColl As New ArrayList
            PameranDisplayColl = m_PameranDisplayMapper.RetrieveByCriteria(criterias)
            Return PameranDisplayColl
        End Function

        Public Function RetrieveBabitProposalHistory(ByVal BabitProposalID As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitProposalHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitProposalHistory), "BabitProposal.ID", MatchType.Exact, BabitProposalID))

            Dim BabitProposalHistoryColl As New ArrayList
            BabitProposalHistoryColl = m_BabitProposalHistoryMapper.RetrieveByCriteria(criterias)
            Return BabitProposalHistoryColl
        End Function

        Public Function RetrieveIklanByBabitProposalID(ByVal BabitProposalID As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BPIklan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BPIklan), "BabitProposal.ID", MatchType.Exact, BabitProposalID))

            Dim BPIklanColl As New ArrayList
            BPIklanColl = m_BPIklan.RetrieveByCriteria(criterias)
            Return BPIklanColl
        End Function
        Public Function RetrieveIklanByID(ByVal ID As String) As BPIklan
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BPIklan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BPIklan), "ID", MatchType.Exact, ID))

            Dim BPIklanColl As New ArrayList
            BPIklanColl = m_BPIklan.RetrieveByCriteria(criterias)
            If (BPIklanColl.Count > 0) Then
                Return CType(BPIklanColl(0), BPIklan)
            End If
            Return New BPIklan

        End Function
        Public Function RetrieveEventActivityByEventID(ByVal EventID As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventActivity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventActivity), "BPEvent.ID", MatchType.Exact, EventID))

            Dim EventActivityColl As New ArrayList
            EventActivityColl = m_EventActivity.RetrieveByCriteria(criterias)
            Return EventActivityColl
        End Function

        Public Function Retrieve(ByVal ID As Integer) As BabitProposal
            Return CType(m_BabitProposalMapper.Retrieve(ID), BabitProposal)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitProposal
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitProposal), "PKNumber", MatchType.Exact, Code))

            Dim BabitProposalColl As ArrayList = m_BabitProposalMapper.RetrieveByCriteria(criterias)
            If (BabitProposalColl.Count > 0) Then
                Return CType(BabitProposalColl(0), BabitProposal)
            End If
            Return New BabitProposal
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitProposalMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitProposalMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal SortColumn As String, ByVal sortDirection As String) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(SortColumn)) And (Not IsNothing(SortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(BabitProposal), SortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitProposalMapper.RetrieveByCriteria(criterias, sortColl)
        End Function


        Public Function RetrieveList() As ArrayList
            Return m_BabitProposalMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(BabitProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitProposalMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(BabitProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitProposalMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitProposal As ArrayList = m_BabitProposalMapper.RetrieveByCriteria(criterias)
            Return _BabitProposal
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitProposalColl As ArrayList = m_BabitProposalMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitProposalColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim BabitProposalColl As ArrayList = m_BabitProposalMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitProposalColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitProposalMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitProposalColl As ArrayList = m_BabitProposalMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitProposalColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(BabitProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim BabitProposalColl As ArrayList = m_BabitProposalMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitProposalColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitProposalColl As ArrayList = m_BabitProposalMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitProposal), columnName, matchOperator, columnValue))
            Return BabitProposalColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(BabitProposal), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitProposal), columnName, matchOperator, columnValue))

            Return m_BabitProposalMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitProposal), "BabitProposalCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitProposal), "BabitProposalCode", AggregateType.Count)
            Return CType(m_BabitProposalMapper.RetrieveScalar(agg, crit), Integer)
        End Function



        Public Sub Update(ByVal objDomain As BabitProposal)
            Try
                m_BabitProposalMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function UpdateBabitProposal(ByVal objDomain As BabitProposal) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitProposalMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.BabitProposal) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    Dim oBabitPropHis As New BabitProposalHistory
                    oBabitPropHis.BabitProposal = objDomain
                    m_TransactionManager.AddInsert(oBabitPropHis, m_userPrincipal.Identity.Name)

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

        Public Function UpdateBabitProposal(ByVal arl As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As BabitProposal In arl
                        Dim objBabitPrososalHistory As New BabitProposalHistory
                        objBabitPrososalHistory.BabitProposal = item
                        objBabitPrososalHistory.Status = item.Status
                        m_TransactionManager.AddInsert(objBabitPrososalHistory, m_userPrincipal.Identity.Name)

                        If item.Status = KTB.DNet.Domain.EnumBabit.StatusBabitProposal.Disetujui Then
                            Dim critPayment As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPayment), "BabitProposal.ID", MatchType.Exact, CInt(item.ID)))
                            critPayment.opAnd(New Criteria(GetType(BabitPayment), "RowStatus", MatchType.Exact, CInt(DBRowStatus.Active)))
                            Dim paymentList As ArrayList = Me.m_BabitPayment.RetrieveByCriteria(critPayment)
                            If (IsNothing(paymentList) Or paymentList.Count <= 0) Then
                                Dim NewBabitPayment As New BabitPayment
                                NewBabitPayment.BabitProposal = item
                                NewBabitPayment.Dealer = item.Dealer
                                NewBabitPayment.PaymentDate = DateTime.Now
                                m_TransactionManager.AddInsert(NewBabitPayment, m_userPrincipal.Identity.Name)
                            Else
                                Dim NewBabitPayment As BabitPayment = New BabitPayment(CType(item.BabitPayments(0), BabitPayment).ID)
                                NewBabitPayment.BabitProposal = item
                                NewBabitPayment.Dealer = item.Dealer
                                NewBabitPayment.PaymentDate = DateTime.Now
                                m_TransactionManager.AddUpdate(NewBabitPayment, m_userPrincipal.Identity.Name)
                            End If
                        End If

                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)

                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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

        Public Function InsertBabitPrososalPameran(ByVal objBabitProposal As KTB.DNet.Domain.BabitProposal, ByVal objBPPameran As KTB.DNet.Domain.BPPameran, ByVal arlPameranDisplay As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objBPPameran, m_userPrincipal.Identity.Name)

                    If (arlPameranDisplay.Count > 0) Then
                        For Each item As PameranDisplay In arlPameranDisplay
                            item.BPPameran = objBPPameran
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    objBabitProposal.BPPameran = objBPPameran
                    m_TransactionManager.AddInsert(objBabitProposal, m_userPrincipal.Identity.Name)

                    Dim objBabitPrososalHistory As New BabitProposalHistory
                    objBabitPrososalHistory.BabitProposal = objBabitProposal
                    objBabitPrososalHistory.Status = objBabitProposal.Status

                    m_TransactionManager.AddInsert(objBabitPrososalHistory, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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

        Public Function UpdateBabitPrososalPameran(ByVal objBabitProposal As KTB.DNet.Domain.BabitProposal, ByVal arlNewPameranDisplay As ArrayList, ByVal arlUpdatePameranDisplay As ArrayList, ByVal arlDeletePameranDisplay As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If (arlNewPameranDisplay.Count > 0) Then
                        For Each item As PameranDisplay In arlNewPameranDisplay
                            item.BPPameran = objBabitProposal.BPPameran
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If (arlUpdatePameranDisplay.Count > 0) Then
                        For Each item As PameranDisplay In arlUpdatePameranDisplay
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If (arlDeletePameranDisplay.Count > 0) Then
                        For Each item As PameranDisplay In arlDeletePameranDisplay
                            m_TransactionManager.AddDelete(item)
                        Next
                    End If

                    Dim objBabitPrososalHistory As New BabitProposalHistory
                    objBabitPrososalHistory.BabitProposal = objBabitProposal
                    objBabitPrososalHistory.Status = objBabitProposal.Status
                    m_TransactionManager.AddInsert(objBabitPrososalHistory, m_userPrincipal.Identity.Name)

                    m_TransactionManager.AddUpdate(objBabitProposal.BPPameran, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objBabitProposal, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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

        Public Function InsertBabitPrososalEvent(ByVal objBabitProposal As KTB.DNet.Domain.BabitProposal, ByVal objBPEvent As KTB.DNet.Domain.BPEvent, ByVal arlEventActivity As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objBPEvent, m_userPrincipal.Identity.Name)

                    If (arlEventActivity.Count > 0) Then
                        For Each item As EventActivity In arlEventActivity
                            item.BPEvent = objBPEvent
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    objBabitProposal.BPEvent = objBPEvent
                    m_TransactionManager.AddInsert(objBabitProposal, m_userPrincipal.Identity.Name)

                    Dim objBabitPrososalHistory As New BabitProposalHistory
                    objBabitPrososalHistory.BabitProposal = objBabitProposal
                    objBabitPrososalHistory.Status = objBabitProposal.Status

                    m_TransactionManager.AddInsert(objBabitPrososalHistory, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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

        Public Function UpdateBabitPrososalEvent(ByVal objBabitProposal As KTB.DNet.Domain.BabitProposal, ByVal arlNewEvent As ArrayList, ByVal arlUpdateEvent As ArrayList, ByVal arlDeleteEvent As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If (arlNewEvent.Count > 0) Then
                        For Each item As EventActivity In arlNewEvent
                            item.BPEvent = objBabitProposal.BPEvent
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If (arlUpdateEvent.Count > 0) Then
                        For Each item As EventActivity In arlUpdateEvent
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If (arlDeleteEvent.Count > 0) Then
                        For Each item As EventActivity In arlDeleteEvent
                            m_TransactionManager.AddDelete(item)
                        Next
                    End If

                    Dim objBabitPrososalHistory As New BabitProposalHistory
                    objBabitPrososalHistory.BabitProposal = objBabitProposal
                    objBabitPrososalHistory.Status = objBabitProposal.Status
                    m_TransactionManager.AddInsert(objBabitPrososalHistory, m_userPrincipal.Identity.Name)

                    m_TransactionManager.AddUpdate(objBabitProposal.BPEvent, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objBabitProposal, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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

        Public Function InsertBabitPrososalIklan(ByVal objBabitProposal As KTB.DNet.Domain.BabitProposal, ByVal arlIklan As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objBabitProposal, m_userPrincipal.Identity.Name)

                    If (arlIklan.Count > 0) Then
                        For Each item As BPIklan In arlIklan
                            item.BabitProposal = objBabitProposal
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    Dim objBabitPrososalHistory As New BabitProposalHistory
                    objBabitPrososalHistory.BabitProposal = objBabitProposal
                    objBabitPrososalHistory.Status = objBabitProposal.Status

                    m_TransactionManager.AddInsert(objBabitPrososalHistory, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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


        Public Function UpdateBabitPrososalIklan(ByVal objBabitProposal As KTB.DNet.Domain.BabitProposal, ByVal arlNewIklan As ArrayList, ByVal arlUpdateIklan As ArrayList, ByVal arlDeleteIklan As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If (arlNewIklan.Count > 0) Then
                        For Each item As BPIklan In arlNewIklan
                            item.BabitProposal = objBabitProposal
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If (arlUpdateIklan.Count > 0) Then
                        For Each item As BPIklan In arlUpdateIklan
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If (arlDeleteIklan.Count > 0) Then
                        For Each item As BPIklan In arlDeleteIklan
                            m_TransactionManager.AddDelete(item)
                        Next
                    End If

                    Dim objBabitPrososalHistory As New BabitProposalHistory
                    objBabitPrososalHistory.BabitProposal = objBabitProposal
                    objBabitPrososalHistory.Status = objBabitProposal.Status
                    m_TransactionManager.AddInsert(objBabitPrososalHistory, m_userPrincipal.Identity.Name)

                    m_TransactionManager.AddUpdate(objBabitProposal, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.BabitProposal) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.BabitProposal).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.BabitProposal).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is BabitProposalHistory) Then
                CType(InsertArg.DomainObject, BabitProposalHistory).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is BPPameran) Then
                CType(InsertArg.DomainObject, BPPameran).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.BPPameran).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is BPEvent) Then
                CType(InsertArg.DomainObject, BPEvent).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.BPEvent).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is BPIklan) Then
                CType(InsertArg.DomainObject, BPIklan).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.BPIklan).MarkLoaded()
            End If
        End Sub

        Public Function Delete(ByVal objDomain As BabitProposal) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                For Each item As BabitProposalHistory In objDomain.BabitProposalHistorys
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next
                nResult = UpdateTransaction(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.BabitProposal) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As BabitProposalHistory In objDomain.BabitProposalHistorys
                        item.BabitProposal = objDomain
                        If item.ID <> 0 Then
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        End If

                    Next

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

        Public Sub UpdateBpIklan(ByVal obj As BPIklan)
            Me.m_BPIklan.Update(obj, Me.m_userPrincipal.Identity.Name)
        End Sub

#End Region

#Region "Custom Method"

        Public Function IsCreatedByKTB(ByVal oBabitProposal As BabitProposal, ByVal objDealer As Dealer) As Boolean
            Dim ktbid As Integer = 0
            Try
                ktbid = CInt(oBabitProposal.CreatedBy.Substring(0, 6))
                If (ktbid = objDealer.ID) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
            Return False
        End Function

        Public Function FilterBabitAllocationByPeriodeList(ByVal arlSource As ArrayList, ByVal dtmFrom As DateTime, ByVal dtmTo As DateTime) As ArrayList
            Dim n As Integer = 0
            Dim arl As New ArrayList
            For Each obj As BabitAllocation In arlSource
                Dim dtmFrom2 As DateTime = New DateTime(obj.Babit.BabitYear, obj.Babit.StartPeriod, 1)
                Dim dtmTo2 As DateTime = New DateTime(obj.Babit.BabitYearEnd, obj.Babit.EndPeriod, 2)
                If (dtmFrom2 <= dtmFrom) And (dtmTo2 >= dtmTo) Then
                    arl.Add(obj)
                Else
                    n += 1
                End If
            Next
            If (n >= arlSource.Count) Then
                Return Nothing
            End If
            Return arl
        End Function

        Public Function FilterBabitProposalByPeriodeList(ByVal arlSource As ArrayList, ByVal dtmFrom As DateTime, ByVal dtmTo As DateTime) As ArrayList
            Dim n As Integer = 0
            Dim arl As New ArrayList
            For Each obj As BabitProposal In arlSource
                Dim dtmFrom2 As DateTime = New DateTime(obj.BabitAllocation.Babit.BabitYear, obj.BabitAllocation.Babit.StartPeriod, 1)
                Dim dtmTo2 As DateTime = New DateTime(obj.BabitAllocation.Babit.BabitYearEnd, obj.BabitAllocation.Babit.EndPeriod, 2)
                If (dtmFrom2 <= dtmFrom) And (dtmTo2 >= dtmTo) Then
                    arl.Add(obj)
                Else
                    n += 1
                End If
            Next
            If (n >= arlSource.Count) Then
                Return Nothing
            End If
            Return arl
        End Function


#End Region

    End Class

End Namespace

