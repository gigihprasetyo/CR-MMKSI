
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
'// Generated on 10/23/2015 - 10:22:57 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Linq

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.SparePart

#End Region

Namespace KTB.DNet.BusinessFacade

    Public Class SparePartOutstandingOrderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartOutstandingOrderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartOutstandingOrderMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartOutstandingOrder).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(SparePartOutstandingOrder))
            Me.DomainTypeCollection.Add(GetType(SparePartOutstandingOrderDetail))
            Me.DomainTypeCollection.Add(GetType(SparePartPO))
            Me.DomainTypeCollection.Add(GetType(SparePartPODetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartOutstandingOrder
            Return CType(m_SparePartOutstandingOrderMapper.Retrieve(ID), SparePartOutstandingOrder)
        End Function

        Public Function Retrieve(ByVal Code As String) As SparePartOutstandingOrder
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrder), "SparePartOutstandingOrderCode", MatchType.Exact, Code))

            Dim SparePartOutstandingOrderColl As ArrayList = m_SparePartOutstandingOrderMapper.RetrieveByCriteria(criterias)
            If (SparePartOutstandingOrderColl.Count > 0) Then
                Return CType(SparePartOutstandingOrderColl(0), SparePartOutstandingOrder)
            End If
            Return New SparePartOutstandingOrder
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartOutstandingOrderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartOutstandingOrderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartOutstandingOrderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartOutstandingOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartOutstandingOrderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartOutstandingOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartOutstandingOrderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartOutstandingOrder As ArrayList = m_SparePartOutstandingOrderMapper.RetrieveByCriteria(criterias)
            Return _SparePartOutstandingOrder
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartOutstandingOrderColl As ArrayList = m_SparePartOutstandingOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartOutstandingOrderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartOutstandingOrderColl As ArrayList = m_SparePartOutstandingOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartOutstandingOrderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartOutstandingOrderColl As ArrayList = m_SparePartOutstandingOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartOutstandingOrder), columnName, matchOperator, columnValue))
            Return SparePartOutstandingOrderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartOutstandingOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrder), columnName, matchOperator, columnValue))

            Return m_SparePartOutstandingOrderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrder), "SparePartOutstandingOrderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SparePartOutstandingOrder), "SparePartOutstandingOrderCode", AggregateType.Count)
            Return CType(m_SparePartOutstandingOrderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateSPPO(ByVal SparePartOutstanding As SparePartOutstandingOrder) As SparePartOutstandingOrder
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartOutstandingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(SparePartOutstandingOrder), "DocumentType", MatchType.Exact, SparePartOutstanding.DocumentType))
            criteria.opAnd(New Criteria(GetType(SparePartOutstandingOrder), "SparePartPO.ID", MatchType.Exact, SparePartOutstanding.SparePartPO.ID))
            Dim arlSPOutstandingOrder As ArrayList = m_SparePartOutstandingOrderMapper.RetrieveByCriteria(criteria)
            If arlSPOutstandingOrder.Count > 0 Then
                Return CType(arlSPOutstandingOrder(0), SparePartOutstandingOrder)
            End If
            Return Nothing

        End Function

        Public Function Insert(ByVal objDomain As SparePartOutstandingOrder) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SparePartOutstandingOrderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartOutstandingOrder) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartOutstandingOrderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartOutstandingOrder)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartOutstandingOrderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartOutstandingOrder)
            Try
                m_SparePartOutstandingOrderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


#End Region

#Region "Custom Method"
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(SparePartOutstandingOrder), SortColumn, sortDirection))

            If SortColumn.ToUpper() = "SparePartPO.TermOfPayment.ID".ToUpper() Then
                Dim sSQL As String = GetRetrieveSpSortByTOP(criterias, sortColl, pageNumber, pageSize, totalRow)
                Dim result As ArrayList = m_SparePartOutstandingOrderMapper.RetrieveSP(sSQL)
                totalRow = GetRowCount(criterias)
                Return result
            End If

            Dim SparePartOutstandingOrderColl As ArrayList = m_SparePartOutstandingOrderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartOutstandingOrderColl
        End Function

        Private Function getIDSPOODtl(ByVal strID As String, ByVal strIDs As String) As Boolean
            For Each _id As String In strIDs.Trim.Split(";")
                If strID = _id Then
                    Return True
                End If
            Next
            Return False
        End Function

        Private Function GetItemOutstandingDetail(ByVal spMasterNumber As String, ByVal arlOrigSPPODEstimetDetail As ArrayList, ByVal strIDs As String) As SparePartOutstandingOrderDetail
            For Each itemDetail As SparePartOutstandingOrderDetail In arlOrigSPPODEstimetDetail
                If itemDetail.PartNumber = spMasterNumber AndAlso getIDSPOODtl(itemDetail.ID, strIDs) = False Then
                    Return itemDetail
                End If
            Next
            Return Nothing
        End Function
        Private Function MergeDataSPOutstanding(ByRef arlOrigSPOutstandingOrderDetails As ArrayList, ByVal arlNewSPOutstandingOrderDetails As ArrayList)
            For Each objItemDetail As SparePartOutstandingOrderDetail In arlOrigSPOutstandingOrderDetails
                objItemDetail.RowStatus = DBRowStatus.Deleted
            Next
            Dim strIDs As String = String.Empty
            For Each itemDetail As SparePartOutstandingOrderDetail In arlNewSPOutstandingOrderDetails
                Dim objOriItemDetail As SparePartOutstandingOrderDetail = GetItemOutstandingDetail(itemDetail.PartNumber, arlOrigSPOutstandingOrderDetails, strIDs)
                If Not IsNothing(objOriItemDetail) Then
                    objOriItemDetail.PartName = itemDetail.PartName
                    objOriItemDetail.PartNumber = itemDetail.PartNumber
                    objOriItemDetail.AllocationAmount = itemDetail.AllocationAmount
                    objOriItemDetail.AllocationQty = itemDetail.AllocationQty
                    objOriItemDetail.OpenAmount = itemDetail.OpenAmount
                    objOriItemDetail.OpenQty = itemDetail.OpenQty
                    objOriItemDetail.EstimateFillDate = itemDetail.EstimateFillDate
                    objOriItemDetail.EstimateFillQty = itemDetail.EstimateFillQty
                    objOriItemDetail.OrderQty = itemDetail.OrderQty
                    objOriItemDetail.SubtitutePartNumber = itemDetail.SubtitutePartNumber
                    objOriItemDetail.RowStatus = DBRowStatus.Active

                    If strIDs = String.Empty Then
                        strIDs = objOriItemDetail.ID
                    Else
                        strIDs += ";" & objOriItemDetail.ID
                    End If
                Else
                    arlOrigSPOutstandingOrderDetails.Add(itemDetail)
                End If
            Next
        End Function
        Public Function InsertFromWindowSevice(ByVal spPOHeader As SparePartOutstandingOrder) As Short
            Dim returnValue As Integer = -1
            Dim isChange As New IsChangeFacade
            If Me.IsTaskFree() Then
                Try
                    Me.SetTaskLocking()
                    Dim spPOValidate As SparePartOutstandingOrder = ValidateSPPO(spPOHeader)
                    'Dim spPO As SparePartPO = spPOHeader.SparePartPO
                    'spPO.ProcessCode = "P"
                    If IsNothing(spPOValidate) Then
                        m_TransactionManager.AddInsert(spPOHeader, m_userPrincipal.Identity.Name)
                        For Each itemDetail As SparePartOutstandingOrderDetail In spPOHeader.SparePartOutstandingOrderDetails
                            itemDetail.SparePartOutstandingOrder = spPOHeader
                            m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        spPOHeader.ID = spPOValidate.ID
                        MergeDataSPOutstanding(spPOValidate.SparePartOutstandingOrderDetails, spPOHeader.SparePartOutstandingOrderDetails)
                        For Each oSPPOED As SparePartOutstandingOrderDetail In spPOValidate.SparePartOutstandingOrderDetails
                            m_TransactionManager.AddUpdate(oSPPOED, m_userPrincipal.Identity.Name)
                        Next

                        Dim ArrListSPPODtl As ArrayList = New System.Collections.ArrayList((From obj As SparePartOutstandingOrderDetail In spPOHeader.SparePartOutstandingOrderDetails
                                                    Order By obj.PartNumber
                                                    Select obj).ToList())

                        Dim strSPOODtlID As String = "''"
                        Dim _isTransfer As Short = 0
                        Dim _status As Short = 0
                        Dim strPartNumber As String = String.Empty
                        For Each oSPPOED As SparePartOutstandingOrderDetail In ArrListSPPODtl
                            'start  :by:dna
                            Dim arlSPPOED As ArrayList
                            Dim oSSPOEDInDB As SparePartOutstandingOrderDetail
                            Dim oSSPOEDFac As New SparePartOutstandingOrderDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                            If strPartNumber <> oSPPOED.PartNumber Then
                                strPartNumber = oSPPOED.PartNumber
                                strSPOODtlID = "''"
                                _isTransfer = 0
                                _status = 0
                            End If
                            Dim cSSPOED As New CriteriaComposite(New Criteria(GetType(SparePartOutstandingOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            cSSPOED.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.ID", MatchType.Exact, spPOHeader.ID))
                            cSSPOED.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "PartNumber", MatchType.Exact, oSPPOED.PartNumber))
                            cSSPOED.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "ID", MatchType.NotInSet, strSPOODtlID))
                            'cSSPOED.opAnd(New Criteria(GetType(SparePartOutstandingOrderDetail), "PartName", MatchType.Exact, oSPPOED.PartName))
                            arlSPPOED = oSSPOEDFac.Retrieve(cSSPOED)
                            If arlSPPOED.Count > 0 Then
                                oSSPOEDInDB = CType(arlSPPOED(0), SparePartOutstandingOrderDetail)
                                oSSPOEDInDB.PartName = oSPPOED.PartName
                                oSSPOEDInDB.PartNumber = oSPPOED.PartNumber
                                oSSPOEDInDB.AllocationAmount = oSPPOED.AllocationAmount
                                oSSPOEDInDB.AllocationQty = oSPPOED.AllocationQty
                                oSSPOEDInDB.OpenAmount = oSPPOED.OpenAmount
                                oSSPOEDInDB.OpenQty = oSPPOED.OpenQty
                                oSSPOEDInDB.SubtitutePartNumber = oSPPOED.SubtitutePartNumber
                                oSSPOEDInDB.OrderQty = oSPPOED.OrderQty
                                oSSPOEDInDB.EstimateFillDate = oSPPOED.EstimateFillDate
                                oSSPOEDInDB.EstimateFillQty = oSPPOED.EstimateFillQty
                                oSSPOEDInDB.CreatedBy = oSPPOED.CreatedBy
                                oSSPOEDInDB.LastUpdateBy = m_userPrincipal.Identity.Name
                                oSSPOEDInDB.LastUpdateTime = DateTime.Now
                                oSSPOEDInDB.RowStatus = CType(DBRowStatus.Active, Short)
                                _status = oSSPOEDInDB.Status
                                _isTransfer = oSSPOEDInDB.IsTransfer
                                'm_TransactionManager.AddUpdate(oSSPOEDInDB, m_userPrincipal.Identity.Name)

                                'Farid additional 20181030
                                If isChange.SparePartOutstandingOrderDetail(oSSPOEDInDB) Then
                                    m_TransactionManager.AddUpdate(oSSPOEDInDB, m_userPrincipal.Identity.Name)
                                End If
                                If strSPOODtlID = "''" Then
                                    strSPOODtlID = "'" & oSSPOEDInDB.ID & "'"
                                Else
                                    strSPOODtlID += ",'" & oSSPOEDInDB.ID & "'"
                                End If
                            Else
                                oSSPOEDInDB = oSPPOED
                                'Bug Fix 2015-06-18 
                                oSSPOEDInDB.SparePartOutstandingOrder = spPOHeader
                                'End of Bug Fix 2015-06-18 

                                oSSPOEDInDB.Status = _status
                                oSSPOEDInDB.IsTransfer = _isTransfer
                                m_TransactionManager.AddInsert(oSSPOEDInDB, m_userPrincipal.Identity.Name)
                            End If
                            'end    :by:dna
                        Next
                        'For Each itemDetail As SparePartOutstandingOrderDetail In spPOValidate.SparePartOutstandingOrderDetails
                        '    itemDetail.SparePartOutstandingOrder = spPOHeader
                        '    If itemDetail.ID = 0 Then
                        '        m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                        '    Else
                        '        m_TransactionManager.AddUpdate(itemDetail, m_userPrincipal.Identity.Name)
                        '    End If
                        'Next

                        'Farid additional 20181030
                        If isChange.SparePartOutstandingOrder(spPOHeader) Then
                            spPOHeader.CreatedBy = spPOValidate.CreatedBy
                            m_TransactionManager.AddUpdate(spPOHeader, m_userPrincipal.Identity.Name)
                        End If
                    End If
                    'm_TransactionManager.AddUpdate(spPO, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = 0
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

            If (TypeOf InsertArg.DomainObject Is SparePartOutstandingOrder) Then
                CType(InsertArg.DomainObject, SparePartOutstandingOrder).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SparePartOutstandingOrder).MarkLoaded()
            End If
        End Sub

        Private Function GetRetrieveSpSortByTOP(ByVal Criterias As ICriteria, ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As String
            Dim sSQL As String = "EXEC up_PagingQuery "
            sSQL &= "@Table = N'SparePartOutstandingOrder', "
            sSQL &= "@PK = N'ID', "
            sSQL &= "@PageSize = " & pageSize & ", "
            sSQL &= "@PageNumber = " & pageNumber & ", "
            sSQL &= "@Filter = N' INNER JOIN SparePartPO ON SparePartOutstandingOrder.SparePartPOID = SparePartPO.ID LEFT JOIN TermOfPayment ON SparePartPO.TermOfPaymentID = TermOfPayment.ID "
            sSQL &= Criterias.ToString().Replace("'", "''").Replace("INNER JOIN SparePartPO ON SparePartOutstandingOrder.SparePartPOID = SparePartPO.ID", "")
            sSQL &= "', @Sort = N'" & Sorts.ToString() & "'"

            Return sSQL
        End Function

        Private Function GetRowCount(ByVal Criterias As ICriteria) As Integer
            Dim agg As Aggregate = New Aggregate(GetType(SparePartOutstandingOrder), "ID", AggregateType.Count)

            Return CType(m_SparePartOutstandingOrderMapper.RetrieveScalar(agg, Criterias), Integer)
        End Function
#End Region

    End Class

End Namespace

