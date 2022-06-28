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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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
Imports KTB.DNet.BusinessFacade.General


#End Region

Namespace KTB.DNet.BusinessFacade.Transfer
    Public Class TransferCeilingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_TransferCeilingMapper As IMapper
        Private m_TransferCeilingDetailMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

        Private m_v_CreditAccoutMapper As IMapper

        Private m_TransactionManager As TransactionManager


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_TransferCeilingMapper = MapperFactory.GetInstance().GetMapper(GetType(TransferCeiling).ToString)
            Me.m_TransferCeilingDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(TransferCeilingDetail).ToString)
            Me.m_v_CreditAccoutMapper = MapperFactory.GetInstance().GetMapper(GetType(v_CreditAccount).ToString())

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.TransferCeiling))

        End Sub

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.TransferCeiling) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.TransferCeiling).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.TransferCeiling).MarkLoaded()
            End If
        End Sub

#End Region

#Region "Retrieve"


        Public Function Retrieve(ByVal ID As Integer) As TransferCeiling
            Return CType(m_TransferCeilingMapper.Retrieve(ID), TransferCeiling)
        End Function

        Public Function Retrieve(ByVal TransferCeilingCode As String) As TransferCeiling
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TransferCeiling), "TransferCeilingCode", MatchType.Exact, TransferCeilingCode))

            Dim TransferCeilingColl As ArrayList = m_TransferCeilingMapper.RetrieveByCriteria(criterias)
            If (TransferCeilingColl.Count > 0) Then
                Return CType(TransferCeilingColl(0), TransferCeiling)
            End If
            Return New TransferCeiling
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TransferCeilingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TransferCeilingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TransferCeilingMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferCeilingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferCeilingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("TransferCeilingCode")) Then
                sortColl.Add(New Sort(GetType(TransferCeiling), "TransferCeilingCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _TransferCeiling As ArrayList = m_TransferCeilingMapper.RetrieveByCriteria(criterias, sortColl)
            Return _TransferCeiling
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TransferCeilingColl As ArrayList = m_TransferCeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferCeilingColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TransferCeilingColl As ArrayList = m_TransferCeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferCeilingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TransferCeiling), columnName, matchOperator, columnValue))
            Dim TransferCeilingColl As ArrayList = m_TransferCeilingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferCeilingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeiling), columnName, matchOperator, columnValue))

            Return m_TransferCeilingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferCeilingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As TransferCeiling) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TransferCeilingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Insert(ByVal aTCs As ArrayList) As Integer
            Try
                For Each oTC As TransferCeiling In aTCs
                    m_TransferCeilingMapper.Insert(oTC, "ws")
                Next
                Return 0
            Catch ex As Exception
                Return -1
            End Try

            Return -1

            If Me.IsTaskFree() Then
                Try

                    For Each oTC As TransferCeiling In aTCs
                        m_TransferCeilingMapper.Insert(oTC, "ws")
                    Next

                    Me.SetTaskLocking()
                    Dim IsPerformTrans As Boolean = True
                    For Each oTC As TransferCeiling In aTCs
                        Me.m_TransactionManager.AddInsert(oTC, "ws")
                    Next
                    If IsPerformTrans Then
                        Me.m_TransactionManager.PerformTransaction()
                        Return 0
                    End If
                Catch ex As Exception
                    Return -1
                End Try
            End If
            Return -1
        End Function

        Public Function Update(ByVal objDomain As TransferCeiling) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TransferCeilingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TransferCeiling)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TransferCeilingMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TransferCeiling)
            Try
                m_TransferCeilingMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransferCeiling), "TransferCeilingCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TransferCeiling), "TransferCeilingCode", AggregateType.Count)

            Return CType(m_TransferCeilingMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TransferCeilingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransferCeiling), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TransferCeilingColl As ArrayList = m_TransferCeilingMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TransferCeilingColl
        End Function

        Public Function RetrieveCeilingStatus(ProductCategoryID As Integer, CreditAccount As String _
                                              , PaymentType As Short, StartDate As DateTime, EndDate As DateTime _
                                              , Optional IsReport As Boolean = False _
                                              , Optional IsReportDetail As Boolean = False) As DataSet
            'Prevent SQL Injection in string CreditAccount 
            Dim cD As New CriteriaComposite(New Criteria(GetType(v_CreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aDs As ArrayList

            cD.opAnd(New Criteria(GetType(v_CreditAccount), "CreditAccount", MatchType.Exact, CreditAccount))

            aDs = m_v_CreditAccoutMapper.RetrieveByCriteria(cD)
            Dim Sql As String

            Sql = "exec sp_TransferCeiling " & ProductCategoryID.ToString() & ", '" & CreditAccount & "'," & PaymentType & ",'" & StartDate.ToString("yyyy.MM.dd") & "','" & EndDate.ToString("yyyy.MM.dd") & "'," & IIf(IsReport, "1", "0") & ", " & IIf(IsReportDetail, "1", "0")
            Return Me.m_TransferCeilingMapper.RetrieveDataSet(Sql)

        End Function

        Public Function RetrieveOutstandingPayment(ProductCategoryID As Integer, CreditAccount As String _
                                              , PaymentType As Short, StartDate As DateTime, EndDate As DateTime _
                                              , Optional IsReport As Boolean = False _
                                              , Optional IsReportDetail As Boolean = False, Optional ByVal PaymentPurposeID As Integer = 0) As DataSet
            'Prevent SQL Injection in string CreditAccount 
            Dim cD As New CriteriaComposite(New Criteria(GetType(v_CreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aDs As ArrayList

            cD.opAnd(New Criteria(GetType(v_CreditAccount), "CreditAccount", MatchType.Exact, CreditAccount))

            aDs = m_v_CreditAccoutMapper.RetrieveByCriteria(cD)
            Dim Sql As String

            Sql = "exec sp_TransferOutstandingPayment " & ProductCategoryID.ToString() & ", '" & CreditAccount & "'," & PaymentType & ",'" & StartDate.ToString("yyyy.MM.dd") & "','" & EndDate.ToString("yyyy.MM.dd") & "'," & IIf(IsReport, "1", "0") & ", " & IIf(IsReportDetail, "1", "0")

            If PaymentPurposeID > 0 Then
                Sql = Sql & ", " & PaymentPurposeID.ToString()
            End If
            Return Me.m_TransferCeilingMapper.RetrieveDataSet(Sql)

        End Function



        Public Function RetrieveCeilingDetail(ByVal TransferCeilingID As Integer) As DataSet
            Dim Sql As String

            Sql = "exec sp_TransferCeilingDetail " & TransferCeilingID.ToString()
            Return Me.m_TransferCeilingMapper.RetrieveDataSet(Sql)

        End Function


        Public Function IsEnoughCeiling(ByRef oPOH As POHeader, TotalPO As Decimal, ByRef oTC As TransferCeiling, ByRef CurrentAvCeiling As Decimal, ByRef sMsg As String) As Boolean
            Dim startDate As Date, endDate As Date
            Dim ds As DataSet
            Dim TotalPOInDB As Decimal = 0
            Dim IsEnough As Boolean = False


            CurrentAvCeiling = 0
            sMsg = ""

            startDate = DateAdd(DateInterval.Day, 1 - oPOH.ReqAllocationDateTime.Day, oPOH.ReqAllocationDateTime)
            endDate = oPOH.ReqAllocationDateTime

            Try

                ds = Me.RetrieveCeilingStatus(oPOH.ContractHeader.Category.ProductCategory.ID _
                                              , oPOH.Dealer.CreditAccount, oPOH.TermOfPayment.PaymentType _
                                              , startDate, endDate, False, False)
                If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
                    Dim dt As DataTable = ds.Tables(0)

                    If (dt.Rows.Count > 0) Then
                        Dim dr As DataRow = dt.Rows(6)

                        CurrentAvCeiling = dr.Item("D" & endDate.Day.ToString())
                    End If

                    Dim TotInDB As Decimal = 0
                    If oPOH.ID > 0 Then TotInDB = oPOH.TotalPODetail()
                    'CurrentAvCeiling = CurrentAvCeiling + TotInDB - TotalPO
                    CurrentAvCeiling = CurrentAvCeiling
                    IsEnough = ((CurrentAvCeiling + TotInDB - TotalPO) >= 0)

                End If

            Catch ex As Exception
                CurrentAvCeiling = 0
                IsEnough = False
            End Try
            If Not IsEnough Then sMsg = "Ceiling Tidak Cukup. Sisa Ceiling adalah " & FormatNumber(CurrentAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            Return IsEnough
        End Function

        Public Function IsEnoughCeiling(ByRef oPOH As PODraftHeader, TotalPO As Decimal, ByRef oTC As TransferCeiling, ByRef CurrentAvCeiling As Decimal, ByRef sMsg As String) As Boolean
            Dim startDate As Date, endDate As Date
            Dim ds As DataSet
            Dim TotalPOInDB As Decimal = 0
            Dim IsEnough As Boolean = False


            CurrentAvCeiling = 0
            sMsg = ""

            startDate = DateAdd(DateInterval.Day, 1 - oPOH.ReqAllocationDateTime.Day, oPOH.ReqAllocationDateTime)
            endDate = oPOH.ReqAllocationDateTime

            Try

                ds = Me.RetrieveCeilingStatus(oPOH.ContractHeader.Category.ProductCategory.ID _
                                              , oPOH.Dealer.CreditAccount, oPOH.TermOfPayment.PaymentType _
                                              , startDate, endDate, False, False)
                If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
                    Dim dt As DataTable = ds.Tables(0)

                    If (dt.Rows.Count > 0) Then
                        Dim dr As DataRow = dt.Rows(6)

                        CurrentAvCeiling = dr.Item("D" & endDate.Day.ToString())
                    End If

                    Dim TotInDB As Decimal = 0
                    If oPOH.ID > 0 Then TotInDB = oPOH.TotalPODetail()
                    'CurrentAvCeiling = CurrentAvCeiling + TotInDB - TotalPO
                    CurrentAvCeiling = CurrentAvCeiling
                    IsEnough = ((CurrentAvCeiling + TotInDB - TotalPO) >= 0)

                End If

            Catch ex As Exception
                CurrentAvCeiling = 0
                IsEnough = False
            End Try
            If Not IsEnough Then sMsg = "Ceiling Tidak Cukup. Sisa Ceiling adalah " & FormatNumber(CurrentAvCeiling, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            Return IsEnough
        End Function

        Public Function UpdateDetail(ByVal _TC As TransferCeiling) As Integer
            Dim iRes As Integer = -1
            Dim TCDs As ArrayList = _TC.TransferCeilingDetails
            Dim TCDb As TransferCeiling
            Dim ID As Integer

            Try
                ID = _TC.ID
                iRes = Me.Update(_TC)
                If (iRes > 0) Then
                    _TC = Me.Retrieve(ID)
                    For Each oTCD As TransferCeilingDetail In TCDs
                        Dim cTCD As CriteriaComposite
                        Dim oTCDDb As TransferCeilingDetail
                        Dim TCDsDb As ArrayList

                        cTCD = New CriteriaComposite(New Criteria(GetType(TransferCeilingDetail), "RowStatus", MatchType.No, CType(DBRowStatus.Canceled, Short)))
                        If Not IsNothing(oTCD.SalesOrder) AndAlso oTCD.SalesOrder.ID > 0 Then
                            cTCD.opAnd(New Criteria(GetType(TransferCeilingDetail), "SalesOrder.ID", MatchType.Exact, oTCD.SalesOrder.ID))
                        ElseIf Not IsNothing(oTCD.TransferPayment) AndAlso oTCD.TransferPayment.ID > 0 Then
                            cTCD.opAnd(New Criteria(GetType(TransferCeilingDetail), "TransferPayment.ID", MatchType.Exact, oTCD.TransferPayment.ID))
                        End If

                        TCDsDb = m_TransferCeilingDetailMapper.RetrieveByCriteria(cTCD)
                        Dim _Amount As Decimal = oTCD.Amount
                        Dim StrSQLDetail As String = " exec TransferCeilingDetail_BalanceUpdate @ProductCategoryID={0}, @CreditAccount='{1}', @PaymentType={2}, @EffectiveDateHeader='{3}',@Amount={4}"

                        StrSQLDetail = String.Format(StrSQLDetail, _TC.ProductCategory.ID, _TC.CreditAccount, _TC.PaymentType, _TC.EffectiveDate.ToString("yyyy/MM/dd"), FormatNumber(_Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True).Replace(",", "").Replace(".", ""))

                        If TCDsDb.Count > 0 Then
                            oTCDDb = TCDsDb(0)

                            oTCDDb.TransferCeiling = _TC
                            oTCDDb.Amount = oTCD.Amount
                            oTCDDb.RowStatus = CInt(DBRowStatus.Active)
                            m_TransferCeilingDetailMapper.Update(oTCDDb, "ws")

                        Else
                            oTCD.TransferCeiling = _TC
                            m_TransferCeilingDetailMapper.Insert(oTCD, "ws")

                        End If
                        If oTCD.IsIncome = 1 Then
                            StrSQLDetail = StrSQLDetail & ", @IsIncome={0},@TransDateDetail='{1}', @TransferPaymentID={2} "
                            StrSQLDetail = String.Format(StrSQLDetail, oTCD.IsIncome, _TC.EffectiveDate.ToString("yyyy/MM/dd"), oTCD.TransferPayment.ID)
                        Else
                            StrSQLDetail = StrSQLDetail & ", @IsIncome={0},@TransDateDetail='{1}', @SalesOrderID={2} "
                            StrSQLDetail = String.Format(StrSQLDetail, oTCD.IsIncome, _TC.EffectiveDate.ToString("yyyy/MM/dd"), oTCD.SalesOrder.ID)
                        End If


                        m_TransferCeilingDetailMapper.ExecuteSP(StrSQLDetail)

                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return iRes
        End Function

        Public Function ExecuteSP(ByVal SQL As String) As Boolean
            'If SQL.IndexOf("exec ") < 0 Then
            '    SQL = SQL.Replace("'", "''")
            '    SQL = "exec SPHelper '" & SQL & "'"
            'End If
            Return Me.m_TransferCeilingMapper.ExecuteSP(SQL)
        End Function
#End Region

    End Class

End Namespace
