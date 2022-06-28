
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
'// Copyright  2016
'// ---------------------
'// $History      : $
'// Generated on 9/27/2016 - 11:45:31 AM
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class SparePartPackingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartPackingMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartPackingMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartPacking).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(SparePartPacking))
            Me.DomainTypeCollection.Add(GetType(SparePartPackingDetail))


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartPacking
            Return CType(m_SparePartPackingMapper.Retrieve(ID), SparePartPacking)
        End Function


        Public Function Retrieve(ByVal InternalHUNo As String) As SparePartPacking
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPacking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPacking), "InternalHUNo", MatchType.Exact, InternalHUNo))

            Dim arl As ArrayList = m_SparePartPackingMapper.RetrieveByCriteria(criterias)
            If (arl.Count > 0) Then
                Return CType(arl(0), SparePartPacking)
            End If
            Return New SparePartPacking
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartPackingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartPackingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartPackingMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPacking), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPackingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPacking), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPackingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPacking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartPacking As ArrayList = m_SparePartPackingMapper.RetrieveByCriteria(criterias)
            Return _SparePartPacking
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPacking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPackingColl As ArrayList = m_SparePartPackingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartPackingColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartPackingColl As ArrayList = m_SparePartPackingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartPackingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPacking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPackingColl As ArrayList = m_SparePartPackingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartPacking), columnName, matchOperator, columnValue))
            Return SparePartPackingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPacking), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPacking), columnName, matchOperator, columnValue))

            Return m_SparePartPackingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As SparePartPacking) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SparePartPackingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartPacking) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartPackingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartPacking)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartPackingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartPacking)
            Try
                m_SparePartPackingMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SparePartPacking) Then
                CType(InsertArg.DomainObject, SparePartPacking).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SparePartPacking).MarkLoaded()
            End If
        End Sub

        Public Function InsertFromWebSevice(ByVal spPacking As SparePartPacking) As Short
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim spPacking_old As SparePartPacking = ValidateSparePartPacking(spPacking)
                    If IsNothing(spPacking_old) Then
                        m_TransactionManager.AddInsert(spPacking, m_userPrincipal.Identity.Name)
                        For Each itemDetail As SparePartPackingDetail In spPacking.SparePartPackingDetails
                            itemDetail.SparePartPacking = spPacking
                            m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        'update rowstatus = -1 for old detail
                        For Each itemDetail_Old As SparePartPackingDetail In spPacking_old.SparePartPackingDetails
                            itemDetail_Old.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(itemDetail_Old, m_userPrincipal.Identity.Name)
                            'objFac.Delete(itemDetail_Old)
                        Next
                        For Each itemDetail As SparePartPackingDetail In spPacking.SparePartPackingDetails

                            'Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartPackingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartPackingDetail), "SparePartPacking.ID", MatchType.Exact, spPacking_old.ID))
                            criterias.opAnd(New Criteria(GetType(SparePartPackingDetail), "InternalHUItemNo", MatchType.Exact, itemDetail.InternalHUItemNo))

                            Dim arlspPackingDet As ArrayList = New SparePartPackingDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                            If Not IsNothing(arlspPackingDet) AndAlso arlspPackingDet.Count > 0 Then
                                Dim ospPackingDetDB As New SparePartPackingDetail
                                ospPackingDetDB = CType(arlspPackingDet(0), SparePartPackingDetail)
                                ospPackingDetDB.SparePartDO = itemDetail.SparePartDO
                                ospPackingDetDB.DOItemNo = itemDetail.DOItemNo
                                ospPackingDetDB.SparePartMaster = itemDetail.SparePartMaster
                                ospPackingDetDB.Qty = itemDetail.Qty
                                ospPackingDetDB.UoM = itemDetail.UoM
                                ospPackingDetDB.RowStatus = CType(DBRowStatus.Active, Short)

                                m_TransactionManager.AddUpdate(ospPackingDetDB, m_userPrincipal.Identity.Name)
                            Else

                                itemDetail.SparePartPacking = spPacking_old
                                m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            End If
                        Next

                        spPacking_old.InternalHUNo = spPacking.InternalHUNo
                        spPacking_old.PackMaterial = spPacking.PackMaterial
                        spPacking_old.PackMaterialDesc = spPacking.PackMaterialDesc
                        spPacking_old.LotCase = spPacking.LotCase
                        spPacking_old.Weight = spPacking.Weight
                        spPacking_old.Volume = spPacking.Volume
                        spPacking_old.TotalItem = spPacking.TotalItem
                        spPacking_old.TotalQty = spPacking.TotalQty

                        m_TransactionManager.AddUpdate(spPacking_old, m_userPrincipal.Identity.Name)

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

        Public Function DeleteFromWebSevice(ByVal spPacking As SparePartPacking) As Short
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    Dim spPacking_DB As SparePartPacking = ValidateSparePartPacking(spPacking)
                    If Not IsNothing(spPacking_DB) Then
                        spPacking_DB.RowStatus = CType(DBRowStatus.Deleted, Short)
                        m_TransactionManager.AddUpdate(spPacking_DB, m_userPrincipal.Identity.Name)
                        For Each itemDetail As SparePartPackingDetail In spPacking_DB.SparePartPackingDetails
                            itemDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(itemDetail, m_userPrincipal.Identity.Name)
                        Next
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

        Public Function ValidateSparePartPacking(ByVal SparePartPacking As SparePartPacking) As SparePartPacking

            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPacking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPacking), "InternalHUNo", MatchType.Exact, SparePartPacking.InternalHUNo))
            Dim arlSparePartPacking As ArrayList = m_SparePartPackingMapper.RetrieveByCriteria(criteria)
            If arlSparePartPacking.Count > 0 Then
                Return CType(arlSparePartPacking(0), SparePartPacking)
            End If
            Return Nothing

        End Function

        Private Function MergeDataSparePartPacking(ByRef objOrigspPackingDetails As ArrayList, ByVal objNewspPackingDetails As ArrayList)
            For Each objItemDetail As SparePartPackingDetail In objOrigspPackingDetails
                objItemDetail.RowStatus = DBRowStatus.Deleted
            Next
            For Each itemDetail As SparePartPackingDetail In objNewspPackingDetails
                'Dim objOriItemDetail As SparePartPackingDetail = GetItemDoDetail(itemDetail.SparePartPacking.DONumber, objOrigspPackingDetails)
                'If Not IsNothing(objOriItemDetail) Then
                '    objOriItemDetail.RowStatus = DBRowStatus.Active
                '    objOriItemDetail.SparePartPOEstimate = itemDetail.SparePartPOEstimate
                '    objOriItemDetail.SparePartMaster = itemDetail.SparePartMaster
                '    objOriItemDetail.Qty = itemDetail.Qty
                '    objOriItemDetail.ItemNoSO = itemDetail.ItemNoSO
                '    objOriItemDetail.ItemNoDO = itemDetail.ItemNoDO
                'Else
                '    objOrigspPackingDetails.Add(itemDetail)
                'End If
            Next
        End Function

        'Private Function GetItemDoDetail(ByVal spMasterNumber As String, ByVal arlOrigSparePartPackingDetail As ArrayList) As SparePartPackingDetail
        '    For Each itemDetail As SparePartPackingDetail In arlOrigSparePartPackingDetail
        '        If itemDetail.SparePartDODetail.SparePartDO.DONumber = spMasterNumber Then
        '            Return itemDetail
        '        End If
        '    Next
        '    Return Nothing
        'End Function

#End Region


    End Class

End Namespace

