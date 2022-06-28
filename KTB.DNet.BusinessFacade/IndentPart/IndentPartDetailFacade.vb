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
'// Generated on 8/2/2007 - 1:07:49 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Reflection
#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.IndentPart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
#End Region

Namespace KTB.DNET.BusinessFacade.IndentPart

    Public Class IndentPartDetailFacade
        Inherits AbstractFacade
#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_IndentPartDetailMapper As IMapper
        Private m_IndentPartHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_IndentPartDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(IndentPartDetail).ToString)
            Me.m_IndentPartHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(IndentPartHeader).ToString)
            Me.m_TransactionManager = New TransactionManager


        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveByHeaderID_SPMasterID(ByVal intHeaderId As Integer, ByVal intSPMasterID As Integer) As IndentPartDetail
            Dim objHeader As IndentPartHeader = Me.m_IndentPartHeaderMapper.Retrieve(intHeaderId)
            For Each objDetail As IndentPartDetail In objHeader.IndentPartDetails
                If (objDetail.SparePartMaster.ID = intSPMasterID) Then
                    Return objDetail
                End If
            Next
            Return Nothing
        End Function

        Public Function Retrieve(ByVal ID As Integer) As IndentPartDetail
            Return CType(m_IndentPartDetailMapper.Retrieve(ID), IndentPartDetail)
        End Function

        'Public Function Retrieve(ByVal Code As String) As IndentPartDetail
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartDetailCode", MatchType.Exact, Code))

        '    Dim IndentPartDetailColl As ArrayList = m_IndentPartDetailMapper.RetrieveByCriteria(criterias)
        '    If (IndentPartDetailColl.Count > 0) Then
        '        Return CType(IndentPartDetailColl(0), IndentPartDetail)
        '    End If
        '    Return New IndentPartDetail
        'End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_IndentPartDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_IndentPartDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_IndentPartDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(IndentPartDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_IndentPartDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(IndentPartDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_IndentPartDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _IndentPartDetail As ArrayList = m_IndentPartDetailMapper.RetrieveByCriteria(criterias)
            Return _IndentPartDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim IndentPartDetailColl As ArrayList = m_IndentPartDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return IndentPartDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(IndentPartDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim IndentPartDetailColl As ArrayList = m_IndentPartDetailMapper.RetrieveByCriteria(criterias, sortColl)
            Return IndentPartDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim IndentPartDetailColl As ArrayList = m_IndentPartDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return IndentPartDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim IndentPartDetailColl As ArrayList = m_IndentPartDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), columnName, matchOperator, columnValue))
            Return IndentPartDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(IndentPartDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), columnName, matchOperator, columnValue))

            Return m_IndentPartDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(IndentPartDetail), sortColumn, sortDirection))

                If sortColumn.ToUpper() = "IndentPartHeader.TermOfPayment.ID".ToUpper() Then
                    Dim sSQL As String = GetRetrieveSpSortByTOP(Criterias, sortColl, pageNumber, pageSize, totalRow)
                    Dim result As ArrayList = m_IndentPartDetailMapper.RetrieveSP(sSQL)
                    totalRow = GetRowCount(Criterias)
                    Return result
                End If

            Else
                sortColl = Nothing
            End If
            Dim IndentPartDetailColl As ArrayList = m_IndentPartDetailMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return IndentPartDetailColl
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function GeneratePO(ByVal arlDetail As ArrayList) As String
            Dim ReturnValue As String = ""
            Dim arlHeader As ArrayList = New ArrayList
            'Dim dealerCode As String = ""
            Dim IdHeader As Integer = 0

            'Get Array Header PO based on Dealer
            For Each IPDetail As IndentPartDetail In arlDetail
                'If dealerCode <> IPDetail.IndentPartHeader.Dealer.DealerCode Then
                If IdHeader <> IPDetail.IndentPartHeader.ID Then
                    'Dim tmpDealerCode As String = IPDetail.IndentPartHeader.Dealer.DealerCode
                    'dealerCode = IPDetail.IndentPartHeader.Dealer.DealerCode
                    Dim tmpIdHeader As String = IPDetail.IndentPartHeader.ID
                    IdHeader = IPDetail.IndentPartHeader.ID
                    'Dim objdealer As Dealer = New DealerFacade(m_userPrincipal).Retrieve(dealerCode)
                    Dim objdealer As Dealer = IPDetail.IndentPartHeader.Dealer
                    Dim objPOHeader As SparePartPO = New SparePartPO
                    'objPOHeader.Dealer = New DealerFacade(m_userPrincipal).Retrieve(dealerCode)
                    objPOHeader.TempIDIndentPartHeader = IdHeader
                    objPOHeader.Dealer = objdealer
                    objPOHeader.PODate = Date.Today
                    objPOHeader.OrderType = "I"
                    If Not IsNothing(IPDetail.IndentPartHeader.TermOfPayment) Then
                        objPOHeader.TermOfPayment = IPDetail.IndentPartHeader.TermOfPayment
                    End If
                    objPOHeader.DMSPRNo = IPDetail.IndentPartHeader.DMSPRNo
                    arlHeader.Add(objPOHeader)
                End If
            Next

            Dim arlIPPO As ArrayList = New ArrayList
            For Each objHeader As SparePartPO In arlHeader
                Dim arlPODetail As ArrayList = New ArrayList
                For Each IPDetail As IndentPartDetail In arlDetail
                    'If IPDetail.IndentPartHeader.Dealer.DealerCode = objHeader.Dealer.DealerCode Then
                    If IPDetail.IndentPartHeader.ID = objHeader.TempIDIndentPartHeader Then

                        Dim objPODetail As SparePartPODetail = New SparePartPODetail
                        Dim objIPPO As IndentPartPO = New IndentPartPO
                        objPODetail.SparePartPO = objHeader
                        objPODetail.Quantity = IPDetail.AllocationQty
                        objPODetail.RetailPrice = IPDetail.Price
                        objPODetail.SparePartMaster = IPDetail.SparePartMaster
                        objPODetail.TotalForecast = IPDetail.TotalForecast
                        arlPODetail.Add(objPODetail)
                        objIPPO.IndentPartDetail = IPDetail
                        objIPPO.SparePartPODetail = objPODetail
                        arlIPPO.Add(objIPPO)
                    End If
                Next

                Dim objTransactionManager As TransactionManager
                objTransactionManager = New TransactionManager
                AddHandler objTransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

                If (Me.IsTaskFree()) Then
                    Try
                        Me.SetTaskLocking()
                        objTransactionManager.AddInsert(objHeader, m_userPrincipal.Identity.Name)
                        For Each PODetail As SparePartPODetail In arlPODetail
                            PODetail.SparePartPO = objHeader
                            objTransactionManager.AddInsert(PODetail, m_userPrincipal.Identity.Name)
                            'Dim objIPDetail As IndentPartDetail = PODetail.IndentPartDetail
                            'If objIPDetail.Qty = objIPDetail.AllocationQty + objIPDetail.POQty Then
                            '    objIPDetail.IsCompletedAllocation = 1
                            'End If
                            'objIPDetail.AllocationQty = 0
                            'objTransactionManager.AddUpdate(objIPDetail, m_userPrincipal.Identity.Name)
                        Next


                        For Each IPPO As IndentPartPO In arlIPPO
                            objTransactionManager.AddInsert(IPPO, m_userPrincipal.Identity.Name)
                            Dim objIPDetail As IndentPartDetail = IPPO.IndentPartDetail
                            If objIPDetail.Qty = objIPDetail.AllocationQty + objIPDetail.POQty Then
                                objIPDetail.IsCompletedAllocation = 1
                            ElseIf objIPDetail.AllocationQty > 0 And objIPDetail.AllocationQty < objIPDetail.Qty Then
                                objIPDetail.IsCompletedAllocation = 2
                            End If
                            objIPDetail.AllocationQty = 0
                            objTransactionManager.AddUpdate(objIPDetail, m_userPrincipal.Identity.Name)
                        Next


                        objTransactionManager.PerformTransaction()
                        ReturnValue = ReturnValue & ";" & New SparePartPOFacade(m_userPrincipal).Retrieve(objHeader.ID).PONumber

                    Catch ex As Exception
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                        If rethrow Then
                            Throw
                        End If
                    Finally
                        Me.RemoveTaskLocking()
                    End Try
                End If

            Next

            'Update Status if Complete
            arlDetail = SortArraylist(arlDetail, GetType(IndentPartDetail), "IndentPartHeader.ID", Sort.SortDirection.ASC)
            Dim tmpHeaderID As String = ""
            Dim arlIPHeader As ArrayList = New ArrayList
            For Each IPDetail As IndentPartDetail In arlDetail
                If IPDetail.IndentPartHeader.ID.ToString <> tmpHeaderID Then
                    Dim objIPHeader As IndentPartHeader = IPDetail.IndentPartHeader
                    arlIPHeader.Add(objIPHeader)
                    tmpHeaderID = IPDetail.IndentPartHeader.ID.ToString
                End If
            Next

            For Each itemIPHeader As IndentPartHeader In arlIPHeader
                Dim intAllocQty As Integer = 0
                Dim intEquipment As Integer = 0
                Dim isAllocHalf As Boolean = False

                For Each itemIPDetail As IndentPartDetail In itemIPHeader.IndentPartDetails
                    If (itemIPDetail.IsCompletedAllocation = 1) Then
                        intAllocQty += 1
                    ElseIf (itemIPDetail.IsCompletedAllocation = 2) Then
                        isAllocHalf = True
                    End If

                    If itemIPDetail.SparePartMaster.TypeCode.ToLower() = EnumEstimationEquipStatus.TYPE_EQUIPMENT.ToLower() Then
                        intEquipment += 1
                    End If
                Next

                If (intEquipment = itemIPHeader.IndentPartDetails.Count) Then
                    If isAllocHalf Then

                        'itemIPHeader.Status = EnumIndentPartStatus.IndentPartStatus.ALOKASI_SEBAGIAN
                        'itemIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi
                        itemIPHeader.Status = EnumIndentPartEquipStatus.EnumStatusDealer.Proses
                        itemIPHeader.StatusKTB = EnumIndentPartEquipStatus.EnumStatusKTB.Proses_Order

                        Dim result As Integer = New IndentPartHeaderFacade(m_userPrincipal).Update(itemIPHeader)
                    Else
                        If intAllocQty = itemIPHeader.IndentPartDetails.Count Then
                            'itemIPHeader.Status = EnumIndentPartStatus.IndentPartStatus.SELESAI
                            'itemIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Selesai

                            itemIPHeader.Status = EnumIndentPartEquipStatus.EnumStatusDealer.Selesai
                            itemIPHeader.StatusKTB = EnumIndentPartEquipStatus.EnumStatusKTB.Selesai
                            Dim result As Integer = New IndentPartHeaderFacade(m_userPrincipal).Update(itemIPHeader)
                        End If
                    End If
                Else
                    If intAllocQty = itemIPHeader.IndentPartDetails.Count Then
                        itemIPHeader.Status = EnumIndentPartStatus.IndentPartStatusDealer.Selesai
                        itemIPHeader.StatusKTB = EnumIndentPartStatus.IndentPartStatusKTB.Selesai
                        Dim result As Integer = New IndentPartHeaderFacade(m_userPrincipal).Update(itemIPHeader)
                    End If
                End If
            Next

            'start  :rename PONumber,for:Yurike, Hendriyanto;on:20120904;by:dna
            Dim oSPPOFac As New SparePartPOFacade(m_userPrincipal)
            Dim iSuccess As Integer
            ReturnValue = String.Empty
            For Each oSPPO As SparePartPO In arlHeader
                oSPPO = oSPPOFac.Retrieve(oSPPO.ID)
                iSuccess = oSPPOFac.Update(oSPPO) 'Update PONumber
                oSPPO = oSPPOFac.Retrieve(oSPPO.ID)

                ReturnValue &= IIf(ReturnValue = String.Empty, "", ";") & oSPPO.PONumber
            Next
            'end    :rename PONumber,for:Yurike, Hendriyanto;on:20120904;by:dna
            Return ReturnValue

        End Function


        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "IndentPartDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(IndentPartDetail), "IndentPartDetailCode", AggregateType.Count)
            Return CType(m_IndentPartDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As IndentPartDetail) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_IndentPartDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As IndentPartDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_IndentPartDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As IndentPartDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_IndentPartDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As IndentPartDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_IndentPartDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Function UpdateAlokasi(ByVal arlToUpdate As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arlToUpdate.Count > 0 Then
                        For Each objIPDetail As IndentPartDetail In arlToUpdate
                            m_TransactionManager.AddUpdate(objIPDetail, m_userPrincipal.Identity.Name)
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


        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SparePartPO) Then
                CType(InsertArg.DomainObject, SparePartPO).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SparePartPO).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is SparePartPODetail) Then
                CType(InsertArg.DomainObject, SparePartPODetail).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SparePartPODetail).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is IndentPartPO) Then
                CType(InsertArg.DomainObject, IndentPartPO).ID = InsertArg.ID

            End If
        End Sub

#End Region

#Region "Custom Method"
        Public Function ValidateItem(ByVal nPOID As Integer, ByVal strPartNumber As String) As IndentPartDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader", MatchType.Exact, nPOID))
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "SparePartMaster.PartNumber", MatchType.Exact, strPartNumber))
            Dim arlIPDetail As ArrayList = m_IndentPartDetailMapper.RetrieveByCriteria(criterias)
            If arlIPDetail.Count > 0 Then
                Return CType(arlIPDetail(0), IndentPartDetail)

            Else
                Return Nothing
            End If
        End Function

        Private Function SortArraylist(ByVal ArlToSort As ArrayList, ByVal ObjType As Type, ByVal SortColumn As String, ByVal SortDirection As Sort.SortDirection) As ArrayList

            Dim isDeepSort As Boolean = (SortColumn.IndexOf(".") <> -1)

            Dim i As Integer
            Dim x, y As Object
            Dim currentValue, prevValue As Object
            For i = 0 To ArlToSort.Count - 1
                If i >= 1 Then
                    If isDeepSort Then 'Only for 2 level max
                        Dim Properties() As String = SortColumn.Split((".").ToCharArray())
                        Dim dummyType As Type = ObjType
                        Dim currentDummyObject As Object
                        Dim prevDummyObject As Object

                        For z As Integer = 0 To Properties.Length - 2
                            currentDummyObject = dummyType.GetProperty(Properties(z)).GetValue(ArlToSort(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                            prevDummyObject = dummyType.GetProperty(Properties(z)).GetValue(ArlToSort(i - 1), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)

                            dummyType = dummyType.GetProperty(Properties(z)).PropertyType
                        Next
                        Dim prop As PropertyInfo
                        prop = dummyType.GetProperty(Properties(Properties.Length - 1))

                        currentValue = dummyType.GetProperty(Properties(Properties.Length - 1)).GetValue(currentDummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                        prevValue = dummyType.GetProperty(Properties(Properties.Length - 1)).GetValue(prevDummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                    Else
                        currentValue = ObjType.GetProperty(SortColumn).GetValue(ArlToSort(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                        prevValue = ObjType.GetProperty(SortColumn).GetValue(ArlToSort(i - 1), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                    End If

                    If SortDirection = Sort.SortDirection.ASC Then
                        If currentValue < prevValue Then
                            x = ArlToSort(i)
                            y = ArlToSort(i - 1)
                            ArlToSort(i) = y
                            ArlToSort(i - 1) = x
                            i = 0
                        End If
                    Else
                        If currentValue > prevValue Then
                            x = ArlToSort(i)
                            y = ArlToSort(i - 1)
                            ArlToSort(i) = y
                            ArlToSort(i - 1) = x
                            i = 0
                        End If
                    End If
                End If
            Next

            Return ArlToSort

        End Function

        Private Function GetRetrieveSpSortByTOP(ByVal Criterias As ICriteria, ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As String
            Dim sSQL As String = "EXEC up_PagingQuery "
            sSQL &= "@Table = N'IndentPartDetail', "
            sSQL &= "@PK = N'ID', "
            sSQL &= "@PageSize = " & pageSize & ", "
            sSQL &= "@PageNumber = " & pageNumber & ", "
            sSQL &= "@Filter = N' INNER JOIN IndentPartHeader ON IndentPartDetail.IndentPartHeaderID = IndentPartHeader.ID LEFT JOIN TermOfPayment ON IndentPartHeader.TermOfPaymentID = TermOfPayment.ID "
            sSQL &= Criterias.ToString().Replace("'", "''").Replace("INNER JOIN IndentPartHeader ON IndentPartDetail.IndentPartHeaderID = IndentPartHeader.ID", "")
            sSQL &= "', @Sort = N'" & Sorts.ToString() & "'"

            Return sSQL
        End Function

        Private Function GetRowCount(ByVal Criterias As ICriteria) As Integer
            Dim agg As Aggregate = New Aggregate(GetType(IndentPartDetail), "ID", AggregateType.Count)

            Return CType(m_IndentPartDetailMapper.RetrieveScalar(agg, Criterias), Integer)
        End Function
#End Region

    End Class

End Namespace

