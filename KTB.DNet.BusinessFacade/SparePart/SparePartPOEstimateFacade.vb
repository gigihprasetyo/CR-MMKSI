
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
'// Generated on 10/27/2015 - 3:30:29 PM
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
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class SparePartPOEstimateFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartPOEstimateMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartPOEstimateMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartPOEstimate).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(SparePartPOEstimate))
            Me.DomainTypeCollection.Add(GetType(SparePartPOEstimateDetail))
            Me.DomainTypeCollection.Add(GetType(SparePartPO))
            Me.DomainTypeCollection.Add(GetType(SparePartPODetail))


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartPOEstimate
            Return CType(m_SparePartPOEstimateMapper.Retrieve(ID), SparePartPOEstimate)
        End Function

        Public Function Retrieve(ByVal sONumber As String) As SparePartPOEstimate
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOEstimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "SONumber", MatchType.Exact, sONumber))
            Dim arlPOEst As ArrayList = m_SparePartPOEstimateMapper.RetrieveByCriteria(criterias)
            If arlPOEst.Count > 0 Then
                Return CType(arlPOEst(0), SparePartPOEstimate)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartPOEstimateMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartPOEstimateMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartPOEstimateMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOEstimate), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPOEstimateMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOEstimate), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPOEstimateMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOEstimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartPOEstimate As ArrayList = m_SparePartPOEstimateMapper.RetrieveByCriteria(criterias)
            Return _SparePartPOEstimate
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOEstimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPOEstimateColl As ArrayList = m_SparePartPOEstimateMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartPOEstimateColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartPOEstimateColl As ArrayList = m_SparePartPOEstimateMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartPOEstimateColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(SparePartPOEstimate), SortColumn, sortDirection))

            If SortColumn.ToUpper() = "SparePartPO.TermOfPayment.ID".ToUpper() Then
                Dim sSQL As String = GetRetrieveSpSortByTOP(criterias, sortColl, pageNumber, pageSize, totalRow)
                Dim result As ArrayList = m_SparePartPOEstimateMapper.RetrieveSP(sSQL)
                totalRow = GetRowCount(criterias)
                Return result
            End If

            Dim SparePartPOEstimateColl As ArrayList = m_SparePartPOEstimateMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartPOEstimateColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOEstimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPOEstimateColl As ArrayList = m_SparePartPOEstimateMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), columnName, matchOperator, columnValue))
            Return SparePartPOEstimateColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOEstimate), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOEstimate), columnName, matchOperator, columnValue))

            Return m_SparePartPOEstimateMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOEstimate), "SparePartPOEstimateCode", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(SparePartPOEstimate), "SparePartPOEstimateCode", AggregateType.Count)
        '    Return CType(m_SparePartPOEstimateMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

        Public Function Insert(ByVal objDomain As SparePartPOEstimate) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SparePartPOEstimateMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartPOEstimate) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartPOEstimateMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartPOEstimate)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartPOEstimateMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartPO)
            Try
                m_SparePartPOEstimateMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
        Public Function ValidateSPPO(ByVal SparePartPOEstimate As SparePartPOEstimate) As SparePartPOEstimate
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOEstimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOEstimate), "SONumber", MatchType.Exact, SparePartPOEstimate.SONumber))
            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOEstimate), "SparePartPO.ID", MatchType.Exact, SparePartPOEstimate.SparePartPO.ID))
            Dim arlSPPOEstimate As ArrayList = m_SparePartPOEstimateMapper.RetrieveByCriteria(criteria)
            If arlSPPOEstimate.Count > 0 Then
                Return CType(arlSPPOEstimate(0), SparePartPOEstimate)
            End If
            Return Nothing

        End Function

        Private Function GetItemPOEstimet(ByVal spMasterNumber As String, ByVal arlOrigSPPODEstimetDetail As ArrayList) As SparePartPOEstimateDetail
            For Each itemDetail As SparePartPOEstimateDetail In arlOrigSPPODEstimetDetail
                If itemDetail.PartNumber = spMasterNumber Then
                    Return itemDetail
                End If
            Next
            Return Nothing
        End Function

        Private Function MergeDataSPPOEstimate(ByRef objOrigSPPOEstimateDetails As ArrayList, ByVal objNewSPPOEstimateDetails As ArrayList)
            For Each objItemDetail As SparePartPOEstimateDetail In objOrigSPPOEstimateDetails
                objItemDetail.RowStatus = DBRowStatus.Deleted
            Next
            For Each itemDetail As SparePartPOEstimateDetail In objNewSPPOEstimateDetails
                Dim objOriItemDetail As SparePartPOEstimateDetail = GetItemPOEstimet(itemDetail.PartNumber, objOrigSPPOEstimateDetails)
                If Not IsNothing(objOriItemDetail) Then
                    objOriItemDetail.RowStatus = DBRowStatus.Active
                    objOriItemDetail.AllocQty = itemDetail.AllocQty
                    objOriItemDetail.AltPartNumber = itemDetail.AltPartNumber
                    objOriItemDetail.ItemStatus = itemDetail.ItemStatus
                    objOriItemDetail.OrderQty = itemDetail.OrderQty
                    objOriItemDetail.PartName = itemDetail.PartName
                    objOriItemDetail.PartNumber = itemDetail.PartNumber
                    objOriItemDetail.RetailPrice = itemDetail.RetailPrice
                Else
                    objOrigSPPOEstimateDetails.Add(itemDetail)
                End If
            Next
        End Function

        Public Function InsertFromWindowSevice(ByVal spPOHeader As SparePartPOEstimate) As Short
            Dim returnValue As Integer = -1
            Dim isChange As New IsChangeFacade

            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    Dim spPOValidate As SparePartPOEstimate = ValidateSPPO(spPOHeader)
                    Dim spPO As SparePartPO = spPOHeader.SparePartPO
                    spPO.ProcessCode = "P"
                    If IsNothing(spPOValidate) Then
                        m_TransactionManager.AddInsert(spPOHeader, m_userPrincipal.Identity.Name)
                        For Each itemDetail As SparePartPOEstimateDetail In spPOHeader.SparePartPOEstimateDetails
                            itemDetail.SparePartPOEstimate = spPOHeader
                            m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        spPOHeader.ID = spPOValidate.ID
                        Dim arlSPPOStatusDetail As ArrayList = MergeDataSPPOEstimate(spPOValidate.SparePartPOEstimateDetails, spPOHeader.SparePartPOEstimateDetails)
                        For Each oSPPOED As SparePartPOEstimateDetail In spPOHeader.SparePartPOEstimateDetails
                            'start  :by:dna
                            Dim aSPPOED As ArrayList
                            Dim cSSPOED As New CriteriaComposite(New Criteria(GetType(SparePartPOEstimateDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            Dim oSSPOEDFac As New SparePartPOEstimateDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            Dim oSSPOEDInDB As SparePartPOEstimateDetail

                            cSSPOED.opAnd(New Criteria(GetType(SparePartPOEstimateDetail), "SparePartPOEstimate.ID", MatchType.Exact, spPOHeader.ID))
                            cSSPOED.opAnd(New Criteria(GetType(SparePartPOEstimateDetail), "PartNumber", MatchType.Exact, oSPPOED.PartNumber))
                            cSSPOED.opAnd(New Criteria(GetType(SparePartPOEstimateDetail), "PartName", MatchType.Exact, oSPPOED.PartName))

                            aSPPOED = oSSPOEDFac.Retrieve(cSSPOED)
                            If aSPPOED.Count > 0 Then
                                oSSPOEDInDB = CType(aSPPOED(0), SparePartPOEstimateDetail)

                                'farid additional 20180315
                                If isChange.ISchangeSparePartPOEstimateDetail(oSPPOED, oSSPOEDInDB) Then
                                    oSSPOEDInDB.PartName = oSPPOED.PartName
                                    oSSPOEDInDB.PartNumber = oSPPOED.PartNumber
                                    oSSPOEDInDB.RetailPrice = oSPPOED.RetailPrice
                                    oSSPOEDInDB.OrderQty = oSPPOED.OrderQty
                                    oSSPOEDInDB.AllocQty = oSPPOED.AllocQty
                                    oSSPOEDInDB.AltPartNumber = oSPPOED.AltPartNumber
                                    oSSPOEDInDB.Discount = oSPPOED.Discount
                                    oSSPOEDInDB.CreatedBy = oSPPOED.CreatedBy

                                    m_TransactionManager.AddUpdate(oSSPOEDInDB, m_userPrincipal.Identity.Name)
                                End If


                            Else
                                oSSPOEDInDB = oSPPOED
                                'Bug Fix 2015-06-18 
                                oSSPOEDInDB.SparePartPOEstimate = spPOHeader
                                'End of Bug Fix 2015-06-18 
                                m_TransactionManager.AddInsert(oSSPOEDInDB, m_userPrincipal.Identity.Name)
                            End If
                            'end    :by:dna
                        Next
                        'For Each itemDetail As SparePartPOEstimateDetail In spPOValidate.SparePartPOEstimateDetails
                        '    itemDetail.SparePartPOEstimate = spPOHeader
                        '    If itemDetail.ID = 0 Then
                        '        m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                        '    Else
                        '        m_TransactionManager.AddUpdate(itemDetail, m_userPrincipal.Identity.Name)
                        '    End If
                        'Next
                        If isChange.ISchangeSparePartPOEstimate(spPOHeader, spPOValidate) Then
                            spPOHeader.CreatedBy = spPOValidate.CreatedBy
                            m_TransactionManager.AddUpdate(spPOHeader, m_userPrincipal.Identity.Name)
                        End If
                        
                    End If

                    'farid additional 20181030
                    'If isChange.ISchangeSparePartPO() Then
                    '    m_TransactionManager.AddUpdate(spPO, m_userPrincipal.Identity.Name)
                    'End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = 0
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    'Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function UpdatePOEstimateSync(ByVal spPOEstimate As SparePartPOEstimate) As String
            Dim returnValue As String = String.Empty
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    For Each itemDetail As SparePartPOEstimateDetail In spPOEstimate.SparePartPOEstimateDetails
                        Dim spPODetail As SparePartPODetail = New SparePartPODetailFacade(System.Threading.Thread.CurrentPrincipal).ValidateItem(spPOEstimate.SparePartPO.ID, itemDetail.PartNumber)
                        If Not IsNothing(spPODetail) Then
                            Dim objSparePart As SparePartMaster = New SparePartMasterFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(itemDetail.PartNumber)
                            If IsNothing(objSparePart) Then
                                Dim objSPPODetail As SparePartPODetail = New SparePartPODetail
                                objSPPODetail.SparePartPO = spPOEstimate.SparePartPO
                                objSPPODetail.SparePartMaster = objSparePart
                                objSPPODetail.Quantity = itemDetail.AllocQty
                                objSPPODetail.RetailPrice = itemDetail.RetailPrice
                                objSPPODetail.CheckListStatus = "1"
                                objSPPODetail.EstimateStatus = "I"
                                objSPPODetail.CreatedBy = "WSM"
                                m_TransactionManager.AddInsert(objSPPODetail, System.Threading.Thread.CurrentPrincipal.Identity.Name)
                            Else
                                If spPODetail.Quantity <> itemDetail.AllocQty Then
                                    spPODetail.Quantity = itemDetail.AllocQty
                                    If itemDetail.AllocQty = 0 Then
                                        spPODetail.EstimateStatus = "D"

                                    Else
                                        spPODetail.EstimateStatus = "U"
                                    End If
                                    m_TransactionManager.AddUpdate(spPODetail, System.Threading.Thread.CurrentPrincipal.Identity.Name)
                                End If

                            End If
                        Else
                            returnValue = itemDetail.PartNumber.Trim
                            Exit For
                        End If
                    Next
                    If returnValue = String.Empty Then
                        spPOEstimate.SparePartPO.ProcessCode = "P"
                        m_TransactionManager.AddUpdate(spPOEstimate.SparePartPO, System.Threading.Thread.CurrentPrincipal.Identity.Name)
                        m_TransactionManager.PerformTransaction()
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    returnValue = "Transaction Error"
                    If rethrow Then
                        Throw
                    End If
                Finally
                    'Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SparePartPOEstimate) Then
                CType(InsertArg.DomainObject, SparePartPOEstimate).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SparePartPOEstimate).MarkLoaded()
            End If
        End Sub


#End Region

#Region "Custom Method"

        Public Function GetAggregateResult(ByVal aggregate As IAggregate, ByVal criteria As ICriteria) As Decimal
            Dim result As Object = m_SparePartPOEstimateMapper.RetrieveScalar(aggregate, criteria)
            If result Is System.DBNull.Value Then
                Return 0
            Else
                Return CType(result, Decimal)
            End If
        End Function

        Public Function ValidateSparePartPOEStimate(ByVal SparePartPOEStimate As SparePartPOEstimate) As SparePartPOEstimate
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOEStimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(KTB.DNET.Domain.SparePartPOEstimate), "SONumber", MatchType.Exact, SparePartPOEStimate.SONumber))
            Dim arlSparePartPOEStimate As ArrayList = m_SparePartPOEstimateMapper.RetrieveByCriteria(criteria)
            If arlSparePartPOEStimate.Count > 0 Then
                Return CType(arlSparePartPOEStimate(0), SparePartPOEStimate)
            End If
            Return Nothing

        End Function

        Public Function InsertFromWebSevice(ByVal spEst As SparePartPOEstimate) As Short
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim spEst_old As SparePartPOEstimate = ValidateSparePartPOEStimate(spEst)
                    If IsNothing(spEst_old) Then
                        If spEst.SparePartPOEstimateDetails.Count > 0 Then
                            For Each itemDetail As SparePartPOEstimateDetail In spEst.SparePartPOEstimateDetails
                                itemDetail.SparePartPOEstimate = spEst
                                m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                        m_TransactionManager.AddInsert(spEst, m_userPrincipal.Identity.Name)
                    Else
                        'Set detail to rowstatus = -1
                        Dim objDetailFacade As SparePartPOEstimateDetailFacade = New SparePartPOEstimateDetailFacade(Me.m_userPrincipal)
                        For Each old_ItemDetail As SparePartPOEstimateDetail In spEst_old.SparePartPOEstimateDetails
                            old_ItemDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(old_ItemDetail, m_userPrincipal.Identity.Name)
                            'objDetailFacade.Delete(old_ItemDetail)
                        Next

                        'update detail to rowstatus = 0 if exist in new list
                        For Each itemDetail As SparePartPOEstimateDetail In spEst.SparePartPOEstimateDetails

                            Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartPOEstimateDetail), "PartNumber", MatchType.Exact, itemDetail.PartNumber))
                            criterias.opAnd(New Criteria(GetType(SparePartPOEstimateDetail), "SparePartPOEstimate.ID", MatchType.Exact, spEst_old.ID))

                            Dim arlItemDetails As ArrayList = New SparePartPOEstimateDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If Not IsNothing(arlItemDetails) AndAlso arlItemDetails.Count > 0 Then
                                Dim old_ItemDetail As New SparePartPOEstimateDetail
                                old_ItemDetail = CType(arlItemDetails(0), SparePartPOEstimateDetail)
                                old_ItemDetail.PartName = itemDetail.PartName
                                old_ItemDetail.OrderQty = itemDetail.OrderQty
                                old_ItemDetail.AllocQty = itemDetail.AllocQty
                                old_ItemDetail.AllocationQty = itemDetail.AllocationQty
                                old_ItemDetail.OpenQty = itemDetail.OpenQty
                                old_ItemDetail.RetailPrice = itemDetail.RetailPrice
                                old_ItemDetail.AltPartNumber = itemDetail.AltPartNumber
                                old_ItemDetail.Discount = itemDetail.Discount
                                old_ItemDetail.RowStatus = CType(DBRowStatus.Active, Short)

                                m_TransactionManager.AddUpdate(old_ItemDetail, m_userPrincipal.Identity.Name)
                            Else

                                itemDetail.SparePartPOEstimate = spEst_old
                                m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            End If

                        Next

                        spEst_old.SparePartPO = spEst.SparePartPO
                        spEst_old.SODate = spEst.SODate
                        spEst_old.DeliveryDate = spEst.DeliveryDate
                        spEst_old.DocumentType = spEst.DocumentType

                        m_TransactionManager.AddUpdate(spEst_old, m_userPrincipal.Identity.Name)

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
                    'Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function


        Public Function DeleteFromWebSevice(ByVal spSO As SparePartPOEstimate) As Short
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    Dim performtransaction As Boolean = True
                    Dim objmapper As IMapper

                    Dim spSO_DB As SparePartPOEstimate = ValidateSparePartPOEStimate(spSO)
                    If Not IsNothing(spSO_DB) Then
                        For Each itemDetail As SparePartPOEstimateDetail In spSO_DB.SparePartPOEstimateDetails
                            itemDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(itemDetail, m_userPrincipal.Identity.Name)
                        Next
                        spSO_DB.RowStatus = CType(DBRowStatus.Deleted, Short)
                        m_TransactionManager.AddUpdate(spSO_DB, m_userPrincipal.Identity.Name)

                        If performtransaction Then
                            m_TransactionManager.PerformTransaction()
                            returnValue = 0
                        End If
                    End If

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    'Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Private Function GetRetrieveSpSortByTOP(ByVal Criterias As ICriteria, ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As String
            Dim sSQL As String = "EXEC up_PagingQuery "
            sSQL &= "@Table = N'SparePartPOEstimate', "
            sSQL &= "@PK = N'ID', "
            sSQL &= "@PageSize = " & pageSize & ", "
            sSQL &= "@PageNumber = " & pageNumber & ", "
            sSQL &= "@Filter = N' INNER JOIN SparePartPO ON SparePartPOEstimate.SparePartPOID = SparePartPO.ID LEFT JOIN TermOfPayment ON SparePartPO.TermOfPaymentID = TermOfPayment.ID "
            sSQL &= Criterias.ToString().Replace("'", "''").Replace("INNER JOIN SparePartPO ON SparePartPOEstimate.SparePartPOID = SparePartPO.ID", "")
            sSQL &= "', @Sort = N'" & Sorts.ToString() & "'"

            Return sSQL
        End Function

        Private Function GetRowCount(ByVal Criterias As ICriteria) As Integer
            Dim agg As Aggregate = New Aggregate(GetType(SparePartPOEstimate), "ID", AggregateType.Count)

            Return CType(m_SparePartPOEstimateMapper.RetrieveScalar(agg, Criterias), Integer)
        End Function

#End Region

    End Class

End Namespace

