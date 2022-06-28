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
'// Generated on 9/26/2005 - 2:38:25 PM
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
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class SparePartPendingOrderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartPendingOrderMapper As IMapper
        Private m_TransactionManager As TransactionManager



#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartPendingOrderMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartPendingOrder).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(SparePartPendingOrder))
            Me.DomainTypeCollection.Add(GetType(SparePartPO))
            Me.DomainTypeCollection.Add(GetType(SparePartPODetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartPendingOrder
            Return CType(m_SparePartPendingOrderMapper.Retrieve(ID), SparePartPendingOrder)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartPendingOrderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartPendingOrderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartPendingOrderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPendingOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPendingOrderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPendingOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPendingOrderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPendingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartPendingOrder As ArrayList = m_SparePartPendingOrderMapper.RetrieveByCriteria(criterias)
            Return _SparePartPendingOrder
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPendingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPendingOrderColl As ArrayList = m_SparePartPendingOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartPendingOrderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartPendingOrderColl As ArrayList = m_SparePartPendingOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartPendingOrderColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Search.Sort(GetType(SparePartPendingOrder), SortColumn, sortDirection))

            Dim SparePartPendingOrderColl As ArrayList = m_SparePartPendingOrderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartPendingOrderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPendingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPendingOrder), columnName, matchOperator, columnValue))
            Dim SparePartPendingOrderColl As ArrayList = m_SparePartPendingOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartPendingOrderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPendingOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPendingOrder), columnName, matchOperator, columnValue))

            Return m_SparePartPendingOrderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPendingOrder), "SparePartPendingOrderCode", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(SparePartPendingOrder), "SparePartPendingOrderCode", AggregateType.Count)
        '    Return CType(m_SparePartPendingOrderMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

        Public Function Insert(ByVal objDomain As SparePartPendingOrder) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SparePartPendingOrderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        'Farid Additional 20181112 -------------------------------------------
        Public Function Update(ByVal objDomain As SparePartPendingOrder, ByVal objSPPo As SparePartPendingOrder) As Integer
            Dim nResult As Integer = -1
            Dim ischange As New IsChangeFacade
            Try
                If ischange.ISchangeSparePartPOPendingOrder(objSPPo, objDomain) Then
                    nResult = m_SparePartPendingOrderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                End If
                'nResult = m_SparePartPendingOrderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function
        'Farid Additional 20181112 -------------------------------------------

        Public Function Update(ByVal objDomain As SparePartPendingOrder) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartPendingOrderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartPendingOrder)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartPendingOrderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
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
                m_SparePartPendingOrderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
        Public Function ValidateSPPO(ByVal SparePartPendingOrder As SparePartPendingOrder) As SparePartPendingOrder
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.SparePartPendingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(SparePartPendingOrder), "DocumentType", MatchType.Exact, SparePartPendingOrder.DocumentType))
            criteria.opAnd(New Criteria(GetType(SparePartPendingOrder), "SparePartPO.ID", MatchType.Exact, SparePartPendingOrder.SparePartPO.ID))
            criteria.opAnd(New Criteria(GetType(SparePartPendingOrder), "SONumber", MatchType.Exact, SparePartPendingOrder.SONumber))
            Dim arlSPPOEstimate As ArrayList = m_SparePartPendingOrderMapper.RetrieveByCriteria(criteria)
            If arlSPPOEstimate.Count > 0 Then
                Return CType(arlSPPOEstimate(0), SparePartPendingOrder)
            End If
            Return Nothing

        End Function



        Public Function InsertFromWindowSevice(ByVal spPOHeader As SparePartPendingOrder) As Short
            Dim returnValue As Integer = -1
            Dim ischange As New IsChangeFacade
            'If RetrieveWithOneCriteria(1, 1, 1, "SONumber", MatchType.Exact, spPOHeader.SONumber).Count > 0 Then
            '    Return returnValue
            'End If
            If Me.IsTaskFree() Then
                Try
                    Me.SetTaskLocking()
                    Dim spPOValidate As SparePartPendingOrder = ValidateSPPO(spPOHeader)
                    Dim spPO As SparePartPO = spPOHeader.SparePartPO

                    spPO.ProcessCode = "P"
                    If IsNothing(spPOValidate) Then
                        m_TransactionManager.AddInsert(spPOHeader, m_userPrincipal.Identity.Name)

                    Else
                        Dim spPOVal As SparePartPO = spPOValidate.SparePartPO
                        spPOHeader.ID = spPOValidate.ID

                        '----- move inside else block based on pak nana confirmation
                        'If spPO.Dealer.ID = spPOVal.Dealer.ID Then

                        '    If ischange.ISchangeSparePartPO(spPO, spPOVal) Then

                        '        m_TransactionManager.AddUpdate(spPO, m_userPrincipal.Identity.Name)

                        '    End If
                        'Else
                        '    Throw New Exception("Invalid Data, Dealer Different, Dealer Existing : " & spPOVal.Dealer.DealerCode & " ,New Dealer : " & spPO.Dealer.DealerCode)
                        'End If
                        '-----

                        If ischange.ISchangeSparePartPOPendingOrder(spPOHeader, spPOValidate) Then
                            spPOHeader.CreatedBy = spPOValidate.CreatedBy
                            m_TransactionManager.AddUpdate(spPOHeader, m_userPrincipal.Identity.Name)

                        End If


                        'For Each itemDetail As SparePartPendingOrderDetail In spPOValidate.SparePartPendingOrderDetails
                        '    itemDetail.SparePartPendingOrder = spPOHeader
                        '    If itemDetail.ID = 0 Then
                        '        m_TransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                        '    Else
                        '        m_TransactionManager.AddUpdate(itemDetail, m_userPrincipal.Identity.Name)
                        '    End If
                        'Next

                    End If

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

        Public Function UpdatePOEstimateSync(ByVal spPOEstimate As SparePartPendingOrder) As String
            Dim returnValue As String = String.Empty
            If Me.IsTaskFree() Then
                Try
                    Me.SetTaskLocking()

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
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SparePartPendingOrder) Then
                CType(InsertArg.DomainObject, SparePartPendingOrder).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SparePartPendingOrder).MarkLoaded()
            End If
        End Sub


#End Region

#Region "Custom Method"


#End Region

    End Class

End Namespace


