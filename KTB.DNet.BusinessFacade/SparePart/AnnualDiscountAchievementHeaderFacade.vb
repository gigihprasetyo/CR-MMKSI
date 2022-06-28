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
'// Generated on 11/09/2005 - 9:04:49 AM
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

Namespace KTB.DNet.BusinessFacade.Sparepart

    Public Class AnnualDiscountAchievementHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AnnualDiscountAchievementHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_AnnualDiscountAchievementHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(AnnualDiscountAchievementHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.AnnualDiscountAchievementHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.EquipmentSalesDetail))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.EquipmentSalesPayment))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AnnualDiscountAchievementHeader
            Return CType(m_AnnualDiscountAchievementHeaderMapper.Retrieve(ID), AnnualDiscountAchievementHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As AnnualDiscountAchievementHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AnnualDiscountAchievementHeader), "RegPONumber", MatchType.Exact, Code))

            Dim AnnualDiscountAchievementHeaderColl As ArrayList = m_AnnualDiscountAchievementHeaderMapper.RetrieveByCriteria(criterias)
            If (AnnualDiscountAchievementHeaderColl.Count > 0) Then
                Return CType(AnnualDiscountAchievementHeaderColl(0), AnnualDiscountAchievementHeader)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AnnualDiscountAchievementHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AnnualDiscountAchievementHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AnnualDiscountAchievementHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(AnnualDiscountAchievementHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AnnualDiscountAchievementHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(AnnualDiscountAchievementHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AnnualDiscountAchievementHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AnnualDiscountAchievementHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AnnualDiscountAchievementHeaderColl As ArrayList = m_AnnualDiscountAchievementHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AnnualDiscountAchievementHeaderColl
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AnnualDiscountAchievementHeader As ArrayList = m_AnnualDiscountAchievementHeaderMapper.RetrieveByCriteria(criterias)
            Return _AnnualDiscountAchievementHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AnnualDiscountAchievementHeaderColl As ArrayList = m_AnnualDiscountAchievementHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AnnualDiscountAchievementHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AnnualDiscountAchievementHeaderColl As ArrayList = m_AnnualDiscountAchievementHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return AnnualDiscountAchievementHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AnnualDiscountAchievementHeaderColl As ArrayList = m_AnnualDiscountAchievementHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AnnualDiscountAchievementHeader), columnName, matchOperator, columnValue))
            Return AnnualDiscountAchievementHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(AnnualDiscountAchievementHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievementHeader), columnName, matchOperator, columnValue))

            Return m_AnnualDiscountAchievementHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievementHeader), "AnnualDiscountAchievementHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(AnnualDiscountAchievementHeader), "AnnualDiscountAchievementHeaderCode", AggregateType.Count)
            Return CType(m_AnnualDiscountAchievementHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Sub Update(ByVal objDomain As AnnualDiscountAchievementHeader)
            Try
                m_AnnualDiscountAchievementHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.AnnualDiscountAchievementHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    For Each item As AnnualDiscountAchievement In objDomain.AnnualDiscountAchievements
                        item.AnnualDiscountAchievementHeader = objDomain
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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.AnnualDiscountAchievementHeader) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.AnnualDiscountAchievementHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.AnnualDiscountAchievementHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is EquipmentSalesDetail) Then

                CType(InsertArg.DomainObject, EquipmentSalesDetail).ID = InsertArg.ID

                'ElseIf (TypeOf InsertArg.DomainObject Is EquipmentSalesPayment) Then

                '    CType(InsertArg.DomainObject, EquipmentSalesPayment).ID = InsertArg.ID

            End If

        End Sub

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.AnnualDiscountAchievementHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    'For Each item As EquipmentSalesPayment In objDomain.EquipmentSalesPayments
                    '    item.AnnualDiscountAchievementHeader = objDomain
                    '    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    'Next

                    For Each item As AnnualDiscountAchievement In objDomain.AnnualDiscountAchievements
                        item.AnnualDiscountAchievementHeader = objDomain
                        If item.ID <> 0 Then
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        End If

                    Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    'For i As Integer = 0 To objDomain.EquipmentSalesDetail.Count - 1
                    'CType(objDomain.EquipmentSalesDetail(i), EquipmentSalesDetail).AnnualDiscountAchievementHeader = objDomain
                    'm_TransactionManager.AddInsert(objDomain.EquipmentSalesDetail(i), m_userPrincipal.Identity.Name)
                    'Next
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

        Public Sub Delete(ByVal objDomain As AnnualDiscountAchievementHeader)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                For Each item As AnnualDiscountAchievement In objDomain.AnnualDiscountAchievements
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                Next
                UpdateTransaction(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function UpdateEquipmentStatus(ByVal eqColl As ArrayList)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As AnnualDiscountAchievementHeader In eqColl
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next

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
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function ShyncroniseAnnualDiscount(ByVal objDomain As KTB.DNet.Domain.AnnualDiscountAchievementHeader) As Integer
            Try

            Dim objAnnualDiscountAchievementHeader As AnnualDiscountAchievementHeader = RetrieveAnnualDiscontAchievementHeader(objDomain)
                If objAnnualDiscountAchievementHeader Is Nothing Then
                    Insert(objDomain)
                Else
                     For Each item As AnnualDiscountAchievement In objDomain.AnnualDiscountAchievements
                        Dim discountAchFacade As AnnualDiscountAchievementFacade = New AnnualDiscountAchievementFacade(System.Threading.Thread.CurrentPrincipal)
                        discountAchFacade.SychronizeAnnualDiscountAchivement(item, objAnnualDiscountAchievementHeader)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Private Function RetrieveAnnualDiscontAchievementHeader(ByVal _annualDiscountHeader As AnnualDiscountAchievementHeader) As AnnualDiscountAchievementHeader
            Dim _AnnualDiscountAchievementHeaderFacade As AnnualDiscountAchievementHeaderFacade
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievementHeader), "DealerCode", MatchType.Exact, _annualDiscountHeader.DealerCode))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievementHeader), "ValidateDateFrom", MatchType.Exact, _annualDiscountHeader.ValidateDateFrom))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievementHeader), "ValidateDateTo", MatchType.Exact, _annualDiscountHeader.ValidateDateTo))
            _AnnualDiscountAchievementHeaderFacade = New AnnualDiscountAchievementHeaderFacade(System.Threading.Thread.CurrentPrincipal)
            Dim collAnnualDiscount As ArrayList = _AnnualDiscountAchievementHeaderFacade.Retrieve(criterias)
            If collAnnualDiscount.Count > 0 Then
                Dim objADC As AnnualDiscountAchievementHeader = CType(collAnnualDiscount(0), AnnualDiscountAchievementHeader)
                Return objADC
            Else
                Return Nothing
            End If
        End Function




#End Region

    End Class

End Namespace

