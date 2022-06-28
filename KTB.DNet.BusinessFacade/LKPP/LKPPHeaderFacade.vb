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
Imports KTB.DNet.BusinessFacade

#End Region



Namespace KTB.DNet.BusinessFacade.LKPP

    Public Class LKPPHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_LKPPHeaderMapper As IMapper
        Private m_LKPPDealerMapper As IMapper
        Private m_LKPPDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager
        Private m_userPrincipal As IPrincipal = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_LKPPHeaderMapper = MapperFactory.GetInstance().GetMapper(GetType(LKPPHeader).ToString)
            Me.m_LKPPDealerMapper = MapperFactory.GetInstance().GetMapper(GetType(LKPPDealer).ToString)
            Me.m_LKPPDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(LKPPDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.LKPPHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.LKPPDealer))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.LKPPDetail))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As LKPPHeader
            Return CType(m_LKPPHeaderMapper.Retrieve(ID), LKPPHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As LKPPHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(LKPPHeader), "ReferenceNumber", MatchType.Exact, Code))

            Dim LKPPHeaderColl As ArrayList = m_LKPPHeaderMapper.RetrieveByCriteria(criterias)
            If (LKPPHeaderColl.Count > 0) Then
                Return CType(LKPPHeaderColl(0), LKPPHeader)
            End If
            Return New LKPPHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_LKPPHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_LKPPHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_LKPPHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LKPPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LKPPHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LKPPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LKPPHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _LKPPHeader As ArrayList = m_LKPPHeaderMapper.RetrieveByCriteria(criterias)
            Return _LKPPHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LKPPHeaderColl As ArrayList = m_LKPPHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LKPPHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(LKPPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim LKPPHeaderColl As ArrayList = m_LKPPHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return LKPPHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(LKPPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim LKPPHeaderColl As ArrayList = m_LKPPHeaderMapper.RetrieveByCriteria(criterias, sortColl)
            Return LKPPHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LKPPHeaderColl As ArrayList = m_LKPPHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(LKPPHeader), columnName, matchOperator, columnValue))
            Return LKPPHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LKPPHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPHeader), columnName, matchOperator, columnValue))

            Return m_LKPPHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveDealerID(ByVal ID As Integer)
            Dim objReturnValue As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(LKPPDealer), "LKPPHeader.ID", MatchType.Exact, ID))
            Dim List As ArrayList
            List = m_LKPPDealerMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = List
                End If
            End If
            Return objReturnValue
        End Function

        Public Function RetrieveLKPPDetail(ByVal ID As Integer)
            Dim objReturnValue As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(LKPPDetail), "LKPPHeader.ID", MatchType.Exact, ID))
            Dim List As ArrayList
            List = m_LKPPDetailMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = List
                End If
            End If
            Return objReturnValue
        End Function
#End Region

#Region "Transaction/Other Public Method"


#Region "Insert"
        Public Function InsertLKPPHeader(ByVal objDomain As LKPPHeader, ByVal objDetails As ArrayList, ByVal objDealers As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If objDetails.Count > 0 Then
                        For Each items As LKPPDetail In objDetails
                            items.LKPPHeader = objDomain
                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If objDealers.Count > 0 Then
                        For Each items As LKPPDealer In objDealers
                            items.LKPPHeader = objDomain
                            m_TransactionManager.AddInsert(items, m_userPrincipal.Identity.Name)
                        Next
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
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function Insert(ByVal objDomain As LKPPHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                m_LKPPHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function
#End Region
#Region "Update"
        Public Function Update(ByVal objDomain As LKPPHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_LKPPHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function ValidateLKPPHeader(ByVal objDomain As LKPPHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    'returnValue = m_LKPPHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
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

        Public Function UpdateLKPPHeader(ByVal objDomain As LKPPHeader) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    If objDomain.LKPPDetails.Count > 0 Then
                        For Each objLKPPDetail As LKPPDetail In objDomain.LKPPDetails
                            'objLKPPDetail.LKPPHeader = objDomain
                            'If objLKPPDetail.ID <> 0 Then
                            '    m_TransactionManager.AddUpdate(objLKPPDetail, m_userPrincipal.Identity.Name)
                            'Else
                            m_TransactionManager.AddInsert(objLKPPDetail, m_userPrincipal.Identity.Name)
                            'End If
                        Next
                    End If

                    If objDomain.LKPPDealers.Count > 0 Then
                        For Each objLKPPDealer As LKPPDealer In objDomain.LKPPDealers
                            If Not IsNothing(objLKPPDealer.Dealer) Then
                                'objLKPPDealer.LKPPHeader = objDomain
                                If objLKPPDealer.ID <> 0 Then
                                    objLKPPDealer.LKPPHeader = objDomain
                                    m_TransactionManager.AddUpdate(objLKPPDealer, m_userPrincipal.Identity.Name)
                                Else
                                    objLKPPDealer.LKPPHeader = objDomain
                                    m_TransactionManager.AddInsert(objLKPPDealer, m_userPrincipal.Identity.Name)
                                End If
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    'returnValue = m_LKPPHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)

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

        Public Function UpdateLKPPHeader(ByVal objDomain As LKPPHeader, ByVal arrDetail As ArrayList, ByVal arrDealer As ArrayList) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    If arrDetail.Count > 0 Then
                        For Each objLKPPDetail As LKPPDetail In arrDetail
                            objLKPPDetail.LKPPHeader = objDomain
                            '   If objLKPPDetail.ID <> 0 Then
                            'm_TransactionManager.AddUpdate(objLKPPDetail, m_userPrincipal.Identity.Name)
                            '  Else
                            m_TransactionManager.AddInsert(objLKPPDetail, m_userPrincipal.Identity.Name)
                            '  End If
                        Next
                    End If

                    If arrDealer.Count > 0 Then

                        ' _LKPPHeaderFacade.DeleteOLDDealer(_OldLKPPDealer)

                        For Each objLKPPDealer As LKPPDealer In arrDealer
                            If Not IsNothing(objLKPPDealer.Dealer) Then

                                m_TransactionManager.AddDelete(objLKPPDealer)

                                objLKPPDealer.LKPPHeader = objDomain

                                m_TransactionManager.AddInsert(objLKPPDealer, m_userPrincipal.Identity.Name)

                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    'returnValue = m_LKPPHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)

                    'If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                    'End If
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
#Region "Delete"
        Public Sub Delete(ByVal objDomain As LKPPHeader)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_LKPPHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As LKPPHeader) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_LKPPHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function DeleteOLDDealer(ByVal arrDealer As ArrayList) As Integer
            Dim iReturn As Integer = -1
            Try
                For Each _dealer As LKPPDealer In arrDealer
                    Dim objDomain As LKPPDealer = _dealer
                    iReturn = m_LKPPDealerMapper.Delete(objDomain)
                Next

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function DeleteOLDLKPPDetail(ByVal arrDetail As ArrayList) As Integer
            Dim iReturn As Integer = -1
            Try
                For Each _detail As LKPPDetail In arrDetail
                    Dim objDomain As LKPPDetail = _detail
                    iReturn = m_LKPPDetailMapper.Delete(objDomain)
                Next

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region


        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LKPPHeader), "ReferenceNumber", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(LKPPHeader), "ReferenceNumber", AggregateType.Count)
            Return CType(m_LKPPHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.LKPPHeader) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.LKPPHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.LKPPHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is LKPPDetail) Then
                CType(InsertArg.DomainObject, LKPPDetail).ID = InsertArg.ID
            ElseIf (TypeOf InsertArg.DomainObject Is LKPPDealer) Then
                CType(InsertArg.DomainObject, LKPPDealer).ID = InsertArg.ID
            End If
        End Sub

        Public Function validateLKPP(ByVal lkppColl As ArrayList)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As LKPPHeader In lkppColl
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

#End Region

    End Class

End Namespace

