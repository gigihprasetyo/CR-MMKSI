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
'// Generated on 8/3/2007 - 11:14:25 AM
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

Imports ktb.DNet.Domain
Imports ktb.DNet.Domain.Search
Imports ktb.DNet.DataMapper.Framework
Imports ktb.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade.MaterialPromotion

    Public Class MaterialPromotionAllocationFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MaterialPromotionAllocationMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MaterialPromotionAllocationMapper = MapperFactory.GetInstance.GetMapper(GetType(MaterialPromotionAllocation).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MaterialPromotionAllocation
            Return CType(m_MaterialPromotionAllocationMapper.Retrieve(ID), MaterialPromotionAllocation)
        End Function

        Public Function Retrieve(ByVal Code As String) As MaterialPromotionAllocation
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotionAllocationCode", MatchType.Exact, Code))

            Dim MaterialPromotionAllocationColl As ArrayList = m_MaterialPromotionAllocationMapper.RetrieveByCriteria(criterias)
            If (MaterialPromotionAllocationColl.Count > 0) Then
                Return CType(MaterialPromotionAllocationColl(0), MaterialPromotionAllocation)
            End If
            Return New MaterialPromotionAllocation
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MaterialPromotionAllocationMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MaterialPromotionAllocationMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MaterialPromotionAllocationMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaterialPromotionAllocationMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaterialPromotionAllocationMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MaterialPromotionAllocation As ArrayList = m_MaterialPromotionAllocationMapper.RetrieveByCriteria(criterias)
            Return _MaterialPromotionAllocation
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MaterialPromotionAllocationColl As ArrayList = m_MaterialPromotionAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MaterialPromotionAllocationColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim MaterialPromotionAllocationColl As ArrayList = m_MaterialPromotionAllocationMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MaterialPromotionAllocationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MaterialPromotionAllocationColl As ArrayList = m_MaterialPromotionAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MaterialPromotionAllocationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MaterialPromotionAllocationColl As ArrayList = m_MaterialPromotionAllocationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), columnName, matchOperator, columnValue))
            Return MaterialPromotionAllocationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionAllocation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), columnName, matchOperator, columnValue))

            Return m_MaterialPromotionAllocationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function InsertUpdateDeleteTransaction(ByVal arlToInsert As ArrayList, ByVal isValidasi As Boolean) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arlToInsert.Count > 0 Then
                        For Each objAlokasi As MaterialPromotionAllocation In arlToInsert
                            Dim alokasiAwal As Integer

                            If isValidasi Then
                                objAlokasi.ValidateQty = objAlokasi.Qty
                            End If

                            If objAlokasi.ID = 0 Then
                                alokasiAwal = 0
                            Else
                                alokasiAwal = Me.Retrieve(objAlokasi.ID).ValidateQty
                            End If

                            Dim m_TransactionManager As TransactionManager = New TransactionManager
                            Dim objMaterialPromotion As KTB.DNet.Domain.MaterialPromotion = New MaterialPromotionFacade(m_userPrincipal).Retrieve(objAlokasi.MaterialPromotion.ID)
                            Dim objMaterialpromotionstockadjustment As MaterialPromotionStockAdjustment = New MaterialPromotionStockAdjustment
                            If objAlokasi.ID = 0 Then
                                If objAlokasi.Qty > 0 Then 'Insert
                                    'Todo Cek Validasistock
                                    objMaterialpromotionstockadjustment.StockAwal = objMaterialPromotion.Stock
                                    objMaterialpromotionstockadjustment.Dealer = objAlokasi.Dealer
                                    objMaterialpromotionstockadjustment.MaterialPromotion = objMaterialPromotion
                                    If alokasiAwal < objAlokasi.ValidateQty Then
                                        objMaterialpromotionstockadjustment.AdjustType = 2
                                    Else
                                        objMaterialpromotionstockadjustment.AdjustType = 1
                                    End If
                                    objMaterialpromotionstockadjustment.Description = "Input Alokasi Dealer " & objAlokasi.Dealer.DealerCode & " Periode " & enumMonthGet.GetName(objAlokasi.MaterialPromotionPeriod.StartDate.Month) & " s/d " & enumMonthGet.GetName(objAlokasi.MaterialPromotionPeriod.EndDate.Month) & " " & objAlokasi.MaterialPromotionPeriod.EndDate.Year.ToString
                                    objMaterialpromotionstockadjustment.Qty = System.Math.Abs(objAlokasi.ValidateQty - alokasiAwal)
                                    objMaterialPromotion.Stock = objMaterialPromotion.Stock - (objAlokasi.ValidateQty - alokasiAwal)
                                    m_TransactionManager.AddInsert(objAlokasi, m_userPrincipal.Identity.Name)
                                    If objAlokasi.ValidateQty <> alokasiAwal Then
                                        m_TransactionManager.AddInsert(objMaterialpromotionstockadjustment, m_userPrincipal.Identity.Name)
                                        m_TransactionManager.AddUpdate(objMaterialPromotion, m_userPrincipal.Identity.Name)
                                    End If

                                End If
                            Else
                                If objAlokasi.Qty > 0 Then 'Update

                                    Dim objAlokasiAwal As MaterialPromotionAllocation = Me.Retrieve(objAlokasi.ID)
                                    Dim Selisih As Integer = objAlokasiAwal.ValidateQty - objAlokasi.ValidateQty

                                    If Selisih < 0 Then
                                        objMaterialpromotionstockadjustment.AdjustType = 2
                                    Else
                                        objMaterialpromotionstockadjustment.AdjustType = 1
                                    End If
                                    objMaterialpromotionstockadjustment.StockAwal = objMaterialPromotion.Stock
                                    objMaterialpromotionstockadjustment.Dealer = objAlokasi.Dealer
                                    objMaterialpromotionstockadjustment.MaterialPromotion = objMaterialPromotion
                                    objMaterialpromotionstockadjustment.Description = "Edit Alokasi Dealer " & objAlokasi.Dealer.DealerCode & " Periode " & enumMonthGet.GetName(objAlokasi.MaterialPromotionPeriod.StartDate.Month) & " s/d " & enumMonthGet.GetName(objAlokasi.MaterialPromotionPeriod.EndDate.Month) & " " & objAlokasi.MaterialPromotionPeriod.EndDate.Year.ToString & " Dari " & objAlokasiAwal.Qty.ToString & " ke " & objAlokasi.Qty.ToString
                                    objMaterialpromotionstockadjustment.Qty = System.Math.Abs(objAlokasi.ValidateQty - alokasiAwal)
                                    objMaterialPromotion.Stock = objMaterialPromotion.Stock - (objAlokasi.ValidateQty - alokasiAwal)
                                    m_TransactionManager.AddUpdate(objAlokasi, m_userPrincipal.Identity.Name)
                                    If objAlokasi.ValidateQty <> alokasiAwal Then
                                        m_TransactionManager.AddInsert(objMaterialpromotionstockadjustment, m_userPrincipal.Identity.Name)
                                        m_TransactionManager.AddUpdate(objMaterialPromotion, m_userPrincipal.Identity.Name)
                                    End If


                                Else 'Delete

                                    Dim objAlokasiAwal As MaterialPromotionAllocation = Me.Retrieve(objAlokasi.ID)

                                    objMaterialpromotionstockadjustment.StockAwal = objMaterialPromotion.Stock
                                    objMaterialpromotionstockadjustment.Dealer = objAlokasi.Dealer
                                    objMaterialpromotionstockadjustment.MaterialPromotion = objMaterialPromotion
                                    objMaterialpromotionstockadjustment.AdjustType = 1
                                    objMaterialpromotionstockadjustment.Description = "Hapus Alokasi Dealer " & objAlokasi.Dealer.DealerCode & " Periode " & enumMonthGet.GetName(objAlokasi.MaterialPromotionPeriod.StartDate.Month) & " s/d " & enumMonthGet.GetName(objAlokasi.MaterialPromotionPeriod.EndDate.Month) & " " & objAlokasi.MaterialPromotionPeriod.EndDate.Year.ToString
                                    objMaterialpromotionstockadjustment.Qty = objAlokasiAwal.ValidateQty
                                    objMaterialPromotion.Stock = objMaterialPromotion.Stock + objAlokasiAwal.ValidateQty
                                    m_TransactionManager.AddUpdate(objAlokasi, m_userPrincipal.Identity.Name)
                                    'm_TransactionManager.AddInsert(objMaterialpromotionstockadjustment, m_userPrincipal.Identity.Name)
                                    'm_TransactionManager.AddUpdate(objMaterialPromotion, m_userPrincipal.Identity.Name)


                                End If
                            End If
                            m_TransactionManager.PerformTransaction()
                        Next
                    End If
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
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotionAllocationCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MaterialPromotionAllocation), "MaterialPromotionAllocationCode", AggregateType.Count)
            Return CType(m_MaterialPromotionAllocationMapper.RetrieveScalar(agg, crit), Integer)
        End Function


        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.MaterialPromotionAllocation) As Integer
            Dim returnValue As Short = 1
            Try
                m_MaterialPromotionAllocationMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                returnValue = -1
            End Try

            Return returnValue
        End Function

        Public Function Delete(ByVal objDomain As KTB.DNet.Domain.MaterialPromotionAllocation) As Integer
            Dim nResult As Integer = 1
            If (Me.IsTaskFree()) Then

                Try
                    If objDomain.RowStatus = DBRowStatus.Active Then
                        objDomain.RowStatus = DBRowStatus.Deleted
                    End If
                    m_MaterialPromotionAllocationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                Catch ex As Exception
                    Dim s As String = ex.Message
                    nResult = -1
                End Try
            End If
            Return nResult
        End Function

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.MaterialPromotionAllocation) As Integer
            Dim iReturn As Integer = 1
            Try
                m_MaterialPromotionAllocationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function DeleteFromDB(ByVal objDomain As KTB.DNet.Domain.MaterialPromotionAllocation) As Integer
            Dim iReturn As Integer = -1
            Try
                m_MaterialPromotionAllocationMapper.Delete(objDomain)
                iReturn = 1
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is MaterialPromotionAllocation) Then
                CType(InsertArg.DomainObject, MaterialPromotionAllocation).ID = InsertArg.ID
                CType(InsertArg.DomainObject, MaterialPromotionAllocation).MarkLoaded()
            End If

        End Sub


#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

