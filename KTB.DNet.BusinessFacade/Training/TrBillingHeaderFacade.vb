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
'// Generated on 11/14/2005 - 10:42:45 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Collections.Generic
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Linq
#End Region

Namespace KTB.DNet.BusinessFacade.Training
    Public Class TrBillingHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrBillingHeaderMapper As IMapper
        Private m_TrBillingDetailMapper As IMapper
        Private m_DepositBDebitNoteMapper As IMapper
        Private m_DepositBPencairanHeaderMapper As IMapper
        Private m_DepositBPencairanDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager
        Private ID_Insert As Integer

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_TrBillingHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(TrBillingHeader).ToString)
            Me.m_TrBillingDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(TrBillingDetail).ToString)
            Me.m_DepositBDebitNoteMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBDebitNote).ToString)
            Me.m_DepositBPencairanHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBPencairanHeader).ToString)
            Me.m_DepositBPencairanDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBPencairanDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.TrBillingDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrBillingHeader
            Return CType(m_TrBillingHeaderMapper.Retrieve(ID), TrBillingHeader)
        End Function

        Public Function Retrieve(ByVal RequestId As String) As TrBillingHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBillingHeader), "DebitNoteNumber", MatchType.Exact, RequestId))

            Dim TrBillingHeaderColl As ArrayList = m_TrBillingHeaderMapper.RetrieveByCriteria(criterias)
            If (TrBillingHeaderColl.Count > 0) Then
                Return CType(TrBillingHeaderColl(0), TrBillingHeader)
            End If
            Return New TrBillingHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrBillingHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrBillingHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrBillingHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrBillingHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrBillingHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrBillingHeader As ArrayList = m_TrBillingHeaderMapper.RetrieveByCriteria(criterias)
            Return _TrBillingHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrBillingHeaderColl As ArrayList = m_TrBillingHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrBillingHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrClassColl As ArrayList = m_TrBillingHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return TrClassColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim TrBillingHeaderColl As ArrayList = m_TrBillingHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrBillingHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim TrBillingHeaderColl As ArrayList = m_TrBillingHeaderMapper.RetrieveByCriteria(criterias, sortColl)
            Return TrBillingHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrBillingHeaderColl As ArrayList = m_TrBillingHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrBillingHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrBillingHeaderColl As ArrayList = m_TrBillingHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrBillingHeader), columnName, matchOperator, columnValue))
            Return TrBillingHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrBillingHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingHeader), columnName, matchOperator, columnValue))

            Return m_TrBillingHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByClassCode(ByVal ClassCode As String) As ArrayList
            Dim ClassAllocationColl As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBillingHeader), "TrClass.ClassCode", MatchType.Exact, ClassCode))
            ClassAllocationColl = m_TrBillingHeaderMapper.RetrieveByCriteria(criterias)
            If ClassAllocationColl.Count > 0 Then
                Return ClassAllocationColl
            End If
            Return New ArrayList
        End Function

        Public Function RetrieveScalar(ByVal Criterias As ICriteria, ByVal aggregate As Aggregate) As Integer
            Dim obj As Object = m_TrBillingHeaderMapper.RetrieveScalar(aggregate, Criterias)
            If obj Is DBNull.Value Then
                Return 0
            Else
                Return CInt(obj)
            End If
        End Function


#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DepositBDebitNote) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DepositBDebitNote).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DepositBDebitNote).MarkLoaded()
                ID_Insert = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is DepositBPencairanHeader) Then
                CType(InsertArg.DomainObject, DepositBPencairanHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DepositBPencairanHeader).MarkLoaded()
                ID_Insert = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is DepositBPencairanDetail) Then
                CType(InsertArg.DomainObject, DepositBPencairanDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function ValidateCode(ByVal classCode As String, ByVal dealerCode As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingHeader), "TrClass.ClassCode", MatchType.Exact, classCode))
            crit.opAnd(New Criteria(GetType(TrBillingHeader), "Dealer.DealerCode", MatchType.Exact, dealerCode))

            Dim agg As Aggregate = New Aggregate(GetType(TrBillingHeader), "ID", AggregateType.Count)

            Return CType(m_TrBillingHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TrBillingHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TrBillingHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Sub Update(ByVal datas As IEnumerable(Of TrBillingHeader))
            For Each itemdata As TrBillingHeader In datas
                Update(itemdata)
            Next
        End Sub

        Public Sub Update(ByVal datas As IEnumerable(Of TrBillingHeader), _
                          ByVal dataDN As IEnumerable(Of DepositBDebitNote), _
                          ByVal dataDH As IEnumerable(Of DepositBPencairanHeader), _
                          ByVal dataDD As IEnumerable(Of DepositBPencairanDetail))

            'Update(itemdata)
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    For Each itemdata As TrBillingHeader In datas
                        m_TransactionManager.AddUpdate(itemdata, m_userPrincipal.Identity.Name)
                        If itemdata.PaymentType = CType(EnumTagihanTraining.TipePembayaran.Deposit_B, Short) Then
                            m_TransactionManager.AddInsert(dataDN.FirstOrDefault(Function(x) x.BillingID = itemdata.ID), m_userPrincipal.Identity.Name)
                            m_TransactionManager.AddInsert(dataDH.FirstOrDefault(Function(x) x.BillingID = itemdata.ID), m_userPrincipal.Identity.Name)
                            m_TransactionManager.AddInsert(dataDD.FirstOrDefault(Function(x) x.BillingID = itemdata.ID), m_userPrincipal.Identity.Name)
                        End If
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
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

        End Sub

        Public Function Update(ByVal objDomain As TrBillingHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrBillingHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As TrBillingHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrBillingHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Function GetAggregateResult(ByVal aggregate As IAggregate, ByVal criteria As ICriteria) As Decimal
            Dim result As Object = m_TrBillingHeaderMapper.RetrieveScalar(aggregate, criteria)
            If result Is System.DBNull.Value Then
                Return 0
            Else
                Return CType(result, Decimal)
            End If
        End Function

#End Region

#Region "Custom Method"
        Public Function CreateRequestId(ByVal dealerID As Integer) As String
            Dim arrParam As ArrayList = New ArrayList()
            Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@DealerID", dealerID)
            arrParam.Add(param1)

            Dim dataTable As DataTable = m_TrBillingHeaderMapper.RetrieveDataSet("Get_RequestIdPayment", arrParam).Tables(0)

            Return dataTable.Rows(0)(0).ToString()
        End Function

        Public Function RetrievePaymentDetail(ByVal TrBillingHeaderID As String) As ArrayList
            Dim ArrPaymentDetail As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBillingDetail), "TrBillingHeader.ID", MatchType.Exact, TrBillingHeaderID))
            ArrPaymentDetail = m_TrBillingDetailMapper.RetrieveByCriteria(criterias)
            If ArrPaymentDetail.Count > 0 Then
                Return ArrPaymentDetail
            End If
            Return New ArrayList
        End Function

        Public Function UpdateFinishBill(ByVal debitNote As DepositBDebitNote, ByVal jvNumber As String) As Boolean
            Dim nResult As Integer = -1
            Try
                Dim obj As TrBillingHeader = Retrieve(debitNote.DNNumber)
                If obj.ID <> 0 Then
                    obj.Status = CType(EnumTagihanTraining.TagihanStatus.Selesai, Short)
                    obj.JVNumber = jvNumber
                    nResult = m_TrBillingHeaderMapper.Update(obj, m_userPrincipal.Identity.Name)
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return False
        End Function

        Public Function UpdateBillChangesTransfer(ByVal debitNote As DepositBDebitNote) As Boolean
            Dim nResult As Integer = -1
            Try
                Dim obj As TrBillingHeader = Retrieve(debitNote.DNNumber)
                If obj.ID <> 0 Then
                    obj.PaymentType = CType(EnumTagihanTraining.TipePembayaran.Transfer, Short)
                    obj.Status = CType(EnumTagihanTraining.TagihanStatus.Pembayaran_Transfer, Short)
                    nResult = m_TrBillingHeaderMapper.Update(obj, m_userPrincipal.Identity.Name)
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return False
        End Function
#End Region

    End Class

End Namespace



