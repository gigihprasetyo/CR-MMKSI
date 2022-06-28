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
'// Generated on 10/7/2005 - 1:28:25 PM
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
Imports KTB.DNet.SAP
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.BusinessFacade.PO

    Public Class POHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_POHeaderMapper As IMapper
        Private m_PODetailMapper As IMapper
        Private m_SalesOrderMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_POHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(POHeader).ToString)
            Me.m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)
            Me.m_SalesOrderMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesOrder).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.POHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PODetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As POHeader
            Return CType(m_POHeaderMapper.Retrieve(ID), POHeader)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_POHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_POHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal Code As String) As POHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(POHeader), "PONumber", MatchType.Exact, Code))

            Dim POHeaderColl As ArrayList = m_POHeaderMapper.RetrieveByCriteria(criterias)
            If (POHeaderColl.Count > 0) Then
                Return CType(POHeaderColl(0), POHeader)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(POHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_POHeaderMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_POHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal ContractHeaderID As Integer) As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(POHeader), "ContractHeaderID", _
                MatchType.Exact, ContractHeaderID))
            criterias.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, _
                CType(DBRowStatus.Active, Short)))
            Return m_POHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(POHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_POHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(POHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_POHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _POHeader As ArrayList = m_POHeaderMapper.RetrieveByCriteria(criterias)
            Return _POHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim POHeaderColl As ArrayList = m_POHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return POHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(POHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim POHeaderColl As ArrayList = m_POHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return POHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim POHeaderColl As ArrayList = m_POHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return POHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim POHeaderColl As ArrayList = m_POHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(POHeader), columnName, matchOperator, columnValue))
            Return POHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(POHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), columnName, matchOperator, columnValue))

            Return m_POHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "POHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(POHeader), "POHeaderCode", AggregateType.Count)
            Return CType(m_POHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.POHeader) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.POHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.POHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is PODetail) Then
                CType(InsertArg.DomainObject, PODetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertPOWithID(ByVal objDomain As KTB.DNet.Domain.POHeader) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If m_userPrincipal.Identity.Name = "" Then
                        _user = "SAP"
                    Else
                        _user = m_userPrincipal.Identity.Name
                    End If
                    For Each item As PODetail In objDomain.PODetails
                        item.POHeader = objDomain
                        m_TransactionManager.AddInsert(item, _user)
                    Next

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

            Dim objNewPO As POHeader = Me.Retrieve(objDomain.ID)
            Dim newID As Integer = objDomain.ID
            objNewPO.PONumber = objNewPO.PONumber.Substring(0, objNewPO.PONumber.Length - newID.ToString.Trim.Length) & newID
            Me.Update(objNewPO)

            Return returnValue
        End Function

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.POHeader) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If m_userPrincipal.Identity.Name = "" Then
                        _user = "SAP"
                    Else
                        _user = m_userPrincipal.Identity.Name
                    End If
                    For Each item As PODetail In objDomain.PODetails
                        item.POHeader = objDomain
                        m_TransactionManager.AddInsert(item, _user)
                    Next




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

        Public Function InsertFromSAP(ByVal objDomain As KTB.DNet.Domain.POHeader) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    objDomain.Status = enumStatusPO.Status.Selesai
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If m_userPrincipal.Identity.Name = "" Then
                        _user = "SAP"
                    Else
                        _user = m_userPrincipal.Identity.Name
                    End If
                    For Each item As PODetail In objDomain.PODetails
                        item.POHeader = objDomain
                        item.AllocQty = item.ReqQty

                        m_TransactionManager.AddInsert(item, _user)
                    Next


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

        Public Function UpdateTransaction(ByVal objDomainColl As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As POHeader In objDomainColl
                        For Each item1 As PODetail In item.PODetails
                            m_TransactionManager.AddUpdate(item1, m_userPrincipal.Identity.Name)
                        Next
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

        Public Function UpdateSingleDomainTransaction(ByVal objDomain As POHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As PODetail In objDomain.PODetails
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

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

        Public Sub Delete(ByVal objDomain As POHeader)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_POHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As POHeader)
            Try
                For Each oPOD As PODetail In objDomain.PODetails
                    m_PODetailMapper.Delete(oPOD)
                Next
                m_POHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
#End Region

#Region "Custom Method"

        Public Function IsEnabledCreditControl(ByVal objDealer As Dealer) As Boolean
            Dim objTransaction As TransactionControl = New DealerFacade(Me.m_userPrincipal).RetrieveTransactionControl(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.CreditControl).ToString)
            If (objTransaction Is Nothing) Then
                Return True
            Else
                If objTransaction.Status = EnumDealerStatus.DealerStatus.Aktive Then
                    Return True
                End If
            End If
            Return False
        End Function

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.POHeader) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If objDomain.RemarkStatus = enumPORemarkStatus.PORemarkStatus.TahanDO Then
                        objDomain.DOBlockHistory = 1 ' Pernah Tahan DO
                    End If
                    If objDomain.PODetails.Count > 0 Then
                        For Each objPODetail As PODetail In objDomain.PODetails
                            objPODetail.POHeader = objDomain
                            If m_userPrincipal.Identity.Name = "" Then
                                _user = "SAP"
                            Else
                                _user = m_userPrincipal.Identity.Name
                            End If
                            m_TransactionManager.AddUpdate(objPODetail, _user)
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
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
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function UpdatePassTop(ByVal objDomains As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If objDomains.Count > 0 Then
                        For Each item As POHeader In objDomains
                            If item.TotalQuantity > 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
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
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function RetrieveIDContract(ByVal id As Integer) As POHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(POHeader), "ContractHeader.ID", MatchType.Exact, id))

            Dim POHeaderColl As ArrayList = m_POHeaderMapper.RetrieveByCriteria(criterias)
            If (POHeaderColl.Count > 0) Then
                Return CType(POHeaderColl(0), POHeader)
            End If
            Return New POHeader
        End Function

        Public Sub SynchronizeToSAP(ByVal ArrlistPOHeader As ArrayList)
            For Each objPOHeader As POHeader In ArrlistPOHeader
                Dim objPOHeader1 As POHeader = Me.Retrieve(objPOHeader.PONumber.ToString)
                If Not objPOHeader1 Is Nothing Then
                    objPOHeader1.SONumber = objPOHeader.SONumber
                    Dim int As Integer = Me.Update(objPOHeader1)
                Else
                    Dim Valid As Boolean = True
                    For Each objPODetail As PODetail In objPOHeader.PODetails
                        If objPODetail.AllocQty > objPODetail.ContractDetail.SisaUnit Then
                            'TODO : Masukin error ke file Log
                            Valid = False
                            Exit For
                        End If
                    Next
                    If Valid Then
                        objPOHeader.Status = enumStatus.Status.Selesai
                        'Start  :Optimize EffectiveDate:dna:20091215
                        objPOHeader.EffectiveDate = Me.GetPOEffectiveDate(objPOHeader.ReqAllocationDateTime, objPOHeader.TermOfPayment.PaymentType, objPOHeader.TermOfPayment.TermOfPaymentValue)
                        'End    :Optimize EffectiveDate:dna:20091215

                        Dim int As Integer = Me.Insert(objPOHeader)
                    End If
                End If
            Next
        End Sub

        Private Sub UpdatePODetail(ByVal obj As PODetail)
            Dim _PODetailFacade As PODetailFacade = New PODetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            _PODetailFacade.Update(obj)
        End Sub

        Private Function GetPoDetailByPOHeaderAndLineNumber(ByVal poheaderID As Integer, ByVal lineItem As Integer) As PODetail
            Dim _PODetailFacade As PODetailFacade = New PODetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "POHeader.ID", MatchType.Exact, poheaderID))
            criterias.opAnd(New Criteria(GetType(PODetail), "LineItem", MatchType.Exact, lineItem))
            Dim listPODetail As ArrayList = _PODetailFacade.Retrieve(criterias)
            If listPODetail.Count > 0 Then
                Return CType(listPODetail(0), PODetail)
            Else
                Return New PODetail
            End If
        End Function

        Private Function GetDeletePO(ByVal oldPO As POHeader, ByVal newPO As POHeader) As ArrayList
            Dim oldDetails As ArrayList = oldPO.PODetails
            Dim newDetails As ArrayList = newPO.PODetails
            Dim found As Boolean = False
            Dim deleteList As ArrayList = New ArrayList
            For Each oldItem As PODetail In oldDetails
                found = False
                For Each newItem As PODetail In newDetails
                    If oldItem.LineItem = newItem.LineItem Then
                        found = True
                    End If
                Next
                If Not found Then
                    deleteList.Add(oldItem)
                End If
            Next
            Return deleteList
        End Function

        Public Function SynchronizeToSAP(ByVal objPOHeader As POHeader, Optional ByVal oSalesOrder As SalesOrder = Nothing, Optional ByVal fromWS As Boolean = False) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Dim Valid As Boolean = True
            Dim performTransaction As Boolean = True
            Dim arrSalesOrder = New ArrayList

            Dim IsChange As New IsChangeFacade()
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    'If Not IsSONumberExist(objPOHeader.SONumber, objPOHeader.PONumber) Then
                    'Dim objPOHeader1 As POHeader = Me.Retrieve(objPOHeader.PONumber.ToString)

                    Dim IsChangeSO As Boolean = False
                    Dim IsInterestChanged As Boolean = False

                    Dim objPOHeader1 As New POHeader
                    Dim objSalesOrder As SalesOrder = objPOHeader.SalesOrders(0)
                    Dim objSalesOrderData As SalesOrder
                    Dim criterias As CriteriaComposite
                    If objSalesOrder.PaymentRef <> String.Empty Then
                        objSalesOrderData = New SalesOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(objSalesOrder.PaymentRef)
                        If objSalesOrderData.ID > 0 Then
                            objPOHeader1 = Me.Retrieve(objSalesOrderData.POHeader.ID)
                        End If
                    Else
                        objPOHeader1 = Me.Retrieve(objPOHeader.PONumber.ToString)
                    End If

                    Dim oPOHeaderID As Integer = 0

                    If Not objPOHeader1 Is Nothing Then
                        oPOHeaderID = objPOHeader1.ID
                        For Each itemSO As SalesOrder In objPOHeader.SalesOrders
                            objSalesOrderData = New SalesOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(objPOHeader.SONumber)
                            If objSalesOrderData.ID = 0 Then
                                itemSO.POHeader = objPOHeader1
                                m_TransactionManager.AddInsert(itemSO, m_userPrincipal.Identity.Name)
                            Else
                                objSalesOrderData.Amount = itemSO.Amount
                                objSalesOrderData.SODate = itemSO.SODate
                                'objSalesOrderData.TotalVH = itemSO.TotalVH
                                'objSalesOrderData.TotalPP = itemSO.TotalPP
                                'objSalesOrderData.TotalIT = itemSO.TotalIT
                                If fromWS Then
                                    objSalesOrderData.TotalVH = itemSO.TotalVH
                                    objSalesOrderData.TotalPP = itemSO.TotalPP
                                    objSalesOrderData.TotalIT = itemSO.TotalIT

                                End If
                                'comment, issue amount vh pp it tidak terupdate
                                'If IsChange.IsChangeSO(objSalesOrderData) Then
                                '    IsChangeSO = True
                                '    m_TransactionManager.AddUpdate(objSalesOrderData, m_userPrincipal.Identity.Name)
                                'End If


                            End If
                        Next

                        If objSalesOrder.SOType <> "ZA5F" And objSalesOrder.SOType <> "ZA5W" Then
                            objPOHeader1.SONumber = objPOHeader.SONumber
                            objPOHeader1.ContractHeader = objPOHeader.ContractHeader
                            objPOHeader1.ReleaseDate = objPOHeader.ReleaseDate
                            objPOHeader1.ReleaseYear = objPOHeader.ReleaseYear
                            objPOHeader1.ReleaseMonth = objPOHeader.ReleaseMonth

                            objPOHeader1.DealerPONumber = objPOHeader.DealerPONumber
                            objPOHeader1.LastUpdateTime = Now
                            objPOHeader1.LastUpdateBy = "WSM"
                            objPOHeader1.ReqAllocationDate = objPOHeader.ReqAllocationDate
                            objPOHeader1.ReqAllocationYear = objPOHeader.ReqAllocationYear
                            objPOHeader1.ReqAllocationMonth = objPOHeader.ReqAllocationMonth
                            objPOHeader1.ReqAllocationDateTime = objPOHeader.ReqAllocationDateTime
                            objPOHeader1.FreePPh22Indicator = objPOHeader.FreePPh22Indicator
                            If objPOHeader.Status = CInt(enumStatusPO.Status.DiBlok).ToString() Then
                                objPOHeader1.Status = CInt(enumStatusPO.Status.DiBlok)
                            End If

                            objPOHeader1.Status = CInt(enumStatusPO.Status.Selesai)

                            If objPOHeader.Status = CInt(enumStatusPO.Status.DiBlok).ToString() Then
                                objPOHeader1.Status = CInt(enumStatusPO.Status.DiBlok)
                            End If

                            If objPOHeader.PONumber <> String.Empty Then
                                objPOHeader1.PONumber = objPOHeader.PONumber
                            End If

                            'Dim deleteList As ArrayList = GetDeletePO(objPOHeader1, objPOHeader)
                            'For Each delItem As poDetail In deleteList
                            '    m_TransactionManager.AddDelete(delItem)
                            'Next
                            If Not objPOHeader.ContractHeader Is Nothing Then
                                objPOHeader.Dealer = objPOHeader.ContractHeader.Dealer
                            End If


                            Dim freeIntIndicator As Integer = objPOHeader1.ContractHeader.PKHeader.FreeIntIndicator
                            Dim PODIdx As Integer = 0

                            If (objPOHeader.TermOfPayment.ID <> objPOHeader1.TermOfPayment.ID) OrElse objPOHeader.IsFactoring <> objPOHeader1.IsFactoring Then
                                IsInterestChanged = True
                            End If
                            For Each item As PODetail In objPOHeader.PODetails
                                Dim poDetail As PODetail = GetPoDetailByPOHeaderAndLineNumber(objPOHeader1.ID, item.LineItem)
                                If Not objPOHeader.ContractHeader Is Nothing Then
                                    If poDetail.AllocQty < item.AllocQty Then
                                        If (item.AllocQty - poDetail.AllocQty) > poDetail.ContractDetail.SisaUnit Then
                                            Valid = False
                                        End If
                                        'If poDetail.AllocQty > poDetail.ContractDetail.SisaUnit Then
                                        '    Valid = False
                                        'End If
                                    End If
                                    If IsInterestChanged Then
                                        Dim objCD As ContractDetail = poDetail.ContractDetail
                                        'Change CR 
                                        ' Dim objPrice As Price = objCD.VechileColor.Price(objPOHeader1.CreatedTime)
                                        Dim objPrice As Price = objCD.VechileColor.Price(objPOHeader.ContractHeader)
                                        If IsNothing(objPrice) Then objPrice = New Price

                                        If objPOHeader1.IsFactoring = 1 Then
                                            ''Append Code Date 2014-08-29
                                            '' CR Factory DIscount
                                            '' By Ali Akbar
                                            Dim _interest As Double = 0
                                            Dim nTOP As Integer = objPOHeader.TermOfPayment.TermOfPaymentValue
                                            Dim nMonth As Integer = DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth)
                                            _interest = Calculation.CountRewardsInterest(objCD, objPrice, nTOP, nMonth, objPOHeader1.ContractHeader.PKHeader.FreeIntIndicator)

                                            poDetail.DiscountReward = objPrice.DiscountReward
                                            poDetail.AmountReward = Calculation.CountRewardAmount(objCD, objPrice, nTOP, nMonth)
                                            poDetail.Price = Calculation.CountRewardsVehiclePrice(objCD, objPrice, nTOP, nMonth)
                                            poDetail.PPh22 = Calculation.CountRewardPPh22(objCD, objPrice, nTOP, nMonth)
                                            poDetail.Interest = FormatNumber(1 * _interest, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                                            poDetail.AmountRewardDepA = Calculation.CountRewardAmountDepositA(objPrice, nTOP, nMonth)

                                            ''End Append
                                            'Commented Out Ut By CR
                                            'poDetail.Interest = FormatNumber(1 * _
                                            '    objPOHeader1.ContractHeader.PKHeader.FreeIntIndicator * _
                                            '    Calculation.CountInterest(objPOHeader.TermOfPayment.TermOfPaymentValue _
                                            '        , DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth) _
                                            '        , objPrice.FactoringInt, objCD.Amount - objCD.GuaranteeAmount _
                                            '        , objPrice.PPh23), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                                        Else
                                            ''Append Code Date 2014-08-29
                                            '' CR Factory DIscount
                                            '' By Ali Akbar
                                            poDetail.DiscountReward = 0
                                            poDetail.AmountReward = 0
                                            poDetail.Price = objCD.Amount
                                            poDetail.PPh22 = objCD.PPh22
                                            poDetail.AmountRewardDepA = 0
                                            ''End Append
                                            Dim itFreedays As Integer = 0
                                            itFreedays = objPOHeader.TermOfPayment.TermOfPaymentValue - poDetail.FreeDays
                                            If itFreedays < 0 Then
                                                itFreedays = 0
                                            End If
                                            poDetail.Interest = FormatNumber(1 * _
                                                objPOHeader1.ContractHeader.PKHeader.FreeIntIndicator * _
                                                Calculation.CountInterest(itFreedays _
                                                    , DateTime.DaysInMonth(objPOHeader.ContractHeader.ContractPeriodYear, objPOHeader.ContractHeader.ContractPeriodMonth) _
                                                    , objPrice.Interest _
                                                    , objCD.Amount - objCD.GuaranteeAmount _
                                                    , objPrice.PPh23), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                                        End If
                                    End If
                                    If Valid Then
                                        If poDetail.ID > 0 Then
                                            'poDetail.ReqQty = item.ReqQty
                                            poDetail.AllocQty = item.AllocQty
                                            If IsChange.IsChangePODetail(poDetail) Then
                                                m_TransactionManager.AddUpdate(poDetail, m_userPrincipal.Identity.Name)
                                            End If

                                        End If
                                    Else
                                        Throw New Exception("Alocation Qty Greater then Sisa Unit.")
                                    End If
                                End If
                            Next
                            objPOHeader1.TermOfPayment = objPOHeader.TermOfPayment
                            'Start  :Optimize EffectiveDate:dna:20091215
                            objPOHeader1.EffectiveDate = Me.GetPOEffectiveDate(objPOHeader1.ReqAllocationDateTime, objPOHeader1.TermOfPayment.PaymentType, objPOHeader1.TermOfPayment.TermOfPaymentValue)
                            'End    :Optimize EffectiveDate:dna:20091215
                            If IsChange.IsChangePOHeader(objPOHeader1) OrElse IsChangeSO OrElse IsInterestChanged Then
                                m_TransactionManager.AddUpdate(objPOHeader1, m_userPrincipal.Identity.Name)
                            End If

                        End If
                    Else
                        oPOHeaderID = objPOHeader.ID
                        For Each objPODetail As PODetail In objPOHeader.PODetails
                            If objPODetail.AllocQty > objPODetail.ContractDetail.SisaUnit Then
                                Valid = False
                                Exit For
                            End If
                        Next

                        If Valid Then
                            If Not objPOHeader.ContractHeader Is Nothing Then
                                objPOHeader.Dealer = objPOHeader.ContractHeader.Dealer
                            End If
                            objPOHeader.Status = enumStatusPO.Status.Selesai
                            'Start  :Optimize EffectiveDate:dna:20091215
                            objPOHeader.EffectiveDate = Me.GetPOEffectiveDate(objPOHeader.ReqAllocationDateTime, objPOHeader.TermOfPayment.PaymentType, objPOHeader.TermOfPayment.TermOfPaymentValue)
                            'End    :Optimize EffectiveDate:dna:20091215
                            m_TransactionManager.AddInsert(objPOHeader, m_userPrincipal.Identity.Name)
                            If m_userPrincipal.Identity.Name = "" Then
                                _user = "SAP"
                            Else
                                _user = m_userPrincipal.Identity.Name
                            End If

                            For Each item As PODetail In objPOHeader.PODetails
                                item.POHeader = objPOHeader
                                item.AllocQty = item.ReqQty
                                m_TransactionManager.AddInsert(item, _user)
                            Next

                            For Each itemSO As SalesOrder In objPOHeader.SalesOrders
                                objSalesOrderData = New SalesOrderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(objPOHeader.SONumber)
                                If objSalesOrderData.ID = 0 Then
                                    itemSO.POHeader = objPOHeader
                                    m_TransactionManager.AddInsert(itemSO, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        Else
                            Throw New Exception("Alocation Qty Greater then Sisa Unit.")
                        End If
                    End If
                    If performTransaction And Valid Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If

                    If Not IsNothing(oSalesOrder) Then
                        UpdateSOandSODueDate(oPOHeaderID, oSalesOrder.SONumber, oSalesOrder.Amount, oSalesOrder.SODate, _
                                         oSalesOrder.TotalVH, oSalesOrder.TotalPP, oSalesOrder.TotalIT, "WS")
                    End If

                    'Else
                    'Throw New Exception("So Number already exist")
                    'End If
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

        Private Function IsSONumberExist(ByVal soNumber As String, ByVal poNumber As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, soNumber.Trim))
            criterias.opAnd(New Criteria(GetType(POHeader), "PONumber", MatchType.Exact, poNumber.Trim))
            Dim aggregates As New Aggregate(GetType(POHeader), "ID", AggregateType.Count)
            If CType(m_POHeaderMapper.RetrieveScalar(aggregates, criterias), Integer) > 0 Then
                Return True
            End If
            Return False
        End Function

        Private Function GetAvalibleSPL(ByVal objPOList As ArrayList, ByVal conStr As String) As ArrayList
            Dim result As ArrayList = New ArrayList
            Dim listSapCr As ArrayList = New ArrayList
            Dim oParamSAPCreditCeiling As SAPCreditCeiling
            If objPOList.Count > 0 Then
                For Each item As POHeader In objPOList
                    oParamSAPCreditCeiling = New SAPCreditCeiling
                    oParamSAPCreditCeiling.DealerCode = item.ContractHeader.Dealer.LegalStatus
                    oParamSAPCreditCeiling.PeriodMonth = item.ContractHeader.ContractPeriodMonth
                    oParamSAPCreditCeiling.PeriodYear = item.ContractHeader.ContractPeriodYear
                    oParamSAPCreditCeiling.SPLNumber = item.ContractHeader.SPLNumber
                    If item.ContractHeader.SPLNumber.Trim <> "" Then
                        listSapCr.Add(oParamSAPCreditCeiling)
                    End If
                Next
                Dim sapresult As ArrayList = GetCreditCeiling(listSapCr, conStr)
                For Each sapItem As SAPCreditCeiling In sapresult
                    If sapItem.TemporaryType <> "" Then
                        result.Add(sapItem.SPLNumber)
                    End If
                Next
            End If
            Return result
        End Function

        Private Function IsSPLExistInSAPList(ByVal spl As String, ByVal sapList As ArrayList) As Boolean
            For Each item As String In sapList
                If item.ToUpper.Trim = spl.ToUpper.Trim Then
                    Return True
                End If
            Next
            Return False
        End Function

        Private Function GetDueDateUsingDailyPayment(ByVal objPO As POHeader) As Date
            Dim objDailyPaymentFacade As DailyPaymentFacade = New DailyPaymentFacade(Me.m_userPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, objPO.ID))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.PaymentPurposeCode", MatchType.[Partial], "VH"))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, 0))
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DailyPayment), "ID", Sort.SortDirection.DESC))
            Dim list As ArrayList = objDailyPaymentFacade.Retrieve(criterias, sortColl)
            Dim objPayment As DailyPayment
            If list.Count > 0 Then
                objPayment = CType(list(0), DailyPayment)
                Return objPayment.BaselineDate
            Else
                Return New Date(1900, 1, 1)
            End If
        End Function

        Public Function GetCreditExposure(ByVal objDealer As Dealer, ByVal objTOP As TermOfPayment, ByVal currentPO As POHeader, ByVal isRegular As Boolean, ByVal SPL As String, ByVal constr As String) As Double
            Dim result As Double = 0
            Dim dueDateCurrentPO As Date = currentPO.ReqAllocationDateTime.AddDays(currentPO.TermOfPayment.TermOfPaymentValue)
            Dim reqDeliveryDateCurrentPO As Date = currentPO.ReqAllocationDateTime
            Dim listSelectedPO As ArrayList
            Dim dueDate As Date
            Dim reqDeliverDate As Date
            Dim firstCondition As Boolean = False
            Dim secondCondition As Boolean = False
            Dim thirdCondition As Boolean = False
            Dim fourthCondition As Boolean = False
            dueDateCurrentPO = New Date(dueDateCurrentPO.Year, dueDateCurrentPO.Month, dueDateCurrentPO.Day, 0, 0, 0)
            reqDeliveryDateCurrentPO = New Date(reqDeliveryDateCurrentPO.Year, reqDeliveryDateCurrentPO.Month, reqDeliveryDateCurrentPO.Day, 0, 0, 0)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.TermOfPaymentValue", MatchType.Greater, 0))
            criterias.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Baru, Integer) & "," & CType(enumStatusPO.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Konfirmasi, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
            criterias.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.LegalStatus", MatchType.Exact, objDealer.LegalStatus))

            Dim sapResultList As ArrayList
            If isRegular Then
                Dim splCriteriaList As ArrayList = New ArrayList
                listSelectedPO = Me.Retrieve(criterias)
                If listSelectedPO.Count > 0 Then
                    For Each item As POHeader In listSelectedPO
                        If item.SONumber.Trim <> "" Then
                            Dim paymentDueDate As Date = GetDueDateUsingDailyPayment(item)
                            If paymentDueDate > New Date(1901, 1, 1) Then
                                dueDate = paymentDueDate
                            Else
                                dueDate = item.ReqAllocationDateTime.AddDays(item.TermOfPayment.TermOfPaymentValue).AddDays(objDealer.DueDate)
                            End If
                        Else
                            dueDate = item.ReqAllocationDateTime.AddDays(item.TermOfPayment.TermOfPaymentValue).AddDays(objDealer.DueDate)
                        End If
                        dueDate = New Date(dueDate.Year, dueDate.Month, dueDate.Day, 0, 0, 0)
                        reqDeliverDate = item.ReqAllocationDateTime
                        reqDeliverDate = New Date(reqDeliverDate.Year, reqDeliverDate.Month, reqDeliverDate.Day, 0, 0, 0)
                        firstCondition = reqDeliverDate <= reqDeliveryDateCurrentPO And dueDate <= dueDateCurrentPO And dueDate >= reqDeliveryDateCurrentPO
                        secondCondition = reqDeliverDate >= reqDeliveryDateCurrentPO And dueDate <= dueDateCurrentPO
                        thirdCondition = reqDeliverDate >= reqDeliveryDateCurrentPO And dueDate >= dueDateCurrentPO And reqDeliverDate <= dueDateCurrentPO
                        fourthCondition = reqDeliverDate <= reqDeliveryDateCurrentPO And dueDate >= dueDateCurrentPO

                        If (firstCondition Or secondCondition Or thirdCondition Or fourthCondition) Then
                            splCriteriaList.Add(item)
                        End If
                    Next
                    sapResultList = GetAvalibleSPL(splCriteriaList, constr)
                    For Each finalItem As POHeader In splCriteriaList

                        If Not IsSPLExistInSAPList(finalItem.ContractHeader.SPLNumber, sapResultList) Then
                            result += finalItem.TotalHargaExposure
                        End If
                    Next
                End If
            Else
                criterias.opAnd(New Criteria(GetType(POHeader), "ContractHeader.SPLNumber", MatchType.Exact, SPL))
                listSelectedPO = Me.Retrieve(criterias)
                If listSelectedPO.Count > 0 Then
                    For Each item As POHeader In listSelectedPO
                        If item.SONumber.Trim <> "" Then
                            Dim paymentDueDate As Date = GetDueDateUsingDailyPayment(item)
                            If paymentDueDate < New Date(1901, 1, 1) Then
                                dueDate = paymentDueDate
                            Else
                                dueDate = item.ReqAllocationDateTime.AddDays(item.TermOfPayment.TermOfPaymentValue).AddDays(objDealer.DueDate)
                            End If
                        Else
                            dueDate = item.ReqAllocationDateTime.AddDays(item.TermOfPayment.TermOfPaymentValue).AddDays(objDealer.DueDate)
                        End If
                        dueDate = New Date(dueDate.Year, dueDate.Month, dueDate.Day, 0, 0, 0)
                        reqDeliverDate = item.ReqAllocationDateTime
                        reqDeliverDate = New Date(reqDeliverDate.Year, reqDeliverDate.Month, reqDeliverDate.Day, 0, 0, 0)
                        firstCondition = reqDeliverDate <= reqDeliveryDateCurrentPO And dueDate <= dueDateCurrentPO And dueDate >= reqDeliveryDateCurrentPO
                        secondCondition = reqDeliverDate >= reqDeliveryDateCurrentPO And dueDate <= dueDateCurrentPO
                        thirdCondition = reqDeliverDate >= reqDeliveryDateCurrentPO And dueDate >= dueDateCurrentPO And reqDeliverDate <= dueDateCurrentPO
                        fourthCondition = reqDeliverDate <= reqDeliveryDateCurrentPO And dueDate >= dueDateCurrentPO

                        reqDeliverDate = item.ReqAllocationDateTime
                        reqDeliverDate = New Date(reqDeliverDate.Year, reqDeliverDate.Month, reqDeliverDate.Day, 0, 0, 0)
                        If (firstCondition Or secondCondition Or thirdCondition Or fourthCondition) Then
                            result += item.TotalHargaExposure
                        End If
                    Next
                End If
            End If
            Return result
        End Function

        Public Function GetPaymentRejection(ByVal objDealer As Dealer, ByVal objTOP As TermOfPayment) As Integer
            'TODO PENDING
            Return 0
        End Function

        Private Function FillCreditCeilingFromSAP(ByVal objSAPCR As ZFUST0042) As SAPCreditCeiling
            Dim objCreditCeiling As SAPCreditCeiling = New SAPCreditCeiling
            Try
                objCreditCeiling.DealerCode = objSAPCR.Knkli
                objCreditCeiling.BlokedAmount = CInt(objSAPCR.Blimk)
                objCreditCeiling.BlokedName = objSAPCR.Blnam
                objCreditCeiling.BufferDay = 0
                objCreditCeiling.CeilingAmount = CLng(objSAPCR.Klimk)
                objCreditCeiling.CreditAccount = objSAPCR.Knkli
                objCreditCeiling.ModifyName = objSAPCR.Mfnam
                objCreditCeiling.SPLNumber = objSAPCR.Splnm
                objCreditCeiling.TemporaryCode = objSAPCR.Tmcod
                objCreditCeiling.TemporaryKind = objSAPCR.Tmknd
                objCreditCeiling.TemporaryType = objSAPCR.Tmtyp
                objCreditCeiling.PeriodMonth = CInt(objSAPCR.Klmonth)
                objCreditCeiling.PeriodYear = CInt(objSAPCR.Klyear)
                If objSAPCR.Bldat.Length > 3 Then
                    If Not objSAPCR.Bldat.Substring(0, 3) = "000" Then
                        objCreditCeiling.BlockedDate = ConvertToDate(objSAPCR.Bldat)
                    End If
                End If
                If objSAPCR.Klfrm.Length > 3 Then
                    If Not objSAPCR.Klfrm.Substring(0, 3) = "000" Then
                        objCreditCeiling.ValidFrom = ConvertToDate(objSAPCR.Klfrm)
                    End If
                End If
                If objSAPCR.Kldto.Length > 0 Then
                    If Not objSAPCR.Kldto.Substring(0, 3) = "000" Then
                        objCreditCeiling.ValidTo = ConvertToDate(objSAPCR.Kldto)
                    End If
                End If

            Catch ex As Exception
            End Try
            Return objCreditCeiling
        End Function

        Private Function ConvertToDate(ByVal strDate As String) As Date
            Dim _dt As Date = New Date(strDate.Substring(4, 4), strDate.Substring(2, 2), strDate.Substring(0, 2))
            Return _dt
        End Function

        Public Function CountACC(ByVal objContract As ContractHeader, ByVal objDealer As Dealer, ByVal objTOP As TermOfPayment, ByVal currentPO As POHeader, ByVal ConStr As String) As Long
            Dim result As Double = 0
            Dim oParamSAPCreditCeiling As SAPCreditCeiling = New SAPCreditCeiling
            oParamSAPCreditCeiling.DealerCode = objDealer.LegalStatus
            oParamSAPCreditCeiling.PeriodMonth = objContract.ContractPeriodMonth
            oParamSAPCreditCeiling.PeriodYear = objContract.ContractPeriodYear
            oParamSAPCreditCeiling.SPLNumber = objContract.SPLNumber
            Dim listSapCr As ArrayList = New ArrayList
            listSapCr.Add(oParamSAPCreditCeiling)
            Dim isRegular As Boolean = False
            Dim objSAPCreditCeiling As SAPCreditCeiling = GetCreditCeiling(listSapCr, ConStr)(0)
            If objSAPCreditCeiling.TemporaryType = "" Then
                isRegular = True
            End If
            Dim CreditCeilingAmount As Double = 0
            Dim totalCreditExposure As Double = 0
            Dim totalPaymentRejection As Double = 0
            If Not objSAPCreditCeiling Is Nothing Then
                CreditCeilingAmount = objSAPCreditCeiling.CeilingAmount - objSAPCreditCeiling.BlokedAmount
                totalCreditExposure = GetCreditExposure(objDealer, objTOP, currentPO, isRegular, objContract.SPLNumber.Trim, ConStr)
                totalPaymentRejection = GetPaymentRejection(objDealer, objTOP)
            End If
            result = CreditCeilingAmount - totalCreditExposure - totalPaymentRejection
            Return result
        End Function

        Public Function GetCreditCeiling(ByVal ojbSAPCRList As ArrayList, ByVal constr As String) As ArrayList
            Dim oSAPDnet As SAPDNet = New SAPDNet(constr)
            Dim listSAPCRReturn As ArrayList = New ArrayList
            Dim oCR As SAPCreditCeiling
            Try
                Dim list As ArrayList = oSAPDnet.GetCreditControl(ojbSAPCRList)
                'For Each item As ZFUST0042Table In list
                '    For Each dt As ZFUST0042 In item
                '        oCR = New SAPCreditCeiling
                '        oCR = FillCreditCeilingFromSAP(dt)
                '        listSAPCRReturn.Add(oCR)
                '    Next
                'Next
                For Each dt As ZFUST0042 In list
                    oCR = New SAPCreditCeiling
                    oCR = FillCreditCeilingFromSAP(dt)
                    listSAPCRReturn.Add(oCR)
                Next
                Return listSAPCRReturn
            Catch ex As Exception
                Throw ex
            End Try
            Return New ArrayList
        End Function

        Public Function GetCreditOutstandingReportCeiling(ByVal pLegalStatus As String, ByVal pDueDate As DateTime, ByVal pTempKind As String, ByVal ConStr As String, ByRef TotalRow As Integer) As ArrayList
            Dim Criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Criterias.opAnd(New Criteria(GetType(SPL), "ValidFrom", MatchType.LesserOrEqual, pDueDate))
            Criterias.opAnd(New Criteria(GetType(SPL), "ValidTo", MatchType.GreaterOrEqual, pDueDate))
            Dim ListSPL As New ArrayList
            ListSPL = New SPLFacade(m_userPrincipal).Retrieve(Criterias)
            Dim oParamSAPCreditCeiling As SAPCreditCeiling
            Dim listSapCr As ArrayList = New ArrayList
            For Each sLegalStatus As String In pLegalStatus.Split(";")
                If sLegalStatus.Trim <> String.Empty Then
                    For Each SPLItem As SPL In ListSPL
                        oParamSAPCreditCeiling = New SAPCreditCeiling
                        oParamSAPCreditCeiling.DealerCode = sLegalStatus
                        oParamSAPCreditCeiling.PeriodMonth = pDueDate.Month
                        oParamSAPCreditCeiling.PeriodYear = pDueDate.Year
                        oParamSAPCreditCeiling.SPLNumber = SPLItem.SPLNumber
                        listSapCr.Add(oParamSAPCreditCeiling)
                    Next
                End If
            Next
            Dim objSAPCreditCeiling As ArrayList = GetCreditCeiling(listSapCr, ConStr)
            Dim ResultList As New ArrayList
            If objSAPCreditCeiling.Count > 0 Then
                For Each item As SAPCreditCeiling In objSAPCreditCeiling
                    If Not item.TemporaryType.Trim = String.Empty Then
                        If pTempKind = String.Empty Then
                            ResultList.Add(item)
                        Else
                            If item.TemporaryKind.IndexOf(pTempKind.Trim()) >= 0 Then
                                ResultList.Add(item)
                            End If
                        End If
                    End If
                Next
            End If
            TotalRow = ResultList.Count
            Return ResultList
        End Function

        Public Function GetCreditTempReportCeiling(ByVal pLegalStatus As String, ByVal pDueDate As DateTime, ByVal ConStr As String) As ArrayList
            Dim Criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Criterias.opAnd(New Criteria(GetType(SPL), "ValidFrom", MatchType.LesserOrEqual, pDueDate))
            Criterias.opAnd(New Criteria(GetType(SPL), "ValidTo", MatchType.GreaterOrEqual, pDueDate))
            Dim ListSPL As New ArrayList
            ListSPL = New SPLFacade(m_userPrincipal).Retrieve(Criterias)
            Dim oParamSAPCreditCeiling As SAPCreditCeiling
            Dim listSapCr As ArrayList = New ArrayList
            For Each sLegalStatus As String In pLegalStatus.Split(";")
                If sLegalStatus.Trim <> String.Empty Then
                    For Each SPLItem As SPL In ListSPL
                        oParamSAPCreditCeiling = New SAPCreditCeiling
                        oParamSAPCreditCeiling.DealerCode = sLegalStatus
                        oParamSAPCreditCeiling.PeriodMonth = pDueDate.Month
                        oParamSAPCreditCeiling.PeriodYear = pDueDate.Year
                        oParamSAPCreditCeiling.SPLNumber = SPLItem.SPLNumber
                        listSapCr.Add(oParamSAPCreditCeiling)
                    Next
                End If
            Next
            Dim objSAPCreditCeiling As ArrayList = GetCreditCeiling(listSapCr, ConStr)
            Dim ResultList As New ArrayList
            If objSAPCreditCeiling.Count > 0 Then
                For Each item As SAPCreditCeiling In objSAPCreditCeiling
                    If Not item.TemporaryType.Trim = String.Empty Then
                        ResultList.Add(item)
                    End If
                Next
            End If
            Return ResultList
        End Function

        Public Function GetCreditPosotionReportCeiling(ByVal pLegalStatus As String, ByVal pDueDate As DateTime, ByVal ConStr As String, ByRef TotalRow As Integer) As ArrayList
            Dim oParamSAPCreditCeiling As SAPCreditCeiling
            Dim listSapCr As ArrayList = New ArrayList
            For Each sLegalStatus As String In pLegalStatus.Split(";")
                If sLegalStatus.Trim <> String.Empty Then
                    oParamSAPCreditCeiling = New SAPCreditCeiling
                    oParamSAPCreditCeiling.DealerCode = sLegalStatus
                    oParamSAPCreditCeiling.PeriodMonth = pDueDate.Month
                    oParamSAPCreditCeiling.PeriodYear = pDueDate.Year
                    oParamSAPCreditCeiling.SPLNumber = ""
                    listSapCr.Add(oParamSAPCreditCeiling)
                End If
            Next
            Dim objSAPCreditCeiling As ArrayList = GetCreditCeiling(listSapCr, ConStr)
            TotalRow = objSAPCreditCeiling.Count
            Return objSAPCreditCeiling
        End Function

        Private Function RetrieveBySONumber(ByVal Code As String) As POHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, Code))

            Dim POHeaderColl As ArrayList = m_POHeaderMapper.RetrieveByCriteria(criterias)
            If (POHeaderColl.Count > 0) Then
                Return CType(POHeaderColl(0), POHeader)
            End If
            Return Nothing
        End Function

        Public Function RetrieveByContract(ByVal Code As Integer) As POHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(POHeader), "ContractHeader.ID", MatchType.Exact, Code))

            Dim POHeaderColl As ArrayList = m_POHeaderMapper.RetrieveByCriteria(criterias)
            If (POHeaderColl.Count > 0) Then
                Return CType(POHeaderColl(0), POHeader)
            End If
            Return Nothing
        End Function

        Public Shared Function IsCODValid(ByVal POH As POHeader, ByRef msg As String) As Boolean
            msg = ""

            Try
                Dim oMTDFac As New MaxTOPDayFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                If POH.TermOfPayment.PaymentType = CInt(enumPaymentType.PaymentType.COD) Then
                    For Each pod As PODetail In POH.PODetails
                        Dim isCOD As New ArrayList


                        Dim cMTD As New CriteriaComposite(New Criteria(GetType(MaxTOPDay), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active.Active, Short)))
                        cMTD.opAnd(New Criteria(GetType(MaxTOPDay), "DealerID", MatchType.Exact, POH.Dealer.ID))
                        cMTD.opAnd(New Criteria(GetType(MaxTOPDay), "VechileTypeID", MatchType.Exact, pod.ContractDetail.VechileColor.VechileType.ID))

                        isCOD = oMTDFac.Retrieve(cMTD)

                        If Not IsNothing(isCOD) AndAlso isCOD.Count > 0 Then
                            If CType(isCOD(0), MaxTOPDay).IsCOD = 0 Then
                                msg = msg & " " & pod.ContractDetail.VechileColor.VechileType.VechileCodeDesc & " \n"
                            End If
                        End If

                    Next

                    If msg <> "" Then

                        msg = "Kendaraan \n " & msg & " tidak berhak COD"
                        Return False
                    End If
                End If
            Catch ex As Exception

            End Try


            Return True
        End Function

        Public Shared Function GetMinTOPDaysByVehicleType(ByVal oPOH As POHeader, ByVal aPODs As ArrayList, Optional ByVal IsFactoring As Boolean = False) As Integer
            Dim MinTOPDays As Integer = 10000
            Dim IsExist As Boolean = False
            Dim MinTemp As Integer
            Dim oMTDFac As New MaxTOPDayFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            For Each oPOD As PODetail In aPODs
                'If oPOD.ContractDetail.VechileColor.VechileType.MaxTOPDays > 0 AndAlso oPOD.ContractDetail.VechileColor.VechileType.MaxTOPDays < MinTOPDays Then
                '    MinTOPDays = oPOD.ContractDetail.VechileColor.VechileType.MaxTOPDays
                '    IsExist = True
                'End If

                MinTemp = oMTDFac.GetMinTOPDays(oPOH.Dealer.ID, oPOD.ContractDetail.VechileColor.VechileType.ID, IsFactoring)
                If MinTemp <= MinTOPDays Then
                    MinTOPDays = MinTemp
                    IsExist = True
                End If
            Next
            If IsExist = True Then
                Return MinTOPDays
            Else
                Return 0 'will be validated by number of days in current month
            End If
            Return MinTOPDays
        End Function

#Region "EffectiveDate"

        Public Function GetPOEffectiveDate(ByVal ReqAllocDateTime As Date, ByVal PaymentType As Integer, Optional ByVal TOPValue As Integer = 0) As Date
            Dim EffDate As Date = Date.MinValue

            If PaymentType = enumPaymentType.PaymentType.COD Then
                EffDate = AddNWorkingDay(ReqAllocDateTime, 2)
            ElseIf PaymentType = enumPaymentType.PaymentType.TOP Then
                EffDate = AddNWorkingDay(ReqAllocDateTime.AddDays(TOPValue), 1)
            ElseIf PaymentType = enumPaymentType.PaymentType.RTGS Then
                EffDate = ReqAllocDateTime
            End If

            Return EffDate
        End Function

        Private Function AddNWorkingDay(ByVal StateDate As Date, ByVal nAdded As Integer, Optional ByVal IsBackWard As Boolean = False) As Date
            'it has the same logic in Utility.CommonFunction.AddNWorkingDay
            Dim objNHFac As NationalHolidayFacade = New NationalHolidayFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("asp"), Nothing)))
            Dim crtNH As CriteriaComposite
            Dim rslDate As Date
            Dim IsHoliday As Boolean = True
            Dim arlNH As ArrayList = New ArrayList
            Dim i As Integer = 0

            rslDate = StateDate
            For i = 1 To Math.Abs(nAdded)
                rslDate = rslDate.AddDays(IIf(IsBackWard, -1, 1))
                IsHoliday = True
                While IsHoliday = True
                    crtNH = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, rslDate))
                    arlNH = objNHFac.Retrieve(crtNH)
                    If arlNH.Count < 1 Then
                        IsHoliday = False
                    Else
                        rslDate = rslDate.AddDays(IIf(IsBackWard = False, 1, -1))
                    End If
                End While
            Next
            Return rslDate
        End Function
#End Region

        Public Function UpdateSOandSODueDate(ByVal POHeaderIDParam As Integer,
            ByVal SONumberParam As String,
            ByVal AmountParam As Decimal,
            ByVal SODateParam As DateTime,
            ByVal AmountVHParam As Decimal,
            ByVal AmountPPParam As Decimal,
            ByVal AmountITParam As Decimal, ByVal LastUpdateByParam As String) As Boolean
            Dim spName As String
            Dim Param As New List(Of SqlClient.SqlParameter)

            spName = "sp_UpdateSOandSODueDate"
            Param.Add(New SqlClient.SqlParameter("@POHeaderID", POHeaderIDParam))
            Param.Add(New SqlClient.SqlParameter("@SONumber", SONumberParam))
            Param.Add(New SqlClient.SqlParameter("@Amount", AmountParam))
            Param.Add(New SqlClient.SqlParameter("@SODate", SODateParam))
            Param.Add(New SqlClient.SqlParameter("@TotalVH", AmountVHParam))
            Param.Add(New SqlClient.SqlParameter("@TotalPP", AmountPPParam))
            Param.Add(New SqlClient.SqlParameter("@TotalIT", AmountITParam))
            Param.Add(New SqlClient.SqlParameter("@LastUpdateBy", LastUpdateByParam))

            Return m_SalesOrderMapper.ExecuteSP(spName, New ArrayList(Param))
        End Function
#End Region

    End Class

End Namespace