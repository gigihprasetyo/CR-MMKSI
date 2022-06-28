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
'// Generated on 10/5/2005 - 3:23:28 PM
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

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.OnlinePayment

    Public Class PaymentRegDocFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PaymentRegDocMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PaymentRegDocMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.PaymentRegDoc).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PaymentRegDoc))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PaymentObligation))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PaymentRegDoc
            Return CType(m_PaymentRegDocMapper.Retrieve(ID), PaymentRegDoc)
        End Function

        Public Function Retrieve(ByVal Code As String) As PaymentRegDoc
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentRegDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PaymentRegDoc), "Code", MatchType.Exact, Code))

            Dim PaymentRegDocColl As ArrayList = m_PaymentRegDocMapper.RetrieveByCriteria(criterias)
            If (PaymentRegDocColl.Count > 0) Then
                Return CType(PaymentRegDocColl(0), PaymentRegDoc)
            End If
            Return New PaymentRegDoc
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PaymentRegDocMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PaymentRegDocMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PaymentRegDocMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PaymentRegDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PaymentRegDocMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PaymentRegDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PaymentRegDocMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentRegDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PaymentRegDoc As ArrayList = m_PaymentRegDocMapper.RetrieveByCriteria(criterias)
            Return _PaymentRegDoc
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentRegDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PaymentRegDocColl As ArrayList = m_PaymentRegDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PaymentRegDocColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(PaymentRegDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim PaymentRegDocColl As ArrayList = m_PaymentRegDocMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PaymentRegDocColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PaymentRegDocColl As ArrayList = m_PaymentRegDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PaymentRegDocColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentRegDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PaymentRegDocColl As ArrayList = m_PaymentRegDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PaymentRegDoc), columnName, matchOperator, columnValue))
            Return PaymentRegDocColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PaymentRegDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentRegDoc), columnName, matchOperator, columnValue))

            Return m_PaymentRegDocMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub Update(ByVal objDomain As PaymentRegDoc)
            Try
                m_PaymentRegDocMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function UpdateOP(ByVal objDomain As PaymentRegDoc)
            Dim iResult As Integer = -1
            Try
                m_PaymentRegDocMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                iResult = objDomain.ID
            Catch ex As Exception
                iResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function
        Public Function UpdateList(ByVal arrToUpdate As ArrayList, ByVal oUserInfo As UserInfo, ByVal oDealer As Dealer) As Integer
            ' Note Hendra : Jika ada perubahan proses di regdoc, jgn lupa ubah juga method di PaymentObligationFacade yg berhubungan dengan regdoc.
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

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentRegDoc), "PaymentRegDocCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PaymentRegDoc), "PaymentRegDocCode", AggregateType.Count)
            Return CType(m_PaymentRegDocMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function Delete(ByVal objDomain As KTB.DNet.Domain.PaymentRegDoc) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As PaymentObligation In objDomain.PaymentObligations
                        item.PaymentRegDoc = objDomain
                        m_TransactionManager.AddDelete(item)
                    Next
                    m_TransactionManager.AddDelete(objDomain)

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


        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.PaymentRegDoc) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As PaymentObligation In objDomain.PaymentObligations
                        item.PaymentRegDoc = objDomain
                        m_TransactionManager.AddUpdate(item, "SAP")
                    Next
                    m_TransactionManager.AddUpdate(objDomain, "SAP")

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

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.PaymentRegDoc) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    For Each item As PaymentObligation In objDomain.PaymentObligations
                        item.PaymentRegDoc = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next


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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.PaymentRegDoc) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.PaymentRegDoc).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.PaymentRegDoc).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is PaymentObligation) Then

                CType(InsertArg.DomainObject, PaymentObligation).ID = InsertArg.ID

            End If

        End Sub

        Public Function RetrieveIDDealer(ByVal id As Integer) As PaymentRegDoc
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentRegDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PaymentRegDoc), "Dealer.ID", MatchType.Exact, id))

            Dim PaymentRegDocColl As ArrayList = m_PaymentRegDocMapper.RetrieveByCriteria(criterias)
            If (PaymentRegDocColl.Count > 0) Then
                Return CType(PaymentRegDocColl(0), PaymentRegDoc)
            End If
            Return New PaymentRegDoc
        End Function
#End Region

    End Class

End Namespace
