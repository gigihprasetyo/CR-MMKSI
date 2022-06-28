
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
'// Copyright  2015
'// ---------------------
'// $History      : $
'// Generated on 11/27/2015 - 2:06:27 PM
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

    Public Class TransactionControlPKFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionControlPKMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TransactionControlPKMapper = MapperFactory.GetInstance.GetMapper(GetType(TransactionControlPK).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TransactionControlPK
            Return CType(m_TransactionControlPKMapper.Retrieve(ID), TransactionControlPK)
        End Function

        Public Function Retrieve(ByVal Code As String) As TransactionControlPK
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransactionControlPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TransactionControlPK), "TransactionControlPKCode", MatchType.Exact, Code))

            Dim TransactionControlPKColl As ArrayList = m_TransactionControlPKMapper.RetrieveByCriteria(criterias)
            If (TransactionControlPKColl.Count > 0) Then
                Return CType(TransactionControlPKColl(0), TransactionControlPK)
            End If
            Return New TransactionControlPK
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TransactionControlPKMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TransactionControlPKMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TransactionControlPKMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransactionControlPK), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransactionControlPKMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransactionControlPK), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransactionControlPKMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransactionControlPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TransactionControlPK As ArrayList = m_TransactionControlPKMapper.RetrieveByCriteria(criterias)
            Return _TransactionControlPK
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransactionControlPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TransactionControlPKColl As ArrayList = m_TransactionControlPKMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransactionControlPKColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TransactionControlPKColl As ArrayList = m_TransactionControlPKMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TransactionControlPKColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransactionControlPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TransactionControlPKColl As ArrayList = m_TransactionControlPKMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TransactionControlPK), columnName, matchOperator, columnValue))
            Return TransactionControlPKColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TransactionControlPK), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransactionControlPK), columnName, matchOperator, columnValue))

            Return m_TransactionControlPKMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransactionControlPK), "TransactionControlPKCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TransactionControlPK), "TransactionControlPKCode", AggregateType.Count)
            Return CType(m_TransactionControlPKMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TransactionControlPK) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TransactionControlPKMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TransactionControlPK) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TransactionControlPKMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TransactionControlPK)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TransactionControlPKMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TransactionControlPK)
            Try
                m_TransactionControlPKMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

#End Region

        Function RetrieveTransactionControl(dealerid As Integer, modelid As Integer, transactionControlPKKind As EnumTransactionControlPKKind.TransactionControlPKKind) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransactionControlPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TransactionControlPK), "Dealer.ID", MatchType.Exact, dealerid))
            criterias.opAnd(New Criteria(GetType(TransactionControlPK), "VechileModel.ID", MatchType.Exact, modelid))
            criterias.opAnd(New Criteria(GetType(TransactionControlPK), "Kind", MatchType.Exact, CType(transactionControlPKKind, Integer)))
            Dim pkTransactionControlList As ArrayList = Retrieve(criterias)
            Dim pkTransactionControl As TransactionControlPK
            If (pkTransactionControlList.Count > 0) Then
                Try

                    pkTransactionControl = CType(pkTransactionControlList.Item(0), TransactionControlPK)
                Catch ex As Exception

                End Try

            End If
            Return IsNothing(pkTransactionControl) OrElse CType(pkTransactionControl.Status.Trim, Integer) = EnumDealerStatus.DealerStatus.NonAktive
        End Function

        Public Function IsTransactionPKBlocked_Old(objPkHeader As PKHeader, objDealer As Dealer, kind As EnumTransactionControlPKKind.TransactionControlPKKind, ByRef stockRatioProblem As String) As Boolean
            Dim dateStart As Date = Date.Now
            If (dateStart.Month = CType(objPkHeader.RequestPeriodeMonth, Integer) AndAlso dateStart.Year = CType(objPkHeader.RequestPeriodeYear, Integer)) Then
                dateStart = New Date(objPkHeader.RequestPeriodeYear, objPkHeader.RequestPeriodeMonth, 1)
                Dim dateEnd As New Date(objPkHeader.RequestPeriodeYear, objPkHeader.RequestPeriodeMonth, Date.DaysInMonth(objPkHeader.RequestPeriodeYear, objPkHeader.RequestPeriodeMonth))

                If (objPkHeader.OrderType = enumOrderType.OrderType.Tambahan OrElse kind = EnumTransactionControlPKKind.TransactionControlPKKind.KONFIRMASI_PK_TAMBAHAN) Then

                    Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.StockActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(StockActual), "Dealer.ID", MatchType.Exact, objDealer.ID))


                    criterias2.opAnd(New Criteria(GetType(StockActual), "Dealer.ID", MatchType.No, 1))
                    criterias2.opAnd(New Criteria(GetType(StockActual), "Dealer.Title", MatchType.Exact, "0"))
                    criterias2.opAnd(New Criteria(GetType(StockActual), "Dealer.Status", MatchType.Exact, "1"))

                    Dim _categoryIDs As New ArrayList
                    Dim _modelIDs As New ArrayList
                    For i As Integer = 0 To objPkHeader.PKDetails.Count - 1
                        Dim categoryid = CType(objPkHeader.PKDetails.Item(i), PKDetail).VechileColor.VechileType.VechileModel.Category.ID
                        If (_categoryIDs.Contains(categoryid)) Then
                            _modelIDs.Add(CType(objPkHeader.PKDetails.Item(i), PKDetail).VechileColor.VechileType.VechileModel.ID)

                            Continue For
                        End If

                        Dim criteriasPlanMeeting As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesPlanMeeting), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasPlanMeeting.opAnd(New Criteria(GetType(SalesPlanMeeting), "DateTime", MatchType.GreaterOrEqual, dateStart))
                        criteriasPlanMeeting.opAnd(New Criteria(GetType(SalesPlanMeeting), "DateTime", MatchType.LesserOrEqual, dateEnd))

                        criteriasPlanMeeting.opAnd(New Criteria(GetType(SalesPlanMeeting), "Category.ID", MatchType.Exact, categoryid))

                        Dim splan As ArrayList = New SalesPlanMeetingFacade(m_userPrincipal).Retrieve(criteriasPlanMeeting)


                        If (Not IsNothing(splan) AndAlso splan.Count > 0 AndAlso CType(splan.Item(0), SalesPlanMeeting).DateTime.Day < Date.Now.Day) Then
                            _categoryIDs.Add(categoryid)

                            _modelIDs.Add(CType(objPkHeader.PKDetails.Item(i), PKDetail).VechileColor.VechileType.VechileModel.ID)

                        End If

                    Next
                    If (_categoryIDs.Count > 0) Then
                        criterias2.opAnd(New Criteria(GetType(StockActual), "VechileModel.Category.ID", MatchType.InSet, "(" & String.Join(",", _categoryIDs.ToArray) & ")"))
                    End If
                    If (_modelIDs.Count > 0) Then
                        criterias2.opAnd(New Criteria(GetType(StockActual), "VechileModel.ID", MatchType.InSet, "(" & String.Join(",", _modelIDs.ToArray) & ")"))
                    End If
                    ''Change 20160211 
                    'Requestor : angga
                    Dim requestdate As Date = New Date(objPkHeader.RequestPeriodeYear, objPkHeader.RequestPeriodeMonth, 1) '.AddMonths(-1)
                    criterias2.opAnd(New Criteria(GetType(StockActual), "Month", MatchType.Exact, CType(requestdate.Month, Integer)))

                    criterias2.opAnd(New Criteria(GetType(StockActual), "Year", MatchType.Exact, CType(requestdate.Year, Integer)))


                    Dim StockActualArrayList As ArrayList = New KTB.DNET.BusinessFacade.StockActualFacade(m_userPrincipal).Retrieve(criterias2)
                    If (StockActualArrayList.Count > 0) Then
                        For Each itemStockActual As StockActual In StockActualArrayList
                            If (kind = EnumTransactionControlPKKind.TransactionControlPKKind.INPUT_PK_TAMBAHAN) Then
                                If (itemStockActual.GetCurrentOrderLeft > 0 AndAlso itemStockActual.StockTarget.IsDealerBlock) Then
                                    If (New KTB.DNET.BusinessFacade.TransactionControlPKFacade(m_userPrincipal).RetrieveTransactionControl(objDealer.ID, itemStockActual.VechileModel.ID, kind)) Then
                                        stockRatioProblem &= "Kurang " & itemStockActual.GetCurrentOrderLeft.ToString & " unit untuk model " & itemStockActual.VechileModel.Description & ";"
                                    End If


                                End If
                            ElseIf (kind = EnumTransactionControlPKKind.TransactionControlPKKind.KONFIRMASI_PK_TAMBAHAN) Then
                                If (itemStockActual.GetCurrentOrderLeft > 0 AndAlso itemStockActual.StockTarget.IsKTBBlock) Then
                                    If (New KTB.DNET.BusinessFacade.TransactionControlPKFacade(m_userPrincipal).RetrieveTransactionControl(objDealer.ID, itemStockActual.VechileModel.ID, kind)) Then
                                        stockRatioProblem &= "Kurang " & itemStockActual.GetCurrentOrderLeft.ToString & " unit untuk model " & itemStockActual.VechileModel.Description & ";"
                                    End If


                                End If
                            End If

                        Next
                        If stockRatioProblem.Trim <> "" Then
                            Return True
                            Exit Function
                        End If
                    End If
                End If
            End If
            Return False
        End Function


        'Created by anh 2016-02-12
        Public Function IsTransactionPKBlocked(objPkHeader As PKHeader, objDealer As Dealer, kind As EnumTransactionControlPKKind.TransactionControlPKKind, ByRef stockRatioProblem As String) As Boolean
            'based on PKHeader
            '- Status : Baru, Validasi  (0,2)
            '- ReqPeriodMonth and Year harus bulan ini
            '- Pengecekan ke SalesPlanMeeting
            '  apakah Hari ini > Datetime di salesplan meeting
            '	kalo iya, ambil kateogri di spm
            '		Cek Kes stockActual:
            '			stock Ratio < Stock Target untuk Model yang sesuai dengan category
            '				cek di IsKTBBlock di StcokTarget apakah 255 berdasarkan model 
            '					cek di transControlPK untuk dealer dan modelid and kind = 1 statusnya = 1
            Dim vRet As Boolean = False
            If (objPkHeader.PKStatus = enumStatusPK.Status.Baru) Or (objPkHeader.PKStatus = enumStatusPK.Status.Validasi) Then
                Dim dateStart As Date = Date.Now
                If (dateStart.Month = CType(objPkHeader.RequestPeriodeMonth, Integer) AndAlso dateStart.Year = CType(objPkHeader.RequestPeriodeYear, Integer)) Then

                    dateStart = New Date(objPkHeader.RequestPeriodeYear, objPkHeader.RequestPeriodeMonth, 1)
                    Dim dateEnd As New Date(objPkHeader.RequestPeriodeYear, objPkHeader.RequestPeriodeMonth, Date.DaysInMonth(objPkHeader.RequestPeriodeYear, objPkHeader.RequestPeriodeMonth))

                    Dim criteriasSPM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesPlanMeeting), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriasSPM.opAnd(New Criteria(GetType(SalesPlanMeeting), "DateTime", MatchType.GreaterOrEqual, dateStart))
                    criteriasSPM.opAnd(New Criteria(GetType(SalesPlanMeeting), "DateTime", MatchType.LesserOrEqual, dateEnd))
                    criteriasSPM.opAnd(New Criteria(GetType(SalesPlanMeeting), "Category.ID", MatchType.Exact, objPkHeader.Category.ID))

                    Dim _spm As ArrayList = New SalesPlanMeetingFacade(m_userPrincipal).Retrieve(criteriasSPM)

                    If (Not IsNothing(_spm) AndAlso _spm.Count > 0 AndAlso CType(_spm.Item(0), SalesPlanMeeting).DateTime.Day < Date.Now.Day) Then
                        Dim _modelIDs As New ArrayList
                        For i As Integer = 0 To objPkHeader.PKDetails.Count - 1
                            _modelIDs.Add(CType(objPkHeader.PKDetails.Item(i), PKDetail).VechileType.VechileModel.ID)
                        Next
                        Dim criteriasStockActual As New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.StockActual), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasStockActual.opAnd(New Criteria(GetType(StockActual), "Dealer.ID", MatchType.Exact, objPkHeader.Dealer.ID))
                        criteriasStockActual.opAnd(New Criteria(GetType(StockActual), "Month", MatchType.Exact, CType(objPkHeader.RequestPeriodeMonth, Integer)))
                        criteriasStockActual.opAnd(New Criteria(GetType(StockActual), "Year", MatchType.Exact, CType(objPkHeader.RequestPeriodeYear, Integer)))
                        criteriasStockActual.opAnd(New Criteria(GetType(StockActual), "VechileModel.Category.ID", MatchType.Exact, objPkHeader.Category.ID))
                        criteriasStockActual.opAnd(New Criteria(GetType(StockActual), "VechileModel.ID", MatchType.InSet, "(" & String.Join(",", _modelIDs.ToArray) & ")"))

                        Dim StockActualArrayList As ArrayList = New KTB.DNET.BusinessFacade.StockActualFacade(m_userPrincipal).Retrieve(criteriasStockActual)

                        If (StockActualArrayList.Count > 0) Then
                            For Each itemStockActual As StockActual In StockActualArrayList
                                If itemStockActual.GetCurrentOrderLeft < itemStockActual.StockTarget.TargetRatio Then
                                    If itemStockActual.StockTarget.IsKTBBlock = 255 Then
                                        If (kind = EnumTransactionControlPKKind.TransactionControlPKKind.INPUT_PK_TAMBAHAN) Then
                                            If (itemStockActual.GetCurrentOrderLeft > 0 AndAlso itemStockActual.StockTarget.IsDealerBlock) Then
                                                If (New KTB.DNET.BusinessFacade.TransactionControlPKFacade(m_userPrincipal).RetrieveTransactionControl(objDealer.ID, itemStockActual.VechileModel.ID, kind)) Then
                                                    stockRatioProblem &= "Kurang " & itemStockActual.GetCurrentOrderLeft.ToString & " unit untuk model " & itemStockActual.VechileModel.Description & ";"
                                                End If
                                            End If
                                        ElseIf (kind = EnumTransactionControlPKKind.TransactionControlPKKind.KONFIRMASI_PK_TAMBAHAN) Then
                                            If (itemStockActual.GetCurrentOrderLeft > 0 AndAlso itemStockActual.StockTarget.IsKTBBlock) Then
                                                If (New KTB.DNET.BusinessFacade.TransactionControlPKFacade(m_userPrincipal).RetrieveTransactionControl(objDealer.ID, itemStockActual.VechileModel.ID, kind)) Then
                                                    stockRatioProblem &= "Kurang " & itemStockActual.GetCurrentOrderLeft.ToString & " unit untuk model " & itemStockActual.VechileModel.Description & ";"
                                                End If
                                            End If
                                        End If
                                    End If
                                End If

                            Next
                            If stockRatioProblem.Trim <> "" Then
                                vRet = True
                            End If
                        End If
                    End If
                End If
            End If

            Return vRet

        End Function

    End Class

End Namespace
