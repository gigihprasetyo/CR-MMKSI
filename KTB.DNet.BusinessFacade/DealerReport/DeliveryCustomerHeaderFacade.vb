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
#End Region

Namespace KTB.DNet.BusinessFacade.DealerReport
    Public Class DeliveryCustomerHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_Mapper As IMapper
        Private m_StockMovementMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_Mapper = MapperFactory.GetInstance().GetMapper(GetType(DeliveryCustomerHeader).ToString)
            Me.m_StockMovementMapper = MapperFactory.GetInstance().GetMapper(GetType(StockMovement).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DeliveryCustomerHeader
            Return CType(m_Mapper.Retrieve(ID), DeliveryCustomerHeader)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_Mapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_Mapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_Mapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_Mapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal crit As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_Mapper.RetrieveByCriteria(crit, sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_Mapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DCColl As ArrayList = m_Mapper.RetrieveByCriteria(criterias)
            Return DCColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DCColl As ArrayList = m_Mapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DCColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DCColl As ArrayList = m_Mapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DCColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_Mapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_Mapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DCColl As ArrayList = m_Mapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DCColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim DCColl As ArrayList = m_Mapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DCColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), columnName, matchOperator, columnValue))
            Dim DCColl As ArrayList = m_Mapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DCColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DeliveryCustomerHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), columnName, matchOperator, columnValue))

            Return m_Mapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As DeliveryCustomerHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                m_Mapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DeliveryCustomerHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_Mapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DeliveryCustomerHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_Mapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As DeliveryCustomerHeader) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_Mapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DeliveryCustomerHeader) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DeliveryCustomerHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DeliveryCustomerHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.DeliveryCustomerDetail) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DeliveryCustomerDetail).ID = InsertArg.ID
            End If

        End Sub



        Public Function InsertTransaction(ByVal objDeliveryCustomerHeader As DeliveryCustomerHeader, ByVal arrDeliveryCustomerDetails As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()


                    m_TransactionManager.AddInsert(objDeliveryCustomerHeader, m_userPrincipal.Identity.Name)

                    If arrDeliveryCustomerDetails.Count > 0 Then
                        For Each item As DeliveryCustomerDetail In arrDeliveryCustomerDetails
                            item.DeliveryCustomerHeader = objDeliveryCustomerHeader
                            If Not objDeliveryCustomerHeader.Dealer Is Nothing Then
                                item.ChassisMaster.StockDealer = objDeliveryCustomerHeader.Dealer.ID
                                item.ChassisMaster.StockDate = objDeliveryCustomerHeader.PostingDate
                                item.ChassisMaster.StockStatus = ""
                            Else
                                item.ChassisMaster.StockStatus = "X"
                                'item.ChassisMaster.StockDate = New Date(1900, 1, 1)
                                'item.ChassisMaster.StockDealer = Nothing
                            End If
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)

                            Dim sm As New StockMovement
                            sm.ChassisMaster = item.ChassisMaster
                            sm.Dealer = objDeliveryCustomerHeader.Dealer
                            sm.FromDealer = objDeliveryCustomerHeader.FromDealer
                            sm.ProcessDate = objDeliveryCustomerHeader.PostingDate

                            m_TransactionManager.AddInsert(sm, m_userPrincipal.Identity.Name)

                            m_TransactionManager.AddUpdate(item.ChassisMaster, m_userPrincipal.Identity.Name)
                        Next
                    End If


                    m_TransactionManager.PerformTransaction()
                    returnValue = objDeliveryCustomerHeader.ID
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
        Public Sub DeleteTransaction(ByVal objDomain As DeliveryCustomerHeader)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)

                For Each item As DeliveryCustomerDetail In objDomain.DeliveryCustomerDetails
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next

                UpdateTransaction(objDomain, objDomain.DeliveryCustomerDetails)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


        Public Function UpdateTransaction(ByVal objDomain As DeliveryCustomerHeader, ByVal arrDeliveryCustomerDetails As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrDeliveryCustomerDetails.Count > 0 Then

                        For Each item As DeliveryCustomerDetail In arrDeliveryCustomerDetails
                            item.DeliveryCustomerHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)

                                Dim sm As New StockMovement
                                sm.ChassisMaster = item.ChassisMaster
                                sm.Dealer = objDomain.Dealer
                                sm.FromDealer = objDomain.FromDealer
                                sm.ProcessDate = objDomain.PostingDate

                                m_TransactionManager.AddInsert(sm, m_userPrincipal.Identity.Name)
                            End If

                            If Not objDomain.Dealer Is Nothing Then
                                item.ChassisMaster.StockDealer = objDomain.Dealer.ID
                                item.ChassisMaster.StockDate = objDomain.PostingDate
                                item.ChassisMaster.StockStatus = ""
                            Else
                                'item.ChassisMaster.StockDealer = Nothing
                                'item.ChassisMaster.StockDate = New Date(1900, 1, 1)
                                item.ChassisMaster.StockStatus = "X"
                            End If
                            m_TransactionManager.AddUpdate(item.ChassisMaster, m_userPrincipal.Identity.Name)

                        Next
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
        Public Function UpdateTransaction(ByVal objDomain As DeliveryCustomerHeader, ByVal arrDeliveryCustomerDetails As ArrayList, ByVal arrDeliveryCustomerDetailDeleted As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper


                    If arrDeliveryCustomerDetailDeleted.Count > 0 Then
                        For Each item As DeliveryCustomerDetail In arrDeliveryCustomerDetailDeleted
                            item.RowStatus = DBRowStatus.Deleted
                            If item.ChassisMaster.StockStatus = "X" Then
                                item.ChassisMaster.StockStatus = ""
                            Else
                                item.ChassisMaster.StockStatus = ""
                                item.ChassisMaster.StockDealer = objDomain.FromDealer
                            End If

                            Dim arr As New ArrayList
                            Dim tempStockMovement As New StockMovement
                            Dim cr As New CriteriaComposite(New Criteria(GetType(StockMovement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            cr.opAnd(New Criteria(GetType(StockMovement), "ChassisMaster.ID", MatchType.Exact, item.ChassisMaster.ID))

                            Dim sortColl As SortCollection = New SortCollection
                            sortColl.Add(New Sort(GetType(StockMovement), "CreatedTime", Sort.SortDirection.DESC))

                            arr = m_StockMovementMapper.RetrieveByCriteria(cr, sortColl)

                            If arr.Count > 0 Then
                                tempStockMovement = CType(arr(0), StockMovement)
                                tempStockMovement.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(tempStockMovement, m_userPrincipal.Identity.Name)
                            End If


                            m_TransactionManager.AddUpdate(item.ChassisMaster, m_userPrincipal.Identity.Name)
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDeliveryCustomerDetails.Count > 0 Then
                        For Each item As DeliveryCustomerDetail In arrDeliveryCustomerDetails
                            item.DeliveryCustomerHeader = objDomain
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)

                                Dim sm As New StockMovement
                                sm.ChassisMaster = item.ChassisMaster
                                sm.Dealer = objDomain.Dealer
                                sm.FromDealer = objDomain.FromDealer
                                sm.ProcessDate = objDomain.PostingDate

                                m_TransactionManager.AddInsert(sm, m_userPrincipal.Identity.Name)
                            End If

                            If Not objDomain.Dealer Is Nothing Then
                                item.ChassisMaster.StockDealer = objDomain.Dealer.ID
                                item.ChassisMaster.StockDate = objDomain.PostingDate
                                item.ChassisMaster.StockStatus = ""
                            Else
                                'item.ChassisMaster.StockDealer = Nothing
                                'item.ChassisMaster.StockDate = New Date(1900, 1, 1)
                                item.ChassisMaster.StockStatus = "X"
                            End If
                            m_TransactionManager.AddUpdate(item.ChassisMaster, m_userPrincipal.Identity.Name)




                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
                    End If
                Catch ex As InvalidCastException
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

#End Region

#Region "Custom Method"
        Private Function IsStatusChanges(ByVal objDomain As DeliveryCustomerHeader) As Boolean
            If objDomain.ID > 0 Then
                Dim CurrDeliveryCustomerHeader As DeliveryCustomerHeader = m_Mapper.Retrieve(objDomain.ID)
                If objDomain.Status <> CurrDeliveryCustomerHeader.Status Then
                    Return True
                End If
            End If
            Return False
        End Function

        Public Function UbahStatusSalesDeliveryVechile(ByVal listObjDomain As ArrayList, ByVal NewStatus As EnumSalesDeliveryVechile.SalesDeliveryVechileStatus, ByRef ErrMsg As String) As Integer
            Dim result As Integer = -1

            For Each item As DeliveryCustomerHeader In listObjDomain
                If IsStatusChanges(item) Then
                    ErrMsg = "Status Reg D/O  no : " & item.RegDONumber & " telah berubah. Proses dibatalkan."
                    Return result
                End If
            Next

            Select Case NewStatus
                Case EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Baru
                    For Each objDomain As DeliveryCustomerHeader In listObjDomain
                        result = BaruSalesDeliveryVechile(objDomain)
                        If result = -1 Then Exit For
                    Next
                Case EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Selesai
                    For Each objDomain As DeliveryCustomerHeader In listObjDomain
                        result = SelesaiSalesDeliveryVechile(objDomain)
                        If result = -1 Then Exit For
                    Next
            End Select

            Return result
        End Function

        Public Function UbahStatusSalesDeliveryVechile(ByVal ObjDomain As DeliveryCustomerHeader, ByVal NewStatus As EnumSalesDeliveryVechile.SalesDeliveryVechileStatus, ByRef ErrMsg As String) As Integer
            Dim result As Integer = -1

            If IsStatusChanges(ObjDomain) Then
                ErrMsg = "Status Reg D/O  no : " & ObjDomain.RegDONumber & " telah berubah. Proses dibatalkan."
                Return result
            End If

            Select Case NewStatus
                Case EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Baru
                    result = BaruSalesDeliveryVechile(ObjDomain)
                Case EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Selesai
                    result = SelesaiSalesDeliveryVechile(ObjDomain)
            End Select

            Return result
        End Function

        Public Function BaruSalesDeliveryVechile(ByVal objDomain As DeliveryCustomerHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    objDomain.Status = EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Baru
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

        Public Function SelesaiSalesDeliveryVechile(ByVal objDomain As DeliveryCustomerHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    objDomain.Status = EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Selesai
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

        Public Function BatalSalesDeliveryVechile(ByVal objDomain As DeliveryCustomerHeader, ByVal arrDeliveryCustomerDetailDeleted As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper


                    If arrDeliveryCustomerDetailDeleted.Count > 0 Then
                        For Each item As DeliveryCustomerDetail In arrDeliveryCustomerDetailDeleted
                            item.RowStatus = DBRowStatus.Deleted
                            If item.ChassisMaster.StockStatus = "X" Then
                                item.ChassisMaster.StockStatus = ""
                            Else
                                item.ChassisMaster.StockStatus = ""
                                item.ChassisMaster.StockDealer = objDomain.FromDealer
                            End If

                            Dim arr As New ArrayList
                            Dim tempStockMovement As New StockMovement
                            Dim cr As New CriteriaComposite(New Criteria(GetType(StockMovement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            cr.opAnd(New Criteria(GetType(StockMovement), "ChassisMaster.ID", MatchType.Exact, item.ChassisMaster.ID))

                            Dim sortColl As SortCollection = New SortCollection
                            sortColl.Add(New Sort(GetType(StockMovement), "CreatedTime", Sort.SortDirection.DESC))

                            arr = m_StockMovementMapper.RetrieveByCriteria(cr, sortColl)

                            If arr.Count > 0 Then
                                tempStockMovement = CType(arr(0), StockMovement)
                                tempStockMovement.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(tempStockMovement, m_userPrincipal.Identity.Name)
                            End If


                            m_TransactionManager.AddUpdate(item.ChassisMaster, m_userPrincipal.Identity.Name)
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    objDomain.Status = EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Batal
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
                    End If
                Catch ex As InvalidCastException
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

#End Region

    End Class

End Namespace
