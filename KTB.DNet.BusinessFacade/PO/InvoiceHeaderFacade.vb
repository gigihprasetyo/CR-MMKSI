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
'// PURInvoiceSE       : Enter summary here after generation.
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
Imports KTB.DNet.NewSAPProxy
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.PO

    Public Class InvoiceHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_InvoiceHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_InvoiceHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(InvoiceHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.InvoiceHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.InvoiceDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As InvoiceHeader
            Return CType(m_InvoiceHeaderMapper.Retrieve(ID), InvoiceHeader)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_InvoiceHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_InvoiceHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal Code As String) As InvoiceHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(InvoiceHeader), "InvoiceNumber", MatchType.Exact, Code))

            Dim InvoiceHeaderColl As ArrayList = m_InvoiceHeaderMapper.RetrieveByCriteria(criterias)
            If (InvoiceHeaderColl.Count > 0) Then
                Return CType(InvoiceHeaderColl(0), InvoiceHeader)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(InvoiceHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_InvoiceHeaderMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_InvoiceHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal InvoiceHeaderID As Integer) As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "InvoiceHeaderID", _
                MatchType.Exact, InvoiceHeaderID))
            criterias.opAnd(New Criteria(GetType(InvoiceHeader), "RowStatus", MatchType.Exact, _
                CType(DBRowStatus.Active, Short)))
            Return m_InvoiceHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(InvoiceHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_InvoiceHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(InvoiceHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_InvoiceHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _InvoiceHeader As ArrayList = m_InvoiceHeaderMapper.RetrieveByCriteria(criterias)
            Return _InvoiceHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim InvoiceHeaderColl As ArrayList = m_InvoiceHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return InvoiceHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(InvoiceHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim InvoiceHeaderColl As ArrayList = m_InvoiceHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return InvoiceHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim InvoiceHeaderColl As ArrayList = m_InvoiceHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return InvoiceHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim InvoiceHeaderColl As ArrayList = m_InvoiceHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(InvoiceHeader), columnName, matchOperator, columnValue))
            Return InvoiceHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.domain.Search.SortCollection = New KTB.DNet.domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.domain.Search.Sort(GetType(InvoiceHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), columnName, matchOperator, columnValue))

            Return m_InvoiceHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "InvoiceHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(InvoiceHeader), "InvoiceHeaderCode", AggregateType.Count)
            Return CType(m_InvoiceHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.InvoiceHeader) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.InvoiceHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.InvoiceHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is InvoiceDetail) Then
                CType(InsertArg.DomainObject, InvoiceDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertInvoiceWithID(ByVal objDomain As KTB.DNet.Domain.InvoiceHeader) As Integer
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
                    For Each item As InvoiceDetail In objDomain.InvoiceDetails
                        item.InvoiceHeader = objDomain
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

            Dim objNewInvoice As InvoiceHeader = Me.Retrieve(objDomain.ID)
            Dim newID As Integer = objDomain.ID
            objNewInvoice.InvoiceNumber = objNewInvoice.InvoiceNumber.Substring(0, objNewInvoice.InvoiceNumber.Length - newID.ToString.Trim.Length) & newID
            Me.Update(objNewInvoice)

            Return returnValue
        End Function

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.InvoiceHeader) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Dim _criteria As Criteria
            Dim criterias As CriteriaComposite
            Dim arrChassisMaster As ArrayList
            Dim objChassisMaster = New ChassisMaster
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

                    For Each item As InvoiceDetail In objDomain.InvoiceDetails
                        item.InvoiceHeader = objDomain
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

        Public Function InsertFromSAP(ByVal objDomain As KTB.DNet.Domain.InvoiceHeader) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    'objDomain.Status = enumStatusInvoice.Status.Selesai
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If m_userPrincipal.Identity.Name = "" Then
                        _user = "SAP"
                    Else
                        _user = m_userPrincipal.Identity.Name
                    End If
                    For Each item As InvoiceDetail In objDomain.InvoiceDetails
                        item.InvoiceHeader = objDomain                       
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

                    For Each item As InvoiceHeader In objDomainColl
                        For Each item1 As InvoiceDetail In item.InvoiceDetails
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

        Public Function UpdateSingleDomainTransaction(ByVal objDomain As InvoiceHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As InvoiceDetail In objDomain.InvoiceDetails
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

        Public Sub Delete(ByVal objDomain As InvoiceHeader)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_InvoiceHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
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

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.InvoiceHeader) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If objDomain.InvoiceDetails.Count > 0 Then
                        For Each objInvoiceDetail As InvoiceDetail In objDomain.InvoiceDetails
                            objInvoiceDetail.InvoiceHeader = objDomain
                            If m_userPrincipal.Identity.Name = "" Then
                                _user = "SAP"
                            Else
                                _user = m_userPrincipal.Identity.Name
                            End If
                            m_TransactionManager.AddUpdate(objInvoiceDetail, _user)
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
                        For Each item As InvoiceHeader In objDomains
                            ' If item.TotalQuantity > 0 Then
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            '   End If

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

        Public Function RetrieveIDContract(ByVal id As Integer) As InvoiceHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(InvoiceHeader), "InvoiceHeader.ID", MatchType.Exact, id))

            Dim InvoiceHeaderColl As ArrayList = m_InvoiceHeaderMapper.RetrieveByCriteria(criterias)
            If (InvoiceHeaderColl.Count > 0) Then
                Return CType(InvoiceHeaderColl(0), InvoiceHeader)
            End If
            Return New InvoiceHeader
        End Function

        Public Sub SynchronizeToSAP(ByVal ArrlistInvoiceHeader As ArrayList)
            For Each objInvoiceHeader As InvoiceHeader In ArrlistInvoiceHeader
                Dim objInvoiceHeader1 As InvoiceHeader = Me.Retrieve(objInvoiceHeader.InvoiceNumber.ToString)
                If Not objInvoiceHeader1 Is Nothing Then
                    objInvoiceHeader1.POHeader.ID = objInvoiceHeader.POHeader.ID
                    Dim int As Integer = Me.Update(objInvoiceHeader1)
                Else
                    Dim Valid As Boolean = True
                    'For Each objInvoiceDetail As InvoiceDetail In objInvoiceHeader.InvoiceDetails
                    '    If objInvoiceDetail.AllocQty > objInvoiceDetail.ContractDetail.SisaUnit Then
                    '        'TODO : Masukin error ke file Log
                    '        Valid = False
                    '        Exit For
                    '    End If
                    'Next
                    If Valid Then
                        'objInvoiceHeader.Status = enumStatus.Status.Selesai
                        Dim int As Integer = Me.Insert(objInvoiceHeader)
                    End If
                End If
            Next
        End Sub

        Private Sub UpdateInvoiceDetail(ByVal obj As InvoiceDetail)
            Dim _InvoiceDetailFacade As InvoiceDetailFacade = New InvoiceDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            _InvoiceDetailFacade.Update(obj)
        End Sub

        Private Function GetInvoiceDetailByInvoiceHeaderAndLineNumber(ByVal InvoiceHeaderID As Integer) As InvoiceDetail
            Dim _InvoiceDetailFacade As InvoiceDetailFacade = New InvoiceDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InvoiceDetail), "InvoiceHeader.ID", MatchType.Exact, InvoiceHeaderID))
            ' criterias.opAnd(New Criteria(GetType(InvoiceDetail), "LineItem", MatchType.Exact, lineItem))
            Dim listInvoiceDetail As ArrayList = _InvoiceDetailFacade.Retrieve(criterias)
            If listInvoiceDetail.Count > 0 Then
                Return CType(listInvoiceDetail(0), InvoiceDetail)
            Else
                Return New InvoiceDetail
            End If
        End Function

        Private Function GetDeleteInvoice(ByVal oldInvoice As InvoiceHeader, ByVal newInvoice As InvoiceHeader) As ArrayList
            Dim oldDetails As ArrayList = oldInvoice.InvoiceDetails
            Dim newDetails As ArrayList = newInvoice.InvoiceDetails
            Dim found As Boolean = False
            Dim deleteList As ArrayList = New ArrayList
            For Each oldItem As InvoiceDetail In oldDetails
                found = False
                For Each newItem As InvoiceDetail In newDetails
                    If oldItem.ID = newItem.ID Then
                        found = True
                    End If
                Next
                If Not found Then
                    deleteList.Add(oldItem)
                End If
            Next
            Return deleteList
        End Function

        Public Function SynchronizeToSAP(ByVal objInvoiceHeader As InvoiceHeader) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Dim Valid As Boolean = True
            Dim performTransaction As Boolean = True
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    'If Not IsSONumberExist(objInvoiceHeader.SONumber, objInvoiceHeader.InvoiceNumber) Then
                    Dim objInvoiceHeader1 As InvoiceHeader = Me.Retrieve(objInvoiceHeader.InvoiceNumber.ToString)
                    If Not objInvoiceHeader1 Is Nothing Then
                        objInvoiceHeader1.InvoiceNumber = objInvoiceHeader.InvoiceNumber
                        'objInvoiceHeader1.InvoiceHeader = objInvoiceHeader.InvoiceHeader
                        objInvoiceHeader1.InvoiceDate = objInvoiceHeader.InvoiceDate
                        objInvoiceHeader1.LastUpdateTime = Now
                        objInvoiceHeader1.LastUpdateBy = "WSM"
                        objInvoiceHeader1.Amount = objInvoiceHeader.Amount
                        objInvoiceHeader1.POHeader.ID = objInvoiceHeader.POHeader.ID
                        objInvoiceHeader1.Cancelled = objInvoiceHeader.Cancelled
                        objInvoiceHeader1.PaymentRef = objInvoiceHeader.PaymentRef
                        ' objInvoiceHeader1.InvoiceNumber = objInvoiceHeader.InvoiceNumber
                        Dim deleteList As ArrayList = GetDeleteInvoice(objInvoiceHeader1, objInvoiceHeader)
                        For Each delItem As InvoiceDetail In deleteList
                            m_TransactionManager.AddDelete(delItem)
                        Next
                        For Each item As InvoiceDetail In objInvoiceHeader.InvoiceDetails
                            Dim InvoiceDetail As InvoiceDetail = GetInvoiceDetailByInvoiceHeaderAndLineNumber(objInvoiceHeader1.ID)
                            ' If InvoiceDetail.AllocQty < item.AllocQty Then
                            ' If InvoiceDetail.AllocQty > InvoiceDetail.ContractDetail.SisaUnit Then
                            'Valid = False
                            'End If
                            'End If
                            If Valid Then
                                If InvoiceDetail.ID > 0 Then
                                    'InvoiceDetail.ReqQty = item.ReqQty
                                    ' InvoiceDetail.AllocQty = item.AllocQty
                                    m_TransactionManager.AddUpdate(InvoiceDetail, m_userPrincipal.Identity.Name)
                                End If
                            Else
                                Throw New Exception("Alocation Qty Greater then Sisa Unit.")
                            End If
                        Next
                        m_TransactionManager.AddUpdate(objInvoiceHeader1, m_userPrincipal.Identity.Name)

                    Else
                        ' For Each objInvoiceDetail As InvoiceDetail In objInvoiceHeader.InvoiceDetails
                        ' If objInvoiceDetail.AllocQty > objInvoiceDetail.ContractDetail.SisaUnit Then
                        ' Valid = False
                        '  Exit For
                        'End If
                        '   Next
                        If Valid Then
                            'objInvoiceHeader.Status = enumStatusInvoice.Status.Selesai
                            m_TransactionManager.AddInsert(objInvoiceHeader, m_userPrincipal.Identity.Name)
                            If m_userPrincipal.Identity.Name = "" Then
                                _user = "SAP"
                            Else
                                _user = m_userPrincipal.Identity.Name
                            End If
                            For Each item As InvoiceDetail In objInvoiceHeader.InvoiceDetails
                                item.InvoiceHeader = objInvoiceHeader
                                ' item.AllocQty = item.ReqQty
                                m_TransactionManager.AddInsert(item, _user)
                            Next
                        Else
                            Throw New Exception("Alocation Qty Greater then Sisa Unit.")
                        End If
                    End If
                    If performTransaction And Valid Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
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

        Private Function IsSONumberExist(ByVal soNumber As String, ByVal InvoiceNumber As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(InvoiceHeader), "SONumber", MatchType.Exact, soNumber.Trim))
            criterias.opAnd(New Criteria(GetType(InvoiceHeader), "InvoiceNumber", MatchType.Exact, InvoiceNumber.Trim))
            Dim aggregates As New Aggregate(GetType(InvoiceHeader), "ID", AggregateType.Count)
            If CType(m_InvoiceHeaderMapper.RetrieveScalar(aggregates, criterias), Integer) > 0 Then
                Return True
            End If
            Return False
        End Function

        Private Function GetAvalibleSPL(ByVal objInvoiceList As ArrayList, ByVal conStr As String) As ArrayList
            Dim result As ArrayList = New ArrayList
            Dim listSapCr As ArrayList = New ArrayList
            Dim oParamSAPCreditCeiling As SAPCreditCeiling
            If objInvoiceList.Count > 0 Then
                'For Each item As InvoiceHeader In objInvoiceList
                '    oParamSAPCreditCeiling = New SAPCreditCeiling
                '    'oParamSAPCreditCeiling.DealerCode = item.InvoiceHeader.Dealer.LegalStatus
                '    'oParamSAPCreditCeiling.PeriodMonth = item.InvoiceHeader.ContractPeriodMonth
                '    'oParamSAPCreditCeiling.PeriodYear = item.InvoiceHeader.ContractPeriodYear
                '    'oParamSAPCreditCeiling.SPLNumber = item.InvoiceHeader.SPLNumber
                '    If item.InvoiceHeader.SPLNumber.Trim <> "" Then
                '        listSapCr.Add(oParamSAPCreditCeiling)
                '    End If
                'Next
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

        Private Function GetDueDateUsingDailyPayment(ByVal objInvoice As InvoiceHeader) As Date
            Dim objDailyPaymentFacade As DailyPaymentFacade = New DailyPaymentFacade(Me.m_userPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "InvoiceHeader.ID", MatchType.Exact, objInvoice.ID))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurInvoicese.PaymentPurInvoiceseCode", MatchType.Partial, "VH"))
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

        'Public Function GetCreditExInvoicesure(ByVal objDealer As Dealer, ByVal objTOP As TermOfPayment, ByVal currentInvoice As InvoiceHeader, ByVal isRegular As Boolean, ByVal SPL As String, ByVal constr As String) As Double
        '    Dim result As Double = 0
        '    Dim dueDateCurrentInvoice As Date = currentInvoice.ReqAllocationDateTime.AddDays(currentInvoice.TermOfPayment.TermOfPaymentValue)
        '    Dim reqDeliveryDateCurrentInvoice As Date = currentInvoice.ReqAllocationDateTime
        '    Dim listSelectedInvoice As ArrayList
        '    Dim dueDate As Date
        '    Dim reqDeliverDate As Date
        '    Dim firstCondition As Boolean = False
        '    Dim secondCondition As Boolean = False
        '    Dim thirdCondition As Boolean = False
        '    Dim fourthCondition As Boolean = False
        '    dueDateCurrentInvoice = New Date(dueDateCurrentInvoice.Year, dueDateCurrentInvoice.Month, dueDateCurrentInvoice.Day, 0, 0, 0)
        '    reqDeliveryDateCurrentInvoice = New Date(reqDeliveryDateCurrentInvoice.Year, reqDeliveryDateCurrentInvoice.Month, reqDeliveryDateCurrentInvoice.Day, 0, 0, 0)
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(InvoiceHeader), "TermOfPayment.TermOfPaymentValue", MatchType.Greater, 0))
        '    criterias.opAnd(New Criteria(GetType(InvoiceHeader), "Status", MatchType.InSet, "(" & CType(enumStatusInvoice.Status.Baru, Integer) & "," & CType(enumStatusInvoice.Status.Rilis, Integer) & "," & CType(enumStatusInvoice.Status.Konfirmasi, Integer) & "," & CType(enumStatusInvoice.Status.Selesai, Integer) & "," & CType(enumStatusInvoice.Status.Setuju, Integer) & ")"))
        '    criterias.opAnd(New Criteria(GetType(InvoiceHeader), "InvoiceHeader.Dealer.LegalStatus", MatchType.Exact, objDealer.LegalStatus))

        '    Dim sapResultList As ArrayList
        '    If isRegular Then
        '        Dim splCriteriaList As ArrayList = New ArrayList
        '        listSelectedInvoice = Me.Retrieve(criterias)
        '        If listSelectedInvoice.Count > 0 Then
        '            For Each item As InvoiceHeader In listSelectedInvoice
        '                If item.SONumber.Trim <> "" Then
        '                    Dim paymentDueDate As Date = GetDueDateUsingDailyPayment(item)
        '                    If paymentDueDate > New Date(1901, 1, 1) Then
        '                        dueDate = paymentDueDate
        '                    Else
        '                        dueDate = item.ReqAllocationDateTime.AddDays(item.TermOfPayment.TermOfPaymentValue).AddDays(objDealer.DueDate)
        '                    End If
        '                Else
        '                    dueDate = item.ReqAllocationDateTime.AddDays(item.TermOfPayment.TermOfPaymentValue).AddDays(objDealer.DueDate)
        '                End If
        '                dueDate = New Date(dueDate.Year, dueDate.Month, dueDate.Day, 0, 0, 0)
        '                reqDeliverDate = item.ReqAllocationDateTime
        '                reqDeliverDate = New Date(reqDeliverDate.Year, reqDeliverDate.Month, reqDeliverDate.Day, 0, 0, 0)
        '                firstCondition = reqDeliverDate <= reqDeliveryDateCurrentInvoice And dueDate <= dueDateCurrentInvoice And dueDate >= reqDeliveryDateCurrentInvoice
        '                secondCondition = reqDeliverDate >= reqDeliveryDateCurrentInvoice And dueDate <= dueDateCurrentInvoice
        '                thirdCondition = reqDeliverDate >= reqDeliveryDateCurrentInvoice And dueDate >= dueDateCurrentInvoice And reqDeliverDate <= dueDateCurrentInvoice
        '                fourthCondition = reqDeliverDate <= reqDeliveryDateCurrentInvoice And dueDate >= dueDateCurrentInvoice

        '                If (firstCondition Or secondCondition Or thirdCondition Or fourthCondition) Then
        '                    splCriteriaList.Add(item)
        '                End If
        '            Next
        '            sapResultList = GetAvalibleSPL(splCriteriaList, constr)
        '            For Each finalItem As InvoiceHeader In splCriteriaList

        '                If Not IsSPLExistInSAPList(finalItem.InvoiceHeader.SPLNumber, sapResultList) Then
        '                    result += finalItem.TotalHargaExInvoicesure
        '                End If
        '            Next
        '        End If
        '    Else
        '        criterias.opAnd(New Criteria(GetType(InvoiceHeader), "InvoiceHeader.SPLNumber", MatchType.Exact, SPL))
        '        listSelectedInvoice = Me.Retrieve(criterias)
        '        If listSelectedInvoice.Count > 0 Then
        '            For Each item As InvoiceHeader In listSelectedInvoice
        '                If item.SONumber.Trim <> "" Then
        '                    Dim paymentDueDate As Date = GetDueDateUsingDailyPayment(item)
        '                    If paymentDueDate < New Date(1901, 1, 1) Then
        '                        dueDate = paymentDueDate
        '                    Else
        '                        dueDate = item.ReqAllocationDateTime.AddDays(item.TermOfPayment.TermOfPaymentValue).AddDays(objDealer.DueDate)
        '                    End If
        '                Else
        '                    dueDate = item.ReqAllocationDateTime.AddDays(item.TermOfPayment.TermOfPaymentValue).AddDays(objDealer.DueDate)
        '                End If
        '                dueDate = New Date(dueDate.Year, dueDate.Month, dueDate.Day, 0, 0, 0)
        '                reqDeliverDate = item.ReqAllocationDateTime
        '                reqDeliverDate = New Date(reqDeliverDate.Year, reqDeliverDate.Month, reqDeliverDate.Day, 0, 0, 0)
        '                firstCondition = reqDeliverDate <= reqDeliveryDateCurrentInvoice And dueDate <= dueDateCurrentInvoice And dueDate >= reqDeliveryDateCurrentInvoice
        '                secondCondition = reqDeliverDate >= reqDeliveryDateCurrentInvoice And dueDate <= dueDateCurrentInvoice
        '                thirdCondition = reqDeliverDate >= reqDeliveryDateCurrentInvoice And dueDate >= dueDateCurrentInvoice And reqDeliverDate <= dueDateCurrentInvoice
        '                fourthCondition = reqDeliverDate <= reqDeliveryDateCurrentInvoice And dueDate >= dueDateCurrentInvoice

        '                reqDeliverDate = item.ReqAllocationDateTime
        '                reqDeliverDate = New Date(reqDeliverDate.Year, reqDeliverDate.Month, reqDeliverDate.Day, 0, 0, 0)
        '                If (firstCondition Or secondCondition Or thirdCondition Or fourthCondition) Then
        '                    result += item.TotalHargaExInvoicesure
        '                End If
        '            Next
        '        End If
        '    End If
        '    Return result
        'End Function

        'Public Function GetPaymentRejection(ByVal objDealer As Dealer, ByVal objTOP As TermOfPayment) As Integer
        '    'TODO PENDING
        '    Return 0
        'End Function

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

        'Public Function CountACC(ByVal objContract As InvoiceHeader, ByVal objDealer As Dealer, ByVal objTOP As TermOfPayment, ByVal currentInvoice As InvoiceHeader, ByVal ConStr As String) As Long
        '    Dim result As Double = 0
        '    Dim oParamSAPCreditCeiling As SAPCreditCeiling = New SAPCreditCeiling
        '    oParamSAPCreditCeiling.DealerCode = objDealer.LegalStatus
        '    oParamSAPCreditCeiling.PeriodMonth = objContract.ContractPeriodMonth
        '    oParamSAPCreditCeiling.PeriodYear = objContract.ContractPeriodYear
        '    oParamSAPCreditCeiling.SPLNumber = objContract.SPLNumber
        '    Dim listSapCr As ArrayList = New ArrayList
        '    listSapCr.Add(oParamSAPCreditCeiling)
        '    Dim isRegular As Boolean = False
        '    Dim objSAPCreditCeiling As SAPCreditCeiling = GetCreditCeiling(listSapCr, ConStr)(0)
        '    If objSAPCreditCeiling.TemporaryType = "" Then
        '        isRegular = True
        '    End If
        '    Dim CreditCeilingAmount As Double = 0
        '    Dim totalCreditExInvoicesure As Double = 0
        '    Dim totalPaymentRejection As Double = 0
        '    If Not objSAPCreditCeiling Is Nothing Then
        '        CreditCeilingAmount = objSAPCreditCeiling.CeilingAmount - objSAPCreditCeiling.BlokedAmount
        '        totalCreditExInvoicesure = GetCreditExInvoicesure(objDealer, objTOP, currentInvoice, isRegular, objContract.SPLNumber.Trim, ConStr)
        '        totalPaymentRejection = GetPaymentRejection(objDealer, objTOP)
        '    End If
        '    result = CreditCeilingAmount - totalCreditExInvoicesure - totalPaymentRejection
        '    Return result
        'End Function

        Public Function GetCreditCeiling(ByVal ojbSAPCRList As ArrayList, ByVal constr As String) As ArrayList
            Dim oSAPDnet As SAPDNet = New SAPDNet(constr)
            Dim listSAPCRReturn As ArrayList = New ArrayList
            Dim oCR As SAPCreditCeiling
            Try
                Dim list As ArrayList = oSAPDnet.GetCreditControl(ojbSAPCRList)
                For Each item As ZFUST0042Table In list
                    For Each dt As ZFUST0042 In item
                        oCR = New SAPCreditCeiling
                        oCR = FillCreditCeilingFromSAP(dt)
                        listSAPCRReturn.Add(oCR)
                    Next
                Next
                Return listSAPCRReturn
            Catch ex As Exception
                Throw ex
            End Try
            Return New ArrayList
        End Function

        Public Function GetCreditOutstandingReInvoicertCeiling(ByVal pLegalStatus As String, ByVal pDueDate As DateTime, ByVal pTempKind As String, ByVal ConStr As String, ByRef TotalRow As Integer) As ArrayList
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

        Public Function GetCreditTempReInvoicertCeiling(ByVal pLegalStatus As String, ByVal pDueDate As DateTime, ByVal ConStr As String) As ArrayList
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

        Public Function GetCreditInvoicesotionReInvoicertCeiling(ByVal pLegalStatus As String, ByVal pDueDate As DateTime, ByVal ConStr As String, ByRef TotalRow As Integer) As ArrayList
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
#End Region

    End Class

End Namespace