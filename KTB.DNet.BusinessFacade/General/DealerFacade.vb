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
'// Generated on 8/3/2005 - 3:58:00 PM
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
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.Collections.Generic
Imports System.Linq

#End Region

Namespace KTB.DNet.BusinessFacade.General

    Public Class DealerFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_DealerMapper As IMapper
        Private m_TransactionControlMapper As IMapper
        Private m_TransactionControlHistoryMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager
        Private m_ViewDealerWSCMapper As IMapper
        Private m_ViewDealerWSCProccessedMapper As IMapper
        Private m_ViewDealerWSCBBMapper As IMapper
        Private m_ViewDealerWSCProccessedBBMapper As IMapper
        Private m_UserInfoMapper As IMapper
        Private m_DealerAdditional As IMapper
        Private m_DealerStallEquipment As IMapper
        Private m_DealerFacility As IMapper
        Private m_DealerPaymentMethod As IMapper
        Private m_StandardCode As IMapper
        Private ID_Insert As Integer
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)


            Me.m_userPrincipal = userPrincipal
            Me.m_DealerMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.Dealer).ToString)
            Me.m_ViewDealerWSCMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.view_DealerWSC).ToString)
            Me.m_ViewDealerWSCProccessedMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.view_DealerWSCProccessed).ToString)
            Me.m_ViewDealerWSCBBMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.view_DealerWSCBB).ToString)
            Me.m_ViewDealerWSCProccessedBBMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.view_DealerWSCProccessedBB).ToString)
            Me.m_TransactionControlMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.TransactionControl).ToString)
            Me.m_TransactionControlHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.TransactionControlHistory).ToString)
            Me.m_UserInfoMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.UserInfo).ToString)
            Me.m_StandardCode = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.StandardCode).ToString)
            'Me.m_DealerPaymentMethod = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.dealerpay).ToString)
            Me.m_DealerFacility = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.Dealerfacility).ToString)
            Me.m_DealerStallEquipment = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.DealerStallEquipment).ToString)
            Me.m_DealerAdditional = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.DealerAdditional).ToString)
            Me.objTransactionManager = New TransactionManager
            AddHandler objTransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.Dealer))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BusinessArea))
            'Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.Dealerfacility))
            'Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DealerStallEquipment))
            'Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DealerAdditional))

        End Sub

#End Region

#Region "Get Public Method"

        Public Function GetDealer(ByVal ID As Integer) As KTB.DNet.Domain.Dealer
            Try
                Return CType(m_DealerMapper.Retrieve(ID), KTB.DNet.Domain.Dealer)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return Nothing
        End Function

        Public Function GetDealer(ByVal DealerCode As String) As KTB.DNet.Domain.Dealer
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, DealerCode))
                Dim poColl As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias)
                If (poColl.Count > 0) Then
                    Return CType(poColl(0), KTB.DNet.Domain.Dealer)
                End If
                Return New KTB.DNet.Domain.Dealer
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return Nothing
        End Function

#End Region

#Region "Retrieve"

        Public Function RetrieveDealerWSC(ByVal criteria As CriteriaComposite, ByVal sortColl As SortCollection) As ArrayList
            Return m_ViewDealerWSCMapper.RetrieveByCriteria(criteria, sortColl)
        End Function

        Public Function RetrieveDealerWSCProccessed(ByVal criteria As CriteriaComposite, ByVal sortColl As SortCollection) As ArrayList
            Return m_ViewDealerWSCProccessedMapper.RetrieveByCriteria(criteria, sortColl)
        End Function

        Public Function RetrieveDealerWSCBB(ByVal criteria As CriteriaComposite, ByVal sortColl As SortCollection) As ArrayList
            Return m_ViewDealerWSCBBMapper.RetrieveByCriteria(criteria, sortColl)
        End Function

        Public Function RetrieveDealerWSCProccessedBB(ByVal criteria As CriteriaComposite, ByVal sortColl As SortCollection) As ArrayList
            Return m_ViewDealerWSCProccessedBBMapper.RetrieveByCriteria(criteria, sortColl)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As KTB.DNet.Domain.Dealer
            Return CType(m_DealerMapper.Retrieve(ID), KTB.DNet.Domain.Dealer)
        End Function

        Public Function GetDealerByCreditAccount(ByVal CreditAccount As String) As KTB.DNet.Domain.Dealer
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, CreditAccount))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "CreditAccount", MatchType.Exact, CreditAccount))

                Dim poColl As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias)
                If (poColl.Count > 0) Then
                    Return CType(poColl(0), KTB.DNet.Domain.Dealer)
                End If
                Return New KTB.DNet.Domain.Dealer
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return Nothing
        End Function

        Public Function GetDealerByGroupID(ByVal groupID As Integer) As ArrayList
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerGroup.ID", MatchType.Exact, groupID))

                Return m_DealerMapper.RetrieveByCriteria(criterias)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return Nothing
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.Dealer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, Code))

            Dim DealerColl As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias)
            If (DealerColl.Count > 0) Then
                Return CType(DealerColl(0), KTB.DNet.Domain.Dealer)
            End If
            Return New KTB.DNet.Domain.Dealer
        End Function

        Public Function Retrieve(ByVal Code As String, ByVal iGroupID As Integer) As KTB.DNet.Domain.Dealer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerGroup.ID", MatchType.Exact, iGroupID))

            Dim DealerColl As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias)
            If (DealerColl.Count > 0) Then
                Return CType(DealerColl(0), KTB.DNet.Domain.Dealer)
            End If
            Return New KTB.DNet.Domain.Dealer
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DealerMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DealerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DealerMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Dealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal crit As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Dealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            'Return m_DealerMapper.RetrieveList(sortColl)
            Return m_DealerMapper.RetrieveByCriteria(crit, sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Dealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Dealer As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias)
            Return _Dealer
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Dealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_DealerMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sorts As ICollection) As ArrayList
            Return m_DealerMapper.RetrieveByCriteria(Criterias, sorts)
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerColl As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DealerColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Dealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DealerColl As ArrayList = m_DealerMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
      ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Dealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DealerColl As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DealerColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim DealerColl As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias)
            Return DealerColl
        End Function
        ' 13-Aug-2007   Deddy H     Penambahan function retrieve dengan sort
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(Dealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim DealerColl As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerColl

        End Function

        Public Function RetrieveByCriteriaAndSort(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal Shorting As ICollection) As ArrayList
            Dim DealerColl As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias, Shorting, pageNumber, pageSize, totalRow)
            Return DealerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), columnName, matchOperator, columnValue))
            Dim DealerColl As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DealerColl
        End Function
        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Dealer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), columnName, matchOperator, columnValue))

            Return m_DealerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveTransactionControl(ByVal nDealerID As Integer, ByVal strTransactionType As String) As TransactionControl
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Dealer.ID", MatchType.Exact, nDealerID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Kind", MatchType.Exact, strTransactionType))
            Dim ArrTransactionControl As ArrayList = m_TransactionControlMapper.RetrieveByCriteria(criterias)
            If ArrTransactionControl.Count > 0 Then
                Return ArrTransactionControl(0)
            End If
            Return Nothing
        End Function
        Public Function RetrieveTransactionControlByCriteria(ByVal criterias As ICriteria) As TransactionControl
            Dim ArrTransactionControl As ArrayList = m_TransactionControlMapper.RetrieveByCriteria(criterias)
            If ArrTransactionControl.Count > 0 Then
                Return ArrTransactionControl(0)
            End If
            Return Nothing
        End Function

        Public Function RetrieveTransactionControlActiveList(ByVal StrStatus As String, ByVal strTransactionType As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Status", MatchType.Exact, StrStatus))
            If strTransactionType <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Kind", MatchType.Exact, strTransactionType))
            End If

            Dim ArrTransactionControl As ArrayList = m_TransactionControlMapper.RetrieveByCriteria(criterias)
            If ArrTransactionControl.Count > 0 Then
                Return ArrTransactionControl
            End If
            Return Nothing
        End Function

        Public Function RetrieveTransactionControlActiveListAll(ByVal strTransactionType As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Kind", MatchType.Exact, strTransactionType))

            Dim ArrTransactionControl As ArrayList = m_TransactionControlMapper.RetrieveByCriteria(criterias)
            If ArrTransactionControl.Count > 0 Then
                Return ArrTransactionControl
            End If
            Return Nothing
        End Function

        Public Function RetrieveOneInstance(ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As Dealer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), columnName, matchOperator, columnValue))
            Dim DealerColl As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias)
            If DealerColl.Count > 0 Then
                Return DealerColl(0)
            End If
            Return Nothing
        End Function

        Public Function RetrieveUsingSP(ByVal SPName As String) As DataTable
            Dim arr As DataSet
            arr = m_DealerMapper.RetrieveDataSet(SPName)

            If arr.Tables.Count > 0 Then
                Return arr.Tables(0)
            Else
                Return Nothing
            End If

        End Function

#End Region

#Region "Transaction Methods"

        Public Function InsertDealer(ByVal objDomain As KTB.DNet.Domain.Dealer, ByVal arrBusinessArea As ArrayList, ByVal arrVehicleCategory As ArrayList, ByVal arrDealerProfile As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree() OrElse 1 = 1) Then
                Try
                    '    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    objTransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If arrBusinessArea.Count > 0 Then
                        For Each objBusinessArea As BusinessArea In arrBusinessArea
                            objBusinessArea.Dealer = objDomain
                            objTransactionManager.AddInsert(objBusinessArea, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    objTransactionManager.PerformTransaction()

                    'dealerProfile
                    If arrDealerProfile.Count > 0 Then
                        For Each objDealerProfile As DealerProfile In arrDealerProfile
                            objDealerProfile.Dealer = objDomain

                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(DealerProfile), "ProfileHeader.ID", MatchType.Exact, objDealerProfile.ProfileHeader.ID))
                            criterias.opAnd(New Criteria(GetType(DealerProfile), "Dealer.ID", MatchType.Exact, objDomain.ID))
                            criterias.opAnd(New Criteria(GetType(DealerProfile), "ProfileGroup.ID", MatchType.Exact, objDealerProfile.ProfileGroup.ID))
                            Dim arlDealerProfile As ArrayList = New DealerProfileFacade(m_userPrincipal).Retrieve(criterias)
                            If arlDealerProfile.Count > 0 Then
                                objTransactionManager.AddUpdate(objDealerProfile, m_userPrincipal.Identity.Name)
                            Else
                                objTransactionManager.AddInsert(objDealerProfile, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    'vehicle category
                    If arrVehicleCategory.Count > 0 Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerCategory), "Dealer.ID", MatchType.Exact, CType(objDomain.ID, Integer)))

                        Dim DealerCategoryFac As DealerCategoryFacade = New DealerCategoryFacade(m_userPrincipal)
                        Dim arrDealerCategory As ArrayList = DealerCategoryFac.Retrieve(criterias)
                        If arrDealerCategory.Count = 0 Then
                            For Each item As String In arrVehicleCategory
                                Dim objDealerCategory As New DealerCategory
                                objDealerCategory.Dealer = objDomain
                                objDealerCategory.Category = New CategoryFacade(m_userPrincipal).Retrieve(CType(item, Integer))
                                objTransactionManager.AddInsert(objDealerCategory, m_userPrincipal.Identity.Name)
                            Next
                        Else
                            For Each objDealerCategory As DealerCategory In arrDealerCategory
                                Dim existCategory As Boolean = False
                                For Each objCategory As Category In arrVehicleCategory
                                    If objDealerCategory.Category.ID = objCategory.ID Then
                                        existCategory = True
                                    End If
                                Next
                                If existCategory Then
                                    objDealerCategory.RowStatus = 0
                                    objTransactionManager.AddUpdate(objDealerCategory, m_userPrincipal.Identity.Name)
                                Else
                                    objDealerCategory.RowStatus = -1
                                    objTransactionManager.AddUpdate(objDealerCategory, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If performTransaction Then
                        '  objTransactionManager.PerformTransaction()
                        returnValue = 0
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

        ' 09-Aug-2007   Deddy H     Penambahan func baru tuk handle profile
        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.Dealer, ByVal objListProfile As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    'objTransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    ' data dealer tdk diinsert, hanya profile saja
                    For Each item As DealerProfile In objListProfile
                        item.Dealer = objDomain
                        objTransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        For Each items As DealerProfileHistory In item.DealerProfileHistorys
                            items.DealerProfile = item
                            objTransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        Next
                    Next
                    If performTransaction Then
                        objTransactionManager.PerformTransaction()
                        returnValue = ID_Insert
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        returnValue = -1
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        ' 09-Aug-2007   Deddy H     Penambahan func baru tuk handle profile
        Public Function Update(ByVal objDomain As Dealer, ByVal objListProfile As ArrayList, ByVal objProfileGroup As ProfileGroup) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    For Each item As DealerProfile In objListProfile
                        item.Dealer = objDomain
                        Dim oldProfile As DealerProfile = GetDealerProfile(objDomain, objProfileGroup, item.ProfileHeader)
                        If oldProfile.ID > 0 Then
                            For Each items As DealerProfileHistory In item.DealerProfileHistorys
                                items.DealerProfile = oldProfile
                                objTransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile.ProfileValue = item.ProfileValue
                            objTransactionManager.AddUpdate(oldProfile, m_userPrincipal.Identity.Name)
                        Else
                            item.Dealer = objDomain
                            objTransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            For Each items As DealerProfileHistory In item.DealerProfileHistorys
                                items.DealerProfile = item
                                objTransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                            Next
                            oldProfile.ProfileValue = item.ProfileValue
                        End If

                    Next

                    objTransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        objTransactionManager.PerformTransaction()
                        returnValue = 0
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

        ' 09-Aug-2007   Deddy H     Penambahan func baru tuk handle profile
        Public Function GetDealerProfile(ByVal objDealer As Dealer, ByVal objGroup As ProfileGroup, ByVal objDomain As ProfileHeader) As DealerProfile
            Dim objFacade As DealerProfileFacade = New DealerProfileFacade(System.Threading.Thread.CurrentPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerProfile), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(DealerProfile), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))
            criterias.opAnd(New Criteria(GetType(DealerProfile), "ProfileHeader.ID", MatchType.Exact, objDomain.ID))
            Dim objListSalesmanProfile As ArrayList = objFacade.Retrieve(criterias)
            If objListSalesmanProfile.Count > 0 Then
                Return CType(objListSalesmanProfile(0), DealerProfile)
            End If
            Return New DealerProfile
        End Function

        Public Function UpdateDealer(ByVal objDomain As KTB.DNet.Domain.Dealer, ByVal arrBusinessArea As ArrayList, ByVal arrVehicleCategory As ArrayList, ByVal arrDealerProfile As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrBusinessArea.Count > 0 Then
                        For Each objBusinessArea As BusinessArea In arrBusinessArea
                            If objBusinessArea.RowStatus = 0 Then
                                objBusinessArea.Dealer = objDomain
                                objTransactionManager.AddInsert(objBusinessArea, m_userPrincipal.Identity.Name)
                            Else
                                If objBusinessArea.RowStatus = 1 Then
                                    objBusinessArea.RowStatus = 0
                                Else
                                    objBusinessArea.RowStatus = -1
                                End If
                                objTransactionManager.AddUpdate(objBusinessArea, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    'dealerProfile
                    If arrDealerProfile.Count > 0 Then
                        For Each objDealerProfile As DealerProfile In arrDealerProfile
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(DealerProfile), "ProfileHeader.ID", MatchType.Exact, objDealerProfile.ProfileHeader.ID))
                            criterias.opAnd(New Criteria(GetType(DealerProfile), "Dealer.ID", MatchType.Exact, objDealerProfile.Dealer.ID))
                            criterias.opAnd(New Criteria(GetType(DealerProfile), "ProfileGroup.ID", MatchType.Exact, objDealerProfile.ProfileGroup.ID))
                            Dim arlDealerProfile As ArrayList = New DealerProfileFacade(m_userPrincipal).Retrieve(criterias)
                            If arlDealerProfile.Count > 0 Then
                                For Each objDealerProfileUpd As DealerProfile In arlDealerProfile
                                    objDealerProfileUpd.ProfileValue = objDealerProfile.ProfileValue
                                    objTransactionManager.AddUpdate(objDealerProfileUpd, m_userPrincipal.Identity.Name)
                                Next
                            Else
                                objTransactionManager.AddInsert(objDealerProfile, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    'vehicleCategory
                    If arrVehicleCategory.Count > 0 Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerCategory), "Dealer.ID", MatchType.Exact, CType(objDomain.ID, Integer)))

                        Dim DealerCategoryFac As DealerCategoryFacade = New DealerCategoryFacade(m_userPrincipal)
                        Dim arrDealerCategory As ArrayList = DealerCategoryFac.Retrieve(criterias)
                        If arrDealerCategory.Count = 0 Then
                            For Each item As String In arrVehicleCategory
                                Dim objDealerCategory As New DealerCategory
                                objDealerCategory.Dealer = objDomain
                                objDealerCategory.Category = New CategoryFacade(m_userPrincipal).Retrieve(CType(item, Integer))
                                objTransactionManager.AddInsert(objDealerCategory, m_userPrincipal.Identity.Name)
                            Next
                        Else
                            'cek category based on registered in DealerCategory
                            For Each objDealerCategory As DealerCategory In arrDealerCategory
                                Dim existCategory As Boolean = False
                                For Each item As String In arrVehicleCategory
                                    If objDealerCategory.Category.ID = New CategoryFacade(m_userPrincipal).Retrieve(CType(item, Integer)).ID Then
                                        existCategory = True
                                    End If
                                Next
                                If existCategory Then
                                    objDealerCategory.RowStatus = 0
                                    objTransactionManager.AddUpdate(objDealerCategory, m_userPrincipal.Identity.Name)
                                Else
                                    objDealerCategory.RowStatus = -1
                                    objTransactionManager.AddUpdate(objDealerCategory, m_userPrincipal.Identity.Name)
                                End If
                            Next
                            'cek new category
                            For Each item As String In arrVehicleCategory
                                Dim existCategory As Boolean = False
                                For Each objDealerCategory As DealerCategory In arrDealerCategory
                                    If objDealerCategory.Category.ID = New CategoryFacade(m_userPrincipal).Retrieve(CType(item, Integer)).ID Then
                                        existCategory = True
                                    End If
                                Next
                                If Not existCategory Then
                                    Dim objDealerCategory As New DealerCategory
                                    objDealerCategory.Dealer = objDomain
                                    objDealerCategory.Category = New CategoryFacade(m_userPrincipal).Retrieve(CType(item, Integer))
                                    objTransactionManager.AddInsert(objDealerCategory, m_userPrincipal.Identity.Name)
                                End If
                            Next

                        End If
                    End If

                    objTransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    objTransactionManager.PerformTransaction()
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
        Public Function UpdateTransactionControl(ByVal arrTC As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim objTCHistory As TransactionControlHistory
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrTC.Count > 0 Then
                        For Each objTC As TransactionControl In arrTC
                            If objTC.ID > 0 Then
                                'objTC.RowStatus = DBRowStatus.Active
                                objTransactionManager.AddUpdate(objTC, m_userPrincipal.Identity.Name)
                                objTCHistory = New TransactionControlHistory
                                objTCHistory.TransactionControl = objTC
                                objTCHistory.TransactionControl.Dealer = objTC.Dealer
                                If objTC.UpdateTransHistory Then
                                    If objTC.Status = EnumDealerStatus.DealerStatus.Aktive Then
                                        objTCHistory.StatusTo = EnumDealerStatus.DealerStatus.Aktive
                                        objTCHistory.StatusFrom = EnumDealerStatus.DealerStatus.NonAktive
                                    Else
                                        objTCHistory.StatusTo = EnumDealerStatus.DealerStatus.NonAktive
                                        objTCHistory.StatusFrom = EnumDealerStatus.DealerStatus.Aktive
                                    End If
                                    objTransactionManager.AddInsert(objTCHistory, m_userPrincipal.Identity.Name)
                                End If
                            Else
                                objTransactionManager.AddInsert(objTC, m_userPrincipal.Identity.Name)
                                objTCHistory = New TransactionControlHistory
                                objTC.MarkLoaded()
                                objTCHistory.TransactionControl = objTC
                                objTCHistory.TransactionControl.Dealer = objTC.Dealer
                                'objTCHistory.TransactionControl.Dealer = objTC.Dealer
                                If objTC.UpdateTransHistory Then
                                    If objTC.Status = EnumDealerStatus.DealerStatus.Aktive Then
                                        objTCHistory.StatusTo = EnumDealerStatus.DealerStatus.Aktive
                                        objTCHistory.StatusFrom = -1
                                    Else
                                        objTCHistory.StatusTo = EnumDealerStatus.DealerStatus.NonAktive
                                        objTCHistory.StatusFrom = -1
                                    End If
                                    objTransactionManager.AddInsert(objTCHistory, m_userPrincipal.Identity.Name)
                                End If

                            End If

                        Next
                    End If
                    objTransactionManager.PerformTransaction()
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

        Public Function UpdateTransactionControlPK(ByVal arrTC As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim objTCHistory As TransactionControlPKHistory
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrTC.Count > 0 Then
                        For Each objTC As TransactionControlPK In arrTC
                            If objTC.ID > 0 Then
                                'objTC.RowStatus = DBRowStatus.Active
                                objTransactionManager.AddUpdate(objTC, m_userPrincipal.Identity.Name)
                                objTCHistory = New TransactionControlPKHistory
                                objTCHistory.TransactionControlPK = objTC
                                objTCHistory.TransactionControlPK.Dealer = objTC.Dealer
                                If objTC.UpdateTransHistory Then
                                    If objTC.Status = EnumDealerStatus.DealerStatus.Aktive Then
                                        objTCHistory.StatusTo = EnumDealerStatus.DealerStatus.Aktive
                                        objTCHistory.StatusFrom = EnumDealerStatus.DealerStatus.NonAktive
                                    Else
                                        objTCHistory.StatusTo = EnumDealerStatus.DealerStatus.NonAktive
                                        objTCHistory.StatusFrom = EnumDealerStatus.DealerStatus.Aktive
                                    End If
                                    objTransactionManager.AddInsert(objTCHistory, m_userPrincipal.Identity.Name)
                                End If
                            Else
                                objTransactionManager.AddInsert(objTC, m_userPrincipal.Identity.Name)
                                objTCHistory = New TransactionControlPKHistory
                                objTC.MarkLoaded()
                                objTCHistory.TransactionControlPK = objTC
                                objTCHistory.TransactionControlPK.Dealer = objTC.Dealer
                                'objTCHistory.TransactionControlPK.Dealer = objTC.Dealer
                                If objTC.UpdateTransHistory Then
                                    If objTC.Status = EnumDealerStatus.DealerStatus.Aktive Then
                                        objTCHistory.StatusTo = EnumDealerStatus.DealerStatus.Aktive
                                        objTCHistory.StatusFrom = -1
                                    Else
                                        objTCHistory.StatusTo = EnumDealerStatus.DealerStatus.NonAktive
                                        objTCHistory.StatusFrom = -1
                                    End If
                                    objTransactionManager.AddInsert(objTCHistory, m_userPrincipal.Identity.Name)
                                End If

                            End If

                        Next
                    End If
                    objTransactionManager.PerformTransaction()
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
        Public Function Update(ByVal objDomain As City) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DealerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Update(ByVal objDomain As Dealer) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DealerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.Dealer) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.Dealer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.Dealer).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is TransactionControlHistory) Then
                CType(InsertArg.DomainObject, TransactionControlHistory).id = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is TransactionControl) Then
                CType(InsertArg.DomainObject, TransactionControl).ID = InsertArg.ID
                ' 09-Aug-2007   Deddy H     Penambahan untuk profile
            ElseIf (TypeOf InsertArg.DomainObject Is DealerProfile) Then
                CType(InsertArg.DomainObject, DealerProfile).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.DealerProfile).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is DealerProfileHistory) Then
                CType(InsertArg.DomainObject, DealerProfileHistory).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is TransactionControlPKHistory) Then '151210
                CType(InsertArg.DomainObject, TransactionControlPKHistory).id = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is TransactionControlPK) Then
                CType(InsertArg.DomainObject, TransactionControlPK).ID = InsertArg.ID
            End If

        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Try
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, Code))
                Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.Dealer), "DealerCode", AggregateType.Count)

                Return CType(m_DealerMapper.RetrieveScalar(agg, crit), Integer)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try

            Return Nothing
        End Function

#End Region

#Region "Helper Method"



#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DealerMapper.RetrieveByCriteria(criterias, sorts)
        End Function


        Public Function ValidateBlockedTransactionControl(ByVal nDealerID As Integer, ByVal strTransactionType As String) As Integer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Dealer.ID", MatchType.Exact, nDealerID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Kind", MatchType.Exact, strTransactionType))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TransactionControl), "Status", MatchType.Exact, "0"))
            Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.TransactionControl), "Kind", AggregateType.Count)
            Return CType(m_TransactionControlMapper.RetrieveScalar(agg, criterias), Integer)

        End Function

        Public Shared Function GenerateDealerCodeSelection(ByVal objDealer As Dealer, ByVal User As IPrincipal) As String
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.UserName", MatchType.Exact, User.Identity.Name.Substring(6)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.Dealer.ID", MatchType.Exact, objDealer.ID))
            Dim ArlUserOrgAssignment As ArrayList = New UserOrgAssignmentFacade(User).Retrieve(criterias)
            Dim strDealerCodeColection As String = "('" & objDealer.DealerCode
            If ArlUserOrgAssignment.Count > 0 Then
                For Each item As UserOrgAssignment In ArlUserOrgAssignment
                    strDealerCodeColection = strDealerCodeColection & "','" & item.Dealer.DealerCode
                Next
            End If
            strDealerCodeColection = strDealerCodeColection & "')"

            Return strDealerCodeColection
        End Function

        Public Shared Function GenerateDealerIDSelection(ByVal objDealer As Dealer, ByVal User As IPrincipal) As String
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.UserName", MatchType.Exact, User.Identity.Name.Substring(6)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserOrgAssignment), "UserInfo.Dealer.ID", MatchType.Exact, objDealer.ID))
            Dim ArlUserOrgAssignment As ArrayList = New UserOrgAssignmentFacade(User).Retrieve(criterias)
            Dim strDealerCodeColection As String = String.Empty
            If ArlUserOrgAssignment.Count > 0 Then
                strDealerCodeColection = "("
                For Each item As UserOrgAssignment In ArlUserOrgAssignment
                    strDealerCodeColection = strDealerCodeColection & item.Dealer.ID.ToString() & ", "
                Next
                strDealerCodeColection = strDealerCodeColection.Remove(strDealerCodeColection.Length - 2, 2)
                strDealerCodeColection = strDealerCodeColection & ")"
            Else
                strDealerCodeColection = "(0)"
            End If

            Return strDealerCodeColection
        End Function

        Public Function GenerateDealerIDSelection(ByVal strDealerCode As String) As String
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, "('" & strDealerCode.Replace(";", "','") & "')"))
            Dim arrDealer As ArrayList = m_DealerMapper.RetrieveByCriteria(criterias)
            Dim strDealerCodeColection As String = String.Empty
            If arrDealer.Count > 0 Then
                strDealerCodeColection = "("
                For Each item As Dealer In arrDealer
                    strDealerCodeColection = strDealerCodeColection & item.ID.ToString() & ", "
                Next
                strDealerCodeColection = strDealerCodeColection.Remove(strDealerCodeColection.Length - 2, 2)
                strDealerCodeColection = strDealerCodeColection & ")"
            Else
                strDealerCodeColection = "(0)"
            End If

            Return strDealerCodeColection
        End Function

        Public Function RecordCount(ByVal aggregate As IAggregate, ByVal criteria As ICriteria) As Integer
            Return CType(m_UserInfoMapper.RetrieveScalar(aggregate, criteria), Integer)
        End Function

        Public Function ValidateDealer(ByVal oDealer As Dealer) As Dealer
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, oDealer.DealerCode))
            Dim arlDealer As ArrayList = m_DealerMapper.RetrieveByCriteria(criteria)
            If arlDealer.Count > 0 Then
                Return CType(arlDealer(0), Dealer)
            End If
            Return Nothing
        End Function

        Private Function GetValueID(ByVal category As String, ByVal code As String) As Integer
            Dim result As Integer = 0

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StandardCode), "ValueCode", MatchType.Exact, code))
            criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, category))

            Dim arrFacility As ArrayList = m_StandardCode.RetrieveByCriteria(criterias)
            If arrFacility.Count > 0 Then
                result = CType(arrFacility(0), StandardCode).ValueId

            End If

            Return result
        End Function

        Public Function InsertFromWebSevice(ByVal oDealer As Dealer) As Short
            Dim returnValue As Integer = -1
            Dim isChange As New IsChangeFacade
            If Me.IsTaskFree() Then
                Try
                    '  Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim isNewDealer As Boolean = False
                    Dim oDealer_old As Dealer = ValidateDealer(oDealer)

                    If IsNothing(oDealer_old) Then
                        isNewDealer = True
                        'If insert new data dealer then CreditAccount set 000000
                        'oDealer.CreditAccount = "000000"
                        objTransactionManager.AddInsert(oDealer, m_userPrincipal.Identity.Name)

                        If IsNothing(oDealer.MainDealer) Then
                            'Dim oDealer2 As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(oDealer.ID)
                            'If Not IsNothing(oDealer2) Then
                            '    oDealer2.MainDealer = oDealer
                            '    objTransactionManager.AddUpdate(oDealer2, m_userPrincipal.Identity.Name)
                            'End If
                            'If oDealer.OrganizationBranchType = 1 Then
                            '    Dim odealer2 As Dealer = oDealer
                            '    odealer2.MainDealer = oDealer
                            '    objTransactionManager.AddUpdate(odealer2, m_userPrincipal.Identity.Name)
                            'Else


                        End If

                        If oDealer.BusinessAreas.Count > 0 Then
                            For Each itemDetail As BusinessArea In oDealer.BusinessAreas
                                itemDetail.Dealer = oDealer
                                objTransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                        If oDealer.DealerPajaks.Count > 0 Then
                            For Each itemDetail As DealerPajak In oDealer.DealerPajaks
                                itemDetail.Dealer = oDealer
                                objTransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                        If oDealer.DealerBankAccounts.Count > 0 Then
                            For Each itemDetail As DealerBankAccount In oDealer.DealerBankAccounts
                                itemDetail.Dealer = oDealer
                                itemDetail.Status = CType(EnumMasterDealer.Status.Active, Short)
                                objTransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                        If oDealer.DealerFacility.Length > 0 Then
                            For idx As Integer = 1 To oDealer.DealerFacility.Length
                                Dim vCode As String = oDealer.DealerFacility.Substring(idx - 1, 1)
                                Dim objFacility As New Dealerfacility
                                objFacility.Dealer = oDealer
                                objFacility.Facility = GetValueID("DealerFacility", vCode)
                                objTransactionManager.AddInsert(objFacility, m_userPrincipal.Identity.Name)
                            Next
                        End If
                        If oDealer.DealerStallEquipment.Length > 0 Then
                            For idx As Integer = 1 To oDealer.DealerStallEquipment.Length
                                Dim vCode As String = oDealer.DealerStallEquipment.Substring(idx - 1, 1)
                                Dim objStall As New DealerStallEquipment
                                objStall.Dealer = oDealer
                                objStall.StallEquipment = GetValueID("DealerStallEquipment", vCode)
                                objTransactionManager.AddInsert(objStall, m_userPrincipal.Identity.Name)
                            Next
                        End If
                        If oDealer.DealerPaymentMethod.Length > 0 Then
                            For idx As Integer = 1 To oDealer.DealerPaymentMethod.Length
                                Dim vCode As String = oDealer.DealerPaymentMethod.Substring(idx - 1, 1)
                                Dim objPayM As New DealerPaymentMethod
                                objPayM.Dealer = oDealer
                                objPayM.PaymentMethod = GetValueID("PaymentMethod", vCode)
                                objTransactionManager.AddInsert(objPayM, m_userPrincipal.Identity.Name)
                            Next
                        End If
                        If oDealer.ServiceGrade.Length > 0 Or oDealer.EquipmentClass.Length > 0 Then
                            Dim objDealerAdditional As New DealerAdditional
                            objDealerAdditional.Dealer = oDealer
                            objDealerAdditional.EquipmentClass = GetValueID("EquipmentClass", oDealer.EquipmentClass)
                            objDealerAdditional.ServiceGrade = GetValueID("ServiceGrade", oDealer.ServiceGrade)
                            objTransactionManager.AddInsert(objDealerAdditional, m_userPrincipal.Identity.Name)

                        End If

                        If oDealer.DealerOperationAreaBusiness.Count > 0 Then
                            For Each objBA As DealerOperationAreaBussiness In oDealer.DealerOperationAreaBusiness
                                objBA.Dealer = oDealer
                                objTransactionManager.AddInsert(objBA, m_userPrincipal.Identity.Name)
                            Next
                        End If
                        If oDealer.DealerCategorys.Count > 0 Then
                            For Each objCtg As DealerCategory In oDealer.DealerCategorys
                                objCtg.Dealer = oDealer
                                objTransactionManager.AddInsert(objCtg, m_userPrincipal.Identity.Name)
                            Next
                        End If

                    Else
                        'For Each itemDetail As BusinessArea In oDealer.BusinessAreas
                        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        '    criterias.opAnd(New Criteria(GetType(BusinessArea), "Dealer.ID", MatchType.Exact, oDealer_old.ID))
                        '    criterias.opAnd(New Criteria(GetType(BusinessArea), "Kind", MatchType.Exact, itemDetail.Kind))
                        '    Dim arlBusinessAreas As ArrayList = New BusinessAreaFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(criterias)

                        '    If arlBusinessAreas.Count > 0 Then
                        '        Dim objBusinessArea As BusinessArea = CType(arlBusinessAreas(0), BusinessArea)
                        '        objBusinessArea.Kind = itemDetail.Kind
                        '        objBusinessArea.Title = itemDetail.Title
                        '        objBusinessArea.ContactPerson = itemDetail.ContactPerson
                        '        objBusinessArea.Email = itemDetail.Email
                        '        objBusinessArea.Phone = itemDetail.Phone
                        '        objBusinessArea.Fax = itemDetail.Fax
                        '        objBusinessArea.HP = itemDetail.HP
                        '        objBusinessArea.RowStatus = CType(DBRowStatus.Active, Short)
                        '        objTransactionManager.AddUpdate(objBusinessArea, m_userPrincipal.Identity.Name)
                        '    Else
                        '        itemDetail.Dealer = oDealer_old
                        '        objTransactionManager.AddInsert(itemDetail, m_userPrincipal.Identity.Name)
                        '    End If
                        'Next

                        For Each _objDealerPajak As DealerPajak In oDealer.DealerPajaks
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPajak), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(DealerPajak), "Dealer.ID", MatchType.Exact, oDealer_old.ID))
                            Dim arlDealerPajaks As ArrayList = New DealerPajakFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(criterias)

                            If arlDealerPajaks.Count > 0 Then
                                Dim objDealerPajak As DealerPajak = CType(arlDealerPajaks(0), DealerPajak)
                                objDealerPajak.NPWP = _objDealerPajak.NPWP
                                objDealerPajak.KPP = _objDealerPajak.KPP
                                objDealerPajak.Pejabat1 = _objDealerPajak.Pejabat1
                                objDealerPajak.Jabatan1 = _objDealerPajak.Jabatan1
                                objDealerPajak.Pejabat2 = _objDealerPajak.Pejabat2
                                objDealerPajak.Jabatan2 = _objDealerPajak.Jabatan2
                                objDealerPajak.Pejabat3 = _objDealerPajak.Pejabat3
                                objDealerPajak.Jabatan3 = _objDealerPajak.Jabatan3
                                objDealerPajak.RowStatus = CType(DBRowStatus.Active, Short)
                                objTransactionManager.AddUpdate(objDealerPajak, m_userPrincipal.Identity.Name)
                            Else
                                _objDealerPajak.Dealer = oDealer_old
                                objTransactionManager.AddInsert(_objDealerPajak, m_userPrincipal.Identity.Name)
                            End If
                        Next

                        For Each _objDealerBankAccount As DealerBankAccount In oDealer.DealerBankAccounts
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "Dealer.ID", MatchType.Exact, oDealer_old.ID))
                            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "BankAccount", MatchType.Exact, _objDealerBankAccount.BankAccount))
                            criterias.opAnd(New Criteria(GetType(DealerBankAccount), "BankKey", MatchType.Exact, _objDealerBankAccount.BankKey))
                            Dim arlDealerBankAccounts As ArrayList = New DealerBankAccountFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(criterias)

                            If arlDealerBankAccounts.Count > 0 Then
                                Dim objDealerBankAccount As DealerBankAccount = CType(arlDealerBankAccounts(0), DealerBankAccount)
                                objDealerBankAccount.BankKey = _objDealerBankAccount.BankKey
                                objDealerBankAccount.BankAccount = _objDealerBankAccount.BankAccount
                                objDealerBankAccount.BankName = _objDealerBankAccount.BankName
                                objDealerBankAccount.BeneficiaryName = _objDealerBankAccount.BeneficiaryName
                                objDealerBankAccount.Status = CType(EnumMasterDealer.Status.Active, Short)
                                objDealerBankAccount.RowStatus = CType(DBRowStatus.Active, Short)
                                objTransactionManager.AddUpdate(objDealerBankAccount, m_userPrincipal.Identity.Name)
                            Else
                                _objDealerBankAccount.Dealer = oDealer_old
                                _objDealerBankAccount.RowStatus = CType(DBRowStatus.Active, Short)
                                objTransactionManager.AddInsert(_objDealerBankAccount, m_userPrincipal.Identity.Name)
                            End If
                        Next

                        For Each ifacility As Dealerfacility In oDealer_old.DealerFacilitys
                            ifacility.RowStatus = -1
                            ifacility.Dealer = oDealer_old
                            objTransactionManager.AddUpdate(ifacility, m_userPrincipal.Identity.Name)
                        Next
                        If oDealer.DealerFacility.Length > 0 Then
                            For idx As Integer = 1 To oDealer.DealerFacility.Length
                                Dim vCode As String = oDealer.DealerFacility.Substring(idx - 1, 1)
                                Dim objFacility As New Dealerfacility
                                objFacility.Dealer = oDealer_old
                                objFacility.Facility = GetValueID("DealerFacility", vCode)
                                objTransactionManager.AddInsert(objFacility, m_userPrincipal.Identity.Name)
                            Next
                        End If


                        For Each iStall As DealerStallEquipment In oDealer_old.DealerStallEquipments
                            iStall.RowStatus = -1
                            iStall.Dealer = oDealer_old
                            objTransactionManager.AddUpdate(iStall, m_userPrincipal.Identity.Name)
                        Next
                        If oDealer.DealerStallEquipment.Length > 0 Then
                            For idx As Integer = 1 To oDealer.DealerStallEquipment.Length
                                Dim vCode As String = oDealer.DealerStallEquipment.Substring(idx - 1, 1)
                                Dim objStall As New DealerStallEquipment
                                objStall.Dealer = oDealer_old
                                objStall.StallEquipment = GetValueID("DealerStallEquipment", vCode)
                                objTransactionManager.AddInsert(objStall, m_userPrincipal.Identity.Name)
                            Next
                        End If

                        For Each iPayM As DealerPaymentMethod In oDealer_old.DealerPaymentMethods
                            iPayM.RowStatus = -1
                            iPayM.Dealer = oDealer_old
                            objTransactionManager.AddUpdate(iPayM, m_userPrincipal.Identity.Name)
                        Next
                        If oDealer.DealerPaymentMethod.Length > 0 Then
                            For idx As Integer = 1 To oDealer.DealerPaymentMethod.Length
                                Dim vCode As String = oDealer.DealerPaymentMethod.Substring(idx - 1, 1)
                                Dim objPayM As New DealerPaymentMethod
                                objPayM.Dealer = oDealer_old
                                objPayM.PaymentMethod = GetValueID("PaymentMethod", vCode)
                                objTransactionManager.AddInsert(objPayM, m_userPrincipal.Identity.Name)
                            Next
                        End If
                        If oDealer.ServiceGrade.Length > 0 Or oDealer.EquipmentClass.Length > 0 Then
                            Dim objDealerAdditional As New DealerAdditional
                            If oDealer_old.DealerAdditionals.Count > 0 Then
                                objDealerAdditional = CType(oDealer_old.DealerAdditionals(0), DealerAdditional)
                                objDealerAdditional.Dealer = oDealer_old
                                objDealerAdditional.EquipmentClass = GetValueID("EquipmentClass", oDealer.EquipmentClass)
                                objDealerAdditional.ServiceGrade = GetValueID("ServiceGrade", oDealer.ServiceGrade)
                                objDealerAdditional.LastUpdateBy = "ws"
                                objTransactionManager.AddUpdate(objDealerAdditional, m_userPrincipal.Identity.Name)

                            Else
                                objDealerAdditional.Dealer = oDealer_old
                                objDealerAdditional.EquipmentClass = GetValueID("EquipmentClass", oDealer.EquipmentClass)
                                objDealerAdditional.ServiceGrade = GetValueID("ServiceGrade", oDealer.ServiceGrade)
                                objTransactionManager.AddInsert(objDealerAdditional, m_userPrincipal.Identity.Name)
                            End If


                        End If

                        If oDealer.DealerOperationAreaBusiness.Count > 0 Then
                            For Each objBA As DealerOperationAreaBussiness In oDealer.DealerOperationAreaBusiness
                                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerOperationAreaBussiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias.opAnd(New Criteria(GetType(DealerOperationAreaBussiness), "Dealer.ID", MatchType.Exact, oDealer_old.ID))
                                criterias.opAnd(New Criteria(GetType(DealerOperationAreaBussiness), "AreaBusiness", MatchType.Exact, objBA.AreaBusiness))
                                criterias.opAnd(New Criteria(GetType(DealerOperationAreaBussiness), "DealerOperation", MatchType.Exact, objBA.DealerOperation))
                                Dim arlBA As ArrayList = New DealerOperationAreaBussinessFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(criterias)

                                If arlBA.Count = 0 Then
                                    objBA.Dealer = oDealer_old
                                    objTransactionManager.AddInsert(objBA, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                        If oDealer.DealerCategorys.Count > 0 Then
                            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crits.opAnd(New Criteria(GetType(DealerCategory), "Dealer.ID", MatchType.Exact, oDealer_old.ID))

                            Dim arlOldCtg As ArrayList = New DealerCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(crits)
                            Dim listNewCtg As List(Of DealerCategory) = oDealer.DealerCategorys.Cast(Of DealerCategory).ToList()
                            For Each iCtg As DealerCategory In arlOldCtg
                                If listNewCtg.Where(Function(x) x.Category.ID = iCtg.Category.ID).Count = 0 Then
                                    iCtg.RowStatus = CType(DBRowStatus.Deleted, Short)
                                    objTransactionManager.AddUpdate(iCtg, m_userPrincipal.Identity.Name)
                                End If
                            Next

                            For Each objCtg As DealerCategory In oDealer.DealerCategorys
                                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias.opAnd(New Criteria(GetType(DealerCategory), "Dealer.ID", MatchType.Exact, oDealer_old.ID))
                                criterias.opAnd(New Criteria(GetType(DealerCategory), "Category.ID", MatchType.Exact, objCtg.Category.ID))
                                Dim arlCtg As ArrayList = New DealerCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(criterias)

                                If arlCtg.Count = 0 Then
                                    objCtg.Dealer = oDealer_old
                                    objTransactionManager.AddInsert(objCtg, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If

                        If oDealer_old.DealerCode.Trim <> oDealer.DealerCode.Trim OrElse
                                oDealer_old.DealerName.Trim <> oDealer.DealerName.Trim OrElse
                                oDealer_old.Status.Trim <> oDealer.Status.Trim OrElse
                                oDealer_old.Title.Trim <> oDealer.Title.Trim OrElse
                                oDealer_old.OrganizationBranchType <> oDealer.OrganizationBranchType OrElse
                                oDealer_old.CreditAccount <> oDealer.CreditAccount OrElse
                                oDealer_old.SearchTerm1.Trim <> oDealer.SearchTerm1.Trim OrElse
                                oDealer_old.SearchTerm2.Trim <> oDealer.SearchTerm2.Trim OrElse
                                (Not IsNothing(oDealer_old.DealerGroup) AndAlso Not IsNothing(oDealer.DealerGroup) AndAlso oDealer_old.DealerGroup.ID <> oDealer.DealerGroup.ID) OrElse
                                oDealer_old.Address.Trim <> oDealer.Address.Trim OrElse
                                (Not IsNothing(oDealer_old.City) AndAlso Not IsNothing(oDealer.City) AndAlso oDealer_old.City.ID <> oDealer.City.ID) OrElse
                                oDealer_old.ZipCode.Trim <> oDealer.ZipCode.Trim OrElse
                                (Not IsNothing(oDealer_old.Province) AndAlso Not IsNothing(oDealer.Province) AndAlso oDealer_old.Province.ID <> oDealer.Province.ID) OrElse
                                oDealer_old.Phone.Trim <> oDealer.Phone.Trim OrElse
                                oDealer_old.Fax.Trim <> oDealer.Fax.Trim OrElse
                                oDealer_old.Website.Trim <> oDealer.Website.Trim OrElse
                                oDealer_old.Email.Trim <> oDealer.Email.Trim OrElse
                                oDealer_old.SalesUnitFlag.Trim <> oDealer.SalesUnitFlag.Trim OrElse
                                oDealer_old.ServiceFlag.Trim <> oDealer.ServiceFlag.Trim OrElse
                                oDealer_old.SparepartFlag.Trim <> oDealer.SparepartFlag.Trim OrElse
                                (Not IsNothing(oDealer_old.Area1) AndAlso Not IsNothing(oDealer.Area1) AndAlso oDealer_old.Area1.ID <> oDealer.Area1.ID) OrElse
                                (Not IsNothing(oDealer_old.Area2) AndAlso Not IsNothing(oDealer.Area2) AndAlso oDealer_old.Area2.ID <> oDealer.Area2.ID) OrElse
                                oDealer_old.SPANumber.Trim <> oDealer.SPANumber.Trim OrElse
                                Format(oDealer_old.SPADate, "dd/MM/yyyy") <> Format(oDealer.SPADate, "dd/MM/yyyy") OrElse
                                oDealer_old.FreePPh22Indicator <> oDealer.FreePPh22Indicator OrElse
                                Format(oDealer_old.FreePPh22From, "dd/MM/yyyy") <> Format(oDealer.FreePPh22From, "dd/MM/yyyy") OrElse
                                Format(oDealer_old.FreePPh22To, "dd/MM/yyyy") <> Format(oDealer.FreePPh22To, "dd/MM/yyyy") OrElse
                                oDealer_old.LegalStatus.Trim <> oDealer.LegalStatus.Trim OrElse
                                oDealer_old.WSCNO.Trim <> oDealer.WSCNO.Trim OrElse
                                oDealer_old.SortKey.Trim <> oDealer.SortKey.Trim OrElse
                                oDealer_old.ReconAccount.Trim <> oDealer.ReconAccount.Trim OrElse
                                oDealer_old.PaymentBlock.Trim <> oDealer.PaymentBlock.Trim OrElse
                                oDealer_old.TaxCode1.Trim <> oDealer.TaxCode1.Trim OrElse
                                oDealer_old.CashManagementGroup.Trim <> oDealer.CashManagementGroup.Trim OrElse
                                Format(oDealer_old.DueDate, "dd/MM/yyyy") <> Format(oDealer.DueDate, "dd/MM/yyyy") OrElse
                                oDealer_old.AgreementNo.Trim <> oDealer.AgreementNo.Trim OrElse
                                Format(oDealer_old.AgreementDate, "dd/MM/yyyy") <> Format(oDealer.AgreementDate, "dd/MM/yyyy") OrElse
                                oDealer_old.CreditAccount.Trim <> oDealer.CreditAccount.Trim OrElse
                                oDealer_old.RowStatus <> oDealer.RowStatus Then

                            ''--- Process Update Dealer
                            oDealer_old.DealerCode = oDealer.DealerCode.Trim
                            oDealer_old.DealerName = oDealer.DealerName.Trim
                            'If IsNothing(oDealer.MainDealer) And oDealer.OrganizationBranchType = 1 Then
                            '    oDealer_old.MainDealer = oDealer_old
                            'Else
                            '    oDealer_old.MainDealer = oDealer.MainDealer
                            'End If
                            oDealer_old.Status = oDealer.Status.Trim
                            oDealer_old.Title = oDealer.Title.Trim
                            oDealer_old.SearchTerm1 = oDealer.SearchTerm1.Trim
                            oDealer_old.SearchTerm2 = oDealer.SearchTerm2.Trim
                            If oDealer.CreditAccount <> "" Then
                                oDealer_old.CreditAccount = oDealer.CreditAccount.Trim
                            End If

                            oDealer_old.DealerGroup = oDealer.DealerGroup
                            oDealer_old.ParentDealer = oDealer.ParentDealer
                            oDealer_old.CustomerLegal = oDealer.CustomerLegal
                            oDealer_old.Address = oDealer.Address.Trim
                            oDealer_old.City = oDealer.City
                            oDealer_old.ZipCode = oDealer.ZipCode.Trim
                            oDealer_old.Province = oDealer.Province
                            oDealer_old.Phone = oDealer.Phone.Trim
                            oDealer_old.Fax = oDealer.Fax.Trim
                            oDealer_old.Website = oDealer.Website.Trim
                            oDealer_old.Email = oDealer.Email.Trim
                            oDealer_old.SalesUnitFlag = oDealer.SalesUnitFlag.Trim
                            oDealer_old.ServiceFlag = oDealer.ServiceFlag.Trim
                            oDealer_old.SparepartFlag = oDealer.SparepartFlag.Trim
                            oDealer_old.Area1 = oDealer.Area1
                            oDealer_old.Area2 = oDealer.Area2
                            oDealer_old.MainArea = oDealer.MainArea
                            oDealer_old.SPANumber = oDealer.SPANumber.Trim
                            oDealer_old.SPADate = oDealer.SPADate
                            oDealer_old.FreePPh22Indicator = oDealer.FreePPh22Indicator
                            oDealer_old.FreePPh22From = oDealer.FreePPh22From
                            oDealer_old.FreePPh22To = oDealer.FreePPh22To
                            oDealer_old.LegalStatus = oDealer.LegalStatus.Trim
                            oDealer_old.DueDate = oDealer.DueDate
                            oDealer_old.AgreementNo = oDealer.AgreementNo.Trim
                            oDealer_old.AgreementDate = oDealer.AgreementDate
                            oDealer_old.CreditAccount = oDealer_old.CreditAccount.Trim
                            oDealer_old.WSCNO = oDealer.WSCNO
                            oDealer_old.ReconAccount = oDealer.ReconAccount
                            oDealer_old.PaymentBlock = oDealer.PaymentBlock
                            oDealer_old.SortKey = oDealer.SortKey
                            oDealer_old.CashManagementGroup = oDealer.CashManagementGroup
                            oDealer_old.TaxCode1 = oDealer.TaxCode1
                            oDealer_old.OrganizationBranchType = oDealer.OrganizationBranchType     'penambahan CR Body & Paint
                            oDealer_old.RowStatus = DBRowStatus.Active

                            '-- CR DigitalizeProduct ---
                            oDealer_old.NickNameDigital = oDealer.NickNameDigital
                            oDealer_old.NickNameEcommerce = oDealer.NickNameEcommerce
                            oDealer_old.Longitude = oDealer.Longitude
                            oDealer_old.Latitude = oDealer.Latitude
                            oDealer_old.Publish = oDealer.Publish

                            objTransactionManager.AddUpdate(oDealer_old, m_userPrincipal.Identity.Name)
                        End If



                    End If

                    If performTransaction Then
                        objTransactionManager.PerformTransaction()
                        returnValue = 0
                    End If
                    If isNewDealer Then
                        If oDealer.OrganizationBranchType = 1 Then
                            Dim odealer2 As Dealer = oDealer
                            odealer2.MainDealer = oDealer
                            returnValue = m_DealerMapper.Update(odealer2, m_userPrincipal.Identity.Name)
                        End If
                        Dim objTC As New TransactionControl
                        objTC.Dealer = oDealer
                        objTC.Kind = 14
                        objTC.DateControl = 25
                        objTC.Status = 0
                        objTC.ID = m_TransactionControlMapper.Insert(objTC, m_userPrincipal.Identity.Name)


                        Dim objTCH As New TransactionControlHistory
                        objTCH.TransactionControl = objTC
                        objTCH.StatusFrom = -1
                        objTCH.StatusTo = 0
                        m_TransactionControlHistoryMapper.Insert(objTCH, m_userPrincipal.Identity.Name)
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    '    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

#End Region

    End Class

#Region "IComparer Class"

    Public Class CompareCityName
        Implements IComparer

        Private m_IsAsc As Boolean

        Public Sub New(ByVal IsAsc As Boolean)
            Me.m_IsAsc = IsAsc
        End Sub

        Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
            Implements IComparer.Compare

            Return IIf(m_IsAsc, (New CaseInsensitiveComparer).Compare(CType(x, Dealer).City.CityName, CType(y, Dealer).City.CityName), _
                (New CaseInsensitiveComparer).Compare(CType(y, Dealer).City.CityName, CType(x, Dealer).City.CityName))
        End Function
    End Class
#End Region

End Namespace
