 

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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 8/14/2007 - 2:31:36 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Web

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.OnlinePayment

    Public Class PaymentObligationFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PaymentObligationMapper As IMapper
        Private m_PaymentRegDocMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PaymentObligationMapper = MapperFactory.GetInstance.GetMapper(GetType(PaymentObligation).ToString)
            m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            'Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PaymentObligation))
            'Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PaymentObligationHistory))
            'Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PaymentRegDoc))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PaymentObligation
            Return CType(m_PaymentObligationMapper.Retrieve(ID), PaymentObligation)
        End Function

        Public Function Retrieve(ByVal _assignment As String) As PaymentObligation
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "Assignment", MatchType.Exact, _assignment))
            Dim PaymentObligationColl As ArrayList = m_PaymentObligationMapper.RetrieveByCriteria(criterias)
            If (PaymentObligationColl.Count > 0) Then
                Return CType(PaymentObligationColl(0), PaymentObligation)
            End If
            Return New PaymentObligation
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PaymentObligationMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PaymentObligationMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PaymentObligationMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PaymentObligation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_PaymentObligationMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PaymentObligation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_PaymentObligationMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PaymentObligation As ArrayList = m_PaymentObligationMapper.RetrieveByCriteria(criterias)
            Return _PaymentObligation
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PaymentObligationColl As ArrayList = m_PaymentObligationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PaymentObligationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PaymentObligationColl As ArrayList = m_PaymentObligationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PaymentObligationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim PaymentObligationColl As ArrayList = m_PaymentObligationMapper.RetrieveByCriteria(criterias)
            Return PaymentObligationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(PaymentObligation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim CompetitorBrandColl As ArrayList = m_PaymentObligationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CompetitorBrandColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PaymentObligationColl As ArrayList = m_PaymentObligationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PaymentObligation), columnName, matchOperator, columnValue))
            Return PaymentObligationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PaymentObligation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), columnName, matchOperator, columnValue))
            Return m_PaymentObligationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PaymentObligation) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PaymentObligation).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PaymentObligation).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is PaymentObligationHistory) Then
                CType(InsertArg.DomainObject, PaymentObligationHistory).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is PaymentRegDoc) Then
                CType(InsertArg.DomainObject, PaymentRegDoc).ID = InsertArg.ID
                CType(InsertArg.DomainObject, PaymentRegDoc).MarkLoaded()
            End If

        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "PaymentObligation Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PaymentObligation), "PaymentObligation Code", AggregateType.Count)
            Return CType(m_PaymentObligationMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As PaymentObligation) As Integer
            Dim iReturn As Integer = 1
            Try
                iReturn = m_PaymentObligationMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As PaymentObligation) As Integer
            Dim nResult As Integer = 1
            Try
                nResult = m_PaymentObligationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function InsertFromService(ByVal objDomain As PaymentObligation) As Integer
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    Dim oldPaymentObligation As PaymentObligation = Me.GetExistingPaymentObligation(objDomain)
                    Dim objHistory As PaymentObligationHistory = New PaymentObligationHistory
                    objHistory.Status = objDomain.Status
                    objHistory.ProcessBy = "WSM"
                    objHistory.ProcessDate = Now
                    If oldPaymentObligation.ID > 0 Then
                        oldPaymentObligation.Amount = objDomain.Amount
                        oldPaymentObligation.ConfirmedBy = objDomain.ConfirmedBy
                        oldPaymentObligation.ConfirmedTime = objDomain.ConfirmedTime
                        oldPaymentObligation.Description = objDomain.Description
                        oldPaymentObligation.DocDate = objDomain.DocDate
                        oldPaymentObligation.DueDate = objDomain.DueDate
                        oldPaymentObligation.IsTOP = objDomain.IsTOP
                        oldPaymentObligation.ValidateMD5Code = objDomain.ValidateMD5Code
                        oldPaymentObligation.PaidDate = objDomain.PaidDate
                        oldPaymentObligation.Sequence = objDomain.Sequence
                        oldPaymentObligation.Status = objDomain.Status
                        oldPaymentObligation.ValidateBy = objDomain.ValidateBy
                        oldPaymentObligation.ValidateTime = objDomain.ValidateTime
                        oldPaymentObligation.PaymentAssignmentType = objDomain.PaymentAssignmentType
                        oldPaymentObligation.TransactionDueDate = objDomain.TransactionDueDate
                        objHistory.PaymentObligation = oldPaymentObligation
                        m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                        m_TransactionManager.AddUpdate(oldPaymentObligation, m_userPrincipal.Identity.Name)
                    Else
                        objHistory.PaymentObligation = objDomain
                        m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                        m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)

                    End If
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = objDomain.ID
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

            Return returnVal

        End Function


        Public Function UpdateFromService(ByVal objDomain As PaymentObligation) As Integer
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    Dim oldPaymentObligation As PaymentObligation = Me.GetExistingPaymentObligation(objDomain)
                    Dim objHistory As PaymentObligationHistory = New PaymentObligationHistory
                    objHistory.Status = objDomain.Status
                    objHistory.ProcessBy = "WSM"
                    objHistory.ProcessDate = Now
                    If oldPaymentObligation.ID > 0 Then
                        m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                        m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                    End If
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = objDomain.ID
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
            Return returnVal
        End Function

        Public Function ValidatePaymentObligation(ByVal listDomain As ArrayList, ByVal pUserInfo As UserInfo, ByVal pDealer As Dealer) As Integer
            Dim iResult As Integer = -1
            Dim NewRegDoc As PaymentRegDoc
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    If CType(listDomain.Item(0), PaymentObligation).SourceDocument = EnumOnlinePayment.SourceDocument.MANUAL Then
                        NewRegDoc = New PaymentRegDoc
                        NewRegDoc.UserInfo = pUserInfo
                        NewRegDoc.Dealer = pDealer
                        NewRegDoc.IpAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
                        NewRegDoc.CreateTime = DateTime.Now
                        'NewRegDoc.BORNumber = "Unknown yet"
                        m_TransactionManager.AddInsert(NewRegDoc, m_userPrincipal.Identity.Name)
                    End If

                    For Each item As PaymentObligation In listDomain

                        Select Case CType(item.SourceDocument, EnumOnlinePayment.SourceDocument)
                            Case EnumOnlinePayment.SourceDocument.MANUAL
                                item.Status = EnumOnlinePayment.StatusOnlinePayment.Proses
                                item.ValidateIPAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
                                item.ValidateTime = DateTime.Now
                                item.ValidateBy = m_userPrincipal.Identity.Name
                                item.ConfirmedBy = m_userPrincipal.Identity.Name
                                item.ConfirmedTime = DateTime.Now
                                item.PaymentRegDoc = NewRegDoc

                            Case EnumOnlinePayment.SourceDocument.SAP
                                item.Status = EnumOnlinePayment.StatusOnlinePayment.Validasi
                                item.ValidateIPAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
                                item.ValidateTime = DateTime.Now
                                item.ValidateBy = m_userPrincipal.Identity.Name
                                item.ValidateMD5Code = item.Dealer.DealerCode & item.ValidateTime.ToString("ddMMyyyyHHmmss")

                        End Select

                        'insert history
                        Dim NewHistory As New PaymentObligationHistory
                        NewHistory.PaymentObligation = item
                        NewHistory.ProcessBy = m_userPrincipal.Identity.Name
                        NewHistory.ProcessDate = DateTime.Now
                        NewHistory.Status = item.Status

                        m_TransactionManager.AddInsert(NewHistory, m_userPrincipal.Identity.Name)
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        iResult = 1
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
            Return iResult
        End Function

        Private Function GetExistingPaymentObligation(ByVal objDomain As PaymentObligation) As PaymentObligation
            Dim objPO As PaymentObligation = New PaymentObligation
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "Assignment", MatchType.Exact, objDomain.Assignment))
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "PaymentObligationType.ID", MatchType.Exact, objDomain.PaymentObligationType.ID))
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.ID", MatchType.Exact, objDomain.Dealer.ID))
            Dim list As ArrayList = Me.Retrieve(criterias)
            If list.Count > 0 Then
                objPO = list(0)
            End If
            Return objPO
        End Function

        Public Function Update(ByVal arrToUpdate As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrToUpdate.Count > 0 Then
                        For Each objPaymentObligation As PaymentObligation In arrToUpdate
                            m_TransactionManager.AddUpdate(objPaymentObligation, m_userPrincipal.Identity.Name)
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

        Public Function UpdateList(ByVal arrToUpdate As ArrayList, ByVal oUserInfo As UserInfo, ByVal oDealer As Dealer) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrToUpdate.Count > 0 Then
                        Dim objRegDoc As PaymentRegDoc = New PaymentRegDoc
                        objRegDoc.UserInfo = oUserInfo
                        objRegDoc.Dealer = oDealer
                        objRegDoc.IpAddress = HttpContext.Current.Request.Params("REMOTE_ADDR")
                        m_TransactionManager.AddInsert(objRegDoc, m_userPrincipal.Identity.Name)
                        For Each objPaymentObligation As PaymentObligation In arrToUpdate
                            objPaymentObligation.PaymentRegDoc = objRegDoc
                            m_TransactionManager.AddUpdate(objPaymentObligation, m_userPrincipal.Identity.Name)
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

#Region "Custom Method"

        Public Function Retrieve(ByVal Code As String, ByVal merk As String) As PaymentObligation
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "Code", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "CompetitorBrand.Code", MatchType.Exact, merk))
            Dim PaymentObligationColl As ArrayList = m_PaymentObligationMapper.RetrieveByCriteria(criterias)
            If (PaymentObligationColl.Count > 0) Then
                Return CType(PaymentObligationColl(0), PaymentObligation)
            End If
            Return New PaymentObligation
        End Function



#End Region

    End Class

End Namespace


