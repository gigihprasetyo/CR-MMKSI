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
Imports System.Math

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
#End Region

Namespace KTB.DNet.BusinessFacade.PO

    Public Class PPQtyFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_PPQtyMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PPQtyMapper = MapperFactory.GetInstance.GetMapper(GetType(PPQty).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PPQty))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.POHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PODetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As PPQty
            Return CType(m_PPQtyMapper.Retrieve(ID), PPQty)
        End Function

        Public Function Retrieve(ByVal Code As String) As PPQty
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PPQty), "PPQtyCode", MatchType.Exact, Code))

            Dim PPQtyColl As ArrayList = m_PPQtyMapper.RetrieveByCriteria(criterias)
            If (PPQtyColl.Count > 0) Then
                Return CType(PPQtyColl(0), PPQty)
            End If
            Return New PPQty
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PPQtyMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PPQtyMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PPQtyMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PPQty), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PPQtyMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PPQty), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PPQtyMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _PPQty As ArrayList = m_PPQtyMapper.RetrieveByCriteria(criterias)
            Return _PPQty
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PPQtyColl As ArrayList = m_PPQtyMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PPQtyColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PPQtyColl As ArrayList = m_PPQtyMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PPQtyColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PPQtyColl As ArrayList = m_PPQtyMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(PPQty), columnName, matchOperator, columnValue))
            Return PPQtyColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.domain.Search.SortCollection = New KTB.DNet.domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.domain.Search.Sort(GetType(POHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), columnName, matchOperator, columnValue))

            Return m_PPQtyMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "PPQtyCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(PPQty), "PPQtyCode", AggregateType.Count)
            Return CType(m_PPQtyMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As PPQty) As Integer
            Dim iReturn As Integer = -2
            Try
                m_PPQtyMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Sub Update(ByVal objDomain As PPQty)
            Try
                m_PPQtyMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function SynchronizeQuantity(ByVal objDomain As KTB.DNet.Domain.PPQty) As Integer
            Dim returnValue As Integer = -1
            'If (Me.IsTaskFree()) Then
            '    Try
            '        Me.SetTaskLocking()
            '        Dim performTransaction As Boolean = True
            '        Dim ObjMapper As IMapper


            Dim ppColl As ArrayList = RetrieveProposeQuantity(objDomain)

            If ppColl.Count > 0 Then

                Dim objPPQty As PPQty = CType(ppColl.Item(0), PPQty)
                If objPPQty.AllocationQty <> objDomain.AllocationQty Then
                    objPPQty.AllocationQty = objDomain.AllocationQty
                    'm_TransactionManager.AddUpdate(objPPQty, m_userPrincipal.Identity.Name)
                    Update(objPPQty)
                End If
            Else
                'm_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                Insert(objDomain)
            End If

            Dim objPODetailFacade As New PODetailFacade(Me.m_userPrincipal)
            Dim PoDetailColl As ArrayList = RetreivePODetails(New Date(objDomain.PeriodeYear, objDomain.PeriodeMonth, objDomain.PeriodeDate), objDomain.DealerCode, objDomain.MaterialNumber, objDomain.ProductionYear)

            'Cari total permintan
            Dim totalRequest As Integer = 0
            For Each item As PODetail In PoDetailColl
                totalRequest += item.ReqQty
            Next

            Dim isChanged As Boolean = False
            Dim calculatedValue As Integer = 0

            For Each item As PODetail In PoDetailColl

                isChanged = False
                calculatedValue = 0
                If totalRequest = 0 Then
                    If item.ProposeQty <> 0 Then
                        item.ProposeQty = 0
                        isChanged = True
                    End If
                Else
                    calculatedValue = Math.Floor((item.ReqQty / totalRequest) * objDomain.AllocationQty)
                    If item.ProposeQty <> calculatedValue Then
                        item.ProposeQty = calculatedValue
                        isChanged = True
                    End If
                End If

                If item.ProposeQty > item.ReqQty Then
                    If item.AllocQty <> item.ReqQty Then
                        item.AllocQty = item.ReqQty
                        isChanged = True
                    End If
                Else
                    If item.AllocQty <> item.ProposeQty Then
                        item.AllocQty = item.ProposeQty
                        isChanged = True
                    End If
                End If
                'm_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                If isChanged Then
                    objPODetailFacade.Update(item)
                End If
            Next




            '        If performTransaction Then
            '            m_TransactionManager.PerformTransaction()
            '            returnValue = objDomain.ID
            '        End If
            '    Catch ex As Exception
            '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
            '        If rethrow Then
            '            Throw
            '        End If
            '    Finally
            '        Me.RemoveTaskLocking()
            '    End Try
            'End If
            Return returnValue
        End Function

        Public Function SynchronizeQuantity(ByVal arlPPQtys As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim ppColl As ArrayList
            Dim TotalPPQty As Integer
            Dim oPPQty As PPQty
            Dim aPPQtys As New ArrayList
            Dim OldMatNumber As String = String.Empty
            Dim isExist As Boolean


            For Each objDomain As PPQty In arlPPQtys
                ppColl = RetrieveProposeQuantity(objDomain)
                If IsNothing(oPPQty) Then oPPQty = objDomain

                If ppColl.Count > 0 Then
                    Dim objPPQty As PPQty = CType(ppColl.Item(0), PPQty)

                    If objPPQty.AllocationQty <> objDomain.AllocationQty Then
                        objPPQty.AllocationQty = objDomain.AllocationQty
                        Update(objPPQty)
                    End If
                Else
                    Insert(objDomain)
                End If

                isExist = False
                For i As Integer = 0 To aPPQtys.Count - 1
                    If (objDomain.MaterialNumber.Trim.ToLower() = CType(aPPQtys(i), PPQty).MaterialNumber.Trim().ToLower() _
                    AndAlso objDomain.ProductionYear = CType(aPPQtys(i), PPQty).ProductionYear) Then
                        isExist = True
                        Exit For
                    End If
                Next
                If (isExist = False) Then
                    aPPQtys.Add(objDomain)
                End If
                'If objDomain.MaterialNumber <> OldMatNumber Then
                '    aPPQtys.Add(objDomain)
                '    OldMatNumber = objDomain.MaterialNumber
                'End If
            Next

            For i As Integer = 0 To aPPQtys.Count - 1
                oPPQty = aPPQtys(i)

                If IsNothing(oPPQty) Then Return returnValue
                TotalPPQty = GetTotalPPqty(oPPQty)
                Dim objPODetailFacade As New PODetailFacade(Me.m_userPrincipal)
                Dim PoDetailColl As ArrayList = RetreivePODetailsAllDealer(New Date(oPPQty.PeriodeYear, oPPQty.PeriodeMonth, oPPQty.PeriodeDate), oPPQty.MaterialNumber, oPPQty.ProductionYear)
                'Cari total permintan
                Dim totalRequest As Integer = 0
                For Each item As PODetail In PoDetailColl
                    totalRequest += item.ReqQty
                Next

                Dim isChanged As Boolean = False
                Dim calculatedValue As Integer = 0
                Dim Sisa As Integer = TotalPPQty
                Dim IsUpdated As Boolean = False

                For Each item As PODetail In PoDetailColl
                    isChanged = False
                    calculatedValue = 0
                    If totalRequest = 0 Then
                        If item.ProposeQty <> 0 Then
                            item.ProposeQty = 0
                            isChanged = True
                        End If
                    Else
                        Dim tmpDec As Decimal = (CType(item.ReqQty, Decimal) / CType(totalRequest, Decimal)) * CType(TotalPPQty, Decimal)
                        Dim tmpInt As Integer = Math.Floor((CType(item.ReqQty, Decimal) / CType(totalRequest, Decimal)) * CType(TotalPPQty, Decimal))
                        If tmpDec - CType(tmpInt, Decimal) >= 0.5 Then
                            tmpInt += 1
                        End If
                        If tmpInt > Sisa Then tmpInt = Sisa
                        calculatedValue = tmpInt
                        'calculatedValue = Math.Floor((CType(item.ReqQty, Decimal) / CType(totalRequest, Decimal)) * CType(TotalPPQty, Decimal))
                        If item.ProposeQty <> calculatedValue Then
                            item.ProposeQty = calculatedValue
                            isChanged = True
                        End If
                    End If

                    If item.ProposeQty > item.ReqQty Then
                        If item.AllocQty <> item.ReqQty Then
                            item.AllocQty = item.ReqQty
                            isChanged = True
                            Sisa -= item.AllocQty
                        End If
                    Else
                        If item.AllocQty <> item.ProposeQty Then
                            item.AllocQty = item.ProposeQty
                            isChanged = True
                            Sisa -= item.AllocQty
                        End If
                    End If
                    'm_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    If isChanged AndAlso Sisa >= 0 Then
                        IsUpdated = True
                        objPODetailFacade.Update(item)
                    End If
                Next
                'PoDetailColl()
                'update atphistory
                If PoDetailColl.Count > 0 Then
                    Dim oPOD As PODetail = PoDetailColl(0)
                    Dim OldRowStatus As Short = oPOD.RowStatus
                    oPOD.RowStatus = 5
                    objPODetailFacade.Update(oPOD)
                    oPOD.RowStatus = OldRowStatus
                End If


                If 1 = 2 Then 'ALL ATPHISTORY LOGIC WILL BE MAINTAINED IN SP_ALLOCATION      IsUpdated = False Then ' PoDetailColl.Count < 1 Then
                    Dim cATPH As New CriteriaComposite(New Criteria(GetType(ATPHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim sATPH As New SortCollection
                    Dim dt As Date
                    Dim aATPHs As ArrayList
                    Dim oATPHFac As New ATPHistoryFacade(System.Threading.Thread.CurrentPrincipal)
                    Dim oATPH As ATPHistory

                    TotalPPQty = GetTotalPPqty(oPPQty)
                    dt = DateSerial(oPPQty.PeriodeYear, oPPQty.PeriodeMonth, oPPQty.PeriodeDate)

                    cATPH.opAnd(New Criteria(GetType(ATPHistory), "MaterialNumber", MatchType.Exact, oPPQty.MaterialNumber))
                    cATPH.opAnd(New Criteria(GetType(ATPHistory), "AllocationDate", MatchType.GreaterOrEqual, dt.ToString("yyyy/MM/dd 00:00:00")))
                    cATPH.opAnd(New Criteria(GetType(ATPHistory), "AllocationDate", MatchType.Lesser, dt.AddDays(1).ToString("yyyy/MM/dd 00:00:00")))
                    sATPH.Add(New Sort(GetType(ATPHistory), "ID", Sort.SortDirection.DESC))
                    aATPHs = oATPHFac.Retrieve(cATPH, sATPH)
                    If aATPHs.Count > 0 Then
                        oATPH = aATPHs(0)
                        oATPH.StokATP = TotalPPQty ' oATPH.StokATP + TotalPPQty
                        oATPH.StokSebelum = TotalPPQty ' oATPH.StokSebelum + TotalPPQty
                        oATPH.StokSesudah = TotalPPQty ' 
                        oATPHFac.Update(oATPH)
                    End If

                End If

            Next



            Return returnValue
        End Function

        Public Function RetrieveScalar(ByVal Criterias As ICriteria, ByVal aggregate As Aggregate) As Integer
            Dim obj As Object = m_PPQtyMapper.RetrieveScalar(aggregate, Criterias)
            If obj Is DBNull.Value Then
                Return 0
            Else
                Return CInt(obj)
            End If
        End Function

        Public Function DeleteFromDB(ByVal objDomain As PPQty) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PPQtyMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeletedPPQty(ByVal objDomain As KTB.DNet.Domain.PPQty) As Integer
            Dim returnValue As Integer = -1

            'Dim ppColl As ArrayList = RetrieveDeletedProposeQuantity(objDomain)

            'If ppColl.Count > 0 Then
            '    For Each objPPQty As PPQty In ppColl
            '        DeleteFromDB(objPPQty)
            '    Next
            'End If

            Dim objHelper As New PPQtyDeleteHelper
            objHelper.PeriodeDate = objDomain.PeriodeDate
            objHelper.PeriodeMonth = objDomain.PeriodeMonth
            objHelper.PeriodeYear = objDomain.PeriodeYear
            objHelper.ProductionYear = objDomain.ProductionYear
            objHelper.MaterialNumber = objDomain.MaterialNumber

            Dim result As Integer = New PPQtyDeleteHelperFacade(System.Threading.Thread.CurrentPrincipal).Insert(objHelper)

            Return returnValue
        End Function



#End Region

#Region "Custom Method"

        Public Function RetreivePODetails(ByVal periode As Date, ByVal dealerCode As String, ByVal materialNumber As String, ByVal productionYear As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _status As Integer = enumStatusPO.Status.Konfirmasi
            criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ContractHeader.Dealer.ID", MatchType.Exact, RetrieveDealer(dealerCode).ID))
            criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.ID", MatchType.Exact, RetrieveVehicleColor(materialNumber).ID))
            criterias.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.Exact, _status))
            criterias.opAnd(New Criteria(GetType(POHeader), "ContractHeader.ProductionYear", MatchType.Exact, productionYear))
            Return New PODetailFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
        End Function

        Public Function RetreivePODetailsAllDealer(ByVal periode As Date, ByVal materialNumber As String, ByVal productionYear As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _status As Integer = enumStatusPO.Status.Konfirmasi
            'criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ContractHeader.Dealer.ID", MatchType.Exact, RetrieveDealer(dealerCode).ID))
            criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.ID", MatchType.Exact, RetrieveVehicleColor(materialNumber).ID))
            criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.Exact, _status))
            criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ContractHeader.ProductionYear", MatchType.Exact, productionYear))
            Return New PODetailFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
        End Function

        Public Function RetrieveProposeQuantity(ByVal _ppqty As PPQty) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeDate", MatchType.Exact, _ppqty.PeriodeDate))
            criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeMonth", MatchType.Exact, _ppqty.PeriodeMonth))
            criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeYear", MatchType.Exact, _ppqty.PeriodeYear))
            criterias.opAnd(New Criteria(GetType(PPQty), "DealerCode", MatchType.Exact, _ppqty.DealerCode))
            criterias.opAnd(New Criteria(GetType(PPQty), "ProductionYear", MatchType.Exact, _ppqty.ProductionYear))
            criterias.opAnd(New Criteria(GetType(PPQty), "MaterialNumber", MatchType.Exact, _ppqty.MaterialNumber))
            Return New PPQtyFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
        End Function

        Private Function GetTotalPPqty(ByVal _ppqty As PPQty) As Integer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aPs As ArrayList
            Dim Total As Integer = 0
            Dim aPPQty As New Aggregate(GetType(PPQty), "AllocationQty", AggregateType.Sum)

            criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeDate", MatchType.Exact, _ppqty.PeriodeDate))
            criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeMonth", MatchType.Exact, _ppqty.PeriodeMonth))
            criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeYear", MatchType.Exact, _ppqty.PeriodeYear))
            'criterias.opAnd(New Criteria(GetType(PPQty), "DealerCode", MatchType.Exact, _ppqty.DealerCode))
            criterias.opAnd(New Criteria(GetType(PPQty), "ProductionYear", MatchType.Exact, _ppqty.ProductionYear))
            criterias.opAnd(New Criteria(GetType(PPQty), "MaterialNumber", MatchType.Exact, _ppqty.MaterialNumber))

            Try
                Total = New PPQtyFacade(System.Threading.Thread.CurrentPrincipal).RetrieveScalar(criterias, aPPQty)
            Catch ex As Exception
                Total = 0
            End Try
            Return Total
        End Function

        Private Function RetrieveDealer(ByVal code As String) As Dealer
            Return New DealerFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(code)
        End Function

        Private Function RetrieveVehicleColor(ByVal materialNumber As String) As VechileColor
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileColor), "MaterialNumber", MatchType.Exact, materialNumber))
            Dim al As ArrayList = New VechileColorFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
            If al.Count < 1 Then
                Throw New Exception("Material tidak Ditemukan.")
            End If
            Return CType(al(0), VechileColor)
        End Function

        Public Function RetrieveAlokasiStock(ByVal ID As Integer) As AlokasiStok_view
            Dim m_AlokasiStokMapper As IMapper
            m_AlokasiStokMapper = MapperFactory.GetInstance.GetMapper(GetType(AlokasiStok_view).ToString)
            Return CType(m_AlokasiStokMapper.Retrieve(ID), AlokasiStok_view)
        End Function

        Public Function RetrieveAlokasiStock(ByVal criterias As ICriteria) As ArrayList
            Dim m_AlokasiStokMapper As IMapper
            m_AlokasiStokMapper = MapperFactory.GetInstance.GetMapper(GetType(AlokasiStok_view).ToString)
            Return m_AlokasiStokMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveList(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(PPQty), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PPQtyMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveDeletedProposeQuantity(ByVal _ppqty As PPQty) As ArrayList
            ' Modified by Ikhsan 20081027
            ' to add function of criteria to delete past Proposed Allocation
            ' Requested by Rurike/Doni KTB
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeDate", MatchType.Exact, _ppqty.PeriodeDate))
            criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeMonth", MatchType.Exact, _ppqty.PeriodeMonth))
            criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeYear", MatchType.Exact, _ppqty.PeriodeYear))
            'criterias.opAnd(New Criteria(GetType(PPQty), "DealerCode", MatchType.Exact, _ppqty.DealerCode))
            criterias.opAnd(New Criteria(GetType(PPQty), "ProductionYear", MatchType.Exact, _ppqty.ProductionYear))
            criterias.opAnd(New Criteria(GetType(PPQty), "MaterialNumber", MatchType.Exact, _ppqty.MaterialNumber))
            Return New PPQtyFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
        End Function

#End Region

    End Class

End Namespace