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

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.PO

    Public Class DailyPaymentFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DailyPaymentMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            
            Me.m_userPrincipal = userPrincipal
            Me.m_DailyPaymentMapper = MapperFactory.GetInstance.GetMapper(GetType(DailyPayment).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DailyPayment
            Return CType(m_DailyPaymentMapper.Retrieve(ID), DailyPayment)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DailyPaymentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DailyPaymentMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveFromSP(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Dim SQL As String
            Dim SQLOpt As String = ""
            Dim SQLWhere As String
            Dim SQLOrder As String

            If (Not IsNothing(criterias)) Then
                SQLOpt = criterias.ToString
                If (SQLOpt.EndsWith(" WHERE ")) Then
                    SQLOpt = SQLOpt.Replace(" WHERE ", "")
                End If
            End If
            SQLWhere = SQLOpt
            SQLWhere = SQLWhere.Replace("'", "''")
            If (Not IsNothing(sorts)) Then
                For Each obj As Object In sorts
                    Dim joinClauses As System.Collections.Specialized.StringCollection = CType(obj, Sort).GetJoinClause()
                    For Each joinClause As String In joinClauses
                        If (SQLOpt.IndexOf(joinClause) = -1) Then
                            SQLOpt = SQLOpt.Insert(SQLOpt.IndexOf(" WHERE "), joinClause)
                        End If
                    Next
                Next
                SQLWhere = SQLOpt
                SQLWhere = SQLWhere.Replace("'", "''")
                SQLOrder = " ORDER BY " + CType(sorts, Object).ToString()
                If (SQLOrder.EndsWith(" ORDER BY ")) Then
                    SQLOrder = SQLOrder.Replace(" ORDER BY ", "")
                End If
            End If

            SQL = "exec sp_DailyPayment '" & SQLWhere & "', '" & SQLOrder & "'"

            Return m_DailyPaymentMapper.RetrieveSP(SQL)
        End Function

        Public Function Retrieve(ByVal DocNumber As String) As DailyPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "ReceiptNumber", MatchType.Exact, DocNumber))

            Dim DailyPaymentColl As ArrayList = m_DailyPaymentMapper.RetrieveByCriteria(criterias)
            If (DailyPaymentColl.Count > 0) Then
                Return CType(DailyPaymentColl(0), DailyPayment)
            End If
            Return Nothing
        End Function

        Public Function RetrieveByDocNumber(ByVal DocNumber As String) As DailyPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "DocNumber", MatchType.Exact, DocNumber))

            Dim DailyPaymentColl As ArrayList = m_DailyPaymentMapper.RetrieveByCriteria(criterias)
            If (DailyPaymentColl.Count > 0) Then
                Return CType(DailyPaymentColl(0), DailyPayment)
            End If
            Return Nothing
        End Function


        Public Function RetrieveList() As ArrayList
            Return m_DailyPaymentMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DailyPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DailyPaymentMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DailyPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DailyPaymentMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DailyPayment As ArrayList = m_DailyPaymentMapper.RetrieveByCriteria(criterias)
            Return _DailyPayment
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DailyPaymentColl As ArrayList = m_DailyPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DailyPaymentColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DailyPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DailyPaymentColl As ArrayList = m_DailyPaymentMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DailyPaymentColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DailyPaymentColl As ArrayList = m_DailyPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DailyPaymentColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As SortCollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DailyPaymentColl As ArrayList = m_DailyPaymentMapper.RetrieveByCriteria(criterias, sorts, pageNumber, pageSize, totalRow)
            Return DailyPaymentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DailyPaymentColl As ArrayList = m_DailyPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DailyPayment), columnName, matchOperator, columnValue))
            Return DailyPaymentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.domain.Search.SortCollection = New KTB.DNet.domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.domain.Search.Sort(GetType(DailyPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), columnName, matchOperator, columnValue))

            Return m_DailyPaymentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveAggregate(ByVal crit As CriteriaComposite, ByVal agg As Aggregate) As Object
            Dim Total As Decimal = 0
            Try
                Total = CType(m_DailyPaymentMapper.RetrieveScalar(agg, crit), Object)
            Catch ex As Exception
                Total = 0
            End Try
            Return Total '  CType(m_DailyPaymentMapper.RetrieveScalar(agg, crit), Object)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColl As SortCollection) As ArrayList
            Dim DailyPaymentColl As ArrayList = m_DailyPaymentMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DailyPaymentColl
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "DailyPaymentCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DailyPayment), "DailyPaymentCode", AggregateType.Count)
            Return CType(m_DailyPaymentMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        'Public Function _insertDP(ByVal dpcoll As ArrayList)
        '    For Each item As DailyPayment In dpcoll
        '        insertDP(item)
        '    Next
        'End Function

        Private Function IsDailyPaymentExist(ByVal POID As Integer, ByVal docNumber As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "DocNumber", MatchType.Exact, docNumber))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, POID))
            Dim _dailyPOCollection As ArrayList = New DailyPaymentFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
            If _dailyPOCollection.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Function GetDailyPayment(ByVal POID As Integer, ByVal docNumber As String, Optional ByVal FiscalYear As Short = 0) As DailyPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "DocNumber", MatchType.Exact, docNumber))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, POID))
            'criterias.opAnd(New Criteria(GetType(DailyPayment), "FiscalYear", MatchType.Exact, FiscalYear))
            Dim _dailyPOCollection As ArrayList = New DailyPaymentFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            If _dailyPOCollection.Count > 0 Then
                For Each objDP As DailyPayment In _dailyPOCollection
                    If objDP.FiscalYear = FiscalYear Then
                        Return objDP
                    End If
                Next
                Return _dailyPOCollection.Item(0)
            Else
                Return New DailyPayment
            End If


            If _dailyPOCollection.Count > 0 Then
                Return _dailyPOCollection.Item(0)
            Else
                Return New DailyPayment
            End If
        End Function

        Private Function GetDailyPayment(ByVal POID As Integer, ByVal SlipNumber As String, ByVal PaymentPurposeID As Integer) As DailyPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, POID))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "SlipNumber", MatchType.Exact, SlipNumber))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.Exact, PaymentPurposeID))
            Dim _dailyPOCollection As ArrayList = New DailyPaymentFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            If _dailyPOCollection.Count > 0 Then
                Return _dailyPOCollection.Item(0)
            Else
                Return New DailyPayment
            End If
        End Function

        Private Function GetDPByRegNumber(ByVal RegNumber As String, ByVal POHID As Integer) As DailyPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "DailyPaymentHeader.RegNumber", MatchType.Exact, RegNumber))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, pohid))
            Dim _dailyPOCollection As ArrayList = New DailyPaymentFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            If _dailyPOCollection.Count > 0 Then
                Return _dailyPOCollection.Item(0)
            Else
                Return New DailyPayment
            End If
        End Function

        Private Function GetDailyPaymentUnique(ByVal POID As Integer, ByVal SlipNumber As String, ByVal PaymentPurposeID As Integer, ByVal DocNumber As String, ByVal FiscalYear As Integer) As DailyPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, POID))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "SlipNumber", MatchType.Exact, SlipNumber))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "DocNumber", MatchType.Exact, DocNumber))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "FiscalYear", MatchType.Exact, FiscalYear))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.Exact, PaymentPurposeID))
            Dim _dailyPOCollection As ArrayList = New DailyPaymentFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            If _dailyPOCollection.Count > 0 Then
                Return _dailyPOCollection.Item(0)
            Else
                Return New DailyPayment
            End If
        End Function

        Public Function insertDP(ByVal dp As DailyPayment)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    'Me.SetTaskLocking()
                    Dim performTransaction As Boolean = False 'True
                    Dim ObjMapper As IMapper
                    Dim value As Integer
                    Dim oSCHFac As StatusChangeHistoryFacade = New StatusChangeHistoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("SAP"), Nothing))

                    'prevent NULL value
                    dp.RemarkStatus = EnumPaymentRemarkStatus.GetEnumValue(EnumPaymentRemarkStatus.GetStringValue(dp.RemarkStatus))

                    'Update By heru dan pak ary
                    'Jika sonumber dan docnumber sama update, jika tidak ketemu insert

                    'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "ReceiptNumber", MatchType.Exact, dp.ReceiptNumber))
                    'Dim agg As Aggregate = New Aggregate(GetType(DailyPayment), "ReceiptNumber", AggregateType.Count)
                    'value = CType(m_DailyPaymentMapper.RetrieveScalar(agg, crit), Integer)
                    Dim _dpold As DailyPayment
                    Dim IsNewFromDNet As Boolean = False
                    If Not IsNothing(dp) AndAlso Not IsNothing(dp.POHeader) AndAlso dp.POHeader.IsFactoring = 1 Then
                        If Not IsNothing(dp.DailyPaymentHeader) AndAlso dp.DailyPaymentHeader.RegNumber.Trim <> String.Empty Then
                            _dpold = GetDPByRegNumber(dp.DailyPaymentHeader.RegNumber, dp.POHeader.ID)
                        Else
                            _dpold = GetDailyPaymentUnique(dp.POHeader.ID, dp.SlipNumber, dp.PaymentPurpose.ID, dp.DocNumber, dp.FiscalYear)
                        End If
                        If IsNothing(_dpold) Then _dpold = New DailyPayment
                    Else
                        If dp.DocNumber = "" AndAlso dp.FiscalYear = 0 Then 'Factoring : input DP from DNet application
                            _dpold = dp
                            IsNewFromDNet = True
                        Else 'From SAP
                            If IsNothing(dp.DailyPaymentHeader) OrElse dp.DailyPaymentHeader.ID < 1 Then
                                _dpold = New DailyPayment
                            Else
                                _dpold = GetDPByRegNumber(dp.DailyPaymentHeader.RegNumber, dp.POHeader.ID)
                            End If
                        End If
                    End If

                    If _dpold.ID > 0 Then
                        _dpold.IsChangedWSM = False

                        If _dpold.Amount <> dp.Amount Then
                            _dpold.Amount = dp.Amount
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.BaselineDate <> dp.BaselineDate Then
                            _dpold.BaselineDate = dp.BaselineDate
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.DocDate <> dp.DocDate Then
                            _dpold.DocDate = dp.DocDate
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.DocNumber <> dp.DocNumber Then
                            _dpold.DocNumber = dp.DocNumber
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.PaymentPurpose.ID <> dp.PaymentPurpose.ID Then
                            _dpold.PaymentPurpose = dp.PaymentPurpose
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.DocNumber <> dp.DocNumber Then
                            _dpold.DocNumber = dp.DocNumber
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.POHeader.ID <> dp.POHeader.ID Then
                            _dpold.POHeader = dp.POHeader
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.ReceiptNumber <> dp.ReceiptNumber Then
                            _dpold.ReceiptNumber = dp.ReceiptNumber
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.SAPCreator <> dp.SAPCreator Then
                            _dpold.SAPCreator = dp.SAPCreator
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.SlipNumber <> dp.SlipNumber Then
                            _dpold.SlipNumber = dp.SlipNumber
                            _dpold.IsChangedWSM = True
                        End If
                        _dpold.Bank = GetSlipNumberBank(_dpold.SlipNumber)
                        _dpold.BankID = _dpold.Bank.ID
                        _dpold.EntryType = dp.EntryType
                        '_dpold.GyroType = _dpold.GyroType ' let it be
                        _dpold.IsChangedWSM = True
                        If (Not IsNothing(_dpold.SalesOrder) AndAlso Not IsNothing(dp.SalesOrder)) Or _
                        (IsNothing(_dpold.SalesOrder) AndAlso Not IsNothing(dp.SalesOrder)) Then
                            If (IsNothing(_dpold.SalesOrder) AndAlso Not IsNothing(dp.SalesOrder)) OrElse _
                            (_dpold.SalesOrder.ID <> dp.SalesOrder.ID) Then
                                _dpold.SalesOrder = dp.SalesOrder
                                _dpold.IsChangedWSM = True
                            End If
                        Else
                            _dpold.IsChangedWSM = True
                        End If

                        If _dpold.EffectiveDate <> dp.EffectiveDate Then
                            _dpold.EffectiveDate = dp.EffectiveDate
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.IsReversed <> dp.IsReversed Then
                            _dpold.IsReversed = dp.IsReversed
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.IsCleared <> dp.IsCleared Then
                            _dpold.IsCleared = dp.IsCleared
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.Reason <> dp.Reason Then
                            _dpold.Reason = dp.Reason
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.EntryDate <> dp.EntryDate Then
                            _dpold.EntryDate = dp.EntryDate
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.PIC <> dp.PIC Then
                            _dpold.PIC = dp.PIC
                            _dpold.IsChangedWSM = True
                        End If
                        If _dpold.FiscalYear <> dp.FiscalYear Then
                            _dpold.FiscalYear = dp.FiscalYear
                            _dpold.IsChangedWSM = True
                        End If

                        Dim IsGoingToBeFinished As Boolean = False
                        Dim StatusToReplace As Integer
                        If Not IsNewFromDNet Then
                            If _dpold.Status = EnumPaymentStatus.PaymentStatus.Validasi Then '20120809;yurike:donimeisano;donas
                                StatusToReplace = _dpold.Status
                                IsGoingToBeFinished = True

                                _dpold.Status = EnumPaymentStatus.PaymentStatus.Selesai
                            End If
                        End If
                        If dp.IsReversed = 2 Then
                            _dpold.IsReversed = IIf(dp.IsReversed > 0, 1, 0) '1=X;2=DocNumber
                            Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            Dim tmpArlDP As New ArrayList
                            tmpArlDP.Add(dp)
                            Dim arlDP As New ArrayList
                            Dim arlDPToUpdate As New ArrayList
                            Dim crtDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crtDP.opAnd(New Criteria(GetType(DailyPayment), "DocNumber", MatchType.Exact, dp.ReversedDocNumber))
                            crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, dp.POHeader.ID))
                            arlDP = objDPFac.Retrieve(crtDP)
                            For Each oDP As DailyPayment In arlDP
                                oDP.IsReversed = 1
                                'If oDP.ID = item.ID Then
                                oDP.EffectiveDate = dp.EffectiveDate
                                oDP.IsCleared = dp.IsCleared
                                'End If
                                arlDPToUpdate.Add(oDP)
                            Next
                            objDPFac.Update(arlDPToUpdate)

                        End If
                        If _dpold.IsChangedWSM Then
                            If _dpold.DocDate.Year < 1990 Then
                                _dpold.DocDate = DateSerial(1990, _dpold.DocDate.Month, _dpold.DocDate.Day)
                            End If
                            If _dpold.BaselineDate.Year < 1990 Then
                                _dpold.BaselineDate = DateSerial(1990, _dpold.BaselineDate.Month, _dpold.BaselineDate.Day)
                            End If
                            'm_TransactionManager.AddUpdate(_dpold, m_userPrincipal.Identity.Name)
                            m_DailyPaymentMapper.Update(_dpold, m_userPrincipal.Identity.Name)
                            If IsGoingToBeFinished Then
                                oSCHFac.Insert(CInt(LookUp.DocumentType.Gyro), _dpold.ID, StatusToReplace, _dpold.Status)
                            End If
                        End If
                    Else
                        If Not IsNewFromDNet Then
                            dp.Status = EnumPaymentStatus.PaymentStatus.Selesai

                            If dp.SlipNumber.Trim.StartsWith("TRF") Then
                                dp.EntryType = EnumGyroEntryType.EntryType.Transfer
                            Else
                                dp.EntryType = EnumGyroEntryType.EntryType.Gyro
                            End If
                            dp.GyroType = EnumGyroType.GyroType.Normal
                            dp.Bank = GetSlipNumberBank(dp.SlipNumber)
                            dp.BankID = dp.Bank.ID
                        End If

                        If dp.IsReversed = 2 Then
                            dp.IsReversed = 1
                            Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            Dim tmpArlDP As New ArrayList
                            tmpArlDP.Add(dp)
                            Dim arlDP As New ArrayList
                            Dim arlDPToUpdate As New ArrayList
                            Dim crtDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crtDP.opAnd(New Criteria(GetType(DailyPayment), "DocNumber", MatchType.Exact, dp.ReversedDocNumber))
                            crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, dp.POHeader.ID))
                            arlDP = objDPFac.Retrieve(crtDP)
                            For Each oDP As DailyPayment In arlDP
                                oDP.IsReversed = 1
                                'If oDP.ID = item.ID Then
                                oDP.EffectiveDate = dp.EffectiveDate
                                oDP.IsCleared = dp.IsCleared
                                'End If
                                arlDPToUpdate.Add(oDP)
                            Next
                            objDPFac.Update(arlDPToUpdate)
                        ElseIf dp.IsReversed = 0 Then
                            'find similar slipnumber->indicates : this DP is accelerator, old DP as accelerated(reversed)
                            'Dim aDPReversed As ArrayList
                            'Dim crtDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            'crtDP.opAnd(New Criteria(GetType(DailyPayment), "SlipNumber", MatchType.Exact, dp.SlipNumber))
                            ''aDPReversed = objDPFac.Retrieve(crtDP)

                        End If
                        dp.NumOfTransfered = 0
                        Dim objDPHFac As DailyPaymentHeaderFacade = New DailyPaymentHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        If dp.DocDate.Year < 1990 Then
                            dp.DocDate = DateSerial(1990, dp.DocDate.Month, dp.DocDate.Day)
                        End If
                        If dp.BaselineDate.Year < 1990 Then
                            dp.BaselineDate = DateSerial(1990, dp.BaselineDate.Month, dp.BaselineDate.Day)
                        End If
                        objDPHFac.InsertByDP(dp)
                        'm_TransactionManager.AddInsert(dp, m_userPrincipal.Identity.Name)
                    End If


                    'If value = 0 Then
                    '    m_TransactionManager.AddInsert(dp, m_userPrincipal.Identity.Name)
                    'Else
                    '    Dim _dpold As New DailyPayment
                    '    _dpold = Retrieve(dp.ReceiptNumber)
                    '    _dpold.Amount = dp.Amount
                    '    _dpold.BaselineDate = dp.BaselineDate
                    '    _dpold.DocDate = dp.DocDate
                    '    _dpold.DocNumber = dp.DocNumber
                    '    _dpold.PaymentPurpose = dp.PaymentPurpose
                    '    _dpold.POHeader = dp.POHeader
                    '    _dpold.ReceiptNumber = dp.ReceiptNumber
                    '    _dpold.SAPCreator = dp.SAPCreator
                    '    _dpold.SlipNumber = dp.SlipNumber

                    '    m_TransactionManager.AddUpdate(_dpold, m_userPrincipal.Identity.Name)
                    'End If


                    'If performTransaction Then
                    '    m_TransactionManager.PerformTransaction()
                    '    returnValue = 0
                    'End If
                    returnValue = 0
                    ProcessStatusTolakan(dp.StatusTolakan)
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

        Private Sub ProcessStatusTolakan(ByVal _statusTolak As String)
            Dim _tolak As Integer = -1
            If Not _statusTolak Is Nothing Then
                If _statusTolak <> "" Then
                    If _statusTolak.Split(":").Length < 2 Then
                        Return
                    End If
                    If _statusTolak.Split(":")(0).Trim.ToUpper = "RSCH" Then
                        _tolak = CInt(RejectStatusPayment.RejectStatusEnum.Reschedule)
                    Else
                        If _statusTolak.Split(":")(0).Trim.ToUpper = "RCLR" Then
                            _tolak = CInt(RejectStatusPayment.RejectStatusEnum.ReClearing)
                        Else
                            If _statusTolak.Split(":")(0).Trim.ToUpper = "RPLC" Then
                                _tolak = CInt(RejectStatusPayment.RejectStatusEnum.Replacing)
                            End If
                        End If
                    End If
                    If _tolak > 0 Then
                        Dim paymentList As ArrayList = New ArrayList
                        For Each item As String In _statusTolak.Split(":")(1).Trim.ToUpper.Split(" ")
                            Dim payment As DailyPayment = RetrieveByDocNumber(item)
                            If Not payment Is Nothing Then
                                If payment.ID > 0 Then
                                    payment.RejectStatus = _tolak
                                    paymentList.Add(payment)
                                End If

                            End If
                        Next
                        If paymentList.Count > 0 Then
                            Update(paymentList)
                        End If
                    End If
                End If
            End If
        End Sub


        Public Function GetAggregateResult(ByVal aggregate As IAggregate, ByVal criteria As ICriteria) As Decimal
            Dim result As Object = m_DailyPaymentMapper.RetrieveScalar(aggregate, criteria)
            If result Is System.DBNull.Value Then
                Return 0
            Else
                Return CType(result, Decimal)
            End If
        End Function

#End Region

#Region "Custom Method"
        Public Sub Update(ByVal ArrList As ArrayList)
            Try
                For Each item As DailyPayment In ArrList
                    'prevent NULL value
                    item.RemarkStatus = EnumPaymentRemarkStatus.GetEnumValue(EnumPaymentRemarkStatus.GetStringValue(item.RemarkStatus))
                    If item.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.Reject OrElse item.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.NotCleared Then
                        item.IsCleared = 0
                    Else
                        item.IsCleared = 1
                    End If
                    If item.IsCleared = 1 Then item.Status = EnumPaymentStatus.PaymentStatus.Selesai
                    If item.DocDate.Year < 1990 Then
                        item.DocDate = DateSerial(1990, item.DocDate.Month, item.DocDate.Day)
                    End If
                    If item.BaselineDate.Year < 1990 Then
                        item.BaselineDate = DateSerial(1990, item.BaselineDate.Month, item.BaselineDate.Day)
                    End If
                    m_DailyPaymentMapper.Update(item, m_userPrincipal.Identity.Name)
                Next
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Update(ByVal obj As DailyPayment)
            Try
                'prevent NULL value
                obj.RemarkStatus = EnumPaymentRemarkStatus.GetEnumValue(EnumPaymentRemarkStatus.GetStringValue(obj.RemarkStatus))
                If obj.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.Reject OrElse obj.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.NotCleared Then
                    obj.IsCleared = 0
                Else
                    obj.IsCleared = 1
                End If
                If obj.IsCleared = 1 Then obj.Status = EnumPaymentStatus.PaymentStatus.Selesai
                If obj.DocDate.Year < 1990 Then
                    obj.DocDate = DateSerial(1990, obj.DocDate.Month, obj.DocDate.Day)
                End If
                If obj.BaselineDate.Year < 1990 Then
                    obj.BaselineDate = DateSerial(1990, obj.BaselineDate.Month, obj.BaselineDate.Day)
                End If
                m_DailyPaymentMapper.Update(obj, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Insert(ByVal obj As DailyPayment)
            Try
                'prevent NULL value
                obj.RemarkStatus = EnumPaymentRemarkStatus.GetEnumValue(EnumPaymentRemarkStatus.GetStringValue(obj.RemarkStatus))
                If obj.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.Reject OrElse obj.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.NotCleared Then
                    obj.IsCleared = 0
                Else
                    obj.IsCleared = 1
                End If
                If obj.IsCleared = 1 Then obj.Status = EnumPaymentStatus.PaymentStatus.Selesai
                If obj.DocDate.Year < 1990 Then
                    obj.DocDate = DateSerial(1990, obj.DocDate.Month, obj.DocDate.Day)
                End If
                If obj.BaselineDate.Year < 1990 Then
                    obj.BaselineDate = DateSerial(1990, obj.BaselineDate.Month, obj.BaselineDate.Day)
                End If
                obj.NumOfTransfered = 0
                m_DailyPaymentMapper.Insert(obj, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Delete(ByVal objDomain As DailyPayment)
            Dim nResult As Integer = -1
            Try
                Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim cDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim aDP As ArrayList

                cDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratorID", MatchType.Exact, objDomain.ID))
                aDP = oDPFac.Retrieve(cDP)
                For Each oDP As DailyPayment In aDP
                    oDP.AcceleratorID = 0
                    oDP.AcceleratedGyro = 0
                    oDP.AcceleratedDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                    oDPFac.Update(oDP)
                Next
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                '  m_DailyPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                m_DailyPaymentMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub


        Public Sub SetInterestDiffOfAccelerate(ByRef pDP As DailyPayment, ByRef DiffInterest As Decimal, ByRef PPh As Decimal, ByVal GyroType As Integer, ByVal AccDate As Date, ByVal NewBaseLineDate As Date, Optional ByRef IntRecalculation As Decimal = 0)
            If "Acceleration" = "Acceleration" Then
                Dim oDPOld As DailyPayment
                Dim oVRP As V_RekapPO = pDP.POHeader.V_RekapPO
                If pDP.ID > 0 Then
                    oDPOld = New DailyPaymentFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(pDP.ID)
                    If Not IsNothing(oDPOld) AndAlso oDPOld.ID > 0 Then
                        For Each oDP As DailyPayment In oDPOld.OtherAssignments(True)
                            If oDP.POHeader.ID = oVRP.POHeader.ID Then
                                If GyroType = EnumGyroType.GyroType.Percepatan Then
                                    Dim OldInterest As Decimal = 0
                                    Dim NewInterest As Decimal = 0
                                    Dim IntRate As Decimal = 0

                                    OldInterest = oVRP.TotalHargaIT
                                    For Each oPOD As PODetail In oDP.POHeader.PODetails
                                        Dim oP As Price = oPOD.ContractDetail.VechileColor.Price(oPOD.POHeader.ReqAllocationDateTime)
                                        If Not IsNothing(oP) AndAlso oP.ID > 0 Then IntRate = IIf(oPOD.POHeader.IsFactoring = 1, oP.FactoringInt, oP.Interest)
                                        Exit For
                                    Next
                                    NewInterest = (IntRate / 100) * _
                                                DateDiff(DateInterval.Day, oVRP.POHeader.ReqAllocationDateTime, AccDate) / _
                                                DateTime.DaysInMonth(oDP.POHeader.ReqAllocationDateTime.Year, oDP.POHeader.ReqAllocationDateTime.Month) * _
                                                oDP.Amount * 0.85
                                    Dim tmpDiffInterest As Decimal = OldInterest - NewInterest
                                    'DiffInterest =  NewInterest 'kok kebalik ya..:(
                                    IntRecalculation = NewInterest
                                    DiffInterest = tmpDiffInterest
                                    PPh = DiffInterest * (0.15 / 0.85)
                                ElseIf GyroType = EnumGyroType.GyroType.Tolakan Then
                                    Dim Pinalty As Decimal = 0
                                    Pinalty = DateDiff(DateInterval.Day, oDP.BaselineDate, NewBaseLineDate) / _
                                            30 * 0.65 * 0.85 * oDP.Amount
                                    DiffInterest = Pinalty / 10 'kok dibagi 10 ya... :(
                                    PPh = DiffInterest * (0.15 / 0.85)
                                End If
                            End If
                        Next
                    End If
                End If
            End If

        End Sub

        Private Function GetSlipNumberBank(ByVal SlipNumber As String) As Bank
            Dim intSpacePos As Integer = SlipNumber.IndexOf(" ")
            Dim strFirst As String = ""
            Dim strSecond As String = ""
            Dim oBank As Bank = New Bank
            Dim oBFac As BankFacade = New BankFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            strFirst = Left(SlipNumber, intSpacePos).Trim
            strSecond = Right(SlipNumber, SlipNumber.Length - intSpacePos).Trim
            If SlipNumber.Trim.StartsWith("TRF") Then
                oBank = oBFac.Retrieve(strSecond)
            Else
                oBank = oBFac.Retrieve(strFirst)
            End If
            Return oBank
        End Function

        Public Function isLCExists(ByVal paymentPurposeCode As String, ByVal aDP As ArrayList) As String
            If (paymentPurposeCode <> "LC") Then
                For Each oDP As DailyPayment In aDP
                    Dim po As POHeader = oDP.POHeader
                    If Not IsNothing(po) Then
                        If po.PODestination.ID <> 1 Then '1 adalah pengiriman dari Dealer, yang dicek hanya yg dr MMKSI
                            'cari di salesorderduedate apakah ada yang LC untuk sales order tersebut
                            'Dim getSalesOrderLCQuery As String = "SELECT * FROM DailyPayment A INNER JOIN PaymentPurpose B ON B.ID = A.PaymentPurposeID AND A.POID = " + po.ID.ToString() + " AND B.PaymentPurposeCode = 'LC'"
                            'Perubahan untuk pembayaran LC sekarang masuk ke transferpayment
                            Dim getSalesOrderLCQuery As String = "SELECT * FROM dbo.TransferPayment TP INNER JOIN dbo.PaymentPurpose PP ON PP.ID = TP.PaymentPurposeID " &
                            "INNER JOIN	dbo.TransferPaymentDetail TPD ON TPD.TransferPaymentID = TP.ID INNER JOIN dbo.SalesOrder SO ON SO.ID = TPD.SalesOrderID" &
                            "INNER JOIN dbo.POHeader POH ON POH.SONumber = SO.SONumber WHERE POH.ID =  " + po.ID.ToString() + " AND PP.PaymentPurposeCode = 'LC'"

                            Dim ds As DataSet = RetrieveSp(getSalesOrderLCQuery)
                            If ds.Tables.Count > 0 Then
                                If ds.Tables(0).Rows.Count = 0 Then
                                    Return po.PONumber
                                    Exit Function
                                End If
                            End If                          
                        End If
                    End If
                Next
            End If
            Return ""
        End Function

        Public Function RetrieveSp(str As String) As DataSet
            Return m_DailyPaymentMapper.RetrieveDataSet(str)
        End Function

#End Region

    End Class

End Namespace