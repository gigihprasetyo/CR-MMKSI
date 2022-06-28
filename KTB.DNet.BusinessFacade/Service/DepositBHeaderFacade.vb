
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
'// Generated on 3/14/2016 - 11:37:54 AM
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

Namespace KTB.DNET.BusinessFacade.Service

    Public Class DepositBHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DepositBHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DepositBHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(DepositBHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(DepositBHeader))
            Me.DomainTypeCollection.Add(GetType(DepositBDetail))

        End Sub

#End Region

#Region "Retrieve"
        Public Function RetrieveByDealerID(ByVal DealerID As Short, ByVal sortColumn As String, ByVal sortType As Sort.SortDirection) As DepositBHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositBHeader), "Dealer.ID", MatchType.Exact, DealerID))
            'criterias.opAnd(New Criteria(GetType(DepositBHeader), "ProductCategory.ID", MatchType.Exact, ProductCategoryID))
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBHeader), sortColumn, sortType))
            Else
                sortColl = Nothing
            End If

            Dim DealerColl As ArrayList = m_DepositBHeaderMapper.RetrieveByCriteria(criterias, sortColl)
            If (DealerColl.Count > 0) Then
                Return CType(DealerColl(0), DepositBHeader)
            End If
            Return New DepositBHeader
        End Function

        Public Function Retrieve(ByVal ID As Integer) As DepositBHeader
            Return CType(m_DepositBHeaderMapper.Retrieve(ID), DepositBHeader)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositBHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DepositBHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DepositBHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DepositBHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DepositBHeader As ArrayList = m_DepositBHeaderMapper.RetrieveByCriteria(criterias)
            Return _DepositBHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBHeaderColl As ArrayList = m_DepositBHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DepositBHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DepositBHeaderColl As ArrayList = m_DepositBHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DepositBHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DepositBHeaderColl As ArrayList = m_DepositBHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DepositBHeader), columnName, matchOperator, columnValue))
            Return DepositBHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DepositBHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBHeader), columnName, matchOperator, columnValue))

            Return m_DepositBHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is DepositBHeader) Then
                CType(InsertArg.DomainObject, DepositBHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DepositBHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DepositBDetail) Then
                CType(InsertArg.DomainObject, DepositBDetail).ID = InsertArg.ID
            End If
        End Sub


        Public Function Insert(ByVal objDomain As DepositBHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DepositBHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DepositBHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DepositBHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DepositBHeader)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DepositBHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DepositBHeader)
            Try
                m_DepositBHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function InsertTransaction(ByVal objDomain As DepositBHeader) As Integer
            Dim returnValue As Integer = -1

            'If Me.IsTaskFree() Then 'To view error on parser log
            Try
                Me.SetTaskLocking()

                '-- Check to see if the Deposit header already exists
                'If Not isExistDepositHead(oDeposit) Then

                '-- Insert this deposit along with its lines
                m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                For Each item As DepositBDetail In objDomain.DepositBDetails
                    item.DepositBHeader = objDomain
                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                Next
                'Else
                '    ''-- The Deposit header already exists
                '    ''-- Retrieve and assign Deposit header's ID first
                '    'AssignDepositHeaderID(oDeposit)

                '    ''-- Insert or update Deposit lines
                '    'InsertOrUpdateDepositLines(oDeposit)

                '    ''-- Update Deposit header
                '    'm_TransactionManager.AddUpdate(oDeposit, m_userPrincipal.Identity.Name)

                'End If

                m_TransactionManager.PerformTransaction()
                returnValue = 0

            Catch ex As Exception
                If ExceptionPolicy.HandleException(ex, "Domain Policy") Then
                    Throw
                End If

            Finally
                Me.RemoveTaskLocking()
            End Try
            'End If

            Return returnValue
        End Function

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNET.Domain.DepositBHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    
                    For Each detail As DepositBDetail In objDomain.DepositBDetails
                        'Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositBDetail), "DepositBHeader.ID", MatchType.Exact, detail.DepositBHeader.ID))
                        'Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositBDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(DepositBDetail), "DepositBHeader.ID", MatchType.Exact, detail.DepositBHeader.ID))
                        'criterias.opAnd(New Criteria(GetType(DepositBDetail), "Reff", MatchType.Exact, detail.Reff))
                        'criterias.opAnd(New Criteria(GetType(DepositBDetail), "DocumentNumber", MatchType.Exact, detail.DocumentNumber))

                        'Dim objDepositBDetaillList As ArrayList = New DepositBDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        'Dim objDetail As DepositBDetail = New DepositBDetail
                        'If objDepositBDetaillList.Count > 0 Then
                        '    objDetail = objDepositBDetaillList(0)
                        '    If objDetail.ID > 0 Then
                        '        objDetail.Amount = detail.Amount
                        '        objDetail.Description = detail.Description
                        '        objDetail.RowStatus = CType(DBRowStatus.Active, Short)
                        '        m_TransactionManager.AddUpdate(objDetail, m_userPrincipal.Identity.Name)
                        '    End If
                        'Else
                        '    detail.DepositBHeader = objDomain
                        '    m_TransactionManager.AddInsert(detail, m_userPrincipal.Identity.Name)
                        'End If
                        If detail.ID > 0 Then
                            m_TransactionManager.AddUpdate(detail, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(detail, m_userPrincipal.Identity.Name)
                        End If
                    Next
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

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
#End Region

    End Class

End Namespace

