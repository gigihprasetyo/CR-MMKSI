
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 08/05/2019 - 8:31:43
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

Namespace KTB.DNET.BusinessFacade

    Public Class TOPSPPenaltyFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TOPSPPenaltyMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TOPSPPenaltyMapper = MapperFactory.GetInstance.GetMapper(GetType(TOPSPPenalty).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(TOPSPPenalty))
            Me.DomainTypeCollection.Add(GetType(TOPSPPenaltyDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TOPSPPenalty
            Return CType(m_TOPSPPenaltyMapper.Retrieve(ID), TOPSPPenalty)
        End Function

        Public Function RetrieveByHeaderID(ByVal HeaderID As String) As TOPSPPenalty
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TOPSPPenalty), "TOPSPTransferPayment.ID", MatchType.Exact, HeaderID))

            Dim TOPSPPenaltyColl As ArrayList = m_TOPSPPenaltyMapper.RetrieveByCriteria(criterias)
            If (TOPSPPenaltyColl.Count > 0) Then
                Return CType(TOPSPPenaltyColl(0), TOPSPPenalty)
            End If
            Return New TOPSPPenalty
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TOPSPPenaltyMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TOPSPPenaltyMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TOPSPPenaltyMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPSPPenalty), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TOPSPPenaltyMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPSPPenalty), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TOPSPPenaltyMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TOPSPPenalty As ArrayList = m_TOPSPPenaltyMapper.RetrieveByCriteria(criterias)
            Return _TOPSPPenalty
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TOPSPPenaltyColl As ArrayList = m_TOPSPPenaltyMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TOPSPPenaltyColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(TOPSPPenalty), SortColumn, sortDirection))
            Dim TOPSPPenaltyColl As ArrayList = m_TOPSPPenaltyMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TOPSPPenaltyColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TOPSPPenaltyMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TOPSPPenaltyColl As ArrayList = m_TOPSPPenaltyMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TOPSPPenaltyColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TOPSPPenaltyColl As ArrayList = m_TOPSPPenaltyMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TOPSPPenalty), columnName, matchOperator, columnValue))
            Return TOPSPPenaltyColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TOPSPPenalty), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), columnName, matchOperator, columnValue))

            Return m_TOPSPPenaltyMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPPenalty), "TOPSPPenaltyCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TOPSPPenalty), "TOPSPPenaltyCode", AggregateType.Count)
            Return CType(m_TOPSPPenaltyMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TOPSPPenalty) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TOPSPPenaltyMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TOPSPPenalty) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TOPSPPenaltyMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TOPSPPenalty)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TOPSPPenaltyMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TOPSPPenalty)
            Try
                m_TOPSPPenaltyMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is TOPSPTransferPayment) Then
                CType(InsertArg.DomainObject, TOPSPTransferPayment).ID = InsertArg.ID
                CType(InsertArg.DomainObject, TOPSPTransferPayment).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is TOPSPPenalty) Then
                CType(InsertArg.DomainObject, TOPSPPenalty).ID = InsertArg.ID
                CType(InsertArg.DomainObject, TOPSPPenalty).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is TOPSPPenaltyDetail) Then
                CType(InsertArg.DomainObject, TOPSPPenaltyDetail).ID = InsertArg.ID
                CType(InsertArg.DomainObject, TOPSPPenaltyDetail).MarkLoaded()
            End If
        End Sub

        Public Function InsertTransaction(ByVal objTOPSPTransferPayment As TOPSPTransferPayment, ByVal arrTOPSPPenalty As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objTOPSPTransferPayment, m_userPrincipal.Identity.Name)

                    If arrTOPSPPenalty.Count > 0 Then
                        For Each oTOPSPPenalty As TOPSPPenalty In arrTOPSPPenalty
                            oTOPSPPenalty.TOPSPTransferPayment = objTOPSPTransferPayment
                            m_TransactionManager.AddInsert(oTOPSPPenalty, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objTOPSPTransferPayment.ID
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

        Public Function UpdateTransaction(ByVal objTOPSPTransferPayment As TOPSPTransferPayment, ByVal arrTOPSPPenalty As ArrayList, ByVal arrDeletedTOPSPPenalty As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrDeletedTOPSPPenalty.Count > 0 Then
                        For Each item As TOPSPPenalty In arrDeletedTOPSPPenalty
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrTOPSPPenalty.Count > 0 Then
                        For Each item As TOPSPPenalty In arrTOPSPPenalty
                            item.TOPSPTransferPayment = objTOPSPTransferPayment
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objTOPSPTransferPayment, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objTOPSPTransferPayment.ID
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

        Public Function RetrieveSP(ByVal strSQL As String) As ArrayList
            Return CType(m_TOPSPPenaltyMapper.RetrieveSP(strSQL), ArrayList)
        End Function

        Function UpdateStatusTransaction(ByVal arrChecked As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not IsNothing(arrChecked) Then
                        If arrChecked.Count > 0 Then
                            For Each oTOPSPPenalty As TOPSPPenalty In arrChecked
                                m_TransactionManager.AddUpdate(oTOPSPPenalty, m_userPrincipal.Identity.Name)
                            Next
                        End If
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

        Public Function ValidateTOPSPPenalty(ByVal _oTOPSPPenalty As TOPSPPenalty) As TOPSPPenalty
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.TOPSPPenalty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(KTB.DNET.Domain.TOPSPPenalty), "Dealer.ID", MatchType.Exact, _oTOPSPPenalty.Dealer.ID))
            criteria.opAnd(New Criteria(GetType(KTB.DNET.Domain.TOPSPPenalty), "TOPSPTransferPayment.RegNumber", MatchType.Exact, _oTOPSPPenalty.TOPSPTransferPayment.RegNumber))
            Dim arlTOPSPPenalty As ArrayList = m_TOPSPPenaltyMapper.RetrieveByCriteria(criteria)
            If arlTOPSPPenalty.Count > 0 Then
                Return CType(arlTOPSPPenalty(0), TOPSPPenalty)
            End If
            Return Nothing
        End Function

        Public Function InsertFromWebSevice(ByVal _oTOPSPPenalty As TOPSPPenalty) As Short
            Dim returnValue As Integer = -1
            Dim isChange As New IsChangeFacade
            If Me.IsTaskFree() Then
                Try
                    '  Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim oTOPSPPenalty_old As TOPSPPenalty = ValidateTOPSPPenalty(_oTOPSPPenalty)

                    If IsNothing(oTOPSPPenalty_old) Then
                        m_TransactionManager.AddInsert(_oTOPSPPenalty, m_userPrincipal.Identity.Name)
                        If _oTOPSPPenalty.TOPSPPenaltyDetails.Count > 0 Then
                            For Each itemDetail As TOPSPPenaltyDetail In _oTOPSPPenalty.TOPSPPenaltyDetails
                                itemDetail.TOPSPPenalty = _oTOPSPPenalty
                                m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    Else
                        For Each itemDetail_Old As TOPSPPenaltyDetail In oTOPSPPenalty_old.TOPSPPenaltyDetails
                            itemDetail_Old.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(itemDetail_Old, m_userPrincipal.Identity.Name)
                        Next

                        For Each itemDetail As TOPSPPenaltyDetail In _oTOPSPPenalty.TOPSPPenaltyDetails
                            'Dim criterias As New CriteriaComposite(New Criteria(GetType(TOPSPPenaltyDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty.ID", MatchType.Exact, oTOPSPPenalty_old.ID))
                            criterias.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "SparePartBilling.ID", MatchType.Exact, itemDetail.SparePartBilling.ID))
                            criterias.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "AccountingDocNo", MatchType.Exact, itemDetail.AccountingDocNo))
                            Dim arlTOPSPPenaltyDetails As ArrayList = New TOPSPPenaltyDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WS TOPSPPenalty"), Nothing)).Retrieve(criterias)
                            If arlTOPSPPenaltyDetails.Count > 0 Then
                                Dim oTOPSPPenaltyDetDB As TOPSPPenaltyDetail = New TOPSPPenaltyDetail
                                oTOPSPPenaltyDetDB = CType(arlTOPSPPenaltyDetails(0), TOPSPPenaltyDetail)
                                ' this line is commanded where condition rowstatys has been updated to be -1 before. so

                                oTOPSPPenaltyDetDB.AccountingDocNo = itemDetail.AccountingDocNo
                                oTOPSPPenaltyDetDB.ActualTransferAmount = itemDetail.ActualTransferAmount
                                oTOPSPPenaltyDetDB.ActualTransferDate = itemDetail.ActualTransferDate
                                oTOPSPPenaltyDetDB.PenaltyDays = itemDetail.PenaltyDays
                                oTOPSPPenaltyDetDB.AmountPenalty = itemDetail.AmountPenalty
                                oTOPSPPenaltyDetDB.PPh = itemDetail.PPh
                                oTOPSPPenaltyDetDB.PaymentType = itemDetail.PaymentType
                                oTOPSPPenaltyDetDB.RowStatus = CType(DBRowStatus.Active, Short)

                                m_TransactionManager.AddUpdate(oTOPSPPenaltyDetDB, m_userPrincipal.Identity.Name)
                            Else
                                itemDetail.TOPSPPenalty = oTOPSPPenalty_old
                                m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            End If
                        Next
                        'update rowstatus = -1 for old detail

                        oTOPSPPenalty_old.Dealer = _oTOPSPPenalty.Dealer
                        oTOPSPPenalty_old.TOPSPTransferPayment = _oTOPSPPenalty.TOPSPTransferPayment
                        oTOPSPPenalty_old.Amount = _oTOPSPPenalty.Amount
                        oTOPSPPenalty_old.DebitMemoDate = _oTOPSPPenalty.DebitMemoDate
                        oTOPSPPenalty_old.DebitMemoNumber = _oTOPSPPenalty.DebitMemoNumber
                        m_TransactionManager.AddUpdate(oTOPSPPenalty_old, m_userPrincipal.Identity.Name)
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    '    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function


#End Region

    End Class

End Namespace

