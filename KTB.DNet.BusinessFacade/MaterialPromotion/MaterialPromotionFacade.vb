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
'// Generated on 8/2/2007 - 9:56:16 AM
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

    Public Class MaterialPromotionFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MaterialPromotionMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MaterialPromotionMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNET.Domain.MaterialPromotion).ToString)
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.MaterialPromotion))
            Me.m_TransactionManager = New TransactionManager
            AddHandler Me.m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As KTB.DNET.Domain.MaterialPromotion
            Return CType(m_MaterialPromotionMapper.Retrieve(ID), KTB.DNET.Domain.MaterialPromotion)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNET.Domain.MaterialPromotion
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), "GoodNo", MatchType.Exact, Code))
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), "GoodNo", MatchType.Exact, Code))

            Dim MaterialPromotionColl As ArrayList = m_MaterialPromotionMapper.RetrieveByCriteria(criterias)
            If (MaterialPromotionColl.Count > 0) Then
                Return CType(MaterialPromotionColl(0), KTB.DNET.Domain.MaterialPromotion)
            End If
            Return New KTB.DNET.Domain.MaterialPromotion
        End Function


        Public Function RetrieveActive(ByVal Code As String) As KTB.DNET.Domain.MaterialPromotion
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), "GoodNo", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), "Status", MatchType.Exact, CShort(EnumMaterialPromotion.MasterMaterialPromotionStatus.Aktif)))

            Dim MaterialPromotionColl As ArrayList = m_MaterialPromotionMapper.RetrieveByCriteria(criterias)
            If (MaterialPromotionColl.Count > 0) Then
                Return CType(MaterialPromotionColl(0), KTB.DNET.Domain.MaterialPromotion)
            End If
            Return New KTB.DNET.Domain.MaterialPromotion
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MaterialPromotionMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MaterialPromotionMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MaterialPromotionMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNET.Domain.MaterialPromotion), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaterialPromotionMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNET.Domain.MaterialPromotion), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaterialPromotionMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), "Status", MatchType.Exact, CShort(EnumMaterialPromotion.MasterMaterialPromotionStatus.Aktif)))
            Dim _MaterialPromotion As ArrayList = m_MaterialPromotionMapper.RetrieveByCriteria(criterias)
            Return _MaterialPromotion
        End Function

        Public Function RetrieveActiveListDealer() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), "Status", MatchType.Exact, CShort(EnumMaterialPromotion.MasterMaterialPromotionStatus.Aktif)))
            Dim _MaterialPromotion As ArrayList = m_MaterialPromotionMapper.RetrieveByCriteria(criterias)
            Return _MaterialPromotion
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(KTB.DNET.Domain.MaterialPromotion), SortColumn, sortDirection))

            Dim MaterialPromotionColl As ArrayList = m_MaterialPromotionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return MaterialPromotionColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal criterias As CriteriaComposite) As ArrayList
            Dim MaterialPromotionColl As ArrayList = m_MaterialPromotionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MaterialPromotionColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MaterialPromotionColl As ArrayList = m_MaterialPromotionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MaterialPromotionColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MaterialPromotionColl As ArrayList = m_MaterialPromotionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), columnName, matchOperator, columnValue))
            Return MaterialPromotionColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNET.Domain.MaterialPromotion), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), columnName, matchOperator, columnValue))

            Return m_MaterialPromotionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function DeleteFromDB(ByVal objDomain As KTB.DNet.Domain.MaterialPromotion) As Integer
            Dim nResult As Integer = 1
            If (Me.IsTaskFree()) Then

                Try
                    m_MaterialPromotionMapper.Delete(objDomain)
                Catch ex As Exception
                    Dim s As String = ex.Message
                    nResult = -1
                End Try
            End If
            Return nResult
        End Function

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(KTB.DNET.Domain.MaterialPromotion), "GoodNo", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(KTB.DNET.Domain.MaterialPromotion), "GoodNo", AggregateType.Count)
            Return CType(m_MaterialPromotionMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.MaterialPromotion) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each item As MaterialPromotionPriceHistory In objDomain.MaterialPromotionPriceHistorys
                        item.MaterialPromotion = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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
            Return returnValue
        End Function

        Public Function InsertTransaction(ByVal objDomain As KTB.DNet.Domain.MaterialPromotion) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If objDomain.MaterialPromotionPriceHistorys.Count > 1 Then
                        'get the newer history at index 0
                        Dim objHistory As MaterialPromotionPriceHistory = objDomain.MaterialPromotionPriceHistorys(0)
                        objHistory.MaterialPromotion = objDomain
                        m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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
            Return returnValue
        End Function

        Public Function InsertTransaction(ByVal objDomain As KTB.DNet.Domain.MaterialPromotion, ByVal objHistory As MaterialPromotionPriceHistory) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    objHistory.MaterialPromotion = objDomain
                    m_TransactionManager.AddInsert(objHistory, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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
            Return returnValue
        End Function

        Public Function InsertTransactionStock(ByVal objDomain As KTB.DNet.Domain.MaterialPromotion) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim objMPStockAdj As MaterialPromotionStockAdjustment

                    If objDomain.MaterialPromotionStockAdjustments.Count > 0 Then
                        objMPStockAdj = objDomain.MaterialPromotionStockAdjustments(0)
                        objMPStockAdj.MaterialPromotion = objDomain
                        m_TransactionManager.AddInsert(objMPStockAdj, m_userPrincipal.Identity.Name)
                        returnValue = objMPStockAdj.ID
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objMPStockAdj.ID
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
            Return returnValue
        End Function

        Public Function Delete(ByVal objDomain As KTB.DNet.Domain.MaterialPromotion) As Integer
            Dim nResult As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    'delete the detail first
                    For Each itemDetail As MaterialPromotionPriceHistory In objDomain.MaterialPromotionPriceHistorys
                        Dim objMPHistory As MaterialPromotionPriceHistory = itemDetail
                        m_TransactionManager.AddDelete(objMPHistory)
                    Next

                    'then delete the header
                    m_TransactionManager.AddDelete(objDomain)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        nResult = objDomain.ID
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
            Return nResult
        End Function

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.MaterialPromotion) As Integer
            Dim iReturn As Integer = -1
            Try
                m_MaterialPromotionMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                iReturn = objDomain.ID
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.MaterialPromotion) Then

                CType(InsertArg.DomainObject, KTB.DNET.Domain.MaterialPromotion).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.MaterialPromotion).MarkLoaded()
            Else
                If (TypeOf InsertArg.DomainObject Is MaterialPromotionStockAdjustment) Then

                    CType(InsertArg.DomainObject, MaterialPromotionStockAdjustment).ID = InsertArg.ID
                    CType(InsertArg.DomainObject, MaterialPromotionStockAdjustment).MarkLoaded()
                End If
            End If
        End Sub
#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

