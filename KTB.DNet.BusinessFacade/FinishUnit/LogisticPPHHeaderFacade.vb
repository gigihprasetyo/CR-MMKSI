
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
'// Copyright  2017
'// ---------------------
'// $History      : $
'// Generated on 9/14/2017 - 9:38:23 AM
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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade.FinishUnit

    Public Class LogisticPPHHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_LogisticPPHHeaderMapper As IMapper
        Private m_LogisticFeeMapper As IMapper
        Private m_TransactionManager As TransactionManager


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_LogisticPPHHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(LogisticPPHHeader).ToString)
            Me.m_LogisticFeeMapper = MapperFactory.GetInstance().GetMapper(GetType(LogisticFee).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.ParkingFeeReturnHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.ParkingFeeReturnDetail))
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.PenaltyParkirHistory))

        End Sub


#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As LogisticPPHHeader
            Return CType(m_LogisticPPHHeaderMapper.Retrieve(ID), LogisticPPHHeader)
        End Function


        Public Function Retrieve(ByVal RegNo As String) As LogisticPPHHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPPHHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(LogisticPPHHeader), "NoReg", MatchType.Exact, RegNo))

            Dim aPODs As ArrayList = m_LogisticPPHHeaderMapper.RetrieveByCriteria(criterias)
            If (aPODs.Count > 0) Then
                Return CType(aPODs(0), LogisticPPHHeader)
            End If
            Return New LogisticPPHHeader
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_LogisticPPHHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_LogisticPPHHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_LogisticPPHHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LogisticPPHHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LogisticPPHHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LogisticPPHHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LogisticPPHHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPPHHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _LogisticPPHHeader As ArrayList = m_LogisticPPHHeaderMapper.RetrieveByCriteria(criterias)
            Return _LogisticPPHHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPPHHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LogisticPPHHeaderColl As ArrayList = m_LogisticPPHHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LogisticPPHHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LogisticPPHHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DepositAPencairanColl As ArrayList = m_LogisticPPHHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DepositAPencairanColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim LogisticPPHHeaderColl As ArrayList = m_LogisticPPHHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return LogisticPPHHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPPHHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LogisticPPHHeaderColl As ArrayList = m_LogisticPPHHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(LogisticPPHHeader), columnName, matchOperator, columnValue))
            Return LogisticPPHHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LogisticPPHHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPPHHeader), columnName, matchOperator, columnValue))

            Return m_LogisticPPHHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As LogisticPPHHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_LogisticPPHHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As LogisticPPHHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_LogisticPPHHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As LogisticPPHHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_LogisticPPHHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As LogisticPPHHeader)
            Try
                m_LogisticPPHHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function GenerateNoRegBuktiPotongPPh(ByVal arlPF As ArrayList, ByVal txtNoBuktiPotong As String, ByVal pph As Decimal)
            Dim retVal As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim totalAmount As Double = 0
                    For Each item As LogisticFee In arlPF
                        totalAmount = totalAmount + CDbl(item.Amount)
                    Next

                    Dim pf As LogisticFee = CType(arlPF(0), LogisticFee)

                    Dim dealer As Dealer = New KTB.DNET.BusinessFacade.General.DealerFacade(Me.m_userPrincipal).Retrieve(pf.Dealer.CreditAccount)

                    Dim objPFRH As LogisticPPHHeader
                    objPFRH = New LogisticPPHHeader
                    objPFRH.Dealer = pf.Dealer 'dealer
                    objPFRH.NoReg = ""
                    objPFRH.BuktiPotongNumber = txtNoBuktiPotong
                    objPFRH.ReturnDate = Date.Now
                    objPFRH.TotalAmount = totalAmount
                    objPFRH.Description = "Pengembalian PPh Logistic Bulan " & MonthName(pf.LogisticDN.BillingDate.Month) & " " & pf.LogisticDN.BillingDate.Year
                    If totalAmount > 0 Then
                        'objPFRH.PPHAmount = totalAmount * 0.02
                        objPFRH.PPHAmount = totalAmount * pph / 100
                    End If
                    objPFRH.Status = EnumPengembalianLogisticPPHStatus.PengembalianPPhStatus.Baru

                    m_TransactionManager.AddInsert(objPFRH, m_userPrincipal.Identity.Name)
                    Dim objPFRD As LogisticPPHDetail
                    For Each item As LogisticFee In arlPF
                        Dim oPFFacade As LogisticFeeFacade = New LogisticFeeFacade(m_userPrincipal)
                        Dim objPenaltyStatusFac As LogisticFeeHistoryFacade = New LogisticFeeHistoryFacade(m_userPrincipal)

                        item.Status = EnumLogisticFeeStatus.LogisticFeeStatus.Proses
                        oPFFacade.Update(item)

                        'Insert status changed ti PenaltyparkirHistory
                        Dim objStatusHistOld As LogisticFeeHistory
                        Dim crtHist As CriteriaComposite
                        crtHist = New CriteriaComposite(New Criteria(GetType(LogisticFeeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crtHist.opAnd(New Criteria(GetType(LogisticFeeHistory), "LogisticFee.ID", MatchType.Exact, item.ID))

                        Dim sortColl As SortCollection = New SortCollection
                        sortColl.Add(New Sort(GetType(LogisticFeeHistory), "CreatedTime", Sort.SortDirection.DESC))

                        Dim arlHist As ArrayList = objPenaltyStatusFac.Retrieve(crtHist, sortColl)
                        If arlHist.Count > 0 Then
                            objStatusHistOld = CType(arlHist(0), LogisticFeeHistory)
                        Else
                            objStatusHistOld = New LogisticFeeHistory
                        End If

                        Dim objStatusHist As New LogisticFeeHistory
                        objStatusHist.LogisticFee = item
                        If objStatusHistOld.ID > 0 Then
                            objStatusHist.OldStatus = objStatusHistOld.NewStatus
                        End If
                        objStatusHist.NewStatus = EnumLogisticFeeStatus.LogisticFeeStatus.Proses
                        m_TransactionManager.AddInsert(objStatusHist, m_userPrincipal.Identity.Name)
                        'end 

                        objPFRD = New LogisticPPHDetail
                        objPFRD.LogisticPPHHeader = objPFRH
                        objPFRD.LogisticFee = item
                        m_TransactionManager.AddInsert(objPFRD, m_userPrincipal.Identity.Name)

                    Next

                    m_TransactionManager.PerformTransaction()
                    retVal = 1
                Catch ex As Exception
                    retVal = -1
                    'Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "domain policy")
                    'If rethrow Then
                    '    Throw
                    'End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return retVal
        End Function

        Public Function UpdateLogisticPPHHeaderStatusSelesai(ByVal pphColl As ArrayList, ByVal iStatus As Integer, ByVal oldStatus As Integer)
            Dim returnvalue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try

                    Me.SetTaskLocking()
                    Dim objHistoryFacade As LogisticFeeReturnHistoryFacade
                    Dim objHistory As LogisticFeeReturnHistory
                    For Each objPFRH As LogisticPPHHeader In pphColl
                        'add by anh 20160120 
                        objHistoryFacade = New LogisticFeeReturnHistoryFacade(m_userPrincipal)

                        Dim crtLogisticFeeHist As CriteriaComposite
                        crtLogisticFeeHist = New CriteriaComposite(New Criteria(GetType(LogisticFeeReturnHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crtLogisticFeeHist.opAnd(New Criteria(GetType(LogisticFeeReturnHistory), "LogisticPPHHeader.ID", MatchType.Exact, objPFRH.ID))
                        crtLogisticFeeHist.opAnd(New Criteria(GetType(LogisticFeeReturnHistory), "NewStatus", MatchType.Exact, iStatus))

                        Dim sortLogisticFeeColl As SortCollection = New SortCollection
                        sortLogisticFeeColl.Add(New Sort(GetType(LogisticFeeReturnHistory), "CreatedTime", Sort.SortDirection.DESC))

                        Dim arlLogisticFeeHist As ArrayList = objHistoryFacade.Retrieve(crtLogisticFeeHist, sortLogisticFeeColl)
                        If arlLogisticFeeHist.Count > 0 Then
                            objHistory = CType(arlLogisticFeeHist(0), LogisticFeeReturnHistory)
                            m_TransactionManager.AddUpdate(objHistory, m_userPrincipal.Identity.Name)
                        Else
                            objHistory = New LogisticFeeReturnHistory
                            objHistory.LogisticPPHHeader = objPFRH
                            objHistory.OldStatus = oldStatus
                            objHistory.NewStatus = iStatus
                            m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                        End If
                        'end 'add by anh 20160120 

                        objPFRH.Status = iStatus
                        m_TransactionManager.AddUpdate(objPFRH, m_userPrincipal.Identity.Name)

                        Dim objPenaltyStatusFac As LogisticFeeHistoryFacade = New LogisticFeeHistoryFacade(m_userPrincipal)

                        For Each item As LogisticPPHDetail In objPFRH.LogisticPPHDetails
                            If iStatus = EnumPengembalianLogisticPPHStatus.PengembalianPPhStatus.Batal Then
                                Dim oPF As LogisticFee = New LogisticFee
                                oPF = item.LogisticFee
                                oPF.Status = EnumLogisticFeeStatus.LogisticFeeStatus.Baru
                                m_TransactionManager.AddUpdate(oPF, m_userPrincipal.Identity.Name)


                                'Insert status changed ti PenaltyparkirHistory
                                Dim objStatusHistOld As LogisticFeeHistory
                                Dim crtHist As CriteriaComposite
                                crtHist = New CriteriaComposite(New Criteria(GetType(LogisticFeeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crtHist.opAnd(New Criteria(GetType(LogisticFeeHistory), "LogisticFee.ID", MatchType.Exact, item.LogisticFee.ID))

                                Dim sortColl As SortCollection = New SortCollection
                                sortColl.Add(New Sort(GetType(LogisticFeeHistory), "CreatedTime", Sort.SortDirection.DESC))

                                Dim arlHist As ArrayList = objPenaltyStatusFac.Retrieve(crtHist, sortColl)
                                If arlHist.Count > 0 Then
                                    objStatusHistOld = CType(arlHist(0), LogisticFeeHistory)
                                Else
                                    objStatusHistOld = New LogisticFeeHistory
                                End If

                                Dim objStatusHist As New LogisticFeeHistory
                                objStatusHist.LogisticFee = item.LogisticFee
                                If objStatusHistOld.ID > 0 Then
                                    objStatusHist.OldStatus = objStatusHistOld.NewStatus
                                End If
                                objStatusHist.NewStatus = EnumLogisticFeeStatus.LogisticFeeStatus.Proses
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

        Public Function UpdateLogisticPPHHeaderStatus(ByVal pphColl As ArrayList, ByVal iStatus As Integer)
            Dim returnvalue As Integer = -1
                Try

                    Me.SetTaskLocking()
                    Dim objHistoryFacade As LogisticFeeReturnHistoryFacade
                    Dim objHistory As LogisticFeeReturnHistory
                    For Each objPFRH As LogisticPPHHeader In pphColl
                        objHistoryFacade = New LogisticFeeReturnHistoryFacade(m_userPrincipal)

                        Dim crtLogisticFeeHist As CriteriaComposite
                        crtLogisticFeeHist = New CriteriaComposite(New Criteria(GetType(LogisticFeeReturnHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crtLogisticFeeHist.opAnd(New Criteria(GetType(LogisticFeeReturnHistory), "LogisticPPHHeader.ID", MatchType.Exact, objPFRH.ID))
                        crtLogisticFeeHist.opAnd(New Criteria(GetType(LogisticFeeReturnHistory), "NewStatus", MatchType.Exact, iStatus))

                        Dim sortLogisticFeeColl As SortCollection = New SortCollection
                        sortLogisticFeeColl.Add(New Sort(GetType(LogisticFeeReturnHistory), "CreatedTime", Sort.SortDirection.DESC))

                        Dim arlLogisticFeeHist As ArrayList = objHistoryFacade.Retrieve(crtLogisticFeeHist, sortLogisticFeeColl)
                        If arlLogisticFeeHist.Count > 0 Then
                            objHistory = CType(arlLogisticFeeHist(0), LogisticFeeReturnHistory)
                        Else
                            objHistory = New LogisticFeeReturnHistory
                        End If

                        'objHistory = New LogisticFeeReturnHistory
                        objHistory.LogisticPPHHeader = objPFRH
                        objHistory.OldStatus = objPFRH.Status
                        objHistory.NewStatus = iStatus
                        m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)

                        objPFRH.Status = iStatus
                        m_TransactionManager.AddUpdate(objPFRH, m_userPrincipal.Identity.Name)

                        Dim objPenaltyStatusFac As LogisticFeeHistoryFacade = New LogisticFeeHistoryFacade(m_userPrincipal)

                        For Each item As LogisticPPHDetail In objPFRH.LogisticPPHDetails
                            If iStatus = EnumPengembalianLogisticPPHStatus.PengembalianPPhStatus.Batal Then
                                Dim oPF As LogisticFee = New LogisticFee
                                oPF = item.LogisticFee
                                oPF.Status = EnumLogisticFeeStatus.LogisticFeeStatus.Baru
                                m_TransactionManager.AddUpdate(oPF, m_userPrincipal.Identity.Name)


                                'Insert status changed ti PenaltyparkirHistory
                                Dim objStatusHistOld As LogisticFeeHistory
                                Dim crtHist As CriteriaComposite
                                crtHist = New CriteriaComposite(New Criteria(GetType(LogisticFeeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crtHist.opAnd(New Criteria(GetType(LogisticFeeHistory), "LogisticFee.ID", MatchType.Exact, item.LogisticFee.ID))

                                Dim sortColl As SortCollection = New SortCollection
                                sortColl.Add(New Sort(GetType(LogisticFeeHistory), "CreatedTime", Sort.SortDirection.DESC))

                                Dim arlHist As ArrayList = objPenaltyStatusFac.Retrieve(crtHist, sortColl)
                                If arlHist.Count > 0 Then
                                    objStatusHistOld = CType(arlHist(0), LogisticFeeHistory)
                                Else
                                    objStatusHistOld = New LogisticFeeHistory
                                End If

                                Dim objStatusHist As New LogisticFeeHistory
                                objStatusHist.LogisticFee = item.LogisticFee
                                If objStatusHistOld.ID > 0 Then
                                    objStatusHist.OldStatus = objStatusHistOld.NewStatus
                                End If
                                objStatusHist.NewStatus = EnumLogisticFeeStatus.LogisticFeeStatus.Proses
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

            Return returnvalue
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.LogisticPPHHeader) Then
                CType(InsertArg.DomainObject, KTB.DNET.Domain.LogisticPPHHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.LogisticPPHHeader).MarkLoaded()
            End If
        End Sub
      
#End Region

    End Class

End Namespace