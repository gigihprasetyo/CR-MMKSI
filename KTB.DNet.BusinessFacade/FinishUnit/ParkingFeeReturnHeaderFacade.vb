#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2008

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
'// Copyright  2008
'// ---------------------
'// $History      : $
'// Generated on 12/04/2008 - 16:03:00 
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

Imports KTB.Dnet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.Dnet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit

    Public Class ParkingFeeReturnHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ParkingFeeReturnHeaderMapper As IMapper
        Private m_ParkingFeeMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ParkingFeeReturnHeaderMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.ParkingFeeReturnHeader).ToString)
            Me.m_ParkingFeeMapper = MapperFactory.GetInstance().GetMapper(GetType(ParkingFee).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.ParkingFeeReturnHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.ParkingFeeReturnDetail))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PenaltyParkirHistory))
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveScalar(ByVal criterias As ICriteria, ByVal agg As Aggregate) As Object
            Return m_ParkingFeeReturnHeaderMapper.RetrieveScalar(agg, criterias)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As ParkingFeeReturnHeader
            Return CType(m_ParkingFeeReturnHeaderMapper.Retrieve(ID), ParkingFeeReturnHeader)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ParkingFeeReturnHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ParkingFeeReturnHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ParkingFeeReturnHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(ParkingFeeReturnHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_ParkingFeeReturnHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(ParkingFeeReturnHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_ParkingFeeReturnHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositAPencairan As ArrayList = m_ParkingFeeReturnHeaderMapper.RetrieveByCriteria(criterias)
            Return _DepositAPencairan
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAPencairanColl As ArrayList = m_ParkingFeeReturnHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositAPencairanColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ParkingFeeReturnHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositAPencairanColl As ArrayList = m_ParkingFeeReturnHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositAPencairanColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ParkingFeeReturnHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ParkingFeeReturnHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositAPencairanColl As ArrayList = m_ParkingFeeReturnHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositAPencairanColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositAPencairanColl As ArrayList = m_ParkingFeeReturnHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ParkingFeeReturnHeader), columnName, matchOperator, columnValue))
            Return DepositAPencairanColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(ParkingFeeReturnHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ParkingFeeReturnHeader), columnName, matchOperator, columnValue))

            Return m_ParkingFeeReturnHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Update(ByVal objDomain As ParkingFeeReturnHeader) As Integer
            'Dim returnValue As Integer = -1
            'If (Me.IsTaskFree()) Then
            '    Try
            '        Me.SetTaskLocking()
            '        Dim performTransaction As Boolean = True
            '        Dim ObjMapper As IMapper

            '        m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

            '        If performTransaction Then
            '            m_TransactionManager.PerformTransaction()
            '            returnValue = objDomain.ID
            '        End If
            '    Catch ex As Exception
            '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
            '        If rethrow Then
            '            Throw
            '        End If
            '    Finally
            '        Me.RemoveTaskLocking()
            '    End Try
            'End If
            'Return returnValue

            Dim nResult As Integer = -1
            Try
                nResult = m_ParkingFeeReturnHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As ParkingFeeReturnHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ParkingFeeReturnHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function GenerateNoRegBuktiPotongPPh(ByVal arlPF As ArrayList, ByVal txtNoBuktiPotong As String)
            Dim retVal As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim totalAmount As Double = 0
                    For Each item As ParkingFee In arlPF
                        totalAmount = totalAmount + CDbl(item.Amount)
                    Next

                    Dim pf As ParkingFee = CType(arlPF(0), ParkingFee)

                    'Dim dealer As Dealer = CType(arlPF(0), ParkingFee).Dealer --> 20120605 by anh

                    Dim dealer As dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(Me.m_userPrincipal).Retrieve(pf.Dealer.CreditAccount)

                    Dim objPFRH As ParkingFeeReturnHeader
                    objPFRH = New ParkingFeeReturnHeader
                    objPFRH.Dealer = pf.Dealer 'dealer
                    objPFRH.NoReg = ""
                    objPFRH.BuktiPotongNumber = txtNoBuktiPotong
                    objPFRH.ReturnDate = Date.Now
                    objPFRH.TotalAmount = totalAmount
                    If totalAmount > 0 Then
                        objPFRH.PPHAmount = totalAmount / 10
                    End If
                    objPFRH.Status = EnumPengembalianPPhStatus.PengembalianPPhStatus.Baru


                    m_TransactionManager.AddInsert(objPFRH, m_userPrincipal.Identity.Name)

                    Dim objPFRD As ParkingFeeReturnDetail
                    For Each item As ParkingFee In arlPF
                        Dim oPFFacade As ParkingFeeFacade = New ParkingFeeFacade(m_userPrincipal)
                        Dim objPenaltyStatusFac As PenaltyParkirHistoryFacade = New PenaltyParkirHistoryFacade(m_userPrincipal)

                        item.Status = EnumParkingFeeStatus.ParkingFeeStatus.Proses
                        oPFFacade.Update(item)

                        'Insert status changed ti PenaltyparkirHistory
                        Dim objStatusHistOld As PenaltyParkirHistory
                        Dim crtHist As CriteriaComposite
                        crtHist = New CriteriaComposite(New Criteria(GetType(PenaltyParkirHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crtHist.opAnd(New Criteria(GetType(PenaltyParkirHistory), "ParkingFee.ID", MatchType.Exact, item.ID))

                        Dim sortColl As SortCollection = New SortCollection
                        sortColl.Add(New Sort(GetType(PenaltyParkirHistory), "CreatedTime", Sort.SortDirection.DESC))

                        Dim arlHist As ArrayList = objPenaltyStatusFac.Retrieve(crtHist, sortColl)
                        If arlHist.Count > 0 Then
                            objStatusHistOld = CType(arlHist(0), PenaltyParkirHistory)
                        Else
                            objStatusHistOld = New PenaltyParkirHistory
                        End If

                        Dim objStatusHist As New PenaltyParkirHistory
                        objStatusHist.ParkingFee = item
                        If objStatusHistOld.ID > 0 Then
                            objStatusHist.OldStatus = objStatusHistOld.NewStatus
                        End If
                        objStatusHist.NewStatus = EnumParkingFeeStatus.ParkingFeeStatus.Proses
                        m_TransactionManager.AddInsert(objStatusHist, m_userPrincipal.Identity.Name)
                        'end 

                        objPFRD = New ParkingFeeReturnDetail
                        objPFRD.ParkingFeeReturnHeader = objPFRH
                        objPFRD.ParkingFee = item
                        m_TransactionManager.AddInsert(objPFRD, m_userPrincipal.Identity.Name)

                    Next

                    m_TransactionManager.PerformTransaction()
                    retVal = 1
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "domain policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return retVal
        End Function

        Public Function UpdateParkingFeeReturHeaderStatusSelesai(ByVal pphColl As ArrayList, ByVal iStatus As Integer, ByVal oldStatus As Integer)
            Dim returnvalue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try

                    Me.SetTaskLocking()
                    Dim objHistoryFacade As ParkingFeeHistoryFacade
                    Dim objHistory As ParkingFeeHistory
                    For Each objPFRH As ParkingFeeReturnHeader In pphColl
                        'add by anh 20160120 
                        objHistoryFacade = New ParkingFeeHistoryFacade(m_userPrincipal)

                        Dim crtParkingFeeHist As CriteriaComposite
                        crtParkingFeeHist = New CriteriaComposite(New Criteria(GetType(ParkingFeeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crtParkingFeeHist.opAnd(New Criteria(GetType(ParkingFeeHistory), "ParkingFeeReturnHeader.ID", MatchType.Exact, objPFRH.ID))
                        crtParkingFeeHist.opAnd(New Criteria(GetType(ParkingFeeHistory), "NewStatus", MatchType.Exact, iStatus))

                        Dim sortParkingFeeColl As SortCollection = New SortCollection
                        sortParkingFeeColl.Add(New Sort(GetType(ParkingFeeHistory), "CreatedTime", Sort.SortDirection.DESC))

                        Dim arlParkingFeeHist As ArrayList = objHistoryFacade.Retrieve(crtParkingFeeHist, sortParkingFeeColl)
                        If arlParkingFeeHist.Count > 0 Then
                            objHistory = CType(arlParkingFeeHist(0), ParkingFeeHistory)
                            m_TransactionManager.AddUpdate(objHistory, m_userPrincipal.Identity.Name)
                        Else
                            objHistory = New ParkingFeeHistory
                            objHistory.ParkingFeeReturnHeader = objPFRH
                            objHistory.OldStatus = oldStatus
                            objHistory.NewStatus = iStatus
                            m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                        End If
                        'end 'add by anh 20160120 


                        'objHistory = New ParkingFeeHistory
                        'objHistory.ParkingFeeReturnHeader = objPFRH
                        'objHistory.OldStatus = objPFRH.Status
                        'objHistory.NewStatus = iStatus
                        'If arlParkingFeeHist.Count > 0 Then
                        '    m_TransactionManager.AddUpdate(objHistory, m_userPrincipal.Identity.Name)
                        'Else
                        '    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                        'End If

                        objPFRH.Status = iStatus
                        m_TransactionManager.AddUpdate(objPFRH, m_userPrincipal.Identity.Name)

                        Dim objPenaltyStatusFac As PenaltyParkirHistoryFacade = New PenaltyParkirHistoryFacade(m_userPrincipal)

                        For Each item As ParkingFeeReturnDetail In objPFRH.ParkingFeeReturnDetails
                            If iStatus = EnumPengembalianPPhStatus.PengembalianPPhStatus.Batal Then
                                Dim oPF As ParkingFee = New ParkingFee
                                oPF = item.ParkingFee
                                oPF.Status = EnumParkingFeeStatus.ParkingFeeStatus.Baru
                                m_TransactionManager.AddUpdate(oPF, m_userPrincipal.Identity.Name)


                                'Insert status changed ti PenaltyparkirHistory
                                Dim objStatusHistOld As PenaltyParkirHistory
                                Dim crtHist As CriteriaComposite
                                crtHist = New CriteriaComposite(New Criteria(GetType(PenaltyParkirHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crtHist.opAnd(New Criteria(GetType(PenaltyParkirHistory), "ParkingFee.ID", MatchType.Exact, item.ParkingFee.ID))

                                Dim sortColl As SortCollection = New SortCollection
                                sortColl.Add(New Sort(GetType(PenaltyParkirHistory), "CreatedTime", Sort.SortDirection.DESC))

                                Dim arlHist As ArrayList = objPenaltyStatusFac.Retrieve(crtHist, sortColl)
                                If arlHist.Count > 0 Then
                                    objStatusHistOld = CType(arlHist(0), PenaltyParkirHistory)
                                Else
                                    objStatusHistOld = New PenaltyParkirHistory
                                End If

                                Dim objStatusHist As New PenaltyParkirHistory
                                objStatusHist.ParkingFee = item.ParkingFee
                                If objStatusHistOld.ID > 0 Then
                                    objStatusHist.OldStatus = objStatusHistOld.NewStatus
                                End If
                                objStatusHist.NewStatus = EnumParkingFeeStatus.ParkingFeeStatus.Proses
                                m_TransactionManager.AddInsert(objStatusHist, m_userPrincipal.Identity.Name)
                                'end 
                            End If
                        Next
                    Next
                    m_TransactionManager.PerformTransaction()
                    returnvalue = 1
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "domain policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnvalue
        End Function

        Public Function UpdateParkingFeeReturHeaderStatus(ByVal pphColl As ArrayList, ByVal iStatus As Integer)
            Dim returnvalue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try

                    Me.SetTaskLocking()
                    Dim objHistoryFacade As ParkingFeeHistoryFacade
                    Dim objHistory As ParkingFeeHistory
                    For Each objPFRH As ParkingFeeReturnHeader In pphColl
                        'add by anh 20160120 
                        objHistoryFacade = New ParkingFeeHistoryFacade(m_userPrincipal)

                        Dim crtParkingFeeHist As CriteriaComposite
                        crtParkingFeeHist = New CriteriaComposite(New Criteria(GetType(ParkingFeeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crtParkingFeeHist.opAnd(New Criteria(GetType(ParkingFeeHistory), "ParkingFeeReturnHeader.ID", MatchType.Exact, objPFRH.ID))
                        crtParkingFeeHist.opAnd(New Criteria(GetType(ParkingFeeHistory), "NewStatus", MatchType.Exact, iStatus))

                        Dim sortParkingFeeColl As SortCollection = New SortCollection
                        sortParkingFeeColl.Add(New Sort(GetType(ParkingFeeHistory), "CreatedTime", Sort.SortDirection.DESC))

                        Dim arlParkingFeeHist As ArrayList = objHistoryFacade.Retrieve(crtParkingFeeHist, sortParkingFeeColl)
                        If arlParkingFeeHist.Count > 0 Then
                            objHistory = CType(arlParkingFeeHist(0), ParkingFeeHistory)
                        Else
                            objHistory = New ParkingFeeHistory
                        End If
                        'end 'add by anh 20160120 


                        'objHistory = New ParkingFeeHistory
                        objHistory.ParkingFeeReturnHeader = objPFRH
                        objHistory.OldStatus = objPFRH.Status
                        objHistory.NewStatus = iStatus
                        m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)

                        objPFRH.Status = iStatus
                        m_TransactionManager.AddUpdate(objPFRH, m_userPrincipal.Identity.Name)

                        Dim objPenaltyStatusFac As PenaltyParkirHistoryFacade = New PenaltyParkirHistoryFacade(m_userPrincipal)

                        For Each item As ParkingFeeReturnDetail In objPFRH.ParkingFeeReturnDetails
                            If iStatus = EnumPengembalianPPhStatus.PengembalianPPhStatus.Batal Then
                                Dim oPF As ParkingFee = New ParkingFee
                                oPF = item.ParkingFee
                                oPF.Status = EnumParkingFeeStatus.ParkingFeeStatus.Baru
                                m_TransactionManager.AddUpdate(oPF, m_userPrincipal.Identity.Name)


                                'Insert status changed ti PenaltyparkirHistory
                                Dim objStatusHistOld As PenaltyParkirHistory
                                Dim crtHist As CriteriaComposite
                                crtHist = New CriteriaComposite(New Criteria(GetType(PenaltyParkirHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crtHist.opAnd(New Criteria(GetType(PenaltyParkirHistory), "ParkingFee.ID", MatchType.Exact, item.ParkingFee.ID))

                                Dim sortColl As SortCollection = New SortCollection
                                sortColl.Add(New Sort(GetType(PenaltyParkirHistory), "CreatedTime", Sort.SortDirection.DESC))

                                Dim arlHist As ArrayList = objPenaltyStatusFac.Retrieve(crtHist, sortColl)
                                If arlHist.Count > 0 Then
                                    objStatusHistOld = CType(arlHist(0), PenaltyParkirHistory)
                                Else
                                    objStatusHistOld = New PenaltyParkirHistory
                                End If

                                Dim objStatusHist As New PenaltyParkirHistory
                                objStatusHist.ParkingFee = item.ParkingFee
                                If objStatusHistOld.ID > 0 Then
                                    objStatusHist.OldStatus = objStatusHistOld.NewStatus
                                End If
                                objStatusHist.NewStatus = EnumParkingFeeStatus.ParkingFeeStatus.Proses
                                m_TransactionManager.AddInsert(objStatusHist, m_userPrincipal.Identity.Name)
                                'end 
                            End If
                        Next
                    Next
                    m_TransactionManager.PerformTransaction()
                    returnvalue = 1
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "domain policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnvalue
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.ParkingFeeReturnHeader) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.ParkingFeeReturnHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.ParkingFeeReturnHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DepositAPencairanD) Then

                CType(InsertArg.DomainObject, DepositAPencairanD).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"
        
#End Region

    End Class

End Namespace
